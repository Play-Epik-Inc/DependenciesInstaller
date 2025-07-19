using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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

        public static void BuildApplication(string path, Label buildLogs, ErrorProvider errorDialog, ProgressBar progressBar, Button baseButton)
        {
            //Variables
            string baseFolderPath, gamePath;
            DateTime timeStarted = DateTime.Now, timeEnded;

            //Setting-up the UI
            buildLogs.Visible = true;
            progressBar.Visible = true;
            baseButton.Enabled = false;
            baseButton.Cursor = Cursors.WaitCursor;

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

                Thread.Sleep(3000);
                
                //Get base directory path
                baseFolderPath = Path.GetDirectoryName(path);
                buildLogs.Text = $"{GetCurrentDate()}: Finding base Path...";
                Console.WriteLine(GetCurrentDate() + ": Base Folder: " + baseFolderPath);
                progressBar.Value = 40;

                Thread.Sleep(1000);

                //Create "Game" folder into that
                gamePath = Path.Combine(path, "Game");
                Console.WriteLine(GetCurrentDate() + ": Game path: " + gamePath);
                if (!Directory.Exists(gamePath))
                    Directory.CreateDirectory(gamePath);
                else
                    MessageBox.Show($"A folder with the name of \"Game\" already exist, the program will copy files into that folder.", "Build Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buildLogs.Text = $"{GetCurrentDate()}: Creating Game folder...";
                progressBar.Value = 50;

                Thread.Sleep(1500);

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
                        MessageBox.Show($"An error occured during the build and now is canceled, Error: {ex}", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buildLogs.ForeColor = Color.Red;
                        buildLogs.AutoEllipsis = true;
                        buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: {ex}";
                        ResetUI(buildLogs, progressBar);
                        break;
                    }
                }
                progressBar.Value = 60;

                Thread.Sleep(2000);

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
                            MessageBox.Show($"An error occurred during the build and now is canceled, ERROR: '{folderName}': {ex.Message}", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            buildLogs.ForeColor = Color.Red;
                            buildLogs.Text = $"{GetCurrentDate()}: FOLDER MOVE FAILED, Error: {ex.Message}";
                            ResetUI(buildLogs, progressBar);
                            break;
                        }
                    }
                }
                progressBar.Value = 70;

                Thread.Sleep(2000);

                //Get Installer.exe and installing it.
                string applicationDirectory = Directory.GetCurrentDirectory();
                string exePersistentDirectory = Path.Combine(applicationDirectory, "Installer.exe");
                string destinationPath = Path.Combine(path, "Installer.exe");

                buildLogs.Text = $"{GetCurrentDate()}: Installing \"Installer.exe\"...";

                try
                {
                    if (File.Exists(destinationPath))
                    {
                        MessageBox.Show($"An error occured during the build and now is canceled, Error: Installer.exe already exists.", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buildLogs.ForeColor = Color.Red;
                        buildLogs.AutoEllipsis = true;
                        buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: Installer.exe already exists.";
                        ResetUI(buildLogs, progressBar);
                    }
                    else
                        File.Copy(exePersistentDirectory, destinationPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured during the build and now is canceled, Error: {ex}", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    buildLogs.ForeColor = Color.Red;
                    buildLogs.AutoEllipsis = true;
                    buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: {ex}";
                    ResetUI(buildLogs, progressBar);
                }
                progressBar.Value = 80;

                Thread.Sleep(1000);

                //Install Install_EasyAntiCheat.bat into Game folder
                string batPersistentDirectory = Path.Combine(applicationDirectory, "Install_EasyAntiCheat.bat");
                buildLogs.Text = $"{GetCurrentDate()}: Installing \"Install_EasyAntiCheat.bat\" (886796dcf0f723)...";

                try
                {
                    if (File.Exists(Path.Combine(gamePath, "Install_EasyAntiCheat.bat")))
                    {
                        MessageBox.Show($"An error occured during the build and now is canceled, Error: Installer.exe already exists.", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        buildLogs.ForeColor = Color.Red;
                        buildLogs.AutoEllipsis = true;
                        buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: Installer.exe already exists.";
                        ResetUI(buildLogs, progressBar);
                    }
                    else 
                        File.Copy(batPersistentDirectory, Path.Combine(gamePath, "Install_EasyAntiCheat.bat"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured during the build and now is canceled, Error: {ex}", "Build Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    buildLogs.ForeColor = Color.Red;
                    buildLogs.AutoEllipsis = true;
                    buildLogs.Text = $"{GetCurrentDate()}: BUILD FAILED, Error: {ex}";
                    ResetUI(buildLogs, progressBar);
                }
                progressBar.Value = 90;

                Thread.Sleep(2000);

                //Build completed.
                timeEnded = DateTime.Now;

                string timeElapsedMinutes = (timeEnded - timeStarted).Minutes.ToString();
                string timeElapsedSeconds;

                //Format output
                if ((timeEnded - timeStarted).Seconds < 10)
                    timeElapsedSeconds = $"0{(timeEnded - timeStarted).Seconds.ToString()}";
                else
                    timeElapsedSeconds = (timeEnded - timeStarted).Seconds.ToString();

                //Setting-up the UI
                progressBar.Value = 100;
                baseButton.Cursor = Cursors.Default;
                baseButton.Enabled = true;
                buildLogs.ForeColor = Color.Green;
                buildLogs.Text = $"{GetCurrentDate()}: BUILD SUCCEDED! (Time elapsed: {timeElapsedMinutes}:{timeElapsedSeconds})";

                //Reset UI
                ResetUI(buildLogs, progressBar);
            }
        }

        public static async void ResetUI(Label logs, ProgressBar bar)
        {
            await Task.Delay(5000);
            logs.Visible = false;
            bar.Visible = false;
        }

        public static string GetCurrentDate() {return $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";}
    }
}
