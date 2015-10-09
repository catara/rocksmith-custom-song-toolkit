using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

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
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.chbx_Save_All = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_AlbumArtPath_Correction = new RocksmithToolkitGUI.CueTextBox();
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
            this.button1 = new System.Windows.Forms.Button();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
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
            this.DataGridView1.Size = new System.Drawing.Size(878, 544);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Enabled = false;
            this.btn_ChangeCover.Location = new System.Drawing.Point(941, 214);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(84, 26);
            this.btn_ChangeCover.TabIndex = 129;
            this.btn_ChangeCover.Text = "Change cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(1031, 214);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(138, 112);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 127;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // chbx_Save_All
            // 
            this.chbx_Save_All.AutoSize = true;
            this.chbx_Save_All.Enabled = false;
            this.chbx_Save_All.Location = new System.Drawing.Point(1051, 16);
            this.chbx_Save_All.Name = "chbx_Save_All";
            this.chbx_Save_All.Size = new System.Drawing.Size(37, 17);
            this.chbx_Save_All.TabIndex = 124;
            this.chbx_Save_All.Text = "All";
            this.chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(1092, 7);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 26);
            this.button8.TabIndex = 123;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(935, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 35);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_AlbumArtPath_Correction
            // 
            this.txt_AlbumArtPath_Correction.Cue = "Album art Path Correction";
            this.txt_AlbumArtPath_Correction.Enabled = false;
            this.txt_AlbumArtPath_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath_Correction.Location = new System.Drawing.Point(884, 188);
            this.txt_AlbumArtPath_Correction.Name = "txt_AlbumArtPath_Correction";
            this.txt_AlbumArtPath_Correction.Size = new System.Drawing.Size(291, 20);
            this.txt_AlbumArtPath_Correction.TabIndex = 134;
            this.txt_AlbumArtPath_Correction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Album_Correction
            // 
            this.txt_Album_Correction.Cue = "Album Correction";
            this.txt_Album_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Correction.Location = new System.Drawing.Point(953, 136);
            this.txt_Album_Correction.Name = "txt_Album_Correction";
            this.txt_Album_Correction.Size = new System.Drawing.Size(222, 20);
            this.txt_Album_Correction.TabIndex = 133;
            // 
            // txt_Artist_Correction
            // 
            this.txt_Artist_Correction.Cue = "Artist  Correction";
            this.txt_Artist_Correction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Correction.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Correction.Location = new System.Drawing.Point(953, 84);
            this.txt_Artist_Correction.Name = "txt_Artist_Correction";
            this.txt_Artist_Correction.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist_Correction.TabIndex = 132;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(953, 58);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.ReadOnly = true;
            this.txt_Artist.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist.TabIndex = 131;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Enabled = false;
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(884, 162);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(291, 20);
            this.txt_AlbumArtPath.TabIndex = 130;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(884, 7);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 128;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(953, 110);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.ReadOnly = true;
            this.txt_Album.Size = new System.Drawing.Size(222, 20);
            this.txt_Album.TabIndex = 126;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1104, 524);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
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
            this.chbx_Include_ArtistSort.Location = new System.Drawing.Point(1010, 524);
            this.chbx_Include_ArtistSort.Name = "chbx_Include_ArtistSort";
            this.chbx_Include_ArtistSort.Size = new System.Drawing.Size(89, 17);
            this.chbx_Include_ArtistSort.TabIndex = 274;
            this.chbx_Include_ArtistSort.Text = "Incl_Artistsort";
            this.chbx_Include_ArtistSort.UseVisualStyleBackColor = true;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(894, 501);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(110, 35);
            this.btn_Apply.TabIndex = 275;
            this.btn_Apply.Text = "Apply changes to the Main DB";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(1102, 485);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(73, 35);
            this.btn_DecompressAll.TabIndex = 276;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // btn_CopyArtist2ArtistSort
            // 
            this.btn_CopyArtist2ArtistSort.ForeColor = System.Drawing.Color.Green;
            this.btn_CopyArtist2ArtistSort.Location = new System.Drawing.Point(1010, 489);
            this.btn_CopyArtist2ArtistSort.Name = "btn_CopyArtist2ArtistSort";
            this.btn_CopyArtist2ArtistSort.Size = new System.Drawing.Size(84, 26);
            this.btn_CopyArtist2ArtistSort.TabIndex = 277;
            this.btn_CopyArtist2ArtistSort.Text = "Artist->ArtistSort";
            this.btn_CopyArtist2ArtistSort.UseVisualStyleBackColor = true;
            this.btn_CopyArtist2ArtistSort.Click += new System.EventHandler(this.btn_CopyArtist2ArtistSort_Click);
            // 
            // btn_CopyTitle2TitleSort
            // 
            this.btn_CopyTitle2TitleSort.ForeColor = System.Drawing.Color.Green;
            this.btn_CopyTitle2TitleSort.Location = new System.Drawing.Point(1010, 457);
            this.btn_CopyTitle2TitleSort.Name = "btn_CopyTitle2TitleSort";
            this.btn_CopyTitle2TitleSort.Size = new System.Drawing.Size(84, 26);
            this.btn_CopyTitle2TitleSort.TabIndex = 278;
            this.btn_CopyTitle2TitleSort.Text = "Title->TitleSort";
            this.btn_CopyTitle2TitleSort.UseVisualStyleBackColor = true;
            this.btn_CopyTitle2TitleSort.Click += new System.EventHandler(this.btn_CopyTitle2TitleSort_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Location = new System.Drawing.Point(884, 30);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(49, 29);
            this.lbl_NoRec.TabIndex = 279;
            this.lbl_NoRec.Text = " Records";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(912, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 22);
            this.button1.TabIndex = 383;
            this.button1.Text = "Default Cover";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(1092, 35);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(73, 17);
            this.chbx_AutoSave.TabIndex = 384;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            // 
            // Standardization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 548);
            this.Controls.Add(this.chbx_AutoSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_NoRec);
            this.Controls.Add(this.btn_CopyTitle2TitleSort);
            this.Controls.Add(this.btn_CopyArtist2ArtistSort);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.chbx_Include_ArtistSort);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_AlbumArtPath_Correction);
            this.Controls.Add(this.txt_Album_Correction);
            this.Controls.Add(this.txt_Artist_Correction);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.txt_AlbumArtPath);
            this.Controls.Add(this.btn_ChangeCover);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.chbx_Save_All);
            this.Controls.Add(this.picbx_AlbumArtPath);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_Album);
            this.Name = "Standardization";
            this.Text = "Standardization";
            this.Load += new System.EventHandler(this.Standardization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
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
        private CueTextBox txt_AlbumArtPath_Correction;
        private DLCManager.Files filed;
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

        public Standardization(string txt_DBFolder, DLCManager.Files filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
    }
}