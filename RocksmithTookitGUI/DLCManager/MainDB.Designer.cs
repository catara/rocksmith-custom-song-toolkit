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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.chbx_Avail_Old = new System.Windows.Forms.CheckBox();
            this.chbx_Avail_Duplicate = new System.Windows.Forms.CheckBox();
            this.button15 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbx_Format = new System.Windows.Forms.ComboBox();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.chbx_Cover = new System.Windows.Forms.CheckBox();
            this.button14 = new System.Windows.Forms.Button();
            this.chbx_Bonus = new System.Windows.Forms.CheckBox();
            this.chbx_Save_All = new System.Windows.Forms.CheckBox();
            this.chbx_BassDD = new System.Windows.Forms.CheckBox();
            this.button13 = new System.Windows.Forms.Button();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btn_Conv_And_Transfer = new System.Windows.Forms.Button();
            this.chbx_Beta = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.chbx_Rhythm = new System.Windows.Forms.CheckBox();
            this.chbx_Combo = new System.Windows.Forms.CheckBox();
            this.chbx_Bass = new System.Windows.Forms.CheckBox();
            this.chbx_Lead = new System.Windows.Forms.CheckBox();
            this.chbx_Selected = new System.Windows.Forms.CheckBox();
            this.button7 = new System.Windows.Forms.Button();
            this.chbx_Preview = new System.Windows.Forms.CheckBox();
            this.btn_SearchReset = new System.Windows.Forms.Button();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.chbx_Sections = new System.Windows.Forms.CheckBox();
            this.chbx_Alternate = new System.Windows.Forms.CheckBox();
            this.chbx_DD = new System.Windows.Forms.CheckBox();
            this.chbx_Original = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Tones = new System.Windows.Forms.Button();
            this.btn_Arrangements = new System.Windows.Forms.Button();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Close = new System.Windows.Forms.Button();
            this.chbx_Has_Been_Corrected = new System.Windows.Forms.CheckBox();
            this.txt_AlbumArtPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_Year = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Group = new RocksmithToolkitGUI.CueTextBox();
            this.txt_BassPicking = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.cueTextBox5 = new RocksmithToolkitGUI.CueTextBox();
            this.cueTextBox4 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AverageTempo = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Preview_Volume = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Volume = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Alt_No = new RocksmithToolkitGUI.CueTextBox();
            this.txt_APP_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_DLC_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Tuning = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Version = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Author = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Rating = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Track_No = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Title_Sort = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Title = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist_Sort = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist = new RocksmithToolkitGUI.CueTextBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.chbx_Has_Been_Corrected);
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.lbl_NoRec);
            this.Panel1.Controls.Add(this.chbx_Avail_Old);
            this.Panel1.Controls.Add(this.chbx_Avail_Duplicate);
            this.Panel1.Controls.Add(this.button15);
            this.Panel1.Controls.Add(this.comboBox1);
            this.Panel1.Controls.Add(this.cbx_Format);
            this.Panel1.Controls.Add(this.txt_AlbumArtPath);
            this.Panel1.Controls.Add(this.btn_ChangeCover);
            this.Panel1.Controls.Add(this.chbx_Cover);
            this.Panel1.Controls.Add(this.txt_Artist_ShortName);
            this.Panel1.Controls.Add(this.txt_Album_ShortName);
            this.Panel1.Controls.Add(this.button14);
            this.Panel1.Controls.Add(this.chbx_Bonus);
            this.Panel1.Controls.Add(this.chbx_Save_All);
            this.Panel1.Controls.Add(this.chbx_BassDD);
            this.Panel1.Controls.Add(this.txt_Album_Year);
            this.Panel1.Controls.Add(this.txt_Group);
            this.Panel1.Controls.Add(this.txt_BassPicking);
            this.Panel1.Controls.Add(this.txt_ID);
            this.Panel1.Controls.Add(this.button13);
            this.Panel1.Controls.Add(this.cueTextBox5);
            this.Panel1.Controls.Add(this.cueTextBox4);
            this.Panel1.Controls.Add(this.picbx_AlbumArtPath);
            this.Panel1.Controls.Add(this.txt_AverageTempo);
            this.Panel1.Controls.Add(this.txt_Preview_Volume);
            this.Panel1.Controls.Add(this.button12);
            this.Panel1.Controls.Add(this.button11);
            this.Panel1.Controls.Add(this.txt_Volume);
            this.Panel1.Controls.Add(this.button10);
            this.Panel1.Controls.Add(this.btn_Conv_And_Transfer);
            this.Panel1.Controls.Add(this.txt_Alt_No);
            this.Panel1.Controls.Add(this.chbx_Beta);
            this.Panel1.Controls.Add(this.btn_Save);
            this.Panel1.Controls.Add(this.txt_APP_ID);
            this.Panel1.Controls.Add(this.txt_DLC_ID);
            this.Panel1.Controls.Add(this.txt_Tuning);
            this.Panel1.Controls.Add(this.chbx_Rhythm);
            this.Panel1.Controls.Add(this.chbx_Combo);
            this.Panel1.Controls.Add(this.chbx_Bass);
            this.Panel1.Controls.Add(this.chbx_Lead);
            this.Panel1.Controls.Add(this.txt_Version);
            this.Panel1.Controls.Add(this.txt_Author);
            this.Panel1.Controls.Add(this.txt_Rating);
            this.Panel1.Controls.Add(this.txt_Track_No);
            this.Panel1.Controls.Add(this.txt_Album);
            this.Panel1.Controls.Add(this.txt_Title_Sort);
            this.Panel1.Controls.Add(this.txt_Title);
            this.Panel1.Controls.Add(this.txt_Artist_Sort);
            this.Panel1.Controls.Add(this.txt_Artist);
            this.Panel1.Controls.Add(this.chbx_Selected);
            this.Panel1.Controls.Add(this.button7);
            this.Panel1.Controls.Add(this.chbx_Preview);
            this.Panel1.Controls.Add(this.btn_SearchReset);
            this.Panel1.Controls.Add(this.txt_Description);
            this.Panel1.Controls.Add(this.chbx_Broken);
            this.Panel1.Controls.Add(this.btn_Search);
            this.Panel1.Controls.Add(this.chbx_Sections);
            this.Panel1.Controls.Add(this.chbx_Alternate);
            this.Panel1.Controls.Add(this.chbx_DD);
            this.Panel1.Controls.Add(this.chbx_Original);
            this.Panel1.Controls.Add(this.button4);
            this.Panel1.Controls.Add(this.button3);
            this.Panel1.Controls.Add(this.button2);
            this.Panel1.Controls.Add(this.button1);
            this.Panel1.Controls.Add(this.btn_Tones);
            this.Panel1.Controls.Add(this.btn_Arrangements);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 359);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1106, 202);
            this.Panel1.TabIndex = 3;
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.AutoSize = true;
            this.lbl_NoRec.Location = new System.Drawing.Point(12, 2);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(59, 13);
            this.lbl_NoRec.TabIndex = 113;
            this.lbl_NoRec.Text = "of Records";
            // 
            // chbx_Avail_Old
            // 
            this.chbx_Avail_Old.AutoSize = true;
            this.chbx_Avail_Old.Location = new System.Drawing.Point(857, 181);
            this.chbx_Avail_Old.Name = "chbx_Avail_Old";
            this.chbx_Avail_Old.Size = new System.Drawing.Size(71, 17);
            this.chbx_Avail_Old.TabIndex = 112;
            this.chbx_Avail_Old.Text = "Old Avail.";
            this.chbx_Avail_Old.UseVisualStyleBackColor = true;
            // 
            // chbx_Avail_Duplicate
            // 
            this.chbx_Avail_Duplicate.AutoSize = true;
            this.chbx_Avail_Duplicate.Location = new System.Drawing.Point(931, 180);
            this.chbx_Avail_Duplicate.Name = "chbx_Avail_Duplicate";
            this.chbx_Avail_Duplicate.Size = new System.Drawing.Size(100, 17);
            this.chbx_Avail_Duplicate.TabIndex = 111;
            this.chbx_Avail_Duplicate.Text = "Duplicate Avail.";
            this.chbx_Avail_Duplicate.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(12, 155);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(120, 35);
            this.button15.TabIndex = 110;
            this.button15.Text = "Standarization";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBox1.Location = new System.Drawing.Point(970, 84);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(46, 21);
            this.comboBox1.TabIndex = 109;
            // 
            // cbx_Format
            // 
            this.cbx_Format.Enabled = false;
            this.cbx_Format.FormattingEnabled = true;
            this.cbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.cbx_Format.Location = new System.Drawing.Point(970, 57);
            this.cbx_Format.Name = "cbx_Format";
            this.cbx_Format.Size = new System.Drawing.Size(46, 21);
            this.cbx_Format.TabIndex = 108;
            this.cbx_Format.Text = "PS3";
            this.cbx_Format.SelectedValueChanged += new System.EventHandler(this.cbx_Format_SelectedValueChanged);
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Enabled = false;
            this.btn_ChangeCover.Location = new System.Drawing.Point(318, 143);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(84, 26);
            this.btn_ChangeCover.TabIndex = 106;
            this.btn_ChangeCover.Text = "Change cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            this.btn_ChangeCover.Click += new System.EventHandler(this.btn_ChangeCover_Click);
            // 
            // chbx_Cover
            // 
            this.chbx_Cover.AutoSize = true;
            this.chbx_Cover.Enabled = false;
            this.chbx_Cover.Location = new System.Drawing.Point(669, 71);
            this.chbx_Cover.Name = "chbx_Cover";
            this.chbx_Cover.Size = new System.Drawing.Size(54, 17);
            this.chbx_Cover.TabIndex = 105;
            this.chbx_Cover.Text = "Cover";
            this.chbx_Cover.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Enabled = false;
            this.button14.Location = new System.Drawing.Point(12, 117);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(120, 35);
            this.button14.TabIndex = 102;
            this.button14.Text = "Select All";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // chbx_Bonus
            // 
            this.chbx_Bonus.AutoSize = true;
            this.chbx_Bonus.Enabled = false;
            this.chbx_Bonus.Location = new System.Drawing.Point(669, 57);
            this.chbx_Bonus.Name = "chbx_Bonus";
            this.chbx_Bonus.Size = new System.Drawing.Size(56, 17);
            this.chbx_Bonus.TabIndex = 101;
            this.chbx_Bonus.Text = "Bonus";
            this.chbx_Bonus.UseVisualStyleBackColor = true;
            // 
            // chbx_Save_All
            // 
            this.chbx_Save_All.AutoSize = true;
            this.chbx_Save_All.Enabled = false;
            this.chbx_Save_All.Location = new System.Drawing.Point(874, 111);
            this.chbx_Save_All.Name = "chbx_Save_All";
            this.chbx_Save_All.Size = new System.Drawing.Size(37, 17);
            this.chbx_Save_All.TabIndex = 100;
            this.chbx_Save_All.Text = "All";
            this.chbx_Save_All.UseVisualStyleBackColor = true;
            // 
            // chbx_BassDD
            // 
            this.chbx_BassDD.AutoSize = true;
            this.chbx_BassDD.Location = new System.Drawing.Point(857, 163);
            this.chbx_BassDD.Name = "chbx_BassDD";
            this.chbx_BassDD.Size = new System.Drawing.Size(68, 17);
            this.chbx_BassDD.TabIndex = 99;
            this.chbx_BassDD.Text = "Bass DD";
            this.chbx_BassDD.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Enabled = false;
            this.button13.Location = new System.Drawing.Point(707, 137);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(107, 26);
            this.button13.TabIndex = 94;
            this.button13.Text = "Add Preview";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(207, 117);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(105, 81);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 91;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(622, 111);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(68, 26);
            this.button12.TabIndex = 88;
            this.button12.Text = "Preview";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(511, 111);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(68, 26);
            this.button11.TabIndex = 87;
            this.button11.Text = "Audio";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(970, 133);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(133, 26);
            this.button10.TabIndex = 85;
            this.button10.Text = "Remove BassDD";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // btn_Conv_And_Transfer
            // 
            this.btn_Conv_And_Transfer.Enabled = false;
            this.btn_Conv_And_Transfer.Location = new System.Drawing.Point(970, 106);
            this.btn_Conv_And_Transfer.Name = "btn_Conv_And_Transfer";
            this.btn_Conv_And_Transfer.Size = new System.Drawing.Size(133, 26);
            this.btn_Conv_And_Transfer.TabIndex = 84;
            this.btn_Conv_And_Transfer.Text = "Conv2Ps3 & Copy/FTP";
            this.btn_Conv_And_Transfer.UseVisualStyleBackColor = true;
            // 
            // chbx_Beta
            // 
            this.chbx_Beta.AutoSize = true;
            this.chbx_Beta.Location = new System.Drawing.Point(738, 57);
            this.chbx_Beta.Name = "chbx_Beta";
            this.chbx_Beta.Size = new System.Drawing.Size(48, 17);
            this.chbx_Beta.TabIndex = 82;
            this.chbx_Beta.Text = "Beta";
            this.chbx_Beta.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(874, 80);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(84, 26);
            this.btn_Save.TabIndex = 81;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // chbx_Rhythm
            // 
            this.chbx_Rhythm.AutoSize = true;
            this.chbx_Rhythm.Enabled = false;
            this.chbx_Rhythm.Location = new System.Drawing.Point(606, 74);
            this.chbx_Rhythm.Name = "chbx_Rhythm";
            this.chbx_Rhythm.Size = new System.Drawing.Size(62, 17);
            this.chbx_Rhythm.TabIndex = 77;
            this.chbx_Rhythm.Text = "Rhythm";
            this.chbx_Rhythm.UseVisualStyleBackColor = true;
            // 
            // chbx_Combo
            // 
            this.chbx_Combo.AutoSize = true;
            this.chbx_Combo.Enabled = false;
            this.chbx_Combo.Location = new System.Drawing.Point(553, 74);
            this.chbx_Combo.Name = "chbx_Combo";
            this.chbx_Combo.Size = new System.Drawing.Size(59, 17);
            this.chbx_Combo.TabIndex = 76;
            this.chbx_Combo.Text = "Combo";
            this.chbx_Combo.UseVisualStyleBackColor = true;
            // 
            // chbx_Bass
            // 
            this.chbx_Bass.AutoSize = true;
            this.chbx_Bass.Enabled = false;
            this.chbx_Bass.Location = new System.Drawing.Point(499, 91);
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
            this.chbx_Lead.Location = new System.Drawing.Point(499, 74);
            this.chbx_Lead.Name = "chbx_Lead";
            this.chbx_Lead.Size = new System.Drawing.Size(50, 17);
            this.chbx_Lead.TabIndex = 74;
            this.chbx_Lead.Text = "Lead";
            this.chbx_Lead.UseVisualStyleBackColor = true;
            // 
            // chbx_Selected
            // 
            this.chbx_Selected.AutoSize = true;
            this.chbx_Selected.Location = new System.Drawing.Point(669, 25);
            this.chbx_Selected.Name = "chbx_Selected";
            this.chbx_Selected.Size = new System.Drawing.Size(68, 17);
            this.chbx_Selected.TabIndex = 64;
            this.chbx_Selected.Text = "Selected";
            this.chbx_Selected.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(970, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(133, 26);
            this.button7.TabIndex = 58;
            this.button7.Text = "Duplicate";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // chbx_Preview
            // 
            this.chbx_Preview.AutoSize = true;
            this.chbx_Preview.Enabled = false;
            this.chbx_Preview.Location = new System.Drawing.Point(738, 40);
            this.chbx_Preview.Name = "chbx_Preview";
            this.chbx_Preview.Size = new System.Drawing.Size(64, 17);
            this.chbx_Preview.TabIndex = 57;
            this.chbx_Preview.Text = "Preview";
            this.chbx_Preview.UseVisualStyleBackColor = true;
            // 
            // btn_SearchReset
            // 
            this.btn_SearchReset.Location = new System.Drawing.Point(151, 80);
            this.btn_SearchReset.Name = "btn_SearchReset";
            this.btn_SearchReset.Size = new System.Drawing.Size(51, 32);
            this.btn_SearchReset.TabIndex = 56;
            this.btn_SearchReset.Text = "Reset";
            this.btn_SearchReset.UseVisualStyleBackColor = true;
            this.btn_SearchReset.Click += new System.EventHandler(this.button6_Click);
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(802, 9);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(156, 69);
            this.txt_Description.TabIndex = 54;
            this.txt_Description.Text = "Description";
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Location = new System.Drawing.Point(738, 25);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 53;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(151, 18);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(51, 60);
            this.btn_Search.TabIndex = 51;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // chbx_Sections
            // 
            this.chbx_Sections.AutoSize = true;
            this.chbx_Sections.Enabled = false;
            this.chbx_Sections.Location = new System.Drawing.Point(669, 41);
            this.chbx_Sections.Name = "chbx_Sections";
            this.chbx_Sections.Size = new System.Drawing.Size(67, 17);
            this.chbx_Sections.TabIndex = 48;
            this.chbx_Sections.Text = "Sections";
            this.chbx_Sections.UseVisualStyleBackColor = true;
            // 
            // chbx_Alternate
            // 
            this.chbx_Alternate.AutoSize = true;
            this.chbx_Alternate.Location = new System.Drawing.Point(707, 5);
            this.chbx_Alternate.Name = "chbx_Alternate";
            this.chbx_Alternate.Size = new System.Drawing.Size(68, 17);
            this.chbx_Alternate.TabIndex = 46;
            this.chbx_Alternate.Text = "Alternate";
            this.chbx_Alternate.UseVisualStyleBackColor = true;
            // 
            // chbx_DD
            // 
            this.chbx_DD.AutoSize = true;
            this.chbx_DD.Enabled = false;
            this.chbx_DD.Location = new System.Drawing.Point(553, 54);
            this.chbx_DD.Name = "chbx_DD";
            this.chbx_DD.Size = new System.Drawing.Size(42, 17);
            this.chbx_DD.TabIndex = 45;
            this.chbx_DD.Text = "DD";
            this.chbx_DD.UseVisualStyleBackColor = true;
            // 
            // chbx_Original
            // 
            this.chbx_Original.AutoSize = true;
            this.chbx_Original.Location = new System.Drawing.Point(606, 53);
            this.chbx_Original.Name = "chbx_Original";
            this.chbx_Original.Size = new System.Drawing.Size(61, 17);
            this.chbx_Original.TabIndex = 44;
            this.chbx_Original.Text = "Original";
            this.chbx_Original.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(970, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 26);
            this.button4.TabIndex = 40;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(1022, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 26);
            this.button3.TabIndex = 39;
            this.button3.Text = "Package";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(1022, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 26);
            this.button2.TabIndex = 38;
            this.button2.Text = "Add DD";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 37;
            this.button1.Text = "Open DB in M$ Access";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Tones
            // 
            this.btn_Tones.Location = new System.Drawing.Point(10, 47);
            this.btn_Tones.Name = "btn_Tones";
            this.btn_Tones.Size = new System.Drawing.Size(122, 23);
            this.btn_Tones.TabIndex = 36;
            this.btn_Tones.Text = "Open Tones";
            this.btn_Tones.UseVisualStyleBackColor = true;
            this.btn_Tones.Click += new System.EventHandler(this.btn_Tones_Click);
            // 
            // btn_Arrangements
            // 
            this.btn_Arrangements.Location = new System.Drawing.Point(10, 18);
            this.btn_Arrangements.Name = "btn_Arrangements";
            this.btn_Arrangements.Size = new System.Drawing.Size(122, 23);
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
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(1106, 353);
            this.DataGridView1.TabIndex = 2;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1031, 180);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // chbx_Has_Been_Corrected
            // 
            this.chbx_Has_Been_Corrected.AutoSize = true;
            this.chbx_Has_Been_Corrected.Location = new System.Drawing.Point(931, 163);
            this.chbx_Has_Been_Corrected.Name = "chbx_Has_Been_Corrected";
            this.chbx_Has_Been_Corrected.Size = new System.Drawing.Size(122, 17);
            this.chbx_Has_Been_Corrected.TabIndex = 274;
            this.chbx_Has_Been_Corrected.Text = "Has Been Corrected";
            this.chbx_Has_Been_Corrected.UseVisualStyleBackColor = true;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(318, 170);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(496, 20);
            this.txt_AlbumArtPath.TabIndex = 107;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Artist_ShortName
            // 
            this.txt_Artist_ShortName.Cue = "Short";
            this.txt_Artist_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_ShortName.Location = new System.Drawing.Point(435, 3);
            this.txt_Artist_ShortName.Name = "txt_Artist_ShortName";
            this.txt_Artist_ShortName.Size = new System.Drawing.Size(58, 20);
            this.txt_Artist_ShortName.TabIndex = 104;
            // 
            // txt_Album_ShortName
            // 
            this.txt_Album_ShortName.Cue = "Short";
            this.txt_Album_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_ShortName.Location = new System.Drawing.Point(368, 92);
            this.txt_Album_ShortName.Name = "txt_Album_ShortName";
            this.txt_Album_ShortName.Size = new System.Drawing.Size(58, 20);
            this.txt_Album_ShortName.TabIndex = 103;
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
            // txt_Group
            // 
            this.txt_Group.Cue = "<Group>";
            this.txt_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Group.ForeColor = System.Drawing.Color.Gray;
            this.txt_Group.Location = new System.Drawing.Point(511, 141);
            this.txt_Group.Name = "txt_Group";
            this.txt_Group.Size = new System.Drawing.Size(107, 20);
            this.txt_Group.TabIndex = 97;
            // 
            // txt_BassPicking
            // 
            this.txt_BassPicking.Cue = "Bass Picking";
            this.txt_BassPicking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_BassPicking.ForeColor = System.Drawing.Color.Gray;
            this.txt_BassPicking.Location = new System.Drawing.Point(857, 137);
            this.txt_BassPicking.Name = "txt_BassPicking";
            this.txt_BassPicking.Size = new System.Drawing.Size(107, 20);
            this.txt_BassPicking.TabIndex = 96;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(318, 117);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 95;
            // 
            // cueTextBox5
            // 
            this.cueTextBox5.Cue = "Vol.";
            this.cueTextBox5.Enabled = false;
            this.cueTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cueTextBox5.ForeColor = System.Drawing.Color.Gray;
            this.cueTextBox5.Location = new System.Drawing.Point(674, 143);
            this.cueTextBox5.Name = "cueTextBox5";
            this.cueTextBox5.Size = new System.Drawing.Size(27, 20);
            this.cueTextBox5.TabIndex = 93;
            // 
            // cueTextBox4
            // 
            this.cueTextBox4.Cue = "Vol.";
            this.cueTextBox4.Enabled = false;
            this.cueTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cueTextBox4.ForeColor = System.Drawing.Color.Gray;
            this.cueTextBox4.Location = new System.Drawing.Point(641, 143);
            this.cueTextBox4.Name = "cueTextBox4";
            this.cueTextBox4.Size = new System.Drawing.Size(27, 20);
            this.cueTextBox4.TabIndex = 92;
            // 
            // txt_AverageTempo
            // 
            this.txt_AverageTempo.Cue = "Avg. Tempo";
            this.txt_AverageTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AverageTempo.ForeColor = System.Drawing.Color.Gray;
            this.txt_AverageTempo.Location = new System.Drawing.Point(725, 115);
            this.txt_AverageTempo.Name = "txt_AverageTempo";
            this.txt_AverageTempo.Size = new System.Drawing.Size(89, 20);
            this.txt_AverageTempo.TabIndex = 90;
            // 
            // txt_Preview_Volume
            // 
            this.txt_Preview_Volume.Cue = "Vol.";
            this.txt_Preview_Volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Preview_Volume.ForeColor = System.Drawing.Color.Gray;
            this.txt_Preview_Volume.Location = new System.Drawing.Point(692, 115);
            this.txt_Preview_Volume.Name = "txt_Preview_Volume";
            this.txt_Preview_Volume.Size = new System.Drawing.Size(27, 20);
            this.txt_Preview_Volume.TabIndex = 89;
            // 
            // txt_Volume
            // 
            this.txt_Volume.Cue = "Vol.";
            this.txt_Volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Volume.ForeColor = System.Drawing.Color.Gray;
            this.txt_Volume.Location = new System.Drawing.Point(581, 115);
            this.txt_Volume.Name = "txt_Volume";
            this.txt_Volume.Size = new System.Drawing.Size(27, 20);
            this.txt_Volume.TabIndex = 86;
            // 
            // txt_Alt_No
            // 
            this.txt_Alt_No.Cue = "No.";
            this.txt_Alt_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Alt_No.ForeColor = System.Drawing.Color.Gray;
            this.txt_Alt_No.Location = new System.Drawing.Point(769, 2);
            this.txt_Alt_No.Name = "txt_Alt_No";
            this.txt_Alt_No.Size = new System.Drawing.Size(27, 20);
            this.txt_Alt_No.TabIndex = 83;
            // 
            // txt_APP_ID
            // 
            this.txt_APP_ID.Cue = "App ID";
            this.txt_APP_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_APP_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_APP_ID.Location = new System.Drawing.Point(778, 88);
            this.txt_APP_ID.Name = "txt_APP_ID";
            this.txt_APP_ID.Size = new System.Drawing.Size(73, 20);
            this.txt_APP_ID.TabIndex = 80;
            // 
            // txt_DLC_ID
            // 
            this.txt_DLC_ID.Cue = "DLC ID";
            this.txt_DLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLC_ID.Location = new System.Drawing.Point(553, 89);
            this.txt_DLC_ID.Name = "txt_DLC_ID";
            this.txt_DLC_ID.Size = new System.Drawing.Size(219, 20);
            this.txt_DLC_ID.TabIndex = 79;
            // 
            // txt_Tuning
            // 
            this.txt_Tuning.Cue = "Tuning (All)";
            this.txt_Tuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tuning.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tuning.Location = new System.Drawing.Point(614, 3);
            this.txt_Tuning.Name = "txt_Tuning";
            this.txt_Tuning.Size = new System.Drawing.Size(91, 20);
            this.txt_Tuning.TabIndex = 78;
            // 
            // txt_Version
            // 
            this.txt_Version.Cue = "Version";
            this.txt_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Version.ForeColor = System.Drawing.Color.Gray;
            this.txt_Version.Location = new System.Drawing.Point(499, 49);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(42, 20);
            this.txt_Version.TabIndex = 73;
            // 
            // txt_Author
            // 
            this.txt_Author.Cue = "Author";
            this.txt_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Author.ForeColor = System.Drawing.Color.Gray;
            this.txt_Author.Location = new System.Drawing.Point(499, 28);
            this.txt_Author.Name = "txt_Author";
            this.txt_Author.Size = new System.Drawing.Size(156, 20);
            this.txt_Author.TabIndex = 72;
            // 
            // txt_Rating
            // 
            this.txt_Rating.Cue = "Rating (All)";
            this.txt_Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Rating.ForeColor = System.Drawing.Color.Gray;
            this.txt_Rating.Location = new System.Drawing.Point(563, 3);
            this.txt_Rating.Name = "txt_Rating";
            this.txt_Rating.Size = new System.Drawing.Size(45, 20);
            this.txt_Rating.TabIndex = 71;
            // 
            // txt_Track_No
            // 
            this.txt_Track_No.Cue = "Track No.";
            this.txt_Track_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Track_No.ForeColor = System.Drawing.Color.Gray;
            this.txt_Track_No.Location = new System.Drawing.Point(499, 3);
            this.txt_Track_No.Name = "txt_Track_No";
            this.txt_Track_No.Size = new System.Drawing.Size(58, 20);
            this.txt_Track_No.TabIndex = 70;
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
            this.txt_Title_Sort.Size = new System.Drawing.Size(286, 20);
            this.txt_Title_Sort.TabIndex = 68;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(207, 47);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(286, 20);
            this.txt_Title.TabIndex = 67;
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Cue = "Artist  Sort";
            this.txt_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Sort.Location = new System.Drawing.Point(207, 25);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(286, 20);
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
            // MainDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 561);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "MainDB";
            this.Text = "MainDB";
            this.Load += new System.EventHandler(this.MainDB_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox chbx_Preview;
        private System.Windows.Forms.Button btn_SearchReset;
        private System.Windows.Forms.CheckBox chbx_Broken;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.CheckBox chbx_Sections;
        private System.Windows.Forms.CheckBox chbx_Alternate;
        private System.Windows.Forms.CheckBox chbx_DD;
        private System.Windows.Forms.CheckBox chbx_Original;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
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
        private CueTextBox txt_Rating;
        private CueTextBox txt_Track_No;
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
        private CueTextBox txt_Alt_No;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button btn_Conv_And_Transfer;
        private CueTextBox txt_Preview_Volume;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private CueTextBox txt_Volume;
        private System.Windows.Forms.Button button13;
        private CueTextBox cueTextBox5;
        private CueTextBox cueTextBox4;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_AverageTempo;
        private CueTextBox txt_ID;
        private CueTextBox txt_BassPicking;
        private CueTextBox txt_Group;
        private CueTextBox txt_Album_Year;
        private System.Windows.Forms.CheckBox chbx_BassDD;
        private System.Windows.Forms.CheckBox chbx_Save_All;
        private System.Windows.Forms.CheckBox chbx_Bonus;
        private System.Windows.Forms.Button button14;
        private CueTextBox txt_Artist_ShortName;
        private CueTextBox txt_Album_ShortName;
        private System.Windows.Forms.CheckBox chbx_Cover;
        private System.Windows.Forms.Button btn_ChangeCover;
        private CueTextBox txt_AlbumArtPath;
        private ComboBox comboBox1;
        private ComboBox cbx_Format;
        private Button button15;
        private CheckBox chbx_Avail_Old;
        private CheckBox chbx_Avail_Duplicate;
        private Label lbl_NoRec;
        private Button btn_Close;
        private CheckBox chbx_Has_Been_Corrected;
    }
}