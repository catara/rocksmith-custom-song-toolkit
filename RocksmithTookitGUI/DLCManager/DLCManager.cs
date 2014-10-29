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
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.Xml; //For DD
using RocksmithToolkitLib.DLCPackage.Manifest; //For DD
using System.IO;
using System.Data.OleDb;
using Ookii.Dialogs;
using System.Net;
using System.Reflection;
using System.Security.Cryptography; //For File hash

//For the Export to Excel function
using System.Data.SqlClient;
//using Excel = Microsoft.Office.Interop.Excel;


namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DLCManager : UserControl
    {

        //bcapi
        private const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";
        private bool loading = false;
        public BackgroundWorker bwGenerate = new BackgroundWorker(); //bcapi
        OleDbConnection connection;
        OleDbCommand command;
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
        }
        public Files[] files = new Files[10000];

        public DLCManager()
        {
            InitializeComponent();
            Set_DEBUG();
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
                txt_RocksmithDLCPath.Text = "../../../../tmp";
                txt_DBFolder.Text = "../../../../tmp";
                txt_TempPath.Text = "../../../../tmp\\0";
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
                //txt_RocksmithDLCPath.Text = "";
                //txt_DBFolder.Text = "";
                //txt_TempPath.Text = "";
                //txt_TempPath.Text = "";
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
            lbl_PreviewText.Text = "Title: " + Manipulate_strings(txt_Title.Text, 0);
        }

        private void btn_Preview_Title_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Title: " + Manipulate_strings(txt_Title_Sort.Text, 0);
        }

        private void btn_Preview_Artist_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Artist: " + Manipulate_strings(txt_Artist.Text, 0);
        }

        private void btn_Preview_Artist_Sort_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Sort Artist: " + Manipulate_strings(txt_Artist_Sort.Text, 0);
        }

        private void btn_Preview_Album_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "Album: " + Manipulate_strings(txt_Album.Text, 0);
        }

        private void btn_Preview_File_Name_Click(object sender, EventArgs e)
        {
            lbl_PreviewText.Text = "FileName: " + Manipulate_strings(txt_File_Name.Text, 0);
        }

        public string Manipulate_strings(string words, int k)
        {

            //Read from DB
            int norows = 0;
            //1. Get Random Song ID
            var cmd = "SELECT TOP 1 * FROM Main";
            //Files IDs = new Files();
            norows = SQLAccess(cmd);

            //2. Read from DB
            cmd = "SELECT * FROM Main WHERE ID = " + files[0].ID;
            norows = SQLAccess(cmd);

            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = "";
            var readt = false;
            for (i = 0; i <= txt.Length - 1; i++)
            {
                curtext = txt[i].ToString();//.Substring(i,1);
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
                }

                if (readt == true)
                    curelem += curtext;
                else fulltxt += curtext;

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
                        case "<CDLC>":
                            fulltxt += files[k].DLC;
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
                        case "<Avail. Instr.>":
                            fulltxt += ((files[k].Has_Guitar == "Yes") ? "G" : "") + ((files[k].Has_Bass == "Yes") ? "B" : "");
                            break;
                        case "<Tuning>":
                            fulltxt += files[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            fulltxt += ((files[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((files[k].Has_Bass == "Yes") ? "B" : ""); //not yet done
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
                        case "<DD>":
                            fulltxt += "DD";
                            break;
                        case "<Broken>":
                            fulltxt = ((files[k].Is_Broken == "Yes") ? "<B>" : "") + fulltxt;
                            break;
                        case "<File Name>":
                            fulltxt += files[k].Current_FileName;
                            break;
                        //case ""
                        default:
                            break;
                    }
                }
            }
            rtxt_StatisticsOnReadDLCs.Text = "returning " + i + " char " + fulltxt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return fulltxt;
        }

        //Generic procedure to read and parse Main.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            var DB_Path = txt_DBFolder.Text + "\\Files.accdb";
            //Files[] files = new Files[10000];

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
                        i++;
                    }
                    //Closing Connection
                    dax.Dispose();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in SQLAccess ! " + DB_Path + "-" + cmd);
            }
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
            Set_DEBUG(); //Default value when in dEV/Debug mode, if needed

            var Temp_Path_Import = txt_TempPath.Text + "\\0_import";
            string pathDLC = txt_RocksmithDLCPath.Text;
            if (Directory.Exists(txt_TempPath.Text) != true || Directory.Exists(Temp_Path_Import) != true || (Directory.Exists(pathDLC)) != true)
            {
                MessageBox.Show(String.Format("One of multiple folder missing " + txt_TempPath.Text + ", " + Temp_Path_Import + ", " + pathDLC, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error));
                return;
            }

            //Clean Temp Folder
            if (chbx_CleanTemp.Checked)
            {
                //clean import folder
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Temp_Path_Import);
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    if (dir.Name != "0_import" && dir.Name != "0_old") dir.Delete(true);
                }

                //clean app working folders if in DEBUG mode
                if (chbx_DebugB.Checked) //Delete only if We are in DEBUG Mode
                {
                    System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(txt_TempPath.Text);
                    foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in downloadedMessageInfo2.GetDirectories())
                    {
                        if (dir.Name != "0_import" && dir.Name != "0_old") dir.Delete(true);
                    }
                }
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
            rtxt_StatisticsOnReadDLCs.Text = "Cleaning...." + DB_Path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter daa = new OleDbDataAdapter("DELETE FROM Import;", cnn);
                    daa.Fill(dss, "Import");
                    if (chbx_CleanDB.Checked) //Delete only if We are in DEBUG Mode
                    {
                        OleDbDataAdapter dan = new OleDbDataAdapter("DELETE FROM Main;", cnn);
                        dan.Fill(dss, "Main");
                        OleDbDataAdapter dam = new OleDbDataAdapter("DELETE FROM Arrangements;", cnn);
                        dam.Fill(dss, "Arrangements");
                        OleDbDataAdapter dag = new OleDbDataAdapter("DELETE FROM Tones;", cnn);
                        dag.Fill(dss, "Tones");
                    }
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
            rtxt_StatisticsOnReadDLCs.Text = DB_Path + " Cleaned" + rtxt_StatisticsOnReadDLCs.Text;


            //Populate ImportDB
            //MessageBox.Show(pathDLC, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string[] filez = System.IO.Directory.GetFiles(pathDLC, "*_p.psarc");
            //pB_ReadDLCs.Value = 0;
            //pB_ReadDLCs.Maximum=  files.Length;
            int i = 0;
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
                    //dsz.Tables["Files"].AcceptChanges();
                    //MessageBox.Show(da)
                }

                //pB_ReadDLCs.Increment(1);
                i++;
            }
            rtxt_StatisticsOnReadDLCs.Text = i + " Import files Inserted" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //MessageBox.Show("test"); 

            //Check if CDLC have already been imported
            // 1. If hash already exists do not insert
            // 2. If hash does not exists then:
            // 2.1 If Artist+Album+Title exists check version. If bigger add
            // 3.2 If Artist+Album+Title exists check version. If the same add as alternate
            // 4.3 If Artist+Album+Title exists check version. If smaller ask if adding as alternate or not, show a list of comparisons factors like DLCName,Sort-s, Has-s, Original FileName
            DataSet ds = new DataSet();
            i = 0;
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter daa = new OleDbDataAdapter("SELECT FullPath, Path, FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;", cnn);
                    daa.Fill(ds, "Import");
                    var noOfRec = ds.Tables[0].Rows.Count;//ds.Tables[0].Rows[0].ItemArray[0].ToString();
                    rtxt_StatisticsOnReadDLCs.Text = noOfRec + " New Songs 2 Import into MainDB" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
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
                        pB_ReadDLCs.Maximum = 2 * (ds.Tables[0].Rows.Count - 1);
                        for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            //MessageBox.Show(pB_ReadDLCs.Maximum+" test " +i); 
                            //DataTable AccTable = aSet.Tables["Accounts"];
                            var FullPath = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                            if (!FullPath.IsValidPSARC())
                            {
                                MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(FullPath)), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // UNPACK
                            var unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                            var packagePlatform = FullPath.GetPlatform();
                            // REORGANIZE
                            var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                            //if (i==0) MessageBox.Show("-");
                            if (structured)
                            {
                                //if (i == 0) MessageBox.Show("0.1");
                                unpackedDir = DLCPackageData.DoLikeProject(unpackedDir);
                                //if (i == 0) MessageBox.Show("0.2");
                            }
                            //FIX for adding preview_preview_preview
                            //if (i == 0) MessageBox.Show("2");
                            // LOAD DATA
                            var info = DLCPackageData.LoadFromFolder(unpackedDir, packagePlatform);
                            //if (i == 0) MessageBox.Show("3");
                            rtxt_StatisticsOnReadDLCs.Text = " Song " + (i + 1) + " " + info.SongInfo.Artist + " - " + info.SongInfo.SongDisplayName + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            pB_ReadDLCs.Increment(1);

                            //calculate if has DD (Dynamic Dificulty)..if at least 1 track has a difficulty bigger than 1 then it has
                            var xmlFiles = Directory.GetFiles(unpackedDir, "*.xml", SearchOption.AllDirectories);
                            var platform = FullPath.GetPlatform();
                            var DD = "No";
                            foreach (var xml in xmlFiles)
                            {
                                if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("vocal"))
                                    continue;

                                if (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("showlight"))
                                    continue;

                                platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                                Song2014 xmlContent = Song2014.LoadFromFile(xml);
                                var manifestFunctions = new ManifestFunctions(platform.version);
                                if (manifestFunctions.GetMaxDifficulty(xmlContent) > 1) DD = "Yes";
                            }

                            // READ ARRANGEMENTS

                            //var updateAcmd = "";
                            var Lead = "No";
                            var Bass = "No";
                            var Vocals = "No";
                            var Guitar = "No";
                            var Rhythm = "No";
                            var Combo = "No";
                            var PluckedType = "";
                            var Tunings = "";
                            //public enum ArrangementName : int { Lead = 0/* Single notes */, Rhythm /* Chords */, Combo /* Combo */, Bass, Vocals };
                            //enum ArrangementType { Guitar, Bass, Vocal };
                            //public enum InstrumentTuning {[Description("E Standard")] Standard, [Description("Drop D")] DropD, [Description("Eb")] EFlat, [Description("Open G")] OpenG };
                            //public enum PluckedType : int { NotPicked, Picked };
                            List<string> alist = new List<string>();
                            List<string> blist = new List<string>();
                            foreach (var arg in info.Arrangements)
                            {
                                if (arg.ArrangementType == ArrangementType.Guitar)
                                {
                                    Guitar = "Yes";
                                    if (arg.Tuning != Tunings && Tunings != "") Tunings = "Different";
                                    else Tunings = arg.Tuning;

                                    if (arg.Name == ArrangementName.Lead) Lead = "Yes";
                                    else if (arg.Name == ArrangementName.Rhythm) Rhythm = "Yes";
                                    else if (arg.Name == ArrangementName.Combo) Combo = "Yes";
                                }
                                else if (arg.ArrangementType == ArrangementType.Vocal) Vocals = "Yes";
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
                                }
                                s1 = (arg.SongXml.File.Replace(".xml", ".json").Replace("EOF", "Toolkit"));
                                using (FileStream fs = File.OpenRead(s1))
                                {
                                    SHA1 sha = new SHA1Managed();
                                    blist.Add((BitConverter.ToString(sha.ComputeHash(fs))).ToString());
                                }
                                //rtxt_StatisticsOnReadDLCs.Text = "done ar hashes: " +"\n" + rtxt_StatisticsOnReadDLCs.Text;

                            }

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

                            var author = "";
                            var tkversion = "";
                            if (versionFile.Length > 0)
                                tkversion = ReadPackageToolkitVersion(versionFile[0]);

                            if (versionFile.Length > 0)
                            {
                                author = ReadPackageAuthor(versionFile[0]);
                                if (tkversion.Length == 0)
                                    tkversion = ReadPackageOLDToolkitVersion(versionFile[0]);
                            }


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

                            var import_path = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                            var original_FileName = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                            //Generating the HASH code
                            string art_hash = "";
                            string audio_hash = "";
                            string audioPreview_hash = "";
                            string ss = info.AlbumArtPath;
                            using (FileStream fs = File.OpenRead(ss))
                            {
                                SHA1 sha = new SHA1Managed();
                                art_hash = BitConverter.ToString(sha.ComputeHash(fs));//MessageBox.Show(FileHash+"-"+ss);
                            }
                            //rtxt_StatisticsOnReadDLCs.Text = "hashes: " + ss + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            ss = info.OggPath;
                            using (FileStream fs = File.OpenRead(ss))
                            {
                                SHA1 sha = new SHA1Managed();
                                audio_hash = BitConverter.ToString(sha.ComputeHash(fs));
                            }

                            ss = info.OggPreviewPath;
                            //rtxt_StatisticsOnReadDLCs.Text = "rhashes: " + rtxt_StatisticsOnReadDLCs.Text;
                            if (ss != null)
                                using (FileStream fs = File.OpenRead(ss))
                                {
                                    SHA1 sha = new SHA1Managed();
                                    audioPreview_hash = BitConverter.ToString(sha.ComputeHash(fs));
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
                            //statistics: DLCName,Sort-s, Has-s, Original FileName, toolkit version, hash for all file, file size for all files
                            //var updatecmd = "INSERT INTO Main(";
                            //SELECT if the same artist, album, songname
                            var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + info.SongInfo.Artist + "\") AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\") AND ";
                            sel += "(LCASE(Song_Title) = LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                            sel += "OR LCASE(Song_Title) like \"%" + info.SongInfo.SongDisplayName.ToLower() + "%\" ";
                            //sel += "OR (\"%LCASE(Song_Title)%\" like LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
                            sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + info.SongInfo.SongDisplayNameSort + "\")) OR LCASE(DLC_Name)=LCASE(\"" + info.Name + "\");";
                            //Read from DB
                            int norows = 0;
                            norows = SQLAccess(sel);
                            //rtxt_StatisticsOnReadDLCs.Text = "processing &repackaging for " + norows + " duplicates &" + sel + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            //MessageBox.Show("Chose: 1.Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);                           

                            var b = 0;
                            var artist = "Insert";
                            string j = ""; string k = "";
                            var IDD = "";
                            var folder_name = "";
                            foreach (var file in files)
                            {
                                if (b == norows) break;
                                folder_name = file.Folder_Name;

                                if ((author.ToLower() == file.Author.ToLower() && author != "" && file.Author != "" && file.Author != "Custom Song Creator" && author != "Custom Song Creator") || (file.DLC_Name == info.Name))
                                {
                                    if (file.DLC_Name.ToLower() == info.Name.ToLower())
                                        artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);
                                    else
                                    {
                                        if (file.Version.ToInt32() > info.PackageVersion.ToInt32()) artist = "Update";
                                        if (file.Version.ToInt32() < info.PackageVersion.ToInt32())
                                            if (file.Is_Alternate != "Yes") artist = "Ignore";
                                            else artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);
                                        if (file.Version.ToInt32() == info.PackageVersion.ToInt32()) artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);
                                        // assess=alternate, update or ignore//as maybe a new package(ing) is desired to be inserted in the DB
                                    }
                                }
                                else
                                    if (author.ToLower() != file.Author.ToLower() && (author != "" && author != "Custom Song Creator" && file.Author != "Custom Song Creator" && file.Author != ""))
                                    artist = "Alternate";
                                else artist = AssesConflict(file, info, author, tkversion, DD, Bass, Guitar, Combo, Rhythm, Lead, Tunings, b, norows, original_FileName, art_hash, audio_hash, audioPreview_hash, alist, blist);

                                //Exit condition
                                if (artist == "Alternate")
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = "pr" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    artist = "Insert";
                                    Random random = new Random();

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
                                    string altver = ""; alt = "1";
                                    foreach (DataRow dataRow in dss.Tables[0].Rows)
                                    {
                                        altver = dataRow.ItemArray[0].ToString();
                                        alt = (altver.ToInt32() + 1).ToString(); //Add Alternative_Version_No
                                        //rtxt_StatisticsOnReadDLCs.Text = alt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    }

                                    if (file.DLC_Name.ToLower() == info.Name.ToLower()) info.Name = random.Next(0, 100000) + info.Name;
                                    if (file.Song_Title.ToLower() == info.SongInfo.SongDisplayName.ToLower()) info.SongInfo.SongDisplayName += " a." + (alt.ToInt32() + 1).ToString() + ((author == null || author == "Custom Song Creator") ? "" : " " + author);// ;//random.Next(0, 100000).ToString()
                                    //if (file.Song_Title_Sort == info.SongInfo.SongDisplayNameSort) info.SongInfo.SongDisplayNameSort += random.Next(0, 100000);

                                    // rtxt_StatisticsOnReadDLCs.Text = "highest " + altver + "\n" + rtxt_StatisticsOnReadDLCs.Text;                                    
                                }

                                if (artist != "Ignore") b = norows; //exit if an update/alternate=insert was triggered..autom or by choice(asses)
                                else b++;
                                IDD = file.ID; //Save Id in case of update or asses-update
                                j = file.Version;
                                k = file.Author;
                            }

                            //Define final path for the imported song
                            var norm_path = txt_TempPath.Text + "\\" + ((info.PackageVersion == null) ? "ORIG" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongDisplayName;
                            //if (artist == "ignore") ;

                            //@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=
                            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                            command = connection.CreateCommand();

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
                                command.CommandText += "audioPreview_Hash = @param48 ";
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
                                command.Parameters.AddWithValue("@param15", ((info.PackageVersion == "") ? "1" : info.PackageVersion));
                                command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                                command.Parameters.AddWithValue("@param17", info.Volume);
                                command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                                command.Parameters.AddWithValue("@param19", info.Name);
                                command.Parameters.AddWithValue("@param20", info.AppId);
                                command.Parameters.AddWithValue("@param21", info.AlbumArtPath);
                                command.Parameters.AddWithValue("@param22", info.OggPath);
                                command.Parameters.AddWithValue("@param23", info.OggPreviewPath ?? DBNull.Value.ToString());
                                command.Parameters.AddWithValue("@param24", Bass);
                                command.Parameters.AddWithValue("@param25", Guitar);
                                command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                                command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                                command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                                command.Parameters.AddWithValue("@param29", ((Vocals != "") ? Vocals : "No"));
                                command.Parameters.AddWithValue("@param30", "sect1on");
                                command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param33", Tones_Custom);
                                command.Parameters.AddWithValue("@param34", DD);
                                command.Parameters.AddWithValue("@param35", ((info.PackageVersion != "" && tkversion != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param36", ((author != "" && tkversion != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param37", Tunings);
                                command.Parameters.AddWithValue("@param38", PluckedType);
                                command.Parameters.AddWithValue("@param39", ((info.PackageVersion == "") ? "ORIG" : "CDLC"));
                                command.Parameters.AddWithValue("@param40", info.SignatureType);
                                command.Parameters.AddWithValue("@param41", ((author != "") ? author : (tkversion != "" ? "Custom Song Creator" : "")));
                                command.Parameters.AddWithValue("@param42", tkversion);
                                command.Parameters.AddWithValue("@param43", ((info.PackageVersion == "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param44", ((alt == "") ? "No" : "Yes"));
                                command.Parameters.AddWithValue("@param45", alt);
                                command.Parameters.AddWithValue("@param46", art_hash);
                                command.Parameters.AddWithValue("@param47", audio_hash);
                                command.Parameters.AddWithValue("@param48", audioPreview_hash);

                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                    //Deleted old folder
                                    Directory.Delete(folder_name, true);
                                    //remove original dir TO DO
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
                                //Update by INSERT into Main DB
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
                                command.CommandText += "audioPreview_Hash ";
                                command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                                command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                                command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                                command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48" + ")"; //,@param44,@param45,@param46,@param47,@param48,@param49
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
                                command.Parameters.AddWithValue("@param15", ((info.PackageVersion == "") ? "1" : info.PackageVersion));
                                command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                                command.Parameters.AddWithValue("@param17", info.Volume);
                                command.Parameters.AddWithValue("@param18", info.PreviewVolume);
                                command.Parameters.AddWithValue("@param19", info.Name);
                                command.Parameters.AddWithValue("@param20", info.AppId);
                                command.Parameters.AddWithValue("@param21", info.AlbumArtPath);
                                command.Parameters.AddWithValue("@param22", info.OggPath);
                                command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));// ((info.OggPreviewPath == "") ? DBNull.Value : info.OggPreviewPath));
                                command.Parameters.AddWithValue("@param24", Bass);
                                command.Parameters.AddWithValue("@param25", Guitar);
                                command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                                command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                                command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                                command.Parameters.AddWithValue("@param29", ((Vocals != "") ? Vocals : "No"));
                                command.Parameters.AddWithValue("@param30", "sect1on");
                                command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param33", Tones_Custom);
                                command.Parameters.AddWithValue("@param34", DD);
                                command.Parameters.AddWithValue("@param35", ((info.PackageVersion != "" && tkversion != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param36", ((author != "" && tkversion != "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param37", Tunings);
                                command.Parameters.AddWithValue("@param38", PluckedType);
                                command.Parameters.AddWithValue("@param39", ((info.PackageVersion == "") ? "ORIG" : "CDLC"));
                                command.Parameters.AddWithValue("@param40", info.SignatureType);
                                command.Parameters.AddWithValue("@param41", ((author != "") ? author : (tkversion != "" ? "Custom Song Creator" : "")));
                                command.Parameters.AddWithValue("@param42", tkversion);
                                command.Parameters.AddWithValue("@param43", ((info.PackageVersion == "") ? "Yes" : "No"));
                                command.Parameters.AddWithValue("@param44", ((alt == "") ? "No" : "Yes"));
                                command.Parameters.AddWithValue("@param45", alt);
                                command.Parameters.AddWithValue("@param46", art_hash);
                                command.Parameters.AddWithValue("@param47", audio_hash);
                                command.Parameters.AddWithValue("@param48", audioPreview_hash);
                                //EXECUTE SQL/INSERT
                                try
                                {
                                    command.CommandType = CommandType.Text;
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                finally
                                {
                                    if (connection != null) connection.Close();
                                }
                                //If No version found then defaulted to 1
                                //TO DO If default album cover then mark it as suck !?
                                //If no version found must by Rocksmith Original or DLC

                                rtxt_StatisticsOnReadDLCs.Text = "Records inserted in Main= " + (i + 1) + "..." + rtxt_StatisticsOnReadDLCs.Text;// + "-"+ ds.Tables[0].Rows[i].ItemArray[3].ToString();
                                                                                                                                                 //updatecmd="SELECT Path, FileName,FileName, FileHash, FileSize, ImportDate FROM Import as i LEFT JOIN Main as m on m.File_Hash = i.FileHash OR m.Original_File_Hash = i.FileHash WHERE m.ID is NULL;";
                                                                                                                                                 //OleDbDataAdapter dac = new OleDbDataAdapter(updatecmd, cnn);
                                                                                                                                                 //dac.Fill(ds, "Main");
                            }

                            if (artist == "Insert" || artist == "Update") //Common set of action for all
                            {
                                //Get last inserted ID
                                DataSet dus = new DataSet();
                                OleDbDataAdapter dad = new OleDbDataAdapter("SELECT ID FROM Main WHERE File_Hash=\"" + ds.Tables[0].Rows[i].ItemArray[3].ToString() + "\"", cnn);
                                dad.Fill(dus, "Main");
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
                                            //                                          MessageBox.Show(mss);
                                            poss = mss.ToString().LastIndexOf("\\") + 1;
                                            //MessageBox.Show(poss);
                                            arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                                            if (arg.SongFile.File == "")
                                                arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                                            else arg.SongFile.File = arg.SongFile.File.Replace("0_import", "");
                                            rtxt_StatisticsOnReadDLCs.Text = "..Arrangements renamed..."+n + rtxt_StatisticsOnReadDLCs.Text;//+ arg.SongXml.File+ arg.SongFile.File +
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
                                        command.CommandText += "SNGFileHash";
                                        command.CommandText += ") VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                                        command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                                        command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                                        command.CommandText += ",@param30,@param31,@param32,@param33,@param34";
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
                                            rtxt_StatisticsOnReadDLCs.Text = command.CommandText + "\n" + arg.Name + rtxt_StatisticsOnReadDLCs.Text;
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
                                rtxt_StatisticsOnReadDLCs.Text = "Arrangements Updated" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

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
                                rtxt_StatisticsOnReadDLCs.Text = "ToneDB Updated" + "..." + rtxt_StatisticsOnReadDLCs.Text;

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
                                        File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length));
                                    }
                                    Directory.Delete(source_dir, true);
                                    //var ee = "";
                                    rtxt_StatisticsOnReadDLCs.Text = " DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                }
                                catch (Exception ee)
                                {
                                    rtxt_StatisticsOnReadDLCs.Text = "FAILED3 " + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                    Console.WriteLine(ee.Message);
                                }

                                //Fixing any _preview_preview issue..Start
                                //Correct moved file path audio,preview
                                //Add wem
                                //Corrent arrangements file path
                                var cmd = "UPDATE Main SET";
                                //var cmdA = "UPDATE Arrangements SET";
                                var audiopath = "";
                                var audioprevpath = "";
                                var ms = info.AlbumArtPath;
                                if (ms.Length > 0)
                                {
                                    pos = ms.ToString().LastIndexOf("\\") + 1;
                                    cmd += " AlbumArtPath=\"" + norm_path + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                }

                                //ms = info.Arrangements.;
                                //if (ms.Length > 0)
                                //{
                                //    pos = ms.ToString().LastIndexOf("\\") + 1;
                                //    cmd += " AlbumArtPath=\"" + norm_path + "\\Toolkit\\" + ms.Substring(pos) + "\"";
                                //}
                                pos = (info.OggPath.LastIndexOf(".wem"));
                                ms = info.OggPath;
                                if (ms.Length > 0 && pos > 1)
                                {
                                    ms = ms.Substring(0, pos);
                                    if (info.OggPath.LastIndexOf("_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview"));
                                    pos = ms.LastIndexOf("\\") + 1;
                                    l = ms.Substring(pos).Length;
                                    audiopath = norm_path + "\\Toolkit\\" + ms.Substring(pos, l);
                                    cmd += " , AudioPath=\"" + audiopath + ".wem\"";
                                    cmd += " , OggPath=\"" + norm_path + "\\EOF\\" + ms.Substring(pos, l) + ".ogg\"";
                                }

                                pos = (info.OggPath.LastIndexOf(".wem"));
                                ms = info.OggPath;
                                if (ms.Length > 0 && pos > 1 && (info.OggPreviewPath != null))
                                {
                                    ms = ms.Substring(0, pos);
                                    if (info.OggPath.LastIndexOf("_preview_preview.wem") > 1) ms = ms.Substring(0, ms.LastIndexOf("_preview_preview"));
                                    pos = ms.LastIndexOf("\\") + 1;
                                    l = ms.Substring(pos).Length;
                                    audioprevpath = norm_path + "\\Toolkit\\" + ms.Substring(pos, l);
                                    cmd += " , audioPreviewPath=\"" + audioprevpath + "_preview.wem\"";
                                    cmd += " , oggPreviewPath=\"" + norm_path + "\\EOF\\" + ms.Substring(pos, l) + "_preview.ogg\"";
                                }

                                cmd += " , Folder_Name=\"" + norm_path + "\"";

                                cmd += " WHERE ID=" + CDLC_ID;
                                OleDbDataAdapter das = new OleDbDataAdapter(cmd, cnn);
                                das.Fill(dis, "Main");
                                rtxt_StatisticsOnReadDLCs.Text = "Main DB updated after DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;

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

                                if (chbx_Additional_Manipualtions.GetItemChecked(15)) //16. Move Original Imported files to temp/0_old                               
                                {
                                    //Move imported psarc into the old folder
                                    //var destin_path = txt_RocksmithDLCPath.Text + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                    //txt_TempPath.Text + "\\" + ((info.PackageVersion == null) ? "Original" : "CDLC") + "_" + info.SongInfo.Artist + "_" + info.SongInfo.SongYear + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongDisplayName;
                                    rtxt_StatisticsOnReadDLCs.Text = "done" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    try
                                    {
                                        File.Copy(txt_TempPath.Text + "\\\0\\" + original_FileName, import_path + "\\" + original_FileName);
                                        rtxt_StatisticsOnReadDLCs.Text = "File Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                    }
                                    catch (System.IO.FileNotFoundException ee)
                                    {
                                        rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                        Console.WriteLine(ee.Message);
                                    }
                                }
                            }
                            rtxt_StatisticsOnReadDLCs.Text = "done" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                            pB_ReadDLCs.Increment(1);
                        }
                    }
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

            //Import DLC
            //Populate DB the AUTOM fields
            // FILL PACKAGE CREATOR FORM


            //Show Intro database window
            MainDB frm = new MainDB();
            frm.Show();

            //dataGrid.frmMainForm.ActiveForm.Show();
            //MessageBox.Show("f");
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

        public string AssesConflict(Files filed, DLCPackageData datas, string author, string tkversion, string DD, string Bass, string Guitar, string Combo, string Rhythm, string Lead, string tunnings, int i, int norows, string original_FileName, string art_hash, string audio_hash, string audioPreview_hash, List<string> alist, List<string> blist)
        {
            //rtxt_StatisticsOnReadDLCs = chbx_Additional_Manipualtions.SelectedValue + "\n" + rtxt_StatisticsOnReadDLCs;
            //rtxt_StatisticsOnReadDLCs.Text = "dashes: " + art_hash + " - " + audio_hash + " - " + audioPreview_hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //rtxt_StatisticsOnReadDLCs.Text = "dasheD: " + filed.art_Hash + " - " + filed.audio_Hash + " - " + filed.audioPreview_Hash + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            if (chbx_Additional_Manipualtions.GetItemChecked(13) || (chbx_Additional_Manipualtions.GetItemChecked(14) && (tkversion == "" || (tkversion != "" && filed.Is_Original =="Yes"))))
                //"14. Import all as Alternates" 15. Import any Custom as Alternate if an Original exists
                return "Alternate";
            else
            {
                string text = "Same Current -> Existing " + (i + 2) + "/" + (norows + 1) + " " + filed.Artist + "-" + filed.Album + "\n";
                text += ((datas.SongInfo.SongDisplayName == filed.Song_Title) ? "" : ("\n1/14+ Song Titles: " + datas.SongInfo.SongDisplayName + "->" + filed.Song_Title));
                text += ((datas.SongInfo.SongDisplayNameSort == filed.Song_Title_Sort) ? "" : ("\n2/14+ Song Sort Titles: " + datas.SongInfo.SongDisplayNameSort + "->" + filed.Song_Title_Sort));
                text += ((original_FileName == filed.Original_FileName) ? "" : ("\n3/14+ FileNames: " + original_FileName + "-" + filed.Original_FileName));
                text += ((((tkversion == "") ? "Yes" : "No") == filed.Is_Original) ? "" : ("\n4/14+ Is Original: " + ((tkversion == "") ? "Yes" : "No") + " -> " + filed.Is_Original));
                text += ((tkversion == filed.ToolkitVersion) ? "" : ("\n5/14+ Toolkit version: " + tkversion + " -> " + filed.ToolkitVersion));
                text += ((author == filed.Author) ? "" : ("\n6/14+ Author: " + author + " -> " + filed.Author));
                text += ((tunnings == filed.Tunning) ? "" : ("\n7/14+ Tunning: " + tunnings + " -> " + filed.Tunning));
                text += ((datas.PackageVersion == filed.Version) ? "" : ("\n8/14+ Version: " + datas.PackageVersion + " -> " + filed.Version));
                text += ((datas.Name == filed.DLC_Name) ? "" : ("\n9/14+ DLC ID: " + datas.Name + " -> " + filed.DLC_Name));
                text += ((DD == filed.Has_DD) ? "" : ("\n10/14+ DD: " + DD + " -> " + filed.Has_DD));
                //text += "\nOriginal Is Alternate: " + filed.Is_Alternate + (filed.Is_Alternate == "Yes" ? " v. " + filed.Alternate_Version_No : "");
                text += "\n11/14+ Avail. Instruments: " + ((Bass == "Yes") ? "B" : "") + ((Guitar == "Yes") ? "G" : "") + ((Rhythm == "Yes") ? "R" : "") + ((Lead == "Yes") ? "L" : "") + ((Combo == "Yes") ? "C" : "");
                text += " -> " + ((filed.Has_Bass == "Yes") ? "B" : "") + ((filed.Has_Guitar == "Yes") ? "G" : "") + ((filed.Has_Rhythm == "Yes") ? "R" : "") + ((filed.Has_Lead == "Yes") ? "L" : "") + ((filed.Has_Combo == "Yes") ? "L" : "");
                text += ((filed.AlbumArt_Hash == art_hash) ? "" : "\n12/14+ Diff AlbumArt: Yes");//+ art_hash + "->" + filed.art_Hash
                text += ((filed.Audio_Hash == audio_hash) ? "" : "\n13/14+ Diff AudioFile: Yes");// + audio_hash + "->" + filed.audio_Hash 
                text += ((filed.AudioPreview_Hash == audioPreview_hash) ? "" : "\n14/14+ Diff Preview File: Yes");//  + audioPreview_hash + "->" + filed.audioPreview_Hash

                //files hash
                DataSet ds = new DataSet();
                i = 0;
                var DB_Path = txt_DBFolder.Text + "\\Files.accdb;";
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
                        rtxt_StatisticsOnReadDLCs.Text = noOfRec + "Assesment Arrangement hash file" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
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
                                        }
                                        if (jsonHash == blist[k])
                                            diffjson = false;
                                        else
                                        {
                                            lastConverjsonDateTime_cur = GetTExtFromFile(arg.SongFile.File);
                                            lastConverjsonDateTime_exist = GetTExtFromFile(jsonFile);
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
                    Console.WriteLine(ee.Message);
                }

                //files size//files dates
                DialogResult result1 = MessageBox.Show(text + "\n\nChose:\n\n1. Update\n2. Alternate\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes) return "Update";
                else if (result1 == DialogResult.No) return "Alternate";
                else return "ignore";//if (result1 == DialogResult.Cancel) 
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
            //PACKAGE
            //Import DLC
            //Populate DB the AUTOM fields
            // FILL PACKAGE CREATOR FORM

            //Files[] files = new Files[10000];
            var cmd = "SELECT * FROM Main ";
            if (rbtn_Population_Selected.Checked == true) cmd += "WHERE Selected = \"Yes\"";
            //else if (rbtn_Population_All.Checked) ;
            else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            cmd += " ORDER BY Artist";
            //Read from DB
            int norows = 0;
            norows = SQLAccess(cmd);
            rtxt_StatisticsOnReadDLCs.Text = "processing &repackaging for " + norows + " " + cmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            int i = 0;
            var artist = "";
            //var j = 0;
            foreach (var file in files)
            {

                if (i == norows)
                    break;
                // UNPACK
                //var unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                var packagePlatform = file.Folder_Name.GetPlatform();
                // REORGANIZE
                //var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                //if (structured)
                //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);
                // LOAD DATA
                var info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);

                var data = new DLCPackageData
                {
                    GameVersion = GameVersion.RS2014,
                    Pc = true,
                    Mac = rbtn_Mac.Checked,
                    XBox360 = rbtn_Xbox.Checked,
                    PS3 = rbtn_PS3.Checked,
                    Name = file.DLC_Name,
                    AppId = file.DLC_AppID,

                    //USEFUL CMDs String.IsNullOrEmpty(
                    SongInfo = new SongInfo
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

                var norm_path = txt_TempPath.Text + "\\" + ((data.PackageVersion == null) ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                //manipulating the info
                if (cbx_Activ_Title.Checked)
                    data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i);
                if (cbx_Activ_Title_Sort.Checked)
                    data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i);
                if (cbx_Activ_Artist.Checked)
                    data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i);
                if (cbx_Activ_Artist_Sort.Checked)
                    data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i);
                if (cbx_Activ_Album.Checked)
                    data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i);

                //continuing to manipualte the info
                //1. Add Increment to all songs
                //2. Add Increment to all songs(&Separately per artist)
                //3. Make all DLC IDs unique(&save)
                //4. Add DD (4 Levels)
                //5. Remove DD
                //6. Remove DD only for Bass Guitar
                //7. Remove the 4sec of the Preview song
                //8. Don't repack Broken songs
                //9. Pack to cross-platform Compatible Filenames
                //10. Generate random 30sec Preview
                //11. Use shortnames in the Filename for Artist&Album //take only the capital letter/leters after a space
                //12. Repack Originals
                //13. Repack PC
                //14. Import all Duplicates as Alternates
                //15. Import any Custom as Alternate if an Original exists
                //16. Move Original Imported files to temp/0_old

                if (chbx_Additional_Manipualtions.GetItemChecked(0)) //"1. Add Increment to all Titles"
                    data.Name = i + data.Name;
                artist = "";
                if (data.SongInfo.Artist == files[i + 1].Artist) artist = i + " ";
                else if (i > 0) if (data.SongInfo.Artist == files[i - 1].Artist) artist = i + " ";

                if (chbx_Additional_Manipualtions.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
                    data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;

                if (chbx_Additional_Manipualtions.GetItemChecked(7)) //"8. Don't repack Broken songs"
                    if (file.Is_Broken == "Yes") break;

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
                        }
                    }

                //Fix the _preview_preview issue
                var ms = data.OggPath; var audiopath = ""; var audioprevpath = "";
                //MessageBox.Show("One or more");
                try
                {
                    var sourceAudioFiles = Directory.GetFiles(norm_path, "*.wem", SearchOption.AllDirectories);
                    //var targetAudioFiles = new List<string>();
                    foreach (var fil in sourceAudioFiles)
                    {
                        //MessageBox.Show("test2.02 " + fil);
                        //rtxt_StatisticsOnReadDLCs.Text = "thinking about fixing _preview_preview issue" + rtxt_StatisticsOnReadDLCs.Text;
                        if (fil.LastIndexOf("_preview_preview.wem") > 0)
                        {
                            ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                            File.Move((ms + "_preview.wem"), (ms + ".wem"));
                            File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                            rtxt_StatisticsOnReadDLCs.Text = "fixing _preview_preview issue" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    }
                }
                catch (Exception ee)
                {
                    rtxt_StatisticsOnReadDLCs.Text = "FAILED6" + ee.Message + "---" + ms + "---" + data.OggPath + "\n -" + audiopath + "\n -" + audioprevpath + ".wem" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    Console.WriteLine(ee.Message);
                }
                if (data == null)
                {
                    MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //MessageBox.Show("test2.1");

                //if (GameVersion.RS2014 == GameVersion.RS2012)
                //{
                //    try
                //    {
                //        OggFile.VerifyHeaders(AudioPath);
                //    }
                //    catch (InvalidDataException ex)
                //    {
                //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
                //dlcSavePath = ds.Tables[0].Rows[i].ItemArray[1].ToString() + "\\";// + ((info.PackageVersion == null) ? "Original" : "CDLC") + "-" + info.SongInfo.SongYear +".psarc";
                //var dlcSavePath = GeneralExtensions.GetShortName("{0}_{1}_v{2}", (((file.Version == null) ? "Original" : "CDLC") + "_" + info.SongInfo.SongDisplayName), (info.SongInfo.SongDisplayName + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongYear), info.PackageVersion, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));
                var FN = "";

                if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(txt_File_Name.Text, i);
                else FN = GeneralExtensions.GetShortName("{0}-{1}-v{2}", ("def" + ((file.Version == null) ? "ORIG" : "CDLC") + "_" + file.Artist), (file.Album_Year.ToInt32() + "_" + file.Album + "_" + file.Song_Title), file.Version, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));//((data.PackageVersion == null) ? "Original" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                if (file.Is_Alternate == "Yes") FN += "a." + file.Alternate_Version_No + file.Author;

                rtxt_StatisticsOnReadDLCs.Text = "fn: " + FN + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (chbx_Additional_Manipualtions.GetItemChecked(8))
                {
                    FN = FN.Replace(".", "_");
                    FN = FN.Replace(" ", "_");
                }

                var dlcSavePath = txt_TempPath.Text + "\\" + FN;
                //dlcSavePath += ".pscarc";
                rtxt_StatisticsOnReadDLCs.Text = "after man:" + dlcSavePath + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //rtxt_StatisticsOnReadDLCs.Text ="tosave " + data.SongInfo.SongDisplayName + "\n" + data.SongInfo.SongDisplayNameSort + "\n" + data.SongInfo.Artist + "\n" + data.SongInfo.ArtistSort + "\n" + data.SongInfo.Album + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                // + "\n"++"\n"++"\n"++"\n"++"\n"++"\n" +;
                //if (Path.GetFileName(dlcSavePath).Contains(" ") && rbtn_PS3.Checked)
                //if (!ConfigRepository.Instance().GetBoolean("creator_ps3pkgnamewarn"))
                //MessageBox.Show("test3");
                //if (!bwGenerate.IsBusy && data != null)
                //{
                //updateProgress.Visible = true;
                //currentOperationLabel.Visible = true;
                //dlcGenerateButton.Enabled = false;
                // bwGenerate.RunWorkerAsync(data);
                //rtxt_StatisticsOnReadDLCs.Text ="rez : " + dlcSavePath + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //int progress = 0;
                //bwGenerate.ReportProgress(progress, "Generating PC package");
                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate((dlcSavePath), data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                //progress += 1;
                //bwGenerate.ReportProgress(progress);

                //}
                i++;
                //TO DO DELETE the ORIGINAL IMPORTED FILES or not
            }

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
            MainDB frm = new MainDB();
            frm.Show();
        }
    }
}
