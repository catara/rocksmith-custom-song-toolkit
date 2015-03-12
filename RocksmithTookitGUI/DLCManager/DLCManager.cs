using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using RocksmithToolkitLib;
using RocksmithToolkitLib.Extensions; //dds
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.Xml; //For DD
using RocksmithToolkitLib.DLCPackage.Manifest; //For DD
using System.IO;
using System.Data.OleDb;
using System.Net;
using System.Reflection;
using System.Security.Cryptography; //For File hash
using System.Diagnostics;//repack
using Ookii.Dialogs; //cue text

using RocksmithToolkitLib.Ogg;//pack check
//For the Export to Excel function
using System.Data.SqlClient;
using RocksmithToTabLib;//for psarc browser
//using Excel = Microsoft.Office.Interop.Excel;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {

        //bcapi
        private const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";
        private bool loading = false;
        public BackgroundWorker bwRGenerate = new BackgroundWorker(); //bcapi
        private BackgroundWorker bwConvert = new BackgroundWorker { WorkerReportsProgress = true }; //bcapi1
        private StringBuilder errorsFound;//bcapi1
        string dlcSavePath = "";
        int no_ord = 0;
        int norows = 0;
        DLCPackageData data;


        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory; //when removing DDC

        OleDbConnection connection;
        OleDbCommand command;
        public GameVersion CurrentGameVersion
        {
            get
            {
                //if (RS2014.Checked)
                return GameVersion.RS2014;
                //else
                //    return GameVersion.RS2012; //Default
            }
            set
            {
                //switch (value)
                //{
                //case GameVersion.RS2014:
                //RS2014.Checked = true;
                //break;
                //default:
                //    RS2012.Checked = true;
                //    break;
                //}
            }
        }


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
            public string Available_Old { get; set; }
            public string Available_Duplicate { get; set; }
            public string Has_Been_Corrected { get; set; }
        }
        public Files[] files = new Files[10000];
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

        public DLCManager()
        {
            InitializeComponent();
            Set_DEBUG();
            chbx_Additional_Manipualtions.SetItemCheckState(5, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(7, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(8, CheckState.Checked);
            //chbx_Additional_Manipualtions.SetItemCheckState(13, CheckState.Checked);
            //chbx_Additional_Manipualtions.SetItemCheckState(14, CheckState.Checked);
            if (chbx_DebugB.Checked) chbx_Additional_Manipualtions.SetItemCheckState(15, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(16, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(17, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(22, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(23, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(24, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(28, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(29, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(30, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(32, CheckState.Checked);
            chbx_Additional_Manipualtions.SetItemCheckState(35, CheckState.Checked);
            // Generate package worker
            //rtxt_StatisticsOnReadDLCs.Text = "genz : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.DoWork += new DoWorkEventHandler(GeneratePackage);
            //rtxt_StatisticsOnReadDLCs.Text = "genn : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            bwRGenerate.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            //rtxt_StatisticsOnReadDLCs.Text = "genc : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            //rtxt_StatisticsOnReadDLCs.Text = "gen7 : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            bwRGenerate.WorkerReportsProgress = true;
        }

        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_RocksmithDLCPath.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
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
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
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
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
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
                txt_RocksmithDLCPath.Text = "C:\\GitHub\\PS3";//tmp\\0PC
                txt_DBFolder.Text = "C:\\GitHub\\tmp";
                txt_TempPath.Text = "C:\\GitHub\\tmp\\0";
                chbx_CleanTemp.Checked = true;
                chbx_CleanDB.Checked = true;
                chbx_HomeDBG.Visible = true;
                chbx_WorkDGB.Visible = true;
                chbx_HomeDGBVM.Visible = true;
                //txt_RocksmithDLCPath.Text = "C:\\Users\\ladmin\\Dropbox\\OneDrive\\dlc\\0PC";
                //txt_DBFolder.Text = "C:\\Users\\ladmin\\Dropbox\\OneDrive\\dlc";
                //txt_TempPath.Text = "C:\\Users\\ladmin\\Dropbox\\OneDrive\\dlc\\0PC\\0";
                //txt_RocksmithDLCPath.Text = "Z:\\TOSHIBA EXT\\Steam\\SteamApps\\common\\Rocksmith2014\\dlc";
                //txt_TempPath.Text = "Z:\\TOSHIBA EXT\\Steam\\SteamApps\\common\\Rocksmith2014\\dlc\\0";
                //txt_TempPath.Text = "tmp\\0\\0_import";
                //txt_TempPath.Text = "tmp\\0\\0_old";
                //txt_TempPath.Text = "tmp\\mac";
                //txt_TempPath.Text = "tmp\\ps3";
                //txt_TempPath.Text = "tmp\\xbox360";
            }
            else
            {
                //chbx_CleanTemp.Checked = false;
                //chbx_CleanDB.Checked = false;
                //txt_RocksmithDLCPath.Text = "";
                //txt_DBFolder.Text = "";
                //txt_TempPath.Text = "";
                //txt_TempPath.Text = "";
                chbx_HomeDBG.Visible = false;
                chbx_WorkDGB.Visible = false;
                chbx_HomeDGBVM.Visible = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            ((MainForm)ParentForm).ReloadControls();
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
            lbl_PreviewText.Text = "Title: " + Manipulate_strings(txt_Title.Text, -1, false);
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Title: " + Manipulate_strings(txt_Title_Sort.Text, -1, false);
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Artist: " + Manipulate_strings(txt_Artist.Text, -1, false);
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Artist: " + Manipulate_strings(txt_Artist_Sort.Text, -1, false);
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Album: " + Manipulate_strings(txt_Album.Text, -1, false);
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "FileName: " + Manipulate_strings(txt_File_Name.Text, -1, true);
        }

        public string Manipulate_strings(string words, int k, bool ifn)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "ff" + rtxt_StatisticsOnReadDLCs.Text;
            //Read from DB
            //int norows = 0;
            //1. Get Random Song ID
            var cmd = "";
            if (k == -1)
            {
                k = 0;
                cmd = "SELECT TOP 1 * FROM Main";
                //Files IDs = new Files();
                norows = SQLAccess(cmd);
            }

            //2. Read from DB
            //cmd = "SELECT * FROM Main WHERE ID = " + files[k].ID;
            //norows = SQLAccess(cmd);
            //rtxt_StatisticsOnReadDLCs.Text ="f" + rtxt_StatisticsOnReadDLCs.Text;
            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = "";
            var readt = false;
            var oldtxt = "";
            var last_ = 0;
            for (i = 0; i <= txt.Length - 1; i++)
            {
                //rtxt_StatisticsOnReadDLCs.Text = k  +"\n"+ rtxt_StatisticsOnReadDLCs.Text;
                curtext = txt[i].ToString();//.Substring(i,1);
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
                }

                if (readt == true)
                    curelem += curtext;
                else fulltxt += curtext;

                bool origQAs = true;
                if (chbx_Additional_Manipualtions.GetItemChecked(25)) origQAs = false;
                //else origQAs = false;

                oldtxt = fulltxt;
                if (curtext == ">")
                {
                    readt = false;
                    switch (curelem)
                    {
                        case "<Artist>":
                            fulltxt += files[k].Artist;
                            break;
                        case "<Title>":
                            fulltxt += files[k].Song_Title;
                            break;
                        case "<Version>":
                            fulltxt += files[k].Version;
                            break;
                        case "<DLCName>":
                            fulltxt += files[k].DLC_Name;
                            break;
                        case "<Album>":
                            fulltxt += files[k].Album;
                            break;
                        case "<Track No.>":
                            fulltxt += ((files[k].Track_No != "") ? (" - " + files[k].Track_No) : "");
                            break;
                        case "<Year>":
                            fulltxt += files[k].Album_Year;
                            break;
                        case "<Rating>":
                            fulltxt += ((files[k].Rating == "") ? "0" : files[k].Rating);
                            break;
                        case "<Alt. Vers.>":
                            fulltxt += "ALT" + files[k].Alternate_Version_No;
                            break;
                        case "<Descr.>":
                            fulltxt += files[k].Description;
                            break;
                        case "<Comm.>":
                            fulltxt += files[k].Comments;
                            break;
                        case "<Tuning>":
                            fulltxt += files[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            fulltxt += ((files[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((files[k].Has_Bass == "Yes") ? "B" : ""); //not yet done for all arrangements
                            break;
                        case "<MTrack Det.>":
                            fulltxt += files[k].MultiTrack_Version;//?
                            break;
                        case "<Group>":
                            fulltxt += files[k].Group;
                            break;
                        case "<Beta>":
                            fulltxt = ((files[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
                            break;
                        case "<Broken>":
                            fulltxt = ((files[k].Is_Broken == "Yes") ? "<B>" : "") + fulltxt;
                            break;
                        case "<File Name>":
                            fulltxt += files[k].Current_FileName;
                            break;
                        case "<Bonus>":
                            fulltxt += ((files[k].Has_Bonus_Arrangement == "Yes") ? "Bonus" : ""); //not yet done for all arrangements
                            break;
                        case "<Artist Short>":
                            fulltxt += files[k].Artist_ShortName;
                            break;
                        case "<Album Short>":
                            fulltxt += files[k].Album_ShortName;
                            break;
                        case "<Author>":
                            fulltxt += files[k].Author;
                            break;
                        case "<Space>":
                            fulltxt += " ";
                            break;
                        case "<Avail. Tracks>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Lead == "Yes") ? "L" : "") + ((files[k].Has_Combo == "Yes") ? "C" : "") + ((files[k].Has_Rhythm == "Yes") ? "R" : "");
                            break;
                        case "<Bass_HasDD>":
                            fulltxt += (files[k].Has_BassDD == "No" && files[k].Has_DD == "Yes" ? "NoBDD" : ""); //not yet done
                            break;
                        case "<Avail. Instr.>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        default:
                            if ((origQAs) || (ifn))
                            {
                                switch (curelem)
                                {
                                    case "<DD>":
                                        fulltxt += files[k].Has_DD == "Yes" ? "DD" : "noDD";
                                        break;
                                    case "<CDLC>":
                                        fulltxt += files[k].DLC;
                                        break;
                                    case "<QAs>":
                                        fulltxt += (((files[k].Has_Cover == "Yes") || (files[k].Has_Preview == "Yes") || (files[k].Has_Vocals == "Yes")) ? "" : "NOs-") + ((files[k].Has_Cover == "Yes") ? "" : "C") + ((files[k].Has_Preview == "Yes") ? "" : "P") + ((files[k].Has_Vocals == "Yes") ? "" : "V"); //+((files[k].Has_Sections == "Yes") ? "" : "S")
                                        break;
                                    case "<lastConversionDateTime>":
                                        fulltxt += files[k].Import_Date; //not yet done
                                        break;
                                    default: break;
                                }
                            }
                            break;
                    }
                    if (oldtxt == fulltxt && last_ > 0)
                        //{
                        //    rtxt_StatisticsOnReadDLCs.Text = i + "...\n"+rtxt_StatisticsOnReadDLCs.Text;                        
                        fulltxt = fulltxt.Substring(0, last_);
                    //}
                    last_ = fulltxt.Length;
                }
            }
            //rtxt_StatisticsOnReadDLCs.Text = "returning " + i + " char " + fulltxt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return fulltxt;
        }

        //Generic procedure to read and parse Main.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            var DB_Path = txt_DBFolder.Text + "\\Files.accdb";
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
                    dax.Dispose();

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
                        files[i].Available_Old = dataRow.ItemArray[87].ToString();
                        files[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
                        files[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
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
            SqlConnection cnn;
            string connectionString = null;
            string sql = null;
            string data = null;
            int i = 0;
            int j = 0;

            MessageBox.Show("test");

            //Excel.Application xlApp;
            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlApp = new Excel.Application();
            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            sql = "SELECT * FROM Main";
            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);
            dscmd.Dispose();

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
            string cmd = "SELECT * FROM Main";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            //else if (rbtn_Population_All.Checked) ;
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;
            //MessageBox.Show("-1");
            //cmd += "ORDER BY Artist";
            //Read from DB
            int norows = 0;
            int i = 1;
            norows = SQLAccess(cmd);
            cmd = "DELETE FROM Main WHERE ID IN (";
            if (norows > 0)
                foreach (var file in files)
                {

                    cmd += file.ID.ToString();
                    i++;
                    if (i < norows) cmd += ", ";
                    if (i == norows) break;
                }
            cmd += ");";
            var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";

            DialogResult result1 = MessageBox.Show("Following records will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
                try
                {
                    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        DataSet dus = new DataSet();
                        OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                        dax.Fill(dus, "Main");
                        dax.Dispose();
                        MessageBox.Show("Records have been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Main DB connection in Cleanup ! " + DB_Path + "-" + cmd);
                }
        }



        // Read a Folder (clean temp folder)
        // Decompress the PC DLCs
        // Read details and populate a DB (clean Import DB before, and only populate Main if not there already)
        public void btn_PopulateDB_Click(object sender, EventArgs e)
        {
            rtxt_StatisticsOnReadDLCs.Text = "Starting... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //Set_DEBUG(); //Default value when in dEV/Debug mode, if needed

            var Temp_Path_Import = txt_TempPath.Text + "\\0_import";
            var old_Path_Import = txt_TempPath.Text + "\\0_old";
            var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
            var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
            var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
            string pathDLC = txt_RocksmithDLCPath.Text;
            if (!Directory.Exists(txt_TempPath.Text) || !Directory.Exists(Temp_Path_Import) || !Directory.Exists(pathDLC) || !Directory.Exists(old_Path_Import) || !Directory.Exists(broken_Path_Import) || !Directory.Exists(dupli_Path_Import))
            {
                MessageBox.Show(String.Format("One of the mandatory backend, folder is missing " + txt_TempPath.Text + ", " + Temp_Path_Import + ", " + pathDLC + ", " + old_Path_Import + ", " + broken_Path_Import + ", " + dupli_Path_Import+", "+ dlcpacks, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error));
                try
                {
                    DirectoryInfo di;
                    DialogResult result1 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Cancel Import command\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        if (!Directory.Exists(txt_TempPath.Text) && (txt_TempPath.Text != null)) di = Directory.CreateDirectory(txt_TempPath.Text);
                        if (!Directory.Exists(Temp_Path_Import) && (Temp_Path_Import != null)) di = Directory.CreateDirectory(Temp_Path_Import);
                        if (!Directory.Exists(pathDLC) && (pathDLC != null)) di = Directory.CreateDirectory(pathDLC);
                        if (!Directory.Exists(old_Path_Import) && (old_Path_Import != null)) di = Directory.CreateDirectory(old_Path_Import);
                        if (!Directory.Exists(broken_Path_Import) && (broken_Path_Import != null)) di = Directory.CreateDirectory(broken_Path_Import);
                        if (!Directory.Exists(dupli_Path_Import) && (dupli_Path_Import != null)) di = Directory.CreateDirectory(dupli_Path_Import);
                        if (!Directory.Exists(dlcpacks) && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks);
                        //rtxt_StatisticsOnReadDLCs.Text = Directory.Exists(dupli_Path_Import) +"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    else if (result1 == DialogResult.No) return;
                    else Application.Exit();
                }
                catch (Exception ex)
                {
                    rtxt_StatisticsOnReadDLCs.Text = "issue with creating directories... " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open create folders ! " + txt_TempPath + "-" + Temp_Path_Import + "-" + pathDLC + "-" + old_Path_Import + "-" + broken_Path_Import + "-" + dupli_Path_Import+"-"+ dlcpacks);
                }
                //return;
            }

            //Clean Temp Folder
            if (chbx_CleanTemp.Checked && !chbx_Additional_Manipualtions.GetItemChecked(38)) //39.Use only unpacked songs already in the 0 / 0_Import folder
            {
                //clear content of 0_import folder
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Temp_Path_Import);
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    if (dir.Name != "0_import" && dir.Name != "0_old" && dir.Name != "0_broken" && dir.Name != "0_duplicate" && dir.Name != "0_dlcpacks") dir.Delete(true);
                }

                //clean app working folders 0 folder
                //{
                System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text);
                foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                {
                    if (dir.Name != "0_import" && dir.Name != "0_old" && dir.Name != "0_broken" && dir.Name != "0_duplicate" && dir.Name != "0_dlcpacks") dir.Delete(true);
                }
                //}
            }

            //help code
            //using (var u = new UpdateForm())
            //{
            //    u.Init(onlineVersion);
            //    u.ShowDialog();
            //}


            //Clean ImportDB
            DataSet dss = new DataSet();
            var DB_Path = "";
            //MessageBox.Show("cleaninig");
            DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter daa = new OleDbDataAdapter("DELETE FROM Import;", cnn);
                    daa.Fill(dss, "Import");
                    daa.Dispose();
                    rtxt_StatisticsOnReadDLCs.Text = "Cleaning....Import table...." + DB_Path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_CleanDB.Checked)
                    {
                        OleDbDataAdapter dan = new OleDbDataAdapter("DELETE FROM Main;", cnn);
                        dan.Fill(dss, "Main");
                        OleDbDataAdapter dam = new OleDbDataAdapter("DELETE FROM Arrangements;", cnn);
                        dam.Fill(dss, "Arrangements");
                        OleDbDataAdapter dag = new OleDbDataAdapter("DELETE FROM Tones;", cnn);
                        dag.Fill(dss, "Tones");
                        dan.Dispose();
                        dam.Dispose();
                        dag.Dispose();
                    }

                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                Console.WriteLine(ee.Message);
                rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //continue;
            }
            rtxt_StatisticsOnReadDLCs.Text = DB_Path + " Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

            int i = 0;
            if (!chbx_Additional_Manipualtions.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder
            {
                //GetDirList and calcualte hash
                //MessageBox.Show(pathDLC, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                string[] filez;
                if (chbx_Additional_Manipualtions.GetItemChecked(37)) //38. Import other formats but PC, as well(separately of course)
                    filez = System.IO.Directory.GetFiles(pathDLC, "*.psarc");
                else
                    filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");

                foreach (string s in filez)
                {
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
                        // To inform the user and continue is 
                        // sufficient for this demonstration. 
                        // Your application may require different behavior.
                        Console.WriteLine(ee.Message);
                        rtxt_StatisticsOnReadDLCs.Text = "error at import" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        continue;
                    }
                    //- To remove usage of ee and loading
                    Console.WriteLine("{0} : {1} : {2}", fi.Name, fi.Directory, loading);

                    //details end

                    //Generating the HASH code
                    var FileHash = "";
                    using (FileStream fs = File.OpenRead(s))
                    {
                        SHA1 sha = new SHA1Managed();
                        FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                        fs.Close();
                    }

                    //Populate ImportDB
                    rtxt_StatisticsOnReadDLCs.Text = "File " + (i + 1) + " :" + s + "\n" + rtxt_StatisticsOnReadDLCs.Text; //+ "-------"  + fi.GetHashCode() + "-----------" + fi.Length + "-" + fi.CreationTime + "-" + fi.DirectoryName + "-" + fi.LastWriteTime + "-" + fi.Name;
                    DataSet dsz = new DataSet();
                    DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
                    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        string updatecmd; //s.Substring(s.Length - pathDLC.Length)
                        updatecmd = "INSERT INTO Import (FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate) VALUES (\"" + s + "\",\"";
                        updatecmd += fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\"" + FileHash + "\",\"" + fi.Length + "\",\"";
                        updatecmd += System.DateTime.Today + "\");";
                        OleDbDataAdapter dab = new OleDbDataAdapter(updatecmd, cnb);
                        dab.Fill(dsz, "Import");
                        dab.Dispose();
                        //dsz.Tables["Files"].AcceptChanges();
                        //MessageBox.Show(da)
                    }

                    //pB_ReadDLCs.Increment(1);
                    i++;
                }

                DataSet dz = new DataSet();
                var del = 0;
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    string updatecmd; //s.Substring(s.Length - pathDLC.Length)

                    updatecmd = "SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash = s.FileHash WHERE d.ID is not null GROUP BY s.FileHash;";
                    OleDbDataAdapter dbf = new OleDbDataAdapter(updatecmd, cnb);
                    dbf.Fill(dz, "Import");
                    del = dz.Tables[0].Rows.Count;
                    dbf.Dispose();
                    rtxt_StatisticsOnReadDLCs.Text = updatecmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    updatecmd = @"DELETE FROM Import
                                WHERE ID not IN (SELECT MAX(s.ID) FROM Import s LEFT JOIN Import as d on d.FileHash=s.FileHash WHERE d.ID is not null GROUP BY s.FileHash);";
                    OleDbDataAdapter db = new OleDbDataAdapter(updatecmd, cnb);
                    db.Fill(dz, "Import");
                    db.Dispose();


                    //dsz.Tables["Files"].AcceptChanges();
                    //MessageBox.Show(da)
                }
                rtxt_StatisticsOnReadDLCs.Text = del + "/" + i + " Import files Inserted (excl. " + del + " duplicates)" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }
            else
            {
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(txt_RocksmithDLCPath.Text);
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {

                    //Populate ImportDB
                    rtxt_StatisticsOnReadDLCs.Text = "Folder " + (i + 1) + " :" + "s" + "\n" + rtxt_StatisticsOnReadDLCs.Text; //+ "-------"  + fi.GetHashCode() + "-----------" + fi.Length + "-" + fi.CreationTime + "-" + fi.DirectoryName + "-" + fi.LastWriteTime + "-" + fi.Name;
                    DataSet dsz = new DataSet();
                    DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
                    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        string updatecmd; //s.Substring(s.Length - pathDLC.Length)
                        updatecmd = "INSERT INTO Import (FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate) VALUES (\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"";
                        updatecmd += txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + txt_RocksmithDLCPath.Text + "\\" + dir.Name + "\",\"" + DateTime.Now + "\",\"" + "0" + "\",\"" + "0" + "\",\"";
                        updatecmd += System.DateTime.Today + "\");";
                        OleDbDataAdapter dab = new OleDbDataAdapter(updatecmd, cnb);
                        dab.Fill(dsz, "Import");
                        dab.Dispose();
                        //dsz.Tables["Files"].AcceptChanges();
                        //MessageBox.Show(da)
                        // if (dir.Name != "0_import" && dir.Name != "0_old" && dir.Name != "0_broken" && dir.Name != "0_duplicate") dir.Delete(true);
                    } }
            }

            //START WITH mAINdb UPDATE
            DataSet ds = new DataSet();
            DataSet dns = new DataSet();
            var m = 0;
            var errr = true;
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    var cmd = @"SELECT DISTINCT FullPath, Path, FileName, FileHash, FileSize, ImportDate, m.Import_Date
                            FROM Import as i
                            LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash
                            WHERE m.ID is not NULL;";
                    OleDbDataAdapter dha = new OleDbDataAdapter(cmd, cnn);
                    dha.Fill(dns, "Import");
                    dha.Dispose();
                    var tft = "\n Ignoring "; ;
                    var noOfRec = dns.Tables[0].Rows.Count;
                    //rtxt_StatisticsOnReadDLCs.Text = noOfRec + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(30) && noOfRec > 0)
                        tft = "";
                    for (m = 0; m < noOfRec; m++)
                    {
                        //rtxt_StatisticsOnReadDLCs.Text = "x" + "\n" + rtxt_StatisticsOnReadDLCs.Text;//dns.Tables[0].Rows[m].ItemArray[0].ToString()
                        File.Delete(dns.Tables[0].Rows[m].ItemArray[0].ToString());
                        tft += "\n Deleting " + dns.Tables[0].Rows[m].ItemArray[0].ToString() + " as imported on " + dns.Tables[0].Rows[m].ItemArray[5].ToString();
                    }

                    rtxt_StatisticsOnReadDLCs.Text = tft + noOfRec + "/" + (noOfRec + m) + " already imported" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    cmd = @"SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate
                                FROM Import as i
                                LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash
                                WHERE m.ID is NULL;";
                    OleDbDataAdapter daa = new OleDbDataAdapter(cmd, cnn);
                    daa.Fill(ds, "Import");
                    daa.Dispose();
                    noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    rtxt_StatisticsOnReadDLCs.Text = noOfRec + "/" + (noOfRec + m) + " New Songs 2 Import into MainDB" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //OleDbDataAdapter dac = new OleDbDataAdapter("INSERT INTO Main (Import_Path, Original_FileName, Current_FileName, File_Hash, File_Size, Import_Date) SELECT Path, FileName,FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                    //dac.Fill(ds, "Main");
                    //OleDbDataAdapter dac = new OleDbDataAdapter(updatecmd, cnb);

                    //OleDbDataAdapter dad = new OleDbDataAdapter("SELECT count(*) FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is not NULL;", cnn);
                    //dad.Fill(ds, "Import");

                    if (noOfRec > 0)
                    {
                        //connection.Open();
                        //DataSet dds = new DataSet();
                        //OleDbDataAdapter daf = new OleDbDataAdapter("SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                        //daf.Fill(dds, "Import");
                        //oledbAdapter.Dispose();
                        //connection.Close();
                        //dss.Tables["Import"].AcceptChanges();
                        pB_ReadDLCs.Value = 0;
                        pB_ReadDLCs.Maximum = 2 * (noOfRec - 1);
                        for (i = 0; i <= noOfRec - 1; i++)
                        {
                            //MessageBox.Show(pB_ReadDLCs.Maximum+" test " +i); 
                            //DataTable AccTable = aSet.Tables["Accounts"];
                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            rtxt_StatisticsOnReadDLCs.Text = (i + 1) + "/" + noOfRec + " " + FullPath + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if (!chbx_Additional_Manipualtions.GetItemChecked(37))
                                if (!FullPath.IsValidPSARC())
                                {
                                    MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            errr = true;
                            var unpackedDir = "";
                            //rtxt_StatisticsOnReadDLCs.Text = "1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            var packagePlatform = FullPath.GetPlatform();
                            var Available_Duplicate = "No";
                            var Available_Old = "No";
                            if (!chbx_Additional_Manipualtions.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder
                            {

                                try
                                {
                                    // UNPACK
                                    unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                                    //packagePlatform = FullPath.GetPlatform();
                                }
                                catch (Exception ex)
                                {
                                    // MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // MessageBox.Show("Error decompressing the file!(BACH OFFICIAL DLC CAUSE OF WEIRD CHAR IN FILENAME) " + "-" );
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    errr = false;
                                    //rtxt_StatisticsOnReadDLCs.Text = "predone" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    try
                                    {
                                        var Path = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        File.Copy(FullPath, Path, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                                        errr = true; //bcapi???
                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }
                            else unpackedDir = FullPath;

                            //Commenting Reorganize as they might have fixed the incompatib char issue
                            // REORGANIZE
                            //System.Threading.Thread.Sleep(1000);
                            var platform = FullPath.GetPlatform();
                            //rtxt_StatisticsOnReadDLCs.Text = unpackedDir + " reorg...\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized    
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
                                    // System.Threading.Thread.Sleep(20000);
                                    //rtxt_StatisticsOnReadDLCs.Text = "1111 ...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories);
                                    foreach (var json in jsonFiles)
                                    {
                                        if (Path.GetFileNameWithoutExtension(json).ToUpperInvariant().Contains("VOCAL"))
                                            continue;
                                        if (platform.version == RocksmithToolkitLib.GameVersion.RS2014)
                                        {
                                            var jsons = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), SearchOption.AllDirectories);
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
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at reorg" + unpackedDir + "---" + Temp_Path_Import + "...\n" + rtxt_StatisticsOnReadDLCs.Text;

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
                                        var Path = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                        File.Copy(FullPath, Path, true);
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2 @org but copied in the broken folder" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }

                            // rtxt_StatisticsOnReadDLCs.Text = "2" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            var DD = "No";
                            var Bass_Has_DD = "No";
                            var sect1on = "Yes";
                            if (errr)
                            {
                                //FIX for adding preview_preview_preview
                                if (unpackedDir == "")
                                {
                                    unpackedDir = "C:\\GitHub\\tmp\\0\\0_import\\songs_Pc";
                                    rtxt_StatisticsOnReadDLCs.Text = "FAILED2 empty path" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                // LOAD DATA
                                //rtxt_StatisticsOnReadDLCs.Text = "2.5" + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                
                                var info = DLCPackageData.LoadFromFolder(unpackedDir, packagePlatform);
                                //rtxt_StatisticsOnReadDLCs.Text = "3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                string ff = info.SongInfo.Artist, gg = info.SongInfo.ArtistSort, hhh = info.SongInfo.SongDisplayName, jj = info.SongInfo.SongDisplayNameSort, kk = info.SongInfo.Album;
                                if (chbx_Additional_Manipualtions.GetItemChecked(35)) //36.
                                {
                                    //Remove weird/illegal characters
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\\", "");
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "");
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("/", "");
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("?", "");
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace(":", "");
                                    info.SongInfo.Artist = info.SongInfo.Artist.Replace("\"", "'");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\\", "");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("/", "");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("?", "");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace(":", "");
                                    info.SongInfo.ArtistSort = info.SongInfo.ArtistSort.Replace("\"", "'");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\\", "");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("/", "");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("?", "");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace(":", "");
                                    info.SongInfo.SongDisplayName = info.SongInfo.SongDisplayName.Replace("\"", "'");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\\", "");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("/", "");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("?", "");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace(":", "");
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayNameSort.Replace("\"", "'");
                                    info.SongInfo.Album = info.SongInfo.Album.Replace("\\", "");
                                    info.SongInfo.Album = info.SongInfo.Album.Replace("\"", "");
                                    info.SongInfo.Album = info.SongInfo.Album.Replace("/", "");
                                    info.SongInfo.Album = info.SongInfo.Album.Replace("?", "");
                                    info.SongInfo.Album = info.SongInfo.Album.Replace(":", "");

                                    //info.AlbumArtPath = info.SongInfo.Album.Replace("/", "");
                                    if (ff != info.SongInfo.Artist) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegal characters \\,\",/,?,: from Artist..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (gg != info.SongInfo.ArtistSort) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegal characters \\,\",/,?,: from ArtistSort..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (hhh != info.SongInfo.SongDisplayName) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegal characters \\,\",/,?,: from Title..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (jj != info.SongInfo.SongDisplayNameSort) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegal characters \\,\",/,?,: from TitleSort..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (kk != info.SongInfo.Album) rtxt_StatisticsOnReadDLCs.Text = "removing potential illegal characters \\,\",/,?,: from Album..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (chbx_Additional_Manipualtions.GetItemChecked(16)) //17.Import with Artist/ Title same as Artist / Title Sort
                                {
                                    info.SongInfo.ArtistSort = info.SongInfo.Artist;
                                    info.SongInfo.SongDisplayNameSort = info.SongInfo.SongDisplayName;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "5 :"+ info.SongInfo.SongDisplayNameSort +  "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (chbx_Additional_Manipualtions.GetItemChecked(22)) //23. Import with the The/Die only at the end of Title Sort     
                                {
                                    if (chbx_Additional_Manipualtions.GetItemChecked(20))
                                    {
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "The " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "Die " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "the " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "die " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "THE " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",The" : info.SongInfo.SongDisplayNameSort);
                                        if (info.SongInfo.SongDisplayNameSort.Length > 4) info.SongInfo.SongDisplayNameSort = (info.SongInfo.SongDisplayNameSort.Substring(0, 4) == "DIE " ? info.SongInfo.SongDisplayNameSort.Substring(4, info.SongInfo.SongDisplayNameSort.Length - 4) + ",Die" : info.SongInfo.SongDisplayNameSort);

                                    }
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "The " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "Die " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "the " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "die " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "THE " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",The" : info.SongInfo.ArtistSort);
                                    if (info.SongInfo.ArtistSort.Length > 4) info.SongInfo.ArtistSort = (info.SongInfo.ArtistSort.Substring(0, 4) == "DIE " ? info.SongInfo.ArtistSort.Substring(4, info.SongInfo.ArtistSort.Length - 4) + ",Die" : info.SongInfo.ArtistSort);
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                //if (i == 0) MessageBox.Show("3");
                                rtxt_StatisticsOnReadDLCs.Text = "\n Song " + (i + 1) + ": " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                pB_ReadDLCs.Increment(1);

                                //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                                var xmlFiles = Directory.GetFiles(unpackedDir, "*.xml", SearchOption.AllDirectories);
                                platform = FullPath.GetPlatform();
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

                                    // rtxt_StatisticsOnReadDLCs.Text = "ffff\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                                    //rtxt_StatisticsOnReadDLCs.Text = "ddf\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    Song2014 xmlContent = Song2014.LoadFromFile(xml);
                                    //rtxt_StatisticsOnReadDLCs.Text = "xxx\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var manifestFunctions = new ManifestFunctions(platform.version);
                                    //rtxt_StatisticsOnReadDLCs.Text = "aaa\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    //Get sections and lastconvdate
                                    var json = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(xml)), SearchOption.AllDirectories);
                                    //rtxt_StatisticsOnReadDLCs.Text = json.Length + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (json.Length > 0)//&& g==1
                                    {
                                        foreach (var fl in json)
                                        {
                                            //rtxt_StatisticsOnReadDLCs.Text = Path.GetFileNameWithoutExtension(fl).ToLower() + " name " +"\n" + rtxt_StatisticsOnReadDLCs.Text;

                                            if (Path.GetFileNameWithoutExtension(fl).ToLower().Contains("bass") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("rhythm") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("combo"))
                                            {
                                                Attributes2014 attr = Manifest2014<Attributes2014>.LoadFromFile(fl).Entries.ToArray()[0].Value.ToArray()[0].Value;
                                                manifestFunctions.GenerateSectionData(attr, xmlContent);
                                                //rtxt_StatisticsOnReadDLCs.Text = Path.GetFileNameWithoutExtension(fl).ToLower() + " json lastconv date: " + attr.LastConversionDateTime + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                //rtxt_StatisticsOnReadDLCs.Text = json.Length + "sections: " + attr.Sections.Count + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                if (attr.Sections.Count == 0) sect1on = "No";
                                                clist.Add(attr.LastConversionDateTime);
                                                dlist.Add((attr.Sections.Count > 0 ? "Yes" : "No"));
                                                //rtxt_StatisticsOnReadDLCs.Text = "techniques count: " + attr.Techniques.Count + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            }
                                            else
                                            {
                                                rtxt_StatisticsOnReadDLCs.Text = "no section/lastconvdate" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                clist.Add("");
                                                dlist.Add("No");
                                            }
                                        }
                                    }

                                    g++;

                                    if (manifestFunctions.GetMaxDifficulty(xmlContent) > 1) DD = "Yes";

                                    //Bass_Has_DD
                                    //rtxt_StatisticsOnReadDLCs.Text = "\n chekin for BassDD" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var manifestFunctions1 = new ManifestFunctions(platform.version);
                                    if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass"))
                                    {
                                        //rtxt_StatisticsOnReadDLCs.Text = "\n still chekin for BassDD" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                                        Song2014 xmlContent1 = Song2014.LoadFromFile(xml);

                                        if (manifestFunctions1.GetMaxDifficulty(xmlContent1) > 1)
                                            Bass_Has_DD = "Yes";
                                    }
                                }

                                // READ ARRANGEMENTS
                                //rtxt_StatisticsOnReadDLCs.Text = "3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //var updateAcmd = "";
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
                                //var jsonFiles = Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories); //Get directory of JSON files in case song dir is not ORGANIZED :)
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
                                    }

                                    else if (arg.ArrangementType == ArrangementType.Vocal) Vocalss = "Yes";
                                    else if (arg.ArrangementType == ArrangementType.Bass)
                                    {
                                        Bass = "Yes";

                                        PluckedType = arg.PluckedType.ToString();
                                        if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                                        else Tunings = arg.Tuning;
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "gen ar hashes: " +arg.SongXml.File+"/"+arg.SongXml.File + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var s1 = arg.SongXml.File;
                                    using (FileStream fs = File.OpenRead(s1))
                                    {
                                        SHA1 sha = new SHA1Managed();
                                        alist.Add((BitConverter.ToString(sha.ComputeHash(fs))).ToString());
                                        fs.Close();
                                    }

                                    if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                        s1 = (arg.SongXml.File.Replace(".xml", ".json").Replace("\\EOF\\", "\\Toolkit\\"));
                                    else
                                        s1 = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));

                                    using (FileStream fs = File.OpenRead(s1))
                                    {
                                        SHA1 sha = new SHA1Managed();
                                        blist.Add((BitConverter.ToString(sha.ComputeHash(fs))).ToString());
                                        fs.Close();
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "done ar hashes: " +"\n" + rtxt_StatisticsOnReadDLCs.Text;

                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //Check Tones
                                var Tones_Custom = "No";
                                foreach (var tn in info.TonesRS2014)//, Type
                                {
                                    if (tn.IsCustom)
                                        Tones_Custom = "Yes";
                                }


                                var alt = "";
                                //var trackno = "_"; //wip

                                //Get Author and Toolkit version
                                var versionFile = Directory.GetFiles(unpackedDir, "toolkit.version", SearchOption.AllDirectories);
                                tkversion = "";
                                author = "";
                                if (versionFile.Length > 0)
                                    tkversion = ReadPackageToolkitVersion(versionFile[0]);

                                if (versionFile.Length > 0)
                                {
                                    author = ReadPackageAuthor(versionFile[0]);
                                    if (tkversion.Length == 0)
                                        tkversion = ReadPackageOLDToolkitVersion(versionFile[0]);
                                }
                                if (author == "" && tkversion != "") author = "Custom Song Creator";
                                //rtxt_StatisticsOnReadDLCs.Text = vpos + "===" + txt.Length+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                if (versionFile.Length <= 0) Is_Original = "Yes";
                                else Is_Original = "No";
                                //rtxt_StatisticsOnReadDLCs.Text = Is_Original + "===" + versionFile.Length+ "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                //Get Version from FileName
                                var import_path = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                                var original_FileName = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                string txt = original_FileName;
                                int vpos = (txt.IndexOf("v")) + 1;
                                float vv = 0;
                                for (var hh = 0; vpos > 0 && hh < 100; hh++)
                                {
                                    vpos = (txt.IndexOf("v")) + 1;

                                    //rtxt_StatisticsOnReadDLCs.Text = vpos + "===" + txt.Length+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (vpos > 0 && txt.Substring(vpos, 1).ToInt32() > 0)
                                    {
                                        vv = txt.Substring(vpos, 1).ToInt32();
                                        // and info.PackageVersion
                                        if ((txt.Substring(vpos + 1, 1) == "." || txt.Substring(vpos + 1, 1) == "_") && txt.Substring(vpos + 2, 1).ToInt32() > 0)
                                        {
                                            //rtxt_StatisticsOnReadDLCs.Text = info.PackageVersion.ToInt32() + "--=" + (vv.ToString() + "." + txt.Substring(vpos + 2, 1)) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            vv = Convert.ToSingle(vv.ToString() + "." + txt.Substring(vpos + 2, 1));
                                        }
                                        else if (txt.Substring(vpos + 1, 1).ToInt32() > 0) vv = Convert.ToSingle(vv.ToString() + "." + txt.Substring(vpos + 1, 1));
                                        //rtxt_StatisticsOnReadDLCs.Text = "___" + txt.Substring(vpos + 1, 1) + "---" + txt.Substring(vpos + 2, 1) + "---" + txt.Substring(vpos + 2, 1) + "---" + "\n" + rtxt_StatisticsOnReadDLCs.Text; 
                                        if (vv > info.PackageVersion.ToInt32()) info.PackageVersion = vv.ToString();
                                        //else if (info.PackageVersion.ToInt32()>0) info.PackageVersion = vv.ToString();
                                        hh = 100;
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "000" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    txt = txt.Substring(vpos, txt.Length - vpos);
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "=___" + vv + "---" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //foreach (var infofile in versionFile)
                                //{
                                //    rtxt_StatisticsOnReadDLCs.Text += "\n last verrsfi " + infofile;
                                //    tkversion += infofile;
                                //}

                                //example of properly working with sql
                                // Command to Insert Records
                                //OleDbCommand cmdInsert = new OleDbCommand();
                                //cmdInsert.CommandText = "INSERT INTO AutoIncrementTest (Description) VALUES (?)";
                                //cmdInsert.Connection = cnJetDB;
                                //cmdInsert.Parameters.Add(new OleDbParameter("Description", OleDbType.VarChar, 40, "Description"));
                                //oleDa.InsertCommand = cmdInsert;


                                //rtxt_StatisticsOnReadDLCs.Text = info.AlbumArtPath+"---" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                //Generating the HASH code
                                art_hash = "";
                                string audio_hash = "";
                                string audioPreview_hash = "";
                                AlbumArtPath = info.AlbumArtPath;
                                string ss = "";
                                try
                                {
                                    using (FileStream fs = File.OpenRead(AlbumArtPath))
                                    {
                                        SHA1 sha = new SHA1Managed();
                                        art_hash = BitConverter.ToString(sha.ComputeHash(fs));//MessageBox.Show(FileHash+"-"+ss);
                                                                                              //convert to png
                                        ExternalApps.Dds2Png(AlbumArtPath);
                                        fs.Close();
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "hashes: " + ss + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    ss = info.OggPath;
                                    using (FileStream fs = File.OpenRead(ss))
                                    {
                                        SHA1 sha = new SHA1Managed();
                                        audio_hash = BitConverter.ToString(sha.ComputeHash(fs));
                                        fs.Close();
                                    }

                                    ss = info.OggPreviewPath;
                                    //rtxt_StatisticsOnReadDLCs.Text = "rhashes: " + rtxt_StatisticsOnReadDLCs.Text;
                                    if (ss != null)
                                        using (FileStream fs = File.OpenRead(ss))
                                        {
                                            SHA1 sha = new SHA1Managed();
                                            audioPreview_hash = BitConverter.ToString(sha.ComputeHash(fs));
                                            fs.Close();
                                        }
                                    //rtxt_StatisticsOnReadDLCs.Text = "6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                catch (Exception ex)
                                {
                                    // MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    // MessageBox.Show("Error decompressing the file!(BACH OFFICIAL DLC CAUSE OF WEIRD CHAR IN FILENAME) " + "-" );
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + ss + "problem at hash" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    errr = false;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "rhashes: " + art_hash + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                //Check if CDLC have already been imported (hash key)
                                // 1. If hash already exists do not insert
                                // 2. If hash does not exists then:
                                // 2.1.1 If Artist+Album+Title or dlcname exists check author. If same check version
                                // 2.1.1.1 If (Artist+Album+Title or dlcname)+author the same check version If bigger add
                                // 2.1.1.2 If (Artist+Album+Title or dlcname)+author the same check version If smaller ignore
                                // 2.1.1.3 If (Artist+Album+Title or dlcname)+author the same check version If same ?
                                // 3.1.2 If (Artist+Album+Title or dlcname) exists check author. If the not the same add as alternate
                                // 4.1.3 If (Artist+Album+Title or dlcname) exists check author. If empty/generic(Custom Song Creator) show statistics and add as give choice to alternate or ignore
                                //SELECT if the same artist, album, songname
                                var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                                sel += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                                sel += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                                //sel += "OR (\"%LCASE(Song_Title)%\" like LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                                sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\") ORDER BY Is_Original ASC";
                                //Read from DB
                                int norows = 0;
                                norows = SQLAccess(sel);
                                //rtxt_StatisticsOnReadDLCs.Text = "assesing " + norows  + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //MessageBox.Show("Chose: 1.Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);                           

                                var b = 0;
                                var artist = "Insert";
                                string j = ""; string k = "";
                                var IDD = "";
                                var folder_name = "";
                                var filename = "";
                                bool newold = chbx_Additional_Manipualtions.GetItemChecked(32);
                                Random random = new Random();
                                //info.Name = Name;
                                //info.SongInfo.SongDisplayName =  info.SongInfo.SongDisplayName;
                                if (norows > 0)
                                    foreach (var file in files)
                                    {
                                        SongDisplayName = "";
                                        Namee = "";
                                        //rtxt_StatisticsOnReadDLCs.Text = "\n ------"+ file.Folder_Name.ToString() + "------ " + b + " ------"+ file.Current_FileName.ToString() + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        if (b >= norows) break;
                                        folder_name = file.Folder_Name;
                                        filename = file.Current_FileName;
                                        //rtxt_StatisticsOnReadDLCs.Text =file.Author.ToLower() +"-"+author.ToLower() + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        //When importing a original when there is already a similar CDLC
                                        if (author == "" && tkversion == "" && chbx_Additional_Manipualtions.GetItemChecked(14))
                                        {
                                            artist = "Insert";

                                            //Generate MAX Alternate NO
                                            var sel1 = sel.Replace("*", "max(Alternate_Version_No)");
                                            sel1 = sel.Replace(" ORDER BY Is_Original ASC", "");
                                            //rtxt_StatisticsOnReadDLCs.Text = sel1 + "-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            DataSet ddr = new DataSet();
                                            OleDbDataAdapter dat = new OleDbDataAdapter(sel1, cnn);
                                            dat.Fill(ddr, "Main");
                                            dat.Dispose();

                                            //UPDATE the 1(s) not an alternate already
                                            int max = ddr.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32() + 1;
                                            var sel2 = "Update Main Set Song_Title = Song_Title+\" a." + max + "\", Song_Title_Sort = Song_Title_Sort+\" a." + max + "\", Is_Alternate = \"Yes\", Alternate_Version_No=" + max + " where ID in (" + sel.Replace("*", "ID") + ") and Is_Alternate=\"No\"";
                                            //rtxt_StatisticsOnReadDLCs.Text = max.ToString()+"-"+sel2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            DataSet dxr = new DataSet();
                                            OleDbDataAdapter dax = new OleDbDataAdapter(sel2, cnn);
                                            dax.Fill(dxr, "Main");
                                            dax.Dispose();

                                            //Add also a random DLCName if any of the Alternates has the same DLC Name ssame as the original
                                            var sel3 = "UPDATE Main SET DLC_Name = DLC_Name+\"" + random.Next(0, 100000) + "\" WHERE ID in (" + sel.Replace("*", "ID") + ") and LCASE(DLC_Name) = \"" + info.Name.ToLower() + "\"";
                                            //rtxt_StatisticsOnReadDLCs.Text =  random.Next(0, 100000) + "-"+sel3 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            DataSet dxf = new DataSet();
                                            OleDbDataAdapter dbx = new OleDbDataAdapter(sel3, cnn);
                                            dbx.Fill(dxf, "Main");
                                            dbx.Dispose();
                                            break;
                                        }

                                        if ((author.ToLower() == file.Author.ToLower() && author != "" && file.Author != "" && file.Author != "Custom Song Creator" && author != "Custom Song Creator") || (file.DLC_Name == info.Name))
                                        {
                                            if (file.DLC_Name.ToLower() == info.Name.ToLower())
                                                artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original);
                                            else
                                            {
                                                if (file.Version.ToInt32() > info.PackageVersion.ToInt32()) artist = "Update";
                                                if (file.Version.ToInt32() < info.PackageVersion.ToInt32())
                                                    if (file.Is_Alternate != "Yes") { artist = "Ignore"; rtxt_StatisticsOnReadDLCs.Text = "IGNORED" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                                    else artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original);
                                                if (file.Version.ToInt32() == info.PackageVersion.ToInt32()) artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original);
                                                else { artist = "Ignore"; rtxt_StatisticsOnReadDLCs.Text = "IGNORED" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                                // assess=alternate, update or ignore//as maybe a new package(ing) is desired to be inserted in the DB
                                            }
                                        }
                                        else
                                            if (author.ToLower() != file.Author.ToLower() && (author != "" && author != "Custom Song Creator" && file.Author != "Custom Song Creator" && file.Author != ""))
                                            artist = "Alternate";
                                        else artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, DB_Path, clist, dlist, newold, Is_Original);
                                        //rtxt_StatisticsOnReadDLCs.Text = "7 "+b + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        //Exit condition



                                        if (artist == "Alternate")
                                        {
                                            alt = "1";
                                            //txt = (info.PackageVersion != null ? "No" : "Yes");
                                            //rtxt_StatisticsOnReadDLCs.Text = "\n" + "-" + "\n-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            if (Namee != "") info.Name = Namee;
                                            if (SongDisplayName != "") info.SongInfo.SongDisplayName = SongDisplayName;
                                            if (Title_Sort != "") info.SongInfo.SongDisplayNameSort = Title_Sort;
                                            if (ArtistSort != "") info.SongInfo.ArtistSort = ArtistSort;
                                            if (Artist != "") info.SongInfo.Artist = Artist;
                                            if (Is_Alternate != "" && Is_Original == "No") alt = Alternate_No;
                                            if (Album != "") info.SongInfo.Album = Album;
                                            if (Alternate_No != "" && Is_Original == "No") alt = Alternate_No;
                                            if (PackageVersion != "") info.PackageVersion = PackageVersion;
                                            //if (AlbumArtPath != "") info.Name = Name;
                                            //if (art_hash != "") info.Name = Name;
                                            //if (txt == "No") info.PackageVersion = null;
                                            //rtxt_StatisticsOnReadDLCs.Text = "\n"+"-"+ (PackageVersion != "" && info.PackageVersion != null ? "No" : "Yes") + "\n-"+ info.PackageVersion + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            artist = "Insert";

                                            //Get the higgest Alternate Number
                                            sel = "SELECT max(Alternate_Version_No) FROM Main WHERE(LCASE(Artist) =LCASE(\"" + info.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                                            sel += "(LCASE(Song_Title)=LCASE(\"" + info.SongInfo.SongDisplayName + "\") OR ";
                                            sel += "LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" OR ";
                                            sel += "LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\"))) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                                            //Get last inserted ID
                                            //rtxt_StatisticsOnReadDLCs.Text = sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            DataSet dsr = new DataSet();
                                            OleDbDataAdapter dad = new OleDbDataAdapter(sel, cnn);
                                            dad.Fill(dss, "Main");
                                            dad.Dispose();
                                            string altver = "";
                                            foreach (DataRow dataRow in dss.Tables[0].Rows)
                                            {
                                                altver = dataRow.ItemArray[0].ToString();
                                                if (Is_Original == "No") alt = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
                                                                                                                  //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            }

                                            if (file.DLC_Name.ToLower() == info.Name.ToLower()) info.Name = random.Next(0, 100000) + info.Name;
                                            if (file.Song_Title.ToLower() == info.SongInfo.SongDisplayName.ToLower() && Is_Original == "No") info.SongInfo.SongDisplayName += " a." + (alt.ToInt32() + 1).ToString() + ((author == null || author == "Custom Song Creator") ? "" : " " + author);// ;//random.Next(0, 100000).ToString()
                                                                                                                                                                                                                                                                                                 //if (file.Song_Title_Sort == info.SongInfo.SongDisplayNameSort) info.SongInfo.SongDisplayNameSort += random.Next(0, 100000);

                                            // rtxt_StatisticsOnReadDLCs.Text = "highest " + altver + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                    
                                        }

                                        //if (artist != "Ignore")
                                        //{
                                        //    b ++;
                                        //rtxt_StatisticsOnReadDLCs.Text = txt_RocksmithDLCPath.Text + "\\" + original_FileName + " ccccc\n"+ dupli_Path_Import + "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;

                                        //} //exit if an update/alternate=insert was triggered..autom or by choice(asses)
                                        //else
                                        b++;
                                        //rtxt_StatisticsOnReadDLCs.Text =  "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                                        IDD = file.ID; //Save Id in case of update or asses-update
                                                       //rtxt_StatisticsOnReadDLCs.Text = "\\" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                                        j = file.Version;
                                        //rtxt_StatisticsOnReadDLCs.Text = "dd" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                                        k = file.Author;
                                        if (b >= norows || artist != "Insert") break;
                                    }
                                //rtxt_StatisticsOnReadDLCs.Text = "6" + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                                if (artist == "Ignore" && chbx_Additional_Manipualtions.GetItemChecked(29))
                                {
                                    //rtxt_StatisticsOnReadDLCs.Text = dupli_Path_Import + "\\" + original_FileName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    if (!File.Exists(dupli_Path_Import + "\\" + original_FileName))
                                        File.Move(txt_RocksmithDLCPath.Text + "\\" + original_FileName, dupli_Path_Import + "\\" + original_FileName);
                                    else File.Delete(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                                    Available_Duplicate = "Yes";
                                }

                                //Define final path for the imported song
                                //rtxt_StatisticsOnReadDLCs.Text = info.PackageVersion + " ccccc\n" + rtxt_StatisticsOnReadDLCs.Text;
                                var norm_path = txt_TempPath.Text + "\\" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongDisplayName;
                                //if (artist == "ignore") ;

                                //@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=
                                connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                                command = connection.CreateCommand();
                                //rtxt_StatisticsOnReadDLCs.Text = "00 " + original_FileName + rtxt_StatisticsOnReadDLCs.Text;
                                if (artist == "Update")
                                {
                                    //Update MainDB
                                    rtxt_StatisticsOnReadDLCs.Text = "Updating / Overriting " + IDD + "-" + j + "-" + info.PackageVersion + "-" + k + ".." + rtxt_StatisticsOnReadDLCs.Text;

                                    //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                                    command.CommandText = "UPDATE Main SET ";
                                    command.CommandText += "Import_Path = @param1, ";
                                    command.CommandText += "Original_FileName = @param2, ";
                                    command.CommandText += "Current_FileName = @param3, ";
                                    command.CommandText += "File_Hash = @param4, ";
                                    command.CommandText += "Original_File_Hash = @param5, ";
                                    command.CommandText += "File_Size = @param6, ";
                                    command.CommandText += "Import_Date = @param7, ";
                                    command.CommandText += "Folder_Name = @param8, ";
                                    command.CommandText += "Song_Title = @param9, ";
                                    command.CommandText += "Song_Title_Sort = @param10, ";
                                    command.CommandText += "Album = @param11, ";
                                    command.CommandText += "Artist = @param12, ";
                                    command.CommandText += "Artist_Sort = @param13, ";
                                    command.CommandText += "Album_Year = @param14, ";
                                    command.CommandText += "Version = @param15, ";
                                    command.CommandText += "AverageTempo = @param16, ";
                                    command.CommandText += "Volume = @param17, ";
                                    command.CommandText += "Preview_Volume = @param18, ";
                                    command.CommandText += "DLC_Name = @param19, ";
                                    command.CommandText += "DLC_AppID = @param20, ";
                                    command.CommandText += "AlbumArtPath = @param21, ";
                                    command.CommandText += "AudioPath = @param22, ";
                                    command.CommandText += "audioPreviewPath = @param23, ";
                                    command.CommandText += "Has_Bass = @param24, ";
                                    command.CommandText += "Has_Guitar = @param25, ";
                                    command.CommandText += "Has_Lead = @param26, ";
                                    command.CommandText += "Has_Rhythm = @param27, ";
                                    command.CommandText += "Has_Combo = @param28, ";
                                    command.CommandText += "Has_Vocals = @param29, ";
                                    command.CommandText += "Has_Sections = @param30, ";
                                    command.CommandText += "Has_Cover = @param31, ";
                                    command.CommandText += "Has_Preview = @param32, ";
                                    command.CommandText += "Has_Custom_Tone = @param33, ";
                                    command.CommandText += "Has_DD = @param34, ";
                                    command.CommandText += "Has_Version = @param35, ";
                                    command.CommandText += "Has_Author = @param36, ";
                                    command.CommandText += "Tunning = @param37, ";
                                    command.CommandText += "Bass_Picking = @param38, ";
                                    command.CommandText += "DLC = @param39, ";
                                    command.CommandText += "SignatureType = @param40, ";
                                    command.CommandText += "Author = @param41, ";
                                    command.CommandText += "ToolkitVersion = @param42, ";
                                    command.CommandText += "Is_Original = @param43, ";
                                    command.CommandText += "Is_Alternate = @param44, ";
                                    command.CommandText += "Alternate_Version_No = @param45, ";
                                    command.CommandText += "AlbumArt_Hash = @param46, ";
                                    command.CommandText += "Audio_Hash = @param47, ";
                                    command.CommandText += "audioPreview_Hash = @param48, ";
                                    command.CommandText += "Bass_Has_DD = @param49, ";
                                    command.CommandText += "Has_Bonus_Arrangement = @param50, ";
                                    command.CommandText += "Available_Duplicate = @param51, ";
                                    command.CommandText += "Available_Old = @param52, ";
                                    command.CommandText += "Description = @param53, ";
                                    command.CommandText += "Comments = @param54 ";
                                    command.CommandText += "WHERE ID = " + IDD;

                                    command.Parameters.AddWithValue("@param1", import_path);
                                    command.Parameters.AddWithValue("@param2", original_FileName);
                                    command.Parameters.AddWithValue("@param3", original_FileName);
                                    command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                                    command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                                    command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4].ToString());
                                    command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5].ToString());
                                    command.Parameters.AddWithValue("@param8", unpackedDir);
                                    command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                                    command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                                    command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                                    command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                                    command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                                    command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                                    command.Parameters.AddWithValue("@param15", ((info.PackageVersion == null) ? "1" : info.PackageVersion));
                                    command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                                    command.Parameters.AddWithValue("@param17", info.Volume);
                                    command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                                    command.Parameters.AddWithValue("@param19", info.Name);
                                    command.Parameters.AddWithValue("@param20", info.AppId);
                                    command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param22", info.OggPath);
                                    command.Parameters.AddWithValue("@param23", info.OggPreviewPath ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param24", Bass);
                                    command.Parameters.AddWithValue("@param25", Guitar);
                                    command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                                    command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                                    command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                                    command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                                    command.Parameters.AddWithValue("@param30", sect1on);
                                    command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath != "") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param33", Tones_Custom);
                                    command.Parameters.AddWithValue("@param34", DD);
                                    command.Parameters.AddWithValue("@param35", ((info.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param36", ((((author != "" && tkversion != "") || author == "Custom Song Creator") && Is_Original == "No") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param37", Tunings);
                                    command.Parameters.AddWithValue("@param38", PluckedType);
                                    command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                                    command.Parameters.AddWithValue("@param40", info.SignatureType);
                                    command.Parameters.AddWithValue("@param41", ((author != "") ? author : (tkversion != "" ? "Custom Song Creator" : "")));
                                    command.Parameters.AddWithValue("@param42", tkversion);
                                    command.Parameters.AddWithValue("@param43", Is_Original);
                                    command.Parameters.AddWithValue("@param44", ((alt == "") ? "No" : "Yes"));
                                    command.Parameters.AddWithValue("@param45", alt);
                                    command.Parameters.AddWithValue("@param46", art_hash);
                                    command.Parameters.AddWithValue("@param47", audio_hash);
                                    command.Parameters.AddWithValue("@param48", audioPreview_hash);
                                    command.Parameters.AddWithValue("@param49", Bass_Has_DD ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param50", bonus ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param51", Available_Duplicate ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param52", Available_Old ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param53", description ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param54", comment ?? DBNull.Value.ToString());

                                    //EXECUTE SQL/INSERT
                                    try
                                    {
                                        command.CommandType = CommandType.Text;
                                        connection.Open();
                                        command.ExecuteNonQuery();
                                        //Deleted old folder
                                        Directory.Delete(folder_name, true);
                                        ////remove original dir TO DO
                                        //Directory.Delete(source_dir, true);
                                        //move old/aleady imported&saved file
                                        if (chbx_Additional_Manipualtions.GetItemChecked(29))
                                        {
                                            rtxt_StatisticsOnReadDLCs.Text = old_Path_Import + "\\" + filename + "dupli_Path_Import arrangement:" + dupli_Path_Import + "\\" + filename + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            try
                                            {
                                                if (!File.Exists(dupli_Path_Import + "\\" + filename))
                                                    if (File.Exists(old_Path_Import + "\\" + filename))
                                                    {
                                                        File.Move(old_Path_Import + "\\" + filename, dupli_Path_Import + "\\" + filename);
                                                        Available_Duplicate = "Yes";
                                                    }
                                                    else rtxt_StatisticsOnReadDLCs.Text = "___" + rtxt_StatisticsOnReadDLCs.Text;
                                                else
                                                {
                                                    File.Delete(txt_RocksmithDLCPath.Text + "\\" + filename);
                                                    Available_Duplicate = "Yes";
                                                }
                                                rtxt_StatisticsOnReadDLCs.Text = "deleting...dele...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                MessageBox.Show("Issues at duplicate folder" + "-" + ex.Message + filename);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        MessageBox.Show("Can not open Arrangements DB connection in Import ! " + DB_Path + "-" + original_FileName + "-" + command.CommandText);

                                        throw;
                                    }
                                    finally
                                    {
                                        if (connection != null) connection.Close();
                                    }
                                }


                                if (artist == "Insert")
                                {
                                    //Update by INSERT into Main DB+info.AlbumArtPath+"____________"
                                    rtxt_StatisticsOnReadDLCs.Text = "Inserting " + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    //connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+DB_Path+";Persist Security Info=False");
                                    //command = connection.CreateCommand();
                                    //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
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
                                    command.CommandText += "Comments ";
                                    command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                                    command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                                    command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                    command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                                    command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48,@param49";
                                    command.CommandText += ",@param50,@param51,@param52,@param53,@param54" + ")"; //,@param44,@param45,@param46,@param47,@param48,@param49
                                                                                                                  //command.CommandText += ") VALUES(@param50,@param51,@param52" + ")"; //,@param33,@param44,@param44,@param45,@param46,@param47,@param48,@param49

                                    command.Parameters.AddWithValue("@param1", import_path);
                                    command.Parameters.AddWithValue("@param2", original_FileName);
                                    command.Parameters.AddWithValue("@param3", original_FileName);
                                    command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3]);
                                    command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3]);
                                    command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4]);
                                    command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5]);
                                    command.Parameters.AddWithValue("@param8", unpackedDir);
                                    command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                                    command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                                    command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                                    command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                                    command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                                    command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                                    command.Parameters.AddWithValue("@param15", ((info.PackageVersion == null) ? "1" : info.PackageVersion));
                                    command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                                    command.Parameters.AddWithValue("@param17", info.Volume);
                                    command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                                    command.Parameters.AddWithValue("@param19", info.Name);
                                    command.Parameters.AddWithValue("@param20", info.AppId);
                                    command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                                    command.Parameters.AddWithValue("@param22", info.OggPath);
                                    command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));// ((info.OggPreviewPath == "") ? DBNull.Value : info.OggPreviewPath));
                                    command.Parameters.AddWithValue("@param24", Bass);
                                    command.Parameters.AddWithValue("@param25", Guitar);
                                    command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                                    command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                                    command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                                    command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                                    command.Parameters.AddWithValue("@param30", sect1on);
                                    command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath != "") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param33", Tones_Custom);
                                    command.Parameters.AddWithValue("@param34", DD);
                                    command.Parameters.AddWithValue("@param35", ((info.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param36", ((((author != "" && tkversion != "") || author == "Custom Song Creator") && Is_Original == "No") ? "Yes" : "No"));
                                    command.Parameters.AddWithValue("@param37", Tunings);
                                    command.Parameters.AddWithValue("@param38", PluckedType);
                                    command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                                    command.Parameters.AddWithValue("@param40", info.SignatureType);
                                    command.Parameters.AddWithValue("@param41", ((author != "") ? author : (tkversion != "" ? "Custom Song Creator" : "")));
                                    command.Parameters.AddWithValue("@param42", tkversion);
                                    command.Parameters.AddWithValue("@param43", Is_Original);
                                    command.Parameters.AddWithValue("@param44", ((alt == "") ? "No" : "Yes"));
                                    command.Parameters.AddWithValue("@param45", alt);
                                    command.Parameters.AddWithValue("@param46", art_hash);
                                    command.Parameters.AddWithValue("@param47", audio_hash);
                                    command.Parameters.AddWithValue("@param48", audioPreview_hash);
                                    command.Parameters.AddWithValue("@param49", Bass_Has_DD);
                                    command.Parameters.AddWithValue("@param50", bonus);
                                    command.Parameters.AddWithValue("@param51", Available_Duplicate);
                                    command.Parameters.AddWithValue("@param52", Available_Old);
                                    command.Parameters.AddWithValue("@param53", description);
                                    command.Parameters.AddWithValue("@param54", comment);

                                    //EXECUTE SQL/INSERT
                                    try
                                    {
                                        command.CommandType = CommandType.Text;
                                        connection.Open();
                                        command.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        rtxt_StatisticsOnReadDLCs.Text = "error at update" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        throw;
                                    }
                                    finally
                                    {
                                        if (connection != null) connection.Close();
                                    }
                                    //If No version found then defaulted to 1
                                    //TO DO If default album cover then mark it as suck !?
                                    //If no version found must by Rocksmith Original or DLC

                                    rtxt_StatisticsOnReadDLCs.Text = "Records inserted in Main= " + (i + 1) + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = artist + "...||..." + rtxt_StatisticsOnReadDLCs.Text;
                                if (artist == "Insert" || artist == "Update") //Common set of action for all
                                {
                                    //Get last inserted ID
                                    DataSet dus = new DataSet();
                                    OleDbDataAdapter dad = new OleDbDataAdapter("SELECT ID FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"", cnn);
                                    dad.Fill(dus, "Main");
                                    dad.Dispose();
                                    //rtxt_StatisticsOnReadDLCs.Text ="last id= " + dus.Tables[0].Rows[0].ItemArray[0].ToString() + "..." + rtxt_StatisticsOnReadDLCs.Text;

                                    //OleDbDataAdapter objAdapter = new OleDbDataAdapter("SELECT @@IDENTITY AS 'ID';", cnn);

                                    //Useful
                                    // Get and Store IDENTITY (Primary Key) for further
                                    // INSERTS in child table [Order Details]
                                    //cmd.CommandText = "SELECT @@identity";
                                    //string id = cmd.ExecuteScalar().ToString();
                                    //objAdapter.Fill(dus, "Main");
                                    //string strID = dus.Tables["Main"].Rows[0].ToString();

                                    //UPDATE ArarngementsDB
                                    var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                                    int n = 0;
                                    foreach (var arg in info.Arrangements)//, Type
                                    {
                                        command = connection.CreateCommand();
                                        //ss = arg.SongXml.File.ToString();
                                        //string XMLFile_hash="";
                                        //using (FileStream fs = File.OpenRead(ss))
                                        //{
                                        //    SHA1 sha = new SHA1Managed();
                                        //    XMLFile_hash = BitConverter.ToString(sha.ComputeHash(fs));
                                        //}

                                        try
                                        {
                                            var mss = arg.SongXml.File.ToString();
                                            int poss = 0;
                                            if (mss.Length > 0)
                                            {
                                                poss = mss.ToString().LastIndexOf("\\") + 1;
                                                //rtxt_StatisticsOnReadDLCs.Text = norm_path+"__________" + arg.SongFile.File + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                                {
                                                    arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                                                    arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                                }
                                                else
                                                {
                                                    arg.SongXml.File = norm_path + "\\songs\\arr\\" + mss.Substring(poss);
                                                    arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", SearchOption.AllDirectories)[0]));
                                                }
                                            }

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
                                                rtxt_StatisticsOnReadDLCs.Text = "error at insert " + command.CommandText + "\n" + arg.Name + rtxt_StatisticsOnReadDLCs.Text;
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
                                            MessageBox.Show("Can not open Arrangements DB connection in Import ! " + DB_Path + "-" + arg.Name + "-" + command.CommandText);
                                        }
                                    }
                                    rtxt_StatisticsOnReadDLCs.Text = "Arrangements Updated " + info.Arrangements.Count + "...\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    //UPDATE TonesDB
                                    CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                                    connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                                    foreach (var tn in info.TonesRS2014)//, Type
                                    {
                                        command = connection.CreateCommand();
                                        try
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
                                                                                                                                              //rtxt_StatisticsOnReadDLCs.Text = "1: " + tn.Name +"\n"+ rtxt_StatisticsOnReadDLCs.Text;
                                                                                                                                              //rtxt_StatisticsOnReadDLCs.Text = "2: " + (tn.GearList.Amp== null ? "" : tn.GearList.Amp.Type) +"\n"+ rtxt_StatisticsOnReadDLCs.Text;
                                                                                                                                              //rtxt_StatisticsOnReadDLCs.Text = "3: " + (tn.GearList.Amp== null ? "" : tn.GearList.Amp.KnobValues["1"]) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                                                                                                              //rtxt_StatisticsOnReadDLCs.Text = "4: " + (tn.GearList.Amp== null ? "" : tn.GearList.Amp.PedalKey)+"\n"+rtxt_StatisticsOnReadDLCs.Text;
                                            command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27";
                                            command.CommandText += ")";
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

                                            //rtxt_StatisticsOnReadDLCs.Text = command.CommandText + "\n" + tn.Name + rtxt_StatisticsOnReadDLCs.Text;
                                            //EXECUTE SQL/INSERT
                                            try
                                            {
                                                command.CommandType = CommandType.Text;
                                                connection.Open();
                                                command.ExecuteNonQuery();
                                            }
                                            catch (Exception)
                                            {
                                                rtxt_StatisticsOnReadDLCs.Text = "error in arag" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
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
                                            MessageBox.Show("Can not open Tones DB connection in Import ! " + DB_Path + "-" + tn.Name + "-" + command.CommandText);
                                        }
                                    }
                                    rtxt_StatisticsOnReadDLCs.Text = "ToneDB Updated " + info.TonesRS2014.Count + "..." + rtxt_StatisticsOnReadDLCs.Text;

                                    //Move Extracted Song to Temp Folder
                                    int pos = 0;
                                    int l = 0;
                                    DataSet dis = new DataSet();
                                    try //Move from _import into Temp folder (copy+delete as move sometimes fails)
                                    {
                                        //Directory.(unpackedDir, norm_path);
                                        string source_dir = @unpackedDir;
                                        string destination_dir = @norm_path;

                                        // substring is to remove destination_dir absolute path (E:\).

                                        // Create subdirectory structure in destination    
                                        foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                                        {
                                            Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                                            // Example:
                                            //     > C:\sources (and not C:\E:\sources)
                                        }

                                        foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                                        {
                                            File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                                        }
                                        Directory.Delete(source_dir, true);
                                        //var ee = "";
                                        //rtxt_StatisticsOnReadDLCs.Text = " DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    catch (Exception ee)
                                    {
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                                        Console.WriteLine(ee.Message);
                                    }

                                    if (chbx_Additional_Manipualtions.GetItemChecked(15)) //16. Move Original Imported files to temp/0_old                               
                                    {
                                        //Move imported psarc into the old folder
                                        //rtxt_StatisticsOnReadDLCs.Text = "predone" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                        try
                                        {
                                            // if (!File.Exists(txt_RocksmithDLCPath.Text + "\\" + original_FileName))
                                            File.Copy(txt_RocksmithDLCPath.Text + "\\" + original_FileName, old_Path_Import + "\\" + original_FileName, true);
                                            File.Delete(txt_RocksmithDLCPath.Text + "\\" + original_FileName);
                                            Available_Old = "Yes";
                                            rtxt_StatisticsOnReadDLCs.Text = "File Moved to old" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                        }
                                        catch (System.IO.FileNotFoundException ee)
                                        {
                                            rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            Console.WriteLine(ee.Message);
                                        }
                                    }

                                    //Fixing any _preview_preview issue..Start
                                    //Correct moved file path audio,preview
                                    //Add wem
                                    //Corrent arrangements file path
                                    cmd = "UPDATE Main SET Available_Old=\"" + Available_Old + "\",";
                                    //var cmdA = "UPDATE Arrangements SET";
                                    //rtxt_StatisticsOnReadDLCs.Text = "0" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    var audiopath = "";
                                    var audioprevpath = "";
                                    var ms = "";
                                    ms = info.AlbumArtPath;
                                    if (ms != "" && ms != null)
                                    {
                                        //rtxt_StatisticsOnReadDLCs.Text ="\n" +AlbumArtPath +"\n"+ info.AlbumArtPath+"\n000" + ms + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                        pos = ms.ToString().LastIndexOf("\\") + 1;
                                        if (AlbumArtPath == info.AlbumArtPath)
                                            if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                                cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                            else
                                                cmd += " AlbumArtPath=\"" + (info.AlbumArtPath == "" ? "" : norm_path) + "\\gfxassets\\album_art\\" + ms.Substring(pos) + "\"";
                                        else //Override Album Art during the Duplication assements process
                                        {
                                            //rtxt_StatisticsOnReadDLCs.Text = "\nimg override"+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                                            cmd += " AlbumArt_Hash=\"" + art_hash + "\", AlbumArtPath=\"" + AlbumArtPath + "\"";
                                        }
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "1" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    pos = (info.OggPath.LastIndexOf(".wem"));
                                    ms = info.OggPath;

                                    var path_decom1 = "";
                                    var path_decom2 = "";
                                    if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                                    {
                                        path_decom1 = "\\Toolkit\\";
                                        path_decom2 = "\\EOF\\";
                                    }
                                    else
                                    {
                                        path_decom1 = "\\audio\\windows\\";
                                        path_decom2 = "\\audio\\windows\\"; //"\\songs\\arr\\";
                                    }

                                    if (ms.Length > 0 && pos > 1)
                                    {
                                        ms = ms.Substring(0, pos);
                                        if (info.OggPath.LastIndexOf("_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview"));
                                        pos = ms.LastIndexOf("\\") + 1;
                                        l = ms.Substring(pos).Length;
                                        audiopath = norm_path + path_decom1 + ms.Substring(pos, l);
                                        cmd += ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "" : " ,") + " AudioPath=\"" + audiopath + ".wem\"";
                                        cmd += " , OggPath=\"" + norm_path + path_decom2 + ms.Substring(pos, l) + ".ogg\"";
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "2" +cmd+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    pos = (info.OggPath.LastIndexOf(".wem"));
                                    ms = info.OggPath;
                                    if (ms.Length > 0 && pos > 1 && (info.OggPreviewPath != null))
                                    {
                                        ms = ms.Substring(0, pos);
                                        if (info.OggPath.LastIndexOf("_preview_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview_preview"));
                                        pos = ms.LastIndexOf("\\") + 1;
                                        l = ms.Substring(pos).Length;
                                        audioprevpath = norm_path + path_decom1 + ms.Substring(pos, l);
                                        cmd += " , audioPreviewPath=\"" + audioprevpath + "_preview.wem\"";
                                        cmd += " , oggPreviewPath=\"" + norm_path + path_decom2 + ms.Substring(pos, l) + "_preview.ogg\"";
                                    }
                                    //rtxt_StatisticsOnReadDLCs.Text = "3" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    cmd += " , Folder_Name=\"" + norm_path + "\"";

                                    cmd += " WHERE ID=" + CDLC_ID;
                                    // rtxt_StatisticsOnReadDLCs.Text = "3" + cmd+ "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                                    {// 1. If hash already exists do not insert
                                        OleDbDataAdapter dgs = new OleDbDataAdapter(cmd, cn);
                                        dgs.Fill(dis, "Main");
                                        dgs.Dispose();
                                        rtxt_StatisticsOnReadDLCs.Text = "Main DB updated after DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    //fix potentially issues with songs with the audio preview WEM  file the same as the original song(file size{no preview})
                                    //Move wem to KIT folder + rename
                                    //var WemFiles = Directory.GetFiles(unpackedDir, "*.wem", SearchOption.AllDirectories);
                                    //if (WemFiles.Count() <= 0)
                                    //    throw new InvalidDataException("Audio files not found.");
                                    if (info.OggPreviewPath != null)
                                        if (info.OggPreviewPath.LastIndexOf("_preview_preview.wem") > 1)
                                        {
                                            try
                                            {
                                                File.Move((audiopath + "_preview.wem"), (audiopath + ".wem"));
                                                File.Move((audioprevpath + "_preview.wem"), (audioprevpath + ".wem"));
                                                rtxt_StatisticsOnReadDLCs.Text = "Issues w the WEM filenames when no preview " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            }
                                            catch (Exception ee)
                                            {
                                                rtxt_StatisticsOnReadDLCs.Text = "FAILED1" + ee.Message + "----" + info.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                Console.WriteLine(ee.Message);
                                            }
                                        }
                                    //Fixing any _preview_preview issue..End
                                }
                                ////Updating the Standardization table
                                //    try
                                //    {
                                //        cmd = "SELECT * FROM Standardization WHERE StrComp(Artist,\"" + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\""+ info.SongInfo.Album + "\", 0) = 0;";
                                //        //rtxt_StatisticsOnReadDLCs.Text = "assesing populating normalization" + cmd + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //        DataSet dzs = new DataSet();
                                //        OleDbDataAdapter dam = new OleDbDataAdapter(cmd, cnn);
                                //        dam.Fill(dzs, "Main");
                                //        dam.Dispose();
                                //        //rtxt_StatisticsOnReadDLCs.Text = "no of rows returned" + dzs.Tables[0].Rows.Count + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //        if (dzs.Tables[0].Rows.Count == 0)
                                //        {
                                //            cmd = "INSERT INTO Standardization (Artist, Album) VALUES (\"" + info.SongInfo.Artist + "\",\""+ info.SongInfo.Album + "\")";
                                //            //rtxt_StatisticsOnReadDLCs.Text = "populating normalization" + cmd + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //            DataSet dfs = new DataSet();
                                //            OleDbDataAdapter dbm = new OleDbDataAdapter(cmd, cnn);
                                //            dbm.Fill(dfs, "Main");
                                //            dbm.Dispose();
                                //        }
                                //    }
                                //    catch (System.IO.FileNotFoundException ee)
                                //    {
                                //        rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                //        Console.WriteLine(ee.Message);
                                //    }
                                rtxt_StatisticsOnReadDLCs.Text = "done" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                pB_ReadDLCs.Increment(1);
                            }

                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                rtxt_StatisticsOnReadDLCs.Text = "Error when importing..somewhere" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                Console.WriteLine(ee.Message);
                //continue;
            }

            //Cleanup
            if (chbx_Additional_Manipualtions.GetItemChecked(24)) //25. Use translation tables for naming standardization
            {
                Translation_And_Correction(txt_DBFolder.Text);
            }

            //Show Intro database window
            MainDB frm = new MainDB(txt_DBFolder.Text, txt_TempPath.Text, chbx_Additional_Manipualtions.GetItemChecked(33), txt_RocksmithDLCPath.Text);
            frm.Show();

            //dataGrid.frmMainForm.ActiveForm.Show();
            //MessageBox.Show("f");
        }

        public void Translation_And_Correction(string dbp)
        {
            //var cmd2 = "";
            var cmd1 = "";
            var cmd2 = "";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var DB_Path = dbp + "\\Files.accdb";
            var cmd = "";
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    cmd = "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\") OR (AlbumArt_Correction <> \"\") order by id;";
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dad = new OleDbDataAdapter(cmd, cnn);
                    dad.Fill(dus, "Standardization");
                    dad.Dispose();
                    //rtxt_StatisticsOnReadDLCs.Text = "Standardization of " + dus.Tables[0].Rows.Count + " base records\n\n" + rtxt_StatisticsOnReadDLCs.Text;


                    //rtxt_StatisticsOnReadDLCs.Text = "Repack backgroundworker.."+ norows +  rtxt_StatisticsOnReadDLCs.Text;
                    for (var i = 0; i < dus.Tables[0].Rows.Count; i++)
                    {
                        artist_c = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                        album_c = dus.Tables[0].Rows[i].ItemArray[4].ToString();
                        artpath_c = dus.Tables[0].Rows[i].ItemArray[5].ToString();

                        cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + (album_c != "" ? "\"," : "\"") : "") + (album_c != "" ? " Album = \"" + album_c + (artpath_c != "" ? "\"," : "\"") : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"" : "");
                        cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dus.Tables[0].Rows[i].ItemArray[1].ToString() + "\" AND Album=\"" + dus.Tables[0].Rows[i].ItemArray[3].ToString() + "\"";
                        //rtxt_StatisticsOnReadDLCs.Text = dus.Tables[0].Rows[i].ItemArray[0].ToString() +"cmd -" +album_c+"-"+ cmd1 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
                        das.Fill(dus, "Main");
                        das.Dispose();
                    }
                    //OleDbDataAdapter dun = new OleDbDataAdapter(cmd2, cnn);
                    //dun.Fill(dus, "Main");
                    //insert any translation if not already existing
                    cmd2 = "INSERT INTO Standardization (Artist, Album) SELECT DISTINCT (Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Album])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
                    OleDbDataAdapter dam = new OleDbDataAdapter(cmd2, cnn);
                    //rtxt_StatisticsOnReadDLCs.Text = cmd2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    dam.Fill(dus, "Main");
                    dam.Dispose();

                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                rtxt_StatisticsOnReadDLCs.Text = "Error at standardization" + cmd1 + cmd + cmd2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                Console.WriteLine(ee.Message);
                //continue;
            }
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied");
        }

        private static string ReadPackageAuthor(string filePath)
        {
            var info = File.OpenText(filePath);
            string author = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("Package Author:"))
                    author = line.Split(':')[1].Trim();
            }
            info.Close();
            return author;
        }
        private static string ReadPackageToolkitVersion(string filePath)
        {
            var info = File.OpenText(filePath);
            string Toolkit_version = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("Toolkit version:"))
                    Toolkit_version = line.Split(':')[1].Trim();
            }
            info.Close();
            return Toolkit_version;
        }
        private static string ReadPackageOLDToolkitVersion(string filePath)
        {
            var info = File.OpenText(filePath);
            string Toolkit_version = "";
            string line;
            //3 lines
            // MessageBox.Show("test", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            while ((line = info.ReadLine()) != null)
            {
                //MessageBox.Show(line, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //if (line.Contains("Toolkit version:"))
                Toolkit_version = line.Split(':')[0].Trim();
            }
            info.Close();
            return Toolkit_version;
        }

        public string AssesConflict(Files filed, DLCPackageData datas, string Fauthor, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string Vocalss, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist, string dbpathh, List<string> clist, List<string> dlist, bool newold, string Is_Original)
        {
            //rtxt_StatisticsOnReadDLCs = chbx_Additional_Manipualtions.SelectedValue + "\n" + rtxt_StatisticsOnReadDLCs;
            //rtxt_StatisticsOnReadDLCs.Text = "dashes: " + art_hash + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //rtxt_StatisticsOnReadDLCs.Text = "dasheD: " + filed.art_Hash + " - " + filed.audio_Hash + " - " + filed.audioPreview_Hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (chbx_Additional_Manipualtions.GetItemChecked(13) || (chbx_Additional_Manipualtions.GetItemChecked(14) && (tkversion == "" || (tkversion != "" && filed.Is_Original == "Yes"))))
                //"14. Import all as Alternates" 15. Import any Custom as Alternate if an Original exists
                return "Alternate";
            else
            {
                //Duplicates frm = new Duplicates(txt_DBFolder.Text, filed, datas, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);
                //frm.Show();
                DuplicatesManagement frm1 = new DuplicatesManagement(txt_DBFolder.Text, filed, datas, Fauthor, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Vocalss, tunnings, i, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist, txt_TempPath.Text, clist, dlist, newold, Is_Original, txt_RocksmithDLCPath.Text);
                //frm1.Show();
                frm1.ShowDialog();
                //rtxt_StatisticsOnReadDLCs.Text = original_FileName+"-s..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Author != author) author = frm1.Author;
                //rtxt_StatisticsOnReadDLCs.Text = "Setting trasnf vars..." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Description != description) description = frm1.Description;
                //rtxt_StatisticsOnReadDLCs.Text = "0" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Comment != comment) comment = frm1.Comment;
                //rtxt_StatisticsOnReadDLCs.Text = "1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Title != SongDisplayName) SongDisplayName = frm1.Title;
                //rtxt_StatisticsOnReadDLCs.Text = "2" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.Version != tkversion) PackageVersion = frm1.Version;
                //rtxt_StatisticsOnReadDLCs.Text = "\n" + tkversion+"-----"+ author + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (frm1.DLCID != Namee) Namee = frm1.DLCID;
                if (frm1.Is_Alternate != Is_Alternate) Is_Alternate = frm1.Is_Alternate;
                if (frm1.Title_Sort != Title_Sort) Title_Sort = frm1.Title_Sort;
                if (frm1.Artist != Artist) Artist = frm1.Artist;
                if (frm1.ArtistSort != ArtistSort) ArtistSort = frm1.ArtistSort;
                if (frm1.Album != Album) Album = frm1.Album;
                if (frm1.Alternate_No != Alternate_No) Alternate_No = frm1.Alternate_No;
                if (frm1.AlbumArtPath != AlbumArtPath) AlbumArtPath = frm1.AlbumArtPath;
                if (frm1.Art_Hash != art_hash) art_hash = frm1.Art_Hash;
                //      Is_Alternate
                //      Title_Sort
                //      Album
                //rtxt_StatisticsOnReadDLCs.Text = "4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //frm1.BringToFront();
                //frm1.Focus();
                //this.Enabled = false;
                ////Standardization frm = new Standardization(txt_DBFolder.Text);
                ////frm.Show();
                ////MessageBox.Show("test", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //text += ((DD == filed.Has_DD) ? "" : ("\n10/14+ DD: " + DD + " -> " + filed.Has_DD));
                ////text += "\nOriginal Is Alternate: " + filed.Is_Alternate + (filed.Is_Alternate == "Yes" ? " v. " + filed.Alternate_Version_No : "");
                //text += "\n11/14+ Avail. Instr./Tracks: " + ((Bass == "Yes") ? "B" : "") + ((Rhythm == "Yes") ? "R" : "") + ((Lead == "Yes") ? "L" : "") + ((Combo == "Yes") ? "C" : ""); //((Guitar == "Yes") ? "G" : "") + 
                //text += " -> " + ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : ""); //+ ((filed.Has_Guitar == "Yes") ? "G" : "")
                //text += ((filed.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + filed.art_Hash
                //text += ((filed.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + filed.audio_Hash 
                //text += ((filed.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + filed.audioPreview_Hash

                ////files hash
                //DataSet ds = new DataSet();
                //i = 0;
                //var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
                //string jsonHash = "";
                //bool diffjson = true;
                //string XmlHash = "";
                //var XmlName = "";
                //var XmlUUID = "";
                //var XmlFile = "";
                //var jsonFile = "";
                //bool diff = true;
                //int k = 0;
                //string lastConversionDateTime_cur = "";
                //string lastConversionDateTime_exist = "";
                //string lastConverjsonDateTime_cur = "";
                //string lastConverjsonDateTime_exist = "";
                ////MessageBox.Show(DB_Path);
                //try
                //{
                //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                //    {
                //        OleDbDataAdapter daa = new OleDbDataAdapter("SELECT * FROM Arrangements WHERE CDLC_ID=\"" + filed.ID.ToString() + "\";", cnn);
                //        //as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                //        //MessageBox.Show("0");
                //        daa.Fill(ds, "Arrangements");
                //        var noOfRec = 0;
                //        //MessageBox.Show("0.1");
                //        noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                //        rtxt_StatisticsOnReadDLCs.Text = noOfRec + "Assesment Arrangement hash file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        //MessageBox.Show("1");
                //        if (noOfRec > 0)
                //        {
                //            //MessageBox.Show("1");
                //            foreach (var arg in datas.Arrangements)
                //            {
                //                diff = true; diffjson = true;
                //                lastConversionDateTime_cur = "";
                //                lastConversionDateTime_exist = "";
                //                for (i = 0; i <= noOfRec - 1; i++)
                //                {
                //                    //MessageBox.Show(noOfRec.ToString());
                //                    //rtxt_StatisticsOnReadDLCs.Text = alist[i]+"-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    XmlHash = ds.Tables[0].Rows[i].ItemArray[6].ToString(); // XmlHash                                  
                //                    XmlName = ds.Tables[0].Rows[i].ItemArray[17].ToString() + ds.Tables[0].Rows[i].ItemArray[25].ToString(); //type+routemask+
                //                    XmlUUID = ds.Tables[0].Rows[i].ItemArray[28].ToString(); //xml.uuid
                //                    XmlFile = ds.Tables[0].Rows[i].ItemArray[5].ToString(); //xml.filepath
                //                    jsonFile = ds.Tables[0].Rows[i].ItemArray[4].ToString(); //json.filepath
                //                    jsonHash = ds.Tables[0].Rows[i].ItemArray[38].ToString(); // XmlHash      
                //                    arg.SongFile.File = (arg.SongXml.File.Replace(".xml", ".json")).Replace("EOF", "Toolkit");
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-"+XmlName + "=" + (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongXml.File + "=" + XmlFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    //rtxt_StatisticsOnReadDLCs.Text = "-" + arg.SongFile.File + "==" + jsonFile + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    if (XmlName == (arg.ArrangementType.ToString() + arg.RouteMask.ToString()) || (XmlUUID == arg.SongXml.UUID.ToString()))
                //                    // rtxt_StatisticsOnReadDLCs.Text = "-" + XmlHash + "=" + alist[k] + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                    {
                //                        if (XmlHash == alist[k])
                //                            diff = false;
                //                        else
                //                        {
                //                            lastConversionDateTime_cur = GetTExtFromFile(arg.SongXml.File);
                //                            lastConversionDateTime_exist = GetTExtFromFile(XmlFile);
                //                        }
                //                        if (jsonHash == blist[k])
                //                            diffjson = false;
                //                        else
                //                        {
                //                            lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File);
                //                            lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile);
                //                        }
                //                    }
                //                }
                //                text += ((diff) ? "\n" + (14 + i) + "/14+Diff XML" + arg.ArrangementType + arg.RouteMask + ": " + lastConversionDateTime_cur + "->" + lastConversionDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                //                text += ((diffjson) ? "\n" + (15 + i) + "/14+Diff Json" + arg.ArrangementType + arg.RouteMask + ": " + lastConverjsonDateTime_cur + "->" + lastConverjsonDateTime_exist + ": Yes" : "");//+ art_hash + "->" + filed.art_Hash
                //                k++;
                //            }

                //        }
                //    }
                //}
                //catch (System.IO.FileNotFoundException ee)                
                //{
                //    rtxt_StatisticsOnReadDLCs.Text = "Error at last conversion" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    Console.WriteLine(ee.Message);
                //}

                ////files size//files dates
                //DialogResult result1 = MessageBox.Show(text + "\n\nChose:\n\n1. Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //if (result1 == DialogResult.Yes) return "Update";
                //else if (result1 == DialogResult.No) return "Alternate";
                //else return "ignore";//if (result1 == DialogResult.Cancel) 
                //return "Alternate";
                rtxt_StatisticsOnReadDLCs.Text = "REturing from child.." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var tst = "Ignore";
                if (frm1.Asses != "") tst = frm1.Asses;
                frm1.Dispose();
                rtxt_StatisticsOnReadDLCs.Text = "REturing.. to import" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                return tst;

            }
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

        public void btn_RePack_Click(object sender, EventArgs e)
        {
            //if (Path.GetFileName(dlcSavePath).Contains(" ") && rbtn_PS3.Checked)
            //    if (!ConfigRepository.Instance().GetBoolean("creator_ps3pkgnamewarn"))
            //    {
            //        MessageBox.Show(String.Format("PS3 package name can't support space character due to encryption limitation. {0} Spaces will be automatic removed for your PS3 package name.", Environment.NewLine), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //    else
            //    {
            //        ConfigRepository.Instance()["creator_ps3pkgnamewarn"] = true.ToString();
            //    }
            //rtxt_StatisticsOnReadDLCs.Text = "gen r: " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (!bwRGenerate.IsBusy) //&& data != null&& norows > 0
            {
                bwRGenerate.RunWorkerAsync(data);
                //rtxt_StatisticsOnReadDLCs.Text = " not buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }
            else
            {
                //bcapirtxt_StatisticsOnReadDLCs.Text = " Buzy : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            }
            //rtxt_StatisticsOnReadDLCs.Text = "Repack done"+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
        }

        public void GeneratePackage(object sender, DoWorkEventArgs e)
        {
            var cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            //else if (rbtn_Population_All.Checked) ;
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            cmd += " ORDER BY Artist";
            //Read from DB
            norows = 0;
            norows = SQLAccess(cmd);
            //bcapirtxt_StatisticsOnReadDLCs.Text = "Processing &Repackaging for " + norows + " " + cmd + "\n \n" + rtxt_StatisticsOnReadDLCs.Text;

            var i = 0;
            var artist = "";
            //var cmd = "";
            //rtxt_StatisticsOnReadDLCs.Text = "Repack backgroundworker.."+ norows +  rtxt_StatisticsOnReadDLCs.Text;
            foreach (var file in files)
            {
                if (i == norows)
                    break;
                //bcapirtxt_StatisticsOnReadDLCs.Text = "...Pack" + i + " " + file.Artist + " " + file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;// UNPACK
                if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Additional_Manipualtions.GetItemChecked(7))) //"8. Don't repack Broken songs")
                {
                    //var unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                    //MessageBox.Show(file.Artist+file.Song_Title);
                    var packagePlatform = file.Folder_Name.GetPlatform();
                    // REORGANIZE
                    //rtxt_StatisticsOnReadDLCs.Text = "...0.1.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                    //if (structured)
                    //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);
                    // LOAD DATA
                    //rtxt_StatisticsOnReadDLCs.Text = "...0.5.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);
                    //rtxt_StatisticsOnReadDLCs.Text = "...1.."+ file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    var bassRemoved = "No";
                    var DDAdded = "No";

                    var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", SearchOption.AllDirectories);
                    var platform = file.Folder_Name.GetPlatform();
                    if (chbx_Additional_Manipualtions.GetItemChecked(3) || chbx_Additional_Manipualtions.GetItemChecked(5) || chbx_Additional_Manipualtions.GetItemChecked(12) || chbx_Additional_Manipualtions.GetItemChecked(26))
                    {
                        foreach (var xml in xmlFiles)
                        {
                            if (chbx_Additional_Manipualtions.GetItemChecked(12) || chbx_Additional_Manipualtions.GetItemChecked(26))
                                //ADD DD
                                if (
                                    (//chbx_Additional_Manipualtions.GetItemChecked(12)
                            false && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                    && ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm")) && file.Has_DD == "No") || (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && file.Has_BassDD == "No")
                                    )
                                    || //chbx_Additional_Manipualtions.GetItemChecked(26)
                            (false && (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm"))
                                    && file.Has_DD == "No" && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                                    )
                                   )
                                {
                                    File.Copy(xml, xml + ".old", true);
                                    string json = "";
                                    if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized
                                        json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
                                    else
                                        json = (xml.Replace(".xml", ".json").Replace("\\songs\\arr\\", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", SearchOption.AllDirectories)[0])));

                                    File.Copy(json, json + ".old", true);
                                    //bcapirtxt_StatisticsOnReadDLCs.Text = chbx_Additional_Manipualtions.GetItemChecked(12).ToString()+ chbx_Additional_Manipualtions.GetItemChecked(26).ToString()+"...." + Path.GetFileNameWithoutExtension(xml) + "...Adding DD using DDC tool" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    var startInfo = new ProcessStartInfo();

                                    var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_5_max_levels.xml"));
                                    var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_keep_all_levels.xml"));
                                    startInfo.FileName = Path.Combine(AppWD, "ddc", "ddc.exe");

                                    if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized
                                        startInfo.WorkingDirectory = file.Folder_Name + "\\EOF\\";// Path.GetDirectoryName();
                                    else
                                        startInfo.WorkingDirectory = file.Folder_Name + "\\songs\\arr\\";// Path.GetDirectoryName();

                                    startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2}{3}{4}{5}",
                                                                        Path.GetFileName(xml),
                                                                        2, "N", r, c,
                                                                            " -p Y", " -t N");
                                    //rtxt_StatisticsOnReadDLCs.Text = "working dir: "+ startInfo.WorkingDirectory + "...\n--"+startInfo.FileName+"..." +startInfo.Arguments + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    startInfo.UseShellExecute = false;
                                    startInfo.CreateNoWindow = true;
                                    startInfo.RedirectStandardOutput = true;
                                    startInfo.RedirectStandardError = true;

                                    using (var DDC = new Process())
                                    {
                                        // rtxt_StatisticsOnReadDLCs.Text = "...1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                        DDC.StartInfo = startInfo;
                                        DDC.Start();
                                        //consoleOutput = DDC.StandardOutput.ReadToEnd();
                                        //consoleOutput += DDC.StandardError.ReadToEnd();
                                        DDC.WaitForExit(1000 * 60 * 15); //wait 15 minutes
                                                                         // if (DDC.ExitCode > 0 ) rtxt_StatisticsOnReadDLCs.Text = "Issues when adding DD !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        DDAdded = "Yes";
                                        file.Has_BassDD = "Yes";
                                    }
                                }

                            //REMOVE DD
                            //rtxt_StatisticsOnReadDLCs.Text = "...=.." + xml + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(5))
                                || ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm"))
                                && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(3)))
                            // continue;
                            {
                                if (chbx_Additional_Manipualtions.GetItemChecked(5) && !chbx_Additional_Manipualtions.GetItemChecked(3) && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass")) continue;

                                var jsons = "";
                                if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized
                                    jsons = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
                                else
                                    jsons = (xml.Replace(".xml", ".json").Replace("\\songs\\arr\\", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", SearchOption.AllDirectories)[0])));

                                //Save a copy
                                File.Copy(xml, xml + ".old", true);
                                var json = jsons;
                                File.Copy(json, json + ".old", true);

                                //bcapirtxt_StatisticsOnReadDLCs.Text = "...."+Path.GetFileNameWithoutExtension(xml)+"...Removing DD using DDC tool" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                var startInfo = new ProcessStartInfo();

                                var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_dd_remover.xml"));
                                var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
                                startInfo.FileName = Path.Combine(AppWD, "ddc", "ddc.exe");
                                startInfo.WorkingDirectory = file.Folder_Name + jsons;// "\\EOF\\";// Path.GetDirectoryName();
                                startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2}{3}{4}{5}",
                                                                    Path.GetFileName(xml),
                                                                    40, "N", r, c,
                                                                     " -p Y", " -t N"
                                );
                                //rtxt_StatisticsOnReadDLCs.Text = "working dir: "+ startInfo.WorkingDirectory + "...\n--"+startInfo.FileName+"..." +startInfo.Arguments + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                startInfo.UseShellExecute = false;
                                startInfo.CreateNoWindow = true;
                                startInfo.RedirectStandardOutput = true;
                                startInfo.RedirectStandardError = true;
                                var DDCExitCode = 5;
                                using (var DDC = new Process())
                                {
                                    // rtxt_StatisticsOnReadDLCs.Text = "...1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    if (!chbx_Additional_Manipualtions.GetItemChecked(31))
                                    {
                                        DDC.StartInfo = startInfo;
                                        DDC.Start();
                                        //consoleOutput = DDC.StandardOutput.ReadToEnd();
                                        //consoleOutput += DDC.StandardError.ReadToEnd();
                                        DDC.WaitForExit(1000 * 60 * 15); //wait 15 minutes
                                                                         //rtxt_StatisticsOnReadDLCs.Text = "HAS BASS=" + file.Has_BassDD + "...DDEXIT CODE: " + DDC.ExitCode + "----+-" + file.Folder_Name + "++++" + platform.version + "___" + RocksmithToolkitLib.GameVersion.RS2014 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    else DDCExitCode = 5;
                                    if (DDC.ExitCode > 0 && file.Is_Original == "No") rtxt_StatisticsOnReadDLCs.Text = "Issues at CDLC Bass DD removal!" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                                    if (file.Is_Original == "Yes" || DDCExitCode == 5)
                                    { //http://code.google.com/p/rocksmith-custom-song-creator/issues/detail?id=60
                                      //if (platform.version == RocksmithToolkitLib.GameVersion.RS2014)                                        
                                      //{
                                      //bcapirtxt_StatisticsOnReadDLCs.Text = "...Removing DD from Original using own logic" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                      // xml = Directory.GetFiles(unpackedDir, String.Format("*{0}.json", Path.GetFileNameWithoutExtension(json)), SearchOption.AllDirectories);
                                        if (xml.Length > 0)
                                        {
                                            platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                                            Song2014 xmlContent1 = Song2014.LoadFromFile(xml);
                                            var manifestFunctions1 = new ManifestFunctions(platform.version);
                                            var j = manifestFunctions1.GetMaxDifficulty(xmlContent1);
                                            string textfile = File.ReadAllText(xml);

                                            //for each timestamp in the xml file take the highest level entry
                                            var fxml = File.OpenText(xml);
                                            string tecst = "";
                                            string line;
                                            var header = "";
                                            var footer = "";
                                            //Read and Save Header
                                            while ((line = fxml.ReadLine()) != null)
                                            {
                                                header += line + "\n";
                                                if (line.Contains("<levels>")) break;
                                            }
                                            //level the maxdiff overall setting in the xml
                                            var m = 1;
                                            for (m = 1; m <= j; m++)
                                            {
                                                header = header.Replace("maxDifficulty=\"" + m + "\"", "maxDifficulty=\"0\"");
                                            }

                                            //rtxt_StatisticsOnReadDLCs.Text = "head done"+ header.Length + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            //fxml.Close();

                                            //fxml = File.OpenText(xml);
                                            var v = 0; //difficulty level in the parsing
                                            var diff = 0;
                                            float[] timea = new float[10000]; //keeps the timestamp of each note
                                            float[] timeb = new float[10000]; //keeps the timestamp of each anchor
                                            string[] notes = new string[10000]; // keeps the full note details
                                            string[] anchor = new string[10000]; // keeps the full note details
                                            int[] lvla = new int[10000]; //keeps the level of the note&timestamp
                                            int[] lvlb = new int[10000]; //keeps the level of the note&timestamp
                                            //bool is_header = true; //to know when the header has been read and saved
                                            //var l = 0; //storage counter in the array
                                            float ts = 0; //timestamp parsed fro the <notes line
                                            int ea = 0; //top end of the storage array notes
                                            int eb = 0; //top end of the storage array anchor
                                            bool UpdateT = false;
                                            while ((line = fxml.ReadLine()) != null)
                                            {
                                                //header
                                                //if (is_header) header = line + "\n";
                                                if (line.Contains("<level difficulty=\""))
                                                {
                                                    line = line.Replace("<level difficulty=\"", "").TrimStart();
                                                    line = line.Replace("\">", "");
                                                    try { diff = line.ToInt32(); } catch { MessageBox.Show("Errors at DD lvl READ removal"); rtxt_StatisticsOnReadDLCs.Text = "Errors at DD READ removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                                    if (line != v.ToString())
                                                    {
                                                        MessageBox.Show("Errors at DD removal");
                                                        //rtxt_StatisticsOnReadDLCs.Text = "Errors at DD removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                                        
                                                        break;
                                                    }
                                                    v++;
                                                    //  is_header = false;
                                                    //rtxt_StatisticsOnReadDLCs.Text = "level: " + v + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                    continue;
                                                }

                                                //notes
                                                if (line.Contains("<note time=\""))
                                                {
                                                    tecst = (line.Replace("<note time=\"", "")).TrimStart();// ((line.Replace("<note time=\"", "")).TrimStart).IndexOf("\"\"")));
                                                    tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                                    try { ts = Convert.ToSingle(tecst); } catch { MessageBox.Show("Errors at DD time notes READ removal"); rtxt_StatisticsOnReadDLCs.Text = "Errors at DD time removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                                    //rtxt_StatisticsOnReadDLCs.Text = "timesptamp: " + tecst + "-" + ts + "-" + v + "-" + ea + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                    UpdateT = false;
                                                    for (m = 0; m < ea; m++)
                                                    {
                                                        //if (tecst == "12.034") rtxt_StatisticsOnReadDLCs.Text = "time: " + m + "-" + timea[m] + ea + "-" + line + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                        if (ts == timea[m])
                                                        {
                                                            if (v > lvla[m])
                                                            {
                                                                notes[m] = line;
                                                                timea[m] = ts;
                                                                lvla[m] = v;
                                                                UpdateT = true;
                                                            }
                                                            break;
                                                        }
                                                        //else if (time[v]<1) time[v]=
                                                    }
                                                    if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                                    {
                                                        notes[ea] = line;
                                                        timea[ea] = ts;
                                                        lvla[ea] = v;
                                                        ea++;
                                                    }
                                                }
                                                //anchor
                                                if (line.Contains("<anchor time=\""))
                                                {
                                                    tecst = (line.Replace("<anchor time=\"", "")).TrimStart();// ((line.Replace("<note time=\"", "")).TrimStart).IndexOf("\"\"")));
                                                    tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                                    try { ts = Convert.ToSingle(tecst); } catch { MessageBox.Show("Errors at DD time anchor READ removal"); rtxt_StatisticsOnReadDLCs.Text = "Errors at DD anchor removal" + "\n" + rtxt_StatisticsOnReadDLCs.Text; }
                                                    //rtxt_StatisticsOnReadDLCs.Text = "timesptamp: " + tecst + "-" + ts + "-" + v + "-" + ea + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                    UpdateT = false;
                                                    for (m = 0; m < eb; m++)
                                                    {
                                                        //rtxt_StatisticsOnReadDLCs.Text = "time: " + m+"-"+time[m] + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                                        if (ts == timeb[m])
                                                        {
                                                            if (v > lvlb[m])
                                                            {
                                                                anchor[m] = line;
                                                                timeb[m] = ts;
                                                                lvlb[m] = v;
                                                                UpdateT = true;
                                                            }
                                                            break;
                                                        }
                                                        //else if (time[v]<1) time[v]=
                                                    }
                                                    if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                                    {
                                                        anchor[eb] = line;
                                                        timeb[eb] = ts;
                                                        lvlb[eb] = v;
                                                        eb++;
                                                    }
                                                }
                                                //if () ;
                                                //"<note time=\"";
                                                if (line.Contains("<notes>")) continue;
                                                //anchor

                                            }

                                            //rtxt_StatisticsOnReadDLCs.Text = "content: " + ea + "-" +eb+"="+ v + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            //reorder the storage array
                                            var n = 0;
                                            string no;
                                            int lv;
                                            float ti;
                                            for (m = 0; m <= ea - 1; m++)
                                            {
                                                for (n = m + 1; n <= ea; n++)
                                                {
                                                    if (timea[m] > timea[n]) //if TimeStamp is bigger reverse the order
                                                    {
                                                        no = notes[n];
                                                        ti = timea[n];
                                                        lv = lvla[n];
                                                        notes[n] = notes[m];
                                                        timea[n] = timea[m];
                                                        lvla[n] = lvla[m];
                                                        notes[m] = no;
                                                        timea[m] = ti;
                                                        lvla[m] = lv;
                                                    }
                                                }
                                            }
                                            //reorder the anchor storage array
                                            for (m = 0; m <= eb - 1; m++)
                                            {
                                                for (n = m + 1; n <= eb; n++)
                                                {
                                                    if (timeb[m] > timeb[n]) //if TimeStamp is bigger reverse the order
                                                    {
                                                        no = anchor[n];
                                                        ti = timeb[n];
                                                        lv = lvlb[n];
                                                        anchor[n] = anchor[m];
                                                        timeb[n] = timeb[m];
                                                        lvlb[n] = lvlb[m];
                                                        anchor[m] = no;
                                                        timeb[m] = ti;
                                                        lvlb[m] = lv;
                                                    }
                                                }
                                            }
                                            //add level & notes to the footer
                                            footer += "    <level difficulty=\"0\">" + "\n" + "      <notes>" + "\n";
                                            for (m = 0; m <= ea; m++)
                                            {
                                                footer += notes[m] + "\n";
                                            }
                                            footer += "	  </notes>\n      <chords />\n      <anchors>\n";
                                            //add level & notes to the footer
                                            for (m = 0; m <= eb; m++)
                                            {
                                                footer += anchor[m] + "\n";
                                            }
                                            footer += "      </anchors>" + "\n" + "      <handShapes />" + "\n" + "     </level>" + "\n" + "   </levels>" + "\n" + "</song>";
                                            //rtxt_StatisticsOnReadDLCs.Text = "Saving..." + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            fxml.Close();
                                            File.WriteAllText(xml, header + footer);

                                            //textfile = textfile.Replace("<heroLevels>", "");
                                            //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"1\" />", "");
                                            //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"2\" />", "");
                                            //textfile = textfile.Replace("<heroLevel difficulty=\"0\" hero=\"3\" />", "");
                                            //textfile = textfile.Replace("</heroLevels>", "");
                                            //textfile = textfile.Replace("<level difficulty=\""+0+"\">", "<level difficulty = \""+(j+1)+"\">");
                                            //textfile = textfile.Replace("<level difficulty=\""+j+"\">", "<level difficulty=\"0\">");



                                            //level the json as well
                                            //var json = (xml.Replace("EOF", "Toolkit")).Replace(".xml", ".json");
                                            textfile = File.ReadAllText(json);
                                            n = 0;
                                            for (n = 0; n < j; n++)
                                            {
                                                textfile = textfile.Replace("\"MaxPhraseDifficulty\": " + n + ",", "\"MaxPhraseDifficulty\": 0,");
                                            }
                                            File.WriteAllText(json, textfile);
                                            //rtxt_StatisticsOnReadDLCs.Text = "...DD changes written to file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                            //}
                                        }
                                    }
                                    file.Has_BassDD = "No";
                                    bassRemoved = "Yes";
                                    //rtxt_StatisticsOnReadDLCs.Text = "something..." + "...\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }

                                //remove altough original or t0o old dd
                                platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                                Song2014 xmlContent = Song2014.LoadFromFile(xml);
                                var manifestFunctions = new ManifestFunctions(platform.version);
                                //manifestFunctions.GetMaxDifficulty(xmlContent) = "0";

                            }
                            //rtxt_StatisticsOnReadDLCs.Text = "...°.." + xmlFiles.Length + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = "ooooo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(17)) //18.Repack with Artist/ Title same as Artist / Title Sort
                    {
                        file.Artist_Sort = file.Artist;
                        file.Song_Title_Sort = file.Song_Title;
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = "ggggoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(23) && file.Artist_Sort.Length > 4) //24.Pack with The/ Die only at the end of Title Sort 
                    {
                        //rtxt_StatisticsOnReadDLCs.Text = "1eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (chbx_Additional_Manipualtions.GetItemChecked(21) && file.Song_Title_Sort.Length > 4)
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "2eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "The " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "Die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "the " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "THE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                            file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "DIE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                        }
                        //rtxt_StatisticsOnReadDLCs.Text = file.Artist_Sort + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "The " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "Die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "the " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "THE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                        file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "DIE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = "4eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    data = new DLCPackageData
                    {
                        GameVersion = GameVersion.RS2014,
                        Pc = true,
                        Mac = chbx_Mac.Checked,
                        XBox360 = chbx_XBOX360.Checked,
                        PS3 = chbx_PS3.Checked,
                        Name = file.DLC_Name,
                        AppId = file.DLC_AppID,

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
                        Volume = file.Volume.ToInt32(),
                        PreviewVolume = file.Preview_Volume.ToInt32(),
                        SignatureType = info.SignatureType,
                        PackageVersion = file.Version
                    };
                    //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+" test"+i+ data.SongInfo.Artist + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var norm_path = txt_TempPath.Text + "\\" + ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                    //rtxt_StatisticsOnReadDLCs.Text = "8"+data.PackageVersion+"...manipul" + norm_path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //manipulating the info
                    if (cbx_Activ_Title.Checked)
                        data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i, false);
                    // rtxt_StatisticsOnReadDLCs.Text = "...manipul: "+ file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (cbx_Activ_Title_Sort.Checked)
                        data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i, false);
                    //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (cbx_Activ_Artist.Checked)
                        data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i, false);
                    //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (cbx_Activ_Artist_Sort.Checked)
                        data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i, false);
                    //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (cbx_Activ_Album.Checked)
                        data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i, false);
                    //rtxt_StatisticsOnReadDLCs.Text = "...3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    //rtxt_StatisticsOnReadDLCs.Text = "...nipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(0)) //"1. Add Increment to all Titles"
                        data.Name = i + data.Name;

                    //rtxt_StatisticsOnReadDLCs.Text = "...mpl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    artist = "";
                    if (chbx_Additional_Manipualtions.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
                    {
                        if (i > 0)
                            if (data.SongInfo.Artist == files[i - 1].Artist) no_ord += 1;
                            else no_ord = 1;
                        else no_ord += 1;
                        artist = no_ord + " ";
                        data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;
                    }

                    //if (chbx_Additional_Manipualtions.GetItemChecked(7)) //"8. Don't repack Broken songs"
                    //    if (file.Is_Broken == "Yes") break;
                    //rtxt_StatisticsOnReadDLCs.Text = "...4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //rtxt_StatisticsOnReadDLCs.Text = "...manipl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(2)) //"3. Make all DLC IDs unique (&save)"
                        if (file.UniqueDLCName != null) data.Name = file.UniqueDLCName;
                        else
                        {
                            Random random = new Random();
                            data.Name = random.Next(0, 100000) + data.Name;
                            var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                            {
                                DataSet dis = new DataSet();
                                cmd += "UPDATE Main SET UniqueDLCName=" + data.Name + " WHERE ID=" + file.ID;
                                OleDbDataAdapter das = new OleDbDataAdapter(cmd, cnn);
                                das.Fill(dis, "Main");
                                das.Dispose();
                            }
                        }

                    //Fix the _preview_preview issue
                    var ms = data.OggPath; //var audiopath = ""; var audioprevpath = "";
                    var tst = "";
                    //MessageBox.Show("One or more");
                    //rtxt_StatisticsOnReadDLCs.Text = "maybe fixing .."+ file.Folder_Name+"\n"+ norm_path + "\n"+ rtxt_StatisticsOnReadDLCs.Text;
                    try
                    {
                        var sourceAudioFiles = Directory.GetFiles(file.Folder_Name, "*.wem", SearchOption.AllDirectories);
                        //if (sourceAudioFiles.Length>0)
                        //var targetAudioFiles = new List<string>();

                        foreach (var fil in sourceAudioFiles)
                        {
                            tst = fil;
                            //MessageBox.Show("test2.02 " + fil);
                            //rtxt_StatisticsOnReadDLCs.Text = "thinking about fixing _preview_preview issue.." + norm_path +"-"+tst+ "\n"+rtxt_StatisticsOnReadDLCs.Text;
                            if (fil.LastIndexOf("_preview_preview.wem") > 0)
                            {
                                ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                                File.Move((ms + "_preview.wem"), (ms + ".wem"));
                                File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                                //bcapirtxt_StatisticsOnReadDLCs.Text = "fixing _preview_preview issue..." + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "FAILED6-" + ee.Message + tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        Console.WriteLine(ee.Message);
                    }
                    if (data == null)
                    {
                        MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //rtxt_StatisticsOnReadDLCs.Text = "...5" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //dlcSavePath = ds.Tables[0].Rows[i].ItemArray[1].ToString() + "\\";// + ((info.PackageVersion == null) ? "Original" : "CDLC") + "-" + info.SongInfo.SongYear +".psarc";
                    //var dlcSavePath = GeneralExtensions.GetShortName("{0}_{1}_v{2}", (((file.Version == null) ? "Original" : "CDLC") + "_" + info.SongInfo.SongDisplayName), (info.SongInfo.SongDisplayName + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongYear), info.PackageVersion, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));
                    var FN = "";
                    //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(txt_File_Name.Text, i, true);
                    else FN = GeneralExtensions.GetShortName("{0}-{1}-v{2}", ("def" + ((file.Version == null) ? "ORIG" : "CDLC") + "_" + file.Artist), (file.Album_Year.ToInt32() + "_" + file.Album + "_" + file.Song_Title), file.Version, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));//((data.PackageVersion == null) ? "Original" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                    if (file.Is_Alternate == "Yes") FN += "a." + file.Alternate_Version_No + file.Author;

                    //rtxt_StatisticsOnReadDLCs.Text = "fn: " + FN + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_Additional_Manipualtions.GetItemChecked(8) || chbx_PS3.Checked)
                    {
                        FN = FN.Replace(".", "_");
                        FN = FN.Replace(" ", "_");
                    }

                    dlcSavePath = txt_TempPath.Text + "\\" + FN;
                    //rtxt_StatisticsOnReadDLCs.Text = "rez : " + dlcSavePath + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //if (GameVersion.RS2014 == GameVersion.RS2012) //old code
                    //{
                    //    try
                    //    {
                    //        OggFile.VerifyHeaders(data.OggPath);
                    //    }
                    //    catch (InvalidDataException ex)
                    //    {
                    //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                    //rtxt_StatisticsOnReadDLCs.Text = "genf : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                    int progress = 0;
                    errorsFound = new StringBuilder();
                    var numPlatforms = 0;
                    //if (platformPC.Checked)
                    numPlatforms++;
                    if (chbx_Mac.Checked)
                        numPlatforms++;
                    if (chbx_XBOX360.Checked)
                        numPlatforms++;
                    if (chbx_PS3.Checked)
                        numPlatforms++;

                    var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                    // rtxt_StatisticsOnReadDLCs.Text = "...6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    if (chbx_PC.Checked)
                        try
                        {
                            bwRGenerate.ReportProgress(progress, "Generating PC package");
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "1pc..." + rtxt_StatisticsOnReadDLCs.Text;
                            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, CurrentGameVersion));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "2pc..." + rtxt_StatisticsOnReadDLCs.Text;
                            progress += step;
                            bwRGenerate.ReportProgress(progress);
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "3pc..." + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        catch (Exception ex)
                        {
                            errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "...0pc ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                        }

                    if (chbx_Mac.Checked)
                        try
                        {
                            bwRGenerate.ReportProgress(progress, "Generating Mac package");
                            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, CurrentGameVersion));
                            progress += step;
                            bwRGenerate.ReportProgress(progress);
                        }
                        catch (Exception ex)
                        {
                            errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "...0mac ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                        }

                    if (chbx_XBOX360.Checked)
                        try
                        {
                            bwRGenerate.ReportProgress(progress, "Generating XBox 360 package");
                            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, CurrentGameVersion));
                            progress += step;
                            bwRGenerate.ReportProgress(progress);
                        }
                        catch (Exception ex)
                        {
                            errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "...0mac ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                        }

                    if (chbx_PS3.Checked)
                        try
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "ps3...start..." + rtxt_StatisticsOnReadDLCs.Text;
                            bwRGenerate.ReportProgress(progress, "Generating PS3 package");
                            //rtxt_StatisticsOnReadDLCs.Text = dlcSavePath + rtxt_StatisticsOnReadDLCs.Text;
                            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, CurrentGameVersion));
                            //progress += step;
                            //bwRGenerate.ReportProgress(progress);
                            // rtxt_StatisticsOnReadDLCs.Text = "ps3...off..." + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        catch (Exception ex)
                        {
                            errorsFound.AppendLine(String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "...0Ps3 ERROR..."+ dlcSavePath+"---"+ dlcSavePath.Length+ "----" + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    data.CleanCache();
                    //rtxt_StatisticsOnReadDLCs.Text = "gen2 : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    i++;
                    //rtxt_StatisticsOnReadDLCs.Text = "gen r: " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    //TO DO DELETE the ORIGINAL IMPORTED FILES or not
                    //bcapirtxt_StatisticsOnReadDLCs.Text = "\nRepack bkworkerdone.." + i + rtxt_StatisticsOnReadDLCs.Text;

                    //Restore the DDremoved copies
                    xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml.old", SearchOption.AllDirectories);
                    platform = file.Folder_Name.GetPlatform();
                    if (chbx_Additional_Manipualtions.GetItemChecked(5) || chbx_Additional_Manipualtions.GetItemChecked(3))
                    {
                        if (bassRemoved == "Yes") file.Has_BassDD = "Yes";
                        foreach (var xml in xmlFiles)
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "...=.." + xml + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            if ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && file.Has_BassDD == "Yes" && xml.ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(5))
                                || ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rhythm") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo"))
                                    && file.Has_Guitar == "Yes" && xml.ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(3)))
                            // continue;
                            {
                                if (chbx_Additional_Manipualtions.GetItemChecked(5) && !chbx_Additional_Manipualtions.GetItemChecked(3) && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass")) continue;
                                //Save a copy
                                File.Copy(xml.Replace(".old", ""), xml.Replace(".old", ".woDD"), true);
                                File.Copy(xml, xml.Replace(".old", ""), true);
                                var json = "";
                                if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                 
                                    json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
                                else
                                    json = xml.Replace("songs\\arr", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", SearchOption.AllDirectories)[0])).Replace(".xml", ".json");

                                File.Copy(json.Replace(".old", ""), json.Replace(".old", ".woDD"), true);
                                File.Copy(json, json.Replace(".old", ""), true);
                            }
                        }
                    }
                }
                //MessageBox.Show("tst");
            }
            //bcapirtxt_StatisticsOnReadDLCs.Text = "\n...Repack done.." + rtxt_StatisticsOnReadDLCs.Text;
            MessageBox.Show("Repack done");
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "process changed" + rtxt_StatisticsOnReadDLCs.Text;
            if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
                pB_ReadDLCs.Value = e.ProgressPercentage;
            else
                pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;

            ShowCurrentOperation(e.UserState as string);
        }
        private void ShowCurrentOperation(string message)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "current" + rtxt_StatisticsOnReadDLCs.Text;
            //currentOperationLabel.Text = message;
            //currentOperationLabel.Refresh();
        }

        private void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "generate" + rtxt_StatisticsOnReadDLCs.Text;
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

            //dlcGenerateButton.Enabled = true;
            //updateProgress.Visible = false;
            //currentOperationLabel.Visible = false;
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
                //cbx_Groups.Items.Clear;
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
            //Show Intro database window
            MainDB frm = new MainDB(txt_DBFolder.Text, txt_TempPath.Text, chbx_Additional_Manipualtions.GetItemChecked(33), txt_RocksmithDLCPath.Text);
            frm.Show();
        }

        private void btn_Standardization_Click(object sender, EventArgs e)
        {
            var DBb_Path = txt_DBFolder.Text + "\\Files.accdb";
            //MessageBox.Show(DBb_Path);
            Standardization frm = new Standardization(DBb_Path, txt_TempPath.Text, txt_RocksmithDLCPath.Text);
            frm.Show();
        }

        private void chbx_Additional_Manipualtions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Translation_And_Correction(txt_DBFolder.Text);
            MessageBox.Show("Normalization Applied");
        }

        private void chbx_HomeDBG_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "E:\\Users\\bogdan\\Dropbox\\OneDrive\\dlc\\0PC";
            txt_DBFolder.Text = "E:\\Users\\bogdan\\Dropbox\\OneDrive\\dlc";
            txt_TempPath.Text = "E:\\Users\\bogdan\\Dropbox\\OneDrive\\dlc\\0PC\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
        }

        private void chbx_WorkDGB_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "C:\\Users\\8bcapitanoiu\\Dropbox\\OneDrive\\dlc\\0PC";
            txt_DBFolder.Text = "C:\\Users\\8bcapitanoiu\\Dropbox\\OneDrive\\dlc";
            txt_TempPath.Text = "C:\\Users\\8bcapitanoiu\\Dropbox\\OneDrive\\dlc\\0PC\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
        }

        private void mainBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void chbx_HomeDGBVM_CheckedChanged(object sender, EventArgs e)
        {
            txt_RocksmithDLCPath.Text = "Z:\\HFS\\Users\\bogdan\\Dropbox\\OneDrive\\dlc\\0PC";
            txt_DBFolder.Text = "Z:\\HFS\\Users\\bogdan\\Dropbox\\OneDrive\\dlc";
            txt_TempPath.Text = "Z:\\HFS\\Users\\bogdan\\Dropbox\\OneDrive\\dlc\\0PC\\0";
            chbx_CleanTemp.Checked = false;
            chbx_CleanDB.Checked = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            var jsonFiles = Directory.GetFiles(txt_RocksmithDLCPath.Text, "*.psarc.*", SearchOption.AllDirectories);
            var inputFilePath = ""; var locat = ""; var songshsanP=""; var unpackedDir = "";
            var DBb_Path = txt_DBFolder.Text + "\\Files.accdb";
            //string source_dir = "";
            //string destination_dir = "";
            var t = "";
          Platform platformDLC;//
           var platformDLCP = "";
           //Clean ImportDB
           DataSet dss = new DataSet();
           try
           {
               using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
               {
                   if (chbx_CleanCache.Checked)
                   {
                       rtxt_StatisticsOnReadDLCs.Text = "Cleaning....Cache table...." + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                       OleDbDataAdapter dan = new OleDbDataAdapter("DELETE FROM Cache;", cnn);
                       dan.Fill(dss, "Cache");
                       dan.Dispose();
                       rtxt_StatisticsOnReadDLCs.Text = " Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                   }
               }
           }
           catch (System.IO.FileNotFoundException ee)
           {
               // To inform the user and continue is 
               // sufficient for this demonstration. 
               // Your application may require different behavior.
               Console.WriteLine(ee.Message);
               rtxt_StatisticsOnReadDLCs.Text = "Error cleaning Cleaned" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
               //continue;
           }

            foreach (var json in jsonFiles)
            {
                platformDLC = json.GetPlatform(); //Platform 
                    platformDLCP = platformDLC.platform.ToString();
                if (json == txt_RocksmithDLCPath.Text + "\\songs.psarc")
                {
                    inputFilePath = json; locat = "CACHE";
                    t = inputFilePath;
                    if (!chbx_Additional_Manipualtions.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder
                    {
                        try
                        {
                            // UNPACK
                            unpackedDir = Packer.Unpack(txt_RocksmithDLCPath.Text + "\\cache.psarc", txt_TempPath.Text + "\\0_dlcpacks", false, false, false);
                            unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text+ "\\0_dlcpacks", false, false, false);
                            songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                        }
                        catch (Exception ex)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + inputFilePath + "---" + txt_TempPath.Text+ "\\0songs" + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    }
                    else
                    {
                        unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\songs_" + platformDLCP;
                        songshsanP = unpackedDir + "\\manifests\\songs\\songs.hsan";
                    }
                } //repacking at the moment manually with psarc 1.4 and lzma ratio 0
                    else if (json == txt_RocksmithDLCPath.Text + "\\rs1compatibilitydlc.psarc.edat")
                        {
                            inputFilePath = json;
                            locat = "COMPATIBILITY";
                            if (!chbx_Additional_Manipualtions.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder
                            {
                                try // UNPACK
                                {
                                    unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", false, false, false).Replace(".psarc", "");;
                                }
                                catch (Exception ex)
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                            }
                            else unpackedDir = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc_" + platformDLCP;

                            songshsanP = unpackedDir + "\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan";
                            try //rename folder so we can use the read browser function                            
                            {
                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                                renamedir(unpackedDir + "\\manifests\\songs_rs1dlc", unpackedDir + "\\manifests\\songs");

                                //pack again to have BRowser function rading correctly
                                //var info = DLCPackageData.LoadFromFolder(unpackedDir, new Platform(GamePlatform.PS3, CurrentGameVersion));
                                //rtxt_StatisticsOnReadDLCs.Text += info.PS3.ToString();
                                //RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(unpackedDir, info, new Platform(GamePlatform.PS3, CurrentGameVersion));
                                var startInfo = new ProcessStartInfo();
                                //startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                                //startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                //startInfo.Arguments = String.Format(" -p -v RS2014 -f {0} -i {1} -o {2}",
                                //                                    platformDLCP,
                                //                                    unpackedDir,
                                //                                    txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc");// + platformDLCP
                                startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                                startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                                t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
                                startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",
                                                                    t,
                                                                    unpackedDir);// + platformDLCP
                                startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                                if (!File.Exists(t))
                                using (var DDC = new Process())
                                {
                                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                    if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }

                                //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                                renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                                rtxt_StatisticsOnReadDLCs.Text = "renaming internal folder \n" + rtxt_StatisticsOnReadDLCs;
                            }
                            catch (Exception ex)
                            {
                                rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at dir rename" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                else if (json == txt_RocksmithDLCPath.Text + "\\rs1compatibilitydisc.psarc.edat") {
                    inputFilePath = json; locat = "RS1Retail";
                    if (!chbx_Additional_Manipualtions.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/0_Import folder
                    {
                        try // UNPACK
                        {
                            unpackedDir = Packer.Unpack(inputFilePath, txt_TempPath.Text + "\\0_dlcpacks", false, false, false).Replace(".psarc","");
                        }
                        catch (Exception ex)
                        {
                            rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    }
                    else unpackedDir = txt_TempPath.Text +"\\0_dlcpacks\\rs1compatibilitydisc_" + platformDLCP;

                    songshsanP = unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan";
                    try //rename folder so we can use the read browser function                            
                    {
                        //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work
                        renamedir(unpackedDir + "\\manifests\\songs_rs1disc", unpackedDir + "\\manifests\\songs");

                        //pack again to have BRowser function rading correctly
                        //var info = DLCPackageData.LoadFromFolder(unpackedDir, new Platform(GamePlatform.PS3, CurrentGameVersion));
                        //rtxt_StatisticsOnReadDLCs.Text += info.PS3.ToString();
                        //RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(unpackedDir, info, new Platform(GamePlatform.PS3, CurrentGameVersion));
                        var startInfo = new ProcessStartInfo();
                        //startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                        //startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                        //startInfo.Arguments = String.Format(" -p -v RS2014 -f {0} -i {1} -o {2}",
                        //                                    platformDLCP,
                        //                                    unpackedDir,
                        //                                    txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc");// + platformDLCP
                        startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                        startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                        t = txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydisc.psarc";
                        startInfo.Arguments = String.Format(" create --zlib -N -o {0} {1}",                                                            
                                                            t,
                                                            unpackedDir);// + platformDLCP
                        startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                        if (!File.Exists(t))
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1disc pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }

                        //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                        renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1disc");
                        rtxt_StatisticsOnReadDLCs.Text = "renaming internal folder \n" + rtxt_StatisticsOnReadDLCs;
                    }
                    catch (Exception ex)
                    {
                        rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at dir rename" + unpackedDir + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                    }
                } 
                            else continue;
                //var inputFilePath = txt_RocksmithDLCPath.Text + "\\songs.psarc";
                //IList<RocksmithToolkitLib.Song2014ToTab.SongInfoShort> songListShort = null;
                IList<SongInfoShort> songListShort = null;

                Console.WriteLine("Opening archive {0} ...", inputFilePath);
                Console.WriteLine();


                
                //Populate DB
                try
                 {
                   // var t = inputFilePath;//if (!File.Exists(inputFilePath)) 
                    //if (json == txt_RocksmithDLCPath.Text + "\\rs1compatibilitydlc.psarc.edat") inputFilePath= "C:\\GitHub\\rocksmith-custom-song-toolkit\\RocksmithTookitGUI\\bin\\Debug\\edat\\PS3.psarc";
                    var browser = new PsarcBrowser(t);
                    //inputFilePath = t;
                    var songList = browser.GetSongList();
                    var toolkitInfo = browser.GetToolkitInfo();
                    //if (songshsanP.Contains("songs_rs1dlc")) File.Move(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                    foreach (var song in songList)
                    {
                        DataSet dsx = new DataSet();

                        using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                        {
                            OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID from Cache AS O WHERE Identifier=\"" + song.Identifier + "\"", cn);
                            da.Fill(dsx, "Cache");
                            if (dsx.Tables[0].Rows.Count == 0)
                                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBb_Path))
                                {
                                    var commands = cnn.CreateCommand();
                                    commands.CommandText = "INSERT INTO Cache(";
                                    //command.CommandText += "ID, ";
                                    commands.CommandText += "Identifier, ";
                                    commands.CommandText += "Artist, ";
                                    commands.CommandText += "ArtistSort, ";
                                    commands.CommandText += "Album, ";
                                    commands.CommandText += "Title, ";
                                    commands.CommandText += "AlbumYear, ";
                                    commands.CommandText += "Arrangements, ";
                                    commands.CommandText += "Removed, ";
                                    commands.CommandText += "PSARCName, ";
                                    commands.CommandText += "SongsHSANPath, ";
                                    commands.CommandText += "Platform ";
                                    commands.CommandText += ") VALUES (@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param11,@param12,@param13"; ////@param1,
                                    commands.CommandText += ")";

                                    //command.Parameters.AddWithValue("@param1", ID);
                                    commands.Parameters.AddWithValue("@param2", song.Identifier);
                                    commands.Parameters.AddWithValue("@param3", song.Artist);
                                    commands.Parameters.AddWithValue("@param4", song.ArtistSort);
                                    commands.Parameters.AddWithValue("@param5", song.Album);
                                    commands.Parameters.AddWithValue("@param6", song.Title);
                                    commands.Parameters.AddWithValue("@param7", song.Year);
                                    commands.Parameters.AddWithValue("@param8", string.Join(", ", song.Arrangements));
                                    commands.Parameters.AddWithValue("@param9", "Yes");
                                    commands.Parameters.AddWithValue("@param11", locat);
                                    commands.Parameters.AddWithValue("@param12", songshsanP);
                                    commands.Parameters.AddWithValue("@param13", platformDLCP);
                                    //EXECUTE SQL/INSERT
                                    try
                                    {
                                        commands.CommandType = CommandType.Text;
                                        cnn.Open();
                                        commands.ExecuteNonQuery();
                                        rtxt_StatisticsOnReadDLCs.Text = String.Format("[{0}] = {1} - {2}  ({3}, {4})  {{{5}}}", song.Identifier,
                                                                        song.Artist, song.Title, song.Album, song.Year,
                                                                        string.Join(", ", song.Arrangements)) + "\n" + rtxt_StatisticsOnReadDLCs.Text;
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
                                    rtxt_StatisticsOnReadDLCs.Text += Environment.NewLine;
                                }
                        }
                        dsx.Dispose();
                    }

                    //level the maxdiff overall setting in the xml
                    //var m = 1; var j = 0;
                    //for (m = 1; m <= j; m++)
                    //{
                    //    header = header.Replace("maxDifficulty=\"" + m + "\"", "maxDifficulty=\"0\"");
                    //}

                    //foreach (var song in toConvert)
                    //{
                    //    //var score = new Score();
                    //    // get all default or user specified arrangements for the song 
                    //    var arrangements = song.Arrangements;
                    //    Console.WriteLine("Converting song " + song.Identifier + "...");

                    //    foreach (var arr in arrangements)
                    //    {
                    //       ;// var arrangement = browser.GetArrangement(song..Identifier, arr);
                    //        // get maximum difficulty for the arrangement
                    //        var mf = new ManifestFunctions(GameVersion.RS2014);
                    //        ;//int maxDif = mf.GetMaxDifficulty(arrangement);

                    //        //if (allDif) // create seperate file for each difficulty
                    //        //{
                    //        //    for (int difLevel = 0; difLevel <= maxDif; difLevel++)
                    //        //    {
                    //        //        //ExportArrangement(score, arrangement, difLevel, inputFilePath, toolkitInfo);
                    //        //        //Console.WriteLine("Difficulty Level: {0}", difLevel);

                    //        //        //var baseFileName = CleanFileName(
                    //        //        //    String.Format("{0} - {1}", score.Artist, score.Title));
                    //        //        //baseFileName += String.Format(" ({0})", arr);
                    //        //        //baseFileName += String.Format(" (level {0:D2})", difLevel);

                    //        //        //SaveScore(score, baseFileName, outputDir, outputFormat);
                    //        //        //// remember to remove the track from the score again
                    //        //        //score.Tracks.Clear();
                    //        //    }
                    //        //}
                    //        //else // combine maximum difficulty arrangements into one file
                    //        //{
                    //        //    Console.WriteLine(String.Format("Maximum Difficulty Level: {0}", maxDif));
                    //        //    ExportArrangement(score, arrangement, maxDif, inputFilePath, toolkitInfo);
                    //        //}
                    //    }

                    //    //if (!allDif) // only maximum difficulty
                    //    //{
                    //    //    var baseFileName = CleanFileName(
                    //    //        String.Format("{0} - {1}", score.Artist, score.Title));
                    //    //    SaveScore(score, baseFileName, outputDir, outputFormat);
                    //    //}
                    //}

                    //    Console.WriteLine();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error encountered:");
                    Console.WriteLine(ex.Message);
                }
            }
            Cache frm = new Cache(DBb_Path, txt_TempPath.Text, txt_RocksmithDLCPath.Text);
            frm.Show();
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
        public class SongInfoShort
    {
        public string Identifier { get; set; }
        public string Arrangement { get; set; }
    }

private string calc_path(string jsonsFiles)
        {
            var ttt = Path.GetDirectoryName(jsonsFiles);
            var pattth = ttt.IndexOf("\\manifests\\");
            var ddd = ttt.Substring(pattth + 1, ttt.Length - pattth - 1);
            return ddd;
        }


private void renamedir(string source_dir, string destination_dir)
{
    foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))// Create subdirectory structure in destination    
    {
        Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
    }

    Directory.CreateDirectory(destination_dir);
    foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
    {
        File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
    }
    Directory.Delete(source_dir, true);    
}

    }
}