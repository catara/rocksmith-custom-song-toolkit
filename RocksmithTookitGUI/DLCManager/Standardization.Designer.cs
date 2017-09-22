using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;

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
            this.btn_Delete_All = new System.Windows.Forms.DataGridView();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.chbx_Save_All = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.button2 = new System.Windows.Forms.Button();
            this.txt_Year_Correction = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Default_Cover = new System.Windows.Forms.CheckBox();
            this.txt_Album_Short = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Comments = new System.Windows.Forms.RichTextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.pxbx_SavedSpotify = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CorrectWithSpotify = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Delete_All)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbx_SavedSpotify)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Delete_All
            // 
            this.btn_Delete_All.AllowUserToAddRows = false;
            this.btn_Delete_All.AllowUserToDeleteRows = false;
            this.btn_Delete_All.AllowUserToOrderColumns = true;
            this.btn_Delete_All.AllowUserToResizeRows = false;
            this.btn_Delete_All.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.btn_Delete_All.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.btn_Delete_All.Location = new System.Drawing.Point(0, 0);
            this.btn_Delete_All.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_Delete_All.MultiSelect = false;
            this.btn_Delete_All.Name = "btn_Delete_All";
            this.btn_Delete_All.RowHeadersWidth = 61;
            this.btn_Delete_All.Size = new System.Drawing.Size(2090, 1046);
            this.btn_Delete_All.TabIndex = 38;
            this.btn_Delete_All.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.btn_Delete_All.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.btn_Delete_All.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.btn_Delete_All.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Location = new System.Drawing.Point(2100, 690);
            this.btn_ChangeCover.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(168, 50);
            this.btn_ChangeCover.TabIndex = 129;
            this.btn_ChangeCover.Text = "Change cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(2100, 438);
            this.picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(276, 215);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Save_All
            // 
            this.chbx_Save_All.AutoSize = true;
            this.chbx_Save_All.Enabled = false;
            this.chbx_Save_All.Location = new System.Drawing.Point(2112, 125);
            this.chbx_Save_All.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chbx_Save_All.Name = "chbx_Save_All";
            this.chbx_Save_All.Size = new System.Drawing.Size(68, 29);
            this.chbx_Save_All.TabIndex = 124;
            this.chbx_Save_All.Text = "All";
            this.chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(2524, 19);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(168, 50);
            this.button8.TabIndex = 123;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(2206, 15);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(156, 90);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_AlbumArt_Correction
            // 
            this.txt_AlbumArt_Correction.Cue = "Album art Path Correction";
            this.txt_AlbumArt_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArt_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArt_Correction.Location = new System.Drawing.Point(2100, 394);
            this.txt_AlbumArt_Correction.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_AlbumArt_Correction.Name = "txt_AlbumArt_Correction";
            this.txt_AlbumArt_Correction.Size = new System.Drawing.Size(576, 32);
            this.txt_AlbumArt_Correction.TabIndex = 134;
            this.txt_AlbumArt_Correction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Album_Correction
            // 
            this.txt_Album_Correction.Cue = "Album Correction";
            this.txt_Album_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Correction.Location = new System.Drawing.Point(2240, 315);
            this.txt_Album_Correction.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Album_Correction.Name = "txt_Album_Correction";
            this.txt_Album_Correction.Size = new System.Drawing.Size(440, 32);
            this.txt_Album_Correction.TabIndex = 133;
            // 
            // txt_Artist_Correction
            // 
            this.txt_Artist_Correction.Cue = "Artist  Correction";
            this.txt_Artist_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Correction.Location = new System.Drawing.Point(2240, 233);
            this.txt_Artist_Correction.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Artist_Correction.Name = "txt_Artist_Correction";
            this.txt_Artist_Correction.Size = new System.Drawing.Size(440, 32);
            this.txt_Artist_Correction.TabIndex = 132;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(2278, 187);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.ReadOnly = true;
            this.txt_Artist.Size = new System.Drawing.Size(404, 32);
            this.txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Enabled = false;
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(2100, 354);
            this.txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(576, 32);
            this.txt_AlbumArtPath.TabIndex = 130;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(2108, 19);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(88, 32);
            this.txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(2280, 277);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.ReadOnly = true;
            this.txt_Album.Size = new System.Drawing.Size(400, 32);
            this.txt_Album.TabIndex = 126;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(2548, 998);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(144, 52);
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
            this.chbx_Include_ArtistSort.Location = new System.Drawing.Point(2342, 1013);
            this.chbx_Include_ArtistSort.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chbx_Include_ArtistSort.Name = "chbx_Include_ArtistSort";
            this.chbx_Include_ArtistSort.Size = new System.Drawing.Size(174, 29);
            this.chbx_Include_ArtistSort.TabIndex = 274;
            this.chbx_Include_ArtistSort.Text = "Incl_Artistsort";
            this.chbx_Include_ArtistSort.UseVisualStyleBackColor = true;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(2128, 894);
            this.btn_Apply.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(220, 90);
            this.btn_Apply.TabIndex = 275;
            this.btn_Apply.Text = "Apply changes to the Main DB";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(2548, 912);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(148, 85);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // btn_CopyArtist2ArtistSort
            // 
            this.btn_CopyArtist2ArtistSort.ForeColor = System.Drawing.Color.Green;
            this.btn_CopyArtist2ArtistSort.Location = new System.Drawing.Point(2360, 944);
            this.btn_CopyArtist2ArtistSort.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_CopyArtist2ArtistSort.Name = "btn_CopyArtist2ArtistSort";
            this.btn_CopyArtist2ArtistSort.Size = new System.Drawing.Size(168, 50);
            this.btn_CopyArtist2ArtistSort.TabIndex = 277;
            this.btn_CopyArtist2ArtistSort.Text = "Artist->ArtistSort";
            this.btn_CopyArtist2ArtistSort.UseVisualStyleBackColor = true;
            this.btn_CopyArtist2ArtistSort.Click += new System.EventHandler(this.btn_CopyArtist2ArtistSort_Click);
            // 
            // btn_CopyTitle2TitleSort
            // 
            this.btn_CopyTitle2TitleSort.ForeColor = System.Drawing.Color.Green;
            this.btn_CopyTitle2TitleSort.Location = new System.Drawing.Point(2360, 885);
            this.btn_CopyTitle2TitleSort.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_CopyTitle2TitleSort.Name = "btn_CopyTitle2TitleSort";
            this.btn_CopyTitle2TitleSort.Size = new System.Drawing.Size(168, 50);
            this.btn_CopyTitle2TitleSort.TabIndex = 278;
            this.btn_CopyTitle2TitleSort.Text = "Title->TitleSort";
            this.btn_CopyTitle2TitleSort.UseVisualStyleBackColor = true;
            this.btn_CopyTitle2TitleSort.Click += new System.EventHandler(this.btn_CopyTitle2TitleSort_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Location = new System.Drawing.Point(2108, 63);
            this.lbl_NoRec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(100, 56);
            this.lbl_NoRec.TabIndex = 279;
            this.lbl_NoRec.Text = " Records";
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(2380, 21);
            this.chbx_AutoSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(137, 29);
            this.chbx_AutoSave.TabIndex = 384;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            this.chbx_AutoSave.CheckedChanged += new System.EventHandler(this.chbx_AutoSave_CheckedChanged);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(2424, 79);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(268, 50);
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
            this.txt_Artist_Short.Location = new System.Drawing.Point(2106, 187);
            this.txt_Artist_Short.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Artist_Short.Name = "txt_Artist_Short";
            this.txt_Artist_Short.Size = new System.Drawing.Size(160, 32);
            this.txt_Artist_Short.TabIndex = 386;
            this.txt_Artist_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(2100, 744);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(248, 40);
            this.button2.TabIndex = 387;
            this.button2.Text = "Get Spotify Cover";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_Year_Correction
            // 
            this.txt_Year_Correction.Cue = "Year Correction";
            this.txt_Year_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Year_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Year_Correction.Location = new System.Drawing.Point(2494, 698);
            this.txt_Year_Correction.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Year_Correction.Name = "txt_Year_Correction";
            this.txt_Year_Correction.Size = new System.Drawing.Size(184, 32);
            this.txt_Year_Correction.TabIndex = 389;
            // 
            // chbx_Default_Cover
            // 
            this.chbx_Default_Cover.AutoSize = true;
            this.chbx_Default_Cover.Location = new System.Drawing.Point(2298, 698);
            this.chbx_Default_Cover.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.chbx_Default_Cover.Name = "chbx_Default_Cover";
            this.chbx_Default_Cover.Size = new System.Drawing.Size(181, 29);
            this.chbx_Default_Cover.TabIndex = 390;
            this.chbx_Default_Cover.Text = "Default_Cover";
            this.chbx_Default_Cover.UseVisualStyleBackColor = true;
            // 
            // txt_Album_Short
            // 
            this.txt_Album_Short.Cue = "ShortName";
            this.txt_Album_Short.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Short.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Short.Location = new System.Drawing.Point(2108, 277);
            this.txt_Album_Short.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Album_Short.Name = "txt_Album_Short";
            this.txt_Album_Short.Size = new System.Drawing.Size(160, 32);
            this.txt_Album_Short.TabIndex = 391;
            this.txt_Album_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Comments
            // 
            this.txt_Comments.Location = new System.Drawing.Point(2356, 744);
            this.txt_Comments.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(336, 75);
            this.txt_Comments.TabIndex = 392;
            this.txt_Comments.Text = "";
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(2100, 837);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(588, 38);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 393;
            // 
            // pxbx_SavedSpotify
            // 
            this.pxbx_SavedSpotify.Location = new System.Drawing.Point(2380, 438);
            this.pxbx_SavedSpotify.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pxbx_SavedSpotify.Name = "pxbx_SavedSpotify";
            this.pxbx_SavedSpotify.Size = new System.Drawing.Size(276, 215);
            this.pxbx_SavedSpotify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pxbx_SavedSpotify.TabIndex = 394;
            this.pxbx_SavedSpotify.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2164, 660);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 33);
            this.label1.TabIndex = 395;
            this.label1.Text = "Corrected";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2418, 660);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 33);
            this.label2.TabIndex = 396;
            this.label2.Text = "Saved from Spotify";
            // 
            // btn_CorrectWithSpotify
            // 
            this.btn_CorrectWithSpotify.Location = new System.Drawing.Point(2098, 788);
            this.btn_CorrectWithSpotify.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_CorrectWithSpotify.Name = "btn_CorrectWithSpotify";
            this.btn_CorrectWithSpotify.Size = new System.Drawing.Size(250, 40);
            this.btn_CorrectWithSpotify.TabIndex = 397;
            this.btn_CorrectWithSpotify.Text = "Correct with Spotify Cover";
            this.btn_CorrectWithSpotify.UseVisualStyleBackColor = true;
            this.btn_CorrectWithSpotify.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2424, 135);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(268, 50);
            this.button1.TabIndex = 398;
            this.button1.Text = "Delete All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_4);
            // 
            // Standardization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2610, 1054);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_CorrectWithSpotify);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pxbx_SavedSpotify);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.txt_Comments);
            this.Controls.Add(this.txt_Album_Short);
            this.Controls.Add(this.chbx_Default_Cover);
            this.Controls.Add(this.txt_Year_Correction);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txt_Artist_Short);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.chbx_AutoSave);
            this.Controls.Add(this.lbl_NoRec);
            this.Controls.Add(this.btn_CopyTitle2TitleSort);
            this.Controls.Add(this.btn_CopyArtist2ArtistSort);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.chbx_Include_ArtistSort);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_AlbumArt_Correction);
            this.Controls.Add(this.txt_Album_Correction);
            this.Controls.Add(this.txt_Artist_Correction);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.txt_AlbumArtPath);
            this.Controls.Add(this.btn_ChangeCover);
            this.Controls.Add(this.btn_Delete_All);
            this.Controls.Add(this.chbx_Save_All);
            this.Controls.Add(this.picbx_AlbumArtPath);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_Album);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Standardization";
            this.Text = "Standardization";
            this.Load += new System.EventHandler(this.Standardization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Delete_All)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbx_SavedSpotify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView btn_Delete_All;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button3;
        private CueTextBox txt_AlbumArtPath;
        private System.Windows.Forms.Button btn_ChangeCover;
        private CueTextBox txt_ID;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_Album;
        private CueTextBox txt_Artist_Correction;
        private CueTextBox txt_Artist;
        private CueTextBox txt_Album_Correction;
        private CueTextBox txt_AlbumArt_Correction;
        //private DLCManager.MainDBfields filed;
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

        internal Standardization(string txt_DBFolder, MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
            : this(txt_DBFolder, txt_TempPath, txt_RocksmithDLCPath, AllowEncript, AllowORIGDelete)
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
        private System.Windows.Forms.Button button2;
        private CueTextBox txt_Year_Correction;
        private System.Windows.Forms.CheckBox chbx_Default_Cover;
        private CueTextBox txt_Album_Short;
        private System.Windows.Forms.RichTextBox txt_Comments;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private MainDBfields filed;
        private System.Windows.Forms.PictureBox pxbx_SavedSpotify;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CorrectWithSpotify;
        private System.Windows.Forms.Button button1;
    }
}