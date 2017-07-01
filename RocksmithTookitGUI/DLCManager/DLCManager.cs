using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;//repack
using Ookii.Dialogs; //cue text
using RocksmithToTabLib;//for psarc browser
//using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;//regex
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
//Roolkit
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib;
using RocksmithToolkitLib.XmlRepository;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.Xml;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.DLCPackage.Manifest2014;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using RocksmithToolkitLib.Ogg;
using System.Threading.Tasks;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {
        //bcapi
        private bool loading = false;
        public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
        public BackgroundWorker bwConvert = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi1  
        private StringBuilder errorsFound;//bcapi1
        string dlcSavePath = "";
        int no_ord = 0;
        string Groupss = "";
        string netstatus = "NOK";
        DLCPackageData data;

        //Processing global vars
        bool duplit = false;
        int dupliNo = 0;
        int dupliPrcs = 0;
        int[] dupliSongs = new int[100000];
        bool stopp = false;
         static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
         static string MyAppWD = AppWD; //when removing DDC

        DateTime timestamp;
        string logPath = ConfigRepository.Instance()["dlcm_LogPath"];

        OleDbConnection connection;
        OleDbCommand command;

        private GenericFunctions.MainDBfields[] files = new GenericFunctions.MainDBfields[10000];
        private GenericFunctions.MainDBfields[] SongRecord = new GenericFunctions.MainDBfields[10000];

        //internal static string LabelTextt
        //{
        //    get
        //    {
        //        return lbl_NoRec2.Text;
        //    }
        //    set
        //    {
        //        lbl_NoRec2.Text = value;
        //    }
        //}

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

        public GameVersion CurrentGameVersion
        {
            get { return GameVersion.RS2014; }
            set { }
        }

        internal GenericFunctions.MainDBfields[] Fields
        {
            get
            {
                return SongRecord;
            }

            set
            {
                SongRecord = value;
            }
        }

        internal GenericFunctions.MainDBfields[] Files
        {
            get
            {
                return files;
            }

            set
            {
                files = value;
            }
        }
        public void RefreshSelectedStat(string db, string txt)
        {
            if (File.Exists(txt_DBFolder.Text + "\\Files.accdb"))
            {
                var SearchCmd = "SELECT * FROM Main u ";
                DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB(db, SearchCmd, txt_DBFolder.Text);
                var noOfRec = dsz1.Tables[0].Rows.Count;

                SearchCmd = "SELECT * FROM " + db + " u " + " WHERE " + txt + ";";
                DataSet dsz2 = new DataSet(); dsz2 = SelectFromDB(db, SearchCmd, txt_DBFolder.Text);

                var noOfSelRec = dsz2.Tables[0].Rows.Count;
                lbl_NoRec2.Text = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records.";
            }
        }

        public DLCManager()
        {
            InitializeComponent();

            //Enable Preview generation
            if (ConfigRepository.Instance()["general_wwisepath"] == "") ConfigRepository.Instance()["general_wwisepath"] = "C:\\Program Files (x86)\\Audiokinetic\\Wwise v2015.1.9 build 5624";// 2016.2.1.5995";
            if (ConfigRepository.Instance()["general_rs2014path"] == "") ConfigRepository.Instance()["general_rs2014path"] = "C:\\Program Files (x86)\\Steam\\Apps\\common\\Rocksmith2014";
            if (ConfigRepository.Instance()["general_defaultauthor"] == "") ConfigRepository.Instance()["general_defaultauthor"] = "catara";

            //C:\\Program Files (x86)\\Audiokinetic\\Wwise v2013.2.10 build 4884";
            //Populate Se3lectee songs
            RefreshSelectedStat("Main", "1=1");

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
            rbtn_Population_All.Checked = true;
            RepackP = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\PC" : (ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\XBOX360" : (ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\PS3" : (ConfigRepository.Instance()["dlcm_chbx_MAC"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\MAC" : "";
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
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul67"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(67, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(67, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul68"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(68, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(68, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(69, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(69, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(70, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(70, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul71"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(71, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(71, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul72"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(72, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(72, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(73, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(73, CheckState.Unchecked);

            // Generate package worker
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);

            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;

            bwConvert.DoWork += new DoWorkEventHandler(ConvertWEM);

            bwConvert.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwConvert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwConvert.WorkerReportsProgress = true;
            if (!File.Exists(txt_DBFolder.Text + "\\Files.accdb")) chbx_DefaultDB.Checked = true;
            if (!Directory.Exists(txt_TempPath.Text)) txt_TempPath.Text = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\Temp";
            SetImportNo();
        }

        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_RocksmithDLCPath.Text = temppath;
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
                chbx_Rebuild.Visible = true;
                chbx_Additional_Manipulations.Visible = true;
                rtxt_StatisticsOnReadDLCs.Visible = true;

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
                chbx_Additional_Manipulations.Visible = false;

                rtxt_StatisticsOnReadDLCs.Visible = false;

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
                lbl_Artist_Sort.Visible = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            SaveSettings();// Saving for later
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
            ConfigRepository.Instance()["dlcm_AdditionalManipul64"] = chbx_Additional_Manipulations.GetItemChecked(64) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul65"] = chbx_Additional_Manipulations.GetItemChecked(65) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul66"] = chbx_Additional_Manipulations.GetItemChecked(66) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul67"] = chbx_Additional_Manipulations.GetItemChecked(67) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul68"] = chbx_Additional_Manipulations.GetItemChecked(68) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul69"] = chbx_Additional_Manipulations.GetItemChecked(69) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul70"] = chbx_Additional_Manipulations.GetItemChecked(70) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul71"] = chbx_Additional_Manipulations.GetItemChecked(71) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul72"] = chbx_Additional_Manipulations.GetItemChecked(72) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul73"] = chbx_Additional_Manipulations.GetItemChecked(73) ? "Yes" : "No";

            //Save Profiles
            if (chbx_Configurations.SelectedIndex >= 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\";", txt_DBFolder.Text);
                var norec = ds.Tables[0].Rows.Count;
                if (norec == 3)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    var cmd = "UPDATE Groups SET Groups=\"" + txt_RocksmithDLCPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Rocksmith\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"";
                    UpdateDB("Groups", cmd);
                    UpdateDB("Groups", "UPDATE Groups SET Groups=\"" + txt_DBFolder.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"DB\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"");
                    UpdateDB("Groups", "UPDATE Groups SET Groups=\"" + txt_TempPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Temp\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"");
                }
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
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "Title: " + Manipulate_strings(txt_Title.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "Sort Title: " + Manipulate_strings(txt_Title_Sort.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "Artist: " + Manipulate_strings(txt_Artist.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "Sort Artist: " + Manipulate_strings(txt_Artist_Sort.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "Album: " + Manipulate_strings(txt_Album.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ");
            lbl_PreviewText.Text = "FileName: " + Manipulate_strings(txt_File_Name.Text, 0, true, false, false, SongRecord, "", "");
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

            DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT * FROM Main", txt_DBFolder.Text);
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
            SaveSettings();
            string cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + cbx_Groups.Text + "\")";
            //Read from DB
            int i = 1;
            GenericFunctions.MainDBfields[] SongRecord = new GenericFunctions.MainDBfields[10000];
            SongRecord = GenericFunctions.GetRecord_s(cmd);

            var norows = SongRecord[0].NoRec.ToInt32();
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = norows * 100;
            cmd = "DELETE FROM Main WHERE ID IN (";
            var ids = "";
            var hash = "";
            if (norows > 0)
                foreach (var song in SongRecord)
                {
                    if (song != null)
                    {
                        ids += song.ID.ToString();
                        hash += song.Original_File_Hash.ToString();
                        i++;
                        if (i <= norows) { ids += ", "; hash += ", "; }
                        if (i > norows) break;
                    }
                    pB_ReadDLCs.Increment(1);
                }
            cmd += ids + ");";
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";

            var TempPath = txt_TempPath.Text;
            DeleteRecords(ids, cmd, DB_Path, TempPath, norows.ToString(), hash);
        }

       

        public DateTime UpdateLog(DateTime dt, string txt, bool multith)
        {
            DateTime dtt = System.DateTime.Now;
            var ii = Math.Round((dt - dtt).TotalSeconds, 2).ToString();
            if (multith) rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            // Write the string to a file.
            var fn = (logPath == null || !Directory.Exists(logPath) ? MyAppWD : logPath) + "\\" + "current_temp.txt";
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(fn))
            {
                sw.WriteLine(dtt + " - " + ii + " - " + txt);
            }
            return dtt;
        }


        // Read a Folder (clean temp folder)        // Decompress the PC DLCs
        // Read details and populate a DB (clean Import DB before, and only populate Main if not there already)
        public void btn_PopulateDB_Click(object sender, EventArgs e)
        {
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            var tst = "Starting... " + startT; timestamp = UpdateLog(timestamp, tst, true);
            //var DB_Path = "";
            // var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";// "";

            var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";// "";
            var errr = true;
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            SaveSettings();
            tst = "end save settings ..."; timestamp = UpdateLog(timestamp, tst, true);

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
            tst = "end config reading..."; timestamp = UpdateLog(timestamp, tst, true);


            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !chbx_Additional_Manipulations.GetItemChecked(38)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {
                DialogResult result1 = MessageBox.Show("Are you sure you want to DELETE (to Recycle BIN) the following folders:\n\n" + txt_TempPath.Text + "\n0\\0_old\n0\\0_duplicate\n0\\0_repacked\n0\\0_broken\n" + log_Path + "\n0\\0_repacked\\PC\n0\\0_repacked\\PS3\n0\\0_repacked\\MAC\n0\\0_repacked\\XBOX360\n0\\dlcpacks\n0\\dlcpacks\\manifests\n0\\dlcpacks\\temp\n0\\dlcpacks\\manipulated", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                try
                {
                    //DirectoryInfo di;
                    if (result1 == DialogResult.Yes)
                    {
                        //clean app working folders 0 folder   //Delete Files
                        CleanFolder(txt_TempPath.Text, ".accdbb");
                        CleanFolder(txt_TempPath.Text + "\\0_old", "");
                        CleanFolder(txt_TempPath.Text + "\\0_repacked", "");
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\PC", "");
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\PS3", "");
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\MAC", "");
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\XBOX360", "");
                        CleanFolder(txt_TempPath.Text + "\\0_duplicate", "");
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manifests", "");
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks", "");
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\temp", "");
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manipulated", "");
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manipulated\\temp", "");
                        CleanFolder(broken_Path_Import, "");
                        CleanFolder(log_Path, "");
                        //Delete Folders
                        System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text);
                        foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                        {
                            try
                            {
                                if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate")
                                    //dir.Delete(true);
                                    DeleteDirectory(dir.FullName);
                            }
                            catch (Exception ex) { Console.Write(ex); }
                        }
                        System.IO.DirectoryInfo downloadedMessageInfo7 = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks");
                        foreach (DirectoryInfo dir in downloadedMessageInfo7.GetDirectories())
                        {
                            try
                            {
                                if (dir.Name != "temp" && dir.Name != "manipulated" && dir.Name != "manifests")
                                    //dir.Delete(true);
                                    DeleteDirectory(dir.FullName);
                            }
                            catch (Exception ex) { Console.Write(ex); }
                        }
                        //CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not delete folders:\n\n" + "0\n0\\0_old\n0\\0_duplicate\n0\\0_repacked\n0\\0_repacked\\PC\n0\\0_repacked\\PS3\n0\\0_repacked\\MAC\n0\\0_repacked\\XBOX360\n" + broken_Path_Import + "\n" + log_Path + "\n0\\dlcpacks\n0\\dlcpacks\\manifests\n0\\dlcpacks\\temp\n0\\dlcpacks\\manipulated");
                }
            }
            //if (!(File.Exists(txt_TempPath.Text) && File.Exists(txt_TempPath.Text + "\\0_old") && File.Exists(txt_TempPath.Text + "\\0_repacked") && File.Exists(txt_TempPath.Text + "\\0_repacked\\PC") && File.Exists(txt_TempPath.Text + "\\0_repacked\\PS3") && File.Exists(txt_TempPath.Text + "\\0_repacked\\XBBOX360") && File.Exists(txt_TempPath.Text + "\\0_repacked\\MAC") && File.Exists(txt_TempPath.Text + "\\0_duplicate") && File.Exists(txt_TempPath.Text + "\\0_dlcpacks") && File.Exists(broken_Path_Import) && File.Exists(log_Path)))
            //{
            //    DialogResult result2 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Cancel Import command\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (result2 == DialogResult.Yes)
            CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);
            //    else if (result2 == DialogResult.No) return;
            //    else Application.Exit();
            //}
            // Clean temp log
            var fnl = (logPath == null ? MyAppWD : logPath) + "\\" + "current_temp.txt";
            StreamWriter sw = new StreamWriter(File.OpenWrite(fnl));
            sw.Write("Some stuff here");
            sw.Dispose();

            tst = "end folder Cleaning..."; timestamp = UpdateLog(timestamp, tst, true);

            //help code
            //using (var u = new UpdateForm())
            //{
            //    u.Init(onlineVersion);
            //    u.ShowDialog();
            //}
            DataSet dsR = new DataSet();
            if (chbx_Rebuild.Checked)
            {
                string cmdR = @"SELECT * FROM Main as M;";
                if (rbtn_Population_Selected.Checked == true) cmdR += "WHERE Selected = \"Yes\"";
                else if (rbtn_Population_Groups.Checked) cmdR += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")";

                cmdR += " ORDER BY Artist";
                DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", cmdR, txt_DBFolder.Text);
                int noOfRecR = dsR.Tables[0].Rows.Count;
                tst = "Rebuilding" + noOfRecR + "/" + (noOfRecR) + " Songs already imported in MainDB"; timestamp = UpdateLog(timestamp, tst, true);
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                if (noOfRecR > 0)
                {
                    pB_ReadDLCs.Value = 0;
                    pB_ReadDLCs.Maximum = 2 * (noOfRecR - 1);
                }
                tst = "Rebuild select ended."; timestamp = UpdateLog(timestamp, tst, true);
            }

            //Clean ImportDB
            DeleteFromDB("Import", "DELETE FROM Import;");
            tst = "Cleaning....Import table...."; timestamp = UpdateLog(timestamp, tst, true);
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
                //DeleteFromDB("Groups", "DELETE FROM Groups WHERE TYPE=\"Profile\";");
                DeleteFromDB("Import_AuditTrail", "DELETE FROM Import_AuditTrail;");
                DeleteFromDB("Standardization", "DELETE FROM Standardization;");
                DeleteFromDB("Cache", "DELETE FROM Cache;");
                DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"DLC\" or Type=\"Retail\";");
            }

            tst = DB_Path + " Cleaned tables"; timestamp = UpdateLog(timestamp, tst, true);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            int i = 0;
            var ImportPackNo = "0";
            DataSet doz = new DataSet();
            doz = SelectFromDB("Main", "SELECT MAX(s.ID) FROM Main s;", txt_DBFolder.Text);
            var noOfRecx = doz.Tables[0].Rows.Count;
            int gh = doz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            if (noOfRecx > 0) ImportPackNo = (gh + 1).ToString();
            if (ImportPackNo == "" || ImportPackNo == "0") ImportPackNo = "1";

            if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                //GetDirList and calcualte hash for the IMPORTED file
                string[] filez;
                if (chbx_Additional_Manipulations.GetItemChecked(37)) //38. Import other formats but PC, as well(separately of course)
                    filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
                else
                    filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    if (s == "rs1compatibilitydisc_m.psarc" || s == "rs1compatibilitydisc_p_Pc.psarc" || s == "rs1compatibilitydlc_p.psarc" || s == "rs1compatibilitydlc_m.psarc") continue;
                    if (!s.IsValidPSARC())
                    {
                        errorsFound.AppendLine(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)));
                        continue;
                    }
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
                        timestamp = UpdateLog(timestamp, "error at import", true);
                        ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                        frm1.ShowDialog();
                        return;
                        //continue;
                    }
                    //- To remove usage of ee and loading
                    Console.WriteLine("{0} : {1} : {2}", fi.Name, fi.Directory, loading);

                    //details end

                    //Generating the HASH code
                    var FileHash = GetHash(s);

                    //Populate ImportDB
                    tst = "File " + (i + 1) + " :" + s; timestamp = UpdateLog(timestamp, tst, true);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    // DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";


                    var ff = "-";
                    ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;

                    var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform";
                    var plt = fi.FullName.GetPlatform().platform.ToString();
                    var insertvalues = "\"" + s + "\",\"" + fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\"" + FileHash + "\",\"" + fi.Length + "\",\"" + ff + "\",\"" + ImportPackNo + "\",\"" + (plt == "Pc" ? "0Pc" : plt) + "\"";
                    InsertIntoDBwValues("Import", insertcmdd, insertvalues);

                    pB_ReadDLCs.Increment(1);
                    i++;
                }
                tst = "end populating Import..."; timestamp = UpdateLog(timestamp, tst, true);

                //Delete duplicates(same HASH) from ImportDB
                var del = 0;
                var no = 0;
                DataSet dz = new DataSet(); dz = SelectFromDB("Import", "SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash = s.FileHash WHERE d.ID is not null GROUP BY s.FileHash;", txt_DBFolder.Text);
                no = dz.Tables[0].Rows.Count;
                //Remove duplicate DLCs from this Import
                DataSet drz = new DataSet(); drz = SelectFromDB("Import", "SELECT * FROM Import WHERE ID not IN(SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);", txt_DBFolder.Text);
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
                var noOfRecs = 0;
                var tzg = chbx_Additional_Manipulations.GetItemChecked(67);
                if (!chbx_Additional_Manipulations.GetItemChecked(67)) //68. Import duplicates(hash) //69. Delete obvious duplicates (hash) during dupli assesment
                {
                    DataSet dry = new DataSet(); dry = SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);", txt_DBFolder.Text);
                    noOfRecs = dry.Tables[0].Rows.Count;
                    if (noOfRecs > 0)
                    {
                        for (i = 0; i <= noOfRecs - 1; i++)
                            try
                            {
                                var fnn = dry.Tables[0].Rows[i].ItemArray[1].ToString();
                                if (File.Exists(txt_TempPath.Text + "\\0_duplicate\\" + fnn)) fnn = txt_TempPath.Text + "\\0_duplicate\\" + "\\0_duplicate\\" + dry.Tables[0].Rows[i].ItemArray[1].ToString().Replace(".psarc", "[Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "].psarc");
                                File.Move(dry.Tables[0].Rows[i].ItemArray[0].ToString(), fnn);
                                //var insertcmd = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                //var fnnon = Path.GetFileName(fnn);
                                //var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                //var insert = "Select top 1 i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, \"" + NullHandler(imported.Split(';')[0]) + "\" as DLC_ID, \"" + imported.Split(';')[1] + "\" as DLC_Name, \"" + fnn.GetPlatform().platform.ToString() + "\" as Platform FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                //InsertIntoDBwValues("Pack_AuditTrail", insertcmd, insert);
                            }
                            catch (IOException ee) { Console.Write(ee.Message); }
                        DeleteFromDB("Import", "DELETE FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);");
                    }
                    tst = "end delete same hash from Import..."; timestamp = UpdateLog(timestamp, tst, true);
                }

                //commented out as MARKING AS DUPLICATE SONGS ALTOUGH IMPORT WAS NOT FINISHED..OR MARKED MANUALLY AS DUPLICATE
                //insert Imoprt entries that are not in audit trail
                //string updatecmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate"; th
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


                ////Generate Pack No(Import No)
                //DataSet doz = new DataSet(); doz = SelectFromDB("Main", "SELECT MAX(s.ID),MAX(s.Pack) FROM Main s;");
                //var ImportPackNo = "0";
                //var noOfRecx = doz.Tables[0].Rows.Count;
                //if (noOfRecx > 0) ImportPackNo = doz.Tables[0].Rows[0].ItemArray[1].ToString()+1;
                //if (ImportPackNo == "") ImportPackNo = "1";

                //REmove
                noOfRecs = 0;
                if (!chbx_Additional_Manipulations.GetItemChecked(67)) //68. Import duplicates(hash)
                {
                    DataSet drh = new DataSet(); drh = SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);", txt_DBFolder.Text);

                    noOfRecs = drh.Tables[0].Rows.Count;

                    if (noOfRecs > 0)
                    {
                        for (i = 0; i <= noOfRecs - 1; i++)
                            File.Move(drh.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_duplicate\\" + drh.Tables[0].Rows[i].ItemArray[1].ToString());

                        //Delete Duplicates from Current Import
                        DeleteFromDB("Import", "DELETE * FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);");
                    }
                }

                tst = no + "/" + i + " Import files Inserted (excl. " + del + " duplicates)"; timestamp = UpdateLog(timestamp, tst, true);
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                tst = "end delete duplicates from curr import..."; timestamp = UpdateLog(timestamp, tst, true);
            }
            else  //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                //DataSet doz = new DataSet(); doz = SelectFromDB("Main", "SELECT MAX(s.ID),MAX(s.Pack) FROM Main s;");

                var ff = "-";
                ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;
                //var tz = "0";
                //var noOfRecx = doz.Tables[0].Rows.Count;
                //if (noOfRecx > 0) tz = doz.Tables[0].Rows[0].ItemArray[1].ToString();
                //if (tz == "") tz = "1";
                var pth = txt_TempPath.Text;
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(pth);

                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    var tzu = dir.ToString().IndexOf("0_");
                    if (dir.ToString().IndexOf("0_") == 0 || dir.ToString().IndexOf("ORIG") == 4 || dir.ToString().IndexOf("ORIG") == 3 || dir.ToString().IndexOf("CDLC") == 3 || dir.ToString().IndexOf("CDLC") == 4) continue;
                    //Populate ImportDB
                    tst = "Folder " + (i + 1) + " :" + "s"; timestamp = UpdateLog(timestamp, tst, true);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                    string insertcmds = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";
                    var pt = txt_RocksmithDLCPath.Text + "\\" + dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat") + ".psarc";
                    var pxt = dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat") + ".psarc";
                    System.IO.FileInfo ptz = null;
                    try
                    {
                        ptz = new System.IO.FileInfo(pt);
                    }
                    catch (System.IO.FileNotFoundException ee)
                    {
                        Console.WriteLine(ee.Message);
                    }

                    //var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";

                    var insertvals = "\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + dir.Name + "\",\"" + pxt + "\",\"" + ptz.CreationTime + "\",\"" + GetHash(pt) + "\",\"" + pt.Length + "\",\"" + DateTime.Now + "\",\"" + ImportPackNo + "\"";//,\"" + "0" + "\",\"";
                    //insertvals += System.DateTime.Today + "\",\""+ ImportPackNo + "\"";
                    InsertIntoDBwValues("Import", insertcmds, insertvals);
                }
                tst = "end create import based on already decompressed..."; timestamp = UpdateLog(timestamp, tst, true);
            }

            //START WITH mAINdb UPDATE
            var m = 0;
            //    {// 1. If hash already exists do not insert
            var cmd = "SELECT  FullPath, Path, FileName, FileHash, FileSize, ImportDate,i.Pack, i.FileCreationDate, i.ID FROM Import as i";//DISTINCT
            if (!chbx_Additional_Manipulations.GetItemChecked(67)) //68. Import duplicates(hash)
                cmd += " LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE i.ID is not NULL";
            cmd += " ORDER BY i.Platform ASC;";
            DataSet dns = new DataSet(); dns = SelectFromDB("Import", cmd, txt_DBFolder.Text);
            var tft = "\n Ignoring ";
            var noOfRec = dns.Tables[0].Rows.Count;
            if (chbx_Additional_Manipulations.GetItemChecked(29) && noOfRec > 0 && !chbx_Additional_Manipulations.GetItemChecked(67)) //30. When importing delete identical duplicates(same hash/filesize)
            {
                tft = "";
                for (m = 0; m < noOfRec; m++)
                {
                    var newf = dns.Tables[0].Rows[m].ItemArray[0].ToString().Replace(pathDLC, dupli_Path_Import);
                    tst = newf + "\n" + dns.Tables[0].Rows[m].ItemArray[0].ToString(); timestamp = UpdateLog(timestamp, tst, true);
                    if (chbx_Additional_Manipulations.GetItemChecked(29)) //&& !File.Exists(newf) //As the new file might have a different name e.g. (1) ....etc.
                    {
                        File.Copy(dns.Tables[0].Rows[m].ItemArray[0].ToString(), newf, true);
                        try
                        {
                            DeleteFile(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                            // File.Delete(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                        }
                        catch (Exception ex) { MessageBox.Show("Issues when moving to duplicate folder at import" + "-" + ex.Message + dns.Tables[0].Rows[m].ItemArray[0].ToString()); }
                        tft += "\n Deleting " + dns.Tables[0].Rows[m].ItemArray[0].ToString() + " as imported on " + dns.Tables[0].Rows[m].ItemArray[5].ToString();
                    }
                }
            }

            tst = tft + noOfRec + "/" + (noOfRec + m) + " already imported"; timestamp = UpdateLog(timestamp, tst, true);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));


            DataSet ds = new DataSet(); ds = SelectFromDB("Main", cmd, txt_DBFolder.Text);
            noOfRec = ds.Tables[0].Rows.Count;
            tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            if (noOfRec > 0)
            {
                //Move duplicates to the end
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Maximum = 2 * (noOfRec - 1);
                duplit = false;
                dupliNo = 0;
                dupliPrcs = 0;
                if (chbx_Additional_Manipulations.GetItemChecked(41))
                {
                    netstatus = ActivateSpotify_ClickAsync().Result.ToString();
                }

                for (var j = 0; j < 10000; j++) { dupliSongs[j] = 0; }

                for (var j = 0; j <= 1; j++)
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        if (!(j == 1 && dupliSongs[i] == 0))
                        {
                            if (j == 1 && dupliSongs[i] == 1) dupliPrcs++;
                            duplit = false;
                            tst = (j == 0 ? "" : "Duplicates: ") + (j == 0 ? (i + 1) : dupliPrcs) + "/" + (j == 0 ? noOfRec : dupliNo);

                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();


                            //to return: dupliSongs[i] = 1; duplit = true; dupliNo++

                            Random randomp = new Random();
                            int packid = randomp.Next(0, 100000);
                            timestamp = UpdateLog(timestamp, tst + " " + FullPath, true);
                            //Add text over progress bar
                            pB_ReadDLCs.CreateGraphics().DrawString(tst + " " + FullPath.Replace(Path.GetDirectoryName(FullPath) + "\\", "")
                                , new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                            errr = false;
                            if (!chbx_Additional_Manipulations.GetItemChecked(37))
                                if (!FullPath.IsValidPSARC())
                                {
                                    MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                                    {
                                        File.Copy(FullPath.Replace(".psarc", ".invalid"), Pathh, true);
                                        DeleteFile(FullPath.Replace(".psarc", ".invalid"));
                                        //File.Delete(FullPath.Replace(".psarc", ".invalid"));
                                    }
                                    errr = true;
                                    timestamp = UpdateLog(timestamp, "FAILED2 @Import cause of extension issue but copied in the broken folder", true);

                                    UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);
                                    continue;
                                }

                            var unpackedDir = "";
                            var packagePlatform = FullPath.GetPlatform();
                            if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                            {
                                //var fgf = ConfigRepository.Instance()["general_wwisepath"] + "\\Authoring\\Win32\\Release\\bin\\Wwise.exe";
                                //var wwiseCLIPath = Wwise.GetWwisePath();
                                //if (!File.Exists(fgf))//Help\\WwiseHelp_en.chm"))//
                                //{
                                var wwisePath = "";
                                if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                                    wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                                else
                                    wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                                if (wwisePath == "")
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2016.2.1.5995 with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                                    frm1.ShowDialog();
                                    if (frm1.IgnoreSong) break;
                                    if (frm1.StopImport) { j = 10; i = 9999; break; }
                                }
                                try
                                {
                                    // UNPACK
                                    tst = "start unpacking..."; timestamp = UpdateLog(timestamp, tst, true);
                                    if (chbx_Additional_Manipulations.GetItemChecked(51))
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, null);
                                    else
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, false, null);
                                    tst = "end unpacking..."; timestamp = UpdateLog(timestamp, tst, true);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unpacking ..." + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import, true);
                                    errr = false;
                                    try
                                    {
                                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                                        {
                                            File.Copy(FullPath, Pathh, true);
                                            DeleteFile(FullPath);
                                            //File.Delete(FullPath);
                                        }
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst);

                                        errr = true; //bcapi???
                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        timestamp = UpdateLog(timestamp, "FAILED2" + ee.Message, true);
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }
                            else unpackedDir = FullPath;

                            //Commenting Reorganize as they might have fixed the incompatib char issue
                            // REORGANIZE
                            //System.Threading.Thread.Sleep(1000);
                            var platform = FullPath.GetPlatform();
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
                                    var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories);
                                    foreach (var json in jsonFiles)
                                    {
                                        if (Path.GetFileNameWithoutExtension(json).ToUpperInvariant().Contains("VOCAL"))
                                            continue;
                                        if (platform.version == RocksmithToolkitLib.GameVersion.RS2014)
                                        {
                                            var jsons = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), System.IO.SearchOption.AllDirectories);
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
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at reorg" + unpackedDir + "---", true);

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
                                            File.Copy(FullPath, Pathh, true);
                                            DeleteFile(FullPath);
                                            //File.Delete(FullPath);
                                        }
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                                        errr = true;
                                        timestamp = UpdateLog(timestamp, "FAILED2 @org but copied in the broken folder" + ee.Message, true);
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }

                            stopp = false;
                            tst = "start processing..."; timestamp = UpdateLog(timestamp, tst, true); var songt = timestamp;

                            string imported = Processing(i, j, tst, FullPath, DB_Path, errr, broken_Path_Import, ds, Temp_Path_Import, dupli_Path_Import, old_Path_Import, cmd, unpackedDir, packid, false);
                            tst = "end processing..." + (songt - DateTime.Now); timestamp = UpdateLog(timestamp, tst, true);
                            if ((stopp)) break;
                            else
                            if (imported != "0")// && imported != "ignored")
                            {
                                string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
                                var insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA);

                                if (!((imported.ToLower()).IndexOf("ignore") >= 0))// || (imported.ToLower().IndexOf("update") >= 0)
                                {
                                    //Generating the HASH code
                                    var FileHash = ""; var fpath = "";
                                    if (chbx_Additional_Manipulations.GetItemChecked(15)) fpath = FullPath.Replace(txt_RocksmithDLCPath.Text, old_Path_Import);
                                    else fpath = FullPath;

                                    FileHash = GetHash(fpath);

                                    System.IO.FileInfo fi = null; //calc file size
                                    try { fi = new System.IO.FileInfo(fpath); }
                                    catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

                                    insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
                                    var fnnon = Path.GetFileName(fpath);
                                    var packn = FullPath.Substring(0, fpath.IndexOf(fnnon));
                                    insertA = "Select top 1 i.FullPath, \"" + old_Path_Import + "\", i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, \"" + NullHandler(imported.Split(';')[0]) + "\" as DLC_ID, \"" + imported.Split(';')[1] + "\" as DLC_Name, \"" + fpath.GetPlatform().platform.ToString() + "\" as Platform,\"" + Is_Original + "\" as Official FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                                    //fs.Close();
                                    tst = "end _AuditTrailing..."; timestamp = UpdateLog(timestamp, tst, true);
                                }
                            }
                            //else (imported == "ignored")
                        }
                    }
            }
            var endI = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;
            timestamp = UpdateLog(timestamp, "Ended import" + endI, true);

            //Cleanup
            if (chbx_Additional_Manipulations.GetItemChecked(24)) //25. Use translation tables for naming standardization
            {
                tst = "Applying Standardizations";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                Translation_And_Correction((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text));
                tst = "";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                tst = "end Standardization applying..."; timestamp = UpdateLog(timestamp, tst, true);
            }

            SetImportNo();

            //Show Intro database window
            MainDB frm = new MainDB((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), txt_TempPath.Text, chbx_Additional_Manipulations.GetItemChecked(33)
                , txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40));
            frm.Show();
            var endT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            if (chbx_Additional_Manipulations.GetItemChecked(42)) //43. Save import Log
            {
                // Write the string to a file.
                var fn = (log_Path == null ? MyAppWD : log_Path) + "\\" + GetTimestamps(DateTime.Now).Replace(":", "_") + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, true))
                {
                    file.WriteLine("Full Log");
                    file.WriteLine(rtxt_StatisticsOnReadDLCs.Text);
                    file.Close();
                }

                timestamp = UpdateLog(timestamp, "Log saved", true);
            }
            string endtmp = (starttmp - DateTime.Now).ToString();
            timestamp = UpdateLog(timestamp, "The End " + endT + " (" + startT + ") after " + endtmp, true);
        }


        string Check4MultiT(string origFN, string noMFN, string text)
        {
            var FN = origFN.ToLower();
            var ST = noMFN.ToLower();
            text = text.ToLower();
            var retrn = "";
            if (origFN.ToLower().IndexOf(text) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0
                || noMFN.ToLower().IndexOf(text) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0)
            {
                noMFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(noMFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                origFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(origFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                retrn = noMFN + ";" + ((FN != origFN || noMFN != ST) ? "Yes" : "No");
                return retrn;
            }
            retrn = noMFN + ";" + "No";
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
            var DD = "No";
            var Bass_Has_DD = "No";
            var sect1on = "Yes";
            var artist = "Insert";
            DLCPackageData info = null;
            var IDD = "";


            if (!errr)
            {
                //FIX for adding preview_preview_preview
                if (unpackedDir == "")
                {
                    unpackedDir = "C:\\GitHub\\tmp\\0\\dlcpacks\\songs_Pc";
                    timestamp = UpdateLog(timestamp, "Issues at decompressing WEMs or FAILED2 empty path", true);
                }
                // LOAD DATA

                try
                {
                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported", true);
                    Console.WriteLine(ee.Message);
                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                    {
                        File.Copy(FullPath, Pathh, true);
                        DeleteFile(FullPath);
                        //File.Delete(FullPath);
                    }
                    UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                    timestamp = UpdateLog(timestamp, "FAILED2 @Load but copied in the broken folder" + ee.Message, true);
                    Console.WriteLine(ee.Message);
                    return "0";
                }
                tst = "end Loading song from folder..."; timestamp = UpdateLog(timestamp, tst, true);
                //Enable input of
                info.SongInfo.Artist = CleanTitle(info.SongInfo.Artist);
                info.SongInfo.SongDisplayName = CleanTitle(info.SongInfo.SongDisplayName);
                info.SongInfo.ArtistSort = CleanTitle(info.SongInfo.ArtistSort);
                info.SongInfo.SongDisplayNameSort = CleanTitle(info.SongInfo.SongDisplayNameSort);
                info.SongInfo.Album = CleanTitle(info.SongInfo.Album);
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
                    if (ff != info.SongInfo.Artist) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Artist...", true);
                    if (gg != info.SongInfo.ArtistSort) timestamp = UpdateLog(timestamp, "removed potential illegally characters \\,\",/,?,: from ArtistSort...", true);
                    if (hhh != info.SongInfo.SongDisplayName) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Title...", true);
                    if (jj != info.SongInfo.SongDisplayNameSort) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from TitleSort...", true);
                    if (kk != info.SongInfo.Album) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Album...", true);
                }
                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                {
                    info.SongInfo.ArtistSort = info.SongInfo.Artist;
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                }
                if (chbx_Additional_Manipulations.GetItemChecked(22) && info.SongInfo.ArtistSort.Length > 4) //23. Import with the The/Die only at the end of Title Sort     
                {
                    if (chbx_Additional_Manipulations.GetItemChecked(20) && info.SongInfo.SongDisplayNameSort.Length > 4)
                        info.SongInfo.SongDisplayNameSort = MoveTheAtEnd(info.SongInfo.SongDisplayNameSort);
                    info.SongInfo.ArtistSort = MoveTheAtEnd(info.SongInfo.ArtistSort);
                }

                timestamp = UpdateLog(timestamp, "\n Song " + (i + 1) + ": " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName, true);
                tst = "end text cleanup and standardization"; timestamp = UpdateLog(timestamp, tst, true);
                pB_ReadDLCs.Increment(1);

                //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                var xmlFiles = Directory.GetFiles(unpackedDir + "\\songs", "*.xml", System.IO.SearchOption.AllDirectories);
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

                    platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                    Song2014 xmlContent = null;
                    try
                    {
                        xmlContent = Song2014.LoadFromFile(xml);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported" + "----", true);
                        Console.WriteLine(ee.Message);
                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                        {
                            File.Copy(FullPath, Pathh, true);
                            DeleteFile(FullPath);
                            //File.Delete(FullPath);
                        }
                        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                        timestamp = UpdateLog(timestamp, "FAILED2 @XML parse but copied in the broken folder" + ee.Message + "----", true);
                        Console.WriteLine(ee.Message);
                        continue;
                    }

                    var manifestFunctions = new ManifestFunctions(platform.version);
                    //Get sections and lastconvdate
                    var json = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(xml)), System.IO.SearchOption.AllDirectories);
                    if (json.Length > 0)//&& g==1
                    {
                        foreach (var fl in json)
                        {
                            if (Path.GetFileNameWithoutExtension(fl).ToLower().Contains("bass") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("rhythm") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("combo"))
                            {
                                var attr = Manifest2014<Attributes2014>.LoadFromFile(fl).Entries.First().Value.First().Value;
                                manifestFunctions.GenerateSectionData(attr, xmlContent);
                                if (attr.Sections.Count < 2) sect1on = "No";
                                else sect1on = "Yes" + attr.Sections.Count.ToString();
                                clist.Add(attr.LastConversionDateTime);
                                dlist.Add((attr.Sections.Count > 0 ? "Yes" + attr.Sections.Count : "No"));
                            }
                            else
                            {
                                timestamp = UpdateLog(timestamp, "no section/lastconvdate", true);
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
                tst = "end xml readout..."; timestamp = UpdateLog(timestamp, tst, true);

                // READ ARRANGEMENTS
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
                //var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories); //Get directory of JSON files in case song dir is not ORGANIZED :)
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
                    var s1 = arg.SongXml.File;
                    alist.Add(GetHash(s1));

                    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                        s1 = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                    else
                        s1 = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                    blist.Add(GetHash(s1));

                }
                //Check Tones
                var Tones_Custom = "No";
                foreach (var tn in info.TonesRS2014)
                {
                    if (tn.IsCustom)
                        Tones_Custom = "Yes";
                }
                tst = "end Arrangements and Tones readout..."; timestamp = UpdateLog(timestamp, tst, true);


                var alt = "";
                var trackno = -1;
                var SpotifySongID = "";
                var SpotifyArtistID = "";
                var SpotifyAlbumID = "";
                var SpotifyAlbumURL = "";

                Is_MultiTrack = "";
                MultiTrack_Version = "";

                //Get Author and Toolkit version
                var versionFile = Directory.GetFiles(unpackedDir, "toolkit.version", System.IO.SearchOption.AllDirectories);
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
                else if (author != "") Has_author = "Yes";
                if (chbx_Additional_Manipulations.GetItemChecked(57))
                {
                    if (author == "Custom Song Creator") Has_author = "No";
                    author = author.Replace("Custom Song Creator", "");
                }

                if (versionFile.Length <= 0) Is_Original = "Yes";
                else Is_Original = "No";

                //Get Version from FileName
                var import_path = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                var original_FileName = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                string txt = original_FileName;

                int vpos = (txt.IndexOf("_v")) + 2;
                string major = "";
                string minor = "";

                if (info.ToolkitInfo.PackageVersion != "" && (info.ToolkitInfo.PackageVersion != null)) if (info.ToolkitInfo.PackageVersion.Length > 2) if (info.ToolkitInfo.PackageVersion.Substring(info.ToolkitInfo.PackageVersion.Length - 2, 2) == ".0") info.ToolkitInfo.PackageVersion = info.ToolkitInfo.PackageVersion.Substring(0, info.ToolkitInfo.PackageVersion.Length - 2);

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
                    if (info.ToolkitInfo.PackageVersion != null) { if (Convert.ToSingle(info.ToolkitInfo.PackageVersion.Replace("_", ".")) < Convert.ToSingle(ver)) info.ToolkitInfo.PackageVersion = ver; }
                    else info.ToolkitInfo.PackageVersion = ver;
                }
                tst = "end song details readout like v ersion author and tk versioning..."; timestamp = UpdateLog(timestamp, tst, true);

                //example of properly working with sql
                // Command to Insert Records
                //OleDbCommand cmdInsert = new OleDbCommand();
                //cmdInsert.CommandText = "INSERT INTO AutoIncrementTest (Description) VALUES (?)";
                //cmdInsert.Connection = cnJetDB;
                //cmdInsert.Parameters.Add(new OleDbParameter("Description", OleDbType.VarChar, 40, "Description"));
                //oleDa.InsertCommand = cmdInsert;


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
                string Titl, gom = noMFN;

                var multxt = "No Guitar"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
                multxt = "No Band"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
                multxt = "No Band Audio"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
                multxt = "Lead"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "Only Lead"; }
                multxt = "No Lead"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "Lead Only"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "Only Lead"; }
                multxt = "Only Lead"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "No Bass"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "No Bass Audio"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "No Bass"; }
                multxt = "Bass Only"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "Only Bass"; }
                multxt = "Only Bass"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "No Rhythm"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "Only Rhythm"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "Rhythm Only"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "Only Rhythm"; }
                multxt = "(Only BackTrack)"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "backing"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backingtrack"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backtrack"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backing only"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backing audio only"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "backing track"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "Only Band"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
                multxt = "No Vocal"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
                multxt = "Only Vocal"; Titl = Check4MultiT(origFN, noMFN, multxt); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; gom = Titl.Split(';')[0]; MultiTrack_Version = multxt; }

                //Remove MultiTrackLive Info from Title
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes") info.SongInfo.SongDisplayName = gom.TrimEnd().TrimStart().Replace(" ()", "");
                if (Is_MultiTrack == "Yes") timestamp = UpdateLog(timestamp, "Multitrack=-=" + MultiTrack_Version, true);
                tst = "end multitrackcheckin..."; timestamp = UpdateLog(timestamp, tst, true);

                var IsLive = "";
                var IsAcoustic = "";
                var LiveDetails = "";
                multxt = "Live"; Titl = Check4MultiT(origFN, info.SongInfo.SongDisplayName, multxt);
                if ("Yes" == Titl.Split(';')[1])
                { IsLive = "Yes"; gom = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, info.SongInfo.SongDisplayName.IndexOf(multxt) + 4), ""); }
                multxt = "Acoustic"; Titl = Check4MultiT(origFN, info.SongInfo.SongDisplayName, multxt);
                if ("Yes" == Titl.Split(';')[1])
                { IsAcoustic = "Yes"; gom = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, info.SongInfo.SongDisplayName.IndexOf(multxt) + 4), ""); }

                //Remove MultiTrackLive Info from Title
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes") info.SongInfo.SongDisplayName = gom.TrimEnd().TrimStart().Replace(" ()", ""); //(Regex.Replace(noMFN, "( audio)", "", RegexOptions.IgnoreCase)).TrimEnd().TrimStart().Replace(" ()", "");
                else info.SongInfo.SongDisplayName = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(info.SongInfo.SongDisplayName, "(No.)", "[No.]"), "(Backing.)", "[Backing.]"), "(Only.)", "[Only.]"), "(Live.)", "[Live.]"), "(Acoustic.)", "[Acoustic.]");

                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                {
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                }

                //Get TrackNo
                trackno = 0;
                if (chbx_Additional_Manipulations.GetItemChecked(41) && netstatus == "OK")
                {
                    //ActivateSpotify_ClickAsync();
                    Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                    //string s = sptyfy.Result.ToString();
                    //string ert = "";
                    //ert=s.Split(';')[0].ToString();
                    trackno = sptyfy.Result.Split(';')[0].ToInt32();
                    SpotifySongID = sptyfy.Result.Split(';')[1];
                    SpotifyArtistID = sptyfy.Result.Split(';')[2];
                    SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                    SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                    // GetTrackNoSpotifyAsync(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName));
                }
                ExistingTrackNo = "";
                tst = "end get track no from spotify..."; timestamp = UpdateLog(timestamp, tst, true);

                //Generating the HASH code
                art_hash = "";
                string audio_hash = "";
                string audioPreview_hash = "";
                AlbumArtPath = info.AlbumArtPath;
                string ss = "";

                if (AlbumArtPath != "")
                {
                    art_hash = GetHash(AlbumArtPath);
                    //convert to png
                    ExternalApps.Dds2Png(AlbumArtPath);
                }
                ss = info.OggPath;
                audio_hash = GetHash(ss);

                ss = info.OggPreviewPath;
                audioPreview_hash = GetHash(ss);
                tst = "end gen hashcodes..."; timestamp = UpdateLog(timestamp, tst, true);

                //Check if CDLC have already been imported (hash key)
                // 1. If hash already exists do not insert
                // 2. If hash does not exists then:
                // 2.1.1 If Artist+Album+Title or dlcname exists check author. If same check version
                // 2.1.1.1 If (Artist+Album+Title or dlcname)+author the same check version If bigger add
                // 2.1.1.2 If (Artist+Album+Title or dlcname)+author the same check version If smaller ignore
                // 2.1.1.3 If (Artist+Album+Title or dlcname)+author the same check version If same ?
                // 3.1 If (Artist+Album+Title or dlcname) exists check author. If the not the same add as alternate
                // 3.2 If (Artist+Album+Title or dlcname) exists check author. If empty/generic(Custom Song Creator) show statistics and add as give choice to alternate or ignore
                // 4. IF filenames are the same 
                //SELECT if the same artist, album, songname
                var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND ";
                sel += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                sel += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\")";
                sel += "OR LCASE(Original_FileName) =LCASE(\"" + original_FileName + "\")";
                sel += "ORDER BY Is_Original ASC";
                //Read from DB
                SongRecord = GenericFunctions.GetRecord_s(sel);
                var norows = SongRecord[0].NoRec.ToInt32();

                var selduo = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND ";
                selduo += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                selduo += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                selduo += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\" AND Is_Original=\"Yes\")";
                GenericFunctions.MainDBfields[] SongRecord2 = new GenericFunctions.MainDBfields[10000];
                SongRecord2 = GenericFunctions.GetRecord_s(selduo);
                var norowsduo = SongRecord2[0].NoRec.ToInt32();

                var b = 0;
                artist = "Insert";
                string jk = ""; string k = "";
                IDD = "";
                var folder_name = "";
                var DLCC = "";
                var Platformm = "";
                var filename = "";
                var oldfilehas = "";
                var bitrate = 0;
                var SampleRate = 0;
                var HasOrig = "";
                //var SpotifySong_ID = "";
                //var SpotifyArtist_ID = "";
                //var SpotifyAlbum_ID = "";
                //var SpotifyAlbum_URL = "";
                bool newold = chbx_Additional_Manipulations.GetItemChecked(32);
                Random random = new Random();
                if (norows > 0)
                    foreach (var file in SongRecord)
                    {
                        SongDisplayName = "";
                        Namee = "";
                        if (b >= norows) break;
                        folder_name = file.Folder_Name;
                        filename = original_FileName;// file.Current_FileName;
                        IDD = file.ID; //Save Id in case of update or asses-update
                        DLCC = info.Name;
                        Platformm = (import_path + "\\" + original_FileName).GetPlatform().platform.ToString();

                        //When importing a original when there is already a similar CDLC
                        if (author == "" && tkversion == "" && chbx_Additional_Manipulations.GetItemChecked(14) && norowsduo >= 1 && (file.Is_Original != "Yes"))
                        {
                            artist = "Insert";

                            //Generate MAX Alternate NO
                            var fdf = (sel.Replace("ORDER BY Is_Original ASC", "")).Replace("SELECT *", "SELECT max(Alternate_Version_No)");
                            DataSet ddzv = new DataSet(); ddzv = SelectFromDB("Main", (sel.Replace("ORDER BY Is_Original ASC", "")).Replace("SELECT *", "SELECT max(Alternate_Version_No)"), txt_DBFolder.Text);
                            //UPDATE the 1(s) not an alternate already
                            int max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString() != "" ? ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1;
                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Song_Title = Song_Title +\" a." + max + "\", Song_Title_Sort = Song_Title_Sort+\" a." + max + "\", Is_Alternate = \"Yes\", Alternate_Version_No=" + max + " where ID in (" + sel.Replace("*", "ID") + ") and Is_Alternate=\"No\"" + ";");

                            //Add also a random DLCName if any of the Alternates has the same DLC Name as the original
                            DataSet dxf = new DataSet(); dxf = UpdateDB("Main", "UPDATE Main SET DLC_Name = DLC_Name+\"" + random.Next(0, 100000) + "\" WHERE ID in (" + sel.Replace("*", "ID") + ") and LCASE(DLC_Name) = \"" + info.Name.ToLower() + "\"" + ";");
                            break;
                        }

                        //calculate the alternative no (in case is needed)
                        var altver = "";
                        if (info.SongInfo.SongDisplayName.ToLower().IndexOf("lord") >= 0)
                            altver = "";
                        sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + CleanTitle(info.SongInfo.Artist) + "\") AND ";// LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                        sel += "(LCASE(Song_Title)=LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayName) + "\") OR ";
                        sel += "LCASE(Song_Title) like \"%" + CleanTitle(info.SongInfo.SongDisplayName.ToLower()) + "%\" OR ";
                        sel += "LCASE(Song_Title_Sort) =LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayNameSort) + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                        //Get last inserted ID
                        DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, txt_DBFolder.Text);

                        var altvert = dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 1 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                        if (Is_Original == "No") altver = (altvert + 1).ToString();

                        var fsz = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                        Title_Sort = "";
                        ArtistSort = "";
                        Artist = "";
                        Is_Alternate = "";
                        Alternate_No = "";
                        Album = "";
                        PackageVersion = "";
                        bitrate = 0;
                        SampleRate = 0;
                        HasOrig = "";

                        if (Rebuild) artist = "Insert"; //At Rebuild ignore duplicates
                        else
                        if (chbx_Additional_Manipulations.GetItemChecked(68))
                            if (ds.Tables[0].Rows[i].ItemArray[3].ToString() == file.File_Hash) { artist = "Ignore"; timestamp = UpdateLog(timestamp, "IGNORED as already imported(hash)", true); }
                        if (artist != "Ignore")
                            if ((author.ToLower() == file.Author.ToLower() && author != "" && file.Author != "" && file.Author != "Custom Song Creator" && author != "Custom Song Creator") || (file.DLC_Name == info.Name))
                            {
                                if (file.DLC_Name.ToLower() == info.Name.ToLower())
                                    if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                    {
                                        dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                    }
                                    else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive, LiveDetails, IsAcoustic, HasOrig);
                                else
                                {
                                    if (file.Version.ToInt32() > info.ToolkitInfo.PackageVersion.ToInt32()) artist = "Update";
                                    if (file.Version.ToInt32() < info.ToolkitInfo.PackageVersion.ToInt32())
                                        if (file.Is_Alternate != "Yes") { artist = "Ignore"; timestamp = UpdateLog(timestamp, "IGNORED as", true); }
                                        else if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                        {
                                            dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                        }
                                        else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive, LiveDetails, IsAcoustic, HasOrig);
                                    if (file.Version.ToInt32() == info.ToolkitInfo.PackageVersion.ToInt32())
                                        if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                                        {
                                            dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                                        }
                                        else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive, LiveDetails, IsAcoustic, HasOrig);
                                    else { artist = "Ignore"; timestamp = UpdateLog(timestamp, "IGNORED", true); }
                                    // assess=alternate, update or ignore//as maybe a new package(ing) is desired to be inserted in the DB
                                }
                            }
                            else if (author.ToLower() != file.Author.ToLower() && (author != "" && author != "Custom Song Creator" && file.Author != "Custom Song Creator" && file.Author != "")) artist = "Alternate";
                            else if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0)
                            {
                                dupliSongs[i] = 1; duplit = true; dupliNo++; break;
                            }
                            else artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver, fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, platformTXT.ToString(), IsLive, LiveDetails, IsAcoustic, HasOrig);
                        //Exit condition
                        tst = "end check for dupli..."; timestamp = UpdateLog(timestamp, tst, true);
                        if (artist == "Stop")
                        {
                            j = 10000;
                            i = 10000;
                            stopp = true;
                            break;
                        }

                        if (artist == "Alternate")
                        {
                            if (alt == "") alt = "1";
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
                            if (PackageVersion != "") info.ToolkitInfo.PackageVersion = PackageVersion;
                            artist = "Insert";

                            //Get the higgest Alternate Number                            
                            if (file.DLC_Name.ToLower() == info.Name.ToLower()) info.Name = random.Next(0, 100000) + info.Name;
                            if (file.Song_Title.ToLower() == info.SongInfo.SongDisplayName.ToLower() && Is_Original == "No")
                            {
                                info.SongInfo.SongDisplayName += " [a." + (MultiTrack_Version != "" ? MultiTrack_Version + "_" + altver : (altver + ((author == null || author == "" || author == "Custom Song Creator") ? "" : "_" + author))) + "]";// ;//random.Next(0, 100000).ToString()
                                alt = altver;
                                if (chbx_Additional_Manipulations.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                                {
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                                }
                            }
                        }

                        //Doublechecking that no DLC Name is the same (last import 1500 songs generate once such exception :) )
                        DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main WHERE DLC_Name='" + info.Name + "'", txt_DBFolder.Text);
                        if (dms.Tables[0].Rows.Count > 1) info.Name = random.Next(0, 100000) + info.Name;

                        b++;

                        jk = file.Version;
                        k = file.Author;
                        oldfilehas = file.File_Hash;
                        if (b >= norows || artist != "Insert" || IgnoreRest)
                        {
                            if (artist == "Ignore")
                            {
                                string filePath = unpackedDir;
                                try
                                {
                                    DeleteDirectory(filePath);
                                }
                                catch (Exception ex) { Console.Write(ex); }
                            }
                            break;//
                        }

                    }

                if (duplit) return "0";
                else
                {
                    var insertA = "\"" + txt_RocksmithDLCPath.Text + "\", \"" + txt_RocksmithDLCPath.Text + "\", \"" + original_FileName + "\", \"" + ds.Tables[0].Rows[i].ItemArray[5] + "\", \""
                        + ds.Tables[0].Rows[i].ItemArray[3] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[4] + "\", " + IDD + ", \"" + DLCC + "\", \"" + Platformm + "\", \"" + Is_Original + "\"";
                    var dfn = "";
                    //Move file New file to duplicates Ignore is select
                    if (artist == "Ignore" && chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        Available_Duplicate = "Yes";
                        if (!File.Exists(dupli_Path_Import + "\\" + original_FileName)) dfn = dupli_Path_Import + "\\" + original_FileName;
                        else dfn = dupli_Path_Import + "\\" + original_FileName.Replace(".psarc", "[Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "].psarc");
                        try
                        {
                            File.Move(txt_RocksmithDLCPath.Text + "\\" + original_FileName, dfn);
                        }
                        catch (Exception ex) { MessageBox.Show("Issues when moving to duplicate folder after dupli ignore" + "-" + ex.Message + filename); }
                        insertA = "\"" + dfn + "\", \"" + dupli_Path_Import + "\", \"" + dfn.Replace(dupli_Path_Import + "\\", "") + "\", \"" + ds.Tables[0].Rows[i].ItemArray[5] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[3] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[4] + "\", " + IDD + ", \"" + DLCC + "\", \"" + Platformm + "\", \"" + Is_Original + "\"";
                        //dELETE DUPLCAITION FODLER
                        try
                        {
                            DeleteDirectory(unpackedDir);
                            //Directory.Delete(unpackedDir, true);
                        }
                        catch (IOException ex) { Console.Write(ex.Message); }
                    }


                    var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";

                    if (artist == "Ignore")
                    {
                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                        string filePath = unpackedDir;
                        try
                        {
                            DeleteDirectory(filePath);
                        }
                        catch (Exception ex) { Console.Write(ex); }
                    }

                    //Move file Original file to duplicates if Main DB record is being overitten
                    if (artist == "Update" && chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        DataSet dzr = new DataSet(); dzr = SelectFromDB("Main", "SELECT Original_FileName, Available_Old FROM Main WHERE ID=" + IDD + ";", txt_DBFolder.Text);
                        var Original_FileName = dzr.Tables[0].Rows[0].ItemArray[0].ToString();
                        //var hzn=dupli_Path_Import + "\\" + Original_FileName;
                        //var ghj = dzr.Tables[0].Rows[0].ItemArray[1].ToString();
                        //if (File.Exists(dupli_Path_Import + "\\" + Original_FileName) && dzr.Tables[0].Rows[0].ItemArray[1].ToString() != "Yes")
                        //{
                        Available_Duplicate = "Yes";
                        //    if (!File.Exists(txt_TempPath.Text + "\\0_old\\" + Original_FileName))
                        //    {

                        //        try
                        //        {
                        //            File.Move(txt_TempPath.Text + "\\0_old\\" + Original_FileName, dupli_Path_Import + "\\" + Original_FileName);
                        //        }
                        //        catch (Exception ex) { MessageBox.Show("Issues when moving to duplicate folder at dupli Update" + "-" + ex.Message + filename); }
                        //    }
                        //    else File.Delete(txt_RocksmithDLCPath.Text + "\\" + Original_FileName);
                        ////}
                        var fnn = Original_FileName;
                        if (File.Exists(txt_TempPath.Text + "\\0_duplicate\\" + fnn)) fnn = Original_FileName.Replace(".psarc", "[Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "].psarc");
                        try
                        {
                            File.Move(txt_TempPath.Text + "\\0_old\\" + Original_FileName, txt_TempPath.Text + "\\0_duplicate\\" + fnn);
                            DeleteDirectory(folder_name);
                            //  Directory.Delete(folder_name, true);
                        }
                        catch (Exception ex) { MessageBox.Show("Issues when moving to duplicate folder at dupli Update" + "-" + ex.Message + filename); }
                        var cmdupd = "UPDATE Pack_AuditTrail Set FileName='" + fnn + "', PackPath =REPLACE(PackPath,'\\0_old','\\0_duplicate') WHERE FileHash='" + oldfilehas + "' AND PackPath='" + txt_TempPath.Text + "\\0_old" + "'";
                        DataSet dus = new DataSet(); dus = UpdateDB("Pack_AuditTrail", cmdupd + ";");


                        //var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                        //var insertA = txt_TempPath.Text + "\\0_old\\" + ", " + txt_TempPath.Text + "\\0_old\\" + ", " + original_FileName + ", " + ds.Tables[0].Rows[i].ItemArray[5] + ", "
                        //    + ds.Tables[0].Rows[i].ItemArray[3] + ", " + ds.Tables[0].Rows[i].ItemArray[4] + ", " + IDD + ", " + DLCC + ", " + Platformm;
                        //InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                    }
                    tst = "end not dupi measures..."; timestamp = UpdateLog(timestamp, tst, true);
                    if (artist != "Ignore")
                    //    ;// DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + IDD + ")");
                    //else
                    {

                        PreviewTime = "";
                        PreviewLenght = "";
                        var recalc_Preview = false;
                        var duration = ""; var ogg = "";
                        var bitratep = 250001;
                        SampleRate = 49000;
                        if (info.OggPreviewPath != null)
                            if (File.Exists(info.OggPreviewPath.Replace(".wem", ".ogg")))
                                ogg = info.OggPreviewPath.Replace(".wem", ".ogg");
                            else if (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")))
                                ogg = info.OggPreviewPath.Replace(".wem", "_fixed.ogg");
                        if (File.Exists(ogg))
                        {
                            using (var vorbis = new NVorbis.VorbisReader(ogg))
                            {
                                bitratep = vorbis.NominalBitrate;
                                duration = vorbis.TotalTime.ToString();
                                if ((duration.Split(':'))[0] == "00" && (duration.Split(':'))[1] == "00")
                                    PreviewLenght = (duration.Split(':'))[2];
                                else
                                    PreviewLenght = duration;
                                string[] timepiece = duration.Split(':');
                                if (timepiece[0] != "00" || timepiece[1] != "00")
                                    recalc_Preview = true;
                            }
                        }

                        //check Audio bitrate as originals are always at 128..
                        // if (chbx_Additional_Manipulations.GetItemChecked(69))
                        using (var vorbis = new NVorbis.VorbisReader(info.OggPath.Replace(".wem", "_fixed.ogg")))
                        {
                            bitrate = vorbis.NominalBitrate;
                            SampleRate = vorbis.SampleRate;
                        }
                        //if (chbx_Additional_Manipulations.GetItemChecked(70))
                        //{

                        //}
                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
                        if (chbx_Additional_Manipulations.GetItemChecked(69) && info.OggPath != null && (bitrate > ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() + 8000 || SampleRate > 48001))
                        {
                            tst = "start encoding Main audio to 128kb ... " + bitrate + " ... " + SampleRate; timestamp = UpdateLog(timestamp, tst, true);
                            if (WwiseInstalled())
                            {
                                Downstream(info.OggPath);//.Replace(".wem", "_fixed.ogg")
                                audio_hash = GetHash(info.OggPath);
                                using (var vorbis = new NVorbis.VorbisReader(info.OggPath.Replace(".wem", "_fixed.ogg")))
                                {
                                    bitrate = vorbis.NominalBitrate;
                                    SampleRate = vorbis.SampleRate;
                                }
                            }
                            tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true);
                        }

                        //Set Preview
                        if ((chbx_Additional_Manipulations.GetItemChecked(34) && info.OggPreviewPath == null ||
                                (chbx_Additional_Manipulations.GetItemChecked(55) && ((audio_hash != "" && audio_hash == audioPreview_hash) || recalc_Preview))))
                        {
                            timestamp = UpdateLog(timestamp, "Trying to add preview as missing.", true);

                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
                            startInfo.WorkingDirectory = AppWD;
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
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            if (File.Exists(t))
                                using (var DDC = new Process())
                                {
                                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                    if (DDC.ExitCode == 0)
                                    {
                                        var wwisePath = "";
                                        if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                                            wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                                        else
                                            wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                                        if (wwisePath == "")
                                        {
                                            ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2015.1.9 build 5624 with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                                            //v2016.2.1.5995 is currently breaking PC cocnversion (not Ps3)
                                            frm1.ShowDialog();
                                            if (frm1.IgnoreSong) return "0";// break;
                                            if (frm1.StopImport) { j = 10; return "0"; }// break; }
                                        }
                                        if (File.Exists(info.OggPreviewPath)) DeleteFile(info.OggPreviewPath);
                                        GenericFunctions.Converters(tt, GenericFunctions.ConverterTypes.Ogg2Wem, false);
                                        if (File.Exists(info.OggPreviewPath)) DeleteFile(info.OggPreviewPath.Replace(".wem", ".wav"));
                                        if (File.Exists(info.OggPreviewPath)) DeleteFile(info.OggPreviewPath.Replace(".wem", ".ogg"));
                                        info.OggPreviewPath = tt.Replace(".ogg", ".wem");
                                        audioPreview_hash = GetHash(info.OggPreviewPath);

                                        PreviewTime = ConfigRepository.Instance()["dlcm_PreviewStart"];
                                        PreviewLenght = ConfigRepository.Instance()["dlcm_PreviewLenght"];
                                    }
                                }
                        }
                        //Fix _preview.OGG having a diff name than _preview.wem after oggged
                        var previewN = info.OggPreviewPath.ToString().Replace(".wem", ".ogg");
                        if (!File.Exists(previewN))
                        {
                            foreach (string preview_name in Directory.GetFiles(unpackedDir, "*_preview.wem", System.IO.SearchOption.AllDirectories))
                            {
                                foreach (string file_name in Directory.GetFiles(unpackedDir, "*.ogg", System.IO.SearchOption.AllDirectories))
                                {
                                    if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
                                    {
                                        var tl = previewN;
                                        var hg = preview_name;
                                        previewN = preview_name.Replace(".wem", ".ogg");
                                        if (!File.Exists(previewN))
                                        {
                                            try
                                            {
                                                File.Copy(file_name, previewN, true);
                                                DeleteFile(file_name);
                                                //File.Delete(file_name);
                                            }
                                            catch (Exception ee)
                                            {
                                                timestamp = UpdateLog(timestamp, "FAILED1 preview fix" + ee.Message + "----" + info.OggPath + "\n -" + previewN + "\n -" + file_name + ".ogg", true);
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
                                    bitratep = vorbis.NominalBitrate;
                                    if ((vorbis.TotalTime.ToString().Split(':'))[0] == "00" && (vorbis.TotalTime.ToString().Split(':'))[1] == "00")
                                        PreviewLenght = (vorbis.TotalTime.ToString().Split(':'))[2];
                                    else PreviewLenght = vorbis.TotalTime.ToString();
                                    audioPreview_hash = GetHash(info.OggPreviewPath);
                                }
                            }
                        }

                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
                        if (chbx_Additional_Manipulations.GetItemChecked(69) && info.OggPreviewPath != null && (bitratep > ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() + 8000))
                        {
                            tst = "start encoding preview audio to 128kb ... " + bitratep + " ... " + SampleRate; timestamp = UpdateLog(timestamp, tst, true);
                            if (WwiseInstalled())
                            {
                                Downstream(info.OggPreviewPath);//.Replace(".wem", "_fixed.ogg")
                                audioPreview_hash = GetHash(info.OggPreviewPath);
                            }
                            tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true);
                        }

                        tst = "end preview measures..."; timestamp = UpdateLog(timestamp, tst, true);
                    }
                    //Define final path for the imported song
                    var norm_path = txt_TempPath.Text + "\\" + platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + trackno.ToString() + "_" + info.SongInfo.SongDisplayName + "_" + random.Next(0, 100000);

                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
                    command = connection.CreateCommand();
                    if (artist == "Update")
                    {
                        //Update MainDB
                        timestamp = UpdateLog(timestamp, "Updating / Overriting " + IDD + "-" + j + "-" + "" + "-" + k + "..", true);

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
                        command.CommandText += "Live_Details = @param72, ";
                        command.CommandText += "audioBitrate = @param73, ";
                        command.CommandText += "audioSampleRate = @param74, ";
                        command.CommandText += "Is_Acoustic = @param75, ";
                        command.CommandText += "Has_Other_Officials = @param76, ";
                        command.CommandText += "Spotify_Song_ID = @param77, ";
                        command.CommandText += "Spotify_Artist_ID = @param78, ";
                        command.CommandText += "Spotify_Album_ID = @param79, ";
                        command.CommandText += "Spotify_Album_URL = @param80";
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
                        command.Parameters.AddWithValue("@param15", ((info.ToolkitInfo.PackageVersion == null) ? "1" : info.ToolkitInfo.PackageVersion));
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
                        command.Parameters.AddWithValue("@param35", ((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
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
                        command.Parameters.AddWithValue("@param73", bitrate);
                        command.Parameters.AddWithValue("@param74", SampleRate);
                        command.Parameters.AddWithValue("@param75", IsAcoustic);
                        command.Parameters.AddWithValue("@param76", HasOrig);
                        command.Parameters.AddWithValue("@param77", SpotifySongID == null ? DBNull.Value.ToString() : SpotifySongID);
                        command.Parameters.AddWithValue("@param78", SpotifyArtistID == null ? DBNull.Value.ToString() : SpotifyArtistID);
                        command.Parameters.AddWithValue("@param79", SpotifyAlbumID == null ? DBNull.Value.ToString() : SpotifyAlbumID);
                        command.Parameters.AddWithValue("@param80", SpotifyAlbumURL == null ? DBNull.Value.ToString() : SpotifyAlbumURL);
                        //EXECUTE SQL/UPDATE
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                            //Deleted old folder
                            //Directory.Delete(folder_name, true);
                            ////remove original dir TO DO
                            //move old/aleady imported&saved file
                            //if (chbx_Additional_Manipulations.GetItemChecked(29))//30. When NOT importing a duplicate Move it to _duplicate
                            //{
                            //    timestamp = UpdateLog(timestamp, old_Path_Import + "\\" + filename + "dupli_Path_Import arrangement:" + dupli_Path_Import + "\\" + filename);
                            //    try
                            //    {
                            //        Available_Duplicate = "Yes";
                            //        if (!File.Exists(dupli_Path_Import + "\\" + filename))
                            //            if (File.Exists(old_Path_Import + "\\" + filename))
                            //            {
                            //                File.Move(old_Path_Import + "\\" + filename, dupli_Path_Import + "\\" + filename);
                            //                Available_Duplicate = "Yes";
                            //            }
                            //            else timestamp = UpdateLog(timestamp, "___");
                            //        else
                            //        {
                            //            File.Delete(txt_RocksmithDLCPath.Text + "\\" + filename);
                            //            Available_Duplicate = "Yes";
                            //        }
                            //        timestamp = UpdateLog(timestamp, "deleting...dele...");
                            //    }
                            //    catch (Exception ex) { Console.Write(ex.Message); }
                            //}
                            tst = "end updating ..."; timestamp = UpdateLog(timestamp, tst, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Update Main DB connection in Import ! " + DB_Path + "-" + original_FileName + "-" + command.CommandText);

                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                        //Delete Audit trail of pack
                        //DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + IDD + ")");
                    }
                    //var ttz="";
                    //if (trackno == 0) ttz="No";
                    //else ttz="Yes";
                    //ttz = Has_author;
                    //Read Track no
                    //www.metrolyrics.com: Nirvana Bleach Swap Meet
                    if (ExistingTrackNo != "")
                        //{
                        trackno = ExistingTrackNo.ToInt32();
                    //
                    //}
                    if (artist == "Insert")
                    {
                        timestamp = UpdateLog(timestamp, "Inserting ", true);
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
                        command.CommandText += "Live_Details, ";
                        command.CommandText += "audioBitrate,";
                        command.CommandText += "audioSampleRate,";
                        command.CommandText += "Is_Acoustic,";
                        command.CommandText += "Has_Other_Officials, ";
                        command.CommandText += "Spotify_Song_ID, ";
                        command.CommandText += "Spotify_Artist_ID, ";
                        command.CommandText += "Spotify_Album_ID, ";
                        command.CommandText += "Spotify_Album_URL";
                        command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                        command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                        command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                        command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                        command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48,@param49";
                        command.CommandText += ",@param50,@param51,@param52,@param53,@param54,@param55,@param56,@param57,@param58,@param59";
                        command.CommandText += ",@param60,@param61,@param62,@param63,@param64,@param65,@param66,@param67,@param68,@param69";
                        command.CommandText += ",@param70,@param71,@param72,@param73,@param74,@param75,@param76,@param77,@param78,@param79,@param80" + ")";//,@param76


                        command.Parameters.AddWithValue("@param1", import_path);
                        command.Parameters.AddWithValue("@param2", original_FileName);
                        command.Parameters.AddWithValue("@param3", original_FileName);
                        command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3]);
                        command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3]);
                        command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4]);
                        command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5]);// ds.Tables[0].Rows[i].ItemArray[5]);
                        command.Parameters.AddWithValue("@param8", unpackedDir);
                        command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                        command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                        command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                        command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                        command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                        command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                        command.Parameters.AddWithValue("@param15", ((info.ToolkitInfo.PackageVersion == null) ? "1" : info.ToolkitInfo.PackageVersion));
                        command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                        command.Parameters.AddWithValue("@param17", info.Volume);
                        command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                        command.Parameters.AddWithValue("@param19", info.Name);
                        command.Parameters.AddWithValue("@param20", AppIdD);
                        command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                        command.Parameters.AddWithValue("@param22", info.OggPath);
                        command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));
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
                        command.Parameters.AddWithValue("@param35", ((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                        command.Parameters.AddWithValue("@param36", Has_author);
                        command.Parameters.AddWithValue("@param37", Tunings);
                        command.Parameters.AddWithValue("@param38", PluckedType);
                        command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                        command.Parameters.AddWithValue("@param40", info.SignatureType);
                        command.Parameters.AddWithValue("@param41", author);
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
                        command.Parameters.AddWithValue("@param71", IsLive == null ? DBNull.Value.ToString() : IsLive);
                        command.Parameters.AddWithValue("@param72", LiveDetails == null ? DBNull.Value.ToString() : LiveDetails);
                        command.Parameters.AddWithValue("@param73", bitrate);
                        command.Parameters.AddWithValue("@param74", SampleRate);
                        command.Parameters.AddWithValue("@param75", IsAcoustic == null ? DBNull.Value.ToString() : IsAcoustic);
                        command.Parameters.AddWithValue("@param76", HasOrig == null ? DBNull.Value.ToString() : HasOrig);
                        command.Parameters.AddWithValue("@param77", SpotifySongID == null ? DBNull.Value.ToString() : SpotifySongID);
                        command.Parameters.AddWithValue("@param78", SpotifyArtistID == null ? DBNull.Value.ToString() : SpotifyArtistID);
                        command.Parameters.AddWithValue("@param79", SpotifyAlbumID == null ? DBNull.Value.ToString() : SpotifyAlbumID);
                        command.Parameters.AddWithValue("@param80", SpotifyAlbumURL == null ? DBNull.Value.ToString() : SpotifyAlbumURL);
                        //command.Parameters.AddWithValue("@param76", Top10);
                        var rt = (import_path) + "\",\"" + (original_FileName) + "\",\"" + (original_FileName) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[3]) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[3]) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[4]) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[5]) + "\",\"" + (unpackedDir) + "\",\"" + (info.SongInfo.SongDisplayName) + "\",\"" + (info.SongInfo.SongDisplayNameSort) + "\",\"" + (info.SongInfo.Album) + "\",\"" + (info.SongInfo.Artist) + "\",\"" + (info.SongInfo.ArtistSort) + "\",\"" + (info.SongInfo.SongYear) + "\",\"" + (((info.ToolkitInfo.PackageVersion == null) ? "1" : info.ToolkitInfo.PackageVersion)) + "\",\"" + (info.SongInfo.AverageTempo) + "\",\"" + (info.Volume) + "\",\"" + (info.PreviewVolume) + "\",\"" + (info.Name) + "\",\"" + (AppIdD) + "\",\"" + (info.AlbumArtPath ?? DBNull.Value.ToString()) + "\",\"" + (info.OggPath) + "\",\"" + ((info.OggPreviewPath ?? DBNull.Value.ToString())) + "\",\"" + (Bass) + "\",\"" + (Guitar) + "\",\"" + (((Lead != "") ? Lead : "No")) + "\",\"" + (((Rhythm != "") ? Rhythm : "No")) + "\",\"" + (((Combo != "") ? Combo : "No")) + "\",\"" + (((Vocalss != "") ? Vocalss : "No")) + "\",\"" + (sect1on) + "\",\"" + (((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes")) + "\",\"" + (((info.OggPreviewPath != null) ? "Yes" : "No")) + "\",\"" + (Tones_Custom) + "\",\"" + (DD) + "\",\"" + (((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No")) + "\",\"" + (Has_author) + "\",\"" + (Tunings) + "\",\"" + (PluckedType) + "\",\"" + (((Is_Original == "Yes") ? "ORIG" : "CDLC")) + "\",\"" + (info.SignatureType) + "\",\"" + (author) + "\",\"" + (tkversion) + "\",\"" + (Is_Original) + "\",\"" + (((alt == "" || alt == null) ? "No" : "Yes")) + "\",\"" + (alt ?? DBNull.Value.ToString()) + "\",\"" + (art_hash) + "\",\"" + (audio_hash) + "\",\"" + (audioPreview_hash) + "\",\"" + (Bass_Has_DD) + "\",\"" + (bonus) + "\",\"" + (Available_Duplicate) + "\",\"" + (Available_Old) + "\",\"" + (description) + "\",\"" + (comment) + "\",\"" + (info.OggPath.Replace(".wem", "_fixed.ogg")) + "\",\"" + ((info.OggPreviewPath == null ? DBNull.Value.ToString() : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") : info.OggPreviewPath.Replace(".wem", ".ogg")))) + "\",\"" + ((trackno == 0 ? "No" : "Yes")) + "\",\"" + (trackno.ToString()) + "\",\"" + (platformTXT.ToString()) + "\",\"" + (Is_MultiTrack) + "\",\"" + (MultiTrack_Version) + "\",\"" + (YouTube_Link) + "\",\"" + (CustomsForge_Link) + "\",\"" + (CustomsForge_Like) + "\",\"" + (CustomsForge_ReleaseNotes) + "\",\"" + (PreviewTime ?? DBNull.Value.ToString()) + "\",\"" + (PreviewLenght ?? DBNull.Value.ToString()) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[6]) + "\",\"" + (SongLenght) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[7]) + "\",\"" + (IsLive == null ? DBNull.Value.ToString() : IsLive) + "\",\"" + (LiveDetails == null ? DBNull.Value.ToString() : LiveDetails) + "\",\"" + (IsAcoustic == null ? DBNull.Value.ToString() : IsAcoustic) + "\"";
                        //EXECUTE SQL/INSERT
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            timestamp = UpdateLog(timestamp, "error at update " + ex + "\n" + rt, true);
                            throw;
                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                        //If No version found then defaulted to 1
                        //TO DO If default album cover then mark it as suck !?
                        //If no version found must by Rocksmith Original or DLC

                        timestamp = UpdateLog(timestamp, "Records inserted in Main= " + (i + 1), true);
                    }
                    if (artist == "Insert" || artist == "Update") //Common set of action for all
                    {
                        //Get last inserted ID
                        DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT ID FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"", txt_DBFolder.Text);

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
                        foreach (var arg in info.Arrangements)
                        {
                            command = connection.CreateCommand();
                            try
                            {
                                var mss = arg.SongXml.File.ToString();
                                int poss = 0;
                                if (mss.Length > 0)
                                {
                                    poss = mss.ToString().LastIndexOf("\\") + 1;

                                    if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    {
                                        arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                                        arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                    }
                                    else
                                    {
                                        arg.SongXml.File = norm_path + "\\songs\\arr\\" + mss.Substring(poss);
                                        arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
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
                                        arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
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
                                    timestamp = UpdateLog(timestamp, "error at insert " + command.CommandText + "\n" + arg.Name + " " + arg.RouteMask.ToString(), true);
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
                        timestamp = UpdateLog(timestamp, "Arrangements Updated " + info.Arrangements.Count, true);

                        //UPDATE TonesDB
                        CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                        connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
                        foreach (var tn in info.TonesRS2014)
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

                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    timestamp = UpdateLog(timestamp, "error in arag " + CDLC_ID + " " + tn.Name, true);
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
                        timestamp = UpdateLog(timestamp, "ToneDB Updated " + info.TonesRS2014.Count, true);

                        //Move Extracted Song to Temp Folder
                        int pos = 0;
                        int l = 0;
                        DataSet dis = new DataSet();
                        try //Move from _import into Temp folder (copy+delete as move sometimes fails)
                        {
                            string source_dir = @unpackedDir;
                            string destination_dir = @norm_path;

                            // substring is to remove destination_dir absolute path (E:\).
                            // Create subdirectory structure in destination    
                            foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                            {
                                Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                            }

                            foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                            }
                            DeleteDirectory(source_dir);
                            //Directory.Delete(source_dir, true);
                        }
                        catch (Exception ee)
                        {
                            timestamp = UpdateLog(timestamp, "FAILED3 ..", true);
                            Console.WriteLine(ee.Message);
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(15)) //16. Move Original Imported files to temp/0_old                               
                        {
                            //Move imported psarc into the old folder
                            try
                            {
                                File.Copy(txt_RocksmithDLCPath.Text + "\\" + original_FileName, old_Path_Import + "\\" + original_FileName, true);
                                DeleteFile(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                                Available_Old = "Yes";
                                timestamp = UpdateLog(timestamp, "File Moved to old" + "...", true);
                            }
                            catch (System.IO.FileNotFoundException ee)
                            {
                                timestamp = UpdateLog(timestamp, "FAILED2" + ee.Message + "----", true);
                                Console.WriteLine(ee.Message);
                            }
                        }

                        //Fixing any _preview_preview issue..Start
                        //Correct moved file path audio,preview
                        //Add wem
                        //Corrent arrangements file path
                        cmd = "UPDATE Main SET Available_Old=\"" + Available_Old + "\",";
                        var audiopath = "";
                        var audioprevpath = "";
                        var ms = "";
                        ms = info.AlbumArtPath;
                        var cmd2 = "";
                        if (ms != "" && ms != null)
                        {
                            pos = ms.ToString().LastIndexOf("\\") + 1;
                            if (AlbumArtPath == info.AlbumArtPath)
                                if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                else
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\gfxassets\\album_art\\" + ms.Substring(pos) + "\"";
                            else //Override Album Art during the Duplication assements process
                            {
                                cmd += " AlbumArt_Hash=\"" + art_hash + "\", AlbumArtPath=\"" + AlbumArtPath + "\"";
                            }

                            //If Cover was applied to the original then update its album art
                            if (dupliID != "")
                            {
                                cmd2 = cmd + " WHERE ID=" + dupliID;
                                DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmd2 + ";");
                            }
                        }
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


                        var source_dir1 = norm_path + path_decom1;//Delete any Wav file created..by....? ccc
                        foreach (string wav_name in Directory.GetFiles(source_dir1, "*_preview_fixed_preview*", System.IO.SearchOption.AllDirectories))
                        {
                            DeleteFile(wav_name);
                        }

                        //Delete any Wav file created..by....?ccc
                        foreach (string wav_name in Directory.GetFiles(source_dir1, "*.wav", System.IO.SearchOption.AllDirectories))
                        {
                            DeleteFile(wav_name);//, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin); //File.Delete(wav_name);
                        }

                        if (ms.Length > 0 && pos > 1)
                        {
                            ms = ms.Substring(0, pos);
                            pos = ms.LastIndexOf("\\") + 1;
                            l = ms.Substring(pos).Length;
                            audiopath = norm_path + path_decom1 + ms.Substring(pos, l);
                            ////Gather song Lenght
                            cmd += ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "" : " ,") + " AudioPath=\"" + audiopath + ".wem\"";
                            cmd += " , OggPath=\"" + norm_path + path_decom2 + ms.Substring(pos, l) + "_fixed.ogg\" , Song_Lenght=\"" + SongLenght + "\"";
                        }
                        pos = (info.OggPreviewPath.LastIndexOf(".wem"));
                        ms = info.OggPreviewPath;
                        if (ms.Length > 0 && pos > 1 && (info.OggPreviewPath != null))
                        {
                            ms = ms.Substring(0, pos);
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview_preview"));
                            pos = ms.LastIndexOf("\\") + 1;
                            l = ms.Substring(pos).Length;
                            audioprevpath = norm_path + path_decom1 + ms.Substring(pos, l);
                            cmd += " , audioPreviewPath=\"" + audioprevpath + ".wem\"";
                            cmd += " , oggPreviewPath=\"" + audioprevpath + ".ogg\" , PreviewLenght=\"" + PreviewLenght + "\"";
                        }
                        cmd += " , Folder_Name=\"" + norm_path + "\"";

                        cmd += " WHERE ID=" + CDLC_ID;
                        using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            OleDbDataAdapter dgs = new OleDbDataAdapter(cmd, cn);
                            dgs.Fill(dis, "Main");
                            dgs.Dispose();
                            timestamp = UpdateLog(timestamp, "Main DB updated after Song Temp DIR REnamed/Stadardised", true);
                        }
                        //fix potentially issues with songs with the audio preview WEM  file the same as the original song(file size{no preview})
                        //Move wem to KIT folder + rename
                        if (info.OggPreviewPath != null)
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1)
                            {
                                try
                                {
                                    File.Move((audiopath + "_preview.wem"), (audiopath + ".wem"));
                                    File.Move((audioprevpath + "_preview.wem"), (audioprevpath + ".wem"));
                                    timestamp = UpdateLog(timestamp, "Issues w the WEM filenames when no preview ", true);
                                }
                                catch (Exception ee)
                                {
                                    timestamp = UpdateLog(timestamp, "FAILED1" + ee.Message + "----" + info.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem", true);
                                    Console.WriteLine(ee.Message);
                                }
                                timestamp = UpdateLog(timestamp, "Fixed preview_preview issue", true);
                            }
                        //Fixing any _preview_preview issue..End

                        UpdatePackingLog("LogImporting", DB_Path, packid, CDLC_ID, tst);

                    }
                    //Updating the Standardization table
                    DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\"" + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", txt_DBFolder.Text);

                    if (dzs.Tables[0].Rows.Count == 0)
                    {
                        var insertcmdd = "Artist, Album";
                        var insertvalues = "\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\"";
                        InsertIntoDBwValues("Standardization", insertcmdd, insertvalues);
                    }
                    timestamp = UpdateLog(timestamp, "done", true);
                    pB_ReadDLCs.Increment(1);
                }
            }
            //return "ignored";


            var ttt = CDLC_ID.ToString() == "" ? IDD : CDLC_ID.ToString();
            string ffg = info.Name;
            string rtr = ttt + ";" + ffg + ";" + artist;
            //if (rtr == "" ) //
            //    rtr = "";
            return rtr;

        }


        public string GetAlternateNo()
        {
            var a = "";
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //Get the higgest Alternate Number
            var sel = "";
            sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + data.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + data.SongInfo.Album + "\") AND ";
            sel += "(LCASE(Song_Title)=LCASE(\"" + data.SongInfo.SongDisplayName + "\") OR ";
            sel += "LCASE(Song_Title) like \"%" + data.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
            sel += "LCASE(Song_Title_Sort) =LCASE(\"" + data.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + data.Name + "\");";
            DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, txt_DBFolder.Text);
            var altver = dds.Tables[0].Rows[0].ItemArray[0].ToString();
            if (Is_Original == "No") a = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
            return a;
        }

        public void Translation_And_Correction(string dbp)
        // Select only Corrected Arstist OR Album OR Cover combination
        // For Each Corrected Record build up an Update sentence
        // Insert any translation if not already existing
        {
            var cmd1 = "";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var DB_Path = dbp + "\\Files.accdb";
            int aa = 0;
            pB_ReadDLCs.Value = 0;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\") OR (AlbumArt_Correction <> \"\") order by id;", txt_DBFolder.Text);
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = norec;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                artist_c = dataRow.ItemArray[2].ToString();
                album_c = dataRow.ItemArray[4].ToString();
                artpath_c = dataRow.ItemArray[5].ToString();

                cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + (album_c != "" ? "\"," : "\"") : "") + (album_c != "" ? " Album = \"" + album_c + (artpath_c != "" ? "\"," : "\"") : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"" : "");
                cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"";
                dus = UpdateDB("Main", cmd1 + ";");
                pB_ReadDLCs.Increment(1);
            }
            //insert any translation if not already existing

            var insertcmdd = "Artist, Album";
            var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", insertvalues, txt_DBFolder.Text); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            InsertIntoDBwValues("Standardization", insertcmdd, insertvalues);

            var cmd3 = "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)";
            DeleteFromDB("Groups", cmd3); //Cleans out duplicates

            //Apply Artist Short Name
            Standardization.ApplyArtistShort();

            //Apply Album Short Name
            Standardization.ApplyAlbumShort();

            //Apply DefaultCover
            Standardization.MakeCover();

            //DeleteFromDB("Standardization", "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)");
            //DeleteFromDB("Standardization", "DELETE* FROM Standardization WHERE id NOT IN (SELECT min(id) FROM Standardization GROUP BY Artist, Artist_Correction, Album, Album_Correction);");
            DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE id NOT IN( SELECT min(ID) FROM(SELECT ID, Artist , IIF(Artist_Correction is NULL or Artist_Correction = '', '0', Artist_Correction) as Artist_Corrections , Album , IIF(Album_Correction is NULL or Album_Correction = '', '0', Album_Correction) as Album_Corrections FROM Standardization) GROUP BY Artist, Artist_Corrections, Album, Album_Corrections); ");
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + aa + ")");
        }

        internal string AssessConflict(GenericFunctions.MainDBfields filed, DLCPackageData datas, string Fauthor, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string Vocalss, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string dbpathh, List<string> clist, List<string> dlist, bool newold, string Is_Original, string altver, string fsz, string unpackedDir, string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic, string platform, string IsLive, string LiveDetails, string IsAcoustic, string has_Orig)
        {
            if ((tkversion == "" && Is_Original == "Yes") || (tkversion == "" && filed.Is_Original == "Yes"))
            {
                UpdateDB("Main", "UPDATE Main SET Has_Other_Officials=\"Yes\" WHERE ID=" + filed.ID + "");
                has_Orig = "Yes";
                return "Ignore";

            }
            else if (chbx_Additional_Manipulations.GetItemChecked(13) ||
            (chbx_Additional_Manipulations.GetItemChecked(14) && ((tkversion == "" && Is_Original == "Yes") || (tkversion != "" && filed.Is_Original == "No") || (tkversion != "" && Is_Original == "No") || (tkversion == "" && filed.Is_Original == "Yes")))
            || (chbx_Additional_Manipulations.GetItemChecked(56) && ((filed.Is_Multitrack == "Yes" && Is_MultiTrack != "Yes") || (Is_MultiTrack == "Yes" && filed.Is_Multitrack != "Yes") || (Is_MultiTrack == "Yes" && filed.Is_Multitrack == "Yes" && MultiTrack_Version != filed.MultiTrack_Version)))
            || (chbx_Additional_Manipulations.GetItemChecked(66) && ((filed.Is_Live == "Yes" && IsLive != "Yes") || (IsLive == "Yes" && filed.Is_Live != "Yes") || (IsLive == "Yes" && filed.Is_Live == "Yes" && LiveDetails != filed.Live_Details))))
                //"14. Import all as Alternates" 15. Import any Custom as Alternate if an Original exists
                if (art_hash == filed.AlbumArt_Hash && audio_hash == filed.Audio_Hash && audioPreview_hash == filed.AudioPreview_Hash && tkversion == filed.ToolkitVersion && Fauthor == filed.Author && (PackageVersion == filed.Version || (PackageVersion == "" && "1" == filed.Version)) && Is_Original == filed.Is_Original && datas.Name == filed.DLC_Name && platform != filed.Platform) return "Ignore";
                else return "Alternate";
            else
            {
                frm_Duplicates_Management frm1 = new frm_Duplicates_Management(filed, datas, Fauthor, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), clist, dlist, newold, Is_Original, altver, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, FileDate, title_duplic, platform, IsLive, LiveDetails, IsAcoustic);
                frm1.ShowDialog();
                if (frm1.Author != author) if (frm1.Author == "Custom Song Creator" && chbx_Additional_Manipulations.GetItemChecked(47)) author = "";
                    else author = frm1.Author;
                if (frm1.Description != description) description = frm1.Description;
                if (frm1.Comment != comment) comment = frm1.Comment;
                if (frm1.Title != SongDisplayName)
                    if (chbx_Additional_Manipulations.GetItemChecked(46)) SongDisplayName = frm1.Title;
                if (frm1.Version != tkversion) PackageVersion = frm1.Version;
                if (frm1.DLCID != Namee) Namee = frm1.DLCID;
                if (frm1.Is_Alternate != Is_Alternate) Is_Alternate = frm1.Is_Alternate;
                if (frm1.Title_Sort != Title_Sort) Title_Sort = frm1.Title_Sort;
                if (frm1.Artist != Artist) Artist = frm1.Artist;
                if (frm1.ArtistSort != ArtistSort) ArtistSort = frm1.ArtistSort;
                if (frm1.Album != Album) Album = frm1.Album;
                if (frm1.Alternate_No != Alternate_No) Alternate_No = frm1.Alternate_No;
                if (frm1.AlbumArtPath != AlbumArtPath) AlbumArtPath = frm1.AlbumArtPath;
                if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
                if (frm1.MultiT != "") Is_MultiTrack = frm1.MultiT;
                if (frm1.MultiTV != "") MultiTrack_Version = frm1.MultiTV;

                if (frm1.isLive != "") IsLive = frm1.isLive;
                if (frm1.isAcoustic != "") IsLive = frm1.isAcoustic;
                if (frm1.liveDetails != "") LiveDetails = frm1.liveDetails;
                if (frm1.YouTube_Link != "") YouTube_Link = frm1.YouTube_Link;
                if (frm1.CustomsForge_Link != "") CustomsForge_Link = frm1.CustomsForge_Link;
                if (frm1.CustomsForge_Like != "") CustomsForge_Like = frm1.CustomsForge_Like;
                if (frm1.CustomsForge_ReleaseNotes != "") CustomsForge_ReleaseNotes = frm1.CustomsForge_ReleaseNotes;
                if (frm1.dupliID != "") dupliID = frm1.dupliID;
                if (frm1.ExistingTrackNo != "") ExistingTrackNo = frm1.ExistingTrackNo;
                IgnoreRest = false;
                if (frm1.IgnoreRest) IgnoreRest = frm1.IgnoreRest;

                timestamp = UpdateLog(timestamp, "REturing from child..", true);
                var tst = "Ignore";
                if (frm1.Asses != "") tst = frm1.Asses;
                frm1.Dispose();
                timestamp = UpdateLog(timestamp, "REturing.. to import", true);
                return tst;

            }
        }

        public string GetTExtFromFile(string ss)
        {

            var info = File.OpenText(ss);
            string tecst = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("astConversionDateTime"))
                {
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
            SaveSettings();
            pB_ReadDLCs.Value = 0;
            if (btn_RePack.Text == "Stop Repack")
            {
                btn_RePack.Text = "RePack";
                if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();// Cancel the asynchronous operation.
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

                timestamp = UpdateLog(timestamp, "Packing: ", true);
                Groupss = cbx_Groups.Text.ToString();

                var cmd = "SELECT * FROM Main ";
                if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
                else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")";

                cmd += " ORDER BY Artist";
                //Read from DB
                MainDBfields[] SongRecord = new MainDBfields[10000];
                SongRecord = GenericFunctions.GetRecord_s(cmd);
                var norows = SongRecord[0].NoRec.ToInt32();
                pB_ReadDLCs.Maximum = norows * 100;

                if (!bwRGenerate.IsBusy) bwRGenerate.RunWorkerAsync(data);
            }
        }

        public void GeneratePackage(object sender, DoWorkEventArgs e)
        {
            var startT = DateTime.Now;
            var tsst = "Start TH ..."; timestamp = UpdateLog(timestamp, tsst, false);
            var countpacked = 0;
            var counttransf = 0;
            var cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")"; ;

            string copyftp = "";
            cmd += " ORDER BY Artist";
            //Read from DB
            GenericFunctions.MainDBfields[] SongRecord = new GenericFunctions.MainDBfields[10000];
            SongRecord = GetRecord_s(cmd);
            var norows = SongRecord[0].NoRec.ToInt32();

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);

            var i = 0;
            var artist = "";
            foreach (var file in SongRecord)
            {
                if (i == norows || file.Folder_Name == null)
                    break;
                if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Additional_Manipulations.GetItemChecked(7))) //"8. Don't repack Broken songs")
                {
                    tsst = i + "Songg " + file.Artist + "-" + file.Album + "-" + file.Song_Title + "..."; timestamp = UpdateLog(timestamp, tsst, false);
                    var packagePlatform = file.Folder_Name.GetPlatform();
                    // REORGANIZE
                    var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                    // LOAD DATA

                    info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);

                    var bassRemoved = "No";
                    var DDAdded = "No";
                    if (chbx_Additional_Manipulations.GetItemChecked(63))//&& file.Remote_Path.IndexOf("psarc") > 0//64.@Pack Remove Remote File if GameData has been read
                    {
                        if (chbx_PS3.Checked)
                        {
                            var FTPPath = "";
                            if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                            else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                            //MainDB.DeleteFTPFiles(file.Remote_Path, FTPPath);
                        }
                        else
                        {
                            try
                            {
                                var tg = (txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path).Replace(".psarc", ".dupli");
                                if (File.Exists(txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path))
                                    File.Move(txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path, tg);
                            }
                            catch (Exception ex) { Console.Write(ex); }
                        }
                    }
                    tsst = "end load ..."; timestamp = UpdateLog(timestamp, tsst, false);
                    var h = "";
                    // Check and Get last Packed
                    if (chbx_Additional_Manipulations.GetItemChecked(64))
                    {
                        DataSet dbr = new DataSet(); dbr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\" and DLC_ID=" + file.ID + " ORDER BY ID DESC;", txt_DBFolder.Text);
                        var rec = dbr.Tables[0].Rows.Count;
                        if (rec > 0) h = dbr.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                    else
           if (chbx_Additional_Manipulations.GetItemChecked(65) && file.Available_Old == "Yes")
                    {

                        var oldfilePath = txt_TempPath.Text + "\\0_old\\" + file.Original_FileName;
                        if (oldfilePath.GetPlatform().platform.ToString() == (chbx_PC.Checked ? "Pc" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "Ps3" : "XBOX360"))
                        {
                            h = oldfilePath;
                        }
                        else
                        {
                            var SourcePlatform = new Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
                            var TargetPlatform = new Platform((chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360"), GameVersion.RS2014.ToString());

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
                            tsst = "start gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
                            if (needRebuildPackage)
                            {
                                data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
                                // Update AppID
                                if (!TargetPlatform.IsConsole)
                                    data.AppId = "248750";
                                // Build
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(targetFileName, data, new Platform(TargetPlatform.platform, GameVersion.RS2014));
                                tsst = "end generate ..."; timestamp = UpdateLog(timestamp, tsst, false);
                            }
                            else
                            {
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
                                var aggregateFile = Directory.EnumerateFiles(unpackedDir, "*.nt", System.IO.SearchOption.AllDirectories).FirstOrDefault();
                                var aggregateGraphText = File.ReadAllText(aggregateFile);
                                // Tags
                                aggregateGraphText = Regex.Replace(aggregateGraphText, GraphItem.GetPlatformTagDescription(SourcePlatform.platform), GraphItem.GetPlatformTagDescription(TargetPlatform.platform), RegexOptions.Multiline);
                                // Paths
                                aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir0, targetDir0, RegexOptions.Multiline);
                                aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir1, targetDir1, RegexOptions.Multiline);
                                File.WriteAllText(aggregateFile, aggregateGraphText);

                                // Rename directories
                                foreach (var dir in Directory.GetDirectories(unpackedDir, "*.*", System.IO.SearchOption.AllDirectories))
                                {
                                    if (dir.EndsWith(sourceDir0))
                                    {
                                        var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir0)) + targetDir0;
                                        //DirectoryExtension.SafeDelete(newDir);
                                        DeleteDirectory(newDir);
                                        DirectoryExtension.Move(dir, newDir);
                                    }
                                    else if (dir.EndsWith(sourceDir1))
                                    {
                                        var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
                                        //DirectoryExtension.SafeDelete(newDir);
                                        DeleteDirectory(newDir);
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
                                //DirectoryExtension.SafeDelete(unpackedDir);
                                DeleteDirectory(unpackedDir);
                            }

                            var s = txt_TempPath.Text + "\\0_old\\";
                            h = txt_TempPath.Text + "\\0_repacked\\" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\\";
                            h += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));
                            s += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));

                            if (File.Exists(h)) { DeleteFile(h); File.Move(s, h); }
                            else File.Copy(s, h, true);

                            //Generating the HASH code
                            var FileHash = "";
                            FileStream fs;
                            FileHash = GetHash(h);
                            using (fs = File.OpenRead(h))
                            {
                                //SHA1 sha = new SHA1Managed();
                                // BitConverter.ToString(sha.ComputeHash(fs));

                                System.IO.FileInfo fi = null; //calc file size
                                try { fi = new System.IO.FileInfo(h); }
                                catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

                                var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
                                var fnnon = Path.GetFileName(h);
                                var packn = h.Substring(0, h.IndexOf(fnnon));
                                var insertA = "\"" + h + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + h.GetPlatform().platform.ToString() + "\",\"" + Is_Original + "\"";
                                InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                                fs.Close();
                            }
                            tsst = "end gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                    }
                    else
                    {
                        tsst = "Song " + file.Artist + "-" + file.Album + "-" + file.Song_Title + "--" + file.ID + "..."; timestamp = UpdateLog(timestamp, tsst, false);
                        var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                        var platform = file.Folder_Name.GetPlatform();
                        if (chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                        {

                            foreach (var xml in xmlFiles)
                            {
                                Song2014 xmlContent = null;
                                try
                                {

                                    if (chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                                    {
                                        //ADD DD
                                        if (
                                                (false && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                                && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm") && file.Has_DD == "No") || (xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "No")
                                                )
                                                ||
                                                (false && (xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm")
                                                && file.Has_DD == "No" && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                                )
                                               )
                                        {
                                            xmlContent = Song2014.LoadFromFile(xml);
                                            tsst = "start add DD ..."; timestamp = UpdateLog(timestamp, tsst, false);
                                            File.Copy(xml, xml + ".woDD", true);
                                            DDAdded = (AddDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31), "5") == "Yes") ? "No" : "Yes";
                                            file.Has_BassDD = (DDAdded == "Yes") ? "Yes" : "No";
                                        }
                                    }
                                    //REMOVE DD
                                    if (file.Has_BassDD == "Yes")
                                    {
                                        xmlContent = Song2014.LoadFromFile(xml);
                                        if ((!(chbx_Additional_Manipulations.GetItemChecked(52) && file.Keep_BassDD == "Yes") && xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(5))
                                                || (!(chbx_Additional_Manipulations.GetItemChecked(53) && file.Keep_DD == "Yes") && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm"))
                                                  && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(3))
                                               )
                                        {
                                            if (chbx_Additional_Manipulations.GetItemChecked(5) && !chbx_Additional_Manipulations.GetItemChecked(3) && !(xmlContent.Arrangement.ToLower() == "bass")) continue;
                                            bassRemoved = (RemoveDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31)) == "Yes") ? "Yes" : "No";
                                            file.Has_BassDD = (bassRemoved == "Yes") ? "No" : "Yes";
                                            tsst = "end remove DD ..."; timestamp = UpdateLog(timestamp, tsst, false);
                                        }
                                    }
                                }
                                catch (Exception ee)
                                {
                                    Console.Write(ee.Message);
                                }

                            }
                        }

                        //Default APP ID
                        if (chbx_Additional_Manipulations.GetItemChecked(43)) file.DLC_AppID = ConfigRepository.Instance()["general_defaultappid_RS2014"];


                        //get track no
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") //59. @Pack try to get Track No again (&don't save)                        
                        {
                            string z = (MainDB.GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle(info.SongInfo.SongDisplayName))).ToString();
                            file.Track_No = z == "0" && file.Track_No != "" ? file.Track_No : z;
                            // Multithreading and DB access nnot supported
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes" && file.Track_No != "0" && file.Track_No != "-1" && file.Track_No != "") //60.@Pack try to get Track No, again(&save)
                            {
                                UpdateDB("Main", "UPDATE Main SET Track_No=\"" + file.Track_No + "\" WHERE ID=" + file.ID + ";");
                            }
                        }

                        //Gather song Lenght
                        var duration = "";
                        var bitrate = 250001;
                        var bitratep = 250001;
                        var SampleRate = 45001;
                        var recalc_Preview = false;
                        if (file.oggPreviewPath != null && file.oggPreviewPath != "")
                            if (chbx_Additional_Manipulations.GetItemChecked(55))
                            {
                                using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
                                { duration = vorbis.TotalTime.ToString(); bitratep = vorbis.NominalBitrate; }

                                string[] timepieces = duration.Split(':');
                                if (timepieces[0] != "00" || timepieces[1] != "00")
                                    recalc_Preview = true;

                                //check Audio bitrate as originals are always at 128..
                                if (chbx_Additional_Manipulations.GetItemChecked(69))
                                    using (var vorbis = new NVorbis.VorbisReader(file.OggPath))
                                    {
                                        bitrate = vorbis.NominalBitrate;
                                        SampleRate = vorbis.SampleRate;
                                    }
                            }
                        //Conver Audio to lower bitrate
                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
                        if (chbx_Additional_Manipulations.GetItemChecked(69) && file.AudioPath != null && (bitrate > ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() + 8000))
                        {
                            tsst = "START set main audio reconv ..." + bitrate; timestamp = UpdateLog(timestamp, tsst, false);
                            if (WwiseInstalled())
                            {
                                Downstream(file.AudioPath);
                                using (var vorbis = new NVorbis.VorbisReader(file.OggPath))
                                {
                                    bitrate = vorbis.NominalBitrate;
                                    SampleRate = vorbis.SampleRate;
                                }

                                //save new new hash
                                cmd = "UPDATE Main SET ";
                                var audio_hash = "";
                                audio_hash = GetHash(file.AudioPath);
                                cmd += "Audio_Hash=\"" + audio_hash + "\", audioBitrate=\"" + bitrate + "\"";
                                cmd += ",audioSampleRate=\"" + SampleRate + "\"";
                                cmd += " WHERE ID=" + file.ID;
                                DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
                            }
                            tsst = "end set main audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(70))
                        {
                            GenericFunctions.Converters(file.oggPreviewPath, GenericFunctions.ConverterTypes.Ogg2Wem, false);
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", ".wav"));
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.wav"));
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
                            DeleteFile(file.oggPreviewPath.Replace(".ogg", ".wav"));
                            DeleteFile(file.oggPreviewPath.Replace(".ogg", "_preview.wav"));
                            DeleteFile(file.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
                            tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                        if (chbx_Additional_Manipulations.GetItemChecked(71))
                        {
                            //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
                            //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
                            tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }

                        //Set Preview
                        if ((chbx_Additional_Manipulations.GetItemChecked(9) && file.oggPreviewPath == null ||
                            (chbx_Additional_Manipulations.GetItemChecked(55) && (file.AudioPreview_Hash == file.Audio_Hash
                            || file.Song_Lenght == file.PreviewLenght || recalc_Preview))))
                        {
                            //delete old previews!
                            if (file.oggPreviewPath != null) DeleteFile(file.oggPreviewPath);
                            if (file.audioPreviewPath != null) DeleteFile(file.audioPreviewPath);

                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
                            startInfo.WorkingDirectory = AppWD;
                            var t = file.OggPath;
                            var tt = t.Replace(".ogg", "_preview.ogg");
                            var times = ConfigRepository.Instance()["dlcm_PreviewStart"];
                            string[] timepieces = times.Split(':');
                            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                            startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                                t,
                                                                tt,
                                                                r.TotalMilliseconds,
                                                                (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                            //save new previews
                            cmd = "UPDATE Main SET ";
                            if (File.Exists(file.oggPreviewPath))
                                if (PreviewLenght == "" || PreviewLenght == null)
                                    using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
                                    {
                                        var durations = vorbis.TotalTime;
                                        bitratep = vorbis.NominalBitrate;
                                        PreviewLenght = durations.ToString();
                                    }
                            var audioPreview_hash = "";
                            audioPreview_hash = GetHash(file.audioPreviewPath);

                            cmd += " audioPreviewPath=\"" + file.audioPreviewPath + "\"" + " , audioPreview_Hash=\"" + audioPreview_hash + "\"" + " , PreviewTime=\"" + times + "\", audioBitrate =\"" + bitratep + "\"";
                            cmd += " , oggPreviewPath=\"" + file.oggPreviewPath + "\" , PreviewLenght=\"" + (PreviewLenght.IndexOf(":") > 0 ? (PreviewLenght.Split(':'))[2] : PreviewLenght) + "\"";// previewN + "\"";

                            cmd += " WHERE ID=" + file.ID;
                            DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
                            tsst = "end set preview ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }

                        //compress Preview
                        if (chbx_Additional_Manipulations.GetItemChecked(69) && file.audioPreviewPath != null && (bitratep > ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() + 8000))
                        {
                            tsst = "start set preview audio reconv ..." + bitratep; timestamp = UpdateLog(timestamp, tsst, false);
                            if (WwiseInstalled())
                            {
                                Downstream(file.audioPreviewPath);
                                //save new new hash
                                cmd = "UPDATE Main SET ";
                                var audio_previewhash = "";
                                audio_previewhash = GetHash(file.audioPreviewPath);
                                cmd += "audioPreview_Hash=\"" + audio_previewhash + "\"";
                                cmd += " WHERE ID=" + file.ID;
                                DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
                            }
                            tsst = "end set preview audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
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

                        if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) //24.Pack with The/ Die only at the end of Title Sort 
                        {
                            if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4)
                                file.Song_Title_Sort = MoveTheAtEnd(file.Song_Title_Sort);
                            file.Artist_Sort = MoveTheAtEnd(file.Artist_Sort);
                        }

                        var toolkitv = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
                        if (chbx_Additional_Manipulations.GetItemChecked(47)) toolkitv.PackageVersion = ToolkitVersion.version.ToString();
                        else toolkitv.PackageVersion = file.Version;
                        data = new DLCPackageData
                        {
                            GameVersion = GameVersion.RS2014,
                            Pc = false,
                            Mac = false,
                            XBox360 = false,
                            PS3 = false,
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
                        };
                        var rrt = ConfigRepository.Instance()["general_defaultauthor"];
                        if ((file.Author == "Custom Song Creator" || file.Author == "") && rrt != "Custom Song Creator" && chbx_Additional_Manipulations.GetItemChecked(47))
                            file.Author = "RepackedBy" + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
                        if (chbx_Additional_Manipulations.GetItemChecked(54))
                            file.Is_Beta = "Yes";
                        var tkInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
                        //TO FIX
                        // data.ToolkitInfo.PackageAuthor = file.Author.ToString();
                        //data.ToolkitInfo.PackageVersion = file.Version.ToString();
                        tsst = "end load vars..."; timestamp = UpdateLog(timestamp, tsst, false);

                        var norm_path = txt_TempPath.Text + "\\0_repacked\\" + ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                        //manipulating the info
                        var st = "";
                        var sa = "";
                        if (cbx_Activ_Title.Checked)
                            data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");

                        if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4) { st = file.Song_Title; file.Song_Title = MoveTheAtEnd(file.Song_Title); }
                        if (cbx_Activ_Title.Checked)
                            data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
                        if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4) file.Song_Title = st;
                        if (file.Is_Beta == "Yes") if (Groupss != "") data.SongInfo.SongDisplayNameSort = "0" + Groupss + data.SongInfo.SongDisplayNameSort.Substring(1, data.SongInfo.SongDisplayNameSort.Length - 2); //).Replace("][", "-").Replace("]0", "")

                        if (cbx_Activ_Artist.Checked)
                            data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");

                        if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) { sa = file.Artist; file.Artist = MoveTheAtEnd(file.Artist); }
                        if (cbx_Activ_Artist_Sort.Checked)
                            data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
                        if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) file.Artist = sa;

                        if (cbx_Activ_Album.Checked)
                            data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");
                        if (chbx_Additional_Manipulations.GetItemChecked(0)) //"1. Add Increment to all Titles"
                            data.Name = i + data.Name;

                        artist = "";
                        if (chbx_Additional_Manipulations.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
                        {
                            if (i > 0)
                                if (data.SongInfo.Artist == Files[i - 1].Artist) no_ord += 1;
                                else no_ord = 1;
                            else no_ord += 1;
                            artist = no_ord + " ";
                            data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(2))
                            //"3. Make all DLC IDs unique (&save)"
                            if (file.UniqueDLCName != null && file.UniqueDLCName != "") data.Name = file.UniqueDLCName;
                            else
                            {
                                Random random = new Random();
                                data.Name = random.Next(0, 100000) + data.Name;
                            }
                        tsst = "end Advanced setting params ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        //Fix the _preview_preview issue
                        var ms = data.OggPath;
                        var tst = "";
                        try
                        {
                            var sourceAudioFiles = Directory.GetFiles(file.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);

                            foreach (var fil in sourceAudioFiles)
                            {
                                tst = fil;
                                if (fil.LastIndexOf("_preview_preview.wem") > 0)
                                {
                                    ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                                    File.Move((ms + "_preview.wem"), (ms + ".wem"));
                                    File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                                }
                            }
                        }
                        catch (Exception ee) { Console.WriteLine(ee.Message); }
                        if (data == null)
                        {
                            MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var FN = txt_File_Name.Text;

                        if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(FN, i, true, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
                        if (file.Is_Beta == "Yes") if (Groupss != "") FN = "0" + Groupss + FN.Substring(1, FN.Length - 2);//.Replace("][", "-").Replace("]0", "");

                        if (file.Is_Alternate == "Yes" && file.Author != "Custom Song Creator" && file.Author == "" && rrt != "Custom Song Creator" && !chbx_Additional_Manipulations.GetItemChecked(47))
                            FN += "a." + file.Alternate_Version_No + file.Author;


                        if (chbx_Additional_Manipulations.GetItemChecked(8) || chbx_PS3.Checked)
                        {
                            FN = FN.Replace(".", "_");
                            FN = FN.Replace(" ", "_");
                        }

                        dlcSavePath = txt_TempPath.Text + "\\0_repacked\\" + (chbx_XBOX360.Checked ? "XBOX360" : chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "") + "\\" + FN;

                        int progress = (i + 1) * 100;
                        errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        numPlatforms++;
                        if (chbx_Mac.Checked)
                            numPlatforms++;
                        if (chbx_XBOX360.Checked)
                            numPlatforms++;
                        if (chbx_PS3.Checked)
                            numPlatforms++;

                        var DBc_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
                        tsst = "start gen " + data.SongInfo.Artist + "-" + data.SongInfo.Album + "-" + data.SongInfo.SongDisplayName + "..."; timestamp = UpdateLog(timestamp, tsst, false);
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
                        else
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
                        else
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
                        else
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
                        tsst = "end gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        data.CleanCache();
                        i++;
                        var source = chbx_PS3.Checked ? "_ps3.psarc.edat" : (chbx_PC.Checked ? "_p" : (chbx_Mac.Checked ? "_m" : "")) + ".psarc";

                        if (File.Exists(dlcSavePath + source))
                        {
                            //copyftp
                            string txt_FTPPat = "";
                            var dest = "";
                            countpacked++;
                            if (chbx_PS3.Checked && chbx_Additional_Manipulations.GetItemChecked(49))
                            {
                                if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP1"];
                                else txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP2"];
                                dest = txt_TempPath.Text;
                                var a = FTPFile(txt_FTPPat, dlcSavePath + source, txt_TempPath.Text, "");
                                copyftp = " and " + a + "FTPed";
                                if (a == "Truly ") counttransf++;
                            }
                            else if ((chbx_PC.Checked || chbx_Mac.Checked) && chbx_Additional_Manipulations.GetItemChecked(49))
                            {
                                dest = txt_RocksmithDLCPath.Text + "\\" + FN + source;
                                if (File.Exists(dlcSavePath + source))
                                    try
                                    {
                                        File.Copy(dlcSavePath + source, dest, true);
                                        DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + FN + source + "\";");
                                    }
                                    catch (Exception ee)
                                    {
                                        copyftp = "Not "; Console.Write(ee.Message);

                                    }
                                copyftp = " and " + copyftp + "Copied";
                                if (copyftp != "Not ") counttransf++;
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
                                timestamp = UpdateLog(timestamp, "error at pack details save", true);
                                ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                                frm1.ShowDialog();
                            }

                            //Generating the HASH code
                            var FileHash = "";
                            FileHash = GetHash(dlcSavePath + source);
                            //using (FileStream fs = File.OpenRead(dlcSavePath + source))
                            //{
                            //    SHA1 sha = new SHA1Managed();
                            //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                            //    fs.Close();
                            //}

                            var norec = 0;
                            var fnn = dlcSavePath + source;
                            var fnnon = Path.GetFileName(fnn);
                            var packn = fnn.Substring(0, fnn.IndexOf(fnnon)).Substring(0, fnn.Substring(0, fnn.IndexOf(fnnon)).Length - 1);
                            var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\" OR (FileName=\"" + fnnon + "\" AND PackPath=\"" + packn + "\");";
                            DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel, txt_DBFolder.Text);
                            norec = dfs.Tables[0].Rows.Count;
                            if (norec > 0) DeleteFromDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE ID IN (" + sel + ")");
                            string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
                            var insertA = "\"" + dest + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + ((chbx_XBOX360.Checked ? "XBOX360" : (chbx_PC.Checked ? "PC" : (chbx_Mac.Checked ? "MAC" : (chbx_PS3.Checked ? "PS3" : ""))))) + "\",\"" + Is_Original + "\"";
                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                            tsst = "end add pack_audit ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                        //Restore the DDremoved copies
                        xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml.old", System.IO.SearchOption.AllDirectories);
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
                                            json = xml.Replace("songs\\arr", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", System.IO.SearchOption.AllDirectories)[0])).Replace(".xml", ".json");

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
                }
            }
            MessageBox.Show("Repack done " + countpacked + "/" + counttransf + " " + copyftp.Replace("  ", " "));
            string endtmp = (startT - DateTime.Now).ToString();
            // timestamp = UpdateLog(timestamp, "Ended " + endT + " (" + startT + ") after " + endtmp, true);
            tsst = "end repack ..." + "The End " + timestamp + " (" + startT + ") after " + endtmp; timestamp = UpdateLog(timestamp, tsst, false);
        }


        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
                pB_ReadDLCs.Value = e.ProgressPercentage;
            else
                pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;

            ShowCurrentOperation(e.UserState as string);
        }
        private void ShowCurrentOperation(string message)
        {
            //currentOperationLabel.Text = message;
            //currentOperationLabel.Refresh();
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
            chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked);

        }


        private void chbx_HomeDGBVM_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp\\to import";
            txt_DBFolder.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp";
            txt_TempPath.Text = "Z:\\HFS\\Users\\bogdan\\GitHub\\tmp\\to import\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
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


        private void renamedir(string source_dir, string destination_dir)
        {
            try
            {
                Directory.Move(source_dir, destination_dir);
            }
            catch (System.IO.FileNotFoundException ee)
            {
                Console.WriteLine(ee.Message);
                timestamp = UpdateLog(timestamp, "Error cleaning Moving folder: " + source_dir, true);
            }
        }

        private void DBchanged(object sender, EventArgs e)
        {
            //if (chbx_DefaultDB.Checked == true) chbx_DefaultDB.Checked = false;
            // SaveSettings();
            if (!(txt_DBFolder.Text == MyAppWD)) chbx_DefaultDB.Checked = false;
            if (txt_DBFolder.Text.Length > 0) if ((txt_DBFolder.Text.Substring(txt_DBFolder.Text.Length - 1, 1) == "\\")) txt_DBFolder.Text = txt_DBFolder.Text.Substring(0, txt_DBFolder.Text.Length - 1);
        }

        private void chbx_DefaultDB_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_DefaultDB.Checked == true) txt_DBFolder.Text = MyAppWD;
        }

        private void btn_LoadRetailSongs_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Starting Retail Songs processing ...." + DateTime.Now, true);

            var Temp_Path_Import = txt_TempPath.Text + "\\dlcpacks";
            string pathDLC = txt_RocksmithDLCPath.Text;
            if (!chbx_DebugB.Checked) MessageBox.Show("Please make sure one of the following Retail Packs:\ncache.psarc, songs.psarc, rs1compatibilitydisc.psarc(.edat if PS3 format), rs1compatibilitydlc.psarc(.edat) \n\n, are in the Import Folder: " + pathDLC + "\n\nAlso, make sure you have enought space for the packing&unpacking operations Platform x 3GB");
            CreateTempFolderStructure(txt_TempPath.Text, txt_TempPath.Text + "\\0_old", txt_TempPath.Text + "\\0_broken", txt_TempPath.Text + "\\0_duplicate", txt_TempPath.Text + "\\0_dlcpacks", pathDLC, txt_TempPath.Text + "\\0_Repacked", txt_TempPath.Text + "\\0_Repacked\\XBOX", txt_TempPath.Text + "\\0_Repacked\\PC", txt_TempPath.Text + "\\0_Repacked\\MAC", txt_TempPath.Text + "\\0_Repacked\\PS", ConfigRepository.Instance()["dlcm_LogPath"]);

            //read all the .PSARCs in the IMPORT folder
            var jsonFiles = Directory.GetFiles(pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014"), "*.psarc*", System.IO.SearchOption.AllDirectories);
            if (pathDLC.IndexOf("Rocksmith2014\\DLC") == 0) jsonFiles = Directory.GetFiles(pathDLC, "*.psarc*", System.IO.SearchOption.AllDirectories);

            var inputFilePath = ""; var locat = ""; var songshsanP = ""; var unpackedDir = "";
            var DBb_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
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
                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                    {
                        if (dir.Name != "manipulated" && dir.Name != "manifests" && dir.Name != "temp") DeleteDirectory(dir.FullName);
                    }


                    foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                    {
                        DeleteFile(file.FullName);
                    }

                    //clear content of dlcpacks\\manipulated\temp folder
                    System.IO.DirectoryInfo downloadedMMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manipulated\\temp");
                    foreach (DirectoryInfo dir in downloadedMMessageInfo.GetDirectories())
                    {
                        DeleteDirectory(dir.FullName);
                    }

                    foreach (FileInfo file in downloadedMMessageInfo.GetFiles())
                    {
                        DeleteFile(file.FullName);
                    }

                    System.IO.DirectoryInfo downloadedMmMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manifests");
                    foreach (DirectoryInfo dir in downloadedMmMessageInfo.GetDirectories())
                    {
                        DeleteDirectory(dir.FullName);
                    }

                    foreach (FileInfo file in downloadedMmMessageInfo.GetFiles())
                    {
                        DeleteFile(file.FullName);
                    }
                }
                catch (System.IO.FileNotFoundException ee)
                {
                    Console.WriteLine(ee.Message);
                    timestamp = UpdateLog(timestamp, "Error cleaning Temp Folder Cleaned", true);
                    //continue;
                }
            }

            //Clean CachetDB
            DeleteFromDB("Cache n", "DELETE FROM Cache;");
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 2 * 3; //jsonFiles.Length
                                         //UNPACK x3 psarcs
            foreach (var json in jsonFiles)
            {
                platformDLC = json.GetPlatform(); //Platform 
                platformDLCP = platformDLC.platform.ToString();
                if (json == pathDLC + "\\songs.psarc" || json == pathDLC + "\\rs1compatibilitydlc.psarc.edat" || json == pathDLC + "\\rs1compatibilitydisc.psarc.edat" || ((json == pathDLC + "\\rs1compatibilitydlc_p.psarc" || json == pathDLC + "\\rs1compatibilitydisc_p_Pc.psarc") && platformDLCP == "Pc") || ((json == pathDLC + "\\rs1compatibilitydlc_m.psarc" || json == pathDLC + "\\rs1compatibilitydisc_m.psarc") && platformDLCP == "Mac"))
                {
                    timestamp = UpdateLog(timestamp, "Decompressing  " + ".... " + json, true);
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
                                timestamp = UpdateLog(timestamp, "Unpacking cache.psarc.... ", true);
                                if (File.Exists(pathDLC + "\\cache.psarc"))
                                {

                                    unpackedDir = Packer.Unpack(pathDLC + "\\cache.psarc", txt_TempPath.Text + "\\0_dlcpacks\\temp", false, false); //, falseUnpack cache.psarc for RS14 Official Retails songs rePACKING

                                    //check if platform is correctly identified, &if NOT, correct it
                                    var startI = new ProcessStartInfo();
                                    startI.FileName = Path.Combine(AppWD, "7za.exe");
                                    startI.WorkingDirectory = unpackedDir;
                                    var za = unpackedDir + "\\cache8.7z";
                                    startI.Arguments = String.Format(" x {0} -o{1}",
                                                                        za,
                                                                        unpackedDir.Replace("\\cache_Pc", "\\cache_Pc\\manipulated"));
                                    startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
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
                                        if (dir.Name == "manipulated") DeleteDirectory(dir.FullName);
                                    }

                                    //move cache_xx to dlcpacks
                                    if (Directory.Exists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"))) DeleteDirectory(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                    renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                    unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks");
                                }

                                //Process SONGS.PSARC
                                timestamp = UpdateLog(timestamp, "Unpacking songs.psarc.... ", true);
                                unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", false, false);

                                //FIX for unpacking w the wrong folder extension
                                //And unpacking of PS3 WEM
                                if (Directory.Exists(unpackedDir + "\\songs\\bin\\ps3"))
                                {
                                    //Convert WEM to OGG
                                    //if (platformDLCP == "PS3")
                                    //{
                                    var startInfo = new ProcessStartInfo();
                                    startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                    startInfo.WorkingDirectory = unpackedDir;
                                    startInfo.Arguments = String.Format(" --unpack --version=RS2014 --platform={0} --output={1} --input={2} --decodeogg",
                                                                        platformDLCP,
                                                                        unpackedDir.Replace("songs_Pc", ""),
                                                                        inputFilePath);
                                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 15); //wait 15min                                                                                                               
                                    }
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_ps3"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_ps3");
                                    platformDLCP = "PS3";
                                }
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", true, false);

                                if (Directory.Exists(unpackedDir + "\\songs\\bin\\macos"))
                                {
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_Mac"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_Mac");
                                    platformDLCP = "Mac";
                                }

                                //move cache_xx to dlcpacks
                                if (Directory.Exists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"))) DeleteDirectory(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks");

                                songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                            }
                            catch (Exception ex)
                            {
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + inputFilePath + "---" + txt_TempPath.Text + "\\0songs", true);
                            }
                        }
                        else
                        {
                            unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\songs_" + platformDLCP;
                            songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                        }

                        timestamp = UpdateLog(timestamp, "Processed cache.psarc & songs.psarc", true);
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
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + unpackedDir, true);
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                        timestamp = UpdateLog(timestamp, "Repacking " + json + "2 use the internal/Browser Psarc Read function.... ", true);
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                            renamedir(unpackedDir + "\\manifests\\songs_rs1dlc", unpackedDir + "\\manifests\\songs");

                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
                            startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 5); //wait 5min
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1dlc DLC pack !", true);
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            timestamp = UpdateLog(timestamp, "renaming internal folder ", true);
                        }
                        catch (Exception ex)
                        {
                            timestamp = UpdateLog(timestamp, ex.Message + "problem at dir rename" + unpackedDir, true);
                        }
                        timestamp = UpdateLog(timestamp, "Processed rs1compatibilitydlc.psarc", true);
                    }
                    else if (json == pathDLC + "\\rs1compatibilitydisc.psarc.edat" || (json == pathDLC + "\\rs1compatibilitydisc_p_Pc.psarc" && platformDLCP == "Pc") || (json == pathDLC + "\\rs1compatibilitydisc_m.psarc" && platformDLCP == "Mac")) //RS12 RETAIL
                    {
                        inputFilePath = json; locat = "RS1Retail";
                        if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try // UNPACK
                            {
                                if (platformDLCP == "PS3")
                                {
                                    //Packer.Unpack fails
                                    var startInfo = new ProcessStartInfo();
                                    startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                    startInfo.WorkingDirectory = unpackedDir;
                                    startInfo.Arguments = String.Format(" --unpack --decodeogg --version=RS2014 --platform={0} --output={1} --input={2}",
                                                                        platformDLCP,
                                                                        txt_TempPath.Text + "\\0_dlcpacks",
                                                                        inputFilePath);
                                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 1min
                                        if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1dlc DLC pack !", true);
                                    }
                                    unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                }
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", true, false);
                            }
                            catch (Exception ex)
                            {
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + unpackedDir, true);
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                        timestamp = UpdateLog(timestamp, "Repacking " + json + " 2 use the internal/Browser Psarc Read function.... " + json, true);
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                            renamedir(unpackedDir + "\\manifests\\songs_rs1disc", unpackedDir + "\\manifests\\songs");
                            var startInfo = new ProcessStartInfo();
                            startInfo.FileName = Path.Combine(AppWD, "psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc.psarc";
                            startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 10min
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1disc pack !", true);

                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                                renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                                timestamp = UpdateLog(timestamp, "renaming internal folder", true);
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
                                var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", System.IO.SearchOption.AllDirectories);
                                var i = 0;
                                foreach (var wem in wemFiles)
                                {

                                    i++;
                                    startInfo = new ProcessStartInfo();

                                    startInfo.FileName = Path.Combine(AppWD, "ww2ogg.exe");
                                    startInfo.WorkingDirectory = AppWD;
                                    startInfo.Arguments = String.Format(" {0} -o {1} --pcb packed_codebooks_aoTuV_603.bin",
                                                                        wem,
                                                                        wem.Replace(".wem", ".ogg"));
                                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                                    if (File.Exists(wem))
                                        using (var DDC = new Process())
                                        {
                                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 30 * 60); //wait 30min
                                        }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            timestamp = UpdateLog(timestamp, ex.Message + "problem at dir rename" + unpackedDir, true);
                        }
                        timestamp = UpdateLog(timestamp, "Processed rs1compatibilitydisc.psarc", true);
                    }
                    else continue;
                    Console.WriteLine("Opening archive {0} ...", inputFilePath);
                    Console.WriteLine();



                    //Populate DB
                    timestamp = UpdateLog(timestamp, "Populating CACHE DB", true);
                    var pic = "";
                    var browser = new PsarcBrowser(t);
                    var songList = browser.GetSongList();
                    var toolkitInfo = browser.GetToolkitInfo();
                    var AudioP = "";
                    var AudioPP = "";
                    var AudioP1 = "";
                    var AudioPP1 = "";
                    foreach (var song in songList)
                    {
                        DataSet dsx = new DataSet(); dsx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "\"", txt_DBFolder.Text);

                        AudioP1 = (dsx.Tables[0].Rows.Count > 0) ? dsx.Tables[0].Rows[0].ItemArray[0].ToString() : "";

                        DataSet dsdx = new DataSet(); dsdx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "_preview\"", txt_DBFolder.Text);
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

                        DataSet dtx = new DataSet(); dtx = SelectFromDB("Cache", "SELECT ID from Cache AS O WHERE Platform=\"" + platformDLCP + "\" AND Identifier=\"" + song.Identifier.ToString() + "\"", txt_DBFolder.Text);
                        var aa = dtx.Tables[0].Rows.Count;
                        if (dtx.Tables[0].Rows.Count == 0) //If this record isn't already in the DB...add it

                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                            {
                                var commands = cnn.CreateCommand();
                                commands.CommandText = "INSERT INTO Cache(";
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
                                timestamp = UpdateLog(timestamp, Environment.NewLine, true);
                            }
                    }
                }//END no cache.psarc to be decompressed
            }
            timestamp = UpdateLog(timestamp, "Ending Retail Songs processing ...." + DateTime.Now, true);
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
            if (!bwConvert.IsBusy)
            {
                bwConvert.RunWorkerAsync(unpackedDir1);
            }
            else
            {
                MessageBox.Show("Error when multithreading PS3 WEM conv to OGG");
            }


            timestamp = UpdateLog(timestamp, "Ended Decompressing WEMs", true);
        }

        public void ConvertWEM(object sender, DoWorkEventArgs e)
        {
            var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", System.IO.SearchOption.AllDirectories);
            var i = 0;
            foreach (var wem in wemFiles)
            {

                i++;
                var startInfo = new ProcessStartInfo();

                startInfo.FileName = Path.Combine(AppWD, "audiocrossreference.exe");
                startInfo.WorkingDirectory = unpackedDir1;
                startInfo.Arguments = String.Format(" {0}",
                                                    wem);
                startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;
                if (File.Exists(wem))
                    using (var DDC = new Process())
                    {
                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 60); //wait min
                        Console.WriteLine("{0} is active: {1}", DDC.Id, !DDC.HasExited);
                        DDC.Kill();
                    }
            }
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            var xx = Path.Combine(AppWD, "MDBPlus.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            startInfo.Arguments = String.Format(" " + DB_Path);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        

        private void cbx_Groups_DropDown(object sender, EventArgs e)
        {
            //populaet the Group  Dropdown
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";", txt_DBFolder.Text);
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
                for (int j = 0; j < norec; j++)
                {
                    var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    cbx_Groups.Items.Add(tem);
                }
            }
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
            if (chbx_Configurations.Text == "Select Profile") return;

            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" and Type=\"Profile\";", txt_DBFolder.Text);
            var norec = drs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text);

                norec = ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string insertcmdA = "CDLC_ID, Groups, Type, Comments, Profile_Name";
                    var insertA = "\"" + fnn + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"Profile\",\"Rocksmith\",\"" + chbx_Configurations.Text + "\"";
                    InsertIntoDBwValues("Groups", insertcmdA, insertA);

                    insertA = "\"" + fnn + "\",\"" + txt_TempPath.Text + "\",\"Profile\",\"Temp\",\"" + chbx_Configurations.Text + "\"";
                    InsertIntoDBwValues("Groups", insertcmdA, insertA);

                    insertA = "\"" + fnn + "\",\"" + txt_DBFolder.Text + "\",\"Profile\",\"DB\",\"" + chbx_Configurations.Text + "\"";
                    InsertIntoDBwValues("Groups", insertcmdA, insertA);
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Do you Wanna rename the profile?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) { DataSet dts = new DataSet(); dts = UpdateDB("Groups", "UPDATE Groups SET Profile_Name=\"" + chbx_Configurations.Text + "\" WHERE CDLC_ID=" + dts.Tables[0].Rows[0].ItemArray[0].ToString() + "and Type=\"Profile\";"); }
                else MessageBox.Show("Please chose a unique name");
            }
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
        }

        private void chbx_Configurations_DropDown(object sender, EventArgs e)
        {
            //populate the Group  Dropdown
            if (File.Exists(txt_DBFolder.Text + "\\Files.accdb"))
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text);
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
                    for (int j = 0; j < norec; j++)
                    {
                        var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                        chbx_Configurations.Items.Add(tem);
                    }
                }
            }
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            if (chbx_Configurations.Text == "Select Profile") return;
            var cmd = "DELETE FROM Groups WHERE Type=\"Profile\" AND Profile_Name= \"" + chbx_Configurations.Text + "\"";
            DeleteFromDB("Groups", cmd);
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

        private void chbx_Configurations_TextChanged(object sender, EventArgs e)
        {
            //DialogResult result1 = MessageBox.Show("", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result1 == DialogResult.Yes) ;
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
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT Groups FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\"", txt_DBFolder.Text);
            var norec = 0;
            if (ds.Tables.Count > 0) norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                if (chbx_Configurations.Text == ConfigRepository.Instance()["dlcm_DebugProfile"])
                {
                    chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked);
                    chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Unchecked);
                    chbx_CleanDB.Checked = true;
                    chbx_CleanTemp.Checked = true;
                    chbx_DebugB.Checked = true;
                }
                if (chbx_Configurations.Text == "Select Profile") return;
                txt_RocksmithDLCPath.Text = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txt_DBFolder.Text = ds.Tables[0].Rows[2].ItemArray[0].ToString();
                txt_TempPath.Text = ds.Tables[0].Rows[1].ItemArray[0].ToString();

            }
        }


        //string GetHash(string fn)
        //{
        //    //save new new hash
        //    if (File.Exists(fn))
        //    using (FileStream fs = File.OpenRead(fn))
        //    {
        //        SHA1 sha = new SHA1Managed();
        //        return BitConverter.ToString(sha.ComputeHash(fs));
        //    }
        //    else timestamp = UpdateLog(timestamp, fn + "problem at hash file does not exists", true);
        //    //fs.Close();
        //    return "";
        //}

        private void button1_Click_1(object sender, EventArgs e)
        {
            //var t =;
            if (Directory.Exists(logPath))
            {
                try
                {
                    Process process = Process.Start(@logPath);
                    btn_OpenLogsFolder.Text = btn_OpenLogsFolder.Text.Replace(" N/A", "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Duplicate folder in Exporer !");
                }
            }
            else btn_OpenLogsFolder.Text = btn_OpenLogsFolder.Text + " N/A";
        }

        string RepackP = "";
        private void chbx_XBOX360_CheckedChanged(object sender, EventArgs e)
        {
            RepackP = txt_TempPath.Text + "\\0_repacked\\XBOX360";
        }


        private void chbx_Mac_CheckedChanged(object sender, EventArgs e)
        {
            RepackP = txt_TempPath.Text + "\\0_repacked\\MAC";
        }

        private void chbx_PS3_CheckedChanged(object sender, EventArgs e)
        {
            RepackP = txt_TempPath.Text + "\\0_repacked\\PS3";
        }

        private void chbx_PC_CheckedChanged(object sender, EventArgs e)
        {
            RepackP = txt_TempPath.Text + "\\0_repacked\\PC";
        }

        private void btm_GoRepack_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(@RepackP);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void btn_RefreshSelected_Click(object sender, EventArgs e)
        {
            if (rbtn_Population_All.Checked) RefreshSelectedStat("Main", "1=1");
            else if (rbtn_Population_Groups.Checked) RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groups=\"" + cbx_Groups.Text + "\")");
            else if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")");
            //SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")";
        }

        private void cbx_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groups=\"" + cbx_Groups.Text + "\")");
        }

        private void rbtn_Population_Selected_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")");
        }

        private void rbtn_Population_All_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Population_All.Checked) RefreshSelectedStat("Main", "1=1");
        }

        private void btn_Enable_CDLC_Click(object sender, EventArgs e)
        {
            var hexw = GetHash(AppWD + "\\CDLCEnablers\\D3DX9_42.dll");
            var hexm1 = GetHash(AppWD + "\\CDLCEnablers\\insert_dylib");
            var hexm2 = GetHash(AppWD + "\\CDLCEnablers\\libRSBypass.dylib");
            if (File.Exists(txt_RocksmithDLCPath.Text + "\\..\\D3DX9_42.dll"))
                if (hexw != GetHash(txt_RocksmithDLCPath.Text + "\\..\\D3DX9_42.dll"))
                {
                    DialogResult result1 = MessageBox.Show("Windows patch doesnt seem to be installed" + "\n\nChose:\n\n1. Overrite\n2. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                        File.Copy(AppWD + "\\CDLCEnablers\\D3DX9_42.dll", txt_RocksmithDLCPath.Text + "\\..\\D3DX9_42.dll", true);

                }
            if (File.Exists(txt_RocksmithDLCPath.Text + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\Rocksmith2014"))
                if (hexm1 != GetHash(txt_RocksmithDLCPath.Text + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\libRSBypass.dylib") || hexm1 != GetHash(txt_RocksmithDLCPath.Text + "..\\Rocksmith2014.app\\Contents\\MacOS\\insert_dylib"))
                {
                    string line = "";
                    string lines = "";
                    var df = "";
                    File.Copy(AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.orig.command", AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command", true);
                    var info = File.OpenText(AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command");
                    while ((line = info.ReadLine()) != null)
                    {
                        if (line.Contains("RS_PATH="))
                        {
                            //lines += "RS_PATH="+txt_RocksmithDLCPath.Text.Replace("\\dlc","")+ "Rocksmith2014.app/Contents/MacOS" + "\n"; //.Replace("\\","/") 
                            df = "RS_PATH=\"/Volumes/HFS/" + txt_RocksmithDLCPath.Text.Replace("\\dlc", "").Replace("\\", "/").Substring(txt_RocksmithDLCPath.Text.IndexOf(":\\") + 2) + "Rocksmith2014.app/Contents/MacOS\""; // "\n";
                            lines += df + "\n";
                        }
                        else
                            lines += line + "\n";
                    }
                    info.Close();
                    File.WriteAllText(AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command", lines);

                    ////Run script to modify the MAc-APP/executable of rocksmith to allow CDLC
                    //var startInfo = new ProcessStartInfo();
                    //startInfo.FileName = Path.Combine(AppWD, "\\CDLCEnablers\\RUN_PATCH_RS.command");
                    //startInfo.WorkingDirectory = AppWD+ "\\CDLCEnablers\\";
                    //startInfo.Arguments = "";
                    //startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;
                    //    using (var DDC = new Process())
                    //    {
                    //        DDC.StartInfo = startInfo;
                    //        DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //    }
                    MessageBox.Show("In MAC please run the script \n@" + AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command\nthat will modify the App \n@" + df, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            if (File.Exists(txt_RocksmithDLCPath.Text + "\\..\\D3DX9_42.dll"))
                if (hexw != GetHash(txt_RocksmithDLCPath.Text + "\\..\\D3DX9_42.dll")) MessageBox.Show("Windows APP enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Windows APP NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Windows APP NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);


            if (hexm1 == GetHash(txt_RocksmithDLCPath.Text + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\libRSBypass.dylib") || hexm1 == GetHash(txt_RocksmithDLCPath.Text + "..\\Rocksmith2014.app\\Contents\\MacOS\\insert_dylib"))
                MessageBox.Show("MAC APP enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("MAC APP NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txt_RocksmithDLCPath_TextChanged(object sender, EventArgs e)
        {
            if (txt_RocksmithDLCPath.Text.Length > 0) if ((txt_RocksmithDLCPath.Text.Substring(txt_RocksmithDLCPath.Text.Length - 1, 1) == "\\")) txt_RocksmithDLCPath.Text = txt_RocksmithDLCPath.Text.Substring(0, txt_RocksmithDLCPath.Text.Length - 1);
            SetImportNo();
        }
        private void SetImportNo()
        {
            string[] filez;
            //filez[0] = "";
            if (Directory.Exists(txt_RocksmithDLCPath.Text))
            {
                if (chbx_Additional_Manipulations.GetItemChecked(37)) //38. Import other formats but PC, as well (as duplciates)
                    filez = System.IO.Directory.GetFiles(txt_RocksmithDLCPath.Text, "*.psarc*");
                else
                    filez = System.IO.Directory.GetFiles(txt_RocksmithDLCPath.Text, "*_p.psarc");
                btn_PopulateDB.Text = "Import " + filez.Count().ToString() + " DLCs";

            }
            else btn_PopulateDB.Text = "Import N/A DLCs";
        }

        private void btn_CalcNoOfImports_Click(object sender, EventArgs e)
        {
            SetImportNo();
        }

        private void btn_CopyDefaultDBtoTemp_Click(object sender, EventArgs e)
        {
            //var i = DataViewGrid.SelectedCells[0].RowIndex;
            //var filename = DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString();
            //string filePath = TempPath + "\\0_old\\" + filename;
            //var dest = RocksmithDLCPath + "\\" + filename;
            ////var eef = dhs.Tables[0].Rows[i].ItemArray[87].T/*oString();*/
            //if (DataViewGrid.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")//OLd available
            //{
            string fielPath = MyAppWD + "\\Files.accdb";
            string dest = txt_TempPath.Text + "\\Files.accdb";
            if (!Directory.Exists(txt_TempPath.Text)) Directory.CreateDirectory(txt_TempPath.Text);
            if (File.Exists(fielPath))
                try
                {
                    File.Copy(fielPath, dest, true);
                    txt_DBFolder.Text = dest;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    MessageBox.Show(fielPath + "----" + dest + "Error at copy OLD " + ee);
                }
            //}
            ////    pB_ReadDLCs.Value++;
            ////}
            //MessageBox.Show("Old/Iinitially imported File Copied to " + RocksmithDLCPath + "\\");
        }

        private void txt_TempPath_TextChanged(object sender, EventArgs e)
        {

            if (txt_TempPath.Text.Length > 0) if ((txt_TempPath.Text.Substring(txt_TempPath.Text.Length - 1, 1) == "\\")) txt_TempPath.Text = txt_TempPath.Text.Substring(0, txt_TempPath.Text.Length - 1);
        }
    }
}