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
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.DLCPackage.Manifest2014;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using RocksmithToolkitLib.Ogg;
using System.Threading.Tasks;
using System.Threading;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {
        //bcapi
        private bool loading = false;
        //public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
        public BackgroundWorker bwConvert = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi1  
        private StringBuilder errorsFound;//bcapi1
        string dlcSavePath = "";
        int no_ord = 0;
        string Groupss = "";
        public string netstatus = "NOK";
        DLCPackageData data;
        public string SaveOK = "";

        //Processing global vars
        bool duplit = false;
        int dupliNo = 0;
        int dupliPrcs = 0;
        string[,] dupliSongs = new string[2, 10000];
        bool stopp = false;
        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        static string MyAppWD = AppWD; //when removing DDC

        DateTime timestamp;
        string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
        OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
        //OLE DB Services=-2;Mode=Read;

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

        //        00. @Pack Add Increment to all songs Title
        //01. @Pack Add Increment to all songs Title per artist
        //02. @Pack Make all DLC IDs unique(&save)
        //03. @Pack Remove DD
        //04. <Import and remove DD only for Bass>
        //05. @Pack Remove DD only for Bass Guitar
        //06. <Remove the 4sec of the Preview song>
        //07. @Pack skipp Broken songs
        //08. @Pack Name to cross-platform Compatible Filenames
        //09. @Pack Add Preview if missing 00:30 for 30sec(&save)
        //10. @Pack Make all DLC IDs unique
        //11. <@PackAdd DD(5 Levels)>
        //12. Add DD(5 Levels) when missing
        //13. Import all Duplicates as Alternates
        //14. Import any Custom as Alternate if an Original exists
        //15. Move the Imported files to temp/0_old
        //16. Import with Artist/Title same as Artist/Title Sort
        //17. Repack with Artist/Title same as Artist/Title Sort
        //18. <Import without The/Die at the beginning of Artist/Title Sort>
        //19. <Pack without The/Die at the beginning of Artist/Title Sort>
        //20. Import with the The/Die at the end of Artist/Title Sort
        //21. Pack with The/Die at the end of Title Sort
        //22. Import with the The/Die only at the end of Artist Sort
        //23. Pack with The/Die only at the end of Artist Sort
        //24. @Import Use translation tables for naming standardization
        //25. If Original don't add QAs (NOs;DLC/ORIG;etc.)
        //26. When packing Add 5 Levels of DD only to Guitar tracks
        //27. Convert and Transfer/FTP
        //28. If Original don't add QAs (NOs;DLC/ORIG;etc.) except for File Names
        //29. When NOT importing a Duplicate Move it to _duplicate
        //30. When NOT importing a broken song Move it to _broken
        //31. When removing DD use internal logic not DDC
        //32. When importing alternates add newer/older instead of alt.0author
        //33. Forcibly Update Import location of all DB fields
        //34. @Import Add Preview if missing(lenght as per config)
        //35. Remove illegal characters from Songs Metadata
        //36. Keep the Uncompressed Songs superorganized
        //37. Import other formats but PC, as well(as duplicates)
        //38. Import only the unpacked songs already in the "0/" Temp folder
        //39. Encrypt PS3 Retails songs, with External tool
        //40. Delete ORIG HSAN/OGG when Packing Retails songs
        //41. Try to get Track No.from Spotify 
        //42. Save Log After Import(Imported Folder)
        //43. @Import Set the DLCID autom
        //44. @Pack Set the DLCID autom
        //45. <Convert Originals>
        //46. Duplicate Mangement, Title added info is inbetween[]
        //47. Add New Toolkit v.and RePackedByAuthor
        //48. @Import Remove Multitrack/Live info from Title
        //49. @Pack Also Copy/FTP
        //50. @Import place "obvious" duplicates at the end of the Process
        //51. @Import Overrite the XML
        //52. @Pack keep Bass DD if indicated so
        //53. @Pack keep All DD if indicated so
        //54. @Pack consider All songs as beta (place them top of the list)
        //55. Gen Preview if Preview=Audio or Preview is longer than config
        //56. Duplicate manag ignores Multitracks
        //57. Don't save Author when generic (i.e. Custom Song Creator)
        //58. @Pack try to get Track No again(&don't save)
        //59. @Pack try to get Track No again (&save)
        //60. @Rebuild don't overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)
        //61. @Rebuild don't overwrite Standard Song Info (Cover,Year)
        //62. <@Pack duplicate singleTracks L->R / R->L>
        //63. @Pack Remove Remote File if GameData has been read
        //64. @Pack ONLY Copy/FTP the Last Packed song
        //65. @Pack ONLY Copy/FTP the Initially Imported song
        //66. Duplicate manag.ignores Live Songs
        //67. Import duplicates (hash)
        //68. Delete obvious duplicates (hash) during dupli assesment
        //69. Compress AudioFiles to 128VBR @Pack/Import if bigger than 136k
        //70. @Repack pack Preview (bugfix)
        //71. <@Import/Repack check if Original flag is in the Official list and correct>
        //72. <Import other formats but PC, as well (as standalone)>
        //73. Add Track Info&Comments beginning of Lyrics
        //74. Add Track start into Vocals
        //75. Copy to \0\0_Old(Overwrites 15 Move to old)
        //76. Include Tones/arangements Db changes
        //77. After Import open MainDB
        //78. @Import Fix Audio Issues at end
        //99. @Pack>
        //beer

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
            if (File.Exists(txt_DBFolder.Text)) //+ "\\Files.accdb"
            {
                var SearchCmd = "SELECT * FROM Main u ";
                DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB(db, SearchCmd, txt_DBFolder.Text, cnb);
                var noOfRec = 0;
                if (dsz1.Tables.Count > 0) noOfRec = dsz1.Tables[0].Rows.Count;

                SearchCmd = "SELECT * FROM " + db + " u " + " WHERE " + txt + ";";
                DataSet dsz2 = new DataSet(); dsz2 = SelectFromDB(db, SearchCmd, txt_DBFolder.Text, cnb);

                var noOfSelRec = 0;
                if (dsz2.Tables.Count > 0) noOfSelRec = dsz2.Tables[0].Rows.Count;
                lbl_NoRec2.Text = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records.";
            }
        }

        public DLCManager()
        {
            //var a = ConfigRepository.Instance()["dlcm_DBFolder"];
            InitializeComponent();
            //a = ConfigRepository.Instance()["dlcm_DBFolder"];

            //Enable Preview generation
            if (ConfigRepository.Instance()["general_wwisepath"] == "") ConfigRepository.Instance()["general_wwisepath"] = "C:\\Program Files (x86)\\Audiokinetic\\Wwise 2017.1.2.6361";// 2017.1.2.6361";
            if (!Directory.Exists(ConfigRepository.Instance()["general_rs2014path"])) ConfigRepository.Instance()["general_rs2014path"] = "D:\\SteamLibrary\\steamapps\\common\\Rocksmith2014";
            if (ConfigRepository.Instance()["general_defaultauthor"] == "") ConfigRepository.Instance()["general_defaultauthor"] = "catara";

            //C:\\Program Files (x86)\\Audiokinetic\\Wwise v2013.2.10 build 4884";

            //a = ConfigRepository.Instance()["dlcm_DBFolder"];

            //Colored = ConfigRepository.Instance().GetBoolean("cgm_coloredinlay");
            // Saving for later
            DLCManagerOpen();
            // Generate package worker
            //a = ConfigRepository.Instance()["dlcm_DBFolder"];

            bwConvert.DoWork += new DoWorkEventHandler(ConvertWEM);

            bwConvert.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwConvert.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwConvert.WorkerReportsProgress = true;

            if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
            if (!File.Exists(txt_DBFolder.Text))
                chbx_DefaultDB.Checked = true;
            if (!Directory.Exists(txt_TempPath.Text)) txt_TempPath.Text = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\Temp";
            //cnb.Open();
            //SaveSettings();  
            //Populate Se3lectee songs
            RefreshSelectedStat("Main", "1=1");
        }

        public void DLCManagerOpen()
        {
            //txt_DBFolder.Text = "1";
            //var a = ConfigRepository.Instance()["dlcm_DBFolder"];
            txt_RocksmithDLCPath.Text = ConfigRepository.Instance()["dlcm_RocksmithDLCPath"];
            txt_TempPath.Text = ConfigRepository.Instance()["dlcm_TempPath"];

            txt_Title.Text = ConfigRepository.Instance()["dlcm_Title"];
            txt_Title_Sort.Text = ConfigRepository.Instance()["dlcm_Title_Sort"];
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
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul74"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(74, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(74, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul75"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(75, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(75, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul76"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(76, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(76, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul77"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(77, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(77, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul78"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(78, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(78, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul79"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(79, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(79, CheckState.Unchecked);
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "Yes") chbx_Additional_Manipulations.SetItemCheckState(80, CheckState.Checked);
            else chbx_Additional_Manipulations.SetItemCheckState(80, CheckState.Unchecked);

            //a = ConfigRepository.Instance()["dlcm_DBFolder"];
            txt_DBFolder.Text = ConfigRepository.Instance()["dlcm_DBFolder"];//Make sure we change this at end as this will save
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
            var t = 0;
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
            //using (var fbd = new VistaFolderBrowserDialog())
            //{
            //    if (fbd.ShowDialog() != DialogResult.OK)
            //        return;
            //    var temppath = fbd.SelectedPath;
            //    txt_DBFolder.Text = temppath;
            //}
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.IO.StreamReader sr = new
                //   System.IO.StreamReader(openFileDialog1.FileName);
                txt_DBFolder.Text = openFileDialog1.FileName;
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
                btn_Debbug.Visible = true;

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
                btn_Debbug.Visible = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, "Starting SaveSetting", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            SaveSettings();// Saving for later
            ((MainForm)ParentForm).ReloadControls();
            timestamp = UpdateLog(timestamp, "Ending SaveSetting", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        public void SaveSettings()
        {
            if (SaveOK == "") return;
            ConfigRepository.Instance()["dlcm_Title"] = txt_Title.Text;
            ConfigRepository.Instance()["dlcm_Title_Sort"] = txt_Title_Sort.Text;
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
            ConfigRepository.Instance()["dlcm_netstatus"] = cbx_Groups.Text;
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
            ConfigRepository.Instance()["dlcm_AdditionalManipul74"] = chbx_Additional_Manipulations.GetItemChecked(74) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul75"] = chbx_Additional_Manipulations.GetItemChecked(75) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul76"] = chbx_Additional_Manipulations.GetItemChecked(76) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul77"] = chbx_Additional_Manipulations.GetItemChecked(77) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul78"] = chbx_Additional_Manipulations.GetItemChecked(78) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul79"] = chbx_Additional_Manipulations.GetItemChecked(79) ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul80"] = chbx_Additional_Manipulations.GetItemChecked(80) ? "Yes" : "No";

            //Save Profiles
            if (chbx_Configurations.SelectedIndex >= 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\";", txt_DBFolder.Text, cnb);
                var norec = ds.Tables.Count < 1 ? 0 : ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    //var cmd = "UPDATE Groups SET Groups=\"" + txt_RocksmithDLCPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Rocksmith\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"";
                    //UpdateDB("Groups", cmd, cnb);
                    //cmd = "UPDATE Groups SET Groups=\"" + txt_DBFolder.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"DB\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"";
                    //UpdateDB("Groups", cmd, cnb);
                    //cmd = "UPDATE Groups SET Groups=\"" + txt_TempPath.Text + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"Temp\" AND Profile_Name=\"" + chbx_Configurations.Text + "\"";
                    //UpdateDB("Groups", cmd, cnb);
                    var cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_Album"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_Album\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_Artist"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_Artist\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_ArtistSort\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_FileName"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_FileName\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_Title"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_Title\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Activ_TitleSort"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Activ_TitleSort\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul0"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul0\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul1"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul1\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul10"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul10\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul11"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul11\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul12"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul12\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul13"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul13\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul14"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul14\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul15"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul15\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul16"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul16\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul17"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul17\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul18"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul18\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul19"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul19\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul2"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul2\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul20"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul20\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul21"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul21\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul22"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul22\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul23"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul23\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul24"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul24\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul25"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul25\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul26"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul26\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul27"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul27\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul28"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul28\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul29"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul29\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul3"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul3\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul30"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul30\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul31"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul31\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul32"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul32\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul33"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul33\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul34"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul34\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul35"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul35\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul36"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul36\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul37"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul37\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul38"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul38\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul39"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul39\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul4"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul4\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul40"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul40\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul41"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul41\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul42"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul42\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul43"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul43\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul44"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul44\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul45"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul45\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul46"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul46\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul47"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul47\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul48"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul48\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul49"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul49\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul5"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul5\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul50"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul50\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul51"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul51\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul52"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul52\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul53"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul53\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul54"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul54\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul55"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul55\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul56"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul56\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul57"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul57\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul58"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul58\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul59"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul59\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul6"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul6\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul60"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul60\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul61"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul61\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul62"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul62\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul63"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul63\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul64"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul64\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul65"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul65\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul66"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul66\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul67"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul67\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul68"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul68\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul69"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul69\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul7"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul7\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul70"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul70\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul71"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul71\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul72"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul72\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul73"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul73\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul74"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul74\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul75"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul75\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul76\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul77"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul77\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul78"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul78\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul79"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul79\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul8"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul8\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul80"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul80\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul9"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_AdditionalManipul9\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Album"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Album\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Artist"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Artist\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Artist_Sort"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Artist_Sort\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_cbx_Groups"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_cbx_Groups\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_chbx_Mac"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_chbx_Mac\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_chbx_PC"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_chbx_PC\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_chbx_PS3"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_chbx_PS3\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_chbx_XBOX360"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_chbx_XBOX360\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_DBFolder"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_DBFolder\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Debug"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Debug\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_DefaultDB"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_DefaultDB\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_File_Name"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_File_Name\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_netstatus"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_netstatus\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_RocksmithDLCPath\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_TempPath"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_TempPath\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Title"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Title\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);
                    cmd = "UPDATE Groups SET Groups=\"" + ConfigRepository.Instance()["dlcm_Title_Sort"] + "\" WHERE CDLC_ID=\"" + fnn + "\" AND Type=\"Profile\" AND Comments=\"dlcm_Title_Sort\" AND Profile_Name=\"" + chbx_Configurations.Text + "\""; UpdateDB("Groups", cmd, cnb);

                }
            }
            ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] = txt_RocksmithDLCPath.Text;
            ConfigRepository.Instance()["dlcm_TempPath"] = txt_TempPath.Text;
            ConfigRepository.Instance()["dlcm_DBFolder"] = txt_DBFolder.Text;
            SaveOK = "OK";
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
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
            lbl_PreviewText.Text = "Title: " + Manipulate_strings(txt_Title.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
            lbl_PreviewText.Text = "Sort Title: " + Manipulate_strings(txt_Title_Sort.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
            lbl_PreviewText.Text = "Artist: " + Manipulate_strings(txt_Artist.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
            lbl_PreviewText.Text = "Sort Artist: " + Manipulate_strings(txt_Artist_Sort.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
            lbl_PreviewText.Text = "Album: " + Manipulate_strings(txt_Album.Text, 0, false, false, false, SongRecord, "[", "]");
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            SongRecord = GenericFunctions.GetRecord_s("SELECT * FROM Main ", cnb);
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

            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD + "\\AccessDB.accdb;" : txt_DBFolder.Text);
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path;
            //cnn = new SqlConnection(connectionString);
            //cnn.Open();
            //sql = "SELECT * FROM Main";
            //SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            //DataSet ds = new DataSet();
            //dscmd.Fill(ds);
            //dscmd.Dispose();

            DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT * FROM Main", txt_DBFolder.Text, cnb);
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
            SongRecord = GenericFunctions.GetRecord_s(cmd, cnb);

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
            var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD + "\\..\\AccessDB.accdb;" : txt_DBFolder.Text);

            var TempPath = txt_TempPath.Text;
            DeleteRecords(ids, cmd, DB_Path, TempPath, norows.ToString(), hash, cnb);
        }



        //public DateTime UpdateLog(DateTime dt, string txt, bool multith)
        //{
        //    StackFrame stackFrame = new System.Diagnostics.StackTrace(1).GetFrame(1);
        //    string fileName = stackFrame.GetFileName();
        //    string methodName = stackFrame.GetMethod().ToString();
        //    int lineNumber = stackFrame.GetFileLineNumber();

        //    Console.WriteLine("{0}({1}:{2})\n{3}", methodName, Path.GetFileName(fileName), lineNumber, txt);

        //    DateTime dtt = System.DateTime.Now;
        //    var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');

        //    //if (multith) rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text + methodName + Path.GetFileName(fileName) + lineNumber;

        //    // Write the string to a file.
        //    var log = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log";
        //    var fn = (logPath == null || !Directory.Exists(logPath) ? (Directory.Exists(log) ? log : AppWD + ConfigRepository.Instance()["dlcm_LogPath"]) : logPath) + "\\" + "current_temp.txt";
        //    // This text is always added, making the file longer over time
        //    // if it is not deleted.
        //    using (StreamWriter sw = File.AppendText(fn))
        //    {
        //        sw.WriteLine(dtt + " - " + ii + " - " + txt);
        //    }
        //    return dtt;
        //}

        // Read a Folder (clean temp folder)        // Decompress the PC DLCs
        // Read details and populate a DB (clean Import DB before, and only populate Main if not there already)
        public void btn_PopulateDB_Click(object sender, EventArgs e)
        {
            //cnb.Open();
            // 
            // pB_ReadDLCs
            // 
            //ProgressBar pB_ReadDLCs = new ProgressBar();
            //pB_ReadDLCs.Location = new System.Drawing.Point(8, 137);
            //pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            //pB_ReadDLCs.Maximum = 10000;
            //pB_ReadDLCs.Name = "pB_ReadDLCs";
            //pB_ReadDLCs.Size = new System.Drawing.Size(1045, 22);
            //pB_ReadDLCs.Step = 1;
            //pB_ReadDLCs.TabIndex = 263;
            //this.toolTip1.SetToolTip(pB_ReadDLCs, "Progress bar for different operations of CDLC Manager.");
            //this.Controls.Add(pB_ReadDLCs);
            // pB_ReadDLCs

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
            var Log_PSPath = txt_TempPath.Text + "\\0_log";
            var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
            var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
            string pathDLC = txt_RocksmithDLCPath.Text;

            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            if (File.Exists((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_temp.txt")) File.Copy((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_temp.txt", (logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_temp_" + startT + ".txt");
            var tst = "Starting... " + startT; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //var DB_Path = "";
            // var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";// "";

            var DB_Path = txt_DBFolder.Text;//+ "\\Files.accdb;";// "";
            var errr = true;
            //             timestamp = UpdateLog(timestamp, "Issues at decompressing WEMs or FAILED2 empty path", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            SaveSettings();
            tst = "end save settings ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            pB_ReadDLCs.Value = 0;


            tst = "end config reading..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            DialogResult result1 = DialogResult.No;
            DialogResult result2 = DialogResult.No;

            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !chbx_Additional_Manipulations.GetItemChecked(38)) //39.Use only unpacked songs already in the 0 / dlcpacks folder
            {
                result1 = MessageBox.Show("Are you sure you want to DELETE (to Recycle BIN) the following folders:\n\n" + txt_TempPath.Text + "\n0\\0_old\n0\\0_duplicate\n0\\0_repacked\n0\\0_broken\n" + log_Path + "\n0\\0_repacked\\PC\n0\\0_repacked\\PS3\n0\\0_repacked\\MAC\n0\\0_repacked\\XBOX360\n0\\dlcpacks\n0\\dlcpacks\\manifests\n0\\dlcpacks\\temp\n0\\dlcpacks\\manipulated", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
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
                        CleanFolder(txt_TempPath.Text + "\\0_log", "");
                        result2 = MessageBox.Show("Are you sure you want to DELETE Standardizations (&Spotify downloaded info)?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result2 == DialogResult.Yes) CleanFolder(txt_TempPath.Text + "\\0_albumCovers", "");
                        CleanFolder(broken_Path_Import, "");
                        CleanFolder(log_Path, "");
                        //Delete Folders
                        System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text);
                        foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                        {
                            try
                            {
                                if (dir.Name != "0_dlcpacks" && dir.Name != "0_broken" && dir.Name != "0_old" && dir.Name != "0_repacked" && dir.Name != "0_duplicate" && dir.Name != "0_log" && dir.Name != "0_albumCovers")
                                    //dir.Delete(true);
                                    //if (Directory.Exists(dir.FullName))
                                    DeleteDirectory(dir.FullName);
                            }
                            catch (Exception ex) { Console.Write(ex); }
                        }
                        if (Directory.Exists(txt_TempPath.Text + "\\0_dlcpacks"))
                        {
                            System.IO.DirectoryInfo downloadedMessageInfo7 = new DirectoryInfo(txt_TempPath.Text + "\\0_dlcpacks");
                            foreach (DirectoryInfo dir in downloadedMessageInfo7.GetDirectories())
                            {
                                try
                                {
                                    if (dir.Name != "temp" && dir.Name != "manipulated" && dir.Name != "manifests")
                                        //dir.Delete(true);
                                        //if (Directory.Exists(dir.FullName))
                                        DeleteDirectory(dir.FullName);
                                }
                                catch (Exception ex) { Console.Write(ex); }
                            }
                        }
                        //CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("l1009Can not delete folders:\n\n" + "0\n0\\0_old\n0\\0_duplicate\n0\\0_repacked\n0\\0_repacked\\PC\n0\\0_repacked\\PS3\n0\\0_repacked\\MAC\n0\\0_repacked\\XBOX360\n" + broken_Path_Import + "\n" + log_Path + "\n0\\dlcpacks\n0\\dlcpacks\\manifests\n0\\dlcpacks\\temp\n0\\dlcpacks\\manipulated\n" + AlbumCovers_PSPath + "\n" + Log_PSPath);
                }
            }
            //if (!(File.Exists(txt_TempPath.Text) && File.Exists(txt_TempPath.Text + "\\0_old") && File.Exists(txt_TempPath.Text + "\\0_repacked") && File.Exists(txt_TempPath.Text + "\\0_repacked\\PC") && File.Exists(txt_TempPath.Text + "\\0_repacked\\PS3") && File.Exists(txt_TempPath.Text + "\\0_repacked\\XBBOX360") && File.Exists(txt_TempPath.Text + "\\0_repacked\\MAC") && File.Exists(txt_TempPath.Text + "\\0_duplicate") && File.Exists(txt_TempPath.Text + "\\0_dlcpacks") && File.Exists(broken_Path_Import) && File.Exists(log_Path)))
            //{
            //    DialogResult result2 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Cancel Import command\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (result2 == DialogResult.Yes)
            DialogResult res = CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath);
            if (res != DialogResult.No && res != DialogResult.Yes)
                return;// Application.Exit();
            //    else if (result2 == DialogResult.No) return;
            //    else Application.Exit();
            //}
            // Clean temp log
            var fnl = (logPath == null || logPath == "" ? Log_PSPath : logPath) + "\\" + "current_temp.txt";
            StreamWriter sw = new StreamWriter(File.OpenWrite(fnl));
            sw.Write("Some stuff here");
            sw.Dispose();

            tst = "end folder Cleaning..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", cmdR, txt_DBFolder.Text, cnb);
                int noOfRecR = dsR.Tables[0].Rows.Count;
                tst = "Rebuilding" + noOfRecR + "/" + (noOfRecR) + " Songs already imported in MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                if (noOfRecR > 0)
                {
                    pB_ReadDLCs.Value = 0;
                    pB_ReadDLCs.Maximum = 2 * (noOfRecR - 1);
                }
                tst = "Rebuild select ended."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //Clean ImportDB
            DeleteFromDB("Import", "DELETE FROM Import;", cnb);
            tst = "Cleaning....Import table...."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            if (chbx_CleanDB.Checked && result1 == DialogResult.Yes)
            {
                DeleteFromDB("Main", "DELETE FROM Main;", cnb);
                DeleteFromDB("Arrangements", "DELETE FROM Arrangements;", cnb);
                DeleteFromDB("Tones", "DELETE FROM Tones;", cnb);
                DeleteFromDB("LogPacking", "DELETE FROM LogPacking;", cnb);
                DeleteFromDB("LogPackingError", "DELETE FROM LogPackingError; ", cnb);
                DeleteFromDB("LogImporting", "DELETE FROM LogImporting;", cnb);
                DeleteFromDB("LogImportingError", "DELETE FROM LogImportingError;", cnb);
                DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail;", cnb);
                DeleteFromDB("Tones_GearList", "DELETE FROM Tones_GearList;", cnb);
                result1 = MessageBox.Show("Are you sure you want to DELETE All Groups related entities(Profiles, Song Groups/Setlist)?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes) DeleteFromDB("Groups", "DELETE FROM Groups WHERE TYPE=\"Profile\";", cnb);
                DeleteFromDB("Import_AuditTrail", "DELETE FROM Import_AuditTrail;", cnb);
                if (result2 == DialogResult.Yes) DeleteFromDB("Standardization", "DELETE FROM Standardization;", cnb);
                DeleteFromDB("Cache", "DELETE FROM Cache;", cnb);
                DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"DLC\" or Type=\"Retail\";", cnb);
            }

            tst = DB_Path + " Cleaned tables"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            int i = 0;
            var ImportPackNo = "0";
            DataSet doz = new DataSet();
            doz = SelectFromDB("Main", "SELECT MAX(s.ID) FROM Main s;", txt_DBFolder.Text, cnb);
            var noOfRecx = doz.Tables[0].Rows.Count;
            int gh = doz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            if (noOfRecx > 0) ImportPackNo = (gh + 1).ToString();
            if (ImportPackNo == "" || ImportPackNo == "0") ImportPackNo = "1";
            var invalid = "No";

            if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder folder
            {
                //GetDirList and calcualte hash for the IMPORTED file
                string[] filez;
                if (chbx_Additional_Manipulations.GetItemChecked(37)) //38. Import other formats but PC, as well(separately of course)
                    filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
                else
                    filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");
                pB_ReadDLCs.Maximum = filez.Count();
                //string[] broken_fl; for (var j = 0; j < 10000; j++) { broken_fl[j] = 0; }
                //int bb = 0;
                foreach (string s in filez)
                {
                    invalid = "No";
                    if (s == "rs1compatibilitydisc_m.psarc" || s == "rs1compatibilitydisc_p_Pc.psarc" || s == "rs1compatibilitydlc_p.psarc" || s == "rs1compatibilitydlc_m.psarc") continue;
                    if (!s.IsValidPSARC())
                    {
                        //errorsFound.AppendLine(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)));
                        //MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)));
                        timestamp = UpdateLog(timestamp, "error at import " + String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (File.Exists(s.Replace(".psarc", ".invalid"))) File.Move(s.Replace(".psarc", ".invalid"), s);
                        //if (File.Exists(s)) File.Move(s.Replace(".psarc", ".invalid"), s);
                        invalid = "Yes";
                        //broken_fl[bb] = s;
                        ///bb++;
                        //continue;
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
                        timestamp = UpdateLog(timestamp, "error at import", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        ErrorWindow frm1 = new ErrorWindow("(1121)DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
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
                    tst = "File " + (i + 1) + " :" + s; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    // DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";


                    var ff = "-";
                    ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;

                    var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";
                    var plt = fi.FullName.GetPlatform().platform.ToString();
                    var insertvalues = "\"" + s + "\",\"" + fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\"" + FileHash + "\",\"" + fi.Length + "\",\"" + ff + "\",\"" + ImportPackNo + "\",\"" + (plt == "Pc" ? "0Pc" : plt) + "\",\"" + invalid + "\"";
                    InsertIntoDBwValues("Import", insertcmdd, insertvalues, cnb);

                    pB_ReadDLCs.Increment(1);
                    i++;
                }
                tst = "end populating Import..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //Delete duplicates(same HASH) from ImportDB
                var del = 0;
                var no = 0;
                DataSet dz = new DataSet(); dz = SelectFromDB("Import", "SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash = s.FileHash WHERE d.ID is not null GROUP BY s.FileHash;", txt_DBFolder.Text, cnb);
                no = dz.Tables[0].Rows.Count;
                //Remove duplicate DLCs from this Import
                DataSet drz = new DataSet(); drz = SelectFromDB("Import", "SELECT * FROM Import WHERE ID not IN(SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);", txt_DBFolder.Text, cnb);
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
                    DataSet dry = new DataSet(); dry = SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);", txt_DBFolder.Text, cnb);
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
                        DeleteFromDB("Import", "DELETE FROM Import WHERE FileHash IN (SELECT FileHash FROM Import_AuditTrail);", cnb);
                    }
                    tst = "end delete same hash from Import..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                    DataSet drh = new DataSet(); drh = SelectFromDB("Import", "SELECT FullPath,FileName FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);", txt_DBFolder.Text, cnb);

                    noOfRecs = drh.Tables[0].Rows.Count;

                    if (noOfRecs > 0)
                    {
                        for (i = 0; i <= noOfRecs - 1; i++)
                            File.Move(drh.Tables[0].Rows[i].ItemArray[0].ToString(), txt_TempPath.Text + "\\0_duplicate\\" + drh.Tables[0].Rows[i].ItemArray[1].ToString());

                        //Delete Duplicates from Current Import
                        DeleteFromDB("Import", "DELETE * FROM Import WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);", cnb);
                    }
                }

                tst = no + "/" + i + " Import files Inserted (excl. " + del + " duplicates)"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                tst = "end delete duplicates from curr import..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                    tst = "Folder for: " + dir.Name + " :" + "s"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                    string insertcmds = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Invalid";
                    var pt = txt_RocksmithDLCPath.Text + "\\" + dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat").Replace("_Mac", "") + ".psarc";
                    var pxt = dir.Name.Replace(pth, "").Replace("_Pc", "").Replace("_PS3", ".edat").Replace("_Mac", "") + ".psarc";
                    System.IO.FileInfo ptz = null;
                    try
                    {
                        ptz = new System.IO.FileInfo(pt);
                        //var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";

                        var insertvals = "\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name.Replace("_Pc", ".psarc").Replace("_Mac", ".psarc").Replace("_PS3", ".edat.psarc") + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"" + pxt + "\",\"" + ptz.CreationTime + "\",\"" + GetHash(pt) + "\",\"" + ptz.Length + "\",\"" + DateTime.Now + "\",\"" + ImportPackNo + "\",\"" + invalid + "\"";//,\"" + "0" + "\",\"";
                                                                                                                                                                                                                                                                                                                                                                                             //insertvals += System.DateTime.Today + "\",\""+ ImportPackNo + "\"";
                        InsertIntoDBwValues("Import", insertcmds, insertvals, cnb);
                    }
                    catch (System.IO.FileNotFoundException ee)
                    {
                        Console.WriteLine(ee.Message);
                    }
                }
                tst = "end create import based on already decompressed..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            //START WITH mAINdb UPDATE
            var m = 0;
            //Thread.Sleep(2000);
            var cmd = "SELECT  FullPath, Path, FileName, FileHash, FileSize, ImportDate,i.Pack, i.FileCreationDate, i.ID, i.Invalid FROM Import as i";//DISTINCT
            //    {// 1. If hash already exists do not insert
            if (!chbx_Additional_Manipulations.GetItemChecked(67)) //68. Import duplicates(hash)
                cmd += " LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE i.ID is not NULL";
            cmd += " ORDER BY i.Platform='Pc' ASC ,i.Platform='Mac' ASC,i.Platform='PS3' ASC, i.FileName;";
            cnb.Close(); cnb.Open();
            DataSet dns = new DataSet(); var noOfRec = 0;
            while (noOfRec == 0)
            {
                dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
                noOfRec = dns.Tables[0].Rows.Count;
            }
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //dns = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            var tft = "\n Ignoring ";
            //Thread.Sleep(1500);
            noOfRec = dns.Tables[0].Rows.Count;
            if (chbx_Additional_Manipulations.GetItemChecked(29) && noOfRec > 0 && !chbx_Additional_Manipulations.GetItemChecked(67)) //30. When importing delete identical duplicates(same hash/filesize)
            {
                tft = "";
                for (m = 0; m < noOfRec; m++)
                {
                    var newf = dns.Tables[0].Rows[m].ItemArray[0].ToString().Replace(pathDLC, dupli_Path_Import);
                    tst = newf + "\n" + dns.Tables[0].Rows[m].ItemArray[0].ToString(); timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (chbx_Additional_Manipulations.GetItemChecked(29)) //&& !File.Exists(newf) //As the new file might have a different name e.g. (1) ....etc.
                    {
                        File.Copy(dns.Tables[0].Rows[m].ItemArray[0].ToString(), newf, true);
                        try
                        {
                            DeleteFile(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                            // File.Delete(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                        }
                        catch (Exception ex) { MessageBox.Show("l1326Issues when moving to duplicate folder at import" + "-" + ex.Message + dns.Tables[0].Rows[m].ItemArray[0].ToString()); }
                        tft += "\n Deleting " + dns.Tables[0].Rows[m].ItemArray[0].ToString() + " as imported on " + dns.Tables[0].Rows[m].ItemArray[5].ToString();
                    }
                }
            }

            tst = tft + noOfRec + "/" + (noOfRec + m) + " already imported"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));


            DataSet ds = new DataSet(); ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            noOfRec = ds.Tables[0].Rows.Count;
            tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //if (noOfRec == 0)
            //{
            //    //DataSet ds = new DataSet();
            //    ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //    noOfRec = ds.Tables[0].Rows.Count;
            //    tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //}
            //if (noOfRec == 0)
            //{
            //    //DataSet ds = new DataSet();
            //    ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //    noOfRec = ds.Tables[0].Rows.Count;
            //    tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //}
            //if (noOfRec == 0)
            //{
            //    //DataSet ds = new DataSet();
            //    ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //    noOfRec = ds.Tables[0].Rows.Count;
            //    tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //}
            //if (noOfRec == 0)
            //{
            //    //DataSet ds = new DataSet();
            //    ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //    noOfRec = ds.Tables[0].Rows.Count;
            //    tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //}
            //if (noOfRec == 0)
            //{
            //    //DataSet ds = new DataSet();
            //    ds = SelectFromDB("Import", cmd, txt_DBFolder.Text, cnb);
            //    noOfRec = ds.Tables[0].Rows.Count;
            //    tst = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //}

            if (noOfRec > 0)
            {
                //Move duplicates to the end
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Maximum = 2 * (noOfRec - 1);
                duplit = false;
                dupliNo = 0;
                dupliPrcs = 0;
                if (chbx_Additional_Manipulations.GetItemChecked(41) && netstatus != "OK")
                {
                    netstatus = ActivateSpotify_ClickAsync().Result.ToString();
                    timestamp = UpdateLog(timestamp, "ending estabblishing connection with SPOTIFY.", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }

                for (var j = 0; j < 10000; j++) { dupliSongs[0, j] = "0"; dupliSongs[1, j] = "0"; }
                timestamp = UpdateLog(timestamp, "ending setting dupli array to 0.", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                for (var j = 0; j <= 1; j++)
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        tst = "\n\nSTART importing song..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var songt = timestamp;
                        if (!(j == 1 && dupliSongs[0, i] == "0"))
                        {
                            if (j == 1 && dupliSongs[0, i] == "1") dupliPrcs++;
                            duplit = false;
                            tst = (j == 0 ? "" : "Duplicates: ") + (j == 0 ? (i + 1) : dupliPrcs) + "/" + (j == 0 ? noOfRec : dupliNo);

                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            var bbroken = ds.Tables[0].Rows[i].ItemArray[9].ToString();

                            //to return: dupliSongs[i] = 1; duplit = true; dupliNo++

                            Random randomp = new Random();
                            int packid = randomp.Next(0, 100000);
                            timestamp = UpdateLog(timestamp, tst + " " + FullPath, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            //Add text over progress bar
                            pB_ReadDLCs.CreateGraphics().DrawString(tst + " " + FullPath.Replace(Path.GetDirectoryName(FullPath) + "\\", "")
                                , new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                            errr = false;
                            if (!chbx_Additional_Manipulations.GetItemChecked(37))
                                if (!FullPath.IsValidPSARC())
                                {
                                    //MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                                    {
                                        File.Copy(FullPath.Replace(".psarc", ".invalid"), Pathh, true);
                                        DeleteFile(FullPath.Replace(".psarc", ".invalid"));
                                        //File.Delete(FullPath.Replace(".psarc", ".invalid"));
                                    }
                                    errr = true;
                                    timestamp = UpdateLog(timestamp, "FAILED2 @Import cause of extension issue but copied in the broken folder", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                    UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb);
                                    continue;
                                }

                            var unpackedDir = "";
                            var packagePlatform = FullPath.GetPlatform();
                            if (j == 1)
                                unpackedDir = dupliSongs[1, i];
                            else
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
                                    ErrorWindow frm1 = new ErrorWindow("(1408)Please Install Wwise v" + ConfigRepository.Instance()["general_wwisepath"] + " with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                                    frm1.ShowDialog();
                                    if (frm1.IgnoreSong) break;
                                    if (frm1.StopImport) { j = 10; i = 9999; break; }
                                }
                                try
                                {
                                    // UNPACK
                                    tst = "start unpacking..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (chbx_Additional_Manipulations.GetItemChecked(51))
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, null);
                                    else
                                        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, false, null);
                                    tst = "end unpacking..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (unpackedDir.IndexOf("{") > 0 || unpackedDir.IndexOf("}") > 0)
                                    {
                                        if (!Directory.Exists(unpackedDir.Replace("{", "").Replace("}", ""))) Directory.Move(unpackedDir, unpackedDir.Replace("{", "").Replace("}", ""));
                                        unpackedDir = unpackedDir.Replace("{", "").Replace("}", "");
                                    }
                                    dupliSongs[1, i] = unpackedDir;
                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show("Unpacking ..." + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb);

                                        errr = true; //bcapi???
                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        timestamp = UpdateLog(timestamp, "FAILED2" + ee.Message, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }
                            else
                            {
                                unpackedDir = FullPath.Replace("_p.psarc", "_p_Pc").Replace("_m.psarc", "_m_Mac").Replace(".edat.psarc", "_PS3").Replace(txt_RocksmithDLCPath.Text, txt_TempPath.Text);
                                //FullPath = FullPath.Replace("_Pc", ".psarc").Replace("_Mac", ".psarc").Replace("_PS3", ".edat.psarc");
                            }

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
                                    timestamp = UpdateLog(timestamp, ex.Message + "problem at reorg" + unpackedDir + "---", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                                        UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb);

                                        errr = true;
                                        timestamp = UpdateLog(timestamp, "FAILED2 @org but copied in the broken folder" + ee.Message, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }

                            stopp = false;
                            tst = "start processing..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            string imported = Processing(i, j, tst, FullPath, DB_Path, errr, broken_Path_Import, ds, Temp_Path_Import
                                , dupli_Path_Import, old_Path_Import, cmd, unpackedDir, packid, false, bbroken, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            tst = "end processing song..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            if ((stopp)) break;
                            else
                            if (imported != "0")// && imported != "ignored")
                            {
                                string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate";
                                var insertA = "Select i.FullPath, i.Path, i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, i.ImportDate FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA, cnb);

                                if (!((imported.ToLower()).IndexOf("ignore") >= 0) && NullHandler(imported.Split(';')[0]) != null && NullHandler(imported.Split(';')[0]) != "")// || (imported.ToLower().IndexOf("update") >= 0)
                                {
                                    //Generating the HASH code
                                    var FileHash = ""; var fpath = "";
                                    if (chbx_Additional_Manipulations.GetItemChecked(15) || chbx_Additional_Manipulations.GetItemChecked(75)) fpath = FullPath.Replace(txt_RocksmithDLCPath.Text, old_Path_Import);
                                    else fpath = FullPath;

                                    FileHash = GetHash(fpath);

                                    System.IO.FileInfo fi = null; //calc file size
                                    try { fi = new System.IO.FileInfo(fpath); }
                                    catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

                                    insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
                                    var fnnon = Path.GetFileName(fpath);
                                    var packn = FullPath.Substring(0, fpath.IndexOf(fnnon));
                                    insertA = "Select top 1 i.FullPath, \"" + old_Path_Import + "\", i.FileName, i.FileCreationDate, i.FileHash, i.FileSize, " + NullHandler(imported.Split(';')[0]) + " as DLC_ID, \"" + imported.Split(';')[1] + "\" as DLC_Name, \"" + fpath.GetPlatform().platform.ToString() + "\" as Platform,\"" + Is_Original + "\" as Official FROM Import as i LEFT JOIN Import_AuditTrail AS a ON i.FileHash = a.FileHash WHERE(i.ID = " + ds.Tables[0].Rows[i].ItemArray[8].ToString() + ")"; //((a.ID)Is Null) and 
                                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb);
                                    //fs.Close();
                                    tst = "end _AuditTrailing..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }
                            }
                        }
                        tst = "END importing song..." + (songt - DateTime.Now); timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }
            var endI = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;
            timestamp = UpdateLog(timestamp, "Ended import" + endI, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (chbx_Additional_Manipulations.GetItemChecked(78))
            {
                cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, oggPreviewPath FROM Main WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                //cmd += " AND " + ((SearchCmd.IndexOf("WHERE ") > 0) ? (SearchCmd.Substring(SearchCmd.IndexOf("WHERE ") + 5)) : "1=1");
                FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
                //cmd += " AND " + ((SearchCmd.IndexOf("WHERE ") > 0) ? (SearchCmd.Substring(SearchCmd.IndexOf("WHERE ") + 5)) : "1=1");
                FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


                //Populate(ref databox, ref Main);
                //databox.Refresh();
                //pB_ReadDLCs.CreateGraphics().DrawString("Done Fixing Audio Issues for this song.", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            }

            //Cleanup
            if (chbx_Additional_Manipulations.GetItemChecked(24)) //25. Use translation tables for naming standardization
            {
                tst = "Applying Standardizations";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                Translation_And_Correction((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text));
                tst = "";
                pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                tst = "end Standardization applying..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }

            SetImportNo();

            //Show Intro database window
            MainDB frm = new MainDB((chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), txt_TempPath.Text, chbx_Additional_Manipulations.GetItemChecked(33)
                , txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), cnb);
            if (chbx_Additional_Manipulations.GetItemChecked(77)) frm.Show();
            var endT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            if (chbx_Additional_Manipulations.GetItemChecked(42)) //43. Save import Log
            {
                // Write the string to a file.
                var fn = (log_Path == null || log_Path == "" ? Log_PSPath : log_Path) + "\\" + GetTimestamps(DateTime.Now).Replace(":", "_") + ".txt";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fn, true))
                {
                    file.WriteLine("Full Log");
                    file.WriteLine(rtxt_StatisticsOnReadDLCs.Text);
                    file.Close();
                }

                timestamp = UpdateLog(timestamp, "Log saved", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            SaveSettings();
            string endtmp = (starttmp - DateTime.Now).ToString();
            timestamp = UpdateLog(timestamp, "The End " + endT + " (" + startT + ") after " + endtmp, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
              string Temp_Path_Import, string dupli_Path_Import, string old_Path_Import, string cmd, string unpackedDir, int packid, bool Rebuild, string bbbroken
            , ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)

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
                    timestamp = UpdateLog(timestamp, "Issues at decompressing WEMs or FAILED2 empty path", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                // LOAD DATA

                try
                {
                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                }
                catch (Exception ee)
                {
                    MessageBox.Show("l1658" + ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    Console.WriteLine(ee.Message);
                    var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (chbx_Additional_Manipulations.GetItemChecked(30))
                    {
                        File.Copy(FullPath, Pathh, true);/*Replace(txt_TempPath.Text, txt_RocksmithDLCPath.Text)*/
                        DeleteFile(FullPath);
                        //File.Delete(FullPath);
                    }
                    UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb);

                    timestamp = UpdateLog(timestamp, "FAILED2 @Load but copied in the broken folder" + ee.Message, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    Console.WriteLine(ee.Message);
                    return "0";
                }
                tst = "end Loading song from folder..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                    info.SongInfo.Artist = info.SongInfo.Artist.Replace(";", "'");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Trim();
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\\", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("/", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("?", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(":", "");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "'");
                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(";", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Trim();
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\\", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("/", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("?", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(":", "");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "'");
                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(";", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Trim();
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\\", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("/", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("?", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(":", "");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "'");
                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(";", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\\", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "");
                    info.SongInfo.Album = info.SongInfo.Album.Trim();
                    info.SongInfo.Album = info.SongInfo.Album.Replace("/", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("?", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace(":", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace(";", "");
                    info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "'");

                    //info.AlbumArtPath = info.SongInfo.Album.Replace("/", "");
                    if (ff != info.SongInfo.Artist) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Artist...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (gg != info.SongInfo.ArtistSort) timestamp = UpdateLog(timestamp, "removed potential illegally characters \\,\",/,?,: from ArtistSort...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (hhh != info.SongInfo.SongDisplayName) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Title...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (jj != info.SongInfo.SongDisplayNameSort) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from TitleSort...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (kk != info.SongInfo.Album) timestamp = UpdateLog(timestamp, "removed potentially illegal characters \\,\",/,?,: from Album...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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

                timestamp = UpdateLog(timestamp, "\n Song " + (i + 1) + ": " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                tst = "end text cleanup and standardization"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported" + "----", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        Console.WriteLine(ee.Message);
                        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                        if (chbx_Additional_Manipulations.GetItemChecked(30))
                        {
                            File.Copy(FullPath, Pathh, true);
                            DeleteFile(FullPath);
                            //File.Delete(FullPath);
                        }
                        UpdatePackingLog("LogImportingError", DB_Path, packid, "0", Pathh.Replace("'", "") + tst, cnb);

                        timestamp = UpdateLog(timestamp, "FAILED2 @XML parse but copied in the broken folder" + ee.Message + "----", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                timestamp = UpdateLog(timestamp, "no section/lastconvdate", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                tst = "end xml readout..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                tst = "end Arrangements and Tones readout..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


                var alt = "";
                var trackno = -1;
                var SpotifySongID = "";
                var SpotifyArtistID = "";
                var SpotifyAlbumID = "";
                var SpotifyAlbumURL = "";
                var SpotifyAlbumPath = "";

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
                    if (info.ToolkitInfo.PackageVersion != null) { if (Convert.ToSingle(info.ToolkitInfo.PackageVersion.Replace("_", ".").Replace(".9.", ".9").Replace(".0.", ".0").Replace(".1.", ".1").Replace(".2.", ".2").Replace(".3.", ".3").Replace(".4.", ".4").Replace(".5.", ".5").Replace(".6.", ".6").Replace(".7.", ".7").Replace(".8.", ".8")) < Convert.ToSingle(ver)) info.ToolkitInfo.PackageVersion = ver; }
                    else info.ToolkitInfo.PackageVersion = ver;
                }
                tst = "end song details readout like v ersion author and tk versioning..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                if (Is_MultiTrack == "Yes") timestamp = UpdateLog(timestamp, "Multitrack=-=" + MultiTrack_Version, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                tst = "end multitrackcheckin..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //Detect Live
                var IsLive = "";
                var LiveDetails = "";
                multxt = "Live"; Titl = Check4MultiT(origFN, info.SongInfo.SongDisplayName, multxt);
                if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6)
                { IsLive = "Yes"; gom = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, info.SongInfo.SongDisplayName.IndexOf(multxt) + 4), ""); }

                //Detect Acoustic
                var IsAcoustic = "";
                multxt = "Acoustic"; Titl = Check4MultiT(origFN, info.SongInfo.SongDisplayName, multxt);
                if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6)
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
                    tst = "startgetting cover spotify..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                    //string s = sptyfy.Result.ToString();
                    //string ert = "";
                    //ert=s.Split(';')[0].ToString();
                    trackno = sptyfy.Result.Split(';')[0].ToInt32();
                    SpotifySongID = sptyfy.Result.Split(';')[1];
                    SpotifyArtistID = sptyfy.Result.Split(';')[2];
                    SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                    SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                    SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                    // GetTrackNoSpotifyAsync(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName));
                    tst = "end get track no from spotify..." + SpotifyAlbumPath; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    //string coverf = "";
                    //if (SpotifyAlbumID!="-" && SpotifyAlbumID!="") coverf = StartToGetSpotifyAlbumDetails(SpotifyAlbumURL, SpotifyAlbumID);
                    //if (SpotifyAlbumID != "-" && SpotifyAlbumID != "") { SpotifyAlbmID= SpotifyAlbumID; SpotifyAlbmURL } 
                }
                ExistingTrackNo = "";


                //Generating the HASH code
                art_hash = "";
                string audio_hash = "";
                string audioPreview_hash = "";
                AlbumArtPath = info.AlbumArtPath;
                string ss = "";

                if (AlbumArtPath != "" && AlbumArtPath != null)
                {
                    art_hash = GetHash(AlbumArtPath);
                    //convert to png
                    ExternalApps.Dds2Png(AlbumArtPath);
                }
                else
                {
                    var dds = Directory.GetFiles(unpackedDir + "\\gfxassets\\album_art\\", String.Format("*{0}.dds", Path.GetFileNameWithoutExtension(unpackedDir + "\\gfxassets\\album_art\\")), System.IO.SearchOption.AllDirectories);
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
                tst = "end gen hashcodes..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                SongRecord = GenericFunctions.GetRecord_s(sel, cnb);
                var norows = SongRecord[0].NoRec.ToInt32();

                var selduo = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND ";
                selduo += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                selduo += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                selduo += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\" AND Is_Original=\"Yes\")";
                GenericFunctions.MainDBfields[] SongRecord2 = new GenericFunctions.MainDBfields[10000];
                SongRecord2 = GenericFunctions.GetRecord_s(selduo, cnb);
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
                var Duplic = 0;
                var maxDuplic = 0;
                //Calculate the dupli number
                DataSet dxff = new DataSet(); cmd = "SELECT MAX(Duplicate_Of) FROM Main WHERE Duplicate_Of<>\"\" Group BY Duplicate_Of";
                dxff = SelectFromDB("Main", cmd, txt_DBFolder.Text, cnb);
                //DataSet dxff = new DataSet(); dxff = SelectFromDB("Main", (sel.Replace("ORDER BY Is_Original ASC", " Group BY Duplicate_Of")).Replace("SELECT *", "SELECT Duplicate_Of"), txt_DBFolder.Text, cnb);
                maxDuplic = dxff.Tables.Count == 1 ? (dxff.Tables[0].Rows.Count > 0 ? (dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() > 0 ? dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1) : 1) : 1;
                //var SpotifySong_ID = "";
                //var SpotifyArtist_ID = "";
                //var SpotifyAlbum_ID = "";
                //var SpotifyAlbum_URL = "";
                bool newold = chbx_Additional_Manipulations.GetItemChecked(32);
                Random random = new Random();
                if (norows > 0)
                    foreach (var file in SongRecord)
                    {
                        Duplic = Duplic == 0 && maxDuplic == 1 ? file.Duplicate_Of.ToInt32() : maxDuplic;
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
                            DataSet ddzv = new DataSet(); ddzv = SelectFromDB("Main", (sel.Replace("ORDER BY Is_Original ASC", "")).Replace("SELECT *", "SELECT max(Alternate_Version_No)"), txt_DBFolder.Text, cnb);
                            //UPDATE the 1(s) not an alternate already
                            int max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString() != "" ? ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1;
                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Song_Title = Song_Title +\" a." + max + "\", Song_Title_Sort = Song_Title_Sort+\" a." + max + "\", Is_Alternate = \"Yes\", Alternate_Version_No=" + max + " where ID in (" + sel.Replace("*", "ID") + ") and Is_Alternate=\"No\"" + ";", cnb);

                            //Add also a random DLCName if any of the Alternates has the same DLC Name as the original
                            DataSet dxf = new DataSet(); cmd = "UPDATE Main SET Duplicate_Of = +\"" + Duplic + "\" WHERE ID=" + file.ID + ";"; dxf = UpdateDB("Main", cmd, cnb);
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
                        DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, txt_DBFolder.Text, cnb);

                        var altvert = dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 1 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                        if (Is_Original == "No") altver = (altvert + 1).ToString();
                        //if (altvert == -1)
                        //    altvert = 1;
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
                        var dupli_reason = "";

                        var versio = info.ToolkitInfo.PackageVersion == null ? 1 : float.Parse(info.ToolkitInfo.PackageVersion);
                        if (Rebuild) artist = "Insert"; //At Rebuild ignore duplicates
                        else
                        //DUPLICATION DETECTION LOGIC
                        if (chbx_Additional_Manipulations.GetItemChecked(68))
                            if (ds.Tables[0].Rows[i].ItemArray[3].ToString() == file.File_Hash) { artist = "Ignore"; dupli_reason = "IGNORED as already imported(hash)"; }
                        if (artist != "Ignore")
                            if ((author.ToLower() == file.Author.ToLower() && author != "" && file.Author != "" && file.Author != "Custom Song Creator" && author != "Custom Song Creator"))
                            {
                                if (float.Parse(file.Version) < versio) artist = "Update";
                                else if (float.Parse(file.Version) > versio)
                                    if (file.Is_Alternate != "Yes") { artist = "Ignore"; dupli_reason = "Duplicated IGNORED cause version bigger and NOTalternate"; }
                                    else dupli_reason = "Possible Duplicate Import at end. Same Version but alternate";
                                else if (float.Parse(file.Version) == versio) dupli_reason = "Possible Duplicate Import at end. Same Version";
                                //else dupli_reason += "Duplicated wo version number probblems";
                            }
                            //else if (file.Is_Original == "Yes") artist = "alternate";// dupli_reason = "Duplicated added as alternate to Original already in."; }
                            else if (file.DLC_Name.ToLower() == info.Name.ToLower()) dupli_reason += ". Possible Duplicate Import Same DLC Name";  //if (author.ToLower() != file.Author.ToLower() && (author != "" && author != "Custom Song Creator" && file.Author != "Custom Song Creator" && file.Author != "")) artist = "Alternate";
                                                                                                                                                   //else dupli_reason += ". Possible Duplicate Import at end. End Else";

                        timestamp = UpdateLog(timestamp, dupli_reason, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (chbx_Additional_Manipulations.GetItemChecked(50) && j == 0) { dupliSongs[0, i] = "1"; duplit = true; dupliNo++; break; }
                        else if ((dupli_reason != "" && artist != "Ignore") || chbx_Additional_Manipulations.GetItemChecked(79))
                            artist = AssessConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead,
                                Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash,
                                alist, blist, DB_Path, clist, dlist, newold, Is_Original, altvert.ToString(), fsz, unpackedDir,
                                Is_MultiTrack, MultiTrack_Version, ds.Tables[0].Rows[i].ItemArray[7].ToString(), tst, Platformm, IsLive,
                                LiveDetails, IsAcoustic, HasOrig, dupli_reason);

                        //Exit condition
                        tst = "end check for dupli..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                            DataSet dxf = new DataSet(); dxf = UpdateDB("Main", "UPDATE Main SET Duplicate_Of = \"" + Duplic + "\" WHERE ID =" + file.ID + ";", cnb);

                        }

                        //Doublechecking that no DLC Name is the same (last import 1500 songs generate once such exception :) )
                        DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main WHERE DLC_Name='" + info.Name + "'", txt_DBFolder.Text, cnb);
                        if (dms.Tables[0].Rows.Count > 1) info.Name = random.Next(0, 100000) + info.Name;

                        b++;

                        //jk = file.Version;
                        //k = file.Author;
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
                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb);
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
                        DataSet dzr = new DataSet(); dzr = SelectFromDB("Main", "SELECT Original_FileName, Available_Old FROM Main WHERE ID=" + IDD + ";", txt_DBFolder.Text, cnb);
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
                        DataSet dus = new DataSet(); dus = UpdateDB("Pack_AuditTrail", cmdupd + ";", cnb);


                        //var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                        //var insertA = txt_TempPath.Text + "\\0_old\\" + ", " + txt_TempPath.Text + "\\0_old\\" + ", " + original_FileName + ", " + ds.Tables[0].Rows[i].ItemArray[5] + ", "
                        //    + ds.Tables[0].Rows[i].ItemArray[3] + ", " + ds.Tables[0].Rows[i].ItemArray[4] + ", " + IDD + ", " + DLCC + ", " + Platformm;
                        //InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
                    }
                    tst = "end not dupi measures..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (artist != "Ignore")
                    //    ;// DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + IDD + ")");
                    //else
                    {

                        PreviewTime = "";
                        PreviewLenght = "3000";
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


                        // if (chbx_Additional_Manipulations.GetItemChecked(69))
                        if (File.Exists(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            using (var vorbis = new NVorbis.VorbisReader(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            {
                                bitrate = vorbis.NominalBitrate;
                                SampleRate = vorbis.SampleRate;
                            }

                    }
                    //Define final path for the imported song
                    var norm_path = txt_TempPath.Text + "\\" + platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + trackno.ToString() + "_" + info.SongInfo.SongDisplayName + "_" + random.Next(0, 100000);

                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
                    command = connection.CreateCommand();
                    if (artist == "Update")
                    {
                        //Update MainDB
                        timestamp = UpdateLog(timestamp, "Updating / Overriting " + IDD + "-" + j + "-" + "" + "-" + k + "..", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                        command.CommandText += "Spotify_Album_URL = @param80,";
                        command.CommandText += "Is_Broken = @param81,";
                        command.CommandText += "Audio_OrigHash = @param82,";
                        command.CommandText += "Audio_OrigPreviewHash = @param83,";
                        command.CommandText += "AlbumArt_OrigHash = @param84,";
                        command.CommandText += "Duplicate_Of = @param85";
                        command.CommandText += " WHERE ID = " + IDD;

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
                        command.Parameters.AddWithValue("@param81", bbbroken);
                        command.Parameters.AddWithValue("@param82", audio_hash);
                        command.Parameters.AddWithValue("@param83", audioPreview_hash);
                        command.Parameters.AddWithValue("@param84", art_hash);
                        command.Parameters.AddWithValue("@param85", Duplic.ToString());
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
                            tst = "end updating ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        timestamp = UpdateLog(timestamp, "Inserting ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        command.CommandText += "Spotify_Album_URL,";
                        command.CommandText += "Is_Broken,";
                        command.CommandText += "Audio_OrigHash,";
                        command.CommandText += "Audio_OrigPreviewHash,";
                        command.CommandText += "AlbumArt_OrigHash,";
                        command.CommandText += "Duplicate_Of";
                        command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                        command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                        command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                        command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                        command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48,@param49";
                        command.CommandText += ",@param50,@param51,@param52,@param53,@param54,@param55,@param56,@param57,@param58,@param59";
                        command.CommandText += ",@param60,@param61,@param62,@param63,@param64,@param65,@param66,@param67,@param68,@param69";
                        command.CommandText += ",@param70,@param71,@param72,@param73,@param74,@param75,@param76,@param77,@param78,@param79";
                        command.CommandText += ",@param80,@param81,@param82,@param83,@param84,@param85" + ")";//,@param76


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
                        command.Parameters.AddWithValue("@param81", bbbroken);
                        command.Parameters.AddWithValue("@param82", audio_hash);
                        command.Parameters.AddWithValue("@param83", audioPreview_hash);
                        command.Parameters.AddWithValue("@param84", art_hash);
                        command.Parameters.AddWithValue("@param85", Duplic.ToString());
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
                            timestamp = UpdateLog(timestamp, "error at update " + ex + "\n" + rt, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            throw;
                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                        //If No version found then defaulted to 1
                        //TO DO If default album cover then mark it as suck !?
                        //If no version found must by Rocksmith Original or DLC

                        timestamp = UpdateLog(timestamp, "Records inserted in Main= " + (i + 1), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    if (artist == "Insert" || artist == "Update") //Common set of action for all
                    {
                        //Get last inserted ID
                        //Thread.Sleep(4000);
                        cnb.Close();
                        cnb.Open();
                        var fcmd = "SELECT ID,File_Hash FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"";
                        DataSet dus = new DataSet(); var norec = 0;
                        while (norec == 0)
                        {
                            dus = SelectFromDB("Main", fcmd, txt_DBFolder.Text, cnb);
                            norec = dus.Tables[0].Rows.Count;
                        }
                        //Useful
                        // Get and Store IDENTITY (Primary Key) for further
                        // INSERTS in child table [Order Details]
                        //cmd.CommandText = "SELECT @@identity";
                        //string id = cmd.ExecuteScalar().ToString();
                        //objAdapter.Fill(dus, "Main");
                        //string strID = dus.Tables["Main"].Rows[0].ToString();


                        //UPDATE ArarngementsDB

                        var norecf = dus.Tables[0].Rows[0].ItemArray[0];
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

                                var StartTime = "";
                                StartTime = GetTrackStartTime(arg.SongXml.File, arg.RouteMask.ToString(), arg.ArrangementType.ToString());

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
                                    command.CommandText += "Has_Sections = @param36,";
                                    command.CommandText += "Start_Time = @param37";
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
                                    command.CommandText += "Has_Sections,";
                                    command.CommandText += "Start_Time";
                                    command.CommandText += ") VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                                    command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                                    command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                    command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37";
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
                                command.Parameters.AddWithValue("@param37", (StartTime ?? DBNull.Value.ToString()));
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
                                    timestamp = UpdateLog(timestamp, "error at insert " + command.CommandText + "\n" + arg.Name + " " + arg.RouteMask.ToString(), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    throw;
                                }
                                finally
                                {
                                    if (connection != null) connection.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(CDLC_ID + "Can not open Arrangements DB connection in Import ! " + DB_Path + "-" + arg.SongXml.File + "-" + command.CommandText + ex.Message);
                            }
                        }
                        timestamp = UpdateLog(timestamp, "Arrangements Updated " + info.Arrangements.Count, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                                    command.CommandText += "NameSeparator = @param7 ";
                                    //command.CommandText += "AmpType = @param8, ";
                                    //command.CommandText += "AmpCategory = @param9, ";
                                    //command.CommandText += "AmpKnobValues = @param10, ";
                                    //command.CommandText += "AmpKnobKeys = @param11, ";
                                    //command.CommandText += "AmpPedalKey = @param12, ";
                                    //command.CommandText += "CabinetCategory = @param13, ";
                                    //command.CommandText += "CabinetKnobKeys = @param14, ";
                                    //command.CommandText += "CabinetKnobValues = @param15, ";
                                    //command.CommandText += "CabinetPedalKey = @param16, ";
                                    //command.CommandText += "CabinetType = @param17, ";
                                    //command.CommandText += "PostPedal1 = @param18, ";
                                    //command.CommandText += "PostPedal2 = @param19, ";
                                    //command.CommandText += "PostPedal3 = @param20, ";
                                    //command.CommandText += "PostPedal4 = @param21, ";
                                    //command.CommandText += "PrePedal1 = @param22, ";
                                    //command.CommandText += "PrePedal2 = @param23, ";
                                    //command.CommandText += "PrePedal3 = @param24, ";
                                    //command.CommandText += "PrePedal4 = @param25, ";
                                    //command.CommandText += "Rack1 = @param26, ";
                                    //command.CommandText += "Rack2 = @param27, ";
                                    //command.CommandText += "Rack3 = @param28, ";
                                    //command.CommandText += "Rack4 = @param29";
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
                                    command.CommandText += "NameSeparator ";
                                    //command.CommandText += "AmpType, ";
                                    //command.CommandText += "AmpCategory, ";
                                    //command.CommandText += "AmpKnobKeys, ";
                                    //command.CommandText += "AmpKnobValues, ";
                                    //command.CommandText += "AmpPedalKey, ";
                                    //command.CommandText += "CabinetCategory, ";
                                    //command.CommandText += "CabinetKnobKeys, ";
                                    //command.CommandText += "CabinetKnobValues, ";
                                    //command.CommandText += "CabinetPedalKey, ";
                                    //command.CommandText += "CabinetType, ";
                                    //command.CommandText += "PostPedal1, ";
                                    //command.CommandText += "PostPedal2, ";
                                    //command.CommandText += "PostPedal3, ";
                                    //command.CommandText += "PostPedal4, ";
                                    //command.CommandText += "PrePedal1, ";
                                    //command.CommandText += "PrePedal2, ";
                                    //command.CommandText += "PrePedal3, ";
                                    //command.CommandText += "PrePedal4, ";
                                    //command.CommandText += "Rack1, ";
                                    //command.CommandText += "Rack2, ";
                                    //command.CommandText += "Rack3, ";
                                    //command.CommandText += "Rack4";
                                    command.CommandText += ") VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7";
                                    //+ ", @param8, @param9,@param10";
                                    //command.CommandText += ",@param11,@param12,@param14,@param15,@param16,@param17,@param18,@param19,@param13";
                                    //command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                    command.CommandText += ")";
                                }
                                command.Parameters.AddWithValue("@param1", NullHandler(CDLC_ID));
                                command.Parameters.AddWithValue("@param2", NullHandler(tn.Name));
                                command.Parameters.AddWithValue("@param3", NullHandler(tn.IsCustom));
                                command.Parameters.AddWithValue("@param4", NullHandler(tn.SortOrder));
                                command.Parameters.AddWithValue("@param5", NullHandler(tn.Volume));
                                command.Parameters.AddWithValue("@param6", NullHandler(tn.Key));
                                command.Parameters.AddWithValue("@param7", NullHandler(tn.NameSeparator));

                                var insertcmdd = "CDLC_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex";
                                var insertvalues = ""; insertvalues += CDLC_ID + ", \"Amp\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Type));
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Category));
                                string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"Cabinet\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Type));
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Category));
                                vals = ""; keys = ""; if (tn.GearList.Cabinet != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Cabinet.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PostPedal1\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Type));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Category));
                                vals = ""; keys = ""; if (tn.GearList.PostPedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PostPedal2\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Type));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Category));
                                vals = ""; keys = ""; if (tn.GearList.PostPedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PostPedal3\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Type));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Category));
                                vals = ""; keys = ""; if (tn.GearList.PostPedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PostPedal4\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Type));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Category));
                                vals = ""; keys = ""; if (tn.GearList.PostPedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PrePedal1\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Type));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Category));
                                vals = ""; keys = ""; if (tn.GearList.PrePedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PrePedal2\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Type));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Category));
                                vals = ""; keys = ""; if (tn.GearList.PrePedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PrePedal3\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Type));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Category));
                                vals = ""; keys = ""; if (tn.GearList.PrePedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"PrePedal4\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Type));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Category));
                                vals = ""; keys = ""; if (tn.GearList.PrePedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Skin));
                                insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"Rack1\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Type));
                                insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Category));
                                vals = ""; keys = ""; if (tn.GearList.Rack1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"Rack2\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Type));
                                insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Category));
                                vals = ""; keys = ""; if (tn.GearList.Rack2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"Rack3\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Type));
                                insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Category));
                                vals = ""; keys = ""; if (tn.GearList.Rack3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                insertvalues = ""; insertvalues += CDLC_ID + ", \"Rack4\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Type));
                                insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Category));
                                vals = ""; keys = ""; if (tn.GearList.Rack4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                                insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                                insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                                insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.PedalKey));
                                insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Skin));
                                insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.SkinIndex)) + "\"";
                                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb);

                                //command.Parameters.AddWithValue("@param8", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Type)));
                                //command.Parameters.AddWithValue("@param9", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Category)));
                                //command.Parameters.AddWithValue("@param10", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(vals)));//w issues
                                //command.Parameters.AddWithValue("@param11", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(keys)));//w issues
                                //command.Parameters.AddWithValue("@param12", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.PedalKey)));
                                //command.Parameters.AddWithValue("@param13", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Category)));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.Cabinet.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //var rtt = "";
                                //iz = 0;
                                //try
                                //{ iz = tn.GearList.Cabinet.KnobValues.Count; }
                                //catch  (Exception ee) {
                                //    Console.Write(ee); }
                                ////if (vals != "" || iz > 0)
                                ////    rtt = "";
                                //command.Parameters.AddWithValue("@param14", ((tn.GearList.Cabinet == null) ? DBNull.Value.ToString() : NullHandler(vals)));//w issues
                                //command.Parameters.AddWithValue("@param15", (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(keys)));//w issues
                                //command.Parameters.AddWithValue("@param16", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.PedalKey)));
                                //command.Parameters.AddWithValue("@param17", (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Type)));
                                //command.Parameters.AddWithValue("@param18", NullHandler(tn.GearList.PostPedal1.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PostPedal1.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param19", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param20", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param21", NullHandler(tn.GearList.PostPedal1.PedalKey));
                                //command.Parameters.AddWithValue("@param22", NullHandler(tn.GearList.PostPedal1.Skin));
                                //command.Parameters.AddWithValue("@param23", NullHandler(tn.GearList.PostPedal1.SkinIndex));
                                //command.Parameters.AddWithValue("@param24", NullHandler(tn.GearList.PostPedal1.Type));
                                //command.Parameters.AddWithValue("@param25", NullHandler(tn.GearList.PostPedal2.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PostPedal2.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param26", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param27", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param28", NullHandler(tn.GearList.PostPedal2.PedalKey));
                                //command.Parameters.AddWithValue("@param29", NullHandler(tn.GearList.PostPedal2.Skin));
                                //command.Parameters.AddWithValue("@param30", NullHandler(tn.GearList.PostPedal2.SkinIndex));
                                //command.Parameters.AddWithValue("@param31", NullHandler(tn.GearList.PostPedal2.Type));
                                //command.Parameters.AddWithValue("@param32", NullHandler(tn.GearList.PostPedal3.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PostPedal3.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param33", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param34", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param35", NullHandler(tn.GearList.PostPedal3.PedalKey));
                                //command.Parameters.AddWithValue("@param36", NullHandler(tn.GearList.PostPedal3.Skin));
                                //command.Parameters.AddWithValue("@param37", NullHandler(tn.GearList.PostPedal3.SkinIndex));
                                //command.Parameters.AddWithValue("@param38", NullHandler(tn.GearList.PostPedal3.Type));
                                //command.Parameters.AddWithValue("@param39", NullHandler(tn.GearList.PostPedal4.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PostPedal4.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param40", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param41", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param42", NullHandler(tn.GearList.PostPedal4.PedalKey));
                                //command.Parameters.AddWithValue("@param43", NullHandler(tn.GearList.PostPedal4.Skin));
                                //command.Parameters.AddWithValue("@param44", NullHandler(tn.GearList.PostPedal4.SkinIndex));
                                //command.Parameters.AddWithValue("@param45", NullHandler(tn.GearList.PostPedal4.Type));
                                ////command.Parameters.AddWithValue("@param18", NullHandler(tn.GearList.PostPedal1.));
                                ////command.Parameters.AddWithValue("@param19", NullHandler(tn.GearList.PostPedal2));
                                ////command.Parameters.AddWithValue("@param20", NullHandler(tn.GearList.PostPedal3));
                                ////command.Parameters.AddWithValue("@param21", NullHandler(tn.GearList.PostPedal4));
                                //command.Parameters.AddWithValue("@param46", NullHandler(tn.GearList.PrePedal1.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PrePedal1.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param47", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param48", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param49", NullHandler(tn.GearList.PrePedal1.PedalKey));
                                //command.Parameters.AddWithValue("@param50", NullHandler(tn.GearList.PrePedal1.Skin));
                                //command.Parameters.AddWithValue("@param51", NullHandler(tn.GearList.PrePedal1.SkinIndex));
                                //command.Parameters.AddWithValue("@param52", NullHandler(tn.GearList.PrePedal1.Type));
                                //command.Parameters.AddWithValue("@param53", NullHandler(tn.GearList.PrePedal2.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PrePedal2.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param54", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param55", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param56", NullHandler(tn.GearList.PrePedal2.PedalKey));
                                //command.Parameters.AddWithValue("@param57", NullHandler(tn.GearList.PrePedal2.Skin));
                                //command.Parameters.AddWithValue("@param58", NullHandler(tn.GearList.PrePedal2.SkinIndex));
                                //command.Parameters.AddWithValue("@param59", NullHandler(tn.GearList.PrePedal2.Type));
                                //command.Parameters.AddWithValue("@param60", NullHandler(tn.GearList.PrePedal3.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PrePedal3.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param61", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param62", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param63", NullHandler(tn.GearList.PrePedal3.PedalKey));
                                //command.Parameters.AddWithValue("@param64", NullHandler(tn.GearList.PrePedal3.Skin));
                                //command.Parameters.AddWithValue("@param65", NullHandler(tn.GearList.PrePedal3.SkinIndex));
                                //command.Parameters.AddWithValue("@param66", NullHandler(tn.GearList.PrePedal3.Type));
                                //command.Parameters.AddWithValue("@param67", NullHandler(tn.GearList.PostPedal4.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.PostPedal4.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param68", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param69", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param70", NullHandler(tn.GearList.PostPedal4.PedalKey));
                                //command.Parameters.AddWithValue("@param71", NullHandler(tn.GearList.PostPedal4.Skin));
                                //command.Parameters.AddWithValue("@param73", NullHandler(tn.GearList.PostPedal4.SkinIndex));
                                //command.Parameters.AddWithValue("@param74", NullHandler(tn.GearList.PostPedal4.Type));
                                ////command.Parameters.AddWithValue("@param22", NullHandler(tn.GearList.PrePedal1));
                                ////command.Parameters.AddWithValue("@param23", NullHandler(tn.GearList.PrePedal2));
                                ////command.Parameters.AddWithValue("@param24", NullHandler(tn.GearList.PrePedal3));
                                ////command.Parameters.AddWithValue("@param25", NullHandler(tn.GearList.PrePedal4));
                                //command.Parameters.AddWithValue("@param75", NullHandler(tn.GearList.Rack1.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.Rack1.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param76", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param77", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param78", NullHandler(tn.GearList.Rack1.PedalKey));
                                //command.Parameters.AddWithValue("@param79", NullHandler(tn.GearList.Rack1.Skin));
                                //command.Parameters.AddWithValue("@param80", NullHandler(tn.GearList.Rack1.SkinIndex));
                                //command.Parameters.AddWithValue("@param81", NullHandler(tn.GearList.Rack1.Type));
                                //command.Parameters.AddWithValue("@param82", NullHandler(tn.GearList.Rack2.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.Rack2.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param83", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param84", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param85", NullHandler(tn.GearList.Rack2.PedalKey));
                                //command.Parameters.AddWithValue("@param86", NullHandler(tn.GearList.Rack2.Skin));
                                //command.Parameters.AddWithValue("@param87", NullHandler(tn.GearList.Rack2.SkinIndex));
                                //command.Parameters.AddWithValue("@param88", NullHandler(tn.GearList.Rack2.Type));
                                //command.Parameters.AddWithValue("@param89", NullHandler(tn.GearList.Rack3.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.Rack3.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param90", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param91", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param92", NullHandler(tn.GearList.Rack3.PedalKey));
                                //command.Parameters.AddWithValue("@param93", NullHandler(tn.GearList.Rack3.Skin));
                                //command.Parameters.AddWithValue("@param94", NullHandler(tn.GearList.Rack3.SkinIndex));
                                //command.Parameters.AddWithValue("@param95", NullHandler(tn.GearList.Rack3.Type));
                                //command.Parameters.AddWithValue("@param96", NullHandler(tn.GearList.Rack4.Category));
                                //vals = ""; keys = ""; foreach (KeyValuePair<string, float> glckv in tn.GearList.Rack4.KnobValues) { vals += ";" + glckv.Value; keys += ";" + glckv.Key; }
                                //command.Parameters.AddWithValue("@param97", NullHandler(vals));
                                //command.Parameters.AddWithValue("@param98", NullHandler(keys));
                                //command.Parameters.AddWithValue("@param99", NullHandler(tn.GearList.Rack4.PedalKey));
                                //command.Parameters.AddWithValue("@param100", NullHandler(tn.GearList.Rack4.Skin));
                                //command.Parameters.AddWithValue("@param101", NullHandler(tn.GearList.Rack4.SkinIndex));
                                //command.Parameters.AddWithValue("@param102", NullHandler(tn.GearList.Rack4.Type));

                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    timestamp = UpdateLog(timestamp, "error in arag " + CDLC_ID + " " + tn.Name, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        timestamp = UpdateLog(timestamp, "ToneDB Updated " + info.TonesRS2014.Count, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
                            //Directory.Delete(source_dir, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        catch (Exception ee)
                        {
                            timestamp = UpdateLog(timestamp, "FAILED3 ..", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            Console.WriteLine(ee.Message);
                        }

                        if (chbx_Additional_Manipulations.GetItemChecked(15) || chbx_Additional_Manipulations.GetItemChecked(75)) //16. Move Original Imported files to temp/0_old                               
                        {
                            //Move imported psarc into the old folder
                            try
                            {
                                File.Copy(txt_RocksmithDLCPath.Text + "\\" + original_FileName, old_Path_Import + "\\" + original_FileName, true);
                                if (chbx_Additional_Manipulations.GetItemChecked(15)) DeleteFile(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                                Available_Old = "Yes";
                                timestamp = UpdateLog(timestamp, "File Moved to old" + "...", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                            catch (System.IO.FileNotFoundException ee)
                            {
                                timestamp = UpdateLog(timestamp, "FAILED2" + ee.Message + "----", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmd2 + ";", cnb);
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
                                cmd += " , oggPreviewPath=\"" + audioprevpath + ".ogg\" , PreviewLenght=\"" + PreviewLenght + "\"";
                            }
                        cmd += " , Folder_Name=\"" + norm_path + "\"";

                        cmd += " WHERE ID=" + CDLC_ID;
                        DataSet dxis = new DataSet(); dxis = UpdateDB("Main", cmd + ";", cnb);
                        //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        //{
                        //    OleDbDataAdapter dgs = new OleDbDataAdapter(cmd, cn);
                        //    dgs.Fill(dis, "Main");
                        //    dgs.Dispose();
                        //    timestamp = UpdateLog(timestamp, "Main DB updated after Song Temp DIR REnamed/Stadardised", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //}
                        //fix potentially issues with songs with the audio preview WEM  file the same as the original song(file size{no preview})
                        //Move wem to KIT folder + rename
                        if (info.OggPreviewPath != null)
                            if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1)
                            {
                                try
                                {
                                    File.Move((audiopath + "_preview.wem"), (audiopath + ".wem"));
                                    File.Move((audioprevpath + "_preview.wem"), (audioprevpath + ".wem"));
                                    timestamp = UpdateLog(timestamp, "Issues w the WEM filenames when no preview ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }
                                catch (Exception ee)
                                {
                                    timestamp = UpdateLog(timestamp, "FAILED1" + ee.Message + "----" + info.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    Console.WriteLine(ee.Message);
                                }
                                timestamp = UpdateLog(timestamp, "Fixed preview_preview issue", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }

                        var previewN = FixOggwDiffName(audioprevpath, norm_path, timestamp, "", logPath, Temp_Path_Import, "");
                        //Fixing any _preview_preview issue..End
                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000 
                        if (!chbx_Additional_Manipulations.GetItemChecked(78))
                            if ((chbx_Additional_Manipulations.GetItemChecked(69) && info.OggPath != null)
                                || (((chbx_Additional_Manipulations.GetItemChecked(34) && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                                || (chbx_Additional_Manipulations.GetItemChecked(55) && float.Parse(PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_PreviewStart"]))
                                && info.OggPath != null)))
                            {
                                var d1 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
                                if (d1.Split(';')[0] == "1")
                                {
                                    if (chbx_Additional_Manipulations.GetItemChecked(69) && info.OggPath != null)
                                    {
                                        tst = "start encoding Main audio to 128kb from... " + bitrate + " ... " + SampleRate; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                        {
                                            pB_ReadDLCs.CreateGraphics().DrawString("start encoding Main audio to 128kb from... " + bitrate + " ... " + SampleRate, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                                            cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, oggPreviewPath FROM Main WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                            cmd += " AND ID=" + CDLC_ID + "";
                                            FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        }
                                        tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    }
                                    var tt = chbx_Additional_Manipulations.GetItemChecked(34);
                                    var tt2 = info.OggPreviewPath;
                                      var tt345 = info.OggPreviewPath;
                                      var tt5 = chbx_Additional_Manipulations.GetItemChecked(55);
                                      var t4t = float.Parse(PreviewLenght);
                                     var t7t = float.Parse(ConfigRepository.Instance()["dlcm_PreviewLenght"]);
                                       var tt7 = info.OggPath;


                                    if ((chbx_Additional_Manipulations.GetItemChecked(34) && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                                        || (chbx_Additional_Manipulations.GetItemChecked(55)
                                        && float.Parse(PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_PreviewLenght"]))
                                        && info.OggPath != null)
                                    {
                                        tst = "start set preview"; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE ";
                                        cmd += "ID=" + CDLC_ID + "";
                                        if (chbx_Additional_Manipulations.GetItemChecked(55) && float.Parse(PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_PreviewLenght"]) && info.OggPath != null) DeleteFile(info.OggPreviewPath);
                                        FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    }
                                    tst = "end set preview ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }
                                if (d1.Split(';')[1] == "1") artist = "Ignore";
                                if (d1.Split(';')[2] == "1") { j = 10; i = 9999; }
                            }
                        //Set Preview

                        ////Create Preview //Fix Preview
                        //if (chbx_Additional_Manipulations.GetItemChecked() && info.OggPath != null)
                        //{
                        //    cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
                        //    cmd += " AND ID=" + CDLC_ID + ";";
                        //    FixMissingPreview(cmd, cnb, AppWD);

                        //    tst = "end set preview ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //}
                        //Create Preview //Fix Preview


                        UpdatePackingLog("LogImporting", DB_Path, packid, CDLC_ID, tst, cnb);

                    }
                    //Updating the Standardization table
                    DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\"" + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", txt_DBFolder.Text, cnb);

                    if (dzs.Tables[0].Rows.Count == 0)
                    {
                        var insertcmdd = "Artist, Album, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath";
                        var insertvalues = "\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\",\"" + SpotifyArtistID + "\",\"" + SpotifyAlbumID + "\",\"" + SpotifyAlbumURL + "\",\"" + SpotifyAlbumPath + "\"";
                        InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb);
                    }
                    timestamp = UpdateLog(timestamp, "done", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    pB_ReadDLCs.Increment(1);
                }
            }
            //return "ignored";


            var ttt = CDLC_ID.ToString() == "" ? IDD : CDLC_ID.ToString();
            string ffg = "";
            if (errr == false) ffg = info.Name;
            string rtr = ttt + ";" + ffg + ";" + artist;
            //if (rtr == "" ) //
            //    rtr = "";
            return rtr;

        }


        public string GetAlternateNo()
        {
            var a = "";
            var DB_Path = txt_DBFolder.Text;// + "DLCManager\\Files.accdb";(chbx_DefaultDB.Checked == true ? MyAppWD : )
                                            //Get the higgest Alternate Number
            var sel = "";
            sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + data.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + data.SongInfo.Album + "\") AND ";
            sel += "(LCASE(Song_Title)=LCASE(\"" + data.SongInfo.SongDisplayName + "\") OR ";
            sel += "LCASE(Song_Title) like \"%" + data.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
            sel += "LCASE(Song_Title_Sort) =LCASE(\"" + data.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + data.Name + "\");";
            DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, txt_DBFolder.Text, cnb);
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
            var DB_Path = dbp;//+ "\\Files.accdb";
            int aa = 0;
            pB_ReadDLCs.Value = 0;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\") OR (AlbumArt_Correction <> \"\") order by id;", txt_DBFolder.Text, cnb);
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = norec;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                artist_c = dataRow.ItemArray[2].ToString();
                album_c = dataRow.ItemArray[4].ToString();
                artpath_c = dataRow.ItemArray[5].ToString();

                cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + (album_c != "" ? "\"," : "\"") : "") + (album_c != "" ? " Album = \"" + album_c + (artpath_c != "" ? "\"," : "\"") : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"" : "");
                cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"";
                dus = UpdateDB("Main", cmd1 + ";", cnb);
                pB_ReadDLCs.Increment(1);
            }
            //insert any translation if not already existing

            var insertcmdd = "Artist, Album";
            var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", insertvalues, txt_DBFolder.Text, cnb); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb);

            var cmd3 = "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)";
            DeleteFromDB("Groups", cmd3, cnb); //Cleans out duplicates

            //Apply Artist Short Name
            Standardization.ApplyArtistShort(cnb);

            //Apply Album Short Name
            Standardization.ApplyAlbumShort(cnb);

            //Multiply Spotify
            Standardization.ApplySpotify(cnb);

            //Multiply Cover
            Standardization.ApplyDefaultCover(cnb);

            //Apply DefaultCover
            Standardization.MakeCover(cnb);

            //DeleteFromDB("Standardization", "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)");
            //DeleteFromDB("Standardization", "DELETE* FROM Standardization WHERE id NOT IN (SELECT min(id) FROM Standardization GROUP BY Artist, Artist_Correction, Album, Album_Correction);");
            DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE id NOT IN( SELECT min(ID) FROM(SELECT ID, Artist , IIF(Artist_Correction is NULL or Artist_Correction = '', '0', Artist_Correction) as Artist_Corrections , Album , IIF(Album_Correction is NULL or Album_Correction = '', '0', Album_Correction) as Album_Corrections FROM Standardization) GROUP BY Artist, Artist_Corrections, Album, Album_Corrections); ", cnb);
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + aa + ")");
        }

        internal string AssessConflict(GenericFunctions.MainDBfields filed, DLCPackageData datas, string Fauthor, string tkversion, string DD,
            string Bass, string Guitar, string Combo, string Rhythm, string Lead, string Vocalss, string tunnings, int i, int norows,
            string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist,
            string dbpathh, List<string> clist, List<string> dlist, bool newold, string Is_Original, string altver, string fsz, string unpackedDir,
            string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic, string platform, string IsLive, string LiveDetails,
            string IsAcoustic, string has_Orig, string dupli_reason)
        {
            if ((tkversion == "" && Is_Original == "Yes") || (tkversion == "" && filed.Is_Original == "Yes"))
            {
                UpdateDB("Main", "UPDATE Main SET Has_Other_Officials=\"Yes\" WHERE ID=" + filed.ID + "", cnb);
                has_Orig = "Yes";
                dupli_reason += "Initially assed as Duplicate-Original."; timestamp = UpdateLog(timestamp, dupli_reason, true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (!chbx_Additional_Manipulations.GetItemChecked(79)) return "Ignore";
            }
            else             //"14. Import all as Alternates" 15. Import any Custom as Alternate if an Original exists
            if (art_hash == filed.AlbumArt_Hash && audio_hash == filed.Audio_Hash && audioPreview_hash == filed.AudioPreview_Hash
                    && tkversion == filed.ToolkitVersion && Fauthor == filed.Author
                    && (datas.ToolkitInfo.PackageVersion == filed.Version || (datas.ToolkitInfo.PackageVersion == "" && "1" == filed.Version)) && Is_Original == filed.Is_Original
                    && datas.Name == filed.DLC_Name)//&& platform == filed.Platform
            {
                dupli_reason += "Initially assed as duplicate with all files hash identical."; timestamp = UpdateLog(timestamp, dupli_reason, true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (!chbx_Additional_Manipulations.GetItemChecked(79)) return "Ignore";
            }
            else if (chbx_Additional_Manipulations.GetItemChecked(13) ||/* 13.Import all Duplicates as Alternates*/
                                                                        /*14. Import any Custom as Alternate if an Original exists*/
                    (chbx_Additional_Manipulations.GetItemChecked(14) && (tkversion == "" && filed.Is_Original == "Yes"))
                    /*56.Duplicate manag ignores Multitracks*/
                    || (chbx_Additional_Manipulations.GetItemChecked(56) && ((filed.Is_Multitrack == "Yes" && Is_MultiTrack != "Yes")
                    || (Is_MultiTrack == "Yes" && filed.Is_Multitrack != "Yes") || (Is_MultiTrack == "Yes" && filed.Is_Multitrack == "Yes"
                    && MultiTrack_Version != filed.MultiTrack_Version)))
                    /*80.Duplicate manag.ignores Acoustic Songs */
                    || (chbx_Additional_Manipulations.GetItemChecked(80) && ((filed.Is_Acoustic == "Yes" && IsAcoustic != "Yes")
                    || (IsAcoustic == "Yes" && filed.Is_Acoustic != "Yes") || (IsAcoustic == "Yes" && filed.Is_Acoustic == "Yes" && LiveDetails != filed.Live_Details)))
                    /*66.Duplicate manag.ignores Live Songs */
                    || (chbx_Additional_Manipulations.GetItemChecked(66) && ((filed.Is_Live == "Yes" && IsLive != "Yes")
                    || (IsLive == "Yes" && filed.Is_Live != "Yes") || (IsLive == "Yes" && filed.Is_Live == "Yes" && LiveDetails != filed.Live_Details))))
            {
                dupli_reason += "Initially assed as alternate cause multitrack/live/aCOUSRTIC."; timestamp = UpdateLog(timestamp, dupli_reason, true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (!chbx_Additional_Manipulations.GetItemChecked(79)) return "Alternate";
            }

            frm_Duplicates_Management frm1 = new frm_Duplicates_Management(filed, datas, Fauthor, tkversion, DD, Bass, Guitar, Combo,
                Rhythm, Lead, Vocalss, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist,
                (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text), clist, dlist, newold, Is_Original, altver,
                txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40),
                fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, FileDate, title_duplic, platform, IsLive, LiveDetails, IsAcoustic, cnb, dupli_reason);
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

            timestamp = UpdateLog(timestamp, "REturing from child..", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var tst = "Ignore";
            if (frm1.Asses != "") tst = frm1.Asses;
            frm1.Dispose();
            timestamp = UpdateLog(timestamp, "REturing.. to import", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return tst;
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
            //cnb.Open();
            pB_ReadDLCs.Value = 0;

            BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);

            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;
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
                var Log_PSPath = txt_TempPath.Text + "\\0_log";
                var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
                string pathDLC = txt_RocksmithDLCPath.Text;
                CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, Log_PSPath, AlbumCovers_PSPath);

                btn_RePack.Text = "Stop Repack";
                if ((ConfigRepository.Instance()["general_defaultauthor"] == "" || ConfigRepository.Instance()["general_defaultauthor"] == "Custom Song Creator") && chbx_DebugB.Checked) ConfigRepository.Instance()["general_defaultauthor"] = "catara";

                timestamp = UpdateLog(timestamp, "Packing: ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                Groupss = cbx_Groups.Text.ToString();

                var cmd = "SELECT * FROM Main ";
                if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
                else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")";

                cmd += " ORDER BY Artist";
                //Read from DB
                MainDBfields[] SongRecord = new MainDBfields[10000];
                SongRecord = GetRecord_s(cmd, cnb);
                var norows = SongRecord[0].NoRec.ToInt32();
                pB_ReadDLCs.Maximum = norows * 100;

                //if (!bwRGenerate.IsBusy) bwRGenerate.RunWorkerAsync(data);+";"+ 
                //string ID = args[0];
                //bool bassRemoved = args[1] == "true" ? true : false;
                //string chbx_Format = args[2];
                //string netstatus = args[3];
                //bool chbx_Beta = args[4] == "true" ? true : false;
                //string chbx_Group = args[5];
                //string Groupss = args[6];
                //string TempPath = args[7];
                //bool chbx_UniqueID = args[8] == "true" ? true : false;
                //bool chbx_Last_Packed = args[9] == "true" ? true : false;
                //bool chbx_Last_PackedEnabled = args[10] == "true" ? true : false;
                //bool chbx_CopyOld = args[11] == "true" ? true : false;
                //bool chbx_CopyOldEnabled = args[12] == "true" ? true : false;
                //bool chbx_Copy = args[13] == "true" ? true : false;
                //bool chbx_Replace = args[14] == "true" ? true : false;
                //bool chbx_ReplaceEnabled = args[15] == "true" ? true : false;
                ////string SourcePlatform = args[16];
                ////string TargetPlatform = args[17];
                //string Original_FileName = args[18];
                //string Folder_Name = args[19];
                //string txt_RemotePath = args[20];
                //string txt_FTPPath = args[21];
                //bool chbx_RemoveBassDD = args[22] == "true" ? true : false;
                //bool chbx_BassDD = args[23] == "true" ? true : false;
                //bool chbx_KeepBassDD = args[24] == "true" ? true : false;
                //bool chbx_KeepDD = args[25] == "true" ? true : false;
                //string chbx_Original = args[26];
                //string txt_DLC_ID = args[27];
                //string SearchCmd = args[28];
                //string RocksmithDLCPath = args[29];
                //string DLC_Name = args[30];
                var i = 0;
                var artist = "";
                foreach (var file in SongRecord)
                {
                    if (i == norows || file.Folder_Name == null)
                        break;
                    if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Additional_Manipulations.GetItemChecked(7))) //"8. Don't repack Broken songs")
                    {
                        var args = file.ID + ";" + (file.Has_BassDD == "Yes" ? true : false) + ";";
                        args += (chbx_PC.Checked != false ? "PC" : (chbx_PS3.Checked != false ? "PS3" : (chbx_XBOX360.Checked != false ? "XBOX360" : chbx_Mac.Checked != false ? "Mac" : "Pc"))) + ";" + netstatus + ";";
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul54"] == "Yes" ? true : false) + ";" + cbx_Groups.Text + ";";
                        args += Groupss + ";" + Temp_Path_Import + ";"; //chbx_Beta
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul2"] == "Yes" ? true : false) + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul64"] == "Yes" ? true : false) + ";";
                        args += true + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul65"] == "Yes" ? true : false) + ";";//chbx_UniqueID, chbx_Last_Packed,chbx_Last_Packed.Enabled
                        args += true + ";" + (ConfigRepository.Instance()["dlcm_AdditionalManipul49"] == "Yes" ? true : false) + ";"; //initially imported /Enabled
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul63"] == "Yes" ? true : false) + ";" + true + ";"; //CopyFTPFile /replace /REPACE.ENABLED
                        args += SourcePlatform + ";" + TargetPlatform + ";";
                        args += file.Original_FileName + ";" + file.Folder_Name + ";"; ;// DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString() + ";" + DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString() + ";";
                        args += ConfigRepository.Instance()["dlcm_AdditionalManipul49"] + ";" + ConfigRepository.Instance()["dlcm_FTP1"] + ";";
                        args += (ConfigRepository.Instance()["dlcm_AdditionalManipul5"] == "Yes" ? true : false) + ";" + file.Has_BassDD + ";" + file.Keep_BassDD + ";";//Remove Bass , bass dd,chbx_KeepBassDD
                        args += (file.Keep_DD == "Yes" ? true : false) + ";" + file.Is_Original + ";" + file.DLC_Name + ";";//chbx_KeepDD.Checked, chbx_Original.Tex, dlc_id
                        args += cmd + (cmd.IndexOf(";") > 0 ? "" : ";") + txt_RocksmithDLCPath.Text + ";" + file.DLC_Name + ";"; //SearchCmd + ";" + RocksmithDLCPath, DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value
                        args += ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + ";" + i;
                        do
                            Application.DoEvents();
                        while (bwRGenerate.IsBusy);//keep singlethread as toolkit not multithread abled

                        bwRGenerate.RunWorkerAsync(args);
                        i++;
                    }
                }
            }
        }

        //public void GeneratePackage(object sender, DoWorkEventArgs e)
        //{
        //    var startT = DateTime.Now;
        //    var tsst = "Start TH ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //    var countpacked = 0;
        //    var counttransf = 0;
        //    var cmd = "SELECT * FROM Main ";
        //    if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
        //    else if (rbtn_Population_Groups.Checked) cmd += "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + Groupss + "\")"; ;

        //    string copyftp = "";
        //    cmd += " ORDER BY Artist";
        //    //Read from DB
        //    GenericFunctions.MainDBfields[] SongRecord = new GenericFunctions.MainDBfields[10000];
        //    SongRecord = GetRecord_s(cmd);
        //    var norows = SongRecord[0].NoRec.ToInt32();

        //    Random randomp = new Random();
        //    var packid = randomp.Next(0, 100000);

        //    var i = 0;
        //    var artist = "";
        //    foreach (var file in SongRecord)
        //    {
        //        if (i == norows || file.Folder_Name == null)
        //            break;
        //        if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Additional_Manipulations.GetItemChecked(7))) //"8. Don't repack Broken songs")
        //        {
        //            tsst = i + "Songg " + file.Artist + "-" + file.Album + "-" + file.Song_Title + "..."; timestamp = UpdateLog(timestamp, tsst, false);
        //            var packagePlatform = file.Folder_Name.GetPlatform();
        //            // REORGANIZE
        //            var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
        //            // LOAD DATA

        //            info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);

        //            var bassRemoved = "No";
        //            var DDAdded = "No";
        //            if (chbx_Additional_Manipulations.GetItemChecked(63))//&& file.Remote_Path.IndexOf("psarc") > 0//64.@Pack Remove Remote File if GameData has been read
        //            {
        //                if (chbx_PS3.Checked)
        //                {
        //                    var FTPPath = "";
        //                    if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
        //                    else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
        //                    //MainDB.DeleteFTPFiles(file.Remote_Path, FTPPath);
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        var tg = (txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path).Replace(".psarc", ".dupli");
        //                        if (File.Exists(txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path))
        //                            File.Move(txt_RocksmithDLCPath.Text + "\\" + file.Remote_Path, tg);
        //                    }
        //                    catch (Exception ex) { Console.Write(ex); }
        //                }
        //            }
        //            tsst = "end load ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //            var h = "";
        //            // Check and Get last Packed
        //            if (chbx_Additional_Manipulations.GetItemChecked(64))
        //            {
        //                DataSet dbr = new DataSet(); dbr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\" and DLC_ID=" + file.ID + " ORDER BY ID DESC;", txt_DBFolder.Text, cnb);
        //                var rec = dbr.Tables[0].Rows.Count;
        //                if (rec > 0) h = dbr.Tables[0].Rows[0].ItemArray[0].ToString();
        //            }
        //            else
        //   if (chbx_Additional_Manipulations.GetItemChecked(65) && file.Available_Old == "Yes")
        //            {

        //                var oldfilePath = txt_TempPath.Text + "\\0_old\\" + file.Original_FileName;
        //                if (oldfilePath.GetPlatform().platform.ToString() == (chbx_PC.Checked ? "Pc" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "Ps3" : "XBOX360"))
        //                {
        //                    h = oldfilePath;
        //                }
        //                else
        //                {
        //                    var SourcePlatform = new Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
        //                    var TargetPlatform = new Platform((chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360"), GameVersion.RS2014.ToString());

        //                    var needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
        //                    var tmpDir = Path.GetTempPath();

        //                    var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, false, false, SourcePlatform);

        //                    // DESTINATION
        //                    var nameTemplate = (!TargetPlatform.IsConsole) ? "{0}{1}.psarc" : "{0}{1}";
        //                    Random random = new Random();
        //                    int packrid = random.Next(0, 100000);
        //                    var packageName = Path.GetFileNameWithoutExtension(oldfilePath).StripPlatformEndName();
        //                    if (chbx_Additional_Manipulations.GetItemChecked(2)) packageName += packrid;
        //                    packageName = packageName.Replace(".", "_");
        //                    var targetFileName = String.Format(nameTemplate, Path.Combine(Path.GetDirectoryName(oldfilePath), packageName), TargetPlatform.GetPathName()[2]);

        //                    // CONVERSION
        //                    tsst = "start gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                    if (needRebuildPackage)
        //                    {
        //                        data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
        //                        // Update AppID
        //                        if (!TargetPlatform.IsConsole)
        //                            data.AppId = "248750";
        //                        // Build
        //                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(targetFileName, data, new Platform(TargetPlatform.platform, GameVersion.RS2014));
        //                        tsst = "end generate ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                    }
        //                    else
        //                    {
        //                        // Old and new paths
        //                        var sourceDir0 = SourcePlatform.GetPathName()[0].ToLower();
        //                        var sourceDir1 = SourcePlatform.GetPathName()[1].ToLower();
        //                        var targetDir0 = TargetPlatform.GetPathName()[0].ToLower();
        //                        var targetDir1 = TargetPlatform.GetPathName()[1].ToLower();

        //                        if (!TargetPlatform.IsConsole)
        //                        {
        //                            // Replace AppId
        //                            var appIdFile = Path.Combine(unpackedDir, "appid.appid");
        //                            File.WriteAllText(appIdFile, "248750");
        //                        }

        //                        // Replace aggregate graph values
        //                        var aggregateFile = Directory.EnumerateFiles(unpackedDir, "*.nt", System.IO.SearchOption.AllDirectories).FirstOrDefault();
        //                        var aggregateGraphText = File.ReadAllText(aggregateFile);
        //                        // Tags
        //                        aggregateGraphText = Regex.Replace(aggregateGraphText, GraphItem.GetPlatformTagDescription(SourcePlatform.platform), GraphItem.GetPlatformTagDescription(TargetPlatform.platform), RegexOptions.Multiline);
        //                        // Paths
        //                        aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir0, targetDir0, RegexOptions.Multiline);
        //                        aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir1, targetDir1, RegexOptions.Multiline);
        //                        File.WriteAllText(aggregateFile, aggregateGraphText);

        //                        // Rename directories
        //                        foreach (var dir in Directory.GetDirectories(unpackedDir, "*.*", System.IO.SearchOption.AllDirectories))
        //                        {
        //                            if (dir.EndsWith(sourceDir0))
        //                            {
        //                                var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir0)) + targetDir0;
        //                                //DirectoryExtension.SafeDelete(newDir);
        //                                DeleteDirectory(newDir);
        //                                DirectoryExtension.Move(dir, newDir);
        //                            }
        //                            else if (dir.EndsWith(sourceDir1))
        //                            {
        //                                var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
        //                                //DirectoryExtension.SafeDelete(newDir);
        //                                DeleteDirectory(newDir);
        //                                DirectoryExtension.Move(dir, newDir);
        //                            }
        //                        }

        //                        // Recreates SNG because SNG have different keys in PC and Mac
        //                        bool updateSNG = ((SourcePlatform.platform == GamePlatform.Pc && TargetPlatform.platform == GamePlatform.Mac) || (SourcePlatform.platform == GamePlatform.Mac && TargetPlatform.platform == GamePlatform.Pc));

        //                        // Packing
        //                        var dirToPack = unpackedDir;
        //                        if (SourcePlatform.platform == GamePlatform.XBox360)
        //                            dirToPack = Directory.GetDirectories(Path.Combine(unpackedDir, Packer.ROOT_XBox360))[0];

        //                        Packer.Pack(dirToPack, targetFileName, updateSNG, TargetPlatform);
        //                        //DirectoryExtension.SafeDelete(unpackedDir);
        //                        DeleteDirectory(unpackedDir);
        //                    }

        //                    var s = txt_TempPath.Text + "\\0_old\\";
        //                    h = txt_TempPath.Text + "\\0_repacked\\" + (chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "XBOX360") + "\\";
        //                    h += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));
        //                    s += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_PC.Checked ? ".psarc" : (chbx_Mac.Checked ? ".psarc" : (chbx_PS3.Checked ? ".psarc.edat" : "")));

        //                    if (File.Exists(h)) { DeleteFile(h); File.Move(s, h); }
        //                    else File.Copy(s, h, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        //                    //Generating the HASH code
        //                    var FileHash = "";
        //                    FileStream fs;
        //                    FileHash = GetHash(h);
        //                    using (fs = File.OpenRead(h))
        //                    {
        //                        //SHA1 sha = new SHA1Managed();
        //                        // BitConverter.ToString(sha.ComputeHash(fs));

        //                        System.IO.FileInfo fi = null; //calc file size
        //                        try { fi = new System.IO.FileInfo(h); }
        //                        catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("(3915)DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

        //                        var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
        //                        var fnnon = Path.GetFileName(h);
        //                        var packn = h.Substring(0, h.IndexOf(fnnon));
        //                        var insertA = "\"" + h + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + h.GetPlatform().platform.ToString() + "\",\"" + Is_Original + "\"";
        //                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
        //                        fs.Close();
        //                    }
        //                    tsst = "end gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }
        //            }
        //            else
        //            {
        //                tsst = "Song " + file.Artist + "-" + file.Album + "-" + file.Song_Title + "--" + file.ID + "..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
        //                var platform = file.Folder_Name.GetPlatform();
        //                if (chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
        //                {

        //                    foreach (var xml in xmlFiles)
        //                    {
        //                        Song2014 xmlContent = null;
        //                        try
        //                        {

        //                            if (chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
        //                            {
        //                                //ADD DD
        //                                if (
        //                                        (false && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
        //                                        && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm") && file.Has_DD == "No") || (xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "No")
        //                                        )
        //                                        ||
        //                                        (false && (xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm")
        //                                        && file.Has_DD == "No" && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
        //                                        )
        //                                       )
        //                                {
        //                                    xmlContent = Song2014.LoadFromFile(xml);
        //                                    tsst = "start add DD ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                                    File.Copy(xml, xml + ".woDD", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                                    DDAdded = (AddDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31), "5") == "Yes") ? "No" : "Yes";
        //                                    file.Has_BassDD = (DDAdded == "Yes") ? "Yes" : "No";
        //                                }
        //                            }
        //                            //REMOVE DD
        //                            if (file.Has_BassDD == "Yes")
        //                            {
        //                                xmlContent = Song2014.LoadFromFile(xml);
        //                                if ((!(chbx_Additional_Manipulations.GetItemChecked(52) && file.Keep_BassDD == "Yes") && xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(5))
        //                                        || (!(chbx_Additional_Manipulations.GetItemChecked(53) && file.Keep_DD == "Yes") && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm"))
        //                                          && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(3))
        //                                       )
        //                                {
        //                                    if (chbx_Additional_Manipulations.GetItemChecked(5) && !chbx_Additional_Manipulations.GetItemChecked(3) && !(xmlContent.Arrangement.ToLower() == "bass")) continue;
        //                                    bassRemoved = (RemoveDD(file.Folder_Name, file.Is_Original, xml, platform, chbx_Additional_Manipulations.GetItemChecked(36), chbx_Additional_Manipulations.GetItemChecked(31)) == "Yes") ? "Yes" : "No";
        //                                    file.Has_BassDD = (bassRemoved == "Yes") ? "No" : "Yes";
        //                                    tsst = "end remove DD ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                                }
        //                            }
        //                        }
        //                        catch (Exception ee)
        //                        {
        //                            Console.Write(ee.Message);
        //                        }

        //                    }
        //                }

        //                //Default APP ID
        //                if (chbx_Additional_Manipulations.GetItemChecked(43)) file.DLC_AppID = ConfigRepository.Instance()["general_defaultappid_RS2014"];


        //                //get track no
        //                if (ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") //59. @Pack try to get Track No again (&don't save)                        
        //                {
        //                    string z = (MainDB.GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle(info.SongInfo.SongDisplayName))).ToString();
        //                    file.Track_No = z == "0" && file.Track_No != "" ? file.Track_No : z;
        //                    // Multithreading and DB access nnot supported
        //                    if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes" && file.Track_No != "0" && file.Track_No != "-1" && file.Track_No != "") //60.@Pack try to get Track No, again(&save)
        //                    {
        //                        UpdateDB("Main", "UPDATE Main SET Track_No=\"" + file.Track_No + "\" WHERE ID=" + file.ID + ";");
        //                    }
        //                }

        //                //Gather song Lenght
        //                var duration = "";
        //                var bitrate = 250001;
        //                var bitratep = 250001;
        //                var SampleRate = 45001;
        //                var recalc_Preview = false;
        //                if (file.oggPreviewPath != null && file.oggPreviewPath != "")
        //                    if (chbx_Additional_Manipulations.GetItemChecked(55))
        //                    {
        //                        using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
        //                        { duration = vorbis.TotalTime.ToString(); bitratep = vorbis.NominalBitrate; }

        //                        string[] timepieces = duration.Split(':');
        //                        if (timepieces[0] != "00" || timepieces[1] != "00")
        //                            recalc_Preview = true;

        //                        //check Audio bitrate as originals are always at 128..
        //                        if (chbx_Additional_Manipulations.GetItemChecked(69))
        //                            using (var vorbis = new NVorbis.VorbisReader(file.OggPath))
        //                            {
        //                                bitrate = vorbis.NominalBitrate;
        //                                SampleRate = vorbis.SampleRate;
        //                            }
        //                    }
        //                //Conver Audio to lower bitrate
        //                //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
        //                if (chbx_Additional_Manipulations.GetItemChecked(69) && file.AudioPath != null && (bitrate > ConfigRepository.Instance()["dlcm_MaxBitrate"].ToInt32()))
        //                {
        //                    tsst = "START set main audio reconv ..." + bitrate; timestamp = UpdateLog(timestamp, tsst, false);
        //                    var d3 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
        //                    if (d3.Split(';')[0] == "1")
        //                    {
        //                        Downstream(file.AudioPath);
        //                        using (var vorbis = new NVorbis.VorbisReader(file.OggPath))
        //                        {
        //                            bitrate = vorbis.NominalBitrate;
        //                            SampleRate = vorbis.SampleRate;
        //                        }

        //                        //save new new hash
        //                        cmd = "UPDATE Main SET ";
        //                        var audio_hash = "";
        //                        audio_hash = GetHash(file.AudioPath);
        //                        cmd += "Audio_Hash=\"" + audio_hash + "\", audioBitrate=\"" + bitrate + "\"";
        //                        cmd += ",audioSampleRate=\"" + SampleRate + "\"";
        //                        cmd += " WHERE ID=" + file.ID;
        //                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
        //                    }

        //                    if (d3.Split(';')[1] == "1") break;
        //                    if (d3.Split(';')[2] == "1")
        //                    {
        //                        btn_RePack.Text = "RePack";
        //                        if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();
        //                    }
        //                    tsst = "end set main audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }

        //                if (chbx_Additional_Manipulations.GetItemChecked(70))
        //                {
        //                    GenericFunctions.Converters(file.oggPreviewPath, GenericFunctions.ConverterTypes.Ogg2Wem, false);
        //                    //File.Delete(file.oggPreviewPath.Replace(".ogg", ".wav"));
        //                    //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.wav"));
        //                    //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
        //                    DeleteFile(file.oggPreviewPath.Replace(".ogg", ".wav"));
        //                    DeleteFile(file.oggPreviewPath.Replace(".ogg", "_preview.wav"));
        //                    DeleteFile(file.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
        //                    tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }
        //                if (chbx_Additional_Manipulations.GetItemChecked(71))
        //                {
        //                    //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
        //                    //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
        //                    tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }

        //                //Set Preview
        //                if ((chbx_Additional_Manipulations.GetItemChecked(9) && file.oggPreviewPath == null ||
        //                    (chbx_Additional_Manipulations.GetItemChecked(55) && (file.AudioPreview_Hash == file.Audio_Hash
        //                    || file.Song_Lenght == file.PreviewLenght || recalc_Preview))))
        //                {
        //                    //delete old previews!
        //                    if (file.oggPreviewPath != null) DeleteFile(file.oggPreviewPath);
        //                    if (file.audioPreviewPath != null) DeleteFile(file.audioPreviewPath);

        //                    var startInfo = new ProcessStartInfo();
        //                    startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
        //                    startInfo.WorkingDirectory = AppWD;
        //                    var t = file.OggPath;
        //                    var tt = t.Replace(".ogg", "_preview.ogg");
        //                    var times = ConfigRepository.Instance()["dlcm_PreviewStart"];
        //                    string[] timepieces = times.Split(':');
        //                    TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
        //                    startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
        //                                                        t,
        //                                                        tt,
        //                                                        r.TotalMilliseconds,
        //                                                        (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
        //                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

        //                    //save new previews
        //                    cmd = "UPDATE Main SET ";
        //                    if (File.Exists(file.oggPreviewPath))
        //                        if (PreviewLenght == "" || PreviewLenght == null)
        //                            using (var vorbis = new NVorbis.VorbisReader(file.oggPreviewPath))
        //                            {
        //                                var durations = vorbis.TotalTime;
        //                                bitratep = vorbis.NominalBitrate;
        //                                PreviewLenght = durations.ToString();
        //                            }
        //                    var audioPreview_hash = "";
        //                    audioPreview_hash = GetHash(file.audioPreviewPath);

        //                    cmd += " audioPreviewPath=\"" + file.audioPreviewPath + "\"" + " , audioPreview_Hash=\"" + audioPreview_hash + "\"" + " , PreviewTime=\"" + times + "\", audioBitrate =\"" + bitratep + "\"";
        //                    cmd += " , oggPreviewPath=\"" + file.oggPreviewPath + "\" , PreviewLenght=\"" + (PreviewLenght.IndexOf(":") > 0 ? (PreviewLenght.Split(':'))[2] : PreviewLenght) + "\"";// previewN + "\"";

        //                    cmd += " WHERE ID=" + file.ID;
        //                    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
        //                    tsst = "end set preview ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }

        //                //compress Preview
        //                if (chbx_Additional_Manipulations.GetItemChecked(69) && file.audioPreviewPath != null && (bitratep > ConfigRepository.Instance()["dlcm_MaxBitrate"].ToInt32()))
        //                {
        //                    tsst = "start set preview audio reconv ..." + bitratep; timestamp = UpdateLog(timestamp, tsst, false);
        //                    var d4 = WwiseInstalled("Convert Preview Audio if bitrate> ConfigRepository");
        //                    if (d4.Split(';')[0] == "1")
        //                    {
        //                        Downstream(file.audioPreviewPath);
        //                        //save new new hash
        //                        cmd = "UPDATE Main SET ";
        //                        var audio_previewhash = "";
        //                        audio_previewhash = GetHash(file.audioPreviewPath);
        //                        cmd += "audioPreview_Hash=\"" + audio_previewhash + "\"";
        //                        cmd += " WHERE ID=" + file.ID;
        //                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";");
        //                    }

        //                    if (d4.Split(';')[1] == "1") break;
        //                    if (d4.Split(';')[2] == "1")
        //                    {
        //                        btn_RePack.Text = "RePack";
        //                        if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();
        //                    }
        //                    tsst = "end set preview audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }

        //                if (chbx_Additional_Manipulations.GetItemChecked(17)) //18.Repack with Artist/ Title same as Artist / Title Sort
        //                {
        //                    file.Artist_Sort = file.Artist;
        //                    file.Song_Title_Sort = file.Song_Title;
        //                }

        //                if (chbx_Additional_Manipulations.GetItemChecked(10))
        //                {
        //                    Random random = new Random();
        //                    string apppid = random.Next(0, 100000) + file.DLC_Name;
        //                    file.DLC_Name = apppid;
        //                }

        //                if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) //24.Pack with The/ Die only at the end of Title Sort 
        //                {
        //                    if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4)
        //                        file.Song_Title_Sort = MoveTheAtEnd(file.Song_Title_Sort);
        //                    file.Artist_Sort = MoveTheAtEnd(file.Artist_Sort);
        //                }

        //                var toolkitv = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
        //                if (chbx_Additional_Manipulations.GetItemChecked(47)) toolkitv.PackageVersion = ToolkitVersion.RSTKGuiVersion.ToString();
        //                else toolkitv.PackageVersion = file.Version;
        //                data = new DLCPackageData
        //                {
        //                    GameVersion = GameVersion.RS2014,
        //                    Pc = false,
        //                    Mac = false,
        //                    XBox360 = false,
        //                    PS3 = false,
        //                    Name = file.DLC_Name,
        //                    AppId = file.DLC_AppID,
        //                    ArtFiles = info.ArtFiles,
        //                    //Showlights = true,//info.Showlights, //apparently this infor is not read..also the tone base is removed/not read also
        //                    Inlay = info.Inlay,
        //                    LyricArtPath = info.LyricArtPath,

        //                    //USEFUL CMDs String.IsNullOrEmpty(
        //                    SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
        //                    {
        //                        SongDisplayName = file.Song_Title,
        //                        SongDisplayNameSort = file.Song_Title_Sort,
        //                        Album = file.Album,
        //                        SongYear = file.Album_Year.ToInt32(),
        //                        Artist = file.Artist,
        //                        ArtistSort = file.Artist_Sort,
        //                        AverageTempo = file.AverageTempo.ToInt32()
        //                    },

        //                    AlbumArtPath = file.AlbumArtPath,
        //                    OggPath = file.AudioPath,
        //                    OggPreviewPath = ((file.audioPreviewPath != "") ? file.audioPreviewPath : file.AudioPath),
        //                    Arrangements = info.Arrangements, //Not yet done
        //                    Tones = info.Tones,//Not yet done
        //                    TonesRS2014 = info.TonesRS2014,//Not yet done
        //                    Volume = Convert.ToSingle(file.Volume),
        //                    PreviewVolume = Convert.ToSingle(file.Preview_Volume),
        //                    SignatureType = info.SignatureType,
        //                };
        //                var rrt = ConfigRepository.Instance()["general_defaultauthor"];
        //                if ((file.Author == "Custom Song Creator" || file.Author == "") && rrt != "Custom Song Creator" && chbx_Additional_Manipulations.GetItemChecked(47))
        //                    file.Author = "RepackedBy" + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
        //                if (chbx_Additional_Manipulations.GetItemChecked(54))
        //                    file.Is_Beta = "Yes";
        //                var tkInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
        //                //TO FIX
        //                // data.ToolkitInfo.PackageAuthor = file.Author.ToString();
        //                //data.ToolkitInfo.PackageVersion = file.Version.ToString();
        //                tsst = "end load vars..."; timestamp = UpdateLog(timestamp, tsst, false);

        //                var norm_path = txt_TempPath.Text + "\\0_repacked\\" + ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
        //                //manipulating the info
        //                var st = "";
        //                var sa = "";
        //                if (cbx_Activ_Title.Checked)
        //                    data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");

        //                if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4) { st = file.Song_Title; file.Song_Title = MoveTheAtEnd(file.Song_Title); }
        //                if (cbx_Activ_Title.Checked)
        //                    data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
        //                if (chbx_Additional_Manipulations.GetItemChecked(21) && file.Song_Title_Sort.Length > 4) file.Song_Title = st;
        //                if (file.Is_Beta == "Yes") if (Groupss != "") data.SongInfo.SongDisplayNameSort = "0" + Groupss + data.SongInfo.SongDisplayNameSort.Substring(1, data.SongInfo.SongDisplayNameSort.Length - 2); //).Replace("][", "-").Replace("]0", "")

        //                if (cbx_Activ_Artist.Checked)
        //                    data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");

        //                if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) { sa = file.Artist; file.Artist = MoveTheAtEnd(file.Artist); }
        //                if (cbx_Activ_Artist_Sort.Checked)
        //                    data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
        //                if (chbx_Additional_Manipulations.GetItemChecked(23) && file.Artist_Sort.Length > 4) file.Artist = sa;

        //                if (cbx_Activ_Album.Checked)
        //                    data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i, false, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "[", "]");
        //                if (chbx_Additional_Manipulations.GetItemChecked(0)) //"1. Add Increment to all Titles"
        //                    data.Name = i + data.Name;

        //                artist = "";
        //                if (chbx_Additional_Manipulations.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
        //                {
        //                    if (i > 0)
        //                        if (data.SongInfo.Artist == Files[i - 1].Artist) no_ord += 1;
        //                        else no_ord = 1;
        //                    else no_ord += 1;
        //                    artist = no_ord + " ";
        //                    data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;
        //                }

        //                if (chbx_Additional_Manipulations.GetItemChecked(2))
        //                    //"3. Make all DLC IDs unique (&save)"
        //                    if (file.UniqueDLCName != null && file.UniqueDLCName != "") data.Name = file.UniqueDLCName;
        //                    else
        //                    {
        //                        Random random = new Random();
        //                        data.Name = random.Next(0, 100000) + data.Name;
        //                    }
        //                tsst = "end Advanced setting params ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                //Fix the _preview_preview issue
        //                var ms = data.OggPath;
        //                var tst = "";
        //                try
        //                {
        //                    var sourceAudioFiles = Directory.GetFiles(file.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);

        //                    foreach (var fil in sourceAudioFiles)
        //                    {
        //                        tst = fil;
        //                        if (fil.LastIndexOf("_preview_preview.wem") > 0)
        //                        {
        //                            ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
        //                            File.Move((ms + "_preview.wem"), (ms + ".wem"));
        //                            File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
        //                        }
        //                    }
        //                }
        //                catch (Exception ee) { Console.WriteLine(ee.Message); }
        //                if (data == null)
        //                {
        //                    MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    return;
        //                }

        //                //Add comments to beginning of the lyrics
        //                //var der=chbx_Additional_Manipulations.GetItemChecked(72);
        //                //var ft=file.Has_Vocals;
        //var ttt2 = (chbx_Additional_Manipulations.GetItemChecked(72) && file.Has_Vocals == "Yes") ? AddStuffToLyrics(file.ID, file.Comments, rbtn_Population_Groups.Checked ? Groupss : "", file.Has_DD, (bassRemoved == "Yes") ? "No" : "Yes", file.Has_BassDD, file.Author, file.Is_Acoustic, file.Is_Live, file.Live_Details, file.Is_Multitrack, file.Is_Original) : "";
        //var ttt1 = chbx_Additional_Manipulations.GetItemChecked(73) && file.Has_Vocals == "Yes" ? AddTrackStart2Lyrics(file.ID) : "";
        //                var FN = txt_File_Name.Text;

        //                if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(FN, i, true, chbx_Additional_Manipulations.GetItemChecked(25), false, SongRecord, "", "");
        //                if (file.Is_Beta == "Yes") if (Groupss != "") FN = "0" + Groupss + FN.Substring(1, FN.Length - 2);//.Replace("][", "-").Replace("]0", "");

        //                if (file.Is_Alternate == "Yes" && file.Author != "Custom Song Creator" && file.Author == "" && rrt != "Custom Song Creator" && !chbx_Additional_Manipulations.GetItemChecked(47))
        //                    FN += "a." + file.Alternate_Version_No + file.Author;


        //                if (chbx_Additional_Manipulations.GetItemChecked(8) || chbx_PS3.Checked)
        //                {
        //                    FN = FN.Replace(".", "_");
        //                    FN = FN.Replace(" ", "_");
        //                }

        //                dlcSavePath = txt_TempPath.Text + "\\0_repacked\\" + (chbx_XBOX360.Checked ? "XBOX360" : chbx_PC.Checked ? "PC" : chbx_Mac.Checked ? "MAC" : chbx_PS3.Checked ? "PS3" : "") + "\\" + FN;

        //                int progress = (i + 1) * 100;
        //                errorsFound = new StringBuilder();
        //                var numPlatforms = 0;
        //                numPlatforms++;
        //                if (chbx_Mac.Checked)
        //                    numPlatforms++;
        //                if (chbx_XBOX360.Checked)
        //                    numPlatforms++;
        //                if (chbx_PS3.Checked)
        //                    numPlatforms++;

        //                var DBc_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
        //                tsst = "start gen " + data.SongInfo.Artist + "-" + data.SongInfo.Album + "-" + data.SongInfo.SongDisplayName + "..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
        //                if (chbx_PC.Checked)
        //                    try
        //                    {
        //                        data.Pc = true;
        //                        bwRGenerate.ReportProgress(progress, "Generating PC package");
        //                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, CurrentGameVersion));
        //                        progress += step;
        //                        bwRGenerate.ReportProgress(progress);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                        {
        //                            ErrorWindow frm1 = new ErrorWindow("(4332)Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                            frm1.ShowDialog();
        //                        }
        //                        UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
        //                        errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //                    }
        //                else
        //                if (chbx_Mac.Checked)
        //                    try
        //                    {
        //                        data.Mac = true;
        //                        bwRGenerate.ReportProgress(progress, "Generating Mac package");
        //                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, CurrentGameVersion));
        //                        progress += step;
        //                        bwRGenerate.ReportProgress(progress);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                        {
        //                            ErrorWindow frm1 = new ErrorWindow("(4352)Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                            frm1.ShowDialog();
        //                        }
        //                        UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
        //                        errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //                    }
        //                else
        //                if (chbx_XBOX360.Checked)
        //                    try
        //                    {
        //                        data.XBox360 = true;
        //                        bwRGenerate.ReportProgress(progress, "Generating XBox 360 package");
        //                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, CurrentGameVersion));
        //                        progress += step;
        //                        bwRGenerate.ReportProgress(progress);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                        {
        //                            ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                            frm1.ShowDialog();
        //                        }
        //                        UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
        //                        errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //                    }
        //                else
        //                if (chbx_PS3.Checked)
        //                    try
        //                    {
        //                        data.PS3 = true;
        //                        bwRGenerate.ReportProgress(progress, "Generating PS3 package");
        //                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, CurrentGameVersion));
        //                        progress += step;
        //                        bwRGenerate.ReportProgress(progress);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                        {
        //                            ErrorWindow frm1 = new ErrorWindow("(4392)Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                            frm1.ShowDialog();
        //                        }
        //                        errorsFound.AppendLine(String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace));
        //                        UpdatePackingLog("LogPackingError", DBc_Path, packid, file.ID, ex.ToString());
        //                    }
        //                tsst = "end gen ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                data.CleanCache();
        //                i++;

        //                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + file.ID + "", "");
        //                var noOfRec = dus.Tables[0].Rows.Count;
        //                var XMLFilePath = "";
        //                for (var k = 0; k <= noOfRec - 1; k++)
        //                {
        //                    var ArrangementType = dus.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    XMLFilePath = ArrangementType == "Vocal" ? dus.Tables[0].Rows[k].ItemArray[0].ToString() : XMLFilePath;
        //                }
        //                if (file.Has_Vocals == "Yes" && (chbx_Additional_Manipulations.GetItemChecked(72) || chbx_Additional_Manipulations.GetItemChecked(73)))
        //                    if (File.Exists(XMLFilePath + ".orig")) File.Copy(XMLFilePath + ".orig", XMLFilePath, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        //                var source = chbx_PS3.Checked ? "_ps3.psarc.edat" : (chbx_PC.Checked ? "_p" : (chbx_Mac.Checked ? "_m" : "")) + ".psarc";

        //                if (File.Exists(dlcSavePath + source))
        //                {
        //                    //copyftp
        //                    string txt_FTPPat = "";
        //                    var dest = "";
        //                    countpacked++;
        //                    if (chbx_PS3.Checked && chbx_Additional_Manipulations.GetItemChecked(49))
        //                    {
        //                        if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP1"];
        //                        else txt_FTPPat = ConfigRepository.Instance()["dlcm_FTP2"];
        //                        dest = txt_TempPath.Text;
        //                        var a = FTPFile(txt_FTPPat, dlcSavePath + source, txt_TempPath.Text, "");
        //                        copyftp = " and " + a + "FTPed";
        //                        if (a == "Truly ") counttransf++;
        //                    }
        //                    else if ((chbx_PC.Checked || chbx_Mac.Checked) && chbx_Additional_Manipulations.GetItemChecked(49))
        //                    {
        //                        dest = txt_RocksmithDLCPath.Text + "\\" + FN + source;
        //                        if (File.Exists(dlcSavePath + source))
        //                            try
        //                            {
        //                                File.Copy(dlcSavePath + source, dest, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                                DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + FN + source + "\";");
        //                            }
        //                            catch (Exception ee)
        //                            {
        //                                copyftp = "Not "; Console.Write(ee.Message);

        //                            }
        //                        copyftp = " and " + copyftp + "Copied";
        //                        if (copyftp != "Not ") counttransf++;
        //                    }

        //                    //Add Pack Audit Trail
        //                    //calc hash and file size
        //                    System.IO.FileInfo fi = null;
        //                    try
        //                    {
        //                        fi = new System.IO.FileInfo(dlcSavePath + source);
        //                    }
        //                    catch (System.IO.FileNotFoundException ee)
        //                    {
        //                        Console.Write(ee.Message);
        //                        timestamp = UpdateLog(timestamp, "error at pack details save", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                        ErrorWindow frm1 = new ErrorWindow("(4447)DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
        //                        frm1.ShowDialog();
        //                    }

        //                    //Generating the HASH code
        //                    var FileHash = "";
        //                    FileHash = GetHash(dlcSavePath + source);
        //                    //using (FileStream fs = File.OpenRead(dlcSavePath + source))
        //                    //{
        //                    //    SHA1 sha = new SHA1Managed();
        //                    //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
        //                    //    fs.Close();
        //                    //}

        //                    var norec = 0;
        //                    var fnn = dlcSavePath + source;
        //                    var fnnon = Path.GetFileName(fnn);
        //                    var packn = fnn.Substring(0, fnn.IndexOf(fnnon)).Substring(0, fnn.Substring(0, fnn.IndexOf(fnnon)).Length - 1);
        //                    var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\" OR (FileName=\"" + fnnon + "\" AND PackPath=\"" + packn + "\");";
        //                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel, txt_DBFolder.Text, cnb);
        //                    norec = dfs.Tables[0].Rows.Count;
        //                    if (norec > 0) DeleteFromDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE ID IN (" + sel + ")");
        //                    string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform, Official";
        //                    var insertA = "\"" + dest + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + file.ID + ",\"" + file.DLC_Name + "\",\"" + ((chbx_XBOX360.Checked ? "XBOX360" : (chbx_PC.Checked ? "PC" : (chbx_Mac.Checked ? "MAC" : (chbx_PS3.Checked ? "PS3" : ""))))) + "\",\"" + Is_Original + "\"";
        //                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
        //                    tsst = "end add pack_audit ..."; timestamp = UpdateLog(timestamp, tsst, false);
        //                }
        //                //Restore the DDremoved copies
        //                xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml.old", System.IO.SearchOption.AllDirectories);
        //                platform = file.Folder_Name.GetPlatform();
        //                if (chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(3))
        //                {
        //                    //if (bassRemoved == "Yes") file.Has_BassDD = "Yes";
        //                    foreach (var xml in xmlFiles)
        //                    {
        //                        Song2014 xmlContent = null;
        //                        try
        //                        {
        //                            xmlContent = Song2014.LoadFromFile(xml);
        //                            if ((!(chbx_Additional_Manipulations.GetItemChecked(52) && file.Keep_BassDD == "Yes") && xmlContent.Arrangement.ToLower() == "bass" && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(5))
        //                                || (!(chbx_Additional_Manipulations.GetItemChecked(53) && file.Keep_DD == "Yes") && ((xmlContent.Arrangement.ToLower() == "lead" || xmlContent.Arrangement.ToLower() == "combo" || xmlContent.Arrangement.ToLower() == "rthythm"))
        //                                  && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipulations.GetItemChecked(3))
        //                               )
        //                            {
        //                                if (chbx_Additional_Manipulations.GetItemChecked(5) && !chbx_Additional_Manipulations.GetItemChecked(3) && !(xmlContent.Arrangement.ToLower() == "bass")) continue;
        //                                //Save a copy
        //                                File.Copy(xml.Replace(".old", ""), xml.Replace(".old", ".woDD"), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                                File.Copy(xml, xml.Replace(".old", ""), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                                var json = "";
        //                                if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                 
        //                                    json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
        //                                else
        //                                    json = xml.Replace("songs\\arr", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", System.IO.SearchOption.AllDirectories)[0])).Replace(".xml", ".json");

        //                                File.Copy(json.Replace(".old", ""), json.Replace(".old", ".woDD"), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                                File.Copy(json, json.Replace(".old", ""), true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //                            }
        //                        }
        //                        catch (Exception ee) { Console.Write(ee.Message); }
        //                    }
        //                }
        //                var DBc_Path2 = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
        //                if (File.Exists(dlcSavePath + source)) UpdatePackingLog("LogPacking", DBc_Path2, packid, file.ID, "done");
        //            }
        //        }
        //    }
        //    MessageBox.Show("Repack done " + countpacked + "/" + counttransf + " " + copyftp.Replace("  ", " "));
        //    string endtmp = (startT - DateTime.Now).ToString();
        //    // timestamp = UpdateLog(timestamp, "Ended " + endT + " (" + startT + ") after " + endtmp, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        //    tsst = "end repack ..." + "The End " + timestamp + " (" + startT + ") after " + endtmp; timestamp = UpdateLog(timestamp, tsst, false);
        //}


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
            SaveOK = "Ok";
            //Save settings
            SaveSettings();

            //Show Intro database window
            MainDB frm = new MainDB((txt_DBFolder.Text), txt_TempPath.Text, chbx_Additional_Manipulations.GetItemChecked(33), txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), cnb);
            frm.Show();
        }

        private void btn_Standardization_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveOK = "Ok";
            SaveSettings();
            var DBb_Path = txt_DBFolder.Text;// (chbx_DefaultDB.Checked == true ? MyAppWD :) + "\\Files.accdb";
            Standardization frm = new Standardization(DBb_Path, txt_TempPath.Text, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), cnb);
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
                timestamp = UpdateLog(timestamp, "Error cleaning Moving folder: " + source_dir, true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }

        private void chbx_DefaultDB_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_DefaultDB.Checked == true)
                txt_DBFolder.Text = MyAppWD + "\\..\\AccessDB.accdb";
        }

        private void btn_LoadRetailSongs_Click(object sender, EventArgs e)
        {
            timestamp = UpdateLog(timestamp, "Starting Retail Songs processing ...." + DateTime.Now, true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var Temp_Path_Import = txt_TempPath.Text + "\\dlcpacks";
            string pathDLC = txt_RocksmithDLCPath.Text;
            if (!chbx_DebugB.Checked) MessageBox.Show("Please make sure one of the following Retail Packs:\ncache.psarc, songs.psarc, rs1compatibilitydisc.psarc(.edat if PS3 format), rs1compatibilitydlc.psarc(.edat) \n\n, are in the Import Folder: " + pathDLC + "\n\nAlso, make sure you have enought space for the packing&unpacking operations Platform x 3GB");
            CreateTempFolderStructure(txt_TempPath.Text, txt_TempPath.Text + "\\0_old", txt_TempPath.Text + "\\0_broken", txt_TempPath.Text + "\\0_duplicate", txt_TempPath.Text + "\\0_dlcpacks", pathDLC, txt_TempPath.Text + "\\0_Repacked", txt_TempPath.Text + "\\0_Repacked\\XBOX", txt_TempPath.Text + "\\0_Repacked\\PC", txt_TempPath.Text + "\\0_Repacked\\MAC", txt_TempPath.Text + "\\0_Repacked\\PS", ConfigRepository.Instance()["dlcm_LogPath"], txt_TempPath.Text + "\\0_log", txt_TempPath.Text + "\\0_albumCovers");

            //read all the .PSARCs in the IMPORT folder
            var jsonFiles = Directory.GetFiles(pathDLC.Replace("Rocksmith2014\\DLC", "Rocksmith2014"), "*.psarc*", System.IO.SearchOption.AllDirectories);
            if (pathDLC.IndexOf("Rocksmith2014\\DLC") == 0) jsonFiles = Directory.GetFiles(pathDLC, "*.psarc*", System.IO.SearchOption.AllDirectories);

            var inputFilePath = ""; var locat = ""; var songshsanP = ""; var unpackedDir = "";
            var DBb_Path = txt_DBFolder.Text;// (chbx_DefaultDB.Checked == true ? MyAppWD : ) + "\\Files.accdb";
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
                    timestamp = UpdateLog(timestamp, "Error cleaning Temp Folder Cleaned", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //continue;
                }
            }

            //Clean CachetDB
            DeleteFromDB("Cache n", "DELETE FROM Cache;", cnb);
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 2 * 3; //jsonFiles.Length
                                         //UNPACK x3 psarcs
            foreach (var json in jsonFiles)
            {
                platformDLC = json.GetPlatform(); //Platform 
                platformDLCP = platformDLC.platform.ToString();
                if (json == pathDLC + "\\songs.psarc" || json == pathDLC + "\\rs1compatibilitydlc.psarc.edat" || json == pathDLC + "\\rs1compatibilitydisc.psarc.edat" || ((json == pathDLC + "\\rs1compatibilitydlc_p.psarc" || json == pathDLC + "\\rs1compatibilitydisc_p_Pc.psarc") && platformDLCP == "Pc") || ((json == pathDLC + "\\rs1compatibilitydlc_m.psarc" || json == pathDLC + "\\rs1compatibilitydisc_m.psarc") && platformDLCP == "Mac"))
                {
                    timestamp = UpdateLog(timestamp, "Decompressing  " + ".... " + json, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                timestamp = UpdateLog(timestamp, "Unpacking cache.psarc.... ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                timestamp = UpdateLog(timestamp, "Unpacking songs.psarc.... ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + inputFilePath + "---" + txt_TempPath.Text + "\\0songs", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                        }
                        else
                        {
                            unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\songs_" + platformDLCP;
                            songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                        }

                        timestamp = UpdateLog(timestamp, "Processed cache.psarc & songs.psarc", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + unpackedDir, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                        timestamp = UpdateLog(timestamp, "Repacking " + json + "2 use the internal/Browser Psarc Read function.... ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1dlc DLC pack !", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            timestamp = UpdateLog(timestamp, "renaming internal folder ", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        catch (Exception ex)
                        {
                            timestamp = UpdateLog(timestamp, ex.Message + "problem at dir rename" + unpackedDir, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        timestamp = UpdateLog(timestamp, "Processed rs1compatibilitydlc.psarc", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                        if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1dlc DLC pack !", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    }
                                    unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                                }
                                else unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", true, false);
                            }
                            catch (Exception ex)
                            {
                                timestamp = UpdateLog(timestamp, ex.Message + "problem at unpacking" + unpackedDir, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                        }
                        else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc_" + platformDLCP;

                        songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                        timestamp = UpdateLog(timestamp, "Repacking " + json + " 2 use the internal/Browser Psarc Read function.... " + json, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                if (DDC.ExitCode > 0) timestamp = UpdateLog(timestamp, "Issues when packing rs1disc pack !", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                                renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                                timestamp = UpdateLog(timestamp, "renaming internal folder", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                            timestamp = UpdateLog(timestamp, ex.Message + "problem at dir rename" + unpackedDir, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        timestamp = UpdateLog(timestamp, "Processed rs1compatibilitydisc.psarc", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    else continue;
                    Console.WriteLine("Opening archive {0} ...", inputFilePath);
                    Console.WriteLine();



                    //Populate DB
                    timestamp = UpdateLog(timestamp, "Populating CACHE DB", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        DataSet dsx = new DataSet(); dsx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "\"", txt_DBFolder.Text, cnb);

                        AudioP1 = (dsx.Tables[0].Rows.Count > 0) ? dsx.Tables[0].Rows[0].ItemArray[0].ToString() : "";

                        DataSet dsdx = new DataSet(); dsdx = SelectFromDB("WEM2OGGCorrespondence", "SELECT EncryptedID from WEM2OGGCorrespondence AS O WHERE Identifier=\"" + song.Identifier + "_preview\"", txt_DBFolder.Text, cnb);
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

                        DataSet dtx = new DataSet(); dtx = SelectFromDB("Cache", "SELECT ID from Cache AS O WHERE Platform=\"" + platformDLCP + "\" AND Identifier=\"" + song.Identifier.ToString() + "\"", txt_DBFolder.Text, cnb);
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
                                timestamp = UpdateLog(timestamp, Environment.NewLine, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                    }
                }//END no cache.psarc to be decompressed
            }
            timestamp = UpdateLog(timestamp, "Ending Retail Songs processing ...." + DateTime.Now, true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            Cache frm = new Cache(DBb_Path, txt_TempPath.Text, pathDLC, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), cnb);
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Save settings
            SaveSettings();
            Cache frm = new Cache(txt_DBFolder.Text, txt_TempPath.Text, txt_RocksmithDLCPath.Text, chbx_Additional_Manipulations.GetItemChecked(39), chbx_Additional_Manipulations.GetItemChecked(40), cnb);
            frm.ShowDialog();//(chbx_DefaultDB.Checked == true ? MyAppWD :) + "\\Files.accdb"
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


            timestamp = UpdateLog(timestamp, "Ended Decompressing WEMs", true, logPath, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
            var DB_Path = txt_DBFolder.Text;// (chbx_DefaultDB.Checked == true ? MyAppWD : ) + "\\Files.accdb";
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
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";", txt_DBFolder.Text, cnb);
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
            SaveSettings();
            var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, "Saving Profile", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" and Type=\"Profile\";", txt_DBFolder.Text, cnb);
            var norec = 0; norec = drs.Tables.Count;
            if (norec > 0)// drs.Tables[0].Rows.Count;
                if (drs.Tables[0].Rows.Count == 0)
                {
                    DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text, cnb);

                    norec = ds.Tables[0].Rows.Count;
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString() + 1;
                    string insertcmdA = "CDLC_ID, Groups, Type, Comments, Profile_Name";
                    //var insertA = "\"" + fnn + "\",\"" + txt_RocksmithDLCPath.Text + "\",\"Profile\",\"Rocksmith\",\"" + chbx_Configurations.Text + "\"";
                    //InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);

                    //insertA = "\"" + fnn + "\",\"" + txt_TempPath.Text + "\",\"Profile\",\"Temp\",\"" + chbx_Configurations.Text + "\"";
                    //InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);

                    //insertA = "\"" + fnn + "\",\"" + txt_DBFolder.Text + "\",\"Profile\",\"DB\",\"" + chbx_Configurations.Text + "\"";
                    //InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);

                    var insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Album"] + "\",\"Profile\",\"dlcm_Activ_Album\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Artist"] + "\",\"Profile\",\"dlcm_Activ_Artist\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] + "\",\"Profile\",\"dlcm_Activ_ArtistSort\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_FileName"] + "\",\"Profile\",\"dlcm_Activ_FileName\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_Title"] + "\",\"Profile\",\"dlcm_Activ_Title\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Activ_TitleSort"] + "\",\"Profile\",\"dlcm_Activ_TitleSort\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul0"] + "\",\"Profile\",\"dlcm_AdditionalManipul0\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul1"] + "\",\"Profile\",\"dlcm_AdditionalManipul1\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul10"] + "\",\"Profile\",\"dlcm_AdditionalManipul10\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul11"] + "\",\"Profile\",\"dlcm_AdditionalManipul11\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul12"] + "\",\"Profile\",\"dlcm_AdditionalManipul12\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul13"] + "\",\"Profile\",\"dlcm_AdditionalManipul13\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul14"] + "\",\"Profile\",\"dlcm_AdditionalManipul14\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul15"] + "\",\"Profile\",\"dlcm_AdditionalManipul15\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul16"] + "\",\"Profile\",\"dlcm_AdditionalManipul16\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul17"] + "\",\"Profile\",\"dlcm_AdditionalManipul17\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul18"] + "\",\"Profile\",\"dlcm_AdditionalManipul18\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul19"] + "\",\"Profile\",\"dlcm_AdditionalManipul19\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul2"] + "\",\"Profile\",\"dlcm_AdditionalManipul2\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul20"] + "\",\"Profile\",\"dlcm_AdditionalManipul20\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul21"] + "\",\"Profile\",\"dlcm_AdditionalManipul21\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul22"] + "\",\"Profile\",\"dlcm_AdditionalManipul22\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul23"] + "\",\"Profile\",\"dlcm_AdditionalManipul23\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul24"] + "\",\"Profile\",\"dlcm_AdditionalManipul24\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul25"] + "\",\"Profile\",\"dlcm_AdditionalManipul25\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul26"] + "\",\"Profile\",\"dlcm_AdditionalManipul26\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul27"] + "\",\"Profile\",\"dlcm_AdditionalManipul27\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul28"] + "\",\"Profile\",\"dlcm_AdditionalManipul28\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul29"] + "\",\"Profile\",\"dlcm_AdditionalManipul29\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul3"] + "\",\"Profile\",\"dlcm_AdditionalManipul3\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul30"] + "\",\"Profile\",\"dlcm_AdditionalManipul30\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul31"] + "\",\"Profile\",\"dlcm_AdditionalManipul31\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul32"] + "\",\"Profile\",\"dlcm_AdditionalManipul32\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul33"] + "\",\"Profile\",\"dlcm_AdditionalManipul33\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul34"] + "\",\"Profile\",\"dlcm_AdditionalManipul34\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul35"] + "\",\"Profile\",\"dlcm_AdditionalManipul35\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul36"] + "\",\"Profile\",\"dlcm_AdditionalManipul36\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul37"] + "\",\"Profile\",\"dlcm_AdditionalManipul37\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul38"] + "\",\"Profile\",\"dlcm_AdditionalManipul38\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul39"] + "\",\"Profile\",\"dlcm_AdditionalManipul39\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul4"] + "\",\"Profile\",\"dlcm_AdditionalManipul4\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul40"] + "\",\"Profile\",\"dlcm_AdditionalManipul40\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul41"] + "\",\"Profile\",\"dlcm_AdditionalManipul41\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul42"] + "\",\"Profile\",\"dlcm_AdditionalManipul42\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul43"] + "\",\"Profile\",\"dlcm_AdditionalManipul43\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul44"] + "\",\"Profile\",\"dlcm_AdditionalManipul44\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul45"] + "\",\"Profile\",\"dlcm_AdditionalManipul45\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul46"] + "\",\"Profile\",\"dlcm_AdditionalManipul46\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul47"] + "\",\"Profile\",\"dlcm_AdditionalManipul47\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul48"] + "\",\"Profile\",\"dlcm_AdditionalManipul48\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul49"] + "\",\"Profile\",\"dlcm_AdditionalManipul49\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul5"] + "\",\"Profile\",\"dlcm_AdditionalManipul5\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul50"] + "\",\"Profile\",\"dlcm_AdditionalManipul50\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul51"] + "\",\"Profile\",\"dlcm_AdditionalManipul51\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul52"] + "\",\"Profile\",\"dlcm_AdditionalManipul52\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul53"] + "\",\"Profile\",\"dlcm_AdditionalManipul53\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul54"] + "\",\"Profile\",\"dlcm_AdditionalManipul54\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul55"] + "\",\"Profile\",\"dlcm_AdditionalManipul55\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul56"] + "\",\"Profile\",\"dlcm_AdditionalManipul56\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul57"] + "\",\"Profile\",\"dlcm_AdditionalManipul57\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul58"] + "\",\"Profile\",\"dlcm_AdditionalManipul58\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul59"] + "\",\"Profile\",\"dlcm_AdditionalManipul59\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul6"] + "\",\"Profile\",\"dlcm_AdditionalManipul6\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul60"] + "\",\"Profile\",\"dlcm_AdditionalManipul60\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul61"] + "\",\"Profile\",\"dlcm_AdditionalManipul61\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul62"] + "\",\"Profile\",\"dlcm_AdditionalManipul62\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul63"] + "\",\"Profile\",\"dlcm_AdditionalManipul63\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul64"] + "\",\"Profile\",\"dlcm_AdditionalManipul64\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul65"] + "\",\"Profile\",\"dlcm_AdditionalManipul65\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul66"] + "\",\"Profile\",\"dlcm_AdditionalManipul66\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul67"] + "\",\"Profile\",\"dlcm_AdditionalManipul67\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul68"] + "\",\"Profile\",\"dlcm_AdditionalManipul68\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul69"] + "\",\"Profile\",\"dlcm_AdditionalManipul69\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul7"] + "\",\"Profile\",\"dlcm_AdditionalManipul7\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul70"] + "\",\"Profile\",\"dlcm_AdditionalManipul70\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul71"] + "\",\"Profile\",\"dlcm_AdditionalManipul71\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul72"] + "\",\"Profile\",\"dlcm_AdditionalManipul72\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul73"] + "\",\"Profile\",\"dlcm_AdditionalManipul73\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul74"] + "\",\"Profile\",\"dlcm_AdditionalManipul74\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul75"] + "\",\"Profile\",\"dlcm_AdditionalManipul75\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + "\",\"Profile\",\"dlcm_AdditionalManipul76\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul77"] + "\",\"Profile\",\"dlcm_AdditionalManipul77\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul78"] + "\",\"Profile\",\"dlcm_AdditionalManipul78\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul79"] + "\",\"Profile\",\"dlcm_AdditionalManipul79\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul8"] + "\",\"Profile\",\"dlcm_AdditionalManipul8\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul80"] + "\",\"Profile\",\"dlcm_AdditionalManipul80\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_AdditionalManipul9"] + "\",\"Profile\",\"dlcm_AdditionalManipul9\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Album"] + "\",\"Profile\",\"dlcm_Album\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist"] + "\",\"Profile\",\"dlcm_Artist\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Artist_Sort"] + "\",\"Profile\",\"dlcm_Artist_Sort\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_cbx_Groups"] + "\",\"Profile\",\"dlcm_cbx_Groups\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_Mac"] + "\",\"Profile\",\"dlcm_chbx_Mac\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_PC"] + "\",\"Profile\",\"dlcm_chbx_PC\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_PS3"] + "\",\"Profile\",\"dlcm_chbx_PS3\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_chbx_XBOX360"] + "\",\"Profile\",\"dlcm_chbx_XBOX360\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DBFolder"] + "\",\"Profile\",\"dlcm_DBFolder\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Debug"] + "\",\"Profile\",\"dlcm_Debug\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_DefaultDB"] + "\",\"Profile\",\"dlcm_DefaultDB\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_File_Name"] + "\",\"Profile\",\"dlcm_File_Name\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_netstatus"] + "\",\"Profile\",\"dlcm_netstatus\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] + "\",\"Profile\",\"dlcm_RocksmithDLCPath\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_TempPath"] + "\",\"Profile\",\"dlcm_TempPath\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Title"] + "\",\"Profile\",\"dlcm_Title\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    insertA = "\"" + fnn + "\",\"" + ConfigRepository.Instance()["dlcm_Title_Sort"] + "\",\"Profile\",\"dlcm_Title_Sort\",\"" + chbx_Configurations.Text + "\""; InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                }
                else
                {
                    DialogResult result1 = MessageBox.Show("Do you Wanna rename the profile?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes) { DataSet dts = new DataSet(); dts = UpdateDB("Groups", "UPDATE Groups SET Profile_Name=\"" + chbx_Configurations.Text + "\" WHERE CDLC_ID=" + drs.Tables[0].Rows[0].ItemArray[0].ToString() + "and Type=\"Profile\";", cnb); }
                    else MessageBox.Show("Please chose a unique name");
                }
            timestamp = UpdateLog(timestamp, "End SAving the Profile", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
            if (File.Exists(txt_DBFolder.Text)) // + "\\Files.accdb"
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Profile_Name FROM Groups WHERE Type=\"Profile\";", txt_DBFolder.Text, cnb);
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
            var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, "Deleting the Profile", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (chbx_Configurations.Text == "Select Profile") return;
            var cmd = "DELETE FROM Groups WHERE Type=\"Profile\" AND Profile_Name= \"" + chbx_Configurations.Text + "\"";
            DeleteFromDB("Groups", cmd, cnb);
            timestamp = UpdateLog(timestamp, "End Deleting the Profile", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

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
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT Groups FROM Groups WHERE Profile_Name=\"" + chbx_Configurations.Text + "\" ORDER BY Comments ASC", txt_DBFolder.Text, cnb);
            var norec = 0;
            if (ds.Tables.Count > 0) norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                if (chbx_Configurations.Text == ConfigRepository.Instance()["dlcm_DebugProfile"])
                {
                    chbx_Additional_Manipulations.SetItemCheckState(15, CheckState.Unchecked);//move to old
                    chbx_Additional_Manipulations.SetItemCheckState(49, CheckState.Unchecked);//ftp
                    chbx_Additional_Manipulations.SetItemCheckState(24, CheckState.Unchecked);//apply standard
                    chbx_Additional_Manipulations.SetItemCheckState(77, CheckState.Unchecked);//dont open main db
                    chbx_Additional_Manipulations.SetItemCheckState(75, CheckState.Checked);//copy to old
                    chbx_Additional_Manipulations.SetItemCheckState(34, CheckState.Unchecked);//audio
                    chbx_Additional_Manipulations.SetItemCheckState(55, CheckState.Unchecked);//audio
                    chbx_Additional_Manipulations.SetItemCheckState(69, CheckState.Unchecked);//audio
                    chbx_Additional_Manipulations.SetItemCheckState(78, CheckState.Unchecked);//audio
                    chbx_CleanDB.Checked = true;
                    chbx_CleanTemp.Checked = true;
                    chbx_DebugB.Checked = true;
                }
                if (chbx_Configurations.Text == "Select Profile") return;
                ConfigRepository.Instance()["dlcm_Activ_Album"] = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Activ_Artist"] = ds.Tables[0].Rows[1].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] = ds.Tables[0].Rows[2].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Activ_FileName"] = ds.Tables[0].Rows[3].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Activ_Title"] = ds.Tables[0].Rows[4].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Activ_TitleSort"] = ds.Tables[0].Rows[5].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul0"] = ds.Tables[0].Rows[6].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul1"] = ds.Tables[0].Rows[7].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul10"] = ds.Tables[0].Rows[8].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul11"] = ds.Tables[0].Rows[9].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul12"] = ds.Tables[0].Rows[10].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul13"] = ds.Tables[0].Rows[11].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul14"] = ds.Tables[0].Rows[12].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul15"] = ds.Tables[0].Rows[13].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul16"] = ds.Tables[0].Rows[14].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul17"] = ds.Tables[0].Rows[15].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul18"] = ds.Tables[0].Rows[16].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul19"] = ds.Tables[0].Rows[17].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul2"] = ds.Tables[0].Rows[18].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul20"] = ds.Tables[0].Rows[19].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul21"] = ds.Tables[0].Rows[20].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul22"] = ds.Tables[0].Rows[21].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul23"] = ds.Tables[0].Rows[22].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul24"] = ds.Tables[0].Rows[23].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul25"] = ds.Tables[0].Rows[24].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul26"] = ds.Tables[0].Rows[25].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul27"] = ds.Tables[0].Rows[26].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul28"] = ds.Tables[0].Rows[27].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul29"] = ds.Tables[0].Rows[28].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul3"] = ds.Tables[0].Rows[29].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul30"] = ds.Tables[0].Rows[30].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul31"] = ds.Tables[0].Rows[31].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul32"] = ds.Tables[0].Rows[32].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul33"] = ds.Tables[0].Rows[33].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul34"] = ds.Tables[0].Rows[34].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul35"] = ds.Tables[0].Rows[35].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul36"] = ds.Tables[0].Rows[36].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul37"] = ds.Tables[0].Rows[37].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul38"] = ds.Tables[0].Rows[38].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul39"] = ds.Tables[0].Rows[39].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul4"] = ds.Tables[0].Rows[40].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul40"] = ds.Tables[0].Rows[41].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul41"] = ds.Tables[0].Rows[42].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul42"] = ds.Tables[0].Rows[43].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul43"] = ds.Tables[0].Rows[44].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul44"] = ds.Tables[0].Rows[45].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul45"] = ds.Tables[0].Rows[46].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul46"] = ds.Tables[0].Rows[47].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul47"] = ds.Tables[0].Rows[48].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul48"] = ds.Tables[0].Rows[49].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul49"] = ds.Tables[0].Rows[50].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul5"] = ds.Tables[0].Rows[51].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul50"] = ds.Tables[0].Rows[52].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul51"] = ds.Tables[0].Rows[53].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul52"] = ds.Tables[0].Rows[54].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul53"] = ds.Tables[0].Rows[55].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul54"] = ds.Tables[0].Rows[56].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul55"] = ds.Tables[0].Rows[57].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul56"] = ds.Tables[0].Rows[58].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul57"] = ds.Tables[0].Rows[59].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul58"] = ds.Tables[0].Rows[60].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul59"] = ds.Tables[0].Rows[61].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul6"] = ds.Tables[0].Rows[62].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul60"] = ds.Tables[0].Rows[63].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul61"] = ds.Tables[0].Rows[64].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul62"] = ds.Tables[0].Rows[65].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul63"] = ds.Tables[0].Rows[66].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul64"] = ds.Tables[0].Rows[67].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul65"] = ds.Tables[0].Rows[68].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul66"] = ds.Tables[0].Rows[69].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul67"] = ds.Tables[0].Rows[70].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul68"] = ds.Tables[0].Rows[71].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul69"] = ds.Tables[0].Rows[72].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul7"] = ds.Tables[0].Rows[73].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul70"] = ds.Tables[0].Rows[74].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul71"] = ds.Tables[0].Rows[75].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul72"] = ds.Tables[0].Rows[76].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul73"] = ds.Tables[0].Rows[77].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul74"] = ds.Tables[0].Rows[78].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul75"] = ds.Tables[0].Rows[79].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul76"] = ds.Tables[0].Rows[80].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul77"] = ds.Tables[0].Rows[81].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul78"] = ds.Tables[0].Rows[82].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul79"] = ds.Tables[0].Rows[83].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul8"] = ds.Tables[0].Rows[84].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul80"] = ds.Tables[0].Rows[85].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_AdditionalManipul9"] = ds.Tables[0].Rows[86].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Album"] = ds.Tables[0].Rows[87].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Artist"] = ds.Tables[0].Rows[88].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Artist_Sort"] = ds.Tables[0].Rows[89].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_cbx_Groups"] = ds.Tables[0].Rows[90].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_chbx_Mac"] = ds.Tables[0].Rows[91].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_chbx_PC"] = ds.Tables[0].Rows[92].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_chbx_PS3"] = ds.Tables[0].Rows[93].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_chbx_XBOX360"] = ds.Tables[0].Rows[94].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_DBFolder"] = ds.Tables[0].Rows[95].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Debug"] = ds.Tables[0].Rows[96].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_DefaultDB"] = ds.Tables[0].Rows[97].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_File_Name"] = ds.Tables[0].Rows[98].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_netstatus"] = ds.Tables[0].Rows[99].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_RocksmithDLCPath"] = ds.Tables[0].Rows[100].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_TempPath"] = ds.Tables[0].Rows[101].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Title"] = ds.Tables[0].Rows[102].ItemArray[0].ToString();
                ConfigRepository.Instance()["dlcm_Title_Sort"] = ds.Tables[0].Rows[103].ItemArray[0].ToString();
            }
            if (!File.Exists(txt_DBFolder.Text))// + "\\Files.accdb"
                chbx_DefaultDB.Checked = true;
            else if (!(txt_DBFolder.Text == MyAppWD)) chbx_DefaultDB.Checked = false;
            SaveOK = "OK";
            DLCManagerOpen();
            SaveSettings();
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
            //var logPatht = (logPath == null || !Directory.Exists(logPath) ? AppWD + ConfigRepository.Instance()["dlcm_LogPath"] : logPath) + "\\";
            var logPatht = (logPath == null || !Directory.Exists(logPath) ? txt_TempPath.Text + "\\0_log" : logPath);
            if (Directory.Exists(logPatht))
            {
                try
                {
                    Process process = Process.Start(@logPatht);
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
            //if (rbtn_Population_All.Checked) RefreshSelectedStat("Main", "1=1");
            //else if (rbtn_Population_Groups.Checked) RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groups=\"" + cbx_Groups.Text + "\")");
            //else if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")");
            SetImportNo();
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

        ////[XmlType("vocal")]
        //public class newLyrics
        //{
        //    //[XmlAttribute("time")]
        //    public float Time { get; set; }

        //    //[XmlAttribute("note")]
        //    public Int32 Note { get; set; }

        //    //[XmlAttribute("length")]
        //    public float Length { get; set; }

        //    //[XmlAttribute("lyric")] // len 32 (RS1) | len 48 (RS2014)
        //    public string Lyric { get; set; }
        //}

        //private void GetTrackStartTimeW(object sender, EventArgs e)
        //{
        //    //var ST = "C:\\GitHub\\Quick\\0\\Pc_CDLC_Bush_2001_Golden State_3_The People That We Love_86307\\songs\\arr\\cusbushthepeoplethatwelove_vocals.xml";

        //    //Add2LinesInVocals(ST,2);
        //    //Vocals xmlContent = null;
        //    //try
        //    //{
        //    //    xmlContent = Vocals.LoadFromFile(ST);

        //    //    //SongLenght = xmlContent.SongLength.ToString();
        //    //}
        //    //catch (Exception ee)
        //    //{
        //    //    Console.Write(ee.Message);
        //    //}

        //    //Vocals newLyrics = null;
        //    //try
        //    //{
        //    //    newLyrics = Vocals.LoadFromFile(ST + ".newvcl");

        //    //    //SongLenght = xmlContent.SongLength.ToString();
        //    //}
        //    //catch (Exception ee)
        //    //{
        //    //    Console.Write(ee.Message);
        //    //}
        //    //if (!File.Exists(ST + ".orig")) File.Copy(ST, ST + ".orig");
        //    ////newLyrics.Vocal[1].Time;
        //    ////newLyrics = Vocals.LoadFromFile(ST);
        //    //newLyrics.Vocal[0].Time = 0;
        //    //newLyrics.Vocal[0].Note = 254;
        //    //newLyrics.Vocal[0].Length = 3;
        //    //newLyrics.Vocal[0].Lyric = "Song Details:";
        //    //newLyrics.Vocal[1].Time = 3;
        //    //newLyrics.Vocal[1].Note = 254;
        //    //newLyrics.Vocal[1].Length = 2;
        //    //newLyrics.Vocal[1].Lyric = "Lyrics Details:";

        //    ////for (var i = 0; i < xmlContent.Count; i++)
        //    ////{
        //    ////    newLyrics.Vocal[i+2].Time = xmlContent.Vocal[i].Time;
        //    ////    newLyrics.Vocal[i + 2].Note = xmlContent.Vocal[i].Note;
        //    ////    newLyrics.Vocal[i + 2].Length = xmlContent.Vocal[i].Length;
        //    ////    newLyrics.Vocal[i + 2].Lyric = xmlContent.Vocal[i].Lyric;
        //    ////}
        //    ////rev.Length = xmlContent.Vocal[i].Length;turn ST;
        //    ////xmv.Lyric = xmlContent.Vocal[i].Lyric;lContent.Vocal.
        //    ////var v1 = new RocksmithToolkitLib.XML.Vocal();
        //    ////v1.Time = 0;
        //    ////v1.Note = 254;
        //    ////v1.Length = 2;
        //    ////v1.Lyric = "bbog";

        //    ////Vocals newLyrics = new Vocals(ST,false);
        //    //////newLyrics.Vocal[0] = v1;
        //    ////newLyrics.Vocal[1].Note = 254;
        //    ////for (var i = 0; i < xmlContent.Count; i++)
        //    ////{
        //    ////    var v = new RocksmithToolkitLib.XML.Vocal();
        //    ////    v.Time= xmlContent.Vocal[i].Time;
        //    ////    v.Note = xmlContent.Vocal[i].Note;
        //    ////    v.Length = xmlContent.Vocal[i].Length;
        //    ////    v.Lyric = xmlContent.Vocal[i].Lyric;

        //    ////    newLyrics.Vocal[i+2] = v;
        //    ////}
        //    //// write updated xml arrangement
        //    //using (var stream = File.Open(ST, FileMode.Create))
        //    //    newLyrics.Serialize(stream);
        //}



        private void SetImportNo()
        {
            string[] filez;
            var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, "Starting SetNos", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (Directory.Exists(txt_RocksmithDLCPath.Text))
            {
                if (chbx_Additional_Manipulations.GetItemChecked(37)) //38. Import other formats but PC, as well (as duplciates)
                    filez = System.IO.Directory.GetFiles(txt_RocksmithDLCPath.Text, "*.psarc*");
                else
                    filez = System.IO.Directory.GetFiles(txt_RocksmithDLCPath.Text, "*_p.psarc");
                btn_PopulateDB.Text = "Import " + filez.Count().ToString() + " DLCs";

            }
            else btn_PopulateDB.Text = "Import N/A DLCs";

            DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT * FROM Main;", "", cnb);
            //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            var noOfRec = dus.Tables.Count > 0 ? dus.Tables[0].Rows.Count : 0;
            btn_OpenMainDB.Text = "Open MainDB" + " (" + noOfRec.ToString() + ")";
            //if (noOfRec > 0) else btn_OpenMainDB.Text = "Open MainDB";

            if (rbtn_Population_All.Checked) RefreshSelectedStat("Main", "1=1");
            else if (rbtn_Population_Groups.Checked) RefreshSelectedStat("Groups", "(Type=\"DLC\" AND Groups=\"" + cbx_Groups.Text + "\")");
            else if (rbtn_Population_Selected.Checked) RefreshSelectedStat("Main", "(Selected =\"Yes\")");
            timestamp = UpdateLog(timestamp, "Ending SetNos", true, logPath, txt_TempPath.Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
            string dest = txt_RocksmithDLCPath.Text + "\\AccessDB.accdb";
            if (!Directory.Exists(txt_TempPath.Text)) Directory.CreateDirectory(txt_TempPath.Text);
            if (!Directory.Exists(txt_RocksmithDLCPath.Text)) Directory.CreateDirectory(txt_RocksmithDLCPath.Text);
            if (File.Exists(fielPath))
                try
                {
                    File.Copy(fielPath, dest, true);
                    txt_DBFolder.Text = dest;
                    //txt_DBFolder.Text = dest;
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

        private void btn_Param_Click(object sender, EventArgs e)
        {
            var patt = MyAppWD + "\\..\\..\\RocksmithToolkitLib.Config.xml";
            //if (File.Exists(patt))
            //{
            try
            {
                Process process = Process.Start(@patt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Param folder in Exporer !");
            }
            //}
        }

        private void txt_DBFolder_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txt_DBFolder.Text))
            {
                cnb.Close();
                cnb.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Mode= Share Deny None;Data Source=" + txt_DBFolder.Text;
                //cnb.Close();
                SetImportNo();
                if (txt_DBFolder.Text != MyAppWD + "\\..\\AccessDB.accdb") chbx_DefaultDB.Checked = false;
                SaveOK = "OK";
                SaveSettings();
                //cnb.Open();
            }
            //if (!(txt_DBFolder.Text == MyAppWD))
            //    chbx_DefaultDB.Checked = false;
            //if (txt_DBFolder.Text.Length > 0) if ((txt_DBFolder.Text.Substring(txt_DBFolder.Text.Length - 1, 1) == "\\")) txt_DBFolder.Text = txt_DBFolder.Text.Substring(0, txt_DBFolder.Text.Length - 1);

            //cnb.ConnectionString = txt_DBFolder.Text;OLE DB Services=-2;Mode=Read;
        }

        private void btn_Debbug_Click(object sender, EventArgs e)
        {
            ProgressBar pB_ReadDLCs = new ProgressBar();
            pB_ReadDLCs.Location = new System.Drawing.Point(11, 137);
            pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            pB_ReadDLCs.Maximum = 4;
            pB_ReadDLCs.Name = "pB_ReadDLCs";
            pB_ReadDLCs.Size = new System.Drawing.Size(1051, 28);
            pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.TabIndex = 263;
            this.toolTip1.SetToolTip(pB_ReadDLCs, "Progress bar for different operations of CDLC Manager.");
            this.Controls.Add(pB_ReadDLCs);
            //             timestamp = UpdateLog(timestamp, "Issues at decompressing WEMs or FAILED2 empty path", true, logPath, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.CreateGraphics().DrawString("testing", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //DateTime timestamp = UpdateLogs(DateTime.Now, "Issues at decompressing WEMs or FAILED2 empty path", true, logPath, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void txt_RocksmithDLCPath_TextChanged_1(object sender, EventArgs e)
        {
            if (Directory.Exists(txt_RocksmithDLCPath.Text))
            {
                if (txt_RocksmithDLCPath.Text.Length > 0) if ((txt_RocksmithDLCPath.Text.Substring(txt_RocksmithDLCPath.Text.Length - 1, 1) == "\\")) txt_RocksmithDLCPath.Text = txt_RocksmithDLCPath.Text.Substring(0, txt_RocksmithDLCPath.Text.Length - 1);
                SetImportNo();
                if (SaveOK != "") SaveSettings();
            }
        }

        private void txt_TempPath_TextChanged_1(object sender, EventArgs e)
        {
            if (Directory.Exists(txt_TempPath.Text))
            {
                if (txt_TempPath.Text.Length > 0) if ((txt_TempPath.Text.Substring(txt_TempPath.Text.Length - 1, 1) == "\\")) txt_TempPath.Text = txt_TempPath.Text.Substring(0, txt_TempPath.Text.Length - 1);
                if (SaveOK != "") SaveSettings();
            }
        }
    }
}