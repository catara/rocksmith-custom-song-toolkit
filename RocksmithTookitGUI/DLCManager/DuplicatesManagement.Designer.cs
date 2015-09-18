using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RocksmithToolkitLib;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.DLCPackage;
using Ookii.Dialogs; //cue text

namespace RocksmithToolkitGUI.DLCManager
{
    partial class Duplicates_Management
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
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Comment = new System.Windows.Forms.RichTextBox();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_AlbumArt = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.picbx_AlbumArtPathNew = new System.Windows.Forms.PictureBox();
            this.picbx_AlbumArtPathExisting = new System.Windows.Forms.PictureBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.chbx_IsAlternateNew = new System.Windows.Forms.CheckBox();
            this.btn_Alternate = new System.Windows.Forms.Button();
            this.btn_Ignore = new System.Windows.Forms.Button();
            this.lbl_TitleSort = new System.Windows.Forms.Label();
            this.lbl_ArtistSort = new System.Windows.Forms.Label();
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.lbl_IsOriginal = new System.Windows.Forms.Label();
            this.lbl_Toolkit = new System.Windows.Forms.Label();
            this.lbl_Author = new System.Windows.Forms.Label();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.lbl_Tuning = new System.Windows.Forms.Label();
            this.lbl_DLCID = new System.Windows.Forms.Label();
            this.lbl_DD = new System.Windows.Forms.Label();
            this.lbl_AvailableTracks = new System.Windows.Forms.Label();
            this.lbl_Audio = new System.Windows.Forms.Label();
            this.lbl_Preview = new System.Windows.Forms.Label();
            this.lbl_XMLLead = new System.Windows.Forms.Label();
            this.lbl_XMLBass = new System.Windows.Forms.Label();
            this.lbl_XMLCombo = new System.Windows.Forms.Label();
            this.lbl_XMLRhythm = new System.Windows.Forms.Label();
            this.lbl_JSONLead = new System.Windows.Forms.Label();
            this.lbl_JSONBass = new System.Windows.Forms.Label();
            this.lbl_JSONCombo = new System.Windows.Forms.Label();
            this.lbl_JSONRhythm = new System.Windows.Forms.Label();
            this.lbl_Reference = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chbx_IsOriginal = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_Is_Original = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btn_UpdateExisting = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_IDExisting = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btn_DecompressAll = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lbl_diffCount = new System.Windows.Forms.Label();
            this.chbx_IgnoreDupli = new System.Windows.Forms.CheckBox();
            this.btn_RemoveOldNew = new System.Windows.Forms.Button();
            this.lbl_Vocals = new System.Windows.Forms.Label();
            this.lbl_txt_Vocals = new System.Windows.Forms.Label();
            this.chbx_Autosave = new System.Windows.Forms.CheckBox();
            this.btn_TitleNew = new System.Windows.Forms.Button();
            this.btn_TitleExisting = new System.Windows.Forms.Button();
            this.btn_TitleSortNew = new System.Windows.Forms.Button();
            this.btn_TitleSortExisting = new System.Windows.Forms.Button();
            this.btn_AuthorNew = new System.Windows.Forms.Button();
            this.btn_AuthorExisting = new System.Windows.Forms.Button();
            this.lbl_Album = new System.Windows.Forms.Label();
            this.lbl_Artist = new System.Windows.Forms.Label();
            this.btn_AlbumNew = new System.Windows.Forms.Button();
            this.btn_AlbumExisting = new System.Windows.Forms.Button();
            this.btn_ArtistNew = new System.Windows.Forms.Button();
            this.btn_ArtistExisting = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_ArtistSortNew = new System.Windows.Forms.Button();
            this.btn_ArtistSortExisting = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_GoToNew = new System.Windows.Forms.Button();
            this.btn_GoToExisting = new System.Windows.Forms.Button();
            this.btn_AddAge = new System.Windows.Forms.Button();
            this.lbl_Existing = new System.Windows.Forms.Label();
            this.txt_JSONLeadExisting = new RocksmithToolkitGUI.CueTextBox();
            this.lbl_New = new System.Windows.Forms.Label();
            this.txt_XMLLeadNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLLeadExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLBassNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLBassExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLComboNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLComboExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLRhythmNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLRhythmExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONLeadNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONBassNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONBassExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONComboNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONComboExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONRhythmNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONRhythmExisting = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_IsAlternateExisting = new System.Windows.Forms.CheckBox();
            this.txt_AlternateNoExisting = new System.Windows.Forms.NumericUpDown();
            this.txt_AlternateNoNew = new System.Windows.Forms.NumericUpDown();
            this.txt_AlbumExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArtistExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VocalsExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VocalsNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AlbumNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VersionExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VersionNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FileNameExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FileNameNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArtistNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AvailTracksExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AvailTracksNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DDExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DDNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DLCIDExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DLCIDNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TuningExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TuningNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AuthorExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AuthorNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_IsOriginalExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_IsOriginalNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ToolkitExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ToolkitNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TitleSortExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TitleSortNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArtistSortExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArtistSortNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TitleExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TitleNew = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_MultiTrackExisting = new System.Windows.Forms.CheckBox();
            this.chbx_MultiTrackNew = new System.Windows.Forms.CheckBox();
            this.txt_MultiTrackNew = new System.Windows.Forms.ComboBox();
            this.txt_MultiTrackExisting = new System.Windows.Forms.ComboBox();
            this.btn_CoverNew = new System.Windows.Forms.Button();
            this.btn_CoverExisting = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_PlayPreviewNew = new System.Windows.Forms.Button();
            this.btn_PlayAudioNew = new System.Windows.Forms.Button();
            this.btn_PlayPreviewExisting = new System.Windows.Forms.Button();
            this.btn_PlayAudioExisting = new System.Windows.Forms.Button();
            this.btn_AddDD = new System.Windows.Forms.Button();
            this.btn_AddTracks = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbl_CustomsForge_ReleaseNotes = new System.Windows.Forms.Label();
            this.txt_CustomsForge_ReleaseNotesNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CustomsForge_ReleaseNotesExisting = new RocksmithToolkitGUI.CueTextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txt_YouTube_LinkNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CustomsForge_LinkExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_YouTube_LinkExisting = new RocksmithToolkitGUI.CueTextBox();
            this.lbl_CustomsForge_Like = new System.Windows.Forms.Label();
            this.txt_CustomsForge_LinkNew = new RocksmithToolkitGUI.CueTextBox();
            this.lbl_CustomsForge_LinkNew = new System.Windows.Forms.Label();
            this.lbl_YouTube_LinkNew = new System.Windows.Forms.Label();
            this.txt_CustomsForge_LikeNew = new RocksmithToolkitGUI.CueTextBox();
            this.lbfl_YouTube_Link = new System.Windows.Forms.Label();
            this.txt_CustomsForge_LikeExisting = new RocksmithToolkitGUI.CueTextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.btn_AddTunning = new System.Windows.Forms.Button();
            this.btn_AddVersion1 = new System.Windows.Forms.Button();
            this.btn_AddAuthor = new System.Windows.Forms.Button();
            this.lblSoye = new System.Windows.Forms.Label();
            this.txt_SizeExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_SizeNew = new RocksmithToolkitGUI.CueTextBox();
            this.lbl_Size = new System.Windows.Forms.Label();
            this.lbl_Multitrack = new System.Windows.Forms.Label();
            this.chbx_UseBrakets = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathExisting)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AlternateNoExisting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AlternateNoNew)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(57, 208);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 283;
            this.label16.Text = "JSON Rhythm";
            // 
            // txt_Comment
            // 
            this.txt_Comment.Location = new System.Drawing.Point(604, 263);
            this.txt_Comment.Name = "txt_Comment";
            this.txt_Comment.Size = new System.Drawing.Size(156, 69);
            this.txt_Comment.TabIndex = 277;
            this.txt_Comment.Text = "";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(604, 177);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(156, 69);
            this.txt_Description.TabIndex = 276;
            this.txt_Description.Text = "";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(117, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 13);
            this.label15.TabIndex = 275;
            this.label15.Text = "Avaialble Tracks";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(90, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 274;
            this.label14.Text = "Preview";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(101, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 273;
            this.label13.Text = "Audio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(75, 34);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 272;
            this.label12.Text = "XML Lead";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(76, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 271;
            this.label11.Text = "XML Bass";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(66, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 270;
            this.label10.Text = "XML Combo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(63, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 269;
            this.label9.Text = "XML Rhythm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(69, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 267;
            this.label7.Text = "JSON Lead";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 266;
            this.label6.Text = "JSON Bass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 265;
            this.label5.Text = "JSON Combo";
            // 
            // lbl_AlbumArt
            // 
            this.lbl_AlbumArt.AutoSize = true;
            this.lbl_AlbumArt.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_AlbumArt.Location = new System.Drawing.Point(571, 412);
            this.lbl_AlbumArt.Name = "lbl_AlbumArt";
            this.lbl_AlbumArt.Size = new System.Drawing.Size(22, 13);
            this.lbl_AlbumArt.TabIndex = 258;
            this.lbl_AlbumArt.Text = "Vs.";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Title.Location = new System.Drawing.Point(262, 107);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(22, 13);
            this.lbl_Title.TabIndex = 218;
            this.lbl_Title.Text = "Vs.";
            // 
            // picbx_AlbumArtPathNew
            // 
            this.picbx_AlbumArtPathNew.Location = new System.Drawing.Point(405, 353);
            this.picbx_AlbumArtPathNew.Name = "picbx_AlbumArtPathNew";
            this.picbx_AlbumArtPathNew.Size = new System.Drawing.Size(166, 145);
            this.picbx_AlbumArtPathNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPathNew.TabIndex = 216;
            this.picbx_AlbumArtPathNew.TabStop = false;
            // 
            // picbx_AlbumArtPathExisting
            // 
            this.picbx_AlbumArtPathExisting.Location = new System.Drawing.Point(593, 353);
            this.picbx_AlbumArtPathExisting.Name = "picbx_AlbumArtPathExisting";
            this.picbx_AlbumArtPathExisting.Size = new System.Drawing.Size(167, 146);
            this.picbx_AlbumArtPathExisting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPathExisting.TabIndex = 215;
            this.picbx_AlbumArtPathExisting.TabStop = false;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(640, 122);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(120, 35);
            this.btn_Update.TabIndex = 214;
            this.btn_Update.Text = "Update and Overrite";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // chbx_IsAlternateNew
            // 
            this.chbx_IsAlternateNew.AutoSize = true;
            this.chbx_IsAlternateNew.Checked = true;
            this.chbx_IsAlternateNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_IsAlternateNew.Location = new System.Drawing.Point(53, 205);
            this.chbx_IsAlternateNew.Name = "chbx_IsAlternateNew";
            this.chbx_IsAlternateNew.Size = new System.Drawing.Size(68, 17);
            this.chbx_IsAlternateNew.TabIndex = 211;
            this.chbx_IsAlternateNew.Text = "Alternate";
            this.chbx_IsAlternateNew.UseVisualStyleBackColor = true;
            this.chbx_IsAlternateNew.CheckedChanged += new System.EventHandler(this.chbx_IsAlternateNew_CheckedChanged);
            // 
            // btn_Alternate
            // 
            this.btn_Alternate.Location = new System.Drawing.Point(640, 54);
            this.btn_Alternate.Name = "btn_Alternate";
            this.btn_Alternate.Size = new System.Drawing.Size(120, 35);
            this.btn_Alternate.TabIndex = 212;
            this.btn_Alternate.Text = "Import as Alternate";
            this.btn_Alternate.UseVisualStyleBackColor = true;
            this.btn_Alternate.Click += new System.EventHandler(this.btn_Alternate_Click);
            // 
            // btn_Ignore
            // 
            this.btn_Ignore.Location = new System.Drawing.Point(640, 88);
            this.btn_Ignore.Name = "btn_Ignore";
            this.btn_Ignore.Size = new System.Drawing.Size(120, 35);
            this.btn_Ignore.TabIndex = 213;
            this.btn_Ignore.Text = "Ignore as OLD/Duplicate";
            this.btn_Ignore.UseVisualStyleBackColor = true;
            this.btn_Ignore.Click += new System.EventHandler(this.btn_Ignore_Click);
            // 
            // lbl_TitleSort
            // 
            this.lbl_TitleSort.AutoSize = true;
            this.lbl_TitleSort.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_TitleSort.Location = new System.Drawing.Point(262, 131);
            this.lbl_TitleSort.Name = "lbl_TitleSort";
            this.lbl_TitleSort.Size = new System.Drawing.Size(22, 13);
            this.lbl_TitleSort.TabIndex = 284;
            this.lbl_TitleSort.Text = "Vs.";
            // 
            // lbl_ArtistSort
            // 
            this.lbl_ArtistSort.AutoSize = true;
            this.lbl_ArtistSort.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_ArtistSort.Location = new System.Drawing.Point(262, 157);
            this.lbl_ArtistSort.Name = "lbl_ArtistSort";
            this.lbl_ArtistSort.Size = new System.Drawing.Size(22, 13);
            this.lbl_ArtistSort.TabIndex = 285;
            this.lbl_ArtistSort.Text = "Vs.";
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.AutoSize = true;
            this.lbl_FileName.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_FileName.Location = new System.Drawing.Point(262, 182);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(22, 13);
            this.lbl_FileName.TabIndex = 286;
            this.lbl_FileName.Text = "Vs.";
            // 
            // lbl_IsOriginal
            // 
            this.lbl_IsOriginal.AutoSize = true;
            this.lbl_IsOriginal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_IsOriginal.Location = new System.Drawing.Point(262, 207);
            this.lbl_IsOriginal.Name = "lbl_IsOriginal";
            this.lbl_IsOriginal.Size = new System.Drawing.Size(22, 13);
            this.lbl_IsOriginal.TabIndex = 287;
            this.lbl_IsOriginal.Text = "Vs.";
            // 
            // lbl_Toolkit
            // 
            this.lbl_Toolkit.AutoSize = true;
            this.lbl_Toolkit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Toolkit.Location = new System.Drawing.Point(262, 231);
            this.lbl_Toolkit.Name = "lbl_Toolkit";
            this.lbl_Toolkit.Size = new System.Drawing.Size(22, 13);
            this.lbl_Toolkit.TabIndex = 288;
            this.lbl_Toolkit.Text = "Vs.";
            // 
            // lbl_Author
            // 
            this.lbl_Author.AutoSize = true;
            this.lbl_Author.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Author.Location = new System.Drawing.Point(262, 256);
            this.lbl_Author.Name = "lbl_Author";
            this.lbl_Author.Size = new System.Drawing.Size(22, 13);
            this.lbl_Author.TabIndex = 289;
            this.lbl_Author.Text = "Vs.";
            // 
            // lbl_Version
            // 
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Version.Location = new System.Drawing.Point(262, 280);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(22, 13);
            this.lbl_Version.TabIndex = 290;
            this.lbl_Version.Text = "Vs.";
            // 
            // lbl_Tuning
            // 
            this.lbl_Tuning.AutoSize = true;
            this.lbl_Tuning.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Tuning.Location = new System.Drawing.Point(262, 334);
            this.lbl_Tuning.Name = "lbl_Tuning";
            this.lbl_Tuning.Size = new System.Drawing.Size(22, 13);
            this.lbl_Tuning.TabIndex = 291;
            this.lbl_Tuning.Text = "Vs.";
            // 
            // lbl_DLCID
            // 
            this.lbl_DLCID.AutoSize = true;
            this.lbl_DLCID.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_DLCID.Location = new System.Drawing.Point(262, 357);
            this.lbl_DLCID.Name = "lbl_DLCID";
            this.lbl_DLCID.Size = new System.Drawing.Size(22, 13);
            this.lbl_DLCID.TabIndex = 292;
            this.lbl_DLCID.Text = "Vs.";
            // 
            // lbl_DD
            // 
            this.lbl_DD.AutoSize = true;
            this.lbl_DD.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_DD.Location = new System.Drawing.Point(254, 16);
            this.lbl_DD.Name = "lbl_DD";
            this.lbl_DD.Size = new System.Drawing.Size(22, 13);
            this.lbl_DD.TabIndex = 293;
            this.lbl_DD.Text = "Vs.";
            // 
            // lbl_AvailableTracks
            // 
            this.lbl_AvailableTracks.AutoSize = true;
            this.lbl_AvailableTracks.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_AvailableTracks.Location = new System.Drawing.Point(254, 42);
            this.lbl_AvailableTracks.Name = "lbl_AvailableTracks";
            this.lbl_AvailableTracks.Size = new System.Drawing.Size(22, 13);
            this.lbl_AvailableTracks.TabIndex = 294;
            this.lbl_AvailableTracks.Text = "Vs.";
            // 
            // lbl_Audio
            // 
            this.lbl_Audio.AutoSize = true;
            this.lbl_Audio.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Audio.Location = new System.Drawing.Point(254, 68);
            this.lbl_Audio.Name = "lbl_Audio";
            this.lbl_Audio.Size = new System.Drawing.Size(22, 13);
            this.lbl_Audio.TabIndex = 295;
            this.lbl_Audio.Text = "Vs.";
            // 
            // lbl_Preview
            // 
            this.lbl_Preview.AutoSize = true;
            this.lbl_Preview.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Preview.Location = new System.Drawing.Point(254, 94);
            this.lbl_Preview.Name = "lbl_Preview";
            this.lbl_Preview.Size = new System.Drawing.Size(22, 13);
            this.lbl_Preview.TabIndex = 296;
            this.lbl_Preview.Text = "Vs.";
            // 
            // lbl_XMLLead
            // 
            this.lbl_XMLLead.AutoSize = true;
            this.lbl_XMLLead.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLLead.Location = new System.Drawing.Point(252, 34);
            this.lbl_XMLLead.Name = "lbl_XMLLead";
            this.lbl_XMLLead.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLLead.TabIndex = 297;
            this.lbl_XMLLead.Text = "Vs.";
            // 
            // lbl_XMLBass
            // 
            this.lbl_XMLBass.AutoSize = true;
            this.lbl_XMLBass.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLBass.Location = new System.Drawing.Point(252, 59);
            this.lbl_XMLBass.Name = "lbl_XMLBass";
            this.lbl_XMLBass.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLBass.TabIndex = 298;
            this.lbl_XMLBass.Text = "Vs.";
            // 
            // lbl_XMLCombo
            // 
            this.lbl_XMLCombo.AutoSize = true;
            this.lbl_XMLCombo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLCombo.Location = new System.Drawing.Point(252, 83);
            this.lbl_XMLCombo.Name = "lbl_XMLCombo";
            this.lbl_XMLCombo.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLCombo.TabIndex = 299;
            this.lbl_XMLCombo.Text = "Vs.";
            // 
            // lbl_XMLRhythm
            // 
            this.lbl_XMLRhythm.AutoSize = true;
            this.lbl_XMLRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLRhythm.Location = new System.Drawing.Point(252, 109);
            this.lbl_XMLRhythm.Name = "lbl_XMLRhythm";
            this.lbl_XMLRhythm.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLRhythm.TabIndex = 300;
            this.lbl_XMLRhythm.Text = "Vs.";
            // 
            // lbl_JSONLead
            // 
            this.lbl_JSONLead.AutoSize = true;
            this.lbl_JSONLead.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONLead.Location = new System.Drawing.Point(252, 130);
            this.lbl_JSONLead.Name = "lbl_JSONLead";
            this.lbl_JSONLead.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONLead.TabIndex = 302;
            this.lbl_JSONLead.Text = "Vs.";
            // 
            // lbl_JSONBass
            // 
            this.lbl_JSONBass.AutoSize = true;
            this.lbl_JSONBass.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONBass.Location = new System.Drawing.Point(252, 156);
            this.lbl_JSONBass.Name = "lbl_JSONBass";
            this.lbl_JSONBass.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONBass.TabIndex = 303;
            this.lbl_JSONBass.Text = "Vs.";
            // 
            // lbl_JSONCombo
            // 
            this.lbl_JSONCombo.AutoSize = true;
            this.lbl_JSONCombo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONCombo.Location = new System.Drawing.Point(252, 182);
            this.lbl_JSONCombo.Name = "lbl_JSONCombo";
            this.lbl_JSONCombo.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONCombo.TabIndex = 304;
            this.lbl_JSONCombo.Text = "Vs.";
            // 
            // lbl_JSONRhythm
            // 
            this.lbl_JSONRhythm.AutoSize = true;
            this.lbl_JSONRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONRhythm.Location = new System.Drawing.Point(252, 208);
            this.lbl_JSONRhythm.Name = "lbl_JSONRhythm";
            this.lbl_JSONRhythm.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONRhythm.TabIndex = 305;
            this.lbl_JSONRhythm.Text = "Vs.";
            // 
            // lbl_Reference
            // 
            this.lbl_Reference.AutoSize = true;
            this.lbl_Reference.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbl_Reference.Location = new System.Drawing.Point(262, 39);
            this.lbl_Reference.Name = "lbl_Reference";
            this.lbl_Reference.Size = new System.Drawing.Size(22, 13);
            this.lbl_Reference.TabIndex = 307;
            this.lbl_Reference.Text = "Vs.";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(413, 663);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 26);
            this.button1.TabIndex = 308;
            this.button1.Text = "Compare in winMerge";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(601, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 310;
            this.label1.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(601, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 311;
            this.label2.Text = "Comment";
            // 
            // chbx_IsOriginal
            // 
            this.chbx_IsOriginal.AutoSize = true;
            this.chbx_IsOriginal.Checked = true;
            this.chbx_IsOriginal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_IsOriginal.Enabled = false;
            this.chbx_IsOriginal.Location = new System.Drawing.Point(498, 254);
            this.chbx_IsOriginal.Name = "chbx_IsOriginal";
            this.chbx_IsOriginal.Size = new System.Drawing.Size(61, 17);
            this.chbx_IsOriginal.TabIndex = 312;
            this.chbx_IsOriginal.Text = "Original";
            this.chbx_IsOriginal.UseVisualStyleBackColor = true;
            this.chbx_IsOriginal.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(228, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 13);
            this.label17.TabIndex = 313;
            this.label17.Text = "MM-DD-YYYY";
            // 
            // lbl_Is_Original
            // 
            this.lbl_Is_Original.AutoSize = true;
            this.lbl_Is_Original.Location = new System.Drawing.Point(158, 208);
            this.lbl_Is_Original.Name = "lbl_Is_Original";
            this.lbl_Is_Original.Size = new System.Drawing.Size(53, 13);
            this.lbl_Is_Original.TabIndex = 314;
            this.lbl_Is_Original.Text = "Is Original";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(134, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 315;
            this.label19.Text = "DD Available";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(169, 280);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 13);
            this.label20.TabIndex = 316;
            this.label20.Text = "Version";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(101, 256);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 13);
            this.label21.TabIndex = 317;
            this.label21.Text = "Author";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(100, 231);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(39, 13);
            this.label22.TabIndex = 318;
            this.label22.Text = "Toolkit";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(99, 334);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 13);
            this.label23.TabIndex = 320;
            this.label23.Text = "Tuning";
            // 
            // btn_UpdateExisting
            // 
            this.btn_UpdateExisting.Location = new System.Drawing.Point(413, 32);
            this.btn_UpdateExisting.Name = "btn_UpdateExisting";
            this.btn_UpdateExisting.Size = new System.Drawing.Size(93, 20);
            this.btn_UpdateExisting.TabIndex = 321;
            this.btn_UpdateExisting.Text = "Update Existing";
            this.btn_UpdateExisting.UseVisualStyleBackColor = true;
            this.btn_UpdateExisting.Click += new System.EventHandler(this.button2_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label24.Location = new System.Drawing.Point(164, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(96, 15);
            this.label24.TabIndex = 322;
            this.label24.Text = "Currently Importing";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Location = new System.Drawing.Point(284, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 15);
            this.label25.TabIndex = 323;
            this.label25.Text = "Already Imported";
            // 
            // lbl_IDExisting
            // 
            this.lbl_IDExisting.AutoSize = true;
            this.lbl_IDExisting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_IDExisting.Location = new System.Drawing.Point(373, 35);
            this.lbl_IDExisting.Name = "lbl_IDExisting";
            this.lbl_IDExisting.Size = new System.Drawing.Size(59, 15);
            this.lbl_IDExisting.TabIndex = 324;
            this.lbl_IDExisting.Text = "ID Existing";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 357);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(135, 13);
            this.label26.TabIndex = 326;
            this.label26.Text = "DLC Name (autom. unique)";
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(688, 729);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(73, 35);
            this.btn_DecompressAll.TabIndex = 327;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(230, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(89, 18);
            this.label27.TabIndex = 328;
            this.label27.Text = "Differences";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 107);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(27, 13);
            this.label28.TabIndex = 329;
            this.label28.Text = "Title";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(-1, 131);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(33, 13);
            this.label29.TabIndex = 330;
            this.label29.Text = "TSort";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(-1, 157);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(33, 13);
            this.label30.TabIndex = 331;
            this.label30.Text = "ASort";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(11, 182);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(21, 13);
            this.label31.TabIndex = 332;
            this.label31.Text = "FN";
            // 
            // lbl_diffCount
            // 
            this.lbl_diffCount.AutoSize = true;
            this.lbl_diffCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_diffCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_diffCount.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_diffCount.Location = new System.Drawing.Point(321, 6);
            this.lbl_diffCount.Name = "lbl_diffCount";
            this.lbl_diffCount.Size = new System.Drawing.Size(39, 26);
            this.lbl_diffCount.TabIndex = 335;
            this.lbl_diffCount.Text = "x/y";
            // 
            // chbx_IgnoreDupli
            // 
            this.chbx_IgnoreDupli.AutoSize = true;
            this.chbx_IgnoreDupli.Enabled = false;
            this.chbx_IgnoreDupli.Location = new System.Drawing.Point(603, 12);
            this.chbx_IgnoreDupli.Name = "chbx_IgnoreDupli";
            this.chbx_IgnoreDupli.Size = new System.Drawing.Size(157, 17);
            this.chbx_IgnoreDupli.TabIndex = 336;
            this.chbx_IgnoreDupli.Text = "Ignore remaining Duplicates";
            this.chbx_IgnoreDupli.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveOldNew
            // 
            this.btn_RemoveOldNew.Location = new System.Drawing.Point(550, 104);
            this.btn_RemoveOldNew.Name = "btn_RemoveOldNew";
            this.btn_RemoveOldNew.Size = new System.Drawing.Size(84, 19);
            this.btn_RemoveOldNew.TabIndex = 338;
            this.btn_RemoveOldNew.Text = "Clear extra txt";
            this.btn_RemoveOldNew.UseVisualStyleBackColor = true;
            this.btn_RemoveOldNew.Click += new System.EventHandler(this.btn_RemoveOldNew_Click);
            // 
            // lbl_Vocals
            // 
            this.lbl_Vocals.AutoSize = true;
            this.lbl_Vocals.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Vocals.Location = new System.Drawing.Point(254, 120);
            this.lbl_Vocals.Name = "lbl_Vocals";
            this.lbl_Vocals.Size = new System.Drawing.Size(22, 13);
            this.lbl_Vocals.TabIndex = 342;
            this.lbl_Vocals.Text = "Vs.";
            // 
            // lbl_txt_Vocals
            // 
            this.lbl_txt_Vocals.AutoSize = true;
            this.lbl_txt_Vocals.Location = new System.Drawing.Point(158, 120);
            this.lbl_txt_Vocals.Name = "lbl_txt_Vocals";
            this.lbl_txt_Vocals.Size = new System.Drawing.Size(39, 13);
            this.lbl_txt_Vocals.TabIndex = 341;
            this.lbl_txt_Vocals.Text = "Vocals";
            // 
            // chbx_Autosave
            // 
            this.chbx_Autosave.AutoSize = true;
            this.chbx_Autosave.Checked = true;
            this.chbx_Autosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Autosave.Location = new System.Drawing.Point(687, 35);
            this.chbx_Autosave.Name = "chbx_Autosave";
            this.chbx_Autosave.Size = new System.Drawing.Size(73, 17);
            this.chbx_Autosave.TabIndex = 346;
            this.chbx_Autosave.Text = "AutoSave";
            this.chbx_Autosave.UseVisualStyleBackColor = true;
            // 
            // btn_TitleNew
            // 
            this.btn_TitleNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TitleNew.Location = new System.Drawing.Point(506, 104);
            this.btn_TitleNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TitleNew.Name = "btn_TitleNew";
            this.btn_TitleNew.Size = new System.Drawing.Size(18, 17);
            this.btn_TitleNew.TabIndex = 349;
            this.btn_TitleNew.Text = "<";
            this.btn_TitleNew.UseVisualStyleBackColor = true;
            this.btn_TitleNew.Click += new System.EventHandler(this.btn_TitleNew_Click);
            // 
            // btn_TitleExisting
            // 
            this.btn_TitleExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TitleExisting.Location = new System.Drawing.Point(526, 104);
            this.btn_TitleExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TitleExisting.Name = "btn_TitleExisting";
            this.btn_TitleExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_TitleExisting.TabIndex = 348;
            this.btn_TitleExisting.Text = ">";
            this.btn_TitleExisting.UseVisualStyleBackColor = true;
            this.btn_TitleExisting.Click += new System.EventHandler(this.btn_TitleExisting_Click);
            // 
            // btn_TitleSortNew
            // 
            this.btn_TitleSortNew.Enabled = false;
            this.btn_TitleSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TitleSortNew.Location = new System.Drawing.Point(506, 130);
            this.btn_TitleSortNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TitleSortNew.Name = "btn_TitleSortNew";
            this.btn_TitleSortNew.Size = new System.Drawing.Size(18, 17);
            this.btn_TitleSortNew.TabIndex = 351;
            this.btn_TitleSortNew.Text = "<";
            this.btn_TitleSortNew.UseVisualStyleBackColor = true;
            this.btn_TitleSortNew.Click += new System.EventHandler(this.btn_TitleSortNew_Click);
            // 
            // btn_TitleSortExisting
            // 
            this.btn_TitleSortExisting.Enabled = false;
            this.btn_TitleSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TitleSortExisting.Location = new System.Drawing.Point(526, 130);
            this.btn_TitleSortExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TitleSortExisting.Name = "btn_TitleSortExisting";
            this.btn_TitleSortExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_TitleSortExisting.TabIndex = 350;
            this.btn_TitleSortExisting.Text = ">";
            this.btn_TitleSortExisting.UseVisualStyleBackColor = true;
            this.btn_TitleSortExisting.Click += new System.EventHandler(this.btnTitleSortExisting_Click);
            // 
            // btn_AuthorNew
            // 
            this.btn_AuthorNew.Enabled = false;
            this.btn_AuthorNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AuthorNew.Location = new System.Drawing.Point(403, 254);
            this.btn_AuthorNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AuthorNew.Name = "btn_AuthorNew";
            this.btn_AuthorNew.Size = new System.Drawing.Size(18, 17);
            this.btn_AuthorNew.TabIndex = 353;
            this.btn_AuthorNew.Text = "<";
            this.btn_AuthorNew.UseVisualStyleBackColor = true;
            this.btn_AuthorNew.Click += new System.EventHandler(this.btn_AuthorNew_Click);
            // 
            // btn_AuthorExisting
            // 
            this.btn_AuthorExisting.Enabled = false;
            this.btn_AuthorExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AuthorExisting.Location = new System.Drawing.Point(423, 254);
            this.btn_AuthorExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AuthorExisting.Name = "btn_AuthorExisting";
            this.btn_AuthorExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_AuthorExisting.TabIndex = 352;
            this.btn_AuthorExisting.Text = ">";
            this.btn_AuthorExisting.UseVisualStyleBackColor = true;
            this.btn_AuthorExisting.Click += new System.EventHandler(this.btn_AuthorExisting_Click);
            // 
            // lbl_Album
            // 
            this.lbl_Album.AutoSize = true;
            this.lbl_Album.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Album.Location = new System.Drawing.Point(262, 81);
            this.lbl_Album.Name = "lbl_Album";
            this.lbl_Album.Size = new System.Drawing.Size(22, 13);
            this.lbl_Album.TabIndex = 356;
            this.lbl_Album.Text = "Vs.";
            // 
            // lbl_Artist
            // 
            this.lbl_Artist.AutoSize = true;
            this.lbl_Artist.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Artist.Location = new System.Drawing.Point(262, 58);
            this.lbl_Artist.Name = "lbl_Artist";
            this.lbl_Artist.Size = new System.Drawing.Size(22, 13);
            this.lbl_Artist.TabIndex = 357;
            this.lbl_Artist.Text = "Vs.";
            // 
            // btn_AlbumNew
            // 
            this.btn_AlbumNew.Enabled = false;
            this.btn_AlbumNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AlbumNew.Location = new System.Drawing.Point(507, 80);
            this.btn_AlbumNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AlbumNew.Name = "btn_AlbumNew";
            this.btn_AlbumNew.Size = new System.Drawing.Size(18, 17);
            this.btn_AlbumNew.TabIndex = 359;
            this.btn_AlbumNew.Text = "<";
            this.btn_AlbumNew.UseVisualStyleBackColor = true;
            this.btn_AlbumNew.Click += new System.EventHandler(this.btn_AlbumNew_Click);
            // 
            // btn_AlbumExisting
            // 
            this.btn_AlbumExisting.Enabled = false;
            this.btn_AlbumExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AlbumExisting.Location = new System.Drawing.Point(527, 80);
            this.btn_AlbumExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AlbumExisting.Name = "btn_AlbumExisting";
            this.btn_AlbumExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_AlbumExisting.TabIndex = 358;
            this.btn_AlbumExisting.Text = ">";
            this.btn_AlbumExisting.UseVisualStyleBackColor = true;
            this.btn_AlbumExisting.Click += new System.EventHandler(this.btn_AlbumExisting_Click);
            // 
            // btn_ArtistNew
            // 
            this.btn_ArtistNew.Enabled = false;
            this.btn_ArtistNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ArtistNew.Location = new System.Drawing.Point(507, 55);
            this.btn_ArtistNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ArtistNew.Name = "btn_ArtistNew";
            this.btn_ArtistNew.Size = new System.Drawing.Size(18, 17);
            this.btn_ArtistNew.TabIndex = 361;
            this.btn_ArtistNew.Text = "<";
            this.btn_ArtistNew.UseVisualStyleBackColor = true;
            this.btn_ArtistNew.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btn_ArtistExisting
            // 
            this.btn_ArtistExisting.Enabled = false;
            this.btn_ArtistExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ArtistExisting.Location = new System.Drawing.Point(527, 55);
            this.btn_ArtistExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ArtistExisting.Name = "btn_ArtistExisting";
            this.btn_ArtistExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_ArtistExisting.TabIndex = 360;
            this.btn_ArtistExisting.Text = ">";
            this.btn_ArtistExisting.UseVisualStyleBackColor = true;
            this.btn_ArtistExisting.Click += new System.EventHandler(this.btn_ArtistExisting_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 362;
            this.label4.Text = "Artist";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 363;
            this.label18.Text = "Abum";
            // 
            // btn_ArtistSortNew
            // 
            this.btn_ArtistSortNew.Enabled = false;
            this.btn_ArtistSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ArtistSortNew.Location = new System.Drawing.Point(507, 156);
            this.btn_ArtistSortNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ArtistSortNew.Name = "btn_ArtistSortNew";
            this.btn_ArtistSortNew.Size = new System.Drawing.Size(18, 17);
            this.btn_ArtistSortNew.TabIndex = 366;
            this.btn_ArtistSortNew.Text = "<";
            this.btn_ArtistSortNew.UseVisualStyleBackColor = true;
            this.btn_ArtistSortNew.Click += new System.EventHandler(this.btn_ArtistSortNew_Click);
            // 
            // btn_ArtistSortExisting
            // 
            this.btn_ArtistSortExisting.Enabled = false;
            this.btn_ArtistSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ArtistSortExisting.Location = new System.Drawing.Point(527, 156);
            this.btn_ArtistSortExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ArtistSortExisting.Name = "btn_ArtistSortExisting";
            this.btn_ArtistSortExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_ArtistSortExisting.TabIndex = 365;
            this.btn_ArtistSortExisting.Text = ">";
            this.btn_ArtistSortExisting.UseVisualStyleBackColor = true;
            this.btn_ArtistSortExisting.Click += new System.EventHandler(this.btn_ArtistSortExisting_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_GoToNew);
            this.groupBox2.Controls.Add(this.btn_GoToExisting);
            this.groupBox2.Controls.Add(this.btn_AddAge);
            this.groupBox2.Controls.Add(this.lbl_Existing);
            this.groupBox2.Controls.Add(this.txt_JSONLeadExisting);
            this.groupBox2.Controls.Add(this.lbl_New);
            this.groupBox2.Controls.Add(this.txt_XMLLeadNew);
            this.groupBox2.Controls.Add(this.txt_XMLLeadExisting);
            this.groupBox2.Controls.Add(this.txt_XMLBassNew);
            this.groupBox2.Controls.Add(this.txt_XMLBassExisting);
            this.groupBox2.Controls.Add(this.txt_XMLComboNew);
            this.groupBox2.Controls.Add(this.txt_XMLComboExisting);
            this.groupBox2.Controls.Add(this.txt_XMLRhythmNew);
            this.groupBox2.Controls.Add(this.txt_XMLRhythmExisting);
            this.groupBox2.Controls.Add(this.txt_JSONLeadNew);
            this.groupBox2.Controls.Add(this.txt_JSONBassNew);
            this.groupBox2.Controls.Add(this.txt_JSONBassExisting);
            this.groupBox2.Controls.Add(this.txt_JSONComboNew);
            this.groupBox2.Controls.Add(this.txt_JSONComboExisting);
            this.groupBox2.Controls.Add(this.txt_JSONRhythmNew);
            this.groupBox2.Controls.Add(this.txt_JSONRhythmExisting);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lbl_XMLLead);
            this.groupBox2.Controls.Add(this.lbl_XMLBass);
            this.groupBox2.Controls.Add(this.lbl_XMLCombo);
            this.groupBox2.Controls.Add(this.lbl_XMLRhythm);
            this.groupBox2.Controls.Add(this.lbl_JSONLead);
            this.groupBox2.Controls.Add(this.lbl_JSONBass);
            this.groupBox2.Controls.Add(this.lbl_JSONCombo);
            this.groupBox2.Controls.Add(this.lbl_JSONRhythm);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(8, 550);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 233);
            this.groupBox2.TabIndex = 367;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "XML/JSON comparison based on Last Conversion Date or Hash";
            // 
            // btn_GoToNew
            // 
            this.btn_GoToNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GoToNew.Location = new System.Drawing.Point(171, 15);
            this.btn_GoToNew.Name = "btn_GoToNew";
            this.btn_GoToNew.Size = new System.Drawing.Size(21, 15);
            this.btn_GoToNew.TabIndex = 396;
            this.btn_GoToNew.Text = "->";
            this.btn_GoToNew.UseVisualStyleBackColor = true;
            this.btn_GoToNew.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // btn_GoToExisting
            // 
            this.btn_GoToExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GoToExisting.Location = new System.Drawing.Point(348, 14);
            this.btn_GoToExisting.Name = "btn_GoToExisting";
            this.btn_GoToExisting.Size = new System.Drawing.Size(21, 15);
            this.btn_GoToExisting.TabIndex = 395;
            this.btn_GoToExisting.Text = "->";
            this.btn_GoToExisting.UseVisualStyleBackColor = true;
            this.btn_GoToExisting.Click += new System.EventHandler(this.btn_GoToExisting_Click);
            // 
            // btn_AddAge
            // 
            this.btn_AddAge.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddAge.Location = new System.Drawing.Point(374, 14);
            this.btn_AddAge.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddAge.Name = "btn_AddAge";
            this.btn_AddAge.Size = new System.Drawing.Size(14, 15);
            this.btn_AddAge.TabIndex = 390;
            this.btn_AddAge.Text = "+";
            this.btn_AddAge.UseVisualStyleBackColor = false;
            this.btn_AddAge.TextChanged += new System.EventHandler(this.ExistingChanged);
            this.btn_AddAge.Click += new System.EventHandler(this.button6_Click);
            // 
            // lbl_Existing
            // 
            this.lbl_Existing.AutoSize = true;
            this.lbl_Existing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Existing.Location = new System.Drawing.Point(309, 16);
            this.lbl_Existing.Name = "lbl_Existing";
            this.lbl_Existing.Size = new System.Drawing.Size(38, 15);
            this.lbl_Existing.TabIndex = 391;
            this.lbl_Existing.Text = "newer";
            // 
            // txt_JSONLeadExisting
            // 
            this.txt_JSONLeadExisting.Cue = "JSON Lead Existing";
            this.txt_JSONLeadExisting.Enabled = false;
            this.txt_JSONLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONLeadExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONLeadExisting.Location = new System.Drawing.Point(274, 127);
            this.txt_JSONLeadExisting.Name = "txt_JSONLeadExisting";
            this.txt_JSONLeadExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONLeadExisting.TabIndex = 253;
            // 
            // lbl_New
            // 
            this.lbl_New.AutoSize = true;
            this.lbl_New.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_New.Location = new System.Drawing.Point(191, 14);
            this.lbl_New.Name = "lbl_New";
            this.lbl_New.Size = new System.Drawing.Size(32, 15);
            this.lbl_New.TabIndex = 390;
            this.lbl_New.Text = "older";
            // 
            // txt_XMLLeadNew
            // 
            this.txt_XMLLeadNew.Cue = "XML Lead New";
            this.txt_XMLLeadNew.Enabled = false;
            this.txt_XMLLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLLeadNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLLeadNew.Location = new System.Drawing.Point(134, 31);
            this.txt_XMLLeadNew.Name = "txt_XMLLeadNew";
            this.txt_XMLLeadNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLLeadNew.TabIndex = 240;
            this.txt_XMLLeadNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLLeadExisting
            // 
            this.txt_XMLLeadExisting.Cue = "XML Lead Existing";
            this.txt_XMLLeadExisting.Enabled = false;
            this.txt_XMLLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLLeadExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLLeadExisting.Location = new System.Drawing.Point(274, 31);
            this.txt_XMLLeadExisting.Name = "txt_XMLLeadExisting";
            this.txt_XMLLeadExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLLeadExisting.TabIndex = 241;
            // 
            // txt_XMLBassNew
            // 
            this.txt_XMLBassNew.Cue = "XML Bass New";
            this.txt_XMLBassNew.Enabled = false;
            this.txt_XMLBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLBassNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLBassNew.Location = new System.Drawing.Point(134, 56);
            this.txt_XMLBassNew.Name = "txt_XMLBassNew";
            this.txt_XMLBassNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLBassNew.TabIndex = 244;
            this.txt_XMLBassNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLBassExisting
            // 
            this.txt_XMLBassExisting.Cue = "XML Bass Existing";
            this.txt_XMLBassExisting.Enabled = false;
            this.txt_XMLBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLBassExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLBassExisting.Location = new System.Drawing.Point(274, 56);
            this.txt_XMLBassExisting.Name = "txt_XMLBassExisting";
            this.txt_XMLBassExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLBassExisting.TabIndex = 245;
            // 
            // txt_XMLComboNew
            // 
            this.txt_XMLComboNew.Cue = "XML Combo New";
            this.txt_XMLComboNew.Enabled = false;
            this.txt_XMLComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLComboNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLComboNew.Location = new System.Drawing.Point(134, 80);
            this.txt_XMLComboNew.Name = "txt_XMLComboNew";
            this.txt_XMLComboNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLComboNew.TabIndex = 246;
            this.txt_XMLComboNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLComboExisting
            // 
            this.txt_XMLComboExisting.Cue = "XML Combo Existing";
            this.txt_XMLComboExisting.Enabled = false;
            this.txt_XMLComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLComboExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLComboExisting.Location = new System.Drawing.Point(274, 80);
            this.txt_XMLComboExisting.Name = "txt_XMLComboExisting";
            this.txt_XMLComboExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLComboExisting.TabIndex = 247;
            // 
            // txt_XMLRhythmNew
            // 
            this.txt_XMLRhythmNew.Cue = "XML Rhythm New";
            this.txt_XMLRhythmNew.Enabled = false;
            this.txt_XMLRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLRhythmNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLRhythmNew.Location = new System.Drawing.Point(134, 104);
            this.txt_XMLRhythmNew.Name = "txt_XMLRhythmNew";
            this.txt_XMLRhythmNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLRhythmNew.TabIndex = 248;
            this.txt_XMLRhythmNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLRhythmExisting
            // 
            this.txt_XMLRhythmExisting.Cue = "XML Rhythm Existing";
            this.txt_XMLRhythmExisting.Enabled = false;
            this.txt_XMLRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLRhythmExisting.Location = new System.Drawing.Point(274, 104);
            this.txt_XMLRhythmExisting.Name = "txt_XMLRhythmExisting";
            this.txt_XMLRhythmExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLRhythmExisting.TabIndex = 249;
            // 
            // txt_JSONLeadNew
            // 
            this.txt_JSONLeadNew.Cue = "JSON Lead New";
            this.txt_JSONLeadNew.Enabled = false;
            this.txt_JSONLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONLeadNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONLeadNew.Location = new System.Drawing.Point(134, 127);
            this.txt_JSONLeadNew.Name = "txt_JSONLeadNew";
            this.txt_JSONLeadNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONLeadNew.TabIndex = 252;
            this.txt_JSONLeadNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONBassNew
            // 
            this.txt_JSONBassNew.Cue = "JSON Bass New";
            this.txt_JSONBassNew.Enabled = false;
            this.txt_JSONBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONBassNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONBassNew.Location = new System.Drawing.Point(134, 153);
            this.txt_JSONBassNew.Name = "txt_JSONBassNew";
            this.txt_JSONBassNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONBassNew.TabIndex = 254;
            this.txt_JSONBassNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONBassExisting
            // 
            this.txt_JSONBassExisting.Cue = "Artist Sort Existing";
            this.txt_JSONBassExisting.Enabled = false;
            this.txt_JSONBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONBassExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONBassExisting.Location = new System.Drawing.Point(274, 153);
            this.txt_JSONBassExisting.Name = "txt_JSONBassExisting";
            this.txt_JSONBassExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONBassExisting.TabIndex = 255;
            // 
            // txt_JSONComboNew
            // 
            this.txt_JSONComboNew.Cue = "JSON Combo New";
            this.txt_JSONComboNew.Enabled = false;
            this.txt_JSONComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONComboNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONComboNew.Location = new System.Drawing.Point(134, 179);
            this.txt_JSONComboNew.Name = "txt_JSONComboNew";
            this.txt_JSONComboNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONComboNew.TabIndex = 256;
            this.txt_JSONComboNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONComboExisting
            // 
            this.txt_JSONComboExisting.Cue = "JSON Combo Existing";
            this.txt_JSONComboExisting.Enabled = false;
            this.txt_JSONComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONComboExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONComboExisting.Location = new System.Drawing.Point(274, 179);
            this.txt_JSONComboExisting.Name = "txt_JSONComboExisting";
            this.txt_JSONComboExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONComboExisting.TabIndex = 257;
            // 
            // txt_JSONRhythmNew
            // 
            this.txt_JSONRhythmNew.Cue = "JSON Rhythm New";
            this.txt_JSONRhythmNew.Enabled = false;
            this.txt_JSONRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONRhythmNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONRhythmNew.Location = new System.Drawing.Point(134, 205);
            this.txt_JSONRhythmNew.Name = "txt_JSONRhythmNew";
            this.txt_JSONRhythmNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONRhythmNew.TabIndex = 259;
            this.txt_JSONRhythmNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONRhythmExisting
            // 
            this.txt_JSONRhythmExisting.Cue = "JSON Rhythm Existing";
            this.txt_JSONRhythmExisting.Enabled = false;
            this.txt_JSONRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONRhythmExisting.Location = new System.Drawing.Point(274, 205);
            this.txt_JSONRhythmExisting.Name = "txt_JSONRhythmExisting";
            this.txt_JSONRhythmExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONRhythmExisting.TabIndex = 260;
            // 
            // chbx_IsAlternateExisting
            // 
            this.chbx_IsAlternateExisting.AutoSize = true;
            this.chbx_IsAlternateExisting.Checked = true;
            this.chbx_IsAlternateExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_IsAlternateExisting.Location = new System.Drawing.Point(371, 204);
            this.chbx_IsAlternateExisting.Name = "chbx_IsAlternateExisting";
            this.chbx_IsAlternateExisting.Size = new System.Drawing.Size(68, 17);
            this.chbx_IsAlternateExisting.TabIndex = 368;
            this.chbx_IsAlternateExisting.Text = "Alternate";
            this.chbx_IsAlternateExisting.UseVisualStyleBackColor = true;
            this.chbx_IsAlternateExisting.CheckedChanged += new System.EventHandler(this.chbx_IsAlternateExisting_CheckedChanged);
            // 
            // txt_AlternateNoExisting
            // 
            this.txt_AlternateNoExisting.Location = new System.Drawing.Point(334, 202);
            this.txt_AlternateNoExisting.Margin = new System.Windows.Forms.Padding(2);
            this.txt_AlternateNoExisting.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txt_AlternateNoExisting.Name = "txt_AlternateNoExisting";
            this.txt_AlternateNoExisting.Size = new System.Drawing.Size(32, 20);
            this.txt_AlternateNoExisting.TabIndex = 370;
            // 
            // txt_AlternateNoNew
            // 
            this.txt_AlternateNoNew.Location = new System.Drawing.Point(121, 204);
            this.txt_AlternateNoNew.Margin = new System.Windows.Forms.Padding(2);
            this.txt_AlternateNoNew.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txt_AlternateNoNew.Name = "txt_AlternateNoNew";
            this.txt_AlternateNoNew.Size = new System.Drawing.Size(32, 20);
            this.txt_AlternateNoNew.TabIndex = 371;
            // 
            // txt_AlbumExisting
            // 
            this.txt_AlbumExisting.Cue = "Album";
            this.txt_AlbumExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumExisting.Location = new System.Drawing.Point(284, 78);
            this.txt_AlbumExisting.Name = "txt_AlbumExisting";
            this.txt_AlbumExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_AlbumExisting.TabIndex = 344;
            this.txt_AlbumExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_AlbumExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_ArtistExisting
            // 
            this.txt_ArtistExisting.Cue = "Artist";
            this.txt_ArtistExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistExisting.Location = new System.Drawing.Point(284, 55);
            this.txt_ArtistExisting.Name = "txt_ArtistExisting";
            this.txt_ArtistExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistExisting.TabIndex = 343;
            this.txt_ArtistExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ArtistExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_VocalsExisting
            // 
            this.txt_VocalsExisting.Cue = "Vocals Existing";
            this.txt_VocalsExisting.Enabled = false;
            this.txt_VocalsExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VocalsExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_VocalsExisting.Location = new System.Drawing.Point(276, 117);
            this.txt_VocalsExisting.Name = "txt_VocalsExisting";
            this.txt_VocalsExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_VocalsExisting.TabIndex = 340;
            // 
            // txt_VocalsNew
            // 
            this.txt_VocalsNew.Cue = "Vocals New";
            this.txt_VocalsNew.Enabled = false;
            this.txt_VocalsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VocalsNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_VocalsNew.Location = new System.Drawing.Point(209, 117);
            this.txt_VocalsNew.Name = "txt_VocalsNew";
            this.txt_VocalsNew.Size = new System.Drawing.Size(43, 20);
            this.txt_VocalsNew.TabIndex = 339;
            this.txt_VocalsNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlbumNew
            // 
            this.txt_AlbumNew.Cue = "Album";
            this.txt_AlbumNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumNew.Location = new System.Drawing.Point(38, 77);
            this.txt_AlbumNew.Name = "txt_AlbumNew";
            this.txt_AlbumNew.Size = new System.Drawing.Size(222, 20);
            this.txt_AlbumNew.TabIndex = 319;
            this.txt_AlbumNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_VersionExisting
            // 
            this.txt_VersionExisting.Cue = "Version  Existing";
            this.txt_VersionExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VersionExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_VersionExisting.Location = new System.Drawing.Point(284, 277);
            this.txt_VersionExisting.Name = "txt_VersionExisting";
            this.txt_VersionExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_VersionExisting.TabIndex = 282;
            this.txt_VersionExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_VersionNew
            // 
            this.txt_VersionNew.Cue = "Version New";
            this.txt_VersionNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VersionNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_VersionNew.Location = new System.Drawing.Point(217, 277);
            this.txt_VersionNew.Name = "txt_VersionNew";
            this.txt_VersionNew.Size = new System.Drawing.Size(43, 20);
            this.txt_VersionNew.TabIndex = 281;
            this.txt_VersionNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_FileNameExisting
            // 
            this.txt_FileNameExisting.Cue = "File Name Existing";
            this.txt_FileNameExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FileNameExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_FileNameExisting.Location = new System.Drawing.Point(284, 179);
            this.txt_FileNameExisting.Name = "txt_FileNameExisting";
            this.txt_FileNameExisting.ReadOnly = true;
            this.txt_FileNameExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_FileNameExisting.TabIndex = 280;
            this.txt_FileNameExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_FileNameNew
            // 
            this.txt_FileNameNew.Cue = "File Name New";
            this.txt_FileNameNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FileNameNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_FileNameNew.Location = new System.Drawing.Point(38, 179);
            this.txt_FileNameNew.Name = "txt_FileNameNew";
            this.txt_FileNameNew.ReadOnly = true;
            this.txt_FileNameNew.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_FileNameNew.Size = new System.Drawing.Size(222, 20);
            this.txt_FileNameNew.TabIndex = 279;
            this.txt_FileNameNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ArtistNew
            // 
            this.txt_ArtistNew.Cue = "Artist";
            this.txt_ArtistNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistNew.Location = new System.Drawing.Point(38, 54);
            this.txt_ArtistNew.Name = "txt_ArtistNew";
            this.txt_ArtistNew.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistNew.TabIndex = 278;
            this.txt_ArtistNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_PreviewExisting
            // 
            this.txt_PreviewExisting.Cue = "Preview Existing";
            this.txt_PreviewExisting.Enabled = false;
            this.txt_PreviewExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PreviewExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_PreviewExisting.Location = new System.Drawing.Point(276, 91);
            this.txt_PreviewExisting.Name = "txt_PreviewExisting";
            this.txt_PreviewExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_PreviewExisting.TabIndex = 243;
            // 
            // txt_PreviewNew
            // 
            this.txt_PreviewNew.Cue = "Preview New";
            this.txt_PreviewNew.Enabled = false;
            this.txt_PreviewNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PreviewNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_PreviewNew.Location = new System.Drawing.Point(209, 91);
            this.txt_PreviewNew.Name = "txt_PreviewNew";
            this.txt_PreviewNew.Size = new System.Drawing.Size(43, 20);
            this.txt_PreviewNew.TabIndex = 242;
            this.txt_PreviewNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AudioExisting
            // 
            this.txt_AudioExisting.Cue = "Audio Existing";
            this.txt_AudioExisting.Enabled = false;
            this.txt_AudioExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioExisting.Location = new System.Drawing.Point(276, 65);
            this.txt_AudioExisting.Name = "txt_AudioExisting";
            this.txt_AudioExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_AudioExisting.TabIndex = 239;
            // 
            // txt_AudioNew
            // 
            this.txt_AudioNew.Cue = "Audio New";
            this.txt_AudioNew.Enabled = false;
            this.txt_AudioNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioNew.Location = new System.Drawing.Point(209, 65);
            this.txt_AudioNew.Name = "txt_AudioNew";
            this.txt_AudioNew.Size = new System.Drawing.Size(43, 20);
            this.txt_AudioNew.TabIndex = 238;
            this.txt_AudioNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AvailTracksExisting
            // 
            this.txt_AvailTracksExisting.Cue = "Available Tracks Existing";
            this.txt_AvailTracksExisting.Enabled = false;
            this.txt_AvailTracksExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt_AvailTracksExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AvailTracksExisting.Location = new System.Drawing.Point(276, 39);
            this.txt_AvailTracksExisting.Name = "txt_AvailTracksExisting";
            this.txt_AvailTracksExisting.Size = new System.Drawing.Size(45, 21);
            this.txt_AvailTracksExisting.TabIndex = 237;
            // 
            // txt_AvailTracksNew
            // 
            this.txt_AvailTracksNew.Cue = "Available Tracks New";
            this.txt_AvailTracksNew.Enabled = false;
            this.txt_AvailTracksNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txt_AvailTracksNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AvailTracksNew.Location = new System.Drawing.Point(209, 39);
            this.txt_AvailTracksNew.Name = "txt_AvailTracksNew";
            this.txt_AvailTracksNew.Size = new System.Drawing.Size(43, 21);
            this.txt_AvailTracksNew.TabIndex = 236;
            this.txt_AvailTracksNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DDExisting
            // 
            this.txt_DDExisting.Cue = "DD Existing";
            this.txt_DDExisting.Enabled = false;
            this.txt_DDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DDExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_DDExisting.Location = new System.Drawing.Point(276, 13);
            this.txt_DDExisting.Name = "txt_DDExisting";
            this.txt_DDExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_DDExisting.TabIndex = 235;
            // 
            // txt_DDNew
            // 
            this.txt_DDNew.Cue = "DD New";
            this.txt_DDNew.Enabled = false;
            this.txt_DDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DDNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_DDNew.Location = new System.Drawing.Point(209, 13);
            this.txt_DDNew.Name = "txt_DDNew";
            this.txt_DDNew.Size = new System.Drawing.Size(43, 20);
            this.txt_DDNew.TabIndex = 234;
            this.txt_DDNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DLCIDExisting
            // 
            this.txt_DLCIDExisting.Cue = "DLC Name Existing";
            this.txt_DLCIDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLCIDExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLCIDExisting.Location = new System.Drawing.Point(284, 354);
            this.txt_DLCIDExisting.Name = "txt_DLCIDExisting";
            this.txt_DLCIDExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_DLCIDExisting.TabIndex = 233;
            this.txt_DLCIDExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_DLCIDNew
            // 
            this.txt_DLCIDNew.Cue = "DLC Name New";
            this.txt_DLCIDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLCIDNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLCIDNew.Location = new System.Drawing.Point(144, 354);
            this.txt_DLCIDNew.Name = "txt_DLCIDNew";
            this.txt_DLCIDNew.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_DLCIDNew.Size = new System.Drawing.Size(116, 20);
            this.txt_DLCIDNew.TabIndex = 232;
            this.txt_DLCIDNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_TuningExisting
            // 
            this.txt_TuningExisting.Cue = "Tunig Existing";
            this.txt_TuningExisting.Enabled = false;
            this.txt_TuningExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TuningExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_TuningExisting.Location = new System.Drawing.Point(284, 331);
            this.txt_TuningExisting.Name = "txt_TuningExisting";
            this.txt_TuningExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_TuningExisting.TabIndex = 231;
            // 
            // txt_TuningNew
            // 
            this.txt_TuningNew.Cue = "Tuning New";
            this.txt_TuningNew.Enabled = false;
            this.txt_TuningNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TuningNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_TuningNew.Location = new System.Drawing.Point(144, 331);
            this.txt_TuningNew.Name = "txt_TuningNew";
            this.txt_TuningNew.Size = new System.Drawing.Size(116, 20);
            this.txt_TuningNew.TabIndex = 230;
            this.txt_TuningNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AuthorExisting
            // 
            this.txt_AuthorExisting.Cue = "Author Existing";
            this.txt_AuthorExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AuthorExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AuthorExisting.Location = new System.Drawing.Point(284, 253);
            this.txt_AuthorExisting.Name = "txt_AuthorExisting";
            this.txt_AuthorExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_AuthorExisting.TabIndex = 229;
            this.txt_AuthorExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_AuthorNew
            // 
            this.txt_AuthorNew.Cue = "Author New";
            this.txt_AuthorNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AuthorNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AuthorNew.Location = new System.Drawing.Point(144, 253);
            this.txt_AuthorNew.Name = "txt_AuthorNew";
            this.txt_AuthorNew.Size = new System.Drawing.Size(116, 20);
            this.txt_AuthorNew.TabIndex = 228;
            this.txt_AuthorNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_IsOriginalExisting
            // 
            this.txt_IsOriginalExisting.Cue = "Is Original Existing";
            this.txt_IsOriginalExisting.Enabled = false;
            this.txt_IsOriginalExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_IsOriginalExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_IsOriginalExisting.Location = new System.Drawing.Point(284, 204);
            this.txt_IsOriginalExisting.Name = "txt_IsOriginalExisting";
            this.txt_IsOriginalExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_IsOriginalExisting.TabIndex = 227;
            // 
            // txt_IsOriginalNew
            // 
            this.txt_IsOriginalNew.Cue = "Is Original New";
            this.txt_IsOriginalNew.Enabled = false;
            this.txt_IsOriginalNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_IsOriginalNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_IsOriginalNew.Location = new System.Drawing.Point(217, 204);
            this.txt_IsOriginalNew.Name = "txt_IsOriginalNew";
            this.txt_IsOriginalNew.Size = new System.Drawing.Size(43, 20);
            this.txt_IsOriginalNew.TabIndex = 226;
            this.txt_IsOriginalNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ToolkitExisting
            // 
            this.txt_ToolkitExisting.Cue = "Toolkit Existing";
            this.txt_ToolkitExisting.Enabled = false;
            this.txt_ToolkitExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ToolkitExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_ToolkitExisting.Location = new System.Drawing.Point(284, 228);
            this.txt_ToolkitExisting.Name = "txt_ToolkitExisting";
            this.txt_ToolkitExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_ToolkitExisting.TabIndex = 225;
            // 
            // txt_ToolkitNew
            // 
            this.txt_ToolkitNew.Cue = "Toolkit New";
            this.txt_ToolkitNew.Enabled = false;
            this.txt_ToolkitNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ToolkitNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_ToolkitNew.Location = new System.Drawing.Point(144, 228);
            this.txt_ToolkitNew.Name = "txt_ToolkitNew";
            this.txt_ToolkitNew.Size = new System.Drawing.Size(116, 20);
            this.txt_ToolkitNew.TabIndex = 224;
            this.txt_ToolkitNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_TitleSortExisting
            // 
            this.txt_TitleSortExisting.Cue = "Title Sort Existing";
            this.txt_TitleSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleSortExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleSortExisting.Location = new System.Drawing.Point(284, 128);
            this.txt_TitleSortExisting.Name = "txt_TitleSortExisting";
            this.txt_TitleSortExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleSortExisting.TabIndex = 223;
            this.txt_TitleSortExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_TitleSortNew
            // 
            this.txt_TitleSortNew.Cue = "Title Sort New";
            this.txt_TitleSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleSortNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleSortNew.Location = new System.Drawing.Point(38, 128);
            this.txt_TitleSortNew.Name = "txt_TitleSortNew";
            this.txt_TitleSortNew.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleSortNew.TabIndex = 222;
            this.txt_TitleSortNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ArtistSortExisting
            // 
            this.txt_ArtistSortExisting.Cue = "Artist Sort Existing";
            this.txt_ArtistSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSortExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSortExisting.Location = new System.Drawing.Point(284, 154);
            this.txt_ArtistSortExisting.Name = "txt_ArtistSortExisting";
            this.txt_ArtistSortExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistSortExisting.TabIndex = 221;
            this.txt_ArtistSortExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_ArtistSortNew
            // 
            this.txt_ArtistSortNew.Cue = "Artist Sort New";
            this.txt_ArtistSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSortNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSortNew.Location = new System.Drawing.Point(38, 154);
            this.txt_ArtistSortNew.Name = "txt_ArtistSortNew";
            this.txt_ArtistSortNew.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistSortNew.TabIndex = 220;
            this.txt_ArtistSortNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_TitleExisting
            // 
            this.txt_TitleExisting.Cue = "Title Existing";
            this.txt_TitleExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleExisting.Location = new System.Drawing.Point(284, 102);
            this.txt_TitleExisting.Name = "txt_TitleExisting";
            this.txt_TitleExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleExisting.TabIndex = 219;
            this.txt_TitleExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_TitleNew
            // 
            this.txt_TitleNew.Cue = "Title New";
            this.txt_TitleNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleNew.Location = new System.Drawing.Point(38, 102);
            this.txt_TitleNew.Name = "txt_TitleNew";
            this.txt_TitleNew.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleNew.TabIndex = 217;
            this.txt_TitleNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chbx_MultiTrackExisting
            // 
            this.chbx_MultiTrackExisting.AutoSize = true;
            this.chbx_MultiTrackExisting.Location = new System.Drawing.Point(356, 305);
            this.chbx_MultiTrackExisting.Name = "chbx_MultiTrackExisting";
            this.chbx_MultiTrackExisting.Size = new System.Drawing.Size(76, 17);
            this.chbx_MultiTrackExisting.TabIndex = 372;
            this.chbx_MultiTrackExisting.Text = "MultiTrack";
            this.chbx_MultiTrackExisting.UseVisualStyleBackColor = true;
            this.chbx_MultiTrackExisting.CheckedChanged += new System.EventHandler(this.chbx_MultiTrackExisting_CheckedChanged);
            // 
            // chbx_MultiTrackNew
            // 
            this.chbx_MultiTrackNew.AutoSize = true;
            this.chbx_MultiTrackNew.Location = new System.Drawing.Point(124, 306);
            this.chbx_MultiTrackNew.Name = "chbx_MultiTrackNew";
            this.chbx_MultiTrackNew.Size = new System.Drawing.Size(76, 17);
            this.chbx_MultiTrackNew.TabIndex = 373;
            this.chbx_MultiTrackNew.Text = "MultiTrack";
            this.chbx_MultiTrackNew.UseVisualStyleBackColor = true;
            this.chbx_MultiTrackNew.CheckedChanged += new System.EventHandler(this.chbx_MultiTrackNew_CheckedChanged);
            // 
            // txt_MultiTrackNew
            // 
            this.txt_MultiTrackNew.Enabled = false;
            this.txt_MultiTrackNew.FormattingEnabled = true;
            this.txt_MultiTrackNew.Items.AddRange(new object[] {
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
            this.txt_MultiTrackNew.Location = new System.Drawing.Point(195, 303);
            this.txt_MultiTrackNew.Name = "txt_MultiTrackNew";
            this.txt_MultiTrackNew.Size = new System.Drawing.Size(65, 21);
            this.txt_MultiTrackNew.TabIndex = 374;
            this.txt_MultiTrackNew.SelectedIndexChanged += new System.EventHandler(this.txt_MultiTrackNew_SelectedIndexChanged);
            // 
            // txt_MultiTrackExisting
            // 
            this.txt_MultiTrackExisting.FormattingEnabled = true;
            this.txt_MultiTrackExisting.Items.AddRange(new object[] {
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
            this.txt_MultiTrackExisting.Location = new System.Drawing.Point(284, 303);
            this.txt_MultiTrackExisting.Name = "txt_MultiTrackExisting";
            this.txt_MultiTrackExisting.Size = new System.Drawing.Size(65, 21);
            this.txt_MultiTrackExisting.TabIndex = 375;
            // 
            // btn_CoverNew
            // 
            this.btn_CoverNew.Enabled = false;
            this.btn_CoverNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CoverNew.Location = new System.Drawing.Point(563, 503);
            this.btn_CoverNew.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CoverNew.Name = "btn_CoverNew";
            this.btn_CoverNew.Size = new System.Drawing.Size(18, 17);
            this.btn_CoverNew.TabIndex = 378;
            this.btn_CoverNew.Text = "<";
            this.btn_CoverNew.UseVisualStyleBackColor = true;
            this.btn_CoverNew.Click += new System.EventHandler(this.btn_CoverNew_Click);
            // 
            // btn_CoverExisting
            // 
            this.btn_CoverExisting.Enabled = false;
            this.btn_CoverExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CoverExisting.Location = new System.Drawing.Point(583, 503);
            this.btn_CoverExisting.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CoverExisting.Name = "btn_CoverExisting";
            this.btn_CoverExisting.Size = new System.Drawing.Size(18, 17);
            this.btn_CoverExisting.TabIndex = 377;
            this.btn_CoverExisting.Text = ">";
            this.btn_CoverExisting.UseVisualStyleBackColor = true;
            this.btn_CoverExisting.Click += new System.EventHandler(this.btn_CoverExisting_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_PlayPreviewNew);
            this.groupBox3.Controls.Add(this.btn_PlayAudioNew);
            this.groupBox3.Controls.Add(this.btn_PlayPreviewExisting);
            this.groupBox3.Controls.Add(this.btn_PlayAudioExisting);
            this.groupBox3.Controls.Add(this.btn_AddDD);
            this.groupBox3.Controls.Add(this.btn_AddTracks);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txt_DDNew);
            this.groupBox3.Controls.Add(this.txt_DDExisting);
            this.groupBox3.Controls.Add(this.txt_AvailTracksNew);
            this.groupBox3.Controls.Add(this.txt_AvailTracksExisting);
            this.groupBox3.Controls.Add(this.txt_AudioNew);
            this.groupBox3.Controls.Add(this.txt_AudioExisting);
            this.groupBox3.Controls.Add(this.txt_PreviewNew);
            this.groupBox3.Controls.Add(this.txt_PreviewExisting);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.lbl_DD);
            this.groupBox3.Controls.Add(this.lbl_AvailableTracks);
            this.groupBox3.Controls.Add(this.lbl_Audio);
            this.groupBox3.Controls.Add(this.lbl_Preview);
            this.groupBox3.Controls.Add(this.txt_VocalsNew);
            this.groupBox3.Controls.Add(this.txt_VocalsExisting);
            this.groupBox3.Controls.Add(this.lbl_txt_Vocals);
            this.groupBox3.Controls.Add(this.lbl_Vocals);
            this.groupBox3.Location = new System.Drawing.Point(8, 400);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(390, 148);
            this.groupBox3.TabIndex = 379;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Comparisons based on Availability or Hash";
            // 
            // btn_PlayPreviewNew
            // 
            this.btn_PlayPreviewNew.Enabled = false;
            this.btn_PlayPreviewNew.Location = new System.Drawing.Point(141, 90);
            this.btn_PlayPreviewNew.Name = "btn_PlayPreviewNew";
            this.btn_PlayPreviewNew.Size = new System.Drawing.Size(62, 21);
            this.btn_PlayPreviewNew.TabIndex = 398;
            this.btn_PlayPreviewNew.Text = "Play Preview";
            this.btn_PlayPreviewNew.UseVisualStyleBackColor = true;
            this.btn_PlayPreviewNew.Click += new System.EventHandler(this.btn_PlayPreviewNew_Click);
            // 
            // btn_PlayAudioNew
            // 
            this.btn_PlayAudioNew.Location = new System.Drawing.Point(141, 64);
            this.btn_PlayAudioNew.Name = "btn_PlayAudioNew";
            this.btn_PlayAudioNew.Size = new System.Drawing.Size(62, 21);
            this.btn_PlayAudioNew.TabIndex = 397;
            this.btn_PlayAudioNew.Text = "Play Audio";
            this.btn_PlayAudioNew.UseVisualStyleBackColor = true;
            this.btn_PlayAudioNew.Click += new System.EventHandler(this.btn_PlayAudioNew_Click);
            // 
            // btn_PlayPreviewExisting
            // 
            this.btn_PlayPreviewExisting.Enabled = false;
            this.btn_PlayPreviewExisting.Location = new System.Drawing.Point(322, 90);
            this.btn_PlayPreviewExisting.Name = "btn_PlayPreviewExisting";
            this.btn_PlayPreviewExisting.Size = new System.Drawing.Size(62, 21);
            this.btn_PlayPreviewExisting.TabIndex = 396;
            this.btn_PlayPreviewExisting.Text = "Play Preview";
            this.btn_PlayPreviewExisting.UseVisualStyleBackColor = true;
            this.btn_PlayPreviewExisting.Click += new System.EventHandler(this.btn_PlayPreview_Click);
            // 
            // btn_PlayAudioExisting
            // 
            this.btn_PlayAudioExisting.Location = new System.Drawing.Point(322, 64);
            this.btn_PlayAudioExisting.Name = "btn_PlayAudioExisting";
            this.btn_PlayAudioExisting.Size = new System.Drawing.Size(62, 21);
            this.btn_PlayAudioExisting.TabIndex = 395;
            this.btn_PlayAudioExisting.Text = "Play Audio";
            this.btn_PlayAudioExisting.UseVisualStyleBackColor = true;
            this.btn_PlayAudioExisting.Click += new System.EventHandler(this.btn_PlayAudio_Click);
            // 
            // btn_AddDD
            // 
            this.btn_AddDD.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddDD.Location = new System.Drawing.Point(327, 14);
            this.btn_AddDD.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddDD.Name = "btn_AddDD";
            this.btn_AddDD.Size = new System.Drawing.Size(14, 15);
            this.btn_AddDD.TabIndex = 386;
            this.btn_AddDD.Text = "+";
            this.btn_AddDD.UseVisualStyleBackColor = false;
            this.btn_AddDD.Click += new System.EventHandler(this.btn_AddDD_Click);
            // 
            // btn_AddTracks
            // 
            this.btn_AddTracks.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddTracks.Location = new System.Drawing.Point(327, 43);
            this.btn_AddTracks.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddTracks.Name = "btn_AddTracks";
            this.btn_AddTracks.Size = new System.Drawing.Size(14, 15);
            this.btn_AddTracks.TabIndex = 385;
            this.btn_AddTracks.Text = "+";
            this.btn_AddTracks.UseVisualStyleBackColor = false;
            this.btn_AddTracks.Click += new System.EventHandler(this.btn_AddInstruments_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.lbl_CustomsForge_ReleaseNotes);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_ReleaseNotesNew);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_ReleaseNotesExisting);
            this.groupBox4.Controls.Add(this.label59);
            this.groupBox4.Controls.Add(this.txt_YouTube_LinkNew);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_LinkExisting);
            this.groupBox4.Controls.Add(this.txt_YouTube_LinkExisting);
            this.groupBox4.Controls.Add(this.lbl_CustomsForge_Like);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_LinkNew);
            this.groupBox4.Controls.Add(this.lbl_CustomsForge_LinkNew);
            this.groupBox4.Controls.Add(this.lbl_YouTube_LinkNew);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_LikeNew);
            this.groupBox4.Controls.Add(this.lbfl_YouTube_Link);
            this.groupBox4.Controls.Add(this.txt_CustomsForge_LikeExisting);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Location = new System.Drawing.Point(413, 539);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 121);
            this.groupBox4.TabIndex = 380;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CustomsForge Details";
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(330, 88);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(18, 17);
            this.button5.TabIndex = 369;
            this.button5.Text = ">";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(330, 66);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(18, 17);
            this.button4.TabIndex = 368;
            this.button4.Text = ">";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(330, 42);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(18, 17);
            this.button3.TabIndex = 367;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(330, 17);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(18, 17);
            this.button2.TabIndex = 366;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lbl_CustomsForge_ReleaseNotes
            // 
            this.lbl_CustomsForge_ReleaseNotes.AutoSize = true;
            this.lbl_CustomsForge_ReleaseNotes.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_CustomsForge_ReleaseNotes.Location = new System.Drawing.Point(190, 90);
            this.lbl_CustomsForge_ReleaseNotes.Name = "lbl_CustomsForge_ReleaseNotes";
            this.lbl_CustomsForge_ReleaseNotes.Size = new System.Drawing.Size(22, 13);
            this.lbl_CustomsForge_ReleaseNotes.TabIndex = 329;
            this.lbl_CustomsForge_ReleaseNotes.Text = "Vs.";
            // 
            // txt_CustomsForge_ReleaseNotesNew
            // 
            this.txt_CustomsForge_ReleaseNotesNew.Cue = "ReleaseNotes New";
            this.txt_CustomsForge_ReleaseNotesNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_ReleaseNotesNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_ReleaseNotesNew.Location = new System.Drawing.Point(79, 87);
            this.txt_CustomsForge_ReleaseNotesNew.Name = "txt_CustomsForge_ReleaseNotesNew";
            this.txt_CustomsForge_ReleaseNotesNew.Size = new System.Drawing.Size(109, 20);
            this.txt_CustomsForge_ReleaseNotesNew.TabIndex = 326;
            this.txt_CustomsForge_ReleaseNotesNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_CustomsForge_ReleaseNotesExisting
            // 
            this.txt_CustomsForge_ReleaseNotesExisting.Cue = "ReleaseNotes Existing";
            this.txt_CustomsForge_ReleaseNotesExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_ReleaseNotesExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_ReleaseNotesExisting.Location = new System.Drawing.Point(213, 87);
            this.txt_CustomsForge_ReleaseNotesExisting.Name = "txt_CustomsForge_ReleaseNotesExisting";
            this.txt_CustomsForge_ReleaseNotesExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_CustomsForge_ReleaseNotesExisting.TabIndex = 327;
            this.txt_CustomsForge_ReleaseNotesExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(4, 90);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(77, 13);
            this.label59.TabIndex = 328;
            this.label59.Text = "Release Notes";
            // 
            // txt_YouTube_LinkNew
            // 
            this.txt_YouTube_LinkNew.Cue = "YouTube Link New";
            this.txt_YouTube_LinkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_YouTube_LinkNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_YouTube_LinkNew.Location = new System.Drawing.Point(79, 15);
            this.txt_YouTube_LinkNew.Name = "txt_YouTube_LinkNew";
            this.txt_YouTube_LinkNew.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_YouTube_LinkNew.Size = new System.Drawing.Size(110, 20);
            this.txt_YouTube_LinkNew.TabIndex = 314;
            this.txt_YouTube_LinkNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_CustomsForge_LinkExisting
            // 
            this.txt_CustomsForge_LinkExisting.Cue = "CustomsForge Existing";
            this.txt_CustomsForge_LinkExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_LinkExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_LinkExisting.Location = new System.Drawing.Point(213, 40);
            this.txt_CustomsForge_LinkExisting.Name = "txt_CustomsForge_LinkExisting";
            this.txt_CustomsForge_LinkExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_CustomsForge_LinkExisting.TabIndex = 317;
            this.txt_CustomsForge_LinkExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // txt_YouTube_LinkExisting
            // 
            this.txt_YouTube_LinkExisting.Cue = "YouTube Link Existing";
            this.txt_YouTube_LinkExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_YouTube_LinkExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_YouTube_LinkExisting.Location = new System.Drawing.Point(213, 15);
            this.txt_YouTube_LinkExisting.Name = "txt_YouTube_LinkExisting";
            this.txt_YouTube_LinkExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_YouTube_LinkExisting.TabIndex = 315;
            this.txt_YouTube_LinkExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // lbl_CustomsForge_Like
            // 
            this.lbl_CustomsForge_Like.AutoSize = true;
            this.lbl_CustomsForge_Like.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_CustomsForge_Like.Location = new System.Drawing.Point(191, 67);
            this.lbl_CustomsForge_Like.Name = "lbl_CustomsForge_Like";
            this.lbl_CustomsForge_Like.Size = new System.Drawing.Size(22, 13);
            this.lbl_CustomsForge_Like.TabIndex = 325;
            this.lbl_CustomsForge_Like.Text = "Vs.";
            // 
            // txt_CustomsForge_LinkNew
            // 
            this.txt_CustomsForge_LinkNew.Cue = "CustomsForge New";
            this.txt_CustomsForge_LinkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_LinkNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_LinkNew.Location = new System.Drawing.Point(79, 40);
            this.txt_CustomsForge_LinkNew.Name = "txt_CustomsForge_LinkNew";
            this.txt_CustomsForge_LinkNew.Size = new System.Drawing.Size(110, 20);
            this.txt_CustomsForge_LinkNew.TabIndex = 316;
            this.txt_CustomsForge_LinkNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_CustomsForge_LinkNew
            // 
            this.lbl_CustomsForge_LinkNew.AutoSize = true;
            this.lbl_CustomsForge_LinkNew.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_CustomsForge_LinkNew.Location = new System.Drawing.Point(191, 43);
            this.lbl_CustomsForge_LinkNew.Name = "lbl_CustomsForge_LinkNew";
            this.lbl_CustomsForge_LinkNew.Size = new System.Drawing.Size(22, 13);
            this.lbl_CustomsForge_LinkNew.TabIndex = 324;
            this.lbl_CustomsForge_LinkNew.Text = "Vs.";
            // 
            // lbl_YouTube_LinkNew
            // 
            this.lbl_YouTube_LinkNew.AutoSize = true;
            this.lbl_YouTube_LinkNew.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_YouTube_LinkNew.Location = new System.Drawing.Point(191, 18);
            this.lbl_YouTube_LinkNew.Name = "lbl_YouTube_LinkNew";
            this.lbl_YouTube_LinkNew.Size = new System.Drawing.Size(22, 13);
            this.lbl_YouTube_LinkNew.TabIndex = 323;
            this.lbl_YouTube_LinkNew.Text = "Vs.";
            // 
            // txt_CustomsForge_LikeNew
            // 
            this.txt_CustomsForge_LikeNew.Cue = "Like New";
            this.txt_CustomsForge_LikeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_LikeNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_LikeNew.Location = new System.Drawing.Point(79, 64);
            this.txt_CustomsForge_LikeNew.Name = "txt_CustomsForge_LikeNew";
            this.txt_CustomsForge_LikeNew.Size = new System.Drawing.Size(110, 20);
            this.txt_CustomsForge_LikeNew.TabIndex = 318;
            this.txt_CustomsForge_LikeNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbfl_YouTube_Link
            // 
            this.lbfl_YouTube_Link.AutoSize = true;
            this.lbfl_YouTube_Link.Location = new System.Drawing.Point(4, 18);
            this.lbfl_YouTube_Link.Name = "lbfl_YouTube_Link";
            this.lbfl_YouTube_Link.Size = new System.Drawing.Size(51, 13);
            this.lbfl_YouTube_Link.TabIndex = 322;
            this.lbfl_YouTube_Link.Text = "YouTube";
            // 
            // txt_CustomsForge_LikeExisting
            // 
            this.txt_CustomsForge_LikeExisting.Cue = "Like Existing";
            this.txt_CustomsForge_LikeExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CustomsForge_LikeExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_CustomsForge_LikeExisting.Location = new System.Drawing.Point(213, 64);
            this.txt_CustomsForge_LikeExisting.Name = "txt_CustomsForge_LikeExisting";
            this.txt_CustomsForge_LikeExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_CustomsForge_LikeExisting.TabIndex = 319;
            this.txt_CustomsForge_LikeExisting.TextChanged += new System.EventHandler(this.ExistingChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 43);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(74, 13);
            this.label33.TabIndex = 321;
            this.label33.Text = "CustomsForge";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 67);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 13);
            this.label32.TabIndex = 320;
            this.label32.Text = "Like";
            // 
            // btn_AddTunning
            // 
            this.btn_AddTunning.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddTunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddTunning.Location = new System.Drawing.Point(403, 336);
            this.btn_AddTunning.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddTunning.Name = "btn_AddTunning";
            this.btn_AddTunning.Size = new System.Drawing.Size(14, 15);
            this.btn_AddTunning.TabIndex = 387;
            this.btn_AddTunning.Text = "+";
            this.btn_AddTunning.UseVisualStyleBackColor = false;
            this.btn_AddTunning.Click += new System.EventHandler(this.btn_AddTunning_Click);
            // 
            // btn_AddVersion1
            // 
            this.btn_AddVersion1.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddVersion1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddVersion1.Location = new System.Drawing.Point(334, 279);
            this.btn_AddVersion1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddVersion1.Name = "btn_AddVersion1";
            this.btn_AddVersion1.Size = new System.Drawing.Size(14, 15);
            this.btn_AddVersion1.TabIndex = 388;
            this.btn_AddVersion1.Text = "+";
            this.btn_AddVersion1.UseVisualStyleBackColor = false;
            this.btn_AddVersion1.Click += new System.EventHandler(this.btn_AddVersion_Click);
            // 
            // btn_AddAuthor
            // 
            this.btn_AddAuthor.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddAuthor.Location = new System.Drawing.Point(442, 254);
            this.btn_AddAuthor.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AddAuthor.Name = "btn_AddAuthor";
            this.btn_AddAuthor.Size = new System.Drawing.Size(14, 15);
            this.btn_AddAuthor.TabIndex = 389;
            this.btn_AddAuthor.Text = "+";
            this.btn_AddAuthor.UseVisualStyleBackColor = false;
            this.btn_AddAuthor.Click += new System.EventHandler(this.btn_AddAuthor_Click);
            // 
            // lblSoye
            // 
            this.lblSoye.AutoSize = true;
            this.lblSoye.Location = new System.Drawing.Point(93, 381);
            this.lblSoye.Name = "lblSoye";
            this.lblSoye.Size = new System.Drawing.Size(46, 13);
            this.lblSoye.TabIndex = 392;
            this.lblSoye.Text = "File Size";
            // 
            // txt_SizeExisting
            // 
            this.txt_SizeExisting.Cue = "Size Existing";
            this.txt_SizeExisting.Enabled = false;
            this.txt_SizeExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SizeExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_SizeExisting.Location = new System.Drawing.Point(284, 378);
            this.txt_SizeExisting.Name = "txt_SizeExisting";
            this.txt_SizeExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_SizeExisting.TabIndex = 391;
            // 
            // txt_SizeNew
            // 
            this.txt_SizeNew.Cue = "Size New";
            this.txt_SizeNew.Enabled = false;
            this.txt_SizeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SizeNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_SizeNew.Location = new System.Drawing.Point(143, 378);
            this.txt_SizeNew.Name = "txt_SizeNew";
            this.txt_SizeNew.Size = new System.Drawing.Size(116, 20);
            this.txt_SizeNew.TabIndex = 390;
            this.txt_SizeNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_Size
            // 
            this.lbl_Size.AutoSize = true;
            this.lbl_Size.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Size.Location = new System.Drawing.Point(261, 381);
            this.lbl_Size.Name = "lbl_Size";
            this.lbl_Size.Size = new System.Drawing.Size(22, 13);
            this.lbl_Size.TabIndex = 393;
            this.lbl_Size.Text = "Vs.";
            // 
            // lbl_Multitrack
            // 
            this.lbl_Multitrack.AutoSize = true;
            this.lbl_Multitrack.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Multitrack.Location = new System.Drawing.Point(262, 307);
            this.lbl_Multitrack.Name = "lbl_Multitrack";
            this.lbl_Multitrack.Size = new System.Drawing.Size(22, 13);
            this.lbl_Multitrack.TabIndex = 394;
            this.lbl_Multitrack.Text = "Vs.";
            // 
            // chbx_UseBrakets
            // 
            this.chbx_UseBrakets.AutoSize = true;
            this.chbx_UseBrakets.Checked = true;
            this.chbx_UseBrakets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_UseBrakets.Enabled = false;
            this.chbx_UseBrakets.Location = new System.Drawing.Point(489, 334);
            this.chbx_UseBrakets.Name = "chbx_UseBrakets";
            this.chbx_UseBrakets.Size = new System.Drawing.Size(271, 17);
            this.chbx_UseBrakets.TabIndex = 395;
            this.chbx_UseBrakets.Text = "At Save Use Brackets for Additional Title added info";
            this.chbx_UseBrakets.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Enabled = false;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(431, 205);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(14, 15);
            this.button6.TabIndex = 396;
            this.button6.Text = "+";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Enabled = false;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(38, 205);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(14, 15);
            this.button7.TabIndex = 397;
            this.button7.Text = "+";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // Duplicates_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 779);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.chbx_UseBrakets);
            this.Controls.Add(this.lbl_Multitrack);
            this.Controls.Add(this.lbl_Size);
            this.Controls.Add(this.lblSoye);
            this.Controls.Add(this.txt_SizeExisting);
            this.Controls.Add(this.txt_SizeNew);
            this.Controls.Add(this.btn_AddAuthor);
            this.Controls.Add(this.btn_AddVersion1);
            this.Controls.Add(this.btn_AddTunning);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_CoverNew);
            this.Controls.Add(this.btn_CoverExisting);
            this.Controls.Add(this.txt_MultiTrackExisting);
            this.Controls.Add(this.txt_MultiTrackNew);
            this.Controls.Add(this.chbx_MultiTrackNew);
            this.Controls.Add(this.chbx_MultiTrackExisting);
            this.Controls.Add(this.txt_AlternateNoNew);
            this.Controls.Add(this.txt_AlternateNoExisting);
            this.Controls.Add(this.chbx_IsAlternateExisting);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_ArtistSortNew);
            this.Controls.Add(this.btn_ArtistSortExisting);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_ArtistNew);
            this.Controls.Add(this.btn_ArtistExisting);
            this.Controls.Add(this.btn_AlbumNew);
            this.Controls.Add(this.btn_AlbumExisting);
            this.Controls.Add(this.lbl_Artist);
            this.Controls.Add(this.lbl_Album);
            this.Controls.Add(this.btn_AuthorNew);
            this.Controls.Add(this.btn_AuthorExisting);
            this.Controls.Add(this.btn_TitleSortNew);
            this.Controls.Add(this.btn_TitleSortExisting);
            this.Controls.Add(this.btn_TitleNew);
            this.Controls.Add(this.btn_TitleExisting);
            this.Controls.Add(this.chbx_Autosave);
            this.Controls.Add(this.txt_AlbumExisting);
            this.Controls.Add(this.txt_ArtistExisting);
            this.Controls.Add(this.btn_RemoveOldNew);
            this.Controls.Add(this.chbx_IgnoreDupli);
            this.Controls.Add(this.lbl_diffCount);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lbl_IDExisting);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.btn_UpdateExisting);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txt_AlbumNew);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lbl_Is_Original);
            this.Controls.Add(this.chbx_IsOriginal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Reference);
            this.Controls.Add(this.lbl_DLCID);
            this.Controls.Add(this.lbl_Tuning);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.lbl_Author);
            this.Controls.Add(this.lbl_Toolkit);
            this.Controls.Add(this.lbl_IsOriginal);
            this.Controls.Add(this.lbl_FileName);
            this.Controls.Add(this.lbl_ArtistSort);
            this.Controls.Add(this.lbl_TitleSort);
            this.Controls.Add(this.txt_VersionExisting);
            this.Controls.Add(this.txt_VersionNew);
            this.Controls.Add(this.txt_FileNameExisting);
            this.Controls.Add(this.txt_FileNameNew);
            this.Controls.Add(this.txt_ArtistNew);
            this.Controls.Add(this.txt_Comment);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.lbl_AlbumArt);
            this.Controls.Add(this.txt_DLCIDExisting);
            this.Controls.Add(this.txt_DLCIDNew);
            this.Controls.Add(this.txt_TuningExisting);
            this.Controls.Add(this.txt_TuningNew);
            this.Controls.Add(this.txt_AuthorExisting);
            this.Controls.Add(this.txt_AuthorNew);
            this.Controls.Add(this.txt_IsOriginalExisting);
            this.Controls.Add(this.txt_IsOriginalNew);
            this.Controls.Add(this.txt_ToolkitExisting);
            this.Controls.Add(this.txt_ToolkitNew);
            this.Controls.Add(this.txt_TitleSortExisting);
            this.Controls.Add(this.txt_TitleSortNew);
            this.Controls.Add(this.txt_ArtistSortExisting);
            this.Controls.Add(this.txt_ArtistSortNew);
            this.Controls.Add(this.txt_TitleExisting);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.txt_TitleNew);
            this.Controls.Add(this.picbx_AlbumArtPathNew);
            this.Controls.Add(this.picbx_AlbumArtPathExisting);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.chbx_IsAlternateNew);
            this.Controls.Add(this.btn_Alternate);
            this.Controls.Add(this.btn_Ignore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Name = "Duplicates_Management";
            this.Text = "Duplicates Management";
            this.Load += new System.EventHandler(this.DuplicatesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathExisting)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AlternateNoExisting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_AlternateNoNew)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label16;
        private CueTextBox txt_VersionExisting;
        private CueTextBox txt_VersionNew;
        private CueTextBox txt_FileNameExisting;
        private CueTextBox txt_FileNameNew;
        private CueTextBox txt_ArtistNew;
        private RichTextBox txt_Comment;
        private RichTextBox txt_Description;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private CueTextBox txt_JSONRhythmExisting;
        private CueTextBox txt_JSONRhythmNew;
        private Label lbl_AlbumArt;
        private CueTextBox txt_JSONComboExisting;
        private CueTextBox txt_JSONComboNew;
        private CueTextBox txt_JSONBassExisting;
        private CueTextBox txt_JSONBassNew;
        private CueTextBox txt_JSONLeadExisting;
        private CueTextBox txt_JSONLeadNew;
        private CueTextBox txt_XMLRhythmExisting;
        private CueTextBox txt_XMLRhythmNew;
        private CueTextBox txt_XMLComboExisting;
        private CueTextBox txt_XMLComboNew;
        private CueTextBox txt_XMLBassExisting;
        private CueTextBox txt_XMLBassNew;
        private CueTextBox txt_PreviewExisting;
        private CueTextBox txt_PreviewNew;
        private CueTextBox txt_XMLLeadExisting;
        private CueTextBox txt_XMLLeadNew;
        private CueTextBox txt_AudioExisting;
        private CueTextBox txt_AudioNew;
        private CueTextBox txt_AvailTracksExisting;
        private CueTextBox txt_AvailTracksNew;
        private CueTextBox txt_DDExisting;
        private CueTextBox txt_DDNew;
        private CueTextBox txt_DLCIDExisting;
        private CueTextBox txt_DLCIDNew;
        private CueTextBox txt_TuningExisting;
        private CueTextBox txt_TuningNew;
        private CueTextBox txt_AuthorExisting;
        private CueTextBox txt_AuthorNew;
        private CueTextBox txt_IsOriginalExisting;
        private CueTextBox txt_IsOriginalNew;
        private CueTextBox txt_ToolkitExisting;
        private CueTextBox txt_ToolkitNew;
        private CueTextBox txt_TitleSortExisting;
        private CueTextBox txt_TitleSortNew;
        private CueTextBox txt_ArtistSortExisting;
        private CueTextBox txt_ArtistSortNew;
        private CueTextBox txt_TitleExisting;
        private Label lbl_Title;
        private CueTextBox txt_TitleNew;
        private PictureBox picbx_AlbumArtPathNew;
        private PictureBox picbx_AlbumArtPathExisting;
        private Button btn_Update;
        private CheckBox chbx_IsAlternateNew;
        private Button btn_Alternate;
        private Button btn_Ignore;
        private Label lbl_TitleSort;
        private Label lbl_ArtistSort;
        private Label lbl_FileName;
        private Label lbl_IsOriginal;
        private Label lbl_Toolkit;
        private Label lbl_Author;
        private Label lbl_Version;
        private Label lbl_Tuning;
        private Label lbl_DLCID;
        private Label lbl_DD;
        private Label lbl_AvailableTracks;
        private Label lbl_Audio;
        private Label lbl_Preview;
        private Label lbl_XMLLead;
        private Label lbl_XMLBass;
        private Label lbl_XMLCombo;
        private Label lbl_XMLRhythm;
        private Label lbl_JSONLead;
        private Label lbl_JSONBass;
        private Label lbl_JSONCombo;
        private Label lbl_JSONRhythm;
        private Label lbl_Reference;
        private Button button1;
        private Label label1;
        private Label label2;
        private CheckBox chbx_IsOriginal;
        private Label label17;
        private Label lbl_Is_Original;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private CueTextBox txt_AlbumNew;
        private Label label23;
        private Button btn_UpdateExisting;
        private Label label24;
        private Label label25;
        private Label lbl_IDExisting;
        private Label label26;
        private Button btn_DecompressAll;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label lbl_diffCount;
        private CheckBox chbx_IgnoreDupli;
        private Button btn_RemoveOldNew;
        private Label lbl_Vocals;
        private Label lbl_txt_Vocals;
        private CueTextBox txt_VocalsExisting;
        private CueTextBox txt_VocalsNew;
        private CueTextBox txt_AlbumExisting;
        private CueTextBox txt_ArtistExisting;
        private CheckBox chbx_Autosave;
        private Button btn_TitleNew;
        private Button btn_TitleExisting;
        private Button btn_TitleSortNew;
        private Button btn_TitleSortExisting;
        private Button btn_AuthorNew;
        private Button btn_AuthorExisting;
        private Label lbl_Album;
        private Label lbl_Artist;
        private Button btn_AlbumNew;
        private Button btn_AlbumExisting;
        private Button btn_ArtistNew;
        private Button btn_ArtistExisting;
        private Label label4;
        private Label label18;
        private Button btn_ArtistSortNew;
        private Button btn_ArtistSortExisting;
        private GroupBox groupBox2;
        private CheckBox chbx_IsAlternateExisting;
        private NumericUpDown txt_AlternateNoExisting;
        private NumericUpDown txt_AlternateNoNew;
        private CheckBox chbx_MultiTrackExisting;
        private CheckBox chbx_MultiTrackNew;
        private ComboBox txt_MultiTrackNew;
        private ComboBox txt_MultiTrackExisting;
        private Button btn_CoverNew;
        private Button btn_CoverExisting;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label lbl_CustomsForge_ReleaseNotes;
        private CueTextBox txt_CustomsForge_ReleaseNotesNew;
        private CueTextBox txt_CustomsForge_ReleaseNotesExisting;
        private Label label59;
        private CueTextBox txt_YouTube_LinkNew;
        private CueTextBox txt_CustomsForge_LinkExisting;
        private CueTextBox txt_YouTube_LinkExisting;
        private Label lbl_CustomsForge_Like;
        private CueTextBox txt_CustomsForge_LinkNew;
        private Label lbl_CustomsForge_LinkNew;
        private Label lbl_YouTube_LinkNew;
        private CueTextBox txt_CustomsForge_LikeNew;
        private Label lbfl_YouTube_Link;
        private CueTextBox txt_CustomsForge_LikeExisting;
        private Label label33;
        private Label label32;
        private Button btn_AddDD;
        private Button btn_AddTracks;
        private Button btn_AddTunning;
        private Button btn_AddVersion1;
        private Button btn_AddAuthor;
        private Label lbl_Existing;
        private Label lbl_New;
        private Button btn_AddAge;
        private Label lblSoye;
        private CueTextBox txt_SizeExisting;
        private CueTextBox txt_SizeNew;
        private Label lbl_Size;
        private Label lbl_Multitrack;
        private Button btn_GoToNew;
        private Button btn_GoToExisting;
        private Button btn_PlayPreviewNew;
        private Button btn_PlayAudioNew;
        private Button btn_PlayPreviewExisting;
        private Button btn_PlayAudioExisting;
        private CheckBox chbx_UseBrakets;
        private Button button6;
        private Button button7;
    }
}