using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using System.Data.OleDb;
using System.Data.SQLite;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class Standardization
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
            components = new System.ComponentModel.Container();
            databox = new System.Windows.Forms.DataGridView();
            btn_ChangeCover = new System.Windows.Forms.Button();
            picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            chbx_Save_All = new System.Windows.Forms.CheckBox();
            btn_Save = new System.Windows.Forms.Button();
            btn_OpenAccess = new System.Windows.Forms.Button();
            btn_Close = new System.Windows.Forms.Button();
            chbx_Include_ArtistSort = new System.Windows.Forms.CheckBox();
            btn_Apply = new System.Windows.Forms.Button();
            btn_DecompressAll = new System.Windows.Forms.Button();
            btn_CopyArtist2ArtistSort = new System.Windows.Forms.Button();
            btn_CopyTitle2TitleSort = new System.Windows.Forms.Button();
            lbl_NoRec = new System.Windows.Forms.Label();
            chbx_AutoSave = new System.Windows.Forms.CheckBox();
            btn_Delete = new System.Windows.Forms.Button();
            btn_GetSpotifyCover = new System.Windows.Forms.Button();
            chbx_Default_Cover = new System.Windows.Forms.CheckBox();
            txt_Comments = new System.Windows.Forms.RichTextBox();
            pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            pxbx_SavedSpotify = new System.Windows.Forms.PictureBox();
            lbl_corrected = new System.Windows.Forms.Label();
            lbl_SpotifyCover = new System.Windows.Forms.Label();
            btn_CorrectWithSpotify = new System.Windows.Forms.Button();
            btn_DeleteAll = new System.Windows.Forms.Button();
            btn_GetSpotifyAll = new System.Windows.Forms.Button();
            btn_CheckOnline = new System.Windows.Forms.Button();
            cbx_Groups = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            btn_ApplyCurrent = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            chbx_a5 = new System.Windows.Forms.CheckBox();
            chbx_a4 = new System.Windows.Forms.CheckBox();
            chbx_a3 = new System.Windows.Forms.CheckBox();
            chbx_a2 = new System.Windows.Forms.CheckBox();
            chbx_a1 = new System.Windows.Forms.CheckBox();
            btn_StandCover = new System.Windows.Forms.Button();
            btn_suspect = new System.Windows.Forms.Button();
            btn_GoTo = new System.Windows.Forms.Button();
            btn_SearchReset = new System.Windows.Forms.Button();
            btn_Search = new System.Windows.Forms.Button();
            btn_RemoveDuplicates = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            btn_MultiplyAutoGroup = new System.Windows.Forms.Button();
            btn_ApplyDefault = new System.Windows.Forms.Button();
            bbtn_ApplyYear = new System.Windows.Forms.Button();
            txt_Album = new CueTextBox();
            txt_ID = new CueTextBox();
            txt_AlbumArtPath = new CueTextBox();
            txt_Artist = new CueTextBox();
            txt_Artist_Correction = new CueTextBox();
            txt_Album_Correction = new CueTextBox();
            txt_AlbumArt_Correction = new CueTextBox();
            txt_Album_Short = new CueTextBox();
            txt_Year_Correction = new CueTextBox();
            txt_Artist_Short = new CueTextBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)databox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pxbx_SavedSpotify).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // databox
            // 
            databox.AllowUserToAddRows = false;
            databox.AllowUserToDeleteRows = false;
            databox.AllowUserToOrderColumns = true;
            databox.AllowUserToResizeRows = false;
            databox.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            databox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            databox.Dock = System.Windows.Forms.DockStyle.Fill;
            databox.Location = new System.Drawing.Point(0, 0);
            databox.Margin = new System.Windows.Forms.Padding(1);
            databox.MultiSelect = false;
            databox.Name = "databox";
            databox.RowHeadersWidth = 25;
            databox.Size = new System.Drawing.Size(1200, 1265);
            databox.TabIndex = 38;
            databox.CellClick += DataGridView1_CellContentClick_1;
            databox.CellLeave += DataGridView1_CellLeave;
            databox.RowLeave += databox_RowLeave;
            databox.SelectionChanged += databox_SelectionChanged;
            // 
            // btn_ChangeCover
            // 
            btn_ChangeCover.Location = new System.Drawing.Point(487, 780);
            btn_ChangeCover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_ChangeCover.Name = "btn_ChangeCover";
            btn_ChangeCover.Size = new System.Drawing.Size(106, 96);
            btn_ChangeCover.TabIndex = 129;
            btn_ChangeCover.Text = "Change cover";
            btn_ChangeCover.UseVisualStyleBackColor = true;
            btn_ChangeCover.Click += btn_ChangeCover_Click;
            // 
            // picbx_AlbumArtPath
            // 
            picbx_AlbumArtPath.Location = new System.Drawing.Point(11, 979);
            picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            picbx_AlbumArtPath.Size = new System.Drawing.Size(280, 280);
            picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            picbx_AlbumArtPath.TabIndex = 127;
            picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Save_All
            // 
            chbx_Save_All.AutoSize = true;
            chbx_Save_All.Enabled = false;
            chbx_Save_All.Location = new System.Drawing.Point(12, 241);
            chbx_Save_All.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Save_All.Name = "chbx_Save_All";
            chbx_Save_All.Size = new System.Drawing.Size(73, 36);
            chbx_Save_All.TabIndex = 124;
            chbx_Save_All.Text = "All";
            chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            btn_Save.ForeColor = System.Drawing.Color.Green;
            btn_Save.Location = new System.Drawing.Point(435, 13);
            btn_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new System.Drawing.Size(164, 51);
            btn_Save.TabIndex = 123;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += button8_Click;
            // 
            // btn_OpenAccess
            // 
            btn_OpenAccess.Location = new System.Drawing.Point(230, 52);
            btn_OpenAccess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_OpenAccess.Name = "btn_OpenAccess";
            btn_OpenAccess.Size = new System.Drawing.Size(202, 79);
            btn_OpenAccess.TabIndex = 122;
            btn_OpenAccess.Text = "Open DB in M$ Access";
            btn_OpenAccess.UseVisualStyleBackColor = true;
            btn_OpenAccess.Click += btn_OpenAccess_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_Close.Location = new System.Drawing.Point(453, 672);
            btn_Close.Margin = new System.Windows.Forms.Padding(4);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(144, 45);
            btn_Close.TabIndex = 273;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // chbx_Include_ArtistSort
            // 
            chbx_Include_ArtistSort.AutoSize = true;
            chbx_Include_ArtistSort.Checked = true;
            chbx_Include_ArtistSort.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_Include_ArtistSort.Enabled = false;
            chbx_Include_ArtistSort.Location = new System.Drawing.Point(261, 789);
            chbx_Include_ArtistSort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Include_ArtistSort.Name = "chbx_Include_ArtistSort";
            chbx_Include_ArtistSort.Size = new System.Drawing.Size(188, 36);
            chbx_Include_ArtistSort.TabIndex = 274;
            chbx_Include_ArtistSort.Text = "Incl_Artistsort";
            chbx_Include_ArtistSort.UseVisualStyleBackColor = true;
            // 
            // btn_Apply
            // 
            btn_Apply.Location = new System.Drawing.Point(5, 749);
            btn_Apply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_Apply.Name = "btn_Apply";
            btn_Apply.Size = new System.Drawing.Size(252, 80);
            btn_Apply.TabIndex = 275;
            btn_Apply.Text = "Apply changes to the Main DB";
            btn_Apply.UseVisualStyleBackColor = true;
            btn_Apply.Click += Standardization_Click;
            // 
            // btn_DecompressAll
            // 
            btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_DecompressAll.Location = new System.Drawing.Point(452, 712);
            btn_DecompressAll.Margin = new System.Windows.Forms.Padding(4);
            btn_DecompressAll.Name = "btn_DecompressAll";
            btn_DecompressAll.Size = new System.Drawing.Size(144, 67);
            btn_DecompressAll.TabIndex = 276;
            btn_DecompressAll.Text = "Open Main DB";
            btn_DecompressAll.UseVisualStyleBackColor = false;
            btn_DecompressAll.Click += btn_DecompressAll_Click;
            // 
            // btn_CopyArtist2ArtistSort
            // 
            btn_CopyArtist2ArtistSort.ForeColor = System.Drawing.Color.DodgerBlue;
            btn_CopyArtist2ArtistSort.Location = new System.Drawing.Point(230, 130);
            btn_CopyArtist2ArtistSort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_CopyArtist2ArtistSort.Name = "btn_CopyArtist2ArtistSort";
            btn_CopyArtist2ArtistSort.Size = new System.Drawing.Size(202, 41);
            btn_CopyArtist2ArtistSort.TabIndex = 277;
            btn_CopyArtist2ArtistSort.Text = "Artist->ArtistSort";
            btn_CopyArtist2ArtistSort.UseVisualStyleBackColor = true;
            btn_CopyArtist2ArtistSort.Click += btn_CopyArtist2ArtistSort_Click;
            // 
            // btn_CopyTitle2TitleSort
            // 
            btn_CopyTitle2TitleSort.ForeColor = System.Drawing.Color.DodgerBlue;
            btn_CopyTitle2TitleSort.Location = new System.Drawing.Point(230, 170);
            btn_CopyTitle2TitleSort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_CopyTitle2TitleSort.Name = "btn_CopyTitle2TitleSort";
            btn_CopyTitle2TitleSort.Size = new System.Drawing.Size(202, 39);
            btn_CopyTitle2TitleSort.TabIndex = 278;
            btn_CopyTitle2TitleSort.Text = "Title->TitleSort";
            btn_CopyTitle2TitleSort.UseVisualStyleBackColor = true;
            btn_CopyTitle2TitleSort.Click += btn_CopyTitle2TitleSort_Click;
            // 
            // lbl_NoRec
            // 
            lbl_NoRec.Location = new System.Drawing.Point(181, 16);
            lbl_NoRec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_NoRec.Name = "lbl_NoRec";
            lbl_NoRec.Size = new System.Drawing.Size(100, 56);
            lbl_NoRec.TabIndex = 279;
            lbl_NoRec.Text = " Records";
            // 
            // chbx_AutoSave
            // 
            chbx_AutoSave.AutoSize = true;
            chbx_AutoSave.Checked = true;
            chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            chbx_AutoSave.Location = new System.Drawing.Point(291, 13);
            chbx_AutoSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_AutoSave.Name = "chbx_AutoSave";
            chbx_AutoSave.Size = new System.Drawing.Size(147, 36);
            chbx_AutoSave.TabIndex = 384;
            chbx_AutoSave.Text = "AutoSave";
            chbx_AutoSave.UseVisualStyleBackColor = true;
            chbx_AutoSave.CheckedChanged += chbx_AutoSave_CheckedChanged;
            // 
            // btn_Delete
            // 
            btn_Delete.Location = new System.Drawing.Point(435, 62);
            btn_Delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_Delete.Name = "btn_Delete";
            btn_Delete.Size = new System.Drawing.Size(164, 51);
            btn_Delete.TabIndex = 385;
            btn_Delete.Text = "Delete";
            btn_Delete.UseVisualStyleBackColor = true;
            btn_Delete.Click += btn_Delete_Click;
            // 
            // btn_GetSpotifyCover
            // 
            btn_GetSpotifyCover.Location = new System.Drawing.Point(5, 477);
            btn_GetSpotifyCover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GetSpotifyCover.Name = "btn_GetSpotifyCover";
            btn_GetSpotifyCover.Size = new System.Drawing.Size(248, 51);
            btn_GetSpotifyCover.TabIndex = 387;
            btn_GetSpotifyCover.Text = "Get Spotify Cover";
            btn_GetSpotifyCover.UseVisualStyleBackColor = true;
            btn_GetSpotifyCover.Click += btn_GetSpotifyCover_Click;
            // 
            // chbx_Default_Cover
            // 
            chbx_Default_Cover.AutoSize = true;
            chbx_Default_Cover.Location = new System.Drawing.Point(124, 947);
            chbx_Default_Cover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_Default_Cover.Name = "chbx_Default_Cover";
            chbx_Default_Cover.Size = new System.Drawing.Size(196, 36);
            chbx_Default_Cover.TabIndex = 390;
            chbx_Default_Cover.Text = "Default_Cover";
            chbx_Default_Cover.UseVisualStyleBackColor = true;
            // 
            // txt_Comments
            // 
            txt_Comments.Location = new System.Drawing.Point(261, 477);
            txt_Comments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Comments.Name = "txt_Comments";
            txt_Comments.Size = new System.Drawing.Size(332, 136);
            txt_Comments.TabIndex = 392;
            txt_Comments.Text = "";
            // 
            // pB_ReadDLCs
            // 
            pB_ReadDLCs.Location = new System.Drawing.Point(11, 427);
            pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pB_ReadDLCs.Maximum = 20000;
            pB_ReadDLCs.Name = "pB_ReadDLCs";
            pB_ReadDLCs.Size = new System.Drawing.Size(588, 51);
            pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.TabIndex = 393;
            pB_ReadDLCs.Click += PB_ReadDLCs_Click;
            // 
            // pxbx_SavedSpotify
            // 
            pxbx_SavedSpotify.Location = new System.Drawing.Point(317, 979);
            pxbx_SavedSpotify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pxbx_SavedSpotify.Name = "pxbx_SavedSpotify";
            pxbx_SavedSpotify.Size = new System.Drawing.Size(280, 280);
            pxbx_SavedSpotify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pxbx_SavedSpotify.TabIndex = 394;
            pxbx_SavedSpotify.TabStop = false;
            pxbx_SavedSpotify.Click += Pxbx_SavedSpotify_Click;
            // 
            // lbl_corrected
            // 
            lbl_corrected.Location = new System.Drawing.Point(0, 947);
            lbl_corrected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_corrected.Name = "lbl_corrected";
            lbl_corrected.Size = new System.Drawing.Size(116, 32);
            lbl_corrected.TabIndex = 395;
            lbl_corrected.Text = "Corrected:";
            // 
            // lbl_SpotifyCover
            // 
            lbl_SpotifyCover.Location = new System.Drawing.Point(320, 947);
            lbl_SpotifyCover.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbl_SpotifyCover.Name = "lbl_SpotifyCover";
            lbl_SpotifyCover.Size = new System.Drawing.Size(197, 32);
            lbl_SpotifyCover.TabIndex = 396;
            lbl_SpotifyCover.Text = "Saved from Spotify:";
            // 
            // btn_CorrectWithSpotify
            // 
            btn_CorrectWithSpotify.Location = new System.Drawing.Point(5, 528);
            btn_CorrectWithSpotify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_CorrectWithSpotify.Name = "btn_CorrectWithSpotify";
            btn_CorrectWithSpotify.Size = new System.Drawing.Size(248, 51);
            btn_CorrectWithSpotify.TabIndex = 397;
            btn_CorrectWithSpotify.Text = "Correct with Spotify Cover";
            btn_CorrectWithSpotify.UseVisualStyleBackColor = true;
            btn_CorrectWithSpotify.Click += Btn_CorrectWithSpotify_Click;
            // 
            // btn_DeleteAll
            // 
            btn_DeleteAll.Location = new System.Drawing.Point(435, 111);
            btn_DeleteAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_DeleteAll.Name = "btn_DeleteAll";
            btn_DeleteAll.Size = new System.Drawing.Size(164, 51);
            btn_DeleteAll.TabIndex = 398;
            btn_DeleteAll.Text = "Delete All";
            btn_DeleteAll.UseVisualStyleBackColor = true;
            btn_DeleteAll.Visible = false;
            btn_DeleteAll.Click += btn_DeleteAll_Click;
            // 
            // btn_GetSpotifyAll
            // 
            btn_GetSpotifyAll.Location = new System.Drawing.Point(5, 581);
            btn_GetSpotifyAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_GetSpotifyAll.Name = "btn_GetSpotifyAll";
            btn_GetSpotifyAll.Size = new System.Drawing.Size(252, 56);
            btn_GetSpotifyAll.TabIndex = 399;
            btn_GetSpotifyAll.Text = "Get Spotify info for All";
            btn_GetSpotifyAll.UseVisualStyleBackColor = true;
            btn_GetSpotifyAll.Click += btn_GetSpotifyAll_Click;
            // 
            // btn_CheckOnline
            // 
            btn_CheckOnline.Location = new System.Drawing.Point(261, 692);
            btn_CheckOnline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_CheckOnline.Name = "btn_CheckOnline";
            btn_CheckOnline.Size = new System.Drawing.Size(184, 83);
            btn_CheckOnline.TabIndex = 400;
            btn_CheckOnline.Text = "Search and Check on GGL";
            btn_CheckOnline.UseVisualStyleBackColor = true;
            btn_CheckOnline.Click += btn_CheckOnline_Click;
            // 
            // cbx_Groups
            // 
            cbx_Groups.FormattingEnabled = true;
            cbx_Groups.Location = new System.Drawing.Point(347, 629);
            cbx_Groups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbx_Groups.Name = "cbx_Groups";
            cbx_Groups.Size = new System.Drawing.Size(248, 40);
            cbx_Groups.TabIndex = 401;
            cbx_Groups.DropDown += cbx_Groups_DropDown;
            cbx_Groups.SelectedIndexChanged += cbx_Groups_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(256, 629);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(91, 84);
            label3.TabIndex = 402;
            label3.Text = "Auto Group:";
            // 
            // btn_ApplyCurrent
            // 
            btn_ApplyCurrent.Location = new System.Drawing.Point(5, 644);
            btn_ApplyCurrent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_ApplyCurrent.Name = "btn_ApplyCurrent";
            btn_ApplyCurrent.Size = new System.Drawing.Size(252, 51);
            btn_ApplyCurrent.TabIndex = 403;
            btn_ApplyCurrent.Text = "Apply current standard.";
            btn_ApplyCurrent.UseVisualStyleBackColor = true;
            btn_ApplyCurrent.Click += btn_ApplyCurrent_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(1);
            splitContainer1.MinimumSize = new System.Drawing.Size(1600, 1000);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(databox);
            splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitContainer1.Panel1MinSize = 1200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chbx_a5);
            splitContainer1.Panel2.Controls.Add(chbx_a4);
            splitContainer1.Panel2.Controls.Add(chbx_a3);
            splitContainer1.Panel2.Controls.Add(chbx_a2);
            splitContainer1.Panel2.Controls.Add(chbx_a1);
            splitContainer1.Panel2.Controls.Add(btn_StandCover);
            splitContainer1.Panel2.Controls.Add(btn_suspect);
            splitContainer1.Panel2.Controls.Add(btn_GoTo);
            splitContainer1.Panel2.Controls.Add(btn_SearchReset);
            splitContainer1.Panel2.Controls.Add(btn_Search);
            splitContainer1.Panel2.Controls.Add(btn_RemoveDuplicates);
            splitContainer1.Panel2.Controls.Add(button2);
            splitContainer1.Panel2.Controls.Add(btn_MultiplyAutoGroup);
            splitContainer1.Panel2.Controls.Add(btn_ApplyDefault);
            splitContainer1.Panel2.Controls.Add(bbtn_ApplyYear);
            splitContainer1.Panel2.Controls.Add(btn_CheckOnline);
            splitContainer1.Panel2.Controls.Add(btn_ApplyCurrent);
            splitContainer1.Panel2.Controls.Add(txt_Album);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(btn_OpenAccess);
            splitContainer1.Panel2.Controls.Add(cbx_Groups);
            splitContainer1.Panel2.Controls.Add(txt_ID);
            splitContainer1.Panel2.Controls.Add(btn_Save);
            splitContainer1.Panel2.Controls.Add(btn_GetSpotifyAll);
            splitContainer1.Panel2.Controls.Add(picbx_AlbumArtPath);
            splitContainer1.Panel2.Controls.Add(btn_DeleteAll);
            splitContainer1.Panel2.Controls.Add(chbx_Save_All);
            splitContainer1.Panel2.Controls.Add(btn_CorrectWithSpotify);
            splitContainer1.Panel2.Controls.Add(btn_ChangeCover);
            splitContainer1.Panel2.Controls.Add(lbl_SpotifyCover);
            splitContainer1.Panel2.Controls.Add(txt_AlbumArtPath);
            splitContainer1.Panel2.Controls.Add(lbl_corrected);
            splitContainer1.Panel2.Controls.Add(txt_Artist);
            splitContainer1.Panel2.Controls.Add(pxbx_SavedSpotify);
            splitContainer1.Panel2.Controls.Add(txt_Artist_Correction);
            splitContainer1.Panel2.Controls.Add(pB_ReadDLCs);
            splitContainer1.Panel2.Controls.Add(txt_Album_Correction);
            splitContainer1.Panel2.Controls.Add(txt_Comments);
            splitContainer1.Panel2.Controls.Add(txt_AlbumArt_Correction);
            splitContainer1.Panel2.Controls.Add(txt_Album_Short);
            splitContainer1.Panel2.Controls.Add(btn_Close);
            splitContainer1.Panel2.Controls.Add(chbx_Default_Cover);
            splitContainer1.Panel2.Controls.Add(chbx_Include_ArtistSort);
            splitContainer1.Panel2.Controls.Add(txt_Year_Correction);
            splitContainer1.Panel2.Controls.Add(btn_Apply);
            splitContainer1.Panel2.Controls.Add(btn_GetSpotifyCover);
            splitContainer1.Panel2.Controls.Add(btn_DecompressAll);
            splitContainer1.Panel2.Controls.Add(txt_Artist_Short);
            splitContainer1.Panel2.Controls.Add(btn_CopyArtist2ArtistSort);
            splitContainer1.Panel2.Controls.Add(btn_Delete);
            splitContainer1.Panel2.Controls.Add(btn_CopyTitle2TitleSort);
            splitContainer1.Panel2.Controls.Add(chbx_AutoSave);
            splitContainer1.Panel2.Controls.Add(lbl_NoRec);
            splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitContainer1.Panel2MinSize = 600;
            splitContainer1.Size = new System.Drawing.Size(1821, 1265);
            splitContainer1.SplitterDistance = 1200;
            splitContainer1.TabIndex = 404;
            splitContainer1.TabStop = false;
            // 
            // chbx_a5
            // 
            chbx_a5.AutoSize = true;
            chbx_a5.Enabled = false;
            chbx_a5.Location = new System.Drawing.Point(11, 166);
            chbx_a5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_a5.Name = "chbx_a5";
            chbx_a5.Size = new System.Drawing.Size(74, 36);
            chbx_a5.TabIndex = 441;
            chbx_a5.Text = "A5";
            chbx_a5.UseVisualStyleBackColor = true;
            // 
            // chbx_a4
            // 
            chbx_a4.AutoSize = true;
            chbx_a4.Enabled = false;
            chbx_a4.Location = new System.Drawing.Point(11, 135);
            chbx_a4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_a4.Name = "chbx_a4";
            chbx_a4.Size = new System.Drawing.Size(74, 36);
            chbx_a4.TabIndex = 440;
            chbx_a4.Text = "A4";
            chbx_a4.UseVisualStyleBackColor = true;
            // 
            // chbx_a3
            // 
            chbx_a3.AutoSize = true;
            chbx_a3.Enabled = false;
            chbx_a3.Location = new System.Drawing.Point(11, 105);
            chbx_a3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_a3.Name = "chbx_a3";
            chbx_a3.Size = new System.Drawing.Size(74, 36);
            chbx_a3.TabIndex = 439;
            chbx_a3.Text = "A3";
            chbx_a3.UseVisualStyleBackColor = true;
            // 
            // chbx_a2
            // 
            chbx_a2.AutoSize = true;
            chbx_a2.Enabled = false;
            chbx_a2.Location = new System.Drawing.Point(11, 75);
            chbx_a2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_a2.Name = "chbx_a2";
            chbx_a2.Size = new System.Drawing.Size(74, 36);
            chbx_a2.TabIndex = 438;
            chbx_a2.Text = "A2";
            chbx_a2.UseVisualStyleBackColor = true;
            // 
            // chbx_a1
            // 
            chbx_a1.AutoSize = true;
            chbx_a1.Enabled = false;
            chbx_a1.Location = new System.Drawing.Point(11, 45);
            chbx_a1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            chbx_a1.Name = "chbx_a1";
            chbx_a1.Size = new System.Drawing.Size(74, 36);
            chbx_a1.TabIndex = 437;
            chbx_a1.Text = "A1";
            toolTip1.SetToolTip(chbx_a1, "If TRUE will add the Attribute to the Album");
            chbx_a1.UseVisualStyleBackColor = true;
            // 
            // btn_StandCover
            // 
            btn_StandCover.Location = new System.Drawing.Point(94, 12);
            btn_StandCover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_StandCover.Name = "btn_StandCover";
            btn_StandCover.Size = new System.Drawing.Size(195, 35);
            btn_StandCover.TabIndex = 436;
            btn_StandCover.Text = "Standard Cover";
            toolTip1.SetToolTip(btn_StandCover, "Mark as suspect any similar Artist (trimemd, lovercassed comparison, subset of the other name)(smilar album with same artist)");
            btn_StandCover.UseVisualStyleBackColor = true;
            btn_StandCover.Click += btn_StandCover_Click;
            // 
            // btn_suspect
            // 
            btn_suspect.Location = new System.Drawing.Point(435, 158);
            btn_suspect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_suspect.Name = "btn_suspect";
            btn_suspect.Size = new System.Drawing.Size(164, 51);
            btn_suspect.TabIndex = 435;
            btn_suspect.Text = "Suspect";
            toolTip1.SetToolTip(btn_suspect, "Mark as suspect any similar Artist (trimemd, lovercassed comparison, subset of the other name)(smilar album with same artist)");
            btn_suspect.UseVisualStyleBackColor = true;
            btn_suspect.Click += btn_suspect_Click;
            // 
            // btn_GoTo
            // 
            btn_GoTo.Enabled = false;
            btn_GoTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_GoTo.Location = new System.Drawing.Point(379, 885);
            btn_GoTo.Margin = new System.Windows.Forms.Padding(4);
            btn_GoTo.Name = "btn_GoTo";
            btn_GoTo.Size = new System.Drawing.Size(100, 53);
            btn_GoTo.TabIndex = 434;
            btn_GoTo.Text = "Go To";
            btn_GoTo.UseVisualStyleBackColor = true;
            btn_GoTo.Click += btn_GoTo_Click;
            // 
            // btn_SearchReset
            // 
            btn_SearchReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_SearchReset.Location = new System.Drawing.Point(487, 885);
            btn_SearchReset.Margin = new System.Windows.Forms.Padding(4);
            btn_SearchReset.Name = "btn_SearchReset";
            btn_SearchReset.Size = new System.Drawing.Size(100, 53);
            btn_SearchReset.TabIndex = 433;
            btn_SearchReset.Text = "Reset";
            btn_SearchReset.UseVisualStyleBackColor = true;
            btn_SearchReset.Click += btn_SearchReset_Click;
            // 
            // btn_Search
            // 
            btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_Search.Location = new System.Drawing.Point(271, 885);
            btn_Search.Margin = new System.Windows.Forms.Padding(4);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new System.Drawing.Size(100, 53);
            btn_Search.TabIndex = 432;
            btn_Search.Text = "Search";
            btn_Search.UseVisualStyleBackColor = true;
            btn_Search.Click += btn_Search_Click;
            // 
            // btn_RemoveDuplicates
            // 
            btn_RemoveDuplicates.Location = new System.Drawing.Point(11, 885);
            btn_RemoveDuplicates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_RemoveDuplicates.Name = "btn_RemoveDuplicates";
            btn_RemoveDuplicates.Size = new System.Drawing.Size(252, 53);
            btn_RemoveDuplicates.TabIndex = 408;
            btn_RemoveDuplicates.Text = "Remove duplicates";
            btn_RemoveDuplicates.UseVisualStyleBackColor = true;
            btn_RemoveDuplicates.Click += btn_RemoveDuplicates_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(271, 837);
            button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(208, 51);
            button2.TabIndex = 407;
            button2.Text = "Apply 'n Multiply Spotify";
            button2.UseVisualStyleBackColor = true;
            button2.Click += MultiplyAndApplySpotify;
            // 
            // btn_MultiplyAutoGroup
            // 
            btn_MultiplyAutoGroup.Location = new System.Drawing.Point(11, 835);
            btn_MultiplyAutoGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_MultiplyAutoGroup.Name = "btn_MultiplyAutoGroup";
            btn_MultiplyAutoGroup.Size = new System.Drawing.Size(252, 53);
            btn_MultiplyAutoGroup.TabIndex = 406;
            btn_MultiplyAutoGroup.Text = "Apply 'n Multiply Default";
            btn_MultiplyAutoGroup.UseVisualStyleBackColor = true;
            btn_MultiplyAutoGroup.Click += button1_Click;
            // 
            // btn_ApplyDefault
            // 
            btn_ApplyDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btn_ApplyDefault.Location = new System.Drawing.Point(508, 947);
            btn_ApplyDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btn_ApplyDefault.Name = "btn_ApplyDefault";
            btn_ApplyDefault.Size = new System.Drawing.Size(85, 35);
            btn_ApplyDefault.TabIndex = 405;
            btn_ApplyDefault.Text = "Apply Default";
            btn_ApplyDefault.UseVisualStyleBackColor = true;
            btn_ApplyDefault.Click += Btn_ApplyDefault_Click;
            // 
            // bbtn_ApplyYear
            // 
            bbtn_ApplyYear.Location = new System.Drawing.Point(5, 696);
            bbtn_ApplyYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            bbtn_ApplyYear.Name = "bbtn_ApplyYear";
            bbtn_ApplyYear.Size = new System.Drawing.Size(252, 51);
            bbtn_ApplyYear.TabIndex = 404;
            bbtn_ApplyYear.Text = "Apply 'n Multiply Year Corr";
            bbtn_ApplyYear.UseVisualStyleBackColor = true;
            bbtn_ApplyYear.Click += Bbtn_ApplyYear_Click;
            // 
            // txt_Album
            // 
            txt_Album.Cue = "Album";
            txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Album.ForeColor = System.Drawing.Color.Gray;
            txt_Album.Location = new System.Drawing.Point(188, 278);
            txt_Album.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Album.Name = "txt_Album";
            txt_Album.ReadOnly = true;
            txt_Album.Size = new System.Drawing.Size(404, 32);
            txt_Album.TabIndex = 126;
            // 
            // txt_ID
            // 
            txt_ID.Cue = "ID";
            txt_ID.Enabled = false;
            txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_ID.ForeColor = System.Drawing.Color.Gray;
            txt_ID.Location = new System.Drawing.Point(11, 13);
            txt_ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_ID.Name = "txt_ID";
            txt_ID.Size = new System.Drawing.Size(81, 32);
            txt_ID.TabIndex = 128;
            // 
            // txt_AlbumArtPath
            // 
            txt_AlbumArtPath.Cue = "Album art Path";
            txt_AlbumArtPath.Enabled = false;
            txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumArtPath.Location = new System.Drawing.Point(189, 345);
            txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            txt_AlbumArtPath.Size = new System.Drawing.Size(404, 32);
            txt_AlbumArtPath.TabIndex = 130;
            txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Artist
            // 
            txt_Artist.Cue = "Artist";
            txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Artist.ForeColor = System.Drawing.Color.Gray;
            txt_Artist.Location = new System.Drawing.Point(188, 212);
            txt_Artist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Artist.Name = "txt_Artist";
            txt_Artist.ReadOnly = true;
            txt_Artist.Size = new System.Drawing.Size(404, 32);
            txt_Artist.TabIndex = 131;
            // 
            // txt_Artist_Correction
            // 
            txt_Artist_Correction.Cue = "Artist  Correction";
            txt_Artist_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Artist_Correction.ForeColor = System.Drawing.Color.Gray;
            txt_Artist_Correction.Location = new System.Drawing.Point(148, 245);
            txt_Artist_Correction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Artist_Correction.Name = "txt_Artist_Correction";
            txt_Artist_Correction.Size = new System.Drawing.Size(444, 32);
            txt_Artist_Correction.TabIndex = 132;
            txt_Artist_Correction.KeyPress += txt_Artist_Correction_KeyPress;
            // 
            // txt_Album_Correction
            // 
            txt_Album_Correction.Cue = "Album Correction";
            txt_Album_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Album_Correction.ForeColor = System.Drawing.Color.Gray;
            txt_Album_Correction.Location = new System.Drawing.Point(148, 311);
            txt_Album_Correction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Album_Correction.Name = "txt_Album_Correction";
            txt_Album_Correction.Size = new System.Drawing.Size(444, 32);
            txt_Album_Correction.TabIndex = 133;
            txt_Album_Correction.KeyPress += txt_Album_Correction_KeyPress;
            // 
            // txt_AlbumArt_Correction
            // 
            txt_AlbumArt_Correction.Cue = "Album art Path Correction";
            txt_AlbumArt_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_AlbumArt_Correction.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumArt_Correction.Location = new System.Drawing.Point(11, 381);
            txt_AlbumArt_Correction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_AlbumArt_Correction.Name = "txt_AlbumArt_Correction";
            txt_AlbumArt_Correction.Size = new System.Drawing.Size(584, 32);
            txt_AlbumArt_Correction.TabIndex = 134;
            txt_AlbumArt_Correction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Album_Short
            // 
            txt_Album_Short.Cue = "ShortName";
            txt_Album_Short.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Album_Short.ForeColor = System.Drawing.Color.Gray;
            txt_Album_Short.Location = new System.Drawing.Point(10, 278);
            txt_Album_Short.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Album_Short.Name = "txt_Album_Short";
            txt_Album_Short.Size = new System.Drawing.Size(160, 32);
            txt_Album_Short.TabIndex = 391;
            txt_Album_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Year_Correction
            // 
            txt_Year_Correction.Cue = "Year Correction";
            txt_Year_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Year_Correction.ForeColor = System.Drawing.Color.Gray;
            txt_Year_Correction.Location = new System.Drawing.Point(11, 345);
            txt_Year_Correction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Year_Correction.Name = "txt_Year_Correction";
            txt_Year_Correction.Size = new System.Drawing.Size(160, 32);
            txt_Year_Correction.TabIndex = 389;
            // 
            // txt_Artist_Short
            // 
            txt_Artist_Short.Cue = "ShortName";
            txt_Artist_Short.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txt_Artist_Short.ForeColor = System.Drawing.Color.Gray;
            txt_Artist_Short.Location = new System.Drawing.Point(10, 212);
            txt_Artist_Short.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txt_Artist_Short.Name = "txt_Artist_Short";
            txt_Artist_Short.Size = new System.Drawing.Size(160, 32);
            txt_Artist_Short.TabIndex = 386;
            txt_Artist_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Standardization
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1821, 1265);
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximumSize = new System.Drawing.Size(3000, 3000);
            MinimumSize = new System.Drawing.Size(1841, 1336);
            Name = "Standardization";
            Text = "Standardise/Correct Song/CDLC MetaData";
            FormClosing += btn_Save_FormClosing;
            Load += Standardization_Load;
            ((System.ComponentModel.ISupportInitialize)databox).EndInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPath).EndInit();
            ((System.ComponentModel.ISupportInitialize)pxbx_SavedSpotify).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.DataGridView databox;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_OpenAccess;
        private CueTextBox txt_AlbumArtPath;
        private System.Windows.Forms.Button btn_ChangeCover;
        private CueTextBox txt_ID;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_Album;
        private CueTextBox txt_Artist_Correction;
        private CueTextBox txt_Artist;
        private CueTextBox txt_Album_Correction;
        private CueTextBox txt_AlbumArt_Correction;
        //private DLCManager.MainDBfields eXisting;
        private DLCPackageData datas;
        private string author;
        private string tkversion;
        private string dD;
        private string bass;
        private string guitar;
        private string combo;
        private string rhythm;
        private string lead;
        private string tunnings;
        private int i;
        private int norows;
        private string original_FileName;
        private string art_hash;
        private string audio_hash;
        private string audioPreview_hash;
        private List<string> alist;
        private List<string> blist;
        //private OleDbConnection cnnb;

        // internal Standardization(string txt_DBFolder, MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb, SQLiteConnection cnnz)
        internal Standardization(string txt_DBFolder, UtilitiesFunctions.MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb, SQLite.SQLiteConnection cnnc)
           : this(txt_DBFolder, txt_TempPath, txt_RocksmithDLCPath, AllowEncript, AllowORIGDelete, cnnb, null, cnnc)
        {
            this.filed = filed;
            datas = datas;
            this.author = author;
            this.tkversion = tkversion;
            this.dD = dD;
            this.bass = bass;
            this.guitar = guitar;
            this.combo = combo;
            this.rhythm = rhythm;
            this.lead = lead;
            this.tunnings = tunnings;
            this.i = i;
            this.norows = norows;
            this.original_FileName = original_FileName;
            this.art_hash = art_hash;
            this.audio_hash = audio_hash;
            this.audioPreview_hash = audioPreview_hash;
            this.alist = alist;
            this.blist = blist;
            //this.cnb = cnnb;
            //this.cnc = cnnc;
        }

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.CheckBox chbx_Include_ArtistSort;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Button btn_DecompressAll;
        private System.Windows.Forms.Button btn_CopyArtist2ArtistSort;
        private System.Windows.Forms.Button btn_CopyTitle2TitleSort;
        private System.Windows.Forms.Label lbl_NoRec;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
        private System.Windows.Forms.Button btn_Delete;
        private CueTextBox txt_Artist_Short;
        private System.Windows.Forms.Button btn_GetSpotifyCover;
        private CueTextBox txt_Year_Correction;
        private System.Windows.Forms.CheckBox chbx_Default_Cover;
        private CueTextBox txt_Album_Short;
        private System.Windows.Forms.RichTextBox txt_Comments;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private UtilitiesFunctions.MainDBfields filed;
        private System.Windows.Forms.PictureBox pxbx_SavedSpotify;
        private System.Windows.Forms.Label lbl_corrected;
        private System.Windows.Forms.Label lbl_SpotifyCover;
        private System.Windows.Forms.Button btn_CorrectWithSpotify;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.Button btn_GetSpotifyAll;
        private System.Windows.Forms.Button btn_CheckOnline;
        private System.Windows.Forms.ComboBox cbx_Groups;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_ApplyCurrent;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bbtn_ApplyYear;
        private System.Windows.Forms.Button btn_ApplyDefault;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_MultiplyAutoGroup;
        private System.Windows.Forms.Button btn_RemoveDuplicates;
        private System.Windows.Forms.Button btn_GoTo;
        private System.Windows.Forms.Button btn_SearchReset;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_suspect;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btn_StandCover;
        private System.Windows.Forms.CheckBox chbx_a5;
        private System.Windows.Forms.CheckBox chbx_a4;
        private System.Windows.Forms.CheckBox chbx_a3;
        private System.Windows.Forms.CheckBox chbx_a2;
        private System.Windows.Forms.CheckBox chbx_a1;
    }
}