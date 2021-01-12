using Ookii.Dialogs; //cue text
using RocksmithToolkitLib;//4REPACKING
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.Sng2014HSL;
using RocksmithToolkitLib.XML; //For xml read library
using RocksmithToolkitLib.XmlRepository;
using RocksmithToTabLib;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
//using SpotifyAPI.Web.Enums; //Enums
//using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System;
using System.Collections.Generic;
using System.Collections.Specialized;//webparsing
using System.ComponentModel;
using System.Data;
//bcapi
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net; //4ftp
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;

namespace RocksmithToolkitGUI.DLCManager
{

    public partial class MainDB : Form
    {
        public bool AfterImport;
        public MainDB(OleDbConnection cnnb, bool AI)
        {
            InitializeComponent();
            AfterImport = AI;
            cnb = cnnb;

        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        public static BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private BackgroundWorker bwConvert;
        public string convdone;
        int GoTocounter = 0;
        private StringBuilder errorsFound;
        public Platform SourcePlatform { get; set; }

        // Create the ToolTip and associate with the Form container.
        ToolTip toolTip2 = new ToolTip();
        ToolTip toolTip3 = new ToolTip();
        ToolTip toolTip4 = new ToolTip();
        ToolTip toolTip5 = new ToolTip();
        ToolTip toolTip6 = new ToolTip();

        public Platform TargetPlatform { get; set; }
        public string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        public static Process DDC = new Process();
        public static bool ProcessStarted = false;
        public bool SaveOK = false;
        public bool ChangeRows = false;
        public bool SearchON = false;
        public bool SearchExit = false;
        public bool AddPreview = false;
        ToolTip toolTip11 = new ToolTip();
        ToolTip toolTip12 = new ToolTip();
        ToolTip toolTip13 = new ToolTip();
        ToolTip toolTip14 = new ToolTip();
        ToolTip toolTip15 = new ToolTip();
        ToolTip toolTip16 = new ToolTip();
        string Groupss = "";
        static string debug = "";
        public string netstatus = c("dlcm_netstatus");

        private static string _clientId = "";
        private static string _secretId = "";

        //public static SpotifyWebAPI _spotify;
        public string _trackno;
        public string _year;
        //public static PrivateProfile _profile;
        public List<FullTrack> _savedTracks;
        public List<SimplePlaylist> _playlists;
        public string Archive_Path = c("dlcm_TempPath") + "\\0_archive";

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        public bool GroupChanged = false;
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        public string SearchFields = "";
        public OleDbConnection cnb;
        public string GetTrkTxt = "";

        DateTime timestamp;
        string logPath = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
        string tmpPath = c("dlcm_TempPath");
        public static BackgroundWorker bwAutoPlay;

        private void MainDB_Load(object sender, EventArgs e)
        {
            prevWidth = Width;
            prevWindowState = WindowState;
            toolTip1.ShowAlways = true;

            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            var fnl = (logPath == null || !Directory.Exists(logPath) ? c("dlcm_TempPath") + "\\0_log" : logPath) + "\\" + "current_maindbtemp.txt";

            var starttmp = DateTime.Now;
            if (File.Exists((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp.txt"))
            {
                File.Copy((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp.txt"
                      , (logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp" + startT + ".txt", true);
                FileStream swt = File.Open((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp.txt", FileMode.Create);
                swt.Dispose();
            }
            else
            {
                FileStream swt = File.Open((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp.txt", FileMode.Create);
                swt.Dispose();
            }

            bwAutoPlay = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwAutoPlay.DoWork += new DoWorkEventHandler(PlayPreview);
            bwAutoPlay.WorkerReportsProgress = true;

            var tst = "Starting... " + startT; timestamp = UpdateLog(starttmp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            btn_Copy_old.Enabled = true;
            SearchFields = c("dlcm_SearchFields");
            SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY " + c("dlcm_OrderOfFields") + ";";

            //Defaults
            chbx_Format.Text = ConfigRepository.Instance()["dlcm_MainDBFormat"];
            txt_FTPPath.Text = c("dlcm_" + chbx_Format.Text.Replace("PS3_", "FTP"));/*.Replace("Path", "")*/
            txt_PreviewStart.Value.AddMinutes(10);
            if (c("dlcm_RemoveBassDD") == "Yes") chbx_RemoveBassDD.Checked = true;
            else chbx_RemoveBassDD.Checked = false;
            if (c("dlcm_UniqueID") == "Yes") chbx_UniqueID.Checked = true;
            else chbx_UniqueID.Checked = false;
            if (c("dlcm_AdditionalManipul92") == "Yes") chbx_PS3HAN.Checked = true;
            else chbx_PS3HAN.Checked = false;
            if (c("dlcm_AdditionalManipul93") == "Yes") chbx_PS3Retail.Checked = true;
            else chbx_PS3Retail.Checked = false;

            if (c("dlcm_andCopy") == "Yes") chbx_Copy.Checked = true;
            else chbx_Copy.Checked = false;
            chbx_AutoSave.Checked = c("dlcm_Autosave") == "Yes" ? true : false;
            if (c("dlcm_AutoPlay").ToLower() == "yes") chbx_AutoPlay.Checked = true;
            else chbx_AutoPlay.Checked = false;
            chbx_Replace.Checked = c("dlcm_Replace") == "Yes" ? true : false;
            tst = "Stop reading config data... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            ChangeRows = false; Populate(ref databox, ref Main); ChangeRows = true;
            tst = "Stop populating... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            // Loads Groups in chbx_AllGroups Filter box cmb_Filter //Create Groups list Dropbox
            var norec = 0;
            DataSet dsn = new DataSet(); dsn = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type =\"DLC\";", "", cnb);
            norec = dsn.Tables.Count < 1 ? 0 : dsn.Tables[0].Rows.Count;
            if (norec > 0)
                for (int j = 0; j < norec; j++)
                    cmb_Filter.Items.Add("Group " + dsn.Tables[0].Rows[j][0].ToString());//add items

            // Loads Tunnings in Filter box cmb_Filter
            norec = 0;
            DataSet dbn = new DataSet(); dbn = SelectFromDB("Arrangements", "SELECT DISTINCT Tunning FROM Arrangements;", "", cnb);
            norec = dbn.Tables.Count < 1 ? 0 : dbn.Tables[0].Rows.Count;
            if (norec > 0)
                for (int j = 0; j < norec; j++)
                    cmb_Filter.Items.Add("Tuning " + dbn.Tables[0].Rows[j][0].ToString());//add items

            if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") chbx_PS3HAN.Enabled = true;
            if (c("dlcm_Debug") == "Yes")
            {
                rtxt_StatisticsOnReadDLCs.Visible = true;
                btn_Debug.Visible = true;
                txt_OggPath.Visible = true;
                txt_OggPreviewPath.Visible = true;
                txt_OldPath.Visible = true;
                txt_Lyrics.Visible = true;
                txt_AlbumArtPath.Visible = true;
                btn_AddSections.Enabled = true;
                txt_Lyrics_Hash.Visible = true;
                txt_Art_Hash.Visible = true;
                txt_Preview_Hash.Visible = true;
                txt_AudioPath.Visible = true;
                txt_AudioPreviewPath.Visible = true;
                btn_PKGLinker.Visible = true;
                btn_TrueRepacker.Visible = true;
                btn_PKGSigner.Visible = true;
                btn_CompactDB.Visible = true;
                btn_TotalCommander.Visible = true;
                btn_SongOnFire.Visible = true;
                btn_BRM.Visible = true;
                btn_SearchLyrics.Visible = true;
                btn_RockBand.Visible = true;
                btn_WinMerge.Visible = true;
                btn_UltraStarCreator.Visible = true;
            }
            tst = "Stop Groups load and some config/debug settings... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (c("general_defaultauthor") == "" || c("general_defaultauthor") == "Custom Song Creator") ConfigRepository.Instance()["general_defaultauthor"] = "catara";

            //move library if the case
            if (txt_ID.Text != "" && txt_ID.Text != null)
            {
                var tmpp = databox.Rows[0].Cells["Folder_Name"].Value.ToString();
                var OLD_Path = tmpp.Substring(0, tmpp.IndexOf("\\0_data"));
                var NEW_Path = c("dlcm_TempPath");
                if (!Directory.Exists(OLD_Path) || (tmpp.ToLower().IndexOf(NEW_Path.ToLower()) == -1) && Directory.Exists(NEW_Path))
                {
                    var cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "AudioPath=REPLACE(AudioPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
                            "audioPreviewPath=REPLACE(audioPreviewPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
                            "Folder_Name=REPLACE(Folder_Name, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "OggPath = REPLACE(OggPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "oggPreviewPath = REPLACE(oggPreviewPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";";

                    DialogResult result1 = MessageBox.Show("DB Repository has been moved from " + OLD_Path + "\n\n to " + c("dlcm_TempPath") + tmpp + "\n\n-" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmd, cnb);
                        tst = "Main table has been updated ... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet ds = new DataSet();
                        cmd = "UPDATE Arrangements SET JSONFilePath=REPLACE(JSONFilePath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "XMLFilePath=REPLACE(XMLFilePath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";";
                        ds = UpdateDB("Arrangements", cmd, cnb);
                        tst = "Arrangements table has been updated... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet dfs = new DataSet();
                        cmd = "UPDATE Pack_AuditTrail SET PackPath=REPLACE(PackPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";"; dfs = UpdateDB("Pack_AuditTrail", cmd, cnb);
                        tst = "Pack_AuditTrail table has been updated ... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet dhs = new DataSet();
                        cmd = "UPDATE Standardization SET SpotifyAlbumPath=REPLACE(SpotifyAlbumPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";"; dhs = UpdateDB("Standardization", cmd, cnb);
                        tst = "Standardization table has been updated ... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        MessageBox.Show("Main,Arrangements,Pack_AutiTrail and Standardization tables have been updated: " + c("dlcm_TempPath") + tmpp, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Issues with Temp Folder and DB Reporsitory");
                        return;
                    }
                }
            }

            //Update_Selected();
            ListSettings();
            SaveOK = true;
            if (AfterImport)
            {
                cmb_Filter.Text = "Imported Last"; AfterImport = false;
            }
            tst = "Stop cheking if the repository of decompressed songs has been moving... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_OpenDB_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(@c("dlcm_DBFolder"));
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + c("dlcm_DBFolder"));
            }
        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                ArrangementsDB frm = new ArrangementsDB(c("dlcm_DBFolder"), txt_ID.Text, chbx_BassDD.Checked, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                TonesDB frm = new TonesDB(c("dlcm_DBFolder"), txt_ID.Text, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            cmb_Filter.Text = "";
            GoTocounter = 0;
            if (!SearchON)
            {
                if (chbx_AutoSave.Checked) SaveRecord();

                btn_GoTo.Enabled = true;
                btn_ChangeCover.Enabled = false;
                btn_Save.Enabled = false;

                txt_Title.Text = "";
                txt_Artist.Text = "";
                txt_Album.Text = "";
                txt_OldPath.Text = "";
                txt_ID.Text = "";
                txt_Author.Text = "";
                txt_Description.Text = "";
                txt_Live_Details.Text = "";
                txt_AlbumSort.Text = "";
                txt_Artist_Sort.Enabled = false;
                txt_Title_Sort.Enabled = false;
                txt_Artist_ShortName.Enabled = false;
                txt_Album_ShortName.Enabled = false;
                txt_Album_Year.Enabled = false;
                txt_DLC_ID.Enabled = false;
                txt_APP_ID.Enabled = false;
                txt_Platform.Enabled = false;
                txt_AlbumSort.Enabled = false;
                txt_Version.Enabled = false;
                //chbx_AllGroups.Enabled = false;
                chbx_Alternate.Enabled = false;
                txt_Alt_No.Enabled = false;
                bth_GetTrackNo.Enabled = false;
                txt_Track_No.Enabled = false;
                txt_Top10.Enabled = false;
                txt_MultiTrackType.Enabled = false;

                gbox_QualityChecks.Enabled = false;
                //txt_YouTube_Link.Enabled = false;
                //txt_Playthrough.Enabled = false;
                gbox_Pack.Enabled = false;
                gbox_Cover.Enabled = false;
                gbox_Groups.Enabled = false;
                gbox_Audio.Enabled = false;

                btn_OpenSongFolder.Enabled = false;
                btn_Copy_Orig.Enabled = false;
                btn_Arrangements.Enabled = false;
                btn_Tones.Enabled = false;
                btn_AddCoverFlags.Enabled = false;

                txt_OldPath.Visible = true;
                txt_OldPath.ReadOnly = false;
                txt_ID.ReadOnly = false;
                txt_ID.Enabled = true;

                SearchON = true;
            }
            else
               if (txt_Artist.Text != "" || txt_Title.Text != "" || txt_Album.Text != "" || txt_Description.Text != "" || txt_Live_Details.Text != "" || txt_OldPath.Text != "" || txt_Author.Text != "" || txt_ID.Text != "")
                try
                {
                    btn_GoTo.Enabled = false;
                    SearchCmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Title.Text != "" ? "Song_Title Like '%" + txt_Title.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Album.Text != "" ? "Album Like '%" + txt_Album.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Live_Details.Text != "" ? "Live_Details Like '%" + txt_Live_Details.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Description.Text != "" ? "Description Like '%" + txt_Description.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_OldPath.Text != "" ? "Original_FileName Like '%" + txt_OldPath.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_OldPath.Text != "" ? "Author Like '%" + txt_Author.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_ID.Text != "" ? "ID Like '%" + txt_ID.Text + "%'" : "");
                    SearchCmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";
                    SearchCmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                    SearchCmd = SearchCmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
                    SearchCmd = SearchCmd.Replace("WHERE AND", "WHERE ");
                    SearchCmd = SearchCmd.Replace("AND ORDER BY ", "ORDER BY ");

                    Populate(ref databox, ref Main);
                    databox.Refresh();
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message + "Can't run Search ! " + SearchCmd);
                }
            else MessageBox.Show("Add a search criteria");
            //Update_Selected();
        }

        private void btn_SearchReset_Click(object sender, EventArgs e)
        {
            cmb_Filter.Text = "0ALL";
            GoTocounter = 0;
            btn_GoTo.Enabled = false;
            chbx_FilterNot.Checked = false;
            chbx_FilterCompound.Checked = false;
            if (chbx_AutoSave.Checked) SaveRecord();
            //Populate(ref databox, ref Main);
            //databox.Refresh();
            cmb_Filter.Text = "";
            SearchON = false;
            //Update_Selected();
        }

        public void ChangeRow()
        {
            var tst = "Start change row... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (chbx_AutoSave.Checked) SaveRecord();

            txt_OldPath.ReadOnly = true;
            txt_ID.ReadOnly = true;

            int i;
            if (databox.SelectedCells.Count > 0 && databox.Rows[databox.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
                if (SearchON) SearchON = false;
                UpdateGroups();

                i = databox.SelectedCells[0].RowIndex;
                MainDBfields[] SongRecord = new MainDBfields[10000];
                btn_GoTo.Enabled = false;
                try
                {
                    SongRecord = GetRecord_s("SELECT * FROM Main WHERE ID=" + databox.Rows[i].Cells["ID"].Value.ToString(), cnb);
                    if (SongRecord[0].ID != null)
                    {
                        txt_ID.Text = SongRecord[0].ID;
                        txt_Title.Text = SongRecord[0].Song_Title;
                        txt_Title_Sort.Text = SongRecord[0].Song_Title_Sort;
                        txt_Album.Text = SongRecord[0].Album;
                        txt_AlbumSort.Text = SongRecord[0].Album_Sort;
                        txt_Artist.Text = SongRecord[0].Artist;
                        txt_Artist_Sort.Text = SongRecord[0].Artist_Sort;
                        txt_Album_Year.Text = SongRecord[0].Album_Year;
                        txt_AverageTempo.Text = SongRecord[0].AverageTempo;
                        txt_Volume.Text = (SongRecord[0].Volume);
                        txt_Preview_Volume.Text = (SongRecord[0].Preview_Volume);
                        txt_AlbumArtPath.Text = SongRecord[0].AlbumArtPath;
                        txt_Track_No.Text = SongRecord[0].Track_No;
                        txt_Author.Text = SongRecord[0].Author;
                        txt_Version.Text = SongRecord[0].Version;
                        txt_DLC_ID.Text = SongRecord[0].DLC_Name;
                        txt_APP_ID.Text = SongRecord[0].DLC_AppID;
                        txt_MultiTrackType.Text = SongRecord[0].MultiTrack_Version;
                        txt_Alt_No.Text = SongRecord[0].Alternate_Version_No;
                        txt_Tuning.Text = SongRecord[0].Tunning;
                        txt_BassPicking.Text = SongRecord[0].Bass_Picking;
                        txt_Live_Details.Text = SongRecord[0].Live_Details;
                        txt_Rating.Text = SongRecord[0].Rating;
                        txt_Top10.Text = SongRecord[0].Top10;
                        txt_Description.Text = SongRecord[0].Description;
                        txt_Platform.Text = SongRecord[0].Platform;
                        chbx_LyricsLanguage.Text = SongRecord[0].LyricsLanguage;
                        //if () txt_PreviewStart.Value = Convert.ToDateTime("00:00:00");else 
                        if (SongRecord[0].PreviewTime == "00:00" || SongRecord[0].PreviewTime == "00:00:00" || SongRecord[0].PreviewTime == "")
                            txt_PreviewStart.Value = Convert.ToDateTime("00:" + c("dlcm_PreviewStart"));
                        else txt_PreviewStart.Value = Convert.ToDateTime("00:" + (SongRecord[0].PreviewTime is null ? "00:00" : SongRecord[0].PreviewTime));
                        if (SongRecord[0].PreviewLenght == "") txt_PreviewEnd.Value = 30;
                        else txt_PreviewEnd.Text = SongRecord[0].PreviewLenght;
                        txt_YouTube_Link.Text = SongRecord[0].YouTube_Link;
                        btn_Youtube.Enabled = SongRecord[0].YouTube_Link == "" ? false : true;

                        if (txt_Playthrough.Items.Count > 0) for (int k = txt_Playthrough.Items.Count - 1; k >= 0; --k) txt_Playthrough.Items.RemoveAt(k);//remove items
                        txt_Playthrough.Text = "";
                        var scmd = "SELECT PlaythroughYBLink, RouteMask, Bonus, Arrangement_Name FROM Arrangements WHERE CDLC_ID=" + SongRecord[0].ID + ";";
                        DataSet dnss = new DataSet(); dnss = SelectFromDB("Arrangements", scmd, "", cnb);
                        var norecs = dnss.Tables.Count == 0 ? 0 : dnss.Tables[0].Rows.Count;
                        if (norecs > 0) for (int j = 0; j < norecs; j++)
                                if (dnss.Tables[0].Rows[j][0].ToString() != "" && dnss.Tables[0].Rows[j][0].ToString() != null)
                                {
                                    var v = dnss.Tables[0].Rows[j][3].ToString() + "_" + dnss.Tables[0].Rows[j][1].ToString() + "_"
                                    + (dnss.Tables[0].Rows[j][2].ToString().ToLower() == "Yes".ToLower() ? "_B_" : "")
                                    + "_" + dnss.Tables[0].Rows[j][0].ToString();
                                    txt_Playthrough.Items.Add(v);//add items
                                    txt_Playthrough.Text = v;
                                }
                        if (!(txt_Playthrough.Text == "" || txt_Playthrough.Text is null))
                        {
                            txt_Playthrough.Items.Add(SongRecord[0].Youtube_Playthrough);//add items
                            txt_Playthrough.Text = SongRecord[0].Youtube_Playthrough;
                        }

                        //cmb_Tracks
                        ListTracks(SongRecord[0].Duplicate_Of, SongRecord[0].ID, SongRecord[0].Song_Lenght);
                        txt_DuplicateOf.Text = SongRecord[0].Duplicate_Of.ToString();

                        btn_Playthrough.Enabled = (txt_Playthrough.Text == "" || txt_Playthrough.Text is null) ? false : true;
                        txt_CustomsForge_Link.Text = SongRecord[0].CustomsForge_Link;
                        btn_CustomForge_Link.Enabled = SongRecord[0].CustomsForge_Link == "" ? false : true;
                        txt_CustomsForge_Like.Text = SongRecord[0].CustomsForge_Like;
                        txt_CustomsForge_ReleaseNotes.Text = SongRecord[0].CustomsForge_ReleaseNotes;

                        txt_OggPath.Text = SongRecord[0].OggPath;
                        txt_OggPreviewPath.Text = SongRecord[0].oggPreviewPath;
                        txt_OldPath.Text = SongRecord[0].Original_FileName;

                        txt_Artist_ShortName.Text = SongRecord[0].Artist_ShortName;
                        txt_Album_ShortName.Text = SongRecord[0].Album_ShortName;
                        txt_RemotePath.Text = SongRecord[0].Remote_Path;
                        txt_FilesMissingIssues.Text = SongRecord[0].FilesMissingIssues;
                        tst = "Stop populating stnadard fields... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        if (SongRecord[0].UseInternalDDRemovalLogic == "Yes") chbx_UseInternalDDRemovalLogic.Checked = true;
                        else chbx_UseInternalDDRemovalLogic.Checked = false;
                        if (SongRecord[0].Is_Original == "Yes") { chbx_Original.Checked = true; chbx_Original.ForeColor = btn_Debug.ForeColor; }
                        else { chbx_Original.Checked = false; chbx_Original.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Is_Beta == "Yes") chbx_Beta.Checked = true;
                        else chbx_Beta.Checked = false;
                        if (SongRecord[0].Is_Instrumental == "Yes") chbx_IsInstrumental.Checked = true;
                        else chbx_IsInstrumental.Checked = false;
                        if (SongRecord[0].Is_Single == "Yes") chbx_IsSingle.Checked = true;
                        else chbx_IsSingle.Checked = false;
                        if (SongRecord[0].Is_Soundtrack == "Yes") chbx_IsSoundtrack.Checked = true;
                        else chbx_IsSoundtrack.Checked = false;
                        if (SongRecord[0].Is_EP == "Yes") chbx_IsEP.Checked = true;
                        else chbx_IsEP.Checked = false;
                        if (SongRecord[0].Is_Uncensored == "Yes") chbx_IsUncensored.Checked = true;
                        else chbx_IsUncensored.Checked = false;
                        if (SongRecord[0].ImprovedWithDM == "Yes") chbx_ImprovedWithDM.Checked = true;
                        else chbx_ImprovedWithDM.Checked = false;
                        if (SongRecord[0].Is_FullAlbum == "Yes") chbx_FullAlbum.Checked = true;
                        else chbx_FullAlbum.Checked = false;
                        if (SongRecord[0].PitchShiftableEsOrDd == "Yes") chbx_PitchShift.Checked = true;
                        else chbx_PitchShift.Checked = false;
                        if (SongRecord[0].IntheWorks == "Yes") chbx_IntheWorks.Checked = true;
                        else chbx_IntheWorks.Checked = false;
                        if (SongRecord[0].Is_Alternate == "Yes") { chbx_Alternate.Checked = true; txt_Alt_No.Enabled = true; }
                        else { chbx_Alternate.Checked = false; txt_Alt_No.Enabled = false; }
                        if (SongRecord[0].Is_Multitrack == "Yes") { chbx_MultiTrack.Checked = true; txt_MultiTrackType.Enabled = true; }
                        else { chbx_MultiTrack.Checked = false; txt_MultiTrackType.Enabled = false; }
                        if (SongRecord[0].Is_Broken == "Yes") chbx_Broken.Checked = true;
                        else chbx_Broken.Checked = false;
                        if (SongRecord[0].Has_Bass == "Yes") { chbx_Bass.Checked = true; chbx_Bass.ForeColor = System.Drawing.Color.Green; }/*chbx_Bass.Font = new Font(chbx_Bass.Font.Name, 7, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Bass.Checked = false; chbx_Bass.Font = new Font(chbx_Bass.Font.Name, 6, FontStyle.Regular); chbx_Bass.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Lead == "Yes") { chbx_Lead.Checked = true; chbx_Lead.ForeColor = btn_Debug.ForeColor; }/*chbx_Lead.Font = new Font(chbx_Lead.Font.Name, 7, FontStyle.Bold);  | FontStyle.Underline*/
                        else { chbx_Lead.Checked = false; chbx_Lead.Font = new Font(chbx_Lead.Font.Name, 6, FontStyle.Regular); chbx_Lead.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Combo == "Yes") { chbx_Combo.Checked = true; chbx_Combo.ForeColor = System.Drawing.Color.Green; }/*chbx_Combo.Font = new Font(chbx_Combo.Font.Name, 7, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Combo.Checked = false; chbx_Combo.Font = new Font(chbx_Combo.Font.Name, 6, FontStyle.Regular); chbx_Combo.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Rhythm == "Yes") { chbx_Rhythm.Checked = true; chbx_Rhythm.ForeColor = System.Drawing.Color.Green; }/*chbx_Rhythm.Font = new Font(chbx_Rhythm.Font.Name, 7, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Rhythm.Checked = false; chbx_Rhythm.Font = new Font(chbx_Rhythm.Font.Name, 6, FontStyle.Regular); chbx_Rhythm.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Vocals == "Yes") { chbx_Lyrics.Checked = true; btn_ShowLyrics.Enabled = true; btn_AddInstrumental.Enabled = false; chbx_Lyrics.Font = new Font(chbx_Lyrics.Font.Name, 6, FontStyle.Regular); chbx_Lyrics.ForeColor = System.Drawing.Color.Green; }/*bth_ShiftVocalNotes.Enabled = true; num_Lyrics.Enabled = true;btn_CreateLyrics.Enabled = false;*/
                        else { chbx_Lyrics.Checked = false; btn_CreateLyrics.Enabled = true; btn_ShowLyrics.Enabled = false; btn_AddInstrumental.Enabled = true; chbx_Lyrics.Font = new Font(chbx_Lyrics.Font.Name, 6, FontStyle.Regular); chbx_Lyrics.ForeColor = btn_Duplicate.ForeColor; }/*bth_ShiftVocalNotes.Enabled = false; num_Lyrics.Enabled = false;*/
                        if (SongRecord[0].Has_Sections != null)
                        {
                            if (SongRecord[0].Has_Sections.Length > 2) chbx_Sections.Checked = true;
                            else chbx_Sections.Checked = false;
                        }
                        else chbx_Sections.Checked = false;
                        if (SongRecord[0].Has_Cover == "Yes") chbx_Cover.Checked = true;
                        else chbx_Cover.Checked = false;
                        if (SongRecord[0].Has_Preview == "Yes") { chbx_Preview.Checked = true; btn_PlayPreview.Enabled = true; }
                        else { chbx_Preview.Checked = false; btn_PlayPreview.Enabled = false; }
                        if (SongRecord[0].Has_DD == "Yes") { txt_AddDD.Enabled = false; chbx_DD.Checked = true; btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; }
                        else { chbx_DD.Checked = false; btn_AddDD.Enabled = true; btn_RemoveDD.Enabled = false; }
                        if (SongRecord[0].Keep_BassDD == "Yes") { chbx_KeepBassDD.Checked = true; }
                        else { chbx_KeepBassDD.Checked = false; }
                        if (SongRecord[0].Keep_DD == "Yes") { chbx_KeepDD.Checked = true; }
                        else { chbx_KeepDD.Checked = false; }
                        if (SongRecord[0].Selected == "Yes") chbx_Selected.Checked = true;
                        else chbx_Selected.Checked = false;
                        if (SongRecord[0].Has_Author == "Yes") chbx_Author.Checked = true;
                        else chbx_Author.Checked = false;
                        if (SongRecord[0].Has_BassDD == "Yes") { chbx_BassDD.Checked = true; btn_RemoveBassDD.Enabled = true; chbx_KeepBassDD.Enabled = true; chbx_RemoveBassDD.Enabled = true; }
                        else { chbx_BassDD.Checked = false; btn_RemoveBassDD.Enabled = false; chbx_RemoveBassDD.Enabled = false; }
                        if (SongRecord[0].Has_Bonus_Arrangement == "Yes") chbx_Bonus.Checked = true;
                        else chbx_Bonus.Checked = false;
                        chbx_Avail_Old.Checked = false;
                        if (float.Parse(SongRecord[0].audioBitrate, NumberStyles.Float, CultureInfo.CurrentCulture) > c("dlcm_MaxBitRate").ToInt32()
                            || float.Parse(SongRecord[0].audioSampleRate, NumberStyles.Float, CultureInfo.CurrentCulture) > c("dlcm_MaxSampleRate").ToInt32()
                            || SongRecord[0].Has_Preview != "Yes" || SongRecord[0].Has_Track_No != "Yes"
                            || SongRecord[0].Track_No == "-1" || SongRecord[0].Track_No == "0" || txt_Playthrough.Items.Count == 0 || txt_YouTube_Link.Text == "")
                        { btn_Fix_AudioIssues.Enabled = true; }
                        else { btn_Fix_AudioIssues.Enabled = false; }

                        if (SongRecord[0].Available_Old == "Yes") { chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true; }
                        else { chbx_Avail_Old.Checked = false; btn_OldFolder.Enabled = false; ; btn_CopyOld.Enabled = false; chbx_CopyOld.Enabled = false; }
                        chbx_Avail_Duplicate.Checked = false;
                        if (SongRecord[0].Available_Duplicate == "Yes") { chbx_Avail_Duplicate.Checked = true; btn_DuplicateFolder.Enabled = true; }
                        else { chbx_Avail_Duplicate.Checked = false; btn_DuplicateFolder.Enabled = false; }
                        if (SongRecord[0].Has_Been_Corrected == "Yes") chbx_Has_Been_Corrected.Checked = true;
                        else chbx_Has_Been_Corrected.Checked = false;

                        if (SongRecord[0].Has_Had_Audio_Changed == "Yes") chbx_AudioChanged.Checked = true;
                        else chbx_AudioChanged.Checked = false;
                        if (SongRecord[0].Has_Had_Lyrics_Changed == "Yes") chbx_LyricsChanged.Checked = true;
                        else chbx_LyricsChanged.Checked = false;

                        chbx_Originals_Available.Checked = false;
                        if (chbx_Format_Originals.Items.Count > 0) for (int k = chbx_Format_Originals.Items.Count - 1; k >= 0; --k) chbx_Format_Originals.Items.RemoveAt(k);//remove items

                        if (SongRecord[0].Has_Other_Officials == "Yes")
                        {
                            chbx_Originals_Available.Checked = true;
                            var cmd = "SELECT DISTINCT Platform FROM Pack_AuditTrail WHERE Official=\"Yes\" AND DLC_ID=" + SongRecord[0].ID + " AND PackPath not like \"%0_old%\"; ";
                            DataSet dns = new DataSet(); dns = SelectFromDB("Main", cmd, "", cnb);
                            var norec = dns.Tables[0].Rows.Count;
                            if (norec > 0) for (int j = 0; j < norec; j++)
                                {
                                    chbx_Format_Originals.Items.Add(dns.Tables[0].Rows[j][0].ToString());//add items
                                    chbx_Format_Originals.Text = dns.Tables[0].Rows[j][0].ToString();
                                }
                        }
                        else chbx_Originals_Available.Checked = false;

                        txt_Spotify_Song_ID.Text = SongRecord[0].Spotify_Album_ID;
                        txt_Spotify_Artist_ID.Text = SongRecord[0].Spotify_Artist_ID;
                        txt_Spotify_Album_ID.Text = SongRecord[0].Spotify_Album_ID;
                        txt_Spotify_Album_URL.Text = SongRecord[0].Spotify_Album_URL;

                        // Set up the ToolTip text for the Button and Checkbox.
                        toolTip2.SetToolTip(btn_PlayAudio, SongRecord[0].Audio_OrigHash);
                        toolTip3.SetToolTip(btn_PlayPreview, SongRecord[0].Audio_OrigPreviewHash);
                        toolTip4.SetToolTip(gbox_Cover, SongRecord[0].audioBitrate + " - " + SongRecord[0].audioSampleRate);
                        toolTip5.SetToolTip(txt_PreviewStart, SongRecord[0].Song_Lenght);

                        txt_Preview_Hash.Text = SongRecord[0].AudioPreview_Hash;
                        txt_Art_Hash.Text = SongRecord[0].AlbumArt_Hash;
                        txt_AudioPath.Text = SongRecord[0].AudioPath;
                        txt_AudioPreviewPath.Text = SongRecord[0].audioPreviewPath;

                        if (SongRecord[0].Is_Live == "Yes") { chbx_IsLive.Checked = true; txt_Live_Details.Enabled = true; }
                        else { chbx_IsLive.Checked = false; txt_Live_Details.Enabled = true; }
                        if (SongRecord[0].Is_Acoustic == "Yes") { chbx_IsAcoustic.Checked = true; txt_Live_Details.Enabled = true; }
                        else { chbx_IsAcoustic.Checked = false; txt_Live_Details.Enabled = true; }
                        tst = "Stop populating multivalue fields... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        //Lyrics
                        DataSet dsr = new DataSet(); dsr = SelectFromDB("Arrangements", "SELECT XMLFilePath FROM Arrangements WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";", "", cnb);
                        var rec = dsr.Tables.Count == 0 ? 0 : dsr.Tables[0].Rows.Count;
                        if (rec > 0) { txt_Lyrics.Text = dsr.Tables[0].Rows[0].ItemArray[0].ToString(); btn_ChangeLyrics.Text = "Change Lyrics"; }
                        else { txt_Lyrics.Text = ""; btn_ChangeLyrics.Text = "Add Lyrics"; }

                        //Audio Autoplay
                        if (chbx_AutoPlay.Checked && chbx_Preview.Checked)
                        {
                            if (btn_PlayPreview.Text != "Play Preview") btn_PlayPreview.Text = "Play Preview";
                            else btn_PlayPreview.Text = "STOP Preview";
                            AudioBackgroundPlay(txt_OggPreviewPath.Text, true, AppWD);
                            AudioBackgroundPlay(txt_OggPreviewPath.Text, false, AppWD);
                        }
                        if (SongRecord[0].Has_Rhythm != "Yes" && SongRecord[0].Has_Lead == "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "L->R"; }
                        else if (SongRecord[0].Has_Rhythm == "Yes" && SongRecord[0].Has_Lead != "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "R->L"; }
                        else chbx_DupliGTrack.Enabled = false;
                        gbox_Cover.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                        btn_Delete.Enabled = true;
                        btn_Duplicate.Enabled = true;
                        btn_Package.Enabled = true;
                        btn_ChangeCover.Enabled = true;
                        btn_AddPreview.Enabled = true;
                        btn_SelectPreview.Enabled = true;
                        btn_PlayAudio.Enabled = true;
                        btn_Save.Enabled = true;
                        txt_Artist_Sort.Enabled = true;
                        txt_Album.Enabled = true;
                        txt_Title_Sort.Enabled = true;

                        txt_Artist_ShortName.Enabled = true;
                        txt_Album_ShortName.Enabled = true;
                        txt_Album_Year.Enabled = true;
                        txt_DLC_ID.Enabled = true;
                        txt_APP_ID.Enabled = true;
                        txt_Platform.Enabled = true;
                        txt_Author.Enabled = true;
                        txt_Version.Enabled = true;
                        chbx_AllGroups.Enabled = true;
                        chbx_Alternate.Enabled = true;
                        txt_Alt_No.Enabled = true;
                        bth_GetTrackNo.Enabled = true;
                        txt_Track_No.Enabled = true;
                        txt_Top10.Enabled = true;
                        //chbx_IsInstrumental.Enabled = true;
                        //chbx_IsInstrumental.Controls. = true;


                        txt_OldPath.Enabled = true;
                        txt_AlbumSort.Enabled = true;
                        chbx_Replace.Enabled = false;
                        chbx_Last_Packed.Enabled = false;
                        txt_CoundofPacked.Enabled = false;
                        cmb_Packed.Enabled = false;

                        //txt_YouTube_Link.Enabled = true;
                        //txt_Playthrough.Enabled = true;
                        gbox_QualityChecks.Enabled = true;
                        gbox_Pack.Enabled = true;
                        gbox_Cover.Enabled = true;
                        gbox_Groups.Enabled = true;
                        gbox_Audio.Enabled = true;

                        txt_MultiTrackType.Enabled = true;
                        btn_OpenSongFolder.Enabled = true;
                        btn_Copy_Orig.Enabled = true;
                        btn_Arrangements.Enabled = true;
                        btn_Tones.Enabled = true;
                        btn_AddCoverFlags.Enabled = true;

                        if ((SongRecord[0].Remote_Path) != "") chbx_Replace.Enabled = true;//if (File.Exists(txt_FTPPath.Text + "\\" + DataViewGrid.Rows[i].Cells["Remote_Path))

                        //Populate details on Arrangements
                        var sel = "SELECT Arrangement_Name, Start_Time, Bonus, Part, Has_Sections, MaxDifficulty, ToneBase, ToneA, ToneB, ToneC, ToneD, Tunning, TuningPitch, ConversionDateTime," +
                            " Rating, ScrollSpeed, Comments, XMLFilePath FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + " ORDER BY ID DESC;";
                        DataSet ddr = new DataSet(); ddr = SelectFromDB("Arrangements", sel, "", cnb);
                        rec = ddr.Tables.Count > 0 ? ddr.Tables[0].Rows.Count : 0;

                        toolTip11.RemoveAll(); toolTip12.RemoveAll(); toolTip13.RemoveAll(); toolTip14.RemoveAll(); toolTip15.RemoveAll();
                        toolTip2.RemoveAll(); toolTip3.RemoveAll(); toolTip4.RemoveAll(); toolTip5.RemoveAll();
                        if (rec > 0)
                        {
                            var tzt = "";
                            for (var j = 0; j <= rec - 1; j++)
                            {
                                var Arrangement_Name = ddr.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Start_Time = "Starting after " + ddr.Tables[0].Rows[j].ItemArray[1].ToString() + " seconds";
                                var Bonus = ddr.Tables[0].Rows[j].ItemArray[2].ToString().ToLower() == "true" ? ", with Bonus" : "";
                                var Part = ddr.Tables[0].Rows[j].ItemArray[3].ToString().ToLower() == "yes" ? ", with Part" : "";
                                var HasSections = ddr.Tables[0].Rows[j].ItemArray[4].ToString().ToLower().IndexOf("yes") >= 0 ? " with " + ddr.Tables[0].Rows[j].ItemArray[4].ToString().Replace("Yes", "") + " Sections" : "";
                                var MaxDifficulty = ddr.Tables[0].Rows[j].ItemArray[5].ToString().ToLower() != "0" ? ", with Diffculty " + ddr.Tables[0].Rows[j].ItemArray[5].ToString().ToLower() : "";
                                var Tones = ", with tones: " + ddr.Tables[0].Rows[j].ItemArray[6] + ", " + ddr.Tables[0].Rows[j].ItemArray[7] + ", " + ddr.Tables[0].Rows[j].ItemArray[8] + ", " + ddr.Tables[0].Rows[j].ItemArray[9] + ", " + ddr.Tables[0].Rows[j].ItemArray[10];
                                var Tunings = ddr.Tables[0].Rows[j].ItemArray[11] != null ? ", " + ddr.Tables[0].Rows[j].ItemArray[11] : "";
                                var TuningPitch = ddr.Tables[0].Rows[j].ItemArray[12].ToString() != null ? (", pitch: " + ddr.Tables[0].Rows[j].ItemArray[12]) : "";
                                var lastConversion = ddr.Tables[0].Rows[j].ItemArray[13].ToString() != "" ? ", lastconv: " + ddr.Tables[0].Rows[j].ItemArray[13] : "";
                                var Rating = ddr.Tables[0].Rows[j].ItemArray[14] != null && ddr.Tables[0].Rows[j].ItemArray[14].ToString() != "" ? (", rating " + ddr.Tables[0].Rows[j].ItemArray[14].ToString()) : "";
                                var ScroolSpeed = ddr.Tables[0].Rows[j].ItemArray[15].ToString() != null ? (", scroolspeed: " + ddr.Tables[0].Rows[j].ItemArray[15]) : "";
                                var Comment = ddr.Tables[0].Rows[j].ItemArray[16] != null && ddr.Tables[0].Rows[j].ItemArray[16].ToString() != "" ? (", comment: " + ddr.Tables[0].Rows[j].ItemArray[16].ToString()) : "";
                                var XMLFilePath = ", " + ddr.Tables[0].Rows[j].ItemArray[17].ToString();

                                // Set up the ToolTip text for the Button and Checkbox.
                                var txtt = Start_Time + Bonus + Part + HasSections + MaxDifficulty + Tones + Tunings + TuningPitch + lastConversion + Rating + ScroolSpeed + Comment;
                                txtt = txtt.Replace(", , ", ", ").Replace(", , ", ", ").Replace(", , ", ", ").Replace(", , ", ", ");
                                if (Arrangement_Name == "0") toolTip11.SetToolTip(chbx_Lead, toolTip11.GetToolTip(chbx_Lead) + txtt);
                                else if (Arrangement_Name == "1") toolTip12.SetToolTip(chbx_Rhythm, toolTip12.GetToolTip(chbx_Rhythm) + txtt);
                                else if (Arrangement_Name == "3") toolTip13.SetToolTip(chbx_Bass, toolTip13.GetToolTip(chbx_Bass) + txtt);
                                else if (Arrangement_Name == "4") toolTip14.SetToolTip(chbx_Lyrics, toolTip14.GetToolTip(chbx_Lyrics) + txtt);
                                else if (Arrangement_Name == "5") toolTip15.SetToolTip(chbx_Combo, toolTip15.GetToolTip(chbx_Combo) + txtt);
                                var instr = Arrangement_Name == "0" ? "Lead" : (Arrangement_Name == "1" ? "Rhythm" : (Arrangement_Name == "3" ? "Bass" : (Arrangement_Name == "5" ? "Combo" : (""))));
                                var bon = ddr.Tables[0].Rows[j].ItemArray[2].ToString().ToLower() == "true" ? "b" : "";
                                toolTip16.SetToolTip(txt_Tuning, toolTip16.GetToolTip(txt_Tuning) + ", " + instr + ": " + (bon == "" ? "" : bon + "-") + Tunings);
                                tzt += GetDropTunningInstr(ddr.Tables[0].Rows[j].ItemArray[11].ToString());
                                tzt += tzt != "" ? instr + tzt+"\n" : "";
                            }
                            if (tzt!="") toolTip6.SetToolTip(chbx_PitchShift, tzt);
                            else toolTip6.SetToolTip(chbx_PitchShift, "Not in any E standard or Drop D compatible drop(s) tunings");
                        }
                        //Populate all Packed versions of the song
                        var tht = "SELECT PackPath+'\\'+FileName FROM Pack_AuditTrail WHERE CDLC_ID=" + txt_ID.Text + " ORDER BY ID DESC;";
                        DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tht, "", cnb);
                        rec = dvr.Tables[0].Rows.Count;
                        txt_CoundofPacked.Value = rec;
                        if (rec > 0)
                        {
                            chbx_Last_Packed.Enabled = true;
                            txt_CoundofPacked.Enabled = true;
                            cmb_Packed.Enabled = true;
                            if (cmb_Packed.Items.Count > 0)//remove items
                            {
                                cmb_Packed.DataSource = null;
                                for (int k = cmb_Packed.Items.Count - 1; k >= 0; --k) cmb_Packed.Items.RemoveAt(k);
                            }

                            cmb_Packed.Items.Add("None");
                            for (int j = 0; j < rec; j++)//&add items
                                cmb_Packed.Items.Add(dvr.Tables[0].Rows[j][0].ToString());
                            cmb_Packed.Text = "None";
                            tst = "Stop adding duplicates and olds... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        }
                        else chbx_Last_Packed.Enabled = false;
                    }
                }
                catch (Exception ex) { tst = "Error at change row... " + ex; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
            }
            //else MessageBox.Show("No Records/Songs (Imported/Found)");
            Update_Selected();
            tst = "Stop Change Row... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        public void ListTracks(string Duplicate_Of, string ID, string Song_Lenght)
        {
            if (cmb_Tracks.Items.Count > 0) for (int k = cmb_Tracks.Items.Count - 1; k >= 0; --k) cmb_Tracks.Items.RemoveAt(k);//remove items
            var scmd = "SELECT ID FROM Main WHERE ID=" + Duplicate_Of + " OR ID=" + ID + " OR Duplicate_Of=\"" + (Duplicate_Of == "0" ? "999999" : Duplicate_Of)
                + "\" OR Duplicate_Of=\"" + ID + "\";";
            DataSet dzs = new DataSet(); dzs = SelectFromDB("Main", scmd, "", cnb);
            var norecs = dzs.Tables.Count == 0 ? 0 : dzs.Tables[0].Rows.Count;
            var IDs = "0";
            for (var j = 0; j <= norecs - 1; j++) IDs += "," + dzs.Tables[0].Rows[j].ItemArray[0].ToString();

            scmd = "SELECT ID, XMLFileName, Start_Time, RouteMask, Bonus, ArrangementType, CDLC_ID FROM Arrangements WHERE CDLC_ID IN (" + IDs + ");";
            DataSet dnzs = new DataSet(); dnzs = SelectFromDB("Arrangements", scmd, "", cnb);
            norecs = dnzs.Tables.Count == 0 ? 0 : dnzs.Tables[0].Rows.Count;
            if (norecs > 0)
                for (int j = 0; j < norecs; j++)
                    if (dnzs.Tables[0].Rows[j][0].ToString() != "" && dnzs.Tables[0].Rows[j][0].ToString() != null)
                    {
                        if (dnzs.Tables[0].Rows[j][5].ToString().IndexOf("ShowLight") >= 0) continue;
                        scmd = "SELECT Song_Lenght,Duplicate_Of FROM Main WHERE ID=" + dnzs.Tables[0].Rows[j][6].ToString();
                        DataSet djs = new DataSet(); djs = SelectFromDB("Main", scmd, "", cnb);

                        var v = "DLC ID: " + dnzs.Tables[0].Rows[j][6].ToString() + "_" + dnzs.Tables[0].Rows[j][4].ToString() + "_Start Time: "
                        + dnzs.Tables[0].Rows[j][2].ToString() + "s_" +
                        (dnzs.Tables[0].Rows[j][5].ToString() == "Vocal" ? dnzs.Tables[0].Rows[j][5].ToString() : dnzs.Tables[0].Rows[j][3].ToString())
                        + "_" + (dnzs.Tables[0].Rows[j][4].ToString() == "True" ? "Bonus" : "NoBonus") + "_Duplicate Of: " + djs.Tables[0].Rows[0].ItemArray[1].ToString()
                        + "_Dupli/Curent Lenght: " + djs.Tables[0].Rows[0].ItemArray[0].ToString() + "-" + Song_Lenght + "s_ArrangementID=" + dnzs.Tables[0].Rows[j][0].ToString();
                        cmb_Tracks.Items.Add(v);//add items
                    }

            cmb_Tracks.Items.Add(""); cmb_Tracks.Text = "";
        }

        public void SaveRecord()
        {
            if (!SaveOK) return;
            var tst = "Start Savin... " + txt_Artist.Text + " - " + txt_Title.Text; ; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            DataSet dis = new DataSet();

            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                int i = databox.SelectedCells[0].RowIndex;

                tst = "Stop saving fields... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //Save Groups
                if (GroupChanged)
                {
                    //identify all already selected Groups as to not to overitte their
                    var sel = "SELECT CDLC_ID, Groups, ID FROM Groups WHERE Type =\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                    DataSet grp = new DataSet(); grp = SelectFromDB("Groups", sel, "", cnb);
                    var noOfRecs = grp.Tables.Count == 0 ? 0 : grp.Tables[0].Rows.Count;
                    var grpsel = "("; var grpdel = "("; var found = false;
                    for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                    {
                        found = false;
                        for (var k = 0; k < noOfRecs; k++)
                        {
                            if (chbx_AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == chbx_AllGroups.Items[j].ToString())
                            {
                                grpsel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                                found = true;
                                break;
                            }
                            if (!chbx_AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == chbx_AllGroups.Items[j].ToString())
                                grpdel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                        }

                        if (!found && chbx_AllGroups.GetItemChecked(j))
                        {
                            var insertcmdd = "CDLC_ID, Groups, Type, Date_Added";
                            var insertvalues = "\"" + txt_ID.Text + "\", \"" + chbx_AllGroups.Items[j].ToString() + "\", \"DLC\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"";
                            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0);
                        }
                    }
                    grpsel = grpsel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace("(,", "(") + ")";
                    grpdel = grpdel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace("(,", "(") + ")";

                    if (grpdel != "()")
                        DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND ID IN " + grpdel.Replace(",)", ")"), cnb);

                    tst = "Stop saving groups... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    GroupChanged = false;
                }

                var YBPlaythrough = "";
                if (txt_Playthrough.Items.Count > 0)
                    for (int j = 0; j < txt_Playthrough.Items.Count; j++)
                    {
                        var bonus = false;
                        var str = txt_Playthrough.Items[j].ToString();
                        if (str.Length <= 0) continue;
                        var no = str.Substring(0, 1);

                        if (str.IndexOf("_B_") > 0)
                        {
                            bonus = true; YBPlaythrough = str.Substring(str.IndexOf("_B_") + 3, str.Length - str.IndexOf("__") + 2);/* continue;*/
                        }
                        else if (char.IsNumber(no, 0)) { YBPlaythrough = str.Substring(str.IndexOf("__") + 2, str.Length - str.IndexOf("__") - 2 - 1); /*continue; */}
                        else
                        {
                            var updateMcmd = "Update Main Set Youtube_Playthrough = \"" + str + "\"" +
                        " WHERE ID=" + txt_ID.Text + ";";
                            DataSet dcr = new DataSet(); dcr = UpdateDB("Main", updateMcmd, cnb);
                        }
                        var updatecmd = "Update Arrangements Set PlayThroughYBLink = \"" + YBPlaythrough + "\"" +
                        " WHERE CDLC_ID=" + txt_ID.Text + " AND Arrangement_Name=\"" + no + "\"" + (bonus ? " Bonus=\"" + bonus + "\"" : "") + ";";
                        DataSet dxr = new DataSet(); dxr = UpdateDB("Arrangements", updatecmd, cnb);
                    }


                //Update Song / Main DB
                var connection = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
                var command = connection.CreateCommand();
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder")))
                {
                    command.CommandText = "UPDATE Main SET ";
                    command.CommandText += "Song_Title = @param1, ";
                    command.CommandText += "Song_Title_Sort = @param2, ";
                    command.CommandText += "Album = @param3, ";
                    command.CommandText += "Artist = @param4, ";
                    command.CommandText += "Artist_Sort = @param5, ";
                    command.CommandText += "Album_Year = @param6, ";
                    command.CommandText += "AverageTempo = @param7, ";
                    command.CommandText += "Volume = @param8, ";
                    command.CommandText += "Preview_Volume = @param9, ";
                    command.CommandText += "AlbumArtPath = @param10, ";
                    command.CommandText += "AudioPreviewPath = @param12, ";
                    command.CommandText += "Track_No = @param13, ";
                    command.CommandText += "Author = @param14, ";
                    command.CommandText += "Version = @param15, ";
                    command.CommandText += "DLC_Name = @param16, ";
                    command.CommandText += "DLC_AppID = @param17, ";
                    command.CommandText += "Is_Original = @param26, ";
                    command.CommandText += "Is_Beta = @param28, ";
                    command.CommandText += "Is_Alternate = @param29, ";
                    command.CommandText += "Is_Multitrack = @param30, ";
                    command.CommandText += "Is_Broken = @param31, ";
                    command.CommandText += "MultiTrack_Version = @param32, ";
                    command.CommandText += "Alternate_Version_No = @param33, ";
                    command.CommandText += "Has_Vocals = @param40, ";
                    command.CommandText += "Has_Sections = @param41, ";
                    command.CommandText += "Has_Cover = @param42, ";
                    command.CommandText += "Has_Preview = @param43, ";
                    command.CommandText += "Has_DD = @param45, ";
                    command.CommandText += "Tunning = @param47, ";
                    command.CommandText += "Bass_Picking = @param48, ";
                    //command.CommandText += "Tones = @param49, ";
                    //command.CommandText += "Groups = @param50, ";
                    command.CommandText += "Rating = @param51, ";
                    command.CommandText += "Description = @param52, ";
                    command.CommandText += "Has_Track_No = @param54, ";
                    command.CommandText += "PreviewTime = @param56, ";
                    command.CommandText += "PreviewLenght = @param57, ";
                    command.CommandText += "Youtube_Playthrough = @param58, ";
                    command.CommandText += "Keep_BassDD = @param64, ";
                    command.CommandText += "Keep_DD = @param65, ";
                    command.CommandText += "Selected = @param69, ";
                    command.CommandText += "YouTube_Link = @param70, ";
                    command.CommandText += "CustomsForge_Link = @param71, ";
                    command.CommandText += "CustomsForge_Like = @param72, ";
                    command.CommandText += "CustomsForge_ReleaseNotes = @param73, ";
                    command.CommandText += "Has_Author = @param76, ";
                    command.CommandText += "oggPath = @param77, ";
                    command.CommandText += "oggPreviewPath = @param78, ";
                    //command.CommandText += "AlbumArt_Hash = @param80, ";
                    command.CommandText += "AlbumArt_Hash = @param81, ";
                    command.CommandText += "AudioPreview_Hash = @param82, ";
                    command.CommandText += "Bass_Has_DD = @param83, ";
                    command.CommandText += "Has_Bonus_Arrangement = @param84, ";
                    command.CommandText += "Artist_ShortName = @param85, ";
                    command.CommandText += "Album_ShortName = @param86, ";
                    command.CommandText += "Is_Live = @param87, ";
                    command.CommandText += "Live_Details = @param88, ";
                    command.CommandText += "Remote_Path = @param89, ";
                    command.CommandText += "Is_Acoustic = @param90, ";
                    command.CommandText += "Top10 = @param91, ";
                    command.CommandText += "UseInternalDDRemovalLogic = @param92, ";
                    command.CommandText += "Is_Single = @param93, ";
                    command.CommandText += "Is_Soundtrack = @param94, ";
                    command.CommandText += "Is_Instrumental = @param95, ";
                    command.CommandText += "Is_EP = @param96, ";
                    command.CommandText += "Has_Had_Audio_Changed = @param97, ";
                    command.CommandText += "Has_Had_Lyrics_Changed = @param98, ";
                    command.CommandText += "Album_Sort = @param99, ";
                    command.CommandText += "Has_Been_Corrected = @param100, ";
                    command.CommandText += "Duplicate_Of = @param101, ";
                    command.CommandText += "Has_Lead = @param102, ";
                    command.CommandText += "Has_Rhythm = @param103, ";
                    command.CommandText += "Has_Bass = @param104, ";
                    command.CommandText += "Is_Uncensored = @param105, ";
                    command.CommandText += "IntheWorks = @param106, ";
                    command.CommandText += "LyricsLanguage = @param107, ";
                    command.CommandText += "ImprovedWithDM = @param108, ";
                    command.CommandText += "Is_FullAlbum = @param109 ";
                    command.CommandText += " WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", txt_Title.Text);// databox.Rows[i].Cells["Song_Title"].Value.ToString());
                    command.Parameters.AddWithValue("@param2", txt_Title_Sort.Text);// databox.Rows[i].Cells["Song_Title_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param3", txt_Album.Text);//databox.Rows[i].Cells["Album"].Value.ToString());
                    command.Parameters.AddWithValue("@param4", txt_Artist.Text);// databox.Rows[i].Cells["Artist"].Value.ToString());
                    command.Parameters.AddWithValue("@param5", txt_Artist_Sort.Text);//databox.Rows[i].Cells["Artist_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param6", txt_Album_Year.Text);// databox.Rows[i].Cells["Album_Year"].Value.ToString());
                    command.Parameters.AddWithValue("@param7", txt_AverageTempo.Text);// databox.Rows[i].Cells["AverageTempo"].Value.ToString());
                    command.Parameters.AddWithValue("@param8", txt_Volume.Text);// databox.Rows[i].Cells["Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param9", txt_Preview_Volume.Text);// databox.Rows[i].Cells["Preview_Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param10", txt_AlbumArtPath.Text);// databox.Rows[i].Cells["AlbumArtPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param12", txt_AudioPreviewPath.Text);// databox.Rows[i].Cells["AudioPreviewPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param13", txt_Track_No.Text.ToInt32().ToString("D2"));// databox.Rows[i].Cells["Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param14", txt_Author.Text);// databox.Rows[i].Cells["Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param15", txt_Version.Text);// databox.Rows[i].Cells["Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param16", txt_DLC_ID.Text);// databox.Rows[i].Cells["DLC_Name"].Value.ToString());
                    command.Parameters.AddWithValue("@param17", txt_APP_ID.Text);// databox.Rows[i].Cells["DLC_AppID"].Value.ToString());
                    command.Parameters.AddWithValue("@param26", chbx_Original.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Original"].Value.ToString());
                    command.Parameters.AddWithValue("@param28", chbx_Beta.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Beta"].Value.ToString());
                    command.Parameters.AddWithValue("@param29", chbx_Alternate.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Alternate"].Value.ToString());
                    command.Parameters.AddWithValue("@param30", chbx_MultiTrack.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Multitrack"].Value.ToString());
                    command.Parameters.AddWithValue("@param31", chbx_Broken.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["Is_Broken"].Value.ToString());
                    command.Parameters.AddWithValue("@param32", txt_MultiTrackType.Text);// databox.Rows[i].Cells["MultiTrack_Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param33", txt_Alt_No.Text);/// databox.Rows[i].Cells["Alternate_Version_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param40", chbx_Lyrics.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Vocals"].Value.ToString());
                    command.Parameters.AddWithValue("@param41", chbx_Sections.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Sections"].Value.ToString());
                    command.Parameters.AddWithValue("@param42", chbx_Cover.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Cover"].Value.ToString());
                    command.Parameters.AddWithValue("@param43", chbx_Preview.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Preview"].Value.ToString());
                    command.Parameters.AddWithValue("@param45", chbx_DD.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param47", txt_Tuning.Text);/// databox.Rows[i].Cells["Tunning"].Value.ToString());
                    command.Parameters.AddWithValue("@param48", txt_BassPicking.Text);// databox.Rows[i].Cells["Bass_Picking"].Value.ToString());
                    //command.Parameters.AddWithValue("@param49", "");// databox.Rows[i].Cells["Tones"].Value.ToString());
                    //command.Parameters.AddWithValue("@param50", "");//databox.Rows[i].Cells["Groups"].Value.ToString());
                    command.Parameters.AddWithValue("@param51", txt_Rating.Text);/// databox.Rows[i].Cells["Rating"].Value.ToString());
                    command.Parameters.AddWithValue("@param52", txt_Description.Text);// databox.Rows[i].Cells["Description"].Value.ToString());
                    command.Parameters.AddWithValue("@param54", chbx_TrackNo.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param56", txt_PreviewStart.Text);// databox.Rows[i].Cells["PreviewTime"].Value.ToString());
                    command.Parameters.AddWithValue("@param57", txt_PreviewEnd.Text);// databox.Rows[i].Cells["PreviewLenght"].Value.ToString());
                    command.Parameters.AddWithValue("@param58", YBPlaythrough);// databox.Rows[i].Cells["Youtube_Playthrough"].Value.ToString());
                    command.Parameters.AddWithValue("@param64", chbx_KeepBassDD.Checked ? "Yes" : "No");//);// databox.Rows[i].Cells["Keep_BassDD"].Value.ToString());
                    command.Parameters.AddWithValue("@param65", chbx_KeepDD.Checked ? "Yes" : "No");/// databox.Rows[i].Cells["Keep_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param69", chbx_Selected.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Selected"].Value.ToString());
                    command.Parameters.AddWithValue("@param70", txt_YouTube_Link.Text);// databox.Rows[i].Cells["YouTube_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param71", txt_CustomsForge_Link.Text);// databox.Rows[i].Cells["CustomsForge_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param72", txt_CustomsForge_Like.Text);/// databox.Rows[i].Cells["CustomsForge_Like"].Value.ToString());
                    command.Parameters.AddWithValue("@param73", txt_CustomsForge_ReleaseNotes.Text);// databox.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value.ToString());
                    command.Parameters.AddWithValue("@param76", chbx_Author.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["Has_Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param77", txt_OggPath.Text);//databox.Rows[i].Cells["oggPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param78", txt_OggPreviewPath.Text);//databox.Rows[i].Cells["oggPreviewPath"].Value.ToString());
                    //command.Parameters.AddWithValue("@param80", txt_AlbumArtPath);// databox.Rows[i].Cells["AlbumArt_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param81", txt_Art_Hash.Text);//databox.Rows[i].Cells["Audio_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param82", txt_Preview_Hash.Text);//databox.Rows[i].Cells["AudioPreview_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param83", chbx_BassDD.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Bass_Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param84", chbx_Bonus.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString());
                    command.Parameters.AddWithValue("@param85", txt_Artist_ShortName.Text);// databox.Rows[i].Cells["Artist_ShortName"].Value.ToString());
                    command.Parameters.AddWithValue("@param86", txt_Album_ShortName.Text);// databox.Rows[i].Cells["Album_ShortName"].Value.ToString());
                    command.Parameters.AddWithValue("@param87", chbx_IsLive.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Live"].Value.ToString());
                    command.Parameters.AddWithValue("@param88", txt_Live_Details.Text);// databox.Rows[i].Cells["Live_Details"].Value.ToString());
                    command.Parameters.AddWithValue("@param89", txt_RemotePath.Text);// databox.Rows[i].Cells["Remote_Path"].Value.ToString());
                    command.Parameters.AddWithValue("@param90", chbx_IsAcoustic.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Acoustic"].Value.ToString());
                    command.Parameters.AddWithValue("@param91", txt_Rating.Text);// databox.Rows[i].Cells["Top10"].Value.ToString());
                    command.Parameters.AddWithValue("@param92", chbx_UseInternalDDRemovalLogic.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["UseInternalDDRemovalLogic"].Value.ToString());
                    command.Parameters.AddWithValue("@param93", chbx_IsSingle.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Single"].Value.ToString());
                    command.Parameters.AddWithValue("@param94", chbx_IsInstrumental.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Soundtrack"].Value.ToString());
                    command.Parameters.AddWithValue("@param95", chbx_IsSoundtrack.Checked ? "Yes" : "No");// ? "Yes" : "No");// databox.Rows[i].Cells["Is_Instrumental"].Value.ToString());
                    command.Parameters.AddWithValue("@param96", chbx_IsEP.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_EP"].Value.ToString());
                    command.Parameters.AddWithValue("@param97", chbx_AudioChanged.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Had_Audio_Changed"].Value.ToString());
                    command.Parameters.AddWithValue("@param98", chbx_LyricsChanged.Checked ? "Yes" : "No");//databox.Rows[i].Cells["Has_Had_Lyrics_Changed"].Value.ToString());
                    command.Parameters.AddWithValue("@param99", txt_AlbumSort.Text);
                    command.Parameters.AddWithValue("@param100", chbx_Has_Been_Corrected.Checked ? "Yes" : "No"); //var old = txt_Title.Text; chbx_Has_Been_Corrected
                    command.Parameters.AddWithValue("@param101", txt_DuplicateOf.Text);
                    command.Parameters.AddWithValue("@param102", chbx_Lead.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param103", chbx_Rhythm.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param104", chbx_Bass.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param105", chbx_IsUncensored.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param106", chbx_IntheWorks.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param107", chbx_LyricsLanguage.Text ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param108", chbx_ImprovedWithDM.Checked ? "Yes" : "No");
                    command.Parameters.AddWithValue("@param109", chbx_FullAlbum.Checked ? "Yes" : "No");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + c("dlcm_DBFolder") + "-" + command.CommandText + ex.Message);
                    }
                    finally { if (connection != null) connection.Close(); }

                    if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Changes Saved");
                }
            }
            GroupChanged = false;
            tst = "Stop savin... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //Update_Selected();
            tst = "Stop updatin Selected stat... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return;
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            noOfRec = 0;
            lbl_NoRec.Text = " songs.";
            dssx.Dispose();
            var tst = "Selecting... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            dssx = SelectFromDB("Main", SearchCmd, "", cnb);
            tst = "Stop Selecting... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (dssx.Tables.Count > 0)
                if (dssx.Tables[0].Rows.Count > 0)
                {
                    ////Adding Packing date from pack_:audittrail as from code SQL/JOIN is not working
                    //DataSet dts = new DataSet();
                    //if (SearchCmd.IndexOf("Groups,AdditionalSortColumn DESC") > 0) dts = SelectFromDB("Pack_AuditTrail", "SELECT ID, CDLC_ID, PackDate FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\" ORDER BY ID DESC;", "", cnb);/*AND ID = "+ dssx.Tables[0].Rows[l].ItemArray[0] + "*/
                    //noOfRec = dts.Tables.Count == 0 ? 0 : dts.Tables[0].Rows.Count;

                    //if (noOfRec > 0)
                    //    for (var m = 0; m < dssx.Tables[0].Rows.Count; m++)
                    //        for (var l = 0; l < noOfRec; l++)
                    //            if (dssx.Tables[0].Rows[m].ItemArray[0].ToString() == dts.Tables[0].Rows[l].ItemArray[1].ToString())
                    //            {
                    //                dssx.Tables[0].Rows[m].ItemArray[30] += Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Year + "-" +
                    //                   Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Month + "-" +
                    //                   Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Day + " " +
                    //                   Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Hour + ":" +
                    //                   Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Minute + "." +
                    //                   Convert.ToDateTime(dts.Tables[0].Rows[l].ItemArray[2].ToString()).Second;
                    //                break;
                    //            }

                    DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", DisplayIndex = 0, HeaderText = "ID " };
                    DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", DisplayIndex = 1, HeaderText = "Artist " };
                    DataGridViewTextBoxColumn Song_Title = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title", DisplayIndex = 2, HeaderText = "Song_Title " };
                    DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", DisplayIndex = 3, HeaderText = "Album " };
                    DataGridViewTextBoxColumn Album_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Year", DisplayIndex = 4, HeaderText = "Album_Year " };
                    DataGridViewTextBoxColumn Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Track_No", DisplayIndex = 5, HeaderText = "Track_No " };
                    DataGridViewTextBoxColumn Author = new DataGridViewTextBoxColumn { DataPropertyName = "Author", DisplayIndex = 6, HeaderText = "Author " };
                    DataGridViewTextBoxColumn Version = new DataGridViewTextBoxColumn { DataPropertyName = "Version", DisplayIndex = 7, HeaderText = "Version " };
                    DataGridViewTextBoxColumn Import_Date = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Date", DisplayIndex = 8, HeaderText = "Import_Date " };
                    DataGridViewTextBoxColumn Is_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Original", DisplayIndex = 9, HeaderText = "Is_Original " };
                    DataGridViewTextBoxColumn Song_Title_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title_Sort", HeaderText = "Song_Title_Sort " };
                    DataGridViewTextBoxColumn Artist_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Sort", HeaderText = "Artist_Sort " };
                    DataGridViewTextBoxColumn AverageTempo = new DataGridViewTextBoxColumn { DataPropertyName = "AverageTempo", HeaderText = "AverageTempo " };
                    DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
                    DataGridViewTextBoxColumn Preview_Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Preview_Volume", HeaderText = "Preview_Volume " };
                    DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath " };
                    DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath " };
                    DataGridViewTextBoxColumn audioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreviewPath", HeaderText = "audioPreviewPath " };
                    DataGridViewTextBoxColumn DLC_Name = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_Name", HeaderText = "DLC_Name " };
                    DataGridViewTextBoxColumn DLC_AppID = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_AppID", HeaderText = "DLC_AppID " };
                    DataGridViewTextBoxColumn Current_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Current_FileName", HeaderText = "Current_FileName " };
                    DataGridViewTextBoxColumn Original_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Original_FileName", HeaderText = "Original_FileName " };
                    DataGridViewTextBoxColumn Import_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Path", HeaderText = "Import_Path " };
                    DataGridViewTextBoxColumn Folder_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Folder_Name", HeaderText = "Folder_Name " };
                    DataGridViewTextBoxColumn File_Size = new DataGridViewTextBoxColumn { DataPropertyName = "File_Size", HeaderText = "File_Size " };
                    DataGridViewTextBoxColumn File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "File_Hash", HeaderText = "File_Hash " };
                    DataGridViewTextBoxColumn Original_File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Original_File_Hash", HeaderText = "Original_File_Hash " };
                    DataGridViewTextBoxColumn Is_OLD = new DataGridViewTextBoxColumn { DataPropertyName = "Is_OLD", HeaderText = "Is_OLD " };
                    DataGridViewTextBoxColumn Is_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Beta", HeaderText = "Is_Beta " };
                    DataGridViewTextBoxColumn Is_Alternate = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Alternate", HeaderText = "Is_Alternate " };
                    DataGridViewTextBoxColumn Is_Multitrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Multitrack", HeaderText = "Is_Multitrack " };
                    DataGridViewTextBoxColumn Is_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Broken", HeaderText = "Is_Broken " };
                    DataGridViewTextBoxColumn MultiTrack_Version = new DataGridViewTextBoxColumn { DataPropertyName = "MultiTrack_Version", HeaderText = "MultiTrack_Version " };
                    DataGridViewTextBoxColumn Alternate_Version_No = new DataGridViewTextBoxColumn { DataPropertyName = "Alternate_Version_No", HeaderText = "Alternate_Version_No " };
                    DataGridViewTextBoxColumn DLC = new DataGridViewTextBoxColumn { DataPropertyName = "DLC", HeaderText = "DLC " };
                    DataGridViewTextBoxColumn Has_Bass = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bass", HeaderText = "Has_Bass " };
                    DataGridViewTextBoxColumn Has_Guitar = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Guitar", HeaderText = "Has_Guitar " };
                    DataGridViewTextBoxColumn Has_Lead = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Lead", HeaderText = "Has_Lead " };
                    DataGridViewTextBoxColumn Has_Rhythm = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Rhythm", HeaderText = "Has_Rhythm " };
                    DataGridViewTextBoxColumn Has_Combo = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Combo", HeaderText = "Has_Combo " };
                    DataGridViewTextBoxColumn Has_Vocals = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Vocals", HeaderText = "Has_Vocals " };
                    DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections " };
                    DataGridViewTextBoxColumn Has_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Cover", HeaderText = "Has_Cover " };
                    DataGridViewTextBoxColumn Has_Preview = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Preview", HeaderText = "Has_Preview " };
                    DataGridViewTextBoxColumn Has_Custom_Tone = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Custom_Tone", HeaderText = "Has_Custom_Tone " };
                    DataGridViewTextBoxColumn Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Has_DD", HeaderText = "Has_DD " };
                    DataGridViewTextBoxColumn Has_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Version", HeaderText = "Has_Version " };
                    DataGridViewTextBoxColumn Tunning = new DataGridViewTextBoxColumn { DataPropertyName = "Tunning", HeaderText = "Tunning " };
                    DataGridViewTextBoxColumn Bass_Picking = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Picking", HeaderText = "Bass_Picking " };
                    DataGridViewTextBoxColumn Tones = new DataGridViewTextBoxColumn { DataPropertyName = "Tones", HeaderText = "Tones " };
                    DataGridViewTextBoxColumn Groups = new DataGridViewTextBoxColumn { DataPropertyName = "Groups", HeaderText = "Groups " };
                    DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating " };
                    DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
                    DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
                    DataGridViewTextBoxColumn Has_Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Track_No", HeaderText = "Has_Track_No " };
                    DataGridViewTextBoxColumn Platform = new DataGridViewTextBoxColumn { DataPropertyName = "Platform", HeaderText = "Platform " };
                    DataGridViewTextBoxColumn PreviewTime = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewTime", HeaderText = "PreviewTime " };
                    DataGridViewTextBoxColumn PreviewLenght = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewLenght", HeaderText = "PreviewLenght " };
                    DataGridViewTextBoxColumn Temp = new DataGridViewTextBoxColumn { DataPropertyName = "Temp", HeaderText = "Temp " };
                    DataGridViewTextBoxColumn CustomForge_Followers = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Followers", HeaderText = "CustomForge_Followers " };
                    DataGridViewTextBoxColumn CustomForge_Version = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Version", HeaderText = "CustomForge_Version " };
                    DataGridViewTextBoxColumn FilesMissingIssues = new DataGridViewTextBoxColumn { DataPropertyName = "FilesMissingIssues", HeaderText = "FilesMissingIssues " };
                    DataGridViewTextBoxColumn Duplicates = new DataGridViewTextBoxColumn { DataPropertyName = "Duplicates", HeaderText = "Duplicates " };
                    DataGridViewTextBoxColumn Pack = new DataGridViewTextBoxColumn { DataPropertyName = "Pack", HeaderText = "Pack " };
                    DataGridViewTextBoxColumn Keep_BassDD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_BassDD", HeaderText = "Keep_BassDD " };
                    DataGridViewTextBoxColumn Keep_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_DD", HeaderText = "Keep_DD " };
                    DataGridViewTextBoxColumn Keep_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_Original", HeaderText = "Keep_Original " };
                    DataGridViewTextBoxColumn Song_Lenght = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Lenght", HeaderText = "Song_Lenght " };
                    DataGridViewTextBoxColumn Original = new DataGridViewTextBoxColumn { DataPropertyName = "Original", HeaderText = "Original " };
                    DataGridViewTextBoxColumn Selected = new DataGridViewTextBoxColumn { DataPropertyName = "Selected", HeaderText = "Selected " };
                    DataGridViewTextBoxColumn YouTube_Link = new DataGridViewTextBoxColumn { DataPropertyName = "YouTube_Link", HeaderText = "YouTube_Link " };
                    DataGridViewTextBoxColumn CustomsForge_Link = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Link", HeaderText = "CustomsForge_Link " };
                    DataGridViewTextBoxColumn CustomsForge_Like = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Like", HeaderText = "CustomsForge_Like " };
                    DataGridViewTextBoxColumn CustomsForge_ReleaseNotes = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_ReleaseNotes", HeaderText = "CustomsForge_ReleaseNotes " };
                    DataGridViewTextBoxColumn SignatureType = new DataGridViewTextBoxColumn { DataPropertyName = "SignatureType", HeaderText = "SignatureType " };
                    DataGridViewTextBoxColumn ToolkitVersion = new DataGridViewTextBoxColumn { DataPropertyName = "ToolkitVersion", HeaderText = "ToolkitVersion " };
                    DataGridViewTextBoxColumn Has_Author = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Author", HeaderText = "Has_Author " };
                    DataGridViewTextBoxColumn OggPath = new DataGridViewTextBoxColumn { DataPropertyName = "OggPath", HeaderText = "OggPath " };
                    DataGridViewTextBoxColumn oggPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "oggPreviewPath", HeaderText = "oggPreviewPath " };
                    DataGridViewTextBoxColumn UniqueDLCName = new DataGridViewTextBoxColumn { DataPropertyName = "UniqueDLCName", HeaderText = "UniqueDLCName " };
                    DataGridViewTextBoxColumn AlbumArt_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Hash", HeaderText = "AlbumArt_Hash " };
                    DataGridViewTextBoxColumn Audio_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_Hash", HeaderText = "Audio_Hash " };
                    DataGridViewTextBoxColumn audioPreview_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreview_Hash", HeaderText = "audioPreview_Hash " };
                    DataGridViewTextBoxColumn Bass_Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Has_DD", HeaderText = "Bass_Has_DD " };
                    DataGridViewTextBoxColumn Has_Bonus_Arrangement = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bonus_Arrangement", HeaderText = "Has_Bonus_Arrangement " };
                    DataGridViewTextBoxColumn Artist_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_ShortName", HeaderText = "Artist_ShortName " };
                    DataGridViewTextBoxColumn Album_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Album_ShortName", HeaderText = "Album_ShortName " };
                    DataGridViewTextBoxColumn Available_Old = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Old", HeaderText = "Available_Old " };
                    DataGridViewTextBoxColumn Available_Duplicate = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Duplicate", HeaderText = "Available_Duplicate " };
                    DataGridViewTextBoxColumn Has_Been_Corrected = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Been_Corrected", HeaderText = "Has_Been_Corrected " };
                    DataGridViewTextBoxColumn File_Creation_Date = new DataGridViewTextBoxColumn { DataPropertyName = "File_Creation_Date", HeaderText = "File_Creation_Date " };
                    DataGridViewTextBoxColumn Is_Live = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Live", HeaderText = "Is_Live " };
                    DataGridViewTextBoxColumn Live_Details = new DataGridViewTextBoxColumn { DataPropertyName = "Live_Details", HeaderText = "Live_Details " };
                    DataGridViewTextBoxColumn Remote_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Remote_Path", HeaderText = "Remote_Path " };
                    DataGridViewTextBoxColumn audioBitrate = new DataGridViewTextBoxColumn { DataPropertyName = "audioBitrate", HeaderText = "audioBitrate " };
                    DataGridViewTextBoxColumn audioSampleRate = new DataGridViewTextBoxColumn { DataPropertyName = "audioSampleRate", HeaderText = "audioSampleRate " };
                    DataGridViewTextBoxColumn Is_Acoustic = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Acoustic", HeaderText = "Is_Acoustic " };
                    DataGridViewTextBoxColumn Top10 = new DataGridViewTextBoxColumn { DataPropertyName = "Top10", HeaderText = "Top10 " };
                    DataGridViewTextBoxColumn Has_Other_Officials = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Other_Officials", HeaderText = "Has_Other_Officials " };
                    DataGridViewTextBoxColumn Spotify_Song_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Song_ID", HeaderText = "Spotify_Song_ID" };
                    DataGridViewTextBoxColumn Spotify_Artist_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Artist_ID", HeaderText = "Spotify_Artist_ID " };
                    DataGridViewTextBoxColumn Spotify_Album_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Album_ID", HeaderText = "Spotify_Album_ID " };
                    DataGridViewTextBoxColumn Spotify_Album_URL = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Album_URL", HeaderText = "Spotify_Album_URL " };
                    DataGridViewTextBoxColumn Audio_OrigHash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_OrigHash", HeaderText = "Audio_OrigHash " };
                    DataGridViewTextBoxColumn Audio_OrigPreviewHash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_OrigPreviewHash", HeaderText = "Audio_OrigPreviewHash " };
                    DataGridViewTextBoxColumn AlbumArt_OrigHash = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_OrigHash", HeaderText = "AlbumArt_OrigHash " };
                    DataGridViewTextBoxColumn Duplicate_Of = new DataGridViewTextBoxColumn { DataPropertyName = "Duplicate_Of", HeaderText = "Duplicate_Of " };
                    DataGridViewTextBoxColumn Split4Pack = new DataGridViewTextBoxColumn { DataPropertyName = "Split4Pack", HeaderText = "Split4Pack " };
                    DataGridViewTextBoxColumn UseInternalDDRemovalLogic = new DataGridViewTextBoxColumn { DataPropertyName = "UseInternalDDRemovalLogic", HeaderText = "UseInternalDDRemovalLogic " };
                    DataGridViewTextBoxColumn Is_Single = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Single", HeaderText = "Is_Single " };
                    DataGridViewTextBoxColumn Is_EP = new DataGridViewTextBoxColumn { DataPropertyName = "Is_EP", HeaderText = "Is_EP " };
                    DataGridViewTextBoxColumn Is_Instrumental = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Instrumental", HeaderText = "Is_Instrumental " };
                    DataGridViewTextBoxColumn Is_Soundtrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Soundtrack", HeaderText = "Is_Soundtrack " };
                    DataGridViewTextBoxColumn Has_Had_Audio_Changed = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Had_Audio_Changed", HeaderText = "Has_Had_Audio_Changed " };
                    DataGridViewTextBoxColumn Has_Had_Lyrics_Changed = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Had_Lyrics_Changed", HeaderText = "Has_Had_Lyrics_Changed " };
                    DataGridViewTextBoxColumn Album_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Sort", HeaderText = "Album_Sort " };
                    DataGridViewTextBoxColumn Is_Uncensored = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Uncensored", HeaderText = "Is_Uncensored " };
                    DataGridViewTextBoxColumn IntheWorks = new DataGridViewTextBoxColumn { DataPropertyName = "IntheWorks", HeaderText = "IntheWorks " };
                    DataGridViewTextBoxColumn LyricsLanguage = new DataGridViewTextBoxColumn { DataPropertyName = "LyricsLanguage", HeaderText = "LyricsLanguage " };
                    DataGridViewTextBoxColumn ImprovedWithDM = new DataGridViewTextBoxColumn { DataPropertyName = "ImprovedWithDM", HeaderText = "ImprovedWithDM " };
                    DataGridViewTextBoxColumn Is_FullAlbum = new DataGridViewTextBoxColumn { DataPropertyName = "Is_FullAlbum", HeaderText = "Is_FullAlbum " };
                    DataGridViewTextBoxColumn PitchShiftableEsOrDd = new DataGridViewTextBoxColumn { DataPropertyName = "PitchShiftableEsOrDd", HeaderText = "PitchShiftableEsOrDd " };
                    DataGridViewTextBoxColumn Import_AuditTrail_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Is_FullAImport_AuditTrail_IDlbum", HeaderText = "Import_AuditTrail_ID " };
                    DataGridViewTextBoxColumn AdditionalSortColumn = new DataGridViewTextBoxColumn { DataPropertyName = "AdditionalSortColumn", HeaderText = "AdditionalSortColumn " };
                    dssx.Tables["Main"].AcceptChanges(); tst = "dssx.Tables[Main].AcceptChanges()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    DataGridView.DataSource = dssx.Tables["Main"]; tst = "DataGridView.DataSource = dssx.Tables[Main]... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    DataGridView.Refresh(); tst = "DataGridView.Refresh()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    dssx.Dispose(); tst = "dssx.Dispose()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    ChangeRow();
                    tst = "Stop Populating... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                else MessageBox.Show("No Records/Songs (Imported/Found)");
        }
        private void Update_Selected()
        {
            var tst = "Start updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var SearchCmd22 = "Select count(u.ID) " + SearchCmd.Substring(SearchCmd.IndexOf("FROM Main u"), SearchCmd.IndexOf("ORDER BY") - SearchCmd.IndexOf("FROM Main u")) + ";";//+ SearchCmd22.Substring(0, SearchCmd.IndexOf("ORDER BY")) + ";");
            DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Main", SearchCmd22.Replace(",\") ;", ");"), "", cnb);
            tst = "Stop gettin Total... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var noOfRec = "0";
            try
            {
                if (dsz1 != null) if (dsz1.Tables[0].Rows.Count > 0) noOfRec = dsz1.Tables[0].Rows[0].ItemArray[0].ToString();
                if (SearchCmd.IndexOf("GROUP BY") > 0 && noOfRec.ToInt32() < dsz1.Tables[0].Rows.Count) //for the special SLECTS where GORUP messes the count(id)
                    noOfRec = dsz1.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            if (SearchCmd22.IndexOf("u WHERE") > 0)
            {
                SearchCmd22 = SearchCmd22.Replace("FROM Main u WHERE", "FROM Main u WHERE (Selected=\"Yes\") AND (");
                SearchCmd22 = SearchCmd22.Replace(";", "); ");
            }
            else if (SearchCmd22.IndexOf("WHERE u.") > 0) //for the special SELECTS where JOIN messes the ading the other WHERE con dition
            {
                SearchCmd22 = SearchCmd22.Replace("WHERE u.", "WHERE u.Selected=\"Yes\" AND u.");
                //SearchCmd22 = SearchCmd22.Replace(";", "); ");
            }
            else SearchCmd22 = SearchCmd22.Replace("FROM Main u", "FROM Main u WHERE Selected=\"Yes\" ");
            DataSet dsz2 = new DataSet();
            dsz2 = SelectFromDB("Main", SearchCmd22, "", cnb);
            tst = "Stop gettin Selected... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var noOfSelRec = ""; if (dsz2.Tables.Count > 0) if (dsz2.Tables[0].Rows.Count > 0)
                {
                    noOfSelRec = dsz2.Tables[0].Rows[0].ItemArray[0].ToString();
                    if (SearchCmd.IndexOf("GROUP BY") > 0 && noOfSelRec.ToInt32() < dsz2.Tables[0].Rows.Count) //&& noOfSelRec == "1"for the special SLECTS where GORUP messes the count(id)
                        noOfSelRec = dsz2.Tables[0].Rows.Count.ToString();
                }
            lbl_NoRec.Text = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " songs.";
            //if (noOfRec.ToString()=="0") MessageBox.Show("No Records/Songs (Imported/Found)");

            txt_NoOfSplits.Text = Math.Ceiling(decimal.Parse(noOfRec.ToString()) / c("dlcm_No4Spliting").ToInt32()).ToString();
            tst = "Exitin updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_ChangeCover_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_AlbumArtPath.Text != "" || !File.Exists(txt_AlbumArtPath.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing AlbumArt! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Cover PNG file";
                fbd.Filter = "PNG file (*.png)|*.png";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                ConfigRepository.Instance()["dlcm_TempPath"] = fbd.FileName;

                var tmpWorkDir = Path.Combine(c("dlcm_TempPath"), Path.GetFileNameWithoutExtension(c("dlcm_TempPath")));// Create workDir folder
                if (File.Exists(c("dlcm_TempPath").Replace(".png", ".dds"))) ExternalApps.Png2Dds(c("dlcm_TempPath"), Path.Combine(tmpWorkDir, c("dlcm_TempPath").Replace(".png", ".dds")), 512, 512);
                txt_AlbumArtPath.Text = c("dlcm_TempPath");
                gbox_Cover.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                chbx_Cover.Checked = true;
                txt_Art_Hash.Text = GetHash(c("dlcm_TempPath"));
                if (chbx_AutoSave.Checked) SaveRecord();
            }

        }

        private void btn_OpenStandardization_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(c("dlcm_DBFolder"), c("dlcm_TempPath"), c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39").ToLower() == "yes" ? true : false, c("dlcm_AdditionalManipul40").ToLower() == "yes" ? true : false, cnb, txt_Artist.Text);
            frm.Show();
        }

        private void cbx_Format_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chbx_Format.Text == "PS3_US")
            {
                txt_FTPPath.Text = c("dlcm_FTPUS");
                chbx_PS3HAN.Enabled = true;
            }
            else
            if (chbx_Format.Text == "PS3_EU")
            {
                txt_FTPPath.Text = c("dlcm_FTPEU");
                chbx_PS3HAN.Enabled = true;
            }
            else
            if (chbx_Format.Text == "PS3_JP")
            {
                txt_FTPPath.Text = c("dlcm_FTPJP");
                chbx_PS3HAN.Enabled = true;
            }
            else
            {
                chbx_PS3HAN.Enabled = false;
                if (chbx_Format.Text == "Mac")
                    txt_FTPPath.Text = File.Exists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac");/// c("dlcm_RocksmithDLCPath");               
                else
                if (chbx_Format.Text == "PC")
                    txt_FTPPath.Text = File.Exists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0 ? c("general_rs2014path") : c("dlcm_PC");/// c("dlcm_RocksmithDLCPath");               
                else txt_FTPPath.Text = "";
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {

            Close();
        }
        private void savesettings()
        {
            ConfigRepository.Instance()["dlcm_" + chbx_Format.Text.Replace("PS3_", "FTP")] = txt_FTPPath.Text;
            ConfigRepository.Instance()["dlcm_MainDBFormat"] = chbx_Format.Text;
            ConfigRepository.Instance()["dlcm_RemoveBassDD"] = chbx_RemoveBassDD.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_UniqueID"] = chbx_UniqueID.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_andCopy"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_CopyOld"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul92"] = chbx_PS3HAN.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul93"] = chbx_PS3Retail.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DupliGTrack"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autoplay"] = chbx_AutoPlay.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_netstatus"] = netstatus;
            ConfigRepository.Instance()["dlcm_No4Spliting"] = txt_No4Splitting.Value.ToString();
            ConfigRepository.Instance()["dlcm_InclMultiplyManager"] = chbx_Instaces.Checked == true ? "Yes" : "No";
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void dDataGridView1_CellContentClick_1(object sender, KeyEventArgs eee)
        {
            DataGridView1_CellContentClick_1(sender, eee);
        }

        private void DataGridView1_CellContentClick_1(object sender, KeyEventArgs eee)
        {
            throw new NotImplementedException();
        }

        private void btn_OpenCache(object sender, EventArgs e)
        {

        }

        private void btn_PlaySong(object sender, EventArgs e)
        {

        }

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {
            DDC.Refresh();  // Important
            if (DDC.StartInfo.FileName != "")
            {
                if (DDC.HasExited) Console.WriteLine("Exited.");
                else Console.WriteLine("Running.");
                if (DDC.HasExited == false) if (ProcessStarted) { DDC.Kill(); btn_PlayPreview.Text = "Play Preview"; ProcessStarted = false; return; }
            }
            ProcessStarted = false;


            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggdec2.exe"),
                WorkingDirectory = AppWD
            };
            var t = txt_OggPreviewPath.Text;
            startInfo.Arguments = string.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
            {
                ProcessStarted = true;
                DDC.StartInfo = startInfo;
                DDC.Start(); DDC.WaitForExit(1000 * 30 * 1); //wait 1min
                btn_PlayPreview.Text = "Play Preview";
                ProcessStarted = false;
            }
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //switch (Convert.ToString(e.Result))
            //{
            //    case "done":
            //        if (errorsFound.Length <= 0)
            //        {
            //            MessageBox.Show(
            //                      String.Format("DLC was converted from '{0}' to '{1}'.\n", SourcePlatform.platform, TargetPlatform.platform), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information
            //                  ); convdone = "done";
            //        }
            //        else
            //        {
            //            MessageBox.Show(
            //                  String.Format("DLC was converted from '{2}' to '{3}' with errors. See below: {0}{1}{0}", Environment.NewLine, errorsFound.ToString(), SourcePlatform.platform, TargetPlatform.platform), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning
            //              ); convdone = "done";
            //        }

            //        break;
            //}
        }
        private void doConvert(object sender, DoWorkEventArgs e)
        {
            // SOURCE
            var sourcePackage = e.Argument as string;
            errorsFound = new StringBuilder();
            var step = (int)Math.Round(1.0 / sourcePackage.Length * 100, 0);
            int progress = 0;
            bwConvert.ReportProgress(progress, string.Format("Converting '{0}' to {1} platform.", Path.GetFileName(sourcePackage), TargetPlatform.platform.GetPathName()[0]));
            if (!sourcePackage.IsValidPSARC())
            {
                errorsFound.AppendLine(string.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(sourcePackage)));

                return;
            }

            var alertMessage = string.Format("Source package '{0}' seems to be not {1} platform, the conversion impossible.", Path.GetFileName(sourcePackage), SourcePlatform);
            var haveCorrectName = Path.GetFileNameWithoutExtension(sourcePackage).EndsWith(SourcePlatform.GetPathName()[2]);
            if (SourcePlatform.platform == GamePlatform.PS3)
                haveCorrectName = Path.GetFileNameWithoutExtension(sourcePackage).EndsWith(SourcePlatform.GetPathName()[2] + ".psarc");

            if (!haveCorrectName) { errorsFound.AppendLine(alertMessage); }

            try
            {
                // CONVERT
                var output = DLCPackageConverter.Convert(sourcePackage, SourcePlatform, TargetPlatform, "248750");
                if (!string.IsNullOrEmpty(output))
                    errorsFound.AppendLine(output);
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            progress += step;
            bwConvert.ReportProgress(progress);
            bwConvert.ReportProgress(100);
            e.Result = "done";
            convdone = "done";
        }

        private void btn_AddPreview_Click(object sender, EventArgs e)
        //for conversion the wwise needs to be downlaoded from https://www.audiokinetic.com/download/?id=2014.1.6_5318 or https://www.audiokinetic.com/download/?id=2013.2.10_4884
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_AudioPreviewPath.Text != "") result1 = MessageBox.Show("Are you sure you want to replace existing Preview! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggcut.exe"),
                WorkingDirectory = AppWD.Replace("external_tools", "")
            };
            var t = txt_OggPath.Text;
            var tt = t.Replace(".ogg", "_preview_fixed.ogg");
            string[] timepieces = txt_PreviewStart.Text.ToString().Split(':');
            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
            startInfo.Arguments = string.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                t,
                                                tt,
                                                r.TotalMilliseconds,
                                                (r.TotalMilliseconds + (txt_PreviewEnd.Text.ToInt32() * 1000)));
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        var wwisePath = "";
                        if (!string.IsNullOrEmpty(c("general_wwisepath")))
                            wwisePath = c("general_wwisepath");
                        else
                            wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                        if (wwisePath == "")
                        {
                            ErrorWindow frm1 = new ErrorWindow("In order to add a preview, please Install Wwise Launcher then Wwise v" + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", false, false, true, "", "", "");
                            frm1.ShowDialog();
                        }
                        Converters(tt, ConverterTypes.Ogg2Wem, false, true);
                        txt_OggPreviewPath.Text = tt;
                        chbx_Preview.Checked = true;
                        txt_AudioPreviewPath.Text = tt.Replace(".ogg", ".wem");
                        chbx_Preview.Checked = true;
                        btn_PlayPreview.Enabled = true;
                        txt_Preview_Hash.Text = GetHash(tt);
                        AddPreview = true;
                        chbx_AudioChanged.Checked = true;
                        if (chbx_AutoSave.Checked) SaveRecord();
                        chbx_ImprovedWithDM.Checked = true;
                    }
                }
            return;
        }

        private void btn_SelectPreview_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Preview OGG file";
                fbd.Filter = "OGG file (*.ogg)|*.ogg";
                fbd.Multiselect = false;
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                ConfigRepository.Instance()["dlcm_TempPath"] = fbd.FileName;
                txt_OggPreviewPath.Text = c("dlcm_TempPath");
                chbx_Preview.Checked = true;
                txt_OggPreviewPath.Text = c("dlcm_TempPath");
                Converters(c("dlcm_TempPath"), ConverterTypes.Ogg2Wem, true, false);
                txt_AudioPreviewPath.Text = c("dlcm_TempPath").Replace(".ogg", ".wem");
                txt_Preview_Hash.Text = GetHash(c("dlcm_TempPath").Replace(".ogg", ".wem"));
                chbx_AudioChanged.Checked = true;
                if (chbx_AutoSave.Checked) SaveRecord();
            }
        }

        private void txt_Author_TextChanged(object sender, EventArgs e)
        {
            if (txt_Author.Text != null && txt_Author.Text != "") chbx_Author.Checked = true;
            else chbx_Author.Checked = false;
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            var prev = databox.SelectedCells[0].RowIndex;
            if (prev == 0) return;

            if (chbx_AutoSave.Checked) SaveRecord();

            int rowindex;
            var i = databox.SelectedCells[0].RowIndex;
            rowindex = i;
            databox.Rows[rowindex].Selected = false;
            databox.CurrentCell = databox.Rows[rowindex - 1].Cells[0];
            databox.Rows[rowindex].Selected = false;
            databox.Rows[rowindex - 1].Selected = true;
        }

        private void btn_NextItem_Click(object sender, EventArgs e)
        {
            var prev = databox.SelectedCells[0].RowIndex;
            if (databox.Rows.Count <= prev + 2) return;

            if (chbx_AutoSave.Checked) SaveRecord();

            int rowindex;
            var i = databox.SelectedCells[0].RowIndex;
            rowindex = i;
            databox.Rows[rowindex].Selected = false;
            databox.CurrentCell = databox.Rows[rowindex + 1].Cells[0];
            databox.CurrentCell.Selected = true;
            databox.Rows[rowindex + 1].Selected = true;
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            chbx_Selected.Checked = true;
            if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "Yes");

            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "Yes");
            }
            if (chbx_InclLanguage.Checked)
            {
                command.CommandText += ",LyricsLanguage = @param11 ";
                command.Parameters.AddWithValue("@param11", chbx_LyricsLanguage.Text);
            }
            if (chbx_InclBroken.Checked)
            {
                command.CommandText += ",Is_Broken = @param10 ";
                command.Parameters.AddWithValue("@param10", "Yes");
            }

            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("  ", " ").Replace(SearchFields.Replace("  ", " "), "ID ").Replace(";", "") + ")";

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (chbx_InclGroups.Checked)
            {
                //identify all already selected Groups as to not to overitte their
                var sel = "SELECT CDLC_ID, Groups, ID FROM Groups WHERE Type =\"DLC\" AND CDLC_ID IN (" + SearchCmd.Replace("  ", " ").Replace(SearchFields.Replace("  ", " "), "ID ").Replace(";", "") + ")";
                DataSet grp = new DataSet(); grp = SelectFromDB("Groups", sel, "", cnb);
                var noOfRecs = grp.Tables.Count == 0 ? 0 : grp.Tables[0].Rows.Count;
                var grpsel = "("; var found = false;
                for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                {
                    found = false;
                    for (var k = 0; k < noOfRecs; k++)
                        if (chbx_AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == chbx_AllGroups.Items[j].ToString())
                        {
                            grpsel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                            found = true;
                        }

                    if (!found && !chbx_AllGroups.GetItemChecked(j))
                    {
                        var insertcmdd = "CDLC_ID, Groups, Type, Date_Added";
                        var insertvalues = "\"" + txt_ID.Text + "\", \"" + chbx_AllGroups.Items[j].ToString() + "\", \"DLC\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"";
                        InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0);
                    }
                }
                grpsel = grpsel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace(",)", ")") + ")";

                //if (grpsel != "()") DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND ID not IN " + grpsel + "", cnb);

                var tst = "Stop saving groups... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            }

            Populate(ref databox, ref Main);
            databox.Refresh();
            Update_Selected();
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {
            chbx_Selected.Checked = false;
            var cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "No");
            var test = "";
            if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "No");
                test = " or Beta";
                chbx_Beta.Checked = false;
            }
            if (chbx_InclLanguage.Checked)
            {
                command.CommandText += ",LyricsLanguage = @param11 ";
                command.Parameters.AddWithValue("@param11", chbx_LyricsLanguage.Text);
                test = " or Broken";
                chbx_Broken.Checked = false;
            }
            if (chbx_InclBroken.Checked)
            {
                command.CommandText += ",Is_Broken = @param10 ";
                command.Parameters.AddWithValue("@param10", "No");
                test = " or Broken";
                chbx_Broken.Checked = false;
            }
            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + c("dlcm_DBFolder") + "-" + command.CommandText);
                throw;
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }

            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";
            }

            Populate(ref databox, ref Main);
            databox.Refresh();
            Update_Selected();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected" + test);
        }

        private void btn_RemoveBassDD_Click(object sender, EventArgs e)
        {

            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GenericFunctions.GetRecord_s(cmd, cnb);

            var xmlFiles = Directory.GetFiles(SongRecord[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = SongRecord[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
            {
                Song2014 xmlContent = null;
                try
                {
                    xmlContent = Song2014.LoadFromFile(xml);
                    if (xmlContent.Arrangement.ToLower() == "bass" && xml.IndexOf(".old") <= 0)
                    {
                        var bassRemoved = (RemoveDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false, chbx_UseInternalDDRemovalLogic.Checked ? "Yes" : "No") == "Yes") ? "Yes" : "No";
                        chbx_BassDD.Checked = false;
                        btn_RemoveBassDD.Enabled = false;
                        break;
                    }
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            chbx_ImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; btn_RemoveBassDD.Enabled = true;
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GenericFunctions.GetRecord_s(cmd, cnb);

            var xmlFiles = Directory.GetFiles(SongRecord[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = SongRecord[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
                if (xml.IndexOf("showlights") < 1)
                {
                    var DDAdded = (AddDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false, txt_AddDD.Value.ToString()) == "Yes") ? "No" : "Yes";
                }
            chbx_BassDD.Checked = true;
            chbx_DD.Checked = true;
            chbx_ImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_RemoveDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = false; btn_AddDD.Enabled = true; btn_RemoveBassDD.Enabled = false;
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GenericFunctions.GetRecord_s(cmd, cnb);

            var xmlFiles = Directory.GetFiles(SongRecord[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = SongRecord[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
            {
                Song2014 xmlContent = null;
                try
                {
                    xmlContent = Song2014.LoadFromFile(xml);
                    if (!(xmlContent.Arrangement.ToLower() == "showlights" || xmlContent.Arrangement.ToLower() == "vocals") || xml.IndexOf(".old") <= 0)
                    {
                        var DDRemoved = (RemoveDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false, chbx_UseInternalDDRemovalLogic.Checked ? "Yes" : "No") == "Yes") ? "Yes" : "No";
                    }
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            chbx_DD.Checked = false;
            chbx_BassDD.Checked = false;
            chbx_ImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_OldFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = c("dlcm_TempPath") + "\\0_old\\" + databox.Rows[i].Cells["Original_FileName"].Value.ToString();
            try
            {
                Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Old Folder in Explorer ! ");
            }
        }

        private void btn_DuplicateFolder_Click(object sender, EventArgs e)
        {
            string t = c("dlcm_TempPath") + "\\0_duplicate";
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            ////1. Delete Song Folder           
            var sel = "SELECT ID FROM Main WHERE ID IN (";

            for (int k = 0; k < databox.SelectedRows.Count; k++)
            {
                if (k > 0) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            sel += ")";

            var cmd = "DELETE FROM Main WHERE ID IN (" + sel + ")";
            var i = databox.SelectedCells[0].RowIndex;

            //1. Delete DB records
            DeleteRecords(txt_ID.Text, cmd, c("dlcm_DBFolder"), c("dlcm_TempPath"), databox.SelectedRows.Count.ToString(), "", cnb);/*databox.Rows[i].Cells["Original_File_Hash"].Value.ToString()*/

            //refresh 
            Populate(ref databox, ref Main);
            databox.Refresh();
            if (i > 0 && databox.RowCount >= i)
            {
                i = i - 1;
                databox.FirstDisplayedScrollingRowIndex = i; databox.Rows[i].Selected = true;
                databox.Focus();
            }
            else i = 0;
            //Update_Selected();
        }

        private void btn_Duplicate_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();

            //1. Copy Files
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();

            //Generate MAX Alternate NO
            var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + txt_Artist.Text + "\") AND ";
            sel += "(LCASE(Song_Title) = LCASE(\"" + txt_Title.Text + "\") ";
            sel += "OR LCASE(Song_Title) like \"%" + txt_Title.Text.ToLower() + "%\" ";
            sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + txt_Title_Sort.Text + "\")) OR LCASE(DLC_Name) like LCASE(\"%" + txt_DLC_ID.Text + "%\") ORDER BY Is_Original ASC";
            var sel1 = sel.Replace("SELECT *", "SELECT max(Alternate_Version_No)");
            sel1 = sel1.Replace(" ORDER BY Is_Original ASC", "");
            int max = 1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder")))
            {
                DataSet ddzv = new DataSet();
                OleDbDataAdapter dat = new OleDbDataAdapter(sel1, cnn);
                dat.Fill(ddzv, "Main");
                dat.Dispose();
                max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                max += 1;
            }
            string t = filePath + (txt_Platform.Text == "XBOX360" ? "\\Root" : "") + max.ToString();
            string source_dir = @filePath;
            string destination_dir = @t;
            var fold = "";
            var fold2 = "";
            try //Copy dir
            {
                CopyFolder(source_dir, destination_dir);

                //copy old
                if (databox.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")
                {
                    fold = c("dlcm_TempPath") + "\\0_old\\" + databox.Rows[i].Cells["Original_FileName"].Value.ToString();
                    fold2 = Path.GetFileNameWithoutExtension(fold) + "" + max.ToString();
                    File.Copy(fold, fold2.Replace(fold, fold2), true);
                }

            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show("FAILED To copy Files" + ex.Message + "----");
            }

            //2. Copy Records
            try //Copy dir
            {
                var AlbumArtPath = databox.Rows[i].Cells["AlbumArtPath"].Value.ToString().Replace(source_dir, destination_dir);
                var AudioPath = databox.Rows[i].Cells["AudioPath"].Value.ToString().Replace(source_dir, destination_dir);
                var audioPreviewPath = databox.Rows[i].Cells["audioPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var OggPath = databox.Rows[i].Cells["OggPath"].Value.ToString().Replace(source_dir, destination_dir);
                var oggPreviewPath = databox.Rows[i].Cells["oggPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var insertcmdd = "Song_Title, Song_Title_Sort, Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, AlbumArtPath, AudioPath," +
                    " audioPreviewPath, Track_No, Author, Version, DLC_Name, DLC_AppID, Current_FileName, Original_FileName, Import_Path, Import_Date, Folder_Name," +
                    " File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No," +
                    " DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version," +
                    " Tunning, Bass_Picking, Tones, Groups, Rating, Description, Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Youtube_Playthrough," +
                    " CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD, Keep_DD, Keep_Original, Song_Lenght, Original," +
                    " Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, OggPath," +
                    " oggPreviewPath, UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName," +
                    " Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date, Is_Live, Live_Details, Remote_Path, audioBitrate, audioSampleRate, Is_Acoustic," +
                    " Has_Other_Officials, Spotify_Song_ID, Spotify_Artist_ID, Spotify_Album_ID, Spotify_Album_URL, Top10, Audio_OrigHash, Audio_OrigPreviewHash," +
                    " AlbumArt_OrigHash, Duplicate_Of, Split4Pack, UseInternalDDRemovalLogic, Is_Instrumental, Is_Single, Is_Soundtrack, Is_EP, Has_Had_Audio_Changed, Has_Had_Lyrics_Changed, Album_Sort";

                var insertvalues = "SELECT Song_Title+\" alt" + max.ToString() + "\", Song_Title_Sort+\" alt" + max.ToString() + "\", Album, Artist, Artist_Sort, Album_Year," +
                    " AverageTempo, Volume, Preview_Volume, \"" + AlbumArtPath + "\", \"" + AudioPath + "\", \"" + audioPreviewPath + "\", Track_No, Author, " +
                    "Version, DLC_Name+\"" + max.ToString() + "\", DLC_AppID, Current_FileName, \"" + fold2.Replace(fold, fold2) + "\", Import_Path, Import_Date," +
                    " Folder_Name+\"" + max.ToString() + "\", File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, \"" + "Yes" + "\", Is_Multitrack," +
                    " Is_Broken, MultiTrack_Version, " + max.ToString() + ", DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover," +
                    " Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description+\" duplicate\", Comments, Has_Track_No, " +
                    "Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD," +
                    " Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType," +
                    " ToolkitVersion, Has_Author, \"" + OggPath + "\", \"" + oggPreviewPath + "\", UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD," +
                    " Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date, Is_Live," +
                    "Live_Details, Remote_Path, audioBitrate, audioSampleRate, Is_Acoustic, Has_Other_Officials, Spotify_Song_ID, Spotify_Artist_ID, Spotify_Album_ID, " +
                    "Spotify_Album_URL, Top10, Audio_OrigHash, Audio_OrigPreviewHash, AlbumArt_OrigHash, \"" + txt_ID.Text + "\", Split4Pack, UseInternalDDRemovalLogic," +
                    " Is_Instrumental, Is_Single, Is_Soundtrack, Is_EP, Has_Had_Audio_Changed, Has_Had_Lyrics_Changed, Album_Sort" +
                    " FROM Main WHERE ID = " + txt_ID.Text;
                InsertIntoDBwValues("Main", insertcmdd, insertvalues, cnb, 0);


                //getting ID
                DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + txt_Title.Text + " alt" + max.ToString() + "\"", "", cnb);
                var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                    "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                    "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                    "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty";
                insertvalues = "SELECT Arrangement_Name, " + CDLC_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                    "instr(JSONFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                    " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                    " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                    " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash," +
                    " Json_Hash, Part, MaxDifficulty FROM Arrangements WHERE CDLC_ID = " + txt_ID.Text;
                InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0);

                insertcmdd = "Tone_Name, CDLC_ID, Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1," +
                    " PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues," +
                    " AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, ConversionDateTime, lastConverjsonDateTime, Comments";
                insertvalues = "SELECT Tone_Name, \"" + CDLC_ID + "\", Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator," +
                    " Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType," +
                    " AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, ConversionDateTime," +
                    " lastConverjsonDateTime, Comments FROM Tones WHERE CDLC_ID = " + txt_ID.Text; ;
                InsertIntoDBwValues("Tones", insertcmdd, insertvalues, cnb, 0);

                insertcmdd = "CDLC_ID, Gear_Name, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex, Type, Comments, Tone_Name, Tone_ID";
                insertvalues = "SELECT \"" + CDLC_ID + "\", Gear_Name, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex, Type, Comments, Tone_Name, Tone_ID" +
                    " FROM Tones_GearList WHERE CDLC_ID = " + txt_ID.Text; ;
                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, 0);

                MessageBox.Show("Record has been duplicated");

                Populate(ref databox, ref Main);
                databox.Refresh();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show("FAILED To copy REcords" + ex.Message + "----");
            }
        }

        public class CookieAwareWebClient : WebClient
        {
            public void preLogin(string loginPageAddress, NameValueCollection loginData)
            {
                CookieContainer container;

                var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

                WebHeaderCollection myWebHeaderCollection = request.Headers;//Get the headers associated with the request.
                myWebHeaderCollection.Add("Accept-LyricsLanguage", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
                request.Headers = myWebHeaderCollection;
                request.Headers.Add("Cookie", "__cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1");
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36 OPR/41.0.2353.69";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.AutomaticDecompression = System.Net.DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.Host = "customsforge.com";
                request.Method = "GET";
                //  request.CachePolicy.Equals("no-cache, must-revalidate, max-age=0");
                //request.TransferEncoding = true;//.Equals("chunked");// gzip, deflate, lzma";
                //                Upgrade - Insecure - Requests: 1
                //User - Agent: Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 54.0.2840.99 Safari / 537.36 OPR / 41.0.2353.69
                //Accept: text / html,application / xhtml + xml,application / xml; q = 0.9,image / webp,*/*;q=0.8
                //Accept-Encoding: gzip, deflate, lzma, sdch
                //Accept-LyricsLanguage: en-US,en;q=0.8
                //Cookie: __cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1

                //Please use this,
                //HttpWebRequest request = null;
                //  request = HttpWebRequest.Create(loginPageAddress) as HttpWebRequest;
                HttpWebResponse TheRespone = (HttpWebResponse)request.GetResponse();
                string setCookieHeader = TheRespone.Headers[HttpResponseHeader.SetCookie];

                //var buffer = System.Text.Encoding.ASCII.GetBytes(loginData.ToString());
                //request.ContentLength = buffer.Length;
                //var requestStream = request.GetRequestStream();
                //requestStream.Write(buffer, 0, buffer.Length);
                //requestStream.Close();

                container = request.CookieContainer = new CookieContainer();
                CookieContainer = container;

                //var response = request.GetResponse();
                TheRespone.Close();
                //CookieContainer = TheRespone.Headers; ;
                //var t = request.Headers.ToString();
            }

            public void Login(string loginPageAddress, NameValueCollection loginData)
            {
                // CookieContainer container;

                var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);
                request.Headers.Add("Cookie", "__cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1");
                request.ContentType = "application/x-www-form-urlencoded";
                WebHeaderCollection myWebHeaderCollection = request.Headers;//Get the headers associated with the request.
                myWebHeaderCollection.Add("Accept-LyricsLanguage", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
                request.Headers = myWebHeaderCollection;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36 OPR/41.0.2353.69";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.AutomaticDecompression = System.Net.DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.Host = "customsforge.com";
                request.CookieContainer = CookieContainer;
                request.Method = "POST";
                var buffer = System.Text.Encoding.ASCII.GetBytes(loginData.ToString());
                request.ContentLength = buffer.Length;
                var requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();

                var response = request.GetResponse();
                response.Close();
            }

            public CookieAwareWebClient(CookieContainer container)
            {
                CookieContainer = container;
            }

            public CookieAwareWebClient()
              : this(new CookieContainer())
            { }

            public CookieContainer CookieContainer { get; private set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address) as HttpWebRequest;
                request.Headers.Add("Cookie", "__cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1");
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36 OPR/41.0.2353.69";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.KeepAlive = true;
                request.AutomaticDecompression = System.Net.DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.Host = "customsforge.com";
                WebHeaderCollection myWebHeaderCollection = request.Headers;//Get the headers associated with the request.
                myWebHeaderCollection.Add("Accept-LyricsLanguage", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
                request.Headers = myWebHeaderCollection;

                //var request = (HttpWebRequest)base.GetWebRequest(address);
                request.CookieContainer = CookieContainer;
                return request;
            }
        }


        public static int GetTrackNo(string Artist, string Album, string Title, DateTime timestamp)
        {
            string uriString = "https://api.spotify.com/v1/search";
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection
            {
                { "query", keywordString }
            };
            var a1 = ""; var ab = "";
            var albump = 0;
            var artistp = 0;
            var tracknop = 0;
            try
            {
                webClient.QueryString.Add(nameValueCollection);
                var aa = (webClient.DownloadString(uriString));
                ab = aa;
                albump = (aa.ToLower()).IndexOf(Album.ToLower());
                if (albump > 0) aa = aa.Substring(albump, aa.Length - albump);
                artistp = (aa.ToLower()).IndexOf(Artist.ToLower());
                if (artistp > 0) aa = aa.Substring(artistp, aa.Length - artistp);
                tracknop = (aa.ToLower()).IndexOf("track_number");
                if (tracknop > 0) a1 = aa.Substring(tracknop + 15, 3);
                a1 = a1.Replace(",", "");
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            a1 = a1.Trim();
            if (a1 == "" && Album != "")
            {
                a1 = GetTrackNo(Artist, "", Title, timestamp).ToString();
            }
            if (a1 == "" && Artist != "")
            {
                a1 = GetTrackNo("", "", Title, timestamp).ToString();
            }
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
        }

        private async Task bth_GetTrackNo_ClickAsync(object sender, EventArgs e)
        {
            var CleanTitle = "";
            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            if (txt_Title.Text.IndexOf(")") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf(")"), txt_Title.Text.Length - txt_Title.Text.IndexOf(")"));
            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;


            int z = await GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, txt_Title.Text, txt_Album_Year.Text, txt_SpotifyStatus.Text, timestamp);
            txt_Track_No.Text = z == 0 && txt_Track_No.Text != "" ? txt_Track_No.Text : z.ToString();
        }

        private void txt_Track_No_TextChanged(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;

            if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; databox.Rows[i].Cells["Has_Track_No"].Value = "No"; }
            else { chbx_TrackNo.Checked = true; databox.Rows[i].Cells["Has_Track_No"].Value = "Yes"; }
        }

        private void btn_OpenSongFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }
        }

        private void btn_Youtube_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_YouTube_Link.Text);
        }

        private void btn_Playthrough_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_Playthrough.Text);
        }

        private void btn_CustomForge_Link_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_CustomsForge_Link.Text);
        }

        private void chbx_MultiTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_MultiTrack.Checked) txt_MultiTrackType.Enabled = true;
            else txt_MultiTrackType.Enabled = false;
        }

        private void btn_DefaultCover_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_AlbumArtPath.Text != "" || !File.Exists(txt_AlbumArtPath.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing AlbumArt! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            var cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + txt_AlbumArtPath.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" and Album=\"" + txt_Album.Text + "\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);
            RocksmithToolkitGUI.DLCManager.Standardization.MakeCover(cnb);
        }

        private void btn_AddSections_Click(object sender, EventArgs e)
        {
            //var j = databox.SelectedCells[0].RowIndex;
            //var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PathForBRM"), " ").Replace("\\ ", " ");//c("dlcm_TempPath")+"\\0_old\\"+txt_OldPath.Text
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex;timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}

            //string filePath = c("dlcm_TempPath") + "\\0_old\\" + databox.Rows[j].Cells["Original_FileName"].Value.ToString();
            //try
            //{
            //    Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex;timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Old Folder in Explorer ! ");
            //}
            btm(true);
        }

        private void btn_InvertSelect_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Main SET Selected = \"No\"";
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                cmd1 += ",Is_Beta = \"No\" ";
                test = " and Not Beta";
            }
            if (chbx_InclBroken.Checked)
            {
                cmd1 += ",Is_Broken = \"No\" ";
                test = " and Not Broken";
            }
            if (chbx_InclLanguage.Checked)
            {
                cmd1 += ",LyricsLanguage = \"" + chbx_LyricsLanguage.Text + "\" ";
                test = " and with LyricsLanguage";
            }

            if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";

                DeleteFromDB("Groups", cmd, cnb);
                test += " and no Groups";
            }
            cmd1 += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);
            Populate(ref databox, ref Main);
            databox.Refresh();
            var cnt = 0;
            if (dgt.Tables.Count > 0) cnt = dgt.Tables[0].Rows.Count;
            Update_Selected();
            MessageBox.Show("All Filtered songs have been marked as UnSelected" + test);
        }

