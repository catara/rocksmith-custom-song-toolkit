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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.chbx_AutoPlay = new System.Windows.Forms.CheckBox();
            this.btn_FTP = new System.Windows.Forms.Button();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chbx_Autosave = new System.Windows.Forms.CheckBox();
            this.btn_ExpandSelCrossP = new System.Windows.Forms.Button();
            this.cbx_Format = new System.Windows.Forms.ComboBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.txt_Counter = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPath = new RocksmithToolkitGUI.CueTextBox();
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
            this.btn_NextItem = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridView1.RowHeadersWidth = 61;
            this.DataGridView1.Size = new System.Drawing.Size(1183, 456);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.ChangeEdit);
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(828, 465);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(125, 121);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Removed
            // 
            this.chbx_Removed.AutoSize = true;
            this.chbx_Removed.Location = new System.Drawing.Point(754, 566);
            this.chbx_Removed.Name = "chbx_Removed";
            this.chbx_Removed.Size = new System.Drawing.Size(72, 17);
            this.chbx_Removed.TabIndex = 124;
            this.chbx_Removed.Text = "Removed";
            this.chbx_Removed.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(1099, 459);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(84, 26);
            this.btn_Save.TabIndex = 123;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(71, 531);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 37);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1105, 591);
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
            this.btn_DecompressAll.Location = new System.Drawing.Point(8, 531);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(59, 38);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // rtxt_Comments
            // 
            this.rtxt_Comments.Location = new System.Drawing.Point(640, 479);
            this.rtxt_Comments.Margin = new System.Windows.Forms.Padding(2);
            this.rtxt_Comments.Name = "rtxt_Comments";
            this.rtxt_Comments.Size = new System.Drawing.Size(170, 56);
            this.rtxt_Comments.TabIndex = 279;
            this.rtxt_Comments.Text = "";
            // 
            // btn_GenerateHSAN
            // 
            this.btn_GenerateHSAN.Location = new System.Drawing.Point(962, 553);
            this.btn_GenerateHSAN.Name = "btn_GenerateHSAN";
            this.btn_GenerateHSAN.Size = new System.Drawing.Size(110, 35);
            this.btn_GenerateHSAN.TabIndex = 280;
            this.btn_GenerateHSAN.Text = "Regenerate HSAN and  Pack";
            this.btn_GenerateHSAN.UseVisualStyleBackColor = true;
            this.btn_GenerateHSAN.Click += new System.EventHandler(this.btn_GenerateHSAN_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.AutoSize = true;
            this.lbl_NoRec.Location = new System.Drawing.Point(5, 461);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(59, 13);
            this.lbl_NoRec.TabIndex = 281;
            this.lbl_NoRec.Text = " Records";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(742, 534);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(68, 26);
            this.button12.TabIndex = 283;
            this.button12.Text = "Preview";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(640, 534);
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
            this.label2.Location = new System.Drawing.Point(637, 465);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 284;
            this.label2.Text = "Comments";
            // 
            // btn_InvertAll
            // 
            this.btn_InvertAll.Location = new System.Drawing.Point(8, 502);
            this.btn_InvertAll.Name = "btn_InvertAll";
            this.btn_InvertAll.Size = new System.Drawing.Size(59, 21);
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
            this.chbx_Songs2Cache.Location = new System.Drawing.Point(1078, 553);
            this.chbx_Songs2Cache.Name = "chbx_Songs2Cache";
            this.chbx_Songs2Cache.Size = new System.Drawing.Size(109, 17);
            this.chbx_Songs2Cache.TabIndex = 289;
            this.chbx_Songs2Cache.Text = "Only cache.psarc";
            this.chbx_Songs2Cache.UseVisualStyleBackColor = true;
            // 
            // chbx_AutoPlay
            // 
            this.chbx_AutoPlay.AutoSize = true;
            this.chbx_AutoPlay.Enabled = false;
            this.chbx_AutoPlay.Location = new System.Drawing.Point(632, 566);
            this.chbx_AutoPlay.Name = "chbx_AutoPlay";
            this.chbx_AutoPlay.Size = new System.Drawing.Size(80, 17);
            this.chbx_AutoPlay.TabIndex = 290;
            this.chbx_AutoPlay.Text = "<AutoPlay>";
            this.chbx_AutoPlay.UseVisualStyleBackColor = true;
            // 
            // btn_FTP
            // 
            this.btn_FTP.Location = new System.Drawing.Point(4, 14);
            this.btn_FTP.Name = "btn_FTP";
            this.btn_FTP.Size = new System.Drawing.Size(110, 38);
            this.btn_FTP.TabIndex = 291;
            this.btn_FTP.Text = "Copy/FTP Back to Game Folder";
            this.btn_FTP.UseVisualStyleBackColor = true;
            this.btn_FTP.Click += new System.EventHandler(this.btn_FTP_Click);
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(852, 591);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(95, 20);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 293;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Info;
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(8, 573);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 38);
            this.button2.TabIndex = 302;
            this.button2.Text = "Open WEM2OGG Corespondence DB";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(123, 574);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(62, 37);
            this.button4.TabIndex = 303;
            this.button4.Text = "Open HSAN file";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chbx_Autosave
            // 
            this.chbx_Autosave.AutoSize = true;
            this.chbx_Autosave.Checked = true;
            this.chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Autosave.Enabled = false;
            this.chbx_Autosave.Location = new System.Drawing.Point(1020, 463);
            this.chbx_Autosave.Name = "chbx_Autosave";
            this.chbx_Autosave.Size = new System.Drawing.Size(73, 17);
            this.chbx_Autosave.TabIndex = 304;
            this.chbx_Autosave.Text = "AutoSave";
            this.chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // btn_ExpandSelCrossP
            // 
            this.btn_ExpandSelCrossP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExpandSelCrossP.Location = new System.Drawing.Point(86, 457);
            this.btn_ExpandSelCrossP.Name = "btn_ExpandSelCrossP";
            this.btn_ExpandSelCrossP.Size = new System.Drawing.Size(62, 72);
            this.btn_ExpandSelCrossP.TabIndex = 305;
            this.btn_ExpandSelCrossP.Text = "Extend current Selection cross Platforms";
            this.btn_ExpandSelCrossP.UseVisualStyleBackColor = true;
            this.btn_ExpandSelCrossP.Click += new System.EventHandler(this.button5_Click);
            // 
            // cbx_Format
            // 
            this.cbx_Format.FormattingEnabled = true;
            this.cbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.cbx_Format.Location = new System.Drawing.Point(170, 37);
            this.cbx_Format.Name = "cbx_Format";
            this.cbx_Format.Size = new System.Drawing.Size(46, 21);
            this.cbx_Format.TabIndex = 306;
            this.cbx_Format.Text = "PS3";
            this.cbx_Format.SelectedIndexChanged += new System.EventHandler(this.cbx_Format_SelectedIndexChanged);
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(196, 16);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_SteamDLCFolder.TabIndex = 307;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // txt_Counter
            // 
            this.txt_Counter.Cue = "Up/Down";
            this.txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Counter.ForeColor = System.Drawing.Color.Gray;
            this.txt_Counter.Location = new System.Drawing.Point(795, 590);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.Size = new System.Drawing.Size(52, 20);
            this.txt_Counter.TabIndex = 311;
            this.txt_Counter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Counter.Visible = false;
            // 
            // txt_AudioPreviewPath
            // 
            this.txt_AudioPreviewPath.Cue = "Audio Preview Path";
            this.txt_AudioPreviewPath.Enabled = false;
            this.txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPreviewPath.Location = new System.Drawing.Point(391, 587);
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
            this.txt_AudioPath.Location = new System.Drawing.Point(219, 587);
            this.txt_AudioPath.Name = "txt_AudioPath";
            this.txt_AudioPath.Size = new System.Drawing.Size(158, 20);
            this.txt_AudioPath.TabIndex = 300;
            this.txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(120, 14);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(71, 20);
            this.txt_FTPPath.TabIndex = 292;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Platform
            // 
            this.txt_Platform.Cue = "Arrangements";
            this.txt_Platform.Enabled = false;
            this.txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Platform.ForeColor = System.Drawing.Color.Black;
            this.txt_Platform.Location = new System.Drawing.Point(392, 564);
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
            this.txt_SongsHSANPath.Location = new System.Drawing.Point(392, 541);
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
            this.txt_PSARCName.Location = new System.Drawing.Point(392, 515);
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
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(619, 589);
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
            this.txt_Identifier.Location = new System.Drawing.Point(154, 463);
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
            this.txt_Arrangements.Location = new System.Drawing.Point(219, 564);
            this.txt_Arrangements.Name = "txt_Arrangements";
            this.txt_Arrangements.Size = new System.Drawing.Size(159, 20);
            this.txt_Arrangements.TabIndex = 134;
            this.txt_Arrangements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Enabled = false;
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(155, 539);
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
            this.txt_ArtistSort.Location = new System.Drawing.Point(155, 515);
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
            this.txt_Artist.Location = new System.Drawing.Point(155, 489);
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
            this.txt_AlbumYear.Location = new System.Drawing.Point(392, 491);
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
            this.txt_ID.Location = new System.Drawing.Point(8, 479);
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
            this.txt_Album.Location = new System.Drawing.Point(392, 465);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(222, 20);
            this.txt_Album.TabIndex = 126;
            // 
            // btn_NextItem
            // 
            this.btn_NextItem.Location = new System.Drawing.Point(731, 561);
            this.btn_NextItem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_NextItem.Name = "btn_NextItem";
            this.btn_NextItem.Size = new System.Drawing.Size(18, 22);
            this.btn_NextItem.TabIndex = 312;
            this.btn_NextItem.Text = ">";
            this.btn_NextItem.UseVisualStyleBackColor = true;
            this.btn_NextItem.Click += new System.EventHandler(this.btn_NextItem_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.Location = new System.Drawing.Point(709, 561);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(18, 22);
            this.btn_Prev.TabIndex = 313;
            this.btn_Prev.Text = "<";
            this.btn_Prev.UseVisualStyleBackColor = true;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // chbx_PreSavedFTP
            // 
            this.chbx_PreSavedFTP.FormattingEnabled = true;
            this.chbx_PreSavedFTP.Items.AddRange(new object[] {
            "US",
            "EU"});
            this.chbx_PreSavedFTP.Location = new System.Drawing.Point(120, 37);
            this.chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            this.chbx_PreSavedFTP.Size = new System.Drawing.Size(46, 21);
            this.chbx_PreSavedFTP.TabIndex = 329;
            this.chbx_PreSavedFTP.Text = "US";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_FTPPath);
            this.groupBox1.Controls.Add(this.chbx_PreSavedFTP);
            this.groupBox1.Controls.Add(this.btn_FTP);
            this.groupBox1.Controls.Add(this.cbx_Format);
            this.groupBox1.Controls.Add(this.btn_SteamDLCFolder);
            this.groupBox1.Location = new System.Drawing.Point(959, 486);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 66);
            this.groupBox1.TabIndex = 330;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package";
            // 
            // chbx_RemoveBassDD
            // 
            this.chbx_RemoveBassDD.AutoSize = true;
            this.chbx_RemoveBassDD.Checked = true;
            this.chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_RemoveBassDD.Enabled = false;
            this.chbx_RemoveBassDD.Location = new System.Drawing.Point(1078, 572);
            this.chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            this.chbx_RemoveBassDD.Size = new System.Drawing.Size(111, 17);
            this.chbx_RemoveBassDD.TabIndex = 331;
            this.chbx_RemoveBassDD.Text = "Remove Bass DD";
            this.chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // Cache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 614);
            this.Controls.Add(this.chbx_RemoveBassDD);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Prev);
            this.Controls.Add(this.btn_NextItem);
            this.Controls.Add(this.txt_Counter);
            this.Controls.Add(this.btn_ExpandSelCrossP);
            this.Controls.Add(this.chbx_Autosave);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_AudioPreviewPath);
            this.Controls.Add(this.txt_AudioPath);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.chbx_AutoPlay);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        //private DLCManager.Files filed;
        //private DLCPackageData datas;

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
        private System.Windows.Forms.CheckBox chbx_AutoPlay;
        private System.Windows.Forms.Button btn_FTP;
        private CueTextBox txt_FTPPath;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private CueTextBox txt_AudioPreviewPath;
        private CueTextBox txt_AudioPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chbx_Autosave;
        private System.Windows.Forms.Button btn_ExpandSelCrossP;
        private System.Windows.Forms.ComboBox cbx_Format;
        private System.Windows.Forms.Button btn_SteamDLCFolder;
        private CueTextBox txt_Counter;
        private System.Windows.Forms.Button btn_NextItem;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.ComboBox chbx_PreSavedFTP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbx_RemoveBassDD;
    }
}