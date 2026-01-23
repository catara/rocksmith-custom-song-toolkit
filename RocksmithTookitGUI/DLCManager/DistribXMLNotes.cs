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
using Windows.ApplicationModel.Appointments.DataProvider;
using System.Net;
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.Sng;
using Windows.Globalization.DateTimeFormatting;
using RocksmithToolkitLib;
using X360.Other;
//using Microsoft.AspNetCore.Mvc.Formatters;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
//using System.Web.Services.Description;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Windows.Devices.WiFiDirect;
using NLog.Targets;
using Windows.Devices.Geolocation;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using Swan.Formatters;
using Microsoft.VisualBasic.Devices;
using System.Windows.Shell;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Windows.Devices.Lights.Effects;
//using System.Windows.Media;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DistribXMLNotes : Form
    {
        private string sXml; private string FilePath; private string gPlatform; private bool Arrangoff;
        private string BasedOn_CF; private string EoFPath;
        public string perc_time_betw_notes = "0";

        public string ArrangID = "0";

        public DistribXMLNotes(string xml, string filePath, string sPlatform, bool arrangoff, string basedOn_CF, string eoFPath,
            OleDbConnection cnb, SQLite.SQLiteConnection cnc)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {

            InitializeComponent();

            sXml = xml; FilePath = filePath; gPlatform = sPlatform; Arrangoff = arrangoff;/*sXml = xml;*/
            BasedOn_CF = basedOn_CF; EoFPath = eoFPath;
            //perc_time_betw_notes = perc_time_betw_notes;

        }

        private void InitializeComponent()
        {
            components = new Container();
            lbl_Comments = new Label();
            btn_Estimate = new Button();
            txt_songStart = new DateTimePicker();
            txt_songLast = new DateTimePicker();
            xml_last = new DateTimePicker();
            xml_first = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btn_EOF = new Button();
            btn_OpenGP5 = new Button();
            btn_OpenXML = new Button();
            chbx_BassToo = new CheckBox();
            lbl_Track = new Label();
            txt_XMLPath = new TextBox();
            txt_Ttime = new TextBox();
            lbl_TempFolders = new Label();
            txt_GP5 = new TextBox();
            label8 = new Label();
            txt_Correction = new DateTimePicker();
            btn_Album2SortA = new Button();
            button1 = new Button();
            xml_firstMili = new TextBox();
            xml_lastMili = new TextBox();
            txt_songLastMili = new TextBox();
            txt_songStartMili = new TextBox();
            txt_CorrectionMili = new TextBox();
            btn_addcorrection = new Button();
            button4 = new Button();
            btn_RestoreXML = new Button();
            cmb_Sections = new ComboBox();
            label1 = new Label();
            label13 = new Label();
            txt_Description = new RichTextBox();
            btn_Save = new Button();
            btn_Distrib = new Button();
            label14 = new Label();
            txt_Sections = new RichTextBox();
            btn_MoveAllNotesAfter = new Button();
            btn_backup = new Button();
            btn_Close = new Button();
            btn_CleanDescr = new Button();
            btn_CleanStartSong = new Button();
            chbx_removehandshapes = new CheckBox();
            groupBox1 = new GroupBox();
            label22 = new Label();
            label21 = new Label();
            txt_P3LNMili = new TextBox();
            txt_P3LN = new DateTimePicker();
            label19 = new Label();
            txt_P3 = new TextBox();
            label17 = new Label();
            btn_ApplyP3 = new Button();
            btn_AddSections = new Button();
            label20 = new Label();
            txt_P3FNMili = new TextBox();
            txt_P3FN = new DateTimePicker();
            label18 = new Label();
            groupBox2 = new GroupBox();
            txt_RealLastNoteMili = new TextBox();
            txt_RealLastNote = new DateTimePicker();
            txt_Percentage = new TextBox();
            label9 = new Label();
            label6 = new Label();
            btn_ApplyP1 = new Button();
            button3 = new Button();
            groupBox3 = new GroupBox();
            txt_corr = new TextBox();
            btn_ApplyP2 = new Button();
            txt_PercTime = new TextBox();
            label15 = new Label();
            button5 = new Button();
            groupBox4 = new GroupBox();
            txt_DiffLenght = new TextBox();
            label16 = new Label();
            txt_Lastnote = new TextBox();
            label12 = new Label();
            txt_ExpctLenght = new TextBox();
            label11 = new Label();
            txt_CurrentLenght = new TextBox();
            txt_MaxRealLenght = new TextBox();
            label10 = new Label();
            label7 = new Label();
            btn_SyncPh2Sect = new Button();
            btn_Reload = new Button();
            toolTip1 = new ToolTip(components);
            chbx_IgnorePhaseSync = new CheckBox();
            checkBox1 = new CheckBox();
            btn_VisualDistrib = new Button();
            txt_Backup = new TextBox();
            label23 = new Label();
            label24 = new Label();
            label25 = new Label();
            button2 = new Button();
            cmb_Tracks = new CheckedListBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_Comments
            // 
            lbl_Comments.AutoSize = true;
            lbl_Comments.ForeColor = SystemColors.ControlText;
            lbl_Comments.Location = new Point(-140, 346);
            lbl_Comments.Margin = new Padding(2, 0, 2, 0);
            lbl_Comments.Name = "lbl_Comments";
            lbl_Comments.Size = new Size(66, 15);
            lbl_Comments.TabIndex = 438;
            lbl_Comments.Text = "Comments";
            // 
            // btn_Estimate
            // 
            btn_Estimate.ForeColor = Color.Green;
            btn_Estimate.Location = new Point(6, 84);
            btn_Estimate.Margin = new Padding(2);
            btn_Estimate.Name = "btn_Estimate";
            btn_Estimate.Size = new Size(455, 36);
            btn_Estimate.TabIndex = 18;
            btn_Estimate.Text = "Estimate &&  enable Apply button(s)";
            btn_Estimate.UseVisualStyleBackColor = true;
            btn_Estimate.Click += btn_Estimate_Click;
            // 
            // txt_songStart
            // 
            txt_songStart.CustomFormat = "hh:mm:ss";
            txt_songStart.Format = DateTimePickerFormat.Time;
            txt_songStart.Location = new Point(132, 38);
            txt_songStart.Margin = new Padding(2);
            txt_songStart.Name = "txt_songStart";
            txt_songStart.ShowUpDown = true;
            txt_songStart.Size = new Size(63, 23);
            txt_songStart.TabIndex = 2;
            txt_songStart.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // txt_songLast
            // 
            txt_songLast.CustomFormat = "hh:mm:ss";
            txt_songLast.Format = DateTimePickerFormat.Time;
            txt_songLast.Location = new Point(132, 63);
            txt_songLast.Margin = new Padding(2);
            txt_songLast.Name = "txt_songLast";
            txt_songLast.ShowUpDown = true;
            txt_songLast.Size = new Size(63, 23);
            txt_songLast.TabIndex = 5;
            txt_songLast.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // xml_last
            // 
            xml_last.CustomFormat = "hh:mm:ss";
            xml_last.Format = DateTimePickerFormat.Time;
            xml_last.Location = new Point(352, 63);
            xml_last.Margin = new Padding(2);
            xml_last.Name = "xml_last";
            xml_last.ShowUpDown = true;
            xml_last.Size = new Size(66, 23);
            xml_last.TabIndex = 11;
            xml_last.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            xml_last.ValueChanged += xml_last_ValueChanged;
            // 
            // xml_first
            // 
            xml_first.CustomFormat = "hh:mm:ss";
            xml_first.Format = DateTimePickerFormat.Time;
            xml_first.Location = new Point(352, 38);
            xml_first.Margin = new Padding(2);
            xml_first.Name = "xml_first";
            xml_first.ShowUpDown = true;
            xml_first.Size = new Size(66, 23);
            xml_first.TabIndex = 9;
            xml_first.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(2, 41);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(130, 15);
            label2.TabIndex = 445;
            label2.Text = "Audio Track's first Note";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(2, 66);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(128, 15);
            label3.TabIndex = 446;
            label3.Text = "Audio Track's last Note";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(264, 41);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 447;
            label4.Text = "XML First Note";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(264, 64);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 448;
            label5.Text = "XML Last Note";
            // 
            // btn_EOF
            // 
            btn_EOF.Font = new Font("Microsoft Sans Serif", 6.6F);
            btn_EOF.Location = new Point(370, 572);
            btn_EOF.Margin = new Padding(2);
            btn_EOF.Name = "btn_EOF";
            btn_EOF.Size = new Size(87, 22);
            btn_EOF.TabIndex = 25;
            btn_EOF.Text = "Editor on Fire";
            btn_EOF.UseVisualStyleBackColor = true;
            btn_EOF.Click += btn_EOF_Click;
            // 
            // btn_OpenGP5
            // 
            btn_OpenGP5.Font = new Font("Microsoft Sans Serif", 6F);
            btn_OpenGP5.Location = new Point(370, 592);
            btn_OpenGP5.Margin = new Padding(2, 3, 2, 3);
            btn_OpenGP5.Name = "btn_OpenGP5";
            btn_OpenGP5.Size = new Size(88, 18);
            btn_OpenGP5.TabIndex = 26;
            btn_OpenGP5.Text = "Open GP5";
            btn_OpenGP5.UseVisualStyleBackColor = true;
            btn_OpenGP5.Click += btn_OpenGP5_Click;
            // 
            // btn_OpenXML
            // 
            btn_OpenXML.Font = new Font("Microsoft Sans Serif", 6F);
            btn_OpenXML.Location = new Point(370, 608);
            btn_OpenXML.Margin = new Padding(2, 3, 2, 3);
            btn_OpenXML.Name = "btn_OpenXML";
            btn_OpenXML.Size = new Size(87, 18);
            btn_OpenXML.TabIndex = 27;
            btn_OpenXML.Text = "Open (Master) XML";
            btn_OpenXML.UseVisualStyleBackColor = true;
            btn_OpenXML.Click += btn_OpenXML_Click;
            // 
            // chbx_BassToo
            // 
            chbx_BassToo.AutoCheck = false;
            chbx_BassToo.AutoSize = true;
            chbx_BassToo.Enabled = false;
            chbx_BassToo.Location = new Point(378, 421);
            chbx_BassToo.Margin = new Padding(2);
            chbx_BassToo.Name = "chbx_BassToo";
            chbx_BassToo.Size = new Size(115, 19);
            chbx_BassToo.TabIndex = 452;
            chbx_BassToo.Text = "Incl. other Tracks";
            chbx_BassToo.UseVisualStyleBackColor = true;
            chbx_BassToo.Visible = false;
            // 
            // lbl_Track
            // 
            lbl_Track.AutoSize = true;
            lbl_Track.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold);
            lbl_Track.ForeColor = SystemColors.ControlText;
            lbl_Track.Location = new Point(0, 1);
            lbl_Track.Margin = new Padding(2, 0, 2, 0);
            lbl_Track.Name = "lbl_Track";
            lbl_Track.Size = new Size(216, 15);
            lbl_Track.TabIndex = 453;
            lbl_Track.Text = "(Master) Track To-Change Section:";
            // 
            // txt_XMLPath
            // 
            txt_XMLPath.Location = new Point(1, 604);
            txt_XMLPath.Margin = new Padding(2);
            txt_XMLPath.Name = "txt_XMLPath";
            txt_XMLPath.Size = new Size(370, 23);
            txt_XMLPath.TabIndex = 454;
            txt_XMLPath.UseWaitCursor = true;
            // 
            // txt_Ttime
            // 
            txt_Ttime.Enabled = false;
            txt_Ttime.Location = new Point(378, 382);
            txt_Ttime.Margin = new Padding(2);
            txt_Ttime.Name = "txt_Ttime";
            txt_Ttime.Size = new Size(74, 23);
            txt_Ttime.TabIndex = 455;
            txt_Ttime.UseWaitCursor = true;
            txt_Ttime.Visible = false;
            // 
            // lbl_TempFolders
            // 
            lbl_TempFolders.AutoSize = true;
            lbl_TempFolders.Enabled = false;
            lbl_TempFolders.ForeColor = SystemColors.ControlText;
            lbl_TempFolders.Location = new Point(378, 403);
            lbl_TempFolders.Margin = new Padding(2, 0, 2, 0);
            lbl_TempFolders.Name = "lbl_TempFolders";
            lbl_TempFolders.Size = new Size(134, 15);
            lbl_TempFolders.TabIndex = 456;
            lbl_TempFolders.Text = "Avg time to adapt notes";
            lbl_TempFolders.UseWaitCursor = true;
            lbl_TempFolders.Visible = false;
            // 
            // txt_GP5
            // 
            txt_GP5.Location = new Point(1, 582);
            txt_GP5.Margin = new Padding(2);
            txt_GP5.Name = "txt_GP5";
            txt_GP5.Size = new Size(370, 23);
            txt_GP5.TabIndex = 457;
            txt_GP5.UseWaitCursor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Enabled = false;
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(317, 720);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(63, 15);
            label8.TabIndex = 465;
            label8.Text = "Correction";
            label8.Visible = false;
            // 
            // txt_Correction
            // 
            txt_Correction.CustomFormat = "hh:mm:ss";
            txt_Correction.Enabled = false;
            txt_Correction.Format = DateTimePickerFormat.Time;
            txt_Correction.Location = new Point(374, 717);
            txt_Correction.Margin = new Padding(2);
            txt_Correction.Name = "txt_Correction";
            txt_Correction.ShowUpDown = true;
            txt_Correction.Size = new Size(74, 23);
            txt_Correction.TabIndex = 463;
            txt_Correction.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            txt_Correction.Visible = false;
            // 
            // btn_Album2SortA
            // 
            btn_Album2SortA.Font = new Font("Microsoft Sans Serif", 6F);
            btn_Album2SortA.Location = new Point(230, 40);
            btn_Album2SortA.Margin = new Padding(2);
            btn_Album2SortA.Name = "btn_Album2SortA";
            btn_Album2SortA.Size = new Size(18, 18);
            btn_Album2SortA.TabIndex = 4;
            btn_Album2SortA.Text = "<";
            btn_Album2SortA.UseVisualStyleBackColor = true;
            btn_Album2SortA.UseWaitCursor = true;
            btn_Album2SortA.Click += btn_Album2SortA_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 6F);
            button1.Location = new Point(230, 64);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(18, 17);
            button1.TabIndex = 7;
            button1.Text = "<";
            button1.UseVisualStyleBackColor = true;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // xml_firstMili
            // 
            xml_firstMili.Location = new Point(416, 38);
            xml_firstMili.Margin = new Padding(2);
            xml_firstMili.Name = "xml_firstMili";
            xml_firstMili.Size = new Size(47, 23);
            xml_firstMili.TabIndex = 10;
            xml_firstMili.UseWaitCursor = true;
            // 
            // xml_lastMili
            // 
            xml_lastMili.Location = new Point(417, 62);
            xml_lastMili.Margin = new Padding(2);
            xml_lastMili.Name = "xml_lastMili";
            xml_lastMili.Size = new Size(46, 23);
            xml_lastMili.TabIndex = 12;
            xml_lastMili.UseWaitCursor = true;
            xml_lastMili.TextChanged += xml_lastMili_TextChanged;
            // 
            // txt_songLastMili
            // 
            txt_songLastMili.Location = new Point(192, 62);
            txt_songLastMili.Margin = new Padding(2);
            txt_songLastMili.Name = "txt_songLastMili";
            txt_songLastMili.Size = new Size(40, 23);
            txt_songLastMili.TabIndex = 6;
            txt_songLastMili.UseWaitCursor = true;
            // 
            // txt_songStartMili
            // 
            txt_songStartMili.Location = new Point(192, 38);
            txt_songStartMili.Margin = new Padding(2);
            txt_songStartMili.Name = "txt_songStartMili";
            txt_songStartMili.Size = new Size(40, 23);
            txt_songStartMili.TabIndex = 3;
            txt_songStartMili.UseWaitCursor = true;
            // 
            // txt_CorrectionMili
            // 
            txt_CorrectionMili.Enabled = false;
            txt_CorrectionMili.Location = new Point(276, 721);
            txt_CorrectionMili.Margin = new Padding(2);
            txt_CorrectionMili.Name = "txt_CorrectionMili";
            txt_CorrectionMili.Size = new Size(37, 23);
            txt_CorrectionMili.TabIndex = 479;
            txt_CorrectionMili.UseWaitCursor = true;
            txt_CorrectionMili.Visible = false;
            // 
            // btn_addcorrection
            // 
            btn_addcorrection.Enabled = false;
            btn_addcorrection.Font = new Font("Microsoft Sans Serif", 9F);
            btn_addcorrection.Location = new Point(360, 719);
            btn_addcorrection.Margin = new Padding(2);
            btn_addcorrection.Name = "btn_addcorrection";
            btn_addcorrection.Size = new Size(18, 20);
            btn_addcorrection.TabIndex = 474;
            btn_addcorrection.Text = "^";
            btn_addcorrection.UseVisualStyleBackColor = true;
            btn_addcorrection.UseWaitCursor = true;
            btn_addcorrection.Visible = false;
            btn_addcorrection.Click += button1_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Microsoft Sans Serif", 6F);
            button4.Location = new Point(247, 64);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(18, 17);
            button4.TabIndex = 8;
            button4.Text = "0";
            button4.UseVisualStyleBackColor = true;
            button4.UseWaitCursor = true;
            button4.Click += button4_Click;
            // 
            // btn_RestoreXML
            // 
            btn_RestoreXML.ForeColor = Color.Green;
            btn_RestoreXML.Location = new Point(286, 208);
            btn_RestoreXML.Margin = new Padding(2);
            btn_RestoreXML.Name = "btn_RestoreXML";
            btn_RestoreXML.Size = new Size(86, 55);
            btn_RestoreXML.TabIndex = 22;
            btn_RestoreXML.Text = "RestoreXML";
            btn_RestoreXML.UseVisualStyleBackColor = true;
            btn_RestoreXML.Click += btn_RestoreXML_Click;
            // 
            // cmb_Sections
            // 
            cmb_Sections.DropDownWidth = 800;
            cmb_Sections.FormattingEnabled = true;
            cmb_Sections.Location = new Point(1, 16);
            cmb_Sections.Margin = new Padding(0);
            cmb_Sections.MaxDropDownItems = 100;
            cmb_Sections.Name = "cmb_Sections";
            cmb_Sections.Size = new Size(307, 23);
            cmb_Sections.TabIndex = 1;
            cmb_Sections.SelectedIndexChanged += cmb_Sections_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(0, 266);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(167, 15);
            label1.TabIndex = 488;
            label1.Text = "Other Tracks available for sync";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = SystemColors.ControlText;
            label13.Location = new Point(292, 378);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(66, 15);
            label13.TabIndex = 490;
            label13.Text = "Comments";
            // 
            // txt_Description
            // 
            txt_Description.Location = new Point(0, 376);
            txt_Description.Margin = new Padding(2);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(370, 102);
            txt_Description.TabIndex = 24;
            txt_Description.Text = "";
            // 
            // btn_Save
            // 
            btn_Save.ForeColor = Color.Green;
            btn_Save.Location = new Point(377, 480);
            btn_Save.Margin = new Padding(2);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(82, 26);
            btn_Save.TabIndex = 491;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_Distrib
            // 
            btn_Distrib.ForeColor = Color.Green;
            btn_Distrib.Location = new Point(372, 282);
            btn_Distrib.Margin = new Padding(2);
            btn_Distrib.Name = "btn_Distrib";
            btn_Distrib.Size = new Size(90, 43);
            btn_Distrib.TabIndex = 492;
            btn_Distrib.Text = "Align to Other Tracks";
            btn_Distrib.UseVisualStyleBackColor = true;
            btn_Distrib.Click += btn_Distrib_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(292, 486);
            label14.Margin = new Padding(2, 0, 2, 0);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 494;
            label14.Text = "Sections";
            // 
            // txt_Sections
            // 
            txt_Sections.Location = new Point(1, 479);
            txt_Sections.Margin = new Padding(2);
            txt_Sections.Name = "txt_Sections";
            txt_Sections.Size = new Size(370, 102);
            txt_Sections.TabIndex = 493;
            txt_Sections.Text = "";
            // 
            // btn_MoveAllNotesAfter
            // 
            btn_MoveAllNotesAfter.BackColor = Color.LightSteelBlue;
            btn_MoveAllNotesAfter.Font = new Font("Microsoft Sans Serif", 8.25F);
            btn_MoveAllNotesAfter.Location = new Point(312, 1);
            btn_MoveAllNotesAfter.Margin = new Padding(0);
            btn_MoveAllNotesAfter.Name = "btn_MoveAllNotesAfter";
            btn_MoveAllNotesAfter.Size = new Size(150, 38);
            btn_MoveAllNotesAfter.TabIndex = 495;
            btn_MoveAllNotesAfter.Text = "Move Notes";
            btn_MoveAllNotesAfter.UseVisualStyleBackColor = false;
            btn_MoveAllNotesAfter.Click += button3_Click;
            // 
            // btn_backup
            // 
            btn_backup.Font = new Font("Calibri", 9F);
            btn_backup.ForeColor = Color.DodgerBlue;
            btn_backup.Location = new Point(373, 208);
            btn_backup.Margin = new Padding(2);
            btn_backup.Name = "btn_backup";
            btn_backup.Size = new Size(86, 27);
            btn_backup.TabIndex = 498;
            btn_backup.Text = "Backup";
            btn_backup.UseVisualStyleBackColor = true;
            btn_backup.Click += btn_backup_Click;
            // 
            // btn_Close
            // 
            btn_Close.BackColor = Color.LightSteelBlue;
            btn_Close.Font = new Font("Microsoft Sans Serif", 8.25F);
            btn_Close.Location = new Point(373, 233);
            btn_Close.Margin = new Padding(0);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(88, 30);
            btn_Close.TabIndex = 499;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click_3;
            // 
            // btn_CleanDescr
            // 
            btn_CleanDescr.Font = new Font("Microsoft Sans Serif", 6.6F);
            btn_CleanDescr.Location = new Point(377, 439);
            btn_CleanDescr.Margin = new Padding(2);
            btn_CleanDescr.Name = "btn_CleanDescr";
            btn_CleanDescr.Size = new Size(78, 20);
            btn_CleanDescr.TabIndex = 502;
            btn_CleanDescr.Text = "Clean";
            btn_CleanDescr.UseVisualStyleBackColor = true;
            btn_CleanDescr.UseWaitCursor = true;
            btn_CleanDescr.Click += btn_CleanDescr_Click;
            // 
            // btn_CleanStartSong
            // 
            btn_CleanStartSong.Font = new Font("Microsoft Sans Serif", 6F);
            btn_CleanStartSong.Location = new Point(247, 40);
            btn_CleanStartSong.Margin = new Padding(2);
            btn_CleanStartSong.Name = "btn_CleanStartSong";
            btn_CleanStartSong.Size = new Size(18, 18);
            btn_CleanStartSong.TabIndex = 504;
            btn_CleanStartSong.Text = "0";
            btn_CleanStartSong.UseVisualStyleBackColor = true;
            btn_CleanStartSong.UseWaitCursor = true;
            btn_CleanStartSong.Click += btn_CleanStartSong_Click;
            // 
            // chbx_removehandshapes
            // 
            chbx_removehandshapes.AutoSize = true;
            chbx_removehandshapes.Checked = true;
            chbx_removehandshapes.CheckState = CheckState.Checked;
            chbx_removehandshapes.Location = new Point(166, 264);
            chbx_removehandshapes.Margin = new Padding(2);
            chbx_removehandshapes.Name = "chbx_removehandshapes";
            chbx_removehandshapes.Size = new Size(292, 19);
            chbx_removehandshapes.TabIndex = 518;
            chbx_removehandshapes.Text = "Remove handhapes (in case shadow notes appear)";
            chbx_removehandshapes.UseVisualStyleBackColor = true;
            chbx_removehandshapes.CheckedChanged += chbx_removehandshapes_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label22);
            groupBox1.Controls.Add(label21);
            groupBox1.Controls.Add(txt_P3LNMili);
            groupBox1.Controls.Add(txt_P3LN);
            groupBox1.Controls.Add(label19);
            groupBox1.Controls.Add(txt_P3);
            groupBox1.Controls.Add(label17);
            groupBox1.Controls.Add(btn_ApplyP3);
            groupBox1.Controls.Add(btn_AddSections);
            groupBox1.Location = new Point(2, 186);
            groupBox1.Margin = new Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2);
            groupBox1.Size = new Size(285, 74);
            groupBox1.TabIndex = 519;
            groupBox1.TabStop = false;
            groupBox1.Text = "3rd type of Calculation";
            // 
            // label22
            // 
            label22.ForeColor = SystemColors.ControlText;
            label22.Location = new Point(201, 41);
            label22.Margin = new Padding(2, 0, 2, 0);
            label22.Name = "label22";
            label22.Size = new Size(25, 15);
            label22.TabIndex = 527;
            label22.Text = "ms";
            // 
            // label21
            // 
            label21.ForeColor = SystemColors.ControlText;
            label21.Location = new Point(209, 18);
            label21.Margin = new Padding(2, 0, 2, 0);
            label21.Name = "label21";
            label21.Size = new Size(14, 15);
            label21.TabIndex = 526;
            label21.Text = "%";
            // 
            // txt_P3LNMili
            // 
            txt_P3LNMili.Enabled = false;
            txt_P3LNMili.Location = new Point(164, 37);
            txt_P3LNMili.Margin = new Padding(2);
            txt_P3LNMili.Name = "txt_P3LNMili";
            txt_P3LNMili.Size = new Size(38, 23);
            txt_P3LNMili.TabIndex = 524;
            txt_P3LNMili.UseWaitCursor = true;
            // 
            // txt_P3LN
            // 
            txt_P3LN.CustomFormat = "hh:mm:ss";
            txt_P3LN.Enabled = false;
            txt_P3LN.Format = DateTimePickerFormat.Time;
            txt_P3LN.Location = new Point(97, 37);
            txt_P3LN.Margin = new Padding(2);
            txt_P3LN.Name = "txt_P3LN";
            txt_P3LN.ShowUpDown = true;
            txt_P3LN.Size = new Size(67, 23);
            txt_P3LN.TabIndex = 522;
            txt_P3LN.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // label19
            // 
            label19.ForeColor = SystemColors.ControlText;
            label19.Location = new Point(3, 37);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(90, 38);
            label19.TabIndex = 521;
            label19.Text = "Estimated XML LastNote";
            label19.Click += label19_Click;
            // 
            // txt_P3
            // 
            txt_P3.Enabled = false;
            txt_P3.Location = new Point(169, 14);
            txt_P3.Margin = new Padding(2);
            txt_P3.Name = "txt_P3";
            txt_P3.Size = new Size(45, 23);
            txt_P3.TabIndex = 519;
            txt_P3.UseWaitCursor = true;
            // 
            // label17
            // 
            label17.ForeColor = SystemColors.ControlText;
            label17.Location = new Point(3, 15);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new Size(180, 16);
            label17.TabIndex = 520;
            label17.Text = "Space between-notes 2change";
            // 
            // btn_ApplyP3
            // 
            btn_ApplyP3.BackColor = Color.DarkTurquoise;
            btn_ApplyP3.Enabled = false;
            btn_ApplyP3.Font = new Font("Microsoft Sans Serif", 8.25F);
            btn_ApplyP3.Location = new Point(225, 14);
            btn_ApplyP3.Margin = new Padding(0);
            btn_ApplyP3.Name = "btn_ApplyP3";
            btn_ApplyP3.Size = new Size(58, 42);
            btn_ApplyP3.TabIndex = 516;
            btn_ApplyP3.Text = "Apply P3";
            btn_ApplyP3.UseVisualStyleBackColor = false;
            btn_ApplyP3.Visible = false;
            btn_ApplyP3.Click += btn_ApplyP3_Click;
            // 
            // btn_AddSections
            // 
            btn_AddSections.Font = new Font("Microsoft Sans Serif", 6F);
            btn_AddSections.Location = new Point(274, 90);
            btn_AddSections.Margin = new Padding(2);
            btn_AddSections.Name = "btn_AddSections";
            btn_AddSections.Size = new Size(18, 18);
            btn_AddSections.TabIndex = 328;
            btn_AddSections.Text = "+";
            btn_AddSections.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.ForeColor = SystemColors.ControlText;
            label20.Location = new Point(240, 717);
            label20.Margin = new Padding(2, 0, 2, 0);
            label20.Name = "label20";
            label20.Size = new Size(25, 15);
            label20.TabIndex = 525;
            label20.Text = "ms";
            label20.Visible = false;
            // 
            // txt_P3FNMili
            // 
            txt_P3FNMili.Enabled = false;
            txt_P3FNMili.Location = new Point(192, 717);
            txt_P3FNMili.Margin = new Padding(2);
            txt_P3FNMili.Name = "txt_P3FNMili";
            txt_P3FNMili.Size = new Size(38, 23);
            txt_P3FNMili.TabIndex = 523;
            txt_P3FNMili.UseWaitCursor = true;
            txt_P3FNMili.Visible = false;
            // 
            // txt_P3FN
            // 
            txt_P3FN.CustomFormat = "hh:mm:ss";
            txt_P3FN.Enabled = false;
            txt_P3FN.Format = DateTimePickerFormat.Time;
            txt_P3FN.Location = new Point(118, 719);
            txt_P3FN.Margin = new Padding(2);
            txt_P3FN.Name = "txt_P3FN";
            txt_P3FN.ShowUpDown = true;
            txt_P3FN.Size = new Size(67, 23);
            txt_P3FN.TabIndex = 518;
            txt_P3FN.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            txt_P3FN.Visible = false;
            // 
            // label18
            // 
            label18.ForeColor = SystemColors.ControlText;
            label18.Location = new Point(0, 724);
            label18.Margin = new Padding(2, 0, 2, 0);
            label18.Name = "label18";
            label18.Size = new Size(116, 16);
            label18.TabIndex = 517;
            label18.Text = "P3 XML 2ndNote";
            label18.Visible = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txt_RealLastNoteMili);
            groupBox2.Controls.Add(txt_RealLastNote);
            groupBox2.Controls.Add(txt_Percentage);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(btn_ApplyP1);
            groupBox2.Controls.Add(button3);
            groupBox2.Location = new Point(4, 775);
            groupBox2.Margin = new Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2);
            groupBox2.Size = new Size(278, 64);
            groupBox2.TabIndex = 525;
            groupBox2.TabStop = false;
            groupBox2.Text = "1st type of Calculation (old2decomm)";
            // 
            // txt_RealLastNoteMili
            // 
            txt_RealLastNoteMili.Enabled = false;
            txt_RealLastNoteMili.Location = new Point(180, 38);
            txt_RealLastNoteMili.Margin = new Padding(2);
            txt_RealLastNoteMili.Name = "txt_RealLastNoteMili";
            txt_RealLastNoteMili.Size = new Size(36, 23);
            txt_RealLastNoteMili.TabIndex = 503;
            txt_RealLastNoteMili.UseWaitCursor = true;
            // 
            // txt_RealLastNote
            // 
            txt_RealLastNote.CustomFormat = "hh:mm:ss";
            txt_RealLastNote.Enabled = false;
            txt_RealLastNote.Format = DateTimePickerFormat.Time;
            txt_RealLastNote.Location = new Point(116, 37);
            txt_RealLastNote.Margin = new Padding(2);
            txt_RealLastNote.Name = "txt_RealLastNote";
            txt_RealLastNote.ShowUpDown = true;
            txt_RealLastNote.Size = new Size(66, 23);
            txt_RealLastNote.TabIndex = 502;
            txt_RealLastNote.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // txt_Percentage
            // 
            txt_Percentage.Enabled = false;
            txt_Percentage.Location = new Point(132, 14);
            txt_Percentage.Margin = new Padding(2);
            txt_Percentage.Name = "txt_Percentage";
            txt_Percentage.Size = new Size(84, 23);
            txt_Percentage.TabIndex = 498;
            txt_Percentage.UseWaitCursor = true;
            // 
            // label9
            // 
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(5, 17);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(132, 16);
            label9.TabIndex = 501;
            label9.Text = "Percentage(Cur-Exp) P1";
            // 
            // label6
            // 
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(5, 40);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(108, 16);
            label6.TabIndex = 500;
            label6.Text = "New XML LastNote";
            // 
            // btn_ApplyP1
            // 
            btn_ApplyP1.BackColor = SystemColors.Control;
            btn_ApplyP1.Enabled = false;
            btn_ApplyP1.Font = new Font("Microsoft Sans Serif", 8.25F);
            btn_ApplyP1.Location = new Point(216, 14);
            btn_ApplyP1.Margin = new Padding(0);
            btn_ApplyP1.Name = "btn_ApplyP1";
            btn_ApplyP1.Size = new Size(62, 46);
            btn_ApplyP1.TabIndex = 499;
            btn_ApplyP1.Text = "Apply P1";
            toolTip1.SetToolTip(btn_ApplyP1, "Alternative way to calculate lenght. DISABLED as not accurate");
            btn_ApplyP1.UseVisualStyleBackColor = false;
            btn_ApplyP1.Visible = false;
            btn_ApplyP1.Click += btn_Apply_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft Sans Serif", 6F);
            button3.Location = new Point(274, 90);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(18, 18);
            button3.TabIndex = 328;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txt_corr);
            groupBox3.Controls.Add(btn_ApplyP2);
            groupBox3.Controls.Add(txt_PercTime);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(button5);
            groupBox3.Location = new Point(286, 775);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(176, 63);
            groupBox3.TabIndex = 526;
            groupBox3.TabStop = false;
            groupBox3.Text = "2nd type (old2decomm)";
            // 
            // txt_corr
            // 
            txt_corr.Enabled = false;
            txt_corr.Font = new Font("Calibri", 8F);
            txt_corr.Location = new Point(147, 22);
            txt_corr.Margin = new Padding(2);
            txt_corr.Name = "txt_corr";
            txt_corr.Size = new Size(28, 21);
            txt_corr.TabIndex = 511;
            txt_corr.UseWaitCursor = true;
            // 
            // btn_ApplyP2
            // 
            btn_ApplyP2.BackColor = SystemColors.Control;
            btn_ApplyP2.Enabled = false;
            btn_ApplyP2.Font = new Font("Microsoft Sans Serif", 8.25F);
            btn_ApplyP2.Location = new Point(8, 40);
            btn_ApplyP2.Margin = new Padding(0);
            btn_ApplyP2.Name = "btn_ApplyP2";
            btn_ApplyP2.Size = new Size(165, 21);
            btn_ApplyP2.TabIndex = 510;
            btn_ApplyP2.Text = "Apply P2";
            toolTip1.SetToolTip(btn_ApplyP2, "Alternative way to calculate lenght. DISABLED as not accurate");
            btn_ApplyP2.UseVisualStyleBackColor = false;
            btn_ApplyP2.Visible = false;
            btn_ApplyP2.Click += btn_P2_Click;
            // 
            // txt_PercTime
            // 
            txt_PercTime.Enabled = false;
            txt_PercTime.Location = new Point(122, 20);
            txt_PercTime.Margin = new Padding(2);
            txt_PercTime.Name = "txt_PercTime";
            txt_PercTime.Size = new Size(35, 23);
            txt_PercTime.TabIndex = 508;
            txt_PercTime.UseWaitCursor = true;
            // 
            // label15
            // 
            label15.ForeColor = SystemColors.ControlText;
            label15.Location = new Point(6, 20);
            label15.Margin = new Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new Size(116, 16);
            label15.TabIndex = 509;
            label15.Text = "Percentage(Time) P2";
            // 
            // button5
            // 
            button5.Font = new Font("Microsoft Sans Serif", 6F);
            button5.Location = new Point(274, 90);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(18, 18);
            button5.TabIndex = 328;
            button5.Text = "+";
            button5.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txt_DiffLenght);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(txt_Lastnote);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(txt_ExpctLenght);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(txt_CurrentLenght);
            groupBox4.Controls.Add(txt_MaxRealLenght);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label7);
            groupBox4.Location = new Point(2, 118);
            groupBox4.Margin = new Padding(2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(2);
            groupBox4.Size = new Size(459, 68);
            groupBox4.TabIndex = 526;
            groupBox4.TabStop = false;
            groupBox4.Text = "General stats";
            // 
            // txt_DiffLenght
            // 
            txt_DiffLenght.Enabled = false;
            txt_DiffLenght.Location = new Point(402, 20);
            txt_DiffLenght.Margin = new Padding(2);
            txt_DiffLenght.Name = "txt_DiffLenght";
            txt_DiffLenght.Size = new Size(52, 23);
            txt_DiffLenght.TabIndex = 515;
            txt_DiffLenght.UseWaitCursor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.ForeColor = SystemColors.ControlText;
            label16.Location = new Point(363, 24);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new Size(77, 15);
            label16.TabIndex = 516;
            label16.Text = "Diff (cur-exp)";
            toolTip1.SetToolTip(label16, "curent-expected");
            // 
            // txt_Lastnote
            // 
            txt_Lastnote.Enabled = false;
            txt_Lastnote.Location = new Point(324, 44);
            txt_Lastnote.Margin = new Padding(2);
            txt_Lastnote.Name = "txt_Lastnote";
            txt_Lastnote.Size = new Size(130, 23);
            txt_Lastnote.TabIndex = 510;
            txt_Lastnote.UseWaitCursor = true;
            // 
            // label12
            // 
            label12.ForeColor = SystemColors.ControlText;
            label12.Location = new Point(268, 47);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(58, 16);
            label12.TabIndex = 514;
            label12.Text = "Last Note";
            // 
            // txt_ExpctLenght
            // 
            txt_ExpctLenght.Enabled = false;
            txt_ExpctLenght.Location = new Point(114, 20);
            txt_ExpctLenght.Margin = new Padding(2);
            txt_ExpctLenght.Name = "txt_ExpctLenght";
            txt_ExpctLenght.Size = new Size(100, 23);
            txt_ExpctLenght.TabIndex = 507;
            txt_ExpctLenght.UseWaitCursor = true;
            // 
            // label11
            // 
            label11.ForeColor = SystemColors.ControlText;
            label11.Location = new Point(4, 18);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(96, 16);
            label11.TabIndex = 513;
            label11.Text = "Expected Lenght";
            // 
            // txt_CurrentLenght
            // 
            txt_CurrentLenght.Enabled = false;
            txt_CurrentLenght.Location = new Point(324, 22);
            txt_CurrentLenght.Margin = new Padding(2);
            txt_CurrentLenght.Name = "txt_CurrentLenght";
            txt_CurrentLenght.Size = new Size(40, 23);
            txt_CurrentLenght.TabIndex = 509;
            txt_CurrentLenght.UseWaitCursor = true;
            // 
            // txt_MaxRealLenght
            // 
            txt_MaxRealLenght.Enabled = false;
            txt_MaxRealLenght.Location = new Point(114, 44);
            txt_MaxRealLenght.Margin = new Padding(2);
            txt_MaxRealLenght.Name = "txt_MaxRealLenght";
            txt_MaxRealLenght.Size = new Size(100, 23);
            txt_MaxRealLenght.TabIndex = 508;
            txt_MaxRealLenght.UseWaitCursor = true;
            // 
            // label10
            // 
            label10.ForeColor = SystemColors.ControlText;
            label10.Location = new Point(216, 22);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(112, 16);
            label10.TabIndex = 512;
            label10.Text = "Curent XML Lenght";
            // 
            // label7
            // 
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(4, 42);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(93, 16);
            label7.TabIndex = 511;
            label7.Text = "Max real Lenght";
            // 
            // btn_SyncPh2Sect
            // 
            btn_SyncPh2Sect.ForeColor = Color.Green;
            btn_SyncPh2Sect.Location = new Point(375, 507);
            btn_SyncPh2Sect.Margin = new Padding(2);
            btn_SyncPh2Sect.Name = "btn_SyncPh2Sect";
            btn_SyncPh2Sect.Size = new Size(88, 54);
            btn_SyncPh2Sect.TabIndex = 527;
            btn_SyncPh2Sect.Text = "Sync Phrases to Sections (xx/yy)";
            btn_SyncPh2Sect.UseVisualStyleBackColor = true;
            btn_SyncPh2Sect.Click += btn_SyncPh2Sect_Click;
            // 
            // btn_Reload
            // 
            btn_Reload.Font = new Font("Calibri", 9F);
            btn_Reload.ForeColor = Color.DodgerBlue;
            btn_Reload.Location = new Point(373, 181);
            btn_Reload.Margin = new Padding(2);
            btn_Reload.Name = "btn_Reload";
            btn_Reload.Size = new Size(86, 27);
            btn_Reload.TabIndex = 528;
            btn_Reload.Text = "Reload";
            btn_Reload.UseVisualStyleBackColor = true;
            btn_Reload.Click += btn_Reload_Click;
            // 
            // chbx_IgnorePhaseSync
            // 
            chbx_IgnorePhaseSync.Location = new Point(3, 627);
            chbx_IgnorePhaseSync.Margin = new Padding(2);
            chbx_IgnorePhaseSync.Name = "chbx_IgnorePhaseSync";
            chbx_IgnorePhaseSync.Size = new Size(217, 18);
            chbx_IgnorePhaseSync.TabIndex = 533;
            chbx_IgnorePhaseSync.Text = "Ignore Phase vs Section Sync check";
            toolTip1.SetToolTip(chbx_IgnorePhaseSync, "Copy folder to remote location");
            chbx_IgnorePhaseSync.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.Location = new Point(217, 628);
            checkBox1.Margin = new Padding(2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(217, 18);
            checkBox1.TabIndex = 534;
            checkBox1.Text = "Display Done Mss";
            toolTip1.SetToolTip(checkBox1, "Copy folder to remote location");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btn_VisualDistrib
            // 
            btn_VisualDistrib.ForeColor = Color.Green;
            btn_VisualDistrib.Location = new Point(373, 329);
            btn_VisualDistrib.Margin = new Padding(2);
            btn_VisualDistrib.Name = "btn_VisualDistrib";
            btn_VisualDistrib.Size = new Size(90, 43);
            btn_VisualDistrib.TabIndex = 529;
            btn_VisualDistrib.Text = "Visually align/Section";
            btn_VisualDistrib.UseVisualStyleBackColor = true;
            btn_VisualDistrib.Visible = false;
            // 
            // txt_Backup
            // 
            txt_Backup.Enabled = false;
            txt_Backup.Location = new Point(286, 183);
            txt_Backup.Margin = new Padding(2);
            txt_Backup.Name = "txt_Backup";
            txt_Backup.Size = new Size(86, 23);
            txt_Backup.TabIndex = 517;
            txt_Backup.UseWaitCursor = true;
            // 
            // label23
            // 
            label23.ForeColor = SystemColors.ControlText;
            label23.Location = new Point(211, 54);
            label23.Margin = new Padding(2, 0, 2, 0);
            label23.Name = "label23";
            label23.Size = new Size(25, 15);
            label23.TabIndex = 528;
            label23.Text = "ms";
            label23.Visible = false;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.ForeColor = SystemColors.ControlText;
            label24.Location = new Point(298, 285);
            label24.Margin = new Padding(2, 0, 2, 0);
            label24.Name = "label24";
            label24.Size = new Size(47, 15);
            label24.TabIndex = 530;
            label24.Text = "(Slaves)";
            // 
            // label25
            // 
            label25.ForeColor = SystemColors.ControlText;
            label25.Location = new Point(438, 54);
            label25.Margin = new Padding(2, 0, 2, 0);
            label25.Name = "label25";
            label25.Size = new Size(25, 15);
            label25.TabIndex = 531;
            label25.Text = "ms";
            label25.Visible = false;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 6.6F);
            button2.Location = new Point(378, 458);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(78, 20);
            button2.TabIndex = 532;
            button2.Text = "ReApply Last";
            button2.UseVisualStyleBackColor = true;
            button2.UseWaitCursor = true;
            // 
            // cmb_Tracks
            // 
            cmb_Tracks.CheckOnClick = true;
            cmb_Tracks.FormattingEnabled = true;
            cmb_Tracks.HorizontalScrollbar = true;
            cmb_Tracks.Location = new Point(0, 282);
            cmb_Tracks.Margin = new Padding(0);
            cmb_Tracks.Name = "cmb_Tracks";
            cmb_Tracks.Size = new Size(370, 94);
            cmb_Tracks.TabIndex = 23;
            cmb_Tracks.UseWaitCursor = true;
            // 
            // DistribXMLNotes
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(466, 655);
            Controls.Add(checkBox1);
            Controls.Add(chbx_IgnorePhaseSync);
            Controls.Add(button2);
            Controls.Add(label25);
            Controls.Add(label24);
            Controls.Add(label23);
            Controls.Add(txt_Backup);
            Controls.Add(btn_VisualDistrib);
            Controls.Add(label20);
            Controls.Add(btn_Reload);
            Controls.Add(btn_SyncPh2Sect);
            Controls.Add(txt_P3FNMili);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(txt_P3FN);
            Controls.Add(chbx_removehandshapes);
            Controls.Add(label18);
            Controls.Add(btn_CleanStartSong);
            Controls.Add(btn_CleanDescr);
            Controls.Add(btn_Close);
            Controls.Add(btn_backup);
            Controls.Add(btn_MoveAllNotesAfter);
            Controls.Add(label14);
            Controls.Add(txt_Sections);
            Controls.Add(btn_Distrib);
            Controls.Add(btn_Save);
            Controls.Add(label13);
            Controls.Add(txt_Description);
            Controls.Add(label1);
            Controls.Add(cmb_Tracks);
            Controls.Add(cmb_Sections);
            Controls.Add(btn_RestoreXML);
            Controls.Add(button4);
            Controls.Add(txt_CorrectionMili);
            Controls.Add(txt_songLastMili);
            Controls.Add(txt_songStartMili);
            Controls.Add(xml_lastMili);
            Controls.Add(xml_firstMili);
            Controls.Add(btn_addcorrection);
            Controls.Add(button1);
            Controls.Add(btn_Album2SortA);
            Controls.Add(label8);
            Controls.Add(txt_Correction);
            Controls.Add(txt_GP5);
            Controls.Add(txt_Ttime);
            Controls.Add(lbl_TempFolders);
            Controls.Add(txt_XMLPath);
            Controls.Add(lbl_Track);
            Controls.Add(chbx_BassToo);
            Controls.Add(btn_OpenXML);
            Controls.Add(btn_OpenGP5);
            Controls.Add(btn_EOF);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(xml_last);
            Controls.Add(xml_first);
            Controls.Add(txt_songLast);
            Controls.Add(txt_songStart);
            Controls.Add(lbl_Comments);
            Controls.Add(btn_Estimate);
            Margin = new Padding(2);
            Name = "DistribXMLNotes";
            Load += DistribXMLNotes_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //StopImport = true;
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
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

        public void DistribXMLNotes_Load(object sender, EventArgs e)
        {
            //var ud = ""; //var emt = false;

            xml_first.Value = DateTime.Parse("00:00:00");
            xml_last.Value = DateTime.Parse("00:00:00");
            txt_ExpctLenght.Text = "";
            txt_MaxRealLenght.Text = "";
            txt_Percentage.Text = "";
            txt_RealLastNote.Value = DateTime.Parse("00:00:00");
            txt_RealLastNoteMili.Text = "";
            txt_PercTime.Text = "";
            txt_XMLPath.Text = "";

            //Clean structures
            cmb_Tracks.DataSource = null;
            for (int i = cmb_Tracks.Items.Count - 1; i >= 0; --i)
                cmb_Tracks.Items.RemoveAt(i);
            cmb_Sections.DataSource = null;
            for (int i = cmb_Sections.Items.Count - 1; i >= 0; --i)
                cmb_Sections.Items.RemoveAt(i);

            var noOfRec = 0; var XMLFilePath = ""; var RouteMask = ""; string ArrangementType = ""; string CDLC_ID = "0"; string description = ""; string newXMLFilePath = "";
            var ArrangID = "0";
            //var i = databox.SelectedCells[0].RowIndex;
            string destination_dir = FilePath + (gPlatform is null ? "" : ((gPlatform.ToLower() == "XBOX360".ToLower() ? "\\Root" : "")));

            if (sXml != null)
            {
                ArrangID = sXml.Substring(sXml.IndexOf("_ArrangementID="), sXml.Length - sXml.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
                var DLCID = sXml.Substring(8, sXml.IndexOf("_") - 8);
                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName" +
                    ", RouteMask, Start_Time, ArrangementType, SNGFileName, CDLC_ID, Comments  FROM Arrangements WHERE ID=" + ArrangID + GetArrOfficSQLTxt(Arrangoff), "", cnb, cnc);
                noOfRec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
                XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                RouteMask = dus.Tables[0].Rows[0].ItemArray[2].ToString();
                ArrangementType = dus.Tables[0].Rows[0].ItemArray[4].ToString();
                CDLC_ID = dus.Tables[0].Rows[0].ItemArray[6].ToString();
                description = dus.Tables[0].Rows[0].ItemArray[7].ToString();
                newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
                txt_XMLPath.Text = XMLFilePath;
                if (File.Exists(XMLFilePath + ".old3")) { btn_RestoreXML.Enabled = true; txt_Backup.Text = "old3 backup OK"; }
                else { btn_RestoreXML.Enabled = false; ; txt_Backup.Text = "NOK backup"; }
            }
            else
            {
                try
                {
                    if (FilePath is null) return;
                    XMLFilePath = FilePath; txt_XMLPath.Text = XMLFilePath;
                    newXMLFilePath = FilePath;
                    if (File.Exists(XMLFilePath + ".old3")) { btn_RestoreXML.Enabled = true; txt_Backup.Text = "old3 backup OK"; }
                    else { btn_RestoreXML.Enabled = false; ; txt_Backup.Text = "NOK backup"; }

                    var xmlContents = Song2014.LoadFromFile(FilePath);
                    if (xmlContents is null)
                    {
                        //MessageBox.Show("issues at reading xml..please press restore/fix manually");
                        DialogResult result1 = MessageBox.Show("Do you want to restore?"
                            , "issues at reading xml..please press restore/fix manually", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result1 == DialogResult.No) return;
                        btn_RestoreXML_Click(null, null);

                        xmlContents = Song2014.LoadFromFile(FilePath);
                        if (xmlContents is null) { MessageBox.Show("issues at reading the restored xml..Pls fix externally and Try again!"); return; }
                    }

                    ArrangementType = null;
                    RouteMask = xmlContents.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContents.Arrangement.ToString()
                        : xmlContents.ArrangementProperties.RouteMask.ToString();
                    var xmlBackContent = Song2014.LoadFromFile(FilePath+".old3");
                    var bkp = "";
                    if (xmlBackContent is not null) bkp = xmlContents.OptionalProperties is null?"":xmlBackContent.OptionalProperties.JapaneseArtistName.ToString();

                        description = xmlContents.OptionalProperties is null ? bkp : xmlContents.OptionalProperties.JapaneseArtistName.ToString();
                }
                catch (Exception Exx)
                {
                    var timestamp = UpdateLog(DateTime.Now, "Distributing notes Load issues: "+ Exx.Message, false, c("dlcm_TempPath"), "", "", null, null);
                }
                XMLFilePath = FilePath;
                newXMLFilePath = FilePath;

            }
            txt_Description.Text = description.Replace("apply", "apply\n");

            txt_XMLPath.Text = newXMLFilePath;
            if (File.Exists(txt_XMLPath.Text + ".old3")) { btn_RestoreXML.Enabled = true; txt_Backup.Text = "old3 backup OK"; }
            else { btn_RestoreXML.Enabled = false; ; txt_Backup.Text = "NOK backup"; }

            string tx = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, false, -1, -1);// dus.Tables[0].Rows[0].ItemArray[3].ToString());
            string ty = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, true, -1, -1);
            txt_Lastnote.Text = ty;
            //xml_first.Value = xml_first.Value.AddHours(-12);
            xml_first.Value = xml_first.Value.AddSeconds(Math.Truncate(float.Parse(tx)));// ToString(@"hh\:mm\:ss\:fff"); ;
            xml_last.Value = xml_last.Value.AddSeconds(Math.Truncate(float.Parse(ty)));/// DateTime.Parse(GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, true));
            xml_first.Value = xml_first.Value.AddMilliseconds(float.Parse(tx) - Math.Truncate(float.Parse(tx)));
            xml_last.Value = xml_last.Value.AddMilliseconds(float.Parse(ty) - Math.Truncate(float.Parse(ty)));
            xml_firstMili.Text = Math.Round((float.Parse(tx) - Math.Truncate(float.Parse(tx))), 3).ToString();
            xml_lastMili.Text = Math.Round((float.Parse(ty) - Math.Truncate(float.Parse(ty))), 3).ToString();
            txt_CurrentLenght.Text = Math.Round((xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3)
                - xml_first.Value.Hour * 3600 - xml_first.Value.Minute * 60 - xml_first.Value.Second - Math.Round(float.Parse(xml_firstMili.Text), 3)), 3).ToString();
            if (File.Exists(BasedOn_CF)) txt_GP5.Text = BasedOn_CF;
            else txt_GP5.Text = GetGPfile();

            txt_Sections.Text = "";
            cmb_Sections = GenerateSectionsList(newXMLFilePath, cmb_Sections, tx, ty);//Add Sections

            var cmd = "SELECT XMLFilePath, XMLFileName" +
                ", RouteMask, Start_Time, ArrangementType, SNGFileName, CDLC_ID  FROM Arrangements" +
                " WHERE CDLC_ID=" + CDLC_ID + " AND ID<>" + ArrangID + GetArrOfficSQLTxt(Arrangoff) + ";";
            DataSet dis = new DataSet(); dis = SelectFromDB("Arrangements", cmd, "", cnb, cnc);
            noOfRec = GetNoRec(dis, cnb, cnc);
            for (int k = 0; k < noOfRec; k++)
            {
                var XMLFileName = dis.Tables[0].Rows[k][1].ToString();
                var RouteMasks = dis.Tables[0].Rows[k][2].ToString();
                var Start_Time = dis.Tables[0].Rows[k][3].ToString();
                cmb_Tracks.Items.Add(XMLFileName + " - " + RouteMasks + " - " + Start_Time);
            }
            if (noOfRec == 0)
            {
                if (sXml != null) { btn_Distrib.Enabled = false; cmb_Tracks.Items.Add("Lonely XML"); cmb_Tracks.Enabled = false; }
                else
                {
                    var xmlz = Directory.GetFiles(Path.GetDirectoryName(FilePath), "*.xml", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (var xml in xmlz) if (xml != FilePath) cmb_Tracks.Items.Add(Path.GetFileName(xml));
                }
            }
            else { btn_Distrib.Enabled = true; cmb_Tracks.Enabled = true; }

            //List Sections
            var xmlContent = Song2014.LoadFromFile(txt_XMLPath.Text);
            if (xmlContent.Sections is not null)
                for (var j = 0; j < xmlContent.Sections.Length; j++)
                    txt_Sections.Text += xmlContent.Sections[j].Name + "=" + xmlContent.Sections[j].StartTime + ";\n";
            //checkif no sections is the same as no of phrases
            if (xmlContent.PhraseIterations.Length != xmlContent.Sections.Length) SaveSectionAndIntDescription();
            this.Text = Path.GetFileName(txt_XMLPath.Text + " with " + cmb_Sections.Items.Count + " sections.");
        }

        public ComboBox GenerateSectionsList(string newXMLFilePath, ComboBox cmb_Sections, string firstnote, string lastnote)
        {
            if (EoFPath == "") return cmb_Sections;
            Song2014 xmlContent = null; var v = "";
            xmlContent = Song2014.LoadFromFile(newXMLFilePath);
            cmb_Sections.Items.Add(""); float str = -1; float nd = -1;
            bool phrasediffthansect = false;
            if (newXMLFilePath != "" && xmlContent.Sections is not null)
            {
                if (float.Parse(xmlContent.Sections[0].StartTime.ToString()) > float.Parse(firstnote))
                {
                    var st = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, false, str, -1);
                    var zt = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, true, float.Parse(xmlContent.Sections[0].StartTime.ToString()), -1);
                    cmb_Sections.Items.Add("firstnote: " + st + " - endnote: " + zt + " - blank - " + firstnote + "  -FirstSection (Not marked as a section) - First");
                }
                for (var j = 0; j < xmlContent.Sections.Length; j++)
                {
                    str = float.Parse(xmlContent.Sections[j].StartTime.ToString());

                    if (j < xmlContent.Sections.Length - 1)
                        nd = float.Parse(xmlContent.Sections[j + 1].StartTime.ToString());
                    else
                        nd = float.Parse(lastnote);
                    var st = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, false, str, -1);
                    var zt = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, true, nd, -1);

                    v = "firstnote: " + st + " - endnote: " + zt + " - no_order: " + j + " - start_time: " + xmlContent.Sections[j].StartTime + " - sec_name: " + xmlContent.Sections[j].Name
                        + " - sec_no: " + xmlContent.Sections[j].Number;
                    cmb_Sections.Items.Add(v);//add items

                    if (xmlContent.Sections.Length < xmlContent.Phrases.Length)
                        if (xmlContent.Sections[j].StartTime != xmlContent.PhraseIterations[j].Time) phrasediffthansect = true;
                }
            }

            if (xmlContent.Sections.Length != xmlContent.Phrases.Length) phrasediffthansect = true;

            cmb_Sections.Text = "";
            btn_Album2SortA_Click(null, null);
            button1_Click(null, null);
            btn_SyncPh2Sect.Text = btn_SyncPh2Sect.Text.Replace("xx", xmlContent.Sections.Length.ToString());
            btn_SyncPh2Sect.Text = btn_SyncPh2Sect.Text.Replace("yy", xmlContent.Phrases.Length.ToString());
            if (phrasediffthansect) btn_SyncPh2Sect.Enabled = true;
            return cmb_Sections;
        }

        public string GetGPfile()
        {
            if (EoFPath == "") return "";
            if (!File.Exists(EoFPath)) return "";
            var templateList = Directory.EnumerateFiles(System.IO.Path.GetDirectoryName(EoFPath));
            var files = "";
            foreach (var template in templateList)
            {
                if (System.IO.Path.GetExtension(template).ToLower() == ".gp" || Path.GetExtension(template).ToLower() == ".gpx" || Path.GetExtension(template).ToLower() == ".gp5")
                {
                    files += template + ",";
                }
            }
            return files;
        }

        public string calctime(string p)
        {
            //var files = "";
            if (xml_lastMili.Text == "") xml_lastMili.Text = "0";
            if (xml_firstMili.Text == "") xml_firstMili.Text = "0";
            if (txt_songStartMili.Text == "") txt_songStartMili.Text = "0";
            if (txt_songLastMili.Text == "") txt_songLastMili.Text = "0";
            if (txt_CorrectionMili.Text == "") txt_CorrectionMili.Text = "0";
            txt_ExpctLenght.Text =
                Math.Round(
                    (txt_songLast.Value.Minute * 60 + txt_songLast.Value.Second + Math.Round(float.Parse(txt_songLastMili.Text), 3))
                - (txt_songStart.Value.Minute * 60 + txt_songStart.Value.Second + Math.Round(float.Parse(txt_songStartMili.Text), 3))
                , 3).ToString();
            var perc = Math.Round((float.Parse(txt_ExpctLenght.Text) * 100 / float.Parse(txt_CurrentLenght.Text)), 3);
            //perc = float.Parse(txt_ExpctLenght.Text) > float.Parse(txt_CurrentLenght.Text) ? -perc : perc;
            //var ttz = GetTrackStartTime(arg.SongXml.File, arg.RouteMask.ToString(), arg.ArrangementType.ToString(), false);

            var tzz = txt_Lastnote.Text;
            txt_MaxRealLenght.Text = Math.Round((perc * (float.Parse(tzz)) / 100), 3).ToString();//float.Parse(txt_CurrentLenght.Text) - 0
            txt_RealLastNote.Value = DateTime.Parse("00:00:00");
            var d = Math.Round(txt_songLast.Value.Minute * 60 + txt_songLast.Value.Second + Math.Round(float.Parse(txt_songLastMili.Text), 3), 3);
            d = d * perc / 100;//d+ d
            txt_RealLastNote.Value = txt_RealLastNote.Value.AddSeconds(d);
            txt_RealLastNoteMili.Text = (Math.Round(float.Parse(txt_songLastMili.Text) * perc / 100, 3)).ToString();
            txt_Percentage.Text = (perc).ToString();

            //P2 calculate average percentage to ditribute each note using a another way (full timing) as to get the last note close to desired target
            var diff_endMinExpect =
                (xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3))
                -
                (txt_songLast.Value.Hour * 3600 + txt_songLast.Value.Minute * 60 + txt_songLast.Value.Second + Math.Round(float.Parse(txt_songLastMili.Text), 3));

            txt_DiffLenght.Text = (float.Parse(txt_CurrentLenght.Text) - float.Parse(txt_ExpctLenght.Text)).ToString();

            var percc = Math.Round(100 * diff_endMinExpect /
                (xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3))
            , 3).ToString();

            //correcting so not to have second note in the section start to be written before the fist note
            float firstno = float.Parse((txt_songStart.Value.Hour * 3600 + txt_songStart.Value.Minute * 60 + txt_songStart.Value.Second).ToString()) + float.Parse((Math.Round(float.Parse(txt_songStartMili.Text), 3)).ToString());
            float secn = get_second_note(txt_XMLPath.Text, firstno); float corr = float.Parse(percc);
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.2).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            if (firstno > (float.Parse(secn.ToString()) - float.Parse(secn.ToString()) * float.Parse(percc) / 100)) percc = (float.Parse(percc) - 0.5).ToString();
            txt_corr.Text = (float.Parse(percc) - corr).ToString();

            txt_PercTime.Text = percc;

            //P3 calculate average percentage to ditribute each note using a another way (full timing) as to get the last note close to desired target
            var diff_LastNote_and_ExpectedEndNote =
                (xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3))
                -
                (txt_songLast.Value.Hour * 3600 + txt_songLast.Value.Minute * 60 + txt_songLast.Value.Second + Math.Round(float.Parse(txt_songLastMili.Text), 3));

            var percp3 = Math.Round(100 * diff_LastNote_and_ExpectedEndNote /
                (xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3))
            , 3).ToString(); percp3 = Math.Round((100 - float.Parse(txt_Percentage.Text)), 3).ToString();
            txt_P3.Text = percp3;

            txt_P3LN.Value = DateTime.Parse("00:00:00");
            d = xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3);
            d = Math.Round(d - diff_LastNote_and_ExpectedEndNote, 3);
            txt_P3LN.Value = txt_P3LN.Value.AddSeconds(d);
            txt_P3LNMili.Text = (Math.Round(d - Math.Truncate(float.Parse(d.ToString())), 3)).ToString();

            d = get_second_note(txt_XMLPath.Text, firstno); txt_P3FN.Value = DateTime.Parse("00:00:00");
            d = Math.Round(firstno + (d - firstno) * float.Parse(percp3) / 100, 3);
            txt_P3FN.Value = txt_P3FN.Value.AddSeconds(d);
            d = float.Parse(Math.Round((float.Parse(d.ToString()) - Math.Truncate(float.Parse(d.ToString()))), 3).ToString());
            txt_P3FNMili.Text = (Math.Round(float.Parse(d.ToString()) - float.Parse(d.ToString()) * float.Parse(percp3) / 100, 3)).ToString();
            //txt_RealLastNote.Value = txt_RealLastNote.Value.AddMinutes(txt_songLast.Value.Minute + perc * txt_songLast.Value.Minute);
            //txt_RealLastNoteMili.Text = (float.Parse(txt_songLastMili.Text) + perc * float.Parse(txt_songLastMili.Text)).ToString();
            //if (float.Parse(txt_songLastMili.Text) > 1)
            //    txt_RealLastNote.Value = txt_RealLastNote.Value.AddSeconds(1 + txt_songLast.Value.Second + perc * txt_songLast.Value.Second);
            //else if (float.Parse(txt_songLastMili.Text) < 1)
            //    txt_RealLastNote.Value = txt_RealLastNote.Value.AddSeconds(txt_songLast.Value.Second + perc * txt_songLast.Value.Second - 1);
            //else
            //    txt_RealLastNote.Value = txt_RealLastNote.Value.AddSeconds(txt_songLast.Value.Second + perc * txt_songLast.Value.Second);
            //(float.Parse(txt_CurrentLenght.Text) - //(xml_last.Value.Ticks - xml_first.Value.Ticks).ToString();


            var starttt = Math.Round((xml_first.Value.Hour * 3600 + xml_first.Value.Minute * 60 + xml_first.Value.Second + Math.Round(float.Parse(xml_firstMili.Text), 3)), 3);
            if (p == "P1")
                return txt_Percentage.Text + ";" + starttt + ";" + txt_Lastnote.Text + ";" + "p1" + ";" + chbx_removehandshapes.Checked + ";" + txt_PercTime.Text;
            else if (p == "P2")
                return txt_PercTime.Text + ";" + starttt + ";" + txt_Lastnote.Text + ";" + "p2" + ";" + chbx_removehandshapes.Checked + ";" + txt_PercTime.Text;
            else //if (p == "P3")
                return txt_P3.Text + ";" + starttt + ";" + txt_Lastnote.Text + ";" + "p3" + ";" + chbx_removehandshapes.Checked + ";" +
                    txt_P3LN.Value.Hour + ":" + txt_P3LN.Value.Minute + ":" + txt_P3LN.Value.Second + "." + Math.Round(float.Parse(txt_P3LNMili.Text), 3).ToString();//.Substring(2);
            //percentage               starttime         last note                calc type            removehandshapes              unused
        }
        static float get_second_note(string xsml, float t)
        {
            var xmlContent = Song2014.LoadFromFile(xsml);
            float note = float.Parse(t.ToString());
            for (var j = 0; j < xmlContent.Levels.Length; j++)
            {
                for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                    if (xmlContent.Levels[j].Notes[k].Time == float.Parse(t.ToString()))
                        if (xmlContent.Levels[j].Notes.Length >= k + 1)
                            note = xmlContent.Levels[j].Notes[k + 1].Time;

                for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
                    if (xmlContent.Levels[j].Chords[k].Time == float.Parse(t.ToString()))
                        if (xmlContent.Levels[j].Chords.Length >= k + 1)
                            if (note < xmlContent.Levels[j].Chords[k + 1].Time) note = xmlContent.Levels[j].Chords[k + 1].Time;
                break; // dont got to other levels
            }
            return note;
        }
        private void btn_DBFolder_Click(object sender, EventArgs e)
        {
            var result1 = MessageBox.Show("chose file.", "GuitarPro file", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            StartProcesss("notepad.exe", txt_XMLPath.Text);
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
                && !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value)) ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            perc_time_betw_notes = calctime("P3");// float.Parse();
            this.Hide();
        }

        private void btn_Estimate_Click(object sender, EventArgs e)
        {
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
                || (txt_songStartMili.Text != xml_firstMili.Text || txt_songLastMili.Text != xml_lastMili.Text
                || !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value)))
                ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txt_CurrentLenght.Text = Math.Round((xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3)
                - xml_first.Value.Hour * 3600 - xml_first.Value.Minute * 60 - xml_first.Value.Second - Math.Round(float.Parse(xml_firstMili.Text), 3)), 3).ToString();

            //btn_ApplyP1.Enabled = true;
            //btn_ApplyP2.Enabled = true;
            btn_ApplyP3.Enabled = true;
            btn_ApplyP1.Visible = true;
            btn_ApplyP2.Visible = true;
            btn_ApplyP3.Visible = true;
            calctime("P3");
        }

        private void btn_OpenGP5_Click(object sender, EventArgs e)
        {
            StartProcesss("notepad.exe", txt_GP5.Text);
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_OpenXML_Click(object sender, EventArgs e)
        {
            StartProcesss("notepad.exe", txt_XMLPath.Text);
        }

        private void btn_Album2SortA_Click(object sender, EventArgs e)
        {
            txt_songStart.Value = xml_first.Value;
            txt_songStartMili.Text = xml_firstMili.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txt_songLast.Value = xml_last.Value;
            txt_songLastMili.Text = xml_lastMili.Text;
        }


        private void btn_Close_Click_2(object sender, EventArgs e)
        {
            ////take second note of notes & chord and apply the P2(end timing calc is real targetted end time)
            ////till
            //for (var j = 0; j < 100; j++)
            //{
            //    for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
            //        if (timec == 0)
            //            for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
            //                if (xmlContent.Levels[j].Chords[k].Time > 0)
            //                {
            //                }
            //}
            ////start w the 2nd note and increase by 0.5% till closest to end :)
            //// if note returns a value earlier than the one before..keep previous perc :)
            ////var prc = float.Parse(txt_Percentage.Text);
            ////var starte = float.Parse(txt_songStart.Text);
            ////var ende = float.Parse(txt_songLast.Text);
            ////string p = "p2";
            //var not_t = calctime(true);// float.Parse();prc.ToString();
            //float timec = 0; float timep = 0;
            //try
            //{
            //    var xmlContent = Song2014.LoadFromFile(txt_XMLPath.Text);
            //    xmlContent.SongLength = MainDB.calcreltime(xmlContent.SongLength, not_t, 0, false);

            //    for (var j = 0; j < xmlContent.Levels.Length; j++)
            //    {
            //        for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
            //            if (timec==0)
            //                timec = MainDB.calcreltime(xmlContent.Levels[j].Notes[k].Time, not_t, xmlContent.Levels[j].Notes[0].Time, false);
            //        for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
            //            if (xmlContent.Levels[j].Chords[k].Time > 0)
            //            {
            //                timec = MainDB.calcreltime(xmlContent.Levels[j].Chords[k].Time, not_t, xmlContent.Levels[j].Chords[0].Time, false);
            //                if (timep  > timec)
            //                    timep = timec;
            //            }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error shifting timings..." + ex; var timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //}
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
                && !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value)) ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            perc_time_betw_notes = calctime(null);
            this.Hide();
            //Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_songLastMili.Text = "0";
        }

        private void btn_RestoreXML_Click(object sender, EventArgs e)
        {
            if (File.Exists(txt_XMLPath.Text + ".old3"))
            {
                DeleteFile(txt_XMLPath.Text, true); var t = txt_XMLPath.Text + ".old3";
                File.Copy(t, txt_XMLPath.Text, true);
            }
            txt_Description.Text = txt_Description.Text.Contains("--") ?
                txt_Description.Text.Substring(txt_Description.Text.IndexOf("--") + 2, txt_Description.Text.Length - 2 - 1 - txt_Description.Text.IndexOf("--"))
                : "";
            var updatecmdd = "UPDATE Arrangements SET Comments=\"" + txt_Description.Text + "\" WHERE ID = " + ArrangID; //Comments+\"\n
            UpdateDB("Arrangements", updatecmdd, cnb, cnc);

            DistribXMLNotes_Load(null, null);
            //btn_Save_Click(null, null);
        }

        private void cmb_Sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Sections.Text == "") return;

            xml_first.Value = DateTime.Parse("00:00:00");
            xml_last.Value = DateTime.Parse("00:00:00");
            var i = cmb_Sections.SelectedIndex;

            string tx = cmb_Sections.Text; tx = tx.Replace("firstnote: ", "");
            tx = tx.Substring(0, tx.IndexOf(" - "));// dus.Tables[0].Rows[0].ItemArray[3].ToString());
            string ty = cmb_Sections.Text; ty = ty.Substring(ty.IndexOf("endnote: ")); //cmb_Sections.Items[i + 1].ToString();
            ty = i < cmb_Sections.Items.Count ? ty.Replace("endnote: ", "").Substring(0, ty.Replace("endnote: ", "").IndexOf(" - ")) : txt_Lastnote.Text;

            txt_Lastnote.Text = ty;

            TimeSpan time = TimeSpan.FromSeconds(float.Parse(tx));
            xml_first.Value = DateTime.Today.Add(time);
            TimeSpan timez = TimeSpan.FromSeconds(float.Parse(ty));
            xml_last.Value = DateTime.Today.Add(timez);

            xml_first.Value = xml_first.Value.AddMilliseconds(float.Parse(tx) - Math.Truncate(float.Parse(tx)));
            xml_last.Value = xml_last.Value.AddMilliseconds(float.Parse(ty) - Math.Truncate(float.Parse(ty)));
            xml_firstMili.Text = Math.Round((float.Parse(tx) - Math.Truncate(float.Parse(tx))), 3).ToString();
            xml_lastMili.Text = Math.Round((float.Parse(ty) - Math.Truncate(float.Parse(ty))), 3).ToString();
            txt_CurrentLenght.Text = Math.Round((xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3)
                - xml_first.Value.Hour * 3600 - xml_first.Value.Minute * 60 - xml_first.Value.Second - Math.Round(float.Parse(xml_firstMili.Text), 3)), 3).ToString();

            btn_Album2SortA_Click(null, null);
            button1_Click(null, null);
            //cmb_Tracks.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_songStart.Value.Hour > 0 || txt_songStart.Value.Minute > 0 || txt_songStart.Value.Second > 0) ;
            else
            {
                MessageBox.Show("Set a Start!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var r = calctime("P3");
            var t = Math.Round(
                    xml_first.Value.Hour * 3600 + xml_first.Value.Minute * 60 + xml_first.Value.Second + Math.Round(float.Parse(xml_firstMili.Text), 3)
                - txt_songStart.Value.Hour * 3600 - txt_songStart.Value.Minute * 60 - txt_songStart.Value.Second - Math.Round(float.Parse(txt_songStartMili.Text), 3)
                , 3);
            t = xml_first.Value > txt_songStart.Value ? t : -t;
            perc_time_betw_notes = 100 + ";" +
                Math.Round(xml_first.Value.Hour * 3600 + xml_first.Value.Minute * 60 + xml_first.Value.Second + Math.Round(float.Parse(xml_firstMili.Text), 3), 3).ToString()
                + ";" + t.ToString() + ";;;;apply";
            SaveSectionAndIntDescription();
            this.Hide();
            DistribXMLNotes_Load(null, null);
        }

        private void btn_SyncPh2Sect_Click(object sender, EventArgs e)
        {
            SaveSectionAndIntDescription();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            var updatecmdd = "UPDATE Arrangements SET Comments=\"" + txt_Description.Text + "\" WHERE ID = " + ArrangID; //Comments+\"\n
            UpdateDB("Arrangements", updatecmdd, cnb, cnc);

            if (!File.Exists(txt_XMLPath.Text + ".old3")) File.Copy(txt_XMLPath.Text, txt_XMLPath.Text + ".old3", true);

            SaveSectionAndIntDescription();

            //for (var j = 0; j < xmlContent.Sections.Length; j++)
            //    if (xmlContent.Sections[j].StartTime > 0)
            //        xmlContent.Sections[j].StartTime = 0;
            //            < sections count = "5" >
            //  < section name = "modchorus" number = "1" startTime = "36.060" />
            //  < section name = "sdaada" number = "1" startTime = "46.500" />
            //  < section name = "qedqw" number = "1" startTime = "72.125" />
            //  < section name = "outro" number = "1" startTime = "109.861" />
            //  < section name = "noguitar" number = "1" startTime = "196.500" />
            //</ sections >
            //using (var stream = File.Open(txt_XMLPath.Text, FileMode.Create)) xmlContent.Serialize(stream);
            DistribXMLNotes_Load(null, null);
        }

        private void SaveDescriptionInBackup()
        {
            var xmlContent = Song2014.LoadFromFile(txt_XMLPath.Text+".old3");
            if (xmlContent is null)
            {
                MessageBox.Show("issues at reading xml..please press restore");
                return;
            }
            var optionalfound = false; //bool usephases = false;
            if (xmlContent.OptionalProperties is not null) optionalfound = true;
            

            var info = File.OpenText(txt_XMLPath.Text);
            string line;// var noo = "";
            using (StreamWriter sw = File.CreateText(txt_XMLPath.Text + ".newvcl"))
            {
                
                while ((line = info.ReadLine()) != null)
                {
                    
                    if (line.ToLower().Contains("<optional") && optionalfound)
                        line = "<optionalProperties japaneseArtistName = \"" + txt_Description.Text + ";" + perc_time_betw_notes + "\" />";
                    if (line.ToLower().Contains("<arrangementproperties") && !optionalfound)
                        line += "\n<optionalProperties japaneseArtistName = \"" + txt_Description.Text.Replace("\n", "") + perc_time_betw_notes.Replace("\n", "") + "\" />";

                    if (line != "") sw.WriteLine(line);
                }
            }

            info.Close();
            File.Copy(txt_XMLPath.Text + ".newvcl", txt_XMLPath.Text+".old3", true);
            DeleteFile(txt_XMLPath.Text + ".newvcl", false);
        }

        private void SaveSectionAndIntDescription()
        {
            var xmlContent = Song2014.LoadFromFile(txt_XMLPath.Text);
            if (xmlContent is null)
            {
                MessageBox.Show("issues at reading xml..please press restore");
                return;
            }
            var optionalfound = false; bool usephases = false;
            if (xmlContent.OptionalProperties is not null) optionalfound = true;
            var sect = "<sections count = \"" + xmlContent.Phrases.Length + "\">\n";
            if (xmlContent.Phrases.Length != xmlContent.Sections.Length && c("dlcm_AdditionalManipul130")=="No")/*btn_SyncPh2Sect.Enabled && */
            {
                DialogResult result1 = MessageBox.Show("Discrepancy between Sections(" + xmlContent.Sections.Length + ") and phases ("
                    + xmlContent.Phrases.Length + ") has been observed. \n\n(Yes) Override no&Names of Sections with no&Names of Phrases\n\n(No) Override no&Names of Phrases with no&Names of Sectios?"
                    , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) usephases = true;
            }
            //else usephases = true;

            int p = 0;
            if (usephases)
                for (var j = 0; j < xmlContent.Phrases.Length; j++)
                {
                    p++;
                    if (j > 0) if (xmlContent.PhraseIterations[j].PhraseId == xmlContent.PhraseIterations[j - 1].PhraseId) p++;

                    sect += "<section name=\"" + xmlContent.Phrases[j].Name + "\" number=\"" + (j + 1) + "\" startTime=\"" + xmlContent.PhraseIterations[j].Time + "\"/>\n";
                }
            //else
            //    for (var j = 0; j < xmlContent.Sections.Length; j++)
            //    {
            //        sect += "<section name=\"" + xmlContent.Phrases[j].Name + "\" number=\"" + (j + 1) + "\" startTime=\"" + xmlContent.PhraseIterations[j].Time + "\"/>\n";
            //    }

            string[] name = new string[1000]; string[] time = new string[1000];
            string[] args = (txt_Sections.Text).ToString().Split(';');

            var info = File.OpenText(txt_XMLPath.Text);
            string line; var noo = "";
            using (StreamWriter sw = File.CreateText(txt_XMLPath.Text + ".newvcl"))
            {
                var j = 0; var nomansec = 0;
                for (j = 0; j < args.Length - 1; j++)
                {
                    if (args[j] == "") break;
                    nomansec = j;
                    name[j] = args[j].Contains("=") ? args[j].Substring(0, args[j].IndexOf("=")).Replace("\n", "") : "";
                    time[j] = args[j].Contains("=") ? args[j].Substring(args[j].IndexOf("=") + 1, args[j].Length - args[j].IndexOf("=") - 1) : "0";
                }
                j = 0;
                while ((line = info.ReadLine()) != null)
                {
                    if ((line.ToLower().Contains("</phrases>")) && !usephases)
                    {
                        line = "";
                        for (var k = nomansec + 1; k < xmlContent.Phrases.Length && xmlContent.Phrases.Length <= txt_Sections.Lines.Length; k++)
                        {
                            if (txt_Sections.Lines[k] == "") continue;
                            line += "<phrase name = \"" + txt_Sections.Lines[k].Substring(0, txt_Sections.Lines[k].IndexOf("=") - 1) + "\""
                                    + "\" maxDifficulty = \"0\" />\n";
                        }
                        line += "</phrases>";
                        sw.WriteLine(line);
                        continue;
                    }
                    if ((line.ToLower().Contains("</phraseIterations>")) && !usephases)
                    {
                        line = "";
                        for (var k = nomansec + 1; k < xmlContent.PhraseIterations.Length && xmlContent.Phrases.Length <= txt_Sections.Lines.Length; k++)
                        {
                            if (txt_Sections.Lines[k] == "") continue;
                            line += "<phraseIteration time = \"" + txt_Sections.Lines[k].Substring(txt_Sections.Lines[k].IndexOf("="),
                                    (txt_Sections.Lines[k].IndexOf(";") - txt_Sections.Lines[k].IndexOf("=") - 1)) + "\" phraseId = \"" + k + "\" />\n";
                        }
                        line += "</phraseIterations>";
                        sw.WriteLine(line);
                        continue;
                    }

                    if ((line.ToLower().Contains("<sections")) && usephases)
                    {
                        line = sect;/*|| line.ToLower().Contains("sections>")*/
                        sw.WriteLine(line);
                        continue;
                    }
                    else if (line.ToLower().Contains("<sections") && !usephases)
                    {
                        line = line.Replace(" = ", "=");
                        if (line.ToLower().Contains("<sections count=") && nomansec + 1 > xmlContent.Sections.Length)
                        {
                            var rt = "<sections count=\"" + (nomansec + 1) + "\">";/*(args.Length)*/
                            sw.WriteLine(rt);
                        }
                        else sw.WriteLine(line);
                        continue;
                    }
                    //else if (line.ToLower().Contains("<sections") && usephases)
                    //    line = "";

                    if (line.ToLower().Contains("<section") && !usephases)
                    {
                        if (line.ToLower().Contains("number=") || line.ToLower().Contains("number = "))
                        {
                            noo = line.Replace(" = ", "=");
                            noo = noo.Substring(noo.IndexOf("number=") + 7, noo.Length - 7 - 1 - noo.IndexOf("number=")).Trim();
                            noo = noo.Substring(1, noo.Length - 1 - 1).Trim();
                            noo = noo.Substring(0, noo.IndexOf("\""));
                        }
                        line = "<section name = \"" + name[j] + "\" number = \"" + noo + "\" startTime = \"" + time[j] + "\" />";
                        j++;
                    }
                    else if (line.ToLower().Contains("<section") && usephases)
                        line = "";
                    // "<section name = \"" + name[j] + "\" number = \"" + noo + "\" startTime = \"" + time[j] + "\" />";
                    if (line.ToLower().Contains("</sections") && !usephases)
                    {
                        line = "";
                        for (var k = xmlContent.Sections.Length; k < nomansec + 1; k++)
                        {
                            if (txt_Sections.Lines[k] == "") continue;
                            line += "<section name = \"" + txt_Sections.Lines[k].Substring(0, txt_Sections.Lines[k].IndexOf("=")) + "\""
                                    + " number = \"" + k + "\" startTime = \"" + txt_Sections.Lines[k].Substring(txt_Sections.Lines[k].IndexOf("=") + 1,
                                    (txt_Sections.Lines[k].IndexOf(";") - txt_Sections.Lines[k].IndexOf("=") - 1)) + "\" />\n";
                        }
                        line += "</sections>";
                    }
                    if (line.ToLower().Contains("<optional") && optionalfound)
                        line = "<optionalProperties japaneseArtistName = \"" + txt_Description.Text + ";" + perc_time_betw_notes + "\" />";
                    if (line.ToLower().Contains("<arrangementproperties") && !optionalfound)
                        line += "\n<optionalProperties japaneseArtistName = \"" + txt_Description.Text.Replace("\n", "") + perc_time_betw_notes.Replace("\n", "") + "\" />";

                    if (line != "") sw.WriteLine(line);
                }
                //RocksmithToolkitLib.XML.SongSection[] tst = new RocksmithToolkitLib.XML.SongSection[] { { "3", 4 }, { "3", 4 } };
                //tst[0].Name. = name;
                //tst[0].StartTime = float.Parse(time.ToString());
                //if (j + 1 > xmlContent.Sections.Length) xmlContent.Sections.Append(1);
                ////xmlContent.Sections = (RocksmithToolkitLib.XML.SongSection[])xmlContent.Sections.Concat(tst);
                //else
                //xmlContent.Sections[j].StartTime = float.Parse(time.ToString());
            }

            info.Close();
            File.Copy(txt_XMLPath.Text + ".newvcl", txt_XMLPath.Text, true);
            btn_SyncPh2Sect.Enabled = false;
            btn_SyncPh2Sect.Text = "Sync Phrases to Sections (xx/yy)";
            DeleteFile(txt_XMLPath.Text + ".newvcl", false);
        }

        private void Save()
        {
            if (!Directory.Exists(Path.GetDirectoryName(txt_XMLPath.Text) + "\\distr"))
                Directory.CreateDirectory(Path.GetDirectoryName(txt_XMLPath.Text) + "\\distr");
            File.Copy(txt_XMLPath.Text
                , txt_XMLPath.Text.Replace(Path.GetFileName(txt_XMLPath.Text), "") + "\\distr\\" + Path.GetFileName(txt_XMLPath.Text)
                + "." + DateTime.Now.ToString().Replace("/", "").Replace(":", ""), true);
        }


        static float GetNoOrdolf(float time, int t, int k, int main, string[,,] nt0, string[,,] nt1, string[,,] nt2, string[,,] nt3, string[,,] nt4, string[,,] nt5, string[,,] nt6)
        {
            float res = time;
            try
            {
                for (var n = 1; n <= t; n++) //note n
                {
                    if (nt0[k, n, 0] is null) continue;
                    var timesn = float.Parse(nt0[k, n, 0]);
                    if (timesn == time) for (var m = 1; m <= t; m++)
                            if (nt0[main, m, 5] is not null)
                                //if (float.Parse(nt0[k, n, 5]) == float.Parse(nt0[main, m, 5])) //beastie boys taht has intro lead notes just like dank jone is not starting from right note
                                if (float.Parse(nt0[main, m, 0]) >= time)
                                {
                                    res = float.Parse(nt0[main, m, 0]);
                                    break;
                                }
                }
            }
            catch (Exception ex)
            {
                var timestamp = UpdateLog(DateTime.Now, "GetNoOfID issues: "+ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);               
            }

            return res;
        }

        static float GetNoOrd(float time, int t, int k, int main, string[,,] nt0, int startm, float startt)
        {
            float res = time; int o = -1;
            try
            {
                for (var n = 1; n <= t; n++) //note n
                {
                    if (nt0[k, n, 0] is null) continue;
                    var timesn = float.Parse(nt0[k, n, 0]);
                    if (timesn >= startt) o++;
                    if (timesn == time)
                        res = float.Parse(nt0[main, startm + o, 0]);
                    //for (var m = 1; m <= t; m++)
                    //    if (nt0[main, m, 5] is not null)
                    //        if (float.Parse(nt0[k, n, 5]) == float.Parse(nt0[main, m+startm+, 5])) //beastie boys taht has intro lead notes just like dank jone is not starting from right note
                    //        //if (float.Parse(nt0[main, m, 0]) >= time)
                    //        {
                    //            res = float.Parse(nt0[main, m, 0]);
                    //            break;
                    //        }
                }
            }
            catch (Exception ex)
            {
                var timestamp = UpdateLog(DateTime.Now, "GetNoOfOrd issues: " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);
            }

            return res;
        }

        //Read the Target Track
        //Read the backup of reference, closest note to ref, save location & diff
        //By count get the same note from corrected track and figure out
        private void btn_Distrib_Click(object sender, EventArgs e)
        {
            var tt = 0; var tth = 0; var tj = 0; var tn = 0; var tjh = 0; var tnh = 0;
            var tst = ""; tth = cmb_Tracks.Items.Count;
            //nt1 1st string Nt6 th;
            //nt0 just a TIME HOLDER
            //string array             
            //column 1 is instrument/track
            //column 2 is notes
            //column 3 is
            //0 is time
            //1 is fret
            //2 is ?
            //3 is chord or notes
            //4 is Main/Slave
            //5 is no of order
            string[,,] nt0 = new string[10, 3000, 6];//consolidation of all other notes timings(1),(2) unused, incl, is it cord or note(3), or is it Master track to be copied (4),incl no of order(5)
            string[,,] nt1 = new string[10, 3000, 3];
            string[,,] nt2 = new string[10, 3000, 3];
            string[,,] nt3 = new string[10, 3000, 3];
            string[,,] nt4 = new string[10, 3000, 3];
            string[,,] nt5 = new string[10, 3000, 3];
            string[,,] nt6 = new string[10, 3000, 3];
            string[,] chords = new string[2000, 7];
            string[,] arg = new string[10, 7];
            int main = 0;

            DialogResult result1 = DialogResult.No; //var r = "";
            var xmlz = Directory.GetFiles(Path.GetDirectoryName(FilePath), "*.xml", System.IO.SearchOption.TopDirectoryOnly);
            var u = "";
            for (var k = 0; k < cmb_Tracks.SelectedItems.Count; k++) u += ";" + Path.GetDirectoryName(FilePath) + "\\" + cmb_Tracks.SelectedItems[k].ToString();

            float startt = 0;// float.Parse(Math.Round(txt_songStart.Value.Hour * 3600 + txt_songStart.Value.Minute * 60 + txt_songStart.Value.Second + Math.Round(float.Parse(txt_songStartMili.Text), 3), 3).ToString());
                             //float.Parse(Math.Round(xml_first.Value.Hour * 3600 + xml_first.Value.Minute * 60 + xml_first.Value.Second + Math.Round(float.Parse(xml_firstMili.Text), 3), 3).ToString());
            float endt = 1; //cmb_Sections.Text == ""?:float.Parse(Math.Round(xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3), 3).ToString());

            if (cmb_Sections.Text == "")
            {
                startt = float.Parse(Math.Round(xml_first.Value.Hour * 3600 + xml_first.Value.Minute * 60 + xml_first.Value.Second + Math.Round(float.Parse(xml_firstMili.Text), 3), 3).ToString());
                endt = float.Parse(Math.Round(xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3), 3).ToString());
            }
            else
            {
                var st = cmb_Sections.Text.Replace("start_time: ", "").Replace(cmb_Sections.Text.Substring(0, cmb_Sections.Text.IndexOf(" - ") + 3), "");
                st = st.Replace(st.Substring(0, st.IndexOf(" - ") + 3), "");
                st = st.Replace(st.Substring(0, st.IndexOf(" - ") + 3), "");
                st = st.Substring(0, st.IndexOf(" - ")).Trim();
                startt = float.Parse(st);
                var xmlContent = Song2014.LoadFromFile(txt_XMLPath.Text);
                var snglng = xmlContent.SongLength;
                var RouteMask = xmlContent.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContent.Arrangement.ToString()
            : xmlContent.ArrangementProperties.RouteMask.ToString();
                float x = float.Parse(GetTrackStartTime(txt_XMLPath.Text, RouteMask, null, true, -1, -1).ToString());
                float y = float.Parse(Math.Round(xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + Math.Round(float.Parse(xml_lastMili.Text), 3), 3).ToString());
                endt = cmb_Sections.Text == "" ? (endt > x ? endt : x) : (y > 0 && y > x ? y : x);
            }

            result1 = MessageBox.Show("Do you want to (" + startt + ", end note: " + endt + "): \n\n1. (Yes) Sync Section "
                + (cmb_Sections.Text == "" ? ": (none selected start note: " + startt + ", end note: " + endt + ")"
                : "\"" + cmb_Sections.Text + "\"") + "\n\n of (MASTER):\n" + txt_XMLPath.Text + "\n\n to (SLAVE):\n" + u + "\n\n 2. (No) uses an internal algorhythm (broken atm)" +
                "\n\n 3. (Cancel) Stops timing syncing between tracks."
                   , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            u += ";" + txt_XMLPath.Text;
            if (result1 == DialogResult.Cancel) return;
            if (result1 == DialogResult.Yes)
            {
                for (var k = 0; k < xmlz.Count(); k++) //foreach (var xml in xmlz)
                {
                    var t = 1;
                    var xml = xmlz[k].ToString();/*Path.GetDirectoryName(FilePath)+"\\"+*/
                    if (!u.Contains(xml)) continue;
                    Platform platform = (xml).GetPlatform();/*+ "..\\"*/
                    var manifestFunctions = new ManifestFunctions(platform.version);
                    Song2014 xmlContent = null;
                    try
                    {
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(xml);
                            var snglng = xmlContent.SongLength;
                            var RouteMask =
                                xmlContent.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContent.Arrangement.ToString() : xmlContent.ArrangementProperties.RouteMask.ToString();
                        }
                        catch (Exception ex)
                        {

                            var timestamp = UpdateLog(DateTime.Now, "Distribclick load and set: " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);
                            continue;
                        }
                        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                        if (manifestFunctions.GetMaxDifficulty(xmlContent) > 0)
                        {  //Save a copy
                            if (!File.Exists(xml + ".old8")) File.Copy(xml, xml + ".old8", false);
                            else File.Copy(xml, xml + ".old8", true);
                            xmlContent = Song2014.LoadFromFile(xml);
                        }

                        //var i = cmb_Tracks.GetItemChecked(k) ? xml : "";
                        if (txt_XMLPath.Text.Contains(xml))
                        {
                            nt0[k, 0, 4] = "Main";
                            main = k;
                        }

                        var a = xmlContent.ArrangementProperties.RouteMask.ToString();
                        if (a == "Vocal" || a == "ShowLight" || a == "4") continue;

                        //if (1 == 2) //we are only syncing "time" not adding notes(i.e. cords) and so this looks irrelevant
                        for (var n = 0; n < xmlContent.ChordTemplates.Length; n++)
                        {
                            if (xmlContent.ChordTemplates[n].Fret0 > -1) chords[n, 1] = xmlContent.ChordTemplates[n].Fret0.ToString();
                            if (xmlContent.ChordTemplates[n].Fret1 > -1) chords[n, 2] = xmlContent.ChordTemplates[n].Fret1.ToString();
                            if (xmlContent.ChordTemplates[n].Fret2 > -1) chords[n, 3] = xmlContent.ChordTemplates[n].Fret2.ToString();
                            if (xmlContent.ChordTemplates[n].Fret3 > -1) chords[n, 4] = xmlContent.ChordTemplates[n].Fret3.ToString();
                            if (xmlContent.ChordTemplates[n].Fret4 > -1) chords[n, 5] = xmlContent.ChordTemplates[n].Fret4.ToString();
                            if (xmlContent.ChordTemplates[n].Fret5 > -1) chords[n, 6] = xmlContent.ChordTemplates[n].Fret5.ToString();
                        }

                        if (xmlContent.Levels[0].Chords.Length > 0)
                            for (var m = 0; m < xmlContent.Levels[0].Chords.Length; m++)
                            {
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 1] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt1[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt1[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 1]; }
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 2] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt2[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt2[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 2]; }
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 3] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt3[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt3[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 3]; }
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 4] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt4[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt4[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 4]; }
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 5] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt5[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt5[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 5]; }
                                if (chords[xmlContent.Levels[0].Chords[m].ChordId, 6] is not null) if (xmlContent.Levels[0].Chords[m].Time.ToString() != "") { nt0[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt0[k, t, 3] = "c"; nt0[k, t, 5] = t.ToString(); nt6[k, t, 0] = xmlContent.Levels[0].Chords[m].Time.ToString(); nt6[k, t, 1] = chords[xmlContent.Levels[0].Chords[m].ChordId, 6]; }
                                t++;
                            }
                        float ghosts = 0;
                        if (xmlContent.Levels[0].Notes.Length > 0)
                            for (var n = 0; n < xmlContent.Levels[0].Notes.Length; n++)
                            {
                                if (xmlContent.Levels[0].Notes[n].Time == ghosts) t--; ;
                                ghosts = xmlContent.Levels[0].Notes[n].Time;
                                if (xmlContent.Levels[0].Notes[n].String == 0) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt1[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt1[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                else if (xmlContent.Levels[0].Notes[n].String == 1) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt2[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt2[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                else if (xmlContent.Levels[0].Notes[n].String == 2) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt3[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt3[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                else if (xmlContent.Levels[0].Notes[n].String == 3) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt4[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt4[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                else if (xmlContent.Levels[0].Notes[n].String == 4) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt5[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt5[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                else if (xmlContent.Levels[0].Notes[n].String == 5) { nt0[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt0[k, t, 3] = "n"; nt0[k, t, 5] = t.ToString(); nt6[k, t, 0] = xmlContent.Levels[0].Notes[n].Time.ToString(); nt6[k, t, 1] = xmlContent.Levels[0].Notes[n].Fret.ToString(); }
                                //else;
                                t++;
                            }

                        nt0[k, 0, 5] = t.ToString();

                        //sort
                        for (var n = 1; n <= t - 1; n++)
                            for (var m = n + 1; m <= t; m++)
                            {
                                var timesn = nt0[k, n, 0];
                                var timesm = nt0[k, m, 0];
                                if (timesn is not null && timesm is not null)
                                {
                                    if (float.Parse(timesn) > float.Parse(timesm))
                                    {
                                        //if (n > 313 || m > 312) ;

                                        var u0 = nt0[k, n, 0]; nt0[k, n, 0] = nt0[k, m, 0]; nt0[k, m, 0] = u0; //time
                                        var j0 = nt0[k, n, 3]; nt0[k, n, 3] = nt0[k, m, 3]; nt0[k, m, 3] = j0; //notes/cord
                                        var z0 = nt0[k, n, 5]; nt0[k, n, 5] = nt0[k, m, 5]; nt0[k, m, 5] = z0; //main/slave
                                        var z1 = nt1[k, n, 1]; nt1[k, n, 1] = nt1[k, m, 1]; nt1[k, m, 1] = z1;/**/ var u1 = nt1[k, n, 0]; nt1[k, n, 0] = nt1[k, m, 0]; nt1[k, m, 0] = u1;
                                        var z2 = nt2[k, n, 1]; nt2[k, n, 1] = nt2[k, m, 1]; nt2[k, m, 1] = z2; var u2 = nt2[k, n, 0]; nt2[k, n, 0] = nt2[k, m, 0]; nt2[k, m, 0] = u2;
                                        var z3 = nt3[k, n, 1]; nt3[k, n, 1] = nt3[k, m, 1]; nt3[k, m, 1] = z3; var u3 = nt3[k, n, 0]; nt3[k, n, 0] = nt3[k, m, 0]; nt3[k, m, 0] = u3;
                                        var z4 = nt4[k, n, 1]; nt4[k, n, 1] = nt4[k, m, 1]; nt4[k, m, 1] = z4; var u4 = nt4[k, n, 0]; nt4[k, n, 0] = nt4[k, m, 0]; nt4[k, m, 0] = u4;
                                        var z5 = nt5[k, n, 1]; nt5[k, n, 1] = nt5[k, m, 1]; nt5[k, m, 1] = z5; var u5 = nt5[k, n, 0]; nt5[k, n, 0] = nt5[k, m, 0]; nt5[k, m, 0] = u5;
                                        var z6 = nt6[k, n, 1]; nt6[k, n, 1] = nt6[k, m, 1]; nt6[k, m, 1] = z6; var u6 = nt6[k, n, 0]; nt6[k, n, 0] = nt6[k, m, 0]; nt6[k, m, 0] = u6;
                                        //break;
                                    }
                                    else if (float.Parse(timesn) == float.Parse(timesm) && nt0[k, n, 3] == "c")
                                    {
                                        ;
                                        //var z0 = nt0[k, n, 0]; nt0[k, n, 0] = nt0[k, m, 0]; nt1[k, m, 1] = z0;
                                        //var z1 = nt1[k, n, 1]; nt1[k, n, 1] = nt1[k, m, 1]; nt1[k, m, 1] = z1; var u1 = nt1[k, n, 0]; nt1[k, n, 0] = nt1[k, m, 0]; nt1[k, m, 0] = u1;
                                        //var z2 = nt2[k, n, 1]; nt2[k, n, 1] = nt2[k, m, 1]; nt2[k, m, 1] = z2; var u2 = nt2[k, n, 0]; nt2[k, n, 0] = nt2[k, m, 0]; nt2[k, m, 0] = u2;
                                        //var z3 = nt3[k, n, 1]; nt3[k, n, 1] = nt3[k, m, 1]; nt3[k, m, 1] = z3; var u3 = nt3[k, n, 0]; nt3[k, n, 0] = nt3[k, m, 0]; nt3[k, m, 0] = u3;
                                        //var z4 = nt4[k, n, 1]; nt4[k, n, 1] = nt4[k, m, 1]; nt4[k, m, 1] = z4; var u4 = nt4[k, n, 0]; nt4[k, n, 0] = nt4[k, m, 0]; nt4[k, m, 0] = u4;
                                        //var z5 = nt5[k, n, 1]; nt5[k, n, 1] = nt5[k, m, 1]; nt5[k, m, 1] = z5; var u5 = nt5[k, n, 0]; nt5[k, n, 0] = nt5[k, m, 0]; nt5[k, m, 0] = u5;
                                        //var z6 = nt6[k, n, 1]; nt6[k, n, 1] = nt6[k, m, 1]; nt6[k, m, 1] = z6; var u6 = nt6[k, n, 0]; nt6[k, n, 0] = nt6[k, m, 0]; nt6[k, m, 0] = u6;
                                        //break;
                                    }

                                    else if (float.Parse(timesn) == float.Parse(timesm) && nt0[k, m, 3] == "c")
                                    {
                                        var z0 = nt0[k, n, 0]; nt0[k, n, 0] = nt0[k, m, 0]; nt1[k, m, 1] = z0;
                                        var z1 = nt1[k, n, 1]; nt1[k, n, 1] = nt1[k, m, 1]; nt1[k, m, 1] = z1; var u1 = nt1[k, n, 0]; nt1[k, n, 0] = nt1[k, m, 0]; nt1[k, m, 0] = u1;
                                        var z2 = nt2[k, n, 1]; nt2[k, n, 1] = nt2[k, m, 1]; nt2[k, m, 1] = z2; var u2 = nt2[k, n, 0]; nt2[k, n, 0] = nt2[k, m, 0]; nt2[k, m, 0] = u2;
                                        var z3 = nt3[k, n, 1]; nt3[k, n, 1] = nt3[k, m, 1]; nt3[k, m, 1] = z3; var u3 = nt3[k, n, 0]; nt3[k, n, 0] = nt3[k, m, 0]; nt3[k, m, 0] = u3;
                                        var z4 = nt4[k, n, 1]; nt4[k, n, 1] = nt4[k, m, 1]; nt4[k, m, 1] = z4; var u4 = nt4[k, n, 0]; nt4[k, n, 0] = nt4[k, m, 0]; nt4[k, m, 0] = u4;
                                        var z5 = nt5[k, n, 1]; nt5[k, n, 1] = nt5[k, m, 1]; nt5[k, m, 1] = z5; var u5 = nt5[k, n, 0]; nt5[k, n, 0] = nt5[k, m, 0]; nt5[k, m, 0] = u5;
                                        var z6 = nt6[k, n, 1]; nt6[k, n, 1] = nt6[k, m, 1]; nt6[k, m, 1] = z6; var u6 = nt6[k, n, 0]; nt6[k, n, 0] = nt6[k, m, 0]; nt6[k, m, 0] = u6;
                                        break;
                                    }
                                }
                            }
                    }
                    catch (Exception ex)
                    {
                        var timestamp = UpdateLog(DateTime.Now, "Distribclick : " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);

                    }
                }

                //check if the number of notes is comparable
                var z = "Tracks and note&cord count per selected section is:\n\n"; var diff = false;
                for (var k = 0; k < xmlz.Count(); k++)
                {
                    var at = 0;
                    var xml = xmlz[k].ToString();
                    if (!u.Contains(xml)) continue;
                    int t = nt0[k, 0, 5].ToString().ToInt32();
                    Platform platform = (Path.GetDirectoryName(xml) + "..\\").GetPlatform();
                    var manifestFunctions = new ManifestFunctions(platform.version);
                    Song2014 xmlContentf = null;
                    try
                    {
                        try
                        {
                            xmlContentf = Song2014.LoadFromFile(xml);
                            var snglng = xmlContentf.SongLength;
                        }
                        catch (Exception ex)
                        {
                            var timestamp = UpdateLog(DateTime.Now, "Distribclick load and lenght set: " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);

                            continue;
                        }

                        platform.version = RocksmithToolkitLib.GameVersion.RS2014;

                        var a = xmlContentf.ArrangementProperties.RouteMask.ToString();
                        if (a == "Vocal" || a == "ShowLight" || a == "4") continue;

                        if (nt0[k, 0, 4] == "Main" || main == k) z += "\n\tmaster(" + xmlContentf.ArrangementProperties.RouteMask.ToString() + ")= ";
                        else z += "\n\tslave(" + xmlContentf.ArrangementProperties.RouteMask.ToString() + ")= ";

                        if (xmlContentf.Levels[0].Chords.Length > 0)
                            for (var m = 0; m < xmlContentf.Levels[0].Chords.Length; m++)
                            {
                                if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 1] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                                else if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 2] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                                else if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 3] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                                else if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 4] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                                else if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 5] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                                else if (chords[xmlContentf.Levels[0].Chords[m].ChordId, 6] is not null) { if (xmlContentf.Levels[0].Chords[m].Time >= startt && xmlContentf.Levels[0].Chords[m].Time <= endt) at++; }
                            }
                        float ghosts = 0;
                        if (xmlContentf.Levels[0].Notes.Length > 0)
                            for (var n = 0; n < xmlContentf.Levels[0].Notes.Length; n++)
                            {
                                if (xmlContentf.Levels[0].Notes[n].Time == ghosts) continue;
                                ghosts = xmlContentf.Levels[0].Notes[n].Time;
                                if (xmlContentf.Levels[0].Notes[n].String == 0) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                                else if (xmlContentf.Levels[0].Notes[n].String == 1) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                                else if (xmlContentf.Levels[0].Notes[n].String == 2) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                                else if (xmlContentf.Levels[0].Notes[n].String == 3) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                                else if (xmlContentf.Levels[0].Notes[n].String == 4) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                                else if (xmlContentf.Levels[0].Notes[n].String == 5) { if (xmlContentf.Levels[0].Notes[n].Time >= startt && xmlContentf.Levels[0].Notes[n].Time <= endt) at++; }
                            }
                        if (!z.Contains(at.ToString())) diff = true;
                        z += at + ", ";
                    }
                    catch (Exception ex)
                    {
                        var timestamp = UpdateLog(DateTime.Now, "Distribclick : " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);
                    }
                }
                DialogResult result2 = DialogResult.Yes; ;

                if (diff) result2 = MessageBox.Show("No of notes&chords from main to others are different! 1. (Yes) Continue?\n1. (No) Stop distribuion! \n\n" + z, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.No) return;

                //get Master start note
                int startMAster = 0; //int endMAster = 0;
                for (var m = 1; m <= nt0[main, 0, 5].ToString().ToInt32(); m++)
                    if (nt0[main, m, 0] is not null)
                        if (float.Parse(nt0[main, m, 0]) >= startt)
                        {
                            //if (float.Parse(nt0[main, m, 0]) > endt) { endMAster = m;
                            //else if (startMAster == 0) 
                            startMAster = m; break;
                        }
                //}

                //sync notes with Main track ...for the Selected Section
                var al = 0;
                for (var k = 0; k < xmlz.Count(); k++)
                {
                    var xml = xmlz[k].ToString();
                    if (!u.Contains(xml)) continue;
                    //var i = cmb_Tracks.GetItemChecked(k) ? Directory.GetParent(xml) + "\\" + cmb_Tracks.Items[k].ToString().Substring(0, cmb_Tracks.Items[k].ToString().IndexOf(" - ")) + ".xml" : "";
                    if (nt0[k, 0, 4] == "Main" || main == k) continue; //dont sync the Main track from which times are extracted :)
                    int t = nt0[k, 0, 5].ToString().ToInt32();

                    Platform platform = (Path.GetDirectoryName(xml) + "..\\").GetPlatform();
                    var manifestFunctions = new ManifestFunctions(platform.version);
                    Song2014 xmlContente = null;
                    try
                    {
                        try
                        {
                            xmlContente = Song2014.LoadFromFile(xml);
                            var snglng = xmlContente.SongLength;
                        }
                        catch (Exception ex)
                        {
                            var timestamp = UpdateLog(DateTime.Now, "Distribclick load and lenghty set: " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);
                            continue;
                        }

                        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                        //Save a backup
                        if (!File.Exists(xml + ".old5")) File.Copy(xml, xml + ".old5", false);
                        else File.Copy(xml, xml + ".old5", true);

                        var a = xmlContente.ArrangementProperties.RouteMask.ToString();
                        if (a == "Vocal" || a == "ShowLight" || a == "4") continue;

                        //copy times from master incrementally to the
                        if (xmlContente.Levels[0].Chords.Length > 0)
                            for (var m = 0; m < xmlContente.Levels[0].Chords.Length; m++)
                            {
                                // if (chords[xmlContente.Levels[0].Chords[m].ChordId, 1] is not null) 
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                                // if (chords[xmlContente.Levels[0].Chords[m].ChordId, 2] is not null) 
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                                //if (chords[xmlContente.Levels[0].Chords[m].ChordId, 3] is not null) 
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                                // if (chords[xmlContente.Levels[0].Chords[m].ChordId, 4] is not null) 
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                                // if (chords[xmlContente.Levels[0].Chords[m].ChordId, 5] is not null)
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                                //            if (chords[xmlContente.Levels[0].Chords[m].ChordId, 6] is not null)
                                if (xmlContente.Levels[0].Chords[m].Time >= startt && xmlContente.Levels[0].Chords[m].Time <= endt) { al++; xmlContente.Levels[0].Chords[m].Time = GetNoOrd(xmlContente.Levels[0].Chords[m].Time, t, k, main, nt0, startMAster, startt); }
                            }
                        if (xmlContente.Levels[0].Notes.Length > 0)
                            for (var n = 0; n < xmlContente.Levels[0].Notes.Length; n++)
                            {
                                if (n == 48)
                                    ;
                                if (xmlContente.Levels[0].Notes[n].String == 0) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                                else if (xmlContente.Levels[0].Notes[n].String == 1) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                                else if (xmlContente.Levels[0].Notes[n].String == 2) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                                else if (xmlContente.Levels[0].Notes[n].String == 3) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                                else if (xmlContente.Levels[0].Notes[n].String == 4) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                                else if (xmlContente.Levels[0].Notes[n].String == 5) { if (xmlContente.Levels[0].Notes[n].Time >= startt && xmlContente.Levels[0].Notes[n].Time <= endt) { al++; xmlContente.Levels[0].Notes[n].Time = GetNoOrd(xmlContente.Levels[0].Notes[n].Time, t, k, main, nt0, startMAster, startt); } }
                            }
                        using (var stream = File.Open(xml, FileMode.Create))
                            xmlContente.Serialize(stream);
                    }
                    catch (Exception ex)
                    {
                        var timestamp = UpdateLog(DateTime.Now, "Distribclick issue: " + ex.Message, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", null, null);

                        ;
                    }
                }

                tst = "Aligned " + al + "/" + (nt0[1, 0, 5] is null ? 0 : nt0[1, 0, 5]) + " notes/cords, in-between timestamps: " + startt + " and " + endt + ".";
            }
            else
                for (var i = 0; i < tth; i++)
                {
                    return;
                    var k = cmb_Tracks.GetItemChecked(i) ? Directory.GetParent(txt_XMLPath.Text) + "\\" + cmb_Tracks.Items[i].ToString().Substring(0, cmb_Tracks.Items[i].ToString().IndexOf(" - ")) + ".xml" : "";
                    if (!File.Exists(k)) continue;/*|| File.Exists(k + ".old3")*/
                    if (File.Exists(k + ".old7")) File.Copy(k + ".old7", k, true);
                    if (!File.Exists(k + ".old7")) File.Copy(k, k + ".old7", true);

                    tt++;

                    //for (var j = 0; j < xmlContent.Levels.Length; j++)
                    //{
                    //    for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                    //        if (xmlContent.Levels[j].Notes[k].Time > 0)

                    var targf = Song2014.LoadFromFile(k);
                    var bkpref = Song2014.LoadFromFile(txt_XMLPath.Text + ".old3");
                    var reff = Song2014.LoadFromFile(txt_XMLPath.Text);

                    tjh = targf.Levels[0].Notes.Length;
                    for (var j = 0; j < tjh; j++)
                    {
                        var targ_note = targf.Levels[0].Notes[j].Time;
                        for (var m = 0; m < bkpref.Levels[0].Notes.Length - 1; m++)
                        {
                            var bkp_ref = bkpref.Levels[0].Notes[m].Time;
                            var bkp_ref_next = bkpref.Levels[0].Notes[m + 1].Time;
                            var ref_note = reff.Levels[0].Notes[m].Time;
                            if (bkp_ref <= targ_note && bkp_ref_next >= targ_note)
                            {
                                targf.Levels[0].Notes[j].Time = (float)Math.Round(float.Parse((
                                    targ_note - (bkp_ref - ref_note)
                                    ).ToString()), 3);
                                tj++;
                                break;
                            }
                        }
                    }

                    tnh = targf.Levels[0].Chords is not null ? targf.Levels[0].Chords.Length : 0;
                    for (var j = 0; j < tnh; j++)
                    {
                        var targ_note = targf.Levels[0].Chords[j].Time;
                        for (var m = 0; m < bkpref.Levels[0].Chords.Length - 1; m++)
                        {
                            var bkp_ref = bkpref.Levels[0].Chords[m].Time;
                            var bkp_ref_next = bkpref.Levels[0].Chords[m + 1].Time;
                            var ref_note = reff.Levels[0].Chords[m].Time;
                            if (bkp_ref <= targ_note && bkp_ref_next >= targ_note)
                            {
                                targf.Levels[0].Chords[j].Time = (float)Math.Round(float.Parse((
                                    targ_note - (bkp_ref - ref_note)
                                    ).ToString()), 3);
                                tn++;
                                break;
                            }
                        }
                    }

                    tnh = targf.Levels[0].Chords is not null ? targf.Levels[0].Chords.Length : 0;
                    //if (targf.Arrangement)
                    for (var j = 0; j < tnh; j++)
                    {
                        var targ_note = targf.Levels[0].Chords[j].Time;
                        for (var m = 0; m < bkpref.Levels[0].Notes.Length - 1; m++)
                        {
                            var bkp_ref = bkpref.Levels[0].Notes[m].Time;
                            var bkp_ref_next = bkpref.Levels[0].Notes[m + 1].Time;
                            var ref_note = reff.Levels[0].Notes[m].Time;
                            if (bkp_ref <= targ_note && bkp_ref_next >= targ_note)
                            {
                                targf.Levels[0].Chords[j].Time = (float)Math.Round(float.Parse((
                                    targ_note - (bkp_ref - ref_note)
                                    ).ToString()), 3);
                                tn++;
                                break;
                            }
                        }
                    }

                    using (var stream = File.Open(k, FileMode.Create)) targf.Serialize(stream);
                    tst += "\n" + "Distribution applied. " + cmb_Tracks.Items[i].ToString().Substring(0, cmb_Tracks.Items[i].ToString().IndexOf(" - "))
                        + " track: " + tt + "/" + tth + " ,notes: " + tj + "/" + tjh + ", cords: " + tn + "/" + tnh + ".";
                }
            if(c("dlcm_AdditionalManipul131") =="Yes")MessageBox.Show(tst, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Close_Click_3(object sender, EventArgs e)
        {
            perc_time_betw_notes = ";;;;;;close";
            this.Hide();
        }

        private void btn_backup_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (File.Exists(txt_XMLPath.Text + ".old3") && File.Exists(txt_XMLPath.Text + ".old9"))
                result1 = MessageBox.Show("Chose:\n\nYes (1.) to overwrite existing .old3 but not .old9 backups,\nNo (2.) to ignore backup action\nCancel (3.) to overwrite existing .old3 and .old9 backups."
            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            else
            if (File.Exists(txt_XMLPath.Text + ".old3") && !File.Exists(txt_XMLPath.Text + ".old9")) result1 = MessageBox.Show("Chose:\n\nYes (1.) to overwrite existing .old3 backup and create .old9\nNo (2.) to ignore backup action."
            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result1 == DialogResult.Cancel)
            {
                File.Copy(txt_XMLPath.Text + ".old3", txt_XMLPath.Text + ".old9", true);

                File.Copy(txt_XMLPath.Text, txt_XMLPath.Text + ".old3", true);
            }
            else
            if (result1 == DialogResult.Yes)
            {
                if (!File.Exists(txt_XMLPath.Text + ".old9")) File.Copy(txt_XMLPath.Text + ".old3", txt_XMLPath.Text + ".old9", false);

                File.Copy(txt_XMLPath.Text, txt_XMLPath.Text + ".old3", true);
            }
            else
            if (result1 == DialogResult.No)
            {
                //File.Copy(txt_XMLPath.Text, txt_XMLPath.Text + ".old3", true);
            }
            txt_Description.Text = "--" + txt_Description.Text;
            DistribXMLNotes_Load(null, null);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_CleanDescr_Click(object sender, EventArgs e)
        {
            txt_Description.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_CleanStartSong_Click(object sender, EventArgs e)
        {
            txt_songStartMili.Text = "0";
        }

        private void chbx_removehandshapes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_ApplyP3_Click(object sender, EventArgs e)
        {
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
                || (txt_songStartMili.Text != xml_firstMili.Text || txt_songLastMili.Text != xml_lastMili.Text
        && !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value))) ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(txt_XMLPath.Text + ".old3")) File.Copy(txt_XMLPath.Text, txt_XMLPath.Text + ".old3", true);
            
            perc_time_betw_notes = calctime("P3");// txt_P3.Text + ";" + starttt + ";" + txt_Lastnote.Text + ";" + "p3" + ";" + chbx_removehandshapes.Checked + ";" + txt_PercTime.Text;
            perc_time_betw_notes += ";apply";
            txt_Description.Text = txt_Description.Text.Replace(perc_time_betw_notes+ "\n","")+perc_time_betw_notes + "\n";

            Save();
            SaveSectionAndIntDescription();
            SaveDescriptionInBackup();
            this.Hide();
        }

        private void btn_P2_Click(object sender, EventArgs e)
        {
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
                && !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value)) ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            perc_time_betw_notes = calctime("P2");
            this.Hide();
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if ((txt_songLast.Value.Hour > 0 || txt_songLast.Value.Minute > 0 || txt_songLast.Value.Second > 0)
        && !(txt_songLast.Value == xml_last.Value && txt_songStart.Value == xml_first.Value)) ;
            else
            {
                MessageBox.Show("Set a Start/EndTime!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            perc_time_betw_notes = calctime("P1");// float.Parse();
            this.Hide();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            // DistribXMLNotes(sXml, FilePath, gPlatform, Arrangoff, BasedOn_CF, EoFPath, cnb, cnc);
            DistribXMLNotes_Load(null, null);
        }

        private void btn_VisualDistrib_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void xml_lastMili_TextChanged(object sender, EventArgs e)
        {
            var newXMLFilePath = txt_XMLPath.Text;
            var xmlContents = Song2014.LoadFromFile(newXMLFilePath);
            if (xmlContents is null) return;
            //string ArrangementType = null;
            var RouteMask = xmlContents.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContents.Arrangement.ToString()
                : xmlContents.ArrangementProperties.RouteMask.ToString();

            double last = xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + (xml_lastMili.Text == "" ? 0 : Math.Round(float.Parse(xml_lastMili.Text), 3));
            var st = GetTrackStartTime(newXMLFilePath, xmlContents.Arrangement, null, false, -1, -1);
            var zt = GetTrackStartTime(newXMLFilePath, xmlContents.Arrangement, null, true, float.Parse(xmlContents.Sections[0].StartTime.ToString()), last);
            txt_Lastnote.Text = zt;
        }

        private void xml_last_ValueChanged(object sender, EventArgs e)
        {
            var newXMLFilePath = txt_XMLPath.Text;
            var xmlContents = Song2014.LoadFromFile(newXMLFilePath);
            if (xmlContents is null) return;
            //string ArrangementType = null;
            var RouteMask = xmlContents.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContents.Arrangement.ToString()
                : xmlContents.ArrangementProperties.RouteMask.ToString();

            double last = xml_last.Value.Hour * 3600 + xml_last.Value.Minute * 60 + xml_last.Value.Second + (xml_lastMili.Text == "" ? 0 : Math.Round(float.Parse(xml_lastMili.Text), 3));
            var st = GetTrackStartTime(newXMLFilePath, xmlContents.Arrangement, null, false, -1, -1);
            var zt = GetTrackStartTime(newXMLFilePath, xmlContents.Arrangement, null, true, float.Parse(xmlContents.Sections[0].StartTime.ToString()), last);
            txt_Lastnote.Text = zt;
        }
    }
}
