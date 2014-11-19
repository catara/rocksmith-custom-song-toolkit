using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using System.Diagnostics;
using RocksmithToolkitLib.DLCPackage;
using System.IO;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Duplicates : Form
    {
        public Duplicates(string txt_DBFolder, Files filed, DLCPackageData datas, string author, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist)
        { 
            InitializeComponent();
        MessageBox.Show("test0");
            var DB_Path = txt_DBFolder;
            txt_Artist.Text = filed.Artist;

            if (datas.SongInfo.SongDisplayName != filed.Song_Title)
            {
                txt_TitleNew.Text = datas.SongInfo.SongDisplayName;
                txt_TitleExisting.Text = filed.Song_Title;
            }
            if (datas.SongInfo.SongDisplayNameSort != filed.Song_Title_Sort)
            {    
              txt_TitleSortNew.Text = datas.SongInfo.SongDisplayNameSort;
             txt_TitleSortExisting.Text = filed.Song_Title_Sort;
            }

            if (original_FileName != filed.Original_FileName)
            {
                  txt_FileNameNew.Text  = original_FileName;
               txt_FileNameExisting.Text = filed.Original_FileName;
            }

            if (author != filed.Author)
                {
                txt_AuthorNew.Text = (author == "" ? "missing" : author);
                txt_AuthorExisting.Text = (filed.Author == "" ? "missing" : filed.Author)  ;
            }

            if (tunnings != filed.Tunning)
            {
                txt_TuningNew.Text = tunnings;
                txt_TuningExisting.Text = filed.Tunning;
            }

            if (datas.PackageVersion != filed.Version)
            {
                txt_VersionNew.Text = datas.PackageVersion;
                txt_VersionExisting.Text = filed.Version;
            }

            if (DD != filed.Has_DD)
            {
                txt_DDNew.Text = DD;
                txt_DDExisting.Text = filed.Has_DD;
            }

            if (datas.Name != filed.DLC_Name)
            {
                txt_DLCIDNew.Text = datas.Name;
                txt_DLCIDExisting.Text = filed.DLC_Name;
            }

            if ((tkversion == "" ? "Yes" : "No") != filed.Is_Original)
            {
                txt_IsOriginalNew.Text = datas.Name;
                txt_IsOriginalExisting.Text = filed.Is_Original;
            }

            if (tkversion != filed.ToolkitVersion)
            {
                txt_ToolkitNew.Text = tkversion;
                txt_ToolkitExisting.Text = filed.ToolkitVersion;
            }
            if (filed.AlbumArt_Hash != art_hash)
            {
                picbx_AlbumArtPathNew.ImageLocation = datas.AlbumArtPath.Replace(".dds", ".png");
                picbx_AlbumArtPathExisting.ImageLocation = filed.AlbumArtPath.Replace(".dds", ".png");
            }
            if   (filed.Audio_Hash != audio_hash)
                {
                txt_AudioNew.Text = (audio_hash.ToString()=="" ? "" :"Yes");
                txt_AudioExisting.Text = (filed.Audio_Hash.ToString() == "" ? "" : "Yes");

            }
            if   (filed.AudioPreview_Hash != audioPreview_hash)
                    {
                txt_PreviewNew.Text = (audio_hash.ToString() == "" ? "" : "Yes");
                txt_PreviewExisting.Text = (audioPreview_hash.ToString() == "" ? "" : "Yes");
            }


            string text = "Same Current -> Existing " + (i + 2) + "/" + (norows + 1) + " " + filed.Artist + "-" + filed.Album + "\n";
            text += ((datas.SongInfo.SongDisplayName == filed.Song_Title) ? "" : ("\n1/14+ Song Titles: " + datas.SongInfo.SongDisplayName + "->" + filed.Song_Title));
            text += ((datas.SongInfo.SongDisplayNameSort == filed.Song_Title_Sort) ? "" : ("\n2/14+ Song Sort Titles: " + datas.SongInfo.SongDisplayNameSort + "->" + filed.Song_Title_Sort));
            text += ((original_FileName == filed.Original_FileName) ? "" : ("\n3/14+ FileNames: " + original_FileName + " - " + filed.Original_FileName));
            text += ((((tkversion == "") ? "Yes" : "No") == filed.Is_Original) ? "" : ("\n4/14+ Is Original: " + ((tkversion == "") ? "Yes" : "No") + " -> " + filed.Is_Original));
            text += ((tkversion == filed.ToolkitVersion) ? "" : ("\n5/14+ Toolkit version: " + tkversion + " -> " + filed.ToolkitVersion));
            text += ("\n6/14+ Author: " + author + " -> " + filed.Author); //((author == filed.Author) ? "" :
            text += ((tunnings == filed.Tunning) ? "" : ("\n7/14+ Tunning: " + tunnings + " -> " + filed.Tunning));
            text += ((datas.PackageVersion == filed.Version) ? "" : ("\n8/14+ Version: " + datas.PackageVersion + " -> " + filed.Version));
            text += ((datas.Name == filed.DLC_Name) ? "" : ("\n9/14+ DLC ID: " + datas.Name + " -> " + filed.DLC_Name));
            text += ((DD == filed.Has_DD) ? "" : ("\n10/14+ DD: " + DD + " -> " + filed.Has_DD));
            //text += "\nOriginal Is Alternate: " + filed.Is_Alternate + (filed.Is_Alternate == "Yes" ? " v. " + filed.Alternate_Version_No : "");
            text += "\n11/14+ Avail. Instr./Tracks: " + ((Bass == "Yes") ? "B" : "") + ((Rhythm == "Yes") ? "R" : "") + ((Lead == "Yes") ? "L" : "") + ((Combo == "Yes") ? "C" : ""); //((Guitar == "Yes") ? "G" : "") + 
            text += " -> " + ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : ""); //+ ((filed.Has_Guitar == "Yes") ? "G" : "")
            text += ((filed.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + filed.art_Hash
            text += ((filed.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + filed.audio_Hash 
            text += ((filed.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + filed.audioPreview_Hash

            //files hash
            DataSet ds = new DataSet();
            i = 0;
            //var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
            string jsonHash = "";
            bool diffjson = true;
            string XmlHash = "";
            var XmlName = "";
            var XmlUUID = "";
            var XmlFile = "";
            var jsonFile = "";
            bool diff = true;
            int k = 0;
            string lastConversionDateTime_cur = "";
            string lastConversionDateTime_exist = "";
            string lastConverjsonDateTime_cur = "";
            string lastConverjsonDateTime_exist = "";
            //MessageBox.Show(DB_Path);
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter daa = new OleDbDataAdapter("SELECT * FROM Arrangements WHERE CDLC_ID=\"" + filed.ID.ToString() + "\";", cnn);
                    //as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                    //MessageBox.Show("0");
                    daa.Fill(ds, "Arrangements");
                    var noOfRec = 0;
                    //MessageBox.Show("0.1");
                    noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    //rtxt_StatisticsOnReadDLCs.Text = noOfRec + "Assesment Arrangement hash file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //MessageBox.Show("1");
                    if (noOfRec > 0)
                    {
                        //MessageBox.Show("1");
                        foreach (var arg in datas.Arrangements)
                        {
                            diff = true; diffjson = true;
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
                                arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json")).Replace("EOF", "Toolkit");
                                //rtxt_StatisticsOnReadDLCs.Text = "-"+XmlName + "=" + (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongXml.File + "=" + XmlFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongFile.File + "==" + jsonFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (XmlName == (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) || (XmlUUID == arg.SongXml.UUID.ToString()))
                                // rtxt_StatisticsOnReadDLCs.Text = "-" + XmlHash + "=" + alist[k] + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                {
                                    if (XmlHash == alist[k])
                                        diff = false;
                                    else
                                    {
                                        lastConversionDateTime_cur = GetTExtFromFile(arg.SongXml.File);
                                        lastConversionDateTime_exist = GetTExtFromFile(XmlFile);
                                        if (arg.ArrangementType.ToString() == "Bass")
                                        {
                                            txt_XMLBassNew.Text = lastConversionDateTime_cur;
                                            txt_XMLBassExisting.Text = lastConversionDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Lead")
                                        {
                                            txt_XMLLeadNew.Text = lastConversionDateTime_cur;
                                            txt_XMLLeadExisting.Text = lastConversionDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Combo")
                                        {
                                            txt_XMLComboNew.Text = lastConversionDateTime_cur;
                                            txt_XMLComboExisting.Text = lastConversionDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Rhythm")
                                        {
                                            txt_XMLRhythmNew.Text = lastConversionDateTime_cur;
                                            txt_XMLRhythmExisting.Text = lastConversionDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Vocal")
                                        {
                                            txt_XMLVocalNew.Text = lastConversionDateTime_cur;
                                            txt_XMLVocalExisting.Text = lastConversionDateTime_exist;
                                        }
                                    }
                                    if (jsonHash == blist[k])
                                        diffjson = false;
                                    else
                                    {
                                        lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File);
                                        lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile);
                                        if (arg.ArrangementType.ToString() == "Bass")
                                        {
                                            txt_JSONBassNew.Text = lastConverjsonDateTime_cur;
                                            txt_JSONBassExisting.Text = lastConverjsonDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Lead")
                                        {
                                            txt_JSONLeadNew.Text = lastConverjsonDateTime_cur;
                                            txt_JSONLeadExisting.Text = lastConverjsonDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Combo")
                                        {
                                            txt_JSONComboNew.Text = lastConverjsonDateTime_cur;
                                            txt_JSONComboExisting.Text = lastConverjsonDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Rhythm")
                                        {
                                            txt_JSONRhythmNew.Text = lastConverjsonDateTime_cur;
                                            txt_JSONRhythmExisting.Text = lastConverjsonDateTime_exist;
                                        }
                                        if (arg.ArrangementType.ToString() == "Vocal")
                                        {
                                            txt_JSONVocalNew.Text = lastConverjsonDateTime_cur;
                                            txt_JSONVocalExisting.Text = lastConverjsonDateTime_exist;
                                        }
                                    }
                                }
                            }


                            text += ((diff) ? "\n" + (14 + i) + "/14+Diff XML" + arg.ArrangementType + arg.RouteMask + ": " + lastConversionDateTime_cur + "->" + lastConversionDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                            text += ((diffjson) ? "\n" + (15 + i) + "/14+Diff Json" + arg.ArrangementType + arg.RouteMask + ": " + lastConverjsonDateTime_cur + "->" + lastConverjsonDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
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

        }

        public Duplicates(string text, DLCManager.Files filed, DLCPackageData datas, string author, string tkversion, string dD, string bass, string guitar, string combo, string rhythm, string lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist)
        {
            Text = text;
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
            this.tunnings = tunnings;
            this.i = i;
            this.norows = norows;
            this.original_FileName = original_FileName;
            this.art_hash = art_hash;
            this.audio_hash = audio_hash;
            this.audioPreview_hash = audioPreview_hash;
            this.alist = alist;
            this.blist = blist;
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

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
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
            public string Group { get; set; }    //	Group
            public string Rating { get; set; }   //	Rating
            public string Description { get; set; }  //	Description
            public string Comments { get; set; }     //	Comments
            public string Show_Album { get; set; }   //	Show_Album
            public string Show_Track { get; set; }   //	Show_Track
            public string Show_Year { get; set; }    //	Show_Year
            public string Show_CDLC { get; set; }    //	Show_CDLC
            public string Show_Rating { get; set; }  //	Show_Rating
            public string Show_Description { get; set; }     //	Show_Description
            public string Show_Comments { get; set; }    //	Show_Comments
            public string Show_Available_Instruments { get; set; }   //	Show_Available_Instruments
            public string Show_Alternate_Version { get; set; }   //	Show_Alternate_Version
            public string Show_MultiTrack_Details { get; set; }  //	Show_MultiTrack_Details
            public string Show_Group { get; set; }   //	Show_Group
            public string Show_Beta { get; set; }    //	Show_Beta
            public string Show_Broken { get; set; }  //	Show_Broken
            public string Show_DD { get; set; }  //	Show_DD
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
            public string Available_Duplicate { get; set; }
            public string Available_Old { get; set; }
        }
        public Files[] files = new Files[10000];

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "Duplicates";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
    public DataSet dssx = new DataSet();
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
        private string tunnings;
        private int i;
        private int norows;
        private string original_FileName;
        private string art_hash;
        private string audio_hash;
        private string audioPreview_hash;
        private List<string> alist;
        private List<string> blist;

        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void Duplicates_Load(object sender, EventArgs e)
    {
            MessageBox.Show("test0");

        }

        //Generic procedure to read and parse Main.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.accdb";
            //Files[] files = new Files[10000];
            //rtxt_StatisticsOnReadDLCs.Text = "re" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var MaximumSize = 0;
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                    dax.Fill(dus, "Main");

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
                        files[i].Group = dataRow.ItemArray[50].ToString();
                        files[i].Rating = dataRow.ItemArray[51].ToString();
                        files[i].Description = dataRow.ItemArray[52].ToString();
                        files[i].Comments = dataRow.ItemArray[53].ToString();
                        files[i].Show_Album = dataRow.ItemArray[54].ToString();
                        files[i].Show_Track = dataRow.ItemArray[55].ToString();
                        files[i].Show_Year = dataRow.ItemArray[56].ToString();
                        files[i].Show_CDLC = dataRow.ItemArray[57].ToString();
                        files[i].Show_Rating = dataRow.ItemArray[58].ToString();
                        files[i].Show_Description = dataRow.ItemArray[59].ToString();
                        files[i].Show_Comments = dataRow.ItemArray[60].ToString();
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
                        files[i].AudioPreview_Hash = dataRow.ItemArray[82].ToString();
                        files[i].Has_BassDD = dataRow.ItemArray[83].ToString();
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
                    //rtxt_StatisticsOnReadDLCs.Text = "r" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in SQLAccess ! " + DB_Path + "-" + cmd);
            }
            //rtxt_StatisticsOnReadDLCs.Text = "rning " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return MaximumSize;//files[10000];
        }

    }
} 
