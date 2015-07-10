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
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitGUI;
using System.IO;
using System.Security.Cryptography; //For File hash
using RocksmithToolkitLib.Extensions; //dds
using System.Globalization;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.Extensions; //most likely cue text

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Duplicates_Management : Form
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
        public bool IgnoreRest { get; set; }

        //public bool newold { get; set; }
        //public string clist { get; set; }
        //public DuplicatesManagement(string txt_DBFolder, Files filed, DLCPackageData datas, string author, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist)
        //{
        //    InitializeComponent();
        //    //MessageBox.Show("test0");
        //    DB_Path = txt_DBFolder;
        //    DB_Path = DB_Path + "\\Files.accdb";
        //    MessageBox.Show("test1");

        //}

        public Duplicates_Management(DLCManager.Files filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string vocal, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string DBPath, List<string> clist, List<string> dlist, bool newold, string Is_Original, string altvert, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, string FileSize, string unpackedDir,string Is_MultiTrack,string MultiTrack_Version)//string Is_MultiTracking, string Multitracking, 
       //file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original, altver
        {
            //Text = text;
            //MessageBox.Show("test2");
            this.filed = filed;
            this.datas = datas;
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
            //this.Is_MultiTracking = Is_MultiTracking;
            //this.MultiTracking = MultiTracking;
            this.txt_RocksmithDLCPath = txt_RocksmithDLCPath;
            //this.AllowEncript = AllowEncript;
            //this.AllowORIGDelete = AllowORIGDelete;
            this.FileSize = FileSize;
            this.unpackedDir = unpackedDir;
            this.Is_MultiTracks = Is_MultiTrack;
            this.MultiTrack_Versions = MultiTrack_Version;
            InitializeComponent();
            //MessageBox.Show(DB_Path);
            //DB_Path = text;
            //MessageBox.Show(DB_Path);
            //DB_Path = DB_Path;// + "\\Files.accdb";
        }
        public string GetAlternateNo()
        {
            var a = "";
            //Get the higgest Alternate Number
            try
            {
                using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    var sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + datas.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + datas.SongInfo.Album + "\") AND ";
                    sel += "(LCASE(Song_Title)=LCASE(\"" + datas.SongInfo.SongDisplayName + "\") OR ";
                    sel += "LCASE(Song_Title) like \"%" + datas.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
                    sel += "LCASE(Song_Title_Sort) =LCASE(\"" + datas.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + datas.Name + "\");";
                    //Get last inserted ID
                    //rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    DataSet dds = new DataSet();
                    OleDbDataAdapter dda = new OleDbDataAdapter(sel, con);
                    dda.Fill(dds, "Main");
                    dda.Dispose();

                    var altver = dds.Tables[0].Rows[0].ItemArray[0].ToString();
                    if (Is_Original == "No") a = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
                    //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                Console.WriteLine(ee.Message);
                //continue;
            }

            return a;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

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
        public bool AllowEncriptb = false;
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory; //when removing DDC
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

            //MessageBox.Show("test6");
            if (datas.SongInfo.Artist != filed.Artist) { lbl_Artist.ForeColor = lbl_Reference.ForeColor; btn_ArtistExisting.Enabled = true; btn_ArtistNew.Enabled = true; }
            else if (datas.SongInfo.Artist == "" && "" == filed.Artist) lbl_Artist.Text = "";

            txt_ArtistNew.Text = datas.SongInfo.Artist; //filed.
            txt_ArtistExisting.Text = filed.Artist; //

            if (datas.SongInfo.Album != filed.Album) { lbl_Album.ForeColor = lbl_Reference.ForeColor; btn_AlbumExisting.Enabled = true; btn_AlbumNew.Enabled = true; }
            else if (datas.SongInfo.Album == "" && "" == filed.Album) lbl_Album.Text = "";
            txt_AlbumNew.Text = datas.SongInfo.Album; //filed.    
            txt_AlbumExisting.Text = filed.Album; //

            lbl_IDExisting.Text = filed.ID;
            //lbl_IDNew.Text = filed.ID;

            //Alternate
            txt_AlternateNoNew.Value = altver.ToInt32();
            chbx_IsAlternateNew.Checked = filed.Is_Multitrack !="Yes"  ? true : false;;
            chbx_IsAlternateExisting.Checked = filed.Is_Alternate == "Yes" && filed.Is_Multitrack !="Yes" ? true : false;
            txt_AlternateNoExisting.Enabled = filed.Is_Alternate == "Yes" && filed.Is_Multitrack !="Yes"  ? true : false;
            txt_AlternateNoExisting.Value = (filed.Alternate_Version_No.ToInt32() == -1) ? 0 : filed.Alternate_Version_No.ToInt32();

            //Multitrack
            //txt_MultiTrackNew.Text = "";
            chbx_MultiTrackNew.Checked = Is_MultiTracks == "Yes" ? true : false;
            txt_MultiTrackNew.Enabled = Is_MultiTracks == "Yes" ? true : false;
            txt_MultiTrackNew.Text = (MultiTrack_Versions == "") ? "" : MultiTrack_Versions;

            chbx_MultiTrackExisting.Checked = filed.Is_Multitrack == "Yes" ? true : false;
            txt_MultiTrackExisting.Enabled = filed.Is_Multitrack == "Yes" ? true : false;
            txt_MultiTrackExisting.Text = (filed.MultiTrack_Version == "") ? "" : filed.MultiTrack_Version;

            Art_Hash = filed.AlbumArt_Hash;

            txt_YouTube_LinkExisting.Text=filed.YouTube_Link;
            txt_CustomsForge_LinkExisting.Text = filed.YouTube_Link;
            txt_CustomsForge_LikeExisting.Text = filed.YouTube_Link;
            txt_CustomsForge_ReleaseNotesExisting.Text = filed.YouTube_Link;

            // string altver = GetAlternateNo();

            //FileSize
            if (FileSize != filed.File_Size) { lbl_Size.ForeColor = lbl_Reference.ForeColor;}
            txt_SizeNew.Text = FileSize.ToInt32().ToString("###,###,###");
            txt_SizeExisting.Text = filed.File_Size.ToInt32().ToString("###,###,###");


            if (datas.SongInfo.SongDisplayName != filed.Song_Title) {lbl_Title.ForeColor = lbl_Reference.ForeColor; }//btn_TitleExisting.Enabled = true; btn_TitleNew.Enabled = true; }
            else if (datas.SongInfo.SongDisplayName == "" && "" == filed.Song_Title) lbl_Title.Text = "";
            txt_TitleNew.Text = datas.SongInfo.SongDisplayName;
            txt_TitleExisting.Text = filed.Song_Title;

            if (datas.SongInfo.SongDisplayNameSort != filed.Song_Title_Sort) { lbl_TitleSort.ForeColor = lbl_Reference.ForeColor; btn_TitleSortExisting.Enabled = true; btn_TitleSortNew.Enabled = true; }
            else if (datas.SongInfo.SongDisplayNameSort == "" && "" == filed.Song_Title_Sort) lbl_TitleSort.Text = "";
            txt_TitleSortNew.Text = datas.SongInfo.SongDisplayNameSort;
            txt_TitleSortExisting.Text = filed.Song_Title_Sort;

            if (datas.SongInfo.ArtistSort != filed.Artist_Sort) {lbl_ArtistSort.ForeColor = lbl_Reference.ForeColor; btn_ArtistSortExisting.Enabled = true; btn_ArtistSortNew.Enabled = true; }
            else if (datas.SongInfo.ArtistSort == "" && "" == filed.Artist_Sort) lbl_ArtistSort.Text = "";
            txt_ArtistSortNew.Text = datas.SongInfo.ArtistSort;
            txt_ArtistSortExisting.Text = filed.Artist_Sort;

            if (original_FileName != filed.Original_FileName) lbl_FileName.ForeColor = lbl_Reference.ForeColor;
            else if (original_FileName == "" && "" == filed.Original_FileName) lbl_FileName.Text = "";
            txt_FileNameNew.Text = original_FileName;
            txt_FileNameExisting.Text = filed.Original_FileName;

            if (author != filed.Author) {lbl_Author.ForeColor = lbl_FileName.ForeColor; btn_AuthorExisting.Enabled = true; btn_AuthorNew.Enabled = true; }
            else if (author == "" && "" == filed.Author) lbl_Author.Text = "";
            txt_AuthorNew.Text = author;// (author == "" ? "missing" : author);
            txt_AuthorExisting.Text = filed.Author;//(filed.Author == "" ? "missing" : filed.Author);            

            if (tunnings != filed.Tunning) lbl_Tuning.ForeColor = lbl_Reference.ForeColor;
            else if (tunnings == "" && "" == filed.Tunning) lbl_Tuning.Text = "";
            txt_TuningNew.Text = tunnings;
            txt_TuningExisting.Text = filed.Tunning;

            if (datas.PackageVersion != filed.Version) lbl_Version.ForeColor = lbl_Reference.ForeColor;
            else if (datas.PackageVersion == "" && "" == filed.Version) lbl_Version.Text = "";
            txt_VersionNew.Text = datas.PackageVersion;
            txt_VersionExisting.Text = filed.Version;

            if (dD != filed.Has_DD) lbl_DD.ForeColor = lbl_Reference.ForeColor;
            else if (dD == "" && "" == filed.Has_DD) lbl_DD.Text = "";
            txt_DDNew.Text = dD;
            txt_DDExisting.Text = filed.Has_DD;

            if (datas.Name != filed.DLC_Name) lbl_DLCID.ForeColor = lbl_Reference.ForeColor;
            else if (datas.Name == "" && "" == filed.DLC_Name) lbl_DLCID.Text = "";
            txt_DLCIDNew.Text = datas.Name;
            txt_DLCIDExisting.Text = filed.DLC_Name;

            if (Is_Original != filed.Is_Original) lbl_IsOriginal.ForeColor = lbl_Reference.ForeColor;
            else if (Is_Original == "" && "" == filed.Is_Original) lbl_IsOriginal.Text = "";
            txt_IsOriginalNew.Text = Is_Original;
            txt_IsOriginalExisting.Text = filed.Is_Original;
            if (filed.Is_Original == "Yes" || Is_Original == "Yes") lbl_Is_Original.ForeColor = lbl_Reference.ForeColor;

            if (tkversion != filed.ToolkitVersion) lbl_Toolkit.ForeColor = lbl_Reference.ForeColor;
            else if (tkversion == "" && "" == filed.ToolkitVersion) lbl_Toolkit.Text = "";
            txt_ToolkitNew.Text = tkversion;
            txt_ToolkitExisting.Text = filed.ToolkitVersion;

            if (filed.AlbumArt_Hash != art_hash) {lbl_AlbumArt.ForeColor = lbl_Reference.ForeColor;btn_CoverNew.Enabled = true; btn_CoverExisting.Enabled = true; }
            else if (filed.AlbumArt_Hash == "" && "" == art_hash) lbl_AlbumArt.Text = "";

            datas.AlbumArtPath = datas.AlbumArtPath.Replace("/", "\\");
            picbx_AlbumArtPathNew.ImageLocation = datas.AlbumArtPath.Replace(".dds", ".png");
            //txt_Description.Text= datas.AlbumArtPath.Replace(".dds", ".png");
            picbx_AlbumArtPathExisting.ImageLocation = filed.AlbumArtPath.Replace(".dds", ".png");

            if (filed.Audio_Hash != audio_hash) lbl_Audio.ForeColor = lbl_Reference.ForeColor;
            else if (filed.Audio_Hash == "" && "" == audio_hash) lbl_Audio.Text = "";
            txt_AudioNew.Text = (audio_hash.ToString() == "" ? "" : "Yes");
            txt_AudioExisting.Text = (filed.Audio_Hash.ToString() == "" ? "" : "Yes");

            if (filed.AudioPreview_Hash != audioPreview_hash) lbl_Preview.ForeColor = lbl_Reference.ForeColor;
            else if (filed.AudioPreview_Hash == "" && "" == audioPreview_hash) lbl_Vocals.Text = "";
            txt_PreviewNew.Text = (audioPreview_hash.ToString() == "" ? "No" : "Yes");
            txt_PreviewExisting.Text = (filed.AudioPreview_Hash.ToString() == "" ? "No" : "Yes");
            if (audioPreview_hash.ToString() != "" ) btn_PlayPreviewNew.Enabled=true;
            if (filed.AudioPreview_Hash.ToString() != "" ) btn_PlayPreviewExisting.Enabled=true;

            if (filed.Has_Vocals.ToString() != vocal) lbl_Vocals.ForeColor = lbl_Reference.ForeColor;
            //else if (filed.Has_Vocals == "" && "" == vocal) lbl_Vocals.Text = "";
            txt_VocalsNew.Text = (vocal == "Yes" ? "Yes" : "No");
            txt_VocalsExisting.Text = (filed.Has_Vocals.ToString() == "Yes" ? "Yes" : "No");

            if (((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : "") + ((vocal == "Yes") ? "V" : "") != ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : "") + ((filed.Has_Vocals == "Yes") ? "V" : "")) lbl_AvailableTracks.ForeColor = lbl_Reference.ForeColor;
            txt_AvailTracksNew.Text = ((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : "") + ((vocal == "Yes") ? "V" : "");
            txt_AvailTracksExisting.Text = ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : "") + ((filed.Has_Vocals == "Yes") ? "V" : "");

            //Show the alternate/duplicates in the DB
            lbl_diffCount.Text = (i + 1).ToString() + "/" + norows.ToString();
            //lbl_diffCount.Visible = true;
            if (norows > 1) chbx_IgnoreDupli.Enabled = true;

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
            //text += ((dD == filed.Has_DD) ? "" : ("\n10/14+ DD: " + dD + " -> " + filed.Has_DD));
            ////text += "\nOriginal Is Alternate: " + filed.Is_Alternate + (filed.Is_Alternate == "Yes" ? " v. " + filed.Alternate_Version_No : "");
            //text += "\n11/14+ Avail. Instr./Tracks: " + ((bass == "Yes") ? "B" : "") + ((rhythm == "Yes") ? "R" : "") + ((lead == "Yes") ? "L" : "") + ((combo == "Yes") ? "C" : ""); //((Guitar == "Yes") ? "G" : "") + 
            //text += " -> " + ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : ""); //+ ((filed.Has_Guitar == "Yes") ? "G" : "")
            //text += ((filed.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + filed.art_Hash
            //text += ((filed.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + filed.audio_Hash 
            //text += ((filed.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + filed.audioPreview_Hash

            //files hash
            DataSet ds = new DataSet();
            i = 0;
            DB_Path = DB_Path + "\\Files.accdb;";
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
            string lastConverjsonDateTime_exist = "";
            string txtnew = "";
            string txtold = "";
            //MessageBox.Show(DB_Path);
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    var cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + filed.ID.ToString() + ";";//\"\"
                    OleDbDataAdapter daa = new OleDbDataAdapter(cmd, cnn);
                    //as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                    //MessageBox.Show("0");
                    daa.Fill(ds, "Arrangements");
                    daa.Dispose();
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
                        DateTime myOldDate;
                        DateTime myNewDate;
                        DateTime myCurDate;
                        DateTime myExisDate;
                        foreach (var arg in datas.Arrangements)
                        {
                            //diff = true; diffjson = true;
                            lastConversionDateTime_cur = "";
                            lastConversionDateTime_exist = "";
                            for (i = 0; i <= noOfRec - 1; i++)
                            {
                                //MessageBox.Show(noOfRec.ToString());
                                //rtxt_StatisticsOnReadDLCs.Text = alist[i]+"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                XmlHash = ds.Tables[0].Rows[i].ItemArray[6].ToString(); // XmlHash                                  
                                XmlName = ds.Tables[0].Rows[i].ItemArray[17].ToString() + ds.Tables[0].Rows[i].ItemArray[25].ToString(); //type+routemask+
                                XmlUUID = ds.Tables[0].Rows[i].ItemArray[28].ToString(); //xml.uuid
                                XmlFile = ds.Tables[0].Rows[i].ItemArray[5].ToString(); //xml.filepath
                                jsonFile = ds.Tables[0].Rows[i].ItemArray[4].ToString(); //json.filepath
                                jsonHash = ds.Tables[0].Rows[i].ItemArray[38].ToString(); // XmlHash      
                                var xx = Directory.GetFiles(arg.SongXml.File.Replace("\\songs\\arr\\" + arg.SongXml.Name + ".xml", ""), "*.json", SearchOption.AllDirectories)[0];
                                if (1 == 2) //37. Keep the Uncompressed Songs superorganized   chbx_Additional_Manipulations.GetItemChecked(36)                             
                                    arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                                else
                                    arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + DLCManager.calc_path(xx));
                                //arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json")).Replace("EOF", "Toolkit");

                                //rtxt_StatisticsOnReadDLCs.Text = "-"+XmlName + "=" + (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongXml.File + "=" + XmlFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongFile.File + "==" + jsonFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (XmlName == (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) || (XmlUUID == arg.SongXml.UUID.ToString()))
                                // rtxt_StatisticsOnReadDLCs.Text = "-" + XmlHash + "=" + alist[k] + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                {
                                    //if (XmlHash == alist[k])
                                    ////diff = false;
                                    //{

                                    //}
                                    //else
                                    //{
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
                                    //txt_Description.Text = "tst";
                                    if (arg.RouteMask.ToString() == "Bass")
                                    {
                                        if ((lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)) lbl_XMLBass.Text = "";
                                        else if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) lbl_XMLBass.ForeColor = lbl_Reference.ForeColor;
                                        txt_XMLBassNew.Text = lastConversionDateTime_cur;
                                        txt_XMLBassExisting.Text = lastConversionDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Lead")
                                    {
                                        if ((lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)) lbl_XMLLead.Text = "";
                                        else if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) lbl_XMLLead.ForeColor = lbl_Reference.ForeColor;
                                        txt_XMLLeadNew.Text = lastConversionDateTime_cur;
                                        txt_XMLLeadExisting.Text = lastConversionDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Combo")
                                    {
                                        if ((lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)) lbl_XMLCombo.Text = "";
                                        else if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) lbl_XMLCombo.ForeColor = lbl_Reference.ForeColor;
                                        txt_XMLComboNew.Text = lastConversionDateTime_cur;
                                        txt_XMLComboExisting.Text = lastConversionDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Rhythm")
                                    {
                                        if ((lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)) lbl_XMLRhythm.Text = "";
                                        else if (lastConversionDateTime_cur != lastConversionDateTime_exist || XmlHash != alist[k]) lbl_XMLRhythm.ForeColor = lbl_Reference.ForeColor;
                                        txt_XMLRhythmNew.Text = lastConversionDateTime_cur;
                                        txt_XMLRhythmExisting.Text = lastConversionDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Vocal" || XmlName=="VocalNone")
                                    {
                                        //if (lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist)
                                        //lbl_XMLVocal.Text = "";
                                        //else if (lastConversionDateTime_cur != lastConversionDateTime_exist) { lbl_XMLVocal.ForeColor = lbl_Reference.ForeColor; lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }                                            
                                        //txt_XMLVocalNew.Text = lastConversionDateTime_cur;
                                        //txt_XMLVocalExisting.Text = lastConversionDateTime_exist;lbl_XMLVocal.ForeColor = lbl_Reference.ForeColor;

                                        if (XmlHash != alist[k]) {  lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }
                                        else if (XmlHash == "" && "" == alist[k]) lbl_Vocals.Text = "";
                                        //var r = blist[k];
                                    }

                                    //Get the oldest timestamp
                                    CultureInfo enUS = new CultureInfo("en-US");
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
                                    //}
                                    //if (jsonHash != blist[k])
                                    ////diffjson = false;
                                    ////else
                                    //{
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

                                    if (arg.RouteMask.ToString() == "Bass")
                                    {
                                        if ((lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist)) lbl_JSONBass.Text = "";
                                        else if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) lbl_JSONBass.ForeColor = lbl_Reference.ForeColor;
                                        txt_JSONBassNew.Text = lastConverjsonDateTime_cur;
                                        txt_JSONBassExisting.Text = lastConverjsonDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Lead")
                                    {
                                        if ((lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist)) lbl_JSONLead.Text = "";
                                        else if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) lbl_JSONLead.ForeColor = lbl_Reference.ForeColor;
                                        txt_JSONLeadNew.Text = lastConverjsonDateTime_cur;
                                        txt_JSONLeadExisting.Text = lastConverjsonDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Combo")
                                    {
                                        if ((lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist)) lbl_JSONCombo.Text = "";
                                        else if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) lbl_JSONCombo.ForeColor = lbl_Reference.ForeColor;
                                        txt_JSONComboNew.Text = lastConverjsonDateTime_cur;
                                        txt_JSONComboExisting.Text = lastConverjsonDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Rhythm")
                                    {
                                        if ((lastConverjsonDateTime_cur == "" && "" == lastConverjsonDateTime_exist)) lbl_JSONRhythm.Text = "";
                                        else if (lastConverjsonDateTime_cur != lastConverjsonDateTime_exist || jsonHash != blist[k]) lbl_JSONRhythm.ForeColor = lbl_Reference.ForeColor;
                                        txt_JSONRhythmNew.Text = lastConverjsonDateTime_cur;
                                        txt_JSONRhythmExisting.Text = lastConverjsonDateTime_exist;
                                    }
                                    if (arg.RouteMask.ToString() == "Vocal")
                                    {
                                        //if (lastConversionDateTime_cur == "" && "" == lastConversionDateTime_exist) lbl_JSONVocal.Text = "";
                                        //else if (lastConversionDateTime_cur != lastConversionDateTime_exist) { lbl_JSONVocal.ForeColor = lbl_Reference.ForeColor; lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }
                                        //txt_JSONVocalNew.Text = lastConverjsonDateTime_cur;
                                        //txt_JSONVocalExisting.Text = lastConverjsonDateTime_exist;lbl_JSONVocal.ForeColor = lbl_Reference.ForeColor; 
                                        if (jsonHash != blist[k]) { lbl_Vocals.ForeColor = lbl_Reference.ForeColor; }
                                        else if (jsonHash == "" && "" == blist[k]) lbl_Vocals.Text = "";
                                    }
                                    //}
                                }
                                else
                                {
                                }
                            }

                            //txt_Description.Text += dateold + datenew;
                            myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                            myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                            if (dateold != "12-13-11 13:11" && datenew != "12-13-11 13:11")
                            {
                                //txt_Comment.Text += newold.ToString() + filed.Is_Original + Is_Original + Convert.ToDateTime(dateold) + Convert.ToDateTime(datenew);
                                if (newold && filed.Is_Original != "Yes" && Is_Original != "Yes" && myOldDate > myNewDate)
                                {
                                    txtnew = " " + "(older)";
                                    txtold = " " + "(newer)";
                                    ExistChng = true;
                                }
                                else if (newold && filed.Is_Original != "Yes" && Is_Original != "Yes" && myOldDate < myNewDate)
                                {
                                    txtnew = " " + "(newer)";
                                    txtold = " " + "(older)";
                                    ExistChng = true;
                                }
                                txt_TitleNew.Text = datas.SongInfo.SongDisplayName + txtnew + "";
                                txt_TitleExisting.Text = filed.Song_Title + txtold + "";
                            }
                            //text += ((diff) ? "\n" + (14 + i) + "/14+Diff XML" + arg.ArrangementType + arg.RouteMask + ": " + lastConversionDateTime_cur + "->" + lastConversionDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                            //text += ((diffjson) ? "\n" + (15 + i) + "/14+Diff Json" + arg.ArrangementType + arg.RouteMask + ": " + lastConverjsonDateTime_cur + "->" + lastConverjsonDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                            k++;
                        }

                    }
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                //rtxt_StatisticsOnReadDLCs.Text = "Error at last conversion" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                Console.WriteLine(ee.Message);
            }

            //files size//files dates
            //DialogResult result1 = MessageBox.Show(text + "\n\nChose:\n\n1. Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //if (result1 == DialogResult.Yes) return "Update";
            //else if (result1 == DialogResult.No) return "Alternate";
            //else return "ignore";//if (result1 == DialogResult.Cancel) 
            ExistChng = false;
            lbl_New.Text = txtnew;
            lbl_Existing.Text = txtold;
        }

        public class Files
        {
            public string ID { get; set; }
            public string Song_Title { get; set; }
            public string Song_Title_Sort { get; set; }
            public string Album { get; set; }
            public string Artist { get; set; }
            public string Artist_Sort { get; set; }
            public string Album_Year { get; set; }
            public string AverageTempo { get; set; }
            public string Volume { get; set; }
            public string Preview_Volume { get; set; }
            public string AlbumArtPath { get; set; }
            public string AudioPath { get; set; }
            public string audioPreviewPath { get; set; }
            public string Track_No { get; set; }
            public string Author { get; set; }
            public string Version { get; set; }
            public string DLC_Name { get; set; }
            public string DLC_AppID { get; set; }
            public string Current_FileName { get; set; }
            public string Original_FileName { get; set; }
            public string Import_Path { get; set; }
            public string Import_Date { get; set; }
            public string Folder_Name { get; set; }
            public string File_Size { get; set; }
            public string File_Hash { get; set; }
            public string Original_File_Hash { get; set; }
            public string Is_Original { get; set; }
            public string Is_OLD { get; set; }
            public string Is_Beta { get; set; }
            public string Is_Alternate { get; set; }
            public string Is_Multitrack { get; set; }
            public string Is_Broken { get; set; }
            public string MultiTrack_Version { get; set; }
            public string Alternate_Version_No { get; set; }
            public string DLC { get; set; }
            public string Has_Bass { get; set; }
            public string Has_Guitar { get; set; }
            public string Has_Lead { get; set; }
            public string Has_Rhythm { get; set; }
            public string Has_Combo { get; set; }
            public string Has_Vocals { get; set; }
            public string Has_Sections { get; set; }
            public string Has_Cover { get; set; }
            public string Has_Preview { get; set; }
            public string Has_Custom_Tone { get; set; }
            public string Has_DD { get; set; }
            public string Has_Version { get; set; }
            public string Tunning { get; set; }
            public string Bass_Picking { get; set; }
            public string Tones { get; set; }
            public string Group { get; set; }
            public string Rating { get; set; }
            public string Description { get; set; }
            public string Comments { get; set; }
            public string Has_Track_No { get; set; }
            public string Platform { get; set; }
            public string PreviewTime { get; set; }
            public string PreviewLenght { get; set; }
            public string Youtube_Playthrough { get; set; }
            public string CustomForge_Followers { get; set; }
            public string CustomForge_Version { get; set; }
            public string Show_Available_Instruments { get; set; }
            public string Show_Alternate_Version { get; set; }
            public string Show_MultiTrack_Details { get; set; }
            public string Show_Group { get; set; }
            public string Show_Beta { get; set; }
            public string Show_Broken { get; set; }
            public string Show_DD { get; set; }
            public string Original { get; set; }
            public string Selected { get; set; }
            public string YouTube_Link { get; set; }
            public string CustomsForge_Link { get; set; }
            public string CustomsForge_Like { get; set; }
            public string CustomsForge_ReleaseNotes { get; set; }
            public string SignatureType { get; set; }
            public string ToolkitVersion { get; set; }
            public string Has_Author { get; set; }
            public string OggPath { get; set; }
            public string oggPreviewPath { get; set; }
            public string UniqueDLCName { get; set; }
            public string AlbumArt_Hash { get; set; }
            public string Audio_Hash { get; set; }
            public string audioPreview_Hash { get; set; }
            public string Bass_Has_DD { get; set; }
            public string Has_Bonus_Arrangement { get; set; }
            public string Artist_ShortName { get; set; }
            public string Album_ShortName { get; set; }
            public string Available_Duplicate { get; set; }
            public string Available_Old { get; set; }
        }
        public Files[] files = new Files[10000];
        private DLCManager.Files filed;
        private DLCPackageData datas;
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
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            try
            {
                //MessageBox.Show(DB_Path);
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                    dax.Fill(dus, "Main");
                    dax.Dispose();
                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
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
                        files[i].Group = dataRow.ItemArray[50].ToString();
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
                        files[i].Show_Available_Instruments = dataRow.ItemArray[61].ToString();
                        files[i].Show_Alternate_Version = dataRow.ItemArray[62].ToString();
                        files[i].Show_MultiTrack_Details = dataRow.ItemArray[63].ToString();
                        files[i].Show_Group = dataRow.ItemArray[64].ToString();
                        files[i].Show_Beta = dataRow.ItemArray[65].ToString();
                        files[i].Show_Broken = dataRow.ItemArray[66].ToString();
                        files[i].Show_DD = dataRow.ItemArray[67].ToString();
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
                        files[i].audioPreview_Hash = dataRow.ItemArray[82].ToString();
                        files[i].Bass_Has_DD = dataRow.ItemArray[83].ToString();
                        files[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
                        files[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
                        files[i].Album_ShortName = dataRow.ItemArray[86].ToString();
                        files[i].Available_Duplicate = dataRow.ItemArray[87].ToString();
                        files[i].Available_Old = dataRow.ItemArray[88].ToString();
                        i++;
                    }
                    //Closing Connection
                    dax.Dispose();
                    cnn.Close();
                    //rtxt_StatisticsOnReadDLCs.Text += i;
                    //var ex = 0;
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can not open Main DB connection ! ");
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

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
            Asses = "Alternate";
            if (ExistChng)
            {
                DialogResult result1 = MessageBox.Show("Save the Existing Edits?\nYes for save \nNo for Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) UpdateExisting();
            }
            exit();
            this.Hide();
            //this.ParentForm.
        }

        private void btn_Ignore_Click(object sender, EventArgs e)
        {
            Asses = "Ignore";
            if (ExistChng)
            {
                DialogResult result1 = MessageBox.Show("Save the Existing Edits?\nYes for save \nNo for Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) UpdateExisting();
            }
            exit();
            this.Hide();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Asses = "Update";
            exit();
            this.Hide();
        }
        public void exit()
        {
            Author = (txt_AuthorNew.Text == "" ? "Custom Song Creator" : txt_AuthorNew.Text);
            Version = (txt_VersionNew.Text == "" ? "1" : txt_VersionNew.Text);
            DLCID = txt_DLCIDNew.Text;
            Title = txt_TitleNew.Text;
            Comment = txt_Comment.Text;
            Description = txt_Description.Text;
            Is_Alternate = (chbx_IsAlternateNew.Checked ? (txt_IsOriginalNew.Text == "Yes" ? "Yes" : "No") : "No");
            Title_Sort = txt_TitleSortNew.Text;
            Album = txt_AlbumNew.Text;
            //Is_Original= (chbx_IsOriginal.Checked ? "Yes" : "No");
            Alternate_No = chbx_IsAlternateNew.Checked ? txt_AlternateNoNew.Text : null;
            AlbumArtPath = txt_TitleNew.Text!="" ? picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds") : "";//rbtn_CoverExisting.Checked ? picbx_AlbumArtPathExisting.ImageLocation.Replace("png", "dds") : picbx_AlbumArtPathNew.ImageLocation.Replace("png", "dds");
            Art_Hash = txt_TitleNew.Text!="" ? calc_arthash(picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds")) : "";//.Checked ? filed.AlbumArt_Hash : art_hash;
            ArtistSort = txt_ArtistSortNew.Text;
            Artist = txt_ArtistNew.Text;
            MultiT = chbx_MultiTrackNew.Checked ? "Yes": "No";
            MultiTV = txt_MultiTrackNew.Text;
            AlbumAP = AlbumArtPath;
            YouTube_Link = txt_YouTube_LinkExisting.Text;
            CustomsForge_Link = txt_CustomsForge_LinkExisting.Text;
            CustomsForge_Like = txt_CustomsForge_LikeExisting.Text;
            CustomsForge_ReleaseNotes = txt_CustomsForge_ReleaseNotesExisting.Text;
            ExistingTrackNo= filed.Track_No;
            IgnoreRest = chbx_IgnoreDupli.Checked;
            
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
            //if (filed.Album != datas.SongInfo.Album) al = datas.SongInfo.Album;
            //if (filed.Artist != datas.SongInfo.Artist) ar = datas.SongInfo.Artist;
            var alt= chbx_IsAlternateExisting.Checked ? txt_AlternateNoExisting.Value.ToString() : null;

            try
            {
                //MessageBox.Show(DB_Path);
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    sel = "UPDATE Main SET Artist=\"" + ar + "\", Artist_Sort=\"" + txt_ArtistSortExisting.Text + "\", Album=\"" + al + "\", Song_Title=\"" + txt_TitleExisting.Text;
                    sel += "\", Song_Title_Sort=\"" + txt_TitleSortExisting.Text + "\", Author=\"" + (txt_AuthorExisting.Text == "" ? "Custom Song Creator" : txt_AuthorExisting.Text);
                    sel += "\", Version=\"" + (txt_VersionExisting.Text == "" ? "1" : txt_VersionExisting.Text) + "\", DLC_Name=\"" + txt_DLCIDExisting.Text;
                    sel += "\"," + (txt_Description.Text == "" ? "" : " Description = \"" + txt_Description.Text + "\",") + (txt_Comment.Text == "" ? "" : ", Comments = \"" + txt_Comment.Text + "\",") ;
                    sel += " Is_Alternate = \"" + (chbx_IsAlternateExisting.Checked ? "Yes" : "No")+ "\", Alternate_Version_No = \"" + alt+"\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
                    sel += " Is_MultiTrack = \"" + (chbx_MultiTrackExisting.Checked ? "Yes" : "No") + "\", MultiTrack_Version = \"" + (chbx_MultiTrackExisting.Checked ? txt_MultiTrackExisting.Text : "") + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
                    sel += " AlbumArtPath = \"" + picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds") + "\", AlbumArt_Hash = \"" + calc_arthash(picbx_AlbumArtPathExisting.ImageLocation.Replace(".png", ".dds")) + "\",";// + "\"", AlbumArtPath = \"" + (rbtn_CoverNew.Checked ? picbx_AlbumArtPathNew.ImageLocation : picbx_AlbumArtPathExisting.ImageLocation);// + "\", Is_Original = \"" + (chbx_IsOriginal.Checked ? "Yes" : "No");
                    sel += " YouTube_Link = \"" + txt_YouTube_LinkExisting.Text + "\", CustomsForge_Link = \"" + txt_CustomsForge_LinkExisting.Text + "\",";
                    sel += " CustomsForge_Like = \"" + txt_CustomsForge_LikeExisting.Text + "\", CustomsForge_ReleaseNotes = \"" + txt_CustomsForge_ReleaseNotesExisting.Text + "\"";
                    //sel += "\", AlbumArt_Hash = \"" + (rbtn_CoverNew.Checked ? art_hash : filed.AlbumArt_Hash);
                    sel += " WHERE ID=" + lbl_IDExisting.Text;
                    //txt_Description.Text = sel;
                    DataSet ddr = new DataSet();
                    OleDbDataAdapter dat = new OleDbDataAdapter(sel, cnn);

                    dat.Fill(ddr, "Main");
                    dat.Dispose();
                    //MessageBox.Show(DB_Path);
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can not open Main DB connection ! " + sel);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Existing Record updated");

        }

        public static string calc_arthash(string ss)
        {
            using (FileStream fs = File.OpenRead(ss))
            {
                SHA1 sha = new SHA1Managed();
                var art_hash = BitConverter.ToString(sha.ComputeHash(fs));//MessageBox.Show(FileHash+"-"+ss);
                //convert to png
                //ExternalApps.Dds2Png(ss);
                fs.Close();
                return art_hash.ToString();
            }

        }

        public void ExistingChanged(object sender, EventArgs e)
        {
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
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb;", ""), TempPath, false, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb);
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
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (newer)", "").Replace(" (older)", "").Replace(" " + txt_AvailTracksExisting.Text, "").Replace(" " + txt_VersionExisting.Text, "").Replace(" " + txt_AuthorExisting.Text, "").Replace(" noDD", "").Replace(" DD", "").Replace(" "+txt_TuningExisting.Text, "");

            txt_TitleNew.Text = datas.SongInfo.SongDisplayName.Replace(" (newer)", "").Replace(" (older)", "").Replace(" " + txt_AvailTracksNew.Text, "").Replace(" " + txt_VersionNew.Text, "").Replace(" " + txt_AuthorNew.Text, "").Replace(" noDD", "").Replace(" DD", "").Replace(" " + txt_TuningNew.Text, ""); ;
            //txt_TitleExisting.Text = filed.Song_Title;
        }

        private void txt_JSONVocalNew_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_JSONVocalExisting_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_XMLVocalExisting_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_XMLVocalNew_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_TitleNew_Click(object sender, EventArgs e)
        {
            if (txt_TitleNew.Text.IndexOf("(newer)") > 0) txt_TitleNew.Text = txt_TitleExisting.Text.Replace("(newer)", "(older)");
            else if (txt_TitleNew.Text.IndexOf("(older)") > 0) txt_TitleNew.Text = txt_TitleExisting.Text.Replace("(older)", "(newer)");
            else txt_TitleNew.Text = txt_TitleExisting.Text;
            lbl_Title.ForeColor = lbl_diffCount.ForeColor;
            //btn_TitleExisting.Enabled = false;
            //btn_TitleNew.Enabled = false;
        }

        private void btn_TitleExisting_Click(object sender, EventArgs e)
        {
            if (txt_TitleExisting.Text.IndexOf("(newer)") > 0) txt_TitleExisting.Text = txt_TitleNew.Text.Replace("(newer)", "(older)");
            else if (txt_TitleExisting.Text.IndexOf("(older)") > 0) txt_TitleExisting.Text = txt_TitleNew.Text.Replace("(older)", "(newer)");
            else txt_TitleExisting.Text = txt_TitleNew.Text;
            lbl_Title.ForeColor = lbl_diffCount.ForeColor;
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
            lbl_Album.ForeColor = lbl_diffCount.ForeColor;
            btn_AlbumExisting.Enabled = false;
            btn_AlbumNew.Enabled = false;
        }

        private void btn_AlbumExisting_Click(object sender, EventArgs e)
        {
            txt_AlbumExisting.Text = txt_AlbumNew.Text;
            lbl_Album.ForeColor = lbl_diffCount.ForeColor;
            btn_AlbumExisting.Enabled = false;
            btn_AlbumNew.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txt_ArtistNew.Text = txt_ArtistExisting.Text;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            btn_ArtistExisting.Enabled = false;
            btn_ArtistNew.Enabled = false;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
        }

        private void btn_ArtistExisting_Click(object sender, EventArgs e)
        {
            txt_ArtistExisting.Text = txt_ArtistNew.Text;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
            btn_ArtistExisting.Enabled = false;
            btn_ArtistNew.Enabled = false;
            lbl_Artist.ForeColor = lbl_diffCount.ForeColor;
        }

        private void btn_AddVersion_Click(object sender, EventArgs e)
        {
            if (txt_TitleNew.Text.IndexOf(" (older)") > 0 || txt_TitleNew.Text.IndexOf(" (newer)") > 0)
            {
                txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (older)", " v." + txt_VersionNew.Text);
                txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (newer)", " v." + txt_VersionNew.Text);
            }
            else txt_TitleNew.Text = txt_TitleNew.Text+ " v." + txt_VersionNew.Text;

            if (txt_TitleExisting.Text.IndexOf(" (older)") > 0 || txt_TitleExisting.Text.IndexOf(" (newer)") > 0)
            {
                txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (older)", " v." + txt_VersionExisting.Text);
                txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (newer)", " v." + txt_VersionExisting.Text);
            }
            else txt_TitleExisting.Text = txt_TitleExisting.Text+" v." + txt_VersionExisting.Text;
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
            txt_TitleNew.Text = txt_TitleNew.Text.Replace(" "+txt_AvailTracksNew.Text, "") + " " + txt_AvailTracksNew.Text;
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" "+txt_AvailTracksExisting.Text, "") + " " + txt_AvailTracksExisting.Text;
        }

        private void btn_CoverNew_Click(object sender, EventArgs e)
        {
            //datas.AlbumArtPath = datas.AlbumArtPath.Replace("/", "\\");
            picbx_AlbumArtPathNew.ImageLocation = picbx_AlbumArtPathExisting.ImageLocation;//datas.AlbumArtPath.Replace(".dds", ".png");
            btn_CoverNew.Enabled = false;
            btn_CoverExisting.Enabled = false;
            lbl_AlbumArt.ForeColor = lbl_diffCount.ForeColor;
            //txt_Description.Text= datas.AlbumArtPath.Replace(".dds", ".png");
            
        }

        private void btn_CoverExisting_Click(object sender, EventArgs e)
        {
            picbx_AlbumArtPathExisting.ImageLocation = picbx_AlbumArtPathNew.ImageLocation;//filed.AlbumArtPath.Replace(".dds", ".png");
            btn_CoverNew.Enabled = false;
            btn_CoverExisting.Enabled = false;
            lbl_AlbumArt.ForeColor = lbl_diffCount.ForeColor;

            dupliID = lbl_IDExisting.Text;
        }

        private void txt_MultiTrackNew_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_AddAuthor_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text += " " + txt_AuthorNew.Text;
            txt_TitleExisting.Text += " " + txt_AuthorExisting.Text;
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = (txt_TitleNew.Text).Replace(" DD", "").Replace(" noDD", "") + " " + (txt_DDNew.Text == "Yes" ? "DD" : "noDD");
            txt_TitleExisting.Text = (txt_TitleExisting.Text).Replace(" DD", "").Replace(" noDD", "") + " " + (txt_DDExisting.Text == "Yes" ? "DD" : "noDD");
        }

        private void btn_AddTunning_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = txt_TitleNew.Text.Replace(" "+txt_TuningNew.Text, "") + " " + txt_TuningNew.Text;
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" " + txt_TuningExisting.Text, "") + " " + txt_TuningExisting.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt_TitleNew.Text = txt_TitleNew.Text.Replace(" (older)", "").Replace(" (newer)", "") + lbl_New.Text;// " (" + +")";
            txt_TitleExisting.Text = txt_TitleExisting.Text.Replace(" (older)", "").Replace(" (newer)", "") + lbl_Existing.Text;// " (" + +")";
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string t = filed.Folder_Name;
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

        private void btn_GoToExisting_Click(object sender, EventArgs e)
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

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = filed.oggPreviewPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
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
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = filed.OggPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
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
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            //var t = filed.OggPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var t = datas.OggPath.Replace(".wem", "_fixed.ogg"); ;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
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
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = datas.OggPath.Replace(".wem", "_fixed.ogg"); ;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var tt = t.Replace(".ogg", "_preview.ogg");
            //var t = filed.oggPreviewPath;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
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
    }
}
