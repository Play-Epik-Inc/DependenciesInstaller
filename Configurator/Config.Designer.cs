using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Configurator
{
    partial class Config
    {
        //variables
        public static string selectedPath = null;

        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.labelStart = new System.Windows.Forms.Label();
            this.gameFolder = new System.Windows.Forms.Label();
            this.browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonChangeFolder = new System.Windows.Forms.Button();
            this.folderSelected = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.buildButton = new System.Windows.Forms.Button();
            this.buildLogs = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.executeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelStart.Font = new System.Drawing.Font("Clash Display Medium", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStart.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelStart.Location = new System.Drawing.Point(46, 9);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(703, 38);
            this.labelStart.TabIndex = 0;
            this.labelStart.Text = "Configure your game for Easy-AntiCheat";
            // 
            // gameFolder
            // 
            this.gameFolder.AutoSize = true;
            this.gameFolder.Font = new System.Drawing.Font("Clash Display Medium", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameFolder.Location = new System.Drawing.Point(48, 77);
            this.gameFolder.Name = "gameFolder";
            this.gameFolder.Size = new System.Drawing.Size(172, 26);
            this.gameFolder.TabIndex = 1;
            this.gameFolder.Text = "Game Folder: ";
            // 
            // buttonChangeFolder
            // 
            this.buttonChangeFolder.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonChangeFolder.Font = new System.Drawing.Font("Clash Display Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChangeFolder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonChangeFolder.Location = new System.Drawing.Point(627, 77);
            this.buttonChangeFolder.Name = "buttonChangeFolder";
            this.buttonChangeFolder.Size = new System.Drawing.Size(146, 31);
            this.buttonChangeFolder.TabIndex = 2;
            this.buttonChangeFolder.Text = "Change";
            this.buttonChangeFolder.UseMnemonic = false;
            this.buttonChangeFolder.UseVisualStyleBackColor = false;
            this.buttonChangeFolder.Click += new System.EventHandler(this.buttonChangeFolder_Click);
            // 
            // folderSelected
            // 
            this.folderSelected.AutoSize = true;
            this.folderSelected.Font = new System.Drawing.Font("Clash Display Medium", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderSelected.Location = new System.Drawing.Point(226, 77);
            this.folderSelected.Name = "folderSelected";
            this.folderSelected.Size = new System.Drawing.Size(223, 26);
            this.folderSelected.TabIndex = 3;
            this.folderSelected.Text = "No folder selected";
            // 
            // infoLabel
            // 
            this.infoLabel.Font = new System.Drawing.Font("Clash Display Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.ForeColor = System.Drawing.Color.Red;
            this.infoLabel.Location = new System.Drawing.Point(2, 383);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(799, 42);
            this.infoLabel.TabIndex = 4;
            this.infoLabel.Text = "Warning! This version uses the  \"fb01b16ec199f4\" version of Easy \r\nAnti-Cheat, pl" +
    "ease activate this build before publish this version.";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buildButton
            // 
            this.buildButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buildButton.Font = new System.Drawing.Font("Clash Display Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buildButton.Location = new System.Drawing.Point(204, 322);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(374, 48);
            this.buildButton.TabIndex = 5;
            this.buildButton.Text = "Build";
            this.buildButton.UseMnemonic = false;
            this.buildButton.UseVisualStyleBackColor = false;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // buildLogs
            // 
            this.buildLogs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buildLogs.Cursor = System.Windows.Forms.Cursors.Default;
            this.buildLogs.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buildLogs.Font = new System.Drawing.Font("Clash Display Medium", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildLogs.ForeColor = System.Drawing.Color.DarkCyan;
            this.buildLogs.Location = new System.Drawing.Point(-1, 284);
            this.buildLogs.Name = "buildLogs";
            this.buildLogs.Size = new System.Drawing.Size(802, 17);
            this.buildLogs.TabIndex = 6;
            this.buildLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buildLogs.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.progressBar.Location = new System.Drawing.Point(274, 304);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(238, 12);
            this.progressBar.Step = 0;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 7;
            this.progressBar.Value = 20;
            this.progressBar.Visible = false;
            // 
            // executeButton
            // 
            this.executeButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.executeButton.Font = new System.Drawing.Font("Clash Display Medium", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.executeButton.Location = new System.Drawing.Point(317, 246);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(149, 35);
            this.executeButton.TabIndex = 8;
            this.executeButton.Text = "Execute";
            this.executeButton.UseMnemonic = false;
            this.executeButton.UseVisualStyleBackColor = false;
            this.executeButton.Visible = false;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buildLogs);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.folderSelected);
            this.Controls.Add(this.buttonChangeFolder);
            this.Controls.Add(this.gameFolder);
            this.Controls.Add(this.labelStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.Text = "Configurator";
            this.Load += new System.EventHandler(this.Config_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //Components
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label gameFolder;
        private System.Windows.Forms.FolderBrowserDialog browserDialog;
        private System.Windows.Forms.Button buttonChangeFolder;
        private System.Windows.Forms.Label folderSelected;

        //Open dialog box for choose the actual folder
        private void OpenFolderBrowser()
        {
            //Adding depscription to dialog
            browserDialog.Description = "Select the Game Folder of your game for continue.";

            //Get if dialog is closed and get the Folder Selected
            if(browserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = browserDialog.SelectedPath;

                //Update the text of gameFolder and add auto-ellipsis
                folderSelected.AutoEllipsis = true;
                folderSelected.AutoSize = false;
                folderSelected.Width = 350;
                folderSelected.Text = selectedPath;
            }
        }

        //Start build the app
        private async void StartBuilding() {await Program.BuildApplication(selectedPath, buildLogs, progressBar, buildButton, executeButton);}

        private void ExecuteProgram() {Program.Execute(selectedPath);}

        private Label infoLabel;
        private Button buildButton;
        private Label buildLogs;
        private ProgressBar progressBar;
        private Button executeButton;
    }
}