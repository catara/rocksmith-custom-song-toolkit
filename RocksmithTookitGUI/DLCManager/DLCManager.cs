﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using RocksmithToolkitLib;
using RocksmithToolkitLib.Extensions; //dds
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.Xml; //For DD
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.DLCPackage.Manifest2014;
using System.IO;
using System.Data.OleDb;
using System.Security.Cryptography; //For File hash
using System.Diagnostics;//repack
using Ookii.Dialogs; //cue text
//using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using System.Data.SqlClient;//For the Export to Excel function
using RocksmithToTabLib;//for psarc browser
//using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;//regex
using RocksmithToolkitLib.XmlRepository;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {
        //bcapi
        private const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";
        private bool loading = false;
        public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
        public BackgroundWorker bwConvert = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi1  
        private StringBuilder errorsFound;//bcapi1
        string dlcSavePath = "";
        int no_ord = 0;
        //int norows = 0;
        string Groupss = "";
        DLCPackageData data;
        bool x4, x5, x9;// x1, x2, x3,
        string x6, x7, x8;

        //Processing global vars

        bool duplit = false;
        int dupliNo = 0;
        int dupliPrcs = 0;
        int[] dupliSongs = new int[100000];
        bool stopp = false;
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        internal static string MyAppWD = AppWD; //when removing DDC

        OleDbConnection connection;
        OleDbCommand command;

        static public Files[] files = new Files[10000];
        public DLCPackageData info;
        public string author = "";
        public string tkversion = "";
        public string description = "";
        public string comment = "";
        public string SongDisplayName = "";
        public string Namee = "";
        public string art_hash = "";
        public string AlbumArtPath = "";
        public string Alternate_No = "";
        public string Album = "";
        public string Title_Sort = "";
        public string Is_Alternate = "";
        public string Is_Original = "";
        public string ArtistSort = "";
        public string Artist = "";
        public string PackageVersion = "";
        public string unpackedDir1 = "";
        public string package1 = "";
        public string Is_MultiTrack = "";
        public string MultiTrack_Version = "";
        public string YouTube_Link = "";
        public string CustomsForge_Link = "";
        public string CustomsForge_Like = "";
        public string CustomsForge_ReleaseNotes = "";
        public string ExistingTrackNo = "";
        public string dupliID = "";
        public bool IgnoreRest = false;
        public string PreviewTime;
        public string PreviewLenght;
        public string AppIdD;

        public class Files
        {
            public string ID { get; set; }   //	ID
            public string Song_Title { get; set; }   //	Song_Title
            public string Song_Title_Sort { get; set; }  //	Song_Title_Sort
            public string Album { get; set; }    //	Album
            public string Artist { get; set; }   //	Artist
            public string Artist_Sort { get; set; }  //	Artist_Sort
            public string Album_Year { get; set; }   //	Album_Year
            public string AverageTempo { get; set; }     //	AverageTempo
            public string Volume { get; set; }   //	Volume
            public string Preview_Volume { get; set; }   //	Preview_Volume
            public string AlbumArtPath { get; set; }     //	AlbumArtPath
            public string AudioPath { get; set; }    //	AudioPath
            public string audioPreviewPath { get; set; }     //	audioPreviewPath
            public string Track_No { get; set; }     //	Track_No
            public string Author { get; set; }   //	Author
            public string Version { get; set; }  //	Version
            public string DLC_Name { get; set; }     //	DLC_Name
            public string DLC_AppID { get; set; }    //	DLC_AppID
            public string Current_FileName { get; set; }     //	Current_FileName
            public string Original_FileName { get; set; }    //	Original_FileName
            public string Import_Path { get; set; }  //	Import_Path
            public string Import_Date { get; set; }  //	Import_Date
            public string Folder_Name { get; set; }  //	Folder_Name
            public string File_Size { get; set; }    //	File_Size
            public string File_Hash { get; set; }    //	File_Hash
            public string Original_File_Hash { get; set; }   //	Original_File_Hash
            public string Is_Original { get; set; }  //	Is_Original
            public string Is_OLD { get; set; }   //	Is_OLD
            public string Is_Beta { get; set; }  //	Is_Beta
            public string Is_Alternate { get; set; }     //	Is_Alternate
            public string Is_Multitrack { get; set; }    //	Is_Multitrack
            public string Is_Broken { get; set; }    //	Is_Broken
            public string MultiTrack_Version { get; set; }   //	MultiTrack_Version
            public string Alternate_Version_No { get; set; }     //	Alternate_Version_No
            public string DLC { get; set; }  //	DLC
            public string Has_Bass { get; set; }     //	Has_Bass
            public string Has_Guitar { get; set; }   //	Has_Guitar
            public string Has_Lead { get; set; }     //	Has_Lead
            public string Has_Rhythm { get; set; }   //	Has_Rhythm
            public string Has_Combo { get; set; }    //	Has_Combo
            public string Has_Vocals { get; set; }   //	Has_Vocals
            public string Has_Sections { get; set; }     //	Has_Sections
            public string Has_Cover { get; set; }    //	Has_Cover
            public string Has_Preview { get; set; }  //	Has_Preview
            public string Has_Custom_Tone { get; set; }  //	Has_Custom_Tone
            public string Has_DD { get; set; }   //	Has_DD
            public string Has_Version { get; set; }  //	Has_Version
            public string Tunning { get; set; }  //	Tunning
            public string Bass_Picking { get; set; }     //	Bass_Picking
            public string Tones { get; set; }    //	Tones
            public string Groups { get; set; }    //	Groups
            public string Rating { get; set; }   //	Rating
            public string Description { get; set; }  //	Description
            public string Comments { get; set; }     //	Comments
            public string Has_Track_No { get; set; }   //	Show_Album
            public string Platform { get; set; }   //	Show_Track
            public string PreviewTime { get; set; }    //	Show_Year
            public string PreviewLenght { get; set; }    //	Show_CDLC
            public string Youtube_Playthrough { get; set; }  //	Show_Rating
            public string CustomForge_Followers { get; set; }     //	CustomForge_Followers
            public string CustomForge_Version { get; set; }    //	CustomForge_Version
            public string FilesMissingIssues { get; set; }   //	FilesMissingIssues
            public string Duplicates { get; set; }   //	Duplicates
            public string Pack { get; set; }  //	Pack
            public string Keep_BassDD { get; set; }   //	Keep_BassDDs
            public string Keep_DD { get; set; }    //	Keep_DD
            public string Keep_Original { get; set; }  //	Keep_Original
            public string Song_Lenght { get; set; }  //	Song_Lenght
            public string Original { get; set; }     //	Original
            public string Selected { get; set; }     //	Selected
            public string YouTube_Link { get; set; }     //	YouTube_Link
            public string CustomsForge_Link { get; set; }    //	CustomsForge_Link
            public string CustomsForge_Like { get; set; }    //	CustomsForge_Like
            public string CustomsForge_ReleaseNotes { get; set; }    //	CustomsForge_ReleaseNotes
            public string SignatureType { get; set; }
            public string ToolkitVersion { get; set; }
            public string Has_Author { get; set; }
            public string OggPath { get; set; }
            public string oggPreviewPath { get; set; }
            public string UniqueDLCName { get; set; }
            public string AlbumArt_Hash { get; set; }
            public string Audio_Hash { get; set; }
            public string AudioPreview_Hash { get; set; }
            public string Has_BassDD { get; set; }
            public string Has_Bonus_Arrangement { get; set; }
            public string Artist_ShortName { get; set; }
            public string Album_ShortName { get; set; }
            public string Available_Old { get; set; }
            public string Available_Duplicate { get; set; }
            public string Has_Been_Corrected { get; set; }
            public string File_Creation_Date { get; set; }
            public string Is_Live { get; set; }
            public string Live_Details { get; set; }
            public string Remote_Path { get; set; }
        }
        public GameVersion CurrentGameVersion
        {
            get { return GameVersion.RS2014; }
            set { }
        }

        public DLCManager()
        {
            InitializeComponent();

            //Enable Preview generation
            if (ConfigRepository.Instance()["general_wwisepath"] == "") ConfigRepository.Instance()["general_wwisepath"] = "C:\\Program Files (x86)\\Audiokinetic\\Wwise v2014.1.6 build 5318";
            if (ConfigRepository.Instance()["general_rs2014path"] == "") ConfigRepository.Instance()["general_rs2014path"] = "C:\\Program Files (x86)\\Steam\\Apps\\common\\Rocksmith2014";
            if (ConfigRepository.Instance()["general_defaultauthor"] == "") ConfigRepository.Instance()["general_defaultauthor"] = "catara";

            //C:\\Program Files (x86)\\Audiokinetic\\Wwise v2013.2.10 build 4884";

            //Colored = ConfigRepository.Instance().GetBoolean("cgm_coloredinlay");
            // Saving for later
            txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath"];
            txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath"];
            txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder"];
            txt_Title.Text = ConfigRepository.Instance()["dlcm_Title"];
            txt_Title_Sort.Text = ConfigRepository.Instance()["dlcm_Title_sort"];
            txt_Artist.Text = ConfigRepository.Instance()["dlcm_Artist"];
            txt_Artist_Sort.Text = ConfigRepository.Instance()["dlcm_Artist_Sort"];
            txt_Album.Text = ConfigRepository.Instance()["dlcm_Album"];
            txt_File_Name.Text = ConfigRepository.Instance()["dlcm_File_Name"];
            chbx_PC.Checked = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? true : false;
            chbx_Mac.Checked = (ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? true : false;
            chbx_PS3.Checked = (ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? true : false;
            chbx_XBOX360.Checked = (ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? true : false;
            chbx_DebugB.Checked = (ConfigRepository.Instance()["dlcm_Debug"] == "Yes") ? true : false;
            chbx_DefaultDB.Checked = (ConfigRepository.Instance()["dlcm_DefaultDB"] == "Yes") ? true : false;
            Set_DEBUG();
            cbx_Activ_Title.Checked = (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") ? true : false;
            cbx_Activ_Title_Sort.Checked = (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") ? true : false;
            cbx_Activ_Artist.Checked = (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") ? true : false;
            cbx_Activ_Artist_Sort.Checked = (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") ? true : false;
            cbx_Activ_Album.Checked = (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") ? true : false;
            cbx_Activ_File_Name.Checked = (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes") ? true : false;
            //cbx_Groups.Text = ConfigRepository.Instance()["dlcm_cbx_Groups"];
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul0"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(0, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(0, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul1"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(1, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(1, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul2"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(2, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(2, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul3"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(3, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(3, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul4"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(4, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(4, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul5"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(5, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(5, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul6"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(6, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(6, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul7"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(7, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(7, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(8, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(8, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul9"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(8, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(9, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul10"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(10, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(10, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul11"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(11, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(11, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul12"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(12, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(12, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul13"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(13, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(13, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul14"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(14, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(14, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul15"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul16"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(16, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(16, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul17"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(17, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(17, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul18"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(18, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(18, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul19"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(19, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(19, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul20"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(20, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(20, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(21, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(21, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul22"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(22, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(22, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(23, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(23, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul24"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(24, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(24, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul25"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(25, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(25, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul26"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(26, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(26, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul27"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(27, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(27, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul28"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(28, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(28, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul29"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(29, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(29, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul30"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(30, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(3, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul30"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(31, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(31, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul32"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(32, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(32, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul33"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(33, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(33, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul34"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(34, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(34, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul35"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(35, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(35, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul36"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(36, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(36, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul37"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(37, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(37, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul38"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(38, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(38, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul39"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(39, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(39, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul40"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(40, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(40, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul41"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(41, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(41, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul42"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(42, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(42, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul43"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(43, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(43, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul44"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(44, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(44, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul45"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(45, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(45, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul46"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(46, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(46, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(47, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(47, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(48, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(48, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul49"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul50"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(50, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(50, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul51"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(51, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(51, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(52, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(52, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(53, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(53, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul54"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(54, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(54, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(55, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(55, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul56"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(56, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(56, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul57"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(57, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(57, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(58, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(58, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(59, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(59, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(60, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(60, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(61, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(61, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul62"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(62, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(62, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul63"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(63, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(63, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul64"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(64, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(64, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul65"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(65, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(65, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul66"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(66, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(66, CheckState.Unchecked);

            //Initialize profiles chbx_Configurations
            // chbx_Configurations.Items[0] = ConfigRepository.Instance()["dlcm_Prof"];
            //chbx_Configurations.Items[1] = ConfigRepository.Instance()["dlcm_Prof2"];
            //chbx_Configurations.Items[2] = ConfigRepository.Instance()["dlcm_Prof3"];
            //chbx_Configurations.Items[3] = ConfigRepository.Instance()["dlcm_Prof4"];
            //chbx_Configurations.Items[4] = ConfigRepository.Instance()["dlcm_Prof5"];

            // Generate package worker
            //rtxt_StatisticsOnReadDLCs.Text = "genz : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            //rtxt_StatisticsOnReadDLCs.Text = "genn : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            //rtxt_StatisticsOnReadDLCs.Text = "genc : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            //rtxt_StatisticsOnReadDLCs.Text = "gen7 : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.WorkerReportsProgress = true;

            bwConvert.DoWork += new DoWorkEventHandler(ConvertWEM);
            //rtxt_StatisticsOnReadDLCs.Text = "genn : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            bwConvert.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            //rtxt_StatisticsOnReadDLCs.Text = "genc : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwConvert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            //rtxt_StatisticsOnReadDLCs.Text = "gen7 : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwConvert.WorkerReportsProgress = true;
        }

        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_RocksmithDLCPath.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
            }
        }

        private void btn_TempPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_TempPath.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
            }
        }

        private void btn_DBFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_DBFolder.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
            }
        }

        private void chbx_DebugB_CheckedChanged(object sender, EventArgs e)
        {
            Set_DEBUG();
        }

        private void Set_DEBUG()
        {
            if (chbx_DebugB.Checked)
            {
                txt_RocksmithDLCPath.Text = "c:\\GitHub\\tmp\\to import"; x6 = ConfigRepository.Instance()["dlcm_RocksmithDLCPath"]; ;
                txt_DBFolder.Text = "C:\\GitHub\\tmp"; x7 = ConfigRepository.Instance()["dlcm_DBFolder"];
                txt_TempPath.Text = "C:\\GitHub\\tmp\\0"; x8 = ConfigRepository.Instance()["dlcm_TempPath"];
                //x1 = chbx_CleanTemp.Checked; chbx_CleanTemp.Checked = true;
                //x2 = chbx_CleanDB.Checked; chbx_CleanDB.Checked = true;
                //chbx_HomeDBG.Visible = true;
                //chbx_WorkDGB.Visible = true;
                //chbx_HomeDGBVM.Visible = true;
                chbx_Rebuild.Visible = true;
                chbx_Additional_Manipulations.Visible = true;
                //lbl_Log.Visible = true;
                rtxt_StatisticsOnReadDLCs.Visible = true;
                //chbx_DefaultDB.Checked = false; 
                //x3 = chbx_DefaultDB.Checked;
                //chbx_Additional_Manipulations.SetItemCheckState(24, CheckState.Unchecked); x4 = ConfigRepository.Instance()["dlcm_AdditionalManipul24"] == "Yes" ? true : false;
                //chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked); x5 = ConfigRepository.Instance()["dlcm_AdditionalManipul15"] == "Yes" ? true : false;
                //chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Unchecked); x9 = ConfigRepository.Instance()["dlcm_AdditionalManipul49"] == "Yes" ? true : false;

                chbx_CleanTemp.Visible = true;
                chbx_CleanDB.Visible = true;
                chbx_DefaultDB.Visible = true;
                lbl_Title.Visible = true;
                txt_Title.Visible = true;
                cbx_Title.Visible = true;
                cbx_Activ_Title.Visible = true;
                btn_Preview_Title.Visible = true;
                lbl_Title_Sort.Visible = true;
                txt_Title_Sort.Visible = true;
                cbx_Title_Sort.Visible = true;
                cbx_Activ_Title_Sort.Visible = true;
                btn_Preview_Title_Sort.Visible = true;
                lbl_Artist.Visible = true;
                txt_Artist.Visible = true;
                cbx_Artist.Visible = true;
                cbx_Activ_Artist.Visible = true;
                btn_Preview_Artist.Visible = true;
                txt_Artist_Sort.Visible = true;
                cbx_Artist_Sort.Visible = true;
                cbx_Activ_Artist_Sort.Visible = true;
                btn_Preview_Artist_Sort.Visible = true;
                lbl_Album.Visible = true;
                txt_Album.Visible = true;
                cbx_Album.Visible = true;
                cbx_Activ_Album.Visible = true;
                btn_Preview_Album.Visible = true;
                lbl_File_Name.Visible = true;
                txt_File_Name.Visible = true;
                cbx_File_Name.Visible = true;
                cbx_Activ_File_Name.Visible = true;
                btn_Preview_File_Name.Visible = true;
                lbl_PreviewText.Visible = true;
                lbl_Mask.Visible = true;
                lbl_Artist_Sort.Visible = true;

            }
            else
            {
                ////chbx_CleanTemp.Checked = false;
                ////chbx_CleanDB.Checked = false;
                ////txt_RocksmithDLCPath.Text = "";
                ////txt_DBFolder.Text = "";
                ////txt_TempPath.Text = "";
                ////txt_TempPath.Text = "";
                //chbx_HomeDBG.Visible = false;
                //chbx_WorkDGB.Visible = false;
                //chbx_HomeDGBVM.Visible = false;
                chbx_Additional_Manipulations.Visible = false;
                ////lbl_Log.Visible = false;
                //chbx_CleanTemp.Checked = x1;
                //chbx_CleanDB.Checked = x2;

                rtxt_StatisticsOnReadDLCs.Visible = false;

                //chbx_Additional_Manipulations.SetItemCheckState(24, x4 ? CheckState.Checked : CheckState.Unchecked);
                //chbx_Additional_Manipulations.SetItemCheckState(15, x5 ? CheckState.Checked : CheckState.Unchecked);
                //chbx_Additional_Manipulations.SetItemCheckState(49, x9 ? CheckState.Checked : CheckState.Unchecked);

                if (x6 != null) txt_RocksmithDLCPath.Text = x6;
                if (x8 != null) txt_DBFolder.Text = x7;
                if (x7 != null) txt_TempPath.Text = x8;

                chbx_CleanTemp.Visible = false;
                chbx_CleanDB.Visible = false;
                chbx_DefaultDB.Visible = false;
                lbl_Title.Visible = false;
                txt_Title.Visible = false;
                cbx_Title.Visible = false;
                cbx_Activ_Title.Visible = false;
                btn_Preview_Title.Visible = false;
                lbl_Title_Sort.Visible = false;
                txt_Title_Sort.Visible = false;
                cbx_Title_Sort.Visible = false;
                cbx_Activ_Title_Sort.Visible = false;
                btn_Preview_Title_Sort.Visible = false;
                lbl_Artist.Visible = false;
                txt_Artist.Visible = false;
                cbx_Artist.Visible = false;
                cbx_Activ_Artist.Visible = false;
                btn_Preview_Artist.Visible = false;
                txt_Artist_Sort.Visible = false;
                cbx_Artist_Sort.Visible = false;
                cbx_Activ_Artist_Sort.Visible = false;
                btn_Preview_Artist_Sort.Visible = false;
                lbl_Album.Visible = false;
                txt_Album.Visible = false;
                cbx_Album.Visible = false;
                cbx_Activ_Album.Visible = false;
                btn_Preview_Album.Visible = false;
                lbl_File_Name.Visible = false;
                txt_File_Name.Visible = false;
                cbx_File_Name.Visible = false;
                cbx_Activ_File_Name.Visible = false;
                btn_Preview_File_Name.Visible = false;
                lbl_PreviewText.Visible = false;
                lbl_Mask.Visible = false;

            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //Author = ConfigRepository.Instance()["general_defaultauthor"];
            //InlayName = ConfigRepository.Instance()["cgm_inlayname"];
            //Frets24 = ConfigRepository.Instance().GetBoolean("cgm_24frets");
            //Colored = ConfigRepository.Instance().GetBoolean("cgm_coloredinlay");
            // Saving for later
            SaveSettings();
            ((MainForm)ParentForm).ReloadControls();
        }

        public void SaveSettings()
        {
            ConfigRepository.Instance()["dlcm_Title"] = txt_Title.Text;
            ConfigRepository.Instance()["dlcm_Title_sort"] = txt_Title_Sort.Text;
            ConfigRepository.Instance()["dlcm_Artist"] = txt_Artist.Text;
            ConfigRepository.Instance()["dlcm_Artist_Sort"] = txt_Artist_Sort.Text;
            ConfigRepository.Instance()["dlcm_Album"] = txt_Album.Text;
            ConfigRepository.Instance()["dlcm_File_Name"] = txt_File_Name.Text;
            ConfigRepository.Instance()["dlcm_chbx_PC"] = chbx_PC.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_Mac"] = chbx_Mac.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_PS3"] = chbx_PS3.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_XBOX360"] = chbx_XBOX360.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Debug"] = chbx_DebugB.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DefaultDB"] = chbx_DefaultDB.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_Title"] = cbx_Activ_Title.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_TitleSort"] = cbx_Activ_Title_Sort.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_Artist"] = cbx_Activ_Artist.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] = cbx_Activ_Artist_Sort.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_Album"] = cbx_Activ_Album.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_FileName"] = cbx_Activ_File_Name.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_cbx_Groups"] = cbx_Groups.Text;
            ConfigRepository.Instance()["dlcm_AdditionalManipul0"] = chbx_Additional_Manipulations.GetItemChecked(0) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul1"] = chbx_Additional_Manipulations.GetItemChecked(1) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul2"] = chbx_Additional_Manipulations.GetItemChecked(2) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul3"] = chbx_Additional_Manipulations.GetItemChecked(3) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul4"] = chbx_Additional_Manipulations.GetItemChecked(4) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul5"] = chbx_Additional_Manipulations.GetItemChecked(5) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul6"] = chbx_Additional_Manipulations.GetItemChecked(6) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul7"] = chbx_Additional_Manipulations.GetItemChecked(7) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul8"] = chbx_Additional_Manipulations.GetItemChecked(8) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul9"] = chbx_Additional_Manipulations.GetItemChecked(9) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul10"] = chbx_Additional_Manipulations.GetItemChecked(10) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul11"] = chbx_Additional_Manipulations.GetItemChecked(11) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul12"] = chbx_Additional_Manipulations.GetItemChecked(12) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul13"] = chbx_Additional_Manipulations.GetItemChecked(13) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul14"] = chbx_Additional_Manipulations.GetItemChecked(14) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul15"] = chbx_Additional_Manipulations.GetItemChecked(15) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul16"] = chbx_Additional_Manipulations.GetItemChecked(16) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul17"] = chbx_Additional_Manipulations.GetItemChecked(17) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul18"] = chbx_Additional_Manipulations.GetItemChecked(18) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul19"] = chbx_Additional_Manipulations.GetItemChecked(19) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul20"] = chbx_Additional_Manipulations.GetItemChecked(20) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul21"] = chbx_Additional_Manipulations.GetItemChecked(21) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul22"] = chbx_Additional_Manipulations.GetItemChecked(22) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul23"] = chbx_Additional_Manipulations.GetItemChecked(23) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul24"] = chbx_Additional_Manipulations.GetItemChecked(24) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul25"] = chbx_Additional_Manipulations.GetItemChecked(25) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul26"] = chbx_Additional_Manipulations.GetItemChecked(26) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul27"] = chbx_Additional_Manipulations.GetItemChecked(27) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul28"] = chbx_Additional_Manipulations.GetItemChecked(28) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul29"] = chbx_Additional_Manipulations.GetItemChecked(29) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul30"] = chbx_Additional_Manipulations.GetItemChecked(30) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul31"] = chbx_Additional_Manipulations.GetItemChecked(31) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul32"] = chbx_Additional_Manipulations.GetItemChecked(32) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul33"] = chbx_Additional_Manipulations.GetItemChecked(33) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul34"] = chbx_Additional_Manipulations.GetItemChecked(34) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul35"] = chbx_Additional_Manipulations.GetItemChecked(35) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul36"] = chbx_Additional_Manipulations.GetItemChecked(36) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul37"] = chbx_Additional_Manipulations.GetItemChecked(37) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul38"] = chbx_Additional_Manipulations.GetItemChecked(38) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul39"] = chbx_Additional_Manipulations.GetItemChecked(39) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul40"] = chbx_Additional_Manipulations.GetItemChecked(40) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul41"] = chbx_Additional_Manipulations.GetItemChecked(41) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul42"] = chbx_Additional_Manipulations.GetItemChecked(42) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul43"] = chbx_Additional_Manipulations.GetItemChecked(43) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul44"] = chbx_Additional_Manipulations.GetItemChecked(44) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul45"] = chbx_Additional_Manipulations.GetItemChecked(45) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul46"] = chbx_Additional_Manipulations.GetItemChecked(46) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul47"] = chbx_Additional_Manipulations.GetItemChecked(47) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul48"] = chbx_Additional_Manipulations.GetItemChecked(48) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul49"] = chbx_Additional_Manipulations.GetItemChecked(49) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul50"] = chbx_Additional_Manipulations.GetItemChecked(50) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul51"] = chbx_Additional_Manipulations.GetItemChecked(51) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul52"] = chbx_Additional_Manipulations.GetItemChecked(52) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul53"] = chbx_Additional_Manipulations.GetItemChecked(53) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul54"] = chbx_Additional_Manipulations.GetItemChecked(54) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul55"] = chbx_Additional_Manipulations.GetItemChecked(55) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul56"] = chbx_Additional_Manipulations.GetItemChecked(56) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul57"] = chbx_Additional_Manipulations.GetItemChecked(57) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul58"] = chbx_Additional_Manipulations.GetItemChecked(58) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul59"] = chbx_Additional_Manipulations.GetItemChecked(59) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul60"] = chbx_Additional_Manipulations.GetItemChecked(60) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul61"] = chbx_Additional_Manipulations.GetItemChecked(61) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul62"] = chbx_Additional_Manipulations.GetItemChecked(62) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul63"] = chbx_Additional_Manipulations.GetItemChecked(63) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul64"] = chbx_Additional_Manipulations.GetItemChecked(63) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul65"] = chbx_Additional_Manipulations.GetItemChecked(63) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul66"] = chbx_Additional_Manipulations.GetItemChecked(63) ? "Yes" : "No";

            //ConfigRepository.Instance()["dlcm_AdditionalManipul60"] = chbx_Additional_Manipulations.GetItemChecked(60) ? "Yes" : "No";

            //Save Profiles
            if (chbx_Configurations.SelectedIndex >= 0)
            {
                DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\";");
                var norec = ds.Tables[0].Rows.Count;
                if (norec == 3)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    //string insertcmdA = "CDLC_ID, Groups, Type, Comments, Profile_Name";
                    //var insertA = "\"" + fnn + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"Profile\",\"Rocksmith\",\"" + chbx_Configurations.Text + "\"";
                    //DLCManager.InsertIntoDBwValues("Groups", insertcmdA, insertA);
                    var cmd = "UPDATE Groups SET Groups=\"" + txt_RocksmithDLCPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Rocksmith\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"";
                    UpdateDB("Groups", cmd); //txt_DBFolder.Text'ConfigRepository.Instance()["dlcm_DBFolder"]
                    UpdateDB("Groups", "UPDATE Groups SET Groups=\"" + txt_DBFolder.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"DB\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"");
                    UpdateDB("Groups", "UPDATE Groups SET Groups=\"" + txt_TempPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Temp\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"");
                }
                //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath1"] = txt_RocksmithDLCPath.Text;
                //    ConfigRepository.Instance()["dlcm_TempPath1"] = txt_DBFolder.Text;
                //    ConfigRepository.Instance()["dlcm_DBFolder1"] = txt_TempPath.Text;
                //}
                //else if (chbx_Configurations.SelectedIndex == 1)
                //{
                //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath2"] = txt_RocksmithDLCPath.Text;
                //    ConfigRepository.Instance()["dlcm_TempPath2"] = txt_DBFolder.Text;
                //    ConfigRepository.Instance()["dlcm_DBFolder2"] = txt_TempPath.Text;
                //}
                //else if (chbx_Configurations.SelectedIndex == 2)
                //{
                //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath3"] = txt_RocksmithDLCPath.Text;
                //    ConfigRepository.Instance()["dlcm_TempPath3"] = txt_DBFolder.Text;
                //    ConfigRepository.Instance()["dlcm_DBFolder3"] = txt_TempPath.Text;
                //}
                //else if (chbx_Configurations.SelectedIndex == 3)
                //{
                //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath4"] = txt_RocksmithDLCPath.Text;
                //    ConfigRepository.Instance()["dlcm_TempPath4"] = txt_DBFolder.Text;
                //    ConfigRepository.Instance()["dlcm_DBFolder4"] = txt_TempPath.Text;
                //}
                //else if (chbx_Configurations.SelectedIndex == 4)
                //{
                //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath5"] = txt_RocksmithDLCPath.Text;
                //    ConfigRepository.Instance()["dlcm_TempPath5"] = txt_DBFolder.Text;
                //    ConfigRepository.Instance()["dlcm_DBFolder5"] = txt_TempPath.Text;
            }
            ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] = txt_RocksmithDLCPath.Text;
            ConfigRepository.Instance()["dlcm_TempPath"] = txt_TempPath.Text;
            ConfigRepository.Instance()["dlcm_DBFolder"] = txt_DBFolder.Text;
        }

        private void cbx_Activ_Title_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Title.Checked == false)
            {
                txt_Title.Enabled = false;
                cbx_Title.Enabled = false;
            }
            else
            {
                txt_Title.Enabled = true;
                cbx_Title.Enabled = true;
            }
        }

        private void cbx_Activ_Title_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Title_Sort.Checked == false)
            {
                txt_Title_Sort.Enabled = false;
                cbx_Title_Sort.Enabled = false;
            }
            else
            {
                txt_Title_Sort.Enabled = true;
                cbx_Title_Sort.Enabled = true;
            }
        }

        private void cbx_Activ_Artist_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Artist.Checked == false)
            {
                txt_Artist.Enabled = false;
                cbx_Artist.Enabled = false;
            }
            else
            {
                txt_Artist.Enabled = true;
                cbx_Artist.Enabled = true;
            }
        }

        private void cbx_Activ_Artist_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Artist_Sort.Checked == false)
            {
                txt_Artist_Sort.Enabled = false;
                cbx_Artist_Sort.Enabled = false;
            }
            else
            {
                txt_Artist_Sort.Enabled = true;
                cbx_Artist_Sort.Enabled = true;
            }
        }

        private void cbx_Activ_Album_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Album.Checked == false)
            {
                txt_Album.Enabled = false;
                cbx_Album.Enabled = false;
            }
            else
            {
                txt_Album.Enabled = true;
                cbx_Album.Enabled = true;
            }
        }

        private void cbx_Activ_File_Name_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_File_Name.Checked == false)
            {
                txt_File_Name.Enabled = false;
                cbx_File_Name.Enabled = false;
            }
            else
            {
                txt_File_Name.Enabled = true;
                cbx_File_Name.Enabled = true;
            }
        }

        private void btn_Preview_Title_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Title: " + DLCManager.Manipulate_strings(txt_Title.Text, -1, false, false, false, files);
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Title: " + DLCManager.Manipulate_strings(txt_Title_Sort.Text, -1, false, false, false, files);
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Artist: " + DLCManager.Manipulate_strings(txt_Artist.Text, -1, false, false, false, files);
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Artist: " + DLCManager.Manipulate_strings(txt_Artist_Sort.Text, -1, false, false, false, files);
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Album: " + DLCManager.Manipulate_strings(txt_Album.Text, -1, false, false, false, files);
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "FileName: " + DLCManager.Manipulate_strings(txt_File_Name.Text, -1, true, false, false, files);
        }

        public static string Manipulate_strings(string words, int k, bool ifn, bool orig_flag, bool bassRemoved, Files[] files)
        {
            //Read from DB
      //      int norows = 0;
            //1. Get Random Song ID
         //   var cmd = "";
            //if (k == -1)
            //{
            //    k = 0;
            //    cmd = "SELECT TOP 1 * FROM Main";
            //    //Files IDs = new Files();
            //    norows = SQLAccess(cmd);
            //}

            //2. Read from DB
            //cmd = "SELECT * FROM Main WHERE ID = " + files[k].ID;
            //norows = SQLAccess(cmd);
            //rtxt_StatisticsOnReadDLCs.Text ="f" + rtxt_StatisticsOnReadDLCs.Text;
            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = "";
            var readt = false;
            var oldtxt = "";
            var last_ = 0;
            for (i = 0; i <= txt.Length - 1; i++)
            {
                //rtxt_StatisticsOnReadDLCs.Text = k  +"\n"+ rtxt_StatisticsOnReadDLCs.Text;
                curtext = txt[i].ToString();//.Substring(i,1);
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
                }

                if (readt == true)
                    curelem += curtext;
                else fulltxt += curtext;

                bool origQAs = true;
                if (orig_flag) origQAs = false;
                //else origQAs = false;

                oldtxt = fulltxt;
                if (curtext == ">")
                {
                    readt = false;
                    switch (curelem)
                    {
                        case "<Artist>":
                            fulltxt += files[k].Artist;
                            break;
                        case "<Title>":
                            fulltxt += files[k].Song_Title;
                            break;
                        case "<Version>":
                            fulltxt += files[k].Version;
                            break;
                        case "<DLCName>":
                            fulltxt += files[k].DLC_Name;
                            break;
                        case "<Album>":
                            fulltxt += files[k].Album;
                            break;
                        case "<Track No.>":
                            fulltxt += ((files[k].Track_No != "") ? ("-" + files[k].Track_No) : "");
                            break;
                        case "<Year>":
                            fulltxt += files[k].Album_Year;
                            break;
                        case "<Rating>":
                            fulltxt += ((files[k].Rating == "") ? "" : "r." + files[k].Rating);
                            break;
                        case "<Alt. Vers.>":
                            fulltxt += files[k].Alternate_Version_No == "" ? "" : "a." + files[k].Alternate_Version_No;
                            break;
                        case "<Descr.>":
                            fulltxt += files[k].Description;
                            break;
                        case "<Comm.>":
                            fulltxt += files[k].Comments;
                            break;
                        case "<Tuning>":
                            fulltxt += files[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            fulltxt += ((files[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((files[k].Has_Bass == "Yes") ? "B" : ""); //not yet done for all arrangements
                            break;
                        case "<MTrack Det.>":
                            fulltxt += files[k].MultiTrack_Version;//?
                            break;
                        case "<Group>":
                            fulltxt += files[k].Groups;
                            break;
                        case "<Beta>":
                            fulltxt = ((files[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
                            break;
                        case "<Broken>":
                            fulltxt = ((files[k].Is_Broken == "Yes") ? "<B>" : "") + fulltxt;
                            break;
                        case "<File Name>":
                            fulltxt += files[k].Current_FileName;
                            break;
                        case "<Bonus>":
                            fulltxt += ((files[k].Has_Bonus_Arrangement == "Yes") ? "Bonus" : ""); //not yet done for all arrangements
                            break;
                        case "<Live>":
                            fulltxt += ((files[k].Is_Live == "Yes") ? "Live "+ files[k].Live_Details : ""); //not yet done for all arrangements
                            break;
                        case "<Artist Short>":
                            fulltxt += files[k].Artist_ShortName != "" ? files[k].Artist_ShortName : files[k].Artist;
                            break;
                        case "<Album Short>":
                            fulltxt += files[k].Album_ShortName != "" ? files[k].Album_ShortName : files[k].Album;
                            break;
                        case "<Author>":
                            fulltxt += files[k].Author;
                            break;
                        case "<Space>":
                            fulltxt += " ";
                            break;
                        case "<Avail. Tracks>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Lead == "Yes") ? "L" : "") + ((files[k].Has_Combo == "Yes") ? "C" : "") + ((files[k].Has_Rhythm == "Yes") ? "R" : "") + ((files[k].Has_Vocals == "Yes") ? "V" : "");
                            break;
                        case "<Bass_HasDD>":
                            fulltxt += ((files[k].Has_BassDD == "No" || bassRemoved) && files[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        case "<Timestamp>":
                            fulltxt += System.DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
                            break;
                        default:
                            if ((origQAs) || (ifn))
                            {
                                switch (curelem)
                                {
                                    case "<DD>":
                                        fulltxt += files[k].Has_DD == "Yes" ? "DD" : "noDD";
                                        break;
                                    case "<CDLC>":
                                        fulltxt += files[k].DLC;
                                        break;
                                    case "<QAs>":
                                        fulltxt += (((files[k].Has_Cover == "No") || (files[k].Has_Preview == "No") || (files[k].Has_Vocals == "No")) ? "NOs-" : "") + ((files[k].Has_Cover == "Yes") ? "" : "C") + ((files[k].Has_Preview == "Yes") ? "" : "P") + ((files[k].Has_Vocals == "Yes") ? "" : "V"); //+((files[k].Has_Sections == "Yes") ? "" : "S")
                                        break;
                                    case "<lastConversionDateTime>":
                                        fulltxt += files[k].Import_Date; //not yet done
                                        break;
                                    default: break;
                                }
                            }
                            break;
                    }
                    if (oldtxt == fulltxt && last_ > 0)
                        //{
                        //    rtxt_StatisticsOnReadDLCs.Text = i + "...\n"+rtxt_StatisticsOnReadDLCs.Text;                        
                        fulltxt = fulltxt.Substring(0, last_);
                    //}
                    last_ = fulltxt.Length;
                }
            }
            //rtxt_StatisticsOnReadDLCs.Text = "returning " + i + " char " + fulltxt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return fulltxt;
        }

        //Generic procedure to read and parse Main.DB (&others..soon)
        static public int SQLAccess(string cmd) //static
        {
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //Files[] files = new Files[10000];
            //rtxt_StatisticsOnReadDLCs.Text = "re" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var MaximumSize = 0;
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            //DataSet dus = new DataSet();
            //OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //dax.Fill(dus, "Main");
            //dax.Dispose();
            DataSet dus = new DataSet(); dus = DLCManager.SelectFromDB("Main", cmd);

            var i = 0;
            MaximumSize = dus.Tables[0].Rows.Count;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                files[i] = new Files();

                files[i].ID = dataRow.ItemArray[0].ToString();
                files[i].Song_Title = dataRow.ItemArray[1].ToString();
                files[i].Song_Title_Sort = dataRow.ItemArray[2].ToString();
                files[i].Album = dataRow.ItemArray[3].ToString();
                files[i].Artist = dataRow.ItemArray[4].ToString();
                files[i].Artist_Sort = dataRow.ItemArray[5].ToString();
                files[i].Album_Year = dataRow.ItemArray[6].ToString();
                files[i].AverageTempo = dataRow.ItemArray[7].ToString();
                files[i].Volume = dataRow.ItemArray[8].ToString();
                files[i].Preview_Volume = dataRow.ItemArray[9].ToString();
                files[i].AlbumArtPath = dataRow.ItemArray[10].ToString();
                files[i].AudioPath = dataRow.ItemArray[11].ToString();
                files[i].audioPreviewPath = dataRow.ItemArray[12].ToString();
                files[i].Track_No = dataRow.ItemArray[13].ToString();
                files[i].Author = dataRow.ItemArray[14].ToString();
                files[i].Version = dataRow.ItemArray[15].ToString();
                files[i].DLC_Name = dataRow.ItemArray[16].ToString();
                files[i].DLC_AppID = dataRow.ItemArray[17].ToString();
                files[i].Current_FileName = dataRow.ItemArray[18].ToString();
                files[i].Original_FileName = dataRow.ItemArray[19].ToString();
                files[i].Import_Path = dataRow.ItemArray[20].ToString();
                files[i].Import_Date = dataRow.ItemArray[21].ToString();
                files[i].Folder_Name = dataRow.ItemArray[22].ToString();
                files[i].File_Size = dataRow.ItemArray[23].ToString();
                files[i].File_Hash = dataRow.ItemArray[24].ToString();
                files[i].Original_File_Hash = dataRow.ItemArray[25].ToString();
                files[i].Is_Original = dataRow.ItemArray[26].ToString();
                files[i].Is_OLD = dataRow.ItemArray[27].ToString();
                files[i].Is_Beta = dataRow.ItemArray[28].ToString();
                files[i].Is_Alternate = dataRow.ItemArray[29].ToString();
                files[i].Is_Multitrack = dataRow.ItemArray[30].ToString();
                files[i].Is_Broken = dataRow.ItemArray[31].ToString();
                files[i].MultiTrack_Version = dataRow.ItemArray[32].ToString();
                files[i].Alternate_Version_No = dataRow.ItemArray[33].ToString();
                files[i].DLC = dataRow.ItemArray[34].ToString();
                files[i].Has_Bass = dataRow.ItemArray[35].ToString();
                files[i].Has_Guitar = dataRow.ItemArray[36].ToString();
                files[i].Has_Lead = dataRow.ItemArray[37].ToString();
                files[i].Has_Rhythm = dataRow.ItemArray[38].ToString();
                files[i].Has_Combo = dataRow.ItemArray[39].ToString();
                files[i].Has_Vocals = dataRow.ItemArray[40].ToString();
                files[i].Has_Sections = dataRow.ItemArray[41].ToString();
                files[i].Has_Cover = dataRow.ItemArray[42].ToString();
                files[i].Has_Preview = dataRow.ItemArray[43].ToString();
                files[i].Has_Custom_Tone = dataRow.ItemArray[44].ToString();
                files[i].Has_DD = dataRow.ItemArray[45].ToString();
                files[i].Has_Version = dataRow.ItemArray[46].ToString();
                files[i].Tunning = dataRow.ItemArray[47].ToString();
                files[i].Bass_Picking = dataRow.ItemArray[48].ToString();
                files[i].Tones = dataRow.ItemArray[49].ToString();
                files[i].Groups = dataRow.ItemArray[50].ToString();
                files[i].Rating = dataRow.ItemArray[51].ToString();
                files[i].Description = dataRow.ItemArray[52].ToString();
                files[i].Comments = dataRow.ItemArray[53].ToString();
                files[i].Has_Track_No = dataRow.ItemArray[54].ToString();
                files[i].Platform = dataRow.ItemArray[55].ToString();
                files[i].PreviewTime = dataRow.ItemArray[56].ToString();
                files[i].PreviewLenght = dataRow.ItemArray[57].ToString();
                files[i].Youtube_Playthrough = dataRow.ItemArray[58].ToString();
                files[i].CustomForge_Followers = dataRow.ItemArray[59].ToString();
                files[i].CustomForge_Version = dataRow.ItemArray[60].ToString();
                files[i].FilesMissingIssues = dataRow.ItemArray[61].ToString();
                files[i].Duplicates = dataRow.ItemArray[62].ToString();
                files[i].Pack = dataRow.ItemArray[63].ToString();
                files[i].Keep_BassDD = dataRow.ItemArray[64].ToString();
                files[i].Keep_DD = dataRow.ItemArray[65].ToString();
                files[i].Keep_Original = dataRow.ItemArray[66].ToString();
                files[i].Song_Lenght = dataRow.ItemArray[67].ToString();
                files[i].Original = dataRow.ItemArray[68].ToString();
                files[i].Selected = dataRow.ItemArray[69].ToString();
                files[i].YouTube_Link = dataRow.ItemArray[70].ToString();
                files[i].CustomsForge_Link = dataRow.ItemArray[71].ToString();
                files[i].CustomsForge_Like = dataRow.ItemArray[72].ToString();
                files[i].CustomsForge_ReleaseNotes = dataRow.ItemArray[73].ToString();
                files[i].SignatureType = dataRow.ItemArray[74].ToString();
                files[i].ToolkitVersion = dataRow.ItemArray[75].ToString();
                files[i].Has_Author = dataRow.ItemArray[76].ToString();
                files[i].OggPath = dataRow.ItemArray[77].ToString();
                files[i].oggPreviewPath = dataRow.ItemArray[78].ToString();
                files[i].UniqueDLCName = dataRow.ItemArray[79].ToString();
                files[i].AlbumArt_Hash = dataRow.ItemArray[80].ToString();
                files[i].Audio_Hash = dataRow.ItemArray[81].ToString();
                files[i].AudioPreview_Hash = dataRow.ItemArray[82].ToString();
                files[i].Has_BassDD = dataRow.ItemArray[83].ToString();
                files[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
                files[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
                files[i].Album_ShortName = dataRow.ItemArray[86].ToString();
                files[i].Available_Old = dataRow.ItemArray[87].ToString();
                files[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
                files[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
                files[i].File_Creation_Date = dataRow.ItemArray[90].ToString();
                files[i].Is_Live = dataRow.ItemArray[91].ToString();
                files[i].Live_Details = dataRow.ItemArray[92].ToString();
                files[i].Remote_Path = dataRow.ItemArray[93].ToString();
                i++;
            }
            //Closing Connection
            //dax.Dispose();
            //cnn.Close();
            //rtxt_StatisticsOnReadDLCs.Text = "r" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //	0	 	ID
            //	1	 	Song_Title
            //	2	 	Song_Title_Sort
            //	3	 	Album
            //	4	 	Artist
            //	5	 	Artist_Sort
            //	6	 	Album_Year
            //	7	 	AverageTempo
            //	8	 	Volume
            //	9	 	Preview_Volume
            //	10	 	AlbumArtPath
            //	11	 	AudioPath
            //	12	 	audioPreviewPath
            //	13	 	Track_No
            //	14	 	Author
            //	15	 	Version
            //	16	 	DLC_Name
            //	17	 	DLC_AppID
            //	18	 	Current_FileName
            //	19	 	Original_FileName
            //	20	 	Import_Path
            //	21	 	Import_Date
            //	22	 	Folder_Name
            //	23	 	File_Size
            //	24	 	File_Hash
            //	25	 	Original_File_Hash
            //	26	 	Is_Original
            //	27	 	Is_OLD
            //	28	 	Is_Beta
            //	29	 	Is_Alternate
            //	30	 	Is_Multitrack
            //	31	 	Is_Broken
            //	32	 	MultiTrack_Version
            //	33	 	Alternate_Version_No
            //	34	 	DLC
            //	35	 	Has_Bass
            //	36	 	Has_Guitar
            //	37	 	Has_Lead
            //	38	 	Has_Rhythm
            //	39	 	Has_Combo
            //	40	 	Has_Vocals
            //	41	 	Has_Sections
            //	42	 	Has_Cover
            //	43	 	Has_Preview
            //	44	 	Has_Custom_Tone
            //	45	 	Has_DD
            //	46	 	Has_Version
            //	47	 	Tunning
            //	48	 	Bass_Picking
            //	49	 	Tones
            //	50	 	Groups
            //	51	 	Rating
            //	52	 	Description
            //	53	 	Comments
            //	54	 	Has_Track_No
            //	55	 	Platform
            //	56	 	PreviewTime
            //	57	 	PreviewLenght
            //	58	 	Temp
            //	59	 	CustomForge_Followers
            //	60	 	CustomForge_Version
            //	61	 	FilesMissingIssues
            //	62	 	Duplicates
            //	63	 	Pack
            //	64	 	Keep_BassDD
            //	65	 	Keep_DD
            //	66	 	Keep_Original
            //	67	 	Song_Lenght
            //	68	 	Original
            //	69	 	Selected
            //	70	 	YouTube_Link
            //	71	 	CustomsForge_Link
            //	72	 	CustomsForge_Like
            //	73	 	CustomsForge_ReleaseNotes
            //	74	 	SignatureType
            //	75	 	ToolkitVersion
            //	76	 	Has_Author
            //	77	 	OggPath
            //	78	 	oggPreviewPath
            //	79	 	UniqueDLCName
            //	80	 	AlbumArt_Hash
            //	81	 	Audio_Hash
            //	82	 	audioPreview_Hash
            //	83	 	Bass_Has_DD
            //	84	 	Has_Bonus_Arrangement
            //	85	 	Artist_ShortName
            //	86	 	Album_ShortName
            //	87	 	Available_Old
            //	88	 	Available_Duplicate
            //	89	 	Has_Been_Corrected
            // 90 File_Creation_Date
            //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Main DB connection in SQLAccess ! " + DB_Path + "-" + cmd);
            //    ErrorWindow frm1 = new ErrorWindow("an not open Main DB connection in SQLAccess. Try to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
            //    frm1.ShowDialog();
            //}
            //rtxt_StatisticsOnReadDLCs.Text = "rning " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return MaximumSize;//files[10000];
        }

        public object NullHandler(object instance)
        {
            if (instance != null)
                return instance.ToString();


            return DBNull.Value.ToString();// DBNull.Value;
        }

        private void Export_To_Click(object sender, EventArgs e)
        {
            if (cbx_Export.SelectedValue.ToString() == "Excel") ExportExcel();
        }

        public void ExportExcel()
        {
   //         SqlConnection cnn;
         //   string connectionString = null;
          //  string sql = null;
            string data = null;
            int i = 0;
            int j = 0;

            //MessageBox.Show("test");

            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlApp = new Excel.Application();
            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path;
            //cnn = new SqlConnection(connectionString);
            //cnn.Open();
            //sql = "SELECT * FROM Main";
            //SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            //DataSet ds = new DataSet();
            //dscmd.Fill(ds);
            //dscmd.Dispose();

            DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Main", "SELECT * FROM Main");
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                {
                    data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                    //xlWorkSheet.Cells[i + 1, j + 1] = data;
                }
            }

            //xlWorkBook.SaveAs("csharp.net-informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //releaseObject(xlWorkSheet);
            //releaseObject(xlWorkBook);
            //releaseObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file c:\\csharp.net-informations.xls");

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void CleanJSON(string ss)
        {


            //var info = File.OpenText(ss);
            //rtxt_StatisticsOnReadDLCs.Text = "-"+ss + rtxt_StatisticsOnReadDLCs.Text;
            //StreamWriter sr = new StreamWriter(ss);

            ////Read the first line of text
            //var line = sr.ReadLine();
            ////Continue to read until you reach end of file
            //while (line != null)
            //{
            //    rtxt_StatisticsOnReadDLCs.Text = "-" + rtxt_StatisticsOnReadDLCs.Text;
            //    //write the lie to console window
            //    Console.WriteLine(line);
            //    //Read the next line
            //    if (line.Contains("\"SongName\": \""))
            //    {
            //        rtxt_StatisticsOnReadDLCs.Text = line + rtxt_StatisticsOnReadDLCs.Text;
            //        sr.WriteLin(line.Replace("?", ""));
            //        rtxt_StatisticsOnReadDLCs.Text = line + rtxt_StatisticsOnReadDLCs.Text;
            //        //break;
            //    }
            //    line = sr.ReadLine();
            // }

            ////close the file
            //sr.Close();
        }

        private void btn_Cleanup_MainDB_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            //else if (rbtn_Population_All.Checked) ;
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\" " + cbx_Groups.SelectedText + "\")";
            //MessageBox.Show("-1");
            //cmd += "ORDER BY Artist";
            //Read from DB
            int norows = 0;
            int i = 1;
            norows = SQLAccess(cmd);
            cmd = "DELETE FROM Main WHERE ID IN (";
            var ids = "";
            var hash = "";
            if (norows > 0)
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        ids += file.ID.ToString();
                        hash += file.Original_File_Hash.ToString();
                        i++;
                        if (i <= norows) { ids += ", "; hash += ", "; }
                        if (i > norows) break;
                    }
                }
            cmd += ids + ");";
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";

            var TempPath = txt_TempPath.Text;
            DeleteRecords(ids, cmd, DB_Path, TempPath, norows.ToString(), hash);
        }

        public static void DeleteRecords(string ID, string cmd, string DBPath, string TempPath, string norows, string hash)
        {
            //Delete records
            DialogResult result1 = MessageBox.Show(norows + "of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            //try
            {

                //var connections = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBPath); //+ ";Persist Security Info=False"
                //var commands = connections.CreateCommand();
                ////dssx = DataGridView1;
                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBPath))
                //{
                //    //OleDbCommand command = new OleDbCommand(); ;
                //    //Update MainDB
                //    //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                //    //command.CommandText = "UPDATE Main SET ";
                //    commands.CommandText = cmd;

                //    commands = cnn.CreateCommand();
                //    commands.CommandType = CommandType.Text;
                //    cnn.Open();
                //    commands.ExecuteNonQuery();
                //}

                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBPath))
                //{
                //    DataSet dhs = new DataSet();
                //    var rr = cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN (");
                //    OleDbDataAdapter dhx = new OleDbDataAdapter(rr, cnn);
                //    dhx.Fill(dhs, "Main");
                //    dhx.Dispose();
                DataSet dhs = new DataSet(); dhs = DLCManager.SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("));
                var rcount = dhs.Tables[0].Rows.Count;
                for (var i = 0; i < rcount; i++)
                {
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    try
                    {
                        Directory.Delete(filePath, true);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can not Delete Song folder ! ");
                    }

                    //Move psarc file to Duplicates                        
                    string psarcPath = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    try
                    {
                        if (!File.Exists(psarcPath.Replace("0_old", "0_duplicate\\")))
                            File.Move(psarcPath, psarcPath.Replace("0_old", "0_duplicate\\"));
                        else File.Delete(psarcPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can not Move psarc ! ");
                        //File.Delete(psarcPath);
                    }
                }

                //DataSet dus = new DataSet();
                //OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);
                //dax.Fill(dus, "Main");
                //dax.Dispose();
                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd);

                //DataSet dis = new DataSet();

                //Delete Arangements
                // DataSet dks = new DataSet();
                // cmd = "DELETE FROM Arrangements WHERE CDLC_ID IN (" + ID + ")";//txt_ID.Text;
                // OleDbDataAdapter dix = new OleDbDataAdapter(cmd, cnn);
                // dix.Fill(dks, "Arrangements");
                // dix.Dispose();
                DLCManager.DeleteFromDB("Arrangements", "DELETE FROM Arrangements WHERE CDLC_ID IN (" + ID + ")");

                // //Delete Tones
                // DataSet dvs = new DataSet();
                // cmd = "DELETE FROM Tones WHERE CDLC_ID IN (" + ID + ")";//txt_ID.Text;
                // OleDbDataAdapter dvx = new OleDbDataAdapter(cmd, cnn);
                // dvx.Fill(dvs, "Tones");
                // dvx.Dispose();
                DLCManager.DeleteFromDB("Tones", "DELETE FROM Tones WHERE CDLC_ID IN (" + ID + ")");

                // //Delete Audit trail of import
                // DataSet dva = new DataSet();
                //// cmd = "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash + "\")";//txt_ID.Text;
                // cmd = "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash + "\")";
                // OleDbDataAdapter dvat = new OleDbDataAdapter(cmd, cnn);
                // dvat.Fill(dva, "Import_AuditTrail");
                // dvat.Dispose();
                DLCManager.DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash + "\")");

                // //Delete Audit trail of pack
                // DataSet dvt = new DataSet();
                // cmd = "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + ID + ")";//txt_ID.Text;
                // OleDbDataAdapter dvag = new OleDbDataAdapter(cmd, cnn);
                // dvag.Fill(dvt, "Import_AuditTrail");
                // dvag.Dispose();
                DLCManager.DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + ID + ")");

                MessageBox.Show(rcount + " Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Main DB connection in Cleanup ! " + DBPath + "-" + cmd);
            //}


        }

        public static void CreateTempFolderStructure(string Temp_Path_Import, string old_Path_Import, string broken_Path_Import, string dupli_Path_Import, string dlcpacks, string pathDLC, string repacked_path, string repacked_XBOXPath, string repacked_PCPath, string repacked_MACPath, string repacked_PSPath, string log_Path)
        {
            if (!Directory.Exists(Temp_Path_Import) || !Directory.Exists(pathDLC) || !Directory.Exists(old_Path_Import) || !Directory.Exists(broken_Path_Import) || !Directory.Exists(dupli_Path_Import) || !Directory.Exists(dlcpacks + "\\temp") || !Directory.Exists(dlcpacks + "\\manipulated") || !Directory.Exists(dlcpacks + "\\manifests") || !Directory.Exists(dlcpacks + "\\manipulated\\temp") || !Directory.Exists(repacked_path) || !Directory.Exists(repacked_XBOXPath) || !Directory.Exists(repacked_PCPath) || !Directory.Exists(repacked_MACPath) || !Directory.Exists(repacked_PSPath) || !Directory.Exists(log_Path))
            {
                MessageBox.Show(String.Format("One of the mandatory backend, folder is missing " + ", " + Temp_Path_Import + ", " + pathDLC + ", " + old_Path_Import + ", " + broken_Path_Import + ", " + dupli_Path_Import + ", " + dlcpacks + "(manipulated or manipulated-temp or manifests or temp), " + repacked_path + "(split by platform), " + log_Path, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error));
                try
                {
                    DirectoryInfo di;
                    DialogResult result1 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Cancel Import command\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        //if (!Directory.Exists(txt_TempPath.Text) && (txt_TempPath.Text != null)) di = Directory.CreateDirectory(txt_TempPath.Text);
                        if (!Directory.Exists(Temp_Path_Import) && (Temp_Path_Import != null)) di = Directory.CreateDirectory(Temp_Path_Import);
                        if (!Directory.Exists(pathDLC) && (pathDLC != null)) di = Directory.CreateDirectory(pathDLC);
                        if (!Directory.Exists(old_Path_Import) && (old_Path_Import != null)) di = Directory.CreateDirectory(old_Path_Import);
                        if (!Directory.Exists(broken_Path_Import) && (broken_Path_Import != null)) di = Directory.CreateDirectory(broken_Path_Import);
                        if (!Directory.Exists(dupli_Path_Import) && (dupli_Path_Import != null)) di = Directory.CreateDirectory(dupli_Path_Import);
                        if (!Directory.Exists(dlcpacks) && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks);
                        if (!Directory.Exists(dlcpacks + "\\manifests") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manifests");
                        if (!Directory.Exists(dlcpacks + "\\manipulated") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manipulated");
                        if (!Directory.Exists(dlcpacks + "\\manipulated\\temp") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manipulated\\temp");
                        if (!Directory.Exists(dlcpacks + "\\temp") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\temp");
                        if (!Directory.Exists(repacked_path) && (repacked_path != null)) di = Directory.CreateDirectory(repacked_path);
                        if (!Directory.Exists(repacked_XBOXPath) && (repacked_XBOXPath != null)) di = Directory.CreateDirectory(repacked_XBOXPath);
                        if (!Directory.Exists(repacked_PCPath) && (repacked_PCPath != null)) di = Directory.CreateDirectory(repacked_PCPath);
                        if (!Directory.Exists(repacked_MACPath) && (repacked_MACPath != null)) di = Directory.CreateDirectory(repacked_MACPath);
                        if (!Directory.Exists(repacked_PSPath) && (repacked_PSPath != null)) di = Directory.CreateDirectory(repacked_PSPath);
                        if (!Directory.Exists(log_Path) && (log_Path != null)) di = Directory.CreateDirectory(log_Path);
                        //rtxt_StatisticsOnReadDLCs.Text = Directory.Exists(dupli_Path_Import) +"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    else if (result1 == DialogResult.No) return;
                    else Application.Exit();
                }
                catch (Exception ex)
                {
                    //rtxt_StatisticsOnReadDLCs.Text = "issue with creating directories... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open create folders " + Temp_Path_Import + "-" + pathDLC + "-" + old_Path_Import + "-" + broken_Path_Import + "-" + dupli_Path_Import + "-" + dlcpacks + "-" + repacked_path);
                }
                //return;
            }

        }

        public void CleanFolder(string pathfld)
        {
            System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(pathfld);
            foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
            {
                try { file.Delete(); }
                catch (Exception ex) { Console.Write(ex); }
            }
        }

        static public void DeleteFromDB(string DB, string slct)
        {
            //var DB_Path = "";
            //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";// "";
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                try
                {
                    DataSet dss = new DataSet();
                    OleDbDataAdapter dan = new OleDbDataAdapter(slct, cnn);
                    dan.Fill(dss, "DB");
                    dan.Dispose();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                    //rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";// "";
            //MessageBox.Show("cleaninig");
            //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
            //save import table to reference the hashcodes in future imports
            using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                try
                {
                    DataSet dsm = new DataSet();
                    string insertcmd; //s.Substring(s.Length - pathDLC.Length)
                    if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ")";
                    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    dab.Fill(dsm, ftable);
                    dab.Dispose();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow(ee + "DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                    // rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //continue;
                }
            }

        }

        static public DataSet SelectFromDB(string ftable, string fcmds)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";
            DataSet dsm = new DataSet();
            using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cnb);
                    da.Fill(dsm, ftable);
                    da.Dispose();
                    return dsm;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow(ee + "DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return dsm;
                }
            }
        }

        static public DataSet UpdateDB(string ftable, string fcmds)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";
            DataSet dsm = new DataSet();

            OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(fcmds, myConn);
            OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);

            //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            try
            {
                //        OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cnb);
                myDataAdapter.Fill(dsm, ftable);
                //myDataAdapter.Update(dsm,ftable);//, ftable);
                myConn.Close();
                return dsm;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                ErrorWindow frm1 = new ErrorWindow(ee + "DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                frm1.ShowDialog();
                return dsm;
            }

        }

        // Read a Folder (clean temp folder)
        // Decompress the PC DLCs
        // Read details and populate a DB (clean Import DB before, and only populate Main if not there already)
        public void btn_PopulateDB_Click(object sender, EventArgs e)
        {
            var startT = System.DateTime.Now.ToString();
            var tst = "Starting... " + startT; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //Set_DEBUG(); //Default value when in dEV/Debug mode, if needed
            SaveSettings();

            pB_ReadDLCs.Value = 0;

            var Temp_Path_Import = txt_TempPath.Text;
            var old_Path_Import = txt_TempPath.Text + "\\0_old";
            var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
            var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
            var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
            var repacked_Path = txt_TempPath.Text + "\\0_repacked";
            var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
            var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
            var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
            var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
            var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
            string pathDLC = txt_RocksmithDLCPath.Text;

            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !chbx_Additional_Manipulations.GetItemChecked(38)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {
                CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);

                ////clear content of dlcpacks folder
                //System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Temp_Path_Import);
                //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                //{
                //    file.Delete();
                //}
                //foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                //{
                //    if (dir.Name != "0_dlcpacks" && dir.Name != "0_old" && dir.Name != "0_broken" && dir.Name != "0_duplicate" && dir.Name != "0_dlcpacks") dir.Delete(true);
                //}

                //clean app working folders 0 folder
                //{
                CleanFolder(txt_TempPath.Text);
                CleanFolder(txt_TempPath.Text + "\\0_old");
                CleanFolder(txt_TempPath.Text + "\\0_repacked");
                CleanFolder(txt_TempPath.Text + "\\0_repacked\\PC");
                CleanFolder(txt_TempPath.Text + "\\0_repacked\\PS3");
                CleanFolder(txt_TempPath.Text + "\\0_repacked\\MAC");
                CleanFolder(txt_TempPath.Text + "\\0_repacked\\XBOX360");
                CleanFolder(txt_TempPath.Text + "\\0_duplicate");
                System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text);
                foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                {
                    try
                    {
                        if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate")
                            dir.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can not open delete folders ! ");
                    }
                }
                //}
            }
            CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);

            var DB_Path = "";
            //DataSet dss = new DataSet();
            //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
            //DataSet dsr = new DataSet();
            var errr = true;

            //help code
            //using (var u = new UpdateForm())
            //{
            //    u.Init(onlineVersion);
            //    u.ShowDialog();
            //}
            DataSet dsR = new DataSet();
            if (chbx_Rebuild.Checked)

            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                string cmdR = @"SELECT * FROM Main as M;";
                if (rbtn_Population_Selected.Checked == true) cmdR += "WHERE Selected = \"Yes\"";
                //else if (rbtn_Population_All.Checked) ;
                else if (rbtn_Population_Groups.Checked) cmdR += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")"; //cbx_Groups.SelectedText 

                cmdR += " ORDER BY Artist";
                DataSet dooz = new DataSet(); dooz = DLCManager.SelectFromDB("Main", cmdR);
                //OleDbDataAdapter daa = new OleDbDataAdapter(cmdR, cnn);
                //daa.Fill(dsR, "Import");
                //daa.Dispose();
                int noOfRecR = dsR.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                tst = "Rebuilding" + noOfRecR + "/" + (noOfRecR) + " Songs already imported in MainDB"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                if (noOfRecR > 0)
                {

                    pB_ReadDLCs.Value = 0;
                    pB_ReadDLCs.Maximum = 2 * (noOfRecR - 1);
                    //int tt;
                    for (var j = 0; j <= 1; j++) ;

                    //    tt = Processing(0, 0, tst, FullPath, DB_Path, errr, broken_Path_Import, dsR, Temp_Path_Import, cnn, dupli_Path_Import, old_Path_Import, cmdR, unpackedDir, packid, false);
                }
            }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error @Import", false, false);
            //    frm1.ShowDialog();
            //    rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //return;
            //continue;
            //}

            //Clean ImportDB
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            DeleteFromDB("Import", "DELETE FROM Import;");
            tst = "Cleaning....Import table...." + DB_Path; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            if (chbx_CleanDB.Checked)
            {
                DeleteFromDB("Main", "DELETE FROM Main;");
                DeleteFromDB("Arrangements", "DELETE FROM Arrangements;");
                DeleteFromDB("Tones", "DELETE FROM Tones;");
                DeleteFromDB("LogPacking", "DELETE FROM LogPacking;");
                DeleteFromDB("LogPackingError", "DELETE FROM LogPackingError; ");
                DeleteFromDB("LogImporting", "DELETE FROM LogImporting;");
                DeleteFromDB("LogImportingError", "DELETE FROM LogImportingError;");
                DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail;");
                DeleteFromDB("Groups", "DELETE FROM Groups WHERE TYPE=\"Profile\";");
                DeleteFromDB("Import_AuditTrail", "DELETE FROM Import_AuditTrail;");
                DeleteFromDB("Standardization", "DELETE FROM Standardization;");
                DeleteFromDB("Cache", "DELETE FROM Cache;");
            }

            //}
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error @Import", false, false);
            //    frm1.ShowDialog();
            //                 rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    return;
            //    //continue;
            //}
            tst = DB_Path + " Cleaned"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            int i = 0;
            if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                //GetDirList and calcualte hash for the IMPORTED file
                //MessageBox.Show(pathDLC, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string[] filez;
                if (chbx_Additional_Manipulations.GetItemChecked(37)) //38. Import other formats but PC, as well(separately of course)
                    filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
                else
                    filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    //try to get the details
                    // Create the FileInfo object only when needed to ensure 
                    // the information is as current as possible.
                    System.IO.FileInfo fi = null;

                    try
                    {
                        fi = new System.IO.FileInfo(s);
                    }
                    catch (System.IO.FileNotFoundException ee)
                    {
                        Console.WriteLine(ee.Message);
                        rtxt_StatisticsOnReadDLCs.Text = "error at import" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                        frm1.ShowDialog();
                        return;
                        //continue;
                    }
                    //- To remove usage of ee and loading
                    Console.WriteLine("{0} : {1} : {2}", fi.Name, fi.Directory, loading);

                    //details end

                    //Generating the HASH code
                    var FileHash = "";
                    using (FileStream fs = File.OpenRead(s))
                    {
                        SHA1 sha = new SHA1Managed();
                        FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                        fs.Close();
                    }

                    //Populate ImportDB
                    tst = "File " + (i + 1) + " :" + s; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text; //+ "-------"  + fi.GetHashCode() + "-----------" + fi.Length + "-" + fi.CreationTime + "-" + fi.DirectoryName + "-" + fi.LastWriteTime + "-" + fi.Name;
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    //DataSet doz = new DataSet();
                    //DataSet dsz = new DataSet();
                    DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    //{
                    //    string selectcmd; //s.Substring(s.Length - pathDLC.Length)
                    //                      //Get last ID to make it N3ext Import Pack ID
                    //    selectcmd = "SELECT MAX(s.ID) FROM Main s;";
                    //    OleDbDataAdapter dbf = new OleDbDataAdapter(selectcmd, cnb);
                    //    dbf.Fill(doz, "Import");
                    //    dbf.Dispose();
                    DataSet doz = new DataSet(); doz = DLCManager.SelectFromDB("Main", "SELECT MAX(s.ID) FROM Main s;");

                    var ff = "-";
                    ff = System.DateTime.Now.ToString();
                    var tz = "0";
                    var noOfRecx = doz.Tables[0].Rows.Count;
                    if (noOfRecx > 0) tz = doz.Tables[0].Rows[0].ItemArray[0].ToString();
                    if (tz == "") tz = "1";

                    var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";
                    var insertvalues = "\"" + s + "\",\"" + fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\"" + FileHash + "\",\"" + fi.Length + "\",\"" + ff + "\",\"" + tz + "\"";
                    InsertIntoDBwValues("Import", insertcmdd, insertvalues);
                    //updatecmd += "\",\"" + doz.Tables[0].Rows.Count=="0" ? "0": doz.Tables[0].Rows[0].ItemArray[0].ToString() + "\");";
                    //OleDbDataAdapter dab = new OleDbDataAdapter(updatecmd, cnb);
                    //dab.Fill(dsz, "Import");
                    //dab.Dispose();
                    //dsz.Tables["Files"].AcceptChanges();
                    //MessageBox.Show(da)
                    //}

                    pB_ReadDLCs.Increment(1);
                    i++;
                }

                //Delete duplicates(same HASH) from ImportDB
                //DataSet dz = new DataSet();
                //DataSet drz = new DataSet();
                var del = 0;
                var no = 0;
                //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                //{
                //    string selectcmd; //s.Substring(s.Length - pathDLC.Length)

                //    selectcmd = "SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash = s.FileHash WHERE d.ID is not null GROUP BY s.FileHash;";
                //    OleDbDataAdapter dbf = new OleDbDataAdapter(selectcmd, cnb);
                //    dbf.Fill(dz, "Import");
                //    no = dz.Tables[0].Rows.Count;
                //    dbf.Dispose();
                DataSet dz = new DataSet(); dz = DLCManager.SelectFromDB("Import", "SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash = s.FileHash WHERE d.ID is not null GROUP BY s.FileHash;");
                no = dz.Tables[0].Rows.Count;
                // rtxt_StatisticsOnReadDLCs.Text = selectcmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                // selectcmd = @"/*SELECT * FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);"*/;
                //OleDbDataAdapter dff = new OleDbDataAdapter(selectcmd, cnb);
                //dff.Fill(drz, "Import");
                //del = drz.Tables[0].Rows.Count;
                //dff.Dispose();
                //Remove duplicate DLCs from this Import
                DataSet drz = new DataSet(); drz = DLCManager.SelectFromDB("Import", "SELECT * FROM Import WHERE ID not IN(SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);");
                del = drz.Tables[0].Rows.Count;

                //Add Import into AUDIT tRAIL

                //Delete Duplicates from Previous Imports
                //COMMENTED OUT AS IF IMPORT IS STOPPED ...SONGS WILL NOT BE IMPOIRTED AGAIN
                //Move song to duplciate if prev marked as such 
                //var selectcmd = @"SELECT FullPath,FileName FROM Import
                //                WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);";
                //OleDbDataAdapter daa = new OleDbDataAdapter(selectcmd, cnb);
                //DataSet dry = new DataSet();
                //daa.Fill(dry, "Import");
                //daa.Dispose();
                //Remove duplicate DLCs from this Import?
                DataSet dry = new DataSet(); dry = DLCManager.SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);");
                var noOfRecs = dry.Tables[0].Rows.Count;
                if (noOfRecs > 0)
                {
                    for (i = 0; i <= noOfRecs - 1; i++)
                        try
                        {
                            if (!File.Exists(txt_TempPath.Text + "\\0_duplicate\\" + dry.Tables[0].Rows[i].ItemArray[1].ToString())) File.Move(dry.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_duplicate\\" + dry.Tables[0].Rows[i].ItemArray[1].ToString());
                            else File.Move(dry.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_duplicate\\Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + dry.Tables[0].Rows[i].ItemArray[1].ToString());
                        }
                        catch (IOException ee)
                        {
                            Console.Write(ee.Message);
                            //File.Move
                        }
                    //selectcmd = @"DELETE FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);";
                    DeleteFromDB("Import", "DELETE FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);");
                }

                //commented out as MARKING AS DUPLICATE SONGS ALTOUGH IMPORT WAS NOT FINISHED..OR MARKED MANUALLY AS DUPLICATE
                //insert Imoprt entries that are not in audit trail
                //string updatecmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
                //var udatevA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(((i.ID)Is not Null))";
                //InsertIntoDBwValues("Import_AuditTrail", updatecmdA, udatevA);

                //insert Import entries
                //updatecmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
                //udatevA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i WHERE i.ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash)";
                //InsertIntoDBwValues("Import_AuditTrail", updatecmdA, udatevA);

                //selectcmd = @"SELECT FullPath,FileName FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);";
                //OleDbDataAdapter dva = new OleDbDataAdapter(selectcmd, cnb);
                //DataSet drh = new DataSet();
                //dva.Fill(drh, "Import");
                //dva.Dispose();
                //REmove
                DataSet drh = new DataSet(); drh = DLCManager.SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);");

                noOfRecs = drh.Tables[0].Rows.Count;

                if (noOfRecs > 0)
                {
                    for (i = 0; i <= noOfRecs - 1; i++)
                        File.Move(drh.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_duplicate\\" + drh.Tables[0].Rows[i].ItemArray[1].ToString());

                    //Delete Duplicates from Current Import
                    //selectcmd = @"DELETE FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);";
                    DeleteFromDB("Import", "DELETE * FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);");
                }
                //OleDbDataAdapter db = new OleDbDataAdapter(updatecmd, cnb);
                //db.Fill(dz, "Import");
                //db.Dispose();


                //dsz.Tables["Files"].AcceptChanges();
                //MessageBox.Show(da)
                //}
                tst = no + "/" + i + " Import files Inserted (excl. " + del + " duplicates)"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }
            else
            {
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(txt_RocksmithDLCPath.Text);
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {

                    //Populate ImportDB
                    tst = "Folder " + (i + 1) + " :" + "s"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text; //+ "-------"  + fi.GetHashCode() + "-----------" + fi.Length + "-" + fi.CreationTime + "-" + fi.DirectoryName + "-" + fi.LastWriteTime + "-" + fi.Name;
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                    //DataSet dsz = new DataSet();
                    ////  DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    //{
                    string insertcmds = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate)";
                    var insertvals = "\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + DateTime.Now + "\",\"" + "0" + "\",\"" + "0" + "\",\"";
                    insertvals += System.DateTime.Today + "\");";
                    InsertIntoDBwValues("Import", insertcmds, insertvals);
                    //OleDbDataAdapter dab = new OleDbDataAdapter(updatecmds, cnb);
                    //dab.Fill(dsz, "Import");
                    //dab.Dispose();
                    //dsz.Tables["Files"].AcceptChanges();
                    //MessageBox.Show(da)
                    // if (dir.Name != "dlcpacks" && dir.Name != "0_old" && dir.Name != "0_broken" && dir.Name != "0_duplicate") dir.Delete(true);
                    //}
                }
            }

            //save import table to reference the hashcodes in future imports
            //using(OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //        {

            //    DataSet dsm = new DataSet();
            //string updatecmd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
            //var udatev = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(((a.ID)Is Null))";
            //InsertIntoDBwValues("Import_AuditTrail", updatecmd, udatev);

            //    OleDbDataAdapter dab = new OleDbDataAdapter(updatecmd, cnb);
            //    dab.Fill(dsm, "Import");
            //    dab.Dispose();
            //}

            //START WITH mAINdb UPDATE
            //DataSet ds = new DataSet();
            //DataSet dns = new DataSet();
            var m = 0;
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {// 1. If hash already exists do not insert
            var cmd = @"SELECT DISTINCT FullPath, Path, FileName, FileHash, FileSize, ImportDate, m.Import_Date
                            FROM Import as i
                            LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash
                            WHERE m.ID is not NULL;";
            //OleDbDataAdapter dha = new OleDbDataAdapter(cmd, cnn);
            //dha.Fill(dns, "Import");
            //dha.Dispose();
            DataSet dns = new DataSet(); dns = DLCManager.SelectFromDB("Import", cmd);
            var tft = "\n Ignoring "; ;
            var noOfRec = dns.Tables[0].Rows.Count;
            //rtxt_StatisticsOnReadDLCs.Text = noOfRec + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (chbx_Additional_Manipulations.GetItemChecked(29) && noOfRec > 0) //31. When importing delete identical duplicates(same hash/filesize)
                tft = "";
            for (m = 0; m < noOfRec; m++)
            {
                var newf = dns.Tables[0].Rows[m].ItemArray[0].ToString().Replace(pathDLC, dupli_Path_Import);
                tst = newf + "\n" + dns.Tables[0].Rows[m].ItemArray[0].ToString(); rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;//dns.Tables[0].Rows[m].ItemArray[0].ToString()
                if (chbx_Additional_Manipulations.GetItemChecked(29)) //&& !File.Exists(newf) //As the new file might have a different name e.g. (1) ....etc.
                {
                    File.Copy(dns.Tables[0].Rows[m].ItemArray[0].ToString(), newf, true);
                    try
                    {
                        File.Delete(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Issues when moving to duplicate folder at import" + "-" + ex.Message + dns.Tables[0].Rows[m].ItemArray[0].ToString());
                    }
                    tft += "\n Deleting " + dns.Tables[0].Rows[m].ItemArray[0].ToString() + " as imported on " + dns.Tables[0].Rows[m].ItemArray[5].ToString();
                }

            }

            tst = tft + noOfRec + "/" + (noOfRec + m) + " already imported"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            cmd = @"SELECT i.FullPath, i.Path, i.FileName, i.FileHash, i.FileSize, i.ImportDate, i.Pack, i.FileCreationDate,i.ID
                                FROM Import as i
                                LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash
                                WHERE m.ID is NULL;";
            //OleDbDataAdapter daa = new OleDbDataAdapter(cmd, cnn);
            //daa.Fill(ds, "Import");
            //daa.Dispose();
            DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Main", cmd);
            noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
            tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; rtxt_StatisticsOnReadDLCs.Text = tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            //OleDbDataAdapter dac = new OleDbDataAdapter("INSERT INTO Main (Import_Path, Original_FileName, Current_FileName, File_Hash, File_Size, Import_Date) SELECT Path, FileName,FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
            //dac.Fill(ds, "Main");
            //OleDbDataAdapter dac = new OleDbDataAdapter(updatecmd, cnb);

            //OleDbDataAdapter dad = new OleDbDataAdapter("SELECT count(*) FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is not NULL;", cnn);
            //dad.Fill(ds, "Import");

            if (noOfRec > 0)
            {
                //Move duplicates to the end

                //for (var k = 0; k <= noOfRec - 1; k++)
                //{
                //    //for (j = 0; j <= noOfRec - 1; j++)
                //    //{
                //    if (1 == 0)
                //    {
                //        ds.Tables[0].Rows[i].ItemArray[0] = ds.Tables[0].Rows[i];
                //        noOfRec++;
                //    }
                //    //}
                //}
                //connection.Open();
                //DataSet dds = new DataSet();
                //OleDbDataAdapter daf = new OleDbDataAdapter("SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                //daf.Fill(dds, "Import");
                //oledbAdapter.Dispose();
                //connection.Close();
                //dss.Tables["Import"].AcceptChanges();
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Maximum = 2 * (noOfRec - 1);
                duplit = false;
                dupliNo = 0;
                dupliPrcs = 0;
                // string[,] dupliInfo = new string[0, 0];


                for (var j = 0; j < 10000; j++) { dupliSongs[j] = 0; } //int[] dupliSongs = new int[noOfRec];

                for (var j = 0; j <= 1; j++)
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        if (!(j == 1 && dupliSongs[i] == 0))
                        {
                            if (j == 1 && dupliSongs[i] == 1) dupliPrcs++;
                            duplit = false;
                            //MessageBox.Show(pB_ReadDLCs.Maximum+" test " +i); 
                            //DataTable AccTable = aSet.Tables["Accounts"];
                            tst = (j == 0 ? "" : "Duplicates: ") + (j == 0 ? (i + 1) : dupliPrcs) + "/" + (j == 0 ? noOfRec : dupliNo);

                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();


                            //to return: dupliSongs[i] = 1; duplit = true; dupliNo++

                            Random randomp = new Random();
                            int packid = randomp.Next(0, 100000);
                            rtxt_StatisticsOnReadDLCs.Text = tst + " " + FullPath + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //tst = tst; 
                            //var gh = tst.Replace(Path.GetDirectoryName(tst), "");
                            //var td = Path.GetDirectoryName(tst);
                            //Add text over progress bar
                            //pB_ReadDLCs.BeginInvoke(new Action(() => pB_ReadDLCs.Value = i));
                            pB_ReadDLCs.CreateGraphics().DrawString(tst + " " + FullPath.Replace(Path.GetDirectoryName(FullPath) + "\\", "")
                                , new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                            //UpdatePackingLog("LogImporting", DB_Path, packid, FullPath.Replace("'", ""), tst);
                            errr = false;
                            if (!chbx_Additional_Manipulations.GetItemChecked(37))
                                if (!FullPath.IsValidPSARC())
                                {
                                    MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                                    {
                                        File.Copy(FullPath.Replace(".psarc", ".invalid"), Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                                        File.Delete(FullPath.Replace(".psarc", ".invalid"));
                                    }
                                    errr = true;
                                    rtxt_StatisticsOnReadDLCs.Text = "FAILED2 @Import cause of extension issue but copied in the broken folder" + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);
                                    continue;
                                }

                            var unpackedDir = "";
                            //rtxt_StatisticsOnReadDLCs.Text = "1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            var packagePlatform = FullPath.GetPlatform();
                            if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                            {
                                var fgf = ConfigRepository.Instance()["general_wwisepath"] + "\\Authoring\\Win32\\Release\\bin\\Wwise.exe";
                                if (!File.Exists(fgf))//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2014.1.6 build 5318with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                                    frm1.ShowDialog();
                                    if (frm1.IgnoreSong) break;
                                    if (frm1.StopImport) { j = 10; i = 9999; break; }
                                }
                                try
                                {
                                    // UNPACK
                                    if (chbx_Additional_Manipulations.GetItemChecked(51))
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, null);//true,
                                    else
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, false, null);//true,
                                                                                                                   //packagePlatform = FullPath.GetPlatform();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unpacking ..." + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // MessageBox.Show("Error decompressing the file!(BACH OFFICIAL DLC CAUSE OF WEIRD CHAR IN FILENAME) " + "-" );
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    errr = false;
                                    //rtxt_StatisticsOnReadDLCs.Text = "predone" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    try
                                    {
                                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                                        {
                                            File.Copy(FullPath, Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                                            File.Delete(FullPath);
                                        }
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                                        errr = true; //bcapi???
                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }
                            else unpackedDir = FullPath;

                            //Commenting Reorganize as they might have fixed the incompatib char issue
                            // REORGANIZE
                            //System.Threading.Thread.Sleep(1000);
                            var platform = FullPath.GetPlatform();
                            //rtxt_StatisticsOnReadDLCs.Text = unpackedDir + " reorg...\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if (chbx_Additional_Manipulations.GetItemChecked(36) && !errr) //37. Keep the Uncompressed Songs superorganized    
                            {
                                try
                                {
                                    var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                                    if (structured)
                                    {
                                        unpackedDir = DLCPackageData.DoLikeProject(unpackedDir);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // System.Threading.Thread.Sleep(20000);
                                    //rtxt_StatisticsOnReadDLCs.Text = "1111 ...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories);
                                    foreach (var json in jsonFiles)
                                    {
                                        if (Path.GetFileNameWithoutExtension(json).ToUpperInvariant().Contains("VOCAL"))
                                            continue;
                                        if (platform.version == RocksmithToolkitLib.GameVersion.RS2014)
                                        {
                                            var jsons = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), SearchOption.AllDirectories);
                                            if (jsons.Length > 0)
                                            {
                                                string textfile = File.ReadAllText(json);
                                                textfile = textfile.Replace("?", "");
                                                textfile = textfile.Replace("/", "-");
                                                textfile = textfile.Replace("\\\"", "'");
                                                textfile = textfile.Replace("Sonata:", "Sonata");
                                                File.WriteAllText(json, textfile);
                                            }
                                        }
                                    }
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at reorg" + unpackedDir + "---" + Temp_Path_Import + "...\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    try
                                    {
                                        var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                                        if (structured)
                                        {
                                            unpackedDir = DLCPackageData.DoLikeProject(unpackedDir);
                                        }

                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                                        {
                                            File.Copy(FullPath, Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                                            File.Delete(FullPath);
                                        }
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                                        errr = true;
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2 @org but copied in the broken folder" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }

                            stopp = false;
                            string imported = Processing(i, j, tst, FullPath, DB_Path, errr, broken_Path_Import, ds, Temp_Path_Import, dupli_Path_Import, old_Path_Import, cmd, unpackedDir, packid, false);
                            if ((stopp) ) break;
                            // if imported add it to audit trail
                            // if (imported)
                            else
                            if (imported != "0") {
                                    string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
                                var insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA);

                                //Generating the HASH code
                                var FileHash = "";
                                var fpath = FullPath.Replace(txt_RocksmithDLCPath.Text, old_Path_Import);
                                FileStream fs;
                                using (fs = File.OpenRead(fpath))
                                {
                                    SHA1 sha = new SHA1Managed();
                                    FileHash = BitConverter.ToString(sha.ComputeHash(fs));                                    
                                }
                                System.IO.FileInfo fi = null; //calc file size
                                try { fi = new System.IO.FileInfo(fpath); }
                                catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

                                insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                var fnnon = Path.GetFileName(fpath);
                                var packn = FullPath.Substring(0, fpath.IndexOf(fnnon));
                                // insertA = "\"" + old_Path_Import + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ",\"" + info.Name + ",\"" + fnnon.GetPlatform().platform.ToString() + "\"";
                                insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, \""+imported.Split(';')[0] + "\" as DLC_ID, \"" + imported.Split(';')[1]+ "\" as DLC_Name, \"" + fpath.GetPlatform().platform.ToString()+ "\" as Platform FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                DLCManager.InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                                fs.Close();
                            }//
                        }
                    }
            }
            //    }

            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    rtxt_StatisticsOnReadDLCs.Text = "Error when importing..somewhere" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}
            var endI = System.DateTime.Now.ToString();
            rtxt_StatisticsOnReadDLCs.Text = "Ended import" + endI + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            //Cleanup
            if (chbx_Additional_Manipulations.GetItemChecked(24)) //25. Use translation tables for naming standardization
            {
                tst = "Applying Standardizations";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                Translation_And_Correction((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text));
                tst = "";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }

            if (chbx_Additional_Manipulations.GetItemChecked(42)) //43. Save import Log
            {
                // Write the string to a file.
                var fn = (log_Path == null ? MyAppWD : log_Path) + "\\" + GetTimestamps(DateTime.Now).Replace(":", "_") + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, true))//.ToString()
                {
                    file.WriteLine("Full Log");
                    file.WriteLine(rtxt_StatisticsOnReadDLCs.Text);
                    file.Close();
                }
                //string[] lines = {"Full Log"};//rtxt_StatisticsOnReadDLCs.Text};
                //System.IO.File.WriteAllLines(fn+"2", lines);

                rtxt_StatisticsOnReadDLCs.Text = "Log saved" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }

            //Show Intro database window
            MainDB frm = new MainDB((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), txt_TempPath.Text, chbx_Additional_Manipulations.GetItemChecked(33)
                , txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.Show();

            //dataGrid.frmMainForm.ActiveForm.Show();
            //MessageBox.Show("f");
            var endT = System.DateTime.Now.ToString();
            rtxt_StatisticsOnReadDLCs.Text = "Ended " + endT + " (" + startT + ")" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
        }
        string Check4MultiT(string origFN, string noMFN, string text)
        {
            var FN = origFN.ToLower();
            var ST = noMFN.ToLower();
            text = text.ToLower();
            // var z=noMFN.ToLower().IndexOf(text);
            var retrn = "";
            if (origFN.ToLower().IndexOf(text) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0
                || noMFN.ToLower().IndexOf(text) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0)
            {
                noMFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(noMFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                origFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(origFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                retrn = noMFN + ";" + ((FN != origFN || noMFN != ST) ? "Yes" : "No");
                return retrn;
            }
            retrn = noMFN+";"+"No";
            return retrn;
        }

        string Processing(int i, int j, string tst, string FullPath, string DB_Path, bool errr, string broken_Path_Import, DataSet ds,
               string Temp_Path_Import, string dupli_Path_Import, string old_Path_Import, string cmd, string unpackedDir, int packid, bool Rebuild)

        {
            var platform = FullPath.GetPlatform();
            var platformTXT = FullPath.GetPlatform().platform.ToString();
            var Available_Duplicate = "No";
            var Available_Old = "No";
            var CDLC_ID = "";
            // rtxt_StatisticsOnReadDLCs.Text = "2" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var DD = "No";
            var Bass_Has_DD = "No";
            var sect1on = "Yes";
            DLCPackageData info = null;

            if (!errr)
            {
                //FIX for adding preview_preview_preview
                if (unpackedDir == "")
                {
                    unpackedDir = "C:\\GitHub\\tmp\\0\\dlcpacks\\songs_Pc";
                    rtxt_StatisticsOnReadDLCs.Text = "Issues at decompressing WEMs or FAILED2 empty path" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                // LOAD DATA
                //rtxt_StatisticsOnReadDLCs.Text = "2.5" + "\n" + rtxt_StatisticsOnReadDLCs.Text;   
                
                try
                {
                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtxt_StatisticsOnReadDLCs.Text = ee.Message + " Broken Song Not Imported" + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    Console.WriteLine(ee.Message);
                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                    {
                        File.Copy(FullPath, Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                        File.Delete(FullPath);
                    }
                    UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                    rtxt_StatisticsOnReadDLCs.Text = "FAILED2 @Load but copied in the broken folder" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    Console.WriteLine(ee.Message);
                    //continue;
                    return "0";
                }
                //rtxt_StatisticsOnReadDLCs.Text = "3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                string ff = info.SongInfo.Artist, gg = info.SongInfo.ArtistSort, hhh = info.SongInfo.SongDisplayName, jj = info.SongInfo.SongDisplayNameSort, kk = info.SongInfo.Album;
                if (chbx_Additional_Manipulations.GetItemChecked(35)) //36.
                {
                    //Remove weird/illegal characters
                    info.SongInfo.Artist = info.SongInfo.Artist.Trim();
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\\", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("/", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("?", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace(":", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "'");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Trim();
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\\", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("/", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("?", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(":", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "'");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Trim();
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\\", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("/", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("?", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(":", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "'");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Trim();
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\\", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("/", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("?", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(":", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "'");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\\", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "");
                    info.SongInfo.Album = info.SongInfo.Album.Trim();
                    info.SongInfo.Album = info.SongInfo.Album.Replace("/", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("?", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace(":", "");

                    //info.AlbumArtPath = info.SongInfo.Album.Replace("/", "");
                    if (ff != info.SongInfo.Artist) rtxt_StatisticsOnReadDLCs.Text = "removing potentially illegal characters \\,\",/,?,: from Artist..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (gg != info.SongInfo.ArtistSort) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegally characters \\,\",/,?,: from ArtistSort..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (hhh != info.SongInfo.SongDisplayName) rtxt_StatisticsOnReadDLCs.Text = "removing potentially illegal characters \\,\",/,?,: from Title..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (jj != info.SongInfo.SongDisplayNameSort) rtxt_StatisticsOnReadDLCs.Text = "removing potentially illegal characters \\,\",/,?,: from TitleSort..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (kk != info.SongInfo.Album) rtxt_StatisticsOnReadDLCs.Text = "removing potentially illegal characters \\,\",/,?,: from Album..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                {
                    info.SongInfo.ArtistSort = info.SongInfo.Artist;
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "5 :"+ info.SongInfo.SongDisplayNameSort +  "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (chbx_Additional_Manipulations.GetItemChecked(22)) //23. Import with the The/Die only at the end of Title Sort     
                {
                    if (chbx_Additional_Manipulations.GetItemChecked(20))
                    {
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "The " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "Die " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "the " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "die " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "THE " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "DIE " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);

                    }
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "The " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "Die " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "the " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "die " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "THE " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "DIE " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                }
                //rtxt_StatisticsOnReadDLCs.Text = "6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //if (i == 0) MessageBox.Show("3");
                rtxt_StatisticsOnReadDLCs.Text = "\n Song " + (i + 1) + ": " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                pB_ReadDLCs.Increment(1);

                //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                var xmlFiles = Directory.GetFiles(unpackedDir + "\\songs", "*.xml", SearchOption.AllDirectories);
                //platform = FullPath.GetPlatform();
                var g = 0;
                List<string> clist = new List<string>();
                List<string> dlist = new List<string>();
                foreach (var xml in xmlFiles)
                {
                    if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("vocal"))
                    {
                        clist.Add("");
                        dlist.Add("No"); continue;
                    }


                    if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("showlight"))
                    {
                        clist.Add("");
                        dlist.Add("No"); continue;
                    }

                    // rtxt_StatisticsOnReadDLCs.Text = "ffff\n" + rtxt_StatisticsOnReadDLCs.Text;
                    platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                    //rtxt_StatisticsOnReadDLCs.Text = "ddf\n" + rtxt_StatisticsOnReadDLCs.Text;
                    Song2014 xmlContent = null;
                    try
                    {
                        xmlContent = Song2014.LoadFromFile(xml);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rtxt_StatisticsOnReadDLCs.Text = ee.Message + " Broken Song Not Imported" + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        Console.WriteLine(ee.Message);
                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                        {
                            File.Copy(FullPath, Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                            File.Delete(FullPath);
                        }
                        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2 @XML parse but copied in the broken folder" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        Console.WriteLine(ee.Message);
                        continue;
                    }

                    var manifestFunctions = new ManifestFunctions(platform.version);
                    //Get sections and lastconvdate
                    var json = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(xml)), SearchOption.AllDirectories);
                    if (json.Length > 0)//&& g==1
                    {
                        foreach (var fl in json)
                        {
                            if (Path.GetFileNameWithoutExtension(fl).ToLower().Contains("bass") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("rhythm") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("combo"))
                            {
                                //Attributes2014 attr = Manifest2014<Attributes2014>.LoadFromFile(fl).Entries.ToArray()[0].Value.ToArray()[0].Value;
                                var attr = Manifest2014<Attributes2014>.LoadFromFile(fl).Entries.First().Value.First().Value;
                                manifestFunctions.GenerateSectionData(attr, xmlContent);
                                if (attr.Sections.Count < 2) sect1on = "No";
                                clist.Add(attr.LastConversionDateTime);
                                dlist.Add((attr.Sections.Count > 0 ? "Yes" : "No"));
                            }
                            else
                            {
                                rtxt_StatisticsOnReadDLCs.Text = "no section/lastconvdate" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                clist.Add("");
                                dlist.Add("No");
                            }
                        }
                    }

                    g++;

                    if (manifestFunctions.GetMaxDifficulty(xmlContent) > 1) DD = "Yes";

                    //Bass_Has_DD
                    var manifestFunctions1 = new ManifestFunctions(platform.version);
                    xmlContent = null;
                    try
                    {
                        xmlContent = Song2014.LoadFromFile(xml);
                        if (xmlContent.Arrangement.ToLower() == "bass")
                        {
                            platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                            if (manifestFunctions1.GetMaxDifficulty(xmlContent) > 1)
                                Bass_Has_DD = "Yes";
                        }
                    }
                    catch (Exception ee)
                    {
                        Console.Write(ee.Message);
                    }
                }

                // READ ARRANGEMENTS
                //rtxt_StatisticsOnReadDLCs.Text = "3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //var updateAcmd = "";
                var Lead = "No";
                var Bass = "No";
                var Vocalss = "No";
                var Guitar = "No";
                var Rhythm = "No";
                var Combo = "No";
                var PluckedType = "";
                var Tunings = "";
                var bonus = "No";
                List<string> alist = new List<string>();
                List<string> blist = new List<string>();
                var SongLenght = "";
                //var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories); //Get directory of JSON files in case song dir is not ORGANIZED :)
                foreach (var arg in info.Arrangements)
                {
                    if (arg.BonusArr) bonus = "Yes";

                    if (arg.ArrangementType == ArrangementType.Guitar)
                    {
                        Guitar = "Yes";
                        if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                        else Tunings = arg.Tuning;

                        if (arg.Name == ArrangementName.Lead) Lead = "Yes";
                        else if (arg.Name == ArrangementName.Rhythm) Rhythm = "Yes";
                        else if (arg.Name == ArrangementName.Combo) Combo = "Yes";
                        Song2014 xmlContent = null;
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(arg.SongXml.File);
                            SongLenght = xmlContent.SongLength.ToString();
                        }
                        catch (Exception ee)
                        {
                            Console.Write(ee.Message);
                        }
                    }

                    else if (arg.ArrangementType == ArrangementType.Vocal) Vocalss = "Yes";
                    else if (arg.ArrangementType == ArrangementType.Bass)
                    {
                        Bass = "Yes";

                        PluckedType = arg.PluckedType.ToString();
                        if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                        else Tunings = arg.Tuning;
                        Song2014 xmlContent = null;
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(arg.SongXml.File);
                            SongLenght = xmlContent.SongLength.ToString();
                        }
                        catch (Exception ee)
                        {
                            Console.Write(ee.Message);
                        }
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = "gen ar hashes: " +arg.SongXml.File+"/"+arg.SongXml.File + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var s1 = arg.SongXml.File;
                    using (FileStream fs = File.OpenRead(s1))
                    {
                        SHA1 sha = new SHA1Managed();
                        alist.Add((BitConverter.ToString(sha.ComputeHash(fs))).ToString());
                        fs.Close();
                    }

                    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                        s1 = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                    else
                        s1 = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));

                    if (File.Exists(s1))
                        using (FileStream fss = File.OpenRead(s1))
                        {
                            SHA1 sha = new SHA1Managed();
                            blist.Add((BitConverter.ToString(sha.ComputeHash(fss))).ToString());
                            fss.Close();
                        }
                    else blist.Add("0");
                    //rtxt_StatisticsOnReadDLCs.Text = "done ar hashes: " +"\n" + rtxt_StatisticsOnReadDLCs.Text;

                }
                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //Check Tones
                var Tones_Custom = "No";
                foreach (var tn in info.TonesRS2014)//, Type
                {
                    if (tn.IsCustom)
                        Tones_Custom = "Yes";
                }


                var alt = "";
                var trackno = -1;

                Is_MultiTrack = "";
                MultiTrack_Version = "";

                //Get Author and Toolkit version
                var versionFile = Directory.GetFiles(unpackedDir, "toolkit.version", SearchOption.AllDirectories);
                tkversion = "";
                author = "";
                var Has_author = "";
                if (versionFile.Length > 0)
                    tkversion = ReadPackageToolkitVersion(versionFile[0]);

                if (versionFile.Length > 0)
                {
                    author = ReadPackageAuthor(versionFile[0]);
                    if (tkversion.Length == 0)
                        tkversion = ReadPackageOLDToolkitVersion(versionFile[0]);
                }
                if (author == "" && tkversion != "") { author = ""; Has_author = "No"; }
                else Has_author = "Yes";
                if (chbx_Additional_Manipulations.GetItemChecked(57))
                {
                    if (author.IndexOf("Custom Song Creator") > 0) Has_author = "No";
                    author = author.Replace("Custom Song Creator", "");

                }

                //rtxt_StatisticsOnReadDLCs.Text = vpos + "===" + txt.Length+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (versionFile.Length <= 0) Is_Original = "Yes";
                else Is_Original = "No";
                //rtxt_StatisticsOnReadDLCs.Text = Is_Original + "===" + versionFile.Length+ "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //Get Version from FileName
                var import_path = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                var original_FileName = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                string txt = original_FileName;

                int vpos = (txt.IndexOf("_v")) + 2;
                //if (vpos == 0) vpos = (txt.IndexOf("-v")) + 1;
                //if (vpos == 0) vpos = (txt.IndexOf(" v")) + 1;
                //if (vpos == 0) vpos = (txt.IndexOf(".v")) + 1;
                string major = "";
                string minor = "";

                if (info.PackageVersion != "" && (info.PackageVersion != null)) if (info.PackageVersion.Length > 2) if (info.PackageVersion.Substring(info.PackageVersion.Length - 2, 2) == ".0") info.PackageVersion = info.PackageVersion.Substring(0, info.PackageVersion.Length - 2);

                if (txt.Substring(vpos, 1).ToInt32() >= 0 && vpos > 5)
                {
                    major = txt.Substring(vpos, 1);

                    var ends = txt.Substring(vpos, txt.Length - vpos).Replace("-", "").Replace("_", "").Replace(".", "").Replace(" ", "");
                    for (var hh = 1; hh < ends.Length; hh++)
                    {
                        if (ends.Substring(hh, 1).ToInt32() >= 0)
                        {
                            minor += ends.Substring(hh, 1);
                        }
                        else hh = ends.Length;
                    }
                    string ver = major + (minor != "" ? "." : "") + minor;

                    if (ver.Length > 2) if (ver.Substring(ver.Length - 2, 2) == ".0") ver = ver.Substring(0, ver.Length - 2);
                    if (Convert.ToSingle(info.PackageVersion) < Convert.ToSingle(ver)) info.PackageVersion = ver;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "=___" + vv + "---" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //foreach (var infofile in versionFile)
                //{
                //    rtxt_StatisticsOnReadDLCs.Text += "\n last verrsfi " + infofile;
                //    tkversion += infofile;
                //}

                //example of properly working with sql
                // Command to Insert Records
                //OleDbCommand cmdInsert = new OleDbCommand();
                //cmdInsert.CommandText = "INSERT INTO AutoIncrementTest (Description) VALUES (?)";
                //cmdInsert.Connection = cnJetDB;
                //cmdInsert.Parameters.Add(new OleDbParameter("Description", OleDbType.VarChar, 40, "Description"));
                //oleDa.InsertCommand = cmdInsert;


                //rtxt_StatisticsOnReadDLCs.Text = info.AlbumArtPath+"---" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //Set appID
                if (chbx_Additional_Manipulations.GetItemChecked(43)) AppIdD = ConfigRepository.Instance()["general_defaultappid_RS2014"];
                else AppIdD = info.AppId;

                //Set MultiTrack absed on FileName                                
                //No Bass
                //No Lead
                //No Rhythm
                //No Vocal
                //(No Guitars)
                //Only Bass
                //Only Lead
                //Only Rhythm
                //Only Vocal
                //(Only BackTrack)
                string origFN = ds.Tables[0].Rows[i].ItemArray[2].ToString().ToLower();
                string noMFN = info.SongInfo.SongDisplayName;
                string Titl,gom= noMFN;

                var multxt="No Guitar";Titl = Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(No Guitars)"; }
                multxt = "No Band";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(No Guitars)"; }
                multxt = "No Band Audio";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(No Guitars)"; }
                multxt = "Lead";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "Only Lead"; }
                multxt = "No Lead";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "Lead Only";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "Only Lead"; }
                multxt = "Only Lead";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "No Bass";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "No Bass Audio";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "No Bass"; }
                multxt = "Bass Only";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "Only Bass"; }
                multxt = "Only Bass";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "No Rhythm";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "Only Rhythm";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "Rhythm Only";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "Only Rhythm"; }
                multxt = "(Only BackTrack)";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "backing";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backingtrack";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; } 
                multxt = "backtrack";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }                
                multxt = "backing only";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backing audio only";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backing track";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "Only Band";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "No Vocal";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }
                multxt = "Only Vocal";Titl =  Check4MultiT(origFN, noMFN, multxt);  if ( "Yes"== Titl.Split(';')[1]) { Is_MultiTrack = "Yes";gom=Titl.Split(';')[0];MultiTrack_Version = multxt; }

                //Remove MultiTrackLive Info from Title
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes") info.SongInfo.SongDisplayName = gom.TrimEnd().TrimStart().Replace(" ()", "");
                //  info.SongInfo.SongDisplayName = gom;               
                if (Is_MultiTrack == "Yes") rtxt_StatisticsOnReadDLCs.Text = "Multitrack=-=" + MultiTrack_Version + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                var IsLive = "";
                var LiveDetails = "";
                multxt = "Live"; Titl = Check4MultiT(origFN, info.SongInfo.SongDisplayName, multxt);
                if ("Yes" == Titl.Split(';')[1])
                { IsLive = "Yes";gom=Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails = gom.IndexOf(multxt)<= gom.Length-4?"": gom.Replace(gom.Substring(0,info.SongInfo.SongDisplayName.IndexOf(multxt)+4),""); }
               // info.SongInfo.SongDisplayName = gom;

                //Remove MultiTrackLive Info from Title
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes") info.SongInfo.SongDisplayName = gom.TrimEnd().TrimStart().Replace(" ()", ""); //(Regex.Replace(noMFN, "( audio)", "", RegexOptions.IgnoreCase)).TrimEnd().TrimStart().Replace(" ()", "");
                else info.SongInfo.SongDisplayName = Regex.Replace(Regex.Replace(Regex.Replace(info.SongInfo.SongDisplayName, "(No.)", "[No.]"), "(Backing.)", "[Backing.]"), "(Live.)", "[Live.]");

                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                {
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                }

                //Get TrackNo
                trackno = 0;
                if (chbx_Additional_Manipulations.GetItemChecked(41))
                    trackno = (MainDB.GetTrackNo(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName));
                ExistingTrackNo = "";

                //Generating the HASH code
                art_hash = "";
                string audio_hash = "";
                string audioPreview_hash = "";
                AlbumArtPath = info.AlbumArtPath;
                string ss = "";

                try
                {
                    if (AlbumArtPath != "")
                        using (FileStream fs = File.OpenRead(AlbumArtPath))
                        {
                            SHA1 sha = new SHA1Managed();
                            art_hash = BitConverter.ToString(sha.ComputeHash(fs));//MessageBox.Show(FileHash+"-"+ss);
                                                                                  //convert to png
                            ExternalApps.Dds2Png(AlbumArtPath);
                            fs.Close();
                        }
                    //rtxt_StatisticsOnReadDLCs.Text = "hashes: " + ss + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    ss = info.OggPath;
                    using (FileStream fs = File.OpenRead(ss))
                    {
                        SHA1 sha = new SHA1Managed();
                        audio_hash = BitConverter.ToString(sha.ComputeHash(fs));
                        fs.Close();
                    }

                    ss = info.OggPreviewPath;
                    //rtxt_StatisticsOnReadDLCs.Text = "rhashes: " + rtxt_StatisticsOnReadDLCs.Text;
                    if (ss != null)
                        using (FileStream fs = File.OpenRead(ss))
                        {
                            SHA1 sha = new SHA1Managed();
                            audioPreview_hash = BitConverter.ToString(sha.ComputeHash(fs));
                            fs.Close();
                        }
                    //rtxt_StatisticsOnReadDLCs.Text = "6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // MessageBox.Show("Error decompressing the file!(BACH OFFICIAL DLC CAUSE OF WEIRD CHAR IN FILENAME) " + "-" );
                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + ss + "problem at hash" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                    errr = false;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "rhashes: " + art_hash + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //Check if CDLC have already been imported (hash key)
                // 1. If hash already exists do not insert
                // 2. If hash does not exists then:
                // 2.1.1 If Artist+Album+Title or dlcname exists check author. If same check version
                // 2.1.1.1 If (Artist+Album+Title or dlcname)+author the same check version If bigger add
                // 2.1.1.2 If (Artist+Album+Title or dlcname)+author the same check version If smaller ignore
                // 2.1.1.3 If (Artist+Album+Title or dlcname)+author the same check version If same ?
                // 3.1.2 If (Artist+Album+Title or dlcname) exists check author. If the not the same add as alternate
                // 4.1.3 If (Artist+Album+Title or dlcname) exists check author. If empty/generic(Custom Song Creator) show statistics and add as give choice to alternate or ignore
                //SELECT if the same artist, album, songname
                var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND "; //AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\")
                sel += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                sel += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                //sel += "OR (\"%LCASE(Song_Title)%\" like LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\") ORDER BY Is_Original ASC";
                //Read from DB
                int norows = 0;
                norows = SQLAccess(sel);
                //rtxt_StatisticsOnReadDLCs.Text = "assesing " + norows  + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //MessageBox.Show("Chose: 1.Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);                           

                var b = 0;
                var artist = "Insert";
                string jk = ""; string k = "";
                var IDD = "";
                var folder_name = "";
                var filename = "";
                bool newold = chbx_Additional_Manipulations.GetItemChecked(32);
                Random random = new Random();
                //info.Name = Name;
                //info.SongInfo.SongDisplayName =  info.SongInfo.SongDisplayName;
                if (norows > 0)
                    foreach (var file in files)
                    {
                        SongDisplayName = "";
                        Namee = "";
                        //rtxt_StatisticsOnReadDLCs.Text = "\n ------"+ file.Folder_Name.ToString() + "------ " + b + " ------"+ file.Current_FileName.ToString() + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (b >= norows) break;
                        folder_name = file.Folder_Name;
                        filename = file.Current_FileName;
                        //rtxt_StatisticsOnReadDLCs.Text =file.Author.ToLower() +"-"+author.ToLower() + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                        //When importing a original when there is already a similar CDLC
                        if (author == "" && tkversion == "" && chbx_Additional_Manipulations.GetItemChecked(14))
                        {
                            artist = "Insert";

                            //Generate MAX Alternate NO
                            //var sel1 = sel.Replace("SELECT *", "SELECT max(Alternate_Version_No)");
                            //sel1 = sel1.Replace(" ORDER BY Is_Original ASC", "");
                            ////rtxt_StatisticsOnReadDLCs.Text = sel1 + "-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //DataSet ddzv = new DataSet();
                            //OleDbDataAdapter dat = new OleDbDataAdapter(sel1, cnn);
                            //dat.Fill(ddzv, "Main");
                            //dat.Dispose();
                            DataSet ddzv = new DataSet(); ddzv = DLCManager.SelectFromDB("Main", sel.Replace("SELECT *", "SELECT max(Alternate_Version_No)"));

                            //UPDATE the 1(s) not an alternate already
                            int max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1;
                            DataSet dxr = new DataSet(); dxr = DLCManager.UpdateDB("Main", "Update Main Set Song_Title = Song_Title +\" a." + max + "\", Song_Title_Sort = Song_Title_Sort+\" a." + max + "\", Is_Alternate = \"Yes\", Alternate_Version_No=" + max + " where ID in (" + sel.Replace("*", "ID") + ") and Is_Alternate=\"No\"" + ";");
                            //var sel2 = "Update Main Set Song_Title = Song_Title+\" a." + max + "\", Song_Title_Sort = Song_Title_Sort+\" a." + max + "\", Is_Alternate = \"Yes\", Alternate_Version_No=" + max + " where ID in (" + sel.Replace("*", "ID") + ") and Is_Alternate=\"No\"";
                            ////rtxt_StatisticsOnReadDLCs.Text = max.ToString()+"-"+sel2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //DataSet dxr = new DataSet();
                            //OleDbDataAdapter dax = new OleDbDataAdapter(sel2, cnn);
                            //dax.Fill(dxr, "Main");
                            //dax.Dispose();

                            //Add also a random DLCName if any of the Alternates has the same DLC Name ssame as the original
                            DataSet dxf = new DataSet(); dxf = DLCManager.UpdateDB("Main", "UPDATE Main SET DLC_Name = DLC_Name+\"" + random.Next(0, 100000) + "\" WHERE ID in (" + sel.Replace("*", "ID") + ") and LCASE(DLC_Name) = \"" + info.Name.ToLower() + "\"" + ";");
                            //var sel3 = "UPDATE Main SET DLC_Name = DLC_Name+\"" + random.Next(0, 100000) + "\" WHERE ID in (" + sel.Replace("*", "ID") + ") and LCASE(DLC_Name) = \"" + info.Name.ToLower() + "\"";
                            ////rtxt_StatisticsOnReadDLCs.Text =  random.Next(0, 100000) + "-"+sel3 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //DataSet dxf = new DataSet();
                            //OleDbDataAdapter dbx = new OleDbDataAdapter(sel3, cnn);
                            //dbx.Fill(dxf, "Main");
                            //dbx.Dispose();
                            break;
                        }

                        //calculate the alternative no (in case is needed)
                        var altver = "";
                        //try
                        //{
                        //using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        //{
                        //var sel = "";
                        sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + CleanTitle(info.SongInfo.Artist) + "\") AND ";// LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                        sel += "(LCASE(Song_Title)=LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayName) + "\") OR ";
                        sel += "LCASE(Song_Title) like \"%" + CleanTitle(info.SongInfo.SongDisplayName.ToLower()) + "%\" OR ";
                        sel += "LCASE(Song_Title_Sort) =LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayNameSort) + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                        //Get last inserted ID
                        //rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //DataSet dds = new DataSet();
                        //OleDbDataAdapter dda = new OleDbDataAdapter(sel, cnn);
                        //dda.Fill(dds, "Main");
                        //dda.Dispose();
                        DataSet dds = new DataSet(); dds = DLCManager.SelectFromDB("Main", sel);

                        var altvert = dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 0 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                        if (Is_Original == "No") altver = (altvert + 1).ToString(); //file.Alternate_Version_No//Add Alternative_Version_No
                                                                                    //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                        //}
                        //}
                        //catch (System.IO.FileNotFoundException ee)
                        //{
                        //    
                        //    
                        //    
                        //    Console.WriteLine(ee.Message);
                        //    rtxt_StatisticsOnReadDLCs.Text = "error at altver calc \n" + rtxt_StatisticsOnReadDLCs.Text;
                        //    //continue;
                        //}
                        //if (Is_Original == "No") Alternate_No = (GetAlternateNo().ToInt32() + 1).ToString();
                        var fsz = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                        //Namee = "";
                        //SongDisplayName = "";
                        Title_Sort = "";
                        ArtistSort = "";
                        Artist = "";
                        Is_Alternate = "";
                        Alternate_No = "";
                        Album = "";
                        PackageVersion = "";

                        if (Rebuild) artist = "Insert";//chbx_Additional_Manipulations.SetItemCheckState(13, CheckState.Checked); //At Rebuild ignore duplicates
                        else
                        if ((author.ToLower() == file.Author.ToLower() && author != "" && file.Author != "" && file.Author != "Custom Song Creator" && author != "Custom Song Creator") || (file.DLC_Name == info.Name))
                        {
                            if (file.DLC_Name.ToLower() == info.Name.ToLower())
                                if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                {
                                    dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                }
                                else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive);
                            else
                            {
                                if (file.Version.ToInt32() > info.PackageVersion.ToInt32()) artist = "Update";
                                if (file.Version.ToInt32() < info.PackageVersion.ToInt32())
                                    if (file.Is_Alternate != "Yes") { artist = "Ignore"; rtxt_StatisticsOnReadDLCs.Text = "IGNORED" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                    else if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                    {
                                        dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                    }
                                    else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive);
                                if (file.Version.ToInt32() == info.PackageVersion.ToInt32())
                                    if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                    {
                                        dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                    }
                                    else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive);
                                else { artist = "Ignore"; rtxt_StatisticsOnReadDLCs.Text = "IGNORED" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                // assess=alternate, update or ignore//as maybe a new package(ing) is desired to be inserted in the DB
                            }
                        }
                        else if (author.ToLower() != file.Author.ToLower() && (author != "" && author != "Custom Song Creator" && file.Author != "Custom Song Creator" && file.Author != "")) artist = "Alternate";
                        else if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                        {
                            dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                        }
                        else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive);
                        //rtxt_StatisticsOnReadDLCs.Text = "7 "+b + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //Exit condition

                        if (artist == "Stop")
                        {
                            j = 10000;
                            i = 10000;
                            stopp = true;
                            break;
                        }

                        if (artist == "Alternate")
                        {
                            if (alt =="") alt = "1";
                            //txt = (info.PackageVersion != null ? "No" : "Yes");
                            //rtxt_StatisticsOnReadDLCs.Text = "\n" + "-" + "\n-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if (Namee != "") info.Name = Namee;
                            if (SongDisplayName != "") info.SongInfo.SongDisplayName = SongDisplayName;
                            if (Title_Sort != "") info.SongInfo.SongDisplayNameSort = Title_Sort;
                            if (ArtistSort != "") info.SongInfo.ArtistSort = ArtistSort;
                            if (Artist != "") info.SongInfo.Artist = Artist;
                            //?
                            if (Is_Alternate != "" && Is_Original == "No") alt = Alternate_No;
                            if (Alternate_No != "" && Is_Original == "No") alt = Alternate_No;
                            //end?
                            if (Album != "") info.SongInfo.Album = Album;
                            if (PackageVersion != "") info.PackageVersion = PackageVersion;
                            //if (AlbumArtPath != "") info.Name = Name;
                            //if (art_hash != "") info.Name = Name;
                            //if (txt == "No") info.PackageVersion = null;
                            //rtxt_StatisticsOnReadDLCs.Text = "\n"+"-"+ (PackageVersion != "" && info.PackageVersion != null ? "No" : "Yes") + "\n-"+ info.PackageVersion + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            artist = "Insert";

                            //Get the higgest Alternate Number
                            //sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + info.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                            //sel += "(LCASE(Song_Title)=LCASE(\"" + info.SongInfo.SongDisplayName + "\") OR ";
                            //sel += "LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
                            //sel += "LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                            ////Get last inserted ID
                            ////rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //DataSet dsr = new DataSet();
                            //OleDbDataAdapter dad = new OleDbDataAdapter(sel, cnn);
                            //dad.Fill(dsr, "Main");
                            //dad.Dispose();
                            //string altver = "";
                            //foreach (DataRow dataRow in dsr.Tables[0].Rows)
                            //{
                            //    altver = dataRow.ItemArray[0].ToString();

                            //if (Is_Original == "No" && Alternate_No == "") alt = "99";// Alternate_No; //Add Alternative_Version_No
                            //WHAT HAPPENED HERE


                            //    //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //}

                            if (file.DLC_Name.ToLower() == info.Name.ToLower()) info.Name = random.Next(0, 100000) + info.Name;
                            if (file.Song_Title.ToLower() == info.SongInfo.SongDisplayName.ToLower() && Is_Original == "No")
                            { 
                                info.SongInfo.SongDisplayName += " [a." + (MultiTrack_Version != "" ? MultiTrack_Version + (alt.ToInt32()).ToString() : (alt.ToInt32()).ToString() + ((author == null || author == "" || author == "Custom Song Creator") ? "" : " " + author )) + "]";// ;//random.Next(0, 100000).ToString()
                                alt = (alt.ToInt32() + 1).ToString();
                                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                                {
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                                }
                            }//if (file.Song_Title_Sort == info.SongInfo.SongDisplayNameSort) info.SongInfo.SongDisplayNameSort += random.Next(0, 100000);

                            // rtxt_StatisticsOnReadDLCs.Text = "highest " + altver + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                    
                        }


                        //Doublechecking that no DLC Name is the same (last import 1500 songs generate once such exception :) )
                        //var SearchCmd = "SELECT * FROM Main WHERE DLC_Name='" + info.Name + "'";
                        //DataSet dms = new DataSet();
                        //OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd, cnn);
                        //dan.Fill(dms, "Main");
                        DataSet dms = new DataSet(); dms = DLCManager.SelectFromDB("Main", "SELECT * FROM Main WHERE DLC_Name='" + info.Name + "'");
                        if (dms.Tables[0].Rows.Count > 1) info.Name = random.Next(0, 100000) + info.Name;




                        //if (artist != "Ignore")
                        //{
                        //    b ++;
                        //rtxt_StatisticsOnReadDLCs.Text = txt_RocksmithDLCPath.Text + "\\" + original_FileName + " ccccc\n"+ dupli_Path_Import + "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;

                        //} //exit if an update/alternate=insert was triggered..autom or by choice(asses)
                        //else
                        b++;
                        //rtxt_StatisticsOnReadDLCs.Text =  "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                        IDD = file.ID; //Save Id in case of update or asses-update
                                       //rtxt_StatisticsOnReadDLCs.Text = "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                        jk = file.Version;
                        //rtxt_StatisticsOnReadDLCs.Text = "dd" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                        k = file.Author;
                        if (b >= norows || artist != "Insert" || IgnoreRest) break;
                    }
                //rtxt_StatisticsOnReadDLCs.Text = "6" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                //  var a = false;
                if (duplit) return "0";
                else
                {
                    //Move file New file to duplicates Ignore is select
                    if (artist == "Ignore" && chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        //rtxt_StatisticsOnReadDLCs.Text = dupli_Path_Import + "\\" + original_FileName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (!File.Exists(dupli_Path_Import + "\\" + original_FileName))
                        {
                            Available_Duplicate = "Yes";
                            try
                            {
                                File.Move(txt_RocksmithDLCPath.Text + "\\" + original_FileName, dupli_Path_Import + "\\" + original_FileName);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show("Issues when moving to duplicate folder after dupli ignore" + "-" + ex.Message + filename);
                            }
                        }
                        else File.Delete(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                        //dELETE DUPLCAITION FODLER
                        try { Directory.Delete(unpackedDir); }
                        catch (IOException ex)
                        { Console.Write(ex.Message); }

                    }

                    //Move file Original file to duplicates if Main DB record is being overitten
                    if (artist == "Update" && chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        //sel = "SELECT Original_FileName, Available_Old FROM Main WHERE ID=" + IDD + ";";
                        //DataSet dzr = new DataSet();
                        //OleDbDataAdapter dad = new OleDbDataAdapter(sel, cnn);
                        //dad.Fill(dzr, "Main");
                        //dad.Dispose();
                        DataSet dzr = new DataSet(); dzr = DLCManager.SelectFromDB("Main", "SELECT Original_FileName, Available_Old FROM Main WHERE ID=" + IDD + ";");
                        //rtxt_StatisticsOnReadDLCs.Text = dupli_Path_Import + "\\" + original_FileName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        var Original_FileName = dzr.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (File.Exists(dupli_Path_Import + "\\" + Original_FileName) && dzr.Tables[0].Rows[0].ItemArray[1].ToString() != "Yes")
                        {
                            if (!File.Exists(txt_TempPath.Text + "\\0_old\\" + Original_FileName))
                            {
                                Available_Duplicate = "Yes";
                                try
                                {
                                    File.Move(txt_TempPath.Text + "\\0_old\\" + Original_FileName, dupli_Path_Import + "\\" + Original_FileName);
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Issues when moving to duplicate folder at dupli Update" + "-" + ex.Message + filename);
                                }
                            }
                            else File.Delete(txt_RocksmithDLCPath.Text + "\\" + Original_FileName);
                        }

                    }

                    //var platformdlc = "";
                    //var Has_Track_No = "";
                    PreviewTime = "";
                    PreviewLenght = "";
                    var recalc_Preview = false;
                    var duration = ""; var ogg = "";
                    if (info.OggPreviewPath != null) ogg = info.OggPreviewPath.Replace(".wem", "_fixed.ogg");
                    if (File.Exists(ogg))
                    {
                        using (var vorbis = new NVorbis.VorbisReader(ogg))
                        {
                            duration = vorbis.TotalTime.ToString();
                            if ((duration.Split(':'))[0] == "00" && (duration.Split(':'))[1] == "00")
                                PreviewLenght = (duration.Split(':'))[2];
                            else
                                PreviewLenght = duration;
                            string[] timepiece = duration.Split(':');
                            if (timepiece[0] != "00" || timepiece[1] != "00")
                                recalc_Preview = true;//&& timepieces[2].ToInt32() > file.PreviewLenght.ToInt32()) ;}
                        }
                    }


                    //Set Preview

                    if (chbx_Additional_Manipulations.GetItemChecked(34) && info.OggPreviewPath == null || (chbx_Additional_Manipulations.GetItemChecked(55) && ((audio_hash != "" && audio_hash == audioPreview_hash) || recalc_Preview)))
                    {
                        rtxt_StatisticsOnReadDLCs.Text = "Trying to add preview as missing.\n" + rtxt_StatisticsOnReadDLCs.Text;
                        var startInfo = new ProcessStartInfo();
                        startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
                        startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
                        var t = info.OggPath.Replace(".wem", "_fixed.ogg"); //"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
                        var tt = t.Replace("_fixed.ogg", "_preview.ogg");
                        var times = ConfigRepository.Instance()["dlcm_PreviewStart"]; //00:30
                        string[] timepieces = times.Split(':');
                        TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                        startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                            t,
                                                            tt,
                                                            r.TotalMilliseconds,
                                                            (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
                        startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                        if (File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                if (DDC.ExitCode == 0)
                                {
                                    if (!File.Exists(ConfigRepository.Instance()["general_wwisepath"] + "\\Authoring\\Win32\\Release\\bin\\Wwise.exe"))//Help\\WwiseHelp_en.chm"))//
                                    {
                                        ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2014.1.6 build 5318with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                                        frm1.ShowDialog();
                                        if (frm1.IgnoreSong) return "0";// break;
                                        if (frm1.StopImport) { j = 10; return "0"; }// break; }
                                    }
                                    MainDB.Converters(tt, MainDB.ConverterTypes.Ogg2Wem, false);
                                    info.OggPreviewPath = tt.Replace(".ogg", ".wem");
                                    //2info.= tt.Replace();

                                    if (File.Exists(info.OggPreviewPath))
                                        using (FileStream fss = File.OpenRead(info.OggPreviewPath))
                                        {
                                            SHA1 sha = new SHA1Managed();
                                            audioPreview_hash = BitConverter.ToString(sha.ComputeHash(fss));
                                            fss.Close();
                                        }
                                    else
                                    {
                                        info.OggPreviewPath = "";
                                    }
                                    PreviewTime = ConfigRepository.Instance()["dlcm_PreviewStart"];
                                    PreviewLenght = ConfigRepository.Instance()["dlcm_PreviewLenght"];
                                }
                                //Set the the preview time
                                //ogg = info.OggPreviewPath.Replace(".wem", "_fixed.ogg");
                                //if (File.Exists(ogg))
                                //    //if (PreviewLenght == "")
                                //    using (var vorbis = new NVorbis.VorbisReader(ogg))
                                //    {
                                //        duration = vorbis.TotalTime.ToString();
                                //        PreviewLenght = duration;// ConfigRepository.Instance()["dlcm_PreviewLenght"];
                                //    }
                            }
                    }
                    //Fix _preview.OGG having a diff name than _preview.wem after oggged
                    //var destination_dir1 = source_dir1;
                    var previewN = info.OggPreviewPath.ToString().Replace(".wem", ".ogg");
                    //var r = true;
                    //info.OggPath.Substring(info.OggPath.LastIndexOf("\\") + 1, info.OggPath.Substring(pos).Length);
                    if (!File.Exists(previewN))
                    {
                        foreach (string preview_name in Directory.GetFiles(unpackedDir, "*_preview.wem", System.IO.SearchOption.AllDirectories))
                        {
                            //if (!File.Exists(preview_name.Replace(".wem",".ogg")))
                            foreach (string file_name in Directory.GetFiles(unpackedDir, "*.ogg", System.IO.SearchOption.AllDirectories))
                            {
                                //if (file_name.Replace("_preview.wem", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))// || file_name.IndexOf("_fixed") == 0)
                                if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
                                {
                                    var tl = previewN;
                                    var hg = preview_name;
                                    previewN = preview_name.Replace(".wem", ".ogg");
                                    //r = false;
                                    if (!File.Exists(previewN))
                                    {
                                        try
                                        {
                                            File.Copy(file_name, previewN, true);
                                            File.Delete(file_name);
                                        }
                                        catch (Exception ee)
                                        {
                                            rtxt_StatisticsOnReadDLCs.Text = "FAILED1 preview fix" + ee.Message + "----" + info.OggPath + "\n -" + previewN + "\n -" + file_name + ".ogg" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            Console.WriteLine(ee.Message);
                                        }
                                    }
                                }
                            }
                        }
                        if (File.Exists(previewN))
                        {
                            using (var vorbis = new NVorbis.VorbisReader(previewN))
                            {
                                if ((vorbis.TotalTime.ToString().Split(':'))[0] == "00" && (vorbis.TotalTime.ToString().Split(':'))[1] == "00")
                                    PreviewLenght = (vorbis.TotalTime.ToString().Split(':'))[2];// ConfigRepository.Instance()["dlcm_PreviewLenght"];
                                else PreviewLenght = vorbis.TotalTime.ToString();
                            }
                        }
                    }


                    //Define final path for the imported song
                    //rtxt_StatisticsOnReadDLCs.Text = info.PackageVersion + " ccccc\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var norm_path = txt_TempPath.Text + "\\" + platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + trackno.ToString() + "_" + info.SongInfo.SongDisplayName + "_" + random.Next(0, 100000);
                    //if (artist == "ignore") ;

                    //@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=
                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                    command = connection.CreateCommand();
                    //rtxt_StatisticsOnReadDLCs.Text = "00 " + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                    if (artist == "Update")
                    {
                        //Update MainDB
                        rtxt_StatisticsOnReadDLCs.Text = "Updating / Overriting " + IDD + "-" + j + "-" + info.PackageVersion + "-" + k + ".." + rtxt_StatisticsOnReadDLCs.Text;

                        //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                        command.CommandText = "UPDATE Main SET ";
                        command.CommandText += "Import_Path = @param1, ";
                        command.CommandText += "Original_FileName = @param2, ";
                        command.CommandText += "Current_FileName = @param3, ";
                        command.CommandText += "File_Hash = @param4, ";
                        command.CommandText += "Original_File_Hash = @param5, ";
                        command.CommandText += "File_Size = @param6, ";
                        command.CommandText += "Import_Date = @param7, ";
                        command.CommandText += "Folder_Name = @param8, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Song_Title = @param9, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Song_Title_Sort = @param10, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Album = @param11, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Artist = @param12, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Artist_Sort = @param13, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.CommandText += "Album_Year = @param14, ";
                        command.CommandText += "Version = @param15, ";
                        command.CommandText += "AverageTempo = @param16, ";
                        command.CommandText += "Volume = @param17, ";
                        command.CommandText += "Preview_Volume = @param18, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "DLC_Name = @param19, ";
                        command.CommandText += "DLC_AppID = @param20, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.CommandText += "AlbumArtPath = @param21, ";
                        command.CommandText += "AudioPath = @param22, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "audioPreviewPath = @param23, ";
                        command.CommandText += "Has_Bass = @param24, ";
                        command.CommandText += "Has_Guitar = @param25, ";
                        command.CommandText += "Has_Lead = @param26, ";
                        command.CommandText += "Has_Rhythm = @param27, ";
                        command.CommandText += "Has_Combo = @param28, ";
                        command.CommandText += "Has_Vocals = @param29, ";
                        command.CommandText += "Has_Sections = @param30, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.CommandText += "Has_Cover = @param31, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Has_Preview = @param32, ";
                        command.CommandText += "Has_Custom_Tone = @param33, ";
                        command.CommandText += "Has_DD = @param34, ";
                        command.CommandText += "Has_Version = @param35, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Has_Author = @param36, ";
                        command.CommandText += "Tunning = @param37, ";
                        command.CommandText += "Bass_Picking = @param38, ";
                        command.CommandText += "DLC = @param39, ";
                        command.CommandText += "SignatureType = @param40, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Author = @param41, ";
                        command.CommandText += "ToolkitVersion = @param42, ";
                        command.CommandText += "Is_Original = @param43, ";
                        command.CommandText += "Is_Alternate = @param44, ";
                        command.CommandText += "Alternate_Version_No = @param45, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.CommandText += "AlbumArt_Hash = @param46, ";
                        command.CommandText += "Audio_Hash = @param47, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "audioPreview_Hash = @param48, ";
                        command.CommandText += "Bass_Has_DD = @param49, ";
                        command.CommandText += "Has_Bonus_Arrangement = @param50, ";
                        command.CommandText += "Available_Duplicate = @param51, ";
                        command.CommandText += "Available_Old = @param52, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Description = @param53, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "Comments = @param54, ";
                        command.CommandText += "OggPath = @param55, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "OggPreviewPath = @param56, ";
                        command.CommandText += "Has_Track_No = @param57, ";
                        command.CommandText += "Track_No = @param58, ";
                        command.CommandText += "Platform = @param59, ";
                        command.CommandText += "Is_Multitrack = @param60, ";
                        command.CommandText += "MultiTrack_Version = @param61, ";
                        command.CommandText += "YouTube_Link = @param62, ";
                        command.CommandText += "CustomsForge_Link = @param63, ";
                        command.CommandText += "CustomsForge_Like = @param64, ";
                        command.CommandText += "CustomsForge_ReleaseNotes = @param65, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "PreviewTime = @param66, ";
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.CommandText += "PreviewLenght = @param67, ";
                        command.CommandText += "Pack = @param68, ";
                        command.CommandText += "Song_Lenght = @param69, ";
                        command.CommandText += "File_Creation_Date = @param70,";
                        command.CommandText += "Is_Live = @param71, ";
                        command.CommandText += "Live_Details = @param72 ";
                        command.CommandText += "WHERE ID = " + IDD;

                        command.Parameters.AddWithValue("@param1", import_path);
                        command.Parameters.AddWithValue("@param2", original_FileName);
                        command.Parameters.AddWithValue("@param3", original_FileName);
                        command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                        command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                        command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4].ToString());
                        command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5].ToString());
                        command.Parameters.AddWithValue("@param8", unpackedDir);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                        command.Parameters.AddWithValue("@param15", ((info.PackageVersion == null) ? "1" : info.PackageVersion));
                        command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                        command.Parameters.AddWithValue("@param17", info.Volume);
                        command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param19", info.Name);
                        command.Parameters.AddWithValue("@param20", AppIdD);
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param22", info.OggPath);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));
                        command.Parameters.AddWithValue("@param24", Bass);
                        command.Parameters.AddWithValue("@param25", Guitar);
                        command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                        command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                        command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                        command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                        command.Parameters.AddWithValue("@param30", sect1on);
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes"));
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param33", Tones_Custom);
                        command.Parameters.AddWithValue("@param34", DD);
                        command.Parameters.AddWithValue("@param35", ((info.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param36", Has_author);//((((author != "" && tkversion != "") || author == "Custom Song Creator") && Is_Original == "No") ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param37", Tunings);
                        command.Parameters.AddWithValue("@param38", PluckedType);
                        command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                        command.Parameters.AddWithValue("@param40", info.SignatureType);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param41", author);//
                        command.Parameters.AddWithValue("@param42", tkversion);
                        command.Parameters.AddWithValue("@param43", Is_Original);
                        command.Parameters.AddWithValue("@param44", ((alt == "" || alt == null) ? "No" : "Yes"));
                        command.Parameters.AddWithValue("@param45", alt);
                        if (chbx_Additional_Manipulations.GetItemChecked(61)) command.Parameters.AddWithValue("@param46", art_hash);
                        command.Parameters.AddWithValue("@param47", audio_hash);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param48", audioPreview_hash);
                        command.Parameters.AddWithValue("@param49", Bass_Has_DD ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param50", bonus ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param51", Available_Duplicate ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param52", Available_Old ?? DBNull.Value.ToString());
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param53", description ?? DBNull.Value.ToString());
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param54", comment ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param55", info.OggPath.Replace(".wem", "_fixed.ogg"));
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param56", (info.OggPreviewPath == null ? DBNull.Value.ToString() : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") : info.OggPreviewPath.Replace(".wem", ".ogg"))));
                        command.Parameters.AddWithValue("@param57", (trackno == 0 ? "No" : "Yes"));
                        command.Parameters.AddWithValue("@param58", trackno.ToString());
                        command.Parameters.AddWithValue("@param59", platformTXT);
                        command.Parameters.AddWithValue("@param60", Is_MultiTrack);
                        command.Parameters.AddWithValue("@param61", MultiTrack_Version);
                        command.Parameters.AddWithValue("@param62", YouTube_Link);
                        command.Parameters.AddWithValue("@param63", CustomsForge_Link);
                        command.Parameters.AddWithValue("@param64", CustomsForge_Like);
                        command.Parameters.AddWithValue("@param65", CustomsForge_ReleaseNotes);
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param66", PreviewTime ?? DBNull.Value.ToString());
                        if (chbx_Additional_Manipulations.GetItemChecked(60)) command.Parameters.AddWithValue("@param67", PreviewLenght ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param68", ds.Tables[0].Rows[i].ItemArray[6].ToString());
                        command.Parameters.AddWithValue("@param69", SongLenght);
                        command.Parameters.AddWithValue("@param70", ds.Tables[0].Rows[i].ItemArray[7].ToString());
                        command.Parameters.AddWithValue("@param71", IsLive);
                        command.Parameters.AddWithValue("@param72", LiveDetails);
                        //EXECUTE SQL/UPDATE
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                            //Deleted old folder
                            Directory.Delete(folder_name, true);
                            ////remove original dir TO DO
                            //Directory.Delete(source_dir, true);
                            //move old/aleady imported&saved file
                            if (chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                            {
                                rtxt_StatisticsOnReadDLCs.Text = old_Path_Import + "\\" + filename + "dupli_Path_Import arrangement:" + dupli_Path_Import + "\\" + filename + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                try
                                {
                                    if (!File.Exists(dupli_Path_Import + "\\" + filename))
                                        if (File.Exists(old_Path_Import + "\\" + filename))
                                        {
                                            File.Move(old_Path_Import + "\\" + filename, dupli_Path_Import + "\\" + filename);
                                            Available_Duplicate = "Yes";
                                        }
                                        else rtxt_StatisticsOnReadDLCs.Text = "___" + rtxt_StatisticsOnReadDLCs.Text;
                                    else
                                    {
                                        File.Delete(txt_RocksmithDLCPath.Text + "\\" + filename);
                                        Available_Duplicate = "Yes";
                                    }
                                    rtxt_StatisticsOnReadDLCs.Text = "deleting...dele...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                catch (Exception ex)
                                {
                                    Console.Write(ex.Message);
                                    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    //MessageBox.Show("Issues at duplicate folder" + "-" + ex.Message + filename);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Update Main DB connection in Import ! " + DB_Path + "-" + original_FileName + "-" + command.CommandText);

                            //throw;
                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                    }

                    //Read Track no
                    //www.metrolyrics.com: Nirvana Bleach Swap Meet
                    if (ExistingTrackNo != "")
                    {
                        trackno = ExistingTrackNo.ToInt32();
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.google.com/webhp?sourceid=chrome-instant&ion=1&espv=2&es_th=1&ie=UTF-8#q=www.metrolyrics.com:" + info.SongInfo.Artist + info.SongInfo.Album + info.SongInfo.SongDisplayName.Replace(" ", "+"));
                        //request.Proxy = WebProxy.GetDefaultProxy();
                        //request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    }
                    //else

                    //rtxt_StatisticsOnReadDLCs.Text = Available_Duplicate + "==" + Available_Old + rtxt_StatisticsOnReadDLCs.Text;
                    if (artist == "Insert")
                    {
                        //Update by INSERT into Main DB+info.AlbumArtPath+"____________"
                        rtxt_StatisticsOnReadDLCs.Text = "Inserting " + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        //connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+DB_Path+";Persist Security Info=False");
                        //command = connection.CreateCommand();
                        //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                        command.CommandText = "INSERT INTO Main(";
                        command.CommandText += "Import_Path, ";//1-ds.Tables[0].Rows[i].ItemArray[1].ToString()
                        command.CommandText += "Original_FileName, ";//2-ds.Tables[0].Rows[i].ItemArray[2].ToString()
                        command.CommandText += "Current_FileName, ";//3-ds.Tables[0].Rows[i].ItemArray[2].ToString()
                        command.CommandText += "File_Hash, ";//4-ds.Tables[0].Rows[i].ItemArray[3].ToString()
                        command.CommandText += "Original_File_Hash, ";//5-ds.Tables[0].Rows[i].ItemArray[3].ToString()
                        command.CommandText += "File_Size, ";//6-ds.Tables[0].Rows[i].ItemArray[4].ToString()
                        command.CommandText += "Import_Date, ";//7-ds.Tables[0].Rows[i].ItemArray[5].ToString()
                        command.CommandText += "Folder_Name, ";//8-unpackedDir
                        command.CommandText += "Song_Title, ";//9-info.SongInfo.SongDisplayName
                        command.CommandText += "Song_Title_Sort, ";//10-info.SongInfo.SongDisplayNameSort
                        command.CommandText += "Album, ";//11-info.SongInfo.Album
                        command.CommandText += "Artist, ";//12-info.SongInfo.Artist
                        command.CommandText += "Artist_Sort, ";//13-info.SongInfo.ArtistSort
                        command.CommandText += "Album_Year, ";//14-info.SongInfo.SongYear
                        command.CommandText += "Version, ";//15-((info.PackageVersion == null) ? "1" : info.PackageVersion)
                        command.CommandText += "AverageTempo, ";//16-info.SongInfo.AverageTempo
                        command.CommandText += "Volume, ";//17-info.Volume
                        command.CommandText += "Preview_Volume, ";//18-info.PreviewVolume
                        command.CommandText += "DLC_Name, ";//19-info.Name
                        command.CommandText += "DLC_AppID, ";//20-info.AppId
                        command.CommandText += "AlbumArtPath, ";//21-info.AlbumArtPath
                        command.CommandText += "AudioPath, ";//22-info.OggPath
                        command.CommandText += "audioPreviewPath, ";//23-info.OggPreviewPath
                        command.CommandText += "Has_Bass, ";//24-Bass
                        command.CommandText += "Has_Guitar, ";//25-Guitar
                        command.CommandText += "Has_Lead, ";//26-((Lead != "") ? Lead : "No")
                        command.CommandText += "Has_Rhythm, ";//27-((Rhythm != "") ? Rhythm : "No")
                        command.CommandText += "Has_Combo, ";//28-((Combo != "") ? Combo : "No")
                        command.CommandText += "Has_Vocals, ";//29-((Vocals != "") ? Vocals : "No")
                        command.CommandText += "Has_Sections, ";//30-"sect1on"
                        command.CommandText += "Has_Cover, ";//31-((info.AlbumArtPath != null) ? "Yes" : "No")
                        command.CommandText += "Has_Preview, ";//32-((info.OggPreviewPath != null) ? "Yes" : "No")
                        command.CommandText += "Has_Custom_Tone, ";//33-Tones_Custom
                        command.CommandText += "Has_DD, ";//34-DD
                        command.CommandText += "Has_Version, ";//35-((info.PackageVersion != "" && tkversion != "") ? "Yes" : "No")
                        command.CommandText += "Has_Author, ";//36-((author != "" && tkversion != "") ? "Yes" : "No")
                        command.CommandText += "Tunning, ";//37-Tunings
                        command.CommandText += "Bass_Picking, ";//38-PluckedType
                        command.CommandText += "DLC, ";//39-((info.PackageVersion == null) ? "Original" : "CDLC")
                        command.CommandText += "SignatureType, ";//40-info.SignatureType
                        command.CommandText += "Author, ";//41-((author != "") ? author : (tkversion != "" ? "Custom Song Creator" : ""))
                        command.CommandText += "ToolkitVersion, ";//42-tkversion
                        command.CommandText += "Is_Original, ";//43-tkversion
                        command.CommandText += "Is_Alternate, ";//43-tkversion
                        command.CommandText += "Alternate_Version_No, ";//44-alt
                        command.CommandText += "AlbumArt_Hash, ";
                        command.CommandText += "Audio_Hash, ";
                        command.CommandText += "audioPreview_Hash, ";
                        command.CommandText += "Bass_Has_DD, ";
                        command.CommandText += "Has_Bonus_Arrangement, ";
                        command.CommandText += "Available_Duplicate, ";
                        command.CommandText += "Available_Old, ";
                        command.CommandText += "Description, ";
                        command.CommandText += "Comments, ";
                        command.CommandText += "OggPath, ";
                        command.CommandText += "OggPreviewPath, ";
                        command.CommandText += "Has_Track_No, ";
                        command.CommandText += "Track_No, ";
                        command.CommandText += "Platform, ";
                        command.CommandText += "Is_Multitrack, ";
                        command.CommandText += "MultiTrack_Version, ";
                        command.CommandText += "YouTube_Link, ";
                        command.CommandText += "CustomsForge_Link, ";
                        command.CommandText += "CustomsForge_Like, ";
                        command.CommandText += "CustomsForge_ReleaseNotes, ";
                        command.CommandText += "PreviewTime, ";
                        command.CommandText += "PreviewLenght, ";
                        command.CommandText += "Pack, ";
                        command.CommandText += "Song_Lenght, ";
                        command.CommandText += "File_Creation_Date, ";
                        command.CommandText += "Is_Live, ";
                        command.CommandText += "Live_Details ";
                        command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                        command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                        command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                        command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                        command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48,@param49";
                        command.CommandText += ",@param50,@param51,@param52,@param53,@param54,@param55,@param56,@param57,@param58,@param59";
                        command.CommandText += ",@param60,@param61,@param62,@param63,@param64,@param65,@param66,@param67,@param68,@param69";// +")"; //,@param44,@param45,@param46,@param47,@param48,@param49
                        command.CommandText += ",@param70,@param71,@param72" + ")"; //,@param33,@param44,@param44,@param45,@param46,@param47,@param48,@param49


                        command.Parameters.AddWithValue("@param1", import_path);
                        command.Parameters.AddWithValue("@param2", original_FileName);
                        command.Parameters.AddWithValue("@param3", original_FileName);
                        command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3]);
                        command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3]);
                        command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4]);
                        command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5]);
                        command.Parameters.AddWithValue("@param8", unpackedDir);
                        command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                        command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                        command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                        command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                        command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                        command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                        command.Parameters.AddWithValue("@param15", ((info.PackageVersion == null) ? "1" : info.PackageVersion));
                        command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                        command.Parameters.AddWithValue("@param17", info.Volume);
                        command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                        command.Parameters.AddWithValue("@param19", info.Name);
                        command.Parameters.AddWithValue("@param20", AppIdD);
                        command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param22", info.OggPath);
                        command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));// ((info.OggPreviewPath == "") ? DBNull.Value : info.OggPreviewPath));
                        command.Parameters.AddWithValue("@param24", Bass);
                        command.Parameters.AddWithValue("@param25", Guitar);
                        command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                        command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                        command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                        command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                        command.Parameters.AddWithValue("@param30", sect1on);
                        command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes"));
                        command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param33", Tones_Custom);
                        command.Parameters.AddWithValue("@param34", DD);
                        command.Parameters.AddWithValue("@param35", ((info.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param36", Has_author);//((((author != "" && tkversion != "") || author == "Custom Song Creator") && Is_Original == "No") ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param37", Tunings);
                        command.Parameters.AddWithValue("@param38", PluckedType);
                        command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                        command.Parameters.AddWithValue("@param40", info.SignatureType);
                        command.Parameters.AddWithValue("@param41", author);//
                        command.Parameters.AddWithValue("@param42", tkversion);
                        command.Parameters.AddWithValue("@param43", Is_Original);
                        command.Parameters.AddWithValue("@param44", ((alt == "" || alt == null) ? "No" : "Yes"));
                        command.Parameters.AddWithValue("@param45", alt ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param46", art_hash);
                        command.Parameters.AddWithValue("@param47", audio_hash);
                        command.Parameters.AddWithValue("@param48", audioPreview_hash);
                        command.Parameters.AddWithValue("@param49", Bass_Has_DD);
                        command.Parameters.AddWithValue("@param50", bonus);
                        command.Parameters.AddWithValue("@param51", Available_Duplicate);
                        command.Parameters.AddWithValue("@param52", Available_Old);
                        command.Parameters.AddWithValue("@param53", description);
                        command.Parameters.AddWithValue("@param54", comment);
                        command.Parameters.AddWithValue("@param55", info.OggPath.Replace(".wem", "_fixed.ogg"));
                        command.Parameters.AddWithValue("@param56", (info.OggPreviewPath == null ? DBNull.Value.ToString() : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") : info.OggPreviewPath.Replace(".wem", ".ogg"))));
                        command.Parameters.AddWithValue("@param57", (trackno == 0 ? "No" : "Yes"));
                        command.Parameters.AddWithValue("@param58", trackno.ToString());
                        command.Parameters.AddWithValue("@param59", platformTXT.ToString());
                        command.Parameters.AddWithValue("@param60", Is_MultiTrack);
                        command.Parameters.AddWithValue("@param61", MultiTrack_Version);
                        command.Parameters.AddWithValue("@param62", YouTube_Link);
                        command.Parameters.AddWithValue("@param63", CustomsForge_Link);
                        command.Parameters.AddWithValue("@param64", CustomsForge_Like);
                        command.Parameters.AddWithValue("@param65", CustomsForge_ReleaseNotes);
                        command.Parameters.AddWithValue("@param66", PreviewTime ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param67", PreviewLenght ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param68", ds.Tables[0].Rows[i].ItemArray[6]);
                        command.Parameters.AddWithValue("@param69", SongLenght);
                        command.Parameters.AddWithValue("@param70", ds.Tables[0].Rows[i].ItemArray[7]);
                        command.Parameters.AddWithValue("@param71", IsLive == null ? DBNull.Value.ToString():IsLive);
                        command.Parameters.AddWithValue("@param72", LiveDetails == null ? DBNull.Value.ToString():LiveDetails);
                        //EXECUTE SQL/INSERT
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = "error at update "+ex + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            throw;
                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                        //If No version found then defaulted to 1
                        //TO DO If default album cover then mark it as suck !?
                        //If no version found must by Rocksmith Original or DLC

                        rtxt_StatisticsOnReadDLCs.Text = "Records inserted in Main= " + (i + 1) + "..." + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = artist + "...||..." + rtxt_StatisticsOnReadDLCs.Text;
                    if (artist == "Insert" || artist == "Update") //Common set of action for all
                    {
                        //Get last inserted ID
                        //DataSet dus = new DataSet();
                        //OleDbDataAdapter dad = new OleDbDataAdapter("SELECT ID FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"", cnn);
                        //dad.Fill(dus, "Main");
                        //dad.Dispose();
                        DataSet dus = new DataSet(); dus = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"");

                        //rtxt_StatisticsOnReadDLCs.Text ="last id= " + dus.Tables[0].Rows[0].ItemArray[0].ToString() + "..." + rtxt_StatisticsOnReadDLCs.Text;

                        //OleDbDataAdapter objAdapter = new OleDbDataAdapter("SELECT @@IDENTITY AS 'ID';", cnn);

                        //Useful
                        // Get and Store IDENTITY (Primary Key) for further
                        // INSERTS in child table [Order Details]
                        //cmd.CommandText = "SELECT @@identity";
                        //string id = cmd.ExecuteScalar().ToString();
                        //objAdapter.Fill(dus, "Main");
                        //string strID = dus.Tables["Main"].Rows[0].ToString();

                        //UPDATE ArarngementsDB
                        CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                        int n = 0;
                        foreach (var arg in info.Arrangements)//, Type
                        {
                            command = connection.CreateCommand();
                            //ss = arg.SongXml.File.ToString();
                            //string XMLFile_hash="";
                            //using (FileStream fs = File.OpenRead(ss))
                            //{
                            //    SHA1 sha = new SHA1Managed();
                            //    XMLFile_hash = BitConverter.ToString(sha.ComputeHash(fs));
                            //}

                            try
                            {
                                var mss = arg.SongXml.File.ToString();
                                int poss = 0;
                                //var vs1 = "";
                                //var vs2 = "";
                                if (mss.Length > 0)
                                {
                                    poss = mss.ToString().LastIndexOf("\\") + 1;
                                    //rtxt_StatisticsOnReadDLCs.Text = norm_path+"__________" + arg.SongFile.File + "...\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    //    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    //    {
                                    //        vs2 = norm_path + "\\EOF\\" + mss.Substring(poss);
                                    //        vs1 = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                    //    }
                                    //    else
                                    //    {
                                    //        vs2 = norm_path + "\\songs\\arr\\" + mss.Substring(poss);
                                    //        vs1 = vs2.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));
                                    //    }
                                    //}
                                    //if (vs1.Length >= 248) arg.SongFile.File = shortenfile_Name(arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0])), vs1);
                                    //else arg.SongFile.File = vs1;
                                    //if (vs2.Length >= 248) arg.SongXml.File = shortenfile_Name(arg.SongXml.File, vs2);
                                    //else arg.SongXml.File = vs2;

                                    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    {
                                        arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                                        arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                    }
                                    else
                                    {
                                        arg.SongXml.File = norm_path + "\\songs\\arr\\" + mss.Substring(poss);
                                        arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));
                                    }
                                }
                                if (arg.SongFile.File.Length >= 260)
                                {
                                    norm_path = txt_TempPath.Text + "\\" + platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongDisplayName + "_" + random.Next(0, 100000);
                                    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    {
                                        arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                                        arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                    }
                                    else
                                    {
                                        arg.SongXml.File = norm_path + "\\songs\\arr\\" + mss.Substring(poss);
                                        arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));
                                    }

                                }
                                if (Rebuild)
                                {
                                    command.CommandText = "UPDATE Arrangements SET ";
                                    command.CommandText += "CDLC_ID = @param1, ";
                                    command.CommandText += "Arrangement_Name = @param2, ";
                                    command.CommandText += "Tunning = @param3, ";
                                    command.CommandText += "SNGFilePath = @param4, ";
                                    command.CommandText += "SNGFileName = @param5, ";
                                    command.CommandText += "SNGFileLLID = @param6, ";
                                    command.CommandText += "SNGFileUUID = @param7, ";
                                    command.CommandText += "XMLFilePath = @param8, ";
                                    command.CommandText += "XMLFileName = @param9, ";
                                    command.CommandText += "XMLFileLLID = @param10, ";
                                    command.CommandText += "XMLFileUUID = @param11, ";
                                    command.CommandText += "ArrangementSort = @param12, ";
                                    command.CommandText += "TuningPitch = @param13, ";
                                    command.CommandText += "ScrollSpeed = @param14, ";
                                    command.CommandText += "Bonus = @param15, ";
                                    command.CommandText += "ToneBase = @param16, ";
                                    command.CommandText += "ToneMultiplayer = @param17, ";
                                    command.CommandText += "ToneA = @param18, ";
                                    command.CommandText += "ToneB = @param19, ";
                                    command.CommandText += "ToneC = @param20, ";
                                    command.CommandText += "ToneD = @param21, ";
                                    command.CommandText += "Idd = @param22, ";
                                    command.CommandText += "MasterId = @param23, ";
                                    command.CommandText += "ArrangementType = @param24, ";
                                    command.CommandText += "String0 = @param25, ";
                                    command.CommandText += "String1 = @param26, ";
                                    command.CommandText += "String2 = @param27, ";
                                    command.CommandText += "String3 = @param28, ";
                                    command.CommandText += "String4 = @param29, ";
                                    command.CommandText += "String5 = @param30, ";
                                    command.CommandText += "PluckedType = @param31, ";
                                    command.CommandText += "RouteMask = @param32,";
                                    command.CommandText += "XMLFile_Hash = @param33,";
                                    command.CommandText += "SNGFileHash = @param34,";
                                    command.CommandText += "lastConversionDateTime = @param35,";
                                    command.CommandText += "Has_Sections = @param36";
                                }
                                else
                                {
                                    command.CommandText = "INSERT INTO Arrangements(";
                                    command.CommandText += "CDLC_ID, ";
                                    command.CommandText += "Arrangement_Name, ";
                                    command.CommandText += "Tunning, ";
                                    command.CommandText += "SNGFilePath, ";
                                    command.CommandText += "SNGFileName, ";
                                    command.CommandText += "SNGFileLLID, ";
                                    command.CommandText += "SNGFileUUID, ";
                                    command.CommandText += "XMLFilePath, ";
                                    command.CommandText += "XMLFileName, ";
                                    command.CommandText += "XMLFileLLID, ";
                                    command.CommandText += "XMLFileUUID, ";
                                    command.CommandText += "ArrangementSort, ";
                                    command.CommandText += "TuningPitch, ";
                                    command.CommandText += "ScrollSpeed, ";
                                    command.CommandText += "Bonus, ";
                                    command.CommandText += "ToneBase, ";
                                    command.CommandText += "ToneMultiplayer, ";
                                    command.CommandText += "ToneA, ";
                                    command.CommandText += "ToneB, ";
                                    command.CommandText += "ToneC, ";
                                    command.CommandText += "ToneD, ";
                                    command.CommandText += "Idd, ";
                                    command.CommandText += "MasterId, ";
                                    command.CommandText += "ArrangementType, ";
                                    command.CommandText += "String0, ";
                                    command.CommandText += "String1, ";
                                    command.CommandText += "String2, ";
                                    command.CommandText += "String3, ";
                                    command.CommandText += "String4, ";
                                    command.CommandText += "String5, ";
                                    command.CommandText += "PluckedType, ";
                                    command.CommandText += "RouteMask,";
                                    command.CommandText += "XMLFile_Hash,";
                                    command.CommandText += "SNGFileHash,";
                                    command.CommandText += "lastConversionDateTime,";
                                    command.CommandText += "Has_Sections";
                                    command.CommandText += ") VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                                    command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                                    command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                    command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36";
                                    command.CommandText += ")";
                                }
                                command.Parameters.AddWithValue("@param1", CDLC_ID);
                                command.Parameters.AddWithValue("@param2", arg.Name);
                                command.Parameters.AddWithValue("@param3", arg.Tuning ?? DBNull.Value.ToString());
                                command.Parameters.AddWithValue("@param4", (arg.SongFile.File ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param5", (arg.SongFile.Name ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param6", (arg.SongFile.LLID.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param7", (arg.SongFile.UUID.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param8", (arg.SongXml.File ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param9", (arg.SongXml.Name ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param10", (arg.SongXml.LLID.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param11", (arg.SongXml.UUID.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param12", (arg.ArrangementSort.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param13", (arg.TuningPitch.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param14", (arg.ScrollSpeed.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param15", (arg.BonusArr.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param16", (arg.ToneBase ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param17", (arg.ToneMultiplayer ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param18", (arg.ToneA ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param19", (arg.ToneB ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param20", (arg.ToneC ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param21", (arg.ToneD ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param22", (arg.Id.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param23", (arg.MasterId.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param24", (arg.ArrangementType.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param25", (arg.TuningStrings == null ? "" : arg.TuningStrings.String0.ToString()));
                                command.Parameters.AddWithValue("@param26", (arg.TuningStrings == null ? "" : arg.TuningStrings.String1.ToString()));
                                command.Parameters.AddWithValue("@param27", (arg.TuningStrings == null ? "" : arg.TuningStrings.String2.ToString()));
                                command.Parameters.AddWithValue("@param28", (arg.TuningStrings == null ? "" : arg.TuningStrings.String3.ToString()));
                                command.Parameters.AddWithValue("@param29", (arg.TuningStrings == null ? "" : arg.TuningStrings.String4.ToString()));
                                command.Parameters.AddWithValue("@param30", (arg.TuningStrings == null ? "" : arg.TuningStrings.String5.ToString()));
                                command.Parameters.AddWithValue("@param31", (arg.PluckedType.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param32", (arg.RouteMask.ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param33", (alist[n].ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param34", (blist[n].ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param35", (clist[n].ToString() ?? DBNull.Value.ToString()));
                                command.Parameters.AddWithValue("@param36", (dlist[n].ToString() ?? DBNull.Value.ToString()));
                                n++;

                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = "error at insert " + command.CommandText + "\n" + arg.Name + " " + arg.RouteMask.ToString() + rtxt_StatisticsOnReadDLCs.Text;
                                    throw;
                                }
                                finally
                                {
                                    if (connection != null) connection.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(CDLC_ID + "Can not open Arrangements DB connection in Import ! " + DB_Path + "-" + arg.Name + "-" + command.CommandText);
                            }
                        }
                        rtxt_StatisticsOnReadDLCs.Text = "Arrangements Updated " + info.Arrangements.Count + "...\n" + rtxt_StatisticsOnReadDLCs.Text;

                        //UPDATE TonesDB
                        CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                        foreach (var tn in info.TonesRS2014)//, Type
                        {
                            command = connection.CreateCommand();
                            try
                            {
                                if (Rebuild)
                                {
                                    command.CommandText = "Update INTO Tones(";
                                    command.CommandText += "CDLC_ID = @param1, ";
                                    command.CommandText += "Tone_Name = @param2, ";
                                    command.CommandText += "Is_Custom = @param3, ";
                                    command.CommandText += "SortOrder = @param4, ";
                                    command.CommandText += "Volume = @param5, ";
                                    command.CommandText += "Keyy = @param6, ";
                                    command.CommandText += "NameSeparator = @param7, ";
                                    command.CommandText += "AmpType = @param8, ";
                                    command.CommandText += "AmpCategory = @param9, ";
                                    //command.CommandText += "AmpKnobValues = @param10, ";
                                    command.CommandText += "AmpPedalKey = @param11, ";
                                    command.CommandText += "CabinetCategory = @param12, ";
                                    //command.CommandText += "CabinetKnobValues = @param13, ";
                                    command.CommandText += "CabinetPedalKey = @param14, ";
                                    command.CommandText += "CabinetType = @param15, ";
                                    command.CommandText += "PostPedal1 = @param16, ";
                                    command.CommandText += "PostPedal2 = @param17, ";
                                    command.CommandText += "PostPedal3 = @param18, ";
                                    command.CommandText += "PostPedal4 = @param19, ";
                                    command.CommandText += "PrePedal1 = @param20, ";
                                    command.CommandText += "PrePedal2 = @param21, ";
                                    command.CommandText += "PrePedal3 = @param22, ";
                                    command.CommandText += "PrePedal4 = @param23, ";
                                    command.CommandText += "Rack1 = @param24, ";
                                    command.CommandText += "Rack2 = @param25, ";
                                    command.CommandText += "Rack3 = @param26, ";
                                    command.CommandText += "Rack4 = @param27";
                                }
                                else
                                {
                                    command.CommandText = "INSERT INTO Tones(";
                                    command.CommandText += "CDLC_ID, ";
                                    command.CommandText += "Tone_Name, ";
                                    command.CommandText += "Is_Custom, ";
                                    command.CommandText += "SortOrder, ";
                                    command.CommandText += "Volume, ";
                                    command.CommandText += "Keyy, ";
                                    command.CommandText += "NameSeparator, ";
                                    command.CommandText += "AmpType, ";
                                    command.CommandText += "AmpCategory, ";
                                    //command.CommandText += "AmpKnobValues, ";
                                    command.CommandText += "AmpPedalKey, ";
                                    command.CommandText += "CabinetCategory, ";
                                    //command.CommandText += "CabinetKnobValues, ";
                                    command.CommandText += "CabinetPedalKey, ";
                                    command.CommandText += "CabinetType, ";
                                    command.CommandText += "PostPedal1, ";
                                    command.CommandText += "PostPedal2, ";
                                    command.CommandText += "PostPedal3, ";
                                    command.CommandText += "PostPedal4, ";
                                    command.CommandText += "PrePedal1, ";
                                    command.CommandText += "PrePedal2, ";
                                    command.CommandText += "PrePedal3, ";
                                    command.CommandText += "PrePedal4, ";
                                    command.CommandText += "Rack1, ";
                                    command.CommandText += "Rack2, ";
                                    command.CommandText += "Rack3, ";
                                    command.CommandText += "Rack4";
                                    command.CommandText += ") VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9";//,@param10
                                    command.CommandText += ",@param11,@param12,@param14,@param15,@param16,@param17,@param18,@param19";//;,@param13
                                    command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27";
                                    command.CommandText += ")";
                                }
                                command.Parameters.AddWithValue("@param1", NullHandler(CDLC_ID));
                                command.Parameters.AddWithValue("@param2", NullHandler(tn.Name));
                                command.Parameters.AddWithValue("@param3", NullHandler(tn.IsCustom));
                                command.Parameters.AddWithValue("@param4", NullHandler(tn.SortOrder));
                                command.Parameters.AddWithValue("@param5", NullHandler(tn.Volume));
                                command.Parameters.AddWithValue("@param6", NullHandler(tn.Key));
                                command.Parameters.AddWithValue("@param7", NullHandler(tn.NameSeparator));
                                command.Parameters.AddWithValue("@param8", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Type)));
                                command.Parameters.AddWithValue("@param9", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Category)));
                                //command.Parameters.AddWithValue("@param10", (tn.GearList.Amp== null ==null ?DBNull.Value.ToString() :NullHandler(tn.GearList.Amp.KnobValues.Values)));
                                command.Parameters.AddWithValue("@param11", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.PedalKey)));
                                command.Parameters.AddWithValue("@param12", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Category)));
                                //command.Parameters.AddWithValue("@param13", ((tn.GearList.Cabinet == null) ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.KnobValues)));
                                command.Parameters.AddWithValue("@param14", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.PedalKey)));
                                command.Parameters.AddWithValue("@param15", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Type)));
                                command.Parameters.AddWithValue("@param16", NullHandler(tn.GearList.PostPedal1));
                                command.Parameters.AddWithValue("@param17", NullHandler(tn.GearList.PostPedal2));
                                command.Parameters.AddWithValue("@param18", NullHandler(tn.GearList.PostPedal3));
                                command.Parameters.AddWithValue("@param19", NullHandler(tn.GearList.PostPedal4));
                                command.Parameters.AddWithValue("@param20", NullHandler(tn.GearList.PrePedal1));
                                command.Parameters.AddWithValue("@param21", NullHandler(tn.GearList.PrePedal2));
                                command.Parameters.AddWithValue("@param22", NullHandler(tn.GearList.PrePedal3));
                                command.Parameters.AddWithValue("@param23", NullHandler(tn.GearList.PrePedal4));
                                command.Parameters.AddWithValue("@param24", NullHandler(tn.GearList.Rack1));
                                command.Parameters.AddWithValue("@param25", NullHandler(tn.GearList.Rack2));
                                command.Parameters.AddWithValue("@param26", NullHandler(tn.GearList.Rack3));
                                command.Parameters.AddWithValue("@param27", NullHandler(tn.GearList.Rack4));

                                //rtxt_StatisticsOnReadDLCs.Text = command.CommandText + "\n" + tn.Name + rtxt_StatisticsOnReadDLCs.Text;
                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = "error in arag " + CDLC_ID + " " + tn.Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    throw;
                                }
                                finally
                                {
                                    if (connection != null)
                                    {
                                        connection.Close();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(CDLC_ID + "Can not open Tones DB connection in Import ! " + DB_Path + "-" + tn.Name + "-" + command.CommandText);
                            }
                        }
                        rtxt_StatisticsOnReadDLCs.Text = "ToneDB Updated " + info.TonesRS2014.Count + "..." + rtxt_StatisticsOnReadDLCs.Text;

                        //Move Extracted Song to Temp Folder
                        int pos = 0;
                        int l = 0;
                        DataSet dis = new DataSet();
                        try //Move from _import into Temp folder (copy+delete as move sometimes fails)
                        {
                            //Directory.(unpackedDir, norm_path);
                            string source_dir = @unpackedDir;
                            string destination_dir = @norm_path;

                            // substring is to remove destination_dir absolute path (E:\).

                            // Create subdirectory structure in destination    
                            foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                            {
                                Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                                // Example:
                                //     > C:\sources (and not C:\E:\sources)
                            }

                            foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                            }
                            Directory.Delete(source_dir, true);
                            //var ee = "";
                            //rtxt_StatisticsOnReadDLCs.Text = " DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        catch (Exception ee)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                            Console.WriteLine(ee.Message);
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(15)) //16. Move Original Imported files to temp/0_old                               
                        {
                            //Move imported psarc into the old folder
                            //rtxt_StatisticsOnReadDLCs.Text = "predone" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                            try
                            {
                                // if (!File.Exists(txt_RocksmithDLCPath.Text + "\\" + original_FileName))
                                File.Copy(txt_RocksmithDLCPath.Text + "\\" + original_FileName, old_Path_Import + "\\" + original_FileName, true);
                                File.Delete(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                                Available_Old = "Yes";
                                rtxt_StatisticsOnReadDLCs.Text = "File Moved to old" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                            }
                            catch (System.IO.FileNotFoundException ee)
                            {
                                rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                Console.WriteLine(ee.Message);
                            }
                        }

                        //Fixing any _preview_preview issue..Start
                        //Correct moved file path audio,preview
                        //Add wem
                        //Corrent arrangements file path
                        cmd = "UPDATE Main SET Available_Old=\"" + Available_Old + "\",";
                        //var cmdA = "UPDATE Arrangements SET";
                        //rtxt_StatisticsOnReadDLCs.Text = "0" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        var audiopath = "";
                        var audioprevpath = "";
                        var ms = "";
                        ms = info.AlbumArtPath;
                        var cmd2 = "";
                        if (ms != "" && ms != null)
                        {
                            //rtxt_StatisticsOnReadDLCs.Text ="\n" +AlbumArtPath +"\n"+ info.AlbumArtPath+"\n000" + ms + "..." + rtxt_StatisticsOnReadDLCs.Text;
                            pos = ms.ToString().LastIndexOf("\\") + 1;
                            if (AlbumArtPath == info.AlbumArtPath)
                                if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                else
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\gfxassets\\album_art\\" + ms.Substring(pos) + "\"";
                            else //Override Album Art during the Duplication assements process
                            {
                                //rtxt_StatisticsOnReadDLCs.Text = "\nimg override"+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                                cmd += " AlbumArt_Hash=\"" + art_hash + "\", AlbumArtPath=\"" + AlbumArtPath + "\"";
                            }

                            //If Cover was applied to the original then update its album art
                            if (dupliID != "")
                            {
                                cmd2 = cmd + " WHERE ID=" + dupliID;
                                DataSet dhs = new DataSet(); dhs = DLCManager.UpdateDB("Main", cmd2 + ";");
                                //DataSet dhs = new DataSet();
                                //using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                                //{// 1. If hash already exists do not insert
                                //    OleDbDataAdapter dBs = new OleDbDataAdapter(cmd2, cBn);
                                //    dBs.Fill(dhs, "Main");
                                //    dBs.Dispose();
                                //    rtxt_StatisticsOnReadDLCs.Text = "Main DB aLBUM updated after DIR Moved&DUPLICATRE REASIG" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                //}
                            }
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = "1" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        pos = (info.OggPath.LastIndexOf(".wem"));
                        ms = info.OggPath;

                        var path_decom1 = "";
                        var path_decom2 = "";
                        if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                        {
                            path_decom1 = "\\Toolkit\\";
                            path_decom2 = "\\EOF\\";
                        }
                        else
                        {
                            path_decom1 = "\\audio\\" + ((platformTXT.ToString() == "Pc") ? "windows" : ((platformTXT.ToString() == "Mac") ? "mac" : ((platformTXT.ToString() == "PS3") ? "ps3" : (platformTXT.ToString() == "Xbox360") ? "xbox360" : ""))) + "\\";
                            path_decom2 = "\\audio\\" + ((platformTXT.ToString() == "Pc") ? "windows" : ((platformTXT.ToString() == "Mac") ? "mac" : ((platformTXT.ToString() == "PS3") ? "ps3" : (platformTXT.ToString() == "Xbox360") ? "xbox360" : ""))) + "\\"; //"\\songs\\arr\\";
                        }


                        var source_dir1 = norm_path + path_decom1;// Path.GetDirectoryName(info.OggPath);
                                                                  //Delete any Wav file created..by....? ccc
                        foreach (string wav_name in Directory.GetFiles(source_dir1, "*_preview_fixed_preview*", System.IO.SearchOption.AllDirectories))
                        {
                            File.Delete(wav_name);
                        }

                        //Delete any Wav file created..by....?ccc
                        foreach (string wav_name in Directory.GetFiles(source_dir1, "*.wav", System.IO.SearchOption.AllDirectories))
                        {
                            File.Delete(wav_name);
                        }

                        //if (r) 
                        // if (!File.Exists(previewN.Replace("_preview.ogg", "_preview.wem")))
                        // {
                        //     var dpos = previewN.LastIndexOf("\\") + 1;
                        //     var dl = previewN.Substring(dpos).Length;
                        //     var daudiopath = norm_path + path_decom1 + previewN.Substring(dpos, dl-4);
                        //     previewN = daudiopath+"_preview.ogg";
                        //     //r = false;
                        // }
                        //Set the the preview time


                        if (ms.Length > 0 && pos > 1)
                        {
                            ms = ms.Substring(0, pos);
                            //if (info.OggPath.LastIndexOf("_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview"));
                            pos = ms.LastIndexOf("\\") + 1;
                            l = ms.Substring(pos).Length;
                            audiopath = norm_path + path_decom1 + ms.Substring(pos, l);
                            ////Gather song Lenght
                            //ogg = norm_path + path_decom2 + ms.Substring(pos, l) + "_fixed.ogg";
                            //using (var vorbis = new NVorbis.VorbisReader(ogg))
                            //{
                            //    duration = vorbis.TotalTime.ToString();
                            //    if (duration != SongLenght)
                            //    {
                            //        rtxt_StatisticsOnReadDLCs.Text = "diff in song lenghts: " + duration + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //        //MessageBox.Show("lenght" + duration + "-" + SongLenght);
                            //    }
                            //    SongLenght = duration;// ConfigRepository.Instance()["dlcm_PreviewLenght"];
                            //}
                            //previewN = info.OggPath.Substring(info.OggPath.LastIndexOf("\\") + 1, info.OggPath.Substring(pos).Length);
                            cmd += ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "" : " ,") + " AudioPath=\"" + audiopath + ".wem\"";
                            cmd += " , OggPath=\"" + norm_path + path_decom2 + ms.Substring(pos, l) + "_fixed.ogg\" , Song_Lenght=\"" + SongLenght + "\""; //previewN.Replace("_preview","") + "\"";/
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = "2" +cmd+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                        pos = (info.OggPreviewPath.LastIndexOf(".wem"));
                        ms = info.OggPreviewPath;
                        if (ms.Length > 0 && pos > 1 && (info.OggPreviewPath != null))
                        {
                            ms = ms.Substring(0, pos);
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview_preview"));
                            pos = ms.LastIndexOf("\\") + 1;
                            l = ms.Substring(pos).Length;
                            audioprevpath = norm_path + path_decom1 + ms.Substring(pos, l);
                            //var rr = "";//= audioprevpath + "_fixed_preview.wem\"";
                            //if (r)
                            //    rr = audioprevpath + "_fixed_preview.wem\"";
                            //else
                            //    rr = audioprevpath + "_preview.wem\""; 
                            //ogg = audioprevpath + ".ogg";
                            //if (File.Exists(ogg))
                            //    if (PreviewLenght == "")
                            //        using (var vorbis = new NVorbis.VorbisReader(ogg))
                            //        {
                            //            duration = vorbis.TotalTime.ToString();
                            //            if ((duration.Split(':'))[0] == "00" && (duration.Split(':'))[1] == "00")
                            //                PreviewLenght = (duration.Split(':'))[2];// ConfigRepository.Instance()["dlcm_PreviewLenght"];
                            //            else PreviewLenght = PreviewLenght;
                            //            //PreviewLenght.IndexOf(":") > 0 &&  ? (PreviewLenght.Split(':'))[2] :
                            //        }
                            cmd += " , audioPreviewPath=\"" + audioprevpath + ".wem\"";
                            cmd += " , oggPreviewPath=\"" + audioprevpath + ".ogg\" , PreviewLenght=\"" + PreviewLenght + "\"";// previewN + "\"";
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = "3" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        cmd += " , Folder_Name=\"" + norm_path + "\"";

                        cmd += " WHERE ID=" + CDLC_ID;
                        // rtxt_StatisticsOnReadDLCs.Text = "3" + cmd+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                        using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {// 1. If hash already exists do not insert
                            OleDbDataAdapter dgs = new OleDbDataAdapter(cmd, cn);
                            dgs.Fill(dis, "Main");
                            dgs.Dispose();
                            rtxt_StatisticsOnReadDLCs.Text = "Main DB updated after DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        //fix potentially issues with songs with the audio preview WEM  file the same as the original song(file size{no preview})
                        //Move wem to KIT folder + rename
                        //var WemFiles = Directory.GetFiles(unpackedDir, "*.wem", SearchOption.AllDirectories);
                        //if (WemFiles.Count() <= 0)
                        //    throw new InvalidDataException("Audio files not found.");
                        if (info.OggPreviewPath != null)
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1)
                            {
                                try
                                {
                                    File.Move((audiopath + "_preview.wem"), (audiopath + ".wem"));
                                    File.Move((audioprevpath + "_preview.wem"), (audioprevpath + ".wem"));
                                    rtxt_StatisticsOnReadDLCs.Text = "Issues w the WEM filenames when no preview " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                catch (Exception ee)
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = "FAILED1" + ee.Message + "----" + info.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    Console.WriteLine(ee.Message);
                                }
                            }
                        //Fixing any _preview_preview issue..End

                        UpdatePackingLog("LogImporting", DB_Path, packid, CDLC_ID, tst);

                    }
                    //Updating the Standardization table
                    //try
                    //{
                    //cmd = "SELECT * FROM Standardization WHERE StrComp(Artist,\"" + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;";
                    ////rtxt_StatisticsOnReadDLCs.Text = "assesing populating normalization" + cmd + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //DataSet dzs = new DataSet();
                    //OleDbDataAdapter dam = new OleDbDataAdapter(cmd, cnn);
                    //dam.Fill(dzs, "Main");
                    //dam.Dispose();
                    DataSet dzs = new DataSet(); dzs = DLCManager.SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\"" + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;");

                    //rtxt_StatisticsOnReadDLCs.Text = "no of rows returned" + dzs.Tables[0].Rows.Count + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (dzs.Tables[0].Rows.Count == 0)
                    {
                        //cmd = "INSERT INTO Standardization (Artist, Album) VALUES (\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\")";
                        ////rtxt_StatisticsOnReadDLCs.Text = "populating normalization" + cmd + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //DataSet dfs = new DataSet();
                        //OleDbDataAdapter dbm = new OleDbDataAdapter(cmd, cnn);
                        //dbm.Fill(dfs, "Main");
                        //dbm.Dispose();
                        var insertcmdd = "Artist, Album";
                        var insertvalues = "\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\"";
                        DLCManager.InsertIntoDBwValues("Standardization", insertcmdd, insertvalues);
                    }
                    //}
                    //catch (System.IO.FileNotFoundException ee)
                    //{
                    //    rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //    Console.WriteLine(ee.Message);
                    //}
                    rtxt_StatisticsOnReadDLCs.Text = "done" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                    pB_ReadDLCs.Increment(1);
                }
            }
            var ttt = CDLC_ID.ToString();
            string ffg = info.Name;//.ToString();
            string rtr =ttt+ ";" + ffg;
            return rtr;

        }

        //internal static string Manipulate_strings(string v1, int v2, bool v3, bool v4, bool bassRemoved, Files file)
        //{
        //    throw new NotImplementedException();
        //}

        public static string GetTimestamps(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssfff");
        }

        public string GetAlternateNo()
        {
            var a = "";
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //Get the higgest Alternate Number
            //try
            //{
            //    using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            var sel = "";
            sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + data.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + data.SongInfo.Album + "\") AND ";
            sel += "(LCASE(Song_Title)=LCASE(\"" + data.SongInfo.SongDisplayName + "\") OR ";
            sel += "LCASE(Song_Title) like \"%" + data.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
            sel += "LCASE(Song_Title_Sort) =LCASE(\"" + data.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + data.Name + "\");";
            //Get last inserted ID
            //rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //DataSet dds = new DataSet();
            //OleDbDataAdapter dda = new OleDbDataAdapter(sel, con);
            //dda.Fill(dds, "Main");
            //dda.Dispose();
            DataSet dds = new DataSet(); dds = DLCManager.SelectFromDB("Main", sel);
            var altver = dds.Tables[0].Rows[0].ItemArray[0].ToString();
            if (Is_Original == "No") a = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
                                                                            //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            //}
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}

            return a;
        }

        public void Translation_And_Correction(string dbp)
        // Select only Corrected Arstist OR Album OR Cover combination
        // For Each Corrected Record build up an Update sentence
        // Insert any translation if not already existing
        {
            //rtxt_StatisticsOnReadDLCs.Text = "Starting Standartization" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //var cmd2 = "";
            var cmd1 = "";
         //   var cmd2 = "";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
           // var short_c = "";
            var DB_Path = dbp + "\\Files.accdb";
           // var cmd = "";
            int aa = 0;
            pB_ReadDLCs.Value = 0;
            //try
            //{
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //cmd = "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\") OR (AlbumArt_Correction <> \"\") order by id;";
            //    DataSet dus = new DataSet();
            //    OleDbDataAdapter dad = new OleDbDataAdapter(cmd, cnn);
            //    dad.Fill(dus, "Standardization");
            //    dad.Dispose();
            //rtxt_StatisticsOnReadDLCs.Text = "Standardization of " + dus.Tables[0].Rows.Count + " base records\n\n" + rtxt_StatisticsOnReadDLCs.Text;
            DataSet dus = new DataSet(); dus = DLCManager.SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\") OR (AlbumArt_Correction <> \"\") order by id;");
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = norec;
            //rtxt_StatisticsOnReadDLCs.Text = "Repack backgroundworker.."+ norows +  rtxt_StatisticsOnReadDLCs.Text;
            //for (var i = 0; i < norec; i++)
            foreach (DataRow dataRow in dus.Tables[0].Rows)

            {
                artist_c = dataRow.ItemArray[2].ToString();
                album_c = dataRow.ItemArray[4].ToString();
                artpath_c = dataRow.ItemArray[5].ToString();

                cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + (album_c != "" ? "\"," : "\"") : "") + (album_c != "" ? " Album = \"" + album_c + (artpath_c != "" ? "\"," : "\"") : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"" : "");
                cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"";
                dus = DLCManager.UpdateDB("Main", cmd1 + ";");
                ////rtxt_StatisticsOnReadDLCs.Text = dus.Tables[0].Rows[i].ItemArray[0].ToString() +"cmd -" +album_c+"-"+ cmd1 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
                //das.Fill(dus, "Main");
                //das.Dispose();
                pB_ReadDLCs.Increment(1);
            }
            //OleDbDataAdapter dun = new OleDbDataAdapter(cmd2, cnn);
            //dun.Fill(dus, "Main");
            //insert any translation if not already existing
            //cmd2 = "INSERT INTO Standardization (Artist, Album) SELECT DISTINCT (Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            //OleDbDataAdapter dam = new OleDbDataAdapter(cmd2, cnn);
            ////rtxt_StatisticsOnReadDLCs.Text = cmd2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //dam.Fill(dus, "Main");
            var insertcmdd = "Artist, Album";
            var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            DataSet dooz = new DataSet(); dooz = DLCManager.SelectFromDB("Main", insertvalues); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            DLCManager.InsertIntoDBwValues("Standardization", insertcmdd, insertvalues);
            //a = dus.Tables[0].Rows.Count;
            //dam.Dispose();

            var cmd3 = "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)";
            DLCManager.DeleteFromDB("Groups", cmd3); //Cleans out duplicates
            //DataSet dhs = new DataSet();
            //OleDbDataAdapter dcd = new OleDbDataAdapter(cmd3, cnn);
            //dcd.Fill(dhs, "Main");
            //dcd.Dispose();

            //Apply Artist Short Name
            Standardization.ApplyArtistShort();
            //DataSet dfz = new DataSet(); dus = DLCManager.SelectFromDB("Standardization", "SELECT Artist,Artist_Short FROM Standardization WHERE (Artist_Short <> \"\") GROUIP BY Artist,Artist_Short order by id;");
            //norec = dfz.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            //foreach (DataRow dataRow in dfz.Tables[0].Rows)
            //{
            //    artist_c = dataRow.ItemArray[0].ToString();
            //    short_c = dataRow.ItemArray[1].ToString();
            //    cmd1 = "UPDATE Main SET Artist_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\"";
            //    dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            //    cmd1 = "UPDATE Standardization SET Artist_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\"";
            //    dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            //    pB_ReadDLCs.Increment(1);
            //}

            //Apply Album Short Name
            Standardization.ApplyAlbumShort();
            //DataSet dfz = new DataSet(); dus = DLCManager.SelectFromDB("Standardization", "SELECT Artist,Album,Album_Short FROM Standardization WHERE (Album_Short <> \"\") GROUIP BY Artist,Short_Name order by id;");
            //norec = dfz.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            //foreach (DataRow dataRow in dfz.Tables[0].Rows)
            //{
            //    artist_c = dataRow.ItemArray[0].ToString();
            //    album_c= dataRow.ItemArray[1].ToString();
            //    short_c = dataRow.ItemArray[2].ToString();
            //    cmd1 = "UPDATE Main SET Album_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" AND Album=\""+ album_c+"\"";
            //    dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            //    cmd1 = "UPDATE Standardization SET Album_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\"";
            //    dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            //    pB_ReadDLCs.Increment(1);
            //}

            //Apply DefaultCover
            Standardization.MakeCover();//DB_Path);//, txt_AlbumArtPath.Text, txt_Artist.Text, txt_Album.Text); ;
                                        //DataSet dgt = new DataSet(); dus = DLCManager.SelectFromDB("Standardization", "SELECT Artist, Album, AlbumArt_Correction FROM Standardization WHERE (Default_Cover <> \"\") GROUIP BY Artist,Album,Default_Cover order by id;");
                                        //norec = dgt.Tables[0].Rows.Count;
                                        //pB_ReadDLCs.Maximum = norec;
                                        //foreach (DataRow dataRow in dgt.Tables[0].Rows)
                                        //{
                                        //    artist_c = dataRow.ItemArray[0].ToString();
                                        //    album_c = dataRow.ItemArray[1].ToString();
                                        //    artpath_c= dataRow.ItemArray[2].ToString();
                                        //    cmd1 = "UPDATE Main SET AlbumArtPath = \"" + artpath_c + "\" WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"";
                                        //    dus = DLCManager.UpdateDB("Main", cmd1 + ";");

            //}



            DLCManager.DeleteFromDB("Standardization", "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)");
            //}
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    rtxt_StatisticsOnReadDLCs.Text = "Error at standardization" + cmd1 + cmd + cmd2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + aa + ")");
        }

        private static string ReadPackageAuthor(string filePath)
        {
            var info = File.OpenText(filePath);
            string author = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("Package Author:"))
                    author = line.Split(':')[1].Trim();
            }
            info.Close();
            return author;
        }
        private static string ReadPackageToolkitVersion(string filePath)
        {
            var info = File.OpenText(filePath);
            string Toolkit_version = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("Toolkit version:"))
                    Toolkit_version = line.Split(':')[1].Trim();
            }
            info.Close();
            return Toolkit_version;
        }
        private static string ReadPackageOLDToolkitVersion(string filePath)
        {
            var info = File.OpenText(filePath);
            string Toolkit_version = "";
            string line;
            //3 lines
            // MessageBox.Show("test", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            while ((line = info.ReadLine()) != null)
            {
                //MessageBox.Show(line, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //if (line.Contains("Toolkit version:"))
                Toolkit_version = line.Split(':')[0].Trim();
            }
            info.Close();
            return Toolkit_version;
        }

        public string AssessConflict(Files filed, DLCPackageData datas, string Fauthor, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string Vocalss, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string dbpathh, List<string> clist, List<string> dlist, bool newold, string Is_Original, string altver, string fsz, string unpackedDir, string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic, string platform, string IsLive)
        {
            //rtxt_StatisticsOnReadDLCs = chbx_Additional_Manipualtions.SelectedValue + "\n" + rtxt_StatisticsOnReadDLCs;
            //rtxt_StatisticsOnReadDLCs.Text = "dashes: " + art_hash + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //rtxt_StatisticsOnReadDLCs.Text = "dasheD: " + filed.art_Hash + " - " + filed.audio_Hash + " - " + filed.audioPreview_Hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (chbx_Additional_Manipulations.GetItemChecked(13) || (chbx_Additional_Manipulations.GetItemChecked(14) && (tkversion == "" || (tkversion != "" && filed.Is_Original == "Yes"))) || (chbx_Additional_Manipulations.GetItemChecked(56) && ((filed.Is_Multitrack == "Yes" && Is_MultiTrack != "Yes") || (Is_MultiTrack == "Yes" && filed.Is_Multitrack != "Yes"))) || (chbx_Additional_Manipulations.GetItemChecked(66) && ((filed.Is_Live == "Yes" && IsLive != "Yes") || (IsLive == "Yes" && filed.Is_Live != "Yes"))))
                //"14. Import all as Alternates" 15. Import any Custom as Alternate if an Original exists
                return "Alternate";
            else
            {
                //Duplicates frm = new Duplicates(txt_DBFolder.Text, filed, datas, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);
                //frm.Show();
                frm_Duplicates_Management frm1 = new frm_Duplicates_Management(filed, datas, Fauthor, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), clist, dlist, newold, Is_Original, altver, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, FileDate, title_duplic, platform);
                //frm1.Show();
                frm1.ShowDialog();
                //rtxt_StatisticsOnReadDLCs.Text = original_FileName+"-s..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Author != author) author = frm1.Author;
                //rtxt_StatisticsOnReadDLCs.Text = "Setting trasnf vars..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Description != description) description = frm1.Description;
                //rtxt_StatisticsOnReadDLCs.Text = "0" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Comment != comment) comment = frm1.Comment;
                //rtxt_StatisticsOnReadDLCs.Text = "1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Title != SongDisplayName)
                    if (chbx_Additional_Manipulations.GetItemChecked(46)) //SongDisplayName = filed.Song_Title + "[" + frm1.Title.Replace(filed.Song_Title, "") + "]";
                                                                          //    else
                        SongDisplayName = frm1.Title;
                //rtxt_StatisticsOnReadDLCs.Text = "2" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Version != tkversion) PackageVersion = frm1.Version;
                //rtxt_StatisticsOnReadDLCs.Text = "\n" + tkversion+"-----"+ author + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.DLCID != Namee) Namee = frm1.DLCID;
                if (frm1.Is_Alternate != Is_Alternate) Is_Alternate = frm1.Is_Alternate;
                if (frm1.Title_Sort != Title_Sort) Title_Sort = frm1.Title_Sort;
                if (frm1.Artist != Artist) Artist = frm1.Artist;
                if (frm1.ArtistSort != ArtistSort) ArtistSort = frm1.ArtistSort;
                if (frm1.Album != Album) Album = frm1.Album;
                if (frm1.Alternate_No != Alternate_No) Alternate_No = frm1.Alternate_No;
                if (frm1.AlbumArtPath != AlbumArtPath) AlbumArtPath = frm1.AlbumArtPath;
                if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
                if (frm1.MultiT != "") Is_MultiTrack = frm1.MultiT; //MultiT
                if (frm1.MultiTV != "") MultiTrack_Version = frm1.MultiTV; //MultiTV
                                                                           //if (frm1.AlbumArtPath != data.AlbumArtPath) data.AlbumArtPath = frm1.AlbumArtPath;
                if (frm1.YouTube_Link != "") YouTube_Link = frm1.YouTube_Link; //MultiT
                if (frm1.CustomsForge_Link != "") CustomsForge_Link = frm1.CustomsForge_Link; //MultiTV
                if (frm1.CustomsForge_Like != "") CustomsForge_Like = frm1.CustomsForge_Like; //MultiT
                if (frm1.CustomsForge_ReleaseNotes != "") CustomsForge_ReleaseNotes = frm1.CustomsForge_ReleaseNotes; //MultiTV
                if (frm1.dupliID != "") dupliID = frm1.dupliID; //MultiTV
                if (frm1.ExistingTrackNo != "") ExistingTrackNo = frm1.ExistingTrackNo; //MultiTV
                IgnoreRest = false;
                if (frm1.IgnoreRest) IgnoreRest = frm1.IgnoreRest;
                //      Is_Alternate
                //      Title_Sort
                //      Album
                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //frm1.BringToFront();
                //frm1.Focus();
                //this.Enabled = false;
                ////Standardization frm = new Standardization(txt_DBFolder.Text);
                ////frm.Show();
                ////MessageBox.Show("test", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //string text = "Same Current -> Existing " + (i + 2) + "/" + (norows + 1) + " " + filed.Artist + "-" + filed.Album + "\n";
                //text += ((datas.SongInfo.SongDisplayName == filed.Song_Title) ? "" : ("\n1/14+ Song Titles: " + datas.SongInfo.SongDisplayName + "->" + filed.Song_Title));
                //text += ((datas.SongInfo.SongDisplayNameSort == filed.Song_Title_Sort) ? "" : ("\n2/14+ Song Sort Titles: " + datas.SongInfo.SongDisplayNameSort + "->" + filed.Song_Title_Sort));
                //text += ((original_FileName == filed.Original_FileName) ? "" : ("\n3/14+ FileNames: " + original_FileName + " - " + filed.Original_FileName));
                //text += ((((tkversion == "") ? "Yes" : "No") == filed.Is_Original) ? "" : ("\n4/14+ Is Original: " + ((tkversion == "") ? "Yes" : "No") + " -> " + filed.Is_Original));
                //text += ((tkversion == filed.ToolkitVersion) ? "" : ("\n5/14+ Toolkit version: " + tkversion + " -> " + filed.ToolkitVersion));
                //text += ("\n6/14+ Author: " + author + " -> " + filed.Author); //((author == filed.Author) ? "" :
                //text += ((tunnings == filed.Tunning) ? "" : ("\n7/14+ Tunning: " + tunnings + " -> " + filed.Tunning));
                //text += ((datas.PackageVersion == filed.Version) ? "" : ("\n8/14+ Version: " + datas.PackageVersion + " -> " + filed.Version));
                //text += ((datas.Name == filed.DLC_Name) ? "" : ("\n9/14+ DLC ID: " + datas.Name + " -> " + filed.DLC_Name));
                //text += ((DD == filed.Has_DD) ? "" : ("\n10/14+ DD: " + DD + " -> " + filed.Has_DD));
                ////text += "\nOriginal Is Alternate: " + filed.Is_Alternate + (filed.Is_Alternate == "Yes" ? " v. " + filed.Alternate_Version_No : "");
                //text += "\n11/14+ Avail. Instr./Tracks: " + ((Bass == "Yes") ? "B" : "") + ((Rhythm == "Yes") ? "R" : "") + ((Lead == "Yes") ? "L" : "") + ((Combo == "Yes") ? "C" : ""); //((Guitar == "Yes") ? "G" : "") + 
                //text += " -> " + ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : ""); //+ ((filed.Has_Guitar == "Yes") ? "G" : "")
                //text += ((filed.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + filed.art_Hash
                //text += ((filed.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + filed.audio_Hash 
                //text += ((filed.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + filed.audioPreview_Hash

                ////files hash
                //DataSet ds = new DataSet();
                //i = 0;
                //var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
                //string jsonHash = "";
                //bool diffjson = true;
                //string XmlHash = "";
                //var XmlName = "";
                //var XmlUUID = "";
                //var XmlFile = "";
                //var jsonFile = "";
                //bool diff = true;
                //int k = 0;
                //string lastConversionDateTime_cur = "";
                //string lastConversionDateTime_exist = "";
                //string lastConverjsonDateTime_cur = "";
                //string lastConverjsonDateTime_exist = "";
                ////MessageBox.Show(DB_Path);
                //try
                //{
                //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                //    {
                //        OleDbDataAdapter daa = new OleDbDataAdapter("SELECT * FROM Arrangements WHERE CDLC_ID=\"" + filed.ID.ToString() + "\";", cnn);
                //        //as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                //        //MessageBox.Show("0");
                //        daa.Fill(ds, "Arrangements");
                //        var noOfRec = 0;
                //        //MessageBox.Show("0.1");
                //        noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                //        rtxt_StatisticsOnReadDLCs.Text = noOfRec + "Assesment Arrangement hash file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        //MessageBox.Show("1");
                //        if (noOfRec > 0)
                //        {
                //            //MessageBox.Show("1");
                //            foreach (var arg in datas.Arrangements)
                //            {
                //                diff = true; diffjson = true;
                //                lastConversionDateTime_cur = "";
                //                lastConversionDateTime_exist = "";
                //                for (i = 0; i <= noOfRec - 1; i++)
                //                {
                //                    //MessageBox.Show(noOfRec.ToString());
                //                    //rtxt_StatisticsOnReadDLCs.Text = alist[i]+"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    XmlHash = ds.Tables[0].Rows[i].ItemArray[6].ToString(); // XmlHash                                  
                //                    XmlName = ds.Tables[0].Rows[i].ItemArray[17].ToString() + ds.Tables[0].Rows[i].ItemArray[25].ToString(); //type+routemask+
                //                    XmlUUID = ds.Tables[0].Rows[i].ItemArray[28].ToString(); //xml.uuid
                //                    XmlFile = ds.Tables[0].Rows[i].ItemArray[5].ToString(); //xml.filepath
                //                    jsonFile = ds.Tables[0].Rows[i].ItemArray[4].ToString(); //json.filepath
                //                    jsonHash = ds.Tables[0].Rows[i].ItemArray[38].ToString(); // XmlHash      
                //                    arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json")).Replace("EOF", "Toolkit");
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-"+XmlName + "=" + (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongXml.File + "=" + XmlFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongFile.File + "==" + jsonFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    if (XmlName == (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) || (XmlUUID == arg.SongXml.UUID.ToString()))
                //                    // rtxt_StatisticsOnReadDLCs.Text = "-" + XmlHash + "=" + alist[k] + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    {
                //                        if (XmlHash == alist[k])
                //                            diff = false;
                //                        else
                //                        {
                //                            lastConversionDateTime_cur = GetTExtFromFile(arg.SongXml.File);
                //                            lastConversionDateTime_exist = GetTExtFromFile(XmlFile);
                //                        }
                //                        if (jsonHash == blist[k])
                //                            diffjson = false;
                //                        else
                //                        {
                //                            lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File);
                //                            lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile);
                //                        }
                //                    }
                //                }
                //                text += ((diff) ? "\n" + (14 + i) + "/14+Diff XML" + arg.ArrangementType + arg.RouteMask + ": " + lastConversionDateTime_cur + "->" + lastConversionDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                //                text += ((diffjson) ? "\n" + (15 + i) + "/14+Diff Json" + arg.ArrangementType + arg.RouteMask + ": " + lastConverjsonDateTime_cur + "->" + lastConverjsonDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                //                k++;
                //            }

                //        }
                //    }
                //}
                //catch (System.IO.FileNotFoundException ee)                
                //{
                //    rtxt_StatisticsOnReadDLCs.Text = "Error at last conversion" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    Console.WriteLine(ee.Message);
                //}

                ////files size//files dates
                //DialogResult result1 = MessageBox.Show(text + "\n\nChose:\n\n1. Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //if (result1 == DialogResult.Yes) return "Update";
                //else if (result1 == DialogResult.No) return "Alternate";
                //else return "ignore";//if (result1 == DialogResult.Cancel) 
                //return "Alternate";
                rtxt_StatisticsOnReadDLCs.Text = "REturing from child.." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var tst = "Ignore";
                if (frm1.Asses != "") tst = frm1.Asses;
                frm1.Dispose();
                rtxt_StatisticsOnReadDLCs.Text = "REturing.. to import" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                return tst;

            }
        }

        public string GetTExtFromFile(string ss)
        {

            var info = File.OpenText(ss);
            string tecst = "";
            string line;
            //3 lines
            //MessageBox.Show("test", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            while ((line = info.ReadLine()) != null)
            {
                //       rtxt_StatisticsOnReadDLCs.Text = "-" + rtxt_StatisticsOnReadDLCs.Text;
                //MessageBox.Show(line, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (line.Contains("astConversionDateTime"))
                {
                    //rtxt_StatisticsOnReadDLCs.Text = "+" + line + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    tecst = (line.Replace("<lastConversionDateTime>", "")).Replace("</lastConversionDateTime>", "");
                    if (tecst == line) tecst = ((line.Replace("\"", "")).Replace("LastConversionDateTime: ", "")).Replace(",", "");
                    break;
                }

            }
            info.Close();
            return tecst;
        }

        public void btn_RePack_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            if (btn_RePack.Text == "Stop Repack")
            {
                btn_RePack.Text = "RePack";
                if (bwRGenerate.WorkerSupportsCancellation == true)
                {

                    // Cancel the asynchronous operation.
                    bwRGenerate.CancelAsync();
                }
            }
            else
            {
                var Temp_Path_Import = txt_TempPath.Text;
                var old_Path_Import = txt_TempPath.Text + "\\0_old";
                var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
                var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
                var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
                var repacked_Path = txt_TempPath.Text + "\\0_repacked";
                var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
                var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
                var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
                var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
                var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
                string pathDLC = txt_RocksmithDLCPath.Text;
                CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);



                btn_RePack.Text = "Stop Repack";
                if ((ConfigRepository.Instance()["general_defaultauthor"] == "" || ConfigRepository.Instance()["general_defaultauthor"] == "Custom Song Creator") && chbx_DebugB.Checked) ConfigRepository.Instance()["general_defaultauthor"] = "catara";

                //if (Path.GetFileName(dlcSavePath).Contains(" ") && rbtn_PS3.Checked)
                //    if (!ConfigRepository.Instance().GetBoolean("creator_ps3pkgnamewarn"))
                //    {
                //        MessageBox.Show(String.Format("PS3 package name can't support space character due to encryption limitation. {0} Spaces will be automatic removed for your PS3 package name.", Environment.NewLine), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //    else
                //    {
                //        ConfigRepository.Instance()["creator_ps3pkgnamewarn"] = true.ToString();
                //    }
                rtxt_StatisticsOnReadDLCs.Text = "Packing: " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                Groupss = cbx_Groups.Text.ToString();

                var cmd = "SELECT * FROM Main ";
                if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
                //else if (rbtn_Population_All.Checked) ;
                else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")"; //cbx_Groups.SelectedText 

                cmd += " ORDER BY Artist";
                //Read from DB
                var norows = 0;
                norows = SQLAccess(cmd);
                //bcapirtxt_StatisticsOnReadDLCs.Text = "Processing &Repackaging for " + norows + " " + cmd + "\n \n" + rtxt_StatisticsOnReadDLCs.Text;
                pB_ReadDLCs.Maximum = norows * 100;

                if (!bwRGenerate.IsBusy) //&& data != null&& norows > 0
                {
                    bwRGenerate.RunWorkerAsync(data);
                    //rtxt_StatisticsOnReadDLCs.Text = " not buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                else
                {
                    //bcapirtxt_StatisticsOnReadDLCs.Text = " Buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "Repack done"+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }
        }

        public void GeneratePackage(object sender, DoWorkEventArgs e)
        {
            var cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            //else if (rbtn_Population_All.Checked) ;
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")"; ;

            string copyftp = "";
            cmd += " ORDER BY Artist";
            //Read from DB
            var norows = 0;
            norows = SQLAccess(cmd);
            //bcapirtxt_StatisticsOnReadDLCs.Text = "Processing &Repackaging for " + norows + " " + cmd + "\n \n" + rtxt_StatisticsOnReadDLCs.Text;
            //pB_ReadDLCs.Maximum = norows;

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);

            var i = 0;
            var artist = "";
            //var cmd = "";
            //rtxt_StatisticsOnReadDLCs.Text = "Repack backgroundworker.."+ norows +  rtxt_StatisticsOnReadDLCs.Text;
            foreach (var file in files)
            {
                if (i == norows)
                    break;
                //pB_ReadDLCs.Value++;
                //bcapirtxt_StatisticsOnReadDLCs.Text = "...Pack" + i + " " + file.Artist + " " + file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;// UNPACK
                if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Additional_Manipulations.GetItemChecked(7))) //"8. Don't repack Broken songs")
                {
                    //var unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                    //MessageBox.Show(file.Artist+file.Song_Title);
                    var packagePlatform = file.Folder_Name.GetPlatform();
                    // REORGANIZE
                    //rtxt_StatisticsOnReadDLCs.Text = "...0.1.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                    //if (structured)
                    //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);
                    // LOAD DATA
                    //rtxt_StatisticsOnReadDLCs.Text = "...0.5.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);
                    //rtxt_StatisticsOnReadDLCs.Text = "...1.."+ file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    var bassRemoved = "No";
                    var DDAdded = "No";
                    if (chbx_Additional_Manipulations.GetItemChecked(63) && file.FilesMissingIssues.IndexOf("psarc") > 0)
                    {
                        if (file.Platform == "PS3")
                        {
                            var FTPPath = "";
                            if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                            else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                            MainDB.DeleteFTPFiles(file.FilesMissingIssues, FTPPath);
                        }
                        else
                        {
                            try
                            {
                                if (!File.Exists(txt_RocksmithDLCPath.Text + "\"" + file.FilesMissingIssues))
                                    File.Move(txt_RocksmithDLCPath.Text + "\"" + file.FilesMissingIssues, (txt_RocksmithDLCPath.Text + "\"" + file.FilesMissingIssues).Replace(".psarc", ".dupli"));
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex);
                            }

                        }
                    }
                    var h = "";
                    // Check and Get last Packed
                    //var tht = "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\" AND DLC_ID=" +  file.ID + " ORDER BY ID DESC;";
                    //DataSet dvr = new DataSet(); dvr = DLCManager.SelectFromDB("Pack_AuditTrail", tht);
                    //var recs = dvr.Tables[0].Rows.Count;
                    if (chbx_Additional_Manipulations.GetItemChecked(64))
                    {
                        DataSet dbr = new DataSet(); dbr = DLCManager.SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\" and DLC_ID=" + file.ID + " ORDER BY ID DESC;");
                        var rec = dbr.Tables[0].Rows.Count;
                        if (rec > 0) h = dbr.Tables[0].Rows[0].ItemArray[0].ToString();
                        //  else chbx_Last_Packed.Enabled = false;
                    }
                    else
           if (chbx_Additional_Manipulations.GetItemChecked(65) && file.Available_Old=="Yes")
                    {

                        var oldfilePath = txt_TempPath.Text + "\\0_old\\" + file.Original_FileName;//Original_FileName
                        if (oldfilePath.GetPlatform().platform.ToString() == (chbx_PC.Checked ? "Pc" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "Ps3" : "XBOX360"))
                        {
                            h = oldfilePath;
                        }
                        else
                        {
                            var SourcePlatform = new Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
                         //   var convdone = "beginn";
                            var TargetPlatform = new Platform((chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360"), GameVersion.RS2014.ToString());


                            // h = DLCPackageConverter.Convert(oldfilePath, SourcePlatform, TargetPlatform, "248750");

                            var needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
                            var tmpDir = Path.GetTempPath();

                            var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, false, false, SourcePlatform);

                            // DESTINATION
                            var nameTemplate = (!TargetPlatform.IsConsole) ? "{0}{1}.psarc" : "{0}{1}";
                            Random random = new Random();
                            int packrid = random.Next(0, 100000);
                            var packageName = Path.GetFileNameWithoutExtension(oldfilePath).StripPlatformEndName();
                            if (chbx_Additional_Manipulations.GetItemChecked(2)) packageName += packrid;
                            packageName = packageName.Replace(".", "_");
                            var targetFileName = String.Format(nameTemplate, Path.Combine(Path.GetDirectoryName(oldfilePath), packageName), TargetPlatform.GetPathName()[2]);

                            // CONVERSION
                            if (needRebuildPackage)
                            {
                                // RocksmithToolkitLib.DLCPackage.DLCPackageConverter.ConvertPackageRebuilding(unpackedDir, targetFileName, SourcePlatform, TargetPlatform, "248750");
                                data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
                                // Update AppID
                                if (!TargetPlatform.IsConsole)
                                    data.AppId = "248750";

                                // Build
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(targetFileName, data, new Platform(TargetPlatform.platform, GameVersion.RS2014));
                            }
                            else
                            {// DLCPackageConverter.ConvertPackageForSimilarPlatform(unpackedDir, targetFileName, SourcePlatform, TargetPlatform, "248750");
                             // Old and new paths
                                var sourceDir0 = SourcePlatform.GetPathName()[0].ToLower();
                                var sourceDir1 = SourcePlatform.GetPathName()[1].ToLower();
                                var targetDir0 = TargetPlatform.GetPathName()[0].ToLower();
                                var targetDir1 = TargetPlatform.GetPathName()[1].ToLower();

                                if (!TargetPlatform.IsConsole)
                                {
                                    // Replace AppId
                                    var appIdFile = Path.Combine(unpackedDir, "appid.appid");
                                    File.WriteAllText(appIdFile, "248750");
                                }

                                // Replace aggregate graph values
                                var aggregateFile = Directory.EnumerateFiles(unpackedDir, "*.nt", SearchOption.AllDirectories).FirstOrDefault();
                                var aggregateGraphText = File.ReadAllText(aggregateFile);
                                // Tags
                                aggregateGraphText = Regex.Replace(aggregateGraphText, GraphItem.GetPlatformTagDescription(SourcePlatform.platform), GraphItem.GetPlatformTagDescription(TargetPlatform.platform), RegexOptions.Multiline);
                                // Paths
                                aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir0, targetDir0, RegexOptions.Multiline);
                                aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir1, targetDir1, RegexOptions.Multiline);
                                File.WriteAllText(aggregateFile, aggregateGraphText);

                                // Rename directories
                                foreach (var dir in Directory.GetDirectories(unpackedDir, "*.*", SearchOption.AllDirectories))
                                {
                                    if (dir.EndsWith(sourceDir0))
                                    {
                                        var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir0)) + targetDir0;
                                        DirectoryExtension.SafeDelete(newDir);
                                        DirectoryExtension.Move(dir, newDir);
                                    }
                                    else if (dir.EndsWith(sourceDir1))
                                    {
                                        var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
                                        DirectoryExtension.SafeDelete(newDir);
                                        DirectoryExtension.Move(dir, newDir);
                                    }
                                }

                                // Recreates SNG because SNG have different keys in PC and Mac
                                bool updateSNG = ((SourcePlatform.platform == GamePlatform.Pc && TargetPlatform.platform == GamePlatform.Mac) || (SourcePlatform.platform == GamePlatform.Mac && TargetPlatform.platform == GamePlatform.Pc));

                                // Packing
                                var dirToPack = unpackedDir;
                                if (SourcePlatform.platform == GamePlatform.XBox360)
                                    dirToPack = Directory.GetDirectories(Path.Combine(unpackedDir, Packer.ROOT_XBox360))[0];

                                Packer.Pack(dirToPack, targetFileName, updateSNG, TargetPlatform, fixShowlights: false);
                                DirectoryExtension.SafeDelete(unpackedDir);
                            }
                            //DirectoryExtension.SafeDelete(unpackedDir);

                            var s = txt_TempPath.Text + "\\0_old\\"; //+ Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString())); ;
                            h = txt_TempPath.Text + "\\0_repacked\\" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\\"; //+ Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString()));
                            h += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));
                            s += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));// targetFileName;//s.Substring(0, s.LastIndexOf("_")) + (chbx_Format.Text == "PC" ? "_p.psarc" : (chbx_Format.Text == "Mac" ? "_m .psarc" : (chbx_Format.Text == "PS3" ? "_ps3.psarc.edat" : "")));

                            if (File.Exists(h)) { File.Delete(h); File.Move(s, h); }
                            else File.Copy(s, h, true);
                            // h = d;
                            // h = TempPath + "\\0_repacked\\" + (txt_Platform.Text == "Pc" ? "PC" : txt_Platform.Text == "Mac" ? "MAC" : txt_Platform.Text == "Ps3" ? "PS3" : "XBOX360") +DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString();
                            // h = txt_Platform.Text=="Pc"? oldfilePath.Replace("_p.psarc", ""): txt_Platform.Text == "Mac" ? oldfilePath.Replace("_m .psarc", "") : txt_Platform.Text == "Ps3" ? oldfilePath.Replace("_ps3.psarc.edat", "") :oldfilePath;

                            //Generating the HASH code
                            var FileHash = "";
                            //var fpath = h.Replace(txt_RocksmithDLCPath.Text, old_Path_Import);
                            FileStream fs;
                            using (fs = File.OpenRead(h))
                            {
                                SHA1 sha = new SHA1Managed();
                                FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                                //fs.Close();

                                System.IO.FileInfo fi = null; //calc file size
                                try { fi = new System.IO.FileInfo(h); }
                                catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

                                var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                var fnnon = Path.GetFileName(h);
                                var packn = h.Substring(0, h.IndexOf(fnnon));
                                var insertA = "\"" + h + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + h.GetPlatform().platform.ToString() + "\"";
                                //var insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, \"" + imported.Split(';')[0] + "\" as DLC_ID, \"" + imported.Split(';')[1] + "\" as DLC_Name, \"" + fpath.GetPlatform().platform.ToString() + "\" as Platform FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                DLCManager.InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                                fs.Close();
                            }

                            //var t = oldfilePath.Length;
                            //    var u = oldfilePath.IndexOf(Path.G);
                            //var f = (Path.GetFileNameWithoutExtension(oldfilePath)).Length;
                            //var g = Path.GetFileNameWithoutExtension(oldfilePath);
                            //    h = oldfilePath.Substring(0,oldfilePath.Length - oldfilePath.IndexOf(Path.GetFileNameWithoutExtension(oldfilePath))+ (Path.GetFileNameWithoutExtension(oldfilePath)).Length);
                            //bwConvert.DoWork += doConvert;
                            //if (!bwConvert.IsBusy && oldfilePath.Length > 0)
                            //{
                            //    pB_ReadDLCs.Value = 0;
                            //    //pbUpdateProgress.Visible = true;
                            //    //lblCurrentOperation.Visible = true;
                            //    //ToggleUIControls(false);
                            //    bwConvert.RunWorkerAsync(oldfilePath);
                            //}
                            ////  bwConvert.DoWork += doConvert;

                            //    bwConvert.ProgressChanged += (se, ea) =>
                            //    {
                            //        if (ea.ProgressPercentage <= pB_ReadDLCs.Maximum)
                            //            pB_ReadDLCs.Value = ea.ProgressPercentage;
                            //        else
                            //            pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;
                            //    //ShowCurrentOperation(ea.UserState as string);
                            //};
                            //    bwConvert.RunWorkerCompleted += ProcessCompleted;
                            //for (int zz = 0; convdone != "done"; zz++) ;
                            //; ; ;
                            //; ; ;
                            //; ; ;
                            //; ; ;
                        }
                    }
                    else
                    {

                        var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", SearchOption.AllDirectories);
                        var platform = file.Folder_Name.GetPlatform();
                        if (chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                        {
                            foreach (var xml in xmlFiles)
                            {
                                Song2014 xmlContent = null;
                                try
                                {
                                    xmlContent = Song2014.LoadFromFile(xml);
                                    if (chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                                        //ADD DD
                                        if (
                                            (false && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                            && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm") && file.Has_DD == "No") || (xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "No")
                                            )
                                            || //chbx_Additional_Manipualtions.GetItemChecked(26)
                                            (false && (xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm")
                                            && file.Has_DD == "No" && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                            )
                                           )
                                        {
                                            File.Copy(xml, xml + ".woDD", true);
                                            DDAdded = (AddDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31), "5") == "Yes") ? "No" : "Yes";
                                            file.Has_BassDD = (DDAdded == "Yes") ? "Yes" : "No";
                                        }

                                    //REMOVE DD
                                    //rtxt_StatisticsOnReadDLCs.Text = "...=.." + xml + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    //var aa = xml.IndexOf("showlights");
                                    //if (aa<1) && (xml.IndexOf("showlights") <1)
                                    if (file.Has_BassDD == "Yes")
                                        if ((!(chbx_Additional_Manipulations.GetItemChecked(52) && file.Keep_BassDD == "Yes") && xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(5))
                                            || (!(chbx_Additional_Manipulations.GetItemChecked(53) && file.Keep_DD == "Yes") && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm"))
                                              && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(3))
                                           )
                                        // continue;
                                        {
                                            if (chbx_Additional_Manipulations.GetItemChecked(5) && !chbx_Additional_Manipulations.GetItemChecked(3) && !(xmlContent.Arrangement.ToLower() == "bass")) continue;
                                            //File.Copy(xml, xml + ".old", true);
                                            bassRemoved = (RemoveDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31)) == "Yes") ? "Yes" : "No";
                                            file.Has_BassDD = (bassRemoved == "Yes") ? "No" : "Yes";
                                        }
                                }
                                catch (Exception ee)
                                {
                                    Console.Write(ee.Message);
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "...°.." + xmlFiles.Length + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }

                        //Default APP ID
                        if (chbx_Additional_Manipulations.GetItemChecked(43)) file.DLC_AppID = ConfigRepository.Instance()["general_defaultappid_RS2014"];


                        //get track no
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") //59. @Pack try to get Track No again (&don't save)                        
                        {
                            string z = (MainDB.GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle(info.SongInfo.SongDisplayName))).ToString();
                            file.Track_No = z == "0" && file.Track_No != "" ? file.Track_No : z;
                            //
                            //
                            // Multithreading and DB access nnot supported
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes" && file.Track_No != "0" && file.Track_No != "-1" && file.Track_No != "") //60.@Pack try to get Track No, again(&save)
                            {
                                UpdateDB("Main", "UPDATE Main SET Track_No=\"" + file.Track_No + "\" WHERE ID=" + file.ID + ";");
                                // var cnc = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txt_DBFolder.Text); //+ ";Persist Security Info=False"
                                //var command = cnc.CreateCommand();
                                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                                //{

                                //command.CommandText = "UPDATE Main SET ";
                                //command.CommandText += "Track_No = @param8 ";
                                //command.Parameters.AddWithValue("@param8", file.Track_No);
                                //command.CommandText += " WHERE ID IN (" + file.ID+ ")";

                                //try
                                //{
                                //    command.CommandType = CommandType.Text;
                                //    cnc.Open();
                                //    command.ExecuteNonQuery();
                                //    cnc.Close();
                                //    command.Dispose();
                                //}
                                //catch (Exception ex)
                                //{
                                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    //MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                                //    //throw;
                                //}
                                //finally
                                //{
                                //    //if (cnn != null) cnn.Close();
                                //}
                                //using (OleDbConnection cnn2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txt_DBFolder.Text))
                                //{
                                //var sel2 = "Update Main Set Track_No = '" + file.Track_No + "' WHERE ID=" + file.ID + ";";
                                //rtxt_StatisticsOnReadDLCs.Text = max.ToString()+"-"+sel2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //DataSet dxr = new DataSet(); if (file.Track_No.ToString() != "") dxr = DLCManager.UpdateDB("Main", "Update Main Set Track_No = '" + file.Track_No + "' WHERE ID=" + file.ID + ";");
                                //try
                                //{
                                //    OleDbDataAdapter dax = new OleDbDataAdapter(sel2, cnn2);
                                //    if (file.Track_No.ToString() != "")
                                //        // dax.Fill(dxr, "Main");
                                //        dax.Dispose();
                                //}
                                //catch (Exception ex)
                                //{
                                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    //MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                                //}
                                //}
                            }
                        }

                        //Gather song Lenght
                        var duration = "";
                        var recalc_Preview = false;
                        if (file.oggPreviewPath != null && file.oggPreviewPath != "")
                            if (chbx_Additional_Manipulations.GetItemChecked(55))
                            {
                                using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
                                {
                                    duration = vorbis.TotalTime.ToString();
                                }
                                string[] timepieces = duration.Split(':');
                                if (timepieces[0] != "00" || timepieces[1] != "00")
                                    recalc_Preview = true;//&& timepieces[2].ToInt32() > file.PreviewLenght.ToInt32()) ;
                            }
                        //Set Preview
                        if (chbx_Additional_Manipulations.GetItemChecked(9) && file.oggPreviewPath == null || (chbx_Additional_Manipulations.GetItemChecked(55) && (file.AudioPreview_Hash == file.Audio_Hash || file.Song_Lenght == file.PreviewLenght || recalc_Preview)))
                        {
                            //delete old previews!
                            if (file.oggPreviewPath != null) File.Delete(file.oggPreviewPath);
                            if (file.oggPreviewPath != null) File.Delete(file.audioPreviewPath);

                            //rtxt_StatisticsOnReadDLCs.Text = "Trying to add preview as missing.\n" + rtxt_StatisticsOnReadDLCs.Text;
                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
                            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
                            var t = file.OggPath; ;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
                            var tt = t.Replace(".ogg", "_preview.ogg");
                            var times = ConfigRepository.Instance()["dlcm_PreviewStart"];
                            string[] timepieces = times.Split(':');
                            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                            startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                                t,
                                                                tt,
                                                                r.TotalMilliseconds,
                                                                (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            if (File.Exists(t))
                                using (var DDC = new Process())
                                {
                                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                    if (DDC.ExitCode == 0)
                                    {
                                        file.oggPreviewPath = tt;
                                        MainDB.Converters(tt, MainDB.ConverterTypes.Ogg2Wem, false);
                                        file.audioPreviewPath = tt.Replace(".ogg", ".wem");
                                    }
                                }
                            //save new previews
                            cmd = "UPDATE Main SET ";
                            //DataSet dis = new DataSet();
                            if (File.Exists(file.oggPreviewPath))
                                if (PreviewLenght == "" || PreviewLenght == null)
                                    using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
                                    {
                                        var durations = vorbis.TotalTime;
                                        PreviewLenght = durations.ToString();// ConfigRepository.Instance()["dlcm_PreviewLenght"];
                                    }
                            var audioPreview_hash = "";
                            if (File.Exists(file.audioPreviewPath))
                                using (FileStream fs = File.OpenRead(file.audioPreviewPath))
                                {
                                    SHA1 sha = new SHA1Managed();
                                    audioPreview_hash = BitConverter.ToString(sha.ComputeHash(fs));
                                    fs.Close();
                                }
                            cmd += " audioPreviewPath=\"" + file.audioPreviewPath + "\"" + " , audioPreview_Hash=\"" + audioPreview_hash + "\"" + " , PreviewTime=\"" + times + "\"";
                            cmd += " , oggPreviewPath=\"" + file.oggPreviewPath + "\" , PreviewLenght=\"" + (PreviewLenght.IndexOf(":") > 0 ? (PreviewLenght.Split(':'))[2] : PreviewLenght) + "\"";// previewN + "\"";

                            cmd += " WHERE ID=" + file.ID;
                            DataSet dis = new DataSet(); dis = DLCManager.UpdateDB("Main", cmd + ";");
                            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
                            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                            //{// 1. If hash already exists do not insert
                            //    OleDbDataAdapter dgs = new OleDbDataAdapter(cmd, cn);
                            //    dgs.Fill(dis, "Main");
                            //    dgs.Dispose();
                            //    //rtxt_StatisticsOnReadDLCs.Text = "Main DB updated after DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                            //}
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(17)) //18.Repack with Artist/ Title same as Artist / Title Sort
                        {
                            file.Artist_Sort = file.Artist;
                            file.Song_Title_Sort = file.Song_Title;
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(10))
                        {
                            Random random = new Random();
                            string apppid = random.Next(0, 100000) + file.DLC_Name;
                            file.DLC_Name = apppid;
                        }

                        //rtxt_StatisticsOnReadDLCs.Text = "ggggoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) //24.Pack with The/ Die only at the end of Title Sort 
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "1eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4)
                            {
                                //rtxt_StatisticsOnReadDLCs.Text = "2eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "The " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "Die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "the " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "THE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                                file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "DIE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                            }
                            //rtxt_StatisticsOnReadDLCs.Text = file.Artist_Sort + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "The " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "Die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "the " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "THE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                            file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "DIE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = "4eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        var toolkitv = "";
                        if (chbx_Additional_Manipulations.GetItemChecked(47)) toolkitv = ToolkitVersion.version.ToString();
                        else toolkitv = file.Version;
                        data = new DLCPackageData
                        {
                            GameVersion = GameVersion.RS2014,
                            Pc = false,// chbx_PC.Checked, //txt_Platform.Text 
                            Mac = false,//chbx_Mac.Text == "Mac" || file.Platform == "Mac" ? true : false,
                            XBox360 = false,//chbx_XBOX360.Text == "XBOX360" || file.Platform == "XBox360" ? true : false,
                            PS3 = false,//chbx_PS3.Text == "PS3" || file.Platform == "Ps3" ? true : false,
                            Name = file.DLC_Name,
                            AppId = file.DLC_AppID,
                            ArtFiles = info.ArtFiles,
                            Showlights = true,//info.Showlights, //apparently this infor is not read..also the tone base is removed/not read also
                            Inlay = info.Inlay,
                            LyricArtPath = info.LyricArtPath,

                            //USEFUL CMDs String.IsNullOrEmpty(
                            SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
                            {
                                SongDisplayName = file.Song_Title,
                                SongDisplayNameSort = file.Song_Title_Sort,
                                Album = file.Album,
                                SongYear = file.Album_Year.ToInt32(),
                                Artist = file.Artist,
                                ArtistSort = file.Artist_Sort,
                                AverageTempo = file.AverageTempo.ToInt32()
                            },

                            AlbumArtPath = file.AlbumArtPath,
                            OggPath = file.AudioPath,
                            OggPreviewPath = ((file.audioPreviewPath != "") ? file.audioPreviewPath : file.AudioPath),
                            Arrangements = info.Arrangements, //Not yet done
                            Tones = info.Tones,//Not yet done
                            TonesRS2014 = info.TonesRS2014,//Not yet done
                            Volume = Convert.ToSingle(file.Volume),
                            PreviewVolume = Convert.ToSingle(file.Preview_Volume),
                            SignatureType = info.SignatureType,
                            PackageVersion = toolkitv//file.Version                    
                        };
                        //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+" test"+i+ data.SongInfo.Artist + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        var rrt = ConfigRepository.Instance()["general_defaultauthor"];
                        if ((file.Author == "Custom Song Creator" || file.Author == "") && rrt != "Custom Song Creator" && chbx_Additional_Manipulations.GetItemChecked(47))
                            file.Author = "RepackedBy" + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
                        if (chbx_Additional_Manipulations.GetItemChecked(54))
                            file.Is_Beta = "Yes";

                        var norm_path = txt_TempPath.Text + "\\0_repacked\\" + ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                        //rtxt_StatisticsOnReadDLCs.Text = "8"+data.PackageVersion+"...manipul" + norm_path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //manipulating the info
                        if (cbx_Activ_Title.Checked)
                            data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        // rtxt_StatisticsOnReadDLCs.Text = "...manipul: "+ file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (cbx_Activ_Title_Sort.Checked)
                            data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (cbx_Activ_Artist.Checked)
                            data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (cbx_Activ_Artist_Sort.Checked)
                            data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (cbx_Activ_Album.Checked)
                            data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        //rtxt_StatisticsOnReadDLCs.Text = "...3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                        //rtxt_StatisticsOnReadDLCs.Text = "...nipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (chbx_Additional_Manipulations.GetItemChecked(0)) //"1. Add Increment to all Titles"
                            data.Name = i + data.Name;

                        //rtxt_StatisticsOnReadDLCs.Text = "...mpl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        artist = "";
                        if (chbx_Additional_Manipulations.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
                        {
                            if (i > 0)
                                if (data.SongInfo.Artist == files[i - 1].Artist) no_ord += 1;
                                else no_ord = 1;
                            else no_ord += 1;
                            artist = no_ord + " ";
                            data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;
                        }

                        //if (chbx_Additional_Manipualtions.GetItemChecked(7)) //"8. Don't repack Broken songs"
                        //    if (file.Is_Broken == "Yes") break;
                        //rtxt_StatisticsOnReadDLCs.Text = "...4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //rtxt_StatisticsOnReadDLCs.Text = "...manipl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (chbx_Additional_Manipulations.GetItemChecked(2))
                            //"3. Make all DLC IDs unique (&save)"
                            if (file.UniqueDLCName != null && file.UniqueDLCName != "") data.Name = file.UniqueDLCName;
                            else
                            {
                                Random random = new Random();
                                data.Name = random.Next(0, 100000) + data.Name;
                                //Commenting out as Not used
                                // DataSet dis = new DataSet(); dis = DLCManager.UpdateDB("Main", "UPDATE Main SET UniqueDLCName =\"" + data.Name + "\" WHERE ID=" + file.ID + ";");

                                //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                                //{
                                //    DataSet dis = new DataSet();
                                //    cmd = "UPDATE Main SET UniqueDLCName=\"" + data.Name + "\" WHERE ID=" + file.ID;
                                //    OleDbDataAdapter das = new OleDbDataAdapter(cmd, cnn);
                                //    das.Fill(dis, "Main");
                                //    das.Dispose();
                                //}
                            }

                        //Fix the _preview_preview issue
                        var ms = data.OggPath; //var audiopath = ""; var audioprevpath = "";
                        var tst = "";
                        //MessageBox.Show("One or more");
                        //rtxt_StatisticsOnReadDLCs.Text = "maybe fixing .."+ file.Folder_Name+"\n"+ norm_path + "\n"+ rtxt_StatisticsOnReadDLCs.Text;
                        try
                        {
                            var sourceAudioFiles = Directory.GetFiles(file.Folder_Name, "*.wem", SearchOption.AllDirectories);
                            //if (sourceAudioFiles.Length>0)
                            //var targetAudioFiles = new List<string>();

                            foreach (var fil in sourceAudioFiles)
                            {
                                tst = fil;
                                //MessageBox.Show("test2.02 " + fil);
                                //rtxt_StatisticsOnReadDLCs.Text = "thinking about fixing _preview_preview issue.." + norm_path +"-"+tst+ "\n"+rtxt_StatisticsOnReadDLCs.Text;
                                if (fil.LastIndexOf("_preview_preview.wem") > 0)
                                {
                                    ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                                    File.Move((ms + "_preview.wem"), (ms + ".wem"));
                                    File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                                    //bcapirtxt_StatisticsOnReadDLCs.Text = "fixing _preview_preview issue..." + rtxt_StatisticsOnReadDLCs.Text;
                                }
                            }
                        }
                        catch (Exception ee)
                        {
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "FAILED6-" + ee.Message + tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            Console.WriteLine(ee.Message);
                        }
                        if (data == null)
                        {
                            MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = "...5" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //dlcSavePath = ds.Tables[0].Rows[i].ItemArray[1].ToString() + "\\";// + ((info.PackageVersion == null) ? "Original" : "CDLC") + "-" + info.SongInfo.SongYear +".psarc";
                        //var dlcSavePath = GeneralExtensions.GetShortName("{0}_{1}_v{2}", (((file.Version == null) ? "Original" : "CDLC") + "_" + info.SongInfo.SongDisplayName), (info.SongInfo.SongDisplayName + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongYear), info.PackageVersion, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));
                        var FN = "";
                        //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(txt_File_Name.Text, i, true, chbx_Additional_Manipulations.GetItemChecked(25), false, files);
                        //else FN = GetShortNamet("{0}-{1}-v{2}", ("def" + ((file.Version == null) ? "ORIG" : "CDLC") + "_" + file.Artist), (file.Album_Year.ToInt32() + "_" + file.Album + "_" + file.Song_Title), file.Version, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));//((data.PackageVersion == null) ? "Original" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                        if (file.Is_Alternate == "Yes" && file.Author != "Custom Song Creator" && file.Author == "" && rrt != "Custom Song Creator" && !chbx_Additional_Manipulations.GetItemChecked(47)) FN += "a." + file.Alternate_Version_No + file.Author;

                        //rtxt_StatisticsOnReadDLCs.Text = "fn: " + FN + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (chbx_Additional_Manipulations.GetItemChecked(8) || chbx_PS3.Checked)
                        {
                            FN = FN.Replace(".", "_");
                            FN = FN.Replace(" ", "_");
                        }

                        dlcSavePath = txt_TempPath.Text + "\\0_repacked\\" + (chbx_XBOX360.Checked ? "XBOX360" : chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "") + "\\" + FN;
                        //rtxt_StatisticsOnReadDLCs.Text = "rez : " + dlcSavePath + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //if (GameVersion.RS2014 == GameVersion.RS2012) //old code
                        //{
                        //    try
                        //    {
                        //        OggFile.VerifyHeaders(data.OggPath);
                        //    }
                        //    catch (InvalidDataException ex)
                        //    {
                        //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        return;
                        //    }
                        //}
                        //rtxt_StatisticsOnReadDLCs.Text = "genf : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                        int progress = (i + 1) * 100;
                        errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        //if (platformPC.Checked)
                        numPlatforms++;
                        if (chbx_Mac.Checked)
                            numPlatforms++;
                        if (chbx_XBOX360.Checked)
                            numPlatforms++;
                        if (chbx_PS3.Checked)
                            numPlatforms++;

                        var DBc_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";

                        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                        if (chbx_PC.Checked)
                            try
                            {
                                data.Pc = true;
                                bwRGenerate.ReportProgress(progress, "Generating PC package");
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, CurrentGameVersion));
                                progress += step;
                                bwRGenerate.ReportProgress(progress);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
                                errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            }

                        if (chbx_Mac.Checked)
                            try
                            {
                                data.Mac = true;
                                bwRGenerate.ReportProgress(progress, "Generating Mac package");
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, CurrentGameVersion));
                                progress += step;
                                bwRGenerate.ReportProgress(progress);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
                                errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            }

                        if (chbx_XBOX360.Checked)
                            try
                            {
                                data.XBox360 = true;
                                bwRGenerate.ReportProgress(progress, "Generating XBox 360 package");
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, CurrentGameVersion));
                                progress += step;
                                bwRGenerate.ReportProgress(progress);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
                                errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            }

                        if (chbx_PS3.Checked)
                            try
                            {
                                data.PS3 = true;
                                bwRGenerate.ReportProgress(progress, "Generating PS3 package");
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, CurrentGameVersion));
                                progress += step;
                                bwRGenerate.ReportProgress(progress);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                errorsFound.AppendLine(String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace));
                                UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
                            }
                        data.CleanCache();
                        i++;
                        var source = chbx_PS3.Checked ? "_ps3.psarc.edat" : (chbx_PC.Checked ? "_p" : (chbx_Mac.Checked ? "_m" : "")) + ".psarc";

                        if (File.Exists(dlcSavePath + source))
                        {
                            //copyftp
                            string txt_FTPPat = "";
                            var dest = "";

                            if (chbx_PS3.Checked && chbx_Additional_Manipulations.GetItemChecked(49))
                            {
                                if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP1"];
                                else txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP2"];
                                dest = txt_TempPath.Text;
                                var a = MainDB.FTPFile(txt_FTPPat, dlcSavePath + source, txt_TempPath.Text, "");
                                copyftp = " and " + a + "FTPed";
                            }
                            else if ((chbx_PC.Checked || chbx_Mac.Checked) && chbx_Additional_Manipulations.GetItemChecked(49))
                            {
                                dest = txt_RocksmithDLCPath.Text + "\\" + FN + source;
                                if (File.Exists(dlcSavePath + source))
                                    try
                                    {
                                        File.Copy(dlcSavePath + source, dest, true);
                                    }
                                    catch (Exception ee)
                                    {
                                        copyftp = "Not "; Console.Write(ee.Message);
                                    }
                                copyftp = "and " + copyftp + "Copied";
                            }

                            //Add Pack Audit Trail
                            //calc hash and file size
                            System.IO.FileInfo fi = null;
                            try
                            {
                                fi = new System.IO.FileInfo(dlcSavePath + source);
                            }
                            catch (System.IO.FileNotFoundException ee)
                            {
                                Console.Write(ee.Message);
                                rtxt_StatisticsOnReadDLCs.Text = "error at pack details save" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                                frm1.ShowDialog();
                            }

                            //Generating the HASH code
                            var FileHash = "";
                            using (FileStream fs = File.OpenRead(dlcSavePath + source))
                            {
                                SHA1 sha = new SHA1Managed();
                                FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                                fs.Close();
                            }

                            //var cmds = "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";";
                            //DataSet dfs = new DataSet();
                            var norec = 0;
                            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBc_Path))
                            //{
                            //    try
                            //    {
                            //        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                            //        da.Fill(dfs, "Pack_AuditTrail");
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        MessageBox.Show(ex.Message + "Can not open Main DB connection ! ");
                            //    }
                            DataSet dfs = new DataSet(); dfs = DLCManager.SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";");
                            norec = dfs.Tables[0].Rows.Count;
                            if (norec == 0)
                            {
                                var fnn = dlcSavePath + source;
                                string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                var fnnon = Path.GetFileName(fnn);
                                var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                var insertA = "\"" + dest + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + file.Platform + "\"";
                                // var udatevA = "Select "+ "\"" + dest + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + fi.Length + "\",\"" + FileHash + "\"," + file.ID + ",\"" + file.DLC_Name + "\"" + " FROM Pack_AuditTrail WHERE FileHash<>\"" + FileHash + "\"";
                                DLCManager.InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                            }
                        }
                        //Restore the DDremoved copies
                        xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml.old", SearchOption.AllDirectories);
                        platform = file.Folder_Name.GetPlatform();
                        if (chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(3))
                        {
                            //if (bassRemoved == "Yes") file.Has_BassDD = "Yes";
                            foreach (var xml in xmlFiles)
                            {
                                Song2014 xmlContent = null;
                                try
                                {
                                    xmlContent = Song2014.LoadFromFile(xml);
                                    if ((!(chbx_Additional_Manipulations.GetItemChecked(52) && file.Keep_BassDD == "Yes") && xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(5))
                                        || (!(chbx_Additional_Manipulations.GetItemChecked(53) && file.Keep_DD == "Yes") && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm"))
                                          && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(3))
                                       )
                                    {
                                        if (chbx_Additional_Manipulations.GetItemChecked(5) && !chbx_Additional_Manipulations.GetItemChecked(3) && !(xmlContent.Arrangement.ToLower() == "bass")) continue;
                                        //Save a copy
                                        File.Copy(xml.Replace(".old", ""), xml.Replace(".old", ".woDD"), true);
                                        File.Copy(xml, xml.Replace(".old", ""), true);
                                        var json = "";
                                        if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                 
                                            json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
                                        else
                                            json = xml.Replace("songs\\arr", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", SearchOption.AllDirectories)[0])).Replace(".xml", ".json");

                                        File.Copy(json.Replace(".old", ""), json.Replace(".old", ".woDD"), true);
                                        File.Copy(json, json.Replace(".old", ""), true);
                                    }
                                }
                                catch (Exception ee) { Console.Write(ee.Message); }
                            }
                        }
                        var DBc_Path2 = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
                        if (File.Exists(dlcSavePath + source)) UpdatePackingLog("LogPacking", DBc_Path2, packid, file.ID, "done");
                    }
                    //MessageBox.Show("tst");
                }
            }
            //bcapirtxt_StatisticsOnReadDLCs.Text = "\n...Repack done.." + rtxt_StatisticsOnReadDLCs.Text;
            MessageBox.Show("Repack done" + copyftp);
        }

        public static string GetShortNamet(string Format, string Artist, string Title, string Version, bool Acronym)
        {
            //if (!Acronym)
            //    return String.Format(Format, Artist.(GetValidName(true, true)), Title.GetValidName(true, true), Version).Replace(" ", "-");
            //return String.Format(Format, Artist.(Acronym()), Title.GetValidName(true, true), Version).Replace(" ", "-");
            return (Artist + Title + Version).Replace(" ", "");
        }


        public static string GetValidNameg(string value, bool allowSpace = false, bool allowStartsWithNumber = false, bool underscoreSpace = false, bool frets24 = false)
        {
            // valid characters developed from actually reviewing ODLC artist, title, album names
            string name = String.Empty;

            if (!String.IsNullOrEmpty(value))
            {
                // ODLC artist, title, album character use allows these but not these
                // allow use of accents Über ñice \\p{L}
                // allow use of unicode punctuation \\p{P\\{S} not currently implimented
                // may need to be escaped \t\n\f\r#$()*+.?[\^{|  ... '-' needs to be escaped if not at the beginning or end of regex sequence
                // allow use of only these special characters \\-_ /&.:',!?()\"#
                // allow use of alphanumerics a-zA-Z0-9
                // tested and working ... Üuber!@#$%^&*()_+=-09{}][":';<>.,?/ñice

                Regex rgx = new Regex((allowSpace) ? "[^a-zA-Z0-9\\-_ /&.:',!?()\"#\\p{L}]" : "[^a-zA-Z0-9\\-_/&.:',!?()\"#\\p{L} ]");
                name = rgx.Replace(value, "");

                Regex rgx2 = new Regex(@"^[\d]*\s*");
                if (!allowStartsWithNumber)
                    name = rgx2.Replace(name, "");

                // prevent names from starting with special characters -_* etc
                Regex rgx3 = new Regex("^[^A-Za-z0-9]*");
                name = rgx3.Replace(name, "");

                if (frets24)
                {
                    if (name.Contains("24"))
                    {
                        name = name.Replace("_24_", "_");
                        name = name.Replace("_24", "");
                        name = name.Replace("24_", "");
                        name = name.Replace(" 24 ", " ");
                        name = name.Replace("24 ", " ");
                        name = name.Replace(" 24", " ");
                        name = name.Replace("24", "");
                    }
                    name = name.Trim() + " 24";
                }

                if (underscoreSpace)
                    name = name.Replace(" ", "_");
            }

            return name.Trim();
        }

        public static string StripPlatformEndName(string value)
        {
            if (value.EndsWith(GamePlatform.Pc.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.Mac.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.XBox360.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.PS3.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.PS3.GetPathName()[2] + ".psarc"))
            {
                return value.Substring(0, value.LastIndexOf("_"));
            }

            return value;
        }



        static void UpdatePackingLog(string DB, string DBc_Path, int packid, string dlcID, string ex)
        {
            //var sel = "";
            //try
            //{
            //var cmdi = "INSERT into " + DB + "(Pack, CDLC_ID, Dates, Comments) VALUES (\"" + packid + "\",\"" + dlcID + "\",\"" + System.DateTime.Now + "\",\"" + ex.Replace("'", "") + "\")";
            var insertcmdd = "Pack, CDLC_ID, Dates, Comments";
            var insertvalues = "\"" + packid + "\"," + dlcID + ",\"" + System.DateTime.Now + "\",\"" + ex.Replace("'", "") + "\"";
            DLCManager.InsertIntoDBwValues(DB, insertcmdd, insertvalues);
            //    DataSet dsz = new DataSet();
            //    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBc_Path))
            //    {
            //        OleDbDataAdapter dab = new OleDbDataAdapter(cmdi, cnb);
            //        dab.Fill(dsz, "Main");
            //        dab.Dispose();
            //    }
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Main DB connection ! " + sel);
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "process changed" + rtxt_StatisticsOnReadDLCs.Text;
            if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
                pB_ReadDLCs.Value = e.ProgressPercentage;
            else
                pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;

            ShowCurrentOperation(e.UserState as string);
        }
        private void ShowCurrentOperation(string message)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "current" + rtxt_StatisticsOnReadDLCs.Text;
            //currentOperationLabel.Text = message;
            //currentOperationLabel.Refresh();
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "generate" + rtxt_StatisticsOnReadDLCs.Text;
            if (!(e.Result == null))
                switch (e.Result.ToString())
                {

                    case "generate":
                        var message = "Package was generated.";
                        if (errorsFound.Length > 0)
                            message = String.Format("Package was generated with errors! See below: {0}(1}", Environment.NewLine, errorsFound);
                        message += String.Format("{0}You want to open the folder in which the package was generated?{0}", Environment.NewLine);
                        if (MessageBox.Show(message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(Path.GetDirectoryName(dlcSavePath));
                        }
                        break;
                    case "error":
                        var message2 = String.Format("Package generation failed. See below: {0}{1}{0}", Environment.NewLine, errorsFound);
                        MessageBox.Show(message2, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Parent.Focus();
                        break;
                }
            btn_RePack.Text = "RePack";
            //dlcGenerateButton.Enabled = true;
            //updateProgress.Visible = false;
            //currentOperationLabel.Visible = false;
        }

        private void rbtn_Population_Groups_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Population_Groups.Checked)
            {
                cbx_Groups.Enabled = true;
                // to add the query programatically
            }
            else
            {
                cbx_Groups.Enabled = false;
                //cbx_Groups.Items.Clear;
            }
        }

        private void cbx_Title_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Title.Text += cbx_Title.Items[cbx_Title.SelectedIndex].ToString();
        }

        private void cbx_Title_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Title_Sort.Text = txt_Title_Sort.Text + cbx_Title_Sort.Items[cbx_Title_Sort.SelectedIndex].ToString();
        }

        private void cbx_Artist_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Artist.Text += cbx_Artist.Items[cbx_Artist.SelectedIndex].ToString();
        }

        private void cbx_Artist_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Artist_Sort.Text += cbx_Artist_Sort.Items[cbx_Artist.SelectedIndex].ToString();
        }

        private void cbx_Album_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Album.Text += cbx_Album.Items[cbx_Album.SelectedIndex].ToString();
        }

        private void cbx_File_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_File_Name.Text += cbx_File_Name.Items[cbx_File_Name.SelectedIndex].ToString();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();

            //Show Intro database window
            MainDB frm = new MainDB((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), txt_TempPath.Text, chbx_Additional_Manipulations.GetItemChecked(33), txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.Show();
        }

        private void btn_Standardization_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();
            var DBb_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //MessageBox.Show(DBb_Path);
            Standardization frm = new Standardization(DBb_Path, txt_TempPath.Text, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.Show();
        }

        private void chbx_Additional_Manipualtions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Translation_And_Correction((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text));
            MessageBox.Show("Normalization Applied");
        }

        private void chbx_HomeDBG_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "D:\\Spiele\\Steam\\steamapps\\common\\Rocksmith2014\\dlc";
            txt_DBFolder.Text = "C:\\GitHub\\tmp\\";
            txt_TempPath.Text = "C:\\GitHub\\tmp\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
            chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Checked);
        }

        private void chbx_WorkDGB_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "C:\\GitHub\\tmp\\Temp";
            txt_DBFolder.Text = "C:\\GitHub\\tmp\\Temp";
            txt_TempPath.Text = "C:\\GitHub\\tmp\\Temp\\0";
            //chbx_CleanTemp.Checked = false;
            //chbx_CleanDB.Checked = false;
            chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked);

        }

        private void mainBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void chbx_HomeDGBVM_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp\\to import";
            txt_DBFolder.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp";
            txt_TempPath.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp\\to import\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Convert SongInfoShort to SongInfo that contains only user selections or defualts
        /// </summary>
        /// <param name="songListShort"></param>
        /// <param name="songList"></param>
        /// <returns></returns>
        private List<RocksmithToTabLib.SongInfo> SongInfoShortToSongInfo(IList<SongInfoShort> songListShort, IList<RocksmithToTabLib.SongInfo> songList)
        {
            var songIdPre = String.Empty;
            var newSongList = new List<RocksmithToTabLib.SongInfo>();
            var newSongNdx = 0;

            for (var i = 0; i < songListShort.Count(); i++)
            {
                var songIdShort = songListShort[i].Identifier;
                var arrangementShort = songListShort[i].Arrangement;

                if (songIdPre != songIdShort)
                {
                    // add the new song info
                    var songInfo = songList.FirstOrDefault(x => x.Identifier == songIdShort);
                    newSongList.Add(songInfo);
                    newSongNdx++;

                    // clear arrangments so we can add user selections
                    if (arrangementShort != null)
                    {
                        newSongList[newSongNdx - 1].Arrangements.Clear();
                        newSongList[newSongNdx - 1].Arrangements.Add(arrangementShort);
                    }
                }
                else if (songIdPre == songIdShort && arrangementShort != null)
                    newSongList[newSongNdx - 1].Arrangements.Add(arrangementShort);

                songIdPre = songIdShort;
            }

            return newSongList;
        }

        /// <summary>
        /// Struct containing song Identifier and 
        /// song Arrangement information for a PSARC file
        /// </summary>
        private class SongInfoShort
        {
            public string Identifier { get; set; }
            public string Arrangement { get; set; }
        }

        public static string calc_path(string jsonsFiles)
        {
            var ttt = Path.GetDirectoryName(jsonsFiles);
            var pattth = ttt.IndexOf("\\manifests\\");
            var ddd = ttt.Substring(pattth + 1, ttt.Length - pattth - 1);
            return ddd;
        }


        private void renamedir(string source_dir, string destination_dir)
        {
            try
            {
                ////replicate the directory structure
                //foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))// Create subdirectory structure in destination    
                //{
                //    Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                //}

                //Directory.CreateDirectory(destination_dir);
                //foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                //{
                //    File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                //}
                //Directory.Delete(source_dir, true);    

                Directory.Move(source_dir, destination_dir);
            }
            catch (System.IO.FileNotFoundException ee)
            {



                Console.WriteLine(ee.Message);
                rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Moving folder: " + source_dir + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //continue;
            }
        }

        private void DBchanged(object sender, EventArgs e)
        {
            if (chbx_DefaultDB.Checked == true) chbx_DefaultDB.Checked = false;
        }

        private void chbx_DefaultDB_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_DefaultDB.Checked == true) txt_DBFolder.Text = MyAppWD;
            //else txt_DBFolder.Text = "";
        }

        private void button1_Click1(object sender, EventArgs e)
        {

        }

        private void btn_LoadRetailSongs_Click(object sender, EventArgs e)
        {
            rtxt_StatisticsOnReadDLCs.Text = "Starting Retail Songs processing ...." + DateTime.Now + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            var Temp_Path_Import = txt_TempPath.Text + "\\dlcpacks";
            string pathDLC = txt_RocksmithDLCPath.Text;
            if (!chbx_DebugB.Checked) MessageBox.Show("Please make sure one of the following Retail Packs:\ncache.psarc, songs.psarc, rs1compatibilitydisc.psarc(.edat if PS3 format), rs1compatibilitydlc.psarc(.edat) \n\n, are in the Import Folder: " + pathDLC + "\n\nAlso, make sure you have enought space for the packing&unpacking operations Platform x 3GB");
            CreateTempFolderStructure(txt_TempPath.Text, txt_TempPath.Text + "\\0_old", txt_TempPath.Text + "\\0_broken", txt_TempPath.Text + "\\0_duplicate", txt_TempPath.Text + "\\0_dlcpacks", pathDLC, txt_TempPath.Text + "\\0_Repacked", txt_TempPath.Text + "\\0_Repacked\\XBOX", txt_TempPath.Text + "\\0_Repacked\\PC", txt_TempPath.Text + "\\0_Repacked\\MAC", txt_TempPath.Text + "\\0_Repacked\\PS", ConfigRepository.Instance()["dlcm_LogPath"]);

            //read all the .PSARCs in the IMPORT folder
            var jsonFiles = Directory.GetFiles(pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014"), "*.psarc*", SearchOption.AllDirectories);
            if (pathDLC.IndexOf("Rocksmith2014\\DLC") == 0) jsonFiles = Directory.GetFiles(pathDLC, "*.psarc*", SearchOption.AllDirectories);

            var inputFilePath = ""; var locat = ""; var songshsanP = ""; var unpackedDir = "";
            var DBb_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //string source_dir = "";
            //string destination_dir = "";
            var t = "";
            Platform platformDLC;//
            var platformDLCP = "";

            //Clean dlcpack Folders
            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !chbx_Additional_Manipulations.GetItemChecked(38)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {
                try
                {
                    //clear content of dlcpacks folder
                    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks");
                    //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                    //{
                    //    file.Delete();
                    //}
                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                    {
                        if (dir.Name != "manipulated" && dir.Name != "manifests" && dir.Name != "temp") dir.Delete(true);
                    }


                    foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    //clear content of dlcpacks\\manipulated\temp folder
                    System.IO.DirectoryInfo downloadedMMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manipulated\\temp");
                    //foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                    //{
                    //    file.Delete();
                    //}
                    foreach (DirectoryInfo dir in downloadedMMessageInfo.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    foreach (FileInfo file in downloadedMMessageInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    System.IO.DirectoryInfo downloadedMmMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manifests");
                    foreach (DirectoryInfo dir in downloadedMmMessageInfo.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    foreach (FileInfo file in downloadedMmMessageInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                catch (System.IO.FileNotFoundException ee)
                {
                    Console.WriteLine(ee.Message);
                    rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Temp Folder Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //continue;
                }
            }

            //Clean CachetDB
            //DataSet dss = new DataSet();
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
            //    {
            //        if (chbx_CleanDB.Checked)
            //        {
            //            rtxt_StatisticsOnReadDLCs.Text = "Cleaning....Cache table...." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //            OleDbDataAdapter dan = new OleDbDataAdapter("DELETE FROM Cache;", cnn);
            //            dan.Fill(dss, "Cache");
            //            dan.Dispose();
            //            rtxt_StatisticsOnReadDLCs.Text = " Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //        }
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned the CacheDB" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    //continue;
            //}
            DLCManager.DeleteFromDB("Cache n", "DELETE FROM Cache;");
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 2 * 3; //jsonFiles.Length
                                         //UNPACK x3 psarcs
            foreach (var json in jsonFiles)
            {
                platformDLC = json.GetPlatform(); //Platform 
                platformDLCP = platformDLC.platform.ToString();
                if (json == pathDLC + "\\songs.psarc" || json == pathDLC + "\\rs1compatibilitydlc.psarc.edat" || json == pathDLC + "\\rs1compatibilitydisc.psarc.edat" || ((json == pathDLC + "\\rs1compatibilitydlc_p.psarc" || json == pathDLC + "\\rs1compatibilitydisc_p.psarc") && platformDLCP == "Pc") || ((json == pathDLC + "\\rs1compatibilitydlc_m.psarc" || json == pathDLC + "\\rs1compatibilitydisc_m.psarc") && platformDLCP == "Mac"))
                {
                    rtxt_StatisticsOnReadDLCs.Text = "Decompressing  " + ".... " + json + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    pB_ReadDLCs.Increment(2);

                    if (json == pathDLC + "\\songs.psarc") //RS14 RETAIL
                    {
                        inputFilePath = json; locat = "CACHE";
                        t = inputFilePath;
                        if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try
                            {
                                // UNPACK
                                rtxt_StatisticsOnReadDLCs.Text = "Unpacking cache.psarc.... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (File.Exists(pathDLC + "\\cache.psarc"))
                                {

                                    unpackedDir = Packer.Unpack(pathDLC + "\\cache.psarc", txt_TempPath.Text + "\\0_dlcpacks\\temp", false, false); //, falseUnpack cache.psarc for RS14 Official Retails songs rePACKING

                                    //check if platform is correctly identified, &if NOT, correct it
                                    var startI = new ProcessStartInfo();
                                    startI.FileName = Path.Combine(AppWD, "7za.exe");
                                    startI.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                    var za = unpackedDir + "\\cache8.7z";
                                    //if (!Directory.Exists(txt_TempPath.Text + "\\0_dlcpacks\\manifests")) di = Directory.CreateDirectory(txt_TempPath.Text + "\\0_dlcpacks\\manifests");
                                    //if (!Directory.Exists(txt_TempPath.Text + "\\0_dlcpacks\\manifests\\songs")) di = Directory.CreateDirectory(txt_TempPath.Text + "\\0_dlcpacks\\manifests\\songs");
                                    //File.Copy(hsanDir, txt_TempPath.Text + "\\0_dlcpacks\\manifests\\songs\\songs.hsan", true);
                                    startI.Arguments = String.Format(" x {0} -o{1}",
                                                                        za,
                                                                        unpackedDir.Replace("\\cache_Pc", "\\cache_Pc\\manipulated"));// + platformDLCP TempPath + "\\0_dlcpacks\\cache_pc\\
                                    startI.UseShellExecute = true; startI.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                                                                                             //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    var tmtpdir = unpackedDir.Replace("\\cache_Pc", "\\cache_Pc\\manipulated") + "\\audio\\ps3";
                                    if (Directory.Exists(tmtpdir))
                                    {
                                        renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_ps3"));
                                        unpackedDir = unpackedDir.Replace("_Pc", "_ps3");
                                        platformDLCP = "PS3";
                                    }
                                    tmtpdir = unpackedDir.Replace("\\cache_Pc", "\\cache_Pc\\manipulated") + "\\audio\\mac";
                                    if (Directory.Exists(tmtpdir))
                                    {
                                        renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_Mac"));
                                        unpackedDir = unpackedDir.Replace("_Pc", "_Mac");
                                        platformDLCP = "Mac";
                                    }
                                    //clear temp cache_Pc folder
                                    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(unpackedDir);
                                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                                    {
                                        if (dir.Name == "manipulated") dir.Delete(true);
                                    }

                                    //move cache_xx to dlcpacks
                                    if (Directory.Exists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"))) Directory.Delete(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                    renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                    unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks");
                                }

                                //Process SONGS.PSARC
                                rtxt_StatisticsOnReadDLCs.Text = "Unpacking songs.psarc.... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //if (platformDLCP == "PS3")
                                unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", false, false);//, false
                                                                                                                                   //else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\manipulated", true, false, false);


                                //FIX for unpacking w the wrong folder extension
                                //And unpacking of PS3 WEM
                                if (Directory.Exists(unpackedDir + "\\songs\\bin\\ps3"))
                                {
                                    //Convert WEM to OGG
                                    //if (platformDLCP == "PS3")
                                    //{
                                    var startInfo = new ProcessStartInfo();
                                    //var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));//unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                    startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                    startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                                                             //var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + ((platfor == "PS3") ? "" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""))) + ".psarc";
                                    startInfo.Arguments = String.Format(" --unpack --version=RS2014 --platform={0} --output={1} --input={2} --decodeogg",
                                                                        platformDLCP,
                                                                        unpackedDir.Replace("songs_Pc", ""),
                                                                        inputFilePath);// + platformDLCP
                                    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                    //if (!File.Exists(t))
                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 15); //wait 15min
                                                                                                                 //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_ps3"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_ps3");
                                    platformDLCP = "PS3";
                                    //}
                                    //Convert2OGG(unpackedDir + "\\Audio\\"+ (platformDLCP == "Pc" ? "windows" : platformDLCP), platformDLCP);
                                    //else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\manipulated", true, false, false);
                                }
                                //elseif (platformDLCP == "PS3") ;//unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\manipulated", false, false, false);
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", true, false);//false

                                if (Directory.Exists(unpackedDir + "\\songs\\bin\\macos"))
                                {
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_Mac"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_Mac");
                                    platformDLCP = "Mac";
                                }

                                //move cache_xx to dlcpacks
                                if (Directory.Exists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"))) Directory.Delete(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks");

                                //Directory.Move(unpackedDir ,unpackedDir.Replace("\\manipulated", ""));
                                //unpackedDir = unpackedDir.Replace("\\manipulated","");
                                songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                            }
                            catch (Exception ex)
                            {
                                rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + inputFilePath + "---" + txt_TempPath.Text + "\\0songs" + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                        else
                        {
                            unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\songs_" + platformDLCP;
                            songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                        }

                        rtxt_StatisticsOnReadDLCs.Text = "Processed cache.psarc & songs.psarc" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    } //repacking at the moment manually with psarc 1.4 and lzma ratio 0
                    else if (json == pathDLC + "\\rs1compatibilitydlc.psarc.edat" || (json == pathDLC + "\\rs1compatibilitydlc_p.psarc" && platformDLCP == "Pc") || (json == pathDLC + "\\rs1compatibilitydlc_m.psarc" && platformDLCP == "Mac")) //RS12 DLC
                    {
                        inputFilePath = json;
                        locat = "COMPATIBILITY";
                        if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try // UNPACK
                            {
                                unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", false, false).Replace(".psarc", ""); //, false;
                            }
                            catch (Exception ex)
                            {
                                rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                        rtxt_StatisticsOnReadDLCs.Text = "Repacking " + json + "2 use the internal/Browser Psarc Read function.... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                            renamedir(unpackedDir + "\\manifests\\songs_rs1dlc", unpackedDir + "\\manifests\\songs");

                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
                            startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 5); //wait 5min
                                if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            rtxt_StatisticsOnReadDLCs.Text = "renaming internal folder \n" + rtxt_StatisticsOnReadDLCs.Text;

                            //Convert WEM to OGG
                            //Convert2OGG(unpackedDir + "\\Audio\\"+platformDLCP, platformDLCP);
                        }
                        catch (Exception ex)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at dir rename" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        rtxt_StatisticsOnReadDLCs.Text = "Processed rs1compatibilitydlc.psarc" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    else if (json == pathDLC + "\\rs1compatibilitydisc.psarc.edat" || (json == pathDLC + "\\rs1compatibilitydisc_p.psarc" && platformDLCP == "Pc") || (json == pathDLC + "\\rs1compatibilitydisc_m.psarc" && platformDLCP == "Mac")) //RS12 RETAIL
                    {
                        inputFilePath = json; locat = "RS1Retail";
                        if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try // UNPACK
                            {
                                if (platformDLCP == "PS3")
                                {
                                    //Packer.Unpack fails
                                    //unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", false, false, false).Replace(".psarc", "");
                                    var startInfo = new ProcessStartInfo();
                                    //var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));//unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                    startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                    startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                                                             //var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + ((platfor == "PS3") ? "" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""))) + ".psarc";
                                    startInfo.Arguments = String.Format(" --unpack --decodeogg --version=RS2014 --platform={0} --output={1} --input={2}",
                                                                        platformDLCP,
                                                                        txt_TempPath.Text + "\\0_dlcpacks",//unpackedDir.Replace("songs_Pc", ""),
                                                                        inputFilePath);// + platformDLCP
                                    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                    //if (!File.Exists(t))
                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 1min
                                        if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                }
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", true, false);//, false)
                            }
                            catch (Exception ex)
                            {
                                rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                        rtxt_StatisticsOnReadDLCs.Text = "Repacking " + json + " 2 use the internal/Browser Psarc Read function.... " + json + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                            renamedir(unpackedDir + "\\manifests\\songs_rs1disc", unpackedDir + "\\manifests\\songs");
                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc.psarc";
                            startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t)) ;
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 10min
                                if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1disc pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                                renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                                rtxt_StatisticsOnReadDLCs.Text = "renaming internal folder \n" + rtxt_StatisticsOnReadDLCs.Text;
                                //t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3.psarc";
                                //startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                //                            t,
                                //                            unpackedDir);// + platformDLCP
                                //DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 10); //wait 10min
                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing again rs1disc pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            //Convert WEM to OGG
                            //Convert2OGG(unpackedDir + "\\Audio\\"+ (platformDLCP=="Pc"? "windows" : platformDLCP), platformDLCP);
                            //Convert WEM to OGG
                            //if psarc.exe fails
                            if (platformDLCP == "PS3")
                            {
                                //commenting next line altough owrknig to use the official packer
                                //Convert2OGG(unpackedDir + "\\Audio\\" + (platformDLCP == "Pc" ? "windows" : platformDLCP), platformDLCP);
                                var unpackedDir1 = unpackedDir;
                                var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", SearchOption.AllDirectories);
                                var i = 0;
                                foreach (var wem in wemFiles)
                                {

                                    i++;
                                    //rtxt_StatisticsOnReadDLCs.Text = (rtxt_StatisticsOnReadDLCs.Text).Replace("Starting Decompressing WEMs " + (i - 1) + "/", "Starting Decompressing WEMs " + i + "/");
                                    startInfo = new ProcessStartInfo();

                                    startInfo.FileName = Path.Combine(AppWD, "ww2ogg.exe");
                                    startInfo.WorkingDirectory = AppWD;// unpackedDir1;// Path.GetDirectoryName();
                                    startInfo.Arguments = String.Format(" {0} -o {1} --pcb packed_codebooks_aoTuV_603.bin",
                                                                        wem,
                                                                        wem.Replace(".wem", ".ogg"));
                                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                    if (File.Exists(wem))
                                        using (var DDC = new Process())
                                        {
                                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 30 * 60); //wait 30min
                                                                                                                     //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when decrypting wem files !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                                                                                     //Console.WriteLine("{0} is active: {1}", DDC.Id, !DDC.HasExited);
                                                                                                                     //DDC.Kill();
                                        }
                                }
                                //    startInfo = new ProcessStartInfo();
                                //    //var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));//unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                //    startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                //    startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                //    //var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + ((platfor == "PS3") ? "" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""))) + ".psarc";
                                //    startInfo.Arguments = String.Format(" --unpack --decodeogg --version=RS2014 --platform={0} --output={1} --input={2}",
                                //                                        platformDLCP,
                                //                                        txt_TempPath.Text + "\\0_dlcpacks",//unpackedDir.Replace("songs_Pc", ""),
                                //                                        t);// + platformDLCP
                                //    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                //    //if (!File.Exists(t))
                                //    using (var DDC = new Process())
                                //    {
                                //        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 1min
                                //        //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //    }
                                //    renamedir(txt_TempPath.Text + "\\0_dlcpacks", txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3");
                                //    //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                                //    unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                            }
                            //else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\", true, false, false);
                        }
                        catch (Exception ex)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at dir rename" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        rtxt_StatisticsOnReadDLCs.Text = "Processed rs1compatibilitydisc.psarc" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    else continue;
                    //var inputFilePath = txt_RocksmithDLCPath.Text + "\\songs.psarc";
                    //IList<RocksmithToolkitLib.Song2014ToTab.SongInfoShort> songListShort = null;
                    //IList<SongInfoShort> songListShort = null;

                    Console.WriteLine("Opening archive {0} ...", inputFilePath);
                    Console.WriteLine();



                    //Populate DB
                    rtxt_StatisticsOnReadDLCs.Text = "Populating CACHE DB" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var pic = "";
                    //try
                    //{
                    // var t = inputFilePath;//if (!File.Exists(inputFilePath)) 
                    //if (json == txt_RocksmithDLCPath.Text + "\\rs1compatibilitydlc.psarc.edat") inputFilePath= "C:\\GitHub\\rocksmith-custom-song-toolkit\\RocksmithTookitGUI\\bin\\Debug\\edat\\PS3.psarc";
                    var browser = new PsarcBrowser(t);
                    //inputFilePath = t;
                    var songList = browser.GetSongList();
                    var toolkitInfo = browser.GetToolkitInfo();
                    //if (songshsanP.Contains("songs_rs1dlc")) File.Move(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                    var AudioP = "";
                    var AudioPP = "";
                    var AudioP1 = "";
                    var AudioPP1 = "";
                    foreach (var song in songList)
                    {
                        //DataSet dsx = new DataSet();
                        //DataSet dsdx = new DataSet();

                        //using (OleDbConnection cgn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                        //{
                        //    OleDbDataAdapter dah = new OleDbDataAdapter("SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "\"", cgn);
                        //    dah.Fill(dsx, "WEM2OGGCorrespondence");
                        //}
                        DataSet dsx = new DataSet(); dsx = DLCManager.SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "\"");

                        AudioP1 = (dsx.Tables[0].Rows.Count > 0) ? dsx.Tables[0].Rows[0].ItemArray[0].ToString() : "";
                        //dsx.Dispose();
                        //using (OleDbConnection cvn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                        //{
                        //    OleDbDataAdapter dbh = new OleDbDataAdapter("SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "_preview\"", cvn);
                        //    dbh.Fill(dsdx, "WEM2OGGCorrespondence");
                        //}
                        DataSet dsdx = new DataSet(); dsdx = DLCManager.SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "_preview\"");
                        AudioPP1 = (dsdx.Tables[0].Rows.Count > 0) ? dsdx.Tables[0].Rows[0].ItemArray[0].ToString() : "";
                        if (locat == "RS1Retail")
                        {
                            pic = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds");
                            AudioP = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioP1 + (platformDLCP == "PS3" ? ".ogg" : "_fixed.ogg");
                            AudioPP = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioPP1 + (platformDLCP == "PS3" ? ".ogg" : "_fixed.ogg");
                        }
                        else if (locat == "COMPATIBILITY")
                        {
                            pic = songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds");
                            AudioP = (songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioP1 + "_fixed.ogg").Replace("rs1compatibilitydlc", "songs").Replace("_p_Pc", "_Pc").Replace("_p_Pc", "_Mac").Replace("m_Mac", "Mac");
                            AudioPP = (songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioPP1 + "_fixed.ogg").Replace("rs1compatibilitydlc", "songs").Replace("_p_Pc", "_Pc").Replace("_p_Pc", "_Mac").Replace("m_Mac", "Mac");
                        }
                        else if (locat == "CACHE")
                        {
                            pic = songshsanP.Replace("\\manifests\\songs\\songs.hsan", "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds");
                            AudioP = (songshsanP.Replace("\\manifests\\songs\\songs.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioP1 + "_fixed.ogg");
                            AudioPP = (songshsanP.Replace("\\manifests\\songs\\songs.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : platformDLCP)) + "\\") + AudioPP1 + "_fixed.ogg");
                        }

                        //convert to png
                        ExternalApps.Dds2Png(pic);

                        //dtx.Dispose();
                        //var f = "";
                        //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                        //{

                        //    DataSet dtx = new DataSet();
                        //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID from Cache AS O WHERE Platform=\"" + platformDLCP + "\" AND Identifier=\"" + song.Identifier.ToString() + "\"", cn);
                        //    da.Fill(dtx, "Cache");
                        DataSet dtx = new DataSet(); dtx = DLCManager.SelectFromDB("Cache", "SELECT ID from Cache AS O WHERE Platform=\"" + platformDLCP + "\" AND Identifier=\"" + song.Identifier.ToString() + "\"");
                        var aa = dtx.Tables[0].Rows.Count;
                        if (dtx.Tables[0].Rows.Count == 0) //If this record isn't already in the DB...add it

                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                            {
                                var commands = cnn.CreateCommand();
                                commands.CommandText = "INSERT INTO Cache(";
                                //command.CommandText += "ID, ";
                                commands.CommandText += "Identifier, ";
                                commands.CommandText += "Artist, ";
                                commands.CommandText += "ArtistSort, ";
                                commands.CommandText += "Album, ";
                                commands.CommandText += "Title, ";
                                commands.CommandText += "AlbumYear, ";
                                commands.CommandText += "Arrangements, ";
                                commands.CommandText += "Removed, ";
                                commands.CommandText += "AlbumArtPath, ";
                                commands.CommandText += "PSARCName, ";
                                commands.CommandText += "SongsHSANPath, ";
                                commands.CommandText += "Platform, ";
                                commands.CommandText += "AudioPath, ";
                                commands.CommandText += "AudioPreviewPath ";
                                commands.CommandText += ") VALUES (@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15"; ////@param1,
                                commands.CommandText += ")";

                                //command.Parameters.AddWithValue("@param1", ID);
                                commands.Parameters.AddWithValue("@param2", song.Identifier);
                                commands.Parameters.AddWithValue("@param3", song.Artist);
                                commands.Parameters.AddWithValue("@param4", song.ArtistSort);
                                commands.Parameters.AddWithValue("@param5", song.Album);
                                commands.Parameters.AddWithValue("@param6", song.Title);
                                commands.Parameters.AddWithValue("@param7", song.Year);
                                commands.Parameters.AddWithValue("@param8", string.Join(", ", song.Arrangements));
                                commands.Parameters.AddWithValue("@param9", "Yes");
                                commands.Parameters.AddWithValue("@param10", pic.Replace(".dds", ".png"));
                                commands.Parameters.AddWithValue("@param11", locat);
                                commands.Parameters.AddWithValue("@param12", songshsanP);
                                commands.Parameters.AddWithValue("@param13", platformDLCP);
                                commands.Parameters.AddWithValue("@param14", AudioP);
                                commands.Parameters.AddWithValue("@param15", AudioPP);
                                //EXECUTE SQL/INSERT
                                try
                                {
                                    commands.CommandType = CommandType.Text;
                                    cnn.Open();
                                    commands.ExecuteNonQuery();
                                    //rtxt_StatisticsOnReadDLCs.Text = String.Format("Saving: [{0}] = {1} - {2} ", song.Identifier,
                                    //                                song.Artist, song.Title) + "\n" + rtxt_StatisticsOnReadDLCs.Text;//({3}, {4})  {{{5}}//, song.Album, song.Year,string.Join(", ", song.Arrangements)
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DBb_Path + "-" + commands.CommandText);

                                    throw;
                                }
                                finally
                                {
                                    if (connection != null) connection.Close();
                                }
                                rtxt_StatisticsOnReadDLCs.Text += Environment.NewLine;
                            }
                        //        dtx.Clear();
                        //        dtx.Dispose();
                        //        dtx.Reset();
                        //    }


                        //}

                        //level the maxdiff overall setting in the xml
                        //var m = 1; var j = 0;
                        //for (m = 1; m <= j; m++)
                        //{
                        //    header = header.Replace("maxDifficulty=\"" + m + "\"", "maxDifficulty=\"0\"");
                        //}

                        //foreach (var song in toConvert)
                        //{
                        //    //var score = new Score();
                        //    // get all default or user specified arrangements for the song 
                        //    var arrangements = song.Arrangements;
                        //    Console.WriteLine("Converting song " + song.Identifier + "...");

                        //    foreach (var arr in arrangements)
                        //    {
                        //       ;// var arrangement = browser.GetArrangement(song..Identifier, arr);
                        //        // get maximum difficulty for the arrangement
                        //        var mf = new ManifestFunctions(GameVersion.RS2014);
                        //        ;//int maxDif = mf.GetMaxDifficulty(arrangement);

                        //        //if (allDif) // create seperate file for each difficulty
                        //        //{
                        //        //    for (int difLevel = 0; difLevel <= maxDif; difLevel++)
                        //        //    {
                        //        //        //ExportArrangement(score, arrangement, difLevel, inputFilePath, toolkitInfo);
                        //        //        //Console.WriteLine("Difficulty Level: {0}", difLevel);

                        //        //        //var baseFileName = CleanFileName(
                        //        //        //    String.Format("{0} - {1}", score.Artist, score.Title));
                        //        //        //baseFileName += String.Format(" ({0})", arr);
                        //        //        //baseFileName += String.Format(" (level {0:D2})", difLevel);

                        //        //        //SaveScore(score, baseFileName, outputDir, outputFormat);
                        //        //        //// remember to remove the track from the score again
                        //        //        //score.Tracks.Clear();
                        //        //    }
                        //        //}
                        //        //else // combine maximum difficulty arrangements into one file
                        //        //{
                        //        //    Console.WriteLine(String.Format("Maximum Difficulty Level: {0}", maxDif));
                        //        //    ExportArrangement(score, arrangement, maxDif, inputFilePath, toolkitInfo);
                        //        //}
                        //    }

                        //    //if (!allDif) // only maximum difficulty
                        //    //{
                        //    //    var baseFileName = CleanFileName(
                        //    //        String.Format("{0} - {1}", score.Artist, score.Title));
                        //    //    SaveScore(score, baseFileName, outputDir, outputFormat);
                        //    //}
                        //}

                        //    Console.WriteLine();
                    }

                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    MessageBox.Show("Can not Load " + platformDLCP);

                    //    Console.WriteLine("Error encountered:");
                    //    Console.WriteLine(ex.Message);
                    //}
                }//END no cache.psarc to be decompressed
            }
            rtxt_StatisticsOnReadDLCs.Text = "Ending Retail Songs processing ...." + DateTime.Now + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            Cache frm = new Cache(DBb_Path, txt_TempPath.Text, pathDLC, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();
            Cache frm = new Cache((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb", txt_TempPath.Text, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.ShowDialog();
        }

        public void Convert2OGG(string unpackedDir, string platform)
        {
            unpackedDir1 = unpackedDir;
            //rtxt_StatisticsOnReadDLCs.Text = "Starting Decompressing WEMs 0/" + wemFiles.Count().ToString() + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (!bwConvert.IsBusy) //&& data != null&& norows > 0
            {
                bwConvert.RunWorkerAsync(unpackedDir1);
                //rtxt_StatisticsOnReadDLCs.Text = " not buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }
            else
            {
                MessageBox.Show("Error when multithreading PS3 WEM conv to OGG");
                //object sender; DoWorkEventArgs e;
                //ConvertWEM(sender, e);
                //bcapirtxt_StatisticsOnReadDLCs.Text = " Buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }


            rtxt_StatisticsOnReadDLCs.Text = "Ended Decompressing WEMs" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
        }

        public void ConvertWEM(object sender, DoWorkEventArgs e)
        {
            var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", SearchOption.AllDirectories);
            var i = 0;
            foreach (var wem in wemFiles)
            {

                i++;
                //rtxt_StatisticsOnReadDLCs.Text = (rtxt_StatisticsOnReadDLCs.Text).Replace("Starting Decompressing WEMs " + (i - 1) + "/", "Starting Decompressing WEMs " + i + "/");
                var startInfo = new ProcessStartInfo();

                startInfo.FileName = Path.Combine(AppWD, "audiocrossreference.exe");
                startInfo.WorkingDirectory = unpackedDir1;// Path.GetDirectoryName();
                startInfo.Arguments = String.Format(" {0}",
                                                    wem);
                startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                if (File.Exists(wem))
                    using (var DDC = new Process())
                    {
                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 60); //wait min
                                                                                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when decrypting wem files !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        Console.WriteLine("{0} is active: {1}", DDC.Id, !DDC.HasExited);
                        DDC.Kill();
                    }
            }
        }
        public static string AddDD(string Folder_Name, string Is_Original, string xml, Platform platform, bool superOrg, bool InternalLog, string noLevels)
        {
            string DDAdded = "No";
            if (!File.Exists(xml + ".old")) File.Copy(xml, xml + ".old", false);
            else { File.Copy(xml + ".old", xml, true); }
            string json = "";
            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
            else
                json = (xml.Replace(".xml", ".json").Replace("songs\\arr", calc_path(Directory.GetFiles(Folder_Name, "*.json", SearchOption.AllDirectories)[0])));

            if (!File.Exists(json + ".old")) File.Copy(json, json + ".old", false);
            else { File.Copy(json + ".old", json, true); if (Is_Original == "Yes") return "Yes"; }
            //bcapirtxt_StatisticsOnReadDLCs.Text = chbx_Additional_Manipualtions.GetItemChecked(12).ToString()+ chbx_Additional_Manipualtions.GetItemChecked(26).ToString()+"...." + Path.GetFileNameWithoutExtension(xml) + "...Adding DD using DDC tool" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var startInfo = new ProcessStartInfo();

            //var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_5_max_levels.xml"));
            var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
            startInfo.FileName = Path.Combine(AppWD, "..\\..\\ddc", "ddc.exe");

            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                startInfo.WorkingDirectory = Folder_Name + "\\EOF\\";// Path.GetDirectoryName();
            else
                startInfo.WorkingDirectory = Folder_Name + "\\songs\\arr\\";// Path.GetDirectoryName();

            startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2} {3}{4}{5}",//{6}
                                                Path.GetFileName(xml),
                                                4, "N", c, //r, 
                                                    " -p Y", " -t Y");
            //rtxt_StatisticsOnReadDLCs.Text = "working dir: "+ startInfo.WorkingDirectory + "...\n--"+startInfo.FileName+"..." +startInfo.Arguments + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            using (var DDC = new Process())
            {
                // rtxt_StatisticsOnReadDLCs.Text = "...1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                DDC.StartInfo = startInfo;
                DDC.Start();
                //consoleOutput = DDC.StandardOutput.ReadToEnd();
                //consoleOutput += DDC.StandardError.ReadToEnd();
                DDC.WaitForExit(1000 * 60 * 5); //wait 5 minutes
                                                // if (DDC.ExitCode > 0 ) rtxt_StatisticsOnReadDLCs.Text = "Issues when adding DD !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                DDAdded = "Yes"; //rtxt_StatisticsOnReadDLCs.Text = "DDAdded: " + DDAdded + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            }
            return DDAdded;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void DLCManager_Load(object sender, EventArgs e)
        {

        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            var xx = Path.Combine(AppWD, "MDBPlus.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");// Path.GetDirectoryName();
            startInfo.Arguments = String.Format(" " + DB_Path);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + DB_Path);
            }
        }

        private void chbx_CleanDB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_TempPath_TextChanged(object sender, EventArgs e)
        {

        }

        public static string RemoveDD(string Folder_Name, string Is_Original, string xml, Platform platform, bool superOrg, bool InternalLog)
        {
            var Has_BassDD = "No";

            var jsons = "";
            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                jsons = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
            else
                jsons = (xml.Replace(".xml", ".json").Replace("songs\\arr", calc_path(Directory.GetFiles(Folder_Name, "*.json", SearchOption.AllDirectories)[0])));

            //Save a copy
            if (!File.Exists(xml + ".old")) File.Copy(xml, xml + ".old", false);
            else File.Copy(xml, xml + ".old", true);
            var json = jsons;
            if (!File.Exists(json + ".old")) File.Copy(json, json + ".old", false);
            else { File.Copy(json + ".old", json, true); }//            return "Yes"; }

            //bcapirtxt_StatisticsOnReadDLCs.Text = "...."+Path.GetFileNameWithoutExtension(xml)+"...Removing DD using DDC tool" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var startInfo = new ProcessStartInfo();

            var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_dd_remover.xml"));
            var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
            startInfo.FileName = Path.Combine(AppWD, "..\\..\\ddc", "ddc.exe");
            startInfo.WorkingDirectory = Folder_Name;// +jsons;// "\\EOF\\";// Path.GetDirectoryName();
            startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2}{3}{4}",//{5}
                                                Path.GetFileName(xml),
                                                40, "N", r,// c,
                                                 " -p Y"//, " -t N"
            );
            //rtxt_StatisticsOnReadDLCs.Text = "working dir: "+ startInfo.WorkingDirectory + "...\n--"+startInfo.FileName+"..." +startInfo.Arguments + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            var DDCExitCode = 5;
            using (var DDC = new Process())
            {
                // rtxt_StatisticsOnReadDLCs.Text = "...1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                if (!InternalLog)//32. When removing DD use internal logic not DDC
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start();
                    //consoleOutput = DDC.StandardOutput.ReadToEnd();
                    //consoleOutput += DDC.StandardError.ReadToEnd();
                    DDC.WaitForExit(1000 * 60 * 5); //wait 5 minutes
                                                    //rtxt_StatisticsOnReadDLCs.Text = "HAS BASS=" + file.Has_BassDD + "...DDEXIT CODE: " + DDC.ExitCode + "----+-" + file.Folder_Name + "++++" + platform.version + "___" + RocksmithToolkitLib.GameVersion.RS2014 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                    //if (DDC.ExitCode > 0 && Is_Original == "No") rtxt_StatisticsOnReadDLCs.Text = "Issues at CDLC Bass DD removal!" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
                else DDCExitCode = 5;

                if (Is_Original == "Yes" || DDCExitCode == 5)
                { //http://code.google.com/p/rocksmith-custom-song-creator/issues/detail?id=60
                  //if (platform.version == RocksmithToolkitLib.GameVersion.RS2014)                                        
                  //{
                  //bcapirtxt_StatisticsOnReadDLCs.Text = "...Removing DD from Original using own logic" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                  // xml = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), SearchOption.AllDirectories);
                    if (xml.Length > 0)
                    {
                        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                        Song2014 xmlContent1 = Song2014.LoadFromFile(xml);
                        var manifestFunctions1 = new ManifestFunctions(platform.version);
                        var j = manifestFunctions1.GetMaxDifficulty(xmlContent1);
                        string textfile = File.ReadAllText(xml);

                        //for each timestamp in the xml file take the highest level entry
                        var fxml = File.OpenText(xml);
                        string tecst = "";
                        string line;
                        var header = "";
                        var footer = "";
                        //Read and Save Header
                        while ((line = fxml.ReadLine()) != null)
                        {
                            if (line.Contains("<levels")) break;
                            header += line + "\n";
                        }
                        header += "\n<levels count=\"1\">\n";
                        //level the maxdiff overall setting in the xml
                        var m = 1;
                        for (m = 1; m <= j; m++)
                        {
                            header = header.Replace("maxDifficulty=\"" + m + "\"", "maxDifficulty=\"0\"");
                        }

                        //rtxt_StatisticsOnReadDLCs.Text = "head done"+ header.Length + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //fxml.Close();

                        //fxml = File.OpenText(xml);
                        var v = 0; //difficulty level in the parsing
                        var diff = 0;
                        float[] timea = new float[10000]; //keeps the timestamp of each note
                        float[] timeb = new float[10000]; //keeps the timestamp of each anchor
                        string[] notes = new string[10000]; // keeps the full note details
                        string[] anchor = new string[10000]; // keeps the full note details
                        int[] lvla = new int[10000]; //keeps the level of the note&timestamp
                        int[] lvlb = new int[10000]; //keeps the level of the note&timestamp
                                                     //bool is_header = true; //to know when the header has been read and saved
                                                     //var l = 0; //storage counter in the array
                        float ts = 0; //timestamp parsed fro the <notes line
                        int ea = 0; //top end of the storage array notes
                        int eb = 0; //top end of the storage array anchor
                        bool UpdateT = false;
                        while ((line = fxml.ReadLine()) != null)
                        {
                            //header
                            //if (is_header) header = line + "\n";
                            if (line.Contains("<level difficulty=\""))
                            {
                                line = line.Replace("<level difficulty=\"", "").TrimStart();
                                line = line.Replace("\">", "");
                                try { diff = line.ToInt32(); }
                                catch
                                {
                                    MessageBox.Show("Errors at DD lvl READ removal"); //rtxt_StatisticsOnReadDLCs.Text = "Errors at DD READ removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                if (line != v.ToString())
                                {
                                    MessageBox.Show("Errors at DD removal");
                                    //rtxt_StatisticsOnReadDLCs.Text = "Errors at DD removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                                        
                                    break;
                                }
                                v++;
                                //  is_header = false;
                                //rtxt_StatisticsOnReadDLCs.Text = "level: " + v + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                continue;
                            }

                            //notes
                            if (line.Contains("<note time=\""))
                            {
                                tecst = (line.Replace("<note time=\"", "")).TrimStart();// ((line.Replace("<note time=\"", "")).TrimStart).IndexOf("\"\"")));
                                tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                try { ts = Convert.ToSingle(tecst); }
                                catch
                                {
                                    MessageBox.Show("Errors at DD time notes READ removal"); //rtxt_StatisticsOnReadDLCs.Text = "Errors at DD time removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "timesptamp: " + tecst + "-" + ts + "-" + v + "-" + ea + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                UpdateT = false;
                                for (m = 0; m < ea; m++)
                                {
                                    //if (tecst == "12.034") rtxt_StatisticsOnReadDLCs.Text = "time: " + m + "-" + timea[m] + ea + "-" + line + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (ts == timea[m])
                                    {
                                        if (v > lvla[m])
                                        {
                                            notes[m] = line.Replace("\">", "\" />");
                                            timea[m] = ts;
                                            lvla[m] = v;
                                            UpdateT = true;
                                        }
                                        break;
                                    }
                                    //else if (time[v]<1) time[v]=
                                }
                                if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                {
                                    notes[ea] = line.Replace("\">", "\" />");
                                    timea[ea] = ts;
                                    lvla[ea] = v;
                                    ea++;
                                }
                            }
                            //anchor
                            if (line.Contains("<anchor time=\""))
                            {
                                tecst = (line.Replace("<anchor time=\"", "")).TrimStart();// ((line.Replace("<note time=\"", "")).TrimStart).IndexOf("\"\"")));
                                tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                try { ts = Convert.ToSingle(tecst); }
                                catch
                                {
                                    MessageBox.Show("Errors at DD time anchor READ removal"); //rtxt_StatisticsOnReadDLCs.Text = "Errors at DD anchor removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "timesptamp: " + tecst + "-" + ts + "-" + v + "-" + ea + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                UpdateT = false;
                                for (m = 0; m < eb; m++)
                                {
                                    //rtxt_StatisticsOnReadDLCs.Text = "time: " + m+"-"+time[m] + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (ts == timeb[m])
                                    {
                                        if (v > lvlb[m])
                                        {
                                            anchor[m] = line;
                                            timeb[m] = ts;
                                            lvlb[m] = v;
                                            UpdateT = true;
                                        }
                                        break;
                                    }
                                    //else if (time[v]<1) time[v]=
                                }
                                if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                {
                                    anchor[eb] = line;
                                    timeb[eb] = ts;
                                    lvlb[eb] = v;
                                    eb++;
                                }
                            }
                            //if () ;
                            //"<note time=\"";
                            if (line.Contains("<notes>")) continue;
                            //anchor

                        }

                        //rtxt_StatisticsOnReadDLCs.Text = "content: " + ea + "-" +eb+"="+ v + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //reorder the storage array
                        var n = 0;
                        string no;
                        int lv;
                        float ti;
                        for (m = 0; m <= ea - 1; m++)
                        {
                            for (n = m + 1; n <= ea; n++)
                            {
                                if (timea[m] > timea[n]) //if TimeStamp is bigger reverse the order
                                {
                                    no = notes[n];
                                    ti = timea[n];
                                    lv = lvla[n];
                                    notes[n] = notes[m];
                                    timea[n] = timea[m];
                                    lvla[n] = lvla[m];
                                    notes[m] = no;
                                    timea[m] = ti;
                                    lvla[m] = lv;
                                }
                            }
                        }
                        //reorder the anchor storage array
                        for (m = 0; m <= eb - 1; m++)
                        {
                            for (n = m + 1; n <= eb; n++)
                            {
                                if (timeb[m] > timeb[n]) //if TimeStamp is bigger reverse the order
                                {
                                    no = anchor[n];
                                    ti = timeb[n];
                                    lv = lvlb[n];
                                    anchor[n] = anchor[m];
                                    timeb[n] = timeb[m];
                                    lvlb[n] = lvlb[m];
                                    anchor[m] = no;
                                    timeb[m] = ti;
                                    lvlb[m] = lv;
                                }
                            }
                        }
                        //add level & notes to the footer
                        footer += "    <level difficulty=\"0\">" + "\n" + "      <notes>" + "\n";
                        for (m = 0; m <= ea; m++)
                        {
                            footer += notes[m] + "\n";
                        }
                        footer += "	  </notes>\n      <chords />\n      <anchors>\n";
                        //add level & notes to the footer
                        for (m = 0; m <= eb; m++)
                        {
                            footer += anchor[m] + "\n";
                        }
                        footer += "      </anchors>" + "\n" + "      <handShapes />" + "\n" + "     </level>" + "\n" + "   </levels>" + "\n" + "</song>";
                        //rtxt_StatisticsOnReadDLCs.Text = "Saving..." + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                        fxml.Close();
                        File.WriteAllText(xml, header + footer);

                        //textfile = textfile.Replace("<heroLevels>", "");
                        //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"1\" />", "");
                        //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"2\" />", "");
                        //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"3\" />", "");
                        //textfile = textfile.Replace("</heroLevels>", "");
                        //textfile = textfile.Replace("<level difficulty=\""+0+"\">", "<level difficulty = \""+(j+1)+"\">");
                        //textfile = textfile.Replace("<level difficulty=\""+j+"\">", "<level difficulty=\"0\">");



                        //level the json as well
                        //var json = (xml.Replace("EOF", "Toolkit")).Replace(".xml", ".json");
                        textfile = File.ReadAllText(json);
                        n = 0;
                        for (n = 0; n < j; n++)
                        {
                            textfile = textfile.Replace("\"MaxPhraseDifficulty\": " + n + ",", "\"MaxPhraseDifficulty\": 0,");
                        }
                        File.WriteAllText(json, textfile);
                        //rtxt_StatisticsOnReadDLCs.Text = "...DD changes written to file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //}
                    }
                }
                Has_BassDD = "Yes";
                //rtxt_StatisticsOnReadDLCs.Text = "something..." + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
            }

            //remove altough original or t0o old dd
            platform.version = RocksmithToolkitLib.GameVersion.RS2014;
            Song2014 xmlContent = Song2014.LoadFromFile(xml);
            var manifestFunctions = new ManifestFunctions(platform.version);
            //manifestFunctions.GetMaxDifficulty(xmlContent) = "0";
            return Has_BassDD;

        }

        private void cbx_Groups_DropDown(object sender, EventArgs e)
        {
            //populaet the Group  Dropdown
            //DataSet ds = new DataSet();
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(ds, "Main");
            DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";");
            var norec = ds.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (cbx_Groups.Items.Count > 0)
                {
                    cbx_Groups.DataSource = null;
                    for (int k = cbx_Groups.Items.Count - 1; k >= 0; --k)
                    {
                        if (!cbx_Groups.Items[k].ToString().Contains("--"))
                        {
                            cbx_Groups.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                cbx_Groups.DataSource = null;
                for (int j = 0; j < norec; j++) //.ItemArray[j].ToString()
                {
                    var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    cbx_Groups.Items.Add(tem);
                }

            }
            //}
        }

        private void btn_GoImport_Click(object sender, EventArgs e)
        {
            string t = txt_RocksmithDLCPath.Text;
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void btm_GoTemp_Click(object sender, EventArgs e)
        {
            string t = txt_TempPath.Text;
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void btn_ProfilesSave_Click(object sender, EventArgs e)
        {
            //if (chbx_Configurations.SelectedIndex == 0)
            //{
            //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath1"] = txt_RocksmithDLCPath.Text;
            //    ConfigRepository.Instance()["dlcm_DBFolder1"] = txt_DBFolder.Text;
            //    ConfigRepository.Instance()["dlcm_TempPath1"] = txt_TempPath.Text;
            //    ConfigRepository.Instance()["dlcm_Prof1"] = chbx_Configurations.Text;
            //}
            //else if (chbx_Configurations.SelectedIndex == 1)
            //{
            //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath2"] = txt_RocksmithDLCPath.Text;
            //    ConfigRepository.Instance()["dlcm_DBFolder2"] = txt_DBFolder.Text;
            //    ConfigRepository.Instance()["dlcm_TempPath2"] = txt_TempPath.Text;
            //    ConfigRepository.Instance()["dlcm_Prof2"] = chbx_Configurations.Text;
            //}
            //else if (chbx_Configurations.SelectedIndex == 2)
            //{
            //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath3"] = txt_RocksmithDLCPath.Text;
            //    ConfigRepository.Instance()["dlcm_DBFolder3"] = txt_DBFolder.Text;
            //    ConfigRepository.Instance()["dlcm_TempPath3"] = txt_TempPath.Text;
            //    ConfigRepository.Instance()["dlcm_Prof3"] = chbx_Configurations.Text;
            //}
            //else if (chbx_Configurations.SelectedIndex == 3)
            //{
            //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath4"] = txt_RocksmithDLCPath.Text;
            //    ConfigRepository.Instance()["dlcm_DBFolder4"] = txt_DBFolder.Text;
            //    ConfigRepository.Instance()["dlcm_TempPath4"] = txt_TempPath.Text;
            //    ConfigRepository.Instance()["dlcm_Prof4"] = chbx_Configurations.Text;
            //}
            //else if (chbx_Configurations.SelectedIndex == 4)
            //{
            //    ConfigRepository.Instance()["dlcm_RocksmithDLCPath5"] = txt_RocksmithDLCPath.Text;
            //    ConfigRepository.Instance()["dlcm_DBFolder5"] = txt_DBFolder.Text;
            //    ConfigRepository.Instance()["dlcm_TempPath5"] = txt_TempPath.Text;
            //    ConfigRepository.Instance()["dlcm_Prof5"] = chbx_Configurations.Text;
            //}
            DataSet drs = new DataSet(); drs = DLCManager.SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\";");
            var norec = drs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"Profile\";");

                norec = ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string insertcmdA = "CDLC_ID, Groups, Type, Comments, Profile_Name";
                    var insertA = "\"" + fnn + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"Profile\",\"Rocksmith\",\"" + chbx_Configurations.Text + "\"";
                    DLCManager.InsertIntoDBwValues("Groups", insertcmdA, insertA);

                    insertA = "\"" + fnn + "\",\"" + txt_TempPath.Text + "\",\"Profile\",\"Temp\",\"" + chbx_Configurations.Text + "\"";
                    DLCManager.InsertIntoDBwValues("Groups", insertcmdA, insertA);

                    insertA = "\"" + fnn + "\",\"" + txt_DBFolder.Text + "\",\"Profile\",\"DB\",\"" + chbx_Configurations.Text + "\"";
                    DLCManager.InsertIntoDBwValues("Groups", insertcmdA, insertA);
                }
            }
            else MessageBox.Show("Please chose a unique name");
        }

        private void chbx_Rebuild_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_Rebuild.Checked) btn_PopulateDB.Text = "Import DLCs";
            else btn_PopulateDB.Text = "Rebuild DLC Main DB";
        }

        private void btn_OpenGame_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();
            //Game frm = new Game((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb", txt_TempPath.Text, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            //  frm.ShowDialog();
        }

        private void chbx_Configurations_DropDown(object sender, EventArgs e)
        {
            //populaet the Group  Dropdown
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(ds, "Main");

            DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Groups", "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";");
            var norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                //remove items
                if (chbx_Configurations.Items.Count > 0)
                {
                    chbx_Configurations.DataSource = null;
                    for (int k = chbx_Configurations.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_Configurations.Items[k].ToString().Contains("--"))
                        {
                            chbx_Configurations.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                chbx_Configurations.DataSource = null;
                for (int j = 0; j < norec; j++) //.ItemArray[j].ToString()
                {
                    var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    chbx_Configurations.Items.Add(tem);
                }
            }
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            var cmd = "DELETE FROM Groups WHERE Type=\"Profile\" AND Profile_Name= \"" + chbx_Configurations.Text + "\"";
            //DataSet dhs = new DataSet();
            DeleteFromDB("Groups", cmd);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chbx_XBOX360_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbx_Mac_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbx_PS3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbx_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_PC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtn_Population_All_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtn_Population_Selected_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Mask_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Artist_Sort_Click(object sender, EventArgs e)
        {

        }

        private void txt_Artist_Sort_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Artist_Click(object sender, EventArgs e)
        {

        }

        private void txt_Artist_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Title_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_File_Name_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Album_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Title_Sort_Click(object sender, EventArgs e)
        {

        }

        private void txt_File_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Album_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Title_Sort_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Title_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbx_Export_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_RocksmithDLCPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void chbx_CleanTemp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rtxt_StatisticsOnReadDLCs_TextChanged(object sender, EventArgs e)
        {

        }

        private void pB_ReadDLCs_Click(object sender, EventArgs e)
        {

        }

        private void lbl_RocksmithDLCPath_Click(object sender, EventArgs e)
        {

        }

        private void lbl_TempFolders_Click(object sender, EventArgs e)
        {

        }

        private void lbl_PreviewText_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void btm_GoDB_Click(object sender, EventArgs e)
        {
            string t = txt_DBFolder.Text;
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private string shortenfile_Name(string currn, string futuren)
        {
            Random random = new Random();
            var rt = random.Next(0, 100000);
            var dest = currn.Substring(0, currn.LastIndexOf("\\")) + "\\" + rt.ToString() + currn.Substring(currn.LastIndexOf("."));
            var fdes = futuren.Substring(0, futuren.LastIndexOf("\\")) + "\\" + rt.ToString() + futuren.Substring(futuren.LastIndexOf("."));

            if (fdes.Length < 260)
            {
                if (File.Exists(currn)) File.Move(currn, dest);
                return fdes;
            }
            else
            {
                MessageBox.Show("File " + currn + " " + futuren + " too big to rename");
                return currn;
            }
        }

        private void chbx_Configurations_SelectedIndexChanged(object sender, EventArgs e)
        {

            x4 = ConfigRepository.Instance()["dlcm_AdditionalManipul24"] == "Yes" ? true : false;
            x5 = ConfigRepository.Instance()["dlcm_AdditionalManipul15"] == "Yes" ? true : false;
            x9 = ConfigRepository.Instance()["dlcm_AdditionalManipul49"] == "Yes" ? true : false;
            //    if (chbx_Configurations.SelectedIndex == 0)
            //    {
            //        txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath1"];
            //        txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder1"];
            //        txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath1"];
            //    }
            //    else if (chbx_Configurations.SelectedIndex == 1)
            //    {
            //        txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath2"];
            //        txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder2"];
            //        txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath2"];
            //    }
            //    else if (chbx_Configurations.SelectedIndex == 2)
            //    {
            //        txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath3"];
            //        txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder3"];
            //        txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath3"];
            //    }
            //    else if (chbx_Configurations.SelectedIndex == 3)
            //    {
            //        txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath4"];
            //        txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder4"];
            //        txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath4"];
            //    }
            //    else if (chbx_Configurations.SelectedIndex == 4)
            //    {
            //        txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath5"];
            //        txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder5"];
            //        txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath5"];
            //    }t
            // var cmds = "SELECT Groups FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\"";
            DataSet ds = new DataSet(); ds = DLCManager.SelectFromDB("Groups", "SELECT Groups FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\"");
            var norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                txt_RocksmithDLCPath.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txt_DBFolder.Text = ds.Tables[0].Rows[2].ItemArray[0].ToString();
                txt_TempPath.Text = ds.Tables[0].Rows[1].ItemArray[0].ToString();
            }
        }
        static string CleanTitle(string st)
        {
            if (st.IndexOf("[") > 0) return st.Substring(0, st.IndexOf("["));
            return st;
            //if (tittle.IndexOf("]") > 0) CleanTitle += tittle.Substring(tittle.IndexOf("]"), tittle.Length - tittle.IndexOf("]"));
            //else if (tittle.IndexOf("[") == 0 || tittle.Substring(0, 1) != "[") CleanTitle = tittle;
        }
    }
}