using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using HtmlAgilityPack;
//bcapi
using System.Data.OleDb;
//using RocksmithToolkitGUI.OggConverter;//convert ogg to wem
using RocksmithToolkitLib.Ogg;//convert ogg to wem
using RocksmithToolkitLib.XML; //For xml read library
using RocksmithToolkitLib.Extensions;
using System.IO;
using Ookii.Dialogs; //cue text
using System.Net; //4ftp
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToolkitLib;//4REPACKING
using System.Collections.Specialized;//webparsing
using System.Security.Cryptography; //For File hash
using RocksmithToolkitLib.DLCPackage.AggregateGraph;
using RocksmithToTabLib;//for psarc browser
using System.Drawing;
//using Newtonsoft.Json;
//using SpotifyAPI.Web.Enums;
//using SpotifyAPI.Web;
//using SpotifyAPI.Web.Models;

using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.XmlRepository;
using System.Text.RegularExpressions;//regex
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
//using ScrapySharp.Network;
//using ScrapySharp.Html.Forms;
//using ScrapySharp.Extensions;
//using ScrapySharp.Html;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using RocksmithToolkitLib.Sng2014HSL;

using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using Image = System.Drawing.Image;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
//using static System.Collections.Generic.Dictionary<TKey, TValue>;
using System.Collections;
using RocksmithToolkitLib.DLCPackage.Manifest2014.Tone;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class MainDB : Form
    {
        public MainDB(string txt_DBFolder, string txt_TempPath, bool updateDB, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb)
        {
            InitializeComponent();
            DB_Path = txt_DBFolder;// + "\\Files.accdb";
            TempPath = txt_TempPath;            
            RocksmithDLCPath = txt_RocksmithDLCPath;
            updateDBb = updateDB;
            AllowEncriptb = AllowEncript;
            AllowORIGDeleteb = AllowORIGDelete;
            cnb = cnnb;
            // Generate package worker
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwRGenerate.WorkerReportsProgress = true;

        }
        //OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=DLCManager\\AccessDB.accdb");

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        public BackgroundWorker bwRGenerate = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        public static BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        private BackgroundWorker bwConvert;
        public string convdone;
        private StringBuilder errorsFound;
        public Platform SourcePlatform { get; set; }
        public Platform TargetPlatform { get; set; }
        public string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        public bool SaveOK = true;
        public bool SearchON = false;
        public bool SearchExit = false;
        public bool AddPreview = false;
        string Groupss = "";
        DLCPackageData data;
        static string debug = "";
        public string netstatus = ConfigRepository.Instance()["dlcm_netstatus"];

        public static SpotifyWebAPI _spotify;
        public string _trackno;
        public string _year;
        public static PrivateProfile _profile;
        public List<FullTrack> _savedTracks;
        public List<SimplePlaylist> _playlists;
        public string Archive_Path = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_archive";

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public bool GroupChanged = false;
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        public string SearchFields = "";
        public bool updateDBb = false;
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        public OleDbConnection cnb;
        public string GetTrkTxt = "";
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        //internal static string MyAppWD = AppWD; //when removing DDC

        DateTime timestamp;
        string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
        string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];

        private void MainDB_Load(object sender, EventArgs e)
        {
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");

            // Clean temp log
            if (File.Exists((logPath == null || !Directory.Exists(logPath) ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : logPath) + "\\" + "current_temp.txt")) File.Copy((logPath == null || !Directory.Exists(logPath) ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : logPath) + "\\" + "current_temp.txt", (logPath == null || !Directory.Exists(logPath) ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : logPath) + "\\" + "current_temp_" + startT + ".txt");
            var fnl = (logPath == null || !Directory.Exists(logPath) ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : logPath) + "\\" + "current_temp.txt";
            if (File.Exists(fnl))
            {
                FileStream sw = File.Open(fnl, FileMode.Create);
                //StreamWriter sw = new StreamWriter(File.OpenWrite(fnl));
                //sw.Write("Some stuff here");
                sw.Dispose();
            }
            txt_SpotifyStatus.Text = netstatus;


            var tst = "Starting... " + startT; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //DataAccess da = new DataAccess();
            //btn_Copy_old.Text = char.ConvertFromUtf32(8595);
            btn_Copy_old.Enabled = true;
            SearchFields = "ID, Artist, Song_Title, Track_No, Album, Album_Year, Author, Version, Import_Date, Is_Original, Selected, Tunning, Bass_Picking, Is_Beta, Platform, Has_DD, Bass_Has_DD," +
                " Is_Alternate, Is_Multitrack, Is_Live, Is_Acoustic, Is_Broken, MultiTrack_Version, Alternate_Version_No, Live_Details, Groups, Rating, Description, PreviewTime, PreviewLenght, " +
                "Song_Lenght, Comments, FilesMissingIssues, DLC_AppID, DLC_Name, Artist_Sort, Song_Title_Sort, AverageTempo, Volume, Preview_Volume, SignatureType, ToolkitVersion, AlbumArtPath, AudioPath, " +
                "audioPreviewPath, OggPath, oggPreviewPath, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, File_Size, Current_FileName, Original_FileName, Import_Path, Folder_Name, File_Hash, Original_File_Hash," +
                " Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Bonus_Arrangement, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_Version, Has_Author, Has_Track_No, File_Creation_Date," +
                " Has_Been_Corrected, Pack, Available_Old, Available_Duplicate, Tones, Keep_BassDD, Keep_DD, Keep_Original, Original, YouTube_Link, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, CustomsForge_Link," +
                " CustomsForge_Like, CustomsForge_ReleaseNotes, UniqueDLCName, Artist_ShortName, Album_ShortName, DLC, Is_OLD, Remote_Path, audioBitrate, audioSampleRate, Top10, Has_Other_Officials," +
                " Spotify_Song_ID, Spotify_Artist_ID, Spotify_Album_ID, Spotify_Album_URL, Audio_OrigHash, Audio_OrigPreviewHash, AlbumArt_OrigHash, Duplicate_Of";
            SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY Artist, Album_Year, Album, Track_No, Song_Title;";
            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            tst = "Stop populating... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Defaults
            chbx_PreSavedFTP.Text = ConfigRepository.Instance()["dlcm_FTP"];
            txt_FTPPath.Text = chbx_PreSavedFTP.Text == "EU" ? ConfigRepository.Instance()["dlcm_FTP1"] : ConfigRepository.Instance()["dlcm_FTP2"];
            txt_PreviewStart.Value.AddMinutes(10);
            if (ConfigRepository.Instance()["dlcm_RemoveBassDD"] == "Yes") chbx_RemoveBassDD.Checked = true;
            else chbx_RemoveBassDD.Checked = false;
            if (ConfigRepository.Instance()["dlcm_UniqueID"] == "Yes") chbx_UniqueID.Checked = true;
            else chbx_UniqueID.Checked = false;
            //For the moment don't save these statuses
            //if (ConfigRepository.Instance()["dlcm_DupliGTrack"] == "Yes") chbx_DupliGTrack.Checked = true;
            //else chbx_DupliGTrack.Checked = false;
            //if (ConfigRepository.Instance()["dlcm_CopyOld"] == "Yes") chbx_CopyOld.Checked = true;
            //else chbx_CopyOld.Checked = false;
            //chbx_Last_Packed.Checked = ConfigRepository.Instance()["dlcm_Last_Packed"] == "Yes" ? true : false;

            if (ConfigRepository.Instance()["dlcm_andCopy"] == "Yes") chbx_Copy.Checked = true;
            else chbx_Copy.Checked = false;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            chbx_Replace.Checked = ConfigRepository.Instance()["dlcm_Replace"] == "Yes" ? true : false;
            tst = "Stop reading config data... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            // Loads Groups in chbx_AllGroups Filter box cmb_Filter //Create Groups list Dropbox
            var norec = 0;
            DataSet dsn = new DataSet(); dsn = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type =\"DLC\";", "", cnb);
            norec = dsn.Tables.Count < 1 ? 0 : dsn.Tables[0].Rows.Count;

            if (norec > 0)
                for (int j = 0; j < norec; j++)
                    cmb_Filter.Items.Add("Group " + dsn.Tables[0].Rows[j][0].ToString());//add items

            chbx_Format.Text = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? "PC" : ((ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? "Mac" : ((ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? "PS3" : ((ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? "XBOX360" : "-")));

            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes")
            {
                rtxt_StatisticsOnReadDLCs.Visible = true;
                btn_Debug.Visible = true;
                txt_OggPath.Visible = true;
                txt_OggPreviewPath.Visible = true;
                txt_OldPath.Visible = true;
                txt_Lyrics.Visible = true;
                txt_AlbumArtPath.Visible = true;
                btn_AddSections.Enabled = true;
            }
            tst = "Stop Groups load and some config/debug settings... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (ConfigRepository.Instance()["general_defaultauthor"] == "" || ConfigRepository.Instance()["general_defaultauthor"] == "Custom Song Creator") ConfigRepository.Instance()["general_defaultauthor"] = "catara";
            if (lbl_NoRec.Text != "0 records." && noOfRec > 0)
                if (!Directory.Exists(databox.Rows[0].Cells["Folder_Name"].Value.ToString()) || updateDBb)
                {
                    var tmpp = "\\ORIG"; var OLD_Path = ""; var cmd = "";
                    for (var h = 0; h < 2; h++)
                    {
                        DataSet duk = new DataSet(); duk = SelectFromDB("Main", "SELECT top 1 AlbumArtPath, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE AudioPath LIKE '%" + tmpp + "%' AND audioPreviewPath is not null and oggPreviewPath is not null;", "", cnb);

                        if (duk.Tables[0].Rows.Count > 0)
                            OLD_Path = duk.Tables[0].Rows[0].ItemArray[0].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[0].ToString().IndexOf(tmpp)) + tmpp;
                        if (OLD_Path != "")
                            if (!Directory.Exists(OLD_Path) || updateDBb)
                            {
                                cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath, left(AlbumArtPath,instr(AlbumArtPath, '" + tmpp + "')-1),'" + TempPath + "'), AudioPath=REPLACE(AudioPath,left(AudioPath,instr(AudioPath, '" + tmpp + "')-1),'" + TempPath + "')";
                                cmd += ", Folder_Name=REPLACE(Folder_Name, left(Folder_Name,instr(Folder_Name, '" + tmpp + "')-1),'" + TempPath + "')";
                                cmd += " WHERE instr(AlbumArtPath, '" + tmpp + "')>0";

                                DialogResult result1 = MessageBox.Show("DB Repository has been moved from " + OLD_Path + "\n\n to " + TempPath + tmpp + "\n\n-" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result1 == DialogResult.Yes)  //|| updateDBb
                                {
                                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmd + ";", cnb);

                                    DataSet ds = new DataSet();
                                    OLD_Path = duk.Tables[0].Rows[0].ItemArray[2].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[2].ToString().IndexOf(tmpp)) + tmpp;
                                    cmd = "UPDATE Main SET OggPath=REPLACE(OggPath, left(OggPath,instr(OggPath, '" + tmpp + "')-1),'" + TempPath + tmpp + "') WHERE OggPath IS NOT NULL AND instr(OggPath, '" + tmpp + "')>0";
                                    ds = UpdateDB("Main", cmd + ";", cnb);

                                    OLD_Path = duk.Tables[0].Rows[0].ItemArray[3].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[3].ToString().IndexOf(tmpp)) + tmpp;
                                    cmd = "UPDATE Main SET oggPreviewPath=REPLACE(oggPreviewPath, left(oggPreviewPath,instr(oggPreviewPath, '" + tmpp + "')-1),'" + TempPath + tmpp + "'), audioPreviewPath=REPLACE(audioPreviewPath, left(audioPreviewPath,instr(audioPreviewPath, '" + tmpp + "')-1),'" + TempPath + "') WHERE oggPreviewPath IS NOT NULL AND instr(oggPreviewPath, '" + tmpp + "')>0";
                                    ds = UpdateDB("Main", cmd + ";", cnb);

                                    MessageBox.Show("Main table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    cmd = "UPDATE Arrangements SET SNGFilePath=REPLACE(SNGFilePath, left(SNGFilePath,instr(SNGFilePath, '" + tmpp + "')-1),'" + TempPath + tmpp + "'), XMLFilePath=REPLACE(XMLFilePath, left(XMLFilePath,instr(XMLFilePath, '" + tmpp + "')-1),'" + TempPath + tmpp + "') WHERE instr(XMLFilePath, '" + tmpp + "')>0";
                                    ds = UpdateDB("Main", cmd + ";", cnb);

                                    MessageBox.Show("Arrangements table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Issues with Temp Folder and DB Reporsitory");
                                    return;
                                }
                                tst = "Stop Fix Loocation... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            }
                    }

                    //DataViewGrid.Rows[1].Selected = true;
                    //    DataGridViewRow row;
                    //    var i = DataViewGrid.SelectedCells[0].RowIndex;
                    //    int rowindex = i;
                    ////DataViewGrid.Rows[0].Selected = true;
                    ////DataViewGrid.Rows[rowindex].Selected = false;
                    ////DataViewGrid.Rows[0].Selected = true;
                    //DataViewGrid.Rows[0].Selected = true;
                    //row = DataViewGrid.Rows[0];
                    tmpp = "\\CDLC";

                }
            Update_Selected();
            tst = "Stop cheking if the repository of decompressed songs has been moving... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        //private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //        if (DataViewGrid.Columns[DataViewGrid.CurrentCell.ColumnIndex].Name == "ContactsColumn")
        //        {
        //            ComboBox cb = e.Control as ComboBox;
        //            if (cb != null)
        //            {
        //                cb.SelectionChangeCommitted -= _SelectionChangeCommitted;
        //                cb.SelectionChangeCommitted += _SelectionChangeCommitted;
        //            }
        //        }
        //}

        //private void _SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (CheckBox1.Checked)
        //    {
        //        MessageBox.Show(((DataGridViewComboBoxEditingControl)sender).Text);
        //    }
        //}

        //private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //MAYBE HERE CAN ACTIVATE THE INDIV CELLS
        //}

        private void btn_OpenDB_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(@DB_Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + DB_Path);
            }
        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                ArrangementsDB frm = new ArrangementsDB(DB_Path, txt_ID.Text, chbx_BassDD.Checked, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                TonesDB frm = new TonesDB(DB_Path, txt_ID.Text, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose a Song.");
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //var line = -1;
            //line = DataViewGrid.SelectedCells[0].RowIndex;
            //if (line > -1) ChangeRow();
            //pB_ReadDLCs.Value = 0;
        }


        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!SearchON)
            {
                if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
                else SaveOK = false;

                btn_ChangeCover.Enabled = false;
                btn_Save.Enabled = false;

                txt_Title.Text = "";
                txt_Artist.Text = "";
                txt_Album.Text = "";
                txt_Description.Text = "";
                txt_Live_Details.Text = "";
                txt_Artist_Sort.Enabled = false;
                txt_Title_Sort.Enabled = false;
                txt_Artist_ShortName.Enabled = false;
                txt_Album_ShortName.Enabled = false;
                txt_Album_Year.Enabled = false;
                txt_DLC_ID.Enabled = false;
                txt_APP_ID.Enabled = false;
                txt_Platform.Enabled = false;
                txt_Author.Enabled = false;
                txt_Version.Enabled = false;
                chbx_AllGroups.Enabled = false;
                chbx_Alternate.Enabled = false;
                txt_Alt_No.Enabled = false;
                bth_GetTrackNo.Enabled = false;
                txt_Track_No.Enabled = false;
                txt_Top10.Enabled = false;

                SearchON = true;
                txt_ID.Text = "";
            }
            else
               if (txt_Artist.Text != "" || txt_Title.Text != "" || txt_Album.Text != "" || txt_Description.Text != "" || txt_Live_Details.Text != "")
                try
                {
                    SearchCmd = "SELECT * FROM Main u WHERE " + (txt_Artist.Text != "" ? "Artist Like '%" + txt_Artist.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Title.Text != "" ? "Song_Title Like '%" + txt_Title.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Album.Text != "" ? "Album Like '%" + txt_Album.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Live_Details.Text != "" ? "Live_Details Like '%" + txt_Live_Details.Text + "%'" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Description.Text != "" ? "Description Like '%" + txt_Description.Text + "%'" : "");
                    SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ;";
                    SearchCmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                    SearchCmd = SearchCmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
                    SearchCmd = SearchCmd.Replace("WHERE AND", "WHERE ");
                    SearchCmd = SearchCmd.Replace("AND ORDER BY ", "ORDER BY ");
                    dssx.Dispose();
                    Populate(ref databox, ref Main);
                    //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                    databox.Refresh();
                }
                catch (System.IO.FileNotFoundException ee) { MessageBox.Show(ee.Message + "Can't run Search ! " + SearchCmd); }
            else MessageBox.Show("Add a search criteria");
        }

        private void btn_SearchReset_Click(object sender, EventArgs e)
        {
            SearchFields = "ID, Artist, Song_Title, Track_No, Album, Album_Year, Author, Version, Import_Date, Is_Original, Selected, Tunning, Bass_Picking, Is_Beta, Platform, Has_DD, Bass_Has_DD, Is_Alternate, Is_Multitrack, Is_Live, Is_Acoustic, Is_Broken, MultiTrack_Version, Alternate_Version_No, Live_Details, Groups, Rating, Description, PreviewTime, PreviewLenght, Song_Lenght, Comments, FilesMissingIssues, DLC_AppID, DLC_Name, Artist_Sort, Song_Title_Sort, AverageTempo, Volume, Preview_Volume, SignatureType, ToolkitVersion, AlbumArtPath, AudioPath, audioPreviewPath, OggPath, oggPreviewPath, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, File_Size, Current_FileName, Original_FileName, Import_Path, Folder_Name, File_Hash, Original_File_Hash, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Bonus_Arrangement, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_Version, Has_Author, Has_Track_No, File_Creation_Date, Has_Been_Corrected, Pack, Available_Old, Available_Duplicate, Tones, Keep_BassDD, Keep_DD, Keep_Original, Original, YouTube_Link, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, UniqueDLCName, Artist_ShortName, Album_ShortName, DLC, Is_OLD, Remote_Path, audioBitrate, audioSampleRate, Top10 ";
            SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY Artist, Album_Year, Album, Track_No, Song_Title;";
            dssx.Dispose();
            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();

            SearchON = false;
        }

        public void ChangeRow()
        {
            var tst = "Start change row... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            //else SaveOK = false;
            if (SearchON) SearchON = false;

            int i;
            if (databox.SelectedCells.Count > 0 && databox.Rows[databox.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
                UpdateGroups();
                ////Create Groups list Dropbox
                //var norec = 0;
                //DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";");
                //norec = ds.Tables[0].Rows.Count;
                //if (norec > 0)
                //{
                //    if (chbx_Group.Items.Count > 0)//remove items
                //    {
                //        chbx_Group.DataSource = null;
                //        for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                //        {
                //            chbx_Group.Items.RemoveAt(k);
                //            chbx_AllGroups.Items.RemoveAt(k);
                //        }
                //    }

                //    for (int j = 0; j < norec; j++)//add items
                //    {
                //        chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                //        chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                //    }
                //    tst = "Stop greting Group dropdownn... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //}


                //DataSet dds = new DataSet(); dds = SelectFromDB("Groups", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";");

                //var nocrec = dds.Tables[0].Rows.Count;

                //if (nocrec > 0)
                //{
                //    for (int j = 0; j < nocrec; j++)
                //    {
                //        int index = chbx_AllGroups.Items.IndexOf(dds.Tables[0].Rows[j][0].ToString());
                //        if (index > 0) chbx_AllGroups.SetItemChecked(index, true);
                //    }
                //    tst = "Stop selecting groups of this song... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //}

                i = databox.SelectedCells[0].RowIndex;
                txt_ID.Text = databox.Rows[i].Cells["ID"].Value.ToString();
                txt_Title.Text = databox.Rows[i].Cells["Song_Title"].Value.ToString();
                txt_Title_Sort.Text = databox.Rows[i].Cells["Song_Title_Sort"].Value.ToString();
                txt_Album.Text = databox.Rows[i].Cells["Album"].Value.ToString();
                txt_Artist.Text = databox.Rows[i].Cells["Artist"].Value.ToString();
                txt_Artist_Sort.Text = databox.Rows[i].Cells["Artist_Sort"].Value.ToString();
                txt_Album_Year.Text = databox.Rows[i].Cells["Album_Year"].Value.ToString();
                txt_AverageTempo.Text = databox.Rows[i].Cells["AverageTempo"].Value.ToString();
                txt_Volume.Text = (databox.Rows[i].Cells["Volume"].Value.ToString());
                txt_Preview_Volume.Text = (databox.Rows[i].Cells["Preview_Volume"].Value.ToString());
                txt_AlbumArtPath.Text = databox.Rows[i].Cells["AlbumArtPath"].Value.ToString();
                txt_Track_No.Text = databox.Rows[i].Cells["Track_No"].Value.ToString();
                txt_Author.Text = databox.Rows[i].Cells["Author"].Value.ToString();
                txt_Version.Text = databox.Rows[i].Cells["Version"].Value.ToString();
                txt_DLC_ID.Text = databox.Rows[i].Cells["DLC_Name"].Value.ToString();
                txt_APP_ID.Text = databox.Rows[i].Cells["DLC_AppID"].Value.ToString();
                txt_MultiTrackType.Text = databox.Rows[i].Cells["MultiTrack_Version"].Value.ToString();
                txt_Alt_No.Text = databox.Rows[i].Cells["Alternate_Version_No"].Value.ToString();
                txt_Tuning.Text = databox.Rows[i].Cells["Tunning"].Value.ToString();
                txt_BassPicking.Text = databox.Rows[i].Cells["Bass_Picking"].Value.ToString();
                txt_Live_Details.Text = databox.Rows[i].Cells["Live_Details"].Value.ToString();
                txt_Rating.Text = databox.Rows[i].Cells["Rating"].Value.ToString();
                txt_Top10.Text = databox.Rows[i].Cells["Top10"].Value.ToString();
                txt_Description.Text = databox.Rows[i].Cells["Description"].Value.ToString();
                txt_Platform.Text = databox.Rows[i].Cells["Platform"].Value.ToString();
                if (databox.Rows[i].Cells["PreviewTime"].Value.ToString() == "") txt_PreviewStart.Value = Convert.ToDateTime("00:00");
                else txt_PreviewStart.Value = Convert.ToDateTime("00:" + databox.Rows[i].Cells["PreviewTime"].Value.ToString());
                if (databox.Rows[i].Cells["PreviewLenght"].Value.ToString() == "") txt_PreviewEnd.Value = 30;
                else txt_PreviewEnd.Text = databox.Rows[i].Cells["PreviewLenght"].Value.ToString();
                txt_YouTube_Link.Text = databox.Rows[i].Cells["YouTube_Link"].Value.ToString();
                btn_Youtube.Enabled = databox.Rows[i].Cells["YouTube_Link"].Value.ToString() == "" ? false : true;
                txt_CustomsForge_Link.Text = databox.Rows[i].Cells["CustomsForge_Link"].Value.ToString();
                btn_CustomForge_Link.Enabled = databox.Rows[i].Cells["CustomsForge_Link"].Value.ToString() == "" ? false : true;
                txt_CustomsForge_Like.Text = databox.Rows[i].Cells["CustomsForge_Like"].Value.ToString();
                txt_CustomsForge_ReleaseNotes.Text = databox.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value.ToString();

                txt_OggPath.Text = databox.Rows[i].Cells["OggPath"].Value.ToString();
                txt_OggPreviewPath.Text = databox.Rows[i].Cells["OggPreviewPath"].Value.ToString();
                txt_OldPath.Text = databox.Rows[i].Cells["Original_FileName"].Value.ToString();

                txt_Artist_ShortName.Text = databox.Rows[i].Cells["Artist_ShortName"].Value.ToString();
                txt_Album_ShortName.Text = databox.Rows[i].Cells["Album_ShortName"].Value.ToString();
                txt_RemotePath.Text = databox.Rows[i].Cells["Remote_Path"].Value.ToString();
                txt_FilesMissingIssues.Text = databox.Rows[i].Cells["FilesMissingIssues"].Value.ToString();
                tst = "Stop populating stnadard fields... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                if (databox.Rows[i].Cells["Is_Original"].Value.ToString() == "Yes") { chbx_Original.Checked = true; chbx_Original.ForeColor = btn_Debug.ForeColor; }
                else { chbx_Original.Checked = false; chbx_Original.ForeColor = btn_Duplicate.ForeColor; }
                if (databox.Rows[i].Cells["Is_Beta"].Value.ToString() == "Yes") chbx_Beta.Checked = true;
                else chbx_Beta.Checked = false;
                if (databox.Rows[i].Cells["Is_Alternate"].Value.ToString() == "Yes") { chbx_Alternate.Checked = true; txt_Alt_No.Enabled = true; }
                else { chbx_Alternate.Checked = false; txt_Alt_No.Enabled = false; }
                if (databox.Rows[i].Cells["Is_Multitrack"].Value.ToString() == "Yes") { chbx_MultiTrack.Checked = true; txt_MultiTrackType.Enabled = true; }
                else { chbx_MultiTrack.Checked = false; txt_MultiTrackType.Enabled = false; }
                if (databox.Rows[i].Cells["Is_Broken"].Value.ToString() == "Yes") chbx_Broken.Checked = true;
                else chbx_Broken.Checked = false;
                if (databox.Rows[i].Cells["Has_Bass"].Value.ToString() == "Yes") chbx_Bass.Checked = true;
                else chbx_Bass.Checked = false;
                if (databox.Rows[i].Cells["Has_Lead"].Value.ToString() == "Yes") chbx_Lead.Checked = true;
                else chbx_Lead.Checked = false;
                if (databox.Rows[i].Cells["Has_Combo"].Value.ToString() == "Yes") chbx_Combo.Checked = true;
                else chbx_Combo.Checked = false;
                if (databox.Rows[i].Cells["Has_Rhythm"].Value.ToString() == "Yes") chbx_Rhythm.Checked = true;
                else chbx_Rhythm.Checked = false;
                if (databox.Rows[i].Cells["Has_Vocals"].Value.ToString() == "Yes") { chbx_Lyrics.Checked = true; btn_CreateLyrics.Enabled = false; }
                else { chbx_Lyrics.Checked = false; btn_CreateLyrics.Enabled = true; }
                if (databox.Rows[i].Cells["Has_Sections"].Value.ToString().Length > 2) chbx_Sections.Checked = true;//=="Yes" 
                else chbx_Sections.Checked = false;
                if (databox.Rows[i].Cells["Has_Cover"].Value.ToString() == "Yes") chbx_Cover.Checked = true;
                else chbx_Cover.Checked = false;
                if (databox.Rows[i].Cells["Has_Preview"].Value.ToString() == "Yes") { chbx_Preview.Checked = true; btn_PlayPreview.Enabled = true; }
                else { chbx_Preview.Checked = false; btn_PlayPreview.Enabled = false; }
                if (databox.Rows[i].Cells["Has_DD"].Value.ToString() == "Yes") { txt_AddDD.Enabled = false; chbx_DD.Checked = true; btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; }
                else { chbx_DD.Checked = false; btn_AddDD.Enabled = true; btn_RemoveDD.Enabled = false; }
                if (databox.Rows[i].Cells["Keep_BassDD"].Value.ToString() == "Yes") { chbx_KeepBassDD.Checked = true; }
                else { chbx_KeepBassDD.Checked = false; }
                if (databox.Rows[i].Cells["Keep_DD"].Value.ToString() == "Yes") { chbx_KeepDD.Checked = true; }
                else { chbx_KeepDD.Checked = false; }
                if (databox.Rows[i].Cells["Selected"].Value.ToString() == "Yes") chbx_Selected.Checked = true;
                else chbx_Selected.Checked = false;
                if (databox.Rows[i].Cells["Has_Author"].Value.ToString() == "Yes") chbx_Author.Checked = true;
                else chbx_Author.Checked = false;
                if (databox.Rows[i].Cells["Bass_Has_DD"].Value.ToString() == "Yes") { chbx_BassDD.Checked = true; btn_RemoveBassDD.Enabled = true; chbx_KeepBassDD.Enabled = true; chbx_RemoveBassDD.Enabled = true; }
                else { chbx_BassDD.Checked = false; btn_RemoveBassDD.Enabled = false; chbx_RemoveBassDD.Enabled = false; }
                if (databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
                else chbx_Bonus.Checked = false;
                chbx_Avail_Old.Checked = false;
                if (float.Parse(databox.Rows[i].Cells["audioBitrate"].Value.ToString()) > ConfigRepository.Instance()["dlcm_MaxBitRate"].ToInt32()
                    || float.Parse(databox.Rows[i].Cells["audioSampleRate"].Value.ToString()) > ConfigRepository.Instance()["dlcm_MaxSampleRate"].ToInt32()
                    || databox.Rows[i].Cells["Has_Preview"].Value.ToString() == "No")
                { btn_Fix_AudioIssues.Enabled = true; }

                if (databox.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes") { chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; btn_CopyOld.Enabled = true; chbx_CopyOld.Enabled = true; }
                else { chbx_Avail_Old.Checked = false; btn_OldFolder.Enabled = false; ; btn_CopyOld.Enabled = false; chbx_CopyOld.Enabled = false; }
                chbx_Avail_Duplicate.Checked = false;
                if (databox.Rows[i].Cells["Available_Duplicate"].Value.ToString() == "Yes") { chbx_Avail_Duplicate.Checked = true; btn_DuplicateFolder.Enabled = true; }
                else { chbx_Avail_Duplicate.Checked = false; btn_DuplicateFolder.Enabled = false; }
                if (databox.Rows[i].Cells["Has_Been_Corrected"].Value.ToString() == "Yes") chbx_Has_Been_Corrected.Checked = true;
                else chbx_Has_Been_Corrected.Checked = false;
                if (databox.Rows[i].Cells["Has_Been_Corrected"].Value.ToString() == "Yes") chbx_Has_Been_Corrected.Checked = true;
                else chbx_Has_Been_Corrected.Checked = false;
                chbx_Originals_Available.Checked = false;
                if (chbx_Format_Originals.Items.Count > 0) for (int k = chbx_Format_Originals.Items.Count - 1; k >= 0; --k) chbx_Format_Originals.Items.RemoveAt(k);//remove items

                if (databox.Rows[i].Cells["Has_Other_Officials"].Value.ToString() == "Yes")
                {
                    chbx_Originals_Available.Checked = true;
                    var cmd = "SELECT DISTINCT Platform FROM Pack_AuditTrail WHERE Official=\"Yes\" AND DLC_ID=" + databox.Rows[i].Cells["ID"].Value.ToString() + " AND PackPath not like \"%0_old%\"; ";
                    DataSet dns = new DataSet(); dns = SelectFromDB("Main", cmd, "", cnb);
                    var norec = dns.Tables[0].Rows.Count;
                    if (norec > 0) for (int j = 0; j < norec; j++)
                        {
                            chbx_Format_Originals.Items.Add(dns.Tables[0].Rows[j][0].ToString());//add items
                            chbx_Format_Originals.Text = dns.Tables[0].Rows[j][0].ToString();
                        }
                }
                else chbx_Originals_Available.Checked = false;

                txt_Spotify_Song_ID.Text = databox.Rows[i].Cells["Spotify_Album_ID"].Value.ToString();
                txt_Spotify_Artist_ID.Text = databox.Rows[i].Cells["Spotify_Artist_ID"].Value.ToString();
                txt_Spotify_Album_ID.Text = databox.Rows[i].Cells["Spotify_Album_ID"].Value.ToString();
                txt_Spotify_Album_URL.Text = databox.Rows[i].Cells["Spotify_Album_URL"].Value.ToString();
                // Create the ToolTip and associate with the Form container.
                ToolTip toolTip1 = new ToolTip();
                ToolTip toolTip2 = new ToolTip();
                ToolTip toolTip3 = new ToolTip();
                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.btn_PlayAudio, databox.Rows[i].Cells["Audio_OrigHash"].Value.ToString());
                toolTip2.SetToolTip(this.btn_PlayPreview, databox.Rows[i].Cells["Audio_OrigPreviewHash"].Value.ToString());
                toolTip3.SetToolTip(this.picbx_AlbumArtPath, databox.Rows[i].Cells["audioBitrate"].Value.ToString() + " - " + databox.Rows[i].Cells["audioSampleRate"].Value.ToString());

                if (databox.Rows[i].Cells["Is_Live"].Value.ToString() == "Yes") { chbx_Is_Live.Checked = true; txt_Live_Details.Enabled = true; }
                else { chbx_Is_Live.Checked = false; txt_Live_Details.Enabled = true; }
                if (databox.Rows[i].Cells["Is_Acoustic"].Value.ToString() == "Yes") { chbx_Is_Acoustic.Checked = true; txt_Live_Details.Enabled = true; }
                else { chbx_Is_Acoustic.Checked = false; txt_Live_Details.Enabled = true; }
                tst = "Stop populating multivalue fields... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //Lyrics
                DataSet dsr = new DataSet(); dsr = SelectFromDB("Arrangements", "SELECT XMLFilePath FROM Arrangements WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";", "", cnb);
                var rec = dsr.Tables[0].Rows.Count;
                if (rec > 0) txt_Lyrics.Text = dsr.Tables[0].Rows[0].ItemArray[0].ToString();
                else txt_Lyrics.Text = "";

                if (databox.Rows[i].Cells["Has_Rhythm"].Value.ToString() != "Yes" && databox.Rows[i].Cells["Has_Lead"].Value.ToString() == "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "L->R"; }
                else if (databox.Rows[i].Cells["Has_Rhythm"].Value.ToString() == "Yes" && databox.Rows[i].Cells["Has_Lead"].Value.ToString() != "Yes") { chbx_DupliGTrack.Enabled = true; chbx_DupliGTrack.Text = "R->L"; }
                else chbx_DupliGTrack.Enabled = false;
                picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
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

                chbx_Replace.Enabled = false;
                chbx_Last_Packed.Enabled = false;
                txt_CoundofPacked.Enabled = false;
                cmb_Packed.Enabled = false;
                if ((databox.Rows[i].Cells["Remote_Path"].Value.ToString()) != "") chbx_Replace.Enabled = true;//if (File.Exists(txt_FTPPath.Text + "\\" + DataViewGrid.Rows[i].Cells["Remote_Path"].Value.ToString()))
                //Populate all Packed versionsd of the song
                var tht = "SELECT PackPath+'\\'+FileName FROM Pack_AuditTrail WHERE DLC_ID=" + txt_ID.Text + " ORDER BY ID DESC;";
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tht, "", cnb);
                rec = dvr.Tables[0].Rows.Count;
                txt_CoundofPacked.Value = rec;
                if (rec > 1)
                {
                    chbx_Last_Packed.Enabled = true;
                    txt_CoundofPacked.Enabled = true;
                    cmb_Packed.Enabled = true;
                    if (cmb_Packed.Items.Count > 0)//remove items
                    {
                        cmb_Packed.DataSource = null;
                        for (int k = cmb_Packed.Items.Count - 1; k >= 0; --k) cmb_Packed.Items.RemoveAt(k);
                        //chbx_AllGroups.Items.RemoveAt(k);
                    }

                    cmb_Packed.Items.Add("None");
                    for (int j = 0; j < rec; j++)//&add items
                        cmb_Packed.Items.Add(dvr.Tables[0].Rows[j][0].ToString());
                    cmb_Packed.Text = "None";
                    tst = "Stop adding duplciates and olds... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                }
                else chbx_Last_Packed.Enabled = false;


                //if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
                //else SaveOK = false;
            }
        }

        public void btn_Save_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        public void SaveRecord()
        {
            var tst = "Start Savin... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            int i;
            DataSet dis = new DataSet();

            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = databox.SelectedCells[0].RowIndex;

                databox.Rows[i].Cells["Song_Title"].Value = txt_Title.Text;
                databox.Rows[i].Cells["Song_Title_Sort"].Value = txt_Title_Sort.Text;
                databox.Rows[i].Cells["Album"].Value = txt_Album.Text;
                databox.Rows[i].Cells["Artist"].Value = txt_Artist.Text;
                databox.Rows[i].Cells["Artist_Sort"].Value = txt_Artist_Sort.Text;
                databox.Rows[i].Cells["Album_Year"].Value = txt_Album_Year.Text;
                databox.Rows[i].Cells["AverageTempo"].Value = txt_AverageTempo.Text;
                databox.Rows[i].Cells["Volume"].Value = txt_Volume.Text;
                databox.Rows[i].Cells["Preview_Volume"].Value = txt_Preview_Volume.Text;
                databox.Rows[i].Cells["AlbumArtPath"].Value = txt_AlbumArtPath.Text;
                databox.Rows[i].Cells["Track_No"].Value = txt_Track_No.Text;
                databox.Rows[i].Cells["Author"].Value = txt_Author.Text;
                databox.Rows[i].Cells["Version"].Value = txt_Version.Text;
                databox.Rows[i].Cells["DLC_Name"].Value = txt_DLC_ID.Text;
                databox.Rows[i].Cells["DLC_AppID"].Value = txt_APP_ID.Text;
                databox.Rows[i].Cells["MultiTrack_Version"].Value = txt_MultiTrackType.Text;
                databox.Rows[i].Cells["Alternate_Version_No"].Value = txt_Alt_No.Text;
                databox.Rows[i].Cells["Tunning"].Value = txt_Tuning.Text;
                databox.Rows[i].Cells["Bass_Picking"].Value = txt_BassPicking.Text;
                databox.Rows[i].Cells["Rating"].Value = txt_Rating.Text;
                databox.Rows[i].Cells["Top10"].Value = txt_Rating.Text;
                databox.Rows[i].Cells["Description"].Value = txt_Description.Text;
                databox.Rows[i].Cells["Platform"].Value = txt_Platform.Text;
                databox.Rows[i].Cells["Live_Details"].Value = txt_Live_Details.Text;
                databox.Rows[i].Cells["Remote_Path"].Value = txt_RemotePath.Text;
                if (AddPreview) databox.Rows[i].Cells["PreviewStart"].Value = txt_PreviewStart.Text;
                if (AddPreview) databox.Rows[i].Cells["PreviewEnd"].Value = txt_PreviewEnd.Text;
                databox.Rows[i].Cells["Youtube_Playthrough"].Value = txt_Playthough.Text;
                databox.Rows[i].Cells["YouTube_Link"].Value = txt_YouTube_Link.Text;
                databox.Rows[i].Cells["CustomsForge_Link"].Value = txt_CustomsForge_Link.Text;
                databox.Rows[i].Cells["CustomsForge_Like"].Value = txt_CustomsForge_Like.Text;
                databox.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value = txt_CustomsForge_ReleaseNotes.Text;
                databox.Rows[i].Cells["OggPreviewPath"].Value = txt_OggPreviewPath.Text;
                //DataViewGrid.Rows[i].Cells["OriginalFileName"].Value = txt_OldPath.Text;
                databox.Rows[i].Cells["Artist_ShortName"].Value = txt_Artist_ShortName.Text;
                databox.Rows[i].Cells["Album_ShortName"].Value = txt_Album_ShortName.Text;
                if (chbx_Original.Checked) databox.Rows[i].Cells["Is_Original"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Original"].Value = "No";
                if (chbx_Beta.Checked) databox.Rows[i].Cells["Is_Beta"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Beta"].Value = "No";
                if (chbx_Alternate.Checked) databox.Rows[i].Cells["Is_Alternate"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Alternate"].Value = "No";
                if (chbx_MultiTrack.Checked) databox.Rows[i].Cells["Is_MultiTrack"].Value = "Yes";
                else databox.Rows[i].Cells["Is_MultiTrack"].Value = "No";
                if (chbx_Broken.Checked) databox.Rows[i].Cells["Is_Broken"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Broken"].Value = "No";
                if (chbx_Bass.Checked) databox.Rows[i].Cells["Has_Bass"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Bass"].Value = "No";
                if (chbx_Lead.Checked) databox.Rows[i].Cells["Has_Lead"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Lead"].Value = "No";
                if (chbx_Rhythm.Checked) databox.Rows[i].Cells["Has_Rhythm"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Rhythm"].Value = "No";
                if (chbx_Combo.Checked) databox.Rows[i].Cells["Has_Combo"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Combo"].Value = "No";
                if (chbx_Lyrics.Checked) databox.Rows[i].Cells["Has_Vocals"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Vocals"].Value = "No";
                if (chbx_Sections.Checked) databox.Rows[i].Cells["Has_Sections"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Sections"].Value = "No";
                if (chbx_Cover.Checked) databox.Rows[i].Cells["Has_Cover"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Cover"].Value = "No";
                if (chbx_Preview.Checked) databox.Rows[i].Cells["Has_Preview"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Preview"].Value = "No";
                if (chbx_DD.Checked) databox.Rows[i].Cells["Has_DD"].Value = "Yes";
                else databox.Rows[i].Cells["Has_DD"].Value = "No";
                if (chbx_TrackNo.Checked) databox.Rows[i].Cells["Has_Track_No"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Track_No"].Value = "No";
                if (chbx_KeepBassDD.Checked) databox.Rows[i].Cells["Keep_BassDD"].Value = "Yes";
                else databox.Rows[i].Cells["Keep_BassDD"].Value = "No";
                if (chbx_KeepDD.Checked) databox.Rows[i].Cells["Keep_DD"].Value = "Yes";
                else databox.Rows[i].Cells["Keep_DD"].Value = "No";
                if (chbx_Selected.Checked) databox.Rows[i].Cells["Selected"].Value = "Yes";
                else databox.Rows[i].Cells["Selected"].Value = "No";
                if (chbx_Author.Checked) databox.Rows[i].Cells["Has_Author"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Author"].Value = "No";
                if (chbx_BassDD.Checked) databox.Rows[i].Cells["Bass_Has_DD"].Value = "Yes";
                else databox.Rows[i].Cells["Bass_Has_DD"].Value = "No";
                if (chbx_Bonus.Checked) databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value = "Yes";
                else databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value = "No";
                if (chbx_Avail_Old.Checked) databox.Rows[i].Cells["Available_Old"].Value = "Yes";
                else databox.Rows[i].Cells["Available_Old"].Value = "No";
                if (chbx_Is_Live.Checked) databox.Rows[i].Cells["Is_Live"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Live"].Value = "No";
                if (chbx_Is_Acoustic.Checked) databox.Rows[i].Cells["Is_Acoustic"].Value = "Yes";
                else databox.Rows[i].Cells["Is_Acoustic"].Value = "No";
                tst = "Stop saving fields... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //Save Groups
                if (GroupChanged)
                {
                    DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"", cnb);
                    for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                    {
                        if (chbx_AllGroups.GetItemChecked(j))
                        {
                            var insertcmdd = "CDLC_ID, Groups, Type";
                            var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_AllGroups.Items[j] + "\",\"DLC\"";
                            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb);
                        }
                    }
                    tst = "Stop saving fields... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    GroupChanged = false;
                }

                //Update Song / Main DB
                var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
                var command = connection.CreateCommand();
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
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
                    command.CommandText += "Tones = @param49, ";
                    command.CommandText += "Groups = @param50, ";
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
                    command.CommandText += "AlbumArt_Hash = @param80, ";
                    command.CommandText += "Audio_Hash = @param81, ";
                    command.CommandText += "AudioPreview_Hash = @param82, ";
                    command.CommandText += "Bass_Has_DD = @param83, ";
                    command.CommandText += "Has_Bonus_Arrangement = @param84, ";
                    command.CommandText += "Artist_ShortName = @param85, ";
                    command.CommandText += "Album_ShortName = @param86, ";
                    command.CommandText += "Is_Live = @param87, ";
                    command.CommandText += "Live_Details = @param88, ";
                    command.CommandText += "Remote_Path = @param89, ";
                    command.CommandText += "Is_Acoustic = @param90, ";
                    command.CommandText += "Top10 = @param91 ";


                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", databox.Rows[i].Cells["Song_Title"].Value.ToString());
                    command.Parameters.AddWithValue("@param2", databox.Rows[i].Cells["Song_Title_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param3", databox.Rows[i].Cells["Album"].Value.ToString());
                    command.Parameters.AddWithValue("@param4", databox.Rows[i].Cells["Artist"].Value.ToString());
                    command.Parameters.AddWithValue("@param5", databox.Rows[i].Cells["Artist_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param6", databox.Rows[i].Cells["Album_Year"].Value.ToString());
                    command.Parameters.AddWithValue("@param7", databox.Rows[i].Cells["AverageTempo"].Value.ToString());
                    command.Parameters.AddWithValue("@param8", databox.Rows[i].Cells["Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param9", databox.Rows[i].Cells["Preview_Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param10", databox.Rows[i].Cells["AlbumArtPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param12", databox.Rows[i].Cells["AudioPreviewPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param13", databox.Rows[i].Cells["Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param14", databox.Rows[i].Cells["Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param15", databox.Rows[i].Cells["Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param16", databox.Rows[i].Cells["DLC_Name"].Value.ToString());
                    command.Parameters.AddWithValue("@param17", databox.Rows[i].Cells["DLC_AppID"].Value.ToString());
                    command.Parameters.AddWithValue("@param26", databox.Rows[i].Cells["Is_Original"].Value.ToString());
                    command.Parameters.AddWithValue("@param28", databox.Rows[i].Cells["Is_Beta"].Value.ToString());
                    command.Parameters.AddWithValue("@param29", databox.Rows[i].Cells["Is_Alternate"].Value.ToString());
                    command.Parameters.AddWithValue("@param30", databox.Rows[i].Cells["Is_Multitrack"].Value.ToString());
                    command.Parameters.AddWithValue("@param31", databox.Rows[i].Cells["Is_Broken"].Value.ToString());
                    command.Parameters.AddWithValue("@param32", databox.Rows[i].Cells["MultiTrack_Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param33", databox.Rows[i].Cells["Alternate_Version_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param40", databox.Rows[i].Cells["Has_Vocals"].Value.ToString());
                    command.Parameters.AddWithValue("@param41", databox.Rows[i].Cells["Has_Sections"].Value.ToString());
                    command.Parameters.AddWithValue("@param42", databox.Rows[i].Cells["Has_Cover"].Value.ToString());
                    command.Parameters.AddWithValue("@param43", databox.Rows[i].Cells["Has_Preview"].Value.ToString());
                    command.Parameters.AddWithValue("@param45", databox.Rows[i].Cells["Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param47", databox.Rows[i].Cells["Tunning"].Value.ToString());
                    command.Parameters.AddWithValue("@param48", databox.Rows[i].Cells["Bass_Picking"].Value.ToString());
                    command.Parameters.AddWithValue("@param49", databox.Rows[i].Cells["Tones"].Value.ToString());
                    command.Parameters.AddWithValue("@param50", databox.Rows[i].Cells["Groups"].Value.ToString());
                    command.Parameters.AddWithValue("@param51", databox.Rows[i].Cells["Rating"].Value.ToString());
                    command.Parameters.AddWithValue("@param52", databox.Rows[i].Cells["Description"].Value.ToString());
                    command.Parameters.AddWithValue("@param54", databox.Rows[i].Cells["Has_Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param56", databox.Rows[i].Cells["PreviewTime"].Value.ToString());
                    command.Parameters.AddWithValue("@param57", databox.Rows[i].Cells["PreviewLenght"].Value.ToString());
                    command.Parameters.AddWithValue("@param58", databox.Rows[i].Cells["Youtube_Playthrough"].Value.ToString());
                    command.Parameters.AddWithValue("@param64", databox.Rows[i].Cells["Keep_BassDD"].Value.ToString());
                    command.Parameters.AddWithValue("@param65", databox.Rows[i].Cells["Keep_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param69", databox.Rows[i].Cells["Selected"].Value.ToString());
                    command.Parameters.AddWithValue("@param70", databox.Rows[i].Cells["YouTube_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param71", databox.Rows[i].Cells["CustomsForge_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param72", databox.Rows[i].Cells["CustomsForge_Like"].Value.ToString());
                    command.Parameters.AddWithValue("@param73", databox.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value.ToString());
                    command.Parameters.AddWithValue("@param76", databox.Rows[i].Cells["Has_Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param77", databox.Rows[i].Cells["oggPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param78", databox.Rows[i].Cells["oggPreviewPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param80", databox.Rows[i].Cells["AlbumArt_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param81", databox.Rows[i].Cells["Audio_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param82", databox.Rows[i].Cells["AudioPreview_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param83", databox.Rows[i].Cells["Bass_Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param84", databox.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString());
                    command.Parameters.AddWithValue("@param85", databox.Rows[i].Cells["Artist_ShortName"].Value.ToString());
                    command.Parameters.AddWithValue("@param86", databox.Rows[i].Cells["Album_ShortName"].Value.ToString());
                    command.Parameters.AddWithValue("@param87", databox.Rows[i].Cells["Is_Live"].Value.ToString());
                    command.Parameters.AddWithValue("@param88", databox.Rows[i].Cells["Live_Details"].Value.ToString());
                    command.Parameters.AddWithValue("@param89", databox.Rows[i].Cells["Remote_Path"].Value.ToString());
                    command.Parameters.AddWithValue("@param90", databox.Rows[i].Cells["Is_Acoustic"].Value.ToString());
                    command.Parameters.AddWithValue("@param91", databox.Rows[i].Cells["Top10"].Value.ToString());

                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + DB_Path + "-" + command.CommandText + ex.Message); }
                    finally { if (connection != null) connection.Close(); }

                    if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Changes Saved");
                }
            }
            GroupChanged = false;
            tst = "Stop savin... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            Update_Selected();
            tst = "Stop updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            noOfRec = 0;
            lbl_NoRec.Text = " songs.";
            bs.DataSource = null;
            dssx.Dispose();
            dssx = SelectFromDB("Main", SearchCmd, "", cnb);
            // Update_Selected();
            if (dssx.Tables.Count > 0)
            {
                DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", DisplayIndex = 0, Width = 50, HeaderText = "ID " };
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

                DataGridView.AutoResizeColumns();
                bs.ResetBindings(false);
                dssx.Tables["Main"].AcceptChanges();
                bs.DataSource = dssx.Tables["Main"];
                DataGridView.DataSource = null;
                DataGridView.DataSource = bs;
                DataGridView.Refresh();
                dssx.Dispose();

                //advance or step back in the song list
                //int i = 0;
                //if (DataViewGrid.Rows.Count > 1)
                //{
                //    var prev = DataViewGrid.SelectedCells[0].RowIndex;
                //    if (DataViewGrid.Rows.Count == prev + 2)
                //        if (prev == 0) return;
                //        else
                //        {
                //            int rowindex;
                //            DataGridViewRow row;
                //            i = DataViewGrid.SelectedCells[0].RowIndex;
                //            rowindex = i;
                //            DataViewGrid.Rows[rowindex - 1].Selected = true;
                //            DataViewGrid.Rows[rowindex].Selected = false;
                //            row = DataViewGrid.Rows[rowindex - 1];
                //        }
                //    else
                //    {
                //        int rowindex;
                //        DataGridViewRow row;
                //        i = DataViewGrid.SelectedCells[0].RowIndex;
                //        rowindex = i;
                //        DataViewGrid.Rows[rowindex + 1].Selected = true;
                //        DataViewGrid.Rows[rowindex].Selected = false;
                //        row = DataViewGrid.Rows[rowindex + 1];
                //    }
                //}
                ChangeRow();
            }
        }
        private void Update_Selected()
        {
            var tst = "Start updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var SearchCmd22 = "Select count(ID) " + SearchCmd.Substring(SearchCmd.IndexOf("FROM"), SearchCmd.IndexOf("ORDER BY") - SearchCmd.IndexOf("FROM")) + ";";//.Substring(0, SearchCmd.IndexOf("ORDER BY")) + ";");
            DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Main", SearchCmd22.Replace(",\") ;", ");"), "", cnb);
            tst = "Stop gettin Total... " + SearchCmd22; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var noOfRec = "0";
            try
            {
                if (dsz1 != null) if (dsz1.Tables[0].Rows.Count > 0) noOfRec = dsz1.Tables[0].Rows[0].ItemArray[0].ToString();
            }
            catch { }
            //var SearchCmd22 = SearchCmd;
            if (SearchCmd22.IndexOf("u WHERE") > 0)
            {
                SearchCmd22 = SearchCmd22.Replace("FROM Main u WHERE", "FROM Main u WHERE (Selected=\"Yes\") AND (");
                SearchCmd22 = SearchCmd22.Replace(";", "); ");
            }
            else
                SearchCmd22 = SearchCmd22.Replace("FROM Main u", "FROM Main u WHERE Selected=\"Yes\" ");
            DataSet dsz2 = new DataSet();
            dsz2 = SelectFromDB("Main", SearchCmd22, "", cnb);
            tst = "Stop gettin Selected... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var noOfSelRec = ""; if (dsz2.Tables.Count > 0) if (dsz2.Tables[0].Rows.Count > 0) noOfSelRec = dsz2.Tables[0].Rows[0].ItemArray[0].ToString();
            lbl_NoRec.Text = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " songs.";
            tst = "Exitin updatin Selected... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_ChangeCover_Click(object sender, EventArgs e)
        {
            var temppath = String.Empty;
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Cover PNG file";
                fbd.Filter = "PNG file (*.png)|*.png";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                temppath = fbd.FileName;

                var tmpWorkDir = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(temppath));// Create workDir folder
                if (File.Exists(temppath.Replace(".png", ".dds"))) ExternalApps.Png2Dds(temppath, Path.Combine(tmpWorkDir, temppath.Replace(".png", ".dds")), 512, 512);
                txt_AlbumArtPath.Text = temppath;
                picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                chbx_Cover.Checked = true;
                var i = databox.SelectedCells[0].RowIndex;
                databox.Rows[i].Cells["Audio_Hash"].Value = temppath.Replace(".png", ".dds");
                databox.Rows[i].Cells["Audio_Hash"].Value = GetHash(temppath);
                //if (File.Exists(temppath))
                //    using (FileStream fss = File.OpenRead(temppath))
                //    {
                //        SHA1 sha = new SHA1Managed();
                //        DataViewGrid.Rows[i].Cells["Audio_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                //        fss.Close();
                //    }
                SaveRecord();
            }

        }

        private void btn_OpenStandardization_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(DB_Path, TempPath, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb, cnb);
            frm.Show();
        }

        private void cbx_Format_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chbx_Format.Text == "PS3")
            {
                chbx_PreSavedFTP.Enabled = true;
                if (chbx_PreSavedFTP.Text == "EU") txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP1"];
                else txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP2"];
            }
            else
            {
                chbx_PreSavedFTP.Enabled = false;
                txt_FTPPath.Text = RocksmithDLCPath;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            savesettings();
        }
        private void savesettings()
        {
            if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
            ConfigRepository.Instance()["dlcm_FTP"] = chbx_PreSavedFTP.Text;
            if (chbx_PreSavedFTP.Text == "EU") ConfigRepository.Instance()["dlcm_FTP1"] = txt_FTPPath.Text;
            if (chbx_PreSavedFTP.Text == "US") ConfigRepository.Instance()["dlcm_FTP2"] = txt_FTPPath.Text;
            ConfigRepository.Instance()["dlcm_RemoveBassDD"] = chbx_RemoveBassDD.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_UniqueID"] = chbx_UniqueID.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_andCopy"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_CopyOld"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DupliGTrack"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_netstatus"] = netstatus;
            this.Close();
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)
        {
            var Filtertxt = cmb_Filter.Text;
            if (Filtertxt == "") return;
            if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
            chbx_Replace.Enabled = false;
            SearchCmd = "SELECT * FROM Main u WHERE ";

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
                    SearchCmd += "Has_Sections not like '%Yes%'";
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
                case "Broken":
                    SearchCmd += "Is_Broken = 'Yes'";
                    break;
                case "Alternate":
                    SearchCmd += "Is_Alternate = 'Yes'";
                    break;
                case "Duplciated":
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
                case "Pc":
                    SearchCmd += "Platform = 'Pc'";
                    break;
                case "PS3":
                    SearchCmd += "Platform = 'PS3'";
                    break;
                case "Mac":
                    SearchCmd += "Platform ='Mac'";
                    break;
                case "XBOX360":
                    SearchCmd += "Platform = 'XBOX360'";
                    break;
                case "0ALL"://0ALL
                    SearchCmd = SearchCmd.Replace(" WHERE ", "");
                    break;
                case "Track No. 1"://Track No. 1
                    SearchCmd += "Track_No = '1'";
                    break;
                case "DLCID diff than Default"://DLCID diff than Default
                    SearchCmd += "DLC_AppID <> '" + ConfigRepository.Instance()["general_defaultappid_RS2014"] + "'";
                    break;
                case "Automatically generated Preview"://Autom gen Preview
                    SearchCmd += "PreviewTime = '" + ConfigRepository.Instance()["dlcm_PreviewStart"] + "' AND PreviewLenght='" + ConfigRepository.Instance()["dlcm_PreviewLenght"] + "'";
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
                case "Songs in Rocksmith Lib":
                    SearchCmd += "Remote_Path <> ''";
                    break;
                case "Main_NoPreviewFile":
                    SearchCmd += "audioPreviewPath = ''";
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
                    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    DataSet djs = new DataSet(); djs = SelectFromDB("Main", "SELECT Import_Date FROM Main ORDER BY Import_Date DESC;", "", cnb);
                    noOfRec = djs.Tables[0].Rows.Count;
                    if (noOfRec > 0) SearchCmd += "Import_Date > #" + firstDayOfMonth.ToShortDateString() + "#";
                    else SearchCmd += "1 = 2";
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
                    var SearchCmd5 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID) AND (n.DLC_Name = m.DLC_Name)";
                    SearchCmd += "ID IN (" + SearchCmd5 + ")";
                    break;

                //case "READ GAMEDATA":
                default:
                    break;
            }

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

            if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ";
            try
            {
                this.databox.DataSource = null; //Then clear the rows:
                this.databox.Rows.Clear();//                Then set the data source to the new list:
                dssx.Dispose();
                Populate(ref databox, ref Main);
                //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
            catch (System.IO.FileNotFoundException ee) { MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd); }

            Update_Selected();
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
            Cache frm = new Cache(DB_Path, TempPath, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb, cnb);
            frm.ShowDialog();
        }

        private void btn_PlaySong(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = txt_OggPath.Text;
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = txt_OggPreviewPath.Text;
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_FTPPath.Text = temppath;
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
            bwConvert.ReportProgress(progress, String.Format("Converting '{0}' to {1} platform.", Path.GetFileName(sourcePackage), TargetPlatform.platform.GetPathName()[0]));
            if (!sourcePackage.IsValidPSARC())
            {
                errorsFound.AppendLine(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(sourcePackage)));

                return;
            }

            var alertMessage = String.Format("Source package '{0}' seems to be not {1} platform, the conversion impossible.", Path.GetFileName(sourcePackage), SourcePlatform);
            var haveCorrectName = Path.GetFileNameWithoutExtension(sourcePackage).EndsWith(SourcePlatform.GetPathName()[2]);
            if (SourcePlatform.platform == GamePlatform.PS3)
                haveCorrectName = Path.GetFileNameWithoutExtension(sourcePackage).EndsWith(SourcePlatform.GetPathName()[2] + ".psarc");

            if (!haveCorrectName) { errorsFound.AppendLine(alertMessage); }

            try
            {
                // CONVERT
                var output = DLCPackageConverter.Convert(sourcePackage, SourcePlatform, TargetPlatform, "248750");
                if (!String.IsNullOrEmpty(output))
                    errorsFound.AppendLine(output);
            }
            catch (Exception ex)
            {
                errorsFound.AppendLine(String.Format("{0}\n{1}\n", ex.Message, ex.StackTrace));
            }
            progress += step;
            bwConvert.ReportProgress(progress);
            bwConvert.ReportProgress(100);
            e.Result = "done";
            convdone = "done";
        }

        private void btn_Conv_And_Transfer_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 7;
            pB_ReadDLCs.Increment(1);
            if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
            pB_ReadDLCs.Increment(1);

            var Temp_Path_Import = TempPath;
            var old_Path_Import = TempPath + "\\0_old";
            var broken_Path_Import = TempPath + "\\0_broken";
            var dupli_Path_Import = TempPath + "\\0_duplicate";
            var dlcpacks = TempPath + "\\0_dlcpacks";
            var repacked_Path = TempPath + "\\0_repacked";
            var repacked_XBOXPath = TempPath + "\\0_repacked\\XBOX360";
            var repacked_PCPath = TempPath + "\\0_repacked\\PC";
            var repacked_MACPath = TempPath + "\\0_repacked\\MAC";
            var repacked_PSPath = TempPath + "\\0_repacked\\PS3";
            var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
            var AlbumCovers_PSPath = TempPath + "\\0_albumCovers";
            var Log_PSPath = TempPath + "\\0_log";
            string pathDLC = ConfigRepository.Instance()["dlcm_RocksmithDLCPath"]; //; RocksmithDLCPath;
            CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath,Archive_Path);



            var i = databox.SelectedCells[0].RowIndex;
            Groupss = chbx_Group.Text.ToString();
            //string h = "";
            //var bassfile = "";
            //string oldfilePath = "";
            //if (chbx_Last_Packed.Checked && chbx_Last_Packed.Enabled)
            //{
            //    DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + chbx_Format.Text + "\" and DLC_ID=" + txt_ID.Text + " ORDER BY ID DESC;", "");
            //    var rec = dvr.Tables[0].Rows.Count;
            //    if (rec > 0) h = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
            //}
            //else
            //if (chbx_CopyOld.Checked && chbx_CopyOld.Enabled)
            //{
            //    oldfilePath = TempPath + "\\0_old\\" + DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString();
            //    if (oldfilePath.GetPlatform().platform.ToString() == (chbx_Format.Text == "PC" ? "Pc" : chbx_Format.Text == "PS3" ? "Ps3" : chbx_Format.Text))
            //    { h = oldfilePath; }
            //    else
            //    {
            //        SourcePlatform = new Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
            //        convdone = "beginn";
            //        TargetPlatform = new Platform(chbx_Format.Text, GameVersion.RS2014.ToString());

            //        var needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
            //        var tmpDir = Path.GetTempPath();

            //        var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, false, false, SourcePlatform);

            //        // DESTINATION
            //        var nameTemplate = (!TargetPlatform.IsConsole) ? "{0}{1}.psarc" : "{0}{1}";
            //        Random randomp = new Random();
            //        int packid = randomp.Next(0, 100000);
            //        var packageName = Path.GetFileNameWithoutExtension(oldfilePath).StripPlatformEndName();
            //        if (chbx_UniqueID.Checked) packageName += packid;
            //        packageName = packageName.Replace(".", "_");
            //        var targetFileName = String.Format(nameTemplate, Path.Combine(Path.GetDirectoryName(oldfilePath), packageName), TargetPlatform.GetPathName()[2]);

            //        // CONVERSION
            //        if (needRebuildPackage)
            //        {
            //            data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
            //            // Update AppID
            //            if (!TargetPlatform.IsConsole)
            //                data.AppId = "248750";

            //            // Build
            //            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(targetFileName, data, new Platform(TargetPlatform.platform, GameVersion.RS2014));
            //        }
            //        else
            //        {
            //            // Old and new paths
            //            var sourceDir0 = SourcePlatform.GetPathName()[0].ToLower();
            //            var sourceDir1 = SourcePlatform.GetPathName()[1].ToLower();
            //            var targetDir0 = TargetPlatform.GetPathName()[0].ToLower();
            //            var targetDir1 = TargetPlatform.GetPathName()[1].ToLower();

            //            if (!TargetPlatform.IsConsole)
            //            {
            //                // Replace AppId
            //                var appIdFile = Path.Combine(unpackedDir, "appid.appid");
            //                File.WriteAllText(appIdFile, "248750");
            //            }

            //            // Replace aggregate graph values
            //            var aggregateFile = Directory.EnumerateFiles(unpackedDir, "*.nt", SearchOption.AllDirectories).FirstOrDefault();
            //            var aggregateGraphText = File.ReadAllText(aggregateFile);
            //            // Tags
            //            aggregateGraphText = Regex.Replace(aggregateGraphText, GraphItem.GetPlatformTagDescription(SourcePlatform.platform), GraphItem.GetPlatformTagDescription(TargetPlatform.platform), RegexOptions.Multiline);
            //            // Paths
            //            aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir0, targetDir0, RegexOptions.Multiline);
            //            aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir1, targetDir1, RegexOptions.Multiline);
            //            File.WriteAllText(aggregateFile, aggregateGraphText);

            //            // Rename directories
            //            foreach (var dir in Directory.GetDirectories(unpackedDir, "*.*", SearchOption.AllDirectories))
            //            {
            //                if (dir.EndsWith(sourceDir0))
            //                {
            //                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir0)) + targetDir0;
            //                    DeleteDirectory(newDir);
            //                    DirectoryExtension.Move(dir, newDir);
            //                }
            //                else if (dir.EndsWith(sourceDir1))
            //                {
            //                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
            //                    DeleteDirectory(newDir);
            //                    DirectoryExtension.Move(dir, newDir);
            //                }
            //            }

            //            // Recreates SNG because SNG have different keys in PC and Mac
            //            bool updateSNG = ((SourcePlatform.platform == GamePlatform.Pc && TargetPlatform.platform == GamePlatform.Mac) || (SourcePlatform.platform == GamePlatform.Mac && TargetPlatform.platform == GamePlatform.Pc));

            //            // Packing
            //            var dirToPack = unpackedDir;
            //            if (SourcePlatform.platform == GamePlatform.XBox360)
            //                dirToPack = Directory.GetDirectories(Path.Combine(unpackedDir, Packer.ROOT_XBox360))[0];

            //            Packer.Pack(dirToPack, targetFileName, updateSNG, TargetPlatform);
            //            DeleteDirectory(unpackedDir);
            //        }
            //        //DirectoryExtension.SafeDelete(unpackedDir);

            //        var s = TempPath + "\\0_old\\";
            //        h = TempPath + "\\0_repacked\\" + (chbx_Format.Text == "PC" ? "PC" : chbx_Format.Text == "Mac" ? "MAC" : chbx_Format.Text == "PS3" ? "PS3" : "XBOX360") + "\\"; //+ Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString()));
            //        h += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_Format.Text == "PC" ? ".psarc" : (chbx_Format.Text == "Mac" ? ".psarc" : (chbx_Format.Text == "PS3" ? ".psarc.edat" : "")));
            //        s += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_Format.Text == "PC" ? ".psarc" : (chbx_Format.Text == "Mac" ? ".psarc" : (chbx_Format.Text == "PS3" ? ".psarc.edat" : "")));// targetFileName;//s.Substring(0, s.LastIndexOf("_")) + (chbx_Format.Text == "PC" ? "_p.psarc" : (chbx_Format.Text == "Mac" ? "_m .psarc" : (chbx_Format.Text == "PS3" ? "_ps3.psarc.edat" : "")));

            //        if (File.Exists(h)) { DeleteFile(h); File.Move(s, h); }
            //        else File.Copy(s, h, true);
            //        //Generating the HASH code
            //        var FileHash = "";
            //        FileStream fs;
            //        FileHash = GetHash(h);
            //        using (fs = File.OpenRead(h))
            //        {
            //            //SHA1 sha = new SHA1Managed();
            //            //FileHash = BitConverter.ToString(sha.ComputeHash(fs));

            //            System.IO.FileInfo fi = null; //calc file size
            //            try { fi = new System.IO.FileInfo(h); }
            //            catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }

            //            var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
            //            var fnnon = Path.GetFileName(h);
            //            var packn = h.Substring(0, h.IndexOf(fnnon));
            //            var insertA = "\"" + h + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + txt_ID.Text + ",\"" + txt_DLC_ID.Text + "\",\"" + h.GetPlatform().platform.ToString() + "\"";
            //            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
            //            fs.Close();
            //        }
            //    }
            //}
            //else
            ////{
            //    var bassRemoved = "No";
            //    if (chbx_RemoveBassDD.Checked && chbx_BassDD.Checked && (!(chbx_KeepBassDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") && !(chbx_KeepDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes")))
            //    {
            //        var xmlFiles = Directory.GetFiles(DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString(), "*.xml", SearchOption.AllDirectories);
            //        Platform platform = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString().GetPlatform();

            //        foreach (var xml in xmlFiles)
            //        {
            //            Song2014 xmlContent = null;
            //            try
            //            {
            //                xmlContent = Song2014.LoadFromFile(xml);
            //                if (xmlContent.Arrangement.ToLower() == "bass" && !(xml.IndexOf(".old") > 0))
            //                {
            //                    bassRemoved = (RemoveDD(DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString(), chbx_Original.Text, xml, platform, false, false) == "Yes") ? "Yes" : "No";
            //                    bassfile = xml;
            //                    break;
            //                }
            //            }
            //            catch (Exception ee)
            //            {
            //                Console.Write(ee);
            //            }
            //        }
            //    }
            //string dlcSavePath = "";
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + txt_ID.Text + "";

            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GetRecord_s(cmd, cnb);

            //var i = 0;
            var bassRemoved = "No";
            foreach (var filez in SongRecord)
            {
                if (filez.ID == null) break;
                  var args = txt_ID.Text + ";" + (bassRemoved == "No" ? "false" : "true") + ";";
                args += chbx_Format.Text + ";" + netstatus + ";";
                args += chbx_Beta.Checked + ";" + chbx_Group.Text + ";";
                args += Groupss + ";" + TempPath + ";";
                args += chbx_UniqueID.Checked + ";" + chbx_Last_Packed.Checked + ";";
                args += chbx_Last_Packed.Enabled + ";" + chbx_CopyOld.Checked + ";";
                args += chbx_CopyOld.Enabled + ";" + chbx_Copy + ";";
                args += chbx_Replace + ";" + chbx_Replace.Enabled + ";";
                args += SourcePlatform + ";" + TargetPlatform + ";";
                args += databox.Rows[i].Cells["Original_FileName"].Value.ToString() + ";" + databox.Rows[i].Cells["Folder_Name"].Value.ToString() + ";";
                args += txt_RemotePath.Text + ";" + txt_FTPPath.Text + ";";
                args += chbx_RemoveBassDD.Checked + ";" + chbx_BassDD.Checked + ";" + chbx_KeepBassDD.Checked + ";";
                args += chbx_KeepDD.Checked + ";" + chbx_Original.Text + ";" + txt_DLC_ID.Text + ";";
                args += SearchCmd + (SearchCmd.IndexOf(";") > 0 ? "" : ";") + pathDLC + ";" + databox.Rows[databox.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value + ";"; //SearchCmd + ";" + RocksmithDLCPath, DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["DLC_Name"].Value
                args += ConfigRepository.Instance()["dlcm_AdditionalManipul76"] + ";" + "1";
                do
                    Application.DoEvents();
                while (bwRGenerate.IsBusy);//keep singlethread as toolkit not multithread abled
                bwRGenerate.RunWorkerAsync(args);//).ToString();
            }
            //null + ";" + false + ";" + chbx_PC.Text + ";" + chbx_PS3.Text + ";" + chbx_XBOX360.Text + ";" + chbx_Mac.Text + ";" + netstatus + ";" + ConfigRepository.Instance()["dlcm_AdditionalManipul54"] == "Yes" ? true : false + ";" + cbx_Groups.Text + ";" + Groupss + ";" + Temp_Path_Import + ";" + ConfigRepository.Instance()["dlcm_AdditionalManipul2"] == "Yes" ? true : false);
            //}
            //if (h == "")
            //{
            //    string copyftp = "";
            //    pB_ReadDLCs.Increment(1);
            //    var dest = "";
            //    var source = "";
            //    if (chbx_Format.Text == "PS3" && chbx_Copy.Checked)
            //    {
            //        source = h + (h.IndexOf("_ps3.psarc.edat") > 0 ? "" : "_ps3.psarc.edat");
            //        var u = ""; if (chbx_Replace.Checked && chbx_Replace.Enabled) u = DeleteFTPFiles(txt_RemotePath.Text, txt_FTPPath.Text);
            //        var a = FTPFile(txt_FTPPath.Text, source, TempPath, SearchCmd);
            //        copyftp = " and " + a + " FTPed";
            //    }
            //    else if ((chbx_Format.Text == "PC" || chbx_Format.Text == "Mac") && chbx_Copy.Checked)
            //    {
            //        var platfrm = (chbx_Format.Text == "PC" ? "_p" : (chbx_Format.Text == "Mac" ? "_m" : ""));
            //        source = h + (h.IndexOf(platfrm + ".psarc") > 0 ? "" : platfrm + ".psarc");
            //        dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
            //        try
            //        {
            //            var fn = source;//.Substring(source.IndexOf("PC\\") + 3, source.Length - source.IndexOf("PS3\\") - 3);
            //            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\";");
            //            if (chbx_Replace.Checked && chbx_Replace.Enabled) File.Move(txt_RemotePath.Text, txt_RemotePath.Text.Replace(platfrm + ".psarc", ".old"));
            //            File.Copy(source, dest, true);
            //        }
            //        catch (Exception ee) { copyftp = "Not"; Console.Write(ee); }
            //        copyftp = " and " + copyftp + " Copied";
            //    }
            //    else if (chbx_Format.Text == "PS3") source = h + "_ps3.psarc.edat";
            //    else source = h + (chbx_Format.Text == "PC" ? "_p" : (chbx_Format.Text == "Mac" ? "_m" : "")) + ".psarc";
            //    if (!((chbx_Last_Packed.Checked && chbx_Last_Packed.Enabled) || (chbx_CopyOld.Checked && chbx_CopyOld.Enabled)))
            //    {
            //        if (bassfile != "" && chbx_RemoveBassDD.Checked && chbx_BassDD.Checked && !(chbx_KeepBassDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") || !(chbx_KeepDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes"))
            //        {
            //            try
            //            {
            //                File.Copy(bassfile.Replace(".old", ""), bassfile, true);
            //            }
            //            catch (Exception ee) { Console.Write(ee); }
            //        }
            //    }
            //    //Add Pack Audit Trail
            //    if (!(chbx_CopyOld.Checked && chbx_CopyOld.Enabled && oldfilePath.GetPlatform().platform.ToString() != chbx_Format.Text))
            //    {
            //        //calc hash and file size
            //        System.IO.FileInfo fi = null;
            //        try
            //        {
            //            fi = new System.IO.FileInfo(source);

            //            //using (FileStream fs = File.OpenRead(source))
            //            //{
            //            //    SHA1 sha = new SHA1Managed();
            //            //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
            //            //    fs.Close();
            //            //}

            //            //Generating the HASH code
            //            var FileHash = ""; FileHash = GetHash(source);

            //            DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "");
            //            var norec = 0;

            //            norec -= dfs.Tables[0].Rows.Count;
            //            if (norec == 0)
            //            {
            //                var j = DataViewGrid.SelectedCells[0].RowIndex;
            //                string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
            //                var insertA = "\"" + dest + "\",\"" + source.Replace(Path.GetFileName(source), "") + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + DataViewGrid.Rows[j].Cells["ID"].Value + ",\"" + DataViewGrid.Rows[j].Cells["DLC_Name"].Value + "\",\"" + chbx_Format.Text + "\"";

            //                InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA);
            //            }
            //        }
            //        catch (System.IO.FileNotFoundException ee)
            //        {
            //            ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
            //            frm1.ShowDialog(); Console.Write(ee);
            //        }
            //    }
            pB_ReadDLCs.Increment(1);
            //MessageBox.Show("Song Packed");// + copyftp.Replace("  ", " "));

        }

        private void btn_AddPreview_Click(object sender, EventArgs e)
        //for conversion the wwise needs to be downlaoded from https://www.audiokinetic.com/download/?id=2014.1.6_5318 or https://www.audiokinetic.com/download/?id=2013.2.10_4884
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            var t = txt_OggPath.Text;
            var tt = t.Replace(".ogg", "_preview.ogg");
            string[] timepieces = txt_PreviewStart.Text.ToString().Split(':');
            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
            startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
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
                        if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                            wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                        else
                            wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                        if (wwisePath == "")
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v" + ConfigRepository.Instance()["general_wwisepath"] + " with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                            frm1.ShowDialog();
                        }
                        Converters(tt, ConverterTypes.Ogg2Wem, false, true);
                        txt_OggPreviewPath.Text = tt;
                        chbx_Preview.Checked = true;
                        var i = databox.SelectedCells[0].RowIndex;
                        databox.Rows[i].Cells["Has_Preview"].Value = "Yes";
                        databox.Rows[i].Cells["audioPreviewPath"].Value = tt.Replace(".ogg", ".wem");
                        databox.Rows[i].Cells["PreviewTime"].Value = txt_PreviewStart.Text;
                        databox.Rows[i].Cells["PreviewLenght"].Value = txt_PreviewEnd.Text;
                        btn_PlayPreview.Enabled = true;
                        databox.Rows[i].Cells["audioPreview_Hash"].Value = GetHash(tt);
                        //if (File.Exists(tt))
                        //    using (FileStream fss = File.OpenRead(tt))
                        //    {
                        //        SHA1 sha = new SHA1Managed();
                        //        DataViewGrid.Rows[i].Cells["audioPreview_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                        //        fss.Close();
                        //    }
                        AddPreview = true;
                        SaveRecord();
                    }
                }
            return;
        }


        private void txt_PreviewStart_Leave(object sender, EventArgs e)
        {
            if (txt_PreviewEnd.Text != null)
                txt_PreviewEnd.Text = "30";
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_Author_Leave(object sender, EventArgs e)
        {
            if (txt_Author.Text != null) chbx_Author.Checked = true;
        }

        private void btn_SelectPreview_Click(object sender, EventArgs e)
        {
            var temppath = String.Empty;
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Preview OGG file";
                fbd.Filter = "OGG file (*.ogg)|*.ogg";
                fbd.Multiselect = false;
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                temppath = fbd.FileName;
                txt_OggPreviewPath.Text = temppath;
                chbx_Preview.Checked = true;
                var i = databox.SelectedCells[0].RowIndex;
                databox.Rows[i].Cells["oggPreviewPath"].Value = temppath;
                Converters(temppath, ConverterTypes.Ogg2Wem, true, false);
                databox.Rows[i].Cells["audioPreviewPath"].Value = temppath.Replace(".ogg", ".wem");
                databox.Rows[i].Cells["audioPreview_Hash"].Value = GetHash(temppath.Replace(".ogg", ".wem"));
                //if (File.Exists(temppath.Replace(".ogg", ".wem")))
                //    using (FileStream fss = File.OpenRead(temppath.Replace(".ogg", ".wem")))
                //    {
                //        SHA1 sha = new SHA1Managed();
                //        DataViewGrid.Rows[i].Cells["audioPreview_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                //        fss.Close();
                //    }
                SaveRecord();
            }
        }

        private void txt_Author_TextChanged(object sender, EventArgs e)
        {
            if (txt_Author.Text != null) chbx_Author.Checked = true;
        }

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (txt_Album.Text != "" && !SearchON) ChangeRow();
            //var line = -1;
            //line = DataViewGrid.SelectedCells[0].RowIndex;
            //if (line > -1) ChangeRow();
            //pB_ReadDLCs.Value = 0;
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            var prev = databox.SelectedCells[0].RowIndex;
            if (prev == 0) return;

            SaveRecord();

            int rowindex;
            DataGridViewRow row;
            var i = databox.SelectedCells[0].RowIndex;
            rowindex = i;
            databox.Rows[rowindex - 1].Selected = true;
            databox.Rows[rowindex].Selected = false;
            row = databox.Rows[rowindex - 1];
        }

        private void btn_NextItem_Click(object sender, EventArgs e)
        {
            var prev = databox.SelectedCells[0].RowIndex;
            if (databox.Rows.Count == prev + 2) return;

            SaveRecord();

            int rowindex;
            DataGridViewRow row;
            var i = databox.SelectedCells[0].RowIndex;
            rowindex = i;
            databox.Rows[rowindex + 1].Selected = true;
            databox.Rows[rowindex].Selected = false;
            row = databox.Rows[rowindex + 1];
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {

            if (chbx_Group.Text == "" && chbx_InclGroups.Checked)
            {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            var command = cnn.CreateCommand();
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "Yes");

            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "Yes");
            }

            if (SearchCmd.IndexOf("ID, Artist") > 0) command.CommandText += " WHERE ID IN ( SELECT ID FROM Main ORDER BY Artist, Album_Year, Album, Track_No, Song_Title)";
            else
                command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }


            if (chbx_InclGroups.Checked)
            {
                //var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";
                //DeleteFromDB("Groups", cmd);
                pB_ReadDLCs.Value = 0;
                DataSet dus = new DataSet(); dus = SelectFromDB("Main", SearchCmd, "", cnb);
                var norec = dus.Tables[0].Rows.Count;
                pB_ReadDLCs.Maximum = norec;
                foreach (DataRow dataRow in dus.Tables[0].Rows)
                {
                    var insertcmdd = "CDLC_ID, Groups, Type";
                    var insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + chbx_Group.Text + "\",\"DLC\"";
                    //insertvalues = SearchCmd.Replace("*", "");
                    InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb);
                    pB_ReadDLCs.Increment(1);
                }
            }

            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();
            //var com = "Select * FROM Main";

            //if (SearchCmd.IndexOf("ID, Artist") > 0) com += " WHERE ID IN ( SELECT ID FROM Main ORDER BY Artist, Album_Year, Album, Track_No, Song_Title)";
            //else
            //    com += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            //DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", com);
            Update_Selected();
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {

            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            var command = cnn.CreateCommand();
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
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                throw;
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }

            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";

                //DeleteFromDB("Groups", cmd);
                //var insertcmdd = "CDLC_ID, Groups, Type";
                //var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"DLC\"";
                //InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb);
            }

            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();
            Update_Selected();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected" + test);
        }

        //public string GeneratePackage(string ID, bool bassRemoved)
        //{
        //    string dlcSavePath = "";
        //    var cmd = "SELECT * FROM Main ";
        //    cmd += "WHERE ID = " + ID + "";

        //    //Read from DB
        //    MainDBfields[] SongRecord = new MainDBfields[10000];
        //    SongRecord = GenericFunctions.GetRecord_s(cmd);

        //    var i = 0;
        //    foreach (var filez in SongRecord)
        //    {
        //        if (i > 0) //ONLY 1  FILE WILL BE READ
        //            break;
        //        i++;
        //        var packagePlatform = filez.Folder_Name.GetPlatform();
        //        // REORGANIZE
        //        var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
        //        //if (structured)
        //        //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);
        //        // LOAD DATA
        //        var info = DLCPackageData.LoadFromFolder(filez.Folder_Name, packagePlatform);

        //        var xmlFiles = Directory.GetFiles(filez.Folder_Name, "*.xml", SearchOption.AllDirectories);
        //        var platform = filez.Folder_Name.GetPlatform();
        //        pB_ReadDLCs.Increment(1);
        //        data = new DLCPackageData
        //        {
        //            GameVersion = GameVersion.RS2014,
        //            Pc = chbx_Format.Text == "PC" || filez.Platform == "Pc" ? true : false,
        //            Mac = chbx_Format.Text == "Mac" || filez.Platform == "Mac" ? true : false,
        //            XBox360 = chbx_Format.Text == "XBOX360" || filez.Platform == "Xbox360" ? true : false,
        //            PS3 = chbx_Format.Text == "PS3" || filez.Platform == "Ps3" ? true : false,
        //            Name = filez.DLC_Name,
        //            AppId = filez.DLC_AppID,
        //            ArtFiles = info.ArtFiles, //not complete
        //            //Showlights = true,//info.Showlights, //apparently this info is not read..also the tone base is removed/not read also
        //            Inlay = info.Inlay,
        //            LyricArtPath = info.LyricArtPath,

        //            //USEFUL CMDs String.IsNullOrEmpty(
        //            SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
        //            {
        //                SongDisplayName = filez.Song_Title,
        //                SongDisplayNameSort = filez.Song_Title_Sort,
        //                Album = filez.Album,
        //                SongYear = filez.Album_Year.ToInt32(),
        //                Artist = filez.Artist,
        //                ArtistSort = filez.Artist_Sort,
        //                AverageTempo = filez.AverageTempo.ToInt32()
        //            },

        //            AlbumArtPath = filez.AlbumArtPath,
        //            OggPath = filez.AudioPath,
        //            OggPreviewPath = ((filez.audioPreviewPath != "") ? filez.audioPreviewPath : filez.AudioPath),
        //            Arrangements = info.Arrangements, //Not yet done
        //            Tones = info.Tones,//Not yet done
        //            TonesRS2014 = info.TonesRS2014,//Not yet done
        //            Volume = Convert.ToSingle(filez.Volume),
        //            PreviewVolume = Convert.ToSingle(filez.Preview_Volume),
        //            SignatureType = info.SignatureType
        //        };
        //        //RocksmithToolkitLib.DLCPackage.ToolkitInfo.PackageVersion = filez.ToolkitVersion//Version

        //        //Add Tones
        //        //var cmds = "SELECT * FROM Tones WHERE CDLC_ID=" + txt_ID.Text + ";";
        //        //DataSet dfs = new DataSet();
        //        //var norec = 0;
        //        //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
        //        //{
        //        //    try
        //        //    {
        //        //        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
        //        //        da.Fill(dfs, "Tones");
        //        //    }
        //        //    catch (Exception ex)
        //        //    {

        //        //    }
        //        //Update Tones
        //        var norec = 0;
        //        DataSet dfs = new DataSet(); dfs = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + txt_ID.Text + ";", "");
        //        foreach (var arg in info.TonesRS2014)//, Type
        //        {
        //            norec = dfs.Tables[0].Rows.Count;
        //            for (int j = 0; j < norec; j++)
        //            {
        //                data.TonesRS2014[j].Name = dfs.Tables[0].Rows[j].ItemArray[1].ToString();
        //                data.TonesRS2014[j].Volume = float.Parse(dfs.Tables[0].Rows[j].ItemArray[3].ToString());
        //                data.TonesRS2014[j].Key = dfs.Tables[0].Rows[j].ItemArray[4].ToString();
        //                data.TonesRS2014[j].IsCustom = dfs.Tables[0].Rows[j].ItemArray[5].ToString() == "True" ? true : false;
        //                data.TonesRS2014[j].SortOrder = decimal.Parse(dfs.Tables[0].Rows[j].ItemArray[11].ToString());
        //                data.TonesRS2014[j].NameSeparator = dfs.Tables[0].Rows[j].ItemArray[12].ToString();
        //                //dictionary types not saved in the DB yet
        //                var nrc = 0;
        //                DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Amp\";", "");
        //                nrc = dsc.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    data.TonesRS2014[j].GearList.Amp.Type = dsc.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Amp.Category = dsc.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FG = new Dictionary<string, float>();
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    strArrK = dsc.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsc.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; k <= strArrK.Length - 1; l++) FG.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Amp.KnobValues = FG;
        //                    data.TonesRS2014[j].GearList.Amp.PedalKey = dsc.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.Skin = dsc.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dsc.Tables[0].Rows[k].ItemArray[6].ToString());
        //                }
        //                nrc = 0;
        //                DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Cabinet\";", "");
        //                nrc = dsa.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.Cabinet.Type = dsa.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Cabinet.Category = dsa.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsa.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsa.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.Cabinet.PedalKey = dsa.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.Skin = dsa.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dsa.Tables[0].Rows[k].ItemArray[6].ToString());
        //                }
        //                nrc = 0;
        //                DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PostPedal1\";", "");
        //                nrc = dss1.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PostPedal1.Type = dss1.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.Category = dss1.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dss1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dss1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PostPedal1.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PostPedal1.PedalKey = dss1.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.Skin = dss1.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dss1.Tables[0].Rows[k].ItemArray[6].ToString());
        //                }
        //                nrc = 0;
        //                DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PostPedal2\";", "");
        //                nrc = dss2.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PostPedal2.Type = dss2.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal2.Category = dss2.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dss2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dss2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PostPedal2.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PostPedal2.PedalKey = dss2.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal2.Skin = dss2.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal2.SkinIndex = float.Parse(dss2.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PostPedal3\";", "");
        //                nrc = dss3.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PostPedal3.Type = dss3.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal3.Category = dss3.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dss3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dss3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PostPedal3.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PostPedal3.PedalKey = dss3.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal3.Skin = dss3.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal3.SkinIndex = float.Parse(dss3.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PostPedal4\";", "");
        //                nrc = dss4.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PostPedal4.Type = dss4.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal4.Category = dss4.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dss4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dss4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PostPedal4.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PostPedal4.PedalKey = dss4.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal4.Skin = dss4.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PostPedal4.SkinIndex = float.Parse(dss4.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }

        //                nrc = 0;
        //                DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PrePedal1\";", "");
        //                nrc = dsp1.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PrePedal1.Type = dsp1.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal1.Category = dsp1.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsp1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsp1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PrePedal1.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PrePedal1.PedalKey = dsp1.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal1.Skin = dsp1.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal1.SkinIndex = float.Parse(dsp1.Tables[0].Rows[k].ItemArray[6].ToString());
        //                }
        //                nrc = 0;
        //                DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PrePedal2\";", "");
        //                nrc = dsp2.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PrePedal2.Type = dsp2.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal2.Category = dsp2.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsp2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsp2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PrePedal2.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PrePedal2.PedalKey = dsp2.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal2.Skin = dsp2.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal2.SkinIndex = float.Parse(dsp2.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PrePedal3\";", "");
        //                nrc = dsp3.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PrePedal3.Type = dsp3.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal3.Category = dsp3.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsp3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsp3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PrePedal3.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PrePedal3.PedalKey = dsp3.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal3.Skin = dsp3.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal3.SkinIndex = float.Parse(dsp3.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"PrePedal4\";", "");
        //                nrc = dsp4.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.PrePedal4.Type = dsp4.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal4.Category = dsp4.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsp4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsp4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.PrePedal4.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.PrePedal4.PedalKey = dsp4.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal4.Skin = dsp4.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.PrePedal4.SkinIndex = float.Parse(dsp4.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }

        //                nrc = 0;
        //                DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Rack1\";", "");
        //                nrc = dsr1.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.Rack1.Type = dsr1.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Rack1.Category = dsr1.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsr1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsr1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Rack1.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.Rack1.PedalKey = dsr1.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.Rack1.Skin = dsr1.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.Rack1.SkinIndex = float.Parse(dsr1.Tables[0].Rows[k].ItemArray[6].ToString());
        //                }
        //                nrc = 0;
        //                DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Rack2\";", "");
        //                nrc = dsr2.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.Rack2.Type = dsr2.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Rack2.Category = dsr2.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsr2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsr2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Rack2.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.Rack2.PedalKey = dsr2.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.Rack2.Skin = dsr2.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.Rack2.SkinIndex = float.Parse(dsr2.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Rack3\";", "");
        //                nrc = dsr3.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.Rack3.Type = dsr3.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Rack3.Category = dsr3.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsr3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsr3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Rack3.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.Rack3.PedalKey = dsr3.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.Rack3.Skin = dsr3.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.Rack3.SkinIndex = float.Parse(dsr3.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                nrc = 0;
        //                DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"Rack4\";", "");
        //                nrc = dsr4.Tables[0].Rows.Count;
        //                for (int k = 0; k < nrc; k++)
        //                {
        //                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
        //                    data.TonesRS2014[j].GearList.Rack4.Type = dsr4.Tables[0].Rows[k].ItemArray[0].ToString();
        //                    data.TonesRS2014[j].GearList.Rack4.Category = dsr4.Tables[0].Rows[k].ItemArray[1].ToString();
        //                    Dictionary<string, float> FS = new Dictionary<string, float>();
        //                    strArrK = dsr4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
        //                    strArrV = dsr4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
        //                    for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
        //                    data.TonesRS2014[j].GearList.Rack4.KnobValues = FS;
        //                    data.TonesRS2014[j].GearList.Rack4.PedalKey = dsr4.Tables[0].Rows[k].ItemArray[4].ToString();
        //                    data.TonesRS2014[j].GearList.Rack4.Skin = dsr4.Tables[0].Rows[k].ItemArray[5].ToString();
        //                    data.TonesRS2014[j].GearList.Rack4.SkinIndex = float.Parse(dsr4.Tables[0].Rows[k].ItemArray[6].ToString());

        //                }
        //                //Type myType = typeof(RocksmithToolkitLib.DLCPackage.Manifest2014.Tone.Gear2014);
        //                //PropertyInfo myPropInfo = myType.GetProperty(propertyName);
        //                //myPropInfo.SetValue(this, value, null);
        //                //data.TonesRS2014[j].GearList.PostPedal1.;//.sdfs.Tables[0].Rows[14].ItemArray[0].ToString() };
        //                //data.TonesRS2014[j].GearList.PostPedal2 = dfs.Tables[0].Rows[15].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.PostPedal3 = dfs.Tables[0].Rows[16].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.PostPedal4 = dfs.Tables[0].Rows[17].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.PrePedal1 = dfs.Tables[0].Rows[18].ItemArray[0].ToString().ToInt32();
        //                //data.TonesRS2014[j].GearList.PrePedal2 = dfs.Tables[0].Rows[19].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.PrePedal3 = dfs.Tables[0].Rows[20].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.PrePedal4 = dfs.Tables[0].Rows[21].ItemArray[0].ToString();
        //                //Pedal2014 rack1 = new Pedal2014();
        //                //Pedal2014 rack2 = new Pedal2014();
        //                //rack1.
        //                //data.TonesRS2014[j].GearList.Rack1 = dfs.Tables[0].Rows[22].ItemArray[0].ToString().ToInt32();
        //                //data.TonesRS2014[j].GearList.Rack2 = dfs.Tables[0].Rows[23].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.Rack3 = dfs.Tables[0].Rows[24].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.Rack4 = dfs.Tables[0].Rows[25].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.Amp.Type = dfs.Tables[0].Rows[26].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.Amp.Category = dfs.Tables[0].Rows[j].ItemArray[27].ToString();
        //                ////IDictionary dictionar;// ="";
        //                ////KeyCollection Kes;//= dfs.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
        //                //Dictionary<string, float> FG = new Dictionary<string, float>();
        //                //string[] strArrK = null;
        //                //string[] strArrV = null;
        //                ////int count = 0;
        //                ////str = "CSharp split test";
        //                //char[] splitchar = { ';' };
        //                //strArrK = dfs.Tables[0].Rows[28].ItemArray[0].ToString().Split(splitchar);
        //                //strArrV = dfs.Tables[0].Rows[29].ItemArray[0].ToString().Split(splitchar);
        //                //for (int k = 0; k <= strArrK.Length - 1; k++) FG.Add(strArrK[k], strArrV[k].ToInt32());
        //                //data.TonesRS2014[j].GearList.Amp.KnobValues = FG;
        //                ////data.TonesRS2014[j].GearList.Amp.KnobValues.Keys = dfs.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
        //                ////data.TonesRS2014[j].GearList.Amp.KnobValues.Keys = float.Parse((dfs.Tables[0].Rows[29].ItemArray[0].ToString());
        //                //data.TonesRS2014[j].GearList.Amp.PedalKey = dfs.Tables[0].Rows[j].ItemArray[30].ToString();
        //                //data.TonesRS2014[j].GearList.Cabinet.Category = dfs.Tables[0].Rows[j].ItemArray[31].ToString();
        //                //Dictionary<string, float> FS = new Dictionary<string, float>();
        //                //strArrK = dfs.Tables[0].Rows[32].ItemArray[0].ToString().Split(splitchar);
        //                //strArrV = dfs.Tables[0].Rows[33].ItemArray[0].ToString().Split(splitchar);
        //                //for (int k = 0; k <= strArrK.Length - 1; k++)  FG.Add(strArrK[k], strArrV[k].ToInt32());
        //                //data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
        //                ////data.TonesRS2014[j].GearList.Cabinet.KnobValues.Values = float.Parse((dfs.Tables[0].Rows[32].ItemArray[0].ToString());
        //                ////data.TonesRS2014[j].GearList.Cabinet.KnobValues.Keys = dfs.Tables[0].Rows[33].ItemArray[0].ToString();
        //                //data.TonesRS2014[j].GearList.Cabinet.PedalKey = dfs.Tables[0].Rows[j].ItemArray[34].ToString();
        //                //data.TonesRS2014[j].GearList.Cabinet.Type = dfs.Tables[0].Rows[j].ItemArray[35].ToString();

        //            }
        //            //}
        //        }


        //        //Add Arrangements
        //        norec = 0;
        //        string sds = "";
        //        DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID = " + txt_ID.Text + "; ", "");

        //        norec = ds.Tables[0].Rows.Count;
        //        if (info.Arrangements.Capacity < norec)
        //            data.Arrangements.Add(new RocksmithToolkitLib.DLCPackage.Arrangement
        //            {
        //                Name = ArrangementName.Vocals,
        //                ArrangementType = ArrangementType.Vocal,
        //                ScrollSpeed = 20,

        //                Id = IdGenerator.Guid(),
        //                MasterId = RandomGenerator.NextInt(),
        //                //SongXml = new SongXML { File = xmlFile },
        //                //SongFile = new SongFile { File = "" },
        //                CustomFont = false
        //            });
        //        //norec += 1;

        //        foreach (var arg in info.Arrangements)//, Type
        //        {
        //            for (int j = 0; j < norec; j++)
        //            {
        //                //if (j == data.Arrangements.Count) {
        //                //    //mArr.Add(GenMetronomeArr(data.Arrangements));
        //                //    //mArr[0].SongXml.File = "1";
        //                //    //var mArr = new List<Arrangement>();
        //                //    //data.Arrangements.Capacity = 5;
        //                //    //data.Arrangements.Add(data.Arrangements[j]);
        //                //    // Add Vocal Arrangement
        //                //    data.Arrangements.Add(new Arrangement
        //                //    {
        //                //        Name = ArrangementName.Vocals,
        //                //        ArrangementType = ArrangementType.Vocal,
        //                //        ScrollSpeed = 20,
        //                //        //SongXml = new SongXML { File = xmlFile },
        //                //        //SongFile = new SongFile { File = "" },
        //                //        CustomFont = false
        //                //    });
        //                //    norec += 1;
        //                //}
        //                sds = ds.Tables[0].Rows[j].ItemArray[1].ToString();
        //                //data.Arrangements[j].Name = ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementName.Bass : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Lead" ? RocksmithToolkitLib.Sng.ArrangementName.Lead : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Vocals" ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Rhythm" ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : ds.Tables[0].Rows[j].ItemArray[12].ToString() == "ShowLights" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
        //                data.Arrangements[j].Name = (sds == "3") ? RocksmithToolkitLib.Sng.ArrangementName.Bass : (sds == "0") ? RocksmithToolkitLib.Sng.ArrangementName.Lead : (sds == "4") ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : (sds == "1") ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : sds == "6" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
        //                data.Arrangements[j].BonusArr = ds.Tables[0].Rows[j].ItemArray[3].ToString() == "True" ? true : false;
        //                sds = ds.Tables[0].Rows[j].ItemArray[4].ToString();
        //                data.Arrangements[j].SongFile = new SongFile { File = ds.Tables[0].Rows[j].ItemArray[4].ToString() == "" ? ds.Tables[0].Rows[j].ItemArray[5].ToString().Replace(".xml", ".json") : ds.Tables[0].Rows[j].ItemArray[4].ToString() }; // if (File.Exists(sds))
        //                data.Arrangements[j].SongXml = new SongXML { File = ds.Tables[0].Rows[j].ItemArray[5].ToString() };
        //                data.Arrangements[j].ScrollSpeed = ds.Tables[0].Rows[j].ItemArray[7].ToString().ToInt32();
        //                data.Arrangements[j].Tuning = ds.Tables[0].Rows[j].ItemArray[8].ToString();
        //                data.Arrangements[j].ArrangementSort = ds.Tables[0].Rows[j].ItemArray[12].ToString().ToInt32();
        //                data.Arrangements[j].TuningPitch = ds.Tables[0].Rows[j].ItemArray[13].ToString().ToInt32();
        //                data.Arrangements[j].ToneBase = ds.Tables[0].Rows[j].ItemArray[14].ToString();
        //                data.Arrangements[j].Id = Guid.Parse(ds.Tables[0].Rows[15].ItemArray[0].ToString());
        //                data.Arrangements[j].MasterId = (ds.Tables[0].Rows[j].ItemArray[16].ToString().ToInt32() == 0 ? data.Arrangements[j].MasterId : ds.Tables[0].Rows[j].ItemArray[16].ToString().ToInt32());
        //                data.Arrangements[j].ArrangementType = ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementType.Bass : ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Guitar" ? RocksmithToolkitLib.Sng.ArrangementType.Guitar : ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Vocal" ? RocksmithToolkitLib.Sng.ArrangementType.Vocal : RocksmithToolkitLib.Sng.ArrangementType.ShowLight;
        //                //RocksmithToolkitLib.Sng.ArrangementType.Bass ds.Tables[0].Rows[17].ItemArray[0].ToString();
        //                data.Arrangements[j].TuningStrings.String0 = Int16.Parse(ds.Tables[0].Rows[18].ItemArray[0].ToString());
        //                data.Arrangements[j].TuningStrings.String1 = Int16.Parse(ds.Tables[0].Rows[19].ItemArray[0].ToString());
        //                data.Arrangements[j].TuningStrings.String2 = Int16.Parse(ds.Tables[0].Rows[20].ItemArray[0].ToString());
        //                data.Arrangements[j].TuningStrings.String3 = Int16.Parse(ds.Tables[0].Rows[21].ItemArray[0].ToString());
        //                data.Arrangements[j].TuningStrings.String4 = Int16.Parse(ds.Tables[0].Rows[22].ItemArray[0].ToString());
        //                data.Arrangements[j].TuningStrings.String5 = Int16.Parse(ds.Tables[0].Rows[23].ItemArray[0].ToString());
        //                data.Arrangements[j].PluckedType = ds.Tables[0].Rows[j].ItemArray[24].ToString() == "Picked" ? RocksmithToolkitLib.Sng.PluckedType.Picked : RocksmithToolkitLib.Sng.PluckedType.NotPicked;
        //                data.Arrangements[j].RouteMask = ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Bass" ? RouteMask.Bass : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Lead" ? RouteMask.Lead : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "None" ? RouteMask.None : RouteMask.Any;
        //                // data.Arrangements[j].SongXml.Name = ds.Tables[0].Rows[26].ItemArray[0].ToString();
        //                //data.Arrangements[j].SongXml.LLID = ds.Tables[0].Rows[27].ItemArray[0].ToInt32().ToInt32();
        //                data.Arrangements[j].SongXml.UUID = Guid.Parse(ds.Tables[0].Rows[28].ItemArray[0].ToString());
        //                //data.Arrangements[j].SongFile.Name = ds.Tables[0].Rows[29].ItemArray[0].ToString();
        //                //data.Arrangements[j].SongFile.LLID = Guid.Parse(ds.Tables[0].Rows[30].ItemArray[0].ToString().ToString());
        //                data.Arrangements[j].SongFile.UUID = Guid.Parse(ds.Tables[0].Rows[31].ItemArray[0].ToString());
        //                data.Arrangements[j].ToneMultiplayer = ds.Tables[0].Rows[j].ItemArray[32].ToString();
        //                data.Arrangements[j].ToneA = ds.Tables[0].Rows[j].ItemArray[33].ToString();
        //                data.Arrangements[j].ToneB = ds.Tables[0].Rows[j].ItemArray[34].ToString();
        //                data.Arrangements[j].ToneC = ds.Tables[0].Rows[j].ItemArray[35].ToString();
        //                data.Arrangements[j].ToneD = ds.Tables[0].Rows[j].ItemArray[36].ToString();
        //            }
        //        }
        //        //}

        //        //get track no
        //        if (ConfigRepository.Instance()["dlcm_AdditionalManipul41"] == "Yes" && netstatus != "NOK")
        //        {
        //            var CleanTitle = "";
        //            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
        //            if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
        //            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

        //            //string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
        //            //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
        //            //int z = await GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, txt_Title.Text, txt_Album_Year.Text, txt_SpotifyStatus.Text);
        //            //txt_Track_No.Text = z == 0 && txt_Track_No.Text != "" ? txt_Track_No.Text : z.ToString();

        //            ActivateSpotify_ClickAsync();
        //            if (netstatus == "OK")
        //            {
        //                Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
        //                string s = sptyfy.ToString();
        //                txt_Track_No.Text = s.Split(';')[0];
        //            }
        //            //SpotifySongID = s.Split(';')[1];
        //            //SpotifyArtistID = s.Split(';')[2];
        //            //SpotifyAlbumID = s.Split(';')[3];
        //            //SpotifyAlbumURL = s.Split(';')[4];
        //            // GetTrackNoSpotifyAsync(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName));

        //        }

        //        data.ToolkitInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
        //        data.ToolkitInfo.PackageAuthor = filez.Author;
        //        if ((filez.Author == "Custom Song Creator" || filez.Author == "") && ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" && filez.Is_Original != "Yes")
        //        {
        //            //sving in the txt file is not rally usefull as the system gen the file at every pack :)
        //            filez.Author = "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
        //            data.ToolkitInfo.PackageAuthor = "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
        //            var fxml = File.OpenText(filez.Folder_Name + "\\toolkit.version");
        //            string line;
        //            string header = "";
        //            //Read and Save Header
        //            while ((line = fxml.ReadLine()) != null)
        //            {
        //                if (line.Contains("Package Author:")) header += System.Environment.NewLine + "Package Author: " + filez.Author;
        //                else header += line + System.Environment.NewLine;
        //            }
        //            fxml.Close();
        //            File.WriteAllText(filez.Folder_Name + "\\toolkit.version", header);
        //        }

        //        DirectoryInfo di;
        //        var repacked_Path = TempPath + "\\0_repacked";
        //        if (!Directory.Exists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

        //        var norm_path = "";
        //        if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")
        //            norm_path = repacked_Path + "\\" + Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "[", "]");
        //        else
        //            norm_path = ((filez.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear.ToString() + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
        //        //manipulating the info

        //        if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") data.SongInfo.SongDisplayName = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved, SongRecord, "[", "]");
        //        if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") data.SongInfo.SongDisplayNameSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_sort"], 0, false, false, bassRemoved, SongRecord, "", "");
        //        if (chbx_Beta.Checked) if (chbx_Group.Text != "") data.SongInfo.SongDisplayNameSort = "0" + Groupss + data.SongInfo.SongDisplayNameSort.Substring(1, data.SongInfo.SongDisplayNameSort.Length - 2); //).Replace("][", "-").Replace("]0", "");

        //        if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") data.SongInfo.Artist = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved, SongRecord, "[", "]");
        //        if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "");
        //        if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") data.SongInfo.Album = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved, SongRecord, "[", "]");

        //        pB_ReadDLCs.Increment(1);
        //        if (chbx_UniqueID.Checked)
        //        {
        //            Random random = new Random();
        //            data.Name = random.Next(0, 100000) + data.Name;
        //            norm_path += data.Name;
        //        }

        //        //Fix the _preview_preview issue
        //        var ms = data.OggPath;
        //        var tst = "";
        //        try
        //        {
        //            var sourceAudioFiles = Directory.GetFiles(filez.Folder_Name, "*.wem", SearchOption.AllDirectories);

        //            foreach (var fil in sourceAudioFiles)
        //            {
        //                tst = fil;
        //                if (fil.LastIndexOf("_preview_preview.wem") > 0)
        //                {
        //                    ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
        //                    File.Move((ms + "_preview.wem"), (ms + ".wem"));
        //                    File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
        //                }
        //            }
        //        }
        //        catch (Exception ee) { Console.WriteLine(ee.Message); }
        //        if (data == null)
        //            MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        if (ConfigRepository.Instance()["dlcm_AdditionalManipul71"] == "Yes")
        //        {
        //            GenericFunctions.Converters(data.OggPreviewPath.Replace(".wem", ".ogg"), GenericFunctions.ConverterTypes.Ogg2Wem, false);
        //            DeleteFile(data.OggPreviewPath.Replace(".wem", ".wav"));
        //        }


        //        var FN = ConfigRepository.Instance()["dlcm_File_Name"];
        //        FN = Manipulate_strings(FN, 0, false, false, bassRemoved, SongRecord, "", "");
        //        if (chbx_Beta.Checked) if (chbx_Group.Text != "") FN = "0" + Groupss + FN.Substring(1, FN.Length - 2); //}).Replace("][", "-").Replace("]0", "");

        //        if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes" || chbx_Format.Text == "PS3")
        //        {
        //            FN = FN.Replace(".", "_");
        //            FN = FN.Replace(" ", "_");
        //        }

        //        dlcSavePath = repacked_Path + "\\" + chbx_Format.Text + "\\" + FN;

        //        data.ToolkitInfo.PackageVersion = filez.Version;

        //        int progress = 0;
        //        var errorsFound = new StringBuilder();
        //        var numPlatforms = 0;
        //        numPlatforms++;

        //        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
        //        if (chbx_Format.Text == "PC")
        //            try
        //            {
        //                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
        //                progress += step;
        //            }
        //            catch (Exception ex)
        //            {
        //                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                {
        //                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                    frm1.ShowDialog();
        //                }
        //                errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //            }

        //        if (chbx_Format.Text == "Mac")
        //            try
        //            {
        //                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
        //                progress += step;
        //            }
        //            catch (Exception ex)
        //            {
        //                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                {
        //                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                    frm1.ShowDialog();
        //                }
        //                errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //            }

        //        if (chbx_Format.Text == "XBOX360")
        //            try
        //            {
        //                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
        //                progress += step;
        //            }
        //            catch (Exception ex)
        //            {
        //                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                {
        //                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                    frm1.ShowDialog();
        //                }
        //                errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
        //            }

        //        if (chbx_Format.Text == "PS3")
        //            try
        //            {
        //                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
        //                progress += step;
        //            }
        //            catch (Exception ex)
        //            {
        //                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
        //                {
        //                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
        //                    frm1.ShowDialog();
        //                }
        //                string ss = String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace);
        //                MessageBox.Show(ex + ss);
        //                errorsFound.AppendLine(ss);
        //            }
        //        data.CleanCache();
        //        i++;
        //    }
        //    pB_ReadDLCs.Increment(1);
        //    return dlcSavePath;
        //}


        public int GetHashCode(RocksmithToolkitLib.XML.SongEvent obj)
        {
            if (ReferenceEquals(obj, null))
                return 0;
            return obj.Code.GetHashCode() | obj.Time.GetHashCode();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chbx_PreSavedFTP.Text == "EU") txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP1"];
            else txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP2"];
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
                        var bassRemoved = (RemoveDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false) == "Yes") ? "Yes" : "No";
                        chbx_BassDD.Checked = false;
                        btn_RemoveBassDD.Enabled = false;
                        SaveRecord();
                        break;
                    }
                }
                catch (Exception ee)
                {
                    Console.Write(ee);
                }
            }
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
            SaveRecord();
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
                        var DDRemoved = (RemoveDD(SongRecord[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false) == "Yes") ? "Yes" : "No";
                    }
                }
                catch (Exception ee)
                { Console.Write(ee); }
            }
            chbx_DD.Checked = false;
            chbx_BassDD.Checked = false;
            SaveRecord();
        }

        private void btn_OldFolder_Click(object sender, EventArgs e)
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = TempPath + "\\0_old\\" + databox.Rows[i].Cells["Original_FileName"].Value.ToString();
            try
            {
                Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Old Folder in Explorer ! ");
            }
        }

        private void btn_DuplicateFolder_Click(object sender, EventArgs e)
        {
            string t = TempPath + "\\0_duplicate";
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

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            ////1. Delete Song Folder           

            var cmd = "DELETE FROM Main WHERE ID IN (" + txt_ID.Text + ")";
            var i = databox.SelectedCells[0].RowIndex;

            //1. Delete DB records
            DeleteRecords(txt_ID.Text, cmd, DB_Path, TempPath, "1", databox.Rows[i].Cells["Original_File_Hash"].Value.ToString(), cnb);

            //refresh 
            //var i = DataViewGrid.SelectedCells[0].RowIndex;
            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();

            if (i > 0) databox.FirstDisplayedScrollingRowIndex = i - 1;
            databox.Focus();
            //advance or step back in the song list
            //if (DataViewGrid.Rows.Count > 1)
            //{
            //    var prev = DataViewGrid.SelectedCells[0].RowIndex;
            //    if (DataViewGrid.Rows.Count == prev + 2)
            //        if (prev == 0) return;
            //        else
            //        {
            //            int rowindex;
            //            DataGridViewRow row;
            //            i = DataViewGrid.SelectedCells[0].RowIndex;
            //            rowindex = i;
            //            DataViewGrid.Rows[rowindex - 1].Selected = true;
            //            DataViewGrid.Rows[rowindex].Selected = false;
            //            row = DataViewGrid.Rows[rowindex - 1];
            //        }
            //    else
            //    {
            //        int rowindex;
            //        DataGridViewRow row;
            //        i = DataViewGrid.SelectedCells[0].RowIndex;
            //        rowindex = i;
            //        DataViewGrid.Rows[rowindex + 1].Selected = true;
            //        DataViewGrid.Rows[rowindex].Selected = false;
            //        row = DataViewGrid.Rows[rowindex + 1];
            //    }
            //}
        }

        private void btn_Duplicate_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;

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
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                DataSet ddzv = new DataSet();
                OleDbDataAdapter dat = new OleDbDataAdapter(sel1, cnn);
                dat.Fill(ddzv, "Main");
                dat.Dispose();
                max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                max += 1;
            }
            string t = filePath + max.ToString();
            string source_dir = @filePath;
            string destination_dir = @t;
            var fold = "";
            var fold2 = "";
            try //Copy dir
            {
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

                //copy old
                if (databox.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")
                {
                    fold = TempPath + "\\0_old\\" + databox.Rows[i].Cells["Original_FileName"].Value.ToString();
                    fold2 = Path.GetFileNameWithoutExtension(fold) + "" + max.ToString();
                    File.Copy(fold, fold2.Replace(fold, fold2), true);
                }
                //Directory.Delete(source_dir, true); DONT DELETE

            }
            catch (Exception ee)
            {
                MessageBox.Show("FAILED To copy Files" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }

            //2. Copy Records
            try //Copy dir
            {
                var AlbumArtPath = databox.Rows[i].Cells["AlbumArtPath"].Value.ToString().Replace(source_dir, destination_dir);
                var AudioPath = databox.Rows[i].Cells["AudioPath"].Value.ToString().Replace(source_dir, destination_dir);
                var audioPreviewPath = databox.Rows[i].Cells["audioPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var OggPath = databox.Rows[i].Cells["OggPath"].Value.ToString().Replace(source_dir, destination_dir);
                var oggPreviewPath = databox.Rows[i].Cells["oggPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var insertcmdd = "Song_Title, Song_Title_Sort, Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, AlbumArtPath, AudioPath, audioPreviewPath, Track_No, Author, Version, DLC_Name, DLC_AppID, Current_FileName, Original_FileName, Import_Path, Import_Date, Folder_Name, File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description, Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD, Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, OggPath, oggPreviewPath, UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date,Is_Live,Live_Details,Remote_Path";
                var insertvalues = "SELECT Song_Title+\" alt" + max.ToString() + "\", Song_Title_Sort+\" alt" + max.ToString() + "\", Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, \"" + AlbumArtPath + "\", \"" + AudioPath + "\", \"" + audioPreviewPath + "\", Track_No, Author, Version, DLC_Name+\"" + max.ToString() + "\", DLC_AppID, Current_FileName, \"" + fold2.Replace(fold, fold2) + "\", Import_Path, Import_Date, Folder_Name+\"" + max.ToString() + "\", File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, \"" + "Yes" + "\", Is_Multitrack, Is_Broken, MultiTrack_Version, " + max.ToString() + ", DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description+\" duplicate\", Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD, Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, \"" + OggPath + "\", \"" + oggPreviewPath + "\", UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date,Is_Live,Live_Details,Remote_Path FROM Main  WHERE ID = " + txt_ID.Text;
                InsertIntoDBwValues("Main", insertcmdd, insertvalues, cnb);


                //getting ID
                DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + txt_Title.Text + " alt" + max.ToString() + "\"", "", cnb);
                var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, SNGFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments, Start_Time";
                insertvalues = "SELECT Arrangement_Name, " + CDLC_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(SNGFilePath,len(SNGFilePath)-instr(SNGFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10), XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments, Start_Time FROM Arrangements WHERE CDLC_ID = " + txt_ID.Text;
                InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb);

                insertcmdd = "Tone_Name, CDLC_ID, Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime";
                insertvalues = "SELECT Tone_Name, " + CDLC_ID + ", Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime FROM Tones WHERE CDLC_ID = " + txt_ID.Text; ;
                InsertIntoDBwValues("Tones", insertcmdd, insertvalues, cnb);

                MessageBox.Show("Record has been duplicated");

                //redresh 
                Populate(ref databox, ref Main);
                //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
            catch (Exception ee)
            {
                MessageBox.Show("FAILED To copy REcords" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }
        }

        private async System.Threading.Tasks.Task<int> GetTrackNoAsync()
        {

            //public static async System.Threading.Tasks.Task<int> GetTrackNoAsync(string Artist, string Album, string Title)
            //{

            WebAPIFactory webApiFactory = new WebAPIFactory(
            "http://localhost",
            8000,
            "26d287105e31491889f3cd293d85bfea",
            Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
            Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
            Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);

            try
            {
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //if (_spotify == null)
            //    return 0;

            SInitialSetup();
            //SearchItem item = _spotify.SearchItems(txt_Title.Text, SearchType.Track );//txt_Artist.Text + "+" + | SearchType.Artist
            ////Console.WriteLine(item.Albums.Total); //How many results are there in total? NOTE: item.Tracks = item.Artists = null
            //txt_debug.Text += "\n---" + item.Error.Message.ToString();
            //txt_debug.Text += "\n---" + item.Albums.Total.ToString();

            return 0;

            /*

            var client = new CookieAwareWebClient();
            //    var loginAddress = "http://customsforge.com/index.php?app=core&module=global&section=login";//%20HTTP/1.1";
            //    var loginData = new NameValueCollection { };
            //    client.preLogin(loginAddress, loginData);

            //    loginAddress = "https://customsforge.com/index.php?app=core&module=global&section=login";
            //    loginData = new NameValueCollection
            //    {
            //        {"auth_key", "880ea6a14ea49e853634fbdc5015a024" },
            //        {"referer","http://customsforge.com/" },
            //        {"ips_username", "misterion99" },
            //        {"ips_password", "rahxephon" },
            //        {"rememberMe","1" }
            //};

            //    //var client = new CookieAwareWebClient();
            //    client.Login(loginAddress, loginData);

            //txt_debug.Text = client.ResponseHeaders.ToString();// pageSource;
            //"http://ignition.customsforge.com/search/browse?filters=%7B%22artist%22%3A%22311%22%7D&group=album"
            var txt = "http://ignition.customsforge.com/search/browse?filters=%7B%22artist%22%3A%22" + txt_Artist.Text.Replace(" ", "%20") + "%22%7D&group=album";
            //client.Headers.Add("Cookie", "__cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1"); 


            //WebHeaderCollection myWebHeaderCollection = client.Headers;//Get the headers associated with the request.
            //    myWebHeaderCollection.Add("Accept-Language", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
            //client.Headers = myWebHeaderCollection;
            client.QueryString.Add("Host", "ignition.customsforge.com");
            client.QueryString.Add("Connection", "keep - alive");
            client.QueryString.Add("Cache - Control", "max - age = 0");
            client.QueryString.Add("Upgrade - Insecure - Requests", "1");
            client.QueryString.Add("User-Agent", "Mozilla /5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36 OPR/41.0.2353.69");
            client.QueryString.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,**;q=0.8");
            client.QueryString.Add("Accept-Encoding", "gzip, deflate, lzma, sdch");
            client.QueryString.Add("Accept - Language", "en - US,en; q = 0.8");
            client.Headers.Add("Cookie", "loggedin=logged_in_via_remember; __cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; remember_82e5d2c56bdd0811318f0cf078b78bfc=eyJpdiI6IndmVUUzZ3VQVU42Z3d1YWVKNjdUWUE9PSIsInZhbHVlIjoiZzU1NnZPSWN3S1hReUNwVEhxK1RQWDRNSEdmdFB2MkFBOE43NXgwN3JSZzJKQTBxZnI1d1dERythNnJNRVpCRHduMEFCTDYyWUdmcm9vYjUzemp6OFZjbXJaYUpmekt0cjBCK3NGa2V0a2c9IiwibWFjIjoiNTBkZTY5MTAxM2U4YWIzNDkyMzc0ZDRmMjdiYjk4ZDE1NDFmNmYwYjUwNWUwODk2NWNjODZjMDA1MjJhNDA5YyJ9; last_visit=1480268821466; -community-member_id=0; -community-pass_hash=0; ipsconnect_555568a0a50a95471195ba7cd1461296=0; -community-session_id=b8f94a65942f76fac9d1d26dffeb3e15; __utma=159351336.2130965126.1478445576.1480262916.1480268836.10; __utmc=159351336; __utmz=159351336.1480268836.10.2.utmcsr=ignition.customsforge.com|utmccn=(referral)|utmcmd=referral|utmcct=/dashboard; _ga=GA1.2.2130965126.1478445576; __utma=8165418.2130965126.1478445576.1480268761.1480271320.8; __utmc=8165418; __utmz=8165418.1478451490.2.2.utmcsr=customsforge.com|utmccn=(referral)|utmcmd=referral|utmcct=/index.php; loggedin=logged_in_via_remember; laravel_session=eyJpdiI6IjNHSEs5d04xWEMzRm1lM2dxWlpXR1E9PSIsInZhbHVlIjoiMFFlS0loKzVpZFN4eTNJNkxpSktHRkhWS3A1S1c0TDZRdmJGZG9sejRRNjZNb3hPaDBOREZ2VThTNk1GSE5vZGh1QkZ4azNWUk1BV1BRcnJic2FaRkE9PSIsIm1hYyI6ImE3NzQxOWQ5YmU5OTkwNmE0ZTk4ODk0MjQ4ZGIzYzM3MmNiM2JiN2U2OTZhMmU5ZWI4YzI0NzM0NDZlZWRlMDMifQ%3D%3D");
            Byte[] pageData = client.DownloadData(txt);//"the url of the page behind the login";);
            string pageHtml = System.Text.Encoding.ASCII.GetString(pageData);
            // //Console.WriteLine(pageHtml);
            txt_debug.Text = pageHtml;// pageSource;}
*/

            //    var CleanTitle = "";
            //if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            //if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
            //else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

            //string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
            //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;

        }

        public class CookieAwareWebClient : WebClient
        {
            public void preLogin(string loginPageAddress, NameValueCollection loginData)
            {
                CookieContainer container;

                var request = (HttpWebRequest)WebRequest.Create(loginPageAddress);

                WebHeaderCollection myWebHeaderCollection = request.Headers;//Get the headers associated with the request.
                myWebHeaderCollection.Add("Accept-Language", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
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
                //Accept-Language: en-US,en;q=0.8
                //Cookie: __cfduid=d533a5c9a8a1d92064645aa400f3749ef1478445227; _reamaze_uc=%7B%22fs%22%3A%222016-11-06T15%3A19%3A39.113Z%22%7D; __qca=P0-955620212-1478445749083; -community-rteStatus=rte; _reamaze_sc=1; OX_plg=pm; -community-coppa=0; -community-member_id=180532; -community-pass_hash=79d7e978c9e81c80b6d26037badbf600; ipsconnect_555568a0a50a95471195ba7cd1461296=1; -community-session_id=34de9770ac2dabb37d67e6e1b5ba8007; __utmt=1; __utma=159351336.2130965126.1478445576.1480213859.1480221535.6; __utmb=159351336.2.10.1480221535; __utmc=159351336; __utmz=159351336.1478445576.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); _ga=GA1.2.2130965126.1478445576; _gat=1

                //Please use this,
                //HttpWebRequest request = null;
                //  request = HttpWebRequest.Create(loginPageAddress) as HttpWebRequest;
                HttpWebResponse TheRespone = (HttpWebResponse)request.GetResponse();
                String setCookieHeader = TheRespone.Headers[HttpResponseHeader.SetCookie];

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
                myWebHeaderCollection.Add("Accept-Language", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
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

                // container = request.CookieContainer = new CookieContainer();

                var response = request.GetResponse();
                response.Close();
                //  CookieContainer = container;
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
                myWebHeaderCollection.Add("Accept-Language", "en-US,en;q=0.8");//Include English in the Accept-Langauge header. 
                request.Headers = myWebHeaderCollection;

                //var request = (HttpWebRequest)base.GetWebRequest(address);
                request.CookieContainer = CookieContainer;
                return request;
            }
        }


        //public class AutorizationCodeAuth
        //{
        //    public delegate void OnResponseReceived(AutorizationCodeAuthResponse response);

        //    private SimpleHttpServer _httpServer;
        //    private Thread _httpThread;
        //    public String ClientId { get; set; }
        //    public String RedirectUri { get; set; }
        //    public String State { get; set; }
        //    public Scope Scope { get; set; }
        //    public Boolean ShowDialog { get; set; }

        //    /// <summary>
        //    ///     Will be fired once the user authenticated
        //    /// </summary>
        //    public event OnResponseReceived OnResponseReceivedEvent;

        //    /// <summary>
        //    ///     Start the auth process (Make sure the internal HTTP-Server ist started)
        //    /// </summary>
        //    public void DoAuth()
        //    {
        //        String uri = GetUri();
        //        Process.Start(uri);
        //    }

        //    /// <summary>
        //    ///     Refreshes auth by providing the clientsecret (Don't use this if you're on a client)
        //    /// </summary>
        //    /// <param name="refreshToken">The refresh-token of the earlier gathered token</param>
        //    /// <param name="clientSecret">Your Client-Secret, don't provide it if this is running on a client!</param>
        //    public Token RefreshToken(string refreshToken, string clientSecret)
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Proxy = null;
        //            wc.Headers.Add("Authorization",
        //                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + clientSecret)));
        //            NameValueCollection col = new NameValueCollection
        //        {
        //            {"grant_type", "refresh_token"},
        //            {"refresh_token", refreshToken}
        //        };

        //            String response;
        //            try
        //            {
        //                byte[] data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
        //                response = Encoding.UTF8.GetString(data);
        //            }
        //            catch (WebException e)
        //            {
        //                using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
        //                {
        //                    response = reader.ReadToEnd();
        //                }
        //            }
        //            return JsonConvert.DeserializeObject<Token>(response);
        //        }
        //    }

        //    private String GetUri()
        //    {
        //        StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize/?");
        //        builder.Append("client_id=" + ClientId);
        //        builder.Append("&response_type=code");
        //        builder.Append("&redirect_uri=" + RedirectUri);
        //        builder.Append("&state=" + State);
        //        builder.Append("&scope=" + Scope.GetStringAttribute(" "));
        //        builder.Append("&show_dialog=" + ShowDialog);
        //        return builder.ToString();
        //    }

        //    /// <summary>
        //    ///     Start the internal HTTP-Server
        //    /// </summary>
        //    public void StartHttpServer(int port = 80)
        //    {
        //        _httpServer = new SimpleHttpServer(port, AuthType.Authorization);
        //        _httpServer.OnAuth += HttpServerOnOnAuth;

        //        _httpThread = new Thread(_httpServer.Listen);
        //        _httpThread.Start();
        //    }

        //    private void HttpServerOnOnAuth(AuthEventArgs e)
        //    {
        //        OnResponseReceivedEvent?.Invoke(new AutorizationCodeAuthResponse()
        //        {
        //            Code = e.Code,
        //            State = e.State,
        //            Error = e.Error
        //        });
        //    }

        //    /// <summary>
        //    ///     This will stop the internal HTTP-Server (Should be called after you got the Token)
        //    /// </summary>
        //    public void StopHttpServer()
        //    {
        //        _httpServer = null;
        //    }

        //    /// <summary>
        //    ///     Exchange a code for a Token (Don't use this if you're on a client)
        //    /// </summary>
        //    /// <param name="34392dbf46d04a94b778de26f2324472">The gathered code from the response</param>
        //    /// <param name="7e940a33ba274f5a88fe9ef7934b74d4">Your Client-Secret, don't provide it if this is running on a client!</param>
        //    /// <returns></returns>
        //    public Token ExchangeAuthCode(String code, String clientSecret)
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Proxy = null;

        //            NameValueCollection col = new NameValueCollection
        //        {
        //            {"grant_type", "authorization_code"},
        //            {"code", code},
        //            {"redirect_uri", RedirectUri},
        //            {"client_id", "34392dbf46d04a94b778de26f2324472"},//ClientId
        //            {"client_secret", "7e940a33ba274f5a88fe9ef7934b74d4"}//clientSecret
        //        };

        //            String response;
        //            try
        //            {
        //                byte[] data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
        //                response = Encoding.UTF8.GetString(data);
        //            }
        //            catch (WebException e)
        //            {
        //                using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
        //                {
        //                    response = reader.ReadToEnd();
        //                }
        //            }
        //            return JsonConvert.DeserializeObject<Token>(response);
        //        }
        //    }
        //}

        //public struct AutorizationCodeAuthResponse
        //{
        //    public String Code { get; set; }
        //    public String State { get; set; }
        //    public String Error { get; set; }
        //}


        public static int GetTrackNo(string Artist, string Album, string Title)
        {
            string uriString = "https://api.spotify.com/v1/search";
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("query", keywordString);
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
            catch (Exception ee) { Console.WriteLine(ee.Message); }
            a1 = a1.Trim();
            if (a1 == "" && Album != "")
            {
                a1 = GetTrackNo(Artist, "", Title).ToString();
            }
            if (a1 == "" && Artist != "")
            {
                a1 = GetTrackNo("", "", Title).ToString();
            }
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
        }


        public async void SInitialSetup()
        {
            // if (InvokeRequired)
            // {
            //     Invoke(new System.Action(SInitialSetup));
            //     return;
            // }

            // //authButton.Enabled = false;
            // _profile = await _spotify.GetPrivateProfileAsync();

            // _savedTracks = GetSavedTracks();
            // txt_debug.Text +=  "\n"+_savedTracks.Count.ToString();
            // _savedTracks.ForEach(track => savedTracksListView.Items.Add(new ListViewItem()
            // {
            //     Text = track.Name,
            //     SubItems = { string.Join(",", track.Artists.Select(source => source.Name)), track.Album.Name }
            // }));

            // _playlists = GetPlaylists();
            // txt_debug.Text += "\n" + _playlists.Count.ToString();
            // _playlists.ForEach(playlist => playlistsListBox.Items.Add(playlist.Name));

            // txt_debug.Text += "\n" + _profile.DisplayName;
            // txt_debug.Text += "\n" + _profile.Country;
            // txt_debug.Text += "\n" + _profile.Email;
            // txt_debug.Text += "\n" + _profile.Product;

            // if (_profile.Images != null && _profile.Images.Count > 0)
            // {
            //     using (WebClient wc = new WebClient())
            //     {
            //         byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(_profile.Images[0].Url));
            //         using (MemoryStream stream = new MemoryStream(imageBytes))
            //             picbx_AlbumArtPath.Image = Image.FromStream(stream);
            //     }
            // }

            // SearchItem item = _spotify.SearchItems(txt_Title.Text, SearchType.Track); //txt_Artist.Text + "+" +| SearchType.Artist
            //// Console.WriteLine(item.Albums.Total); //How many results are there in total? NOTE: item.Tracks = item.Artists = null
            // txt_debug.Text += "\n---" + item.Tracks.Total;
        }

        private List<FullTrack> GetSavedTracks()
        {
            Paging<SavedTrack> savedTracks = _spotify.GetSavedTracks();
            List<FullTrack> list = savedTracks.Items.Select(track => track.Track).ToList();

            while (savedTracks.Next != null)
            {
                savedTracks = _spotify.GetSavedTracks(20, savedTracks.Offset + savedTracks.Limit);
                list.AddRange(savedTracks.Items.Select(track => track.Track));
            }

            return list;
        }

        private List<SimplePlaylist> GetPlaylists()
        {
            Paging<SimplePlaylist> playlists = _spotify.GetUserPlaylists(_profile.Id);
            List<SimplePlaylist> list = playlists.Items.ToList();

            while (playlists.Next != null)
            {
                playlists = _spotify.GetUserPlaylists(_profile.Id, 20, playlists.Offset + playlists.Limit);
                list.AddRange(playlists.Items);
            }

            return list;
        }

        //void spotifyToken()
        //{
        //    NSString *body = @"grant_type=client_credentials";
        //    NSData *postData = [body dataUsingEncoding:NSASCIIStringEncoding allowLossyConversion:YES];
        //    NSString *prepareHeader = [NSString stringWithFormat:@"%@:%@", clientId, clientSecret];
        //    NSData *data = [prepareHeader dataUsingEncoding:NSUTF8StringEncoding];
        //    NSString *base64encoded = [data base64EncodedStringWithOptions:0];
        //    NSString *header = [NSString stringWithFormat:@"Basic %@", base64encoded];

        //    NSMutableURLRequest *request = [[NSMutableURLRequest alloc]
        //    init];
        //    [request setURL:[NSURL URLWithString:@"https://accounts.spotify.com/api/token"]];
        //    [request setHTTPBody:postData];
        //    [request setHTTPMethod:@"POST"];
        //    [request setValue:header forHTTPHeaderField:@"Authorization"];

        //    NSURLSession *session = [NSURLSession sessionWithConfiguration:[NSURLSessionConfiguration defaultSessionConfiguration]];
        //    [[session dataTaskWithRequest:request completionHandler:^(NSData* _Nullable data, NSURLResponse * _Nullable response, NSError * _Nullable error) {
        //if (!error) {
        //    dispatch_async(dispatch_get_main_queue(), ^{
        //    // saving somewhere token for further using
        //        //});
        //        //}
        //}] resume];
        //}       

        private async Task bth_GetTrackNo_ClickAsync(object sender, EventArgs e)
        {
            var CleanTitle = "";
            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

            //string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
            int z = await GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, txt_Title.Text, txt_Album_Year.Text, txt_SpotifyStatus.Text);
            txt_Track_No.Text = z == 0 && txt_Track_No.Text != "" ? txt_Track_No.Text : z.ToString();
            //var i = DataViewGrid.SelectedCells[0].RowIndex;

            //if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "No"; }
            //else { chbx_TrackNo.Checked = true; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "Yes"; }
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
            Process.Start("IExplore.exe", txt_Playthough.Text);
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
            var cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + txt_AlbumArtPath.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" and Album=\"" + txt_Album.Text + "\"";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);
            Standardization.MakeCover(cnb);
        }

        private void btn_AddSections_Click(object sender, EventArgs e)
        {
            var j = databox.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppWD, "bpr_v0.3\\bpr.exe");
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_InvertSelect_Click(object sender, EventArgs e)
        {
            //var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            //var command = cnn.CreateCommand();

            //command.CommandText = "UPDATE Main SET ";
            //command.CommandText += "Selected = @param8 ";
            //command.Parameters.AddWithValue("@param8", "Yes");
            //var test = "";
            //if (chbx_InclBeta.Checked)
            //{
            //    command.CommandText += ",Is_Beta = @param9 ";
            //    command.Parameters.AddWithValue("@param9", "Yes");
            //    test = " or Beta";
            //}
            //command.CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            //try
            //{
            //    command.CommandType = CommandType.Text;
            //    cnn.Open();
            //    command.ExecuteNonQuery();
            //    cnn.Close();
            //    command.Dispose();
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            //Populate(ref DataViewGrid, ref Main);
            ////DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            //DataViewGrid.Refresh();
            //DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID Not IN(" + SearchCmd.Replace(" * ", "ID").Replace("; ", "").Replace(SearchFields, "ID") + ")");

            var cmd1 = "UPDATE Main SET Selected = \"No\"";
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                cmd1 += ",Is_Beta = \"No\" ";
                test = " and Not Beta";
            }
            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";

                DeleteFromDB("Groups", cmd, cnb);
                //var insertcmdd = "CDLC_ID, Groups, Type";
                //var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"DLC\"";
                //InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb);
                test += " and no Groups";
            }
            cmd1 += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            DataSet dgt = UpdateDB("Main", cmd1 + ";", cnb);
            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();
            var cnt = 0;
            if (dgt.Tables.Count > 0) cnt = dgt.Tables[0].Rows.Count;
            Update_Selected();
            MessageBox.Show("All Filtered songs have been marked as UnSelected" + test);//(" + cnt + ")
        }

        private void btn_Copy_old_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            DataSet dhs = new DataSet(); var cmd = "SELECT * FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")"; dhs = SelectFromDB("Main", cmd, "", cnb);
            noOfRec = dhs.Tables[0].Rows.Count;
            var dest = "";
            pB_ReadDLCs.Maximum = noOfRec;
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                string filePath = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19];
                dest = RocksmithDLCPath + "\\" + dhs.Tables[0].Rows[i].ItemArray[19];
                var eef = dhs.Tables[0].Rows[i].ItemArray[87].ToString();
                if (eef == "Yes")//OLd available
                {
                    try
                    {
                        File.Copy(filePath, dest, true);
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                        MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ee);
                    }
                }
                pB_ReadDLCs.Value++;
            }
            MessageBox.Show(noOfRec + " Files Copied to " + RocksmithDLCPath + "\\");
        }

        private void btn_SelectInverted_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            var command = cnn.CreateCommand();

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
            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            var CommandText = "UPDATE Main SET ";
            CommandText += "Selected = \"Yes\" ";
            //command.Parameters.AddWithValue("@param8", "Yes");
            if (chbx_InclBeta.Checked)
                //{
                command.CommandText += ",Is_Beta = \"Yes\" ";
            //command.Parameters.AddWithValue("@param9", "Yes");
            //}

            CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")";
            UpdateDB("Main", CommandText, cnb);
            //try
            //{
            //    command.ExecuteNonQuery();
            //    cnn.Close();
            //    command.Dispose();
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            //if (chbx_InclGroups.Checked)
            //{
            //    var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups < \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";

            //    DeleteFromDB("Groups", cmd);
            //    var insertcmdd = "CDLC_ID, Groups, Type";
            //    var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"DLC\"";
            //    InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb);
            //}

            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();

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
                    btn_Search.PerformClick();
        }

        private void btn_Beta_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
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
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            Populate(ref databox, ref Main);
            //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", "Select * FROM Main WHERE ID IN(" + SearchCmd.Replace(" * ", "ID").Replace("; ", "").Replace(SearchFields, "ID") + ")", "", cnb);
            MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Beta");
        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            eof();
        }

        public void eof()
        {
            var i = databox.SelectedCells[0].RowIndex;
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            var paath = ConfigRepository.Instance()["dlcm_EoFPath"];
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "\\eof1.8RC12\\eof.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            startInfo.Arguments = String.Format(" \"" + txt_OggPath.Text + "\"");
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); //DDC.WaitForExit(1000 * 60 * 1);
                }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btn_CreateLyrics_Click(object sender, EventArgs e)
        {
            //1. Open Internet Explorer

            var i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + databox.Rows[i].Cells["Artist"].Value.ToString() + "+" + databox.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "Lyrics";
            MessageBox.Show("http://www.guitarcade.fr/en/cdlc-creation/advanced-features/add-lyrics/ /n/n1.Google the Lyrics e.g." + link + " \n\n2. Use Ultrastar Creator Tab lyrics to the songs time signature (if crashing at play open it from outside DLC Manager)\n\n3. Using EditorOnFire Transform tabbed lyrics to Rocksmith Format(File->Import->L>rics - Song->Track->Vocals then Adjust-Manually and Save)\n\n\n\n4. When done press Import lyrics by using Change Lyrics button", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);


            try
            {
                Process process = Process.Start(@link);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }

            //2. Open Song Folder
            string filePath = databox.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }

            //3. Open Ultrastar pointing at the Song Ogg
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //var xx = Path.Combine(AppWD, "UltraStar Creator\\usc.exe");
            var paath = ConfigRepository.Instance()["dlcm_UltraStarCreator"];
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "\\UltraStar Creator\\usc.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");
            startInfo.Arguments = String.Format(" \"" + txt_OggPath.Text + "\"");
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start();// DDC.WaitForExit(1000 * 60 * 1);
                    try
                    {
                        Process process = Process.Start(@xx);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can't not open Song Folder in Exporer ! ");
                    }
                }

            //4. Open EoF
            eof();

            //5. Import PART VOCALS_RS2.xml

        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text == "") return;
            DataSet drs = new DataSet(); drs = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Groups=\"" + chbx_Group.Text + "\" and Type=\"DLC\";", "", cnb);
            var norec = drs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT MAX(CDLC_ID) FROM Groups WHERE Type=\"DLC\";", "", cnb);

                norec = ds.Tables[0].Rows.Count;
                if (norec > 0)
                {
                    var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    string insertcmdA = "CDLC_ID, Profile_Name, Type, Comments, Groups";
                    var insertA = "\"" + fnn + "\",\"\",\"DLC\",\"\",\"" + chbx_Group.Text + "\"";
                    InsertIntoDBwValues("Groups", insertcmdA, insertA, cnb);
                    UpdateGroups();
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
                //tst = "Stop selecting groups of this song... "; timestamp = UpdateLog(timestamp, tst, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\"", cnb);
            GroupChanged = true;
            UpdateGroups();// ChangeRow();
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
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = txt_OggPath.Text;
            var tt = t.Replace(".ogg", ".wav");
            startInfo.Arguments = String.Format(" \"{0}\" ", t);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        string filePath = tt.Substring(0, tt.LastIndexOf("\\"));
                        try
                        {
                            Process process = Process.Start(@filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Garageband ready Song Folder in Exporer ! ");
                        }
                    }
                }
        }

        private void btn_Artist2SortA_Click(object sender, EventArgs e)
        {

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
            var temppath = String.Empty;
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
                temppath = fbd.FileName;

                //Generating the HASH code
                var FileHash = "";
                FileHash = GetHash(temppath);
                //using (FileStream fs = File.OpenRead(temppath))
                //{
                //    SHA1 sha = new SHA1Managed();
                //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                //    fs.Close();
                //}

                var outputFile = Path.Combine(Path.GetDirectoryName(temppath), String.Format("{0}.sng", Path.GetFileNameWithoutExtension(temppath)));

                using (FileStream outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite))
                {
                    Sng2014File sng = Sng2014File.ConvertXML(temppath, ArrangementType.Vocal);
                    sng.WriteSng(outputStream, new Platform(txt_Platform.Text.ToString(), GameVersion.RS2014.ToString()));
                }

                //MessageBox.Show(String.Format("SNG file was generated! {0}It was saved on same location of xml file specified.", Environment.NewLine), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                var SNGHash = "";
                SNGHash = GetHash(outputFile);
                //using (FileStream fs = File.OpenRead(outputFile))
                //{
                //    SHA1 sha = new SHA1Managed();
                //    SNGHash = BitConverter.ToString(sha.ComputeHash(fs));
                //    fs.Close();
                //}

                DataSet dsr = new DataSet();
                if (chbx_Lyrics.Checked)
                {
                    //in case update command is not working
                    //var Comments = "";DataSet dms = new DataSet(); dms = SelectFromDB("Arrangements", "SELECT Comments FROM Arrangements WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";", "");
                    //var noOfRec = dms.Tables[0].Rows.Count;
                    //if (noOfRec>0) Comments = dms.Tables[0].Rows[i].ItemArray[0].ToString();
                    dsr = UpdateDB("Arrangements", "UPDATE Arrangements SET XMLFilePath=\"" + temppath + "\", XMLFile_Hash=\"" + FileHash + "\",SNGFilePath=\"" + outputFile + "\", SNGFile_Hash=\"" + SNGHash + "\", Comments= Comments +\"\nadded with DLCManager\" WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";", cnb);
                }
                else
                {
                    var insertcmdd = "Arrangement_Name,CDLC_ID, ArrangementType, Bonus, ArrangementSort, TuningPitch, RouteMask, Has_Sections, XMLFilePath, XMLFile_Hash, SNGFilePath, SNGFileHash, Comments";
                    var insertvalues = "\"4\", " + txt_ID.Text + ", \"Vocal\", \"false\", \"0\", \"0\", \"None\",\"No\",\"" + temppath + "\",\"" + FileHash + "\",\"" + outputFile + "\",\"" + SNGHash + "\",\"" + "added with DLCManager" + "\"";
                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb);
                }

                //dag.Fill(dsr, "Main");
                txt_Lyrics.Text = temppath;
                chbx_Lyrics.Checked = true;
                SaveRecord();
            }
        }

        private void btn_ApplyShortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Artist_Short = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet dus = UpdateDB("Main", cmd1 + ";", cnb);
            cmd1 = "UPDATE Main SET Artist_ShortName = \"" + txt_Artist_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\"";
            DataSet difg = UpdateDB("Main", cmd1 + ";", cnb);
        }

        private void btn_ApplyAlbumSortNames_Click(object sender, EventArgs e)
        {
            var cmd1 = "UPDATE Standardization SET Album_Short = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dus = UpdateDB("Main", cmd1 + ";", cnb);
            cmd1 = "UPDATE Main SET Album_ShortName = \"" + txt_Album_ShortName.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            DataSet dhj = UpdateDB("Main", cmd1 + ";", cnb);
        }

        private void DataViewGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
            //var line = -1;
            //line = DataViewGrid.SelectedCells[0].RowIndex;
            //if (line > -1) ChangeRow();
            //pB_ReadDLCs.Value = 0;
        }

        private void MainDB_Leave(object sender, EventArgs e)
        {
            //var SearchCmd = "SELECT * FROM Main u ";
            //DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Main", SearchCmd);
            //var noOfRec = dsz1.Tables[0].Rows.Count;

            //SearchCmd = SearchCmd + " WHERE (Selected=\"Yes\");";
            //DataSet dsz2 = new DataSet(); dsz2 = SelectFromDB("Main", SearchCmd);

            //var noOfSelRec = dsz2.Tables[0].Rows.Count;
            //DLCManager.LabelTextt = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records.";
        }

        private void btn_ReadGameLibrary_Click(object sender, EventArgs e)
        {
            // Process the list of files found in the directory.
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var logmss = "Starting... " + startT; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var pathx = txt_FTPPath.Text;
            UpdateDB("Main", "Update Main Set Remote_Path = \"\";", cnb); logmss = "Cleared Remote Songs... "; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            string[] fileEntries = new string[10000];
            if (chbx_Format.Text == "PS3") fileEntries = GetFTPFiles(pathx);
            else try { fileEntries = Directory.GetFiles(pathx, "*" + (chbx_Format.Text == "PC" ? "_p." : chbx_Format.Text == "Mac" ? "_m." : chbx_Format.Text == "XBOX360" ? "" : "") + "psarc*", SearchOption.TopDirectoryOnly); }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
            var z = 0;
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
                //Get (and SAve each New File Name in the FilesMissingIssues filed)
                foreach (string fileName in fileEntries)
                {
                    tst = pB_ReadDLCs.Value + "/" + z + "-" + Path.GetFileName(fileName);
                    pB_ReadDLCs.Increment(50);
                    pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    if (fileName == null) break;
                    if (fileName.IndexOf("s1compatibility") > 0)
                        continue;
                    z++; logmss = "Reading... " + fileName + "..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileName=\"" + Path.GetFileName(fileName) + "\";", "", cnb);
                    norec = dfs.Tables[0].Rows.Count;
                    if (norec == 0) newn += fileName + "\";\"";
                    else
                    {
                        logmss = "Old song ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var dlcid = dfs.Tables[0].Rows[0].ItemArray[0].ToString();
                        if (("-" + found).IndexOf("\"" + dlcid + "\"") <= 0)
                        {
                            found += dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(fileName) + "\" WHERE ID=" + dlcid + ";", cnb);
                            logmss = "Unique song the Remote location ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        }
                        else
                        {
                            var fn = "";
                            DataSet dfg = new DataSet(); dfg = SelectFromDB("Main", "SELECT ID,Remote_Path FROM Main WHERE ID=" + dlcid + ";", "", cnb);
                            DialogResult result1 = MessageBox.Show("Duplicate DLC has been found!\n\nChose which to imediatelly delete:\n\n1. " + dfg.Tables[0].Rows[0].ItemArray[1].ToString() + "\n2. " + Path.GetFileName(fileName) + "\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (result1 == DialogResult.Yes)
                                fn = dfg.Tables[0].Rows[0].ItemArray[1].ToString();
                            else if (result1 == DialogResult.No) fn = Path.GetFileName(fileName);
                            //else;
                            if (result1 == DialogResult.No || result1 == DialogResult.Yes)
                            {
                                if (chbx_Format.Text == "PS3")
                                {
                                    var FTPPath = "";
                                    if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                                    else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                                    DeleteFTPFiles(fn, FTPPath);
                                }
                                else
                                {
                                    try
                                    {
                                        if (!File.Exists(txt_FTPPath.Text + "\"" + fn))
                                            File.Move(txt_FTPPath.Text + "\\" + fn, txt_FTPPath.Text + "\\" + fn.Replace(".psarc", ".dupli"));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Write(ex);
                                    }

                                }
                            }
                            logmss = "Duplicate song on the Remote location ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                        tt = TempPath + "\\..\\" + t;
                        string a = "ok";
                        var platform = tt.GetPlatform();
                        var platformTXT = tt.GetPlatform().platform.ToString();
                        logmss = "New song to the Library..." + pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + " ...Starting additional/metadata based checks" + t + "...."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (chbx_Format.Text == "PS3" || platformTXT == "PS3")
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
                                    logmss = "Start Unpacck & Read ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    unpackedDir = Packer.Unpack(tt, TempPath, true, true, null);
                                    info = DLCPackageData.LoadFromFolder(unpackedDir, platform);
                                    logmss = "Stop  Unpack&Read ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                                    //Generating the HASH code
                                    var FileHash = ""; FileHash = GetHash(tt);
                                    //using (FileStream fs = File.OpenRead(tt))
                                    //{
                                    //    SHA1 sha = new SHA1Managed();
                                    //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                                    //    fs.Close();
                                    //}
                                    DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + info.Name + "\";", "", cnb);//+ song.Identifier + "\";");
                                    DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + info.SongInfo.SongDisplayName + "\";", "", cnb); //song.Title
                                    DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);
                                    DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + info.Name.Substring(5, info.Name.Length - 5) + "\";", "", cnb);
                                    DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"*" + info.Name.Substring(5, info.Name.Length - 5) + "*\";", "", cnb);
                                    DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"*" + info.SongInfo.SongDisplayName + "*\";", "", cnb);
                                    noreca = dfa.Tables[0].Rows.Count; norecb = dfb.Tables[0].Rows.Count; norecc = dfc.Tables[0].Rows.Count; norecd = dfd.Tables[0].Rows.Count; norece = dfe.Tables[0].Rows.Count; norecf = dff.Tables[0].Rows.Count;
                                    DataSet fxd = new DataSet();
                                    if (norecc == 1) fxd = dfc;
                                    else if (noreca == 1) fxd = dfa;
                                    else if (norecb == 1) fxd = dfb;
                                    else if (norecf == 1) fxd = dff;
                                    else if (norecd == 1) fxd = dfd;
                                    else if (norece == 1) fxd = dfe;
                                    logmss = "Stop check based on metadata ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    if (noreca == 1 || norecb == 1 || norecc == 1 || norecd == 1 || norece == 1 || norecf == 1)
                                    {
                                        System.IO.FileInfo fi = null; //calc file size
                                        try { fi = new System.IO.FileInfo(tt); }
                                        catch (Exception ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); }
                                        if (("-" + found).IndexOf(fxd.Tables[0].Rows[0].ItemArray[0].ToString()) <= 0)
                                        {
                                            var fnn = t;
                                            string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                            var fnnon = Path.GetFileName(fnn);
                                            var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                            var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + info.Name + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\"";
                                            InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb);
                                            found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb);
                                            DeleteFile(tt);
                                            logmss = "Stop adding song based on newly read metadata..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                                                if (chbx_Format.Text == "PS3")
                                                {
                                                    var FTPPath = "";
                                                    if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                                                    else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                                                    DeleteFTPFiles(fn, FTPPath);
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        if (!File.Exists(txt_FTPPath.Text + "\"" + fn))
                                                            File.Move(txt_FTPPath.Text + "\"" + fn, txt_FTPPath.Text + "\"" + fn.Replace(".psarc", ".dupli"));
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Write(ex);
                                                    }
                                                }
                                            }

                                            logmss = "Stop identifying old song based on newly read metadata..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                        }
                                    }
                                    else newn += t + "\";";
                                    try { DeleteDirectory(unpackedDir); } catch (Exception ee) { MessageBox.Show("4011 " + ee.Message + unpackedDir); }
                                }
                                catch (Exception ee)
                                {
                                    logmss = "Error ar Unpacck Or song Read/Load ..." + ee; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    newn += t + "\";\"";
                                }
                                pB_ReadDLCs.Increment(1);
                            }
                            else newn += t + "\";\"";
                        }
                        else
                        {
                            logmss = "Read non PS3 song ..." + pB_ReadDLCs.Value + " / " + pB_ReadDLCs.Maximum + "..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            if (!File.Exists(tt)) try { File.Copy(t, tt, true); } catch (Exception ee) { MessageBox.Show("4020 " + ee.Message); }

                            //quickly read PSARC for basic data
                            var browser = new PsarcBrowser(tt);
                            var songList = browser.GetSongList();
                            var toolkitInfo = browser.GetToolkitInfo();
                            foreach (var song in songList)
                            {
                                var noreca = 0; var norecb = 0; var norecc = 0; var norecd = 0; var norece = 0; var norecf = 0;
                                //Generating the HASH code
                                var FileHash = ""; FileHash = GetHash(tt);
                                //using (FileStream fs = File.OpenRead(tt))
                                //{
                                //    SHA1 sha = new SHA1Managed();
                                //    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                                //    fs.Close();
                                //}
                                logmss = "Ccheccking non PS3 against metadata..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                DataSet dfa = new DataSet(); dfa = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + song.Identifier + "\";", "", cnb);
                                DataSet dfb = new DataSet(); dfb = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + song.Title + "\";", "", cnb);
                                DataSet dfc = new DataSet(); dfc = SelectFromDB("Pack_AuditTrail", "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);
                                DataSet dff = new DataSet(); dff = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + song.Identifier.Substring(5, song.Identifier.Length - 5) + "\";", "", cnb);
                                DataSet dfd = new DataSet(); dfd = SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"*" + song.Identifier.Substring(5, song.Identifier.Length - 5) + "*\";", "", cnb);
                                DataSet dfe = new DataSet(); dfe = SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title like \"*" + song.Title + "*\";", "", cnb);
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
                                    logmss = "Adding non PS3 song ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    //calc file size
                                    System.IO.FileInfo fi = null;
                                    try { fi = new System.IO.FileInfo(tt); }
                                    catch (System.IO.FileNotFoundException ee) { Console.Write(ee); ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false); frm1.ShowDialog(); }

                                    var fnn = t;
                                    string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                    var fnnon = Path.GetFileName(fnn);
                                    var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                                    var insertA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + song.Identifier + "\",\"" + fnnon.GetPlatform().platform.ToString() + "\"";
                                    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb);
                                    found += fxd.Tables[0].Rows[0].ItemArray[0].ToString() + "\";\"";
                                    DataSet dxr = new DataSet(); UpdateDB("Main", "Update Main Set Remote_Path = \"" + Path.GetFileName(t) + "\" WHERE ID=" + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ";", cnb);
                                    try { DeleteFile(tt); } catch (Exception ee) { MessageBox.Show("4067 " + ee.Message); }
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
            MessageBox.Show("Song Recognized/Read " + found.Split(';').Length + "/" + z + " from " + txt_FTPPath.Text + "\n\nUnrecognized:\n" + newn);
            if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ";
            try
            {
                this.databox.DataSource = null; //Then clear the rows:
                this.databox.Rows.Clear();//                Then set the data source to the new list:
                dssx.Dispose();
                Populate(ref databox, ref Main);
                //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
            catch (System.IO.FileNotFoundException ee) { MessageBox.Show("4098 " + ee.Message + "Can't run Filter ! " + SearchCmd); }
            logmss = "Done REad Library ..."; timestamp = UpdateLog(timestamp, logmss, false, logPath, TempPath, "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
        }

        private void btn_RemoveRemoteSong_Click(object sender, EventArgs e)
        {
            var outp = "";
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\";", cnb);
            if (chbx_Format.Text == "PS3")
            {
                var FTPPath = "";
                if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                outp = DeleteFTPFiles(txt_RemotePath.Text, FTPPath);
            }
            else
            {
                try
                {
                    var tg = (RocksmithDLCPath + "\\" + txt_RemotePath.Text).Replace(".psarc", ".dupli");
                    if (File.Exists(RocksmithDLCPath + "\\" + txt_RemotePath.Text))
                        File.Move(RocksmithDLCPath + "\\" + txt_RemotePath.Text, tg);
                    outp = "ok";
                }
                catch (Exception ex) { Console.Write(ex); }
            }
            txt_RemotePath.Text = "";
            MessageBox.Show(outp);
        }

        private void btn_RemoveAllRemoteSongs_Click(object sender, EventArgs e)
        {
            //GetDirList and calcualte hash for the IMPORTED file
            string[] filez;
            //if (ConfigRepository.Instance()["dlcm_FTP1"]=="Yes") //38. Import other formats but PC, as well(separately of course)
            //    filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc*");
            //else 
            if (chbx_Format.Text == "PS3")
            {
                filez = System.IO.Directory.GetFiles(RocksmithDLCPath, (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    var FTPPath = "";
                    if (ConfigRepository.Instance()["dlcm_FTP"] == "EU") FTPPath = ConfigRepository.Instance()["dlcm_FTP1"];
                    else FTPPath = ConfigRepository.Instance()["dlcm_FTP2"];
                    DeleteFTPFiles(s, FTPPath);
                }

                filez = System.IO.Directory.GetFiles(RocksmithDLCPath, (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
            }
            else
            {
                filez = System.IO.Directory.GetFiles(RocksmithDLCPath, (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
                pB_ReadDLCs.Maximum = filez.Count();
                foreach (string s in filez)
                {
                    if (s == "rs1compatibilitydisc_m.psarc" || s == "rs1compatibilitydisc_p_Pc.psarc" || s == "rs1compatibilitydlc_p.psarc" || s == "rs1compatibilitydlc_m.psarc") continue;
                    try
                    {
                        var tg = (RocksmithDLCPath + "\\" + s).Replace(".psarc", ".dupli");
                        if (File.Exists(RocksmithDLCPath + "\\" + s))
                            File.Move(RocksmithDLCPath + "\\" + s, tg);
                    }
                    catch (Exception ex) { Console.Write(ex); }
                }

            }
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"\";", cnb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pB_ReadDLCs.Value = 0;
            //DataSet dhs = new DataSet(); var cmd = "SELECT * FROM Main WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "").Replace(SearchFields, "ID") + ")"; dhs = SelectFromDB("Main", cmd);
            //noOfRec = dhs.Tables[0].Rows.Count;
            //var dest = "";
            //pB_ReadDLCs.Maximum = noOfRec;
            //for (var i = 0; i <= noOfRec - 1; i++)
            //{
            var i = databox.SelectedCells[0].RowIndex;
            var filename = databox.Rows[i].Cells["Original_FileName"].Value.ToString();
            string filePath = TempPath + "\\0_old\\" + filename;
            var dest = RocksmithDLCPath + "\\" + filename;
            //var eef = dhs.Tables[0].Rows[i].ItemArray[87].T/*oString();*/
            if (databox.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")//OLd available
            {
                try
                {
                    File.Copy(filePath, dest, true);
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ee);
                }
            }
            //    pB_ReadDLCs.Value++;
            //}
            MessageBox.Show("Old/Iinitially imported File Copied to " + RocksmithDLCPath + "\\");
        }

        private void btn_Find_FilesMissingIssues_Click(object sender, EventArgs e)
        {
            //Checkfolders
            //string[] filez;
            string filePath = TempPath + "\\";//0_old\\ + dhs.Tables[0].Rows[i].ItemArray[19];
            //filez = System.IO.Directory.EnumerateDirectories(filePath);
            // LINQ query.
            var dirs = from dir in
                     Directory.EnumerateDirectories(filePath, "*")
                       select dir;
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = dirs.Count();
            pB_ReadDLCs.Increment(1);
            foreach (string s in dirs)
            {
                if (s.IndexOf("0_old") <= 0 && s.IndexOf("0_repacked") <= 0 && s.IndexOf("0_duplicate") <= 0 && s.IndexOf("0_dlcpacks") <= 0 && s.IndexOf("0_broken") <= 0)
                {
                    var cmd = "SELECT * FROM Main WHERE Folder_Name=\"" + s + "\" ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                    DataSet drs = new DataSet(); drs = SelectFromDB("Main", cmd, "", cnb);
                    var noOfFlds = 0;
                    //if( s.IndexOf("folder")>0)
                    //     noOfFlds = 0; ;
                    if (drs.Tables.Count > 0) noOfFlds = drs.Tables[0].Rows.Count;
                    if (noOfFlds == 0) DeleteDirectory(s);
                    pB_ReadDLCs.Increment(1);
                }
            }
            //case "Main_Find_FilesMissingIssues":
            //cleanup before checks
            var cmdupd = "UPDATE Main Set FilesMissingIssues =\"\"";
            DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb);

            DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT * FROM Main;", "", cnb);
            var noOfRec = dms.Tables[0].Rows.Count;
            var vFilesMissingIssues = "";
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = noOfRec;
            pB_ReadDLCs.Increment(1);
            var MissingPSARC = false;
            for (var i = 0; i < noOfRec; i++)
            {
                vFilesMissingIssues = "";

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


                //check Arrangements
                var cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + ";";
                DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", cmd, "", cnb);

                var noOfArr = 0;
                noOfArr = dss.Tables[0].Rows.Count;
                if (noOfArr == 0)
                    vFilesMissingIssues = "No Arrangements!!!";
                for (var k = 0; k < noOfArr; k++)
                {
                    try
                    {
                        var ms1 = dss.Tables[0].Rows[k].ItemArray[4].ToString(); //SNGFilePath
                        var ms2 = dss.Tables[0].Rows[k].ItemArray[5].ToString();//XMLFilePath
                        var ms3 = dss.Tables[0].Rows[k].ItemArray[26].ToString(); //XMLFileName
                        if (!File.Exists(ms1) && (ms3.LastIndexOf("showlights") < 1))
                            vFilesMissingIssues += " SNG " + ms3 + "; "; //showlights
                        if (!File.Exists(ms2))
                            vFilesMissingIssues += " XML " + ms3 + "; ";
                        //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done   
                    }
                    catch (Exception ee)
                    { Console.WriteLine(ee.Message); }
                }

                //check Duplicates and oldies
                cmd = "SELECT PackPath+'\\'+FileName,ID FROM Pack_AuditTrail WHERE DLC_ID=" + ID + " ORDER BY ID DESC;"; //"SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + ";";
                DataSet drs = new DataSet(); drs = SelectFromDB("Pack_AuditTrail", cmd, "", cnb);

                noOfArr = 0;
                if (drs.Tables.Count > 0) noOfArr = drs.Tables[0].Rows.Count;
                else Console.Write("");

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
                    }
                    catch (Exception ee)
                    { Console.WriteLine(ee.Message); }
                }
                if (vFilesMissingIssues.ToLower().IndexOf("sunshine") >= 0)
                    Console.Write("");

                pB_ReadDLCs.Increment(1);
                var old = TempPath + "\\0_old\\" + OrigFileName;
                //var duplicate = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done
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
                DataSet dxr = new DataSet(); if (vFilesMissingIssues != "") dxr = UpdateDB("Main", "Update Main Set FilesMissingIssues = \"" + vFilesMissingIssues + "\" WHERE ID=" + ID + "", cnb);
            }
            SearchCmd = SearchCmd.Replace("ORDER BY", "WHERE FilesMissingIssues <> '' ORDER BY");
            if (MissingPSARC) MessageBox.Show("Please note that Pack_AuditTrail has unmatched records. Manaully decide if they have to be deleted based on Reason field.");
            if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ";
            try
            {
                this.databox.DataSource = null; //Then clear the rows:
                this.databox.Rows.Clear();//                Then set the data source to the new list:
                dssx.Dispose();
                Populate(ref databox, ref Main);
                //DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
            catch (System.IO.FileNotFoundException ee) { MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd); }
            Update_Selected();
        }

        private void chbx_CopyOld_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_CopyOld.Checked)
            {
                //chbx_UniqueID.Checked = false;
                chbx_UniqueID.Enabled = false;
                // chbx_RemoveBassDD.Checked = false;
                chbx_RemoveBassDD.Enabled = false;
                //chbx_Last_Packed.Checked = false;
                chbx_Last_Packed.Enabled = false;
            }
            else
            {
                //chbx_UniqueID.Checked = true;
                chbx_UniqueID.Enabled = true;
                //chbx_RemoveBassDD.Checked = true;
                chbx_RemoveBassDD.Enabled = true;
                //chbx_Last_Packed.Checked = true;
                chbx_Last_Packed.Enabled = true;
            }

        }

        private void btn_Artist2SortA_Click_1(object sender, EventArgs e)
        {
            txt_Artist_Sort.Text = txt_Artist.Text;
        }

        private void btn_Remove_All_Packed_Click(object sender, EventArgs e)
        {
            //var filez = System.IO.Directory.GetFiles(RocksmithDLCPath, (chbx_Format.Text == "PC" ? "*_p.psarc" : (chbx_Format.Text == "Mac" ? "*_m.psarc" : "")));
            //pB_ReadDLCs.Maximum = filez.Count();
            //foreach (string s in filez)
            //{
            //    if (s == "rs1compatibilitydisc_m.psarc" || s == "rs1compatibilitydisc_p_Pc.psarc" || s == "rs1compatibilitydlc_p.psarc" || s == "rs1compatibilitydlc_m.psarc") continue;
            //    try
            //    {
            //        var tg = (RocksmithDLCPath + "\\" + s).Replace(".psarc", ".dupli");
            //        if (File.Exists(RocksmithDLCPath + "\\" + s))
            //            File.Move(RocksmithDLCPath + "\\" + s, tg);
            //    }
            //    catch (Exception ex) { Console.Write(ex); }
            //}
            try
            {
                CleanFolder(TempPath + "\\0_repacked\\PC", "", true, Archive_Path);
                CleanFolder(TempPath + "\\0_repacked\\PS3", "", true, Archive_Path);
                CleanFolder(TempPath + "\\0_repacked\\MAC", "", true, Archive_Path);
                CleanFolder(TempPath + "\\0_repacked\\XBOX360", "", true, Archive_Path);
            }
            catch (Exception ex) { Console.Write(ex); }

            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE PackPath like \"*0_repacked*\";", cnb);
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

        private void button3_Click(object sender, EventArgs e)
        {
            txt_Title.Text = txt_Title.Text.Replace("(", "[").Replace(")", "]");
        }

        private void btn_RemoveBrakets_Click(object sender, EventArgs e)
        {
            var a = txt_Title.Text;
            if (a.IndexOf("[") > 0)
            {
                var b = a.Substring(a.IndexOf("[") - 1, a.IndexOf("]") - a.IndexOf("[") + 2);
                txt_Title.Text = a.Replace(b, "");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath like \"%0_duplicate%\"";
            if (chbx_Ignore_Officials.Checked)
            {
                cmd += " AND Official=\"No\";";
                var tcmd = "SELECT PackPath+\"\\\"+FileName " + cmd;
                DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", tcmd, "", cnb);
                var rec = dvr.Tables[0].Rows.Count;
                if (rec > 0)
                {
                    for (int j = 0; j < rec; j++)//&add items
                        DeleteFile(dvr.Tables[0].Rows[j][0].ToString());
                }
            }
            else
            {
                try
                {
                    CleanFolder(TempPath + "\\0_duplicate", "", true, Archive_Path);
                }
                catch (Exception ex) { Console.Write(ex); }
            }

            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var cmd = " FROM Pack_AuditTrail WHERE PackPath+\"\\\"+FileName = \"" + cmb_Packed.Text + "\"";
            DeleteFile(cmb_Packed.Text);
            DataSet dxr = new DataSet(); dxr = UpdateDB("Pack_AuditTrail", "DELETE * " + cmd, cnb);
        }

        private void cmb_Packed_SelectedValueChanged(object sender, EventArgs e)
        {
            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT Official FROM Pack_AuditTrail WHERE PackPath +\"\\\"+FileName = \"" + cmb_Packed.Text + "\"", "", cnb);
            var rec = dvr.Tables[0].Rows.Count;
            if (rec == 1) chbx_Duplicate_Official.Checked = true;

        }


        private void btn_Debug_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task<int> t = GetTrackNoAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var CleanTitle = "";
            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

            string z = (GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, CleanTitle, txt_Album_Year.Text, txt_SpotifyStatus.Text)).ToString();
            txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
        }

        public static async System.Threading.Tasks.Task<int> GetTrackNoFromSpotifyAsync(string Artist, string Album, string Title, string Year, string Status)
        {

            //string uriString = "https://api.spotify.com/v1/search";
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("query", keywordString);
            var a1 = "";
            //var ab = "";
            //var albump = 0;
            //var artistp = 0;
            //var tracknop = 0;
            debug = "";
            try
            {
                _profile = await _spotify.GetPrivateProfileAsync();
                //SearchItem Aitem = _spotify.SearchItems(Album, SearchType.Album);
                //FullAlbum FAitem = _spotify.GetAlbum(Aitem.Albums.Items.);
                //if (Aitem.Error==null) if (Aitem.Albums.Total>0) if (Aitem.Albums.Items[0]. > 0)
                SearchItem Titem = _spotify.SearchItems(Title, SearchType.Track, 50);
                if (Titem.Error == null) if (Titem.Tracks.Total > 0)
                        foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem.Tracks.Items)
                        {
                            if (Titem.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                    if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                    {
                                        a1 = Trac.TrackNumber.ToString();
                                        FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                        using (WebClient wc = new WebClient())
                                        {
                                            byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(FAitem.Images[0].Url));
                                            //using (MemoryStream stream = new MemoryStream(imageBytes))
                                            //    picbx_SpotifyCover.Image = Image.FromStream(stream);
                                        }
                                        if (Trac.Album.Name.ToString().ToLower() == Album.ToLower())
                                        {
                                            continue;
                                        }
                                    }
                                    else debug += Artis.Name.ToString().ToLower() + " - " + Trac.Name + "\n";
                        }
                //else
                //{
                SearchItem Titem2 = _spotify.SearchItems(Title + "+" + Album + "+" + Artist, SearchType.All);
                if (Titem2.Error == null) if (Titem.Tracks.Total > 0)
                        foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem2.Tracks.Items)
                        {
                            if (Titem2.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                    if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                    {
                                        a1 = Trac.TrackNumber.ToString();
                                        FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                        using (WebClient wc = new WebClient())
                                        {
                                            byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(FAitem.Images[0].Url));
                                            //using (MemoryStream stream = new MemoryStream(imageBytes))
                                            //    picbx_SpotifyCover.Image = Image.FromStream(stream);
                                        }
                                        if (Trac.Album.Name.ToString().ToLower() == Album.ToLower())
                                        {
                                            continue;
                                        }
                                    }
                                    else debug += Artis.Name.ToString().ToLower() + " - " + Trac.Name + "\n";
                        }
                //}
                //if (_profile.Images != null && _profile.Images.Count > 0)
                //{
                //    using (WebClient wc = new WebClient())
                //    {
                //        byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(_profile.Images[0].Url));
                //        using (MemoryStream stream = new MemoryStream(imageBytes))
                //            picbx_AlbumArtPath.Image = Image.FromStream(stream);
                //    }
                //}


                //txt_Artist.Text + "+" +| SearchType.Artist
                // Console.WriteLine(item.Albums.Total); //How many results are there in total? NOTE: item.Tracks = item.Artists = null

                //webClient.QueryString.Add(nameValueCollection);
                //var aa = (webClient.DownloadString(uriString));
                //ab = aa;
                //albump = (aa.ToLower()).IndexOf(Album.ToLower());
                //if (albump > 0) aa = aa.Substring(albump, aa.Length - albump);
                //artistp = (aa.ToLower()).IndexOf(Artist.ToLower());
                //if (artistp > 0) aa = aa.Substring(artistp, aa.Length - artistp);
                //tracknop = (aa.ToLower()).IndexOf("track_number");
                //if (tracknop > 0) a1 = aa.Substring(tracknop + 15, 3);
                //a1 = a1.Replace(",", "");
            }
            catch (Exception ee) { Console.WriteLine(ee.Message); }
            a1 = a1.Trim();
            //txt_Track_No.Value = a1;
            //if (a1 == "" && Album != "")
            //{
            //    a1 = GetTrackNoFromSpotifyAsync(Artist, "", Title).ToString();
            //}
            //if (a1 == "" && Artist != "")
            //{
            //    a1 = GetTrackNoFromSpotifyAsync("", "", Title).ToString();
            //}
            //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
        }

        private async Task ActivateSpotify_ClickAsync()
        {
            var status = "NOK";
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                status = (reply.Status == IPStatus.Success) ? "OK" : "NOK";
            }
            catch (Exception)
            {
                status = "NOK";
            }
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "26d287105e31491889f3cd293d85bfea",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);
            if (status == "OK")
            {
                try
                {
                    _spotify = await webApiFactory.GetWebApi();
                    netstatus = "OK";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //if (_spotify == null)
                //    return 0;

                InitialSetup();

            }
        }

        public async void InitialSetup()
        {

            if (netstatus == "OK")
            {
                if (InvokeRequired)
                {
                    Invoke(new System.Action(SInitialSetup));
                    return;
                }

                //authButton.Enabled = false;
                _profile = await _spotify.GetPrivateProfileAsync();

                _savedTracks = GetSavedTracks();
                txt_SpotifyLog.Text += "\nsavedTracks.Count: " + _savedTracks.Count.ToString();
                _savedTracks.ForEach(track => txt_SavedTracks.Items.Add(new ListViewItem()
                {
                    Text = track.Name,
                    SubItems = { string.Join(",", track.Artists.Select(source => source.Name)), track.Album.Name }
                }));

                _playlists = GetPlaylists();
                txt_SpotifyLog.Text += "\nplaylists.Count: " + _playlists.Count.ToString();
                _playlists.ForEach(playlist => txt_SavedPlaylists.Items.Add(playlist.Name));

                txt_SpotifyLog.Text += "\nprofile.DisplayName: " + _profile.DisplayName;
                txt_SpotifyLog.Text += "\nprofile.Country: " + _profile.Country;
                txt_SpotifyLog.Text += "\nprofile.Email: " + _profile.Email;
                txt_SpotifyLog.Text += "\nprofile.Product: " + _profile.Product;

                if (_profile.Images != null && _profile.Images.Count > 0)
                {
                    using (WebClient wc = new WebClient())
                    {
                        byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(_profile.Images[0].Url));
                        using (MemoryStream stream = new MemoryStream(imageBytes))
                            picbx_SpotifyCover.Image = Image.FromStream(stream);
                    }
                }

                txt_SpotifyStatus.Text = "All good";
                //netstatus = "OK";
            }
        }

        private void btn_ActivateSpotify_Click(object sender, EventArgs e)
        {
            ActivateSpotify_ClickAsync();
        }

        private void btn_GetTrack_Click(object sender, EventArgs e)
        {
            if (txt_SpotifyStatus.Text == "" || txt_SpotifyStatus.Text == "NOK")
            {
                ActivateSpotify_ClickAsync();
            }

            if (txt_SpotifyStatus.Text == "All good" || txt_SpotifyStatus.Text == "OK")
            {
                var CleanTitle = "";
                if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
                if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
                else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

                RequestToGetInputReport(txt_Artist.Text, txt_Album.Text, CleanTitle, txt_Album_Year.Text, txt_SpotifyStatus.Text);
                //.ToString();
                //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
            }
        }

        private async Task RequestToGetInputReport(string Artist, string Album, string Title, string Year, string Status)
        {
            // lots of code prior to this
            int bytesRead = await GetTrackNoFromSpotifyAsync(Artist, Album, Title, Year, Status);
            txt_Track_No.Text = bytesRead.ToString() == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : bytesRead.ToString();
            rtxt_StatisticsOnReadDLCs.Text = debug;
        }

        private void bth_GetTrackNo_Click(object sender, EventArgs e)
        {
            btn_GetTrack_Click(sender, e);
        }

        private void MainDB_Leave_1(object sender, EventArgs e)
        {
            savesettings();
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
            var missing = "";
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var filename = dhs.Tables[0].Rows[i].ItemArray[0].ToString();
                string filePath = TempPath + "\\0_old\\" + filename;
                var dest = RocksmithDLCPath + "\\" + filename;
                if (dhs.Tables[0].Rows[i].ItemArray[1].ToString() == "Yes")
                {
                    try
                    {
                        File.Copy(filePath, dest, true);
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                        MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ee);
                    }
                }
                else missing += " ; " + dhs.Tables[0].Rows[i].ItemArray[2].ToString();
                pB_ReadDLCs.Value++;
            }
            MessageBox.Show("All Filtered Old/Iinitially imported File Copied to " + RocksmithDLCPath + "\\ except: " + missing + " where Original is mising");
        }        

    public void btn_Fix_All_Audio_isues_Click(object sender, EventArgs e)
        {
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var starttmp = DateTime.Now;
            //Fix Bitrate
            var cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, oggPreviewPath FROM Main WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
            cmd += " AND ID="+ txt_ID.Text+"";
        FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //if (d1.Split(';')[1] == "1") artist = "Ignore";
            //if (d1.Split(';')[2] == "1") { j = 10; i = 9999; artist = "Ignore"; }
            //tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true);
            //}
            //var tg = "";
            //Set Preview
            //if (artist != "Ignore" && (chbx_Additional_Manipulations.GetItemChecked(34) && info.OggPreviewPath == null ||
            //        (chbx_Additional_Manipulations.GetItemChecked(55) && ((audio_hash != "" && audio_hash == audioPreview_hash) || recalc_Preview))))
            //{
            //timestamp = UpdateLog(timestamp, "Trying to add preview as missing.", true);

            //Create Preview
            //Fix Preview
            cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
            cmd += " AND ID=" + txt_ID + ";";
            FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


            Populate(ref databox, ref Main);
            databox.Refresh();
            pB_ReadDLCs.CreateGraphics().DrawString("Done Fixing All Songs Audio Issues.", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));

            //if (!File.Exists(info.OggPreviewPath))
            //    tg = "";

            //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
            //if (chbx_Additional_Manipulations.GetItemChecked(69) && info.OggPreviewPath != null && (bitratep > ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() + 8000))
            //{
            //tst = "start encoding preview audio to 128kb ... " + bitratep + " ... " + SampleRate; timestamp = UpdateLog(timestamp, tst, true);
            //pB_ReadDLCs.Value = 0;
            //DataSet dhfs = new DataSet();
            //cmd = "SELECT OggPath,bitrate,SampleRate,OggPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
            //dhfs = SelectFromDB("Main", cmd, "", cnb);
            //noOfRec = dhfs.Tables[0].Rows.Count;
            ////var dest = "";
            //pB_ReadDLCs.Maximum = noOfRec;

            //for (var j = 0; j <= noOfRec - 1; j++)
            //{
            //    //var filename = dhxs.Tables[0].Rows[j].ItemArray[0].ToString();
            //    var OggPath = dhfs.Tables[0].Rows[j].ItemArray[0].ToString();
            //    var bitrate = dhfs.Tables[0].Rows[j].ItemArray[1];
            //    var SampleRate = dhfs.Tables[0].Rows[j].ItemArray[2];
            //    var OggPreviewPath = dhfs.Tables[0].Rows[j].ItemArray[3].ToString();
            //    var Folder_Name = dhfs.Tables[0].Rows[j].ItemArray[4].ToString();
            //    var d2 = WwiseInstalled("Convert Preview Audio if bitrate> ConfigRepository");
            //    if (d2.Split(';')[0] == "1")
            //    {
            //        //;// + bitratep + " ... " + SampleRate
            //        pB_ReadDLCs.CreateGraphics().DrawString("start encoding Main audio to 128kb from... ", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            //        //file.move(info.OggPreviewPath);
            //        var destf = OggPreviewPath.Replace("_preview", "xw");
            //        File.Move(OggPreviewPath, destf);
            //        Downstream(destf);//.Replace(".wem", "_fixed.ogg")
            //        File.Move(destf, OggPreviewPath);
            //        var audioPreview_hash = GetHash(OggPreviewPath);
            //        //Delete any Wav file created..by....?ccc
            //        foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(OggPreviewPath), "*.wav", System.IO.SearchOption.AllDirectories))
            //        {
            //            DeleteFile(wav_name);//, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin); //File.Delete(wav_name);
            //        }
            //    }
            //}
            //if (d2.Split(';')[1] == "1") artist = "Ignore";
            //if (d2.Split(';')[2] == "1") { j = 10; i = 9999; artist = "Ignore"; }
            //tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true);

            //tst = "end preview measures..."; timestamp = UpdateLog(timestamp, tst, true);
        }

        public void btn_FixAudioAll_Click(object sender, EventArgs e)
        {
            //if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();// Cancel the asynchronous operation.
            var cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, oggPreviewPath FROM Main WHERE (VAL(audioBitrate) > "
                + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ") AND "
                + "ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")";
            //cmd += " AND " + ((SearchCmd.IndexOf("WHERE ") > 0) ? (SearchCmd.Substring(SearchCmd.IndexOf("WHERE ") + 5)) : "1=1");
            FixAudioIssues(cmd.Replace("; ", ""), cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE Has_Preview=\"No\"";
            //cmd += " AND " + ((SearchCmd.IndexOf("WHERE ") > 0) ? (SearchCmd.Substring(SearchCmd.IndexOf("WHERE ") + 5)) : "1=1");
            FixMissingPreview(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


            Populate(ref databox, ref Main);
            databox.Refresh();
            pB_ReadDLCs.CreateGraphics().DrawString("Done Fixing Audio Issues for this song.", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        }

        public void UpdateProgressBar(string txt)
        {
            pB_ReadDLCs.CreateGraphics().DrawString(txt, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        }
    
    }
}