        private void btn_Copy_old_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            DataSet dhs = new DataSet(); var cmd = "SELECT * FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID ") + ")";
            dhs = SelectFromDB("Main", cmd, "", cnb);
            noOfRec = dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            var dest = ""; var aa = ""; var bb = "";
            pB_ReadDLCs.Maximum = noOfRec; var j = 0;
            for (var i = 0; i < noOfRec; i++)
            {
                string filePath = c("dlcm_TempPath") + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19];
                dest = c("dlcm_RocksmithDLCPath") + "\\" + dhs.Tables[0].Rows[i].ItemArray[19];
                var eef = dhs.Tables[0].Rows[i].ItemArray[87].ToString();
                if (eef == "Yes")//OLd available
                {
                    try
                    {
                        if (File.Exists(dest)) aa += dhs.Tables[0].Rows[i].ItemArray[19] + "\n";
                        else
                        {
                            File.Copy(filePath, dest, false);
                            if (!File.Exists(dest)) MessageBox.Show(filePath + "----" + dest + "Error at copy OLD ");
                            j++;
                        }
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ex);
                    }
                }
                else bb += dhs.Tables[0].Rows[i].ItemArray[19] + "\n";
                pB_ReadDLCs.Value++;
            }
            MessageBox.Show("Notcopied:" + bb + "\n" + j + " Files Copied to " + c("dlcm_RocksmithDLCPath") + ". " + (aa != "" ? "Already existing and so not copied: " + aa : ""));
        }

        private void btn_SelectInverted_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 4; pB_ReadDLCs.Step = 1;
            var command = cnb.CreateCommand();

            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "No");
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "No");
                test = " or Beta";
            }
            if (chbx_InclLanguage.Checked)
            {
                command.CommandText += ",LyricsLanguage = @param11 ";
                command.Parameters.AddWithValue("@param11", chbx_LyricsLanguage.Text);
                test = " or with LyricsLanguage";
            }
            if (chbx_InclBroken.Checked)
            {
                command.CommandText += ",Is_Broken= @param10 ";
                command.Parameters.AddWithValue("@param10", "No");
                test = " or Broken";
            }
            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";

            try
            {
                command.CommandType = CommandType.Text;
                pB_ReadDLCs.Increment(1);
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            var CommandText = "UPDATE Main SET ";
            CommandText += "Selected = \"Yes\" ";

            CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")"; pB_ReadDLCs.Increment(1);
            UpdateDB("Main", CommandText, cnb);

            pB_ReadDLCs.Increment(1); Populate(ref databox, ref Main);
            databox.Refresh(); pB_ReadDLCs.Increment(1);

            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID Not IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")", "", cnb);
            Update_Selected();
            MessageBox.Show("All NON Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected" + test);
        }

        private void txt_Artist_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchClick(e);
        }
        public void SearchClick(KeyPressEventArgs e)
        {
            if (SearchON)

                if (e.KeyChar == (char)Keys.Enter)
                {
                    btn_Search.PerformClick();
                    btn_GoTo.Enabled = false;
                }
        }

        private void btn_Beta_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Is_Beta = @param8 ";
            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            command.Parameters.AddWithValue("@param8", "Yes");
            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Populate(ref databox, ref Main);
            databox.Refresh();
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID IN(" + SearchCmd.Replace(" * ", "ID").Replace("; ", "").Replace(SearchFields, "ID") + ")", "", cnb);
            MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Beta");
        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            eof(true);
        }

        public void eof(bool openfile)
        {
            string audio = "";
            if (openfile)
            {
                var i = databox.SelectedCells[0].RowIndex;

                string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
                audio = txt_OggPath.Text;

                try
                {
                    Process process = Process.Start(@filePath);
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Song Folder in Exporer ! ");
                }
            }
            var paath = c("dlcm_EoFPath");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_EoFPath"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Editor on Fire if you want to use it.", c("dlcm_EoFPath_www"), "Missing Editor on Fire", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            var startInfo = new ProcessStartInfo
            {
                FileName = xx,
                WorkingDirectory = AppWD.Replace("external_tools", ""),
                Arguments = string.Format("'" + audio + "'"),
                UseShellExecute = false,
                CreateNoWindow = true
            };

            if (File.Exists(xx) && File.Exists(c("dlcm_DBFolder")))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start();
                }

            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_CreateLyrics_Click(object sender, EventArgs e)
        {
            //1. Open Internet Explorer
            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "Lyrics";

            ErrorWindow frm1 = new ErrorWindow("Option A: Use Rockband lyrics\nOption B: Tab your own\n\n\nA.\n\t1.Check if already tabbed for RockBand https://rhythmgamingworld.com" +
                "\n\n\t2. Extract using tools on site (C3 CON Tools v4.0.1 https://rhythmgamingworld.com/forums/topic/c3-con-tools-v401-8142020-weve-only-just-begun/ )\n\n\t3.Import Midi \"lyrics\"" +
                " in EditorOnFire (togther with Rocksmith OGG)\n\n\t4.Test in EoF and Shift timing (select all and move or using the +option) and import back to Vocal track " +
                " \n\n\n\nB.\n\t1.Google for Lyrics e.g." + link + " \n\n\t2. Use Ultrastar" +
                "Creator Tab lyrics to the songs time signature (if crashing at play open it from outside DLC Manager)\n\n\t3. Using EditorOnFire Transform Ultrastar simple file" +
                "using Add Lyrics button\n\n\n(IF you are adding Lyrics end of song past what was captured in Ultrastar remember to select the last EoF added lyrics and Note-Lyrics Mark to save it in the saved XML.)"
                ,"http://ignition.customsforge.com/eof", "Tutorial on Adding Vocal Tracks to Rocksmith CDLC", true, true, false, "Option A", "Option B", "");
            frm1.ShowDialog();
            if (frm1.IgnoreSong)
                try
                {
                    Process process = Process.Start(@link.Replace(link, "https://db.c3universe.com/songs"));
                    var j = databox.SelectedCells[0].RowIndex;
                    var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_RockBand"));
                    if (!File.Exists(xx))
                    {
                        ErrorWindow frm = new ErrorWindow("Install C3 Conversion Tools if you want to use it.", c("dlcm_RockBand_www"),
    "Missing C3 Rockband conversion tools", false, false, true, "", "", ""); frm1.ShowDialog(); return;
                    }

                    Process procesf = Process.Start(@xx);
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
                }
            else if (frm1.StopImport)
                try
                {
                    var ps = new ProcessStartInfo(link)
                    {
                        UseShellExecute = true,
                        Verb = "open"
                    };
                    Process.Start(ps);
                    //Process process = Process.Start(@link);
                    //3. Open Ultrastar pointing at the Song Ogg
                    var paath = c("dlcm_UltraStarCreator");
                    var xx = "";
                    if (File.Exists(paath)) xx = paath;
                    else xx = Path.Combine(AppWD, "UltraStar Creator\\usc.exe");
                    if (!File.Exists(xx)) { ErrorWindow frm2 = new ErrorWindow("Install UltraStar Creator if you want to use it.", c("dlcm_UltraStarCreator_www"), "Missing UltraStar Creator", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = xx,
                        WorkingDirectory = AppWD.Replace("external_tools", ""),
                        Arguments = string.Format(" \"" + txt_OggPath.Text + "\""),
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    if (File.Exists(xx) && File.Exists(c("dlcm_DBFolder")))
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo; DDC.Start();
                            Process procez = Process.Start(@xx);
                        }
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
                }
            else return;

            //2. Open Song Folder
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }

            //4. Open EoF
            eof(true);
            //5. Import PART VOCALS_RS2.xm

            MessageBox.Show("If track is ready, press OK to select and add it");
            btn_ChangeLyrics_Click(null, null);

        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text == "") return;
            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Groups=\"" + chbx_Group.Text + "\" and Type=\"DLC\";", "", cnb);
            var norec = drs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"DLC\";", "", cnb);
                DataSet dfs = new DataSet(); dfs = SelectFromDB("Groups", "SELECT MAX(Comments) FROM Groups WHERE Type=\"DLC\";", "", cnb);
                var index = dfs.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1;
                norec = ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string insertcmdA = "CDLC_ID, Profile_Name, Type, Comments, Groups,Date_Added";
                    var insertA = "\"" + fnn + "\",\"\",\"DLC\",\"" + index + "\",\"" + chbx_Group.Text + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"";
                    InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, 0);
                    UpdateGroups();
                    cmb_Filter.Items.Add("Group " + chbx_Group.Text);//add items
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Do you Wanna rename the Group?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) { DataSet dts = new DataSet(); dts = UpdateDB("Groups", "UPDATE Groups SET Profile_Name=\"" + chbx_Group.Text + "\" WHERE CDLC_ID=" + drs.Tables[0].Rows[0].ItemArray[0].ToString() + "and Type=\"DLC\";", cnb); }
                else MessageBox.Show("Please chose a unique name");
            }

        }

        void UpdateGroups()
        {
            //Create Groups list Dropbox
            var norec = 0;
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";", "", cnb);
            norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                if (chbx_Group.Items.Count > 0)//remove items
                {
                    chbx_Group.DataSource = null;
                    for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                    {
                        chbx_Group.Items.RemoveAt(k);
                        chbx_AllGroups.Items.RemoveAt(k);
                    }
                }

                for (int j = 0; j < norec; j++)//add items
                {
                    chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                    chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                }
            }

            DataSet dds = new DataSet(); dds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + databox.Rows[databox.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";", "", cnb);
            var nocrec = dds.Tables[0].Rows.Count;
            if (nocrec > 0)
            {
                for (int j = 0; j < nocrec; j++)
                {
                    var dfg = dds.Tables[0].Rows[j][0].ToString();
                    int index = chbx_AllGroups.Items.IndexOf(dfg);
                    if (index >= 0) chbx_AllGroups.SetItemChecked(index, true);
                }
            }
        }

        void ListSettings()
        {
            //Create Groups list Dropbox
            var norec = 0;
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Comments, Groups, ID FROM Groups WHERE Type=\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\"; ", "", cnb);
            norec = ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                if (chbx_Setting.Items.Count > 0)//remove items
                {
                    chbx_Setting.DataSource = null;
                    for (int k = chbx_Setting.Items.Count - 1; k >= 0; --k)
                        chbx_Setting.Items.RemoveAt(k);
                }

                for (int j = 0; j < norec; j++)//add items
                    chbx_Setting.Items.Add(ds.Tables[0].Rows[j][0].ToString() + "---"
                        + ds.Tables[0].Rows[j][1].ToString() + "+++" + ds.Tables[0].Rows[j][2].ToString());
            }
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\"", cnb);
            GroupChanged = true;
            UpdateGroups();
        }

        private void chbx_AllGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            GroupChanged = true;
        }

        private void chbx_Group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_GarageBand_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggdec.exe"),
                WorkingDirectory = AppWD
            };
            var t = txt_OggPath.Text;
            var tt = t.Replace(".ogg", ".wav");
            startInfo.Arguments = string.Format(" \"{0}\" ", t);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 2); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        string filePath = tt.Substring(0, tt.LastIndexOf("\\"));
                        try
                        {
                            Process process = Process.Start(@filePath);
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Garageband ready Song Folder in Exporer ! ");
                        }
                    }
                }
        }

        private void btn_Artist2SortA_Click(object sender, EventArgs e)
        {
            txt_Artist_Sort.Text = txt_Artist.Text;
        }

        private void btn_Title2SortT_Click(object sender, EventArgs e)
        {
            txt_Title_Sort.Text = txt_Title.Text;
        }

        private void chbx_Alternate_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbx_Alternate.Checked)
            {
                txt_Alt_No.Enabled = true;
                if (txt_Alt_No.Value == 0) txt_Alt_No.Value = 1;
            }
            else
                txt_Alt_No.Enabled = false;
        }

        private void btn_ChangeLyrics_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_Lyrics.Text != "" || !File.Exists(txt_Lyrics.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing Lyric! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Lyric XML file";
                fbd.Filter = "Rocksmith 2014 Lyrics file (*.xml)|*.xml";
                var i = databox.SelectedCells[0].RowIndex;
                fbd.InitialDirectory = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;

                ApplyLyric(fbd.FileName, databox.Rows[i].Cells["Folder_Name"].Value.ToString(), cnb,
                    txt_Platform.Text.ToString().Replace("PC", "Pc"), txt_ID.Text, chbx_Lyrics.Checked);
                txt_Lyrics.Text = fbd.FileName.IndexOf(databox.Rows[i].Cells["Folder_Name"].Value.ToString()) < 0 ? databox.Rows[i].Cells["Folder_Name"].Value.ToString() + "\\songs\\arr\\" + Path.GetFileName(fbd.FileName) : fbd.FileName;
                chbx_LyricsChanged.Checked = true;
                chbx_Lyrics.Checked = true;
                btn_ShowLyrics.Enabled = true;
                btn_CreateLyrics.Enabled = false;
                if (chbx_AutoSave.Checked) SaveRecord();
                ListTracks(txt_DuplicateOf.Text, txt_ID.Text, databox.Rows[i].Cells["Song_Lenght"].Value.ToString());
                chbx_ImprovedWithDM.Checked = true;
            }
        }

        static void ApplyLyric(string fn, string InitialDirectory, OleDbConnection cnb, string plTfrm, string cid, bool exislyr)
        {
            var tmp = "";
            if (fn.IndexOf(InitialDirectory) < 0)
            {
                File.Copy(fn, InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\arr\\" + Path.GetFileName(fn), true);
                tmp = InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\arr\\" + Path.GetFileName(fn);
            }
            else tmp = fn;

            var FileHash = "";
            FileHash = GetHash(tmp);//Generating the HASH code

            var outputFile = Path.Combine(Path.GetDirectoryName(tmp), string.Format("{0}.sng", Path.GetFileNameWithoutExtension(tmp)));

            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite))
            {
                Sng2014File sng = Sng2014File.ConvertXML(tmp, ArrangementType.Vocal);
                sng.WriteSng(outputStream, new Platform(plTfrm, GameVersion.RS2014.ToString()));
            }
            File.Copy(outputFile, InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\bin\\generic\\" + Path.GetFileName(outputFile), true);
            DeleteFile(outputFile);
            outputFile = InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\bin\\generic\\" + Path.GetFileName(outputFile);

            var SNGHash = "";
            SNGHash = GetHash(outputFile);

            DataSet dsr = new DataSet();
            var StartTime = GetTrackStartTime(tmp, RouteMask.None.ToString(), ArrangementType.Vocal.ToString());
            if (exislyr)
            {
                //in case update command is not working
                dsr = UpdateDB("Arrangements", "UPDATE Arrangements SET XMLFilePath=\"" + tmp + "\", XMLFile_Hash=\"" + FileHash + "\",JSONFilePath=\"" + outputFile + "\", SNGFileHash=\"" + SNGHash + "\", Comments= \"Comments added with DLCManager\", Start_Time= \"" + StartTime + "\" WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + cid + ";", cnb);
            }
            else
            {
                var insertcmdd = "Arrangement_Name,CDLC_ID, ArrangementType, Bonus, ArrangementSort, TuningPitch, RouteMask, Has_Sections, XMLFilePath, XMLFile_Hash, JSONFilePath, SNGFileHash, Comments, CleanedXML_Hash, Json_Hash, Start_Time";
                var insertvalues = "\"4\", " + cid + ", \"Vocal\", \"false\", \"0\", \"0\", \"None\",\"No\",\"" + tmp + "\",\"" + FileHash + "\",\"" + outputFile + "\",\"" + SNGHash + "\",\"" + "added with DLCManager " + fn + "\",\"" + SNGHash + "\",\"" + FileHash + "\",\"" + StartTime + "\"";
                InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0);
            }


        }

        private void btn_ApplyAlbumSortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Album_Short = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dus = UpdateDB("Standardization", cmd1 + ";", cnb);
            cmd1 = "UPDATE Main SET Album_ShortName = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dhj = UpdateDB("Main", cmd1 + ";", cnb);
        }

        private void btn_ReadGameLibrary_Click(object sender, EventArgs e)
        {
            // Process the list of files found in the directory.
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var logmss = "Starting... " + startT; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var pathx = txt_FTPPath.Text;
            UpdateDB("Main", "Update Main Set Remote_Path = \"\";", cnb); logmss = "Cleared Remote Songs... "; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            string[] fileEntries = new string[10000];
            if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") fileEntries = GetFTPFiles(pathx);
            else try { fileEntries = Directory.GetFiles(pathx, "*" + (chbx_Format.Text == "PC" ? "_p." : chbx_Format.Text == "Mac" ? "_m." : chbx_Format.Text == "XBOX360" ? "" : "") + "psarc*", SearchOption.TopDirectoryOnly); }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            var z = 0;

            var pack = GetMax("Pack_AuditTrail","Pack", cnb);

            var found = "\"";
            var newn = "\"";
            var norec = 0;
            var t = "";
            var j = 0;
            if (fileEntries != null)
            {
                foreach (string fileName in fileEntries) z++;
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Step = 1;
                pB_ReadDLCs.Maximum = z;
                z = 0;
                var tst = "";
                //Get (and SAve each New File Name in the FilesMissingIssues eXisting)
                foreach (string fileName in fileEntries)
                {
                    tst = pB_ReadDLCs.Value + "/" + z + "-" + Path.GetFileName(fileName);
                    pB_ReadDLCs.Increment(50);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    if (fileName == null) break;
                    if (fileName.IndexOf("s1compatibility") > 0)
                        continue;
                    z++; logmss = "Reading... " + fileName + "..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID FROM Pack_AuditTrail WHERE FileName=\"" + Path.GetFileName(fileName) + "\";", "", cnb);
                    norec = dfs.Tables[0].Rows.Count;
                    if (norec == 0) newn += fileName + "\";\"";
                    else
                    {
                        logmss = "Old song ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var dlcid = dfs.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (("-" + found).IndexOf("\"" + dlcid + "\"") <= 0)
                        {
                            found += dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(fileName) + "\" WHERE ID=" + dlcid + ";", cnb);
                            logmss = "Unique song the Remote location ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        else
                        {
                            var fn = "";
                            DataSet dfg = new DataSet(); dfg = SelectFromDB("Main", "SELECT ID,Remote_Path FROM Main WHERE ID=" + dlcid + ";", "", cnb);
                            DialogResult result1 = MessageBox.Show("Duplicate DLC has been found!\n\nChose which to imediatelly delete:\n\n1. " + dfg.Tables[0].Rows[0].ItemArray[1].ToString() + "\n\n2. " + Path.GetFileName(fileName) + "\n\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (result1 == DialogResult.Yes)
                                fn = dfg.Tables[0].Rows[0].ItemArray[1].ToString();
                            else if (result1 == DialogResult.No) fn = Path.GetFileName(fileName);
                            if (result1 == DialogResult.No || result1 == DialogResult.Yes)
                            {
                                if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")
                                {
                                    var FTPPath = "";
                                    FTPPath = txt_FTPPath.Text;
                                    DeleteFTPFiles(fn, FTPPath);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!File.Exists(txt_FTPPath.Text + "\"" + fn))
                                            File.Move(txt_FTPPath.Text + "\\" + fn, txt_FTPPath.Text + "\\" + fn.Replace(".psarc", ".dupli"));
                                    }
                                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                                }
                            }
                            logmss = "Duplicate song on the Remote location ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                    }
                }
                found = (found.Length > 2) ? found.Substring(0, found.Length - 2) : "";
                var newnn = (newn.Length > 2) ? newn.Substring(0, newn.Length - 2) : "";
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Maximum = newnn.Split(';').Length;
                if (newnn.Length > 0)
                {
                    newn = "";
                    for (j = 0; j < newnn.Split(';').Length; j++)
                    {
                        t = newnn.Split(';')[j].Replace("\"", "");

                        //Copy to decompress/import/FTP
                        var tt = "";
                        tt = c("dlcm_TempPath") + "\\..\\" + t;
                        string a = "ok";
                        var platform = tt.GetPlatform();
                        var platformTXT = tt.GetPlatform().platform.ToString();
                        logmss = "New song to the Library..." + pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " ...Starting additional/metadata based checks" + t + "...."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU" || platformTXT == "PS3")
                        {
                            if (!File.Exists(tt))
                                if (tt.IndexOf("�") < 1 && tt.IndexOf("?") < 1 && tt.IndexOf("rs1compatibilitydisc.psarc.edat") < 1 && tt != null)
                                    a = CopyFTPFile(Path.GetFileName(t), tt, txt_FTPPath.Text);
                            if (a == "ok")
                            {
                                var unpackedDir = "";
                                DLCPackageData info = null;
                                try
                                {
                                    logmss = "Start Unpacck & Read ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    unpackedDir = Packer.Unpack(tt, c("dlcm_TempPath"), SourcePlatform, true, true);
                                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform);
                                    logmss = "Stop  Unpack&Read ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                                    //Generating the HASH code
                                    var FileHash = ""; FileHash = GetHash(tt);
                                    DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + info.Name + "\";", "", cnb);//+ song.Identifier + "\";");
                                    DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + info.SongInfo.SongDisplayName + "\";", "", cnb); //song.Title
                                    DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);
                                    DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + info.Name.Substring(5, info.Name.Length - 5) + "\";", "", cnb);
                                    DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"%" + info.Name.Substring(5, info.Name.Length - 5) + "%\";", "", cnb);
                                    DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"%" + info.SongInfo.SongDisplayName + "%\";", "", cnb);
                                    noreca = dfa.Tables[0].Rows.Count; norecb = dfb.Tables[0].Rows.Count; norecc = dfc.Tables[0].Rows.Count; norecd = dfd.Tables[0].Rows.Count; norece = dfe.Tables[0].Rows.Count; norecf = dff.Tables[0].Rows.Count;
                                    DataSet fxd = new DataSet();
                                    if (norecc == 1) fxd = dfc;
                                    else if (noreca == 1) fxd = dfa;
                                    else if (norecb == 1) fxd = dfb;
                                    else if (norecf == 1) fxd = dff;
                                    else if (norecd == 1) fxd = dfd;
                                    else if (norece == 1) fxd = dfe;
                                    logmss = "Stop check based on metadata ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (noreca == 1 || norecb == 1 || norecc == 1 || norecd == 1 || norece == 1 || norecf == 1)
                                    {
                                        System.IO.FileInfo fi = null; //calc file size
                                        try { fi = new System.IO.FileInfo(tt); }
                                        catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

                                        if (("-" + found).IndexOf(fxd.Tables[0].Rows[0].ItemArray[0].ToString()) <= 0)
                                        {
                                            var fnn = t;

                                            var i = 0;
                                            string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack";
                                            var fnnon = Path.GetFileName(fnn);
                                            var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                            var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + info.Name + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\",\"" + pack + "\"";
                                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                                            found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb);
                                            DeleteFile(tt);
                                            logmss = "Stop adding song based on newly read metadata..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        }
                                        else
                                        {
                                            var fn = "";
                                            DataSet dfg = new DataSet(); dfg = SelectFromDB("Main", "SELECT ID,Remote_Path FROM Main WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", "", cnb);
                                            DialogResult result1 = MessageBox.Show("Duplicate DLC has been found!\n\nChose which to imediatelly delete:\n\n1. " + dfg.Tables[0].Rows[0].ItemArray[1].ToString() + "2. " + Path.GetFileName(t) + "\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                            if (result1 == DialogResult.Yes)
                                                fn = dfg.Tables[0].Rows[0].ItemArray[1].ToString();
                                            else if (result1 == DialogResult.No) fn = Path.GetFileName(t);
                                            if (result1 == DialogResult.No || result1 == DialogResult.Yes)
                                            {
                                                if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")
                                                {
                                                    var FTPPath = txt_FTPPath.Text;// c("dlcm_FTP" + c("dlcm_FTP"));
                                                    DeleteFTPFiles(fn, FTPPath);
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        if (!File.Exists(txt_FTPPath.Text + "\"" + fn))
                                                            File.Move(txt_FTPPath.Text + "\"" + fn, txt_FTPPath.Text + "\"" + fn.Replace(".psarc", ".dupli"));
                                                    }
                                                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                                                }
                                            }

                                            logmss = "Stop identifying old song based on newly read metadata..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        }
                                    }
                                    else newn += t + "\";";
                                    DeleteDirectory(unpackedDir);
                                }
                                catch (Exception ee)
                                {
                                    logmss = "Error ar Unpacck Or song Read/Load ..." + ee; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    newn += t + "\";\"";
                                }
                                pB_ReadDLCs.Increment(1);
                            }
                            else newn += t + "\";\"";
                        }
                        else
                        {
                            logmss = "Read non PS3 song ..." + pB_ReadDLCs.Value + " / " + pB_ReadDLCs.Maximum + "..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            if (!File.Exists(tt)) try { File.Copy(t, tt, true); } catch (Exception ee) { MessageBox.Show("4020 " + ee.Message); }

                            //quickly read PSARC for basic data
                            var browser = new PsarcBrowser(tt);
                            var songlist = browser.GetSongList();
                            var toolkitInfo = browser.GetToolkitInfo();
                            foreach (var song in songlist)
                            {
                                var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                                //Generating the HASH code
                                var FileHash = ""; FileHash = GetHash(tt);
                                logmss = "Checking non PS3 against metadata..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + song.Identifier + "\";", "", cnb);
                                DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + song.Title + "\";", "", cnb);
                                DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);
                                DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + song.Identifier.Substring(5, song.Identifier.Length - 5) + "\";", "", cnb);
                                DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"%" + song.Identifier.Substring(5, song.Identifier.Length - 5) + "%\";", "", cnb);
                                DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"%" + song.Title + "%\";", "", cnb);
                                noreca = dfa.Tables[0].Rows.Count; norecb = dfb.Tables[0].Rows.Count; norecc = dfc.Tables[0].Rows.Count; norecd = dfd.Tables[0].Rows.Count; norece = dfe.Tables[0].Rows.Count; norecf = dff.Tables[0].Rows.Count;
                                DataSet fxd = new DataSet();
                                if (norecc == 1) fxd = dfc;
                                else if (noreca == 1) fxd = dfa;
                                else if (norecb == 1) fxd = dfb;
                                else if (norecf == 1) fxd = dff;
                                else if (norecd == 1) fxd = dfd;
                                else if (norece == 1) fxd = dfe;
                                if (noreca == 1 || norecb == 1 || norecc == 1 || norecd == 1 || norece == 1 || norecf == 1)
                                {
                                    logmss = "Adding non PS3 song ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    //calc file size
                                    System.IO.FileInfo fi = null;
                                    try { fi = new System.IO.FileInfo(tt); }
                                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

                                    var fnn = t;
                                    string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack";
                                    var fnnon = Path.GetFileName(fnn);
                                    var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                    var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + song.Identifier + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\",\"" + pack + "\"";
                                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                                    found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                    DataSet dxr = new DataSet(); UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb);
                                    try { DeleteFile(tt); }
                                    catch (Exception ex)
                                    {
                                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                        MessageBox.Show("4067 " + ex.Message);
                                    }
                                }
                                else newn += t + "\";\"";
                            }
                            pB_ReadDLCs.Increment(1);
                        }
                    }
                }

                if (found.Length >= 2) found = found.Substring(0, found.Length - 2) == ";" ? found.Substring(0, found.Length - 1) : found;
                else found = "\"0\"";
                if (newn.Length >= 2) newn = newn.Length > 2 ? newn.Substring(0, newn.Length - 2) : "";

                SearchCmd = "SELECT * FROM Main u WHERE ";
                SearchCmd += "CSTR(u.ID) IN (" + found.Replace(";", ",") + ") ORDER BY Remote_Path";
                chbx_Replace.Enabled = true;
            }
            else SearchCmd = "SELECT * FROM Main u";// 1 =1";
            MessageBox.Show("Song Recognized/Read " + found.Replace(";", "\n;").Split(';').Length + "/" + z + "\n\nfrom " + txt_FTPPath.Text + "\n\nUnrecognized:\n" + newn);
            if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ";
            try
            {
                databox.DataSource = null; //Then clear the rows:
                databox.Rows.Clear();//                Then set the data source to the new list:
                dssx.Dispose();
                Populate(ref databox, ref Main);
                databox.Refresh();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show("4098 " + ex.Message + "Can't run Filter ! " + SearchCmd);
            }
            cmb_Filter.Text = "Songs in Rocksmith Game Lib";
            logmss = "Done Reading the Library ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_RemoveRemoteSong_Click(object sender, EventArgs e)
        {
            var outp = "";
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\" WHERE ID=" + txt_ID.Text + ";", cnb);
            if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")
            {
                var FTPPath = txt_FTPPath.Text;
                outp = DeleteFTPFiles(txt_RemotePath.Text, FTPPath);
            }
            else
            {
                try
                {
                    var tg = (c("dlcm_RocksmithDLCPath") + "\\" + txt_RemotePath.Text).Replace(".psarc", ".dupli");
                    if (File.Exists(c("dlcm_RocksmithDLCPath") + "\\" + txt_RemotePath.Text))
                        File.Move(c("dlcm_RocksmithDLCPath") + "\\" + txt_RemotePath.Text, tg);
                    outp = "ok";
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            txt_RemotePath.Text = "";
            MessageBox.Show("Removed " + outp);
        }

        private void btn_RemoveAllRemoteSongs_Click(object sender, EventArgs e)
        {
            //GetDirList and calcualte hash for the IMPORTED file
            string[] filez;
            if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")
            {
                filez = System.IO.Directory.GetFiles(c("dlcm_RocksmithDLCPath"), (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    var FTPPath = txt_FTPPath.Text;
                }

                filez = System.IO.Directory.GetFiles(c("dlcm_RocksmithDLCPath"), (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
            }
            else
            {
                filez = System.IO.Directory.GetFiles(c("dlcm_RocksmithDLCPath"), (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    if (s == "rs1compatibilitydisc_m.psarc" || s == "rs1compatibilitydisc_p_Pc.psarc" || s == "rs1compatibilitydlc_p.psarc" || s == "rs1compatibilitydlc_m.psarc") continue;
                    try
                    {
                        var tg = (c("dlcm_RocksmithDLCPath") + "\\" + s).Replace(".psarc", ".dupli");
                        if (File.Exists(c("dlcm_RocksmithDLCPath") + "\\" + s))
                            File.Move(c("dlcm_RocksmithDLCPath") + "\\" + s, tg);
                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }

            }
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\";", cnb);
            MessageBox.Show("All Remote-s songs have been deleted");
        }

        private void btn_Find_FilesMissingIssues_Click(object sender, EventArgs e)
        {

            //check wem bitrate
            //note some ogg to wem recopressions raise the textual bitrate
            if (c("dlcm_AdditionalManipul83") == "Yes")
            {
                MessageBox.Show("Note some WEM connv raises the bitrate of the audio(but not the file size) so some wems will always get renncoded when checking");
                var sel = "SELECT AudioPath, audioPreviewPath, OggPath, oggPreviewPath, audioBitrate, ID FROM Main WHERE"
                    + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY Spotify_Artist_ID ASC";
                DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Standardization", sel, "", cnb);
                var noOfRecs = SongRecord.Tables[0].Rows.Count;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;
                var j = 0;
                for (var i = 0; i < noOfRecs; i++)
                {
                    var paath = c("dlcm_MediaInfo_CLI");
                    var xx = "";
                    if (File.Exists(paath)) xx = paath;
                    else xx = Path.Combine(AppWD, "MediaInfo_CLI_17.12_Windows_x64", "MediaInfo.exe");
                    if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install MediaInfo CLI if you want to use it.", c("dlcm_MediaInfo_CLI_www"), "Missing MediaInfo CLI", false, false, true, "", "", ""); frm1.ShowDialog(); break; }

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = xx,
                        WorkingDirectory = AppWD
                    };
                    var t = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                    startInfo.Arguments = string.Format(" --Inform=Audio;%BitRate% \"{0}\"", t);
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;

                    if (File.Exists(t))
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo;
                            DDC.Start();
                            string stdoutx = DDC.StandardOutput.ReadToEnd();
                            string stderrx = DDC.StandardError.ReadToEnd();
                            DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            if (stdoutx != "\r\n") if (float.Parse(stdoutx.Replace("\r\n", ""), NumberStyles.Float, CultureInfo.CurrentCulture) > float.Parse(c("dlcm_MaxBitRate"), NumberStyles.Float, CultureInfo.CurrentCulture))
                                {
                                    //Fix Bitrate
                                    var cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "";
                                    FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "MainDB");

                                    j++; timestamp = UpdateLog(timestamp, SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "_" + j + "/" + i + "/" + noOfRecs + " bitrate " + stdoutx.Replace("\r\n", ""), true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }

                        }
                    //pB_ReadDLCs.Maximum = noOfRecs;
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing 0_data folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }


            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Increment(1);
            var o = 1;
            timestamp = UpdateLog(timestamp, o + "/5 Check existing 0_data folders", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Check existing 0_data folders
            string filePath = c("dlcm_TempPath") + "\\";
            var dirs = from dir in
                     Directory.EnumerateDirectories(filePath, "*")
                       select dir;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = dirs.Count();
            foreach (string s in dirs)
            {
                if (s.IndexOf("0_old") <= 0 && s.IndexOf("0_repacked") <= 0 && s.IndexOf("0_duplicate") <= 0 && s.IndexOf("0_dlcpacks") <= 0 && s.IndexOf("0_broken") <= 0 && s.IndexOf("0_albumCovers") <= 0 && s.IndexOf("0_log") <= 0 && s.IndexOf("0_archive") <= 0 && s.IndexOf("0_data") <= 0 && s.IndexOf("0_temp") <= 0 && s.IndexOf("0_to_import") <= 0)
                {
                    var cmd = "SELECT * FROM Main WHERE Folder_Name=\"" + s + "\" ORDER BY ID DESC;";
                    DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, "", cnb);
                    var noOfFlds = 0;
                    if (drs.Tables.Count > 0) noOfFlds = drs.Tables[0].Rows.Count;
                    var oldvl = c("dlcm_AdditionalManipul81");
                    ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = "Yes";
                    if (noOfFlds == 0) DeleteDirectory(s);
                    ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing 0_data folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }

            o++; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Value = 0; timestamp = UpdateLog(timestamp, o + "/4 check Spotify Album art file in Standardisation", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //check Spotify Album art file in Standardisation
            var cmdS = "SELECT ID,SpotifyAlbumPath FROM Standardization WHERE SpotifyAlbumPath=\"\" OR SpotifyAlbumPath=Null; ";
            DataSet dns = new DataSet(); dns = SelectFromDB("Standardization", cmdS, "", cnb);

            var noOfArrS = 0;
            noOfArrS = dns.Tables[0].Rows.Count;
            var IDs = "0"; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfArrS;
            for (var k = 0; k < noOfArrS; k++)
            {
                try
                {
                    var FN = dns.Tables[0].Rows[k].ItemArray[1].ToString();
                    var ID = dns.Tables[0].Rows[k].ItemArray[0].ToString();
                    if (!File.Exists(FN)) IDs += "," + ID + "";
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "check Spotify Album art file in Standardisation", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            if (noOfArrS != 0)
            {
                var cmdupdS = "UPDATE Standardization Set SpotifyAlbumPath =\"\" WHERE ID IN (" + IDs + ")";
                DataSet dfs = new DataSet(); dfs = UpdateDB("Standardization", cmdupdS + ";", cnb);
            }

            o++; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/4 check Groups for deleted songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //check Groups for deleted songs
            var cmdz = "SELECT * FROM Groups WHERE ID IN (SELECT F.ID FROM Groups as F left JOIN (SELECT ID FROM Main) as G ON G.ID=val(F.CDLC_ID) WHERE F.Type=\"DLC\" and G.ID is Null)";
            DataSet dnz = new DataSet(); dnz = SelectFromDB("Groups", cmdz, "", cnb);
            var noOfArrZ = dnz.Tables.Count <= 0 ? 0 : dnz.Tables[0].Rows.Count;
            cmdz = "DELETE * FROM " + cmdz + "";
            if (noOfArrZ > 0)
            {
                dnz = UpdateDB("Groups", cmdz.Replace("SELECT * FROM ", "") + ";", cnb);
                MessageBox.Show("Cleanned the Groups table of " + noOfArrZ + " deleted songs");
            }

            o++; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/3 check Groups for deleted songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //cleanup before checks
            var cmdupd = "UPDATE Main Set FilesMissingIssues =\"\" WHERE"
                    + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")";
            DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb);

            DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main WHERE"
                    + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")", "", cnb);
            var noOfRec = dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
            var vFilesMissingIssues = "";
            //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRec;
            var MissingPSARC = false;
            var cnt = 0; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRec;
            for (var i = 0; i < noOfRec; i++)
            {
                vFilesMissingIssues = ""; pB_ReadDLCs.Value = i;

                var ID = dms.Tables[0].Rows[i].ItemArray[0].ToString();
                var AlbumArtPath = dms.Tables[0].Rows[i].ItemArray[10].ToString();
                var AudioPath = dms.Tables[0].Rows[i].ItemArray[11].ToString();
                var AudioPreviewPath = dms.Tables[0].Rows[i].ItemArray[12].ToString();
                var OggPath = dms.Tables[0].Rows[i].ItemArray[77].ToString();
                var OggPreviewPath = dms.Tables[0].Rows[i].ItemArray[78].ToString();
                var OrigFileName = dms.Tables[0].Rows[i].ItemArray[19].ToString();
                var hasOld = dms.Tables[0].Rows[i].ItemArray[87].ToString();
                var hasPrev = dms.Tables[0].Rows[i].ItemArray[43].ToString();
                var hasCov = dms.Tables[0].Rows[i].ItemArray[42].ToString();
                var FileH = dms.Tables[0].Rows[i].ItemArray[24].ToString();


                //check if File exists in  Pack Audit Trail
                var cmd = "SELECT PackPath+'\\'+FileName,ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileH + "\" ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                DataSet dws = new DataSet(); dws = SelectFromDB("Pack_AuditTrail", cmd, "", cnb);

                var noOfArrs = 0;
                if (dws.Tables.Count > 0) noOfArrs = dws.Tables[0].Rows.Count;
                if (noOfArrs == 0)
                {
                    vFilesMissingIssues += " PackAuditTrail missing ";
                    var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Official, Reason, Pack";
                    var insertA = "Select top 1 i.Import_Path+\"\\\"+Original_FileName" + ", \"" + c("dlcm_TempPath") + "\\0_old" + "\", i.Original_FileName, i.File_Creation_Date, i.File_Hash, i.File_Size," +
                        " ID, DLC_Name, Platform, Is_Original, \"Added by consistency checks\", i.Pack FROM Main as i WHERE i.File_Hash = \"" + FileH + "\"";
                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                }
                //check if File exists in Import Audit Trail
                cmd = "SELECT FullPath,ID FROM Import_AuditTrail WHERE FileHash=\"" + FileH + "\" ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                DataSet dqs = new DataSet(); dqs = SelectFromDB("Pack_AuditTrail", cmd, "", cnb);

                var noOfArrss = 0;
                if (dqs.Tables.Count > 0) noOfArrss = dqs.Tables[0].Rows.Count;
                if (noOfArrss == 0)
                {
                    vFilesMissingIssues += " Import_AuditTrail missing ";
                    string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";
                    var insertA = "Select i.Import_Path+\"\\\"+Original_FileName" + ", \"" + c("dlcm_RocksmithDLCPath") + "\", i.Original_FileName, i.File_Creation_Date, i.File_Hash, i.File_Size, i.Import_Date, i.Pack FROM Main as i WHERE " +
                        " i.File_Hash = \"" + FileH + "\"";
                    InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA, cnb, 0);
                }
                //check Arrangements
                cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + ";";
                DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", cmd, "", cnb);


                var noOfArr = 0;
                noOfArr = dss.Tables[0].Rows.Count;
                if (noOfArr == 0)
                    vFilesMissingIssues = "No Arrangements!!!";
                //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfArr;
                for (var k = 0; k < noOfArr; k++)
                {
                    try
                    {
                        var ms1 = dss.Tables[0].Rows[k].ItemArray[4].ToString(); //JSONFilePath
                        var ms2 = dss.Tables[0].Rows[k].ItemArray[5].ToString();//XMLFilePath
                        var ms3 = dss.Tables[0].Rows[k].ItemArray[26].ToString(); //XMLFileName
                        string ms4 = "";
                        if (ms2.IndexOf("\\Mac") >= 0) ms4 = ms2.Replace("\\arr\\", "\\bin\\macos\\");
                        else if (ms2.IndexOf("\\PS3") >= 0) ms4 = ms2.Replace("\\arr\\", "\\bin\\ps3\\");
                        else ms4 = ms2.Replace("\\arr\\", "\\bin\\generic\\");
                        ms4 = ms4.Substring(0, ms4.LastIndexOf("\\") + 1) + dss.Tables[0].Rows[k].ItemArray[29].ToString() + ".sng"; //SNGFilePathms4.Length - ms4.LastIndexOf("\\")-1
                        if (!File.Exists(ms1) && (ms3.LastIndexOf("showlights") < 1))
                            vFilesMissingIssues += " JSON " + ms1 + "; "; //showlights
                        if (!File.Exists(ms2))
                            vFilesMissingIssues += " XML " + ms3 + "; ";
                        if ((ms3 != "" && dss.Tables[0].Rows[k].ItemArray[29].ToString() != "") && !File.Exists(ms4) && (ms3.LastIndexOf("showlights") < 1))
                            vFilesMissingIssues += " SNG " + ms4 + "; ";
                        //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done   
                        //pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check arrangements", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    catch (Exception ex) { var tsst = "Error at file existence ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }

                //check DB Duplicates and oldies 
                cmd = "SELECT PackPath+'\\'+FileName,ID FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                DataSet drs = new DataSet(); drs = SelectFromDB("Pack_AuditTrail", cmd, "", cnb);

                noOfArr = 0;
                if (drs.Tables.Count > 0) noOfArr = drs.Tables[0].Rows.Count;
                else Console.Write("");

                //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfArr;
                for (var k = 0; k < noOfArr; k++)
                {
                    try
                    {
                        var ms1 = drs.Tables[0].Rows[k].ItemArray[0].ToString();
                        if (!File.Exists(ms1))
                        {
                            //vFilesMissingIssues += " oldie " + ms1 + "; ";
                            DataSet dnr = new DataSet(); dnr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set Reason = 'Missing PSARC' WHERE ID=" + drs.Tables[0].Rows[k].ItemArray[1].ToString() + ";", cnb);
                            MissingPSARC = true;
                        }
                        //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done 
                        //pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check Pack_AuditTrail", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }
                if (vFilesMissingIssues.ToLower().IndexOf("sunshine") >= 0)
                    Console.Write("");
                //check the files Duplicates and oldies 


                //pB_ReadDLCs.Increment(1);
                var old = c("dlcm_TempPath") + "\\0_old\\" + OrigFileName;
                if (!File.Exists(AlbumArtPath) && hasCov == "Yes")
                    vFilesMissingIssues += " AlbumArtPath; ";
                if (!File.Exists(AudioPath))
                    vFilesMissingIssues += " AudioPath; ";
                if (!File.Exists(AudioPreviewPath) && hasPrev == "Yes")
                    vFilesMissingIssues += " AudioPreviewPath; ";
                if (!File.Exists(OggPath))
                    vFilesMissingIssues += " OggPath; ";
                if (!File.Exists(OggPreviewPath) && hasPrev == "Yes")
                    vFilesMissingIssues += " OggPreviewPath; ";
                if (!File.Exists(old) && hasOld == "Yes")
                    vFilesMissingIssues += " old; ";
                DataSet dxr = new DataSet(); if (vFilesMissingIssues != "")
                {
                    dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"Yes\", FilesMissingIssues = \"" + vFilesMissingIssues + "\" WHERE ID=" + ID + "", cnb);
                    cnt++;
                }
            }

            if (cnt > 0)
            {
                SearchCmd = SearchCmd.Replace("ORDER BY", "WHERE FilesMissingIssues <> '' ORDER BY");
                if (MissingPSARC) MessageBox.Show("Please note that Pack_AuditTrail has unmatched records. Manaully decide if they have to be deleted based on Reason field.");
                if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ";

                cmb_Filter.Text = "Show Songs with FilesMissing Issues";/* (vFilesMissingIssues.IndexOf(";") > 0 && vFilesMissingIssues.Length > 1 ? "s" : "")*/
                MessageBox.Show("Song" + cnt + " with FilesMissing Issues identified");
            }
            else
                MessageBox.Show("No Songs with FilesMissing Issues identified");
        }

        private void chbx_CopyOld_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_CopyOld.Checked)
            {
                chbx_UniqueID.Enabled = false;
                chbx_RemoveBassDD.Enabled = false;
                chbx_Last_Packed.Enabled = false;
            }
            else
            {
                chbx_UniqueID.Enabled = true;
                chbx_RemoveBassDD.Enabled = true;
                chbx_Last_Packed.Enabled = true;
            }

        }

        private void btn_Remove_All_Packed_Click(object sender, EventArgs e)
        {
            try
            {
                CleanFolder(c("dlcm_TempPath") + "\\0_repacked\\PC", "", false, false, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                CleanFolder(c("dlcm_TempPath") + "\\0_repacked\\PS3", "", false, false, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                CleanFolder(c("dlcm_TempPath") + "\\0_repacked\\MAC", "", false, false, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                CleanFolder(c("dlcm_TempPath") + "\\0_repacked\\XBOX360", "", false, false, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\";", cnb);
            MessageBox.Show("All repacked songs have been deleted");
        }

        private void chbx_Copy_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbx_Copy.Checked)
            {
                chbx_CopyOld.Checked = false;
                chbx_Replace.Checked = false;
                chbx_Last_Packed.Checked = false;
            }
        }

        private void btn_RemoveBrakets_Click(object sender, EventArgs e)
        {
            var a = txt_Title.Text;
            if (a.IndexOf("[") > 0)
            {
                var b = a.Substring(a.IndexOf("["), a.IndexOf(")") - a.IndexOf("[") + 1);
                txt_Title.Text = a.Replace(b, "").Trim();
            }
            var i = databox.SelectedCells[0].RowIndex;
            if (txt_Title.Text != databox.Rows[i].Cells["Song_Title"].Value.ToString())
                chbx_Has_Been_Corrected.Checked = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath like \"%0_duplicate%\"";
            var tcmd = "SELECT PackPath+\"\\\"+FileName ";
            if (chbx_Ignore_Officials.Checked)
            {
                cmd += " AND Official=\"No\";";
                tcmd = "SELECT PackPath+\"\\\"+FileName " + cmd;
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb);
                var rec = dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString());
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb);
            MessageBox.Show("All Duplicates songs have been deleted");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath+\"\\\"+FileName = \"" + cmb_Packed.Text + "\"";
            DeleteFile(cmb_Packed.Text);
            cmb_Packed.Items.RemoveAt(cmb_Packed.SelectedIndex);
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb);
        }

        private void cmb_Packed_SelectedValueChanged(object sender, EventArgs e)
        {
            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT Official FROM Pack_AuditTrail WHERE PackPath +\"\\\"+FileName = \"" + cmb_Packed.Text + "\"", "", cnb);
            var rec = dvr.Tables[0].Rows.Count;
            if (rec == 1) chbx_Duplicate_Official.Checked = true;
        }


        public async void btn_Debug_ClickAsync(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var CleanTitle = "";
            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            if (txt_Title.Text.IndexOf(")") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf(")"), txt_Title.Text.Length - txt_Title.Text.IndexOf(")"));
            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

            string z = (GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, CleanTitle, txt_Album_Year.Text, txt_SpotifyStatus.Text, timestamp)).ToString();
            txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
        }

        public static async System.Threading.Tasks.Task<int> GetTrackNoFromSpotifyAsync(string Artist, string Album, string Title, string Year, string Status, DateTime timestamp)
        {
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection
            {
                { "query", keywordString }
            };
            var a1 = "";
            debug = "";
            try
            {
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            a1 = a1.Trim();
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
        }

        private async Task ActivateSpotify_ClickAsync()
        {
        }

        public async void InitialSetup()
        {

            if (netstatus == "OK")
            {
                if (InvokeRequired)
                {
                    Invoke(new System.Action(InitialSetup));
                    return;
                }

                txt_SpotifyStatus.Text = "All good";
            }
        }

        public HttpClient _client;
        public void btn_ActivateSpotify_Click(object sender, EventArgs e)
        {
            if (c("dlcm_AdditionalManipul82") == "Yes")
            {
                DialogResult result3 = MessageBox.Show("As selected by option 41 Tool will connect to Spotify to retrieve Track No, album covers, Year information, etc.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            ActivateSpotify_ClickAsync();
            SpotifyMain();


            //SpotifyWebBuilder _builder;
            //_builder = new SpotifyWebBuilder();
        }

        void SpotifyMain()
        {
        }

        private void btn_GetTrack_Click(object sender, EventArgs e)
        {

            var trackno = 0;
            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            var sel = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"0\" OR Track_No = \"00\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb);
            var noOfRec = SongRecord.Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

            if (netstatus == "OK" || noOfRec > 0) txt_Track_No.Value = FixWithSpotifyDetails(SongRecord, 0, noOfRec, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp);
        }

        private async Task RequestToGetInputReport(string Artist, string Album, string Title, string Year, string Status, DateTime timestamp)
        {
            // lots of code prior to this
            int bytesRead = await GetTrackNoFromSpotifyAsync(Artist, Album, Title, Year, Status, timestamp);
            txt_Track_No.Text = bytesRead.ToString() == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text.ToInt32().ToString("D2") : bytesRead.ToString("D2");
            rtxt_StatisticsOnReadDLCs.Text = debug;
        }

        private void bth_GetTrackNo_Click(object sender, EventArgs e)
        {
            btn_GetTrack_Click(sender, e);
        }

        private void btn_Copy_Orig_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            DataSet dhs = new DataSet();
            var cmd = "SELECT Original_FileName,Available_Old, ID FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            dhs = SelectFromDB("Main", cmd, "", cnb);
            noOfRec = dhs.Tables[0].Rows.Count;
            //var dest = "";
            pB_ReadDLCs.Maximum = noOfRec;
            var missing = ""; var dest = "";
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var filename = dhs.Tables[0].Rows[i].ItemArray[0].ToString();
                string filePath = c("dlcm_TempPath") + "\\0_old\\" + filename;
                if (!File.Exists(c("dlcm_RocksmithDLCPath") + "\\" + filename)) dest = c("dlcm_RocksmithDLCPath") + "\\" + filename;
                else dest = c("dlcm_TempPath") + "\\" + filename;
                if (dhs.Tables[0].Rows[i].ItemArray[1].ToString() == "Yes")
                {
                    try
                    {
                        File.Copy(filePath, dest, true);
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ex);
                    }
                }
                else missing += " ; " + dhs.Tables[0].Rows[i].ItemArray[2].ToString();
                pB_ReadDLCs.Value++;
            }
            MessageBox.Show("All Filtered Old/Iinitially imported File Copied to " + Path.GetPathRoot(dest) + "\\ except: " + missing + " where Original is mising");
        }

        public void btn_FixAudioAll_Click(object sender, EventArgs e)
        {
            var cancel = false;
            if (btn_FixAudioAll.Text != "Fix Audio Issues") { cancel = true; btn_FixAudioAll.Text = "Fix Audio Issues"; }
            else btn_FixAudioAll.Text = "Cancel Fix Audio";
            btn_FixAudioAll.Text = "Fix Audio Issues";

            FixAudioAll_Click(netstatus, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, SearchCmd, SearchFields, logPath, timestamp);

            Populate(ref databox, ref Main);
            databox.Refresh();
            pB_ReadDLCs.CreateGraphics().DrawString("Done Fixing Audio Issues for this song.", new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            btn_FixAudioAll.Text = "Fix Audio Issues";
        }

        public static void FixAudioAll_Click(string netstatus, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs
            , RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string SearchCmd, string SearchFields, string logPath, DateTime timestamp)
        {

            var cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main " +
                "WHERE FilesMissingIssues=\"\" AND Has_Preview=\"No\" AND Is_Broken<>\"Yes\"" +
                "" + (c("dlcm_AdditionalManipul55").ToLower() != "yes" ? "" :
                " OR (VAL(PreviewLenght) > " + float.Parse(c("dlcm_MaxPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture) + ")" +
                (c("dlcm_AdditionalManipul88").ToLower() != "yes" ? "" :
                " OR (VAL(PreviewLenght) < " + float.Parse(c("dlcm_MinPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture)) + ")");
            FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB");

            cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, oggPath, oggPreviewPath FROM Main" +
                " WHERE FilesMissingIssues=\"\" AND (VAL(audioBitrate) > "
                + (c("dlcm_MaxBitRate")) + " or VAL(audioSampleRate) > " + (c("dlcm_MaxSampleRate")) + ") AND "
                + "ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") AND Is_Broken<>\"Yes\"";
            FixAudioIssues(cmd.Replace("; ", ""), cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB");

            //Clean Temp folder of Audio files
            foreach (var fil in Directory.GetFiles(c("dlcm_TempPath") + "\\0_temp"))

            {
                if (fil.IndexOf(".wav") > 0 || fil.IndexOf(".ogg") > 0 || fil.IndexOf(".wem") > 0)
                {
                    DeleteFile(fil);
                }
            }

            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            if (netstatus == "OK")
            {
                var sels = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"00\" OR Track_No = \"0\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY ID ASC";
                DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sels, "", cnb);
                var noOfRecs = SongRecord.Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;

                for (var i = 0; i < noOfRecs; i++)
                {
                    pB_ReadDLCs.Increment(1);
                    FixWithSpotifyDetails(SongRecord, i, noOfRecs, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp);
                }
                SongRecord.Dispose();
            }
            //fix missing youtube details
            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK" && c("dlcm_youtubestatus") == "OK")
            {
                var sel = "SELECT * FROM Main WHERE (YouTube_Link = \"\" OR Youtube_Playthrough = \"\" or YouTube_Link is null OR Youtube_Playthrough is null) "
               + "AND ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY ID ASC";
                //Read from DB
                MainDBfields[] SongRecords = new MainDBfields[10000];
                SongRecords = GetRecord_s(sel, cnb);
                var noOfRec = 0;
                foreach (var filez in SongRecords)
                {
                    if (filez.Artist == null) break;
                    noOfRec++;
                }
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec;
                var j = 0;
                foreach (var filez in SongRecords)
                {
                    if (filez.Artist == null) break;
                    if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();
                    if (netstatus == "OK" && c("dlcm_youtubestatus") == "OK") GetYoutubeDetailsAsync(SongRecords[j], j, cnb, pB_ReadDLCs, "MainDB");
                    j++;
                }
            }

        }

        public static int FixWithSpotifyDetails(DataSet SongRecord, int i, int noOfRec, OleDbConnection cnb, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, string windw, DateTime timestamp)
        {
            int trackno = 0;
            try
            {
                Task<string> sptyfy = null;
                if (c("dlcm_AdditionalManipul87") == "Yes")
                    sptyfy = StartToGetSpotifyDetails(SongRecord.Tables[0].Rows[i].ItemArray[3].ToString(), SongRecord.Tables[0].Rows[i].ItemArray[2].ToString(), SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(), SongRecord.Tables[0].Rows[i].ItemArray[4].ToString(), "");
                else return 0;
                trackno = sptyfy.Result.Split(';')[0].ToInt32();
                var SpotifySongID = sptyfy.Result.Split(';')[1];
                var SpotifyArtistID = sptyfy.Result.Split(';')[2];
                var SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                var SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                var SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                var Year_Correction = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";


                if (trackno > 0 || (SpotifySongID != "" && SpotifySongID != null))
                {
                    var cmds = "UPDATE Standardization SET ";
                    cmds += " SpotifyAlbumID=\"" + SpotifyAlbumID + "\"" + ", SpotifyAlbumURL=\"" + SpotifyAlbumURL + "\"" + ", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\", Year_Correction=\"" + Year_Correction + "\"";
                    cmds += " WHERE (SpotifyAlbumID=\"-\" OR SpotifyAlbumID=\"\" OR SpotifyAlbumID is null) AND (Album=\""
                        + SongRecord.Tables[0].Rows[i].ItemArray[2].ToString() + "\" OR Album_Correction=\"" + SongRecord.Tables[0].Rows[i].ItemArray[2].ToString() + "\") AND (Artist=\""
                        + SongRecord.Tables[0].Rows[i].ItemArray[3].ToString() + "\" OR Artist_Correction=\"" + SongRecord.Tables[0].Rows[i].ItemArray[3].ToString() + "\")";
                    DataSet dis = new DataSet();
                    if (trackno > 0 && SpotifySongID != "" && SpotifySongID != "-")
                        dis = UpdateDB("Standardization", cmds + ";", cnb);

                    var cmdz = "UPDATE Main SET Has_Track_No=\"Yes\", Track_No=\"" + trackno.ToString("D2") + "\",Spotify_Song_ID=\"" + SpotifySongID + "\", Spotify_Artist_ID=\"" + SpotifyArtistID + "\"";
                    cmdz += ", Spotify_Album_ID=\"" + SpotifyAlbumID + "\"";
                    cmdz += " WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[5].ToString();
                    DataSet dos = new DataSet();
                    if (trackno > 0 && SpotifySongID != "" && SpotifySongID != "-")
                        dos = UpdateDB("Main", cmdz + ";", cnb);
                }

                timestamp = UpdateLog(timestamp, i + "/" + (noOfRec - 1) + " Spotify details: " + trackno + " " + SpotifyAlbumPath, true, c("dlcm_TempPath"), "", windw, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            catch (Exception ex) { var tust = "Spotify Error ..." + ex; timestamp = UpdateLog(timestamp, tust, false, c("dlcm_TempPath"), "", "", null, null); }
            return trackno;
        }

        private void btn_Remove_Packed_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < cmb_Packed.Items.Count - 1; i++)
                if (i != cmb_Packed.SelectedIndex)
                {
                    DeleteFile(cmb_Packed.Items[i].ToString());
                    cmb_Packed.Items.RemoveAt(i);
                    DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE PackPath+FileName=\"" + cmb_Packed.Items[i].ToString() + "\" AND PackPath unlike \"%old%\"; ", cnb);
                }
            MessageBox.Show("All repacked songs have been deleted");
        }

        private void btn_AssesIfDuplicate_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;

            //Read from DB 
            var sel = "SELECT * FROM Main WHERE ID IN (";

            for (int k = 1; k < databox.SelectedRows.Count; k++)
            {
                if (k > 1) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            sel += ")";
            var SongRecord = GenericFunctions.GetRecord_s(sel, cnb);
            var norows = SongRecord[0].NoRec.ToInt32();

            var b = 0;
            var dupli_assesment = "";
            var IDD = "";
            var folder_name = "";
            var DLCC = "";
            var Platformm = "";
            var filename = "";
            var HasOrig = "";
            var Duplic = 0;
            var maxDuplic = 0;
            //Calculate the dupli number
            DataSet dxff = new DataSet(); var cmd = "SELECT MAX(Duplicate_Of) FROM Main WHERE Duplicate_Of<>\"\" Group BY Duplicate_Of";
            dxff = SelectFromDB("Main", cmd, c("dlcm_DBFolder"), cnb);
            maxDuplic = dxff.Tables.Count == 1 ? (dxff.Tables[0].Rows.Count > 0 ? (dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() > 0 ? dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1) : 1) : 1;

            bool newold = c("dlcm_AdditionalManipul32") == "Yes" ? true : false;
            Random random = new Random();
            var j = 0;

            var versio = databox.SelectedRows[0].Cells["Version"].Value.ToString() == null ? 1 : float.Parse(databox.SelectedRows[0].Cells["Version"].Value.ToString().Replace("_", "."), NumberStyles.Float, CultureInfo.CurrentCulture);//else dupli_reason += ". Possible Duplicate Import at end. End Else";
            DLCPackageData info = null;
            var platform = (c("dlcm_TempPath") + "\\0_old\\" + databox.SelectedRows[0].Cells["Original_FileName"].Value.ToString()).GetPlatform();
            try
            {
                info = DLCPackageData.LoadFromFolder(databox.SelectedRows[0].Cells["Folder_Name"].Value.ToString(), platform); //Generating preview with different name
            }
            catch (Exception ex)
            {

                timestamp = UpdateLog(timestamp, "Issues at loading song: " + ex, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            List<string> xmlhlist = new List<string>(); //xml hash
            List<string> jsonhlist = new List<string>(); //json hash
            List<string> dlist = new List<string>(); //last conversion
            List<string> clist = new List<string>(); //section
            List<string> cxmlhlist = new List<string>(); //cleanedxml hash
            List<string> snghlist = new List<string>(); //sng hash


            //var Original_FileName=
            DataSet dhf = new DataSet(); cmd = "SELECT XMLFile_Hash, Json_Hash, ConversionDateTime, Has_Sections, CleanedXML_Hash, SNGFileHash FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text;
            dhf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb);
            for (var h = 0; h < dhf.Tables[0].Rows.Count; h++)
            {
                xmlhlist.Add(dhf.Tables[0].Rows[h].ItemArray[0].ToString());
                jsonhlist.Add(dhf.Tables[0].Rows[h].ItemArray[1].ToString());
                dlist.Add(dhf.Tables[0].Rows[h].ItemArray[2].ToString());
                clist.Add(dhf.Tables[0].Rows[h].ItemArray[3].ToString());
                cxmlhlist.Add(dhf.Tables[0].Rows[h].ItemArray[4].ToString());
                snghlist.Add(dhf.Tables[0].Rows[h].ItemArray[5].ToString());
            }

            DLCC = txt_DLC_ID.Text;
            Platformm = (databox.SelectedRows[0].Cells["Import_Path"].Value + "\\" + databox.SelectedRows[0].Cells["Original_FileName"].Value).GetPlatform().platform.ToString();

            //calculate the alternative no (in case is needed)
            var altver = "";
            if (txt_Title.Text.ToLower().IndexOf("lord") >= 0)
                altver = "";
            sel = sel.Replace("*", "MAX (Alternate_Version_No");
            //Get last inserted ID
            DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, c("dlcm_DBFolder"), cnb);

            var altvert = dds.Tables.Count > 0 ? (dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 1 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32()) : 1;
            if (chbx_Original.Text == "No") altver = (altvert + 1).ToString();


            info.SongInfo.SongDisplayName = databox.SelectedRows[0].Cells["Song_Title"].Value.ToString();
            info.SongInfo.SongDisplayNameSort = databox.SelectedRows[0].Cells["Song_Title_Sort"].Value.ToString();
            info.SongInfo.Artist = databox.SelectedRows[0].Cells["Artist"].Value.ToString();
            info.SongInfo.ArtistSort = databox.SelectedRows[0].Cells["Artist_Sort"].Value.ToString();
            info.SongInfo.Album = databox.SelectedRows[0].Cells["Album"].Value.ToString();
            info.SongInfo.AlbumSort = databox.SelectedRows[0].Cells["Album_Sort"].Value.ToString();
            info.SongInfo.SongYear = databox.SelectedRows[0].Cells["Album_Year"].Value.ToString().ToInt32();
            info.Name = databox.SelectedRows[0].Cells["DLC_Name"].Value.ToString();

            var fsz = databox.SelectedRows[0].Cells["File_Size"].Value.ToString();
            var hash = databox.SelectedRows[0].Cells["Original_File_Hash"].Value.ToString();
            var author = databox.SelectedRows[0].Cells["Author"].Value.ToString();
            var tkversion = databox.SelectedRows[0].Cells["ToolkitVersion"].Value.ToString();
            var DD = databox.SelectedRows[0].Cells["Has_DD"].Value.ToString();
            var Bass = databox.SelectedRows[0].Cells["Has_Bass"].Value.ToString();
            var Guitar = databox.SelectedRows[0].Cells["Has_Guitar"].Value.ToString();
            var Combo = databox.SelectedRows[0].Cells["Has_Combo"].Value.ToString();
            var Rhythm = databox.SelectedRows[0].Cells["Has_Rhythm"].Value.ToString();
            var Lead = databox.SelectedRows[0].Cells["Has_Lead"].Value.ToString();
            var Vocalss = databox.SelectedRows[0].Cells["Has_Vocals"].Value.ToString();
            var original_FileName = databox.SelectedRows[0].Cells["Original_FileName"].Value.ToString();
            var art_hash = databox.SelectedRows[0].Cells["AlbumArt_OrigHash"].Value.ToString();
            var audioPreview_hash = databox.SelectedRows[0].Cells["Audio_OrigPreviewHash"].Value.ToString();
            var Is_Original = databox.SelectedRows[0].Cells["Is_Original"].Value.ToString();
            var Is_MultiTrack = databox.SelectedRows[0].Cells["Is_Multitrack"].Value.ToString();
            var MultiTrack_Version = databox.SelectedRows[0].Cells["MultiTrack_Version"].Value.ToString();
            var LiveDetails = databox.SelectedRows[0].Cells["Live_Details"].Value.ToString();
            var IsLive = databox.SelectedRows[0].Cells["Is_Live"].Value.ToString();
            var IsAcoustic = databox.SelectedRows[0].Cells["Is_Acoustic"].Value.ToString();
            var unpackedDir = databox.SelectedRows[0].Cells["Is_Acoustic"].Value.ToString();
            var audio_hash = databox.SelectedRows[0].Cells["Audio_OrigHash"].Value.ToString();
            var File_Creation_Date = databox.SelectedRows[0].Cells["File_Creation_Date"].Value.ToString();
            var Tunings = databox.SelectedRows[0].Cells["Tunning"].Value.ToString();
            var Rebuild = false;
            var action = ""; var reas = "";
            if (norows >= 1)
                foreach (var file in SongRecord)
                {
                    j++;
                    if (file.ID == null) break;
                    Duplic = Duplic == 0 && maxDuplic == 1 ? file.Duplicate_Of.ToInt32() : maxDuplic;

                    folder_name = file.Folder_Name;
                    filename = databox.Rows[0].Cells["Original_FileName"].Value.ToString();// file.Current_FileName;
                    IDD = file.ID; //Save Id in case of update or asses-update

                    var dupli_reason = "";
                    var dupli_assesment_reason = "";
                    timestamp = UpdateLog(timestamp, dupli_reason, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    bool platform_doesnt_matters = c("dlcm_AdditionalManipul76") == "Yes" ? (file.Platform == Platformm ? true : false) : true;
                    var tst = "";
                    frm_Duplicates_Management frm1 = new frm_Duplicates_Management(file, info, author, tkversion, DD, Bass, Guitar, Combo,
                        Rhythm, Lead, Vocalss, Tunings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, xmlhlist, jsonhlist,
                        c("dlcm_DBFolder"), clist, dlist, newold, Is_Original, altvert.ToString(),
                        c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39") == "Yes" ? true : false, c("dlcm_AdditionalManipul40") == "Yes" ? true : false,
                        fsz, unpackedDir, Is_MultiTrack, MultiTrack_Version, File_Creation_Date, j.ToString() + "" + "", Platformm, IsLive, LiveDetails, IsAcoustic, cnb, dupli_reason, databox.SelectedRows[0].Cells["Song_Lenght"].Value.ToString(), "-",
                        databox.SelectedRows[0].Cells["ID"].Value.ToString().ToInt32(), cxmlhlist, snghlist, databox.Rows[0].Cells["File_Hash"].Value.ToString());
                    frm1.ShowDialog();
                    if (frm1.Author != author) if (frm1.Author == "Custom Song Creator" && c("dlcm_AdditionalManipul47") == "Yes") author = "";
                        else author = frm1.Author;
                    //if (frm1.Description != description) description = frm1.Description;
                    //if (frm1.Comment != comment) comment = frm1.Comment;
                    if (frm1.Title != info.SongInfo.SongDisplayName)
                        if (c("dlcm_AdditionalManipul46") == "Yes") info.SongInfo.SongDisplayName = frm1.Title;
                    //if (frm1.Version != tkversion) PackageVersion = frm1.Version;
                    //if (frm1.DLCID != Namee) Namee = frm1.DLCID;
                    //if (frm1.Is_Alternate != Is_Alternate) Is_Alternate = frm1.Is_Alternate;
                    if (frm1.Title_Sort != info.SongInfo.SongDisplayNameSort) info.SongInfo.SongDisplayNameSort = frm1.Title_Sort;
                    if (frm1.Artist != info.SongInfo.Artist) info.SongInfo.Artist = frm1.Artist;
                    if (frm1.ArtistSort != info.SongInfo.ArtistSort) info.SongInfo.ArtistSort = frm1.ArtistSort;
                    if (frm1.Album != info.SongInfo.Album) info.SongInfo.Album = frm1.Album;
                    if (frm1.albumsort != info.SongInfo.AlbumSort) info.SongInfo.AlbumSort = frm1.albumsort;
                    //if (frm1.Alternate_No != Alternate_No) Alternate_No = frm1.Alternate_No;
                    //if (frm1.AlbumArtPath != AlbumArtPath) AlbumArtPath = frm1.AlbumArtPath;
                    //if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
                    if (frm1.MultiT != "") Is_MultiTrack = frm1.MultiT;
                    ////////////if (frm1.MultiTV != "") MultiTrack_Version = frm1.MultiTV;

                    if (frm1.isLive != "") IsLive = frm1.isLive;
                    if (frm1.isAcoustic != "") IsLive = frm1.isAcoustic;
                    if (frm1.liveDetails != "") LiveDetails = frm1.liveDetails;
                    //if (frm1.YouTube_Link != "") YouTube_Link = frm1.YouTube_Link;
                    //if (frm1.CustomsForge_Link != "") CustomsForge_Link = frm1.CustomsForge_Link;
                    //if (frm1.CustomsForge_Like != "") CustomsForge_Like = frm1.CustomsForge_Like;
                    //if (frm1.CustomsForge_ReleaseNotes != "") CustomsForge_ReleaseNotes = frm1.CustomsForge_ReleaseNotes;
                    //if (frm1.dupliID != "") dupliID = frm1.dupliID;
                    //if (frm1.ExistingTrackNo != "") ExistingTrackNo = frm1.ExistingTrackNo;
                    //IgnoreRest = false;
                    //if (frm1.IgnoreRest) IgnoreRest = frm1.IgnoreRest;

                    //timestamp = UpdateLog(timestamp, "REturing from child..", true, txt_c("dlcm_TempPath").Text, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //var tst = "Ignore;Manual_Decision";
                    //if (frm1.Asses != "") tst = frm1.Asses;
                    //frm1.Dispose();
                    if (frm1.Asses != "") dupli_assesment = frm1.Asses;
                    timestamp = UpdateLog(timestamp, "REturing.. to duplicate management " + tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    if (dupli_assesment is null) continue;
                    string[] retunc = dupli_assesment.Split(';');//Get Duplication assessment and its reason
                    dupli_assesment = retunc[0];
                    if (dupli_assesment == "Ignore") action += databox.SelectedRows[0].Cells["ID"].Value.ToString() + ",";
                    else if (dupli_assesment == "Update") action += file.ID + ",";
                    if (retunc.Length > 1) dupli_assesment_reason = retunc[1];
                    //Exit condition
                    var tsst = "end check for dupli..."; timestamp = UpdateLog(timestamp, tsst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (dupli_assesment == "Stop") break;
                }
            var cmdd = "";
            if (action.Length > 1 && dupli_assesment != "Stop")
            {
                DataSet dhd = new DataSet(); cmdd = "SELECT * FROM Main WHERE ID IN (" + action.Substring(0, action.Length - 1) + ")";
                dhd = SelectFromDB("Main", cmdd, c("dlcm_DBFolder"), cnb);

                cmdd = "DELETE FROM Main WHERE ID IN (" + action.Substring(0, action.Length - 1) + ")";

                //1. Delete DB records
                DeleteRecords(action.Substring(0, action.Length - 1), cmdd, c("dlcm_DBFolder"), c("dlcm_TempPath"), (dhd.Tables.Count > 0 ? dhd.Tables[0].Rows.Count : 0).ToString(), "--", cnb);
            }
            var a = SaveOK; SaveOK = false;
            Populate(ref databox, ref Main);
            databox.Refresh(); SaveOK = a;
            if (i > 0 && databox.RowCount >= i)
            {
                i = i - 1;
                databox.FirstDisplayedScrollingRowIndex = i; databox.Rows[i].Selected = true;
                databox.Focus();
            }
            else i = 0;
            Update_Selected();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var sel = "SELECT Max(Split4Pack) FROM Main ";
            DataSet dct = SelectFromDB("Main", sel + ";", c("dlcm_DBFolder"), cnb);

            var cmd1 = "UPDATE Main SET Split4Pack = \"\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);

            var noOfRecs = dct.Tables.Count > 0 ? (dct.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(dct.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            for (var j = 1; j <= noOfRecs; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                DeleteDirectory(dest);
            }
        }

        public void btn_AddPackSplit_Click(object sender, EventArgs e)
        {
            CleanPack();
            var sel = SearchCmd;
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb);
            var noOfRecs = SongRecord.Tables[0].Rows.Count;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;

            var t = sel.IndexOf("FROM Main");
            var h = sel.IndexOf(") WHERE") + 1;
            var c = sel.Length;
            var l = sel.Substring(t, c - t);
            var j = 0;

            if (chbx_Instaces.Checked) DeleteMultiInstances();
            for (j = 1; j < txt_No4Splitting.Value + 1; j++)
            {
                sel = "SELECT top " + txt_NoOfSplits.Value + " ID FROM (SELECT ID,Split4Pack " + l.Replace(";", "") + ") WHERE Split4Pack= \"\"";
                var cmd2 = "UPDATE Main SET Split4Pack = \"" + j + "\" WHERE ID in (" + sel + ")";
                DataSet dbt = UpdateDB("Main", cmd2 + ";", cnb);
                if (chbx_Instaces.Checked)
                {
                    var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                    CopyFolder(AppWD + "\\..\\..", dest);

                    //change no of order
                    //var fxml = File.OpenText(dest + "\\RocksmithToolkitLib.Config.xml");
                    //string line = "";
                    //string header = "";
                    var ff = ConfigRepository.Instance()["dlcm_Configurations"];
                    ////Read and Save Header
                    //while ((line = fxml.ReadLine()) != null)
                    //{
                    //    if (line.Contains("dlcm_Split4Pack"))
                    //        header += "<Config Key=\"dlcm_Split4Pack\" Value=\"" + j + "\" />" + System.Environment.NewLine;
                    //    else if (line.Contains("dlcm_Configurations")) header += "<Config Key=\"dlcm_Configurations\" Value=\"" + ff + "\" />" + System.Environment.NewLine;
                    //    else if (line.Contains("dlcm_Grouping")) header += "<Config Key=\"dlcm_Grouping\" Value=\"Split\" />" + System.Environment.NewLine;
                    //    else header += line + System.Environment.NewLine;
                    //}
                    //fxml.Close();
                    //File.WriteAllText(dest + "\\RocksmithToolkitLib.Config.xml", header);
                    ConfigRepository.Instance()["dlcm_Split4Pack"] = j.ToString(); savesettings();

                    var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                    try
                    {
                        Process process = Process.Start(@xx);
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ff, "", "", null, null);
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                    }
                }
            }


            //var cmd2 = "UPDATE Main SET Split4Pack = \"" + j + "\" WHERE ID in (" + "SELECT ID FROM(SELECT ID, Split4Pack " + l.Replace("; ", "") + ") WHERE Split4Pack = \"\""; + ")";/*sel.Replace(c("dlcm_SearchFields"),"ID")*/
            //DataSet dbt = UpdateDB("Main", cmd2 + ";", cnb);

            Populate(ref databox, ref Main);
            databox.Refresh();
            Update_Selected();
            MessageBox.Show((j - 1).ToString() + " packs of songs have been created/marked.");
        }

        private void chbx_PS3HAN_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_PS3HAN.Checked) chbx_PS3Retail.Enabled = true;
            else chbx_PS3Retail.Enabled = false;
        }
        private void btn_SongOnFire_Click(object sender, EventArgs e)
        {
            eof(false);
        }

        private void btn_BRM_Click(object sender, EventArgs e)
        {
            btm(false);
        }

        private void btm(bool openfile)
        {
            if (openfile)
            {
                var j = databox.SelectedCells[0].RowIndex;
                string filePath = c("dlcm_TempPath") + "\\0_old\\" + databox.Rows[j].Cells["Original_FileName"].Value.ToString();

                try
                {
                    Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Old Folder in Explorer ! ");
                }
            }

            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PathForBRM"), " ").Replace("\\ ", " ");//c("dlcm_TempPath")+"\\0_old\\"+txt_OldPath.Text
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Beats & Phrases Resynchronizer if you want to use it.", c("dlcm_PathForBRM_www"), "Missing Beats & Phrases Resynchronizer", false, false, true, "", "", ""); frm1.ShowDialog(); return; }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT Max(Split4Pack) FROM Main ", "", cnb);
            var nosplits = ds.Tables.Count > 0 ? (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            if (nosplits > 0)
            {
                for (var j = 1; j < nosplits; j++)
                {
                    var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;

                    var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                    try
                    {
                        Process process = Process.Start(@xx);
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                    }
                }
            }
        }
        int prevWidth;
        FormWindowState prevWindowState;

        private void btn_OpenRetail_Click(object sender, EventArgs e)
        {
            Cache frm = new Cache(c("dlcm_DBFolder"), c("dlcm_TempPath"), c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39").ToLower() == "yes" ? true : false, c("dlcm_AdditionalManipul40").ToLower() == "yes" ? true : false, cnb);
            frm.ShowDialog();
        }

        private void btn_ApplyArtistShortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Artist_Short = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet dus = UpdateDB("Main", cmd1 + ";", cnb);
            cmd1 = "UPDATE Main SET Artist_ShortName = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet difg = UpdateDB("Main", cmd1 + ";", cnb);
        }

        private void btn_Replace_Brakets_Click(object sender, EventArgs e)
        {
            txt_Title.Text = txt_Title.Text.Replace("(", "[").Replace(")", "]");
            var i = databox.SelectedCells[0].RowIndex;
            if (txt_Title.Text != databox.Rows[i].Cells["Song_Title"].Value.ToString())
                chbx_Has_Been_Corrected.Checked = true;
        }

        private void btn_PlayAudio_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProcessStarted) { DDC.Kill(); ProcessStarted = false; btn_PlayPreview.Text = "Play Audio"; return; }
            }
            catch (Exception er)
            {
            }
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggdec2.exe"),
                WorkingDirectory = AppWD
            };
            var t = txt_OggPath.Text;
            startInfo.Arguments = string.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
            {
                DDC.StartInfo = startInfo;
                DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                btn_PlayAudio.Text = "Stop";
                ProcessStarted = true;
            }
        }

        private void btn_Fix_AudioIssues_Click(object sender, EventArgs e)
        {
            SaveOK = false;
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            var cancel = false;
            if (btn_Fix_AudioIssues.Text != "Fix Audio") { cancel = true; btn_Fix_AudioIssues.Text = "Fix Audio"; }
            else btn_Fix_AudioIssues.Text = "Cancel Fix Audio";

            //Create Preview
            //Fix Preview
            var cmd = "SELECT ID,AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath  FROM Main WHERE Has_Preview=\"No\"";
            cmd += " AND ID=" + txt_ID.Text + "";
            var prev = FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB");

            //Fix Bitrate
            cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath  FROM Main WHERE (VAL(audioBitrate) > " + (c("dlcm_MaxBitRate")) + " or VAL(audioSampleRate) > " + (c("dlcm_MaxSampleRate")) + ")";
            cmd += " AND ID=" + txt_ID.Text + "";
            FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB");

            //Get Spotify
            //fix missing spotify details
            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            var sel = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"0\" OR Track_No = \"00\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb);
            var noOfRec = SongRecord.Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

            if (netstatus == "OK" || noOfRec > 0) FixWithSpotifyDetails(SongRecord, 0, noOfRec, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp);


            //fix missing youtube details
            sel = "SELECT * FROM Main WHERE (YouTube_Link = \"\" OR Youtube_Playthrough = \"\" or YouTube_Link is null OR Youtube_Playthrough is null) "
               + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            //Read from DB
            MainDBfields[] SongRecords = new MainDBfields[10000];
            SongRecords = GetRecord_s(sel, cnb);
            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") GetYoutubeDetailsAsync(SongRecords[0], 0, cnb, pB_ReadDLCs, "MainDB");

            var i = databox.SelectedCells[0].RowIndex;
            Populate(ref databox, ref Main);
            databox.Refresh();

            if (i > 0) databox.FirstDisplayedScrollingRowIndex = i - 1;
            databox.Focus();
            btn_Fix_AudioIssues.Text = "Fix Audio";
            SaveOK = true;
            MessageBox.Show("Fixed Previews: " + prev + ", Spotify: " + noOfRec + ", Youtube: " + SongRecords[0].NoRec);
        }

        private void btn_CopyOld_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            var filename = databox.Rows[i].Cells["Original_FileName"].Value.ToString();
            string filePath = c("dlcm_TempPath") + "\\0_old\\" + filename;
            var dest = c("dlcm_RocksmithDLCPath") + "\\" + filename;
            if (databox.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")//OLd available
            {
                try
                {
                    File.Copy(filePath, dest, true);
                    MessageBox.Show("Old/Iinitially imported File Copied to " + c("dlcm_RocksmithDLCPath") + "\\");
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ex);
                }
            }
        }

        private void btn_AddCoverFlags_Click(object sender, EventArgs e)
        {

        }

        private void btn_Package_Click(object sender, EventArgs e)
        {
            var old = ConfigRepository.Instance()["dlcm_MuliThreading"];
            var pack = ""; var norows = 0; var brkn = 0;
            if (chbx_Format.Text == "PS4" || chbx_Format.Text == "iOS") { MessageBox.Show("Not yet working. rEtail songs can be eliminated just new ones packaged\n\nWiP"); return; }
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 4;
            pB_ReadDLCs.Increment(1);
            if (chbx_AutoSave.Checked) SaveRecord();
            savesettings();
            pB_ReadDLCs.Increment(1);
            ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "No";

            var Temp_Path_Import = c("dlcm_TempPath");
            var old_Path_Import = c("dlcm_TempPath") + "\\0_old";
            var dflt_Path_Import = c("dlcm_TempPath") + "\\0_to_import";
            var dataPath = c("dlcm_TempPath") + "\\0_data";
            var broken_Path_Import = c("dlcm_TempPath") + "\\0_broken";
            var dupli_Path_Import = c("dlcm_TempPath") + "\\0_duplicate";
            var dlcpacks = c("dlcm_TempPath") + "\\0_dlcpacks";
            var repacked_Path = c("dlcm_TempPath") + "\\0_repacked";
            var repacked_XBOXPath = c("dlcm_TempPath") + "\\0_repacked\\XBOX360";
            var repacked_PCPath = c("dlcm_TempPath") + "\\0_repacked\\PC";
            var repacked_MACPath = c("dlcm_TempPath") + "\\0_repacked\\MAC";
            var repacked_PSPath = c("dlcm_TempPath") + "\\0_repacked\\PS3";
            var log_Path = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
            var AlbumCovers_PSPath = c("dlcm_TempPath") + "\\0_albumCovers";
            var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            var Temp_Path = c("dlcm_TempPath") + "\\0_temp";
            string pathDLC = c("dlcm_RocksmithDLCPath");
            CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);

            //// Generate package worker
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;

            var i = databox.SelectedCells[0].RowIndex;

            //Read from DB 
            var sel = "";

            for (int k = 1; k < databox.SelectedRows.Count; k++)
            {
                if (k > 1) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            //var SongRecord = GenericFunctions.GetRecord_s(sel, cnb);
            //var norows = SongRecord[0].NoRec.ToInt32();

            Groupss = chbx_Group.Text.ToString();
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID in (" + sel + ")";/*txt_ID.Text*/

            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GetRecord_s(cmd, cnb);
            if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();

            string spotystatus = "", ybstatus = "", ftpstatus = "";
            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")) if (FTPAvail(txt_FTPPath.Text).ToLower() == "ok") ftpstatus = "ok";/*"", ConfigRepository.Instance()["dlcm_FTP" + ConfigRepository.Instance()["dlcm_FTP"]]*/
                else ftpstatus = "nok";

            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackagePreparation();

            //DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT max(val(Pack)) FROM Main", null, cnb);
            //if (dms.Tables[0].Rows.Count > 0) pack = (int.Parse(dms.Tables[0].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            pack = GetMax("Pack_AuditTrail", "Pack", cnb);

            //var i = 0;
            var bassRemoved = "No";
            foreach (var filez in SongRecord)
            {
                if (filez.ID == null) break;
                var args = txt_ID.Text + ";" + (bassRemoved == "No" ? "false" : "true") + ";";
                args += (chbx_Format.Text == "PC" ? "PC" : "") + ";" + (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU" ? "PS3" : "") + ";" + (chbx_Format.Text == "XBOX360" ? "XBOX360" : "") + ";" + (chbx_Format.Text == "Mac" ? "Mac" : "") + ";" + netstatus + ";";
                args += chbx_Beta.Checked + ";" + chbx_Group.Text + ";";
                args += Groupss + ";" + c("dlcm_TempPath") + ";";
                args += chbx_UniqueID.Checked + ";" + chbx_Last_Packed.Checked + ";";
                args += chbx_Last_Packed.Enabled + ";" + chbx_CopyOld.Checked + ";";
                args += chbx_CopyOld.Enabled + ";" + chbx_Copy.Checked + ";";
                args += chbx_Replace.Checked + ";" + chbx_Replace.Enabled + ";";
                args += pack + ";" + "MainDB" + ";";/*SourcePlatform*/
                args += databox.Rows[i].Cells["Original_FileName"].Value.ToString() + ";" + databox.Rows[i].Cells["Folder_Name"].Value.ToString() + ";";
                args += c("dlcm_AdditionalManipul49") + ";" + txt_RemotePath.Text + ";" + txt_FTPPath.Text + ";";
                args += chbx_RemoveBassDD.Checked + ";" + chbx_BassDD.Checked + ";" + chbx_KeepBassDD.Checked + ";";
                args += chbx_KeepDD.Checked + ";" + chbx_Original.Text + ";" + txt_ID.Text + ";";
                args += SearchCmd + (SearchCmd.IndexOf(";") > 0 ? "" : ";") + pathDLC + ";" + databox.Rows[databox.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value + ";"; //SearchCmd + ";" + c("dlcm_RocksmithDLCPath"), DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value
                args += c("dlcm_AdditionalManipul76") + ";" + "" + ";" + "MainDB" + ";" + "" + ";" + spotystatus + ";" + ybstatus + ";" + ftpstatus; ;
                pB_ReadDLCs.Increment(1);
                bwRGenerate.RunWorkerAsync(args);
                do
                    Application.DoEvents();
                while (bwRGenerate.IsBusy && !bwRGenerate.CancellationPending);//keep singlethread as toolkit not multithread abled
            }
            pB_ReadDLCs.Increment(1);

            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackage();
            ConfigRepository.Instance()["dlcm_MuliThreading"] = old;

            //GenerateSumamrty
            var PS3P = 0; var PCP = 0; var MACP = 0; var XBOXP = 0; var FailedP = 0; var ListP = "";
            var PS3F = 0; var PCF = 0; var MACF = 0; var XBOXF = 0; var cmds = "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\"";
            string summary = ""; 

            DataSet dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT CDLC_ID, Comments FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb);
            var noOfRecs = 0;
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) noOfRecs = dmz.Tables[0].Rows.Count;// tdmz.Tables[0].Rows.Count;// dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            FailedP = noOfRecs;

            if (FailedP > 0)
            {
                for (var j = 0; j < noOfRecs; j++)
                {
                    var dnz = new DataSet(); dnz = SelectFromDB("Main", "SELECT Artist, Song_Title FROM Main where ID=" + dmz.Tables[0].Rows[j].ItemArray[0].ToString() + "", null, cnb);
                    //if (dnz.Tables.Count > 0) if (dnz.Tables[0].Rows.Count > 0) noOfRecs = dmz.Tables[0].Rows.Count;
                    ListP += dmz.Tables[0].Rows[j].ItemArray[0].ToString() + "-" + dnz.Tables[0].Rows[0].ItemArray[0].ToString() + "-" + dnz.Tables[0].Rows[0].ItemArray[1].ToString() + "-" +
                        dmz.Tables[0].Rows[j].ItemArray[1].ToString() + "\n";
                }

                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PS3P = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                cmds = "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"";
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PCP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Mac\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) MACP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"XBOX360\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\" AND FTPed=\"Yes\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PS3F = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\" AND FTPed=\"Yes\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PCF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Mac\" AND FTPed=\"Yes\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) MACF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"XBOX360\" AND FTPed=\"Yes\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT COUNT(ID) FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb);
                if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) FailedP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT CDLC_ID, Comments FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb);

                //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT COUNT(ID) FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb);
                //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) FailedP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();

                //Show Summary window
                summary = "Packed/(Copied/FTPed) Summary(ID: " + pack + " of 1 selected songs) \n" +
                    "\nPacked PS3:" + PS3P + "/" + PS3F +
                   "\nPacked PC: " + PCP + "/" + PCF +
                    "\nPacked MAC: " + MACP + "/" + MACF +
                    "\nPacked XBOX: " + XBOXP + "/" + XBOXF +
                    "\nPacked All: " + (PS3P + PCP + MACP + XBOXP) + "/" + (PS3F + PCF + MACF + XBOXF) +
                    "\nBroken (Not considered4repacking): " + brkn +
                    "\n\nFailed at packing PC: " + FailedP +
                    ("\n\nListP: " + ListP);
            }
            bwRGenerate.Dispose();
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Summary of the Individual Repack process", false, false, true, "", "", "");

            timestamp = UpdateLog(timestamp, "Ended Packing " + summary + "\n songs.", true, null, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (FailedP > 0) frm9.Show();
        }

        private void btm_GoTemp_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = txt_FTPPath.Text;
            Process process;
            try
            {
                if (filePath.ToLower().IndexOf("ftp") > -1)
                {
                    filePath = filePath.Replace("/", "\\");
                    process = Process.Start("iexplore.exe", filePath);
                }
                else
                {
                    if (File.Exists(filePath)) process = Process.Start("explorer.exe", filePath);
                    else MessageBox.Show(txt_FTPPath.Text + " not available.");

                }
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Old Folder in Explorer ! ");
            }
        }

        private void btn_Like_Click(object sender, EventArgs e)
        {

        }

        private void btn_Followers_Click(object sender, EventArgs e)
        {

        }

        private async void btn_Debug_Click(object sender, EventArgs e)
        {
            DataSet dhf = new DataSet(); var cmd = "SELECT XMLFilePath,ID FROM Arrangements where CleanedXML_Hash=\"\"";// WHERE CDLC_ID=" + txt_ID.Text;
            dhf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
            pB_ReadDLCs.Maximum = dhf.Tables[0].Rows.Count;
            for (var h = 0; h < dhf.Tables[0].Rows.Count; h++)
            {
                var rt = GetHashCleanXML(dhf.Tables[0].Rows[h].ItemArray[0].ToString());
                var cmdupd = "UPDATE Arrangements Set CleanedXML_Hash=\"" + rt + "\" WHERE ID = " + dhf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb);
                if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                var tst = "XML hasin: " + h; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            DataSet ghf = new DataSet(); cmd = "SELECT JSONFilePath,ID FROM Arrangements where Json_Hash=\"\"";// WHERE CDLC_ID=" + txt_ID.Text;
            ghf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
            pB_ReadDLCs.Maximum = ghf.Tables[0].Rows.Count; pB_ReadDLCs.Value = 0;
            for (var h = 0; h < ghf.Tables[0].Rows.Count; h++)
            {
                var rt = ghf.Tables[0].Rows[h].ItemArray[0].ToString();//.Replace("\\arr\\", "\\bin\\");
                //if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                //    rt = (rt.Replace(".xml", ".sng").Replace("\\EOF\\", "\\Toolkit\\"));
                //else
                //    rt = rt.Replace(".xml", ".sng").Replace("\\songs\\bin", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                //snghlist.Add(GetHash(s1));
                rt = GetHashCleanXML(rt);
                var cmdupd = "UPDATE Arrangements Set Json_Hash=\"" + rt + "\" WHERE ID = " + ghf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb);
                if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                var tst = "Json hasin: " + pB_ReadDLCs.Value; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }

        //public async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        //{
        //    AuthorizationCodeAuth auth = (AuthorizationCodeAuth)sender;
        //    auth.Stop();

        //    Token token = await auth.ExchangeCode(payload.Code);
        //    SpotifyWebAPI api = new SpotifyWebAPI
        //    {
        //        AccessToken = token.AccessToken,
        //        TokenType = token.TokenType
        //    };
        //    //PrintUsefulData(api);
        //}

        //public async void PrintUsefulData(SpotifyWebAPI api)
        //{
        //    PrivateProfile profile = await api.GetPrivateProfileAsync();
        //    string name = string.IsNullOrEmpty(profile.DisplayName) ? profile.Id : profile.DisplayName;
        //    rtxt_StatisticsOnReadDLCs.Text = "Hello there, " + name;

        //    rtxt_StatisticsOnReadDLCs.Text += "Your playlists:";
        //    Paging<SimplePlaylist> playlists = await api.GetUserPlaylistsAsync(profile.Id);
        //    do
        //    {
        //        playlists.Items.ForEach(playlist =>
        //        {
        //            rtxt_StatisticsOnReadDLCs.Text += playlist.Name;
        //        });
        //        playlists = await api.GetNextPageAsync(playlists);
        //    } while (playlists.HasNextPage());
        //}

        private void button2_Click_3(object sender, EventArgs e)
        {
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set Reason = \"\" WHERE Reson=\"Missing PSARC\"; ", cnb);
        }

        private void btn_Remove_HashDuplicates_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail as i LEFT JOIN Pack_AuditTrail as p on p.FileHash=i.FileHash and p.id<>i.ID WHERE i.PackPath like \"%0_duplicate%\"";
            var tcmd = "SELECT PackPath+\"\\\"+FileName ";
            if (chbx_Ignore_Officials.Checked)
            {
                cmd += " AND Official=\"No\";";
                tcmd = "SELECT PackPath+\"\\\"+FileName " + cmd;
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb);
                var rec = dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString());
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }

            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb);
            MessageBox.Show("All HashDuplicates songs have been deleted");
        }

        private void btn_Remove_AllDuplicates_Click(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath like \"%0_duplicate%\"";
            var tcmd = "SELECT PackPath+\"\\\"+FileName ";
            if (chbx_Ignore_Officials.Checked)
            {
                cmd += " AND Official=\"No\";";
                tcmd = "SELECT PackPath+\"\\\"+FileName " + cmd;
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb);
                var rec = dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString());
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb);
            MessageBox.Show("All Duplicates songs have been deleted");
        }

        private void btm_PKGLinker_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PKG_Linker"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install PKG Linker Server if you want to use it.", c("dlcm_PKG_Linker_www"), "Missing PKG Linker Server", false, false, true, "", "", ""); frm1.ShowDialog(); return; }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_TrueRepacker_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_TrueAncestor_PKG_Repacker"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install TrueAncestor PKG Repacker if you want to use it.", c("dlcm_TrueAncestor_PKG_Repacker_www"), "Missing TrueAncestorPKG Repacker", false, false, true, "", "", ""); frm1.ShowDialog(); return; }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_PKGSigner_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PS3xploit-resigner"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install PS3xploit Resigner if you want to use it.", c("dlcm_PS3xploit-resigner_www"), "Missing PS3xploit Resigner", false, false, true, "", "", ""); frm1.ShowDialog(); return; }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_TotalCommander_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_TCommander"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Total Commander if you want to use it.", c("dlcm_TCommander_www"), "Missing Total Commander", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_OpenMulti_Click(object sender, EventArgs e)
        {

            for (var j = 1; j < 100; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                if (!(Directory.Exists(dest))) return;
                CopyFolder(AppWD + "\\..\\..", dest); var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                try
                {
                    Process process = Process.Start(@xx);
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                }
            }
        }
        private void SymbolLinkFolder(string t, string tt)
        {
            if (t == "") return;
            if (File.Exists(t)) DeleteFile(t);
            else
            if (Directory.Exists(t))
            {
                DeleteDirectory(t);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = "mklink",
                WorkingDirectory = AppWD
            };

            startInfo.Arguments = string.Format(" /D \"{0}\" \"{1}\"", t, tt);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void txt_MultiTrackType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Filtertxt = cmb_Filter.Text;
            var OrderAlt = "";
            if (Filtertxt == "") return;
            if (chbx_AutoSave.Checked) SaveRecord(); SaveOK = false;

            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = SearchCmd;/*, "Part of No Group", "Part of Any Group" */

            var i = databox.SelectedCells.Count == 0 ? 0 : databox.SelectedCells[0].RowIndex;

            chbx_Replace.Enabled = false;
            var oldSearchCmd = SearchCmd;
            SearchCmd = SearchCmd.Length == 0 ? "SELECT * FROM Main " : SearchCmd.Substring(0, (SearchCmd.IndexOf(" WHERE") - 1) > 0 ? (SearchCmd.IndexOf(" WHERE") - 1) : ((SearchCmd.IndexOf(" ORDER") - 1) > 0 ? (SearchCmd.IndexOf(" ORDER") - 1) : SearchCmd.Length - 1));
            SearchCmd += "u WHERE ";

            switch (Filtertxt)
            {
                case "No Cover":
                    SearchCmd += "Has_Cover <> 'Yes'";
                    break;
                case "No Preview":
                    SearchCmd += "Has_Preview <> 'Yes'";
                    break;
                case "No Vocals":
                    SearchCmd += "Has_Vocals <> 'Yes'";
                    break;
                case "No Section":
                    SearchCmd += "Has_Sections ='Yes'";
                    break;
                case "No Bass":
                    SearchCmd += "Has_Bass <> 'Yes'";
                    break;
                case "No Guitar":
                    SearchCmd += "Has_Guitar <> 'Yes'";
                    break;
                case "No Track No.":
                    SearchCmd += "Has_Track_No <> 'Yes' OR Track_No='-1' OR Track_No='0'";
                    break;
                case "No Version":
                    SearchCmd += "Has_Version <> 'Yes'";
                    break;
                case "No Author":
                    SearchCmd += "Has_Author <> 'Yes'";
                    break;
                case "Original":
                    SearchCmd += "Is_Original = 'Yes'";
                    break;
                case "CDLC":
                    SearchCmd += "Is_Original <> 'Yes'";
                    break;
                case "Selected":
                    SearchCmd += "Selected = 'Yes'";
                    break;
                case "Beta":
                    SearchCmd += "Is_Beta = 'Yes'";
                    break;
                case "Live":
                    SearchCmd += "Is_Live = 'Yes'";
                    break;
                case "Acoustic":
                    SearchCmd += "Is_Acoustic = 'Yes'";
                    break;
                case "Instrumental":
                    SearchCmd += "Is_Instrumental = 'Yes'";
                    break;
                case "Single":
                    SearchCmd += "Is_Single= 'Yes'";
                    break;
                case "Soundtrack":
                    SearchCmd += "Is_Soundtrack = 'Yes'";
                    break;
                case "EP":
                    SearchCmd += "Is_EP = 'Yes'";
                    break;
                case "Audio Changed":
                    SearchCmd += "Has_Had_Audio_Changed = 'Yes'";
                    break;
                case "Lyrics Changed":
                    SearchCmd += "Has_Had_Lyrics_Changed = 'Yes'";
                    break;
                case "Broken":
                    SearchCmd += "Is_Broken = 'Yes'";
                    break;
                case "Alternate":
                    SearchCmd += "Is_Alternate = 'Yes'";
                    break;
                case "Duplicated":
                    SearchCmd += "Duplicate_Of <> ''";
                    break;
                case "With DD":
                    SearchCmd += "Has_DD = 'Yes'";
                    break;
                case "No DD":
                    SearchCmd += "Has_DD <> 'Yes'";
                    break;
                case "No Bass DD":
                    SearchCmd += "Bass_Has_DD <> 'Yes'";
                    break;
                case "Digitech Drop compatible Guitar (Straight down conv from E Standard or Drop D)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\", \"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\", \"BbDropAb\", \"A Drop G\"))";
                    break;
                case "Digitech Drop compatible Guitar (Straight down conv from E Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\") AND p.ArrangementType=\"Guitar\")";
                    break;
                case "Digitech Drop compatible Bass (Straight down conv from E Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\") AND p.ArrangementType=\"Bass\")";
                    break;
                case "Digitech Drop compatible (Straight down conv from E Standard)":
                    //<TuningDefinition Version="RS2012" Name="EFlat" UIName="Eb" >< Tuning string0="-1" string1="-1" string2="-1" string3="-1" string4="-1" string5 = "-1" />
                    //<TuningDefinition Version="RS2014" Name="EbStandard" UIName="Eb Standard"> <Tuning string0="-1" string1="-1" string2="-1" string3="-1" string4="-1" string5="-1"/>
                    //<TuningDefinition Version="RS2014" Name="DStandard" UIName="D Standard"><Tuning string0="-2" string1="-2" string2="-2" string3="-2" string4="-2" string5="-2"/>
                    //<TuningDefinition Version="RS2014" Name="C#Standard" UIName="C# Standard"><Tuning string0="-3" string1="-3" string2="-3" string3="-3" string4="-3" string5="-3"/>
                    //<TuningDefinition Version="RS2014" Name="CStandard" UIName="C Standard"><Tuning string0="-4" string1="-4" string2="-4" string3="-4" string4="-4" string5="-4"/>
                    //<TuningDefinition Version="RS2014" Name="BStandard" UIName="B Standard" Custom="true"><Tuning string0="-5" string1="-5" string2="-5" string3="-5" string4="-5" string5="-5"/>
                    //<TuningDefinition Version="RS2014" Name="BbStandard" UIName="Bb Standard" Custom="true"><Tuning string0="-6" string1="-6" string2="-6" string3="-6" string4="-6" string5="-6"/>
                    //<TuningDefinition Version="RS2014" Name="AStandard" UIName="A Standard" Custom="true"><Tuning string0="-7" string1="-7" string2="-7" string3="-7" string4="-7" string5="-7"/>
                    //<TuningDefinition Version="RS2014" Name="AbStandard" UIName="Ab Standard" Custom="true"><Tuning string0="-7" string1="-7" string2="-7" string3="-7" string4="-7" string5="-7"/>
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\"))";
                    break;
                case "Digitech Drop compatible Guitar (Straight down conv from D Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                        ", \"BbDropAb\", \"A Drop G\") AND p.ArrangementType=\"Guitar\")";
                    break;
                case "Digitech Drop compatible Bass (Straight down conv from D Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                        ", \"BbDropAb\", \"A Drop G\") AND p.ArrangementType=\"Bass\")";
                    break;
                case "Digitech Drop compatible (Straight down conv from Drop D)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                        ", \"BbDropAb\", \"A Drop G\"))";
                    //<TuningDefinition Version="RS2014" Name="EbDropDb" UIName="Eb Drop Db"><Tuning string0="-3" string1="-1" string2="-1" string3="-1" string4="-1" string5="-1"/>
                    //<TuningDefinition Version="RS2014" Name="DDropC" UIName="D Drop C"><Tuning string0="-4" string1="-2" string2="-2" string3="-2" string4="-2" string5="-2"/>
                    //<TuningDefinition Version="RS2014" Name="C#DropB" UIName="C# Drop B" Custom="true"><Tuning string0="-5" string1="-3" string2="-3" string3="-3" string4="-3" string5="-3"/>
                    //<TuningDefinition Version="RS2014" Name="CdropA#" UIName="C Drop A#" Custom="true"><Tuning string0="-6" string1="-4" string2="-4" string3="-4" string4="-4" string5="-4"/>
                    //<TuningDefinition Version="RS2014" Name="BDropA" UIName="B Drop A" Custom="true"><Tuning string0="-7" string1="-5" string2="-5" string3="-5" string4="-5" string5="-5"/>
                    //<TuningDefinition Version="RS2014" Name="BbDropAb" UIName="Bb Drop Ab" Custom="true"><Tuning string0="-8" string1="-6" string2="-6" string3="-6" string4="-6" string5="-6"/>
                    //<TuningDefinition Version="RS2014" Name="ADropG" UIName="A Drop G" Custom="true"><Tuning string0="-9" string1="-7" string2="-7" string3="-7" string4="-7" string5="-7"/>
                    break;
                case "E Standard":
                    SearchCmd += "Tunning = 'E Standard'";
                    break;
                case "Eb Standard":
                    SearchCmd += "Tunning = 'Eb Standard'";
                    break;
                case "Drop D":
                    SearchCmd += "Tunning = 'Drop D'";
                    break;
                case "Other Tunings":
                    SearchCmd += "Tunning not in ('E Standard','Eb Standard','Drop D')";
                    break;
                case "With Bonus":
                    SearchCmd += "Has_Bonus_Arrangement = 'Yes'";
                    break;
                case "Imported as Pc":
                    SearchCmd += "Platform = 'Pc'";
                    break;
                case "Imported as PS3":
                    SearchCmd += "Platform = 'PS3'";
                    break;
                case "Imported as Mac":
                    SearchCmd += "Platform ='Mac'";
                    break;
                case "Imported as XBOX360":
                    SearchCmd += "Platform = 'XBOX360'";
                    break;
                case "Packed as Pc"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND p.Platform=\"Pc\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"PC\") >0)";
                    break;
                case "Packed as PS3"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"PS3\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"PS3\") >0)";
                    break;
                case "Packed as Mac"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"Mac\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"MAC\") >0)";
                    break;
                case "Packed as XBOX360"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"XBOX360\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"XBOX360\") >0)";
                    break;
                case "0ALL"://0ALL
                    SearchFields = c("dlcm_SearchFields");
                    SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY " + c("dlcm_OrderOfFields") + ";";
                    //SearchCmd = SearchCmd.Replace(" WHERE ", "");
                    break;
                case "ALL Others"://0ALL
                    SearchCmd = "SELECT * FROM Main WHERE ID not in (" + oldSearchCmd.Replace("*", "ID") + ")";
                    break;
                case "Track No. 1"://Track No. 1
                    SearchCmd += "Track_No = '1'";
                    break;
                case "DLCID diff than Default"://DLCID diff than Default
                    SearchCmd += "DLC_AppID <> '" + c("general_defaultappid_RS2014") + "'";
                    break;
                case "Automatically generated Preview"://Autom gen Preview
                    SearchCmd += "PreviewTime = '" + c("dlcm_PreviewStart") + "' AND (PreviewLenght>'" + c("dlcm_PreviewLenght") + "'-1) AND (PreviewLenght<'" + c("dlcm_PreviewLenght") + "'+1)";
                    break;
                case "Any DLCManager generated Preview"://Autom gen Preview
                    SearchCmd += "PreviewTime <> '' AND PreviewLenght <> ''";
                    break;
                case "With Duplicates"://With Duplicates
                    SearchCmd += "Available_Duplicate = 'Yes'";
                    break;
                case "Main_NoOLD":
                    SearchCmd += "Available_Old <> 'Yes'";
                    break;
                case "Show Songs with FilesMissing Issues":
                    SearchCmd += "FilesMissingIssues <> ''";
                    break;
                case "Songs in Rocksmith Game Lib":
                    SearchCmd += "Remote_Path <> ''";
                    break;
                case "In the Works":
                    SearchCmd += "IntheWorks = 'Yes'";
                    break;
                case "Improved with DLC Manager":
                    SearchCmd += "ImprovedWIthDM = 'Yes'";
                    break;
                case "Full Album":
                    SearchCmd += "Is_FullAlbum = 'Yes'";
                    break;
                case "Uncensored":
                    SearchCmd += "Uncensored = 'Yes'";
                    break;
                case "Main_NoPreviewFile":
                    SearchCmd += "audioPreviewPath = ''";
                    break;
                case "Packed (curr. Platform)":
                    SearchCmd += " ID IN (SELECT CDLC_ID FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\" AND Platform=\"" + chbx_Format.Text + "\")";
                    break;
                case "Different Artist/Album/Title vs Sort counterparts":
                    SearchCmd += " Artist <> Artist_Sort OR Album <> Album_Sort OR Song_Title <> Song_Title_Sort";
                    break;
                case "Same (imported/old) File Name":
                    //SLOW SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    //var SearchCmd52 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT m.ID,Original_FileName FROM Main AS m ORDER BY LCASE(Original_FileName)", "", cnb);
                    noOfRec = dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
                    var IDgg = "";
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dgs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dgs.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    IDgg += dgs.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dgs.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                else break;

                    var SearchCmd52 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDgg + ")";
                    SearchCmd52 = SearchCmd52.Replace(", )", ")");
                    OrderAlt = "LCASE(Original_FileName), LCASE(Artist), LCASE(Song_Title)";

                    SearchCmd += "ID IN (" + SearchCmd52 + ")";
                    break;
                case "Same hash File Name":
                    //SLOW SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    //var SearchCmd52 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    DataSet dgc = new DataSet(); dgc = SelectFromDB("Main", "SELECT m.ID,File_Hash FROM Main AS m ORDER BY File_Hash", "", cnb);
                    noOfRec = dgc.Tables.Count == 0 ? 0 : dgc.Tables[0].Rows.Count;
                    var IDgl = "";
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dgc.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dgc.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    IDgl += dgc.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dgc.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                else break;

                    var SearchCmd5f2 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDgl + ")";
                    SearchCmd5f2 = SearchCmd5f2.Replace(", )", ")");
                    OrderAlt = "LCASE(Original_FileName), LCASE(Artist), LCASE(Song_Title)";

                    SearchCmd += "ID IN (" + SearchCmd5f2 + ")";
                    break;
                case "with Errors at Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID FROM LogPackingError)";
                    break;
                case "with Errors at Last Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID from LogPackingError WHERE Pack=(SELECT TOP 1 Pack from LogPackingError GROUP BY Pack ORDER BY val(Pack) DESC))";
                    break;
                case "Imported Last":
                    DataSet dds = new DataSet(); dds = SelectFromDB("Main", "SELECT top 1 Pack FROM Main order by ID DESC;", "", cnb);
                    noOfRec = dds.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "Pack=\"" + dds.Tables[0].Rows[0].ItemArray[0].ToString() + "\"";
                    else SearchCmd += "1 = 2";
                    break;
                case "Imported Current Month":
                    DateTime date = DateTime.Today;
                    var firstDayOfMonth = (new DateTime(date.Year, date.Month, 1)).ToString();
                    //DataSet djs = new DataSet(); djs = SelectFromDB("Main", "SELECT Import_Date FROM Main ORDER BY Import_Date DESC;", "", cnb);
                    //noOfRec = djs.Tables[0].Rows.Count;
                    //if (noOfRec > 0) SearchCmd += "Import_Date > #" + firstDayOfMonth.ToShortDateString() + "#";
                    SearchCmd += "LEFT(Import_Date,6) =\"" + (((firstDayOfMonth.Split('/'))[2]).Split(' '))[0] + (((firstDayOfMonth.Split('/'))[0]).Length == 1 ? "0" + (firstDayOfMonth.Split('/'))[0] : (firstDayOfMonth.Split('/'))[1]) + "\"";
                    //else SearchCmd += "1 = 2";
                    break;
                case "Reverse current Filter":
                    SearchCmd = "SELECT * FROM Main WHERE ID not IN (" + oldSearchCmd.Replace("*", "ID") + ") ORDER BY " + c("dlcm_OrderOfFields") + "";
                    break;
                case "Packed Last":
                    DataSet dzs = new DataSet(); dzs = SelectFromDB("LogPacking", "SELECT top 1 Pack FROM LogPacking order by ID DESC;", "", cnb);
                    noOfRec = dzs.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPacking WHERE Pack='" + dzs.Tables[0].Rows[0].ItemArray[0].ToString() + "')";
                    else SearchCmd += "1 = 2";
                    break;
                case "Packing Errors":
                    DataSet dks = new DataSet(); dks = SelectFromDB("LogPackingError", "SELECT top 1 Pack FROM LogPackingError order by ID DESC;", "", cnb);

                    noOfRec = dks.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPackingError WHERE Pack='" + dks.Tables[0].Rows[0].ItemArray[0].ToString() + "')";
                    else SearchCmd += "1 = 2";
                    break;
                case "Same DLCName":
                    //SLOW var SearchCmd5 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.DLC_Name) = LCASE(Main_1.DLC_Name) AND Main.ID <> Main_1.ID";
                    DataSet dos = new DataSet(); dos = SelectFromDB("Main", "SELECT m.ID,DLC_Name FROM Main AS m ORDER BY LCASE(DLC_Name)", "", cnb);
                    noOfRec = dos.Tables.Count == 0 ? 0 : dos.Tables[0].Rows.Count;
                    var IDg = ""; var ttt = 0;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            //{
                            for (var v = l + 1; v < noOfRec; v++)
                            {
                                ttt++;
                                if (dos.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dos.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    IDg += dos.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dos.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                else break;
                            }

                    var SearchCmd5 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDg + ")";
                    SearchCmd5 = SearchCmd5.Replace(", )", ")");
                    OrderAlt = "LCASE(DLC_Name), LCASE(Artist), LCASE(Song_Title)";

                    SearchCmd += "ID IN (" + SearchCmd5 + ")";
                    break;
                case "Same Title&Artist":
                    //var SearchCmd55 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID) AND (LCASE(n.Song_Title) = LCASE(m.Song_Title) and LCASE(n.Artist) = LCASE(m.Artist)) WHERE n.ID is not NULL";
                    //SearchCmd += "ID IN (" + SearchCmd55 + ")";
                    //break;
                    DataSet dqs = new DataSet(); dqs = SelectFromDB("Main", "SELECT m.ID,m.Song_Title, m.Artist FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title)", "", cnb);
                    noOfRec = dqs.Tables.Count == 0 ? 0 : dqs.Tables[0].Rows.Count;
                    var IDu = ""; var tts = 0;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                        {
                            var st = dqs.Tables[0].Rows[l].ItemArray[1].ToString();
                            for (var v = l + 1; v < noOfRec; v++)
                            {
                                tts++;
                                if (dqs.Tables[0].Rows[l].ItemArray[2].ToString().ToLower() == dqs.Tables[0].Rows[v].ItemArray[2].ToString().ToLower())
                                    //{
                                    if (dqs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dqs.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                        IDu += dqs.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dqs.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                    //}
                                    else break;
                            }
                        }

                    var SearchCmd75 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDu + ")";
                    SearchCmd75 = SearchCmd75.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_Title)";

                    SearchCmd += "ID IN (" + SearchCmd75 + ")";
                    break;
                case "Same Title(no[])&Artist":
                    DataSet dns = new DataSet(); dns = SelectFromDB("Main", "SELECT m.ID, Artist,m.Song_Title FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title)", "", cnb);
                    noOfRec = dns.Tables.Count == 0 ? 0 : dns.Tables[0].Rows.Count;
                    var IDf = ""; bool done = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                        {
                            for (var v = l + 1; v < noOfRec; v++)
                            {
                                if (dns.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dns.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitle(dns.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitle(dns.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
                                        IDf += dns.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dns.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                }
                                else break;
                            }
                        }

                    var SearchCmd45 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDf + ")";
                    SearchCmd45 = SearchCmd45.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_Title)";

                    SearchCmd += "ID IN (" + SearchCmd45 + ")";
                    break;
                case "Same Artist&Album different Year":
                    //var SearchCmdr5 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID AND LCASE(n.Artist) = LCASE(m.Artist) AND LCASE(n.Album) = LCASE(m.Album) AND n.Album_Year <> m.Album_Year) WHERE n.ID is not NULL";
                    //SearchCmd += "ID IN (" + SearchCmdr5 + ")";
                    //break;
                    DataSet das = new DataSet(); das = SelectFromDB("Main", "SELECT m.ID, m.Artist, m.Album, m.Album_Year FROM Main AS m ORDER BY LCASE(Artist), LCASE(Album), Album_Year", "", cnb);
                    noOfRec = das.Tables.Count == 0 ? 0 : das.Tables[0].Rows.Count;
                    var IDr = "";
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                //{
                                if (das.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == das.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (das.Tables[0].Rows[l].ItemArray[2].ToString().ToLower() == das.Tables[0].Rows[v].ItemArray[2].ToString().ToLower())
                                    {
                                        if (das.Tables[0].Rows[l].ItemArray[3].ToString() != das.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())
                                            //{
                                            IDr += das.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + das.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                        //dones = true;
                                        //}
                                    }
                                    else break; //if (dones) { done = false; break; }
                                                //    }

                    //}
                    //}

                    var SearchCmdr5 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDr + ")";
                    SearchCmdr5 = SearchCmdr5.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Album), Album_Year, Song_Title";

                    SearchCmd += "ID IN (" + SearchCmdr5 + ")";
                    break;
                case "Same Artist&Title(no[]) different Album":
                    DataSet dws = new DataSet(); dws = SelectFromDB("Main", "SELECT m.ID,m.Artist, m.Song_Title, m.Album FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title), LCASE(Album)", "", cnb);
                    noOfRec = dws.Tables.Count == 0 ? 0 : dws.Tables[0].Rows.Count;
                    var IDc = ""; var dones = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dws.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dws.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (CleanTitle(dws.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitle(dws.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
                                    {
                                        if (dws.Tables[0].Rows[l].ItemArray[3].ToString().ToLower() != dws.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())
                                            IDc += dws.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dws.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                    }
                                    else break;

                    var SearchCmd41 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDc + ")";
                    SearchCmd41 = SearchCmd41.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_Title), LCASE(Album)";

                    SearchCmd += "ID IN (" + SearchCmd41 + ")";
                    break;
                //var SearchCmdv5 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID) AND (n.Artist = m.Artist) AND (n.Song_Title like %m.Song_Title%) AND (n.Album <> m.Album)";
                //SearchCmd += "ID IN (" + SearchCmdv5 + ")";
                //break;
                case "Same Artist&Title(no[]) different Year":
                    DataSet dts = new DataSet(); dts = SelectFromDB("Main", "SELECT m.ID, m.Artist, m.Song_Title, m.Album_Year FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_title), Album_Year", "", cnb);
                    noOfRec = dts.Tables.Count == 0 ? 0 : dts.Tables[0].Rows.Count;
                    var IDe = ""; var donez = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                //{
                                if (dts.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dts.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitle(dts.Tables[0].Rows[l].ItemArray[2].ToString()) == CleanTitle(dts.Tables[0].Rows[v].ItemArray[2].ToString()))
                                        if (dts.Tables[0].Rows[l].ItemArray[3].ToString() != dts.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())
                                            IDe += dts.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dts.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                }
                                else if (donez) { done = false; break; }

                    var SearchCmd40 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDe + ")";
                    SearchCmd40 = SearchCmd40.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_title), Album_Year";

                    SearchCmd += "ID IN (" + SearchCmd40 + ")";
                    break;
                case "Songs IMPORTED later than current song value":
                    //DateTime dates = DateTime.Today;
                    //var firstDayOfMonth = (new DateTime(date.Year, date.Month, 1)).ToString();
                    string d = databox.Rows[i].Cells["Import_Date"].Value.ToString();
                    SearchCmd += "CINT(LEFT(Import_Date,4)) >=" + d.Substring(0, 4) + "";
                    SearchCmd += " AND CINT(RIGHT(LEFT(Import_Date,6),2)) >=" + d.Substring(4, 2) + "";
                    SearchCmd += " AND CINT(RIGHT(LEFT(Import_Date,8),2)) >=" + d.Substring(6, 2) + "";
                    break;
                case "Sorted by Groups value/Group added date":
                    //var SearchCmd8 = "SELECT ID FROM Main"; //"LEFT JOIN Groups AS mn ON Groups=\"" + chbx_Group.Text + "\" AND Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                    //string ddv = databox.Rows[i].Cells["Import_Date"].Value.ToString();
                    //SearchCmd8 += "CINT(LEFT(Date_Added,4)) >=" + ddv.Substring(0, 4) + "";
                    //SearchCmd8 += " AND CINT(RIGHT(LEFT(Date_Added,6),2)) >=" + ddv.Substring(4, 2) + "";
                    //SearchCmd8 += " AND CINT(RIGHT(LEFT(Date_Added,8),2)) >=" + ddv.Substring(6, 2) + "";

                    //SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields")+", mn.Date_Added");
                    if (chbx_Group.Text == "")
                    {
                        DialogResult result1 = MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return;
                    }
                    SearchCmd += "ID IN (" + oldSearchCmd + ")";
                    //SearchCmd += SearchCmd8+ " DESC mn.Date_Added";
                    break;
                case "Part of No Group":
                    var SearchCmd9 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\"";

                    SearchCmd += "ID NOT IN (" + SearchCmd9 + ")";
                    break;
                case "Part of Any Group":
                    var SearchCmd10 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\"";
                    SearchCmd += "ID IN (" + SearchCmd10 + ")";
                    break;
                case "Songs ADDED later to the Groups value/group than the import date of the current song value":
                    if (chbx_Group.Text == "")
                    {
                        DialogResult result1 = MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return;
                    }
                    var SearchCmd7 = "SELECT CDLC_ID as IDs FROM Groups AS m WHERE Groups=\"" + chbx_Group.Text + "\" AND Type=\"DLC\" AND ";// CDLC_ID=\"" + txt_ID+"\"";
                    string dd = databox.Rows[i].Cells["Import_Date"].Value.ToString();
                    SearchCmd7 += "CINT(LEFT(Date_Added,4)) >=" + dd.Substring(0, 4) + "";
                    SearchCmd7 += " AND CINT(RIGHT(LEFT(Date_Added,6),2)) >=" + dd.Substring(4, 2) + "";
                    SearchCmd7 += " AND CINT(RIGHT(LEFT(Date_Added,8),2)) >=" + dd.Substring(6, 2) + "";

                    SearchCmd += "ID IN (" + SearchCmd7 + ")";
                    break;
                case "Sorted by Last Packdate":
                    var SearchCmd17 = "SELECT DISTINCT CDLC_ID as IDs FROM Pack_AuditTrail ";

                    SearchCmd += "ID IN (" + SearchCmd17 + ")";
                    break;
                //case "READ GAMEDATA":
                default:
                    break;
            }

            if (Filtertxt.IndexOf("Tuning ") > -1)
            {
                var SearchCmd8 = "SELECT CDLC_ID FROM Arrangements WHERE LCASE(Tunning)=LCASE(\"" + Filtertxt.Replace("Tuning ", "") + "\")";// CDLC_ID=\"" + txt_ID+"\"";

                SearchCmd += "ID IN (" + SearchCmd8 + ")";
            }

            var Filterorg = Filtertxt;
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filterorg)) SearchCmd = SearchCmd.Replace("Main.", "");/*, "Part of No Group", "Part of Any Group" */
            if (Filtertxt.Length > 5) Filtertxt = cmb_Filter.Text.Substring(0, 5);

            switch (Filtertxt)
            {
                case "Group":

                    var SearchCmd6 = "SELECT m.CDLC_ID FROM Groups AS m WHERE m.CDLC_ID = CSTR(u.ID) AND m.Groups =  '" + cmb_Filter.Text.Substring(6, cmb_Filter.Text.Length - 6) + "'";
                    SearchCmd += "CSTR(u.ID) IN (" + SearchCmd6 + ")";
                    break;
                default:
                    break;
            }

            var fields = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM")) : SearchCmd.Length - 1).Replace("SELECT ", "");
            if (chbx_FilterCompound.Checked)
            {
                if (chbx_FilterNot.Checked)
                {
                    SearchCmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM") + 6) : SearchCmd.Length - 1)
                        + "(" + oldSearchCmd + ") WHERE ID not IN (" + SearchCmd.Replace(fields, "ID") + ") ORDER BY " + c("dlcm_OrderOfFields") + "";
                    chbx_FilterNot.Checked = false;
                }
                else
                    SearchCmd = ((SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM") + 6) : SearchCmd.Length - 1)
                        + "(" + oldSearchCmd + ") WHERE ID IN (" + SearchCmd.Replace(c("dlcm_SearchFields"), "ID")).Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "") + ") ORDER BY " + c("dlcm_OrderOfFields") + "").Replace(";", "");
                SearchCmd = (SearchCmd.Length - SearchCmd.Replace("(", "").Length) == (SearchCmd.Length - SearchCmd.Replace(")", "").Length) ? SearchCmd : SearchCmd + ")";
                chbx_FilterCompound.Checked = false;
            }
            else if (chbx_FilterNot.Checked)
            {
                SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE ID IN (" + oldSearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "")
                    + ") and ID NOT IN (" + SearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "") + ") ORDER BY " + c("dlcm_OrderOfFields");
                SearchCmd = SearchCmd.Replace("Maiu", "Main u");
                chbx_FilterNot.Checked = false;
            }

            //Speed up the re-run of any Query by only providing list of IDs
            var cmd = "SELECT ID FROM (" + SearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace("ORDER BY " + c("dlcm_OrderOfFields"), "") + ") order by ID DESC";
            cmd = cmd.Replace("Maiu", "Main u");
            DataSet dms = new DataSet(); dms = SelectFromDB("Main", cmd, "", cnb);
            noOfRec = dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
            var IDS = "0, ";
            if (noOfRec > 0)
                //{            }
                for (var l = 0; l < noOfRec; l++)
                    IDS += dms.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";

            SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE u.ID IN (" + IDS + ")";
            SearchCmd = SearchCmd.Replace(", )", ")");

            if (Filterorg == "Sorted by Groups value/Group added date")
            {
                SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", Main.")).Replace(" ID", " Main.ID");
                SearchCmd = SearchCmd.Replace(" FROM Main u", ", Groups.Date_Added FROM Main LEFT JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");

                //"" +" AND ((Groups.Date_Added  Is Not Null))" +
                //"INNER JOIN Groups ON Main.ID = val(Groups.CDLC_ID) WHERE Groups.Groups = \"" + txt_Groups.Text + "\"";
                //"LEFT JOIN Groups AS mn ON Groups=\"" + chbx_Group.Text + "\" AND Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                SearchCmd += " AND \"" + chbx_Group.Text + "\" = Groups.Groups ORDER BY Groups.Date_Added DESC; ";// + " WHERE Groups.Date_Added is not NULL ORDER BY Groups.Date_Added DESC"; //SearchCmd.Replace(c("dlcm_OrderOfFields"), "")
            }
            else if (Filterorg == "Sorted by Last Packdate")
            {
                //M#Access Select is not working on the left join and sort from tool, altough oworking from Access Query screen
                SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", u.")).Replace(" ID", " u.ID");
                SearchCmd = SearchCmd.Replace(" FROM Main u", ", MAX(PA.ID) AS AdditionalSortColumn FROM Main u " +
                    "LEFT JOIN (SELECT * FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\") AS PA ON u.ID = PA.CDLC_ID");
                SearchCmd += " GROUP BY " + (c("dlcm_SearchFields").Replace(", ", ", u.")) + " ORDER BY MAX(PA.ID) DESC";
                SearchCmd = SearchCmd.Replace(" ID", " u.ID");
                SearchCmd = "SELECT * FROM (" + SearchCmd + ") ORDER BY Groups,AdditionalSortColumn DESC;";

                //Speed up the re-run of any Query by only providing list of IDs
                //cmd = "SELECT * FROM (" + SearchCmd.Replace(";", "") + ") ORDER BY AdditionalSortColumn DESC;";//.Replace(c("dlcm_SearchFields"), "ID").Replace("ORDER BY " + c("dlcm_OrderOfFields"), "") + ") order by ID DESC";
                //cmd = cmd.Replace("Maiu", "Main u");
                //DataSet dts = new DataSet(); dts = SelectFromDB("Main", cmd, "", cnb);
                //noOfRec = dts.Tables.Count == 0 ? 0 : dts.Tables[0].Rows.Count;
                //IDS = "0, ";
                //if (noOfRec > 0)
                //    //{            }
                //    for (var l = 0; l < noOfRec; l++)
                //        IDS += dts.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";

                //SearchCmd = "SELECT * FROM Main u WHERE u.ID IN (" + IDS + ") ";/*ORDER BY AdditionalSortColumn DESC*/
                //SearchCmd = SearchCmd.Replace(", )", ")");


            }
            //if (Filterorg == "Part of No Group")
            //{
            //    SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", Main.")).Replace(" ID", " Main.ID");
            //    SearchCmd = SearchCmd.Replace(" FROM Main u", ", Groups.Date_Added FROM Main OUTTER JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");
            //}
            //if (Filterorg == "Part of Any Group")
            //{
            //    SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", Main.")).Replace(" ID", " Main.ID");
            //    SearchCmd = SearchCmd.Replace(" FROM Main u", " FROM Main INNER JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");
            //}
            else if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY " + (OrderAlt != "" ? OrderAlt : c("dlcm_OrderOfFields")) + " ";
            else SearchCmd += SearchCmd.Replace(c("dlcm_OrderOfFields"), "") + (OrderAlt != "" ? OrderAlt : c("dlcm_OrderOfFields")) + " ";

            try
            {
                dssx.Dispose();
                Populate(ref databox, ref Main);
                databox.Refresh();
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message + "Can't run Filter ! " + SearchCmd);
            }

            if (Filterorg == "Sorted by Groups value/Group added date") SearchCmd = SearchCmdf;

            //Update_Selected();
            SaveOK = c("dlcm_Autosave") == "Yes" ? true : false;
        }

        private void txt_Platform_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_AllGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_RemoveSelectedRemote_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath+\"\\\"+FileName = \"" + cmb_Packed.Text + "\"";
            DeleteFile(cmb_Packed.Text);
            cmb_Packed.Items.RemoveAt(cmb_Packed.SelectedIndex);
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            CleanPack();
            DeleteMultiInstances();
        }
        private void CleanPack()
        {
            var cmd1 = "UPDATE Main SET Split4Pack = \"\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);
        }
        private void DeleteMultiInstances()
        {
            for (var j = 1; j < 100; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                if (Directory.Exists(dest)) DeleteDirectory(dest);
            }
        }
        private void cMS_RightClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (tlSMI_Next.) ;
        }

        private void txt_Track_No_ValueChanged(object sender, EventArgs e)
        {
            if (txt_Track_No.Text != null) chbx_TrackNo.Checked = true;
            else chbx_TrackNo.Checked = false;
        }

        private void tlSMI_Beta_Click(object sender, EventArgs e)
        {
            var j = mouseLocation.RowIndex;

            var i = databox.SelectedCells[0].RowIndex;

            //Read from DB 
            var sel = "";

            for (int k = 1; k < databox.SelectedRows.Count; k++)
            {
                if (k > 1) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            var SongRecord = GenericFunctions.GetRecord_s(sel, cnb);
            var norows = SongRecord[0].NoRec.ToInt32();

            if (i == j) chbx_Beta.Checked = chbx_Beta.Checked ? false : true;
            else
            {
                var updatecmd = "UPDATE Main SET Is_Beta=\"Yes\" WHERE ID in (" +sel + ")";// databox.Rows[j].Cells["ID"].Value.ToString()
                UpdateDB("Main", updatecmd, cnb);
            }
        }

        private void tlSMI_Next_Click(object sender, EventArgs e)
        {
            var prev = mouseLocation.RowIndex;// databox.SelectedCells[0].RowIndex;
            if (databox.Rows.Count <= prev + 2) return;

            var j = databox.SelectedCells[0].RowIndex;
            if (chbx_AutoSave.Checked) SaveRecord();

            //int rowindex;
            var i = mouseLocation.RowIndex; ;// databox.SelectedCells[0].RowIndex;
            //rowindex = i;
            databox.Rows[j].Selected = false;
            databox.CurrentCell = databox.Rows[i + 1].Cells[0];
            databox.CurrentCell.Selected = true;
            databox.Rows[i + 1].Selected = true;
        }

        private void tlSMI_Pack_Click(object sender, EventArgs e)
        {
            btn_Package_Click(sender, e);
            //return;
            //var old = ConfigRepository.Instance()["dlcm_MuliThreading"];
            //if (chbx_Format.Text == "<PS4>" || chbx_Format.Text == "<iOS>") { MessageBox.Show("Not yet working. rEtail songs can be eliminated just new ones packaged\n\nWiP"); return; }
            //pB_ReadDLCs.Value = 0;
            //pB_ReadDLCs.Maximum = 4;
            //pB_ReadDLCs.Increment(1);
            //if (chbx_AutoSave.Checked) SaveRecord();
            //savesettings();
            //pB_ReadDLCs.Increment(1);
            //ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "No";

            //var Temp_Path_Import = c("dlcm_TempPath");
            //var old_Path_Import = c("dlcm_TempPath") + "\\0_old";
            //var dflt_Path_Import = c("dlcm_TempPath") + "\\0_to_import";
            //var dataPath = c("dlcm_TempPath") + "\\0_data";
            //var broken_Path_Import = c("dlcm_TempPath") + "\\0_broken";
            //var dupli_Path_Import = c("dlcm_TempPath") + "\\0_duplicate";
            //var dlcpacks = c("dlcm_TempPath") + "\\0_dlcpacks";
            //var repacked_Path = c("dlcm_TempPath") + "\\0_repacked";
            //var repacked_XBOXPath = c("dlcm_TempPath") + "\\0_repacked\\XBOX360";
            //var repacked_PCPath = c("dlcm_TempPath") + "\\0_repacked\\PC";
            //var repacked_MACPath = c("dlcm_TempPath") + "\\0_repacked\\MAC";
            //var repacked_PSPath = c("dlcm_TempPath") + "\\0_repacked\\PS3";
            //var log_Path = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
            //var AlbumCovers_PSPath = c("dlcm_TempPath") + "\\0_albumCovers";
            //var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            //var Temp_Path = c("dlcm_TempPath") + "\\0_temp";
            //string pathDLC = c("dlcm_RocksmithDLCPath");
            //CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);

            ////// Generate package worker
            //bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            //bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            //bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            //bwRGenerate.WorkerReportsProgress = true;

            //var i = mouseLocation.RowIndex;// databox.SelectedCells[0].RowIndex;
            //Groupss = chbx_Group.Text.ToString();
            //var cmd = "SELECT * FROM Main ";
            //cmd += "WHERE ID = " + databox.Rows[i].Cells["ID"].Value.ToString() + "";

            ////Read from DB
            //MainDBfields[] SongRecord = new MainDBfields[10000];
            //SongRecord = GetRecord_s(cmd, cnb);
            //if (netstatus == "NOK" || netstatus == "") netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();

            //string spotystatus = "", ybstatus = "", ftpstatus = "";
            //if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")) if (FTPAvail(txt_FTPPath.Text).ToLower() == "ok") ftpstatus = "ok";/*"", ConfigRepository.Instance()["dlcm_FTP" + ConfigRepository.Instance()["dlcm_FTP"]]*/
            //    else ftpstatus = "nok";

            //if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackagePreparation();

            ////var i = 0;
            ////var bassRemoved = "No";
            //foreach (var filez in SongRecord)
            //{
            //    if (filez.ID == null) break;
            //    var args = filez.ID + ";" + "false" + ";";
            //    args += (chbx_Format.Text == "PC" ? "PC" : "") + ";" + (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU" ? "PS3" : "") + ";" + (chbx_Format.Text == "XBOX360" ? "XBOX360" : "") + ";" + (chbx_Format.Text == "Mac" ? "Mac" : "") + ";" + netstatus + ";";
            //    args += chbx_Beta.Checked + ";" + chbx_Group.Text + ";";
            //    args += Groupss + ";" + c("dlcm_TempPath") + ";";
            //    args += chbx_UniqueID.Checked + ";" + chbx_Last_Packed.Checked + ";";
            //    args += chbx_Last_Packed.Enabled + ";" + chbx_CopyOld.Checked + ";";
            //    args += chbx_CopyOld.Enabled + ";" + chbx_Copy.Checked + ";";
            //    args += chbx_Replace.Checked + ";" + chbx_Replace.Enabled + ";";
            //    args += SourcePlatform + ";" + "MainDB" + ";";
            //    args += filez.Original_FileName + ";" + filez.Folder_Name + ";";
            //    args += c("dlcm_AdditionalManipul49") + ";" + txt_RemotePath.Text + ";" + txt_FTPPath.Text + ";";
            //    args += chbx_RemoveBassDD.Checked + ";" + chbx_BassDD.Checked + ";" + chbx_KeepBassDD.Checked + ";";
            //    args += chbx_KeepDD.Checked + ";" + chbx_Original.Text + ";" + filez.ID + ";";
            //    args += SearchCmd + (SearchCmd.IndexOf(";") > 0 ? "" : ";") + pathDLC + ";" + filez.DLC_Name + ";";
            //    args += c("dlcm_AdditionalManipul76") + ";" + "" + ";" + "MainDB" + ";" + "" + ";" + spotystatus + ";" + ybstatus + ";" + ftpstatus; ;
            //    pB_ReadDLCs.Increment(1);
            //    bwRGenerate.RunWorkerAsync(args);
            //    do
            //        Application.DoEvents();
            //    while (bwRGenerate.IsBusy && !bwRGenerate.CancellationPending);//keep singlethread as toolkit not multithread abled
            //}
            //pB_ReadDLCs.Increment(1);

            //if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackage();
            //ConfigRepository.Instance()["dlcm_MuliThreading"] = old;
        }

        private DataGridViewCellEventArgs mouseLocation;
        private void tlSMI_Selected_Click(object sender, EventArgs e)
        {
            var j = mouseLocation.RowIndex;
            var i = databox.SelectedCells[0].RowIndex;
            if (i == j) chbx_Selected.Checked = chbx_Selected.Checked ? false : true;
            else
            {
                //Read from DB 
                var sel = "";

                for (int k = 1; k < databox.SelectedRows.Count; k++)
                {
                    if (k > 1) sel += ", ";
                    sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
                }
                var updatecmd = "UPDATE Main SET Selected=\"Yes\" WHERE ID in (" + sel + ")";
                UpdateDB("Main", updatecmd, cnb);// if (!File.Exists(XMLFilePath + ".old2")) File.Copy(XMLFilePath, newXMLFilePath, true)
            }
        }

        private void btn_AddInstrumental_Click(object sender, EventArgs e)
        {
            var fn = AppWD + "\\lyric.xml";
            FileStream swt = File.Open(fn, FileMode.Create);
            var i = databox.SelectedCells[0].RowIndex;

            swt.Dispose();
            using (StreamWriter sw = File.AppendText(fn))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n<vocals count = \"1\"" +
                    ">\n\t<vocal time=\"10\" note=\"254\" length=\"" + (Decimal.Parse(databox.Rows[i].Cells["Song_Lenght"].Value.ToString()) - 15)
                    + "\" lyric=\"Instrumental\" />\n</vocals>");// This text is always added, making the file longer over time if it is not deleted.
            }
            //ApplyLyric(fn, databox.Rows[i].Cells["Folder_Name"].Value.ToString());
            ApplyLyric(fn, databox.Rows[i].Cells["Folder_Name"].Value.ToString(), cnb, txt_Platform.Text.ToString().Replace("PC", "Pc")
                , txt_ID.Text, chbx_Lyrics.Checked);
            txt_Lyrics.Text =
               txt_Lyrics.Text = fn.IndexOf(databox.Rows[i].Cells["Folder_Name"].Value.ToString()) < 0 ? databox.Rows[i].Cells["Folder_Name"].Value.ToString() + (txt_Platform.Text.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\arr\\" + Path.GetFileName(fn) : fn;
            chbx_LyricsChanged.Checked = true;
            chbx_Lyrics.Checked = true;
            btn_ShowLyrics.Enabled = true;
            btn_CreateLyrics.Enabled = false;
            DeleteFile(fn);
            chbx_ImprovedWithDM.Checked = true;
            MessageBox.Show("Empty track added");
        }

        private void databox_SelectionChanged(object sender, EventArgs e)
        {
            var tst = "Start Selection Leave... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (ChangeRows) ChangeRow();
            tst = "Stop Selection Leave... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //refresh 
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void btn_SteamDLCFolder_Click_1(object sender, EventArgs e)
        {
            if (chbx_Format.Text.IndexOf("PS3") >= -1) return;
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;

                txt_FTPPath.Text = fbd.SelectedPath;
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bth_ShiftVocalNotes_Click(object sender, EventArgs e)
        {
            var tsst = "";
            var ArrangID = cmb_Tracks.Text.Substring(cmb_Tracks.Text.IndexOf("_ArrangementID="), cmb_Tracks.Text.Length - cmb_Tracks.Text.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
            var DLCID = cmb_Tracks.Text.Substring(8, cmb_Tracks.Text.IndexOf("_") - 8);

            var i = databox.SelectedCells[0].RowIndex;
            string destination_dir = databox.Rows[i].Cells["Folder_Name"].Value.ToString() + (txt_Platform.Text.ToLower() == "XBOX360".ToLower() ? "\\Root" : "");

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName, RouteMask, Start_Time, JSONFilePath,SNGFileName  FROM Arrangements WHERE ID=" + ArrangID + "", "", cnb);
            var noOfRec = dus.Tables[0].Rows.Count;
            var XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            var SNGFileName = dus.Tables[0].Rows[0].ItemArray[5].ToString();
            string json = dus.Tables[0].Rows[0].ItemArray[4].ToString();
            var newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
            var newJSONFilePath = destination_dir + "\\manifests" + json.Substring(json.IndexOf("\\manifests") + 10, json.Length - json.IndexOf("\\manifests") - 10);// json.LastIndexOf("\\") + ".json";
            var newSNGFilePath = destination_dir +
                (destination_dir.IndexOf("\\Ps3\\") > 0 ? "\\songs\\bin\\Ps3\\" : (destination_dir.IndexOf("\\Mac\\") > 0 ? "\\songs\\bin\\Mac\\" : "\\songs\\bin\\generic\\")) +
                SNGFileName + ".sng";
            var sng = XMLFilePath.Substring(0, XMLFilePath.IndexOf("\\songs\\")) +
                (XMLFilePath.IndexOf("\\Ps3\\") > 0 ? "\\songs\\bin\\Ps3\\" : (XMLFilePath.IndexOf("\\Mac\\") > 0 ? "\\songs\\bin\\Mac\\" : "\\songs\\bin\\generic\\")) +
                SNGFileName + ".sng";

            var s2s = float.Parse(num_Lyrics.Value.ToString());

            if (cmb_Tracks.Text.ToLower().IndexOf("vocal") >= 0)
            {

                if (txt_ID.Text != DLCID)
                {
                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID.Text + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                        "instr(JSONFilePath, '\\manifests')-10), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, \"Added from " + DLCID + " " + txt_Title.Text + "\", (Start_Time+" + s2s + "), CleanedXML_Hash," +
                        " Json_Hash, Part, MaxDifficulty FROM Arrangements WHERE ID = " + ArrangID;

                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0);
                }
                else
                {
                    var updatecmd = "UPDATE Arrangements SET Comments=Comments+\" shifted by " + num_Lyrics.Value.ToString()
                        + "\", Start_Time=Start_Time+" + num_Lyrics.Value.ToString() + " WHERE ID = " + ArrangID;
                    UpdateDB("Arrangements", updatecmd, cnb);// if (!File.Exists(XMLFilePath + ".old2")) File.Copy(XMLFilePath, newXMLFilePath, true);
                }

                try
                {
                    if (!File.Exists(newXMLFilePath + ".old2") && File.Exists(newXMLFilePath)) File.Copy(newXMLFilePath, newXMLFilePath + ".old2", true);
                    if (txt_ID.Text != DLCID) File.Copy(XMLFilePath, newXMLFilePath, true);
                    //if (!File.Exists(newSNGFilePath + ".old2") && File.Exists(newSNGFilePath)) File.Copy(newSNGFilePath, newSNGFilePath + ".old2", true);
                    //if (txt_ID.Text != DLCID) File.Copy(sng, newSNGFilePath, true);
                    //if (!File.Exists(newJSONFilePath + ".old2") && File.Exists(newJSONFilePath)) File.Copy(newJSONFilePath, newJSONFilePath + ".old2", true);
                    //if (txt_ID.Text != DLCID) File.Copy(json, newJSONFilePath, true);
                    XMLFilePath = newXMLFilePath;
                }
                catch (Exception ex)
                {
                    tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }

                Vocals xmlContent = null;
                if (XMLFilePath != "")
                    try
                    {
                        xmlContent = Vocals.LoadFromFile(XMLFilePath);
                        for (var j = 0; j < xmlContent.Vocal.Length; j++)
                            xmlContent.Vocal[j].Time = xmlContent.Vocal[j].Time + float.Parse(num_Lyrics.Value.ToString());

                        using (var stream = File.Open(XMLFilePath, FileMode.Create))
                            xmlContent.Serialize(stream);
                        cleanlyrics(txt_ID.Text, cnb);
                        var updatecmdd = "UPDATE Arrangements SET XMLFile_Hash=\"" + GetHash(XMLFilePath) + "\" WHERE ID = " + ArrangID;
                        UpdateDB("Arrangements", updatecmdd, cnb);
                        //updatecmdd = "UPDATE Main SET Has_Vocal=\"Yes\" WHERE ID = " + ArrangID;
                        //UpdateDB("Main", updatecmdd, cnb);
                    }
                    catch (Exception ex) { tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

                chbx_Lyrics.Checked = true;
            }
            else
            {

                var bonus = "bonus";
                if (txt_ID.Text != DLCID)
                {
                    var cmd = "SELECT XMLFilePath, XMLFileName, RouteMask, Start_Time, JSONFilePath,SNGFileName FROM Arrangements WHERE ID = " + ArrangID + " AND Bonus =\"True\" and RoueMask=\"" + dus.Tables[0].Rows[0].ItemArray[2].ToString() + "\"";
                    DataSet dis = new DataSet(); dis = SelectFromDB("Arrangements", cmd, "", cnb);
                    noOfRec = dis.Tables.Count <= 0 ? 0 : dis.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                    {
                        DialogResult result1 = MessageBox.Show("Normal Bonus track alreaday existing. Not adding current proposed teack. ", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    else
                        bonus = "True";
                    //bonus = "false";

                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID.Text + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                        "instr(JSONFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, \"Added from " + DLCID + " " + txt_Title.Text + "\", (Start_Time+" + s2s + "), CleanedXML_Hash," +
                        " Json_Hash, Part, MaxDifficulty FROM Arrangements WHERE ID = " + ArrangID;

                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0);

                    try
                    {
                        if (!File.Exists(newXMLFilePath + ".old2") && File.Exists(newXMLFilePath)) File.Copy(newXMLFilePath, newXMLFilePath + ".old2", true);
                        File.Copy(XMLFilePath, newXMLFilePath, true);
                        if (!File.Exists(newJSONFilePath + ".old2") && File.Exists(newJSONFilePath)) File.Copy(newJSONFilePath, newJSONFilePath + ".old2", true);
                        File.Copy(json, newJSONFilePath, true);
                        //if (!File.Exists(newSNGFilePath + ".old2") && File.Exists(newSNGFilePath)) File.Copy(newSNGFilePath, newSNGFilePath + ".old2", true);
                        //if (txt_ID.Text != DLCID) File.Copy(sng, newSNGFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
                else newXMLFilePath = XMLFilePath;

                try
                {
                    if (!File.Exists(newXMLFilePath + ".old3")) File.Copy(newXMLFilePath, newXMLFilePath + ".old3", true);
                    Song2014 xmlContent = null;
                    if (XMLFilePath != "")
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(newXMLFilePath);
                            xmlContent.SongLength += s2s;
                            for (var j = 0; j < xmlContent.PhraseIterations.Length; j++)
                                if (xmlContent.PhraseIterations[j].Time > 0) xmlContent.PhraseIterations[j].Time += s2s;
                            for (var j = 0; j < xmlContent.Ebeats.Length; j++)
                                if (xmlContent.Ebeats[j].Time > 0) xmlContent.Ebeats[j].Time += s2s;
                            for (var j = 0; j < xmlContent.Sections.Length; j++)
                                if (xmlContent.Sections[j].StartTime > 0) xmlContent.Sections[j].StartTime += s2s;
                            for (var j = 0; j < xmlContent.Levels.Length; j++)
                            {
                                for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                                    if (xmlContent.Levels[j].Notes[k].Time > 0) xmlContent.Levels[j].Notes[k].Time += s2s;
                                for (var k = 0; k < xmlContent.Levels[j].Anchors.Length; k++)
                                    if (xmlContent.Levels[j].Anchors[k].Time > 0) xmlContent.Levels[j].Anchors[k].Time += s2s;
                            }
                        }
                        catch (Exception ex)
                        {
                            tsst = "Error shifting timings..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        }

                    using (var stream = File.Open(newXMLFilePath, FileMode.Create)) xmlContent.Serialize(stream);

                    if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Bass") chbx_Bass.Checked = true;
                    else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Lead") chbx_Lead.Checked = true;
                    else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Rhythm") chbx_Rhythm.Checked = true;
                    else;
                    //? RouteMask.Bass : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Lead" ? RouteMask.Lead : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "None" ? RouteMask.None : RouteMask.Any;

                    var updatecmdd = "UPDATE Arrangements SET Comments=Comments+\" shifted by " + num_Lyrics.Value.ToString()
                        + "\", Start_Time=Start_Time+" + num_Lyrics.Value.ToString()
                        + ", XMLFile_Hash=" + GetHash(XMLFilePath) + ", Bonus=\"" + bonus + "\" WHERE ID = " + ArrangID; ;
                    UpdateDB("Arrangements", updatecmdd, cnb);
                }
                catch (Exception ex)
                {
                    tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    if (!File.Exists(newXMLFilePath + ".old3")) File.Copy(newXMLFilePath + ".old3", newXMLFilePath, true);
                }

            }

            if (tsst == "")
            {
                ListTracks(txt_DuplicateOf.Text, txt_ID.Text, databox.Rows[i].Cells["Song_Lenght"].Value.ToString());
                chbx_ImprovedWithDM.Checked = true;
                MessageBox.Show("Track " + dus.Tables[0].Rows[0].ItemArray[1].ToString() + "shifted by " + num_Lyrics.Value);
            }
            else
                MessageBox.Show("Track " + dus.Tables[0].Rows[0].ItemArray[1].ToString() + "was NOT shifted by " + num_Lyrics.Value);

            return;
        }

        public static void AudioBackgroundPlay(string t, bool cancel, string AppWD)
        {
            if (!File.Exists(t)) return;
            var args = "";
            if (cancel)
            {
                DDC.Refresh();  // Important
                if (DDC.StartInfo.FileName != "")
                {
                    if (DDC.HasExited) Console.WriteLine("Exited.");
                    else Console.WriteLine("Running.");
                    if (DDC.HasExited == false) if (ProcessStarted) DDC.Kill();
                }
                ProcessStarted = false;
            }// Cancel the asynchronous operation.
            else
            {
                ProcessStarted = true;
                args = t + ";" + AppWD;
                bwAutoPlay.RunWorkerAsync(args);
                do
                    Application.DoEvents();
                while (bwAutoPlay.IsBusy);//keep singlethread as toolkit not multithread abled
            }
        }

        public static async void PlayPreview(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument.ToString();
            string[] args = (e.Argument).ToString().Split(';');
            string OggPath = args[0];
            string AppWD = args[1];

            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggdec2.exe"),
                WorkingDirectory = AppWD
            };
            startInfo.Arguments = string.Format(" -p \"{0}\"", OggPath);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(OggPath))
            {
                DDC.StartInfo = startInfo;
                DDC.Start(); DDC.WaitForExit(1000 * 1 * 1); //wait 1min
            }
            return;
        }

        private void Txt_OldPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }

        private void Txt_Description_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }

        private void Txt_Live_Details_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }

        private void Txt_Author_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }
        void DetailsChanged(string s)
        {
            var i = databox.SelectedCells.Count > 0 ? databox.SelectedCells[0].RowIndex : 0;
            if (i > 0) if (txt_Artist.Text != databox.Rows[i].Cells[s].Value.ToString())
                    chbx_Has_Been_Corrected.Checked = true;
        }

        private void Txt_Artist_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Artist");
        }

        private void Txt_Album_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Album");
        }

        private void Txt_Title_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Song_Title");
        }

        private void Txt_Album_Year_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Album_Year");
        }

        private void Databox_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ;
        }

        private void Databox_Sorted(object sender, EventArgs e)
        {
            ;
        }

        private void Brn_CompactDB_Click(object sender, EventArgs e)
        {
            CompactAndRepair(cnb);
        }

        private void Btn_SearchLyrics_Click(object sender, EventArgs e)
        {
            //1. Open Internet Explorer
            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "Lyrics";

            try
            {
                Process process = Process.Start(@link);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }
        }

        private void Btn_SearchYB_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "rocksmith";

            try
            {
                Process process = Process.Start(@link);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }
        }

        private void Button3_Click_2(object sender, EventArgs e)
        {
            if (txt_Lyrics.Text != "") Process.Start("explorer.exe", txt_Lyrics.Text);
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_RockBand"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install C3 Conversion Tools if you want to use it.", c("dlcm_RockBand_www"), "Missing C3 Rockband conversion tools", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for decompressing rockbad songs ! " + xx);
            }
        }

        private void Btn_WinMerge_Click(object sender, EventArgs e)
        {
            var paath = c("dlcm_WinMerge");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "WinMerge\\winmergeu.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install WinMerge if you want to use it.", c("dlcm_WinMerge_www"), "Missing WinMerge", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;
            if (File.Exists(xx))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
        }

        public void Btn_ExportGuitarPro_Click(object sender, EventArgs e)
        {
            var ExportTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var dir = ConfigRepository.Instance()["dlcm_TempPath"] + "\\" + "0_temp" + "\\" + ExportTime;
            Directory.CreateDirectory(dir);

            try
            {
                Process process = Process.Start(@dir);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            var cmd = "SELECT ID, Original_FileName, Artist, Song_Title FROM Main WHERE ID=" + txt_ID.Text + "";
            DataSet dt; dt = SelectFromDB("Main", cmd, "", cnb);

            cmd = "SELECT XMLFilePath FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + " AND ArrangementType in (\"Guitar\",\"Bass\",\"Vocal\")";
            DataSet dz; dz = SelectFromDB("Arrangements", cmd, "", cnb);

            foreach (DataRow myRow in dt.Tables[0].Rows)
            {
                var idd = myRow["ID"].ToString();
                for (var j = 0; j < dz.Tables[0].Rows.Count; j++)
                {
                    //foreach (DataColumn myColumn in dt.Tables[0].Columns)
                    //{
                    var fileName = dz.Tables[0].Rows[j].ItemArray[0].ToString();
                    DLCManager.getGP5(dir, idd, myRow["Original_FileName"].ToString(), ConfigRepository.Instance()["dlcm_0_old"] + "\\0_old\\" + myRow["Original_FileName"].ToString(),
                        myRow["Song_Title"].ToString(), myRow["Artist"].ToString(), fileName, timestamp);
                }
            }
        }

        private void btn_OpenRepackedFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = Directory.Exists(c("dlcm_TempPath") + "\\0_repacked") ? c("dlcm_TempPath") + "\\0_repacked" : c("dlcm_0_repacked") + "\\" + chbx_Format.Text.ToUpper().Replace("_US", "").Replace("_EU", "").Replace("_JP", "");
            try
            {
                Process process = Process.Start(filePath);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Repacked Folder in Explorer ! ");
            }
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveRecord();
            savesettings();
        }

        private void txt_Artist_Sort_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Artist_Sort");
        }

        private void txt_Title_Sort_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Song_Title_Sort");
        }

        private void txt_AlbumSort_Leave(object sender, EventArgs e)
        {
            DetailsChanged("Album_Sort");
        }

        private void MainDB_FormClosed(object sender, CancelEventArgs e)
        {

        }

        private void MainDB_Leave_1(object sender, FormClosingEventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            savesettings();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var paath = c("dlcm_UltraStarCreator");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "UltraStar Creator\\usc.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install UltraStar Creator if you want to use it.", c("dlcm_UltraStarCreator_www"), "Missing UltraStar Creator", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx)) //&& File.Exists(replace(c("dlcm_DBFolder")))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
        }

        private void chbx_Setting_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Filtertxt = chbx_Setting.Text;
            if (Filtertxt.IndexOf("------") < 0)
                txt_ValueSetting.Text = Filtertxt.Substring(Filtertxt.IndexOf("---") + 3, Filtertxt.LastIndexOf("+++") - Filtertxt.IndexOf("---") - 3);
        }

        private void btn_SaveSetting_Click(object sender, EventArgs e)
        {
            var Value = txt_ValueSetting.Text;
            var Filtertxt = chbx_Setting.Text;
            if (Filtertxt == "") return;
            var Setting = Filtertxt.Substring(0, Filtertxt.LastIndexOf("--") - 1);
            var ID = Filtertxt.Substring(Filtertxt.IndexOf("+++") + 3, Filtertxt.Length - Filtertxt.IndexOf("+++") - 3);
            var cmd = "UPDATE Groups SET Groups=\"" + Value + "\" WHERE CDLC_ID=\"" + ID + "\" AND Type=\"Profile\" AND Comments=\"" + Setting + "\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\"";
            UpdateDB("Groups", cmd, cnb);
        }

        private void btn_GoTo_Click(object sender, EventArgs e)
        {
            var i = 0;
            var cmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_Title.Text != "" ? "Song_Title Like '%" + txt_Title.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_Album.Text != "" ? "Album Like '%" + txt_Album.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_Live_Details.Text != "" ? "Live_Details Like '%" + txt_Live_Details.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_Description.Text != "" ? "Description Like '%" + txt_Description.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_OldPath.Text != "" ? "Original_FileName Like '%" + txt_OldPath.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_OldPath.Text != "" ? "Author Like '%" + txt_Author.Text + "%'" : "");
            cmd += " AND ";
            cmd += (txt_ID.Text != "" ? "ID Like '%" + txt_ID.Text + "%'" : "");
            cmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";
            cmd = cmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            cmd = cmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
            cmd = cmd.Replace("WHERE AND", "WHERE ");
            cmd = cmd.Replace("AND ORDER BY ", "ORDER BY ");

            DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb); noOfRec = dhxs.Tables[0].Rows.Count;
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", SearchCmd, "", cnb); var noRec = dhs.Tables[0].Rows.Count;
            //var ID = 0;
            for (var j = GoTocounter; j <= noOfRec - 1; j++)
            {
                if (i != 0) break;
                for (var k = 0; k <= noRec - 1; k++)
                {
                    if (dhs.Tables[0].Rows[k].ItemArray[0].ToString() == dhxs.Tables[0].Rows[j].ItemArray[0].ToString())/*&& j == GoTocounter*/
                    {
                        i = k;
                        GoTocounter++;
                        break;
                    }
                }
            }
            if (i > 0 && databox.RowCount >= i)
            {
                databox.FirstDisplayedScrollingRowIndex = i;
                databox.Focus();
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Details not matching any Song in the DB", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void databox_CellMouseEnter(object sender, DataGridViewCellEventArgs location)
        {
            // Deal with hovering over a cell.
            mouseLocation = location;
        }

        private void btn_ReduceLenght_Click(object sender, EventArgs e)
        {
            chbx_ImprovedWithDM.Checked = true;
        }

        private void txt_NoOfSplits_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var sel = SearchCmd;
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb);
            var noOfRecs = SongRecord.Tables[0].Rows.Count;
            txt_NoOfSplits.Value = Math.Ceiling(noOfRecs / txt_No4Splitting.Value);
        }

        private void txt_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchClick(e);
        }
    }
}