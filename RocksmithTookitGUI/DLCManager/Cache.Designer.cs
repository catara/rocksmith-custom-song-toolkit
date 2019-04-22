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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btn_PlayPreview = new System.Windows.Forms.Button();
            this.btn_PlayAudio = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_InvertAll = new System.Windows.Forms.Button();
            this.chbx_Songs2Cache = new System.Windows.Forms.CheckBox();
            this.chbx_AutoPlay = new System.Windows.Forms.CheckBox();
            this.btn_FTP = new System.Windows.Forms.Button();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.btn_OpenCorrespondence = new System.Windows.Forms.Button();
            this.btn_OpeHSAN = new System.Windows.Forms.Button();
            this.chbx_Autosave = new System.Windows.Forms.CheckBox();
            this.btn_ExpandSelCrossP = new System.Windows.Forms.Button();
            this.cbx_Format = new System.Windows.Forms.ComboBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.btn_NextItem = new System.Windows.Forms.Button();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_FTPPath = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            this.btn_GroupsAdd = new System.Windows.Forms.Button();
            this.btn_GroupsRemove = new System.Windows.Forms.Button();
            this.chbx_AllGroups = new System.Windows.Forms.CheckedListBox();
            this.chbx_Group = new System.Windows.Forms.ComboBox();
            this.btn_GroupLoad = new System.Windows.Forms.Button();
            this.btn_GroupSave = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.chbx_Selected = new System.Windows.Forms.CheckBox();
            this.btn_SelectNone = new System.Windows.Forms.Button();
            this.cmb_Filter = new System.Windows.Forms.ComboBox();
            this.chbx_RemoveDD = new System.Windows.Forms.CheckBox();
            this.txt_Counter = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPath = new RocksmithToolkitGUI.CueTextBox();
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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.Location = new System.Drawing.Point(16, 4);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.RowHeadersWidth = 61;
            this.DataGridView1.Size = new System.Drawing.Size(2328, 878);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.ChangeEdit);
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(1620, 894);
            this.picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(251, 232);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Removed
            // 
            this.chbx_Removed.AutoSize = true;
            this.chbx_Removed.Location = new System.Drawing.Point(201, 980);
            this.chbx_Removed.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_Removed.Name = "chbx_Removed";
            this.chbx_Removed.Size = new System.Drawing.Size(135, 29);
            this.chbx_Removed.TabIndex = 124;
            this.chbx_Removed.Text = "Removed";
            this.chbx_Removed.UseVisualStyleBackColor = true;
            this.chbx_Removed.CheckStateChanged += new System.EventHandler(this.chbx_Removed_CheckStateChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(2160, 885);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(168, 50);
            this.btn_Save.TabIndex = 123;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(155, 1061);
            this.button3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 86);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(2196, 1189);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(144, 50);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(3, 1061);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(141, 86);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // rtxt_Comments
            // 
            this.rtxt_Comments.Location = new System.Drawing.Point(1244, 911);
            this.rtxt_Comments.Margin = new System.Windows.Forms.Padding(4);
            this.rtxt_Comments.Name = "rtxt_Comments";
            this.rtxt_Comments.Size = new System.Drawing.Size(336, 104);
            this.rtxt_Comments.TabIndex = 279;
            this.rtxt_Comments.Text = "";
            // 
            // btn_GenerateHSAN
            // 
            this.btn_GenerateHSAN.Location = new System.Drawing.Point(1876, 1060);
            this.btn_GenerateHSAN.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_GenerateHSAN.Name = "btn_GenerateHSAN";
            this.btn_GenerateHSAN.Size = new System.Drawing.Size(220, 102);
            this.btn_GenerateHSAN.TabIndex = 280;
            this.btn_GenerateHSAN.Text = "Regenerate HSAN and  Pack";
            this.btn_GenerateHSAN.UseVisualStyleBackColor = true;
            this.btn_GenerateHSAN.Click += new System.EventHandler(this.btn_GenerateHSAN_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.AutoSize = true;
            this.lbl_NoRec.Location = new System.Drawing.Point(191, 894);
            this.lbl_NoRec.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(98, 25);
            this.lbl_NoRec.TabIndex = 281;
            this.lbl_NoRec.Text = " Records";
            // 
            // btn_PlayPreview
            // 
            this.btn_PlayPreview.Location = new System.Drawing.Point(1448, 1019);
            this.btn_PlayPreview.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_PlayPreview.Name = "btn_PlayPreview";
            this.btn_PlayPreview.Size = new System.Drawing.Size(136, 50);
            this.btn_PlayPreview.TabIndex = 283;
            this.btn_PlayPreview.Text = "Preview";
            this.btn_PlayPreview.UseVisualStyleBackColor = true;
            this.btn_PlayPreview.Click += new System.EventHandler(this.btn_PlayPreview_Click);
            // 
            // btn_PlayAudio
            // 
            this.btn_PlayAudio.Location = new System.Drawing.Point(1244, 1019);
            this.btn_PlayAudio.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_PlayAudio.Name = "btn_PlayAudio";
            this.btn_PlayAudio.Size = new System.Drawing.Size(136, 50);
            this.btn_PlayAudio.TabIndex = 282;
            this.btn_PlayAudio.Text = "Audio";
            this.btn_PlayAudio.UseVisualStyleBackColor = true;
            this.btn_PlayAudio.Click += new System.EventHandler(this.btn_PlayAudio_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1236, 881);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 284;
            this.label2.Text = "Comments";
            // 
            // btn_InvertAll
            // 
            this.btn_InvertAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_InvertAll.Location = new System.Drawing.Point(11, 996);
            this.btn_InvertAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_InvertAll.Name = "btn_InvertAll";
            this.btn_InvertAll.Size = new System.Drawing.Size(149, 50);
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
            this.chbx_Songs2Cache.Location = new System.Drawing.Point(2100, 1064);
            this.chbx_Songs2Cache.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_Songs2Cache.Name = "chbx_Songs2Cache";
            this.chbx_Songs2Cache.Size = new System.Drawing.Size(211, 29);
            this.chbx_Songs2Cache.TabIndex = 289;
            this.chbx_Songs2Cache.Text = "Only cache.psarc";
            this.chbx_Songs2Cache.UseVisualStyleBackColor = true;
            // 
            // chbx_AutoPlay
            // 
            this.chbx_AutoPlay.AutoSize = true;
            this.chbx_AutoPlay.Enabled = false;
            this.chbx_AutoPlay.Location = new System.Drawing.Point(1336, 1071);
            this.chbx_AutoPlay.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_AutoPlay.Name = "chbx_AutoPlay";
            this.chbx_AutoPlay.Size = new System.Drawing.Size(154, 29);
            this.chbx_AutoPlay.TabIndex = 290;
            this.chbx_AutoPlay.Text = "<AutoPlay>";
            this.chbx_AutoPlay.UseVisualStyleBackColor = true;
            // 
            // btn_FTP
            // 
            this.btn_FTP.Location = new System.Drawing.Point(8, 28);
            this.btn_FTP.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_FTP.Name = "btn_FTP";
            this.btn_FTP.Size = new System.Drawing.Size(220, 89);
            this.btn_FTP.TabIndex = 291;
            this.btn_FTP.Text = "Copy/FTP Back to Game Folder";
            this.btn_FTP.UseVisualStyleBackColor = true;
            this.btn_FTP.Click += new System.EventHandler(this.btn_FTP_Click);
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(1643, 1190);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(4);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(547, 48);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 293;
            // 
            // btn_OpenCorrespondence
            // 
            this.btn_OpenCorrespondence.BackColor = System.Drawing.SystemColors.Info;
            this.btn_OpenCorrespondence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenCorrespondence.Location = new System.Drawing.Point(3, 1152);
            this.btn_OpenCorrespondence.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OpenCorrespondence.Name = "btn_OpenCorrespondence";
            this.btn_OpenCorrespondence.Size = new System.Drawing.Size(220, 92);
            this.btn_OpenCorrespondence.TabIndex = 302;
            this.btn_OpenCorrespondence.Text = "Open WEM2OGG Corespondence DB";
            this.btn_OpenCorrespondence.UseVisualStyleBackColor = false;
            this.btn_OpenCorrespondence.Click += new System.EventHandler(this.btn_OpenCorrespondence_Click);
            // 
            // btn_OpeHSAN
            // 
            this.btn_OpeHSAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpeHSAN.Location = new System.Drawing.Point(245, 1152);
            this.btn_OpeHSAN.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_OpeHSAN.Name = "btn_OpeHSAN";
            this.btn_OpeHSAN.Size = new System.Drawing.Size(152, 92);
            this.btn_OpeHSAN.TabIndex = 303;
            this.btn_OpeHSAN.Text = "Open HSAN file";
            this.btn_OpeHSAN.UseVisualStyleBackColor = true;
            this.btn_OpeHSAN.Click += new System.EventHandler(this.btn_OpeHSAN_Click);
            // 
            // chbx_Autosave
            // 
            this.chbx_Autosave.AutoSize = true;
            this.chbx_Autosave.Checked = true;
            this.chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Autosave.Location = new System.Drawing.Point(1997, 890);
            this.chbx_Autosave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_Autosave.Name = "chbx_Autosave";
            this.chbx_Autosave.Size = new System.Drawing.Size(137, 29);
            this.chbx_Autosave.TabIndex = 304;
            this.chbx_Autosave.Text = "AutoSave";
            this.chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // btn_ExpandSelCrossP
            // 
            this.btn_ExpandSelCrossP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExpandSelCrossP.Location = new System.Drawing.Point(411, 1152);
            this.btn_ExpandSelCrossP.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_ExpandSelCrossP.Name = "btn_ExpandSelCrossP";
            this.btn_ExpandSelCrossP.Size = new System.Drawing.Size(272, 92);
            this.btn_ExpandSelCrossP.TabIndex = 305;
            this.btn_ExpandSelCrossP.Text = "Extend current Selection cross Platforms";
            this.btn_ExpandSelCrossP.UseVisualStyleBackColor = true;
            this.btn_ExpandSelCrossP.Click += new System.EventHandler(this.btn_ExpandSelCrossP_Click);
            // 
            // cbx_Format
            // 
            this.cbx_Format.FormattingEnabled = true;
            this.cbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.cbx_Format.Location = new System.Drawing.Point(340, 71);
            this.cbx_Format.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cbx_Format.Name = "cbx_Format";
            this.cbx_Format.Size = new System.Drawing.Size(88, 33);
            this.cbx_Format.TabIndex = 306;
            this.cbx_Format.Text = "PS3";
            this.cbx_Format.SelectedIndexChanged += new System.EventHandler(this.cbx_Format_SelectedIndexChanged);
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(392, 31);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(44, 29);
            this.btn_SteamDLCFolder.TabIndex = 307;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // btn_NextItem
            // 
            this.btn_NextItem.Location = new System.Drawing.Point(295, 931);
            this.btn_NextItem.Margin = new System.Windows.Forms.Padding(4);
            this.btn_NextItem.Name = "btn_NextItem";
            this.btn_NextItem.Size = new System.Drawing.Size(36, 42);
            this.btn_NextItem.TabIndex = 312;
            this.btn_NextItem.Text = ">";
            this.btn_NextItem.UseVisualStyleBackColor = true;
            this.btn_NextItem.Click += new System.EventHandler(this.btn_NextItem_Click);
            // 
            // btn_Prev
            // 
            this.btn_Prev.Location = new System.Drawing.Point(261, 931);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(36, 42);
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
            this.chbx_PreSavedFTP.Location = new System.Drawing.Point(240, 71);
            this.chbx_PreSavedFTP.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            this.chbx_PreSavedFTP.Size = new System.Drawing.Size(88, 33);
            this.chbx_PreSavedFTP.TabIndex = 329;
            this.chbx_PreSavedFTP.Text = "US";
            this.chbx_PreSavedFTP.SelectedIndexChanged += new System.EventHandler(this.chbx_PreSavedFTP_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_FTPPath);
            this.groupBox1.Controls.Add(this.chbx_PreSavedFTP);
            this.groupBox1.Controls.Add(this.btn_FTP);
            this.groupBox1.Controls.Add(this.cbx_Format);
            this.groupBox1.Controls.Add(this.btn_SteamDLCFolder);
            this.groupBox1.Location = new System.Drawing.Point(1872, 935);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(456, 128);
            this.groupBox1.TabIndex = 330;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package";
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(240, 28);
            this.txt_FTPPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(137, 32);
            this.txt_FTPPath.TabIndex = 292;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chbx_RemoveBassDD
            // 
            this.chbx_RemoveBassDD.AutoSize = true;
            this.chbx_RemoveBassDD.Checked = true;
            this.chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_RemoveBassDD.Location = new System.Drawing.Point(2100, 1100);
            this.chbx_RemoveBassDD.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            this.chbx_RemoveBassDD.Size = new System.Drawing.Size(213, 29);
            this.chbx_RemoveBassDD.TabIndex = 331;
            this.chbx_RemoveBassDD.Text = "Remove Bass DD";
            this.chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // btn_GroupsAdd
            // 
            this.btn_GroupsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupsAdd.Location = new System.Drawing.Point(1432, 1195);
            this.btn_GroupsAdd.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_GroupsAdd.Name = "btn_GroupsAdd";
            this.btn_GroupsAdd.Size = new System.Drawing.Size(43, 39);
            this.btn_GroupsAdd.TabIndex = 385;
            this.btn_GroupsAdd.Text = "+";
            this.btn_GroupsAdd.UseVisualStyleBackColor = true;
            this.btn_GroupsAdd.Click += new System.EventHandler(this.btn_GroupsAdd_Click);
            // 
            // btn_GroupsRemove
            // 
            this.btn_GroupsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupsRemove.Location = new System.Drawing.Point(1475, 1195);
            this.btn_GroupsRemove.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_GroupsRemove.Name = "btn_GroupsRemove";
            this.btn_GroupsRemove.Size = new System.Drawing.Size(43, 39);
            this.btn_GroupsRemove.TabIndex = 384;
            this.btn_GroupsRemove.Text = "-";
            this.btn_GroupsRemove.UseVisualStyleBackColor = true;
            this.btn_GroupsRemove.Click += new System.EventHandler(this.btn_GroupsRemove_Click);
            // 
            // chbx_AllGroups
            // 
            this.chbx_AllGroups.CheckOnClick = true;
            this.chbx_AllGroups.FormattingEnabled = true;
            this.chbx_AllGroups.Location = new System.Drawing.Point(693, 1055);
            this.chbx_AllGroups.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_AllGroups.Name = "chbx_AllGroups";
            this.chbx_AllGroups.Size = new System.Drawing.Size(368, 186);
            this.chbx_AllGroups.Sorted = true;
            this.chbx_AllGroups.TabIndex = 386;
            this.chbx_AllGroups.SelectedValueChanged += new System.EventHandler(this.chbx_AllGroups_SelectedValueChanged);
            // 
            // chbx_Group
            // 
            this.chbx_Group.FormattingEnabled = true;
            this.chbx_Group.Location = new System.Drawing.Point(1075, 1198);
            this.chbx_Group.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_Group.Name = "chbx_Group";
            this.chbx_Group.Size = new System.Drawing.Size(351, 33);
            this.chbx_Group.TabIndex = 383;
            // 
            // btn_GroupLoad
            // 
            this.btn_GroupLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupLoad.Location = new System.Drawing.Point(1173, 1111);
            this.btn_GroupLoad.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_GroupLoad.Name = "btn_GroupLoad";
            this.btn_GroupLoad.Size = new System.Drawing.Size(115, 86);
            this.btn_GroupLoad.TabIndex = 388;
            this.btn_GroupLoad.Text = "Load List";
            this.btn_GroupLoad.UseVisualStyleBackColor = true;
            this.btn_GroupLoad.Click += new System.EventHandler(this.btn_GroupLoad_Click);
            // 
            // btn_GroupSave
            // 
            this.btn_GroupSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupSave.Location = new System.Drawing.Point(1075, 1111);
            this.btn_GroupSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_GroupSave.Name = "btn_GroupSave";
            this.btn_GroupSave.Size = new System.Drawing.Size(100, 86);
            this.btn_GroupSave.TabIndex = 387;
            this.btn_GroupSave.Text = "Save List";
            this.btn_GroupSave.UseVisualStyleBackColor = true;
            this.btn_GroupSave.Click += new System.EventHandler(this.btn_GroupSave_Click);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectAll.Location = new System.Drawing.Point(11, 956);
            this.btn_SelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(149, 46);
            this.btn_SelectAll.TabIndex = 390;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.btn_SelectAll_Click);
            // 
            // chbx_Selected
            // 
            this.chbx_Selected.AutoSize = true;
            this.chbx_Selected.Location = new System.Drawing.Point(201, 1018);
            this.chbx_Selected.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_Selected.Name = "chbx_Selected";
            this.chbx_Selected.Size = new System.Drawing.Size(128, 29);
            this.chbx_Selected.TabIndex = 389;
            this.chbx_Selected.Text = "Selected";
            this.chbx_Selected.UseVisualStyleBackColor = true;
            this.chbx_Selected.CheckStateChanged += new System.EventHandler(this.chbx_Selected_CheckStateChanged);
            this.chbx_Selected.Click += new System.EventHandler(this.chbx_Selected_Click);
            // 
            // btn_SelectNone
            // 
            this.btn_SelectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectNone.Location = new System.Drawing.Point(11, 911);
            this.btn_SelectNone.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_SelectNone.Name = "btn_SelectNone";
            this.btn_SelectNone.Size = new System.Drawing.Size(149, 46);
            this.btn_SelectNone.TabIndex = 391;
            this.btn_SelectNone.Text = "Select None";
            this.btn_SelectNone.UseVisualStyleBackColor = true;
            this.btn_SelectNone.Click += new System.EventHandler(this.btn_SelectNone_Click);
            // 
            // cmb_Filter
            // 
            this.cmb_Filter.FormattingEnabled = true;
            this.cmb_Filter.Items.AddRange(new object[] {
            "0ALL",
            "PC",
            "Mac",
            "PS3",
            "XBOX",
            "Removed",
            "Selected"});
            this.cmb_Filter.Location = new System.Drawing.Point(339, 1100);
            this.cmb_Filter.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cmb_Filter.Name = "cmb_Filter";
            this.cmb_Filter.Size = new System.Drawing.Size(340, 33);
            this.cmb_Filter.TabIndex = 393;
            this.cmb_Filter.SelectedValueChanged += new System.EventHandler(this.cmb_Filter_SelectedValueChanged);
            // 
            // chbx_RemoveDD
            // 
            this.chbx_RemoveDD.AutoSize = true;
            this.chbx_RemoveDD.Checked = true;
            this.chbx_RemoveDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_RemoveDD.Enabled = false;
            this.chbx_RemoveDD.Location = new System.Drawing.Point(2100, 1136);
            this.chbx_RemoveDD.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.chbx_RemoveDD.Name = "chbx_RemoveDD";
            this.chbx_RemoveDD.Size = new System.Drawing.Size(159, 29);
            this.chbx_RemoveDD.TabIndex = 394;
            this.chbx_RemoveDD.Text = "Remove DD";
            this.chbx_RemoveDD.UseVisualStyleBackColor = true;
            // 
            // txt_Counter
            // 
            this.txt_Counter.Cue = "Up/Down";
            this.txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Counter.ForeColor = System.Drawing.Color.Gray;
            this.txt_Counter.Location = new System.Drawing.Point(1528, 1194);
            this.txt_Counter.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.Size = new System.Drawing.Size(100, 32);
            this.txt_Counter.TabIndex = 311;
            this.txt_Counter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Counter.Visible = false;
            // 
            // txt_AudioPreviewPath
            // 
            this.txt_AudioPreviewPath.Cue = "Audio Preview Path";
            this.txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPreviewPath.Location = new System.Drawing.Point(1288, 1154);
            this.txt_AudioPreviewPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_AudioPreviewPath.Name = "txt_AudioPreviewPath";
            this.txt_AudioPreviewPath.ReadOnly = true;
            this.txt_AudioPreviewPath.Size = new System.Drawing.Size(224, 32);
            this.txt_AudioPreviewPath.TabIndex = 301;
            this.txt_AudioPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_AudioPreviewPath.Visible = false;
            // 
            // txt_AudioPath
            // 
            this.txt_AudioPath.Cue = "Audio Path";
            this.txt_AudioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPath.Location = new System.Drawing.Point(1288, 1110);
            this.txt_AudioPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_AudioPath.Name = "txt_AudioPath";
            this.txt_AudioPath.ReadOnly = true;
            this.txt_AudioPath.Size = new System.Drawing.Size(224, 32);
            this.txt_AudioPath.TabIndex = 300;
            this.txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_AudioPath.Visible = false;
            // 
            // txt_Platform
            // 
            this.txt_Platform.Cue = "Arrangements";
            this.txt_Platform.Enabled = false;
            this.txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Platform.ForeColor = System.Drawing.Color.Black;
            this.txt_Platform.Location = new System.Drawing.Point(791, 931);
            this.txt_Platform.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Platform.Name = "txt_Platform";
            this.txt_Platform.Size = new System.Drawing.Size(92, 32);
            this.txt_Platform.TabIndex = 288;
            this.txt_Platform.Text = "Platform";
            this.txt_Platform.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SongsHSANPath
            // 
            this.txt_SongsHSANPath.Cue = "Arrangements";
            this.txt_SongsHSANPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SongsHSANPath.ForeColor = System.Drawing.Color.Black;
            this.txt_SongsHSANPath.Location = new System.Drawing.Point(788, 1011);
            this.txt_SongsHSANPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_SongsHSANPath.Name = "txt_SongsHSANPath";
            this.txt_SongsHSANPath.ReadOnly = true;
            this.txt_SongsHSANPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_SongsHSANPath.Size = new System.Drawing.Size(440, 32);
            this.txt_SongsHSANPath.TabIndex = 287;
            this.txt_SongsHSANPath.Text = "Songs.HSAN Path";
            this.txt_SongsHSANPath.Visible = false;
            // 
            // txt_PSARCName
            // 
            this.txt_PSARCName.Cue = "Arrangements";
            this.txt_PSARCName.Enabled = false;
            this.txt_PSARCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PSARCName.ForeColor = System.Drawing.Color.Black;
            this.txt_PSARCName.Location = new System.Drawing.Point(973, 931);
            this.txt_PSARCName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_PSARCName.Name = "txt_PSARCName";
            this.txt_PSARCName.Size = new System.Drawing.Size(251, 32);
            this.txt_PSARCName.TabIndex = 286;
            this.txt_PSARCName.Text = "PSARC Name";
            this.txt_PSARCName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album Art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(1620, 1136);
            this.txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.ReadOnly = true;
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(247, 32);
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
            this.txt_Identifier.Location = new System.Drawing.Point(784, 969);
            this.txt_Identifier.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Identifier.Name = "txt_Identifier";
            this.txt_Identifier.Size = new System.Drawing.Size(440, 32);
            this.txt_Identifier.TabIndex = 277;
            // 
            // txt_Arrangements
            // 
            this.txt_Arrangements.Cue = "Arrangements";
            this.txt_Arrangements.Enabled = false;
            this.txt_Arrangements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Arrangements.ForeColor = System.Drawing.Color.Gray;
            this.txt_Arrangements.Location = new System.Drawing.Point(784, 890);
            this.txt_Arrangements.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Arrangements.Name = "txt_Arrangements";
            this.txt_Arrangements.Size = new System.Drawing.Size(440, 32);
            this.txt_Arrangements.TabIndex = 134;
            this.txt_Arrangements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Enabled = false;
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(339, 976);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(440, 32);
            this.txt_Title.TabIndex = 133;
            // 
            // txt_ArtistSort
            // 
            this.txt_ArtistSort.Cue = "ArtistSort";
            this.txt_ArtistSort.Enabled = false;
            this.txt_ArtistSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSort.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSort.Location = new System.Drawing.Point(339, 934);
            this.txt_ArtistSort.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_ArtistSort.Name = "txt_ArtistSort";
            this.txt_ArtistSort.Size = new System.Drawing.Size(440, 32);
            this.txt_ArtistSort.TabIndex = 132;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Enabled = false;
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(339, 892);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(440, 32);
            this.txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumYear
            // 
            this.txt_AlbumYear.Cue = "AlbumYear";
            this.txt_AlbumYear.Enabled = false;
            this.txt_AlbumYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumYear.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumYear.Location = new System.Drawing.Point(687, 1019);
            this.txt_AlbumYear.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_AlbumYear.Name = "txt_AlbumYear";
            this.txt_AlbumYear.Size = new System.Drawing.Size(92, 32);
            this.txt_AlbumYear.TabIndex = 130;
            this.txt_AlbumYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(185, 935);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(75, 32);
            this.txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Enabled = false;
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(339, 1019);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(340, 32);
            this.txt_Album.TabIndex = 126;
            this.txt_Album.TextChanged += new System.EventHandler(this.txt_Album_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 1069);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 25);
            this.label1.TabIndex = 395;
            this.label1.Text = "Filter:";
            // 
            // Cache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2353, 1248);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbx_RemoveDD);
            this.Controls.Add(this.cmb_Filter);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.chbx_Selected);
            this.Controls.Add(this.btn_SelectNone);
            this.Controls.Add(this.btn_GroupLoad);
            this.Controls.Add(this.btn_GroupSave);
            this.Controls.Add(this.btn_GroupsAdd);
            this.Controls.Add(this.btn_GroupsRemove);
            this.Controls.Add(this.chbx_AllGroups);
            this.Controls.Add(this.chbx_Group);
            this.Controls.Add(this.chbx_RemoveBassDD);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Prev);
            this.Controls.Add(this.btn_NextItem);
            this.Controls.Add(this.txt_Counter);
            this.Controls.Add(this.btn_ExpandSelCrossP);
            this.Controls.Add(this.chbx_Autosave);
            this.Controls.Add(this.btn_OpeHSAN);
            this.Controls.Add(this.btn_OpenCorrespondence);
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
            this.Controls.Add(this.btn_PlayPreview);
            this.Controls.Add(this.btn_PlayAudio);
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
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Cache";
            this.Text = "List of songs delivered with the Retail version of Rocksmith";
            this.Load += new System.EventHandler(this.Cache_Load);
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
        private System.Windows.Forms.Button btn_PlayPreview;
        private System.Windows.Forms.Button btn_PlayAudio;
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
        private System.Windows.Forms.Button btn_OpenCorrespondence;
        private System.Windows.Forms.Button btn_OpeHSAN;
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
        private System.Windows.Forms.Button btn_GroupsAdd;
        private System.Windows.Forms.Button btn_GroupsRemove;
        private System.Windows.Forms.CheckedListBox chbx_AllGroups;
        private System.Windows.Forms.ComboBox chbx_Group;
        private System.Windows.Forms.Button btn_GroupLoad;
        private System.Windows.Forms.Button btn_GroupSave;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckBox chbx_Selected;
        private System.Windows.Forms.Button btn_SelectNone;
        private System.Windows.Forms.ComboBox cmb_Filter;
        private System.Windows.Forms.CheckBox chbx_RemoveDD;
        private System.Windows.Forms.Label label1;
    }
}