using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Installer
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            string programPath = AppDomain.CurrentDomain.BaseDirectory;
            string batPath = Path.Combine(programPath, "Game", "Install_EasyAntiCheat.bat");
            string exePath = Path.Combine(programPath, "Game", "start_protected_game.exe");

            string appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appFolder = Path.Combine(appDataRoot, "PlayEpikInstaller");
            Directory.CreateDirectory(appFolder);

            string savedDataPath = Path.Combine(appFolder, "installerLog.txt");

            try
            {
                //STARTING .BAT INSTALLER
                if (!File.Exists(savedDataPath)) //NOTE: CHECK IF IS ALREADY INSTALLED
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = batPath;
                    psi.Verb = "runas";
                    psi.UseShellExecute = true;

                    using (Process p = Process.Start(psi))
                    {
                        p.WaitForExit();
                    }

                    File.WriteAllText(savedDataPath, $"Installation complete: {DateTime.Now}, DO NOT DELETE THIS FILE.");
                }

                //STARTING THE GAME
                Process.Start(exePath);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox(IntPtr.Zero, $"Fatal error: an unknown error occured during the execution of the game, error code: {ex.Message}", "Fatal Error", 0x00000010);
                Environment.Exit(0);
            }
        }
    }
}
