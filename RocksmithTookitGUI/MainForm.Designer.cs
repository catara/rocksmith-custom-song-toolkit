using RocksmithToolkitGUI.DLCManager;

namespace RocksmithToolkitGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // hidden easter egg - commented out bad practice
        //protected void LoadTemplate(string path)
        //{
        //    this.dlcPackageCreator1.LoadTemplateFile(path);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dLCLibraryManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DLCManagerTab = new System.Windows.Forms.TabPage();
            dlcm = new DLCManager.DLCManager();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            btnUpdate = new System.Windows.Forms.Button();
            toolTip = new System.Windows.Forms.ToolTip(components);
            btnDevTestMethod = new System.Windows.Forms.Button();
            GeneralConfigTab = new System.Windows.Forms.TabPage();
            generalConfig1 = new Config.GeneralConfig();
            zigProConverterTab = new System.Windows.Forms.TabPage();
            zpeConverter1 = new ZpeConverter.ZpeConverter();
            cdlcConverterTab = new System.Windows.Forms.TabPage();
            cdlC2Tab1 = new CDLC2Tab.CDLC2Tab();
            oggConverterTab = new System.Windows.Forms.TabPage();
            oggConverter1 = new OggConverter.OggConverter();
            sngConverterTab = new System.Windows.Forms.TabPage();
            sngConverter1 = new SngConverter.SngConverter();
            dlcInlayCreatorTab = new System.Windows.Forms.TabPage();
            dlcInlayCreator1 = new DLCInlayCreator.DLCInlayCreator();
            dlcConverterTab = new System.Windows.Forms.TabPage();
            dlcConverter1 = new DLCConverter.DLCConverter();
            dlcPackerUnpackerTab = new System.Windows.Forms.TabPage();
            dlcPackerUnpacker1 = new DLCPackerUnpacker.DLCPackerUnpacker();
            dlcPackageCreatorTab = new System.Windows.Forms.TabPage();
            dlcPackageCreator1 = new DLCPackageCreator.DLCPackageCreator();
            tabControl1 = new System.Windows.Forms.TabControl();
            DDCTab = new System.Windows.Forms.TabPage();
            ddc1 = new DDC.DDC();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            GeneralConfigTab.SuspendLayout();
            zigProConverterTab.SuspendLayout();
            cdlcConverterTab.SuspendLayout();
            oggConverterTab.SuspendLayout();
            sngConverterTab.SuspendLayout();
            dlcInlayCreatorTab.SuspendLayout();
            dlcConverterTab.SuspendLayout();
            dlcPackerUnpackerTab.SuspendLayout();
            dlcPackageCreatorTab.SuspendLayout();
            tabControl1.SuspendLayout();
            DDCTab.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, configurationToolStripMenuItem, dLCLibraryManagerToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(592, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { restartToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            restartToolStripMenuItem.Text = "Restart";
            restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { helpToolStripMenuItem, aboutToolStripMenuItem1 });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            aboutToolStripMenuItem1.Text = "About";
            aboutToolStripMenuItem1.Click += aboutToolStripMenuItem1_Click;
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            configurationToolStripMenuItem.Text = "Configuration";
            configurationToolStripMenuItem.Click += configurationToolStripMenuItem_Click;
            // 
            // dLCLibraryManagerToolStripMenuItem
            // 
            dLCLibraryManagerToolStripMenuItem.Name = "dLCLibraryManagerToolStripMenuItem";
            dLCLibraryManagerToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            dLCLibraryManagerToolStripMenuItem.Text = "DLC Library Manager";
            dLCLibraryManagerToolStripMenuItem.Click += dLCLibraryManagerToolStripMenuItem_Click;
            // 
            // DLCManagerTab
            // 
            DLCManagerTab.Controls.Add(dlcm);
            DLCManagerTab.Location = new System.Drawing.Point(4, 24);
            DLCManagerTab.Name = "DLCManagerTab";
            DLCManagerTab.Padding = new System.Windows.Forms.Padding(3);
            DLCManagerTab.Size = new System.Drawing.Size(192, 72);
            DLCManagerTab.TabIndex = 11;
            DLCManagerTab.Text = "Manage DLCs";
            DLCManagerTab.UseVisualStyleBackColor = true;
            // 
            // dlcm
            // 
            dlcm.Location = new System.Drawing.Point(0, 0);
            dlcm.MinimumSize = new System.Drawing.Size(600, 600);
            dlcm.Name = "dlcm";
            dlcm.Size = new System.Drawing.Size(600, 600);
            dlcm.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            pictureBox1.Image = Properties.Resources.toolkit_logo;
            pictureBox1.Location = new System.Drawing.Point(75, 24);
            pictureBox1.Margin = new System.Windows.Forms.Padding(8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(398, 69);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnUpdate.AutoSize = true;
            btnUpdate.BackColor = System.Drawing.SystemColors.Control;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            btnUpdate.Location = new System.Drawing.Point(352, 0);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(240, 42);
            btnUpdate.TabIndex = 0;
            btnUpdate.Text = "Click here to update";
            toolTip.SetToolTip(btnUpdate, "Toolkit Auto Updater Status\r\n\r\nNOTE:\r\nAuto updater uses TLS 1.2\r\nWin10 users may need to \r\nmanually activate TLS 1.2");
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // toolTip
            // 
            toolTip.AutomaticDelay = 100;
            toolTip.AutoPopDelay = 8000;
            toolTip.InitialDelay = 100;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 20;
            // 
            // btnDevTestMethod
            // 
            btnDevTestMethod.Location = new System.Drawing.Point(279, 0);
            btnDevTestMethod.Name = "btnDevTestMethod";
            btnDevTestMethod.Size = new System.Drawing.Size(98, 24);
            btnDevTestMethod.TabIndex = 17;
            btnDevTestMethod.Text = "Dev Use Only";
            toolTip.SetToolTip(btnDevTestMethod, "For Debugging");
            btnDevTestMethod.UseVisualStyleBackColor = true;
            btnDevTestMethod.Visible = false;
            btnDevTestMethod.Click += btnDevTestMethod_Click;
            // 
            // GeneralConfigTab
            // 
            GeneralConfigTab.Controls.Add(generalConfig1);
            GeneralConfigTab.Location = new System.Drawing.Point(4, 24);
            GeneralConfigTab.Name = "GeneralConfigTab";
            GeneralConfigTab.Padding = new System.Windows.Forms.Padding(3);
            GeneralConfigTab.Size = new System.Drawing.Size(192, 72);
            GeneralConfigTab.TabIndex = 9;
            GeneralConfigTab.Text = "General Config";
            GeneralConfigTab.UseVisualStyleBackColor = true;
            // 
            // generalConfig1
            // 
            generalConfig1.Location = new System.Drawing.Point(9, 1);
            generalConfig1.Name = "generalConfig1";
            generalConfig1.Size = new System.Drawing.Size(522, 560);
            generalConfig1.TabIndex = 0;
            generalConfig1.Load += generalConfig1_Load;
            // 
            // zigProConverterTab
            // 
            zigProConverterTab.Controls.Add(zpeConverter1);
            zigProConverterTab.Location = new System.Drawing.Point(4, 24);
            zigProConverterTab.Margin = new System.Windows.Forms.Padding(2);
            zigProConverterTab.Name = "zigProConverterTab";
            zigProConverterTab.Size = new System.Drawing.Size(192, 72);
            zigProConverterTab.TabIndex = 8;
            zigProConverterTab.Text = "Ziggy Pro";
            zigProConverterTab.UseVisualStyleBackColor = true;
            // 
            // zpeConverter1
            // 
            zpeConverter1.Location = new System.Drawing.Point(18, 19);
            zpeConverter1.Name = "zpeConverter1";
            zpeConverter1.Size = new System.Drawing.Size(500, 386);
            zpeConverter1.TabIndex = 0;
            // 
            // cdlcConverterTab
            // 
            cdlcConverterTab.Controls.Add(cdlC2Tab1);
            cdlcConverterTab.Location = new System.Drawing.Point(4, 24);
            cdlcConverterTab.Name = "cdlcConverterTab";
            cdlcConverterTab.Size = new System.Drawing.Size(192, 72);
            cdlcConverterTab.TabIndex = 10;
            cdlcConverterTab.Text = "CDLC 2 Tab";
            cdlcConverterTab.UseVisualStyleBackColor = true;
            // 
            // cdlC2Tab1
            // 
            cdlC2Tab1.Location = new System.Drawing.Point(54, 24);
            cdlC2Tab1.Name = "cdlC2Tab1";
            cdlC2Tab1.Size = new System.Drawing.Size(420, 209);
            cdlC2Tab1.TabIndex = 0;
            // 
            // oggConverterTab
            // 
            oggConverterTab.Controls.Add(oggConverter1);
            oggConverterTab.Location = new System.Drawing.Point(4, 24);
            oggConverterTab.Margin = new System.Windows.Forms.Padding(2);
            oggConverterTab.Name = "oggConverterTab";
            oggConverterTab.Padding = new System.Windows.Forms.Padding(2);
            oggConverterTab.Size = new System.Drawing.Size(192, 72);
            oggConverterTab.TabIndex = 6;
            oggConverterTab.Text = "OGG";
            oggConverterTab.UseVisualStyleBackColor = true;
            // 
            // oggConverter1
            // 
            oggConverter1.Location = new System.Drawing.Point(19, 23);
            oggConverter1.Name = "oggConverter1";
            oggConverter1.Size = new System.Drawing.Size(496, 431);
            oggConverter1.TabIndex = 0;
            // 
            // sngConverterTab
            // 
            sngConverterTab.Controls.Add(sngConverter1);
            sngConverterTab.Location = new System.Drawing.Point(4, 24);
            sngConverterTab.Margin = new System.Windows.Forms.Padding(2);
            sngConverterTab.Name = "sngConverterTab";
            sngConverterTab.Padding = new System.Windows.Forms.Padding(2);
            sngConverterTab.Size = new System.Drawing.Size(192, 72);
            sngConverterTab.TabIndex = 5;
            sngConverterTab.Text = "SNG";
            sngConverterTab.UseVisualStyleBackColor = true;
            // 
            // sngConverter1
            // 
            sngConverter1.Location = new System.Drawing.Point(20, 21);
            sngConverter1.Name = "sngConverter1";
            sngConverter1.Size = new System.Drawing.Size(496, 259);
            sngConverter1.TabIndex = 0;
            // 
            // dlcInlayCreatorTab
            // 
            dlcInlayCreatorTab.Controls.Add(dlcInlayCreator1);
            dlcInlayCreatorTab.Location = new System.Drawing.Point(4, 24);
            dlcInlayCreatorTab.Name = "dlcInlayCreatorTab";
            dlcInlayCreatorTab.Padding = new System.Windows.Forms.Padding(3);
            dlcInlayCreatorTab.Size = new System.Drawing.Size(192, 72);
            dlcInlayCreatorTab.TabIndex = 4;
            dlcInlayCreatorTab.Text = "Inlay Creator";
            dlcInlayCreatorTab.UseVisualStyleBackColor = true;
            // 
            // dlcInlayCreator1
            // 
            dlcInlayCreator1.Location = new System.Drawing.Point(15, 15);
            dlcInlayCreator1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            dlcInlayCreator1.Name = "dlcInlayCreator1";
            dlcInlayCreator1.Size = new System.Drawing.Size(507, 520);
            dlcInlayCreator1.TabIndex = 0;
            // 
            // dlcConverterTab
            // 
            dlcConverterTab.Controls.Add(dlcConverter1);
            dlcConverterTab.Location = new System.Drawing.Point(4, 24);
            dlcConverterTab.Margin = new System.Windows.Forms.Padding(2);
            dlcConverterTab.Name = "dlcConverterTab";
            dlcConverterTab.Padding = new System.Windows.Forms.Padding(2);
            dlcConverterTab.Size = new System.Drawing.Size(192, 72);
            dlcConverterTab.TabIndex = 2;
            dlcConverterTab.Text = "Converter";
            dlcConverterTab.UseVisualStyleBackColor = true;
            // 
            // dlcConverter1
            // 
            dlcConverter1.Location = new System.Drawing.Point(65, 19);
            dlcConverter1.MinimumSize = new System.Drawing.Size(400, 279);
            dlcConverter1.Name = "dlcConverter1";
            dlcConverter1.Size = new System.Drawing.Size(400, 302);
            dlcConverter1.SourcePlatform = null;
            dlcConverter1.TabIndex = 0;
            dlcConverter1.TargetPlatform = null;
            // 
            // dlcPackerUnpackerTab
            // 
            dlcPackerUnpackerTab.Controls.Add(dlcPackerUnpacker1);
            dlcPackerUnpackerTab.Location = new System.Drawing.Point(4, 24);
            dlcPackerUnpackerTab.Margin = new System.Windows.Forms.Padding(2);
            dlcPackerUnpackerTab.Name = "dlcPackerUnpackerTab";
            dlcPackerUnpackerTab.Padding = new System.Windows.Forms.Padding(2);
            dlcPackerUnpackerTab.Size = new System.Drawing.Size(192, 72);
            dlcPackerUnpackerTab.TabIndex = 1;
            dlcPackerUnpackerTab.Text = "Packer/Unpacker";
            dlcPackerUnpackerTab.UseVisualStyleBackColor = true;
            // 
            // dlcPackerUnpacker1
            // 
            dlcPackerUnpacker1.Location = new System.Drawing.Point(43, 14);
            dlcPackerUnpacker1.MinimumSize = new System.Drawing.Size(400, 308);
            dlcPackerUnpacker1.Name = "dlcPackerUnpacker1";
            dlcPackerUnpacker1.Size = new System.Drawing.Size(448, 462);
            dlcPackerUnpacker1.TabIndex = 0;
            // 
            // dlcPackageCreatorTab
            // 
            dlcPackageCreatorTab.Controls.Add(dlcPackageCreator1);
            dlcPackageCreatorTab.Location = new System.Drawing.Point(4, 24);
            dlcPackageCreatorTab.Margin = new System.Windows.Forms.Padding(2);
            dlcPackageCreatorTab.Name = "dlcPackageCreatorTab";
            dlcPackageCreatorTab.Padding = new System.Windows.Forms.Padding(2);
            dlcPackageCreatorTab.Size = new System.Drawing.Size(584, 562);
            dlcPackageCreatorTab.TabIndex = 0;
            dlcPackageCreatorTab.Text = "CDLC Creator";
            dlcPackageCreatorTab.UseVisualStyleBackColor = true;
            // 
            // dlcPackageCreator1
            // 
            dlcPackageCreator1.Album = "";
            dlcPackageCreator1.AlbumSort = "";
            dlcPackageCreator1.AlbumYear = "";
            dlcPackageCreator1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dlcPackageCreator1.Artist = "";
            dlcPackageCreator1.ArtistSort = "";
            dlcPackageCreator1.AutoSize = true;
            dlcPackageCreator1.AverageTempo = "";
            dlcPackageCreator1.DLCKey = "";
            dlcPackageCreator1.JapaneseArtistName = "";
            dlcPackageCreator1.JapaneseSongTitle = "";
            dlcPackageCreator1.Location = new System.Drawing.Point(17, 1);
            dlcPackageCreator1.Name = "dlcPackageCreator1";
            dlcPackageCreator1.PackageVersion = "";
            dlcPackageCreator1.Size = new System.Drawing.Size(557, 560);
            dlcPackageCreator1.SongTitle = "";
            dlcPackageCreator1.SongTitleSort = "";
            dlcPackageCreator1.TabIndex = 0;
            dlcPackageCreator1.Load += dlcPackageCreator1_Load_1;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(dlcPackageCreatorTab);
            tabControl1.Controls.Add(dlcPackerUnpackerTab);
            tabControl1.Controls.Add(dlcConverterTab);
            tabControl1.Controls.Add(DDCTab);
            tabControl1.Controls.Add(dlcInlayCreatorTab);
            tabControl1.Controls.Add(sngConverterTab);
            tabControl1.Controls.Add(oggConverterTab);
            tabControl1.Controls.Add(cdlcConverterTab);
            tabControl1.Controls.Add(zigProConverterTab);
            tabControl1.Controls.Add(GeneralConfigTab);
            tabControl1.Controls.Add(DLCManagerTab);
            tabControl1.Location = new System.Drawing.Point(0, 100);
            tabControl1.Margin = new System.Windows.Forms.Padding(8);
            tabControl1.MinimumSize = new System.Drawing.Size(550, 590);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(592, 590);
            tabControl1.TabIndex = 16;
            // 
            // DDCTab
            // 
            DDCTab.Controls.Add(ddc1);
            DDCTab.Location = new System.Drawing.Point(4, 24);
            DDCTab.Name = "DDCTab";
            DDCTab.Padding = new System.Windows.Forms.Padding(3);
            DDCTab.Size = new System.Drawing.Size(192, 72);
            DDCTab.TabIndex = 3;
            DDCTab.Text = "DDC";
            DDCTab.ToolTipText = "Generate dynamic difficulty for arrangements.";
            DDCTab.UseVisualStyleBackColor = true;
            // 
            // ddc1
            // 
            ddc1.Location = new System.Drawing.Point(0, 0);
            ddc1.MinimumSize = new System.Drawing.Size(530, 380);
            ddc1.Name = "ddc1";
            ddc1.Size = new System.Drawing.Size(580, 470);
            ddc1.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(592, 694);
            Controls.Add(btnDevTestMethod);
            Controls.Add(btnUpdate);
            Controls.Add(tabControl1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(600, 688);
            Name = "MainForm";
            Text = "Song Creator Toolkit for Rocksmith";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            KeyDown += MainForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            GeneralConfigTab.ResumeLayout(false);
            zigProConverterTab.ResumeLayout(false);
            cdlcConverterTab.ResumeLayout(false);
            oggConverterTab.ResumeLayout(false);
            sngConverterTab.ResumeLayout(false);
            dlcInlayCreatorTab.ResumeLayout(false);
            dlcConverterTab.ResumeLayout(false);
            dlcPackerUnpackerTab.ResumeLayout(false);
            dlcPackageCreatorTab.ResumeLayout(false);
            dlcPackageCreatorTab.PerformLayout();
            tabControl1.ResumeLayout(false);
            DDCTab.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem dLCLibraryManagerToolStripMenuItem;//bcapi
        private System.Windows.Forms.TabPage DLCManagerTab;//bcapi
        private DLCManager.DLCManager dlcm;//bcapi
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnDevTestMethod;
        private System.Windows.Forms.TabPage GeneralConfigTab;
        private Config.GeneralConfig generalConfig1;
        private System.Windows.Forms.TabPage zigProConverterTab;
        private ZpeConverter.ZpeConverter zpeConverter1;
        private System.Windows.Forms.TabPage cdlcConverterTab;
        private CDLC2Tab.CDLC2Tab cdlC2Tab1;
        private System.Windows.Forms.TabPage oggConverterTab;
        private OggConverter.OggConverter oggConverter1;
        private System.Windows.Forms.TabPage sngConverterTab;
        private SngConverter.SngConverter sngConverter1;
        private System.Windows.Forms.TabPage dlcInlayCreatorTab;
        private DLCInlayCreator.DLCInlayCreator dlcInlayCreator1;
        private System.Windows.Forms.TabPage dlcConverterTab;
        private DLCConverter.DLCConverter dlcConverter1;
        private System.Windows.Forms.TabPage dlcPackerUnpackerTab;
        private DLCPackerUnpacker.DLCPackerUnpacker dlcPackerUnpacker1;
        private System.Windows.Forms.TabPage dlcPackageCreatorTab;
        private DLCPackageCreator.DLCPackageCreator dlcPackageCreator1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DDCTab;
        private DDC.DDC ddc1;
        //bcapi
        //private System.Windows.Forms.TabPage dLCManagerTab;
        //private DLCManager.DLCManager dLCManager1;
    }
}
