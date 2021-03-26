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
        public string liveDetails { get; set; }
        public string dupli_reason { get; set; }
        public string lengty { get; set; }
        public string allothers { get; set; }
        public string yearalbum { get; set; }
        public string albumsort { get; set; }
        public OleDbConnection cnb { get; set; }
        public int reffy { get; set; }
        public string isAcoustic { get; set; }
        public string isFullAlbum { get; set; }
        public string isEP { get; set; }
        public string isSingle { get; set; }
        public string isSoundtrack { get; set; }
        public string isInstrumental { get; set; }
        public string isUncensored { get; set; }
        public string isRemastered { get; set; }
        public string inTheWorks { get; set; }
        //public bool newold { get; set; }
        //public string clist { get; set; }
        //public DuplicatesManagement(string txt_DBFolder, Files eXisting, DLCPackageData dataNew, string author, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> xmlhlist, List<string> jsonhlist)
        //{
        //    InitializeComponent();
        //    //MessageBox.Show("test0");
        //    DB_Path = txt_DBFolder;
        //    DB_Path = DB_Path + "\\AccessDB.accdb";
        //    MessageBox.Show("test1");

        //}

        internal frm_Duplicates_Management(GenericFunctions.MainDBfields filed, DLCPackageData datas, string author, string tkversion, string dD,
            string bass, string guitar, string combo, string rhythm, string lead, string vocal, string tunnings, int i, int norows, string original_FileName,
            string art_hash, string audio_hash, string audioPreview_hash, List<string> xmlhlist, List<string> jsonhlist, string DBPath, List<string> clist,
            List<string> dlist, bool newold, string Is_Original, string altvert, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete,
            string FileSize, string unpackedDir, string Is_MultiTrack, string MultiTrack_Version, string FileDate, string title_duplic,
            string original_Platform, string IsLive, string LiveDetails, string IsAcoustic, OleDbConnection cnnb, string dupli_reasons
            , string lengty, string allothers, int reff, List<string> cxmlhlist, List<string> snghlist, string filehash
            , string IsInstrumental, string IsSoundtrack, string IsFullAlbum, string IsSingle, string IsEP, string IsUncensored, string IsRemastered, string InTheWorks)//, string yeara, string albumsa)//string Is_MultiTracking, string Multitracking, 
        //file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_reff, audio_hash, audioPreview_hash, xmlhlist, jsonhlist, DB_Path, clist, dlist, newold, Is_Original, altver
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
            //this.xmlhlist = xmlhlist;
            //this.jsonhlist = jsonhlist;
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
            this.isInstrumental = IsInstrumental;
            this.isEP = IsEP;
            this.isSingle = IsSingle;
            this.isUncensored = IsUncensored;
            this.isRemastered = IsRemastered;
            this.isFullAlbum = IsFullAlbum;
            this.isSoundtrack = IsSoundtrack;
            this.inTheWorks = InTheWorks;
            //MessageBox.Show(DB_Path);
            //DB_Path = text;
            //MessageBox.Show(DB_Path);
            //DB_Path = DB_Path;// + "\\AccessDB.accdb";
            this.dupli_reason = dupli_reasons;
            this.cnb = cnnb;
            this.lengty = lengty;
            this.allothers = allothers;
            this.reffy = reff;
            this.cxmlhlist = cxmlhlist;
            this.snghlist = snghlist;
            this.xmlhlist = xmlhlist;
            this.jsonhlist = jsonhlist;
            this.filehash = filehash;
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
            if (dataNew.SongInfo.Artist.ToLower() is null) return;
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
                btn_Alternate.Text = "Keep as Is" + (chbx_Autosave.Checked ? " and Save" : "");
                btn_Ignore.Text = "Delete yrLeft";
                btn_Update.Text = "Delete yrRight";
                btn_StopImport.Text = "Stop the Assesment";
                lbl_IDNew.Text = reffy.ToString();
                lbl_IDNew.Visible = true;
                lblNew.Text = "Your Left Side";
                lblExisting.Text = "Your right Side";
            }
            else
            {
                btn_Alternate.Text = "Import New as Alternate";
                btn_Ignore.Text = "Ignore New as OLD / Duplicate";
                btn_Update.Text = "Update and Overrite Existing";
                btn_StopImport.Text = "Stop the Import";
                lbl_IDNew.Visible = false;
                lblNew.Text = "Currently Importing";
                lblExisting.Text = "Already Existing";
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
            if (Is_Original != "Yes")
            {
                txt_AlternateNoNew.Value = eXisting.Is_Alternate == "Yes" ? 0 : altver.ToInt32();
                chbx_IsAlternateNew.Checked = eXisting.Is_Alternate == "Yes" ? false : true; /*!(eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" && Is_MultiTracks != "Yes") ? : false; */
            }
            if (eXisting.Is_Original != "Yes")
            {
                chbx_IsAlternateExisting.Checked = eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" ? true : false;
                txt_AlternateNoExisting.Enabled = eXisting.Is_Alternate == "Yes" && eXisting.Is_Multitrack != "Yes" ? true : false;
                txt_AlternateNoExisting.Value = (eXisting.Alternate_Version_No.ToInt32() == -1) ? 0 : eXisting.Alternate_Version_No.ToInt32();
            }

            //Multitrack
            //txt_MultiTrackNew.Text = "";
            if (Is_MultiTracks != (eXisting.Is_Multitrack == "No" ? "" : eXisting.Is_Multitrack)
                || MultiTrack_Versions != (eXisting.MultiTrack_Version == "No" ? "" : eXisting.MultiTrack_Version)
                || isLive != (eXisting.Is_Live == "No" ? "" : eXisting.Is_Live)
                || liveDetails != (eXisting.Live_Details == "No" ? "" : eXisting.Live_Details)) { lbl_Multitrack.ForeColor = lbl_Reference.ForeColor; }
            else if (((Is_MultiTracks == ""|| Is_MultiTracks == "No") && ("" == eXisting.Is_Multitrack || "No" == eXisting.Is_Multitrack))
                && (MultiTrack_Versions == ""  && "" == eXisting.MultiTrack_Version)
                && ((isLive == ""  || isLive == "No" ) && ("" == eXisting.Is_Live || "No" == eXisting.Is_Live))
                && (liveDetails == "" && "" == eXisting.Live_Details)) lbl_Multitrack.Text = "";
            
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

            if (isAcoustic != (eXisting.Is_Acoustic=="No"? "" : eXisting.Is_Acoustic)) lbl_P2.ForeColor = lbl_Reference.ForeColor;
            else if ((isAcoustic == ""|| isAcoustic=="No") && ("" == eXisting.Is_Acoustic || "No" == eXisting.Is_Acoustic)) lbl_P2.Text = "";
            chbx_AcousticNew.Checked = isAcoustic == "Yes" ? true : false;
            chbx_AcousticExisting.Checked = eXisting.Is_Acoustic == "Yes" ? true : false;

            if (isEP != (eXisting.Is_EP == "No" ? "" : eXisting.Is_EP)) lbl_P3.ForeColor = lbl_Reference.ForeColor;
            else if ((isEP == "" || isEP == "No") && ("" == eXisting.Is_EP || "No" == eXisting.Is_EP)) lbl_P3.Text = "";
            chbx_EPNew.Checked = isEP == "Yes" ? true : false;
            chbx_EPExisting.Checked = eXisting.Is_EP == "Yes" ? true : false;

            if (isSingle != (eXisting.Is_Single == "No" ? "" : eXisting.Is_Single)) lbl_P1.ForeColor = lbl_Reference.ForeColor;
            else if ((isSingle == "" || isSingle == "No") && ("" == eXisting.Is_Single || "No" == eXisting.Is_Single)) lbl_P1.Text = "";
            chbx_SingleNew.Checked = isSingle == "Yes" ? true : false;
            chbx_SingleExisting.Checked = eXisting.Is_Single == "Yes" ? true : false;

            if (isInstrumental != (eXisting.Is_Instrumental == "No" ? "" : eXisting.Is_Instrumental)) lbl_P1.ForeColor = lbl_Reference.ForeColor;
            else if ((isInstrumental == "" || isInstrumental == "No") && ("" == eXisting.Is_Instrumental || "No" == eXisting.Is_Instrumental)) lbl_P1.Text = "";
            chbx_InstrumentalNew.Checked = isInstrumental == "Yes" ? true : false;
            chbx_InstrumentalExisting.Checked = eXisting.Is_Instrumental == "Yes" ? true : false;

            if (isFullAlbum != (eXisting.Is_FullAlbum == "No" ? "" : eXisting.Is_FullAlbum)) lbl_P2.ForeColor = lbl_Reference.ForeColor;
            else if ((isFullAlbum == "" || isFullAlbum == "No") && ("" == eXisting.Is_FullAlbum || "No" == eXisting.Is_FullAlbum)) lbl_P2.Text = "";
            chbx_FullAlbumNew.Checked = isFullAlbum == "Yes" ? true : false;
            chbx_FullAlbumExisting.Checked = eXisting.Is_FullAlbum == "Yes" ? true : false;

            if (isSoundtrack != (eXisting.Is_Soundtrack == "No" ? "" : eXisting.Is_Soundtrack)) lbl_P4.ForeColor = lbl_Reference.ForeColor;
            else if ((isSoundtrack == "" || isSoundtrack == "No") && ("" == eXisting.Is_Soundtrack || "No" == eXisting.Is_Soundtrack)) lbl_P4.Text = "";
            chbx_SoundtrackNew.Checked = isSoundtrack == "Yes" ? true : false;
            chbx_SoundtrackExisting.Checked = eXisting.Is_Soundtrack == "Yes" ? true : false;

            if (isUncensored != (eXisting.Is_Uncensored == "No" ? "" : eXisting.Is_Uncensored)) lbl_P4.ForeColor = lbl_Reference.ForeColor;
            else if ((isUncensored == "" || isUncensored == "No") && ("" == eXisting.Is_Uncensored || "No" == eXisting.Is_Uncensored)) lbl_P4.Text = "";
            chbx_UncensoredNew.Checked = isUncensored == "Yes" ? true : false;
            chbx_UncensoredExisting.Checked = eXisting.Is_Uncensored == "Yes" ? true : false;

            if (isRemastered != (eXisting.Is_Remastered == "No" ? "" : eXisting.Is_Remastered)) lbl_P3.ForeColor = lbl_Reference.ForeColor;
            else if ((isRemastered == "" || isRemastered == "No") && ("" == eXisting.Is_Remastered || "No" == eXisting.Is_Remastered)) lbl_P3.Text = "";
            chbx_RemasteredNew.Checked = isRemastered == "Yes" ? true : false;
            chbx_RemasteredExisting.Checked = eXisting.Is_Remastered == "Yes" ? true : false;

            if (inTheWorks != (eXisting.IntheWorks == "No" ? "" : eXisting.IntheWorks)) lbl_P3.ForeColor = lbl_Reference.ForeColor;
            else if ((inTheWorks == "" || inTheWorks == "No") && ("" == eXisting.IntheWorks || "No" == eXisting.IntheWorks)) lbl_P3.Text = "";
            chbx_InTheWorksNew.Checked = inTheWorks == "Yes" ? true : false;
            chbx_InTheWorksExisting.Checked = eXisting.IntheWorks == "Yes" ? true : false;


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

            ToolTip toolTip10 = new ToolTip(); //jsonbass
            toolTip10.SetToolTip(lbl_Size, toolTip10.GetToolTip(lbl_Size) + "Existing Hash: " + eXisting.File_Hash + "\nNew Hash: " + filehash);
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

            if (dataNew.SongInfo.AlbumSort != eXisting.Album_Sort) { lbl_AlbumSort.ForeColor = lbl_Reference.ForeColor; txt_AlbumSortExisting.Enabled = true; btn_AlbumSortNew.Enabled = true; }
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
            if (Is_Original == "Yes") lbl_NewIs_Original.ForeColor = lbl_Reference.ForeColor;
            if (eXisting.Is_Original == "Yes") lbl_ExistingIs_Original.ForeColor = lbl_Reference.ForeColor;

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
            if (eXisting.AlbumArtPath != null) picbx_AlbumArtPathExisting.ImageLocation = eXisting.AlbumArtPath.Replace(".dds", ".png");


            //SAme hash (option 79 selected?)
            if (filehash == eXisting.File_Hash)
            {
                if (GetHash(c("dlcm_RocksmithDLCPath") + "\\" + original_FileName) != GetHash(c("dlcm_TempPath") + "\\0_old\\" + eXisting.Original_FileName)) lbl_FileHash.Text = lbl_FileHash.Text + " saved, now different :)";
                lbl_FileHash.Visible = true;
            }

            if (eXisting.Audio_OrigHash != audio_hash) lbl_Audio.ForeColor = lbl_Reference.ForeColor;/*|| eXisting.Audio_Hash != audio_hash*/
            else if (eXisting.Audio_OrigHash == "" && "" == audio_hash) lbl_Audio.Text = "";
            else { lbl_AudioMain.ForeColor = System.Drawing.Color.Green; lbl_AudioMain.Font = new Font(lbl_AudioMain.Font.Name, 9, FontStyle.Bold | FontStyle.Underline); }
            txt_AudioNew.Text = (audio_hash.ToString() == "" ? "" : "Yes");
            if(eXisting.Audio_OrigHash != null) txt_AudioExisting.Text = (eXisting.Audio_OrigHash.ToString() == "" ? "" : "Yes");

            if (eXisting.Audio_OrigPreviewHash != audioPreview_hash) lbl_Preview.ForeColor = lbl_Reference.ForeColor;
            else if (eXisting.Audio_OrigPreviewHash == "" && "" == audioPreview_hash) lbl_Vocals.Text = "";
            else { lbl_AudioPreview.ForeColor = System.Drawing.Color.Green; lbl_AudioMain.Font = new Font(lbl_AudioPreview.Font.Name, 9, FontStyle.Bold | FontStyle.Underline); }
            txt_PreviewNew.Text = (audioPreview_hash.ToString() == "" ? "No" : "Yes");
            if(eXisting.Audio_OrigPreviewHash != null) txt_PreviewExisting.Text = (eXisting.Audio_OrigPreviewHash.ToString() == "" ? "No" : "Yes");
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
            var cleanedXMLHash = "";
            var sngHash = "";
            //bool diff;// = true;
            int k = 0;
            string ConversionDateTime_cur = "";
            string ConversionDateTime_exist = "";
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

            ToolTip toolTip1 = new ToolTip(); //vocals
            ToolTip toolTip2 = new ToolTip(); //lead
            ToolTip toolTip3 = new ToolTip(); //rhythm
            ToolTip toolTip4 = new ToolTip(); //bass
            ToolTip toolTip5 = new ToolTip(); //combo
            ToolTip toolTip6 = new ToolTip(); //jsonlead
            ToolTip toolTip7 = new ToolTip(); //jsonrhythm
            ToolTip toolTip8 = new ToolTip(); //jsonbass
            ToolTip toolTip9 = new ToolTip(); //jsoncombo
            toolTip2.RemoveAll();

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
                    ConversionDateTime_cur = "";
                    ConversionDateTime_exist = "";
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        //MessageBox.Show(noOfRec.ToString());
                        //rtxt_StatisticsOnReadDLCs.Text = xmlhlist[i]+"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        XmlHash = ds.Tables[0].Rows[i].ItemArray[6].ToString(); // XmlHash                                  
                        XmlName = ds.Tables[0].Rows[i].ItemArray[17].ToString() + ds.Tables[0].Rows[i].ItemArray[25].ToString(); //type+routemask+
                        XmlUUID = ds.Tables[0].Rows[i].ItemArray[28].ToString(); //xml.uuid
                        XmlFile = ds.Tables[0].Rows[i].ItemArray[5].ToString(); //xml.filepath
                        jsonFile = ds.Tables[0].Rows[i].ItemArray[4].ToString(); //json.filepath
                        jsonHash = ds.Tables[0].Rows[i].ItemArray[43].ToString(); // jsonHash  
                        cleanedXMLHash = ds.Tables[0].Rows[i].ItemArray[42].ToString(); // cleanedXmlHash  
                        sngHash = ds.Tables[0].Rows[i].ItemArray[38].ToString(); // sng file hash 

                        var xx = Directory.GetFiles(arg.SongXml.File.Replace("\\songs\\arr\\" + arg.SongXml.Name + ".xml", ""), "*.json", SearchOption.AllDirectories)[0];
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul36"] == "Yes") //37. Keep the Uncompressed Songs superorganized   chbx_Additional_Manipulations.GetItemChecked(36)                             
                            arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                        else
                            arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(xx));

                        ConversionDateTime_cur = GetTExtFromFile(arg.SongXml.File).Trim(' ');
                        ConversionDateTime_exist = GetTExtFromFile(XmlFile).Trim(' ');
                        if (ConversionDateTime_cur.Length > 3)
                        {
                            if (ConversionDateTime_cur.IndexOf("-") == 1) ConversionDateTime_cur = "0" + ConversionDateTime_cur;
                            if (ConversionDateTime_cur.IndexOf("-", 3) == 4) ConversionDateTime_cur = ConversionDateTime_cur.Substring(0, 3) + "0" + ConversionDateTime_cur.Substring(3, ((ConversionDateTime_cur.Length) - 3));
                            if (ConversionDateTime_cur.IndexOf(":") == 10) ConversionDateTime_cur = ConversionDateTime_cur.Substring(0, 9) + "0" + ConversionDateTime_cur.Substring(9, ConversionDateTime_cur.Length - 9);
                        }
                        if (ConversionDateTime_exist.Length > 3)
                        {
                            if (ConversionDateTime_exist.IndexOf("-") == 1) ConversionDateTime_exist = "0" + ConversionDateTime_exist;
                            if (ConversionDateTime_exist.IndexOf("-", 3) == 4) ConversionDateTime_exist = ConversionDateTime_exist.Substring(0, 3) + "0" + ConversionDateTime_exist.Substring(3, ((ConversionDateTime_exist.Length) - 3));
                            if (ConversionDateTime_exist.IndexOf(":") == 10) ConversionDateTime_exist = ConversionDateTime_exist.Substring(0, 9) + "0" + ConversionDateTime_exist.Substring(9, ConversionDateTime_exist.Length - 9);
                        }

                        lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File).Trim(' ');
                        lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile).Trim(' ');
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
                        {
                            //XML (incl. cleaned XML and SNG) check
                            if (arg.RouteMask.ToString() == "Bass")
                            {
                                bassxml = "";
                                lbl_XMLBass.Visible = true;
                                if (!(ConversionDateTime_cur == "" && "" == ConversionDateTime_exist))
                                    if ((ConversionDateTime_cur != ConversionDateTime_exist || XmlHash != xmlhlist[k]) && (cleanedXMLHash != cxmlhlist[k] && sngHash != snghlist[k])) { lbl_XMLBass.ForeColor = lbl_Reference.ForeColor; btn_WM_Bass.Enabled = true; }
                                toolTip4.SetToolTip(lbl_XMLBass, toolTip4.GetToolTip(lbl_XMLBass) + "Existing ConversionDate: " + ConversionDateTime_exist + "\nNew ConversionDate: " + ConversionDateTime_cur + "\nExisting XML: "
                                    + XmlHash + "\nNew XML: " + xmlhlist[k] + "\nExisting Cleaned XML: " + cleanedXMLHash + "\nNew Cleaned XML: " + cxmlhlist[k] + "\nExisting SNG: " + sngHash + "\nNew SNG: " + snghlist[k]);
                                txt_XMLBassNew.Text = ConversionDateTime_cur;
                                txt_XMLBassExisting.Text = ConversionDateTime_exist;
                                bassxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Lead")
                            {
                                leadxml = "";
                                lbl_XMLLead.Visible = true;
                                if (!(ConversionDateTime_cur == "" && "" == ConversionDateTime_exist))
                                    if ((ConversionDateTime_cur != ConversionDateTime_exist || XmlHash != xmlhlist[k]) && (cleanedXMLHash != cxmlhlist[k] && sngHash != snghlist[k])) { lbl_XMLLead.ForeColor = lbl_Reference.ForeColor; btn_WM_Leads.Enabled = true; }
                                toolTip2.SetToolTip(lbl_XMLLead, toolTip2.GetToolTip(lbl_XMLLead) + "Existing ConversionDate: " + ConversionDateTime_exist + "\nNew ConversionDate: " + ConversionDateTime_cur + "\nExisting XML: "
                                    + XmlHash + "\nNew XML: " + xmlhlist[k] + "\nExisting Cleaned XML: " + cleanedXMLHash + "\nNew Cleaned XML: " + cxmlhlist[k] + "\nExisting SNG: " + sngHash + "\nNew SNG: " + snghlist[k]);
                                txt_XMLLeadNew.Text = ConversionDateTime_cur;
                                txt_XMLLeadExisting.Text = ConversionDateTime_exist;
                                leadxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Combo")
                            {
                                comboxml = "";
                                lbl_XMLCombo.Visible = true;
                                if (!(ConversionDateTime_cur == "" && "" == ConversionDateTime_exist))
                                    if ((ConversionDateTime_cur != ConversionDateTime_exist || XmlHash != xmlhlist[k]) && (cleanedXMLHash != cxmlhlist[k] && sngHash != snghlist[k])) { lbl_XMLCombo.ForeColor = lbl_Reference.ForeColor; btn_WM_Combo.Enabled = true; }
                                toolTip5.SetToolTip(lbl_XMLCombo, toolTip5.GetToolTip(lbl_XMLCombo) + "Existing ConversionDate: " + ConversionDateTime_exist + "\nNew ConversionDate: " + ConversionDateTime_cur + "\nExisting XML: "
                                     + XmlHash + "\nNew XML: " + xmlhlist[k] + "\nExisting Cleaned XML: " + cleanedXMLHash + "\nNew Cleaned XML: " + cxmlhlist[k] + "\nExisting SNG: " + sngHash + "\nNew SNG: " + snghlist[k]);
                                txt_XMLComboNew.Text = ConversionDateTime_cur;
                                txt_XMLComboExisting.Text = ConversionDateTime_exist;
                                leadxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Rhythm")
                            {
                                rhythmxml = "";
                                lbl_XMLRhythm.Visible = true;
                                if (!(ConversionDateTime_cur == "" && "" == ConversionDateTime_exist))
                                    if ((ConversionDateTime_cur != ConversionDateTime_exist || XmlHash != xmlhlist[k]) && (cleanedXMLHash != cxmlhlist[k] && sngHash != snghlist[k])) { lbl_XMLRhythm.ForeColor = lbl_Reference.ForeColor; btn_WM_Rhythm.Enabled = true; }
                                toolTip3.SetToolTip(lbl_XMLRhythm, toolTip3.GetToolTip(lbl_XMLRhythm) + "Existing ConversionDate: " + ConversionDateTime_exist + "\nNew ConversionDate: " + ConversionDateTime_exist + "\nExisting XML: "
                                    + XmlHash + "\nNew XML: " + xmlhlist[k] + "\nExisting Cleaned XML: " + cleanedXMLHash + "\nNew Cleaned XML: " + cxmlhlist[k] + "\nExisting SNG: " + sngHash + "\nNew SNG: " + snghlist[k]);
                                txt_XMLRhythmNew.Text = ConversionDateTime_cur;
                                txt_XMLRhythmExisting.Text = ConversionDateTime_exist;
                                rhythmxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";

                            }

                            if (arg.RouteMask.ToString() == "Vocal" || XmlName == "VocalNone")
                            {
                                //if () { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; btn_WM_Vocals.Enabled = true; }
                                lbl_Vocals.Visible = true;
                                if ((XmlHash != xmlhlist[k] || jsonHash != jsonhlist[k]) && (cleanedXMLHash != cxmlhlist[k] && sngHash != snghlist[k])) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; btn_WM_Vocals.Enabled = true; }
                                toolTip1.SetToolTip(lbl_Vocals, toolTip1.GetToolTip(lbl_Vocals) + "Existing ConversionDate: " + ConversionDateTime_exist + "\nNew ConversionDate: " + ConversionDateTime_cur + "\nExisting XML: "
                                    + XmlHash + "\nNew XML: " + xmlhlist[k] + "\nExisting Cleaned XML: " + cleanedXMLHash + "\nNew Cleaned XML: " + cxmlhlist[k] + "\nExisting SNG: " + sngHash + "\nNew SNG: " + snghlist[k]);
                                vocalxml = "\"" + arg.SongXml.File + "\"" + " " + "\"" + XmlFile + "\"";
                            }

                            //JSON check
                            if (arg.RouteMask.ToString() == "Bass")
                            {
                                bassjson = "";
                                lbl_JSONBass.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != jsonhlist[k]) { lbl_JSONBass.ForeColor = lbl_Reference.ForeColor; btn_TN_Bass.Enabled = true; }
                                toolTip8.SetToolTip(lbl_JSONBass, toolTip8.GetToolTip(lbl_JSONBass) + "Existing ConversionDate: " + lastConverjsonDateTime_exist + "\nNew ConversionDate: " + lastConverjsonDateTime_cur + "\nExisting JSON: "
                                    + jsonHash + "\nNew JSON: " + jsonhlist[k]);
                                txt_JSONBassNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONBassExisting.Text = lastConverjsonDateTime_exist;
                                bassjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";

                            }
                            if (arg.RouteMask.ToString() == "Lead")
                            {
                                lbl_JSONLead.Visible = true;
                                leadjson = "";
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != jsonhlist[k]) { lbl_JSONLead.ForeColor = lbl_Reference.ForeColor; btn_TN_Lead.Enabled = true; }
                                toolTip6.SetToolTip(lbl_JSONLead, toolTip6.GetToolTip(lbl_JSONLead) + "Existing ConversionDate: " + lastConverjsonDateTime_exist + "\nNew ConversionDate: " + lastConverjsonDateTime_cur + "\nExisting JSON: "
                                + jsonHash + "\nNew JSON: " + jsonhlist[k]);
                                txt_JSONLeadNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONLeadExisting.Text = lastConverjsonDateTime_exist;
                                leadjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";
                            }
                            if (arg.RouteMask.ToString() == "Combo")
                            {
                                combojson = "";
                                lbl_JSONCombo.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != jsonhlist[k]) { lbl_JSONCombo.ForeColor = lbl_Reference.ForeColor; btn_TN_Combo.Enabled = true; }
                                toolTip9.SetToolTip(lbl_JSONCombo, toolTip9.GetToolTip(lbl_JSONCombo) + "Existing ConversionDate: " + lastConverjsonDateTime_exist + "\nNew ConversionDate: " + lastConverjsonDateTime_cur + "\nExisting JSON: "
                                + jsonHash + "\nNew JSON: " + jsonhlist[k]);
                                txt_JSONComboNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONComboExisting.Text = lastConverjsonDateTime_exist;
                                combojson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";
                            }
                            if (arg.RouteMask.ToString() == "Rhythm")
                            {
                                rhythmjson = "";
                                lbl_JSONRhythm.Visible = true;
                                if (!(lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist))
                                    if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != jsonhlist[k]) { lbl_JSONRhythm.ForeColor = lbl_Reference.ForeColor; btn_TN_Rhythm.Enabled = true; }
                                toolTip7.SetToolTip(lbl_JSONRhythm, toolTip7.GetToolTip(lbl_JSONRhythm) + "Existing ConversionDate: " + lastConverjsonDateTime_exist + "\nNew ConversionDate: " + lastConverjsonDateTime_cur + "\nExisting JSON: "
                                + jsonHash + "\nNew JSON: " + jsonhlist[k]);
                                txt_JSONRhythmNew.Text = lastConverjsonDateTime_cur;
                                txt_JSONRhythmExisting.Text = lastConverjsonDateTime_exist;
                                rhythmjson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";
                            }
                            //if (arg.ArrangementType.ToString() == "Vocal")
                            //{
                            //    vocaljson = "";
                            //    lbl_Vocals.Visible = true;
                            //    if (jsonHash != jsonhlist[k]) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; btn_WM_Vocals.Enabled = true; }
                            //    vocaljson = "\"" + arg.SongFile.File + "\"" + " " + "\"" + jsonFile + "\"";
                            //}
                        }


                        //Get the oldest timestamp
                        myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                        myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);

                        if (ConversionDateTime_cur.Length > 3)
                        {
                            myCurDate = DateTime.ParseExact(ConversionDateTime_cur, "MM-dd-yy HH:mm", enUS);
                            if ((myCurDate > myNewDate) && (arg.RouteMask.ToString() == "Bass" || arg.RouteMask.ToString() == "Lead" || arg.RouteMask.ToString() == "Rhythm" || arg.RouteMask.ToString() == "Combo"))
                                datenew = ConversionDateTime_cur;
                        }
                        if (ConversionDateTime_exist.Length > 3)
                        {
                            myExisDate = DateTime.ParseExact(ConversionDateTime_exist, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                            if (myExisDate > myOldDate && (arg.RouteMask.ToString() == "Bass" || arg.RouteMask.ToString() == "Lead" || arg.RouteMask.ToString() == "Rhythm" || arg.RouteMask.ToString() == "Combo"))
                                dateold = ConversionDateTime_exist;
                        }

                        myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                        myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                        if (dateold != "12-13-11 13:11" && datenew != "12-13-11 13:11")
                        {
                            //Commenting out the Auto addition of OLDer&Newer
                            if (newold && eXisting.Is_Original != "Yes" && Is_Original != "Yes")
                            {
                                txtnew = " " + (btn_UseDates.Checked ? (myNewDate >= myOldDate ? lbl_Existing.Text : lbl_New.Text) : myNewDate.ToString());
                                txtold = " " + (btn_UseDates.Checked ? (myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text) : myOldDate.ToString());
                            }
                            LastConvDate_exis = myOldDate > LastConvDate_exis ? myOldDate : LastConvDate_exis;
                            LastConvDate_new = myNewDate > LastConvDate_new ? myNewDate : LastConvDate_new;
                        }
                    }
                    //if (jsonHash != jsonhlist[k]) lbl_tonediff.Visible = true;
                    k++;
                }

            }

            ExistChng = false;
            lbl_New.Text = txtnew;
            lbl_Existing.Text = txtold;
            lbl_DateNew.Text = LastConvDate_new.ToString();
            lbl_DateExisting.Text = LastConvDate_exis.ToString();

            this.Text += ". " + title_duplic;

            //some issue...its always different ergo not reliable ergo disabling any informal indicator
            lbl_JSONLead.Text = "";
            lbl_JSONBass.Text = "";
            lbl_JSONCombo.Text = "";
            lbl_JSONRhythm.Text = "";

            if (ConfigRepository.Instance()["dlcm_AdditionalManipul13"] == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul85"] == "Yes")
            {
                btn_AddStandard_Click(null, null); btn_Alternate_Click(null, null);
                return;
            }
        }

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
        private List<string> xmlhlist;
        private List<string> jsonhlist;
        private List<string> cxmlhlist;
        private List<string> snghlist;
        private List<string> clist;
        private List<string> dlist;
        private bool newold;
        private string filehash;

        public static string GetTExtFromFile(string ss)
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
                while ((line = info.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("lastconversiondatetime"))
                    {
                        tecst = line.ToLower().Replace("<lastconversiondatetime>", "").Replace("</lastconversiondatetime>", "");
                        if (tecst == line.ToLower())
                            tecst = line.ToLower().Replace("\"", "").Replace("lastconversiondatetime: ", "").Replace(",", "");
                        break;
                    }
                }
                info.Close();
            }
            return tecst;
        }

        private void btn_Alternate_Click(object sender, EventArgs e)
        {
            Asses = "Alternate;manual decision: Alternative";
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

        private void btn_Ignore_Click(object sender, EventArgs e)
        {
            Asses = "Ignore;manual decision: Ignore";
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
            Asses = "Update;manual decision: Update";
            exit();
            this.Hide();
        }
        public void exit()
        {
            Author = (txt_AuthorNew.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "No" ? "" : "Custom Song Creator") : txt_AuthorNew.Text);
            Version = (txt_VersionNew.Text == "" ? "1" : txt_VersionNew.Text);
            DLCID = txt_DLCIDNew.Text;
            Title = txt_TitleNew.Text;
            Comment = txt_DescriptionExisting.Text;//not used
            Description = txt_DescriptionNew.Text;
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
            YouTube_Link = txt_YouTube_LinkNew.Text;
            CustomsForge_Link = txt_CustomsForge_LinkNew.Text;
            CustomsForge_Like = txt_CustomsForge_LikeNew.Text;
            CustomsForge_ReleaseNotes = txt_CustomsForge_ReleaseNotesNew.Text;
            ExistingTrackNo = eXisting.Track_No;
            IgnoreRest = chbx_IgnoreDupli.Checked;
            isLive = chbx_LiveNew.Checked ? "Yes" : "No";
            isAcoustic = chbx_AcousticNew.Checked ? "Yes" : "No";
            isSingle = chbx_SingleNew.Checked ? "Yes" : "No";
            isEP = chbx_EPNew.Checked ? "Yes" : "No";
            isInstrumental = chbx_InstrumentalNew.Checked ? "Yes" : "No";
            isFullAlbum = chbx_FullAlbumNew.Checked ? "Yes" : "No";
            isSoundtrack = chbx_SoundtrackNew.Checked ? "Yes" : "No";
            isUncensored = chbx_UncensoredNew.Checked ? "Yes" : "No";
            isRemastered = chbx_RemasteredNew.Checked ? "Yes" : "No";
            inTheWorks = chbx_InTheWorksNew.Checked ? "Yes" : "No";
            yearalbum = txt_YearNew.Text; ;
            albumsort = txt_AlbumSortNew.Text;

            ConfigRepository.Instance()["dlcm_DupliM_Sync"] = chbx_Sort.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_Autosave.Checked == true ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_DupliUseDates"] = btn_UseDates.Checked == true ? "Yes" : "No";

            //Delete old arrangements and tones
            //Clean CachetDB
            if (Asses.IndexOf("Update") == 0)
            {
                DeleteFromDB("Tones", "DELETE * FROM Tones WHERE CDLC_ID=" + lbl_IDExisting.Text, cnb);
                DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE CDLC_ID=" + lbl_IDExisting.Text, cnb);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateExisting();
            ExistChng = false;
        }

        public void UpdateExisting()
        {
            var sel = "";
            // in case a similar artist covered the song ...don't attach wrong names
            var al = txt_AlbumExisting.Text;
            var ar = txt_ArtistExisting.Text;
            var alt = chbx_IsAlternateExisting.Checked ? txt_AlternateNoExisting.Value.ToString() : null;

            sel = "UPDATE Main SET Artist=\"" + ar + "\", Artist_Sort=\"" + txt_ArtistSortExisting.Text + "\", Album=\"" + al + "\", Song_Title=\"" + txt_TitleExisting.Text;
            sel += "\", Song_Title_Sort=\"" + txt_TitleSortExisting.Text + "\", Author=\"" + (txt_AuthorExisting.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" ? "Custom Song Creator" : "") : txt_AuthorExisting.Text);
            sel += "\", Version=\"" + (txt_VersionExisting.Text == "" ? "1" : txt_VersionExisting.Text) + "\", DLC_Name=\"" + txt_DLCIDExisting.Text + "\",";
            sel += (txt_DescriptionExisting.Text == "" ? "" : " Description = \"" + txt_DescriptionExisting.Text + "\",");/// + (txt_DescriptionExistig.Text == "" ? "" : " Comments = \"" + txt_DescriptionExistig.Text + "\","); //"\"," +
            sel += " Is_Alternate = \"" + (chbx_IsAlternateExisting.Checked ? "Yes" : "No") + "\", Alternate_Version_No = \"" + alt + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_MultiTrack = \"" + (chbx_MultiTrackExisting.Checked ? "Yes" : "No") + "\", MultiTrack_Version = \"" + (chbx_MultiTrackExisting.Checked ? txt_MultiTrackExisting.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Live = \"" + (chbx_LiveExisting.Checked ? "Yes" : "No") + "\", Live_Details = \"" + (chbx_LiveExisting.Checked ? txt_LiveDetailsExisting.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Acoustic = \"" + (chbx_AcousticExisting.Checked ? "Yes" : "No") + "\"," + " Album_Year = \"" + txt_YearExisting.Text + "\",";
            sel += " Is_Remastered = \"" + (chbx_RemasteredExisting.Checked ? "Yes" : "No");
            sel += "\", Is_Uncensored = \"" + (chbx_UncensoredExisting.Checked ? "Yes" : "No");
            sel += "\", Is_Soundtrack = \"" + (chbx_SoundtrackExisting.Checked ? "Yes" : "No");
            sel += "\", Is_FullAlbum = \"" + (chbx_FullAlbumExisting.Checked ? "Yes" : "No");
            sel += "\", Is_Instrumental = \"" + (chbx_InstrumentalExisting.Checked ? "Yes" : "No");
            sel += "\", Is_EP = \"" + (chbx_EPExisting.Checked ? "Yes" : "No");
            sel += "\", InTheWorks = \"" + (chbx_InTheWorksExisting.Checked ? "Yes" : "No");
            sel += "\", Is_Single = \"" + (chbx_SingleExisting.Checked ? "Yes" : "No");
            sel += "\", AlbumArtPath = \"" + picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds") + "\", AlbumArt_Hash = \"" + GetHash(picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds")) + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " YouTube_Link = \"" + txt_YouTube_LinkExisting.Text + "\", CustomsForge_Link = \"" + txt_CustomsForge_LinkExisting.Text + "\",";
            sel += " CustomsForge_Like = \"" + txt_CustomsForge_LikeExisting.Text + "\", CustomsForge_ReleaseNotes = \"" + txt_CustomsForge_ReleaseNotesExisting.Text + "\"";
            //sel += "\", AlbumArt_Hash = \"" + (rbtn_CoverNew.Checked ? art_hash : eXisting.AlbumArt_Hash);
            sel += " WHERE ID=" + lbl_IDExisting.Text;

            DataSet ddr = new DataSet(); ddr = UpdateDB("Main", sel + ";", cnb);
            if (reffy != 0)
                UpdateCompareBase();
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

            sel = "UPDATE Main SET Artist=\"" + ar + "\", Artist_Sort=\"" + txt_ArtistSortNew.Text + "\", Album=\"" + al + "\", Song_Title=\"" + txt_TitleNew.Text;
            sel += "\", Song_Title_Sort=\"" + txt_TitleSortNew.Text + "\", Author=\"" + (txt_AuthorNew.Text == "" ? (ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" ? "Custom Song Creator" : "") : txt_AuthorNew.Text);
            sel += "\", Version=\"" + (txt_VersionNew.Text == "" ? "1" : txt_VersionNew.Text) + "\", DLC_Name=\"" + txt_DLCIDNew.Text + "\",";
            sel += (txt_DescriptionNew.Text == "" ? "" : " Description = \"" + txt_DescriptionNew.Text + "\",");/// + (txt_DescriptionExistig.Text == "" ? "" : " Comments = \"" + txt_DescriptionExistig.Text + "\","); //"\"," +
            sel += " Is_Alternate = \"" + (chbx_IsAlternateNew.Checked ? "Yes" : "No") + "\", Alternate_Version_No = \"" + alt + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_MultiTrack = \"" + (chbx_MultiTrackNew.Checked ? "Yes" : "No") + "\", MultiTrack_Version = \"" + (chbx_MultiTrackNew.Checked ? txt_MultiTrackNew.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Live = \"" + (chbx_LiveNew.Checked ? "Yes" : "No") + "\", Live_Details = \"" + (chbx_LiveNew.Checked ? txt_LiveDetailsNew.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
            sel += " Is_Acoustic = \"" + (chbx_AcousticNew.Checked ? "Yes" : "No") + "\"," + " Album_Year = \"" + txt_YearNew.Text ;
            sel += "\", Is_Remastered = \"" + (chbx_RemasteredNew.Checked ? "Yes" : "No");
            sel += "\", Is_Uncensored = \"" + (chbx_UncensoredNew.Checked ? "Yes" : "No");
            sel += "\", Is_Soundtrack = \"" + (chbx_SoundtrackNew.Checked ? "Yes" : "No");
            sel += "\", Is_FullAlbum = \"" + (chbx_FullAlbumNew.Checked ? "Yes" : "No");
            sel += "\", Is_Instrumental = \"" + (chbx_InstrumentalNew.Checked ? "Yes" : "No");
            sel += "\", Is_EP = \"" + (chbx_EPNew.Checked ? "Yes" : "No");
            sel += "\", InTheWorks = \"" + (chbx_InTheWorksNew.Checked ? "Yes" : "No");
            sel += "\", Is_Single = \"" + (chbx_SingleNew.Checked ? "Yes" : "No");
            sel += "\", AlbumArtPath = \"" + picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds") + "\", AlbumArt_Hash = \"" + GetHash(picbx_AlbumArtPathNew.ImageLocation.Replace(".png", ".dds")) + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathNew.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
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

            //if (chbx_AcousticExisting.Checked == chbx_AcousticNew.Checked)
            //{
            //    btn_AuthorExisting.Enabled = false;
            //    btn_AuthorNew.Enabled = false;
            //    lbl_Author.ForeColor = lbl_diffCount.ForeColor;
            //}
            //else
            //{
            //    btn_AuthorExisting.Enabled = true;
            //    btn_AuthorNew.Enabled = true;
            //    lbl_Author.ForeColor = lbl_Reference.ForeColor;
            //}

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
            MainDB frm = new MainDB(cnb, false);//.Replace("\\AccessDB.accdb;", "")
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
            if (chbx_DescriptionSave.Checked) txt_DescriptionNew.Text = txt_DescriptionNew.Text + " " + txt_AlbumNew.Text;
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
            if (chbx_DescriptionSave.Checked) txt_DescriptionExisting.Text = txt_DescriptionExisting.Text + " " + txt_AlbumExisting.Text;
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

            if (eXisting.Audio_OrigHash == audio_hash || eXisting.Audio_OrigPreviewHash == audioPreview_hash || eXisting.AlbumArt_OrigHash == art_hash) btn_CommentSimilar_Click(null, null);

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
            txt_TitleNew.Text = txt_TitleNew.Text.Replace(OldDate.Trim(), "");
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(NewDate.Trim(), "");
            txt_TitleNew.Text = (txt_TitleNew.Text.Replace(NewDate.Trim(), "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + NewDate + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace(OldDate.Trim(), "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + OldDate + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            //txt_TitleNew.Text = (txt_TitleNew.Text.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "").Replace("]", "") + (txt_TitleNew.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + lbl_New.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            ////txt_TitleNew.Text = txt_TitleNew.Text.Replace((myNewDate >= myOldDate ? lbl_New.Text : lbl_Existing.Text), "");
            //txt_TitleExisting.Text = (txt_TitleExisting.Text.Replace((myNewDate >= myOldDate ? lbl_Existing.Text : lbl_New.Text), "").Replace("]", "") + (txt_TitleExisting.Text.IndexOf("[") > 0 && chbx_UseBrakets.Checked ? "" : " [") + lbl_Existing.Text + (chbx_UseBrakets.Checked ? "]" : "")).Replace("[ ", "[");// " (" + +")";
            if (txt_TitleExisting.Text == txt_TitleNew.Text) lbl_Title.ForeColor = lbl_Reference.ForeColor;
            else lbl_Title.ForeColor = lbl_Artist.ForeColor;
            txt_TitleNew.Text = txt_TitleNew.Text.Replace("  ", " ");
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace("  ", " ");
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

            if (chbx_Sort.Checked) btn_AlbumNew_Click(null, null);
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
            if (chbx_Sort.Checked) btn_AlbumExisting_Click(null, null);
        }

        private void Btn_Album2SortA_Click(object sender, EventArgs e)
        {
            syncAlbum();
        }

        private void Lbl_ArtistSort_Click(object sender, EventArgs e)
        {

        }

        private void btn_CommentSimilar_Click(object sender, EventArgs e)
        {
            var tst = "Same Hashed ";
            if (eXisting.Audio_OrigHash == audio_hash) tst += "Audio";
            if (eXisting.Audio_OrigPreviewHash == audioPreview_hash) tst += ", Preview ";
            if (eXisting.AlbumArt_OrigHash == art_hash) tst += ", Album Art ";
            tst += "file(s),";

            if (txt_AuthorNew.Text != txt_AuthorExisting.Text && (txt_AuthorNew.Text != "" || txt_AuthorNew.Text != null)
                && (txt_AuthorExisting.Text != "" || txt_AuthorExisting.Text != null))
            {
                txt_DescriptionNew.Text += tst + " similar to " + (txt_AuthorExisting.Text == "" || txt_AuthorExisting.Text is null ? txt_FileNameExisting.Text : txt_AuthorExisting.Text);
                txt_DescriptionExisting.Text += tst + " similar to " + (txt_AuthorNew.Text == "" || txt_AuthorNew.Text is null ? txt_FileNameNew.Text : txt_AuthorNew.Text);
            }
            else
            {
                txt_DescriptionNew.Text += tst + " similar to " + txt_FileNameExisting.Text;
                txt_DescriptionExisting.Text += tst + " similar to " + txt_FileNameNew.Text;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btn_OpenStandardization_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(c("dlcm_DBFolder"), c("dlcm_TempPath"), c("dlcm_RocksmithDLCPath"), c("dlcm_AdditionalManipul39").ToLower() == "yes" ? true : false, c("dlcm_AdditionalManipul40").ToLower() == "yes" ? true : false, cnb, txt_ArtistNew.Text);
            frm.Show();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
