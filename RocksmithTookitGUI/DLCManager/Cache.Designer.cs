using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class Cache
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.chbx_Removed = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_DecompressAll = new System.Windows.Forms.Button();
            this.rtxt_Comments = new System.Windows.Forms.RichTextBox();
            this.btn_GenerateHSAN = new System.Windows.Forms.Button();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_InvertAll = new System.Windows.Forms.Button();
            this.chbx_Songs2Cache = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_FTP = new System.Windows.Forms.Button();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.chbx_FTP2 = new System.Windows.Forms.CheckBox();
            this.chbx_FTP1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chbx_VLCHome = new System.Windows.Forms.CheckBox();
            this.chbx_VLCWork = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_AudioPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VLCPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FTPPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Platform = new RocksmithToolkitGUI.CueTextBox();
            this.txt_SongsHSANPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PSARCName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AlbumArtPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Identifier = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Arrangements = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Title = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArtistSort = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AlbumYear = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Autosave = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersWidth = 61;
            this.DataGridView1.Size = new System.Drawing.Size(1175, 544);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(832, 551);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(125, 125);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Removed
            // 
            this.chbx_Removed.AutoSize = true;
            this.chbx_Removed.Location = new System.Drawing.Point(16, 567);
            this.chbx_Removed.Name = "chbx_Removed";
            this.chbx_Removed.Size = new System.Drawing.Size(72, 17);
            this.chbx_Removed.TabIndex = 124;
            this.chbx_Removed.Text = "Removed";
            this.chbx_Removed.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(1082, 551);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(84, 26);
            this.btn_Save.TabIndex = 123;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 584);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 35);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1094, 672);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(12, 619);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(73, 35);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // rtxt_Comments
            // 
            this.rtxt_Comments.Location = new System.Drawing.Point(644, 565);
            this.rtxt_Comments.Margin = new System.Windows.Forms.Padding(2);
            this.rtxt_Comments.Name = "rtxt_Comments";
            this.rtxt_Comments.Size = new System.Drawing.Size(170, 56);
            this.rtxt_Comments.TabIndex = 279;
            this.rtxt_Comments.Text = "";
            // 
            // btn_GenerateHSAN
            // 
            this.btn_GenerateHSAN.Location = new System.Drawing.Point(1056, 577);
            this.btn_GenerateHSAN.Name = "btn_GenerateHSAN";
            this.btn_GenerateHSAN.Size = new System.Drawing.Size(110, 35);
            this.btn_GenerateHSAN.TabIndex = 280;
            this.btn_GenerateHSAN.Text = "Regenerate HSAN";
            this.btn_GenerateHSAN.UseVisualStyleBackColor = true;
            this.btn_GenerateHSAN.Click += new System.EventHandler(this.btn_GenerateHSAN_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.AutoSize = true;
            this.lbl_NoRec.Location = new System.Drawing.Point(12, 551);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(59, 13);
            this.lbl_NoRec.TabIndex = 281;
            this.lbl_NoRec.Text = "of Records";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(746, 620);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(68, 26);
            this.button12.TabIndex = 283;
            this.button12.Text = "Preview";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(644, 620);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(68, 26);
            this.button11.TabIndex = 282;
            this.button11.Text = "Audio";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(641, 551);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 284;
            this.label2.Text = "Comments";
            // 
            // btn_InvertAll
            // 
            this.btn_InvertAll.Location = new System.Drawing.Point(1094, 650);
            this.btn_InvertAll.Name = "btn_InvertAll";
            this.btn_InvertAll.Size = new System.Drawing.Size(72, 21);
            this.btn_InvertAll.TabIndex = 285;
            this.btn_InvertAll.Text = "Invert All";
            this.btn_InvertAll.UseVisualStyleBackColor = true;
            this.btn_InvertAll.Click += new System.EventHandler(this.btn_InvertAll_Click);
            // 
            // chbx_Songs2Cache
            // 
            this.chbx_Songs2Cache.AutoSize = true;
            this.chbx_Songs2Cache.Checked = true;
            this.chbx_Songs2Cache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Songs2Cache.Location = new System.Drawing.Point(999, 613);
            this.chbx_Songs2Cache.Name = "chbx_Songs2Cache";
            this.chbx_Songs2Cache.Size = new System.Drawing.Size(167, 17);
            this.chbx_Songs2Cache.TabIndex = 289;
            this.chbx_Songs2Cache.Text = "Pack Songs into Cache.psarc";
            this.chbx_Songs2Cache.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(746, 650);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 290;
            this.checkBox1.Text = "AutoPlay";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btn_FTP
            // 
            this.btn_FTP.Location = new System.Drawing.Point(994, 629);
            this.btn_FTP.Name = "btn_FTP";
            this.btn_FTP.Size = new System.Drawing.Size(79, 47);
            this.btn_FTP.TabIndex = 291;
            this.btn_FTP.Text = "<Copy/>FTP Back to Game Folder";
            this.btn_FTP.UseVisualStyleBackColor = true;
            this.btn_FTP.Click += new System.EventHandler(this.btn_FTP_Click);
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(961, 584);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(95, 20);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 293;
            // 
            // chbx_FTP2
            // 
            this.chbx_FTP2.AutoSize = true;
            this.chbx_FTP2.Location = new System.Drawing.Point(963, 645);
            this.chbx_FTP2.Name = "chbx_FTP2";
            this.chbx_FTP2.Size = new System.Drawing.Size(32, 17);
            this.chbx_FTP2.TabIndex = 294;
            this.chbx_FTP2.Text = "2";
            this.chbx_FTP2.UseVisualStyleBackColor = true;
            this.chbx_FTP2.CheckedChanged += new System.EventHandler(this.chbx_FTP2_CheckedChanged);
            // 
            // chbx_FTP1
            // 
            this.chbx_FTP1.AutoSize = true;
            this.chbx_FTP1.Location = new System.Drawing.Point(963, 629);
            this.chbx_FTP1.Name = "chbx_FTP1";
            this.chbx_FTP1.Size = new System.Drawing.Size(32, 17);
            this.chbx_FTP1.TabIndex = 295;
            this.chbx_FTP1.Text = "1";
            this.chbx_FTP1.UseVisualStyleBackColor = true;
            this.chbx_FTP1.CheckedChanged += new System.EventHandler(this.chbx_FTP1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(955, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 22);
            this.button1.TabIndex = 296;
            this.button1.Text = "Path 2 VLC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chbx_VLCHome
            // 
            this.chbx_VLCHome.AutoSize = true;
            this.chbx_VLCHome.Location = new System.Drawing.Point(1063, 406);
            this.chbx_VLCHome.Name = "chbx_VLCHome";
            this.chbx_VLCHome.Size = new System.Drawing.Size(54, 17);
            this.chbx_VLCHome.TabIndex = 298;
            this.chbx_VLCHome.Text = "Home";
            this.chbx_VLCHome.UseVisualStyleBackColor = true;
            this.chbx_VLCHome.Visible = false;
            this.chbx_VLCHome.CheckedChanged += new System.EventHandler(this.chbx_VLCHome_CheckedChanged);
            // 
            // chbx_VLCWork
            // 
            this.chbx_VLCWork.AutoSize = true;
            this.chbx_VLCWork.Location = new System.Drawing.Point(1123, 406);
            this.chbx_VLCWork.Name = "chbx_VLCWork";
            this.chbx_VLCWork.Size = new System.Drawing.Size(52, 17);
            this.chbx_VLCWork.TabIndex = 299;
            this.chbx_VLCWork.Text = "Work";
            this.chbx_VLCWork.UseVisualStyleBackColor = true;
            this.chbx_VLCWork.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Info;
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(12, 656);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 41);
            this.button2.TabIndex = 302;
            this.button2.Text = "Open WEM2OGG Corespondence DB";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(89, 620);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 34);
            this.button4.TabIndex = 303;
            this.button4.Text = "Open Psarc";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt_AudioPreviewPath
            // 
            this.txt_AudioPreviewPath.Cue = "Audio Preview Path";
            this.txt_AudioPreviewPath.Enabled = false;
            this.txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPreviewPath.Location = new System.Drawing.Point(395, 673);
            this.txt_AudioPreviewPath.Name = "txt_AudioPreviewPath";
            this.txt_AudioPreviewPath.Size = new System.Drawing.Size(222, 20);
            this.txt_AudioPreviewPath.TabIndex = 301;
            this.txt_AudioPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AudioPath
            // 
            this.txt_AudioPath.Cue = "Audio Path";
            this.txt_AudioPath.Enabled = false;
            this.txt_AudioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPath.Location = new System.Drawing.Point(145, 673);
            this.txt_AudioPath.Name = "txt_AudioPath";
            this.txt_AudioPath.Size = new System.Drawing.Size(222, 20);
            this.txt_AudioPath.TabIndex = 300;
            this.txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_VLCPath
            // 
            this.txt_VLCPath.Cue = "VLC Path";
            this.txt_VLCPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VLCPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_VLCPath.Location = new System.Drawing.Point(996, 380);
            this.txt_VLCPath.Name = "txt_VLCPath";
            this.txt_VLCPath.Size = new System.Drawing.Size(170, 20);
            this.txt_VLCPath.TabIndex = 297;
            this.txt_VLCPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_VLCPath.Visible = false;
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(1079, 629);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(87, 20);
            this.txt_FTPPath.TabIndex = 292;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Platform
            // 
            this.txt_Platform.Cue = "Arrangements";
            this.txt_Platform.Enabled = false;
            this.txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Platform.ForeColor = System.Drawing.Color.Black;
            this.txt_Platform.Location = new System.Drawing.Point(396, 650);
            this.txt_Platform.Name = "txt_Platform";
            this.txt_Platform.Size = new System.Drawing.Size(222, 20);
            this.txt_Platform.TabIndex = 288;
            this.txt_Platform.Text = "Platform";
            this.txt_Platform.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SongsHSANPath
            // 
            this.txt_SongsHSANPath.Cue = "Arrangements";
            this.txt_SongsHSANPath.Enabled = false;
            this.txt_SongsHSANPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SongsHSANPath.ForeColor = System.Drawing.Color.Black;
            this.txt_SongsHSANPath.Location = new System.Drawing.Point(396, 627);
            this.txt_SongsHSANPath.Name = "txt_SongsHSANPath";
            this.txt_SongsHSANPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_SongsHSANPath.Size = new System.Drawing.Size(222, 20);
            this.txt_SongsHSANPath.TabIndex = 287;
            this.txt_SongsHSANPath.Text = "Songs.HSAN Path";
            // 
            // txt_PSARCName
            // 
            this.txt_PSARCName.Cue = "Arrangements";
            this.txt_PSARCName.Enabled = false;
            this.txt_PSARCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PSARCName.ForeColor = System.Drawing.Color.Black;
            this.txt_PSARCName.Location = new System.Drawing.Point(396, 601);
            this.txt_PSARCName.Name = "txt_PSARCName";
            this.txt_PSARCName.Size = new System.Drawing.Size(222, 20);
            this.txt_PSARCName.TabIndex = 286;
            this.txt_PSARCName.Text = "PSARC Name";
            this.txt_PSARCName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album Art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(623, 675);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(170, 20);
            this.txt_AlbumArtPath.TabIndex = 278;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_AlbumArtPath.Visible = false;
            // 
            // txt_Identifier
            // 
            this.txt_Identifier.Cue = "IDentifier";
            this.txt_Identifier.Enabled = false;
            this.txt_Identifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Identifier.ForeColor = System.Drawing.Color.Gray;
            this.txt_Identifier.Location = new System.Drawing.Point(145, 549);
            this.txt_Identifier.Name = "txt_Identifier";
            this.txt_Identifier.Size = new System.Drawing.Size(223, 20);
            this.txt_Identifier.TabIndex = 277;
            // 
            // txt_Arrangements
            // 
            this.txt_Arrangements.Cue = "Arrangements";
            this.txt_Arrangements.Enabled = false;
            this.txt_Arrangements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Arrangements.ForeColor = System.Drawing.Color.Gray;
            this.txt_Arrangements.Location = new System.Drawing.Point(146, 650);
            this.txt_Arrangements.Name = "txt_Arrangements";
            this.txt_Arrangements.Size = new System.Drawing.Size(222, 20);
            this.txt_Arrangements.TabIndex = 134;
            this.txt_Arrangements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Enabled = false;
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(145, 625);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(222, 20);
            this.txt_Title.TabIndex = 133;
            // 
            // txt_ArtistSort
            // 
            this.txt_ArtistSort.Cue = "ArtistSort";
            this.txt_ArtistSort.Enabled = false;
            this.txt_ArtistSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSort.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSort.Location = new System.Drawing.Point(145, 601);
            this.txt_ArtistSort.Name = "txt_ArtistSort";
            this.txt_ArtistSort.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistSort.TabIndex = 132;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Enabled = false;
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(145, 575);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumYear
            // 
            this.txt_AlbumYear.Cue = "AlbumYear";
            this.txt_AlbumYear.Enabled = false;
            this.txt_AlbumYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumYear.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumYear.Location = new System.Drawing.Point(396, 577);
            this.txt_AlbumYear.Name = "txt_AlbumYear";
            this.txt_AlbumYear.Size = new System.Drawing.Size(222, 20);
            this.txt_AlbumYear.TabIndex = 130;
            this.txt_AlbumYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(94, 564);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Enabled = false;
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(396, 551);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(222, 20);
            this.txt_Album.TabIndex = 126;
            // 
            // chbx_Autosave
            // 
            this.chbx_Autosave.AutoSize = true;
            this.chbx_Autosave.Checked = true;
            this.chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Autosave.Enabled = false;
            this.chbx_Autosave.Location = new System.Drawing.Point(1003, 554);
            this.chbx_Autosave.Name = "chbx_Autosave";
            this.chbx_Autosave.Size = new System.Drawing.Size(73, 17);
            this.chbx_Autosave.TabIndex = 304;
            this.chbx_Autosave.Text = "AutoSave";
            this.chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(955, 676);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 21);
            this.button5.TabIndex = 305;
            this.button5.Text = "Apply cross Platforms";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Cache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 694);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chbx_Autosave);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_AudioPreviewPath);
            this.Controls.Add(this.txt_AudioPath);
            this.Controls.Add(this.chbx_VLCWork);
            this.Controls.Add(this.chbx_VLCHome);
            this.Controls.Add(this.txt_VLCPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chbx_FTP1);
            this.Controls.Add(this.chbx_FTP2);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.txt_FTPPath);
            this.Controls.Add(this.btn_FTP);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chbx_Songs2Cache);
            this.Controls.Add(this.txt_Platform);
            this.Controls.Add(this.txt_SongsHSANPath);
            this.Controls.Add(this.txt_PSARCName);
            this.Controls.Add(this.btn_InvertAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.lbl_NoRec);
            this.Controls.Add(this.btn_GenerateHSAN);
            this.Controls.Add(this.rtxt_Comments);
            this.Controls.Add(this.txt_AlbumArtPath);
            this.Controls.Add(this.txt_Identifier);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_Arrangements);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.txt_ArtistSort);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.txt_AlbumYear);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.chbx_Removed);
            this.Controls.Add(this.picbx_AlbumArtPath);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_Album);
            this.Name = "Cache";
            this.Text = "List of songs delivered with the Retail version of Rocksmith";
            this.Load += new System.EventHandler(this.Standardization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.CheckBox chbx_Removed;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button button3;
        private CueTextBox txt_AlbumYear;
        private CueTextBox txt_ID;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_Album;
        private CueTextBox txt_ArtistSort;
        private CueTextBox txt_Artist;
        private CueTextBox txt_Title;
        private CueTextBox txt_Arrangements;
        private DLCManager.Files filed;
        private DLCPackageData datas;

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_DecompressAll;
        private CueTextBox txt_Identifier;
        private CueTextBox txt_AlbumArtPath;
        private System.Windows.Forms.RichTextBox rtxt_Comments;
        private System.Windows.Forms.Button btn_GenerateHSAN;
        private System.Windows.Forms.Label lbl_NoRec;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_InvertAll;
        private CueTextBox txt_PSARCName;
        private CueTextBox txt_SongsHSANPath;
        private CueTextBox txt_Platform;
        private System.Windows.Forms.CheckBox chbx_Songs2Cache;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btn_FTP;
        private CueTextBox txt_FTPPath;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private System.Windows.Forms.CheckBox chbx_FTP2;
        private System.Windows.Forms.CheckBox chbx_FTP1;
        private System.Windows.Forms.Button button1;
        private CueTextBox txt_VLCPath;
        private System.Windows.Forms.CheckBox chbx_VLCHome;
        private System.Windows.Forms.CheckBox chbx_VLCWork;
        private CueTextBox txt_AudioPreviewPath;
        private CueTextBox txt_AudioPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chbx_Autosave;
        private System.Windows.Forms.Button button5;
    }
}