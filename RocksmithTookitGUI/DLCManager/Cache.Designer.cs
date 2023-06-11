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
            DataGridView1 = new System.Windows.Forms.DataGridView();
            picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            chbx_Removed = new System.Windows.Forms.CheckBox();
            btn_Save = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            btn_Close = new System.Windows.Forms.Button();
            btn_DecompressAll = new System.Windows.Forms.Button();
            rtxt_Comments = new System.Windows.Forms.RichTextBox();
            btn_GenerateHSAN = new System.Windows.Forms.Button();
            lbl_NoRec = new System.Windows.Forms.Label();
            btn_PlayPreview = new System.Windows.Forms.Button();
            btn_PlayAudio = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            btn_InvertAll = new System.Windows.Forms.Button();
            chbx_Songs2Cache = new System.Windows.Forms.CheckBox();
            chbx_AutoPlay = new System.Windows.Forms.CheckBox();
            btn_FTP = new System.Windows.Forms.Button();
            pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            btn_OpenCorrespondence = new System.Windows.Forms.Button();
            btn_OpeHSAN = new System.Windows.Forms.Button();
            chbx_Autosave = new System.Windows.Forms.CheckBox();
            btn_ExpandSelCrossP = new System.Windows.Forms.Button();
            cbx_Format = new System.Windows.Forms.ComboBox();
            btn_SteamDLCFolder = new System.Windows.Forms.Button();
            btn_NextItem = new System.Windows.Forms.Button();
            btn_Prev = new System.Windows.Forms.Button();
            chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            txt_FTPPath = new CueTextBox();
            chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            btn_GroupsAdd = new System.Windows.Forms.Button();
            btn_GroupsRemove = new System.Windows.Forms.Button();
            chbx_AllGroups = new System.Windows.Forms.CheckedListBox();
            chbx_Group = new System.Windows.Forms.ComboBox();
            btn_GroupLoad = new System.Windows.Forms.Button();
            btn_GroupSave = new System.Windows.Forms.Button();
            btn_SelectAll = new System.Windows.Forms.Button();
            chbx_Selected = new System.Windows.Forms.CheckBox();
            btn_SelectNone = new System.Windows.Forms.Button();
            cmb_Filter = new System.Windows.Forms.ComboBox();
            chbx_RemoveDD = new System.Windows.Forms.CheckBox();
            txt_Counter = new CueTextBox();
            txt_AudioPreviewPath = new CueTextBox();
            txt_AudioPath = new CueTextBox();
            txt_Platform = new CueTextBox();
            txt_SongsHSANPath = new CueTextBox();
            txt_PSARCName = new CueTextBox();
            txt_AlbumArtPath = new CueTextBox();
            txt_Identifier = new CueTextBox();
            txt_Arrangements = new CueTextBox();
            txt_Title = new CueTextBox();
            txt_ArtistSort = new CueTextBox();
            txt_Artist = new CueTextBox();
            txt_AlbumYear = new CueTextBox();
            txt_ID = new CueTextBox();
            txt_Album = new CueTextBox();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridView1
            // 
            DataGridView1.AllowUserToOrderColumns = true;
            DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            DataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridView1.Location = new System.Drawing.Point(16, 4);
            DataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DataGridView1.RowHeadersWidth = 61;
            DataGridView1.Size = new System.Drawing.Size(2328, 877);
            DataGridView1.TabIndex = 38;
            DataGridView1.CellClick += DataGridView1_CellContentClick_1;
            DataGridView1.CellContentDoubleClick += DataGridView1_CellContentClick_1;
            DataGridView1.CellDoubleClick += DataGridView1_CellContentClick_1;
            DataGridView1.CellLeave += DataGridView1_CellLeave;
            DataGridView1.SelectionChanged += ChangeEdit;
            // 
            // picbx_AlbumArtPath
            // 
            picbx_AlbumArtPath.Location = new System.Drawing.Point(1620, 893);
            picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            picbx_AlbumArtPath.Size = new System.Drawing.Size(252, 252);
            picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picbx_AlbumArtPath.TabIndex = 127;
            picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Removed
            // 
            chbx_Removed.AutoSize = true;
            chbx_Removed.Location = new System.Drawing.Point(200, 980);
            chbx_Removed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Removed.Name = "chbx_Removed";
            chbx_Removed.Size = new System.Drawing.Size(146, 36);
            chbx_Removed.TabIndex = 124;
            chbx_Removed.Text = "Removed";
            chbx_Removed.UseVisualStyleBackColor = true;
            chbx_Removed.CheckStateChanged += chbx_Removed_CheckStateChanged;
            // 
            // btn_Save
            // 
            btn_Save.ForeColor = System.Drawing.Color.Green;
            btn_Save.Location = new System.Drawing.Point(2160, 884);
            btn_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new System.Drawing.Size(168, 51);
            btn_Save.TabIndex = 123;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += button8_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(156, 1060);
            button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(172, 85);
            button3.TabIndex = 122;
            button3.Text = "Open DB in M$ Access";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button1_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_Close.Location = new System.Drawing.Point(2196, 1188);
            btn_Close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(144, 51);
            btn_Close.TabIndex = 273;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // btn_DecompressAll
            // 
            btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_DecompressAll.Location = new System.Drawing.Point(4, 1060);
            btn_DecompressAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_DecompressAll.Name = "btn_DecompressAll";
            btn_DecompressAll.Size = new System.Drawing.Size(140, 85);
            btn_DecompressAll.TabIndex = 276;
            btn_DecompressAll.Text = "Open Main DB";
            btn_DecompressAll.UseVisualStyleBackColor = false;
            btn_DecompressAll.Click += btn_DecompressAll_Click;
            // 
            // rtxt_Comments
            // 
            rtxt_Comments.Location = new System.Drawing.Point(1244, 912);
            rtxt_Comments.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            rtxt_Comments.Name = "rtxt_Comments";
            rtxt_Comments.Size = new System.Drawing.Size(336, 104);
            rtxt_Comments.TabIndex = 279;
            rtxt_Comments.Text = "";
            // 
            // btn_GenerateHSAN
            // 
            btn_GenerateHSAN.Location = new System.Drawing.Point(1876, 1060);
            btn_GenerateHSAN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GenerateHSAN.Name = "btn_GenerateHSAN";
            btn_GenerateHSAN.Size = new System.Drawing.Size(220, 101);
            btn_GenerateHSAN.TabIndex = 280;
            btn_GenerateHSAN.Text = "Regenerate HSAN and  Pack";
            btn_GenerateHSAN.UseVisualStyleBackColor = true;
            btn_GenerateHSAN.Click += btn_GenerateHSAN_Click;
            // 
            // lbl_NoRec
            // 
            lbl_NoRec.AutoSize = true;
            lbl_NoRec.Location = new System.Drawing.Point(192, 893);
            lbl_NoRec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_NoRec.Name = "lbl_NoRec";
            lbl_NoRec.Size = new System.Drawing.Size(104, 32);
            lbl_NoRec.TabIndex = 281;
            lbl_NoRec.Text = " Records";
            // 
            // btn_PlayPreview
            // 
            btn_PlayPreview.Location = new System.Drawing.Point(1448, 1020);
            btn_PlayPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_PlayPreview.Name = "btn_PlayPreview";
            btn_PlayPreview.Size = new System.Drawing.Size(136, 51);
            btn_PlayPreview.TabIndex = 283;
            btn_PlayPreview.Text = "Preview";
            btn_PlayPreview.UseVisualStyleBackColor = true;
            btn_PlayPreview.Click += btn_PlayPreview_Click;
            // 
            // btn_PlayAudio
            // 
            btn_PlayAudio.Location = new System.Drawing.Point(1244, 1020);
            btn_PlayAudio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_PlayAudio.Name = "btn_PlayAudio";
            btn_PlayAudio.Size = new System.Drawing.Size(136, 51);
            btn_PlayAudio.TabIndex = 282;
            btn_PlayAudio.Text = "Audio";
            btn_PlayAudio.UseVisualStyleBackColor = true;
            btn_PlayAudio.Click += btn_PlayAudio_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(1236, 880);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(130, 32);
            label2.TabIndex = 284;
            label2.Text = "Comments";
            // 
            // btn_InvertAll
            // 
            btn_InvertAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_InvertAll.Location = new System.Drawing.Point(12, 996);
            btn_InvertAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_InvertAll.Name = "btn_InvertAll";
            btn_InvertAll.Size = new System.Drawing.Size(148, 51);
            btn_InvertAll.TabIndex = 285;
            btn_InvertAll.Text = "Invert All";
            btn_InvertAll.UseVisualStyleBackColor = true;
            btn_InvertAll.Click += btn_InvertAll_Click;
            // 
            // chbx_Songs2Cache
            // 
            chbx_Songs2Cache.AutoSize = true;
            chbx_Songs2Cache.Checked = true;
            chbx_Songs2Cache.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_Songs2Cache.Location = new System.Drawing.Point(2100, 1064);
            chbx_Songs2Cache.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Songs2Cache.Name = "chbx_Songs2Cache";
            chbx_Songs2Cache.Size = new System.Drawing.Size(224, 36);
            chbx_Songs2Cache.TabIndex = 289;
            chbx_Songs2Cache.Text = "Only cache.psarc";
            chbx_Songs2Cache.UseVisualStyleBackColor = true;
            // 
            // chbx_AutoPlay
            // 
            chbx_AutoPlay.AutoSize = true;
            chbx_AutoPlay.Enabled = false;
            chbx_AutoPlay.Location = new System.Drawing.Point(1336, 1072);
            chbx_AutoPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_AutoPlay.Name = "chbx_AutoPlay";
            chbx_AutoPlay.Size = new System.Drawing.Size(172, 36);
            chbx_AutoPlay.TabIndex = 290;
            chbx_AutoPlay.Text = "<AutoPlay>";
            chbx_AutoPlay.UseVisualStyleBackColor = true;
            // 
            // btn_FTP
            // 
            btn_FTP.Location = new System.Drawing.Point(8, 28);
            btn_FTP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_FTP.Name = "btn_FTP";
            btn_FTP.Size = new System.Drawing.Size(220, 88);
            btn_FTP.TabIndex = 291;
            btn_FTP.Text = "Copy/FTP Back to Game Folder";
            btn_FTP.UseVisualStyleBackColor = true;
            btn_FTP.Click += btn_FTP_Click;
            // 
            // pB_ReadDLCs
            // 
            pB_ReadDLCs.Location = new System.Drawing.Point(1644, 1189);
            pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            pB_ReadDLCs.Maximum = 20000;
            pB_ReadDLCs.Name = "pB_ReadDLCs";
            pB_ReadDLCs.Size = new System.Drawing.Size(548, 48);
            pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.TabIndex = 293;
            // 
            // btn_OpenCorrespondence
            // 
            btn_OpenCorrespondence.BackColor = System.Drawing.SystemColors.Info;
            btn_OpenCorrespondence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_OpenCorrespondence.Location = new System.Drawing.Point(4, 1152);
            btn_OpenCorrespondence.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_OpenCorrespondence.Name = "btn_OpenCorrespondence";
            btn_OpenCorrespondence.Size = new System.Drawing.Size(220, 92);
            btn_OpenCorrespondence.TabIndex = 302;
            btn_OpenCorrespondence.Text = "Open WEM2OGG Corespondence DB";
            btn_OpenCorrespondence.UseVisualStyleBackColor = false;
            btn_OpenCorrespondence.Click += btn_OpenCorrespondence_Click;
            // 
            // btn_OpeHSAN
            // 
            btn_OpeHSAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_OpeHSAN.Location = new System.Drawing.Point(244, 1152);
            btn_OpeHSAN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_OpeHSAN.Name = "btn_OpeHSAN";
            btn_OpeHSAN.Size = new System.Drawing.Size(152, 92);
            btn_OpeHSAN.TabIndex = 303;
            btn_OpeHSAN.Text = "Open HSAN file";
            btn_OpeHSAN.UseVisualStyleBackColor = true;
            btn_OpeHSAN.Click += btn_OpeHSAN_Click;
            // 
            // chbx_Autosave
            // 
            chbx_Autosave.AutoSize = true;
            chbx_Autosave.Checked = true;
            chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_Autosave.Location = new System.Drawing.Point(1996, 891);
            chbx_Autosave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Autosave.Name = "chbx_Autosave";
            chbx_Autosave.Size = new System.Drawing.Size(147, 36);
            chbx_Autosave.TabIndex = 304;
            chbx_Autosave.Text = "AutoSave";
            chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // btn_ExpandSelCrossP
            // 
            btn_ExpandSelCrossP.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_ExpandSelCrossP.Location = new System.Drawing.Point(412, 1152);
            btn_ExpandSelCrossP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_ExpandSelCrossP.Name = "btn_ExpandSelCrossP";
            btn_ExpandSelCrossP.Size = new System.Drawing.Size(272, 92);
            btn_ExpandSelCrossP.TabIndex = 305;
            btn_ExpandSelCrossP.Text = "Extend current Selection cross Platforms";
            btn_ExpandSelCrossP.UseVisualStyleBackColor = true;
            btn_ExpandSelCrossP.Click += btn_ExpandSelCrossP_Click;
            // 
            // cbx_Format
            // 
            cbx_Format.FormattingEnabled = true;
            cbx_Format.Items.AddRange(new object[] { "PC", "PS3", "Mac", "XBOX360" });
            cbx_Format.Location = new System.Drawing.Point(228, 72);
            cbx_Format.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbx_Format.Name = "cbx_Format";
            cbx_Format.Size = new System.Drawing.Size(200, 40);
            cbx_Format.TabIndex = 306;
            cbx_Format.Text = "PS3";
            cbx_Format.SelectedIndexChanged += cbx_Format_SelectedIndexChanged;
            // 
            // btn_SteamDLCFolder
            // 
            btn_SteamDLCFolder.Location = new System.Drawing.Point(392, 32);
            btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            btn_SteamDLCFolder.Size = new System.Drawing.Size(44, 28);
            btn_SteamDLCFolder.TabIndex = 307;
            btn_SteamDLCFolder.Text = "...";
            btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            btn_SteamDLCFolder.Click += btn_SteamDLCFolder_Click;
            // 
            // btn_NextItem
            // 
            btn_NextItem.Location = new System.Drawing.Point(296, 932);
            btn_NextItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_NextItem.Name = "btn_NextItem";
            btn_NextItem.Size = new System.Drawing.Size(36, 43);
            btn_NextItem.TabIndex = 312;
            btn_NextItem.Text = ">";
            btn_NextItem.UseVisualStyleBackColor = true;
            btn_NextItem.Click += btn_NextItem_Click;
            // 
            // btn_Prev
            // 
            btn_Prev.Location = new System.Drawing.Point(260, 932);
            btn_Prev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btn_Prev.Name = "btn_Prev";
            btn_Prev.Size = new System.Drawing.Size(36, 43);
            btn_Prev.TabIndex = 313;
            btn_Prev.Text = "<";
            btn_Prev.UseVisualStyleBackColor = true;
            btn_Prev.Click += btn_Prev_Click;
            // 
            // chbx_PreSavedFTP
            // 
            chbx_PreSavedFTP.FormattingEnabled = true;
            chbx_PreSavedFTP.Items.AddRange(new object[] { "US", "EU" });
            chbx_PreSavedFTP.Location = new System.Drawing.Point(2281, 1132);
            chbx_PreSavedFTP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            chbx_PreSavedFTP.Size = new System.Drawing.Size(88, 40);
            chbx_PreSavedFTP.TabIndex = 329;
            chbx_PreSavedFTP.Text = "US";
            chbx_PreSavedFTP.Visible = false;
            chbx_PreSavedFTP.SelectedIndexChanged += chbx_PreSavedFTP_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txt_FTPPath);
            groupBox1.Controls.Add(btn_FTP);
            groupBox1.Controls.Add(cbx_Format);
            groupBox1.Controls.Add(btn_SteamDLCFolder);
            groupBox1.Location = new System.Drawing.Point(1872, 936);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            groupBox1.Size = new System.Drawing.Size(456, 128);
            groupBox1.TabIndex = 330;
            groupBox1.TabStop = false;
            groupBox1.Text = "Package";
            // 
            // txt_FTPPath
            // 
            txt_FTPPath.Cue = "FTP_Path";
            txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            txt_FTPPath.Location = new System.Drawing.Point(228, 28);
            txt_FTPPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_FTPPath.Name = "txt_FTPPath";
            txt_FTPPath.Size = new System.Drawing.Size(148, 32);
            txt_FTPPath.TabIndex = 292;
            txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chbx_RemoveBassDD
            // 
            chbx_RemoveBassDD.AutoSize = true;
            chbx_RemoveBassDD.Checked = true;
            chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_RemoveBassDD.Location = new System.Drawing.Point(2100, 1100);
            chbx_RemoveBassDD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            chbx_RemoveBassDD.Size = new System.Drawing.Size(226, 36);
            chbx_RemoveBassDD.TabIndex = 331;
            chbx_RemoveBassDD.Text = "Remove Bass DD";
            chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // btn_GroupsAdd
            // 
            btn_GroupsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_GroupsAdd.Location = new System.Drawing.Point(1432, 1196);
            btn_GroupsAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GroupsAdd.Name = "btn_GroupsAdd";
            btn_GroupsAdd.Size = new System.Drawing.Size(44, 40);
            btn_GroupsAdd.TabIndex = 385;
            btn_GroupsAdd.Text = "+";
            btn_GroupsAdd.UseVisualStyleBackColor = true;
            btn_GroupsAdd.Click += btn_GroupsAdd_Click;
            // 
            // btn_GroupsRemove
            // 
            btn_GroupsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_GroupsRemove.Location = new System.Drawing.Point(1476, 1196);
            btn_GroupsRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GroupsRemove.Name = "btn_GroupsRemove";
            btn_GroupsRemove.Size = new System.Drawing.Size(44, 40);
            btn_GroupsRemove.TabIndex = 384;
            btn_GroupsRemove.Text = "-";
            btn_GroupsRemove.UseVisualStyleBackColor = true;
            btn_GroupsRemove.Click += btn_GroupsRemove_Click;
            // 
            // chbx_AllGroups
            // 
            chbx_AllGroups.CheckOnClick = true;
            chbx_AllGroups.FormattingEnabled = true;
            chbx_AllGroups.Location = new System.Drawing.Point(692, 1056);
            chbx_AllGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_AllGroups.Name = "chbx_AllGroups";
            chbx_AllGroups.Size = new System.Drawing.Size(368, 148);
            chbx_AllGroups.Sorted = true;
            chbx_AllGroups.TabIndex = 386;
            chbx_AllGroups.SelectedValueChanged += chbx_AllGroups_SelectedValueChanged;
            // 
            // chbx_Group
            // 
            chbx_Group.FormattingEnabled = true;
            chbx_Group.Location = new System.Drawing.Point(1076, 1197);
            chbx_Group.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Group.Name = "chbx_Group";
            chbx_Group.Size = new System.Drawing.Size(352, 40);
            chbx_Group.TabIndex = 383;
            // 
            // btn_GroupLoad
            // 
            btn_GroupLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_GroupLoad.Location = new System.Drawing.Point(1183, 1141);
            btn_GroupLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GroupLoad.Name = "btn_GroupLoad";
            btn_GroupLoad.Size = new System.Drawing.Size(116, 49);
            btn_GroupLoad.TabIndex = 388;
            btn_GroupLoad.Text = "Load List";
            btn_GroupLoad.UseVisualStyleBackColor = true;
            btn_GroupLoad.Click += btn_GroupLoad_Click;
            // 
            // btn_GroupSave
            // 
            btn_GroupSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_GroupSave.Location = new System.Drawing.Point(1076, 1141);
            btn_GroupSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GroupSave.Name = "btn_GroupSave";
            btn_GroupSave.Size = new System.Drawing.Size(100, 49);
            btn_GroupSave.TabIndex = 387;
            btn_GroupSave.Text = "Save List";
            btn_GroupSave.UseVisualStyleBackColor = true;
            btn_GroupSave.Click += btn_GroupSave_Click;
            // 
            // btn_SelectAll
            // 
            btn_SelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_SelectAll.Location = new System.Drawing.Point(12, 956);
            btn_SelectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_SelectAll.Name = "btn_SelectAll";
            btn_SelectAll.Size = new System.Drawing.Size(148, 45);
            btn_SelectAll.TabIndex = 390;
            btn_SelectAll.Text = "Select All";
            btn_SelectAll.UseVisualStyleBackColor = true;
            btn_SelectAll.Click += btn_SelectAll_Click;
            // 
            // chbx_Selected
            // 
            chbx_Selected.AutoSize = true;
            chbx_Selected.Location = new System.Drawing.Point(200, 1019);
            chbx_Selected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Selected.Name = "chbx_Selected";
            chbx_Selected.Size = new System.Drawing.Size(137, 36);
            chbx_Selected.TabIndex = 389;
            chbx_Selected.Text = "Selected";
            chbx_Selected.UseVisualStyleBackColor = true;
            chbx_Selected.CheckStateChanged += chbx_Selected_CheckStateChanged;
            chbx_Selected.Click += chbx_Selected_Click;
            // 
            // btn_SelectNone
            // 
            btn_SelectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_SelectNone.Location = new System.Drawing.Point(12, 912);
            btn_SelectNone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_SelectNone.Name = "btn_SelectNone";
            btn_SelectNone.Size = new System.Drawing.Size(148, 45);
            btn_SelectNone.TabIndex = 391;
            btn_SelectNone.Text = "Select None";
            btn_SelectNone.UseVisualStyleBackColor = true;
            btn_SelectNone.Click += btn_SelectNone_Click;
            // 
            // cmb_Filter
            // 
            cmb_Filter.FormattingEnabled = true;
            cmb_Filter.Items.AddRange(new object[] { "0ALL", "PC", "Mac", "PS3", "XBOX", "Removed", "Selected" });
            cmb_Filter.Location = new System.Drawing.Point(340, 1100);
            cmb_Filter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmb_Filter.Name = "cmb_Filter";
            cmb_Filter.Size = new System.Drawing.Size(340, 40);
            cmb_Filter.TabIndex = 393;
            cmb_Filter.SelectedValueChanged += cmb_Filter_SelectedValueChanged;
            // 
            // chbx_RemoveDD
            // 
            chbx_RemoveDD.AutoSize = true;
            chbx_RemoveDD.Checked = true;
            chbx_RemoveDD.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_RemoveDD.Enabled = false;
            chbx_RemoveDD.Location = new System.Drawing.Point(2100, 1136);
            chbx_RemoveDD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_RemoveDD.Name = "chbx_RemoveDD";
            chbx_RemoveDD.Size = new System.Drawing.Size(173, 36);
            chbx_RemoveDD.TabIndex = 394;
            chbx_RemoveDD.Text = "Remove DD";
            chbx_RemoveDD.UseVisualStyleBackColor = true;
            // 
            // txt_Counter
            // 
            txt_Counter.Cue = "Up/Down";
            txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Counter.ForeColor = System.Drawing.Color.Gray;
            txt_Counter.Location = new System.Drawing.Point(1528, 1195);
            txt_Counter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Counter.Name = "txt_Counter";
            txt_Counter.Size = new System.Drawing.Size(100, 32);
            txt_Counter.TabIndex = 311;
            txt_Counter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_Counter.Visible = false;
            // 
            // txt_AudioPreviewPath
            // 
            txt_AudioPreviewPath.Cue = "Audio Preview Path";
            txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            txt_AudioPreviewPath.Location = new System.Drawing.Point(1304, 1148);
            txt_AudioPreviewPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AudioPreviewPath.Name = "txt_AudioPreviewPath";
            txt_AudioPreviewPath.ReadOnly = true;
            txt_AudioPreviewPath.Size = new System.Drawing.Size(307, 32);
            txt_AudioPreviewPath.TabIndex = 301;
            txt_AudioPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AudioPreviewPath.Visible = false;
            // 
            // txt_AudioPath
            // 
            txt_AudioPath.Cue = "Audio Path";
            txt_AudioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AudioPath.ForeColor = System.Drawing.Color.Gray;
            txt_AudioPath.Location = new System.Drawing.Point(1304, 1109);
            txt_AudioPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AudioPath.Name = "txt_AudioPath";
            txt_AudioPath.ReadOnly = true;
            txt_AudioPath.Size = new System.Drawing.Size(307, 32);
            txt_AudioPath.TabIndex = 300;
            txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AudioPath.Visible = false;
            // 
            // txt_Platform
            // 
            txt_Platform.Cue = "Arrangements";
            txt_Platform.Enabled = false;
            txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Platform.ForeColor = System.Drawing.Color.Black;
            txt_Platform.Location = new System.Drawing.Point(792, 932);
            txt_Platform.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Platform.Name = "txt_Platform";
            txt_Platform.Size = new System.Drawing.Size(92, 32);
            txt_Platform.TabIndex = 288;
            txt_Platform.Text = "Platform";
            txt_Platform.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SongsHSANPath
            // 
            txt_SongsHSANPath.Cue = "Arrangements";
            txt_SongsHSANPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_SongsHSANPath.ForeColor = System.Drawing.Color.Black;
            txt_SongsHSANPath.Location = new System.Drawing.Point(788, 1012);
            txt_SongsHSANPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_SongsHSANPath.Name = "txt_SongsHSANPath";
            txt_SongsHSANPath.ReadOnly = true;
            txt_SongsHSANPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            txt_SongsHSANPath.Size = new System.Drawing.Size(440, 32);
            txt_SongsHSANPath.TabIndex = 287;
            txt_SongsHSANPath.Text = "Songs.HSAN Path";
            txt_SongsHSANPath.Visible = false;
            // 
            // txt_PSARCName
            // 
            txt_PSARCName.Cue = "Arrangements";
            txt_PSARCName.Enabled = false;
            txt_PSARCName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_PSARCName.ForeColor = System.Drawing.Color.Black;
            txt_PSARCName.Location = new System.Drawing.Point(972, 932);
            txt_PSARCName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_PSARCName.Name = "txt_PSARCName";
            txt_PSARCName.Size = new System.Drawing.Size(252, 32);
            txt_PSARCName.TabIndex = 286;
            txt_PSARCName.Text = "PSARC Name";
            txt_PSARCName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlbumArtPath
            // 
            txt_AlbumArtPath.Cue = "Album Art Path";
            txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumArtPath.Location = new System.Drawing.Point(1620, 1148);
            txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            txt_AlbumArtPath.ReadOnly = true;
            txt_AlbumArtPath.Size = new System.Drawing.Size(248, 32);
            txt_AlbumArtPath.TabIndex = 278;
            txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            txt_AlbumArtPath.Visible = false;
            // 
            // txt_Identifier
            // 
            txt_Identifier.Cue = "IDentifier";
            txt_Identifier.Enabled = false;
            txt_Identifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Identifier.ForeColor = System.Drawing.Color.Gray;
            txt_Identifier.Location = new System.Drawing.Point(784, 968);
            txt_Identifier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Identifier.Name = "txt_Identifier";
            txt_Identifier.Size = new System.Drawing.Size(440, 32);
            txt_Identifier.TabIndex = 277;
            // 
            // txt_Arrangements
            // 
            txt_Arrangements.Cue = "Arrangements";
            txt_Arrangements.Enabled = false;
            txt_Arrangements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Arrangements.ForeColor = System.Drawing.Color.Gray;
            txt_Arrangements.Location = new System.Drawing.Point(784, 891);
            txt_Arrangements.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Arrangements.Name = "txt_Arrangements";
            txt_Arrangements.Size = new System.Drawing.Size(440, 32);
            txt_Arrangements.TabIndex = 134;
            txt_Arrangements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Title
            // 
            txt_Title.Cue = "Title";
            txt_Title.Enabled = false;
            txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Title.ForeColor = System.Drawing.Color.Gray;
            txt_Title.Location = new System.Drawing.Point(340, 976);
            txt_Title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Title.Name = "txt_Title";
            txt_Title.Size = new System.Drawing.Size(440, 32);
            txt_Title.TabIndex = 133;
            // 
            // txt_ArtistSort
            // 
            txt_ArtistSort.Cue = "ArtistSort";
            txt_ArtistSort.Enabled = false;
            txt_ArtistSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_ArtistSort.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistSort.Location = new System.Drawing.Point(340, 933);
            txt_ArtistSort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_ArtistSort.Name = "txt_ArtistSort";
            txt_ArtistSort.Size = new System.Drawing.Size(440, 32);
            txt_ArtistSort.TabIndex = 132;
            // 
            // txt_Artist
            // 
            txt_Artist.Cue = "Artist";
            txt_Artist.Enabled = false;
            txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Artist.ForeColor = System.Drawing.Color.Gray;
            txt_Artist.Location = new System.Drawing.Point(340, 892);
            txt_Artist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Artist.Name = "txt_Artist";
            txt_Artist.Size = new System.Drawing.Size(440, 32);
            txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumYear
            // 
            txt_AlbumYear.Cue = "AlbumYear";
            txt_AlbumYear.Enabled = false;
            txt_AlbumYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AlbumYear.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumYear.Location = new System.Drawing.Point(688, 1020);
            txt_AlbumYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AlbumYear.Name = "txt_AlbumYear";
            txt_AlbumYear.Size = new System.Drawing.Size(92, 32);
            txt_AlbumYear.TabIndex = 130;
            txt_AlbumYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            txt_ID.Cue = "ID";
            txt_ID.Enabled = false;
            txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_ID.ForeColor = System.Drawing.Color.Gray;
            txt_ID.Location = new System.Drawing.Point(184, 936);
            txt_ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new System.Drawing.Size(76, 32);
            txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            txt_Album.Cue = "Album";
            txt_Album.Enabled = false;
            txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Album.ForeColor = System.Drawing.Color.Gray;
            txt_Album.Location = new System.Drawing.Point(340, 1020);
            txt_Album.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Album.Name = "txt_Album";
            txt_Album.Size = new System.Drawing.Size(340, 32);
            txt_Album.TabIndex = 126;
            txt_Album.TextChanged += txt_Album_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(340, 1068);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 32);
            label1.TabIndex = 395;
            label1.Text = "Filter:";
            // 
            // Cache
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(2408, 1288);
            Controls.Add(label1);
            Controls.Add(chbx_PreSavedFTP);
            Controls.Add(chbx_RemoveDD);
            Controls.Add(cmb_Filter);
            Controls.Add(btn_SelectAll);
            Controls.Add(chbx_Selected);
            Controls.Add(btn_SelectNone);
            Controls.Add(btn_GroupLoad);
            Controls.Add(btn_GroupSave);
            Controls.Add(btn_GroupsAdd);
            Controls.Add(btn_GroupsRemove);
            Controls.Add(chbx_AllGroups);
            Controls.Add(chbx_Group);
            Controls.Add(chbx_RemoveBassDD);
            Controls.Add(groupBox1);
            Controls.Add(btn_Prev);
            Controls.Add(btn_NextItem);
            Controls.Add(txt_Counter);
            Controls.Add(btn_ExpandSelCrossP);
            Controls.Add(chbx_Autosave);
            Controls.Add(btn_OpeHSAN);
            Controls.Add(btn_OpenCorrespondence);
            Controls.Add(txt_AudioPreviewPath);
            Controls.Add(txt_AudioPath);
            Controls.Add(pB_ReadDLCs);
            Controls.Add(chbx_AutoPlay);
            Controls.Add(chbx_Songs2Cache);
            Controls.Add(txt_Platform);
            Controls.Add(txt_SongsHSANPath);
            Controls.Add(txt_PSARCName);
            Controls.Add(btn_InvertAll);
            Controls.Add(label2);
            Controls.Add(btn_PlayPreview);
            Controls.Add(btn_PlayAudio);
            Controls.Add(lbl_NoRec);
            Controls.Add(btn_GenerateHSAN);
            Controls.Add(rtxt_Comments);
            Controls.Add(txt_AlbumArtPath);
            Controls.Add(txt_Identifier);
            Controls.Add(btn_DecompressAll);
            Controls.Add(btn_Close);
            Controls.Add(txt_Arrangements);
            Controls.Add(txt_Title);
            Controls.Add(txt_ArtistSort);
            Controls.Add(txt_Artist);
            Controls.Add(txt_AlbumYear);
            Controls.Add(DataGridView1);
            Controls.Add(chbx_Removed);
            Controls.Add(picbx_AlbumArtPath);
            Controls.Add(btn_Save);
            Controls.Add(txt_ID);
            Controls.Add(button3);
            Controls.Add(txt_Album);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "Cache";
            Text = "List of songs delivered with the Retail version of Rocksmith";
            FormClosing += Cache_FormClosing;
            Load += Cache_Load;
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        //private DLCManager.Files eXisting;
        //private DLCPackageData dataNew;

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