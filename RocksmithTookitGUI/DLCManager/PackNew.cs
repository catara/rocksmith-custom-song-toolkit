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
using static Gpif.Rhythm;
using X360.Other;
using RocksmithToolkitLib.DLCPackage.XBlock;
using WinRT;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Swan;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class PackNew : Form
    {

        private DLCPackageData info;
        ToolTip toolTip1 = new ToolTip();
        public bool StopPack { get; set; }
        public PackNew(DLCPackageData Info, OleDbConnection cnb, SQLite.SQLiteConnection cnc)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {

            InitializeComponent();
            info = Info;

            //lbl_Link.Text = link;
            //txt_Description.Text = mss;
            //IgnoreSong = false;
            StopPack = false;
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
            components = new Container();
            helpProvider1 = new HelpProvider();
            splitContainer1 = new SplitContainer();
            btn_GotoRockband = new Button();
            txt_RockBand = new CueTextBox();
            lbl_Descri = new Label();
            txt_Descriptions = new RichTextBox();
            label3 = new Label();
            btn_EoFPath = new Button();
            btn_GP5 = new Button();
            btn_DefaultAuthor = new Button();
            label2 = new Label();
            txt_ToneDetails = new RichTextBox();
            btn_GoToGuitarUltimate = new Button();
            btn_GoToCustomforge = new Button();
            btn_GoToYoutube = new Button();
            btn_Album2SortA = new Button();
            label1 = new Label();
            lbl_Settings = new Label();
            txt_toDos = new RichTextBox();
            txt_Description = new RichTextBox();
            txt_TabLinks = new CueTextBox();
            txt_BasedOnYB = new CueTextBox();
            txt_YBLink = new CueTextBox();
            txt_Author = new CueTextBox();
            txt_BasedOnCF = new CueTextBox();
            txt_CDLC_Name = new CueTextBox();
            txt_CDLCID = new CueTextBox();
            txt_Version = new CueTextBox();
            btn_Spotify = new Button();
            txt_TrackNo = new CueTextBox();
            txt_Spotify = new CueTextBox();
            txt_GPFilePath = new CueTextBox();
            txt_UpdateDate = new CueTextBox();
            txt_PackageDate = new CueTextBox();
            txt_EoFPath = new CueTextBox();
            txt_PrevDate = new CueTextBox();
            chbx_RequiresSlide = new CheckBox();
            txt_RemoteFolder = new TextBox();
            btn_Cancel = new Button();
            lbl_LinkTN = new LinkLabel();
            lbl_LinkS = new LinkLabel();
            chbx_SaveRemotely = new CheckBox();
            lbl_LinkCF = new LinkLabel();
            lbl_LinkUG = new LinkLabel();
            chbx_SaveInDB = new CheckBox();
            chbx_SaveInVerisonInfo = new CheckBox();
            lbl_LinkYB = new LinkLabel();
            btn_B3 = new Button();
            toolTip1 = new ToolTip(components);
            this.txt_CF_Author = new CueTextBox();
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
            splitContainer1.Margin = new Padding(2);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(this.txt_CF_Author);
            splitContainer1.Panel1.Controls.Add(btn_GotoRockband);
            splitContainer1.Panel1.Controls.Add(txt_RockBand);
            splitContainer1.Panel1.Controls.Add(lbl_Descri);
            splitContainer1.Panel1.Controls.Add(txt_Descriptions);
            splitContainer1.Panel1.Controls.Add(label3);
            splitContainer1.Panel1.Controls.Add(btn_EoFPath);
            splitContainer1.Panel1.Controls.Add(btn_GP5);
            splitContainer1.Panel1.Controls.Add(btn_DefaultAuthor);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(txt_ToneDetails);
            splitContainer1.Panel1.Controls.Add(btn_GoToGuitarUltimate);
            splitContainer1.Panel1.Controls.Add(btn_GoToCustomforge);
            splitContainer1.Panel1.Controls.Add(btn_GoToYoutube);
            splitContainer1.Panel1.Controls.Add(btn_Album2SortA);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(lbl_Settings);
            splitContainer1.Panel1.Controls.Add(txt_toDos);
            splitContainer1.Panel1.Controls.Add(txt_Description);
            splitContainer1.Panel1.Controls.Add(txt_TabLinks);
            splitContainer1.Panel1.Controls.Add(txt_BasedOnYB);
            splitContainer1.Panel1.Controls.Add(txt_YBLink);
            splitContainer1.Panel1.Controls.Add(txt_Author);
            splitContainer1.Panel1.Controls.Add(txt_BasedOnCF);
            splitContainer1.Panel1.Controls.Add(txt_CDLC_Name);
            splitContainer1.Panel1.Controls.Add(txt_CDLCID);
            splitContainer1.Panel1.Controls.Add(txt_Version);
            splitContainer1.Panel1.Controls.Add(btn_Spotify);
            splitContainer1.Panel1.Controls.Add(txt_TrackNo);
            splitContainer1.Panel1.Controls.Add(txt_Spotify);
            splitContainer1.Panel1.Controls.Add(txt_GPFilePath);
            splitContainer1.Panel1.Controls.Add(txt_UpdateDate);
            splitContainer1.Panel1.Controls.Add(txt_PackageDate);
            splitContainer1.Panel1.Controls.Add(txt_EoFPath);
            splitContainer1.Panel1.Controls.Add(txt_PrevDate);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chbx_RequiresSlide);
            splitContainer1.Panel2.Controls.Add(txt_RemoteFolder);
            splitContainer1.Panel2.Controls.Add(btn_Cancel);
            splitContainer1.Panel2.Controls.Add(lbl_LinkTN);
            splitContainer1.Panel2.Controls.Add(lbl_LinkS);
            splitContainer1.Panel2.Controls.Add(chbx_SaveRemotely);
            splitContainer1.Panel2.Controls.Add(lbl_LinkCF);
            splitContainer1.Panel2.Controls.Add(lbl_LinkUG);
            splitContainer1.Panel2.Controls.Add(chbx_SaveInDB);
            splitContainer1.Panel2.Controls.Add(chbx_SaveInVerisonInfo);
            splitContainer1.Panel2.Controls.Add(lbl_LinkYB);
            splitContainer1.Panel2.Controls.Add(btn_B3);
            splitContainer1.Size = new Size(471, 640);
            splitContainer1.SplitterDistance = 450;
            splitContainer1.SplitterWidth = 2;
            splitContainer1.TabIndex = 336;
            // 
            // btn_GotoRockband
            // 
            btn_GotoRockband.Font = new Font("Microsoft Sans Serif", 6F);
            btn_GotoRockband.Location = new Point(444, 123);
            btn_GotoRockband.Margin = new Padding(1);
            btn_GotoRockband.Name = "btn_GotoRockband";
            btn_GotoRockband.Size = new Size(20, 17);
            btn_GotoRockband.TabIndex = 453;
            btn_GotoRockband.Text = ">";
            toolTip1.SetToolTip(btn_GotoRockband, "Go To tab Link");
            btn_GotoRockband.UseVisualStyleBackColor = true;
            btn_GotoRockband.Click += btn_GotoRockband_Click;
            // 
            // txt_RockBand
            // 
            txt_RockBand.Cue = "Rockband (Based on) Link";
            txt_RockBand.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_RockBand.ForeColor = Color.Gray;
            txt_RockBand.Location = new Point(2, 122);
            txt_RockBand.Margin = new Padding(2);
            txt_RockBand.Name = "txt_RockBand";
            txt_RockBand.Size = new Size(439, 20);
            txt_RockBand.TabIndex = 452;
            // 
            // lbl_Descri
            // 
            lbl_Descri.ForeColor = SystemColors.ControlText;
            lbl_Descri.Location = new Point(4, 205);
            lbl_Descri.Margin = new Padding(1, 0, 1, 0);
            lbl_Descri.Name = "lbl_Descri";
            lbl_Descri.Size = new Size(49, 49);
            lbl_Descri.TabIndex = 451;
            lbl_Descri.Text = "Description";
            // 
            // txt_Descriptions
            // 
            txt_Descriptions.BorderStyle = BorderStyle.None;
            txt_Descriptions.Location = new Point(59, 207);
            txt_Descriptions.Margin = new Padding(2);
            txt_Descriptions.Name = "txt_Descriptions";
            txt_Descriptions.Size = new Size(405, 62);
            txt_Descriptions.TabIndex = 450;
            txt_Descriptions.Text = "";
            // 
            // label3
            // 
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(369, 27);
            label3.Margin = new Padding(1, 0, 1, 0);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 449;
            label3.Text = "Prev";
            // 
            // btn_EoFPath
            // 
            btn_EoFPath.Location = new Point(444, 42);
            btn_EoFPath.Margin = new Padding(0);
            btn_EoFPath.Name = "btn_EoFPath";
            btn_EoFPath.Size = new Size(22, 20);
            btn_EoFPath.TabIndex = 447;
            btn_EoFPath.Text = "...";
            toolTip1.SetToolTip(btn_EoFPath, "Manually add EoF Path");
            btn_EoFPath.UseVisualStyleBackColor = true;
            btn_EoFPath.Click += btn_EoFPath_Click;
            // 
            // btn_GP5
            // 
            btn_GP5.Location = new Point(443, 161);
            btn_GP5.Margin = new Padding(0);
            btn_GP5.Name = "btn_GP5";
            btn_GP5.Size = new Size(22, 20);
            btn_GP5.TabIndex = 16;
            btn_GP5.Text = "...";
            toolTip1.SetToolTip(btn_GP5, "Manually Add GP files used in generating the project");
            btn_GP5.UseVisualStyleBackColor = true;
            btn_GP5.Click += btn_DBFolder_Click;
            // 
            // btn_DefaultAuthor
            // 
            btn_DefaultAuthor.Font = new Font("Microsoft Sans Serif", 6F);
            btn_DefaultAuthor.Location = new Point(203, 5);
            btn_DefaultAuthor.Margin = new Padding(1);
            btn_DefaultAuthor.Name = "btn_DefaultAuthor";
            btn_DefaultAuthor.Size = new Size(20, 17);
            btn_DefaultAuthor.TabIndex = 1;
            btn_DefaultAuthor.Text = "<";
            toolTip1.SetToolTip(btn_DefaultAuthor, "Default value from general_defaultauthor");
            btn_DefaultAuthor.UseVisualStyleBackColor = true;
            btn_DefaultAuthor.Click += btn_DefaultAuthor_Click;
            // 
            // label2
            // 
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(3, 385);
            label2.Margin = new Padding(1, 0, 1, 0);
            label2.Name = "label2";
            label2.Size = new Size(49, 49);
            label2.TabIndex = 436;
            label2.Text = "Tone Details";
            // 
            // txt_ToneDetails
            // 
            txt_ToneDetails.BorderStyle = BorderStyle.None;
            txt_ToneDetails.Location = new Point(59, 387);
            txt_ToneDetails.Margin = new Padding(2);
            txt_ToneDetails.Name = "txt_ToneDetails";
            txt_ToneDetails.Size = new Size(405, 62);
            txt_ToneDetails.TabIndex = 21;
            txt_ToneDetails.Text = "";
            // 
            // btn_GoToGuitarUltimate
            // 
            btn_GoToGuitarUltimate.Font = new Font("Microsoft Sans Serif", 6F);
            btn_GoToGuitarUltimate.Location = new Point(444, 146);
            btn_GoToGuitarUltimate.Margin = new Padding(1);
            btn_GoToGuitarUltimate.Name = "btn_GoToGuitarUltimate";
            btn_GoToGuitarUltimate.Size = new Size(20, 16);
            btn_GoToGuitarUltimate.TabIndex = 433;
            btn_GoToGuitarUltimate.Text = ">";
            toolTip1.SetToolTip(btn_GoToGuitarUltimate, "Go To tab Link");
            btn_GoToGuitarUltimate.UseVisualStyleBackColor = true;
            // 
            // btn_GoToCustomforge
            // 
            btn_GoToCustomforge.Font = new Font("Microsoft Sans Serif", 6F);
            btn_GoToCustomforge.Location = new Point(444, 99);
            btn_GoToCustomforge.Margin = new Padding(1);
            btn_GoToCustomforge.Name = "btn_GoToCustomforge";
            btn_GoToCustomforge.Size = new Size(20, 17);
            btn_GoToCustomforge.TabIndex = 14;
            btn_GoToCustomforge.Text = ">";
            toolTip1.SetToolTip(btn_GoToCustomforge, "Go To tab Link");
            btn_GoToCustomforge.UseVisualStyleBackColor = true;
            btn_GoToCustomforge.Click += btn_GoToCustomforge_Click;
            // 
            // btn_GoToYoutube
            // 
            btn_GoToYoutube.Font = new Font("Microsoft Sans Serif", 6F);
            btn_GoToYoutube.Location = new Point(444, 78);
            btn_GoToYoutube.Margin = new Padding(1);
            btn_GoToYoutube.Name = "btn_GoToYoutube";
            btn_GoToYoutube.Size = new Size(20, 18);
            btn_GoToYoutube.TabIndex = 12;
            btn_GoToYoutube.Text = ">";
            toolTip1.SetToolTip(btn_GoToYoutube, "Go To youtube Link");
            btn_GoToYoutube.UseVisualStyleBackColor = true;
            btn_GoToYoutube.Click += btn_GoToYoutube_Click;
            // 
            // btn_Album2SortA
            // 
            btn_Album2SortA.Font = new Font("Microsoft Sans Serif", 6F);
            btn_Album2SortA.Location = new Point(444, 60);
            btn_Album2SortA.Margin = new Padding(1);
            btn_Album2SortA.Name = "btn_Album2SortA";
            btn_Album2SortA.Size = new Size(20, 17);
            btn_Album2SortA.TabIndex = 10;
            btn_Album2SortA.Text = ">";
            toolTip1.SetToolTip(btn_Album2SortA, "Go To youtube Link");
            btn_Album2SortA.UseVisualStyleBackColor = true;
            btn_Album2SortA.Click += btn_Album2SortA_Click;
            // 
            // label1
            // 
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(2, 325);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(45, 16);
            label1.TabIndex = 408;
            label1.Text = "ToDo-s";
            // 
            // lbl_Settings
            // 
            lbl_Settings.ForeColor = SystemColors.ControlText;
            lbl_Settings.Location = new Point(2, 271);
            lbl_Settings.Margin = new Padding(1, 0, 1, 0);
            lbl_Settings.Name = "lbl_Settings";
            lbl_Settings.Size = new Size(54, 35);
            lbl_Settings.TabIndex = 407;
            lbl_Settings.Text = "Version Changes";
            // 
            // txt_toDos
            // 
            txt_toDos.BorderStyle = BorderStyle.None;
            txt_toDos.Location = new Point(59, 325);
            txt_toDos.Margin = new Padding(2);
            txt_toDos.Name = "txt_toDos";
            txt_toDos.Size = new Size(405, 58);
            txt_toDos.TabIndex = 20;
            txt_toDos.Text = "";
            // 
            // txt_Description
            // 
            txt_Description.BorderStyle = BorderStyle.None;
            txt_Description.Location = new Point(59, 273);
            txt_Description.Margin = new Padding(2);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(405, 48);
            txt_Description.TabIndex = 19;
            txt_Description.Text = "";
            // 
            // txt_TabLinks
            // 
            txt_TabLinks.Cue = "Tab Link (s; separated by commas)";
            txt_TabLinks.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_TabLinks.ForeColor = Color.Gray;
            txt_TabLinks.Location = new Point(2, 144);
            txt_TabLinks.Margin = new Padding(2);
            txt_TabLinks.Name = "txt_TabLinks";
            txt_TabLinks.Size = new Size(439, 20);
            txt_TabLinks.TabIndex = 15;
            // 
            // txt_BasedOnYB
            // 
            txt_BasedOnYB.Cue = "YB (Based on) Link(s; separated by commas)";
            txt_BasedOnYB.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_BasedOnYB.ForeColor = Color.Gray;
            txt_BasedOnYB.Location = new Point(2, 78);
            txt_BasedOnYB.Margin = new Padding(2);
            txt_BasedOnYB.Name = "txt_BasedOnYB";
            txt_BasedOnYB.Size = new Size(439, 20);
            txt_BasedOnYB.TabIndex = 11;
            // 
            // txt_YBLink
            // 
            txt_YBLink.Cue = "YB Link (s; separated by commas)";
            txt_YBLink.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_YBLink.ForeColor = Color.Gray;
            txt_YBLink.Location = new Point(2, 58);
            txt_YBLink.Margin = new Padding(2);
            txt_YBLink.Name = "txt_YBLink";
            txt_YBLink.Size = new Size(439, 20);
            txt_YBLink.TabIndex = 9;
            // 
            // txt_Author
            // 
            txt_Author.Cue = "Author";
            txt_Author.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_Author.ForeColor = Color.Gray;
            txt_Author.Location = new Point(2, 6);
            txt_Author.Margin = new Padding(2);
            txt_Author.Name = "txt_Author";
            txt_Author.Size = new Size(200, 20);
            txt_Author.TabIndex = 0;
            // 
            // txt_BasedOnCF
            // 
            txt_BasedOnCF.Cue = "CF (Based on) Link(; separated by commas)";
            txt_BasedOnCF.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_BasedOnCF.ForeColor = Color.Gray;
            txt_BasedOnCF.Location = new Point(2, 98);
            txt_BasedOnCF.Margin = new Padding(2);
            txt_BasedOnCF.Name = "txt_BasedOnCF";
            txt_BasedOnCF.Size = new Size(439, 20);
            txt_BasedOnCF.TabIndex = 13;
            // 
            // txt_CDLC_Name
            // 
            txt_CDLC_Name.Cue = "DLC Name";
            txt_CDLC_Name.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_CDLC_Name.ForeColor = Color.Gray;
            txt_CDLC_Name.Location = new Point(369, 5);
            txt_CDLC_Name.Margin = new Padding(2);
            txt_CDLC_Name.Name = "txt_CDLC_Name";
            txt_CDLC_Name.Size = new Size(95, 20);
            txt_CDLC_Name.TabIndex = 2;
            // 
            // txt_CDLCID
            // 
            txt_CDLCID.Cue = "CDLC ID";
            txt_CDLCID.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_CDLCID.ForeColor = Color.Gray;
            txt_CDLCID.Location = new Point(152, 25);
            txt_CDLCID.Margin = new Padding(2);
            txt_CDLCID.Name = "txt_CDLCID";
            txt_CDLCID.Size = new Size(73, 20);
            txt_CDLCID.TabIndex = 5;
            // 
            // txt_Version
            // 
            txt_Version.Cue = "Version";
            txt_Version.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_Version.ForeColor = Color.Gray;
            txt_Version.Location = new Point(54, 25);
            txt_Version.Margin = new Padding(2);
            txt_Version.Name = "txt_Version";
            txt_Version.Size = new Size(97, 20);
            txt_Version.TabIndex = 4;
            // 
            // btn_Spotify
            // 
            btn_Spotify.Font = new Font("Microsoft Sans Serif", 6F);
            btn_Spotify.Location = new Point(444, 184);
            btn_Spotify.Margin = new Padding(1);
            btn_Spotify.Name = "btn_Spotify";
            btn_Spotify.Size = new Size(20, 16);
            btn_Spotify.TabIndex = 18;
            btn_Spotify.Text = ">";
            toolTip1.SetToolTip(btn_Spotify, "Spotify");
            btn_Spotify.UseVisualStyleBackColor = true;
            btn_Spotify.Click += btn_Spotify_Click;
            // 
            // txt_TrackNo
            // 
            txt_TrackNo.Cue = "Track No.";
            txt_TrackNo.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_TrackNo.ForeColor = Color.Gray;
            txt_TrackNo.Location = new Point(2, 25);
            txt_TrackNo.Margin = new Padding(2);
            txt_TrackNo.Name = "txt_TrackNo";
            txt_TrackNo.Size = new Size(50, 20);
            txt_TrackNo.TabIndex = 3;
            // 
            // txt_Spotify
            // 
            txt_Spotify.Cue = "Spotify ID";
            txt_Spotify.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_Spotify.ForeColor = Color.Gray;
            txt_Spotify.Location = new Point(2, 181);
            txt_Spotify.Margin = new Padding(2);
            txt_Spotify.Name = "txt_Spotify";
            txt_Spotify.Size = new Size(439, 20);
            txt_Spotify.TabIndex = 17;
            // 
            // txt_GPFilePath
            // 
            txt_GPFilePath.Cue = "GP file (s; separated by commas;will be coppied in EoF path if not already there)";
            txt_GPFilePath.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_GPFilePath.ForeColor = Color.Gray;
            txt_GPFilePath.Location = new Point(2, 163);
            txt_GPFilePath.Margin = new Padding(2);
            txt_GPFilePath.Name = "txt_GPFilePath";
            txt_GPFilePath.Size = new Size(439, 20);
            txt_GPFilePath.TabIndex = 446;
            // 
            // txt_UpdateDate
            // 
            txt_UpdateDate.Cue = "Update date";
            txt_UpdateDate.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_UpdateDate.ForeColor = Color.Gray;
            txt_UpdateDate.Location = new Point(299, 25);
            txt_UpdateDate.Margin = new Padding(2);
            txt_UpdateDate.Name = "txt_UpdateDate";
            txt_UpdateDate.ReadOnly = true;
            txt_UpdateDate.Size = new Size(73, 20);
            txt_UpdateDate.TabIndex = 7;
            // 
            // txt_PackageDate
            // 
            txt_PackageDate.Cue = "Package Date";
            txt_PackageDate.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_PackageDate.ForeColor = Color.Gray;
            txt_PackageDate.Location = new Point(224, 25);
            txt_PackageDate.Margin = new Padding(2);
            txt_PackageDate.Name = "txt_PackageDate";
            txt_PackageDate.ReadOnly = true;
            txt_PackageDate.Size = new Size(73, 20);
            txt_PackageDate.TabIndex = 6;
            // 
            // txt_EoFPath
            // 
            txt_EoFPath.Cue = "EoF Project Path";
            txt_EoFPath.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_EoFPath.ForeColor = Color.Gray;
            txt_EoFPath.Location = new Point(2, 42);
            txt_EoFPath.Margin = new Padding(2);
            txt_EoFPath.Name = "txt_EoFPath";
            txt_EoFPath.Size = new Size(439, 20);
            txt_EoFPath.TabIndex = 8;
            // 
            // txt_PrevDate
            // 
            txt_PrevDate.Cue = "Prev Update date";
            txt_PrevDate.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_PrevDate.ForeColor = Color.Gray;
            txt_PrevDate.Location = new Point(398, 25);
            txt_PrevDate.Margin = new Padding(2);
            txt_PrevDate.Name = "txt_PrevDate";
            txt_PrevDate.ReadOnly = true;
            txt_PrevDate.Size = new Size(73, 20);
            txt_PrevDate.TabIndex = 448;
            // 
            // chbx_RequiresSlide
            // 
            chbx_RequiresSlide.Location = new Point(275, 60);
            chbx_RequiresSlide.Margin = new Padding(2);
            chbx_RequiresSlide.Name = "chbx_RequiresSlide";
            chbx_RequiresSlide.Size = new Size(144, 18);
            chbx_RequiresSlide.TabIndex = 440;
            chbx_RequiresSlide.Text = "Requires slide";
            toolTip1.SetToolTip(chbx_RequiresSlide, "Copy folder to remote location");
            chbx_RequiresSlide.UseVisualStyleBackColor = true;
            // 
            // txt_RemoteFolder
            // 
            txt_RemoteFolder.Font = new Font("Microsoft Sans Serif", 8.25F);
            txt_RemoteFolder.ForeColor = Color.Gray;
            txt_RemoteFolder.Location = new Point(392, 38);
            txt_RemoteFolder.Margin = new Padding(2);
            txt_RemoteFolder.Name = "txt_RemoteFolder";
            txt_RemoteFolder.ReadOnly = true;
            txt_RemoteFolder.Size = new Size(49, 20);
            txt_RemoteFolder.TabIndex = 439;
            txt_RemoteFolder.Text = "NOKs";
            toolTip1.SetToolTip(txt_RemoteFolder, "OK/NOK state of the FTP/Repack folder");
            // 
            // btn_Cancel
            // 
            btn_Cancel.Dock = DockStyle.Bottom;
            btn_Cancel.Location = new Point(0, 103);
            btn_Cancel.Margin = new Padding(2);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(471, 27);
            btn_Cancel.TabIndex = 31;
            btn_Cancel.Text = "Stop the packing";
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // lbl_LinkTN
            // 
            lbl_LinkTN.Location = new Point(6, 68);
            lbl_LinkTN.Margin = new Padding(2, 0, 2, 0);
            lbl_LinkTN.Name = "lbl_LinkTN";
            lbl_LinkTN.Size = new Size(114, 16);
            lbl_LinkTN.TabIndex = 26;
            lbl_LinkTN.TabStop = true;
            lbl_LinkTN.Text = "Search for Track No.";
            lbl_LinkTN.LinkClicked += lbl_LinkTN_LinkClicked;
            // 
            // lbl_LinkS
            // 
            lbl_LinkS.Location = new Point(6, 52);
            lbl_LinkS.Margin = new Padding(2, 0, 2, 0);
            lbl_LinkS.Name = "lbl_LinkS";
            lbl_LinkS.Size = new Size(138, 16);
            lbl_LinkS.TabIndex = 14;
            lbl_LinkS.TabStop = true;
            lbl_LinkS.Text = "Search for the Spotify ID";
            lbl_LinkS.LinkClicked += lbl_LinkS_LinkClicked;
            // 
            // chbx_SaveRemotely
            // 
            chbx_SaveRemotely.Checked = true;
            chbx_SaveRemotely.CheckState = CheckState.Checked;
            chbx_SaveRemotely.Location = new Point(275, 38);
            chbx_SaveRemotely.Margin = new Padding(2);
            chbx_SaveRemotely.Name = "chbx_SaveRemotely";
            chbx_SaveRemotely.Size = new Size(144, 18);
            chbx_SaveRemotely.TabIndex = 29;
            chbx_SaveRemotely.Text = "Save Remotely";
            toolTip1.SetToolTip(chbx_SaveRemotely, "Copy folder to remote location");
            chbx_SaveRemotely.UseVisualStyleBackColor = true;
            // 
            // lbl_LinkCF
            // 
            lbl_LinkCF.Location = new Point(6, 35);
            lbl_LinkCF.Margin = new Padding(2, 0, 2, 0);
            lbl_LinkCF.Name = "lbl_LinkCF";
            lbl_LinkCF.Size = new Size(178, 16);
            lbl_LinkCF.TabIndex = 25;
            lbl_LinkCF.TabStop = true;
            lbl_LinkCF.Text = "Search for tab on Custom Forge";
            lbl_LinkCF.LinkClicked += lbl_LinkCF_LinkClicked;
            // 
            // lbl_LinkUG
            // 
            lbl_LinkUG.Location = new Point(6, 19);
            lbl_LinkUG.Margin = new Padding(2, 0, 2, 0);
            lbl_LinkUG.Name = "lbl_LinkUG";
            lbl_LinkUG.Size = new Size(183, 16);
            lbl_LinkUG.TabIndex = 23;
            lbl_LinkUG.TabStop = true;
            lbl_LinkUG.Text = "Search for tab on Ultimate Guitar";
            lbl_LinkUG.LinkClicked += lbl_LinkUG_LinkClicked;
            // 
            // chbx_SaveInDB
            // 
            chbx_SaveInDB.Checked = true;
            chbx_SaveInDB.CheckState = CheckState.Checked;
            chbx_SaveInDB.Enabled = false;
            chbx_SaveInDB.Location = new Point(275, 20);
            chbx_SaveInDB.Margin = new Padding(2);
            chbx_SaveInDB.Name = "chbx_SaveInDB";
            chbx_SaveInDB.Size = new Size(122, 18);
            chbx_SaveInDB.TabIndex = 28;
            chbx_SaveInDB.Text = "<Save in DB>?";
            chbx_SaveInDB.UseVisualStyleBackColor = true;
            // 
            // chbx_SaveInVerisonInfo
            // 
            chbx_SaveInVerisonInfo.Checked = true;
            chbx_SaveInVerisonInfo.CheckState = CheckState.Checked;
            chbx_SaveInVerisonInfo.Location = new Point(275, 3);
            chbx_SaveInVerisonInfo.Margin = new Padding(2);
            chbx_SaveInVerisonInfo.Name = "chbx_SaveInVerisonInfo";
            chbx_SaveInVerisonInfo.Size = new Size(208, 18);
            chbx_SaveInVerisonInfo.TabIndex = 27;
            chbx_SaveInVerisonInfo.Text = "Save in PackageComments";
            toolTip1.SetToolTip(chbx_SaveInVerisonInfo, "Save in the package toolkit XML for reusing later ");
            chbx_SaveInVerisonInfo.UseVisualStyleBackColor = true;
            // 
            // lbl_LinkYB
            // 
            lbl_LinkYB.Location = new Point(6, 4);
            lbl_LinkYB.Margin = new Padding(2, 0, 2, 0);
            lbl_LinkYB.Name = "lbl_LinkYB";
            lbl_LinkYB.Size = new Size(108, 16);
            lbl_LinkYB.TabIndex = 22;
            lbl_LinkYB.TabStop = true;
            lbl_LinkYB.Text = "Search for YoutuBe";
            lbl_LinkYB.LinkClicked += Lbl_Link_LinkClicked;
            // 
            // btn_B3
            // 
            btn_B3.Dock = DockStyle.Bottom;
            btn_B3.Location = new Point(0, 130);
            btn_B3.Margin = new Padding(2);
            btn_B3.Name = "btn_B3";
            btn_B3.Size = new Size(471, 58);
            btn_B3.TabIndex = 30;
            btn_B3.Text = "OK (PackNow!)";
            btn_B3.UseVisualStyleBackColor = true;
            btn_B3.Click += btn_OK_Click;
            // 
            // txt_CF_Author
            // 
            this.txt_CF_Author.Cue = "CF Author-s";
            this.txt_CF_Author.Font = new Font("Microsoft Sans Serif", 8.25F);
            this.txt_CF_Author.ForeColor = Color.Gray;
            this.txt_CF_Author.Location = new Point(224, 5);
            this.txt_CF_Author.Margin = new Padding(2);
            this.txt_CF_Author.Name = "txt_CF_Author";
            this.txt_CF_Author.Size = new Size(141, 20);
            this.txt_CF_Author.TabIndex = 454;
            // 
            // PackNew
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new Size(471, 640);
            Controls.Add(splitContainer1);
            Margin = new Padding(2);
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
            StopPack = true;
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (chbx_SaveInVerisonInfo.Checked) ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = CleanInvalidXmlChars(
                txt_Author.Text + ";" + txt_CDLC_Name.Text + ";" + txt_TrackNo.Text + ";" + //0-2
                txt_Version.Text + ";" + txt_CDLCID.Text + ";" + txt_EoFPath.Text + ";" + //3-5
                txt_YBLink.Text.Replace(";", ",") + ";" + txt_BasedOnYB.Text.Replace(";", ",") + ";" + txt_BasedOnCF.Text.Replace(";", ",") + ";" + //6-8
                txt_TabLinks.Text.Replace(";", ",") + ";" + txt_Spotify.Text.Replace(";", ",") + ";" + txt_Description.Text.Replace(";", ",") + ";" +//9-11
                 txt_toDos.Text.Replace(";", ",") + ";" + txt_ToneDetails.Text.Replace(";", ",") + ";" + (chbx_SaveInVerisonInfo.Checked ? "Yes" : "No") + ";" +//12-14
                 (chbx_SaveInDB.Checked ? "Yes" : "No") + ";" + (chbx_SaveRemotely.Checked ? "Yes" : "No") + ";" + ConfigRepository.Instance()["dlcm_EoFPath"] + ";" + //15-17
                txt_PackageDate.Text + ";" + txt_UpdateDate.Text + ";" + txt_GPFilePath.Text + ";" +//18-20
               info.SongInfo.SongDisplayName + ";" + info.SongInfo.Artist + ";" + info.SongInfo.Album + ";" +//21-23                                                                                                           //  
                txt_Descriptions.Text + ";" + txt_RockBand.Text + ";" + (chbx_RequiresSlide.Checked ? "Yes" : "No") + ";" + txt_CF_Author.Text + ";"
                + "Author,DLC_Name,TrackNo," +
                "Version,CDLCID,txt_EoFPath," +
                "YBLink,BasedOnYB,BasedOnCF," +
                "TabLinks,Spotify,Description," +
                "toDo,ToneDetails,SaveInVerisonInfo," +
                "SaveInDB,SaveRemotely,SaveRemotelyPath," +
                "PackageDate,UpdateDate,BasedOn_GP,Songtitle,Artist,Album,PackageDetails,BaseOnRB,Has_Slide,CF_Author;");

            if (chbx_SaveRemotely.Checked)
            {
                var pth = "";
                if (Path.GetDirectoryName(txt_EoFPath.Text) != Path.GetDirectoryName(info.AlbumArtPath))
                {
                    if (txt_EoFPath.Text.Contains("\\notes.eof")) pth = Path.GetDirectoryName(txt_EoFPath.Text);
                    else pth = txt_EoFPath.Text;
                }
                else if (txt_EoFPath.Text.Contains("\\notes.eof")) pth = Path.GetDirectoryName(txt_EoFPath.Text);
                else pth = txt_EoFPath.Text;

                //copy to remote lib(192.168.1.100\\0/0_in_the_work folder
                var ccrf = c("dlcm_0_temp") + "\\0_intheworks\\" + pth.Replace("\\notes.eof", "").Replace(Path.GetDirectoryName(txt_EoFPath.Text.Replace("\\notes.eof", "")) + "\\", "");
                ccrf = ccrf.Replace("\\0_temp\\", "\\");/*.Replace("\\\\", "\\")*/
                var er = CopyFolder(pth, ccrf);

                //copy to local lib(0/0_in_the_work folder)
                var ccr = c("dlcm_TempPath") + "\\0_intheworks\\" + pth.Replace("\\notes.eof", "").Replace(Path.GetDirectoryName(txt_EoFPath.Text.Replace("\\notes.eof", "")) + "\\", "");
                ccr = ccr.Replace("\\0_temp\\", "\\");/*.Replace("\\\\", "\\")*/
                var ew = pth != ccr ? CopyFolder(pth, ccr) : "";

                MessageBox.Show("Also Copied:\n\n\t1." + Directory.Exists(ccrf) + " - " + ccrf + "\n\n\t2. " + Directory.Exists(ccr) + " - " + ccr);
            }
            if (chbx_SaveInDB.Checked)
            {
                //var ccrf = c("dlcm_0_temp") + "\\0_temp\\" + Path.GetDirectoryName(txt_E		endt	144	floatoFPath.Text);
                //var er=CopyFolder(txt_EoFPath.Text, ccrf);
                //MessageBox.Show("Also Copied: " + Directory.Exists(ccrf) + " - " + ccrf);
                ////timestamp = UpdateLog(timestamp, ccrf + " Remote copied zip: " + File.Exists(ccrf), true, c("dlcm_TempPath"), "", "DLCManager", null, null);
            }
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
            string link = "https://www.google.com/search?q=" + "Youtube" + "+" + info.SongInfo.Artist.Replace(" ", "%20") + "+" + info.SongInfo.Album.Replace(" ", "%20") + "+" + info.SongInfo.SongDisplayName.Replace(" ", "%20") + "+" + "Lyrics";
            StartProcesss(link, null);
        }

        public void PackNew_Load(object sender, EventArgs e)
        {
            var ud = ""; var emt = false;
            if (info != null)
            {
                this.Text = info.SongInfo.Artist + " - " + info.SongInfo.SongYear + " - " + info.SongInfo.Album + " - " + info.SongInfo.SongDisplayName;
                if (info.ToolkitInfo != null)
                {
                    if (info.ToolkitInfo.PackageComment.Contains(";"))
                    {
                        string[] ag = info.ToolkitInfo.PackageComment.ToString().Split(';');
                        txt_Author.Text = ag[0].Replace("Repacked by ", "");//\" Value=\"Repacked by catara\"
                        if (ag.Length >= 20)
                        {
                            if (!ag[ag.Length - 2].ToLower().Contains("author") || ag[0].Contains("Yes"))
                            {
                                string[] a = ag[ag.Length - 2].Split(',');
                                var a1 = ""; var a2 = ""; var a3 = ""; var a4 = ""; var a5 = "";
                                try { a1 = a[0] + ": " + ag[0] + "\n" + a[1] + ": " + ag[1] + "\n" + a[2] + ": " + ag[2] + "\n" + a[3] + ": " + ag[3] + "\n" + a[4] + ": " + ag[4] + "\n" + a[5] + ": " + ag[5] + "\n" + a[6] + ": " + ag[6] + "\n" + ag[7] + "\n" + a[8] + ": " + ag[8] + "\n" + a[9] + ": " + ag[9] + "\n"; } catch (Exception e1) { MessageBox.Show("error 9-"); }
                                try { a2 = a[10] + ": " + ag[10] + "\n" + a[11] + ": " + ag[11] + "\n" + a[12] + ": " + ag[12] + "\n" + a[13] + ": " + ag[13] + "\n" + a[14] + ": " + ag[14] + "\n" + a[15] + ": " + ag[15] + "\n" + a[16] + ": " + ag[16] + "\n" + a[17]; } catch (Exception e1) { MessageBox.Show("error 17-"); }
                                try { a3 = ": " + ag[17] + "\n" + a[18] + ": " + ag[18] + "\n" + a[19] + ": " + ag[19] + "\n" + a[20] + ": " + ag[20] + "\n"; } catch (Exception e1) { MessageBox.Show("error 20-"); }
                                try { a4 = a[21] + ": " + ag[21] + "\n" + a[22] + ": " + a[22] + "\n" + a[23] + ": " + ag[23]; } catch (Exception e1) { MessageBox.Show("error 21+"); }
                                try { a5 = a[24] + ": " + ag[25] + "\n" + a[26] + "\n" + a[27]; } catch (Exception e1) { MessageBox.Show("error 24+"); }
                                MessageBox.Show("There is something wrong with the NEW Song Metadata saved in the project XML PackageComment tag. Please fix and reload" + "\n:\n\n" + a1 + a2 + a3 + a4 + a5);
                                return;/*+ "\n" + a[28]; */
                            }
                            try
                            {
                                ud = ag[18];
                                txt_CDLC_Name.Text = ag[1] == "" ? info.Name : ag[1]; txt_TrackNo.Text = ag[2];
                                decimal d = decimal.Parse(ag[3]);
                                d = IncrementLastDigit(d);
                                txt_Version.Text = d.ToString(); txt_CDLCID.Text = ag[4];
                                txt_EoFPath.Text = Path.GetDirectoryName(info.Arrangements[0].SongXml.File.ToString()); txt_YBLink.Text = ag[6]; txt_BasedOnYB.Text = ag[7];
                                txt_BasedOnCF.Text = ag[8]; txt_TabLinks.Text = ag[9]; txt_Spotify.Text = ag[10]; txt_Description.Text = ag[11]; txt_toDos.Text = ag[12]; txt_ToneDetails.Text = ag[13];
                                chbx_SaveInVerisonInfo.Checked = ag[14] == "Yes" ? true : false; chbx_SaveInDB.Checked = ag[15] == "Yes" ? true : false; chbx_SaveRemotely.Checked = ag[16] == "Yes" ? true : false;
                                //txt_PackageDate.Text = ag[18];
                                txt_GPFilePath.Text = ag[20];txt_CF_Author.Text = ag[27]; 
                                txt_Descriptions.Text = ag[24]; txt_RockBand.Text = ag[25]; chbx_RequiresSlide.Checked = ag[26] == "Yes" ? true : false;
                                //ConfigRepository.Instance()["dlcm_EoFPath"] + ";"
                                //"Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate,BasedOn_GP;"
                            }
                            catch (Exception e1) { MessageBox.Show("error at read meta in comment"); }

                            var sel = "SELECT ID FROM Main WHERE LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                            DataSet dds = new DataSet(); if (txt_CDLCID.Text == "") dds = SelectFromDB("Main", sel, c("dlcm_DBFolder"), cnb, cnc);
                            var norec = GetNoRec(dds, cnb, cnc); var dlcn = "";
                            if (norec > 1)
                            {
                                for (int m = 0; m < norec; m++)
                                {
                                    dlcn += "\n" + info.Name + " - " + dds.Tables[0].Rows[m].ItemArray[1].ToString();
                                }
                                var result = MessageBox.Show("More than on DLC. (1) Yes - Take the first\n(2) No - Ignore and continue (set manually)" +
                                    "\n(3) Cancel - Stop packing?\n\n" + dlcn
                                    , "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                                if (result == DialogResult.Cancel)
                                {
                                    StopPack = true; return;
                                }
                                else if (result == DialogResult.Yes) txt_CDLCID.Text = txt_CDLCID.Text == "" ? dds.Tables[0].Rows[0].ItemArray[1].ToString() : txt_CDLCID.Text;
                            }
                            else if (norec > 0) txt_CDLCID.Text = txt_CDLCID.Text == "" ? dds.Tables[0].Rows[0].ItemArray[0].ToString() : txt_CDLCID.Text;
                            //Get last inserted ID;
                            //if (txt_PackageDate.Text == "")
                            txt_PackageDate.Text = DateTime.Now.ToString();
                            //else
                            //{
                            toolTip1.SetToolTip(txt_UpdateDate, "Previous Update Date: " + ud);
                            txt_PrevDate.Text = ud;
                            //}
                            emt = true;
                        }
                        if (txt_GPFilePath.Text == "") txt_GPFilePath.Text = GetGPfile();

                        //Check if Remote copying folder is accessible
                        var pth = "";
                        if (Path.GetDirectoryName(txt_EoFPath.Text) != Path.GetDirectoryName(info.AlbumArtPath))
                        {
                            if (pth.Contains("\\notes.eof")) pth = Path.GetDirectoryName(txt_EoFPath.Text);
                            else pth = txt_EoFPath.Text;
                        }
                        else if (pth.Contains("\\notes.eof")) pth = Path.GetDirectoryName(txt_EoFPath.Text);
                        else pth = txt_EoFPath.Text;

                        var ccrf = c("dlcm_0_temp") + "\\0_intheworks\\" + pth.Replace("\\notes.eof", "").Replace(Path.GetDirectoryName(txt_EoFPath.Text.Replace("\\notes.eof", "")) + "\\", "");
                        ccrf = ccrf.Replace("\\0_temp\\", "\\");/*.Replace("\\\\", "\\")*/
                        if (Directory.Exists(ccrf)) txt_RemoteFolder.Text = "OK";
                        else txt_RemoteFolder.Text = "NOK";
                        //if (!Directory.Exists(txt_EoFPath.Text))chbx_SaveRemotely.Checked = false; 
                        //var ccr = c("dlcm_0_temp") + "\\0_intheworks\\";
                        //ccr = ccr.Replace("\\0_temp\\", "\\");/*.Replace("\\\\", "\\")*/
                        toolTip1.SetToolTip(chbx_SaveRemotely, "Copy folder to remote location:" + ccrf);
                    }
                    if (txt_CDLCID.Text != "")
                    {

                    }
                    if (!emt)
                    {

                        txt_CDLC_Name.Text = info.Name;
                        txt_Version.Text = info.ToolkitInfo.PackageVersion;
                        //txt_CDLCID.Text = ag[4];
                        txt_EoFPath.Text = Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), "notes.eof");
                        txt_Author.Text = info.ToolkitInfo.PackageAuthor == null || info.ToolkitInfo.PackageAuthor.Contains("CDLC Creator") ? ConfigRepository.Instance()["general_defaultauthor"].Replace("Repacked by ", "") : info.ToolkitInfo.PackageAuthor.Replace("Repacked by ", "");
                        //"Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate"

                        //if (txt_PackageDate.Text == "")
                        txt_PackageDate.Text = DateTime.Now.ToString();
                        txt_UpdateDate.Text = DateTime.Now.ToString();
                        txt_GPFilePath.Text = GetGPfile();
                    }
                }
            }

            var result1 = MessageBox.Show("Things not to forget before packing newly a EoF song:\n" +
                    "\n1. Add 4000+ miliSecs of Leading Silence (ReEncode ?always? works)" +
                    "\n2. Add a meaningful name for Tracks that are Final" +
                    "\n3. Set Track Type (Rhythm/Lead)" +
                    "\n4. Set if an Bonus/Alternate track" +
                    "\n5. Remove Difficulty Limit" +
                    "\n6. Set difficulty to 0" +
                    "\n7. Set Bass Pick setting (or wo)" +
                    "\n8. Delete All then Generate Fee Hand positions" +
                    "\n9. Add tone (import from existing .psarc then rename to match existing empty placeholders tones that you just deleted)" +
                    "\n10. Add tone changes (start of the song, Default_'Instrument', then at instrument kickoff 4+ sec)" +
                    "\n11. Set Default tone to Default_'Instrument'" +
                    "\n12. Add Lyrics even if created in 3min using UltraCreator (press mouse to elongate some lyrics or extend them when precise tab them in EoF)" +
                    "\n\nOptional:\n13. Clean then Set song Sections"
                    , "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);/*\n\n(Cancel) will give you a chance to reload the then completed XMLs*/

            if (result1 == DialogResult.Cancel) btn_Close_Click(null, null);
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
            files = files.Length > 0 ? files.Substring(0, files.Length - 2) : files;
            return files;
        }
        private void btn_DBFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you GuitarPro5 file";
                fbd.Filter = "GP-5 file (*.gp5)|*.gp5";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                //if (fbd.FileName == Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File)))
                string[] filePaths = fbd.FileNames;

                foreach (var filePath in filePaths)
                {
                    var tard = "";
                    if (txt_EoFPath.Text == "") tard = Path.GetDirectoryName(info.Arrangements[0].SongXml.File);
                    else tard = Path.GetDirectoryName(txt_EoFPath.Text);
                    FileCopy(fbd.InitialDirectory + "\\" + filePath, Path.Combine(tard, fbd.FileName), true, 0, false);
                    txt_GPFilePath.Text += fbd.InitialDirectory + "\\" + filePath; //Path.Combine(Path.GetDirectoryName(info.Arrangements[0].SongXml.File), Path.GetFileName(fbd.SelectedPath));
                }
            }
        }

        private void lbl_LinkUG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = "https://www.google.com/search?q=" + "ultiamte-guitar.com" + "+" + info.SongInfo.Artist.Replace(" ", "%20") + "+" + info.SongInfo.Album.Replace(" ", "%20") + "+" + info.SongInfo.SongDisplayName.Replace(" ", "%20") + "+" + "Lyrics";
            StartProcesss(link, null);
        }

        private void lbl_LinkCF_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = "https://www.google.com/search?q=" + "customforge.com" + "+" + info.SongInfo.Artist.Replace(" ", "%20") + "+" + info.SongInfo.Album.Replace(" ", "%20") + "+" + info.SongInfo.SongDisplayName.Replace(" ", "%20") + "+" + "Lyrics";
            StartProcesss(link, null);
        }

        private void lbl_LinkS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lbl_LinkTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = "https://www.google.com/search?q=" + "track%20number" + "+" + info.SongInfo.Artist.Replace(" ", "%20") + "+" + info.SongInfo.Album.Replace(" ", "%20") + "+" + info.SongInfo.SongDisplayName.Replace(" ", "%20") + "+" + "Lyrics";
            StartProcesss(link, null);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (chbx_SaveInVerisonInfo.Checked) ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = CleanInvalidXmlChars(
    txt_Author.Text + ";" + txt_CDLC_Name.Text + ";" + txt_TrackNo.Text + ";" + //0-2
    txt_Version.Text + ";" + txt_CDLCID.Text + ";" + txt_EoFPath.Text + ";" + //3-5
    txt_YBLink.Text.Replace(";", ",") + ";" + txt_BasedOnYB.Text.Replace(";", ",") + ";" + txt_BasedOnCF.Text.Replace(";", ",") + ";" + //6-8
    txt_TabLinks.Text.Replace(";", ",") + ";" + txt_Spotify.Text.Replace(";", ",") + ";" + txt_Description.Text.Replace(";", ",") + ";" +//9-11
     txt_toDos.Text.Replace(";", ",") + ";" + txt_ToneDetails.Text.Replace(";", ",") + ";" + (chbx_SaveInVerisonInfo.Checked ? "Yes" : "No") + ";" +//12-14
     (chbx_SaveInDB.Checked ? "Yes" : "No") + ";" + (chbx_SaveRemotely.Checked ? "Yes" : "No") + ";" + ConfigRepository.Instance()["dlcm_EoFPath"] + ";" + //15-17
    txt_PackageDate.Text + ";" + txt_UpdateDate.Text + ";" + txt_GPFilePath.Text + ";" +//18-20
   info.SongInfo.SongDisplayName + ";" + info.SongInfo.Artist + ";" + info.SongInfo.Album + ";" +//21-23    
   txt_Descriptions.Text + ";" + txt_RockBand.Text + ";" + (chbx_RequiresSlide.Checked ? "Yes" : "No") + ";"+ txt_CF_Author +";"// 
                + "Author,DLC_Name,TrackNo," +
    "Version,CDLCID,txt_EoFPath," +
    "YBLink,BasedOnYB,BasedOnCF," +
    "TabLinks,Spotify,Description," +
    "toDo,ToneDetails,SaveInVerisonInfo," +
    "SaveInDB,SaveRemotely,SaveRemotelyPath," +
    "PackageDate,UpdateDate,BasedOn_GP,Songtitle,Artist,Album,PackageDetails,BaseOnRB,Has_Slide,CF_Author;");

            StopPack = true;
            this.Hide();
        }

        private void btm_DefaultAuthor_Click(object sender, EventArgs e)
        {

        }

        private void btn_DefaultAuthor_Click(object sender, EventArgs e)
        {
            txt_Author.Text = c("general_defaultauthor").Replace("Repacked by ", "");
        }

        private void btn_EoFPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select the Editor on Fire file path";
                fbd.Filter = "EoF file (*.eof)|*.eof";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                txt_EoFPath.Text = fbd.FileName;
            }
        }

        private void btn_Album2SortA_Click(object sender, EventArgs e)
        {

        }

        private void btn_Spotify_Click(object sender, EventArgs e)
        {
            StartProcesss(txt_Spotify.Text.Split(',')[0], null);
        }

        private void btn_GotoRockband_Click(object sender, EventArgs e)
        {
            StartProcesss(txt_RockBand.Text.Split(',')[0], null);
        }

        private void btn_GoToCustomforge_Click(object sender, EventArgs e)
        {
            StartProcesss(txt_BasedOnCF.Text.Split(',')[0], null);
        }

        private void btn_GoToYoutube_Click(object sender, EventArgs e)
        {
            StartProcesss(txt_BasedOnYB.Text.Split(',')[0], null);
        }
    }
}
