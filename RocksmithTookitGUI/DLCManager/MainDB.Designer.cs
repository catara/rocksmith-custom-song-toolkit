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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btn_Prev = new System.Windows.Forms.Button();
            this.btn_NextItem = new System.Windows.Forms.Button();
            this.txt_PreviewEnd = new System.Windows.Forms.NumericUpDown();
            this.btn_SelectPreview = new System.Windows.Forms.Button();
            this.chbx_Author = new System.Windows.Forms.CheckBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.chbx_FTP1 = new System.Windows.Forms.CheckBox();
            this.chbx_FTP2 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Filter = new System.Windows.Forms.ComboBox();
            this.chbx_Has_Been_Corrected = new System.Windows.Forms.CheckBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.chbx_Avail_Old = new System.Windows.Forms.CheckBox();
            this.chbx_Avail_Duplicate = new System.Windows.Forms.CheckBox();
            this.button15 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbx_Format = new System.Windows.Forms.ComboBox();
            this.btn_ChangeCover = new System.Windows.Forms.Button();
            this.chbx_Cover = new System.Windows.Forms.CheckBox();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.chbx_Bonus = new System.Windows.Forms.CheckBox();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
            this.chbx_BassDD = new System.Windows.Forms.CheckBox();
            this.btn_AddPreview = new System.Windows.Forms.Button();
            this.picbx_AlbumArtPath = new System.Windows.Forms.PictureBox();
            this.btn_PlayPreview = new System.Windows.Forms.Button();
            this.btn_PlayAudio = new System.Windows.Forms.Button();
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
            this.btn_SelectNone = new System.Windows.Forms.Button();
            this.txt_PreviewStart = new System.Windows.Forms.DateTimePicker();
            this.txt_AudioPreviewPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AudioPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_FTPPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_AlbumArtPath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Artist_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_ShortName = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Album_Year = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Group = new RocksmithToolkitGUI.CueTextBox();
            this.txt_BassPicking = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.txt_PreviewEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbx_AlbumArtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.txt_PreviewStart);
            this.Panel1.Controls.Add(this.btn_SelectNone);
            this.Panel1.Controls.Add(this.btn_Prev);
            this.Panel1.Controls.Add(this.btn_NextItem);
            this.Panel1.Controls.Add(this.txt_PreviewEnd);
            this.Panel1.Controls.Add(this.btn_SelectPreview);
            this.Panel1.Controls.Add(this.chbx_Author);
            this.Panel1.Controls.Add(this.txt_AudioPreviewPath);
            this.Panel1.Controls.Add(this.txt_AudioPath);
            this.Panel1.Controls.Add(this.btn_SteamDLCFolder);
            this.Panel1.Controls.Add(this.chbx_FTP1);
            this.Panel1.Controls.Add(this.chbx_FTP2);
            this.Panel1.Controls.Add(this.txt_FTPPath);
            this.Panel1.Controls.Add(this.checkBox2);
            this.Panel1.Controls.Add(this.button5);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.cmb_Filter);
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
            this.Panel1.Controls.Add(this.btn_SelectAll);
            this.Panel1.Controls.Add(this.chbx_Bonus);
            this.Panel1.Controls.Add(this.chbx_AutoSave);
            this.Panel1.Controls.Add(this.chbx_BassDD);
            this.Panel1.Controls.Add(this.txt_Album_Year);
            this.Panel1.Controls.Add(this.txt_Group);
            this.Panel1.Controls.Add(this.txt_BassPicking);
            this.Panel1.Controls.Add(this.txt_ID);
            this.Panel1.Controls.Add(this.btn_AddPreview);
            this.Panel1.Controls.Add(this.picbx_AlbumArtPath);
            this.Panel1.Controls.Add(this.txt_AverageTempo);
            this.Panel1.Controls.Add(this.txt_Preview_Volume);
            this.Panel1.Controls.Add(this.btn_PlayPreview);
            this.Panel1.Controls.Add(this.btn_PlayAudio);
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
            this.Panel1.Location = new System.Drawing.Point(0, 563);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1659, 311);
            this.Panel1.TabIndex = 3;
            // 
            // btn_Prev
            // 
            this.btn_Prev.Location = new System.Drawing.Point(779, 237);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.Size = new System.Drawing.Size(27, 34);
            this.btn_Prev.TabIndex = 319;
            this.btn_Prev.Text = "<";
            this.btn_Prev.UseVisualStyleBackColor = true;
            this.btn_Prev.Click += new System.EventHandler(this.btn_Prev_Click);
            // 
            // btn_NextItem
            // 
            this.btn_NextItem.Location = new System.Drawing.Point(812, 237);
            this.btn_NextItem.Name = "btn_NextItem";
            this.btn_NextItem.Size = new System.Drawing.Size(27, 34);
            this.btn_NextItem.TabIndex = 318;
            this.btn_NextItem.Text = ">";
            this.btn_NextItem.UseVisualStyleBackColor = true;
            this.btn_NextItem.Click += new System.EventHandler(this.btn_NextItem_Click);
            // 
            // txt_PreviewEnd
            // 
            this.txt_PreviewEnd.Location = new System.Drawing.Point(976, 214);
            this.txt_PreviewEnd.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txt_PreviewEnd.Name = "txt_PreviewEnd";
            this.txt_PreviewEnd.Size = new System.Drawing.Size(55, 26);
            this.txt_PreviewEnd.TabIndex = 316;
            this.txt_PreviewEnd.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btn_SelectPreview
            // 
            this.btn_SelectPreview.Location = new System.Drawing.Point(786, 272);
            this.btn_SelectPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SelectPreview.Name = "btn_SelectPreview";
            this.btn_SelectPreview.Size = new System.Drawing.Size(141, 34);
            this.btn_SelectPreview.TabIndex = 315;
            this.btn_SelectPreview.Text = "Change Preview";
            this.btn_SelectPreview.UseVisualStyleBackColor = true;
            this.btn_SelectPreview.Click += new System.EventHandler(this.btn_SelectPreview_Click);
            // 
            // chbx_Author
            // 
            this.chbx_Author.AutoSize = true;
            this.chbx_Author.Enabled = false;
            this.chbx_Author.Location = new System.Drawing.Point(1113, 111);
            this.chbx_Author.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Author.Name = "chbx_Author";
            this.chbx_Author.Size = new System.Drawing.Size(83, 24);
            this.chbx_Author.TabIndex = 314;
            this.chbx_Author.Text = "Author";
            this.chbx_Author.UseVisualStyleBackColor = true;
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(1617, 209);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(33, 23);
            this.btn_SteamDLCFolder.TabIndex = 311;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // chbx_FTP1
            // 
            this.chbx_FTP1.AutoSize = true;
            this.chbx_FTP1.Location = new System.Drawing.Point(1400, 212);
            this.chbx_FTP1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_FTP1.Name = "chbx_FTP1";
            this.chbx_FTP1.Size = new System.Drawing.Size(44, 24);
            this.chbx_FTP1.TabIndex = 310;
            this.chbx_FTP1.Text = "1";
            this.chbx_FTP1.UseVisualStyleBackColor = true;
            this.chbx_FTP1.CheckedChanged += new System.EventHandler(this.chbx_FTP1_CheckedChanged);
            // 
            // chbx_FTP2
            // 
            this.chbx_FTP2.AutoSize = true;
            this.chbx_FTP2.Location = new System.Drawing.Point(1455, 212);
            this.chbx_FTP2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_FTP2.Name = "chbx_FTP2";
            this.chbx_FTP2.Size = new System.Drawing.Size(44, 24);
            this.chbx_FTP2.TabIndex = 309;
            this.chbx_FTP2.Text = "2";
            this.chbx_FTP2.UseVisualStyleBackColor = true;
            this.chbx_FTP2.CheckedChanged += new System.EventHandler(this.chbx_FTP2_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(760, 214);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 24);
            this.checkBox2.TabIndex = 291;
            this.checkBox2.Text = "AutoPlay";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(204, 236);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(98, 69);
            this.button5.TabIndex = 278;
            this.button5.Text = "Open Retail DB";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1209, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 277;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 20);
            this.label1.TabIndex = 276;
            this.label1.Text = "Filter";
            // 
            // cmb_Filter
            // 
            this.cmb_Filter.FormattingEnabled = true;
            this.cmb_Filter.Items.AddRange(new object[] {
            "No Guitar",
            "No Preview",
            "No Section",
            "No Vocals",
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
            "Other Tunings"});
            this.cmb_Filter.Location = new System.Drawing.Point(128, 26);
            this.cmb_Filter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_Filter.Name = "cmb_Filter";
            this.cmb_Filter.Size = new System.Drawing.Size(172, 28);
            this.cmb_Filter.TabIndex = 275;
            this.cmb_Filter.SelectedValueChanged += new System.EventHandler(this.cmb_Filter_SelectedValueChanged);
            // 
            // chbx_Has_Been_Corrected
            // 
            this.chbx_Has_Been_Corrected.AutoSize = true;
            this.chbx_Has_Been_Corrected.Enabled = false;
            this.chbx_Has_Been_Corrected.Location = new System.Drawing.Point(932, 253);
            this.chbx_Has_Been_Corrected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Has_Been_Corrected.Name = "chbx_Has_Been_Corrected";
            this.chbx_Has_Been_Corrected.Size = new System.Drawing.Size(180, 24);
            this.chbx_Has_Been_Corrected.TabIndex = 274;
            this.chbx_Has_Been_Corrected.Text = "Has Been Corrected";
            this.chbx_Has_Been_Corrected.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1546, 277);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(108, 31);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Location = new System.Drawing.Point(18, 3);
            this.lbl_NoRec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(73, 44);
            this.lbl_NoRec.TabIndex = 113;
            this.lbl_NoRec.Text = "of Records";
            // 
            // chbx_Avail_Old
            // 
            this.chbx_Avail_Old.AutoSize = true;
            this.chbx_Avail_Old.Enabled = false;
            this.chbx_Avail_Old.Location = new System.Drawing.Point(1031, 280);
            this.chbx_Avail_Old.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Avail_Old.Name = "chbx_Avail_Old";
            this.chbx_Avail_Old.Size = new System.Drawing.Size(100, 24);
            this.chbx_Avail_Old.TabIndex = 112;
            this.chbx_Avail_Old.Text = "Old Avail.";
            this.chbx_Avail_Old.UseVisualStyleBackColor = true;
            // 
            // chbx_Avail_Duplicate
            // 
            this.chbx_Avail_Duplicate.AutoSize = true;
            this.chbx_Avail_Duplicate.Enabled = false;
            this.chbx_Avail_Duplicate.Location = new System.Drawing.Point(1132, 280);
            this.chbx_Avail_Duplicate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Avail_Duplicate.Name = "chbx_Avail_Duplicate";
            this.chbx_Avail_Duplicate.Size = new System.Drawing.Size(143, 24);
            this.chbx_Avail_Duplicate.TabIndex = 111;
            this.chbx_Avail_Duplicate.Text = "Duplicate Avail.";
            this.chbx_Avail_Duplicate.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(15, 251);
            this.button15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(180, 54);
            this.button15.TabIndex = 110;
            this.button15.Text = "Open Standarization DB";
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
            this.comboBox1.Location = new System.Drawing.Point(1455, 129);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 28);
            this.comboBox1.TabIndex = 109;
            // 
            // cbx_Format
            // 
            this.cbx_Format.FormattingEnabled = true;
            this.cbx_Format.Items.AddRange(new object[] {
            "PC",
            "PS3",
            "Mac",
            "XBOX360"});
            this.cbx_Format.Location = new System.Drawing.Point(1455, 88);
            this.cbx_Format.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbx_Format.Name = "cbx_Format";
            this.cbx_Format.Size = new System.Drawing.Size(67, 28);
            this.cbx_Format.TabIndex = 108;
            this.cbx_Format.Text = "PS3";
            this.cbx_Format.SelectedValueChanged += new System.EventHandler(this.cbx_Format_SelectedValueChanged);
            // 
            // btn_ChangeCover
            // 
            this.btn_ChangeCover.Location = new System.Drawing.Point(631, 240);
            this.btn_ChangeCover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_ChangeCover.Name = "btn_ChangeCover";
            this.btn_ChangeCover.Size = new System.Drawing.Size(126, 34);
            this.btn_ChangeCover.TabIndex = 106;
            this.btn_ChangeCover.Text = "Change Cover";
            this.btn_ChangeCover.UseVisualStyleBackColor = true;
            this.btn_ChangeCover.Click += new System.EventHandler(this.btn_ChangeCover_Click);
            // 
            // chbx_Cover
            // 
            this.chbx_Cover.AutoSize = true;
            this.chbx_Cover.Enabled = false;
            this.chbx_Cover.Location = new System.Drawing.Point(1007, 111);
            this.chbx_Cover.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Cover.Name = "chbx_Cover";
            this.chbx_Cover.Size = new System.Drawing.Size(76, 24);
            this.chbx_Cover.TabIndex = 105;
            this.chbx_Cover.Text = "Cover";
            this.chbx_Cover.UseVisualStyleBackColor = true;
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Location = new System.Drawing.Point(15, 193);
            this.btn_SelectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(87, 54);
            this.btn_SelectAll.TabIndex = 102;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.button14_Click);
            // 
            // chbx_Bonus
            // 
            this.chbx_Bonus.AutoSize = true;
            this.chbx_Bonus.Enabled = false;
            this.chbx_Bonus.Location = new System.Drawing.Point(1007, 90);
            this.chbx_Bonus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Bonus.Name = "chbx_Bonus";
            this.chbx_Bonus.Size = new System.Drawing.Size(81, 24);
            this.chbx_Bonus.TabIndex = 101;
            this.chbx_Bonus.Text = "Bonus";
            this.chbx_Bonus.UseVisualStyleBackColor = true;
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Enabled = false;
            this.chbx_AutoSave.Location = new System.Drawing.Point(1311, 171);
            this.chbx_AutoSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(105, 24);
            this.chbx_AutoSave.TabIndex = 100;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            // 
            // chbx_BassDD
            // 
            this.chbx_BassDD.AutoSize = true;
            this.chbx_BassDD.Enabled = false;
            this.chbx_BassDD.Location = new System.Drawing.Point(932, 280);
            this.chbx_BassDD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_BassDD.Name = "chbx_BassDD";
            this.chbx_BassDD.Size = new System.Drawing.Size(99, 24);
            this.chbx_BassDD.TabIndex = 99;
            this.chbx_BassDD.Text = "Bass DD";
            this.chbx_BassDD.UseVisualStyleBackColor = true;
            // 
            // btn_AddPreview
            // 
            this.btn_AddPreview.Location = new System.Drawing.Point(1030, 211);
            this.btn_AddPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_AddPreview.Name = "btn_AddPreview";
            this.btn_AddPreview.Size = new System.Drawing.Size(105, 29);
            this.btn_AddPreview.TabIndex = 94;
            this.btn_AddPreview.Text = "Add Preview";
            this.btn_AddPreview.UseVisualStyleBackColor = true;
            this.btn_AddPreview.Click += new System.EventHandler(this.button13_Click);
            // 
            // picbx_AlbumArtPath
            // 
            this.picbx_AlbumArtPath.Location = new System.Drawing.Point(310, 170);
            this.picbx_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picbx_AlbumArtPath.Name = "picbx_AlbumArtPath";
            this.picbx_AlbumArtPath.Size = new System.Drawing.Size(158, 135);
            this.picbx_AlbumArtPath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbx_AlbumArtPath.TabIndex = 91;
            this.picbx_AlbumArtPath.TabStop = false;
            // 
            // btn_PlayPreview
            // 
            this.btn_PlayPreview.Location = new System.Drawing.Point(918, 170);
            this.btn_PlayPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_PlayPreview.Name = "btn_PlayPreview";
            this.btn_PlayPreview.Size = new System.Drawing.Size(102, 40);
            this.btn_PlayPreview.TabIndex = 88;
            this.btn_PlayPreview.Text = "Preview";
            this.btn_PlayPreview.UseVisualStyleBackColor = true;
            this.btn_PlayPreview.Click += new System.EventHandler(this.btm_PlayPreview_Click);
            // 
            // btn_PlayAudio
            // 
            this.btn_PlayAudio.Location = new System.Drawing.Point(766, 171);
            this.btn_PlayAudio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_PlayAudio.Name = "btn_PlayAudio";
            this.btn_PlayAudio.Size = new System.Drawing.Size(102, 40);
            this.btn_PlayAudio.TabIndex = 87;
            this.btn_PlayAudio.Text = "Audio";
            this.btn_PlayAudio.UseVisualStyleBackColor = true;
            this.btn_PlayAudio.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(1455, 237);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(200, 40);
            this.button10.TabIndex = 85;
            this.button10.Text = "Remove BassDD";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // btn_Conv_And_Transfer
            // 
            this.btn_Conv_And_Transfer.Location = new System.Drawing.Point(1455, 163);
            this.btn_Conv_And_Transfer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Conv_And_Transfer.Name = "btn_Conv_And_Transfer";
            this.btn_Conv_And_Transfer.Size = new System.Drawing.Size(200, 40);
            this.btn_Conv_And_Transfer.TabIndex = 84;
            this.btn_Conv_And_Transfer.Text = "Pack & Copy/FTP";
            this.btn_Conv_And_Transfer.UseVisualStyleBackColor = true;
            this.btn_Conv_And_Transfer.Click += new System.EventHandler(this.btn_Conv_And_Transfer_Click);
            // 
            // chbx_Beta
            // 
            this.chbx_Beta.AutoSize = true;
            this.chbx_Beta.Location = new System.Drawing.Point(1113, 88);
            this.chbx_Beta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Beta.Name = "chbx_Beta";
            this.chbx_Beta.Size = new System.Drawing.Size(69, 24);
            this.chbx_Beta.TabIndex = 82;
            this.chbx_Beta.Text = "Beta";
            this.chbx_Beta.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.ForeColor = System.Drawing.Color.Green;
            this.btn_Save.Location = new System.Drawing.Point(1311, 123);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(126, 40);
            this.btn_Save.TabIndex = 81;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.button8_Click);
            // 
            // chbx_Rhythm
            // 
            this.chbx_Rhythm.AutoSize = true;
            this.chbx_Rhythm.Enabled = false;
            this.chbx_Rhythm.Location = new System.Drawing.Point(911, 111);
            this.chbx_Rhythm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Rhythm.Name = "chbx_Rhythm";
            this.chbx_Rhythm.Size = new System.Drawing.Size(90, 24);
            this.chbx_Rhythm.TabIndex = 77;
            this.chbx_Rhythm.Text = "Rhythm";
            this.chbx_Rhythm.UseVisualStyleBackColor = true;
            this.chbx_Rhythm.CheckedChanged += new System.EventHandler(this.chbx_Rhythm_CheckedChanged);
            // 
            // chbx_Combo
            // 
            this.chbx_Combo.AutoSize = true;
            this.chbx_Combo.Enabled = false;
            this.chbx_Combo.Location = new System.Drawing.Point(821, 108);
            this.chbx_Combo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Combo.Name = "chbx_Combo";
            this.chbx_Combo.Size = new System.Drawing.Size(86, 24);
            this.chbx_Combo.TabIndex = 76;
            this.chbx_Combo.Text = "Combo";
            this.chbx_Combo.UseVisualStyleBackColor = true;
            this.chbx_Combo.CheckedChanged += new System.EventHandler(this.chbx_Combo_CheckedChanged);
            // 
            // chbx_Bass
            // 
            this.chbx_Bass.AutoSize = true;
            this.chbx_Bass.Enabled = false;
            this.chbx_Bass.Location = new System.Drawing.Point(745, 142);
            this.chbx_Bass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Bass.Name = "chbx_Bass";
            this.chbx_Bass.Size = new System.Drawing.Size(71, 24);
            this.chbx_Bass.TabIndex = 75;
            this.chbx_Bass.Text = "Bass";
            this.chbx_Bass.UseVisualStyleBackColor = true;
            // 
            // chbx_Lead
            // 
            this.chbx_Lead.AutoSize = true;
            this.chbx_Lead.Enabled = false;
            this.chbx_Lead.Location = new System.Drawing.Point(744, 108);
            this.chbx_Lead.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Lead.Name = "chbx_Lead";
            this.chbx_Lead.Size = new System.Drawing.Size(71, 24);
            this.chbx_Lead.TabIndex = 74;
            this.chbx_Lead.Text = "Lead";
            this.chbx_Lead.UseVisualStyleBackColor = true;
            // 
            // chbx_Selected
            // 
            this.chbx_Selected.AutoSize = true;
            this.chbx_Selected.Location = new System.Drawing.Point(1007, 40);
            this.chbx_Selected.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Selected.Name = "chbx_Selected";
            this.chbx_Selected.Size = new System.Drawing.Size(98, 24);
            this.chbx_Selected.TabIndex = 64;
            this.chbx_Selected.Text = "Selected";
            this.chbx_Selected.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(1455, 3);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(200, 40);
            this.button7.TabIndex = 58;
            this.button7.Text = "Duplicate";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // chbx_Preview
            // 
            this.chbx_Preview.AutoSize = true;
            this.chbx_Preview.Enabled = false;
            this.chbx_Preview.Location = new System.Drawing.Point(1113, 62);
            this.chbx_Preview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Preview.Name = "chbx_Preview";
            this.chbx_Preview.Size = new System.Drawing.Size(89, 24);
            this.chbx_Preview.TabIndex = 57;
            this.chbx_Preview.Text = "Preview";
            this.chbx_Preview.UseVisualStyleBackColor = true;
            // 
            // btn_SearchReset
            // 
            this.btn_SearchReset.Location = new System.Drawing.Point(225, 156);
            this.btn_SearchReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SearchReset.Name = "btn_SearchReset";
            this.btn_SearchReset.Size = new System.Drawing.Size(76, 49);
            this.btn_SearchReset.TabIndex = 56;
            this.btn_SearchReset.Text = "Reset";
            this.btn_SearchReset.UseVisualStyleBackColor = true;
            this.btn_SearchReset.Click += new System.EventHandler(this.button6_Click);
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(1211, 26);
            this.txt_Description.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(232, 93);
            this.txt_Description.TabIndex = 54;
            this.txt_Description.Text = "";
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Location = new System.Drawing.Point(1113, 38);
            this.chbx_Broken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(86, 24);
            this.chbx_Broken.TabIndex = 53;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(225, 61);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(76, 92);
            this.btn_Search.TabIndex = 51;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // chbx_Sections
            // 
            this.chbx_Sections.AutoSize = true;
            this.chbx_Sections.Enabled = false;
            this.chbx_Sections.Location = new System.Drawing.Point(1007, 65);
            this.chbx_Sections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Sections.Name = "chbx_Sections";
            this.chbx_Sections.Size = new System.Drawing.Size(97, 24);
            this.chbx_Sections.TabIndex = 48;
            this.chbx_Sections.Text = "Sections";
            this.chbx_Sections.UseVisualStyleBackColor = true;
            // 
            // chbx_Alternate
            // 
            this.chbx_Alternate.AutoSize = true;
            this.chbx_Alternate.Location = new System.Drawing.Point(1060, 7);
            this.chbx_Alternate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Alternate.Name = "chbx_Alternate";
            this.chbx_Alternate.Size = new System.Drawing.Size(100, 24);
            this.chbx_Alternate.TabIndex = 46;
            this.chbx_Alternate.Text = "Alternate";
            this.chbx_Alternate.UseVisualStyleBackColor = true;
            // 
            // chbx_DD
            // 
            this.chbx_DD.AutoSize = true;
            this.chbx_DD.Enabled = false;
            this.chbx_DD.Location = new System.Drawing.Point(822, 74);
            this.chbx_DD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_DD.Name = "chbx_DD";
            this.chbx_DD.Size = new System.Drawing.Size(59, 24);
            this.chbx_DD.TabIndex = 45;
            this.chbx_DD.Text = "DD";
            this.chbx_DD.UseVisualStyleBackColor = true;
            // 
            // chbx_Original
            // 
            this.chbx_Original.AutoSize = true;
            this.chbx_Original.Location = new System.Drawing.Point(911, 77);
            this.chbx_Original.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbx_Original.Name = "chbx_Original";
            this.chbx_Original.Size = new System.Drawing.Size(88, 24);
            this.chbx_Original.TabIndex = 44;
            this.chbx_Original.Text = "Original";
            this.chbx_Original.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(1455, 43);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 40);
            this.button4.TabIndex = 40;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(1533, 83);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 40);
            this.button3.TabIndex = 39;
            this.button3.Text = "Package";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(1533, 123);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 40);
            this.button2.TabIndex = 38;
            this.button2.Text = "Add DD";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 136);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 54);
            this.button1.TabIndex = 37;
            this.button1.Text = "Open DB in M$ Access";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Tones
            // 
            this.btn_Tones.Location = new System.Drawing.Point(15, 99);
            this.btn_Tones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Tones.Name = "btn_Tones";
            this.btn_Tones.Size = new System.Drawing.Size(183, 35);
            this.btn_Tones.TabIndex = 36;
            this.btn_Tones.Text = "Open Tones";
            this.btn_Tones.UseVisualStyleBackColor = true;
            this.btn_Tones.Click += new System.EventHandler(this.btn_Tones_Click);
            // 
            // btn_Arrangements
            // 
            this.btn_Arrangements.Location = new System.Drawing.Point(15, 61);
            this.btn_Arrangements.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Arrangements.Name = "btn_Arrangements";
            this.btn_Arrangements.Size = new System.Drawing.Size(183, 35);
            this.btn_Arrangements.TabIndex = 35;
            this.btn_Arrangements.Text = "Open Arrangements";
            this.btn_Arrangements.UseVisualStyleBackColor = true;
            this.btn_Arrangements.Click += new System.EventHandler(this.btn_Arrangements_Click);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Location = new System.Drawing.Point(-230, 148);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(203, 24);
            this.CheckBox1.TabIndex = 34;
            this.CheckBox1.Text = "Show only MessageBox";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.Location = new System.Drawing.Point(0, 12);
            this.DataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DataGridView1.Name = "DataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.Size = new System.Drawing.Size(1659, 543);
            this.DataGridView1.TabIndex = 2;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.ChangeEdit);
            // 
            // btn_SelectNone
            // 
            this.btn_SelectNone.Location = new System.Drawing.Point(108, 193);
            this.btn_SelectNone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_SelectNone.Name = "btn_SelectNone";
            this.btn_SelectNone.Size = new System.Drawing.Size(87, 54);
            this.btn_SelectNone.TabIndex = 320;
            this.btn_SelectNone.Text = "Select None";
            this.btn_SelectNone.UseVisualStyleBackColor = true;
            this.btn_SelectNone.Click += new System.EventHandler(this.btn_SelectNone_Click);
            // 
            // txt_PreviewStart
            // 
            this.txt_PreviewStart.CustomFormat = "mm:ss";
            this.txt_PreviewStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_PreviewStart.Location = new System.Drawing.Point(880, 214);
            this.txt_PreviewStart.Name = "txt_PreviewStart";
            this.txt_PreviewStart.ShowUpDown = true;
            this.txt_PreviewStart.Size = new System.Drawing.Size(91, 26);
            this.txt_PreviewStart.TabIndex = 322;
            this.txt_PreviewStart.Value = new System.DateTime(2015, 5, 10, 0, 0, 0, 0);
            // 
            // txt_AudioPreviewPath
            // 
            this.txt_AudioPreviewPath.Cue = "Audio Preview Path";
            this.txt_AudioPreviewPath.Enabled = false;
            this.txt_AudioPreviewPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPreviewPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPreviewPath.Location = new System.Drawing.Point(632, 277);
            this.txt_AudioPreviewPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_AudioPreviewPath.Name = "txt_AudioPreviewPath";
            this.txt_AudioPreviewPath.Size = new System.Drawing.Size(146, 26);
            this.txt_AudioPreviewPath.TabIndex = 313;
            this.txt_AudioPreviewPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AudioPath
            // 
            this.txt_AudioPath.Cue = "Audio Path";
            this.txt_AudioPath.Enabled = false;
            this.txt_AudioPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AudioPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AudioPath.Location = new System.Drawing.Point(477, 277);
            this.txt_AudioPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_AudioPath.Name = "txt_AudioPath";
            this.txt_AudioPath.Size = new System.Drawing.Size(146, 26);
            this.txt_AudioPath.TabIndex = 312;
            this.txt_AudioPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_FTPPath
            // 
            this.txt_FTPPath.Cue = "FTP_Path";
            this.txt_FTPPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_FTPPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_FTPPath.Location = new System.Drawing.Point(1503, 206);
            this.txt_FTPPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_FTPPath.Name = "txt_FTPPath";
            this.txt_FTPPath.Size = new System.Drawing.Size(104, 26);
            this.txt_FTPPath.TabIndex = 308;
            this.txt_FTPPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlbumArtPath
            // 
            this.txt_AlbumArtPath.Cue = "Album art Path";
            this.txt_AlbumArtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AlbumArtPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_AlbumArtPath.Location = new System.Drawing.Point(477, 247);
            this.txt_AlbumArtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_AlbumArtPath.Name = "txt_AlbumArtPath";
            this.txt_AlbumArtPath.Size = new System.Drawing.Size(146, 26);
            this.txt_AlbumArtPath.TabIndex = 107;
            this.txt_AlbumArtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Artist_ShortName
            // 
            this.txt_Artist_ShortName.Cue = "Short";
            this.txt_Artist_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_ShortName.Location = new System.Drawing.Point(652, 5);
            this.txt_Artist_ShortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Artist_ShortName.Name = "txt_Artist_ShortName";
            this.txt_Artist_ShortName.Size = new System.Drawing.Size(85, 26);
            this.txt_Artist_ShortName.TabIndex = 104;
            // 
            // txt_Album_ShortName
            // 
            this.txt_Album_ShortName.Cue = "Short";
            this.txt_Album_ShortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_ShortName.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_ShortName.Location = new System.Drawing.Point(550, 140);
            this.txt_Album_ShortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Album_ShortName.Name = "txt_Album_ShortName";
            this.txt_Album_ShortName.Size = new System.Drawing.Size(85, 26);
            this.txt_Album_ShortName.TabIndex = 103;
            // 
            // txt_Album_Year
            // 
            this.txt_Album_Year.Cue = "Year";
            this.txt_Album_Year.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album_Year.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album_Year.Location = new System.Drawing.Point(643, 140);
            this.txt_Album_Year.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Album_Year.Name = "txt_Album_Year";
            this.txt_Album_Year.Size = new System.Drawing.Size(66, 26);
            this.txt_Album_Year.TabIndex = 98;
            // 
            // txt_Group
            // 
            this.txt_Group.Cue = "<Group>";
            this.txt_Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Group.ForeColor = System.Drawing.Color.Gray;
            this.txt_Group.Location = new System.Drawing.Point(477, 211);
            this.txt_Group.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Group.Name = "txt_Group";
            this.txt_Group.Size = new System.Drawing.Size(146, 26);
            this.txt_Group.TabIndex = 97;
            // 
            // txt_BassPicking
            // 
            this.txt_BassPicking.Cue = "Bass Picking";
            this.txt_BassPicking.Enabled = false;
            this.txt_BassPicking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_BassPicking.ForeColor = System.Drawing.Color.Gray;
            this.txt_BassPicking.Location = new System.Drawing.Point(1230, 212);
            this.txt_BassPicking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_BassPicking.Name = "txt_BassPicking";
            this.txt_BassPicking.Size = new System.Drawing.Size(158, 26);
            this.txt_BassPicking.TabIndex = 96;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(477, 180);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(66, 26);
            this.txt_ID.TabIndex = 95;
            // 
            // txt_AverageTempo
            // 
            this.txt_AverageTempo.Cue = "Avg. Tempo";
            this.txt_AverageTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_AverageTempo.ForeColor = System.Drawing.Color.Gray;
            this.txt_AverageTempo.Location = new System.Drawing.Point(550, 180);
            this.txt_AverageTempo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_AverageTempo.Name = "txt_AverageTempo";
            this.txt_AverageTempo.Size = new System.Drawing.Size(132, 26);
            this.txt_AverageTempo.TabIndex = 90;
            // 
            // txt_Preview_Volume
            // 
            this.txt_Preview_Volume.Cue = "Vol.";
            this.txt_Preview_Volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Preview_Volume.ForeColor = System.Drawing.Color.Gray;
            this.txt_Preview_Volume.Location = new System.Drawing.Point(1025, 177);
            this.txt_Preview_Volume.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Preview_Volume.Name = "txt_Preview_Volume";
            this.txt_Preview_Volume.Size = new System.Drawing.Size(38, 26);
            this.txt_Preview_Volume.TabIndex = 89;
            // 
            // txt_Volume
            // 
            this.txt_Volume.Cue = "Vol.";
            this.txt_Volume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Volume.ForeColor = System.Drawing.Color.Gray;
            this.txt_Volume.Location = new System.Drawing.Point(872, 177);
            this.txt_Volume.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Volume.Name = "txt_Volume";
            this.txt_Volume.Size = new System.Drawing.Size(38, 26);
            this.txt_Volume.TabIndex = 86;
            // 
            // txt_Alt_No
            // 
            this.txt_Alt_No.Cue = "No.";
            this.txt_Alt_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Alt_No.ForeColor = System.Drawing.Color.Gray;
            this.txt_Alt_No.Location = new System.Drawing.Point(1168, 5);
            this.txt_Alt_No.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Alt_No.Name = "txt_Alt_No";
            this.txt_Alt_No.Size = new System.Drawing.Size(38, 26);
            this.txt_Alt_No.TabIndex = 83;
            // 
            // txt_APP_ID
            // 
            this.txt_APP_ID.Cue = "App ID";
            this.txt_APP_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_APP_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_APP_ID.Location = new System.Drawing.Point(1154, 137);
            this.txt_APP_ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_APP_ID.Name = "txt_APP_ID";
            this.txt_APP_ID.Size = new System.Drawing.Size(108, 26);
            this.txt_APP_ID.TabIndex = 80;
            // 
            // txt_DLC_ID
            // 
            this.txt_DLC_ID.Cue = "DLC ID";
            this.txt_DLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_DLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_DLC_ID.Location = new System.Drawing.Point(822, 138);
            this.txt_DLC_ID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_DLC_ID.Name = "txt_DLC_ID";
            this.txt_DLC_ID.Size = new System.Drawing.Size(326, 26);
            this.txt_DLC_ID.TabIndex = 79;
            // 
            // txt_Tuning
            // 
            this.txt_Tuning.Cue = "Tuning (All)";
            this.txt_Tuning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tuning.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tuning.Location = new System.Drawing.Point(921, 5);
            this.txt_Tuning.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Tuning.Name = "txt_Tuning";
            this.txt_Tuning.Size = new System.Drawing.Size(134, 26);
            this.txt_Tuning.TabIndex = 78;
            // 
            // txt_Version
            // 
            this.txt_Version.Cue = "Version";
            this.txt_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Version.ForeColor = System.Drawing.Color.Gray;
            this.txt_Version.Location = new System.Drawing.Point(745, 72);
            this.txt_Version.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(61, 26);
            this.txt_Version.TabIndex = 73;
            // 
            // txt_Author
            // 
            this.txt_Author.Cue = "Author";
            this.txt_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Author.ForeColor = System.Drawing.Color.Gray;
            this.txt_Author.Location = new System.Drawing.Point(744, 36);
            this.txt_Author.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Author.Name = "txt_Author";
            this.txt_Author.Size = new System.Drawing.Size(232, 26);
            this.txt_Author.TabIndex = 72;
            this.txt_Author.TextChanged += new System.EventHandler(this.txt_Author_TextChanged);
            this.txt_Author.Leave += new System.EventHandler(this.txt_Author_Leave);
            // 
            // txt_Rating
            // 
            this.txt_Rating.Cue = "Rating (All)";
            this.txt_Rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Rating.ForeColor = System.Drawing.Color.Gray;
            this.txt_Rating.Location = new System.Drawing.Point(844, 5);
            this.txt_Rating.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Rating.Name = "txt_Rating";
            this.txt_Rating.Size = new System.Drawing.Size(66, 26);
            this.txt_Rating.TabIndex = 71;
            // 
            // txt_Track_No
            // 
            this.txt_Track_No.Cue = "Track No.";
            this.txt_Track_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Track_No.ForeColor = System.Drawing.Color.Gray;
            this.txt_Track_No.Location = new System.Drawing.Point(745, 5);
            this.txt_Track_No.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Track_No.Name = "txt_Track_No";
            this.txt_Track_No.Size = new System.Drawing.Size(85, 26);
            this.txt_Track_No.TabIndex = 70;
            // 
            // txt_Album
            // 
            this.txt_Album.Cue = "Album";
            this.txt_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Album.ForeColor = System.Drawing.Color.Gray;
            this.txt_Album.Location = new System.Drawing.Point(310, 140);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(232, 26);
            this.txt_Album.TabIndex = 69;
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Cue = "Title Sort";
            this.txt_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title_Sort.Location = new System.Drawing.Point(310, 106);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(427, 26);
            this.txt_Title_Sort.TabIndex = 68;
            // 
            // txt_Title
            // 
            this.txt_Title.Cue = "Title";
            this.txt_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Title.ForeColor = System.Drawing.Color.Gray;
            this.txt_Title.Location = new System.Drawing.Point(310, 72);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(427, 26);
            this.txt_Title.TabIndex = 67;
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Cue = "Artist  Sort";
            this.txt_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist_Sort.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist_Sort.Location = new System.Drawing.Point(310, 38);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(427, 26);
            this.txt_Artist_Sort.TabIndex = 66;
            // 
            // txt_Artist
            // 
            this.txt_Artist.Cue = "Artist";
            this.txt_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Artist.ForeColor = System.Drawing.Color.Gray;
            this.txt_Artist.Location = new System.Drawing.Point(310, 5);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(331, 26);
            this.txt_Artist.TabIndex = 65;
            // 
            // MainDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1659, 874);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.DataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainDB";
            this.Text = "MainDB";
            this.Load += new System.EventHandler(this.MainDB_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PreviewEnd)).EndInit();
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
        private System.Windows.Forms.Button btn_PlayPreview;
        private System.Windows.Forms.Button btn_PlayAudio;
        private CueTextBox txt_Volume;
        private System.Windows.Forms.Button btn_AddPreview;
        private System.Windows.Forms.PictureBox picbx_AlbumArtPath;
        private CueTextBox txt_AverageTempo;
        private CueTextBox txt_ID;
        private CueTextBox txt_BassPicking;
        private CueTextBox txt_Group;
        private CueTextBox txt_Album_Year;
        private System.Windows.Forms.CheckBox chbx_BassDD;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
        private System.Windows.Forms.CheckBox chbx_Bonus;
        private System.Windows.Forms.Button btn_SelectAll;
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
        private Label label1;
        private ComboBox cmb_Filter;
        private Label label2;
        private Button button5;
        private CheckBox checkBox2;
        private Button btn_SteamDLCFolder;
        private CheckBox chbx_FTP1;
        private CheckBox chbx_FTP2;
        private CueTextBox txt_FTPPath;
        private CueTextBox txt_AudioPreviewPath;
        private CueTextBox txt_AudioPath;
        private CheckBox chbx_Author;
        private Button btn_SelectPreview;
        private NumericUpDown txt_PreviewEnd;
        private Button btn_Prev;
        private Button btn_NextItem;
        private Button btn_SelectNone;
        private DateTimePicker txt_PreviewStart;
    }
}