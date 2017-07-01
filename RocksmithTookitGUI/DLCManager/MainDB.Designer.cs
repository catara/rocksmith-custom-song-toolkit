using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ookii.Dialogs;
using RocksmithToolkitLib;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class MainDB
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataViewGrid = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbx_DupliGTrack = new System.Windows.Forms.CheckBox();
            this.chbx_CopyOld = new System.Windows.Forms.CheckBox();
            this.chbx_KeepBassDD = new System.Windows.Forms.CheckBox();
            this.btn_Copy_old = new System.Windows.Forms.Button();
            this.chbx_InclGroups = new System.Windows.Forms.CheckBox();
            this.chbx_InclBeta = new System.Windows.Forms.CheckBox();
            this.btn_GarageBand = new System.Windows.Forms.Button();
            this.chbx_Replace = new System.Windows.Forms.CheckBox();
            this.chbx_Last_Packed = new System.Windows.Forms.CheckBox();
            this.btn_Package = new System.Windows.Forms.Button();
            this.chbx_UniqueID = new System.Windows.Forms.CheckBox();
            this.btn_CopyOld = new System.Windows.Forms.Button();
            this.btn_ApplyAlbumSortNames = new System.Windows.Forms.Button();
            this.btn_ApplyArtistShortNames = new System.Windows.Forms.Button();
            this.btn_Artist2SortA = new System.Windows.Forms.Button();
            this.btn_Title2SortT = new System.Windows.Forms.Button();
            this.bbtn_Replace_Brakets = new System.Windows.Forms.Button();
            this.btn_RemoveBrakets = new System.Windows.Forms.Button();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.chbx_Originals_Available = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.chbx_Duplicate_Official = new System.Windows.Forms.CheckBox();
            this.txt_Tuning = new RocksmithToolkitGUI.CueTextBox();
            this.btn_Beta = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btn_Find_FilesMissingIssues = new System.Windows.Forms.Button();
            this.txt_FilesMissingIssues = new RocksmithToolkitGUI.CueTextBox();
            this.btn_ReadGameLibrary = new System.Windows.Forms.Button();
            this.txt_OldPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Platform = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.txt_Track_No = new System.Windows.Forms.NumericUpDown();
            this.btn_ChangeLyrics = new System.Windows.Forms.Button();
            this.txt_Lyrics = new RocksmithToolkitGUI.CueTextBox();
            this.btn_GroupsAdd = new System.Windows.Forms.Button();
            this.btn_GroupsRemove = new System.Windows.Forms.Button();
            this.chbx_AllGroups = new System.Windows.Forms.CheckedListBox();
            this.btn_AddCoverFlags = new System.Windows.Forms.Button();
            this.btn_DefaultCover = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Top10 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Rating = new System.Windows.Forms.NumericUpDown();
            this.chbx_Group = new System.Windows.Forms.ComboBox();
            this.btn_OpenSongFolder = new System.Windows.Forms.Button();
            this.txt_Alt_No = new System.Windows.Forms.NumericUpDown();
            this.bth_GetTrackNo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btm_GoTemp = new System.Windows.Forms.Button();
            this.chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            this.chbx_Format = new System.Windows.Forms.ComboBox();
            this.chbx_Copy = new System.Windows.Forms.CheckBox();
            this.txt_FTPPath = new RocksmithToolkitGUI.CueTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chbx_Bass = new System.Windows.Forms.CheckBox();
            this.txt_Live_Details = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Lead = new System.Windows.Forms.CheckBox();
            this.chbx_Combo = new System.Windows.Forms.CheckBox();
            this.chbx_Rhythm = new System.Windows.Forms.CheckBox();
            this.txt_BassPicking = new RocksmithToolkitGUI.CueTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chbx_Is_Acoustic = new System.Windows.Forms.CheckBox();
            this.chbx_Is_Live = new System.Windows.Forms.CheckBox();
            this.btn_EOF = new System.Windows.Forms.Button();
            this.btn_CreateLyrics = new System.Windows.Forms.Button();
            this.chbx_KeepDD = new System.Windows.Forms.CheckBox();
            this.btn_AddSections = new System.Windows.Forms.Button();
            this.chbx_Lyrics = new System.Windows.Forms.CheckBox();
            this.chbx_TrackNo = new System.Windows.Forms.CheckBox();
            this.btn_AddDD = new System.Windows.Forms.Button();
            this.btn_RemoveDD = new System.Windows.Forms.Button();
            this.chbx_DD = new System.Windows.Forms.CheckBox();
            this.txt_AddDD = new System.Windows.Forms.NumericUpDown();
            this.chbx_Original = new System.Windows.Forms.CheckBox();
            this.chbx_Sections = new System.Windows.Forms.CheckBox();
            this.chbx_Preview = new System.Windows.Forms.CheckBox();
            this.chbx_Author = new System.Windows.Forms.CheckBox();
            this.btn_RemoveBassDD = new System.Windows.Forms.Button();
            this.chbx_BassDD = new System.Windows.Forms.CheckBox();
            this.chbx_Bonus = new System.Windows.Forms.CheckBox();
            this.chbx_Cover = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbx_Format_Originals = new System.Windows.Forms.ComboBox();
            this.btn_DuplicateFolder = new System.Windows.Forms.Button();
            this.chbx_Avail_Duplicate = new System.Windows.Forms.CheckBox();
            this.btn_OldFolder = new System.Windows.Forms.Button();
            this.chbx_Avail_Old = new System.Windows.Forms.CheckBox();
            this.chbx_Has_Been_Corrected = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.btn_SelectInverted = new System.Windows.Forms.Button();
            this.btn_InvertSelect = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_MultiTrackType = new System.Windows.Forms.ComboBox();
            this.chbx_MultiTrack = new System.Windows.Forms.CheckBox();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.chbx_Selected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbx_Beta = new System.Windows.Forms.CheckBox();
            this.btn_SelectNone = new System.Windows.Forms.Button();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.btn_NextItem = new System.Windows.Forms.Button();
            this.cmb_Filter = new System.Windows.Forms.ComboBox();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Volume = new System.Windows.Forms.NumericUpDown();
            this.txt_Preview_Volume = new System.Windows.Forms.NumericUpDown();
            this.btn_AddPreview = new System.Windows.Forms.Button();
            this.txt_PreviewStart = new System.Windows.Forms.DateTimePicker();
            this.btn_PlayAudio = new System.Windows.Forms.Button();
            this.btn_PlayPreview = new System.Windows.Forms.Button();
            this.txt_AverageTempo = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_AutoPlay = new System.Windows.Forms.CheckBox();
            this.txt_OggPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewEnd = new System.Windows.Forms.NumericUpDown();
            this.txt_OggPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.btn_SelectPreview = new System.Windows.Forms.Button();
            this.btn_OpenRetail = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_OpenStandardization = new System.Windows.Forms.Button();
            this.txt_AlbumArtPath = new RocksmithToolkitGUI.CueTextBox();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.txt_Artist_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
            this.txt_Album_Year = new RocksmithToolkitGUI.CueTextBox();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_APP_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DLC_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Version = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Author = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Title_Sort = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Title = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist_Sort = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist = new RocksmithToolkitGUI.CueTextBox();
            this.btn_Duplicate = new System.Windows.Forms.Button();
            this.btn_SearchReset = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.chbx_Alternate = new System.Windows.Forms.CheckBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_OpenDB = new System.Windows.Forms.Button();
            this.btn_Tones = new System.Windows.Forms.Button();
            this.btn_Arrangements = new System.Windows.Forms.Button();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txt_CustomForge_Vwersion = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_CustomsForge_ReleaseNotes = new System.Windows.Forms.RichTextBox();
            this.btn_Playthrough = new System.Windows.Forms.Button();
            this.txt_Playthough = new RocksmithToolkitGUI.CueTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_Like = new System.Windows.Forms.Button();
            this.txt_Followers = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_CustomsForge_Like = new System.Windows.Forms.NumericUpDown();
            this.btn_Followers = new System.Windows.Forms.Button();
            this.btn_CustomForge_Link = new System.Windows.Forms.Button();
            this.txt_debug = new System.Windows.Forms.RichTextBox();
            this.btn_Youtube = new System.Windows.Forms.Button();
            this.label59 = new System.Windows.Forms.Label();
            this.btn_Debug = new System.Windows.Forms.Button();
            this.txt_YouTube_Link = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CustomsForge_Link = new RocksmithToolkitGUI.CueTextBox();
            this.lbfl_YouTube_Link = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chbx_Ignore_Officials = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_Remove_All_Packed = new System.Windows.Forms.Button();
            this.cmb_Packed = new System.Windows.Forms.ComboBox();
            this.txt_CoundofPacked = new System.Windows.Forms.NumericUpDown();
            this.btn_Remove_Packed = new System.Windows.Forms.Button();
            this.btn_RemoveAllRemoteSongs = new System.Windows.Forms.Button();
            this.btn_RemoveRemoteSong = new System.Windows.Forms.Button();
            this.txt_RemotePath = new RocksmithToolkitGUI.CueTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txt_Spotify_Album_URL = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Spotify_Album_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Spotify_Artist_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Spotify_Song_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_SpotifyStatus = new RocksmithToolkitGUI.CueTextBox();
            this.picbx_SpotifyCover = new System.Windows.Forms.PictureBox();
            this.btn_GetTrack = new System.Windows.Forms.Button();
            this.txt_SavedPlaylists = new System.Windows.Forms.ListBox();
            this.txt_SavedTracks = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txt_SpotifyLog = new System.Windows.Forms.RichTextBox();
            this.btn_ActivateSpotify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewGrid)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Track_No)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Top10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Alt_No)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AddDD)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Preview_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PreviewEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomForge_Vwersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Followers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomsForge_Like)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CoundofPacked)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_SpotifyCover)).BeginInit();
            this.SuspendLayout();
            // 
            // DataViewGrid
            // 
            this.DataViewGrid.AllowUserToAddRows = false;
            this.DataViewGrid.AllowUserToDeleteRows = false;
            this.DataViewGrid.AllowUserToOrderColumns = true;
            this.DataViewGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataViewGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataViewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataViewGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataViewGrid.Location = new System.Drawing.Point(2, 3);
            this.DataViewGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DataViewGrid.MultiSelect = false;
            this.DataViewGrid.Name = "DataViewGrid";
            this.DataViewGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataViewGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataViewGrid.Size = new System.Drawing.Size(1108, 339);
            this.DataViewGrid.TabIndex = 2;
            this.DataViewGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataViewGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataViewGrid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataViewGrid_RowLeave);
            this.DataViewGrid.SelectionChanged += new System.EventHandler(this.ChangeEdit);
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "hey";
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "test";
            // 
            // chbx_DupliGTrack
            // 
            this.chbx_DupliGTrack.AutoSize = true;
            this.chbx_DupliGTrack.Enabled = false;
            this.chbx_DupliGTrack.Location = new System.Drawing.Point(82, 58);
            this.chbx_DupliGTrack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_DupliGTrack.Name = "chbx_DupliGTrack";
            this.chbx_DupliGTrack.Size = new System.Drawing.Size(55, 17);
            this.chbx_DupliGTrack.TabIndex = 332;
            this.chbx_DupliGTrack.Text = "L<->R";
            this.toolTip1.SetToolTip(this.chbx_DupliGTrack, "Duplicates one of the Guitar Tracks into the missing one, to eliminate the change" +
        " of track type in Game.");
            this.chbx_DupliGTrack.UseVisualStyleBackColor = true;
            // 
            // chbx_CopyOld
            // 
            this.chbx_CopyOld.AutoSize = true;
            this.chbx_CopyOld.Enabled = false;
            this.chbx_CopyOld.Location = new System.Drawing.Point(4, 72);
            this.chbx_CopyOld.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_CopyOld.Name = "chbx_CopyOld";
            this.chbx_CopyOld.Size = new System.Drawing.Size(50, 17);
            this.chbx_CopyOld.TabIndex = 327;
            this.chbx_CopyOld.Text = "Initial";
            this.toolTip1.SetToolTip(this.chbx_CopyOld, "Copy / Convert directly the original (no DLC Manager Manipulation)");
            this.chbx_CopyOld.UseVisualStyleBackColor = true;
            this.chbx_CopyOld.CheckedChanged += new System.EventHandler(this.chbx_CopyOld_CheckedChanged);
            // 
            // chbx_KeepBassDD
            // 
            this.chbx_KeepBassDD.AutoSize = true;
            this.chbx_KeepBassDD.Enabled = false;
            this.chbx_KeepBassDD.Location = new System.Drawing.Point(100, 18);
            this.chbx_KeepBassDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_KeepBassDD.Name = "chbx_KeepBassDD";
            this.chbx_KeepBassDD.Size = new System.Drawing.Size(15, 14);
            this.chbx_KeepBassDD.TabIndex = 329;
            this.toolTip1.SetToolTip(this.chbx_KeepBassDD, "Keep Bass DD at pack");
            this.chbx_KeepBassDD.UseVisualStyleBackColor = true;
            // 
            // btn_Copy_old
            // 
            this.btn_Copy_old.Enabled = false;
            this.btn_Copy_old.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Copy_old.Location = new System.Drawing.Point(956, 226);
            this.btn_Copy_old.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Copy_old.Name = "btn_Copy_old";
            this.btn_Copy_old.Size = new System.Drawing.Size(146, 20);
            this.btn_Copy_old.TabIndex = 328;
            this.btn_Copy_old.Text = "Copy All Filtered Songs 2 Import folder";
            this.toolTip1.SetToolTip(this.btn_Copy_old, "Copy all OLD files for the Selected Filter into root of Import folder");
            this.btn_Copy_old.UseVisualStyleBackColor = true;
            this.btn_Copy_old.Click += new System.EventHandler(this.btn_Copy_old_Click);
            // 
            // chbx_InclGroups
            // 
            this.chbx_InclGroups.AutoSize = true;
            this.chbx_InclGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbx_InclGroups.Location = new System.Drawing.Point(6, 34);
            this.chbx_InclGroups.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_InclGroups.Name = "chbx_InclGroups";
            this.chbx_InclGroups.Size = new System.Drawing.Size(65, 14);
            this.chbx_InclGroups.TabIndex = 382;
            this.chbx_InclGroups.Text = "Incl. Groups";
            this.toolTip1.SetToolTip(this.chbx_InclGroups, "Sets the Group for all To Be Selected Songs based on the value in the Group input" +
        " box");
            this.chbx_InclGroups.UseVisualStyleBackColor = true;
            // 
            // chbx_InclBeta
            // 
            this.chbx_InclBeta.AutoSize = true;
            this.chbx_InclBeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbx_InclBeta.Location = new System.Drawing.Point(72, 34);
            this.chbx_InclBeta.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_InclBeta.Name = "chbx_InclBeta";
            this.chbx_InclBeta.Size = new System.Drawing.Size(55, 14);
            this.chbx_InclBeta.TabIndex = 381;
            this.chbx_InclBeta.Text = "Incl. Beta";
            this.toolTip1.SetToolTip(this.chbx_InclBeta, "Adds 0_Group to the Titles of the songs To Be Selected");
            this.chbx_InclBeta.UseVisualStyleBackColor = true;
            // 
            // btn_GarageBand
            // 
            this.btn_GarageBand.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GarageBand.Location = new System.Drawing.Point(258, 14);
            this.btn_GarageBand.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_GarageBand.Name = "btn_GarageBand";
            this.btn_GarageBand.Size = new System.Drawing.Size(30, 20);
            this.btn_GarageBand.TabIndex = 333;
            this.btn_GarageBand.Text = "GB";
            this.toolTip1.SetToolTip(this.btn_GarageBand, "Generates a GarageBand compatible AudioFile to load as a additional track to the " +
        "intruments and mic tracks.");
            this.btn_GarageBand.UseVisualStyleBackColor = true;
            this.btn_GarageBand.Click += new System.EventHandler(this.btn_GarageBand_Click);
            // 
            // chbx_Replace
            // 
            this.chbx_Replace.AutoSize = true;
            this.chbx_Replace.Enabled = false;
            this.chbx_Replace.Location = new System.Drawing.Point(48, 72);
            this.chbx_Replace.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Replace.Name = "chbx_Replace";
            this.chbx_Replace.Size = new System.Drawing.Size(66, 17);
            this.chbx_Replace.TabIndex = 333;
            this.chbx_Replace.Text = "Replace";
            this.toolTip1.SetToolTip(this.chbx_Replace, "At Copy / FTP Replace the Song found in theb DLC target folder (providing the Lib" +
        "rary was READ before suing the Filter)");
            this.chbx_Replace.UseVisualStyleBackColor = true;
            // 
            // chbx_Last_Packed
            // 
            this.chbx_Last_Packed.AutoSize = true;
            this.chbx_Last_Packed.Enabled = false;
            this.chbx_Last_Packed.Location = new System.Drawing.Point(108, 72);
            this.chbx_Last_Packed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Last_Packed.Name = "chbx_Last_Packed";
            this.chbx_Last_Packed.Size = new System.Drawing.Size(46, 17);
            this.chbx_Last_Packed.TabIndex = 334;
            this.chbx_Last_Packed.Text = "Last";
            this.toolTip1.SetToolTip(this.chbx_Last_Packed, " Copy / Convert only the last Packed CDLC");
            this.chbx_Last_Packed.UseVisualStyleBackColor = true;
            // 
            // btn_Package
            // 
            this.btn_Package.Enabled = false;
            this.btn_Package.Location = new System.Drawing.Point(4, 18);
            this.btn_Package.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Package.Name = "btn_Package";
            this.btn_Package.Size = new System.Drawing.Size(59, 25);
            this.btn_Package.TabIndex = 39;
            this.btn_Package.Text = "Package";
            this.toolTip1.SetToolTip(this.btn_Package, "Package the current Song using the last  Song&FileNAme telplates and a unique DLC" +
        "_Name&ID");
            this.btn_Package.UseVisualStyleBackColor = true;
            this.btn_Package.Click += new System.EventHandler(this.btn_Conv_And_Transfer_Click);
            // 
            // chbx_UniqueID
            // 
            this.chbx_UniqueID.AutoSize = true;
            this.chbx_UniqueID.Checked = true;
            this.chbx_UniqueID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_UniqueID.Location = new System.Drawing.Point(60, 44);
            this.chbx_UniqueID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_UniqueID.Name = "chbx_UniqueID";
            this.chbx_UniqueID.Size = new System.Drawing.Size(92, 17);
            this.chbx_UniqueID.TabIndex = 329;
            this.chbx_UniqueID.Text = "UniqueDLCID";
            this.toolTip1.SetToolTip(this.chbx_UniqueID, "Replace the Unique ID with the last saved at conversion/packaging");
            this.chbx_UniqueID.UseVisualStyleBackColor = true;
            // 
            // btn_CopyOld
            // 
            this.btn_CopyOld.Enabled = false;
            this.btn_CopyOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CopyOld.Location = new System.Drawing.Point(114, 14);
            this.btn_CopyOld.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CopyOld.Name = "btn_CopyOld";
            this.btn_CopyOld.Size = new System.Drawing.Size(12, 14);
            this.btn_CopyOld.TabIndex = 328;
            this.btn_CopyOld.Text = "->";
            this.toolTip1.SetToolTip(this.btn_CopyOld, "Copy OLD/Initial imported file to Import folder");
            this.btn_CopyOld.UseVisualStyleBackColor = true;
            this.btn_CopyOld.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_ApplyAlbumSortNames
            // 
            this.btn_ApplyAlbumSortNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ApplyAlbumSortNames.Location = new System.Drawing.Point(410, 90);
            this.btn_ApplyAlbumSortNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ApplyAlbumSortNames.Name = "btn_ApplyAlbumSortNames";
            this.btn_ApplyAlbumSortNames.Size = new System.Drawing.Size(18, 16);
            this.btn_ApplyAlbumSortNames.TabIndex = 407;
            this.btn_ApplyAlbumSortNames.Text = ">";
            this.toolTip1.SetToolTip(this.btn_ApplyAlbumSortNames, "Sets the Album Short Name in MainDB && StandardizationDB. \r\n");
            this.btn_ApplyAlbumSortNames.UseVisualStyleBackColor = true;
            this.btn_ApplyAlbumSortNames.Click += new System.EventHandler(this.btn_ApplyAlbumSortNames_Click);
            // 
            // btn_ApplyArtistShortNames
            // 
            this.btn_ApplyArtistShortNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ApplyArtistShortNames.Location = new System.Drawing.Point(468, 3);
            this.btn_ApplyArtistShortNames.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ApplyArtistShortNames.Name = "btn_ApplyArtistShortNames";
            this.btn_ApplyArtistShortNames.Size = new System.Drawing.Size(18, 16);
            this.btn_ApplyArtistShortNames.TabIndex = 406;
            this.btn_ApplyArtistShortNames.Text = ">";
            this.toolTip1.SetToolTip(this.btn_ApplyArtistShortNames, "Sets the Artist Short Name in MainDB && StandardizationDB. ");
            this.btn_ApplyArtistShortNames.UseVisualStyleBackColor = true;
            this.btn_ApplyArtistShortNames.Click += new System.EventHandler(this.btn_ApplyShortNames_Click);
            // 
            // btn_Artist2SortA
            // 
            this.btn_Artist2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Artist2SortA.Location = new System.Drawing.Point(410, 3);
            this.btn_Artist2SortA.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Artist2SortA.Name = "btn_Artist2SortA";
            this.btn_Artist2SortA.Size = new System.Drawing.Size(18, 16);
            this.btn_Artist2SortA.TabIndex = 403;
            this.btn_Artist2SortA.Text = ">";
            this.toolTip1.SetToolTip(this.btn_Artist2SortA, "Copies the Artist to ArtistSort");
            this.btn_Artist2SortA.UseVisualStyleBackColor = true;
            this.btn_Artist2SortA.Click += new System.EventHandler(this.btn_Artist2SortA_Click_1);
            // 
            // btn_Title2SortT
            // 
            this.btn_Title2SortT.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Title2SortT.Location = new System.Drawing.Point(410, 73);
            this.btn_Title2SortT.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Title2SortT.Name = "btn_Title2SortT";
            this.btn_Title2SortT.Size = new System.Drawing.Size(18, 15);
            this.btn_Title2SortT.TabIndex = 402;
            this.btn_Title2SortT.Text = ">";
            this.toolTip1.SetToolTip(this.btn_Title2SortT, "Copies the Title to Title Sort");
            this.btn_Title2SortT.UseVisualStyleBackColor = true;
            this.btn_Title2SortT.Click += new System.EventHandler(this.btn_Title2SortT_Click);
            // 
            // bbtn_Replace_Brakets
            // 
            this.bbtn_Replace_Brakets.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bbtn_Replace_Brakets.Location = new System.Drawing.Point(410, 46);
            this.bbtn_Replace_Brakets.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bbtn_Replace_Brakets.Name = "bbtn_Replace_Brakets";
            this.bbtn_Replace_Brakets.Size = new System.Drawing.Size(18, 14);
            this.bbtn_Replace_Brakets.TabIndex = 412;
            this.bbtn_Replace_Brakets.Text = "(";
            this.toolTip1.SetToolTip(this.bbtn_Replace_Brakets, "Replaces () with []");
            this.bbtn_Replace_Brakets.UseVisualStyleBackColor = true;
            this.bbtn_Replace_Brakets.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_RemoveBrakets
            // 
            this.btn_RemoveBrakets.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveBrakets.Location = new System.Drawing.Point(410, 59);
            this.btn_RemoveBrakets.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_RemoveBrakets.Name = "btn_RemoveBrakets";
            this.btn_RemoveBrakets.Size = new System.Drawing.Size(18, 14);
            this.btn_RemoveBrakets.TabIndex = 413;
            this.btn_RemoveBrakets.Text = "[";
            this.toolTip1.SetToolTip(this.btn_RemoveBrakets, "Removes content of []");
            this.btn_RemoveBrakets.UseVisualStyleBackColor = true;
            this.btn_RemoveBrakets.Click += new System.EventHandler(this.btn_RemoveBrakets_Click);
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(126, 92);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 6);
            this.btn_SteamDLCFolder.TabIndex = 311;
            this.btn_SteamDLCFolder.Text = "...";
            this.toolTip1.SetToolTip(this.btn_SteamDLCFolder, "Open Steam DLC Folder");
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // chbx_Originals_Available
            // 
            this.chbx_Originals_Available.AutoSize = true;
            this.chbx_Originals_Available.Enabled = false;
            this.chbx_Originals_Available.Location = new System.Drawing.Point(6, 66);
            this.chbx_Originals_Available.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Originals_Available.Name = "chbx_Originals_Available";
            this.chbx_Originals_Available.Size = new System.Drawing.Size(87, 17);
            this.chbx_Originals_Available.TabIndex = 329;
            this.chbx_Originals_Available.Text = "Official Avail.";
            this.toolTip1.SetToolTip(this.chbx_Originals_Available, "Shows if Duplicates of DLC exists that are actually Official songs but from a Dif" +
        "ferent Platform");
            this.chbx_Originals_Available.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(602, 58);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 20);
            this.button3.TabIndex = 345;
            this.button3.Text = "-";
            this.toolTip1.SetToolTip(this.button3, "Delete Current Duplicate");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // chbx_Duplicate_Official
            // 
            this.chbx_Duplicate_Official.AutoSize = true;
            this.chbx_Duplicate_Official.Enabled = false;
            this.chbx_Duplicate_Official.Location = new System.Drawing.Point(581, 88);
            this.chbx_Duplicate_Official.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Duplicate_Official.Name = "chbx_Duplicate_Official";
            this.chbx_Duplicate_Official.Size = new System.Drawing.Size(58, 17);
            this.chbx_Duplicate_Official.TabIndex = 346;
            this.chbx_Duplicate_Official.Text = "Official";
            this.toolTip1.SetToolTip(this.chbx_Duplicate_Official, "Shows if Duplicates of DLC exists that are actually Official songs but from a Dif" +
        "ferent Platform");
            this.chbx_Duplicate_Official.UseVisualStyleBackColor = true;
            // 
            // txt_Tuning
            // 
            this.txt_Tuning.Cue = "Tuning (All)";
            this.txt_Tuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tuning.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tuning.Location = new System.Drawing.Point(6, 51);
            this.txt_Tuning.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Tuning.Name = "txt_Tuning";
            this.txt_Tuning.Size = new System.Drawing.Size(74, 20);
            this.txt_Tuning.TabIndex = 78;
            this.toolTip1.SetToolTip(this.txt_Tuning, "For all instruments.");
            // 
            // btn_Beta
            // 
            this.btn_Beta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Beta.Location = new System.Drawing.Point(340, 14);
            this.btn_Beta.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Beta.Name = "btn_Beta";
            this.btn_Beta.Size = new System.Drawing.Size(56, 21);
            this.btn_Beta.TabIndex = 381;
            this.btn_Beta.Text = "Beta Filtered";
            this.toolTip1.SetToolTip(this.btn_Beta, "Make all filtered songs as Beta.");
            this.btn_Beta.UseVisualStyleBackColor = true;
            this.btn_Beta.Visible = false;
            this.btn_Beta.Click += new System.EventHandler(this.btn_Beta_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(2, 347);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1114, 274);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Panel1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(1106, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DLC Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btn_RemoveBrakets);
            this.Panel1.Controls.Add(this.bbtn_Replace_Brakets);
            this.Panel1.Controls.Add(this.btn_Find_FilesMissingIssues);
            this.Panel1.Controls.Add(this.txt_FilesMissingIssues);
            this.Panel1.Controls.Add(this.btn_ReadGameLibrary);
            this.Panel1.Controls.Add(this.txt_OldPath);
            this.Panel1.Controls.Add(this.btn_Copy_old);
            this.Panel1.Controls.Add(this.txt_Platform);
            this.Panel1.Controls.Add(this.label13);
            this.Panel1.Controls.Add(this.txt_Description);
            this.Panel1.Controls.Add(this.btn_ApplyAlbumSortNames);
            this.Panel1.Controls.Add(this.btn_ApplyArtistShortNames);
            this.Panel1.Controls.Add(this.txt_Track_No);
            this.Panel1.Controls.Add(this.btn_ChangeLyrics);
            this.Panel1.Controls.Add(this.txt_Lyrics);
            this.Panel1.Controls.Add(this.btn_Artist2SortA);
            this.Panel1.Controls.Add(this.btn_Title2SortT);
            this.Panel1.Controls.Add(this.btn_GroupsAdd);
            this.Panel1.Controls.Add(this.btn_GroupsRemove);
            this.Panel1.Controls.Add(this.chbx_AllGroups);
            this.Panel1.Controls.Add(this.btn_AddCoverFlags);
            this.Panel1.Controls.Add(this.btn_DefaultCover);
            this.Panel1.Controls.Add(this.label8);
            this.Panel1.Controls.Add(this.txt_Top10);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.txt_Rating);
            this.Panel1.Controls.Add(this.chbx_Group);
            this.Panel1.Controls.Add(this.btn_OpenSongFolder);
            this.Panel1.Controls.Add(this.txt_Alt_No);
            this.Panel1.Controls.Add(this.bth_GetTrackNo);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.groupBox6);
            this.Panel1.Controls.Add(this.groupBox5);
            this.Panel1.Controls.Add(this.groupBox4);
            this.Panel1.Controls.Add(this.groupBox3);
            this.Panel1.Controls.Add(this.groupBox2);
            this.Panel1.Controls.Add(this.groupBox1);
            this.Panel1.Controls.Add(this.btn_OpenRetail);
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.btn_OpenStandardization);
            this.Panel1.Controls.Add(this.txt_AlbumArtPath);
            this.Panel1.Controls.Add(this.btn_ChangeCover);
            this.Panel1.Controls.Add(this.txt_Artist_ShortName);
            this.Panel1.Controls.Add(this.txt_Album_ShortName);
            this.Panel1.Controls.Add(this.chbx_AutoSave);
            this.Panel1.Controls.Add(this.txt_Album_Year);
            this.Panel1.Controls.Add(this.picbx_AlbumArtPath);
            this.Panel1.Controls.Add(this.btn_Save);
            this.Panel1.Controls.Add(this.txt_APP_ID);
            this.Panel1.Controls.Add(this.txt_DLC_ID);
            this.Panel1.Controls.Add(this.txt_Version);
            this.Panel1.Controls.Add(this.txt_Author);
            this.Panel1.Controls.Add(this.txt_Album);
            this.Panel1.Controls.Add(this.txt_Title_Sort);
            this.Panel1.Controls.Add(this.txt_Title);
            this.Panel1.Controls.Add(this.txt_Artist_Sort);
            this.Panel1.Controls.Add(this.txt_Artist);
            this.Panel1.Controls.Add(this.btn_Duplicate);
            this.Panel1.Controls.Add(this.btn_SearchReset);
            this.Panel1.Controls.Add(this.btn_Search);
            this.Panel1.Controls.Add(this.chbx_Alternate);
            this.Panel1.Controls.Add(this.btn_Delete);
            this.Panel1.Controls.Add(this.btn_OpenDB);
            this.Panel1.Controls.Add(this.btn_Tones);
            this.Panel1.Controls.Add(this.btn_Arrangements);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(2, 0);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1102, 246);
            this.Panel1.TabIndex = 4;
            // 
            // btn_Find_FilesMissingIssues
            // 
            this.btn_Find_FilesMissingIssues.Location = new System.Drawing.Point(950, 3);
            this.btn_Find_FilesMissingIssues.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Find_FilesMissingIssues.Name = "btn_Find_FilesMissingIssues";
            this.btn_Find_FilesMissingIssues.Size = new System.Drawing.Size(95, 25);
            this.btn_Find_FilesMissingIssues.TabIndex = 411;
            this.btn_Find_FilesMissingIssues.Text = "Check All songs";
            this.btn_Find_FilesMissingIssues.UseVisualStyleBackColor = true;
            this.btn_Find_FilesMissingIssues.Click += new System.EventHandler(this.btn_Find_FilesMissingIssues_Click);
            // 
            // txt_FilesMissingIssues
            // 
            this.txt_FilesMissingIssues.Cue = "FilesMissingIssues";
            this.txt_FilesMissingIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FilesMissingIssues.ForeColor = System.Drawing.Color.Gray;
            this.txt_FilesMissingIssues.HideSelection = false;
            this.txt_FilesMissingIssues.Location = new System.Drawing.Point(598, 229);
            this.txt_FilesMissingIssues.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_FilesMissingIssues.Name = "txt_FilesMissingIssues";
            this.txt_FilesMissingIssues.ReadOnly = true;
            this.txt_FilesMissingIssues.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_FilesMissingIssues.Size = new System.Drawing.Size(232, 20);
            this.txt_FilesMissingIssues.TabIndex = 334;
            // 
            // btn_ReadGameLibrary
            // 
            this.btn_ReadGameLibrary.Location = new System.Drawing.Point(950, 30);
            this.btn_ReadGameLibrary.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ReadGameLibrary.Name = "btn_ReadGameLibrary";
            this.btn_ReadGameLibrary.Size = new System.Drawing.Size(152, 25);
            this.btn_ReadGameLibrary.TabIndex = 410;
            this.btn_ReadGameLibrary.Text = "Read Game DLC Library";
            this.btn_ReadGameLibrary.UseVisualStyleBackColor = true;
            this.btn_ReadGameLibrary.Click += new System.EventHandler(this.btn_ReadGameLibrary_Click);
            // 
            // txt_OldPath
            // 
            this.txt_OldPath.Cue = "Old Path";
            this.txt_OldPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_OldPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_OldPath.HideSelection = false;
            this.txt_OldPath.Location = new System.Drawing.Point(832, 229);
            this.txt_OldPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_OldPath.Name = "txt_OldPath";
            this.txt_OldPath.ReadOnly = true;
            this.txt_OldPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_OldPath.Size = new System.Drawing.Size(116, 20);
            this.txt_OldPath.TabIndex = 334;
            this.txt_OldPath.Visible = false;
            // 
            // txt_Platform
            // 
            this.txt_Platform.FormattingEnabled = true;
            this.txt_Platform.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.txt_Platform.Location = new System.Drawing.Point(434, 112);
            this.txt_Platform.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Platform.Name = "txt_Platform";
            this.txt_Platform.Size = new System.Drawing.Size(50, 21);
            this.txt_Platform.TabIndex = 335;
            this.txt_Platform.Text = "PS3";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(498, 231);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 408;
            this.label13.Text = "Comments";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(392, 214);
            this.txt_Description.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(104, 31);
            this.txt_Description.TabIndex = 334;
            this.txt_Description.Text = "";
            this.txt_Description.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Artist_KeyPress);
            // 
            // txt_Track_No
            // 
            this.txt_Track_No.Location = new System.Drawing.Point(502, 4);
            this.txt_Track_No.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Track_No.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txt_Track_No.Name = "txt_Track_No";
            this.txt_Track_No.Size = new System.Drawing.Size(34, 20);
            this.txt_Track_No.TabIndex = 405;
            this.txt_Track_No.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txt_Track_No.ValueChanged += new System.EventHandler(this.txt_Track_No_TextChanged);
            // 
            // btn_ChangeLyrics
            // 
            this.btn_ChangeLyrics.Location = new System.Drawing.Point(598, 208);
            this.btn_ChangeLyrics.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ChangeLyrics.Name = "btn_ChangeLyrics";
            this.btn_ChangeLyrics.Size = new System.Drawing.Size(98, 21);
            this.btn_ChangeLyrics.TabIndex = 404;
            this.btn_ChangeLyrics.Text = "Change Lyrics";
            this.btn_ChangeLyrics.UseVisualStyleBackColor = true;
            this.btn_ChangeLyrics.Click += new System.EventHandler(this.btn_ChangeLyrics_Click);
            // 
            // txt_Lyrics
            // 
            this.txt_Lyrics.Cue = "Lyrics Path";
            this.txt_Lyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Lyrics.ForeColor = System.Drawing.Color.Gray;
            this.txt_Lyrics.HideSelection = false;
            this.txt_Lyrics.Location = new System.Drawing.Point(500, 210);
            this.txt_Lyrics.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Lyrics.Name = "txt_Lyrics";
            this.txt_Lyrics.ReadOnly = true;
            this.txt_Lyrics.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Lyrics.Size = new System.Drawing.Size(98, 20);
            this.txt_Lyrics.TabIndex = 334;
            this.txt_Lyrics.Visible = false;
            // 
            // btn_GroupsAdd
            // 
            this.btn_GroupsAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupsAdd.Location = new System.Drawing.Point(246, 132);
            this.btn_GroupsAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_GroupsAdd.Name = "btn_GroupsAdd";
            this.btn_GroupsAdd.Size = new System.Drawing.Size(22, 20);
            this.btn_GroupsAdd.TabIndex = 334;
            this.btn_GroupsAdd.Text = "+";
            this.btn_GroupsAdd.UseVisualStyleBackColor = true;
            this.btn_GroupsAdd.Click += new System.EventHandler(this.btn_GroupsAdd_Click);
            // 
            // btn_GroupsRemove
            // 
            this.btn_GroupsRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GroupsRemove.Location = new System.Drawing.Point(268, 132);
            this.btn_GroupsRemove.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_GroupsRemove.Name = "btn_GroupsRemove";
            this.btn_GroupsRemove.Size = new System.Drawing.Size(22, 20);
            this.btn_GroupsRemove.TabIndex = 333;
            this.btn_GroupsRemove.Text = "-";
            this.btn_GroupsRemove.UseVisualStyleBackColor = true;
            this.btn_GroupsRemove.Click += new System.EventHandler(this.btn_GroupsRemove_Click);
            // 
            // chbx_AllGroups
            // 
            this.chbx_AllGroups.CheckOnClick = true;
            this.chbx_AllGroups.FormattingEnabled = true;
            this.chbx_AllGroups.Location = new System.Drawing.Point(206, 175);
            this.chbx_AllGroups.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_AllGroups.Name = "chbx_AllGroups";
            this.chbx_AllGroups.Size = new System.Drawing.Size(182, 64);
            this.chbx_AllGroups.Sorted = true;
            this.chbx_AllGroups.TabIndex = 382;
            this.chbx_AllGroups.SelectedValueChanged += new System.EventHandler(this.chbx_AllGroups_SelectedValueChanged);
            // 
            // btn_AddCoverFlags
            // 
            this.btn_AddCoverFlags.Enabled = false;
            this.btn_AddCoverFlags.Location = new System.Drawing.Point(832, 204);
            this.btn_AddCoverFlags.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_AddCoverFlags.Name = "btn_AddCoverFlags";
            this.btn_AddCoverFlags.Size = new System.Drawing.Size(114, 21);
            this.btn_AddCoverFlags.TabIndex = 383;
            this.btn_AddCoverFlags.Text = "Add Cover Flags";
            this.btn_AddCoverFlags.UseVisualStyleBackColor = true;
            // 
            // btn_DefaultCover
            // 
            this.btn_DefaultCover.Location = new System.Drawing.Point(832, 181);
            this.btn_DefaultCover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_DefaultCover.Name = "btn_DefaultCover";
            this.btn_DefaultCover.Size = new System.Drawing.Size(114, 21);
            this.btn_DefaultCover.TabIndex = 382;
            this.btn_DefaultCover.Text = "Default Cover";
            this.btn_DefaultCover.UseVisualStyleBackColor = true;
            this.btn_DefaultCover.Click += new System.EventHandler(this.btn_DefaultCover_Click);
            // 
            // label8
            // 
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(598, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 18);
            this.label8.TabIndex = 334;
            this.label8.Text = "in Top 10";
            // 
            // txt_Top10
            // 
            this.txt_Top10.Enabled = false;
            this.txt_Top10.Location = new System.Drawing.Point(566, 3);
            this.txt_Top10.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Top10.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txt_Top10.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Top10.Name = "txt_Top10";
            this.txt_Top10.Size = new System.Drawing.Size(34, 20);
            this.txt_Top10.TabIndex = 335;
            this.txt_Top10.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(490, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 333;
            this.label6.Text = "#";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(204, 134);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 324;
            this.label3.Text = "Group:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(320, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 18);
            this.label5.TabIndex = 325;
            this.label5.Text = "/5 CDLC stars";
            // 
            // txt_Rating
            // 
            this.txt_Rating.Location = new System.Drawing.Point(294, 134);
            this.txt_Rating.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Rating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_Rating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Rating.Name = "txt_Rating";
            this.txt_Rating.Size = new System.Drawing.Size(26, 20);
            this.txt_Rating.TabIndex = 332;
            this.txt_Rating.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chbx_Group
            // 
            this.chbx_Group.FormattingEnabled = true;
            this.chbx_Group.Location = new System.Drawing.Point(206, 154);
            this.chbx_Group.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Group.Name = "chbx_Group";
            this.chbx_Group.Size = new System.Drawing.Size(182, 21);
            this.chbx_Group.TabIndex = 323;
            this.chbx_Group.SelectedIndexChanged += new System.EventHandler(this.chbx_Group_SelectedIndexChanged);
            // 
            // btn_OpenSongFolder
            // 
            this.btn_OpenSongFolder.Location = new System.Drawing.Point(10, 136);
            this.btn_OpenSongFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OpenSongFolder.Name = "btn_OpenSongFolder";
            this.btn_OpenSongFolder.Size = new System.Drawing.Size(122, 20);
            this.btn_OpenSongFolder.TabIndex = 331;
            this.btn_OpenSongFolder.Text = "Open Song Folder";
            this.btn_OpenSongFolder.UseVisualStyleBackColor = true;
            this.btn_OpenSongFolder.Click += new System.EventHandler(this.btn_OpenSongFolder_Click);
            // 
            // txt_Alt_No
            // 
            this.txt_Alt_No.Location = new System.Drawing.Point(614, 46);
            this.txt_Alt_No.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Alt_No.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txt_Alt_No.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Alt_No.Name = "txt_Alt_No";
            this.txt_Alt_No.Size = new System.Drawing.Size(32, 20);
            this.txt_Alt_No.TabIndex = 330;
            this.txt_Alt_No.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // bth_GetTrackNo
            // 
            this.bth_GetTrackNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bth_GetTrackNo.Location = new System.Drawing.Point(542, 3);
            this.bth_GetTrackNo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bth_GetTrackNo.Name = "bth_GetTrackNo";
            this.bth_GetTrackNo.Size = new System.Drawing.Size(18, 16);
            this.bth_GetTrackNo.TabIndex = 329;
            this.bth_GetTrackNo.Text = "<";
            this.bth_GetTrackNo.UseVisualStyleBackColor = true;
            this.bth_GetTrackNo.Click += new System.EventHandler(this.bth_GetTrackNo_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(488, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 18);
            this.label4.TabIndex = 325;
            this.label4.Text = "v.";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.btm_GoTemp);
            this.groupBox6.Controls.Add(this.chbx_Last_Packed);
            this.groupBox6.Controls.Add(this.chbx_Replace);
            this.groupBox6.Controls.Add(this.chbx_DupliGTrack);
            this.groupBox6.Controls.Add(this.chbx_CopyOld);
            this.groupBox6.Controls.Add(this.chbx_RemoveBassDD);
            this.groupBox6.Controls.Add(this.pB_ReadDLCs);
            this.groupBox6.Controls.Add(this.chbx_PreSavedFTP);
            this.groupBox6.Controls.Add(this.btn_Package);
            this.groupBox6.Controls.Add(this.chbx_Format);
            this.groupBox6.Controls.Add(this.chbx_Copy);
            this.groupBox6.Controls.Add(this.txt_FTPPath);
            this.groupBox6.Controls.Add(this.btn_SteamDLCFolder);
            this.groupBox6.Controls.Add(this.chbx_UniqueID);
            this.groupBox6.Location = new System.Drawing.Point(952, 102);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox6.Size = new System.Drawing.Size(156, 130);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Package and Copy";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 99);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 5);
            this.button1.TabIndex = 413;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btm_GoTemp
            // 
            this.btm_GoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btm_GoTemp.Location = new System.Drawing.Point(124, 24);
            this.btm_GoTemp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btm_GoTemp.Name = "btm_GoTemp";
            this.btm_GoTemp.Size = new System.Drawing.Size(22, 14);
            this.btm_GoTemp.TabIndex = 412;
            this.btm_GoTemp.Text = "->";
            this.btm_GoTemp.UseVisualStyleBackColor = true;
            // 
            // chbx_RemoveBassDD
            // 
            this.chbx_RemoveBassDD.AutoSize = true;
            this.chbx_RemoveBassDD.Checked = true;
            this.chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_RemoveBassDD.Location = new System.Drawing.Point(4, 59);
            this.chbx_RemoveBassDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            this.chbx_RemoveBassDD.Size = new System.Drawing.Size(82, 17);
            this.chbx_RemoveBassDD.TabIndex = 331;
            this.chbx_RemoveBassDD.Text = "wo BassDD";
            this.chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(4, 108);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(146, 15);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 330;
            // 
            // chbx_PreSavedFTP
            // 
            this.chbx_PreSavedFTP.FormattingEnabled = true;
            this.chbx_PreSavedFTP.Items.AddRange(new object[] {
            "EU",
            "US"});
            this.chbx_PreSavedFTP.Location = new System.Drawing.Point(4, 89);
            this.chbx_PreSavedFTP.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            this.chbx_PreSavedFTP.Size = new System.Drawing.Size(46, 21);
            this.chbx_PreSavedFTP.TabIndex = 328;
            this.chbx_PreSavedFTP.Text = "US";
            this.chbx_PreSavedFTP.SelectedIndexChanged += new System.EventHandler(this.cbx_Format_SelectedValueChanged);
            // 
            // chbx_Format
            // 
            this.chbx_Format.FormattingEnabled = true;
            this.chbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.chbx_Format.Location = new System.Drawing.Point(67, 23);
            this.chbx_Format.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Format.Name = "chbx_Format";
            this.chbx_Format.Size = new System.Drawing.Size(55, 21);
            this.chbx_Format.TabIndex = 108;
            this.chbx_Format.Text = "PS3";
            this.chbx_Format.SelectedIndexChanged += new System.EventHandler(this.cbx_Format_SelectedValueChanged);
            // 
            // chbx_Copy
            // 
            this.chbx_Copy.AutoSize = true;
            this.chbx_Copy.Checked = true;
            this.chbx_Copy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Copy.Location = new System.Drawing.Point(4, 44);
            this.chbx_Copy.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Copy.Name = "chbx_Copy";
            this.chbx_Copy.Size = new System.Drawing.Size(56, 17);
            this.chbx_Copy.TabIndex = 326;
            this.chbx_Copy.Text = "&&Copy";
            this.chbx_Copy.UseVisualStyleBackColor = true;
            this.chbx_Copy.CheckedChanged += new System.EventHandler(this.chbx_Copy_CheckedChanged);
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(58, 90);
            this.txt_FTPPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(64, 20);
            this.txt_FTPPath.TabIndex = 308;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chbx_Bass);
            this.groupBox5.Controls.Add(this.txt_Live_Details);
            this.groupBox5.Controls.Add(this.chbx_Lead);
            this.groupBox5.Controls.Add(this.chbx_Combo);
            this.groupBox5.Controls.Add(this.chbx_Rhythm);
            this.groupBox5.Controls.Add(this.txt_BassPicking);
            this.groupBox5.Controls.Add(this.txt_Tuning);
            this.groupBox5.Location = new System.Drawing.Point(490, 66);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox5.Size = new System.Drawing.Size(154, 75);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Available Instruments";
            // 
            // chbx_Bass
            // 
            this.chbx_Bass.AutoSize = true;
            this.chbx_Bass.Enabled = false;
            this.chbx_Bass.Location = new System.Drawing.Point(66, 34);
            this.chbx_Bass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Bass.Name = "chbx_Bass";
            this.chbx_Bass.Size = new System.Drawing.Size(49, 17);
            this.chbx_Bass.TabIndex = 75;
            this.chbx_Bass.Text = "Bass";
            this.chbx_Bass.UseVisualStyleBackColor = true;
            // 
            // txt_Live_Details
            // 
            this.txt_Live_Details.Cue = "Live/Acoustic Details";
            this.txt_Live_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Live_Details.ForeColor = System.Drawing.Color.Gray;
            this.txt_Live_Details.Location = new System.Drawing.Point(82, 51);
            this.txt_Live_Details.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Live_Details.Name = "txt_Live_Details";
            this.txt_Live_Details.Size = new System.Drawing.Size(72, 20);
            this.txt_Live_Details.TabIndex = 335;
            this.txt_Live_Details.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Artist_KeyPress);
            // 
            // chbx_Lead
            // 
            this.chbx_Lead.AutoSize = true;
            this.chbx_Lead.Enabled = false;
            this.chbx_Lead.Location = new System.Drawing.Point(6, 16);
            this.chbx_Lead.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Lead.Name = "chbx_Lead";
            this.chbx_Lead.Size = new System.Drawing.Size(50, 17);
            this.chbx_Lead.TabIndex = 74;
            this.chbx_Lead.Text = "Lead";
            this.chbx_Lead.UseVisualStyleBackColor = true;
            // 
            // chbx_Combo
            // 
            this.chbx_Combo.AutoSize = true;
            this.chbx_Combo.Enabled = false;
            this.chbx_Combo.Location = new System.Drawing.Point(66, 16);
            this.chbx_Combo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Combo.Name = "chbx_Combo";
            this.chbx_Combo.Size = new System.Drawing.Size(59, 17);
            this.chbx_Combo.TabIndex = 76;
            this.chbx_Combo.Text = "Combo";
            this.chbx_Combo.UseVisualStyleBackColor = true;
            // 
            // chbx_Rhythm
            // 
            this.chbx_Rhythm.AutoSize = true;
            this.chbx_Rhythm.Enabled = false;
            this.chbx_Rhythm.Location = new System.Drawing.Point(6, 34);
            this.chbx_Rhythm.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Rhythm.Name = "chbx_Rhythm";
            this.chbx_Rhythm.Size = new System.Drawing.Size(62, 17);
            this.chbx_Rhythm.TabIndex = 77;
            this.chbx_Rhythm.Text = "Rhythm";
            this.chbx_Rhythm.UseVisualStyleBackColor = true;
            // 
            // txt_BassPicking
            // 
            this.txt_BassPicking.Cue = "Bass Picking";
            this.txt_BassPicking.Enabled = false;
            this.txt_BassPicking.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.txt_BassPicking.ForeColor = System.Drawing.Color.Gray;
            this.txt_BassPicking.Location = new System.Drawing.Point(116, 34);
            this.txt_BassPicking.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_BassPicking.Name = "txt_BassPicking";
            this.txt_BassPicking.Size = new System.Drawing.Size(38, 17);
            this.txt_BassPicking.TabIndex = 96;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chbx_Is_Acoustic);
            this.groupBox4.Controls.Add(this.chbx_Is_Live);
            this.groupBox4.Controls.Add(this.btn_EOF);
            this.groupBox4.Controls.Add(this.btn_CreateLyrics);
            this.groupBox4.Controls.Add(this.chbx_KeepDD);
            this.groupBox4.Controls.Add(this.chbx_KeepBassDD);
            this.groupBox4.Controls.Add(this.btn_AddSections);
            this.groupBox4.Controls.Add(this.chbx_Lyrics);
            this.groupBox4.Controls.Add(this.chbx_TrackNo);
            this.groupBox4.Controls.Add(this.btn_AddDD);
            this.groupBox4.Controls.Add(this.btn_RemoveDD);
            this.groupBox4.Controls.Add(this.chbx_DD);
            this.groupBox4.Controls.Add(this.txt_AddDD);
            this.groupBox4.Controls.Add(this.chbx_Original);
            this.groupBox4.Controls.Add(this.chbx_Sections);
            this.groupBox4.Controls.Add(this.chbx_Preview);
            this.groupBox4.Controls.Add(this.chbx_Author);
            this.groupBox4.Controls.Add(this.btn_RemoveBassDD);
            this.groupBox4.Controls.Add(this.chbx_BassDD);
            this.groupBox4.Controls.Add(this.chbx_Bonus);
            this.groupBox4.Controls.Add(this.chbx_Cover);
            this.groupBox4.Location = new System.Drawing.Point(652, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox4.Size = new System.Drawing.Size(162, 140);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Quality Checks";
            // 
            // chbx_Is_Acoustic
            // 
            this.chbx_Is_Acoustic.AutoSize = true;
            this.chbx_Is_Acoustic.Location = new System.Drawing.Point(76, 120);
            this.chbx_Is_Acoustic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Is_Acoustic.Name = "chbx_Is_Acoustic";
            this.chbx_Is_Acoustic.Size = new System.Drawing.Size(67, 17);
            this.chbx_Is_Acoustic.TabIndex = 336;
            this.chbx_Is_Acoustic.Text = "Acoustic";
            this.chbx_Is_Acoustic.UseVisualStyleBackColor = true;
            // 
            // chbx_Is_Live
            // 
            this.chbx_Is_Live.AutoSize = true;
            this.chbx_Is_Live.Location = new System.Drawing.Point(6, 120);
            this.chbx_Is_Live.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Is_Live.Name = "chbx_Is_Live";
            this.chbx_Is_Live.Size = new System.Drawing.Size(46, 17);
            this.chbx_Is_Live.TabIndex = 334;
            this.chbx_Is_Live.Text = "Live";
            this.chbx_Is_Live.UseVisualStyleBackColor = true;
            // 
            // btn_EOF
            // 
            this.btn_EOF.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EOF.Location = new System.Drawing.Point(130, 14);
            this.btn_EOF.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_EOF.Name = "btn_EOF";
            this.btn_EOF.Size = new System.Drawing.Size(30, 18);
            this.btn_EOF.TabIndex = 332;
            this.btn_EOF.Text = "EoF";
            this.btn_EOF.UseVisualStyleBackColor = true;
            this.btn_EOF.Click += new System.EventHandler(this.btn_EOF_Click);
            // 
            // btn_CreateLyrics
            // 
            this.btn_CreateLyrics.Enabled = false;
            this.btn_CreateLyrics.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateLyrics.Location = new System.Drawing.Point(138, 102);
            this.btn_CreateLyrics.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_CreateLyrics.Name = "btn_CreateLyrics";
            this.btn_CreateLyrics.Size = new System.Drawing.Size(18, 18);
            this.btn_CreateLyrics.TabIndex = 331;
            this.btn_CreateLyrics.Text = "+";
            this.btn_CreateLyrics.UseVisualStyleBackColor = true;
            this.btn_CreateLyrics.Click += new System.EventHandler(this.btn_CreateLyrics_Click);
            // 
            // chbx_KeepDD
            // 
            this.chbx_KeepDD.AutoSize = true;
            this.chbx_KeepDD.Location = new System.Drawing.Point(142, 37);
            this.chbx_KeepDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_KeepDD.Name = "chbx_KeepDD";
            this.chbx_KeepDD.Size = new System.Drawing.Size(15, 14);
            this.chbx_KeepDD.TabIndex = 330;
            this.chbx_KeepDD.UseVisualStyleBackColor = true;
            // 
            // btn_AddSections
            // 
            this.btn_AddSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddSections.Location = new System.Drawing.Point(138, 84);
            this.btn_AddSections.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_AddSections.Name = "btn_AddSections";
            this.btn_AddSections.Size = new System.Drawing.Size(18, 18);
            this.btn_AddSections.TabIndex = 328;
            this.btn_AddSections.Text = "+";
            this.btn_AddSections.UseVisualStyleBackColor = true;
            this.btn_AddSections.Click += new System.EventHandler(this.btn_AddSections_Click);
            // 
            // chbx_Lyrics
            // 
            this.chbx_Lyrics.AutoSize = true;
            this.chbx_Lyrics.Enabled = false;
            this.chbx_Lyrics.Location = new System.Drawing.Point(76, 104);
            this.chbx_Lyrics.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Lyrics.Name = "chbx_Lyrics";
            this.chbx_Lyrics.Size = new System.Drawing.Size(58, 17);
            this.chbx_Lyrics.TabIndex = 327;
            this.chbx_Lyrics.Text = "Vocals";
            this.chbx_Lyrics.UseVisualStyleBackColor = true;
            // 
            // chbx_TrackNo
            // 
            this.chbx_TrackNo.AutoSize = true;
            this.chbx_TrackNo.Enabled = false;
            this.chbx_TrackNo.Location = new System.Drawing.Point(76, 71);
            this.chbx_TrackNo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_TrackNo.Name = "chbx_TrackNo";
            this.chbx_TrackNo.Size = new System.Drawing.Size(74, 17);
            this.chbx_TrackNo.TabIndex = 326;
            this.chbx_TrackNo.Text = "Track No.";
            this.chbx_TrackNo.UseVisualStyleBackColor = true;
            // 
            // btn_AddDD
            // 
            this.btn_AddDD.Enabled = false;
            this.btn_AddDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddDD.Location = new System.Drawing.Point(118, 34);
            this.btn_AddDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_AddDD.Name = "btn_AddDD";
            this.btn_AddDD.Size = new System.Drawing.Size(18, 18);
            this.btn_AddDD.TabIndex = 316;
            this.btn_AddDD.Text = "+";
            this.btn_AddDD.UseVisualStyleBackColor = true;
            this.btn_AddDD.Click += new System.EventHandler(this.btn_AddDD_Click);
            // 
            // btn_RemoveDD
            // 
            this.btn_RemoveDD.Enabled = false;
            this.btn_RemoveDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveDD.Location = new System.Drawing.Point(50, 34);
            this.btn_RemoveDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_RemoveDD.Name = "btn_RemoveDD";
            this.btn_RemoveDD.Size = new System.Drawing.Size(18, 18);
            this.btn_RemoveDD.TabIndex = 315;
            this.btn_RemoveDD.Text = "-";
            this.btn_RemoveDD.UseVisualStyleBackColor = true;
            this.btn_RemoveDD.Click += new System.EventHandler(this.btn_RemoveDD_Click);
            // 
            // chbx_DD
            // 
            this.chbx_DD.AutoSize = true;
            this.chbx_DD.Enabled = false;
            this.chbx_DD.Location = new System.Drawing.Point(6, 38);
            this.chbx_DD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_DD.Name = "chbx_DD";
            this.chbx_DD.Size = new System.Drawing.Size(42, 17);
            this.chbx_DD.TabIndex = 45;
            this.chbx_DD.Text = "DD";
            this.chbx_DD.UseVisualStyleBackColor = true;
            // 
            // txt_AddDD
            // 
            this.txt_AddDD.Enabled = false;
            this.txt_AddDD.Location = new System.Drawing.Point(72, 36);
            this.txt_AddDD.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_AddDD.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.txt_AddDD.Name = "txt_AddDD";
            this.txt_AddDD.Size = new System.Drawing.Size(42, 20);
            this.txt_AddDD.TabIndex = 325;
            this.txt_AddDD.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // chbx_Original
            // 
            this.chbx_Original.AutoSize = true;
            this.chbx_Original.Location = new System.Drawing.Point(6, 88);
            this.chbx_Original.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Original.Name = "chbx_Original";
            this.chbx_Original.Size = new System.Drawing.Size(58, 17);
            this.chbx_Original.TabIndex = 44;
            this.chbx_Original.Text = "Official";
            this.chbx_Original.UseVisualStyleBackColor = true;
            // 
            // chbx_Sections
            // 
            this.chbx_Sections.AutoSize = true;
            this.chbx_Sections.Enabled = false;
            this.chbx_Sections.Location = new System.Drawing.Point(76, 88);
            this.chbx_Sections.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Sections.Name = "chbx_Sections";
            this.chbx_Sections.Size = new System.Drawing.Size(67, 17);
            this.chbx_Sections.TabIndex = 48;
            this.chbx_Sections.Text = "Sections";
            this.chbx_Sections.UseVisualStyleBackColor = true;
            // 
            // chbx_Preview
            // 
            this.chbx_Preview.AutoSize = true;
            this.chbx_Preview.Enabled = false;
            this.chbx_Preview.Location = new System.Drawing.Point(6, 104);
            this.chbx_Preview.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Preview.Name = "chbx_Preview";
            this.chbx_Preview.Size = new System.Drawing.Size(64, 17);
            this.chbx_Preview.TabIndex = 57;
            this.chbx_Preview.Text = "Preview";
            this.chbx_Preview.UseVisualStyleBackColor = true;
            // 
            // chbx_Author
            // 
            this.chbx_Author.AutoSize = true;
            this.chbx_Author.Enabled = false;
            this.chbx_Author.Location = new System.Drawing.Point(6, 54);
            this.chbx_Author.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Author.Name = "chbx_Author";
            this.chbx_Author.Size = new System.Drawing.Size(57, 17);
            this.chbx_Author.TabIndex = 314;
            this.chbx_Author.Text = "Author";
            this.chbx_Author.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveBassDD
            // 
            this.btn_RemoveBassDD.Enabled = false;
            this.btn_RemoveBassDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveBassDD.Location = new System.Drawing.Point(74, 16);
            this.btn_RemoveBassDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_RemoveBassDD.Name = "btn_RemoveBassDD";
            this.btn_RemoveBassDD.Size = new System.Drawing.Size(18, 18);
            this.btn_RemoveBassDD.TabIndex = 85;
            this.btn_RemoveBassDD.Text = "-";
            this.btn_RemoveBassDD.UseVisualStyleBackColor = true;
            this.btn_RemoveBassDD.Click += new System.EventHandler(this.btn_RemoveBassDD_Click);
            // 
            // chbx_BassDD
            // 
            this.chbx_BassDD.AutoSize = true;
            this.chbx_BassDD.Enabled = false;
            this.chbx_BassDD.Location = new System.Drawing.Point(6, 18);
            this.chbx_BassDD.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_BassDD.Name = "chbx_BassDD";
            this.chbx_BassDD.Size = new System.Drawing.Size(68, 17);
            this.chbx_BassDD.TabIndex = 99;
            this.chbx_BassDD.Text = "Bass DD";
            this.chbx_BassDD.UseVisualStyleBackColor = true;
            // 
            // chbx_Bonus
            // 
            this.chbx_Bonus.AutoSize = true;
            this.chbx_Bonus.Enabled = false;
            this.chbx_Bonus.Location = new System.Drawing.Point(76, 55);
            this.chbx_Bonus.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Bonus.Name = "chbx_Bonus";
            this.chbx_Bonus.Size = new System.Drawing.Size(56, 17);
            this.chbx_Bonus.TabIndex = 101;
            this.chbx_Bonus.Text = "Bonus";
            this.chbx_Bonus.UseVisualStyleBackColor = true;
            // 
            // chbx_Cover
            // 
            this.chbx_Cover.AutoSize = true;
            this.chbx_Cover.Enabled = false;
            this.chbx_Cover.Location = new System.Drawing.Point(6, 71);
            this.chbx_Cover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Cover.Name = "chbx_Cover";
            this.chbx_Cover.Size = new System.Drawing.Size(54, 17);
            this.chbx_Cover.TabIndex = 105;
            this.chbx_Cover.Text = "Cover";
            this.chbx_Cover.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbx_Format_Originals);
            this.groupBox3.Controls.Add(this.chbx_Originals_Available);
            this.groupBox3.Controls.Add(this.btn_CopyOld);
            this.groupBox3.Controls.Add(this.btn_DuplicateFolder);
            this.groupBox3.Controls.Add(this.chbx_Avail_Duplicate);
            this.groupBox3.Controls.Add(this.btn_OldFolder);
            this.groupBox3.Controls.Add(this.chbx_Avail_Old);
            this.groupBox3.Controls.Add(this.chbx_Has_Been_Corrected);
            this.groupBox3.Location = new System.Drawing.Point(698, 146);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Size = new System.Drawing.Size(130, 82);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import Audit Trail";
            // 
            // chbx_Format_Originals
            // 
            this.chbx_Format_Originals.FormattingEnabled = true;
            this.chbx_Format_Originals.Location = new System.Drawing.Point(89, 64);
            this.chbx_Format_Originals.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Format_Originals.Name = "chbx_Format_Originals";
            this.chbx_Format_Originals.Size = new System.Drawing.Size(43, 21);
            this.chbx_Format_Originals.TabIndex = 414;
            // 
            // btn_DuplicateFolder
            // 
            this.btn_DuplicateFolder.Enabled = false;
            this.btn_DuplicateFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DuplicateFolder.Location = new System.Drawing.Point(98, 46);
            this.btn_DuplicateFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_DuplicateFolder.Name = "btn_DuplicateFolder";
            this.btn_DuplicateFolder.Size = new System.Drawing.Size(22, 16);
            this.btn_DuplicateFolder.TabIndex = 327;
            this.btn_DuplicateFolder.Text = "->";
            this.btn_DuplicateFolder.UseVisualStyleBackColor = true;
            this.btn_DuplicateFolder.Click += new System.EventHandler(this.btn_DuplicateFolder_Click);
            // 
            // chbx_Avail_Duplicate
            // 
            this.chbx_Avail_Duplicate.AutoSize = true;
            this.chbx_Avail_Duplicate.Enabled = false;
            this.chbx_Avail_Duplicate.Location = new System.Drawing.Point(6, 47);
            this.chbx_Avail_Duplicate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Avail_Duplicate.Name = "chbx_Avail_Duplicate";
            this.chbx_Avail_Duplicate.Size = new System.Drawing.Size(100, 17);
            this.chbx_Avail_Duplicate.TabIndex = 111;
            this.chbx_Avail_Duplicate.Text = "Duplicate Avail.";
            this.chbx_Avail_Duplicate.UseVisualStyleBackColor = true;
            // 
            // btn_OldFolder
            // 
            this.btn_OldFolder.Enabled = false;
            this.btn_OldFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OldFolder.Location = new System.Drawing.Point(88, 14);
            this.btn_OldFolder.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OldFolder.Name = "btn_OldFolder";
            this.btn_OldFolder.Size = new System.Drawing.Size(22, 14);
            this.btn_OldFolder.TabIndex = 326;
            this.btn_OldFolder.Text = "->";
            this.btn_OldFolder.UseVisualStyleBackColor = true;
            this.btn_OldFolder.Click += new System.EventHandler(this.btn_OldFolder_Click);
            // 
            // chbx_Avail_Old
            // 
            this.chbx_Avail_Old.AutoSize = true;
            this.chbx_Avail_Old.Enabled = false;
            this.chbx_Avail_Old.Location = new System.Drawing.Point(6, 14);
            this.chbx_Avail_Old.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Avail_Old.Name = "chbx_Avail_Old";
            this.chbx_Avail_Old.Size = new System.Drawing.Size(88, 17);
            this.chbx_Avail_Old.TabIndex = 112;
            this.chbx_Avail_Old.Text = "Old Available";
            this.chbx_Avail_Old.UseVisualStyleBackColor = true;
            // 
            // chbx_Has_Been_Corrected
            // 
            this.chbx_Has_Been_Corrected.AutoSize = true;
            this.chbx_Has_Been_Corrected.Enabled = false;
            this.chbx_Has_Been_Corrected.Location = new System.Drawing.Point(6, 30);
            this.chbx_Has_Been_Corrected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Has_Been_Corrected.Name = "chbx_Has_Been_Corrected";
            this.chbx_Has_Been_Corrected.Size = new System.Drawing.Size(122, 17);
            this.chbx_Has_Been_Corrected.TabIndex = 274;
            this.chbx_Has_Been_Corrected.Text = "Has Been Corrected";
            this.chbx_Has_Been_Corrected.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbx_InclGroups);
            this.groupBox2.Controls.Add(this.txt_ID);
            this.groupBox2.Controls.Add(this.chbx_InclBeta);
            this.groupBox2.Controls.Add(this.btn_SelectInverted);
            this.groupBox2.Controls.Add(this.btn_InvertSelect);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txt_MultiTrackType);
            this.groupBox2.Controls.Add(this.chbx_MultiTrack);
            this.groupBox2.Controls.Add(this.btn_SelectAll);
            this.groupBox2.Controls.Add(this.chbx_Broken);
            this.groupBox2.Controls.Add(this.chbx_Selected);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chbx_Beta);
            this.groupBox2.Controls.Add(this.btn_SelectNone);
            this.groupBox2.Controls.Add(this.lbl_NoRec);
            this.groupBox2.Controls.Add(this.btn_NextItem);
            this.groupBox2.Controls.Add(this.cmb_Filter);
            this.groupBox2.Controls.Add(this.btn_Prev);
            this.groupBox2.Location = new System.Drawing.Point(2, 3);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(200, 126);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Song Section";
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(28, 86);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(52, 20);
            this.txt_ID.TabIndex = 95;
            // 
            // btn_SelectInverted
            // 
            this.btn_SelectInverted.Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectInverted.Location = new System.Drawing.Point(80, 68);
            this.btn_SelectInverted.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SelectInverted.Name = "btn_SelectInverted";
            this.btn_SelectInverted.Size = new System.Drawing.Size(46, 16);
            this.btn_SelectInverted.TabIndex = 380;
            this.btn_SelectInverted.Text = "InvertSelect";
            this.btn_SelectInverted.UseVisualStyleBackColor = true;
            this.btn_SelectInverted.Click += new System.EventHandler(this.btn_SelectInverted_Click);
            // 
            // btn_InvertSelect
            // 
            this.btn_InvertSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_InvertSelect.Location = new System.Drawing.Point(40, 50);
            this.btn_InvertSelect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_InvertSelect.Name = "btn_InvertSelect";
            this.btn_InvertSelect.Size = new System.Drawing.Size(40, 34);
            this.btn_InvertSelect.TabIndex = 379;
            this.btn_InvertSelect.Text = "UnSelectFiltered";
            this.btn_InvertSelect.UseVisualStyleBackColor = true;
            this.btn_InvertSelect.Click += new System.EventHandler(this.btn_InvertSelect_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 89);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 378;
            this.label12.Text = "ID :";
            // 
            // txt_MultiTrackType
            // 
            this.txt_MultiTrackType.Enabled = false;
            this.txt_MultiTrackType.FormattingEnabled = true;
            this.txt_MultiTrackType.Items.AddRange(new object[] {
            "",
            "No Bass",
            "No Lead",
            "No Rhythm",
            "No Vocal",
            "(No Guitars)",
            "Only Bass",
            "Only Lead",
            "Only Rhythm",
            "Only Drums",
            "Only Vocal",
            "(Only BackTrack)"});
            this.txt_MultiTrackType.Location = new System.Drawing.Point(108, 104);
            this.txt_MultiTrackType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_MultiTrackType.Name = "txt_MultiTrackType";
            this.txt_MultiTrackType.Size = new System.Drawing.Size(86, 21);
            this.txt_MultiTrackType.TabIndex = 377;
            // 
            // chbx_MultiTrack
            // 
            this.chbx_MultiTrack.AutoSize = true;
            this.chbx_MultiTrack.Location = new System.Drawing.Point(128, 84);
            this.chbx_MultiTrack.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_MultiTrack.Name = "chbx_MultiTrack";
            this.chbx_MultiTrack.Size = new System.Drawing.Size(76, 17);
            this.chbx_MultiTrack.TabIndex = 376;
            this.chbx_MultiTrack.Text = "MultiTrack";
            this.chbx_MultiTrack.UseVisualStyleBackColor = true;
            this.chbx_MultiTrack.CheckStateChanged += new System.EventHandler(this.chbx_MultiTrack_CheckedChanged);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectAll.Location = new System.Drawing.Point(80, 50);
            this.btn_SelectAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(46, 16);
            this.btn_SelectAll.TabIndex = 102;
            this.btn_SelectAll.Text = "SelectFiltered";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.btn_SelectAll_Click);
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Location = new System.Drawing.Point(128, 36);
            this.chbx_Broken.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 53;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // chbx_Selected
            // 
            this.chbx_Selected.AutoSize = true;
            this.chbx_Selected.Location = new System.Drawing.Point(128, 52);
            this.chbx_Selected.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Selected.Name = "chbx_Selected";
            this.chbx_Selected.Size = new System.Drawing.Size(68, 17);
            this.chbx_Selected.TabIndex = 64;
            this.chbx_Selected.Text = "Selected";
            this.chbx_Selected.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 276;
            this.label1.Text = "Filter:";
            // 
            // chbx_Beta
            // 
            this.chbx_Beta.AutoSize = true;
            this.chbx_Beta.Location = new System.Drawing.Point(128, 68);
            this.chbx_Beta.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Beta.Name = "chbx_Beta";
            this.chbx_Beta.Size = new System.Drawing.Size(48, 17);
            this.chbx_Beta.TabIndex = 82;
            this.chbx_Beta.Text = "Beta";
            this.chbx_Beta.UseVisualStyleBackColor = true;
            // 
            // btn_SelectNone
            // 
            this.btn_SelectNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SelectNone.Location = new System.Drawing.Point(4, 50);
            this.btn_SelectNone.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SelectNone.Name = "btn_SelectNone";
            this.btn_SelectNone.Size = new System.Drawing.Size(34, 34);
            this.btn_SelectNone.TabIndex = 320;
            this.btn_SelectNone.Text = "UnSelect All";
            this.btn_SelectNone.UseVisualStyleBackColor = true;
            this.btn_SelectNone.Click += new System.EventHandler(this.btn_SelectNone_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoRec.Location = new System.Drawing.Point(2, 106);
            this.lbl_NoRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(102, 11);
            this.lbl_NoRec.TabIndex = 113;
            this.lbl_NoRec.Text = " Records";
            // 
            // btn_NextItem
            // 
            this.btn_NextItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NextItem.Location = new System.Drawing.Point(96, 86);
            this.btn_NextItem.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_NextItem.Name = "btn_NextItem";
            this.btn_NextItem.Size = new System.Drawing.Size(18, 16);
            this.btn_NextItem.TabIndex = 318;
            this.btn_NextItem.Text = ">";
            this.btn_NextItem.UseVisualStyleBackColor = true;
            this.btn_NextItem.Click += new System.EventHandler(this.btn_NextItem_Click);
            // 
            // cmb_Filter
            // 
            this.cmb_Filter.FormattingEnabled = true;
            this.cmb_Filter.Items.AddRange(new object[] {
            "0ALL",
            "Songs in Rocksmith Lib",
            "Show Songs with FilesMissing Issues",
            "Imported Last",
            "No Cover",
            "No Guitar",
            "No Preview",
            "No Section",
            "No Vocals",
            "No Track No.",
            "No Version",
            "No Author",
            "No Bass DD",
            "No Bass",
            "No DD",
            "With DD",
            "Alternate",
            "Beta",
            "Broken",
            "Selected",
            "With Bonus",
            "Original",
            "CDLC",
            "Drop D",
            "E Standard",
            "Eb Standard",
            "Other Tunings",
            "Live",
            "Acoustic",
            "Pc",
            "PS3",
            "Mac",
            "XBOX360",
            "DLCID diff than Default",
            "Same DLCName",
            "Automatically generated Preview",
            "Any DLCManager generated Preview",
            "With Duplicates",
            "Main_NoOLD",
            "Imported Current Month",
            "Packed Last",
            "Packing Errors"});
            this.cmb_Filter.Location = new System.Drawing.Point(35, 12);
            this.cmb_Filter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmb_Filter.Name = "cmb_Filter";
            this.cmb_Filter.Size = new System.Drawing.Size(158, 21);
            this.cmb_Filter.TabIndex = 275;
            this.cmb_Filter.SelectedIndexChanged += new System.EventHandler(this.cmb_Filter_SelectedValueChanged);
            // 
            // btn_Prev
            // 
            this.btn_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Prev.Location = new System.Drawing.Point(80, 86);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(18, 16);
            this.btn_Prev.TabIndex = 319;
            this.btn_Prev.Text = "<";
            this.btn_Prev.UseVisualStyleBackColor = true;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Volume);
            this.groupBox1.Controls.Add(this.btn_GarageBand);
            this.groupBox1.Controls.Add(this.txt_Preview_Volume);
            this.groupBox1.Controls.Add(this.btn_AddPreview);
            this.groupBox1.Controls.Add(this.txt_PreviewStart);
            this.groupBox1.Controls.Add(this.btn_PlayAudio);
            this.groupBox1.Controls.Add(this.btn_PlayPreview);
            this.groupBox1.Controls.Add(this.txt_AverageTempo);
            this.groupBox1.Controls.Add(this.chbx_AutoPlay);
            this.groupBox1.Controls.Add(this.txt_OggPath);
            this.groupBox1.Controls.Add(this.txt_PreviewEnd);
            this.groupBox1.Controls.Add(this.txt_OggPreviewPath);
            this.groupBox1.Controls.Add(this.btn_SelectPreview);
            this.groupBox1.Location = new System.Drawing.Point(392, 135);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(303, 76);
            this.groupBox1.TabIndex = 325;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Section";
            // 
            // txt_Volume
            // 
            this.txt_Volume.DecimalPlaces = 1;
            this.txt_Volume.Location = new System.Drawing.Point(74, 16);
            this.txt_Volume.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Volume.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_Volume.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.txt_Volume.Name = "txt_Volume";
            this.txt_Volume.Size = new System.Drawing.Size(42, 20);
            this.txt_Volume.TabIndex = 324;
            // 
            // txt_Preview_Volume
            // 
            this.txt_Preview_Volume.DecimalPlaces = 1;
            this.txt_Preview_Volume.Location = new System.Drawing.Point(182, 16);
            this.txt_Preview_Volume.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Preview_Volume.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_Preview_Volume.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.txt_Preview_Volume.Name = "txt_Preview_Volume";
            this.txt_Preview_Volume.Size = new System.Drawing.Size(38, 20);
            this.txt_Preview_Volume.TabIndex = 323;
            // 
            // btn_AddPreview
            // 
            this.btn_AddPreview.Enabled = false;
            this.btn_AddPreview.Location = new System.Drawing.Point(206, 54);
            this.btn_AddPreview.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_AddPreview.Name = "btn_AddPreview";
            this.btn_AddPreview.Size = new System.Drawing.Size(82, 20);
            this.btn_AddPreview.TabIndex = 94;
            this.btn_AddPreview.Text = "Add Preview";
            this.btn_AddPreview.UseVisualStyleBackColor = true;
            this.btn_AddPreview.Click += new System.EventHandler(this.btn_AddPreview_Click);
            // 
            // txt_PreviewStart
            // 
            this.txt_PreviewStart.CustomFormat = "mm:ss";
            this.txt_PreviewStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_PreviewStart.Location = new System.Drawing.Point(114, 36);
            this.txt_PreviewStart.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_PreviewStart.Name = "txt_PreviewStart";
            this.txt_PreviewStart.ShowUpDown = true;
            this.txt_PreviewStart.Size = new System.Drawing.Size(62, 20);
            this.txt_PreviewStart.TabIndex = 322;
            this.txt_PreviewStart.Value = new System.DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // btn_PlayAudio
            // 
            this.btn_PlayAudio.Location = new System.Drawing.Point(6, 14);
            this.btn_PlayAudio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_PlayAudio.Name = "btn_PlayAudio";
            this.btn_PlayAudio.Size = new System.Drawing.Size(66, 20);
            this.btn_PlayAudio.TabIndex = 87;
            this.btn_PlayAudio.Text = "Play Audio";
            this.btn_PlayAudio.UseVisualStyleBackColor = true;
            this.btn_PlayAudio.Click += new System.EventHandler(this.btn_PlaySong);
            // 
            // btn_PlayPreview
            // 
            this.btn_PlayPreview.Enabled = false;
            this.btn_PlayPreview.Location = new System.Drawing.Point(118, 14);
            this.btn_PlayPreview.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_PlayPreview.Name = "btn_PlayPreview";
            this.btn_PlayPreview.Size = new System.Drawing.Size(62, 20);
            this.btn_PlayPreview.TabIndex = 88;
            this.btn_PlayPreview.Text = "Preview";
            this.btn_PlayPreview.UseVisualStyleBackColor = true;
            this.btn_PlayPreview.Click += new System.EventHandler(this.btn_PlayPreview_Click);
            // 
            // txt_AverageTempo
            // 
            this.txt_AverageTempo.Cue = "Avg. Tempo";
            this.txt_AverageTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AverageTempo.ForeColor = System.Drawing.Color.Gray;
            this.txt_AverageTempo.Location = new System.Drawing.Point(220, 16);
            this.txt_AverageTempo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_AverageTempo.Name = "txt_AverageTempo";
            this.txt_AverageTempo.Size = new System.Drawing.Size(34, 20);
            this.txt_AverageTempo.TabIndex = 90;
            // 
            // chbx_AutoPlay
            // 
            this.chbx_AutoPlay.AutoSize = true;
            this.chbx_AutoPlay.Checked = true;
            this.chbx_AutoPlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoPlay.Enabled = false;
            this.chbx_AutoPlay.Location = new System.Drawing.Point(218, 36);
            this.chbx_AutoPlay.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_AutoPlay.Name = "chbx_AutoPlay";
            this.chbx_AutoPlay.Size = new System.Drawing.Size(68, 17);
            this.chbx_AutoPlay.TabIndex = 291;
            this.chbx_AutoPlay.Text = "AutoPlay";
            this.chbx_AutoPlay.UseVisualStyleBackColor = true;
            // 
            // txt_OggPath
            // 
            this.txt_OggPath.Cue = "Ogg Path";
            this.txt_OggPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_OggPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_OggPath.HideSelection = false;
            this.txt_OggPath.Location = new System.Drawing.Point(6, 38);
            this.txt_OggPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_OggPath.Name = "txt_OggPath";
            this.txt_OggPath.ReadOnly = true;
            this.txt_OggPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_OggPath.Size = new System.Drawing.Size(98, 20);
            this.txt_OggPath.TabIndex = 312;
            this.txt_OggPath.Visible = false;
            // 
            // txt_PreviewEnd
            // 
            this.txt_PreviewEnd.Location = new System.Drawing.Point(178, 36);
            this.txt_PreviewEnd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_PreviewEnd.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txt_PreviewEnd.Name = "txt_PreviewEnd";
            this.txt_PreviewEnd.Size = new System.Drawing.Size(38, 20);
            this.txt_PreviewEnd.TabIndex = 316;
            this.txt_PreviewEnd.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // txt_OggPreviewPath
            // 
            this.txt_OggPreviewPath.Cue = "Ogg Preview Path";
            this.txt_OggPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_OggPreviewPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_OggPreviewPath.HideSelection = false;
            this.txt_OggPreviewPath.Location = new System.Drawing.Point(6, 56);
            this.txt_OggPreviewPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_OggPreviewPath.Name = "txt_OggPreviewPath";
            this.txt_OggPreviewPath.ReadOnly = true;
            this.txt_OggPreviewPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_OggPreviewPath.Size = new System.Drawing.Size(98, 20);
            this.txt_OggPreviewPath.TabIndex = 313;
            this.txt_OggPreviewPath.Visible = false;
            // 
            // btn_SelectPreview
            // 
            this.btn_SelectPreview.Enabled = false;
            this.btn_SelectPreview.Location = new System.Drawing.Point(108, 54);
            this.btn_SelectPreview.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SelectPreview.Name = "btn_SelectPreview";
            this.btn_SelectPreview.Size = new System.Drawing.Size(96, 20);
            this.btn_SelectPreview.TabIndex = 315;
            this.btn_SelectPreview.Text = "Change Preview";
            this.btn_SelectPreview.UseVisualStyleBackColor = true;
            this.btn_SelectPreview.Click += new System.EventHandler(this.btn_SelectPreview_Click);
            // 
            // btn_OpenRetail
            // 
            this.btn_OpenRetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_OpenRetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenRetail.Location = new System.Drawing.Point(136, 206);
            this.btn_OpenRetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OpenRetail.Name = "btn_OpenRetail";
            this.btn_OpenRetail.Size = new System.Drawing.Size(66, 21);
            this.btn_OpenRetail.TabIndex = 278;
            this.btn_OpenRetail.Text = "Open Retail DB";
            this.btn_OpenRetail.UseVisualStyleBackColor = false;
            this.btn_OpenRetail.Click += new System.EventHandler(this.btn_OpenCache);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1026, 82);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(80, 20);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_OpenStandardization
            // 
            this.btn_OpenStandardization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_OpenStandardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenStandardization.Location = new System.Drawing.Point(10, 206);
            this.btn_OpenStandardization.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OpenStandardization.Name = "btn_OpenStandardization";
            this.btn_OpenStandardization.Size = new System.Drawing.Size(122, 21);
            this.btn_OpenStandardization.TabIndex = 110;
            this.btn_OpenStandardization.Text = "Open Standarization DB";
            this.btn_OpenStandardization.UseVisualStyleBackColor = false;
            this.btn_OpenStandardization.Click += new System.EventHandler(this.btn_OpenStandardization_Click);
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.HideSelection = false;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(832, 135);
            this.txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.ReadOnly = true;
            this.txt_AlbumArtPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(114, 20);
            this.txt_AlbumArtPath.TabIndex = 107;
            this.txt_AlbumArtPath.Visible = false;
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Enabled = false;
            this.btn_ChangeCover.Location = new System.Drawing.Point(832, 158);
            this.btn_ChangeCover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(114, 21);
            this.btn_ChangeCover.TabIndex = 106;
            this.btn_ChangeCover.Text = "Change Cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            this.btn_ChangeCover.Click += new System.EventHandler(this.btn_ChangeCover_Click);
            // 
            // txt_Artist_ShortName
            // 
            this.txt_Artist_ShortName.Cue = "Short";
            this.txt_Artist_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_ShortName.Location = new System.Drawing.Point(434, 3);
            this.txt_Artist_ShortName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist_ShortName.Name = "txt_Artist_ShortName";
            this.txt_Artist_ShortName.Size = new System.Drawing.Size(30, 20);
            this.txt_Artist_ShortName.TabIndex = 104;
            // 
            // txt_Album_ShortName
            // 
            this.txt_Album_ShortName.Cue = "Short";
            this.txt_Album_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_ShortName.Location = new System.Drawing.Point(366, 90);
            this.txt_Album_ShortName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album_ShortName.Name = "txt_Album_ShortName";
            this.txt_Album_ShortName.Size = new System.Drawing.Size(42, 20);
            this.txt_Album_ShortName.TabIndex = 103;
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(956, 86);
            this.chbx_AutoSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(73, 17);
            this.chbx_AutoSave.TabIndex = 100;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            // 
            // txt_Album_Year
            // 
            this.txt_Album_Year.Cue = "Year";
            this.txt_Album_Year.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Year.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Year.Location = new System.Drawing.Point(434, 90);
            this.txt_Album_Year.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album_Year.Name = "txt_Album_Year";
            this.txt_Album_Year.Size = new System.Drawing.Size(52, 20);
            this.txt_Album_Year.TabIndex = 98;
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(818, 8);
            this.picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(128, 124);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 91;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Enabled = false;
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(1026, 57);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 25);
            this.btn_Save.TabIndex = 81;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_APP_ID
            // 
            this.txt_APP_ID.Cue = "App ID";
            this.txt_APP_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_APP_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_APP_ID.Location = new System.Drawing.Point(366, 112);
            this.txt_APP_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_APP_ID.Name = "txt_APP_ID";
            this.txt_APP_ID.Size = new System.Drawing.Size(62, 20);
            this.txt_APP_ID.TabIndex = 80;
            // 
            // txt_DLC_ID
            // 
            this.txt_DLC_ID.Cue = "DLC Name";
            this.txt_DLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLC_ID.Location = new System.Drawing.Point(206, 112);
            this.txt_DLC_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_DLC_ID.Name = "txt_DLC_ID";
            this.txt_DLC_ID.Size = new System.Drawing.Size(156, 20);
            this.txt_DLC_ID.TabIndex = 79;
            // 
            // txt_Version
            // 
            this.txt_Version.Cue = "Version";
            this.txt_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Version.ForeColor = System.Drawing.Color.Gray;
            this.txt_Version.Location = new System.Drawing.Point(504, 45);
            this.txt_Version.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(42, 20);
            this.txt_Version.TabIndex = 73;
            // 
            // txt_Author
            // 
            this.txt_Author.Cue = "Author";
            this.txt_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Author.ForeColor = System.Drawing.Color.Gray;
            this.txt_Author.Location = new System.Drawing.Point(490, 24);
            this.txt_Author.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Author.Name = "txt_Author";
            this.txt_Author.Size = new System.Drawing.Size(156, 20);
            this.txt_Author.TabIndex = 72;
            this.txt_Author.TextChanged += new System.EventHandler(this.txt_Author_TextChanged);
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(206, 90);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(156, 20);
            this.txt_Album.TabIndex = 69;
            this.txt_Album.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Artist_KeyPress);
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Cue = "Title Sort";
            this.txt_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title_Sort.Location = new System.Drawing.Point(206, 70);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(202, 20);
            this.txt_Title_Sort.TabIndex = 68;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(206, 48);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(202, 20);
            this.txt_Title.TabIndex = 67;
            this.txt_Title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Artist_KeyPress);
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Cue = "Artist  Sort";
            this.txt_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Sort.Location = new System.Drawing.Point(206, 26);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist_Sort.TabIndex = 66;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(206, 3);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(202, 20);
            this.txt_Artist.TabIndex = 65;
            this.txt_Artist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Artist_KeyPress);
            // 
            // btn_Duplicate
            // 
            this.btn_Duplicate.Enabled = false;
            this.btn_Duplicate.Location = new System.Drawing.Point(1046, 3);
            this.btn_Duplicate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Duplicate.Name = "btn_Duplicate";
            this.btn_Duplicate.Size = new System.Drawing.Size(55, 25);
            this.btn_Duplicate.TabIndex = 58;
            this.btn_Duplicate.Text = "Duplicate";
            this.btn_Duplicate.UseVisualStyleBackColor = true;
            this.btn_Duplicate.Click += new System.EventHandler(this.btn_Duplicate_Click);
            // 
            // btn_SearchReset
            // 
            this.btn_SearchReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SearchReset.Location = new System.Drawing.Point(434, 60);
            this.btn_SearchReset.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SearchReset.Name = "btn_SearchReset";
            this.btn_SearchReset.Size = new System.Drawing.Size(50, 28);
            this.btn_SearchReset.TabIndex = 56;
            this.btn_SearchReset.Text = "Reset";
            this.btn_SearchReset.UseVisualStyleBackColor = true;
            this.btn_SearchReset.Click += new System.EventHandler(this.btn_SearchReset_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(434, 27);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(50, 28);
            this.btn_Search.TabIndex = 51;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // chbx_Alternate
            // 
            this.chbx_Alternate.AutoSize = true;
            this.chbx_Alternate.Location = new System.Drawing.Point(550, 48);
            this.chbx_Alternate.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Alternate.Name = "chbx_Alternate";
            this.chbx_Alternate.Size = new System.Drawing.Size(68, 17);
            this.chbx_Alternate.TabIndex = 46;
            this.chbx_Alternate.Text = "Alternate";
            this.chbx_Alternate.UseVisualStyleBackColor = true;
            this.chbx_Alternate.CheckedChanged += new System.EventHandler(this.chbx_Alternate_CheckStateChanged);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Location = new System.Drawing.Point(950, 57);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(72, 25);
            this.btn_Delete.TabIndex = 40;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_OpenDB
            // 
            this.btn_OpenDB.Location = new System.Drawing.Point(136, 157);
            this.btn_OpenDB.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_OpenDB.Name = "btn_OpenDB";
            this.btn_OpenDB.Size = new System.Drawing.Size(66, 49);
            this.btn_OpenDB.TabIndex = 37;
            this.btn_OpenDB.Text = "Open DB in M$ Access";
            this.btn_OpenDB.UseVisualStyleBackColor = true;
            this.btn_OpenDB.Click += new System.EventHandler(this.btn_OpenDB_Click);
            // 
            // btn_Tones
            // 
            this.btn_Tones.Location = new System.Drawing.Point(10, 182);
            this.btn_Tones.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Tones.Name = "btn_Tones";
            this.btn_Tones.Size = new System.Drawing.Size(122, 22);
            this.btn_Tones.TabIndex = 36;
            this.btn_Tones.Text = "Open Tones";
            this.btn_Tones.UseVisualStyleBackColor = true;
            this.btn_Tones.Click += new System.EventHandler(this.btn_Tones_Click);
            // 
            // btn_Arrangements
            // 
            this.btn_Arrangements.Location = new System.Drawing.Point(10, 157);
            this.btn_Arrangements.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Arrangements.Name = "btn_Arrangements";
            this.btn_Arrangements.Size = new System.Drawing.Size(122, 25);
            this.btn_Arrangements.TabIndex = 35;
            this.btn_Arrangements.Text = "Open Arrangements";
            this.btn_Arrangements.UseVisualStyleBackColor = true;
            this.btn_Arrangements.Click += new System.EventHandler(this.btn_Arrangements_Click);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Location = new System.Drawing.Point(-154, 92);
            this.CheckBox1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(139, 17);
            this.CheckBox1.TabIndex = 34;
            this.CheckBox1.Text = "Show only MessageBox";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(548, -25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 277;
            this.label2.Text = "Description:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(1106, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CustomForge";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_Beta);
            this.groupBox7.Controls.Add(this.txt_CustomForge_Vwersion);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.txt_CustomsForge_ReleaseNotes);
            this.groupBox7.Controls.Add(this.btn_Playthrough);
            this.groupBox7.Controls.Add(this.txt_Playthough);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.btn_Like);
            this.groupBox7.Controls.Add(this.txt_Followers);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.txt_CustomsForge_Like);
            this.groupBox7.Controls.Add(this.btn_Followers);
            this.groupBox7.Controls.Add(this.btn_CustomForge_Link);
            this.groupBox7.Controls.Add(this.txt_debug);
            this.groupBox7.Controls.Add(this.btn_Youtube);
            this.groupBox7.Controls.Add(this.label59);
            this.groupBox7.Controls.Add(this.btn_Debug);
            this.groupBox7.Controls.Add(this.txt_YouTube_Link);
            this.groupBox7.Controls.Add(this.txt_CustomsForge_Link);
            this.groupBox7.Controls.Add(this.lbfl_YouTube_Link);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Location = new System.Drawing.Point(2, 5);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox7.Size = new System.Drawing.Size(1094, 243);
            this.groupBox7.TabIndex = 382;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "CustomsForge Details";
            // 
            // txt_CustomForge_Vwersion
            // 
            this.txt_CustomForge_Vwersion.Location = new System.Drawing.Point(86, 118);
            this.txt_CustomForge_Vwersion.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_CustomForge_Vwersion.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txt_CustomForge_Vwersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_CustomForge_Vwersion.Name = "txt_CustomForge_Vwersion";
            this.txt_CustomForge_Vwersion.Size = new System.Drawing.Size(42, 20);
            this.txt_CustomForge_Vwersion.TabIndex = 390;
            this.txt_CustomForge_Vwersion.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 120);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 389;
            this.label11.Text = "Version";
            // 
            // txt_CustomsForge_ReleaseNotes
            // 
            this.txt_CustomsForge_ReleaseNotes.Location = new System.Drawing.Point(228, 11);
            this.txt_CustomsForge_ReleaseNotes.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_CustomsForge_ReleaseNotes.Name = "txt_CustomsForge_ReleaseNotes";
            this.txt_CustomsForge_ReleaseNotes.Size = new System.Drawing.Size(110, 24);
            this.txt_CustomsForge_ReleaseNotes.TabIndex = 382;
            this.txt_CustomsForge_ReleaseNotes.Text = "";
            // 
            // btn_Playthrough
            // 
            this.btn_Playthrough.Enabled = false;
            this.btn_Playthrough.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Playthrough.Location = new System.Drawing.Point(128, 40);
            this.btn_Playthrough.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Playthrough.Name = "btn_Playthrough";
            this.btn_Playthrough.Size = new System.Drawing.Size(18, 16);
            this.btn_Playthrough.TabIndex = 388;
            this.btn_Playthrough.Text = ">";
            this.btn_Playthrough.UseVisualStyleBackColor = true;
            // 
            // txt_Playthough
            // 
            this.txt_Playthough.Cue = "PlayThrough";
            this.txt_Playthough.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Playthough.ForeColor = System.Drawing.Color.Gray;
            this.txt_Playthough.Location = new System.Drawing.Point(82, 38);
            this.txt_Playthough.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Playthough.Name = "txt_Playthough";
            this.txt_Playthough.Size = new System.Drawing.Size(40, 20);
            this.txt_Playthough.TabIndex = 386;
            this.txt_Playthough.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 41);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 387;
            this.label10.Text = "Playthrough";
            // 
            // btn_Like
            // 
            this.btn_Like.Enabled = false;
            this.btn_Like.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Like.Location = new System.Drawing.Point(128, 78);
            this.btn_Like.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Like.Name = "btn_Like";
            this.btn_Like.Size = new System.Drawing.Size(18, 16);
            this.btn_Like.TabIndex = 385;
            this.btn_Like.Text = ">";
            this.btn_Like.UseVisualStyleBackColor = true;
            // 
            // txt_Followers
            // 
            this.txt_Followers.Location = new System.Drawing.Point(88, 96);
            this.txt_Followers.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Followers.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txt_Followers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Followers.Name = "txt_Followers";
            this.txt_Followers.Size = new System.Drawing.Size(42, 20);
            this.txt_Followers.TabIndex = 384;
            this.txt_Followers.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 383;
            this.label9.Text = "Followers";
            // 
            // txt_CustomsForge_Like
            // 
            this.txt_CustomsForge_Like.Location = new System.Drawing.Point(88, 76);
            this.txt_CustomsForge_Like.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_CustomsForge_Like.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txt_CustomsForge_Like.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_CustomsForge_Like.Name = "txt_CustomsForge_Like";
            this.txt_CustomsForge_Like.Size = new System.Drawing.Size(34, 20);
            this.txt_CustomsForge_Like.TabIndex = 382;
            this.txt_CustomsForge_Like.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // btn_Followers
            // 
            this.btn_Followers.Enabled = false;
            this.btn_Followers.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Followers.Location = new System.Drawing.Point(130, 96);
            this.btn_Followers.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Followers.Name = "btn_Followers";
            this.btn_Followers.Size = new System.Drawing.Size(18, 16);
            this.btn_Followers.TabIndex = 368;
            this.btn_Followers.Text = ">";
            this.btn_Followers.UseVisualStyleBackColor = true;
            // 
            // btn_CustomForge_Link
            // 
            this.btn_CustomForge_Link.Enabled = false;
            this.btn_CustomForge_Link.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CustomForge_Link.Location = new System.Drawing.Point(128, 59);
            this.btn_CustomForge_Link.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_CustomForge_Link.Name = "btn_CustomForge_Link";
            this.btn_CustomForge_Link.Size = new System.Drawing.Size(18, 16);
            this.btn_CustomForge_Link.TabIndex = 367;
            this.btn_CustomForge_Link.Text = ">";
            this.btn_CustomForge_Link.UseVisualStyleBackColor = true;
            this.btn_CustomForge_Link.Click += new System.EventHandler(this.btn_CustomForge_Link_Click);
            // 
            // txt_debug
            // 
            this.txt_debug.Location = new System.Drawing.Point(164, 59);
            this.txt_debug.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_debug.Name = "txt_debug";
            this.txt_debug.Size = new System.Drawing.Size(246, 180);
            this.txt_debug.TabIndex = 328;
            this.txt_debug.Text = "";
            this.txt_debug.Visible = false;
            // 
            // btn_Youtube
            // 
            this.btn_Youtube.Enabled = false;
            this.btn_Youtube.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Youtube.Location = new System.Drawing.Point(128, 18);
            this.btn_Youtube.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btn_Youtube.Name = "btn_Youtube";
            this.btn_Youtube.Size = new System.Drawing.Size(18, 16);
            this.btn_Youtube.TabIndex = 366;
            this.btn_Youtube.Text = ">";
            this.btn_Youtube.UseVisualStyleBackColor = true;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(152, 18);
            this.label59.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(77, 13);
            this.label59.TabIndex = 328;
            this.label59.Text = "Release Notes";
            // 
            // btn_Debug
            // 
            this.btn_Debug.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_Debug.Location = new System.Drawing.Point(156, 34);
            this.btn_Debug.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Debug.Name = "btn_Debug";
            this.btn_Debug.Size = new System.Drawing.Size(58, 22);
            this.btn_Debug.TabIndex = 326;
            this.btn_Debug.Text = "DEBUG (Get Track No)";
            this.btn_Debug.UseVisualStyleBackColor = true;
            this.btn_Debug.Visible = false;
            this.btn_Debug.Click += new System.EventHandler(this.btn_Debug_Click);
            // 
            // txt_YouTube_Link
            // 
            this.txt_YouTube_Link.Cue = "YouTube Link New";
            this.txt_YouTube_Link.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_YouTube_Link.ForeColor = System.Drawing.Color.Gray;
            this.txt_YouTube_Link.Location = new System.Drawing.Point(82, 16);
            this.txt_YouTube_Link.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_YouTube_Link.Name = "txt_YouTube_Link";
            this.txt_YouTube_Link.Size = new System.Drawing.Size(40, 20);
            this.txt_YouTube_Link.TabIndex = 314;
            this.txt_YouTube_Link.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_CustomsForge_Link
            // 
            this.txt_CustomsForge_Link.Cue = "CustomsForge New";
            this.txt_CustomsForge_Link.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_Link.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_Link.Location = new System.Drawing.Point(78, 57);
            this.txt_CustomsForge_Link.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_CustomsForge_Link.Name = "txt_CustomsForge_Link";
            this.txt_CustomsForge_Link.Size = new System.Drawing.Size(46, 20);
            this.txt_CustomsForge_Link.TabIndex = 316;
            this.txt_CustomsForge_Link.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbfl_YouTube_Link
            // 
            this.lbfl_YouTube_Link.AutoSize = true;
            this.lbfl_YouTube_Link.Location = new System.Drawing.Point(26, 18);
            this.lbfl_YouTube_Link.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbfl_YouTube_Link.Name = "lbfl_YouTube_Link";
            this.lbfl_YouTube_Link.Size = new System.Drawing.Size(51, 13);
            this.lbfl_YouTube_Link.TabIndex = 322;
            this.lbfl_YouTube_Link.Text = "YouTube";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 60);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(74, 13);
            this.label33.TabIndex = 321;
            this.label33.Text = "CustomsForge";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(60, 78);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 13);
            this.label32.TabIndex = 320;
            this.label32.Text = "Like";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chbx_Duplicate_Official);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.chbx_Ignore_Officials);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.btn_Remove_All_Packed);
            this.tabPage3.Controls.Add(this.cmb_Packed);
            this.tabPage3.Controls.Add(this.txt_CoundofPacked);
            this.tabPage3.Controls.Add(this.btn_Remove_Packed);
            this.tabPage3.Controls.Add(this.btn_RemoveAllRemoteSongs);
            this.tabPage3.Controls.Add(this.btn_RemoveRemoteSong);
            this.tabPage3.Controls.Add(this.txt_RemotePath);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Size = new System.Drawing.Size(1106, 248);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GameData";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chbx_Ignore_Officials
            // 
            this.chbx_Ignore_Officials.AutoSize = true;
            this.chbx_Ignore_Officials.Checked = true;
            this.chbx_Ignore_Officials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Ignore_Officials.Location = new System.Drawing.Point(438, 88);
            this.chbx_Ignore_Officials.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chbx_Ignore_Officials.Name = "chbx_Ignore_Officials";
            this.chbx_Ignore_Officials.Size = new System.Drawing.Size(149, 17);
            this.chbx_Ignore_Officials.TabIndex = 344;
            this.chbx_Ignore_Officials.Text = "Ignore Officials Duplicates";
            this.chbx_Ignore_Officials.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(294, 84);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 20);
            this.button2.TabIndex = 343;
            this.button2.Text = "Remove All Duplicates";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Remove_All_Packed
            // 
            this.btn_Remove_All_Packed.Location = new System.Drawing.Point(150, 84);
            this.btn_Remove_All_Packed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Remove_All_Packed.Name = "btn_Remove_All_Packed";
            this.btn_Remove_All_Packed.Size = new System.Drawing.Size(140, 20);
            this.btn_Remove_All_Packed.TabIndex = 342;
            this.btn_Remove_All_Packed.Text = "Remove All Packed Songs";
            this.btn_Remove_All_Packed.UseVisualStyleBackColor = true;
            this.btn_Remove_All_Packed.Click += new System.EventHandler(this.btn_Remove_All_Packed_Click);
            // 
            // cmb_Packed
            // 
            this.cmb_Packed.Enabled = false;
            this.cmb_Packed.FormattingEnabled = true;
            this.cmb_Packed.Location = new System.Drawing.Point(186, 60);
            this.cmb_Packed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmb_Packed.Name = "cmb_Packed";
            this.cmb_Packed.Size = new System.Drawing.Size(414, 21);
            this.cmb_Packed.TabIndex = 341;
            this.cmb_Packed.SelectedValueChanged += new System.EventHandler(this.cmb_Packed_SelectedValueChanged);
            // 
            // txt_CoundofPacked
            // 
            this.txt_CoundofPacked.Location = new System.Drawing.Point(150, 60);
            this.txt_CoundofPacked.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_CoundofPacked.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_CoundofPacked.Name = "txt_CoundofPacked";
            this.txt_CoundofPacked.Size = new System.Drawing.Size(32, 20);
            this.txt_CoundofPacked.TabIndex = 340;
            this.txt_CoundofPacked.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // btn_Remove_Packed
            // 
            this.btn_Remove_Packed.Enabled = false;
            this.btn_Remove_Packed.Location = new System.Drawing.Point(4, 59);
            this.btn_Remove_Packed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Remove_Packed.Name = "btn_Remove_Packed";
            this.btn_Remove_Packed.Size = new System.Drawing.Size(140, 45);
            this.btn_Remove_Packed.TabIndex = 338;
            this.btn_Remove_Packed.Text = "Remove All but the Curently Selected Packed Song";
            this.btn_Remove_Packed.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveAllRemoteSongs
            // 
            this.btn_RemoveAllRemoteSongs.Location = new System.Drawing.Point(4, 31);
            this.btn_RemoveAllRemoteSongs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_RemoveAllRemoteSongs.Name = "btn_RemoveAllRemoteSongs";
            this.btn_RemoveAllRemoteSongs.Size = new System.Drawing.Size(140, 20);
            this.btn_RemoveAllRemoteSongs.TabIndex = 333;
            this.btn_RemoveAllRemoteSongs.Text = "Remove All Remote Songs";
            this.btn_RemoveAllRemoteSongs.UseVisualStyleBackColor = true;
            this.btn_RemoveAllRemoteSongs.Click += new System.EventHandler(this.btn_RemoveAllRemoteSongs_Click);
            // 
            // btn_RemoveRemoteSong
            // 
            this.btn_RemoveRemoteSong.Location = new System.Drawing.Point(4, 5);
            this.btn_RemoveRemoteSong.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_RemoveRemoteSong.Name = "btn_RemoveRemoteSong";
            this.btn_RemoveRemoteSong.Size = new System.Drawing.Size(140, 20);
            this.btn_RemoveRemoteSong.TabIndex = 332;
            this.btn_RemoveRemoteSong.Text = "Remove Remote Song";
            this.btn_RemoveRemoteSong.UseVisualStyleBackColor = true;
            this.btn_RemoveRemoteSong.Click += new System.EventHandler(this.btn_RemoveRemoteSong_Click);
            // 
            // txt_RemotePath
            // 
            this.txt_RemotePath.Cue = "Remote Path";
            this.txt_RemotePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_RemotePath.ForeColor = System.Drawing.Color.Gray;
            this.txt_RemotePath.HideSelection = false;
            this.txt_RemotePath.Location = new System.Drawing.Point(150, 7);
            this.txt_RemotePath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_RemotePath.Name = "txt_RemotePath";
            this.txt_RemotePath.ReadOnly = true;
            this.txt_RemotePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_RemotePath.Size = new System.Drawing.Size(452, 20);
            this.txt_RemotePath.TabIndex = 335;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txt_Spotify_Album_URL);
            this.tabPage4.Controls.Add(this.txt_Spotify_Album_ID);
            this.tabPage4.Controls.Add(this.txt_Spotify_Artist_ID);
            this.tabPage4.Controls.Add(this.txt_Spotify_Song_ID);
            this.tabPage4.Controls.Add(this.txt_SpotifyStatus);
            this.tabPage4.Controls.Add(this.picbx_SpotifyCover);
            this.tabPage4.Controls.Add(this.btn_GetTrack);
            this.tabPage4.Controls.Add(this.txt_SavedPlaylists);
            this.tabPage4.Controls.Add(this.txt_SavedTracks);
            this.tabPage4.Controls.Add(this.txt_SpotifyLog);
            this.tabPage4.Controls.Add(this.btn_ActivateSpotify);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage4.Size = new System.Drawing.Size(1106, 248);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Spotify";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txt_Spotify_Album_URL
            // 
            this.txt_Spotify_Album_URL.Cue = "Spotify Album URL";
            this.txt_Spotify_Album_URL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Spotify_Album_URL.ForeColor = System.Drawing.Color.Gray;
            this.txt_Spotify_Album_URL.HideSelection = false;
            this.txt_Spotify_Album_URL.Location = new System.Drawing.Point(842, 178);
            this.txt_Spotify_Album_URL.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Spotify_Album_URL.Name = "txt_Spotify_Album_URL";
            this.txt_Spotify_Album_URL.ReadOnly = true;
            this.txt_Spotify_Album_URL.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Spotify_Album_URL.Size = new System.Drawing.Size(207, 20);
            this.txt_Spotify_Album_URL.TabIndex = 404;
            // 
            // txt_Spotify_Album_ID
            // 
            this.txt_Spotify_Album_ID.Cue = "Spotify Album ID";
            this.txt_Spotify_Album_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Spotify_Album_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_Spotify_Album_ID.HideSelection = false;
            this.txt_Spotify_Album_ID.Location = new System.Drawing.Point(842, 156);
            this.txt_Spotify_Album_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Spotify_Album_ID.Name = "txt_Spotify_Album_ID";
            this.txt_Spotify_Album_ID.ReadOnly = true;
            this.txt_Spotify_Album_ID.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Spotify_Album_ID.Size = new System.Drawing.Size(207, 20);
            this.txt_Spotify_Album_ID.TabIndex = 403;
            // 
            // txt_Spotify_Artist_ID
            // 
            this.txt_Spotify_Artist_ID.Cue = "Spotify Artist ID";
            this.txt_Spotify_Artist_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Spotify_Artist_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_Spotify_Artist_ID.HideSelection = false;
            this.txt_Spotify_Artist_ID.Location = new System.Drawing.Point(842, 134);
            this.txt_Spotify_Artist_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Spotify_Artist_ID.Name = "txt_Spotify_Artist_ID";
            this.txt_Spotify_Artist_ID.ReadOnly = true;
            this.txt_Spotify_Artist_ID.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Spotify_Artist_ID.Size = new System.Drawing.Size(207, 20);
            this.txt_Spotify_Artist_ID.TabIndex = 402;
            // 
            // txt_Spotify_Song_ID
            // 
            this.txt_Spotify_Song_ID.Cue = "Spotify Song ID";
            this.txt_Spotify_Song_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Spotify_Song_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_Spotify_Song_ID.HideSelection = false;
            this.txt_Spotify_Song_ID.Location = new System.Drawing.Point(842, 112);
            this.txt_Spotify_Song_ID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Spotify_Song_ID.Name = "txt_Spotify_Song_ID";
            this.txt_Spotify_Song_ID.ReadOnly = true;
            this.txt_Spotify_Song_ID.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_Spotify_Song_ID.Size = new System.Drawing.Size(207, 20);
            this.txt_Spotify_Song_ID.TabIndex = 401;
            // 
            // txt_SpotifyStatus
            // 
            this.txt_SpotifyStatus.Cue = "Authentification Status";
            this.txt_SpotifyStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SpotifyStatus.ForeColor = System.Drawing.Color.Gray;
            this.txt_SpotifyStatus.HideSelection = false;
            this.txt_SpotifyStatus.Location = new System.Drawing.Point(180, 26);
            this.txt_SpotifyStatus.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_SpotifyStatus.Name = "txt_SpotifyStatus";
            this.txt_SpotifyStatus.ReadOnly = true;
            this.txt_SpotifyStatus.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_SpotifyStatus.Size = new System.Drawing.Size(207, 20);
            this.txt_SpotifyStatus.TabIndex = 400;
            // 
            // picbx_SpotifyCover
            // 
            this.picbx_SpotifyCover.Location = new System.Drawing.Point(4, 52);
            this.picbx_SpotifyCover.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.picbx_SpotifyCover.Name = "picbx_SpotifyCover";
            this.picbx_SpotifyCover.Size = new System.Drawing.Size(128, 124);
            this.picbx_SpotifyCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_SpotifyCover.TabIndex = 399;
            this.picbx_SpotifyCover.TabStop = false;
            // 
            // btn_GetTrack
            // 
            this.btn_GetTrack.Location = new System.Drawing.Point(886, 52);
            this.btn_GetTrack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_GetTrack.Name = "btn_GetTrack";
            this.btn_GetTrack.Size = new System.Drawing.Size(106, 42);
            this.btn_GetTrack.TabIndex = 398;
            this.btn_GetTrack.Text = "Get Spotify Details";
            this.btn_GetTrack.UseVisualStyleBackColor = true;
            this.btn_GetTrack.Click += new System.EventHandler(this.btn_GetTrack_Click);
            // 
            // txt_SavedPlaylists
            // 
            this.txt_SavedPlaylists.FormattingEnabled = true;
            this.txt_SavedPlaylists.Location = new System.Drawing.Point(624, 34);
            this.txt_SavedPlaylists.Name = "txt_SavedPlaylists";
            this.txt_SavedPlaylists.Size = new System.Drawing.Size(215, 186);
            this.txt_SavedPlaylists.TabIndex = 397;
            // 
            // txt_SavedTracks
            // 
            this.txt_SavedTracks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.txt_SavedTracks.FullRowSelect = true;
            this.txt_SavedTracks.Location = new System.Drawing.Point(406, 30);
            this.txt_SavedTracks.Name = "txt_SavedTracks";
            this.txt_SavedTracks.Size = new System.Drawing.Size(204, 194);
            this.txt_SavedTracks.TabIndex = 396;
            this.txt_SavedTracks.UseCompatibleStateImageBehavior = false;
            this.txt_SavedTracks.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Title";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Artist";
            this.columnHeader5.Width = 117;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Album";
            this.columnHeader6.Width = 131;
            // 
            // txt_SpotifyLog
            // 
            this.txt_SpotifyLog.Location = new System.Drawing.Point(140, 58);
            this.txt_SpotifyLog.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_SpotifyLog.Name = "txt_SpotifyLog";
            this.txt_SpotifyLog.Size = new System.Drawing.Size(246, 180);
            this.txt_SpotifyLog.TabIndex = 395;
            this.txt_SpotifyLog.Text = "";
            // 
            // btn_ActivateSpotify
            // 
            this.btn_ActivateSpotify.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_ActivateSpotify.Location = new System.Drawing.Point(8, 24);
            this.btn_ActivateSpotify.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_ActivateSpotify.Name = "btn_ActivateSpotify";
            this.btn_ActivateSpotify.Size = new System.Drawing.Size(163, 22);
            this.btn_ActivateSpotify.TabIndex = 394;
            this.btn_ActivateSpotify.Text = "Authentificate with Spotify";
            this.btn_ActivateSpotify.UseVisualStyleBackColor = true;
            this.btn_ActivateSpotify.Click += new System.EventHandler(this.btn_ActivateSpotify_Click);
            // 
            // MainDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1135, 512);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.DataViewGrid);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainDB";
            this.Text = "MainDB";
            this.Load += new System.EventHandler(this.MainDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataViewGrid)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Track_No)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Top10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Alt_No)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AddDD)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Preview_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PreviewEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomForge_Vwersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Followers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomsForge_Like)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CoundofPacked)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_SpotifyCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.DataGridView DataViewGrid;
        private ToolTip toolTip1;
        private TabControl tabControl;
        private TabPage tabPage1;
        internal Panel Panel1;
        private Button btn_ChangeLyrics;
        private CueTextBox txt_Lyrics;
        private Button btn_Artist2SortA;
        private Button btn_Title2SortT;
        private Button btn_GroupsAdd;
        private Button btn_GroupsRemove;
        private CheckedListBox chbx_AllGroups;
        private Button btn_AddCoverFlags;
        private Button btn_DefaultCover;
        private Label label8;
        private NumericUpDown txt_Top10;
        private Label label6;
        private Label label3;
        private Label label5;
        private NumericUpDown txt_Rating;
        private ComboBox chbx_Group;
        private Button btn_OpenSongFolder;
        private NumericUpDown txt_Alt_No;
        private Button bth_GetTrackNo;
        private Label label4;
        private GroupBox groupBox6;
        private CheckBox chbx_DupliGTrack;
        private CheckBox chbx_CopyOld;
        private CheckBox chbx_RemoveBassDD;
        private ProgressBar pB_ReadDLCs;
        private ComboBox chbx_PreSavedFTP;
        private Button btn_Package;
        private ComboBox chbx_Format;
        private CheckBox chbx_Copy;
        private CueTextBox txt_FTPPath;
        private Button btn_SteamDLCFolder;
        private CheckBox chbx_UniqueID;
        private GroupBox groupBox5;
        private CheckBox chbx_Bass;
        private CheckBox chbx_Lead;
        private CheckBox chbx_Combo;
        private CheckBox chbx_Rhythm;
        private CueTextBox txt_BassPicking;
        private CueTextBox txt_Tuning;
        private GroupBox groupBox4;
        private Button btn_EOF;
        private Button btn_CreateLyrics;
        private CheckBox chbx_KeepDD;
        private CheckBox chbx_KeepBassDD;
        private Button btn_AddSections;
        private CheckBox chbx_Lyrics;
        private CheckBox chbx_TrackNo;
        private Button btn_AddDD;
        private Button btn_RemoveDD;
        private CheckBox chbx_DD;
        private NumericUpDown txt_AddDD;
        private CheckBox chbx_Original;
        private CheckBox chbx_Sections;
        private CheckBox chbx_Preview;
        private CheckBox chbx_Author;
        private Button btn_RemoveBassDD;
        private CheckBox chbx_BassDD;
        private CheckBox chbx_Bonus;
        private CheckBox chbx_Cover;
        private GroupBox groupBox3;
        private Button btn_Copy_old;
        private Button btn_DuplicateFolder;
        private CheckBox chbx_Avail_Duplicate;
        private Button btn_OldFolder;
        private CheckBox chbx_Avail_Old;
        private CheckBox chbx_Has_Been_Corrected;
        private GroupBox groupBox2;
        private CheckBox chbx_InclGroups;
        private CueTextBox txt_ID;
        private CheckBox chbx_InclBeta;
        private Button btn_SelectInverted;
        private Button btn_InvertSelect;
        private Label label12;
        private ComboBox txt_MultiTrackType;
        private CheckBox chbx_MultiTrack;
        private Button btn_SelectAll;
        private CheckBox chbx_Broken;
        private CheckBox chbx_Selected;
        private Label label1;
        private CheckBox chbx_Beta;
        private Button btn_SelectNone;
        private Label lbl_NoRec;
        private Button btn_NextItem;
        private ComboBox cmb_Filter;
        private Button btn_Prev;
        private GroupBox groupBox1;
        private Button btn_GarageBand;
        private NumericUpDown txt_Volume;
        private NumericUpDown txt_Preview_Volume;
        private Button btn_AddPreview;
        private DateTimePicker txt_PreviewStart;
        private Button btn_PlayAudio;
        private Button btn_PlayPreview;
        private CueTextBox txt_AverageTempo;
        private CheckBox chbx_AutoPlay;
        public CueTextBox txt_OggPath;
        private NumericUpDown txt_PreviewEnd;
        private CueTextBox txt_OggPreviewPath;
        private Button btn_SelectPreview;
        private Button btn_OpenRetail;
        private Label label2;
        private Button btn_Close;
        private Button btn_OpenStandardization;
        public CueTextBox txt_AlbumArtPath;
        private Button btn_ChangeCover;
        private CueTextBox txt_Artist_ShortName;
        private CueTextBox txt_Album_ShortName;
        private CheckBox chbx_AutoSave;
        private CueTextBox txt_Album_Year;
        private PictureBox picbx_AlbumArtPath;
        private Button btn_Save;
        private CueTextBox txt_APP_ID;
        private CueTextBox txt_DLC_ID;
        private CueTextBox txt_Version;
        private CueTextBox txt_Author;
        private CueTextBox txt_Album;
        private CueTextBox txt_Title_Sort;
        private CueTextBox txt_Title;
        private CueTextBox txt_Artist_Sort;
        private CueTextBox txt_Artist;
        private Button btn_Duplicate;
        private Button btn_SearchReset;
        private Button btn_Search;
        private CheckBox chbx_Alternate;
        private Button btn_Delete;
        private Button btn_OpenDB;
        private Button btn_Tones;
        private Button btn_Arrangements;
        internal CheckBox CheckBox1;
        private TabPage tabPage2;
        private GroupBox groupBox7;
        private Button btn_Beta;
        private NumericUpDown txt_CustomForge_Vwersion;
        private Label label11;
        private RichTextBox txt_CustomsForge_ReleaseNotes;
        private Button btn_Playthrough;
        private CueTextBox txt_Playthough;
        private Label label10;
        private Button btn_Like;
        private NumericUpDown txt_Followers;
        private Label label9;
        private NumericUpDown txt_CustomsForge_Like;
        private Button btn_Followers;
        private Button btn_CustomForge_Link;
        private RichTextBox txt_debug;
        private Button btn_Youtube;
        private Label label59;
        private Button btn_Debug;
        private CueTextBox txt_YouTube_Link;
        private CueTextBox txt_CustomsForge_Link;
        private Label lbfl_YouTube_Link;
        private Label label33;
        private Label label32;
        private NumericUpDown txt_Track_No;
        private CheckBox chbx_Replace;
        private TabPage tabPage3;
        private CueTextBox txt_RemotePath;
        private Button btn_RemoveAllRemoteSongs;
        private Button btn_RemoveRemoteSong;
        private Button btn_ApplyArtistShortNames;
        private Button btn_ApplyAlbumSortNames;
        private CheckBox chbx_Last_Packed;
        private CueTextBox txt_Live_Details;
        private CheckBox chbx_Is_Live;
        private Button btn_Remove_All_Packed;
        private ComboBox cmb_Packed;
        private NumericUpDown txt_CoundofPacked;
        private Button btn_Remove_Packed;
        private Label label13;
        private RichTextBox txt_Description;
        private ComboBox txt_Platform;
        private CueTextBox txt_OldPath;
        private Button btn_ReadGameLibrary;
        private Button btn_CopyOld;
        private Button btn_Find_FilesMissingIssues;
        private CueTextBox txt_FilesMissingIssues;
        private Button button1;
        private Button btm_GoTemp;
        private Button button2;
        private Button btn_RemoveBrakets;
        private Button bbtn_Replace_Brakets;
        private CheckBox chbx_Is_Acoustic;
        private ComboBox chbx_Format_Originals;
        private CheckBox chbx_Originals_Available;
        private CheckBox chbx_Ignore_Officials;
        private Button button3;
        private CheckBox chbx_Duplicate_Official;
        private TabPage tabPage4;
        private PictureBox picbx_SpotifyCover;
        private Button btn_GetTrack;
        private ListBox txt_SavedPlaylists;
        private ListView txt_SavedTracks;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private RichTextBox txt_SpotifyLog;
        private Button btn_ActivateSpotify;
        private CueTextBox txt_SpotifyStatus;
        private CueTextBox txt_Spotify_Album_ID;
        private CueTextBox txt_Spotify_Artist_ID;
        private CueTextBox txt_Spotify_Song_ID;
        private CueTextBox txt_Spotify_Album_URL;
    }
}