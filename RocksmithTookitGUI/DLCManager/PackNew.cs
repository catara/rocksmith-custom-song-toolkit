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
            helpProvider1 = new HelpProvider();
            splitContainer1 = new SplitContainer();
            btn_DBFolder = new Button();
            txt_GPFilePath = new CueTextBox();
            txt_UpdateDate = new CueTextBox();
            txt_PackageDate = new CueTextBox();
            txt_EoFPath = new CueTextBox();
            btm_DefaultAuthor = new Button();
            txt_CDLCID = new CueTextBox();
            txt_Version = new CueTextBox();
            btn_Spotify = new Button();
            txt_TrackNo = new CueTextBox();
            txt_Spotify = new CueTextBox();
            label2 = new Label();
            txt_ToneDetails = new RichTextBox();
            txt_CDLC_Name = new CueTextBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            btn_Album2SortA = new Button();
            txt_BasedOnCF = new CueTextBox();
            label1 = new Label();
            lbl_Settings = new Label();
            txt_toDos = new RichTextBox();
            txt_Description = new RichTextBox();
            txt_TabLinks = new CueTextBox();
            txt_BasedOnYB = new CueTextBox();
            txt_YBLink = new CueTextBox();
            txt_Author = new CueTextBox();
            linkLabel4 = new LinkLabel();
            linkLabel3 = new LinkLabel();
            chbx_SaveRemotely = new CheckBox();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            chbx_SaveInDB = new CheckBox();
            chbx_SaveInVerisonInfo = new CheckBox();
            lbl_Link = new LinkLabel();
            btn_B3 = new Button();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(btn_DBFolder);
            splitContainer1.Panel1.Controls.Add(txt_GPFilePath);
            splitContainer1.Panel1.Controls.Add(txt_UpdateDate);
            splitContainer1.Panel1.Controls.Add(txt_PackageDate);
            splitContainer1.Panel1.Controls.Add(txt_EoFPath);
            splitContainer1.Panel1.Controls.Add(btm_DefaultAuthor);
            splitContainer1.Panel1.Controls.Add(txt_CDLCID);
            splitContainer1.Panel1.Controls.Add(txt_Version);
            splitContainer1.Panel1.Controls.Add(btn_Spotify);
            splitContainer1.Panel1.Controls.Add(txt_TrackNo);
            splitContainer1.Panel1.Controls.Add(txt_Spotify);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(txt_ToneDetails);
            splitContainer1.Panel1.Controls.Add(txt_CDLC_Name);
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(btn_Album2SortA);
            splitContainer1.Panel1.Controls.Add(txt_BasedOnCF);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(lbl_Settings);
            splitContainer1.Panel1.Controls.Add(txt_toDos);
            splitContainer1.Panel1.Controls.Add(txt_Description);
            splitContainer1.Panel1.Controls.Add(txt_TabLinks);
            splitContainer1.Panel1.Controls.Add(txt_BasedOnYB);
            splitContainer1.Panel1.Controls.Add(txt_YBLink);
            splitContainer1.Panel1.Controls.Add(txt_Author);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(linkLabel4);
            splitContainer1.Panel2.Controls.Add(linkLabel3);
            splitContainer1.Panel2.Controls.Add(chbx_SaveRemotely);
            splitContainer1.Panel2.Controls.Add(linkLabel2);
            splitContainer1.Panel2.Controls.Add(linkLabel1);
            splitContainer1.Panel2.Controls.Add(chbx_SaveInDB);
            splitContainer1.Panel2.Controls.Add(chbx_SaveInVerisonInfo);
            splitContainer1.Panel2.Controls.Add(lbl_Link);
            splitContainer1.Panel2.Controls.Add(btn_B3);
            splitContainer1.Size = new Size(942, 1067);
            splitContainer1.SplitterDistance = 724;
            splitContainer1.TabIndex = 336;
            // 
            // btn_DBFolder
            // 
            btn_DBFolder.Location = new Point(822, 267);
            btn_DBFolder.Margin = new Padding(0);
            btn_DBFolder.Name = "btn_DBFolder";
            btn_DBFolder.Size = new Size(44, 40);
            btn_DBFolder.TabIndex = 16;
            btn_DBFolder.Text = "...";
            btn_DBFolder.UseVisualStyleBackColor = true;
            btn_DBFolder.Click += btn_DBFolder_Click;
            // 
            // txt_GPFilePath
            // 
            txt_GPFilePath.Cue = "GP file (s; separated by commas;will be coppied in EoF path if not already there)";
            txt_GPFilePath.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_GPFilePath.ForeColor = Color.Gray;
            txt_GPFilePath.Location = new Point(4, 272);
            txt_GPFilePath.Name = "txt_GPFilePath";
            txt_GPFilePath.Size = new Size(815, 32);
            txt_GPFilePath.TabIndex = 446;
            // 
            // txt_UpdateDate
            // 
            txt_UpdateDate.Cue = "Update date";
            txt_UpdateDate.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_UpdateDate.ForeColor = Color.Gray;
            txt_UpdateDate.Location = new Point(634, 50);
            txt_UpdateDate.Name = "txt_UpdateDate";
            txt_UpdateDate.Size = new Size(142, 32);
            txt_UpdateDate.TabIndex = 7;
            // 
            // txt_PackageDate
            // 
            txt_PackageDate.Cue = "Package Date";
            txt_PackageDate.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_PackageDate.ForeColor = Color.Gray;
            txt_PackageDate.Location = new Point(451, 50);
            txt_PackageDate.Name = "txt_PackageDate";
            txt_PackageDate.Size = new Size(142, 32);
            txt_PackageDate.TabIndex = 6;
            // 
            // txt_EoFPath
            // 
            txt_EoFPath.Cue = "EoF Project Path";
            txt_EoFPath.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_EoFPath.ForeColor = Color.Gray;
            txt_EoFPath.Location = new Point(3, 83);
            txt_EoFPath.Name = "txt_EoFPath";
            txt_EoFPath.Size = new Size(815, 32);
            txt_EoFPath.TabIndex = 8;
            // 
            // btm_DefaultAuthor
            // 
            btm_DefaultAuthor.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btm_DefaultAuthor.Location = new Point(406, 10);
            btm_DefaultAuthor.Margin = new Padding(2);
            btm_DefaultAuthor.Name = "btm_DefaultAuthor";
            btm_DefaultAuthor.Size = new Size(39, 34);
            btm_DefaultAuthor.TabIndex = 1;
            btm_DefaultAuthor.Text = "<";
            btm_DefaultAuthor.UseVisualStyleBackColor = true;
            // 
            // txt_CDLCID
            // 
            txt_CDLCID.Cue = "CDLC ID";
            txt_CDLCID.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_CDLCID.ForeColor = Color.Gray;
            txt_CDLCID.Location = new Point(303, 50);
            txt_CDLCID.Name = "txt_CDLCID";
            txt_CDLCID.Size = new Size(142, 32);
            txt_CDLCID.TabIndex = 5;
            // 
            // txt_Version
            // 
            txt_Version.Cue = "Version";
            txt_Version.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Version.ForeColor = Color.Gray;
            txt_Version.Location = new Point(107, 50);
            txt_Version.Name = "txt_Version";
            txt_Version.Size = new Size(190, 32);
            txt_Version.TabIndex = 4;
            // 
            // btn_Spotify
            // 
            btn_Spotify.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Spotify.Location = new Point(824, 309);
            btn_Spotify.Margin = new Padding(2);
            btn_Spotify.Name = "btn_Spotify";
            btn_Spotify.Size = new Size(39, 32);
            btn_Spotify.TabIndex = 18;
            btn_Spotify.Text = ">";
            btn_Spotify.UseVisualStyleBackColor = true;
            // 
            // txt_TrackNo
            // 
            txt_TrackNo.Cue = "Track No.";
            txt_TrackNo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_TrackNo.ForeColor = Color.Gray;
            txt_TrackNo.Location = new Point(4, 50);
            txt_TrackNo.Name = "txt_TrackNo";
            txt_TrackNo.Size = new Size(97, 32);
            txt_TrackNo.TabIndex = 3;
            // 
            // txt_Spotify
            // 
            txt_Spotify.Cue = "Spotify ID";
            txt_Spotify.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Spotify.ForeColor = Color.Gray;
            txt_Spotify.Location = new Point(3, 309);
            txt_Spotify.Name = "txt_Spotify";
            txt_Spotify.Size = new Size(816, 32);
            txt_Spotify.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(736, 581);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(138, 32);
            label2.TabIndex = 436;
            label2.Text = "ToneDetails";
            // 
            // txt_ToneDetails
            // 
            txt_ToneDetails.BorderStyle = BorderStyle.None;
            txt_ToneDetails.Location = new Point(4, 581);
            txt_ToneDetails.Margin = new Padding(4, 5, 4, 5);
            txt_ToneDetails.Name = "txt_ToneDetails";
            txt_ToneDetails.Size = new Size(859, 123);
            txt_ToneDetails.TabIndex = 21;
            txt_ToneDetails.Text = "";
            // 
            // txt_CDLC_Name
            // 
            txt_CDLC_Name.Cue = "DLC Name";
            txt_CDLC_Name.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_CDLC_Name.ForeColor = Color.Gray;
            txt_CDLC_Name.Location = new Point(451, 12);
            txt_CDLC_Name.Name = "txt_CDLC_Name";
            txt_CDLC_Name.Size = new Size(390, 32);
            txt_CDLC_Name.TabIndex = 2;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(824, 234);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(39, 32);
            button3.TabIndex = 433;
            button3.Text = ">";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(824, 196);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(39, 34);
            button2.TabIndex = 14;
            button2.Text = ">";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(824, 155);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(39, 37);
            button1.TabIndex = 12;
            button1.Text = ">";
            button1.UseVisualStyleBackColor = true;
            // 
            // btn_Album2SortA
            // 
            btn_Album2SortA.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Album2SortA.Location = new Point(824, 117);
            btn_Album2SortA.Margin = new Padding(2);
            btn_Album2SortA.Name = "btn_Album2SortA";
            btn_Album2SortA.Size = new Size(39, 34);
            btn_Album2SortA.TabIndex = 10;
            btn_Album2SortA.Text = ">";
            btn_Album2SortA.UseVisualStyleBackColor = true;
            // 
            // txt_BasedOnCF
            // 
            txt_BasedOnCF.Cue = "CF (Based on) Link(; separated by commas)";
            txt_BasedOnCF.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_BasedOnCF.ForeColor = Color.Gray;
            txt_BasedOnCF.Location = new Point(4, 196);
            txt_BasedOnCF.Name = "txt_BasedOnCF";
            txt_BasedOnCF.Size = new Size(815, 32);
            txt_BasedOnCF.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(781, 461);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(90, 32);
            label1.TabIndex = 408;
            label1.Text = "ToDo-s";
            // 
            // lbl_Settings
            // 
            lbl_Settings.AutoSize = true;
            lbl_Settings.ForeColor = SystemColors.ControlText;
            lbl_Settings.Location = new Point(686, 355);
            lbl_Settings.Margin = new Padding(2, 0, 2, 0);
            lbl_Settings.Name = "lbl_Settings";
            lbl_Settings.Size = new Size(191, 32);
            lbl_Settings.TabIndex = 407;
            lbl_Settings.Text = "Version Changes";
            // 
            // txt_toDos
            // 
            txt_toDos.BorderStyle = BorderStyle.None;
            txt_toDos.Location = new Point(4, 461);
            txt_toDos.Margin = new Padding(4, 5, 4, 5);
            txt_toDos.Name = "txt_toDos";
            txt_toDos.Size = new Size(859, 115);
            txt_toDos.TabIndex = 20;
            txt_toDos.Text = "";
            // 
            // txt_Description
            // 
            txt_Description.BorderStyle = BorderStyle.None;
            txt_Description.Location = new Point(4, 355);
            txt_Description.Margin = new Padding(4, 5, 4, 5);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(859, 96);
            txt_Description.TabIndex = 19;
            txt_Description.Text = "";
            // 
            // txt_TabLinks
            // 
            txt_TabLinks.Cue = "Tab Link (s; separated by commas)";
            txt_TabLinks.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_TabLinks.ForeColor = Color.Gray;
            txt_TabLinks.Location = new Point(4, 234);
            txt_TabLinks.Name = "txt_TabLinks";
            txt_TabLinks.Size = new Size(815, 32);
            txt_TabLinks.TabIndex = 15;
            // 
            // txt_BasedOnYB
            // 
            txt_BasedOnYB.Cue = "YB (Based on) Link(s; separated by commas)";
            txt_BasedOnYB.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_BasedOnYB.ForeColor = Color.Gray;
            txt_BasedOnYB.Location = new Point(3, 155);
            txt_BasedOnYB.Name = "txt_BasedOnYB";
            txt_BasedOnYB.Size = new Size(816, 32);
            txt_BasedOnYB.TabIndex = 11;
            // 
            // txt_YBLink
            // 
            txt_YBLink.Cue = "YB Link (s; separated by commas)";
            txt_YBLink.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_YBLink.ForeColor = Color.Gray;
            txt_YBLink.Location = new Point(4, 117);
            txt_YBLink.Name = "txt_YBLink";
            txt_YBLink.Size = new Size(815, 32);
            txt_YBLink.TabIndex = 9;
            // 
            // txt_Author
            // 
            txt_Author.Cue = "Author";
            txt_Author.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Author.ForeColor = Color.Gray;
            txt_Author.Location = new Point(4, 12);
            txt_Author.Name = "txt_Author";
            txt_Author.Size = new Size(397, 32);
            txt_Author.TabIndex = 0;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.Location = new Point(12, 106);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new Size(227, 32);
            linkLabel4.TabIndex = 26;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "Search for Track No.";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new Point(12, 81);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new Size(276, 32);
            linkLabel3.TabIndex = 14;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Search for the Spotify ID";
            // 
            // chbx_SaveRemotely
            // 
            chbx_SaveRemotely.AutoSize = true;
            chbx_SaveRemotely.Checked = true;
            chbx_SaveRemotely.CheckState = CheckState.Checked;
            chbx_SaveRemotely.Enabled = false;
            chbx_SaveRemotely.Location = new Point(634, 76);
            chbx_SaveRemotely.Name = "chbx_SaveRemotely";
            chbx_SaveRemotely.Size = new Size(203, 36);
            chbx_SaveRemotely.TabIndex = 29;
            chbx_SaveRemotely.Text = "Save Remotely";
            chbx_SaveRemotely.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(12, 56);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(355, 32);
            linkLabel2.TabIndex = 25;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Search for tab on Custom Forge";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(12, 30);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(366, 32);
            linkLabel1.TabIndex = 23;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Search for tab on Ultimate Guitar";
            // 
            // chbx_SaveInDB
            // 
            chbx_SaveInDB.AutoSize = true;
            chbx_SaveInDB.Checked = true;
            chbx_SaveInDB.CheckState = CheckState.Checked;
            chbx_SaveInDB.Enabled = false;
            chbx_SaveInDB.Location = new Point(634, 41);
            chbx_SaveInDB.Name = "chbx_SaveInDB";
            chbx_SaveInDB.Size = new Size(161, 36);
            chbx_SaveInDB.TabIndex = 28;
            chbx_SaveInDB.Text = "Save in DB";
            chbx_SaveInDB.UseVisualStyleBackColor = true;
            // 
            // chbx_SaveInVerisonInfo
            // 
            chbx_SaveInVerisonInfo.AutoSize = true;
            chbx_SaveInVerisonInfo.Checked = true;
            chbx_SaveInVerisonInfo.CheckState = CheckState.Checked;
            chbx_SaveInVerisonInfo.Enabled = false;
            chbx_SaveInVerisonInfo.Location = new Point(634, 6);
            chbx_SaveInVerisonInfo.Name = "chbx_SaveInVerisonInfo";
            chbx_SaveInVerisonInfo.Size = new Size(332, 36);
            chbx_SaveInVerisonInfo.TabIndex = 27;
            chbx_SaveInVerisonInfo.Text = "Save in PackageComments";
            chbx_SaveInVerisonInfo.UseVisualStyleBackColor = true;
            // 
            // lbl_Link
            // 
            lbl_Link.AutoSize = true;
            lbl_Link.Location = new Point(12, 7);
            lbl_Link.Name = "lbl_Link";
            lbl_Link.Size = new Size(217, 32);
            lbl_Link.TabIndex = 22;
            lbl_Link.TabStop = true;
            lbl_Link.Text = "Search for YoutuBe";
            lbl_Link.LinkClicked += Lbl_Link_LinkClicked;
            // 
            // btn_B3
            // 
            btn_B3.Dock = DockStyle.Bottom;
            btn_B3.Location = new Point(0, 223);
            btn_B3.Name = "btn_B3";
            btn_B3.Size = new Size(942, 116);
            btn_B3.TabIndex = 30;
            btn_B3.Text = "OK (PackNow!)";
            btn_B3.UseVisualStyleBackColor = true;
            btn_B3.Click += btn_OK_Click;
            // 
            // PackNew
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(942, 1067);
            Controls.Add(splitContainer1);
            Name = "PackNew";
            Load += PackNew_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
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
                        if (fbd.SelectedPath == Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File)))
                            FileCopy(fbd.SelectedPath, Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), Path.GetFileName(fbd.SelectedPath))
                                , true, 0, false);
                    txt_GPFilePath.Text = Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), Path.GetFileName(fbd.SelectedPath));
                }
        }
    }
}
