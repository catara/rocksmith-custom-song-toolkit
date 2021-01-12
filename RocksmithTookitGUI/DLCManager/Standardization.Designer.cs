﻿using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using System.Data.OleDb;

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
            this.databox = new System.Windows.Forms.DataGridView();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.chbx_Save_All = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.btn_OpenAccess = new System.Windows.Forms.Button();
            this.txt_AlbumArt_Correction = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_Correction = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist_Correction = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AlbumArtPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album = new RocksmithToolkitGUI.CueTextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.chbx_Include_ArtistSort = new System.Windows.Forms.CheckBox();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.btn_DecompressAll = new System.Windows.Forms.Button();
            this.btn_CopyArtist2ArtistSort = new System.Windows.Forms.Button();
            this.btn_CopyTitle2TitleSort = new System.Windows.Forms.Button();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.txt_Artist_Short = new RocksmithToolkitGUI.CueTextBox();
            this.btn_GetSpotifyCover = new System.Windows.Forms.Button();
            this.txt_Year_Correction = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Default_Cover = new System.Windows.Forms.CheckBox();
            this.txt_Album_Short = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Comments = new System.Windows.Forms.RichTextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.pxbx_SavedSpotify = new System.Windows.Forms.PictureBox();
            this.lbl_corrected = new System.Windows.Forms.Label();
            this.lbl_SpotifyCover = new System.Windows.Forms.Label();
            this.btn_CorrectWithSpotify = new System.Windows.Forms.Button();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            this.btn_GetSpotifyAll = new System.Windows.Forms.Button();
            this.btn_CheckOnline = new System.Windows.Forms.Button();
            this.cbx_Groups = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_ApplyCurrent = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_ApplyDefault = new System.Windows.Forms.Button();
            this.bbtn_ApplyYear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.databox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbx_SavedSpotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // databox
            // 
            this.databox.AllowUserToAddRows = false;
            this.databox.AllowUserToDeleteRows = false;
            this.databox.AllowUserToOrderColumns = true;
            this.databox.AllowUserToResizeRows = false;
            this.databox.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.databox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.databox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databox.Location = new System.Drawing.Point(0, 0);
            this.databox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.databox.MultiSelect = false;
            this.databox.Name = "databox";
            this.databox.RowHeadersWidth = 61;
            this.databox.Size = new System.Drawing.Size(676, 583);
            this.databox.TabIndex = 38;
            this.databox.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.databox.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.databox.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.databox_RowLeave);
            this.databox.SelectionChanged += new System.EventHandler(this.databox_SelectionChanged);
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Location = new System.Drawing.Point(214, 390);
            this.btn_ChangeCover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(83, 25);
            this.btn_ChangeCover.TabIndex = 129;
            this.btn_ChangeCover.Text = "Change cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            this.btn_ChangeCover.Click += new System.EventHandler(this.btn_ChangeCover_Click);
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(5, 437);
            this.picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(140, 140);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Save_All
            // 
            this.chbx_Save_All.AutoSize = true;
            this.chbx_Save_All.Enabled = false;
            this.chbx_Save_All.Location = new System.Drawing.Point(5, 60);
            this.chbx_Save_All.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Save_All.Name = "chbx_Save_All";
            this.chbx_Save_All.Size = new System.Drawing.Size(37, 17);
            this.chbx_Save_All.TabIndex = 124;
            this.chbx_Save_All.Text = "All";
            this.chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(217, 7);
            this.button8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(82, 25);
            this.button8.TabIndex = 123;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // btn_OpenAccess
            // 
            this.btn_OpenAccess.Location = new System.Drawing.Point(138, 37);
            this.btn_OpenAccess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OpenAccess.Name = "btn_OpenAccess";
            this.btn_OpenAccess.Size = new System.Drawing.Size(78, 50);
            this.btn_OpenAccess.TabIndex = 122;
            this.btn_OpenAccess.Text = "Open DB in M$ Access";
            this.btn_OpenAccess.UseVisualStyleBackColor = true;
            this.btn_OpenAccess.Click += new System.EventHandler(this.btn_OpenAccess_Click);
            // 
            // txt_AlbumArt_Correction
            // 
            this.txt_AlbumArt_Correction.Cue = "Album art Path Correction";
            this.txt_AlbumArt_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArt_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArt_Correction.Location = new System.Drawing.Point(5, 191);
            this.txt_AlbumArt_Correction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_AlbumArt_Correction.Name = "txt_AlbumArt_Correction";
            this.txt_AlbumArt_Correction.Size = new System.Drawing.Size(294, 20);
            this.txt_AlbumArt_Correction.TabIndex = 134;
            this.txt_AlbumArt_Correction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Album_Correction
            // 
            this.txt_Album_Correction.Cue = "Album Correction";
            this.txt_Album_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Correction.Location = new System.Drawing.Point(75, 152);
            this.txt_Album_Correction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album_Correction.Name = "txt_Album_Correction";
            this.txt_Album_Correction.Size = new System.Drawing.Size(224, 20);
            this.txt_Album_Correction.TabIndex = 133;
            // 
            // txt_Artist_Correction
            // 
            this.txt_Artist_Correction.Cue = "Artist  Correction";
            this.txt_Artist_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Correction.Location = new System.Drawing.Point(75, 112);
            this.txt_Artist_Correction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist_Correction.Name = "txt_Artist_Correction";
            this.txt_Artist_Correction.Size = new System.Drawing.Size(224, 20);
            this.txt_Artist_Correction.TabIndex = 132;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(95, 92);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.ReadOnly = true;
            this.txt_Artist.Size = new System.Drawing.Size(204, 20);
            this.txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Enabled = false;
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(95, 171);
            this.txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(204, 20);
            this.txt_AlbumArtPath.TabIndex = 130;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(5, 7);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(46, 20);
            this.txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(95, 132);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.ReadOnly = true;
            this.txt_Album.Size = new System.Drawing.Size(204, 20);
            this.txt_Album.TabIndex = 126;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(227, 336);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 23);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // chbx_Include_ArtistSort
            // 
            this.chbx_Include_ArtistSort.AutoSize = true;
            this.chbx_Include_ArtistSort.Checked = true;
            this.chbx_Include_ArtistSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Include_ArtistSort.Enabled = false;
            this.chbx_Include_ArtistSort.Location = new System.Drawing.Point(131, 395);
            this.chbx_Include_ArtistSort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Include_ArtistSort.Name = "chbx_Include_ArtistSort";
            this.chbx_Include_ArtistSort.Size = new System.Drawing.Size(89, 17);
            this.chbx_Include_ArtistSort.TabIndex = 274;
            this.chbx_Include_ArtistSort.Text = "Incl_Artistsort";
            this.chbx_Include_ArtistSort.UseVisualStyleBackColor = true;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(3, 375);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(126, 40);
            this.btn_Apply.TabIndex = 275;
            this.btn_Apply.Text = "Apply changes to the Main DB";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.Standardization_Click);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(226, 356);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(72, 33);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // btn_CopyArtist2ArtistSort
            // 
            this.btn_CopyArtist2ArtistSort.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btn_CopyArtist2ArtistSort.Location = new System.Drawing.Point(50, 37);
            this.btn_CopyArtist2ArtistSort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CopyArtist2ArtistSort.Name = "btn_CopyArtist2ArtistSort";
            this.btn_CopyArtist2ArtistSort.Size = new System.Drawing.Size(84, 25);
            this.btn_CopyArtist2ArtistSort.TabIndex = 277;
            this.btn_CopyArtist2ArtistSort.Text = "Artist->ArtistSort";
            this.btn_CopyArtist2ArtistSort.UseVisualStyleBackColor = true;
            this.btn_CopyArtist2ArtistSort.Click += new System.EventHandler(this.btn_CopyArtist2ArtistSort_Click);
            // 
            // btn_CopyTitle2TitleSort
            // 
            this.btn_CopyTitle2TitleSort.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btn_CopyTitle2TitleSort.Location = new System.Drawing.Point(50, 62);
            this.btn_CopyTitle2TitleSort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CopyTitle2TitleSort.Name = "btn_CopyTitle2TitleSort";
            this.btn_CopyTitle2TitleSort.Size = new System.Drawing.Size(84, 25);
            this.btn_CopyTitle2TitleSort.TabIndex = 278;
            this.btn_CopyTitle2TitleSort.Text = "Title->TitleSort";
            this.btn_CopyTitle2TitleSort.UseVisualStyleBackColor = true;
            this.btn_CopyTitle2TitleSort.Click += new System.EventHandler(this.btn_CopyTitle2TitleSort_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Location = new System.Drawing.Point(91, 8);
            this.lbl_NoRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(50, 28);
            this.lbl_NoRec.TabIndex = 279;
            this.lbl_NoRec.Text = " Records";
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(145, 7);
            this.chbx_AutoSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(73, 17);
            this.chbx_AutoSave.TabIndex = 384;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            this.chbx_AutoSave.CheckedChanged += new System.EventHandler(this.chbx_AutoSave_CheckedChanged);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(217, 37);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(82, 25);
            this.btn_Delete.TabIndex = 385;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // txt_Artist_Short
            // 
            this.txt_Artist_Short.Cue = "ShortName";
            this.txt_Artist_Short.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Short.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Short.Location = new System.Drawing.Point(5, 92);
            this.txt_Artist_Short.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist_Short.Name = "txt_Artist_Short";
            this.txt_Artist_Short.Size = new System.Drawing.Size(82, 20);
            this.txt_Artist_Short.TabIndex = 386;
            this.txt_Artist_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_GetSpotifyCover
            // 
            this.btn_GetSpotifyCover.Location = new System.Drawing.Point(3, 239);
            this.btn_GetSpotifyCover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_GetSpotifyCover.Name = "btn_GetSpotifyCover";
            this.btn_GetSpotifyCover.Size = new System.Drawing.Size(124, 25);
            this.btn_GetSpotifyCover.TabIndex = 387;
            this.btn_GetSpotifyCover.Text = "Get Spotify Cover";
            this.btn_GetSpotifyCover.UseVisualStyleBackColor = true;
            // 
            // txt_Year_Correction
            // 
            this.txt_Year_Correction.Cue = "Year Correction";
            this.txt_Year_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Year_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Year_Correction.Location = new System.Drawing.Point(5, 171);
            this.txt_Year_Correction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Year_Correction.Name = "txt_Year_Correction";
            this.txt_Year_Correction.Size = new System.Drawing.Size(82, 20);
            this.txt_Year_Correction.TabIndex = 389;
            // 
            // chbx_Default_Cover
            // 
            this.chbx_Default_Cover.AutoSize = true;
            this.chbx_Default_Cover.Location = new System.Drawing.Point(62, 421);
            this.chbx_Default_Cover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Default_Cover.Name = "chbx_Default_Cover";
            this.chbx_Default_Cover.Size = new System.Drawing.Size(94, 17);
            this.chbx_Default_Cover.TabIndex = 390;
            this.chbx_Default_Cover.Text = "Default_Cover";
            this.chbx_Default_Cover.UseVisualStyleBackColor = true;
            // 
            // txt_Album_Short
            // 
            this.txt_Album_Short.Cue = "ShortName";
            this.txt_Album_Short.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Short.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Short.Location = new System.Drawing.Point(5, 132);
            this.txt_Album_Short.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album_Short.Name = "txt_Album_Short";
            this.txt_Album_Short.Size = new System.Drawing.Size(82, 20);
            this.txt_Album_Short.TabIndex = 391;
            this.txt_Album_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Comments
            // 
            this.txt_Comments.Location = new System.Drawing.Point(131, 239);
            this.txt_Comments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(168, 70);
            this.txt_Comments.TabIndex = 392;
            this.txt_Comments.Text = "";
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(5, 213);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(294, 25);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 393;
            this.pB_ReadDLCs.Click += new System.EventHandler(this.PB_ReadDLCs_Click);
            // 
            // pxbx_SavedSpotify
            // 
            this.pxbx_SavedSpotify.Location = new System.Drawing.Point(159, 437);
            this.pxbx_SavedSpotify.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pxbx_SavedSpotify.Name = "pxbx_SavedSpotify";
            this.pxbx_SavedSpotify.Size = new System.Drawing.Size(140, 140);
            this.pxbx_SavedSpotify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pxbx_SavedSpotify.TabIndex = 394;
            this.pxbx_SavedSpotify.TabStop = false;
            this.pxbx_SavedSpotify.Click += new System.EventHandler(this.Pxbx_SavedSpotify_Click);
            // 
            // lbl_corrected
            // 
            this.lbl_corrected.Location = new System.Drawing.Point(0, 421);
            this.lbl_corrected.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_corrected.Name = "lbl_corrected";
            this.lbl_corrected.Size = new System.Drawing.Size(58, 16);
            this.lbl_corrected.TabIndex = 395;
            this.lbl_corrected.Text = "Corrected:";
            // 
            // lbl_SpotifyCover
            // 
            this.lbl_SpotifyCover.Location = new System.Drawing.Point(160, 421);
            this.lbl_SpotifyCover.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_SpotifyCover.Name = "lbl_SpotifyCover";
            this.lbl_SpotifyCover.Size = new System.Drawing.Size(99, 16);
            this.lbl_SpotifyCover.TabIndex = 396;
            this.lbl_SpotifyCover.Text = "Saved from Spotify:";
            // 
            // btn_CorrectWithSpotify
            // 
            this.btn_CorrectWithSpotify.Location = new System.Drawing.Point(3, 264);
            this.btn_CorrectWithSpotify.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CorrectWithSpotify.Name = "btn_CorrectWithSpotify";
            this.btn_CorrectWithSpotify.Size = new System.Drawing.Size(124, 25);
            this.btn_CorrectWithSpotify.TabIndex = 397;
            this.btn_CorrectWithSpotify.Text = "Correct with Spotify Cover";
            this.btn_CorrectWithSpotify.UseVisualStyleBackColor = true;
            this.btn_CorrectWithSpotify.Click += new System.EventHandler(this.Btn_CorrectWithSpotify_Click);
            // 
            // btn_DeleteAll
            // 
            this.btn_DeleteAll.Location = new System.Drawing.Point(217, 62);
            this.btn_DeleteAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_DeleteAll.Name = "btn_DeleteAll";
            this.btn_DeleteAll.Size = new System.Drawing.Size(82, 25);
            this.btn_DeleteAll.TabIndex = 398;
            this.btn_DeleteAll.Text = "Delete All";
            this.btn_DeleteAll.UseVisualStyleBackColor = true;
            this.btn_DeleteAll.Visible = false;
            this.btn_DeleteAll.Click += new System.EventHandler(this.btn_DeleteAll_Click);
            // 
            // btn_GetSpotifyAll
            // 
            this.btn_GetSpotifyAll.Location = new System.Drawing.Point(3, 291);
            this.btn_GetSpotifyAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_GetSpotifyAll.Name = "btn_GetSpotifyAll";
            this.btn_GetSpotifyAll.Size = new System.Drawing.Size(126, 28);
            this.btn_GetSpotifyAll.TabIndex = 399;
            this.btn_GetSpotifyAll.Text = "Get Spotify info for All";
            this.btn_GetSpotifyAll.UseVisualStyleBackColor = true;
            this.btn_GetSpotifyAll.Click += new System.EventHandler(this.btn_GetSpotifyAll_Click);
            // 
            // btn_CheckOnline
            // 
            this.btn_CheckOnline.Location = new System.Drawing.Point(131, 346);
            this.btn_CheckOnline.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CheckOnline.Name = "btn_CheckOnline";
            this.btn_CheckOnline.Size = new System.Drawing.Size(92, 41);
            this.btn_CheckOnline.TabIndex = 400;
            this.btn_CheckOnline.Text = "Search and Check on GGL";
            this.btn_CheckOnline.UseVisualStyleBackColor = true;
            this.btn_CheckOnline.Click += new System.EventHandler(this.btn_CheckOnline_Click);
            // 
            // cbx_Groups
            // 
            this.cbx_Groups.FormattingEnabled = true;
            this.cbx_Groups.Location = new System.Drawing.Point(173, 315);
            this.cbx_Groups.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbx_Groups.Name = "cbx_Groups";
            this.cbx_Groups.Size = new System.Drawing.Size(126, 21);
            this.cbx_Groups.TabIndex = 401;
            this.cbx_Groups.DropDown += new System.EventHandler(this.cbx_Groups_DropDown);
            this.cbx_Groups.SelectedIndexChanged += new System.EventHandler(this.cbx_Groups_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(128, 315);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 42);
            this.label3.TabIndex = 402;
            this.label3.Text = "Auto Group:";
            // 
            // btn_ApplyCurrent
            // 
            this.btn_ApplyCurrent.Location = new System.Drawing.Point(3, 322);
            this.btn_ApplyCurrent.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ApplyCurrent.Name = "btn_ApplyCurrent";
            this.btn_ApplyCurrent.Size = new System.Drawing.Size(126, 25);
            this.btn_ApplyCurrent.TabIndex = 403;
            this.btn_ApplyCurrent.Text = "Apply current standard.";
            this.btn_ApplyCurrent.UseVisualStyleBackColor = true;
            this.btn_ApplyCurrent.Click += new System.EventHandler(this.btn_ApplyCurrent_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.databox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_ApplyDefault);
            this.splitContainer1.Panel2.Controls.Add(this.bbtn_ApplyYear);
            this.splitContainer1.Panel2.Controls.Add(this.btn_CheckOnline);
            this.splitContainer1.Panel2.Controls.Add(this.btn_ApplyCurrent);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Album);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btn_OpenAccess);
            this.splitContainer1.Panel2.Controls.Add(this.cbx_Groups);
            this.splitContainer1.Panel2.Controls.Add(this.txt_ID);
            this.splitContainer1.Panel2.Controls.Add(this.button8);
            this.splitContainer1.Panel2.Controls.Add(this.btn_GetSpotifyAll);
            this.splitContainer1.Panel2.Controls.Add(this.picbx_AlbumArtPath);
            this.splitContainer1.Panel2.Controls.Add(this.btn_DeleteAll);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_Save_All);
            this.splitContainer1.Panel2.Controls.Add(this.btn_CorrectWithSpotify);
            this.splitContainer1.Panel2.Controls.Add(this.btn_ChangeCover);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_SpotifyCover);
            this.splitContainer1.Panel2.Controls.Add(this.txt_AlbumArtPath);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_corrected);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Artist);
            this.splitContainer1.Panel2.Controls.Add(this.pxbx_SavedSpotify);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Artist_Correction);
            this.splitContainer1.Panel2.Controls.Add(this.pB_ReadDLCs);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Album_Correction);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Comments);
            this.splitContainer1.Panel2.Controls.Add(this.txt_AlbumArt_Correction);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Album_Short);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Close);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_Default_Cover);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_Include_ArtistSort);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Year_Correction);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Apply);
            this.splitContainer1.Panel2.Controls.Add(this.btn_GetSpotifyCover);
            this.splitContainer1.Panel2.Controls.Add(this.btn_DecompressAll);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Artist_Short);
            this.splitContainer1.Panel2.Controls.Add(this.btn_CopyArtist2ArtistSort);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Delete);
            this.splitContainer1.Panel2.Controls.Add(this.btn_CopyTitle2TitleSort);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_AutoSave);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_NoRec);
            this.splitContainer1.Size = new System.Drawing.Size(983, 583);
            this.splitContainer1.SplitterDistance = 676;
            this.splitContainer1.TabIndex = 404;
            // 
            // btn_ApplyDefault
            // 
            this.btn_ApplyDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ApplyDefault.Location = new System.Drawing.Point(254, 421);
            this.btn_ApplyDefault.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ApplyDefault.Name = "btn_ApplyDefault";
            this.btn_ApplyDefault.Size = new System.Drawing.Size(43, 17);
            this.btn_ApplyDefault.TabIndex = 405;
            this.btn_ApplyDefault.Text = "Apply Default";
            this.btn_ApplyDefault.UseVisualStyleBackColor = true;
            this.btn_ApplyDefault.Click += new System.EventHandler(this.Btn_ApplyDefault_Click);
            // 
            // bbtn_ApplyYear
            // 
            this.bbtn_ApplyYear.Location = new System.Drawing.Point(3, 348);
            this.bbtn_ApplyYear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bbtn_ApplyYear.Name = "bbtn_ApplyYear";
            this.bbtn_ApplyYear.Size = new System.Drawing.Size(126, 25);
            this.bbtn_ApplyYear.TabIndex = 404;
            this.bbtn_ApplyYear.Text = "Apply \'n Multiply Year Corr";
            this.bbtn_ApplyYear.UseVisualStyleBackColor = true;
            this.bbtn_ApplyYear.Click += new System.EventHandler(this.Bbtn_ApplyYear_Click);
            // 
            // Standardization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(983, 583);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Standardization";
            this.Text = "Standardise/Correct Song/CDLC MetaData";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.btn_Save_FormClosing);
            this.Load += new System.EventHandler(this.Standardization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.databox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbx_SavedSpotify)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView databox;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.Button button8;
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

        internal Standardization(string txt_DBFolder, MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb)
            : this(txt_DBFolder, txt_TempPath, txt_RocksmithDLCPath, AllowEncript, AllowORIGDelete, cnnb,null)
        {
            this.filed = filed;
            this.datas = datas;
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
            this.cnb = cnnb;
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
        private MainDBfields filed;
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
    }
}