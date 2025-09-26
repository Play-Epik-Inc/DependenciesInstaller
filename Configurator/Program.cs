using Configurator.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configurator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Config());
        }

        public static async Task BuildApplication(string path, Label buildLogs, ProgressBar progressBar, Button baseButton, Button executeButton)
        {
            //Variables
            string baseFolderPath, gamePath;
            DateTime timeStarted = DateTime.Now, timeEnded;

            //Setting-up the UI
            buildLogs.Visible = true;
            progressBar.Visible = true;
            baseButton.Visible = false;

            //Control if path == null
            if (path == null || path.Trim() == "")
            {
                MessageBox.Show("Error during the build of Installer.exe, Error 404: Folder Not Found or Empty.", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buildLogs.Text = $"{GetCurrentDate()}: ERROR 404, Folder not found.";
                buildLogs.ForeColor = Color.Red;
            }
            else
            {
                //Starting up the build
                buildLogs.ForeColor = Color.DarkCyan;
                buildLogs.Text = $"{GetCurrentDate()}: Starting building Installer.exe...";
                progressBar.Value = 20;

                await Task.Delay(2500);

                //Get base directory path
                baseFolderPath = Path.GetDirectoryName(path);
                buildLogs.Text = $"{GetCurrentDate()}: Finding base Path...";
                Console.WriteLine(GetCurrentDate() + ": Base Folder: " + baseFolderPath);
                progressBar.Value = 40;

                await Task.Delay(1000);

                //Create "Game" folder into that
                gamePath = Path.Combine(path, "Game");
                Console.WriteLine(GetCurrentDate() + ": Game path: " + gamePath);
                if (!Directory.Exists(gamePath))
                    Directory.CreateDirectory(gamePath);
                else
                    MessageBox.Show($"A folder with the name of \"Game\" already exists, the program will copy files into that folder.", "Build Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buildLogs.Text = $"{GetCurrentDate()}: Creating Game folder...";
                progressBar.Value = 50;

                await Task.Delay(1500);

                //Copy game_files into the new "Game" folder
                int availableFiles = Directory.GetFiles(path).Length, currentCopied = 0;
                string[] files = Directory.GetFiles(path);
                List<String> filesToMove = new List<string>();

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    if (fileName != "Game")
                        filesToMove.Add(file);
                }

                foreach(string fileToMove in filesToMove)
                {
                    string fileName = Path.GetFileName(fileToMove);
                    string dstFile = Path.Combine(gamePath, fileName);

                    //UI Update
                    currentCopied++;
                    buildLogs.Text = $"{GetCurrentDate()}: Copying files ({currentCopied}/{availableFiles})...";

                    //Avoid conflicts
                    if(File.Exists(dstFile))
                        File.Delete(dstFile);

                    //Error handling
                    try {File.Move(fileToMove, dstFile);}
                    catch (Exception ex)
                    {
                        await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                        throw new BuildFailedException(buildLogs, ex);
                    }
                }
                progressBar.Value = 60;

                await Task.Delay(2000);

                //Move sub_folders also
                int currentlyMoved = 0;
                string[] folders = Directory.GetDirectories(path);

                foreach (string folderPath in folders)
                {
                    string folderName = Path.GetFileName(folderPath);
                    if (folderName != "Game")
                    {
                        string dstFolder = Path.Combine(gamePath, folderName);

                        try
                        {
                            if (Directory.Exists(dstFolder))
                                Directory.Delete(dstFolder, true);

                            Directory.Move(folderPath, dstFolder);
                            currentlyMoved++;

                            buildLogs.Text = $"{GetCurrentDate()}: Moving folders ({folders.Length-1}/{currentlyMoved})...";
                        }
                        catch (Exception ex)
                        {
                            await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                            throw new BuildFailedException(buildLogs, ex);
                        }
                    }
                }
                progressBar.Value = 70;

                await Task.Delay(2000);

                //Get Installer.exe and installing it.
                string applicationDirectory = Directory.GetCurrentDirectory();
                string exePersistentDirectory = Path.Combine(applicationDirectory, "Installer.exe");
                string destinationPath = Path.Combine(path, "Installer.exe");

                buildLogs.Text = $"{GetCurrentDate()}: Installing \"Installer.exe\"...";

                try
                {
                    if (File.Exists(destinationPath))
                    {
                        await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                        throw new BuildFailedException(buildLogs, null);
                    }
                    else
                        File.Copy(exePersistentDirectory, destinationPath);
                }
                catch (Exception ex)
                {
                    await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                    throw new BuildFailedException(buildLogs, ex);
                }
                progressBar.Value = 80;

                await Task.Delay(1000);

                //Install Install_EasyAntiCheat.bat into Game folder
                string batPersistentDirectory = Path.Combine(applicationDirectory, "Install_EasyAntiCheat.bat");
                buildLogs.Text = $"{GetCurrentDate()}: Installing \"Install_EasyAntiCheat.bat\" (886796dcf0f723)...";

                try
                {
                    if (File.Exists(Path.Combine(gamePath, "Install_EasyAntiCheat.bat")))
                    {
                        MessageBox.Show($"An error occurred during the build and now is canceled, Error: Installer.exe already exists.", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buildLogs.ForeColor = Color.Red;
                        buildLogs.AutoEllipsis = true;
                        buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: Installer.exe already exists.";
                        await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                    }
                    else 
                        File.Copy(batPersistentDirectory, Path.Combine(gamePath, "Install_EasyAntiCheat.bat"));
                }
                catch (Exception ex)
                {
                    await ResetUI(buildLogs, progressBar, executeButton, baseButton);
                    throw new BuildFailedException(buildLogs, ex);
                }
                progressBar.Value = 90;

                await Task.Delay(1000);

                //Build completed.
                timeEnded = DateTime.Now;

                string timeElapsedMinutes = (timeEnded - timeStarted).Minutes.ToString();
                string timeElapsedSeconds;

                //Format output for Seconds
                timeElapsedSeconds = ((timeEnded - timeStarted).Seconds < 10) ? $"0{(timeEnded - timeStarted).Seconds.ToString()}" : (timeEnded - timeStarted).Seconds.ToString();

                //Setting-up the UI
                progressBar.Value = 100;
                buildLogs.ForeColor = Color.Green;
                executeButton.Visible = true;
                buildLogs.Text = $"{GetCurrentDate()}: BUILD SUCCEEDED! (Time elapsed: {timeElapsedMinutes}:{timeElapsedSeconds})";

                //Reset UI
                await ResetUI(buildLogs, progressBar, executeButton, baseButton);
            }
        }

        public static void Execute(string path)
        {
            //Get executable path from base path.
            string executablePath = Path.Combine(path, "Installer.exe");

            try
            {
                //Create the ProcessStartInfo element with UseShellExecute set to false, however this doesn't work.
                var psi = new ProcessStartInfo
                {
                    FileName = executablePath,
                    UseShellExecute = false
                };

                Process.Start(psi); //Start the Process
            }
            catch (Win32Exception ex)
            {
                //Catch the Win32Exception for non-base privilegies of Configurator App.
                throw new ExecuteFailedException(ex);
            }
        }

        public static async Task ResetUI(Label logs, ProgressBar bar, Button executeButton, Button buildButton)
        {
            await Task.Delay(5000);
            logs.Visible = false;
            bar.Visible = false;
            executeButton.Visible = false;
            buildButton.Visible = true;
        }
        public static string GetCurrentDate() {return $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";}
    }
}
