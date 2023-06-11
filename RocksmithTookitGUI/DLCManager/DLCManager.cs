using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib;
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.DLCPackage.Manifest2014;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.XmlRepository;
using RocksmithToTabLib;//for psarc browser
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using SQLite;
using System.Diagnostics;//repackf
using System.Globalization;
//bcapi
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;//regex
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using ZipFile = System.IO.Compression.ZipFile;
using Microsoft.Extensions.Logging;
using System.Windows;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Media;
using X360.Other;
using Swan;
using System.Drawing.Printing;
using static Gpif.Rhythm;
using System.Diagnostics.Eventing.Reader;
//using System.Threading;
namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {
        //bcapi
        //private bool loading = false;
        public BackgroundWorker bwConvert = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        string Groupss = "";
        public string netstatus = "NOK";
        public bool FiltrParams = false;
        public bool ChanginProfile = false;
        public string SaveOK = "";
        public int mutit = 0;
        public string inserts = "";
        string[] insrts = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
        bool nostatrefresh = false;
        bool DefaultExist = false;

        public string SearchCmd = "";

        ToolTip toolTip11 = new ToolTip();

        //Processing global vars
        bool duplit = false;
        int dupliNo = 0;
        int dupliPrcs = 0;
        string[,] dupliSongs = new string[2, c("dlcm_maxsongsinDLCM").ToInt32()];

        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        static string MyAppWD = AppWD; //when removing DDC

        DateTime timestamp;
        bool DBChanged = true;
        string logPath = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");

        //OleDbConnection connection;
        //OleDbCommand command;

        private UtilitiesFunctions.MainDBfields[] files = new UtilitiesFunctions.MainDBfields[20000];
        private UtilitiesFunctions.MainDBfields[] SongRecord = new UtilitiesFunctions.MainDBfields[20000];

        public DLCPackageData info;
        public string author = "";
        public string tkversion = "";
        public string description = "";
        public string comment = "";
        public string SongDisplayName = "";
        public string Namee = "";
        public string Description = "";
        public string art_hash = "";
        public string AlbumArtPath = "";
        public string Alternate_No = "";
        public string Album = "";
        public string Title_Sort = "";
        public string Is_Alternate = "";
        public string Is_Original = "";
        public string ArtistSort = "";
        public string AlbumSort = "";
        public string AlbumYear = "";
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
        public string IsLive = ""; public string LiveDetails = ""; public string IsAcoustic = ""; public string IsSingle = ""; public string IsSoundtrack = "";
        public string IsInstrumental = ""; public string IsEP = ""; public string IsUncensored = ""; public string IsFullAlbum = ""; public string IsRemastered = ""; public string InTheWorks = "";
        public string IsKaraoke = ""; public string IsDemo = ""; public string IsMedley = ""; public string IsMultiStrings = ""; public string HasFeaturing = ""; public string IsRemix = "";
        public string IsCover = ""; public string IsDeluxe = ""; public string IsGreatestHits = ""; public string IsMidi = ""; public string IsGameSoundtrack = ""; public string IsTVTheme = ""; public string IsAmateurCover = "";
        public string BasedOn_Youtube = ""; string BasedOn_CF = ""; string BasedOn_Tabs = ""; string ToDos = ""; string ToneDetails = ""; string PackageDetails = "";/* public string HasCapo = "";*/
        //public string manualdec;
        //public string automdec;
        public int manualdec = 0;
        public string manualdecnames = "";
        public int automdec = 0;
        public string automdecnames = "";

        public DLCPackageData infoorig = null;
        public int bitrateorig = 0;
        public int SampleRateorig = 0;
        public string PreviewLenghtorig;

        public GameVersion CurrentGameVersion
        {
            get { return GameVersion.RS2014; }
            set { }
        }

        internal UtilitiesFunctions.MainDBfields[] Fields
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

        internal UtilitiesFunctions.MainDBfields[] Files
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

        public DLCManager()
        {
            timestamp = UpdateLog(timestamp, "startig dlc manag", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (ConfigRepository.Instance()["dlcm_StartInDLCM"] == "NOO") return;

            ConfigRepository.Instance()["dlcm_chosendb"] = "";
            if (!Directory.Exists(ConfigRepository.Instance()["dlcm_RocksmithDLCPath"]))
            {
                ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_to_import";
            }
            DialogResult res = CreateTempFolderStructure(c("dlcm_TempPath"), c("dlcm_TempPath") + "\\0_old", c("dlcm_TempPath") + "\\0_broken", c("dlcm_TempPath") + "\\0_duplicate"
                , c("dlcm_TempPath") + "\\0_dlcpacks", c("dlcm_RocksmithDLCPath"),
                c("dlcm_TempPath") + "\\0_repacked", c("dlcm_TempPath") + "\\0_repacked\\XBOX360", c("dlcm_TempPath") + "\\0_repacked\\PC", c("dlcm_TempPath") + "\\0_repacked\\MAC", c("dlcm_TempPath") + "\\0_repacked\\PS3"
                , c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath"), c("dlcm_TempPath") + "\\0_albumCovers", c("dlcm_TempPath") + "\\0_log", c("dlcm_TempPath") + "\\0_archive", c("dlcm_TempPath") + "\\0_data"
                , c("dlcm_TempPath") + "\\0_temp", c("dlcm_TempPath") + "\\0_to_import");
            if (res != DialogResult.No && res != DialogResult.Yes)
                return;

            string v = ConfigRepository.Instance()["dlcm_AccessDLLVersion"];
            string dbbg = ConfigRepository.Instance()["dlcm_DBFolder"];
            if (DisplayData().IndexOf("Microsoft.ACE.OLEDB.16.0") >= 0) ConfigRepository.Instance()["dlcm_AccessDLLVersion"] = "ACE.OLEDB.16.0";
            else if (DisplayData().IndexOf("Microsoft.ACE.OLEDB.12.0") >= 0) ConfigRepository.Instance()["dlcm_AccessDLLVersion"] = "ACE.OLEDB.12.0";

            OpenDBQuick();
            InitializeComponent();

            var SearchFields = c("dlcm_SearchFields");
            SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY " + c("dlcm_OrderOfFields") + ";";

            if (ConfigRepository.Instance()["dlcm_Configurations"] != "")
            {
                chbx_Configurations.Text = ConfigRepository.Instance()["dlcm_Configurations"].ToString();
                ChanginProfile = true;
            }
            //SAve template folder as sometimes it gets corrupted
            var appRootDir = Path.GetDirectoryName(Application.ExecutablePath);
            var templateDir = Path.Combine(appRootDir, "Template");
            // substring is to remove destination_dir absolute path (E:\).
            // Create subdirectory structure in destination    
            var destination_dir = AppWD + "\\Template";
            if (!DirectoryExists(destination_dir))
            {
                CopyFolder(templateDir, destination_dir);
            }
#if (DEBUG)
            btn_Debug.Visible = true;
#endif
            //Enable Preview generation
            if (ConfigRepository.Instance()["general_wwisepath"] == "")
                if (ConfigRepository.Instance()["dlcm_localwwise"] == "") ConfigRepository.Instance()["general_wwisepath"] = "C:\\Program Files (x86)\\Audiokinetic\\" + c("dlcm_wwise");// 2017.2.0.6500";
                else ConfigRepository.Instance()["general_wwisepath"] = AppWD + "\\" + c("dlcm_localwwise") + "\\" + c("dlcm_wwise");
            else if (AppWD.ToLower().Contains("debug\\rk") && Directory.Exists(AppWD + "\\" + c("dlcm_localwwise") + "\\" + c("dlcm_wwise"))) ConfigRepository.Instance()["general_wwisepath"] = AppWD + "\\" + c("dlcm_localwwise") + "\\" + c("dlcm_wwise");
            //enable ps3 encryption
            //MessageBox.Show(ConfigRepository.Instance()["general_wwisepath"]+"-"+AppWD);
            if (c("dlcm_localjava") != "") System.Diagnostics.Process.Start("setx", " - m JAVA_HOME \"" + AppWD + "\\" + c("dlcm_localjava") + "\"");
            //var startInfo = new ProcessStartInfo
            //{
            //    FileName = Path.Combine(AppWD, "cmd"),
            //    WorkingDirectory = AppWD
            //};
            //var tr = AppWD + "\\" + c("dlcm_localjava");
            ////startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = string.Format("setx - m JAVA_HOME \"{0}\"", tr);
            //startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;
            //Process DDC = new Process();
            //
            //{
            //    DDC.StartInfo = startInfo;
            //    //DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
            //}
            bwConvert.DoWork += new DoWorkEventHandler(ConvertWEM);

            bwConvert.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwConvert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwConvert.WorkerReportsProgress = true;

            // if (!ChanginProfile) 
            OpenDb();

            txt_DBFolder.Text = c("dlcm_DBFolder");
            txt_TempPath.Text = c("dlcm_TempPath");
            txt_RocksmithDLCPath.Text = c("dlcm_RocksmithDLCPath");
            //if (!File.Exists(txt_DBFolder.Text))                chbx_DefaultDB.Checked = true;
            if (!DirectoryExists(txt_TempPath.Text)) txt_TempPath.Text = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\Temp";
            var dontreopen = false;
            if (ConfigRepository.Instance()["dlcm_StartInDLCM"] != "NOO")
            {
                ProfilesRefresh();
                DLCManagerOpen();

                BackupDB(GetParam(4, chbx_Additional_Manipulations));

                SaveSettings();
                if (ConfigRepository.Instance()["dlcm_StartInDLCM"] == "Yes" && lbl_NoRec2.Text != "0/0 records." && lbl_NoRec2.Text != "0/0/0 records.")
                    btn_DecompressAll_Click(null, null);
            }
            else ConfigRepository.Instance()["dlcm_StartInDLCM"] = "";

            var hhh = c("dlcm_PC") + "\n" + ConfigRepository.Instance()["general_rs2014path"] + "\n" + c("dlcm_Mac");
            var txt = "\n\ndlcm_PC " + (DirectoryExists(c("dlcm_PC")) ? "(Y): " : "(N): ") + c("dlcm_PC") + "\n\ngeneral_rs2014path " + (DirectoryExists(c("general_rs2014path")) ? "(Y): " : "(N): ") + ConfigRepository.Instance()["general_rs2014path"]
                + "\n\ndlcm_Mac " + (DirectoryExists(c("dlcm_Mac")) ? "(Y): " : "(N): ") + c("dlcm_Mac") + "\n\ndlcm_PS4 " + (DirectoryExists(c("dlcm_PS4")) ? "(Y): " : "(N): ") + c("dlcm_PS4")
                + "\n\ndlcm_FTPEU (N/A)" + c("dlcm_FTPEU") + "\ndlcm_FTPJP (N/A)" + c("dlcm_FTPJP") + "\ndlcm_FTPUS (N/A)" + c("dlcm_FTPUS") + "\ndlcm_XBOX (N/A)" + c("dlcm_XBOX") +
                "\n\ndlcm_RocksmithDLCPath " + (DirectoryExists(c("dlcm_RocksmithDLCPath")) ? "(Y): " : "(N): ") + c("dlcm_RocksmithDLCPath") + "\n\n" + "Old default: " + "D:\\Spiele\\Steam\\steamapps\\common\\Rocksmith2014" + (DirectoryExists("D:\\Spiele\\Steam\\steamapps\\common\\Rocksmith2014") ? "(Y)." : "(N).");
            toolTip1.SetToolTip(lbl_NoRec2, txt);

            if (!DirectoryExists(ConfigRepository.Instance()["general_rs2014path"]))
                if (DirectoryExists(c("dlcm_PC"))) ConfigRepository.Instance()["general_rs2014path"] = c("dlcm_PC").Replace("\\dlc", "");
                else if (DirectoryExists(c("dlcm_Mac"))) ConfigRepository.Instance()["general_rs2014path"] = c("dlcm_Mac").Replace("\\dlc", "");
                else if (DirectoryExists(c("dlcm_PS4"))) ConfigRepository.Instance()["general_rs2014path"] = c("dlcm_PS4").Replace("\\dlc", "");
                else if (File.Exists(c("dlcm_RocksmithDLCPath") + "\\..\\Rocksmith2014.exe")) ConfigRepository.Instance()["general_rs2014path"] = c("dlcm_RocksmithDLCPath") + "\\..\\";
                else if (DirectoryExists("D:\\Spiele\\Steam\\steamapps\\common\\Rocksmith2014"))
                    ConfigRepository.Instance()["general_rs2014path"] = "D:\\Spiele\\Steam\\steamapps\\common\\Rocksmith2014";
                else MessageBox.Show("No Game Folder found!\n\nPlease update ONE of the following Config values, for enabling further functionalities like Retail List of Songs Editing:" + txt
                   , "Rocksmith 2014 Game NOT Found!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolTip1.SetToolTip(txt_RocksmithDLCPath, "\n\n\nfyi Game Folder is : " + ConfigRepository.Instance()["general_rs2014path"]);

            if (ConfigRepository.Instance()["general_defaultauthor"] == "") ConfigRepository.Instance()["general_defaultauthor"] = "catara";

            if (c("dlcm_ShowConenctivityOnce") == "Maybe") ConfigRepository.Instance()["dlcm_ShowConenctivityOnce"] = "Yes";//default always to show message only once
            hhh = ConfigRepository.Instance()["general_rs2014path"];
            if (DirectoryExists(c("general_rs2014path")))
            {
                btn_LoadRetailSongs.Enabled = true;
                lbl_Access.Visible = false;
            }
            else
            {
                btn_LoadRetailSongs.Enabled = false;
                if (lbl_Access.Visible) lbl_Access.Text += " Rksmth path missing";
                else { lbl_Access.Visible = true; lbl_Access.Text = "Rksmth path missing"; }
            }
        }

        public void DLCManagerOpen()
        {
            timestamp = UpdateLog(timestamp, "starting dlc manager open", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //txt_DBFolder.Text = "1";
            //var a = ConfigRepository.Instance()["dlcm_DBFolder"];
            var t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);

            txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath"];
            txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath"];

            var slct = "SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u WHERE Type=\"Profile\"" +
                " AND Profile_Name=\"" + c("dlcm_Configurations") + "\" and Comments like \"%dlcm_AdditionalManipul%\"";
            chbx_Additional_Manipulations = GenerateParamsList(chbx_Additional_Manipulations, slct);
            lbl_Settings.Text = lbl_Settings.Text + " " + chbx_Additional_Manipulations.Items.Count.ToString() + "/" + GetNoRecords(slct, cnb, cnc).ToString();

            cbx_Groups = GenerateFilterList(cbx_Groups, cnb, cnc); var oldsave = SaveOK; SaveOK = "";
            var tf = ConfigRepository.Instance()["dlcm_Groups"];
            //if (cbx_Groups.Text != tf) cbx_Groups.Text = tf;//else it will be empty
            SaveOK = oldsave;

            //If DLCManagerOpen is initiated from ChangeProfile then dont run :) 

            int k = chbx_Configurations.Items.Count - 1;
            for (k = k; k >= 0; --k)
                if (chbx_Configurations.Items[k].ToString() == ConfigRepository.Instance()["dlcm_Configurations"])
                {
                    //chbx_Configurations.SelectedIndex = k; break;
                }
            //if (k < 0)
            if (ChanginProfile)
            {
                ChangeProfile();
                var missingsoftware = "";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_EoFPath"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_EoFPath"] + " - " + ConfigRepository.Instance()["dlcm_EoFPath_www"]
                        + " ... " + "(optionally) Used in Adding/Precision-time-aligning Lyrics.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_UltraStarCreator"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_UltraStarCreator"] + " - " + ConfigRepository.Instance()["dlcm_UltraStarCreator_www"]
                        + " ... " + "(optionally) Used in creating lyrics out of nothing (raw lyrics).";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_WinMerge"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_WinMerge"] + " - " + ConfigRepository.Instance()["dlcm_WinMerge_www"]
                        + " ... " + "(optionally) Used in offering a XML file wysiwyg based comparing duplicates tracks.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_TCommander"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_TCommander"] + " - " + ConfigRepository.Instance()["dlcm_TCommander_www"]
                        + " ... " + "(optionally) Needed when packing RETAIL files on PS3.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_PathForBRM"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_PathForBRM"] + " - " + ConfigRepository.Instance()["dlcm_PathForBRM_www"]
                        + " ... " + "(optionally) Used to see the Rocksmith tab \"in-a-visual-way\" or to create phases (old tool not under DEV).";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker"] + " - " + ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker_www"]
                        + " ... " + "(optionally) Neede when packing for PS3 jailbroken with HAN.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_PS3xploit-resigner"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_PS3xploit-resigner"] + " - " + ConfigRepository.Instance()["dlcm_PS3xploit-resigner_www"]
                        + " ... " + "(optionally) Needed when signing packages for PS3 jailbroken with HAN.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_MediaInfo_CLI"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_MediaInfo_CLI"] + " - " + ConfigRepository.Instance()["dlcm_MediaInfo_CLI_www"]
                        + " ... " + "(optionally) Used (sometimes) in normalising the bitrate of Audio of the song to reduce file size.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_PKG_Linker"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_PKG_Linker"] + " - " + ConfigRepository.Instance()["dlcm_PKG_Linker_www"]
                        + " ... " + "(optionally) Used by the server used to distribute songs for PS3 jailbroken with HAN.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_RockBand"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_RockBand"] + " - " + ConfigRepository.Instance()["dlcm_RockBand_www"]
                        + " ... " + "(optionally) Used to decompress songs made for Rockband to quickly copy their vocal track to Rocksmith.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_MDBPlus"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_MDBPlus"] + " - " + ConfigRepository.Instance()["dlcm_MDBPlus_www"]
                        + " ... " + "(optionally) Used to View/Edit ACCDB database when 32 bit Driver&Office is installed (old sometimes not working on ARM CPUs).";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_RS2014-Mod-Installer"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_RS2014-Mod-Installer"] + " - " + ConfigRepository.Instance()["dlcm_RS2014-Mod-Installer_www"]
                        + " ... " + "(optionally) Used to run a modded version of Rocksmith for tweaking interface or recording your screen,";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_Database.NET"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_Database.NET"] + " - " + ConfigRepository.Instance()["dlcm_Database.NET_www"]
                        + " ... " + "(optionally) General DB editor for .accdb (Microsoft Access) and .db (sqlite3) (only trial for migration inbetween DBs).";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCEnablerPC"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_DLCEnablerPC"] + " - " + ConfigRepository.Instance()["dlcm_DLCEnablerPC_www"]
                        + " ... " + "(mandatory) Only way to play songs not bought from Ubioft/Rocksmith-store on Windows.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCEnablerMAC"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_DLCEnablerMAC"] + " - " + ConfigRepository.Instance()["dlcm_DLCEnablerMAC_www"]
                        + " ... " + "(mandatory) Only way to play songs not bought from Ubioft/Rocksmith-store on MAc.";
                if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCBuilder"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_DLCBuilder"] + " - " + ConfigRepository.Instance()["dlcm_DLCBuilder_www"]
                        + " ... " + "(optionally) useful for generating a 'notes'/EoF-file out a psarc (also used t6o make a song just like RocksmithToolkit but newer tech still under DEV).";
                //if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCEnablerMAC"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_DLCEnablerMAC_www"]
                //        + " ... " + "";
                //if (!File.Exists(ConfigRepository.Instance()["dlcm_Audiokinetic"])) missingsoftware += "\n" + ConfigRepository.Instance()["dlcm_Audiokinetic_www"]
                //        + " ... " + "";
                if (missingsoftware != "")
                {
                    ErrorWindow frm1 = new ErrorWindow(missingsoftware + "\n\n (Profile:" + ConfigRepository.Instance()["dlcm_Configurations"].ToString() + ")"
                        , "", "Warning there is missing software used in some of the DCLManager features", false, false, true, "", "", "");
                    frm1.ShowDialog();
                    if (frm1.IgnoreSong) ;
                    if (frm1.StopImport) {; }

                    MessageBox.Show("Finished installing?\n" + missingsoftware, "Reminder:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    missingsoftware = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_EoFPath"])) ConfigRepository.Instance()["dlcm_EoFPath"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_UltraStarCreator"])) ConfigRepository.Instance()["dlcm_UltraStarCreator_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_WinMerge"])) ConfigRepository.Instance()["dlcm_WinMerge_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_TCommander"])) ConfigRepository.Instance()["dlcm_TCommander_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_PathForBRM"])) ConfigRepository.Instance()["dlcm_PathForBRM_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker"])) ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_PS3xploit-resigner"])) ConfigRepository.Instance()["dlcm_PS3xploit-resigner_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_MediaInfo_CLI"])) ConfigRepository.Instance()["dlcm_MediaInfo_CLI_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_PKG_Linker"])) ConfigRepository.Instance()["dlcm_PKG_Linker_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_RockBand"])) ConfigRepository.Instance()["dlcm_RockBand_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCBuilder"])) ConfigRepository.Instance()["dlcm_DLCBuilder_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_sqliteodbc"])) ConfigRepository.Instance()["dlcm_sqliteodbc_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_RS2014-Mod-Installer"])) ConfigRepository.Instance()["dlcm_RS2014-Mod-Installer_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_Database.NET"])) ConfigRepository.Instance()["dlcm_Database.NET_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_MDBPlus"])) ConfigRepository.Instance()["dlcm_MDBPlus_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCEnablerPC"])) ConfigRepository.Instance()["dlcm_DLCEnablerPC_www"] = "";
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_DLCEnablerMAC"])) ConfigRepository.Instance()["dlcm_DLCEnablerMAC_www"] = "";
                    //if (!File.Exists(ConfigRepository.Instance()["dlcm_Audiokinetic"])) ConfigRepository.Instance()["dlcm_Audiokinetic_www"] = "";
                }
            }

            //chbx_Configurations.se = ConfigRepository.Instance()["dlcm_Configurations"];
            txt_Title.Text = ConfigRepository.Instance()["dlcm_Title"];
            txt_Title_Sort.Text = ConfigRepository.Instance()["dlcm_Title_Sort"];
            txt_Artist.Text = ConfigRepository.Instance()["dlcm_Artist"];
            txt_Artist_Sort.Text = ConfigRepository.Instance()["dlcm_Artist_Sort"];
            txt_Album.Text = ConfigRepository.Instance()["dlcm_Album"];
            txt_Album_Sort.Text = ConfigRepository.Instance()["dlcm_Album_Sort"];
            txt_File_Name.Text = ConfigRepository.Instance()["dlcm_File_Name"];
            txt_Lyric_Info.Text = ConfigRepository.Instance()["dlcm_Lyric_Info"];
            chbx_PC.Checked = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? true : false;
            chbx_Mac.Checked = (ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? true : false;
            chbx_PS3.Checked = (ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? true : false;
            chbx_XBOX360.Checked = (ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? true : false;
            chbx_DebugB.Checked = (ConfigRepository.Instance()["dlcm_Debug"] == "Yes") ? true : false;
            chbx_DefaultDB.Checked = (ConfigRepository.Instance()["dlcm_DefaultDB"] == "Yes") ? true : false;
            chbx_Rebuild.Checked = (ConfigRepository.Instance()["dlcm_AdditionalManipul106"] == "Yes") ? true : false;
            //rbtn_Population_All.Checked = true;

            //if (ConfigRepository.Instance()["dlcm_Grouping"] == "All") rbtn_Population_All.Checked = true;
            //else if (ConfigRepository.Instance()["dlcm_Grouping"] == "Groups") rbtn_Population_Groups.Checked = true;
            //else if (ConfigRepository.Instance()["dlcm_Grouping"] == "Selected") rbtn_Population_Selected.Checked = true;
            //else
            nostatrefresh = false; if (ConfigRepository.Instance()["dlcm_Groups"].Contains("Split")) rbtn_Population_PackNO.Checked = true;
            if (ConfigRepository.Instance()["dlcm_Split4Pack"] != "" && ConfigRepository.Instance()["dlcm_Split4Pack"].ToInt32() == 0) nostatrefresh = true;
            //else rbtn_Population_All.Checked = true;

            //if (ConfigRepository.Instance()["dlcm_Split4Pack"] != "" && ConfigRepository.Instance()["dlcm_Split4Pack"].ToInt32() > 0) nostatrefresh = true;
            txt_NoOfSplits.Text = ConfigRepository.Instance()["dlcm_Split4Pack"];
            nostatrefresh = false;

            //RepackP = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\PC" : (ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\XBOX360" : (ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\PS3" : (ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? txt_TempPath.Text + "\\0_repacked\\MAC" : "";
            Set_DEBUG();
            cbx_Activ_Title.Checked = (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") ? true : false;
            cbx_Activ_Title_Sort.Checked = (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") ? true : false;
            cbx_Activ_Artist.Checked = (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") ? true : false;
            cbx_Activ_Artist_Sort.Checked = (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") ? true : false;
            cbx_Activ_Album.Checked = (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") ? true : false;
            cbx_Activ_Album_Sort.Checked = (ConfigRepository.Instance()["dlcm_Activ_AlbumSort"] == "Yes") ? true : false;
            cbx_Activ_File_Name.Checked = (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes") ? true : false;
            cbx_Activ_Lyric_Info.Checked = (ConfigRepository.Instance()["dlcm_Activ_LyricInfo"] == "Yes") ? true : false;
            cbx_Groups.Text = ConfigRepository.Instance()["dlcm_Groups"];
            txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder"];//Make sure we change this at end as this will save
        }

        private void ProfilesRefresh()
        {
            //DataSet dcs = new DataSet(); dcs = SelectFromDB("Arrangements", "SELECT DISTINCT CAST(CapoFret as numeric) as CDLC_ID FROM Arrangements WHERE CapoFret<>'' AND CapoFret<>'0'", txt_DBFolder.Text, cnb, cnc);

            var profileexists = false;
            //populate the Group  Dropdown
            if (File.Exists(txt_DBFolder.Text))
            {
                //DataSet des = new DataSet(); des = SelectFromDB("Groups", "SELECT ID, Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u WHERE Type='Filter' ORDER BY DisplayGroup, DisplayName ASC", txt_DBFolder.Text, cnb, cnc);

                // Groups[] ds = new Groups[20000]; ds = SelectFromDB("Groups", "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text, cnb, cnc);
                var cmd = "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";";
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", cmd, txt_DBFolder.Text, cnb, cnc);

                var norec = GetNoRec(ds, cnb, cnc);
                if (norec > 0)
                {
                    //remove items
                    if (chbx_Configurations.Items.Count > 0)
                    {
                        chbx_Configurations.DataSource = null;
                        for (int k = chbx_Configurations.Items.Count - 1; k >= 0; --k)
                            if (!chbx_Configurations.Items[k].ToString().Contains("--"))
                                chbx_Configurations.Items.RemoveAt(k);
                    }
                    //add items
                    chbx_Configurations.DataSource = null;
                    for (int j = 0; j < norec; j++)
                    {
                        // var tem = ds[j].Profile_Name; //ds.Tables[0].Rows[j].ItemArray[0].ToString();
                        var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                        chbx_Configurations.Items.Add(tem);
                        if (tem == "Default") DefaultExist = true;
                        if (ConfigRepository.Instance()["dlcm_Configurations"].ToString() == tem) profileexists = true;
                    }
                }
            }
            if ((chbx_Configurations.Text == "" || !profileexists) && DefaultExist)
            {
                MessageBox.Show("Since profile " + chbx_Configurations.Text + " is missing, defaulted to Default", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

                timestamp = UpdateLog(timestamp, "Profile Default-ed (fyi " + ConfigRepository.Instance()["dlcm_Configurations"].ToString()
                    + (profileexists ? "exists" : "doesn't exist") + ")", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                ConfigRepository.Instance()["dlcm_Configurations"] = "Default";
                chbx_Configurations.Text = "Default";
            }
        }


        public void ChangeProfile()
        {

            timestamp = UpdateLog(timestamp, "Starting Changing Profiles", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //SaveOK = "Ok"; SaveSettings();//Save settings

            pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 1;
            if (chbx_Configurations.Text == "Select Profile") return;/*|| !ChanginProfile*/
            else if (chbx_Configurations.Text == "" && DefaultExist)
            {
                MessageBox.Show("Since profile " + chbx_Configurations.Text + " is missing, defaulted to Default", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                timestamp = UpdateLog(timestamp, "Profile Default-ed", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                chbx_Configurations.Text = "Default";
                return;
            }
            else if (chbx_Configurations.Text == "" && !DefaultExist)
            {
                MessageBox.Show("No User or Default Profile!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT Groupz, Comments FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text
                    + "\" ORDER BY Comments ASC", txt_DBFolder.Text, cnb, cnc);
            var norec = GetNoRec(ds, cnb, cnc); var tst = "";
            //if (ds.Tables.Count > 0) norec = ds.Tables[0].Rows.Count;
            if (norec == 0 & DefaultExist)
            {
                //ds = SelectFromDB("Groups", "SELECT Groups, Comments FROM Groups WHERE Profile_Name=\"Default\" ORDER BY Comments ASC", txt_DBFolder.Text, cnb, cnc);
                //if (ds.Tables.Count > 0)
                //{
                //    norec = ds.Tables[0].Rows.Count;
                //    if (norec > 0)
                //    {
                //     
                //if (ConfigRepository.Instance()["dlcm_Dbg"] != "Yes") 
                MessageBox.Show("Since profile " + chbx_Configurations.Text + " is missing, default-ed to Default", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                timestamp = UpdateLog(timestamp, "Profile Default-ed", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                chbx_Configurations.Text = "Default";
                //    }if () 
                //}
            }
            if (norec > 0)
            {
                //ConfigRepository.Instance()["dlcm_Groups"] = "";
                pB_ReadDLCs.Increment(1); var newdb = "";
                var conff = "";
                for (int j = 0; j < norec; j++)
                {
                    if (ds.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_DBFolder") newdb = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    if (ds.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_Configurations") conff = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                }

                //don't use 
                var no = GetNoRecords("SELECT ID FROM Groups WHERE Profile_Name='" + conff + "'", cnb, cnc);
                if (no <= 0)
                {
                    MessageBox.Show("Since profile " + chbx_Configurations.Text + " is missing, default-ed to Default", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    timestamp = UpdateLog(timestamp, "Profile Default-ed", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    ConfigRepository.Instance()["dlcm_Configurations"] = "Default";
                }


                if (ConfigRepository.Instance()["dlcm_DBFolder"].ToLower() != newdb.ToLower() && File.Exists(newdb))
                {
                    var old = ConfigRepository.Instance()["dlcm_DBFolder"].ToLower();
                    for (int j = 0; j < norec; j++)
                        if (!(ds.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_DBFolder"
                            && ConfigRepository.Instance()[ds.Tables[0].Rows[j].ItemArray[1].ToString()].Contains(ConfigRepository.Instance()["dlcm_chosendb"]))
                            ||
                            !(ConfigRepository.Instance()["dlcm_AdditionalManipul114"] == "Yes"
                            && ConfigRepository.Instance()[ds.Tables[0].Rows[j].ItemArray[1].ToString()].Contains(".db")
                            || !(no <= 0 && ds.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_Configurations")))
                            ConfigRepository.Instance()[ds.Tables[0].Rows[j].ItemArray[1].ToString()] = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    //cnb.Close();
                    //cnb.ConnectionString = "Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security Info=False;Mode= Share Deny None;Data Source=" + newdb;
                    if ((newdb.Contains(ConfigRepository.Instance()["dlcm_chosendb"]))
                            ||
                            (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] == "Yes"
                            && newdb.Contains(".db")
                            ))
                        txt_DBFolder.Text = newdb;
                    //ConfigRepository.Instance()["dlcm_Configurations"] = "Default";
                    var t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);
                    if (!t) return;

                    var sel = "SELECT Groupz, Comments FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" ORDER BY Comments ASC";
                    DataSet drs = new DataSet(); drs = SelectFromDB("Groups", sel, txt_DBFolder.Text, cnb, cnc);
                    var norecs = GetNoRec(drs, cnb, cnc);
                    //if (drs.Tables.Count > 0) norecs = drs.Tables[0].Rows.Count;
                    if (norecs == 0)
                    {
                        AddNewProfile(true, c("dlcm_Configurations")); tst = chbx_Configurations.Text + " Profile inserted..." + old + "Changed DB to get New Profile..." + newdb;
                    }
                    else
                    {
                        //cnb/cnc functions prb dont works as var populated only in uitiltie 
                        DialogResult result1 = MessageBox.Show("Do you want to overrite existing Profile (" + c("dlcm_Configurations")//ConfigRepository.Instance()["dlcm_DBFolder"].ToLower() 
                            + ") in the just changed DB (" + cnb.DataSource + ") and its (same name) Profile (" + chbx_Configurations.Text + ")?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result1 == DialogResult.Yes)
                        {
                            SaveOK = "OK"; SaveSettings(); SaveOK = ""; tst = chbx_Configurations.Text + "Profile overwitten..." + old + "Changed DB to get New Profile..." + newdb;
                        }
                    }

                    chbx_Configurations.Text = ConfigRepository.Instance()["dlcm_Configurations"];
                    //ds = SelectFromDB("Groups", "SELECT Groups, Comments FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" ORDER BY Comments ASC", txt_DBFolder.Text, cnb, cnc);
                    //norec = 0;
                    //if (ds.Tables.Count > 0) norec = ds.Tables[0].Rows.Count;
                    //tst = "eChanged DB to get New Profile...";
                    timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"]
                        , "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //timestamp = UpdateLog(timestamp, "REturing.. to import", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                else
                    for (int j = 0; j < norec; j++)
                        //{
                        if (ds.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_Configurations")
                            //{
                            ConfigRepository.Instance()["dlcm_Configurations"] = chbx_Configurations.Text;
                        //    tst = chbx_Configurations.Text;
                        //}
                        else
                            ConfigRepository.Instance()[ds.Tables[0].Rows[j].ItemArray[1].ToString()] = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                //}

                pB_ReadDLCs.Increment(1);
            }
            else
            {
                //MessageBox.Show("No profile name found for " + chbx_Configurations.Text, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chbx_Configurations.Text = "";
            }

            //}
            pB_ReadDLCs.Increment(1);
            if (!File.Exists(txt_DBFolder.Text)) chbx_DefaultDB.Checked = true;
            else if (!(txt_DBFolder.Text == MyAppWD + "\\..\\AccessDB.accdb") || !(txt_DBFolder.Text == MyAppWD + "\\..\\SQLLiteDB.db")) chbx_DefaultDB.Checked = false;
            //SaveOK = "";

            tst = "Loading Selected Profile..."; timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //save read profile to Config
            SaveOK = "OK";
            //DLCManagerOpen();
            SaveOK = "";
            SetImportNo();
            pB_ReadDLCs.Increment(1);

            //Setting autoamting some debug options if profile is named debug/as per config
            if (chbx_Configurations.Text == ConfigRepository.Instance()["dlcm_DebugProfile"])
            {
                var txt = "15 move to old Unchecked\n" +
                        "49 ftp Unchecked\n" +
                        "24 apply standard Unchecked\n" +
                        "77 dont open main db Unchecked\n" +
                        "75 copy to old checked\n" +
                        "34 audio Unchecked\n" +
                        "55 audio Unchecked\n" +
                        "69 audio Unchecked\n" +
                        "78 audio Unchecked\n" +
                "chbx_CleanTemp.Checked = true;\n" +
                        "chbx_DebugB.Checked = true;\n";
                MessageBox.Show("Cause Debug Profile was selected (same Profile as option dlcm_DebugProfile)" + txt
                   , MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(15), CheckState.Unchecked);//move to old
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(49), CheckState.Unchecked);//ftp
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(24), CheckState.Unchecked);//apply standard
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(77), CheckState.Unchecked);//dont open main db
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(75), CheckState.Checked);//copy to old
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(34), CheckState.Unchecked);//audio
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(55), CheckState.Unchecked);//audio
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(69), CheckState.Unchecked);//audio
                chbx_Additional_Manipulations.SetItemCheckState(GetParamLocation(78), CheckState.Unchecked);//audio
                                                                                                            //chbx_CleanDB.Checked = true;
                chbx_CleanTemp.Checked = true;
                chbx_DebugB.Checked = true;
            }
            ChanginProfile = false;
            //SaveOK = "OK"; SaveSettings();
            tst = "End Selected Profile..."; timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        }

        public const long BUFFER_SIZE = 4096;



        public static string GetFormat(bool chbx_PC, bool chbx_Mac, bool chbx_PS4, bool chbx_iOS, bool chbx_XBOX360, bool chbx_PS3)/*string db, string txt, */
        {
            var rt = "";
            if (chbx_PC) rt = "\"Pc\"";
            if (chbx_PS3) rt = "\"PS3\"";
            if (chbx_Mac) rt = "\"Mac\"";
            if (chbx_PS4) rt = "\"PS4\"";
            if (chbx_iOS) rt = "\"iOS\"";
            if (chbx_XBOX360) rt = "\"XBOX360\"";
            return rt.Replace("\"\"", "\",\"");
        }
        public void RefreshSelectedStat(bool extrasplit)/*string db, string txt, */
        {
            if (File.Exists(txt_DBFolder.Text) && !nostatrefresh)
            {
                timestamp = UpdateLog(timestamp, "Start generating stats...", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //Find out All songs
                var SCmd = "SELECT ID FROM Main u ";

                DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Main", SCmd, txt_DBFolder.Text, cnb, cnc);
                var noOfRec = GetNoRec(dsz1, cnb, cnc);
                //if (dsz1.Tables.Count > 0) noOfRec = dsz1.Tables[0].Rows.Count;

                timestamp = UpdateLog(timestamp, "End select all...", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                var noOfSelRec = noOfRec;
                SCmd = "SELECT ID FROM Main ";
                SCmd += "WHERE cstr(ID) IN (" + GetFilter(cbx_Groups.Text, SearchCmd, 0, "", cnb, "", GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc) + ")";
                SCmd = SCmd.Replace(c("dlcm_SearchFields"), "ID");
                //if (rbtn_Population_PackNO.Checked) SearchCmd += " AND Split4Pack='" + txt_NoOfSplits.Text + "'";
                DataSet dsz2 = new DataSet(); dsz2 = SelectFromDB("Main", SCmd, txt_DBFolder.Text, cnb, cnc);
                noOfSelRec = GetNoRec(dsz2, cnb, cnc); ;
                //if (dsz2.Tables.Count > 0) noOfSelRec = dsz2.Tables[0].Rows.Count;

                timestamp = UpdateLog(timestamp, "End selecting selected...", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                var noOfSelRec3 = -1;
                if (extrasplit)
                {
                    SCmd = "SELECT ID FROM Main ";
                    SCmd += GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text, cnb, "", 0,
                        GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc);
                    DataSet dsz3 = new DataSet(); dsz3 = SelectFromDB("Main", SCmd, txt_DBFolder.Text, cnb, cnc);

                    timestamp = UpdateLog(timestamp, "End selecting extra", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    //if (dsz3.Tables.Count > 0) noOfSelRec3 = dsz3.Tables[0].Rows.Count;
                    //else 
                    noOfSelRec3 = GetNoRec(dsz3, cnb, cnc);
                }
                lbl_NoRec2.Text = noOfSelRec3 < 0 ? noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records." : (noOfSelRec3.ToString() + "/") + noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records.";
            }
        }


        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_RocksmithDLCPath.Text = temppath;
                SetImportNo();
            }
        }

        private void btn_TempPath_Click(object sender, EventArgs e)
        {
            //var t = 0;
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
            var result1 = MessageBox.Show("Chose Yes for selecting an existing DB (.accdb/.db file)\n Chose No for selecting an Empty Folder (use button near by to copy Scheleton DB).", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No)
                using (var fbd = new VistaFolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                        txt_DBFolder.Text = fbd.SelectedPath;
                }
            else if (result1 == DialogResult.Yes)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txt_DBFolder.Text = openFileDialog1.FileName;
                    var t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);
                }
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
                //chbx_Rebuild.Visible = true;
                chbx_Additional_Manipulations.Visible = true;
                rtxt_StatisticsOnReadDLCs.Visible = true;
                txt_FilterParams.Visible = true;
                btn_FilterParams.Visible = true;

                chbx_PS4.Visible = true;
                chbx_iOS.Visible = true;

                chbx_CleanTemp.Visible = true;
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
                lbl_AlbumSort.Visible = true;
                txt_Album_Sort.Visible = true;
                cbx_Album_Sort.Visible = true;
                cbx_Activ_Album_Sort.Visible = true;
                btn_Preview_Album_Sort.Visible = true;
                lbl_LyricInfo.Visible = true;
                txt_Lyric_Info.Visible = true;
                cbx_Lyric_Info.Visible = true;
                cbx_Activ_Lyric_Info.Visible = true;
                btn_Preview_Lyric_Info.Visible = true;
                lbl_File_Name.Visible = true;
                txt_File_Name.Visible = true;
                cbx_File_Name.Visible = true;
                cbx_Activ_File_Name.Visible = true;
                btn_Preview_File_Name.Visible = true;
                lbl_PreviewText.Visible = true;
                lbl_Mask.Visible = true;
                lbl_Artist_Sort.Visible = true;
                //btn_Debbug.Visible = true;
                lbl_Log.Visible = true;
                lbl_Settings.Visible = true;
                chbx_iOS.Visible = true;
                chbx_PS4.Visible = true;

            }
            else
            {
                chbx_Additional_Manipulations.Visible = false;
                chbx_PS4.Visible = false;
                chbx_iOS.Visible = false;
                rtxt_StatisticsOnReadDLCs.Visible = false;
                txt_FilterParams.Visible = false;
                btn_FilterParams.Visible = false;

                chbx_CleanTemp.Visible = false;
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
                lbl_AlbumSort.Visible = false;
                txt_Album_Sort.Visible = false;
                cbx_Album_Sort.Visible = false;
                cbx_Activ_Album_Sort.Visible = false;
                btn_Preview_Album_Sort.Visible = false;
                lbl_LyricInfo.Visible = false;
                txt_Lyric_Info.Visible = false;
                cbx_Lyric_Info.Visible = false;
                cbx_Activ_Lyric_Info.Visible = false;
                btn_Preview_Lyric_Info.Visible = false;
                lbl_File_Name.Visible = false;
                txt_File_Name.Visible = false;
                cbx_File_Name.Visible = false;
                cbx_Activ_File_Name.Visible = false;
                btn_Preview_File_Name.Visible = false;
                lbl_PreviewText.Visible = false;
                lbl_Mask.Visible = false;
                lbl_Artist_Sort.Visible = false;
                btn_Add2Retail.Visible = false;
                lbl_Log.Visible = false;
                lbl_Settings.Visible = false;
                chbx_iOS.Visible = false;
                chbx_PS4.Visible = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Starting SaveSetting", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            SaveOK = "OK"; SaveSettings();// Saving for later
            ConfigRepository.Instance()["dlcm_StartInDLCM"] = "NOO";
            SaveOK = "";
            ((MainForm)ParentForm).ReloadControls();
            timestamp = UpdateLog(timestamp, "Ending SaveSetting", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }


        public void SaveSettings()
        {
            if (SaveOK == "") return;
            timestamp = UpdateLog(timestamp, "starting save settings", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var oldprof = ConfigRepository.Instance()["dlcm_Configurations"];
            ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] = txt_RocksmithDLCPath.Text;
            ConfigRepository.Instance()["dlcm_TempPath"] = txt_TempPath.Text;
            ConfigRepository.Instance()["dlcm_DBFolder"] = txt_DBFolder.Text;
            ConfigRepository.Instance()["dlcm_Title"] = txt_Title.Text;
            ConfigRepository.Instance()["dlcm_Title_Sort"] = txt_Title_Sort.Text;
            ConfigRepository.Instance()["dlcm_Artist"] = txt_Artist.Text;
            ConfigRepository.Instance()["dlcm_Artist_Sort"] = txt_Artist_Sort.Text;
            ConfigRepository.Instance()["dlcm_Album"] = txt_Album.Text;
            ConfigRepository.Instance()["dlcm_Album_Sort"] = txt_Album_Sort.Text;
            ConfigRepository.Instance()["dlcm_File_Name"] = txt_File_Name.Text;
            ConfigRepository.Instance()["dlcm_Lyric_Info"] = txt_Lyric_Info.Text;
            ConfigRepository.Instance()["dlcm_chbx_PC"] = chbx_PC.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_Mac"] = chbx_Mac.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_PS3"] = chbx_PS3.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_chbx_XBOX360"] = chbx_XBOX360.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Debug"] = chbx_DebugB.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DefaultDB"] = chbx_DefaultDB.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Configurations"] = chbx_Configurations.Text;
            ConfigRepository.Instance()["dlcm_Activ_Title"] = cbx_Activ_Title.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_TitleSort"] = cbx_Activ_Title_Sort.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_Artist"] = cbx_Activ_Artist.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] = cbx_Activ_Artist_Sort.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_Album"] = cbx_Activ_Album.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_AlbumSort"] = cbx_Activ_Album_Sort.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_FileName"] = cbx_Activ_File_Name.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Activ_LyricsInfo"] = cbx_Activ_Lyric_Info.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Groups"] = cbx_Groups.Text;
            ConfigRepository.Instance()["dlcm_Split4Pack"] = txt_NoOfSplits.Text;
            ConfigRepository.Instance()["dlcm_FilterPrevious"] = cbx_Groups.Text;
            ConfigRepository.Instance()["dlcm_FilterCompound"] = chbx_FilterCompound.Checked ? "Yes" : "No";
            //ConfigRepository.Instance()["dlcm_Grouping"] = rbtn_Population_All.Checked ? "All" : (rbtn_Population_Groups.Checked ? "Groups" : (rbtn_Population_Selected.Checked ? "Selected" : (rbtn_Population_PackNO.Checked ? "Split" : "")));

            saveOptions(chbx_Additional_Manipulations);

            if (chbx_Configurations.SelectedIndex >= 0)
                //{
                SaveProfileToDB(oldprof, cnb, cnc);
            SaveOK = "";
            //}
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
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Title: " + Manipulate_strings(txt_Title.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, false, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Sort Title: " + Manipulate_strings(txt_Title_Sort.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Artist: " + Manipulate_strings(txt_Artist.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, false, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Sort Artist: " + Manipulate_strings(txt_Artist_Sort.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Album: " + Manipulate_strings(txt_Album.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, false, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "FileName: " + Manipulate_strings(txt_File_Name.Text, 0, true, false, false, SongRecord, "", "", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void Export_To_Click(object sender, EventArgs e)
        {
            if (cbx_Export.Text == "Excel") ExportExcel();
            else
                ExportHTML();
        }

        public void ExportHTML()
        {
            SaveOK = "OK"; SaveSettings(); SaveOK = "";
            var ExportFields = ConfigRepository.Instance()["dlcm_ExportFields"];

            var ExportTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var cmd = "SELECT " + ExportFields.Replace("CDLC_ID=\"\"", "CDLC_ID = CSTR(M.ID)").Replace("dlcm_0_old", "\"" + ConfigRepository.Instance()["dlcm_TempPath"] + "\"") + " FROM Main M ";
            cmd = cmd.Replace("\\\\", "\\").Replace("\\\\", "\\");
            //if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = 'Yes'";
            //else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type='DLC' AND Groupz='" + cbx_Groups.Text + "')";
            //else if (rbtn_Population_PackNO.Checked)

            var Filtertxt = cbx_Groups.Text;
            var OrderAlt = "";
            if (Filtertxt == "") return;
            //if (chbx_AutoSave.Checked) SaveRecord(); SaveOK = false;

            var i = 0;// databox.SelectedCells.Count == 0 ? 0 : databox.SelectedCells[0].RowIndex;
            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = cmd;/*, "Part of No Group", "Part of Any Group" */
            //cmd = GetFilter(Filtertxt, cmd, i, SearchCmdf);

            cmd += GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text, cnb, "", 0,
                GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc);

            DataSet dt;

            dt = SelectFromDB("Main", cmd, "", cnb, cnc);

            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append("<html >");
            strHTMLBuilder.Append("\n<head>");

            var norecx = GetNoRec(dt, cnb, cnc); //dt.Tables.Count > 0 ? dt.Tables[0].Rows.Count : 0;
            if (norecx == 0)
            {
                MessageBox.Show("Nothin' to export.");
                return;
            }
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.Maximum = norecx;

            strHTMLBuilder.Append("<style type=\"text/css\">" +
                "\n.tg  {border-collapse:collapse; border-spacing:0; border-color:#aabcfe;}" +
                "\n.tg td{font-family:Arial, sans-serif; font-size:14px; padding: 10px 5px; border-style:solid; border-width:1px; overflow: hidden; word -break:normal; border-color:#aabcfe;color:#669;background-color:#e8edff;white-space: nowrap;}" +
                "\n.tg th{font-family:Arial, sans-serif; font-size:14px; font-weight:normal; padding: 10px 5px; border-style:solid; border-width:1px; overflow: hidden; word -break:normal; border-color:#aabcfe;color:#039;background-color:#b9c9fe;}" +
                "\n.tg.tg-hmp3{background-color:#D2E4FC;text-align:left;vertical-align:top}" +
                "\n.tg.tg-0lax{ text-align:left; vertical-align:top}" +
                            "\n</style> ");
            strHTMLBuilder.Append("\n</head>");
            strHTMLBuilder.Append("\n<body>");
            strHTMLBuilder.Append("\n<table class=\"tg\"");
            //border ='1px' cellpadding='1' cellspacing='1' bgcolor='lightyellow' style='font-family:Garamond; font-size:smaller'>");

            strHTMLBuilder.Append("\n<tr>\n");
            foreach (DataColumn myColumn in dt.Tables[0].Columns)
            {
                strHTMLBuilder.Append("\n<th class=\"tg-0lax\">");
                strHTMLBuilder.Append(myColumn.ColumnName);
                strHTMLBuilder.Append("</th>");

            }

            strHTMLBuilder.Append("</tr>");

            var col = 0;
            var idd = "";
            var dir = ConfigRepository.Instance()["dlcm_TempPath"] + "\\" + "0_temp" + "\\" + ExportTime;
            Directory.CreateDirectory(dir);
            string al = "";
            i = 0; var samealum = false;
            var repetrow = 0;
            foreach (DataRow myRow in dt.Tables[0].Rows)
            {
                strHTMLBuilder.Append("\n<tr>");
                col = 0; string hey = ""; i++;
                idd = myRow["ID"].ToString();
                /* var baseFileNamelead = "";*/
                var baseFileName = ""; var baseFile = "";/* var arrtype = ""; *//*var baseFileNamebass2 = "";*/ /*var baseFileNamerhthym2 = "";*/
                repetrow = 0;
                if (al == myRow["Album"].ToString()) samealum = true;
                else samealum = false;
                foreach (DataColumn myColumn in dt.Tables[0].Columns)
                {
                    baseFileName = ""; /*arrtype = ""; */baseFile = ""; /*arrtype = "";*/
                    var fileName = Path.GetFileName(myRow[myColumn.ColumnName].ToString());
                    var ffile = myRow[myColumn.ColumnName].ToString();

                    if (ConfigRepository.Instance()["dlcm_AdditionalManipul95"].ToLower() == "yes")
                        try
                        {
                            File.Copy(ffile, dir + "\\" + idd + fileName, true);

                            //Convert to tabs
                            var splitPoint = Path.GetFileNameWithoutExtension(fileName).LastIndexOf('_');
                            var arrangement = Path.GetFileNameWithoutExtension(fileName).Substring(splitPoint + 1);
                            // skip any files for vocals and/or showlights
                            if (arrangement.ToLower() == "showlights")/*arrangement.ToLower() == "vocals" || a*/
                                continue;
                            //Song2014 rs2014Song;

                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul96"].ToLower() == "yes")
                                hey = getGP5(dir, idd, fileName, myRow["Original_File"].ToString(),
                                     myRow["Song_Title"].ToString(), myRow["Artist"].ToString(), myRow[myColumn.ColumnName].ToString(), timestamp);
                            string[] ret = hey.Split(';');
                            baseFile = ret[0];
                            baseFileName = ret[1];
                        }
                        catch (Exception Ex)
                        {
                            var tsst = "Issues at copy filestrem..." + Ex.Message.ToString(); timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }

                    col++;

                    if (myRow[myColumn.ColumnName].ToString().IndexOf(".png") > 0 || myColumn.ColumnName.ToString() == "Album" || myColumn.ColumnName.ToString() == "Album_Year")
                    {
                        var r = true;
                        if (i < GetNoRec(dt, cnb, cnc))/*dt.Tables[0].Rows.Count*/
                            if (dt.Tables[0].Rows[i].ItemArray[5].ToString() == myRow["Album"].ToString() && !samealum) /*al*/
                            {
                                r = false;
                                for (repetrow = 1; repetrow < GetNoRec(dt, cnb, cnc) - i; repetrow++)
                                    if (dt.Tables[0].Rows[i + repetrow].ItemArray[5].ToString() != myRow["Album"].ToString())
                                        break;
                                strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" rowspan=\"" + (repetrow + 1) + "\"");

                                if (File.Exists(dir + "\\" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString())))
                                    strHTMLBuilder.Append("><img width = 50px height = 50px src = \"" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()) + "\"></td>");
                                else
                                if (myColumn.ColumnName.ToString() == "Album" || myColumn.ColumnName.ToString() == "Album_Year")
                                    strHTMLBuilder.Append(">" + myRow[myColumn.ColumnName].ToString() + "</td>");
                                else strHTMLBuilder.Append("\"></td>");
                            }

                        if ((al != myRow["Album"].ToString()) && r)/**/
                        {
                            strHTMLBuilder.Append("\n<td class=\"tg-dg7a\"");
                            if (File.Exists(dir + "\\" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString())))
                                strHTMLBuilder.Append("><img width = 50px height = 50px src = \"" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()) + "\"></td>");
                            else
                            if (myColumn.ColumnName.ToString() == "Album" || myColumn.ColumnName.ToString() == "Album_Year")
                                strHTMLBuilder.Append(">" + myRow[myColumn.ColumnName].ToString() + "</td>");
                            else
                                strHTMLBuilder.Append("></td>");
                        }
                    }

                    else if (myRow[myColumn.ColumnName].ToString().IndexOf(".ogg") > 0)
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\">");
                        if (!File.Exists(dir + "\\" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()))) { strHTMLBuilder.Append("></td>"); continue; }
                        strHTMLBuilder.Append("<audio controls><source src=\"" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()) + "\" type=\"audio/ogg\"> ");
                        strHTMLBuilder.Append("Your browser does not support the audio element.</audio>");
                        strHTMLBuilder.Append("</td>");
                    }
                    else if (myColumn.ColumnName == "Lyrics")
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" ");
                        if (!File.Exists(dir + "\\" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()))) { strHTMLBuilder.Append("></td>"); continue; }
                        strHTMLBuilder.Append("onclick = \"location.href='" + idd + myRow["Artist"].ToString() + myRow["Song_Title"].ToString() + ".txt\" style=\"cursor: pointer\" > ");
                        strHTMLBuilder.Append("Lyrics");
                        strHTMLBuilder.Append("</td>");
                    }
                    else if (myColumn.ColumnName.ToString().IndexOf("Tab_") >= 0)
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" ");
                        if (!File.Exists(dir + "\\" + baseFileName + ".txt")) { strHTMLBuilder.Append("></td>"); continue; }
                        strHTMLBuilder.Append("onclick = \"location.href='" + baseFileName + ".txt'\" style=\"cursor: pointer\" > ");
                        strHTMLBuilder.Append("Tab");
                        strHTMLBuilder.Append("</td>");
                    }
                    else if (myColumn.ColumnName.ToString().IndexOf("GP5_") >= 0)
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" ");
                        if (!File.Exists(dir + "\\" + baseFile + ".gp5")) { strHTMLBuilder.Append("></td>"); continue; }
                        strHTMLBuilder.Append("onclick = \"location.href='" + baseFile + ".gp5'\" style=\"cursor: pointer\" > ");
                        strHTMLBuilder.Append("GP5");
                        strHTMLBuilder.Append("</td>");
                    }
                    else if (myRow[myColumn.ColumnName].ToString().IndexOf(":\\") > 0)
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" ");
                        if (!File.Exists(dir + "\\" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()))) { strHTMLBuilder.Append("></td>"); continue; }
                        strHTMLBuilder.Append("onclick = \"location.href='" + idd + Path.GetFileName(myRow[myColumn.ColumnName].ToString()) + "'\" style=\"cursor: pointer\" > ");
                        strHTMLBuilder.Append(Path.GetFileName(myRow[myColumn.ColumnName].ToString()));
                        strHTMLBuilder.Append("</td>");
                    }
                    else if (myRow[myColumn.ColumnName].ToString().IndexOf("//") > 0)
                    {
                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\" ");
                        strHTMLBuilder.Append("onclick = \"location.href='" + myRow[myColumn.ColumnName].ToString() + "'\" style=\"cursor: pointer\" > ");
                        strHTMLBuilder.Append("link");
                        strHTMLBuilder.Append("</td>");
                    }
                    else
                    {

                        strHTMLBuilder.Append("\n<td class=\"tg-dg7a\">");
                        strHTMLBuilder.Append(myRow[myColumn.ColumnName].ToString());
                        strHTMLBuilder.Append("</td>");
                    }

                }

                al = myRow["Album"].ToString();
                strHTMLBuilder.Append("</tr>");

                pB_ReadDLCs.Increment(1);
            }

            //Close tags.  
            strHTMLBuilder.Append("\n</table>");
            strHTMLBuilder.Append("\n</body>");
            strHTMLBuilder.Append("\n</html>");

            string Htmltext = strHTMLBuilder.ToString();

            System.IO.File.WriteAllText(dir + "\\000Export" + ExportTime + ".HTML", Htmltext);
            StartProcesss(@dir, null);
            //try
            //{
            //    Process process = Process.Start(@dir);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Song Folder in Exporer ! ");
            //}
        }



        public static string getGP5(string dir, string idd, string fileName, string OriginalFileN, string SongTitle
            , string ArtistF, string xmlc, DateTime timestamp)
        {
            string baseFile = ""; string baseFileName = "";
            bool vocal = true; bool validXML = true;

            using (var obj = new RocksmithToolkitGUI.CDLC2Tab.CDLC2Gp5())
            {
                try
                {
                    var mf = new ManifestFunctions(GameVersion.RS2014);
                    var score = new Score();
                    var browser = new PsarcBrowser(OriginalFileN);
                    var toolkitInfo = browser.GetToolkitInfo();

                    try
                    {
                        var xmlContent = Vocals.LoadFromFile(xmlc);
                        var arrtype = xmlContent.Vocal.ToString();
                    }
                    catch (Exception Ex)
                    {
                        vocal = false;
                        var tsst = "Invalid XML..." + Ex.Message.ToString(); timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        try
                        {
                            var rrangemen = Song2014.LoadFromFile(xmlc);
                        }
                        catch (Exception Exx)
                        {
                            validXML = false;
                            tsst = "Invalid XML..." + Exx.Message.ToString(); timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }
                    }

                    if (vocal)
                    {
                        var t = ""; var l = ""; var lyric = "";
                        var Lyrics = Vocals.LoadFromFile(xmlc);
                        for (var i = 0; i < Lyrics.Vocal.Length; i++)
                        {
                            l = Lyrics.Vocal[i].Lyric; t = "";
                            if (i > 0) if (Lyrics.Vocal[i].Time - Lyrics.Vocal[i - 1].Time - Lyrics.Vocal[i].Length > 3) t = "\n(" + Lyrics.Vocal[i].Time + ")\n";
                            if (l.IndexOf("+") >= 0)
                                lyric += t + " " + (l.Length > 1 ? l.Substring(0, l.Length - 1) : l) + "\n";
                            else lyric += t + " " + l;
                        }
                        baseFile = idd + ArtistF + " - " + SongTitle + " - " + "Vocals";
                        lyric = lyric.Replace("- ", "").Replace("-", "").Replace("\n ", "\n").Trim();
                        using (var stream = File.Open(dir + "\\" + baseFile + ".txt", FileMode.Create)) ;
                        using (var sw = File.AppendText(dir + "\\" + baseFile + ".txt"))
                        {
                            sw.WriteLine(lyric);
                        }
                    }
                    if (validXML)
                    {
                        var rrangement = Song2014.LoadFromFile(xmlc);
                        var rs2014Song = obj.PsarcToSong2014(OriginalFileN, null, rrangement.Arrangement.ToLower());
                        using (var obj2 = new RocksmithToolkitLib.Conversion.Rs2014Converter())
                            obj2.Song2014ToAsciiTab(rs2014Song, dir, false);
                        RocksmithToolkitGUI.CDLC2Tab.CDLC2Gp5.ExportArrangement(score, rrangement, 999, dir + "\\" + idd + fileName, toolkitInfo);
                        baseFile = score.Title + " - " + rrangement.Arrangement;
                        baseFileName = idd + RocksmithToolkitGUI.CDLC2Tab.CDLC2Gp5.CleanFileName(String.Format("{0} - {1} - {2}", ArtistF,
                            SongTitle, rrangement.Arrangement));
                        if (File.Exists(dir + "\\" + baseFile + ".txt"))
                        {
                            File.Copy(dir + "\\" + baseFile + ".txt", dir + "\\" + baseFileName + ".txt", true);
                            File.Delete(dir + "\\" + baseFile + ".txt");
                            baseFile = baseFileName;
                        }
                        RocksmithToolkitGUI.CDLC2Tab.CDLC2Gp5.SaveScore(score, baseFileName, dir, "gp5");
                    }
                }
                catch (Exception Ex)
                {
                    var tsst = "Error export tabs7lyrics..." + Ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                }
            }
            return baseFile + ";" + baseFileName;
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

            var DB_Path = txt_DBFolder.Text;// (chbx_DefaultDB.Checked == true ? MyAppWD + "\\AccessDB.accdb;" :
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //connectionString = "Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path;
            //cnn = new SqlConnection(connectionString);
            //cnn.Open();
            //sql = "SELECT * FROM Main";
            //SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            //DataSet ds = new DataSet();
            //dscmd.Fill(ds);
            //dscmd.Dispose();

            DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT * FROM Main", txt_DBFolder.Text, cnb, cnc);
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
                var tsst = "Exception Occured while releasing object " + ex.Message.ToString() + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btn_Cleanup_MainDB_Click(object sender, EventArgs e)
        {

            SaveOK = "OK"; SaveSettings(); SaveOK = "";
            var cmd = "SELECT * FROM Main";// ID IN (" + GetFilter(cbx_Groups.Text, SearchCmd, 0, "") + ")";// GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_Selected.Checked, rbtn_Population_Groups.Checked
                                           //, rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text);
            cmd += GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text, cnb, "", 0,
                GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc);

            //Read from DB
            int i = 1;
            UtilitiesFunctions.MainDBfields[] SongRecord = new UtilitiesFunctions.MainDBfields[20000];
            SongRecord = UtilitiesFunctions.GetRecord_s(cmd, cnb, cnc);

            var tst = cmd; timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            tst = "Removing the following Songs-IDs..."; timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


            var norows = SongRecord[0].NoRec.ToInt32();
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.Maximum = norows;
            cmd = "DELETE FROM Main WHERE ID IN (";
            var ids = "";
            var hash = "";
            if (norows > 0)
                foreach (var song in SongRecord)
                {
                    if (song != null)
                    {
                        tst = "Removing ..." + song.Original_FileName.ToString(); timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        ids += song.ID.ToString();
                        hash += song.Original_File_Hash.ToString();
                        i++;
                        if (i <= norows) { ids += ", "; hash += ", "; }
                        if (i > norows) break;
                    }
                    pB_ReadDLCs.Increment(1);
                }
            cmd += ids + ");";
            var DB_Path = txt_DBFolder.Text; ;// (chbx_DefaultDB.Checked == true ? MyAppWD + "\\..\\AccessDB.accdb;" : );

            var TempPath = txt_TempPath.Text;
            DeleteRecords(ids, cmd, DB_Path, TempPath, norows.ToString(), hash, cnb, pB_ReadDLCs, cnc);
            SetImportNo();
        }

        // Read a Folder (clean temp folder)        // Decompress the PC DLCs
        // Read details and populate a DB (clean Import DB before, and only populate Main if not there already)
        public void btn_PopulateDB_Click(object sender, EventArgs e)
        {
            int totalFiles = 0; int Weridfiles = 0; string WeridfilesName = ""; int importhashdupli = 0; string importhashduplinames = "";
            int existhashdupli = 0; string existhashduplinames = ""; int importhashdupli2 = 0; string importhashdupli2names = "";
            int invalids3 = 0; string invalids3names = ""; int invalids = 0; string invalidsnames = "";
            manualdec = 0; manualdecnames = ""; automdec = 0; automdecnames = "";

            var Temp_Path_Import = txt_TempPath.Text;
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            var tst = "Starting... " + startT; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            bool stopp = false;
            var dflt_Path_Import = txt_TempPath.Text + "\\0_to_import";
            var old_Path_Import = txt_TempPath.Text + "\\0_old";
            var dataPath = txt_TempPath.Text + "\\0_data";
            var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
            var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
            var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
            var repacked_Path = txt_TempPath.Text + "\\0_repacked";
            var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
            var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
            var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
            var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
            var Log_PSPath = txt_TempPath.Text + "\\0_log";
            var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
            var Archive_Path = txt_TempPath.Text + "\\0_archive";
            var Temp_Path = txt_TempPath.Text + "\\0_temp";
            var log_Path = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
            string pathDLC = txt_RocksmithDLCPath.Text;

            var slct = "SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u " +
"WHERE Type=\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" and Comments like \"%dlcm_AdditionalManipul%\"" +
" AND DisplayGroup in ('Import','General') ORDER BY DisplayGroup ASC";
            Selection frm1 = new Selection(slct, "Continue Importing", "Stop");
            frm1.ShowDialog();
            if (frm1.StopImport) return;
            SaveProfileToDB(c("dlcm_Configurations"), cnb, cnc);
            chbx_Additional_Manipulations = GenerateParamsList(chbx_Additional_Manipulations, slct);
            lbl_Settings.Text = lbl_Settings.Text + " " + chbx_Additional_Manipulations.Items.Count.ToString() + "/" + GetNoRecords(slct, cnb, cnc).ToString();

            var gg = GetParam(24, chbx_Additional_Manipulations);
            var DB_Path = txt_DBFolder.Text;
            var errr = true;
            SaveOK = "OK"; SaveSettings(); SaveOK = "";
            tst = "end save settings Start ..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            tst = "Assessing to clean Folders..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            DialogResult result1 = DialogResult.No;
            DialogResult result2 = DialogResult.No;

            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !GetParam(38, chbx_Additional_Manipulations)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {
                var pth = txt_TempPath.Text + "\n\\0_old\n\\0_duplicate\n\\0_repacked\n\\0_broken\n" + log_Path +
                    "\n\\0_repacked\\PC\n\\0_repacked\\PS3\n\\0_repacked\\MAC\n\\0_repacked\\XBOX360\n\\dlcpacks\n\\0_dlcpacks\\manifests" +
                    "\n\\0_dlcpacks\\temp\n\\0_dlcpacks\\manipulated\n\\0_dlcpacks\\origs\n\\0_albumCovers\nNOT: \n\\0_archive\n\\0_to_import";
                result1 = MessageBox.Show("Are you sure you want to DELETE (to Recycle BIN) the following folders:\n\n"
                    + pth +
                    //"\n\n" + dataPath + "\n" + Temp_Path + 
                    "\n\n NOTE All PSARCs are moved at deletion to 0\\0_archive\n&All Logs are kept in 0\\0_Log Folder \nDB: "
                    + c("dlcm_DBFolder") + "\n\n\"No\" Skipps the Delete\n\"Cancel\" stops the Import!", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Cancel) return;
                //else if (result1 == DialogResult.No) ;
                //else 
                try
                {
                    DialogResult rets = CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC,
        repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);
                    //if (rets != DialogResult.No && rets != DialogResult.Yes)
                    //    return;
                    if (result1 == DialogResult.Yes)
                    {
                        //clean app working folders 0 folder //Delete Files
                        CleanFolder(txt_TempPath.Text, ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_old", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(dataPath, ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_repacked", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\PC", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\PS3", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\MAC", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_repacked\\XBOX360", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_duplicate", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manifests", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks", ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\temp", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manipulated", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\manipulated\\temp", ".accdb;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_dlcpacks\\origs", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_temp", ".accdb;.db;.psarc;.zip", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(txt_TempPath.Text + "\\0_data", ".accdb;.db;.psarc", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        result2 = MessageBox.Show("Are you sure you want to DELETE Standardizations (&Spotify downloaded info)?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result2 == DialogResult.Yes) CleanFolder(txt_TempPath.Text + "\\0_albumCovers", "", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(broken_Path_Import, ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        CleanFolder(log_Path, ".accdb;.db;.psarc", false, true, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //Delete Folders
                        System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text); var oldvl = ConfigRepository.Instance()["dlcm_AdditionalManipul81"]; ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = "Yes";
                        foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                        {
                            if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate"
                                && dir.Name != "0_log" && dir.Name != "0_albumCovers" && dir.Name != "0_archive" && dir.Name != "0_data" && dir.Name != "0_temp" && dir.Name != "0_to_import")
                                DeleteDirectory(dir.FullName, false);
                        }

                        System.IO.DirectoryInfo downloadedMessageInfo3 = new DirectoryInfo(txt_TempPath.Text + "\\0_data");
                        foreach (DirectoryInfo dir in downloadedMessageInfo3.GetDirectories())
                        {
                            if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate"
                                && dir.Name != "0_log" && dir.Name != "0_albumCovers" && dir.Name != "0_archive" && dir.Name != "0_data" && dir.Name != "0_temp" && dir.Name != "0_to_import")
                                DeleteDirectory(dir.FullName, false);
                        }

                        if (DirectoryExists(txt_TempPath.Text + "\\0_dlcpacks"))
                        {
                            System.IO.DirectoryInfo downloadedMessageInfo7 = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks");
                            var p = 0;
                            foreach (DirectoryInfo dir in downloadedMessageInfo7.GetDirectories())
                            {
                                p++;
                                tst = "Assessing to clean Folders " + p + "..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                try
                                {
                                    if (dir.Name != "temp" && dir.Name != "manipulated" && dir.Name != "manifests" && dir.Name != "origs")
                                        DeleteDirectory(dir.FullName, false);
                                }
                                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                            }
                        }
                        ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
                    }
                    tst = "end folder Cleaning..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex)
                {
                    var tsst = "Error 123..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("l1009Can not delete folders:\n\n" +
                        pth + "\n\nPaths:\n"
                        + broken_Path_Import + "\n" + log_Path + "\n"
                        + AlbumCovers_PSPath + "\n" + Log_PSPath + "\n" + Archive_Path + "\n" + dataPath);
                }
            }

            DialogResult res = CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC,
                repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);
            if (res != DialogResult.No && res != DialogResult.Yes)
                return;

            // Clean temp log
            CreateLog(logPath);
            //var fnl = (logPath == null || logPath == "" ? Log_PSPath : logPath) + "\\" + "current_temp.txt";
            //StreamWriter sw = new StreamWriter(File.OpenWrite(fnl));
            //sw.Write("Some stuff here");
            //sw.Dispose();

            //help code
            //using (var u = new UpdateForm())
            //{
            //    u.Init(onlineVersion);
            //    u.ShowDialog();
            //}
            DataSet dsR = new DataSet();
            if (chbx_Rebuild.Checked)
            {
                string cmdR = @"SELECT * FROM Main as M ";
                //WHERE cstr(ID) IN (";
                ////if (rbtn_Population_Selected.Checked == true)
                //cmdR += GetFilter(cbx_Groups.Text,SearchCmd,0,"",cnb,"")+")";
                ////else if (rbtn_Population_Groups.Checked) cmdR += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + Groupss + "\")";

                cmdR += " ORDER BY Artist";
                DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", cmdR, txt_DBFolder.Text, cnb, cnc);
                int noOfRecR = GetNoRec(dsR, cnb, cnc);//dsR.Tables[0].Rows.Count;
                tst = "Rebuilding" + noOfRecR + "/" + (noOfRecR) + " Songs already imported in MainDB";
                timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                if (noOfRecR > 0)
                {
                    pB_ReadDLCs.Value = 0;
                    pB_ReadDLCs.Maximum = 2 * (noOfRecR - 1);
                }
                tst = "Rebuild select ended."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //Clean ImportDB
            DeleteFromDB("Import", "DELETE * FROM Import;", cnb, cnc);
            tst = "Assesing if Cleaning....Import table...."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (chbx_CleanTemp.Checked)
            {
                DeleteFromDB("Main", "DELETE * FROM Main;", cnb, cnc);
                DeleteFromDB("Arrangements", "DELETE * FROM Arrangements;", cnb, cnc);
                DeleteFromDB("Tones", "DELETE * FROM Tones;", cnb, cnc);
                DeleteFromDB("LogPacking", "DELETE * FROM LogPacking;", cnb, cnc);
                DeleteFromDB("LogPackingError", "DELETE * FROM LogPackingError; ", cnb, cnc);
                DeleteFromDB("LogImporting", "DELETE * FROM LogImporting;", cnb, cnc);
                DeleteFromDB("LogImportingError", "DELETE * FROM LogImportingError;", cnb, cnc);
                DeleteFromDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail;", cnb, cnc);
                DeleteFromDB("Tones_GearList", "DELETE * FROM Tones_GearList;", cnb, cnc);
                result1 = MessageBox.Show("Are you sure you want to DELETE All Groups related entities (Profiles, Song Groups/Setlist) Except the Default grp/profile?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes)
                {
                    DeleteFromDB("Groups", "DELETE * FROM Groups WHERE TYPE=\"Profile\" and Profile_Name<>\"Default\";", cnb, cnc);
                    DataSet dxf = new DataSet(); var cmds = "UPDATE Groups SET Groupz=\"Default\" WHERE Comments ='dlcm_Configurations' AND Profile_Name==\"Default\";";
                    dxf = UpdateDB("Groups", cmds, cnb, cnc);
                }
                DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail;", cnb, cnc);
                if (result2 == DialogResult.Yes) DeleteFromDB("Standardization", "DELETE * FROM Standardization;", cnb, cnc);
                DeleteFromDB("Cache", "DELETE * FROM Cache;", cnb, cnc);
                DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" or Type=\"Retail\";", cnb, cnc);
                //try { cnb.Close(); cnc.Close(); } catch (Exception ex) {; }


                UtilitiesFunctions.CompactAndRepair(cnb);
                //OpenDb();
                tst = DB_Path + " Cleaned tables"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            tst = "Assesing if CheckValidityGetHASH Add 2 Import"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            int i = 0;
            var ImportPackNo = "0";
            DataSet doz = new DataSet();
            doz = SelectFromDB("Main", "SELECT MAX(s.ID) FROM Main s;", txt_DBFolder.Text, cnb, cnc);
            var noOfRecz = GetNoRec(doz, cnb, cnc);
            //try { noOfRecz = doz is null ? 0 : (doz.Tables.Count == 0 ? 0 : doz.Tables[0].Rows.Count); } catch (Exception ex) { noOfRecz = 0; }
            int gh = 0;
            //if (doz is not null)//(noOfRecz >=1)
            //    if (doz.Tables.Count > 0)
            //        if (doz.Tables[0].Rows.Count>0)

            if (noOfRecz.ToString() != "0")
                gh = doz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();

            if (noOfRecz > 0) ImportPackNo = (gh + 1).ToString();
            if (ImportPackNo == "" || ImportPackNo == "0") ImportPackNo = "1";
            var invalid = "No";
            string[] filez = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
            var viles = "";
            if (!GetParam(38, chbx_Additional_Manipulations)) //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                //GetDirList and calcualte hash for the IMPORTED file
                var searchPattern = new Regex(
                        @"(psarc|xbox)",
                        RegexOptions.IgnoreCase);
                if (GetParam(37, chbx_Additional_Manipulations)) //38. Import other formats but PC, as well(separately of course)
                                                                 //filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
                {
                    var files = Directory.EnumerateFiles(pathDLC)
                    .Where(f => searchPattern.IsMatch(f))
                    .ToList();
                    var ig = 0;
                    foreach (var fg in files)
                    {
                        filez[ig] = fg;
                        viles += (ig + 1) + ". " + fg + "\n";
                        ig++;
                    }
                }
                else
                    filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");

                pB_ReadDLCs.Maximum = countFilez(filez);

                totalFiles = countFilez(filez) - 1;

                var tre = countFilez(filez);
                var doneEvent = new AutoResetEvent(false);
                inserts = "";
                i = 1; mutit = 0; var x = 0;
                for (x = 0; x < countFilez(filez); x++) insrts[x] = "";

                tst = "CheckValidityGetHASH to Add2Import"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                pB_ReadDLCs.Maximum = x; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                foreach (var s in filez)
                {
                    if (s == null) break;
                    invalid = "No";
                    if (s.IndexOf("rs1compatibilitydisc_p.psarc") > 0 || s.IndexOf("rs1compatibilitydisc_m.psarc") > 0 || s.IndexOf("rs1compatibilitydisc_p_Pc.psarc") > 0 || s.IndexOf("rs1compatibilitydlc_p.psarc") > 0
                        || s.IndexOf("rs1compatibilitydlc_m.psarc") > 0 || s.IndexOf("rs1compatibilitydisc.psarc.edat") > 0) invalid = "Yes";// continue;
                    if (1 == 2)
                    {
                        BackgroundWorker bwbVAlid = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                        bwbVAlid.DoWork += new DoWorkEventHandler(CheckValidityGetHASHAdd2Import);
                        bwbVAlid.ProgressChanged += new ProgressChangedEventHandler(ValidProgressChanged);
                        bwbVAlid.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ValidProcessCompleted);
                        bwbVAlid.WorkerReportsProgress = true;
                        var arg = s + ";" + i + ";" + ImportPackNo + ";" + invalid; /*+ ";" + DB_Path;+ logPath + ";" + Temp_Path_Import + ";"*/
                        bwbVAlid.RunWorkerAsync(arg);// CheckValidityGetHASHAdd2Import();
                                                     //Application.DoEvents();
                                                     //do
                                                     //Application.DoEvents();
                                                     //while (bwbVAlid.IsBusy);//keep singlethread as toolkit not multithread abled
                        Application.DoEvents();
                    }
                    else
                    {
                        insrts[i] = CheckValidityGetHASHAdd2ImportPub(s + ";" + i + ";" + ImportPackNo + ";" + invalid).ToString().Replace("\"", "").Replace("\"", "");
                        mutit = i - 1;
                    }
                    i++;
                    pB_ReadDLCs.Value = i;
                    //tst = "Assesing if CheckValidityGetHASH Add 2 Import" + i + "/" + (i - 1) + "..." + "/" + Path.GetFileNameWithoutExtension(s); timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //if (i==500) System.Threading.Thread.Sleep(20000);
                }
                pB_ReadDLCs.Maximum = i - 1; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                do
                    pB_ReadDLCs.Value = mutit;
                while (mutit < i - 2);//multithreading background workers --WAIT TILL ALL are done- 10
                                      //wait 10more sec 
                                      //mutit = 0;
                pB_ReadDLCs.Value = mutit;
                tst = mutit + "/" + (i - 1) + "CheckValidityGetHASH Add 2 Import"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                pB_ReadDLCs.Maximum = i - 1; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                if (DB_Path.Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
                {
                    var cmds = new OleDbCommand("BEGIN TRANSACTION", cnb);
                    var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";

                    cmds.CommandText = @"INSERT INTO Import (" + insertcmdd + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);

                    //if (cmds.Connection.State.ToString() == "Closed")
                    //    cmds.Connection.Open();
                    //cmds.Prepare();
                    //OleDbTransaction tran = cnb.BeginTransaction();
                    //cmds.Transaction = tran;
                    string[] args = new string[50]; for (var y = 0; y < 50; y++) args[y] = "";

                    for (var n = 1; n <= mutit + 1; n++)
                    {
                        string s = insrts[n];
                        if (s == null || s == "") continue;
                        args = (s).ToString().Split(';');
                        cmds.Parameters[0].Value = args[0];
                        cmds.Parameters[1].Value = args[1];
                        cmds.Parameters[2].Value = args[2];
                        cmds.Parameters[3].Value = args[3];
                        cmds.Parameters[4].Value = args[4];
                        cmds.Parameters[5].Value = args[5];
                        cmds.Parameters[6].Value = args[6];
                        cmds.Parameters[7].Value = args[7];
                        cmds.Parameters[8].Value = args[8];
                        cmds.Parameters[9].Value = args[9];
                        UpdateDBbyExecuteNonQuery(cmds, cnb, cnc);
                        //cmds.ExecuteNonQuery();
                        pB_ReadDLCs.Value = n;
                    }
                }
                else
                {

                    var cmds = new OleDbCommand("BEGIN TRANSACTION", cnb);
                    var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";

                    cmds.CommandText = @"INSERT INTO Import (" + insertcmdd + ") VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);
                    cmds.Parameters.Add("?", OleDbType.VarWChar, 255);

                    if (cmds.Connection.State.ToString() == "Closed")
                        cmds.Connection.Open();
                    cmds.Prepare();
                    OleDbTransaction tran = cnb.BeginTransaction();
                    cmds.Transaction = tran;
                    string[] args = new string[50]; for (var y = 0; y < 50; y++) args[y] = "";

                    for (var n = 1; n <= mutit + 1; n++)
                    {
                        string s = insrts[n];
                        if (s == null || s == "") continue;
                        args = (s).ToString().Split(';');
                        cmds.Parameters[0].Value = args[0];
                        cmds.Parameters[1].Value = args[1];
                        cmds.Parameters[2].Value = args[2];
                        cmds.Parameters[3].Value = args[3];
                        cmds.Parameters[4].Value = args[4];
                        cmds.Parameters[5].Value = args[5];
                        cmds.Parameters[6].Value = args[6];
                        cmds.Parameters[7].Value = args[7];
                        cmds.Parameters[8].Value = args[8];
                        cmds.Parameters[9].Value = args[9];
                        UpdateDBbyExecuteNonQuery(cmds, cnb, cnc);
                        //cmds.ExecuteNonQuery();
                        pB_ReadDLCs.Value = n;
                    }

                    tran.Commit();
                    cmds.Dispose();
                }
                //do
                //    Application.DoEvents();
                //while (bwbVAlid.IsBusy);//keep singlethread as toolkit not multithread abled
                tst = "end populating Import(inserting)..." + tre; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            else  //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                var ff = "-";
                ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
                var pth = txt_TempPath.Text;
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(pth);
                var no = 0;
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    var tzu = dir.ToString().IndexOf("0_");
                    if (dir.ToString().IndexOf("0_") == 0 || dir.ToString().IndexOf("ORIG") == 4 || dir.ToString().IndexOf("ORIG") == 3 || dir.ToString().IndexOf("CDLC") == 3 || dir.ToString().IndexOf("CDLC") == 4) continue;
                    //Populate ImportDB
                    tst += "\nFolder for: " + dir.Name + " :" + "s";
                    string insertcmds = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Invalid";
                    var pt = txt_RocksmithDLCPath.Text + "\\" + dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat").Replace("_Mac", "") + ".psarc";
                    var pxt = dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat").Replace("_Mac", "") + ".psarc";
                    System.IO.FileInfo ptz = null;
                    try
                    {
                        ptz = new System.IO.FileInfo(pt);
                        var insertvals = "\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name.Replace("_Pc", ".psarc").Replace("_Mac", ".psarc").Replace("_PS3", ".edat.psarc") + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"" + pxt + "\",\"" + ptz.CreationTime + "\",\"" + GetHash(pt) + "\",\"" + ptz.Length + "\",\"" + DateTime.Now + "\",\"" + ImportPackNo + "\",\"" + invalid + "\"";//,\"" + "0" + "\",\"";
                        InsertIntoDBwValues("Import", insertcmds, insertvals, cnb, mutit, cnc);
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    }
                    no++;
                }
                totalFiles = no;
                tst += "end create import based on already decompressed..." + no + " songas"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            // DELETE  obvious duplicates 
            var m = 0;
            //Remove werid files (smaller than 3ook, no consistent extension)
            var noOfRecs = 0; var frt = ""; var cmd = ""; var k = 0; var dt = "";
            if (GetParam(11, chbx_Additional_Manipulations)) /*&& !GetParam(79, chbx_Additional_Manipulations)*/
            {
                DataSet dry = new DataSet(); cmd = "SELECT ID, FullPath, FileName, FileSize FROM Import;";
                dry = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);
                noOfRecs = GetNoRec(dry, cnb, cnc);//(dry.Tables.Count == 0) ? 0 : dry.Tables[0].Rows.Count;
                var fnn = ""; double fsize = 0;
                if (noOfRecs > 0)
                {
                    pB_ReadDLCs.Maximum = noOfRecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                    tst += " Remove duplicate DLCs from this Import " + noOfRecs; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var IDs = "";
                    for (k = 0; k <= noOfRecs - 1; k++)
                    {
                        fnn = dry.Tables[0].Rows[k].ItemArray[1].ToString();
                        frt = dry.Tables[0].Rows[k].ItemArray[2].ToString();
                        fsize = double.Parse(dry.Tables[0].Rows[k].ItemArray[3].ToString().Replace("\"", "").Replace("\"", ""));
                        var not_r8 = false;
                        dt += "\n" + dry.Tables[0].Rows[k].ItemArray[0].ToString(); pB_ReadDLCs.Increment(1);
                        if (fnn.IndexOf(".psarc") > 0)
                            if (fnn.IndexOf(".psarc.edat") == fnn.IndexOf(".psarc"))
                                ;
                            else if (fnn.IndexOf(".psarc") == fnn.Length - 6)
                                ;
                            else not_r8 = true;

                        if (fsize <= 600000) not_r8 = true;

                        if (not_r8)
                        {
                            fnn = CopyMoveFileSafely(fnn, Archive_Path + "\\" + frt, GetParam(75, chbx_Additional_Manipulations), dry.Tables[0].Rows[k].ItemArray[2].ToString(), false);
                            IDs = dry.Tables[0].Rows[k].ItemArray[0].ToString() + ";";
                            Weridfiles++; WeridfilesName += "\n" + frt;
                        }
                    }
                    //Add to pack_audittrail duplicates from Current Import
                    //no dlc id attached to these entries
                    IDs = IDs == "" ? "0" : IDs.Substring(0, IDs.Length - 1);
                    DeleteFromDB("Import", "DELETE * FROM Import WHERE ID IN (" + IDs + ");", cnb, cnc);
                }
                tst = "end check&/delete same " + k + "hash from Import...Import_AuditTrail" + noOfRecs + dt; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //Remove HASH duplicate DLCs from this Import?
            noOfRecs = 0; frt = ""; cmd = ""; dt = "";
            if (!GetParam(67, chbx_Additional_Manipulations) && (!GetParam(79, chbx_Additional_Manipulations) || GetParam(68, chbx_Additional_Manipulations)))
            {
                DataSet dry = new DataSet(); cmd = "SELECT FullPath, FileName, FileHash, FullPath as CopyPath, \"" + broken_Path_Import + "\\\" as PackPath, ImportDate as PackDate, FileSize," +
                " \"\" as CDLC_ID, \"\" as DLC_Name, Platform,\"Invalides\" as Reason" + " FROM Import " +
                "WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);";
                dry = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);
                noOfRecs = GetNoRec(dry, cnb, cnc);//(dry.Tables.Count == 0) ? 0 : dry.Tables[0].Rows.Count;
                var fnn = "";
                if (noOfRecs > 0)
                {
                    pB_ReadDLCs.Maximum = noOfRecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                    tst += " Remove duplicate DLCs from this Import " + noOfRecs; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    for (k = 0; k <= noOfRecs - 1; k++)
                    {
                        fnn = dry.Tables[0].Rows[k].ItemArray[0].ToString();
                        frt = dry.Tables[0].Rows[k].ItemArray[1].ToString();
                        dt += "\n" + dry.Tables[0].Rows[k].ItemArray[0].ToString(); pB_ReadDLCs.Increment(1);
                        fnn = CopyMoveFileSafely(fnn, Archive_Path + "\\" + frt, GetParam(75, chbx_Additional_Manipulations),
                            dry.Tables[0].Rows[k].ItemArray[2].ToString(), false);
                        importhashdupli++; importhashduplinames = "\n" + frt;
                    }
                    //Add to pack_audittrail duplicates from Current Import
                    //no dlc id attached to these entries
                    DeleteFromDB("Import", "DELETE * FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);", cnb, cnc);
                }
                tst = "end check&/delete same " + k + "hash from Import...Import_AuditTrail" + noOfRecs + dt; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //REmove HASH duplicates from the import source/location
            noOfRecs = 0;
            if (!GetParam(67, chbx_Additional_Manipulations) && (!GetParam(79, chbx_Additional_Manipulations) || GetParam(68, chbx_Additional_Manipulations)))
            {
                DataSet drh = new DataSet(); cmd = "SELECT FullPath, FileName, FileHash, FullPath as CopyPath, \"" + broken_Path_Import + "\\\" as PackPath, ImportDate as PackDate, FileSize," +
                " \"\" as CDLC_ID, \"\" as DLC_Name, Platform,\"Invalides\" as Reason" + " FROM Import" +
                " WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);";
                drh = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);

                noOfRecs = GetNoRec(drh, cnb, cnc); //drh.Tables[0].Rows.Count;
                if (noOfRecs > 0)
                {
                    pB_ReadDLCs.Maximum = noOfRecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0; var trt = "";
                    tst += "REmove duplicates from the import source/location " + noOfRecs; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    for (i = 0; i <= noOfRecs - 1; i++)
                    {
                        trt += "\n" + drh.Tables[0].Rows[i].ItemArray[0].ToString(); pB_ReadDLCs.Increment(1);

                        CopyMoveFileSafely(drh.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_archive\\" + drh.Tables[0].Rows[i].ItemArray[1].ToString()
                            , GetParam(75, chbx_Additional_Manipulations), drh.Tables[0].Rows[i].ItemArray[2].ToString(), false);
                        existhashdupli++; existhashduplinames += "\n" + drh.Tables[0].Rows[i].ItemArray[1].ToString();
                    }

                    //Add to pack_audittrail duplicates from Current Import
                    //no dlc id attached to these entries
                    DeleteFromDB("Import", "DELETE * FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash" +
                        " WHERE d.ID is not null GROUP BY s.FileHash);", cnb, cnc);
                }
                tst = "-" + "/" + i + " Import files Inserted (excl. " + noOfRecs + " duplicates)" + frt; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import,
                  "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //REmove invalids from the import source/location
            noOfRecs = 0;
            DataSet dzh = new DataSet(); cmd = "SELECT FullPath, FileName, FileHash, FullPath as CopyPath, \"" + broken_Path_Import + "\\\" as PackPath, ImportDate as PackDate, FileSize," +
                " \"\" as CDLC_ID, \"\" as DLC_Name, Platform,\"Invalides\" as Reason" + " FROM Import WHERE Invalid=\"Yes\"";
            dzh = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);

            noOfRecs = GetNoRec(dzh, cnb, cnc); //dzh.Tables.Count > 0 ? dzh.Tables[0].Rows.Count : 0;
            var j = 0;
            if (noOfRecs > 0)
            {
                pB_ReadDLCs.Maximum = noOfRecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
                var trt = "";
                tst = "REmove invalides from the import source/location " + noOfRecs; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                for (j = 0; j <= noOfRecs - 1; j++)
                {
                    trt += "\n" + dzh.Tables[0].Rows[j].ItemArray[0].ToString(); pB_ReadDLCs.Increment(1);

                    CopyMoveFileSafely(dzh.Tables[0].Rows[j].ItemArray[0].ToString(), broken_Path_Import + "\\" + dzh.Tables[0].Rows[j].ItemArray[1].ToString()
                        , GetParam(75, chbx_Additional_Manipulations), dzh.Tables[0].Rows[j].ItemArray[2].ToString(), false);
                    invalids++; invalidsnames += "\n" + dzh.Tables[0].Rows[j].ItemArray[1].ToString();
                }
                //Add to pack_audittrail duplicates from Current Import
                //no dlc id attached to these entries

                //Delete Duplicates from Current Import
                DeleteFromDB("Import", "DELETE * FROM Import WHERE Invalid=\"Yes\"", cnb, cnc);
                tst = "-" + "/" + j + " Invalid Songs Not imported" + frt; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import,
                  "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //REMOVE duplicates already in Main (Duplicate of Import_AuditTrail?)
            cmd = "SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate, i.Pack, i.FileCreationDate, i.ID, i.Invalid, FullPath as CopyPath," +
                " \"" + old_Path_Import + "\\\" as PackPath, ImportDate as PackDate, m.ID as CDLC_ID, m.DLC_Name as DLC_Name, i.Platform" +
                ",\"Hash_Duplicates\" as Reason FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is not NULL";
            //    {// 1. If hash already exists do not insert
            DataSet dns = new DataSet(); var noOfRec = 0;

            dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);
            noOfRec = GetNoRec(dns, cnb, cnc); //dns.Tables.Count > 0 ? dns.Tables[0].Rows.Count : 0;

            var tft = "";
            var pack = GetMax("Pack_AuditTrail", "Pack", cnb, cnc);
            if (noOfRec > 0 && (!GetParam(67, chbx_Additional_Manipulations) && (!GetParam(79, chbx_Additional_Manipulations) || GetParam(68, chbx_Additional_Manipulations)))) //GetParam(29, chbx_Additional_Manipulations) && 30. When importing delete identical duplicates(same hash/filesize)!GetParam(67) && 
            {
                tft = "";
                for (m = 0; m < noOfRec; m++)
                {
                    tst = "REMOVE duplicates already in Main (Duplicate of Import_AuditTrail?) " + noOfRecs; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var newf = dns.Tables[0].Rows[m].ItemArray[0].ToString().Replace(pathDLC, Archive_Path);
                    tst = newf + "\n" + dns.Tables[0].Rows[m].ItemArray[0].ToString(); timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //if (GetParam(29, chbx_Additional_Manipulations)) //&& !File.Exists(newf) //As the new file might have a different name e.g. (1) ....etc.
                    //{
                    frt += "\n" + dns.Tables[0].Rows[m].ItemArray[0].ToString(); pB_ReadDLCs.Increment(1);
                    CopyMoveFileSafely(dns.Tables[0].Rows[m].ItemArray[0].ToString(), newf, GetParam(75, chbx_Additional_Manipulations)
                        , dns.Tables[0].Rows[m].ItemArray[3].ToString(), false);
                    importhashdupli2++; importhashdupli2names += "\n" + dns.Tables[0].Rows[m].ItemArray[2].ToString();
                    //}
                    tft += "\n Deleting " + dns.Tables[0].Rows[m].ItemArray[0].ToString() + " as imported on " + dns.Tables[0].Rows[m].ItemArray[5].ToString();
                }
                //Add to pack_audittrail duplicates from Current Import
                var insertcmd = "CopyPath, PackPath, FileName, FileHash, FileSize, PackDate, Pack, CDLC_ID, DLC_Name, Platform, Reason";
                cmd = "SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate, i.Pack, m.ID as CDLC_ID, m.DLC_Name as DLC_Name, i.Platform" +
                ",\"Hash_Duplicates\" as Reason FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is not NULL";
                InsertIntoDBwValues("Pack_AuditTrail", insertcmd, cmd, cnb, 0, cnc);/*+ ",\"" + pack+ "\""*/
                //Delete Duplicates from Current Import
                DeleteFromDB("Import", "DELETE * FROM Import WHERE ID IN (SELECT ID FROM (" + cmd + "));", cnb, cnc);
                tst = frt; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //START WITH mAINdb UPDATE
            //try { cnb.Close(); if (cnc is not null) cnc.Close(); } catch (Exception ex) {; }
            //OpenDb();
            cmd = "SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate, i.Pack as Pack, i.FileCreationDate as FileCreationDate, i.ID as ID, i.Invalid as Invalid FROM Import as i";
            cmd += " ORDER BY LEN(FileName) ASC, i.Platform='Pc' ASC ,i.Platform='Mac' ASC,i.Platform='PS3' ASC, i.FileName ASC;";


            DataSet ds = new DataSet(); ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb, cnc);
            noOfRec = GetNoRec(ds, cnb, cnc);//ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : 0;
            tst = noOfRec + "/" + (noOfRec + m + i + j) + " New Songs to Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (noOfRec > 0)
            {
                //Move duplicates to the end

                duplit = false;
                dupliNo = 0;
                dupliPrcs = 0;
                if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();

                for (j = 0; j < c("dlcm_maxsongsinDLCM").ToInt32(); j++) { dupliSongs[0, j] = "0"; dupliSongs[1, j] = "0"; }
                timestamp = UpdateLog(timestamp, "ending setting dupli array to 0.", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                var stats = "0";
                for (j = 0; j <= 1; j++)
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        pB_ReadDLCs.Maximum = noOfRec; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = i;
                        tst = "\n\nSTART importing song..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var songt = timestamp;
                        if (!(j == 1 && dupliSongs[0, i] == "0"))
                        {
                            if (j == 1 && dupliSongs[0, i] == "1") dupliPrcs++;
                            duplit = false;
                            stats = (j == 0 ? "" : "Duplicates: ") + (j == 0 ? (i + 1) : dupliPrcs) + "/" + (j == 0 ? noOfRec : dupliNo);
                            tst = stats;
                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            var bbroken = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                            var filehash = ds.Tables[0].Rows[i].ItemArray[3].ToString();

                            Random randomp = new Random();
                            int packid = randomp.Next(0, 100000);
                            timestamp = UpdateLog(timestamp, tst + " " + FullPath, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                            errr = false;
                            //    if (!GetParam(37, chbx_Additional_Manipulations))
                            //    //if (!FullPath.IsValidPSARC())
                            //    {
                            //        //MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                            //            if (GetParam(30, chbx_Additional_Manipulations))
                            //            {
                            //                File.Copy(FullPath.Replace(".psarc", ".invalid"), Pathh, true);
                            //                DeleteFile(FullPath.Replace(".psarc", ".invalid"));
                            //                //File.Delete(FullPath.Replace(".psarc", ".invalid"));
                            //            }
                            //            errr = true;
                            //            timestamp = UpdateLog(timestamp, "FAILED2 @Import cause of extension issue but copied in the broken folder", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                            //            UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb, cnc);
                            //            continue;
                            //}

                            var unpackedDir = "";
                            var packagePlatform = FullPath.GetPlatform();
                            if (j == 1)
                                unpackedDir = dupliSongs[1, i];
                            else
                            if (!GetParam(38, chbx_Additional_Manipulations)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                            {
                                var wwisePath = "";
                                if (!string.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                                    wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                                else
                                    wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                                if (wwisePath == "")
                                {
                                    ErrorWindow frm2 = new ErrorWindow("In order to use decompress Songs for previewig purposes, please Install Wwise Launcher then Wwise v" + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true, true, "", "", "");
                                    frm2.ShowDialog();
                                    if (frm2.IgnoreSong) break;
                                    if (frm2.StopImport) { j = 10; i = 9999; break; }
                                }
                                try
                                {
                                    // UNPACK
                                    tst = stats + " start unpacking..." + Path.GetFileName(FullPath); timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (GetParam(51, chbx_Additional_Manipulations))
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, SourcePlatform, true, true);
                                    else
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, SourcePlatform, true, false);
                                    tst = "end unpacking..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (unpackedDir.IndexOf("{") > 0 || unpackedDir.IndexOf("}") > 0)
                                    {
                                        if (!DirectoryExists(unpackedDir.Replace("{", "").Replace("}", ""))) Directory.Move(unpackedDir, unpackedDir.Replace("{", "").Replace("}", ""));
                                        unpackedDir = unpackedDir.Replace("{", "").Replace("}", "");
                                    }
                                    dupliSongs[1, i] = unpackedDir;
                                }
                                catch (Exception ex)
                                {
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    errr = false;
                                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    if (GetParam(30, chbx_Additional_Manipulations))
                                        CopyMoveFileSafely(FullPath, Pathh, GetParam(75, chbx_Additional_Manipulations), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                                    invalids3++; invalids3names += "\n" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb, cnc);
                                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                    errr = true;
                                }
                            }
                            else
                            {
                                unpackedDir = FullPath.Replace("_p.psarc", "_p_Pc").Replace("_m.psarc", "_m_Mac").Replace(".edat.psarc", "_PS3").Replace(txt_RocksmithDLCPath.Text, txt_TempPath.Text);
                            }

                            //Commenting Reorganize as they might have fixed the incompatib char issue
                            // REORGANIZE
                            //System.Threading.Thread.Sleep(1000);
                            var platform = FullPath.GetPlatform();
                            if (GetParam(36, chbx_Additional_Manipulations) && !errr) //37. Keep the Uncompressed Songs superorganized    
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
                                            var jsons = Directory.GetFiles(unpackedDir, string.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), System.IO.SearchOption.AllDirectories);
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
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at reorg" + unpackedDir + "---", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                    try
                                    {
                                        var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                                        if (structured)
                                        {
                                            unpackedDir = DLCPackageData.DoLikeProject(unpackedDir);
                                        }
                                    }
                                    catch (Exception ee)
                                    {
                                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        if (GetParam(30, chbx_Additional_Manipulations))
                                            CopyMoveFileSafely(FullPath, Pathh, GetParam(75, chbx_Additional_Manipulations),
                                                ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb, cnc);

                                        errr = true;
                                        timestamp = UpdateLog(timestamp, "FAILED2 @org but copied in the broken folder" + ee.Message, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }

                            stopp = false;
                            tst = "start processing..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            if (FullPath.ToLower().Contains("fontain"))
                                stopp = false;
                            string imported_status = Processing(i, j, tst, FullPath, DB_Path, errr, broken_Path_Import, ds, Temp_Path_Import
                                , dupli_Path_Import, old_Path_Import, cmd, unpackedDir, packid, ConfigRepository.Instance()["dlcm_AdditionalManipul106"] == "Yes" ? true : false, bbroken, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs
                                , dupliPrcs + "/" + dupliNo, dataPath, filehash, pack);
                            tst = "end processing song..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                            //Get Duplication assessment and its reason
                            string[] retunc = imported_status.Split(';');
                            var i_status = retunc[0]; var dupli_assesment_reason = ""; var dupli_assesment = ""; var CDLC_Name = "";
                            if (retunc.Count() == 2) CDLC_Name = retunc[1];
                            if (retunc.Count() == 3) dupli_assesment = retunc[2];
                            if (retunc.Count() == 4) dupli_assesment_reason = retunc[3];
                            if (retunc.Count() == 5) stopp = retunc[4] == "yes" ? true : false;
                            if (stopp) break;
                            else if (i_status != "0" && i_status != "" && i_status != null)// && imported != "ignored")
                            {
                                string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";
                                var insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate, i.Pack FROM Import as" +
                                    " i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);


                                //try { cnb/*.*/Close(); cnc.Close(); } catch (Exception ex) {; }
                                OpenDb();
                                var fcmd = "SELECT TOP 1 ID, FileHash FROM Import_AuditTrail ORDER BY ID DESC";
                                DataSet dus = new DataSet();
                                dus = SelectFromDB("Import_AuditTrail", fcmd, txt_DBFolder.Text, cnb, cnc);
                                var IAT_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                                var cmdupd = "UPDATE Import_AuditTrail Set Song_Title=\"" + infoorig.SongInfo.SongDisplayName + "\", Song_Title_Sort=\"" + infoorig.SongInfo.SongDisplayNameSort
                                    + "\", Album=\"" + infoorig.SongInfo.Album + "\", Album_Sort=\"" + infoorig.SongInfo.AlbumSort + "\", Artist=\"" + infoorig.SongInfo.Artist
                                    + "\", Artist_Sort=\"" + infoorig.SongInfo.ArtistSort + "\", Album_Year=\"" + infoorig.SongInfo.SongYear
                                    + "\", AlbumArtPath=\"" + infoorig.AlbumArtPath //+ "\", Album_ArtPathOrig=\"" + infoorig.AlbumArtPath
                                    + "\", DLC_Name=\"" + infoorig.Name + "\", DLC_AppID=\"" + infoorig.AppId + "\", PreviewLenght=\"" + PreviewLenghtorig
                                    + "\", audioBitrate=\"" + bitrateorig + "\", audioSampleRate=\"" + SampleRateorig + "\", CDLC_ID=\"" + i_status + "\""
                                    + " WHERE ID=" + IAT_ID;
                                DataSet dhs = new DataSet(); dhs = UpdateDB("Import_AuditTrail", cmdupd + ";", cnb, cnc);

                                cmdupd = "UPDATE Main Set Import_AuditTrail_ID=\"" + IAT_ID + "\" WHERE ID=" + i_status;
                                DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmdupd + ";", cnb, cnc);


                                //NullHandler(
                                if (dupli_assesment.ToLower() != "ignore" && dupli_assesment != null && dupli_assesment != "")// || (imported.ToLower().IndexOf("update") >= 0)
                                {
                                    //Generating the HASH code
                                    var FileHash = ""; var fpath = "";
                                    if (GetParam(15, chbx_Additional_Manipulations) || GetParam(75, chbx_Additional_Manipulations)) fpath = FullPath.Replace(txt_RocksmithDLCPath.Text, old_Path_Import);
                                    else fpath = FullPath;

                                    FileHash = GetHash(fpath);

                                    System.IO.FileInfo fi = null; //calc file size
                                    try { fi = new System.IO.FileInfo(fpath); }
                                    catch (Exception ex)
                                    {
                                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                        ErrorWindow frm3 = new ErrorWindow("Error at file opening for Pack_AuditTrail", "", "", false, false, true, "", "", "");
                                    }

                                    insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Official, Reason, Pack";
                                    var fnnon = Path.GetFileName(fpath);
                                    insertA = "Select top 1 i.FullPath, \"" + old_Path_Import + "\", i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, "
                                        + i_status + " as CDLC_ID, \"" + CDLC_Name + "\" as DLC_Name, \"" + fpath.GetPlatform().platform.ToString() + "\" as Platform,\""
                                        + Is_Original + "\" as Official,\"" + dupli_assesment_reason + "\" as Reason, i.Pack FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE i.ID IN ("
                                        + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ", \"" + ds.Tables[0].Rows[i].ItemArray[6].ToString() + "\")";
                                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);

                                    if (GetParam(75, chbx_Additional_Manipulations))
                                    {
                                        insertA = insertA.Substring(0, insertA.Length - ds.Tables[0].Rows[i].ItemArray[6].ToString().Length + 1) + pack + "\")";
                                        insertA = insertA.Replace(old_Path_Import, txt_RocksmithDLCPath.Text);
                                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);
                                    }
                                    tst = "end _AuditTrailing..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }
                            }
                        }
                        tst = "END importing song..." + (songt - DateTime.Now); timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }
            var endI = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;
            timestamp = UpdateLog(timestamp, "Ended import" + endI, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (GetParam(78, chbx_Additional_Manipulations))
            {
                MainDB.FixAudioAll_Click(netstatus, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "SELECT * FROM Main ", "penisuri", "DLCManager", timestamp, cnc);
            }

            //Cleanup
            if (GetParam(24, chbx_Additional_Manipulations)) //25. Use translation tables for naming standardization
            {
                tst = "Applying Standardizations";/*(chbx_DefaultDB.Checked == true ? MyAppWD : */
                GenericFunctions.Translation_And_Correction(txt_DBFolder.Text, pB_ReadDLCs, cnb, rtxt_StatisticsOnReadDLCs, cnc);
                tst = "end Standardization applying..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            tst = "DELETE * FROM Pack_AuditTrail WHERE id NOT IN( SELECT min(ID) FROM (SELECT ID, CopyPath,PackPath, FileHash FROM Pack_AuditTrail) GROUP BY CopyPath,PackPath, FileHash);";

            if (chbx_CleanTemp.Checked) chbx_CleanTemp.Checked = false;
            SetImportNo();

            //Show Intro database window
            //cnb.Close();
            MainDB frm = new MainDB(cnb, cnc, true);
            if (GetParam(77, chbx_Additional_Manipulations)) frm.Show();
            var endT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            if (GetParam(42, chbx_Additional_Manipulations)) //43. Save import Log
            {
                // Write the string to a file.
                var fn = (log_Path == null || log_Path == "" ? Log_PSPath : log_Path) + "\\" + GetTimestamps(DateTime.Now).Replace(":", "_") + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, true))
                {
                    file.WriteLine("Full Log");
                    file.WriteLine(rtxt_StatisticsOnReadDLCs.Text);
                    file.Close();
                }

                timestamp = UpdateLog(timestamp, "Log saved", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            SaveOK = "OK"; SaveSettings(); SaveOK = "";

            BackupDB(true);

            //get no of actually imported songs
            //START WITH mAINdb UPDATE
            cmd = "SELECT ID FROM Main as i";
            cmd += " WHERE Pack = \"" + ImportPackNo + "\";";


            DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, txt_DBFolder.Text, cnb, cnc);
            noOfRec = GetNoRec(drs, cnb, cnc); //drs.Tables.Count > 0 ? drs.Tables[0].Rows.Count : 0;

            //Show Summary window
            string summary = "Import Summary: \n\nFile in Import folders\\Actually Imported: " + totalFiles.ToString() + "\\" + noOfRec +
                "\n\nWeirdfiles (300k, not proper name syntax e.g. (copy 1-x) etc.): " + Weridfiles.ToString() + WeridfilesName + "\n" +
                (Weridfiles > 0 ? "\n" : "") + "Duplicate in the to be imported files, hash based: " + importhashdupli.ToString() + "\n" + importhashduplinames +
                (importhashdupli > 0 ? "\n" : "") + "Duplicate of Existing already imported DLC, hash based: " + importhashdupli2.ToString() + "\n" + importhashdupli2names +
                //"\nDuplicate hash based: " + importhashdupli3.ToString() +
                (importhashdupli2 > 0 ? "\n" : "") + "Invalids as determined by DLCManager: " + invalids.ToString() + "\n" + invalidsnames +
                //"\nInvalids : " + invalids2.ToString() +
                (invalids > 0 ? "\n" : "") + "With issues at decompresion: " + invalids3.ToString() + "\n" + invalids3names +
                (invalids3 > 0 ? "\n" : "") + "Duplicate manually managed: " + manualdec.ToString() + "\n" + manualdecnames +
                (manualdec > 0 ? "\n" : "") + "\nDuplicates automatically managed: " + automdec.ToString() + "\n" + automdecnames +
                "\n\nList of inititally marked as to be imported files:\n" + viles;
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Import of NEW songs Summary", false, false, true, "", "", "");
            frm9.Show();

            string endtmp = (starttmp - DateTime.Now).ToString();
            timestamp = UpdateLog(timestamp, "\n" + summary + "\nThe End " + endT + " (" + startT + ") after " + endtmp, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }


        string Processing(int i, int j, string tst, string FullPath, string DB_Path, bool errr, string broken_Path_Import, DataSet ds,
              string Temp_Path_Import, string dupli_Path_Import, string old_Path_Import, string cmd, string unpackedDir, int packid, bool Rebuild, string bbbroken
            , ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, string duplicstat, string dataPath, string filehash, string pack)

        {
            timestamp = UpdateLog(timestamp, " Start btn_PopulateDB_Click", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var platform = FullPath.GetPlatform();
            var platformTXT = FullPath.GetPlatform().platform.ToString();
            var Available_Duplicate = "No";
            var Available_Old = "No";
            var CDLC_ID = "";
            var DD = "No";
            var Bass_Has_DD = "No";
            var Has_Capo = "";
            var Has_JVocals = "No";
            var Has_Showlights = "No";
            var sect1on = "No";
            var dupli_assesment = "Insert";
            var dupli_assesment_reason = "new";
            bool stopp = false;
            infoorig = null;
            DLCPackageData info = null;
            bitrateorig = 0;
            SampleRateorig = 0;
            PreviewLenghtorig = "";
            var IDD = "";


            if (!errr)
            {
                //FIX for adding preview_preview_preview
                if (unpackedDir == "")
                {
                    unpackedDir = "C:\\GitHub\\tmp\\0\\dlcpacks\\songs_Pc";
                    timestamp = UpdateLog(timestamp, "Issues at decompressing WEMs or FAILED2 empty path", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }

                // LOAD DATA
                try
                {
                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                }
                catch (Exception ee)
                {
                    timestamp = UpdateLog(timestamp, "Error" + ee.Message + " Broken Song Not Imported", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (GetParam(30, chbx_Additional_Manipulations))
                        CopyMoveFileSafely(FullPath, Pathh, GetParam(75, chbx_Additional_Manipulations), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                    return "0";
                }
                infoorig = info;

                //if (info.SongInfo.Artist.Contains("Kanye"))
                //{/* || info.SongInfo.Artist.Contains("Rammstein")*/
                //    tst = "";
                //    SampleRateorig = 0;
                //}
                var oldArtistN = info.SongInfo.Artist;
                var oldSongN = info.SongInfo.SongDisplayName;
                var oldAlbumN = info.SongInfo.Album;
                var oldYearN = info.SongInfo.SongYear;

                tst = "end Loading song from folder..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //Enable input of
                info.SongInfo.Artist = CleanTitle(info.SongInfo.Artist);
                info.SongInfo.SongDisplayName = CleanTitle(info.SongInfo.SongDisplayName);
                info.SongInfo.ArtistSort = CleanTitle(info.SongInfo.ArtistSort);
                info.SongInfo.SongDisplayNameSort = CleanTitle(info.SongInfo.SongDisplayNameSort);
                info.SongInfo.Album = CleanTitle(info.SongInfo.Album);
                string ff = info.SongInfo.Artist, gg = info.SongInfo.ArtistSort, hhh = info.SongInfo.SongDisplayName, jj = info.SongInfo.SongDisplayNameSort, kk = info.SongInfo.Album;
                if (GetParam(35, chbx_Additional_Manipulations)) //36.
                {
                    //Remove weird/illegal characters
                    info.SongInfo.Artist = info.SongInfo.Artist.Trim().TrimEnd();
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\\", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "'");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("/", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("?", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace(":", "");
                    //info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "'");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace(";", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("  ", "");
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\u009c", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Trim().TrimEnd();
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\\", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "'");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("/", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("?", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(":", "");
                    //info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "'");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(";", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\u009c", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Trim().TrimEnd();
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\\", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "'");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("/", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("?", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(":", "");
                    //info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "'");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(";", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("  ", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\u009c", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Trim().TrimEnd();
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\\", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("/", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "'");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("?", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(":", "");
                    //info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "'");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(";", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("  ", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\u009c", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\\", "");
                    //info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "");
                    info.SongInfo.Album = info.SongInfo.Album.Trim().TrimEnd();
                    info.SongInfo.Album = info.SongInfo.Album.Replace("/", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("?", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace(":", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace(";", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "'");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("  ", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("\u009c", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Trim().TrimEnd();
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("/", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("?", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace(":", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace(";", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("\"", "'");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("  ", "");
                    info.SongInfo.AlbumSort = info.SongInfo.Album.Replace("\u009c", "");

                    //info.AlbumArtPath = info.SongInfo.Album.Replace("/", "");
                    if (ff != info.SongInfo.Artist) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Artist...", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (gg != info.SongInfo.ArtistSort) timestamp = UpdateLog(timestamp, "removed potential illegally characters \\,\",/,?,: from ArtistSort...", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (hhh != info.SongInfo.SongDisplayName) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Title...", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (jj != info.SongInfo.SongDisplayNameSort) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from TitleSort...", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (kk != info.SongInfo.Album) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Album...", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                if (GetParam(16, chbx_Additional_Manipulations)) //17.Import with Artist/ Title same as Artist / Title Sort
                {
                    info.SongInfo.ArtistSort = info.SongInfo.Artist;
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                    info.SongInfo.AlbumSort = info.SongInfo.Album;
                }

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul19"] == "Yes") //21.Pack with The/ Die only at the end of Title Sort 
                {
                    if (info.SongInfo.SongDisplayNameSort.ToInt32() > 4 && info.SongInfo.SongDisplayNameSort.ToLower().IndexOf("the") == 0 && info.SongInfo.SongDisplayNameSort.ToLower().IndexOf("die") == 0)
                        info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 3);
                    if (info.SongInfo.ArtistSort.ToInt32() > 4 && info.SongInfo.ArtistSort.ToLower().IndexOf("the") == 0 && info.SongInfo.ArtistSort.ToLower().IndexOf("die") == 0)
                        info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 3);
                }

                if (GetParam(22, chbx_Additional_Manipulations)) //23. Import with the The/Die only at the end of Title/Artist/Album Sort
                {
                    if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = MoveTheAtEnd(info.SongInfo.SongDisplayNameSort);
                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = MoveTheAtEnd(info.SongInfo.ArtistSort);
                    if (info.SongInfo.AlbumSort.Length > 4) info.SongInfo.AlbumSort = MoveTheAtEnd(info.SongInfo.AlbumSort);
                }
                if (GetParam(20, chbx_Additional_Manipulations)) //23. Import with the The/Die only at the end of Title/Artist/Album   
                {
                    if (info.SongInfo.SongDisplayName.Length > 4) info.SongInfo.SongDisplayName = MoveTheAtEnd(info.SongInfo.SongDisplayName);
                    if (info.SongInfo.Artist.Length > 4) info.SongInfo.Artist = MoveTheAtEnd(info.SongInfo.Artist);
                    if (info.SongInfo.Album.Length > 4) info.SongInfo.Album = MoveTheAtEnd(info.SongInfo.Album);
                }
                tst = "end text cleanup and standardization";
                timestamp = UpdateLog(timestamp, "Song " + (i + 1) + ": " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (info.SongInfo.Artist.ToLower().Contains("fontain") || info.SongInfo.SongDisplayName.ToLower().Contains("sad"))
                    tst = "-";
                timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                pB_ReadDLCs.Increment(1);

                //REUSING CODE IN IMPORTING RETAILs
                //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                var xmlFiles = Directory.GetFiles(unpackedDir + (platformTXT == "XBox360" ? "\\Root" : "") + "\\songs", "*.xml", System.IO.SearchOption.AllDirectories);
                List<string> clist = new List<string>();//LastConversionDateTime
                List<string> dlist = new List<string>();//Sections
                List<string> elist = new List<string>(); //Part flag
                //List<string> hlist = new List<string>();
                string PArt = "0"; var LastConversionDateTime = "";
                string MaxDD = "0"; var k = -1;
                DateTime myOldDate;
                DateTime myNewDate;
                string datemax = "12-13-11 13:11";
                string dateold = "12-13-11 13:11";
                CultureInfo enUS = new CultureInfo("en-US");

                myNewDate = DateTime.ParseExact(datemax, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);

                //foreach (var xml in xmlFiles)
                //{
                // READ ARRANGEMENTS
                var Lead = "No";
                var Bass = "No";
                var Vocalss = "No";
                var Guitar = "No";
                var Rhythm = "No";
                var Combo = "No";
                var PluckedType = "";
                var Tunings = "";
                var PitchShiftableEsOrDd = "";
                //var Import_AuditTrail_ID = "";
                var bonus = "No";
                List<string> xmlhlist = new List<string>();
                List<string> jsonhlist = new List<string>();
                List<string> cxmlhlist = new List<string>();//cleaned XMLhash
                List<string> snghlist = new List<string>();//JSON/Tone hash
                var SongLenght = "";
                var maxarnglenght = 0;
                foreach (var arg in info.Arrangements)
                {
                    var xml = arg.SongXml.File;
                    k++;

                    clist.Add("");
                    dlist.Add("");
                    elist.Add("");
                    //hlist.Add("");                   

                    var s1 = xml;
                    xmlhlist.Add(GetHash(xml));
                    cxmlhlist.Add(GetHashCleanXML(xml));

                    if (GetParam(36, chbx_Additional_Manipulations) && xml != "") //37. Keep the Uncompressed Songs superorganized                                
                        s1 = (xml.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                    else if (xml != "")
                        s1 = xml.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                    jsonhlist.Add(GetHash(s1));

                    if (GetParam(36, chbx_Additional_Manipulations) && xml != "") //37. Keep the Uncompressed Songs superorganized                                
                        s1 = (xml.Replace(".xml", ".sng").Replace("\\EOF\\", "\\Toolkit\\"));
                    else if (xml != "")
                        s1 = arg.SongXml.File.Replace(".xml", ".sng").Replace("\\songs\\arr", "\\" + calc_path_sng(Directory.GetFiles(unpackedDir, "*.sng", System.IO.SearchOption.AllDirectories)[0]));
                    snghlist.Add(GetHash(s1));

                    // 
                    //if ()
                    //{
                    //    dlist[k] = "No"; continue;
                    //}
                    if (arg.BonusArr) bonus = "Yes";
                    if (arg.ArrangementType == ArrangementType.Vocal) Vocalss = "Yes";
                    if (arg.ArrangementName.ToString() == "6" || arg.ArrangementName.ToString() == "ShowLights") Has_Showlights = "Yes";
                    if (arg.ArrangementType == ArrangementType.Vocal || arg.ArrangementType == ArrangementType.ShowLight)
                    { dlist[k] = ""; clist[k] = ""; continue; }
                    platform.version = RocksmithToolkitLib.GameVersion.RS2014;

                    clist[k] = GetLastConversionDateTime(xml, timestamp, broken_Path_Import, ds.Tables[0].Rows[i].ItemArray[2].ToString()
                        , FullPath, ds.Tables[0].Rows[i].ItemArray[3].ToString(), unpackedDir);

                    if (clist[k].Length > 3)
                        if (DateTime.ParseExact(clist[k], "MM-dd-yy HH:mm", enUS) > DateTime.ParseExact(datemax, "MM-dd-yy HH:mm", enUS))
                            datemax = clist[k];

                    //if (arg.Sng2014.Sections.Count < 2) sect1on = "No";
                    //else sect1on = "Yes" + arg.Sng2014.Sections.Count.ToString();
                    //if (arg.Sng2014 !=null)/* (arg.Sng2014.Sections.Count > 0 ? "Yes" + arg.Sng2014.Sections.Count : "No");*/
                    dlist[k] = GetSections(xml);
                    elist[k] = GetPart(xml);
                    //}
                    //else
                    //{
                    if (clist[k] == "")
                    {
                        timestamp = UpdateLog(timestamp, "no section/lastconvdate", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //dlist[k] = "No";
                    }
                    //clist[k] = ""; ;
                    //}
                    //var lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File).Trim(' '); ;
                    ////var lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile).Trim(' '); ;
                    //if (lastConverjsonDateTime_cur.Length > 3)
                    //{
                    //    if (lastConverjsonDateTime_cur.IndexOf("-") == 1) lastConverjsonDateTime_cur = "0" + lastConverjsonDateTime_cur;
                    //    if (lastConverjsonDateTime_cur.IndexOf("-", 3) == 4) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 3) + "0" + lastConverjsonDateTime_cur.Substring(3, ((lastConverjsonDateTime_cur.Length) - 3));
                    //    if (lastConverjsonDateTime_cur.IndexOf(":") == 10) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 9) + "0" + lastConverjsonDateTime_cur.Substring(9, lastConverjsonDateTime_cur.Length - 9);
                    //}
                    //hlist[k] = lastConverjsonDateTime_cur;

                    //MAximum difficulty/dinamic Difficulty level
                    MaxDD = GetMaxDifficulty(xml, timestamp, broken_Path_Import, ds.Tables[0].Rows[i].ItemArray[2].ToString()
                        , FullPath, ds.Tables[0].Rows[i].ItemArray[3].ToString(), unpackedDir);
                    if (MaxDD.ToInt32() >= 1) DD = "Yes"; //1 should still be multiGTet

                    //Bass_Has_DD
                    if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && MaxDD.ToInt32() >= 1) Bass_Has_DD = "Yes";
                    if (arg.CapoFret != null && arg.CapoFret.ToString() != "" && arg.CapoFret > 0) Has_Capo = "Yes";
                    //if (arg.TuningStrings. != null && arg.CapoFret.ToString() != "" && arg.CapoFret > 0) Has_Capo = "Yes";
                    if (arg.ArrangementName.ToString() == "5" || arg.ArrangementName.ToString() == "JVocals") Has_JVocals = "Yes";
                    //var manifestFunctions1 = new ManifestFunctions(platform.version);
                    //xmlContent = null;
                    //try
                    //{
                    //    xmlContent = Song2014.LoadFromFile(xml);
                    //    if (xmlContent.Arrangement.ToLower() == "bass")
                    //    {
                    //        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                    //        if (manifestFunctions1.GetMaxDifficulty(xmlContent) >= 1)
                    //            Bass_Has_DD = "Yes";
                    //    }
                    //}
                    //catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                    //}
                    // tst = "end xml readout..."+arg.ArrangementType; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    if (arg.SongXml.File == null || arg.SongXml.File == "") if (arg.SongXml.File.Length > maxarnglenght) maxarnglenght = arg.SongXml.File.Length; //Calculating the longest filepath(usualy  

                    if (arg.ArrangementType == ArrangementType.Guitar)
                    {
                        Guitar = "Yes";
                        if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                        else Tunings = arg.Tuning;
                        if (arg.Tuning == "Eb" || arg.Tuning == "Eb Standard" || arg.Tuning == "D Standard" || arg.Tuning == "C# Standard" || arg.Tuning == "C Standard"
                            || arg.Tuning == "B Standard" || arg.Tuning == "Bb Standard" || arg.Tuning == "AStandard" || arg.Tuning == "AbStandard"
                            || arg.Tuning == "Eb Drop Db" || arg.Tuning == "D Drop C" || arg.Tuning == "C#DropB" || arg.Tuning == "C Drop A#" || arg.Tuning == "B Drop A"
                            || arg.Tuning == "BbDropAb" || arg.Tuning == "A Drop G") PitchShiftableEsOrDd = "Yes";

                        if (arg.ArrangementName == ArrangementName.Lead) Lead = "Yes";
                        else if (arg.ArrangementName == ArrangementName.Rhythm) Rhythm = "Yes";
                        else if (arg.ArrangementName == ArrangementName.Combo) Combo = "Yes";
                        Song2014 xmlContent = null;
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(arg.SongXml.File);
                            SongLenght = xmlContent.SongLength.ToString();
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                        catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                    }
                    tst = "end xml readout..." + arg.ArrangementType; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                //Check Tones
                var Tones_Custom = "No";
                foreach (var tn in info.TonesRS2014)
                {
                    if (tn.IsCustom)
                        Tones_Custom = "Yes";
                }
                tst = "end Arrangements and Tones readout..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


                var alt = "";
                var trackno = -1;
                var SpotifySongID = "";
                var SpotifyArtistID = "";
                var SpotifyAlbumID = "";
                var SpotifyAlbumURL = "";
                var SpotifyAlbumPath = "";
                var SpotifyAlbumYear = "";
                var ybRAddress = "";
                var ybAddress = "";
                var ybLAddress = "";
                var ybBAddress = "";
                var ybCAddress = "";
                var ybSAddress = "";

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
                if (GetParam(57, chbx_Additional_Manipulations))
                {
                    if (author == "Custom Song Creator") Has_author = "No";
                    author = author.Replace("Custom Song Creator", "").Trim();
                }
                else if (GetParam(47, chbx_Additional_Manipulations))
                {
                    if (author == "") Has_author = "No";
                    author = "Custom Song Creator";
                }

                if (versionFile.Length <= 0) Is_Original = "Yes";
                else Is_Original = "No";

                //Get Version from FileName
                var import_path = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                var original_FileName = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                string txt = original_FileName;

                int vpos1 = (txt.IndexOf("_v")) + 2;
                int vpos2 = (txt.IndexOf("_v_")) + 3;
                int vpos = vpos2 > 5 ? vpos2 : vpos1;
                string major = "";
                string minor = "";

                if (info.ToolkitInfo.PackageVersion != "" && (info.ToolkitInfo.PackageVersion != null))
                    if (info.ToolkitInfo.PackageVersion.Length > 2)
                        if (info.ToolkitInfo.PackageVersion.Substring(info.ToolkitInfo.PackageVersion.Length - 2, 2) == ".0")
                            info.ToolkitInfo.PackageVersion = info.ToolkitInfo.PackageVersion.Substring(0, info.ToolkitInfo.PackageVersion.Length - 2);

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
                    try
                    {
                        if (info.ToolkitInfo.PackageVersion != null) { if (Convert.ToSingle(info.ToolkitInfo.PackageVersion.Replace("_", ".").Replace(".9.", ".9").Replace(".0.", ".0").Replace(".1.", ".1").Replace(".2.", ".2").Replace(".3.", ".3").Replace(".4.", ".4").Replace(".5.", ".5").Replace(".6.", ".6").Replace(".7.", ".7").Replace(".8.", ".8")) < Convert.ToSingle(ver)) info.ToolkitInfo.PackageVersion = ver; }
                        else info.ToolkitInfo.PackageVersion = ver;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                tst = "end song details readout like version author and tk versioning..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //Set appID
                if (GetParam(43, chbx_Additional_Manipulations)) AppIdD = ConfigRepository.Instance()["general_defaultappid_RS2014"];
                else AppIdD = info.AppId;

                //Set MultiTrack based on FileName                                
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
                IsLive = ""; LiveDetails = ""; IsAcoustic = ""; IsSingle = ""; IsSoundtrack = "";
                IsInstrumental = ""; IsEP = ""; IsUncensored = ""; IsFullAlbum = ""; IsRemastered = ""; InTheWorks = "";
                IsKaraoke = ""; IsDemo = ""; HasFeaturing = ""; IsRemix = ""; IsCover = ""; IsMedley = ""; IsMultiStrings = "";
                BasedOn_Youtube = ""; BasedOn_CF = ""; BasedOn_Tabs = ""; ToDos = ""; ToneDetails = ""; PackageDetails = "";

                //Get Packaging Details
                ////var g=GetPackagingDetails();
                //string[] ggg = info.ToolkitInfo.PackageComment.ToString().Split(';');
                //BasedOn_Youtube = ggg[0]; BasedOn_CF = ggg[1]; BasedOn_Tabs = ggg[2]; ToDos = ggg[3]; ToneDetails = ggg[4]; PackageDetails = ggg[5];

                var ret = GetExtraAttributes(origFN, noMFN, gom, info.SongInfo.SongDisplayName, info.SongInfo.Album, info.ToolkitInfo.PackageAuthor, info.Name, info.SongInfo.Artist);
                string[] ag = ret.ToString().Split(';');
                //return Is_MultiTrack + ";" + MultiTrack_Version + ";" + IsLive + ";" + LiveDetails + ";" + IsAcoustic + ";" + IsSingle + ";" + IsSoundtrack + ";" + IsInstrumental + ";" + IsEP + ";" + IsUncensored + ";" + IsFullAlbum + ";"
                //    + IsRemastered + ";" + InTheWorks + ";" + IsKaraoke + ";" + IsDemo + ";" + HasFeaturing + ";" + IsRemix + ";" + IsCover + ";" + SongDisplayName + ";" + Album + ";" + IsMedley + ";" + IsMultiStrings;
                Is_MultiTrack = ag[0]; MultiTrack_Version = ag[1]; IsLive = ag[2]; LiveDetails = ag[3]; IsAcoustic = ag[4]; IsSingle = ag[5]; IsSoundtrack = ag[6]; IsInstrumental = ag[7]; IsEP = ag[8];
                IsUncensored = ag[9]; IsFullAlbum = ag[10]; IsRemastered = ag[11]; InTheWorks = ag[12]; IsKaraoke = ag[13]; IsDemo = ag[14]; HasFeaturing = ag[15]; IsRemix = ag[16]; IsCover = ag[17];

                if (info.SongInfo.SongDisplayName != ag[18] || info.SongInfo.Album != ag[19])
                { dupliSongs[0, i] = "1"; duplit = true; dupli_assesment_reason = "not a duplicate but with Title/AlbumName cleansed, and to be scheduled for assesment end of Import"; }
                info.SongInfo.SongDisplayName = ag[18]; info.SongInfo.Album = ag[19];

                IsMedley = ag[20]; IsMultiStrings = ag[21]; IsDeluxe = ag[22]; IsGreatestHits = ag[23]; IsMidi = ag[24]; IsGameSoundtrack = ag[25]; IsTVTheme = ag[26]; IsAmateurCover = ag[27];
                //Remove Live/Others Info from Title
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul104"] == "Yes") info.SongInfo.SongDisplayName = gom.TrimEnd().Replace("()", "").TrimStart(); //(Regex.Replace(noMFN, "( audio)", "", RegexOptions.IgnoreCase)).TrimEnd().TrimStart().Replace(" ()", "");
                else
                {
                    //info.SongInfo.SongDisplayName = ReplaceTxt(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(
                    // info.SongInfo.SongDisplayName, "(No.)", "[No.]"), "(Backing.)", "[Backing.]"), "(Only.)", "[Only.]"),
                    // "(Live.)", "[Live.]"), "(Acoustic.)", "[Acoustic.]"), "Unplugged", "[Unplugged.]"), "Uncensored", "[Uncensored.]"), "[Instrumental.]", "Instrumental."), "(Single.)", "[Single.]")
                    // , "(Remastered.)", "[Remastered.]"), "(FullAlbum.)", "[FullAlbum.]"), "(Full Album.)", "[Full Album.]"), info.SongInfo.SongDisplayName, ConfigRepository.Instance()["dlcm_AdditionalManipul103"] == "Yes" ? true : false);
                    info.SongInfo.SongDisplayName = ReplaceTxt(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(
                         info.SongInfo.SongDisplayName, "(No.)", "[No.]"), "(Backing.)", "[Backing.]"), "(Only.)", "[Only.]"),
                         "(Live.)", "[Live.]"), "(Acoustic.)", "[Acoustic.]"), "Unplugged", "[Unplugged.]"), "Uncensored", "[Uncensored.]"), "[Instrumental.]", "Instrumental."), "(Single.)", "[Single.]")
                         , "(Remastered.)", "[Remastered.]"), "(FullAlbum.)", "[FullAlbum.]"), "(Full Album.)", "[Full Album.]"), "(Demo.)", "[Demo.]")
                         , "(Remix.)", "[Remix.]"), "(Karaoke.)", "[Karaoke.]"), "(Featuring.)", "[Featuring.]"), "(Cover.)", "[Cover.]"), "(Feat.)", "[Feat.]"), "(Medley)", "[Medley.]"), info.SongInfo.SongDisplayName, ConfigRepository.Instance()["dlcm_AdditionalManipul103"] == "Yes" ? true : false, "SongDisplayName");
                }

                tst = "end multitrackcheckin..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                info.SongInfo.SongDisplayName = CleanTitleOfWeirdChars(info.SongInfo.SongDisplayName);
                info.SongInfo.Artist = CleanTitleOfWeirdChars(info.SongInfo.Artist);
                info.SongInfo.Album = CleanTitleOfWeirdChars(info.SongInfo.Album);

                if (GetParam(16, chbx_Additional_Manipulations)) //17.Import with  Artist / Title / album Sort same as Artist/ Title / Album
                {
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                    info.SongInfo.ArtistSort = info.SongInfo.Artist;
                    info.SongInfo.AlbumSort = info.SongInfo.Album;
                }

                //Get TrackNo
                trackno = 0;

                if (netstatus == "OK")
                {
                    netstatus = CheckIfConnectedToSpotify().Result.ToString();
                    if (netstatus == "OK" && GetParam(41, chbx_Additional_Manipulations))
                    {
                        tst = "startgetting cover spotify..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                        trackno = sptyfy.Result.Split(';')[0].ToInt32();
                        SpotifySongID = sptyfy.Result.Split(';')[1];
                        SpotifyArtistID = sptyfy.Result.Split(';')[2];
                        SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                        SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                        SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                        SpotifyAlbumYear = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";
                        tst = "end get track no from spotify..." + SpotifyAlbumPath; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    string yAddress = null;
                    try
                    {
                        MainDBfields SongRecord = new MainDBfields
                        {
                            Album = info.SongInfo.Album
                        };
                        ; SongRecord.Artist = info.SongInfo.Artist; SongRecord.Song_Title = info.SongInfo.SongDisplayName;
                        SongRecord.Has_Lead = (Lead != "" ? Lead : "No"); SongRecord.Has_Rhythm = (Rhythm != "" ? Rhythm : "No"); SongRecord.Has_Bass = Bass;
                        SongRecord.Has_Combo = (Combo != "" ? Combo : "No");
                        if (yAddress != null)
                        {
                            ybAddress = yAddress.Split(';')[0];
                            ybLAddress = yAddress.Split(';')[1];
                            ybBAddress = yAddress.Split(';')[2];
                            ybRAddress = yAddress.Split(';')[3];
                            ybCAddress = yAddress.Split(';')[4];
                            ybSAddress = yAddress.Split(';')[5];
                        }
                    }
                    catch (AggregateException ex)
                    {
                        foreach (var e in ex.InnerExceptions)
                            tst = "error get track no from spotify..." + e.Message; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                }
                ExistingTrackNo = "";

                //Generating the HASH code
                art_hash = "";
                string audio_hash = "";
                string audio_changed = "";
                string audioPreview_hash = "";
                AlbumArtPath = info.AlbumArtPath;
                string ss = "";

                if (AlbumArtPath != "" && AlbumArtPath != null)
                {
                    art_hash = GetHash(AlbumArtPath);
                    tst = "trying to convert DDS2PNG"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //convert to png
                    ExternalApps.Dds2Png(AlbumArtPath);
                }
                else
                {
                    var dds = Directory.GetFiles(unpackedDir + (platformTXT == "XBox360" ? "\\Root" : "") + "\\gfxassets\\album_art\\", string.Format("*{0}.dds", Path.GetFileNameWithoutExtension(unpackedDir + "\\gfxassets\\album_art\\")), System.IO.SearchOption.AllDirectories);
                    if (dds.Length > 0)//&& g==1
                    {
                        foreach (var fl in dds)
                        {
                            if (fl.IndexOf("256") > 0)
                            {
                                info.AlbumArtPath = fl;
                                AlbumArtPath = info.AlbumArtPath;
                                art_hash = GetHash(AlbumArtPath);
                                //convert to png
                                ExternalApps.Dds2Png(AlbumArtPath);
                            }
                        }
                    }
                }
                ss = info.OggPath;
                audio_hash = GetHash(ss);

                ss = info.OggPreviewPath;
                audioPreview_hash = GetHash(ss);
                tst = "end gen hashcodes..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //normalise the artist and album and year
                string returned = GenericFunctions.GetTranslation_And_Correction(txt, pB_ReadDLCs, cnb, null, info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongYear.ToString(), cnc);//, info.AlbumArtPath, info.SongInfo.Artist, info.SongInfo.Album);

                string[] args = returned.ToString().Split(';');
                info.SongInfo.Artist = args[0] != "" ? args[0] : info.SongInfo.Artist;
                info.SongInfo.Album = args[1] != "" ? args[1] : info.SongInfo.Album;
                info.SongInfo.SongYear = args[2] != "" ? int.Parse(args[2]) : info.SongInfo.SongYear;

                //Check if CDLC have already been imported (hash key)
                var sel = "SELECT * FROM Main WHERE (LCASE(IIF(INSTR(Artist,\"[\")>0, MID(Artist,1,INSTR(Artist,\"[\")-1), Artist))=LCASE(\"" + CleanTitle(info.SongInfo.Artist) + "\") AND ";
                sel += "(LCASE(IIF(INSTR(Song_Title,\"[\")>0, MID(Song_Title,1,INSTR(Song_Title,\"[\")-1), Song_Title)) = LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayName) + "\") ";
                sel += "OR LCASE(IIF(INSTR(Song_Title,\"[\")>0, MID(Song_Title,1,INSTR(Song_Title,\"[\")-1), Song_Title)) like \"%" + CleanTitle(info.SongInfo.SongDisplayName.ToLower()) + "%\" ";
                sel += "OR LCASE(IIF(INSTR(Song_Title_Sort,\"[\")>0, MID(Song_Title_Sort,1,INSTR(Song_Title_Sort,\"[\")-1), Song_Title_Sort)) like \"%" + CleanTitle(info.SongInfo.SongDisplayNameSort.ToLower()) + "%\")) OR LCASE(DLC_Name) like \"%" + CleanTitle(info.Name.ToLower()) + "%\" ";
                sel += "OR LCASE(IIF(INSTR(Original_FileName,\"[\")>0, MID(Original_FileName,1,INSTR(Original_FileName,\"[\")-1), Original_FileName)) = LCASE(\"" + CleanTitle(original_FileName) + "\")";

                //Read from DB
                SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);
                var norows = SongRecord[0].NoRec.ToInt32();

                var selduo = "SELECT * FROM Main WHERE LCASE(IIF(INSTR(Artist,\"[\")>0, MID(Artist,1,INSTR(Artist,\"[\")-1), Artist))=LCASE(\"" + CleanTitle(info.SongInfo.Artist) + "\") AND ";
                selduo += "(LCASE(IIF(INSTR(Song_Title,\"[\")>0, MID(Song_Title,1,INSTR(Song_Title,\"[\")-1), Song_Title)) = LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayName) + "\") ";
                selduo += "OR LCASE(IIF(INSTR(Song_Title,\"[\")>0, MID(Song_Title,1,INSTR(Song_Title,\"[\")-1), Song_Title)) like \"%" + CleanTitle(info.SongInfo.SongDisplayName.ToLower()) + "%\" ";
                selduo += "OR LCASE(IIF(INSTR(Song_Title_Sort,\"[\")>0, MID(Song_Title_Sort,1,INSTR(Song_Title_Sort,\"[\")-1), Song_Title_Sort)) =LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayNameSort)
                    + "\")) OR LCASE(IIF(INSTR(DLC_Name,\"[\")>0, MID(DLC_Name,1,INSTR(DLC_Name,\"[\")-1), DLC_Name))=LCASE(\"" + CleanTitle(info.Name) + "\" AND Is_Original=\"Yes\")";
                UtilitiesFunctions.MainDBfields[] SongRecord2 = new UtilitiesFunctions.MainDBfields[20000];
                SongRecord2 = UtilitiesFunctions.GetRecord_s(selduo, cnb, cnc);
                var norowsduo = SongRecord2[0].NoRec.ToInt32();

                var b = -1;
                dupli_assesment = "";
                IDD = "";
                var folder_name = "";
                var DLCC = "";
                var Platformm = "";
                var filename = "";
                var oldfilehas = "";
                var bitrate = 0;
                var SampleRate = 0;
                var HasOrig = "";
                var Duplic = 0;
                bool newold = GetParam(32, chbx_Additional_Manipulations);
                Random random = new Random();
                DLCC = info.Name;

                //Sort by author, AudioHash, PreviewHash, CoverHash, LenghtHash
                int ord = 0;
                if (norows > 0) foreach (var fil in SongRecord)
                    {
                        b++; if (b >= norows) break; if (b < ord) continue;
                        if (fil.Author != "" && fil.Author != null && fil.Author == author)
                        {
                            var tmp = SongRecord[b]; SongRecord[b] = SongRecord[ord]; SongRecord[ord] = tmp; ord++;
                        }
                    }
                b = -1;
                if (norows > 0) foreach (var fil in SongRecord)
                    {
                        b++; if (b >= norows) break; if (b < ord) continue;
                        if (fil.AlbumArt_OrigHash != "" && fil.AlbumArt_OrigHash != null && fil.AlbumArt_OrigHash == art_hash)
                        {
                            var tmp = SongRecord[b]; SongRecord[b] = SongRecord[ord]; SongRecord[ord] = tmp; ord++;
                        }

                    }
                b = -1;
                if (norows > 0) foreach (var fil in SongRecord)
                    {
                        b++; if (b >= norows) break; if (b < ord) continue;
                        if (fil.Audio_OrigHash != "" && fil.Audio_OrigHash != null && fil.Audio_OrigHash == audio_hash)
                        {
                            var tmp = SongRecord[b]; SongRecord[b] = SongRecord[ord]; SongRecord[ord] = tmp; ord++;
                        }
                    }
                b = -1;
                if (norows > 0) foreach (var fil in SongRecord)
                    {
                        b++; if (b >= norows) break; if (b < ord) continue;
                        if (fil.Audio_OrigPreviewHash != "" && fil.Audio_OrigPreviewHash != null && fil.Audio_OrigPreviewHash == audioPreview_hash)
                        {
                            var tmp = SongRecord[b]; SongRecord[b] = SongRecord[ord]; SongRecord[ord] = tmp; ord++;
                        }
                    }
                b = -1;
                if (norows > 0) foreach (var fil in SongRecord)
                    {
                        b++; if (b >= norows) break; if (b < ord) continue;
                        if (fil.Song_Lenght != "" && fil.Song_Lenght != null && fil.Song_Lenght == SongLenght)
                        {
                            var tmp = SongRecord[b]; SongRecord[b] = SongRecord[ord]; SongRecord[ord] = tmp; ord++;
                        }
                    }

                b = 0;
                if (norows > 0)
                    foreach (var file in SongRecord)
                    {
                        SongDisplayName = "";
                        Namee = "";
                        // Description = "";
                        if (b >= norows) break;
                        folder_name = file.Folder_Name;
                        filename = original_FileName;// file.Current_FileName;
                        IDD = file.ID; //Save Id in case of update or asses-update
                        Platformm = (import_path + "\\" + original_FileName).GetPlatform().platform.ToString();

                        //calculate the alternative no (in case is needed)
                        var altver = "";
                        if (info.SongInfo.SongDisplayName.ToLower().IndexOf("lord") >= 0)
                            altver = "";
                        sel = "SELECT max(Alternate_Version_No) FROM Main WHERE (LCASE(REPLACE(REPLACE(Artist,\"[\",\"\"),\"]\",\"\"))) = LCASE(\"" + CleanTitle(info.SongInfo.Artist) + "\") AND ";// LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                        sel += "(LCASE(REPLACE(REPLACE(Song_Title,\"[\",\"\"),\"]\",\"\")) = LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayName) + "\") OR ";
                        sel += "LCASE(REPLACE(REPLACE(Song_Title,\"[\",\"\"),\"]\",\"\")) like \"%" + CleanTitle(info.SongInfo.SongDisplayName.ToLower()) + "%\" OR ";
                        sel += "LCASE(REPLACE(REPLACE(Song_Title_Sort,\"[\",\"\"),\"]\",\"\")) =LCASE(\"" + CleanTitle(info.SongInfo.SongDisplayNameSort) + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                        //Get last inserted ID
                        DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, txt_DBFolder.Text, cnb, cnc);

                        var altvert = GetNoRec(dds, cnb, cnc) > 0 ? (dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 1 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32()) : 0;
                        if (Is_Original == "No") altver = (altvert + 1).ToString();
                        //if (altvert == -1)
                        //    altvert = 1;
                        var fsz = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                        var hash = ds.Tables[0].Rows[i].ItemArray[3].ToString();
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
                        var dupli_reason = "";

                        //in case we have versions with 2 dots :) e.g. v3.4.5
                        string countdots = info.ToolkitInfo.PackageVersion == null ? "" : info.ToolkitInfo.PackageVersion.Replace("_", ".");
                        var versn = ""; var firstdot = 0;
                        for (int m = 0; m < countdots.Length; m++)
                        {
                            if (countdots[m] == '.') firstdot++;
                            else versn += countdots[m];
                            if (firstdot == 1) { versn += countdots[m]; firstdot++; }
                        }
                        var versio = versn == null || versn == "" ? 1 : float.Parse(versn, NumberStyles.Float, CultureInfo.CurrentCulture);//else dupli_reason += ". Possible Duplicate Import at end. End Else";

                        //listing all other duplicates for long lists of duplicates e.g. 20 mostly cause dlckey is a small common word
                        var AllOther = ""; var cnt = 0;
                        foreach (var fil in SongRecord)
                        {
                            cnt++;
                            if (fil.Original_FileName == "" || fil.Original_FileName is null) break;
                            AllOther += cnt + ". " + fil.Original_FileName + " - " + fil.Artist + " - " + fil.Album + " - " + fil.Song_Title + " - " + fil.Author
                                + " - " + fil.Version + "\n";
                        }

                        timestamp = UpdateLog(timestamp, dupli_reason, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        dupli_assesment = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead,
                           Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash,
                           xmlhlist, jsonhlist, DB_Path, clist, dlist, newold, Is_Original, altvert.ToString(), fsz, unpackedDir,
                           Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), duplicstat, Platformm, IsLive,
                           LiveDetails, IsAcoustic, HasOrig, dupli_reason, sel, Duplic, Rebuild, versio, norowsduo, hash, j,
                           SongLenght, AllOther, cxmlhlist, snghlist, filehash, IsUncensored, IsEP
                           , IsSingle, IsSoundtrack, IsFullAlbum, IsRemastered, IsInstrumental, InTheWorks, IsKaraoke, IsDemo
                           , HasFeaturing, IsRemix, IsCover, IsMedley, IsMultiStrings, Has_Capo);  //else if (dupli_assesment == "")                      

                        string[] retunc = dupli_assesment.Split(';');//Get Duplication assessment and its reason
                        dupli_assesment = retunc[0];
                        if (retunc.Length > 1) dupli_assesment_reason = retunc[1];

                        if (j == 0 && dupli_assesment == "") { dupliSongs[0, i] = "1"; duplit = true; dupliNo++; break; }
                        //Exit condition
                        tst = "end check for dupli..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (dupli_assesment == "Stop")
                        {
                            stopp = true;
                            break;
                        }

                        if (Namee != "") info.Name = Namee;
                        if (SongDisplayName != "") info.SongInfo.SongDisplayName = CleanTitleOfWeirdChars(SongDisplayName);
                        if (Title_Sort != "") info.SongInfo.SongDisplayNameSort = CleanTitleOfWeirdChars(Title_Sort);
                        if (ArtistSort != "") info.SongInfo.ArtistSort = CleanTitleOfWeirdChars(ArtistSort);
                        if (Artist != "") info.SongInfo.Artist = CleanTitleOfWeirdChars(Artist);
                        if (AlbumSort != "") info.SongInfo.AlbumSort = CleanTitleOfWeirdChars(AlbumSort);
                        if (AlbumYear != "") info.SongInfo.SongYear = AlbumYear.ToInt32();
                        if (Album != "") info.SongInfo.Album = CleanTitleOfWeirdChars(Album);
                        if (PackageVersion != "") info.ToolkitInfo.PackageVersion = PackageVersion;

                        if (dupli_assesment == "Alternate")
                        {
                            dupli_assesment = "Insert";
                            if (dupli_assesment_reason.IndexOf("manual decision: ") >= 0)
                            {
                                manualdec++; manualdecnames += "\n" + "Alternate: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                            }
                            else
                            {
                                automdec++; automdecnames += "\n" + "Alternate: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                            }

                            //Get the highest Alternate Number
                            if (dupli_assesment_reason != "notalt")
                            {
                                if (alt == "") alt = "1";
                                if (Is_Alternate != "" && Is_Original == "No") alt = Alternate_No;
                                if (Alternate_No != "" && Is_Original == "No") alt = Alternate_No;
                                //end?                            
                                if (file.DLC_Name.ToLower() == info.Name.ToLower()) info.Name = random.Next(0, 100000) + info.Name;
                                if (file.Song_Title.ToLower() == info.SongInfo.SongDisplayName.ToLower() && Is_Original == "No")
                                {
                                    info.SongInfo.SongDisplayName += " [a." + (MultiTrack_Version != "" ? MultiTrack_Version + "_" + altver : (altver + ((author == null || author == "" || author == "Custom Song Creator") ? "" : "_" + author))) + "]";// ;//random.Next(0, 100000).ToString()
                                    alt = altver;
                                }

                                if (Duplic == 0) Duplic = IDD.ToInt32();
                            }
                            else
                            { alt = ""; Duplic = 0; Is_Alternate = ""; }
                        }
                        if (GetParam(16, chbx_Additional_Manipulations)) //17.Import with Artist/ Title same as Artist / Title Sort
                        {
                            info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                            info.SongInfo.ArtistSort = info.SongInfo.Artist;
                            info.SongInfo.AlbumSort = info.SongInfo.Album;
                        }
                        b++;

                        oldfilehas = file.File_Hash;
                        if (b >= norows || dupli_assesment != "Insert" || IgnoreRest)
                        {
                            if (dupli_assesment == "Ignore")
                            {
                                if (dupli_assesment_reason.IndexOf("manual decision: ") >= 0)
                                {
                                    manualdec++; manualdecnames += "\n" + "Not imported: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                                }
                                else
                                {
                                    automdec++; automdecnames += "\n" + "Not imported: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                                }
                                string filePath = unpackedDir;
                                try
                                {
                                    DeleteDirectory(filePath, false);
                                }
                                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                            }
                            if (dupli_assesment == "Update")
                            {
                                if (dupli_assesment_reason.IndexOf("manual decision: ") >= 0)
                                {
                                    manualdec++; manualdecnames += "\n" + "Overrite: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                                }
                                else
                                {
                                    automdec++; automdecnames += "\n" + "Ignored: " + dupli_assesment_reason + "---" + original_FileName + "/" + file.Original_FileName;
                                }
                            }
                            break;
                        }
                    }
                else
                {
                    dupli_assesment = "Insert";
                    dupli_assesment_reason = "new";
                }

                //Doublechecking that no DLC Name is the same (last import 4500 songs generate 11 such exception :( )
                DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main WHERE DLC_Name='" + info.Name + "'", txt_DBFolder.Text, cnb, cnc);
                //if (dms.Tables.Count > 0) if (dms.Tables[0].Rows.Count > 0)
                if (GetNoRec(dms, cnb, cnc) > 0) info.Name = random.Next(0, 100000) + info.Name;

                if (duplit) return "0";
                else
                {
                    var insertA = "\"" + txt_RocksmithDLCPath.Text + "\", \"" + txt_RocksmithDLCPath.Text + "\", \"" + original_FileName + "\", \"" + ds.Tables[0].Rows[i].ItemArray[5] + "\", \""
                        + ds.Tables[0].Rows[i].ItemArray[3] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[4] + "\", " + IDD + ", \"" + DLCC + "\", \"" + Platformm + "\", \"" + Is_Original + "\", \"" + dupli_assesment_reason + "\"" + ", \"" + ds.Tables[0].Rows[i].ItemArray[6] + "\"";
                    var dfn = txt_RocksmithDLCPath.Text + "\\" + original_FileName;
                    //Move file New file to duplicates Ignore is select
                    if (dupli_assesment == "Ignore" && GetParam(29, chbx_Additional_Manipulations))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        Available_Duplicate = "Yes";
                        dfn = CopyMoveFileSafely(dfn, dupli_Path_Import + "\\" + original_FileName,
                            GetParam(75, chbx_Additional_Manipulations), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                        insertA = "\"" + txt_RocksmithDLCPath.Text + "\\" + original_FileName + "\", \"" + dupli_Path_Import + "\", \"" + Path.GetFileName(dfn) + "\", \""
                            + ds.Tables[0].Rows[i].ItemArray[5] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[3] + "\", \"" + ds.Tables[0].Rows[i].ItemArray[4]
                            + "\", " + IDD + ", \"" + DLCC + "\", \"" + Platformm + "\", \"" + Is_Original + "\", \"" + dupli_assesment_reason + "\"" + ", \"" + ds.Tables[0].Rows[i].ItemArray[6] + "\"";
                        //dELETE DUPLCAITION FODLER
                        if (DirectoryExists(unpackedDir)) DeleteDirectory(unpackedDir, false);
                    }

                    var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Official, Reason, Pack";

                    if (dupli_assesment == "Ignore")
                    {
                        //insertA = insertA.Substring(0, insertA.Length - ds.Tables[0].Rows[i].ItemArray[6].ToString().Length + 1) + pack + "\"";
                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);
                        string filePath = unpackedDir;
                        if (DirectoryExists(unpackedDir)) DeleteDirectory(filePath, false);
                        if (GetParam(75, chbx_Additional_Manipulations))
                        {
                            insertA = insertA.Replace(dupli_Path_Import, txt_RocksmithDLCPath.Text);
                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);
                        }
                    }

                    //Move file Original file to duplicates if Main DB record is being overwritten
                    if (dupli_assesment == "Update" && GetParam(29, chbx_Additional_Manipulations))//30. When NOT importing a duplicate Move it to _duplicate
                    {
                        DataSet dzr = new DataSet(); dzr = SelectFromDB("Main", "SELECT Original_FileName, Available_Old FROM Main WHERE ID=" + IDD + ";", txt_DBFolder.Text, cnb, cnc);
                        var Original_FileName = dzr.Tables[0].Rows[0].ItemArray[0].ToString();
                        Available_Duplicate = "Yes";
                        var fnn = Original_FileName;

                        CopyMoveFileSafely(txt_TempPath.Text + "\\0_old\\" + Original_FileName, txt_TempPath.Text + "\\0_duplicate\\" + fnn
                            , GetParam(75, chbx_Additional_Manipulations), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                        DeleteDirectory(folder_name, false);
                        if (File.Exists(txt_TempPath.Text + "\\0_old\\" + Original_FileName)) DeleteFile(txt_TempPath.Text + "\\0_old\\" + Original_FileName, false);

                        var cmdupd = "UPDATE Pack_AuditTrail Set FileName=\"" + fnn + "\", PackPath =REPLACE(PackPath,'\\0_old','\\0_duplicate') WHERE FileHash='" + oldfilehas + "' AND PackPath='" + txt_TempPath.Text + "\\0_old" + "'";
                        DataSet dus = new DataSet(); dus = UpdateDB("Pack_AuditTrail", cmdupd + ";", cnb, cnc);
                        if (GetParam(75, chbx_Additional_Manipulations))
                        {
                            insertA = insertA.Replace(dupli_Path_Import, txt_RocksmithDLCPath.Text);
                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, mutit, cnc);
                        }
                    }

                    //Clean Arrangements and Tones
                    if (dupli_assesment == "Update")
                    {
                        //Delete Arangements
                        DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE CDLC_ID IN (" + IDD + ")", cnb, cnc);
                        // //Delete Tones
                        DeleteFromDB("Tones", "DELETE * FROM Tones WHERE CDLC_ID IN (" + IDD + ")", cnb, cnc);
                        //DeleteFromDB("Tones_Gearlist", "DELETE * FROM Tones_Gearlist tg LEFT JOIN Tones ON tg.Tone_ID_ID IN (" + IDD + ")", cnb, cnc);
                        //delete audit?
                    }

                    tst = "end not dupi measures..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (dupli_assesment != "Ignore")
                    {
                        PreviewTime = "";
                        PreviewLenght = "3000";
                        var duration = ""; var bitratep = 250001;
                        SampleRate = 49000;
                        if (info.OggPreviewPath != null)
                            if (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")))
                            {
                                using (var vorbis = new NVorbis.VorbisReader(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")))
                                {
                                    bitratep = vorbis.NominalBitrate;
                                    var durationp = vorbis.TotalTime.ToString();

                                    var rf = float.Parse((durationp.Split(':'))[0], NumberStyles.Float, CultureInfo.CurrentCulture) * 3600;
                                    var rg = float.Parse((durationp.Split(':'))[1], NumberStyles.Float, CultureInfo.CurrentCulture) * 60;
                                    var rh = float.Parse((durationp.Split(':'))[2], NumberStyles.Float, CultureInfo.CurrentCulture);
                                    var rt = rf + rg + rh;
                                    PreviewLenght = rt.ToString();
                                    PreviewLenghtorig = PreviewLenght;
                                    string[] timepiece = durationp.Split(':');
                                }
                            }

                        if (File.Exists(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            using (var vorbis = new NVorbis.VorbisReader(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            {
                                bitrate = vorbis.NominalBitrate;
                                bitrateorig = bitrate;
                                SampleRate = vorbis.SampleRate;
                                SampleRateorig = SampleRate;
                                duration = vorbis.TotalTime.ToString();
                                if (vorbis.TotalTime.TotalSeconds > 780) IsFullAlbum = "Yes";
                            }
                    }

                    //cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path);

                    var txt_AuthorText = ""; var txt_CDLC_NameText = ""; var txt_TrackNoText = ""; var txt_VersionText = ""; var txt_CDLCIDText = ""; var txt_EoFPathText = ""; var txt_YBLinkText = ""; var txt_BasedOnYBText = "";
                    var txt_BasedOnCFText = ""; var txt_TabLinksText = ""; var txt_SpotifyText = ""; var txt_DescriptionText = ""; var txt_toDosText = ""; var txt_ToneDetailsText = ""; var chbx_SaveInVerisonInfoText = false;
                    var chbx_SaveInDBTxet = false; var chbx_SaveRemotelyText = false; var txt_PackageDate = ""; var txt_UpdateDate = ""; var txt_BasedOn_GP = "";
                    var SaveRemotelyPath = ""; 
                    var versionffile = Directory.GetFiles(unpackedDir, "toolkit.version", System.IO.SearchOption.AllDirectories);
                    var PackageComment = "";
                    if (versionffile.Length > 0)
                    {
                        PackageComment = ReadPackageComment(versionffile[0]);
                    }
                    if (info.ToolkitInfo != null)
                        if (PackageComment != "") ;/*info.ToolkitInfo.PackageComment*/
                    if (PackageComment.IndexOf(";") > 0)
                    {
                        string[] agg = PackageComment.Split(';');// info.ToolkitInfo.PackageComment.ToString().Split(';');
                        txt_AuthorText = agg[0]; txt_CDLC_NameText = agg[1]; txt_TrackNoText = agg[2];
                        txt_VersionText = agg[3]; txt_CDLCIDText = agg[4];txt_EoFPathText = agg[5];
                        txt_YBLinkText = agg[6]; txt_BasedOnYBText = agg[7];txt_BasedOnCFText = agg[8];
                        txt_TabLinksText = agg[9]; txt_SpotifyText = agg[10]; txt_DescriptionText = agg[11];
                        txt_toDosText = agg[12]; txt_ToneDetailsText = agg[13]; chbx_SaveInVerisonInfoText = agg[14] == "Yes" ? true : false;
                        chbx_SaveInDBTxet = agg[15] == "Yes" ? true : false; chbx_SaveRemotelyText = agg[16] == "Yes" ? true : false;SaveRemotelyPath = agg[17];
                        txt_PackageDate = agg[18]; txt_UpdateDate = agg[19]; txt_BasedOn_GP = agg[20];
                    }
                    //Read Track no
                    //www.metrolyrics.com: Nirvana Bleach Swap Meet
                    if (ExistingTrackNo != "")
                        trackno = ExistingTrackNo.ToInt32();
                    else trackno = trackno.ToString() == "" ? txt_TrackNoText.ToInt32() : trackno;/*.Parse(txt_TrackNoText)*/
                    info.Version = txt_VersionText;

                    //cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                    AddSong(timestamp, IDD, cnb, rtxt_StatisticsOnReadDLCs, pB_ReadDLCs, xmlhlist, jsonhlist, clist,
                        dlist, snghlist, cxmlhlist, elist, import_path, original_FileName, ds, unpackedDir, i, info
                      , AppIdD, Guitar, Combo, Rhythm, Bass, Lead, Vocalss, sect1on, Tones_Custom, DD, tkversion, Is_Original
                      , Has_author, Tunings, author, alt, art_hash, audio_hash, audioPreview_hash, Bass_Has_DD, bonus, Available_Duplicate
                      , Available_Old, description, comment, PluckedType, trackno, platformTXT, Is_MultiTrack, MultiTrack_Version, IsLive
                      , CustomsForge_Link, CustomsForge_Like, YouTube_Link, CustomsForge_ReleaseNotes, PreviewTime, PreviewLenght, SongLenght, IsAcoustic
                      , LiveDetails, bitrate, SampleRate, HasOrig, SpotifySongID, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, bbbroken
                      , Duplic, ybSAddress, ybRAddress, IsSingle, IsEP, IsInstrumental, IsSoundtrack, ybAddress, oldAlbumN
                      , audio_changed, oldArtistN, oldSongN, oldYearN, IsUncensored, datemax, IsFullAlbum, PitchShiftableEsOrDd
                      , IsRemastered, InTheWorks, dupli_assesment, j, IsCover, IsDemo, IsRemix, HasFeaturing, IsKaraoke, txt_BasedOnYBText
                      , txt_BasedOnCFText, txt_TabLinksText, txt_toDosText, txt_ToneDetailsText, txt_DescriptionText
                      , txt_PackageDate, txt_UpdateDate, txt_BasedOn_GP, Has_Capo, Has_Showlights, Has_JVocals, IsMedley, IsMultiStrings, IsDeluxe, IsGreatestHits
                      , IsMidi, IsGameSoundtrack, IsTVTheme, IsAmateurCover, txt_EoFPathText, txt_YBLinkText, txt_SpotifyText, cnc);
                    //if (connection != null) connection.Close();//some issues with saving and reading such records
                    if (dupli_assesment == "Insert" || dupli_assesment == "Update") //Common set of action for all
                    {
                        //Get last inserted ID
                        //Thread.Sleep(4000);
                        //try { cnb.Close(); if (cnc is not null) cnc.Close(); } catch (Exception ex) {; }
                        OpenDb();
                        var fcmd = "SELECT TOP 1 ID,File_Hash FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\" ORDER BY ID DESC";
                        DataSet dus = new DataSet(); var norec = 0;
                        while (norec == 0)
                        {
                            dus = SelectFromDB("Main", fcmd, txt_DBFolder.Text, cnb, cnc);
                            norec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
                        }
                        //Useful
                        // Get and Store IDENTITY (Primary Key) for further
                        // INSERTS in child table [Order Details]
                        //cmd.CommandText = "SELECT @@identity";
                        //string id = cmd.ExecuteScalar().ToString();
                        //objAdapter.Fill(dus, "Main");
                        //string strID = dus.Tables["Main"].Rows[0].ToString();

                        var norecf = dus.Tables[0].Rows[0].ItemArray[0];
                        CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                        //if (Is_Duplic) { DataSet dxf = new DataSet(); dxf = UpdateDB("Main", "UPDATE Main SET Duplicate_Of = \"" + Duplic + "\" WHERE ID =" + CDLC_ID + ";", cnb, cnc); }

                        //Define final path for the imported song
                        var namernd = CDLC_ID;
                        var norm_path = GetNormPath(unpackedDir, txt_TempPath.Text, dataPath, platformTXT, info, Is_Original, CDLC_ID, trackno, namernd);

                        //UPDATE ArarngementsDB
                        //connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                        AddArrangements(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, clist, dlist, snghlist, cxmlhlist, elist, ybLAddress, ybBAddress
                            , ybRAddress, ybCAddress, MaxDD, norm_path, Rebuild, platformTXT, unpackedDir, cnb, info, CDLC_ID, "No", cnc);
                        //if (connection != null) connection.Close();//some issues with saving and reading such records

                        //                    AddArrangements(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, hlist, dlist, snghlist, cxmlhlist, elist, ybLAddress, ybBAddress
                        //    , ybRAddress, ybCAddress, MaxDD, norm_path, ConfigRepository.Instance()["dlcm_AdditionalManipul106"] == "Yes" ? true : false, platformTXT, unpackedDir
                        //, false, platformDLCP, unpackedDir, connection, info, CDLC_ID, "Yes");

                        //UPDATE TonesDB
                        //connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                        AddTones(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, clist, dlist, snghlist, cxmlhlist, elist, MaxDD, norm_path
                            , Rebuild, platformTXT, unpackedDir, cnb, info, CDLC_ID, clist, mutit, "No", cnc);
                        //if (connection != null) connection.Close();//some issues with saving and reading such records

                        //Move Extracted Song to Temp Folder
                        int pos = 0;
                        int l = 0;
                        DataSet dis = new DataSet();
                        try //Move from _import into Temp folder (copy+delete as move sometimes fails)
                        {
                            string source_dir = @unpackedDir;
                            string destination_dir = @norm_path;
                            CopyFolder(source_dir, destination_dir);
                            DeleteDirectory(source_dir, false);
                        }
                        catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }

                        //Fixing any _preview_preview issue..Start
                        //Correct moved file path audio,preview
                        //Add wem
                        //Corrent arrangements file path
                        //add new filename if already existing
                        cmd = "UPDATE Main SET ";// Available_Old=\"" + Available_Old + "\",";

                        if (GetParam(15, chbx_Additional_Manipulations) || GetParam(75, chbx_Additional_Manipulations)) //16. Move Original Imported files to temp/0_old                               
                        {//Move imported psarc into the old folder                            
                            var r = CopyMoveFileSafely(txt_RocksmithDLCPath.Text + "\\" + original_FileName, old_Path_Import + "\\" + original_FileName
                                , GetParam(75, chbx_Additional_Manipulations) /*&& !GetParam(75, chbx_Additional_Manipulations) ? false : true*/
                                , ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                            if (r != old_Path_Import + "\\" + original_FileName && File.Exists(r)) cmd += "Original_FileName =\"" + Path.GetFileName(r) + "\", Current_FileName =\"" + Path.GetFileName(r) + "\",";
                            if (File.Exists(r)) cmd += " Available_Old=\"Yes\",";
                        }

                        var audiopath = "";
                        var audioprevpath = "";
                        var ms = "";
                        ms = info.AlbumArtPath;
                        var cmd2 = "";
                        if (ms != "" && ms != null)
                        {
                            pos = ms.ToString().LastIndexOf("\\") + 1;
                            if (AlbumArtPath == info.AlbumArtPath)
                                if (GetParam(36, chbx_Additional_Manipulations)) //37. Keep the Uncompressed Songs superorganized                                
                                {
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                    cmd += ", Album_ArtPathOrig=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                }
                                else
                                {
                                    cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + (platformTXT == "XBox360" ? "\\Root" : "") + "\\gfxassets\\album_art\\" + ms.Substring(pos) + "\"";
                                    cmd += ", Album_ArtPathOrig=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + (platformTXT == "XBox360" ? "\\Root" : "") + "\\gfxassets\\album_art\\" + ms.Substring(pos) + "\"";
                                }
                            else //Override Album Art during the Duplication assements process
                                cmd += " AlbumArt_Hash=\"" + art_hash + "\", AlbumArtPath=\"" + AlbumArtPath + "\", Album_ArtPathOrig =\"" + AlbumArtPath + "\"";


                            //If Cover was applied to the original then update its album art
                            if (dupliID != "")
                            {
                                cmd2 = cmd + " WHERE ID=" + dupliID;
                                DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmd2 + ";", cnb, cnc);
                            }
                        }
                        pos = (info.OggPath.LastIndexOf(".wem"));
                        ms = info.OggPath;

                        var path_decom1 = "";
                        var path_decom2 = "";
                        if (GetParam(36, chbx_Additional_Manipulations)) //37. Keep the Uncompressed Songs superorganized                                
                        {
                            path_decom1 = "\\Toolkit\\";
                            path_decom2 = "\\EOF\\";
                        }
                        else
                        {
                            path_decom1 = (platformTXT == "XBox360" ? "\\Root" : "") + "\\audio\\" + ((platformTXT.ToString() == "Pc") ? "windows" : ((platformTXT.ToString() == "Mac") ? "mac" : ((platformTXT.ToString() == "PS3") ? "ps3" : (platformTXT.ToString() == "Xbox360") ? "xbox360" : ""))) + "\\";
                            path_decom2 = (platformTXT == "XBox360" ? "\\Root" : "") + "\\audio\\" + ((platformTXT.ToString() == "Pc") ? "windows" : ((platformTXT.ToString() == "Mac") ? "mac" : ((platformTXT.ToString() == "PS3") ? "ps3" : (platformTXT.ToString() == "Xbox360") ? "xbox360" : ""))) + "\\"; //"\\songs\\arr\\";
                        }

                        if (ms.Length > 0 && pos > 1)
                        {
                            ms = ms.Substring(0, pos);
                            pos = ms.LastIndexOf("\\") + 1;
                            l = ms.Substring(pos).Length;
                            audiopath = norm_path + path_decom1 + ms.Substring(pos, l);

                            cmd += cmd.Substring(cmd.Length - 1, 1) == "," ? "" : ","
                                //((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "" : " ,")
                                //+((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "" : " ,")
                                + " AudioPath=\"" + audiopath + ".wem\"";
                            cmd += " , OggPath=\"" + norm_path + path_decom2 + ms.Substring(pos, l) + "_fixed.ogg\" , Song_Lenght=\"" + SongLenght + "\"";/*_fixed*/
                            info.OggPath = audiopath + ".wem\"";
                        }
                        pos = info.OggPreviewPath == null ? 0 : (info.OggPreviewPath.LastIndexOf(".wem"));
                        ms = info.OggPreviewPath;
                        if (pos > 1 && (info.OggPreviewPath != null))
                            if (ms.Length > 0)
                            {
                                ms = ms.Substring(0, pos);
                                if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview_preview"));
                                pos = ms.LastIndexOf("\\") + 1;
                                l = ms.Substring(pos).Length;
                                audioprevpath = norm_path + path_decom1 + ms.Substring(pos, l);
                                cmd += " , audioPreviewPath=\"" + audioprevpath + ".wem\"";
                                cmd += " , oggPreviewPath=\"" + audioprevpath + "_fixed.ogg\" , PreviewLenght=\"" + PreviewLenght + "\"";
                                info.OggPreviewPath = audioprevpath + ".wem";
                            }
                        cmd += " , Folder_Name=\"" + norm_path + "\"";

                        cmd += " WHERE ID=" + CDLC_ID;
                        DataSet dxis = new DataSet(); dxis = UpdateDB("Main", cmd + ";", cnb, cnc);

                        //fix potentially issues with songs with the audio preview WEM  file the same as the original song(file size{no preview})
                        //Move wem to KIT folder + rename
                        if (info.OggPreviewPath != null)
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1)
                            {
                                try
                                {
                                    File.Move((audiopath + "_preview.wem"), (audiopath + ".wem"));
                                    File.Move((audioprevpath + "_preview.wem"), (audioprevpath + ".wem"));
                                    timestamp = UpdateLog(timestamp, "Issues w the WEM filenames when no preview ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }
                                catch (Exception ee)
                                {
                                    timestamp = UpdateLog(timestamp, "FAILED1" + ee.Message + "----" + info.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    Console.WriteLine(ee.Message);
                                }
                                timestamp = UpdateLog(timestamp, "Fixed preview_preview issue", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }

                        var previewN = audioprevpath;
                        //Fixing any _preview_preview issue..End
                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000 
                        if (!GetParam(78, chbx_Additional_Manipulations))
                            if ((GetParam(69, chbx_Additional_Manipulations) && info.OggPath != null)
                                || (((GetParam(34, chbx_Additional_Manipulations) && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                                || (GetParam(55, chbx_Additional_Manipulations) && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                                || (GetParam(88, chbx_Additional_Manipulations) && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                > float.Parse(ConfigRepository.Instance()["dlcm_MinPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                                && info.OggPath != null)))
                            {
                                var d1 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
                                if (d1.Split(';')[0] == "1")
                                {
                                    if ((GetParam(34, chbx_Additional_Manipulations) && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                                        || (GetParam(55, chbx_Additional_Manipulations)
                                        && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                                        && info.OggPath != null)
                                    {
                                        tst = "start set preview"; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main WHERE ";
                                        cmd += "FilesMissingIssues is null AND ID=" + CDLC_ID + "";
                                        if (GetParam(55, chbx_Additional_Manipulations) && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture) > float.Parse(ConfigRepository.Instance()["dlcm_PreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture) && info.OggPreviewPath != null) DeleteFile(info.OggPreviewPath, false);
                                        FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "DLCManager", cnc);
                                    }
                                    tst = "end set preview ..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                    if (GetParam(69, chbx_Additional_Manipulations) && info.OggPath != null)
                                    {
                                        cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE " +
                                            "FilesMissingIssues is null AND (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                        cmd += " AND ID=" + CDLC_ID + "";
                                        FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "DLCManager", cnc);
                                    }
                                    tst = "end set encoding to" + ConfigRepository.Instance()["dlcm_MaxBitRate"] + "kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                }
                                if (d1.Split(';')[1] == "1") dupli_assesment = "Ignore";
                                if (d1.Split(';')[2] == "1") { j = 10; i = 9999; }
                            }

                        //Delete any preview_fixed_preview file created..by....? ccc
                        var source_dir1 = norm_path + path_decom1;
                        foreach (string preview_fixed_preview in Directory.GetFiles(source_dir1, "*_preview_fixed_preview*", System.IO.SearchOption.AllDirectories))
                        {
                            DeleteFile(preview_fixed_preview, false);
                        }

                        //Delete any Wav file created..by....?ccc
                        foreach (string wav_name in Directory.GetFiles(source_dir1, "*.wav", System.IO.SearchOption.AllDirectories))
                        {
                            DeleteFile(wav_name, false);
                        }
                        //Set Preview

                        ////Create Preview //Fix Preview
                        //if (GetParam() && info.OggPath != null)
                        //{
                        //    cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
                        //    cmd += " AND ID=" + CDLC_ID + ";";
                        //    FixMissingPreview(cmd, cnb, AppWD);

                        //    tst = "end set preview ..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //}
                        //Create Preview //Fix Preview


                        UpdatePackingLog("LogImporting", DB_Path, packid, CDLC_ID, tst, cnb, cnc);

                    }
                    //Updating the Standardization table
                    DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\""
                        + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", txt_DBFolder.Text, cnb, cnc);

                    if (GetNoRec(dzs, cnb, cnc) == 0)/*dzs.Tables[0].Rows.Count */
                    {
                        var insertcmdd = "Artist, Album, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Year_Correction";
                        var insertvalues = "\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\",\"" + SpotifyArtistID + "\",\"" + SpotifyAlbumID
                            + "\",\"" + SpotifyAlbumURL + "\",\"" + SpotifyAlbumPath + "\",\"" + SpotifyAlbumYear + "\"";
                        InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb, mutit, cnc);
                    }
                    timestamp = UpdateLog(timestamp, "done", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    pB_ReadDLCs.Increment(1);
                }
            }

            var CDLCID = CDLC_ID.ToString() == "" ? IDD : CDLC_ID.ToString();
            string CDLCName = (errr == false) ? info.Name : "";
            string rtr = CDLCID + ";" + CDLCName + ";" + dupli_assesment + ";" + dupli_assesment_reason + ";" + (stopp ? "yes" : "no");
            return rtr;
        }

        //public void OpenDb()
        //{
        //    //if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] == "Yes" && File.Exists(tz))
        //    //{
        //    //    DialogResult result1 = DialogResult.Cancel;
        //    //    result1 = MessageBox.Show("Chose DB System:\n1. (Yes) Microsoft Access (.accdb) or\n2. (No) SQLite3 (.db)."
        //    //    , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    //    if (result1 == DialogResult.Yes) ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
        //    //}
        //    var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
        //    tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
        //    if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
        //        try
        //        {
        //            if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
        //        }
        //        catch (Exception exx)
        //        {

        //            ShowConnectivityError(exx, "", lbl_Access);
        //            try
        //            {
        //                if (File.Exists(cnb.DataSource.ToString())) cnb.Open(); //2nd time makes it work sometimes e.g. x64 solution
        //            }
        //            catch (Exception ex)
        //            {
        //                if (lbl_Access != null)
        //                {
        //                    lbl_Access.Text = "missing Access plugin!";
        //                    lbl_Access.Visible = true;
        //                }
        //                string vb = null; vb = DisplayData();
        //                ShowConnectivityError(ex, "2nd FAIL to use M$ ACCESS plugin:\n" + vb, lbl_Access);
        //                //revert to SQLite
        //                if (File.Exists(tz))
        //                {
        //                    ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";

        //                    ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
        //                    //cnz = new SQLiteConnection("Data Source="+ ConfigRepository.Instance()["dlcm_DBFolder"]);
        //                    //cnz.Open();

        //                    //cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);

        //                    //SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(fcmds, cnc);
        //                }
        //                else MessageBox.Show("No Microsoft Access or SQLite databases (or access;plugins etc) available. Good Luck as (the) C-DLC Manager wont really work!");
        //            }
        //        }
        //    else
        //        try
        //        {
        //            ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
        //            if (File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"])) cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
        //            ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
        //        }
        //        catch (Exception exx)
        //        {
        //            ShowConnectivityError(exx, "", lbl_Access);
        //        }
        //    //if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] == "Yes")
        //    //{
        //    //cnz = new SQLiteConnection("Data Source="+ ConfigRepository.Instance()["dlcm_DBFolder"]);
        //    //cnz.Open();              

        //    //SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(fcmds, cnc);
        //    //}
        //    //var tdz = ConfigRepository.Instance()["dlcm_DBFolder"];
        //    //tdz = tdz.Replace("AccessDB.accdb", "SQLLiteDB.db");
        //    //ConfigRepository.Instance()["dlcm_DBFolder"] = tdz;
        //    //cnz = new SQLiteConnection("Data Source="+ ConfigRepository.Instance()["dlcm_DBFolder"]);
        //    //cnz.Open();
        //}
        //static string GetAlternateNo(DLCPackageData datas, OleDbConnection cnb, SQLiteConnection cnc, string Is_Original)
        static string GetAlternateNo(DLCPackageData datas, OleDbConnection cnb, SQLite.SQLiteConnection cnc, string Is_Original)
        {
            var a = "";

            //Get the higgest Alternate Number
            var sel = "";
            sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + datas.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + datas.SongInfo.Album + "\") AND ";
            sel += "(LCASE(Song_Title)=LCASE(\"" + datas.SongInfo.SongDisplayName + "\") OR ";
            sel += "LCASE(Song_Title) like \"%" + datas.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
            sel += "LCASE(Song_Title_Sort) =LCASE(\"" + datas.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + datas.Name + "\");";
            DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, "", cnb, cnc);
            var altver = dds.Tables[0].Rows[0].ItemArray[0].ToString();
            if (Is_Original == "No") a = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
            return a;
        }

        public string GetTExtFromFile(string ss)
        {

            var info = File.OpenText(ss);
            string tecst = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.ToLower().Contains("lastconversiondatetime"))
                {
                    tecst = (line.ToLower().Replace("<lastconversiondatetime>", ""));
                    if (tecst == line) tecst = ((line.Replace("\"", "")).ToLower().Replace("lastconversiondatetime: ", "")).Replace(",", "");
                    break;
                }
            }
            info.Dispose();
            info.Close();
            return tecst;
        }

        public void Btn_RePack_Click(object sender, EventArgs e)
        {
            var old = ConfigRepository.Instance()["dlcm_MuliThreading"];
            var metadatadisplayedonce = ConfigRepository.Instance()["dlcm_AdditionalManipul115"];
            timestamp = UpdateLog(timestamp, "\n----------------------------------------------\nStarting Packing " + " songs.", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var slct = "SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u " +
"WHERE Type=\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" and Comments like \"%dlcm_AdditionalManipul%\"" +
" AND DisplayGroup IN ('Pack','General') ORDER BY DisplayGroup DESC";
            Selection frm1 = new Selection(slct, "Continue Packing", "Stop");
            frm1.ShowDialog();
            if (frm1.StopImport) return;

            SaveOK = "OK"; SaveSettings(); SaveOK = "";
            var atleastone = false;
            var pack = ""; var norows = 0; var brkn = 0;

            BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);

            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;

            var i = 0; var metainfo = "";
            if (btn_RePack.Text == "Stop Repack")
            {
                btn_RePack.Text = "RePack";
                if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();// Cancel the asynchronous operation.
            }
            else
            {
                var Temp_Path_Import = txt_TempPath.Text;
                var dataPath = txt_TempPath.Text + "\\0_data";
                var old_Path_Import = txt_TempPath.Text + "\\0_old";
                var dflt_Path_Import = txt_TempPath.Text + "\\0_to_import";
                var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
                var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
                var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
                var repacked_Path = txt_TempPath.Text + "\\0_repacked";
                var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
                var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
                var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
                var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
                var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
                var Log_PSPath = txt_TempPath.Text + "\\0_log";
                var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
                var Archive_Path = txt_TempPath.Text + "\\0_archive";
                var Temp_Path = txt_TempPath.Text + "\\0_temp";
                string pathDLC = txt_RocksmithDLCPath.Text;
                CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC,
                    repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);

                btn_RePack.Text = "Stop Repack";
                if ((ConfigRepository.Instance()["general_defaultauthor"] == "" || ConfigRepository.Instance()["general_defaultauthor"] == "Custom Song Creator") && chbx_DebugB.Checked) ConfigRepository.Instance()["general_defaultauthor"] = "catara";

                timestamp = UpdateLog(timestamp, "Packing: ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                var cmd = "SELECT * FROM Main ";//AND Groupz=\""+cbx_Groups.Text+"\") ";// cd.Replace( " ORDER BY Artist,Album_Year,Album,Track_No,ID","");AND Type=\""+ chbx_Configurations.Text+ "\" 
                cmd += GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text, cnb, "", 0,
                    GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc);
                cmd = cmd.Replace(c("dlcm_SearchFields"), "ID");

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul106"] == "Yes")
                {
                    MessageBox.Show("Only Pack if in 1 groups (excl. Default) (Option 106 set to Yes): ");
                    cmd = cmd.Replace(" ORDER BY Artist,Album_Year,Album,Track_No,ID", "") + " AND vw_CountGroups.CountGrp < 2" + " ORDER BY Artist,Album_Year,Album,Track_No,ID";
                    cmd = cmd.Replace("SELECT * FROM Main", "SELECT Main.*, vw_CountGroups.CountGrp FROM Main LEFT JOIN vw_CountGroups ON str(vw_CountGroups.CDLC_ID)= STR(Main.ID)");
                }

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul109"] == "Yes")
                {
                    MessageBox.Show("Previous filter indicated to be removed from Selection (Option 109 set to Yes): " + c("dlcm_FilterPrevious"));
                    cmd = cmd.Replace(" ORDER BY Artist,Album_Year,Album,Track_No,ID", "") + " AND CSTR(u.ID) NOT IN (" + "SELECT m.CDLC_ID FROM Groups AS m WHERE m.CDLC_ID = CSTR(u.ID) AND m.Groupz IN ('" + c("dlcm_FilterPrevious") + "','Default')" + ")" + " ORDER BY Artist,Album_Year,Album,Track_No,ID";
                    //cmd = cmd.Replace(" ORDER BY Artist,Album_Year,Album,Track_No,ID", "") + "AND "+c("dlcm_FilterPrevious") + " ORDER BY Artist,Album_Year,Album,Track_No,ID";
                    //cmd = cmd.Replace("SELECT * FROM Main", "SELECT Main.*, vw_CountGroups.CountGrp FROM Main LEFT JOIN vw_CountGroups ON str(vw_CountGroups.CDLC_ID)= STR(Main.ID)");
                }
                //Read from DB
                MainDBfields[] SongRecord = new MainDBfields[20000];
                timestamp = UpdateLog(timestamp, "\n" + cmd, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", SearchCmd, "", cnb, cnc);
                var noOfgRec = GetNoRec(dgs, cnb, cnc); //dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
                DataSet djs = new DataSet(); djs = SelectFromDB("Main", cmd, "", cnb, cnc);
                var noOfjRec = GetNoRec(djs, cnb, cnc); //djs.Tables.Count == 0 ? 0 : djs.Tables[0].Rows.Count;
                if (noOfjRec != noOfgRec)
                {
                    DialogResult result7 = MessageBox.Show("No of Songs currently Selected (" + noOfgRec + ") is different than no of songs returned by the Filter(" + noOfjRec + ")" +
                        ". do you wanna keep current selected DLCs? \"No\" will take as Focus population the filter results.", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result7 == DialogResult.Yes) cmd = SearchCmd.Replace(c("dlcm_SearchFields"), "*");
                }
                SongRecord = GetRecord_s(cmd, cnb, cnc);
                norows = SongRecord[0].NoRec.ToInt32();
                if (norows > 0) CreatePackingGroup(cmd, cnb, cbx_Groups.Text, norows, cnc);
                timestamp = UpdateLog(timestamp, "Select over. " + norows, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                string spotystatus = "", ybstatus = "", ftpstatus = "";

                if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();
                if ((GetParam(58, chbx_Additional_Manipulations) || GetParam(59, chbx_Additional_Manipulations)) && netstatus != "OK")
                {
                    if (GetParam(82, chbx_Additional_Manipulations))
                    {
                        DialogResult result3 = MessageBox.Show("As selected by option 41 Tool will connect to Spotify to retrieve Track No, album covers, Year information, etc.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    if (spotystatus == "OK") netstatus = UtilitiesFunctions.CheckIfConnectedToSpotify().Result.ToString();
                    timestamp = UpdateLog(timestamp, "ending establishing connection with SPOTIFY.", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                var tzu = c("dlcm_MainDBFormat");
                if (c("dlcm_MainDBFormat").IndexOf("PS3") >= 0) if (FTPAvail(c("dlcm_FTP" + c("dlcm_MainDBFormat").Replace("PS3_", ""))).ToLower() != "ok") ftpstatus = "nok";
                    else ftpstatus = "ok";
                else ftpstatus = "nok";

                //Set pack id
                pack = (int.Parse(GetMax("Pack_AuditTrail", "Pack", cnb, cnc)) + 1).ToString();
                //DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT max(val(Pack)) FROM Main", txt_DBFolder.Text, cnb, cnc);
                //if (GetNoRec(dms, cnb, cnc) > 0) pack = (int.Parse(dms.Tables[0].Rows[0].ItemArray[0].ToString()) + 1).ToString();

                //dms.Tables[0].Rows.Count
                if (chbx_PS3.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul92"] == "Yes") HANPackagePreparation();
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = norows; pB_ReadDLCs.Step = 1;
                var ordno = "";
                foreach (var file in SongRecord)
                {
                    i++;
                    timestamp = UpdateLog(timestamp, "\n+--------------------------------------------------------" +
                        "\nStarting Packing " + i + "/" + norows + " song: " + file.Artist + " - " + file.Song_Title, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    pB_ReadDLCs.Increment(1);
                    if (i == norows + 1 || file.Song_Title == "") break; /*{ i = 0; }*/
                    ordno = ordno + "1";
                    if ((file.Is_Broken == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul7"] == "Yes"))
                    {
                        brkn++;
                        continue;
                    }
                    if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !GetParam(7, chbx_Additional_Manipulations))) //"8. Don't repack Broken songs")
                    {
                        var args = file.ID + ";" + (file.Bass_Has_DD == "Yes" ? true : false) + ";";
                        args += (chbx_PC.Checked != false ? "PC" : "") + ";" + (chbx_PS3.Checked != false ? "PS3" : "") + ";" + (chbx_XBOX360.Checked != false ? "XBOX360" : "") + ";" + (chbx_Mac.Checked != false ? "Mac" : "") + ";" + netstatus + ";";
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul54"] == "Yes" ? true : false) + ";" + cbx_Groups.Text + ";";
                        args += Groupss + ";" + Temp_Path_Import + ";"; //chbx_Beta
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul2"] == "Yes" ? true : false) + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul64"] == "Yes" ? true : false) + ";";
                        args += true + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul65"] == "Yes" ? true : false) + ";";//chbx_UniqueID, chbx_Last_Packed,chbx_Last_Packed.Enabled
                        args += true + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul49"] == "Yes" ? true : false) + ";"; //pack&copy
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul63"] == "Yes" ? true : false) + ";" + true + ";"; //CopyFTPFile /replace /REPACE.ENABLED
                        args += pack + ";" + "DLCManager" + ";";
                        args += file.Original_FileName.Replace(";", "") + ";" + file.Folder_Name.Replace(";", "") + ";"; ;// DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString() + ";" + DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString() + ";";
                        args += ConfigRepository.Instance()["dlcm_AdditionalManipul49"] + ";" + file.Remote_Path.Replace(";", "") + ";";
                        args += (c("dlcm_MainDBFormat").IndexOf("PS3") >= 0 ? (c("dlcm_FTP" + c("dlcm_MainDBFormat").Replace("PS3_", ""))) : "") + ";";
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul5"] == "Yes" ? true : false) + ";" + file.Bass_Has_DD + ";" + file.Keep_BassDD + ";";//Remove Bass , bass dd,chbx_KeepBassDD
                        args += (file.Keep_DD == "Yes" ? true : false) + ";" + file.Is_Original + ";" + file.ID + ";";//chbx_KeepDD.Checked, chbx_Original.Tex, dlc_id
                        args += cmd + (cmd.IndexOf(";") > 0 ? "" : ";") + txt_RocksmithDLCPath.Text + ";" + file.DLC_Name + ";"; //SearchCmd + ";" + RocksmithDLCPath, DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value
                        args += ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + ";" + c("dlcm_Split4Pack") + ";" + "DLCManager" + ";" + ordno + i + ";" + spotystatus + ";" + ybstatus + ";" + ftpstatus + ";" + "No"; //i in case of multi
                        string[] argz = (args).ToString().Split(';');//4debugpurposes
                        ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "";
                        bwRGenerate.RunWorkerAsync(args);
                        do
                            Application.DoEvents();
                        while (bwRGenerate.IsBusy || ConfigRepository.Instance()["dlcm_GlobalTempVariable"] != "Yes");//keep singlethread as toolkit not multithread abled

                        //if (!bwRGenerate.IsBusy) 
                        atleastone = true;
                    }
                    else brkn++;
                }
            }
            timestamp = UpdateLog(timestamp, "Ending Packing  songs.", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (chbx_PS3.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul92"] == "Yes" && atleastone) HANPackage();

            bwRGenerate.Dispose();
            btn_RePack.Text = "RePack";
            ConfigRepository.Instance()["dlcm_MuliThreading"] = old;
            GeneratePackingSummary(pack, ConfigRepository.Instance()["dlcm_Global2TempVariable"], brkn, cnb, i, norows, cnc);
            ConfigRepository.Instance()["dlcm_AdditionalManipul115"] = metadatadisplayedonce;
            timestamp = UpdateLog(timestamp, "Ended Packing", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        }


        public void CheckValidityGetHASHAdd2Import(object sender, DoWorkEventArgs e)
        {
            var startT = DateTime.Now;
            timestamp = UpdateLog(timestamp, "CheckValidityGetHASHAdd2Import", false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            string[] args = (e.Argument).ToString().Split(';');
            string s = args[0].ToString();
            string i = args[1];
            var tsst = "Start Gathering ...";
            string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];
            timestamp = UpdateLog(timestamp, tsst, false, tmpPath, i, "", null, null);
            BackgroundWorker worker = (BackgroundWorker)sender;


            string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
            string ImportPackNo = args[2];
            var invalid = args[3];

            if (!s.IsValidPSARC())
            {
                timestamp = UpdateLog(timestamp, "errort at import " + string.Format("File '{0}' isn't valid. File extension was changed to '.invalid'",
                    Path.GetFileName(s)), true, tmpPath, mutit.ToString(), "", null, null);
                if (!File.Exists(s) && File.Exists(s.Replace(".psarc", ".invalid"))) File.Move(s.Replace(".psarc", ".invalid"), s);
                invalid = "Yes";
            }

            //try to get the details
            // Create the FileInfo object only when needed to ensure 
            // the information is as current as possible.

            string FileHash = ""; string plt = ""; string fiDirectoryName = ""; string fiName = ""; string fiCreationTime = ""; string fiLength = "";
            try
            {
                System.IO.FileInfo fi = null;
                fi = new System.IO.FileInfo(s);
                plt = fi.FullName.GetPlatform().platform.ToString();
                fiDirectoryName = fi.DirectoryName; fiName = fi.Name; fiCreationTime = fi.CreationTime.ToString(); fiLength = fi.Length.ToString();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                timestamp = UpdateLog(timestamp, "error at file.info", true, tmpPath, "", "DLCManager", null, null);
                ErrorWindow frm1 = new ErrorWindow("error when calc file info ", "", "Error at import", false, false, true, "", "", "");
                frm1.ShowDialog();
            }
            //Generating the HASH code
            FileHash = GetHash(s);
            var ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;

            var insertvalues = "\"" + s + "\";\"" + fiDirectoryName + "\";\"" + fiName + "\";\"" + fiCreationTime + "\";\""
                + FileHash + "\";\"" + fiLength + "\";\"" + ff + "\";\"" + ImportPackNo + "\";\"" + (plt == "Pc" ? "Pc" : plt) + "\";\"" + invalid + "\";";
            insrts[i.ToInt32()] = insertvalues.Replace("\"", "");

            //    // pretend like this a really complex calculation going on eating up CPU time
            //    System.Threading.Thread.Sleep(100);
            worker.ReportProgress(100);
            e.Result = "42";
            mutit += 1;
        }
        public void ValidProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
            //    pB_ReadDLCs.Value = e.ProgressPercentage;
            //else
            //    pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;
            ;
            //ShowCurrentOperation(e.UserState as string);
        }
        public void ProgressChanged(object sender, ProgressChangedEventArgs e)
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
            //if (!(e.Result == null))
            //    switch (e.Result.ToString())
            //    {

            //        case "generate":
            //            var message = "Package was generated.";
            //            if (errorsFound.Length > 0)
            //                message = String.Format("Package was generated with errors! See below: {0}(1}", Environment.NewLine, errorsFound);
            //            message += String.Format("{0}You want to open the folder in which the package was generated?{0}", Environment.NewLine);
            //            if (MessageBox.Show(message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //            {
            //                Process.Start(Path.GetDirectoryName(dlcSavePath));
            //            }
            //            break;
            //        case "error":
            //            var message2 = String.Format("Package generation failed. See below: {0}{1}{0}", Environment.NewLine, errorsFound);
            //            MessageBox.Show(message2, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            Parent.Focus();
            //            break;
            //    }
            ////btn_RePack.Text = "RePack";
        }
        private void ValidProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (!(e.Result == null))
            //    switch (e.Result.ToString())
            //    {

            //case "generate":
            //mutit += 1;
            //        break;
            //    case "error":
            //        var message2 = String.Format("Package generation failed. See below: {0}{1}{0}", Environment.NewLine, errorsFound);                        
            //        break;
            //}
        }

        //private void rbtn_Population_Groups_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtn_Population_Groups.Checked)
        //    {
        //        cbx_Groups.Enabled = true;
        //        if (cbx_Groups.Text != "") RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groupz=\"" + cbx_Groups.Text + "\")", "");
        //        // to add the query programatically
        //    }
        //    else
        //    {
        //        cbx_Groups.Enabled = false;
        //    }
        //}

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
            txt_Artist_Sort.Text += cbx_Artist_Sort.Items[cbx_Artist_Sort.SelectedIndex].ToString();
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
            SaveOK = "Ok";
            timestamp = UpdateLog(timestamp, "Starting to open MainDB", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Save settings
            SaveSettings();
            //timestamp = UpdateLog(timestamp, "Ending saving settings", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Show Intro database window
            MainDB frm = new MainDB(cnb, cnc, false);
            timestamp = UpdateLog(timestamp, "End initiate MainDB", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            frm.Show();
            timestamp = UpdateLog(timestamp, "End showing MainDB", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        }

        private void btn_Standardization_Click(object sender, EventArgs e)
        {
            SaveOK = "Ok";//Save settings
            SaveSettings(); SaveOK = "";
            var DBb_Path = txt_DBFolder.Text;
            Standardization frm = new Standardization(DBb_Path, txt_TempPath.Text, txt_RocksmithDLCPath.Text, GetParam(39, chbx_Additional_Manipulations), GetParam(40, chbx_Additional_Manipulations), cnb, null, cnc);
            frm.Show();
        }

        private void chbx_Additional_Manipualtions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {/*(chbx_DefaultDB.Checked == true ? MyAppWD : */
            GenericFunctions.Translation_And_Correction(txt_DBFolder.Text, pB_ReadDLCs, cnb, rtxt_StatisticsOnReadDLCs, cnc);
        }

        /// <summary>
        /// Convert SongInfoShort to SongInfo that contains only user selections or defualts
        /// </summary>
        /// <param name="sonsnghlistShort"></param>
        /// <param name="sonsnghlist"></param>
        /// <returns></returns>
        private List<RocksmithToTabLib.SongInfo> SongInfoShortToSongInfo(IList<SongInfoShort> sonsnghlistShort, IList<RocksmithToTabLib.SongInfo> sonsnghlist)
        {
            var songIdPre = string.Empty;
            var newSonsnghlist = new List<RocksmithToTabLib.SongInfo>();
            var newSongNdx = 0;

            for (var i = 0; i < sonsnghlistShort.Count(); i++)
            {
                var songIdShort = sonsnghlistShort[i].Identifier;
                var arrangementShort = sonsnghlistShort[i].Arrangement;

                if (songIdPre != songIdShort)
                {
                    // add the new song info
                    var songInfo = sonsnghlist.FirstOrDefault(x => x.Identifier == songIdShort);
                    newSonsnghlist.Add(songInfo);
                    newSongNdx++;

                    // clear arrangments so we can add user selections
                    if (arrangementShort != null)
                    {
                        newSonsnghlist[newSongNdx - 1].Arrangements.Clear();
                        newSonsnghlist[newSongNdx - 1].Arrangements.Add(arrangementShort);
                    }
                }
                else if (songIdPre == songIdShort && arrangementShort != null)
                    newSonsnghlist[newSongNdx - 1].Arrangements.Add(arrangementShort);

                songIdPre = songIdShort;
            }

            return newSonsnghlist;
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
            if (!Directory.Exists(source_dir)) return;
            try
            {
                if (Directory.Exists(destination_dir)) IOExtension.DeleteDirectory(destination_dir);
                Directory.Move(source_dir, destination_dir);
            }
            catch (Exception ee)/*System.IO.DirectoryNotFoundException */
            {
                Console.WriteLine(ee.Message);
                timestamp = UpdateLog(timestamp, "Error Renaming folder: " + source_dir + ee.Message, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }

        private void chbx_DefaultDB_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_DefaultDB.Checked == true)
            {
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
                    txt_DBFolder.Text = MyAppWD + "\\..\\AccessDB.accdb";
                else
                    txt_DBFolder.Text = MyAppWD + "\\..\\SQLLite.db";
                MessageBox.Show("Defaulting DB location to app folder template DB.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool CheckAlreadyImportedRetail(string psarcname, string hash)
        {
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Groups", "SELECT * FROM Groups WHERE Type=\"RetailHashz\" " +
                "AND Comments=\"" + hash + "\"", "", cnb, cnc);
            var rcount = GetNoRec(dhs, cnb, cnc); //dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            DataSet dh = new DataSet(); dh = SelectFromDB("Cache", "SELECT * FROM Cache WHERE PSARCHash=\"" + hash + "\"", "", cnb, cnc);
            var count = GetNoRec(dh, cnb, cnc); //dh.Tables.Count == 0 ? 0 : dh.Tables[0].Rows.Count;
            if (rcount > 0 && count > 0) return true;
            return false;
        }
        private bool CheckAlreadyImportedCache(string psarcname, string hash)
        {
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Groups", "SELECT * FROM Groups WHERE Type=\"RetailHashz\" " +
                "AND Comments=\"" + hash + "\"", "", cnb, cnc);
            var rcount = GetNoRec(dhs, cnb, cnc); //dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            if (rcount > 0) return true;
            return false;
        }
        private void btn_LoadRetailSongs_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "1/20 Starting Retail Songs processing ...." + DateTime.Now, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var nosongs = 0; var noarg = 0; var notones = 0; var errsongs = ""; var errargs = ""; var errtones = ""; var errs = ""; var k = -1;

            DataSet dhs = new DataSet(); dhs = SelectFromDB("Cache", "SELECT * FROM Cache ", "", cnb, cnc);
            var rcount = GetNoRec(dhs, cnb, cnc);//dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            DialogResult result1 = chbx_CleanTemp.Checked || GetParam(38, chbx_Additional_Manipulations) || rcount == 0 ? DialogResult.No :
                MessageBox.Show(rcount + " entries will be deleted. Are you sure?. E.g manually save existing cache table" +
                " to them manually  import/insert/mark back personal entries/select-flag/comments\n" +
                "\"dlcpacks\" Folder Clean-up NOT selected or \"too many fields\" warning enabled. Regardless" +
                " do you want to clean&ignore?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //rcount == 0 ? DialogResult.Yes : MessageBox.Show(
            //rcount + " entries will be deleted. Are you sure?. E.g manually save existing cache table to them manually
            //import/insert/mark back personal entries/select-flag/comments", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result1 == DialogResult.No)
            //{
            //    return;
            //}
            var cnt = 0;
            var cnti = 0;

            var Temp_Path_Import = txt_TempPath.Text + "\\dlcpacks";
            string pathDLC = c("general_rs2014path") + "\\DLC";
            if (!chbx_DebugB.Checked) MessageBox.Show("Please make sure one of the following Retail Packs:\ncache.psarc, songs.psarc, rs1compatibilitydisc.psarc(.edat if PS3 format), rs1compatibilitydlc.psarc(.edat) \n\n, are in the Import or Game Folder(s): " + pathDLC + "\n\nAlso, make sure you have enough space for the packing & unpacking operations Platform x 10GB");
            CreateTempFolderStructure(txt_TempPath.Text, txt_TempPath.Text + "\\0_old", txt_TempPath.Text + "\\0_broken", txt_TempPath.Text + "\\0_duplicate"
                , txt_TempPath.Text + "\\0_dlcpacks", pathDLC, txt_TempPath.Text + "\\0_Repacked", txt_TempPath.Text + "\\0_Repacked\\XBOX360",
                txt_TempPath.Text + "\\0_Repacked\\PC", txt_TempPath.Text + "\\0_Repacked\\MAC", txt_TempPath.Text + "\\0_Repacked\\PS3",
                ConfigRepository.Instance()["dlcm_LogPath"], txt_TempPath.Text + "\\0_log", txt_TempPath.Text + "\\0_albumCovers", txt_TempPath.Text + "\\0_archive"
                , txt_TempPath.Text + "\\0_data", txt_TempPath.Text + "\\0_temp", txt_TempPath.Text + "\\0_to_import");

            //read all the .PSARCs in the IMPORT folder
            //if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")
            string[] psarcFiles1; string[] tmp = { "", "" };
            var DBb_Path = txt_DBFolder.Text;
            string importlist = "";
            //if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();
            //if (netstatus != "NOK" && netstatus != "") netstatus = CheckIfConnectedToInternet().Result.ToString();
            ConfigRepository.Instance()["dlcm_FTPstatus"] = "OK";
            psarcFiles1 = GetFTPFilesPlusDLC(c("dlcm_FTPEU"), pathDLC, "EU");
            var psarcFiles2 = psarcFiles1.Concat(GetFTPFilesPlusDLC(c("dlcm_FTPUS"), pathDLC, "US")).ToArray();
            var psarcFiles3 = psarcFiles2.Concat(GetFTPFilesPlusDLC(c("dlcm_FTPJP"), pathDLC, "JP")).ToArray();
            var psarcFiles4 = c("dlcm_PS4").IndexOf("ftp") >= 0 ? psarcFiles3.Concat(GetFTPFilesPlusDLC(c("dlcm_PS4"), pathDLC, "")).ToArray() : psarcFiles3.Concat(GetFilesPlusDLC(c("dlcm_PS4"))).ToArray();
            var psarcFiles5 = c("dlcm_PC") == pathDLC ? tmp : psarcFiles4.Concat(GetFilesPlusDLC(c("dlcm_PC"))).ToArray();
            var psarcFiles6 = psarcFiles5.Concat(c("dlcm_Mac") == pathDLC || c("dlcm_PC") == c("dlcm_Mac") ? tmp : GetFilesPlusDLC(c("dlcm_Mac"))).ToArray();
            var psarcFiles7 = psarcFiles6.Concat(GetFilesPlusDLC(c("dlcm_RocksmithDLCPath"))).ToArray();
            var psarcFiles = psarcFiles7 == null ? tmp : (psarcFiles7.Concat(GetFilesPlusDLC(pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014"))).ToArray());
            ConfigRepository.Instance()["dlcm_FTPstatus"] = "OK";
            //string[] psarcFiles; 
            //if (pathDLC.IndexOf("\\DLC") >= 0) //l == 4 && 
            //     psarcFiles = psarcFiles4.Concat(Directory.GetFiles(pathDLC, "*.psarc*", System.IO.SearchOption.AllDirectories)).ToArray();
            //else psarcFiles = psarcFiles4.Concat(Directory.GetFiles(pathDLC+"\\DLC")).ToArray();
            //for (var l = 0; l <= 5; l++)
            //{

            //    if (l == 0 && c("dlcm_FTPEU")!="" ) psarcFiles = GetFTPFiles(c("dlcm_FTPEU"));
            //    else if (l == 1 && c("dlcm_FTPUS") != "") psarcFiles = GetFTPFiles(c("dlcm_FTPUS"));
            //    else if (l == 2 && c("dlcm_FTPJP") != "") psarcFiles = GetFTPFiles(c("dlcm_FTPJP"));
            //        else if (l == 3) psarcFiles = Directory.GetFiles(pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014"), "*.psarc*", System.IO.SearchOption.AllDirectories);
            //    else if (l == 4 && pathDLC.IndexOf("\\DLC") >= 0) psarcFiles = Directory.GetFiles(pathDLC, "*.psarc*", System.IO.SearchOption.AllDirectories);
            if (psarcFiles.Count() == 0) MessageBox.Show("Nothing is available to be Loaded", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                var tz = "";
                foreach (var psarc in psarcFiles)
                {
                    if (psarc != "")
                        if (!tz.Contains(Path.GetDirectoryName(psarc))) tz += "; " + Path.GetDirectoryName(psarc);
                }
                MessageBox.Show("Based on setup (" + c("dlcm_FTPEU") + "\n" + pathDLC + "\n" + c("dlcm_FTPUS") + "\n" + c("dlcm_FTPJP") + "\n" + c("dlcm_PS4") + "\n" + c("dlcm_PC")
                    + "\n" + c("dlcm_Mac") + "\n" + c("dlcm_RocksmithDLCPath") + "\n" + pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014") + "\n) Found psarcs in the following folders" + tz.Replace("; ", "\n"), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var inputFilePath = ""; var locat = ""; var songshsanP = ""; var unpackedDir = "";
            var t = "";
            Platform platformDLC;//
            var platformDLCP = "";

            //Clean dlcpack Folders
            //Clean Temp Folder
            var oldvl = ConfigRepository.Instance()["dlcm_AdditionalManipul81"]; ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = "Yes";
            //DialogResult result2 =
            if ((chbx_CleanTemp.Checked && !GetParam(38, chbx_Additional_Manipulations)) || result1 == DialogResult.Yes) //39.Use only unpacked songs already in the 0 / dlcpacks folder                
            {
                ConfigRepository.Instance()["dlcm_MoreWEMBNK"] = "Yes";
                try
                {
                    //clear content of dlcpacks folder
                    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks");
                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                        if (dir.Name != "manipulated" && dir.Name != "manifests" && dir.Name != "temp" && dir.Name != "origs")
                        {
                            timestamp = UpdateLog(timestamp, "Deleting ...." + dir.FullName, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            DeleteDirectory(dir.FullName, false);
                        }

                    foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                        DeleteFile(file.FullName, false);

                    //clear content of dlcpacks\manipulated\temp folder
                    System.IO.DirectoryInfo downloadedMMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manipulated");
                    foreach (DirectoryInfo dir in downloadedMMessageInfo.GetDirectories())
                    {
                        timestamp = UpdateLog(timestamp, "Deleting ...." + dir.FullName, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (dir.FullName.IndexOf("manipulated\\temp") < 0) DeleteDirectory(dir.FullName, false);
                    }

                    foreach (FileInfo file in downloadedMMessageInfo.GetFiles())
                    {
                        DeleteFile(file.FullName, false);
                    }

                    //clear content of dlcpacks\temp folder
                    System.IO.DirectoryInfo downloadedMMessageInfo6 = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\temp");
                    foreach (DirectoryInfo dir in downloadedMMessageInfo6.GetDirectories())
                    {
                        timestamp = UpdateLog(timestamp, "Deleting ...." + dir.FullName, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        DeleteDirectory(dir.FullName, false);
                    }
                    foreach (FileInfo file in downloadedMMessageInfo6.GetFiles())
                    {
                        DeleteFile(file.FullName, false);
                    }

                    System.IO.DirectoryInfo downloadedMmMessageInfo = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\manifests");
                    foreach (DirectoryInfo dir in downloadedMmMessageInfo.GetDirectories())
                    {
                        timestamp = UpdateLog(timestamp, "Deleting ...." + dir.FullName, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        DeleteDirectory(dir.FullName, false);
                    }

                    foreach (FileInfo file in downloadedMmMessageInfo.GetFiles())
                    {
                        DeleteFile(file.FullName, false);
                    }

                    System.IO.DirectoryInfo downloadedMmMessageInfo7 = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks\\origs");
                    foreach (FileInfo file in downloadedMmMessageInfo7.GetFiles())
                    {
                        DeleteFile(file.FullName, false);
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    timestamp = UpdateLog(timestamp, "Error cleaning Temp Folder Cleaned", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }

            ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
            //Clean CachetDB
            if (result1 == DialogResult.Yes)
            {
                DeleteFromDB("Cache", "DELETE * FROM Cache;", cnb, cnc);
                DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE Official=\"Yes\";", cnb, cnc);
                DeleteFromDB("Tones", "DELETE * FROM Tones WHERE Official=\"Yes\"; ", cnb, cnc);
                DeleteFromDB("Tones_GearList", "DELETE * FROM Tones_GearList WHERE Official=\"Yes\"; ", cnb, cnc);
            }
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 2 * 3;
            var pic = "";
            //UNPACK x3 psarcs
            var hash = "";
            var Cashe = "";
            foreach (var psarc in psarcFiles)
            {
                if (psarc == null || psarc == "") continue;
                platformDLC = psarc.GetPlatform(); //Platform 
                platformDLCP = platformDLC.platform.ToString();
                Cashe = "";
                if (psarc.IndexOf("\\songs.psarc") >= 0 || psarc.IndexOf("\\rs1compatibilitydlc") >= 0
                    || psarc.IndexOf("\\rs1compatibilitydisc") >= 0)
                {
                    timestamp = UpdateLog(timestamp, "2/20 Decompressing  " + ".... " + psarc, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    pB_ReadDLCs.Increment(2);
                    var ps = ""; var PSACRBackupPath = "";
                    if (psarc.ToLower().IndexOf("\\ps4\\") >= 0 || psarc.ToUpper().IndexOf("CUSA00745") >= 0 || psarc.ToUpper().IndexOf("CUSA00692") >= 0)
                    {
                        ps += "PS4";
                        if (psarc.ToUpper().IndexOf("CUSA00745") >= 0) ps += "EU";
                        else if (psarc.ToUpper().IndexOf("CUSA00692") >= 0) ps += "US";
                    }

                    else if (platformDLCP.ToLower() == "mac" || platformDLC.platform.ToString().ToLower() == "mac") ps += "Mac";
                    else if (platformDLCP.ToLower() == "xbox360" || platformDLC.platform.ToString().ToLower() == "mac") ps += "XBOX360";
                    else if (platformDLCP.ToLower() == "ps3" || platformDLC.platform.ToString().ToLower() == "PS3" || psarc.ToUpper().Contains("BLES01862") || psarc.ToUpper().Contains("BLUS31182") || psarc.ToUpper().Contains("BLJM61049"))
                    {
                        ps += "PS3";
                        if (psarc.ToUpper().IndexOf("BLES01862") >= 0) ps += "EU";
                        else if (psarc.ToUpper().IndexOf("BLUS31182") >= 0) ps += "US";
                        else if (psarc.ToUpper().IndexOf("BLJM61049") >= 0) ps += "JP";
                    }
                    else if (platformDLCP.ToLower() == "pc" || platformDLC.platform.ToString().ToLower() == "pc")
                        ps += "Pc";
                    platformDLCP = ps;

                    File.Copy(psarc, txt_TempPath.Text + "\\0_dlcpacks\\origs\\" + Path.GetFileName(psarc) + ps, true);
                    if (psarc.IndexOf("songs.psarc") >= 0) //RS14 RETAIL
                    {
                        inputFilePath = psarc; locat = "CACHE" + ps;
                        t = inputFilePath;
                        if (!GetParam(38, chbx_Additional_Manipulations)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try
                            {
                                // UNPACK CACHE
                                timestamp = UpdateLog(timestamp, "3/20 Unpacking cache.psarc.... ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                var f = GetHash(psarc.Replace("songs.psarc", "cache.psarc"));
                                if (CheckAlreadyImportedCache(ps, f) && File.Exists(txt_TempPath.Text + "\\0_dlcpacks\\cache" + ps + ".psarc"))
                                    timestamp = UpdateLog(timestamp, "Ignored as imported in DB and file system.... as" + (txt_TempPath.Text + "\\0_dlcpacks\\cache" + ps + ".psarc"), true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                else
                                    if (File.Exists(psarc.Replace("songs.psarc", "cache.psarc")))
                                {
                                    try
                                    {
                                        unpackedDir = Packer.Unpack(psarc.Replace("songs.psarc", "cache.psarc"), txt_TempPath.Text + "\\0_dlcpacks\\temp", SourcePlatform, false, false); //, falseUnpack cache.psarc for RS14 Official Retails songs rePACKING
                                    }
                                    catch (Exception ex)
                                    {
                                        Platform srcPlatform = psarc.Replace("songs.psarc", "cache.psarc").GetPlatform();
                                        var startInfo = new ProcessStartInfo
                                        {
                                            FileName = Path.Combine(AppWD, "packer.exe"),
                                            WorkingDirectory = txt_TempPath.Text + "\\0_dlcpacks\\temp",
                                            Arguments = string.Format(" --unpack --version=RS2014 --platform={0} --output={1} --input={2} --decodeogg",
                                            srcPlatform,
                                            txt_TempPath.Text + "\\0_dlcpacks\\temp",
                                            psarc.Replace("songs.psarc", "cache.psarc")),
                                            UseShellExecute = false,
                                            CreateNoWindow = true //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                        };//\\cache_psarc_RS2014_PS3
                                        using (var DDC = new Process())
                                        {
                                            //try
                                            //{
                                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 15); //wait 15min
                                            if (DDC.ExitCode > 0)
                                            {
                                                //ps="PS3";
                                                unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\temp\\cache_psarc_RS2014_Pc";
                                                Directory.CreateDirectory(unpackedDir);
                                                File.Copy(psarc.Replace("songs.psarc", "cache.psarc"), unpackedDir + "\\cache.psarc", true);
                                                var startInfos = new ProcessStartInfo
                                                {
                                                    FileName = Path.Combine(AppWD, "PSArcTool.exe"),
                                                    WorkingDirectory = unpackedDir,
                                                    Arguments = " " + unpackedDir + "\\cache.psarc",
                                                    UseShellExecute = false,
                                                    CreateNoWindow = false,
                                                    RedirectStandardOutput = true,
                                                    RedirectStandardError = true
                                                };//\\cache_psarc_RS2014_PS3
                                                using (var DDdC = new Process())
                                                {
                                                    //try
                                                    //{
                                                    DDdC.StartInfo = startInfos; DDdC.Start(); DDdC.WaitForExit(1000 * 60); //wait 1min
                                                    if (DDC.ExitCode == 0)
                                                        File.Delete(unpackedDir + "\\cache.psarc");
                                                    //}
                                                    //catch (Exception eexx)
                                                    //{
                                                    else
                                                    {

                                                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                                                        System.Diagnostics.ProcessStartInfo startInfoo = new
                                                        System.Diagnostics.ProcessStartInfo();
                                                        //startInfoo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                                        startInfoo.FileName = "cmd.exe";
                                                        startInfoo.Arguments = " /C " + Path.Combine(AppWD, "PSArcTool.exe") + " " + unpackedDir + "\\cache.psarc";
                                                        process.StartInfo = startInfoo;
                                                        process.Start();
                                                        startInfoo.Arguments = " " + Path.Combine(AppWD, "PSArcTool.exe") + " " + unpackedDir + "\\cache.psarc";
                                                        process.StartInfo = startInfoo;
                                                        process.Start();
                                                    }
                                                    //timestamp = UpdateLog(timestamp, "error: " + psarc.Replace("songs.psarc", "cache.psarc") + ". " + ex, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                                    //}

                                                }
                                            }
                                            //catch (Exception exx)
                                            //{
                                            //    unpackedDir= txt_TempPath.Text + "\\0_dlcpacks\\temp\\songs_psarc_RS2014_PS3";
                                            //    Directory.CreateDirectory(unpackedDiür);
                                            //    var startInfos = new ProcessStartInfo
                                            //    {
                                            //        FileName = Path.Combine(AppWD, "PSArcTool.exe"),
                                            //        WorkingDirectory = unpackedDir,
                                            //        Arguments = string.Format(" ",
                                            //        psarc.Replace("songs.psarc", "cache.psarc")),
                                            //        UseShellExecute = false,
                                            //        CreateNoWindow = true //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                            //    };//\\cache_psarc_RS2014_PS3
                                            //    using (var DDdC = new Process())
                                            //    {
                                            //        try
                                            //        {
                                            //            DDdC.StartInfo = startInfos; DDdC.Start(); DDdC.WaitForExit(1000 * 60 * 15); //wait 15min
                                            //        }
                                            //        catch (Exception eexx)
                                            //        {
                                            //            timestamp = UpdateLog(timestamp, "error: " + psarc.Replace("songs.psarc", "cache.psarc") + ". " + ex + ". " + exx + ". " + eexx, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                            //        }
                                            //    }
                                            //}
                                        }
                                    }

                                    //check if platform is correctly identified, &if NOT, correct it
                                    var startI = new ProcessStartInfo
                                    {
                                        FileName = Path.Combine(AppWD, "..\\..\\tools\\7za.exe"),
                                        WorkingDirectory = unpackedDir
                                    };
                                    var za = unpackedDir + "\\cache8.7z";
                                    startI.Arguments = string.Format(" x {0} -o{1}",
                                                                        za,
                                                                        unpackedDir);//.Replace("\\..\\cache_psarc_RS2014_Pc", "\\..\\cache_psarc_RS2014_Pc\\manipulated"));
                                    startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                        if (DDC.ExitCode > 0)
                                        {
                                            var tsst = "Error ..." + DDC.ExitCode; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                        }
                                    }/*.Replace("\\cache_psarc_RS2014_Pc", "\\cache_psarc_RS2014_Ps3\\manipulated")*/
                                    var tmtpdir = unpackedDir + "\\audio\\ps3";
                                    if (DirectoryExists(tmtpdir))
                                    {
                                        renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_PS3" + ps));//.Replace("\\temp\\","\\"));
                                        unpackedDir = unpackedDir.Replace("_Pc", "_PS3" + ps);
                                        platformDLCP = "PS3";
                                    }//.Replace("\\cache_psarc_RS2014_Pc", "\\cache_psarc_RS2014_Pc\\manipulated")
                                    else if (DirectoryExists(unpackedDir + "\\audio\\mac"))
                                    {
                                        //tmtpdir = unpackedDir.Replace("\\cache_psarc_RS2014_Pc", "\\cache_psarc_RS2014_Pc\\manipulated") + "\\audio\\mac";
                                        renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_Mac"));//.Replace("\\temp\\", "\\"));
                                        unpackedDir = unpackedDir.Replace("_Pc", "_Mac");
                                        platformDLCP = "Mac";
                                    }/*.Replace("\\cache_psarc_RS2014_Pc", "\\cache_psarc_RS2014_Pc_Pc\\manipulated")*/

                                    else if (DirectoryExists(unpackedDir + "\\audio\\generic") || psarc.Contains("CUSA00745") || psarc.Contains("CUSA00692"))
                                    {
                                        renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_PS4"));//.Replace("\\temp\\", "\\"))
                                        unpackedDir = unpackedDir.Replace("_Pc", "_PS4");
                                        platformDLCP = "PS4";
                                    }
                                    else if (DirectoryExists(unpackedDir + "\\audio\\windows"))
                                    {
                                        //tmtpdir = unpackedDir.Replace("\\cache_psarc_RS2014_Pc", "\\cache_psarc_RS2014_Pc\\manipulated") + "\\audio\\pc";
                                        //renamedir(unpackedDir, unpackedDir.Replace("\\temp\\", "\\"));
                                        platformDLCP = "Pc";
                                    }
                                    else
                                    {
                                        var tsst = "Error ..." + unpackedDir; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                        MessageBox.Show("Platform/Import-file not supported yet: " + unpackedDir, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    locat += platformDLCP;
                                    //clear temp cache_Pc folder
                                    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(unpackedDir);
                                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                                    {
                                        if (dir.Name == "manipulated") DeleteDirectory(dir.FullName, false);
                                    }

                                    //move cache_xx to dlcpacks
                                    if (DirectoryExists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks") + ps)) DeleteDirectory(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"), false);
                                    renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks") + ps);
                                    unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks") + ps;
                                    Cashe = unpackedDir;
                                    File.Copy(psarc.Replace("songs.psarc", "cache.psarc"), txt_TempPath.Text + "\\0_dlcpacks\\cache" + ps + ".psarc", true);
                                    PSACRBackupPath = txt_TempPath.Text + "\\0_dlcpacks\\cache" + ps + ".psarc";
                                }

                                //Process SONGS.PSARC
                                var h = GetHash(psarc);
                                if (CheckAlreadyImportedRetail(ps, h) && File.Exists(txt_TempPath.Text + "\\0_dlcpacks\\songs" + ps + ".psarc"))
                                {
                                    timestamp = UpdateLog(timestamp, "Ignored as imported in DB and file system.... " + psarc, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    continue;
                                }

                                timestamp = UpdateLog(timestamp, "4/20 Unpacking & ogging songs.psarc.... ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                var SourcePlatfor = inputFilePath.GetPlatform();
                                if (psarc != pathDLC + "\\songs.psarc.edat") unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", SourcePlatfor, true, false);

                                //FIX for unpacking w the wrong folder extension
                                //And unpacking of PS3 WEM
                                if (DirectoryExists(unpackedDir + "\\songs\\bin\\ps3") && !DirectoryExists(unpackedDir) || (File.Exists(inputFilePath) && psarc == pathDLC + "\\songs.psarc.edat"))/*|| */
                                {
                                    var startInfo = new ProcessStartInfo
                                    {
                                        FileName = Path.Combine(AppWD, "packer.exe"),
                                        WorkingDirectory = unpackedDir,
                                        Arguments = string.Format(" --unpack --version=RS2014 --platform={0} --output={1} --input={2} --decodeogg",
                                                                        platformDLCP,
                                                                        unpackedDir.Replace("songs_Pc", "") + ps,
                                                                        inputFilePath),
                                        UseShellExecute = false,
                                        CreateNoWindow = true //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                    };

                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 15); //wait 15min                                                                                                               
                                    }
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_ps3" + ps));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_ps3" + ps);
                                    platformDLCP = "PS3";
                                }
                                //else if (psarc != pathDLC + "\\songs.psarc.edat")
                                //    timestamp = UpdateLog(timestamp, "4/20 Unpacking songs.psarc.... ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); 
                                //unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks\\temp", SourcePlatform, true, false);

                                if (DirectoryExists(unpackedDir + "\\songs\\bin\\macos"))
                                {
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_Mac"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_Mac");
                                    platformDLCP = "Mac";
                                }
                                if (DirectoryExists(unpackedDir + "\\audio\\generic"))
                                {
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_PS4"));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_Ps4");
                                    platformDLCP = "PS4";
                                }
                                if (DirectoryExists(unpackedDir + "\\audio\\ps3"))
                                {
                                    renamedir(unpackedDir, unpackedDir.Replace("_Pc", "_PS3" + ps));
                                    unpackedDir = unpackedDir.Replace("_Pc", "_PS3" + ps);
                                    platformDLCP = "PS3";
                                }

                                //move songs_xx to dlcpacks
                                if (DirectoryExists(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"))) DeleteDirectory(unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"), false);
                                renamedir(unpackedDir, unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks"));
                                unpackedDir = unpackedDir.Replace("\\0_dlcpacks\\temp", "\\0_dlcpacks");

                                File.Copy(psarc, txt_TempPath.Text + "\\0_dlcpacks\\songs" + ps + ".psarc", true);
                                songshsanP = File.Exists(unpackedDir + "\\manifests\\songs\\songs" + ps + ".hsan") ? unpackedDir + "\\manifests\\songs\\songs" + ps + ".hsan" : unpackedDir + "\\manifests\\songs\\songs.hsan";
                                PSACRBackupPath += ";" + txt_TempPath.Text + "\\0_dlcpacks\\songs" + ps + ".psarc";
                                hash = h;
                            }
                            catch (Exception ex) { timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + inputFilePath + "---" + txt_TempPath.Text + "\\0songs", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
                        }
                        else
                        {
                            unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\songs_" + ps;
                            songshsanP = unpackedDir + "\\manifests\\songs\\songs" + ps + ".hsan";
                        }

                        timestamp = UpdateLog(timestamp, "5/20 Processed cache.psarc & songs.psarc", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    } //repacking at the moment manually with psarc 1.4 and lzma ratio 0
                    else if (psarc.IndexOf("rs1compatibilitydlc") >= 0) //RS12 DLC|| psarc.IndexOf("rs1compatibilitydlc") >= 0
                    {
                        inputFilePath = psarc;
                        locat = "COMPATIBILITY" + ps + platformDLCP;
                        var h = GetHash(psarc);
                        if (CheckAlreadyImportedRetail(ps, h) && File.Exists(txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc" + ps + ".psarc")) continue;

                        if (!GetParam(38, chbx_Additional_Manipulations)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try // UNPACK
                            {
                                unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", SourcePlatform, false, false).Replace(".psarc", ""); //, false;
                            }
                            catch (Exception ex) { timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + unpackedDir, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc_" + platformDLCP;


                        timestamp = UpdateLog(timestamp, "6/20 Repacking " + psarc + "2 use the internal/Browser Psarc Read function.... ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                            renamedir(unpackedDir, unpackedDir + ps); unpackedDir = unpackedDir + ps;
                            renamedir(unpackedDir + "\\manifests\\songs_rs1dlc", unpackedDir + "\\manifests\\songs");

                            var startInfo = new ProcessStartInfo
                            {
                                FileName = Path.Combine(AppWD, "psarc.exe"),
                                WorkingDirectory = unpackedDir
                            };
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc" + ps + ".psarc";
                            PSACRBackupPath = t;
                            hash = h;
                            startInfo.Arguments = string.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 5); //wait 5min
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1dlc DLC pack !", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            timestamp = UpdateLog(timestamp, "7/20 renaming internal folder ", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                        }
                        catch (Exception ex)
                        {
                            timestamp = UpdateLog(timestamp, ex.Message + "Error at dir rename" + unpackedDir, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                        timestamp = UpdateLog(timestamp, "8/20 Processed rs1compatibilitydlc.psarc", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    else if (psarc.IndexOf("rs1compatibilitydisc") >= 0) //RS12 RETAIL
                    {
                        inputFilePath = psarc; locat = "RS1Retail" + ps + platformDLCP;
                        var h = GetHash(psarc);
                        if (CheckAlreadyImportedRetail(ps, h) && File.Exists(txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc" + ps + ".psarc")) continue;
                        if (!GetParam(38, chbx_Additional_Manipulations)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                        {
                            try // UNPACK
                            {
                                if (platformDLCP == "PS3")
                                {
                                    //Packer.Unpack fails
                                    var startInfo = new ProcessStartInfo
                                    {
                                        FileName = Path.Combine(AppWD, "packer.exe"),
                                        WorkingDirectory = unpackedDir,
                                        Arguments = string.Format(" --unpack --decodeogg --version=RS2014 --platform={0} --output={1} --input={2}",
                                                                        platformDLCP,
                                                                        txt_TempPath.Text + "\\0_dlcpacks",
                                                                        inputFilePath),
                                        UseShellExecute = false,
                                        CreateNoWindow = true
                                    };

                                    using (var DDC = new Process())
                                    {
                                        DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 1min
                                        if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Error when packing rs1dlc DLC pack !", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    }
                                    unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                }
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", SourcePlatform, true, false);
                            }
                            catch (Exception ex)
                            {
                                timestamp = UpdateLog(timestamp, ex.Message + "Error at unpacking" + unpackedDir, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_" + platformDLC;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                        timestamp = UpdateLog(timestamp, "9/20 Repacking " + psarc + " 2 use the internal/Browser Psarc Read function.... " + psarc, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        try //rename folder so we can use the read browser function                            
                        {
                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work

                            renamedir(unpackedDir, unpackedDir + ps); unpackedDir = unpackedDir + ps;
                            renamedir(unpackedDir + "\\manifests\\songs_rs1disc", unpackedDir + "\\manifests\\songs");
                            songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                            var startInfo = new ProcessStartInfo
                            {
                                FileName = Path.Combine(AppWD, "psarc.exe"),
                                WorkingDirectory = unpackedDir
                            };
                            t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc" + ps + ".psarc";
                            PSACRBackupPath = t;
                            hash = h;
                            startInfo.Arguments = string.Format(" create --zlib -N -o {0} {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 20); //wait 10min
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Error when packing rs1disc pack !", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                                renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                                timestamp = UpdateLog(timestamp, "10/20 renaming internal folder", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }

                            if (platformDLCP == "PS3")
                            {
                                //commenting next line altough owrknig to use the official packer
                                var unpackedDir1 = unpackedDir;
                                var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", System.IO.SearchOption.AllDirectories);
                                var i = 0;
                                foreach (var wem in wemFiles)
                                {
                                    i++;
                                    startInfo = new ProcessStartInfo
                                    {
                                        FileName = Path.Combine(AppWD, "ww2ogg.exe"),
                                        WorkingDirectory = AppWD,
                                        Arguments = string.Format(" {0} -o {1} --pcb packed_codebooks_aoTuV_603.bin",
                                                                        wem,
                                                                        wem.Replace(".wem", "_fixed.ogg")),
                                        UseShellExecute = false,
                                        CreateNoWindow = true
                                    };

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
                            timestamp = UpdateLog(timestamp, ex.Message + "Error at dir rename" + unpackedDir, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        timestamp = UpdateLog(timestamp, "11/20 Processed rs1compatibilitydisc.psarc", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    else continue;
                    //Console.WriteLine("Opening archive {0} ...", inputFilePath);
                    //Console.WriteLine();

                    importlist += "\n" + locat;

                    //Populate DB
                    timestamp = UpdateLog(timestamp, "18/20 Populating CACHE DB", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var browser = new PsarcBrowser(t);
                    var songlist = browser.GetSongList();
                    var toolkitInfo = browser.GetToolkitInfo();
                    var AudioP = "";
                    var AudioPP = "";
                    //load to rename songs
                    try
                    {

                        timestamp = UpdateLog(timestamp, "load Folder to rename songs.", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var infoS = DLCPackageData.LoadFromFolder(unpackedDir, unpackedDir.GetPlatform());
                        //noarg++;
                    }
                    catch (Exception ex)
                    {
                        var tsst = "rror at Loading Arangements of REtaial packs ..." + ex.Message.Replace("Error", "rror").Replace("ERROR", "RROR").Replace("error", "rror");
                        if (ConfigRepository.Instance()["dlcm_MoreWEMBNK"] != "Yes" && (ex.Message.ToString().ToLower().IndexOf("wem") >= 0 || ex.Message.ToString().ToLower().IndexOf("bnk") >= 0)) tsst = "E" + tsst;
                        timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    }

                    foreach (var song in songlist)
                    {
                        var AudioP1 = "song_" + song.Identifier;
                        var AudioPP1 = "song_" + song.Identifier + "_preview";
                        cnt++;

                        if (locat.IndexOf("RS1Retail") >= 0)
                        {
                            pic = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds");
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioP = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\")
                                    + AudioP1 + (platformDLCP == "PS3" ? "_fixed.ogg" : "_fixed.ogg");
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioPP = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\")
                                    + AudioPP1 + (platformDLCP == "PS3" ? "_fixed.ogg" : "_fixed.ogg");
                        }
                        else if (locat.IndexOf("COMPATIBILITY") >= 0)
                        {
                            pic = songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds");
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioP = (songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\")
                                    + AudioP1 + "_fixed.ogg").Replace("rs1compatibilitydlc", "songs").Replace("_p_Pc", "_Pc").Replace("_p_Pc", "_Mac").Replace("m_Mac", "Mac");
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioPP = (songshsanP.Replace("\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan", "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\")
                                    + AudioPP1 + "_fixed.ogg").Replace("rs1compatibilitydlc", "songs").Replace("_p_Pc", "_Pc").Replace("_p_Pc", "_Mac").Replace("m_Mac", "Mac");
                        }
                        else if (locat.IndexOf("CACHE") >= 0)
                        {
                            //var folder = File.Exists(unpackedDir + "\\manifests\\songs\\songs" + ps + ".hsan") ? unpackedDir + "\\manifests\\songs\\songs" + ps + ".hsan" : unpackedDir + "\\manifests\\songs\\songs.hsan";
                            pic = unpackedDir + "\\gfxassets\\album_art\\album_" + song.Identifier + "_256.dds";
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioP = unpackedDir + "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\" + AudioP1 + "_fixed.ogg";
                            if (Directory.Exists(unpackedDir + "\\audio")) AudioPP = unpackedDir + "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP))) + "\\" + AudioPP1 + "_fixed.ogg";/*_fixed*/
                        }
                        else
                        {
                            timestamp = UpdateLog(timestamp, "error on known psarc", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            break;
                        }
                        var et = "rror WEM Issues";
                        if (!File.Exists(AudioP))/*platformDLCP == "PS4"*/
                        {
                            DataSet dsx = new DataSet(); dsx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID, Identifier from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "\"", txt_DBFolder.Text, cnb, cnc);
                            //var aah = dsx.Tables.Count >= 0 ? "" : (dsx.Tables[0].Rows.Count > 0 ? "" : dsx.Tables[0].Rows[0].ItemArray[0]);
                            AudioP = (GetNoRec(dsx, cnb, cnc) > 0 ? (AudioP = unpackedDir + "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP)))) + "\\" + dsx.Tables[0].Rows[0].ItemArray[0].ToString() : "") + "_fixed.ogg";
                            /*dsx.Tables[0].Rows.Count*/
                        }
                        if (!File.Exists(AudioP))  //COMPATIBILITY doenst have any audio ..ti investigate maybve reference it from Main.db or check the value oif this file where files are newer better etc.
                        {
                            if (locat.IndexOf("CACHE") < 0 && locat.IndexOf("COMPATIBILITY") < 0 && song.Identifier.IndexOf("rs1") < 0 && song.Identifier.IndexOf("rs2") < 0) et = "e" + et;
                            timestamp = UpdateLog(timestamp, et + " " + AudioP + " " + song.Identifier, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            AudioP = "";
                        }

                        if (!File.Exists(AudioPP))/*platformDLCP == "PS4"*/
                        {
                            DataSet dsdx = new DataSet(); dsdx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "_preview\"", txt_DBFolder.Text, cnb, cnc);
                            AudioPP = (GetNoRec(dsdx, cnb, cnc) > 0 ? (AudioPP = unpackedDir + "\\audio\\"
                                + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP == "Xbox360" ? "xbox360" : (platformDLCP == "PS4" ? "generic" : platformDLCP)))) + "\\" + dsdx.Tables[0].Rows[0].ItemArray[0].ToString() : "") + "_fixed.ogg";
                            /*dsdx.Tables[0].Rows.Count*/
                        }
                        if (!File.Exists(AudioPP)) //COMPATIBILITY doenst have any audio ..ti investigate maybve reference it from Main.db or check the value oif this file where files are newer better etc.
                        {
                            if (locat.IndexOf("CACHE") < 0 && locat.IndexOf("COMPATIBILITY") < 0 && song.Identifier.IndexOf("rs1") < 0 && song.Identifier.IndexOf("rs2") < 0) et = "e" + et;
                            timestamp = UpdateLog(timestamp, et + " " + AudioPP + " preview " + song.Identifier, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            AudioPP = "";
                        }
                        //convert to png
                        if (!File.Exists(pic))
                        {
                            timestamp = UpdateLog(timestamp, "rror PIC Issues" + " " + pic + " " + song.Identifier, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            pic = "";
                        }
                        else ExternalApps.Dds2Png(pic);

                        DataSet dtx = new DataSet(); dtx = SelectFromDB("Cache", "SELECT ID from Cache AS O WHERE Platform=\"" + platformDLCP + "\" AND Identifier=\"" + song.Identifier.ToString() + "\"", txt_DBFolder.Text, cnb, cnc);
                        //var aa = dtx.Tables[0].Rows.Count;
                        if (GetNoRec(dtx, cnb, cnc) == 0) //If this record isn't already in the DB...add it ---dtx.Tables[0].Rows.Count

                        //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBb_Path))
                        {
                            var commands = cnb.CreateCommand();
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
                            commands.CommandText += "AudioPreviewPath, ";
                            commands.CommandText += "PS3Region, ";
                            commands.CommandText += "PSACRBackupPath, ";
                            commands.CommandText += "AudioHash, ";
                            commands.CommandText += "AudioPreviewHash, ";
                            commands.CommandText += "ArtHash, ";
                            commands.CommandText += "PSARCHash, ";
                            commands.CommandText += "AudioPathWEM, ";
                            commands.CommandText += "AudioPreviewPathWEM, ";
                            commands.CommandText += "AlbumSort, ";
                            commands.CommandText += "CACHEPsarcName ";
                            commands.CommandText += ") VALUES (@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13" +
                                ",@param14,@param15,@param16,@param17,@param18,@param19,@param20,@param21,@param22,@param23,@param24,@param25"; ////@param1,
                            commands.CommandText += ")";
                            //var a = GetParam(19, chbx_Additional_Manipulations);
                            commands.Parameters.AddWithValue("@param2", song.Identifier);
                            commands.Parameters.AddWithValue("@param3", song.Artist);
                            commands.Parameters.AddWithValue("@param4", song.ArtistSort);
                            commands.Parameters.AddWithValue("@param5", song.Album);
                            commands.Parameters.AddWithValue("@param6", song.Title);
                            commands.Parameters.AddWithValue("@param7", song.Year);
                            commands.Parameters.AddWithValue("@param8", string.Join(", ", song.Arrangements));
                            commands.Parameters.AddWithValue("@param9", GetParam(19, chbx_Additional_Manipulations) ? "Yes" : "No");
                            commands.Parameters.AddWithValue("@param10", pic);//.Replace(".dds", ".png")
                            commands.Parameters.AddWithValue("@param11", locat);
                            commands.Parameters.AddWithValue("@param12", songshsanP);
                            commands.Parameters.AddWithValue("@param13", platformDLCP);
                            commands.Parameters.AddWithValue("@param14", AudioP);
                            commands.Parameters.AddWithValue("@param15", AudioPP);
                            commands.Parameters.AddWithValue("@param16", ps.IndexOf("PS3") < 0 ? "" : ps.Replace("PS3", ""));
                            commands.Parameters.AddWithValue("@param17", PSACRBackupPath);
                            commands.Parameters.AddWithValue("@param18", GetHash(AudioP.Replace("_fixed.ogg", ".wem")));
                            commands.Parameters.AddWithValue("@param19", GetHash(AudioPP.Replace("_fixed.ogg", ".wem")));
                            commands.Parameters.AddWithValue("@param20", GetHash(pic));
                            commands.Parameters.AddWithValue("@param21", hash);
                            commands.Parameters.AddWithValue("@param22", AudioP.Replace("_fixed.ogg", ".wem"));
                            commands.Parameters.AddWithValue("@param23", AudioPP.Replace("_fixed.ogg", ".wem"));
                            commands.Parameters.AddWithValue("@param24", song.Album);
                            commands.Parameters.AddWithValue("@param24", Cashe);
                            //commands.Parameters.AddWithValue("@param24", song.Identifier);song.
                            //EXECUTE SQL/INSERT
                            commands.CommandType = CommandType.Text;
                            UpdateDBbyExecuteNonQuery(commands, cnb, cnc);
                            //try
                            //{
                            //    cnn.Open();
                            //    commands.ExecuteNonQuery();
                            //    cnti++;
                            //}
                            //catch (Exception ex)
                            //{
                            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DBb_Path + "-" + commands.CommandText);
                            //    errsongs += ";" + song.Title;
                            //    throw;
                            //}
                            //finally
                            //{
                            //    if (connection != null) connection.Close();
                            //}
                            //timestamp = UpdateLog(timestamp, song.Artist + "-" + song.Title, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                    }

                    timestamp = UpdateLog(timestamp, "12/20 Load for arrangements read", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    //cnb.Close();
                    //cnb.Open();
                    //var fcmd = "SELECT TOP 1 ID FROM Cache WHERE File_Hash=\"" + hash + "\" ORDER BY ID DESC";
                    //DataSet dus = new DataSet(); 
                    //    dus = SelectFromDB("Cache", fcmd, txt_DBFolder.Text, cnb, cnc);
                    ////Useful
                    //// Get and Store IDENTITY (Primary Key) for further
                    //// INSERTS in child table [Order Details]
                    ////cmd.CommandText = "SELECT @@identity";
                    ////string id = cmd.ExecuteScalar().ToString();
                    ////objAdapter.Fill(dus, "Main");
                    ////string strID = dus.Tables["Main"].Rows[0].ToString();
                    //CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                    //Load arrangements
                    DLCPackageData info = null;
                    var er = false;
                    if (Directory.Exists(unpackedDir + "\\audio"))
                        try
                        {
                            info = DLCPackageData.LoadFromFolder(unpackedDir, unpackedDir.GetPlatform());
                            noarg++;
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Erro at Loading Arangements of REtaial packs ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            er = true;
                            errargs = ";" + unpackedDir;
                        }
                    else er = true;
                    timestamp = UpdateLog(timestamp, "12/20 End Load for arrangements read", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (!er)
                    {
                        //cnb.Close();
                        //cnb.Open();

                        //var fcmd = "SELECT TOP 1 ID FROM Cache WHERE PSARCHash=\"" + hash + "\" ORDER BY ID DESC";
                        //DataSet dus = new DataSet(); var norec = 0;
                        ////while (norec == 0)
                        ////{
                        //dus = SelectFromDB("Cache", fcmd, txt_DBFolder.Text, cnb, cnc);
                        //norec = dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
                        ////}
                        ////Useful
                        //// Get and Store IDENTITY (Primary Key) for further
                        //// INSERTS in child table [Order Details]

                        ////SqlCommand cmd = new SqlCommand();
                        ////cmd.CommandText = "SELECT @@identity";
                        ////using (OleDbDataAdapter da = new OleDbDataAdapter(cmd, cnb))
                        ////    try
                        ////    {
                        ////        DataSet dus = new DataSet();
                        ////        string id = cmd.ExecuteScalar().ToString();
                        ////da.Fill(dus, "Main");
                        ////string strID = dus.Tables["Main"].Rows[0].ToString();

                        ////    }
                        ////    catch (Exception ex) { ; }

                        ////var norecf = dus.Tables[0].Rows[0].ItemArray[0];
                        //if (norec == 0) return;
                        //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                        var FullPath = inputFilePath;
                        var platform = FullPath.GetPlatform();
                        var platformTXT = FullPath.GetPlatform().platform.ToString();
                        var DD = "No";
                        var Bass_Has_DD = "No";
                        var broken_Path_Import = c("dlcm_TempPath") + "\\0_broken";
                        var tst = "";

                        //Get TrackNo
                        var trackno = 0;
                        var SpotifySongID = "";
                        var SpotifyArtistID = "";
                        var SpotifyAlbumID = "";
                        var SpotifyAlbumURL = "";
                        var SpotifyAlbumPath = "";
                        var SpotifyAlbumYear = "";
                        var ybAddress = "";
                        var ybLAddress = "";
                        var ybBAddress = "";
                        var ybRAddress = "";
                        var ybCAddress = "";
                        var ybSAddress = "";

                        //REUSING CODE FROM Import normal DLCs
                        //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                        var xmlFiles = Directory.GetFiles(unpackedDir + (platformTXT == "XBox360" ? "\\Root" : "") + "\\songs", "*.xml", System.IO.SearchOption.AllDirectories);
                        List<string> clist = new List<string>();//LastConversionDateTime
                        List<string> dlist = new List<string>();//sections
                        List<string> elist = new List<string>(); //Part flag
                        //List<string> /*hlist*/ = new List<string>();
                        string PArt = "0"; var LastConversionDateTime = "";
                        string MaxDD = "0"; k = -1;
                        DateTime myOldDate;
                        DateTime myNewDate;
                        string datemax = "12-13-11 13:11";
                        string dateold = "12-13-11 13:11";

                        CultureInfo enUS = new CultureInfo("en-US");
                        myNewDate = DateTime.ParseExact(datemax, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                        myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);

                        //foreach (var xml in xmlFiles)
                        //{
                        // READ ARRANGEMENTS
                        var Lead = "No";
                        var Bass = "No";
                        var Vocalss = "No";
                        var Guitar = "No";
                        var Rhythm = "No";
                        var Combo = "No";
                        var PluckedType = "";
                        var Tunings = "";
                        var PitchShiftableEsOrDd = "";
                        //var Import_AuditTrail_ID = "";
                        var bonus = "No";
                        List<string> xmlhlist = new List<string>();
                        List<string> jsonhlist = new List<string>();
                        List<string> cxmlhlist = new List<string>();//cleaned XMLhash
                        List<string> snghlist = new List<string>();//JSON/Tone hash
                        var SongLenght = "";
                        var maxarnglenght = 0;
                        foreach (var arg in info.Arrangements)
                        {
                            var xml = arg.SongXml.File;
                            k++;

                            clist.Add("");
                            dlist.Add("");
                            elist.Add("");
                            //hlist.Add("");

                            elist[k] = GetPart(xml);

                            var s1 = xml;
                            xmlhlist.Add(GetHash(xml));
                            cxmlhlist.Add(GetHashCleanXML(xml));

                            if (GetParam(36, chbx_Additional_Manipulations) && xml != "") //37. Keep the Uncompressed Songs superorganized                                
                                s1 = (xml.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                            else if (xml != "")
                                s1 = xml.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                            jsonhlist.Add(GetHash(s1));

                            if (GetParam(36, chbx_Additional_Manipulations) && xml != "") //37. Keep the Uncompressed Songs superorganized                                
                                s1 = (xml.Replace(".xml", ".sng").Replace("\\EOF\\", "\\Toolkit\\"));
                            else if (xml != "")
                                s1 = arg.SongXml.File.Replace(".xml", ".sng").Replace("\\songs\\arr", "\\" + calc_path_sng(Directory.GetFiles(unpackedDir, "*.sng", System.IO.SearchOption.AllDirectories)[0]));
                            snghlist.Add(GetHash(s1));


                            // 

                            //if ()
                            //{
                            //    dlist[k] = "No"; continue;
                            //}

                            //     cmd = "SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate,i.Pack, i.FileCreationDate, i.ID, i.Invalid FROM Import as i";
                            //cmd += " ORDER BY LEN(FileName) ASC, i.Platform='Pc' ASC ,i.Platform='Mac' ASC,i.Platform='PS3' ASC, i.FileName ASC;";

                            if (arg.BonusArr) bonus = "Yes";
                            if (arg.ArrangementType == ArrangementType.Vocal) Vocalss = "Yes";

                            if (arg.ArrangementType == ArrangementType.Vocal || arg.ArrangementType == ArrangementType.ShowLight)
                            { dlist[k] = "No"; clist[k] = ""; continue; }
                            platform.version = RocksmithToolkitLib.GameVersion.RS2014;

                            clist[k] = GetLastConversionDateTime(xml, timestamp, broken_Path_Import, inputFilePath
                                , FullPath, null, unpackedDir);

                            if (clist[k].Length > 3)
                                if (DateTime.ParseExact(clist[k], "MM-dd-yy HH:mm", enUS) > DateTime.ParseExact(datemax, "MM-dd-yy HH:mm", enUS))
                                    datemax = clist[k];

                            //if (attr.Sections.Count < 2) sect1on = "No";
                            //else sect1on = "Yes" + attr.Sections.Count.ToString();

                            //dlist[k] = (attr.Sections.Count > 0 ? "Yes" + attr.Sections.Count : "No");
                            //}
                            //else
                            //{
                            if (clist[k] == "")
                            {
                                timestamp = UpdateLog(timestamp, "13/20 no section/lastconvdate", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                dlist[k] = "No";
                            }
                            //clist[k] = ""; ;
                            //}
                            //var lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File).Trim(' '); ;
                            ////var lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile).Trim(' '); ;
                            //if (lastConverjsonDateTime_cur.Length > 3)
                            //{
                            //    if (lastConverjsonDateTime_cur.IndexOf("-") == 1) lastConverjsonDateTime_cur = "0" + lastConverjsonDateTime_cur;
                            //    if (lastConverjsonDateTime_cur.IndexOf("-", 3) == 4) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 3) + "0" + lastConverjsonDateTime_cur.Substring(3, ((lastConverjsonDateTime_cur.Length) - 3));
                            //    if (lastConverjsonDateTime_cur.IndexOf(":") == 10) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 9) + "0" + lastConverjsonDateTime_cur.Substring(9, lastConverjsonDateTime_cur.Length - 9);
                            //}
                            //hlist[k] = lastConverjsonDateTime_cur;

                            //MAximum difficulty/dinamic Difficulty level
                            MaxDD = GetMaxDifficulty(xml, timestamp, broken_Path_Import, inputFilePath
                                , FullPath, null, unpackedDir);
                            if (MaxDD.ToInt32() >= 1) DD = "Yes"; //1 should still be multiGTet

                            //Bass_Has_DD
                            if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && MaxDD.ToInt32() >= 1)
                                Bass_Has_DD = "Yes";
                            //var manifestFunctions1 = new ManifestFunctions(platform.version);
                            //xmlContent = null;
                            //try
                            //{
                            //    xmlContent = Song2014.LoadFromFile(xml);
                            //    if (xmlContent.Arrangement.ToLower() == "bass")
                            //    {
                            //        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                            //        if (manifestFunctions1.GetMaxDifficulty(xmlContent) >= 1)
                            //            Bass_Has_DD = "Yes";
                            //    }
                            //}
                            //catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                            //}
                            tst = "14/20 end xml readout..." + k + "/" + info.Arrangements.Count + ". " + arg.SongXml.Name; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                            if (arg.SongXml.File == null || arg.SongXml.File == "") if (arg.SongXml.File.Length > maxarnglenght) maxarnglenght = arg.SongXml.File.Length; //Calculating the longest filepath(usualy  

                            if (arg.ArrangementType == ArrangementType.Guitar)
                            {
                                Guitar = "Yes";
                                if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                                else Tunings = arg.Tuning;
                                if (arg.Tuning == "Eb" || arg.Tuning == "Eb Standard" || arg.Tuning == "D Standard" || arg.Tuning == "C# Standard" || arg.Tuning == "C Standard"
                                    || arg.Tuning == "B Standard" || arg.Tuning == "Bb Standard" || arg.Tuning == "AStandard" || arg.Tuning == "AbStandard"
                                    || arg.Tuning == "Eb Drop Db" || arg.Tuning == "D Drop C" || arg.Tuning == "C#DropB" || arg.Tuning == "C Drop A#" || arg.Tuning == "B Drop A"
                                    || arg.Tuning == "BbDropAb" || arg.Tuning == "A Drop G") PitchShiftableEsOrDd = "Yes";

                                if (arg.ArrangementName == ArrangementName.Lead) Lead = "Yes";
                                else if (arg.ArrangementName == ArrangementName.Rhythm) Rhythm = "Yes";
                                else if (arg.ArrangementName == ArrangementName.Combo) Combo = "Yes";
                                Song2014 xmlContent = null;
                                try
                                {
                                    xmlContent = Song2014.LoadFromFile(arg.SongXml.File);
                                    SongLenght = xmlContent.SongLength.ToString();
                                }
                                catch (Exception ex)
                                {
                                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                    errargs += ";" + xml;
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
                                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); errargs += ";" + xml; }
                            }

                        }
                        //Check Tones
                        var Tones_Custom = "No";
                        foreach (var tn in info.TonesRS2014)
                        {
                            if (tn.IsCustom)
                                Tones_Custom = "Yes";
                            notones++;
                        }
                        tst = "15/20 end Arrangements and Tones readout..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        if (netstatus == "OK")
                        {
                            netstatus = UtilitiesFunctions.CheckIfConnectedToSpotify().Result.ToString();
                            if (netstatus == "OK" && GetParam(41, chbx_Additional_Manipulations))
                            {
                                tst = "16/20 startgetting cover spotify..."; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                                trackno = sptyfy.Result.Split(';')[0].ToInt32();
                                SpotifySongID = sptyfy.Result.Split(';')[1];
                                SpotifyArtistID = sptyfy.Result.Split(';')[2];
                                SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                                SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                                SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                                SpotifyAlbumYear = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";
                                tst = "17/20 end get track no from spotify..." + SpotifyAlbumPath; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                            string yAddress = null;
                            try
                            {
                                MainDBfields SongRecord = new MainDBfields
                                {
                                    Album = info.SongInfo.Album
                                };
                                ; SongRecord.Artist = info.SongInfo.Artist; SongRecord.Song_Title = info.SongInfo.SongDisplayName;
                                SongRecord.Has_Lead = (Lead != "" ? Lead : "No"); SongRecord.Has_Rhythm = (Rhythm != "" ? Rhythm : "No"); SongRecord.Has_Bass = Bass;
                                SongRecord.Has_Combo = (Combo != "" ? Combo : "No");
                                if (yAddress != null)
                                {
                                    ybAddress = yAddress.Split(';')[0];
                                    ybLAddress = yAddress.Split(';')[1];
                                    ybBAddress = yAddress.Split(';')[2];
                                    ybRAddress = yAddress.Split(';')[3];
                                    ybCAddress = yAddress.Split(';')[4];
                                    ybSAddress = yAddress.Split(';')[5];
                                }
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var erf in ex.InnerExceptions)
                                    tst = "error get track no from spotify..." + erf.Message; timestamp = UpdateLog(timestamp, tst, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                        }
                        ExistingTrackNo = "";

                        var dataPath = c("dlcm_TempPath") + "\\0_data";
                        //var namernd = CDLC_ID;
                        var Rebuild = ConfigRepository.Instance()["dlcm_AdditionalManipul106"] == "Yes" ? true : false;
                        var norm_path = unpackedDir;// GetNormPath(, c("dlcm_TempPath"), dataPath, platformTXT, info, Is_Original, "", trackno, "");

                        //UPDATE ArarngementsDB
                        //xmlhlist, jsonhlist, hlist, dlist, snghlist, cxmlhlist, elist, ybLAddress, ybBAddress, ybRAddress, ybCAddressMaxDD
                        //connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBb_Path); //+ ";Persist Security Info=False"
                        AddArrangements(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, clist, dlist, snghlist, cxmlhlist, elist, ybLAddress, ybBAddress
                                , ybRAddress, ybCAddress, MaxDD, norm_path, Rebuild, platformTXT, unpackedDir, cnb, info, "", "Yes", cnc);
                        //if (connection != null) connection.Close();//some issues with saving and reading such records

                        //UPDATE TonesDB
                        //clist
                        //connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBb_Path); //+ ";Persist Security Info=False"
                        AddTones(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, clist, dlist, snghlist, cxmlhlist, elist
                            , MaxDD, norm_path, Rebuild, platformTXT, unpackedDir, cnb, info, "", clist, mutit, "Yes", cnc);
                        //if (connection != null) connection.Close();//some issues with saving and reading such records

                        //timestamp = UpdateLog(timestamp, "Finish Loading for arrangements read", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //onnection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                        //AddTones(timestamp, null, "DLCManager", pB_ReadDLCs, xmlhlist, jsonhlist, hlist, dlist, snghlist, cxmlhlist, elist, ybLAddress, ybBAddress, ybRAddress, ybCAddress, MaxDD, norm_path
                        //    , Rebuild, platformTXT, unpackedDir, connection, info, CDLC_ID, clist, mutit, "No");
                        //if (connection != null) connection.Close();//some issues with saving and reading such records

                        //Updating the Standardization table
                        DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\""
                            + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", txt_DBFolder.Text, cnb, cnc);

                        if (GetNoRec(dzs, cnb, cnc) == 0)/*dzs.Tables[0].Rows.Count*/
                        {
                            var insertcmdd = "Artist, Album, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Year_Correction";
                            var insertvalues = "\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\",\"" + SpotifyArtistID + "\",\"" + SpotifyAlbumID
                                + "\",\"" + SpotifyAlbumURL + "\",\"" + SpotifyAlbumPath + "\",\"" + SpotifyAlbumYear + "\"";
                            InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb, mutit, cnc);
                        }
                        timestamp = UpdateLog(timestamp, "done", true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    }

                }
                else
                {
                    timestamp = UpdateLog(timestamp, "Ignored as not a relevant/supported for manipualtion PSARC.... " + psarc, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    continue;
                }//END no cache.psarc to be decompressed
                 //}
            }
            timestamp = UpdateLog(timestamp, "19/20 Ending Retail Songs processing ...." + DateTime.Now, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Show Intro database window
            //cnb.Close();
            Cache frm = new Cache(DBb_Path, txt_TempPath.Text, pathDLC, GetParam(39, chbx_Additional_Manipulations), GetParam(40, chbx_Additional_Manipulations), cnb, cnc);
            if (GetParam(77, chbx_Additional_Manipulations)) frm.Show();
            MessageBox.Show("Load of Retail(s) Set of Songs DONE: " + importlist + "\n\nStats:\nNo. songs: " + cnti + "\nNo. PSARCs collections with Arangements: " + noarg + "\nNo. Arangements: " + k + "\nNo Tones: " + notones
                + "\n\nErrors\nErrors in songs: " + errsongs + "\nErrors in Arrangements: " + errargs, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        public void ConvertWEM(object sender, DoWorkEventArgs e)
        {
            var wemFiles = Directory.GetFiles(unpackedDir1, "*.wem", System.IO.SearchOption.AllDirectories);
            var i = 0;
            foreach (var wem in wemFiles)
            {

                i++;
                var startInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(AppWD, "audiocrossreference.exe"),
                    WorkingDirectory = unpackedDir1,
                    Arguments = string.Format(" {0}",
                                                    wem),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
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
            var DB_Path = txt_DBFolder.Text;
            var xx = "";
            var oledb = DisplayData();
            if (oledb.Contains("ACE.OLEDB.12") && (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes" || ConfigRepository.Instance()["dlcm_DBFolder"].Contains("accdb")))
            {
                xx = AppWD.Replace("DLCManager\\external_tools", "") + "\\" + ConfigRepository.Instance()["dlcm_MDBPlus"];
                if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install MDB Plus if you want to use it.", ConfigRepository.Instance()["dlcm_MDBPlus_www"], "Missing Microsoft Data Base Plus", false, false, true, "", "", ""); frm1.ShowDialog(); return; }
            }
            else
                if (File.Exists("C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe"))
            {
                xx = Path.GetDirectoryName(DB_Path) + "\\LinkDB.accdb";
                StartProcesss(@xx, null);

                var startInfox = new ProcessStartInfo
                {
                    FileName = "C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe",
                    WorkingDirectory = AppWD.Replace("external_tools", ""),
                    //Arguments = string.Format(" " + DB_Path),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                if (File.Exists(DB_Path))
                    using (var DDC = new Process())
                    {
                        DDC.StartInfo = startInfox; DDC.Start(); DDC.WaitForExit(1000 * 1);
                    }
                StartProcesss("C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe", null);
                return;
            }
            else
            {
                MessageBox.Show("No Microsoft Access or SQLite databases" +
                    " i.e.: C:\\Program Files\\SQLite ODBC Driver for Win64 (existing: " + (File.Exists("C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe") ? "true" : "false") + ")");
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = xx,
                WorkingDirectory = AppWD.Replace("external_tools", ""),
                Arguments = string.Format(" " + DB_Path),
                UseShellExecute = false,
                CreateNoWindow = true
            };

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 1);
                }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void cbx_Groups_DropDown(object sender, EventArgs e)
        {
            ////populate the Group  Dropdown
            //DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groupz FROM Groups WHERE Type=\"DLC\";", txt_DBFolder.Text, cnb, cnc);
            //var norec = ds.Tables[0].Rows.Count;

            //if (norec > 0)
            //{
            //    //remove items
            //    if (cbx_Groups.Items.Count > 0)
            //    {
            //        cbx_Groups.DataSource = null;
            //        for (int k = cbx_Groups.Items.Count - 1; k >= 0; --k)
            //        {
            //            if (!cbx_Groups.Items[k].ToString().Contains("--"))
            //            {
            //                cbx_Groups.Items.RemoveAt(k);
            //            }
            //        }
            //    }
            //    //add items
            //    cbx_Groups.DataSource = null;
            //    for (int j = 0; j < norec; j++)
            //    {
            //        var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
            //        cbx_Groups.Items.Add(tem);
            //    }
            //}
        }

        private void btn_GoImport_Click(object sender, EventArgs e)
        {
            StartProcesss(@txt_RocksmithDLCPath.Text, null);
            //string t = txt_RocksmithDLCPath.Text;
            //try
            //{
            //    Process process = Process.Start(@t);
            //}
            //catch (Exception ex)
            //{
            //    DialogResult result1 = MessageBox.Show("Can not open Import folder in Exporer !\nDo you want to create folder?\n" + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result1 == DialogResult.Yes) { try { Directory.CreateDirectory(t); } catch (Exception Ex) { MessageBox.Show("Can not create Import folder in Exporer !\n" + Ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); } }
            //}
        }

        private void btm_GoTemp_Click(object sender, EventArgs e)
        {
            StartProcesss(@txt_TempPath.Text, null);
            //string t = txt_TempPath.Text;
            //try
            //{
            //    Process process = Process.Start(@t);
            //}
            //catch (Exception ex)
            //{
            //    DialogResult result1 = MessageBox.Show("Can not open Temp folder in Exporer !\nDo you want to create folder?\n" + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result1 == DialogResult.Yes) { try { Directory.CreateDirectory(t); } catch (Exception Ex) { MessageBox.Show("Can not create Temp folder in Exporer !\n" + Ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); } }

            //}
        }

        private void btn_ProfilesSave_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Add Profile", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (chbx_Configurations.Text == "Select Profile") return;
            SaveOK = "OK"; var OleProf = ConfigRepository.Instance()["dlcm_Configurations"];
            SaveSettings(); SaveOK = "";

            AddNewProfile(false, OleProf);
            timestamp = UpdateLog(timestamp, "End SAving the Profile", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        }

        private void AddNewProfile(bool copydefault, string OleProf)
        {
            //string OleProf = c("dlcm_Configurations");
            if (OleProf == "" || copydefault) OleProf = "Default";
            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" and Type=\"Profile\";", txt_DBFolder.Text, cnb, cnc);
            var norec = GetNoRec(drs, cnb, cnc);//0; norec = drs.Tables.Count;
                                                //if ( > 0)drs.Tables[0].Rows.Count
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text, cnb, cnc);

                norec = GetNoRec(ds, cnb, cnc);//ds.Tables[0].Rows.Count;
                var fnn = (ds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1).ToString();
                string insertcmdA = "CDLC_ID, Groupz, Type, Comments, Profile_Name, DisplayName, DisplayGroup, DisplayPosition, Description";

                //string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack";
                var insertA = "SELECT \"" + fnn + "\", Groupz, Type, Comments, REPLACE(Profile_Name,\"" + OleProf + "\",\"" + chbx_Configurations.Text + "\"), DisplayName, DisplayGroup, DisplayPosition, Description FROM Groups WHERE Profile_Name =\"" + OleProf + "\"";
                InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, 0, cnc);/*\"" + fnn+"\"*/

                //var insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Album"] + "\",\"Profile\",\"dlcm_Activ_Album\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_albumCovers"] + "\",\"Profile\",\"dlcm_0_albumCovers\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_archive"] + "\",\"Profile\",\"dlcm_0_archive\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_broken"] + "\",\"Profile\",\"dlcm_0_broken\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_data"] + "\",\"Profile\",\"dlcm_0_data\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_duplicate"] + "\",\"Profile\",\"dlcm_0_duplicate\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_old"] + "\",\"Profile\",\"dlcm_0_old\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_albumCovers"] + "\",\"Profile\",\"dlcm_0_albumCovers\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_repacked"] + "\",\"Profile\",\"dlcm_0_repacked\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_0_temp"] + "\",\"Profile\",\"dlcm_0_temp\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AccessACE.OLEDB.16.0"] + "\",\"Profile\",\"dlcm_AccessACE.OLEDB.16.0\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + "\",\"Profile\",\"dlcm_AccessDLLVersion\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Album"] + "\",\"Profile\",\"dlcm_Album\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Album_Sort"] + "\",\"Profile\",\"dlcm_Album_Sort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist"] + "\",\"Profile\",\"dlcm_Artist\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist_Sort"] + "\",\"Profile\",\"dlcm_Artist_Sort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AutoPlay"] + "\",\"Profile\",\"dlcm_AutoPlay\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Autosave"] + "\",\"Profile\",\"dlcm_Autosave\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_BitRate"] + "\",\"Profile\",\"dlcm_BitRate\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Configurations"] + "\",\"Profile\",\"dlcm_Configurations\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_CopyOld"] + "\",\"Profile\",\"dlcm_CopyOld\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DLCManager_Version"] + "\",\"Profile\",\"dlcm_DLCManager_Version\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DebugProfile"] + "\",\"Profile\",\"dlcm_DebugProfile\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DupliUseDates"] + "\",\"Profile\",\"dlcm_DupliUseDates\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DupliM_Sync"] + "\",\"Profile\",\"dlcm_DupliM_Sync\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_EoFPath"] + "\",\"Profile\",\"dlcm_EoFPath\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_EoFPath_www"] + "\",\"Profile\",\"dlcm_EoFPath_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_FTPEU"] + "\",\"Profile\",\"dlcm_FTPEU\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_FTPJP"] + "\",\"Profile\",\"dlcm_FTPJP\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_FTPUS"] + "\",\"Profile\",\"dlcm_FTPUS\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_IOS"] + "\",\"Profile\",\"dlcm_IOS\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + "\",\"Profile\",\"dlcm_GlobalTempVariable\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Last_Packed"] + "\",\"Profile\",\"dlcm_Last_Packed\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_LogPath"] + "\",\"Profile\",\"dlcm_LogPath\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Lyric_Info"] + "\",\"Profile\",\"dlcm_Lyric_Info\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MDBPlus"] + "\",\"Profile\",\"dlcm_MDBPlus\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MDBPlus_www"] + "\",\"Profile\",\"dlcm_MDBPlus_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Mac"] + "\",\"Profile\",\"dlcm_Mac\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MainDBFormat"] + "\",\"Profile\",\"dlcm_MainDBFormat\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MaxBitRate"] + "\",\"Profile\",\"dlcm_MaxBitRate\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MaxLyricLenght_PS3"] + "\",\"Profile\",\"dlcm_MaxLyricLenght_PS3\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MaxPreviewLenght"] + "\",\"Profile\",\"dlcm_MaxPreviewLenght\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MaxSampleRate"] + "\",\"Profile\",\"dlcm_MaxSampleRate\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MediaInfo_CLI"] + "\",\"Profile\",\"dlcm_MediaInfo_CLI\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MediaInfo_CLI_www"] + "\",\"Profile\",\"dlcm_MediaInfo_CLI_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MinPreviewLenght"] + "\",\"Profile\",\"dlcm_MinPreviewLenght\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_MuliThreading"] + "\",\"Profile\",\"dlcm_MuliThreading\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_OrderOfFields"] + "\",\"Profile\",\"dlcm_OrderOfFields\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PC"] + "\",\"Profile\",\"dlcm_PC\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PKG_Linker"] + "\",\"Profile\",\"dlcm_PKG_Linker\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PKG_Linker_www"] + "\",\"Profile\",\"dlcm_PKG_Linker_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PS3xploit-resigner"] + "\",\"Profile\",\"dlcm_PS3xploit-resigner\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PS3xploit-resigner_www"] + "\",\"Profile\",\"dlcm_PS3xploit-resigner_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PS4"] + "\",\"Profile\",\"dlcm_PS4\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PathForBRM"] + "\",\"Profile\",\"dlcm_PathForBRM\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PathForBRM_www"] + "\",\"Profile\",\"dlcm_PathForBRM_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PreviewLenght"] + "\",\"Profile\",\"dlcm_PreviewLenght\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_PreviewStart"] + "\",\"Profile\",\"dlcm_PreviewStart\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Prof"] + "\",\"Profile\",\"dlcm_Prof\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RemoveBassDD"] + "\",\"Profile\",\"dlcm_RemoveBassDD\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Replace"] + "\",\"Profile\",\"dlcm_Replace\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RockBand"] + "\",\"Profile\",\"dlcm_RockBand\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RockBand_www"] + "\",\"Profile\",\"dlcm_RockBand_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] + "\",\"Profile\",\"dlcm_RocksmithDLCPath\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_SampleRate"] + "\",\"Profile\",\"dlcm_SampleRate\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Split4Pack"] + "\",\"Profile\",\"dlcm_Split4Pack\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_SpotifyClientAPI"] + "\",\"Profile\",\"dlcm_SpotifyClientAPI\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_SpotifySecretAPI"] + "\",\"Profile\",\"dlcm_SpotifySecretAPI\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TCommander"] + "\",\"Profile\",\"dlcm_TCommander\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TCommander_www"] + "\",\"Profile\",\"dlcm_TCommander_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker"] + "\",\"Profile\",\"dlcm_TrueAncestor_PKG_Repacker\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TrueAncestor_PKG_Repacker_www"] + "\",\"Profile\",\"dlcm_TrueAncestor_PKG_Repacker_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_UltraStarCreator"] + "\",\"Profile\",\"dlcm_UltraStarCreator\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_UltraStarCreator_www"] + "\",\"Profile\",\"dlcm_UltraStarCreator_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_UniqueID"] + "\",\"Profile\",\"dlcm_UniqueID\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_WinMerge"] + "\",\"Profile\",\"dlcm_WinMerge\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_WinMerge_www"] + "\",\"Profile\",\"dlcm_WinMerge_www\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_XBOX"] + "\",\"Profile\",\"dlcm_XBOX\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_YoutubeAPI"] + "\",\"Profile\",\"dlcm_YoutubeAPI\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_andCopy"] + "\",\"Profile\",\"dlcm_andCopy\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_spotistatus"] + "\",\"Profile\",\"dlcm_spotistatus\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_wwise"] + "\",\"Profile\",\"dlcm_wwise\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_localwwise"] + "\",\"Profile\",\"dlcm_localwwise\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Grouping"] + "\",\"Profile\",\"dlcm_Grouping\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_SearchFields"] + "\",\"Profile\",\"dlcm_SearchFields\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_ExportFields"] + "\",\"Profile\",\"dlcm_ExportFields\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_ArangementFields"] + "\",\"Profile\",\"dlcm_ArangementFields\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Artist"] + "\",\"Profile\",\"dlcm_Activ_Artist\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] + "\",\"Profile\",\"dlcm_Activ_ArtistSort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_FileName"] + "\",\"Profile\",\"dlcm_Activ_FileName\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Title"] + "\",\"Profile\",\"dlcm_Activ_Title\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_TitleSort"] + "\",\"Profile\",\"dlcm_Activ_TitleSort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                ////Params
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul0"] + "\",\"Profile\",\"dlcm_AdditionalManipul0\",\"" + chbx_Configurations.Text + "\",\"Add Increment to all songs Title\",\"Pack\",\"1\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul1"] + "\",\"Profile\",\"dlcm_AdditionalManipul1\",\"" + chbx_Configurations.Text + "\",\"Add Increment to all songs Title per artist\",\"Pack\",\"2\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul2"] + "\",\"Profile\",\"dlcm_AdditionalManipul2\",\"" + chbx_Configurations.Text + "\",\"Make all DLC IDs unique (&save)\",\"Pack\",\"3\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul3"] + "\",\"Profile\",\"dlcm_AdditionalManipul3\",\"" + chbx_Configurations.Text + "\",\"Remove DD\",\"Pack\",\"4\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul4"] + "\",\"Profile\",\"dlcm_AdditionalManipul4\",\"" + chbx_Configurations.Text + "\",\"Backup DB during Startup\",\"General\",\"1\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul5"] + "\",\"Profile\",\"dlcm_AdditionalManipul5\",\"" + chbx_Configurations.Text + "\",\"Remove DD only for Bass Guitar\",\"Pack\",\"5\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul6"] + "\",\"Profile\",\"dlcm_AdditionalManipul6\",\"" + chbx_Configurations.Text + "\",\"When converting Audio use local folder structure\",\"General\",\"2\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul7"] + "\",\"Profile\",\"dlcm_AdditionalManipul7\",\"" + chbx_Configurations.Text + "\",\"Skip Broken songs\",\"Pack\",\"6\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul8"] + "\",\"Profile\",\"dlcm_AdditionalManipul8\",\"" + chbx_Configurations.Text + "\",\"Name to cross-platform Compatible Filenames\",\"Pack\",\"7\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul9"] + "\",\"Profile\",\"dlcm_AdditionalManipul9\",\"" + chbx_Configurations.Text + "\",\"Add Preview if missing 00:30 for 30sec (&save)\",\"Pack\",\"8\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul10"] + "\",\"Profile\",\"dlcm_AdditionalManipul10\",\"" + chbx_Configurations.Text + "\",\"Make all DLC IDs unique\",\"Pack\",\"9\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul11"] + "\",\"Profile\",\"dlcm_AdditionalManipul11\",\"" + chbx_Configurations.Text + "\",\"<Add DD (5 Levels)>\",\"Pack\",\"10\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul12"] + "\",\"Profile\",\"dlcm_AdditionalManipul12\",\"" + chbx_Configurations.Text + "\",\"<Add DD (5 Levels) when missing>\",\"Pack\",\"11\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul13"] + "\",\"Profile\",\"dlcm_AdditionalManipul13\",\"" + chbx_Configurations.Text + "\",\"All Duplicates as Alternates\",\"Import\",\"1\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul14"] + "\",\"Profile\",\"dlcm_AdditionalManipul14\",\"" + chbx_Configurations.Text + "\",\"Any Custom as Alternate if an Original exists\",\"Import\",\"2\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul15"] + "\",\"Profile\",\"dlcm_AdditionalManipul15\",\"" + chbx_Configurations.Text + "\",\"Move the Imported files to temp/0_old\",\"Import\",\"3\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul16"] + "\",\"Profile\",\"dlcm_AdditionalManipul16\",\"" + chbx_Configurations.Text + "\",\"Make Artist/Title same as Artist/Title/Album Sort\",\"Import\",\"4\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul17"] + "\",\"Profile\",\"dlcm_AdditionalManipul17\",\"" + chbx_Configurations.Text + "\",\"Make Artist/Title same as Artist/Title/Album Sort\",\"Pack\",\"12\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul18"] + "\",\"Profile\",\"dlcm_AdditionalManipul18\",\"" + chbx_Configurations.Text + "\",\"<Import without The/Die at the beginning of Artist/Title Sort>\",\"Import\",\"5\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul19"] + "\",\"Profile\",\"dlcm_AdditionalManipul19\",\"" + chbx_Configurations.Text + "\",\"<Pack without The/Die at the beginning of Artist/Title Sort>\",\"Pack\",\"13\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul20"] + "\",\"Profile\",\"dlcm_AdditionalManipul20\",\"" + chbx_Configurations.Text + "\",\"Move The/Die at the end of Title Sort\",\"Import\",\"6\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul21"] + "\",\"Profile\",\"dlcm_AdditionalManipul21\",\"" + chbx_Configurations.Text + "\",\"Move The/Die at the end of Title/Title Sort\",\"Pack\",\"14\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul22"] + "\",\"Profile\",\"dlcm_AdditionalManipul22\",\"" + chbx_Configurations.Text + "\",\"Move The/Die only at the end of Artist/Album/xx Sort\",\"Import\",\"7\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul23"] + "\",\"Profile\",\"dlcm_AdditionalManipul23\",\"" + chbx_Configurations.Text + "\",\"Pack with The/Die only at the end of Artist/Album/xx Sort\",\"Pack\",\"15\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul24"] + "\",\"Profile\",\"dlcm_AdditionalManipul24\",\"" + chbx_Configurations.Text + "\",\"Use translation tables for naming standardization\",\"Import\",\"8\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul25"] + "\",\"Profile\",\"dlcm_AdditionalManipul25\",\"" + chbx_Configurations.Text + "\",\"If Original don't add QAs (NOs;DLC/ORIG;etc.)\",\"Pack\",\"16\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul26"] + "\",\"Profile\",\"dlcm_AdditionalManipul26\",\"" + chbx_Configurations.Text + "\",\"Add 5 Levels of DD only to Guitar tracks\",\"Pack\",\"17\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul27"] + "\",\"Profile\",\"dlcm_AdditionalManipul27\",\"" + chbx_Configurations.Text + "\",\"Convert and Transfer/FTP\",\"Pack\",\"18\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul28"] + "\",\"Profile\",\"dlcm_AdditionalManipul28\",\"" + chbx_Configurations.Text + "\",\"If Original don't add QAs (NOs;DLC/ORIG;etc.) except for File Names\",\"Pack\",\"19\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul29"] + "\",\"Profile\",\"dlcm_AdditionalManipul29\",\"" + chbx_Configurations.Text + "\",\"Move Duplicates to _duplicate\",\"Import\",\"9\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul30"] + "\",\"Profile\",\"dlcm_AdditionalManipul30\",\"" + chbx_Configurations.Text + "\",\"Move broken songs to _broken\",\"Import\",\"10\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul31"] + "\",\"Profile\",\"dlcm_AdditionalManipul31\",\"" + chbx_Configurations.Text + "\",\"When removing DD use internal logic not DDC\",\"Import\",\"11\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul32"] + "\",\"Profile\",\"dlcm_AdditionalManipul32\",\"" + chbx_Configurations.Text + "\",\"When importing alternates add newer/older instead of alt.0author\",\"Import\",\"12\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul33"] + "\",\"Profile\",\"dlcm_AdditionalManipul33\",\"" + chbx_Configurations.Text + "\",\"Forcibly Update Import location of all DB fields\",\"Import\",\"13\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul34"] + "\",\"Profile\",\"dlcm_AdditionalManipul34\",\"" + chbx_Configurations.Text + "\",\"Add Preview if missing (lenght> as per config)\",\"Import\",\"14\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul35"] + "\",\"Profile\",\"dlcm_AdditionalManipul35\",\"" + chbx_Configurations.Text + "\",\"Remove illegal characters from Songs Metadata\",\"Import\",\"15\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul36"] + "\",\"Profile\",\"dlcm_AdditionalManipul36\",\"" + chbx_Configurations.Text + "\",\"Keep the Uncompressed Songs superorganized\",\"Import\",\"16\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul37"] + "\",\"Profile\",\"dlcm_AdditionalManipul37\",\"" + chbx_Configurations.Text + "\",\"Import other formats but PC\",\"Import\",\"17\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul38"] + "\",\"Profile\",\"dlcm_AdditionalManipul38\",\"" + chbx_Configurations.Text + "\",\"Import only the unpacked songs already in the 0_Temp folder\",\"Import\",\"18\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul39"] + "\",\"Profile\",\"dlcm_AdditionalManipul39\",\"" + chbx_Configurations.Text + "\",\"Encrypt PS3 Retails songs, with External tool\",\"Pack\",\"20\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul40"] + "\",\"Profile\",\"dlcm_AdditionalManipul40\",\"" + chbx_Configurations.Text + "\",\"Delete ORIG HSAN/OGG when Packing Retails songs\",\"Pack\",\"21\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul41"] + "\",\"Profile\",\"dlcm_AdditionalManipul41\",\"" + chbx_Configurations.Text + "\",\"Try to get Track No. &Details from Spotify (&yb links)\",\"General\",\"3\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul42"] + "\",\"Profile\",\"dlcm_AdditionalManipul42\",\"" + chbx_Configurations.Text + "\",\"Save Log After\",\"Import\",\"19\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul43"] + "\",\"Profile\",\"dlcm_AdditionalManipul43\",\"" + chbx_Configurations.Text + "\",\"Set the DLCID autom\",\"Import\",\"20\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul44"] + "\",\"Profile\",\"dlcm_AdditionalManipul44\",\"" + chbx_Configurations.Text + "\",\"Set the DLCID autom\",\"Pack\",\"22\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul45"] + "\",\"Profile\",\"dlcm_AdditionalManipul45\",\"" + chbx_Configurations.Text + "\",\"<Convert Originals>\",\"Pack\",\"23\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul46"] + "\",\"Profile\",\"dlcm_AdditionalManipul46\",\"" + chbx_Configurations.Text + "\",\"Duplicate Mangement, Title added info is inbetween separators: []\",\"General\",\"4\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul47"] + "\",\"Profile\",\"dlcm_AdditionalManipul47\",\"" + chbx_Configurations.Text + "\",\"Add New Toolkit v. and RePackedByAuthor\",\"General\",\"5\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul48"] + "\",\"Profile\",\"dlcm_AdditionalManipul48\",\"" + chbx_Configurations.Text + "\",\"Remove Multitrack/Live/Acoustic info from Title\",\"Import\",\"21\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul49"] + "\",\"Profile\",\"dlcm_AdditionalManipul49\",\"" + chbx_Configurations.Text + "\",\"Also Copy/FTP\",\"Pack\",\"24\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul50"] + "\",\"Profile\",\"dlcm_AdditionalManipul50\",\"" + chbx_Configurations.Text + "\",\"Manually assess duplicates at the end\",\"Import\",\"22\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul51"] + "\",\"Profile\",\"dlcm_AdditionalManipul51\",\"" + chbx_Configurations.Text + "\",\"@Unpack Overwrite the XML\",\"Import\",\"23\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul52"] + "\",\"Profile\",\"dlcm_AdditionalManipul52\",\"" + chbx_Configurations.Text + "\",\"keep Bass DD if indicated so\",\"Pack\",\"25\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul53"] + "\",\"Profile\",\"dlcm_AdditionalManipul53\",\"" + chbx_Configurations.Text + "\",\"keep All DD if indicated so\",\"Pack\",\"26\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul54"] + "\",\"Profile\",\"dlcm_AdditionalManipul54\",\"" + chbx_Configurations.Text + "\",\"consider All songs as beta (place them top of the list)\",\"Pack\",\"27\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul55"] + "\",\"Profile\",\"dlcm_AdditionalManipul55\",\"" + chbx_Configurations.Text + "\",\"Gen Preview if Preview=Audio or Preview is longer than config (default 30s)\",\"Import\",\"24\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul56"] + "\",\"Profile\",\"dlcm_AdditionalManipul56\",\"" + chbx_Configurations.Text + "\",\"Duplicate manag ignores Multitracks\",\"Import\",\"25\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul57"] + "\",\"Profile\",\"dlcm_AdditionalManipul57\",\"" + chbx_Configurations.Text + "\",\"Don't save Author when generic (i.e. Custom Song Creator)\",\"Import\",\"26\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul58"] + "\",\"Profile\",\"dlcm_AdditionalManipul58\",\"" + chbx_Configurations.Text + "\",\"try to get Track No again (&don't save)\",\"Pack\",\"28\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul59"] + "\",\"Profile\",\"dlcm_AdditionalManipul59\",\"" + chbx_Configurations.Text + "\",\"try to get Track No again (&save)\",\"Pack\",\"29\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul60"] + "\",\"Profile\",\"dlcm_AdditionalManipul60\",\"" + chbx_Configurations.Text + "\",\"At Rebuild don't overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)\",\"General\",\"6\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul61"] + "\",\"Profile\",\"dlcm_AdditionalManipul61\",\"" + chbx_Configurations.Text + "\",\"At Rebuild don't overwrite Standard Song Info (Cover,Year)\",\"General\",\"7\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul62"] + "\",\"Profile\",\"dlcm_AdditionalManipul62\",\"" + chbx_Configurations.Text + "\",\"< duplicate singleTracks L->R / R->L>\",\"Pack\",\"30\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul63"] + "\",\"Profile\",\"dlcm_AdditionalManipul63\",\"" + chbx_Configurations.Text + "\",\"Remove Remote File if GameData has been read\",\"Pack\",\"31\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul64"] + "\",\"Profile\",\"dlcm_AdditionalManipul64\",\"" + chbx_Configurations.Text + "\",\"ONLY Copy/FTP the Last Packed song\",\"Pack\",\"32\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul65"] + "\",\"Profile\",\"dlcm_AdditionalManipul65\",\"" + chbx_Configurations.Text + "\",\"ONLY Copy/FTP the Initially Imported song\",\"Pack\",\"33\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul66"] + "\",\"Profile\",\"dlcm_AdditionalManipul66\",\"" + chbx_Configurations.Text + "\",\"Duplicate manag. ignores Live Songs\",\"Import\",\"27\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul67"] + "\",\"Profile\",\"dlcm_AdditionalManipul67\",\"" + chbx_Configurations.Text + "\",\"Import duplicates (hash)\",\"Import\",\"28\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul68"] + "\",\"Profile\",\"dlcm_AdditionalManipul68\",\"" + chbx_Configurations.Text + "\",\"Delete obvious duplicates (hash) during dupli assesment\",\"Import\",\"29\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul69"] + "\",\"Profile\",\"dlcm_AdditionalManipul69\",\"" + chbx_Configurations.Text + "\",\"Compress AudioFiles to 128VBR /Import if bigger than 136k\",\"Import\",\"30\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul70"] + "\",\"Profile\",\"dlcm_AdditionalManipul70\",\"" + chbx_Configurations.Text + "\",\"Repack Preview (bugfix)\",\"Pack\",\"34\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul71"] + "\",\"Profile\",\"dlcm_AdditionalManipul71\",\"" + chbx_Configurations.Text + "\",\"<@Import/Repack check if Original flag is in the Official list and correct>\",\"Import\",\"31\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul72"] + "\",\"Profile\",\"dlcm_AdditionalManipul72\",\"" + chbx_Configurations.Text + "\",\"Import other formats but PC, as standalone\",\"Import\",\"32\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul73"] + "\",\"Profile\",\"dlcm_AdditionalManipul73\",\"" + chbx_Configurations.Text + "\",\"Add Track Info&Comments beginning of Lyrics\",\"Pack\",\"35\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul74"] + "\",\"Profile\",\"dlcm_AdditionalManipul74\",\"" + chbx_Configurations.Text + "\",\"Add Track start into Vocals\",\"Pack\",\"36\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul75"] + "\",\"Profile\",\"dlcm_AdditionalManipul75\",\"" + chbx_Configurations.Text + "\",\"Copy to \0\0_Old (Overwrites 15 Move to old)\",\"Import\",\"33\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + "\",\"Profile\",\"dlcm_AdditionalManipul76\",\"" + chbx_Configurations.Text + "\",\"Include Tones/arangements Db changes\",\"Pack\",\"37\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul77"] + "\",\"Profile\",\"dlcm_AdditionalManipul77\",\"" + chbx_Configurations.Text + "\",\"After Import open MainDB\",\"General\",\"8\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul78"] + "\",\"Profile\",\"dlcm_AdditionalManipul78\",\"" + chbx_Configurations.Text + "\",\"Fix Audio Issues at end\",\"Import\",\"34\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul79"] + "\",\"Profile\",\"dlcm_AdditionalManipul79\",\"" + chbx_Configurations.Text + "\",\"Manually Asses All Suspicious Duplicates\",\"Import\",\"35\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul80"] + "\",\"Profile\",\"dlcm_AdditionalManipul80\",\"" + chbx_Configurations.Text + "\",\"Duplicate manag. ignores Acoustic Songs\",\"Import\",\"36\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul81"] + "\",\"Profile\",\"dlcm_AdditionalManipul81\",\"" + chbx_Configurations.Text + "\",\"Any Delete (non psarc) goes to RecycleBin\",\"General\",\"9\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul82"] + "\",\"Profile\",\"dlcm_AdditionalManipul82\",\"" + chbx_Configurations.Text + "\",\"Show warning that It will connect to Spotify\",\"General\",\"10\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul83"] + "\",\"Profile\",\"dlcm_AdditionalManipul83\",\"" + chbx_Configurations.Text + "\",\"All suspicious Duplicates will be marked as Duplicates (Ignore)\",\"Import\",\"37\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul84"] + "\",\"Profile\",\"dlcm_AdditionalManipul84\",\"" + chbx_Configurations.Text + "\",\"When checking Songs validate wem bitrate (10% wem conversion raises the bitrate)\",\"General\",\"11\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul85"] + "\",\"Profile\",\"dlcm_AdditionalManipul85\",\"" + chbx_Configurations.Text + "\",\"Apply standard naming to all duplicates\",\"Import\",\"38\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul86"] + "\",\"Profile\",\"dlcm_AdditionalManipul86\",\"" + chbx_Configurations.Text + "\",\"Keep XML Manipulations\",\"Import\",\"39\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul87"] + "\",\"Profile\",\"dlcm_AdditionalManipul87\",\"" + chbx_Configurations.Text + "\",\"Use Latest Spotify API (Web)\",\"General\",\"12\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul88"] + "\",\"Profile\",\"dlcm_AdditionalManipul88\",\"" + chbx_Configurations.Text + "\",\"Gen Preview if Preview is shorter than config (default 10s)\",\"Import\",\"40\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul89"] + "\",\"Profile\",\"dlcm_AdditionalManipul89\",\"" + chbx_Configurations.Text + "\",\"Allow multiple instances of the DLC Manager (for faster repackaging)\",\"Internal\",\"41\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul90"] + "\",\"Profile\",\"dlcm_AdditionalManipul90\",\"" + chbx_Configurations.Text + "\",\"When adding times into vocals(74) add only in seconds\",\"Pack\",\"38\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul91"] + "\",\"Profile\",\"dlcm_AdditionalManipul91\",\"" + chbx_Configurations.Text + "\",\"Add group to Filename\",\"Pack\",\"39\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul92"] + "\",\"Profile\",\"dlcm_AdditionalManipul92\",\"" + chbx_Configurations.Text + "\",\"Package for a HAN enabled PS3\",\"Pack\",\"40\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul93"] + "\",\"Profile\",\"dlcm_AdditionalManipul93\",\"" + chbx_Configurations.Text + "\",\"for a HAN Enabled PS3 then also copy Retail(RS2012) Songs\",\"Pack\",\"41\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul94"] + "\",\"Profile\",\"dlcm_AdditionalManipul94\",\"" + chbx_Configurations.Text + "\",\"After lyrics manipulation Open them in Notepad\",\"Pack\",\"42\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul95"] + "\",\"Profile\",\"dlcm_AdditionalManipul95\",\"" + chbx_Configurations.Text + "\",\"@Export create Package (in@0_temp)\",\"General\",\"13\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul96"] + "\",\"Profile\",\"dlcm_AdditionalManipul96\",\"" + chbx_Configurations.Text + "\",\"@Export create Tabs\",\"General\",\"14\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul97"] + "\",\"Profile\",\"dlcm_AdditionalManipul97\",\"" + chbx_Configurations.Text + "\",\"Only never packed Songs (Overwrites 98)\",\"Pack\",\"43\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul98"] + "\",\"Profile\",\"dlcm_AdditionalManipul98\",\"" + chbx_Configurations.Text + "\",\"Only never packed Songs for the target Platform\",\"Pack\",\"44\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul99"] + "\",\"Profile\",\"dlcm_AdditionalManipul99\",\"" + chbx_Configurations.Text + "\",\"<If Group pack ignore songs that are also in other Groups>\",\"Pack\",\"45\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul100"] + "\",\"Profile\",\"dlcm_AdditionalManipul100\",\"" + chbx_Configurations.Text + "\",\"<Pack anew instead of converting (e.g. Pack Orig file)>\",\"Pack\",\"46\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul101"] + "\",\"Profile\",\"dlcm_AdditionalManipul101\",\"" + chbx_Configurations.Text + "\",\"Check song\",\"Pack\",\"47\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul102"] + "\",\"Profile\",\"dlcm_AdditionalManipul102\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul103"] + "\",\"Profile\",\"dlcm_AdditionalManipul103\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul104"] + "\",\"Profile\",\"dlcm_AdditionalManipul104\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul105"] + "\",\"Profile\",\"dlcm_AdditionalManipul105\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul106"] + "\",\"Profile\",\"dlcm_AdditionalManipul106\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul107"] + "\",\"Profile\",\"dlcm_AdditionalManipul107\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul108"] + "\",\"Profile\",\"dlcm_AdditionalManipul108\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul109"] + "\",\"Profile\",\"dlcm_AdditionalManipul109\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul110"] + "\",\"Profile\",\"dlcm_AdditionalManipul110\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul111"] + "\",\"Profile\",\"dlcm_AdditionalManipul111\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul112"] + "\",\"Profile\",\"dlcm_AdditionalManipul112\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul113"] + "\",\"Profile\",\"dlcm_AdditionalManipul113\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul114"] + "\",\"Profile\",\"dlcm_AdditionalManipul114\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul115"] + "\",\"Profile\",\"dlcm_AdditionalManipul115\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul116"] + "\",\"Profile\",\"dlcm_AdditionalManipul116\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul117"] + "\",\"Profile\",\"dlcm_AdditionalManipul117\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul118"] + "\",\"Profile\",\"dlcm_AdditionalManipul118\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul119"] + "\",\"Profile\",\"dlcm_AdditionalManipul119\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul120"] + "\",\"Profile\",\"dlcm_AdditionalManipul120\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul121"] + "\",\"Profile\",\"dlcm_AdditionalManipul121\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul122"] + "\",\"Profile\",\"dlcm_AdditionalManipul122\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul123"] + "\",\"Profile\",\"dlcm_AdditionalManipul123\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul124"] + "\",\"Profile\",\"dlcm_AdditionalManipul124\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul125"] + "\",\"Profile\",\"dlcm_AdditionalManipul125\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul126"] + "\",\"Profile\",\"dlcm_AdditionalManipul126\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul127"] + "\",\"Profile\",\"dlcm_AdditionalManipul127\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul128"] + "\",\"Profile\",\"dlcm_AdditionalManipul128\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul129"] + "\",\"Profile\",\"dlcm_AdditionalManipul129\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul130"] + "\",\"Profile\",\"dlcm_AdditionalManipul130\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul131"] + "\",\"Profile\",\"dlcm_AdditionalManipul131\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul132"] + "\",\"Profile\",\"dlcm_AdditionalManipul132\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul133"] + "\",\"Profile\",\"dlcm_AdditionalManipul133\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul134"] + "\",\"Profile\",\"dlcm_AdditionalManipul134\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul135"] + "\",\"Profile\",\"dlcm_AdditionalManipul135\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul136"] + "\",\"Profile\",\"dlcm_AdditionalManipul136\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul137"] + "\",\"Profile\",\"dlcm_AdditionalManipul137\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul138"] + "\",\"Profile\",\"dlcm_AdditionalManipul138\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul139"] + "\",\"Profile\",\"dlcm_AdditionalManipul139\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul140"] + "\",\"Profile\",\"dlcm_AdditionalManipul140\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul141"] + "\",\"Profile\",\"dlcm_AdditionalManipul141\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul142"] + "\",\"Profile\",\"dlcm_AdditionalManipul142\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul143"] + "\",\"Profile\",\"dlcm_AdditionalManipul143\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul144"] + "\",\"Profile\",\"dlcm_AdditionalManipul144\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul145"] + "\",\"Profile\",\"dlcm_AdditionalManipul145\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul146"] + "\",\"Profile\",\"dlcm_AdditionalManipul146\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul147"] + "\",\"Profile\",\"dlcm_AdditionalManipul147\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul148"] + "\",\"Profile\",\"dlcm_AdditionalManipul148\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul149"] + "\",\"Profile\",\"dlcm_AdditionalManipul149\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul150"] + "\",\"Profile\",\"dlcm_AdditionalManipul150\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul151"] + "\",\"Profile\",\"dlcm_AdditionalManipul151\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul152"] + "\",\"Profile\",\"dlcm_AdditionalManipul152\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul153"] + "\",\"Profile\",\"dlcm_AdditionalManipul153\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul154"] + "\",\"Profile\",\"dlcm_AdditionalManipul154\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul155"] + "\",\"Profile\",\"dlcm_AdditionalManipul155\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul156"] + "\",\"Profile\",\"dlcm_AdditionalManipul156\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul157"] + "\",\"Profile\",\"dlcm_AdditionalManipul157\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul158"] + "\",\"Profile\",\"dlcm_AdditionalManipul158\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul159"] + "\",\"Profile\",\"dlcm_AdditionalManipul159\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul160"] + "\",\"Profile\",\"dlcm_AdditionalManipul160\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul161"] + "\",\"Profile\",\"dlcm_AdditionalManipul161\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul162"] + "\",\"Profile\",\"dlcm_AdditionalManipul162\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul163"] + "\",\"Profile\",\"dlcm_AdditionalManipul163\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul164"] + "\",\"Profile\",\"dlcm_AdditionalManipul164\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul165"] + "\",\"Profile\",\"dlcm_AdditionalManipul165\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul166"] + "\",\"Profile\",\"dlcm_AdditionalManipul166\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul167"] + "\",\"Profile\",\"dlcm_AdditionalManipul167\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul168"] + "\",\"Profile\",\"dlcm_AdditionalManipul168\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul169"] + "\",\"Profile\",\"dlcm_AdditionalManipul169\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul170"] + "\",\"Profile\",\"dlcm_AdditionalManipul170\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul171"] + "\",\"Profile\",\"dlcm_AdditionalManipul171\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul172"] + "\",\"Profile\",\"dlcm_AdditionalManipul172\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul173"] + "\",\"Profile\",\"dlcm_AdditionalManipul173\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul174"] + "\",\"Profile\",\"dlcm_AdditionalManipul174\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul175"] + "\",\"Profile\",\"dlcm_AdditionalManipul175\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul176"] + "\",\"Profile\",\"dlcm_AdditionalManipul176\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul177"] + "\",\"Profile\",\"dlcm_AdditionalManipul177\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul178"] + "\",\"Profile\",\"dlcm_AdditionalManipul178\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul179"] + "\",\"Profile\",\"dlcm_AdditionalManipul179\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul180"] + "\",\"Profile\",\"dlcm_AdditionalManipul180\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul181"] + "\",\"Profile\",\"dlcm_AdditionalManipul181\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul182"] + "\",\"Profile\",\"dlcm_AdditionalManipul182\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul183"] + "\",\"Profile\",\"dlcm_AdditionalManipul183\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul184"] + "\",\"Profile\",\"dlcm_AdditionalManipul184\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul185"] + "\",\"Profile\",\"dlcm_AdditionalManipul185\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul186"] + "\",\"Profile\",\"dlcm_AdditionalManipul186\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul187"] + "\",\"Profile\",\"dlcm_AdditionalManipul187\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul188"] + "\",\"Profile\",\"dlcm_AdditionalManipul188\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul189"] + "\",\"Profile\",\"dlcm_AdditionalManipul189\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul190"] + "\",\"Profile\",\"dlcm_AdditionalManipul190\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul191"] + "\",\"Profile\",\"dlcm_AdditionalManipul191\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul192"] + "\",\"Profile\",\"dlcm_AdditionalManipul192\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul193"] + "\",\"Profile\",\"dlcm_AdditionalManipul193\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul194"] + "\",\"Profile\",\"dlcm_AdditionalManipul194\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul195"] + "\",\"Profile\",\"dlcm_AdditionalManipul195\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul196"] + "\",\"Profile\",\"dlcm_AdditionalManipul196\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul197"] + "\",\"Profile\",\"dlcm_AdditionalManipul197\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul198"] + "\",\"Profile\",\"dlcm_AdditionalManipul198\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul199"] + "\",\"Profile\",\"dlcm_AdditionalManipul199\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul200"] + "\",\"Profile\",\"dlcm_AdditionalManipul200\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);


                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Album"] + "\",\"Profile\",\"dlcm_Album\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Album_Sort"] + "\",\"Profile\",\"dlcm_Album_Sort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist"] + "\",\"Profile\",\"dlcm_Artist\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist_Sort"] + "\",\"Profile\",\"dlcm_Artist_Sort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Groups"] + "\",\"Profile\",\"dlcm_Groups\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_Mac"] + "\",\"Profile\",\"dlcm_chbx_Mac\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_PC"] + "\",\"Profile\",\"dlcm_chbx_PC\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_PS3"] + "\",\"Profile\",\"dlcm_chbx_PS3\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_XBOX360"] + "\",\"Profile\",\"dlcm_chbx_XBOX360\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DBFolder"] + "\",\"Profile\",\"dlcm_DBFolder\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Debug"] + "\",\"Profile\",\"dlcm_Debug\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DefaultDB"] + "\",\"Profile\",\"dlcm_DefaultDB\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_File_Name"] + "\",\"Profile\",\"dlcm_File_Name\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_netstatus"] + "\",\"Profile\",\"dlcm_netstatus\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] + "\",\"Profile\",\"dlcm_RocksmithDLCPath\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TempPath"] + "\",\"Profile\",\"dlcm_TempPath\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Title"] + "\",\"Profile\",\"dlcm_Title\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Title_Sort"] + "\",\"Profile\",\"dlcm_Title_Sort\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_InclMultiplyManager"] + "\",\"Profile\",\"dlcm_InclMultiplyManager\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_No4Spliting"] + "\",\"Profile\",\"dlcm_No4Spliting\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_StartInDLCM"] + "\",\"Profile\",\"dlcm_StartInDLCM\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_timestamp"] + "\",\"Profile\",\"dlcm_timestamp\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DBVersion"] + "\",\"Profile\",\"dlcm_DBVersion\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_ThreadNo"] + "\",\"Profile\",\"dlcm_ThreadNo\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
                //insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_ShowConenctivityOnce"] + "\",\"Profile\",\"dlcm_ShowConenctivityOnce\",\"" + chbx_Configurations.Text + "\",\"\",\"\",\"\",\"\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, mutit, cnc);
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Do you Wanna rename the profile?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) { DataSet dts = new DataSet(); dts = UpdateDB("Groups", "UPDATE Groups SET Profile_Name=\"" + chbx_Configurations.Text + "\" WHERE CDLC_ID=" + drs.Tables[0].Rows[0].ItemArray[0].ToString() + "and Type=\"Profile\";", cnb, cnc); }
                else MessageBox.Show("Please chose a unique name. (input a New name to copy curent profiule to, then press Plus)");
            }
            ProfilesRefresh();
        }

        private void chbx_Rebuild_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_Rebuild.Checked) btn_PopulateDB.Text = "Import DLCs";
            else btn_PopulateDB.Text = "Rebuild DLC Main DB";
        }

        private void btn_OpenGame_Click(object sender, EventArgs e)
        {
            SaveSettings();//Save settings
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Deleting the Profile", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            string promptValue = chbx_Configurations.Text;
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\" ORDER BY Profile_Name ASC", txt_DBFolder.Text, cnb, cnc);
            var norec = GetNoRec(ds, cnb, cnc); var options = "";
            //if (ds.Tables.Count > 0) norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
                for (int j = 0; j < norec; j++)
                    options += ds.Tables[0].Rows[j].ItemArray[0].ToString() + " - ";

            if (chbx_Configurations.Text == "Select Profile" || chbx_Configurations.Text == "") promptValue = Prompt.ShowDialog("Type the Profile to be deleted if referencing to another DB", "Type Profile Name (" + options + ")");

            var cmd = "DELETE * FROM Groups WHERE Type=\"Profile\" AND Profile_Name= \"" + promptValue + "\"";
            DeleteFromDB("Groups", cmd, cnb, cnc);
            chbx_Configurations.Text = "";
            ProfilesRefresh();
            timestamp = UpdateLog(timestamp, "End Deleting the Profile", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btm_GoDB_Click(object sender, EventArgs e)
        {
            if (@txt_DBFolder.Text.Contains(".accdb")) StartProcesss(@txt_DBFolder.Text, null);
            else btn_Log_Click(null, null);
            //string t = txt_DBFolder.Text;
            //try
            //{
            //    Process process = Process.Start(@t);
            //}
            //catch (Exception ex)
            //{
            //    try { Process process = Process.Start(Path.GetDirectoryName(t)); }
            //    catch (Exception Exx)
            //    {
            //        DialogResult result1 = MessageBox.Show("Can not open DB folder in Exporer !\nDo you want to create folder?\n" + Exx.Message + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result1 == DialogResult.Yes) { try { Directory.CreateDirectory(t); } catch (Exception Ex) { MessageBox.Show("Can not create DB folder in Exporer !\n" + Ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); } }
            //    }
            //}
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            var logPatht = (logPath == null || !DirectoryExists(logPath) ? txt_TempPath.Text + "\\0_log" : logPath);
            if (DirectoryExists(logPatht))
            {
                StartProcesss(@logPatht, null);
                btn_OpenLogsFolder.Text = btn_OpenLogsFolder.Text.Replace(" N/A", "");
                //try
                //{
                //    Process process = Process.Start(@logPatht);
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Can not open Duplicate folder in Exporer !");
                //}
            }
            else btn_OpenLogsFolder.Text = btn_OpenLogsFolder.Text + " N/A";
        }

        //string RepackP = "";
        //private void chbx_XBOX360_CheckedChanged(object sender, EventArgs e)
        //{
        //    //RepackP = txt_TempPath.Text + "\\0_repacked\\XBOX360";
        //}


        //private void chbx_Mac_CheckedChanged(object sender, EventArgs e)
        //{
        //    //RepackP = txt_TempPath.Text + "\\0_repacked\\MAC";
        //}

        //private void chbx_PS3_CheckedChanged(object sender, EventArgs e)
        //{
        //    //RepackP = txt_TempPath.Text + "\\0_repacked\\PS3";
        //}

        //private void chbx_PC_CheckedChanged(object sender, EventArgs e)
        //{
        //    //RepackP = txt_TempPath.Text + "\\0_repacked\\PC";
        //}

        private void btm_GoRepack_Click(object sender, EventArgs e)
        {
            var RepackP = !chbx_PC.Checked ? (!chbx_Mac.Checked ? (!chbx_PS3.Checked ? (!chbx_PC.Checked ? (txt_TempPath.Text + "\\0_repacked\\") : txt_TempPath.Text + "\\0_repacked\\XBOX360") : txt_TempPath.Text + "\\0_repacked\\PS3") : txt_TempPath.Text + "\\0_repacked\\MAC") : txt_TempPath.Text + "\\0_repacked\\PC";
            StartProcesss(@RepackP, null);
            //try
            //{
            //    Process process = Process.Start(@RepackP);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            //}
        }

        //private void cbx_Groups_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groupz=\"" + cbx_Groups.Text + "\")", "");
        //}

        //private void rbtn_Population_Selected_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")", "");
        //}

        //private void rbtn_Population_All_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rbtn_Population_All.Checked) RefreshSelectedStat("Main", "", "");
        //}

        private void btn_Enable_CDLC_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Getting Hashes of patched files to compare against Game files", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var hexw = GetHash(AppWD + "\\CDLCEnablers\\D3DX9_42.dll");
            var hexm1 = GetHash(AppWD + "\\CDLCEnablers\\insert_dylib");
            var hexm2 = GetHash(AppWD + "\\CDLCEnablers\\libRSBypass.dylib");
            timestamp = UpdateLog(timestamp, "Done getting hashes", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var cf = c("general_rs2014path");
            var cg = c("dlcm_PC");
            var pathPC = DirectoryExists(cf) && cf.IndexOf(":\\") >= 0 ? cf : cg;
            var pathMac = (File.Exists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac"));
            if (File.Exists(pathPC + "\\..\\D3DX9_42.dll") || File.Exists(pathPC + "\\D3DX9_42.dll"))
            {
                if (hexw != GetHash(pathPC + "\\..\\D3DX9_42.dll") || hexw != GetHash(pathPC + "\\D3DX9_42.dll"))
                {
                    DialogResult result1 = MessageBox.Show("Windows patch doesnt seem to be installed" + "\n\nChose:\n\n1. Overrite\n2. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        if (pathPC.IndexOf("\\DLC\\") >= 0) File.Copy(AppWD + "\\CDLCEnablers\\D3DX9_42.dll", pathPC + "\\..\\D3DX9_42.dll", true);
                        else File.Copy(AppWD + "\\CDLCEnablers\\D3DX9_42.dll", pathPC + "\\D3DX9_42.dll", true);
                        timestamp = UpdateLog(timestamp, "Copied D3DX9_42.dll", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                }
            }
            else
            {
                if (File.Exists(pathPC + "\\Rocksmith2014.exe"))
                {
                    //
                    //DialogResult result1 = MessageBox.Show("Windows patch doesnt seem to be installed" + "\n\nChose:\n\n1. Overrite\n2. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result1 == DialogResult.Yes)
                    //    if (pathPC.IndexOf("\\DLC\\") >= 0) File.Copy(AppWD + "\\CDLCEnablers\\D3DX9_42.dll", pathPC + "\\..\\D3DX9_42.dll", true);
                    //else 
                    File.Copy(AppWD + "\\CDLCEnablers\\D3DX9_42.dll", pathPC + "\\D3DX9_42.dll", true);

                    timestamp = UpdateLog(timestamp, "Copied D3DX9_42.dll", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                //
                else
                {
                    DialogResult result1 = MessageBox.Show(pathPC + "Windows Rocksmith path not valid.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (File.Exists(pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\Rocksmith2014"))
                if (hexm1 != GetHash(pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\libRSBypass.dylib")
                    || hexm1 != GetHash(pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\insert_dylib"))
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
                            df = "RS_PATH=\"" + pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\"";
                            lines += df.Replace("\\\\", "\\").Replace("\\Mac\\", "\\").Replace("\\", "/") + "\n";
                        }
                        else
                            lines += line + "\n";
                    }
                    info.Close();
                    File.WriteAllText(AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command", lines);
                    timestamp = UpdateLog(timestamp, "Wrote RUN_PATCH_RS.command to run in macos", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                    MessageBox.Show("In MAC please run the script \n@" + AppWD + "\\CDLCEnablers\\RUN_PATCH_RS.command\nthat will modify the App \n\nAdd Dll to trusted sources if suggested otehrwise by AllowAnyway in Preferences->General@" + df, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            if (File.Exists(pathPC + "\\..\\D3DX9_42.dll") || File.Exists(pathPC + "\\D3DX9_42.dll"))
                if (hexw != GetHash(pathPC + "\\..\\D3DX9_42.dll") || hexw != GetHash(pathPC + "\\D3DX9_42.dll"))
                    MessageBox.Show("Windows APP @" + pathPC + " enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Windows APP @" + pathPC + " NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Windows APP @" + pathPC + " NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);


            if (hexm1 == GetHash(pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\libRSBypass.dylib")
                || hexm1 == GetHash(pathMac + "\\..\\Rocksmith2014.app\\Contents\\MacOS\\insert_dylib"))
                MessageBox.Show("MAC APP (maybe) enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("MAC APP NOT enabled to show CDLC", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private int countFilez(string[] filez)
        {
            var j = 0;
            for (j = 0; j < filez.Count(); j++) { if (filez[j] == "" || filez[j] == null) break; }
            return j + 1;
        }
        private void SetImportNo()
        {
            string[] filez;
            int cnt = 0;
            timestamp = UpdateLog(timestamp, "Starting SetNos", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (DirectoryExists(txt_RocksmithDLCPath.Text))
            {
                //GetDirList and calcualte hash for the IMPORTED file
                var searchPattern = new Regex(
                        @"(psarc|xbox)",
                        RegexOptions.IgnoreCase);
                if (GetParam(37, chbx_Additional_Manipulations)) //38. Import other formats but PC, as well(separately of course)
                                                                 //filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
                {
                    var files = Directory.EnumerateFiles(txt_RocksmithDLCPath.Text)
                    .Where(f => searchPattern.IsMatch(f))
                    .ToList();
                    cnt = files.Count();
                }
                else
                {
                    filez = System.IO.Directory.GetFiles(txt_RocksmithDLCPath.Text, "*_p.psarc");
                    cnt = filez.Count();
                }
                btn_PopulateDB.Text = "Import " + cnt.ToString() + " DLCs";
            }
            else btn_PopulateDB.Text = "Import N/A DLCs";

            timestamp = UpdateLog(timestamp, "Ending SetNos import files", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT ID FROM Main;", "", cnb, cnc);
            var noOfRec = GetNoRec(dus, cnb, cnc);//dus.Tables.Count > 0 ? dus.Tables[0].Rows.Count : 0;
            btn_OpenMainDB.Text = "Open MainDB" + " (" + noOfRec.ToString() + ")";

            //if (rbtn_Population_All.Checked) 
            RefreshSelectedStat(rbtn_Population_PackNO.Checked);
            //else if (rbtn_Population_Groups.Checked)
            //    if (ConfigRepository.Instance()["dlcm_AdditionalManipul89"] == "Yes")
            //        RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groupz=\"" + cbx_Groups.Text + "\")", "Split4Pack=\"" + txt_NoOfSplits.Text + "\"");
            //    else
            //        RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groupz=\"" + cbx_Groups.Text + "\")", "");
            //else if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")", "");

            timestamp = UpdateLog(timestamp, "Ending setNo", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);//"Ending Get    " + filez.Count().ToString() + "     No of Records    " + noOfRec.ToString() + "     No of File to import    "++"     No of Selected/Grouped Songs"
        }

        private void btn_CalcNoOfImports_Click(object sender, EventArgs e)
        {
            SetImportNo();
        }

        private void btn_CopyDefaultDBtoTemp_Click(object sender, EventArgs e)
        {
            string fielPath;
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
                fielPath = MyAppWD + "\\Files.accdb";
            else
                fielPath = MyAppWD + "\\SQLLite.db";
            string dest = "";

            if (!DirectoryExists(txt_TempPath.Text)) Directory.CreateDirectory(txt_TempPath.Text);
            if (!DirectoryExists(txt_RocksmithDLCPath.Text)) Directory.CreateDirectory(txt_RocksmithDLCPath.Text);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
                dest = txt_TempPath.Text + "\\AccessDB.accdb";
            else
                dest = txt_TempPath.Text + "\\SQLLiteDB.db";
            //if (DirectoryExists(txt_DBFolder.Text) || DirectoryExists(Path.GetDirectoryName(txt_DBFolder.Text))) dest = txt_DBFolder.Text.Replace("\\AccessDB.accdb","") + "\\AccessDB.accdb";
            if (File.Exists(fielPath))
                try
                {
                    if (File.Exists(dest))
                    {
                        //MessageBox.Show("Remove/rename MANUALLY Existing File at " + dest);
                        DialogResult result1 = DialogResult.No;
                        result1 = MessageBox.Show("Do you want to automatically Save a copy of existing DB or MANUALLY move Existing File and restart. " + dest + "", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result1 == DialogResult.No) ;
                        else
                        {
                            //cnb.Close(); //Prb doesnt as cnb exsts only in utlities fucntions
                            //DeleteFile(dest + "old", true);
                            File.Move(dest, dest + System.DateTime.Now.ToString().Replace(":", "").Replace("/", "") + "old");
                            File.Copy(fielPath, dest, false);
                        }
                    }
                    else File.Copy(fielPath, dest, false);
                    txt_DBFolder.Text = dest;
                    var t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);
                    ProfilesRefresh();
                    //lbl_Access.Visible = false;
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    MessageBox.Show(fielPath + "----" + dest + "Error at copy OLD " + ex.Message);
                }
        }

        private void btn_Param_Click(object sender, EventArgs e)
        {
            StartProcesss(MyAppWD + "\\..\\..\\RocksmithToolkitLib.Config.xml", null);
            //var patt = MyAppWD + "\\..\\..\\RocksmithToolkitLib.Config.xml";
            //try
            //{
            //    Process process = Process.Start(@patt);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Param folder in Exporer !");
            //}
        }

        private void btn_Add2Retail_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "1/8 Starting Packing " + " songs.", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //var TargetPlatform = new Platform("Pc", GameVersion.RS2014.ToString());

            //var needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
            //var tmpDir = Path.GetTempPath();

            //var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, SourcePlainfotform, false, false);


            //var startInfo = new ProcessStartInfo();
            //startInfo.FileName = Path.Combine(AppWD, "..\\..\\packer.exe");
            //startInfo.WorkingDirectory = srcPath;// Path.GetDirectoryName();
            //startInfo.Arguments = String.Format(" --pack --version=RS2014 --platform={0} --output={1} --input={2}",
            //                                            srcPlatform,
            //                                            "Pc",
            //                                            srcPath);// + platformDLCP
            //startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            ////if (!File.Exists(t))
            //using (var DDC = new Process())
            //{
            //    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 90 * 1); //wait 1min
            //                                                                            //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    if (DDC.ExitCode > 0) MessageBox.Show(DDC.ExitCode.ToString());
            //}

            //return;
            SaveOK = "OK"; SaveSettings(); SaveOK = "";

            var HSAN = "";

            MainDBfields[] SongRecord = new MainDBfields[20000];
            var formatt = ((chbx_PC.Checked != false ? "Pc" : "") + ";" + (chbx_PS3.Checked != false ? "PS3" : "") + ";" + (chbx_XBOX360.Checked != false ? "XBOX360" : "") + ";" + (chbx_Mac.Checked != false ? "Mac" : "")).Replace(";", "");
            var songspsarc = c("dlcm_TempPath") + "\\0_dlcpacks\\songs_psarc_RS2014_Pc";
            var oldwembnk = ConfigRepository.Instance()["dlcm_MoreWEMBNK"];
            ConfigRepository.Instance()["dlcm_MoreWEMBNK"] = "Yes";
            var platformstring = "";
            Platform platfor;
            var dirToPack = "";
            //platformstring = songspsarc.GetPlatform().platform.ToString();// new Platform(, GameVersion.RS2014.ToString());
            // TO AUTOMATICALLY EXTRACT
            if (platformstring == null || platformstring == "" || platformstring == "None") { platformstring = chbx_PC.Checked ? "Pc" : (chbx_PS3.Checked ? "PS3" : (chbx_Mac.Checked ? "Mac" : (chbx_PS4.Checked ? "PS4" : ("")))); }
            var officialpackedsongs = ""; var cofficialpackedsongs = 0;
            var DLCpackedsongs = ""; var cDLCpackedsongs = 0;
            var Archive_Path = txt_TempPath.Text + "\\0_archive";

            var cm = "SELECT SongsHSANPath FROM CACHE WHERE Platform=\"" + platformstring + "\" AND PS3Region IN (\"\",\"\"US\") AND PSARCName LIKE \"%CACHE%\";";// AND Platform=\"" + formatt + "\"";
            DataSet dv = new DataSet(); dv = SelectFromDB("Cache", cm, "", cnb, cnc);
            //"AlbumArtPath=\"" + file.sourceFile.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds") + "\""
            var rex = GetNoRec(dv, cnb, cnc); //dv.Tables.Count == 0 ? 0 : dv.Tables[0].Rows.Count;
            var SongsPath = "";
            if (rex > 0)
            {
                SongsPath = dv.Tables[0].Rows[0].ItemArray[0].ToString().Substring(0, dv.Tables[0].Rows[0].ItemArray[0].ToString().IndexOf("\\manifests\\songs\\"));
            }
            else SongsPath = c("dlcm_TempPath") + "\\0_dlcpacks\\temp\\songs_psarc_RS2014_Pc";

            if (!GetParam(38, chbx_Additional_Manipulations)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {

                //CLEAN manipulated folder
                //var dio = Directory.CreateDirectory(c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs_psarc_RS2014_Pc\\");
                CleanFolder(c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\", "", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);/*+ Path.GetDirectoryName(SongsPath) + "\\"*/
                //System.IO.DirectoryInfo downloadedMessageInfo3 = new DirectoryInfo(txt_TempPath.Text + "\\0_data");
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\");/*+ Path.GetDirectoryName(SongsPath) + "\\"*/
                var k = 0;
                if (Directory.Exists(SongsPath))
                {
                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                        k++;
                    pB_ReadDLCs.Maximum = k;
                    foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                    {
                        pB_ReadDLCs.Increment(1);
                        timestamp = UpdateLog(timestamp, "2/8 Cleaning Folder" + dir.FullName, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate"
                            && dir.Name != "0_log" && dir.Name != "0_albumCovers" && dir.Name != "0_archive" && dir.Name != "0_data" && dir.Name != "0_temp" && dir.Name != "0_to_import")
                            DeleteDirectory(dir.FullName, true);
                    }
                }
                var j = 0; var japonceoperatio = true;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;

                //gettings songs to add to REtail pack
                var cmdz = "SELECT * FROM Main " + GetSelectedSongs(cbx_Groups.Text.ToString(), rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text, cnb, "", 0,
                    GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked), null, cnc);
                //rbtn_Population_Selected.Checked, rbtn_Population_Groups.Checked
                //    , rbtn_Population_PackNO.Checked, txt_NoOfSplits.Text);

                //Read from CACHE DB ; get retails songs
                SongRecord = GetRecord_s(cmdz, cnb, cnc);
                var norows = SongRecord[0].NoRec.ToInt32();
                //SongRecord[SongRecord[0].NoRec.ToInt32()].Folder_Name = songspsarc;//.Replace("\\temp\\", "\\");
                cm = "SELECT AlbumArtPath, AudioPathWEM, AudioPreviewPathWEM, Artist, Album, Title, ID, SongsHSANPath,Identifier FROM CACHE WHERE Removed<>\"Yes\" AND PSARCName LIKE \"%CACHEPc%\";";// AND Platform=\"" + formatt + "\"";
                DataSet dvs = new DataSet(); dvs = SelectFromDB("Cache", cmdz, "", cnb, cnc);
                //"AlbumArtPath=\"" + file.sourceFile.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds") + "\""
                var re = GetNoRec(dvs, cnb, cnc); //dvs.Tables.Count == 0 ? 0 : dvs.Tables[0].Rows.Count;
                if (re > 0)
                {
                    for (var jj = 0; jj <= re - 1; jj++)
                    {
                        var n = SongRecord[0].NoRec.ToInt32();
                        SongRecord[n] = new MainDBfields();
                        var artp = dvs.Tables[0].Rows[jj].ItemArray[0].ToString().Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds");
                        SongRecord[n].AlbumArtPath = artp;//.Replace("\\temp\\", "\\");
                        SongRecord[n].AudioPath = dvs.Tables[0].Rows[jj].ItemArray[1].ToString().Replace("_fixed.ogg", ".wem");
                        SongRecord[n].audioPreviewPath = dvs.Tables[0].Rows[jj].ItemArray[2].ToString().Replace("_fixed.ogg", ".wem");
                        SongRecord[n].Folder_Name = Path.GetDirectoryName(artp).Replace("\\gfxassets\\album_art", "");// dvr.Tables[0].Rows[jj].ItemArray[2].ToString();
                        SongRecord[n].Artist = dvs.Tables[0].Rows[jj].ItemArray[3].ToString();
                        SongRecord[n].Album = dvs.Tables[0].Rows[jj].ItemArray[4].ToString();
                        SongRecord[n].Song_Title = dvs.Tables[0].Rows[jj].ItemArray[5].ToString();
                        SongRecord[n].ID = dvs.Tables[0].Rows[jj].ItemArray[6].ToString();
                        //SongRecord[n].SongsHSANPath = dvr.Tables[0].Rows[jj].ItemArray[7].ToString();
                        SongRecord[n].DLC_Name = dvs.Tables[0].Rows[jj].ItemArray[8].ToString();
                        SongRecord[0].NoRec = (n + 1).ToString();

                    }
                    //HSAN = dvr.Tables[0].Rows[SongRecord[0].NoRec.ToInt32()-1].ItemArray[7].ToString();
                }
                var txt = "";

                //ADD/copy Retails Folder songs as per current Filter
                pB_ReadDLCs.Maximum = norows;
                var dest = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\" + "songs_psarc_RS2014_" + platformstring; var dlcname = "";/*SongsPath.Replace("\\0_dlcpacks\\"*/
                var listofdlcs = "";
                foreach (var sr in SongRecord)
                {
                    j++;
                    if (sr == null) break;
                    if (sr.Folder_Name == null) break;

                    dirToPack = sr.Folder_Name;
                    //if (platfor == null) { TO AUTOMATICALLY EXTRACT
                    bool IsDLC = dirToPack.IndexOf("dlcpack") >= 0 ? true : false;
                    var r = GenericFunctions.copyallfilesinatemplate(sr, j, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, norows, cnb, IsDLC, dest, platformstring, cnc);
                    if (IsDLC & r)
                    {
                        officialpackedsongs += sr.Artist + " - " + sr.Album + " - " + sr.Song_Title + "\n"; cofficialpackedsongs++;
                    }
                    else
                    {
                        DLCpackedsongs += sr.Artist + " - " + sr.Album + " - " + sr.Song_Title + "\n"; cDLCpackedsongs++;
                    }

                    //pB_ReadDLCs.Increment(1);
                    if (japonceoperatio && dirToPack.IndexOf("ultrasoul") >= 0)
                    {
                        japonceoperatio = false;
                        timestamp = UpdateLog(timestamp, "3/8 Adding flat and jap lyrics", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        dest = "";
                        //Directory.CreateDirectory(dest + "\\" + "flatmodels\\rs");
                        //if (File.Exists(dirToPack + "\\flatmodels\\rs\\rsenumerable_song.flat"))
                        //    FileCopy(dirToPack + "\\flatmodels\\rs\\rsenumerable_song.flat", dest + "\\" + "flatmodels\\rs\\rsenumerable_song.flat", true, j, false);/*c("dlcm_TempPath") + "\\0_dlcpacks\\temp\\songs_psarc_RS2014_Pc*/

                        if (File.Exists(dirToPack + "\\assets\\ui\\lyrics\\ultrasoul\\lyrics_ultrasoul.dds"))
                        {
                            Directory.CreateDirectory(dest + "\\song_ultrasoul\\" + "assets\\ui\\lyrics\\");
                            FileCopy(dirToPack + "\\assets\\ui\\lyrics\\ultrasoul\\lyrics_ultrasoul.dds", dest + "\\song_ultrasoul\\"
                                + "assets\\ui\\lyrics\\lyrics_ultrasoul.dds", true, j, false);/*c("dlcm_TempPath") + "\\0_dlcpacks\\temp\\songs_psarc_RS2014_Pc*/
                        }
                    }
                }

                //
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "Maybe")
                {
                    string batFileName = @"C:\t\copy.bat";
                    //System.Diagnostics.Process.Start(batFileName);
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = false
                    };

                    var process = new Process { StartInfo = startInfo };
                    process.Start();
                    process.StandardInput.WriteLine(@batFileName);
                    process.StandardInput.WriteLine("exit");
                    process.WaitForExit();
                }
                //foreach (var file in info.Arrangements)
                //{
                //    dest = c("dlcm_TempPath") + "\\dlcpacks\\manipulated\\songs_psarc_RS2014_Pc\\" + info.Arrangements[i].SongXml.Name;

                //    i++;
                //}
                //foreach (var file in info.Arrangements)
                //{
                //    FileCopy(file.SongXml.File);
                //}


                //Packer.Pack(dirToPack, targetFileName, SourcePlatform, updateSNG, true); //30.09 added false updateManifest
                //dlcSavePath = repacked_Path + "\\" + chbx_PC + "\\" + FN;
                //RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dirToPack, info, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                //ProgressBar pB_ReadDLCs = new ProgressBar
                //{
                //    Location = new System.Drawing.Point(11, 137),
                //    Margin = new System.Windows.Forms.Padding(5, 2, 5, 2),
                //    Maximum = 4,
                //    Name = "pB_ReadDLCs",
                //    Size = new System.Drawing.Size(1051, 28),
                //    Step = 1,
                //    TabIndex = 263
                //};
                //toolTip1.SetToolTip(pB_ReadDLCs, "Progress bar for different operations of CDLC Manager.");
                //Controls.Add(pB_ReadDLCs);
            }

            //pasck songs.psarc
            var srcPath = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs_psarc_RS2014_" + platformstring;
            //var srcPath = c()+"\\0_dlcpacks\\manipulated\\songs_psarc_RS2014_" + platformstring;
            //HSAN = srcPath+ "\\manifests\\songs_dlc_songs_psarc_rs2014_pc\\songs_dlc_songs_psarc_rs2014_pc.hsan";
            //HSAN = "C:\\t\\0\\0_dlcpacks\\manipulated\\New folder\\songs_psarc_RS2014_Pc\\manifests\\songs_dlc_songs_psarc_rs2014_pc\\songs_dlc_songs_psarc_rs2014_pc.hsan";
            string destPath = "";//= "C:\\t\\0\\0_dlcpacks\\manipulated\\songs.psarc";

            timestamp = UpdateLog(timestamp, "4/8 Loading/AggregateGraph2014.DoLikeSongPack " + srcPath, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var songPackDir = "";
            renamedir(srcPath, srcPath.Replace("_psarc_RS2014_PS3", ""));
            srcPath = srcPath.Replace("_psarc_RS2014_PS3", "");
            if (platformstring.ToUpper() == "PS3")
                songPackDir = AggregateGraph2014.DoLikeSongPack(srcPath, c("general_defaultappid_RS2014"), "ps3", "ps3");
            else
                songPackDir = AggregateGraph2014.DoLikeSongPack(srcPath, c("general_defaultappid_RS2014"), "generic", "windows");
            timestamp = UpdateLog(timestamp, "5/8 packing song.psarc", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            destPath = Path.Combine(Path.GetDirectoryName(srcPath), "songs.psarc");// , String.Format("{0}_songpack_p.psarc", Path.GetFileName(srcPath)));
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            //clean hsan
            renamedir(songPackDir + "\\manifests\\songs_dlc_songs", songPackDir + "\\manifests\\songs");
            File.Move(songPackDir + "\\manifests\\songs\\songs_dlc_songs.hsan", songPackDir + "\\manifests\\songs\\songs.hsan");
            //GenericFunctions.manipulateHSAN2021(songPackDir + "\\manifests\\songs_dlc_songs_psarc_rs2014_" + platformstring + "\\songs_dlc_songs_psarc_rs2014_" + platformstring + ".hsan", cnb, SongRecord);

            GenericFunctions.manipulateHSAN2021(songPackDir + "\\manifests\\songs\\songs.hsan", cnb, SongRecord);
            //HSAN = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs_dlc_songs_psarc_rs2014_" + platformstring + ".hsan";
            HSAN = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs.hsan";
            //FileCopy(songPackDir + "\\manifests\\songs_dlc_songs_psarc_rs2014_" + platformstring + "\\songs_dlc_songs_psarc_rs2014_" + platformstring + ".hsan", HSAN, true, 0, false);
            FileCopy(songPackDir + "\\manifests\\songs\\songs.hsan", HSAN, true, 0, false);
            try
            {                                                                                      // Pc:RS2014 Only for now can't mix platform packages
                if (platformstring.ToUpper() == "PS3")
                {
                    CleanNTGenNT(songPackDir + "\\songs_aggregategraph.nt");
                    Packer.Pack(songPackDir + "\\", destPath, overridePlatform: new Platform(GamePlatform.PS3, GameVersion.RS2014), false, true);
                }
                else
                    Packer.Pack(songPackDir, destPath, overridePlatform: new Platform(GamePlatform.Pc, GameVersion.RS2014));
            }
            catch (Exception ex)
            {
                var tsst = "Error at psarc packing songs.psarc" + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
            // clean up now (song pack folder)
            if (DirectoryExists(songPackDir))
                IOExtension.DeleteDirectory(songPackDir);

            sw.Stop();
            GlobalExtension.ShowProgress("Finished packing archive (elapsed time): " + sw.Elapsed, 100);

            timestamp = UpdateLog(timestamp, "6/8 unpacking to extract manifests\\songs_dlc_songs_psarc_rs2014_pc\\songs_dlc_songs_psarc_rs2014_pc.hsan", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //var unpackedDir = Packer.Unpack(destPath, txt_TempPath.Text + "\\0_dlcpacks\\temp", overridePlatform: new Platform(GamePlatform.Pc, GameVersion.RS2014), false, false); //, falseUnpack cache.psarc for RS14 Official Retails songs rePACKING

            var archivePath = destPath;
            platformstring = "Pc";
            srcPath = "";// "C:\\t\\0\\0_dlcpacks\\manipulated\\cache_psarc_RS2014_" + platformstring;

            var cmd = "SELECT CACHEPsarcName FROM CACHE WHERE Platform=\"" + platformstring + "\" PSARCName LIKE \"%CACHE%\";";// AND Platform=\"" + formatt + "\"";
            DataSet dvr = new DataSet(); dvr = SelectFromDB("Cache", cmd, "", cnb, cnc);
            //"AlbumArtPath=\"" + file.sourceFile.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds") + "\""
            var rec = GetNoRec(dvr, cnb, cnc); //dvr.Tables.Count == 0 ? 0 : dvr.Tables[0].Rows.Count;
            if (rec == 0)
            {
                timestamp = UpdateLog(timestamp, "Error: No CACHEPSARC saved", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                return;
            }
            var CACHEPsarcName = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
            srcPath = c("dlcm_TempPath") + "\\0_dlcpacks\\" + CACHEPsarcName;
            CleanFolder(srcPath.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\"), "", false, false, Archive_Path, "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            CopyFolder(srcPath, srcPath.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\"));

            destPath = "C:\\t\\0\\0_dlcpacks\\manipulated\\cache.psarc";

            //Compress Hsan into cache.xxx and its cache7.7z component
            //DirectoryInfo dic;
            timestamp = UpdateLog(timestamp, "7/8 Adding hsan to cache7.7z", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var startI = new ProcessStartInfo();
            var hsanDir = srcPath.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\") + "\\manifests\\songs\\songs.hsan";
            // unpackedDir + "\\" + "manifests\\songs.hsan";// unpackedDir + "\\" + "manifests\\songs_dlc_songs_psarc_rs2014_pc\\songs_dlc_songs_psarc_rs2014_pc.hsan";/*"dlcm_TempPath")+ "\\0_dlcpacks\\" + */
            startI.FileName = Path.Combine(AppWD, "..\\..\\tools\\7za.exe");
            startI.WorkingDirectory = c("dlcm_TempPath") + "\\0_dlcpacks\\";// Path.GetDirectoryName();
            var za = srcPath.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\") + "\\cache7.7z";
            Directory.CreateDirectory(Path.GetDirectoryName(hsanDir));
            FileCopy(HSAN, hsanDir, true, 0, false);

            //if (!DirectoryExists(c("dlcm_TempPath") + "\\0_dlcpacks\\manifests")) dic = Directory.CreateDirectory(c("dlcm_TempPath") + "\\0_dlcpacks\\manifests");
            //if (!DirectoryExists(c("dlcm_TempPath") + "\\0_dlcpacks\\manifests\\songs")) dic = Directory.CreateDirectory(c("dlcm_TempPath") + "\\0_dlcpacks\\manifests\\songs");
            //File.Copy(hsanDir, c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\cache_psarc_RS2014_Pc" + platformstring+"\\manifests\\songs\\" + Path.GetFileName(destPath), true);

            startI.Arguments = String.Format(" a {0} {1}",
                                                za,
                                                c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\cache_psarc_RS2014_Pc" + "\\manifests\\");// + platformDLCP TempPath + "\\0_dlcpacks\\cache_pc\\ {2}, 
            startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
            using (var DDC = new Process())
            {
                DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Error at Packing cache7.7z IN \\manifests\\songs\\songs.hsan", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); ;
            }

            //copy
            hsanDir = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\cache_psarc_RS2014_Pc\\" + "songs_dlc_songs_psarc_rs2014_pc.hsan";
            FileCopy(HSAN, hsanDir, true, 0, false);
            startI.Arguments = String.Format(" a {0} {1}",
                                    za,
                                    hsanDir);// + platformDLCP TempPath + "\\0_dlcpacks\\cache_pc\\ {2}, 
            startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
            using (var DDC = new Process())
            {
                DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Error at Packing cache7.7z ROOT songs_dlc_songs_psarc_rs2014_pc.hsan", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); ;
            }

            if (File.Exists(HSAN + ".orig")) File.Copy(HSAN + ".orig", HSAN, true);

            //package cache.psarc
            timestamp = UpdateLog(timestamp, "8/8 Packing cache.psarc", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            Platform srcPlatform = srcPath.GetPlatform();
            try
            {
                Packer.Pack(srcPath, destPath, overridePlatform: new Platform(GamePlatform.Pc, GameVersion.RS2014), false, false);
            }
            catch (Exception ex)
            {
                var tsst = "Error at psarc packing cache.psarc" + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }

            ConfigRepository.Instance()["dlcm_MoreWEMBNK"] = oldwembnk;
            var summary = "Done repacking : " + archivePath + ", " + destPath + ".\n\nAdded DLCS(" + cDLCpackedsongs + "):\n\n" + DLCpackedsongs + "\n\nAdded RetailsSong(" + cofficialpackedsongs + ")s:\n\n" + officialpackedsongs;
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Import of NEW songs Summary", false, false, true, "", "", "");
            frm9.Show();
        }



        private int GetParamLocation(int t)
        {
            if (chbx_Additional_Manipulations.Items.Count == 0) return t;
            if (chbx_Additional_Manipulations.Items.Count <= t)
                return 0;
            if (chbx_Additional_Manipulations.Items[1].ToString().IndexOf("{") < 1) return t;

            for (var i = 0; i < chbx_Additional_Manipulations.Items.Count; i++)
            {
                string k = chbx_Additional_Manipulations.Items[i].ToString();
                if (k.IndexOf("{") < 1) continue;
                var orderno = k.Substring(k.IndexOf("{") + 1, k.IndexOf("}") - k.IndexOf("{") - 1);
                if (orderno.ToInt32() == t)
                {
                    return i;
                    break;
                }
            }

            return t;
        }


        // 1. If hash already exists do not insert
        // 2. If hash does not exists then:
        // 2.1.1 If Artist+Album+Title or dlcname exists check author. If same check version
        // 2.1.1.1 If (Artist+Album+Title or dlcname)+author the same check version If bigger add
        // 2.1.1.2 If (Artist+Album+Title or dlcname)+author the same check version If smaller ignore
        // 2.1.1.3 If (Artist+Album+Title or dlcname)+author the same check version If same ?
        // 3.1 If (Artist+Album+Title or dlcname) exists check author. If the not the same add as alternate
        // 3.2 If (Artist+Album+Title or dlcname) exists check author. If empty/generic(Custom Song Creator) show statistics and add as give choice to alternate or ignore
        // 4. IF filenames are the same 
        internal string AssessConflict(UtilitiesFunctions.MainDBfields filed, DLCPackageData datas, string Fauthor, string tkversion, string DD,
            string Bass, string Guitar, string Combo, string Rhythm, string Lead, string Vocalss, string tunnings, int i, int norows,
            string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> xmlhlist, List<string> jsonhlist,
            string dbpathh, List<string> clist, List<string> dlist, bool newold, string Is_Original, string altver, string fsz, string unpackedDir,
            string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic, string platform, string IsLive, string LiveDetails,
            string IsAcoustic, string has_Orig, string dupli_reason, string sel, int Duplic, bool Rebuild, float versio, float norowsduo, string fzg,
            int j, string lengty, string allothers, List<string> cxmlhlist, List<string> snghlist, string filehash, string IsUncensored, string IsEP
            , string IsSingle, string IsSoundtrack, string IsFullAlbum, string IsRemastered, string IsInstrumental, string InTheWorks, string IsKaraoke, string IsDemo,
            string HasFeaturing, string IsRemix, string IsCover, string IsMedley, string IsMultiStrings, string HasCapo)//, string HasCapo
        {
            bool platform_doesnt_matters = GetParam(72, chbx_Additional_Manipulations) == true ? (filed.Platform == platform ? true : false) : true;
            if (!GetParam(79, chbx_Additional_Manipulations))
            {
                if (Rebuild) return "Insert;new"; //At Rebuild ignore duplicates
                                                  //When impocleanrting a original when there is already a similar CDLC
                else if (author == "" && tkversion == "" && Is_Original == "Yes" && GetParam(14, chbx_Additional_Manipulations) && norowsduo >= 1 && (filed.Is_Original != "Yes"))
                {
                    //Generate MAX Alternate NO
                    var fdf = (sel.Replace("ORDER BY Is_Original ASC", "")).Replace("SELECT *", "SELECT max(Alternate_Version_No)");
                    DataSet ddzv = new DataSet(); ddzv = SelectFromDB("Main", (sel.Replace("ORDER BY Is_Original ASC", "")).Replace("SELECT *", "SELECT max(Alternate_Version_No)"), txt_DBFolder.Text, cnb, cnc);
                    //UPDATE the 1(s) not an alternate already
                    int max = GetNoRec(ddzv, cnb, cnc) > 0 ? ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1;
                    DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Song_Title = Song_Title +\" a." + max + "\" ," +
                                            " Song_Title_Sort = Song_Title_Sort+\" a." + max + "\" , Is_Alternate = \"Yes\", Alternate_Version_No=\"" + max + "\" where ID in (" +
                                            sel.Replace("*", "ID").Replace("SELECT max(Alternate_Version_No)", "SELECT ID").Replace(";", "") + ") and Is_Alternate=\"No\"" + ");", cnb, cnc);

                    //Add duplicate_of
                    DataSet dxf = new DataSet(); var cmd = "UPDATE Main SET Duplicate_Of = +\"" + Duplic + "\" WHERE ID=" + filed.ID + ";"; dxf = UpdateDB("Main", cmd, cnb, cnc);
                    return "Insert;autom: new";
                }
                else if (GetParam(68, chbx_Additional_Manipulations) && fzg == filed.File_Hash) return "Ignore;autom: IGNORED as already imported(hash)"; //DUPLICATION DETECTION LOGIC (based on author and version .... more complex one in the asses procedure)

                else if ((author.ToLower() == filed.Author.ToLower() && author != "" && filed.Author != "" && filed.Author != "Custom Song Creator" && author != "Custom Song Creator"))
                {
                    if (MultiTrack_Version == filed.MultiTrack_Version && filed.MultiTrack_Version != "" && MultiTrack_Version != "")
                        ;
                    else
                        if (MultiTrack_Version != filed.MultiTrack_Version && (filed.MultiTrack_Version != "" || MultiTrack_Version != ""))
                        ;
                    else if (float.Parse(filed.Version, NumberStyles.Float, CultureInfo.CurrentCulture) < versio) return "Update;autom: Bigger version";
                    else if (float.Parse(filed.Version, NumberStyles.Float, CultureInfo.CurrentCulture) > versio)
                        return "Ignore;autom: Duplicated IGNORED cause version bigger and NOTalternate";//autom: Duplicated_cause_version_bigger";;autom: ";
                    else if (float.Parse(filed.Version, NumberStyles.Float, CultureInfo.CurrentCulture) == versio) dupli_reason = "autom: Possible Duplicate Import at end. Same Version";
                    //else dupli_reason += "Duplicated wo version number probblems";
                }

                if (filed.DLC_Name.ToLower() == datas.Name.ToLower()) dupli_reason += ". Possible Duplicate Import Same DLC Name";
                if (((art_hash == filed.AlbumArt_Hash && audio_hash == filed.Audio_Hash && audioPreview_hash == filed.AudioPreview_Hash) || (art_hash == filed.AlbumArt_OrigHash && audio_hash == filed.Audio_OrigHash && audioPreview_hash == filed.Audio_OrigPreviewHash))
                       && tkversion == filed.ToolkitVersion && Fauthor == filed.Author
                       && (datas.ToolkitInfo.PackageVersion == filed.Version || (datas.ToolkitInfo.PackageVersion == "" && "1" == filed.Version)) && Is_Original == filed.Is_Original
                       && datas.Name == filed.DLC_Name && (platform_doesnt_matters))
                {
                    dupli_reason += "autom: Initially assed as duplicate with all files hash identical."; timestamp = UpdateLog(timestamp, dupli_reason, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    return "Ignore;" + dupli_reason;/*autom: Dupli_Internal_Hash"*/
                }

                else if (((tkversion == "" && Is_Original == "Yes") || (filed.ToolkitVersion == "" && filed.Is_Original == "Yes"))
                    && filed.Platform != platform && !GetParam(72, chbx_Additional_Manipulations))
                {
                    UpdateDB("Main", "UPDATE Main SET Has_Other_Officials=\"Yes\" WHERE ID=" + filed.ID + "", cnb, cnc);
                    has_Orig = "Yes";
                    dupli_reason += "autom: Initially assed as Duplicate-Original."; timestamp = UpdateLog(timestamp, dupli_reason, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    return "Ignore;" + dupli_reason;/*autom: Dupli_Orig_OtherPlatform"*/
                }

                else if (((tkversion == "" && Is_Original == "Yes") || (filed.ToolkitVersion == "" && filed.Is_Original == "Yes"))
                    && filed.Platform != platform && GetParam(72, chbx_Additional_Manipulations))
                {
                    UpdateDB("Main", "UPDATE Main SET Has_Other_Officials=\"Yes\" WHERE ID=" + filed.ID + "", cnb, cnc);
                    has_Orig = "Yes";
                    dupli_reason += "autom: Initially assed as Duplicate-Original-DiffPlatform."; timestamp = UpdateLog(timestamp, dupli_reason, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    return "Alternate;" + dupli_reason;/*autom: alt*/
                }

                else if (author.ToLower() == filed.Author.ToLower() && author != "" && filed.Author != "" && filed.Author != "Custom Song Creator" && author != "Custom Song Creator"
                && filed.Version != datas.ToolkitInfo.PackageVersion.ToString() && platform != filed.Platform
                && !GetParam(72, chbx_Additional_Manipulations) && Is_Original != "Yes" && filed.Is_Original != "Yes")

                {
                    dupli_reason += "autom: Initially assed as duplicate cause same aothor but different platform."; timestamp = UpdateLog(timestamp, dupli_reason, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    return "Ignore;" + dupli_reason;/*autom: Dupli_Different_Platform*/
                }

                else if ((GetParam(13, chbx_Additional_Manipulations) && !GetParam(85, chbx_Additional_Manipulations)) ||/* 13.Import all Duplicates as Alternates*/
                    (GetParam(14, chbx_Additional_Manipulations) && (tkversion != "" && Is_Original == "No") && (filed.ToolkitVersion == "" && filed.Is_Original == "Yes"))/*14. Import any Custom as Alternate if an Original exists*/
                    /*56.Duplicate manag ignores Multitracks*/
                    || (GetParam(56, chbx_Additional_Manipulations) && ((filed.Is_Multitrack == "Yes" && Is_MultiTrack != "Yes")
                    || (Is_MultiTrack == "Yes" && filed.Is_Multitrack != "Yes") || (Is_MultiTrack == "Yes" && filed.Is_Multitrack == "Yes"
                    && MultiTrack_Version != filed.MultiTrack_Version)))
                    /*80.Duplicate manag.ignores Acoustic Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Acoustic == "Yes" && IsAcoustic != "Yes")
                    || (IsAcoustic == "Yes" && filed.Is_Acoustic != "Yes") || (IsAcoustic == "Yes" && filed.Is_Acoustic == "Yes" && LiveDetails != filed.Live_Details))
                    /*66.Duplicate manag.ignores Live Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Live == "Yes" && IsLive != "Yes")
                    || (IsLive == "Yes" && filed.Is_Live != "Yes") || (IsLive == "Yes" && filed.Is_Live == "Yes" && LiveDetails != filed.Live_Details))
                    /*66.Duplicate manag.ignores Remastered Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Remastered == "Yes" && IsRemastered != "Yes")
                    || (IsRemastered == "Yes" && filed.Is_Remastered != "Yes") || (IsRemastered == "Yes" && filed.Is_Remastered == "Yes"))
                    /*66.Duplicate manag.ignores Single Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Single == "Yes" && IsSingle != "Yes")
                    || (IsSingle == "Yes" && filed.Is_Single != "Yes") || (IsSingle == "Yes" && filed.Is_Single == "Yes"))
                    /*66.Duplicate manag.ignores �P Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_EP == "Yes" && IsEP != "Yes")
                    || (IsEP == "Yes" && filed.Is_EP != "Yes") || (IsEP == "Yes" && filed.Is_EP == "Yes"))
                    /*66.Duplicate manag.ignores Instrumental Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Instrumental == "Yes" && IsInstrumental != "Yes")
                    || (IsInstrumental == "Yes" && filed.Is_Instrumental != "Yes") || (IsInstrumental == "Yes" && filed.Is_Instrumental == "Yes"))
                    /*66.Duplicate manag.ignores SoundTrack Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Soundtrack == "Yes" && IsSoundtrack != "Yes")
                    || (IsSoundtrack == "Yes" && filed.Is_Soundtrack != "Yes") || (IsSoundtrack == "Yes" && filed.Is_Soundtrack == "Yes"))
                    /*66.Duplicate manag.ignores FullAlbum Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_FullAlbum == "Yes" && IsFullAlbum != "Yes")
                    || (IsFullAlbum == "Yes" && filed.Is_FullAlbum != "Yes") || (IsFullAlbum == "Yes" && filed.Is_FullAlbum == "Yes"))
                    /*66.Duplicate manag.ignores Uncensored Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Uncensored == "Yes" && IsUncensored != "Yes")
                    || (IsUncensored == "Yes" && filed.Is_Uncensored != "Yes") || (IsUncensored == "Yes" && filed.Is_Uncensored == "Yes")
                    /*66.Duplicate manag.ignores Karaoke Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Karaoke == "Yes" && IsKaraoke != "Yes")
                    || (IsKaraoke == "Yes" && filed.Is_Karaoke != "Yes") || (IsKaraoke == "Yes" && filed.Is_Karaoke == "Yes")
                    /*66.Duplicate manag.ignores Cover Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Cover == "Yes" && IsCover != "Yes")
                    || (IsCover == "Yes" && filed.Is_Cover != "Yes") || (IsCover == "Yes" && filed.Is_Cover == "Yes")
                //66.Duplicate manag.ignores HasFeaturing Songs
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Has_Featuring == "Yes" && HasFeaturing != "Yes")
                    || (HasFeaturing == "Yes" && filed.Has_Featuring != "Yes") || (HasFeaturing == "Yes" && filed.Has_Featuring == "Yes")
                    /*66.Duplicate manag.ignores Demo Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Demo == "Yes" && IsDemo != "Yes")
                    || (IsDemo == "Yes" && filed.Is_Demo != "Yes") || (IsDemo == "Yes" && filed.Is_Demo == "Yes")
                    /*66.Duplicate manag.ignores Uncensored Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Remix == "Yes" && IsRemix != "Yes")
                    || (IsRemix == "Yes" && filed.Is_Remix != "Yes") || (IsRemix == "Yes" && filed.Is_Remix == "Yes")
                    //66.Duplicate manag.ignores Capo Songs
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Has_Capo == "Yes" && HasCapo != "Yes")
                    || (HasCapo == "Yes" && filed.Has_Capo != "Yes") || (HasCapo == "Yes" && filed.Has_Capo == "Yes")
                    /*66.Duplicate manag.ignores Uncensored Songs */
                    //|| (GetParam(66, chbx_Additional_Manipulations) && ((filed.Has_ShowLights == "Yes" && HasShowLights != "Yes")
                    //|| (HasShowLights == "Yes" && filed.Has_ShowLights != "Yes") || (HasShowLights == "Yes" && filed.Has_ShowLights == "Yes")
                    /*66.Duplicate manag.ignores Uncensored Songs */
                    //|| (GetParam(66, chbx_Additional_Manipulations) && ((filed.Has_JVocals == "Yes" && HasJVocals != "Yes")
                    //|| (HasJVocals == "Yes" && filed.Has_JVocals != "Yes") || (HasJVocals == "Yes" && filed.Has_JVocals == "Yes")
                    /*66.Duplicate manag.ignores Medley Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Medley == "Yes" && IsMedley != "Yes")
                    || (IsMedley == "Yes" && filed.Is_Medley != "Yes") || (IsMedley == "Yes" && filed.Is_Medley == "Yes")
                    //66.Duplicate manag.ignores Multistrings Songs 
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_MultiStrings == "Yes" && IsMultiStrings != "Yes")
                    || (IsMultiStrings == "Yes" && filed.Is_MultiStrings != "Yes") || (IsMultiStrings == "Yes" && filed.Is_MultiStrings == "Yes")
                    /*66.Duplicate manag.ignores Deluxe Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Deluxe == "Yes" && IsDeluxe != "Yes")
                    || (IsDeluxe == "Yes" && filed.Is_Demo != "Yes") || (IsDeluxe == "Yes" && filed.Is_Deluxe == "Yes")
                    /*66.Duplicate manag.ignores GreatestHits Songs */
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_GreatestHits == "Yes" && IsGreatestHits != "Yes")
                    || (IsGreatestHits == "Yes" && filed.Is_GreatestHits != "Yes") || (IsGreatestHits == "Yes" && filed.Is_GreatestHits == "Yes")
                    //66.Duplicate manag.ignores Midi Songs HasFeaturing
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_Midi == "Yes" && IsMidi != "Yes")
                    || (IsMidi == "Yes" && filed.Is_Midi != "Yes") || (IsMidi == "Yes" && filed.Is_Midi == "Yes")
                    //66.Duplicate manag.ignores GameSoundtrack Songs 
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_GameSoundtrack == "Yes" && IsGameSoundtrack != "Yes")
                    || (IsGameSoundtrack == "Yes" && filed.Is_GameSoundtrack != "Yes") || (IsGameSoundtrack == "Yes" && filed.Is_GameSoundtrack == "Yes")
                    //66.Duplicate manag.ignores TV Theme Songs 
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_TVTheme == "Yes" && IsTVTheme != "Yes")
                    || (IsTVTheme == "Yes" && filed.Is_TVTheme != "Yes") || (IsTVTheme == "Yes" && filed.Is_TVTheme == "Yes")
                    //66.Duplicate manag.ignores Amateur Cover Songs
                    || (GetParam(66, chbx_Additional_Manipulations) && ((filed.Is_AmateurCover == "Yes" && IsAmateurCover != "Yes")
                    || (IsAmateurCover == "Yes" && filed.Is_AmateurCover != "Yes") || (IsAmateurCover == "Yes" && filed.Is_AmateurCover == "Yes")
                    )))))))))))))))))))))))))))))))))))))))
                {
                    dupli_reason += "autom: Initially assed as alternate cause multitrack/live/aCOUSTIC/."; timestamp = UpdateLog(timestamp, dupli_reason, true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    return "Alternate;" + dupli_reason;/*autom: alt*/
                }
            }
            if (GetParam(50, chbx_Additional_Manipulations) && j == 0) return ";" + dupli_reason + " no automated filterin";

            if (GetParam(83, chbx_Additional_Manipulations)) return "Ignore;" + dupli_reason + " . Duplication defaulted by Option 83." +
                    "";/*(chbx_DefaultDB.Checked == true ? MyAppWD : */
            frm_Duplicates_Management frm1 = new frm_Duplicates_Management(filed, datas, Fauthor, tkversion, DD, Bass, Guitar, Combo,
                Rhythm, Lead, Vocalss, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, xmlhlist, jsonhlist,
                txt_DBFolder.Text, clist, dlist, newold, Is_Original, altver,
                txt_RocksmithDLCPath.Text, GetParam(39, chbx_Additional_Manipulations), GetParam(40, chbx_Additional_Manipulations),
                fsz, unpackedDir, Is_MultiTrack == "No" ? "" : Is_MultiTrack, MultiTrack_Version, FileDate, title_duplic, platform, IsLive == "No" ? "" : IsLive, LiveDetails, IsAcoustic == "No" ? "" : IsAcoustic,
                cnb, dupli_reason, lengty, allothers, 0, cxmlhlist, snghlist, filehash
                , IsInstrumental == "No" ? "" : IsInstrumental, IsSoundtrack == "No" ? "" : IsSoundtrack, IsFullAlbum == "No" ? "" : IsFullAlbum, IsSingle == "No" ? "" : IsSingle,
                IsEP == "No" ? "" : IsEP, IsUncensored == "No" ? "" : IsUncensored, IsRemastered == "No" ? "" : IsRemastered
                , InTheWorks == "No" ? "" : InTheWorks, IsKaraoke == "No" ? "" : IsKaraoke, IsDemo == "No" ? "" : IsDemo, HasFeaturing == "No" ? "" : HasFeaturing,
                IsRemix == "No" ? "" : IsRemix, IsCover == "No" ? "" : IsCover, IsMedley == "No" ? "" : IsMedley, IsMultiStrings == "No" ? "" : IsMultiStrings
                , IsDeluxe == "No" ? "" : IsDeluxe, IsGreatestHits == "No" ? "" : IsGreatestHits, HasCapo == "No" ? "" : HasCapo
                , IsMidi == "No" ? "" : IsMidi, IsGameSoundtrack == "No" ? "" : IsGameSoundtrack, IsTVTheme == "No" ? "" : IsTVTheme, IsAmateurCover == "No" ? "" : IsAmateurCover
                , cnc);//);

            frm1.ShowDialog();
            if (frm1.Author != author) if (frm1.Author == "Custom Song Creator" && GetParam(57, chbx_Additional_Manipulations)) author = "";
                else author = frm1.Author == "Custom Song Creator" && GetParam(47, chbx_Additional_Manipulations) ? "Custom Song Creator" : frm1.Author;
            if (frm1.Description != description) description = frm1.Description;
            if (frm1.Comment != comment) comment = frm1.Comment;
            if (frm1.Title != SongDisplayName)
                if (GetParam(46, chbx_Additional_Manipulations)) SongDisplayName = frm1.Title;
            if (frm1.Version != tkversion) PackageVersion = frm1.Version;
            if (frm1.DLCID != Namee) Namee = frm1.DLCID;
            if (frm1.Is_Alternate != Is_Alternate) Is_Alternate = frm1.Is_Alternate;
            if (frm1.Title_Sort != Title_Sort) Title_Sort = frm1.Title_Sort;
            if (frm1.Artist != Artist) Artist = frm1.Artist;
            if (frm1.ArtistSort != ArtistSort) ArtistSort = frm1.ArtistSort;
            if (frm1.yearalbum != AlbumYear) AlbumYear = frm1.yearalbum;
            if (frm1.albumsort != AlbumSort) AlbumSort = frm1.albumsort;
            if (frm1.Album != Album) Album = frm1.Album;
            if (frm1.Alternate_No != Alternate_No) Alternate_No = frm1.Alternate_No;
            if (frm1.AlbumArtPath != AlbumArtPath) AlbumArtPath = frm1.AlbumArtPath;
            if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
            if (frm1.MultiT != "") Is_MultiTrack = frm1.MultiT;
            if (frm1.MultiTV != "") MultiTrack_Version = frm1.MultiTV;

            if (frm1.isLive != "") IsLive = frm1.isLive;
            if (frm1.liveDetails != "") LiveDetails = frm1.liveDetails;
            if (frm1.isAcoustic != "") IsAcoustic = frm1.isAcoustic;
            if (frm1.isInstrumental != "") IsInstrumental = frm1.isInstrumental;
            if (frm1.isSoundtrack != "") IsSoundtrack = frm1.isSoundtrack;
            if (frm1.isFullAlbum != "") IsFullAlbum = frm1.isFullAlbum;
            if (frm1.isSingle != "") IsSingle = frm1.isSingle;
            if (frm1.isEP != "") IsEP = frm1.isEP;
            if (frm1.isUncensored != "") IsUncensored = frm1.isUncensored;
            if (frm1.isRemastered != "") IsRemastered = frm1.isRemastered;
            if (frm1.isCover != "") IsCover = frm1.isCover;
            if (frm1.isDemo != "") IsDemo = frm1.isDemo;
            if (frm1.isRemix != "") IsRemix = frm1.isRemix;
            if (frm1.isKaraoke != "") IsKaraoke = frm1.isKaraoke;
            if (frm1.hasFeaturing != "") HasFeaturing = frm1.hasFeaturing;
            if (frm1.isMedley != "") IsMedley = frm1.isMedley;
            if (frm1.isMultiStrings != "") IsKaraoke = frm1.isMultiStrings;
            if (frm1.isDeluxe != "") IsDeluxe = frm1.isDeluxe;
            if (frm1.isGreatestHits != "") IsGreatestHits = frm1.isGreatestHits;
            if (frm1.isMidi != "") IsMidi = frm1.isMidi;
            if (frm1.isGameSoundtrack != "") IsGameSoundtrack = frm1.isGameSoundtrack;
            if (frm1.isTVTheme != "") IsTVTheme = frm1.isTVTheme;
            if (frm1.isAmateurCover != "") IsAmateurCover = frm1.isAmateurCover;

            if (frm1.YouTube_Link != "") YouTube_Link = frm1.YouTube_Link;
            if (frm1.CustomsForge_Link != "") CustomsForge_Link = frm1.CustomsForge_Link;
            if (frm1.CustomsForge_Like != "") CustomsForge_Like = frm1.CustomsForge_Like;
            if (frm1.CustomsForge_ReleaseNotes != "") CustomsForge_ReleaseNotes = frm1.CustomsForge_ReleaseNotes;
            if (frm1.dupliID != "") dupliID = frm1.dupliID;
            if (frm1.ExistingTrackNo != "") ExistingTrackNo = frm1.ExistingTrackNo;
            IgnoreRest = false;
            if (frm1.IgnoreRest) IgnoreRest = frm1.IgnoreRest;

            timestamp = UpdateLog(timestamp, "REturing from child..", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var tst = "Ignore;manual decision : empty decision";
            if (frm1.Asses != "") tst = frm1.Asses;
            frm1.Dispose();
            timestamp = UpdateLog(timestamp, "REturing.. to import", true, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return tst;
        }

        private void txt_DBFolder_Leave(object sender, EventArgs e)
        {
            bool t = false;
            //if (DBChanged)
            t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);
            //DBChanged = true;
            SetImportNo();
            //return t; 
        }

        private void DLCManager_Load(object sender, EventArgs e)
        {
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            CreateLog(logPath);
            var tst = "Starting... " + startT; var timestamp = UpdateLog(starttmp, tst, true, c("dlcm_TempPath"), c("dlcm_Split4Pack"), "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            ChanginProfile = false;
            DLCManagerOpen();
        }

        private void DLCManager_Enter(object sender, EventArgs e)
        {
            DataSet ddzv = new DataSet(); var Select = "SELECT * FROM Main";
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            if (File.Exists(DB_Path))
            {
                try
                {
                    DataSet dsm = new DataSet();
                    dsm = SelectFromDB("Groups", Select, txt_DBFolder.Text, cnb, cnc);
                    //if (dsm.Tables.Count > 0) noOfRec = dsm.Tables[0].Rows.Count;
                    //if (noOfRec == 0)
                    //    using (OleDbDataAdapter da = new OleDbDataAdapter(Select, cnb))

                    //        da.Fill(ddzv, "Main");
                    //        da.Dispose();
                }
                catch (Exception ex) { ShowConnectivityError(ex, ""); }/*, lbl_Access*/
            }
            else
            {
                ErrorWindow frm1 = new ErrorWindow("Missing Database from " + txt_RocksmithDLCPath.Text + ". Please add it or use the default Empty DB.", ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]], "Error @Import", false, false, true, "", "", "");
                frm1.ShowDialog();
            }

            CheckJava();

            var d1 = WwiseInstalled("If you want to use DLCManager audio analysing & fixing & converion tools please download");

            //DLCManager;
        }

        private void rbtn_Population_PackNO_CheckedChanged(object sender, EventArgs e)
        {
            RefreshSelectedStat(rbtn_Population_PackNO.Checked);
            //Add packsplit//populaet the Group  Dropdown
            //DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT Max(Split4Pack) FROM Main ", txt_DBFolder.Text, cnb, cnc);
            //var norec = ds.Tables.Count > 0 ? (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
        }

        private void txt_NoOfSplits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtn_Population_PackNO.Checked) RefreshSelectedStat(rbtn_Population_PackNO.Checked);
        }

        private void txt_RocksmithDLCPath_KeyDown(object sender, KeyEventArgs e)
        {
            ImportPathChanged();
        }
        public void ImportPathChanged()
        {
            if (DirectoryExists(txt_RocksmithDLCPath.Text))
            {
                if (txt_RocksmithDLCPath.Text.Length > 0) if ((txt_RocksmithDLCPath.Text.Substring(txt_RocksmithDLCPath.Text.Length - 1, 1) == "\\")) txt_RocksmithDLCPath.Text = txt_RocksmithDLCPath.Text.Substring(0, txt_RocksmithDLCPath.Text.Length - 1);
                SetImportNo();
                if (SaveOK != "") SaveSettings();
            }
        }

        private void txt_DBFolder_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_RocksmithDLCPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            if (DirectoryExists(txt_RocksmithDLCPath.Text))
            {
                if (txt_RocksmithDLCPath.Text.Length > 0) if ((txt_RocksmithDLCPath.Text.Substring(txt_RocksmithDLCPath.Text.Length - 1, 1) == "\\")) txt_RocksmithDLCPath.Text = txt_RocksmithDLCPath.Text.Substring(0, txt_RocksmithDLCPath.Text.Length - 1);
                SetImportNo();
                if (SaveOK != "") SaveSettings();
            }
        }

        private void txt_TempPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            var Temp_Path_Import = txt_TempPath.Text;
            var dflt_Path_Import = txt_TempPath.Text + "\\0_to_temp";
            var old_Path_Import = txt_TempPath.Text + "\\0_old";
            var dataPath = txt_TempPath.Text + "\\0_data";
            var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
            var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
            var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
            var repacked_Path = txt_TempPath.Text + "\\0_repacked";
            var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
            var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
            var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
            var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
            var Log_PSPath = txt_TempPath.Text + "\\0_log";
            var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
            var Archive_Path = txt_TempPath.Text + "\\0_archive";
            var Temp_Path = txt_TempPath.Text + "\\0_temp";
            var log_Path = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string pathDLC = txt_RocksmithDLCPath.Text; DialogResult res = new DialogResult();
            if (DirectoryExists(txt_TempPath.Text))
            {
                if (txt_TempPath.Text.Length > 0) if ((txt_TempPath.Text.Substring(txt_TempPath.Text.Length - 2, 2) != "\\0"))
                    {
                        txt_TempPath.Text = txt_TempPath.Text + "\\0";

                        if (!DirectoryExists(txt_TempPath.Text)) res = CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks,
                            pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);
                        if (res != DialogResult.No && res != DialogResult.Yes)
                            return;
                    }

            }
            else
            {
                if (DirectoryExists(txt_TempPath.Text)) res = CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks,
                    pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);
                if (res != DialogResult.No && res != DialogResult.Yes)
                    return;
            }
            if (SaveOK != "") SaveSettings();
        }

        private void Chbx_Configurations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChanginProfile = false;
            ChangeProfile();
            ChanginProfile = true;
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveOK = "Ok";
            SaveSettings();//Save settings
        }

        private void Txt_DBFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            var t = DBPathChange(txt_DBFolder.Text, chbx_Configurations.Text);
            DBChanged = false;
        }

        private void Btn_Album_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Album_Sort: " + Manipulate_strings(txt_Album_Sort.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, false, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void Btn_Lyric_Info_Click(object sender, EventArgs e)
        {
            SongRecord = UtilitiesFunctions.GetRecord_s("SELECT * FROM Main ", cnb, cnc);
            lbl_PreviewText.Text = "Lyric_info: " + Manipulate_strings(txt_Lyric_Info.Text, 0, false, false, false, SongRecord, "[", "]", ConfigRepository.Instance()["dlcm_AdditionalManipul54"].ToLower() == "yes" ? true : false, false, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
        }

        private void Cbx_Activ_Album_Sort_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Album_Sort.Checked == false)
            {
                txt_Album_Sort.Enabled = false;
                cbx_Album_Sort.Enabled = false;
            }
            else
            {
                txt_Album_Sort.Enabled = true;
                cbx_Album_Sort.Enabled = true;
            }
        }

        private void Cbx_Activ_Lyric_Info_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Activ_Lyric_Info.Checked == false)
            {
                txt_Lyric_Info.Enabled = false;
                cbx_Lyric_Info.Enabled = false;
            }
            else
            {
                txt_Lyric_Info.Enabled = true;
                cbx_Lyric_Info.Enabled = true;
            }
        }

        private void Txt_NoOfSplits_DropDown(object sender, EventArgs e)
        {
            //ConfigRepository.Instance()["dlcm_Split4Pack"] = txt_NoOfSplits.Text;
            ////Add packsplit
            ////populate the Group  Dropdown
            //DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT Max(Split4Pack) FROM Main ", txt_DBFolder.Text, cnb, cnc);
            //var norec = ds.Tables.Count > 0 ? (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            //if (norec > 0)
            //{
            //    var a = txt_NoOfSplits.Text;
            //    //remove items
            //    if (txt_NoOfSplits.Items.Count > 0)
            //    {
            //        txt_NoOfSplits.DataSource = null;
            //        for (int k = txt_NoOfSplits.Items.Count - 1; k >= 0; --k)
            //        {
            //            if (!txt_NoOfSplits.Items[k].ToString().Contains("--"))
            //            {
            //                txt_NoOfSplits.Items.RemoveAt(k);
            //            }
            //        }
            //    }
            //    //add items
            //    txt_NoOfSplits.DataSource = null;
            //    for (int j = 1; j <= norec; j++) txt_NoOfSplits.Items.Add(j.ToString());
            //}
        }

        private void btn_Album2SortA_Click(object sender, EventArgs e)
        {
            txt_DBFolder.Text = txt_TempPath.Text;
        }

        private void txt_RocksmithDLCPath_Leave(object sender, EventArgs e)
        {
            if (DirectoryExists(txt_RocksmithDLCPath.Text))
            {
                if (txt_RocksmithDLCPath.Text.Length > 0) if ((txt_RocksmithDLCPath.Text.Substring(txt_RocksmithDLCPath.Text.Length - 1, 1) == "\\")) txt_RocksmithDLCPath.Text = txt_RocksmithDLCPath.Text.Substring(0, txt_RocksmithDLCPath.Text.Length - 1);
                SetImportNo();
                if (SaveOK != "") SaveSettings();
            }
        }

        private void cbx_Lyric_Info_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Lyric_Info.Text += cbx_Lyric_Info.Items[cbx_Lyric_Info.SelectedIndex].ToString();
        }

        private void btn_FilterParams_Click(object sender, EventArgs e)
        {
            int i;
            if (FiltrParams || chbx_Additional_Manipulations.SelectedIndex + 1 >= chbx_Additional_Manipulations.Items.Count) i = chbx_Additional_Manipulations.SelectedIndex + 1;
            else i = 0;
            FiltrParams = true;

            var index = 0;
            //int index = chbx_Additional_Manipulations.Items.IndexOf(txt_FilterParams.Text);
            for (int j = i; j < chbx_Additional_Manipulations.Items.Count; j++)
                if ((chbx_Additional_Manipulations.Items[j].ToString().ToLower()).IndexOf(txt_FilterParams.Text.ToLower()) >= 0)
                {
                    index = j;
                    break;
                }
            if (index >= 0) chbx_Additional_Manipulations.SetSelected(index, true);
        }

        private void txt_FilterParams_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            btn_FilterParams_Click(null, null);
        }

        private void btn_Retail_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveOK = "OK"; SaveSettings(); SaveOK = "";
            Cache frm = new Cache(txt_DBFolder.Text, txt_TempPath.Text, txt_RocksmithDLCPath.Text, GetParam(39, chbx_Additional_Manipulations), GetParam(40, chbx_Additional_Manipulations), cnb, cnc);
            frm.ShowDialog();
        }

        private void txt_NoOfSplits_ValueChanged(object sender, EventArgs e)
        {
            RefreshSelectedStat(rbtn_Population_PackNO.Checked);
        }

        private void cbx_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Filtertxt = cbx_Groups.Text;
            ConfigRepository.Instance()["dlcm_FilterCompound"] = chbx_FilterCompound.Checked ? "Yes" : "No";
            if (Filtertxt == "") return; /*SaveSettings();*/

            //var i = databox.SelectedCells.Count == 0 ? 0 : databox.SelectedCells[0].RowIndex;
            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = SearchCmd;/*, "Part of No Group", "Part of Any Group" */
            SearchCmd = GetFilter(Filtertxt, SearchCmd, 0, SearchCmdf, cnb, cbx_Groups.Text, "\"" + GetFormat(chbx_PC.Checked, chbx_Mac.Checked, chbx_PS4.Checked, chbx_iOS.Checked, chbx_XBOX360.Checked, chbx_PS3.Checked) + "\""
                , null, cnc);

            if (Filtertxt == "Sorted by Groups value/Group added date") SearchCmd = SearchCmdf;

            RefreshSelectedStat(rbtn_Population_PackNO.Checked);
            chbx_FilterCompound.Checked = false;
        }

        private void cbx_Groups_DropDown_1(object sender, EventArgs e)
        {
            ConfigRepository.Instance()["dlcm_FilterPrevious"] = cbx_Groups.Text;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var paath = c("general_rs2014path");
            var xx = "";
            if (File.Exists(paath)) xx = Path.Combine(paath, "Rocksmith2014.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Rcoksmith or add path to Conbfig of tool if you want to run it with this button.", c("dlcm_UltraStarCreator_www"), "Missing UltraStar Creator", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = paath;
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx)) //&& File.Exists(replace(c("dlcm_DBFolder")))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
        }

        private void btn_Debug_Click(object sender, EventArgs e)
        {
            //CleanAlternates_and_MultiplyGroups(cnb, cnc, pB_ReadDLCs);
            //MultiplyMetaAlbum(cnb, cnc, pB_ReadDLCs);
            Re_ReadToolkitinfoInfo(pB_ReadDLCs);

        }
        private void Re_ReadToolkitinfoInfo(ProgressBar pB_ReadDLCs)
        {
            var txt_AuthorText = ""; var txt_CDLC_NameText = ""; var txt_TrackNoText = ""; var txt_VersionText = ""; var txt_CDLCIDText = ""; var txt_EoFPathText = ""; var txt_YBLinkText = ""; var txt_BasedOnYBText = "";
            var txt_BasedOnCFText = ""; var txt_TabLinksText = ""; var txt_SpotifyText = ""; var txt_DescriptionText = ""; var txt_toDosText = ""; var txt_ToneDetailsText = ""; var chbx_SaveInVerisonInfoText = false;
            var chbx_SaveInDBTxet = false; var chbx_SaveRemotelyText = false; var txt_PackageDate = ""; var txt_UpdateDate = ""; var txt_BasedOn_GP = "";
            var SaveRemotelyPath = "";

            var sel = "SELECT Folder_Name, ID FROM Main "/*WHERE */
                + " ;";
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRecs = GetNoRec(SongRecord, cnb, cnc);
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;
            var j = 0;
            for (var i = 0; i < noOfRecs; i++)
            {

                var t = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(); var IDD = SongRecord.Tables[0].Rows[i].ItemArray[1].ToString();

                var versionffile = Directory.GetFiles(t, "toolkit.version", System.IO.SearchOption.AllDirectories);
                var PackageComment = "";
                if (versionffile.Length > 0)
                {
                    PackageComment = ReadPackageComment(versionffile[0]);
                }
                //if (info.ToolkitInfo != null)
                //    if (PackageComment != "") ;/*info.ToolkitInfo.PackageComment*/
                if (PackageComment.IndexOf(";") > 0)
                {
                    string[] agg = PackageComment.Split(';');// info.ToolkitInfo.PackageComment.ToString().Split(';');
                    txt_AuthorText = agg[0]; txt_CDLC_NameText = agg[1]; txt_TrackNoText = agg[2]; txt_VersionText = agg[3]; txt_CDLCIDText = agg[4];
                    txt_EoFPathText = agg[5]; txt_YBLinkText = agg[6]; txt_BasedOnYBText = agg[7];
                    txt_BasedOnCFText = agg[8]; txt_TabLinksText = agg[9]; txt_SpotifyText = agg[10]; txt_DescriptionText = agg[11];
                    txt_toDosText = agg[12]; txt_ToneDetailsText = agg[13]; chbx_SaveInVerisonInfoText = agg[14] == "Yes" ? true : false;
                    chbx_SaveInDBTxet = agg[15] == "Yes" ? true : false; chbx_SaveRemotelyText = agg[16] == "Yes" ? true : false;
                    SaveRemotelyPath= agg[17]; txt_PackageDate = agg[18]; txt_UpdateDate = agg[19]; txt_BasedOn_GP = agg[20];//ConfigRepository.Instance()["dlcm_EoFPath"] + ";"
                    
                    var command = cnb.CreateCommand();
                    command.CommandText = "UPDATE Main SET ";
                    command.CommandText += "YouTube_Link = @param62, ";
                    command.CommandText += "Spotify_Song_ID = @param77, ";
                    command.CommandText += "BasedOn_Youtube = @param106, ";
                    command.CommandText += "BasedOn_CF = @param107, ";
                    command.CommandText += "BasedOn_Tabs = @param108, ";
                    command.CommandText += "ToDos = @param109, ";
                    command.CommandText += "ToneDetails = @param110, ";
                    command.CommandText += "PackageDetails = @param111, ";
                    command.CommandText += "PackingDate = @param112, ";
                    command.CommandText += "UpdateVersionDate = @param113, ";
                    command.CommandText += "BasedOn_GP = @param120, ";
                    command.CommandText += "EoFPath= @param128 ";
                    //command.CommandText += "YouTube_Link = @param129 ";
                    //command.CommandText += "Spotify_Song_ID = @param130 ";
                    command.CommandText += " WHERE ID = " + IDD;

                    
                    command.Parameters.AddWithValue("@param62", txt_YBLinkText ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param77", txt_SpotifyText ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param86", ybSAddress == null ? DBNull.Value.ToString() : ybRAddress);
                    command.Parameters.AddWithValue("@param106", BasedOn_Youtube ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param107", BasedOn_CF ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param108", BasedOn_Tabs ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param109", ToDos ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param110", ToneDetails ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param111", PackageDetails ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param112", txt_PackageDate ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param113", txt_UpdateDate ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param120", txt_BasedOn_GP ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param128", txt_EoFPathText ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param129", txt_YBLinkText ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param130", txt_SpotifyText ?? DBNull.Value.ToString());
                    command.CommandType = CommandType.Text;
                    UpdateDBbyExecuteNonQuery(command, cnb, cnc);

                }
                pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing BitRate", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }


    }
    //private void DLCManagerOpen(object sender, EventArgs e)
    //{

    //}
}