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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
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
            this.button2 = new System.Windows.Forms.Button();
            this.txt_YouTube_Link = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CustomsForge_Link = new RocksmithToolkitGUI.CueTextBox();
            this.lbfl_YouTube_Link = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Rating = new System.Windows.Forms.NumericUpDown();
            this.btn_OpenSongFolder = new System.Windows.Forms.Button();
            this.txt_Alt_No = new System.Windows.Forms.NumericUpDown();
            this.txt_Track_No = new System.Windows.Forms.NumericUpDown();
            this.bth_GetTrackNo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Platform = new RocksmithToolkitGUI.CueTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chbx_RemoveBassDD = new System.Windows.Forms.CheckBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.chbx_UniqueID = new System.Windows.Forms.CheckBox();
            this.chbx_PreSavedFTP = new System.Windows.Forms.ComboBox();
            this.btn_Package = new System.Windows.Forms.Button();
            this.chbx_CopyOriginal = new System.Windows.Forms.CheckBox();
            this.chbx_Format = new System.Windows.Forms.ComboBox();
            this.chbx_Copy = new System.Windows.Forms.CheckBox();
            this.txt_FTPPath = new RocksmithToolkitGUI.CueTextBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chbx_Bass = new System.Windows.Forms.CheckBox();
            this.chbx_Lead = new System.Windows.Forms.CheckBox();
            this.chbx_Combo = new System.Windows.Forms.CheckBox();
            this.chbx_Rhythm = new System.Windows.Forms.CheckBox();
            this.txt_Tuning = new RocksmithToolkitGUI.CueTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_AddSections = new System.Windows.Forms.Button();
            this.chbx_Lyrics = new System.Windows.Forms.CheckBox();
            this.chbx_TrackNo = new System.Windows.Forms.CheckBox();
            this.btn_AddDD = new System.Windows.Forms.Button();
            this.btn_RemoveDD = new System.Windows.Forms.Button();
            this.txt_BassPicking = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_DD = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.chbx_Original = new System.Windows.Forms.CheckBox();
            this.chbx_Sections = new System.Windows.Forms.CheckBox();
            this.chbx_Preview = new System.Windows.Forms.CheckBox();
            this.chbx_Author = new System.Windows.Forms.CheckBox();
            this.btn_RemoveBassDD = new System.Windows.Forms.Button();
            this.chbx_BassDD = new System.Windows.Forms.CheckBox();
            this.chbx_Bonus = new System.Windows.Forms.CheckBox();
            this.chbx_Cover = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_DuplicateFolder = new System.Windows.Forms.Button();
            this.chbx_Avail_Duplicate = new System.Windows.Forms.CheckBox();
            this.btn_OldFolder = new System.Windows.Forms.Button();
            this.chbx_Avail_Old = new System.Windows.Forms.CheckBox();
            this.chbx_Has_Been_Corrected = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_MultiTrackType = new System.Windows.Forms.ComboBox();
            this.chbx_MultiTrack = new System.Windows.Forms.CheckBox();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.chbx_Group = new System.Windows.Forms.ComboBox();
            this.chbx_Selected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbx_Beta = new System.Windows.Forms.CheckBox();
            this.btn_SelectNone = new System.Windows.Forms.Button();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.cmb_Filter = new System.Windows.Forms.ComboBox();
            this.btn_NextItem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Volume = new System.Windows.Forms.NumericUpDown();
            this.txt_Preview_Volume = new System.Windows.Forms.NumericUpDown();
            this.btn_AddPreview = new System.Windows.Forms.Button();
            this.txt_PreviewStart = new System.Windows.Forms.DateTimePicker();
            this.btn_PlayAudio = new System.Windows.Forms.Button();
            this.btn_PlayPreview = new System.Windows.Forms.Button();
            this.txt_AverageTempo = new RocksmithToolkitGUI.CueTextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txt_OggPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewEnd = new System.Windows.Forms.NumericUpDown();
            this.txt_OggPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.btn_SelectPreview = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
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
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.chbx_Alternate = new System.Windows.Forms.CheckBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Tones = new System.Windows.Forms.Button();
            this.btn_Arrangements = new System.Windows.Forms.Button();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.DataViewGrid = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Followers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomsForge_Like)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Alt_No)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Track_No)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Preview_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PreviewEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.button4);
            this.Panel1.Controls.Add(this.button3);
            this.Panel1.Controls.Add(this.groupBox7);
            this.Panel1.Controls.Add(this.label8);
            this.Panel1.Controls.Add(this.numericUpDown2);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.txt_Rating);
            this.Panel1.Controls.Add(this.btn_OpenSongFolder);
            this.Panel1.Controls.Add(this.txt_Alt_No);
            this.Panel1.Controls.Add(this.txt_Track_No);
            this.Panel1.Controls.Add(this.bth_GetTrackNo);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.txt_Platform);
            this.Panel1.Controls.Add(this.groupBox6);
            this.Panel1.Controls.Add(this.groupBox5);
            this.Panel1.Controls.Add(this.groupBox4);
            this.Panel1.Controls.Add(this.groupBox3);
            this.Panel1.Controls.Add(this.groupBox2);
            this.Panel1.Controls.Add(this.groupBox1);
            this.Panel1.Controls.Add(this.button5);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.button15);
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
            this.Panel1.Controls.Add(this.txt_Description);
            this.Panel1.Controls.Add(this.btn_Search);
            this.Panel1.Controls.Add(this.chbx_Alternate);
            this.Panel1.Controls.Add(this.btn_Delete);
            this.Panel1.Controls.Add(this.button1);
            this.Panel1.Controls.Add(this.btn_Tones);
            this.Panel1.Controls.Add(this.btn_Arrangements);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 357);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1109, 283);
            this.Panel1.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(834, 209);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 22);
            this.button4.TabIndex = 383;
            this.button4.Text = "Add Cover Flags";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(834, 185);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 22);
            this.button3.TabIndex = 382;
            this.button3.Text = "Default Cover";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numericUpDown3);
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
            this.groupBox7.Controls.Add(this.button2);
            this.groupBox7.Controls.Add(this.txt_YouTube_Link);
            this.groupBox7.Controls.Add(this.txt_CustomsForge_Link);
            this.groupBox7.Controls.Add(this.lbfl_YouTube_Link);
            this.groupBox7.Controls.Add(this.label33);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Location = new System.Drawing.Point(10, 237);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1093, 42);
            this.groupBox7.TabIndex = 381;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "CustomsForge Details";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(686, 14);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown3.TabIndex = 390;
            this.numericUpDown3.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(639, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 389;
            this.label11.Text = "Version";
            // 
            // txt_CustomsForge_ReleaseNotes
            // 
            this.txt_CustomsForge_ReleaseNotes.Location = new System.Drawing.Point(812, 10);
            this.txt_CustomsForge_ReleaseNotes.Name = "txt_CustomsForge_ReleaseNotes";
            this.txt_CustomsForge_ReleaseNotes.Size = new System.Drawing.Size(109, 25);
            this.txt_CustomsForge_ReleaseNotes.TabIndex = 382;
            this.txt_CustomsForge_ReleaseNotes.Text = "";
            // 
            // btn_Playthrough
            // 
            this.btn_Playthrough.Enabled = false;
            this.btn_Playthrough.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Playthrough.Location = new System.Drawing.Point(259, 19);
            this.btn_Playthrough.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Playthrough.Name = "btn_Playthrough";
            this.btn_Playthrough.Size = new System.Drawing.Size(18, 17);
            this.btn_Playthrough.TabIndex = 388;
            this.btn_Playthrough.Text = ">";
            this.btn_Playthrough.UseVisualStyleBackColor = true;
            this.btn_Playthrough.Click += new System.EventHandler(this.btn_Playthrough_Click);
            // 
            // txt_Playthough
            // 
            this.txt_Playthough.Cue = "PlayThrough";
            this.txt_Playthough.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Playthough.ForeColor = System.Drawing.Color.Gray;
            this.txt_Playthough.Location = new System.Drawing.Point(214, 17);
            this.txt_Playthough.Name = "txt_Playthough";
            this.txt_Playthough.Size = new System.Drawing.Size(40, 20);
            this.txt_Playthough.TabIndex = 386;
            this.txt_Playthough.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(151, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 387;
            this.label10.Text = "Playthrough";
            // 
            // btn_Like
            // 
            this.btn_Like.Enabled = false;
            this.btn_Like.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Like.Location = new System.Drawing.Point(503, 18);
            this.btn_Like.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Like.Name = "btn_Like";
            this.btn_Like.Size = new System.Drawing.Size(18, 17);
            this.btn_Like.TabIndex = 385;
            this.btn_Like.Text = ">";
            this.btn_Like.UseVisualStyleBackColor = true;
            // 
            // txt_Followers
            // 
            this.txt_Followers.Location = new System.Drawing.Point(571, 17);
            this.txt_Followers.Margin = new System.Windows.Forms.Padding(2);
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
            this.txt_Followers.Size = new System.Drawing.Size(41, 20);
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
            this.label9.Location = new System.Drawing.Point(524, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 383;
            this.label9.Text = "Followers";
            // 
            // txt_CustomsForge_Like
            // 
            this.txt_CustomsForge_Like.Location = new System.Drawing.Point(464, 16);
            this.txt_CustomsForge_Like.Margin = new System.Windows.Forms.Padding(2);
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
            this.txt_CustomsForge_Like.Size = new System.Drawing.Size(35, 20);
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
            this.btn_Followers.Location = new System.Drawing.Point(612, 17);
            this.btn_Followers.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Followers.Name = "btn_Followers";
            this.btn_Followers.Size = new System.Drawing.Size(18, 17);
            this.btn_Followers.TabIndex = 368;
            this.btn_Followers.Text = ">";
            this.btn_Followers.UseVisualStyleBackColor = true;
            // 
            // btn_CustomForge_Link
            // 
            this.btn_CustomForge_Link.Enabled = false;
            this.btn_CustomForge_Link.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CustomForge_Link.Location = new System.Drawing.Point(413, 16);
            this.btn_CustomForge_Link.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CustomForge_Link.Name = "btn_CustomForge_Link";
            this.btn_CustomForge_Link.Size = new System.Drawing.Size(18, 17);
            this.btn_CustomForge_Link.TabIndex = 367;
            this.btn_CustomForge_Link.Text = ">";
            this.btn_CustomForge_Link.UseVisualStyleBackColor = true;
            this.btn_CustomForge_Link.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // txt_debug
            // 
            this.txt_debug.Location = new System.Drawing.Point(926, 7);
            this.txt_debug.Name = "txt_debug";
            this.txt_debug.Size = new System.Drawing.Size(104, 36);
            this.txt_debug.TabIndex = 328;
            this.txt_debug.Text = "";
            this.txt_debug.Visible = false;
            // 
            // btn_Youtube
            // 
            this.btn_Youtube.Enabled = false;
            this.btn_Youtube.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Youtube.Location = new System.Drawing.Point(128, 18);
            this.btn_Youtube.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Youtube.Name = "btn_Youtube";
            this.btn_Youtube.Size = new System.Drawing.Size(18, 17);
            this.btn_Youtube.TabIndex = 366;
            this.btn_Youtube.Text = ">";
            this.btn_Youtube.UseVisualStyleBackColor = true;
            this.btn_Youtube.Click += new System.EventHandler(this.btn_Youtube_Click);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(737, 16);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(77, 13);
            this.label59.TabIndex = 328;
            this.label59.Text = "Release Notes";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1031, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 36);
            this.button2.TabIndex = 326;
            this.button2.Text = "Get Track No";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_YouTube_Link
            // 
            this.txt_YouTube_Link.Cue = "YouTube Link New";
            this.txt_YouTube_Link.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_YouTube_Link.ForeColor = System.Drawing.Color.Gray;
            this.txt_YouTube_Link.Location = new System.Drawing.Point(83, 16);
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
            this.txt_CustomsForge_Link.Location = new System.Drawing.Point(364, 15);
            this.txt_CustomsForge_Link.Name = "txt_CustomsForge_Link";
            this.txt_CustomsForge_Link.Size = new System.Drawing.Size(45, 20);
            this.txt_CustomsForge_Link.TabIndex = 316;
            this.txt_CustomsForge_Link.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbfl_YouTube_Link
            // 
            this.lbfl_YouTube_Link.AutoSize = true;
            this.lbfl_YouTube_Link.Location = new System.Drawing.Point(26, 19);
            this.lbfl_YouTube_Link.Name = "lbfl_YouTube_Link";
            this.lbfl_YouTube_Link.Size = new System.Drawing.Size(51, 13);
            this.lbfl_YouTube_Link.TabIndex = 322;
            this.lbfl_YouTube_Link.Text = "YouTube";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(291, 18);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(74, 13);
            this.label33.TabIndex = 321;
            this.label33.Text = "CustomsForge";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(436, 18);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 13);
            this.label32.TabIndex = 320;
            this.label32.Text = "Like";
            // 
            // label8
            // 
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(599, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 334;
            this.label8.Text = "in Top 10";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(565, 3);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(34, 20);
            this.numericUpDown2.TabIndex = 335;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(489, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 18);
            this.label6.TabIndex = 333;
            this.label6.Text = "#";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(291, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 18);
            this.label5.TabIndex = 325;
            this.label5.Text = "/5 CDLC stars";
            // 
            // txt_Rating
            // 
            this.txt_Rating.Location = new System.Drawing.Point(264, 136);
            this.txt_Rating.Margin = new System.Windows.Forms.Padding(2);
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
            // btn_OpenSongFolder
            // 
            this.btn_OpenSongFolder.Location = new System.Drawing.Point(10, 126);
            this.btn_OpenSongFolder.Name = "btn_OpenSongFolder";
            this.btn_OpenSongFolder.Size = new System.Drawing.Size(122, 21);
            this.btn_OpenSongFolder.TabIndex = 331;
            this.btn_OpenSongFolder.Text = "Open Song Folder";
            this.btn_OpenSongFolder.UseVisualStyleBackColor = true;
            this.btn_OpenSongFolder.Click += new System.EventHandler(this.btn_OpenSongFolder_Click);
            // 
            // txt_Alt_No
            // 
            this.txt_Alt_No.Location = new System.Drawing.Point(615, 48);
            this.txt_Alt_No.Margin = new System.Windows.Forms.Padding(2);
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
            // txt_Track_No
            // 
            this.txt_Track_No.Location = new System.Drawing.Point(504, 3);
            this.txt_Track_No.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Track_No.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txt_Track_No.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Track_No.Name = "txt_Track_No";
            this.txt_Track_No.Size = new System.Drawing.Size(35, 20);
            this.txt_Track_No.TabIndex = 325;
            this.txt_Track_No.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txt_Track_No.ValueChanged += new System.EventHandler(this.txt_Track_No_TextChanged);
            // 
            // bth_GetTrackNo
            // 
            this.bth_GetTrackNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bth_GetTrackNo.Location = new System.Drawing.Point(542, 6);
            this.bth_GetTrackNo.Name = "bth_GetTrackNo";
            this.bth_GetTrackNo.Size = new System.Drawing.Size(21, 15);
            this.bth_GetTrackNo.TabIndex = 329;
            this.bth_GetTrackNo.Text = "<";
            this.bth_GetTrackNo.UseVisualStyleBackColor = true;
            this.bth_GetTrackNo.Click += new System.EventHandler(this.bth_GetTrackNo_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(488, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 18);
            this.label4.TabIndex = 325;
            this.label4.Text = "v.";
            // 
            // txt_Platform
            // 
            this.txt_Platform.Cue = "Platform";
            this.txt_Platform.Enabled = false;
            this.txt_Platform.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Platform.ForeColor = System.Drawing.Color.Gray;
            this.txt_Platform.Location = new System.Drawing.Point(444, 115);
            this.txt_Platform.Name = "txt_Platform";
            this.txt_Platform.Size = new System.Drawing.Size(27, 20);
            this.txt_Platform.TabIndex = 327;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chbx_RemoveBassDD);
            this.groupBox6.Controls.Add(this.pB_ReadDLCs);
            this.groupBox6.Controls.Add(this.chbx_UniqueID);
            this.groupBox6.Controls.Add(this.chbx_PreSavedFTP);
            this.groupBox6.Controls.Add(this.btn_Package);
            this.groupBox6.Controls.Add(this.chbx_CopyOriginal);
            this.groupBox6.Controls.Add(this.chbx_Format);
            this.groupBox6.Controls.Add(this.chbx_Copy);
            this.groupBox6.Controls.Add(this.txt_FTPPath);
            this.groupBox6.Controls.Add(this.btn_SteamDLCFolder);
            this.groupBox6.Location = new System.Drawing.Point(950, 102);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(156, 136);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Package and Copy";
            // 
            // chbx_RemoveBassDD
            // 
            this.chbx_RemoveBassDD.AutoSize = true;
            this.chbx_RemoveBassDD.Checked = true;
            this.chbx_RemoveBassDD.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_RemoveBassDD.Location = new System.Drawing.Point(73, 65);
            this.chbx_RemoveBassDD.Name = "chbx_RemoveBassDD";
            this.chbx_RemoveBassDD.Size = new System.Drawing.Size(82, 17);
            this.chbx_RemoveBassDD.TabIndex = 331;
            this.chbx_RemoveBassDD.Text = "wo BassDD";
            this.chbx_RemoveBassDD.UseVisualStyleBackColor = true;
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(4, 104);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(145, 18);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 330;
            // 
            // chbx_UniqueID
            // 
            this.chbx_UniqueID.AutoSize = true;
            this.chbx_UniqueID.Checked = true;
            this.chbx_UniqueID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_UniqueID.Location = new System.Drawing.Point(4, 65);
            this.chbx_UniqueID.Name = "chbx_UniqueID";
            this.chbx_UniqueID.Size = new System.Drawing.Size(71, 17);
            this.chbx_UniqueID.TabIndex = 329;
            this.chbx_UniqueID.Text = "UniqueID";
            this.chbx_UniqueID.UseVisualStyleBackColor = true;
            // 
            // chbx_PreSavedFTP
            // 
            this.chbx_PreSavedFTP.FormattingEnabled = true;
            this.chbx_PreSavedFTP.Items.AddRange(new object[] {
            "EU",
            "US"});
            this.chbx_PreSavedFTP.Location = new System.Drawing.Point(4, 81);
            this.chbx_PreSavedFTP.Name = "chbx_PreSavedFTP";
            this.chbx_PreSavedFTP.Size = new System.Drawing.Size(46, 21);
            this.chbx_PreSavedFTP.TabIndex = 328;
            this.chbx_PreSavedFTP.Text = "US";
            this.chbx_PreSavedFTP.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // btn_Package
            // 
            this.btn_Package.Enabled = false;
            this.btn_Package.Location = new System.Drawing.Point(9, 19);
            this.btn_Package.Name = "btn_Package";
            this.btn_Package.Size = new System.Drawing.Size(81, 26);
            this.btn_Package.TabIndex = 39;
            this.btn_Package.Text = "Package";
            this.btn_Package.UseVisualStyleBackColor = true;
            this.btn_Package.Click += new System.EventHandler(this.btn_Conv_And_Transfer_Click);
            // 
            // chbx_CopyOriginal
            // 
            this.chbx_CopyOriginal.AutoSize = true;
            this.chbx_CopyOriginal.Enabled = false;
            this.chbx_CopyOriginal.Location = new System.Drawing.Point(73, 48);
            this.chbx_CopyOriginal.Name = "chbx_CopyOriginal";
            this.chbx_CopyOriginal.Size = new System.Drawing.Size(61, 17);
            this.chbx_CopyOriginal.TabIndex = 327;
            this.chbx_CopyOriginal.Text = "Original";
            this.chbx_CopyOriginal.UseVisualStyleBackColor = true;
            // 
            // chbx_Format
            // 
            this.chbx_Format.FormattingEnabled = true;
            this.chbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.chbx_Format.Location = new System.Drawing.Point(93, 22);
            this.chbx_Format.Name = "chbx_Format";
            this.chbx_Format.Size = new System.Drawing.Size(46, 21);
            this.chbx_Format.TabIndex = 108;
            this.chbx_Format.Text = "PS3";
            this.chbx_Format.SelectedIndexChanged += new System.EventHandler(this.cbx_Format_SelectedIndexChanged);
            this.chbx_Format.SelectedValueChanged += new System.EventHandler(this.cbx_Format_SelectedValueChanged);
            // 
            // chbx_Copy
            // 
            this.chbx_Copy.AutoSize = true;
            this.chbx_Copy.Checked = true;
            this.chbx_Copy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Copy.Location = new System.Drawing.Point(4, 48);
            this.chbx_Copy.Name = "chbx_Copy";
            this.chbx_Copy.Size = new System.Drawing.Size(71, 17);
            this.chbx_Copy.TabIndex = 326;
            this.chbx_Copy.Text = "and Copy";
            this.chbx_Copy.UseVisualStyleBackColor = true;
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(58, 82);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(64, 20);
            this.txt_FTPPath.TabIndex = 308;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(127, 84);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_SteamDLCFolder.TabIndex = 311;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.chbx_Bass);
            this.groupBox5.Controls.Add(this.chbx_Lead);
            this.groupBox5.Controls.Add(this.chbx_Combo);
            this.groupBox5.Controls.Add(this.chbx_Rhythm);
            this.groupBox5.Controls.Add(this.txt_Tuning);
            this.groupBox5.Location = new System.Drawing.Point(492, 69);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(155, 78);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Available Instruments";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(81, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 334;
            this.label7.Text = "(All Instr.)";
            // 
            // chbx_Bass
            // 
            this.chbx_Bass.AutoSize = true;
            this.chbx_Bass.Enabled = false;
            this.chbx_Bass.Location = new System.Drawing.Point(68, 36);
            this.chbx_Bass.Name = "chbx_Bass";
            this.chbx_Bass.Size = new System.Drawing.Size(49, 17);
            this.chbx_Bass.TabIndex = 75;
            this.chbx_Bass.Text = "Bass";
            this.chbx_Bass.UseVisualStyleBackColor = true;
            // 
            // chbx_Lead
            // 
            this.chbx_Lead.AutoSize = true;
            this.chbx_Lead.Enabled = false;
            this.chbx_Lead.Location = new System.Drawing.Point(6, 17);
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
            this.chbx_Combo.Location = new System.Drawing.Point(68, 17);
            this.chbx_Combo.Name = "chbx_Combo";
            this.chbx_Combo.Size = new System.Drawing.Size(59, 17);
            this.chbx_Combo.TabIndex = 76;
            this.chbx_Combo.Text = "Combo";
            this.chbx_Combo.UseVisualStyleBackColor = true;
            this.chbx_Combo.CheckedChanged += new System.EventHandler(this.chbx_Combo_CheckedChanged);
            // 
            // chbx_Rhythm
            // 
            this.chbx_Rhythm.AutoSize = true;
            this.chbx_Rhythm.Enabled = false;
            this.chbx_Rhythm.Location = new System.Drawing.Point(6, 36);
            this.chbx_Rhythm.Name = "chbx_Rhythm";
            this.chbx_Rhythm.Size = new System.Drawing.Size(62, 17);
            this.chbx_Rhythm.TabIndex = 77;
            this.chbx_Rhythm.Text = "Rhythm";
            this.chbx_Rhythm.UseVisualStyleBackColor = true;
            this.chbx_Rhythm.CheckedChanged += new System.EventHandler(this.chbx_Rhythm_CheckedChanged);
            // 
            // txt_Tuning
            // 
            this.txt_Tuning.Cue = "Tuning (All)";
            this.txt_Tuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tuning.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tuning.Location = new System.Drawing.Point(6, 54);
            this.txt_Tuning.Name = "txt_Tuning";
            this.txt_Tuning.Size = new System.Drawing.Size(75, 20);
            this.txt_Tuning.TabIndex = 78;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_AddSections);
            this.groupBox4.Controls.Add(this.chbx_Lyrics);
            this.groupBox4.Controls.Add(this.chbx_TrackNo);
            this.groupBox4.Controls.Add(this.btn_AddDD);
            this.groupBox4.Controls.Add(this.btn_RemoveDD);
            this.groupBox4.Controls.Add(this.txt_BassPicking);
            this.groupBox4.Controls.Add(this.chbx_DD);
            this.groupBox4.Controls.Add(this.numericUpDown1);
            this.groupBox4.Controls.Add(this.chbx_Original);
            this.groupBox4.Controls.Add(this.chbx_Sections);
            this.groupBox4.Controls.Add(this.chbx_Preview);
            this.groupBox4.Controls.Add(this.chbx_Author);
            this.groupBox4.Controls.Add(this.btn_RemoveBassDD);
            this.groupBox4.Controls.Add(this.chbx_BassDD);
            this.groupBox4.Controls.Add(this.chbx_Bonus);
            this.groupBox4.Controls.Add(this.chbx_Cover);
            this.groupBox4.Location = new System.Drawing.Point(653, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(163, 142);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Quality Checks";
            // 
            // btn_AddSections
            // 
            this.btn_AddSections.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddSections.Location = new System.Drawing.Point(139, 94);
            this.btn_AddSections.Name = "btn_AddSections";
            this.btn_AddSections.Size = new System.Drawing.Size(21, 20);
            this.btn_AddSections.TabIndex = 328;
            this.btn_AddSections.Text = "+";
            this.btn_AddSections.UseVisualStyleBackColor = true;
            this.btn_AddSections.Click += new System.EventHandler(this.btn_AddSections_Click);
            // 
            // chbx_Lyrics
            // 
            this.chbx_Lyrics.AutoSize = true;
            this.chbx_Lyrics.Enabled = false;
            this.chbx_Lyrics.Location = new System.Drawing.Point(76, 116);
            this.chbx_Lyrics.Name = "chbx_Lyrics";
            this.chbx_Lyrics.Size = new System.Drawing.Size(84, 17);
            this.chbx_Lyrics.TabIndex = 327;
            this.chbx_Lyrics.Text = "Vocal Track";
            this.chbx_Lyrics.UseVisualStyleBackColor = true;
            // 
            // chbx_TrackNo
            // 
            this.chbx_TrackNo.AutoSize = true;
            this.chbx_TrackNo.Enabled = false;
            this.chbx_TrackNo.Location = new System.Drawing.Point(76, 78);
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
            this.btn_AddDD.Location = new System.Drawing.Point(54, 36);
            this.btn_AddDD.Name = "btn_AddDD";
            this.btn_AddDD.Size = new System.Drawing.Size(21, 20);
            this.btn_AddDD.TabIndex = 316;
            this.btn_AddDD.Text = "+";
            this.btn_AddDD.UseVisualStyleBackColor = true;
            this.btn_AddDD.Click += new System.EventHandler(this.btn_AddDD_Click);
            // 
            // btn_RemoveDD
            // 
            this.btn_RemoveDD.Enabled = false;
            this.btn_RemoveDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveDD.Location = new System.Drawing.Point(75, 36);
            this.btn_RemoveDD.Name = "btn_RemoveDD";
            this.btn_RemoveDD.Size = new System.Drawing.Size(21, 20);
            this.btn_RemoveDD.TabIndex = 315;
            this.btn_RemoveDD.Text = "-";
            this.btn_RemoveDD.UseVisualStyleBackColor = true;
            this.btn_RemoveDD.Click += new System.EventHandler(this.btn_RemoveDD_Click);
            // 
            // txt_BassPicking
            // 
            this.txt_BassPicking.Cue = "Bass Picking";
            this.txt_BassPicking.Enabled = false;
            this.txt_BassPicking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_BassPicking.ForeColor = System.Drawing.Color.Gray;
            this.txt_BassPicking.Location = new System.Drawing.Point(98, 15);
            this.txt_BassPicking.Name = "txt_BassPicking";
            this.txt_BassPicking.Size = new System.Drawing.Size(62, 20);
            this.txt_BassPicking.TabIndex = 96;
            // 
            // chbx_DD
            // 
            this.chbx_DD.AutoSize = true;
            this.chbx_DD.Enabled = false;
            this.chbx_DD.Location = new System.Drawing.Point(6, 39);
            this.chbx_DD.Name = "chbx_DD";
            this.chbx_DD.Size = new System.Drawing.Size(42, 17);
            this.chbx_DD.TabIndex = 45;
            this.chbx_DD.Text = "DD";
            this.chbx_DD.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(101, 37);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown1.TabIndex = 325;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // chbx_Original
            // 
            this.chbx_Original.AutoSize = true;
            this.chbx_Original.Location = new System.Drawing.Point(6, 97);
            this.chbx_Original.Name = "chbx_Original";
            this.chbx_Original.Size = new System.Drawing.Size(61, 17);
            this.chbx_Original.TabIndex = 44;
            this.chbx_Original.Text = "Original";
            this.chbx_Original.UseVisualStyleBackColor = true;
            // 
            // chbx_Sections
            // 
            this.chbx_Sections.AutoSize = true;
            this.chbx_Sections.Enabled = false;
            this.chbx_Sections.Location = new System.Drawing.Point(76, 97);
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
            this.chbx_Preview.Location = new System.Drawing.Point(6, 116);
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
            this.chbx_Author.Location = new System.Drawing.Point(6, 58);
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
            this.btn_RemoveBassDD.Location = new System.Drawing.Point(75, 16);
            this.btn_RemoveBassDD.Name = "btn_RemoveBassDD";
            this.btn_RemoveBassDD.Size = new System.Drawing.Size(21, 20);
            this.btn_RemoveBassDD.TabIndex = 85;
            this.btn_RemoveBassDD.Text = "-";
            this.btn_RemoveBassDD.UseVisualStyleBackColor = true;
            this.btn_RemoveBassDD.Click += new System.EventHandler(this.btn_RemoveBassDD_Click);
            // 
            // chbx_BassDD
            // 
            this.chbx_BassDD.AutoSize = true;
            this.chbx_BassDD.Enabled = false;
            this.chbx_BassDD.Location = new System.Drawing.Point(6, 19);
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
            this.chbx_Bonus.Location = new System.Drawing.Point(76, 59);
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
            this.chbx_Cover.Location = new System.Drawing.Point(6, 78);
            this.chbx_Cover.Name = "chbx_Cover";
            this.chbx_Cover.Size = new System.Drawing.Size(54, 17);
            this.chbx_Cover.TabIndex = 105;
            this.chbx_Cover.Text = "Cover";
            this.chbx_Cover.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_DuplicateFolder);
            this.groupBox3.Controls.Add(this.chbx_Avail_Duplicate);
            this.groupBox3.Controls.Add(this.btn_OldFolder);
            this.groupBox3.Controls.Add(this.chbx_Avail_Old);
            this.groupBox3.Controls.Add(this.chbx_Has_Been_Corrected);
            this.groupBox3.Location = new System.Drawing.Point(691, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(137, 86);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import Audit Trail";
            // 
            // btn_DuplicateFolder
            // 
            this.btn_DuplicateFolder.Enabled = false;
            this.btn_DuplicateFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DuplicateFolder.Location = new System.Drawing.Point(109, 64);
            this.btn_DuplicateFolder.Name = "btn_DuplicateFolder";
            this.btn_DuplicateFolder.Size = new System.Drawing.Size(21, 16);
            this.btn_DuplicateFolder.TabIndex = 327;
            this.btn_DuplicateFolder.Text = "->";
            this.btn_DuplicateFolder.UseVisualStyleBackColor = true;
            this.btn_DuplicateFolder.Click += new System.EventHandler(this.btn_DuplicateFolder_Click);
            // 
            // chbx_Avail_Duplicate
            // 
            this.chbx_Avail_Duplicate.AutoSize = true;
            this.chbx_Avail_Duplicate.Enabled = false;
            this.chbx_Avail_Duplicate.Location = new System.Drawing.Point(7, 64);
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
            this.btn_OldFolder.Location = new System.Drawing.Point(93, 18);
            this.btn_OldFolder.Name = "btn_OldFolder";
            this.btn_OldFolder.Size = new System.Drawing.Size(21, 15);
            this.btn_OldFolder.TabIndex = 326;
            this.btn_OldFolder.Text = "->";
            this.btn_OldFolder.UseVisualStyleBackColor = true;
            this.btn_OldFolder.Click += new System.EventHandler(this.btn_OldFolder_Click);
            // 
            // chbx_Avail_Old
            // 
            this.chbx_Avail_Old.AutoSize = true;
            this.chbx_Avail_Old.Enabled = false;
            this.chbx_Avail_Old.Location = new System.Drawing.Point(7, 18);
            this.chbx_Avail_Old.Name = "chbx_Avail_Old";
            this.chbx_Avail_Old.Size = new System.Drawing.Size(88, 17);
            this.chbx_Avail_Old.TabIndex = 112;
            this.chbx_Avail_Old.Text = "Old Available";
            this.chbx_Avail_Old.UseVisualStyleBackColor = true;
            this.chbx_Avail_Old.CheckedChanged += new System.EventHandler(this.chbx_Avail_Old_CheckedChanged);
            // 
            // chbx_Has_Been_Corrected
            // 
            this.chbx_Has_Been_Corrected.AutoSize = true;
            this.chbx_Has_Been_Corrected.Enabled = false;
            this.chbx_Has_Been_Corrected.Location = new System.Drawing.Point(7, 41);
            this.chbx_Has_Been_Corrected.Name = "chbx_Has_Been_Corrected";
            this.chbx_Has_Been_Corrected.Size = new System.Drawing.Size(122, 17);
            this.chbx_Has_Been_Corrected.TabIndex = 274;
            this.chbx_Has_Been_Corrected.Text = "Has Been Corrected";
            this.chbx_Has_Been_Corrected.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_MultiTrackType);
            this.groupBox2.Controls.Add(this.chbx_MultiTrack);
            this.groupBox2.Controls.Add(this.btn_SelectAll);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chbx_Broken);
            this.groupBox2.Controls.Add(this.chbx_Group);
            this.groupBox2.Controls.Add(this.chbx_Selected);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chbx_Beta);
            this.groupBox2.Controls.Add(this.btn_SelectNone);
            this.groupBox2.Controls.Add(this.txt_ID);
            this.groupBox2.Controls.Add(this.lbl_NoRec);
            this.groupBox2.Controls.Add(this.btn_Prev);
            this.groupBox2.Controls.Add(this.cmb_Filter);
            this.groupBox2.Controls.Add(this.btn_NextItem);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 124);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Song Section";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
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
            "No Drums",
            "No Vocal",
            "(No Guitars)",
            "Only Bass",
            "Only Lead",
            "Only Rhythm",
            "Only Drums",
            "Only Vocal",
            "(Only BackTrack)"});
            this.txt_MultiTrackType.Location = new System.Drawing.Point(133, 101);
            this.txt_MultiTrackType.Name = "txt_MultiTrackType";
            this.txt_MultiTrackType.Size = new System.Drawing.Size(65, 21);
            this.txt_MultiTrackType.TabIndex = 377;
            // 
            // chbx_MultiTrack
            // 
            this.chbx_MultiTrack.AutoSize = true;
            this.chbx_MultiTrack.Location = new System.Drawing.Point(131, 85);
            this.chbx_MultiTrack.Name = "chbx_MultiTrack";
            this.chbx_MultiTrack.Size = new System.Drawing.Size(64, 17);
            this.chbx_MultiTrack.TabIndex = 376;
            this.chbx_MultiTrack.Text = "MultiTrk";
            this.chbx_MultiTrack.UseVisualStyleBackColor = true;
            this.chbx_MultiTrack.CheckedChanged += new System.EventHandler(this.chbx_MultiTrack_CheckedChanged);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Location = new System.Drawing.Point(70, 83);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(58, 35);
            this.btn_SelectAll.TabIndex = 102;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.button14_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 324;
            this.label3.Text = "Group:";
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Location = new System.Drawing.Point(131, 69);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 53;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // chbx_Group
            // 
            this.chbx_Group.FormattingEnabled = true;
            this.chbx_Group.Location = new System.Drawing.Point(10, 59);
            this.chbx_Group.Name = "chbx_Group";
            this.chbx_Group.Size = new System.Drawing.Size(65, 21);
            this.chbx_Group.TabIndex = 323;
            // 
            // chbx_Selected
            // 
            this.chbx_Selected.AutoSize = true;
            this.chbx_Selected.Location = new System.Drawing.Point(131, 51);
            this.chbx_Selected.Name = "chbx_Selected";
            this.chbx_Selected.Size = new System.Drawing.Size(68, 17);
            this.chbx_Selected.TabIndex = 64;
            this.chbx_Selected.Text = "Selected";
            this.chbx_Selected.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 276;
            this.label1.Text = "Filter:";
            // 
            // chbx_Beta
            // 
            this.chbx_Beta.AutoSize = true;
            this.chbx_Beta.Location = new System.Drawing.Point(131, 33);
            this.chbx_Beta.Name = "chbx_Beta";
            this.chbx_Beta.Size = new System.Drawing.Size(48, 17);
            this.chbx_Beta.TabIndex = 82;
            this.chbx_Beta.Text = "Beta";
            this.chbx_Beta.UseVisualStyleBackColor = true;
            // 
            // btn_SelectNone
            // 
            this.btn_SelectNone.Location = new System.Drawing.Point(8, 83);
            this.btn_SelectNone.Name = "btn_SelectNone";
            this.btn_SelectNone.Size = new System.Drawing.Size(58, 35);
            this.btn_SelectNone.TabIndex = 320;
            this.btn_SelectNone.Text = "Select None";
            this.btn_SelectNone.UseVisualStyleBackColor = true;
            this.btn_SelectNone.Click += new System.EventHandler(this.btn_SelectNone_Click);
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(81, 43);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 95;
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Location = new System.Drawing.Point(8, 16);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(49, 29);
            this.lbl_NoRec.TabIndex = 113;
            this.lbl_NoRec.Text = "of Records";
            // 
            // btn_Prev
            // 
            this.btn_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Prev.Location = new System.Drawing.Point(83, 65);
            this.btn_Prev.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(18, 17);
            this.btn_Prev.TabIndex = 319;
            this.btn_Prev.Text = "<";
            this.btn_Prev.UseVisualStyleBackColor = true;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // cmb_Filter
            // 
            this.cmb_Filter.FormattingEnabled = true;
            this.cmb_Filter.Items.AddRange(new object[] {
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
            "Pc",
            "PS3",
            "Mac",
            "XBOX360"});
            this.cmb_Filter.Location = new System.Drawing.Point(114, 11);
            this.cmb_Filter.Name = "cmb_Filter";
            this.cmb_Filter.Size = new System.Drawing.Size(80, 21);
            this.cmb_Filter.TabIndex = 275;
            this.cmb_Filter.SelectedValueChanged += new System.EventHandler(this.cmb_Filter_SelectedValueChanged);
            // 
            // btn_NextItem
            // 
            this.btn_NextItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NextItem.Location = new System.Drawing.Point(103, 65);
            this.btn_NextItem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_NextItem.Name = "btn_NextItem";
            this.btn_NextItem.Size = new System.Drawing.Size(18, 17);
            this.btn_NextItem.TabIndex = 318;
            this.btn_NextItem.Text = ">";
            this.btn_NextItem.UseVisualStyleBackColor = true;
            this.btn_NextItem.Click += new System.EventHandler(this.btn_NextItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Volume);
            this.groupBox1.Controls.Add(this.txt_Preview_Volume);
            this.groupBox1.Controls.Add(this.btn_AddPreview);
            this.groupBox1.Controls.Add(this.txt_PreviewStart);
            this.groupBox1.Controls.Add(this.btn_PlayAudio);
            this.groupBox1.Controls.Add(this.btn_PlayPreview);
            this.groupBox1.Controls.Add(this.txt_AverageTempo);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.txt_OggPath);
            this.groupBox1.Controls.Add(this.txt_PreviewEnd);
            this.groupBox1.Controls.Add(this.txt_OggPreviewPath);
            this.groupBox1.Controls.Add(this.btn_SelectPreview);
            this.groupBox1.Location = new System.Drawing.Point(369, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 93);
            this.groupBox1.TabIndex = 325;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio Section";
            // 
            // txt_Volume
            // 
            this.txt_Volume.DecimalPlaces = 1;
            this.txt_Volume.Location = new System.Drawing.Point(75, 16);
            this.txt_Volume.Margin = new System.Windows.Forms.Padding(2);
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
            this.txt_Preview_Volume.Location = new System.Drawing.Point(182, 15);
            this.txt_Preview_Volume.Margin = new System.Windows.Forms.Padding(2);
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
            this.txt_Preview_Volume.Size = new System.Drawing.Size(39, 20);
            this.txt_Preview_Volume.TabIndex = 323;
            // 
            // btn_AddPreview
            // 
            this.btn_AddPreview.Enabled = false;
            this.btn_AddPreview.Location = new System.Drawing.Point(216, 36);
            this.btn_AddPreview.Name = "btn_AddPreview";
            this.btn_AddPreview.Size = new System.Drawing.Size(95, 22);
            this.btn_AddPreview.TabIndex = 94;
            this.btn_AddPreview.Text = "Add Preview";
            this.btn_AddPreview.UseVisualStyleBackColor = true;
            this.btn_AddPreview.Click += new System.EventHandler(this.button13_Click);
            // 
            // txt_PreviewStart
            // 
            this.txt_PreviewStart.CustomFormat = "mm:ss";
            this.txt_PreviewStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_PreviewStart.Location = new System.Drawing.Point(113, 38);
            this.txt_PreviewStart.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PreviewStart.Name = "txt_PreviewStart";
            this.txt_PreviewStart.ShowUpDown = true;
            this.txt_PreviewStart.Size = new System.Drawing.Size(62, 20);
            this.txt_PreviewStart.TabIndex = 322;
            this.txt_PreviewStart.Value = new System.DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // btn_PlayAudio
            // 
            this.btn_PlayAudio.Location = new System.Drawing.Point(6, 14);
            this.btn_PlayAudio.Name = "btn_PlayAudio";
            this.btn_PlayAudio.Size = new System.Drawing.Size(68, 21);
            this.btn_PlayAudio.TabIndex = 87;
            this.btn_PlayAudio.Text = "Audio";
            this.btn_PlayAudio.UseVisualStyleBackColor = true;
            this.btn_PlayAudio.Click += new System.EventHandler(this.button11_Click);
            // 
            // btn_PlayPreview
            // 
            this.btn_PlayPreview.Enabled = false;
            this.btn_PlayPreview.Location = new System.Drawing.Point(119, 14);
            this.btn_PlayPreview.Name = "btn_PlayPreview";
            this.btn_PlayPreview.Size = new System.Drawing.Size(62, 21);
            this.btn_PlayPreview.TabIndex = 88;
            this.btn_PlayPreview.Text = "Preview";
            this.btn_PlayPreview.UseVisualStyleBackColor = true;
            this.btn_PlayPreview.Click += new System.EventHandler(this.btm_PlayPreview_Click);
            // 
            // txt_AverageTempo
            // 
            this.txt_AverageTempo.Cue = "Avg. Tempo";
            this.txt_AverageTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AverageTempo.ForeColor = System.Drawing.Color.Gray;
            this.txt_AverageTempo.Location = new System.Drawing.Point(226, 15);
            this.txt_AverageTempo.Name = "txt_AverageTempo";
            this.txt_AverageTempo.Size = new System.Drawing.Size(44, 20);
            this.txt_AverageTempo.TabIndex = 90;
            this.txt_AverageTempo.TextChanged += new System.EventHandler(this.txt_AverageTempo_TextChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(9, 38);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(68, 17);
            this.checkBox2.TabIndex = 291;
            this.checkBox2.Text = "AutoPlay";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // txt_OggPath
            // 
            this.txt_OggPath.Cue = "Ogg Path";
            this.txt_OggPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_OggPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_OggPath.Location = new System.Drawing.Point(7, 64);
            this.txt_OggPath.Multiline = true;
            this.txt_OggPath.Name = "txt_OggPath";
            this.txt_OggPath.ReadOnly = true;
            this.txt_OggPath.Size = new System.Drawing.Size(99, 20);
            this.txt_OggPath.TabIndex = 312;
            this.txt_OggPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_PreviewEnd
            // 
            this.txt_PreviewEnd.Location = new System.Drawing.Point(177, 38);
            this.txt_PreviewEnd.Margin = new System.Windows.Forms.Padding(2);
            this.txt_PreviewEnd.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txt_PreviewEnd.Name = "txt_PreviewEnd";
            this.txt_PreviewEnd.Size = new System.Drawing.Size(37, 20);
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
            this.txt_OggPreviewPath.Location = new System.Drawing.Point(110, 64);
            this.txt_OggPreviewPath.Name = "txt_OggPreviewPath";
            this.txt_OggPreviewPath.Size = new System.Drawing.Size(99, 20);
            this.txt_OggPreviewPath.TabIndex = 313;
            this.txt_OggPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_SelectPreview
            // 
            this.btn_SelectPreview.Enabled = false;
            this.btn_SelectPreview.Location = new System.Drawing.Point(216, 61);
            this.btn_SelectPreview.Name = "btn_SelectPreview";
            this.btn_SelectPreview.Size = new System.Drawing.Size(95, 22);
            this.btn_SelectPreview.TabIndex = 315;
            this.btn_SelectPreview.Text = "Change Preview";
            this.btn_SelectPreview.UseVisualStyleBackColor = true;
            this.btn_SelectPreview.Click += new System.EventHandler(this.btn_SelectPreview_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(136, 199);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(65, 35);
            this.button5.TabIndex = 278;
            this.button5.Text = "Open Retail DB";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 277;
            this.label2.Text = "Description:";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1019, 82);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(84, 20);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button15.Location = new System.Drawing.Point(10, 199);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(122, 35);
            this.button15.TabIndex = 110;
            this.button15.Text = "Open Standarization DB";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(834, 137);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(113, 20);
            this.txt_AlbumArtPath.TabIndex = 107;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Enabled = false;
            this.btn_ChangeCover.Location = new System.Drawing.Point(834, 161);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(113, 22);
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
            this.txt_Artist_ShortName.Location = new System.Drawing.Point(435, 3);
            this.txt_Artist_ShortName.Name = "txt_Artist_ShortName";
            this.txt_Artist_ShortName.Size = new System.Drawing.Size(51, 20);
            this.txt_Artist_ShortName.TabIndex = 104;
            // 
            // txt_Album_ShortName
            // 
            this.txt_Album_ShortName.Cue = "Short";
            this.txt_Album_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_ShortName.Location = new System.Drawing.Point(367, 91);
            this.txt_Album_ShortName.Name = "txt_Album_ShortName";
            this.txt_Album_ShortName.Size = new System.Drawing.Size(62, 20);
            this.txt_Album_ShortName.TabIndex = 103;
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Enabled = false;
            this.chbx_AutoSave.Location = new System.Drawing.Point(947, 63);
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
            this.txt_Album_Year.Location = new System.Drawing.Point(432, 91);
            this.txt_Album_Year.Name = "txt_Album_Year";
            this.txt_Album_Year.Size = new System.Drawing.Size(45, 20);
            this.txt_Album_Year.TabIndex = 98;
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(819, 5);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(128, 128);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 91;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Enabled = false;
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(1019, 55);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(84, 26);
            this.btn_Save.TabIndex = 81;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // txt_APP_ID
            // 
            this.txt_APP_ID.Cue = "App ID";
            this.txt_APP_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_APP_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_APP_ID.Location = new System.Drawing.Point(367, 115);
            this.txt_APP_ID.Name = "txt_APP_ID";
            this.txt_APP_ID.Size = new System.Drawing.Size(73, 20);
            this.txt_APP_ID.TabIndex = 80;
            // 
            // txt_DLC_ID
            // 
            this.txt_DLC_ID.Cue = "DLC ID";
            this.txt_DLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLC_ID.Location = new System.Drawing.Point(207, 114);
            this.txt_DLC_ID.Name = "txt_DLC_ID";
            this.txt_DLC_ID.Size = new System.Drawing.Size(156, 20);
            this.txt_DLC_ID.TabIndex = 79;
            // 
            // txt_Version
            // 
            this.txt_Version.Cue = "Version";
            this.txt_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Version.ForeColor = System.Drawing.Color.Gray;
            this.txt_Version.Location = new System.Drawing.Point(503, 47);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(42, 20);
            this.txt_Version.TabIndex = 73;
            // 
            // txt_Author
            // 
            this.txt_Author.Cue = "Author";
            this.txt_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Author.ForeColor = System.Drawing.Color.Gray;
            this.txt_Author.Location = new System.Drawing.Point(491, 24);
            this.txt_Author.Name = "txt_Author";
            this.txt_Author.Size = new System.Drawing.Size(156, 20);
            this.txt_Author.TabIndex = 72;
            this.txt_Author.TextChanged += new System.EventHandler(this.txt_Author_TextChanged);
            this.txt_Author.Leave += new System.EventHandler(this.txt_Author_Leave);
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(207, 91);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(156, 20);
            this.txt_Album.TabIndex = 69;
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Cue = "Title Sort";
            this.txt_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title_Sort.Location = new System.Drawing.Point(207, 69);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(222, 20);
            this.txt_Title_Sort.TabIndex = 68;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(207, 47);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(222, 20);
            this.txt_Title.TabIndex = 67;
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Cue = "Artist  Sort";
            this.txt_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Sort.Location = new System.Drawing.Point(207, 25);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist_Sort.TabIndex = 66;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(207, 3);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist.TabIndex = 65;
            // 
            // btn_Duplicate
            // 
            this.btn_Duplicate.Enabled = false;
            this.btn_Duplicate.Location = new System.Drawing.Point(970, 2);
            this.btn_Duplicate.Name = "btn_Duplicate";
            this.btn_Duplicate.Size = new System.Drawing.Size(133, 26);
            this.btn_Duplicate.TabIndex = 58;
            this.btn_Duplicate.Text = "Duplicate";
            this.btn_Duplicate.UseVisualStyleBackColor = true;
            this.btn_Duplicate.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_SearchReset
            // 
            this.btn_SearchReset.Location = new System.Drawing.Point(435, 53);
            this.btn_SearchReset.Name = "btn_SearchReset";
            this.btn_SearchReset.Size = new System.Drawing.Size(51, 32);
            this.btn_SearchReset.TabIndex = 56;
            this.btn_SearchReset.Text = "Reset";
            this.btn_SearchReset.UseVisualStyleBackColor = true;
            this.btn_SearchReset.Click += new System.EventHandler(this.button6_Click);
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(207, 157);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(156, 77);
            this.txt_Description.TabIndex = 54;
            this.txt_Description.Text = "";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(435, 24);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(51, 29);
            this.btn_Search.TabIndex = 51;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // chbx_Alternate
            // 
            this.chbx_Alternate.AutoSize = true;
            this.chbx_Alternate.Location = new System.Drawing.Point(551, 50);
            this.chbx_Alternate.Name = "chbx_Alternate";
            this.chbx_Alternate.Size = new System.Drawing.Size(68, 17);
            this.chbx_Alternate.TabIndex = 46;
            this.chbx_Alternate.Text = "Alternate";
            this.chbx_Alternate.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Location = new System.Drawing.Point(970, 28);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(133, 26);
            this.btn_Delete.TabIndex = 40;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 53);
            this.button1.TabIndex = 37;
            this.button1.Text = "Open DB in M$ Access";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Tones
            // 
            this.btn_Tones.Location = new System.Drawing.Point(10, 175);
            this.btn_Tones.Name = "btn_Tones";
            this.btn_Tones.Size = new System.Drawing.Size(122, 23);
            this.btn_Tones.TabIndex = 36;
            this.btn_Tones.Text = "Open Tones";
            this.btn_Tones.UseVisualStyleBackColor = true;
            this.btn_Tones.Click += new System.EventHandler(this.btn_Tones_Click);
            // 
            // btn_Arrangements
            // 
            this.btn_Arrangements.Location = new System.Drawing.Point(10, 148);
            this.btn_Arrangements.Name = "btn_Arrangements";
            this.btn_Arrangements.Size = new System.Drawing.Size(122, 26);
            this.btn_Arrangements.TabIndex = 35;
            this.btn_Arrangements.Text = "Open Arrangements";
            this.btn_Arrangements.UseVisualStyleBackColor = true;
            this.btn_Arrangements.Click += new System.EventHandler(this.btn_Arrangements_Click);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Location = new System.Drawing.Point(-153, 96);
            this.CheckBox1.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(139, 17);
            this.CheckBox1.TabIndex = 34;
            this.CheckBox1.Text = "Show only MessageBox";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // DataViewGrid
            // 
            this.DataViewGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataViewGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataViewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataViewGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.DataViewGrid.Location = new System.Drawing.Point(0, -2);
            this.DataViewGrid.Name = "DataViewGrid";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataViewGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DataViewGrid.Size = new System.Drawing.Size(1106, 353);
            this.DataViewGrid.TabIndex = 2;
            this.DataViewGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataViewGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataViewGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataViewGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataViewGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataViewGrid.SelectionChanged += new System.EventHandler(this.ChangeEdit);
            // 
            // toolTip1
            // 
            this.toolTip1.Tag = "hey";
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "test";
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // MainDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1109, 640);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.DataViewGrid);
            this.Name = "MainDB";
            this.Text = "MainDB";
            this.Load += new System.EventHandler(this.MainDB_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Followers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CustomsForge_Like)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Alt_No)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Track_No)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.DataViewGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.DataGridView DataViewGrid;
        private System.Windows.Forms.Button btn_Duplicate;
        private System.Windows.Forms.CheckBox chbx_Preview;
        private System.Windows.Forms.Button btn_SearchReset;
        private System.Windows.Forms.CheckBox chbx_Broken;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.CheckBox chbx_Sections;
        private System.Windows.Forms.CheckBox chbx_Alternate;
        private System.Windows.Forms.CheckBox chbx_DD;
        private System.Windows.Forms.CheckBox chbx_Original;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Package;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Tones;
        private System.Windows.Forms.Button btn_Arrangements;
        internal System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.CheckBox chbx_Selected;
        private CueTextBox txt_Tuning;
        private System.Windows.Forms.CheckBox chbx_Rhythm;
        private System.Windows.Forms.CheckBox chbx_Combo;
        private System.Windows.Forms.CheckBox chbx_Bass;
        private System.Windows.Forms.CheckBox chbx_Lead;
        private CueTextBox txt_Version;
        private CueTextBox txt_Author;
        private CueTextBox txt_Album;
        private CueTextBox txt_Title_Sort;
        private CueTextBox txt_Title;
        private CueTextBox txt_Artist_Sort;
        private CueTextBox txt_Artist;
        private CueTextBox txt_APP_ID;
        private CueTextBox txt_DLC_ID;
        private System.Windows.Forms.RichTextBox txt_Description;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.CheckBox chbx_Beta;
        private System.Windows.Forms.Button btn_RemoveBassDD;
        private System.Windows.Forms.Button btn_PlayPreview;
        private System.Windows.Forms.Button btn_PlayAudio;
        private System.Windows.Forms.Button btn_AddPreview;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_AverageTempo;
        private CueTextBox txt_ID;
        private CueTextBox txt_BassPicking;
        private CueTextBox txt_Album_Year;
        private System.Windows.Forms.CheckBox chbx_BassDD;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
        private System.Windows.Forms.CheckBox chbx_Bonus;
        private System.Windows.Forms.Button btn_SelectAll;
        private CueTextBox txt_Artist_ShortName;
        private CueTextBox txt_Album_ShortName;
        private System.Windows.Forms.CheckBox chbx_Cover;
        private System.Windows.Forms.Button btn_ChangeCover;
        private ComboBox chbx_Format;
        private Button button15;
        private CheckBox chbx_Avail_Old;
        private CheckBox chbx_Avail_Duplicate;
        private Label lbl_NoRec;
        private Button btn_Close;
        private CheckBox chbx_Has_Been_Corrected;
        private Label label1;
        private ComboBox cmb_Filter;
        private Label label2;
        private Button button5;
        private CheckBox checkBox2;
        private Button btn_SteamDLCFolder;
        private CueTextBox txt_FTPPath;
        private CueTextBox txt_OggPreviewPath;
        private CheckBox chbx_Author;
        private Button btn_SelectPreview;
        private NumericUpDown txt_PreviewEnd;
        private Button btn_Prev;
        private Button btn_NextItem;
        private Button btn_SelectNone;
        private DateTimePicker txt_PreviewStart;
        private ComboBox chbx_Group;
        private ToolTip toolTip1;
        private GroupBox groupBox1;
        private Label label3;
        private GroupBox groupBox2;
        private NumericUpDown numericUpDown1;
        private NumericUpDown txt_Volume;
        private NumericUpDown txt_Preview_Volume;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private Button btn_DuplicateFolder;
        private Button btn_OldFolder;
        private Button btn_AddDD;
        private Button btn_RemoveDD;
        private ComboBox chbx_PreSavedFTP;
        private CheckBox chbx_CopyOriginal;
        private CheckBox chbx_Copy;
        private GroupBox groupBox6;
        private Button button2;
        private CueTextBox txt_Platform;
        private RichTextBox txt_debug;
        private Label label4;
        private Button bth_GetTrackNo;
        private CheckBox chbx_UniqueID;
        private CheckBox chbx_TrackNo;
        private ProgressBar pB_ReadDLCs;
        private CheckBox chbx_RemoveBassDD;
        private NumericUpDown txt_Track_No;
        public CueTextBox txt_OggPath;
        private NumericUpDown txt_Alt_No;
        private CheckBox chbx_Lyrics;
        private Button btn_OpenSongFolder;
        private Label label6;
        private Label label5;
        private NumericUpDown txt_Rating;
        private Label label7;
        private Label label8;
        private NumericUpDown numericUpDown2;
        private ComboBox txt_MultiTrackType;
        private CheckBox chbx_MultiTrack;
        private GroupBox groupBox7;
        private Button btn_CustomForge_Link;
        private Button btn_Youtube;
        private Label label59;
        private CueTextBox txt_YouTube_Link;
        private CueTextBox txt_CustomsForge_Link;
        private Label lbfl_YouTube_Link;
        private Label label33;
        private Label label32;
        private Button btn_Playthrough;
        private CueTextBox txt_Playthough;
        private Label label10;
        private Button btn_Like;
        private NumericUpDown txt_Followers;
        private Label label9;
        private NumericUpDown txt_CustomsForge_Like;
        private Button btn_Followers;
        private NumericUpDown numericUpDown3;
        private Label label11;
        private RichTextBox txt_CustomsForge_ReleaseNotes;
        private Button btn_AddSections;
        private Button button3;
        public CueTextBox txt_AlbumArtPath;
        private Button button4;
    }
}