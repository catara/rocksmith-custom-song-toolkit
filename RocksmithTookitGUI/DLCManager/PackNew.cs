using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using RocksmithToolkitGUI;
using RocksmithToolkitGUI.DLCManager;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using RocksmithToolkitLib.Extensions; //dds
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.XmlRepository;
using System.Data.SQLite;
using SQLite;
using System.IO;
using RocksmithToolkitLib.Sng2014HSL;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class PackNew : Form
    {

        private DLCPackageData info;
        ToolTip toolTip1 = new ToolTip();
        public PackNew(DLCPackageData info, OleDbConnection cnb, SQLite.SQLiteConnection cnc)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {

            InitializeComponent();
            this.info = info;

            //lbl_Link.Text = link;
            //txt_Description.Text = mss;
            //IgnoreSong = false;
            //StopImport = false;
            //ErrorWindow.ActiveForm.Text = Title;
            //btn_B1.Visible = B1Visi;
            //btn_B2.Visible = B2Visi;
            //btn_B3.Visible = B3Visi;
            //if (B1Txt != "") btn_B1.Text = B1Txt;
            //if (B2Txt != "") btn_B2.Text = B2Txt;
            //if (B3Txt != "") btn_B3.Text = B3Txt;
            //MessageBox.Show("test0");
            //DB_Path = txt_DBFolder;
            //TempPath = txt_TempPath;
            //RocksmithDLCPath = txt_RocksmithDLCPath;


        }

        private void InitializeComponent()
        {
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_DBFolder = new System.Windows.Forms.Button();
            this.txt_GPFilePath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_UpdateDate = new RocksmithToolkitGUI.CueTextBox();
            this.txt_PackageDate = new RocksmithToolkitGUI.CueTextBox();
            this.txt_EoFPath = new RocksmithToolkitGUI.CueTextBox();
            this.btm_DefaultAuthor = new System.Windows.Forms.Button();
            this.txt_CDLCID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Version = new RocksmithToolkitGUI.CueTextBox();
            this.btn_Spotify = new System.Windows.Forms.Button();
            this.txt_TrackNo = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Spotify = new RocksmithToolkitGUI.CueTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ToneDetails = new System.Windows.Forms.RichTextBox();
            this.txt_CDLC_Name = new RocksmithToolkitGUI.CueTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Album2SortA = new System.Windows.Forms.Button();
            this.txt_BasedOnCF = new RocksmithToolkitGUI.CueTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Settings = new System.Windows.Forms.Label();
            this.txt_toDos = new System.Windows.Forms.RichTextBox();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.txt_TabLinks = new RocksmithToolkitGUI.CueTextBox();
            this.txt_BasedOnYB = new RocksmithToolkitGUI.CueTextBox();
            this.txt_YBLink = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Author = new RocksmithToolkitGUI.CueTextBox();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.chbx_SaveRemotely = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chbx_SaveInDB = new System.Windows.Forms.CheckBox();
            this.chbx_SaveInVerisonInfo = new System.Windows.Forms.CheckBox();
            this.lbl_Link = new System.Windows.Forms.LinkLabel();
            this.btn_B3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.btn_DBFolder);
            this.splitContainer1.Panel1.Controls.Add(this.txt_GPFilePath);
            this.splitContainer1.Panel1.Controls.Add(this.txt_UpdateDate);
            this.splitContainer1.Panel1.Controls.Add(this.txt_PackageDate);
            this.splitContainer1.Panel1.Controls.Add(this.txt_EoFPath);
            this.splitContainer1.Panel1.Controls.Add(this.btm_DefaultAuthor);
            this.splitContainer1.Panel1.Controls.Add(this.txt_CDLCID);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Version);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Spotify);
            this.splitContainer1.Panel1.Controls.Add(this.txt_TrackNo);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Spotify);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txt_ToneDetails);
            this.splitContainer1.Panel1.Controls.Add(this.txt_CDLC_Name);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Album2SortA);
            this.splitContainer1.Panel1.Controls.Add(this.txt_BasedOnCF);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_Settings);
            this.splitContainer1.Panel1.Controls.Add(this.txt_toDos);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Description);
            this.splitContainer1.Panel1.Controls.Add(this.txt_TabLinks);
            this.splitContainer1.Panel1.Controls.Add(this.txt_BasedOnYB);
            this.splitContainer1.Panel1.Controls.Add(this.txt_YBLink);
            this.splitContainer1.Panel1.Controls.Add(this.txt_Author);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel4);
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel3);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_SaveRemotely);
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel2);
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_SaveInDB);
            this.splitContainer1.Panel2.Controls.Add(this.chbx_SaveInVerisonInfo);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_Link);
            this.splitContainer1.Panel2.Controls.Add(this.btn_B3);
            this.splitContainer1.Size = new System.Drawing.Size(942, 1067);
            this.splitContainer1.SplitterDistance = 724;
            this.splitContainer1.TabIndex = 336;
            // 
            // btn_DBFolder
            // 
            this.btn_DBFolder.Location = new System.Drawing.Point(822, 267);
            this.btn_DBFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btn_DBFolder.Name = "btn_DBFolder";
            this.btn_DBFolder.Size = new System.Drawing.Size(44, 40);
            this.btn_DBFolder.TabIndex = 447;
            this.btn_DBFolder.Text = "...";
            this.btn_DBFolder.UseVisualStyleBackColor = true;
            this.btn_DBFolder.Click += new System.EventHandler(this.btn_DBFolder_Click);
            // 
            // txt_GPFilePath
            // 
            this.txt_GPFilePath.Cue = "GP file (s; separated by commas;will be coppied in EoF path if not already there)" +
    "";
            this.txt_GPFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_GPFilePath.ForeColor = System.Drawing.Color.Gray;
            this.txt_GPFilePath.Location = new System.Drawing.Point(4, 272);
            this.txt_GPFilePath.Name = "txt_GPFilePath";
            this.txt_GPFilePath.Size = new System.Drawing.Size(815, 32);
            this.txt_GPFilePath.TabIndex = 446;
            // 
            // txt_UpdateDate
            // 
            this.txt_UpdateDate.Cue = "Update date";
            this.txt_UpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_UpdateDate.ForeColor = System.Drawing.Color.Gray;
            this.txt_UpdateDate.Location = new System.Drawing.Point(634, 50);
            this.txt_UpdateDate.Name = "txt_UpdateDate";
            this.txt_UpdateDate.Size = new System.Drawing.Size(142, 32);
            this.txt_UpdateDate.TabIndex = 445;
            // 
            // txt_PackageDate
            // 
            this.txt_PackageDate.Cue = "Package Date";
            this.txt_PackageDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_PackageDate.ForeColor = System.Drawing.Color.Gray;
            this.txt_PackageDate.Location = new System.Drawing.Point(451, 50);
            this.txt_PackageDate.Name = "txt_PackageDate";
            this.txt_PackageDate.Size = new System.Drawing.Size(142, 32);
            this.txt_PackageDate.TabIndex = 444;
            // 
            // txt_EoFPath
            // 
            this.txt_EoFPath.Cue = "EoF Project Path";
            this.txt_EoFPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_EoFPath.ForeColor = System.Drawing.Color.Gray;
            this.txt_EoFPath.Location = new System.Drawing.Point(3, 83);
            this.txt_EoFPath.Name = "txt_EoFPath";
            this.txt_EoFPath.Size = new System.Drawing.Size(815, 32);
            this.txt_EoFPath.TabIndex = 443;
            // 
            // btm_DefaultAuthor
            // 
            this.btm_DefaultAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btm_DefaultAuthor.Location = new System.Drawing.Point(406, 10);
            this.btm_DefaultAuthor.Margin = new System.Windows.Forms.Padding(2);
            this.btm_DefaultAuthor.Name = "btm_DefaultAuthor";
            this.btm_DefaultAuthor.Size = new System.Drawing.Size(39, 34);
            this.btm_DefaultAuthor.TabIndex = 442;
            this.btm_DefaultAuthor.Text = "<";
            this.btm_DefaultAuthor.UseVisualStyleBackColor = true;
            // 
            // txt_CDLCID
            // 
            this.txt_CDLCID.Cue = "CDLC ID";
            this.txt_CDLCID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_CDLCID.ForeColor = System.Drawing.Color.Gray;
            this.txt_CDLCID.Location = new System.Drawing.Point(303, 50);
            this.txt_CDLCID.Name = "txt_CDLCID";
            this.txt_CDLCID.Size = new System.Drawing.Size(142, 32);
            this.txt_CDLCID.TabIndex = 441;
            // 
            // txt_Version
            // 
            this.txt_Version.Cue = "Version";
            this.txt_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_Version.ForeColor = System.Drawing.Color.Gray;
            this.txt_Version.Location = new System.Drawing.Point(107, 50);
            this.txt_Version.Name = "txt_Version";
            this.txt_Version.Size = new System.Drawing.Size(190, 32);
            this.txt_Version.TabIndex = 440;
            // 
            // btn_Spotify
            // 
            this.btn_Spotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Spotify.Location = new System.Drawing.Point(824, 309);
            this.btn_Spotify.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Spotify.Name = "btn_Spotify";
            this.btn_Spotify.Size = new System.Drawing.Size(39, 32);
            this.btn_Spotify.TabIndex = 439;
            this.btn_Spotify.Text = ">";
            this.btn_Spotify.UseVisualStyleBackColor = true;
            // 
            // txt_TrackNo
            // 
            this.txt_TrackNo.Cue = "Track No.";
            this.txt_TrackNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_TrackNo.ForeColor = System.Drawing.Color.Gray;
            this.txt_TrackNo.Location = new System.Drawing.Point(4, 50);
            this.txt_TrackNo.Name = "txt_TrackNo";
            this.txt_TrackNo.Size = new System.Drawing.Size(97, 32);
            this.txt_TrackNo.TabIndex = 438;
            // 
            // txt_Spotify
            // 
            this.txt_Spotify.Cue = "Spotify ID";
            this.txt_Spotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_Spotify.ForeColor = System.Drawing.Color.Gray;
            this.txt_Spotify.Location = new System.Drawing.Point(3, 309);
            this.txt_Spotify.Name = "txt_Spotify";
            this.txt_Spotify.Size = new System.Drawing.Size(816, 32);
            this.txt_Spotify.TabIndex = 437;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(736, 581);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 436;
            this.label2.Text = "ToneDetails";
            // 
            // txt_ToneDetails
            // 
            this.txt_ToneDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ToneDetails.Location = new System.Drawing.Point(4, 581);
            this.txt_ToneDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ToneDetails.Name = "txt_ToneDetails";
            this.txt_ToneDetails.Size = new System.Drawing.Size(859, 123);
            this.txt_ToneDetails.TabIndex = 435;
            this.txt_ToneDetails.Text = "";
            // 
            // txt_CDLC_Name
            // 
            this.txt_CDLC_Name.Cue = "DLC Name";
            this.txt_CDLC_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_CDLC_Name.ForeColor = System.Drawing.Color.Gray;
            this.txt_CDLC_Name.Location = new System.Drawing.Point(451, 12);
            this.txt_CDLC_Name.Name = "txt_CDLC_Name";
            this.txt_CDLC_Name.Size = new System.Drawing.Size(390, 32);
            this.txt_CDLC_Name.TabIndex = 434;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(824, 234);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 32);
            this.button3.TabIndex = 433;
            this.button3.Text = ">";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(824, 196);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(39, 34);
            this.button2.TabIndex = 432;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(824, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 37);
            this.button1.TabIndex = 431;
            this.button1.Text = ">";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_Album2SortA
            // 
            this.btn_Album2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Album2SortA.Location = new System.Drawing.Point(824, 117);
            this.btn_Album2SortA.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Album2SortA.Name = "btn_Album2SortA";
            this.btn_Album2SortA.Size = new System.Drawing.Size(39, 34);
            this.btn_Album2SortA.TabIndex = 430;
            this.btn_Album2SortA.Text = ">";
            this.btn_Album2SortA.UseVisualStyleBackColor = true;
            // 
            // txt_BasedOnCF
            // 
            this.txt_BasedOnCF.Cue = "CF (Based on) Link(; separated by commas)";
            this.txt_BasedOnCF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_BasedOnCF.ForeColor = System.Drawing.Color.Gray;
            this.txt_BasedOnCF.Location = new System.Drawing.Point(4, 196);
            this.txt_BasedOnCF.Name = "txt_BasedOnCF";
            this.txt_BasedOnCF.Size = new System.Drawing.Size(815, 32);
            this.txt_BasedOnCF.TabIndex = 409;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(781, 461);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 32);
            this.label1.TabIndex = 408;
            this.label1.Text = "ToDo-s";
            // 
            // lbl_Settings
            // 
            this.lbl_Settings.AutoSize = true;
            this.lbl_Settings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Settings.Location = new System.Drawing.Point(686, 355);
            this.lbl_Settings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Settings.Name = "lbl_Settings";
            this.lbl_Settings.Size = new System.Drawing.Size(191, 32);
            this.lbl_Settings.TabIndex = 407;
            this.lbl_Settings.Text = "Version Changes";
            // 
            // txt_toDos
            // 
            this.txt_toDos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_toDos.Location = new System.Drawing.Point(4, 461);
            this.txt_toDos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_toDos.Name = "txt_toDos";
            this.txt_toDos.Size = new System.Drawing.Size(859, 115);
            this.txt_toDos.TabIndex = 339;
            this.txt_toDos.Text = "";
            // 
            // txt_Description
            // 
            this.txt_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Description.Location = new System.Drawing.Point(4, 355);
            this.txt_Description.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(859, 96);
            this.txt_Description.TabIndex = 338;
            this.txt_Description.Text = "";
            // 
            // txt_TabLinks
            // 
            this.txt_TabLinks.Cue = "Tab Link (s; separated by commas)";
            this.txt_TabLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_TabLinks.ForeColor = System.Drawing.Color.Gray;
            this.txt_TabLinks.Location = new System.Drawing.Point(4, 234);
            this.txt_TabLinks.Name = "txt_TabLinks";
            this.txt_TabLinks.Size = new System.Drawing.Size(815, 32);
            this.txt_TabLinks.TabIndex = 3;
            // 
            // txt_BasedOnYB
            // 
            this.txt_BasedOnYB.Cue = "YB (Based on) Link(s; separated by commas)";
            this.txt_BasedOnYB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_BasedOnYB.ForeColor = System.Drawing.Color.Gray;
            this.txt_BasedOnYB.Location = new System.Drawing.Point(3, 155);
            this.txt_BasedOnYB.Name = "txt_BasedOnYB";
            this.txt_BasedOnYB.Size = new System.Drawing.Size(816, 32);
            this.txt_BasedOnYB.TabIndex = 2;
            // 
            // txt_YBLink
            // 
            this.txt_YBLink.Cue = "YB Link (s; separated by commas)";
            this.txt_YBLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_YBLink.ForeColor = System.Drawing.Color.Gray;
            this.txt_YBLink.Location = new System.Drawing.Point(4, 117);
            this.txt_YBLink.Name = "txt_YBLink";
            this.txt_YBLink.Size = new System.Drawing.Size(815, 32);
            this.txt_YBLink.TabIndex = 1;
            // 
            // txt_Author
            // 
            this.txt_Author.Cue = "Author";
            this.txt_Author.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_Author.ForeColor = System.Drawing.Color.Gray;
            this.txt_Author.Location = new System.Drawing.Point(4, 12);
            this.txt_Author.Name = "txt_Author";
            this.txt_Author.Size = new System.Drawing.Size(397, 32);
            this.txt_Author.TabIndex = 0;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(12, 106);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(227, 32);
            this.linkLabel4.TabIndex = 15;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Search for Track No.";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(12, 81);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(276, 32);
            this.linkLabel3.TabIndex = 14;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Search for the Spotify ID";
            // 
            // chbx_SaveRemotely
            // 
            this.chbx_SaveRemotely.AutoSize = true;
            this.chbx_SaveRemotely.Checked = true;
            this.chbx_SaveRemotely.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_SaveRemotely.Enabled = false;
            this.chbx_SaveRemotely.Location = new System.Drawing.Point(634, 76);
            this.chbx_SaveRemotely.Name = "chbx_SaveRemotely";
            this.chbx_SaveRemotely.Size = new System.Drawing.Size(203, 36);
            this.chbx_SaveRemotely.TabIndex = 13;
            this.chbx_SaveRemotely.Text = "Save Remotely";
            this.chbx_SaveRemotely.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(12, 56);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(355, 32);
            this.linkLabel2.TabIndex = 12;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Search for tab on Custom Forge";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 30);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(366, 32);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Search for tab on Ultimate Guitar";
            // 
            // chbx_SaveInDB
            // 
            this.chbx_SaveInDB.AutoSize = true;
            this.chbx_SaveInDB.Checked = true;
            this.chbx_SaveInDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_SaveInDB.Enabled = false;
            this.chbx_SaveInDB.Location = new System.Drawing.Point(634, 41);
            this.chbx_SaveInDB.Name = "chbx_SaveInDB";
            this.chbx_SaveInDB.Size = new System.Drawing.Size(161, 36);
            this.chbx_SaveInDB.TabIndex = 10;
            this.chbx_SaveInDB.Text = "Save in DB";
            this.chbx_SaveInDB.UseVisualStyleBackColor = true;
            // 
            // chbx_SaveInVerisonInfo
            // 
            this.chbx_SaveInVerisonInfo.AutoSize = true;
            this.chbx_SaveInVerisonInfo.Checked = true;
            this.chbx_SaveInVerisonInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_SaveInVerisonInfo.Enabled = false;
            this.chbx_SaveInVerisonInfo.Location = new System.Drawing.Point(634, 6);
            this.chbx_SaveInVerisonInfo.Name = "chbx_SaveInVerisonInfo";
            this.chbx_SaveInVerisonInfo.Size = new System.Drawing.Size(332, 36);
            this.chbx_SaveInVerisonInfo.TabIndex = 9;
            this.chbx_SaveInVerisonInfo.Text = "Save in PackageComments";
            this.chbx_SaveInVerisonInfo.UseVisualStyleBackColor = true;
            // 
            // lbl_Link
            // 
            this.lbl_Link.AutoSize = true;
            this.lbl_Link.Location = new System.Drawing.Point(12, 7);
            this.lbl_Link.Name = "lbl_Link";
            this.lbl_Link.Size = new System.Drawing.Size(217, 32);
            this.lbl_Link.TabIndex = 7;
            this.lbl_Link.TabStop = true;
            this.lbl_Link.Text = "Search for YoutuBe";
            this.lbl_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lbl_Link_LinkClicked);
            // 
            // btn_B3
            // 
            this.btn_B3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_B3.Location = new System.Drawing.Point(0, 223);
            this.btn_B3.Name = "btn_B3";
            this.btn_B3.Size = new System.Drawing.Size(942, 116);
            this.btn_B3.TabIndex = 5;
            this.btn_B3.Text = "OK (PackNow!)";
            this.btn_B3.UseVisualStyleBackColor = true;
            this.btn_B3.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // PackNew
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(942, 1067);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PackNew";
            this.Load += new System.EventHandler(this.PackNew_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //StopImport = true;
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = txt_Author.Text + ";" + txt_CDLC_Name.Text + ";" + txt_TrackNo.Text + ";" + txt_Version.Text + ";" + txt_CDLCID.Text + ";" + txt_EoFPath.Text + ";" + txt_YBLink.Text.Replace(";", ",") + ";" + txt_BasedOnYB.Text.Replace(";", ",")
                + ";" + txt_BasedOnCF.Text.Replace(";", ",") + ";" + txt_TabLinks.Text.Replace(";", ",") + ";" + txt_Spotify.Text.Replace(";", ",") + ";" + txt_Description.Text.Replace(";", ",") + ";" + txt_toDos.Text.Replace(";", ",") + ";" + txt_ToneDetails.Text.Replace(";", ",") + ";" + (chbx_SaveInVerisonInfo.Checked ? "Yes" : "No")
                + ";" + ";" + (chbx_SaveInDB.Checked ? "Yes" : "No") + ";" + (chbx_SaveRemotely.Checked ? "Yes" : "No") + ConfigRepository.Instance()["dlcm_EoFPath"] + ";" + ";" + txt_PackageDate.Text + ";" + txt_UpdateDate.Text + ";" + txt_GPFilePath.Text
                + "Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate,BasedOn_GP";

            //exit();
            this.Hide();
        }

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            //IgnoreSong = true;
            this.Hide();
        }

        private void lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            //Process.Start(txt_YBLink.Text);
        }

        public void PackNew_Load(object sender, EventArgs e)
        {
            var ud = ""; var emt = false;
            if (info.ToolkitInfo != null)
                if (info.ToolkitInfo.PackageComment.Contains(";"))
                {
                    string[] ag = info.ToolkitInfo.PackageComment.ToString().Split(';');
                    txt_Author.Text = ag[0].Replace("Repacked by", "");
                    if (ag.Length >= 20)
                    {
                        ud = ag[19];
                        txt_CDLC_Name.Text = ag[1]; txt_TrackNo.Text = ag[2]; txt_Version.Text = ag[3]; txt_CDLCID.Text = ag[4]; txt_EoFPath.Text = ag[5]; txt_YBLink.Text = ag[6]; txt_BasedOnYB.Text = ag[7];
                        txt_BasedOnCF.Text = ag[8]; txt_TabLinks.Text = ag[9]; txt_Spotify.Text = ag[10]; txt_Description.Text = ag[11]; txt_toDos.Text = ag[12]; txt_ToneDetails.Text = ag[14];
                        chbx_SaveInVerisonInfo.Checked = ag[15] == "Yes" ? true : false; chbx_SaveInDB.Checked = ag[16] == "Yes" ? true : false; chbx_SaveRemotely.Checked = ag[17] == "Yes" ? true : false;
                        //txt_PackageDate.Text = ag[18];
                        txt_GPFilePath.Text = ag[20];//ConfigRepository.Instance()["dlcm_EoFPath"] + ";"
                        //"Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate,BasedOn_GP"

                        //if (txt_PackageDate.Text == "")
                        txt_PackageDate.Text = DateTime.Now.ToString();
                        //else
                        //{
                        toolTip1.SetToolTip(txt_UpdateDate, "Previous Update Date: " + ud);
                        //}
                        emt = true;
                    }
                    if (txt_GPFilePath.Text == "") txt_GPFilePath.Text = GetGPfile();
                }
            if (!emt)
            {

                txt_CDLC_Name.Text = info.Name;
                txt_Version.Text = info.ToolkitInfo.PackageVersion;
                //txt_CDLCID.Text = ag[4];
                txt_EoFPath.Text = Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), "notes.eof");
                txt_Author.Text = info.ToolkitInfo.PackageAuthor == null || info.ToolkitInfo.PackageAuthor.Contains("CDLC Creator") ? ConfigRepository.Instance()["general_defaultauthor"] : info.ToolkitInfo.PackageAuthor;
                //"Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate"

                //if (txt_PackageDate.Text == "")
                txt_PackageDate.Text = DateTime.Now.ToString();
                txt_UpdateDate.Text = DateTime.Now.ToString();
                txt_GPFilePath.Text = GetGPfile();
            }

        }

        public string GetGPfile()
        {
            var templateList = Directory.EnumerateFiles(Path.GetDirectoryName(info.Arrangements[0].SongXml.File));
            var files = "";
            foreach (var template in templateList)
            {
                if (Path.GetExtension(template).ToLower() == ".gp" || Path.GetExtension(template).ToLower() == ".gpx" || Path.GetExtension(template).ToLower() == ".gp5")
                {
                    files += template + ",";
                }
            }
            return files;
        }
        private void btn_DBFolder_Click(object sender, EventArgs e)
            {
                var result1 = MessageBox.Show("chose file.", "GuitarPro file", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.OK)
                    using (var fbd = new VistaFolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                       if (fbd.SelectedPath== Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File)))
                            FileCopy(fbd.SelectedPath, Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), Path.GetFileName(fbd.SelectedPath))
                                , true,0,false);
                    txt_GPFilePath.Text = Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), Path.GetFileName(fbd.SelectedPath));
                }
            }
        }
    }
