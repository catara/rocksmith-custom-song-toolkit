using RocksmithToolkitLib.XmlRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {

        public class MainDBfields
        {
            public string NoRec { get; set; }   //	NoRec
            public string ID { get; set; }   //	NoRec
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
            public string Groups { get; set; }    //	Groups
            public string Rating { get; set; }   //	Rating
            public string Description { get; set; }  //	Description
            public string Comments { get; set; }     //	Comments
            public string Has_Track_No { get; set; }   //	Show_Album
            public string Platform { get; set; }   //	Show_Track
            public string PreviewTime { get; set; }    //	Show_Year
            public string PreviewLenght { get; set; }    //	Show_CDLC
            public string Youtube_Playthrough { get; set; }  //	Show_Rating
            public string CustomForge_Followers { get; set; }     //	CustomForge_Followers
            public string CustomForge_Version { get; set; }    //	CustomForge_Version
            public string FilesMissingIssues { get; set; }   //	FilesMissingIssues
            public string Duplicates { get; set; }   //	Duplicates
            public string Pack { get; set; }  //	Pack
            public string Keep_BassDD { get; set; }   //	Keep_BassDDs
            public string Keep_DD { get; set; }    //	Keep_DD
            public string Keep_Original { get; set; }  //	Keep_Original
            public string Song_Lenght { get; set; }  //	Song_Lenght
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
            public string File_Creation_Date { get; set; }
            public string Is_Live { get; set; }
            public string Live_Details { get; set; }
            public string Remote_Path { get; set; }
            public string audioBitrate { get; set; }
            public string audioSampleRate { get; set; }
        }

        static public MainDBfields[] GetRecord_s(string cmd) //static
        {
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //Files[] files = new Files[10000];
            //rtxt_StatisticsOnReadDLCs.Text = "re" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var MaximumSize = 0;
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            //DataSet dus = new DataSet();
            //OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //dax.Fill(dus, "Main");
            //dax.Dispose();
            MainDBfields[] query = new MainDBfields[10000];
            DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd);

            var i = 0;
            MaximumSize = dus.Tables[0].Rows.Count;

            query[0] = new MainDBfields();
            query[0].NoRec = MaximumSize.ToString();
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {                
                query[i].NoRec = MaximumSize.ToString();
                query[i].ID = dataRow.ItemArray[0].ToString();
                query[i].Song_Title = dataRow.ItemArray[1].ToString();
                query[i].Song_Title_Sort = dataRow.ItemArray[2].ToString();
                query[i].Album = dataRow.ItemArray[3].ToString();
                query[i].Artist = dataRow.ItemArray[4].ToString();
                query[i].Artist_Sort = dataRow.ItemArray[5].ToString();
                query[i].Album_Year = dataRow.ItemArray[6].ToString();
                query[i].AverageTempo = dataRow.ItemArray[7].ToString();
                query[i].Volume = dataRow.ItemArray[8].ToString();
                query[i].Preview_Volume = dataRow.ItemArray[9].ToString();
                query[i].AlbumArtPath = dataRow.ItemArray[10].ToString();
                query[i].AudioPath = dataRow.ItemArray[11].ToString();
                query[i].audioPreviewPath = dataRow.ItemArray[12].ToString();
                query[i].Track_No = dataRow.ItemArray[13].ToString();
                query[i].Author = dataRow.ItemArray[14].ToString();
                query[i].Version = dataRow.ItemArray[15].ToString();
                query[i].DLC_Name = dataRow.ItemArray[16].ToString();
                query[i].DLC_AppID = dataRow.ItemArray[17].ToString();
                query[i].Current_FileName = dataRow.ItemArray[18].ToString();
                query[i].Original_FileName = dataRow.ItemArray[19].ToString();
                query[i].Import_Path = dataRow.ItemArray[20].ToString();
                query[i].Import_Date = dataRow.ItemArray[21].ToString();
                query[i].Folder_Name = dataRow.ItemArray[22].ToString();
                query[i].File_Size = dataRow.ItemArray[23].ToString();
                query[i].File_Hash = dataRow.ItemArray[24].ToString();
                query[i].Original_File_Hash = dataRow.ItemArray[25].ToString();
                query[i].Is_Original = dataRow.ItemArray[26].ToString();
                query[i].Is_OLD = dataRow.ItemArray[27].ToString();
                query[i].Is_Beta = dataRow.ItemArray[28].ToString();
                query[i].Is_Alternate = dataRow.ItemArray[29].ToString();
                query[i].Is_Multitrack = dataRow.ItemArray[30].ToString();
                query[i].Is_Broken = dataRow.ItemArray[31].ToString();
                query[i].MultiTrack_Version = dataRow.ItemArray[32].ToString();
                query[i].Alternate_Version_No = dataRow.ItemArray[33].ToString();
                query[i].DLC = dataRow.ItemArray[34].ToString();
                query[i].Has_Bass = dataRow.ItemArray[35].ToString();
                query[i].Has_Guitar = dataRow.ItemArray[36].ToString();
                query[i].Has_Lead = dataRow.ItemArray[37].ToString();
                query[i].Has_Rhythm = dataRow.ItemArray[38].ToString();
                query[i].Has_Combo = dataRow.ItemArray[39].ToString();
                query[i].Has_Vocals = dataRow.ItemArray[40].ToString();
                query[i].Has_Sections = dataRow.ItemArray[41].ToString();
                query[i].Has_Cover = dataRow.ItemArray[42].ToString();
                query[i].Has_Preview = dataRow.ItemArray[43].ToString();
                query[i].Has_Custom_Tone = dataRow.ItemArray[44].ToString();
                query[i].Has_DD = dataRow.ItemArray[45].ToString();
                query[i].Has_Version = dataRow.ItemArray[46].ToString();
                query[i].Tunning = dataRow.ItemArray[47].ToString();
                query[i].Bass_Picking = dataRow.ItemArray[48].ToString();
                query[i].Tones = dataRow.ItemArray[49].ToString();
                query[i].Groups = dataRow.ItemArray[50].ToString();
                query[i].Rating = dataRow.ItemArray[51].ToString();
                query[i].Description = dataRow.ItemArray[52].ToString();
                query[i].Comments = dataRow.ItemArray[53].ToString();
                query[i].Has_Track_No = dataRow.ItemArray[54].ToString();
                query[i].Platform = dataRow.ItemArray[55].ToString();
                query[i].PreviewTime = dataRow.ItemArray[56].ToString();
                query[i].PreviewLenght = dataRow.ItemArray[57].ToString();
                query[i].Youtube_Playthrough = dataRow.ItemArray[58].ToString();
                query[i].CustomForge_Followers = dataRow.ItemArray[59].ToString();
                query[i].CustomForge_Version = dataRow.ItemArray[60].ToString();
                query[i].FilesMissingIssues = dataRow.ItemArray[61].ToString();
                query[i].Duplicates = dataRow.ItemArray[62].ToString();
                query[i].Pack = dataRow.ItemArray[63].ToString();
                query[i].Keep_BassDD = dataRow.ItemArray[64].ToString();
                query[i].Keep_DD = dataRow.ItemArray[65].ToString();
                query[i].Keep_Original = dataRow.ItemArray[66].ToString();
                query[i].Song_Lenght = dataRow.ItemArray[67].ToString();
                query[i].Original = dataRow.ItemArray[68].ToString();
                query[i].Selected = dataRow.ItemArray[69].ToString();
                query[i].YouTube_Link = dataRow.ItemArray[70].ToString();
                query[i].CustomsForge_Link = dataRow.ItemArray[71].ToString();
                query[i].CustomsForge_Like = dataRow.ItemArray[72].ToString();
                query[i].CustomsForge_ReleaseNotes = dataRow.ItemArray[73].ToString();
                query[i].SignatureType = dataRow.ItemArray[74].ToString();
                query[i].ToolkitVersion = dataRow.ItemArray[75].ToString();
                query[i].Has_Author = dataRow.ItemArray[76].ToString();
                query[i].OggPath = dataRow.ItemArray[77].ToString();
                query[i].oggPreviewPath = dataRow.ItemArray[78].ToString();
                query[i].UniqueDLCName = dataRow.ItemArray[79].ToString();
                query[i].AlbumArt_Hash = dataRow.ItemArray[80].ToString();
                query[i].Audio_Hash = dataRow.ItemArray[81].ToString();
                query[i].AudioPreview_Hash = dataRow.ItemArray[82].ToString();
                query[i].Has_BassDD = dataRow.ItemArray[83].ToString();
                query[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
                query[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
                query[i].Album_ShortName = dataRow.ItemArray[86].ToString();
                query[i].Available_Old = dataRow.ItemArray[87].ToString();
                query[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
                query[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
                query[i].File_Creation_Date = dataRow.ItemArray[90].ToString();
                query[i].Is_Live = dataRow.ItemArray[91].ToString();
                query[i].Live_Details = dataRow.ItemArray[92].ToString();
                query[i].Remote_Path = dataRow.ItemArray[93].ToString();
                query[i].audioBitrate = dataRow.ItemArray[94].ToString();
                query[i].audioSampleRate = dataRow.ItemArray[95].ToString();
                i++;
                query[i] = new MainDBfields();
            }
            return query;//files[10000];
        }
        static public String GetHash(string filename) //static
        {
            //Generating the HASH code
            var FileHash = "";
            try
            { if (File.Exists(filename))
                using (FileStream fs = File.OpenRead(filename))
                {
                    SHA1 sha = new SHA1Managed();
                    FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                    fs.Close();
                }
            }
            catch (Exception ee){ }
            return FileHash;
        }

        static public void DeleteFromDB(string DB, string slct)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";// "";
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                try
                {
                    DataSet dss = new DataSet();
                    OleDbDataAdapter dan = new OleDbDataAdapter(slct, cnn);
                    dan.Fill(dss, "DB");
                    dan.Dispose();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                }
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";
            //save import table to reference the hashcodes in future imports
            using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                try
                {
                    DataSet dsm = new DataSet();
                    string insertcmd;
                    if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    dab.Fill(dsm, ftable);
                    dab.Dispose();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow(ee + "DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                }
            }

        }

        static public DataSet SelectFromDB(string ftable, string fcmds)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";
            DataSet dsm = new DataSet();
            using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cnb);
                    da.Fill(dsm, ftable);
                    da.Dispose();
                    return dsm;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import"+ee , false, false);
                    frm1.ShowDialog();
                    return dsm;
                }
            }
        }
        static public string MoveTheAtEnd(string t)
        {
            var txt = (t.Substring(0, 4) == "The " ? t.Substring(4, t.Length - 4) + ",The" : t);
            txt = (txt.Substring(0, 4) == "Die " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);
            txt = (txt.Substring(0, 4) == "the " ? txt.Substring(4, t.Length - 4) + ",The" : txt);
            txt = (txt.Substring(0, 4) == "die " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);
            txt = (txt.Substring(0, 4) == "THE " ? txt.Substring(4, t.Length - 4) + ",The" : txt);
            txt = (txt.Substring(0, 4) == "DIE " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);

            return txt;
        }

        static public void CleanFolder(string pathfld)
        {
            pathfld = pathfld + "\\";
            System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(pathfld);
            foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
            {
                try { file.Delete(); }
                catch (Exception ex) { Console.Write(ex); }
            }
        }

        static public DataSet UpdateDB(string ftable, string fcmds)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb;";
            DataSet dsm = new DataSet();

            OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = new OleDbCommand(fcmds, myConn);
            OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);
            try
            {
                myDataAdapter.Fill(dsm, ftable);
                myConn.Close();
                return dsm;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                ErrorWindow frm1 = new ErrorWindow(ee + "DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                frm1.ShowDialog();
                return dsm;
            }
        }

    }
}
