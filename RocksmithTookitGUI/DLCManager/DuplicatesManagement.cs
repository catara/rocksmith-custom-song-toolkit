using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using RocksmithToolkitLib; //config
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitGUI;
using System.IO;
using System.Security.Cryptography; //For File hash
using RocksmithToolkitLib.Extensions; //dds
using System.Globalization;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.XmlRepository;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
//using RocksmithToolkitLib.Extensions; //most likely cue text

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class frm_Duplicates_Management : Form
    {
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public string DLCID { get; set; }
        public string Asses { get; set; }
        public string Is_Alternate { get; set; }
        public string Title_Sort { get; set; }
        public string Album { get; set; }
        //public string Is_Original { get; set; }
        public string AlbumArtPath { get; set; }
        public string Alternate_No { get; set; }
        public string Art_Hash { get; set; }
        public string ArtistSort { get; set; }
        public string Artist { get; set; }
        public string TempPath { get; set; }
        public string altver { get; set; }
        public string MultiT { get; set; }
        public string MultiTV { get; set; }
        public string AlbumAP { get; set; }
        public string YouTube_Link { get; set; }
        public string CustomsForge_Link { get; set; }
        public string CustomsForge_Like { get; set; }
        public string CustomsForge_ReleaseNotes { get; set; }
        public string ExistingTrackNo { get; set; }
        public string dupliID { get; set; }
        public string txt_RocksmithDLCPath { get; set; }
        public string AllowEncript { get; set; }
        public string AllowORIGDelete { get; set; }
        public string FileSize { get; set; }
        public string unpackedDir { get; set; }
        public string Is_MultiTracks { get; set; }
        public string MultiTrack_Versions { get; set; }
        public string FileDate { get; set; }
        public bool IgnoreRest { get; set; }
        public string title_duplic { get; set; }
        public string original_Platform { get; set; }
        public string isLive { get; set; }
        public string isAcoustic { get; set; }
        public string liveDetails { get; set; }
        public string dupli_reason { get; set; }
        public string lengty { get; set; }
        public string allothers { get; set; }
        public string yearalbum { get; set; }
        public string albumsort { get; set; }
        public OleDbConnection cnb { get; set; }
        public int reffy { get; set; }

        //public bool newold { get; set; }
        //public string clist { get; set; }
        //public DuplicatesManagement(string txt_DBFolder, Files eXisting, DLCPackageData dataNew, string author, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist)
        //{
        //    InitializeComponent();
        //    //MessageBox.Show("test0");
        //    DB_Path = txt_DBFolder;
        //    DB_Path = DB_Path + "\\AccessDB.accdb";
        //    MessageBox.Show("test1");

        //}

        internal frm_Duplicates_Management(GenericFunctions.MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD,
            string bass, string guitar, string combo, string rhythm, string lead, string vocal, string tunnings, int i, int norows, string original_FileName,
            string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string DBPath, List<string> clist,
            List<string> dlist, bool newold, string Is_Original, string altvert, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete,
            string FileSize, string unpackedDir, string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic,
            string original_Platform, string IsLive, string LiveDetails, string IsAcoustic, OleDbConnection cnnb, string dupli_reasons
            , string lengty, string allothers, int reff)//, string yeara, string albumsa)//string Is_MultiTracking, string Multitracking, 
        //file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_reff, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver
        {
            //Text = text;
            //MessageBox.Show("test2");
            this.eXisting = filed;
            this.dataNew = datas;
            this.author = author;
            this.tkversion = tkversion;
            this.dD = dD;
            this.bass = bass;
            this.guitar = guitar;
            this.combo = combo;
            this.rhythm = rhythm;
            this.lead = lead;
            this.vocal = vocal;
            this.tunnings = tunnings;
            this.i = i;
            this.norows = norows;
            this.original_FileName = original_FileName;
            this.art_hash = art_hash;
            this.audio_hash = audio_hash;
            this.audioPreview_hash = audioPreview_hash;
            this.alist = alist;
            this.blist = blist;
            this.DB_Path = DBPath;
            this.clist = clist;
            this.dlist = dlist;
            this.newold = newold;
            this.Is_Original = Is_Original;
            this.altver = altvert;
            this.FileDate = FileDate;
            this.title_duplic = title_duplic;
            //this.MultiTracking = MultiTracking;
            this.txt_RocksmithDLCPath = txt_RocksmithDLCPath;
            //this.AllowEncript = AllowEncript;
            //this.AllowORIGDelete = AllowORIGDelete;
            this.FileSize = FileSize;
            this.unpackedDir = unpackedDir;
            this.Is_MultiTracks = Is_MultiTrack;
            this.MultiTrack_Versions = MultiTrack_Version;
            this.original_Platform = original_Platform;
            this.liveDetails = LiveDetails;
            this.isLive = IsLive;
            this.isAcoustic = IsAcoustic;
            //MessageBox.Show(DB_Path);
            //DB_Path = text;
            //MessageBox.Show(DB_Path);
            //DB_Path = DB_Path;// + "\\AccessDB.accdb";
            this.dupli_reason = dupli_reasons;
            this.cnb = cnnb;
            this.lengty = lengty;
            this.allothers = allothers;
            this.reffy = reff;
            //this.yearalbum = yeara;
            //this.albumsort = albums;

            InitializeComponent();
        }
        //public string GetAlternateNo()
        //{
        //    var a = "";
        //    //Get the higgest Alternate Number
        //    //try
        //    //{
        //    //    using (OleDbConnection con = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
        //    //{
        //    var sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + dataNew.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + dataNew.SongInfo.Album + "\") AND ";
        //    sel += "(LCASE(Song_Title)=LCASE(\"" + dataNew.SongInfo.SongDisplayName + "\") OR ";
        //    sel += "LCASE(Song_Title) like \"%" + dataNew.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
        //    sel += "LCASE(Song_Title_Sort) =LCASE(\"" + dataNew.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + dataNew.Name + "\");";
        //    //Get last inserted ID
        //    //rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
        //    DataSet dds = new DataSet(); dds = SelectFromDB("Main", sel, "", cnb);
        //    //OleDbDataAdapter dda = new OleDbDataAdapter(sel, con);
        //    //dda.Fill(dds, "Main");
        //    //dda.Dispose();

        //    var altver = dds.Tables[0].Rows[0].ItemArray[0].ToString();
        //    if (Is_Original == "No") a = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
        //                                                                    //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

        //    //    }
        //    //}
        //    //catch (System.IO.FileNotFoundException ee)
        //    //{
        //    //    
        //    //    
        //    //    
        //    //    Console.WriteLine(ee.Message);
        //    //    //continue;
        //    //}

        //    return a;
        //}

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        public DateTime myOldDate;
        public DateTime myNewDate;

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool ExistChng = false;
        public bool AllowORIGDeleteb = false;
        public string leadxml;
        public string bassxml;
        public string comboxml;
        public string rhythmxml;
        public string vocalxml;
        public string leadjson;
        public string bassjson;
        public string combojson;
        public string rhythmjson;
        public string vocaljson;
        public bool AllowEncriptb = false;
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void DuplicatesManagement_Load(object sender, EventArgs e)
        {
            Author = "";
            Version = "";
            DLCID = "";
            Title = "";
            Comment = "";
            Description = "";
            dupliID = "";

            this.Text += " " + dupli_reason + " ";

            btn_Title2SortT.Text = char.ConvertFromUtf32(8595);
            btn_Artist2SortA.Text = char.ConvertFromUtf32(8595);
            btn_Album2SortA.Text = char.ConvertFromUtf32(8595);

            chbx_Sort.Checked = ConfigRepository.Instance()["dlcm_DupliM_Sync"] == "Yes" ? true : false;
            chbx_Autosave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            btn_UseDates.Checked = ConfigRepository.Instance()["dlcm_DupliUseDates"] == "Yes" ? true : false;

            lbl_tonediff.Visible = false;
            lbl_previewFootnote.Visible = false;

            if (reffy != 0)
            {
                btn_Ignore.Text = "Delete New";
                btn_Update.Text = "Delete Existing";
                btn_StopImport.Text = "Stop the Assesment";
                lbl_IDNew.Text = reffy.ToString();
                lbl_IDNew.Visible = true;
            }
            else
            {
                btn_Ignore.Text = "Ignore New as OLD / Duplicate";
                btn_Update.Text = "Update and Overrite Existing";
                btn_StopImport.Text = "Stop the Import";
                lbl_IDNew.Visible = false;
            }

            //MessageBox.Show("test6");
            if (dataNew.SongInfo.Artist != eXisting.Artist) { lbl_Artist.ForeColor = lbl_Reference.ForeColor; btn_ArtistExisting.Enabled = true; btn_ArtistNew.Enabled = true; }
            else if (dataNew.SongInfo.Artist == "" && "" == eXisting.Artist) lbl_Artist.Text = "";

            txt_ArtistNew.Text = dataNew.SongInfo.Artist; //eXisting.
            txt_ArtistExisting.Text = eXisting.Artist; //

            if (dataNew.SongInfo.Album != eXisting.Album) { lbl_Album.ForeColor = lbl_Reference.ForeColor; btn_AlbumExisting.Enabled = true; btn_AlbumNew.Enabled = true; }
            else if (dataNew.SongInfo.Album == "" && "" == eXisting.Album) lbl_Album.Text = "";
            txt_AlbumNew.Text = dataNew.SongInfo.Album; //eXisting.    
            txt_AlbumExisting.Text = eXisting.Album; //

            lbl_IDExisting.Text = eXisting.ID;
            //lbl_IDNew.Text = eXisting.ID;

            //Alternate
            txt_AlternateNoNew.Value = altver.ToInt32();

            chbx_IsAlternateNew.Checked = true; /*!(eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" && Is_MultiTracks != "Yes") ? : false; */
            chbx_IsAlternateExisting.Checked = eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" ? true : false;
            txt_AlternateNoExisting.Enabled = eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" ? true : false;
            txt_AlternateNoExisting.Value = (eXisting.Alternate_Version_No.ToInt32() == -1) ? 0 : eXisting.Alternate_Version_No.ToInt32();

            //Multitrack
            //txt_MultiTrackNew.Text = "";
            if (Is_MultiTracks != eXisting.Is_Multitrack || MultiTrack_Versions != eXisting.MultiTrack_Version || isLive != eXisting.Is_Live || liveDetails != eXisting.Live_Details) { lbl_Multitrack.ForeColor = lbl_Reference.ForeColor; }
            else if ((Is_MultiTracks == "" && "" == eXisting.Is_Multitrack) && (MultiTrack_Versions == "" && "" == eXisting.MultiTrack_Version) && (isLive == "" && "" == eXisting.Is_Live) && (liveDetails == "" && "" == eXisting.Live_Details)) lbl_Multitrack.Text = "";
            chbx_MultiTrackNew.Checked = Is_MultiTracks == "Yes" ? true : false;
            txt_MultiTrackNew.Enabled = Is_MultiTracks == "Yes" ? true : false;
            txt_MultiTrackNew.Text = (MultiTrack_Versions == "") ? "" : MultiTrack_Versions;

            chbx_MultiTrackExisting.Checked = eXisting.Is_Multitrack == "Yes" ? true : false;
            txt_MultiTrackExisting.Enabled = eXisting.Is_Multitrack == "Yes" ? true : false;
            txt_MultiTrackExisting.Text = (eXisting.MultiTrack_Version == "") ? "" : eXisting.MultiTrack_Version;

            //Live Track
            chbx_LiveNew.Checked = isLive == "Yes" ? true : false;
            txt_LiveDetailsNew.Enabled = isLive == "Yes" ? true : false;
            txt_LiveDetailsNew.Text = (liveDetails == "") ? "" : liveDetails;

            //Lenght of Track
            if (lengty != eXisting.Song_Lenght)
            {
                txt_LenghtExisting.ForeColor = System.Drawing.Color.Red; txt_LenghtExisting.Font = new Font(txt_LenghtExisting.Font.Name, 9, FontStyle.Bold | FontStyle.Underline);
                txt_LenghtNew.ForeColor = System.Drawing.Color.Red; txt_LenghtNew.Font = new Font(txt_LenghtNew.Font.Name, 9, FontStyle.Bold | FontStyle.Underline);
            }
            txt_LenghtExisting.Text = eXisting.Song_Lenght;
            txt_LenghtNew.Text = lengty;

            chbx_LiveExisting.Checked = eXisting.Is_Live == "Yes" ? true : false;
            txt_LiveDetailsExisting.Enabled = eXisting.Is_Live == "Yes" ? true : false;
            txt_LiveDetailsExisting.Text = (eXisting.Live_Details == "") ? "" : eXisting.Live_Details;

            chbx_AcousticNew.Checked = isAcoustic == "Yes" ? true : false;
            chbx_AcousticExisting.Checked = eXisting.Is_Acoustic == "Yes" ? true : false;

            Art_Hash = eXisting.AlbumArt_Hash;

            txt_YouTube_LinkExisting.Text = eXisting.YouTube_Link;
            txt_CustomsForge_LinkExisting.Text = eXisting.YouTube_Link;
            txt_CustomsForge_LikeExisting.Text = eXisting.YouTube_Link;
            txt_CustomsForge_ReleaseNotesExisting.Text = eXisting.YouTube_Link;

            // string altver = GetAlternateNo();

            //FileSize
            if (FileSize != eXisting.File_Size) { lbl_Size.ForeColor = lbl_Reference.ForeColor; }
            txt_SizeNew.Text = FileSize.ToInt32().ToString("###,###,###");
            txt_SizeExisting.Text = eXisting.File_Size.ToInt32().ToString("###,###,###");
            ////FileSize
            //if (FileSize != eXisting.File_Creation_Date) { lbl_Size.ForeColor = lbl_Reference.ForeColor; }
            //txt_SizeNew.Text = FileSize.ToInt32().ToString("###,###,###");
            //txt_SizeExisting.Text = eXisting.File_Size.ToInt32().ToString("###,###,###");


            if (dataNew.SongInfo.SongDisplayName != eXisting.Song_Title) { lbl_Title.ForeColor = lbl_Reference.ForeColor; }//btn_TitleExisting.Enabled = true; btn_TitleNew.Enabled = true; }
            else if (dataNew.SongInfo.SongDisplayName == "" && "" == eXisting.Song_Title) lbl_Title.Text = "";
            txt_TitleNew.Text = dataNew.SongInfo.SongDisplayName;
            txt_TitleExisting.Text = eXisting.Song_Title;

            if (dataNew.SongInfo.SongDisplayNameSort != eXisting.Song_Title_Sort) { lbl_TitleSort.ForeColor = lbl_Reference.ForeColor; btn_TitleSortExisting.Enabled = true; btn_TitleSortNew.Enabled = true; }
            else if (dataNew.SongInfo.SongDisplayNameSort == "" && "" == eXisting.Song_Title_Sort) lbl_TitleSort.Text = "";
            txt_TitleSortNew.Text = dataNew.SongInfo.SongDisplayNameSort;
            txt_TitleSortExisting.Text = eXisting.Song_Title_Sort;

            if (dataNew.SongInfo.ArtistSort != eXisting.Artist_Sort) { lbl_ArtistSort.ForeColor = lbl_Reference.ForeColor; btn_ArtistSortExisting.Enabled = true; btn_ArtistSortNew.Enabled = true; }
            else if (dataNew.SongInfo.ArtistSort == "" && "" == eXisting.Artist_Sort) lbl_ArtistSort.Text = "";
            txt_ArtistSortNew.Text = dataNew.SongInfo.ArtistSort;
            txt_ArtistSortExisting.Text = eXisting.Artist_Sort;

            if (dataNew.SongInfo.AlbumSort != eXisting.Album_Sort) { lbl_AlbumSort.ForeColor = lbl_Reference.ForeColor; txt_AlbumSortNew.Enabled = true; btn_AlbumSortNew.Enabled = true; }
            else if (dataNew.SongInfo.AlbumSort == "" && "" == eXisting.Album_Sort) lbl_AlbumSort.Text = "";
            txt_AlbumSortNew.Text = dataNew.SongInfo.AlbumSort;
            txt_AlbumSortExisting.Text = eXisting.Album_Sort;


            if (dataNew.SongInfo.SongYear.ToString() != eXisting.Album_Year)
            {
                txt_YearExisting.ForeColor = System.Drawing.Color.Red; txt_YearExisting.Font = new Font(txt_YearExisting.Font.Name, 9, FontStyle.Bold | FontStyle.Underline);
                txt_YearNew.ForeColor = System.Drawing.Color.Red; txt_YearNew.Font = new Font(txt_YearNew.Font.Name, 9, FontStyle.Bold | FontStyle.Underline);
            }
            //{ lbl_TitleSort.ForeColor = lbl_Reference.ForeColor; btn_TitleSortExisting.Enabled = true; btn_TitleSortNew.Enabled = true; }
            //else if (dataNew.SongInfo.SongDisplayNameSort == "" && "" == eXisting.Song_Title_Sort) lbl_TitleSort.Text = "";
            txt_YearNew.Text = dataNew.SongInfo.SongYear.ToString();
            txt_YearExisting.Text = eXisting.Album_Year;

            if (original_FileName != eXisting.Original_FileName) lbl_FileName.ForeColor = lbl_Reference.ForeColor;
            else if (original_FileName == "" && "" == eXisting.Original_FileName) lbl_FileName.Text = "";
            txt_FileNameNew.Text = original_FileName;
            txt_FileNameExisting.Text = eXisting.Original_FileName;

            if (original_FileName != eXisting.Original_FileName) { txt_PlatformNew.ForeColor = lbl_Reference.ForeColor; /*Color.Red;*/ txt_PlatformExisting.ForeColor = lbl_Reference.ForeColor;/*Color.Red;*/ }
            //else if (original_FileName == "" && "" == eXisting.Original_Platform) txt_PlatformNew.ForeColor= Color.Red;
            txt_PlatformNew.Text = original_Platform;
            txt_PlatformExisting.Text = eXisting.Platform;//.original_Platform;

            if (author != eXisting.Author) { lbl_Author.ForeColor = lbl_FileName.ForeColor; btn_AuthorExisting.Enabled = true; btn_AuthorNew.Enabled = true; }
            else if (author == "" && "" == eXisting.Author) lbl_Author.Text = "";
            txt_AuthorNew.Text = author;// (author == "" ? "missing" : author);
            txt_AuthorExisting.Text = eXisting.Author;//(eXisting.Author == "" ? "missing" : eXisting.Author);            

            if (tunnings != eXisting.Tunning) lbl_Tuning.ForeColor = lbl_Reference.ForeColor;
            else if (tunnings == "" && "" == eXisting.Tunning) lbl_Tuning.Text = "";
            txt_TuningNew.Text = tunnings;
            txt_TuningExisting.Text = eXisting.Tunning;

            if ((dataNew.ToolkitInfo.PackageVersion == null ? "1" : dataNew.ToolkitInfo.PackageVersion.ToString()) != eXisting.Version) lbl_Version.ForeColor = lbl_Reference.ForeColor;
            else if (dataNew.ToolkitInfo.PackageVersion == "" && "" == eXisting.Version) lbl_Version.Text = "";
            txt_VersionNew.Text = (dataNew.ToolkitInfo.PackageVersion == null ? "1" : dataNew.ToolkitInfo.PackageVersion.ToString());
            txt_VersionExisting.Text = eXisting.Version;
            txt_FileDateNew.Text = FileDate;
            txt_FileDateExisting.Text = eXisting.File_Creation_Date;

            if (dD != eXisting.Has_DD) lbl_DD.ForeColor = lbl_Reference.ForeColor;
            else if (dD == "" && "" == eXisting.Has_DD) lbl_DD.Text = "";
            txt_DDNew.Text = dD;
            txt_DDExisting.Text = eXisting.Has_DD;

            if (dataNew.Name != eXisting.DLC_Name) lbl_DLCID.ForeColor = lbl_Reference.ForeColor;
            else if (dataNew.Name == "" && "" == eXisting.DLC_Name) lbl_DLCID.Text = "";
            txt_DLCIDNew.Text = dataNew.Name;
            txt_DLCIDExisting.Text = eXisting.DLC_Name;

            if (Is_Original != eXisting.Is_Original) lbl_IsOriginal.ForeColor = lbl_Reference.ForeColor;
            else if (Is_Original == "" && "" == eXisting.Is_Original) lbl_IsOriginal.Text = "";
            txt_IsOriginalNew.Text = Is_Original;
            txt_IsOriginalExisting.Text = eXisting.Is_Original;
            if (eXisting.Is_Original == "Yes" || Is_Original == "Yes") lbl_Is_Original.ForeColor = lbl_Reference.ForeColor;

            if (tkversion != eXisting.ToolkitVersion) lbl_Toolkit.ForeColor = lbl_Reference.ForeColor;
            else if (tkversion == "" && "" == eXisting.ToolkitVersion) lbl_Toolkit.Text = "";
            txt_ToolkitNew.Text = tkversion;
            txt_ToolkitExisting.Text = eXisting.ToolkitVersion;

            if (eXisting.AlbumArt_OrigHash != art_hash) { lbl_AlbumArt.ForeColor = lbl_Reference.ForeColor; btn_CoverNew.Enabled = true; btn_CoverExisting.Enabled = true; }
            else if (eXisting.AlbumArt_OrigHash == "" && "" == art_hash) lbl_AlbumArt.Text = "";
            else { lbl_Covers.ForeColor = System.Drawing.Color.Green; lbl_Covers.Font = new Font(lbl_Covers.Font.Name, 9, FontStyle.Bold | FontStyle.Underline); }
            if (dataNew.AlbumArtPath != null)
            {
                dataNew.AlbumArtPath = dataNew.AlbumArtPath.Replace("/", "\\");
                picbx_AlbumArtPathNew.ImageLocation = dataNew.AlbumArtPath.Replace(".dds", ".png");
            }
            //txt_Description.Text= dataNew.AlbumArtPath.Replace(".dds", ".png");
            picbx_AlbumArtPathExisting.ImageLocation = eXisting.AlbumArtPath.Replace(".dds", ".png");

            if (eXisting.Audio_OrigHash != audio_hash) lbl_Audio.ForeColor = lbl_Reference.ForeColor;/*|| eXisting.Audio_Hash != audio_hash*/
            else if (eXisting.Audio_OrigHash == "" && "" == audio_hash) lbl_Audio.Text = "";
            else { lbl_AudioMain.ForeColor = System.Drawing.Color.Green; lbl_AudioMain.Font = new Font(lbl_AudioMain.Font.Name, 9, FontStyle.Bold | FontStyle.Underline); }
            txt_AudioNew.Text = (audio_hash.ToString() == "" ? "" : "Yes");
            txt_AudioExisting.Text = (eXisting.Audio_OrigHash.ToString() == "" ? "" : "Yes");

            if (eXisting.Audio_OrigPreviewHash != audioPreview_hash) lbl_Preview.ForeColor = lbl_Reference.ForeColor;
            else if (eXisting.Audio_OrigPreviewHash == "" && "" == audioPreview_hash) lbl_Vocals.Text = "";
            else { lbl_AudioPreview.ForeColor = System.Drawing.Color.Green; lbl_AudioMain.Font = new Font(lbl_AudioPreview.Font.Name, 9, FontStyle.Bold | FontStyle.Underline); }
            txt_PreviewNew.Text = (audioPreview_hash.ToString() == "" ? "No" : "Yes");
            txt_PreviewExisting.Text = (eXisting.Audio_OrigPreviewHash.ToString() == "" ? "No" : "Yes");
            if (audioPreview_hash.ToString() != "") btn_PlayPreviewNew.Enabled = true;
            if (eXisting.Audio_OrigPreviewHash.ToString() != "") btn_PlayPreviewExisting.Enabled = true;
            if (eXisting.PreviewTime != "") { lbl_Preview.ForeColor = lbl_previewFootnote.ForeColor; lbl_previewFootnote.Visible = true; }

            if (eXisting.Has_Vocals.ToString() != vocal) lbl_Vocals.ForeColor = lbl_Reference.ForeColor;
            //else if (eXisting.Has_Vocals == "" && "" == vocal) lbl_Vocals.Text = "";
            txt_VocalsNew.Text = (vocal == "Yes" ? "Yes" : "No");
            txt_VocalsExisting.Text = (eXisting.Has_Vocals.ToString() == "Yes" ? "Yes" : "No");

            if (((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : "") + ((vocal == "Yes") ? "V" : "") != ((eXisting.Has_Bass == "Yes") ? "B" : "") + ((eXisting.Has_Rhythm == "Yes") ? "R" : "") + ((eXisting.Has_Lead == "Yes") ? "L" : "") + ((eXisting.Has_Combo == "Yes") ? "L" : "") + ((eXisting.Has_Vocals == "Yes") ? "V" : "")) lbl_AvailableTracks.ForeColor = lbl_Reference.ForeColor;
            txt_AvailTracksNew.Text = ((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : "") + ((vocal == "Yes") ? "V" : "");
            txt_AvailTracksExisting.Text = ((eXisting.Has_Bass == "Yes") ? "B" : "") + ((eXisting.Has_Rhythm == "Yes") ? "R" : "") + ((eXisting.Has_Lead == "Yes") ? "L" : "") + ((eXisting.Has_Combo == "Yes") ? "L" : "") + ((eXisting.Has_Vocals == "Yes") ? "V" : "");

            //Show the alternate/duplicates in the DB
            lbl_diffCount.Text = (i + 1).ToString() + "/" + norows.ToString();
            //lbl_diffCount.Visible = true;
            if (norows > 1)
            {
                chbx_IgnoreDupli.Enabled = true;
                btn_ShowInfoOthers.Enabled = true;
            }

            if (eXisting.Artist.ToLower() != dataNew.SongInfo.Artist.ToLower() || eXisting.Album.ToLower() != dataNew.SongInfo.Album.ToLower()) btn_NotADuplicate.Text = "(Likely) NOT a Duplicate";
            else btn_NotADuplicate.Text = "NOT a Duplicate";
            //    btn_NotADuplicate.Visible = true;
            //else btn_NotADuplicate.Visible = false;


            //string text = "Same Current -> Existing " + (i + 2) + "/" + (norows + 1) + " " + eXisting.Artist + "-" + eXisting.Album + "\n";
            //text += ((dataNew.SongInfo.SongDisplayName == eXisting.Song_Title) ? "" : ("\n1/14+ Song Titles: " + dataNew.SongInfo.SongDisplayName + "->" + eXisting.Song_Title));
            //text += ((dataNew.SongInfo.SongDisplayNameSort == eXisting.Song_Title_Sort) ? "" : ("\n2/14+ Song Sort Titles: " + dataNew.SongInfo.SongDisplayNameSort + "->" + eXisting.Song_Title_Sort));
            //text += ((original_FileName == eXisting.Original_FileName) ? "" : ("\n3/14+ FileNames: " + original_FileName + " - " + eXisting.Original_FileName));
            //text += ((((tkversion == "") ? "Yes" : "No") == eXisting.Is_Original) ? "" : ("\n4/14+ Is Original: " + ((tkversion == "") ? "Yes" : "No") + " -> " + eXisting.Is_Original));
            //text += ((tkversion == eXisting.ToolkitVersion) ? "" : ("\n5/14+ Toolkit version: " + tkversion + " -> " + eXisting.ToolkitVersion));
            //text += ("\n6/14+ Author: " + author + " -> " + eXisting.Author); //((author == eXisting.Author) ? "" :
            //text += ((tunnings == eXisting.Tunning) ? "" : ("\n7/14+ Tunning: " + tunnings + " -> " + eXisting.Tunning));
            //text += ((dataNew.PackageVersion == eXisting.Version) ? "" : ("\n8/14+ Version: " + dataNew.PackageVersion + " -> " + eXisting.Version));
            //text += ((dataNew.Name == eXisting.DLC_Name) ? "" : ("\n9/14+ DLC ID: " + dataNew.Name + " -> " + eXisting.DLC_Name));
            //text += ((dD == eXisting.Has_DD) ? "" : ("\n10/14+ DD: " + dD + " -> " + eXisting.Has_DD));
            ////text += "\nOriginal Is Alternate: " + eXisting.Is_Alternate + (eXisting.Is_Alternate == "Yes" ? " v. " + eXisting.Alternate_Version_No : "");
            //text += "\n11/14+ Avail. Instr./Tracks: " + ((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : ""); //((Guitar == "Yes") ? "G" : "") + 
            //text += " -> " + ((eXisting.Has_Bass == "Yes") ? "B" : "") + ((eXisting.Has_Rhythm == "Yes") ? "R" : "") + ((eXisting.Has_Lead == "Yes") ? "L" : "") + ((eXisting.Has_Combo == "Yes") ? "L" : ""); //+ ((eXisting.Has_Guitar == "Yes") ? "G" : "")
            //text += ((eXisting.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + eXisting.art_Hash
            //text += ((eXisting.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + eXisting.audio_Hash 
            //text += ((eXisting.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + eXisting.audioPreview_Hash

            //files hash
            //DataSet ds = new DataSet();
            i = 0;
            //DB_Path = DB_Path + "\\AccessDB.accdb;";
            string jsonHash = "";
            //bool diffjson;// = true;
            string XmlHash = "";
            var XmlName = "";
            var XmlUUID = "";
            var XmlFile = "";
            var jsonFile = "";
            //bool diff;// = true;
            int k = 0;
            string lastConversionDateTime_cur = "";
            string lastConversionDateTime_exist = "";
            string lastConverjsonDateTime_cur = "";
            CultureInfo enUS = new CultureInfo("en-US");
            string lastConverjsonDateTime_exist = "";

            DateTime LastConvDate_exis = DateTime.ParseExact("12-13-11 13:11", "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
            DateTime LastConvDate_new = DateTime.ParseExact("12-13-11 13:11", "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
            string txtnew = "";
            string txtold = "";
            //MessageBox.Show(DB_Path);
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            //        var cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + eXisting.ID.ToString() + ";";//\"\"
            //        OleDbDataAdapter daa = new OleDbDataAdapter(cmd, cnn);
            //        //as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
            //        //MessageBox.Show("0");
            //        daa.Fill(ds, "Arrangements");
            //        daa.Dispose();
            DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + eXisting.ID.ToString() + ";", "", cnb);
            var noOfRec = 0;
            //MessageBox.Show("0.1");
            noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                                              //rtxt_StatisticsOnReadDLCs.Text = noOfRec + "Assesment Arrangement hash file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                              //MessageBox.Show("1");
            if (noOfRec > 0)
            {
                //MessageBox.Show("1");
                string datenew = "12-13-11 13:11";
                string dateold = "12-13-11 13:11";
                txtnew = "";
                txtold = "";
                DateTime myCurDate;
                DateTime myExisDate;
                foreach (var arg in dataNew.Arrangements)
                {
                    //diff = true; diffjson = true;
                    lastConversionDateTime_cur = "";
                    lastConversionDateTime_exist = "";
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        //MessageBox.Show(noOfRec.ToString());
                        //rtxt_StatisticsOnReadDLCs.Text = alist[i]+"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        XmlHash = ds.Tables[0].Rows[i].ItemArray[43].ToString(); // XmlHash                                  
                        XmlName = ds.Tables[0].Rows[i].ItemArray[17].ToString() + ds.Tables[0].Rows[i].ItemArray[25].ToString(); //type+routemask+
                        XmlUUID = ds.Tables[0].Rows[i].ItemArray[28].ToString(); //xml.uuid
                        XmlFile = ds.Tables[0].Rows[i].ItemArray[5].ToString(); //xml.filepath
                        jsonFile = ds.Tables[0].Rows[i].ItemArray[4].ToString(); //json.filepath
                        jsonHash = ds.Tables[0].Rows[i].ItemArray[42].ToString(); // XmlHash      
                        var xx = Directory.GetFiles(arg.SongXml.File.Replace("\\songs\\arr\\" + arg.SongXml.Name + ".xml", ""), "*.json", SearchOption.AllDirectories)[0];
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul36"] == "Yes") //37. Keep the Uncompressed Songs superorganized   chbx_Additional_Manipulations.GetItemChecked(36)                             
                            arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                        else
                            arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(xx));

                        lastConversionDateTime_cur = GetTExtFromFile(arg.SongXml.File).Trim(' ');
                        lastConversionDateTime_exist = GetTExtFromFile(XmlFile).Trim(' ');
                        if (lastConversionDateTime_cur.Length > 3)
                        {
                            if (lastConversionDateTime_cur.IndexOf("-") == 1) lastConversionDateTime_cur = "0" + lastConversionDateTime_cur;
                            if (lastConversionDateTime_cur.IndexOf("-", 3) == 4) lastConversionDateTime_cur = lastConversionDateTime_cur.Substring(0, 3) + "0" + lastConversionDateTime_cur.Substring(3, ((lastConversionDateTime_cur.Length) - 3));
                            if (lastConversionDateTime_cur.IndexOf(":") == 10) lastConversionDateTime_cur = lastConversionDateTime_cur.Substring(0, 9) + "0" + lastConversionDateTime_cur.Substring(9, lastConversionDateTime_cur.Length - 9);
                        }
                        if (lastConversionDateTime_exist.Length > 3)
                        {
                            if (lastConversionDateTime_exist.IndexOf("-") == 1) lastConversionDateTime_exist = "0" + lastConversionDateTime_exist;
                            if (lastConversionDateTime_exist.IndexOf("-", 3) == 4) lastConversionDateTime_exist = lastConversionDateTime_exist.Substring(0, 3) + "0" + lastConversionDateTime_exist.Substring(3, ((lastConversionDateTime_exist.Length) - 3));
                            if (lastConversionDateTime_exist.IndexOf(":") == 10) lastConversionDateTime_exist = lastConversionDateTime_exist.Substring(0, 9) + "0" + lastConversionDateTime_exist.Substring(9, lastConversionDateTime_exist.Length - 9);
                        }

                        lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File).Trim(' '); ;
                        lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile).Trim(' '); ;
                        if (lastConverjsonDateTime_cur.Length > 3)
                        {
                            if (lastConverjsonDateTime_cur.IndexOf("-") == 1) lastConverjsonDateTime_cur = "0" + lastConverjsonDateTime_cur;
                            if (lastConverjsonDateTime_cur.IndexOf("-", 3) == 4) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 3) + "0" + lastConverjsonDateTime_cur.Substring(3, ((lastConverjsonDateTime_cur.Length) - 3));
                            if (lastConverjsonDateTime_cur.IndexOf(":") == 10) lastConverjsonDateTime_cur = lastConverjsonDateTime_cur.Substring(0, 9) + "0" + lastConverjsonDateTime_cur.Substring(9, lastConverjsonDateTime_cur.Length - 9);
                        }
                        if (lastConverjsonDateTime_exist.Length > 3)
                        {
                            if (lastConverjsonDateTime_exist.IndexOf("-") == 1) lastConverjsonDateTime_exist = "0" + lastConverjsonDateTime_exist;
                            if (lastConverjsonDateTime_exist.IndexOf("-", 3) == 4) lastConverjsonDateTime_exist = lastConverjsonDateTime_exist.Substring(0, 3) + "0" + lastConverjsonDateTime_exist.Substring(3, ((lastConverjsonDateTime_exist.Length) - 3));
                            if (lastConverjsonDateTime_exist.IndexOf(":") == 10) lastConverjsonDateTime_exist = lastConverjsonDateTime_exist.Substring(0, 9) + "0" + lastConverjsonDateTime_exist.Substring(9, lastConverjsonDateTime_exist.Length - 9);
                        }

                        if (XmlName == (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) || (XmlUUID == arg.SongXml.UUID.ToString()))
                        // rtxt_StatisticsOnReadDLCs.Text = "-" + XmlHash + "=" + alist[k] + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        {


                            if (arg.RouteMask.ToString() == "Bass")
                            {
                                bassxml = "";
                                lbl_XMLBass.Visible = true;
                                if (!(lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist))
                                    if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) { lbl_XMLBass.ForeColor = lbl_Reference.ForeColor; btn_WM_Bass.Enabled = true; }
                                txt_XMLBassNew.Text = lastConversionDateTime_cur;
                                txt_XMLBassExisting.Text = lastConversionDateTime_exist;
                                bassxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Lead")
                            {
                                leadxml = "";
                                lbl_XMLLead.Visible = true;
                                if (!(lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist))
                                    if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) { lbl_XMLLead.ForeColor = lbl_Reference.ForeColor; btn_WM_Leads.Enabled = true; }
                                txt_XMLLeadNew.Text = lastConversionDateTime_cur;
                                txt_XMLLeadExisting.Text = lastConversionDateTime_exist;
                                leadxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Combo")
                            {
                                comboxml = "";
                                lbl_XMLCombo.Visible = true;
                                if (!(lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist))
                                    if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) { lbl_XMLCombo.ForeColor = lbl_Reference.ForeColor; btn_WM_Combo.Enabled = true; }
                                txt_XMLComboNew.Text = lastConversionDateTime_cur;
                                txt_XMLComboExisting.Text = lastConversionDateTime_exist;
                                leadxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Rhythm")
                            {
                                rhythmxml = "";
                                lbl_XMLRhythm.Visible = true;
                                if (!(lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist))
                                    if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) { lbl_XMLRhythm.ForeColor = lbl_Reference.ForeColor; btn_WM_Rhythm.Enabled = true; }
                                txt_XMLRhythmNew.Text = lastConversionDateTime_cur;
                                txt_XMLRhythmExisting.Text = lastConversionDateTime_exist;
                                rhythmxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Vocal" || XmlName == "VocalNone")
                            {
                                //if (lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)
                                //lbl_XMLVocal.Text = "";
                                //else if (lastConversionDateTime_cur != lastConversionDateTime_exist) { lbl_XMLVocal.ForeColor = lbl_Reference.ForeColor; lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }                                            
                                //txt_XMLVocalNew.Text = lastConversionDateTime_cur;
                                //txt_XMLVocalExisting.Text = lastConversionDateTime_exist;lbl_XMLVocal.ForeColor = lbl_Reference.ForeColor;
                                if (jsonHash != blist[k]) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; btn_WM_Vocals.Enabled = true; }
                                lbl_Vocals.Visible = true;
                                if (XmlHash != alist[k]) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }
                                vocalxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";
                                //else if (XmlHash == "" && "" == alist[k]) ;
                                //var r = blist[k];
                            }

                            if (arg.RouteMask.ToString() == "Bass")
                            {
                                bassjson = "";
                                lbl_JSONBass.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) { lbl_JSONBass.ForeColor = lbl_Reference.ForeColor; btn_TN_Bass.Enabled = true; }
                                txt_JSONBassNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONBassExisting.Text = lastConverjsonDateTime_exist;
                                bassjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Lead")
                            {
                                lbl_JSONLead.Visible = true;
                                leadjson = "";
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) { lbl_JSONLead.ForeColor = lbl_Reference.ForeColor; btn_TN_Lead.Enabled = true; }
                                txt_JSONLeadNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONLeadExisting.Text = lastConverjsonDateTime_exist;
                                leadjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Combo")
                            {
                                combojson = "";
                                lbl_JSONCombo.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) { lbl_JSONCombo.ForeColor = lbl_Reference.ForeColor; btn_TN_Combo.Enabled = true; }
                                txt_JSONComboNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONComboExisting.Text = lastConverjsonDateTime_exist;
                                combojson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Rhythm")
                            {
                                rhythmjson = "";
                                lbl_JSONRhythm.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) { lbl_JSONRhythm.ForeColor = lbl_Reference.ForeColor; btn_TN_Rhythm.Enabled = true; }
                                txt_JSONRhythmNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONRhythmExisting.Text = lastConverjsonDateTime_exist;
                                rhythmjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                            if (arg.ArrangementType.ToString() == "Vocal")
                            {
                                vocaljson = "";
                                lbl_Vocals.Visible = true;
                                //if (lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist) lbl_JSONVocal.Text = "";
                                //else if (lastConversionDateTime_cur != lastConversionDateTime_exist) { lbl_JSONVocal.ForeColor = lbl_Reference.ForeColor; lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }
                                //txt_JSONVocalNew.Text = lastConverjsonDateTime_cur;
                                //txt_JSONVocalExisting.Text = lastConverjsonDateTime_exist;lbl_JSONVocal.ForeColor = lbl_Reference.ForeColor; 
                                if (jsonHash != blist[k]) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; btn_WM_Vocals.Enabled = true; }
                                // else if (jsonHash == "" && "" == blist[k]) ;
                                vocaljson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                        }


                        //Get the oldest timestamp
                        myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                        myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);

                        if (lastConversionDateTime_cur.Length > 3)
                        {
                            myCurDate = DateTime.ParseExact(lastConversionDateTime_cur, "MM-dd-yy HH:mm", enUS);
                            if ((myCurDate > myNewDate) && (arg.RouteMask.ToString() == "Bass" || arg.RouteMask.ToString() == "Lead" || arg.RouteMask.ToString() == "Rhythm" || arg.RouteMask.ToString() == "Combo"))
                                datenew = lastConversionDateTime_cur;
                        }
                        if (lastConversionDateTime_exist.Length > 3)
                        {
                            myExisDate = DateTime.ParseExact(lastConversionDateTime_exist, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                            if (myExisDate > myOldDate && (arg.RouteMask.ToString() == "Bass" || arg.RouteMask.ToString() == "Lead" || arg.RouteMask.ToString() == "Rhythm" || arg.RouteMask.ToString() == "Combo"))
                                dateold = lastConversionDateTime_exist;
                        }

                        myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                        myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                        if (dateold != "12-13-11 13:11" && datenew != "12-13-11 13:11")
                        {
                            //Commenting out the Auto addition of OLDer&Newer
                            if (newold && eXisting.Is_Original != "Yes" && Is_Original != "Yes")//&& myOldDate > myNewDate)
                            {
                                txtnew = " " + (btn_UseDates.Checked ? (myNewDate >= myOldDate ? lbl_Existing.Text : lbl_New.Text) : myNewDate.ToString());
                                txtold = " " + (btn_UseDates.Checked ? (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text) : myOldDate.ToString());
                            }
                            //else if (newold && eXisting.Is_Original != "Yes" && Is_Original != "Yes" && myOldDate < myNewDate)
                            //{
                            //    txtnew = " " + (btn_UseDates.Checked ? "(newer)":myNewDate.ToString());
                            //    txtold = " " + (btn_UseDates.Checked ? "(older)": myOldDate.ToString());
                            //}
                            LastConvDate_exis = myOldDate > LastConvDate_exis ? myOldDate : LastConvDate_exis;
                            LastConvDate_new = myNewDate > LastConvDate_new ? myNewDate : LastConvDate_new;
                        }
                    }



                    k++;
                }

            }
            //}
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    //rtxt_StatisticsOnReadDLCs.Text = "Error at last conversion" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //    Console.WriteLine(ee.Message);
            //}

            //files size//files dates
            //DialogResult result1 = MessageBox.Show(text + "\n\nChose:\n\n1. Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //if (result1 == DialogResult.Yes) return "Update";
            //else if (result1 == DialogResult.No) return "Alternate";
            //else return "ignore";//if (result1 == DialogResult.Cancel) 
            ExistChng = false;
            lbl_New.Text = txtnew;
            lbl_Existing.Text = txtold;
            lbl_DateNew.Text = LastConvDate_new.ToString();
            lbl_DateExisting.Text = LastConvDate_exis.ToString();
            if (LastConvDate_new.ToString() == LastConvDate_exis.ToString()) lbl_tonediff.Visible = true;

            this.Text += ". " + title_duplic;

            if (ConfigRepository.Instance()["dlcm_AdditionalManipul13"] == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul85"] == "Yes")
            {
                btn_AddStandard_Click(null, null); btn_Alternate_Click(null, null);
                return;
            }
        }

        //private class Files
        //{
        //    public string ID { get; set; }
        //    public string Song_Title { get; set; }
        //    public string Song_Title_Sort { get; set; }
        //    public string Album { get; set; }
        //    public string Artist { get; set; }
        //    public string Artist_Sort { get; set; }
        //    public string Album_Year { get; set; }
        //    public string AverageTempo { get; set; }
        //    public string Volume { get; set; }
        //    public string Preview_Volume { get; set; }
        //    public string AlbumArtPath { get; set; }
        //    public string AudioPath { get; set; }
        //    public string audioPreviewPath { get; set; }
        //    public string Track_No { get; set; }
        //    public string Author { get; set; }
        //    public string Version { get; set; }
        //    public string DLC_Name { get; set; }
        //    public string DLC_AppID { get; set; }
        //    public string Current_FileName { get; set; }
        //    public string Original_FileName { get; set; }
        //    public string Import_Path { get; set; }
        //    public string Import_Date { get; set; }
        //    public string Folder_Name { get; set; }
        //    public string File_Size { get; set; }
        //    public string File_Hash { get; set; }
        //    public string Original_File_Hash { get; set; }
        //    public string Is_Original { get; set; }
        //    public string Is_OLD { get; set; }
        //    public string Is_Beta { get; set; }
        //    public string Is_Alternate { get; set; }
        //    public string Is_Multitrack { get; set; }
        //    public string Is_Broken { get; set; }
        //    public string MultiTrack_Version { get; set; }
        //    public string Alternate_Version_No { get; set; }
        //    public string DLC { get; set; }
        //    public string Has_Bass { get; set; }
        //    public string Has_Guitar { get; set; }
        //    public string Has_Lead { get; set; }
        //    public string Has_Rhythm { get; set; }
        //    public string Has_Combo { get; set; }
        //    public string Has_Vocals { get; set; }
        //    public string Has_Sections { get; set; }
        //    public string Has_Cover { get; set; }
        //    public string Has_Preview { get; set; }
        //    public string Has_Custom_Tone { get; set; }
        //    public string Has_DD { get; set; }
        //    public string Has_Version { get; set; }
        //    public string Tunning { get; set; }
        //    public string Bass_Picking { get; set; }
        //    public string Tones { get; set; }
        //    public string Group { get; set; }
        //    public string Rating { get; set; }
        //    public string Description { get; set; }
        //    public string Comments { get; set; }
        //    public string Has_Track_No { get; set; }
        //    public string Platform { get; set; }
        //    public string PreviewTime { get; set; }
        //    public string PreviewLenght { get; set; }
        //    public string Youtube_Playthrough { get; set; }
        //    public string CustomForge_Followers { get; set; }
        //    public string CustomForge_Version { get; set; }
        //    public string FilesMissingIssues { get; set; }
        //    public string Duplicates { get; set; }
        //    public string Pack { get; set; }
        //    public string Keep_BassDD { get; set; }
        //    public string Keep_DD { get; set; }
        //    public string Keep_Original { get; set; }
        //    public string Song_Lenght { get; set; }
        //    public string Original { get; set; }
        //    public string Selected { get; set; }
        //    public string YouTube_Link { get; set; }
        //    public string CustomsForge_Link { get; set; }
        //    public string CustomsForge_Like { get; set; }
        //    public string CustomsForge_ReleaseNotes { get; set; }
        //    public string SignatureType { get; set; }
        //    public string ToolkitVersion { get; set; }
        //    public string Has_Author { get; set; }
        //    public string OggPath { get; set; }
        //    public string oggPreviewPath { get; set; }
        //    public string UniqueDLCName { get; set; }
        //    public string AlbumArt_Hash { get; set; }
        //    public string Audio_Hash { get; set; }
        //    public string audioPreview_Hash { get; set; }
        //    public string Bass_Has_DD { get; set; }
        //    public string Has_Bonus_Arrangement { get; set; }
        //    public string Artist_ShortName { get; set; }
        //    public string Album_ShortName { get; set; }
        //    public string Available_Old { get; set; }
        //    public string Available_Duplicate { get; set; }
        //    public string Has_Been_Corrected { get; set; }
        //    public string File_Creation_Date { get; set; }
        //}
        //private Files[] files = new Files[10000];
        internal MainDBfields[] files = new MainDBfields[10000];
        private MainDBfields eXisting;
        private DLCPackageData dataNew;
        private string author;
        private string tkversion;
        private string dD;
        private string bass;
        private string guitar;
        private string combo;
        private string rhythm;
        private string lead;
        private string vocal;
        private string tunnings;
        private int i;
        private int norows;
        private string original_FileName;
        private string art_hash;
        private string audio_hash;
        private string audioPreview_hash;
        private string Is_Original;
        private List<string> alist;
        private List<string> blist;
        private List<string> clist;
        private List<string> dlist;
        private bool newold;




        //Generic procedure to read and parse Main.DB (&others..soon)
        //public int SQLAccess(string cmd)
        //{
        //    //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
        //    //Files[] files = new Files[10000];

        //    var MaximumSize = 0;

        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
        //    //try
        //    //{
        //    //    //MessageBox.Show(DB_Path);
        //    //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
        //    //    {
        //    //        DataSet dus = new DataSet();
        //    //        OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
        //    //        dax.Fill(dus, "Main");
        //    //        dax.Dispose();
        //    DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd);
        //    var i = 0;
        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
        //    MaximumSize = dus.Tables[0].Rows.Count;
        //    foreach (DataRow dataRow in dus.Tables[0].Rows)
        //    {
        //        files[i] = new Files();

        //        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
        //        files[i].ID = dataRow.ItemArray[0].ToString();
        //        files[i].Song_Title = dataRow.ItemArray[1].ToString();
        //        files[i].Song_Title_Sort = dataRow.ItemArray[2].ToString();
        //        files[i].Album = dataRow.ItemArray[3].ToString();
        //        files[i].Artist = dataRow.ItemArray[4].ToString();
        //        files[i].Artist_Sort = dataRow.ItemArray[5].ToString();
        //        files[i].Album_Year = dataRow.ItemArray[6].ToString();
        //        files[i].AverageTempo = dataRow.ItemArray[7].ToString();
        //        files[i].Volume = dataRow.ItemArray[8].ToString();
        //        files[i].Preview_Volume = dataRow.ItemArray[9].ToString();
        //        files[i].AlbumArtPath = dataRow.ItemArray[10].ToString();
        //        files[i].AudioPath = dataRow.ItemArray[11].ToString();
        //        files[i].audioPreviewPath = dataRow.ItemArray[12].ToString();
        //        files[i].Track_No = dataRow.ItemArray[13].ToString();
        //        files[i].Author = dataRow.ItemArray[14].ToString();
        //        files[i].Version = dataRow.ItemArray[15].ToString();
        //        files[i].DLC_Name = dataRow.ItemArray[16].ToString();
        //        files[i].DLC_AppID = dataRow.ItemArray[17].ToString();
        //        files[i].Current_FileName = dataRow.ItemArray[18].ToString();
        //        files[i].Original_FileName = dataRow.ItemArray[19].ToString();
        //        files[i].Import_Path = dataRow.ItemArray[20].ToString();
        //        files[i].Import_Date = dataRow.ItemArray[21].ToString();
        //        files[i].Folder_Name = dataRow.ItemArray[22].ToString();
        //        files[i].File_Size = dataRow.ItemArray[23].ToString();
        //        files[i].File_Hash = dataRow.ItemArray[24].ToString();
        //        files[i].Original_File_Hash = dataRow.ItemArray[25].ToString();
        //        files[i].Is_Original = dataRow.ItemArray[26].ToString();
        //        files[i].Is_OLD = dataRow.ItemArray[27].ToString();
        //        files[i].Is_Beta = dataRow.ItemArray[28].ToString();
        //        files[i].Is_Alternate = dataRow.ItemArray[29].ToString();
        //        files[i].Is_Multitrack = dataRow.ItemArray[30].ToString();
        //        files[i].Is_Broken = dataRow.ItemArray[31].ToString();
        //        files[i].MultiTrack_Version = dataRow.ItemArray[32].ToString();
        //        files[i].Alternate_Version_No = dataRow.ItemArray[33].ToString();
        //        files[i].DLC = dataRow.ItemArray[34].ToString();
        //        files[i].Has_Bass = dataRow.ItemArray[35].ToString();
        //        files[i].Has_Guitar = dataRow.ItemArray[36].ToString();
        //        files[i].Has_Lead = dataRow.ItemArray[37].ToString();
        //        files[i].Has_Rhythm = dataRow.ItemArray[38].ToString();
        //        files[i].Has_Combo = dataRow.ItemArray[39].ToString();
        //        files[i].Has_Vocals = dataRow.ItemArray[40].ToString();
        //        files[i].Has_Sections = dataRow.ItemArray[41].ToString();
        //        files[i].Has_Cover = dataRow.ItemArray[42].ToString();
        //        files[i].Has_Preview = dataRow.ItemArray[43].ToString();
        //        files[i].Has_Custom_Tone = dataRow.ItemArray[44].ToString();
        //        files[i].Has_DD = dataRow.ItemArray[45].ToString();
        //        files[i].Has_Version = dataRow.ItemArray[46].ToString();
        //        files[i].Tunning = dataRow.ItemArray[47].ToString();
        //        files[i].Bass_Picking = dataRow.ItemArray[48].ToString();
        //        files[i].Tones = dataRow.ItemArray[49].ToString();
        //        files[i].Group = dataRow.ItemArray[50].ToString();
        //        files[i].Rating = dataRow.ItemArray[51].ToString();
        //        files[i].Description = dataRow.ItemArray[52].ToString();
        //        files[i].Comments = dataRow.ItemArray[53].ToString();
        //        files[i].Has_Track_No = dataRow.ItemArray[54].ToString();
        //        files[i].Platform = dataRow.ItemArray[55].ToString();
        //        files[i].PreviewTime = dataRow.ItemArray[56].ToString();
        //        files[i].PreviewLenght = dataRow.ItemArray[57].ToString();
        //        files[i].Youtube_Playthrough = dataRow.ItemArray[58].ToString();
        //        files[i].CustomForge_Followers = dataRow.ItemArray[59].ToString();
        //        files[i].CustomForge_Version = dataRow.ItemArray[60].ToString();
        //        files[i].FilesMissingIssues = dataRow.ItemArray[61].ToString();
        //        files[i].Duplicates = dataRow.ItemArray[62].ToString();
        //        files[i].Pack = dataRow.ItemArray[63].ToString();
        //        files[i].Keep_BassDD = dataRow.ItemArray[64].ToString();
        //        files[i].Keep_DD = dataRow.ItemArray[65].ToString();
        //        files[i].Keep_Original = dataRow.ItemArray[66].ToString();
        //        files[i].Song_Lenght = dataRow.ItemArray[67].ToString();
        //        files[i].Original = dataRow.ItemArray[68].ToString();
        //        files[i].Selected = dataRow.ItemArray[69].ToString();
        //        files[i].YouTube_Link = dataRow.ItemArray[70].ToString();
        //        files[i].CustomsForge_Link = dataRow.ItemArray[71].ToString();
        //        files[i].CustomsForge_Like = dataRow.ItemArray[72].ToString();
        //        files[i].CustomsForge_ReleaseNotes = dataRow.ItemArray[73].ToString();
        //        files[i].SignatureType = dataRow.ItemArray[74].ToString();
        //        files[i].ToolkitVersion = dataRow.ItemArray[75].ToString();
        //        files[i].Has_Author = dataRow.ItemArray[76].ToString();
        //        files[i].OggPath = dataRow.ItemArray[77].ToString();
        //        files[i].oggPreviewPath = dataRow.ItemArray[78].ToString();
        //        files[i].UniqueDLCName = dataRow.ItemArray[79].ToString();
        //        files[i].AlbumArt_Hash = dataRow.ItemArray[80].ToString();
        //        files[i].Audio_Hash = dataRow.ItemArray[81].ToString();
        //        files[i].audioPreview_Hash = dataRow.ItemArray[82].ToString();
        //        files[i].Bass_Has_DD = dataRow.ItemArray[83].ToString();
        //        files[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
        //        files[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
        //        files[i].Album_ShortName = dataRow.ItemArray[86].ToString();
        //        files[i].Available_Old = dataRow.ItemArray[87].ToString();
        //        files[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
        //        files[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
        //        files[i].File_Creation_Date = dataRow.ItemArray[90].ToString();
        //        i++;
        //    }
        //    //        //Closing Connection
        //    //        dax.Dispose();
        //    //        cnn.Close();
        //    //        //rtxt_StatisticsOnReadDLCs.Text += i;
        //    //        //var ex = 0;
        //    //    }
        //    //}
        //    //catch (System.IO.FileNotFoundException ee)
        //    //{
        //    //    MessageBox.Show(ee.Message + "Can not open Main DB connection ! ");
        //    //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //}
        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
        //    return MaximumSize;//files[10000];
        //}

        public string GetTExtFromFile(string ss)
        {
            string tecst = "";
            if (File.Exists(ss))
            {
                StreamReader info = null;
                try
                {
                    info = File.OpenText(ss);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open XML/JSON file for Duplciation assessment ! " + ss + "-");

                    //                throw;
                }


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
            }
            return tecst;
        }

        private void btn_Alternate_Click(object sender, EventArgs e)
        {
            Asses = "Alternate;alt";
            if (ExistChng)
            {
                if (!chbx_Autosave.Checked)
                {
                    DialogResult result1 = MessageBox.Show("Save the Existing Edits?\nYes for save \nNo for Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes) UpdateExisting();
                }
                else UpdateExisting();
            }
            exit();
            this.Hide();
            //this.ParentForm.
        }

        private void btn_Ignore_Click(object sender, EventArgs e)
        {
            Asses = "Ignore;Manual_Decision";
            if (ExistChng)
            {
                if (!chbx_Autosave.Checked)
                {
                    DialogResult result1 = MessageBox.Show("Save the Existing Edits?\nYes for save \nNo for Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes) UpdateExisting();
                }
                else UpdateExisting();
            }
            exit();
            this.Hide();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Asses = "Update;Manual_Decision";
            exit();
            this.Hide();
        }
        public void exit()
        {
            Author = (txt_AuthorNew.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul57"] == "No" ? "Custom Song Creator" : "") : txt_AuthorNew.Text);
            Version = (txt_VersionNew.Text == "" ? "1" : txt_VersionNew.Text);
            DLCID = txt_DLCIDNew.Text;
            Title = txt_TitleNew.Text;
            Comment = txt_DescriptionExistig.Text;//not used
            Description = txt_Description.Text;
            Is_Alternate = (chbx_IsAlternateNew.Checked ? (txt_IsOriginalNew.Text == "Yes" ? "Yes" : "No") : "No");
            Title_Sort = txt_TitleSortNew.Text;
            Album = txt_AlbumNew.Text;
            //Is_Original= (chbx_IsOriginal.Checked ? "Yes" : "No");
            Alternate_No = chbx_IsAlternateNew.Checked ? txt_AlternateNoNew.Text : null;
            AlbumArtPath = txt_TitleNew.Text != "" && picbx_AlbumArtPathNew.ImageLocation != null ? picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds") : "";//rbtn_CoverExisting.Checked ? picbx_AlbumArtPathExisting.ImageLocation.Replace("png", "dds") : picbx_AlbumArtPathNew.ImageLocation.Replace("png", "dds");
            Art_Hash = txt_TitleNew.Text != "" && picbx_AlbumArtPathNew.ImageLocation != null ? GetHash(picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds")) : "";//.Checked ? eXisting.AlbumArt_Hash : art_hash;
            ArtistSort = txt_ArtistSortNew.Text;
            Artist = txt_ArtistNew.Text;
            MultiT = chbx_MultiTrackNew.Checked ? "Yes" : "No";
            MultiTV = txt_MultiTrackNew.Text;
            AlbumAP = AlbumArtPath;
            YouTube_Link = txt_YouTube_LinkExisting.Text;
            CustomsForge_Link = txt_CustomsForge_LinkExisting.Text;
            CustomsForge_Like = txt_CustomsForge_LikeExisting.Text;
            CustomsForge_ReleaseNotes = txt_CustomsForge_ReleaseNotesExisting.Text;
            ExistingTrackNo = eXisting.Track_No;
            IgnoreRest = chbx_IgnoreDupli.Checked;
            isLive = chbx_LiveNew.Checked ? "Yes" : "No";
            isAcoustic = chbx_AcousticNew.Checked ? "Yes" : "No";
            yearalbum = txt_YearNew.Text; ;
            albumsort = txt_AlbumSortNew.Text;

            ConfigRepository.Instance()["dlcm_DupliM_Sync"] = chbx_Sort.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_Autosave.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DupliUseDates"] = btn_UseDates.Checked == true ? "Yes" : "No";

            //Delete old arrangements and tones
            //Clean CachetDB
            //DataSet dss = new DataSet();
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            if (Asses == "Update")
            {
                DeleteFromDB("Tones", "DELETE FROM Tones WHERE CDLC_ID=" + lbl_IDExisting.Text, cnb);
                DeleteFromDB("Arrangements", "DELETE FROM Arrangements WHERE CDLC_ID=" + lbl_IDExisting.Text, cnb);
                //OleDbDataAdapter dan = new OleDbDataAdapter("DELETE FROM Tones WHERE CDLC_ID=" + lbl_IDExisting.Text, cnn);
                //dan.Fill(dss, "Cache");
                //dan.Dispose();
                //OleDbDataAdapter das = new OleDbDataAdapter("DELETE FROM Arrangements WHERE CDLC_ID=" + lbl_IDExisting.Text, cnn);
                //das.Fill(dss, "Cache");
                //das.Dispose();
            }
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //}

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateExisting();
            ExistChng = false;
        }

        public void UpdateExisting()
        {
            var sel = "";
            //var cmd = "";
            // in case a similar artist covered the song ...don't attach wrong names
            var al = txt_AlbumExisting.Text;
            var ar = txt_ArtistExisting.Text;
            //if (eXisting.Album != dataNew.SongInfo.Album) al = dataNew.SongInfo.Album;
            //if (eXisting.Artist != dataNew.SongInfo.Artist) ar = dataNew.SongInfo.Artist;
            var alt = chbx_IsAlternateExisting.Checked ? txt_AlternateNoExisting.Value.ToString() : null;

            //try
            //{
            //    //MessageBox.Show(DB_Path);
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            sel = "UPDATE Main SET Artist=\"" + ar + "\", Artist_Sort=\"" + txt_ArtistSortExisting.Text + "\", Album=\"" + al + "\", Song_Title=\"" + txt_TitleExisting.Text;
            sel += "\", Song_Title_Sort=\"" + txt_TitleSortExisting.Text + "\", Author=\"" + (txt_AuthorExisting.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" ? "" : "Custom Song Creator") : txt_AuthorExisting.Text);
            sel += "\", Version=\"" + (txt_VersionExisting.Text == "" ? "1" : txt_VersionExisting.Text) + "\", DLC_Name=\"" + txt_DLCIDExisting.Text + "\",";
            sel += (txt_DescriptionExistig.Text == "" ? "" : " Description = \"" + txt_DescriptionExistig.Text + "\",");/// + (txt_DescriptionExistig.Text == "" ? "" : " Comments = \"" + txt_DescriptionExistig.Text + "\","); //"\"," +
            sel += " Is_Alternate = \"" + (chbx_IsAlternateExisting.Checked ? "Yes" : "No") + "\", Alternate_Version_No = \"" + alt + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_MultiTrack = \"" + (chbx_MultiTrackExisting.Checked ? "Yes" : "No") + "\", MultiTrack_Version = \"" + (chbx_MultiTrackExisting.Checked ? txt_MultiTrackExisting.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Live = \"" + (chbx_LiveExisting.Checked ? "Yes" : "No") + "\", Live_Details = \"" + (chbx_LiveExisting.Checked ? txt_LiveDetailsExisting.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Acoustic = \"" + (chbx_AcousticExisting.Checked ? "Yes" : "No") + "\"," + " Album_Year = \"" + txt_YearExisting.Text + "\",";
            sel += " AlbumArtPath = \"" + picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds") + "\", AlbumArt_Hash = \"" + GetHash(picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds")) + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " YouTube_Link = \"" + txt_YouTube_LinkExisting.Text + "\", CustomsForge_Link = \"" + txt_CustomsForge_LinkExisting.Text + "\",";
            sel += " CustomsForge_Like = \"" + txt_CustomsForge_LikeExisting.Text + "\", CustomsForge_ReleaseNotes = \"" + txt_CustomsForge_ReleaseNotesExisting.Text + "\"";
            //sel += "\", AlbumArt_Hash = \"" + (rbtn_CoverNew.Checked ? art_hash : eXisting.AlbumArt_Hash);
            sel += " WHERE ID=" + lbl_IDExisting.Text;
            //txt_Description.Text = sel;
            DataSet ddr = new DataSet(); ddr = UpdateDB("Main", sel + ";", cnb);
            if (reffy != 0)
                UpdateCompareBase();
            //OleDbDataAdapter dat = new OleDbDataAdapter(sel, cnn);

                //dat.Fill(ddr, "Main");
                //dat.Dispose();
                //MessageBox.Show(DB_Path);
                //    }
                //}
                //catch (System.IO.FileNotFoundException ee)
                //{
                //    MessageBox.Show(ee.Message + "Can not open Main DB connection ! " + sel);
                //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            if (!chbx_Autosave.Checked) MessageBox.Show("Existing Record updated");

        }
        public void UpdateCompareBase()
        {
            var sel = "";
            //var cmd = "";
            // in case a similar artist covered the song ...don't attach wrong names
            var al = txt_AlbumNew.Text;
            var ar = txt_ArtistNew.Text;
            //if (eXisting.Album != dataNew.SongInfo.Album) al = dataNew.SongInfo.Album;
            //if (eXisting.Artist != dataNew.SongInfo.Artist) ar = dataNew.SongInfo.Artist;
            var alt = chbx_IsAlternateNew.Checked ? txt_AlternateNoNew.Value.ToString() : null;

            //try
            //{
            //    //MessageBox.Show(DB_Path);
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            sel = "UPDATE Main SET Artist=\"" + ar + "\", Artist_Sort=\"" + txt_ArtistSortNew.Text + "\", Album=\"" + al + "\", Song_Title=\"" + txt_TitleNew.Text;
            sel += "\", Song_Title_Sort=\"" + txt_TitleSortNew.Text + "\", Author=\"" + (txt_AuthorNew.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" ? "" : "Custom Song Creator") : txt_AuthorNew.Text);
            sel += "\", Version=\"" + (txt_VersionNew.Text == "" ? "1" : txt_VersionNew.Text) + "\", DLC_Name=\"" + txt_DLCIDNew.Text + "\",";
            sel += (txt_DescriptionExistig.Text == "" ? "" : " Description = \"" + txt_DescriptionExistig.Text + "\",");/// + (txt_DescriptionExistig.Text == "" ? "" : " Comments = \"" + txt_DescriptionExistig.Text + "\","); //"\"," +
            sel += " Is_Alternate = \"" + (chbx_IsAlternateNew.Checked ? "Yes" : "No") + "\", Alternate_Version_No = \"" + alt + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_MultiTrack = \"" + (chbx_MultiTrackNew.Checked ? "Yes" : "No") + "\", MultiTrack_Version = \"" + (chbx_MultiTrackNew.Checked ? txt_MultiTrackNew.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Live = \"" + (chbx_LiveNew.Checked ? "Yes" : "No") + "\", Live_Details = \"" + (chbx_LiveNew.Checked ? txt_LiveDetailsNew.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Acoustic = \"" + (chbx_AcousticNew.Checked ? "Yes" : "No") + "\"," + " Album_Year = \"" + txt_YearNew.Text + "\",";
            sel += " AlbumArtPath = \"" + picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds") + "\", AlbumArt_Hash = \"" + GetHash(picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds")) + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " YouTube_Link = \"" + txt_YouTube_LinkNew.Text + "\", CustomsForge_Link = \"" + txt_CustomsForge_LinkNew.Text + "\",";
            sel += " CustomsForge_Like = \"" + txt_CustomsForge_LikeNew.Text + "\", CustomsForge_ReleaseNotes = \"" + txt_CustomsForge_ReleaseNotesNew.Text + "\"";
            //sel += "\", AlbumArt_Hash = \"" + (rbtn_CoverNew.Checked ? art_hash : eXisting.AlbumArt_Hash);
            sel += " WHERE ID=" + reffy;
            //txt_Description.Text = sel;
            DataSet ddr = new DataSet(); ddr = UpdateDB("Main", sel + ";", cnb);
            //OleDbDataAdapter dat = new OleDbDataAdapter(sel, cnn);

            //dat.Fill(ddr, "Main");
            //dat.Dispose();
            //MessageBox.Show(DB_Path);
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Main DB connection ! " + sel);
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            if (!chbx_Autosave.Checked) MessageBox.Show("New Record updated");

        }

        //public static string calc_arthash(string ss)
        //{
        //    if (File.Exists(ss))
        //    {
        //        using (FileStream fs = File.OpenRead(ss))
        //        {
        //            SHA1 sha = new SHA1Managed();
        //            var art_hash = BitConverter.ToString(sha.ComputeHash(fs));//MessageBox.Show(FileHash+"-"+ss);
        //            //convert to png
        //            //ExternalApps.Dds2Png(ss);
        //            fs.Close();
        //            return art_hash.ToString();
        //        }
        //    }
        //    else return null;
        //}

        public void ExistingChanged(object sender, EventArgs e)
        {
            if (txt_ArtistSortExisting.Text == txt_ArtistSortNew.Text)
            {
                btn_ArtistSortExisting.Enabled = false;
                btn_ArtistSortNew.Enabled = false;
                lbl_ArtistSort.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_ArtistSortExisting.Enabled = true;
                btn_ArtistSortNew.Enabled = true;
                lbl_ArtistSort.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_TitleSortExisting.Text == txt_TitleSortNew.Text)
            {
                btn_TitleSortExisting.Enabled = false;
                btn_TitleSortNew.Enabled = false;
                lbl_TitleSort.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_TitleSortExisting.Enabled = true;
                btn_TitleSortNew.Enabled = true;
                lbl_TitleSort.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_TitleExisting.Text == txt_TitleNew.Text)
            {
                btn_TitleExisting.Enabled = false;
                btn_TitleNew.Enabled = false;
                lbl_Title.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_TitleExisting.Enabled = true;
                btn_TitleNew.Enabled = true;
                lbl_Title.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_AlbumExisting.Text == txt_AlbumNew.Text)
            {
                btn_AlbumExisting.Enabled = false;
                btn_AlbumNew.Enabled = false;
                lbl_Album.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_AlbumExisting.Enabled = true;
                btn_AlbumNew.Enabled = true;
                lbl_Album.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_ArtistExisting.Text == txt_ArtistNew.Text)
            {
                btn_ArtistExisting.Enabled = false;
                btn_ArtistNew.Enabled = false;
                lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_ArtistExisting.Enabled = true;
                btn_ArtistNew.Enabled = true;
                lbl_Artist.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_AuthorExisting.Text == txt_AuthorNew.Text)
            {
                btn_AuthorExisting.Enabled = false;
                btn_AuthorNew.Enabled = false;
                lbl_Author.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_AuthorExisting.Enabled = true;
                btn_AuthorNew.Enabled = true;
                lbl_Author.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_AlbumSortExisting.Text == txt_AlbumSortNew.Text)
            {
                btn_AlbumSortExisting.Enabled = false;
                btn_AlbumSortNew.Enabled = false;
                lbl_AlbumSort.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                lbl_AlbumSort.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_YearExisting.Text == txt_YearNew.Text)
            {
                lbl_YearExisting.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                btn_AlbumSortExisting.Enabled = true;
                btn_AlbumSortNew.Enabled = true;
                lbl_AlbumSort.ForeColor = lbl_Reference.ForeColor;
            }

            if (txt_LenghtNew.Text == txt_LenghtExisting.Text)
            {
                lbl_LenghtNew.ForeColor = lbl_diffCount.ForeColor;
            }
            else
            {
                lbl_LenghtNew.ForeColor = lbl_Reference.ForeColor;
            }

            ExistChng = true;
        }

        private void rbtn_CoverNew_CheckedChanged(object sender, EventArgs e)
        {
            //art_hash = Art_Hash;
            //AlbumArtPath = picbx_AlbumArtPathExisting.ImageLocation;
        }

        private void rbtn_CoverExisting_CheckedChanged(object sender, EventArgs e)
        {
            //art_hash = Art_Hash;
            //AlbumArtPath = picbx_AlbumArtPathExisting.ImageLocation;
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(cnb,false);//.Replace("\\AccessDB.accdb;", "")
            frm.Show();
        }

        private void txt_TitleExisting_TextChanged(object sender, EventArgs e)
        {

        }

        private void chbx_DeleteOldNew_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_RemoveOldNew_Click(object sender, EventArgs e)
        {
            // //txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (older)", "");
            // txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (newer)", "");
            //// txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (older)", "");
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul46"] == "Yes")
            {
                txt_TitleExisting.Text = CleanTitle(txt_TitleExisting.Text);
                txt_TitleNew.Text = CleanTitle(txt_TitleNew.Text);
            }
            else
            {
                txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "").Replace("a." + txt_AlternateNoExisting.Text, "").Replace("v." + txt_VersionExisting.Text, "").Replace(txt_AuthorExisting.Text == "" ? "--?" : txt_AuthorExisting.Text, "").Replace("noDD", "").Replace("DD", "").Replace(txt_TuningExisting.Text, "")).Replace(txt_AvailTracksExisting.Text, "").Replace("  ", " ").Replace("  ", " ").Replace(" [ ]", "").Replace(" []", "");
                txt_TitleNew.Text = (dataNew.SongInfo.SongDisplayName.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "").Replace("a." + txt_AlternateNoNew.Text, "").Replace("v." + txt_VersionNew.Text, "").Replace(txt_AuthorNew.Text == "" ? "--?" : txt_AuthorNew.Text, "").Replace("noDD", "").Replace("DD", "").Replace(txt_TuningNew.Text, "")).Replace(txt_AvailTracksNew.Text, "").Replace("  ", " ").Replace("  ", " ").Replace(" [ ]", "").Replace(" []", "");
            }
            //txt_TitleExisting.Text = eXisting.Song_Title;
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();

        }

        private void btn_TitleNew_Click(object sender, EventArgs e)
        {
            if (txt_TitleNew.Text.IndexOf("(newer)") > 0) txt_TitleNew.Text = txt_TitleExisting.Text.Replace("(newer)", "(older)");
            else if (txt_TitleNew.Text.IndexOf("(older)") > 0) txt_TitleNew.Text = txt_TitleExisting.Text.Replace("(older)", "(newer)");
            else txt_TitleNew.Text = txt_TitleExisting.Text;
            lbl_Title.ForeColor = lbl_diffCount.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
            //btn_TitleExisting.Enabled = false;
            //btn_TitleNew.Enabled = false;
        }

        private void btn_TitleExisting_Click(object sender, EventArgs e)
        {
            if (txt_TitleExisting.Text.IndexOf("(newer)") > 0) txt_TitleExisting.Text = txt_TitleNew.Text.Replace("(newer)", "(older)");
            else if (txt_TitleExisting.Text.IndexOf("(older)") > 0) txt_TitleExisting.Text = txt_TitleNew.Text.Replace("(older)", "(newer)");
            else txt_TitleExisting.Text = txt_TitleNew.Text;
            lbl_Title.ForeColor = lbl_diffCount.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
            //btn_TitleExisting.Enabled = false;
            //btn_TitleNew.Enabled = false;
        }

        private void btn_TitleSortNew_Click(object sender, EventArgs e)
        {
            txt_TitleSortNew.Text = txt_TitleSortExisting.Text;
            lbl_TitleSort.ForeColor = lbl_diffCount.ForeColor;
            btn_TitleSortExisting.Enabled = false;
            btn_TitleSortNew.Enabled = false;
        }

        private void btnTitleSortExisting_Click(object sender, EventArgs e)
        {
            txt_TitleSortExisting.Text = txt_TitleSortNew.Text;
            lbl_TitleSort.ForeColor = lbl_diffCount.ForeColor;
            btn_TitleSortExisting.Enabled = false;
            btn_TitleSortNew.Enabled = false;
        }

        private void btn_AuthorNew_Click(object sender, EventArgs e)
        {
            txt_AuthorNew.Text = txt_AuthorExisting.Text;
            lbl_Author.ForeColor = lbl_diffCount.ForeColor;
            btn_AuthorExisting.Enabled = false;
            btn_AuthorNew.Enabled = false;
        }

        private void btn_AuthorExisting_Click(object sender, EventArgs e)
        {
            txt_AuthorExisting.Text = txt_AuthorNew.Text;
            lbl_Author.ForeColor = lbl_diffCount.ForeColor;
            btn_AuthorExisting.Enabled = false;
            btn_AuthorNew.Enabled = false;
        }

        private void btn_AlbumNew_Click(object sender, EventArgs e)
        {
            txt_AlbumNew.Text = txt_AlbumExisting.Text;
            txt_AlbumSortNew.Text = txt_AlbumSortExisting.Text;
            lbl_Album.ForeColor = lbl_diffCount.ForeColor;
            btn_AlbumExisting.Enabled = false;
            btn_AlbumNew.Enabled = false;
            txt_YearNew.Text = txt_YearExisting.Text;
            if (chbx_Sort.Checked) syncAlbum();
        }

        private void btn_AlbumExisting_Click(object sender, EventArgs e)
        {
            txt_AlbumExisting.Text = txt_AlbumNew.Text;
            txt_YearExisting.Text = txt_YearNew.Text;
            lbl_Album.ForeColor = lbl_diffCount.ForeColor;
            btn_AlbumExisting.Enabled = false;
            btn_AlbumNew.Enabled = false;
            txt_YearExisting.Text = txt_YearNew.Text;
            if (chbx_Sort.Checked) syncAlbum();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txt_ArtistNew.Text = txt_ArtistExisting.Text;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            btn_ArtistExisting.Enabled = false;
            btn_ArtistNew.Enabled = false;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            if (chbx_Sort.Checked) { syncArtist(); syncAlbum(); }
        }

        private void btn_ArtistExisting_Click(object sender, EventArgs e)
        {
            txt_ArtistExisting.Text = txt_ArtistNew.Text;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            btn_ArtistExisting.Enabled = false;
            btn_ArtistNew.Enabled = false;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            if (chbx_Sort.Checked) { syncArtist(); syncAlbum(); }
        }

        private void btn_AddVersion_Click(object sender, EventArgs e)
        {
            if (txt_TitleExisting.Text.IndexOf(" " + (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text)) > 0 || txt_TitleNew.Text.IndexOf(" " + (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text)) > 0)
            {
                txt_TitleNew.Text = (txt_TitleNew.Text.Replace(" v." + txt_VersionNew.Text, "")).Replace(" " + (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), " v." + txt_VersionNew.Text);
                txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" v." + txt_VersionExisting.Text, "").Replace(" " + (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), " v." + txt_VersionExisting.Text);
                //txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (newer)", " v." + txt_VersionNew.Text).Replace("]", "") + (chbx_UseBrakets.Checked ? "]" : "");
            }
            else
            {
                txt_TitleNew.Text = (txt_TitleNew.Text.Replace(" v." + txt_VersionNew.Text, "").Replace("[v." + txt_VersionNew.Text, "[").Replace("]", "") + (txt_TitleNew.Text.IndexOf(" [") > 0 ? "" : " [") + " v." + (txt_VersionNew.Text).Replace("[ ", "[").Replace("  ", " ") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
                txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(" v." + txt_VersionExisting.Text, "").Replace("[v." + txt_VersionExisting.Text, "[").Replace("]", "") + (txt_TitleExisting.Text.IndexOf(" [") > 0 ? "" : " [") + " v." + (txt_VersionExisting.Text + (chbx_UseBrakets.Checked ? "]" : ""))).Replace("[ ", "[").Replace("  ", " ");
            }
            //    if (txt_TitleExisting.Text.IndexOf(" (older)") > 0 || txt_TitleExisting.Text.IndexOf(" (newer)") > 0)
            //{
            //    txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" v." + txt_VersionExisting.Text, "").Replace(" (older)", " v." + txt_VersionExisting.Text);
            //    txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (newer)", " v." + txt_VersionExisting.Text).Replace("]", "") + (chbx_UseBrakets.Checked ? "]" : "");
            //}
            //else txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(" v." + txt_VersionExisting.Text, "").Replace("[v." + txt_VersionExisting.Text, "[").Replace("]", "") + (txt_TitleExisting.Text.IndexOf(" [") > 0 ? "" : " [") + " v." + (txt_VersionExisting.Text + (chbx_UseBrakets.Checked ? "]" : ""))).Replace("[ ", "[").Replace("  ", " ");

            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_ArtistSortNew_Click(object sender, EventArgs e)
        {
            txt_ArtistSortNew.Text = txt_ArtistSortExisting.Text;
            btn_ArtistSortExisting.Enabled = false;
            btn_ArtistSortNew.Enabled = false;
            lbl_ArtistSort.ForeColor = lbl_diffCount.ForeColor;
        }

        private void btn_ArtistSortExisting_Click(object sender, EventArgs e)
        {
            txt_ArtistSortExisting.Text = txt_ArtistSortNew.Text;
            btn_ArtistSortExisting.Enabled = false;
            btn_ArtistSortNew.Enabled = false;
            lbl_ArtistSort.ForeColor = lbl_diffCount.ForeColor;
        }

        private void chbx_IsAlternateExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_IsAlternateExisting.Checked) txt_AlternateNoExisting.Enabled = true;
            else txt_AlternateNoExisting.Enabled = false;
        }

        private void chbx_IsAlternateNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_IsAlternateNew.Checked) txt_AlternateNoNew.Enabled = true;
            else txt_AlternateNoNew.Enabled = false;
        }

        private void chbx_MultiTrackNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_MultiTrackNew.Checked) txt_MultiTrackNew.Enabled = true;
            else txt_MultiTrackNew.Enabled = false;
        }

        private void chbx_MultiTrackExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_MultiTrackExisting.Checked) { txt_MultiTrackExisting.Enabled = true; ExistChng = true; }
            else txt_MultiTrackExisting.Enabled = false;
        }

        private void btn_AddInstruments_Click(object sender, EventArgs e)
        {
            if (txt_AvailTracksNew.Text.Length == 1) txt_AvailTracksNew.Text = txt_AvailTracksNew.Text == "L" ? "Lead" : (txt_AvailTracksNew.Text == "B" ? "Bass" : txt_AvailTracksNew.Text == "C" ? "Combo" : (txt_AvailTracksNew.Text == "R" ? "Rhythm" : (txt_AvailTracksNew.Text == "V" ? "Vocal" : "Instrument")));
            if (txt_AvailTracksExisting.Text.Length == 1) txt_AvailTracksExisting.Text = txt_AvailTracksExisting.Text == "L" ? "Lead" : (txt_AvailTracksExisting.Text == "B" ? "Bass" : txt_AvailTracksExisting.Text == "C" ? "Combo" : (txt_AvailTracksExisting.Text == "R" ? "Rhythm" : (txt_AvailTracksExisting.Text == "V" ? "Vocal" : "Instrument")));
            txt_TitleNew.Text = (txt_TitleNew.Text.Replace(" " + txt_AvailTracksNew.Text, " ").Replace("[" + txt_AvailTracksNew.Text, "[").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_AvailTracksNew.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(" " + txt_AvailTracksExisting.Text, " ").Replace("[" + txt_AvailTracksExisting.Text, "[").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_AvailTracksExisting.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_CoverNew_Click(object sender, EventArgs e)
        {
            //dataNew.AlbumArtPath = dataNew.AlbumArtPath.Replace("/", "\\");
            picbx_AlbumArtPathNew.ImageLocation = picbx_AlbumArtPathExisting.ImageLocation;//dataNew.AlbumArtPath.Replace(".dds", ".png");
            btn_CoverNew.Enabled = false;
            btn_CoverExisting.Enabled = false;
            lbl_AlbumArt.ForeColor = lbl_diffCount.ForeColor;
            //txt_Description.Text= dataNew.AlbumArtPath.Replace(".dds", ".png");

        }

        private void btn_CoverExisting_Click(object sender, EventArgs e)
        {
            picbx_AlbumArtPathExisting.ImageLocation = picbx_AlbumArtPathNew.ImageLocation;//eXisting.AlbumArtPath.Replace(".dds", ".png");
            btn_CoverNew.Enabled = false;
            btn_CoverExisting.Enabled = false;
            lbl_AlbumArt.ForeColor = lbl_diffCount.ForeColor;

            dupliID = lbl_IDExisting.Text;
        }

        private void btn_AddAuthor_Click(object sender, EventArgs e)
        {/*" " + " " +*/
            if (txt_AuthorNew.Text.Length > 0) txt_TitleNew.Text = (txt_TitleNew.Text.Replace(txt_AuthorNew.Text.Trim(), "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + (txt_AuthorNew.Text).Replace("Custom Song Creator", "") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ").Replace(" ]", "]").Replace("[]", "");
            if (txt_AuthorExisting.Text.Length > 0) txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(txt_AuthorExisting.Text.Trim(), "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + (txt_AuthorExisting.Text).Replace("Custom Song Creator", "") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ").Replace(" ]", "]").Replace("[]", "");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = ((txt_TitleNew.Text).Replace("noDD", "").Replace("DD", "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + (txt_DDNew.Text == "Yes" ? "DD" : "noDD") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            txt_TitleExisting.Text = ((txt_TitleExisting.Text).Replace("noDD", "").Replace("DD", "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + (txt_DDExisting.Text == "Yes" ? "DD" : "noDD").Replace("[ ", "[") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_AddTunning_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = ((txt_TitleNew.Text.Replace(txt_TuningNew.Text, "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_TuningNew.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[")).Replace("[ ", "[").Replace("  ", " ");
            txt_TitleExisting.Text = ((txt_TitleExisting.Text.Replace(txt_TuningExisting.Text, "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_TuningExisting.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[")).Replace("[ ", "[").Replace("  ", " ");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_GoToExisting_Click(object sender, EventArgs e)
        {
            string t = eXisting.Folder_Name;
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open New folder in Exporer ! ");
            }
        }

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = eXisting.oggPreviewPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p \"{0}\"",
                                                t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        private void btn_PlayAudio_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = eXisting.OggPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            //var outputBuilder = new StringBuilder();
            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void btn_PlayAudioNew_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            //var t = eXisting.OggPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var t = dataNew.OggPath.Replace(".wem", "_fixed.ogg"); ;//_fixed"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            //var tt = t.Replace(".ogg", "_preview.ogg");
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            //var outputBuilder = new StringBuilder();
            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void btn_PlayPreviewNew_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = dataNew.OggPath.Replace(".wem", ".ogg"); ;//_fixed"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var tt = t.Replace(".ogg", "_preview_fixed.ogg");
            //var t = eXisting.oggPreviewPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p \"{0}\"",
                                                tt);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(tt))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        private void button6_Click_2(object sender, EventArgs e)
        {

        }

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    txt_TitleNew.Text = ((txt_TitleNew.Text.Replace(" a." + txt_AlternateNoNew.Text, "").Replace("[a." + txt_AlternateNoNew.Text, "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_AlternateNoNew.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[")).Replace("[ ", "[").Replace("  ", " ");
        //    txt_TitleExisting.Text = ((txt_TitleExisting.Text.Replace(" a." + txt_AlternateNoExisting.Text, "").Replace("[a." + txt_AlternateNoExisting.Text, "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? " " : " [") + txt_AlternateNoExisting.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[")).Replace("[ ", "[").Replace("  ", " ");
        //    if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
        //    else lbl_Title.ForeColor = lbl_Artist.ForeColor;
        //}

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            Asses = "Stop";
            exit();
            this.Hide();
        }

        private void btn_Title2SortT_Click(object sender, EventArgs e)
        {
            syncTitle();
        }

        private void syncTitle()
        {
            txt_TitleSortExisting.Text = txt_TitleExisting.Text;
            txt_TitleSortNew.Text = txt_TitleNew.Text;
            if (txt_TitleSortExisting.Text == txt_TitleSortNew.Text) lbl_TitleSort.ForeColor = lbl_diffCount.ForeColor;
        }

        private void button6_Click_3(object sender, EventArgs e)
        {

        }
        private void syncArtist()
        {
            txt_ArtistSortExisting.Text = txt_ArtistExisting.Text;
            txt_ArtistSortNew.Text = txt_ArtistNew.Text;
            if (txt_ArtistSortExisting.Text == txt_ArtistSortNew.Text) lbl_ArtistSort.ForeColor = lbl_diffCount.ForeColor;
        }
        private void syncAlbum()
        {
            txt_AlbumSortExisting.Text = txt_AlbumExisting.Text;
            txt_AlbumSortNew.Text = txt_AlbumNew.Text;
            if (txt_AlbumSortExisting.Text == txt_AlbumSortNew.Text) lbl_AlbumSort.ForeColor = lbl_diffCount.ForeColor;
        }

        private void chbx_Autosave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_WM_Lead(string xml_inst)
        {
            var paath = ConfigRepository.Instance()["dlcm_WinMerge"];
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "WinMerge\\winmergeu.exe");
            if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install WinMerge if you want to use it.", ConfigRepository.Instance()["dlcm_WinMerge_www"], "Missing WinMerge", false, false, true, "", "", ""); frm1.ShowDialog(); return; }

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");// Path.GetDirectoryName();
            startInfo.Arguments = String.Format(xml_inst);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx)) //&& File.Exists(replace(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }

        }

        private void btn_WM_Lead_Click_1(object sender, EventArgs e)
        {
            btn_WM_Lead(leadxml);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(bassxml);
        }

        private void btn_WM_Combo_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(comboxml);
        }

        private void btn_WM_Rhythm_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(rhythmxml);
        }

        private void btn_TN_Lead_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(leadjson);
        }

        private void btn_TN_Bass_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(bassjson);
        }

        private void btn_TN_Combo_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(combojson);
        }

        private void btn_TN_Rhythm_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(rhythmjson);
        }

        private void btn_WM_Vocals_Click(object sender, EventArgs e)
        {
            btn_WM_Lead(vocalxml);
        }

        private void btn_AddPlatform_Click_1(object sender, EventArgs e)
        {
            txt_TitleNew.Text = (txt_TitleNew.Text.Replace(" a." + txt_PlatformNew.Text, "").Replace("[a." + txt_PlatformNew.Text, "[").Replace("]", "") + (txt_TitleNew.Text.IndexOf(" [") > 0 ? "" : " [") + " a." + (txt_PlatformNew.Text).Replace("[ ", "[").Replace("  ", " ") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(" a." + txt_PlatformExisting.Text, "").Replace("[a." + txt_PlatformExisting.Text, "[").Replace("]", "") + (txt_TitleExisting.Text.IndexOf(" [") > 0 ? "" : " [") + " a." + (txt_PlatformExisting.Text + (chbx_UseBrakets.Checked ? "]" : ""))).Replace("[ ", "[").Replace("  ", " ");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Toolkit.ForeColor = lbl_Reference.ForeColor;
            else lbl_Toolkit.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void chbx_LiveExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_LiveExisting.Checked || chbx_AcousticExisting.Checked) { txt_LiveDetailsExisting.Enabled = true; ExistChng = true; }
            else txt_LiveDetailsExisting.Enabled = false;
        }

        private void chbx_LiveNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_LiveNew.Checked || chbx_AcousticNew.Checked) { txt_LiveDetailsNew.Enabled = true; ExistChng = true; }
            else txt_LiveDetailsNew.Enabled = false;
        }

        private void btn_NotADuplicate_Click(object sender, EventArgs e)
        {
            //chbx_IgnoreDupli.Checked = true;
            chbx_IsAlternateNew.Checked = true;
            dataNew.Name = dataNew.SongInfo.SongDisplayName.Replace(" ", "") + dataNew.SongInfo.Artist.Replace(" ", "");
            eXisting.DLC_Name = eXisting.Song_Title.Replace(" ", "") + eXisting.Artist.Replace(" ", "");
            Asses = "Alternate;notalt";
            if (ExistChng)
            {
                if (!chbx_Autosave.Checked)
                {
                    DialogResult result1 = MessageBox.Show("Save the Existing Edits?\nYes for save \nNo for Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes) UpdateExisting();
                }
                else UpdateExisting();
            }
            exit();
            this.Hide();
        }

        private void btn_AddStandard_Click(object sender, EventArgs e)
        {
            if (txt_AuthorExisting.Text != txt_AuthorNew.Text) btn_AddAuthor_Click(null, null);
            if (txt_TuningExisting.Text != txt_TuningNew.Text) btn_AddTunning_Click(null, null);
            if (txt_DDExisting.Text != txt_DDNew.Text) btn_AddDD_Click(null, null);
            if (txt_AvailTracksExisting.Text != txt_AvailTracksNew.Text) btn_AddInstruments_Click(null, null);
            if (lbl_Existing.Text != lbl_New.Text) btn_AddAge_Click(null, null);

        }

        private void txt_XMLBassNew_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_UseDates_CheckedChanged(object sender, EventArgs e)
        {
            //var n = 0; var m = 0;
            //if (myNewDate.ToString().Substring(0, myNewDate.ToString().IndexOf(" ")-1) == myOldDate.ToString().Substring(0, myOldDate.ToString().IndexOf(" ")-1))
            //{
            //    if (myNewDate.ToString().Substring(myNewDate.ToString().IndexOf(" ") + 1, myNewDate.) == myOldDate.ToString().Substring(0, 15))
            //    {
            //        if (myNewDate.ToString().Substring(0, 21) == myOldDate.ToString().Substring(0, 21))
            //        {
            //            n = myNewDate.ToString().Length; m = myOldDate.ToString().Length;
            //        }
            //        else
            //        {
            //            n = myNewDate.ToString().Length; m = myOldDate.ToString().Length;
            //        }
            //    }
            //    else
            //    {
            //        n = myNewDate.ToString().Length; m = myOldDate.ToString().Length;
            //    }
            //}
            //else { n = myNewDate.ToString().IndexOf(" ")-1; m = myOldDate.ToString().IndexOf(" ")-1; } 

            lbl_New.Text = " " + (btn_UseDates.Checked ? (myNewDate >= myOldDate ? "(newer)" : "(older)") : myNewDate.ToString());/*.Substring(0,n)*/
            lbl_Existing.Text = " " + (btn_UseDates.Checked ? (myNewDate < myOldDate ? "(newer)" : "(older)") : myOldDate.ToString());//.Substring(0,m)
        }

        private void btbm_ShowInfoOthers_Click(object sender, EventArgs e)
        {

        }

        private void btn_ShowInfoOthers_Click(object sender, EventArgs e)
        {
            ErrorWindow frm1 = new ErrorWindow(this.allothers, "", "List of all other Duplicates being assesed", false, false, true, "", "", "");
            frm1.ShowDialog();
        }

        private void btn_AddAge_Click(object sender, EventArgs e)
        {
            string NewDate, OldDate;
            string[] myNewDates = lbl_New.Text.ToString().Split(' ');/*myNewDate*/
            string[] myOldDates = lbl_Existing.Text.ToString().Split(' ');/*myOldDate*/
            if (btn_UseDates.Checked) { NewDate = lbl_New.Text; OldDate = lbl_Existing.Text; }
            else if (myNewDates[0] != myOldDates[0]) { NewDate = myNewDates[0].Trim(); OldDate = myOldDates[0].Trim(); }
            else if (myNewDates[1] != myOldDates[1]) { NewDate = myNewDates[0].Trim() + " " + myNewDates[1].Trim(); OldDate = myOldDates[0].Trim() + " " + myOldDates[1].Trim(); }
            else { NewDate = myNewDate.ToString().Trim(); OldDate = myOldDate.ToString().Trim(); }
            //txt_TitleNew.Text = txt_TitleNew.Text.Replace(NewDate.Trim(), "");
            //txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(OldDate.Trim(), "");
            txt_TitleNew.Text = (txt_TitleNew.Text.Replace(NewDate.Trim(), "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + NewDate + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(OldDate.Trim(), "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + OldDate + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            //txt_TitleNew.Text = (txt_TitleNew.Text.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + lbl_New.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            ////txt_TitleNew.Text = txt_TitleNew.Text.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "");
            //txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace((myNewDate >= myOldDate ? lbl_Existing.Text : lbl_New.Text), "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + lbl_Existing.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_GoToNew_Click(object sender, EventArgs e)
        {
            string t = unpackedDir;
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open New folder in Exporer ! ");
            }
        }

        private void btn_AddAlternate_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = (txt_TitleNew.Text.Replace(" a." + txt_AlternateNoNew.Text, "").Replace("[a." + txt_AlternateNoNew.Text, "[").Replace("]", "") + (txt_TitleNew.Text.IndexOf(" [") > 0 ? "" : " [") + " a." + (txt_AlternateNoNew.Text).Replace("[ ", "[").Replace("  ", " ") + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[").Replace("  ", " ");
            txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(" a." + txt_AlternateNoExisting.Text, "").Replace("[a." + txt_AlternateNoExisting.Text, "[").Replace("]", "") + (txt_TitleExisting.Text.IndexOf(" [") > 0 ? "" : " [") + " a." + (txt_AlternateNoExisting.Text + (chbx_UseBrakets.Checked ? "]" : ""))).Replace("[ ", "[").Replace("  ", " ");
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            if (chbx_Sort.Checked) syncTitle();
        }

        private void btn_Artist2SortA_Click(object sender, EventArgs e)
        {
            syncArtist();
        }

        private void Btn_AlbumSortNew_Click(object sender, EventArgs e)
        {
            txt_AlbumSortNew.Text = txt_AlbumSortExisting.Text;
            btn_AlbumSortExisting.Enabled = false;
            btn_AlbumSortNew.Enabled = false;
            lbl_AlbumSort.ForeColor = lbl_diffCount.ForeColor;
            txt_YearNew.Text = txt_YearExisting.Text;

            txt_YearNew.ForeColor = lbl_diffCount.ForeColor;
            txt_YearExisting.ForeColor = lbl_diffCount.ForeColor;
        }

        private void Btn_AlbumSortExisting_Click(object sender, EventArgs e)
        {
            txt_AlbumSortExisting.Text = txt_AlbumSortNew.Text;
            txt_YearExisting.Text = txt_YearNew.Text;
            btn_AlbumSortExisting.Enabled = false;
            btn_AlbumSortNew.Enabled = false;
            lbl_AlbumSort.ForeColor = lbl_diffCount.ForeColor;

            txt_YearNew.ForeColor = lbl_diffCount.ForeColor;
            txt_YearExisting.ForeColor = lbl_diffCount.ForeColor;
        }

        private void Btn_Album2SortA_Click(object sender, EventArgs e)
        {
            syncAlbum();
        }

        private void Lbl_ArtistSort_Click(object sender, EventArgs e)
        {

        }
    }
}
