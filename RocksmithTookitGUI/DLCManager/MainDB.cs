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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Web.UI;
using System.Windows.Forms;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using System.Data.SQLite;
using SQLite;
using System.Security.Cryptography;
using NLog.LayoutRenderers.Wrappers;
using X360.Other;
using RocksmithToolkitLib.DLCPackage.XBlock;
using Microsoft.VisualBasic;
using System.Windows.Documents;
using System.Data.Entity;
using Windows.Foundation.Metadata;
using SpotifyApi.NetCore;
using System.Linq.Expressions;
using Swan;
using RocksmithToolkitGUI.DDC;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Win32;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
//using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.Design;
using RocksmithToolkitLib.Ogg;
using Microsoft.Extensions.Logging;
using Windows.Devices.Geolocation;
using RocksmithToolkitLib.DLCPackage.AggregateGraph;
using X360.Profile;
using System.Windows.Controls;
using ToolTip = System.Windows.Forms.ToolTip;
using ProgressBar = System.Windows.Forms.ProgressBar;
using RichTextBox = System.Windows.Forms.RichTextBox;
using CheckBox = System.Windows.Forms.CheckBox;
using System.Windows.Interop;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Font = System.Drawing.Font;

namespace RocksmithToolkitGUI.DLCManager
{

    public partial class MainDB : Form
    {
        public bool AfterImport;
        //public MainDB(OleDbConnection cnnb, SQLiteConnection cnnz, bool AI)
        public MainDB(OleDbConnection cnnb, SQLite.SQLiteConnection cnnc, bool AI)/*, IContainer components*/
        {
            InitializeComponent();
            AfterImport = AI;
            cnb = cnnb;
            cnc = cnnc;
            //MainDB.ActiveForm.Size = c("dlcm_WindowSize").Replace("x",", ");
            //if (c("dlcm_WindowMax") == "Yes") MainDB.ActiveForm.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Maximized;
            //this.components = components;
        }

        private string Filename = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Text.txt");
        public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        public static BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private BackgroundWorker bwConvert;
        public string convdone;
        int GoTocounter = 0;
        string filezPath = "";
        bool filezPathDefault = false;
        private StringBuilder errorsFound;
        public RocksmithToolkitLib.Platform SourcePlatform { get; set; }

        // Create the ToolTip and associate with the Form container.
        //ToolTip toolTip2 = new ToolTip();
        //ToolTip toolTip3 = new ToolTip();
        //ToolTip toolTip4 = new ToolTip();
        //ToolTip toolTip5 = new ToolTip();
        //ToolTip toolTip6 = new ToolTip();
        //ToolTip toolTip7 = new ToolTip();

        public RocksmithToolkitLib.Platform TargetPlatform { get; set; }
        public string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        public static Process DDC = new Process();
        public static bool ProcessStarted = false;
        public bool SaveOK = false;
        public bool ChangeRows = false;
        public bool SearchON = false;
        public bool SearchExit = false;
        public bool AddPreview = false;
        ToolTip toolTip11 = new ToolTip();
        //ToolTip toolTip12 = new ToolTip();
        //ToolTip toolTip13 = new ToolTip();
        //ToolTip toolTip14 = new ToolTip();
        //ToolTip toolTip15 = new ToolTip();
        //ToolTip toolTip16 = new ToolTip();
        string Groupss = "";
        static string debug = "";
        public string netstatus = c("dlcm_netstatus");

        private static string _clientId;
        private static string _secretId;

        //public static SpotifyWebAPI _spotify;
        public string _trackno;
        public string _year;
        //public static PrivateProfile _profile;
        public List<FullTrack> _savedTracks;
        //public List<SimplePlaylist> _playlists;
        public string Archive_Path = c("dlcm_TempPath") + "\\0_archive";

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        public bool GroupChanged = false;
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        public string oldSearchCmd = "";
        public string SearchFields = "";
        //public OleDbConnection cnb;
        ////public SQLiteConnection cnz;
        //public SQLite.SQLiteConnection cnc;
        public string GetTrkTxt = "";
        string not_t = "0";
        float not_p = 0;

        DateTime timestamp;
        string logPath = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
        string tmpPath = c("dlcm_TempPath");
        public static BackgroundWorker bwAutoPlay;
        public bool arrangoff = false;
        private void MainDB_Load(object sender, EventArgs e)
        {
            this.Text = c("dlcm_DLCManager_ReleaseDetails") + "-" + c("dlcm_DLCManager_ReleaseNotes");
            prevWidth = Width;
            prevWindowState = WindowState;
            toolTip1.ShowAlways = true;
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipTitle = "Info bubble";

            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            var fnl = (logPath == null || !DirectoryExists(logPath) ? c("dlcm_TempPath") + "\\0_log" : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt";
            var starttmp = DateTime.Now;
            if (File.Exists((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt"))
            {
                File.Copy((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt"
                      , (logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp" + startT + ".txt", true);
                FileStream swt = File.Open((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt", FileMode.Create);
                swt.Dispose();
            }
            else
            {
                FileStream swt = File.Open((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt", FileMode.Create);
                swt.Dispose();
            }

            bwAutoPlay = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwAutoPlay.DoWork += new DoWorkEventHandler(PlayPreview);
            bwAutoPlay.WorkerReportsProgress = true;

            var tst = "Starting... " + startT; timestamp = UpdateLog(starttmp, tst, false, c("dlcm_TempPath"), c("dlcm_Split4Pack"), "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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

            //ConfigRepository.Instance()["dlcm_FilterGroup"] = chbx_Group.Text;
            //ConfigRepository.Instance()["dlcm_FilterNot"] = chbx_FilterNot.Checked ? "Yes" : "No";
            //ConfigRepository.Instance()["dlcm_FilterCompound"] = chbx_FilterCompound.Checked ? "Yes" : "No";
            //ConfigRepository.Instance()["dlcm_FilterOverlap"] = chbx_Overlap.Checked ? "Yes" : "No";
            //chbx_Group.Checked = false;
            if (c("dlcm_FilterGroup") == "Yes") chbx_InclGroups.Checked = true;
            else chbx_InclGroups.Checked = false;
            if (c("dlcm_FilterNot") == "Yes") chbx_FilterNot.Checked = true;
            else chbx_FilterNot.Checked = false;
            if (c("dlcm_FilterCompound") == "Yes") chbx_FilterCompound.Checked = true;
            else chbx_FilterCompound.Checked = false;
            if (c("dlcm_FilterOverlap") == "Yes") chbx_Overlap.Checked = true;
            else chbx_Overlap.Checked = false;

            if (c("dlcm_andCopy") == "Yes") chbx_Copy.Checked = true;
            else chbx_Copy.Checked = false;
            if (c("dlcm_AdditionalManipul54") == "Yes") chbx_PackBeta.Checked = true;
            else chbx_PackBeta.Checked = false;
            if (c("dlcm_AdditionalManipul7") == "Yes") chbx_ExcludeBroken.Checked = true;
            else chbx_ExcludeBroken.Checked = false;
            chbx_AutoSave.Checked = c("dlcm_Autosave") == "Yes" ? true : false;
            if (c("dlcm_AutoPlay").ToLower() == "yes") chbx_AutoPlay.Checked = true;
            else chbx_AutoPlay.Checked = false;
            chbx_Replace.Checked = c("dlcm_Replace") == "Yes" ? true : false;
            tst = "Stop reading config data... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            ChangeRows = false; Populate(ref databox, ref Main); ChangeRows = true;
            tst = "Stop populating... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


            //Create Groups
            CreateGroups();

            //Add fields for sorting
            CreateFieldsDropdown();

            //Create filters from Db or programatically (e.g. groups)
            cmb_Filter = GenerateFilterList(cmb_Filter, cnb, cnc);
            lbGroups = GenerateFilterListBox(lbGroups, cmb_Filter, cnb, cnc);
            //// Loads Groups in chbx_AllGroups Filter box cmb_Filter //Create Groups list Dropbox
            //var norec = 0;
            //DataSet dsn = new DataSet(); dsn = SelectFromDB("Groups", "SELECT DISTINCT Groupz FROM Groups WHERE Type =\"DLC\";", "", cnb, cnc);
            //norec = dsn.Tables.Count < 1 ? 0 : dsn.Tables[0].Rows.Count;
            //if (norec > 0)
            //    for (int j = 0; j < norec; j++)
            //        cmb_Filter.Items.Add("Group " + dsn.Tables[0].Rows[j][0].ToString());//add items

            //// Loads Tunnings in Filter box cmb_Filter
            //norec = 0;
            //DataSet dbn = new DataSet(); dbn = SelectFromDB("Arrangements", "SELECT DISTINCT Tunning FROM Arrangements WHERE 1=1" + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            //norec = dbn.Tables.Count < 1 ? 0 : dbn.Tables[0].Rows.Count;
            //if (norec > 0)
            //    for (int j = 0; j < norec; j++)
            //        cmb_Filter.Items.Add("Tuning " + dbn.Tables[0].Rows[j][0].ToString());//add items

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
                txt_Album_OrigArtPath.Visible = true;
                btn_AddSections.Enabled = true;
                //txt_Lyrics_Hash.Visible = true;
                txt_Art_Hash.Visible = true;
                txt_Preview_Hash.Visible = true;
                txt_Audio_Hash.Visible = true;
                txt_AudioPath.Visible = true;
                txt_AudioPreviewPath.Visible = true;
                txt_SongFolder.Visible = true;
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
                var OLD_Path = tmpp.Contains("\\0_data") ? tmpp.Substring(0, tmpp.IndexOf("\\0_data")) : "";
                var NEW_Path = c("dlcm_TempPath");
                if (!DirectoryExists(OLD_Path) || (tmpp.ToLower().IndexOf(NEW_Path.ToLower()) == -1) && DirectoryExists(NEW_Path))
                {
                    var cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "Album_ArtPathOrig = REPLACE(Album_ArtPathOrig, '" + OLD_Path + "', '" + NEW_Path + "'), " +
                            "AudioPath=REPLACE(AudioPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
                            "audioPreviewPath=REPLACE(audioPreviewPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
                            "Folder_Name=REPLACE(Folder_Name, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "OggPath = REPLACE(OggPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "oggPreviewPath = REPLACE(oggPreviewPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";";

                    DialogResult result1 = MessageBox.Show("DB Repository has been moved from " + OLD_Path + "\n\n to " + c("dlcm_TempPath") + tmpp + "\n\n-" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmd, cnb, cnc);
                        tst = "Main table has been updated ... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet ds = new DataSet();
                        cmd = "UPDATE Arrangements SET JSONFilePath=REPLACE(JSONFilePath, '" + OLD_Path + "','" + NEW_Path + "'), " +
                            "XMLFilePath=REPLACE(XMLFilePath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";";
                        ds = UpdateDB("Arrangements", cmd, cnb, cnc);
                        tst = "Arrangements table has been updated... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet dfs = new DataSet();
                        cmd = "UPDATE Pack_AuditTrail SET PackPath=REPLACE(PackPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";"; dfs = UpdateDB("Pack_AuditTrail", cmd, cnb, cnc);
                        tst = "Pack_AuditTrail table has been updated ... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        DataSet dhs = new DataSet();
                        cmd = "UPDATE Standardization SET SpotifyAlbumPath=REPLACE(SpotifyAlbumPath, '" + OLD_Path + "','" + NEW_Path + "')" +
                            ";"; dhs = UpdateDB("Standardization", cmd, cnb, cnc);
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
            StartProcesss(@c("dlcm_DBFolder"), null);
            //try
            //{
            //    Process process = Process.Start(@c("dlcm_DBFolder"));
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //MessageBox.Show("Can not open Main DB connection in MainDB ! " + c("dlcm_DBFolder"));
            //}
        }



        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                ArrangementsDB frm = new ArrangementsDB(c("dlcm_DBFolder"), txt_ID.Text, chbx_BassDD.Checked, cnb, cnc);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                TonesDB frm = new TonesDB(c("dlcm_DBFolder"), txt_ID.Text, cnb, cnc);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //cmb_Filter.Text = "";
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
                btn_GetTrackNo.Enabled = false;
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
                oldSearchCmd = SearchCmd;
            }
            else
               if (txt_Artist.Text != "" || txt_Title.Text != "" || txt_Album.Text != "" || txt_Description.Text != "" || txt_Live_Details.Text != "" || txt_OldPath.Text != "" || txt_Author.Text != "" || txt_ID.Text != "")
                try
                {
                    btn_GoTo.Enabled = false;
                    //DialogResult result1 = DialogResult.Yes;

                    //if (SearchCmd != oldSearchCmd) result1 = MessageBox.Show("Search already applied. Chose:\n  1.(Yes) Expand"
                    //    + SearchCmd.Substring(SearchCmd.IndexOf(" WHERE "), SearchCmd.Length - SearchCmd.IndexOf(" WHERE ") - 1) +
                    //    "\n\n  2.(No) Apply new filter only", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result1 == DialogResult.Yes)
                    SearchCmd = GenSearchGoTo(SearchCmd);
                    //else SearchCmd = oldSearchCmd;
                    //SearchCmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");

                    var t = Populate(ref databox, ref Main);
                    var n = c("dlcm_SearchFields").Replace("  ", " ");
                    var v = SearchCmd.Replace("  ", " ").Replace(n, " * ").Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ");
                    var cmd = v.Replace("Main", "Cache").Replace("Song_Title", "Title").Replace("Album_Year", "AlbumYear");
                    var no_R = GetNoRecords(cmd, cnb, cnc);
                    if (no_R != 0)
                        MessageBox.Show("Retail entry matches Search criteria " + no_R + "(times). Go and manually check there :)");

                    if (t == 0)
                    {
                        // t = Populate(ref databox, ref Main);
                        var condition = ""; var newcond = ""; var oldcond = "";

                        SearchCmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ").Replace("'", "\"");
                        oldSearchCmd = oldSearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ").Replace("'", "\"");

                        if (oldSearchCmd.Contains(" WHERE ")) condition = oldSearchCmd.Substring(oldSearchCmd.IndexOf(" WHERE ") + 7
                            , oldSearchCmd.Length - oldSearchCmd.IndexOf(" WHERE ") + 7 - (oldSearchCmd.IndexOf(" ORDER BY ") > 0 ? oldSearchCmd.Length - oldSearchCmd.IndexOf(" ORDER BY ") + 14 : 0));
                        else condition = "none set";

                        if (SearchCmd.Contains(" WHERE "))
                        {
                            oldcond = SearchCmd.Substring(SearchCmd.IndexOf(" WHERE ") + 7
                            , SearchCmd.Length - SearchCmd.IndexOf(" WHERE ") + 7 - (SearchCmd.IndexOf(" ORDER BY ") > 0 ? SearchCmd.Length - SearchCmd.IndexOf(" ORDER BY ") + 14 : 0));
                            newcond = oldcond.Replace(condition, "");
                            // newcond = newcond.Substring(4, newcond.Length - 4);
                            newcond = newcond.Trim();
                            //     .Replace("  ", " ").Replace("  ", " ").Replace("  ", " "));.Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ");
                        }
                        var g = "";
                        newcond = newcond.Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ");
                        var no_o = GetNoRecords(oldSearchCmd.Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE "), cnb, cnc);
                        var no_n = GetNoRecords("SELECT " + c("dlcm_SearchFields") + " FROM Main u " + ("WHERE " + newcond).Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ") + " ORDER BY " + c("dlcm_OrderOfFields") + ";", cnb, cnc);
                        var no_a = GetNoRecords("Select * from Main", cnb, cnc);
                        g = oldcond + "\n\nSearch retuned no result, so\n1. (Yes) (Rec: " + no_o + ") Resetting to old search criteria: " + condition + "\n" +
                            "2. (No) (Rec: " + no_n + ") Search newly (new criteria only): " + newcond
                             + "\n3. (Cancel)  (Rec: " + no_a + ")Reset to Filter 0ALL";
                        showoptionsbug(g, newcond);
                        //SearchCmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("WHERE AND", "WHERE ").Replace("WHERE  AND", "WHERE ").Replace("'", "\"");
                        //if (result3 == DialogResult.Cancel) SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE " + condition;
                        var tt = Populate(ref databox, ref Main);
                        if (tt > 0) { databox.Visible = false; databox.Refresh(); databox.Visible = true; }
                    }
                    else
                    { databox.Visible = false; databox.Refresh(); databox.Visible = true; }

                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message + "Can't run Search ! " + SearchCmd);
                }
            else MessageBox.Show("Add a search criteria");
            //Update_Selected();
        }
        private void showoptionsbug(string g, string newcond)
        {
            DialogResult result35 = DialogResult.No;
            result35 = MessageBox.Show(g, "Search retuned no result!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            //"\n3. (Cancel) Add to previous search criteria: " + oldcond

            if (result35 == DialogResult.Yes) SearchCmd = oldSearchCmd;
            if (result35 == DialogResult.No) SearchCmd = ("SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE " + newcond + " ORDER BY " + c("dlcm_OrderOfFields") + ";").Replace("WHERE AND", "WHERE ");
            if (result35 == DialogResult.Cancel) cmb_Filter.Text = "0ALL";
        }
        private void btn_SearchReset_Click(object sender, EventArgs e)
        {
            cmb_Filter.Text = "0ALL";
            GoTocounter = 0;
            btn_GoTo.Enabled = false;
            chbx_FilterNot.Checked = false;
            chbx_FilterCompound.Checked = false;
            chbx_Overlap.Checked = false;
            if (chbx_AutoSave.Checked) SaveRecord();
            //Populate(ref databox, ref Main);
            //databox.Refresh();
            //cmb_Filter.Text = "";
            SearchON = false;
            //Update_Selected();
        }

        public void getoldinfo(string File_Hash)
        {
            var cmd = "SELECT Artist,Artist_Sort, Song_Title, Song_Title_Sort, Album, Album_Sort, Album_Year, AlbumArtPath, DLC_Name, DLC_AppID, " +
                "PreviewLenght, AudioBitrate, AudioSampleRate FROM Import_AuditTrail WHERE FileHash=\"" + File_Hash + "\" ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
            DataSet dqs = new DataSet(); dqs = SelectFromDB("Import_AuditTrail", cmd, "", cnb, cnc);
            var norecs = GetNoRec(dqs, cnb, cnc);//dqs.Tables.Count == 0 ? 0 : dqs.Tables[0].Rows.Count;
            if (norecs > 0)
            //for (int j = 0; j < norecs; j++)
            //   if (dqs.Tables[0].Rows[1][3].ToString() != "" && dnss.Tables[0].Rows[j][3].ToString() != null)
            {
                toolTip1.SetToolTip(txt_Artist, "Original (at import): " + dqs.Tables[0].Rows[0][0].ToString());
                toolTip1.SetToolTip(txt_Artist_Sort, "Original (at import): " + dqs.Tables[0].Rows[0][1].ToString());
                toolTip1.SetToolTip(txt_Title, "Original (at import): " + dqs.Tables[0].Rows[0][2].ToString());
                toolTip1.SetToolTip(txt_Title_Sort, "Original (at import): " + dqs.Tables[0].Rows[0][3].ToString());
                toolTip1.SetToolTip(txt_Album, "Original (at import): " + dqs.Tables[0].Rows[0][4].ToString());
                toolTip1.SetToolTip(txt_AlbumSort, "Original (at import): " + dqs.Tables[0].Rows[0][5].ToString());
                toolTip1.SetToolTip(txt_Album_Year, "Original (at import): " + dqs.Tables[0].Rows[0][6].ToString());
                toolTip1.SetToolTip(txt_AlbumArtPath, "Original (at import): " + dqs.Tables[0].Rows[0][7].ToString());
                toolTip1.SetToolTip(txt_DLC_ID, "Original (at import): " + dqs.Tables[0].Rows[0][8].ToString());
                toolTip1.SetToolTip(txt_APP_ID, "Original (at import): " + dqs.Tables[0].Rows[0][9].ToString());
                toolTip1.SetToolTip(txt_PreviewEnd, "Original (at import): " + dqs.Tables[0].Rows[0][10].ToString());
                toolTip1.SetToolTip(txt_Volume, "Original (at import): " + dqs.Tables[0].Rows[0][11].ToString());
                toolTip1.SetToolTip(txt_PreviewStart, "Original (at import): " + dqs.Tables[0].Rows[0][12].ToString());
            }
        }

        public void ChangeRow()
        {
            var tst = "Start change row... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (chbx_AutoSave.Checked) SaveRecord();

            txt_OldPath.ReadOnly = true;
            txt_ID.ReadOnly = true;

            try
            {
                if (DDC.StartInfo.FileName != "")
                {
                    DDC.Kill();
                    DDC.Close();
                }
            }
            catch (Exception ex) { tst = "Erro at killing audio... " + ex; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }

            int i;
            if (databox.SelectedCells.Count > 0 && databox.Rows[databox.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
                if (SearchON) SearchON = false;
                Panel1.Visible = false;
                //Create List of Groups
                UpdateGroups();

                i = databox.SelectedCells[0].RowIndex;
                MainDBfields[] SongRecord = new MainDBfields[20000];
                btn_GoTo.Enabled = false;
                try
                {
                    SongRecord = GetRecord_s("SELECT * FROM Main WHERE ID=" + databox.Rows[i].Cells["ID"].Value.ToString(), cnb, cnc);
                    if (SongRecord[0].ID != null)
                    {
                        toolTip1.SetToolTip(chbx_Lead, "");
                        toolTip1.SetToolTip(chbx_Rhythm, "");
                        toolTip1.SetToolTip(chbx_Combo, "");
                        toolTip1.SetToolTip(chbx_Bass, "");
                        toolTip1.SetToolTip(chbx_Lyrics, "");
                        toolTip1.SetToolTip(btn_ADD2HOT, "");

                        //List Last 20 added in Monthly
                        toolTip1.SetToolTip(btn_ADD2HOT, toolTip1.GetToolTip(btn_ADD2HOT) + "\n" + ListHot20());
                        //Add Import original emtadata to ToolTip of each metadata field
                        getoldinfo(SongRecord[0].File_Hash);

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
                        var scmd = "SELECT Arrangement_Name, RouteMask, Bonus, PlaythroughYBLink FROM Arrangements WHERE CDLC_ID=" + SongRecord[0].ID + GetArrOfficSQLTxt(arrangoff);
                        DataSet dnss = new DataSet(); dnss = SelectFromDB("Arrangements", scmd, "", cnb, cnc);
                        var norecs = GetNoRec(dnss, cnb, cnc);//dnss.Tables.Count == 0 ? 0 : dnss.Tables[0].Rows.Count;
                        if (norecs > 0) for (int j = 0; j < norecs; j++)
                                if (dnss.Tables[0].Rows[j][3].ToString() != "" && dnss.Tables[0].Rows[j][3].ToString() != null)
                                {
                                    var b = "";
                                    var t = dnss.Tables[0].Rows[j][3].ToString();
                                    //if (t == "Lead") b=0;
                                    //else if (t == "Rhythm") b=1;
                                    //else if (t == "Combo") b=2;
                                    //else if (t == "Bass") b=3;
                                    //else if (t == "Vocal" || t == "JVocals") b=4;
                                    if (t == "0" || t == "Lead") b = "Lead";
                                    else if (t == "1" || t == "Rhythm") b = "Rhythm";
                                    else if (t == "2" || t == "Combo") b = "Combo";
                                    else if (t == "3" || t == "Bass") b = "Bass";
                                    else if (t == "4" || t == "Vocal" || t == "5" || t == "JVocals") b = "Vocal";

                                    var v = b + "_" + dnss.Tables[0].Rows[j][1].ToString() + "_"
                                    + (dnss.Tables[0].Rows[j][2].ToString().ToLower() == "Yes".ToLower() ? "_B_" : "")
                                    + "_" + dnss.Tables[0].Rows[j][3].ToString();
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

                        //check if any alternative AUDIO tracks are available
                        AddAlternativeAudioTracksAvailability(SongRecord[0].ID);

                        txt_DuplicateOf.Text = SongRecord[0].Duplicate_Of.ToString();
                        btn_Playthrough.Enabled = (txt_Playthrough.Text == "" || txt_Playthrough.Text is null) ? false : true;

                        txt_CustomsForge_Link.Text = SongRecord[0].CustomsForge_Link;
                        btn_CustomForge_Link.Enabled = SongRecord[0].CustomsForge_Link == "" ? false : true;
                        txt_CustomsForge_Like.Text = SongRecord[0].CustomsForge_Like;
                        txt_CustomsForge_ReleaseNotes.Text = SongRecord[0].CustomsForge_ReleaseNotes;
                        txt_PackingDate.Text = SongRecord[0].PackingDate;
                        txt_UpdateVersionDate.Text = SongRecord[0].UpdateVersionDate;
                        txt_BasedOn_GP.Text = SongRecord[0].BasedOn_GP;
                        txt_ToDo_s.Text = SongRecord[0].ToDos;
                        txt_ToneDetails.Text = SongRecord[0].ToneDetails;
                        txt_BasedOn_Tabs.Text = SongRecord[0].BasedOn_Tabs;
                        txt_BasedOn_Youtube.Text = SongRecord[0].BasedOn_Youtube;
                        txt_BasedOn_CF.Text = SongRecord[0].BasedOn_CF;

                        txt_Artist_ShortName.Text = SongRecord[0].Artist_ShortName;
                        txt_Album_ShortName.Text = SongRecord[0].Album_ShortName;
                        txt_RemotePath.Text = SongRecord[0].Remote_Path;
                        txt_FilesMissingIssues.Text = SongRecord[0].FilesMissingIssues;
                        tst = "Stop populating stnadard fields... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        txt_Album_OrigArtPath.Text = SongRecord[0].Album_ArtPathOrig;
                        if (SongRecord[0].AlbumArtPath == SongRecord[0].Album_ArtPathOrig) btn_RestoreAlbumArt.Enabled = false;
                        else btn_RestoreAlbumArt.Enabled = true;
                        if (SongRecord[0].UseInternalDDRemovalLogic == "Yes") chbx_UseInternalDDRemovalLogic.Checked = true;
                        else chbx_UseInternalDDRemovalLogic.Checked = false;
                        if (SongRecord[0].Is_Original == "Yes") { chbx_Original.Checked = true; chbx_Original.ForeColor = btn_Debug.ForeColor; }
                        else { chbx_Original.Checked = false; chbx_Original.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Is_Original == "Yes") chbx_A440.Checked = true;
                        else chbx_A440.Checked = false;
                        if (SongRecord[0].Is_Beta == "Yes") chbx_Beta.Checked = true;
                        else chbx_Beta.Checked = false;
                        /*{chbx_PackBeta.Enabled = true;; chbx_PackBeta.Enabled = false;  }}*/
                        if (SongRecord[0].Has_Capo == "Yes") { chbx_Capo.Checked = true; chbx_Capo.Visible = true; }
                        else { chbx_Capo.Checked = false; chbx_Capo.Visible = false; }
                        if (SongRecord[0].Has_ShowLights == "Yes") { chbx_HasShowLights.Checked = true; chbx_HasShowLights.Visible = true; }
                        else { chbx_HasShowLights.Checked = false; chbx_HasShowLights.Visible = false; }
                        //if (SongRecord[0].ImprovedWithDM == "Yes") chbx_A_IsImprovedWithDM.Checked = true;
                        //else chbx_A_IsImprovedWithDM.Checked = false;

                        //Attributes
                        //Location: 4, 54
                        var x = 1;
                        var y = 52;
                        if (SongRecord[0].Is_Instrumental == "Yes") { chbx_A_IsInstrumental.Location = new Point(x, y); x += chbx_A_IsInstrumental.Size.Width; chbx_A_IsInstrumental.Checked = true; chbx_A_IsInstrumental.Visible = true; }
                        else { chbx_A_IsInstrumental.Checked = false; chbx_A_IsInstrumental.Visible = false; }
                        if (SongRecord[0].Is_Single == "Yes") { chbx_A_IsSingle.Location = new Point(x, y); x += chbx_A_IsSingle.Size.Width; chbx_A_IsSingle.Checked = true; chbx_A_IsSingle.Visible = true; }
                        else { chbx_A_IsSingle.Checked = false; chbx_A_IsSingle.Visible = false; }
                        if (SongRecord[0].Is_Medley == "Yes") { chbx_A_IsMedley.Location = new Point(x, y); x += chbx_A_IsMedley.Size.Width; chbx_A_IsMedley.Checked = true; chbx_A_IsMedley.Visible = true; }
                        else { chbx_A_IsMedley.Checked = false; chbx_A_IsMedley.Visible = false; }
                        if (SongRecord[0].Is_MultiStrings == "Yes") { chbx_A_IsMultiStrings.Location = new Point(x, y); x += chbx_A_IsMultiStrings.Size.Width; chbx_A_IsMultiStrings.Checked = true; chbx_A_IsMultiStrings.Visible = true; }
                        else { chbx_A_IsMultiStrings.Checked = false; chbx_A_IsMultiStrings.Visible = false; }
                        if (SongRecord[0].Is_Soundtrack == "Yes") { chbx_A_IsSoundtrack.Location = new Point(x, y); x += chbx_A_IsSoundtrack.Size.Width; chbx_A_IsSoundtrack.Checked = true; chbx_A_IsSoundtrack.Visible = true; }
                        else { chbx_A_IsSoundtrack.Checked = false; chbx_A_IsSoundtrack.Visible = false; }
                        if (SongRecord[0].Is_EP == "Yes") { chbx_A_IsEP.Location = new Point(x, y); x += chbx_A_IsEP.Size.Width; chbx_A_IsEP.Checked = true; chbx_A_IsEP.Visible = true; }
                        else { chbx_A_IsEP.Checked = false; chbx_A_IsEP.Visible = false; }
                        if (SongRecord[0].Is_Uncensored == "Yes") { chbx_A_IsUncensored.Location = new Point(x, y); x += chbx_A_IsUncensored.Size.Width; chbx_A_IsUncensored.Checked = true; chbx_A_IsUncensored.Visible = true; }
                        else { chbx_A_IsUncensored.Checked = false; chbx_A_IsUncensored.Visible = false; }
                        if (SongRecord[0].Is_FullAlbum == "Yes") { chbx_A_IsFullAlbum.Location = new Point(x, y); x += chbx_A_IsFullAlbum.Size.Width; chbx_A_IsFullAlbum.Checked = true; chbx_A_IsFullAlbum.Visible = true; }
                        else { chbx_A_IsFullAlbum.Checked = false; chbx_A_IsFullAlbum.Visible = false; }
                        if (SongRecord[0].Has_Featuring == "Yes") { chbx_A_HasFeaturing.Location = new Point(x, y); x += chbx_A_HasFeaturing.Size.Width; chbx_A_HasFeaturing.Checked = true; chbx_A_HasFeaturing.Visible = true; }
                        else { chbx_A_HasFeaturing.Checked = false; chbx_A_HasFeaturing.Visible = false; }
                        if (SongRecord[0].Is_Cover == "Yes") { chbx_A_IsCover.Location = new Point(x, y); x += chbx_A_IsCover.Size.Width; chbx_A_IsCover.Checked = true; chbx_A_IsCover.Visible = true; }
                        else { chbx_A_IsCover.Checked = false; chbx_A_IsCover.Visible = false; }
                        if (SongRecord[0].Is_Deluxe == "Yes") { chbx_A_IsDeluxe.Location = new Point(x, y); x += chbx_A_IsDeluxe.Size.Width; chbx_A_IsDeluxe.Checked = true; chbx_A_IsDeluxe.Visible = true; }
                        else { chbx_A_IsDeluxe.Checked = false; chbx_A_IsDeluxe.Visible = false; }
                        if (SongRecord[0].Is_GreatestHits == "Yes") { chbx_A_IsGreatestHits.Location = new Point(x, y); x += chbx_A_IsGreatestHits.Size.Width; chbx_A_IsGreatestHits.Checked = true; chbx_A_IsGreatestHits.Visible = true; }
                        else { chbx_A_IsGreatestHits.Checked = false; chbx_A_IsGreatestHits.Visible = false; }
                        if (SongRecord[0].Is_Midi == "Yes") { chbx_A_IsMidi.Location = new Point(x, y); x += chbx_A_IsMidi.Size.Width; chbx_A_IsMidi.Checked = true; chbx_A_IsMidi.Visible = true; }
                        else { chbx_A_IsMidi.Checked = false; chbx_A_IsMidi.Visible = false; }
                        if (SongRecord[0].Is_MetalCover == "Yes") { chbx_A_IsMetalCover.Location = new Point(x, y); x += chbx_A_IsMetalCover.Size.Width; chbx_A_IsMetalCover.Checked = true; chbx_A_IsMetalCover.Visible = true; }
                        else { chbx_A_IsMetalCover.Checked = false; chbx_A_IsMetalCover.Visible = false; }
                        if (SongRecord[0].Is_GameSoundtrack == "Yes") { chbx_A_IsGameSoundtrack.Location = new Point(x, y); x += chbx_A_IsGameSoundtrack.Size.Width; chbx_A_IsGameSoundtrack.Checked = true; chbx_A_IsGameSoundtrack.Visible = true; }
                        else { chbx_A_IsGameSoundtrack.Checked = false; chbx_A_IsGameSoundtrack.Visible = false; }
                        if (SongRecord[0].Is_Ukulele == "Yes") { chbx_A_IsUkulele.Location = new Point(x, y); x += chbx_A_IsUkulele.Size.Width; chbx_A_IsUkulele.Checked = true; chbx_A_IsUkulele.Visible = true; }
                        else { chbx_A_IsUkulele.Checked = false; chbx_A_IsUkulele.Visible = false; }
                        if (SongRecord[0].Is_TVTheme == "Yes") { chbx_A_IsTVTheme.Location = new Point(x, y); x += chbx_A_IsTVTheme.Size.Width; chbx_A_IsTVTheme.Checked = true; chbx_A_IsTVTheme.Visible = true; }
                        else { chbx_A_IsTVTheme.Checked = false; chbx_A_IsTVTheme.Visible = false; }
                        if (SongRecord[0].Is_AmateurCover == "Yes") { chbx_A_IsAmateurCover.Location = new Point(x, y); x += chbx_A_IsAmateurCover.Size.Width; chbx_A_IsAmateurCover.Checked = true; chbx_A_IsAmateurCover.Visible = true; }
                        else { chbx_A_IsAmateurCover.Checked = false; chbx_A_IsAmateurCover.Visible = false; }
                        if (SongRecord[0].Is_Demo == "Yes") { chbx_A_IsDemo.Location = new Point(x, y); x += chbx_A_IsDemo.Size.Width; chbx_A_IsDemo.Checked = true; chbx_A_IsDemo.Visible = true; }
                        else { chbx_A_IsDemo.Checked = false; chbx_A_IsDemo.Visible = false; }
                        if (SongRecord[0].Is_Karaoke == "Yes") { chbx_A_IsKaraoke.Location = new Point(x, y); x += chbx_A_IsKaraoke.Size.Width; chbx_A_IsKaraoke.Checked = true; chbx_A_IsKaraoke.Visible = true; }
                        else { chbx_A_IsKaraoke.Checked = false; chbx_A_IsKaraoke.Visible = false; }
                        if (SongRecord[0].Is_Remix == "Yes") { chbx_A_IsRemix.Location = new Point(x, y); x += chbx_A_IsRemix.Size.Width; chbx_A_IsRemix.Checked = true; chbx_A_IsRemix.Visible = true; }
                        else { chbx_A_IsRemix.Checked = false; chbx_A_IsRemix.Visible = false; }
                        if (SongRecord[0].Is_Remastered == "Yes") { chbx_A_IsRemastered.Location = new Point(x, y); x += chbx_A_IsRemastered.Size.Width; chbx_A_IsRemastered.Checked = true; chbx_A_IsRemastered.Visible = true; }
                        else { chbx_A_IsRemastered.Checked = false; chbx_A_IsRemastered.Visible = false; }
                        if (SongRecord[0].Is_Live == "Yes") { chbx_A_IsLive.Location = new Point(x, y); x += chbx_A_IsLive.Size.Width; chbx_A_IsLive.Checked = true; chbx_A_IsLive.Visible = true; }
                        else { chbx_A_IsLive.Checked = false; chbx_A_IsLive.Visible = false; }
                        if (SongRecord[0].Is_Acoustic == "Yes") { chbx_A_IsAcoustic.Location = new Point(x, y); x += chbx_A_IsAcoustic.Size.Width; chbx_A_IsAcoustic.Checked = true; chbx_A_IsAcoustic.Visible = true; }
                        else { chbx_A_IsAcoustic.Checked = false; chbx_A_IsAcoustic.Visible = false; }
                        //if (SongRecord[0].Is_EP == "Yes") { chbx_A_IsEP.Location = new Point(x, y); x += chbx_A_IsEP.Size.Width; chbx_A_IsEP.Checked = true; chbx_A_IsEP.Visible = true; }
                        //else { chbx_A_IsEP.Checked = false; chbx_A_IsEP.Visible = false; }

                        if (SongRecord[0].ImprovedWithDM == "Yes") { chbx_IsImprovedWithDM.Checked = true; chbx_IsImprovedWithDM.Visible = true; }
                        else { chbx_IsImprovedWithDM.Checked = false; chbx_IsImprovedWithDM.Visible = false; }
                        if (SongRecord[0].PitchShiftableEsOrDd == "Yes") btn_PitchShift.Enabled = true;
                        else btn_PitchShift.Enabled = false;
                        if (SongRecord[0].IntheWorks == "Yes") { chbx_A_IsInTheWorks.Checked = true; chbx_A_IsInTheWorks.Visible = true; }
                        else { chbx_A_IsInTheWorks.Checked = false; chbx_A_IsInTheWorks.Visible = false; }
                        if (SongRecord[0].Is_Alternate == "Yes") { chbx_Alternate.Checked = true; txt_Alt_No.Enabled = true; }
                        else { chbx_Alternate.Checked = false; txt_Alt_No.Enabled = false; }
                        if (SongRecord[0].Is_Multitrack == "Yes") { chbx_MultiTrack.Checked = true; txt_MultiTrackType.Enabled = true; }
                        else { chbx_MultiTrack.Checked = false; txt_MultiTrackType.Enabled = false; }
                        if (SongRecord[0].Is_Broken == "Yes") chbx_Broken2.Checked = true;
                        else chbx_Broken2.Checked = false;
                        if (SongRecord[0].Has_Bass == "Yes") { chbx_Bass.Checked = true; chbx_Bass.Visible = true; chbx_Bass.ForeColor = System.Drawing.Color.Green; }/*chbx_Bass.Font = new Font(chbx_Bass.Font.Name, 6.5, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Bass.Checked = false; chbx_Bass.Visible = false; chbx_Bass.Font = new System.Drawing.Font(chbx_Bass.Font.Name, 7, FontStyle.Regular); chbx_Bass.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Lead == "Yes") { chbx_Lead.Checked = true; chbx_Lead.Visible = true; chbx_Lead.ForeColor = btn_Debug.ForeColor; }/*chbx_Lead.Font = new Font(chbx_Lead.Font.Name, 6.5, FontStyle.Bold);  | FontStyle.Underline*/
                        else { chbx_Lead.Checked = false; chbx_Lead.Visible = false; chbx_Lead.Font = new System.Drawing.Font(chbx_Lead.Font.Name, 7, FontStyle.Regular); chbx_Lead.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Combo == "Yes") { chbx_Combo.Checked = true; chbx_Combo.Visible = true; chbx_Combo.ForeColor = System.Drawing.Color.Green; }/*chbx_Combo.Font = new Font(chbx_Combo.Font.Name, 6.5, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Combo.Checked = false; chbx_Combo.Visible = false; chbx_Combo.Font = new System.Drawing.Font(chbx_Combo.Font.Name, 7, FontStyle.Regular); chbx_Combo.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Rhythm == "Yes") { chbx_Rhythm.Checked = true; chbx_Rhythm.Visible = true; chbx_Rhythm.ForeColor = System.Drawing.Color.Green; }/*chbx_Rhythm.Font = new Font(chbx_Rhythm.Font.Name, 6.5, FontStyle.Bold); | FontStyle.Underline*/
                        else { chbx_Rhythm.Checked = false; chbx_Rhythm.Visible = false; chbx_Rhythm.Font = new System.Drawing.Font(chbx_Rhythm.Font.Name, 7, FontStyle.Regular); chbx_Rhythm.ForeColor = btn_Duplicate.ForeColor; }
                        if (SongRecord[0].Has_Vocals == "Yes") { chbx_Lyrics.Checked = true; chbx_Lyrics.Visible = true; btn_ShowLyrics.Enabled = true; btn_AddInstrumental.Enabled = false; chbx_Lyrics.Font = new System.Drawing.Font(chbx_Lyrics.Font.Name, 7, FontStyle.Regular); chbx_Lyrics.ForeColor = System.Drawing.Color.Green; }/*bth_ShiftVocalNotes.Enabled = true; num_Lyrics.Enabled = true;btn_CreateLyrics.Enabled = false;*/
                        else { chbx_Lyrics.Checked = false; chbx_Lyrics.Visible = false; btn_CreateLyrics.Enabled = true; btn_ShowLyrics.Enabled = false; btn_AddInstrumental.Enabled = true; chbx_Lyrics.Font = new System.Drawing.Font(chbx_Lyrics.Font.Name, 7, FontStyle.Regular); chbx_Lyrics.ForeColor = btn_Duplicate.ForeColor; }/*bth_ShiftVocalNotes.Enabled = false; num_Lyrics.Enabled = false;*/
                        if (SongRecord[0].Has_Sections != null)
                        {
                            if (SongRecord[0].Has_Sections.Length > 2) chbx_HasSections.Checked = true;
                            else chbx_HasSections.Checked = false;
                        }
                        else chbx_HasSections.Checked = false;
                        if (SongRecord[0].Has_Cover == "Yes") { chbx_HasCover.Checked = true; chbx_HasCover.Visible = true; }
                        else { chbx_HasCover.Checked = false; chbx_HasCover.Visible = false; }
                        if (SongRecord[0].Has_Preview == "Yes") { chbx_HasPreview.Checked = true; btn_PlayPreview.Enabled = true; }
                        else { chbx_HasPreview.Checked = false; btn_PlayPreview.Enabled = false; }
                        if (SongRecord[0].Has_DD == "Yes") { txt_AddDD.Enabled = false; chbx_DD.Checked = true; btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; }
                        else { chbx_DD.Checked = false; btn_AddDD.Enabled = true; btn_RemoveDD.Enabled = false; }
                        if (SongRecord[0].Keep_BassDD == "Yes") { chbx_KeepBassDD.Checked = true; }
                        else { chbx_KeepBassDD.Checked = false; }
                        if (SongRecord[0].Keep_DD == "Yes") { chbx_KeepDD.Checked = true; }
                        else { chbx_KeepDD.Checked = false; }
                        if (SongRecord[0].Selected == "Yes") chbx_Selected.Checked = true;
                        else chbx_Selected.Checked = false;
                        if (SongRecord[0].Has_Author == "Yes") { chbx_HasAuthor.Checked = true; chbx_HasAuthor.Visible = true; }
                        else { chbx_HasAuthor.Checked = false; chbx_HasAuthor.Visible = false; }
                        if (SongRecord[0].Bass_Has_DD == "Yes") { chbx_BassDD.Checked = true; btn_RemoveBassDD.Enabled = true; chbx_KeepBassDD.Enabled = true; chbx_RemoveBassDD.Enabled = true; }
                        else { chbx_BassDD.Checked = false; btn_RemoveBassDD.Enabled = false; chbx_RemoveBassDD.Enabled = false; }
                        if (SongRecord[0].Has_Bonus_Arrangement == "Yes") chbx_Bonus.Checked = true;
                        else chbx_Bonus.Checked = false;
                        //chbx_Avail_Old.Checked = false;
                        if (float.Parse(SongRecord[0].audioBitrate, NumberStyles.Float, CultureInfo.CurrentCulture) > c("dlcm_MaxBitRate").ToInt32()
                            || float.Parse(SongRecord[0].audioSampleRate, NumberStyles.Float, CultureInfo.CurrentCulture) > c("dlcm_MaxSampleRate").ToInt32()
                            || SongRecord[0].Has_Preview != "Yes" || SongRecord[0].Has_Track_No != "Yes"
                            || SongRecord[0].Track_No == "-1" || SongRecord[0].Track_No == "0" || txt_Playthrough.Items.Count == 0 || txt_YouTube_Link.Text == "")
                        { btn_Fix_AudioIssues.Enabled = true; }
                        else { btn_Fix_AudioIssues.Enabled = false; }

                        if (SongRecord[0].Available_Old == "Yes") { chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true; }
                        else { chbx_Avail_Old.Checked = false; btn_OldFolder.Enabled = false; ; btn_CopyOld.Enabled = false; chbx_CopyOld.Enabled = false; }
                        //chbx_Avail_Duplicate.Checked = false;
                        if (SongRecord[0].Available_Duplicate == "Yes") { chbx_Avail_Duplicate.Checked = true; btn_DuplicateFolder.Enabled = true; }
                        else { chbx_Avail_Duplicate.Checked = false; btn_DuplicateFolder.Enabled = false; }
                        if (SongRecord[0].Has_Been_Corrected == "Yes") chbx_Has_Been_Corrected.Checked = true;
                        else chbx_Has_Been_Corrected.Checked = false;

                        if (SongRecord[0].Has_Had_Audio_Changed == "Yes") chbx_AudioChanged.Checked = true;
                        else chbx_AudioChanged.Checked = false;
                        if (SongRecord[0].Has_Had_Lyrics_Changed == "Yes") chbx_LyricsChanged.Checked = true;
                        else chbx_LyricsChanged.Checked = false;

                        if (SongRecord[0].Has_Alternate_Audio == "Yes") chbx_AlternateAudioAvail.Checked = true;
                        else chbx_AlternateAudioAvail.Checked = false;
                        if (SongRecord[0].Has_Alternate_Lyrics == "Yes") chbx_AlternateLyricsAvail.Checked = true;
                        else chbx_AlternateLyricsAvail.Checked = false;

                        chbx_Originals_Available.Checked = false;
                        if (chbx_Format_Originals.Items.Count > 0) for (int k = chbx_Format_Originals.Items.Count - 1; k >= 0; --k) chbx_Format_Originals.Items.RemoveAt(k);//remove items
                        if (SongRecord[0].Has_Other_Officials == "Yes")
                        {
                            chbx_Originals_Available.Checked = true;
                            var cmd = "SELECT DISTINCT Platform FROM Pack_AuditTrail WHERE Official=\"Yes\" AND DLC_ID=" + SongRecord[0].ID + " AND PackPath not like \"%0_old%\"; ";
                            DataSet dns = new DataSet(); dns = SelectFromDB("Main", cmd, "", cnb, cnc);
                            var norec = GetNoRec(dns, cnb, cnc);//dns.Tables[0].Rows.Count;
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
                        toolTip1.SetToolTip(btn_PlayAudio, SongRecord[0].Audio_OrigHash);
                        toolTip1.SetToolTip(btn_PlayPreview, SongRecord[0].Audio_OrigPreviewHash);
                        toolTip1.SetToolTip(gbox_Cover, SongRecord[0].audioBitrate + " - " + SongRecord[0].audioSampleRate);
                        toolTip1.SetToolTip(txt_PreviewStart, SongRecord[0].Song_Lenght);

                        txt_Preview_Hash.Text = SongRecord[0].AudioPreview_Hash;
                        txt_Audio_Hash.Text = SongRecord[0].Audio_Hash;
                        //txt_Lyrics_Hash.Text = SongRecord[0].has
                        txt_Art_Hash.Text = SongRecord[0].AlbumArt_Hash;
                        txt_Remote_Path.Text = SongRecord[0].Remote_Path;
                        txt_AudioPath.Text = File.Exists(SongRecord[0].AudioPath) ? SongRecord[0].AudioPath : "wem audio missing";
                        txt_AudioPreviewPath.Text = File.Exists(SongRecord[0].audioPreviewPath) ? SongRecord[0].audioPreviewPath : "wem preview missing";
                        txt_SongFolder.Text = Directory.Exists(SongRecord[0].Folder_Name) ? SongRecord[0].Folder_Name : "song folder missing";
                        txt_AlbumArtPath.Text = File.Exists(SongRecord[0].AlbumArtPath) ? SongRecord[0].AlbumArtPath : "albumart missing";

                        txt_OggPath.Text = File.Exists(SongRecord[0].OggPath) ? SongRecord[0].OggPath : "audio missing";
                        txt_OggPreviewPath.Text = File.Exists(SongRecord[0].oggPreviewPath) ? SongRecord[0].oggPreviewPath : "preview missing";
                        txt_OldPath.Text = File.Exists(c("dlcm_TempPath") + "\\0_old\\" + SongRecord[0].Original_FileName) ? SongRecord[0].Original_FileName : "orignal imported file missing";
                        tst = "Stop populating multivalue fields... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        //Lyrics
                        DataSet dsr = new DataSet(); dsr = SelectFromDB("Arrangements", "SELECT XMLFilePath FROM Arrangements WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                        var rec = GetNoRec(dsr, cnb, cnc);//dsr.Tables.Count == 0 ? 0 : dsr.Tables[0].Rows.Count;
                        if (rec > 0) { txt_Lyrics.Text = dsr.Tables[0].Rows[0].ItemArray[0].ToString(); btn_ChangeLyrics.Text = "Change Lyrics"; }
                        else { txt_Lyrics.Text = ""; btn_ChangeLyrics.Text = "Add Lyrics"; }

                        //Audio Autoplay
                        if (chbx_AutoPlay.Checked && chbx_HasPreview.Checked)
                        {
                            if (btn_PlayPreview.Text != "Play Preview") btn_PlayPreview.Text = "Play Preview";
                            else btn_PlayPreview.Text = "STOP Preview";
                            AudioBackgroundPlay(txt_OggPreviewPath.Text, true, AppWD);
                            AudioBackgroundPlay(txt_OggPreviewPath.Text, false, AppWD);
                        }
                        if (SongRecord[0].Has_Rhythm != "Yes" && SongRecord[0].Has_Lead == "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "L->R"; }
                        else if (SongRecord[0].Has_Rhythm == "Yes" && SongRecord[0].Has_Lead != "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "R->L"; }
                        else chbx_DupliGTrack.Enabled = false;
                        gbox_Cover.ImageLocation = GetAlbumArtPath(txt_AlbumArtPath.Text, txt_Album_OrigArtPath.Text, ".png");// txt_AlbumArtPath.Text.Replace(".dds", ".png");
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
                        btn_GetTrackNo.Enabled = true;
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
                            " Rating, ScrollSpeed, Comments, XMLFilePath FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + GetArrOfficSQLTxt(arrangoff) + " ORDER BY ID DESC;";
                        DataSet ddr = new DataSet(); ddr = SelectFromDB("Arrangements", sel, "", cnb, cnc);
                        rec = GetNoRec(ddr, cnb, cnc);//ddr.Tables.Count > 0 ? ddr.Tables[0].Rows.Count : 0;

                        //toolTip11.RemoveAll(); toolTip12.RemoveAll(); toolTip13.RemoveAll(); toolTip14.RemoveAll(); toolTip15.RemoveAll();
                        //toolTip2.RemoveAll(); toolTip3.RemoveAll(); toolTip4.RemoveAll(); toolTip5.RemoveAll();
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
                                if (Arrangement_Name == "0" || Arrangement_Name == "Lead") toolTip1.SetToolTip(chbx_Lead, toolTip1.GetToolTip(chbx_Lead) + txtt);
                                else if (Arrangement_Name == "1" || Arrangement_Name == "Rhythm") toolTip1.SetToolTip(chbx_Rhythm, toolTip1.GetToolTip(chbx_Rhythm) + txtt);
                                else if (Arrangement_Name == "2" || Arrangement_Name == "Combo") toolTip1.SetToolTip(chbx_Combo, toolTip1.GetToolTip(chbx_Combo) + txtt);
                                else if (Arrangement_Name == "3" || Arrangement_Name == "Bass") toolTip1.SetToolTip(chbx_Bass, toolTip1.GetToolTip(chbx_Bass) + txtt);
                                else if (Arrangement_Name == "4" || Arrangement_Name == "Vocal" || Arrangement_Name == "5" || Arrangement_Name == "JVocals") toolTip1.SetToolTip(chbx_Lyrics, toolTip1.GetToolTip(chbx_Lyrics) + txtt);
                                //else if (Arrangement_Name == "5" || Arrangement_Name == "JVocals") toolTip1.SetToolTip(chbx_Combo, toolTip1.GetToolTip(chbx_Combo) + txtt);
                                var instr = Arrangement_Name;
                                var bon = ddr.Tables[0].Rows[j].ItemArray[2].ToString().ToLower() == "true" ? "b" : "";
                                toolTip1.SetToolTip(txt_Tuning, toolTip1.GetToolTip(txt_Tuning) + ", " + instr + ": " + (bon == "" ? "" : bon + "-") + Tunings);
                                //tzt += GetDropTunningInstr(ddr.Tables[0].Rows[j].ItemArray[11].ToString());
                                var tt = GetDropTunningInstr(ddr.Tables[0].Rows[j].ItemArray[11].ToString());
                                if (tt != "") tzt += (tzt.IndexOf(tt) < 0) ? (instr + ": " + tt + "\n") : instr + ": Ditto\n";
                            }
                            if (tzt != "") toolTip1.SetToolTip(btn_PitchShift, tzt);
                            else toolTip1.SetToolTip(btn_PitchShift, "Not in any E standard or Drop D, drop(s) tunings, from which you can Drop down using EletroHarmonix Drop pedal");
                        }
                        //Populate all Packed versions of the song
                        var tht = "SELECT (PackPath+'\\'+FileName) as PackPath FROM Pack_AuditTrail WHERE CDLC_ID=" + txt_ID.Text + " ORDER BY ID DESC;";
                        DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tht, "", cnb, cnc);
                        rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables[0].Rows.Count;
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
                Panel1.Visible = true;
            }
            //else MessageBox.Show("No Records/Songs (Imported/Found)");
            Update_Selected();
            tst = "Stop Change Row... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        public string ListHot20()
        {
            var cmd = "SELECT ( M.Artist  &' - '& M.Song_Title  &' - '& G.Date_Added &' - '& M.ID) AS Groupz FROM Groups G" +
               " LEFT JOIN Main M ON VAL(G.CDLC_ID)=M.ID WHERE Groupz=\"" + c("dlcm_HotGrp") + "\" ORDER BY G.Date_Added DESC";
            var songs = "";
            DataSet dz; dz = SelectFromDB("Groups", cmd, "", cnb, cnc);
            var rec = GetNoRec(dz, cnb, cnc);//dz.Tables.Count > 0 ? dz.Tables[0].Rows.Count : 0;/*+'-'+*/
            if (rec > 0)
            {
                for (var j = 0; j <= rec - 1; j++)
                {
                    if (j == c("dlcm_maxsongsinweekly").ToInt32() * 3) return songs;
                    if (j == c("dlcm_maxsongsinweekly").ToInt32()) songs += "-------------" + "\n";
                    songs += dz.Tables[0].Rows[j][0].ToString() + "\n";
                    //songs += " - "+dz.Tables[0].Rows[j][1].ToString();
                    //songs += " - "+dz.Tables[0].Rows[j][2].ToString();
                    //songs += " - "+dz.Tables[0].Rows[j][3].ToString();
                    //songs += " - "+dz.Tables[0].Rows[j][4].ToString() ;
                }
            }
            return songs;
        }
        public void ListTracks(string Duplicate_Of, string ID, string Song_Lenght)
        {
            if (cmb_Tracks.Items.Count > 0) for (int k = cmb_Tracks.Items.Count - 1; k >= 0; --k) cmb_Tracks.Items.RemoveAt(k);//remove items
            var scmd = "SELECT ID FROM Main WHERE ID=" + (Duplicate_Of == "" ? "0" : Duplicate_Of) + " OR ID=" + (ID == "" ? "0" : ID) + " OR Duplicate_Of=\"" + (Duplicate_Of == "0" ? "999999" : Duplicate_Of)
                + "\" OR Duplicate_Of=\"" + (ID == "" ? "0" : ID) + "\";";
            DataSet dzs = new DataSet(); dzs = SelectFromDB("Main", scmd, "", cnb, cnc);
            var norecs = GetNoRec(dzs, cnb, cnc);//dzs.Tables.Count == 0 ? 0 : dzs.Tables[0].Rows.Count;
            var IDs = "0";
            for (var j = 0; j <= norecs - 1; j++) IDs += "," + dzs.Tables[0].Rows[j].ItemArray[0].ToString();

            scmd = "SELECT ID, XMLFileName, Start_Time, RouteMask, Bonus, ArrangementType, CDLC_ID FROM Arrangements WHERE CDLC_ID IN (" + IDs + ")" + GetArrOfficSQLTxt(arrangoff);
            DataSet dnzs = new DataSet(); dnzs = SelectFromDB("Arrangements", scmd, "", cnb, cnc);
            norecs = GetNoRec(dnzs, cnb, cnc);//dnzs.Tables.Count == 0 ? 0 : dnzs.Tables[0].Rows.Count;
            if (norecs > 0)
                for (int j = 0; j < norecs; j++)
                    if (dnzs.Tables[0].Rows[j][0].ToString() != "" && dnzs.Tables[0].Rows[j][0].ToString() != null)
                    {
                        if (dnzs.Tables[0].Rows[j][5].ToString().IndexOf("ShowLight") >= 0) continue;
                        scmd = "SELECT Song_Lenght,Duplicate_Of FROM Main WHERE ID=" + dnzs.Tables[0].Rows[j][6].ToString();
                        DataSet djs = new DataSet(); djs = SelectFromDB("Main", scmd, "", cnb, cnc);
                        double B = 0;
                        try
                        {
                            B = Math.Round(double.Parse(dnzs.Tables[0].Rows[j][2].ToString()), 3);
                        }
                        catch (Exception ex) { B = -1; }
                        var v = "DLC ID: " + dnzs.Tables[0].Rows[j][6].ToString() + ", ";
                        v += (dnzs.Tables[0].Rows[j][5].ToString() == "Vocal" ? dnzs.Tables[0].Rows[j][5].ToString() : dnzs.Tables[0].Rows[j][3].ToString());
                        v += "_" + "Start Time: " + B + "s_";
                        v += "_" + (dnzs.Tables[0].Rows[j][4].ToString() == "True" ? "Bonus" : "NoBonus");
                        v += (djs.Tables[0].Rows[0].ItemArray[1].ToString().ToInt32() > 0 ? "_Duplicate Of: " + djs.Tables[0].Rows[0].ItemArray[1].ToString() : "");
                        v += "_Dupli(if)/Curent Lenght: " + djs.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + Song_Lenght + "s_ArrangementID=" + dnzs.Tables[0].Rows[j][0].ToString();
                        cmb_Tracks.Items.Add(v);//add items
                    }

            cmb_Tracks.Items.Add(""); cmb_Tracks.Text = "";
        }

        //DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"AudioAlternative\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Comments =\"" + wav + "\"", cnb, cnc
        public void AddAlternativeAudioTracksAvailability(string ID)
        {
            if (txt_MultiTrackType.Items.Count > 0) for (int k = txt_MultiTrackType.Items.Count - 1; k >= 0; --k)
                    txt_MultiTrackType.Items[k] = txt_MultiTrackType.Items[k].ToString().Replace(" (Audio Available)", "");//remove items
            //var scmd = "SELECT ID FROM Main WHERE ID=" + (Duplicate_Of == "" ? "0" : Duplicate_Of) + " OR ID=" + (ID == "" ? "0" : ID) + " OR Duplicate_Of=\"" + (Duplicate_Of == "0" ? "999999" : Duplicate_Of)
            //    + "\" OR Duplicate_Of=\"" + (ID == "" ? "0" : ID) + "\";";
            //DataSet dzs = new DataSet(); dzs = SelectFromDB("Main", scmd, "", cnb, cnc);
            //var norecs = GetNoRec(dzs, cnb, cnc);//dzs.Tables.Count == 0 ? 0 : dzs.Tables[0].Rows.Count;
            //var IDs = "0";
            //for (var j = 0; j <= norecs - 1; j++) IDs += "," + dzs.Tables[0].Rows[j].ItemArray[0].ToString();

            var scmd = "SELECT ID, Type, Comments, Groupz FROM Groups WHERE CDLC_ID=\"" + txt_ID.Text + "\" AND Type=\"AudioAlternative\"";
            DataSet dnzs = new DataSet(); dnzs = SelectFromDB("Groups", scmd, "", cnb, cnc);
            var norecs = GetNoRec(dnzs, cnb, cnc);//dnzs.Tables.Count == 0 ? 0 : dnzs.Tables[0].Rows.Count;
            if (norecs > 0)
                for (int j = 0; j < norecs; j++)
                    for (int k = txt_MultiTrackType.Items.Count - 1; k >= 0; --k)
                        if (dnzs.Tables[0].Rows[j][3].ToString() == txt_MultiTrackType.Items[k].ToString().Replace(" (Audio Available)", ""))
                        {
                            txt_MultiTrackType.Items[k] = txt_MultiTrackType.Items[k].ToString().Replace(" (Audio Available)", "") + " (Audio Available)";
                            //if (dnzs.Tables[0].Rows[j][5].ToString().IndexOf("ShowLight") >= 0) continue;
                            //scmd = "SELECT Song_Lenght,Duplicate_Of FROM Main WHERE ID=" + dnzs.Tables[0].Rows[j][6].ToString();
                            //DataSet djs = new DataSet(); djs = SelectFromDB("Main", scmd, "", cnb, cnc);
                            //double B = 0;
                            //try
                            //{
                            //    B = Math.Round(double.Parse(dnzs.Tables[0].Rows[j][2].ToString()), 3);
                            //}
                            //catch (Exception ex) { B = -1; }
                            //var v = "DLC ID: " + dnzs.Tables[0].Rows[j][6].ToString() + ", ";
                            //v += (dnzs.Tables[0].Rows[j][5].ToString() == "Vocal" ? dnzs.Tables[0].Rows[j][5].ToString() : dnzs.Tables[0].Rows[j][3].ToString());
                            //v += "_" + "Start Time: " + B + "s_";
                            //v += "_" + (dnzs.Tables[0].Rows[j][4].ToString() == "True" ? "Bonus" : "NoBonus");
                            //v += (djs.Tables[0].Rows[0].ItemArray[1].ToString().ToInt32() > 0 ? "_Duplicate Of: " + djs.Tables[0].Rows[0].ItemArray[1].ToString() : "");
                            //v += "_Dupli(if)/Curent Lenght: " + djs.Tables[0].Rows[0].ItemArray[0].ToString() + "/" + Song_Lenght + "s_ArrangementID=" + dnzs.Tables[0].Rows[j][0].ToString();
                            //cmb_Tracks.Items.Add(v);//add items
                        }

            //cmb_Tracks.Items.Add(""); cmb_Tracks.Text = "";
        }
        //command.Parameters[35].Value
        //command.Parameters[34].Value
        //command.Parameters[33].Value
        //command.Parameters[32].Value
        //command.Parameters[31].Value
        //command.Parameters[30].Value
        //command.Parameters[29].Value
        //command.Parameters[28].Value
        //command.Parameters[27].Value
        //command.Parameters[26].Value
        //command.Parameters[25].Value
        //command.Parameters[24].Value
        //command.Parameters[23].Value
        //command.Parameters[22].Value
        //command.Parameters[21].Value
        //command.Parameters[20].Value
        //command.Parameters[19].Value
        //command.Parameters[18].Value
        //command.Parameters[17].Value
        //command.Parameters[16].Value
        //command.Parameters[15].Value
        //command.Parameters[14].Value
        //command.Parameters[13].Value
        //command.Parameters[12].Value
        //command.Parameters[11].Value
        //command.Parameters[10].Value
        //command.Parameters[9].Value
        //command.Parameters[8].Value
        //command.Parameters[7].Value
        //command.Parameters[6].Value
        //command.Parameters[5].Value
        //command.Parameters[4].Value
        //command.Parameters[3].Value
        //command.Parameters[2].Value
        //command.Parameters[1].Value
        //command.Parameters[0].Value
        public void savegroups()
        {
            if (GroupChanged)
            {
                //identify all already selected Groups as to not to overwritte their
                var sel = "SELECT CDLC_ID, Groupz, ID, Comments FROM Groups WHERE Type =\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                DataSet grp = new DataSet(); grp = SelectFromDB("Groups", sel, "", cnb, cnc);
                var noOfRecs = GetNoRec(grp, cnb, cnc);//grp.Tables.Count == 0 ? 0 : grp.Tables[0].Rows.Count;
                                                       //var grpsel = "(";
                var grpdel = "("; var found = false;
                for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                {
                    found = false;
                    for (var k = 0; k < noOfRecs; k++)
                    {
                        //if (chbx_AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == chbx_AllGroups.Items[j].ToString())
                        //
                        //    grpsel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                        //    found = true;
                        //    break;
                        //}
                        if (!chbx_AllGroups.GetItemChecked(j) &&
                            grp.Tables[0].Rows[k].ItemArray[1].ToString() + "{" + grp.Tables[0].Rows[k].ItemArray[3].ToString() + "}" == chbx_AllGroups.Items[j].ToString().Substring(0, chbx_AllGroups.Items[j].ToString().IndexOf("}") + 1))
                            grpdel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                        if (chbx_AllGroups.GetItemChecked(j) &&
                        grp.Tables[0].Rows[k].ItemArray[1].ToString() + "{" + grp.Tables[0].Rows[k].ItemArray[3].ToString() + "}"
                        == chbx_AllGroups.Items[j].ToString().Substring(0, chbx_AllGroups.Items[j].ToString().IndexOf("}") + 1))
                            found = true;
                    }

                    if (!found && chbx_AllGroups.GetItemChecked(j))
                    {
                        var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";/*, Comments*/
                        string[] goup = chbx_AllGroups.Items[j].ToString().Split('{');
                        var gpp = goup[0];//chbx_AllGroups.Items[j].ToString() 
                        string[] gp = goup[1].Split('}');
                        if (gp.Contains("(weekly)"))
                            continue;
                        var grpord = gp[0];

                        //check grp id
                        DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + gpp + "\"", "", cnb, cnc);//chbx_AllGroups.Items[chbx_AllGroups.SelectedIndex]
                        var noOfRec = GetNoRec(dgs, cnb, cnc);
                        if (grpord != dgs.Tables[0].Rows[0].ItemArray[0].ToString() || noOfRec > 1)
                            MessageBox.Show("Grp id diff than the DB(obtained from grp name:" + grpord + "; obtained from select:" + dgs.Tables[0].Rows[0].ItemArray[0].ToString() + "; no of distinct no found:" + noOfRec + ")");

                        var insertvalues = "\"" + txt_ID.Text + "\", \"" + gpp + "\", \"DLC\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grpord + "\"";
                        InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                    }
                }
                //grpsel = grpsel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace("(,", "(") + ")";
                grpdel = grpdel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace("(,", "(") + ")";

                if (grpdel != "()")
                    DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND ID IN " + grpdel.Replace(",)", ")"), cnb, cnc);

                var tst = "Stop saving groups... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                GroupChanged = false;
            }
        }
        public void SaveRecord()
        {
            //if (txt_Artist.Text == "") MessageBox.Show("Empty artist. somethign weird is happening");
            if (!SaveOK || txt_Artist.Text == "") return;
            var tst = "Start Savin... " + txt_Artist.Text + " - " + txt_Title.Text; ; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            DataSet dis = new DataSet();

            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                int i = databox.SelectedCells[0].RowIndex;

                //if (txt_Artist.Text != databox.Rows[i].Cells["Artist"].Value.ToString()) MessageBox.Show("name of artist has change.maybe somethign weird is happening?");
                tst = "Start saving Groups... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //Save Groups
                savegroups();

                var YBPlaythrough = "";
                if (txt_Playthrough.Items.Count > 0)
                    for (int j = 0; j < txt_Playthrough.Items.Count; j++)
                    {
                        var bonus = false;
                        var str = txt_Playthrough.Items[j].ToString();
                        if (str.Length <= 0 || str.IndexOf("_") <= 0) continue;
                        var no = str.Substring(0, str.IndexOf("_"));

                        if (str.IndexOf("_B_") > 0)
                        {
                            bonus = true; YBPlaythrough = str.Substring(str.IndexOf("_B_") + 3, str.Length - str.IndexOf("__") + 2);/* continue;*/
                        }
                        else if (char.IsNumber(no, 0)) { YBPlaythrough = str.Substring(str.IndexOf("__") + 2, str.Length - str.IndexOf("__") - 2 - 1); /*continue; */}
                        else
                        {
                            var updateMcmd = "Update Main Set Youtube_Playthrough = \"" + str + "\"" +
                        " WHERE ID=" + txt_ID.Text + ";";
                            DataSet dcr = new DataSet(); dcr = UpdateDB("Main", updateMcmd, cnb, cnc);
                        }

                        if (no == "Lead") no = "Arrangement_Name == '0' OR Arrangement_Name = 'Lead'";
                        else if (no == "Rhythm") no = "Arrangement_Name = '1' OR Arrangement_Name = 'Rhythm'";
                        else if (no == "Combo") no = "Arrangement_Name = '2' OR Arrangement_Name = 'Combo'";
                        else if (no == "Bass") no = "Arrangement_Name = '3' OR Arrangement_Name = 'Bass'";
                        else if (no == "Vocal") no = "Arrangement_Name = '4' OR Arrangement_Name = 'Vocal'";
                        else if (no == "JVocals") no = "Arrangement_Name = '5' OR Arrangement_Name = 'JVocal'";

                        var updatecmd = "Update Arrangements Set PlayThroughYBLink = \"" + YBPlaythrough + "\"" +
                        " WHERE CDLC_ID=" + txt_ID.Text + " AND Arrangement_Name=\"" + no + "\"" + (bonus ? " Bonus=\"" + bonus + "\"" : "") + ";";
                        DataSet dxr = new DataSet(); dxr = UpdateDB("Arrangements", updatecmd, cnb, cnc);
                    }

                //Update Song / Main DB
                var connection = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
                var command = connection.CreateCommand();
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
                command.CommandText += txt_AlbumArtPath.Text.Contains(" missing") ? "" : "AlbumArtPath = @param10, ";
                command.CommandText += txt_AudioPreviewPath.Text.Contains(" missing") ? "" : "AudioPreviewPath = @param12, ";
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
                command.CommandText += txt_OggPath.Text.Contains(" missing") ? "" : "oggPath = @param77, ";
                command.CommandText += txt_OggPreviewPath.Text.Contains(" missing") ? "" : "oggPreviewPath = @param78, ";
                //command.CommandText += "AlbumArt_Hash = @param80, ";
                command.CommandText += txt_Art_Hash.Text.Contains(" missing") ? "" : "AlbumArt_Hash = @param81, ";
                command.CommandText += txt_Preview_Hash.Text.Contains(" missing") ? "" : "AudioPreview_Hash = @param82, ";
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
                command.CommandText += "Is_Instrumental = @param94, ";
                command.CommandText += "Is_Soundtrack = @param95, ";
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
                command.CommandText += "Is_FullAlbum = @param109, ";
                command.CommandText += "Is_Remastered = @param110, ";
                command.CommandText += "Is_Demo = @param111, ";
                command.CommandText += "Is_Cover = @param112, ";
                command.CommandText += "Is_Remix = @param113, ";
                command.CommandText += "Is_Karaoke = @param114, ";
                command.CommandText += "Has_Featuring = @param115, ";
                command.CommandText += "BasedOn_Youtube = @param116, ";
                command.CommandText += "BasedOn_CF = @param117, ";
                command.CommandText += "BasedOn_Tabs = @param118, ";
                command.CommandText += "ToDos = @param119, ";
                command.CommandText += "ToneDetails = @param120, ";
                command.CommandText += "PackageDetails = @param121, ";
                command.CommandText += "Has_Capo = @param122, ";
                command.CommandText += "Is_Medley = @param123, ";
                command.CommandText += "Is_MultiStrings = @param124, ";
                command.CommandText += "Is_Deluxe = @param125, ";
                command.CommandText += "Is_GreatestHits = @param126, ";
                command.CommandText += "Is_Midi = @param127, ";
                command.CommandText += "Is_GameSoundtrack = @param128, ";
                command.CommandText += "Is_TVTheme = @param129, ";
                command.CommandText += "Is_AmateurCover = @param130, ";
                command.CommandText += "BasedOn_GP = @param131, ";
                command.CommandText += "PackingDate = @param132, ";
                command.CommandText += "UpdateVersionDate = @param133, ";
                command.CommandText += "FilesMissingIssues = @param134, ";
                command.CommandText += "Is_MetalCover = @param135, ";
                command.CommandText += "Is_Ukulele = @param136, ";
                command.CommandText += "Has_Alternate_Audio = @param137, ";
                command.CommandText += "Has_Alternate_Lyrics = @param138, ";
                command.CommandText += "A440TunningFrecv = @param139 ";
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
                if (!txt_AlbumArtPath.Text.Contains(" missing")) command.Parameters.AddWithValue("@param10", txt_AlbumArtPath.Text);// databox.Rows[i].Cells["AlbumArtPath"].Value.ToString());
                if (!txt_AudioPreviewPath.Text.Contains(" missing")) command.Parameters.AddWithValue("@param12", txt_AudioPreviewPath.Text);// databox.Rows[i].Cells["AudioPreviewPath"].Value.ToString());
                command.Parameters.AddWithValue("@param13", txt_Track_No.Text.ToInt32().ToString("D2"));// databox.Rows[i].Cells["Track_No"].Value.ToString());
                command.Parameters.AddWithValue("@param14", txt_Author.Text);// databox.Rows[i].Cells["Author"].Value.ToString());
                command.Parameters.AddWithValue("@param15", txt_Version.Text);// databox.Rows[i].Cells["Version"].Value.ToString());
                command.Parameters.AddWithValue("@param16", txt_DLC_ID.Text);// databox.Rows[i].Cells["DLC_Name"].Value.ToString());
                command.Parameters.AddWithValue("@param17", txt_APP_ID.Text);// databox.Rows[i].Cells["DLC_AppID"].Value.ToString());
                command.Parameters.AddWithValue("@param26", chbx_Original.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Original"].Value.ToString());
                command.Parameters.AddWithValue("@param28", chbx_Beta.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Beta"].Value.ToString());
                command.Parameters.AddWithValue("@param29", chbx_Alternate.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Alternate"].Value.ToString());
                command.Parameters.AddWithValue("@param30", chbx_MultiTrack.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Multitrack"].Value.ToString());
                command.Parameters.AddWithValue("@param31", chbx_Broken2.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["Is_Broken"].Value.ToString());
                command.Parameters.AddWithValue("@param32", txt_MultiTrackType.Text);// databox.Rows[i].Cells["MultiTrack_Version"].Value.ToString());
                command.Parameters.AddWithValue("@param33", txt_Alt_No.Text);/// databox.Rows[i].Cells["Alternate_Version_No"].Value.ToString());
                command.Parameters.AddWithValue("@param40", chbx_Lyrics.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Vocals"].Value.ToString());
                command.Parameters.AddWithValue("@param41", chbx_HasSections.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Sections"].Value.ToString());
                command.Parameters.AddWithValue("@param42", chbx_HasCover.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Cover"].Value.ToString());
                command.Parameters.AddWithValue("@param43", chbx_HasPreview.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Preview"].Value.ToString());
                command.Parameters.AddWithValue("@param45", chbx_DD.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_DD"].Value.ToString());
                command.Parameters.AddWithValue("@param47", txt_Tuning.Text);/// databox.Rows[i].Cells["Tunning"].Value.ToString());
                command.Parameters.AddWithValue("@param48", txt_BassPicking.Text);// databox.Rows[i].Cells["Bass_Picking"].Value.ToString());
                                                                                  //command.Parameters.AddWithValue("@param49", "");// databox.Rows[i].Cells["Tones"].Value.ToString());
                                                                                  //command.Parameters.AddWithValue("@param50", "");//databox.Rows[i].Cells["Groups"].Value.ToString());
                command.Parameters.AddWithValue("@param51", txt_Rating.Text);/// databox.Rows[i].Cells["Rating"].Value.ToString());
                command.Parameters.AddWithValue("@param52", txt_Description.Text);// databox.Rows[i].Cells["Description"].Value.ToString());
                command.Parameters.AddWithValue("@param54", chbx_A_HasTrackNo.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Track_No"].Value.ToString());
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
                command.Parameters.AddWithValue("@param76", chbx_HasAuthor.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["Has_Author"].Value.ToString());
                if (!txt_OggPath.Text.Contains(" missing")) command.Parameters.AddWithValue("@param77", txt_OggPath.Text);//databox.Rows[i].Cells["oggPath"].Value.ToString());
                if (!txt_OggPreviewPath.Text.Contains(" missing")) command.Parameters.AddWithValue("@param78", txt_OggPreviewPath.Text);//databox.Rows[i].Cells["oggPreviewPath"].Value.ToString());
                                                                                                                                        //command.Parameters.AddWithValue("@param80", txt_AlbumArtPath);// databox.Rows[i].Cells["AlbumArt_Hash"].Value.ToString());
                if (!txt_Art_Hash.Text.Contains(" missing")) command.Parameters.AddWithValue("@param81", txt_Art_Hash.Text);//databox.Rows[i].Cells["Audio_Hash"].Value.ToString());
                if (!txt_Preview_Hash.Text.Contains(" missing")) command.Parameters.AddWithValue("@param82", txt_Preview_Hash.Text);//databox.Rows[i].Cells["AudioPreview_Hash"].Value.ToString());
                command.Parameters.AddWithValue("@param83", chbx_BassDD.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Bass_Has_DD"].Value.ToString());
                command.Parameters.AddWithValue("@param84", chbx_Bonus.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString());
                command.Parameters.AddWithValue("@param85", txt_Artist_ShortName.Text);// databox.Rows[i].Cells["Artist_ShortName"].Value.ToString());
                command.Parameters.AddWithValue("@param86", txt_Album_ShortName.Text);// databox.Rows[i].Cells["Album_ShortName"].Value.ToString());
                command.Parameters.AddWithValue("@param87", chbx_A_IsLive.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Live"].Value.ToString());
                command.Parameters.AddWithValue("@param88", txt_Live_Details.Text);// databox.Rows[i].Cells["Live_Details"].Value.ToString());
                command.Parameters.AddWithValue("@param89", txt_RemotePath.Text);// databox.Rows[i].Cells["Remote_Path"].Value.ToString());
                command.Parameters.AddWithValue("@param90", chbx_A_IsAcoustic.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Acoustic"].Value.ToString());
                command.Parameters.AddWithValue("@param91", txt_Rating.Text);// databox.Rows[i].Cells["Top10"].Value.ToString());
                command.Parameters.AddWithValue("@param92", chbx_UseInternalDDRemovalLogic.Checked ? "Yes" : "No");//  databox.Rows[i].Cells["UseInternalDDRemovalLogic"].Value.ToString());
                command.Parameters.AddWithValue("@param93", chbx_A_IsSingle.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Single"].Value.ToString());
                command.Parameters.AddWithValue("@param94", chbx_A_IsInstrumental.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_Soundtrack"].Value.ToString());
                command.Parameters.AddWithValue("@param95", chbx_A_IsSoundtrack.Checked ? "Yes" : "No");// ? "Yes" : "No");// databox.Rows[i].Cells["Is_Instrumental"].Value.ToString());
                command.Parameters.AddWithValue("@param96", chbx_A_IsEP.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Is_EP"].Value.ToString());
                command.Parameters.AddWithValue("@param97", chbx_AudioChanged.Checked ? "Yes" : "No");// databox.Rows[i].Cells["Has_Had_Audio_Changed"].Value.ToString());
                command.Parameters.AddWithValue("@param98", chbx_LyricsChanged.Checked ? "Yes" : "No");//databox.Rows[i].Cells["Has_Had_Lyrics_Changed"].Value.ToString());
                command.Parameters.AddWithValue("@param99", txt_AlbumSort.Text);
                command.Parameters.AddWithValue("@param100", chbx_Has_Been_Corrected.Checked ? "Yes" : "No"); //var old = txt_Title.Text; chbx_Has_Been_Corrected
                command.Parameters.AddWithValue("@param101", txt_DuplicateOf.Text);
                command.Parameters.AddWithValue("@param102", chbx_Lead.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param103", chbx_Rhythm.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param104", chbx_Bass.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param105", chbx_A_IsUncensored.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param106", chbx_A_IsInTheWorks.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param107", chbx_LyricsLanguage.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param108", chbx_IsImprovedWithDM.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param109", chbx_A_IsFullAlbum.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param110", chbx_A_IsRemastered.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param111", chbx_A_IsDemo.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param112", chbx_A_IsCover.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param113", chbx_A_IsRemix.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param114", chbx_A_IsKaraoke.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param115", chbx_A_HasFeaturing.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param116", txt_BasedOn_Youtube.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param117", txt_BasedOn_CF.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param118", txt_BasedOn_Tabs.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param119", txt_ToDo_s.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param120", txt_ToneDetails.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param121", txt_PackageDetails.Text ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param122", chbx_Capo.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param123", chbx_A_IsMedley.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param124", chbx_A_IsMultiStrings.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param125", chbx_A_IsDeluxe.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param126", chbx_A_IsGreatestHits.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param127", chbx_A_IsMidi.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param128", chbx_A_IsGameSoundtrack.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param129", chbx_A_IsTVTheme.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param130", chbx_A_IsAmateurCover.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param131", txt_BasedOn_GP.Text);
                command.Parameters.AddWithValue("@param132", txt_PackingDate.Text);
                command.Parameters.AddWithValue("@param133", txt_UpdateVersionDate.Text);
                command.Parameters.AddWithValue("@param134", txt_FilesMissingIssues.Text);
                command.Parameters.AddWithValue("@param135", chbx_A_IsUkulele.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param136", chbx_A_IsMetalCover.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param137", chbx_AlternateAudioAvail.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param138", chbx_AlternateLyricsAvail.Checked ? "Yes" : "No");
                command.Parameters.AddWithValue("@param139", chbx_A440.Checked ? "Yes" : "No");
                //command.Parameters.AddWithValue("@param121", chbx_ShowL.Checked ? "Yes" : "No");

                UpdateDBbyExecuteNonQuery(command, connection, cnc);

            }
            GroupChanged = false;
            tst = "Stop savin... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //Update_Selected();
            tst = "Stop updatin Selected stat... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return;
        }

        public int Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            noOfRec = 0;
            lbl_NoRec.Text = " songs.";
            dssx.Dispose();

            var tst = "Selecting... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            dssx = SelectFromDB("Main", SearchCmd, "", cnb, cnc);
            tst = "Stop Selecting... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (dssx.Tables.Count > 0)
            {
                try
                {
                    dssx.Tables["Main"].AcceptChanges(); tst = "dssx.Tables[Main].AcceptChanges()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception exf)
                {
                    tst = "Selecting... erro" + exf.Message; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); ;
                }

                if (dssx.Tables[0].Rows.Count > 0)
                {
                    //creating columns
                    //DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", DisplayIndex = 0, HeaderText = "ID " };
                    //DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", DisplayIndex = 1, HeaderText = "Artist " };
                    //DataGridViewTextBoxColumn Song_Title = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title", DisplayIndex = 2, HeaderText = "Song_Title " };
                    //DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", DisplayIndex = 3, HeaderText = "Album " };
                    //DataGridViewTextBoxColumn Album_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Year", DisplayIndex = 4, HeaderText = "Album_Year " };
                    //DataGridViewTextBoxColumn Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Track_No", DisplayIndex = 5, HeaderText = "Track_No " };
                    //DataGridViewTextBoxColumn Author = new DataGridViewTextBoxColumn { DataPropertyName = "Author", DisplayIndex = 6, HeaderText = "Author " };
                    //DataGridViewTextBoxColumn Version = new DataGridViewTextBoxColumn { DataPropertyName = "Version", DisplayIndex = 7, HeaderText = "Version " };
                    //DataGridViewTextBoxColumn Import_Date = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Date", DisplayIndex = 8, HeaderText = "Import_Date " };
                    //DataGridViewTextBoxColumn Is_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Original", DisplayIndex = 9, HeaderText = "Is_Original " };
                    //DataGridViewTextBoxColumn Song_Title_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title_Sort", HeaderText = "Song_Title_Sort " };
                    //DataGridViewTextBoxColumn Artist_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Sort", HeaderText = "Artist_Sort " };
                    //DataGridViewTextBoxColumn AverageTempo = new DataGridViewTextBoxColumn { DataPropertyName = "AverageTempo", HeaderText = "AverageTempo " };
                    //DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
                    //DataGridViewTextBoxColumn Preview_Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Preview_Volume", HeaderText = "Preview_Volume " };
                    //DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath " };
                    //DataGridViewTextBoxColumn Album_ArtPathOrig = new DataGridViewTextBoxColumn { DataPropertyName = "Album_ArtPathOrig", HeaderText = "Album_ArtPathOrig " };
                    //DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath " };
                    //DataGridViewTextBoxColumn audioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreviewPath", HeaderText = "audioPreviewPath " };
                    //DataGridViewTextBoxColumn DLC_Name = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_Name", HeaderText = "DLC_Name " };
                    //DataGridViewTextBoxColumn DLC_AppID = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_AppID", HeaderText = "DLC_AppID " };
                    //DataGridViewTextBoxColumn Current_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Current_FileName", HeaderText = "Current_FileName " };
                    //DataGridViewTextBoxColumn Original_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Original_FileName", HeaderText = "Original_FileName " };
                    //DataGridViewTextBoxColumn Import_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Path", HeaderText = "Import_Path " };
                    //DataGridViewTextBoxColumn Folder_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Folder_Name", HeaderText = "Folder_Name " };
                    //DataGridViewTextBoxColumn File_Size = new DataGridViewTextBoxColumn { DataPropertyName = "File_Size", HeaderText = "File_Size " };
                    //DataGridViewTextBoxColumn File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "File_Hash", HeaderText = "File_Hash " };
                    //DataGridViewTextBoxColumn Original_File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Original_File_Hash", HeaderText = "Original_File_Hash " };
                    //DataGridViewTextBoxColumn Is_OLD = new DataGridViewTextBoxColumn { DataPropertyName = "Is_OLD", HeaderText = "Is_OLD " };
                    //DataGridViewTextBoxColumn Is_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Beta", HeaderText = "Is_Beta " };
                    //DataGridViewTextBoxColumn Is_Alternate = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Alternate", HeaderText = "Is_Alternate " };
                    //DataGridViewTextBoxColumn Is_Multitrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Multitrack", HeaderText = "Is_Multitrack " };
                    //DataGridViewTextBoxColumn Is_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Broken", HeaderText = "Is_Broken " };
                    //DataGridViewTextBoxColumn MultiTrack_Version = new DataGridViewTextBoxColumn { DataPropertyName = "MultiTrack_Version", HeaderText = "MultiTrack_Version " };
                    //DataGridViewTextBoxColumn Alternate_Version_No = new DataGridViewTextBoxColumn { DataPropertyName = "Alternate_Version_No", HeaderText = "Alternate_Version_No " };
                    //DataGridViewTextBoxColumn DLC = new DataGridViewTextBoxColumn { DataPropertyName = "DLC", HeaderText = "DLC " };
                    //DataGridViewTextBoxColumn Has_Bass = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bass", HeaderText = "Has_Bass " };
                    //DataGridViewTextBoxColumn Has_Guitar = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Guitar", HeaderText = "Has_Guitar " };
                    //DataGridViewTextBoxColumn Has_Lead = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Lead", HeaderText = "Has_Lead " };
                    //DataGridViewTextBoxColumn Has_Rhythm = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Rhythm", HeaderText = "Has_Rhythm " };
                    //DataGridViewTextBoxColumn Has_Combo = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Combo", HeaderText = "Has_Combo " };
                    //DataGridViewTextBoxColumn Has_Vocals = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Vocals", HeaderText = "Has_Vocals " };
                    //DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections " };
                    //DataGridViewTextBoxColumn Has_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Cover", HeaderText = "Has_Cover " };
                    //DataGridViewTextBoxColumn Has_Preview = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Preview", HeaderText = "Has_Preview " };
                    //DataGridViewTextBoxColumn Has_Custom_Tone = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Custom_Tone", HeaderText = "Has_Custom_Tone " };
                    //DataGridViewTextBoxColumn Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Has_DD", HeaderText = "Has_DD " };
                    //DataGridViewTextBoxColumn Has_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Version", HeaderText = "Has_Version " };
                    //DataGridViewTextBoxColumn Has_ShowLights = new DataGridViewTextBoxColumn { DataPropertyName = "Has_ShowLights", HeaderText = "Has_ShowLights " };
                    //DataGridViewTextBoxColumn Has_JVocals = new DataGridViewTextBoxColumn { DataPropertyName = "Has_JVocals", HeaderText = "Has_JVocals " };
                    //DataGridViewTextBoxColumn Tunning = new DataGridViewTextBoxColumn { DataPropertyName = "Tunning", HeaderText = "Tunning " };
                    //DataGridViewTextBoxColumn Bass_Picking = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Picking", HeaderText = "Bass_Picking " };
                    //DataGridViewTextBoxColumn Tones = new DataGridViewTextBoxColumn { DataPropertyName = "Tones", HeaderText = "Tones " };
                    //DataGridViewTextBoxColumn Groups = new DataGridViewTextBoxColumn { DataPropertyName = "Groups", HeaderText = "Groups " };
                    //DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating " };
                    //DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
                    //DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
                    //DataGridViewTextBoxColumn Has_Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Track_No", HeaderText = "Has_Track_No " };
                    //DataGridViewTextBoxColumn Platform = new DataGridViewTextBoxColumn { DataPropertyName = "Platform", HeaderText = "Platform " };
                    //DataGridViewTextBoxColumn PreviewTime = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewTime", HeaderText = "PreviewTime " };
                    //DataGridViewTextBoxColumn PreviewLenght = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewLenght", HeaderText = "PreviewLenght " };
                    //DataGridViewTextBoxColumn Temp = new DataGridViewTextBoxColumn { DataPropertyName = "Temp", HeaderText = "Temp " };
                    //DataGridViewTextBoxColumn CustomForge_Followers = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Followers", HeaderText = "CustomForge_Followers " };
                    //DataGridViewTextBoxColumn CustomForge_Version = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Version", HeaderText = "CustomForge_Version " };
                    //DataGridViewTextBoxColumn FilesMissingIssues = new DataGridViewTextBoxColumn { DataPropertyName = "FilesMissingIssues", HeaderText = "FilesMissingIssues " };
                    //DataGridViewTextBoxColumn Duplicates = new DataGridViewTextBoxColumn { DataPropertyName = "Duplicates", HeaderText = "Duplicates " };
                    //DataGridViewTextBoxColumn Pack = new DataGridViewTextBoxColumn { DataPropertyName = "Pack", HeaderText = "Pack " };
                    //DataGridViewTextBoxColumn Keep_BassDD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_BassDD", HeaderText = "Keep_BassDD " };
                    //DataGridViewTextBoxColumn Keep_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_DD", HeaderText = "Keep_DD " };
                    //DataGridViewTextBoxColumn Keep_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_Original", HeaderText = "Keep_Original " };
                    //DataGridViewTextBoxColumn Song_Lenght = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Lenght", HeaderText = "Song_Lenght " };
                    //DataGridViewTextBoxColumn Original = new DataGridViewTextBoxColumn { DataPropertyName = "Original", HeaderText = "Original " };
                    //DataGridViewTextBoxColumn Selected = new DataGridViewTextBoxColumn { DataPropertyName = "Selected", HeaderText = "Selected " };
                    //DataGridViewTextBoxColumn YouTube_Link = new DataGridViewTextBoxColumn { DataPropertyName = "YouTube_Link", HeaderText = "YouTube_Link " };
                    //DataGridViewTextBoxColumn CustomsForge_Link = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Link", HeaderText = "CustomsForge_Link " };
                    //DataGridViewTextBoxColumn CustomsForge_Like = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Like", HeaderText = "CustomsForge_Like " };
                    //DataGridViewTextBoxColumn CustomsForge_ReleaseNotes = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_ReleaseNotes", HeaderText = "CustomsForge_ReleaseNotes " };
                    //DataGridViewTextBoxColumn SignatureType = new DataGridViewTextBoxColumn { DataPropertyName = "SignatureType", HeaderText = "SignatureType " };
                    //DataGridViewTextBoxColumn ToolkitVersion = new DataGridViewTextBoxColumn { DataPropertyName = "ToolkitVersion", HeaderText = "ToolkitVersion " };
                    //DataGridViewTextBoxColumn Has_Author = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Author", HeaderText = "Has_Author " };
                    //DataGridViewTextBoxColumn OggPath = new DataGridViewTextBoxColumn { DataPropertyName = "OggPath", HeaderText = "OggPath " };
                    //DataGridViewTextBoxColumn oggPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "oggPreviewPath", HeaderText = "oggPreviewPath " };
                    //DataGridViewTextBoxColumn UniqueDLCName = new DataGridViewTextBoxColumn { DataPropertyName = "UniqueDLCName", HeaderText = "UniqueDLCName " };
                    //DataGridViewTextBoxColumn AlbumArt_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Hash", HeaderText = "AlbumArt_Hash " };
                    //DataGridViewTextBoxColumn Audio_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_Hash", HeaderText = "Audio_Hash " };
                    //DataGridViewTextBoxColumn audioPreview_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreview_Hash", HeaderText = "audioPreview_Hash " };
                    //DataGridViewTextBoxColumn Bass_Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Has_DD", HeaderText = "Bass_Has_DD " };
                    //DataGridViewTextBoxColumn Has_Bonus_Arrangement = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bonus_Arrangement", HeaderText = "Has_Bonus_Arrangement " };
                    //DataGridViewTextBoxColumn Artist_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_ShortName", HeaderText = "Artist_ShortName " };
                    //DataGridViewTextBoxColumn Album_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Album_ShortName", HeaderText = "Album_ShortName " };
                    //DataGridViewTextBoxColumn Available_Old = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Old", HeaderText = "Available_Old " };
                    //DataGridViewTextBoxColumn Available_Duplicate = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Duplicate", HeaderText = "Available_Duplicate " };
                    //DataGridViewTextBoxColumn Has_Been_Corrected = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Been_Corrected", HeaderText = "Has_Been_Corrected " };
                    //DataGridViewTextBoxColumn File_Creation_Date = new DataGridViewTextBoxColumn { DataPropertyName = "File_Creation_Date", HeaderText = "File_Creation_Date " };
                    //DataGridViewTextBoxColumn Is_Live = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Live", HeaderText = "Is_Live " };
                    //DataGridViewTextBoxColumn Live_Details = new DataGridViewTextBoxColumn { DataPropertyName = "Live_Details", HeaderText = "Live_Details " };
                    //DataGridViewTextBoxColumn Remote_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Remote_Path", HeaderText = "Remote_Path " };
                    //DataGridViewTextBoxColumn audioBitrate = new DataGridViewTextBoxColumn { DataPropertyName = "audioBitrate", HeaderText = "audioBitrate " };
                    //DataGridViewTextBoxColumn audioSampleRate = new DataGridViewTextBoxColumn { DataPropertyName = "audioSampleRate", HeaderText = "audioSampleRate " };
                    //DataGridViewTextBoxColumn Is_Acoustic = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Acoustic", HeaderText = "Is_Acoustic " };
                    //DataGridViewTextBoxColumn Top10 = new DataGridViewTextBoxColumn { DataPropertyName = "Top10", HeaderText = "Top10 " };
                    //DataGridViewTextBoxColumn Has_Other_Officials = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Other_Officials", HeaderText = "Has_Other_Officials " };
                    //DataGridViewTextBoxColumn Spotify_Song_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Song_ID", HeaderText = "Spotify_Song_ID" };
                    //DataGridViewTextBoxColumn Spotify_Artist_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Artist_ID", HeaderText = "Spotify_Artist_ID " };
                    //DataGridViewTextBoxColumn Spotify_Album_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Album_ID", HeaderText = "Spotify_Album_ID " };
                    //DataGridViewTextBoxColumn Spotify_Album_URL = new DataGridViewTextBoxColumn { DataPropertyName = "Spotify_Album_URL", HeaderText = "Spotify_Album_URL " };
                    //DataGridViewTextBoxColumn Audio_OrigHash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_OrigHash", HeaderText = "Audio_OrigHash " };
                    //DataGridViewTextBoxColumn Audio_OrigPreviewHash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_OrigPreviewHash", HeaderText = "Audio_OrigPreviewHash " };
                    //DataGridViewTextBoxColumn AlbumArt_OrigHash = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_OrigHash", HeaderText = "AlbumArt_OrigHash " };
                    //DataGridViewTextBoxColumn Duplicate_Of = new DataGridViewTextBoxColumn { DataPropertyName = "Duplicate_Of", HeaderText = "Duplicate_Of " };
                    //DataGridViewTextBoxColumn Split4Pack = new DataGridViewTextBoxColumn { DataPropertyName = "Split4Pack", HeaderText = "Split4Pack " };
                    //DataGridViewTextBoxColumn UseInternalDDRemovalLogic = new DataGridViewTextBoxColumn { DataPropertyName = "UseInternalDDRemovalLogic", HeaderText = "UseInternalDDRemovalLogic " };
                    //DataGridViewTextBoxColumn Is_Single = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Single", HeaderText = "Is_Single " };
                    //DataGridViewTextBoxColumn Is_EP = new DataGridViewTextBoxColumn { DataPropertyName = "Is_EP", HeaderText = "Is_EP " };
                    //DataGridViewTextBoxColumn Is_Instrumental = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Instrumental", HeaderText = "Is_Instrumental " };
                    //DataGridViewTextBoxColumn Is_Soundtrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Soundtrack", HeaderText = "Is_Soundtrack " };
                    //DataGridViewTextBoxColumn Has_Had_Audio_Changed = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Had_Audio_Changed", HeaderText = "Has_Had_Audio_Changed " };
                    //DataGridViewTextBoxColumn Has_Had_Lyrics_Changed = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Had_Lyrics_Changed", HeaderText = "Has_Had_Lyrics_Changed " };
                    //DataGridViewTextBoxColumn Album_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Sort", HeaderText = "Album_Sort " };
                    //DataGridViewTextBoxColumn Is_Uncensored = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Uncensored", HeaderText = "Is_Uncensored " };
                    //DataGridViewTextBoxColumn IntheWorks = new DataGridViewTextBoxColumn { DataPropertyName = "IntheWorks", HeaderText = "IntheWorks " };
                    //DataGridViewTextBoxColumn LyricsLanguage = new DataGridViewTextBoxColumn { DataPropertyName = "LyricsLanguage", HeaderText = "LyricsLanguage " };
                    //DataGridViewTextBoxColumn ImprovedWithDM = new DataGridViewTextBoxColumn { DataPropertyName = "ImprovedWithDM", HeaderText = "ImprovedWithDM " };
                    //DataGridViewTextBoxColumn Is_FullAlbum = new DataGridViewTextBoxColumn { DataPropertyName = "Is_FullAlbum", HeaderText = "Is_FullAlbum " };
                    //DataGridViewTextBoxColumn PitchShiftableEsOrDd = new DataGridViewTextBoxColumn { DataPropertyName = "PitchShiftableEsOrDd", HeaderText = "PitchShiftableEsOrDd " };
                    //DataGridViewTextBoxColumn Import_AuditTrail_ID = new DataGridViewTextBoxColumn { DataPropertyName = "Import_AuditTrail_ID", HeaderText = "Import_AuditTrail_ID " };
                    //DataGridViewTextBoxColumn Is_Remastered = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Remastered", HeaderText = "Is_Remastered " };
                    //DataGridViewTextBoxColumn Is_Demo = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Demo", HeaderText = "Is_Demo " };
                    //DataGridViewTextBoxColumn Is_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Cover", HeaderText = "Is_Cover " };
                    //DataGridViewTextBoxColumn Is_Remix = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Remix", HeaderText = "Is_Remix " };
                    //DataGridViewTextBoxColumn Is_Karaoke = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Karaoke", HeaderText = "Is_Karaoke " };
                    //DataGridViewTextBoxColumn Has_Featuring = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Featuring", HeaderText = "Has_Featuring " };
                    //DataGridViewTextBoxColumn Is_Medley = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Medley", HeaderText = "Is_Medley " };
                    //DataGridViewTextBoxColumn Is_MultiStrings = new DataGridViewTextBoxColumn { DataPropertyName = "Is_MultiStrings", HeaderText = "Is_MultiStrings " };
                    //DataGridViewTextBoxColumn Is_Deluxe = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Deluxe", HeaderText = "Is_Deluxe " };
                    //DataGridViewTextBoxColumn Is_GreatestHits = new DataGridViewTextBoxColumn { DataPropertyName = "Is_GreatestHits", HeaderText = "Is_GreatestHits " };
                    //DataGridViewTextBoxColumn Is_Midi = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Midi", HeaderText = "Is_Midi " };
                    //DataGridViewTextBoxColumn Is_GameSoundtrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_GameSoundtrack", HeaderText = "Is_GameSoundtrack " };
                    //DataGridViewTextBoxColumn Is_TVTheme = new DataGridViewTextBoxColumn { DataPropertyName = "Is_TVTheme", HeaderText = "Is_TVTheme " };
                    //DataGridViewTextBoxColumn Is_AmateurCover = new DataGridViewTextBoxColumn { DataPropertyName = "Is_AmateurCover", HeaderText = "Is_AmateurCover " };
                    //DataGridViewTextBoxColumn BasedOn_Youtube = new DataGridViewTextBoxColumn { DataPropertyName = "BasedOn_Youtube", HeaderText = "BasedOn_Youtube " };
                    //DataGridViewTextBoxColumn BasedOn_CF = new DataGridViewTextBoxColumn { DataPropertyName = "BasedOn_CF", HeaderText = "BasedOn_CF " };
                    //DataGridViewTextBoxColumn BasedOn_Tabs = new DataGridViewTextBoxColumn { DataPropertyName = "BasedOn_Tabs", HeaderText = "BasedOn_Tabs " };
                    //DataGridViewTextBoxColumn BasedOn_GP = new DataGridViewTextBoxColumn { DataPropertyName = "BasedOn_GP", HeaderText = "BasedOn_GP " };
                    //DataGridViewTextBoxColumn ToDos = new DataGridViewTextBoxColumn { DataPropertyName = "ToDos", HeaderText = "ToDos " };
                    //DataGridViewTextBoxColumn ToneDetails = new DataGridViewTextBoxColumn { DataPropertyName = "ToneDetails", HeaderText = "ToneDetails " };
                    //DataGridViewTextBoxColumn PackageDetails = new DataGridViewTextBoxColumn { DataPropertyName = "PackageDetails", HeaderText = "PackageDetails " };
                    //DataGridViewTextBoxColumn PackingDate = new DataGridViewTextBoxColumn { DataPropertyName = "PackingDate", HeaderText = "PackingDate " };
                    //DataGridViewTextBoxColumn UpdateVersionDate = new DataGridViewTextBoxColumn { DataPropertyName = "UpdateVersionDate", HeaderText = "UpdateVersionDate " };
                    //DataGridViewTextBoxColumn AdditionalSortColumn = new DataGridViewTextBoxColumn { DataPropertyName = "AdditionalSortColumn", HeaderText = "AdditionalSortColumn " };


                    noOfRec = GetNoRec(dssx, cnb, cnc);//dssx.Tables[0].Rows.Count;
                                                       ////Adding Packing date from pack_:audittrail as from code SQL/JOIN is not working
                                                       //DataSet dts = new DataSet();
                                                       //if (SearchCmd.IndexOf("Groups,AdditionalSortColumn DESC") > 0) dts = SelectFromDB("Pack_AuditTrail", "SELECT ID, CDLC_ID, PackDate FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\" ORDER BY ID DESC;", "", cnb, cnc);/*AND ID = "+ dssx.Tables[0].Rows[l].ItemArray[0] + "*/
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


                    DataGridView.DataSource = dssx.Tables["Main"]; tst = "DataGridView.DataSource = dssx.Tables[Main]... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    DataGridView.Visible = false;
                    DataGridView.Refresh(); DataGridView.Visible = true; tst = "DataGridView.Refresh()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    dssx.Dispose(); tst = "dssx.Dispose()... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    ChangeRow();
                    tst = "Stop Populating... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                else MessageBox.Show("No such Records/Songs (Found)");
            }
            else MessageBox.Show("No Records/Songs (Imported/Found)");

            return noOfRec;
        }
        private void Update_Selected()
        {
            var tst = "Start updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var SearchCmd22 = "Select count(u.ID) as ID " + SearchCmd.Substring(SearchCmd.IndexOf("FROM Main u"), SearchCmd.IndexOf("ORDER BY") - SearchCmd.IndexOf("FROM Main u")) + ";";//+ SearchCmd22.Substring(0, SearchCmd.IndexOf("ORDER BY")) + ";");
            DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Main", SearchCmd22.Replace(",\") ;", ");"), "", cnb, cnc);
            tst = "Stop gettin Total... ";
            timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var noOfRec = "0";
            try
            {
                if (GetNoRec(dsz1, cnb, cnc) > 0) noOfRec = dsz1.Tables[0].Rows[0].ItemArray[0].ToString();
                if (SearchCmd.IndexOf("GROUP BY") > 0 && noOfRec.ToInt32() < dsz1.Tables[0].Rows.Count) //for the special SELECTS where GROUP messes the count(id)
                    noOfRec = dsz1.Tables[0].Rows.Count.ToString();
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            if (SearchCmd22.IndexOf("u WHERE") > 0)
            {
                SearchCmd22 = SearchCmd22.Replace("FROM Main u WHERE", "FROM Main u WHERE (Selected=\"Yes\") AND (");
                SearchCmd22 = SearchCmd22.Replace(";", "); ");
            }
            else if (SearchCmd22.IndexOf("WHERE u.") > 0) //for the special SELECTS where JOIN messes the adding the other WHERE condition
            {
                SearchCmd22 = SearchCmd22.Replace("WHERE u.", "WHERE u.Selected=\"Yes\" AND u.");
                //SearchCmd22 = SearchCmd22.Replace(";", "); ");
            }
            else SearchCmd22 = SearchCmd22.Replace("FROM Main u", "FROM Main u WHERE Selected=\"Yes\" ");
            DataSet dsz2 = new DataSet();
            dsz2 = SelectFromDB("Main", SearchCmd22, "", cnb, cnc);
            tst = "Stop gettin Selected... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var noOfSelRec = ""; if (GetNoRec(dsz2, cnb, cnc) > 0)
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
                chbx_HasCover.Checked = true;
                txt_Art_Hash.Text = GetHash(c("dlcm_TempPath"));
                if (chbx_AutoSave.Checked) SaveRecord();
            }

        }

        private void btn_OpenStandardization_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(c("dlcm_DBFolder"), c("dlcm_TempPath"), c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39").ToLower() == "yes" ? true : false, c("dlcm_AdditionalManipul40").ToLower() == "yes" ? true : false, cnb, txt_Artist.Text, cnc);
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
            if (chbx_Format.Text == "ImportFolder" && DirectoryExists(c("dlcm_RocksmithDLCPath")))
            {
                txt_FTPPath.Text = c("dlcm_RocksmithDLCPath");
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
                chbx_PS3HAN.Enabled = false; var txt = "";
                if (chbx_Format.Text == "PC")
                {
                    //if (!(DirectoryExists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0)) txt += "Defaulted to DLCManager PC Path: " + c("dlcm_PC");
                    txt_FTPPath.Text = DirectoryExists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0 ? c("general_rs2014path") : c("dlcm_PC");/// c("dlcm_RocksmithDLCPath");               
                }
                else
                if (chbx_Format.Text == "Mac" || chbx_Format.Text == "ImportFolder")
                {
                    //if (!(DirectoryExists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0)) txt += "Defaulted to DLCManager PC Path: " + c("dlcm_Mac");
                    txt_FTPPath.Text = DirectoryExists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac");/// c("dlcm_RocksmithDLCPath");        
                }
                else
                if (chbx_Format.Text == "ImportFolder")
                {
                    if (!DirectoryExists(c("general_rs2014path"))) txt = "ImportFolder not existing ergo defaulted to Mac!";
                    txt_FTPPath.Text = DirectoryExists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac");/// c("dlcm_RocksmithDLCPath");        
                }
                else
                {
                    txt = "No indicated Folder available";
                    txt_FTPPath.Text = "";
                }

                if (txt != "")
                {
                    DialogResult result1 = MessageBox.Show(txt, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            toolTip1.SetToolTip(txt_FTPPath, txt_FTPPath.Text);
            toolTip1.SetToolTip(btn_Package, "Packaging to " + txt_FTPPath.Text + " in " + chbx_Format.Text + "format.");
            toolTip1.SetToolTip(chbx_Format, txt_FTPPath.Text);
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
            ConfigRepository.Instance()["dlcm_AdditionalManipul7"] = chbx_ExcludeBroken.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_andCopy"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul54"] = chbx_PackBeta.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_CopyOld"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul92"] = chbx_PS3HAN.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_AdditionalManipul93"] = chbx_PS3Retail.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DupliGTrack"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autoplay"] = chbx_AutoPlay.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_netstatus"] = netstatus;
            ConfigRepository.Instance()["dlcm_No4Spliting"] = txt_No4Splitting.Value.ToString();
            ConfigRepository.Instance()["dlcm_InclMultiplyManager"] = chbx_Instances.Checked == true ? "Yes" : "No";
            //ConfigRepository.Instance()["dlcm_Filter"] = cmb_Filter.Text
            ConfigRepository.Instance()["dlcm_FilterPrevious"] = cmb_Filter.Text;
            ConfigRepository.Instance()["dlcm_FilterGroup"] = chbx_Group.Text;
            ConfigRepository.Instance()["dlcm_FilterNot"] = chbx_FilterNot.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_FilterCompound"] = chbx_FilterCompound.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_FilterOverlap"] = chbx_Overlap.Checked ? "Yes" : "No";

            SaveProfileToDB(ConfigRepository.Instance()["dlcm_Configurations"], cnb, cnc);
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)
        {
            var Filtertxt = cmb_Filter.Text;
            //var OrderAlt = "";
            if (Filtertxt == "") return;
            savesettings();
            if (chbx_AutoSave.Checked) SaveRecord(); SaveOK = false;

            var i = databox.SelectedCells.Count == 0 ? 0 : databox.SelectedCells[0].RowIndex;
            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = SearchCmd;/*, "Part of No Group", "Part of Any Group" */
            SearchCmd = GetFilter(Filtertxt, SearchCmd, i, SearchCmdf, cnb, chbx_Group.Text, "\"" + chbx_Format.Text + "\"", databox.Rows[i].Cells["Import_Date"].Value.ToString(), cnc);

            chbx_Replace.Enabled = false;
            chbx_FilterCompound.Checked = false;
            chbx_FilterNot.Checked = false;
            chbx_Overlap.Checked = false;

            try
            {
                dssx.Dispose();
                Populate(ref databox, ref Main);
                databox.Visible = false; databox.Refresh(); databox.Visible = true;
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message + "Can't run Filter ! " + SearchCmd);
            }

            if (Filtertxt == "Sorted by Groups value/Group added date") SearchCmd = SearchCmdf;

            //Update_Selected();
            SaveOK = c("dlcm_Autosave") == "Yes" ? true : false;

            //ConfigRepository.Instance()["dlcm_FilterPrevious"] = cmb_Filter.;
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
            try
            {
                DDC.Refresh();  // Important
                if (DDC.StartInfo.FileName != "")
                {
                    if (DDC.HasExited) Console.WriteLine("Exited.");
                    else Console.WriteLine("Running.");
                    //if (DDC.HasExited == false) if (ProcessStarted) { 
                    DDC.Kill();
                    DDC.Close();
                    //DDC.Dispose();
                    //btn_PlayPreview.Text = "Play Preview"; ProcessStarted = false; return; }
                }
            }
            catch (Exception ex)
            {
                var tsst = "Erro ...btn_PlayPreview_Click" + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
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
                DDC.Start(); DDC.WaitForExit(1000 * 1); //wait 1sec
                btn_PlayPreview.Text = "Play Preview";
                ProcessStarted = false;
            }
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //switch (Convert.ToString(e.Result))
            //{
            //    case  Filtertxt="done":
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
            if (txt_AudioPreviewPath.Text != "" && File.Exists(txt_AudioPreviewPath.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing Preview! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggcut.exe"),
                WorkingDirectory = AppWD.Replace("external_tools", "")
            };
            var t = txt_OggPath.Text;
            var tt = t.Replace("_fixed", "").Replace(".ogg", "_preview_fixed.ogg");
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
                            ErrorWindow frm1 = new ErrorWindow("In order to add a preview, please Install Wwise Launcher then Wwise v" +
                                wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to " +
                                "WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" +
                                Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", false, false,
                                true, "", "", "", false);
                            frm1.ShowDialog();
                        }
                        Converters(tt, ConverterTypes.Ogg2Wem, false, true);
                        txt_OggPreviewPath.Text = tt;
                        chbx_HasPreview.Checked = true;
                        txt_AudioPreviewPath.Text = tt.Replace(".ogg", ".wem");
                        chbx_HasPreview.Checked = true;
                        btn_PlayPreview.Enabled = true;
                        txt_Preview_Hash.Text = GetHash(tt);
                        AddPreview = true;
                        chbx_AudioChanged.Checked = true;
                        if (chbx_AutoSave.Checked) SaveRecord();
                        chbx_A_IsImprovedWithDM.Checked = true;
                    }
                }
            return;
        }

        private void btn_SelectPreview_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_AudioPreviewPath.Text != "" && !File.Exists(txt_AudioPreviewPath.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing Preview! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Preview OGG file";
                fbd.Filter = "OGG file (*.ogg)|*.ogg";
                fbd.Multiselect = false;
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                ConfigRepository.Instance()["dlcm_TempPath"] = fbd.FileName;
                txt_OggPreviewPath.Text = c("dlcm_TempPath");
                chbx_HasPreview.Checked = true;
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
            if (txt_Author.Text != null && txt_Author.Text != "") chbx_HasAuthor.Checked = true;
            else chbx_HasAuthor.Checked = false;
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
        private void SetSelAndExtraAttrib(bool InclSelected, bool InclBeta, bool InclLanguage, bool InclBroken, bool InclGroups, string LyricsLanguage, string Status, string invert, CheckedListBox AllGroups)
        {
            var cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder"));
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE Main SET ";
            if (InclSelected)
            {
                command.CommandText += "Selected = @param8 ";
                command.Parameters.AddWithValue("@param8", Status);
            }
            DialogResult result1 = DialogResult.No;
            if (InclBeta)
            {
                result1 = MessageBox.Show("Including Setting Beta to YES?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes)
                {
                    command.CommandText += ",Is_Beta = @param9 ";
                    command.Parameters.AddWithValue("@param9", Status);
                }
            }
            if (InclLanguage)
            {
                result1 = MessageBox.Show("Ignoring setting Language to " + chbx_LyricsLanguage.Text + "?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes)
                {
                    command.CommandText += ",LyricsLanguage = @param11 ";
                    command.Parameters.AddWithValue("@param11", LyricsLanguage);
                }
            }
            if (InclBroken)
            {
                result1 = MessageBox.Show("Including Setting Broken to YES?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes)
                {
                    command.CommandText += ",Is_Broken = @param10 ";
                    command.Parameters.AddWithValue("@param10", Status);
                }
            }

            command.CommandText += " WHERE ID " + invert + " IN (" + SearchCmd.Replace("  ", " ").Replace(SearchFields.Replace("  ", " "), "ID ").Replace(";", "") + ")";

            command.CommandType = CommandType.Text;
            UpdateDBbyExecuteNonQuery(command, cnb, cnc);
            //try
            //{
            //    cnn.Open();
            //    command.ExecuteNonQuery();
            //    cnn.Close();
            //    command.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            if (InclGroups)
            {
                var rtz = "";
                //identify all already selected Groups as to not to overitte their
                var sel = "SELECT CDLC_ID, Groupz, ID FROM Groups WHERE Type =\"DLC\" AND CDLC_ID " + invert + " IN (" + SearchCmd.Replace("  ", " ").Replace(SearchFields.Replace("  ", " "), "ID ").Replace(";", "") + ")";
                DataSet grp = new DataSet(); grp = SelectFromDB("Groups", sel, "", cnb, cnc);
                var noOfRecs = GetNoRec(grp, cnb, cnc);//grp.Tables.Count == 0 ? 0 : grp.Tables[0].Rows.Count;
                for (var k = 0; k < noOfRecs; k++)
                    if (AllGroups.GetItemChecked(k)) rtz = chbx_AllGroups.Items[k].ToString() + ",";
                result1 = MessageBox.Show("Including Setting adding each song to the groups curently selected(" + rtz + ")?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.Yes)
                {
                    var grpsel = "("; var found = false;
                    for (int j = 0; j < AllGroups.Items.Count; j++)
                    {
                        string[] gp = AllGroups.Items[j].ToString().Split('{');
                        found = false;
                        for (var k = 0; k < noOfRecs; k++)
                            if (AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == gp[0])
                            {
                                grpsel += "," + grp.Tables[0].Rows[k].ItemArray[2].ToString() + ",";
                                found = true;
                            }

                        if (!found && !AllGroups.GetItemChecked(j) && !invert.Contains("not"))
                        {
                            DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + gp[0] + "\"", "", cnb, cnc);
                            var noOfRec = GetNoRec(dgs, cnb, cnc); var grpordno = noOfRec > 0 ? dgs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";

                            var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                            var insertvalues = "\"" + txt_ID.Text + "\", \"" + gp[0] + "\", \"DLC\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grpordno + "\"";
                            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        }
                        else if (AllGroups.GetItemChecked(j) && invert.Contains("not"))
                        {
                            grpsel = grpsel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace(",)", ")") + ")";
                            if (grpsel != "()") DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND ID not IN " + grpsel + "", cnb, cnc);
                        }
                    }
                    //grpsel = grpsel.Replace(",,", ",").Replace("(,", "(").Replace(",)", ")").Replace(",)", ")") + ")";

                    //if (grpsel != "()") DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND ID not IN " + grpsel + "", cnb, cnc);

                    var tst = "Stop saving groups... "; timestamp = UpdateLog(timestamp, tst, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }
        }
        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            //DialogResult result1 = DialogResult.No;
            chbx_Selected.Checked = true;
            if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetSelAndExtraAttrib(true, chbx_InclBeta.Checked, chbx_InclLanguage.Checked, chbx_InclBroken.Checked, chbx_InclGroups.Checked, chbx_LyricsLanguage.Text, "Yes", "", chbx_AllGroups);

            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
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
                test += " or Beta";
                chbx_Beta.Checked = false;
            }
            if (chbx_InclLanguage.Checked)
            {
                command.CommandText += ",LyricsLanguage = @param11 ";
                command.Parameters.AddWithValue("@param11", chbx_LyricsLanguage.Text);
                test += " or Broken";
                chbx_Broken2.Checked = false;
            }
            if (chbx_InclBroken.Checked)
            {
                command.CommandText += ",Is_Broken = @param10,FilesMissingIssues  = @param11";
                command.Parameters.AddWithValue("@param10", "No");
                command.Parameters.AddWithValue("@param11", "");
                test += " or Broken (incl. FilesMissingIssues)";
                chbx_Broken2.Checked = false;
            }
            command.CommandType = CommandType.Text;
            UpdateDBbyExecuteNonQuery(command, cnb, cnc);
            //try
            //{
            //    cnn.Open();
            //    command.ExecuteNonQuery();
            //    cnn.Close();
            //    command.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + c("dlcm_DBFolder") + "-" + command.CommandText);
            //    throw;
            //}
            //finally
            //{
            //    if (cnn != null) cnn.Close();
            //}

            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groupz= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";
            }

            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
            Update_Selected();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected" + test);
        }

        private void btn_RemoveBassDD_Click(object sender, EventArgs e)
        {

            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = UtilitiesFunctions.GetRecord_s(cmd, cnb, cnc);

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
            chbx_A_IsImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; btn_RemoveBassDD.Enabled = true;
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = UtilitiesFunctions.GetRecord_s(cmd, cnb, cnc);

            var xmlFiles = Directory.GetFiles(SongRecord[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = SongRecord[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
                if (xml.IndexOf("showlights") < 1)
                {
                    var DDAdded = (AddDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false, txt_AddDD.Value.ToString()) == "Yes") ? "No" : "Yes";
                }
            chbx_BassDD.Checked = true;
            chbx_DD.Checked = true;
            chbx_A_IsImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_RemoveDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = false; btn_AddDD.Enabled = true; btn_RemoveBassDD.Enabled = false;
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            cmd += " ORDER BY Artist";
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = UtilitiesFunctions.GetRecord_s(cmd, cnb, cnc);

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
            chbx_A_IsImprovedWithDM.Checked = true;
            if (chbx_AutoSave.Checked) SaveRecord();
        }

        private void btn_OldFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = c("dlcm_TempPath") + "\\0_old\\" + databox.Rows[i].Cells["Original_FileName"].Value.ToString();
            StartProcesss("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            //try
            //{
            //    Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Old Folder in Explorer ! ");
            //}
        }

        private void btn_DuplicateFolder_Click(object sender, EventArgs e)
        {
            string t = c("dlcm_TempPath") + "\\0_duplicate";
            StartProcesss(@t, null);
            //try
            //{
            //    Process process = Process.Start(@t);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            //}
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            ////1. Delete Song Folder

            ///Use multi-select if the case
            var sel = "";
            for (int k = 0; k < databox.SelectedRows.Count; k++)
            {
                if (k > 0) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            var cmd = "DELETE FROM Main";
            cmd += sel.Length > 0 ? " WHERE ID IN (" + sel + ")" : txt_ID.Text;
            var i = databox.SelectedCells[0].RowIndex;

            //1. Delete DB records
            DeleteRecords(txt_ID.Text, cmd, c("dlcm_DBFolder"), c("dlcm_TempPath"), databox.SelectedRows.Count.ToString(), "", cnb, pB_ReadDLCs, cnc);/*databox.Rows[i].Cells["Original_File_Hash"].Value.ToString()*/

            //refresh 
            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
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
                    File.Copy(fold, fold2.Replace(fold, fold2) + Path.GetExtension(fold), true);
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
                var AlbumOrigArtPath = databox.Rows[i].Cells["Album_ArtPathOrig"].Value.ToString().Replace(source_dir, destination_dir);
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
                    " AlbumArt_OrigHash, Duplicate_Of, Split4Pack, UseInternalDDRemovalLogic, Is_Instrumental, Is_Single, Is_Soundtrack, Is_EP, Has_Had_Audio_Changed" +
                    ", Has_Had_Lyrics_Changed, Album_Sort, Is_Uncensored, IntheWorks, LyricsLanguage, LastConversionDateTime, ImprovedWithDM, Is_FullAlbum, PitchShiftableEsOrDd, Import_AuditTrail_ID, Is_Remastered" +
                    ",EoFPath, Is_Karaoke, Is_Cover, Has_Featuring, Is_Demo, Is_Remix, BasedOn_Youtube, BasedOn_CF, BasedOn_Tabs, ToDos, ToneDetails, PackageDetails, PackingDate, UpdateVersionDate, Has_Capo," +
                    " Has_ShowLights, Has_JVocals, Is_Medley, Is_MultiStrings, BasedOn_GP, Album_ArtPathOrig, Is_Deluxe, Is_GreatestHits, Is_Midi, Is_GameSoundtrack, Is_TVTheme, Is_AmateurCover, Is_MetalCover, Is_Ukulele, Has_Alternate_Audio, Has_Alternate_Lyrics, A440TunningFrecv";

                var insertvalues = "SELECT Song_Title+\" alt" + max.ToString() + "\", Song_Title_Sort+\" alt" + max.ToString() + "\", Album, Artist, Artist_Sort, Album_Year," +
                    " AverageTempo, Volume, Preview_Volume, \"" + AlbumArtPath + "\", \"" + AudioPath + "\", \"" + audioPreviewPath + "\", Track_No, Author, " +
                    "Version, DLC_Name+\"" + max.ToString() + "\", DLC_AppID, Current_FileName, \"" + fold2.Replace(fold, fold2) + Path.GetExtension(fold) + "\", Import_Path, Import_Date," +
                    " Folder_Name+\"" + max.ToString() + "\", File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, \"" + "Yes" + "\", Is_Multitrack," +
                    " Is_Broken, MultiTrack_Version, " + max.ToString() + ", DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover," +
                    " Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description+\" duplicate\", Comments, Has_Track_No, " +
                    "Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD," +
                    " Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType," +
                    " ToolkitVersion, Has_Author, \"" + OggPath + "\", \"" + oggPreviewPath + "\", UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD," +
                    " Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date, Is_Live," +
                    "Live_Details, Remote_Path, audioBitrate, audioSampleRate, Is_Acoustic, Has_Other_Officials, Spotify_Song_ID, Spotify_Artist_ID, Spotify_Album_ID, " +
                    "Spotify_Album_URL, Top10, Audio_OrigHash, Audio_OrigPreviewHash, AlbumArt_OrigHash, \"" + txt_ID.Text + "\", Split4Pack, UseInternalDDRemovalLogic," +
                    " Is_Instrumental, Is_Single, Is_Soundtrack, Is_EP, Has_Had_Audio_Changed, Has_Had_Lyrics_Changed, Album_Sort, Is_Uncensored, IntheWorks, LyricsLanguage," +
                    " LastConversionDateTime, ImprovedWithDM, Is_FullAlbum, PitchShiftableEsOrDd, Import_AuditTrail_ID, Is_Remastered" +
                    ",EoFPath, Is_Karaoke, Is_Cover, Has_Featuring, Is_Demo, Is_Remix, BasedOn_Youtube, BasedOn_CF, BasedOn_Tabs, ToDos, ToneDetails, PackageDetails, PackingDate, UpdateVersionDate, Has_Capo," +
                    " Has_ShowLights, Has_JVocals, Is_Medley, Is_MultiStrings, BasedOn_GP, \"" + AlbumOrigArtPath + "\", Is_Deluxe, Is_GreatestHits, Is_Midi, Is_GameSoundtrack, Is_TVTheme, Is_AmateurCover, Is_MetalCover, Is_Ukulele, Has_Alternate_Audio, Has_Alternate_Lyrics, A440TunningFrecv" +
                    " FROM Main WHERE ID = " + txt_ID.Text;
                InsertIntoDBwValues("Main", insertcmdd, insertvalues, cnb, 0, cnc);


                //getting ID
                DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + txt_Title.Text + " alt" + max.ToString() + "\"", "", cnb, cnc);
                var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                    "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                    "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                    "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty, OrigSongTrack, PrimaryTrack, Favorite, Broken, Official, PersistentID, CapoFret";
                insertvalues = "SELECT Arrangement_Name, " + CDLC_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                    "instr(JSONFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                    " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                    " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                    " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash," +
                    " Json_Hash, Part, MaxDifficulty, OrigSongTrack, PrimaryTrack, Favorite, Broken, Official, PersistentID, CapoFret FROM Arrangements WHERE CDLC_ID = " + txt_ID.Text;
                InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0, cnc);

                insertcmdd = "Tone_Name, CDLC_ID, Volume, Keyy, Is_Custom, Description, Favorite, SortOrder, NameSeparator, " +
                    //"GearList, AmpRack, Pedals, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues," +
                    //" AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType"+
                    " ConversionDateTime, lastConverjsonDateTime, Comments, Official";
                insertvalues = "SELECT Tone_Name, \"" + CDLC_ID + "\", Volume, Keyy, Is_Custom, Description, Favorite, SortOrder, NameSeparator, " +
                    //" GearList, AmpRack, Pedals, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType," +
                    //" AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType,"+
                    "ConversionDateTime, lastConverjsonDateTime, Comments, Official FROM Tones WHERE CDLC_ID = " + txt_ID.Text; ;
                InsertIntoDBwValues("Tones", insertcmdd, insertvalues, cnb, 0, cnc);

                insertcmdd = "CDLC_ID, Gear_Name, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex, Type, Comments, Tone_Name, Tone_ID, Official";
                insertvalues = "SELECT \"" + CDLC_ID + "\", Gear_Name, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex, Type, Comments, Tone_Name, Tone_ID, Official" +
                    " FROM Tones_GearList WHERE CDLC_ID = " + txt_ID.Text; ;
                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, 0, cnc);

                MessageBox.Show("Record has been duplicated");

                Populate(ref databox, ref Main);
                databox.Visible = false; databox.Refresh(); databox.Visible = true;
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

            if (txt_Track_No.Text == "-1") { chbx_A_HasTrackNo.Checked = false; databox.Rows[i].Cells["Has_Track_No"].Value = "No"; }
            else { chbx_A_HasTrackNo.Checked = true; databox.Rows[i].Cells["Has_Track_No"].Value = "Yes"; }
        }

        private void btn_OpenSongFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
            StartProcesss(filePath, null);
            //try
            //{
            //    Process process = Process.Start(@filePath);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Song Folder in Exporer ! ");
            //}
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
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
            MakeCover(cnb, cnc);
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
            //var cmd1 = "UPDATE Main SET Selected = \"No\"";
            //var test = "";
            //if (chbx_InclBeta.Checked)
            //{
            //    cmd1 += ",Is_Beta = \"No\" ";
            //    test = " and Not Beta";
            //}
            //if (chbx_InclBroken.Checked)
            //{
            //    cmd1 += ",Is_Broken = \"No\" ";
            //    test = " and Not Broken";
            //}
            //if (chbx_InclLanguage.Checked)
            //{
            //    cmd1 += ",LyricsLanguage = \"" + chbx_LyricsLanguage.Text + "\" ";
            //    test = " and with LyricsLanguage";
            //}

            //if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            //{
            //    MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (chbx_InclGroups.Checked)
            //{
            //    var cmd = "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groupz= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";

            //    DeleteFromDB("Groups", cmd, cnb, cnc);
            //    test += " and no Groups";
            //}
            //cmd1 += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 4; pB_ReadDLCs.Step = 1;
            SetSelAndExtraAttrib(true, chbx_InclBeta.Checked, chbx_InclLanguage.Checked, chbx_InclBroken.Checked, chbx_InclGroups.Checked, chbx_LyricsLanguage.Text, "No", "", chbx_AllGroups);
            pB_ReadDLCs.Increment(1); Populate(ref databox, ref Main);

            databox.Visible = false; databox.Refresh(); databox.Visible = true; pB_ReadDLCs.Increment(1);

            Update_Selected(); pB_ReadDLCs.Increment(1);
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")", "", cnb, cnc);
            pB_ReadDLCs.Increment(1); var r = GetNoRec(dhs, cnb, cnc);//dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            MessageBox.Show("All Filtered songs(" + r + ") in DB have been UN-Selected");/*+ test*/

            //DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
            //Populate(ref databox, ref Main);
            //databox.Refresh();
            //var cnt = 0;
            //if (dgt.Tables.Count > 0) cnt = dgt.Tables[0].Rows.Count;
            //Update_Selected();
            //MessageBox.Show("All Filtered songs have been marked as UnSelected" + test);
        }

        private void btn_Copy_old_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            var cmd = "SELECT * FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID ") + ")";
            //DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd, "", cnb, cnc);
            //noOfRec = GetNoRec(dhs, cnb, cnc);//dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
            var SongRecord = UtilitiesFunctions.GetRecord_s(cmd, cnb, cnc);
            var norows = SongRecord[0].NoRec.ToInt32();
            var dest = ""; var aa = ""; var bb = "";
            pB_ReadDLCs.Maximum = norows; var j = 0; var err = "";
            for (var i = 0; i < norows; i++)
            {
                string filePath = c("dlcm_TempPath") + "\\0_old\\" + SongRecord[i].Original_FileName;// dhs.Tables[0].Rows[i].ItemArray[19];
                dest = c("dlcm_RocksmithDLCPath") + "\\" + SongRecord[i].Original_FileName;// dhs.Tables[0].Rows[i].ItemArray[19];
                var eef = SongRecord[i].Available_Old;// dhs.Tables[0].Rows[i].ItemArray[90].ToString();
                if (eef == "Yes")//OLd available
                {
                    try
                    {
                        if (File.Exists(dest)) aa += SongRecord[i].Original_FileName + "\n";
                        else
                            try
                            {
                                File.Copy(filePath, dest, false);
                                j++;
                            }
                            catch (Exception ex)
                            {
                                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                err += SongRecord[i].Original_FileName + "\n";
                            }
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        //MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ex);
                    }
                }
                else bb += SongRecord[i].Original_FileName + "\n";
                pB_ReadDLCs.Value++;
            }
            MessageBox.Show((bb != "" ? "Not " : "") + "Copied:\n" + bb + "\nout of: " + j + "/" + noOfRec + ".\n\nto folder: " + c("dlcm_RocksmithDLCPath") + ". " + (aa != "" ? "\n\nAlready existing and so not copied: " + aa : "")
                + (err != "" ? "\n\nerrors: " + err : ""));
        }

        private void btn_SelectInverted_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = 4; pB_ReadDLCs.Step = 1;
            //DialogResult result1 = DialogResult.No;
            SetSelAndExtraAttrib(true, chbx_InclBeta.Checked, chbx_InclLanguage.Checked, chbx_InclBroken.Checked, chbx_InclGroups.Checked, chbx_LyricsLanguage.Text, "No", "", chbx_AllGroups);
            //            var command = cnb.CreateCommand();

            //            command.CommandText = "UPDATE Main SET ";
            //            command.CommandText += "Selected = @param8 ";
            //            command.Parameters.AddWithValue("@param8", "No");
            //            var test = "";
            //            if (chbx_InclBeta.Checked)
            //            {
            //                result1 = MessageBox.Show("Including Setting Beta to YES?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            //                if (result1 == DialogResult.Yes)
            //                {
            //                    command.CommandText += ",Is_Beta = @param9 ";
            //                command.Parameters.AddWithValue("@param9", "No");
            //                test = " or Beta";
            //            }
            //        }
            //            if (chbx_InclLanguage.Checked)
            //            {       
            //            result1 = MessageBox.Show("Including Setting Beta to YES?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            //                if (result1 == DialogResult.Yes)
            //                {
            //                    command.CommandText += ",LyricsLanguage = @param11 ";
            //                command.Parameters.AddWithValue("@param11", chbx_LyricsLanguage.Text);
            //                test = " or with LyricsLanguage";
            //            }
            //}
            //            if (chbx_InclBroken.Checked)
            //            {
            //                command.CommandText += ",Is_Broken= @param10 ";
            //                command.Parameters.AddWithValue("@param10", "No");
            //                test = " or Broken";
            //            }
            //            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";

            //            try
            //            {
            //                command.CommandType = CommandType.Text;
            //                pB_ReadDLCs.Increment(1);
            //                command.ExecuteNonQuery();
            //            }
            //            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            //var CommandText = "UPDATE Main SET ";
            //CommandText += "Selected = \"Yes\" ";

            //CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")"; pB_ReadDLCs.Increment(1);
            //UpdateDB("Main", CommandText, cnb, cnc);

            pB_ReadDLCs.Increment(1); SetSelAndExtraAttrib(true, chbx_InclBeta.Checked, chbx_InclLanguage.Checked, chbx_InclBroken.Checked, chbx_InclGroups.Checked, chbx_LyricsLanguage.Text, "No", "not ", chbx_AllGroups);

            pB_ReadDLCs.Increment(1); Populate(ref databox, ref Main);

            databox.Visible = false; databox.Refresh(); databox.Visible = true; pB_ReadDLCs.Increment(1);

            Update_Selected(); pB_ReadDLCs.Increment(1);
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID Not IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")", "", cnb, cnc);
            pB_ReadDLCs.Increment(1); MessageBox.Show("All NON Filtered songs(" + GetNoRec(dhs, cnb, cnc) + ") in DB have been marked as Selected");/*+ test*/
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
            command.CommandType = CommandType.Text;
            UpdateDBbyExecuteNonQuery(command, cnb, cnc);
            //try
            //{
            //    
            //    cnn.Open();
            //    command.ExecuteNonQuery();
            //    cnn.Close();
            //    command.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID IN(" + SearchCmd.Replace(" * ", "ID").Replace("; ", "").Replace(SearchFields, "ID") + ")", "", cnb, cnc);
            MessageBox.Show("All Filtered songs(" + GetNoRec(dhs, cnb, cnc) + ") in DB have been marked as Beta");
        }

        public void eof(bool openfile, string filePath)
        {
            string audio = "";
            if (openfile)
            {
                audio = txt_OggPath.Text;
                StartProcesss(@filePath, null);
                //try
                //{
                //    Process process = Process.Start(@filePath);
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Can not open Song Folder in Exporer ! ");
                //}
            }
            var paath = c("dlcm_EoFPath");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_EoFPath"));
            if (!File.Exists(xx))
            {
                ErrorWindow frm1 = new ErrorWindow("Install Editor on Fire if you want to use it.", c("dlcm_EoFPath_www"),
                "Missing Editor on Fire", false, false, true, "", "", "", false); frm1.ShowDialog(); return;
            }

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

            //StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btn_CreateLyrics_Click(object sender, EventArgs e)
        {
            //1. Open Internet Explorer
            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "Lyrics";

            ErrorWindow frm1 = new ErrorWindow("Option A: Use Rockband lyrics\nOption B: Tab your own\n\n\nA.\n\t1.Check if already tabbed for RockBand https://rhythmgamingworld.com" +
                "\n\n\t2. Extract using GameData tab, C3 CON Tools v4.0.1 tool BatchExtractor button (or on site https://rhythmgamingworld.com/forums/topic/c3-con-tools-v401-8142020-weve-only-just-begun/ )\n\n\t3.Import Midi \"lyrics\"" +
                " in EditorOnFire (togther with Rocksmith OGG)\n\n\t4.Test in EoF and Shift timing (select all and move or using the +option) and import back to Vocal track " +
                " \n\n\n\nB.\n\t1.Google for Lyrics e.g." + link + " \n\n\t2. Use Ultrastar" +
                "Creator Tab lyrics to the songs time signature (if crashing at play open it from outside DLC Manager)\n\n\t3. Using EditorOnFire Transform Ultrastar simple file" +
                "using Add Lyrics button\n\n\n(IF you are adding Lyrics end of song past what was captured in Ultrastar remember to select the last EoF added lyrics and Note-Lyrics Mark to save it in the saved XML.)"
                , "http://ignition.customsforge.com/eof", "Tutorial on Adding Vocal Tracks to Rocksmith CDLC", true, true, false, "Option A"
                , "Option B", "", false);
            frm1.ShowDialog();
            if (frm1.IgnoreSong)
            //try
            {
                //Process process = Process.Start("https://db.c3universe.com/songs");/*.Replace(link, "https://db.c3universe.com/songs")*/
                StartProcesss("https://db.c3universe.com/songs", null);
                var j = databox.SelectedCells[0].RowIndex;
                var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_RockBand"));
                if (!File.Exists(xx))
                {
                    ErrorWindow frm = new ErrorWindow("Install C3 Conversion Tools if you want to use it.", c("dlcm_RockBand_www"),
                                                    "Missing C3 Rockband conversion tools", false, false, true, "", "", "", false); frm.ShowDialog(); return;
                }
                StartProcesss(@xx, null);
                //Process procesf = Process.Start(@xx);
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
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
                    if (!File.Exists(xx))
                    {
                        ErrorWindow frm2 = new ErrorWindow("Install UltraStar Creator if you want to use it.",
                        c("dlcm_UltraStarCreator_www"), "Missing UltraStar Creator", false, false, true, "", "", "", false); frm2.ShowDialog(); return;
                    }

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
            StartProcesss(@filePath, null);
            //try
            //{
            //    Process process = Process.Start(@filePath);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            //}

            //4. Open EoF
            eof(true, filePath);
            //5. Import PART VOCALS_RS2.xm

            MessageBox.Show("If track is ready, press OK to select and add it");
            btn_ChangeLyrics_Click(null, null);

        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text == "")
            {
                MessageBox.Show("Add a group Name!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE (Groupz=\"" + chbx_Group.Text + "\" OR Comments=\"" + txt_Order.Value + "\") and Type=\"DLC\";", "", cnb, cnc);
            var norec = GetNoRec(drs, cnb, cnc);//drs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"DLC\";", "", cnb, cnc);
                //DataSet dfs = new DataSet(); dfs = SelectFromDB("Groups", "SELECT MAX(Comments) FROM Groups WHERE Type=\"DLC\" AND Comments<>\"\";", "", cnb, cnc);
                //var index = dfs.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1;
                norec = GetNoRec(ds, cnb, cnc);//ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string insertcmdA = "CDLC_ID, Profile_Name, Type, Comments, Groupz,Date_Added";
                    var insertA = "\"" + fnn + "\",\"\",\"DLC\",\"" + txt_Order.Value + "\",\"" + chbx_Group.Text + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"";/*index*/
                    InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb, 0, cnc);
                    CreateGroups(); UpdateGroups();
                    cmb_Filter.Items.Add("Group " + chbx_Group.Text);//add items
                }
            }
            else
            {
                var same = " (";
                DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT CDLC_ID,Comments FROM Groups WHERE Groupz=\"" + chbx_Group.Text + "\" and Type=\"DLC\";", "", cnb, cnc);
                var noec = GetNoRec(dgs, cnb, cnc);//dgs.Tables[0].Rows.Count;
                //var fnn = "";
                if (noec == 0) same += " Group name";
                //{

                //    fnn = dgs.Tables[0].Rows[0].ItemArray[0].ToString();
                //}
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Comments=\"" + txt_Order.Value + "\" and Type=\"DLC\" AND " +
                    "CDLC_ID NOT IN (SELECT CDLC_ID FROM Groups WHERE Groupz=\"" + chbx_Group.Text + "\" and Type=\"DLC\");", "", cnb, cnc);
                var noc = GetNoRec(dhs, cnb, cnc);//dhs.Tables[0].Rows.Count;
                if (noc > 0) same += same == " (" ? " same Order number " : " and same Order number ";
                same += same == " (" ? " to a new Order Number (" + txt_Order.Value + "))" : " already exists)";
                //DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"DLC\";", "", cnb, cnc);
                //            norec = ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count;
                //            if (norec > 0) same += same == " ("?" to a new Order Number)":" already exists)";
                //            else same += "new Order Number )";
                DialogResult result1 = MessageBox.Show("Do you Wanna rename/update the (" + chbx_Group.Text + " / " + dgs.Tables[0].Rows[0].ItemArray[1].ToString() + ") Group?" + same, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {
                    if (same.ToLower().Contains("Order number".ToLower()))
                        if (!same.ToLower().Contains("new"))
                        {
                            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"DLC\";", "", cnb, cnc);
                            norec = GetNoRec(ds, cnb, cnc);//ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count;
                            if (norec > 0)
                            {
                                var r = (ds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1).ToString();
                                DialogResult result2 = MessageBox.Show("Do you Wanna update other Same Order Number group to the next avail one (" + r + ") ? ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result1 == DialogResult.Yes) txt_Order.Text = r;
                            }
                        }
                    DataSet dts = new DataSet(); var sel = "UPDATE Groups SET ";
                    if (same.ToLower().Contains("same")) sel += "Groupz=\"" + chbx_Group.Text + "\",";
                    sel += " Comments=\"" + txt_Order.Text + "\" WHERE (Type=\"DLC\" AND Groupz=\"" + chbx_Group.Text + "\")";/*CDLC_ID=" + drs.Tables[0].Rows[0].ItemArray[0].ToString() + "and*/
                    if (same.ToLower().Contains("same")) sel += " OR Comments =\"" + dgs.Tables[0].Rows[0].ItemArray[1].ToString() + "\",";
                    dts = UpdateDB("Groups", sel, cnb, cnc);
                    CreateGroups();
                }
                //else MessageBox.Show("Please chose a unique name");
            }
        }
        void CreateFieldsDropdown()
        {
            var t = c("dlcm_SearchFields").Replace(" ", "").Split(',');
            for (int j = 0; j < c("dlcm_SearchFields").Replace(" ", "").Split(',').Length; j++)
                cmb_SearchFields.Items.Add(t[j].ToString());
        }

        void CreateGroups()
        {
            //Create Groups list Dropbox
            var norec = 0;
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groupz,Comments FROM Groups WHERE Type=\"DLC\" ORDER BY Comments;", "", cnb, cnc);
            norec = GetNoRec(ds, cnb, cnc);//ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : 0;
            if (norec > 0)
                if (chbx_Group.Items.Count > 0)//remove items
                {
                    chbx_Group.DataSource = null;
                    for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                    {
                        chbx_Group.Items.RemoveAt(k);
                        chbx_AllGroups.Items.RemoveAt(k);
                    }
                }

            if (c("dlcm_maxsongsinweekly").ToInt32() > 0 && norec > 0)
            {
                var cmd = "UPDATE Groups SET Groupz=\"" + "Group Top " + c("dlcm_maxsongsinweekly") + "(weekly)" + "\" WHERE Groupz like '*(weekly)*'";
                UpdateDB("Groups", cmd, cnb, cnc);
                DataSet drm = new DataSet(); drm = SelectFromDB("Groups",
                    "SELECT Top 1 Comments FROM Groups WHERE Type=\"DLC\" AND Comments<>\"\" AND Groupz=\"Group " + "Top " + c("dlcm_maxsongsinweekly") + "(weekly)" + "\"", "", cnb, cnc);
                var norecs = GetNoRec(drm, cnb, cnc);//drs.Tables[0].Rows.Count;
                var no = "";
                if (norecs > 0)
                    no = drm.Tables[0].Rows[0][0].ToString();

                chbx_Group.Items.Add("Top " + c("dlcm_maxsongsinweekly") + "(weekly)");
                if (norecs > 0)
                    chbx_AllGroups.Items.Add("Top " + c("dlcm_maxsongsinweekly") + "(weekly)" + "{" + no + "}[" + (GetCount(c("dlcm_HotGrp")) >= 7 ? 7 : GetCount(c("dlcm_HotGrp"))) + "]");
            }

            for (int j = 0; j < norec; j++)//add items
            {
                if (ds.Tables[0].Rows[j][0].ToString().Contains("(weekly)")) continue;

                //Get Order no
                DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT Top 1 Comments FROM Groups WHERE Type=\"DLC\" AND Comments<>\"\" AND Groupz=\"" + ds.Tables[0].Rows[j][0].ToString() + "\"", "", cnb, cnc);
                var norecs = GetNoRec(drs, cnb, cnc);//drs.Tables[0].Rows.Count;
                var ord = "";
                if (norecs > 0)
                    ord = drs.Tables[0].Rows[0][0].ToString();

                chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString() + "{" + ord + "}[" + GetCount(ds.Tables[0].Rows[j][0].ToString()) + "]");
            }
        }

        int GetCount(string Grp)
        {
            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT count(*) as ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + Grp + "\"", "", cnb, cnc);
            var norecs = GetNoRec(drs, cnb, cnc);//drs.Tables.Count == 0 ? 0 : drs.Tables[0].Rows.Count;
            if (norecs > 0)
            {
                return drs.Tables[0].Rows[0][0].ToString().ToInt32();
            }
            else return 0;
        }

        //public static DateTime REconvStringtoDAte(string s)
        //{
        //    return null;
        //}
        void UpdateGroups()
        {
            try
            {

                //remove flag
                for (int j = 0; j < chbx_AllGroups.Items.Count; j++) chbx_AllGroups.SetItemChecked(j, false);

                //getHotest
                DataSet ddg = new DataSet(); ddg = SelectFromDB("Groups", "SELECT TOP " + c("dlcm_maxsongsinweekly") + " Groupz, Comments, Date_Added FROM Groups " +
                            "WHERE Type=\"DLC\" ORDER BY Date_Added Desc;", "", cnb, cnc);
                var nocrec = ddg.Tables[0].Rows.Count;
                //DateTime min= DateTime.Now;
                var min = "";
                var lastrow = ((c("dlcm_maxsongsinweekly").ToInt32() - 1) > c("dlcm_maxsongsinweekly").ToInt32()) ?
                    nocrec : c("dlcm_maxsongsinweekly").ToInt32() - 1;
                if (nocrec > 0) min = ddg.Tables[0].Rows[lastrow][2].ToString().Replace(" ", "");

                btn_ADD2HOT.Text = "Add to Hot"; btn_ADD2HOT.Enabled = true;
                DataSet dds = new DataSet(); dds = SelectFromDB("Groups", "SELECT DISTINCT Groupz, Comments, Date_Added FROM Groups " +
                    "WHERE Type=\"DLC\" AND CDLC_ID=\"" + databox.Rows[databox.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\" ORDER BY Date_Added Desc;", "", cnb, cnc);
                nocrec = GetNoRec(dds, cnb, cnc);//dds.Tables[0].Rows.Count;
                bool nothot = true; /*var m = 0;*/
                if (nocrec > 0)
                {
                    for (int j = 0; j < nocrec; j++)
                    {
                        var dfg = dds.Tables[0].Rows[j][0].ToString();
                        var grp = dds.Tables[0].Rows[j][1].ToString();
                        var da = dds.Tables[0].Rows[j][2].ToString();
                        for (var k = 0; k < chbx_AllGroups.Items.Count; k++)
                            if (chbx_AllGroups.Items[k].ToString().IndexOf(dfg + "{" + grp + "}") >= 0)/*[" + GetCount(dfg) + "]"*/
                            {
                                chbx_AllGroups.SetItemChecked(k, true);
                                if (dfg == c("dlcm_HotGrp"))
                                {/*.ToDateTime()*/
                                    btn_ADD2HOT.Enabled = true;
                                    if (double.Parse(da.Replace(" ", "")) <= double.Parse(min)) //;// btn_ADD2HOT.Enabled = false;
                                                                                                // else 
                                        btn_ADD2HOT.Text = "MakeHot-est(" + c("dlcm_maxsongsinweekly") + ")";
                                    nothot = false;
                                    if (chbx_AllGroups.Items[0].ToString().ToString().Contains("weekly")) chbx_AllGroups.SetItemChecked(0, true);
                                }
                            }
                    }
                    if (nothot)
                        btn_ADD2HOT.Text = "Add2Hot";
                }
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

        }

        void ListSettings()
        {
            //Create Groups list Dropbox
            var norec = 0;
            var tt = "SELECT DISTINCT Comments, Groupz, ID FROM Groups WHERE Type =\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" ORDER BY Comments;";
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Comments, Groupz, ID FROM Groups WHERE Type=\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" ORDER BY Comments; ", "", cnb, cnc);
            norec = GetNoRec(ds, cnb, cnc);//ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : 0;
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
            DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND Groupz= \"" + chbx_Group.Text + "\"", cnb, cnc);
            GroupChanged = true;
            CreateGroups(); UpdateGroups();
        }

        private void chbx_AllGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            //var norec = 0;
            GroupChanged = true;
            try
            {
                var v = chbx_AllGroups.Items[chbx_AllGroups.SelectedIndex];
                string[] gp = v.ToString().Split('{');
                DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT TOP 1 Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + gp[0] + "\"", "", cnb, cnc);
                var noOfRegc = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count > 0 ? dgs.Tables[0].Rows.Count : 0;
                if (noOfRegc > 0)
                    txt_Order.Value = Decimal.Parse(dgs.Tables[0].Rows[0].ItemArray[0].ToString());

            }
            catch (Exception er) { var tsst = "Error ..." + er.Message; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
        }

        private void chbx_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var norec = 0;
            //GroupChanged = true;
            try
            {
                DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT TOP 1 Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + chbx_Group.Text + "\"", "", cnb, cnc);
                var noOfRegc = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count > 0 ? dgs.Tables[0].Rows.Count : 0;
                if (noOfRegc > 0)
                    txt_Order.Value = Decimal.Parse(dgs.Tables[0].Rows[0].ItemArray[0].ToString());

            }
            catch (Exception er) { var tsst = "Error ..." + er.Message; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
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
                        StartProcesss(@filePath, null);
                        //try
                        //{
                        //    Process process = Process.Start(@filePath);
                        //}
                        //catch (Exception ex)
                        //{
                        //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    MessageBox.Show("Can not open Garageband ready Song Folder in Exporer ! ");
                        //}
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
            if (txt_Lyrics.Text != "" && !File.Exists(txt_Lyrics.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing Lyric! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
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
                    txt_Platform.Text.ToString().Replace("PC", "Pc"), txt_ID.Text, chbx_Lyrics.Checked, cnc);
                txt_Lyrics.Text = fbd.FileName.IndexOf(databox.Rows[i].Cells["Folder_Name"].Value.ToString()) < 0 ? databox.Rows[i].Cells["Folder_Name"].Value.ToString() + "\\songs\\arr\\" + Path.GetFileName(fbd.FileName) : fbd.FileName;
                chbx_LyricsChanged.Checked = true;
                chbx_Lyrics.Checked = true;
                btn_ShowLyrics.Enabled = true;
                btn_CreateLyrics.Enabled = false;
                if (chbx_AutoSave.Checked) SaveRecord();
                ListTracks(txt_DuplicateOf.Text, txt_ID.Text, databox.Rows[i].Cells["Song_Lenght"].Value.ToString());
                chbx_A_IsImprovedWithDM.Checked = true;
            }
        }

        static void ApplyLyric(string fn, string InitialDirectory, OleDbConnection cnb, string plTfrm, string cid, bool exislyr, SQLite.SQLiteConnection cnc)
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
                sng.WriteSng(outputStream, new RocksmithToolkitLib.Platform(plTfrm, GameVersion.RS2014.ToString()));
            }
            File.Copy(outputFile, InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\bin\\generic\\" + Path.GetFileName(outputFile), true);
            DeleteFile(outputFile, false);
            outputFile = InitialDirectory + (plTfrm.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") + "\\songs\\bin\\generic\\" + Path.GetFileName(outputFile);

            var SNGHash = "";
            SNGHash = GetHash(outputFile);

            DataSet dsr = new DataSet();
            var StartTime = GetTrackStartTime(tmp, RouteMask.None.ToString(), ArrangementType.Vocal.ToString(), false, -1);
            if (exislyr)
            {
                //in case update command is not working
                dsr = UpdateDB("Arrangements", "UPDATE Arrangements SET XMLFilePath=\"" + tmp + "\", XMLFile_Hash=\"" + FileHash + "\",JSONFilePath=\"" + outputFile + "\", SNGFileHash=\"" + SNGHash + "\", Comments= \"Comments added with DLCManager\", Start_Time= \"" + StartTime + "\" WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + cid + ";", cnb, cnc);
            }
            else
            {
                var insertcmdd = "Arrangement_Name,CDLC_ID, ArrangementType, Bonus, ArrangementSort, TuningPitch, RouteMask, Has_Sections, XMLFilePath, XMLFile_Hash, JSONFilePath, SNGFileHash, Comments, CleanedXML_Hash, Json_Hash, Start_Time, XMLFileName";
                var insertvalues = "\"4\", " + cid + ", \"Vocal\", \"false\", \"0\", \"0\", \"None\",\"No\",\"" + tmp + "\",\"" + FileHash + "\",\"" + outputFile + "\",\"" + SNGHash + "\",\"" +
                    "added with DLCManager " + fn + "\",\"" + SNGHash + "\",\"" + FileHash + "\",\"" + StartTime + "\",\"" + Path.GetFileName(tmp) + "\"";
                InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0, cnc);
            }


        }

        private void btn_ApplyAlbumSortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Album_Short = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
            cmd1 = "UPDATE Main SET Album_ShortName = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dhj = UpdateDB("Main", cmd1 + ";", cnb, cnc);
        }

        public void ReadGameLibrary()
        {
            // Process the list of files found in the directory.
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var logmss = "Starting... " + startT; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var pathx = txt_FTPPath.Text;
            UpdateDB("Main", "Update Main Set Remote_Path = \"\";", cnb, cnc); logmss = "Cleared Remote Songs... "; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            string[] fileEntries = new string[20000];
            string[] fileEntrie = new string[20000];
            string[] dates = new string[20000]; ConfigRepository.Instance()["dlcm_FTPstatus"] = "OK";
            if (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") fileEntrie = GetFTPFiles(pathx);
            else try { fileEntries = Directory.GetFiles(pathx, "*" + (chbx_Format.Text == "PC" ? "_p." : chbx_Format.Text == "Mac" ? "_m." : chbx_Format.Text == "XBOX360" ? "" : "") + "psarc*", SearchOption.TopDirectoryOnly); }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            var z = 0;
            if (fileEntrie is null) return;
            if (fileEntrie[0] is null) return;

            var l = 0;
            foreach (string fileName in fileEntrie)
            {
                if (fileName == null) break;
                string[] args = (fileName).ToString().Split(';');
                fileEntries[l] = args[0];
                dates[l] = args[1];
                l++;
            }
            var pack = GetMax("Pack_AuditTrail", "Pack", cnb, cnc);

            var found = "\"";
            var newn = "\"";
            var norec = 0;
            var t = "";
            var j = 0;
            if (fileEntries != null)
            {
                //foreach (string fileName in fileEntries)
                //{
                //    if (fileName == null) break; z++;
                //}
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Step = 1;
                pB_ReadDLCs.Maximum = l;
                z = 0;
                var tst = "";
                //Get (and SAve each New File Name in the FilesMissingIssues eXisting)
                foreach (string fileName in fileEntries)
                {
                    if (fileName == null) break;
                    tst = pB_ReadDLCs.Value + "/" + z + "-" + Path.GetFileName(fileName);
                    pB_ReadDLCs.Increment(50);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new System.Drawing.Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

                    if (fileName.IndexOf("s1compatibility") > 0)
                        continue;
                    z++; logmss = "Reading... " + fileName + "..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID, PackDate FROM Pack_AuditTrail WHERE FileName=\"" + Path.GetFileName(fileName) + "\";", "", cnb, cnc);
                    norec = GetNoRec(dfs, cnb, cnc);//dfs.Tables[0].Rows.Count;
                    if (norec == 0) newn += fileName + "\";\"";
                    else
                    {
                        logmss = "Old song ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var dlcid = dfs.Tables[0].Rows[0].ItemArray[0].ToString(); var dat = dfs.Tables[0].Rows[0].ItemArray[1].ToString();
                        if (("-" + found).IndexOf("\"" + dlcid + "\"") <= 0)
                        {
                            found += dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(fileName) + "\" WHERE ID=" + dlcid + ";", cnb, cnc);
                            logmss = "Unique song the Remote location ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        else
                        {
                            var fn = "";
                            DataSet dfg = new DataSet(); dfg = SelectFromDB("Main", "SELECT ID, Remote_Path, \"\", ID, Artist, Album, Song_Title, Album_Year, File_Creation FROM Main WHERE ID=" + dlcid + ";", "", cnb, cnc);
                            //var ptz = new System.IO.FileInfo(fileName);
                            var info = dfg.Tables[0].Rows[0].ItemArray[3].ToString() + " - " + dfg.Tables[0].Rows[0].ItemArray[4].ToString()
                                + " - " + dfg.Tables[0].Rows[0].ItemArray[5].ToString() + " - " + dfg.Tables[0].Rows[0].ItemArray[6].ToString()
                                + " - " + dfg.Tables[0].Rows[0].ItemArray[7].ToString();
                            DialogResult result1 = MessageBox.Show("Duplicate DLC has been found!\n\n" + info + ":\n\nChose which to imediatelly delete:\n\n1. "
                                + dfg.Tables[0].Rows[0].ItemArray[1].ToString() + " (" + dfg.Tables[0].Rows[0].ItemArray[8].ToString() + ")\n\n2. " +
                                Path.GetFileName(fileName) + " (" + dates[z] + ")\n\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            //DialogResult result1 = MessageBox.Show("Duplicate DLC has been found!\n\nChose which to imediatelly delete:\n\n1. " + dfg.Tables[0].Rows[0].ItemArray[1].ToString() + +" (" + dfg.Tables[0].Rows[0].ItemArray[].ToString() + ")\n\n2. " + Path.GetFileName(fileName) + " (" + ptz + ")\n\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                                    unpackedDir = Packer.Unpack(tt, c("dlcm_TempPath"), platform, true, true);
                                    try
                                    {
                                        info = DLCPackageData.LoadFromFolder(unpackedDir, platform);
                                    }
                                    catch (Exception ee)
                                    {
                                        logmss = "Error at song Read/Load ..." + ee; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        newn += t + "\";\"";
                                        continue;
                                    }
                                    logmss = "Stop  Unpack&Read ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                                    //Generating the HASH code
                                    var FileHash = ""; FileHash = GetHash(tt);
                                    DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + info.Name + "\";", "", cnb, cnc);//+ song.Identifier + "\";");
                                    DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + info.SongInfo.SongDisplayName + "\";", "", cnb, cnc); //song.Title
                                    DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb, cnc);
                                    DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + info.Name.Substring(5, info.Name.Length - 5) + "\";", "", cnb, cnc);
                                    DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"%" + info.Name.Substring(5, info.Name.Length - 5) + "%\";", "", cnb, cnc);
                                    DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"%" + info.SongInfo.SongDisplayName + "%\";", "", cnb, cnc);
                                    noreca = GetNoRec(dfa, cnb, cnc);//dfa.Tables[0].Rows.Count;
                                    norecb = GetNoRec(dfb, cnb, cnc);//dfb.Tables[0].Rows.Count;
                                    norecc = GetNoRec(dfc, cnb, cnc);//dfc.Tables[0].Rows.Count;
                                    norecd = GetNoRec(dfd, cnb, cnc);//dfd.Tables[0].Rows.Count;
                                    norece = GetNoRec(dfe, cnb, cnc);//dfe.Tables[0].Rows.Count;
                                    norecf = GetNoRec(dff, cnb, cnc);//dff.Tables[0].Rows.Count;
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

                                            //var i = 0;
                                            string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack";
                                            var fnnon = Path.GetFileName(fnn);
                                            var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                            var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + info.Name + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\",\"" + pack + "\"";
                                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0, cnc);
                                            found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb, cnc);
                                            DeleteFile(tt, false);
                                            logmss = "Stop adding song based on newly read metadata..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        }
                                        else
                                        {
                                            var fn = "";
                                            DataSet dfg = new DataSet(); dfg = SelectFromDB("Main", "SELECT ID,Remote_Path FROM Main WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", "", cnb, cnc);
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
                                    DeleteDirectory(unpackedDir, false);
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

                            var Temp_Path = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_temp";
                            var unpackedDir = "";
                            DLCPackageData info = null;
                            platform = tt.GetPlatform();
                            timestamp = UpdateLog(timestamp, "Unpack song", true, Temp_Path, "", "", null, null);

                            try
                            {
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul51"] == "Yes")
                                    unpackedDir = Packer.Unpack(tt, Temp_Path, platform, true, true);
                                else
                                    unpackedDir = Packer.Unpack(tt, Temp_Path, platform, true, false);
                                timestamp = UpdateLog(timestamp, "Load song", true, Temp_Path, "", "DLCManager", null, null);
                                info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                                DeleteDirectory(unpackedDir, false);
                            }
                            catch (Exception ee)
                            {
                                timestamp = UpdateLog(timestamp, "Erro" + ee.Message + " Broken Song at Pack", true, Temp_Path, "", "", null, null);
                                //var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                //if (chbx_Additional_Manipulations.GetItemChecked(30))
                                //    CopyMoveFileSafely(FullPath, Pathh, chbx_Additional_Manipulations.GetItemChecked(75), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                            }


                            //var browser = new PsarcBrowser(tt);
                            //var songlist = browser.GetSongList();
                            //var toolkitInfo = browser.GetToolkitInfo();
                            //foreach (var song in songlist)
                            //{
                            var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                            //Generating the HASH code
                            var FileHash = ""; FileHash = GetHash(tt);
                            logmss = "Checking non PS3 against metadata..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + info.Name + "\";", "", cnb, cnc);
                            DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + info.SongInfo.SongDisplayName + "\";", "", cnb, cnc);
                            DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT CDLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb, cnc);
                            DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + info.Name.Substring(5, info.Name.Length - 5) + "\";", "", cnb, cnc);
                            DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"%" + info.Name.Substring(5, info.Name.Length - 5) + "%\";", "", cnb, cnc);
                            DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"%" + info.SongInfo.SongDisplayName + "%\";", "", cnb, cnc);
                            //noreca = dfa.Tables[0].Rows.Count; norecb = dfb.Tables[0].Rows.Count; norecc = dfc.Tables[0].Rows.Count; norecd = dfd.Tables[0].Rows.Count; norece = dfe.Tables[0].Rows.Count; norecf = dff.Tables[0].Rows.Count;
                            noreca = GetNoRec(dfa, cnb, cnc);//dfa.Tables[0].Rows.Count;
                            norecb = GetNoRec(dfb, cnb, cnc);//dfb.Tables[0].Rows.Count;
                            norecc = GetNoRec(dfc, cnb, cnc);//dfc.Tables[0].Rows.Count;
                            norecd = GetNoRec(dfd, cnb, cnc);//dfd.Tables[0].Rows.Count;
                            norece = GetNoRec(dfe, cnb, cnc);//dfe.Tables[0].Rows.Count;
                            norecf = GetNoRec(dff, cnb, cnc);//dff.Tables[0].Rows.Count;
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
                                var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + info.Name + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\",\"" + pack + "\"";
                                InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0, cnc);
                                found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                DataSet dxr = new DataSet(); UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb, cnc);
                                try { DeleteFile(tt, false); }
                                catch (Exception ex)
                                {
                                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                    MessageBox.Show("4067 " + ex.Message);
                                }
                            }
                            else newn += t + "\";\"";
                            //}
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
                databox.Visible = false; databox.Refresh(); databox.Visible = true;
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show("4098 " + ex.Message + "Can't run Filter ! " + SearchCmd);
            }
            cmb_Filter.Text = "Songs in Rocksmith Game Lib";
            logmss = "Done Reading the Library ..."; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }
        private void btn_ReadGameLibrary_Click(object sender, EventArgs e)
        {
            ReadGameLibrary();
        }

        private void btn_RemoveRemoteSong_Click(object sender, EventArgs e)
        {
            var outp = "";
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\" WHERE ID=" + txt_ID.Text + ";", cnb, cnc);
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\";", cnb, cnc);
            MessageBox.Show("All Remote-s songs have been deleted");
        }

        private void btn_Find_FilesMissingIssues_Click(object sender, EventArgs e)
        {
            var NoRencodes = 0; var NoOrphan = 0; var NoOrphanSpotifyArt = 0; ; var NoOrphanD = 0;
            var NoAudio = 0; var NoAlbumArt = 0; var NoOggPreview = 0; var NoAudioPreview = 0; var NoOgg = 0; var NoOld = 0; var NoPackTrail = 0;
            var NoSNG = 0; var NoXML = 0; var NoJSON = 0; var NoArg = 0; var NoExtra = 0; var NoOGroup = 0; var NoOArg = 0; var NoOTones = 0; var NoOTGL = 0;
            var max = 13; var NoPAT = 0; var NoOGrp = 0; var noOfDLCGrpDpl = 0; var NoMetaFilled = 0; var NoMetaMissing = 0;
            var o = 1; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; var oldvl = c("dlcm_AdditionalManipul81"); var filePath = ""; pB_ReadDLCs.Maximum = max;
            IEnumerable<string> dirs;

            BackupDB(true);
            //1.check wem bitrate
            //note some ogg to wem recopressions raise the textual bitrate
            var dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to process all Audio/WEM files, check the individual bitrate then downstream to desired?\n" +
                "Note some WEM connv raises the bitrate of the audio(but not the file size) so some wems will always get rencoded when checking.";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                timestamp = UpdateLog(timestamp, "0/" + max + " Check for off settings BitRate", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                var sel = "SELECT AudioPath, audioPreviewPath, OggPath, oggPreviewPath, audioBitrate, ID FROM Main WHERE"
                    + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY Spotify_Artist_ID ASC";
                DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
                var noOfRecs = GetNoRec(SongRecord, cnb, cnc);//.Tables.Count > 0 ? SongRecord.Tables[0].Rows.Count : 0;
                NoRencodes = noOfRecs;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;
                var j = 0;
                for (var i = 0; i < noOfRecs; i++)
                {
                    if (!ShowDialogue(dlg, " process all ", noOfRecs, "fix", 2)) break;
                    var paath = c("dlcm_MediaInfo_CLI");
                    var xx = "";
                    if (File.Exists(paath)) xx = paath;
                    else xx = Path.Combine(AppWD, "MediaInfo_CLI_17.12_Windows_x64", "MediaInfo.exe");
                    if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install MediaInfo CLI if you want to use it.", c("dlcm_MediaInfo_CLI_www"), "Missing MediaInfo CLI", false, false, true, "", "", "", false); frm1.ShowDialog(); break; }

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
                                    FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "MainDB", cnc);

                                    j++; timestamp = UpdateLog(timestamp, SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "_" + j + "/" + i + "/" + noOfRecs + " bitrate " + stdoutx.Replace("\r\n", ""), true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                }

                        }
                    //pB_ReadDLCs.Maximum = noOfRecs;
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing BitRate", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }

            //2.Check existing 0_data folders
            timestamp = UpdateLog(timestamp, o + "/" + max + " Check existing 0_data folders", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o;
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to Check 0_data folders if belonging to a (Main)DB CDLC?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                filePath = c("dlcm_TempPath") + "\\0_data\\"; dirs = from dir in Directory.EnumerateDirectories(filePath, "*") select dir;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = dirs.Count();
                oldvl = c("dlcm_AdditionalManipul81");
                ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = "Yes";
                string[] del = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                foreach (string s in dirs)
                {

                    //if (s.IndexOf("0_old") <= 0 && s.IndexOf("0_repacked") <= 0 && s.IndexOf("0_duplicate") <= 0 && s.IndexOf("0_dlcpacks") <= 0 && s.IndexOf("0_broken") <= 0 && s.IndexOf("0_albumCovers") <= 0 && s.IndexOf("0_log") <= 0 && s.IndexOf("0_archive") <= 0 && s.IndexOf("0_data") <= 0 && s.IndexOf("0_temp") <= 0 && s.IndexOf("0_to_import") <= 0)
                    //{
                    var cmd = "SELECT ID FROM Main WHERE Folder_Name=\"" + s + "\" ";/*ORDER BY ID DESC;*/
                    DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, "", cnb, cnc);
                    var noOfFlds = GetNoRec(drs, cnb, cnc);// 0;
                                                           //if (drs.Tables.Count > 0) noOfFlds = drs.Tables[0].Rows.Count;
                    if (noOfFlds == 0)
                    {
                        NoOrphanD++;
                        del[NoOrphanD] = s;
                        timestamp = UpdateLog(timestamp, "\n\tTo Delete orphaned folder:" + s, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing 0_data folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = NoOrphanD;
                if (NoOrphanD > 0)
                    if (!ShowDialogue(dlg, " Check ", NoOrphanD, "delete (more info in logs)", 2))
                        for (var j = 0; j < NoExtra; j++)
                        {
                            pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Deleting existing 0_data folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            DeleteDirectory(del[j], false);
                            timestamp = UpdateLog(timestamp, "\n\tDeleted orphan folder:" + del[j], true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }

            }
            // timestamp = UpdateLog(timestamp, o + "/4 check Spotify Album art file in Standardisation", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //3.Check existing 0 folders
            //string 
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " Check existing folders in home \"0\"", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to Check for rogue folders in home \"0\"?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                filePath = c("dlcm_TempPath") + "\\";
                dirs = from dir in Directory.EnumerateDirectories(filePath, "*") select dir;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = dirs.Count();
                string[] del = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                foreach (string s in dirs)
                {
                    if (s.IndexOf("0_old") <= 0 && s.IndexOf("0_repacked") <= 0 && s.IndexOf("0_duplicate") <= 0 && s.IndexOf("0_dlcpacks") <= 0 && s.IndexOf("0_broken") <= 0 && s.IndexOf("0_albumCovers") <= 0 && s.IndexOf("0_log") <= 0 && s.IndexOf("0_archive") <= 0 && s.IndexOf("0_data") <= 0 && s.IndexOf("0_temp") <= 0 && s.IndexOf("0_to_import") <= 0)
                    {
                        //var cmd = "SELECT * FROM Main WHERE Folder_Name=\"" + s + "\" ORDER BY ID DESC;";
                        //DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, "", cnb, cnc);
                        //var noOfFlds = 0;
                        //if (drs.Tables.Count > 0) noOfFlds = drs.Tables[0].Rows.Count;
                        // oldvl = c("dlcm_AdditionalManipul81");
                        //ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = "Yes";
                        //if (noOfFlds == 0)
                        //{
                        NoOrphan++;
                        del[NoOrphan] = s;
                        timestamp = UpdateLog(timestamp, "\n\tTo Delete rogue folder:" + s, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //}
                        //ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
                    }
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing 0 folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = NoOrphan;
                if (NoOrphan > 0)
                    if (!ShowDialogue(dlg, " Check ", NoOrphan, "delete (more info in logs)", 2))
                        for (var j = 0; j < NoExtra; j++)
                        {
                            pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "To Delete existing 0 folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            DeleteDirectory(del[j], false);
                            timestamp = UpdateLog(timestamp, "\n\tDeleted rogue folder:" + del[j], true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
            }

            //4.Check existing 0_old files
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check orphan songs in _old", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check for orphan songs in _old?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                filePath = c("dlcm_TempPath") + "\\0_old"; dirs = from dir in Directory.EnumerateFiles(filePath, "*") select dir;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = dirs.Count();
                string[] cpys = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                string[] cpyd = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                foreach (string s in dirs)
                {
                    var cmd = "SELECT ID FROM Main WHERE Original_FileName=\"" + s.Replace(filePath + "\\", "") + "\"";/* ORDER BY ID DESC;*/
                    DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, "", cnb, cnc);
                    var noOfFlds = GetNoRec(drs, cnb, cnc);//0;
                                                           //if (drs.Tables.Count > 0) noOfFlds = drs.Tables[0].Rows.Count;
                    if (noOfFlds == 0)
                    {
                        NoExtra++;
                        cpys[NoExtra] = s;
                        cpyd[NoExtra] = c("dlcm_TempPath") + "\\0_to_import" + "\\" + Path.GetFileName(s);
                        timestamp = UpdateLog(timestamp, "\n\tTo move to import folder orphaned songs:" + s, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing 0_old for Extrafiles folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = NoExtra;
                if (NoExtra > 0)
                    if (!ShowDialogue(dlg, " check ", NoExtra, "copy safely (more info in logs)", 2))
                        for (var j = 0; j < NoExtra; j++)
                        {
                            pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "To Copy existing 0_old for Extrafiles folders", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            CopyMoveFileSafely(cpys[j], cpyd[j], false, GetHash(cpys[j]), false);/*filePath + "\\" + filePath + "\\" + GetHash(s)*/
                            timestamp = UpdateLog(timestamp, "\n\tCopied: " + cpys[j] + " to" + cpyd[j], true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
            }
            ////Clean dead CDLC_ID
            //o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check Groups for orhpan CDLC songs or groups", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //var tcmdz = "SELECT * FROM Groups LEFT JOIN Main ON Groups.CDLC_ID = Main.ID WHERE(((Main.ID)Is Null));";
            //DataSet dwz = new DataSet(); dwz = SelectFromDB("Arrangements", tcmdz, "", cnb, cnc);
            //var noOfArrw = dwz.Tables.Count <= 0 ? 0 : dwz.Tables[0].Rows.Count;
            //tcmdz = "DELETE * FROM (" + tcmdz + "";
            //if (noOfArrw > 0)
            //{
            //    DataSet dmz = new DataSet(); dmz = UpdateDB("Groups", tcmdz + ");", cnb, cnc); NoOGrp = noOfArrw;/*.Replace("SELECT * FROM ", "")*/
            //    timestamp = UpdateLog(timestamp, "\n\tTo remove: " + NoOGrp + " orphaned Groups entries", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //}

            //5 Clean Groups duplicate CDLC_ID
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check duplicate DLC songs in Groups (belonging to the same GRP)", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check duplicate DLC songs in Groups (belonging to the same GRP)?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT * FROM Groups WHERE Type=\"DLC\"", "", cnb, cnc);
                var noOfRech = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
                var IDgg = ""; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRech;
                if (noOfRech > 0)
                {
                    for (var l = 0; l < noOfRech; l++)
                    {
                        for (var v = l + 1; v < noOfRech; v++)
                            if (dgs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dgs.Tables[0].Rows[v].ItemArray[1].ToString().ToLower()
                                && dgs.Tables[0].Rows[l].ItemArray[2].ToString().ToLower() == dgs.Tables[0].Rows[v].ItemArray[2].ToString().ToLower())
                            {
                                IDgg += dgs.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";
                                noOfDLCGrpDpl++;
                                timestamp = UpdateLog(timestamp, "\n\tTo delete duplicate DLC songs in Groups:" + IDgg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                break;
                            }
                        pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check existing DLC in Group table", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }

                    if (!ShowDialogue(dlg, " check ", noOfDLCGrpDpl, "delete (more info in logs)", 2))
                    {
                        ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "To DELETE duplicate DLC in Group table", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var tcmdz = "DELETE * FROM Groups WHERE ID IN (" + IDgg + "";
                        DataSet dmz = new DataSet(); dmz = UpdateDB("Groups", tcmdz + "0);", cnb, cnc); /*.Replace("SELECT * FROM ", "")*/
                        timestamp = UpdateLog(timestamp, "\n\tRemoved: " + NoOGrp + " duplicate Groups entries:" + tcmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    }
                }
            }

            //6. check Spotify Album art file in Standardisation
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check Spotify Album art file in Standardisation", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Spotify Album art files, in Standardisation DB?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                var cmdS = "SELECT ID,SpotifyAlbumPath FROM Standardization WHERE SpotifyAlbumPath=\"\" OR SpotifyAlbumPath=Null; ";
                DataSet dns = new DataSet(); dns = SelectFromDB("Standardization", cmdS, "", cnb, cnc);
                var noOfArrS = 0;
                noOfArrS = GetNoRec(dns, cnb, cnc);//dns.Tables.Count > 0 ? dns.Tables[0].Rows.Count : 0;
                var IDs = "0"; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfArrS;
                string[] upd = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                for (var k = 0; k < noOfArrS; k++)
                {
                    try
                    {
                        var FN = dns.Tables[0].Rows[k].ItemArray[1].ToString();
                        var ID = dns.Tables[0].Rows[k].ItemArray[0].ToString();
                        if (!File.Exists(FN))
                        {
                            IDs += "," + ID + "";
                            timestamp = UpdateLog(timestamp, "\n\tTo remove: " + FN, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            NoOrphanSpotifyArt++;
                        }
                        pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "check Spotify Album art file in Standardisation", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }

                if (noOfArrS != 0)
                    if (!ShowDialogue(dlg, " check ", noOfArrS, "delete (more info in logs)", 2))
                    {
                        ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "To Remove Spotify Album art file path in Standardisation", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var cmdupdS = "UPDATE Standardization Set SpotifyAlbumPath =\"\" WHERE ID IN (" + IDs + ")";
                        DataSet dfs = new DataSet(); dfs = UpdateDB("Standardization", cmdupdS + ";", cnb, cnc);
                        timestamp = UpdateLog(timestamp, "\n\tRemoved (" + noOfArrS + "): " + IDs, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                ConfigRepository.Instance()["dlcm_AdditionalManipul81"] = oldvl;//restore setting for recycle bin
            }

            //7.check Groups for orphan entries
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check Groups for deleted songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var cmdz = "";
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Groups for deleted songs?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                ////cmdz = "SELECT * FROM Groups WHERE ID IN (SELECT F.ID FROM Groups as F left JOIN (SELECT ID FROM Main) as G ON G.ID=val(F.CDLC_ID) WHERE F. and G.ID is Null)";
                cmdz = "SELECT Groups.ID, Main.ID FROM Groups LEFT JOIN Main ON Main.ID = val(IIf(IsNull(Groups.CDLC_ID),0,Groups.CDLC_ID)) WHERE Main.ID Is Null AND Groups.Type=\"DLC\"; ";
                DataSet dnz = new DataSet(); dnz = SelectFromDB("Groups", cmdz, "", cnb, cnc);
                var noOfArrZ = GetNoRec(dnz, cnb, cnc);//dnz.Tables.Count <= 0 ? 0 : dnz.Tables[0].Rows.Count;
                timestamp = UpdateLog(timestamp, "\n\tGroups to delete songs: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                cmdz = "DELETE * FROM (" + cmdz + "";
                ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Remove " + noOfArrZ + " orphaned Groups DLC entries", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (noOfArrZ > 0)
                    if (!ShowDialogue(dlg, " check ", noOfArrZ, "delete (more info in logs)", 2))
                    {
                        dnz = UpdateDB("Groups", cmdz + ");", cnb, cnc); NoOGroup = noOfArrZ;/*.Replace("SELECT * FROM ", "")*/
                        timestamp = UpdateLog(timestamp, "\n\tRemoved: " + noOfArrZ + " orphaned Groups DLC entries: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }

            //8.Delete orphan Arrangements
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check Arraangements for orhpan entries songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Arrangements for orhpan entries songs?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                cmdz = "SELECT * FROM Arrangements LEFT JOIN Main ON Arrangements.CDLC_ID = Main.ID WHERE(((Main.ID)Is Null));";
                DataSet djz = new DataSet(); djz = SelectFromDB("Arrangements", cmdz, "", cnb, cnc);
                var noOfArrj = GetNoRec(djz, cnb, cnc);//djz.Tables.Count <= 0 ? 0 : djz.Tables[0].Rows.Count;
                timestamp = UpdateLog(timestamp, "\n\tArrangements to delete: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                cmdz = "DELETE * FROM (" + cmdz + "";
                ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Remove " + noOfArrj + " Arraangements for orhpan entries songs", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (noOfArrj > 0)
                    if (!ShowDialogue(dlg, " check ", noOfArrj, "delete (more info in logs)", 2))
                    {
                        djz = UpdateDB("Arrangements", cmdz + ");", cnb, cnc); NoOArg = noOfArrj;/*.Replace("SELECT * FROM ", "")*/
                        timestamp = UpdateLog(timestamp, "\n\tRemoved: " + noOfArrj + " orphaned Arrangements entries: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }

            //9.Delete orphan Tones
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check Tones for orhpan entries songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Tones for orhpan entries songs?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                cmdz = "SELECT * FROM Tones LEFT JOIN Main ON Tones.CDLC_ID = Main.ID WHERE(((Main.ID)Is Null));";
                DataSet dlz = new DataSet(); dlz = SelectFromDB("Tones", cmdz, "", cnb, cnc);
                var noOfArrl = GetNoRec(dlz, cnb, cnc);//dlz.Tables.Count <= 0 ? 0 : dlz.Tables[0].Rows.Count;
                timestamp = UpdateLog(timestamp, "\n\tTone to delete: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                cmdz = "DELETE * FROM (" + cmdz + "";
                ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Remove " + noOfArrl + " Tones for orhpan entries songs", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (noOfArrl > 0)
                    if (!ShowDialogue(dlg, " check ", noOfArrl, "delete (more info in logs)", 2))
                    {
                        dlz = UpdateDB("Tones", cmdz + ");", cnb, cnc); NoOTones = noOfArrl;/*.Replace("SELECT * FROM ", "")*/
                        timestamp = UpdateLog(timestamp, "\n\tRemoved: " + noOfArrl + " orphaned Tones entries: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }

            //10.Delete orphan tones details
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + "4 check Tones_GearList details for orhpan entries songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Tones_GearList for orhpan entries songs?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                cmdz = "SELECT * FROM Tones_GearList LEFT JOIN Tones ON Tones_GearList.Tone_ID = Tones.ID WHERE Tones.ID Is Null";
                DataSet dvz = new DataSet(); dvz = SelectFromDB("Tones_GearList", cmdz, "", cnb, cnc);
                var noOfArrv = GetNoRec(dvz, cnb, cnc);//dvz.Tables.Count <= 0 ? 0 : dvz.Tables[0].Rows.Count;
                timestamp = UpdateLog(timestamp, "\n\tOrphaned tones_details to delete: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                cmdz = "DELETE * FROM (" + cmdz + "";
                ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Remove " + noOfArrv + " ones_GearList for orhpan entries songs", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (noOfArrv > 0)
                    if (!ShowDialogue(dlg, " check ", noOfArrv, "delete (more info in logs)", 2))
                    {
                        dvz = UpdateDB("Tones_GearList", cmdz + ");", cnb, cnc); NoOArg = noOfArrv;/*.Replace("SELECT * FROM ", "") */
                        timestamp = UpdateLog(timestamp, "\n\tRemoved: " + noOfArrv + " orphaned Tones details entries from Tones_GearList: " + cmdz, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
            }

            //11.Fill in missing meta (save of pre import song details) data on Import 
            o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + "4 check Tones_GearList details for orhpan entries songs", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check Import_AuditTrail for missing Old/At import metadata?";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                cmdz = "SELECT CDLC_ID,ID FROM Import_AuditTrail WHERE val(CDLC_ID) in (SELECT ID FROM Main) and (Artist='' OR Artist_Sort='' OR Song_Title='' OR Song_Title_Sort='' OR" +
                    " Album='' OR Album_Sort='' OR Album_Year='' OR AlbumArtPath='' OR DLC_Name='' OR DLC_AppID=''" +
                    //" OR PreviewLenght='' OR AudioBitrate='' OR AudioSampleRate=''" +
                    ")";
                //" \" +\r\n            //    \"PreviewLenght, AudioBitrate, AudioSampleRate FROM Import_AuditTrail";
                DataSet dvz = new DataSet(); dvz = SelectFromDB("Import_AuditTrail", cmdz, "", cnb, cnc);
                NoMetaMissing = GetNoRec(dvz, cnb, cnc);//dvz.Tables.Count <= 0 ? 0 : dvz.Tables[0].Rows.Count;
                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = NoMetaMissing;
                //            NoMetaMissing = dgz.Tables.Count <= 0 ? 0 : dgz.Tables[0].Rows.Count;
                //NoMetaFilled + "/" + NoMetaMissing 
                cmdz = "DELETE * FROM (" + cmdz + "";
                if (NoMetaMissing > 0)
                {
                    string[] upd = new string[c("dlcm_maxsongsinDLCM").ToInt32()];
                    for (int j = 0; j < NoMetaMissing; j++)/*chbx_Additional_Manipulations.Items.Count*/
                    {
                        cmdz = "SELECT Original_FileName,Folder_Name FROM Main WHERE ID =" + dvz.Tables[0].Rows[j][0].ToString() + "";
                        //" \" +\r\n            //    \"PreviewLenght, AudioBitrate, AudioSampleRate FROM Import_AuditTrail";
                        DataSet dgz = new DataSet(); dgz = SelectFromDB("Import_AuditTrail", cmdz, "", cnb, cnc);
                        var t = dgz.Tables[0].Rows[0][0].ToString();
                        var tt = c("dlcm_TempPath") + "\\..\\" + t;
                        var platformz = tt.GetPlatform();

                        var unpackedDir = dgz.Tables[0].Rows[0][1].ToString();
                        DLCPackageData info = null;
                        try
                        {
                            info = DLCPackageData.LoadFromFolder(unpackedDir, platformz);
                            //if (File.Exists(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            //using (var vorbis = new NVorbis.VorbisReader(info.OggPath.Replace(".wem", "_fixed.ogg")))
                            //{
                            //    bitrate = vorbis.NominalBitrate;
                            //    bitrateorig = bitrate;
                            //    SampleRate = vorbis.SampleRate;
                            //    SampleRateorig = SampleRate;
                            //    duration = vorbis.TotalTime.ToString();
                            //    if (vorbis.TotalTime.TotalSeconds > 780) IsFullAlbum = "Yes";
                            //}

                            upd[j] = "UPDATE Import_AuditTrail SET Artist='" + info.SongInfo.Artist + "', Artist_Sort='" + info.SongInfo.ArtistSort + "', " +
                               "Song_Title='" + info.SongInfo.SongDisplayName + "' OR Song_Title_Sort='" + info.SongInfo.SongDisplayNameSort + "'," +
                               " Album='" + info.SongInfo.Album + "', Album_Sort='" + info.SongInfo.AlbumSort + "'," +
                               " Album_Year='" + info.SongInfo.SongYear + "', AlbumArtPath='" + info.AlbumArtPath + "'," +
                               " DLC_Name='" + info.Name + "', DLC_AppID='" + info.AppId + "'" +
                               //", PreviewLenght='', AudioBitrate='', AudioSampleRate='" +
                               " WHERE ID=" + dvz.Tables[0].Rows[j][1].ToString();
                            NoMetaMissing++;
                            pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "check empty Meta", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        catch (Exception ee)
                        {
                            var logmss = "Error at song Read/Load ..." + ee; timestamp = UpdateLog(timestamp, logmss, false, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                    }
                    pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = NoMetaMissing;
                    if (NoMetaMissing > 0)
                        if (!ShowDialogue(dlg, " check ", NoMetaMissing, "update", 2))
                            for (int j = 0; j < NoMetaMissing; j++)/*chbx_Additional_Manipulations.Items.Count*/
                            {
                                pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "apply empty Meta", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                DataSet dfu = new DataSet(); dfu = UpdateDB("Import_AuditTrail", upd[j] + ";", cnb, cnc);
                                //dvz = UpdateDB("Import_AuditTrail", cmdz + ");", cnb, cnc); NoOArg = NoMetaMissing;/*.Replace("SELECT * FROM ", "") */
                                timestamp = UpdateLog(timestamp, "\n\tUpdated: " + NoMetaMissing + "-" + upd[j] + " empty metadata of Import_AuditTrail", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                }
            }

            //12.1.check for any missing files issues
            FixAudiofileInconsist(0, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd);

            //12.2.check for any missing files issues
            var cnt = 0; var MissingPSARC = false; o++; pB_ReadDLCs.Maximum = max; pB_ReadDLCs.Value = o; timestamp = UpdateLog(timestamp, o + "/" + max + " check for Missing Files issues", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            dlg = pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " Do you want to check for Missing Files issues? ";
            if (ShowDialogue(dlg, "", -1, "", 2))
            {
                //cleanup before checks
                var cmdupd = "UPDATE Main Set FilesMissingIssues =\"\" WHERE"
                    + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace(";", "").Replace(SearchFields, "ID") + ")";
                DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);

                DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main WHERE"
                        + " ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")", "", cnb, cnc);
                var noOfRec = GetNoRec(dms, cnb, cnc);//dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
                var vFilesMissingIssues = "";
                //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRec;

                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRec;
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
                    DataSet dws = new DataSet(); dws = SelectFromDB("Pack_AuditTrail", cmd, "", cnb, cnc);

                    var noOfArrs = GetNoRec(dws, cnb, cnc);//0;
                                                           //if (dws.Tables.Count > 0) noOfArrs = dws.Tables[0].Rows.Count;
                    if (noOfArrs == 0)
                    {
                        NoPAT++;
                        //vFilesMissingIssues += " PackAuditTrail missing ";
                        var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Official, Reason, Pack";
                        var insertA = "Select top 1 i.Import_Path+\"\\\"+Original_FileName" + ", \"" + c("dlcm_TempPath") + "\\0_old" + "\", i.Original_FileName, i.File_Creation_Date, i.File_Hash, i.File_Size," +
                            " ID, DLC_Name, Platform, Is_Original, \"Added by consistency checks\", i.Pack FROM Main as i WHERE i.File_Hash = \"" + FileH + "\"";
                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0, cnc);
                    }

                    //check if File exists in Import Audit Trail
                    cmd = "SELECT FullPath,ID FROM Import_AuditTrail WHERE FileHash=\"" + FileH + "\" ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                    DataSet dqs = new DataSet(); dqs = SelectFromDB("Pack_AuditTrail", cmd, "", cnb, cnc);

                    var noOfArrss = GetNoRec(dqs, cnb, cnc);//0;
                                                            //if (dqs.Tables.Count > 0) noOfArrss = dqs.Tables[0].Rows.Count;
                    if (noOfArrss == 0)
                    {
                        vFilesMissingIssues += " Import_AuditTrail missing ";
                        string insertcmdA = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack";
                        var insertA = "Select i.Import_Path+\"\\\"+Original_FileName" + ", \"" + c("dlcm_RocksmithDLCPath") + "\", i.Original_FileName, i.File_Creation_Date, i.File_Hash, i.File_Size, i.Import_Date, i.Pack FROM Main as i WHERE " +
                            " i.File_Hash = \"" + FileH + "\"";
                        InsertIntoDBwValues("Import_AuditTrail", insertcmdA, insertA, cnb, 0, cnc);
                    }

                    //check Arrangements
                    cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + GetArrOfficSQLTxt(arrangoff);
                    DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", cmd, "", cnb, cnc);

                    var noOfArr = GetNoRec(dss, cnb, cnc);//0;
                                                          //noOfArr = dss.Tables[0].Rows.Count;
                    if (noOfArr == 0)
                    { vFilesMissingIssues = "No Arrangements!!!"; NoArg++; }
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

                            if (!File.Exists(ms1) && (ms3.LastIndexOf("showlights") < 1)) { vFilesMissingIssues += "Missing JSON " + ms1 + "; "; NoJSON++; }//showlights
                            if (!File.Exists(ms2)) { vFilesMissingIssues += "Missing XML " + ms3 + "; "; NoXML++; }
                            if ((ms3 != "" && dss.Tables[0].Rows[k].ItemArray[29].ToString() != "") && !File.Exists(ms4) && (ms3.LastIndexOf("showlights") < 1)) { vFilesMissingIssues += "Missing SNG " + ms4 + "; "; NoSNG++; }

                            if (File.Exists(ms1)) { long ms1l = new System.IO.FileInfo(ms1).Length; if (ms1l == 0 && (ms3.LastIndexOf("showlights") < 1)) { vFilesMissingIssues += "FileSize Zero JSON " + ms1 + "; "; NoJSON++; } }//showlights
                            if (File.Exists(ms2)) { long ms2l = new System.IO.FileInfo(ms2).Length; if (ms2l == 0) { vFilesMissingIssues += "FileSize Zero XML " + ms3 + "; "; NoXML++; } }
                            if (File.Exists(ms4)) { long ms4l = new System.IO.FileInfo(ms4).Length; if (ms4l == 0 && ms3.LastIndexOf("showlights") < 1) { vFilesMissingIssues += "FileSize Zero SNG " + ms4 + "; "; NoSNG++; } }
                            //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done   
                            //pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check arrangements", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        catch (Exception ex) { var tsst = "Error at file existence ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                    }

                    //check DB Duplicates and oldies 
                    cmd = "SELECT PackPath+'\\'+FileName,ID FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                    DataSet drs = new DataSet(); drs = SelectFromDB("Pack_AuditTrail", cmd, "", cnb, cnc);

                    noOfArr = GetNoRec(drs, cnb, cnc);//0;
                                                      //if (drs.Tables.Count > 0) noOfArr = drs.Tables[0].Rows.Count;
                                                      //else Console.Write("");

                    //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfArr;
                    for (var k = 0; k < noOfArr; k++)
                    {
                        try
                        {
                            var ms1 = drs.Tables[0].Rows[k].ItemArray[0].ToString();
                            if (!FileExistsAndIsNotZeroKB(ms1))
                            {
                                //vFilesMissingIssues += " oldie " + ms1 + "; ";
                                DataSet dnr = new DataSet(); dnr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set Reason = 'Missing PSARC' WHERE ID=" + drs.Tables[0].Rows[k].ItemArray[1].ToString() + ";", cnb, cnc);
                                NoPackTrail++;
                                MissingPSARC = true;
                            }
                            //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done 

                        }
                        catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                    }
                    if (vFilesMissingIssues.ToLower().IndexOf("sunshine") >= 0)
                        Console.Write("");
                    //check the files Duplicates and oldies 
                    //pB_ReadDLCs.Increment(1);
                    var old = c("dlcm_TempPath") + "\\0_old\\" + OrigFileName;
                    if (!FileExistsAndIsNotZeroKB(AlbumArtPath) && hasCov == "Yes") { vFilesMissingIssues += "Missing AlbumArtPath; "; NoAlbumArt++; }
                    if (!FileExistsAndIsNotZeroKB(AudioPath)) { vFilesMissingIssues += "Missing AudioPath; "; NoAudio++; }
                    if (!FileExistsAndIsNotZeroKB(AudioPreviewPath) && hasPrev == "Yes") { vFilesMissingIssues += "Missing AudioPreviewPath; "; NoAudioPreview++; }
                    if (!FileExistsAndIsNotZeroKB(OggPath)) { vFilesMissingIssues += "Missing OggPath; "; NoOgg++; }
                    if (!FileExistsAndIsNotZeroKB(OggPreviewPath) && hasPrev == "Yes") { vFilesMissingIssues += "Missing OggPreviewPath; "; NoOggPreview++; }
                    if (!FileExistsAndIsNotZeroKB(old) && hasOld == "Yes") { vFilesMissingIssues += "Missing old; "; NoOld++; }

                    if (FileExistsAndIsNotZeroKB(AlbumArtPath)) { long AlbumArtPathl = new System.IO.FileInfo(AlbumArtPath).Length; if (AlbumArtPathl == 0 && hasCov == "Yes") { vFilesMissingIssues += "FileSize Zero AlbumArt; "; NoAlbumArt++; } }
                    if (FileExistsAndIsNotZeroKB(AudioPath)) { long AudioPathl = new System.IO.FileInfo(AudioPath).Length; if (AudioPathl == 0) { vFilesMissingIssues += "FileSize Zero Audio; "; NoAudio++; } }
                    if (FileExistsAndIsNotZeroKB(AudioPreviewPath)) { long AudioPreviewPathl = new System.IO.FileInfo(AudioPreviewPath).Length; if (AudioPreviewPathl == 0 && hasPrev == "Yes") { vFilesMissingIssues += "FileSize Zero AudioPreview; "; NoAudioPreview++; } }
                    if (FileExistsAndIsNotZeroKB(OggPath)) { long OggPathl = new System.IO.FileInfo(OggPath).Length; if (OggPathl == 0) { vFilesMissingIssues += "FileSize Zero Ogg; "; NoOgg++; } }
                    if (FileExistsAndIsNotZeroKB(OggPreviewPath)) { long OggPreviewPathl = new System.IO.FileInfo(OggPreviewPath).Length; if (OggPreviewPathl == 0 && hasPrev == "Yes") { vFilesMissingIssues += "FileSize Zero OggPreview; "; NoOggPreview++; } }
                    if (FileExistsAndIsNotZeroKB(old)) { long oldl = new System.IO.FileInfo(old).Length; if (oldl == 0 && hasOld == "Yes") { vFilesMissingIssues += "FileSize Zero old; "; NoOld++; } }

                    DataSet dxr = new DataSet(); if (vFilesMissingIssues != "")
                    {
                        dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"Yes\", FilesMissingIssues = \"" + vFilesMissingIssues + "\" WHERE ID=" + ID + "", cnb, cnc);
                        cnt++;
                    }
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "Check for missing files", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            }

            if (cnt > 0)
            {
                SearchCmd = SearchCmd.Replace("ORDER BY", "WHERE FilesMissingIssues <> \"\" ORDER BY");
                if (MissingPSARC) MessageBox.Show("Please note that Pack_AuditTrail has unmatched records. Manaully decide if they have to be deleted based on Reason field.");
                if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ";

                cmb_Filter.Text = "Show Songs with FilesMissing Issues";/* (vFilesMissingIssues.IndexOf(";") > 0 && vFilesMissingIssues.Length > 1 ? "s" : "")*/
                MessageBox.Show("Song" + cnt + " with FilesMissing Issues identified"
                    + "\n\nChecking Details:"
                    + "\n\n1. No Rencodes (Option 83): " + NoRencodes
                    + "\n\n2. No. Folders in 0_data wo entry MainDB in (check Recycle Bin): " + NoOrphanD
                    + "\n\n3. No. Folders in 0 temporarely left there after (failed?) import: " + NoOrphan
                    + "\n\n4. No. Standarization Sportify Album Covers with no File: " + NoOrphanSpotifyArt
                    + "\n\n5. No. Songs in Groups with no Song in MainDB: " + NoRencodes
                    + "\n\n6. No. Checked/Faulty Packed (audit trail)links (Delete Manually): " + NoPackTrail
                    + "\n\n7. Checked/Faulty Missing Original Files:" + NoOld
                    + "\n\n8. No. Extra Old/Original files (Check your Import folder):" + NoExtra
                    //+ "\n\nNo Unattached (Check your Archive folder and reimport with all as duplicates flag Option xx):" + 
                    + "\n\n9. Checked/Faulty Missing Missing audio WEM/Game Files: " + NoAudio + "/" + NoAudioPreview
                    + "\n\n10. Checked/Faulty Missing Missing audio OGG/DLCM Preview Files: " + NoOgg + "/" + NoOggPreview
                    + "\n\n11. Checked/Faulty Missing Cover Files: " + NoAlbumArt
                    + "\n\n12. No JSON/XML/SNG: " + NoJSON + "/" + NoXML + "/" + NoSNG
                    + "\n\n13. No Arrangements: " + NoArg
                    + "\n\n14. No. orphan Group entries Deleted: " + NoOGroup
                    + "\n\n15. No. orphan Arrangements Deleted: " + NoOArg
                    + "\n\n16. No. orphan Tones entries Deleted: " + NoOTones
                    //+ "\n\n17. No. orphan Groups DLC Deleted: " + NoOGrp
                    + "\n\n17. No Duplicate Group DLC: " + noOfDLCGrpDpl
                    + "\n\n17. No. orphan ToneGL Deleted: " + NoOTGL
                    + "\n\n17. No PackedAuditrailed (fixed): " + NoPAT
                    + "\n\n18. No Meta missing from ImportAuditrail (/fixed): " + NoMetaFilled + "/" + NoMetaMissing
                    //+ "\n\nExtra Original Files:"
                    );
            }
            else
                MessageBox.Show("No Songs with FilesMissing Issues identified");
        }

        static bool FileExistsAndIsNotZeroKB(string f)
        {
            if (File.Exists(f) && new System.IO.FileInfo(f).Length > 0) return true;
            return false;
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

            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\";", cnb, cnc);
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
            if (a.IndexOf("[") > 0 && a.IndexOf("]") > 0)
            {
                var b = a.Substring(a.IndexOf("["), a.IndexOf("]") - a.IndexOf("[") + 1);
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
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb, cnc);
                var rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString(), false);
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb, cnc);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb, cnc);
            MessageBox.Show("All Duplicates songs have been deleted");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath+\"\\\"+FileName = \"" + cmb_Packed.Text + "\"";
            DeleteFile(cmb_Packed.Text, false);
            cmb_Packed.Items.RemoveAt(cmb_Packed.SelectedIndex);
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb, cnc);
        }

        private void cmb_Packed_SelectedValueChanged(object sender, EventArgs e)
        {
            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT Official FROM Pack_AuditTrail WHERE PackPath +\"\\\"+FileName = \"" + cmb_Packed.Text + "\"", "", cnb, cnc);
            var rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables[0].Rows.Count;
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

            //var trackno = 0;
            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            var sel = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"0\" OR Track_No = \"00\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRec = GetNoRec(SongRecord, cnb, cnc);//.Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

            if (netstatus == "OK" || noOfRec > 0) txt_Track_No.Value = FixWithSpotifyDetails(SongRecord, 0, noOfRec, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp, cnc);
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
            dhs = SelectFromDB("Main", cmd, "", cnb, cnc);
            noOfRec = GetNoRec(dhs, cnb, cnc);//dhs.Tables[0].Rows.Count;
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
            //btn_FixAudioAll.Text = "Fix Audio Issues";

            FixAudioAll_Click(netstatus, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, SearchCmd, SearchFields, logPath, timestamp, cnc);

            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
            pB_ReadDLCs.CreateGraphics().DrawString("Done Fixing Audio Issues for this song.", new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            btn_FixAudioAll.Text = "Fix Audio Issues";
        }

        public static void FixAudioAll_Click(string netstatus, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs
                    //   , RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string SearchCmd, string SearchFields, string logPath, DateTime timestamp, SQLiteConnection cnz)
                    , RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string SearchCmd, string SearchFields, string logPath, DateTime timestamp, SQLite.SQLiteConnection cnc)
        {

            var cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main " +
                "WHERE FilesMissingIssues is null AND (Has_Preview=\"No\" OR oggPreviewPath=\"\" OR audioPreviewPath=\"\") AND Is_Broken<>\"Yes\"" +
                "" + (c("dlcm_AdditionalManipul55").ToLower() != "yes" ? "" :
                " OR (VAL(PreviewLenght) > " + float.Parse(c("dlcm_MaxPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture) + ")" +
                (c("dlcm_AdditionalManipul88").ToLower() != "yes" ? "" :
                " OR (VAL(PreviewLenght) < " + float.Parse(c("dlcm_MinPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture)) + ")");
            FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB", cnc);

            cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, oggPath, oggPreviewPath FROM Main" +
                " WHERE FilesMissingIssues is null AND (VAL(audioBitrate) > "
                + (c("dlcm_MaxBitRate")) + " or VAL(audioSampleRate) > " + (c("dlcm_MaxSampleRate")) + ") AND "
                + "ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") AND Is_Broken<>\"Yes\"";
            FixAudioIssues(cmd.Replace("; ", ""), cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB", cnc);

            //Clean Temp folder of Audio files
            foreach (var fil in Directory.GetFiles(c("dlcm_TempPath") + "\\0_temp"))

            {
                if (fil.IndexOf(".wav") > 0 || fil.IndexOf(".ogg") > 0 || fil.IndexOf(".wem") > 0)
                {
                    DeleteFile(fil, false);
                }
            }

            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            if (netstatus == "OK")
            {
                var sels = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"00\" OR Track_No = \"0\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY ID ASC";
                DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sels, "", cnb, cnc);
                var noOfRecs = GetNoRec(SongRecord, cnb, cnc);//.Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

                pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;

                for (var i = 0; i < noOfRecs; i++)
                {
                    pB_ReadDLCs.Increment(1);
                    FixWithSpotifyDetails(SongRecord, i, noOfRecs, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp, cnc);
                }
                SongRecord.Dispose();
            }
            //fix missing youtube details
            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK" && c("dlcm_youtubestatus") == "OK")
            {
                var sel = "SELECT * FROM Main WHERE (YouTube_Link = \"\" OR Youtube_Playthrough = \"\" or YouTube_Link is null OR Youtube_Playthrough is null) "
               + "AND ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ") ORDER BY ID ASC";
                //Read from DB
                MainDBfields[] SongRecords = new MainDBfields[20000];
                SongRecords = GetRecord_s(sel, cnb, cnc);
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
                    if (netstatus == "OK" && c("dlcm_youtubestatus") == "OK") GetYoutubeDetailsAsync(SongRecords[j], j, cnb, pB_ReadDLCs, "MainDB", false, cnc);
                    j++;
                }
            }

        }

        public static int FixWithSpotifyDetails(DataSet SongRecord, int i, int noOfRec, OleDbConnection cnb, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, string windw, DateTime timestamp, SQLite.SQLiteConnection cnc)
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
                        dis = UpdateDB("Standardization", cmds + ";", cnb, cnc);

                    var cmdz = "UPDATE Main SET Has_Track_No=\"Yes\", Track_No=\"" + trackno.ToString("D2") + "\",Spotify_Song_ID=\"" + SpotifySongID + "\", Spotify_Artist_ID=\"" + SpotifyArtistID + "\"";
                    cmdz += ", Spotify_Album_ID=\"" + SpotifyAlbumID + "\"";
                    cmdz += " WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[5].ToString();
                    DataSet dos = new DataSet();
                    if (trackno > 0 && SpotifySongID != "" && SpotifySongID != "-")
                        dos = UpdateDB("Main", cmdz + ";", cnb, cnc);
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
                    DeleteFile(cmb_Packed.Items[i].ToString(), false);
                    cmb_Packed.Items.RemoveAt(i);
                    DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE PackPath+FileName=\"" + cmb_Packed.Items[i].ToString() + "\" AND PackPath unlike \"%old%\"; ", cnb, cnc);
                }
            MessageBox.Show("All repacked songs have been deleted");
        }

        private void btn_AssesIfDuplicate_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            if (chbx_AutoSave.Checked) SaveRecord();
            savesettings();

            //Use Multi-select if the case
            var sel = "SELECT * FROM Main WHERE ID IN (";
            int k = 0;
            for (k = 1; k < databox.SelectedRows.Count; k++)
            {
                if (k > 1) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }
            if (k <= 1) return;

            sel += ")";
            var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);
            var norows = SongRecord[0].NoRec.ToInt32();

            //var b = 0;
            var dupli_assesment = "";
            var IDD = "";
            var folder_name = "";
            var DLCC = "";
            var Platformm = "";
            var filename = "";
            //var HasOrig = "";
            var Duplic = 0;
            var maxDuplic = 0;
            //Calculate the dupli number
            DataSet dxff = new DataSet(); var cmd = "SELECT MAX(Duplicate_Of) FROM Main WHERE Duplicate_Of<>\"\" Group BY Duplicate_Of";
            dxff = SelectFromDB("Main", cmd, c("dlcm_DBFolder"), cnb, cnc);
            maxDuplic = dxff.Tables.Count == 1 ? (GetNoRec(dxff, cnb, cnc) > 0 ? (
                dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() > 0 ? dxff.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1 : 1) : 1) : 1;

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
            DataSet dhf = new DataSet(); cmd = "SELECT XMLFile_Hash, Json_Hash, ConversionDateTime, Has_Sections, CleanedXML_Hash, SNGFileHash FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + GetArrOfficSQLTxt(arrangoff);
            dhf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb, cnc);
            for (var h = 0; h < GetNoRec(dhf, cnb, cnc); h++)
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
            sel = sel.Replace("*", "MAX (Alternate_Version_No)");
            //Get last inserted ID
            DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, c("dlcm_DBFolder"), cnb, cnc);

            var altvert = GetNoRec(dds, cnb, cnc) > 0 ? (dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() == -1 ? 1 : dds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32()) : 1;
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
            var InTheWorks = databox.SelectedRows[0].Cells["IntheWorks"].Value.ToString();
            var IsAcoustic = databox.SelectedRows[0].Cells["Is_Acoustic"].Value.ToString();
            var IsRemastered = databox.SelectedRows[0].Cells["Is_Remastered"].Value.ToString();
            var IsUncensored = databox.SelectedRows[0].Cells["Is_Uncensored"].Value.ToString();
            var IsSingle = databox.SelectedRows[0].Cells["Is_Single"].Value.ToString();
            var IsSoundtrack = databox.SelectedRows[0].Cells["Is_Soundtrack"].Value.ToString();
            var IsFullAlbum = databox.SelectedRows[0].Cells["Is_FullAlbum"].Value.ToString();
            var IsInstrumental = databox.SelectedRows[0].Cells["Is_Instrumental"].Value.ToString();

            var IsKaraoke = databox.SelectedRows[0].Cells["Is_Karaoke"].Value.ToString();
            var IsDemo = databox.SelectedRows[0].Cells["Is_Demo"].Value.ToString();
            var HasFeaturing = databox.SelectedRows[0].Cells["Has_Featuring"].Value.ToString();
            var IsRemix = databox.SelectedRows[0].Cells["Is_Remix"].Value.ToString();
            var IsCover = databox.SelectedRows[0].Cells["Is_Cover"].Value.ToString();
            var IsEP = databox.SelectedRows[0].Cells["Is_EP"].Value.ToString();
            var IsMedley = databox.SelectedRows[0].Cells["Is_Medley"].Value.ToString();
            var IsMultiStrings = databox.SelectedRows[0].Cells["Is_MultiStrings"].Value.ToString();
            var HasCapo = databox.SelectedRows[0].Cells["Has_Capo"].Value.ToString();
            var IsDeluxe = databox.SelectedRows[0].Cells["Is_Deluxe"].Value.ToString();
            var IsGreatestHits = databox.SelectedRows[0].Cells["Is_GreatestHits"].Value.ToString();
            var IsMidi = databox.SelectedRows[0].Cells["Is_Midi"].Value.ToString();
            var IsGameSoundtrack = databox.SelectedRows[0].Cells["Is_GameSoundtrack"].Value.ToString();
            var IsTVTheme = databox.SelectedRows[0].Cells["Is_TVTheme"].Value.ToString();
            var IsAmateurCover = databox.SelectedRows[0].Cells["Is_AmateurCover"].Value.ToString();
            var IsMetalCover = databox.SelectedRows[0].Cells["Is_MetalCover"].Value.ToString();
            var IsUkulele = databox.SelectedRows[0].Cells["Is_Ukulele"].Value.ToString();
            var Is_Midi = databox.SelectedRows[0].Cells["Is_Midi"].Value.ToString();

            var unpackedDir = databox.SelectedRows[0].Cells["Folder_Name"].Value.ToString();
            var audio_hash = databox.SelectedRows[0].Cells["Audio_OrigHash"].Value.ToString();
            var File_Creation_Date = databox.SelectedRows[0].Cells["File_Creation_Date"].Value.ToString();
            var Tunings = databox.SelectedRows[0].Cells["Tunning"].Value.ToString();
            var A440TunningFrecv = databox.SelectedRows[0].Cells["A440TunningFrecv"].Value.ToString();
            var Alternate_No = databox.SelectedRows[0].Cells["Alternate_Version_No"].Value.ToString();
            var description = databox.SelectedRows[0].Cells["Description"].Value.ToString();
            var Duplicate_of = databox.SelectedRows[0].Cells["Duplicate_of"].Value.ToString();
            var AlbumArtPath = databox.SelectedRows[0].Cells["AlbumArtPath"].Value.ToString();
            var Is_Alternate = databox.SelectedRows[0].Cells["Is_Alternate"].Value.ToString();
            //var Rebuild = false;
            var action = ""; //var reas = "";
            if (norows >= 1)
                foreach (var file in SongRecord)
                {
                    j++;
                    if (file.ID == null) break;
                    Duplic = Duplic == 0 && maxDuplic == 1 ? file.Duplicate_Of.ToInt32() : maxDuplic;

                    folder_name = file.Folder_Name;
                    filename = databox.Rows[0].Cells["Original_FileName"].Value.ToString();// file.Current_FileName;
                    IDD = file.ID; //Save Id in case of update oDataGridViewr asses-update

                    var dupli_reason = "";
                    var dupli_assesment_reason = "";
                    timestamp = UpdateLog(timestamp, dupli_reason, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    bool platform_doesnt_matters = c("dlcm_AdditionalManipul76") == "Yes" ? (file.Platform == Platformm ? true : false) : true;
                    var tst = "";
                    frm_Duplicates_Management frm1 = new frm_Duplicates_Management(file, info, author, tkversion, DD, Bass, Guitar, Combo,
                        Rhythm, Lead, Vocalss, Tunings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, xmlhlist, jsonhlist,
                        c("dlcm_DBFolder"), clist, dlist, newold, Is_Original, altvert.ToString(),
                        c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39") == "Yes" ? true : false, c("dlcm_AdditionalManipul40") == "Yes" ? true : false,
                        fsz, unpackedDir, Is_MultiTrack == "No" ? "" : Is_MultiTrack, MultiTrack_Version, File_Creation_Date, j.ToString() + "" + "", Platformm, IsLive == "No" ? "" : IsLive, LiveDetails, IsAcoustic, cnb, dupli_reason, databox.SelectedRows[0].Cells["Song_Lenght"].Value.ToString(), "-",
                        databox.SelectedRows[0].Cells["ID"].Value.ToString().ToInt32(), cxmlhlist, snghlist, databox.Rows[0].Cells["File_Hash"].Value.ToString(),
                        IsInstrumental == "No" ? "" : IsInstrumental, IsSoundtrack == "No" ? "" : IsSoundtrack, IsFullAlbum == "No" ? "" : IsFullAlbum
                        , IsSingle == "No" ? "" : IsSingle, IsEP == "No" ? "" : IsEP, IsUncensored == "No" ? "" : IsUncensored, IsRemastered == "No" ? "" : IsRemastered, InTheWorks == "No" ? "" : InTheWorks
                        , IsKaraoke == "No" ? "" : IsKaraoke, IsDemo == "No" ? "" : IsDemo, HasFeaturing == "No" ? "" : HasFeaturing, IsRemix == "No" ? "" : IsRemix, IsCover == "No" ? "" : IsCover
                        , IsMedley == "No" ? "" : IsMedley, IsMultiStrings == "No" ? "" : IsMultiStrings
                        , IsDeluxe == "No" ? "" : IsDeluxe, IsGreatestHits == "No" ? "" : IsGreatestHits
                        , IsMidi == "No" ? "" : IsMidi, IsGameSoundtrack == "No" ? "" : IsGameSoundtrack, IsTVTheme == "No" ? "" : IsTVTheme, IsAmateurCover == "No" ? "" : IsAmateurCover
                        , IsMetalCover == "No" ? "" : IsMetalCover, IsUkulele == "No" ? "" : IsUkulele, HasCapo == "No" ? "" : HasCapo, txt_Description.Text, A440TunningFrecv, cnc);
                    frm1.ShowDialog();
                    if (frm1.Author != author) if (frm1.Author == "Custom Song Creator" && c("dlcm_AdditionalManipul47") == "Yes") author = "";
                        else author = frm1.Author;
                    if (frm1.Description != description) description = frm1.Description;
                    //if (frm1.Comment != comment) comment = frm1.Comment;
                    if (frm1.Title != info.SongInfo.SongDisplayName)
                        if (c("dlcm_AdditionalManipul46") == "Yes") info.SongInfo.SongDisplayName = frm1.Title;
                    //if (frm1.Version != tkversion) PackageVersion = frm1.Version;
                    //if (frm1.DLCID != Namee) Namee = frm1.DLCID;
                    if (frm1.Is_Alternate != Is_Alternate) chbx_Alternate.Checked = frm1.Is_Alternate == "Yes" ? true : false;
                    if (frm1.Title_Sort != info.SongInfo.SongDisplayNameSort) txt_Title.Text = frm1.Title_Sort;
                    if (frm1.Artist != info.SongInfo.Artist) txt_Artist.Text = frm1.Artist;
                    if (frm1.ArtistSort != info.SongInfo.ArtistSort) txt_Artist_Sort.Text = frm1.ArtistSort;
                    if (frm1.Album != info.SongInfo.Album) txt_Album.Text = frm1.Album;
                    if (frm1.albumsort != info.SongInfo.AlbumSort) txt_AlbumSort.Text = frm1.albumsort;
                    if (frm1.Alternate_No != Alternate_No && !(frm1.Alternate_No is null)) txt_Alt_No.Value = int.Parse(frm1.Alternate_No);
                    if (frm1.AlbumArtPath != AlbumArtPath) txt_AlbumArtPath.Text = frm1.AlbumArtPath;
                    //if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
                    if (frm1.MultiT != "") chbx_MultiTrack.Checked = frm1.MultiT == "Yes" ? true : false;
                    ////////////if (frm1.MultiTV != "") MultiTrack_Version = frm1.MultiTV;

                    if (frm1.isLive != "") chbx_A_IsLive.Checked = frm1.isLive == "Yes" ? true : false;
                    if (frm1.liveDetails != "") txt_Live_Details.Text = frm1.liveDetails;

                    if (frm1.isAcoustic != "") chbx_A_IsAcoustic.Checked = frm1.isAcoustic == "Yes" ? true : false;
                    if (frm1.isEP != "") chbx_A_IsEP.Checked = frm1.isEP == "Yes" ? true : false;
                    if (frm1.isSingle != "") chbx_A_IsSingle.Checked = frm1.isSingle == "Yes" ? true : false;
                    if (frm1.isInstrumental != "") chbx_A_IsInstrumental.Checked = frm1.isInstrumental == "Yes" ? true : false;
                    if (frm1.isFullAlbum != "") chbx_A_IsFullAlbum.Checked = frm1.isFullAlbum == "Yes" ? true : false;
                    if (frm1.isSoundtrack != "") chbx_A_IsSoundtrack.Checked = frm1.isSoundtrack == "Yes" ? true : false;
                    if (frm1.isUncensored != "") chbx_A_IsUncensored.Checked = frm1.isUncensored == "Yes" ? true : false;
                    if (frm1.isRemastered != "") chbx_A_IsRemastered.Checked = frm1.isRemastered == "Yes" ? true : false;
                    if (frm1.inTheWorks != "") chbx_A_IsInTheWorks.Checked = frm1.inTheWorks == "Yes" ? true : false;

                    if (frm1.isCover != "") chbx_A_IsCover.Checked = frm1.isCover == "Yes" ? true : false;
                    if (frm1.isDemo != "") chbx_A_IsDemo.Checked = frm1.isDemo == "Yes" ? true : false;
                    if (frm1.isRemix != "") chbx_A_IsRemix.Checked = frm1.isRemix == "Yes" ? true : false;
                    if (frm1.isKaraoke != "") chbx_A_IsKaraoke.Checked = frm1.isKaraoke == "Yes" ? true : false;
                    if (frm1.hasFeaturing != "") chbx_A_HasFeaturing.Checked = frm1.hasFeaturing == "Yes" ? true : false;

                    if (frm1.isGreatestHits != "") chbx_A_IsGreatestHits.Checked = frm1.isGreatestHits == "Yes" ? true : false;
                    if (frm1.isDeluxe != "") chbx_A_IsDeluxe.Checked = frm1.isDeluxe == "Yes" ? true : false;
                    if (frm1.hasCapo != "") chbx_Capo.Checked = frm1.hasCapo == "Yes" ? true : false;
                    if (frm1.isMedley != "") chbx_A_IsMedley.Checked = frm1.isMedley == "Yes" ? true : false;
                    if (frm1.isMultiStrings != "") chbx_A_IsMultiStrings.Checked = frm1.isMultiStrings == "Yes" ? true : false;
                    if (frm1.isMidi != "") chbx_A_IsMidi.Checked = frm1.isMidi == "Yes" ? true : false;
                    if (frm1.isGameSoundtrack != "") chbx_A_IsMetalCover.Checked = frm1.isGameSoundtrack == "Yes" ? true : false;

                    //if (frm1.YouTube_Link != "") YouTube_Link = frm1.YouTube_Link;
                    //if (frm1.CustomsForge_Link != "") CustomsForge_Link = frm1.CustomsForge_Link;
                    //if (frm1.CustomsForge_Like != "") CustomsForge_Like = frm1.CustomsForge_Like;
                    //if (frm1.CustomsForge_ReleaseNotes != "") CustomsForge_ReleaseNotes = frm1.CustomsForge_ReleaseNotes;
                    if (frm1.dupliID != "") txt_DuplicateOf.Text = frm1.dupliID;
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
                dhd = SelectFromDB("Main", cmdd, c("dlcm_DBFolder"), cnb, cnc);

                cmdd = "DELETE FROM Main WHERE ID IN (" + action.Substring(0, action.Length - 1) + ")";

                //1. Delete DB records
                DeleteRecords(action.Substring(0, action.Length - 1), cmdd, c("dlcm_DBFolder"), c("dlcm_TempPath"),
                    (GetNoRec(dhd, cnb, cnc) > 0 ? GetNoRec(dhd, cnb, cnc) : 0).ToString(), "--", cnb, pB_ReadDLCs, cnc);
            }
            var a = SaveOK; SaveOK = false;
            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true; SaveOK = a;
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
            DataSet dct = SelectFromDB("Main", sel + ";", c("dlcm_DBFolder"), cnb, cnc);

            var cmd1 = "UPDATE Main SET Split4Pack = \"\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);

            var noOfRecs = GetNoRec(dct, cnb, cnc) > 0 ? (dct.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(dct.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            for (var j = 1; j <= noOfRecs; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                DeleteDirectory(dest, false);
            }
        }

        public void btn_AddPackSplit_Click(object sender, EventArgs e)
        {
            var old = ConfigRepository.Instance()["dlcm_Groups"];
            CleanPack();
            var sel = SearchCmd;
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRecs = GetNoRec(SongRecord, cnb, cnc); // .Tables[0].Rows.Count;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs;

            var t = sel.IndexOf("FROM Main");
            var h = sel.IndexOf(") WHERE") + 1;
            var c = sel.Length;
            var l = sel.Substring(t, c - t);
            var j = 0;

            if (chbx_Instances.Checked) DeleteMultiInstances();
            for (j = 1; j < txt_No4Splitting.Value + 1; j++)
            {
                sel = "SELECT top " + txt_NoOfSplits.Value + " ID FROM (SELECT ID,Split4Pack " + l.Replace(";", "") + ") WHERE Split4Pack= \"\"";
                var cmd2 = "UPDATE Main SET Split4Pack = \"" + j + "\" WHERE ID in (" + sel + ")";
                DataSet dbt = UpdateDB("Main", cmd2 + ";", cnb, cnc);
                if (chbx_Instances.Checked)
                {
                    ConfigRepository.Instance()["dlcm_Split4Pack"] = j.ToString();
                    ConfigRepository.Instance()["dlcm_Groups"] = "Split " + j.ToString();
                    //ConfigRepository.Instance()["dlcm_FilterGroup"] = "Split " + j.ToString();
                    savesettings();

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


                    var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                    StartProcesss(@xx, null);
                    //try
                    //{
                    //    Process process = Process.Start(@xx);
                    //}
                    //catch (Exception ex)
                    //{
                    //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ff, "", "", null, null);
                    //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                    //}
                }
            }


            //var cmd2 = "UPDATE Main SET Split4Pack = \"" + j + "\" WHERE ID in (" + "SELECT ID FROM(SELECT ID, Split4Pack " + l.Replace("; ", "") + ") WHERE Split4Pack = \"\""; + ")";/*sel.Replace(c("dlcm_SearchFields"),"ID")*/
            //DataSet dbt = UpdateDB("Main", cmd2 + ";", cnb, cnc);

            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
            Update_Selected();
            ConfigRepository.Instance()["dlcm_Groups"] = old;
            MessageBox.Show((j - 1).ToString() + " packs of songs have been created/marked.");
        }

        private void chbx_PS3HAN_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_PS3HAN.Checked) chbx_PS3Retail.Enabled = true;
            else chbx_PS3Retail.Enabled = false;
        }
        private void btn_SongOnFire_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
            eof(false, filePath);
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
                StartProcesss("explorer.exe", string.Format("/select,\"{0}\"", filePath));
                //try
                //{
                //    Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Can not open Old Folder in Explorer ! ");
                //}
            }

            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PathForBRM"), " ").Replace("\\ ", " ");//c("dlcm_TempPath")+"\\0_old\\"+txt_OldPath.Text
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Beats & Phrases Resynchronizer if you want to use it.", c("dlcm_PathForBRM_www"), "Missing Beats & Phrases Resynchronizer", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT Max(Split4Pack) FROM Main ", "", cnb, cnc);
            var nosplits = GetNoRec(ds, cnb, cnc) > 0 ? (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            if (nosplits > 0)
            {
                for (var j = 1; j < nosplits; j++)
                {
                    var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;

                    var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                    StartProcesss(@xx, null);
                    //try
                    //{
                    //    Process process = Process.Start(@xx);
                    //}
                    //catch (Exception ex)
                    //{
                    //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                    //}
                }
            }
        }
        int prevWidth;
        FormWindowState prevWindowState;

        private void btn_OpenRetail_Click(object sender, EventArgs e)
        {
            Cache frm = new Cache(c("dlcm_DBFolder"), c("dlcm_TempPath"), c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39").ToLower() == "yes" ? true : false, c("dlcm_AdditionalManipul40").ToLower() == "yes" ? true : false, cnb, cnc);
            frm.ShowDialog();
        }

        private void btn_ApplyArtistShortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Artist_Short = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet dus = UpdateDB("Main", cmd1 + ";", cnb, cnc);
            cmd1 = "UPDATE Main SET Artist_ShortName = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet difg = UpdateDB("Main", cmd1 + ";", cnb, cnc);
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
                if (DDC.StartInfo.FileName != "")
                {
                    DDC.Kill();//  if (ProcessStarted) { DProcessStarted = false; btn_PlayPreview.Text = "Play Audio"; return; }
                    DDC.Close();
                }
            }
            catch (Exception er)
            {
                var tsst = "Erro btn_PlayAudio_Click..." + er.Message.ToString(); timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggdec2.exe"),
                WorkingDirectory = AppWD
            };
            var t = txt_MultiTrackType.Text.Contains(" (Audio Available)") ? GetAlternativeAudioFile(txt_MultiTrackType.Text, txt_SongFolder.Text, txt_OggPath.Text) : txt_OggPath.Text;
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
            var prev = FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB", cnc);

            //Fix Bitrate
            cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath  FROM Main WHERE (VAL(audioBitrate) > " + (c("dlcm_MaxBitRate")) + " or VAL(audioSampleRate) > " + (c("dlcm_MaxSampleRate")) + ")";
            cmd += " AND ID=" + txt_ID.Text + "";
            FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cancel, "MainDB", cnc);

            //Get Spotify
            //fix missing spotify details
            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            var sel = "SELECT Song_Title, Song_Title_Sort, Album, Artist, Album_Year, ID FROM Main WHERE (Track_No = \"0\" OR Track_No = \"00\" OR Track_No = \"-1\" OR Track_No = \"\" OR Track_No is null) "
                + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRec = GetNoRec(SongRecord, cnb, cnc); // .Tables.Count == 0 ? 0 : SongRecord.Tables[0].Rows.Count;

            if (netstatus == "OK" || noOfRec > 0) FixWithSpotifyDetails(SongRecord, 0, noOfRec, cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, "MainDB", timestamp, cnc);


            //fix missing youtube details
            sel = "SELECT * FROM Main WHERE (YouTube_Link = \"\" OR Youtube_Playthrough = \"\" or YouTube_Link is null OR Youtube_Playthrough is null) "
               + "AND ID = " + txt_ID.Text + " ORDER BY Spotify_Artist_ID ASC";
            //Read from DB
            MainDBfields[] SongRecords = new MainDBfields[20000];
            SongRecords = GetRecord_s(sel, cnb, cnc);
            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") GetYoutubeDetailsAsync(SongRecords[0], 0, cnb, pB_ReadDLCs, "MainDB", false, cnc);

            var i = databox.SelectedCells[0].RowIndex;
            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;

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
            var metadatadisplayedonce = ConfigRepository.Instance()["dlcm_AdditionalManipul115"];
            ConfigRepository.Instance()["dlcm_errorsstring"] = "";
            var pack = ""; /*var norows = 0; */var brkn = 0;
            if (chbx_Format.Text == "PS4" || chbx_Format.Text == "iOS") { MessageBox.Show("Not yet working. rEtail songs can be eliminated just new ones packaged\n\nWiP"); return; }
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 5;
            pB_ReadDLCs.Increment(1);

            var slct = "SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u " +
    "WHERE Type=\"Profile\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" and Comments like \"%dlcm_AdditionalManipul%\" AND DisplayGroup IN ('Pack', 'General') ORDER BY DisplayGroup DESC";
            Selection frm1 = new Selection(slct, "Continue Packing", "Stop");
            frm1.ShowDialog();
            if (frm1.StopImport) return;
            //UpdateLog(DateTime.Now, "----", false, c("dlcm_TempPath"), "", "", null, null);

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
            var intheworks_Path = c("dlcm_TempPath") + "\\0_intheworks";
            var export_Path = c("dlcm_TempPath") + "\\0_export";
            string pathDLC = c("dlcm_RocksmithDLCPath");
            CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path
                , repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath
                , Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import, intheworks_Path, export_Path);

            //// Generate package worker
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;

            var i = databox.SelectedCells[0].RowIndex;

            //Read from DB 
            //Use multi-selected if the case
            var sel = "";
            for (int k = 0; k < databox.SelectedRows.Count; k++)
            {
                if (k > 0) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            //var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);
            //var norows = SongRecord[0].NoRec.ToInt32();

            Groupss = chbx_Group.Text.ToString();
            var cmd = "SELECT * FROM Main ";
            cmd += sel.Length > 0 ? "WHERE ID in (" + sel + ")" : txt_ID.Text;/**/

            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(cmd, cnb, cnc);
            if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();

            string spotystatus = "", ybstatus = "", ftpstatus = "";
            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU")) if (FTPAvail(txt_FTPPath.Text).ToLower() == "ok") ftpstatus = "ok";/*"", ConfigRepository.Instance()["dlcm_FTP" + ConfigRepository.Instance()["dlcm_FTP"]]*/
                else ftpstatus = "nok";

            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackagePreparation();

            //DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT max(val(Pack)) FROM Main", null, cnb, cnc);
            //if (dms.Tables[0].Rows.Count > 0) pack = (int.Parse(dms.Tables[0].Rows[0].ItemArray[0].ToString()) + 1).ToString();
            pack = (int.Parse(GetMax("Pack_AuditTrail", "Pack", cnb, cnc)) + 1).ToString();

            //var i = 0;
            var bassRemoved = "No"; var metainfo = ""; var norows = SongRecord[0].NoRec.ToInt32();

            if (norows > 0) CreatePackingGroup(cmd, cnb, cmb_Filter.Text, norows, cnc);

            // calc a sumamry of the metadata
            var f = false;
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul123"] == "Yes")
            {
                var tgst = "Start full summary of Metadata of the soon-to-be-packed songs"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                if (GetMEtaBeforePacking(0, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, null, timestamp, AppWD, cmd, c("dlcm_AdditionalManipul54") == "Yes" ? true : false, Groupss, c("dlcm_SearchFields"))) f = true;
                tgst = "End full summary of Metadata of the soon to be packed songs"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            if (f) return;

            var ordno = ""; i = 0; pB_ReadDLCs.Maximum = norows; pB_ReadDLCs.Value = 0;
            foreach (var filez in SongRecord)
            {
                if (filez.ID == null) break;
                ordno = ordno + "1"; i++;
                timestamp = UpdateLog(timestamp, "\nPacking " + pB_ReadDLCs.Value + "/" + filez.NoRec + " song: " + filez.Artist + " - " + filez.Song_Title, true, Temp_Path_Import, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //if (chbx_PackBeta.Checked) filez.Is_Beta = "Yes";(chbx_PackBeta.Checked?true:)
                var args = filez.ID + ";" + (bassRemoved == "No" ? "false" : "true") + ";";
                args += (chbx_Format.Text == "PC" ? "PC" : "") + ";" + (chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU" ? "PS3" : "") + ";" + (chbx_Format.Text == "XBOX360" ? "XBOX360" : "") + ";" + (chbx_Format.Text == "Mac" ? "Mac" : "") + ";" + netstatus + ";";
                args += (filez.Is_Beta == "Yes" ? true : false) + ";" + chbx_Group.Text + ";";
                args += Groupss + ";" + c("dlcm_TempPath") + ";";
                args += chbx_UniqueID.Checked + ";" + chbx_Last_Packed.Checked + ";";
                args += chbx_Last_Packed.Enabled + ";" + chbx_CopyOld.Checked + ";";
                args += chbx_CopyOld.Enabled + ";" + chbx_Copy.Checked + ";";
                args += chbx_Replace.Checked + ";" + chbx_Replace.Enabled + ";";
                args += pack + ";" + "MainDB" + ";";/*SourcePlatform*/
                args += filez.Original_FileName.Replace(";", "") + ";" + filez.Folder_Name.Replace(";", "") + ";";
                args += c("dlcm_AdditionalManipul49") + ";" + txt_RemotePath.Text.Replace(";", "") + ";" + txt_FTPPath.Text.Replace(";", "") + ";";
                args += chbx_RemoveBassDD.Checked + ";" + (filez.Bass_Has_DD == "Yes" ? true : false) + ";" + chbx_KeepBassDD.Checked + ";";
                args += chbx_KeepDD.Checked + ";" + filez.Is_Original + ";" + filez.ID + ";";
                args += SearchCmd + (SearchCmd.IndexOf(";") > 0 ? "" : ";") + pathDLC + ";" + filez.DLC_Name + ";"; //SearchCmd + ";" + c("dlcm_RocksmithDLCPath"), DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value
                args += c("dlcm_AdditionalManipul76") + ";" + "" + ";" + "MainDB" + ";" + "" + ordno + i + ";" + spotystatus + ";" + ybstatus + ";" + ftpstatus + ";" + "No";
                ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "";
                pB_ReadDLCs.Increment(1);
                bwRGenerate.RunWorkerAsync(args);
                do
                    System.Windows.Forms.Application.DoEvents();
                while (bwRGenerate.IsBusy || ConfigRepository.Instance()["dlcm_GlobalTempVariable"] != "Yes");//keep singlethread as toolkit not multithread abled!bwRGenerate.CancellationPending &&
                if (ConfigRepository.Instance()["dlcm_Global2TempVariable"] == "Yes") break;
                if (txt_ID.Text == filez.ID)
                {
                    cmd = "SELECT * from LogPacking WHERE Pack=\"" + pack + "\" AND CDLC_ID=" + filez.ID + "";
                    DataSet dus = new DataSet(); dus = SelectFromDB("LogPacking", cmd, "", cnb, cnc); var rowc = GetNoRec(dus, cnb, cnc);

                    if (rowc == 0)
                    {
                        chbx_Broken2.Checked = true;
                        cmd = "SELECT Comments from LogPackingError WHERE Pack=\"" + pack + "\" AND CDLC_ID=" + filez.ID + " ORDER BY ID DESC;";
                        DataSet dis = new DataSet(); dis = SelectFromDB("LogPackingError", cmd, "", cnb, cnc);
                        //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                        var rec = GetNoRec(dis, cnb, cnc);//dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
                        if (rec > 0) txt_FilesMissingIssues.Text = dis.Tables[0].Rows[0].ItemArray[0].ToString();
                        // UpdatePackingLog("LogPackingError", ConfigRepository.Instance()["dlcm_DBFolder"], pack.ToInt32(), filez.ID, "", cnb, cnc);
                    }
                }
            }
            pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Value = 3;
            pB_ReadDLCs.Increment(1);

            if ((chbx_Format.Text == "PS3_US" || chbx_Format.Text == "PS3_JP" || chbx_Format.Text == "PS3_EU") && c("dlcm_AdditionalManipul92") == "Yes") HANPackage();
            ConfigRepository.Instance()["dlcm_MuliThreading"] = old;
            bwRGenerate.Dispose();
            GeneratePackingSummary(pack, ConfigRepository.Instance()["dlcm_Global2TempVariable"], brkn, cnb, i, norows, cnc);
            ConfigRepository.Instance()["dlcm_AdditionalManipul115"] = metadatadisplayedonce;
            timestamp = UpdateLog(timestamp, "Ended Packing.", true, null, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

        }

        private void btn_GoTemp_Click(object sender, EventArgs e)
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

        //public static void Wav2Ogg(string sourcePath, string destinationPath, int qualityFactor)
        //{
        //    if (destinationPath == null)
        //        destinationPath = String.Format("{0}", Path.ChangeExtension(sourcePath, "ogg"));
        //    // interestingly ODLC uses 44100 or 48000 interchangeably ... so resampling is not necessary
        //    var cmdArgs = String.Format(" -q {2} \"{0}\" -o \"{1}\"", sourcePath, destinationPath, Convert.ToString(qualityFactor));

        //    GeneralExtension.RunExternalExecutable(APP_OGGENC, true, false, true, cmdArgs);
        //}

        ///// <summary>
        ///// Covert Wav to Wem using WwiseCLI.exe
        ///// for faster conversion, source path should be wav file
        ///// </summary>
        ///// <param name="wavSourcePath"></param>
        ///// <param name="destinationPath"></param>
        ///// <param name="audioQuality"></param>
        //public static void Wav2Wem(string wavSourcePath, string destinationPath, int audioQuality)
        //{
        //    try
        //    {
        //        var wwiseCLIPath = GetWwisePath();
        //        var wwiseTemplateDir = LoadWwiseTemplate(wavSourcePath, audioQuality);

        //        // console writes may be captured by starting toolkit in a command window an redirecting the output to a file
        //        // e.g., ‘RocksmithToolkitGUI.exe >console.log’ 
        //        Console.WriteLine("WwiseCLI:\n\'" + wwiseCLIPath + "\'\n\nTemplate:\n\'" + wwiseTemplateDir + "\'");

        //        // apply magicDust to WwiseCLI.exe to force conversions (known Wwise2010 issue)
        //        ExternalApps.Wav2Wem(wwiseCLIPath, wwiseTemplateDir, 10);
        //        GetWwiseFiles(destinationPath, wwiseTemplateDir);
        //    }
        //    catch (Exception ex)
        //    {
        //        //overridden ex, can't get real ex/msg, use log + throw;
        //        throw new Exception("Wwise audio file conversion failed: " + ex.Message + Environment.NewLine);
        //    }
        //}


        private async void btn_Debug_Click(object sender, EventArgs e)
        {
            //    cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
            //                    "Album_ArtPathOrig = REPLACE(Album_ArtPathOrig, '" + OLD_Path + "', '" + NEW_Path + "'), " +
            //                    "AudioPath=REPLACE(AudioPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
            //                    "audioPreviewPath=REPLACE(audioPreviewPath,'" + OLD_Path + "','" + NEW_Path + "'), " +
            //                    "Folder_Name=REPLACE(Folder_Name, '" + OLD_Path + "','" + NEW_Path + "'), " +
            //                    "OggPath = REPLACE(OggPath, '" + OLD_Path + "','" + NEW_Path + "'), " +
            //                    "oggPreviewPath = REPLACE(oggPreviewPath, '" + OLD_Path + "','" + NEW_Path + "')" +
            //                    ";";
            FixAudiofileInconsist(0, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd);
        }

        private async void btn_Debug_Click_1(object sender, EventArgs e)
        {
            DataSet dhf = new DataSet(); var cmd = "SELECT XMLFilePath,ID FROM Arrangements where CleanedXML_Hash=\"\"" + GetArrOfficSQLTxt(arrangoff);// WHERE CDLC_ID=" + txt_ID.Text;
            dhf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb, cnc);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
            pB_ReadDLCs.Maximum = GetNoRec(dhf, cnb, cnc);//dhf.Tables[0].Rows.Count;dhf.Tables[0].Rows.Count
            for (var h = 0; h < pB_ReadDLCs.Maximum; h++)
            {
                var rt = GetHashCleanXML(dhf.Tables[0].Rows[h].ItemArray[0].ToString());
                var cmdupd = "UPDATE Arrangements Set CleanedXML_Hash=\"" + rt + "\" WHERE ID = " + dhf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb, cnc);
                if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                var tst = "XML hasin: " + h; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            DataSet ghf = new DataSet(); cmd = "SELECT JSONFilePath,ID FROM Arrangements where Json_Hash=\"\"" + GetArrOfficSQLTxt(arrangoff);// WHERE CDLC_ID=" + txt_ID.Text;
            ghf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb, cnc);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
            pB_ReadDLCs.Maximum = GetNoRec(ghf, cnb, cnc); //ghf.Tables[0].Rows.Count
            ; pB_ReadDLCs.Value = 0;
            for (var h = 0; h < pB_ReadDLCs.Maximum; h++)
            {
                var rt = ghf.Tables[0].Rows[h].ItemArray[0].ToString();//.Replace("\\arr\\", "\\bin\\");
                                                                       //if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                                                       //    rt = (rt.Replace(".xml", ".sng").Replace("\\EOF\\", "\\Toolkit\\"));
                                                                       //else
                                                                       //    rt = rt.Replace(".xml", ".sng").Replace("\\songs\\bin", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                                                                       //snghlist.Add(GetHash(s1));
                rt = GetHashCleanXML(rt);
                var cmdupd = "UPDATE Arrangements Set Json_Hash=\"" + rt + "\" WHERE ID = " + ghf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb, cnc);
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set Reason = \"\" WHERE Reson=\"Missing PSARC\"; ", cnb, cnc);
        }

        private void btn_Remove_HashDuplicates_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail as i LEFT JOIN Pack_AuditTrail as p on p.FileHash=i.FileHash and p.id<>i.ID WHERE i.PackPath like \"%0_duplicate%\"";
            var tcmd = "SELECT PackPath+\"\\\"+FileName ";
            if (chbx_Ignore_Officials.Checked)
            {
                cmd += " AND Official=\"No\";";
                tcmd = "SELECT PackPath+\"\\\"+FileName " + cmd;
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb, cnc);
                var rec = GetNoRec(dvr, cnb, cnc); //dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString(), false);
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }

            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb, cnc);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb, cnc);
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
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb, cnc);
                var rec = GetNoRec(dvr, cnb, cnc); //dvr.Tables[0].Rows.Count;
                if (rec > 0) for (int j = 0; j < rec; j++) DeleteFile(dvr.Tables[0].Rows[j][0].ToString(), false);
            }
            else
            {
                try
                {
                    CleanFolder(c("dlcm_TempPath") + "\\0_duplicate", "", false, true, Archive_Path, "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            DeleteFromDB("Import_AuditTrail", "SELECT * WHERE FileHash NOT IN (SELECT FileHash FROM Main)", cnb, cnc);
            DeleteFromDB("Pack_AuditTrail", tcmd, cnb, cnc);
            MessageBox.Show("All Duplicates songs have been deleted");
        }

        private void btn_PKGLinker_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PKG_Linker"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install PKG Linker Server if you want to use it.", c("dlcm_PKG_Linker_www"), "Missing PKG Linker Server", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}
        }

        private void btn_TrueRepacker_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_TrueAncestor_PKG_Repacker"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install TrueAncestor PKG Repacker if you want to use it.", c("dlcm_TrueAncestor_PKG_Repacker_www"), "Missing TrueAncestorPKG Repacker", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}
        }

        private void btn_PKGSigner_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_PS3xploit-resigner"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install PS3xploit Resigner if you want to use it.", c("dlcm_PS3xploit-resigner_www"), "Missing PS3xploit Resigner", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}
        }

        private void btn_TotalCommander_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_TCommander"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Total Commander if you want to use it.", c("dlcm_TCommander_www"), "Missing Total Commander", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            //}
        }

        private void btn_OpenMulti_Click(object sender, EventArgs e)
        {

            for (var j = 1; j < 100; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                if (!(DirectoryExists(dest))) return;
                CopyFolder(AppWD + "\\..\\..", dest); var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dest + "\\RocksmithToolkitGUI.exe");
                StartProcesss(@xx, null);
                //try
                //{
                //    Process process = Process.Start(@xx);
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
                //}
            }
        }
        private void SymbolLinkFolder(string t, string tt)
        {
            if (t == "") return;
            if (File.Exists(t)) DeleteFile(t, false);
            else
            if (DirectoryExists(t))
            {
                DeleteDirectory(t, false);
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


        //private void cmb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var Filtertxt = cmb_Filter.Text;
        //    var OrderAlt = "";
        //    if (Filtertxt == "") return;
        //    if (chbx_AutoSave.Checked) SaveRecord(); SaveOK = false;


        //    try
        //    {
        //        dssx.Dispose();
        //        Populate(ref databox, ref Main);
        //        databox.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
        //        MessageBox.Show(ex.Message + "Can't run Filter ! " + SearchCmd);
        //    }

        //    if (Filterorg == "Sorted by Groups value/Group added date") SearchCmd = SearchCmdf;

        //    //Update_Selected();
        //    SaveOK = c("dlcm_Autosave") == "Yes" ? true : false;
        //}

        private void txt_Platform_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_AllGroups_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_RemoveSelectedRemote_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath+\"\\\"+FileName = \"" + cmb_Packed.Text + "\"";
            DeleteFile(cmb_Packed.Text, false);
            cmb_Packed.Items.RemoveAt(cmb_Packed.SelectedIndex);
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb, cnc);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            CleanPack();
            DeleteMultiInstances();
        }
        private void CleanPack()
        {
            var cmd1 = "UPDATE Main SET Split4Pack = \"\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
        }
        private void DeleteMultiInstances()
        {
            for (var j = 1; j < 100; j++)
            {
                var dest = AppWD.Substring(0, AppWD.Replace("\\DLCManager\\external_tools", "").LastIndexOf("\\")) + "\\RK" + j;
                if (DirectoryExists(dest)) DeleteDirectory(dest, false);
            }
        }
        private void cMS_RightClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (tlSMI_Next.) ;
        }

        private void txt_Track_No_ValueChanged(object sender, EventArgs e)
        {
            if (txt_Track_No.Text != null) chbx_A_HasTrackNo.Checked = true;
            else chbx_A_HasTrackNo.Checked = false;
        }
        private void ContextMenuMenu_Broken_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_Broken2, "Is_Broken", "Yes", "", "", "");
            //var j = mouseLocation.RowIndex;

            //var i = databox.SelectedCells[0].RowIndex;

            ////Use multi-select if the case
            //var sel = "SELECT * FROM Main WHERE ID IN ("; int k = 0;
            //for (k = 0; k < databox.SelectedRows.Count; k++)
            //{
            //    if (k >= 1) sel += ", ";
            //    sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            //}
            //sel += ")";
            //if (k == 1 && i != j) sel = databox.Rows[j].Cells["ID"].Value.ToString();

            //var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);/*.Replace("*", "ID")*/
            //var norows = SongRecord[0].NoRec.ToInt32();

            //if (i == j) chbx_Broken2.Checked = true;
            //else
            //{
            //    var updatecmd = "UPDATE Main SET Is_Broken=\"Yes\" WHERE ID in (" + sel.Replace("*", "ID") + ")";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    UpdateDB("Main", updatecmd, cnb, cnc);
            //}
            //Populate(ref databox, ref Main);
            //databox.Refresh();
        }

        private void ContextMenuMenu_UnBroken_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_Broken2, "Is_Broken", "\"No\"", "", "", "");
            //var j = mouseLocation.RowIndex;

            //var i = databox.SelectedCells[0].RowIndex;

            ////Use multi-select if the case
            //var sel = "SELECT * FROM Main WHERE ID IN ("; int k = 0;
            //for (k = 0; k < databox.SelectedRows.Count; k++)
            //{
            //    if (k >= 1) sel += ", ";
            //    sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            //}
            //sel += ")";
            //if (k == 1 && i != j) sel = databox.Rows[j].Cells["ID"].Value.ToString();

            //var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);/*.Replace("*","ID")*/
            //var norows = SongRecord[0].NoRec.ToInt32();

            //if (i == j) chbx_Broken2.Checked = true;
            //else
            //{
            //    var updatecmd = "UPDATE Main SET Is_Broken=\"No\" WHERE ID in (" + sel.Replace("*", "ID") + ")";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    UpdateDB("Main", updatecmd, cnb, cnc);
            //}
            //Populate(ref databox, ref Main);
            //databox.Refresh();
        }

        private void ContextMenuMenu_Reverse_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_Broken2, "Is_Broken", "\"Yes\"", "Is_Broken=\"No\"", "\"Yes\"", "Is_Broken=\"Yes\"");
            //var updatecmd = "UPDATE Main SET Is_Broken=\"Yes\" WHERE ID in (" + sel.Replace("*", "ID") + ") AND Is_Broken=\"No\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //UpdateDB("Main", updatecmd, cnb, cnc);
            //updatecmd = "UPDATE Main SET Is_Broken=\"No\" WHERE ID in (" + sel.Replace("*", "ID") + ") AND Is_Broken=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //UpdateDB("Main", updatecmd, cnb, cnc);
            //var j = mouseLocation.RowIndex;

            //var i = databox.SelectedCells[0].RowIndex;

            ////Use multi-select if the case
            //var sel = "SELECT * FROM Main WHERE ID IN ("; int k = 0;
            //for (k = 0; k < databox.SelectedRows.Count; k++)
            //{
            //    if (k >= 1) sel += ", ";
            //    sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            //}
            //sel += ")";
            //if (k == 1 && i != j) sel = databox.Rows[j].Cells["ID"].Value.ToString();

            //var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);/*.Replace("*", "ID"),*/
            //var norows = SongRecord[0].NoRec.ToInt32();

            //if (i == j) chbx_Broken2.Checked = chbx_Broken2.Checked ? false : true;
            //else
            //{
            //    var updatecmd = "UPDATE Main SET Is_Broken=\"Yes\" WHERE ID in (" + sel.Replace("*", "ID") + ") AND Is_Broken=\"No\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    UpdateDB("Main", updatecmd, cnb, cnc);
            //    updatecmd = "UPDATE Main SET Is_Broken=\"No\" WHERE ID in (" + sel.Replace("*", "ID") + ") AND Is_Broken=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    UpdateDB("Main", updatecmd, cnb, cnc);
            //}
            //Populate(ref databox, ref Main);
            //databox.Refresh();
        }
        private void tlSMI_Beta_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_Beta, "Is_Beta", "Yes", "", "", "");
            //var j = mouseLocation.RowIndex;

            //var i = databox.SelectedCells[0].RowIndex;

            ////Use multi-select if the case
            //var sel = "SELECT * FROM Main WHERE ID IN ("; int k = 0;
            //for (k = 0; k < databox.SelectedRows.Count; k++)
            //{
            //    if (k >= 1) sel += ", ";
            //    sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            //}
            //sel += ")";
            //if (k == 1 && i != j) sel = databox.Rows[j].Cells["ID"].Value.ToString();

            //var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);/*.Replace("*", "ID")*/
            //var norows = SongRecord[0].NoRec.ToInt32();

            //if (i == j) chbx_Beta.Checked = chbx_Beta.Checked ? false : true;
            //else
            //{
            //    var updatecmd = "UPDATE Main SET Is_Beta=\"Yes\" WHERE ID in (" + sel.Replace("*", "ID") + ")";// AND Is_Beta=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    UpdateDB("Main", updatecmd, cnb, cnc);

            //    // updatecmd = "UPDATE Main SET Is_Beta=\"No\" WHERE ID in (" + sel + ") AND Is_Beta=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
            //    //UpdateDB("Main", updatecmd, cnb, cnc);
            //}
            //Populate(ref databox, ref Main);
            //databox.Refresh();
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
            //SongRecord = GetRecord_s(cmd, cnb, cnc);
            //if (netstatus == "NOK" || netstatus == "") netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();

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
            var selunsel = false;
            var count = 0; var cnt = databox.SelectedRows.Count;
            foreach (DataGridViewRow row in databox.SelectedRows) //Use Multi-select if the case
                if (row.Cells["Selected"].Value.ToString().ToLower() == "Yes".ToLower())
                {
                    selunsel = true; count++;
                }

            if (selunsel) applyricghtclick(chbx_Selected, "Selected", "No", "", "", "");
            else applyricghtclick(chbx_Selected, "Selected", "Yes", "", "", "");
            MessageBox.Show(!selunsel ? "(" + count + " unselected out of " + cnt + ") Items Marked as Selected" : "(" + count + " selected out of " + cnt + ") Items Marked as UnSelected");
        }

        private void applyricghtclick(CheckBox chbx, string field, string value1, string condition1, string value2, string condition2)
        {
            //if (databox.SelectedRows.Count == 1) chbx.Checked = chbx.Checked ? false : true;
            //else
            //{
            SaveRecord();
            var sel = "";
            foreach (DataGridViewRow row in databox.SelectedRows) //Use Multi-select if the case
                sel += row.Cells[0].Value.ToString() + ", ";

            sel += ";"; sel = sel.Replace(", ;", "");
            var updatecmd = "UPDATE Main SET " + field + "=\"" + value1 + "\" WHERE ID in (" + sel.Replace("*", "ID") + ")" +
               (condition1 == "" ? "" : " AND " + condition1);
            UpdateDB("Main", updatecmd + ";", cnb, cnc);

            if (condition2 != "")
            {
                updatecmd = "UPDATE Main SET " + field + "=" + value1 + " WHERE ID in (" + sel.Replace("*", "ID") + ")" +
                (condition1 == "" ? "" : " AND " + condition1);
                UpdateDB("Main", updatecmd, cnb, cnc);
            }

            SaveOK = false; Populate(ref databox, ref Main); SaveOK = true;
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
            //}
        }

        private void btn_AddInstrumental_Click(object sender, EventArgs e)
        {
            DialogResult result1 = DialogResult.Yes;
            if (txt_Lyrics.Text != "" && !File.Exists(txt_Lyrics.Text)) result1 = MessageBox.Show("Are you sure you want to replace existing Lyric! ", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return;

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
                , txt_ID.Text, chbx_Lyrics.Checked, cnc);
            txt_Lyrics.Text =
               txt_Lyrics.Text = fn.IndexOf(databox.Rows[i].Cells["Folder_Name"].Value.ToString()) < 0 ? databox.Rows[i].Cells["Folder_Name"].Value.ToString() + (txt_Platform.Text.ToLower() == "XBOX360".ToLower() ? "\\Root" : "")
               + "\\songs\\arr\\" + Path.GetFileName(fn) : fn;
            chbx_LyricsChanged.Checked = true;
            chbx_Lyrics.Checked = true;
            btn_ShowLyrics.Enabled = true;
            btn_CreateLyrics.Enabled = false;
            DeleteFile(fn, false);
            chbx_A_IsImprovedWithDM.Checked = true;
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
            databox.Visible = false;
            databox.Refresh(); databox.Visible = true;
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
        private void bth_ShiftVocalNotes_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            ShiftVocalNotes(cmb_Tracks.Text.ToString(), filezPath, not_t, num_Lyrics.Value, i
                , txt_ID.Text, timestamp, txt_Platform.Text, arrangoff, not_p, databox.Rows[i].Cells["Song_Lenght"].Value.ToString(), txt_DuplicateOf.Text,
                databox.Rows[i].Cells["Folder_Name"].Value.ToString(), txt_Title.Text);
            chbx_Lyrics.Checked = true;
            //if (cmb_Tracks.ToString() != "")
            //{
            //    if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Bass") { chbx_Bass.Checked = true; chbx_Bass.Visible = true; }
            //    else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Lead") { chbx_Lead.Checked = true; chbx_Lead.Visible = true; }
            //    else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Rhythm") { chbx_Rhythm.Checked = true; chbx_Rhythm.Visible = true; }
            //    else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Combo") { chbx_Combo.Checked = true; chbx_Combo.Visible = true; }
            //    else;
            //}

            if (cmb_Tracks.Text.ToString() != "") chbx_A_IsImprovedWithDM.Checked = true;
            if (cmb_Tracks.Text.ToString() != "") ListTracks(txt_DuplicateOf.Text, txt_ID.Text, databox.Rows[i].Cells["Song_Lenght"].Value.ToString());
        }
        public static void ShiftVocalNotes(string cmb_Tracks, string filezPath, string not_t, decimal num_Lyrics, int i, string txt_ID, DateTime timestamp
            , string txt_Platform, bool arrangoff, float not_p, string Song_Lenght, string txt_DuplicateOf, string Folder_Name, string txt_Title)
        {
            if (cmb_Tracks == "" && filezPath == "")
            {
                MessageBox.Show("Chose a Track first!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tsst = "";
            var shiftt = not_t == "0" ? num_Lyrics.ToString() : not_t + "%";

            var noOfRec = 0; var XMLFilePath = ""; var RouteMask = ""; string ArrangementType = ""; string DLCID = "0"; string destination_dir = ""; string newXMLFilePath = "";
            var ArrangID = ""; var newJSONFilePath = ""; var json = ""; var SNGFileName = ""; var newSNGFilePath = ""; var sng = "";
            DataSet dus = new DataSet(); var lwnght = "";

            if (cmb_Tracks is null ? false : cmb_Tracks.ToString() != "")
            {
                ArrangID = cmb_Tracks.Substring(cmb_Tracks.IndexOf("_ArrangementID="),
                    cmb_Tracks.Length - cmb_Tracks.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
                DLCID = cmb_Tracks.Substring(8, cmb_Tracks.IndexOf(",") - 8);
                destination_dir = Folder_Name + (txt_Platform.ToLower() == "XBOX360".ToLower() ? "\\Root" : "");

                dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName, " +
                    "RouteMask, Start_Time, JSONFilePath,SNGFileName  FROM Arrangements WHERE ID=" + ArrangID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                noOfRec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
                XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                SNGFileName = dus.Tables[0].Rows[0].ItemArray[5].ToString();
                json = dus.Tables[0].Rows[0].ItemArray[4].ToString(); lwnght = dus.Tables[0].Rows[0].ItemArray[1].ToString();
                newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
                newJSONFilePath = destination_dir + "\\manifests" + json.Substring(json.IndexOf("\\manifests") + 10, json.Length - json.IndexOf("\\manifests") - 10);// json.LastIndexOf("\\") + ".json";
                newSNGFilePath = destination_dir +
                   (destination_dir.IndexOf("\\Ps3\\") > 0 ? "\\songs\\bin\\Ps3\\" : (destination_dir.IndexOf("\\Mac\\") > 0 ? "\\songs\\bin\\Mac\\" : "\\songs\\bin\\generic\\")) +
                   SNGFileName + ".sng";
                sng = XMLFilePath.Substring(0, XMLFilePath.IndexOf("\\songs\\")) +
                   (XMLFilePath.IndexOf("\\Ps3\\") > 0 ? "\\songs\\bin\\Ps3\\" : (XMLFilePath.IndexOf("\\Mac\\") > 0 ? "\\songs\\bin\\Mac\\" : "\\songs\\bin\\generic\\")) +
                   SNGFileName + ".sng";
            }
            else
            {
                try
                {
                    var xmlContents = Song2014.LoadFromFile(filezPath);
                    ArrangementType = null;
                    RouteMask = xmlContents.ArrangementProperties.RouteMask.ToString() == "0" ? xmlContents.Arrangement.ToString() : xmlContents.ArrangementProperties.RouteMask.ToString();
                }
                catch (Exception Exx)
                {
                }
                XMLFilePath = filezPath;
                newXMLFilePath = filezPath;
                lwnght = "";// not_t;
            }
            var s2s = float.Parse(Math.Round(num_Lyrics, 3).ToString());

            if (cmb_Tracks is null ? false : cmb_Tracks.ToLower().IndexOf("vocal") >= 0)
            {

                if (txt_ID != DLCID)
                {
                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty, Default, Primary";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                        "instr(JSONFilePath, '\\manifests')-10), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, \"Added from " + DLCID + " " + txt_Title + "\", (Start_Time+" + s2s + "), CleanedXML_Hash," +
                        " Json_Hash, Part, MaxDifficulty, \"\", \"\" FROM Arrangements WHERE ID = " + ArrangID;

                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0, cnc);
                }
                else
                {
                    var updatecmd = "UPDATE Arrangements SET Comments=Comments+\" shifted by " + num_Lyrics.ToString()
                        + "\", Start_Time=Start_Time+" + num_Lyrics.ToString() + " WHERE ID = " + ArrangID;
                    UpdateDB("Arrangements", updatecmd, cnb, cnc);// if (!File.Exists(XMLFilePath + ".old2")) File.Copy(XMLFilePath, newXMLFilePath, true);
                }

                try
                {
                    if (!File.Exists(newXMLFilePath + ".old2") && File.Exists(newXMLFilePath)) File.Copy(newXMLFilePath, newXMLFilePath + ".old2", false);
                    if (txt_ID != DLCID) File.Copy(XMLFilePath, newXMLFilePath, false);
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
                            xmlContent.Vocal[j].Time = xmlContent.Vocal[j].Time + float.Parse(num_Lyrics.ToString());

                        using (var stream = File.Open(XMLFilePath, FileMode.Create))
                            xmlContent.Serialize(stream);
                        cleanlyrics(txt_ID, cnb, false, cnc);
                        var updatecmdd = "UPDATE Arrangements SET XMLFile_Hash=\"" + GetHash(XMLFilePath) + "\" WHERE ID = " + ArrangID;
                        UpdateDB("Arrangements", updatecmdd, cnb, cnc);
                        //updatecmdd = "UPDATE Main SET Has_Vocal=\"Yes\" WHERE ID = " + ArrangID;
                        //UpdateDB("Main", updatecmdd, cnb, cnc);
                    }
                    catch (Exception ex) { tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            else
            {
                DialogResult result11 = DialogResult.OK;
                if (num_Lyrics == 0) result11 = MessageBox.Show("You are trying to shift the Arrangements notes by 0 seconds.\nDo you want to start even-Distribution workflow instead?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result11 == DialogResult.Yes)

                //    ;
                var bonus = "bonus";
                if (txt_ID != DLCID && cmb_Tracks.ToString() != "")
                {
                    var cmd = "SELECT XMLFilePath, XMLFileName, RouteMask, Start_Time, JSONFilePath,SNGFileName FROM Arrangements WHERE ID = " + ArrangID + " AND Bonus =\"True\" and RouteMask=\"" + dus.Tables[0].Rows[0].ItemArray[2].ToString() + "\"" + GetArrOfficSQLTxt(arrangoff);
                    DataSet dis = new DataSet(); dis = SelectFromDB("Arrangements", cmd, "", cnb, cnc);
                    noOfRec = GetNoRec(dis, cnb, cnc); //dis.Tables.Count <= 0 ? 0 : dis.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                    {
                        DialogResult result1 = MessageBox.Show("Normal Bonus track already existing. Not adding current proposed track. ", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    else
                        bonus = "True";
                    //bonus = "false";

                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty, Default, PrimaryTrack, Favorite, Broken, Official, PersistentID, CapoFret";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                        "instr(JSONFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, \"Added from " + DLCID + " " + txt_Title + "\", (Start_Time+" + s2s + "), CleanedXML_Hash," +
                        " Json_Hash, Part, MaxDifficulty, Default, PrimaryTrack, Favorite, Broken, Official, PersistentID, CapoFret FROM Arrangements WHERE ID = " + ArrangID;

                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0, cnc);

                    try
                    {
                        if (!File.Exists(newXMLFilePath + ".old2") && File.Exists(newXMLFilePath)) File.Copy(newXMLFilePath, newXMLFilePath + ".old2", false);
                        File.Copy(XMLFilePath, newXMLFilePath, false);
                        if (!File.Exists(newJSONFilePath + ".old2") && File.Exists(newJSONFilePath)) File.Copy(newJSONFilePath, newJSONFilePath + ".old2", false);
                        File.Copy(json, newJSONFilePath, false);
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
                        if (not_t == "0")
                            try
                            {
                                xmlContent = Song2014.LoadFromFile(newXMLFilePath);
                                //xmlContent.SongLength += s2s;
                                s2s = float.Parse(Math.Round(s2s, 3).ToString());
                                for (var j = 0; j < xmlContent.PhraseIterations.Length; j++)
                                    if (xmlContent.PhraseIterations[j].Time >= not_p)
                                        xmlContent.PhraseIterations[j].Time = float.Parse(Math.Round(xmlContent.PhraseIterations[j].Time + s2s, 3).ToString());
                                for (var j = 0; j < xmlContent.Ebeats.Length; j++)
                                    ;// if (xmlContent.Ebeats[j].Time >= not_p) xmlContent.Ebeats[j].Time += s2s;
                                for (var j = 0; j < xmlContent.Sections.Length; j++)
                                    if (xmlContent.Sections[j].StartTime >= not_p)
                                        xmlContent.Sections[j].StartTime = float.Parse(Math.Round(xmlContent.Sections[j].StartTime + s2s, 3).ToString());
                                for (var j = 0; j < xmlContent.Levels.Length; j++)
                                {
                                    for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                                        if (xmlContent.Levels[j].Notes[k].Time >= not_p)
                                            xmlContent.Levels[j].Notes[k].Time = float.Parse(Math.Round(xmlContent.Levels[j].Notes[k].Time + s2s, 3).ToString());

                                    for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
                                        if (xmlContent.Levels[j].Chords[k].Time >= not_p)
                                        {
                                            xmlContent.Levels[j].Chords[k].Time = float.Parse(Math.Round(xmlContent.Levels[j].Chords[k].Time + s2s, 3).ToString());
                                            for (var m = 0; m < xmlContent.Levels[j].Chords[k].ChordNotes.Length; m++)
                                                xmlContent.Levels[j].Chords[k].ChordNotes[m].Time = xmlContent.Levels[j].Chords[k].Time;
                                        }
                                    for (var k = 0; k < xmlContent.Levels[j].Anchors.Length; k++)
                                        if (xmlContent.Levels[j].Anchors[k].Time >= not_p)
                                            xmlContent.Levels[j].Anchors[k].Time = float.Parse(Math.Round(xmlContent.Levels[j].Anchors[k].Time + s2s, 3).ToString());
                                    for (var k = 0; k < xmlContent.Levels[j].HandShapes.Length; k++)
                                        if (xmlContent.Levels[j].HandShapes[k].StartTime >= not_p)
                                        {
                                            xmlContent.Levels[j].HandShapes[k].StartTime = float.Parse(Math.Round(xmlContent.Levels[j].HandShapes[k].StartTime + s2s, 3).ToString()); ;
                                            xmlContent.Levels[j].HandShapes[k].EndTime = float.Parse(Math.Round(xmlContent.Levels[j].HandShapes[k].EndTime + s2s, 3).ToString()); ;
                                        }
                                }
                            }
                            catch (Exception ex)
                            {
                                tsst = "Error shifting timings..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            }
                        else // add variable diff to notes
                            try
                            {
                                xmlContent = Song2014.LoadFromFile(newXMLFilePath);
                                for (var j = 0; j < xmlContent.PhraseIterations.Length; j++)
                                    if (xmlContent.PhraseIterations[j].Time > 0)
                                        xmlContent.PhraseIterations[j].Time = calcreltime(xmlContent.PhraseIterations[j].Time, not_t, xmlContent.PhraseIterations[0].Time, false, XMLFilePath
                                            , (j > 0 ? xmlContent.PhraseIterations[j - 1].Time : 0));
                                //for (var j = 0; j < xmlContent.Ebeats.Length; j++)
                                //    if (xmlContent.Ebeats[j].Time > 0)
                                //        ;//  xmlContent.Ebeats[j].Time = calcreltime(xmlContent.Ebeats[j].Time, not_t, xmlContent.Ebeats[0].Time, false);

                                for (var j = 0; j < xmlContent.Levels.Length; j++)
                                {
                                    if (not_t.Contains("p3"))
                                    {
                                        //float[][] sounds =new float[10000,3]; //[time][note/chord]
                                        float[,] sounds = new float[3000, 4];
                                        //initialise
                                        //for (var k = 0; k < sounds.Length-1; k++)
                                        //{
                                        //    sounds[k, 0] = 0;
                                        //    sounds[k, 1] = 0;
                                        //    sounds[k, 2] = 0;
                                        //}

                                        //consolidate all chord and notes as to move them using P3 algorithm in relation to eachother

                                        if (xmlContent.Levels[j].Notes.Length > 3000 || xmlContent.Levels[j].Chords.Length > 3000)
                                            MessageBox.Show("more than 3000 notes/chords: " + xmlContent.Levels[j].Notes.Length + "/" + xmlContent.Levels[j].Chords.Length);
                                        for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++) if (xmlContent.Levels[j].Notes[k].Time > 0)
                                            {
                                                sounds[k, 0] = xmlContent.Levels[j].Notes[k].Time;
                                                sounds[k, 1] = 0;
                                                sounds[k, 2] = k;
                                                sounds[k, 3] = xmlContent.Levels[j].Notes[k].Sustain;
                                            }
                                        for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++) if (xmlContent.Levels[j].Chords[k].Time > 0)
                                            {
                                                sounds[xmlContent.Levels[j].Notes.Length + k, 0] = xmlContent.Levels[j].Chords[k].Time;
                                                sounds[xmlContent.Levels[j].Notes.Length + k, 1] = 1;
                                                sounds[xmlContent.Levels[j].Notes.Length + k, 2] = k;
                                                sounds[xmlContent.Levels[j].Notes.Length + k, 3] = xmlContent.Levels[j].Chords[k].ChordNotes[0].Sustain;
                                            }
                                        var len = xmlContent.Levels[j].Notes.Length + xmlContent.Levels[j].Chords.Length;
                                        //sort
                                        try
                                        {
                                            for (var k = 0; k < len - 1; k++)
                                                if (sounds[k, 0] == 0) break;
                                                else
                                                    for (var n = k; n < len; n++)
                                                        if (sounds[k, 0] > sounds[n, 0])
                                                        {
                                                            float a1 = sounds[k, 0];
                                                            float a2 = sounds[k, 1];
                                                            float a3 = sounds[k, 2];
                                                            float a4 = sounds[k, 3];

                                                            sounds[k, 0] = sounds[n, 0];
                                                            sounds[k, 1] = sounds[n, 1];
                                                            sounds[k, 2] = sounds[n, 2];
                                                            sounds[k, 3] = sounds[n, 3];

                                                            sounds[n, 0] = a1;
                                                            sounds[n, 1] = a2;
                                                            sounds[n, 2] = a3;
                                                            sounds[n, 3] = a4;
                                                        }
                                                        else if (sounds[n, 0] == 0) break;
                                        }
                                        catch (Exception ex)
                                        {
                                            tsst = "Error shifting timings..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                        }
                                        //distribute notes
                                        for (var k = 0; k < len; k++)
                                            if (sounds[k, 0] == 0) break;
                                            else
                                            {
                                                sounds[k, 0] = calcreltime(sounds[k, 0], not_t, sounds[0, 0], false, XMLFilePath, (k > 0 ? sounds[k - 1, 0] : 0));
                                                sounds[k, 3] = calcreltime(sounds[k, 3], not_t, sounds[0, 0], false, XMLFilePath, (k > 0 ? sounds[k - 1, 3] : 0));
                                            }
                                        //reintegrate in  notes,chords separate arrays
                                        for (var k = 0; k < len; k++)
                                            if (sounds[k, 0] == 0) break;
                                            else if (sounds[k, 1] == 0)
                                            {
                                                xmlContent.Levels[j].Notes[int.Parse(sounds[k, 2].ToString())].Time = sounds[k, 0];
                                                xmlContent.Levels[j].Notes[int.Parse(sounds[k, 2].ToString())].Sustain = sounds[k, 3];
                                            }
                                            else if (sounds[k, 1] == 1)
                                            {
                                                xmlContent.Levels[j].Chords[int.Parse(sounds[k, 2].ToString())].Time = sounds[k, 0];
                                                xmlContent.Levels[j].Chords[int.Parse(sounds[k, 2].ToString())].ChordNotes[0].Sustain = sounds[k, 3];
                                            }
                                        //distrib same to indiv chord timings
                                        for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++) for (var m = 0; m < xmlContent.Levels[j].Chords[k].ChordNotes.Length; m++)
                                            {
                                                xmlContent.Levels[j].Chords[k].ChordNotes[m].Time = xmlContent.Levels[j].Chords[k].Time;
                                                if (m > 0) xmlContent.Levels[j].Chords[k].ChordNotes[m].Sustain = xmlContent.Levels[j].Chords[k].ChordNotes[0].Sustain;
                                            }
                                    }
                                    else
                                    {
                                        for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                                            if (xmlContent.Levels[j].Notes[k].Time > 0)
                                            {
                                                xmlContent.Levels[j].Notes[k].Time = calcreltime(xmlContent.Levels[j].Notes[k].Time, not_t, xmlContent.Levels[j].Notes[0].Time, false, XMLFilePath
                                                    , (k > 0 ? xmlContent.Levels[j].Notes[k - 1].Time : 0));
                                                xmlContent.Levels[j].Notes[k].Sustain = xmlContent.Levels[j].Notes[k].Sustain + float.Parse(not_t.Split(';')[2]) * xmlContent.Levels[j].Notes[k].Sustain;
                                            }
                                        for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
                                            if (xmlContent.Levels[j].Chords[k].Time > 0)
                                            {
                                                xmlContent.Levels[j].Chords[k].Time = calcreltime(xmlContent.Levels[j].Chords[k].Time, not_t, xmlContent.Levels[j].Chords[0].Time, false, XMLFilePath
                                                    , (k > 0 ? xmlContent.Levels[j].Chords[k - 1].Time : 0));
                                                for (var m = 0; m < xmlContent.Levels[j].Chords[k].ChordNotes.Length; m++)
                                                    xmlContent.Levels[j].Chords[k].ChordNotes[m].Time = xmlContent.Levels[j].Chords[k].Time;
                                            }
                                    }

                                    //not movingsections are anyway to be moved with shift option
                                    //for (var k = 0; k < xmlContent.Sections.Length; k++)
                                    //    if (k + 1 < xmlContent.Sections.Length)
                                    //        if (xmlContent.Sections[k].StartTime < float.Parse(not_t.Split(';')[2]) && xmlContent.Sections[k + 1].StartTime > float.Parse(not_t.Split(';')[2]))
                                    //        {
                                    //            //var st = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, true, double.Parse(not_t.Split(';')[1]));
                                    //            //var zt = GetTrackStartTime(newXMLFilePath, xmlContent.Arrangement, null, true, double.Parse(xmlContent.Sections[0].StartTime.ToString()));
                                    //            //cmb_Sections.Items.Add("firstnote: " + st + " - endnote: " + zt + " - blank - " + firstnote + "  -FirstSection (Not marked as a section) - First");

                                    //            xmlContent.Sections[k + 1].StartTime = float.Parse(st) - float.Parse((0.06).ToString());
                                    //            //calcreltime(xmlContent.Sections[j].StartTime, not_t, xmlContent.Sections[0].StartTime, true, XMLFilePath
                                    //            //, (j > 0 ? xmlContent.Sections[j - 1].StartTime : 0));
                                    //            //for (var j = 0; j < xmlContent.Levels.Length; j++)
                                    //            //break;
                                    //        }

                                    for (var k = 0; k < xmlContent.Levels[j].Anchors.Length; k++)
                                        if (xmlContent.Levels[j].Anchors[k].Time > 0)
                                            xmlContent.Levels[j].Anchors[k].Time = calcreltime(xmlContent.Levels[j].Anchors[k].Time, not_t, xmlContent.Levels[j].Anchors[0].Time, false, XMLFilePath
                                                , (k > 0 ? xmlContent.Levels[j].Anchors[k - 1].Time : 0));

                                    for (var k = 0; k < xmlContent.Levels[j].HandShapes.Length; k++)
                                        if (xmlContent.Levels[j].HandShapes[k].StartTime > 0)
                                            if (not_t.Split(';')[4] == "True")
                                            {
                                                xmlContent.Levels[j].HandShapes[k].StartTime = 0;
                                                xmlContent.Levels[j].HandShapes[k].EndTime = 0;
                                            }
                                            else
                                            {
                                                xmlContent.Levels[j].HandShapes[k].StartTime = calcreltime(xmlContent.Levels[j].HandShapes[k].StartTime, not_t, xmlContent.Levels[j].HandShapes[0].StartTime, true, XMLFilePath
                                                        , (j > 0 ? xmlContent.Levels[j].HandShapes[k - 1].StartTime : 0));
                                                xmlContent.Levels[j].HandShapes[k].EndTime = calcreltime(xmlContent.Levels[j].HandShapes[k].EndTime, not_t, xmlContent.Levels[j].HandShapes[0].StartTime, true, XMLFilePath
                                                        , (j > 0 ? xmlContent.Levels[j].HandShapes[k - 1].EndTime : 0));
                                            }

                                    //no need to modif lenght as song length and not note based one
                                    //string tx = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, false, -1);// dus.Tables[0].Rows[0].ItemArray[3].ToString());
                                    //string ty = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, true, -1);
                                    //xmlContent.SongLength = getf;
                                }
                            }
                            catch (Exception ex)
                            {
                                tsst = "Error shifting timings..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            }

                    using (var stream = File.Open(newXMLFilePath, FileMode.Create)) xmlContent.Serialize(stream);
                    if (cmb_Tracks.ToString() != "")
                    {
                        var updatecmdd = "UPDATE Arrangements SET Comments=Comments+\" shifted by " + (not_t == "0" ? num_Lyrics.ToString() : shiftt)
                            + "\", Start_Time=(VAL(Start_Time)+" + num_Lyrics.ToString()
                            + "), XMLFile_Hash=\"" + GetHash(XMLFilePath) + "\", Bonus=\"" + bonus + "\" WHERE ID = " + ArrangID; ;
                        UpdateDB("Arrangements", updatecmdd, cnb, cnc);
                    }
                }
                catch (Exception ex)
                {
                    tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    if (!File.Exists(newXMLFilePath + ".old3")) File.Copy(newXMLFilePath + ".old3", newXMLFilePath, true);
                }

            }

            if (tsst == "")
                MessageBox.Show((cmb_Tracks is null ? "" : (cmb_Tracks.ToString() != "" ? "" : "(Single)")) + "Track " + lwnght + " shifted by " + shiftt);
            else
                MessageBox.Show("Track " + dus.Tables[0].Rows[0].ItemArray[1].ToString() + " was NOT shifted by " + (not_t == "0" ? num_Lyrics : not_t + "%"));

            not_t = "0";
            return;
        }

        public static float calcreltime(float iniv, string not_t, float startt, bool ignotetim, string xsml, float prev_new_note)
        {
            var args = (not_t).ToString().Split(';');
            var prc = float.Parse(args[0]);
            var starte = float.Parse(args[1]);
            var ende = float.Parse(args[2]);
            string p = args[3];

            if (!(float.Parse(iniv.ToString()) > starte && float.Parse(iniv.ToString()) <= ende && !ignotetim))
                //    ;// return iniv;//= ignore the start of the section as a fixed anchor
                //else 
                if (!ignotetim) return iniv;

            //correct

            float vall = 0;
            //= ROUNDDOWN(E9 - ($C$3 * (E9 -$E$1) / 100),3)
            float f = 0;
            if (p == "p1")
                f = (starte + (prc * (float.Parse(iniv.ToString()) - starte) / 100));// ((float.Parse(iniv.ToString()) != starte) ? : float.Parse(iniv.ToString()));
                                                                                     //iniv - (prc * (iniv - startt) / 100);
            else if (p == "p2")
                f = (float.Parse(iniv.ToString()) - float.Parse(iniv.ToString()) * prc / 100);// ((float.Parse(iniv.ToString()) != starte) ? : float.Parse(iniv.ToString())); //
            else
            {
                float pn = float.Parse((Math.Round((ignotetim ? getprevapprox_note_or_cord(xsml, iniv) : getprevnote_or_cord(xsml, iniv)), 3)).ToString());
                f = (iniv - pn) - prc * float.Parse((iniv - pn).ToString()) / 100;//iniv - (prc * float.Parse(iniv.ToString()) / 100);
                f = prev_new_note == 0 ? iniv : prev_new_note + f;
            }
            //f = calcavg_vs_prevnote(iniv, prc, xsml);// ((float.Parse(iniv.ToString()) != starte) ? : float.Parse(iniv.ToString())); //
            //=D6-D6*D8/100
            vall = float.Parse((Math.Round(f, 3)).ToString());
            return float.Parse(vall.ToString());
        }

        public static double getprevnote_or_cord(string xsml, double t)
        {
            var xmlContent = Song2014.LoadFromFile(xsml);
            float note = float.Parse(Math.Round(t, 3).ToString());
            for (var j = 0; j < xmlContent.Levels.Length; j++)
            {
                for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                    if (xmlContent.Levels[j].Notes[k].Time == note) if (0 <= k - 1) note = xmlContent.Levels[j].Notes[k - 1].Time;

                for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
                    if (xmlContent.Levels[j].Chords[k].Time == note)
                        if (0 <= k - 1)
                            note = xmlContent.Levels[j].Chords[k - 1].Time;
            }
            return note;
        }
        public static double getprevapprox_note_or_cord(string xsml, double t)
        {
            var xmlContent = Song2014.LoadFromFile(xsml);
            float note = float.Parse(Math.Round(t, 3).ToString());
            for (var j = 0; j < xmlContent.Levels.Length; j++)
            {
                for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                    if (xmlContent.Levels[j].Notes[k].Time < note) if (0 <= k - 1) note = xmlContent.Levels[j].Notes[k - 1].Time;

                for (var k = 0; k < xmlContent.Levels[j].Chords.Length; k++)
                    if (xmlContent.Levels[j].Chords[k].Time < note)
                        if (0 <= k - 1)
                            note = xmlContent.Levels[j].Chords[k - 1].Time;
            }
            return note;
        }

        public static double calcavg_vs_prevnote(float iniv, double perc, string xsml)
        {
            float note = iniv;
            double secn = getprevnote_or_cord(xsml, iniv);
            note = (float.Parse(secn.ToString()) - float.Parse(iniv.ToString()) * float.Parse(perc.ToString()) / 100);
            return note;
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

        //            ShowConnectivityError(exx, "", null);
        //            try
        //            {
        //                if (File.Exists(cnb.DataSource.ToString())) cnb.Open(); //2nd time makes it work sometimes e.g. x64 solution
        //            }
        //            catch (Exception ex)
        //            {
        //                //if (lbl_Access != null)
        //                //{
        //                //    lbl_Access.Text = "missing Access plugin!";
        //                //    lbl_Access.Visible = true;
        //                //}
        //                string vb = null; vb = DisplayData();
        //                ShowConnectivityError(ex, "2nd FAIL to use M$ ACCESS plugin:\n" + vb, null);
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
        //            ShowConnectivityError(exx, "", null);
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
                    //if (DDC.HasExited == false) if (ProcessStarted) 
                    DDC.Kill();
                    DDC.Close();
                }
                ProcessStarted = false;
            }// Cancel the asynchronous operation.
            else
            {
                ProcessStarted = true;
                args = t + ";" + AppWD;
                bwAutoPlay.RunWorkerAsync(args);
                do
                    System.Windows.Forms.Application.DoEvents();
                while (bwAutoPlay.IsBusy);//keep singlethread as toolkit not multithread abled
            }
        }

        public static async void PlayPreview(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument.ToString();
            string[] args = (e.Argument).ToString().Split(';');
            string OggPath = args[0];
            string AppWD = args[1];
            try
            {
                if (DDC.StartInfo.FileName != "")
                {
                    DDC.Kill();
                    DDC.Close();
                }
            }
            catch (Exception ex)
            {
                ;
            }

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
            StartProcesss(@link, null);
            //try
            //{
            //    Process process = Process.Start(@link);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            //}
        }

        private void Btn_SearchYB_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "rocksmith";
            StartProcesss(@link, null);
            //try
            //{
            //    Process process = Process.Start(@link);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            //}
        }

        private void Button3_Click_2(object sender, EventArgs e)
        {
            if (txt_Lyrics.Text != "") Process.Start("explorer.exe", txt_Lyrics.Text);
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            //var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_RockBand"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install C3 Conversion Tools if you want to use it.", c("dlcm_RockBand_www"), "Missing C3 Rockband conversion tools", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open External tool for decompressing rockbad songs ! " + xx);
            //}
        }

        private void Btn_WinMerge_Click(object sender, EventArgs e)
        {
            var paath = c("dlcm_WinMerge");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "WinMerge\\winmergeu.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install WinMerge if you want to use it.", c("dlcm_WinMerge_www"), "Missing WinMerge", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }

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

            var cmd = "SELECT ID, Original_FileName, Artist, Song_Title FROM Main WHERE ID=" + txt_ID.Text + "";
            DataSet dt; dt = SelectFromDB("Main", cmd, "", cnb, cnc);

            cmd = "SELECT XMLFilePath FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + " AND ArrangementType in (\"Guitar\",\"Bass\",\"Vocal\")" + GetArrOfficSQLTxt(arrangoff);
            DataSet dz; dz = SelectFromDB("Arrangements", cmd, "", cnb, cnc);

            foreach (DataRow myRow in dt.Tables[0].Rows)
            {
                var idd = myRow["ID"].ToString();
                for (var j = 0; j < GetNoRec(dz, cnb, cnc); j++)//dz.Tables[0].Rows.Count
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
            string filePath = DirectoryExists(c("dlcm_TempPath") + "\\0_repacked") ? c("dlcm_TempPath") + "\\0_repacked" : c("dlcm_0_repacked") + "\\" + chbx_Format.Text.ToUpper().Replace("_US", "").Replace("_EU", "").Replace("_JP", "");
            StartProcesss(@filePath, null);
            //try
            //{
            //    Process process = Process.Start(filePath);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Repacked Folder in Explorer ! ");
            //}
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            savesettings();
            SaveRecord();
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
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install UltraStar Creator if you want to use it.", c("dlcm_UltraStarCreator_www"), "Missing UltraStar Creator", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }

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
            ConfigRepository.Instance()[Setting] = Value;
            var cmd = "UPDATE Groups SET Groupz=\"" + Value + "\" WHERE ID=" + ID + " AND Type=\"Profile\" AND Comments=\"" + Setting + "\" AND Profile_Name=\"" + c("dlcm_Configurations") + "\"";
            UpdateDB("Groups", cmd, cnb, cnc);
        }
        public string GenSearchGoTo(string cmd)
        {
            var mainrez = ",";
            DataSet dbs = new DataSet(); dbs = SelectFromDB("Main", SearchCmd, "", cnb, cnc);
            var norecb = GetNoRec(dbs, cnb, cnc); //dbs.Tables.Count == 0 ? 0 : dbs.Tables[0].Rows.Count;
            if (norecb > 0)
                for (int k = 0; k < norecb; k++) mainrez += "," + dbs.Tables[0].Rows[k][0].ToString() + ",";

            var t = cmd.Substring(0, cmd.IndexOf(" ORDER BY"));
            cmd = t + (t.ToUpper().Contains("WHERE") ? " AND " : " WHERE ");

            var cm = (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");
            cm += (txt_Title.Text != "" ? " AND Song_Title Like '%" + txt_Title.Text + "%'" : "");
            cm += (txt_Album.Text != "" ? " AND Album Like '%" + txt_Album.Text + "%'" : "");


            cmd += cm + (txt_Live_Details.Text != "" ? " AND Live_Details Like '%" + txt_Live_Details.Text + "%'" : "");
            cmd += (txt_Description.Text != "" ? " AND Description Like '%" + txt_Description.Text + "%'" : "");
            cmd += (txt_OldPath.Text != "" ? " AND Original_FileName Like '%" + txt_OldPath.Text + "%'" : "");
            cmd += (txt_OldPath.Text != "" ? " AND Author Like '%" + txt_Author.Text + "%'" : "");
            cmd += (txt_ID.Text != "" ? " AND ID =" + txt_ID.Text + "" : "");
            cmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";

            cmd = cmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            cmd = cmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
            cmd = cmd.Replace("Main u  AND", "Main u WHERE").Replace("Main u WHERE", "Main u WHERE").Replace("Main u AND", "Main u WHERE").Replace("Main u AND", "Main u WHERE");
            cmd = cmd.Replace("WHERE  AND", "WHERE");
            cmd = cmd.Replace("WHERE  AND", "WHERE").Replace("WHERE  AND", "WHERE").Replace("WHERE AND", "WHERE").Replace("WHERE AND", "WHERE");
            cmd = cmd.Replace("WHERE AND", "WHERE ");
            cmd = cmd.Replace("AND ORDER BY ", "ORDER BY ");

            cm += (txt_ID.Text != "" ? " AND CDLC_ID =\"" + txt_ID.Text + "\"" : "");
            if (cm == "") cm = "AND 1=1";
            //Artist,Artist_Sort, Song_Title, Song_Title_Sort, Album, Album_Sort, Album_Year, AlbumArtPath, DLC_Name, DLC_AppID, " +
            //    "PreviewLenght, AudioBitrate, AudioSampleRate FROM Import_AuditTrail
            //    AND CDCL_ID not in ("+cmd+")
            if (txt_Artist.Text is not null || txt_Title.Text is not null || txt_Album.Text is not null)
            {
                DataSet dzs = new DataSet(); dzs = SelectFromDB("Import_AuditTrail", cmd, "", cnb, cnc);
                var norecz = GetNoRec(dzs, cnb, cnc); //dzs.Tables.Count == 0 ? 0 : dzs.Tables[0].Rows.Count;
                var cmdz = "SELECT CDLC_ID FROM Import_AuditTrail WHERE " + cm + "  ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                cmdz = cmdz.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                cmdz = cmdz.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
                cmdz = cmdz.Replace("Main u  AND", "Main u WHERE").Replace("Main u WHERE", "Main u WHERE").Replace("Main u AND", "Main u WHERE").Replace("Main u AND", "Main u WHERE");
                cmdz = cmdz.Replace("WHERE  AND", "WHERE");
                cmdz = cmdz.Replace("WHERE  AND", "WHERE").Replace("WHERE  AND", "WHERE").Replace("WHERE AND", "WHERE").Replace("WHERE AND", "WHERE");
                cmdz = cmdz.Replace("WHERE AND", "WHERE ");
                cmdz = cmdz.Replace("AND ORDER BY ", "ORDER BY ");
                DataSet dqs = new DataSet(); dqs = SelectFromDB("Import_AuditTrail", cmdz, "", cnb, cnc);            //            v 
                var norecs = GetNoRec(dqs, cnb, cnc); //dqs.Tables.Count == 0 ? 0 : dqs.Tables[0].Rows.Count;
                string additionallyfound = null;
                if (norecz > 0 && 0 < norecs)
                    for (int k = 0; k < norecz; k++)
                    {
                        var found = false;
                        for (int j = 0; j < norecs; j++)
                            if (dzs.Tables[0].Rows[k][0].ToString() == dqs.Tables[0].Rows[j][0].ToString()) found = true;
                        if (!found && !mainrez.Contains("," + dzs.Tables[0].Rows[k][0].ToString() + ","))
                            additionallyfound += dzs.Tables[0].Rows[k][0].ToString() + ",";
                    }
                if (additionallyfound is not null)
                {
                    additionallyfound = additionallyfound.Substring(0, additionallyfound.Length - 1);
                    MessageBox.Show("Some Entries shown (" + additionallyfound + ") " +
                    "also due to addiitonal findings based on original metadata (Artist,title,Album)",
                    MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    //            //for (int j = 0; j < norecs; j++)
                    //            //   if (dqs.Tables[0].Rows[1][3].ToString() != "" && dnss.Tables[0].Rows[j][3].ToString() != null)
                    //            {
                    //                CDLC2Tab 
                    //cmd+=
                    cmd = cmd.Replace(";", "") + " OR ID IN (" + additionallyfound + ");";
                }
            }

            return cmd;
        }
        private void btn_GoTo_Click(object sender, EventArgs e)
        {
            var i = 0;

            var cmd = GenSearchGoTo(SearchCmd);
            //SearchCmd.Substring(0, SearchCmd.IndexOf(" ORDER BY")) + " AND " + (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_Title.Text != "" ? "Song_Title Like '%" + txt_Title.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_Album.Text != "" ? "Album Like '%" + txt_Album.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_Live_Details.Text != "" ? "Live_Details Like '%" + txt_Live_Details.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_Description.Text != "" ? "Description Like '%" + txt_Description.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_OldPath.Text != "" ? "Original_FileName Like '%" + txt_OldPath.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_OldPath.Text != "" ? "Author Like '%" + txt_Author.Text + "%'" : "");
            //cmd += " AND ";
            //cmd += (txt_ID.Text != "" ? "ID Like '%" + txt_ID.Text + "%'" : "");
            //cmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";
            //cmd = cmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            //cmd = cmd.Replace("Main u  AND", "Main u").Replace("Main u AND", "Main u").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
            //cmd = cmd.Replace("WHERE AND", "WHERE ");
            //cmd = cmd.Replace("AND ORDER BY ", "ORDER BY ");

            DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb, cnc); noOfRec = GetNoRec(dhxs, cnb, cnc); //.Tables[0].Rows.Count;
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", SearchCmd, "", cnb, cnc); var noRec = GetNoRec(dhs, cnb, cnc); //.Tables[0].Rows.Count;
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
            chbx_A_IsImprovedWithDM.Checked = true;
        }

        private void txt_NoOfSplits_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var sel = SearchCmd;
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRecs = GetNoRec(SongRecord, cnb, cnc); //.Tables[0].Rows.Count;
            txt_NoOfSplits.Value = Math.Ceiling(noOfRecs / txt_No4Splitting.Value);
        }

        private void txt_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchClick(e);
        }

        private void btn_OpenMainDBLog_Click(object sender, EventArgs e)
        {
            var logPatht = (logPath == null || !DirectoryExists(logPath) ? tmpPath + "\\0_log\\" : logPath);
            logPatht = logPatht + c("dlcm_Split4Pack") + "current_maindbtemp.txt";
            if (DirectoryExists(logPatht))
            {
                StartProcesss(@logPatht, null);
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
        }

        private void btn_OpenLogsFolder_Click(object sender, EventArgs e)
        {
            var fnl = (logPath == null || !DirectoryExists(logPath) ? c("dlcm_TempPath") + "\\0_log" : logPath) + "\\" + c("dlcm_Split4Pack") + "current_maindbtemp.txt";
        }

        private void ContextMenuStrip_RightClick_Opening(object sender, CancelEventArgs e)
        {
            var j = mouseLocation.RowIndex;
            if (j < 0) return;
            ContextMenuMenu_Info.Text = "Info: " + databox.Rows[j].Cells["ID"].Value.ToString()
                + " ," + databox.Rows[j].Cells["Artist"].Value.ToString()
                + " ," + databox.Rows[j].Cells["Album"].Value.ToString()
                + " ," + databox.Rows[j].Cells["Song_Title"].Value.ToString();
        }

        private void ContextMenuMenu_ToTry_Click(object sender, EventArgs e)
        {
            AddtoGrp("toTry");
        }

        private void ContextMenuMenu_Default_Click(object sender, EventArgs e)
        {
            AddtoGrp("Default");

        }

        private void ContextMenuMenu_Playable_Click(object sender, EventArgs e)
        {
            AddtoGrp("Playable");
        }

        private void AddtoGrp(string grp)
        {
            var i = databox.SelectedCells[0].RowIndex;
            var j = mouseLocation.RowIndex;
            var idclicked = databox.Rows[j].Cells["ID"].Value.ToString();


            //Use multi-selected if the case
            var sel = "";
            for (int k = 0; k < databox.SelectedRows.Count; k++)
            {
                if (k > 0) sel += ", ";
                sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
            }

            Groupss = chbx_Group.Text.ToString();
            var cmd = "SELECT * FROM Main ";

            DialogResult result1 = DialogResult.No;
            if (databox.SelectedRows.Count > 1 && !(" " + sel + ",").Contains(" " + idclicked + ","))
                result1 = MessageBox.Show("The CDLC/Song you clicked on, is not in the selected rows. \nDo you want to use Selected population (Yes)(" + sel
                    + ")\n\n or Song(" + idclicked + ") (No)", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes || (databox.SelectedRows.Count > 1 && (sel + ",").Contains(" " + idclicked + ",")))
            {
                cmd += sel.Length > 0 ? "WHERE ID in (" + sel + ")" : txt_ID.Text;
            }
            else cmd += "WHERE ID in (" + idclicked + ")";

            //Read from DB instead of reading the databox ...just because
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(cmd, cnb, cnc);

            var ordno = ""; i = 0; pB_ReadDLCs.Maximum = SongRecord[0].NoRec.ToInt32(); pB_ReadDLCs.Value = 0;

            //get grp id
            DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp + "\"", "", cnb, cnc);//chbx_AllGroups.Items[chbx_AllGroups.SelectedIndex]
            var noOfRec = GetNoRec(dgs, cnb, cnc);
            var ord = noOfRec > 0 ? dgs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";

            foreach (var filez in SongRecord)
            {
                if (filez is null) break;
                DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + filez.ID + "\" AND Groupz =\"" + grp + "\"", cnb, cnc);

                var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                var insertvalues = "\"" + filez.ID + "\", \"" + grp + "\", \"DLC\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"" + ",\"" + ord + "\"";
                InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
            }

            Populate(ref databox, ref Main);
            databox.Visible = false; databox.Refresh(); databox.Visible = true;
        }

        private void ContextMenuMenu_Delete_Click(object sender, EventArgs e)
        {
            btn_Delete_Click(null, null);
        }

        private void ContextMenuMenu_Duplicate_Click(object sender, EventArgs e)
        {
            btn_AssesIfDuplicate_Click(null, null);
        }

        private void btn_RockBandSite_Click(object sender, EventArgs e)
        {
            //try
            //{
            //Process process = Process.Start("https://db.c3universe.com/songs");/*.Replace(link, "https://db.c3universe.com/songs")*/
            StartProcesss("https://db.c3universe.com/songs", null);
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_RockBand"));
            if (!File.Exists(xx))
            {
                ErrorWindow frm = new ErrorWindow("Install C3 Conversion Tools if you want to use it.", c("dlcm_RockBand_www"),
                        "Missing C3 Rockband conversion tools", false, false, true, "", "", "", false); frm.ShowDialog(); return;
            }
            StartProcesss(@xx, null);
            //    Process procesf = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            //}
        }

        private void btn_GatherAttributes_Click(object sender, EventArgs e)
        {
            var cmd = "SELECT * FROM Main"; ;
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(cmd, cnb, cnc);
            pB_ReadDLCs.Maximum = SongRecord[0].NoRec.ToInt32();
            pB_ReadDLCs.Value = 0;
            var tst = "Records to pass through and re-analise for Single, Karaoke, Live , acoustic etc." + SongRecord[0].NoRec.ToInt32(); timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            for (var h = 0; h < SongRecord[0].NoRec.ToInt32(); h++)
            {
                var Is_MultiTrack = SongRecord[h].Is_Multitrack; var MultiTrack_Version = SongRecord[h].MultiTrack_Version;
                var IsLive = SongRecord[h].Is_Live; var LiveDetails = SongRecord[h].Live_Details; var IsAcoustic = SongRecord[h].Is_Acoustic; var IsSingle = SongRecord[h].Is_Single; var IsSoundtrack = SongRecord[h].Is_Soundtrack;
                var IsInstrumental = SongRecord[h].Is_Instrumental; var IsEP = SongRecord[h].Is_EP; var IsUncensored = SongRecord[h].Is_Uncensored; var IsFullAlbum = SongRecord[h].Is_FullAlbum; var IsRemastered = SongRecord[h].Is_Remastered; var InTheWorks = SongRecord[h].IntheWorks;
                var IsKaraoke = SongRecord[h].Is_Karaoke; var IsDemo = SongRecord[h].Is_Demo; var HasFeaturing = SongRecord[h].Has_Featuring; var IsRemix = SongRecord[h].Is_Remix; var IsCover = SongRecord[h].Is_Cover;
                var SongDisplayName = CleanTitle(SongRecord[h].Song_Title); var Artist = SongRecord[h].Artist; var Album = SongRecord[h].Album; var IsMedley = SongRecord[h].Is_Medley; var IsMultiStrings = SongRecord[h].Is_MultiStrings;
                var IsDeluxe = SongRecord[h].Is_Deluxe; var IsGreatestHits = SongRecord[h].Is_GreatestHits; var IsMidi = SongRecord[h].Is_Midi; var IsGameSoundtrack = SongRecord[h].Is_GameSoundtrack;
                ; var IsTVTheme = SongRecord[h].Is_TVTheme; var IsAmateurCover = SongRecord[h].Is_AmateurCover; var IsMetalCover = SongRecord[h].Is_MetalCover; var IsUkulele = SongRecord[h].Is_Ukulele;
                if (Album.IndexOf("AC/DC Live") >= 0)
                    ;

                var ret = GetExtraAttributes(SongRecord[h].Original_FileName, SongDisplayName, SongDisplayName, SongDisplayName, SongRecord[h].Album, SongRecord[h].Author, SongRecord[h].DLC_Name, SongRecord[h].Artist);

                string[] ag = ret.ToString().Split(';');
                Is_MultiTrack = ag[0]; MultiTrack_Version = ag[1]; IsLive = ag[2]; LiveDetails = ag[3]; IsAcoustic = ag[4]; IsSingle = ag[5]; IsSoundtrack = ag[6]; IsInstrumental = ag[7]; IsEP = ag[8]; IsUncensored = ag[9];
                IsFullAlbum = ag[10]; IsRemastered = ag[11]; InTheWorks = ag[12]; IsKaraoke = ag[13]; IsDemo = ag[14]; HasFeaturing = ag[15]; IsRemix = ag[16]; IsCover = ag[17];
                SongDisplayName = ag[18]; Album = ag[19]; IsMedley = ag[20]; IsMultiStrings = ag[21]; IsGreatestHits = ag[22]; IsMedley = ag[23]; IsMidi = ag[24]; IsGameSoundtrack = ag[25];
                IsTVTheme = ag[26]; IsAmateurCover = ag[27]; IsMetalCover = ag[28]; IsUkulele = ag[29];
                if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                //"(No.)", "[No.]"),Regex.Replace(
                //SongDisplayName = ReplaceTxt(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(
                //         SongDisplayName, "(Backing.)", "[Backing.]"), "(Only.)", "[Only.]"),
                //         "(Live.)", "[Live.]"), "(Acoustic.)", "[Acoustic.]"), "Unplugged", "[Unplugged.]"), "Uncensored", "[Uncensored.]"), "(Instrumental.)", "[Instrumental.]"), "(Single.)", "[Single.]")
                //         , "(Remastered.)", "[Remastered.]"), "(FullAlbum.)", "[FullAlbum.]"), "(Full Album.)", "[Full Album.]"), "(Demo.)", "[Demo.]")
                //         , "(Remix.)", "[Remix.]"), "(Karaoke.)", "[Karaoke.]"), "(Featuring.)", "[Featuring.]"), "(Cover.)", "[Cover.]"), "(Feat.)", "[Feat.]"), SongDisplayName, ConfigRepository.Instance()["dlcm_AdditionalManipul103"] == "Yes" ? true : false);

                SongDisplayName = CleanTitleOfWeirdChars(SongDisplayName);
                Artist = CleanTitleOfWeirdChars(Artist);
                Album = CleanTitleOfWeirdChars(Album);

                var changed = "";
                var sel = "UPDATE Main SET ";
                if (SongRecord[h].Artist != Artist) { sel += "Artist=\"" + Artist + "\", Artist_Sort=\"" + Artist + "\", "; changed += "\nArtist=\"" + SongRecord[h].Artist + " -> " + Artist; }
                if (SongRecord[h].Album != Album) { sel += " Album=\"" + Album + "\", Album_Sort=\"" + Album + "\", "; changed += "\nAlbum=\"" + SongRecord[h].Album + " -> " + Album; }
                if (CleanTitle(SongRecord[h].Song_Title) != CleanTitle(SongDisplayName)) { sel += " Song_Title=\"" + CleanTitle(SongDisplayName) + "\",Song_Title_Sort=\"" + SongDisplayName + "\", "; changed += "\nSong_Title=\"" + SongRecord[h].Song_Title + " -> " + SongDisplayName; }
                if (SongRecord[h].Is_Multitrack != "Yes" && Is_MultiTrack == "Yes") { sel += " Is_MultiTrack = \"" + Is_MultiTrack + "\""; changed += "\nIs_MultiTrack=\"" + SongRecord[h].Is_Multitrack + " -> " + Is_MultiTrack; }
                if (SongRecord[h].Is_Live != "Yes" && IsLive == "Yes") { sel += " Is_Live = \"" + IsLive + "\", Live_Details = \"" + LiveDetails + "\", "; changed += "\nIs_Live=\"" + SongRecord[h].Is_Live + " -> " + IsLive; }
                if (SongRecord[h].Is_Acoustic != "Yes" && IsAcoustic == "Yes") { sel += " Is_Acoustic = \"" + IsAcoustic + "\", "; changed += "\nIs_Acoustic=\"" + SongRecord[h].Is_Acoustic + " -> " + IsAcoustic; }
                if (SongRecord[h].Is_Remastered != "Yes" && IsRemastered == "Yes") { sel += " Is_Remastered = \"" + IsRemastered + "\", "; changed += "\nIs_Remastered=\"" + SongRecord[h].Is_Remastered + " -> " + IsRemastered; }
                if (SongRecord[h].Is_Uncensored != "Yes" && IsUncensored == "Yes") { sel += " Is_Uncensored = \"" + IsUncensored + "\", "; changed += "\nIs_Uncensored=\"" + SongRecord[h].Is_Uncensored + " -> " + IsUncensored; }
                if (SongRecord[h].Is_Soundtrack != "Yes" && IsSoundtrack == "Yes") { sel += " Is_Soundtrack = \"" + IsSoundtrack + "\", "; changed += "\nIs_Soundtrack=\"" + SongRecord[h].Is_Soundtrack + " -> " + IsSoundtrack; }
                if (SongRecord[h].Is_FullAlbum != "Yes" && IsFullAlbum == "Yes") { sel += " Is_FullAlbum = \"" + IsFullAlbum + "\", "; changed += "\nIs_FullAlbum =\"" + SongRecord[h].Is_FullAlbum + " -> " + IsFullAlbum; }
                if (SongRecord[h].Is_Karaoke != "Yes" && IsKaraoke == "Yes") { sel += " Is_Karaoke = \"" + IsKaraoke + "\", "; changed += "\nIs_Karaoke=\"" + SongRecord[h].Is_Karaoke + " -> " + IsKaraoke; }
                if (SongRecord[h].Is_Instrumental != "Yes" && IsInstrumental == "Yes") { sel += " Is_Instrumental = \"" + IsInstrumental + "\", "; changed += "\nIsInstrumental=\"" + SongRecord[h].Is_Instrumental + " -> " + IsInstrumental; }
                if (SongRecord[h].Is_Demo != "Yes" && IsDemo == "Yes") { sel += " Is_Demo = \"" + IsDemo + "\", "; changed += "\nIs_Demo=\"" + SongRecord[h].Is_Demo + " -> " + IsDemo; }
                if (SongRecord[h].Is_Remix != "Yes" && IsRemix == "Yes") { sel += " Is_Remix = \"" + IsRemix + "\", "; changed += "\nIs_Remix=\"" + SongRecord[h].Is_Remix + " -> " + IsRemix; }
                if (SongRecord[h].Is_Cover != "Yes" && IsCover == "Yes") { sel += " Is_Cover = \"" + IsCover + "\", "; changed += "\nIs_Cover=\"" + SongRecord[h].Is_Cover + " -> " + IsCover; }
                if (SongRecord[h].Has_Featuring != "Yes" && HasFeaturing == "Yes") { sel += " Has_Featuring = \"" + HasFeaturing + "\", "; changed += "\nHas_Featuring=\"" + SongRecord[h].Has_Featuring + " -> " + HasFeaturing; }
                if (SongRecord[h].Is_EP != "Yes" && IsEP == "Yes") { sel += " Is_EP = \"" + IsEP + "\", "; changed += "\nIs_EP=\"" + SongRecord[h].Is_EP + " -> " + IsEP; }
                if (SongRecord[h].IntheWorks != "Yes" && InTheWorks == "Yes") { sel += " InTheWorks = \"" + InTheWorks + "\", "; changed += "\nIntheWorks=\"" + SongRecord[h].IntheWorks + " -> " + InTheWorks; }
                if (SongRecord[h].Is_Single != "Yes" && IsSingle == "Yes") { sel += " Is_Single = \"" + IsSingle + "\", "; changed += "\nIs_Single=\"" + SongRecord[h].Is_Single + " -> " + IsSingle; }
                if (SongRecord[h].Live_Details != LiveDetails && LiveDetails != "") { sel += " Live_Details  = \"" + LiveDetails + "\", "; changed += "\nLive_Details=\"" + SongRecord[h].Live_Details + " -> " + LiveDetails; }
                if (SongRecord[h].MultiTrack_Version != MultiTrack_Version && MultiTrack_Version != "") { sel += " MultiTrack_Version  = \"" + MultiTrack_Version + "\", "; changed += "\nMultiTrack_Version=\"" + SongRecord[h].MultiTrack_Version + " -> " + MultiTrack_Version; }
                if (SongRecord[h].Is_Medley != IsMedley && IsMedley != "") { sel += " Is_Medley  = \"" + IsMedley + "\", "; changed += "\nIs_Medley=\"" + SongRecord[h].Is_Medley + " -> " + IsMedley; }
                if (SongRecord[h].Is_MultiStrings != IsMultiStrings && IsMultiStrings != "") { sel += " Is_MultiStrings  = \"" + IsMultiStrings + "\", "; changed += "\nIs_MultiStrings=\"" + SongRecord[h].Is_MultiStrings + " -> " + IsMultiStrings; }
                if (SongRecord[h].Is_Deluxe != IsDeluxe && IsDeluxe != "") { sel += " Is_Deluxe  = \"" + IsMedley + "\", "; changed += "\nIs_Deluxe=\"" + SongRecord[h].Is_Deluxe + " -> " + IsDeluxe; }
                if (SongRecord[h].Is_GreatestHits != IsGreatestHits && IsGreatestHits != "") { sel += " Is_GreatestHits  = \"" + IsGreatestHits + "\", "; changed += "\nIs_GreatestHits=\"" + SongRecord[h].Is_GreatestHits + " -> " + IsGreatestHits; }
                if (SongRecord[h].Is_Midi != IsMidi && IsMidi != "") { sel += " Is_Midi  = \"" + IsMidi + "\", "; changed += "\nIs_Midi=\"" + SongRecord[h].Is_Midi + " -> " + IsMidi; }
                if (SongRecord[h].Is_GameSoundtrack != IsGameSoundtrack && IsGameSoundtrack != "") { sel += " Is_GameSoundtrack  = \"" + IsGameSoundtrack + "\", "; changed += "\nIs_GameSoundtrack=\"" + SongRecord[h].Is_GameSoundtrack + " -> " + IsGameSoundtrack; }
                if (SongRecord[h].Is_TVTheme != IsTVTheme && IsTVTheme != "") { sel += " Is_TVTheme  = \"" + IsTVTheme + "\", "; changed += "\nIs_TVTheme=\"" + SongRecord[h].Is_TVTheme + " -> " + IsTVTheme; }
                if (SongRecord[h].Is_AmateurCover != IsAmateurCover && IsAmateurCover != "") { sel += " Is_AmateurCover  = \"" + IsAmateurCover + "\", "; changed += "\nIs_AmateurCover=\"" + SongRecord[h].Is_AmateurCover + " -> " + IsAmateurCover; }
                if (SongRecord[h].Is_MetalCover != IsMetalCover && IsMetalCover != "") { sel += " Is_MetalCover  = \"" + IsMetalCover + "\", "; changed += "\nIs_MetalCover=\"" + SongRecord[h].Is_MetalCover + " -> " + IsMetalCover; }
                if (SongRecord[h].Is_Ukulele != IsUkulele && IsUkulele != "") { sel += " Is_Ukulele  = \"" + IsUkulele + "\", "; changed += "\nIs_Ukulele=\"" + SongRecord[h].Is_Ukulele + " -> " + IsUkulele; }
                sel += " WHERE ID=" + SongRecord[h].ID;

                DataSet ddr = new DataSet();
                if (changed != "")
                {
                    DialogResult rets = MessageBox.Show("FN: " + SongRecord[h].Original_FileName + "\n" + SongDisplayName + "-" + Album + "\nFidings/Songs: " + h + "/" + SongRecord[0].NoRec.ToInt32() + "\n\nFindings of Existing attributes compared(,) with Newly Found value: " + changed + "\n\n. OK with you?"
                        , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (rets != DialogResult.No)
                        ddr = UpdateDB("Main", sel.Replace(", WHERE", " WHERE").Replace(",  WHERE", " WHERE") + ";", cnb, cnc);
                }
            }
        }

        private void btn_MKlink_Click(object sender, EventArgs e)
        {

            try
            {
                var v = c("dlcm_TempPath");
                CreateTempFolderStructure(v, v + "\\0_old", v + "\\0_broken", v + "\\0_duplicate", v + "\\0_dlcpacks", c("dlcm_RocksmithDLCPath"),
                       v + "\\0_repacked", v + "\\0_repacked\\XBOX360", v + "\\0_repacked\\PC", v + "\\0_repacked\\MAC", v + "\\0_repacked\\PS3"
                       , c("dlcm_LogPath") == "" ? v + "\\0_log" : c("dlcm_LogPath"), v + "\\0_albumCovers", v + "\\0_log", v + "\\0_archive"
                       , v + "\\0_data", v + "\\0_temp", c("dlcm_RocksmithDLCPath"), v + "\\0_intheworks", v + "\\0_export");
            }
            catch (Exception ex)
            {
                var tsst = "Error at folder create ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }

            CreateMKLinks();

        }

        private void btn_PitchShift_Click(object sender, EventArgs e)
        {
            //Show Summary window
            string summary = toolTip1.GetToolTip(btn_PitchShift);
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Info about on how to Drop Down using Digitect DROP pedal", false, false, true, "", "", "", false);
            frm9.ShowDialog();
        }

        private void cmb_Filter_DropDown(object sender, EventArgs e)
        {
            ConfigRepository.Instance()["dlcm_FilterPrevious"] = cmb_Filter.Text;
        }

        private void btn_RunExternalProgramsChecks_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string encryptResult = UtilitiesFunctions.EncryptSongsPSARC();
            DialogResult result1 = MessageBox.Show(encryptResult, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void unBrokenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chbx_IntheWorks_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbx_Preview_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_Artist_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var t = 0;
            t++;
            ReadGameLibrary();
        }

        private void ContextMenuMenu_Groups_Click(object sender, EventArgs e)
        {

        }

        private void soundtrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsSoundtrack, "Is_SoundTrack", "Yes", "", "", "");
        }
        //public void UpdateAttributes(string attrib)
        //{
        //    var j = mouseLocation.RowIndex;

        //    var i = databox.SelectedCells[0].RowIndex;

        //    //Use multi-select if the case
        //    var sel = "SELECT * FROM Main WHERE ID IN ("; int k = 0;
        //    for (k = 0; k < databox.SelectedRows.Count; k++)
        //    {
        //        if (k >= 1) sel += ", ";
        //        sel += databox.SelectedRows[k].Cells["ID"].Value.ToString();
        //    }
        //    sel += ")";
        //    if (k == 1 && i != j) sel = databox.Rows[j].Cells["ID"].Value.ToString();

        //    var SongRecord = UtilitiesFunctions.GetRecord_s(sel, cnb, cnc);/*.Replace("*", "ID")*/
        //    var norows = SongRecord[0].NoRec.ToInt32();

        //    if (i == j) chbx_Beta.Checked = chbx_Beta.Checked ? false : true;
        //    else
        //    {
        //        var updatecmd = "UPDATE Main SET " + attrib + "=\"Yes\" WHERE ID in (" + sel.Replace("*", "ID") + ")";// AND Is_Beta=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
        //        UpdateDB("Main", updatecmd, cnb, cnc);

        //        // updatecmd = "UPDATE Main SET Is_Beta=\"No\" WHERE ID in (" + sel + ") AND Is_Beta=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
        //        //UpdateDB("Main", updatecmd, cnb, cnc);
        //    }
        //    Populate(ref databox, ref Main);
        //    databox.Refresh();
        //}

        private void ContextMenuAttrib_UnCensored_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsUncensored, "Is_Uncensored", "Yes", "", "", "");
        }

        private void ContextMenuAttrib_Manipulated_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsImprovedWithDM, "Is_Manipulated", "Yes", "", "", "");
        }

        private void ContextMenuAttrib_FullAlbum_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsFullAlbum, "Is_FullAlbum", "Yes", "", "", "");
        }

        private void instrumentalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsInstrumental, "Is_Instrumental", "Yes", "", "", "");
        }

        private void featuringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_HasFeaturing, "Has_Featuring", "Yes", "", "", "");
        }

        private void acousticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsAcoustic, "is_Acoustic", "Yes", "", "", "");
        }

        private void karaokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsKaraoke, "Is_Karaoke", "Yes", "", "", "");
        }

        private void inTheWorksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsInTheWorks, "Is_IntheWorks", "Yes", "", "", "");
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsSingle, "Is_Single", "Yes", "", "", "");
        }

        private void reMasteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsRemastered, "Is_Remastered", "Yes", "", "", "");
        }

        private void demoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsDemo, "Is_Demo", "Yes", "", "", "");
        }

        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsLive, "Is_Live", "Yes", "", "", "");
        }

        private void medleyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsMedley, "Is_Medley", "Yes", "", "", "");
        }

        private void ePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsEP, "Is_EP", "Yes", "", "", "");
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_HasAuthor, "Has_Author", "Yes", "", "", "");
        }

        private void showLightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_HasShowLights, "Has_ShowLights", "Yes", "", "", "");
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_HasSections, "Has_Sections", "Yes", "", "", "");
        }

        private void coverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_HasCover, "Has_Cover", "Yes", "", "", "");
        }

        private void trackNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_HasTrackNo, "Has_TrackNo", "Yes", "", "", "");
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_HasPreview, "Has_Preview", "Yes", "", "", "");
        }

        private void remixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsRemix, "Is_Remix", "Yes", "", "", "");
        }

        private void multiStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsMultiStrings, "Is_MultiStrings", "Yes", "", "", "");
        }

        private void btn_MODStarter_Click(object sender, EventArgs e)
        {
            var paath = c("dlcm_RS2014-Mod-Installer");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "RS2014-Mod-Installer.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Mods .", c("dlcm_RS2014-Mod-Installer\""), "Missing RS2014 Mod Integrator!", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }

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

        private void button4_Click_2(object sender, EventArgs e)
        {
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_Database.NET"));
            if (!File.Exists(xx))
            {
                ErrorWindow frm = new ErrorWindow("Install Database .NET", c("dlcm_Database.NET_www"),
                        "Install Database .NET!", false, false, true, "", "", "", false); frm.ShowDialog(); return;
            }
            StartProcesss(@xx, null);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            StartProcesss(c(c("dlcm_Access" + c("dlcm_AccessDLLVersion")) + "_www"), null);
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_Access" + c("dlcm_AccessDLLVersion")));
            if (!File.Exists(xx))
            {
                ErrorWindow frm = new ErrorWindow("Install Microsoft Access driver.", c(c("dlcm_Access" + c("dlcm_AccessDLLVersion")) + "_www"),
                        "", false, false, true, "", "", "", false); frm.ShowDialog(); return;
            }
            StartProcesss(@xx, null);
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_sqliteodbc"));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install OLEDB SQL Lite 3 driver.", c("dlcm_sqliteodbc_www"), "Install OLEDB SQL Lite 3 driver!", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_Access" + c("dlcm_AccessDLLVersion")));
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Microsoft Access driver..", c(c("dlcm_Access" + c("dlcm_AccessDLLVersion")) + "_www"), "Install Microsoft Access driver!", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Microsoft Access driver and comamnd line.", c("dlcm_sqliteodbc_www"), c("dlcm_sqliteodbc_www"), false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            StartProcesss(@xx, null);
        }

        private void btn_ADD2HOT_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
            {
                var t = chbx_AllGroups.Items[j].ToString().Substring(0, chbx_AllGroups.Items[j].ToString().IndexOf("}") + 1);
                if (t.Contains(c("dlcm_HotGrp")) && !chbx_AllGroups.GetItemChecked(j))
                {
                    chbx_AllGroups.SetItemChecked(j, true);

                    GroupChanged = true;
                    var dtt = System.DateTime.Now.ToString("yyyyMMdd HHmmssfff");

                    var updatecmd = "UPDATE Groups SET Date_Added=\"" + dtt + "\" WHERE Type=\"DLC\" AND CDLC_ID in (\"" + txt_ID.Text + "\") AND Groupz=\"" + c("dlcm_HotGrp") + "\"";// AND Is_Beta=\"Yes\"";// databox.Rows[j].Cells["ID"].Value.ToString()
                    UpdateDB("Groups", updatecmd, cnb, cnc);

                    return;
                    //if (chbx_AllGroups.GetItemChecked(j) && grp.Tables[0].Rows[k].ItemArray[1].ToString() == chbx_AllGroups.Items[j].ToString())

                    //  if (!chbx_AllGroups.GetItemChecked(j) &&
                    //chbx_AllGroups.Items[j].ToString().Substring(0, chbx_AllGroups.Items[j].ToString().IndexOf("}") + 1))

                }
            }
            ;
            //

            ////var cmd = "SELECT * Groups WHERE Groupz=\"Hot (monthly)\"";
            ////DataSet dz; dz = SelectFromDB("Arrangements", cmd, "", cnb, cnc);

            ////foreach (DataRow myRow in dz.Tables[0].Rows)
            ////{
            ////    var idd = myRow["ID"].ToString();
            ////    //for (var j = 0; j < dz.Tables[0].Rows.Count; j++)

            ////        //foreach (DataColumn myColumn in dt.Tables[0].Columns)
            ////        //{
            ////        //var fileName = dz.Tables[0].Rows[j].ItemArray[0].ToString();
            ////}

            //GroupChanged = true;
        }

        private void bnt_DCLBuilder_Click(object sender, EventArgs e)
        {
            //var xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Program Files\\SQLite ODBC Driver for Win64\\sqlite3.exe");
            //if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install Microsoft Access driver and comamnd line.", c("dlcm_sqliteodbc_www"), c("dlcm_sqliteodbc_www"), false, false, true, "", "", "", false); frm1.ShowDialog(); return; }
            var xx = Path.Combine(AppWD, c("dlcm_DLCBuilder"));
            if (!File.Exists(xx))
            {
                ErrorWindow frm = new ErrorWindow("Install DCL Builder.", c("dlcm_DLCBuilder_www"),
                        "Install DLC Builder!", false, false, true, "", "", "", false); frm.ShowDialog(); return;
            }
            StartProcesss(@xx, null);
        }
        private void btn_AutoCover_Click(object sender, EventArgs e)
        {
            string ids = GetSameAlbum(txt_Artist.Text, txt_Album_Year.Text, txt_Album.Text, txt_ID.Text, true);
            int t = SetMultiCover(ids);

        }

        private void btn_restoreAlbumArt_Click(object sender, EventArgs e)
        {
            txt_AlbumArtPath.Text = txt_Album_OrigArtPath3.Text;
        }

        private void deluxeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsDeluxe, "Is_Deluxe", "Yes", "", "", "");
        }

        private void greatestHitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_GreatestHits", "Yes", "", "", "");
        }

        private void btn_Distribute_Evenly_Click(object sender, EventArgs e)
        {
            var i = -1; string BasedOn_CF = ""; string EoFPath = "";
            if (databox.SelectedCells.Count > 0)
            {
                i = databox.SelectedCells[0].RowIndex;
                BasedOn_CF = databox.Rows[i].Cells["BasedOn_GP"].Value.ToString();
                EoFPath = databox.Rows[i].Cells["BasedOn_GP"].Value.ToString();
            }
            string track = cmb_Tracks.Text.ToString();
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            if (cmb_Tracks.Text.ToString() == "")
            {
                //MessageBox.Show("Chose a Track first!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                DialogResult result1 = DialogResult.Cancel;
                if ((!filezPathDefault || filezPath == "") && i > 0) result1 = MessageBox.Show("Chose a XML (Yes) \n or \n Chose a track (No)\nor\n "
                    + (filezPath == "" ? "Chose new" : "Reuse") + Path.GetFileName(filezPath) + " XML and don't ask again (if track selector is empty) till program restart (Cancel)...", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                else result1 = DialogResult.Yes;

                openFileDialog1.Title = "Select the XML you want to distribute notes around";
                if (result1 == DialogResult.Yes)
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        filezPath = openFileDialog1.FileName;
                        EoFPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        BasedOn_CF = "";
                        track = null;
                    }
                    else return;
                }
                if (result1 == DialogResult.Cancel)
                {
                    filezPathDefault = true;
                    if (filezPath == "") if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) filezPath = openFileDialog1.FileName;
                    EoFPath = Path.GetDirectoryName(filezPath);
                    BasedOn_CF = "";
                    track = null;
                }
                else if (result1 == DialogResult.No)
                {
                    filezPath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
                    return;
                }
            }


            //var old = num_Lyrics.Value;
            //if (cmb_Tracks.Text.ToString() != "")
            //{
            DistribXMLNotes frm = new DistribXMLNotes(track, filezPath, txt_Platform.Text, arrangoff, BasedOn_CF, EoFPath, cnb, cnc);
            frm.ShowDialog();
            if (frm.perc_time_betw_notes != not_t) not_t = frm.perc_time_betw_notes;
            if (not_t.Split(';')[0] == "100")
            {
                string[] args = (not_t).ToString().Split(';');
                not_p = float.Parse(args[1]);
                num_Lyrics.Value = decimal.Parse(args[2]);
                not_t = "0";
                bth_ShiftVocalNotes_Click(null, null);
            }
            else if (not_t != "0") bth_ShiftVocalNotes_Click(null, null);
            try
            {
                if (frm.perc_time_betw_notes.Split(';')[6] == "apply") btn_Distribute_Evenly_Click(null, null);
            }
            catch (Exception es)
            {
            }
        }

        private void gameSoundTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_GameSoundtrack", "Yes", "", "", "");
        }

        private void midiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_Midi", "Yes", "", "", "");
        }

        private void tVThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_TVTheme", "Yes", "", "", "");
        }

        private void amateurCoverToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsAmateurCover, "Is_AmateurCover", "Yes", "", "", "");
        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();
            eof(true, filePath);
        }

        private void btnRefreshOld_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            if (File.Exists(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text))
            {
                if (calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text) != long.Parse(databox.Rows[i].Cells["File_Size"].Value.ToString()))
                {
                    var cmd = "SELECT * FROM Main where Original_FileName=\"" + txt_OldPath.Text + "\" and ID <> " + txt_ID.Text + ";";
                    MainDBfields[] SongRecord = new MainDBfields[20000];
                    SongRecord = GetRecord_s(cmd, cnb, cnc);
                    //pB_ReadDLCs.Maximum = SongRecord[0].NoRec.ToInt32();
                    var tst = "Records to pass through and re-analise for Single, Karaoke, Live , acoustic etc." + SongRecord[0].NoRec.ToInt32(); timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var list_dupli_details = "";
                    for (var h = 0; h < SongRecord[0].NoRec.ToInt32(); h++) list_dupli_details += SongRecord[h].Original_FileName + " - " + SongRecord[h].File_Size + " - " + SongRecord[h].ID;// + " - ";

                    var result1 = MessageBox.Show("Looks like an existing file is already in '0_old' has the diff file-size than at import.\n(New:" +
                    calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text).ToString() + " - old:" + databox.Rows[i].Cells["File_Size"].Value.ToString()
                    + ((list_dupli_details != "") ? ")\n\nand other same file names are in the DB" + list_dupli_details : ")\n\nno other CDLC has this filename as imported song.")
                    + "\n\nGet actual file and reimport with same refresh button!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_OldPath.Text = txt_OldPath.Text + ".new";
                }
                else
                {
                    if (!chbx_Avail_Old.Checked)
                    {
                        chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true;
                        var cmdupd = "UPDATE Main Set Available_Old=\"Yes\" WHERE ID=" + txt_ID.Text;
                        DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        MessageBox.Show("Old exists in the OLD folder, FLAG updated.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Old exists in the OLD folder!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

                System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(c("dlcm_TempPath") + "\\0_archive\\");
                var t = "";
                foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                    if (Path.GetFileNameWithoutExtension(file.Name) == Path.GetFileNameWithoutExtension(txt_OldPath.Text))
                        t += file.Length + "," + file.FullName + ";\n";
                if (t.Length > 0)
                {
                    var result1 = MessageBox.Show("Which file to be copied from below ARCHIVE found list: \n\nNew:" + t + "\n\nold: " + databox.Rows[i].Cells["File_Size"].Value.ToString() + " - " + txt_OldPath.Text + "\n\n (Yes=1)"
                                                , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result1 == DialogResult.Yes) txt_OldPath.Text = Path.GetFileName(CopyMoveFileSafely(t.Split(',')[1].Replace(";", "").Replace("\n", ""), c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", ""), true, null, false));
                }

                t = "";
                System.IO.DirectoryInfo downloadedMessageInfo3 = new DirectoryInfo(c("dlcm_TempPath") + "\\0_duplicate\\");
                if (!File.Exists(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", "")))
                    foreach (FileInfo file in downloadedMessageInfo3.GetFiles())
                        if (Path.GetFileNameWithoutExtension(file.Name) == Path.GetFileNameWithoutExtension(txt_OldPath.Text))
                            t += file.Length + "," + file.FullName + ";\n";
                if (t.Length > 0)
                {
                    var result1 = MessageBox.Show("Which file to be copied from below DUPLICATE found list: \n\nNew:" + t + "\n\nold: " + databox.Rows[i].Cells["File_Size"].Value.ToString() + " - " + txt_OldPath.Text + "\n\n (Yes=1)"
                                                , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result1 == DialogResult.Yes) txt_OldPath.Text = Path.GetFileName(CopyMoveFileSafely(t.Split(',')[1].Replace(";", "").Replace("\n", ""), c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", ""), true, null, false));
                }

                if (File.Exists(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", "")))
                {
                    txt_OldPath.Text = txt_OldPath.Text.Replace(".new", "");//txt_OldPath.Text = CopyMoveFileSafely(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text, c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", ""), true, null, false);
                    databox.Rows[i].Cells["File_Size"].Value = calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text);
                    databox.Rows[i].Cells["File_Hash"].Value = GetHash(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text);
                    chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true;
                    var cmdupd = "UPDATE Main Set Available_Old=\"Yes\", File_Size=\"" + calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text) + "\", File_Hash=\"" + GetHash(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text)
                                + "\" WHERE ID=" + txt_ID.Text; DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                    MessageBox.Show("Old added in the OLD folder, FLAG updated.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txt_OldPath.Text = Path.GetFileName(CopyMoveFileSafely(openFileDialog1.FileName, c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text.Replace(".new", ""), true, null, false));
                    databox.Rows[i].Cells["File_Size"].Value = calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text);
                    databox.Rows[i].Cells["File_Hash"].Value = GetHash(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text);
                    chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true;
                    var cmdupd = "UPDATE Main Set Available_Old=\"Yes\", File_Size=\"" + calc_FileSize(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text) + "\", File_Hash=\"" + GetHash(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text)
                                + "\" WHERE ID=" + txt_ID.Text; DataSet dhs = new DataSet(); dhs = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                    MessageBox.Show("Old added in the OLD folder, FLAG updated.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (File.Exists(c("dlcm_TempPath") + "\\0_old\\" + txt_OldPath.Text))
                {
                    chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true;
                    MessageBox.Show("Old added in the OLD folder!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("nothing Done!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            SaveRecord();
        }
        static long calc_FileSize(string s)
        {
            //calc file size
            System.IO.FileInfo fi = null;
            long f = 0;
            try
            {
                fi = new System.IO.FileInfo(s);
                f = fi.Length;
            }
            catch (Exception ex)
            {
                var tsst = "Erro at file cal ..." + ex; var timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
            return f;
        }

        private void btn_StemRolller_Click(object sender, EventArgs e)
        {
            var paath = c("dlcm_StemRoller");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "stemroller-2.0.2-win-cuda\\StemRoller.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install StemRoller if you want to use it.", c("dlcm_StemRoller_www"), "Missing Stemroller", false, false, true, "", "", "", false); frm1.ShowDialog(); return; }

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

        private void btn_BasedOn_GP_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_BasedOn_GP.Text);
        }

        private void btn_BasedOn_Youtube_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_BasedOn_Youtube.Text);
        }

        private void btn_Like_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_BasedOn_CF.Text);
        }

        private void btn_Followers_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_BasedOn_Tabs.Text);
        }


        //No Bass
        //No Lead
        //No Rhythm
        //No Vocal
        //(No Guitars)
        //Only Bass
        //Only Lead
        //Only Rhythm
        //Only Drums
        //Only Vocal
        //(Only BackTrack)
        private void btn_AddAudioaMultitrack_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                var audio = Directory.GetFiles(temppath, "*.wav", System.IO.SearchOption.AllDirectories);

                foreach (var wav in audio)
                {
                    var v = "";
                    if (wav.Contains("bass"))
                        v = "Only Bass";
                    else if (wav.Contains("drums"))
                        v = "Only Drums";
                    else if (wav.Contains("instrumental"))
                        v = "(Only BackTrack)";
                    else if (wav.Contains("other"))
                        v = "(No Drums No Vocal)";
                    else if (wav.Contains("vocals"))
                        v = "Only Vocal";
                    else MessageBox.Show("track unidentified !!\n\nplease add: bass, drums, instrumental, other, vocals to title, then reimport.\n\n" + wav);
                    if (v != "")
                    {
                        DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"AudioAlternative\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Comments =\"" + wav + "\"", cnb, cnc);
                        var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                        var insertvalues = "\"" + txt_ID.Text + "\", \"" + v + "\", \"AudioAlternative\", \"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + wav + "\"";
                        InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        if (!Directory.Exists(txt_SongFolder.Text + "\\multitracks\\")) Directory.CreateDirectory(txt_SongFolder.Text + "\\multitracks\\");
                        int audioQuality = 4;
                        Wwise.Wav2Wem(wav, wav.Replace(".wav", ".wem"), audioQuality);
                        File.Copy(wav.Replace(".wav", ".wem"), txt_SongFolder.Text + "\\multitracks\\" + Path.GetFileName(wav.Replace(".wav", ".wem")), true);
                        UtilitiesFunctions.Wav2Ogg(wav, wav.Replace(".wav", ".ogg"), audioQuality); // 4
                        File.Copy(wav.Replace(".wav", ".ogg"), txt_SongFolder.Text + "\\multitracks\\" + Path.GetFileName(wav.Replace(".wav", ".ogg")), true);
                        chbx_MultiTrack.Checked = true;
                    }
                }
            }
        }

        private void btn_ApplyMultiFilters_Click(object sender, EventArgs e)
        {
            var grp = "";
            if (lbGroups.SelectedItems.Count < 1) MessageBox.Show("Please select multiple Values using SHIFT/CTRL");
            for (var i = 0; i < lbGroups.SelectedItems.Count; i++)
                grp += lbGroups.SelectedItems[i].ToString().Replace("Group ", "") + ";";
            grp = grp.Substring(0, grp.Length - 1);

            var grpexist = false;
            for (var i = 0; i < lbGroups.SelectedItems.Count; i++)
                if (lbGroups.SelectedItems[i].ToString() == "MultiSelect[" + grp + "]") grpexist = true;
            if (!grpexist) cmb_Filter.Items.Add("MultiSelect[" + grp + "]");

            cmb_Filter.Text = "MultiSelect[" + grp + "]";
            //RefreshSelectedStat(rbtn_Population_PackNO.Checked);
        }

        private void btn_Debug2_Click(object sender, EventArgs e)
        {
            GetMEtaBeforePacking(0, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd, chbx_Beta.Checked, Groupss, SearchFields);
        }

        private void lbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_ApplyMultiFilters.Text = "Apply (" + lbGroups.SelectedItems.Count.ToString() + ") Filters";
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            txt_FilesMissingIssues.Text = "";
            chbx_Broken2.Checked = false;
        }

        private void metalCoverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_MetalCover", "Yes", "", "", "");
        }

        private void ukuleleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyricghtclick(chbx_A_IsGreatestHits, "Is_Ukulele", "Yes", "", "", "");
        }

        private void btn_RebuildIndivFields_Click(object sender, EventArgs e)
        {
            var cmd = "SELECT * FROM Main"; ;
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(cmd, cnb, cnc);
            pB_ReadDLCs.Maximum = SongRecord[0].NoRec.ToInt32();
            pB_ReadDLCs.Value = 0;
            var tst = "Records to pass through to rebuild some individual atributes" + SongRecord[0].NoRec.ToInt32(); timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.Maximum = SongRecord[0].NoRec.ToInt32();
            tabControl.SelectedIndex = 0;
            for (var h = 0; h < SongRecord[0].NoRec.ToInt32(); h++)
            {
                pB_ReadDLCs.Value++;
                //var Is_MultiTrack = SongRecord[h].Is_Multitrack; var MultiTrack_Version = SongRecord[h].MultiTrack_Version;
                var changed = "";
                var sel = "UPDATE Main SET ";
                //if (SongRecord[h].Artist != Artist) { sel += "Artist=\"" + Artist + "\", Artist_Sort=\"" + Artist + "\", "; changed += "\nArtist=\"" + SongRecord[h].Artist + " -> " + Artist; }


                //check Arrangements
                var sect = "";
                cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + SongRecord[h].ID + GetArrOfficSQLTxt(arrangoff);
                DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", cmd, "", cnb, cnc);

                var noOfArr = GetNoRec(dss, cnb, cnc);
                for (var k = 0; k < noOfArr; k++)
                {
                    var arr = "UPDATE Arrangements SET ";
                    var ms1 = dss.Tables[0].Rows[k].ItemArray[4].ToString(); //JSONFilePath
                    var ms2 = dss.Tables[0].Rows[k].ItemArray[5].ToString();//XMLFilePath
                    var ms3 = dss.Tables[0].Rows[k].ItemArray[26].ToString(); //XMLFileName
                    var ID = dss.Tables[0].Rows[k].ItemArray[0].ToString();
                    if (chbx_RebuildField.Text.Contains("Sections")) sect = GetSections(ms2);
                    if (sect != "" && !sel.Contains("Has_Sections") && chbx_RebuildField.Text.Contains("Sections")) sel += "Has_Sections=\"Yes\", ";
                    if (sect != "" && !arr.Contains("Has_Sections") && chbx_RebuildField.Text.Contains("Sections")) arr += "Sections=\"Yes\", ";
                    if (!sel.Contains("A440TunningFrecv") && chbx_RebuildField.Text.Contains("A440TunningFrecv") && chbx_RebuildField.Text.Contains("(Main)"))
                        sel += "A440TunningFrecv=\"" + GetTuningFrecv(ms2, false) + "\", ";
                    if (!arr.Contains("A440TunningFrecv") && chbx_RebuildField.Text.Contains("A440TunningFrecv") && chbx_RebuildField.Text.Contains("(Arrangements)"))
                        arr += "A440TunningFrecv=\"" + GetTuningFrecv(ms2, true) + "\", ";
                    //if ((arg.TuningPitch.Equals(440.0) ? "440Hz" : "NonStnd") != A440TunningFrecv && A440TunningFrecv != "") A440TunningFrecv = "NonStnd";
                    if (chbx_RebuildField.Text.Contains("(Arrangements)") && arr.Contains("="))
                    {
                        arr += " WHERE ID=" + ID;
                        DataSet dgr = new DataSet();
                        dgr = UpdateDB("Arrangements", arr.Replace(", WHERE", " WHERE").Replace(",  WHERE", " WHERE") + ";", cnb, cnc);
                        tst = SongRecord[h].ID + "/" + h + "/" + SongRecord[0].NoRec.ToInt32() + " - " + ms2; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    }
                }

                if (chbx_RebuildField.Text.Contains("(Main)") && sel.Contains("="))
                {
                    sel += " WHERE ID=" + SongRecord[h].ID;
                    DataSet ddr = new DataSet();
                    ddr = UpdateDB("Main", sel.Replace(", WHERE", " WHERE").Replace(",  WHERE", " WHERE") + ";", cnb, cnc);
                    tst = SongRecord[h].ID + "/" + h + "/" + SongRecord[0].NoRec.ToInt32() + " - " + SongRecord[h].Artist + " - " + SongRecord[h].Song_Title; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                //else if (chbx_RebuildField.Text.Contains("(Arrangements)")) ddr = UpdateDB("Arrangements", arr.Replace(", WHERE", " WHERE").Replace(",  WHERE", " WHERE") + ";", cnb, cnc);
                //}
            }
        }

        private void btn_JavaSite_Click(object sender, EventArgs e)
        {
            var xx = "";
            DialogResult result3 = MessageBox.Show("1. (Yes) Oracle Java 32/64bit x86 \n 2. (No) M$ OpenJava 64bit ARM", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result3 == DialogResult.Yes) xx = "https://www.java.com/en/download/manual.jsp";
            else xx = "https://docs.microsoft.com/en-us/java/openjdk/download)";
            StartProcesss(@xx, null);
        }

        private void btn_AudioConvSite_Click(object sender, EventArgs e)
        {
            var xx = "https://www.audiokinetic.com/download/";
            StartProcesss(@xx, null);
        }
    }
}