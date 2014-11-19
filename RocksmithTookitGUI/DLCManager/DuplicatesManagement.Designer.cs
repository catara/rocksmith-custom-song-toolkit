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
    partial class DuplicatesManagement
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
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_AlbumArt = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.picbx_AlbumArtPathNew = new System.Windows.Forms.PictureBox();
            this.picbx_AlbumArtPathExisting = new System.Windows.Forms.PictureBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
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
            this.lbl_XMLVocal = new System.Windows.Forms.Label();
            this.lbl_JSONLead = new System.Windows.Forms.Label();
            this.lbl_JSONBass = new System.Windows.Forms.Label();
            this.lbl_JSONCombo = new System.Windows.Forms.Label();
            this.lbl_JSONRhythm = new System.Windows.Forms.Label();
            this.lbl_JSONVocal = new System.Windows.Forms.Label();
            this.lbl_Reference = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_VersionExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_VersionNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FileNameExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FileNameNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONVocalExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONVocalNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONRhythmExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONRhythmNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONComboExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONComboNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONBassExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONBassNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONLeadExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_JSONLeadNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLVocalExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLVocalNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLRhythmExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLRhythmNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLComboExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLComboNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLBassExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLBassNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PreviewNew = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLLeadExisting = new RocksmithToolkitGUI.CueTextBox();
            this.txt_XMLLeadNew = new RocksmithToolkitGUI.CueTextBox();
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cueTextBox1 = new RocksmithToolkitGUI.CueTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathExisting)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(73, 632);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 13);
            this.label16.TabIndex = 283;
            this.label16.Text = "XML Rhythm";
            // 
            // txt_Comment
            // 
            this.txt_Comment.Location = new System.Drawing.Point(603, 263);
            this.txt_Comment.Name = "txt_Comment";
            this.txt_Comment.Size = new System.Drawing.Size(156, 69);
            this.txt_Comment.TabIndex = 277;
            this.txt_Comment.Text = "";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(603, 177);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(156, 69);
            this.txt_Description.TabIndex = 276;
            this.txt_Description.Text = "";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(130, 337);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 13);
            this.label15.TabIndex = 275;
            this.label15.Text = "Avaialble Tracks";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(130, 389);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 13);
            this.label14.TabIndex = 274;
            this.label14.Text = "Preview";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(130, 363);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 273;
            this.label13.Text = "Audio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 427);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 272;
            this.label12.Text = "XML Lead";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 450);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 271;
            this.label11.Text = "XML Bass";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(73, 478);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 270;
            this.label10.Text = "XML Combo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 504);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 269;
            this.label9.Text = "XML Rhythm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 531);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 268;
            this.label8.Text = "XML Vocal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 556);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 267;
            this.label7.Text = "JSON Lead";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 580);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 266;
            this.label6.Text = "JSON Bass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 608);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 265;
            this.label5.Text = "JSON Combo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 608);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 264;
            this.label4.Text = "JSON ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 660);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 263;
            this.label3.Text = "JSON Vocal";
            // 
            // lbl_AlbumArt
            // 
            this.lbl_AlbumArt.AutoSize = true;
            this.lbl_AlbumArt.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_AlbumArt.Location = new System.Drawing.Point(570, 395);
            this.lbl_AlbumArt.Name = "lbl_AlbumArt";
            this.lbl_AlbumArt.Size = new System.Drawing.Size(22, 13);
            this.lbl_AlbumArt.TabIndex = 258;
            this.lbl_AlbumArt.Text = "Vs.";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Title.Location = new System.Drawing.Point(261, 60);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(22, 13);
            this.lbl_Title.TabIndex = 218;
            this.lbl_Title.Text = "Vs.";
            // 
            // picbx_AlbumArtPathNew
            // 
            this.picbx_AlbumArtPathNew.Location = new System.Drawing.Point(402, 337);
            this.picbx_AlbumArtPathNew.Name = "picbx_AlbumArtPathNew";
            this.picbx_AlbumArtPathNew.Size = new System.Drawing.Size(166, 145);
            this.picbx_AlbumArtPathNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPathNew.TabIndex = 216;
            this.picbx_AlbumArtPathNew.TabStop = false;
            // 
            // picbx_AlbumArtPathExisting
            // 
            this.picbx_AlbumArtPathExisting.Location = new System.Drawing.Point(592, 336);
            this.picbx_AlbumArtPathExisting.Name = "picbx_AlbumArtPathExisting";
            this.picbx_AlbumArtPathExisting.Size = new System.Drawing.Size(167, 146);
            this.picbx_AlbumArtPathExisting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPathExisting.TabIndex = 215;
            this.picbx_AlbumArtPathExisting.TabStop = false;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(639, 121);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(120, 35);
            this.btn_Update.TabIndex = 214;
            this.btn_Update.Text = "Update and Overrite";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Checked = true;
            this.chbx_Broken.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_Broken.Enabled = false;
            this.chbx_Broken.Location = new System.Drawing.Point(565, 121);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(68, 17);
            this.chbx_Broken.TabIndex = 211;
            this.chbx_Broken.Text = "Alternate";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // btn_Alternate
            // 
            this.btn_Alternate.Location = new System.Drawing.Point(639, 55);
            this.btn_Alternate.Name = "btn_Alternate";
            this.btn_Alternate.Size = new System.Drawing.Size(120, 35);
            this.btn_Alternate.TabIndex = 212;
            this.btn_Alternate.Text = "Alternate";
            this.btn_Alternate.UseVisualStyleBackColor = true;
            this.btn_Alternate.Click += new System.EventHandler(this.btn_Alternate_Click);
            // 
            // btn_Ignore
            // 
            this.btn_Ignore.Location = new System.Drawing.Point(639, 88);
            this.btn_Ignore.Name = "btn_Ignore";
            this.btn_Ignore.Size = new System.Drawing.Size(120, 35);
            this.btn_Ignore.TabIndex = 213;
            this.btn_Ignore.Text = "Ignore";
            this.btn_Ignore.UseVisualStyleBackColor = true;
            this.btn_Ignore.Click += new System.EventHandler(this.btn_Ignore_Click);
            // 
            // lbl_TitleSort
            // 
            this.lbl_TitleSort.AutoSize = true;
            this.lbl_TitleSort.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_TitleSort.Location = new System.Drawing.Point(261, 84);
            this.lbl_TitleSort.Name = "lbl_TitleSort";
            this.lbl_TitleSort.Size = new System.Drawing.Size(22, 13);
            this.lbl_TitleSort.TabIndex = 284;
            this.lbl_TitleSort.Text = "Vs.";
            // 
            // lbl_ArtistSort
            // 
            this.lbl_ArtistSort.AutoSize = true;
            this.lbl_ArtistSort.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_ArtistSort.Location = new System.Drawing.Point(261, 110);
            this.lbl_ArtistSort.Name = "lbl_ArtistSort";
            this.lbl_ArtistSort.Size = new System.Drawing.Size(22, 13);
            this.lbl_ArtistSort.TabIndex = 285;
            this.lbl_ArtistSort.Text = "Vs.";
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.AutoSize = true;
            this.lbl_FileName.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_FileName.Location = new System.Drawing.Point(261, 135);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(22, 13);
            this.lbl_FileName.TabIndex = 286;
            this.lbl_FileName.Text = "Vs.";
            // 
            // lbl_IsOriginal
            // 
            this.lbl_IsOriginal.AutoSize = true;
            this.lbl_IsOriginal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_IsOriginal.Location = new System.Drawing.Point(261, 160);
            this.lbl_IsOriginal.Name = "lbl_IsOriginal";
            this.lbl_IsOriginal.Size = new System.Drawing.Size(22, 13);
            this.lbl_IsOriginal.TabIndex = 287;
            this.lbl_IsOriginal.Text = "Vs.";
            // 
            // lbl_Toolkit
            // 
            this.lbl_Toolkit.AutoSize = true;
            this.lbl_Toolkit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Toolkit.Location = new System.Drawing.Point(261, 184);
            this.lbl_Toolkit.Name = "lbl_Toolkit";
            this.lbl_Toolkit.Size = new System.Drawing.Size(22, 13);
            this.lbl_Toolkit.TabIndex = 288;
            this.lbl_Toolkit.Text = "Vs.";
            // 
            // lbl_Author
            // 
            this.lbl_Author.AutoSize = true;
            this.lbl_Author.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Author.Location = new System.Drawing.Point(261, 209);
            this.lbl_Author.Name = "lbl_Author";
            this.lbl_Author.Size = new System.Drawing.Size(22, 13);
            this.lbl_Author.TabIndex = 289;
            this.lbl_Author.Text = "Vs.";
            // 
            // lbl_Version
            // 
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Version.Location = new System.Drawing.Point(261, 233);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(22, 13);
            this.lbl_Version.TabIndex = 290;
            this.lbl_Version.Text = "Vs.";
            // 
            // lbl_Tuning
            // 
            this.lbl_Tuning.AutoSize = true;
            this.lbl_Tuning.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Tuning.Location = new System.Drawing.Point(261, 259);
            this.lbl_Tuning.Name = "lbl_Tuning";
            this.lbl_Tuning.Size = new System.Drawing.Size(22, 13);
            this.lbl_Tuning.TabIndex = 291;
            this.lbl_Tuning.Text = "Vs.";
            // 
            // lbl_DLCID
            // 
            this.lbl_DLCID.AutoSize = true;
            this.lbl_DLCID.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_DLCID.Location = new System.Drawing.Point(261, 285);
            this.lbl_DLCID.Name = "lbl_DLCID";
            this.lbl_DLCID.Size = new System.Drawing.Size(22, 13);
            this.lbl_DLCID.TabIndex = 292;
            this.lbl_DLCID.Text = "Vs.";
            // 
            // lbl_DD
            // 
            this.lbl_DD.AutoSize = true;
            this.lbl_DD.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_DD.Location = new System.Drawing.Point(261, 311);
            this.lbl_DD.Name = "lbl_DD";
            this.lbl_DD.Size = new System.Drawing.Size(22, 13);
            this.lbl_DD.TabIndex = 293;
            this.lbl_DD.Text = "Vs.";
            // 
            // lbl_AvailableTracks
            // 
            this.lbl_AvailableTracks.AutoSize = true;
            this.lbl_AvailableTracks.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_AvailableTracks.Location = new System.Drawing.Point(261, 337);
            this.lbl_AvailableTracks.Name = "lbl_AvailableTracks";
            this.lbl_AvailableTracks.Size = new System.Drawing.Size(22, 13);
            this.lbl_AvailableTracks.TabIndex = 294;
            this.lbl_AvailableTracks.Text = "Vs.";
            // 
            // lbl_Audio
            // 
            this.lbl_Audio.AutoSize = true;
            this.lbl_Audio.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Audio.Location = new System.Drawing.Point(261, 363);
            this.lbl_Audio.Name = "lbl_Audio";
            this.lbl_Audio.Size = new System.Drawing.Size(22, 13);
            this.lbl_Audio.TabIndex = 295;
            this.lbl_Audio.Text = "Vs.";
            // 
            // lbl_Preview
            // 
            this.lbl_Preview.AutoSize = true;
            this.lbl_Preview.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_Preview.Location = new System.Drawing.Point(261, 389);
            this.lbl_Preview.Name = "lbl_Preview";
            this.lbl_Preview.Size = new System.Drawing.Size(22, 13);
            this.lbl_Preview.TabIndex = 296;
            this.lbl_Preview.Text = "Vs.";
            // 
            // lbl_XMLLead
            // 
            this.lbl_XMLLead.AutoSize = true;
            this.lbl_XMLLead.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLLead.Location = new System.Drawing.Point(261, 427);
            this.lbl_XMLLead.Name = "lbl_XMLLead";
            this.lbl_XMLLead.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLLead.TabIndex = 297;
            this.lbl_XMLLead.Text = "Vs.";
            // 
            // lbl_XMLBass
            // 
            this.lbl_XMLBass.AutoSize = true;
            this.lbl_XMLBass.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLBass.Location = new System.Drawing.Point(261, 450);
            this.lbl_XMLBass.Name = "lbl_XMLBass";
            this.lbl_XMLBass.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLBass.TabIndex = 298;
            this.lbl_XMLBass.Text = "Vs.";
            // 
            // lbl_XMLCombo
            // 
            this.lbl_XMLCombo.AutoSize = true;
            this.lbl_XMLCombo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLCombo.Location = new System.Drawing.Point(261, 476);
            this.lbl_XMLCombo.Name = "lbl_XMLCombo";
            this.lbl_XMLCombo.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLCombo.TabIndex = 299;
            this.lbl_XMLCombo.Text = "Vs.";
            // 
            // lbl_XMLRhythm
            // 
            this.lbl_XMLRhythm.AutoSize = true;
            this.lbl_XMLRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLRhythm.Location = new System.Drawing.Point(261, 504);
            this.lbl_XMLRhythm.Name = "lbl_XMLRhythm";
            this.lbl_XMLRhythm.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLRhythm.TabIndex = 300;
            this.lbl_XMLRhythm.Text = "Vs.";
            // 
            // lbl_XMLVocal
            // 
            this.lbl_XMLVocal.AutoSize = true;
            this.lbl_XMLVocal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_XMLVocal.Location = new System.Drawing.Point(261, 528);
            this.lbl_XMLVocal.Name = "lbl_XMLVocal";
            this.lbl_XMLVocal.Size = new System.Drawing.Size(22, 13);
            this.lbl_XMLVocal.TabIndex = 301;
            this.lbl_XMLVocal.Text = "Vs.";
            // 
            // lbl_JSONLead
            // 
            this.lbl_JSONLead.AutoSize = true;
            this.lbl_JSONLead.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONLead.Location = new System.Drawing.Point(261, 554);
            this.lbl_JSONLead.Name = "lbl_JSONLead";
            this.lbl_JSONLead.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONLead.TabIndex = 302;
            this.lbl_JSONLead.Text = "Vs.";
            // 
            // lbl_JSONBass
            // 
            this.lbl_JSONBass.AutoSize = true;
            this.lbl_JSONBass.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONBass.Location = new System.Drawing.Point(261, 580);
            this.lbl_JSONBass.Name = "lbl_JSONBass";
            this.lbl_JSONBass.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONBass.TabIndex = 303;
            this.lbl_JSONBass.Text = "Vs.";
            // 
            // lbl_JSONCombo
            // 
            this.lbl_JSONCombo.AutoSize = true;
            this.lbl_JSONCombo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONCombo.Location = new System.Drawing.Point(261, 606);
            this.lbl_JSONCombo.Name = "lbl_JSONCombo";
            this.lbl_JSONCombo.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONCombo.TabIndex = 304;
            this.lbl_JSONCombo.Text = "Vs.";
            // 
            // lbl_JSONRhythm
            // 
            this.lbl_JSONRhythm.AutoSize = true;
            this.lbl_JSONRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONRhythm.Location = new System.Drawing.Point(261, 632);
            this.lbl_JSONRhythm.Name = "lbl_JSONRhythm";
            this.lbl_JSONRhythm.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONRhythm.TabIndex = 305;
            this.lbl_JSONRhythm.Text = "Vs.";
            // 
            // lbl_JSONVocal
            // 
            this.lbl_JSONVocal.AutoSize = true;
            this.lbl_JSONVocal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_JSONVocal.Location = new System.Drawing.Point(261, 658);
            this.lbl_JSONVocal.Name = "lbl_JSONVocal";
            this.lbl_JSONVocal.Size = new System.Drawing.Size(22, 13);
            this.lbl_JSONVocal.TabIndex = 306;
            this.lbl_JSONVocal.Text = "Vs.";
            // 
            // lbl_Reference
            // 
            this.lbl_Reference.AutoSize = true;
            this.lbl_Reference.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbl_Reference.Location = new System.Drawing.Point(261, 39);
            this.lbl_Reference.Name = "lbl_Reference";
            this.lbl_Reference.Size = new System.Drawing.Size(22, 13);
            this.lbl_Reference.TabIndex = 307;
            this.lbl_Reference.Text = "Vs.";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(405, 541);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 39);
            this.button1.TabIndex = 308;
            this.button1.Text = "Compare in winMerge";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(402, 488);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 28);
            this.panel1.TabIndex = 309;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(64, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(173, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 310;
            this.label1.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(600, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 311;
            this.label2.Text = "Comment";
            // 
            // txt_VersionExisting
            // 
            this.txt_VersionExisting.Cue = "Version  Existing";
            this.txt_VersionExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VersionExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_VersionExisting.Location = new System.Drawing.Point(283, 230);
            this.txt_VersionExisting.Name = "txt_VersionExisting";
            this.txt_VersionExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_VersionExisting.TabIndex = 282;
            // 
            // txt_VersionNew
            // 
            this.txt_VersionNew.Cue = "Version New";
            this.txt_VersionNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_VersionNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_VersionNew.Location = new System.Drawing.Point(144, 230);
            this.txt_VersionNew.Name = "txt_VersionNew";
            this.txt_VersionNew.Size = new System.Drawing.Size(116, 20);
            this.txt_VersionNew.TabIndex = 281;
            this.txt_VersionNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_FileNameExisting
            // 
            this.txt_FileNameExisting.Cue = "File Name Existing";
            this.txt_FileNameExisting.Enabled = false;
            this.txt_FileNameExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FileNameExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_FileNameExisting.Location = new System.Drawing.Point(283, 132);
            this.txt_FileNameExisting.Name = "txt_FileNameExisting";
            this.txt_FileNameExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_FileNameExisting.TabIndex = 280;
            // 
            // txt_FileNameNew
            // 
            this.txt_FileNameNew.Cue = "File Name New";
            this.txt_FileNameNew.Enabled = false;
            this.txt_FileNameNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FileNameNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_FileNameNew.Location = new System.Drawing.Point(38, 132);
            this.txt_FileNameNew.Name = "txt_FileNameNew";
            this.txt_FileNameNew.Size = new System.Drawing.Size(222, 20);
            this.txt_FileNameNew.TabIndex = 279;
            this.txt_FileNameNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Enabled = false;
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(532, 12);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(222, 20);
            this.txt_Artist.TabIndex = 278;
            this.txt_Artist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONVocalExisting
            // 
            this.txt_JSONVocalExisting.Cue = "JSON Vocal Existing";
            this.txt_JSONVocalExisting.Enabled = false;
            this.txt_JSONVocalExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONVocalExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONVocalExisting.Location = new System.Drawing.Point(283, 655);
            this.txt_JSONVocalExisting.Name = "txt_JSONVocalExisting";
            this.txt_JSONVocalExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONVocalExisting.TabIndex = 262;
            this.txt_JSONVocalExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONVocalNew
            // 
            this.txt_JSONVocalNew.Cue = "JSON Vocal New";
            this.txt_JSONVocalNew.Enabled = false;
            this.txt_JSONVocalNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONVocalNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONVocalNew.Location = new System.Drawing.Point(144, 655);
            this.txt_JSONVocalNew.Name = "txt_JSONVocalNew";
            this.txt_JSONVocalNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONVocalNew.TabIndex = 261;
            this.txt_JSONVocalNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONRhythmExisting
            // 
            this.txt_JSONRhythmExisting.Cue = "JSON Rhythm Existing";
            this.txt_JSONRhythmExisting.Enabled = false;
            this.txt_JSONRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONRhythmExisting.Location = new System.Drawing.Point(283, 629);
            this.txt_JSONRhythmExisting.Name = "txt_JSONRhythmExisting";
            this.txt_JSONRhythmExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONRhythmExisting.TabIndex = 260;
            this.txt_JSONRhythmExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONRhythmNew
            // 
            this.txt_JSONRhythmNew.Cue = "JSON Rhythm New";
            this.txt_JSONRhythmNew.Enabled = false;
            this.txt_JSONRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONRhythmNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONRhythmNew.Location = new System.Drawing.Point(144, 629);
            this.txt_JSONRhythmNew.Name = "txt_JSONRhythmNew";
            this.txt_JSONRhythmNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONRhythmNew.TabIndex = 259;
            this.txt_JSONRhythmNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONComboExisting
            // 
            this.txt_JSONComboExisting.Cue = "JSON Combo Existing";
            this.txt_JSONComboExisting.Enabled = false;
            this.txt_JSONComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONComboExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONComboExisting.Location = new System.Drawing.Point(283, 603);
            this.txt_JSONComboExisting.Name = "txt_JSONComboExisting";
            this.txt_JSONComboExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONComboExisting.TabIndex = 257;
            this.txt_JSONComboExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONComboNew
            // 
            this.txt_JSONComboNew.Cue = "JSON Combo New";
            this.txt_JSONComboNew.Enabled = false;
            this.txt_JSONComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONComboNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONComboNew.Location = new System.Drawing.Point(144, 603);
            this.txt_JSONComboNew.Name = "txt_JSONComboNew";
            this.txt_JSONComboNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONComboNew.TabIndex = 256;
            this.txt_JSONComboNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONBassExisting
            // 
            this.txt_JSONBassExisting.Cue = "Artist Sort Existing";
            this.txt_JSONBassExisting.Enabled = false;
            this.txt_JSONBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONBassExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONBassExisting.Location = new System.Drawing.Point(283, 577);
            this.txt_JSONBassExisting.Name = "txt_JSONBassExisting";
            this.txt_JSONBassExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONBassExisting.TabIndex = 255;
            this.txt_JSONBassExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONBassNew
            // 
            this.txt_JSONBassNew.Cue = "JSON Bass New";
            this.txt_JSONBassNew.Enabled = false;
            this.txt_JSONBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONBassNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONBassNew.Location = new System.Drawing.Point(144, 577);
            this.txt_JSONBassNew.Name = "txt_JSONBassNew";
            this.txt_JSONBassNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONBassNew.TabIndex = 254;
            this.txt_JSONBassNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONLeadExisting
            // 
            this.txt_JSONLeadExisting.Cue = "JSON Lead Existing";
            this.txt_JSONLeadExisting.Enabled = false;
            this.txt_JSONLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONLeadExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONLeadExisting.Location = new System.Drawing.Point(283, 551);
            this.txt_JSONLeadExisting.Name = "txt_JSONLeadExisting";
            this.txt_JSONLeadExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONLeadExisting.TabIndex = 253;
            this.txt_JSONLeadExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_JSONLeadNew
            // 
            this.txt_JSONLeadNew.Cue = "JSON Lead New";
            this.txt_JSONLeadNew.Enabled = false;
            this.txt_JSONLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_JSONLeadNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_JSONLeadNew.Location = new System.Drawing.Point(144, 551);
            this.txt_JSONLeadNew.Name = "txt_JSONLeadNew";
            this.txt_JSONLeadNew.Size = new System.Drawing.Size(116, 20);
            this.txt_JSONLeadNew.TabIndex = 252;
            this.txt_JSONLeadNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLVocalExisting
            // 
            this.txt_XMLVocalExisting.Cue = "XML Vocal Existing";
            this.txt_XMLVocalExisting.Enabled = false;
            this.txt_XMLVocalExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLVocalExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLVocalExisting.Location = new System.Drawing.Point(283, 525);
            this.txt_XMLVocalExisting.Name = "txt_XMLVocalExisting";
            this.txt_XMLVocalExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLVocalExisting.TabIndex = 251;
            this.txt_XMLVocalExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLVocalNew
            // 
            this.txt_XMLVocalNew.Cue = "XML Vocal New";
            this.txt_XMLVocalNew.Enabled = false;
            this.txt_XMLVocalNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLVocalNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLVocalNew.Location = new System.Drawing.Point(144, 525);
            this.txt_XMLVocalNew.Name = "txt_XMLVocalNew";
            this.txt_XMLVocalNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLVocalNew.TabIndex = 250;
            this.txt_XMLVocalNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLRhythmExisting
            // 
            this.txt_XMLRhythmExisting.Cue = "XML Rhythm Existing";
            this.txt_XMLRhythmExisting.Enabled = false;
            this.txt_XMLRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLRhythmExisting.Location = new System.Drawing.Point(283, 499);
            this.txt_XMLRhythmExisting.Name = "txt_XMLRhythmExisting";
            this.txt_XMLRhythmExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLRhythmExisting.TabIndex = 249;
            this.txt_XMLRhythmExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLRhythmNew
            // 
            this.txt_XMLRhythmNew.Cue = "XML Rhythm New";
            this.txt_XMLRhythmNew.Enabled = false;
            this.txt_XMLRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLRhythmNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLRhythmNew.Location = new System.Drawing.Point(144, 499);
            this.txt_XMLRhythmNew.Name = "txt_XMLRhythmNew";
            this.txt_XMLRhythmNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLRhythmNew.TabIndex = 248;
            this.txt_XMLRhythmNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLComboExisting
            // 
            this.txt_XMLComboExisting.Cue = "XML Combo Existing";
            this.txt_XMLComboExisting.Enabled = false;
            this.txt_XMLComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLComboExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLComboExisting.Location = new System.Drawing.Point(283, 473);
            this.txt_XMLComboExisting.Name = "txt_XMLComboExisting";
            this.txt_XMLComboExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLComboExisting.TabIndex = 247;
            this.txt_XMLComboExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLComboNew
            // 
            this.txt_XMLComboNew.Cue = "XML Combo New";
            this.txt_XMLComboNew.Enabled = false;
            this.txt_XMLComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLComboNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLComboNew.Location = new System.Drawing.Point(144, 473);
            this.txt_XMLComboNew.Name = "txt_XMLComboNew";
            this.txt_XMLComboNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLComboNew.TabIndex = 246;
            this.txt_XMLComboNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLBassExisting
            // 
            this.txt_XMLBassExisting.Cue = "XML Bass Existing";
            this.txt_XMLBassExisting.Enabled = false;
            this.txt_XMLBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLBassExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLBassExisting.Location = new System.Drawing.Point(283, 447);
            this.txt_XMLBassExisting.Name = "txt_XMLBassExisting";
            this.txt_XMLBassExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLBassExisting.TabIndex = 245;
            this.txt_XMLBassExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLBassNew
            // 
            this.txt_XMLBassNew.Cue = "XML Bass New";
            this.txt_XMLBassNew.Enabled = false;
            this.txt_XMLBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLBassNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLBassNew.Location = new System.Drawing.Point(144, 447);
            this.txt_XMLBassNew.Name = "txt_XMLBassNew";
            this.txt_XMLBassNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLBassNew.TabIndex = 244;
            this.txt_XMLBassNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_PreviewExisting
            // 
            this.txt_PreviewExisting.Cue = "Preview Existing";
            this.txt_PreviewExisting.Enabled = false;
            this.txt_PreviewExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PreviewExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_PreviewExisting.Location = new System.Drawing.Point(283, 386);
            this.txt_PreviewExisting.Name = "txt_PreviewExisting";
            this.txt_PreviewExisting.Size = new System.Drawing.Size(59, 20);
            this.txt_PreviewExisting.TabIndex = 243;
            this.txt_PreviewExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_PreviewNew
            // 
            this.txt_PreviewNew.Cue = "Preview New";
            this.txt_PreviewNew.Enabled = false;
            this.txt_PreviewNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_PreviewNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_PreviewNew.Location = new System.Drawing.Point(201, 386);
            this.txt_PreviewNew.Name = "txt_PreviewNew";
            this.txt_PreviewNew.Size = new System.Drawing.Size(59, 20);
            this.txt_PreviewNew.TabIndex = 242;
            this.txt_PreviewNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLLeadExisting
            // 
            this.txt_XMLLeadExisting.Cue = "XML Lead Existing";
            this.txt_XMLLeadExisting.Enabled = false;
            this.txt_XMLLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLLeadExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLLeadExisting.Location = new System.Drawing.Point(283, 424);
            this.txt_XMLLeadExisting.Name = "txt_XMLLeadExisting";
            this.txt_XMLLeadExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLLeadExisting.TabIndex = 241;
            this.txt_XMLLeadExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMLLeadNew
            // 
            this.txt_XMLLeadNew.Cue = "XML Lead New";
            this.txt_XMLLeadNew.Enabled = false;
            this.txt_XMLLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLLeadNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLLeadNew.Location = new System.Drawing.Point(144, 424);
            this.txt_XMLLeadNew.Name = "txt_XMLLeadNew";
            this.txt_XMLLeadNew.Size = new System.Drawing.Size(116, 20);
            this.txt_XMLLeadNew.TabIndex = 240;
            this.txt_XMLLeadNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AudioExisting
            // 
            this.txt_AudioExisting.Cue = "Audio Existing";
            this.txt_AudioExisting.Enabled = false;
            this.txt_AudioExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioExisting.Location = new System.Drawing.Point(283, 360);
            this.txt_AudioExisting.Name = "txt_AudioExisting";
            this.txt_AudioExisting.Size = new System.Drawing.Size(59, 20);
            this.txt_AudioExisting.TabIndex = 239;
            this.txt_AudioExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AudioNew
            // 
            this.txt_AudioNew.Cue = "Audio New";
            this.txt_AudioNew.Enabled = false;
            this.txt_AudioNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioNew.Location = new System.Drawing.Point(201, 360);
            this.txt_AudioNew.Name = "txt_AudioNew";
            this.txt_AudioNew.Size = new System.Drawing.Size(59, 20);
            this.txt_AudioNew.TabIndex = 238;
            this.txt_AudioNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AvailTracksExisting
            // 
            this.txt_AvailTracksExisting.Cue = "Available Tracks Existing";
            this.txt_AvailTracksExisting.Enabled = false;
            this.txt_AvailTracksExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AvailTracksExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_AvailTracksExisting.Location = new System.Drawing.Point(283, 334);
            this.txt_AvailTracksExisting.Name = "txt_AvailTracksExisting";
            this.txt_AvailTracksExisting.Size = new System.Drawing.Size(45, 20);
            this.txt_AvailTracksExisting.TabIndex = 237;
            this.txt_AvailTracksExisting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AvailTracksNew
            // 
            this.txt_AvailTracksNew.Cue = "Available Tracks New";
            this.txt_AvailTracksNew.Enabled = false;
            this.txt_AvailTracksNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AvailTracksNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AvailTracksNew.Location = new System.Drawing.Point(217, 334);
            this.txt_AvailTracksNew.Name = "txt_AvailTracksNew";
            this.txt_AvailTracksNew.Size = new System.Drawing.Size(43, 20);
            this.txt_AvailTracksNew.TabIndex = 236;
            this.txt_AvailTracksNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DDExisting
            // 
            this.txt_DDExisting.Cue = "DD Existing";
            this.txt_DDExisting.Enabled = false;
            this.txt_DDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DDExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_DDExisting.Location = new System.Drawing.Point(283, 308);
            this.txt_DDExisting.Name = "txt_DDExisting";
            this.txt_DDExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_DDExisting.TabIndex = 235;
            // 
            // txt_DDNew
            // 
            this.txt_DDNew.Cue = "DD New";
            this.txt_DDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DDNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_DDNew.Location = new System.Drawing.Point(144, 308);
            this.txt_DDNew.Name = "txt_DDNew";
            this.txt_DDNew.Size = new System.Drawing.Size(116, 20);
            this.txt_DDNew.TabIndex = 234;
            this.txt_DDNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DLCIDExisting
            // 
            this.txt_DLCIDExisting.Cue = "DLCID Existing";
            this.txt_DLCIDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLCIDExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLCIDExisting.Location = new System.Drawing.Point(283, 282);
            this.txt_DLCIDExisting.Name = "txt_DLCIDExisting";
            this.txt_DLCIDExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_DLCIDExisting.TabIndex = 233;
            // 
            // txt_DLCIDNew
            // 
            this.txt_DLCIDNew.Cue = "DLCID New";
            this.txt_DLCIDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLCIDNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLCIDNew.Location = new System.Drawing.Point(38, 282);
            this.txt_DLCIDNew.Name = "txt_DLCIDNew";
            this.txt_DLCIDNew.Size = new System.Drawing.Size(222, 20);
            this.txt_DLCIDNew.TabIndex = 232;
            this.txt_DLCIDNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_TuningExisting
            // 
            this.txt_TuningExisting.Cue = "Tunig Existing";
            this.txt_TuningExisting.Enabled = false;
            this.txt_TuningExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TuningExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_TuningExisting.Location = new System.Drawing.Point(283, 256);
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
            this.txt_TuningNew.Location = new System.Drawing.Point(144, 256);
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
            this.txt_AuthorExisting.Location = new System.Drawing.Point(283, 206);
            this.txt_AuthorExisting.Name = "txt_AuthorExisting";
            this.txt_AuthorExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_AuthorExisting.TabIndex = 229;
            // 
            // txt_AuthorNew
            // 
            this.txt_AuthorNew.Cue = "Author New";
            this.txt_AuthorNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AuthorNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_AuthorNew.Location = new System.Drawing.Point(144, 206);
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
            this.txt_IsOriginalExisting.Location = new System.Drawing.Point(283, 157);
            this.txt_IsOriginalExisting.Name = "txt_IsOriginalExisting";
            this.txt_IsOriginalExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_IsOriginalExisting.TabIndex = 227;
            // 
            // txt_IsOriginalNew
            // 
            this.txt_IsOriginalNew.Cue = "Is Original New";
            this.txt_IsOriginalNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_IsOriginalNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_IsOriginalNew.Location = new System.Drawing.Point(144, 157);
            this.txt_IsOriginalNew.Name = "txt_IsOriginalNew";
            this.txt_IsOriginalNew.Size = new System.Drawing.Size(116, 20);
            this.txt_IsOriginalNew.TabIndex = 226;
            this.txt_IsOriginalNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ToolkitExisting
            // 
            this.txt_ToolkitExisting.Cue = "Toolkit Existing";
            this.txt_ToolkitExisting.Enabled = false;
            this.txt_ToolkitExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ToolkitExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_ToolkitExisting.Location = new System.Drawing.Point(283, 181);
            this.txt_ToolkitExisting.Name = "txt_ToolkitExisting";
            this.txt_ToolkitExisting.Size = new System.Drawing.Size(116, 20);
            this.txt_ToolkitExisting.TabIndex = 225;
            // 
            // txt_ToolkitNew
            // 
            this.txt_ToolkitNew.Cue = "Toolkit New";
            this.txt_ToolkitNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ToolkitNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_ToolkitNew.Location = new System.Drawing.Point(144, 181);
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
            this.txt_TitleSortExisting.Location = new System.Drawing.Point(283, 81);
            this.txt_TitleSortExisting.Name = "txt_TitleSortExisting";
            this.txt_TitleSortExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleSortExisting.TabIndex = 223;
            // 
            // txt_TitleSortNew
            // 
            this.txt_TitleSortNew.Cue = "Title Sort New";
            this.txt_TitleSortNew.Enabled = false;
            this.txt_TitleSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleSortNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleSortNew.Location = new System.Drawing.Point(38, 81);
            this.txt_TitleSortNew.Name = "txt_TitleSortNew";
            this.txt_TitleSortNew.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleSortNew.TabIndex = 222;
            this.txt_TitleSortNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ArtistSortExisting
            // 
            this.txt_ArtistSortExisting.Cue = "Artist Sort Existing";
            this.txt_ArtistSortExisting.Enabled = false;
            this.txt_ArtistSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSortExisting.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSortExisting.Location = new System.Drawing.Point(283, 107);
            this.txt_ArtistSortExisting.Name = "txt_ArtistSortExisting";
            this.txt_ArtistSortExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_ArtistSortExisting.TabIndex = 221;
            // 
            // txt_ArtistSortNew
            // 
            this.txt_ArtistSortNew.Cue = "Artist Sort New";
            this.txt_ArtistSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArtistSortNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArtistSortNew.Location = new System.Drawing.Point(38, 107);
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
            this.txt_TitleExisting.Location = new System.Drawing.Point(283, 55);
            this.txt_TitleExisting.Name = "txt_TitleExisting";
            this.txt_TitleExisting.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleExisting.TabIndex = 219;
            // 
            // txt_TitleNew
            // 
            this.txt_TitleNew.Cue = "Title New";
            this.txt_TitleNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TitleNew.ForeColor = System.Drawing.Color.Gray;
            this.txt_TitleNew.Location = new System.Drawing.Point(38, 55);
            this.txt_TitleNew.Name = "txt_TitleNew";
            this.txt_TitleNew.Size = new System.Drawing.Size(222, 20);
            this.txt_TitleNew.TabIndex = 217;
            this.txt_TitleNew.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(565, 144);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 312;
            this.checkBox1.Text = "Original";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(239, 409);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 13);
            this.label17.TabIndex = 313;
            this.label17.Text = "MM-DD-YYYY";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(81, 160);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 13);
            this.label18.TabIndex = 314;
            this.label18.Text = "Is Original";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(65, 311);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(69, 13);
            this.label19.TabIndex = 315;
            this.label19.Text = "DD Available";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(87, 233);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(42, 13);
            this.label20.TabIndex = 316;
            this.label20.Text = "Version";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(86, 209);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 13);
            this.label21.TabIndex = 317;
            this.label21.Text = "Author";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(82, 184);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(39, 13);
            this.label22.TabIndex = 318;
            this.label22.Text = "Toolkit";
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = "Album";
            this.cueTextBox1.Enabled = false;
            this.cueTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cueTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.cueTextBox1.Location = new System.Drawing.Point(532, 32);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(222, 20);
            this.cueTextBox1.TabIndex = 319;
            this.cueTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(87, 259);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 13);
            this.label23.TabIndex = 320;
            this.label23.Text = "Tuning";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(477, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 35);
            this.button2.TabIndex = 321;
            this.button2.Text = "Update Existing";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label24.Location = new System.Drawing.Point(138, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(122, 15);
            this.label24.TabIndex = 322;
            this.label24.Text = "Importing at the moment";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Location = new System.Drawing.Point(283, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 15);
            this.label25.TabIndex = 323;
            this.label25.Text = "Already Imported";
            // 
            // DuplicatesManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 676);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cueTextBox1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Reference);
            this.Controls.Add(this.lbl_JSONVocal);
            this.Controls.Add(this.lbl_JSONRhythm);
            this.Controls.Add(this.lbl_JSONCombo);
            this.Controls.Add(this.lbl_JSONBass);
            this.Controls.Add(this.lbl_JSONLead);
            this.Controls.Add(this.lbl_XMLVocal);
            this.Controls.Add(this.lbl_XMLRhythm);
            this.Controls.Add(this.lbl_XMLCombo);
            this.Controls.Add(this.lbl_XMLBass);
            this.Controls.Add(this.lbl_XMLLead);
            this.Controls.Add(this.lbl_Preview);
            this.Controls.Add(this.lbl_Audio);
            this.Controls.Add(this.lbl_AvailableTracks);
            this.Controls.Add(this.lbl_DD);
            this.Controls.Add(this.lbl_DLCID);
            this.Controls.Add(this.lbl_Tuning);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.lbl_Author);
            this.Controls.Add(this.lbl_Toolkit);
            this.Controls.Add(this.lbl_IsOriginal);
            this.Controls.Add(this.lbl_FileName);
            this.Controls.Add(this.lbl_ArtistSort);
            this.Controls.Add(this.lbl_TitleSort);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txt_VersionExisting);
            this.Controls.Add(this.txt_VersionNew);
            this.Controls.Add(this.txt_FileNameExisting);
            this.Controls.Add(this.txt_FileNameNew);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.txt_Comment);
            this.Controls.Add(this.txt_Description);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_JSONVocalExisting);
            this.Controls.Add(this.txt_JSONVocalNew);
            this.Controls.Add(this.txt_JSONRhythmExisting);
            this.Controls.Add(this.txt_JSONRhythmNew);
            this.Controls.Add(this.lbl_AlbumArt);
            this.Controls.Add(this.txt_JSONComboExisting);
            this.Controls.Add(this.txt_JSONComboNew);
            this.Controls.Add(this.txt_JSONBassExisting);
            this.Controls.Add(this.txt_JSONBassNew);
            this.Controls.Add(this.txt_JSONLeadExisting);
            this.Controls.Add(this.txt_JSONLeadNew);
            this.Controls.Add(this.txt_XMLVocalExisting);
            this.Controls.Add(this.txt_XMLVocalNew);
            this.Controls.Add(this.txt_XMLRhythmExisting);
            this.Controls.Add(this.txt_XMLRhythmNew);
            this.Controls.Add(this.txt_XMLComboExisting);
            this.Controls.Add(this.txt_XMLComboNew);
            this.Controls.Add(this.txt_XMLBassExisting);
            this.Controls.Add(this.txt_XMLBassNew);
            this.Controls.Add(this.txt_PreviewExisting);
            this.Controls.Add(this.txt_PreviewNew);
            this.Controls.Add(this.txt_XMLLeadExisting);
            this.Controls.Add(this.txt_XMLLeadNew);
            this.Controls.Add(this.txt_AudioExisting);
            this.Controls.Add(this.txt_AudioNew);
            this.Controls.Add(this.txt_AvailTracksExisting);
            this.Controls.Add(this.txt_AvailTracksNew);
            this.Controls.Add(this.txt_DDExisting);
            this.Controls.Add(this.txt_DDNew);
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
            this.Controls.Add(this.chbx_Broken);
            this.Controls.Add(this.btn_Alternate);
            this.Controls.Add(this.btn_Ignore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Name = "DuplicatesManagement";
            this.Text = "Duplicates Management";
            this.Load += new System.EventHandler(this.DuplicatesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPathExisting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label16;
        private CueTextBox txt_VersionExisting;
        private CueTextBox txt_VersionNew;
        private CueTextBox txt_FileNameExisting;
        private CueTextBox txt_FileNameNew;
        private CueTextBox txt_Artist;
        private RichTextBox txt_Comment;
        private RichTextBox txt_Description;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private CueTextBox txt_JSONVocalExisting;
        private CueTextBox txt_JSONVocalNew;
        private CueTextBox txt_JSONRhythmExisting;
        private CueTextBox txt_JSONRhythmNew;
        private Label lbl_AlbumArt;
        private CueTextBox txt_JSONComboExisting;
        private CueTextBox txt_JSONComboNew;
        private CueTextBox txt_JSONBassExisting;
        private CueTextBox txt_JSONBassNew;
        private CueTextBox txt_JSONLeadExisting;
        private CueTextBox txt_JSONLeadNew;
        private CueTextBox txt_XMLVocalExisting;
        private CueTextBox txt_XMLVocalNew;
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
        private CheckBox chbx_Broken;
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
        private Label lbl_XMLVocal;
        private Label lbl_JSONLead;
        private Label lbl_JSONBass;
        private Label lbl_JSONCombo;
        private Label lbl_JSONRhythm;
        private Label lbl_JSONVocal;
        private Label lbl_Reference;
        private Button button1;
        private Panel panel1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;
        private Label label2;
        private CheckBox checkBox1;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private CueTextBox cueTextBox1;
        private Label label23;
        private Button button2;
        private Label label24;
        private Label label25;
    }
}