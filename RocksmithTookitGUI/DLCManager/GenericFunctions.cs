//yb
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.VisualBasic.FileIO; //addded references Asembly VB for deleting to recycle bin
using RocksmithToolkitLib;
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.DLCPackage.AggregateGraph;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.Ogg;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.XmlRepository;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
//using SpotifyAPI.Web.Enums; //Enums
//using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Access;//https://stackoverflow.com/questions/58130446/net-core-3-0-and-ms-office-interop?msclkid=c246b2bcb21811ecb85b2c5e7437aac3
using System.Collections.Specialized;
using RocksmithToolkitLib.DLCPackage.Manifest2014;
using Newtonsoft.Json.Linq;
using System.IO.Packaging;
//using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using SQLite;
using System.Windows.Input;
using Microsoft.Win32;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using X360.Other;
using RocksmithToolkitLib.DLCPackage.XBlock;
using static System.Data.Entity.Infrastructure.Design.Executor;
using SpotifyApi.NetCore;
using System.Runtime.Intrinsics.Arm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Net.Mime.MediaTypeNames;
//using System.Speech.Synthesis;
using System.Windows.Documents;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Printing;
using X360.FATX;
using System.Threading;
using RocksmithToolkitLib.Sng2014HSL;
using Arrangement = RocksmithToolkitLib.DLCPackage.Arrangement;
using Rocksmith2014.XML;
using Vocals = RocksmithToolkitLib.XML.Vocals;
using Font = System.Drawing.Font;
using System.Runtime.Intrinsics.X86;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.InteropServices;
using Windows.ApplicationModel;
using RocksmithToolkitLib.SngToTab;
using X360.Profile;
using System.Windows;
using FontStyle = System.Drawing.FontStyle;

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {
        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        public const long BUFFER_SIZE = 4096;
        public static StringBuilder errorsFound;
        //public static bool metadatadisplayedonce = false;
        public static CultureInfo enUS = new CultureInfo("en-US");
        public static void dbmissingHandle(string ex)
        {
            UpdateLog(DateTime.Now, ex, true, c("dlcm_TempPath"), "", "DLCManager", null, null);
            var missing = false; DateTime zipdate = new DateTime(1900, 1, 1); var fil = ""; long fill = 0;
            if (!File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"]))
            {
                DialogResult result1 = DialogResult.No;
                if (File.Exists(AppWD + "\\AccessDB.accdb"))
                {
                    System.IO.FileInfo f1 = null;
                    System.IO.FileInfo f2 = null;
                    try
                    {
                        f1 = new System.IO.FileInfo(AppWD + "\\AccessDB.accdb");
                        f2 = new System.IO.FileInfo(AppWD + "\\SQLLiteDB.db");
                        var ct1 = f1.CreationTime;
                        var sz1 = f1.Length;
                        var ct2 = f2.CreationTime;
                        var sz2 = f2.Length;

                        //getlastbackups
                        if (Directory.Exists(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\0_temp"))
                        {
                            System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\0_temp");
                            foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                            {
                                if (!file.FullName.Contains(".gz")) continue;
                                fil = file.FullName;
                                fill = file.Length;
                                if (zipdate < file.CreationTime) zipdate = file.CreationTime;
                            }
                        }
                        UpdateLog(DateTime.Now, "Finished processing " + Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\0_temp searching for backups"
                            , false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        System.IO.DirectoryInfo downloadedMessageInfo1 = new DirectoryInfo(AppWD);
                        foreach (FileInfo file in downloadedMessageInfo1.GetFiles())
                        {
                            if (!file.FullName.Contains(".gz")) continue;
                            fil = file.FullName;
                            fill = file.Length;
                            if (zipdate < file.CreationTime) zipdate = file.CreationTime;
                        }
                        UpdateLog(DateTime.Now, "Finished processing " + AppWD + " searching for backups"
                            , false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        try
                        {
                            System.IO.DirectoryInfo downloadedMessageInfo3 = new DirectoryInfo(c("dlcm_0_temp") + "\\0_temp\\");
                            foreach (FileInfo file in downloadedMessageInfo3.GetFiles())
                            {
                                if (!file.FullName.Contains(".gz")) continue;
                                fil = file.FullName;
                                fill = file.Length;
                                if (zipdate < file.CreationTime) zipdate = file.CreationTime;
                            }
                            UpdateLog(DateTime.Now, "Finished processing " + c("dlcm_0_temp") + "\\0_temp\\ searching for backups"
                                , false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                            System.IO.DirectoryInfo downloadedMessageInfo4 = new DirectoryInfo(c("dlcm_0_temp"));
                            foreach (FileInfo file in downloadedMessageInfo4.GetFiles())
                            {
                                if (!file.FullName.Contains(".gz")) continue;
                                fil = file.FullName;
                                fill = file.Length;
                                if (zipdate < file.CreationTime) zipdate = file.CreationTime;
                            }
                            UpdateLog(DateTime.Now, "Finished processing " + c("dlcm_0_temp") + " searching for backups"
                                , false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }
                        catch (Exception ezx)
                        {
                            var tsst = "Error ..." + ezx.Message; var timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        }

                        result1 = System.Windows.Forms.MessageBox.Show("DB file not found: " + ConfigRepository.Instance()["dlcm_DBFolder"] + "!" +
                            "\n\n(Yes)Do you want to restore last Saved/Backed-up DBs? (" + (fil is null?"missing "+ c("dlcm_0_temp") + "\\0_temp\\" + "backup ":fil)
                            + ", date: " + zipdate + ",size:" + fill + ")" +
                            "\ninto target folder?\n\n" +
                            "(No) " + (!Directory.Exists(Path.GetDirectoryName(c("dlcm_DBFolder"))) ? "Create path:" : "") + " Copy template DBs (" +
                            "\nAccessDB " + ct1 + " " + sz1 + ";\nSQLLiteDB " + ct2 + " " + sz2 + ") to: " + Path.GetDirectoryName(c("dlcm_DBFolder")) +
                            "\n\n(Cancel) will reference to the DB Template in Program folder: " + AppWD
                            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    }
                    catch (Exception ezx)
                    {
                        var tsst = "Error ..." + ezx; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    }
                }


                if (result1 == DialogResult.Yes)
                {
                    //DialogResult result2 = DialogResult.No;
                    //result2 = MessageBox.Show("(Yes) Want to use decompressed backup files with timestamp: " + zipdate +
                    //    "\n\n(No) Or default to the DB Template in Program folder: " + AppWD
                    //       , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    //if (result2 == DialogResult.Yes) 

                    unzipdb(Path.GetDirectoryName(c("dlcm_DBFolder")), fil);//unzip
                    if (!File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"])) missing = true;
                    else missing = false;
                }
                else if (result1 == DialogResult.No)
                {
                    //var Temp_Path_Import = txt_TempPath.Text;
                    //var dflt_Path_Import = txt_TempPath.Text + "\\0_to_temp";
                    //var old_Path_Import = txt_TempPath.Text + "\\0_old";
                    //var dataPath = txt_TempPath.Text + "\\0_data";
                    //var broken_Path_Import = txt_TempPath.Text + "\\0_broken";
                    //var dupli_Path_Import = txt_TempPath.Text + "\\0_duplicate";
                    //var dlcpacks = txt_TempPath.Text + "\\0_dlcpacks";
                    //var repacked_Path = txt_TempPath.Text + "\\0_repacked";
                    //var repacked_XBOXPath = txt_TempPath.Text + "\\0_repacked\\XBOX360";
                    //var repacked_PCPath = txt_TempPath.Text + "\\0_repacked\\PC";
                    //var repacked_MACPath = txt_TempPath.Text + "\\0_repacked\\MAC";
                    //var repacked_PSPath = txt_TempPath.Text + "\\0_repacked\\PS3";
                    //var Log_PSPath = txt_TempPath.Text + "\\0_log";
                    //var AlbumCovers_PSPath = txt_TempPath.Text + "\\0_albumCovers";
                    //var Archive_Path = txt_TempPath.Text + "\\0_archive";
                    //var Temp_Path = txt_TempPath.Text + "\\0_temp";
                    //var log_Path = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
                    //string pathDLC = txt_RocksmithDLCPath.Text; DialogResult res = new DialogResult();
                    //CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks,
                    //pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path, AlbumCovers_PSPath, Log_PSPath, Archive_Path, dataPath, Temp_Path, dflt_Path_Import);
                    if (!Directory.Exists(Path.GetDirectoryName(c("dlcm_DBFolder"))))
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(c("dlcm_DBFolder")));
                        }
                        catch (Exception ezx)
                        {
                            var tsst = "Error at path create..." + ezx; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }
                    if (!Directory.Exists(Path.GetDirectoryName(c("dlcm_DBFolder")))) missing = true;
                    else
                    {
                        //    ConfigRepository.Instance()["dlcm_DBFolder"] = AppWD + "\\AccessDB.accdb";
                        //    MessageBox.Show("As could not create Path missing, DB was Defaulted to: " + ConfigRepository.Instance()["dlcm_DBFolder"],
                        //MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        //}
                        FileCopy(AppWD + "\\AccessDB.accdb", Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\AccessDB.accdb", true, 1, false);
                        FileCopy(AppWD + "\\SQLLiteDB.db", Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\SQLLiteDB.db", true, 1, false);
                        FileCopy(AppWD + "\\LinkDB.accdb", Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\LinkDB.accdb", true, 1, false);
                        missing = false;
                    }
                }
                else if (result1 == DialogResult.Cancel) missing = true;
            }

            if (missing)/*&& (Directory.Exists(ConfigRepository.Instance()["dlcm_DBFolder"]))*/
            {
                ConfigRepository.Instance()["dlcm_DBFolder"] = AppWD + "\\AccessDB.accdb";
                ConfigRepository.Instance()["dlcm_DBFolder"] = ConfigRepository.Instance()["dlcm_DBFolder"] + ((ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes") ? "\\..\\AccessDB.accdb" : "\\..\\SQLLiteDB.db");
                System.Windows.Forms.MessageBox.Show("As DB Path missing, DB was Defaulted to: " + ConfigRepository.Instance()["dlcm_DBFolder"],
                    MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //cnb.ConnectionString = "Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"];
            //timestamp = 
            UpdateLog(DateTime.Now, "DB" +
            "(s: AccessDB.accdb-restored(" + File.Exists(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\AccessDB.accdb") + ")," +
            "\nLinkDB.accdb-restored(" + File.Exists(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\LinkDB.accdb") + ")," +
            "\nSQLLiteDB.db-restored(" + File.Exists(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\SQLLiteDB.db") + "),)" +
            "\n set to: " + ConfigRepository.Instance()["dlcm_DBFolder"], true, c("dlcm_TempPath"), "", "DLCManager", null, null);
        }

        public static void BackupDB(bool zip)/*, System.Windows.Forms.Label lbl*/
        {
            var dtt = System.DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var timestamp = System.DateTime.Now;

            var zipFile = c("dlcm_TempPath") + "\\0_temp\\" + dtt.Replace("/", "").Replace(":", "").Substring(0, 8) + ".gz";// "C:\data\myzip.zip";
            if (!File.Exists(zipFile) && zip)
            {
                timestamp = UpdateLog(timestamp, "Create zip", true, c("dlcm_TempPath"), "", "DLCManager", null, null);
                //try { cnb/*.*/Close(); cnc.Close(); } catch (Exception ex) {; }

                //AddFileToZip(zipFile, c("dlcm_DBFolder")); //Remove_Content_Types_FromZip(zipFile);
                AddFileToZip(zipFile, Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\AccessDB.accdb");
                AddFileToZip(zipFile, Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\SQLLiteDB.db");
                AddFileToZip(zipFile, Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\LinkDB.accdb");

                //copy to remote
                // FileCopy(c("dlcm_DBFolder"), c("dlcm_0_temp") + "\\0_temp\\" + dtt.Replace("/", "").Replace(":", "").Substring(0, 8) + ".gz", true, 1, false);
                var ccrf = c("dlcm_0_temp") + "\\0_temp\\" + dtt.Replace("/", "").Replace(":", "").Substring(0, 8) + ".gz";
                FileCopy(zipFile, ccrf, true, 1, false);
                timestamp = UpdateLog(timestamp, ccrf + " Remote copied zip: " + File.Exists(ccrf), true, c("dlcm_TempPath"), "", "DLCManager", null, null);

                //copy internally
                FileCopy(zipFile, AppWD + "\\dbbackup.gz", true, 1, false);
                if (AppWD.Contains("\\Debug\\")) FileCopy(zipFile, AppWD + "\\..\\..\\..\\..\\..\\DLCManager\\external_tools\\dbbackup.gz", true, 1, false);
                FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\AccessDB.accdb", AppWD + "\\AccessDB.accdb", true, 1, false);
                FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\SQLLiteDB.db", AppWD + "\\SQLLiteDB.db", true, 1, false);
                FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\LinkDB.accdb", AppWD + "\\LinkDB.accdb", true, 1, false);
                if (AppWD.Contains("\\Debug\\")) FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\AccessDB.accdb", AppWD + "\\..\\..\\..\\..\\..\\DLCManager\\external_tools\\AccessDB.accdb", true, 1, false);
                if (AppWD.Contains("\\Debug\\")) FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\SQLLiteDB.db", AppWD + "\\..\\..\\..\\..\\..\\DLCManager\\external_tools\\SQLLiteDB.db", true, 1, false);
                if (AppWD.Contains("\\Debug\\")) FileCopy(Path.GetDirectoryName(c("dlcm_DBFolder")) + "\\LinkDB.accdb", AppWD + "\\..\\..\\..\\..\\..\\DLCManager\\external_tools\\LinkDB.accdb", true, 1, false);
                timestamp = UpdateLog(timestamp, "Local backup: done (true/false) AccessDB.accdb: " + File.Exists(AppWD + "\\AccessDB.accdb") +
                "Local backup: done SQLLiteDB.accdb: " + File.Exists(AppWD + "\\SQLLiteDB.db") +
                    "Local backup: done LinkDB.accdb: " + File.Exists(AppWD + "\\LinkDB.accdb"), true, c("dlcm_TempPath"), "", "DLCManager", null, null);
                //.Replace("DLCManager\\external_tools", "")
                try
                {
                    var x = new System.IO.FileInfo(zipFile).Length;
                    if (File.Exists(zipFile))
                        if (new System.IO.FileInfo(zipFile).Length <= 100)
                        {
                            timestamp = UpdateLog(timestamp, "Error at zip create (" + x + ")", true, c("dlcm_TempPath"), "", "DLCManager", null, null);
                            DeleteFile(zipFile, true);
                            AddFileToZip(zipFile, c("dlcm_DBFolder"));
                        }
                    OpenDb();

                    timestamp = UpdateLog(timestamp, "EndCreate zip", true, c("dlcm_TempPath"), "", "DLCManager", null, null);
                }
                catch (Exception ex) { UpdateLog(timestamp, "Error at Create zip" + ex.Message, true, c("dlcm_TempPath"), "", "DLCManager", null, null); }/*, lbl_Access*/
            }
        }
        static public string GetExtraAttributes(string origFN, string noMFN, string gom, string SongDisplayName, string Album, string PackageAuthor, string Name, string Artist)
        {
            var Is_MultiTrack = ""; var MultiTrack_Version = "";
            var IsLive = ""; var LiveDetails = ""; var IsAcoustic = ""; var IsSingle = ""; var IsSoundtrack = ""; var IsMetalCover = ""; var IsUkulele = "";
            var IsInstrumental = ""; var IsEP = ""; var IsUncensored = ""; var IsFullAlbum = ""; var IsRemastered = ""; var InTheWorks = "";
            var IsKaraoke = ""; var IsDemo = ""; var HasFeaturing = ""; var IsRemix = ""; var IsCover = ""; var IsMultiStrings = ""; var IsMedley = "";
            var Titl = ""; var IsGreatestHits = ""; var IsDeluxe = ""; var IsGameSoundtrack = ""; var IsMidi = ""; var IsTVTheme = ""; var IsAmateurCover = "";
            if (Album == "") Album = "---";
            var multibool = ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes" ? true : false;
            var multxt = "No Guitar"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
            multxt = "No Band"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
            multxt = "No Band Audio"; Titl = Check4MultiT(SongDisplayName, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(No Guitars)"; }
            multxt = "No Lead"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "Lead"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Lead"; }
            multxt = "(Lead)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "No Lead"; }
            multxt = "Lead Only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Lead"; }
            multxt = "Only Lead"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "Drums Only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Drums"; }
            multxt = "(No Drums No Vocal)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Drums"; }
            multxt = "Only Guitars"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "No Bass"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "(Bass)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "No Bass"; }
            multxt = "No Bass Audio"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "No Bass"; }
            multxt = "Bass Only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Bass"; }
            multxt = "Only Bass"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "No Rhythm"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "Only Rhythm"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "Rhythm Only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "Only Rhythm"; }
            multxt = "(Only BackTrack)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "(Only Back Track)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "backingtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backing track"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backing audio only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backing track"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backing only"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "backing"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "Only Band"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = "(Only BackTrack)"; }
            multxt = "No Vocal"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = multxt; }
            multxt = "FullBand"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = ""; }
            multxt = "(FullBand)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { Is_MultiTrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; MultiTrack_Version = ""; }

            //detect minor types
            multibool = ConfigRepository.Instance()["dlcm_AdditionalManipul104"] == "Yes" ? true : false;
            multxt = "Instrumental"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsInstrumental = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "(Single)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; SongDisplayName = Titl.Split(';')[0]; }/*gom = Titl.Split(';')[0];*/
            multxt = "(Single)"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "(Single-Edit)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "CD Single"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "12 inch"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "Singles 12"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "\"12"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "12\""; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "'12"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "12'"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"12 inch"; }
            multxt = "7 inch"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "\" 7inch"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "'7"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "7'"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "'7"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "7'"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; LiveDetails += " \"7 inch"; }
            multxt = "Single"; if (IsSingle != "Yes") Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "(EP)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "(EP)"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; Album = Titl.Split(';')[0]; }
            //multxt = " EP "; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            //multxt = " EP "; Titl = Check4MultiT(origFN, Album, multxt, multibool); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "The Original Soundtrack from the Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Television Soundtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack from the Motion Picture"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack from the Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "The Original Motion Picture Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "original motion picture score"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Game Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsGameSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "(movie ver.)"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            // multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "The Original Movie Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "main title theme"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "main title theme"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "TV Theme"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "TV Theme"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Soundtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            if (Album.Length > 2) if (Album.ToLower().Contains(" ost") || Album.ToLower().Contains("(ost") || Album.ToLower().Substring(0, 2) == "ost") { multxt = "OST"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; } }
            if (SongDisplayName.ToLower().Contains(" ost") || SongDisplayName.ToLower().Contains("(ost")) { multxt = "OST"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; } }
            multxt = "Theme Reprise"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Theme Song"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Theme Song"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = " Theme"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = " Theme"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsTVTheme = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Uncensored"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsUncensored = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "FullAlbum"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsFullAlbum = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Full Album"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsFullAlbum = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Remaster"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Karaoke Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsKaraoke = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Karaoke"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsKaraoke = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Demo"; Titl = SongDisplayName.ToLower().Contains("demon") ? ";" : Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsDemo = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Demo"; Titl = Album.ToLower().Contains("demon") ? ";" : Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsDemo = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Extended Remixed"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Extended Remixed"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Extended Mix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Extended Mix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remixed"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Remixed"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Megamix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }                
            multxt = "Remix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Mix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Mix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "radio edit"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Single Edit"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Movie Edit"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "movie ver"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Extended"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Edit"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Cover"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsCover = "Yes"; /*SongDisplayName = Titl.Split(';')[0];*/ }
            multxt = "Medley"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsMedley = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Medley"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMedley = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Deluxe Edition"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsDeluxe = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Deluxe"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsDeluxe = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Deluxe"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsDeluxe = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "GreatestHits"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsGreatestHits = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "GreatestHits"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsGreatestHits = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "midi"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMidi = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Ukulele"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsUkulele = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Metal Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMetalCover = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "MetalCover"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMetalCover = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Metal Cover"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMetalCover = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "MetalVersion"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMetalCover = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Metal Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMetalCover = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "5 String"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMultiStrings = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "6 String"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMultiStrings = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "7 String"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMultiStrings = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "8 String"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMultiStrings = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "9 String"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMultiStrings = "Yes"; SongDisplayName = Titl.Split(';')[0]; }

            //Set In the works if Author/Your name can be found somewhere
            if (SongDisplayName.IndexOf(c("general_defaultauthor")) >= 0
                || Name.IndexOf(c("general_defaultauthor")) >= 0) InTheWorks = "Yes";
            if (PackageAuthor != null) if (PackageAuthor.IndexOf(c("general_defaultauthor")) >= 0) InTheWorks = "Yes";

            //Detect Live
            multxt = "(Live)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6) { IsLive = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }
            multxt = "Unplugged"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9) { IsLive = "Yes"; IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }
            multxt = "Live"; Titl = Check4MultiT(origFN, Album, multxt, false, "Album");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 4) { IsLive = "Yes"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Album.IndexOf(multxt) + 4), ""); }
            multxt = "Unplugged"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9) { IsLive = "Yes"; IsAcoustic = "Yes"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Album.IndexOf(multxt) + 4), ""); }

            //Detect Featuring
            multxt = "Feat."; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 5)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Feat.", "[Ft.").Replace(" feat.", "[Ft.")).Replace("(Feat.", "[Ft.").Replace("(feat.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }

            multxt = "Ft."; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 3)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Ft.", "[Ft.").Replace(" ft.", "[Ft.")).Replace("(Ft.", "[Ft.").Replace("(ft.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }

            multxt = "Featuring"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Featuring", "[Ft.").Replace(" featuring", "[Ft.")).Replace("(Featuring", "[Ft.").Replace("(featuring", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }

            multxt = "Feat."; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 5)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Feat.", "[Ft.").Replace(" feat.", "[Ft.")).Replace("(Feat.", "[Ft.").Replace("(feat.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }

            multxt = "Ft."; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 3)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Ft.", "[Ft.").Replace(" ft.", "[Ft.")).Replace("(Ft.", "[Ft.").Replace("(ft.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }

            multxt = "Featuring"; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Featuring", "[Ft.").Replace(" featuring", "[Ft.")).Replace("(Featuring", "[Ft.").Replace("(featuring", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }

            //if (SongDisplayName.IndexOf("Rocker") >= 0)
            //    ;

            //Detect Acoustic
            multxt = "Acoustic Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6) { IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }

            multxt = "Acoustic"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6) { IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }

            //checking if any of the softcoded Artist+Album need to have ana attribute applied
            var cmd1 = "SELECT ID FROM Standardization " +
                        "WHERE (CustomToAtribute_1='Yes' ) AND " +/*or CustomToAtribute_1 is not Null*/
                        " (IIF(Artist_Correction <>\"\",Artist_Correction,Artist)=\"" + Artist + "\" OR IIF(Album_Correction <>\"\",Album_Correction,Album)=\"" + Album + "\")";
            var no = GetNoRecords(cmd1, cnb, cnc);
            if (no > 0)
            {
                if (c("dlcm_CustomToAtribute_1").ToLower().Contains("gamesoundtrack")) IsGameSoundtrack = "Yes";
                if (c("dlcm_CustomToAtribute_1").ToLower().Contains("amateurcover")) IsAmateurCover = "Yes";
                if (c("dlcm_CustomToAtribute_1").ToLower().Contains("tvtheme")) IsTVTheme = "Yes";
                if (c("dlcm_CustomToAtribute_1").ToLower().Contains("Is_Soundtrack")) IsSoundtrack = "Yes";
            }

            var cmd2 = "SELECT ID FROM Standardization " +
            "WHERE (CustomToAtribute_2='Yes' ) AND " +/*or CustomToAtribute_2 is not Null*/
            " (IIF(Artist_Correction <>\"\",Artist_Correction,Artist)=\"" + Artist + "\" OR IIF(Album_Correction <>\"\",Album_Correction,Album)=\"" + Album + "\")";
            var no2 = GetNoRecords(cmd2, cnb, cnc);
            if (no2 > 0)
            {
                if (c("dlcm_CustomToAtribute_2").ToLower().Contains("gamesoundtrack")) IsGameSoundtrack = "Yes";
                if (c("dlcm_CustomToAtribute_2").ToLower().Contains("amateurcover")) IsAmateurCover = "Yes";
                if (c("dlcm_CustomToAtribute_2").ToLower().Contains("tvtheme")) IsTVTheme = "Yes";
                if (c("dlcm_CustomToAtribute_2").ToLower().Contains("Is_Soundtrack")) IsSoundtrack = "Yes";
            }

            var cmd3 = "SELECT ID FROM Standardization " +
            "WHERE (CustomToAtribute_3='Yes' ) AND " +/*or CustomToAtribute_3 is not Null*/
            " (IIF(Artist_Correction <>\"\",Artist_Correction,Artist)=\"" + Artist + "\" OR IIF(Album_Correction <>\"\",Album_Correction,Album)=\"" + Album + "\")";
            var no3 = GetNoRecords(cmd3, cnb, cnc);
            if (no3 > 0)
            {
                if (c("dlcm_CustomToAtribute_3").ToLower().Contains("gamesoundtrack")) IsGameSoundtrack = "Yes";
                if (c("dlcm_CustomToAtribute_3").ToLower().Contains("amateurcover")) IsAmateurCover = "Yes";
                if (c("dlcm_CustomToAtribute_3").ToLower().Contains("tvtheme")) IsTVTheme = "Yes";
                if (c("dlcm_CustomToAtribute_3").ToLower().Contains("Is_Soundtrack")) IsSoundtrack = "Yes";
            }

            var cmd4 = "SELECT ID FROM Standardization " +
            "WHERE (CustomToAtribute_4='Yes' ) AND " +/*or CustomToAtribute_4 is not Null*/
            " (IIF(Artist_Correction <>\"\",Artist_Correction,Artist)=\"" + Artist + "\" OR IIF(Album_Correction <>\"\",Album_Correction,Album)=\"" + Album + "\")";
            var no4 = GetNoRecords(cmd4, cnb, cnc);
            if (no4 > 0)
            {
                if (c("dlcm_CustomToAtribute_4").ToLower().Contains("gamesoundtrack")) IsGameSoundtrack = "Yes";
                if (c("dlcm_CustomToAtribute_4").ToLower().Contains("amateurcover")) IsAmateurCover = "Yes";
                if (c("dlcm_CustomToAtribute_4").ToLower().Contains("tvtheme")) IsTVTheme = "Yes";
                if (c("dlcm_CustomToAtribute_4").ToLower().Contains("Is_Soundtrack")) IsSoundtrack = "Yes";
            }

            var cmd5 = "SELECT ID FROM Standardization " +
            "WHERE (CustomToAtribute_5='Yes' ) AND " +/*or CustomToAtribute_5 is not Null*/
            " (IIF(Artist_Correction <>\"\",Artist_Correction,Artist)=\"" + Artist + "\" OR IIF(Album_Correction <>\"\",Album_Correction,Album)=\"" + Album + "\")";
            var no5 = GetNoRecords(cmd5, cnb, cnc);
            if (no5 > 0)
            {
                if (c("dlcm_CustomToAtribute_5").ToLower().Contains("gamesoundtrack")) IsGameSoundtrack = "Yes";
                if (c("dlcm_CustomToAtribute_5").ToLower().Contains("amateurcover")) IsAmateurCover = "Yes";
                if (c("dlcm_CustomToAtribute_5").ToLower().Contains("tvtheme")) IsTVTheme = "Yes";
                if (c("dlcm_CustomToAtribute_5").ToLower().Contains("Is_Soundtrack")) IsSoundtrack = "Yes";
            }

            //var r = "";
            //1234567891011
            //1213141616171819202122
            return Is_MultiTrack + ";" + MultiTrack_Version + ";" + IsLive + ";" + LiveDetails + ";" + IsAcoustic + ";" + IsSingle + ";" + IsSoundtrack + ";" + IsInstrumental + ";" + IsEP + ";" + IsUncensored + ";" + IsFullAlbum + ";"
                + IsRemastered + ";" + InTheWorks + ";" + IsKaraoke + ";" + IsDemo + ";" + HasFeaturing + ";" + IsRemix + ";" + IsCover + ";" + SongDisplayName + ";" + Album + ";" + IsMedley + ";" + IsMultiStrings
                + ";" + IsDeluxe + ";" + IsGreatestHits + ";" + IsMidi + ";" + IsGameSoundtrack + ";" + IsTVTheme + ";" + IsAmateurCover + ";" + IsMetalCover + ";" + IsUkulele;
        }

        public static string Check4MultiT(string origFN, string noMFN, string text, bool multibool, string tag)
        {
            var FN = origFN.ToLower();
            var ST = noMFN.ToLower();
            text = text.ToLower();
            var aaa = noMFN;
            if (origFN.ToLower().IndexOf(text) >= 0 || origFN.ToLower().IndexOf(text.Replace(" ", "")) >= 0 || origFN.ToLower().IndexOf(text.Replace(" ", "_")) >= 0 || origFN.ToLower().IndexOf(text.Replace(" ", "-")) >= 0
                || noMFN.ToLower().IndexOf(text) >= 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "")) >= 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "_")) >= 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "-")) >= 0)
            {
                noMFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(noMFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                origFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(origFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                var t = ReplaceTxt(aaa, noMFN, multibool, tag);
                if (t == "")
                    return aaa + ";" + "No";
                else return t + ";" + ((FN != origFN || noMFN != ST) ? "Yes" : "No");
            }
            return ReplaceTxt(aaa, noMFN, multibool, tag) + ";" + "No";
        }

        public static string ReplaceTxt(string orgstr, string replstr, bool ask4permission, string tag)
        {
            var a = orgstr;
            if (orgstr != replstr)
            {
                if (replstr == "") return orgstr;
                //DialogResult result111 = DialogResult.Yes;
                //if (ask4permission && ConfigRepository.Instance()["dlcm_AdditionalManipul118"] != "Yes") result111 = MessageBox.Show("Tag:" + tag + "\n\nDo you agree with replacement of \n\nOld Meta info: " + orgstr + "\nwith\nNew Meta info: " + replstr + "\n\n(Cancel=Ignore flag set too)", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                //if (result111 == DialogResult.Yes) a = replstr;
                //if (result111 == DialogResult.Cancel) a = "";
                var ass = c("dlcm_Global2TempVariable");
                ErrorWindow frm2 = null;
                var txt = "Tag:" + tag + "\n\nDo you agree with replacement of: \n\n\t\tOld Meta info: " + orgstr + "\n\twith\n\t\tNew Meta info: " + replstr + "";
                if (ask4permission && ConfigRepository.Instance()["dlcm_AdditionalManipul118"] != "Yes")
                {
                    frm2 = new ErrorWindow(txt, "", "Attribute Gathering/Meta Cleanup", true, true, true, //ignore,stop,okey
                     "Get attribute and leave the meta info as is", "Ignore setting of any flag and don't run any clean-up action","Get attribute and clean the meta info of it" , true);
                    frm2.ShowDialog();
                }

                if (a != c("dlcm_Global2TempVariable")) replstr = c("dlcm_Global2TempVariable").Replace("Tag:" + tag + "\n\nDo you agree with replacement of \n\n\t\tOld Meta info: " + orgstr + "\n\twith\nNew Meta info: "+ replstr + "", "");
                ConfigRepository.Instance()["dlcm_Global2TempVariable"] = ass;
                if (frm2 is null) a=replstr;
                else if (frm2.StopImport) return "";
                else if (frm2.IgnoreSong) return a;
                else return replstr;
            }
            return a;
        }

        public static void CreatePackingGroup(string cmd, OleDbConnection cnb, string filter, int norows, SQLite.SQLiteConnection cnc)
        {
            //var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DateTime timestamp;
            timestamp = UpdateLog(DateTime.Now, "Deleting All Packing groups & inserintg newly " + norows, true, null, null, "", null, null);
            if (filter == "Packing") return;
            DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type = \"DLC\" AND Groupz = \"Packing\"", cnb, cnc);
            string insertcmd; insertcmd = "CDLC_ID, Groupz, Type, Comments, Date_Added ";//INSERT INTO Groups ()
            //var insertcmdd = "Artist, Album, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Year_Correction";
            var insertvalues = cmd.Replace("*", "ID, \"Packing\", \"DLC\", \"89\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"") + ";";
            //"\"" + info.SongInfo.Artist + "\",\"" + info.SongInfo.Album + "\",\"" + SpotifyArtistID + "\",\"" + SpotifyAlbumID
            //    + "\",\"" + SpotifyAlbumURL + "\",\"" + SpotifyAlbumPath + "\",\"" + SpotifyAlbumYear + "\"";
            InsertIntoDBwValues("Groups", insertcmd, insertvalues, cnb, 0, cnc);

            //try
            //{
            //    DataSet dsm = new DataSet();
            //    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
            //    dab.Fill(dsm, "Groups");
            //    dab.Dispose();
            //}
            //catch (Exception ee)
            //{
            //    ShowConnectivityError(ee, "");/*, null*/
            //}
        }

        //public static string GetFilter(string Filtertxt, string SearchCmd, int i, string Searchcmdf, OleDbConnection cnb, string Group, string chbx_Format, string Import_Date, SQLiteConnection cnz)
        public static string GetFilter(string Filtertxt, string SearchCmd, int i, string Searchcmdf, OleDbConnection cnb, string Group, string chbx_Format, string Import_Date, SQLite.SQLiteConnection cnc)
        {
            var oldfilter = Filtertxt;
            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = SearchCmd;/*, "Part of No Group", "Part of Any Group" */


            var oldSearchCmd = SearchCmd;
            SearchCmd = SearchCmd.Length == 0 ? "SELECT * FROM Main " : SearchCmd.Replace("Main u", "Main").Substring(0, (SearchCmd.IndexOf(" WHERE") - 1) > 0 ? (SearchCmd.IndexOf(" WHERE") - 1) : ((SearchCmd.IndexOf(" ORDER") - 1) > 0 ? (SearchCmd.IndexOf(" ORDER") - 1) : SearchCmd.Length - 1));
            SearchCmd += " u WHERE ";
            //if (Filtertxt.IndexOf("Group ") > 0) Filtertxt = Filtertxt.Replace("Group ", "")
            //if (Filtertxt.IndexOf("Tunning ") > 0) Filtertxt = Filtertxt.Replace("Tunning ", "")
            var noOfRec = 0;
            var OrderAlt = ""; var SearchFields = "";
            switch (true)
            {
                case true when Filtertxt == "No Cover":
                    SearchCmd += "Has_Cover <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Preview":
                    SearchCmd += "Has_Preview <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Vocals":
                    SearchCmd += "Has_Vocals <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Section":
                    SearchCmd += "Has_Sections =\"Yes\"";
                    break;
                case true when Filtertxt == "No Bass":
                    SearchCmd += "Has_Bass <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Guitar":
                    SearchCmd += "Has_Guitar <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Track No.":
                    SearchCmd += "Has_Track_No <> \"Yes\" OR Track_No=\"-1\" OR Track_No=\"0\"";
                    break;
                case true when Filtertxt == "No Version":
                    SearchCmd += "Has_Version <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Author":
                    SearchCmd += "Has_Author <> \"Yes\"";
                    break;
                case true when Filtertxt == "Original":
                    SearchCmd += "Is_Original = \"Yes\"";
                    break;
                case true when Filtertxt == "CDLC":
                    SearchCmd += "Is_Original <> \"Yes\"";
                    break;
                case true when Filtertxt == "Selected":
                    SearchCmd += "Selected = \"Yes\"";
                    break;
                case true when Filtertxt == "Beta":
                    SearchCmd += "Is_Beta = \"Yes\"";
                    break;
                case true when Filtertxt == "Live":
                    SearchCmd += "Is_Live = \"Yes\"";
                    break;
                case true when Filtertxt == "Acoustic":
                    SearchCmd += "Is_Acoustic = \"Yes\"";
                    break;
                case true when Filtertxt == "Remastered":
                    SearchCmd += "Is_Remastered = \"Yes\"";
                    break;
                case true when Filtertxt == "Instrumental":
                    SearchCmd += "Is_Instrumental = \"Yes\"";
                    break;
                case true when Filtertxt == "Single":
                    SearchCmd += "Is_Single= \"Yes\"";
                    break;
                case true when Filtertxt == "Soundtrack":
                    SearchCmd += "Is_Soundtrack = \"Yes\"";
                    break;
                case true when Filtertxt == "EP":
                    SearchCmd += "Is_EP = \"Yes\"";
                    break;
                case true when Filtertxt == "Full Album":
                    SearchCmd += "Is_FullAlbum = \"Yes\"";
                    break;
                case true when Filtertxt == "Uncensored":
                    SearchCmd += "Is_Uncensored = \"Yes\"";
                    break;
                case true when Filtertxt == "Audio Changed":
                    SearchCmd += "Has_Had_Audio_Changed = \"Yes\"";
                    break;
                case true when Filtertxt == "Lyrics Changed":
                    SearchCmd += "Has_Had_Lyrics_Changed = \"Yes\"";
                    break;
                case true when Filtertxt == "Broken":
                    SearchCmd += "Is_Broken = \"Yes\"";
                    break;
                case true when Filtertxt == "Alternate":
                    SearchCmd += "Is_Alternate = \"Yes\"";
                    break;
                case true when Filtertxt == "Cover":
                    SearchCmd += "Is_Cover = \"Yes\"";
                    break;
                case true when Filtertxt == "Demo":
                    SearchCmd += "Is_Demo = \"Yes\"";
                    break;
                case true when Filtertxt == "Remix":
                    SearchCmd += "Is_Remix = \"Yes\"";
                    break;
                case true when Filtertxt == "Karaoke":
                    SearchCmd += "Is_Karaoke = \"Yes\"";
                    break;
                case true when Filtertxt == "Featuring":
                    SearchCmd += "Has_Featuring = \"Yes\"";
                    break;
                case true when Filtertxt == "Duplicated":
                    SearchCmd += "Duplicate_Of <> \"\"";
                    break;
                case true when Filtertxt == "With DD":
                    SearchCmd += "Has_DD = \"Yes\"";
                    break;
                case true when Filtertxt == "No DD":
                    SearchCmd += "Has_DD <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Bass DD":
                    SearchCmd += "Bass_Has_DD <> \"Yes\"";
                    break;
                case true when Filtertxt == "No ShowLights":
                    SearchCmd += "Has_ShowLights <> \"Yes\"";
                    break;
                case true when Filtertxt == "Capo":
                    SearchCmd += "Has_Capo = \"Yes\"";
                    break;
                case true when Filtertxt == "Japanese Vocals":
                    SearchCmd += "Has_JVocals = \"Yes\"";
                    break;
                case true when Filtertxt == "MultiStrings":
                    SearchCmd += "Is_MultiStrings = \"Yes\"";
                    break;
                case true when Filtertxt == "Deluxe":
                    SearchCmd += "Is_Deluxe = \"Yes\"";
                    break;
                case true when Filtertxt == "Greatest Hits":
                    SearchCmd += "Is_GreatestHits = \"Yes\"";
                    break;
                case true when Filtertxt == "TV Theme":
                    SearchCmd += "Is_TVTheme = \"Yes\"";
                    break;
                case true when Filtertxt == "Midi":
                    SearchCmd += "Is_Midi = \"Yes\"";
                    break;
                case true when Filtertxt == "Game Soundtrack":
                    SearchCmd += "Is_GameSoundtrack = \"Yes\"";
                    break;
                case true when Filtertxt == "Amateur Cover":
                    SearchCmd += "Is_AmateurCover = \"Yes\"";
                    break;
                case true when Filtertxt == "Medley":
                    SearchCmd += "Is_Medley = \"Yes\"";
                    break;
                case true when Filtertxt == "Pack Batch Blank":
                    SearchCmd += "Split4Pack <> \"\" or Split4Pack is not null";
                    break;
                case true when Filtertxt == "Packing":
                    var SearchCmd19 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz=\"Packing\"";

                    SearchCmd += "ID NOT IN (" + SearchCmd19 + ")";
                    break;
                case true when Filtertxt == "Capo 0":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=0)";
                    break;
                case true when Filtertxt == "Capo 1":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=1)";
                    break;
                case true when Filtertxt == "Capo 2":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=2)";
                    break;
                case true when Filtertxt == "Capo 3":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=3)";
                    break;
                case true when Filtertxt == "Capo 4":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=4)";
                    break;
                case true when Filtertxt == "Capo 5":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=5)";
                    break;
                case true when Filtertxt == "Capo 6":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=6)";
                    break;
                case true when Filtertxt == "Capo 7":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=7)";
                    break;
                case true when Filtertxt == "Capo 8":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=8)";
                    break;
                case true when Filtertxt == "Capo 9":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=9)";
                    break;
                case true when Filtertxt == "Capo 10":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=10)";
                    break;
                case true when Filtertxt == "Capo 11":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=11)";
                    break;
                case true when Filtertxt == "Capo 12":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=12)";
                    break;
                case true when Filtertxt == "Capo 13":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE VAL(CapoFret)=13)";
                    break;
                case true when Filtertxt == "Pack 1":
                    SearchCmd += "Split4Pack =\"1\"";
                    break;
                case true when Filtertxt == "Pack 2":
                    SearchCmd += "Split4Pack =\"2\"";
                    break;
                case true when Filtertxt == "Pack 3":
                    SearchCmd += "Split4Pack =\"3\"";
                    break;
                case true when Filtertxt == "Pack 4":
                    SearchCmd += "Split4Pack =\"4\"";
                    break;
                case true when Filtertxt == "Pack 5":
                    SearchCmd += "Split4Pack =\"5\"";
                    break;
                case true when Filtertxt == "Pack 6":
                    SearchCmd += "Split4Pack =\"6\"";
                    break;
                case true when Filtertxt == "Pack 7":
                    SearchCmd += "Split4Pack =\"7\"";
                    break;
                case true when Filtertxt == "Pack 8":
                    SearchCmd += "Split4Pack =\"8\"";
                    break;
                case true when Filtertxt == "Pack 9":
                    SearchCmd += "Split4Pack =\"9\"";
                    break;
                case true when Filtertxt == "Pack 10":
                    SearchCmd += "Split4Pack =\"10\"";
                    break;
                case true when Filtertxt == "Preview issues":
                    SearchCmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main " +
                                "WHERE FilesMissingIssues is null AND (Has_Preview=\"No\" OR oggPreviewPath=\"\" OR audioPreviewPath=\"\") AND Is_Broken<>\"Yes\"" +
                                "" + (c("dlcm_AdditionalManipul55").ToLower() != "yes" ? "" :
                                " OR (VAL(PreviewLenght) > " + float.Parse(c("dlcm_MaxPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture) + ")" +
                                (c("dlcm_AdditionalManipul88").ToLower() != "yes" ? "" :
                                " OR (VAL(PreviewLenght) < " + float.Parse(c("dlcm_MinPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture)) + ")");
                    break;
                case true when Filtertxt == "Over Bitrate or SampleRate":
                    SearchCmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, oggPath, oggPreviewPath FROM Main" +
                        " WHERE FilesMissingIssues is null AND (VAL(audioBitrate) > "
                        + (c("dlcm_MaxBitRate")) + " or VAL(audioSampleRate) > " + (c("dlcm_MaxSampleRate")) + ") AND Is_Broken<>\"Yes\"";
                    break;
                case true when Filtertxt == "Digitech Drop compatible Guitar (Straight down conv from E Standard or Drop D)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\", \"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\", \"BbDropAb\", \"A Drop G\"))";
                    break;
                case true when Filtertxt == "Digitech Drop compatible Guitar (Straight down conv from E Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\") AND p.ArrangementType=\"Guitar\")";
                    break;
                case true when Filtertxt == "Digitech Drop compatible Bass (Straight down conv from E Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                        ", \"Bb Standard\", \"AStandard\", \"AbStandard\") AND p.ArrangementType=\"Bass\")";
                    break;
                case true when Filtertxt == "Digitech Drop compatible (Straight down conv from E Standard)":
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
                case true when Filtertxt == "Digitech Drop compatible Guitar (Straight down conv from D Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                        ", \"BbDropAb\", \"A Drop G\") AND p.ArrangementType=\"Guitar\")";
                    break;
                case true when Filtertxt == "Digitech Drop compatible Bass (Straight down conv from D Standard)":
                    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                        ", \"BbDropAb\", \"A Drop G\") AND p.ArrangementType=\"Bass\")";
                    break;
                case true when Filtertxt == "Digitech Drop compatible (Straight down conv from Drop D)":
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
                case true when Filtertxt == "E Standard":
                    SearchCmd += "Tunning = \"E Standard\"";
                    break;
                case true when Filtertxt == "Eb Standard":
                    SearchCmd += "Tunning = \"Eb Standard\"";
                    break;
                case true when Filtertxt == "Drop D":
                    SearchCmd += "Tunning = \"Drop D\"";
                    break;
                case true when Filtertxt == "Other Tunings":
                    SearchCmd += "Tunning not in (\"E Standard\",\"Eb Standard\",\"Drop D\")";
                    break;
                case true when Filtertxt == "With Bonus":
                    SearchCmd += "Has_Bonus_Arrangement = \"Yes\"";
                    break;
                case true when Filtertxt == "Imported as Pc":
                    SearchCmd += "Platform = \"Pc\"";
                    break;
                case true when Filtertxt == "Imported as PS3":
                    SearchCmd += "Platform = \"PS3\"";
                    break;
                case true when Filtertxt == "Imported as Mac":
                    SearchCmd += "Platform =\"Mac\"";
                    break;
                case true when Filtertxt == "Imported as XBOX360":
                    SearchCmd += "Platform = \"XBOX360\"";
                    break;
                case true when Filtertxt == "Packed as Pc"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND p.Platform=\"Pc\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"PC\") >0)";
                    break;
                case true when Filtertxt == "Packed as PS3"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"PS3\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"PS3\") >0)";
                    break;
                case true when Filtertxt == "Packed as Mac"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"Mac\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"MAC\") >0)";
                    break;
                case true when Filtertxt == "Packed as XBOX360"://DLCID diff than Default
                    SearchCmd += "ID IN (SELECT CDLC_ID FROM Pack_AuditTrail p WHERE u.ID=p.CDLC_ID AND Platform=\"XBOX360\" AND instr(replace(ucase(PackPath),\",\",\",\"),\"XBOX360\") >0)";
                    break;
                case true when Filtertxt == "0ALL"://0ALL
                    SearchFields = c("dlcm_SearchFields");
                    SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY " + c("dlcm_OrderOfFields") + ";";
                    //SearchCmd = SearchCmd.Replace(" WHERE ", "");
                    break;
                case true when Filtertxt == "ALL Others"://0ALL
                    SearchCmd = "SELECT * FROM Main WHERE ID not in (" + oldSearchCmd.Replace("*", "ID") + ")";
                    break;
                case true when Filtertxt == "Track No. 1"://Track No. 1
                    SearchCmd += "Track_No = \"1\"";
                    break;
                case true when Filtertxt == "DLCID diff than Default"://DLCID diff than Default
                    SearchCmd += "DLC_AppID <> \"" + c("general_defaultappid_RS2014") + "\"";
                    break;
                case true when Filtertxt == "Automatically generated Preview"://Autom gen Preview
                    SearchCmd += "PreviewTime = \"" + c("dlcm_PreviewStart") + "\" AND (PreviewLenght>\"" + c("dlcm_PreviewLenght") + "\"-1) AND (PreviewLenght<\"" + c("dlcm_PreviewLenght") + "\"+1)";
                    break;
                case true when Filtertxt == "Any DLCManager generated Preview"://Autom gen Preview
                    SearchCmd += "PreviewTime <> \"\" AND PreviewLenght <> \"\"";
                    break;
                case true when Filtertxt == "With Duplicates"://With Duplicates
                    SearchCmd += "Available_Duplicate = \"Yes\"";
                    break;
                case true when Filtertxt == "Main_NoOLD":
                    SearchCmd += "Available_Old <> \"Yes\"";
                    break;
                case true when Filtertxt == "Show Songs with FilesMissing Issues":
                    SearchCmd += "FilesMissingIssues <> \"\"";
                    break;
                case true when Filtertxt == "Songs in Rocksmith Game Lib":
                    SearchCmd += "Remote_Path <> \"\"";
                    break;
                case true when Filtertxt == "In the Works":
                    SearchCmd += "IntheWorks = \"Yes\"";
                    break;
                case true when Filtertxt == "Improved with DLC Manager":
                    SearchCmd += "ImprovedWIthDM = \"Yes\"";
                    break;
                case true when Filtertxt == "Main_NoPreviewFile":
                    SearchCmd += "audioPreviewPath = \"\"";
                    break;
                case true when Filtertxt == "Packed (curr. Platform)":
                    SearchCmd += " ID IN (SELECT CDLC_ID FROM Pack_AuditTrail WHERE PackPath like \"%0_repacked%\" AND LCASE(Platform) IN (" + chbx_Format.ToLower() + ")";
                    break;
                case true when Filtertxt == "Different Artist/Album/Title vs Sort counterparts":
                    SearchCmd += " Artist <> Artist_Sort OR Album <> Album_Sort OR Song_Title <> Song_Title_Sort";
                    break;
                case true when Filtertxt == "No Old (flag)":
                    SearchCmd += " Available_Old <> \"Yes\"";
                    break;
                case true when Filtertxt == "No Arrangements":
                    var SearchCmdg2 = "SELECT Main.ID, count(Arrangements.CDLC_ID) as noofar" +
                                        " FROM Main LEFT JOIN Arrangements ON Main.ID = Arrangements.CDLC_ID" + //("+ SearchCmd + ")
                                        " GROUP BY main.id,Arrangements.CDLC_ID" +
                                        " ORDER BY count(Arrangements.CDLC_ID) ASC;";
                    DataSet djs = new DataSet(); djs = SelectFromDB("Main", SearchCmdg2, "", cnb, cnc);
                    var noOfRei = GetNoRec(djs, cnb, cnc);
                    SearchCmdg2 = SearchCmdg2.Replace(", )", ")");

                    var idfg = "";
                    for (var ii = 0; ii < noOfRei; ii++)
                    {
                        //pB_ReadDLCs.Value = i;
                        var CIDd = djs.Tables[0].Rows[ii].ItemArray[0].ToString();
                        var count = double.Parse(djs.Tables[0].Rows[ii].ItemArray[1].ToString());
                        if (count == 0) idfg += CIDd + ",";
                        else break;
                    }
                    idfg += ";"; idfg = idfg.Replace(", ;", ";");
                    if (!idfg.Contains(",")) idfg = "0";
                    SearchCmd += "ID IN (" + idfg + ")";
                    break;
                case true when Filtertxt == "No Old (checks for file existence)":
                    DataSet des = new DataSet(); des = SelectFromDB("Main", SearchCmd, "", cnb, cnc);//"SELECT * FROM Main;"
                    var noOfRece = GetNoRec(des, cnb, cnc);

                    //pB_ReadDLCs.Value = 0; pB_ReadDLCs.Maximum = noOfRece;
                    var vFilesMissingIssues = "";
                    for (var ii = 0; ii < noOfRece; ii++)
                    {
                        //pB_ReadDLCs.Value = i;
                        var IDd = des.Tables[0].Rows[ii].ItemArray[0].ToString();
                        var OrigFileName = des.Tables[0].Rows[ii].ItemArray[19].ToString();
                        var hasOld = des.Tables[0].Rows[ii].ItemArray[89].ToString();
                        var old = c("dlcm_TempPath") + "\\0_old\\" + OrigFileName;
                        if (hasOld == "Yes") if (!File.Exists(old)) vFilesMissingIssues += IDd + ",";
                    }
                    vFilesMissingIssues += ";"; vFilesMissingIssues = vFilesMissingIssues.Replace(", ;", ";");
                    if (!vFilesMissingIssues.Contains(",")) vFilesMissingIssues = "0";
                    SearchCmd += "ID IN (" + vFilesMissingIssues + ")";
                    break;
                case true when Filtertxt == "Same (imported/old) File Name":
                    //SLOW SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    //var SearchCmd52 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT m.ID,Original_FileName FROM Main AS m ORDER BY LCASE(Original_FileName)", "", cnb, cnc);
                    noOfRec = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
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
                case true when Filtertxt == "Same Artist&Title(no[]) & SongLenght":
                    //SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON Main.Song_Lenght = Main_1.Song_Lenght AND  AND Main.ID<> Main_1.ID";
                    DataSet dvs = new DataSet(); dvs = SelectFromDB("Main", "SELECT m.ID,m.Artist, m.Song_Title, m.Album, Song_Lenght FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title), LCASE(Album)", "", cnb, cnc);
                    noOfRec = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count == 0 ? 0 : dvs.Tables[0].Rows.Count;
                    var IDb = "";
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dvs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dvs.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (CleanTitleFurther(dvs.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitleFurther(dvs.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
                                    {
                                        if (dvs.Tables[0].Rows[l].ItemArray[3].ToString().ToLower() != dvs.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())

                                            if (dvs.Tables[0].Rows[l].ItemArray[4].ToString().ToLower() == dvs.Tables[0].Rows[v].ItemArray[4].ToString().ToLower())

                                                IDb += dvs.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dvs.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                    }
                                    else break;

                    var SearchCmd421 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDb + ")";
                    SearchCmd421 = SearchCmd421.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_Title), LCASE(Album)";

                    SearchCmd += "ID IN (" + SearchCmd421 + ")";
                    break;
                case true when Filtertxt == "Same hash File Name":
                    //SLOW SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    //var SearchCmd52 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    DataSet dgc = new DataSet(); dgc = SelectFromDB("Main", "SELECT m.ID,File_Hash FROM Main AS m ORDER BY File_Hash", "", cnb, cnc);
                    noOfRec = GetNoRec(dgc, cnb, cnc);//dgc.Tables.Count == 0 ? 0 : dgc.Tables[0].Rows.Count;
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
                case true when Filtertxt == "with Preview audio Errors (max/min length)":
                    var cmdg = "SELECT ID FROM Main " +
                            "WHERE FilesMissingIssues is null AND (Has_Preview=\"No\" OR oggPreviewPath=\"\" OR audioPreviewPath=\"\") AND Is_Broken<>\"Yes\"" +
                            "" + (c("dlcm_AdditionalManipul55").ToLower() != "yes" ? "" :
                            " OR (VAL(PreviewLenght) > " + float.Parse(c("dlcm_MaxPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture) + ")" +
                            (c("dlcm_AdditionalManipul88").ToLower() != "yes" ? "" :
                            " OR (VAL(PreviewLenght) < " + float.Parse(c("dlcm_MinPreviewLenght"), NumberStyles.Float, CultureInfo.CurrentCulture)) + ")");
                    SearchCmd += " ID IN (" + cmdg + ")";
                    break;
                case true when Filtertxt == "with Song audio Errors (max bitrate/audiosample)":
                    var rtg = SearchCmd.Replace(" * ", " ID ");
                    rtg = rtg.Replace("; ", "");
                    rtg = rtg.Replace(c("dlcm_SearchFields"), "ID") + ";";
                    rtg = rtg.Replace("WHERE ;", "");
                    var cmdh = "SELECT ID FROM Main" +
                            " WHERE FilesMissingIssues is null AND (VAL(audioBitrate) > "
                            + c("dlcm_MaxBitRate") + " or VAL(audioSampleRate) > " + c("dlcm_MaxSampleRate") + ") AND "
                            + "ID IN(" + rtg + ") AND Is_Broken<>\"Yes\"";
                    SearchCmd += " ID IN (" + cmdh + ")";
                    break;
                case true when Filtertxt == "with Errors at Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID FROM LogPackingError)";
                    break;
                case true when Filtertxt == "with Errors at Last Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID from LogPackingError WHERE Pack=(SELECT TOP 1 Pack from LogPackingError GROUP BY Pack ORDER BY val(Pack) DESC))";
                    break;
                case true when Filtertxt == "Imported Last":
                    DataSet dds = new DataSet(); dds = SelectFromDB("Main", "SELECT top 1 Pack FROM Main order by ID DESC;", "", cnb, cnc);
                    noOfRec = GetNoRec(dds, cnb, cnc);//dds.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "Pack=\"" + dds.Tables[0].Rows[0].ItemArray[0].ToString() + "\"";
                    else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Imported Current Month":
                    DateTime date = DateTime.Today;
                    var firstDayOfMonth = (new DateTime(date.Year, date.Month, 1)).ToString();
                    //DataSet djs = new DataSet(); djs = SelectFromDB("Main", "SELECT Import_Date FROM Main ORDER BY Import_Date DESC;", "", cnb, cnc);
                    //noOfRec = djs.Tables[0].Rows.Count;
                    //if (noOfRec > 0) SearchCmd += "Import_Date > #" + firstDayOfMonth.ToShortDateString() + "#";
                    SearchCmd += "LEFT(Import_Date,6) =\"" + (((firstDayOfMonth.Split('/'))[2]).Split(' '))[0] + (((firstDayOfMonth.Split('/'))[0]).Length == 1 ? "0" + (firstDayOfMonth.Split('/'))[0] : (firstDayOfMonth.Split('/'))[1]) + "\"";
                    //else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Reverse current Filter":
                    SearchCmd = "SELECT * FROM Main WHERE ID not IN (" + oldSearchCmd.Replace("*", "ID") + ") ORDER BY " + c("dlcm_OrderOfFields") + "";
                    break;
                case true when Filtertxt == "Packed Last":
                    DataSet dzs = new DataSet(); dzs = SelectFromDB("LogPacking", "SELECT top 1 Pack FROM LogPacking order by ID DESC;", "", cnb, cnc);
                    noOfRec = GetNoRec(dzs, cnb, cnc);//dzs.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPacking WHERE Pack=\"" + dzs.Tables[0].Rows[0].ItemArray[0].ToString() + "\")";
                    else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Packing Errors":
                    DataSet dks = new DataSet(); dks = SelectFromDB("LogPackingError", "SELECT top 1 Pack FROM LogPackingError order by ID DESC;", "", cnb, cnc);

                    noOfRec = GetNoRec(dks, cnb, cnc);//dks.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPackingError WHERE Pack=\"" + dks.Tables[0].Rows[0].ItemArray[0].ToString() + "\")";
                    else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Same DLCName":
                    //SLOW var SearchCmd5 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.DLC_Name) = LCASE(Main_1.DLC_Name) AND Main.ID <> Main_1.ID";
                    DataSet dos = new DataSet(); dos = SelectFromDB("Main", "SELECT m.ID,DLC_Name FROM Main AS m ORDER BY LCASE(DLC_Name)", "", cnb, cnc);
                    noOfRec = GetNoRec(dos, cnb, cnc);//dos.Tables.Count == 0 ? 0 : dos.Tables[0].Rows.Count;
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
                case true when Filtertxt == "Same Title&Artist":
                    //var SearchCmd55 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID) AND (LCASE(n.Song_Title) = LCASE(m.Song_Title) and LCASE(n.Artist) = LCASE(m.Artist)) WHERE n.ID is not NULL";
                    //SearchCmd += "ID IN (" + SearchCmd55 + ")";
                    //break;
                    DataSet dqs = new DataSet(); dqs = SelectFromDB("Main", "SELECT m.ID,m.Song_Title, m.Artist FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title)", "", cnb, cnc);
                    noOfRec = GetNoRec(dqs, cnb, cnc);//dqs.Tables.Count == 0 ? 0 : dqs.Tables[0].Rows.Count;
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
                case true when Filtertxt == "Same Title(no[])&Artist":
                    DataSet dns = new DataSet(); dns = SelectFromDB("Main", "SELECT m.ID, Artist,m.Song_Title FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title)", "", cnb, cnc);
                    noOfRec = GetNoRec(dns, cnb, cnc);//dns.Tables.Count == 0 ? 0 : dns.Tables[0].Rows.Count;
                    var IDf = "";/* bool done = false;*/
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                        {
                            for (var v = l + 1; v < noOfRec; v++)
                            {
                                if (dns.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dns.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitleFurther(dns.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitleFurther(dns.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
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
                case true when Filtertxt == "Same Artist&Album different Year":
                    //var SearchCmdr5 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID AND LCASE(n.Artist) = LCASE(m.Artist) AND LCASE(n.Album) = LCASE(m.Album) AND n.Album_Year <> m.Album_Year) WHERE n.ID is not NULL";
                    //SearchCmd += "ID IN (" + SearchCmdr5 + ")";
                    //break;
                    DataSet das = new DataSet(); das = SelectFromDB("Main", "SELECT m.ID, m.Artist, m.Album, m.Album_Year FROM Main AS m ORDER BY LCASE(Artist), LCASE(Album), Album_Year", "", cnb, cnc);
                    noOfRec = GetNoRec(das, cnb, cnc);//das.Tables.Count == 0 ? 0 : das.Tables[0].Rows.Count;
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
                case true when Filtertxt == "Same Artist&Title(no[]) different Album":
                    DataSet dws = new DataSet(); dws = SelectFromDB("Main", "SELECT m.ID,m.Artist, m.Song_Title, m.Album FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_Title), LCASE(Album)", "", cnb, cnc);
                    noOfRec = GetNoRec(dws, cnb, cnc);//dws.Tables.Count == 0 ? 0 : dws.Tables[0].Rows.Count;
                    var IDc = ""; /*var dones = false;*/
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dws.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dws.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (CleanTitleFurther(dws.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitleFurther(dws.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
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
                case true when Filtertxt == "Same Artist&Title(no[]) different Year":
                    DataSet dts = new DataSet(); dts = SelectFromDB("Main", "SELECT m.ID, m.Artist, m.Song_Title, m.Album_Year FROM Main AS m ORDER BY LCASE(Artist), LCASE(Song_title), Album_Year", "", cnb, cnc);
                    noOfRec = GetNoRec(dts, cnb, cnc);//dts.Tables.Count == 0 ? 0 : dts.Tables[0].Rows.Count;
                    var IDe = ""; var donez = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                //{
                                if (dts.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dts.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitleFurther(dts.Tables[0].Rows[l].ItemArray[2].ToString()) == CleanTitleFurther(dts.Tables[0].Rows[v].ItemArray[2].ToString()))
                                        if (dts.Tables[0].Rows[l].ItemArray[3].ToString() != dts.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())
                                            IDe += dts.Tables[0].Rows[l].ItemArray[0].ToString() + ", " + dts.Tables[0].Rows[v].ItemArray[0].ToString() + ", ";
                                }
                                else if (donez) { donez = false; break; }

                    var SearchCmd40 = "SELECT " + c("dlcm_SearchFields") + " FROM Main WHERE ID IN (" + IDe + ")";
                    SearchCmd40 = SearchCmd40.Replace(", )", ")");
                    OrderAlt = "LCASE(Artist), LCASE(Song_title), Album_Year";

                    SearchCmd += "ID IN (" + SearchCmd40 + ")";
                    break;
                case true when Filtertxt == "Songs IMPORTED later than current song value":
                    //DateTime dates = DateTime.Today;
                    //var firstDayOfMonth = (new DateTime(date.Year, date.Month, 1)).ToString();
                    string d = Import_Date;
                    SearchCmd += "CINT(LEFT(Import_Date,4)) >=" + d.Substring(0, 4) + "";
                    SearchCmd += " AND CINT(RIGHT(LEFT(Import_Date,6),2)) >=" + d.Substring(4, 2) + "";
                    SearchCmd += " AND CINT(RIGHT(LEFT(Import_Date,8),2)) >=" + d.Substring(6, 2) + "";
                    break;
                case true when Filtertxt == "Sorted by Groups value/Group added date":
                    //var SearchCmd8 = "SELECT ID FROM Main"; //"LEFT JOIN Groups AS mn ON Groupz=\"" + Group + "\" AND Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                    //string ddv = databox.Rows[i].Cells["Import_Date"].Value.ToString();
                    //SearchCmd8 += "CINT(LEFT(Date_Added,4)) >=" + ddv.Substring(0, 4) + "";
                    //SearchCmd8 += " AND CINT(RIGHT(LEFT(Date_Added,6),2)) >=" + ddv.Substring(4, 2) + "";
                    //SearchCmd8 += " AND CINT(RIGHT(LEFT(Date_Added,8),2)) >=" + ddv.Substring(6, 2) + "";

                    //SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields")+", mn.Date_Added");
                    if (Group == "")
                    {
                        DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }
                    SearchCmd += "ID IN (" + oldSearchCmd + ")";
                    //SearchCmd += SearchCmd8+ " DESC mn.Date_Added";
                    break;
                case true when Filtertxt == "Part of No Group":
                    var SearchCmd9 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\"";

                    SearchCmd += "ID NOT IN (" + SearchCmd9 + ")";
                    break;
                case true when Filtertxt == "Part of No Group (Excl. Default)":
                    var SearchCm114 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz <>\"Default\"";

                    SearchCmd += "ID NOT IN (" + SearchCm114 + ")";
                    break;
                case true when Filtertxt == "Part of Any Group":
                    var SearchCmd10 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\"";
                    SearchCmd += "ID IN (" + SearchCmd10 + ")";
                    break;
                case true when Filtertxt == "Part of Any Group (Excl. Default)":
                    var SearchCmd12 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz <>\"Default\"";
                    SearchCmd += "ID IN (" + SearchCmd12 + ")";
                    break;
                case true when Filtertxt == "Part of any Group besides the selected below and Default":
                    if (Group == "")
                    {
                        DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please Select the group to Filter out songs on.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }
                    var SearchCmd11 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz <> \"" + Group + "\" AND Groupz <>\"Default\"";
                    SearchCmd += "ID IN (" + SearchCmd11 + ")";
                    break;
                case true when Filtertxt == "Part of the selected below and Others ignoring Default":
                    if (Group == "")
                    {
                        DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please Select the group to Filter out songs on.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }

                    var SearchCmdj1 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz = \"" + Group + "\"";
                    DataSet dys = new DataSet(); dys = SelectFromDB("Main", SearchCmdj1, "", cnb, cnc);
                    var noOftRec = GetNoRec(dys, cnb, cnc);//dys.Tables.Count == 0 ? 0 : dys.Tables[0].Rows.Count;

                    DataSet dbs = new DataSet(); dbs = SelectFromDB("Main", "SELECT CDLC_ID FROM vw_CountGroups WHERE vw_CountGroups.CountGrp>1", "", cnb, cnc);
                    noOfRec = GetNoRec(dbs, cnb, cnc);//dbs.Tables.Count == 0 ? 0 : dbs.Tables[0].Rows.Count;
                    var IDw = ""; //var doney = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOftRec; l++)
                            for (var v = 0; v < noOfRec; v++)
                                //{
                                //    if (dbs.Tables[0].Rows[l].ItemArray[0].ToString().ToLower() == "11684")
                                //        ;
                                if (dys.Tables[0].Rows[l].ItemArray[0].ToString().ToLower() == dbs.Tables[0].Rows[v].ItemArray[0].ToString().ToLower())
                                    //{
                                    IDw += dys.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";
                    //doney = true;
                    //}
                    //else if (doney) { doney = false; break; }
                    //}
                    //var SearchCmd1r = "SELECT DISTINCT VAL(Groups.CDLC_ID) FROM Groups LEFT JOIN vw_CountGroups ON str(vw_CountGroups.CDLC_ID) = STR(Groups.CDLC_ID) WHERE Type=\"DLC\" AND Groups = \"" + Group + "\" and vw_CountGroups.CountGrp>1";
                    SearchCmd += "ID IN (" + IDw + ")";
                    break;
                case true when Filtertxt == "Songs ADDED later to the Groups value/group than the import date of the current song value":
                    if (Group == "")
                    {
                        DialogResult result1 = System.Windows.Forms.MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }
                    var SearchCmd7 = "SELECT CDLC_ID as IDs FROM Groups AS m WHERE Groupz=\"" + Group + "\" AND Type=\"DLC\" AND ";// CDLC_ID=\"" + txt_ID+"\"";
                    string dd = Import_Date;
                    SearchCmd7 += "CINT(LEFT(Date_Added,4)) >=" + dd.Substring(0, 4) + "";
                    SearchCmd7 += " AND CINT(RIGHT(LEFT(Date_Added,6),2)) >=" + dd.Substring(4, 2) + "";
                    SearchCmd7 += " AND CINT(RIGHT(LEFT(Date_Added,8),2)) >=" + dd.Substring(6, 2) + "";

                    SearchCmd += "ID IN (" + SearchCmd7 + ")";
                    break;
                case true when Filtertxt == "Sorted by Last Packdate":
                    var SearchCmd17 = "SELECT DISTINCT CDLC_ID as IDs FROM Pack_AuditTrail ";

                    SearchCmd += "ID IN (" + SearchCmd17 + ")";
                    break;
                //case true when  Filtertxt == "READ GAMEDATA":
                default:
                    if (Filtertxt != "" && !Filtertxt.Contains("(weekly)") && !Filtertxt.Contains("Tuning ")
                        && Filtertxt != "Sorted by Groups value/Group added date" && Filtertxt != "Sorted by Last Packdate"
                        && Filtertxt.Substring(0, 5) != "Group" && !Filtertxt.Contains("MultiSelect[")) System.Windows.Forms.MessageBox.Show("No filter code found");
                    break;
            }

            if (Filtertxt.IndexOf("Tuning ") > -1)
            {
                var SearchCmd8 = "SELECT CDLC_ID FROM Arrangements WHERE LCASE(Tunning)=LCASE(\"" + Filtertxt.Replace("Tuning ", "") + "\")";// CDLC_ID=\"" + txt_ID+"\"";

                SearchCmd += "ID IN (" + SearchCmd8 + ")";
            }

            var Filterorg = Filtertxt;
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filterorg)) SearchCmd = SearchCmd.Replace("Main.", "");/*, "Part of No Group", "Part of Any Group" */
            ;

            switch (true)
            {
                case true when Filtertxt.Length > 5:
                    if (Filtertxt.Substring(0, 5) == "Group")
                        //var SearchCmd6 = ;SELECT * FROM Main u LEFT JOIN Groups AS m ON m.CDLC_ID = CSTR(u.ID) WHERE m.Groupz =  \"Zoe\"
                        //SearchCmd += "CSTR(u.ID) IN (" + "SELECT m.CDLC_ID FROM Groups AS m WHERE m.CDLC_ID = CSTR(u.ID) AND m.Groupz =  \"" + Filtertxt.Substring(6, Filtertxt.Length - 6).Trim() + "\"" + ")";
                        if (!Filtertxt.Contains("(weekly)"))//"Group Top " + c("dlcm_maxsongsinweekly") + 
                            SearchCmd = "SELECT u.ID FROM Main u LEFT JOIN Groups AS m ON m.CDLC_ID = CSTR(u.ID) WHERE m.Groupz =  \"" + Filtertxt.Substring(6, Filtertxt.Length - 6).Trim() + "\"";// + ")";
                        else
                        {
                            SearchCmd = "SELECT Top " + c("dlcm_maxsongsinweekly") +
                             " CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + c("dlcm_HotGrp") + "\" ORDER BY ID DESC";
                            DataSet dvs = new DataSet();
                            dvs = SelectFromDB("Groups", SearchCmd, "", cnb, cnc);
                            var nores = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count == 0 ? 0 : dvs.Tables[0].Rows.Count;
                            var idss = "";
                            if (nores > 0)
                                for (var l = 0; l < nores; l++)
                                    idss += dvs.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";
                            SearchCmd = "SELECT u.ID FROM Main u where u.ID  in (" + idss + "0)";
                        }
                    break;
                default:
                    break;
            }

            switch (true)
            {
                case true when Filtertxt.Length > 12:
                    if (Filtertxt.Substring(0, 12) == "MultiSelect[")
                    {
                        var msel = Filtertxt.Replace("MultiSelect[", "").Replace("]", "");
                        string[] grp = msel.Split(';'); var SCmd = "";
                        for (var b = 0; b < grp.Length; b++)
                        {
                            SCmd += GetFilter("Group " + grp[b], SearchCmd, 0, "", cnb, "", null, null, cnc)
                                    + ",";
                            SCmd = SCmd.Replace(c("dlcm_SearchFields"), "ID");
                            SCmd = SCmd.Replace("ORDER BY " + c("dlcm_OrderOfFields"), "");

                            SCmd = SCmd.Replace(")  ,", ") OR ID IN (");
                            SCmd = SCmd.Replace("(SELECT ID FROM Main u WHERE u.ID IN (", "(");
                        }
                        SearchCmd = SCmd.Substring(0, SCmd.Length - 17);
                        SearchCmd = SearchCmd.Substring(0, SearchCmd.Length - 1) + ")";
                        SearchCmd = "SELECT ID FROM Main u WHERE ID in (" + GetSelectIDs(SearchCmd, cnb, cnc) + ")";
                        //var SearchCmd6 = ;SELECT * FROM Main u LEFT JOIN Groups AS m ON m.CDLC_ID = CSTR(u.ID) WHERE m.Groupz =  \"Zoe\"
                        //SearchCmd += "CSTR(u.ID) IN (" + "SELECT m.CDLC_ID FROM Groups AS m WHERE m.CDLC_ID = CSTR(u.ID) AND m.Groupz =  \"" + Filtertxt.Substring(6, Filtertxt.Length - 6).Trim() + "\"" + ")";
                        //if (!Filtertxt.Contains("Top " + c("dlcm_maxsongsinweekly") + "(weekly)"))
                        //    SearchCmd = "SELECT u.ID FROM Main u LEFT JOIN Groups AS m ON m.CDLC_ID = CSTR(u.ID) WHERE m.Groupz =  \"" + Filtertxt.Substring(6, Filtertxt.Length - 6).Trim() + "\"";// + ")";
                        //else
                        //{
                        //    SearchCmd = "SELECT Top " + c("dlcm_maxsongsinweekly") +
                        //     " CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + c("dlcm_HotGrp") + "\" ORDER BY ID DESC";
                        //    DataSet dvs = new DataSet();
                        //    dvs = SelectFromDB("Groups", SearchCmd, "", cnb, cnc);
                        //    var nores = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count == 0 ? 0 : dvs.Tables[0].Rows.Count;
                        //    var idss = "";
                        //    if (nores > 0)
                        //        for (var l = 0; l < nores; l++)
                        //            idss += dvs.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";
                        //    SearchCmd = "SELECT u.ID FROM Main u where u.ID  in (" + idss + "0)";
                    }
                    break;
                default:
                    break;
            }

            var fields = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM")) : SearchCmd.Length - 1).Replace("SELECT ", "");
            if (c("dlcm_FilterCompound") == "Yes")
            {
                //if (c("dlcm_FilterNot") == "Yes")
                //    //{
                //    SearchCmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM") + 6) : SearchCmd.Length - 1)
                //        + "(" + oldSearchCmd + ") WHERE ID not IN (" + SearchCmd.Replace(fields, "ID") + ") ORDER BY " + c("dlcm_OrderOfFields") + "";
                ////chbx_FilterNot.Checked = false;
                ////}
                //else
                SearchCmd = "SELECT ID FROM Main WHERE ID in (" + GetSelectIDs(SearchCmd, cnb, cnc) + ")"
                    + " UNION ALL " +
                    "SELECT ID FROM Main WHERE ID in (" + GetSelectIDs(oldSearchCmd, cnb, cnc) + ")";
                //SearchCmd = SearchCmd.Replace(SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")), "SELECT ID").Replace(";", "").Substring(0, SearchCmd.IndexOf(" ORDER BY "))
                //SearchCmd = SearchCmd.Replace("ID FROM", c("dlcm_SearchFields") + " FROM Main");
                //oldSearchCmd.Replace(oldSearchCmd.Substring(0, oldSearchCmd.IndexOf(" FROM")), "SELECT ID").Substring(0, SearchCmd.IndexOf(" ORDER BY "));
                //((SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM") > 0 ? (SearchCmd.IndexOf(" FROM") + 6) : SearchCmd.Length - 1)
                ////+ "(" + oldSearchCmd + ") " +
                //+" Main WHERE ID IN (" + oldSearchCmd.Replace(c("dlcm_SearchFields"), "ID")).Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "") + ") ORDER BY " + c("dlcm_OrderOfFields") + "").Replace(";", "");
                //SearchCmd = (SearchCmd.Length - SearchCmd.Replace("(", "").Length) == (SearchCmd.Length - SearchCmd.Replace(")", "").Length) ? SearchCmd : SearchCmd + ")";

            }
            else if (c("dlcm_FilterNot") == "Yes")
            {
                SearchCmd = "SELECT ID FROM Main WHERE ID in (" + GetSelectIDs(oldSearchCmd, cnb, cnc) + ") AND ID not in (" + GetSelectIDs(SearchCmd, cnb, cnc) + ")";
                //SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE ID IN (" + oldSearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "")
                //    + ") and ID NOT IN (" + SearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "") + ") ORDER BY " + c("dlcm_OrderOfFields");
                //SearchCmd = SearchCmd.Replace("Maiu", "Main u");

            }
            else if (c("dlcm_FilterOverlap") == "Yes")
            {
                SearchCmd = "SELECT ID FROM Main WHERE ID in (" + GetSelectIDs(SearchCmd, cnb, cnc) + ") AND ID in (" + GetSelectIDs(oldSearchCmd, cnb, cnc) + ")";
                //SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE ID IN (" + oldSearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "")
                //    + ") and ID NOT IN (" + SearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace(" ORDER BY " + c("dlcm_OrderOfFields"), "") + ") ORDER BY " + c("dlcm_OrderOfFields");
                //SearchCmd = SearchCmd.Replace("Maiu", "Main u");

            }

            //Speed up the re-run of any Query by only providing list of IDs
            var cmd = "SELECT ID FROM (" + SearchCmd.Replace(";", "").Replace(c("dlcm_SearchFields"), "ID").Replace("ORDER BY " + c("dlcm_OrderOfFields"), "") + ") order by ID DESC";
            cmd = cmd.Replace(", )", ")").Replace("WHERE )", ")");
            cmd = cmd.Replace("Maiu", "Main u");
            DataSet dms = new DataSet(); dms = SelectFromDB("Main", cmd, "", cnb, cnc);
            noOfRec = GetNoRec(dms, cnb, cnc);//dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
            var IDS = "0, ";
            if (noOfRec > 0)
                //{            }
                for (var l = 0; l < noOfRec; l++)
                    IDS += dms.Tables[0].Rows[l].ItemArray[0].ToString() + ", ";

            SearchCmd = "SELECT " + c("dlcm_SearchFields") + " FROM Main u WHERE u.ID IN (" + IDS + ")";
            SearchCmd = SearchCmd.Replace(", )", ")").Replace("WHERE )", ")");

            if (Filterorg == "Sorted by Groups value/Group added date")
            {
                SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", Main.")).Replace(" ID", " Main.ID");
                SearchCmd = SearchCmd.Replace(" FROM Main u", ", Groups.Date_Added FROM Main LEFT JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");

                //"" +" AND ((Groups.Date_Added  Is Not Null))" +
                //"INNER JOIN Groups ON Main.ID = val(Groups.CDLC_ID) WHERE Groups.Groups = \"" + txt_Groups.Text + "\"";
                //"LEFT JOIN Groups AS mn ON Groupz=\"" + Group + "\" AND Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                SearchCmd += " AND \"" + Group + "\" = Groups.Groupz ORDER BY Groups.Date_Added DESC; ";// + " WHERE Groups.Date_Added is not NULL ORDER BY Groups.Date_Added DESC"; //SearchCmd.Replace(c("dlcm_OrderOfFields"), "")
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
                //DataSet dts = new DataSet(); dts = SelectFromDB("Main", cmd, "", cnb, cnc);
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
            //    SearchCmd = SearchCmd.Replace(" FROM Main u", ", Groupz.Date_Added FROM Main OUTTER JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");
            //}
            //if (Filterorg == "Part of Any Group")
            //{
            //    SearchCmd = SearchCmd.Replace(c("dlcm_SearchFields"), c("dlcm_SearchFields").Replace(", ", ", Main.")).Replace(" ID", " Main.ID");
            //    SearchCmd = SearchCmd.Replace(" FROM Main u", " FROM Main INNER JOIN Groups ON (STR(Main.ID) = STR(Groups.CDLC_ID) AND Type=\"DLC\")");
            //}
            else if (SearchCmd.IndexOf("ORDER BY") < 1) SearchCmd += " ORDER BY " + (OrderAlt != "" ? OrderAlt : c("dlcm_OrderOfFields")) + " ";
            else SearchCmd += SearchCmd.Replace(c("dlcm_OrderOfFields"), "") + (OrderAlt != "" ? OrderAlt : c("dlcm_OrderOfFields")) + " ";

            return SearchCmd;
        }
        public static void manipulateHSAN2021(string hsanPath, OleDbConnection cnb, MainDBfields[] SongRecord)
        {
            var inputFilePath = hsanPath;//cache.psarc

            string textfile = "";// File.ReadAllText(inputFilePath);

            //for each timestamp in the xml file take the highest level entry
            if (File.Exists(inputFilePath + ".orig")) File.Copy(inputFilePath + ".orig", inputFilePath, true);
            var fxml = File.OpenText(inputFilePath);
            if (!File.Exists(inputFilePath + ".orig")) File.Copy(inputFilePath, inputFilePath + ".orig", true);
            //string tecst = "";
            string line;
            //var header = "";
            //var linedone = true;
            var songkey = "";
            //var footer = "";
            //var lastline = false; //if the last song is not removed then the end should be appended

            textfile = "{";
            textfile += "\n    \"Entries\" : {";
            var IDD = "";
            var linenew = "";
            //var cmd = "";
            line = fxml.ReadLine(); line = fxml.ReadLine();
            //Read and Save Header
            while ((line = fxml.ReadLine()) != null)
            {

                if (line.Contains("InsertRoot")) break; //got to the end so

                if ((line.Contains(": {\"") || line.Contains(":{\"")) && !line.Contains("Attributes") && !line.Contains("Tuning")) linenew = "";

                linenew += "\n" + line;

                if (line.Contains("\"SongKey\": \"") || line.Contains("\"SongKey\" : \""))
                {
                    songkey = "";
                    songkey = ((line.Trim().ToLower()).Replace("\"songkey\" : \"", "").Replace("\"songkey\": \"", "").Replace("\"", "")).Replace(",", "");

                    for (var jj = 0; jj <= SongRecord[0].NoRec.ToInt32() - 1; jj++)
                        if (SongRecord[jj].DLC_Name.ToLower() == songkey.ToLower())
                        {
                            IDD = "Yes";
                            break;
                        }
                }

                if (line.Contains("},"))
                {
                    if (IDD == "Yes")
                        textfile += linenew;
                    IDD = "";
                    linenew = "";
                }
            }
            textfile += "\n" + "    \"InsertRoot\" : \"Static.Songs.Headers\"";

            textfile += "\n" + "}";
            fxml.Close();
            File.WriteAllText(inputFilePath, textfile);
        }

        //public static void SaveProfileToDB(string oldprof, OleDbConnection cnb, SQLiteConnection cnc)
        public static void SaveProfileToDB(string oldprof, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            //Save Profiles

            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Profile_Name=\"" + oldprof + "\";", "", cnb, cnc);/*chbx_Configurations.Text*/
            var norec = GetNoRec(ds, cnb, cnc);//ds.Tables.Count < 1 ? 0 : ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                var cmd = "";
                //saving  connfig values to dba

                var norecs = 0;
                DataSet dsg = new DataSet(); dsg = SelectFromDB("Groups", "SELECT DISTINCT Comments, Groupz, ID FROM Groups WHERE Type=\"Profile\" AND Profile_Name=\"" + oldprof + "\"; ", "", cnb, cnc);/*c("dlcm_Configurations")*/
                norecs = GetNoRec(dsg, cnb, cnc);//dsg.Tables[0].Rows.Count;
                var rt = 0;
                // var t = ""; //var tt = "";
                if (norecs > 0)
                    //{
                    for (int j = 0; j < norecs; j++)
                        //{
                        //    if (dsg.Tables[0].Rows[j].ItemArray[0].ToString() == "dlcm_Groups")
                        //    {
                        //        t = ConfigRepository.Instance()[dsg.Tables[0].Rows[j].ItemArray[0].ToString()];
                        //        tt = dsg.Tables[0].Rows[j].ItemArray[1].ToString();
                        //    }
                        if (c(dsg.Tables[0].Rows[j].ItemArray[0].ToString()) != dsg.Tables[0].Rows[j].ItemArray[1].ToString())
                        {
                            //if (dsg.Tables[0].Rows[j].ItemArray[1].ToString() == "dlcm_AdditionalManipul89")
                            //    ;
                            cmd = "UPDATE Groups SET Groupz=\"" + c(dsg.Tables[0].Rows[j].ItemArray[0].ToString()) + "\" WHERE ID=" + dsg.Tables[0].Rows[j].ItemArray[2].ToString() + " " +
                                "AND Type=\"Profile\"  AND Profile_Name=\"" + oldprof + "\"";/*AND Comments=\"" + c(dss.Tables[0].Rows[j][0].ToString()) + "\"chbx_Configurations.Text*/
                            UpdateDB("Groups", cmd, cnb, cnc); rt++; //AltMet
                        }
                //}
                //}
                dsg.Dispose();
            }
        }

        static public string GetDropTunningInstr(string Tunning)
        {
            var tzt = "";
            switch (Tunning)
            {
                case "Eb":
                    tzt += "Half Step down/ Led 1 on the Pedal from Es ";
                    break;
                case "Eb Standard":
                    tzt += "Half Step down/ Led 1 on the Pedal from Es ";
                    break;
                case "D Standard":
                    tzt += "1 Step down/ Led 2 on the Pedal from Es ";
                    break;
                case "C# Standard":
                    tzt += "1 and a Half Step down/ Led 3 on the Pedal from Es ";
                    break;
                case "C Standard":
                    tzt += "2 Steps down/ Led 4 on the Pedal from Es ";
                    break;
                case "B Standard":
                    tzt += "2 and a Half Steps down/ Led 5 on the Pedal from Es ";
                    break;
                case "Bb Standard":
                    tzt += "3 Steps down/ Led 6 on the Pedal from Es ";
                    break;
                case "AStandard":
                    tzt += "3 and Half Steps down/ Led 7 on the Pedal from Es ";
                    break;
                case "AbStandard":
                    tzt += "3 and Half Steps down/ Led 7 on the Pedal from Es ";
                    break;
                case "Eb Drop Db":
                    tzt += "First Cord 2 Steps down the rest Half Step down/ Led 1 on the Pedal from dD ";
                    break;
                case "D Drop C":
                    tzt += "First Cord 2 Steps down the rest Half Step down/ Led 2 on the Pedal from dD ";
                    break;
                case "C# Drop B":
                    tzt += "First Cord 2 Steps down the rest 1 Step down/ Led 3 on the Pedal from dD ";
                    break;
                case "C Drop A#":
                    tzt += "First Cord 2 Steps down the rest 1 and a Half Step down/ Led 4 on the Pedal from dD ";
                    break;
                case "B Drop A":
                    tzt += "First Cord 2 Steps down the rest 2 Steps down/ Led 5 on the Pedal from dD ";
                    break;
                case "Bb Drop Ab":
                    tzt += "First Cord 2 Steps down the rest 2 and a Half Steps down/ Led 6 on the Pedal from dD ";
                    break;
                case "A Drop G":
                    tzt += "First Cord 2 Steps down the rest 3 Steps down/ Led 7 on the Pedal from dD ";
                    break;
                default:
                    break;

                    //<TuningDefinition Version="RS2012" Name="EFlat" UIName="Eb" >< Tuning string0="-1" string1="-1" string2="-1" string3="-1" string4="-1" string5 = "-1" />
                    //<TuningDefinition Version="RS2014" Name="EbStandard" UIName="Eb Standard"> <Tuning string0="-1" string1="-1" string2="-1" string3="-1" string4="-1" string5="-1"/>
                    //<TuningDefinition Version="RS2014" Name="DStandard" UIName="D Standard"><Tuning string0="-2" string1="-2" string2="-2" string3="-2" string4="-2" string5="-2"/>
                    //<TuningDefinition Version="RS2014" Name="C#Standard" UIName="C# Standard"><Tuning string0="-3" string1="-3" string2="-3" string3="-3" string4="-3" string5="-3"/>
                    //<TuningDefinition Version="RS2014" Name="CStandard" UIName="C Standard"><Tuning string0="-4" string1="-4" string2="-4" string3="-4" string4="-4" string5="-4"/>
                    //<TuningDefinition Version="RS2014" Name="BStandard" UIName="B Standard" Custom="true"><Tuning string0="-5" string1="-5" string2="-5" string3="-5" string4="-5" string5="-5"/>
                    //<TuningDefinition Version="RS2014" Name="BbStandard" UIName="Bb Standard" Custom="true"><Tuning string0="-6" string1="-6" string2="-6" string3="-6" string4="-6" string5="-6"/>
                    //<TuningDefinition Version="RS2014" Name="AStandard" UIName="A Standard" Custom="true"><Tuning string0="-7" string1="-7" string2="-7" string3="-7" string4="-7" string5="-7"/>
                    //<TuningDefinition Version="RS2014" Name="AbStandard" UIName="Ab Standard" Custom="true"><Tuning string0="-7" string1="-7" string2="-7" string3="-7" string4="-7" string5="-7"/>
                    //        SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb\", \"Eb Standard\", \"D Standard\", \"C# Standard\", \"C Standard\", \"B Standard\"" +
                    //            ", \"Bb Standard\", \"AStandard\", \"AbStandard\"))";

                    //case "Digitech Drop compatible (Straight down conv from Drop D)":
                    //    SearchCmd += "ID IN (SELECT DISTINCT CDLC_ID FROM Arrangements p WHERE p.Tunning IN (\"Eb Drop Db\", \"D Drop C\", \"C#DropB\", \"C Drop A#\", \"B Drop A\"" +
                    //        ", \"BbDropAb\", \"A Drop G\"))";
                    //<TuningDefinition Version="RS2014" Name="EbDropDb" UIName="Eb Drop Db"><Tuning string0="-3" string1="-1" string2="-1" string3="-1" string4="-1" string5="-1"/>
                    //<TuningDefinition Version="RS2014" Name="DDropC" UIName="D Drop C"><Tuning string0="-4" string1="-2" string2="-2" string3="-2" string4="-2" string5="-2"/>
                    //<TuningDefinition Version="RS2014" Name="C#DropB" UIName="C# Drop B" Custom="true"><Tuning string0="-5" string1="-3" string2="-3" string3="-3" string4="-3" string5="-3"/>
                    //<TuningDefinition Version="RS2014" Name="CdropA#" UIName="C Drop A#" Custom="true"><Tuning string0="-6" string1="-4" string2="-4" string3="-4" string4="-4" string5="-4"/>
                    //<TuningDefinition Version="RS2014" Name="BDropA" UIName="B Drop A" Custom="true"><Tuning string0="-7" string1="-5" string2="-5" string3="-5" string4="-5" string5="-5"/>
                    //<TuningDefinition Version="RS2014" Name="BbDropAb" UIName="Bb Drop Ab" Custom="true"><Tuning string0="-8" string1="-6" string2="-6" string3="-6" string4="-6" string5="-6"/>
            }
            return tzt;
        }

        public static string Get_Tracks_WBonusAndParts(string id, bool arrangoff, string folder)
        {
            var cmd = "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name" +
                " FROM Arrangements WHERE CDLC_ID=" + id + GetArrOfficSQLTxt(arrangoff);
            DataSet dcs = new DataSet(); dcs = SelectFromDB("Arrangements", cmd, "", cnb, cnc);
            var noOfRezc = GetNoRec(dcs, cnb, cnc);//dcs.Tables[0].Rows.Count;
                                                   //float FirstLyric = 5000;
                                                   //float FirstVocal = 0;
            bool readmanually = false;
            var B = ""; var L = ""; var R = ""; var C = ""; var V = "";
            var arangFile = Directory.Exists(folder + "\\songs\\arr") ? Directory.GetFiles(folder + "\\songs\\arr", "*.*", System.IO.SearchOption.AllDirectories) : null;
            if (noOfRezc < 1)
            {
                readmanually = true;
                noOfRezc = arangFile is null ? 0 : arangFile.Length;
            }
            for (var j = 0; j <= noOfRezc - 1; j++)
            {
                var Bonus = "";
                var ArrangementType = "";
                var RouteMask = "";
                var Arrangement_Name = "";
                var Part = "0";
                if (!readmanually)
                {
                    ArrangementType = dcs.Tables[0].Rows[j].ItemArray[3].ToString();
                    RouteMask = dcs.Tables[0].Rows[j].ItemArray[4].ToString();
                    Arrangement_Name = dcs.Tables[0].Rows[j].ItemArray[7].ToString();
                    Bonus = dcs.Tables[0].Rows[j].ItemArray[1].ToString();
                    Part = dcs.Tables[0].Rows[j].ItemArray[6].ToString();
                }
                else
                {
                    try
                    {
                        var xmlContent = Song2014.LoadFromFile(arangFile[j]);
                        //if (xmlContent == null) xmlContent = Vocals.LoadFromFile(arangFile[j]); 
                        if (xmlContent == null)
                        {
                            var xmlContents = Vocals.LoadFromFile(arangFile[j]);
                            if (xmlContents != null)
                                V += "V";
                            continue;
                        }
                        ArrangementType = "";
                        RouteMask = xmlContent.ArrangementProperties.RouteMask.ToString();
                        Arrangement_Name = xmlContent.Arrangement.ToString();
                        Bonus = xmlContent.ArrangementProperties.BonusArr.ToString() != "0" ? "true" : "false";
                        Part = xmlContent.Part.ToString() == "" ? "0" : xmlContent.Part.ToString();
                    }
                    catch (Exception Exx)
                    {
                        var xmlContent = Vocals.LoadFromFile(arangFile[j]);
                        if (xmlContent != null) V += "V";
                        continue;
                    }
                }

                //data.Arrangements[n].ArrangementName = (sds == "3" || sds == "Bass" ? ArrangementName.Bass : (sds == "0" || sds == "Lead" ? ArrangementName.Lead :
                //                        (sds == "4" || sds == "Vocals" ? ArrangementName.Vocals : (sds == "1" || sds == "Rhythm" ? ArrangementName.Rhythm :
                //                        (sds == "6" || sds == "ShowLights" ? ArrangementName.ShowLights : (sds == "2" || sds == "Combo" ? ArrangementName.Bass : ArrangementName.Rhythm))))));

                if (ArrangementType == "Vocal")
                    V += "V";
                if (ArrangementType == "ShowLight" || ArrangementType == "4") continue;
                if (/*Arrangement_Name == "Combo"*/ RouteMask == "Combo") C += "C";
                else if (RouteMask == "Bass" || RouteMask == "4" || Arrangement_Name == "Bass" || Arrangement_Name == "3") B += "B";
                else if (RouteMask == "Lead" || Arrangement_Name == "0" || RouteMask == "1" || RouteMask == "1") L += "L";
                else if (RouteMask == "Rhythm" || RouteMask == "2" || Arrangement_Name == "Rhythm" || Arrangement_Name == "2") R += "R";

                if (Bonus.ToLower() == "true")
                {
                    if (Arrangement_Name == "3" || Arrangement_Name == "Bass" || RouteMask == "Bass" || RouteMask == "4") B += "b";
                    if ( /*Arrangement_Name == "Combo"*/ RouteMask == "Combo") C += "b";
                    else
                    {
                        if (RouteMask == "Lead" || Arrangement_Name == "0" || RouteMask == "1" || Arrangement_Name == "Lead") L += "b";
                        if (RouteMask == "Rhythm" || RouteMask == "2" || Arrangement_Name == "Rhythm" || Arrangement_Name == "2") R += "b";
                    }
                }
                if (Part is not null && Part !="")
                   if( double.Parse(Part) == 2){
                    if (RouteMask == "Bass" || Arrangement_Name == "3" || Arrangement_Name == "Bass" || RouteMask == "4") B += "a";
                    if (/*Arrangement_Name == "Combo"*/ RouteMask == "Combo") C += "a";
                    else
                    {
                        if (RouteMask == "Lead" || RouteMask == "1" || Arrangement_Name == "Lead" || Arrangement_Name == "0") L += "a";
                        if (RouteMask == "Rhythm" || RouteMask == "2" || Arrangement_Name == "Rhythm" || Arrangement_Name == "2") R += "a";
                    }
                }
            }
            var ret = L + B + R + C + V;
            return ret;

        }
        public static string Manipulate_strings(string words, int k, bool ifn, bool orig_flag, bool bassRemoved, UtilitiesFunctions.MainDBfields[] SongRecord
                    //, string sep1, string sep2, bool beta, bool sort, bool arrangoff, SQLiteConnection cnz)//, string always_grp, string grp)
                    , string sep1, string sep2, bool beta, bool sort, bool arrangoff, SQLite.SQLiteConnection cnc)//, string always_grp, string grp)
        {

            //2. Read from DB
            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = "";// sep1;
            var readt = false;
            var oldtxt = "";
            var last_ = 0;
            //OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            //var arng = "";


            for (i = 0; i <= txt.Length - 1; i++)
            {
                curtext = txt[i].ToString();
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
                    last_ = fulltxt.Length;
                }

                if (readt == true)
                    curelem += curtext;
                else fulltxt += curtext;

                bool origQAs = true;
                if (orig_flag) origQAs = false;

                oldtxt = fulltxt;
                string tzt = "";
                if (curtext == ">")
                {
                    readt = false;
                    switch (curelem)
                    {
                        case "<Artist>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Artist.Length > 4 && sort)
                                tzt = MoveTheAtEnd(SongRecord[k].Artist);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Artist;// sep1 +  + sep2;
                            break;
                        case "<Title>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Song_Title.Length > 4 && sort)
                                tzt = MoveTheAtEnd(SongRecord[k].Song_Title);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Song_Title;// sep1 +  + sep2;
                            break;
                        case "<Album>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Album.Length > 4)                                                                                         //    data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                tzt = MoveTheAtEnd(SongRecord[k].Album);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Album;// sep1 + S + sep2;
                            break;
                        case "<Artist Sort>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Artist_Sort.Length > 4)
                                tzt = MoveTheAtEnd(SongRecord[k].Artist_Sort);// + sep1 + sep2;
                            else tzt = SongRecord[k].Artist_Sort;// sep1 +  + sep2;
                            break;
                        case "<Title Sort>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Song_Title_Sort.Length > 4)                                                                                         //    data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                tzt = MoveTheAtEnd(SongRecord[k].Song_Title_Sort);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Song_Title_Sort;// sep1 +  + sep2;
                            break;
                        case "<Album Sort>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Album_Sort.Length > 4)                                                                                         //    data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                tzt = MoveTheAtEnd(SongRecord[k].Album_Sort);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Album_Sort;// sep1 +  + sep2;
                            break;
                        case "<Artist Short>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Artist.Length > 4 && sort && SongRecord[k].Artist_ShortName == "")
                                tzt = MoveTheAtEnd(SongRecord[k].Artist);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Artist_ShortName != "" ? SongRecord[k].Artist_ShortName : SongRecord[k].Artist;
                            break;
                        case "<Album Short>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Album.Length > 4 && sort && SongRecord[k].Album_ShortName == "")
                                tzt = MoveTheAtEnd(SongRecord[k].Album);// sep1 +  + sep2;
                            else tzt = SongRecord[k].Album_ShortName != "" ? SongRecord[k].Album_ShortName : SongRecord[k].Album;
                            break;
                        case "<Author>":
                            tzt = "by " + SongRecord[k].Author;
                            break;
                        case "<Version>":
                            tzt = "v." + SongRecord[k].Version;
                            break;
                        case "<DLCName>":
                            tzt = SongRecord[k].DLC_Name;
                            break;
                        case "<Track No.>":
                            tzt = ((SongRecord[k].Track_No == "" || SongRecord[k].Track_No == "0" || SongRecord[k].Track_No == "00") ? "" : ("-" + SongRecord[k].Track_No));
                            break;
                        case "<Year>":
                            tzt = SongRecord[k].Album_Year;
                            break;
                        case "<Rating>":
                            tzt = ((SongRecord[k].Rating == "") ? "" : "rating." + SongRecord[k].Rating);
                            break;
                        case "<RatingShort>":
                            tzt = ((SongRecord[k].Rating == "") ? "" : "r." + SongRecord[k].Rating);
                            break;
                        case "<Alternate Version>":
                            tzt = SongRecord[k].Alternate_Version_No == "" ? "" : "a." + SongRecord[k].Alternate_Version_No;
                            break;
                        case "<Duplicate>":
                            tzt = SongRecord[k].Duplicates == "" ? "" : "d." + SongRecord[k].Duplicate_Of;
                            break;
                        case "<Has Capo>":
                            tzt = SongRecord[k].Has_Capo == "" ? "" : "_Capo";
                            break;
                        case "<Has Official>":
                            DataSet dqs = new DataSet(); dqs = SelectFromDB("Main", "SELECT ID, Is_Original FROM Main " +
                                "WHERE Duplicate_Of=\"" + SongRecord[k].ID + "\" OR ID=" + SongRecord[k].ID +
                                (SongRecord[k].Duplicate_Of.ToString().ToInt32() == 0 || SongRecord[k].Duplicate_Of == "" ? "" : " OR " + "Duplicate_Of=\"" + SongRecord[k].Duplicate_Of + "\" OR ID=" + SongRecord[k].Duplicate_Of)
                                + ";", "", cnb, cnc);//,Comments ,Comments
                            var noOfRect = GetNoRec(dqs, cnb, cnc);//dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;

                            for (var j = 0; j <= noOfRect - 1; j++)
                            {
                                if (dqs.Tables[0].Rows[j].ItemArray[1].ToString() == "Yes")
                                    tzt += (SongRecord[k].Duplicate_Of == dqs.Tables[0].Rows[j].ItemArray[0].ToString() && noOfRect > 1) ? "IsOfficialBUTHasAlternate" : (noOfRect == 1 ? "" : "_HasOfficial");
                                break;
                            }
                            //tzt = SongRecord[k].Has_Other_Officials == "" ? "" : "o." + SongRecord[k].alter;
                            break;
                        //case "<Descr.>":
                        //    tzt = SongRecord[k].Comments;
                        //    break;
                        case "<Description>":
                            tzt = (SongRecord[k].Description == "" ? "" : "Notes: " + SongRecord[k].Description) +
                                (SongRecord[k].Live_Details == "" ? "" : "-Details:" + SongRecord[k].Live_Details) +
                                (SongRecord[k].ToDos == "" ? "" : "-todos:" + SongRecord[k].ToDos) +
                                (SongRecord[k].ToneDetails == "" ? "" : "-ToneDetails:" + SongRecord[k].ToneDetails) +
                            (SongRecord[k].FilesMissingIssues == "" ? "" : "-FilesMissingIssues:" + SongRecord[k].FilesMissingIssues);
                            break;
                        case "<Tuning>":
                            tzt = SongRecord[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            tzt = ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((SongRecord[k].Has_Bass == "Yes") ? "B" : ""); //not yet done for all arrangements
                            break;
                        case "<Multi Track Details>":
                            tzt = SongRecord[k].MultiTrack_Version == "" ? "" : "-MultiTrack " + SongRecord[k].MultiTrack_Version;//?
                            break;
                        case "<Live Details>":
                            tzt = SongRecord[k].Live_Details;
                            break;
                        case "<Attributes extended>":
                            tzt += Manipulate_strings("<Bonus>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Live>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Acoustic>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Instrumental>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<EP>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Uncensored>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<SoundTrack>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Single>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<FullAlbum>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Manipulated>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Deluxe>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<GreatestHits>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Midi>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<AmateurCover>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<GameSoundtrack>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<TVTheme>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<IntheWorks>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Remastered>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Karaoke>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Cover>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Demo>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Remix>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Lyrics Language>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Beta>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc) == "0" ? "Beta" : "";
                            tzt += Manipulate_strings("<Medley>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<MultiStrings>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Official>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<AudioBitrate>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Alternate Version>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Duplicate>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Has Official>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Has Capo>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += Manipulate_strings("<Has Official>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += "_" + Manipulate_strings("<Import Date>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            tzt += "_" + Manipulate_strings("<Volume>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);
                            // tzt += "_" + Manipulate_strings("<DigitechDropDetails>", 0, false, false, bassRemoved, SongRecord, "[", "]", beta, false, false, cnc);

                            tzt = tzt == "" || tzt == "__" ? "" : "Attributes: " + tzt;
                            break;
                        case "<Group>":
                            tzt = SongRecord[k].Groups;
                            break;
                        case "<Groups>":
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Group", "SELECT DISTINCT(Groupz) FROM Groups WHERE CDLC_ID=\"" + SongRecord[k].ID + "\" AND Type=\"DLC\" AND Groupz<>'Packing' ORDER BY Groupz", "", cnb, cnc);//,Comments ,Comments
                            var noOfRectz = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;
                            tzt = noOfRectz > 0 ? "Groups: " : "";
                            for (var j = 0; j <= noOfRectz - 1; j++)
                            {
                                var grp = dvs.Tables[0].Rows[j].ItemArray[0].ToString() + ", ";
                                tzt += grp;
                            }
                            break;
                        case "<GroupIndex>":
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" AND Groupz<>'Packing' ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRehc = GetNoRec(dbs, cnb, cnc);//dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;
                            if (noOfRehc > 0) tzt = dbs.Tables[0].Rows[0].ItemArray[0].ToString();
                            break;
                        case "<GroupIndexAndName>":
                            DataSet dös = new DataSet(); dös = SelectFromDB("Groups", "SELECT TOP 1 Groupz, Comments FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" AND Groupz<>'Packing' ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRehq = GetNoRec(dös, cnb, cnc);//dqs.Tables.Count > 0 ? dqs.Tables[0].Rows.Count : 0;
                            if (noOfRehq > 0) tzt = dös.Tables[0].Rows[0].ItemArray[0].ToString() + dös.Tables[0].Rows[0].ItemArray[1].ToString();
                            break;
                        case "<FirstGroupIndexAndName>":
                            DataSet dps = new DataSet(); dps = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + SongRecord[k].ID + "\" AND Groupz<>'Packing' ORDER BY Comments", "", cnb, cnc);
                            var noOfRepq = GetNoRec(dps, cnb, cnc); //dps.Tables.Count > 0 ? dps.Tables[0].Rows.Count : 0;
                            if (noOfRepq > 0) tzt = dps.Tables[0].Rows[0].ItemArray[0].ToString() + dps.Tables[0].Rows[0].ItemArray[1].ToString();
                            break;
                        case "<BetaOrGroupIndex>":
                            DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" AND Groupz<>'Packing' ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRegc = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count > 0 ? dgs.Tables[0].Rows.Count : 0;
                            if (noOfRegc > 0)
                                tzt = dgs.Tables[0].Rows[0].ItemArray[0].ToString();
                            else
                            {
                                if (beta)
                                    fulltxt = "0" + fulltxt;
                                else
                                    fulltxt = ((SongRecord[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
                            }
                            break;
                        case "<Beta>":
                            if (beta) fulltxt = "0" + fulltxt;
                            else fulltxt = ((SongRecord[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
                            break;
                        case "<Broken>":
                            fulltxt = ((SongRecord[k].Is_Broken == "Yes") ? "Brkn-" : "") + fulltxt;
                            break;
                        case "<File Name>":
                            tzt = SongRecord[k].Current_FileName;
                            break;
                        case "<Bonus>":
                            tzt = ((SongRecord[k].Has_Bonus_Arrangement == "Yes") ? "wBonus" : ""); //not yet done for all arrangements
                            break;
                        case "<Live>":
                            tzt = ((SongRecord[k].Is_Live == "Yes") ? "-Live " + SongRecord[k].Live_Details : "");
                            break;
                        case "<Acoustic>":
                            tzt = ((SongRecord[k].Is_Acoustic == "Yes") ? "-Acoustic " + SongRecord[k].Live_Details : "");
                            break;
                        case "<Instrumental>":
                            tzt = ((SongRecord[k].Is_Instrumental == "Yes") ? "-Instrumental " : "");
                            break;
                        case "<EP>":
                            tzt = ((SongRecord[k].Is_EP == "Yes") ? "-EP " : "");
                            break;
                        case "<Uncensored>":
                            tzt = ((SongRecord[k].Is_Uncensored == "Yes") ? "-Uncensored " : "");
                            break;
                        case "<SoundTrack>":
                            tzt = ((SongRecord[k].Is_Soundtrack == "Yes") ? "-SoundTrack " : "");
                            break;
                        case "<Single>":
                            tzt = ((SongRecord[k].Is_Single == "Yes") ? "-Single " : "");
                            break;
                        case "<FullAlbum>":
                            tzt = ((SongRecord[k].Is_FullAlbum == "Yes") ? "-FullAlbum " : "");
                            break;
                        case "<Manipulated>":
                            tzt = ((SongRecord[k].ImprovedWithDM == "Yes") ? "-Manipulated " : "");
                            break;
                        case "<Deluxe>":
                            tzt = ((SongRecord[k].Is_Deluxe == "Yes") ? "-Deluxe " : "");
                            break;
                        case "<GreatestHits>":
                            tzt = ((SongRecord[k].Is_GreatestHits == "Yes") ? "-GreatestHits " : "");
                            break;
                        case "<Midi>":
                            tzt = ((SongRecord[k].Is_Midi == "Yes") ? "-Midi " : "");
                            break;
                        case "<AmateurCover>":
                            tzt = ((SongRecord[k].Is_AmateurCover == "Yes") ? "-AmateurCover " : "");
                            break;
                        case "<GameSoundtrack>":
                            tzt = ((SongRecord[k].Is_GameSoundtrack == "Yes") ? "-GameSoundtrack " : "");
                            break;
                        case "<TVTheme>":
                            tzt = ((SongRecord[k].Is_TVTheme == "Yes") ? "-TVTheme " : "");
                            break;
                        case "<IntheWorks>":
                            tzt = ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                            break;
                        //case "<IntheWorksWDetails>":
                        //    tzt = ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                        //    break;
                        case "<Remastered>":
                            tzt = ((SongRecord[k].Is_Remastered == "Yes") ? "-Remastered " : "");
                            break;
                        case "<Karaoke>":
                            tzt = ((SongRecord[k].Is_Karaoke == "Yes") ? "-Karaoke " : "");
                            break;
                        case "<Cover>":
                            tzt = ((SongRecord[k].Is_Cover == "Yes") ? "-Cover " : "");
                            break;
                        case "<Demo>":
                            tzt = ((SongRecord[k].Is_Demo == "Yes") ? "-Demo " : "");
                            break;
                        case "<Remix>":
                            tzt = ((SongRecord[k].Is_Remix == "Yes") ? "-Remix " : "");
                            break;
                        case "<MultiStrings>":
                            tzt = ((SongRecord[k].Is_MultiStrings == "Yes") ? "-MultiStrings " : "");
                            break;
                        case "<Medley>":
                            tzt = ((SongRecord[k].Is_Medley == "Yes") ? "-Medley " : "");
                            break;
                        case "<Official>":
                            tzt = ((SongRecord[k].Is_Original == "Yes") ? "-Official " : "");
                            break;
                        case "<ToDos>":
                            tzt = SongRecord[k].ToDos;
                            break;
                        case "<Volume>":
                            tzt = SongRecord[k].Volume + " volume";
                            break;
                        case "<Preview Volume>":
                            tzt = SongRecord[k].Preview_Volume + " volume";
                            break;
                        case "<Import Date>":
                            tzt = SongRecord[k].Import_Date + " imported";
                            break;
                        case "<AudioBitrate>":
                            tzt = "-Audio:" + SongRecord[k].audioBitrate + "kb " + SongRecord[k].audioSampleRate + "kHz" + (SongRecord[k].Has_Had_Audio_Changed == "Yes" ? "_DLCMDownsampled" : "");
                            break;
                        case "<Lyrics Language>":
                            tzt = " " + SongRecord[k].LyricsLanguage + " ";
                            break;
                        case "<Track Tile (potentially) removed details>":
                            tzt = ((SongRecord[k].Is_Acoustic == "Yes") ? "-Acoustic " + SongRecord[k].Live_Details : "");
                            tzt += ((SongRecord[k].Is_Live == "Yes") ? "-Live " : "");
                            //tzt += ((SongRecord[k].Is_Instrumental == "Yes") ? "-Instrumental " : "");
                            //tzt += ((SongRecord[k].Is_EP == "Yes") ? "-EP " : "");
                            tzt += ((SongRecord[k].Is_Uncensored == "Yes") ? "-Uncensored " : "");
                            //tzt += ((SongRecord[k].Is_Soundtrack == "Yes") ? "-SoundTrack " : "");
                            //tzt += ((SongRecord[k].Is_Single == "Yes") ? "-Single " : "");
                            tzt += ((SongRecord[k].Is_FullAlbum == "Yes") ? "-FullAlbum  " : "");
                            //tzt += ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                            //tzt += ((SongRecord[k].Is_Remastered == "Yes") ? "-Remastered " : "");
                            tzt += ((SongRecord[k].Is_Karaoke == "Yes") ? "-Karaoke " : "");
                            //tzt += ((SongRecord[k].Is_Cover == "Yes") ? "-Cover " : "");
                            //tzt += ((SongRecord[k].Is_Demo == "Yes") ? "-Demo  " : "");
                            //tzt += ((SongRecord[k].Is_Remix == "Yes") ? "-Remix " : "");
                            //tzt += ((SongRecord[k].Is_GreatestHits == "Yes") ? "-GreatestHits  " : "");
                            //tzt += ((SongRecord[k].Is_TVTheme == "Yes") ? "-TVTheme " : "");
                            tzt += ((SongRecord[k].Is_AmateurCover == "Yes") ? "-AmateurCover " : "");
                            tzt += ((SongRecord[k].Is_Midi == "Yes") ? "-Midi " : "");
                            tzt += ((SongRecord[k].Has_Capo == "Yes") ? "-Capo " : "");
                            tzt += ((SongRecord[k].Is_Medley == "Yes") ? "-Medley " : "");
                            tzt += ((GetDropTunningInstr(SongRecord[k].Tunning) != "") ? "-DigitecDropDCompatible " : "");
                            //tzt += ((SongRecord[k].Is_GameSoundtrack == "Yes") ? "-GameSoundtrack  " : "");
                            tzt += ((SongRecord[k].Is_MultiStrings == "Yes") ? "-MultiStrings: " + SongRecord[k].MultiTrack_Version : "");
                            //tzt += ((SongRecord[k].Is_Deluxe == "Yes") ? "-Deluxe " : "");
                            //no manipulated/improved at not a property of song more of DLCM
                            break;
                        case "<CDLC_ID>":
                            tzt = SongRecord[k].ID;
                            break;
                        case "<DLCM Release>":
                            tzt = c("dlcm_DLCManager_Release");
                            break;
                        case "<DLCM ReleaseName>":
                            tzt = c("dlcm_DLCManager_ReleaseName");
                            break;
                        case "<DLCM ReleaseVersion>":
                            tzt = c("dlcm_DLCManager_ReleaseVersion");
                            break;
                        case "<Date>":
                            tzt = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
                            break;
                        case "<Capo>":
                            tzt = ((SongRecord[k].Has_Capo == "Yes") ? "-Capo " : "");
                            break;
                        case "<CapoFret>":
                            DataSet dos = new DataSet(); dos = SelectFromDB("Arrangements", "SELECT CapoFret, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfReoc = GetNoRec(dos, cnb, cnc);//dos.Tables[0].Rows.Count;

                            for (var j = 0; j <= noOfReoc - 1; j++)
                            {
                                var CapoFret = dos.Tables[0].Rows[j].ItemArray[0].ToString();
                                tzt = "CapoOn-" + CapoFret + " ";
                                break;
                            }
                            break;
                        case "<DigitechDropFlag>":
                            DataSet dys = new DataSet(); dys = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRekc = GetNoRec(dys, cnb, cnc);//dys.Tables[0].Rows.Count;

                            for (var j = 0; j <= noOfRekc - 1; j++)
                            {
                                var Tunning = dys.Tables[0].Rows[j].ItemArray[0].ToString();
                                if (Tunning == "Eb" || Tunning == "Eb Standard" || Tunning == "D Standard" || Tunning == "C# Standard" || Tunning == "C Standard" || Tunning == "B Standard"
                                     || Tunning == "Bb Standard" || Tunning == "AStandard" || Tunning == "AbStandard")
                                {
                                    tzt = "DigitechDropFromEs";
                                    break;
                                }
                                else
                                     if (Tunning == "Eb Drop Db" || Tunning == "D Drop C" || Tunning == "C# Drop B" || Tunning == "C Drop A#" || Tunning == "B Drop A" || Tunning == "Bb Drop Ab"
                                     || Tunning == "Bb Standard")
                                {
                                    tzt = "DigitechDropFromDropD";
                                    break;
                                }
                            }
                            break;
                        //case "<DigitechDropEstandardDirectFlag>":
                        //    DataSet dhs = new DataSet(); dhs = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb, cnc);
                        //    var noOfRemc = dhs.Tables[0].Rows.Count;

                        //    for (var j = 0; j <= noOfRemc - 1; j++)
                        //    {
                        //        var Tunning = dhs.Tables[0].Rows[j].ItemArray[0].ToString();
                        //        if (Tunning == "Eb" || Tunning == "Eb Standard" || Tunning == "D Standard" || Tunning == "C# Standard" || Tunning == "C Standard" || Tunning == "B Standard"
                        //             || Tunning == "Bb Standard" || Tunning == "AStandard" || Tunning == "AbStandard")
                        //        {
                        //            tzt = "DigitechDropFromEs";
                        //            break;
                        //        }
                        //    }
                        //    break;
                        case "<DigitechDropDetails>":/*EstandardDirect*/
                            DataSet dns = new DataSet(); dns = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRenc = GetNoRec(dns, cnb, cnc);//dns.Tables[0].Rows.Count;

                            for (var j = 0; j <= noOfRenc - 1; j++)
                            {
                                var Tunning = dns.Tables[0].Rows[j].ItemArray[0].ToString();
                                var ArrangementType = dns.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dns.Tables[0].Rows[j].ItemArray[4].ToString();
                                var Arrangement_Name = dns.Tables[0].Rows[j].ItemArray[7].ToString();
                                if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                                var instr = RouteMask == "Bass" ? " B-" : (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C-" : (RouteMask == "Rhythm" ? " R-" : (RouteMask == "Lead" ? " L-" : "?")));
                                var a = GetDropTunningInstr(Tunning);
                                tzt += a == "" ? "" : "_" + instr + (tzt.Contains(a) ? "ditto-" + Tunning : a);
                            }
                            break;
                        //case "<DigitechDropDropDDirectFlag>":
                        //    DataSet dhz = new DataSet(); dhz = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb, cnc);
                        //    var noOfRezc = dhz.Tables[0].Rows.Count;

                        //    for (var j = 0; j <= noOfRezc - 1; j++)
                        //    {
                        //        var Tunning = dhz.Tables[0].Rows[j].ItemArray[0].ToString();
                        //        if (Tunning == "Eb" || Tunning == "Eb Standard" || Tunning == "D Standard" || Tunning == "C# Standard" || Tunning == "C Standard" || Tunning == "B Standard"
                        //             || Tunning == "Bb Standard" || Tunning == "AStandard" || Tunning == "AbStandard")
                        //        {
                        //            tzt = "DigitechDropFromDropD";
                        //            break;
                        //        }
                        //    }
                        //    break;
                        //case "<DigitechDropDDirectDetails>":
                        //    DataSet drs = new DataSet(); dns = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb, cnc);
                        //    var noOfRerc = drs.Tables[0].Rows.Count;

                        //    for (var j = 0; j <= noOfRerc - 1; j++)
                        //    {
                        //        var Tunning = drs.Tables[0].Rows[j].ItemArray[0].ToString();
                        //        var ArrangementType = drs.Tables[0].Rows[j].ItemArray[3].ToString();
                        //        if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                        //        var instr = ArrangementType == "Bass" ? "B-" : (ArrangementType == "Lead" ? "L-" : (ArrangementType == "Rhythm" ? "R-" : (ArrangementType == "Combo" ? "C-" : "?")));
                        //        switch (Tunning)
                        //        {
                        //            case "Eb":
                        //                tzt += instr + "Half Step down/ Led 1 on the Pedal from dD ";
                        //                break;
                        //            case "Eb Standard":
                        //                tzt += instr + "Half Step down/ Led 1 on the Pedal from dD ";
                        //                break;
                        //            case "D Standard":
                        //                tzt += instr + "1 Step down/ Led 2 on the Pedal from dD ";
                        //                break;
                        //            case "C# Standard":
                        //                tzt += instr + "1 and a Half Step down/ Led 3 on the Pedal from dD ";
                        //                break;
                        //            case "C Standard":
                        //                tzt += instr + "2 Steps down/ Led 4 on the Pedal from dD ";
                        //                break;
                        //            case "B Standard":
                        //                tzt += instr + "2 and a Half Steps down/ Led 5 on the Pedal from dD ";
                        //                break;
                        //            case "Bb Standard":
                        //                tzt += instr + "3 Steps down/ Led 6 on the Pedal from dD ";
                        //                break;
                        //            case "AStandard":
                        //                tzt += instr + "3 and Half Steps down/ Led 7 on the Pedal from dD ";
                        //                break;
                        //            case "AbStandard":
                        //                tzt += instr + "3 and Half Steps down/ Led 7 on the Pedal from dD ";
                        //                break;
                        //        }
                        //    }
                        //    break;
                        case "<Random5>":
                            Random randomp = new Random();
                            int rn = randomp.Next(0, 100000);
                            tzt = rn.ToString();
                            break;
                        case "<Space>":
                            tzt = " ";
                            break;
                        case "<Avail. Tracks>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Lead == "Yes") ? "L" : "") + ((SongRecord[k].Has_Combo == "Yes") ? "C" : "") + ((SongRecord[k].Has_Rhythm == "Yes") ? "R" : "") + ((SongRecord[k].Has_Vocals == "Yes") ? "V" : "");
                            break;
                        case "<Avail. Tracks w Bonus>":
                            tzt = Get_Tracks_WBonusAndParts(SongRecord[k].ID, arrangoff, "");
                            //DataSet dcs = new DataSet(); dcs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                            //    + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            //var noOfRezc = GetNoRec(dcs, cnb, cnc);//dcs.Tables[0].Rows.Count;
                            ////float FirstLyric = 5000;
                            ////float FirstVocal = 0;
                            //var B = ""; var L = ""; var R = ""; var C = "";

                            //for (var j = 0; j <= noOfRezc - 1; j++)
                            //{
                            //    var Bonus = dcs.Tables[0].Rows[j].ItemArray[1].ToString();
                            //    var ArrangementType = dcs.Tables[0].Rows[j].ItemArray[3].ToString();
                            //    var RouteMask = dcs.Tables[0].Rows[j].ItemArray[4].ToString();
                            //    var Arrangement_Name = dcs.Tables[0].Rows[j].ItemArray[7].ToString();
                            //    if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                            //    if (Arrangement_Name == "2" || Arrangement_Name == "Combo") C = "C";
                            //    else if (RouteMask == "Bass") B = "B";
                            //    else if (RouteMask == "Lead") L = "L";
                            //    else if (RouteMask == "Rhythm") R = "R";

                            //    if (Bonus.ToLower() == "true")
                            //    {
                            //        if (RouteMask == "Bass") B += "b";
                            //        if (Arrangement_Name == "2" || Arrangement_Name == "Combo") C += "b";
                            //        else
                            //        {
                            //            if (RouteMask == "Lead") L += "b";
                            //            if (RouteMask == "Rhythm") R += "b";
                            //        }
                            //    }
                            //}
                            //tzt = L + B + R + C;
                            break;
                        case "<Avail. Tracks w Favorite>":
                            DataSet dks = new DataSet(); dks = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Favorite, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRezk = GetNoRec(dks, cnb, cnc);//dks.Tables[0].Rows.Count; 
                            var Bk = ""; var Lk = ""; var Rk = ""; var Ck = "";
                            var C = ""; var B = ""; var L = ""; var R = "";

                            for (var j = 0; j <= noOfRezk - 1; j++)
                            {
                                var Bonus = dks.Tables[0].Rows[j].ItemArray[1].ToString();
                                var ArrangementType = dks.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dks.Tables[0].Rows[j].ItemArray[4].ToString();
                                var Favorite = dks.Tables[0].Rows[j].ItemArray[7].ToString();
                                var Arrangement_Name = dks.Tables[0].Rows[j].ItemArray[8].ToString();
                                if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                                if (Arrangement_Name == "2" || Arrangement_Name == "Combo") C = "C";
                                else if (RouteMask == "Bass") B = "B";
                                else if (RouteMask == "Lead") L = "L";
                                else if (RouteMask == "Rhythm") R = "R";

                                if (Favorite.ToLower() == "yes")
                                {
                                    if (RouteMask == "Bass") Bk += "f";
                                    if (Arrangement_Name == "2" || Arrangement_Name == "Combo") Ck += "f";
                                    else
                                    {
                                        if (RouteMask == "Lead") Lk += "f";
                                        if (RouteMask == "Rhythm") Rk += "f";
                                    }
                                }
                            }
                            tzt = Lk + Bk + Rk + Ck;
                            break;
                        case "<Bass Has DynamicDificulty>":
                            tzt = ((SongRecord[k].Bass_Has_DD == "No" || bassRemoved) && SongRecord[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        case "<Avail. Tracks and Timings>":
                            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecP = GetNoRec(dup, cnb, cnc) > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRec = GetNoRec(dus, cnb, cnc);//dus.Tables[0].Rows.Count;
                            for (var j = 0; j <= noOfRec - 1; j++)
                            {
                                var XMLFilePath = dus.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Bonus = dus.Tables[0].Rows[j].ItemArray[1].ToString();
                                var Commentz = dus.Tables[0].Rows[j].ItemArray[2].ToString();
                                var ArrangementType = dus.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dus.Tables[0].Rows[j].ItemArray[4].ToString();
                                var StartTime = dus.Tables[0].Rows[j].ItemArray[5].ToString();
                                var Part = dus.Tables[0].Rows[j].ItemArray[6].ToString().ToLower() == "yes" ? "p" : "";
                                var Arrangement_Name = dus.Tables[0].Rows[j].ItemArray[7].ToString();
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                /*var b = "";*/
                                var p = "";
                                if (noOfRecP > 1) p = Part;

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C" + p + shortstart : "")
                                    + (RouteMask == "Bass" ? " B" + p + shortstart : "")
                                    + (RouteMask == "Rhythm" ? " R" + p + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + p + shortstart : ((RouteMask == "Lead") ? " L" + p + shortstart : "")));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings&Bonus>":
                            DataSet dxp = new DataSet(); dxp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb, cnc);
                            var noOfRecc = GetNoRec(dxp, cnb, cnc) > 0 ? (string.IsNullOrEmpty(dxp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dxp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name " +
                                "FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecv = GetNoRec(dxs, cnb, cnc);//dxs.Tables[0].Rows.Count;
                            for (var j = 0; j <= noOfRecv - 1; j++)
                            {
                                var XMLFilePath = dxs.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Bonus = dxs.Tables[0].Rows[j].ItemArray[1].ToString();
                                var Commentz = dxs.Tables[0].Rows[j].ItemArray[2].ToString();
                                var ArrangementType = dxs.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dxs.Tables[0].Rows[j].ItemArray[4].ToString();
                                var StartTime = dxs.Tables[0].Rows[j].ItemArray[5].ToString();
                                var Part = dxs.Tables[0].Rows[j].ItemArray[6].ToString().ToLower() == "yes" ? "p" : "";
                                var Arrangement_Name = dxs.Tables[0].Rows[j].ItemArray[7].ToString();
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                var b = ""; var p = "";
                                if (Bonus.ToLower() == "true") b = "b";
                                if (noOfRecc > 1) p = Part;

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C" + b + p + shortstart : "") + (RouteMask == "Bass" ? " B" + b + p + shortstart : "") + (RouteMask == "Rhythm" ? " R" + b + p + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + b + p + shortstart : ((RouteMask == "Lead") ? " L" + p + shortstart : "")));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings&Bonus&Favorite>":
                            //DataSet dbp = new DataSet(); dbp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            //var noOfRecb = GetNoRec(dbp, cnb, cnc);// > 0 ? (string.IsNullOrEmpty(dbp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dbp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dts = new DataSet(); dts = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Favorite, Arrangement_Name " +
                                "FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecm = GetNoRec(dts, cnb, cnc);//dts.Tables[0].Rows.Count;
                            for (var j = 0; j <= noOfRecm - 1; j++)
                            {
                                var XMLFilePath = dts.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Bonus = dts.Tables[0].Rows[j].ItemArray[1].ToString();
                                var Commentz = dts.Tables[0].Rows[j].ItemArray[2].ToString();
                                var ArrangementType = dts.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dts.Tables[0].Rows[j].ItemArray[4].ToString();
                                var StartTime = dts.Tables[0].Rows[j].ItemArray[5].ToString();
                                var Part = dts.Tables[0].Rows[j].ItemArray[6].ToString();//.ToLower() == "yes" ? "p" : "";
                                var Favorite = dts.Tables[0].Rows[j].ItemArray[7].ToString().ToLower();
                                var Arrangement_Name = dts.Tables[0].Rows[j].ItemArray[8].ToString();
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                var b = ""; var p = ""; var f = ""; var a = "";
                                if (Bonus.ToLower() == "true") b = "b";
                                if (Favorite.ToLower() == "yes") f = "f";
                                if (Part.ToInt32() > 1) a = "a";
                                //if (noOfRecb > 1) p = Part;+ p+ p+ p+ p+ p

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C" + b + a + f + shortstart : "") + (RouteMask == "Bass" ? " B" + b + a + f + shortstart : "") +
                                    (RouteMask == "Rhythm" ? " R" + b + a + f + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + b + a + f + shortstart
                                    : ((RouteMask == "Lead") ? " L" + b + a + f + shortstart : "")));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings>":
                            DataSet dsp = new DataSet(); dsp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecPS = GetNoRec(dsp, cnb, cnc) > 0 ? (string.IsNullOrEmpty(dsp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dsp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecS = GetNoRec(dss, cnb, cnc);//dss.Tables[0].Rows.Count;
                            for (var j = 0; j <= noOfRecS - 1; j++)
                            {
                                var XMLFilePath = dss.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Bonus = dss.Tables[0].Rows[j].ItemArray[1].ToString();
                                var Commentz = dss.Tables[0].Rows[j].ItemArray[2].ToString();
                                var ArrangementType = dss.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dss.Tables[0].Rows[j].ItemArray[4].ToString();
                                var StartTime = dss.Tables[0].Rows[j].ItemArray[5].ToString();
                                var Part = dss.Tables[0].Rows[j].ItemArray[6].ToString().ToLower() == "yes" ? "p" : "";
                                var Arrangement_Name = dss.Tables[0].Rows[j].ItemArray[7].ToString();
                                string shorterstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".")) + "s") : StartTime;

                                var p = "";
                                //if (Bonus.ToLower() == "true") b = "b";+ bvar b = "";  + b+ b+ b
                                if (noOfRecPS > 1) p = Part;

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "2" ? " C" + p + shorterstart : "") + (RouteMask == "Bass" ? " B" + p + shorterstart : "")
                                    + (RouteMask == "Rhythm" ? " R" + p + shorterstart : ((RouteMask == "Lead" ? " L" + p + shorterstart : "")));
                            }
                            break;
                        case "<Timestamp>":
                            tzt = DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss");//.Replace(":", "-");//.Replace(" ", "").Replace(".", "");
                            break;
                        case "<TimestampShort>":
                            tzt = DateTime.Now.ToString("yyMMdd HHmms");//.Replace(":", "-");//.Replace(" ", "").Replace(".", "");
                            break;
                        default:
                            if ((origQAs) || (ifn))
                            {
                                switch (curelem)
                                {
                                    case "<DD>":
                                        tzt = SongRecord[k].Has_DD == "Yes" ? "DD" : "noDD";
                                        break;
                                    case "<CDLC>":
                                        tzt = SongRecord[k].DLC;
                                        break;
                                    case "<Quality Assurance Attributes>":
                                        tzt = ((SongRecord[k].Is_Broken == "Yes") ? " Broken" : "")
                                            + (((SongRecord[k].Has_Cover == "No") || (SongRecord[k].Has_Preview == "No") || (SongRecord[k].Has_Vocals == "No") || (SongRecord[k].Has_Sections == "No") || (SongRecord[k].Has_ShowLights == "No") || (SongRecord[k].Available_Old == "No"))
                                            ? "NOs:" : "") + ((SongRecord[k].Has_Cover == "Yes") ? "" : " Cover") + ((SongRecord[k].Has_Preview == "Yes") ? "" : " Preview") + ((SongRecord[k].Has_Sections == "Yes") ? "" : " Sections")
                                            + ((SongRecord[k].Has_Vocals == "Yes") ? "" : " Vocal") + ((SongRecord[k].Has_ShowLights == "Yes") ? "" : " Showlights") + ((SongRecord[k].Available_Old == "Yes") ? "" : " Originally/OLDImportedAvail.") + ((SongRecord[k].Available_Old == "Yes") ? "" : " OriginallyImportedAvail.")
                                            + ((SongRecord[k].FilesMissingIssues != "") ? "_w FilesMissingIssues" : "");
                                        break;
                                    case "<LastConversionDateTime>":
                                        tzt = SongRecord[k].LastConversionDateTime;
                                        break;
                                    default:
                                        var timestamp = UpdateLog(DateTime.Now, "Error no " + curelem + "tag found", false, c("dlcm_TempPath"), "", "", null, null);
                                        break;
                                }
                            }
                            else
                            {
                                var timestamp = UpdateLog(DateTime.Now, "Error no " + curelem + "tag found", false, c("dlcm_TempPath"), "", "", null, null);
                            }
                            break;
                    }
                    if (tzt != "")
                        //if (SongRecord[0].Album.ToLower().IndexOf("live at") < 0 || (curelem != "<Live>"))
                        // fulltxt = fulltxt.IndexOf(sep1) > 0 ? fulltxt.Replace(tzt.Replace(sep1, "").Replace(sep2, ""), "") + tzt : fulltxt + tzt;
                        //else
                        fulltxt += (fulltxt == "" ? "" : "_") + tzt;

                    if (oldtxt == fulltxt && last_ > 0) fulltxt = fulltxt.Substring(0, last_);
                    last_ = fulltxt.Length;
                }
            }
            fulltxt = fulltxt.Replace("-_", "-").Replace("_-", "-").Replace("- _", "-").Replace("_ -", "-").Replace("--", "-").Replace("__", "_").Replace("_ ", "_").Replace(" _", "_").Replace("- ", "-").Replace(" -", "-").Replace(",_", "_").Replace(",-", "-").Replace("--", "-").Replace("__", "_");
            fulltxt = fulltxt.Trim();
            if (fulltxt.Length > 2) if (fulltxt.Substring(fulltxt.Length - 1, 1) == "-") fulltxt = fulltxt.Substring(0, fulltxt.Length - 1);
            if (fulltxt.Length > 2) if (fulltxt.Substring(fulltxt.Length - 1, 1) == "_") fulltxt = fulltxt.Substring(0, fulltxt.Length - 1);
            fulltxt = fulltxt.Trim();
            if ((sep1 + sep2).Length > 0) return (fulltxt.Trim()).Replace(sep1 + sep2, "").Replace(sep1 + " " + sep2, "").Replace(sep1 + "-", sep1).Replace("-" + sep2, sep2).Replace(sep1 + " ", sep1).Replace(" " + sep2, sep2).Trim();
            // return (fulltxt + sep2).Replace(sep1 + sep2, "").Replace(sep1 + "-", sep1).Replace("-" + sep2, sep2).Replace(sep1 + " ", sep1).Replace(" " + sep2, sep2);
            else return fulltxt;/*'-'*/
        }

        //public static void DeleteRecords(string IDs, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb, ProgressBar pB_ReadDLCs, SQLiteConnection cnz)
        public static void DeleteRecords(string IDs, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb, ProgressBar pB_ReadDLCs, SQLite.SQLiteConnection cnc)
        {
            //Delete records
            DialogResult result1 = System.Windows.Forms.MessageBox.Show(norows + " of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("), "", cnb, cnc);
                var rcount = GetNoRec(dhs, cnb, cnc);//dhs.Tables[0].Rows.Count;
                var tsst = "Updating PAck detail to point to Archive"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                string psarcPath = ""; var cmmd = "";

                //bool deletemanip = false;
                //for (var i = 0; i < rcount; i++)
                //    //{
                //    if (dhs.Tables[0].Rows[i].ItemArray[120].ToString() == "Yes")
                //    {
                //        DialogResult result11 = MessageBox.Show("There are Song(s) manipulated with DLC Manager are you sure you want them to be deleted?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (result11 == DialogResult.Yes) deletemanip = true;
                //        break;
                //    }
                //}
                if (rcount < 1) return;
                for (var i = 0; i < rcount; i++)
                {
                    if (dhs.Tables[0].Rows[i].ItemArray[120].ToString() == "Yes")
                    {
                        DialogResult result11 = System.Windows.Forms.MessageBox.Show("This Song " + dhs.Tables[0].Rows[i].ItemArray[4].ToString() + " - " + dhs.Tables[0].Rows[i].ItemArray[1].ToString()
                            + "was (marked as) manipulated" + (dhs.Tables[0].Rows[i].ItemArray[117].ToString() == "Yes" ? "&intheworks" : "")
                            + " with DLC Manager are you sure you want it to be deleted?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result11 == DialogResult.No) continue;
                    }
                    if (dhs.Tables[0].Rows[i].ItemArray[117].ToString() == "Yes")
                    {
                        DialogResult result11 = System.Windows.Forms.MessageBox.Show("This Song " + dhs.Tables[0].Rows[i].ItemArray[4].ToString() + " - " + dhs.Tables[0].Rows[i].ItemArray[1].ToString()
                            + "was marked as in the Works with DLC Manager are you sure you want it to be deleted?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result11 == DialogResult.No) continue;
                    }
                    psarcPath += (File.Exists(TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString()) ? "" : ", " + TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString());
                    cmmd = cmd.Replace(dhs.Tables[0].Rows[i].ItemArray[19].ToString(), "");
                }

                if (cmd != cmmd)
                {
                    DialogResult resultgf = System.Windows.Forms.MessageBox.Show(psarcPath + " have missing original files. Are you sure you want the records removed as atm you could still generate the songs?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultgf == DialogResult.No) cmd = cmmd.Replace(", ,", ",").Replace(",,", ",").Replace(", )", ")").Replace(",)", ")");
                }

                DialogResult resultf = System.Windows.Forms.MessageBox.Show("Do you wanna have Audit Trail (of these "+ norows + ") to be deleted too, as to allow a re-import them again?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultf == DialogResult.Yes)
                {
                    // //Delete Audit trail of import
                    DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (" + cmd.Replace("DELETE ", "SELECT File_Hash ") + ")", cnb, cnc);
                }

                pB_ReadDLCs.Maximum = rcount;
                pB_ReadDLCs.Step = 1;
                pB_ReadDLCs.Value = 1;
                for (var i = 0; i < rcount; i++)
                {
                    pB_ReadDLCs.Increment(1);
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    bool avail = dhs.Tables[0].Rows[i].ItemArray[89].ToString() == "Yes" ? true : false;
                    DeleteDirectory(filePath, false);

                    //Move psarc file to archive
                    //
                    cmd = "SELECT * FROM Main where Original_FileName=\"" + dhs.Tables[0].Rows[i].ItemArray[19].ToString() + "\"";
                    MainDBfields[] SongRecord = new MainDBfields[20000];
                    SongRecord = GetRecord_s(cmd, cnb, cnc);

                    if (SongRecord[0].NoRec.ToInt32() > 1) continue;
                    string psarcPathh = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    var fh = GetHash(psarcPathh);
                    if (avail) psarcPathh = CopyMoveFileSafely(psarcPathh, psarcPathh.Replace("0_old", "0_archive"), false, fh, false);
                }

                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmmd.Replace("DELETE FROM", "DELETE * FROM"), "", cnb, cnc);

                //Delete Arangements
                DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE CDLC_ID IN (" + IDs + ")", cnb, cnc);

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE * FROM Tones WHERE CDLC_ID IN (" + IDs + ")", cnb, cnc);

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE * FROM Tones_GearList WHERE CDLC_ID IN (" + IDs + ")", cnb, cnc);

                //// //Delete Audit trail of import
                //DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash.Replace(", ", "\", \"") + "\")", cnb, cnc); 

                //Delete Audit trail of pack
                DeleteFromDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE CDLC_ID IN (" + IDs + ")", cnb, cnc);

                //Delete songs from Groups
                DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND CDLC_ID IN (\"" + IDs + "\")", cnb, cnc);

                System.Windows.Forms.MessageBox.Show(rcount + "  Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static string AddDD(string Folder_Name, string Is_Original, string xml, Platform platform, bool superOrg, bool InternalLog, string noLevels)
        {
            string DDAdded = "No";
            if (!File.Exists(xml + ".old")) File.Copy(xml, xml + ".old", false);
            else { File.Copy(xml + ".old", xml, true); }
            string json = "";
            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
            else
                json = (xml.Replace(".xml", ".json").Replace("songs\\arr", calc_path(Directory.GetFiles(Folder_Name, "*.json", System.IO.SearchOption.AllDirectories)[0])));

            if (!File.Exists(json + ".old")) File.Copy(json, json + ".old", false);
            else { File.Copy(json + ".old", json, true); if (Is_Original == "Yes") return "Yes"; }
            var startInfo = new ProcessStartInfo();

            var c = string.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
            startInfo.FileName = Path.Combine(AppWD, "..\\..\\ddc", "ddc.exe");

            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                startInfo.WorkingDirectory = Folder_Name + "\\EOF\\";
            else
                startInfo.WorkingDirectory = Folder_Name + (platform.platform.ToString().ToLower() == "XBox360".ToLower() ? "\\Root" : "") + "\\songs\\arr\\";

            startInfo.Arguments = string.Format("\"{0}\" -l {1} -s {2} {3}{4}{5}",
                                                Path.GetFileName(xml),
                                                4, "N", c,
                                                    " -p Y", " -t Y");
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            using (var DDC = new Process())
            {
                DDC.StartInfo = startInfo;
                DDC.Start();
                DDC.WaitForExit(1000 * 60 * 5); //wait 5 minutes
                DDAdded = "Yes";

            }
            return DDAdded;
        }

        public static string RemoveDD(string Folder_Name, string Is_Original, string xml, Platform platform, bool superOrg, bool InternalLog, string UseInternalLog)
        {
            var Bass_Has_DD = "No";

            var jsons = "";
            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                jsons = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
            else
                jsons = (xml.Replace(".xml", ".json").Replace("songs\\arr", calc_path(Directory.GetFiles(Folder_Name, "*.json", System.IO.SearchOption.AllDirectories)[0])));

            // Bass_Has_DD
            var manifestFunctions = new ManifestFunctions(platform.version);
            Song2014 xmlContent = null;
            try
            {
                xmlContent = Song2014.LoadFromFile(xml);
                if (xmlContent.Arrangement.ToLower() == "bass")
                {
                    platform.version = RocksmithToolkitLib.GameVersion.RS2014;
                    if (manifestFunctions.GetMaxDifficulty(xmlContent) <= 0)
                        return "No";
                }

                //Save a copy
                if (!File.Exists(xml + ".old")) File.Copy(xml, xml + ".old", false);
                else File.Copy(xml, xml + ".old", true);
                var json = jsons;
                if (!File.Exists(json + ".old")) File.Copy(json, json + ".old", false);
                else { File.Copy(json + ".old", json, true); }

                var rampPath = Path.Combine(AppWD.Replace("DLCManager\\external_tools", ""), "ddc\\ddc_dd_remover.xml");
                var cfgPath = Path.Combine(AppWD.Replace("DLCManager\\external_tools", ""), "ddc\\ddc_default.cfg");

                var cmbPhraseLen = ConfigRepository.Instance().GetDecimal("ddc_phraselength");
                var consoleOutput = string.Empty;
                try
                {
                    if (UseInternalLog != "Yes") DDCreator.ApplyDD(xml, (int)cmbPhraseLen, false, rampPath, cfgPath, out consoleOutput, true, false);
                }
                catch (Exception ex)
                {
                    var tsst = "Error at remove DD..." + ex; UpdateLog(DateTime.Now, tsst, false, "", "", "", null, null);
                    UseInternalLog = "Yes";
                }

                if (Is_Original == "Yes" || !string.IsNullOrEmpty(consoleOutput) || UseInternalLog == "Yes")
                    try
                    { //http://code.google.com/p/rocksmith-custom-song-creator/issues/detail?id=60

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
                                if (line.Contains("<levels")) break;
                                header += line + "\n";
                            }
                            header += "\n<levels count=\"1\">\n";
                            //level the maxdiff overall setting in the xml
                            var m = 1;
                            for (m = 1; m <= j; m++)
                            {
                                header = header.Replace("maxDifficulty=\"" + m + "\"", "maxDifficulty=\"0\"");
                            }

                            var v = 0; //difficulty level in the parsing
                            var diff = 0;
                            float[] timea = new float[10000]; //keeps the timestamp of each note
                            float[] timeb = new float[10000]; //keeps the timestamp of each anchor
                            string[] notes = new string[10000]; // keeps the full note details
                            string[] bends = new string[10000]; // keeps the bends single note details
                            string[] anchor = new string[10000]; // keeps the full note details
                            int[] lvla = new int[10000]; //keeps the level of the note&timestamp
                            string[] bnds = new string[10000]; //keeps the bends of the note&timestamp
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
                                if (line.Contains("<level difficulty=\""))
                                {
                                    line = line.Replace("<level difficulty=\"", "").Trim();
                                    line = line.Replace("\">", "");
                                    try { diff = line.ToInt32(); }
                                    catch
                                    {
                                        System.Windows.Forms.MessageBox.Show("Errors at DD lvl READ removal");
                                    }
                                    v = diff;
                                    continue;
                                }

                                //notes
                                if (line.Contains("<note time=\""))
                                {
                                    tecst = (line.Replace("<note time=\"", "")).TrimStart();
                                    tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                    try { ts = Convert.ToSingle(tecst); }
                                    catch (Exception ex)
                                    {
                                        var tsst = "Error11 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                        System.Windows.Forms.MessageBox.Show("Errors at DD time notes READ removal");
                                    }

                                    UpdateT = false;
                                    for (m = 0; m < ea; m++)
                                    {
                                        if (ts == timea[m])
                                        {
                                            if (v > lvla[m])
                                            {
                                                notes[m] = line.Replace("\">", "\" />");
                                                timea[m] = ts;
                                                lvla[m] = v;
                                                UpdateT = true;
                                                if (line.IndexOf("bend=\"0\"") == -1 && line.IndexOf("bend=\"") > 0)
                                                {
                                                    line = fxml.ReadLine();
                                                    if (line.IndexOf("bendValue") > 0 && line.IndexOf("bendValues>") > 0)
                                                    {
                                                        line = fxml.ReadLine();
                                                        if (line.IndexOf("bendValue time") > 0)
                                                        {
                                                            bends[m] = line;
                                                            m += 2;
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                    {
                                        notes[ea] = line.Replace("\">", "\" />");
                                        timea[ea] = ts;
                                        lvla[ea] = v;
                                        ea++;
                                        if (line.IndexOf("bend=\"0\"") == -1 && line.IndexOf("bend=\"") > 0)
                                        {
                                            line = fxml.ReadLine();
                                            if (line.IndexOf("bendValue") > 0 && line.IndexOf("bendValues>") > 0)
                                            {
                                                line = fxml.ReadLine();
                                                if (line.IndexOf("bendValue time") > 0)
                                                {
                                                    bends[m] = line;
                                                    m += 2;
                                                }
                                            }
                                        }
                                    }
                                }
                                //anchor
                                if (line.Contains("<anchor time=\""))
                                {
                                    tecst = (line.Replace("<anchor time=\"", "")).TrimStart();
                                    tecst = tecst.Replace(tecst.Substring(tecst.IndexOf("\"")), "");
                                    try { ts = Convert.ToSingle(tecst); }
                                    catch (Exception ex)
                                    {
                                        var tsst = "Error12 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                        System.Windows.Forms.MessageBox.Show("Errors at DD time anchor READ removal");
                                    }
                                    UpdateT = false;
                                    for (m = 0; m < eb; m++)
                                    {
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
                                    }
                                    if (!UpdateT) //if TimeStamp has not been found in the storage array then save it
                                    {
                                        anchor[eb] = line;
                                        timeb[eb] = ts;
                                        lvlb[eb] = v;
                                        eb++;
                                    }
                                }
                                if (line.Contains("<notes>")) continue;

                            }

                            //reorder the storage array
                            var n = 0;
                            string no;
                            string be;
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
                                        be = bends[n];
                                        notes[n] = notes[m];
                                        timea[n] = timea[m];
                                        lvla[n] = lvla[m];
                                        bends[n] = bends[m];
                                        notes[m] = no;
                                        timea[m] = ti;
                                        lvla[m] = lv;
                                        bends[m] = be;
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
                                //footer += notes[m] + "\n";
                                if (bends[m] != "" && bends[m] != null)
                                {
                                    footer += notes[m].Replace("/>", ">") + "\n";
                                    footer += "  <bendValues>\n";
                                    footer += "   " + bends[m] + "\n";
                                    footer += "  </bendValues>\n";
                                    footer += " </note>\n";
                                }
                                else footer += notes[m] + "\n";
                            }
                            footer += "	  </notes>\n      <chords />\n      <anchors>\n";
                            //add level & notes to the footer
                            for (m = 0; m <= eb; m++)
                            {
                                footer += anchor[m] + "\n";
                            }
                            footer += "      </anchors>" + "\n" + "      <handShapes />" + "\n" + "     </level>" + "\n" + "   </levels>" + "\n" + "</song>";
                            fxml.Close();
                            File.WriteAllText(xml, header + footer);

                            //level the json as well
                            textfile = File.ReadAllText(json);
                            n = 0;
                            for (n = 0; n < j; n++)
                            {
                                textfile = textfile.Replace("\"MaxPhraseDifficulty\": " + n + ",", "\"MaxPhraseDifficulty\": 0,");
                            }
                            File.WriteAllText(json, textfile);
                        }
                        Bass_Has_DD = "Yes";
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error at Internal remove DD..." + ex; UpdateLog(DateTime.Now, tsst, false, "", "", "", null, null);
                    }
            }
            catch (Exception ex)
            {
                var tsst = "Error at Load XML remove DD..." + ex; UpdateLog(DateTime.Now, tsst, false, "", "", "", null, null);
            }

            return Bass_Has_DD;

        }

        public static void Downstream(string fn, float bitrate, string windw)
        {
            File.Copy(fn, fn + ".old", true);
            var startInfo = new ProcessStartInfo();
            var tst = ""; var timestamp = DateTime.Now;
            //MessageBox.Show(AppWD+"-"+ fn);
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = fn.Replace(".wem", "_fixed.ogg").Replace("_fixed_fixed.ogg", "_fixed.ogg").TrimStart(' ');// (fn.IndexOf("preview.wem") > 0 ? : "");//fn.Replace(".wem", "_fixed.ogg")
            var tt = t + "l";
            startInfo.Arguments = string.Format(" \"{0}\" -o \"{1}\" -Q",
                                                t,
                                                tt);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;
            //to capture error mss
            //startInfo.RedirectStandardOutput = true; 
            //startInfo.RedirectStandardError = true;
            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start();
                    //string stdoutx = DDC.StandardOutput.ReadToEnd();
                    //string stderrx = DDC.StandardError.ReadToEnd();
                    DDC.WaitForExit(1000 * 60 * 5); //wait 5min 
                    if (DDC.ExitCode == 0)
                    {
                        startInfo = new ProcessStartInfo
                        {
                            FileName = Path.Combine(AppWD, "oggenc.exe"),
                            WorkingDirectory = AppWD,
                            Arguments = string.Format(" \"{0}\" -o \"{1}\" -b \"{2}\" -Q --resample \"{3}\" -c \"author=catara\"",//
                                                            tt,
                                                            t,
                                                            ConfigRepository.Instance()["dlcm_BitRate"].Substring(0, 3),
                                                            ConfigRepository.Instance()["dlcm_SampleRate"]),
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };

                        if (File.Exists(t))
                            using (var DDgC = new Process())
                            {
                                tst = "Downstream from " + bitrate.ToString() + " to" + ConfigRepository.Instance()["dlcm_BitRate"] + "-" + ConfigRepository.Instance()["dlcm_SampleRate"] + "..."; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", windw, null, null);

                                //MessageBox.Show(AppWD + "+" + tt);
                                DDgC.StartInfo = startInfo; DDgC.Start(); DDgC.WaitForExit(1000 * 60 * 5); //wait 5min
                                if (DDgC.ExitCode == 0)
                                {
                                    DeleteFile(tt, false);

                                    DeleteFile(fn, false);
                                    var i = 1;
                                    do //sometimes it fails
                                    {
                                        tst = "Convert to wem ... " + i + " - " + fn;
                                        timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "", null, null);

                                        //MessageBox.Show(AppWD + "/" + t);
                                        UtilitiesFunctions.Converters(t, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);

                                        System.IO.FileInfo fi = null; //calc file size
                                        try { fi = new System.IO.FileInfo(t.Replace(".ogg", ".wem")); }
                                        catch (Exception ex) { var tsst = "Error13 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                                        if (!File.Exists(t.Replace(".ogg", ".wem")) || fi.Length == 0)
                                        {
                                            File.Copy(fn + ".old", t.Replace(".ogg", ".wem"), true);
                                            if (i > 3)
                                            {
                                                //fix as sometime the template folder gets poluted and breaks eveything
                                                var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                                var templateDir = Path.Combine(appRootDir, "Template");
                                                var backup_dir = AppWD + "\\Template";
                                                //DeleteDirectory(templateDir, false); //commenting as giving windows folder blocked folder
                                                CopyFolder(backup_dir, templateDir);
                                            }
                                        }
                                        else break;
                                        i++;
                                    }
                                    while (i < 10);

                                    if (File.Exists(t.Replace(".ogg", ".wem")) && t.Replace(".ogg", ".wem") != fn)
                                    {
                                        File.Copy(t.Replace(".ogg", ".wem"), fn, true);
                                        DeleteFile(t.Replace(".ogg", ".wem"), false);
                                    }
                                    else if (t.Replace(".ogg", ".wem") != fn) File.Copy(fn + ".old", fn, true);
                                    if (File.Exists(t.Replace(".ogg", ".wav"))) DeleteFile(t.Replace(".ogg", ".wav"), false);
                                    if (File.Exists(t.Replace("_fixed.ogg", "_preview_fixed.wav"))) DeleteFile(t.Replace("_fixed.ogg", "_preview_fixed.wav"), false);
                                    if (File.Exists(t.Replace(".ogg", "_preview.wem"))) DeleteFile(t.Replace(".ogg", "_preview.wem"), false);
                                }
                                else
                                {
                                    var tsst = "Error15 ..." + "Error downsizingpreview" + DDgC.ExitCode + DDgC.StartInfo.FileName + DDgC.StartInfo.ErrorDialog + DDgC.StartInfo.Arguments; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                                    File.Copy(fn + ".old", fn, true);
                                }
                            }
                    }
                    else
                    {
                        var tsst = "Error16 ..." + "Error downsizingpreview" + DDC.ExitCode + DDC.StartInfo.FileName + DDC.StartInfo.ErrorDialog + DDC.StartInfo.Arguments; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                        File.Copy(fn + ".old", fn, true);
                    }
                }
            DeleteFile(fn + ".old", false);
        }

        public static CheckedListBox GenerateParamsList(CheckedListBox chbx_Additional_Manipulations, string slct)
        {
            //Get group and norder index
            var SearchCmd = "SELECT Type, Profile_Name, DisplayGroup FROM Groups u WHERE Type=\"Groups\" ORDER BY DisplayGroup ASC";
            DataSet dv = new DataSet(); dv = SelectFromDB("Groups", SearchCmd, c("dlcm_DBFolder"), cnb, cnc);
            var n = GetNoRec(dv, cnb, cnc);

            //SELECT all Params for current Profile
            SearchCmd = slct;
            //"SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u WHERE Type=\"Profile\"" +
            //" AND Profile_Name=\"" + c("dlcm_Configurations") + "\" and Comments like \"%dlcm_AdditionalManipul%\"";
            DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Groups", SearchCmd, c("dlcm_DBFolder"), cnb, cnc);
            var noOfRec = GetNoRec(dsz1, cnb, cnc);

            //clear PArams
            chbx_Additional_Manipulations.DataSource = null;
            for (int i = chbx_Additional_Manipulations.Items.Count - 1; i >= 0; --i)
                chbx_Additional_Manipulations.Items.RemoveAt(i);

            //AddOrderNo Group Index + order no in group
            var DisplayGroup = "";
            for (int j = 0; j < noOfRec; j++)
            {
                DisplayGroup = dsz1.Tables[0].Rows[j][3].ToString();
                var Comments = dsz1.Tables[0].Rows[j][4].ToString();
                if (Comments.Length == 1) Comments = "0" + Comments;
                dsz1.Tables[0].Rows[j][5] = GiveOrder(dv, n, DisplayGroup) + Comments;
            }

            //OrderList of Params based on Group order and then Item in the group order
            var tmp = "";
            for (int l = 0; l < noOfRec; l++)
                for (int m = l + 1; m < noOfRec; m++)
                {
                    //if (dsz1.Tables[0].Rows[l][1].ToString() == "dlcm_AdditionalManipul89" || dsz1.Tables[0].Rows[m][1].ToString() == "dlcm_AdditionalManipul89")
                    //    ;
                    if (dsz1.Tables[0].Rows[m][5].ToString().ToInt32() < dsz1.Tables[0].Rows[l][5].ToString().ToInt32())
                    {
                        tmp = dsz1.Tables[0].Rows[l][0].ToString(); dsz1.Tables[0].Rows[l][0] = dsz1.Tables[0].Rows[m][0].ToString(); dsz1.Tables[0].Rows[m][0] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][1].ToString(); dsz1.Tables[0].Rows[l][1] = dsz1.Tables[0].Rows[m][1].ToString(); dsz1.Tables[0].Rows[m][1] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][2].ToString(); dsz1.Tables[0].Rows[l][2] = dsz1.Tables[0].Rows[m][2].ToString(); dsz1.Tables[0].Rows[m][2] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][3].ToString(); dsz1.Tables[0].Rows[l][3] = dsz1.Tables[0].Rows[m][3].ToString(); dsz1.Tables[0].Rows[m][3] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][4].ToString(); dsz1.Tables[0].Rows[l][4] = dsz1.Tables[0].Rows[m][4].ToString(); dsz1.Tables[0].Rows[m][4] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][5].ToString(); dsz1.Tables[0].Rows[l][5] = dsz1.Tables[0].Rows[m][5].ToString(); dsz1.Tables[0].Rows[m][5] = tmp;
                        tmp = dsz1.Tables[0].Rows[l][6].ToString(); dsz1.Tables[0].Rows[l][6] = dsz1.Tables[0].Rows[m][6].ToString(); dsz1.Tables[0].Rows[m][6] = tmp;
                    }
                }

            //add items
            DisplayGroup = "";
            var z = 0;
            for (int k = 0; k < noOfRec; k++)
            {
                var Type = dsz1.Tables[0].Rows[k][0].ToString();
                var Comments = dsz1.Tables[0].Rows[k][1].ToString();

                var DisplayName = dsz1.Tables[0].Rows[k][2].ToString();
                if (DisplayName == "") continue;
                if (DisplayGroup != dsz1.Tables[0].Rows[k][3].ToString())
                {
                    chbx_Additional_Manipulations.Items.Add(dsz1.Tables[0].Rows[k][3].ToString());
                    chbx_Additional_Manipulations.SetItemCheckState(z, CheckState.Indeterminate);
                    z++;
                }
                DisplayGroup = dsz1.Tables[0].Rows[k][3].ToString();
                var DisplayPosition = dsz1.Tables[0].Rows[k][4].ToString();
                var Groups = dsz1.Tables[0].Rows[k][6].ToString();
                chbx_Additional_Manipulations.Items.Add(("Yes" == ConfigRepository.Instance()["dlcm_Debug"] ? DisplayPosition + ". " : "")
                    + DisplayName + " {" + Comments.Replace("dlcm_AdditionalManipul", "") + "}");
                chbx_Additional_Manipulations.SetItemCheckState(z, Groups.ToLower() == "no" ? CheckState.Unchecked : CheckState.Checked);
                z++;
            }

            return chbx_Additional_Manipulations;
        }

        public static bool checkaudiofilesifreadable(string audio)
        {
            var paath = c("dlcm_MediaInfo_CLI");
            var xx = "";
            if (File.Exists(paath)) xx = paath;
            else xx = Path.Combine(AppWD, "MediaInfo_Windows_x64", "MediaInfo.exe");
            if (!File.Exists(xx) || !File.Exists(audio))
            {
                //ErrorWindow frm1 = new ErrorWindow(
                //    (!File.Exists(xx) ? "Install MediaInfo CLI if you want to use it." : "") +
                //    (!File.Exists(audio) ? "File still missing" : ""), c("dlcm_MediaInfo_CLI_www"), "Missing MediaInfo CLI"
                //, false, false, true, "", "", "", false); frm1.ShowDialog();
                //timestamp = UpdateLog(timestamp, (!File.Exists(xx) ? "Install MediaInfo CLI if you want to use it." : "") + (!File.Exists(audio) ? "File still missing" : "")
                //    , true, c("dlcm_TempPath"), "", "MainDB", null, null);
                return !File.Exists(xx) ? false : (!File.Exists(audio) ? true : false);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = xx,
                WorkingDirectory = AppWD
            };
            var t = audio;
            startInfo.Arguments = string.Format(" --Inform=Audio;%BitRate% \"{0}\"", t);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            var bad = false;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start();
                    string stdoutx = DDC.StandardOutput.ReadToEnd();
                    string stderrx = DDC.StandardError.ReadToEnd();
                    DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (stdoutx != "\r\n")
                    {
                        if (float.Parse(stdoutx.Replace("\r\n", ""), NumberStyles.Float, CultureInfo.CurrentCulture) <= 0)
                            bad = true;
                        //Fix Bitrate
                        //var cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "";
                        //FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "MainDB", cnc);

                        //j++; 
                        // timestamp = UpdateLog(timestamp, audio + stdoutx.Replace("\r\n", ""), true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    else
                        bad = true;
                    //timestamp = UpdateLog(timestamp, audio + stdoutx, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                }
            return bad;
        }

        public static void GeneratePackage(object sender, DoWorkEventArgs e)
        {
            Random randomp = new Random();
            var packid = "";
            string[] args = (e.Argument).ToString().Split(';');
            var cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            //var cnz = new SQLiteConnection("Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            var cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
            // cnc.Open(); SQLiteConnection cnc;
            string ID = args[0];
            bool error = false;
            var startT = DateTime.Now;
            string TempPath = "";
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = c("dlcm_TempPath");
            string multithreadname = "";
            var error_reason = "";
            var tsst = "\nStart TH ..."; DateTime timestamp = startT;
            var form = "";
            var cmd = "SELECT * FROM Main ";
            cmd += "WHERE ID = " + ID + "";
            DLCPackageData data;
            if (ConfigRepository.Instance()["dlcm_GlobalTempVariable"] != "") return;
            else ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "g";

            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(cmd, cnb, cnc);
            string Folder_Name = SongRecord[0].Folder_Name;
            try
            {
                //not needed as all dont in utilities
                //if (cnb.State.ToString() == "Open") 
                //    cnb.Open();

                //var vars = e.Argument as string[];
                bool bassRemoved = args[1].ToLower() == "true" ? true : false;
                string chbx_PC = args[2];
                string chbx_PS3 = args[3];
                string chbx_XBOX = args[4];
                string chbx_Mac = args[5];
                string netstatus = args[6];
                bool chbx_Beta = c("dlcm_AdditionalManipul102") == "Yes" ? false : (args[7].ToLower() == "true" ? true : false);
                string chbx_Group = args[8];
                string Groupss = args[9];
                TempPath = args[10];
                bool chbx_UniqueID = args[11].ToLower() == "true" ? true : false;
                bool chbx_Last_Packed = args[12].ToLower() == "true" ? true : false;
                bool chbx_Last_PackedEnabled = args[13].ToLower() == "true" ? true : false;
                bool chbx_CopyOld = args[14].ToLower() == "true" ? true : false;
                bool chbx_CopyOldEnabled = args[15].ToLower() == "true" ? true : false;
                bool chbx_Copy = (args[16].ToLower() == "true" || args[12].ToLower() == "true" || args[14].ToLower() == "true") ? true : false;
                bool chbx_Replace = args[17].ToLower() == "true" ? true : false;
                bool chbx_ReplaceEnabled = args[18].ToLower() == "true" ? true : false;
                packid = args[19];
                string windw = args[20];
                string Original_FileName = SongRecord[0].Original_FileName;

                string txt_RemotePath = SongRecord[0].Remote_Path;
                string txt_FTPPath = args[25];
                bool chbx_RemoveBassDD = c("dlcm_AdditionalManipul102") == "Yes" ? false : (args[26].ToLower() == "true" ? true : false);
                bool chbx_BassDD = SongRecord[0].Bass_Has_DD.ToLower() == "yes" ? true : false;
                bool chbx_KeepBassDD = c("dlcm_AdditionalManipul102") == "Yes" ? true : (SongRecord[0].Keep_BassDD.ToLower() == "yes" ? true : false);
                bool chbx_KeepDD = SongRecord[0].Keep_DD.ToLower() == "yes" ? true : false;
                string chbx_Original = SongRecord[0].Is_Original;
                string txt_DLC_ID = args[31];
                string SearchCmd = args[32];
                string RocksmithDLCPath = args[33];
                string DLC_Name = SongRecord[0].DLC_Name;
                bool updateTonesArrangs = ConfigRepository.Instance()["dlcm_AdditionalManipul76"].ToLower() == "yes" ? true : false;
                multithreadname = c("dlcm_MuliThreading") == "No" ? "" : args[36];
                form = args[37];
                var ord_no = args[38];
                var spotystatus = args[39];
                var ybstatus = args[40];
                var ftpstatus = args[41];
                var arrangoff = args[42].ToString() == "Yes" ? true : false;
                string chbx_UseInternalDD = ConfigRepository.Instance()["dlcm_AdditionalManipul31"].ToLower() == "yes" ? "Yes" : SongRecord[0].UseInternalDDRemovalLogic;//(.ToLower() == "yes" ? true : false;
                string chbx_Format = (chbx_PC != "" ? "PC" : (chbx_PS3 != "" ? "PS3" : (chbx_XBOX != "" ? "XBOX360" : (chbx_Mac != "" ? "Mac" : ""))));
                UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, form, null, null);

                //if (c("dlcm_MuliThreading") == "No")/*&& form != "DLCManager"*/
                //    ConfigRepository.Instance()["dlcm_MuliThreading"] = txt_DLC_ID;
                //else if (c("dlcm_MuliThreading") == txt_DLC_ID) return;

                //get packld
                //DataSet dms = new DataSet(); dms = SelectFromDB("Main", "SELECT max(val(Pack)) as ID FROM Main", c("dlcm_DBFolder"), cnb, cnc);
                //if (GetNoRec(dms, cnb, cnc) > 0) packid = (int.Parse(dms.Tables[0].Rows[0].ItemArray[0].ToString()) + 1).ToString();

                string dlcSavePath = "";
                string h = "";
                string oldfilePath = ""; var rec = 0; var needRebuildPackage = false;
                if (chbx_CopyOld && chbx_CopyOldEnabled)
                {
                    oldfilePath = TempPath + "\\0_old\\" + Original_FileName;
                    if (oldfilePath.GetPlatform().platform.ToString() == (chbx_Format == "PC" ? "Pc" : chbx_Format == "PS3" ? "Ps3" : chbx_Format))
                    {
                        h = oldfilePath;
                    }
                    else
                    {
                        //RocksmithToolkitLib.Platform SourcePlatform = inputFilePath.GetPlatform();
                        RocksmithToolkitLib.Platform SourcePlatform = new RocksmithToolkitLib.Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
                        TargetPlatform = new Platform(chbx_Format, GameVersion.RS2014.ToString());

                        needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
                        var tmpDir = Path.GetTempPath();

                        var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, SourcePlatform, false, false);

                        // DESTINATION
                        var nameTemplate = (!TargetPlatform.IsConsole) ? "{0}{1}.psarc" : "{0}{1}";
                        randomp = new Random();
                        var packageName = Path.GetFileNameWithoutExtension(oldfilePath).StripPlatformEndName();
                        if (chbx_UniqueID) packageName += packid;
                        packageName = packageName.Replace(".", "_");
                        var targetFileName = string.Format(nameTemplate, Path.Combine(Path.GetDirectoryName(oldfilePath), packageName), TargetPlatform.GetPathName()[2]);

                        data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
                        SongRecord[0].Album = data.SongInfo.Album;
                        SongRecord[0].Artist = data.SongInfo.Artist.ToString();
                        SongRecord[0].Artist_Sort = data.SongInfo.ArtistSort;
                        SongRecord[0].Song_Title = data.SongInfo.SongDisplayName;
                        SongRecord[0].Song_Title_Sort = data.SongInfo.SongDisplayNameSort;
                        SongRecord[0].Album_Year = data.SongInfo.SongYear.ToString();
                        SongRecord[0].Is_Original = data.ToolkitInfo.ToolkitVersion == "" ? "Yes" : "No";
                        SongRecord[0].Track_No = "00";
                        SongRecord[0].Groups = Groupss;
                        if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes" && c("dlcm_AdditionalManipul102") != "Yes")/*repacked_Path + "\\" + */
                            targetFileName = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul91"] == "Yes") targetFileName = Groupss + targetFileName;/* && c("dlcm_AdditionalManipul102") != "Yes" */

                        h = TempPath + "\\0_repacked\\" + (chbx_Format == "PC" ? "PC" : chbx_Format == "Mac" ? "MAC" : chbx_Format == "PS3" ? "PS3" : "XBOX360") + "\\"; //+ Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString()));
                        h += Path.GetFileNameWithoutExtension(targetFileName);
                        targetFileName = h;
                        // CONVERSION
                        if (needRebuildPackage)
                        {
                            // Update AppID
                            if (!TargetPlatform.IsConsole)
                                data.AppId = "248750";

                            // Build
                            RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(targetFileName, data, new Platform(TargetPlatform.platform, GameVersion.RS2014));
                        }
                        else
                        {
                            // Old and new paths
                            var sourceDir0 = SourcePlatform.GetPathName()[0].ToLower();
                            var sourceDir1 = SourcePlatform.GetPathName()[1].ToLower();
                            var targetDir0 = TargetPlatform.GetPathName()[0].ToLower();
                            var targetDir1 = TargetPlatform.GetPathName()[1].ToLower();

                            if (!TargetPlatform.IsConsole)
                            {
                                // Replace AppId
                                var appIdFile = Path.Combine(unpackedDir, "appid.appid");
                                File.WriteAllText(appIdFile, "248750");
                            }

                            // Replace aggregate graph values
                            var aggregateFile = Directory.EnumerateFiles(unpackedDir, "*.nt", System.IO.SearchOption.AllDirectories).FirstOrDefault();
                            var aggregateGraphText = File.ReadAllText(aggregateFile);
                            // Tags
                            aggregateGraphText = Regex.Replace(aggregateGraphText, GraphItem.GetPlatformTagDescription(SourcePlatform.platform), GraphItem.GetPlatformTagDescription(TargetPlatform.platform), RegexOptions.Multiline);
                            // Paths
                            aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir0, targetDir0, RegexOptions.Multiline);
                            aggregateGraphText = Regex.Replace(aggregateGraphText, sourceDir1, targetDir1, RegexOptions.Multiline);
                            File.WriteAllText(aggregateFile, aggregateGraphText);

                            // Rename directories
                            foreach (var dir in Directory.GetDirectories(unpackedDir, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                if (dir.EndsWith(sourceDir0))
                                {
                                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir0)) + targetDir0;
                                    DeleteDirectory(newDir, false);
                                    Directory.Move(dir, newDir);
                                }
                                else if (dir.EndsWith(sourceDir1))
                                {
                                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
                                    DeleteDirectory(newDir, false);
                                    Directory.Move(dir, newDir);
                                }
                            }

                            // Recreates SNG because SNG have different keys in PC and Mac
                            bool updateSNG = ((SourcePlatform.platform == GamePlatform.Pc && TargetPlatform.platform == GamePlatform.Mac) || (SourcePlatform.platform == GamePlatform.Mac && TargetPlatform.platform == GamePlatform.Pc));

                            // Packing
                            var dirToPack = unpackedDir;
                            if (SourcePlatform.platform == GamePlatform.XBox360)
                                dirToPack = Directory.GetDirectories(Path.Combine(unpackedDir, Packer.ROOT_XBOX360))[0];

                            Packer.Pack(dirToPack, targetFileName, SourcePlatform, updateSNG, true); //30.09 added false updateManifest
                            DeleteDirectory(unpackedDir, false);
                        }
                        h = chbx_Format == "PS3" ? h.Replace(".", "_").Replace(" ", "_").Replace("/", "") : h;
                        h += (chbx_Format == "PC" ? "_p.psarc" : (chbx_Format == "Mac" ? "_m.psarc" : (chbx_Format == "PS3" ? "_ps3.psarc.edat" : "")));
                    }
                }
                if (((chbx_Last_Packed && chbx_Last_PackedEnabled) && !(chbx_CopyOld && chbx_CopyOldEnabled)) && (!File.Exists(h) || h == ""))
                {
                    DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+\"\\\"+FileName FROM Pack_AuditTrail WHERE Platform=\"" + chbx_Format + "\" and CDLC_ID=" + ID + " ORDER BY ID DESC;", "", cnb, cnc);
                    rec = GetNoRec(dvr, cnb, cnc); //dvr.Tables[0].Rows.Count;
                    if (rec > 0) h = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                if ((!(chbx_Last_Packed && chbx_Last_PackedEnabled) || (chbx_Last_Packed && chbx_Last_PackedEnabled && rec == 0)) && !(chbx_CopyOld && chbx_CopyOldEnabled) && (!File.Exists(h) || h == ""))
                {
                    var i = 0;
                    tsst = "Repacking " + SongRecord[0].NoRec + " song(s)"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                    foreach (var filez in SongRecord)
                    {
                        if (i > 0) //ONLY 1  FILE WILL BE READ
                            break;
                        if (i > 0) //sometimes the break doesnt work?
                            continue;
                        i++;
                        var packagePlatform = filez.Folder_Name.GetPlatform();
                        // REORGANIZE
                        var structured = ConfigRepository.Instance().GetBoolean("creator_structured");

                        //RemoveDD DD 
                        tsst = "removing DD"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        bassRemoved = false;
                        Platform platformz = Folder_Name.GetPlatform(); string[] xmlFilez = new string[c("dlcm_maxsongsinDLCM").ToInt32()]; bool done = true; var countd = 0;
                        do
                        {
                            try
                            {
                                countd++;
                                xmlFilez = Directory.GetFiles(Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                                done = true;
                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(10000);
                                done = false;
                                tsst = "Issues at reading folder!! Retry: " + countd + "/10"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            }
                        }
                        while (!done && countd <= 10);
                        if (xmlFilez[0] != null)
                            foreach (var xml in xmlFilez)
                            {
                                if (xml.ToLower().IndexOf("showlights") < 0 && xml.ToLower().IndexOf("vocals") < 0 && c("dlcm_AdditionalManipul102") != "Yes" && xml != null)
                                    try
                                    {
                                        Song2014 xmlContent = null;
                                        xmlContent = Song2014.LoadFromFile(xml);
                                        if (xmlContent is not null)
                                        {
                                            if (xmlContent.Arrangement.ToLower() == "bass" && !(xml.IndexOf(".old") > 0))

                                                if ((ConfigRepository.Instance()["dlcm_AdditionalManipul3"] == "Yes" || chbx_RemoveBassDD) && chbx_BassDD && (!(chbx_KeepBassDD && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") && !(chbx_KeepDD && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes")))
                                                {
                                                    bassRemoved = (RemoveDD(Folder_Name, chbx_Original, xml, platformz, false, false, chbx_UseInternalDD) == "Yes") ? true : false;
                                                    timestamp = UpdateLog(timestamp, "Removing Bass.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);
                                                }

                                            if (xmlContent.Arrangement.ToLower() != "bass" && xml.IndexOf(".old") <= 0)
                                            {
                                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul3"] == "Yes" && !(chbx_KeepDD && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes"))
                                                {
                                                    bassRemoved = (RemoveDD(Folder_Name, chbx_Original, xml, platformz, false, false, chbx_UseInternalDD) == "Yes") ? true : false;
                                                    timestamp = UpdateLog(timestamp, "Removing non bass DD.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var tust = "Error remove dd..." + ex; UpdateLog(DateTime.Now, tust, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                    }
                            }

                        //Add Author if empty or other conditions
                        tsst = "Adding Author info"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        if ((filez.Author == "Custom Song Creator" || filez.Author == "") && ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" && filez.Is_Original != "Yes")
                        {
                            //saving in the txt file is not rally usefull as the system gen the file at every pack :)
                            filez.Author = ConfigRepository.Instance()["general_defaultauthor"].ToLower().IndexOf("repackedby") >= 0 || ConfigRepository.Instance()["general_defaultauthor"].ToLower().IndexOf("repacked by") >= 0
                                ? ConfigRepository.Instance()["general_defaultauthor"].ToUpper() : "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();

                            if (File.Exists(filez.Folder_Name + "\\toolkit.version"))
                            {
                                var fxml = File.OpenText(filez.Folder_Name + "\\toolkit.version");
                                string line;
                                string header = "";
                                //Read and Save Header
                                while ((line = fxml.ReadLine()) != null)
                                {
                                    if (line.Contains("Package Author:")) header += System.Environment.NewLine + "Package Author: " + filez.Author;
                                    else header += line + System.Environment.NewLine;
                                }
                                fxml.Close();
                                File.WriteAllText(filez.Folder_Name + "\\toolkit.version", header);
                            }
                        }

                        //modify lyrics
                        tsst = "Modifying lyrics"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        try
                        {
                            cleanlyrics(filez.ID, cnb, false, cnc);
                            string ttt2 = (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && filez.Has_Vocals == "Yes" && c("dlcm_AdditionalManipul102") != "Yes")
                                ? AddStuffToLyrics(filez.ID, filez.Description, Groupss, filez.Has_DD, (filez.Bass_Has_DD == "Yes") ? "No" : "Yes", filez.Bass_Has_DD, filez.Author, filez.Is_Acoustic, filez.Is_Live, filez.Live_Details
                                , filez.Is_Multitrack, filez.Is_Original, cnb, cnc, SongRecord, chbx_Beta, false) : "";
                            string ttt1 = (ConfigRepository.Instance()["dlcm_AdditionalManipul74"]).ToLower() == "Yes".ToLower() && filez.Has_Vocals.ToLower() == "Yes".ToLower() && c("dlcm_AdditionalManipul102") != "Yes"
                                ? AddTrackStart2Lyrics(filez.ID, cnb, false, cnc) : "";
                            if (ttt2 != "" || ttt1 != "") cleanlyrics(filez.ID, cnb, false, cnc);
                        }
                        catch (Exception ex)
                        {
                            var tust = "Issues at mody lyrics..." + ex; UpdateLog(DateTime.Now, tust, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        }

                        //open lyrics after manipulation
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul94"] == "Yes" && filez.Has_Vocals == "Yes" && c("dlcm_AdditionalManipul102") != "Yes")
                        {
                            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType FROM Arrangements WHERE CDLC_ID=" + filez.ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);

                            var noOfRec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
                            var ST = "";
                            for (i = 0; i <= noOfRec - 1; i++)
                            {
                                var XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
                                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                                if (ArrangementType == "Vocal") ST = XMLFilePath;
                            }

                            string filePath = ST;
                            if (ST != null && ST != "") StartProcesss(filePath, null);
                            //try
                            //    {
                            //        Process process = Process.Start(filePath);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        var trst = "Error lyrics..." + ex; UpdateLog(DateTime.Now, trst, false, c("dlcm_TempPath"), "", "", null, null);
                            //    }
                            DialogResult result1 = DialogResult.Yes;
                            result1 = System.Windows.Forms.MessageBox.Show("(Yes) Are you done with reading the Lyrics file?\n(No) stops current individual packing action.\n(Cancel) stops overall packing action.", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                            //if (result1 == DialogResult.Yes) ;
                            //else
                            if (result1 == DialogResult.No) return;
                            else if (result1 == DialogResult.Cancel)
                            {
                                ConfigRepository.Instance()["dlcm_Global2TempVariable"] = "Yes";
                                return;
                            }
                        }

                        // LOAD DATA
                        timestamp = UpdateLog(timestamp, "Loading song.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);

                        //verify if too many audios
                        var xmlFil = Directory.GetFiles(filez.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFil) if (xml != filez.AudioPath && xml != filez.audioPreviewPath && !xml.Contains("multitracks")) DeleteFile(xml, false);
                        var xmlFi = Directory.GetFiles(filez.Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFi) if (xml != filez.OggPath && xml != filez.oggPreviewPath && !xml.Contains("multitracks")) DeleteFile(xml, false);

                        var info = DLCPackageData.LoadFromFolder(filez.Folder_Name, packagePlatform);

                        var xmlFiles = Directory.GetFiles(filez.Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                        var platform = filez.Folder_Name.GetPlatform();
                        float volume = float.Parse(filez.Volume.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                        float volumep = float.Parse(filez.Preview_Volume, NumberStyles.Float, CultureInfo.CurrentCulture);

                        //set art incl check if existing c("dlcm_defaultalbumart")
                        info.ArtFiles[1].sourceFile = GetAlbumArtPath(filez.AlbumArtPath.Replace("256", info.ArtFiles[0].sizeX.ToString())
                            , filez.Album_ArtPathOrig.Replace("256", info.ArtFiles[0].sizeX.ToString()), ".dds");
                        info.ArtFiles[1].sourceFile = GetAlbumArtPath(filez.AlbumArtPath.Replace("256", info.ArtFiles[1].sizeX.ToString())
                            , filez.Album_ArtPathOrig.Replace("256", info.ArtFiles[0].sizeX.ToString()), ".dds");
                        info.ArtFiles[2].sourceFile = GetAlbumArtPath(filez.AlbumArtPath.Replace("256", info.ArtFiles[2].sizeX.ToString())
                            , filez.Album_ArtPathOrig.Replace("256", info.ArtFiles[0].sizeX.ToString()), ".dds");

                        timestamp = (!File.Exists(filez.audioPreviewPath) || !File.Exists(filez.AudioPath)) ?
                            UpdateLog(timestamp, "erro no wem on song.." + filez.AudioPath + " " + File.Exists(filez.audioPreviewPath) + "-"
                            + filez.audioPreviewPath + " " + File.Exists(filez.AudioPath), true, tmpPath, multithreadname, form, null, null) : timestamp;

                        if (!File.Exists(filez.AudioPath)) filez.Song_Title = filez.Song_Title + " [wDefaultedAudfile]";
                        filez.Song_Title = filez.Song_Title + ((filez.audioPreviewPath != "") ? (File.Exists(filez.audioPreviewPath) ? "" : "[wMainAudioAsPreview]") : (File.Exists(filez.AudioPath) ? "" : "[wMainAudioAsPreview]"));

                        data = new DLCPackageData
                        {
                            GameVersion = GameVersion.RS2014,
                            Pc = filez.Platform == "Pc" ? true : false,
                            Mac = filez.Platform == "Mac" ? true : false,
                            XBox360 = filez.Platform == "Xbox360" ? true : false,
                            PS3 = filez.Platform == "Ps3" ? true : false,
                            Name = filez.DLC_Name,
                            AppId = filez.DLC_AppID,
                            ArtFiles = info.ArtFiles,
                            //Showlights = true,//info.Showlights, //apparently this info is not read..also the tone base is removed/not read also
                            Inlay = info.Inlay,
                            //LyricArtPath = info.LyricArtPath,

                            //USEFUL CMDs String.IsNullOrEmpty(
                            SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
                            {
                                SongDisplayName = filez.Song_Title,
                                SongDisplayNameSort = filez.Song_Title_Sort,
                                Album = filez.Album,
                                AlbumSort = filez.Album_Sort,
                                SongYear = filez.Album_Year.ToInt32(),
                                Artist = filez.Artist,
                                ArtistSort = filez.Artist_Sort,
                                AverageTempo = filez.AverageTempo.ToInt32()
                            },

                            //AlbumArtPath = filez.AlbumArtPath,
                            OggPath = File.Exists(filez.AudioPath) ? filez.AudioPath : c("dlcm_defaultaudio"),
                            OggPreviewPath = ((filez.audioPreviewPath != "") ? (File.Exists(filez.audioPreviewPath) ? filez.audioPreviewPath : c("dlcm_defaultpreview")) : (File.Exists(filez.AudioPath) ? filez.AudioPath : c("dlcm_defaultaudio"))),
                            //OggPath = filez.OggPath,
                            //OggPreviewPath = ((filez.oggPreviewPath != "") ? filez.oggPreviewPath : filez.OggPath),
                            Arrangements = info.Arrangements,
                            Tones = info.Tones,
                            TonesRS2014 = info.TonesRS2014,
                            Volume = volume,
                            PreviewVolume = volumep,
                            SignatureType = info.SignatureType
                        };
                        if ((!File.Exists(filez.audioPreviewPath) || !File.Exists(filez.AudioPath)))
                            data.SongInfo.Artist = FixAudiofileInconsist(filez.ID.ToInt32(), null, null, null, timestamp, AppWD, "SELECT * FROM Main WHERE ID=" + filez.ID + "")
                                ? (data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wDownAud]") : data.SongInfo.Artist + "[wAudfileIncfixed]") : "";

                        timestamp = (!File.Exists(filez.audioPreviewPath) || !File.Exists(filez.AudioPath)) ?
                            UpdateLog(timestamp, "Error no wem on song.." + filez.AudioPath + " " + File.Exists(filez.audioPreviewPath) + "-"
                            + filez.audioPreviewPath + " " + File.Exists(filez.AudioPath), true, tmpPath, multithreadname, form, null, null) : timestamp;                        //IF Vocals have been added  but Repack is set not to consider them
                        if (updateTonesArrangs)
                        {
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Tones_GearList", "SELECT * FROM Tones_GearList WHERE Tone_ID in (SELECT ID FROM Tones WHERE CDLC_ID=" + ID + GetArrOfficSQLTxt(arrangoff) + ");", "", cnb, cnc);
                            var norecx = GetNoRec(dbs, cnb, cnc); //dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;

                            if (norecx == 0 && info.TonesRS2014.Count != 0) updateTonesArrangs = false;// MessageBox.Show("Vocals not included as added in the DLCManager tool, but Option 76 is Unselected ergo no DLCManager-DB changes are considered at packing");
                            else updateTonesArrangs = true;
                        }//else if (ConfigRepository.Instance()["dlcm_AdditionalManipul76"].ToLower() == "yes") 

                        //IF Vocals have been added  but Repack is set not to consider them
                        if (!updateTonesArrangs)
                        {
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + " AND ArrangementType=\"Vocal\"" + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var norec = GetNoRec(dvs, cnb, cnc); //dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;
                            bool vocalmissing = true;
                            foreach (var arg in info.Arrangements)//, Type
                            {
                                if (arg.ArrangementType.ToString() == "Vocal") vocalmissing = false;
                            }
                            if (norec > 0 && vocalmissing)
                            {
                                var ftst = "Vocals not included; as manually added in the DLCManager tool, but Option 76 (use songs changes made in DLCManager) is Unselected ergo no DLCManager-DB changes are considered at packing.\n" + data.SongInfo.Artist + " " + data.SongInfo.SongDisplayName;
                                timestamp = UpdateLog(timestamp, "Erro on song.." + ftst, true, tmpPath, multithreadname, form, null, null);
                                ftst = ftst.Replace("\n", "");
                                UpdateDB("Main", "Update Main Set FilesMissingIssues=(\"Issues with Vocal loading(check sng,josn exist besides XML).\") WHERE ID=" + ID + ";", cnb, cnc);/*+REPLACE(FilesMissingIssues,\""+ ftst+"\",\"\")*/
                            }
                        }

                        tsst = "Adding Tones"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        var tz = 0; var j = 0; var jf = 0;
                        try
                        {
                            if (updateTonesArrangs)
                            {
                                //Update Tones
                                var norec = 0;
                                DataSet dfs = new DataSet(); dfs = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                                try
                                {
                                    foreach (var arg in info.TonesRS2014)//, Type
                                    {
                                        j = 0; jf++; //jf,tz,j used for debugging
                                        norec = GetNoRec(dfs, cnb, cnc); //dfs.Tables[0].Rows.Count;
                                        for (j = 0; j < norec; j++)
                                        {
                                            //if (j == 2 && jf == 3)
                                            //    tz = tz;
                                            if (arg.Name == dfs.Tables[0].Rows[j].ItemArray[1].ToString())
                                            {
                                                tz = 0; tz++;//1
                                                var TID = dfs.Tables[0].Rows[j].ItemArray[0].ToString();
                                                data.TonesRS2014[j].Name = dfs.Tables[0].Rows[j].ItemArray[1].ToString();
                                                data.TonesRS2014[j].Volume = float.Parse(dfs.Tables[0].Rows[j].ItemArray[3].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                data.TonesRS2014[j].Key = dfs.Tables[0].Rows[j].ItemArray[4].ToString();
                                                data.TonesRS2014[j].IsCustom = dfs.Tables[0].Rows[j].ItemArray[5].ToString().ToLower() == "true" ? true : false;
                                                data.TonesRS2014[j].SortOrder = decimal.Parse(dfs.Tables[0].Rows[j].ItemArray[8].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                data.TonesRS2014[j].NameSeparator = dfs.Tables[0].Rows[j].ItemArray[9].ToString();
                                                //dictionary types not saved in the DB yet
                                                var nrc = 0;
                                                DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Amp\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsc, cnb, cnc); //dsc.Tables[0].Rows.Count;
                                                tz++;//2
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    if (dsc.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Amp.Type = dsc.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsc.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Amp.Category = dsc.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FG = new Dictionary<string, float>();
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    strArrK = dsc.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsc.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" || strArrV[l] != "") FG.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FG.Count != 0) data.TonesRS2014[j].GearList.Amp.KnobValues = FG;
                                                    if (dsc.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Amp.PedalKey = dsc.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsc.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Amp.Skin = dsc.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsc.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Amp.SkinIndex = float.Parse(dsc.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                                nrc = 0;
                                                DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Cabinet\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsa, cnb, cnc); //dsa.Tables[0].Rows.Count;
                                                tz++;//3
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsa.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Type = dsa.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsa.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Category = dsa.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsa.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsa.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
                                                    if (dsa.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.PedalKey = dsa.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsa.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Skin = dsa.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsa.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.SkinIndex = float.Parse(dsa.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                                nrc = 0;
                                                DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal1\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dss1, cnb, cnc); //dss1.Tables[0].Rows.Count;
                                                tz++;//4
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dss1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Type = dss1.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dss1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Category = dss1.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dss1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dss1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal1.KnobValues = FS;
                                                    if (dss1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.PedalKey = dss1.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dss1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dss1.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dss1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dss1.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                                nrc = 0;
                                                DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal2\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dss2, cnb, cnc); //dss2.Tables[0].Rows.Count; 
                                                tz++;//5
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dss2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Type = dss2.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dss2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Category = dss2.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dss2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dss2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal2.KnobValues = FS;
                                                    if (dss2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.PedalKey = dss2.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dss2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Skin = dss2.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dss2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.SkinIndex = float.Parse(dss2.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal3\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dss3, cnb, cnc); //dss3.Tables[0].Rows.Count;
                                                tz++;//6
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dss3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Type = dss3.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dss3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Category = dss3.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dss3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dss3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal3.KnobValues = FS;
                                                    if (dss3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.PedalKey = dss3.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dss3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Skin = dss3.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dss3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.SkinIndex = float.Parse(dss3.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal4\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dss4, cnb, cnc); //dss4.Tables[0].Rows.Count; 
                                                tz++; //7
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dss4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Type = dss4.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dss4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Category = dss4.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dss4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dss4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal4.KnobValues = FS;
                                                    if (dss4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.PedalKey = dss4.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dss4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Skin = dss4.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dss4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.SkinIndex = float.Parse(dss4.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }

                                                nrc = 0;
                                                DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal1\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsp1, cnb, cnc); //dsp1.Tables[0].Rows.Count;
                                                tz++;//8
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsp1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Type = dsp1.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsp1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Category = dsp1.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsp1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsp1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal1.KnobValues = FS;
                                                    if (dsp1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.PedalKey = dsp1.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsp1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Skin = dsp1.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsp1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.SkinIndex = float.Parse(dsp1.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                                nrc = 0;
                                                DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal2\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsp2, cnb, cnc); //dsp2.Tables[0].Rows.Count; 
                                                tz++;//9
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsp2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Type = dsp2.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsp2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Category = dsp2.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsp2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsp2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal2.KnobValues = FS;
                                                    if (dsp2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.PedalKey = dsp2.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsp2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Skin = dsp2.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsp2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.SkinIndex = float.Parse(dsp2.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal3\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsp3, cnb, cnc); //dsp3.Tables[0].Rows.Count; 
                                                tz++;//10
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsp3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Type = dsp3.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsp3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Category = dsp3.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsp3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsp3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal3.KnobValues = FS;
                                                    if (dsp3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.PedalKey = dsp3.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsp3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Skin = dsp3.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsp3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.SkinIndex = float.Parse(dsp3.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal4\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsp4, cnb, cnc); //dsp4.Tables[0].Rows.Count;
                                                tz++;//11
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsp4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Type = dsp4.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsp4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Category = dsp4.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsp4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsp4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal4.KnobValues = FS;
                                                    if (dsp4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.PedalKey = dsp4.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsp4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Skin = dsp4.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsp4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.SkinIndex = float.Parse(dsp4.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }

                                                nrc = 0;
                                                DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack1\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsr1, cnb, cnc); //dsr1.Tables[0].Rows.Count;
                                                tz++;//12
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsr1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Type = dsr1.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsr1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Category = dsr1.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsr1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsr1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack1.KnobValues = FS;
                                                    if (dsr1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack1.PedalKey = dsr1.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsr1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Skin = dsr1.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsr1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack1.SkinIndex = float.Parse(dsr1.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                                nrc = 0;
                                                DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack2\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsr2, cnb, cnc); //dsr2.Tables[0].Rows.Count;
                                                tz++;//13
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsr2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Type = dsr2.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsr2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Category = dsr2.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsr2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsr2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack2.KnobValues = FS;
                                                    if (dsr2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack2.PedalKey = dsr2.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsr2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Skin = dsr2.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsr2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack2.SkinIndex = float.Parse(dsr2.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack3\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsr3, cnb, cnc); //dsr3.Tables[0].Rows.Count; 
                                                tz++;//14
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsr3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Type = dsr3.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsr3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Category = dsr3.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsr3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsr3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack3.KnobValues = FS;
                                                    if (dsr3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack3.PedalKey = dsr3.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsr3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Skin = dsr3.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsr3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack3.SkinIndex = float.Parse(dsr3.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

                                                }
                                                nrc = 0;
                                                DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack4\" ORDER BY Type DESC;", "", cnb, cnc);
                                                nrc = GetNoRec(dsr4, cnb, cnc); //dsr4.Tables[0].Rows.Count; 
                                                tz++;//15
                                                for (int k = 0; k < nrc; k++)
                                                {
                                                    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                                    if (dsr4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Type = dsr4.Tables[0].Rows[k].ItemArray[0].ToString();
                                                    if (dsr4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Category = dsr4.Tables[0].Rows[k].ItemArray[1].ToString();
                                                    Dictionary<string, float> FS = new Dictionary<string, float>();
                                                    strArrK = dsr4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                                    strArrV = dsr4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                                    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l], NumberStyles.Float, CultureInfo.CurrentCulture));
                                                    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack4.KnobValues = FS;
                                                    if (dsr4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack4.PedalKey = dsr4.Tables[0].Rows[k].ItemArray[4].ToString();
                                                    if (dsr4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Skin = dsr4.Tables[0].Rows[k].ItemArray[5].ToString();
                                                    if (dsr4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack4.SkinIndex = float.Parse(dsr4.Tables[0].Rows[k].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error = true; error_reason += "error updating tones?" + data.SongInfo.Artist + " " + data.SongInfo.SongDisplayName + " " + j + jf + tz + ex;
                                }

                                //Add Arrangements
                                tsst = "Adding Arrangements"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                norec = 0;
                                string sds = "";
                                DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID = " + ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                                norec = GetNoRec(ds, cnb, cnc); //ds.Tables[0].Rows.Count;
                                if (norec > data.Arrangements.Count)
                                {
                                    for (int k = 0; k < norec; k++)
                                    {
                                        bool same = false;
                                        for (int l = 0; l < data.Arrangements.Count; l++)
                                            if (data.Arrangements[l].SongXml.Name.ToString() == ds.Tables[0].Rows[k].ItemArray[26].ToString())
                                                same = true;
                                        if (same) continue;

                                        // Add Vocal Arrangement
                                        var sd = ds.Tables[0].Rows[k].ItemArray[1].ToString();
                                        var a = (sd == "3" || sds == "Bass" ? "-" : (sd == "0" || sds == "Lead" ? "-_" :
                                            (sd == "4" || sds == "Vocals" ? "--" : (sd == "1" || sds == "Rhythm" ? "---" : (sd == "6" || sds == "ShowLights" ? "----" : "l")))));
                                        data.Arrangements.Add(new Arrangement
                                        {
                                            ArrangementName = (sd == "3" || sds == "Bass" ? ArrangementName.Bass : (sd == "0" || sds == "Lead" ? ArrangementName.Lead :
                                            (sd == "4" || sds == "Vocals" ? ArrangementName.Vocals : (sd == "1" || sds == "Rhythm" ? ArrangementName.Rhythm : (sd == "6" || sds == "ShowLights" ? ArrangementName.ShowLights
                                            : (sd == "2" || sds == "Combo" ? ArrangementName.Bass : ArrangementName.Rhythm)))))),// ArrangementName.Vocals,
                                                                                                                                 //ArrangementType = ;// ArrangementType.Vocal,
                                                                                                                                 //        ScrollSpeed = 20,
                                            SongXml = new SongXML { File = ds.Tables[0].Rows[k].ItemArray[5].ToString() },
                                            //        //SongFile = new SongFile { File = "" },
                                            //        CustomFont = false
                                        }
                                                                );
                                    }
                                }
                                tsst = "Continuing Adding Arrangements"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                var n = 0;
                                foreach (var arg in info.Arrangements)//, Type
                                {
                                    sds = ds.Tables[0].Rows[n].ItemArray[1].ToString();
                                    //data.Arrangements[n].Name = ArrangementName.Vocals;
                                    //data.Arrangements[n].Name = ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementName.Bass : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Lead" ? RocksmithToolkitLib.Sng.ArrangementName.Lead : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Vocals" ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Rhythm" ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : ds.Tables[0].Rows[n].ItemArray[12].ToString() == "ShowLights" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
                                    data.Arrangements[n].ArrangementName = (sds == "3" || sds == "Bass" ? ArrangementName.Bass : (sds == "0" || sds == "Lead" ? ArrangementName.Lead :
                                        (sds == "4" || sds == "Vocals" ? ArrangementName.Vocals : (sds == "1" || sds == "Rhythm" ? ArrangementName.Rhythm :
                                        (sds == "6" || sds == "ShowLights" ? ArrangementName.ShowLights : (sds == "2" || sds == "Combo" ? ArrangementName.Bass : ArrangementName.Rhythm))))));
                                    data.Arrangements[n].BonusArr = ds.Tables[0].Rows[n].ItemArray[3].ToString().ToLower() == "true" ? true : false;
                                    sds = ds.Tables[0].Rows[n].ItemArray[4].ToString();
                                    data.Arrangements[n].SongFile = new SongFile { File = ds.Tables[0].Rows[n].ItemArray[4].ToString() == "" ? ds.Tables[0].Rows[n].ItemArray[5].ToString().Replace(".xml", ".json") : ds.Tables[0].Rows[n].ItemArray[4].ToString() }; // if (File.Exists(sds))
                                    data.Arrangements[n].SongXml = new SongXML { File = ds.Tables[0].Rows[n].ItemArray[5].ToString() };
                                    //data.Arrangements[n].SongXml = new SongXML { File = ds.Tables[0].Rows[n].ItemArray[5].ToString() };
                                    data.Arrangements[n].ScrollSpeed = ds.Tables[0].Rows[n].ItemArray[7].ToString().ToInt32();
                                    data.Arrangements[n].Tuning = ds.Tables[0].Rows[n].ItemArray[8].ToString();
                                    data.Arrangements[n].ArrangementSort = ds.Tables[0].Rows[n].ItemArray[12].ToString().ToInt32();
                                    data.Arrangements[n].TuningPitch = ds.Tables[0].Rows[n].ItemArray[13].ToString().ToInt32();
                                    data.Arrangements[n].ToneBase = ds.Tables[0].Rows[n].ItemArray[14].ToString();
                                    //var sd= ds.Tables[0].Rows[15].ItemArray[0].ToString();
                                    if (ds.Tables[0].Rows[n].ItemArray[15].ToString() != "") data.Arrangements[n].Id = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[15].ToString());
                                    else data.Arrangements[n].Id = Guid.NewGuid();
                                    data.Arrangements[n].MasterId = (ds.Tables[0].Rows[n].ItemArray[16].ToString().ToInt32() == 0 ? data.Arrangements[n].MasterId : ds.Tables[0].Rows[n].ItemArray[16].ToString().ToInt32());
                                    data.Arrangements[n].ArrangementType = ds.Tables[0].Rows[n].ItemArray[17].ToString() == "Bass" ? ArrangementType.Bass : ds.Tables[0].Rows[n].ItemArray[17].ToString() == "Guitar" ? ArrangementType.Guitar : ds.Tables[0].Rows[n].ItemArray[17].ToString() == "Vocal" ? ArrangementType.Vocal : ArrangementType.ShowLight;
                                    //RocksmithToolkitLib.Sng.ArrangementType.Bass ds.Tables[0].Rows[17].ItemArray[0].ToString();
                                    if (ds.Tables[0].Rows[n].ItemArray[18].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String0 = short.Parse(ds.Tables[0].Rows[n].ItemArray[18].ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[19].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String1 = short.Parse(ds.Tables[0].Rows[n].ItemArray[19].ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[20].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String2 = short.Parse(ds.Tables[0].Rows[n].ItemArray[20].ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[21].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String3 = short.Parse(ds.Tables[0].Rows[n].ItemArray[21].ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[22].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String4 = short.Parse(ds.Tables[0].Rows[n].ItemArray[22].ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[23].ToString() != "" && data.Arrangements[n].TuningStrings != null) data.Arrangements[n].TuningStrings.String5 = short.Parse(ds.Tables[0].Rows[n].ItemArray[23].ToString());
                                    data.Arrangements[n].PluckedType = ds.Tables[0].Rows[n].ItemArray[24].ToString() == "Picked" ? PluckedType.Picked : PluckedType.NotPicked;
                                    data.Arrangements[n].RouteMask = ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Bass" ? RouteMask.Bass : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Lead" ? RouteMask.Lead : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "None" ? RouteMask.None : RouteMask.Any;
                                    //data.Arrangements[n].SongXml.Name = ds.Tables[0].Rows[n].ItemArray[26].ToString();
                                    //data.Arrangements[n].SongXml.LLID = ds.Tables[0].Rows[n].ItemArray[27].ToInt32().ToInt32();
                                    if (ds.Tables[0].Rows[n].ItemArray[28].ToString() != "") data.Arrangements[n].SongXml.UUID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[28].ToString());
                                    else data.Arrangements[n].SongXml.UUID = Guid.NewGuid();
                                    //data.Arrangements[n].SongFile.Name = ds.Tables[0].Rows[n].ItemArray[29].ToString();
                                    //data.Arrangements[n].SongFile.LLID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[30].ToString().ToString());
                                    if (ds.Tables[0].Rows[n].ItemArray[31].ToString() != "") data.Arrangements[n].SongFile.UUID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[31].ToString());
                                    else data.Arrangements[n].SongFile.UUID = Guid.NewGuid();
                                    //data.Arrangements[n].SongXML.
                                    data.Arrangements[n].ToneMultiplayer = ds.Tables[0].Rows[n].ItemArray[32].ToString();
                                    data.Arrangements[n].ToneA = ds.Tables[0].Rows[n].ItemArray[33].ToString();
                                    data.Arrangements[n].ToneB = ds.Tables[0].Rows[n].ItemArray[34].ToString();
                                    data.Arrangements[n].ToneC = ds.Tables[0].Rows[n].ItemArray[35].ToString();
                                    data.Arrangements[n].ToneD = ds.Tables[0].Rows[n].ItemArray[36].ToString();
                                    n++;
                                }
                            }
                            timestamp = UpdateLog(timestamp, "End Loading song..", true, tmpPath, multithreadname, form, null, null);
                        }
                        catch (Exception ex)
                        {
                            error = true; error_reason += "error updating arangement?" + data.SongInfo.Artist + " " + data.SongInfo.SongDisplayName + " " + j + jf + tz + ex;
                        }
                        //get track no
                        if ((ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") && netstatus != "NOK")
                        {
                            tsst = "Get track no."; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            try
                            {
                                Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                                var trackno = sptyfy.Result.Split(';')[0].ToInt32();
                                filez.Track_No = trackno.ToString("D2");
                                var SpotifySongID = sptyfy.Result.Split(';')[1];
                                var SpotifyArtistID = sptyfy.Result.Split(';')[2];
                                var SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                                var SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                                var SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                                var SpotifyAlbumYear = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";
                                UpdateLog(DateTime.Now, "Retrieved Spotify details " + SpotifyAlbumPath, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes" && SpotifySongID != "" && SpotifySongID != "-" && trackno > 0)
                                {
                                    var cmds = "UPDATE Main SET Has_Track_No=\"Yes\", Track_No=\"" + trackno.ToString("D2") + "\", Spotify_Song_ID=\"" + SpotifySongID + "\", Spotify_Artist_ID=\"" + SpotifyArtistID + "\"";
                                    cmds += ", Spotify_Album_ID=\"" + SpotifyAlbumID + "\"" + ", Spotify_Album_URL=\"" + SpotifyAlbumURL + "\"";// + ",Spotify_Album_Path=\"" + SpotifyAlbumPath + "\"";
                                    cmds += " WHERE ID=" + filez.ID;
                                    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmds + ";", cnb, cnc);
                                    //ADD STADARDISATION UPDATE
                                    //Updating the Standardization table

                                    var updcmd = "UPDATE Standardization SET SpotifyArtistID=\"" + SpotifyArtistID + "\" , SpotifyAlbumID=\"" + SpotifyArtistID + "\", SpotifyAlbumURL=\""
                                        + SpotifyAlbumURL + "\", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\", Year_Correction=\"" + SpotifyAlbumYear + "\" WHERE (Artist=\"" + info.SongInfo.Artist + "\" OR Artist_Correction=\""
                                        + info.SongInfo.Artist + "\") AND (Album=\"" + info.SongInfo.Album + "\" OR Album_Correction=\"" + info.SongInfo.Album + "\")";

                                    UpdateDB("Standardization", updcmd + ";", cnb, cnc);
                                }
                            }
                            catch (Exception ex) { var tust = "Spotify Error ..." + ex; UpdateLog(DateTime.Now, tust, false, c("dlcm_TempPath"), multithreadname, form, null, null); }
                        }

                        //Gather song Lenght
                        tsst = "Gather song Lenght"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        var bitrate = 350001;
                        var bitratep = 350001;
                        var SampleRate = 48001;
                        var PreviewLenght = "";
                        if (filez.oggPreviewPath != null && filez.oggPreviewPath != "")
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes")
                            {
                                using (var vorbis = new NVorbis.VorbisReader(filez.oggPreviewPath))
                                {
                                    PreviewLenght = vorbis.TotalTime.ToString(); bitratep = vorbis.NominalBitrate;
                                }
                                PreviewLenght = (PreviewLenght.Split(':')[0].ToInt32() * 3600 + PreviewLenght.Split(':')[1].ToInt32() * 60 + PreviewLenght.Split(':')[2].Split('.')[0].ToInt32()).ToString();


                                //check Audio bitrate as originals are always at 128..
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes")
                                    using (var vorbis = new NVorbis.VorbisReader(filez.OggPath))
                                    {
                                        bitrate = vorbis.NominalBitrate;
                                        SampleRate = vorbis.SampleRate;
                                    }
                            }

                        //Convert Audio to lower bitrate
                        //Convert Audio if bitrate> ConfigRepository.Instance()["dlcm_Bitrate"].ToInt32() +8000
                        var tst = "";
                        //if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"]=="Yes" && info.OggPath != null)
                        if ((ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && info.OggPath != null)
                           || (((ConfigRepository.Instance()["dlcm_AdditionalManipul9"] == "Yes" && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                           || (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght == "" ? "0" :
                           PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                           > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                           || (ConfigRepository.Instance()["dlcm_AdditionalManipul88"] == "Yes" && float.Parse(PreviewLenght == "" ? "0" :
                           PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                           < float.Parse(ConfigRepository.Instance()["dlcm_MinPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                           && info.OggPath != null)))
                        {
                            var d1 = WwiseInstalled("Convert Audio if bitrate > ConfigRepository");
                            if (d1.Split(';')[0] == "1")
                            {
                                if (PreviewLenght == null || PreviewLenght == "") PreviewLenght = "0";
                                if ((ConfigRepository.Instance()["dlcm_AdditionalManipul9"] == "Yes" && (info.OggPreviewPath == null || info.OggPreviewPath == ""))
                                    || (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                    > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                                    || (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                    < float.Parse(ConfigRepository.Instance()["dlcm_MinPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture))
                                && info.OggPath != null)
                                {
                                    tsst = "Convert Audio to lower bitrate"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                    cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main WHERE ";
                                    cmd += "ID=" + ID + "";
                                    if (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes"
                                        && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture)
                                        ||
                                        ConfigRepository.Instance()["dlcm_AdditionalManipul88"] == "Yes"
                                        && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        < float.Parse(ConfigRepository.Instance()["dlcm_MinPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture)
                                        && info.OggPreviewPath != null) DeleteFile(info.OggPreviewPath, false);
                                    FixMissingPreview(cmd, cnb, AppWD, null, null, false, windw, cnc);
                                    data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wfixMssPrew]") : data.SongInfo.Artist + " [wfixMssPrew]";
                                }

                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && info.OggPath != null)
                                {
                                    cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, OggPath, oggPreviewPath  FROM Main " +
                                        "WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                    cmd += " AND ID=" + ID;
                                    FixAudioIssues(cmd, cnb, AppWD, null, null, false, windw, cnc);
                                    data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wDownAud]") : data.SongInfo.Artist + " [wDownAud]";
                                }

                            }
                            if (d1.Split(';')[2] == "1") { System.Windows.Forms.Application.Exit(); }
                        }

                        //REcompress Audio as some PS3 frameworks are not compatible
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul122"] != "Yes")
                        {
                            tsst = "REcompress Audio as some PS3 frameworks are not compatible"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            File.Copy(filez.oggPreviewPath.ToString(), c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath.ToString()), true);
                            Converters(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath.ToString()), ConverterTypes.Ogg2Wem, false, true);
                            File.Copy(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath.ToString()).Replace(".ogg", ".wem"), filez.audioPreviewPath, true);

                            File.Copy(filez.OggPath.ToString(), c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.OggPath.ToString()), true);
                            Converters(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.OggPath.ToString()), ConverterTypes.Ogg2Wem, false, true);
                            File.Copy(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.OggPath.ToString()).Replace(".ogg", ".wem"), filez.AudioPath, true);

                            var wemo = checkaudiofilesifreadable(filez.audioPreviewPath.ToString()) ? "" : filez.audioPreviewPath;
                            var wem = checkaudiofilesifreadable(filez.AudioPath.ToString()) ? "" : filez.AudioPath;
                            var oggo = checkaudiofilesifreadable(filez.oggPreviewPath.ToString()) ? "" : filez.oggPreviewPath;
                            var ogg = checkaudiofilesifreadable(filez.OggPath.ToString()) ? "" : filez.OggPath;

                            //, "SELECT ID FROM Main WHERE ID=" + filez.ID.ToInt32())
                            if (wem == "" || wemo == "" || ogg == "" || oggo == "") FixAudiofileInconsist(0, null, null, null, timestamp, AppWD, SearchCmd);
                            CleanAudioFolder(filez.AudioPath, filez.audioPreviewPath, filez.oggPreviewPath, filez.OggPath, filez.Folder_Name, filez.ID.ToInt32());

                            //tsst = "framework bug 1 time fix"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            //var paath = c("dlcm_MediaInfo_CLI");
                            //var xx = "";
                            //if (File.Exists(paath)) xx = paath;
                            //else xx = Path.Combine(AppWD, "MediaInfo_CLI_17.12_Windows_x64", "MediaInfo.exe");
                            //if (!File.Exists(xx)) { ErrorWindow frm1 = new ErrorWindow("Install MediaInfo CLI if you want to use it.", c("dlcm_MediaInfo_CLI_www"), "Missing MediaInfo CLI", false, false, true, "", "", "", false); frm1.ShowDialog(); break; }

                            //var startInfo = new ProcessStartInfo
                            //{
                            //    FileName = xx,
                            //    WorkingDirectory = AppWD
                            //};
                            //var t = filez.audioPreviewPath;
                            //startInfo.Arguments = string.Format(" --Inform=Audio;%BitRate% \"{0}\"", t);
                            //startInfo.UseShellExecute = false;
                            //startInfo.CreateNoWindow = true;
                            //startInfo.RedirectStandardOutput = true;
                            //startInfo.RedirectStandardError = true;

                            //if (File.Exists(t))
                            //    using (var DDC = new Process())
                            //    {
                            //        DDC.StartInfo = startInfo;
                            //        DDC.Start();
                            //        string stdoutx = DDC.StandardOutput.ReadToEnd();
                            //        string stderrx = DDC.StandardError.ReadToEnd();
                            //        DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            //        if (stdoutx != "\r\n") if (float.Parse(stdoutx.Replace("\r\n", ""), NumberStyles.Float, CultureInfo.CurrentCulture) > float.Parse(c("dlcm_MaxBitRate"), NumberStyles.Float, CultureInfo.CurrentCulture))
                            //            {
                            //                FixPreview(filez, timestamp);

                            //                timestamp = UpdateLog(timestamp, stdoutx.Replace("\r\n", ""), true, c("dlcm_TempPath"), "", "MainDB", null, null);
                            //            }

                            //    }

                            //filez.audioPreviewPath
                            //tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
                            data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wRECOMPAUD]") : data.SongInfo.Artist + " [wRECOMPAUD]";
                        }
                        if ((!File.Exists(filez.audioPreviewPath) || !File.Exists(filez.AudioPath)))
                            data.SongInfo.Artist = FixAudiofileInconsist(filez.ID.ToInt32(), null, null, null, timestamp, AppWD, "SELECT * FROM Main WHERE ID=" + filez.ID + "") ?
                                (data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wRECOMPAUD]") : data.SongInfo.Artist + " [wRECOMPAUD]") : "";

                        timestamp = (!File.Exists(filez.audioPreviewPath) || !File.Exists(filez.AudioPath)) ?
                            UpdateLog(timestamp, "Error/error no wems on song.." + filez.AudioPath + "--" + filez.audioPreviewPath, true, tmpPath, multithreadname, form, null, null) : timestamp;
                        //Add Multitrack if diff audio
                        //STILLTODO: downstize :)
                        if (filez.MultiTrack_Version.Contains(" (Audio Available)"))
                        {
                            tsst = "get alternative audio"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            data.OggPath = GetAlternativeAudioFile(filez.MultiTrack_Version, filez.Folder_Name, filez.OggPath).Replace(".ogg", ".wem");
                            //GetAlternativeAudioFile(filez.MultiTrack_Version, filez.Folder_Name, filez.OggPath);
                            //data.AudioPath =
                        }

                        //if (ConfigRepository.Instance()["dlcm_AdditionalManipul123"] == "Yes")
                        //{
                        //    var tgst = Show "; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        //    //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
                        //    //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
                        //    //tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
                        //}

                        DirectoryInfo di;
                        var repacked_Path = TempPath + "\\0_repacked";
                        if (!DirectoryExists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

                        //Fix the _preview_preview issue

                        var ms = data.OggPath;
                        try
                        {
                            var sourceAudioFiles = Directory.GetFiles(filez.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);
                            foreach (var fil in sourceAudioFiles)
                            {
                                tst = fil;
                                if (fil.LastIndexOf("_preview_preview.wem") > 0)
                                {
                                    tsst = "Fixed the _preview_preview issue"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                    ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                                    File.Move((ms + "_preview.wem"), (ms + ".wem"));
                                    File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var tust = "Error preview..." + ex; UpdateLog(DateTime.Now, tust, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        }

                        var FN = "";
                        if (MetaChangeActiv("FileName", ConfigRepository.Instance()["dlcm_Activ_FileName"]).Contains("nactive")) //(ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")/*repacked_Path + "\\" + */
                            FN = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
                        else
                            FN = ((filez.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear.ToString() + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul91"] == "Yes")
                        {
                            var tgst = "Add Groups to File Name"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            FN = Groupss + FN;
                        }

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes")
                        {

                            var tgst = "Clean path"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            FN = CleanPath(FN);
                        }

                        data = getmetadata(data, timestamp, filez, Groupss, chbx_UniqueID, ord_no, bassRemoved, chbx_Beta, FN, false);

                        ErrorWindow frm2 = null;
                        if (c("dlcm_AdditionalManipul115") == "Yes" & ConfigRepository.Instance()["dlcm_AdditionalManipul123"] != "Yes")/*metadatadisplayedonce & */
                        {
                            //Show full sumamry of MAetadata of the soon to be packed songs 
                            //if (ConfigRepository.Instance()["dlcm_AdditionalManipul123"] == "Yes")
                            //{
                            //    var tgst = "Start full summary of Metadata of the soon-to-be-packed songs"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            //    if (!GetMEtaBeforePacking(ID.ToInt32(), null, null, null, timestamp, AppWD, SearchCmd, chbx_Beta, Groupss, c("dlcm_SearchFields"))) f= true;
                            //    tgst = "End full summary of Metadata of the soon to be packed songs"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            //}
                            //else
                            //{
                            var txt = "Do you like the metadata?\n\n\n(Yes) Continue packing\n(No) stops current individual packing action.\n(Cancel) stops overall packing action. :\n\n" + ConfigRepository.Instance()["dlcm_Global2TempVariable"].Replace("\n", "\n\n") + "";
                            frm2 = new ErrorWindow(txt, "", "Summary before packing. one song sample", true, false, true, "Continue Packing", "", "Cancel Packing of All", true);
                            frm2.ShowDialog();
                            ConfigRepository.Instance()["dlcm_AdditionalManipul115"] = "No";
                            //}
                        }
                        //if (result2 == DialogResult.Yes) ;
                        //else 
                        if (frm2 is not null) if (frm2.StopImport) return;

                        int progress = 0;
                        var errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        numPlatforms++; var unpackedDir = "";
                        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                        timestamp = UpdateLog(timestamp, "Packing" + PreviewLenght, true, c("dlcm_TempPath"), null, null, null, null);

                        //check if already packed
                        if (c("dlcm_AdditionalManipul98") == "Yes" && form != "MainDB")
                        {
                            DataSet dvr = new DataSet(); if (chbx_PC == "PC") dvr = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Pc\"", "", cnb, cnc);
                            if (GetNoRec(dvr, cnb, cnc) > 0) chbx_PC = "";
                            DataSet dvd = new DataSet(); if (chbx_Mac == "Mac") dvd = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Mac\"", "", cnb, cnc);
                            if (GetNoRec(dvd, cnb, cnc) > 0) chbx_Mac = "";
                            DataSet dvx = new DataSet(); if (chbx_PS3 == "PS3") dvx = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"PS3\"", "", cnb, cnc);
                            if (GetNoRec(dvx, cnb, cnc) > 0) chbx_PS3 = "";
                        }

                        var er = "";
                        if (chbx_PC == "PC")
                            try
                            {
                                dlcSavePath = repacked_Path + "\\" + chbx_PC + "\\" + FN; er = "";
                                er = RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                                if (!File.Exists(er)) { error = true; error_reason += "@PC pack" + er; }
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for " +
                                        "64b https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required"
                                        + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing",
                                        false, false, true, "", "", "", false);
                                    frm1.ShowDialog();
                                }
                                var tgst = dlcSavePath + "Erro generate..." + ex.Message; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@PC pack" + ex.Message;
                            }

                        if (chbx_Mac == "Mac")
                            try
                            {
                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_Mac + "\\" + FN; er = "";
                                er = RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
                                if (!File.Exists(er)) { error = true; error_reason += "@Mac3 pack" + er; }
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b " +
                                        "https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" +
                                        Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false,
                                        true, "", "", "", false);
                                    frm1.ShowDialog();
                                }
                                var tgst = dlcSavePath + "Erro generate..." + ex.Message; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@Mac pack" + ex.Message;
                            }

                        if (chbx_XBOX == "XBOX360")
                            try
                            {
                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_XBOX + "\\" + FN; er = "";
                                er = RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
                                if (!File.Exists(er)) { error = true; error_reason += "@XBOX360 pack" + er; }
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b" +
                                        " https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" +
                                        Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true
                                        , "", "", "", false);
                                    frm1.ShowDialog();
                                }
                                var tgst = dlcSavePath + "Erro at xbox generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@XBOX pack" + ex.Message;
                            }

                        if (chbx_PS3 == "PS3")
                            try
                            {
                                //Use Orig Audio as some PS3 are badly encoded and DLCM is not yet fully functional there

                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul122"] == "Yes")
                                {
                                    var tgst = "Use Orig Audio as some PS3 are badly encoded and DLCM is not yet fully functional there"; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                    RocksmithToolkitLib.Platform SourcePlatform = (c("dlcm_TempPath") + "\\0_old\\" + filez.Original_FileName).GetPlatform();
                                    unpackedDir = Packer.Unpack(c("dlcm_TempPath") + "\\0_old\\" + filez.Original_FileName, c("dlcm_TempPath") + "\\0_temp\\", SourcePlatform, true, true);
                                    DLCPackageData infoz = null;
                                    infoz = DLCPackageData.LoadFromFolder(unpackedDir, platform);

                                    var oldprev = filez.audioPreviewPath;
                                    var olda = filez.AudioPath;
                                    filez.audioPreviewPath = infoz.OggPreviewPath is null ? infoz.OggPath : infoz.OggPreviewPath;
                                    filez.AudioPath = infoz.OggPath;
                                    filez.oggPreviewPath = infoz.OggPreviewPath is null ? filez.OggPath : infoz.OggPreviewPath.Replace(".wem", "_fixed.ogg");
                                    filez.OggPath = infoz.OggPath.Replace(".wem", "_fixed.ogg");//is null ? filez.OggPath : infoz.OggPreviewPath.Replace(".wem", "_fixed.ogg");

                                    var wemo = checkaudiofilesifreadable(filez.audioPreviewPath.ToString()) ? oldprev : filez.audioPreviewPath;
                                    var wem = checkaudiofilesifreadable(filez.AudioPath.ToString()) ? olda : filez.AudioPath;
                                    var oggo = checkaudiofilesifreadable(filez.oggPreviewPath.ToString()) ? "" : filez.oggPreviewPath;
                                    var ogg = checkaudiofilesifreadable(filez.OggPath.ToString()) ? "" : filez.OggPath;
                                    data.OggPreviewPath = wemo;
                                    data.OggPath = wem;
                                    data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wORIGAUD]") : data.SongInfo.Artist + " [wORIGAUD]";
                                    //, "SELECT ID FROM Main WHERE ID=" + filez.ID.ToInt32())
                                    if (ogg == "" || oggo == "" || wem == "" || wemo == "")/*wem == olda || wemo == oldprev || */
                                    {
                                        UpdateLog(DateTime.Now, "Issues at using orig audio", false, ConfigRepository.Instance()["dlcm_TempPath"], null, null, null, null);
                                        data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wprob@ORIGaud]") : data.SongInfo.Artist + " [wprob@ORIGaud]"; ;
                                    }
                                    //MessageBox.Show("Issues at using orig audio");
                                }

                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_PS3 + "\\" + FN; er = "";
                                er = RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));

                                //try again with recompressed wem
                                if (!File.Exists(er))
                                    try
                                    {
                                        var tgt = dlcSavePath + "wem erro..." + er; UpdateLog(DateTime.Now, tgt, false, ConfigRepository.Instance()["dlcm_TempPath"], null, null, null, null);

                                        var audio = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.OggPath);
                                        File.Copy(filez.OggPath, audio, true);
                                        UtilitiesFunctions.Converters(audio, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);

                                        DeleteFile(audio.Replace(".ogg", ".wav"), false);
                                        File.Copy(audio.Replace(".ogg", ".wem"), filez.AudioPath, true);
                                        DeleteFile(audio.Replace(".ogg", ".wem"), false);

                                        var audiop = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath);
                                        File.Copy(filez.oggPreviewPath, audiop, true);
                                        UtilitiesFunctions.Converters(audiop, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);

                                        DeleteFile(audiop.Replace(".ogg", ".wav"), false);
                                        File.Copy(audiop.Replace(".ogg", "_preview.wem"), filez.audioPreviewPath, true);
                                        DeleteFile(audiop.Replace(".ogg", ".wem"), false);

                                        data.SongInfo.Artist = data.SongInfo.Artist.Contains("]") ? data.SongInfo.Artist.Replace("]", " wRecompressedAudio]") : data.SongInfo.Artist + " [wRecompressedAudio]";

                                        var er1 = er; er = "";
                                        er = RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                                        progress += step;
                                        if (!File.Exists(er)) { error = true; error_reason += "@PS3 pack: errro1:" + er1 + "; error2:" + er; }
                                    }
                                    catch (Exception exx)
                                    {
                                        if (exx.Message.IndexOf("No JDK or JRE") > 0)
                                        {
                                            ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b " +
                                                "https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" +
                                                Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false,
                                                true, "", "", "", false);
                                            frm1.ShowDialog();
                                            string ss = string.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, exx.StackTrace);
                                        }

                                        var tgst = dlcSavePath + "Erro @ps3generate..." + exx.Message + "-" + c("dlcm_errorsstring"); UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                        error = true;
                                        error_reason += "@PS3 pack" + er;
                                    }
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    var tgt = dlcSavePath + "wem erro..." + ex.Message.Replace("error", "erro") + "-" + c("dlcm_errorsstring"); UpdateLog(DateTime.Now, tgt, false, ConfigRepository.Instance()["dlcm_TempPath"], null, null, null, null);

                                    //FIXbrokenWEM()
                                    var audio = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.OggPath);
                                    File.Copy(filez.OggPath, audio, true);
                                    UtilitiesFunctions.Converters(audio, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
                                    DeleteFile(audio.Replace(".ogg", ".wav"), false);
                                    File.Copy(audio.Replace(".ogg", ".wem"), filez.AudioPath, true);
                                    DeleteFile(audio.Replace(".ogg", ".wem"), false);


                                    var audiop = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath);
                                    File.Copy(filez.oggPreviewPath, audiop, true);
                                    UtilitiesFunctions.Converters(audiop, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
                                    DeleteFile(audiop.Replace(".ogg", ".wav"), false);
                                    File.Copy(audiop.Replace(".ogg", "_preview.wem"), filez.audioPreviewPath, true);
                                    DeleteFile(audiop.Replace(".ogg", ".wem"), false);

                                    RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                                    progress += step;
                                }
                                catch (Exception exx)
                                {
                                    //var tgt = "Erro during rocksmith lib internal packaging..." + exx.Message+"-"+filez.OggPath; UpdateLog(DateTime.Now, tgt, false, ConfigRepository.Instance()["dlcm_TempPath"], null, null, null, null);

                                    if (exx.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                    {
                                        ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b " +
                                            "https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" +
                                            Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false,
                                            true, "", "", "", false);
                                        frm1.ShowDialog();
                                        string ss = string.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace);
                                    }

                                    var tgst = dlcSavePath + "Erro @ps3generate..." + exx.Message + "-" + c("dlcm_errorsstring"); UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                    error = true;
                                    error_reason += "@PS3 pack" + ex.Message;
                                }
                            }
                        data.CleanCache();
                        DeleteDirectory(unpackedDir, true);
                        i++;
                    }
                    h = dlcSavePath;
                }

                var source = "";
                string copyftp = "";
                var dest = "";
                var copiedpath = "";

                if (h != "")
                {
                    timestamp = UpdateLog(timestamp, "Start the Copy/FTPing process " + dlcSavePath, true, tmpPath, multithreadname, form, null, null);

                    //calc hash and file size
                    System.IO.FileInfo fi = null;
                    try
                    {
                        var platfrm = "_ps3";
                        if (chbx_PS3 == "PS3")
                        {
                            h = h.Replace("\\0_repacked\\PC", "\\0_repacked\\PS3").Replace("\\0_repacked\\Mac", "\\0_repacked\\PC").Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC");
                            source = h.IndexOf("_ps3.psarc.edat") <= 0 ? h + "_ps3.psarc.edat" : h; fi = new System.IO.FileInfo(source);
                            if (!File.Exists(source)) { error = true; error_reason += "ps3 file missing. broken packaging."; }
                            else if (fi.Length == 0) { error = true; error_reason += "ps3 filesize zero."; }

                            var u = ""; var a = "";

                            if (File.Exists(source) && chbx_Copy)
                            {
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul92"] == "Yes")
                                {
                                    var TrueGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game\\") + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")).Substring(33, 9);
                                    //copy edat in DLC folder
                                    var dst = TrueGameFldr + "\\USRDIR\\DLC\\" + Path.GetFileName(source);
                                    if (File.Exists(source) && dst.Length < 256) File.Copy(source, dst, true);
                                    timestamp = UpdateLog(timestamp, "copy edat in DLC folder" + dst, true, tmpPath, multithreadname, form, null, null);
                                }
                                else
                                {
                                    dest = txt_FTPPath;
                                    if (chbx_Replace) u = DeleteFTPedSongs(txt_RemotePath, dest, cnb, txt_DLC_ID, ftpstatus, cnc);
                                    a = FTPFile(txt_FTPPath, source, TempPath, SearchCmd, ID, cnb, ftpstatus, cnc);
                                    copyftp = (" and " + a + " FTPed(PS3)").Replace("  ", " ");
                                    timestamp = UpdateLog(timestamp, copyftp, true, tmpPath, multithreadname, form, null, null);
                                }
                            }
                            else
                                if (!File.Exists(source))
                            {
                                error = true; error_reason += "@FTP";
                            }

                            if (!error && ConfigRepository.Instance()["dlcm_AdditionalManipul101"] == "Yes")
                            {
                                error = CheckSong(source); if (error) error_reason += "Packed but failing at Ps3Packing PSARC ErrorCheck"+ ConfigRepository.Instance()["dlcm_Global2TempVariable"];
                            }
                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest + fi.Name;
                            if (!error)
                                Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_FTPPath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PS3, packid, a, chbx_Copy, cnc);
                        }
                        //else if (chbx_PS3 == "PS3" && !error) Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_FTPPath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PS3, packid, "");

                        platfrm = "_p"; copiedpath = "";
                        if (chbx_PC == "PC")// && chbx_Copy
                        {
                            source = h.IndexOf("_p.psarc") <= 0 ? h.Replace("\\0_repacked\\PS3", "\\0_repacked\\PC").Replace("\\0_repacked\\Mac", "\\0_repacked\\PC").Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC") + platfrm + ".psarc" : h;
                            fi = new System.IO.FileInfo(source);
                            if (fi.Length == 0 || !File.Exists(source))
                            {
                                error = true; error_reason += "chbx_PC filesize zero";
                            }
                            dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
                            if (!error && ConfigRepository.Instance()["dlcm_AdditionalManipul101"] == "Yes")
                            {
                                error = CheckSong(source); if (error) error_reason += "Packed but failing at Pc Packing PSARC ErrorCheck" + ConfigRepository.Instance()["dlcm_Global2TempVariable"];
                            }

                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest.Replace(Path.GetDirectoryName(dest), Directory.Exists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0 ? c("general_rs2014path") : c("dlcm_PC"));
                            if (!error)
                                copyftp += " and " + Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_RemotePath, source, copiedpath == "" ? dest : copiedpath, cnb, fi, ID, DLC_Name, chbx_PC, packid, "", chbx_Copy, cnc) //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                 + " Copied(PC)";
                        }
                        //else if (chbx_PC == "PC" && !error) Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_FTPPath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PS3, packid, "");

                        platfrm = "_m"; copiedpath = "";
                        if (chbx_Mac == "Mac")// && chbx_Copy
                        {
                            source = h.IndexOf("_m.psarc") <= 0 ? h.Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC").Replace("\\0_repacked\\PS3", "\\0_repacked\\Mac").Replace("\\0_repacked\\PC", "\\0_repacked\\Mac") + platfrm + ".psarc" : h;
                            fi = new System.IO.FileInfo(source);
                            if (fi.Length == 0 || !File.Exists(source))
                            {
                                error = true; error_reason += "mac filezise zero";
                            }
                            dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
                            if (!error && ConfigRepository.Instance()["dlcm_AdditionalManipul101"] == "Yes")
                            {
                                error = CheckSong(source); if (error) error_reason += "Packed but failing at Mac Packing PSARC ErrorCheck" + ConfigRepository.Instance()["dlcm_Global2TempVariable"];
                            }
                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest.Replace(Path.GetDirectoryName(dest), Directory.Exists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac"));
                            if (!error)
                                copiedpath = dest.Replace(Path.GetDirectoryName(dest), Directory.Exists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac"));
                            copyftp += " and " + Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_RemotePath, source, copiedpath == "" ? dest : copiedpath, cnb, fi, ID, DLC_Name, chbx_Mac, packid, "", chbx_Copy, cnc) //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                            + " Copied(Mac)";
                        }
                        //else if (chbx_Mac == "Mac" && !error) Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_FTPPath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PS3, packid, "");
                        timestamp = UpdateLog(timestamp, "Stop the Copy/FTPing process", true, tmpPath, multithreadname, form, null, null);
                    }
                    catch (Exception ex)
                    {
                        var tgst = "Erro after packing at ftping..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        error = true; error_reason += tgst;
                    }
                }
                else
                {
                    error = true; error_reason += "Packed path is empty " + dlcSavePath;
                }
                //Add Pack Audit Trail

                //cnb.Close();//prb wont work as variable only avail in utilities 

            }
            catch (Exception ex)
            {
                error = true; error_reason += "overall?" + ex.Message;
            }

            //Restore XML changes
            var xmlFilex = DirectoryExists(Folder_Name) ? Directory.GetFiles(Folder_Name, "*.old", System.IO.SearchOption.AllDirectories) : null;
            if (xmlFilex != null) foreach (var xml in xmlFilex)
                {
                    if (xml.ToLower().IndexOf("showlights") < 0)
                        try
                        {
                            File.Copy(xml, xml.Replace(".old", ""), true);
                            timestamp = UpdateLog(timestamp, "Restore XML", true, tmpPath, multithreadname, form, null, null);
                            DeleteFile(xml, false);
                        }
                        catch (Exception ex)
                        {
                            var tgst = "Error at restoring xml changes..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
                        }
                }

            if (error)
            {
                e.Cancel = true;
                UpdatePackingLog("LogPackingError", ConfigRepository.Instance()["dlcm_DBFolder"], packid.ToInt32(), args[0], error_reason, cnb, cnc);
                timestamp = UpdateLog(timestamp, "End Packing", true, tmpPath, multithreadname, form, null, null);
            }
            else
                UpdatePackingLog("LogPacking", ConfigRepository.Instance()["dlcm_DBFolder"], packid.ToInt32(), args[0], "", cnb, cnc);


            e.Cancel = true;
            e.Result = "done";
            ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "Yes";
            return;
        }

        public static DLCPackageData getmetadata(DLCPackageData data, DateTime timestamp, MainDBfields filez, string Groupss, bool chbx_UniqueID
            , string ord_no, bool bassRemoved, bool chbx_Beta, string FN, bool uniformcolumnlenght)/*, MainDBfields[] SongRecord*/
        {
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord[0] = filez;
            var tsst = "Medata data manipulating"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), null, null, null, null);

            //SongRecord[0].Author = filez.Author;

            filez.Groups = Groupss;

            //IntheWorksWDetails
            var r1 = ConfigRepository.Instance()["dlcm_Title"]; ConfigRepository.Instance()["dlcm_Title"] = AddIntheWorks(c("dlcm_Title"));
            var r2 = ConfigRepository.Instance()["dlcm_Title_Sort"]; ConfigRepository.Instance()["dlcm_Title_Sort"] = AddIntheWorks(c("dlcm_Title_Sort"));
            var r3 = ConfigRepository.Instance()["dlcm_Artist"]; ConfigRepository.Instance()["dlcm_Artist"] = AddIntheWorks(c("dlcm_Artist"));
            var r4 = ConfigRepository.Instance()["dlcm_Artist_Sort"]; ConfigRepository.Instance()["dlcm_Artist_Sort"] = AddIntheWorks(c("dlcm_Artist_Sort"));
            var r5 = ConfigRepository.Instance()["dlcm_Album"]; ConfigRepository.Instance()["dlcm_Album"] = AddIntheWorks(c("dlcm_Album"));
            var r6 = ConfigRepository.Instance()["dlcm_Album_Sort"]; ConfigRepository.Instance()["dlcm_Album_Sort"] = AddIntheWorks(c("dlcm_Album_Sort"));
            var r7 = ConfigRepository.Instance()["dlcm_File_Name"]; ConfigRepository.Instance()["dlcm_File_Name"] = AddIntheWorks(c("dlcm_File_Name"));
            var r8 = ConfigRepository.Instance()["dlcm_Lyric_Info"]; ConfigRepository.Instance()["dlcm_Lyric_Info"] = AddIntheWorks(c("dlcm_Lyric_Info"));

            //manipulating the info
            if (c("dlcm_AdditionalManipul102") != "Yes")
            {
                //if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes")
                    data.SongInfo.SongDisplayName = (MetaChangeActiv("SongDisplayName", ConfigRepository.Instance()["dlcm_Activ_Title"]).Contains("nactive")) ? data.SongInfo.SongDisplayName : Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false, false, cnc);
                //if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") 
                    data.SongInfo.SongDisplayNameSort = (MetaChangeActiv("SongDisplayNameSort", ConfigRepository.Instance()["dlcm_Activ_TitleSort"]).Contains("nactive")) ? data.SongInfo.SongDisplayNameSort : Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);
                //if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") 
                    data.SongInfo.Artist = (MetaChangeActiv("Artist", ConfigRepository.Instance()["dlcm_Activ_Artist"]).Contains("nactive")) ? data.SongInfo.Artist : Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false, false, cnc);
                //if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") 
                    data.SongInfo.ArtistSort = (MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")) ? data.SongInfo.ArtistSort : Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);
                //if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") 
                    data.SongInfo.Album = (MetaChangeActiv("Album", ConfigRepository.Instance()["dlcm_Activ_Album"]).Contains("nactive")) ? data.SongInfo.Album : Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true, false, cnc);
                //if (ConfigRepository.Instance()["dlcm_Activ_AlbumSort"] == "Yes") 
                    data.SongInfo.AlbumSort = (MetaChangeActiv("AlbumSort", ConfigRepository.Instance()["dlcm_Activ_AlbumSort"]).Contains("nactive")) ? data.SongInfo.AlbumSort : Manipulate_strings(ConfigRepository.Instance()["dlcm_Album_Sort"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true, false, cnc);

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes") //21.Pack with The/ Die only at the end of Title Sort 
                {
                    if (data.SongInfo.SongDisplayNameSort.Length > 4) data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                    filez.Song_Title_Sort = data.SongInfo.SongDisplayNameSort;
                    if (data.SongInfo.ArtistSort.Length > 4) data.SongInfo.ArtistSort = MoveTheAtEnd(data.SongInfo.ArtistSort);
                    filez.Artist_Sort = data.SongInfo.ArtistSort;
                    if (data.SongInfo.AlbumSort.Length > 4) data.SongInfo.AlbumSort = MoveTheAtEnd(data.SongInfo.AlbumSort);
                    filez.Album_Sort = data.SongInfo.AlbumSort;
                }
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && data.SongInfo.ArtistSort.Length > 4) //21.Pack with The/ Die only at the end of Title Sort 
                {
                    if (data.SongInfo.SongDisplayNameSort.Length > 4) data.SongInfo.SongDisplayName = MoveTheAtEnd(data.SongInfo.SongDisplayName);
                    filez.Song_Title = data.SongInfo.SongDisplayNameSort;
                    if (data.SongInfo.Artist.Length > 4) data.SongInfo.Artist = MoveTheAtEnd(data.SongInfo.Artist);
                    filez.Artist = data.SongInfo.Artist;
                    if (data.SongInfo.Album.Length > 4) data.SongInfo.Album = MoveTheAtEnd(data.SongInfo.Album);
                    filez.Album = data.SongInfo.Album;
                }

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul1"] == "Yes")
                    data.SongInfo.SongDisplayName = ord_no + "_" + data.SongInfo.SongDisplayName;
            }

            if (c("dlcm_AdditionalManipul44") == "Yes")
                data.AppId = ConfigRepository.Instance()["general_defaultappid_RS2014"];// (data.Version == GameVersion.RS2014 ?: ConfigRepository.Instance()["general_defaultappid_RS2012"]);

            if (c("dlcm_AdditionalManipul113") == "Yes")
                if (MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive"))
                    if (!ConfigRepository.Instance()["dlcm_Artist_Sort"].Contains("GroupIndex"))
                    {
                        var r = ConfigRepository.Instance()["dlcm_Artist_Sort"];
                        ConfigRepository.Instance()["dlcm_ArtistSort"] = ConfigRepository.Instance()["dlcm_Artist_Sort"].Replace("<FirstGroupIndexAndName>", "");
                        data.SongInfo.ArtistSort = r;
                        ConfigRepository.Instance()["dlcm_Artist_Sort"] = ConfigRepository.Instance()["dlcm_Artist_Sort"].Replace("<Beta>", "");
                        data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);
                        data.SongInfo.ArtistSort = Manipulate_strings("<FirstGroupIndexAndName>", 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc)
                    + data.SongInfo.ArtistSort;
                        ConfigRepository.Instance()["dlcm_Artist_Sort"] = r;
                    }

            if (ConfigRepository.Instance()["dlcm_AdditionalManipul2"] == "Yes")
                //"3. Make all DLC IDs unique (&save)"
                if (filez.UniqueDLCName != null && filez.UniqueDLCName != "") data.Name = filez.UniqueDLCName;
                else
                {
                    Random random = new Random();
                    data.Name = random.Next(0, 100000) + data.Name;
                }

            if (chbx_UniqueID)
            {
                Random random = new Random();
                data.Name = random.Next(0, 100000) + data.Name;
            }


            if (data == null)
                UpdateLog(DateTime.Now, "One or more fields are missing information.", false, c("dlcm_TempPath"), "", "", null, null);

            //Add comments to beginning of the lyrics
            var der = ConfigRepository.Instance()["dlcm_AdditionalManipul73"];
            var ft = filez.Has_Vocals;

            /*File Name should be standardised.. no need for 0&Group at the beginning MAYBE MAYBE WHAT IF i wanna structure my files based on group anyway((ConfigRepository.Instance()["dlcm_File_Name"].IndexOf("<Beta>") > -1) ? "" : "0") + */


            // data.ToolkitInfo.PackageVersion = filez.Version;


            ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = filez.Author + ";" + data.Name + ";" + filez.Track_No + ";" + filez.Version + ";" + SongRecord[0].ID
                + ";" + filez.EoFPath + ";" + filez.YouTube_Link + ";" + filez.BasedOn_Youtube
            + ";" + filez.BasedOn_CF + ";" + filez.BasedOn_Tabs + ";" + filez.Spotify_Song_ID + ";" + filez.Description + ";" + filez.ToDos
            + ";" + filez.ToneDetails + ";" + "Yes"
            + ";" + ";" + "Yes" + ";" + "Yes" + ConfigRepository.Instance()["dlcm_EoFPath"]
            + ";" + ";" + filez.PackingDate + ";" + filez.UpdateVersionDate + ";" + filez.BasedOn_GP
            + "Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate,BasedOn_GP;";

            //data.ToolkitInfo.PackageComment = ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + data.ToolkitInfo.PackageComment;
            ConfigRepository.Instance()["dlcm_Global2TempVariable"] =
               // "\nArtistSort: " + data.SongInfo.ArtistSort +// (uniformcolumnlenght?:)+
               // "\nArtist: " + data.SongInfo.Artist +
               // "\nSongDisplayName: " + data.SongInfo.SongDisplayName +
               // "\nSongDisplayNameSort: " + data.SongInfo.SongDisplayNameSort +
               // "\nAlbum: " + data.SongInfo.Album +
               // "\nAlbumSort: " + data.SongInfo.AlbumSort +
               // "\nDLCName: " + data.Name +
               // "\nDLC_ID: " + data.AppId +
               // "\nFile Name: " + FN +
               //(c("dlcm_Activ_LyricInfo") == "Yes" ? "\nLyrics: " + Manipulate_strings(ConfigRepository.Instance()["dlcm_Lyric_Info"], 0, false, false, bassRemoved ? true : false, SongRecord, "", "", chbx_Beta, false, false, cnc) : "") +
               // "\nPackage internal Comment: " + ((ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + filez.PackageDetails) == null ? "" : (ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + filez.PackageDetails))
                
                "ArtistSort" + MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]) + data.SongInfo.ArtistSort 
                + "\n" + "Artist" + MetaChangeActiv("Artist", ConfigRepository.Instance()["dlcm_Activ_Artist"]) + data.SongInfo.Artist 
                + "\n" + "SongDisplayName" + MetaChangeActiv("SongDisplayName", ConfigRepository.Instance()["dlcm_Activ_Title"]) + data.SongInfo.SongDisplayName 
                + "\n" + "SongDisplayNameSort" + MetaChangeActiv("SongDisplayNameSort", ConfigRepository.Instance()["dlcm_Activ_TitleSort"]) + data.SongInfo.SongDisplayNameSort 
                + "\n" + "Album" + MetaChangeActiv("Album", ConfigRepository.Instance()["dlcm_Activ_Album"]) + data.SongInfo.Album 
                + "\n" + "AlbumSort" + MetaChangeActiv("AlbumSort", ConfigRepository.Instance()["dlcm_Activ_AlbumSort"]) + data.SongInfo.AlbumSort 
                + "\n" + "DLCName: " + data.Name  //+ MetaChangeActiv("DLCName", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + "\n" + "DLC_ID: " + data.AppId  //+ MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + "\n" + "File Name" + MetaChangeActiv("FileName", ConfigRepository.Instance()["dlcm_Activ_FileName"]) + FN 
                + "\n" + "Lyrics" + MetaChangeActiv("Lyrics", ConfigRepository.Instance()["dlcm_Activ_LyricsInfo"]) + Manipulate_strings(ConfigRepository.Instance()["dlcm_Lyric_Info"], 0, false, false, bassRemoved ? true : false, SongRecord, "", "", chbx_Beta, false, false, cnc)
                +" \n" + "Package internal Comment: " + ((ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + filez.PackageDetails) == null ? "" : (ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + filez.PackageDetails))
            + "----";

            //IntheWorksWDetails
            ConfigRepository.Instance()["dlcm_Title"] = r1;
            ConfigRepository.Instance()["dlcm_Title_Sort"] = r2;
            ConfigRepository.Instance()["dlcm_Artist"] = r3;
            ConfigRepository.Instance()["dlcm_Artist_Sort"] = r4;
            ConfigRepository.Instance()["dlcm_Album"] = r5;
            ConfigRepository.Instance()["dlcm_Album_Sort"] = r6;
            ConfigRepository.Instance()["dlcm_File_Name"] = r7;
            ConfigRepository.Instance()["dlcm_Lyric_Info"] = r8;

            data.SongInfo.SongDisplayName = filez.Song_Title;
            data.SongInfo.SongDisplayNameSort = filez.Song_Title_Sort;
            data.SongInfo.Artist = filez.Artist;
            data.SongInfo.ArtistSort = filez.Artist_Sort;
            data.SongInfo.Album = filez.Album;
            data.SongInfo.AlbumSort = filez.Album_Sort;
            data.Name = filez.DLC_Name;
            data.ToolkitInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo
            {
                PackageAuthor = filez.Author,
                PackageVersion = filez.Version,
                PackageComment = ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + filez.Comments
            };

            UpdateLog(DateTime.Now, "Metadata:\n" + ConfigRepository.Instance()["dlcm_Global2TempVariable"].Replace("error", "erro").Replace("Error", "erro") + "\n", false, c("dlcm_TempPath"), "", "", null, null);/*\n*/
            return data;
        }

        public static void FixPreview(MainDBfields filez, DateTime timestamp)
        {
            if (filez.ID == null) return;
            var t = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(filez.oggPreviewPath);
            File.Copy(filez.oggPreviewPath, t, true);
            UtilitiesFunctions.Converters(t, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
            var fifi = new System.IO.FileInfo(t.Replace(".ogg", ".wem"));
            if (!File.Exists(t.Replace(".ogg", ".wem")) || fifi.Length == 0)
            {
                //fix as sometime the template folder gets poluted and breaks eveything
                var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                var templateDir = Path.Combine(appRootDir, "Template");
                var backup_dir = AppWD + "\\Template";
                //DeleteDirectory(templateDir, false);
                CopyFolder(backup_dir, templateDir);
                // var wwiseTemplateDir = LoadWwiseTemplate(wavSourcePath, audioQuality);
                UtilitiesFunctions.Converters(t, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
            }
            DeleteFileIfExisting(t.Replace(".ogg", "_fixed.wav"));
            DeleteFileIfExisting(t.Replace(".ogg", "_preview_fixed.wav"));
            DeleteFileIfExisting(t.Replace(".ogg", "_preview_fixed.ogg"));
            File.Copy(t.Replace(".ogg", ".wem"), filez.audioPreviewPath, true);
            DeleteFileIfExisting(t);
            filez.oggPreviewPath = FixWEMwDiffName(filez.oggPreviewPath, filez.Folder_Name, timestamp, null, null, null, null, null);
            //File.Copy(t.Replace(".ogg", ".wem"), filez.oggPreviewPath.Replace(".ogg", ".wem"), true); 
            return;
        }
        //public static void GeneratePackingSummary(string pack, string metainfo, int brkn, OleDbConnection cnb, int total, int norows, SQLiteConnection cnz) 
        public static void GeneratePackingSummary(string pack, string metainfo, int brkn, OleDbConnection cnb, int total, int norows, SQLite.SQLiteConnection cnc)
        {
            //GenerateSumamrty
            /*var total = 0;*/
            var PS3P = 0; var PCP = 0; var MACP = 0; var XBOXP = 0; var FailedP = 0; var ListP = "\n"; ; var ListNP = "\n";
            var PS3F = 0; var PCF = 0; var MACF = 0; var XBOXF = 0; var cpy = 0;
            ////DataSet dnz = new DataSet(); dnz = SelectFromDB("Main", cmds, null, cnb, cnc);
            ////if (dnz.Tables.Count > 0) total = dmz.Tables[0].Rows.Count;var cmds = ""; 
            var dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"PS3\"";
            //DataSet /*dmz*/ = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb, cnc);
            // if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            PS3P = GetNoRecords(dmz, cnb, cnc);//dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"PC\"";
            //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0)
            PCP = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                               //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail",
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"MAC\"";//, null, cnb, cnc);// ;
                                                                                                           //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0)
            MACP = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                                //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", 
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"XBOX360\"";
            //    , null, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            XBOXP = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                                 //DataSet dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"", txt_DBFolder.Text, cnb, cnc);
                                                 //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows.Count;
                                                 //DataSet dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"", txt_DBFolder.Text, cnb, cnc);
                                                 //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows.Count;dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\"", txt_DBFolder.Text, cnb, cnc);
                                                 //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", 
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"PS3\" AND FTPed=\"Yes\"";
            //    , null, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            PS3F = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                                //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail",
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"PC\" AND FTPed=\"Yes\"";
            //, null, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            PCF = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                               //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", 
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"MAC\" AND FTPed=\"Yes\"";//, null, cnb, cnc);
                                                                                                                             //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            MACF = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                                //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", 
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND UCASE(Platform)=\"XBOX360\" AND FTPed=\"Yes\"";//, null, cnb, cnc);
                                                                                                                                 // if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            XBOXF = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                                 // dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail",
            dmz = "SELECT ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND FTPed=\"Yes\"";// ;// ;// ;
                                                                                                 //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            cpy = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                                               //dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError",
            dmz = "SELECT ID FROM LogPackingError where Pack=\"" + pack + "\"";
            //    , null, cnb, cnc) ;
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) 
            FailedP = GetNoRecords(dmz, cnb, cnc); //dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();

            var gmz = new DataSet(); gmz = SelectFromDB("LogPackingError", "SELECT CDLC_ID, Comments FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb, cnc); ;
            var noOfRecs = GetNoRec(gmz, cnb, cnc);//if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0)  = dmz.Tables.Count == 0 ? 0 : dmz.Tables[0].Rows.Count;
                                                   //var packapth = "";
            for (var j = 0; j < noOfRecs; j++)
            {
                var dnz = new DataSet(); dnz = SelectFromDB("Main", "SELECT Artist, Song_Title FROM Main where ID=" + gmz.Tables[0].Rows[j].ItemArray[0].ToString() + "", null, cnb, cnc);
                //if (dnz.Tables.Count > 0) if (dnz.Tables[0].Rows.Count > 0) noOfRecs = dmz.Tables[0].Rows.Count;
                var noOfRecu = GetNoRec(dnz, cnb, cnc);
                ListNP += j + ". " + gmz.Tables[0].Rows[j].ItemArray[0].ToString() + "-" + (noOfRecu > 0 ? dnz.Tables[0].Rows[0].ItemArray[0].ToString() : "-") + "-" + dnz.Tables[0].Rows[0].ItemArray[1].ToString() + "-" +
                    gmz.Tables[0].Rows[j].ItemArray[1].ToString() + "\n";
                //dnz.Dispose();
                //packapth = dmz.Tables[0].Rows[j].ItemArray[1].ToString();
            }

            var cmz = new DataSet(); cmz = SelectFromDB("Pack_AuditTrail", "SELECT FileName, PackPath, CDLC_ID FROM Pack_AuditTrail where Pack=\"" + pack + "\"", null, cnb, cnc);
            noOfRecs = GetNoRec(cmz, cnb, cnc);//dmz.Tables.Count == 0 ? 0 : cmz.Tables[0].Rows.Count;
            for (var k = 0; k < noOfRecs; k++)
                //{
                ListP += k + ". " + cmz.Tables[0].Rows[k].ItemArray[2].ToString() + " - " + cmz.Tables[0].Rows[k].ItemArray[0].ToString() + "\n";
            //packapth = cmz.Tables[0].Rows[k].ItemArray[1].ToString();
            //}

            //Show Summary window
            var summary = "Packed/(Copied/FTPed) Summary(PackID: " + pack + " of processed " + total + " of " + norows + " selected songs ) \n" +
                "\nPacked PS3:" + PS3P + "/" + PS3F +
               "\nPacked PC: " + PCP + "/" + PCF +
                "\nPacked MAC: " + MACP + "/" + MACF +
                "\nPacked XBOX: " + XBOXP + "/" + XBOXF +
                "\nPacked All: " + (PS3P + PCP + MACP + XBOXP) + "/" + (PS3F + PCF + MACF + XBOXF) +
                "\nCopied (incl only copied/FTPed): " + cpy +
                "\nBroken (Not considered4repacking): " + (ConfigRepository.Instance()["dlcm_AdditionalManipul7"] == "Yes" ? "not relevant as not selected (option 7)" : brkn) +
                "\n\nSample of Meta info:\n" + metainfo.Replace("\n", "\n\t") +
                "\n\nFailed at packing (" + pack + ") :" + FailedP + "\n" + ListNP +
                ("\n\nListP (" + pack + ") :\n" + ListP);
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Summary of the Mass-Repack process", false, false, true, "", "", "", false);
            frm9.Show();
            UpdateLog(DateTime.Now, "Ending Packing " + summary + "\n songs.", true, null, "", "DLCManager", null, null);
        }

        public static void saveOptions(CheckedListBox chbx_Additional_Manipulations)
        {
            for (int j = 0; j < chbx_Additional_Manipulations.Items.Count; j++)
            {
                string orderno = chbx_Additional_Manipulations.Items[j].ToString();
                if (orderno.IndexOf("{") <= 0 || orderno.IndexOf("}") <= 0) continue;
                else orderno = orderno.Substring(orderno.IndexOf("{") + 1, orderno.IndexOf("}") - orderno.IndexOf("{") - 1);
                //if (orderno == "118")
                //    ;
                ConfigRepository.Instance()["dlcm_AdditionalManipul" + orderno] = chbx_Additional_Manipulations.GetItemChecked(j) ? "Yes" : "No";/*GetParam(j)*/
            }
        }

        //public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLiteConnection cnz)
        public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLite.SQLiteConnection cnc)
        {
            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRecP = GetNoRec(dup, cnb, cnc) > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            //dup.Tables.Count
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time, Bonus, Part FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRec = GetNoRec(dus, cnb, cnc);//dus.Tables[0].Rows.Count;
            var XMLFilePath = ""; var j = 0; var i = 0; var ArrangementType = "";
            for (i = 0; i <= noOfRec - 1; i++)
            {
                ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                if (ArrangementType == "Vocal") XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
            }

            Vocals newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(XMLFilePath);
            }
            catch (Exception ex) { var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }
            int note1 = newLyrics.Vocal[0].Note;

            var RouteMask = "";
            for (i = 0; i <= noOfRec - 1; i++)
            {
                RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                if (RouteMask == "Lead" || RouteMask == "Bass" || RouteMask == "Rhythm")
                    Add2LinesInVocals(XMLFilePath, 1, "0.001", note1); //per each arrangement add an empty line
            }

            var maxL = int.Parse(ConfigRepository.Instance()["dlcm_MaxLyricLenght_PS3"]);

            var strartt = "";
            var Part = "";
            var b = "";
            var p = "";

            Vocals xmlContent = null;
            if (XMLFilePath != "")
                try
                {
                    xmlContent = Vocals.LoadFromFile(XMLFilePath);
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        var changeaplied = false;
                        var insertedinlyric = 0;

                        ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                        RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                        strartt = dus.Tables[0].Rows[i].ItemArray[3].ToString();
                        if (strartt == "" || strartt == null) break;
                        Part = dus.Tables[0].Rows[i].ItemArray[5].ToString();
                        b = dus.Tables[0].Rows[i].ItemArray[4].ToString() == "True" ? "b" : "";
                        p = noOfRecP > 1 ? Part : "";
                        if (RouteMask == "Lead" || RouteMask == "Bass" || RouteMask == "Rhythm")
                        {
                            var shortstart = (ConfigRepository.Instance()["dlcm_AdditionalManipul90"].ToLower() == "Yes".ToLower() && strartt.IndexOf(".") > 0
                                ? strartt : strartt.Substring(0, strartt.IndexOf(".") + 2) + "s");
                            for (j = 1; j < xmlContent.Vocal.Length; j++)
                            {
                                if ((xmlContent.Vocal[j].Time + xmlContent.Vocal[j].Length) >= float.Parse(strartt, NumberStyles.Float, CultureInfo.CurrentCulture)
                                    && xmlContent.Vocal[j].Time <= float.Parse(strartt, NumberStyles.Float, CultureInfo.CurrentCulture)
                                    && changeaplied == false)
                                {
                                    xmlContent.Vocal[j - 1].Lyric = "[" + (RouteMask == "Lead" ? "L" + b + p + "(" : RouteMask == "Bass" ? "B" + b + p + "(" : RouteMask == "Rhythm" ? "R" + b + p + "(" : "-") + shortstart + ")]"
                                         + xmlContent.Vocal[j].Lyric.Trim().Replace("  ", " ");
                                    if (xmlContent.Vocal[j - 1].Lyric.Length > maxL)
                                        xmlContent.Vocal[j - 1].Lyric = xmlContent.Vocal[j - 1].Lyric.Substring(0, maxL);
                                    xmlContent.Vocal[j - 1].Length = xmlContent.Vocal[j].Length;
                                    xmlContent.Vocal[j - 1].Time = xmlContent.Vocal[j].Time;
                                    changeaplied = true;
                                    insertedinlyric++;
                                }
                                else if ((xmlContent.Vocal[j].Time + xmlContent.Vocal[j].Length) > float.Parse(strartt, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        && xmlContent.Vocal[j].Time > float.Parse(strartt, NumberStyles.Float, CultureInfo.CurrentCulture)
                                    && changeaplied == false)
                                {
                                    xmlContent.Vocal[j - 1].Lyric = ("[" + (RouteMask == "Lead" ? "L" + b + p + "(" : RouteMask == "Bass" ? "B" + b + p + "(" : RouteMask == "Rhythm" ? "R" + b + p + "(" : "-") + shortstart + ")]").Replace("  ", " ");
                                    if (xmlContent.Vocal[j - 1].Lyric.Length > maxL)
                                        xmlContent.Vocal[j - 1].Lyric = xmlContent.Vocal[j - 1].Lyric.Substring(0, maxL);
                                    xmlContent.Vocal[j - 1].Length = float.Parse("0.5", NumberStyles.Float, CultureInfo.CurrentCulture);
                                    xmlContent.Vocal[j - 1].Time = float.Parse(strartt, NumberStyles.Float, CultureInfo.CurrentCulture);
                                    if (xmlContent.Vocal[j - 1].Time + xmlContent.Vocal[j - 1].Length > xmlContent.Vocal[j].Time)
                                        xmlContent.Vocal[j - 1].Length = (float)(Math.Round(xmlContent.Vocal[j].Time - xmlContent.Vocal[j - 1].Time - float.Parse("0.001", NumberStyles.Float, CultureInfo.CurrentCulture), 3));
                                    changeaplied = true;
                                    j = 1000000;
                                }
                                else
                                {
                                    xmlContent.Vocal[j - 1].Lyric = xmlContent.Vocal[j].Lyric.Trim();
                                    if (xmlContent.Vocal[j - 1].Lyric.Length > maxL)
                                        xmlContent.Vocal[j - 1].Lyric = xmlContent.Vocal[j - 1].Lyric.Substring(0, maxL);
                                    xmlContent.Vocal[j - 1].Length = xmlContent.Vocal[j].Length;
                                    xmlContent.Vocal[j - 1].Time = xmlContent.Vocal[j].Time;
                                }
                            }
                            if (insertedinlyric > 0 && j == xmlContent.Vocal.Length)
                            {
                                xmlContent.Vocal[j - insertedinlyric].Lyric = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var tsst = "Error add lyric ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }

            using (var stream = File.Open(XMLFilePath, FileMode.Create))
                xmlContent.Serialize(stream);
            return XMLFilePath;
        }

        public static string AddStuffToLyrics(string SongID, string Comments, string Group, string Has_DD, string bassRemoved, string Bass_Has_DD, string Author
                    //, string Is_Acoustic, string Is_Live, string Live_Details, string Is_Multitrack, string Is_Original, OleDbConnection cnb, SQLiteConnection cnz, MainDBfields[] SongRecord, bool chbx_Beta, bool arrangoff)
                    , string Is_Acoustic, string Is_Live, string Live_Details, string Is_Multitrack, string Is_Original, OleDbConnection cnb, SQLite.SQLiteConnection cnc, MainDBfields[] SongRecord, bool chbx_Beta, bool arrangoff)
        {
            var ST = "";
            var tgst = "";
            var sdetails = (Comments == "" ? "" : "Comment: " + Comments);
            sdetails += " DynamicDificulty: " + Has_DD + (Has_DD == "Yes" && bassRemoved == "Yes" ? "(BassDDremoved)" : "");
            sdetails += (Author == "" ? "" : " by:" + Author) + (Is_Acoustic == "Yes" ? " Acoustic" : "");
            sdetails += (Is_Live == "Yes" ? " Live" : "") + (Live_Details == "" ? "" : "(" + Live_Details + ")");
            sdetails += (Is_Original == "No" ? " CustomSong" : " ORIGINAL ") + " ";
            var scomments = "";
            var maxL = int.Parse(ConfigRepository.Instance()["dlcm_MaxLyricLenght_PS3"]);
            var tsst = "Start TH ..."; var timestamp = UpdateLog(DateTime.Now, tsst, true, c("dlcm_TempPath"), "", "", null, null);

            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRecP = GetNoRec(dup, cnb, cnc) > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;
            //dup.Tables.Count
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRec = GetNoRec(dus, cnb, cnc);//dus.Tables[0].Rows.Count;
            float FirstLyric = 5000;
            float FirstVocal = 0; var i = 0;
            for (i = 0; i <= noOfRec - 1; i++)
            {
                var XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
                var Bonus = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                var Commentz = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[3].ToString();
                var RouteMask = dus.Tables[0].Rows[i].ItemArray[4].ToString();
                var StartTime = dus.Tables[0].Rows[i].ItemArray[5].ToString();
                var Part = dus.Tables[0].Rows[i].ItemArray[6].ToString();
                if (ArrangementType == "ShowLight") continue;
                if (StartTime == "") return XMLFilePath;
                string shortstart = ConfigRepository.Instance()["dlcm_AdditionalManipul90"].ToLower() == "Yes".ToLower() && StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;
                if (ArrangementType == "Vocal")
                {
                    ST = XMLFilePath;
                    FirstVocal = float.Parse(StartTime, NumberStyles.Float, CultureInfo.CurrentCulture) - 3 > 2 ? float.Parse(StartTime, NumberStyles.Float, CultureInfo.CurrentCulture) : 3;
                }
                //else
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul74"].ToLower() == "Yes".ToLower())
                    if (StartTime != "") if (float.Parse(StartTime, NumberStyles.Float, CultureInfo.CurrentCulture) < FirstLyric)
                            FirstLyric = float.Parse(StartTime, NumberStyles.Float, CultureInfo.CurrentCulture);
                sdetails += (Bonus.ToLower() == "true" ? " Bonus" : "").Trim().Replace("  ", " ");
                var b = ""; var bonus = "";
                if (Bonus.ToLower() == "true")
                {
                    b = "(B)";
                    bonus = "(Bonus)";
                }
                var p = "";
                if (noOfRecP > 1)
                {
                    p = Part;
                }

                scomments += (RouteMask == "Lead" && Commentz != "" ? " L_" + Commentz + b + p : "")
                    + (RouteMask == "Bass" && Commentz != "" ? " B_" + Commentz + b + p : "")
                    + (RouteMask == "Rhythm" && Commentz != "" ? " R_" + Commentz + b + p : "");
                var Instr = (RouteMask == "Lead" ? " Lead" + bonus + p : "")
                    + (RouteMask == "Bass" ? " Bass" + bonus + p : "")
                    + (RouteMask == "Rhythm" ? " Rhythm" + bonus + p : "")
                    + (ArrangementType == "Vocal" ? " Vocal" : "");
                scomments += (StartTime != "" ? " " + Instr + "(" + shortstart + ")" : "");
            }
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul74"].ToLower() != "Yes".ToLower()) FirstLyric = FirstVocal;

            //remove estra spaces
            scomments = scomments.Replace(". ", ".").Replace(": ", ":");
            sdetails = sdetails.Replace(". ", ".").Replace(": ", ":");
            scomments = scomments.Replace("  ", " ").TrimEnd().Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace(":", "");
            sdetails = sdetails.Replace("  ", " ").TrimEnd().Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace(":", "");
            if (c("dlcm_Lyric_Info") != "" && c("dlcm_Activ_LyricInfo") == "Yes")
            {
                sdetails = Manipulate_strings(ConfigRepository.Instance()["dlcm_Lyric_Info"], 0, false, false, bassRemoved == "Yes" ? true : false, SongRecord, "", "", chbx_Beta, false, false, cnc);
                scomments = "";
                sdetails = Manipulate_strings(ConfigRepository.Instance()["dlcm_Lyric_Info"], 0, false, false, bassRemoved == "Yes" ? true : false, SongRecord, "", "", chbx_Beta, false, false, cnc);
            }

            var spacetoadd = FirstLyric - 0.001-1;
            var rt = ST + ".old";
            try
            {
                if (!File.Exists(rt)) File.Copy(ST, rt);
                else File.Copy(rt, ST, true);
            }
            catch (Exception ex) { tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }

            var ft = float.Parse(Math.Ceiling(decimal.Parse((sdetails.Length / maxL).ToString())).ToString()) + 1;
            var fdt = float.Parse(Math.Ceiling(decimal.Parse((scomments.Length / maxL).ToString())).ToString()) + 1;
            spacetoadd = spacetoadd / (fdt);

            Vocals newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(ST);
            }
            catch (Exception ex) { tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }
            int note1 = newLyrics.Vocal[0].Note;
            Add2LinesInVocals(ST, int.Parse(Math.Ceiling(decimal.Parse(fdt.ToString())).ToString()), ((float)(Math.Floor((FirstLyric - 0.001)))).ToString(), note1);/* / 2*/
            newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(ST);
            }
            catch (Exception ex) { tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }

            var size = int.Parse(Math.Ceiling(decimal.Parse(((scomments.Length + int.Parse(fdt.ToString())) / fdt).ToString())).ToString());
            for (i = 0; i < (fdt); i++)
            {
                newLyrics.Vocal[i].Time = (float)(Math.Round(float.Parse("0.001", NumberStyles.Float, CultureInfo.CurrentCulture) + ((FirstLyric - 0.001) ) + i * spacetoadd / fdt, 3));//(float)(Math.Round((float)spacetoadd + newLyrics.Vocal[i].Length, 3));/ 2
                newLyrics.Vocal[i].Note = note1;
                newLyrics.Vocal[i].Length = (float)(Math.Round((spacetoadd / fdt  - float.Parse("0.002", NumberStyles.Float, CultureInfo.CurrentCulture)), 3));/*(double)FirstLyric -  / 2*/
                var txt = scomments.Length >= (size - 1) ? scomments.Substring(0, size - 1) : scomments.Substring(0, scomments.Length);
                newLyrics.Vocal[i].Lyric = "" + txt.Trim().TrimEnd() + "+";
                if (i + 1 < fdt) scomments = scomments.Substring(size - 1, scomments.Length - size + 1);
            }
            using (var stream = File.Open(ST, FileMode.Create))
                newLyrics.Serialize(stream);

            Add2LinesInVocals(ST, int.Parse(Math.Ceiling(decimal.Parse(ft.ToString())).ToString()), "0.001", note1);
            newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(ST);
            }
            catch (Exception ex) { tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }

            spacetoadd = (FirstLyric - 0.001)  / (ft);/*/ 2*/
            size = int.Parse(Math.Ceiling(decimal.Parse(((sdetails.Length + int.Parse(ft.ToString())) / ft).ToString())).ToString());
            for (i = 0; i < (ft); i++)
            {
                newLyrics.Vocal[i].Time = (float)(Math.Round(float.Parse("0.001", NumberStyles.Float, CultureInfo.CurrentCulture) + i * spacetoadd, 3));
                newLyrics.Vocal[i].Note = note1;
                newLyrics.Vocal[i].Length = (float)(Math.Round((spacetoadd - float.Parse("0.002", NumberStyles.Float, CultureInfo.CurrentCulture)), 3));
                var txt = sdetails.Length >= (size - 1) ? sdetails.Substring(0, size - 1) : sdetails.Substring(0, sdetails.Length);
                newLyrics.Vocal[i].Lyric = "" + txt.Trim().TrimEnd() + "+";
                if (i + 1 < ft) sdetails = sdetails.Substring(size - 1, sdetails.Length - size + 1);
            }

            //write new file
            using (var stream = File.Open(ST, FileMode.Create))
                newLyrics.Serialize(stream);
            tsst = "End add stuff to lyrics ..."; timestamp = UpdateLog(timestamp, tsst, true, c("dlcm_TempPath"), "", "", null, null);

            if (tgst.IndexOf("Error") >= 0)
                try
                {
                    if (File.Exists(rt)) File.Copy(rt, ST, true);
                    //else File.Copy(rt, ST, true);
                }
                catch (Exception ex) { tgst = "Error a b ackup restsore..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }
            return ST;
        }
        public static void FixBitrate(object sender, DoWorkEventArgs e)
        {
            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = c("dlcm_TempPath");

            string[] args = (e.Argument).ToString().Split(';');
            string cmd = args[0];
            string AudioPath = args[1];
            float bitrate = float.Parse(args[2], NumberStyles.Float, CultureInfo.CurrentCulture);
            float SampleRate = float.Parse(args[3], NumberStyles.Float, CultureInfo.CurrentCulture);
            string ID = args[4];
            string audioPreviewPath = args[5];
            string oggPath = args[6];
            string oggPreviewPath = args[7];
            string multithreadname = args[8];
            string windw = args[9];
            string err = "";

            //not needed as all done in utilitites
            //System.Data.OleDb.OleDbConnection cnb = null;
            //SQLite.SQLiteConnection cnc = null;
            //try
            //{
            //    cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]
            //   + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source="
            //   + ConfigRepository.Instance()["dlcm_DBFolder"]);
            //    do
            //        System.Threading.Thread.Sleep(1000);
            //    while (cnb.State.ToString() == "Connecting");
            //    //if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
            //    //try { cnb.Close(); cnc.Close(); } catch (Exception ex) {; }
            //    OpenDb();
            //}
            //catch (Exception exx)
            //{
            //    ShowConnectivityError(exx, "FAIL to use M$ ACCESS plugin:\n");/*, null*/
            //    var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
            //    tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
            //    ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
            //    //cnz = new SQLiteConnection("Data Source="+ ConfigRepository.Instance()["dlcm_DBFolder"]);
            //    //cnz.Open();
            //    cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
            //}

            var tsst = "Start FixBitRate TH ..." + AudioPath + "-" + bitrate + "-" + SampleRate; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);
            var d1 = WwiseInstalled("Convert Audio if bitrate > ConfigRepository");
            var audio_hash = "";

            //saving a copy as sometimes fails to convert and orig wem is lost
            if (File.Exists(AudioPath)) File.Copy(AudioPath, AudioPath + ".origi", true);
            else return;
            if (File.Exists(audioPreviewPath)) File.Copy(audioPreviewPath, audioPreviewPath + ".origi", true);
            else return;
            if (File.Exists(oggPath)) File.Copy(oggPath, oggPath + ".origi", true);
            else return;
            if (File.Exists(oggPreviewPath)) File.Copy(oggPreviewPath, oggPreviewPath + ".origi", true);
            else return;

            string g, gg; bool remote = false;
            g = AudioPath;
            gg = audioPreviewPath;
            if (g.IndexOf("\\\\") > -1 || gg.IndexOf("\\\\") > -1 || c("dlcm_AdditionalManipul6") == "Yes")
            {
                try
                {
                    var aa = AudioPath.Replace(".wem", "_fixed.ogg").Replace("_fixed_fixed.ogg", "_fixed.ogg").TrimStart(' ');
                    var bb = audioPreviewPath.Replace(".wem", "_fixed.ogg").Replace("_fixed_fixed.ogg", "_fixed.ogg").TrimStart(' ');
                    File.Copy(AudioPath, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(AudioPath), true);
                    File.Copy(aa, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(aa), true);
                    AudioPath = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(AudioPath);
                    File.Copy(audioPreviewPath, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(audioPreviewPath), true);
                    File.Copy(bb, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(bb), true);
                    audioPreviewPath = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(audioPreviewPath);
                    remote = true;
                }
                catch (Exception ee)
                {
                    err = "FAILED1 FixOggwDiffName"; timestamp = UpdateLog(timestamp, err + ee.Message + "----" + AudioPath.Replace(".wem", "_fixed.ogg") + "\n -" + AudioPath + ".ogg", true, "", "", "windw", null, null);
                    return;
                }
            }

            try
            {
                if (d1.Split(';')[0] == "1")
                {
                    if (File.Exists(AudioPath))
                    {
                        Downstream(AudioPath, bitrate, windw);
                    }
                    audio_hash = GetHash(AudioPath);
                    using (var vorbis = new NVorbis.VorbisReader(AudioPath.Replace(".wem", "_fixed.ogg")))
                    {
                        bitrate = vorbis.NominalBitrate;
                        SampleRate = vorbis.SampleRate;
                    }
                    cmd = "UPDATE Main SET ";
                    cmd += "Audio_Hash=\"" + audio_hash + "\"" + ", audioBitrate =\"" + bitrate + "\"";
                    cmd += ", audioSampleRate=\"" + SampleRate + "\", Has_Had_Audio_Changed=\"Yes\""; ;
                    cmd += " WHERE ID=" + ID;
                    DataSet dios = new DataSet(); dios = UpdateDB("Main", cmd + ";", cnb, cnc);

                    //Update Preview
                    if (audioPreviewPath != null && audioPreviewPath != "")
                    {
                        if (File.Exists(audioPreviewPath)) Downstream(audioPreviewPath, bitrate, windw);
                        audio_hash = GetHash(audioPreviewPath);
                        cmd = "UPDATE Main SET ";
                        cmd += "audioPreview_Hash=\"" + audio_hash;
                        cmd += "\" WHERE ID=" + ID;
                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb, cnc);
                        DeleteFile(AudioPath.Replace(".wem", "_preview.wem") + ".orig", false);
                        DeleteFile(AudioPath.Replace(".wem", "_preview_fixed.ogg") + ".orig", false);
                    }
                    //Delete any Wav file created..by....?ccc
                    foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(AudioPath), "*.wav", System.IO.SearchOption.AllDirectories))
                    {
                        DeleteFile(wav_name, false);
                    }
                }
                e.Result = "done";
                //cnb.Close();
            }
            catch (Exception ee)
            {
                timestamp = UpdateLog(timestamp, "FAILED1 FixOggwDiffName" + ee.Message + "----" + AudioPath.Replace(".wem", "_fixed.ogg") + "\n -" + AudioPath + ".ogg", true, "", "", "windw", null, null);
                Console.WriteLine(ee.Message);
                try
                {
                    if (!File.Exists(AudioPath)) File.Copy(AudioPath + ".origi", AudioPath, true);
                    else File.Copy(AudioPath + ".origi", AudioPath, true);
                }
                catch (Exception Ex) { err = "Error ..." + Ex; UpdateLog(DateTime.Now, err, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            try
            {
                if (remote)
                {
                    File.Copy(AudioPath, g, true);
                    File.Copy(audioPreviewPath, gg, true);

                    AudioPath = g;
                    audioPreviewPath = gg;
                }
            }
            catch (Exception Ex5)
            {
                err = "Error at REstore TH ..." + Ex5; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                return;
            }

            if (File.Exists(AudioPath)) { if (File.Exists(AudioPath + ".origi")) DeleteFile(AudioPath + ".origi", false); }
            else
                if (File.Exists(AudioPath + ".origi")) File.Move(AudioPath + ".origi", AudioPath);

            if (File.Exists(audioPreviewPath)) { if (File.Exists(audioPreviewPath + ".origi")) DeleteFile(audioPreviewPath + ".origi", false); }
            else
                if (File.Exists(audioPreviewPath + ".origi")) File.Move(audioPreviewPath + ".origi", audioPreviewPath);

            if (File.Exists(oggPath)) { if (File.Exists(oggPath + ".origi")) DeleteFile(oggPath + ".origi", false); }
            else
                 if (File.Exists(oggPath + ".origi")) if (File.Exists(oggPath + ".origi")) File.Move(oggPath + ".origi", oggPath);

            if (File.Exists(oggPreviewPath)) { if (File.Exists(oggPreviewPath + ".origi")) DeleteFile(oggPreviewPath + ".origi", false); }
            else
                if (File.Exists(oggPreviewPath + ".origi")) File.Move(oggPreviewPath + ".origi", oggPreviewPath);
            if (c("dlcm_AdditionalManipul6") == "Yes")
            {
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(AudioPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(audioPreviewPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(oggPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(oggPreviewPath), true);
            }

            if (err != "")
            {
                var cmdupd = "UPDATE Main Set FilesMissingIssues =\"Issues at audiofix" + err + "\" WHERE ID =" + ID;
                DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
            }
        }

        public static void FixPreview(object sender, DoWorkEventArgs e)
        {
            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = c("dlcm_TempPath");

            //OggPath, AppWD, OggPreviewPath, cmd, Folder_Name, ID, cnb
            string[] args = (e.Argument).ToString().Split(';');
            string OggPath = args[0];
            string AppWD = args[1];
            string OggPreviewPath = args[2]; var tr = OggPreviewPath;
            string cmd = args[3];
            string Folder_Name = args[4];
            string ID = args[5];
            string multithreadname = args[6];
            string windw = args[7];
            string AudioPath = args[8];
            string audioPreviewPath = args[9];
            var zt = ConfigRepository.Instance()["dlcm_DBFolder"];
            string err = "";

            System.Data.OleDb.OleDbConnection cnb = null;
            SQLite.SQLiteConnection cnc = null;
            try
            {
                cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
                //if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
                //try { cnb.Close(); cnc.Close(); } catch (Exception ex) {; }
                OpenDb();
            }
            catch (Exception exx)
            {
                ShowConnectivityError(exx, "FAIL to use M$ ACCESS plugin:\n");/*, null*/
                var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
                tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
                ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
                //cnz = new SQLiteConnection("Data Source="+ ConfigRepository.Instance()["dlcm_DBFolder"]);
                //cnz.Open();
                cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
            }

            var tsst = "Start FixPreview TH ..."; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "oggcut.exe"),
                WorkingDirectory = AppWD
            };
            var t = OggPath.Replace(".wem", "_fixed.ogg");
            var tt = t.Replace("_fixed.ogg", "_preview_fixed.ogg");

            //saving a copy as sometimes fails to convert and orig wem is lost
            if (File.Exists(AudioPath)) File.Copy(AudioPath, AudioPath + ".orig", true);
            else return;
            if (File.Exists(audioPreviewPath)) File.Copy(audioPreviewPath, audioPreviewPath + ".orig", true);
            //else return;
            if (File.Exists(OggPath)) File.Copy(OggPath, OggPath + ".orig", true);
            else return;
            if (File.Exists(OggPreviewPath)) File.Copy(OggPreviewPath, OggPreviewPath + ".orig", true);
            //else return;

            string g, gg; bool remote = false;
            g = t;
            gg = tt;
            try
            {
                if (g.IndexOf("\\\\") > -1 || gg.IndexOf("\\\\") > -1 || c("dlcm_AdditionalManipul6") == "Yes")
                {
                    File.Copy(t, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(t), true);
                    t = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(t);
                    if (File.Exists(tt)) File.Copy(tt, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(tt), true);
                    tt = c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(tt);
                    remote = true;
                }
            }
            catch (Exception Ex5)
            {
                err = "Error at REstore TH ..." + Ex5; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                return;
            }

            try
            {
                try
                {
                    var times = ConfigRepository.Instance()["dlcm_PreviewStart"]; //00:30
                    string[] timepieces = times.Split(':');
                    var audioPreview_hash = "";
                    var PreviewLenght = "";
                    var songlenght = "";
                    using (var vorbis = new NVorbis.VorbisReader(t))
                    {
                        //bitrate = vorbis.NominalBitrate;
                        //if ((vorbis.TotalTime.ToString().Split(':'))[0] == "00" && (vorbis.TotalTime.ToString().Split(':'))[1] == "00")
                        //    songlenght = (vorbis.TotalTime.ToString().Split(':'))[2];
                        //else songlenght = vorbis.TotalTime.ToString();
                        songlenght = ((vorbis.TotalTime.ToString().Split(':'))[0]).ToInt32() * 3600
                            + ((vorbis.TotalTime.ToString().Split(':'))[1]).ToInt32() * 60
                            + (vorbis.TotalTime.ToString().Split(':'))[2];
                    }

                    TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                    if ((float.Parse(songlenght) * 1000) > (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)))
                        startInfo.Arguments = string.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                            t,
                                                            tt,
                                                            r.TotalMilliseconds,
                                                            (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
                    else
                        startInfo.Arguments = string.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                            t,
                                                            tt,
                                                            0,
                                                            r.TotalMilliseconds);
                    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                    if (File.Exists(t))
                        using (var DDC = new Process())
                        {
                            tsst = "Cut Ogg for preview ..." + OggPath; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, "", null, null);
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            if (DDC.ExitCode == 0 && File.Exists(tt))
                            {
                                var wwisePath = "";
                                if (!string.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                                    wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                                else
                                    wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                                if (wwisePath == "")
                                {
                                    ErrorWindow frm1 = new ErrorWindow("In order to use the FixAudioIssues-Preview, please Install Wwise Launcher" +
                                        " then Wwise v" + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required" +
                                        " for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing " +
                                        "Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation",
                                        true, true, true, "", "", "", false);
                                    frm1.ShowDialog();
                                    if (frm1.StopImport) return;
                                }
                                if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath, false);
                                tsst = "Convert to wem preview ..."; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                                var i = 0;
                                do
                                {
                                    UtilitiesFunctions.Converters(tt, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
                                    i++;
                                    if (!File.Exists(tt.Replace(".ogg", ".wem")))
                                    {
                                        //fix as sometimes the template folder gets poluted and breaks eveything
                                        var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                        var templateDir = Path.Combine(appRootDir, "Template");
                                        var backup_dir = Directory.Exists(AppWD + "\\Template") ? (AppWD + "\\Template") : (AppWD.Replace("\\Debug\\", "\\Release\\") + "\\Template");
                                        DeleteDirectory(templateDir, false);
                                        CopyFolder(backup_dir, templateDir);
                                    }
                                }
                                while (!File.Exists(tt.Replace(".ogg", ".wem")) && i < 10);
                                if (File.Exists(tt.Replace(".ogg", ".wav"))) DeleteFile(tt.Replace(".ogg", ".wav"), false);
                                //if (File.Exists(tt.Replace(".ogg", "_preview.wem"))) DeleteFile(tt.Replace(".ogg", "_preview.wem"));
                                OggPreviewPath = tt.Replace(".ogg", ".wem");
                                if (!File.Exists(tt.Replace(".ogg", ".wem")))
                                {
                                    err = "error @ogg cut..."; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                                    //if (!File.Exists(gg)) File.Move(gg + ".orig", gg);
                                }
                            }
                            else
                            {
                                err = "error @ogg cut..."; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                                //if (File.Exists(gg)) DeleteFile(gg);
                                //if (File.Exists(gg + ".orig")) File.Move(gg + ".orig", gg);
                            }
                        }

                    var previewN = OggPreviewPath.Replace(".wem", ".ogg");
                    PreviewLenght = "";
                    audioPreview_hash = "";
                    if (File.Exists(previewN) && File.Exists(OggPreviewPath))
                    {
                        using (var vorbis = new NVorbis.VorbisReader(previewN))
                        {
                            //bitrate = vorbis.NominalBitrate;
                            if ((vorbis.TotalTime.ToString().Split(':'))[0] == "00" && (vorbis.TotalTime.ToString().Split(':'))[1] == "00")
                                PreviewLenght = (vorbis.TotalTime.ToString().Split(':'))[2];
                            else PreviewLenght = vorbis.TotalTime.ToString();
                            audioPreview_hash = GetHash(OggPreviewPath);
                            //SampleRate = vorbis.SampleRate;
                        }
                        if (remote)
                        {

                            //var rrr = true;
                            try
                            {
                                File.Copy(tt, gg, true);
                                File.Copy(tt.Replace(".ogg", ".wem"), gg.Replace(".ogg", ".wem"), true);
                                //rrr = false;
                            }
                            catch (Exception Ex5)
                            {
                                err = "Error at REstore TH ..." + Ex5; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                            }
                            t = g;
                            tt = gg;
                        }

                        audioPreviewPath = tt.Replace(".ogg", ".wem");
                        OggPreviewPath = tt; tr = OggPreviewPath;
                        cmd = "UPDATE Main SET ";
                        cmd += " audioPreviewPath=\"" + audioPreviewPath + "\" ,audioPreview_Hash =\"" + audioPreview_hash + "\"" + ", OggPreviewPath=\"" + OggPreviewPath + "\", Has_Preview=\"Yes\"";// previewN + "\"";
                        cmd += ", PreviewLenght=\"" + PreviewLenght + "\", Has_Had_Audio_Changed=\"Yes\"";
                        cmd += " WHERE ID=" + ID;
                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb, cnc);
                    }
                    //Delete any Wav file created..by....?ccc
                    foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(OggPath), "*.wav", System.IO.SearchOption.AllDirectories))
                    {
                        DeleteFile(wav_name, false);
                    }
                }
                catch (Exception Ex)
                {
                    err = "Error at FixPreview TH ..." + Ex; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                    // try //{ File.Move(g + ".orig", g); } catch (Exception Ex5) { tsst = "Error at REstore TH ..." + Ex5; timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null); }
                }
            }
            catch (Exception Ex)
            {
                err = "Error at restoring old FixPreview ..." + Ex; timestamp = startT; UpdateLog(timestamp, err, false, tmpPath, multithreadname, windw, null, null);
                //  try { File.Move(g + ".orig", g); } catch (Exception Ex56) { tsst = "Error at REstore TH ..." + Ex56; timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null); }
            }

            //cnb.Close();

            if (File.Exists(AudioPath)) { if (File.Exists(AudioPath + ".orig")) DeleteFile(AudioPath + ".orig", false); }
            else
                File.Move(AudioPath + ".orig", AudioPath);

            if (File.Exists(audioPreviewPath)) { if (File.Exists(audioPreviewPath + ".orig")) DeleteFile(audioPreviewPath + ".orig", false); }
            else
                File.Move(audioPreviewPath + ".orig", audioPreviewPath);

            if (File.Exists(OggPath)) { if (File.Exists(OggPath + ".orig")) DeleteFile(OggPath + ".orig", false); }
            else
                File.Move(OggPath + ".orig", OggPath);

            if (File.Exists(tr)) { if (File.Exists(tr + ".orig")) DeleteFile(tr + ".orig", false); }
            else
                File.Move(tr + ".orig", tr);

            if (c("dlcm_AdditionalManipul6") == "Yes")
            {
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(AudioPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(audioPreviewPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(OggPath), true);
                DeleteFile(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(tr), true);
            }

            if (err != "")
            {
                var cmdupd = "UPDATE Main Set FilesMissingIssues =\"Issues at audiofix" + err + "\" WHERE ID =" + ID;
                DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
            }
            e.Result = "done";
        }

        public static string GetTranslation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb
                //, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, string Artist, string Album, string Year, SQLiteConnection cnz)
                , System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, string Artist, string Album, string Year, SQLite.SQLiteConnection cnc)
        // For 1 song
        // Select all Corrected Arstist OR Album OR Year
        {
            var tst = "Running Translation_And_Correction..."; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var cmd1 = "SELECT * FROM Standardization WHERE (Artist = \"" + Artist + "\" OR Artist_Correction = \"" + Artist + "\") and (Album =\""
                + Album + "\" OR Album_Correction =\"" + Album + "\") AND (Artist_Correction<>\"\" OR Album_Correction<>\"\") order by id;";
            var artist_c = "";
            var album_c = "";
            var albumyear_c = "";
            var DB_Path = dbp;
            pB_ReadDLCs.Value = 0;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd1, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc);
            var norec = GetNoRec(dus, cnb, cnc);//dus.Tables.Count > 0 ? dus.Tables[0].Rows.Count : 0;
            pB_ReadDLCs.Maximum = norec;
            var tsst = "Applying " + norec + "corrections"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (norec > 0)
                foreach (DataRow dataRow in dus.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[2].ToString() != "" ? dataRow.ItemArray[2].ToString() : artist_c;
                    album_c = dataRow.ItemArray[4].ToString() != "" ? dataRow.ItemArray[4].ToString() : album_c;
                    albumyear_c = dataRow.ItemArray[9].ToString() != "" ? dataRow.ItemArray[9].ToString() : albumyear_c;
                }
            cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + "\"," : "")
        + (album_c != "" ? " Album = \"" + album_c + "\"," : "")
        + (albumyear_c != "" ? " Album_Year = \"" + albumyear_c + "\"," : "");

            return (artist_c + ";" + album_c + ";" + albumyear_c);
        }

        public static string OneTranslation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb
            , System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, string Artist, string Album, string Year, string ArtPath, string artist_c, string album_c, SQLite.SQLiteConnection cnc)
        // For 1 song
        // Select all Corrected Arstist OR Album OR Year
        {
            var tst = "Running Translation_And_Correction..."; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + "\"," : "")
                + (album_c != "" ? " Album = \"" + album_c + "\"," : "") + (ArtPath != "" ? " AlbumArtPath = \"" + ArtPath + "\"," : "")
                + (Year != "" ? " Album_Year = \"" + Year + "\"," : "");
            cmd1 += " Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + Artist + "\" AND Album=\"" + Album + "\"";
            var dus = UpdateDB("Main", cmd1, cnb, cnc);

            cmd1 = "UPDATE Standardization SET "
            + (Year != "" ? " Year_Correction = \"" + Year + "\"" : "Year_Correction =Year_Correction");
            cmd1 += " WHERE (Artist=\"" + Artist + "\" or Artist_Correction=\"" + Artist + "\") AND (Album=\"" + Album + "\" or Album_Correction=\"" + Album + "\")";
            var dis = UpdateDB("Standardization", cmd1, cnb, cnc);

            cmd1 = "UPDATE Standardization SET "
                + (artist_c != "" ? "Artist_Correction = \"" + artist_c + "\"" : "")
                + (album_c != "" ? (artist_c != "" ? "," : "") + " Album_Correction = \"" + album_c + "\"" : "")
                + (ArtPath != "" ? (artist_c != "" || album_c != "" ? "," : "") + " AlbumArtPath_Correction = \"" + ArtPath + "\"" : "")
                        + (Year != "" ? (ArtPath != "" || artist_c != "" || album_c != "" ? "," : "") + " Year_Correction = \"" + Year + "\"" : (ArtPath != "" || artist_c != "" || album_c != "" ? "," : "") + "Year_Correction =Year_Correction");
            cmd1 += " WHERE (Artist=\"" + Artist + "\") AND (Album=\"" + Album + "\")";
            var dxxs = UpdateDB("Standardization", cmd1, cnb, cnc);

            return (Artist + ";" + Album + ";" + Year);
        }

        //public static void Translation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLiteConnection cnz)
        public static void Translation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)
        // Select only Corrected Arstist OR Album OR Cover combination
        // For Each Corrected Record build up an Update sentence
        // Insert any translation if not already existing
        {
            var tst = "Running Translation_And_Correction..."; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var stage = 0; var maxtranslationprocesse = 14;
            //Bug-Fix: Make sure no Album & Artist are blank
            var cmd1 = " WHERE Artist is null OR Artist=\"\""; timestamp = UpdateLog(timestamp, "0/" + maxtranslationprocesse + "Standardization bug fix " + cmd1 + ": " + GetNoRecords("SELECT ID FROM Standardization" + cmd1, cnb, cnc).ToString(), true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var gdus = UpdateDB("Standardization", "UPDATE Standardization SET Artist = \"xxx\" " + cmd1, cnb, cnc);
            var cmd2 = " WHERE Album is null OR Album=\"\""; timestamp = UpdateLog(timestamp, "0/" + maxtranslationprocesse + "Standardization bug fix " + cmd2 + ": " + GetNoRecords("SELECT ID FROM Standardization" + cmd2, cnb, cnc).ToString(), true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var gdud = UpdateDB("Standardization", "UPDATE Standardization SET Album = \"xxx\" " + cmd2, cnb, cnc);

            //Apply any correction/standardization to Main //" + norec + "
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; var tsst = stage + "/" + maxtranslationprocesse + " Apply Artist and Album corrections, to MainDB"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var mat = ManuallyApplyTransalations(cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cnc);

            //insert any translation if not already existing
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " insert any translation if not already existing"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var mcr = Multiplycorrections(cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cnc);

            //Removing Standardization duplicates
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Cleans out duplicates"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var mrd = ManuallyRemoveDuplicates(cnb, cnc);

            //Apply Artist Short Name
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Apply Artist Short Name"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var aass = ApplyArtistShort(cnb, cnc);

            //Apply Album Short Name    
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Apply Album Short Name"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var aas = ApplyAlbumShort(cnb, cnc);

            //Multiply Spotify
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Multiply Spotify"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var ms = MultiplySpotify(cnb, cnc);

            //Multiply Cover
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Multiply Cover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var adc = ApplyDefaultCover(cnb, cnc);

            //Apply DefaultCover
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Apply DefaultCover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var mc = MakeCover(cnb, cnc);

            //Apply Artist Auto Group
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Apply Artist Auto Group"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var aaag = ApplyArtistAutoGroup(cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cnc);

            //Apply YearCorrection
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Multiply 1st and apply Year Correction"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var maay = MultiplyAndApplyYear(cnb, cnc);

            //Apply Groups inclusion & exclusion
            var gi = 0; var go = 0; pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Apply group Inclusions and Exclusions (as per config params)"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            if (c("dlcm_GroupIn_GroupIn1") != "") gi += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupIn1"), true).ToInt32();
            if (c("dlcm_GroupIn_GroupIn2") != "") gi += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupIn2"), true).ToInt32();
            if (c("dlcm_GroupIn_GroupIn3") != "") gi += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupIn3"), true).ToInt32();
            if (c("dlcm_GroupIn_GroupOut1") != "") go += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupOut1"), false).ToInt32();
            if (c("dlcm_GroupIn_GroupOut2") != "") go += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupOut2"), false).ToInt32();
            if (c("dlcm_GroupIn_GroupOut3") != "") go += ApplyGroupRules(cnb, cnc, c("dlcm_GroupIn_GroupOut3"), false).ToInt32();

            //Apply alternate Corrections
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Clear Alternates and Multiply Groups per Alternates"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var mg = CleanAlternates_and_MultiplyGroups(cnb, cnc, pB_ReadDLCs);

            //Apply Spotify
            // tsst = "11/13 Multiply 1st and apply Spotify data"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            //Standardization.MultiplyAndApplySpotify(cnb);

            //Multiply Meta per albums
            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Multiply Meta per Album"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var ma = MultiplyMetaAlbum(cnb, cnc, pB_ReadDLCs);

            pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " Finished applying Standardization"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            System.Windows.Forms.MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied:" +
                "\n\n1. MainDB updated with Artist, Album, Year standardizations e.g.  (" + mat + ")" +
                "\n\n2. Standarization multiplied to same Artist & Album (" + mcr + ")" +
                "\n\n3. Standarization records cleansed of duplicates (same Album  +Artist) (" + mrd + ")" +
                "\n\n4. Apply and Multiply Artist shortname (e.g. 'Rage Against the Machine' = 'ratm') (" + aass + ")" +
                "\n\n5. Apply and Multiply Album shortname (e.g. 'Blood Sugar Sex Magik' = 'BSSM') (" + aas + ")" +
                "\n\n6. Multiply Spotify info in Main and Standardization DBs (" + ms + ")" +
                "\n\n7. If an Album Art is marked as Default then set it as such in Standardizartion DB (" + adc + ")" +
                "\n\n8. If an Album Art is marked as Default then set it as such in Main DB (" + mc + ")" +
                "\n\n9. If an Artist is Marked to be added by default to a Group (e.g. 'Danko Jones' -> 'Playable Powercords' group) (" + aaag + ")" +
                "\n\n10. Enforced any Standardization has an Artist or an Album (" + maay + ")" +
                "\n\n11. Multiplied and Apply Standardization Year (" + maay + ")" +
                "\n\n12. Apply any (5) group (3x2) dependency (\n\te.g.1 If in group 'Monthly songs of Interest' then also in 'Definetely To Try' group" +
                "\n\te.g.2 If in group 'Playable songs' then NOT also in 'Definetely To Try' group) (" + gi + "-" + go + "(inserted/deleted))" +
                "\n\n13. Multiply groups for alternates(" + mg + ")" +
                "\n\n14. Multiply meta for albums(" + ma + ")" +
                "", "Sumamry of Translations done, based on Standardisation Table and Rules", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static string ManuallyApplyTransalations(OleDbConnection cnb, ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)
        {
            //Multiply
            //tst = "Apply Already existing translations"; UpdateLog(DateTime.Now, tst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //Standardization.ApplyExistingTranlations(cnb);
            var cmd1 = "SELECT * FROM Standardization WHERE Artist_Correction <> \"\" or Album_Correction <> \"\"  order by id;";//DISTINCT (Artist_Correction + Album_Correction)
                                                                                                                                 //((distinct(ID, Artist, Artist_Correction, Album, Album_Correction, Comments, Artist_Short, Album_Short, Year_Correction)))
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var albumyear_c = "";
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            //int aa = 0;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd1, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc);
            var norec = GetNoRec(dus, cnb, cnc);//dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
            var cnt = 0;
            if (norec > 0)
                foreach (DataRow dataRow in dus.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[2].ToString();
                    album_c = dataRow.ItemArray[4].ToString();
                    artpath_c = dataRow.ItemArray[5].ToString();
                    albumyear_c = dataRow.ItemArray[9].ToString();

                    //tst = cnt + "\"" + norec + "Running Translation_And_Correction..." + artist_c + " " + album_c + " " + albumyear_c; timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var badartistalbum = "Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"" +
                        " AND (" + (artist_c != "" ? "Artist <> \"" + artist_c + "\"" : "") + (artist_c != "" ? " OR Artist_Sort <> \"" + artist_c + "\"" : "") + (album_c != "" ? "OR Album <> \"" + album_c + "\"" : "")
                        + (artpath_c != "" ? " OR AlbumArtPath <> \"" + artpath_c + "\"" : "") + (albumyear_c != "" ? " OR Album_Year <> \"" + albumyear_c + "\"" : "");
                    badartistalbum += ")"; badartistalbum = badartistalbum.Replace("(OR", "(");
                    var norecs = GetNoRecords("SELECT ID from Main WHERE " + badartistalbum, cnb, cnc);
                    cnt += norecs;
                    if (norecs > 0)
                    {
                        cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + "\"," : "")
                            + (album_c != "" ? " Album = \"" + album_c + "\"," : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"," : "") + (albumyear_c != "" ? " Album_Year = \"" + albumyear_c + "\"," : "");
                        cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE " + badartistalbum;
                        cmd1 = cmd1.Replace("SET ,", "SET ").Replace(", ,", ", ").Replace(",,", ", ") + ";";
                        dus = UpdateDB("Main", cmd1, cnb, cnc);
                        UpdateLog(DateTime.Now, "Updated (" + norecs + "times): " + cmd1, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    //else UpdateLog(DateTime.Now, "No TRansalation to update" + cmd1, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            else UpdateLog(DateTime.Now, "No standardization/Correction/Transalation to update" + cmd1, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            UpdateLog(DateTime.Now, "No TRansalation to update" + cmd1, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return norec.ToString() + "/" + cnt.ToString() + " (total/updated)";
        }

        public static string Multiplycorrections(OleDbConnection cnb, ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)
        {
            //var insertcmdd = "Artist, Album";
            //var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
            //    ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN" +
            //    " FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1)" +
            //    " AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND" +
            //    " (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND" +
            //    " ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0)" +
            //    " AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            //DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", insertvalues, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            //InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb, 0);
            var cmd1 = "SELECT distinct Artist, Album FROM Main ORDER BY Artist";
            DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", cmd1, "", cnb, cnc);
            var noOfRec = GetNoRec(dgs, cnb, cnc);//dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
            var cmd2 = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
                ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S" +
                " ORDER BY Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])";
            DataSet dg = new DataSet(); dg = SelectFromDB("Standardization", cmd2, "", cnb, cnc);
            var noOfRecz = GetNoRec(dg, cnb, cnc);//dg.Tables.Count == 0 ? 0 : dg.Tables[0].Rows.Count;
            var found = false/*; var album = ""; var artist = ""*/; var tz = 0; var tsst = "";
            if (noOfRec > 0 && noOfRecz > 0)
                for (var l = 0; l < noOfRec; l++)
                {
                    found = false;
                    for (var v = 0; v < noOfRecz; v++)
                    {
                        //var artfound = false;
                        if (dgs.Tables[0].Rows[l].ItemArray[0].ToString().ToLower() == dg.Tables[0].Rows[v].ItemArray[0].ToString().ToLower())
                        {
                            //artfound = true;
                            if (dgs.Tables[0].Rows[l].ItemArray[1].ToString() == dg.Tables[0].Rows[v].ItemArray[1].ToString())
                            {
                                found = true;
                                break;/*album = dgs.Tables[0].Rows[l].ItemArray[1].ToString(); artist = dgs.Tables[0].Rows[l].ItemArray[0].ToString(); */
                            }
                        }
                        //else if (artfound) break;
                    }
                    if (!found)
                    {
                        tz++;
                        tsst = "Insert new values based on existing Artist, Album, \"" + dgs.Tables[0].Rows[l].ItemArray[0].ToString() + "\",\"" + dgs.Tables[0].Rows[l].ItemArray[1].ToString() + "\""; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        InsertIntoDBwValues("Standardization", "Artist, Album", "\"" + dgs.Tables[0].Rows[l].ItemArray[0].ToString() + "\",\"" + dgs.Tables[0].Rows[l].ItemArray[1].ToString() + "\"", cnb, 0, cnc);
                    }
                }
            else
                UpdateLog(DateTime.Now, "No records to multiply in Standardization(" + noOfRec + ") found in either: " + cmd1 + "\n or \n" + cmd2, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            tsst = "Inserted " + tz + " entries in Standardization(" + noOfRecz + ") based on manual check if existing Artist, Album in Main(" + noOfRec + "), doesnt in transanlastion aforementioned table"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            return tz.ToString() + " (inserted in Standardization)";
        }

        public static string ManuallyRemoveDuplicates(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now; var tsst = "";
            DataSet dr = new DataSet(); dr = SelectFromDB("Standardization", "SELECT Artist, Album, Artist_Correction, Album_Correction, ID FROM Standardization ORDER BY Artist", "", cnb, cnc);
            var noOfRec = GetNoRec(dr, cnb, cnc);//dr.Tables.Count == 0 ? 0 : dr.Tables[0].Rows.Count;
            var IDs = ""; var tz = 0;
            if (noOfRec > 0)
                for (var l = 0; l < noOfRec; l++)
                    for (var v = l + 1; v < noOfRec; v++)
                        if (dr.Tables[0].Rows[l].ItemArray[0].ToString() == dr.Tables[0].Rows[v].ItemArray[0].ToString())/*.ToLower() */
                        {
                            if (dr.Tables[0].Rows[l].ItemArray[1].ToString() == dr.Tables[0].Rows[v].ItemArray[1].ToString()
                                && dr.Tables[0].Rows[l].ItemArray[2].ToString() == dr.Tables[0].Rows[v].ItemArray[2].ToString()
                                && dr.Tables[0].Rows[l].ItemArray[3].ToString() == dr.Tables[0].Rows[v].ItemArray[3].ToString())
                            {
                                tz++;
                                IDs += dr.Tables[0].Rows[v].ItemArray[4].ToString() + ", ";
                            }
                        }
                        else break;

            if (IDs.Length > 0)
            {
                DeleteFromDB("Standardization", "DELETE * from Standardization WHERE ID IN (" + (IDs.Substring(0, IDs.Length - 2)) + ")", cnb, cnc); //Cleans out duplicates
                tsst = "Manually checked: Removed Artist&Album&corespondent corrections Duplicates" + tz + "/" + noOfRec + " correction(" + IDs + ")"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);
            }
            else UpdateLog(timestamp, "No Manually Removed Artist&Album&corespondent corrections Duplicates /" + noOfRec + " corrections", false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);
            return tz.ToString() + " (removed from Standardization)";
        }

        public static string MultiplySpotify(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
         //}

            var tsst = ""; var timestamp = DateTime.Now; var i = 0;
            var cmd = "SELECT distinct iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album)" +
                ", SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Year_Correction FROM Standardization" +
                " WHERE (SpotifyArtistID <> \"\")" +
                " GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist)" +
                ", iif(Album_Correction<>\"\", Album_Correction, Album), SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL,SpotifyAlbumPath, Year_Correction;";
            //+
            //" ORDER BY ID;";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0; 
            var tz = 0;
            if (norecs > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    i++;
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var album_c = dataRow.ItemArray[1].ToString();
                    var SpotifyArtistID = dataRow.ItemArray[2].ToString();
                    var SpotifyAlbumID = dataRow.ItemArray[3].ToString();
                    var SpotifyAlbumURL = dataRow.ItemArray[4].ToString();
                    var SpotifyAlbumPath = dataRow.ItemArray[5].ToString();
                    var SpotifyYear = dataRow.ItemArray[6].ToString();
                    //var cmd1 = "UPDATE Main SET Spotify_Artist_ID = \"" + SpotifyArtistID + "\",Spotify_Album_ID = \"" + SpotifyAlbumID + "\",Spotify_Album_URL = \"" + SpotifyAlbumURL + "\",Spotify_Album_Path = \"" + SpotifyAlbumPath + "\", WHERE Album=\"" + SpotifyAlbumID + "\"";
                    //DataSet dus = UpdateDB("Main", cmd1 + ";");
                    //dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", ""); try { norec = dus.Tables[0].Rows.Count; } catch { }
                    var cmd1 = "WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\") AND "
                        + " SpotifyArtistID <> \"" + SpotifyArtistID + "\" ";/*+ "\",Year_Correction = \"" + SpotifyYear +*/
                    var norec = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc); tz += norec;
                    if (norec > 0)
                    {
                        var dus = UpdateDB("Standardization", "UPDATE Standardization SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \""
                        + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\" " + cmd1, cnb, cnc);
                        tsst = "Multiplying spotify :" + i + "/" + GetNoRec(dfz, cnb, cnc) + " for " + cmd1 + ", " + norec + " times."; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    }/*dfz.Tables[0].Rows.Count*/
                }
            else UpdateLog(timestamp, "no spotify to multiply for " + cmd, false, c("dlcm_TempPath"), "", "", null, null);
            tsst = "Multiplying spotify :" + i + "/" + tz + " times."; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            return norecs + "-" + tz + " (totals/multiplied)";
            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static string MakeCover(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            //var cmd1 = "";
            ////var DB_Path = DB_Path + "\\AccessDB.accdb";
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBs_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"";
            //        OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
            //        das.Fill(dus, "Main");
            //        das.Dispose();
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}

            var NoRec = 0;
            var timestamp = DateTime.Now; var cmd = "SELECT Artist, Album, AlbumArt_Correction FROM Standardization WHERE (AlbumArt_Correction <> \"\") GROUP BY Artist,Album,AlbumArt_Correction;";
            //DataSet dssx = new DataSet();var tsst = ""; 
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBs_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";", cn);
            //    da.Fill(dssx, "Standardization");
            //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //da.Fill(ds, "PositionType");
            //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //da.Fill(ds, "Badge");
            //}

            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var tz = 0;
            //NoRec = dgt.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = NoRec;
            if (GetNoRec(dgt, cnb, cnc) > 0)/*dgt.Tables.Count*/
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var album_c = dataRow.ItemArray[1].ToString();
                    var artpath_c = dataRow.ItemArray[2].ToString();
                    var cmd1 = "";
                    cmd1 = " WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\" And AlbumArtPath <> \"" + artpath_c + "\"";
                    var norec = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc); tz += norec;
                    if (norec > 0)
                    {
                        dgt = UpdateDB("Main", "UPDATE Main SET AlbumArtPath = \"" + artpath_c + "\"" + cmd1 + "; ", cnb, cnc);
                        UpdateLog(timestamp, "Cover to update :" + "Artist =\"" + artist_c + "\" and Album=\"" + album_c + "\" And AlbumArtPath <> \"" + artpath_c + "\", " + norec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                    }
                    //if (artpath_c != "" && album_c != "" && artpath_c != "") dgt = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"", "", cnb, cnc);
                    //try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                    //cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + artpath_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\");";
                    //dgt = UpdateDB("Standardization", cmd1 + ";");
                }
            else UpdateLog(timestamp, "No Cover to update :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);
            //DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"");
            //DataSet dssx = new DataSet(); dxr = SelectFromDB("Main", "SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";");
            UpdateLog(timestamp, NoRec.ToString() + " ( defaulted to standardized Album+Artist cover, cover)", false, c("dlcm_TempPath"), "", "", null, null);
            return NoRec.ToString() + " ( defaulted to standardized Album+Artist cover, cover)";

            // lbl_NoRec = noOfRec.ToString() + " records.";
            //MessageBox.Show("Cover has been defaulted as Cover to " + NoRec.ToString() + " songs");
        }

        //       from main
        //get all same year same artist
        //get all same artist, album
        //get all same artist similar album
        //get all alternate of any of these
        public static string GetSameAlbum(string artist, string year, string album, string ID, bool withlog)
        {
            var timestamp = DateTime.Now;  /*var album_c = ""; var norec = 0;var tz = 0; var tu = 0; var artist_c = ""; var tsst = "";*/ var ids = "";

            //not checking this due to amount of observer false positives
            //var cmd = "SELECT ID FROM Main WHERE Artist=\"" + artist + "\" AND Album_Year=\"" + year + "\"";
            //DataSet dfz = new DataSet(); dfz = SelectFromDB("Main", cmd, "", cnb, cnc);
            //int norecs = dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            //if (norecs > 0) foreach (DataRow dataRow in dfz.Tables[0].Rows) ids += dataRow.ItemArray[0].ToString() + ", ";
            //if (withlog) UpdateLog(timestamp, "get all same year same artist(" + norecs + "):" + ids, false, c("dlcm_TempPath"), "", "MainDB", null, null);

            var cmd = "SELECT ID FROM Main WHERE Artist=\"" + artist + "\" AND Album=\"" + album + "\"";
            DataSet dgz = new DataSet(); dgz = SelectFromDB("Main", cmd, "", cnb, cnc);
            int norecs = GetNoRec(dgz, cnb, cnc);//dgz.Tables.Count > 0 ? dgz.Tables[0].Rows.Count : 0;
            if (norecs > 0) foreach (DataRow dataRow in dgz.Tables[0].Rows) ids += dataRow.ItemArray[0].ToString() + ", ";
            if (withlog) UpdateLog(timestamp, "get all same artist, album(" + norecs + "):" + ids, false, c("dlcm_TempPath"), "", "MainDB", null, null);

            cmd = "SELECT ID FROM Main WHERE Artist=\"" + artist + "\" AND Album like \"%" + album + "%\"";
            DataSet dhz = new DataSet(); dhz = SelectFromDB("Main", cmd, "", cnb, cnc);
            norecs = GetNoRec(dhz, cnb, cnc);//dhz.Tables.Count > 0 ? dhz.Tables[0].Rows.Count : 0;
            if (norecs > 0) foreach (DataRow dataRow in dhz.Tables[0].Rows) ids += dataRow.ItemArray[0].ToString() + ", ";
            if (withlog) UpdateLog(timestamp, "get all same artist similar album(" + norecs + "):" + ids, false, c("dlcm_TempPath"), "", "MainDB", null, null);

            if (ids.Length > 2) ids = ids.Substring(0, ids.Length - 2);

            cmd = "SELECT ID FROM Main WHERE Duplicate_of in (\"" + ids.Replace(", ", "\",\"") + "\")";
            DataSet dvz = new DataSet(); dvz = SelectFromDB("Main", cmd, "", cnb, cnc);
            norecs = GetNoRec(dvz, cnb, cnc);//dvz.Tables.Count > 0 ? dvz.Tables[0].Rows.Count : 0;
            if (norecs > 0) foreach (DataRow dataRow in dvz.Tables[0].Rows) ids += dataRow.ItemArray[0].ToString() + ", ";
            if (withlog) UpdateLog(timestamp, "get all alternate of any of these(" + norecs + "):" + ids, false, c("dlcm_TempPath"), "", "MainDB", null, null);

            if (ids.Length > 2) if (ids.Substring(ids.Length - 2, 2) == ". ") ids = ids.Substring(0, ids.Length - 2);
            return ids;
        }

        public static string ApplyForcedAlbumCoverDefaulting(OleDbConnection cnb, SQLite.SQLiteConnection cnc, System.Windows.Forms.ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            var cmd = "SELECT Artist, Album_Year, Album, ID FROM Main ORDER BY Artist, Album";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Main", cmd, "", cnb, cnc); /*var tz = 0; */var checkedids = "";
            /*var norec = 0;var tsst = "";*/
            int norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0; 
            var t = 0; var gID = ""; var gAlbum_Year = "";
            var timestamp = DateTime.Now; var gArtist = ""; var gAlbum = ""; pB_ReadDLCs.Maximum = norecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
            if (norecs > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    //if (gArtist == dataRow.ItemArray[0].ToString() && gAlbum_Year != dataRow.ItemArray[1].ToString() && gAlbum != dataRow.ItemArray[2].ToString() && gAlbum_Year != "") continue;
                    gArtist = dataRow.ItemArray[0].ToString();
                    gAlbum_Year = dataRow.ItemArray[1].ToString();
                    gAlbum = dataRow.ItemArray[2].ToString();
                    gID = dataRow.ItemArray[3].ToString();
                    if (checkedids.Contains(gID)) continue;

                    pB_ReadDLCs.Increment(1);
                    pB_ReadDLCs.CreateGraphics().Clear(System.Drawing.Color.HotPink);
                    pB_ReadDLCs.CreateGraphics().DrawString(norecs + "\\" + pB_ReadDLCs.Value, new System.Drawing.Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    var ids = GetSameAlbum(gArtist, gAlbum_Year, gAlbum, gID, false);
                    checkedids += "," + ids;

                    //var a = ConfigRepository.Instance()["dlcm_AdditionalManipul84"]; ConfigRepository.Instance()["dlcm_AdditionalManipul84"] = "No";
                    t = SetMultiCover(ids);
                    //ConfigRepository.Instance()["dlcm_AdditionalManipul84"] = a;
                }
            UpdateLog(timestamp, "No of Applied standardizations on Album Cover: " + t, false, c("dlcm_TempPath"), "", "", null, null);

            return t + "/" + norecs + " (Covers standardized)";
        }
        public static int SetMultiCover(string ids)
        {
            //var cmd1 = " WHERE ID IN (" + ids + ")";
            //var norecs = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc); //tz += norecs;

            var cmd = "SELECT Artist, Album_Year, Album, ID, AlbumArtPath FROM Main WHERE ID in (" + ids + ") ORDER BY ID";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Main", cmd, "", cnb, cnc);
            int norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            var tz = "";
            var tArtist = ""; var tAlbum_Year = ""; var tAlbum = ""; var tID = ""; var aap = ""; var aay = ""; bool cleanupreq = false;
            var timestamp = DateTime.Now; int cnt = 0;
            if (norecs > 1)
            {
                foreach (DataRow dataRowz in dfz.Tables[0].Rows)
                {
                    if ((tArtist != dataRowz.ItemArray[0].ToString() || tAlbum != dataRowz.ItemArray[2].ToString() || tAlbum_Year != dataRowz.ItemArray[1].ToString()) && (tArtist != "")) cleanupreq = true;
                    tArtist = dataRowz.ItemArray[0].ToString();
                    tAlbum_Year = dataRowz.ItemArray[1].ToString();
                    tAlbum = dataRowz.ItemArray[2].ToString();
                    tID = dataRowz.ItemArray[3].ToString();
                    if (aap == "")
                    {
                        aap = dataRowz.ItemArray[4].ToString(); aay = dataRowz.ItemArray[1].ToString();
                    }
                    tz += tArtist + " - " + tAlbum + " - " + tAlbum_Year + " - " + tID + "\n";
                }
                tz += "\nwith: " + aap;
                var t = GetNoRecords("SELECT * FROM Main WHERE AlbumArtPath <> \"" + aap + "\" AND ID in (" + ids + "); ", cnb, cnc);
                var dlg = "Do you want to default Cover of the following:\n" + tz + "\n\nchose Cancel to uniformise Years";
                if (cleanupreq && t > 0) cnt += t; ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "";
                //{

                //UpdateLog(timestamp,tz, false, c("dlcm_TempPath"), "", "", null, null);
                //}
                if (cleanupreq && t > 0 ? ShowDialogue(dlg, "", -1, "", 3) : 1 == 2)/* (t > 0 ? 1 == 1 : 1 == 2)*/
                {
                    //cmd = "SELECT top 1 AlbumArtPath FROM Main WHERE ID in (" + ids + ") ORDER BY ID";
                    //DataSet ddz = new DataSet(); ddz = SelectFromDB("Main", cmd, "", cnb, cnc);
                    //norecs = ddz.Tables.Count > 0 ? ddz.Tables[0].Rows.Count : 0; 
                    //if (norecs > 0)
                    //{
                    //foreach (DataRow dataRow in ddz.Tables[0].Rows)
                    //    aap = dataRow.ItemArray[0].ToString();
                    cmd = "UPDATE Main SET AlbumArtPath = \"" + aap + "\" WHERE AlbumArtPath <> \"" + aap + "\" AND ID in (" + ids + ")";
                    if (c("dlcm_GlobalTempVariable") == "OverriteYear") cmd = cmd.Replace(" WHERE ", ", Album_Year=\"" + aay + "\" WHERE ");

                    var dus = UpdateDB("Main", cmd, cnb, cnc);
                    UpdateLog(timestamp, "AlbumArtPath = \"" + aap + " applied to" + norecs + "songs", false, c("dlcm_TempPath"), "", "", null, null);
                    //}
                }
            }
            return cnt;
        }

        public static bool ShowDialogue(string d, string toreplace, int noofstuff, string verb, int nobuttons)
        {
            var buttons = MessageBoxButtons.YesNo;
            if (nobuttons == 2) buttons = MessageBoxButtons.YesNo;
            else if (nobuttons == 3) buttons = MessageBoxButtons.YesNoCancel;

            if (c("dlcm_AdditionalManipul84") != "Yes") return true;

            d = noofstuff > -1 ? d.Replace(toreplace, " " + verb + " " + noofstuff.ToString()) + " " : d;

            var result1 = System.Windows.Forms.MessageBox.Show(d, MESSAGEBOX_CAPTION, buttons, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.No) return false;
            if (result1 == DialogResult.Cancel) ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "OverriteYear";
            return true;
        }

        public static string MultiplyAndApplyYear(OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            //Multiply
            var cmd = "SELECT o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " FROM Standardization AS o LEFT JOIN (SELECT count(artist) as c, artist FROM Standardization group by artist, album)  AS f ON o.Artist = f.Artist" +
                        " WHERE o.Year_Correction<>\"\"" +
                        " GROUP BY  o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " ORDER BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album)";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var artist_c = ""; int norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;var tsst = ""; 
            var album_c = ""; var timestamp = DateTime.Now; var norec = 0; var tz = 0; var tu = 0;
            if (norecs > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    if (artist_c == dataRow.ItemArray[1].ToString() && album_c == dataRow.ItemArray[2].ToString())
                        continue;
                    artist_c = dataRow.ItemArray[1].ToString();
                    album_c = dataRow.ItemArray[2].ToString();
                    //var SpotifyArtistID = dataRow.ItemArray[2].ToString();
                    //var SpotifyAlbumID = dataRow.ItemArray[3].ToString();
                    //var SpotifyAlbumURL = dataRow.ItemArray[4].ToString();
                    //var SpotifyAlbumPath = dataRow.ItemArray[5].ToString();
                    var SpotifyYear = dataRow.ItemArray[3].ToString();
                    //var cmd1 = "UPDATE Main SET Spotify_Artist_ID = \"" + SpotifyArtistID + "\",Spotify_Album_ID = \"" + SpotifyAlbumID + "\",Spotify_Album_URL = \"" + SpotifyAlbumURL + "\",Spotify_Album_Path = \"" + SpotifyAlbumPath + "\", WHERE Album=\"" + SpotifyAlbumID + "\"";
                    //DataSet dus = UpdateDB("Main", cmd1 + ";");
                    //dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", ""); try { norec = dus.Tables[0].Rows.Count; } catch { }
                    var cmd1 = " WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\") AND (Year_Correction <> \"" + SpotifyYear + "\")";
                    //SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \""
                    //+ SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\",
                    norecs = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc); tz += norecs;
                    if (norecs > 0)
                    {
                        var dus = UpdateDB("Standardization", "UPDATE Standardization SET Year_Correction = \"" + SpotifyYear + "\"" + cmd1 + "; ", cnb, cnc);
                        UpdateLog(timestamp, "Year to multipply :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
            else UpdateLog(timestamp, "No Year to multiply :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);

            //DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", "SELECT iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
            cmd = "SELECT distinct iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                " FROM Standardization o WHERE o.Year_Correction<>\"\"" +
                " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction;";
            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var NoRec = GetNoRec(dgt, cnb, cnc);//dgt.Tables.Count > 0 ? dgt.Tables[0].Rows.Count : 0;
            if (NoRec > 0)
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[0].ToString();
                    album_c = dataRow.ItemArray[1].ToString();
                    var year_c = dataRow.ItemArray[2].ToString();
                    var cmd1 = "";
                    cmd1 = " WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\" AND Album_Year <> \"" + year_c + "\"";
                    var norex = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc); tu += norex;
                    if (norex > 0)
                    {
                        dgt = UpdateDB("Main", "UPDATE Main SET Album_Year = \"" + year_c + "\"" + cmd1 + "; ", cnb, cnc);
                        UpdateLog(timestamp, "Year to apply Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\" AND Album_Year <> \"" + year_c + "\", " + norec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                    }
                    //if (artist_c != "" && album_c != "" && year_c != "") dgt = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"", "", cnb, cnc);
                    //try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                }
            else UpdateLog(timestamp, "No Year to apply :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);

            return tz.ToString() + "-" + tu.ToString() + " (Multiplied in Standardization - Applied to Main)";
        }

        public static string MultiplyAndApplySpotify(OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            //Multiply
            var cmd = "SELECT o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " FROM Standardization AS o LEFT JOIN (SELECT count(artist) as c, artist FROM Standardization group by artist, album)  AS f ON o.Artist = f.Artist" +
                        " WHERE o.SpotifyArtistID<>\"\"" +
                        " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath" +
                        " ORDER BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album)";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var artist_c = ""; var timestamp = DateTime.Now;
            var album_c = ""; var tz = 0;
            if (GetNoRec(dfz, cnb, cnc) > 0)/*dfz.Tables.Count*/
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    if (artist_c == dataRow.ItemArray[1].ToString() && album_c == dataRow.ItemArray[2].ToString())
                        continue;
                    artist_c = dataRow.ItemArray[1].ToString();
                    album_c = dataRow.ItemArray[2].ToString();
                    var SpotifyArtistID = dataRow.ItemArray[3].ToString();
                    var SpotifyAlbumID = dataRow.ItemArray[4].ToString();
                    var SpotifyAlbumURL = dataRow.ItemArray[5].ToString();
                    var SpotifyAlbumPath = dataRow.ItemArray[6].ToString();
                    var cmd1 = " WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\") AND SpotifyArtistID <> \"" + SpotifyArtistID + "\"";

                    var norec = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc); tz += norec;
                    if (norec > 0)
                    {
                        var dus = UpdateDB("Main", "UPDATE Main SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \"" + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\" " + cmd1 + ";", cnb, cnc);
                        UpdateLog(timestamp, "Spotify to apply (Artist =\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\") AND SpotifyArtistID <> \"" + SpotifyArtistID + "\", " + norec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
            else UpdateLog(timestamp, "No Spotify to multiply :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);

            var NoRec = 0; var tt = 0;
            cmd = "SELECT distinct iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath" +
                " FROM Standardization o WHERE o.SpotifyArtistID<>\"\"" +
                " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath;";
            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", cmd, "", cnb, cnc);

            if (GetNoRec(dgt, cnb, cnc) > 0)
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[0].ToString();
                    album_c = dataRow.ItemArray[1].ToString();
                    var SpotifyArtistID = dataRow.ItemArray[2].ToString();
                    var SpotifyAlbumID = dataRow.ItemArray[3].ToString();
                    var SpotifyAlbumURL = dataRow.ItemArray[4].ToString();
                    var SpotifyAlbumPath = dataRow.ItemArray[5].ToString();
                    var cmd1 = "";
                    cmd1 = " WHERE Artist=\"" + artist_c + "\" AND (Album=\"" + album_c + "\") AND SpotifyArtistID <> \"" + SpotifyArtistID + "\"";
                    NoRec = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc); tt += NoRec;
                    if (NoRec > 0)
                    {
                        var dus = UpdateDB("Main", "UPDATE Main SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \"" + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\" " + cmd1 + ";", cnb, cnc);
                        UpdateLog(timestamp, "Spotify to apply Artist=\"" + artist_c + "\" AND (Album=\"" + album_c + "\") AND SpotifyArtistID <> \"" + SpotifyArtistID + "\" AND SpotifyArtistID <> \"" + SpotifyArtistID + "\", " + NoRec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
            else UpdateLog(timestamp, "No Spotify to apply :" + cmd, false, c("dlcm_TempPath"), "", "", null, null);

            return tz + "-" + tt + " (update Main - Multiply Standardization Spotify info)";
        }

        public static string ApplyArtistShort(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;
            var norec = 0; /*var tsst = "";*/
            var cmd = "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short" +
                " FROM Standardization WHERE (Artist_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist)," +
                " Artist_Short;";
            DataSet dfz = new DataSet();
            DataSet dus = new DataSet();
            dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var recs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            if (recs > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var short_c = dataRow.ItemArray[1].ToString();
                    var cmd1 = " WHERE Artist=\"" + artist_c + "\" AND Artist_ShortName <> \"" + short_c + "\"";
                    norec = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc);
                    if (norec > 0)
                    {
                        UpdateLog(timestamp, "Updated Main Artist_short=\"" + artist_c + "\" AND Artist_ShortName <> \"" + short_c + "\" with " + short_c + ", " + norec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                        DataSet dis = UpdateDB("Main", "UPDATE Main SET Artist_ShortName = \"" + short_c + "\"" + cmd1 + "; ", cnb, cnc);
                    }

                    //var dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", "", cnb, cnc);
                    //try { norec = dus.Tables[0].Rows.Count; } catch { }

                    cmd1 = "WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND Artist_Short <> \"" + short_c + "\"";
                    norec = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc);
                    if (artist_c != "" && short_c != "" && norec > 0)
                    {
                        UpdateLog(timestamp, "Updated Standardization Artist_Short=\"" + artist_c + "\" to " + short_c + ", " + norec + " times.", false, c("dlcm_TempPath"), "", "", null, null);
                        dus = UpdateDB("Standardization", "UPDATE Standardization SET Artist_Short = \"" + short_c + "\" " + cmd1 + ";", cnb, cnc);
                    }
                }
            else UpdateLog(timestamp, "No shorts to update: " + cmd, false, c("dlcm_TempPath"), "", "", null, null);
            UpdateLog(timestamp, recs.ToString() + "-" + norec.ToString() + " (Update Main - Multiply Standardization)", false, c("dlcm_TempPath"), "", "", null, null);
            return recs.ToString() + "-" + norec.ToString() + " (Update Main - Multiply Standardization)";
            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        //pseudocode:
        //GEt all Default-ing Grousp from Standardization
        //match them against already existing CDLC mapped to the each Dflt Grp
        //then add new dlc to grp
        public static string ApplyArtistAutoGroup(OleDbConnection cnb, ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
            var j = 0;
            DataSet df = new DataSet();
            var selstand = "SELECT DISTINCT Artist_AutoGroup, iif(Artist_Correction<>\"\", Artist_Correction, Artist) FROM Standardization WHERE Artist_AutoGroup<>\"\"";
            df = SelectFromDB("Standardization", selstand, "", cnb, cnc);
            var norec = GetNoRec(df, cnb, cnc);//df.Tables.Count > 0 ? df.Tables[0].Rows.Count : 0;
            var i = 0; var tsst = ""; var norecs = 0;
            pB_ReadDLCs.Maximum = norec; pB_ReadDLCs.Value = 0;
            if (norec > 0)
                foreach (DataRow defaultgrp in df.Tables[0].Rows)
                {
                    string grp = defaultgrp.ItemArray[0].ToString();
                    string artist_c = defaultgrp.ItemArray[1].ToString();

                    //Get just the IDs of songs already defaulted in Groups
                    var IDs = GetSelectIDs("SELECT DISTINCT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + grp + "\" AND VAL(CDLC_ID) IN (SELECT ID FROM Main WHERE Artist =\"" + artist_c + "\")", cnb, cnc);

                    i++;
                    DataSet dfz = new DataSet();
                    selstand = "SELECT ID FROM Main WHERE ID NOT IN (" + IDs + ") AND Artist =\"" + artist_c + "\";";
                    dfz = SelectFromDB("Main", selstand, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
                    norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
                    if (norecs > 0)
                    {
                        //check grp id
                        DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp + "\"", "", cnb, cnc);//chbx_AllGroups.Items[chbx_AllGroups.SelectedIndex]
                        var noOfRec = GetNoRec(dgs, cnb, cnc);
                        var grpnoorg = noOfRec > 0 ? dgs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";

                        pB_ReadDLCs.Maximum = norecs; pB_ReadDLCs.Value = 0;
                        foreach (DataRow dataRow in dfz.Tables[0].Rows)
                        {
                            var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                            var insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grpnoorg + "\"";
                            j++; pB_ReadDLCs.Increment(1);
                            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        }
                        // tsst = "9/12 Apply Artist Auto Group DLC in the grp " + i + "-" + grp + ", " + artist_c + ", " + norecs + "times"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    }
                    pB_ReadDLCs.Maximum = norec; pB_ReadDLCs.Value = i;
                    tsst = "9/12 Applied " + norecs + " " + artist_c + " Auto Group '" + grp + "' grp"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
            tsst = "AutoGroups applied " + j + " times out of " + norec + " Artists marked as autoGroups"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            return j + "/" + norec.ToString() + " (added new songs to Defaulting-Group / Total AutoGroups and unique group&artist||correction)";// norecs.ToString() + "/"
                                                                                                                                                //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        //pseudocode:
        public static void ApplyArtistAutoGroup_old(OleDbConnection cnb, ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
            DataSet dgf = new DataSet();
            dgf = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\"", "", cnb, cnc);
            if (GetNoRec(dgf, cnb, cnc) == 0) return;/*dgf.Tables.Count*/

            DataSet df = new DataSet();
            df = SelectFromDB("Standardization", "SELECT DISTINCT Artist_AutoGroup,iif(Artist_Correction<>\"\", Artist_Correction, Artist) FROM Standardization WHERE Artist_AutoGroup<>\"\"", "", cnb, cnc);
            if (GetNoRec(df, cnb, cnc) > 0) foreach (DataRow defaultgrp in df.Tables[0].Rows)/*df.Tables.Count*/
                {
                    string grp = defaultgrp.ItemArray[0].ToString();
                    string artist_c = defaultgrp.ItemArray[1].ToString();

                    var norec = 0;
                    DataSet dfz = new DataSet();
                    dfz = SelectFromDB("Standardization", "SELECT ID FROM Main WHERE Artist+Album IN (SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist)+iif(Album_Correction<>\"\", Album_Correction, Album) FROM Standardization" +
                        " WHERE (Artist_AutoGroup = \"" + grp + "\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist)+iif(Album_Correction<>\"\", Album_Correction, Album));", "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

                    //DeleteFromDB("Standardization", "DELETE * FROM Groups WHERE Groupz=\"" + grp + "\" AND Type=\"DLC\" ", cnb, cnc);
                    pB_ReadDLCs.Maximum = norec;
                    pB_ReadDLCs.Value = 0;
                    var tsst = "9/12 Apply Artist Auto Group DLC in Default grp " + grp + "check"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);

                    if (GetNoRec(dfz, cnb, cnc) > 0)//dfz.Tables.Count
                        foreach (DataRow dataRow in dfz.Tables[0].Rows)
                        {
                            var found = false;
                            var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                            var insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
                            //insertvalues = SearchCmd.Replace("*", "");
                            pB_ReadDLCs.Increment(1);
                            foreach (DataRow dlc in dgf.Tables[0].Rows) if (dlc.ItemArray[0].ToString() == dataRow.ItemArray[0].ToString()) { found = true; break; }
                            if (!found) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        }
                    var cmd1 = "UPDATE Standardization SET Artist_AutoGroup = \"" + grp + "\" WHERE Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\"";
                    DataSet dfu = new DataSet(); dfu = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                    tsst = "9/12 Apply Artist Auto Group " + grp + " end check"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
                }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        //Trim all the songs and their Alternate_version_no
        // populate all missing duplicates
        // 1. get all the songs with 
        //      - duplicate_of not ""
        //      - is_alternate="Yes"
        //      - Alternate_version_no not ""
        //    1.1. make all alternate songs (Say you incl officials)

        // 2. per each group check if song grp is missing else
        // 2.1 increase alternate songs appropiatels

        // 3. make only one newer
        // 3.1 make the rest older 1/x
        public static string CleanAlternates_and_MultiplyGroups(OleDbConnection cnb, SQLite.SQLiteConnection cnc, ProgressBar pB_ReadDLCs)//, string grps, bool inclexcl(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
            var tsst = "";


            var cmd1 = ""; var cmd2 = ""; ; var cmd3 = "";// var cmd4 = "";

            cmd3 = "UPDATE Main SET Alternate_Version_No=Replace(Alternate_Version_No,\" \",\"\");";
            var no_o = GetNoRecords("SELECT ID FROM Main WHERE Alternate_Version_No<>Replace(Alternate_Version_No,\" \",\"\")", cnb, cnc);
            DataSet dts = new DataSet(); dts = UpdateDB("Main", cmd3, cnb, cnc);

            DataSet dvz = new DataSet();
            cmd2 = "SELECT Artist, Song_Title, Duplicate_of,count(Duplicate_Of) as ID FROM Main WHERE Duplicate_Of=\"0\"  Group by Artist,Song_Title,Duplicate_of;";
            dvz = SelectFromDB("Main", cmd2, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
            var noOfRecr = 0;// dvz.Tables.Count==0 ? 0 : dvz.Tables[0].Rows.Count;
            var gho = GetNoRec(dvz, cnb, cnc);
            var art = ""; var ids = ""; var st = "";
            try
            {
                if (gho > 1)
                    pB_ReadDLCs.Maximum = gho + 1; pB_ReadDLCs.Value = 0; //pB_ReadDLCs.Increment(1);


                foreach (DataRow dataRow in dvz.Tables[0].Rows)
                {
                    pB_ReadDLCs.Value++;

                    if (dataRow.ItemArray[3].ToString().ToInt32() < 2)
                    {
                        if (gho > pB_ReadDLCs.Value) continue;
                        else break;
                    }
                    noOfRecr++;
                    art = dataRow.ItemArray[0].ToString();
                    st = dataRow.ItemArray[1].ToString();

                    DataSet duz = new DataSet();
                    var cmd6 = "SELECT ID FROM Main where LCASE(Artist)=\"" + art.ToLower() + "\" and LCASE(Song_Title)=\"" + st.ToLower() + "\" order by Is_Original DESC;";
                    duz = SelectFromDB("Main", cmd6, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

                    //ids += dataRow.ItemArray[3].ToString() + ",";
                    //if (art != dataRow.ItemArray[0].ToString() && st != dataRow.ItemArray[1].ToString() & art != "" && st != "" && ids != "")
                    //{
                    //DataSet dgz = new DataSet();
                    //cmd4 = "SELECT ID FROM Main WHERE Artist=\""+art+ "\" and Song_Titlet=\""+st+"\" order by Is_Official;";
                    //dvz = SelectFromDB("Groups", cmd4, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

                    var cmd5 = "UPDATE Main SET Is_Alternate='Yes',Duplicate_of=\"" + duz.Tables[0].Rows[0][0].ToString() + "\" WHERE ID IN (" + cmd6 + ")" +
                        (c("dlcm_AdditionalManipul117") == "Yes" ? "" : " AND Is_Original<>''") + ";";
                    DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmd5, cnb, cnc);
                    //art = ""; st = ""; ids = "";
                }
            }
            catch (Exception ex)
            {
                tsst = "Erro ..." + ex; timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
            //delete when sam id as duplcaite of
            //cmd3 = "UPDATE Main SET Duplicate_of='' WHERE Duplicate_of=ID;";
            //DataSet tts = new DataSet(); tts = UpdateDB("Main", cmd3, cnb, cnc);
            //}

            DataSet dgz = new DataSet();
            cmd2 = "SELECT Artist, Song_Title, id, Duplicate_of FROM Main order by Artist, Song_Title, Is_Original ;";
            dgz = SelectFromDB("Main", cmd2, "", cnb, cnc);
            var id = ""; var dup = ""; var dupl = ""; var upd = false; var norm = 0; //pB_ReadDLCs.Increment(1);var noOfRecs = 0; 
            gho = GetNoRec(dgz, cnb, cnc);
            if (gho > 1)
            {
                pB_ReadDLCs.Maximum = gho; pB_ReadDLCs.Value = 0;
                foreach (DataRow dataRow in dgz.Tables[0].Rows)
                {
                    //
                    //;ids += dataRow.ItemArray[2].ToString() + ",";

                    pB_ReadDLCs.Value++;
                    if (CleanTitleFurther(art).ToLower() == CleanTitleFurther(dataRow.ItemArray[0].ToString().ToLower())
                        && CleanTitleFurther(st).ToLower() == CleanTitleFurther(dataRow.ItemArray[1].ToString()).ToLower()
                        & art != "" && st != "")// && st != dataRow.ItemArray[1].ToString())//&& dgz.Tables[0].Rows[3][0].ToString() == "0")
                    {
                        ids += dataRow.ItemArray[2].ToString() + ",";
                        if (dup == "") dup = id;
                        if (dupl == "0" || dataRow.ItemArray[3].ToString() == "0" || dupl != dataRow.ItemArray[3].ToString()) upd = true;
                        norm++;
                    }
                    else
                    {
                        if (ids != "" && upd)/*&& ids.Substring(0, ids.Length - 1).Contains(",")*/
                        {
                            //DataSet dgz = new DataSet(); && ids != ""
                            //cmd4 = "SELECT ID FROM Main WHERE Artist=\""+art+ "\" and Song_Titlet=\""+st+"\" order by Is_Official;";
                            //dvz = SelectFromDB("Groups", cmd4, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

                            var cmd5 = "UPDATE Main SET Is_Alternate='Yes',Duplicate_of=\"" + dup + "\" WHERE ID IN (" + ids + dup + ")" +//ids.Substring(0, ids.Length - 1) + ")" +
                                (c("dlcm_AdditionalManipul117") == "Yes" ? "" : " AND Is_Original<>''") + ";";//+ 
                            DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmd5, cnb, cnc);
                            norm++;
                        }
                        dup = ""; ids = ""; upd = false;
                    }
                    art = dataRow.ItemArray[0].ToString();
                    st = dataRow.ItemArray[1].ToString();
                    dupl = dataRow.ItemArray[3].ToString();
                    id = dataRow.ItemArray[2].ToString();
                }
            }
            //delete when sam id as duplcaite of
            //cmd3 = "UPDATE Main SET Duplicate_of='' WHERE Duplicate_of=ID;";
            //DataSet tts = new DataSet(); tts = UpdateDB("Main", cmd3, cnb, cnc);

            var norec = 0; var altfixed = 0;
            DataSet dzh = new DataSet();
            cmd1 = "SELECT distinct ID,Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM ( " +
                " SELECT distinct ID, Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM Main WHERE Duplicate_of<> '' and Duplicate_of<>'0'" +
                 " UNION ALL" +
                 " SELECT distinct Duplicate_of as ID, Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM Main WHERE Duplicate_of<>'' and Duplicate_of<>'0'" +
                 " UNION ALL" +
                 " SELECT distinct Duplicate_of as ID, Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM Main WHERE Alternate_Version_No<>''" +
                 " UNION ALL" +
                 " SELECT distinct ID, Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM Main WHERE Alternate_Version_No = 'Yes'" +
                 " UNION ALL" +
                 " SELECT distinct ID, Duplicate_of,'' as norod, '' as age,LastConversionDateTime FROM Main WHERE Is_Alternate = 'Yes'" +
                ") ORDER BY Duplicate_of" +
                ";";
            dzh = SelectFromDB("Main", cmd1, "", cnb, cnc);
            norec = GetNoRec(dzh, cnb, cnc);//dzh.Tables.Count == 0 ? 0 : dzh.Tables[0].Rows.Count;/*Artist_AutoGroup /*, Artist_AutoGroup,*/
            if (norec > 0)
            {
                var emptys = 0;
                var idd = "";
                var dupli = "";
                var noor = "";
                var age = "";
                var lastdate = "";
                var start = -1;
                var end = -1;

                for (int k = 0; k < norec; k++)
                {
                    idd = dzh.Tables[0].Rows[k].ItemArray[0].ToString();
                    noor = dzh.Tables[0].Rows[k].ItemArray[2].ToString();
                    age = dzh.Tables[0].Rows[k].ItemArray[3].ToString();
                    lastdate = dzh.Tables[0].Rows[k].ItemArray[4].ToString();
                    if (idd == "0" || dupli == "0") { emptys++; continue; }

                    if (dupli != dzh.Tables[0].Rows[k].ItemArray[1].ToString())
                    {
                        if (start != -1)
                        {
                            end = k - 1;
                            if (start != end && end > start)
                                SetOrderAndAge(dzh, start, end);// if (altfixed++;
                        }
                        else start = k;
                    }

                    dupli = dzh.Tables[0].Rows[k].ItemArray[1].ToString();
                }
            }
            //}

            //tsst = "Apply Groups rules (" + (inclexcl ? "adding to groupas" : "deleting from group") + ")" + grp1 + "cause in: " + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);

            //Show Summary window
            var summary = "Cleaned: \n\n" +
                "\nAlternate no with spaces inside:" + no_o +
               "\nAlternate with duplicate marked as 0 PC cleansed: " + noOfRecr +
               "\nAlternate with SAME (cleaned) artist & title marked as alternates " + norm +
                ("\n\nAlternates manipulated: " + altfixed + " / " + norec);
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Summary of the Mass-Alternate Cleanup process", false, false, true, "", "", "", false);
            frm9.Show();
            return norec.ToString();
        }

        public static DataSet SetOrderAndAge(DataSet dzh, int start, int end)
        {
            for (int k = start; k < end; k++)
            {
                var chang = false;
                //Get the oldest timestamp
                //myNewDate = DateTime.ParseExact(datenew, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                //myOldDate = DateTime.ParseExact(dateold, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                var idd = dzh.Tables[0].Rows[k].ItemArray[0].ToString();
                var dupli = dzh.Tables[0].Rows[k].ItemArray[0].ToString();
                var noor = dzh.Tables[0].Rows[k].ItemArray[2].ToString();
                var age = dzh.Tables[0].Rows[k].ItemArray[3].ToString();
                var ld = dzh.Tables[0].Rows[k].ItemArray[4].ToString();
                var lastdate = DateTime.ParseExact(ld, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                for (int i = k + 1; i <= end; i++)
                {
                    var idd2 = dzh.Tables[0].Rows[i].ItemArray[0].ToString();
                    var dupli2 = dzh.Tables[0].Rows[i].ItemArray[0].ToString();
                    var noor2 = dzh.Tables[0].Rows[i].ItemArray[2].ToString();
                    var age2 = dzh.Tables[0].Rows[i].ItemArray[3].ToString();
                    var ld2 = dzh.Tables[0].Rows[i].ItemArray[4].ToString();
                    var lastdate2 = DateTime.ParseExact(ld2, "MM-dd-yy HH:mm", enUS, System.Globalization.DateTimeStyles.None);
                    if (lastdate < lastdate2)
                    {
                        dzh.Tables[0].Rows[k].ItemArray[0] = idd2;
                        dzh.Tables[0].Rows[k].ItemArray[1] = dupli2;
                        dzh.Tables[0].Rows[k].ItemArray[2] = noor2;
                        dzh.Tables[0].Rows[k].ItemArray[3] = age2;
                        dzh.Tables[0].Rows[k].ItemArray[4] = ld2;
                        dzh.Tables[0].Rows[i].ItemArray[0] = idd;
                        dzh.Tables[0].Rows[i].ItemArray[1] = dupli;
                        dzh.Tables[0].Rows[i].ItemArray[2] = noor;
                        dzh.Tables[0].Rows[i].ItemArray[3] = age;
                        dzh.Tables[0].Rows[i].ItemArray[4] = ld;
                        chang = true;
                    }
                }

                if (chang) UpdateOrdAge(dzh, start, end);
            }

            return dzh;
        }
        public static void UpdateOrdAge(DataSet dzh, int start, int end)
        {
            var nor = 0;
            for (int k = start; k < end; k++)
            {
                var idd = dzh.Tables[0].Rows[k].ItemArray[0].ToString();
                //var dupli = dzh.Tables[0].Rows[0].ItemArray[0].ToString();
                var noor = dzh.Tables[0].Rows[k].ItemArray[2].ToString();
                var age = dzh.Tables[0].Rows[k].ItemArray[3].ToString();
                var ld = dzh.Tables[0].Rows[k].ItemArray[4].ToString();

                DataSet dgf = new DataSet();
                var st = "";
                dgf = SelectFromDB("Main", "SELECT Song_Title FROM Main WHERE ID = " + idd, "", cnb, cnc);
                if (GetNoRec(dgf, cnb, cnc) > 0) st = dgf.Tables[0].Rows[0].ItemArray[0].ToString();/*dgf.Tables.Count*/

                var cmd5 = "UPDATE Main SET " +
                    "Alternate_Version_No,='" + nor +
                    ",Song_Title=" + ReplaceAge(st, GetAge(k, start, end)) +//Replace('newer',"+GetAge(k,start,end)+")'" +"" +
                    "WHERE ID=" + idd + ";";//+ 
                DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmd5, cnb, cnc);
            }
        }

        public static string GetAge(int cur, int start, int end)
        {
            if (cur == start) return "newest";
            if (cur == end) return "oldest";
            return "old" + (end - start == 1 ? "er" : "") + (end - start == 2 ? "er" : "") + (end - start == 3 ? "er" : "") + (end - start == 4 ? "er"
                : "") + (end - start == 5 ? "er" : "") + (end - start == 6 ? "er" : "") + (end - start == 7 ? "er" : "") + (end - start == 8 ? "er" : "")
                + (end - start == 9 ? "er" : "") + (end - start == 10 ? "er" : "") + (end - start == 11 ? "er" : "") + (end - start == 12 ? "er" : "")
                + (end - start == 13 ? "er" : "") + (end - start == 14 ? "er" : "") + (end - start == 15 ? "er" : "") + (end - start == 16 ? "er" : "");
        }

        public static string ReplaceAge(string st, string newage)
        {
            if (st.Contains("newest")) if (st.IndexOf(("newest")) > st.IndexOf("[")) st = st.Replace("newest", "");
            if (st.Contains("oldest")) if (st.IndexOf(("oldest")) > st.IndexOf("[")) st = st.Replace("oldest", "");
            if (st.Contains("olderer")) ReplaceAge(st, newage);
            if (st.Contains("older")) if (st.IndexOf(("older")) > st.IndexOf("[")) st = st.Replace("older", "");
            return st;
        }

        public static string MultiplyMetaAlbum(OleDbConnection cnb, SQLite.SQLiteConnection cnc, ProgressBar pB_ReadDLCs)//, string grps, bool inclexcl(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
                                         //var tsst = "";

            var cmd1 = ""; var cmd3 = ""; //var cmd4 = "";var cmd2 = ""; ;
            pB_ReadDLCs.Maximum = 5; pB_ReadDLCs.Value = 0; pB_ReadDLCs.Increment(1);

            cmd3 = "UPDATE Main SET " + c("dlcm_CustomToAtribute_1") + "=\"Yes\" " +
                "WHERE Artist + Album in (";
            cmd1 = "SELECT IIF(Artist_Correction <>\"\",Artist_Correction,Artist) + IIF(Album_Correction <>\"\",Album_Correction,Album) FROM Standardization " +
                "WHERE CustomToAtribute_1='Yes' or CustomToAtribute_1 is not Null ";
            cmd3 += cmd1 + ") and (" + c("dlcm_CustomToAtribute_1") + "<>'Yes'or " + c("dlcm_CustomToAtribute_1") + " is Null)";
            var no_o1 = GetNoRecords(cmd1, cnb, cnc);
            DataSet dts = new DataSet(); if (no_o1 > 0) dts = UpdateDB("Main", cmd3, cnb, cnc);
            //cmd3 = "UPDATE Main SET Is_GameSoundtrack=\"Yes\" " +
            //    "WHERE Artist + Album in (" +
            //    "SELECT IIF(Artist_Correction <>\"\",Artist_Correction,Artist) + IIF(Album_Correction <>\"\",Album_Correction,Album) FROM Standardization " +
            //    "WHERE CustomToAtribute_1='Yes' or CustomToAtribute_1 is not Null " +
            //    ") and (Is_GameSoundtrack<>'Yes'or Is_GameSoundtrack is Null)";


            pB_ReadDLCs.Value++; var no_o2 = GetNoRecords(cmd1.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_2")).Replace("CustomToAtribute_1", "CustomToAtribute_2"), cnb, cnc);
            DataSet dns = new DataSet(); if (no_o2 > 0 && c("dlcm_CustomToAtribute_2") != "") dns = UpdateDB("Main", cmd3.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_2")).Replace("CustomToAtribute_1", "CustomToAtribute_2"), cnb, cnc);

            pB_ReadDLCs.Value++; var no_o3 = GetNoRecords(cmd1.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_3")).Replace("CustomToAtribute_1", "CustomToAtribute_3"), cnb, cnc);
            DataSet dgs = new DataSet(); if (no_o3 > 0 && c("dlcm_CustomToAtribute_3") != "") dgs = UpdateDB("Main", cmd3.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_3")).Replace("CustomToAtribute_1", "CustomToAtribute_3"), cnb, cnc);

            pB_ReadDLCs.Value++; var no_o4 = GetNoRecords(cmd1.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_4")).Replace("CustomToAtribute_1", "CustomToAtribute_4"), cnb, cnc);
            DataSet dus = new DataSet(); if (no_o4 > 0 && c("dlcm_CustomToAtribute_4") != "") dus = UpdateDB("Main", cmd3.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_4")).Replace("CustomToAtribute_41", "CustomToAtribute_4"), cnb, cnc);

            pB_ReadDLCs.Value++; var no_o5 = GetNoRecords(cmd1.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_5")).Replace("CustomToAtribute_1", "CustomToAtribute_5"), cnb, cnc);
            DataSet dbs = new DataSet(); if (no_o5 > 0 && c("dlcm_CustomToAtribute_5") != "") dbs = UpdateDB("Main", cmd3.Replace(c("dlcm_CustomToAtribute_1"), c("dlcm_CustomToAtribute_5")).Replace("CustomToAtribute_1", "CustomToAtribute_5"), cnb, cnc);

            //DataSet dvz = new DataSet();
            //cmd2 = "SELECT Artist, Album, Duplicate_of,count(Duplicate_Of) as cnt FROM Main WHERE Duplicate_Of=\"0\"  Group by Artist,Song_Title,Duplicate_of;";
            //dvz = SelectFromDB("Main", cmd2, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
            //var noOfRecr = 0;// dvz.Tables.Count==0 ? 0 : dvz.Tables[0].Rows.Count;
            //var art = ""; var ids = ""; var st = ""; pB_ReadDLCs.Maximum= dvz.Tables[0].Rows.Count; pB_ReadDLCs.Value = 0;
            //foreach (DataRow dataRow in dvz.Tables[0].Rows)
            //{
            //    pB_ReadDLCs.Value++;
            //    if (dataRow.ItemArray[3].ToString().ToInt32() < 2) continue;
            //    noOfRecr++;
            //    art = dataRow.ItemArray[0].ToString();
            //    st = dataRow.ItemArray[1].ToString();

            //    DataSet duz = new DataSet();
            //    var cmd6 = "SELECT ID FROM Main where LCASE(Artist)=\"" + art.ToLower() + "\" and LCASE(Song_Title)=\"" + st.ToLower() + "\" order by Is_Original DESC;";
            //    duz = SelectFromDB("Main", cmd6, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

            //    //ids += dataRow.ItemArray[3].ToString() + ",";
            //    //if (art != dataRow.ItemArray[0].ToString() && st != dataRow.ItemArray[1].ToString() & art != "" && st != "" && ids != "")
            //    //{
            //    //DataSet dgz = new DataSet();
            //    //cmd4 = "SELECT ID FROM Main WHERE Artist=\""+art+ "\" and Song_Titlet=\""+st+"\" order by Is_Official;";
            //    //dvz = SelectFromDB("Groups", cmd4, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

            //    var cmd5 = "UPDATE Main SET Is_Alternate='Yes',Duplicate_of=\"" + duz.Tables[0].Rows[0][0].ToString() + "\" WHERE ID IN (" + cmd2 + ")" +
            //        (c("dlcm_AdditionalManipul117") == "Yes" ? "" : " AND Is_Original<>''") + ";";
            //    DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmd5, cnb, cnc);
            //    //art = ""; st = ""; ids = "";
            //}
            ////delete when sam id as duplcaite of
            ////cmd3 = "UPDATE Main SET Duplicate_of='' WHERE Duplicate_of=ID;";
            ////DataSet tts = new DataSet(); tts = UpdateDB("Main", cmd3, cnb, cnc);
            ////}

            //DataSet dgz = new DataSet();
            //cmd2 = "SELECT Artist, Song_Title, id, Duplicate_of FROM Main order by Artist, Song_Title, Is_Original ;";
            //dgz = SelectFromDB("Main", cmd2, "", cnb, cnc);
            //var noOfRecs = 0; var id = ""; var dup = ""; var dupl = ""; var upd = false; pB_ReadDLCs.Maximum = dgz.Tables[0].Rows.Count; pB_ReadDLCs.Value = 0;var norm = 0;
            //foreach (DataRow dataRow in dgz.Tables[0].Rows)
            //{
            //    //
            //    //;ids += dataRow.ItemArray[2].ToString() + ",";

            //    pB_ReadDLCs.Value++;
            //    if (CleanTitleFurther(art).ToLower() == CleanTitleFurther(dataRow.ItemArray[0].ToString().ToLower())
            //        && CleanTitleFurther(st).ToLower() == CleanTitleFurther(dataRow.ItemArray[1].ToString()).ToLower()
            //        & art != "" && st != "")// && st != dataRow.ItemArray[1].ToString())//&& dgz.Tables[0].Rows[3][0].ToString() == "0")
            //    {
            //        ids += dataRow.ItemArray[2].ToString() + ",";
            //        if (dup == "") dup = id;
            //        if (dupl == "0" || dataRow.ItemArray[3].ToString() == "0" || dupl != dataRow.ItemArray[3].ToString()) upd = true;
            //        norm++;
            //    }
            //    else
            //    {
            //        if (ids != "" && upd)/*&& ids.Substring(0, ids.Length - 1).Contains(",")*/
            //        {
            //            //DataSet dgz = new DataSet(); && ids != ""
            //            //cmd4 = "SELECT ID FROM Main WHERE Artist=\""+art+ "\" and Song_Titlet=\""+st+"\" order by Is_Official;";
            //            //dvz = SelectFromDB("Groups", cmd4, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/

            //            var cmd5 = "UPDATE Main SET Is_Alternate='Yes',Duplicate_of=\"" + dup + "\" WHERE ID IN (" + ids+dup + ")" +//ids.Substring(0, ids.Length - 1) + ")" +
            //                (c("dlcm_AdditionalManipul117") == "Yes" ? "" : " AND Is_Original<>''") + ";";//+ 
            //            DataSet dgs = new DataSet(); dgs = UpdateDB("Main", cmd5, cnb, cnc);
            //            norm++;
            //        }
            //        dup = ""; ids = ""; upd = false;
            //    }
            //    art = dataRow.ItemArray[0].ToString();
            //    st = dataRow.ItemArray[1].ToString();
            //    dupl = dataRow.ItemArray[3].ToString();
            //    id = dataRow.ItemArray[2].ToString();
            //}
            ////delete when sam id as duplcaite of
            ////cmd3 = "UPDATE Main SET Duplicate_of='' WHERE Duplicate_of=ID;";
            ////DataSet tts = new DataSet(); tts = UpdateDB("Main", cmd3, cnb, cnc);

            //var norec = 0;
            //DataSet dzh = new DataSet();
            //cmd1 = "SELECT distinct ID,Duplicate_of FROM ( " +
            //    "SELECT distinct ID, Duplicate_of FROM Main WHERE Duplicate_of<> '' and Duplicate_of<>'0'" +
            //    " UNION ALL" +
            //    " SELECT distinct Duplicate_of as ID, Duplicate_of FROM Main WHERE Duplicate_of<>'' and Duplicate_of<>'0'" +
            //    " UNION ALL" +
            //    " SELECT distinct Duplicate_of as ID, Duplicate_of FROM Main WHERE Alternate_Version_No<>''" +
            //    " UNION ALL" +
            //    " SELECT distinct ID, Duplicate_of FROM Main WHERE Alternate_Version_No = 'Yes'" +
            //    " UNION ALL" +
            //    " SELECT distinct ID, Duplicate_of FROM Main WHERE Is_Alternate = 'Yes'" +
            //    ") ORDER BY Duplicate_of" +
            //    ";";
            //dzh = SelectFromDB("Main", cmd1, "", cnb, cnc);
            //norec = dzh.Tables.Count == 0 ? 0 : dzh.Tables[0].Rows.Count;/*Artist_AutoGroup /*, Artist_AutoGroup,*/
            //foreach (DataRow dataRow in dfz.Tables[0].Rows) {
            //    var album_c = dataRow.ItemArray[0].ToString();
            //    var short_c = dataRow.ItemArray[1].ToString();
            //    cmd2 = "SELECT CDLC_ID FROM Groups WHERE Groupz =\"" + grp1 + "\" AND" +
            //    " CDLC_ID not in (SELECT CDLC_ID FROM Groups WHERE TYPE=\"DLC\" AND Groupz in (\"" + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25 + "\"))";
            //dfz = SelectFromDB("Groups", cmd2, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
            //norec = dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            //if (norec > 0) {
            //    tsst = "11/12 Adding" + norec + " New groups: " + cmd1; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);
            //    foreach (DataRow dataRow in dfz.Tables[0].Rows) {
            //        var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
            //        var insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp21 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
            //        InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);

            //        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp22 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
            //        if (grp22 != "" && grp22 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
            //        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp23 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
            //        if (grp23 != "" && grp23 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
            //        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp24 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
            //        if (grp24 != "" && grp24 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
            //        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp25 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"90\"";
            //        if (grp25 != "" && grp25 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
            //    }
            //}
            //}

            //tsst = "Apply Groups rules (" + (inclexcl ? "adding to groupas" : "deleting from group") + ")" + grp1 + "cause in: " + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);

            ////Show Summary window
            //var summary = "Cleaned: \n" +
            //    "\nAlternate no with spaces inside:" + no_o +
            //   "\nAlternate with dupicate marked as 0 PC: " + noOfRecr +
            //    ("\n\nAletrnates manipulated: " + 0 + " / " + norec);
            //ErrorWindow frm9 = new ErrorWindow(summary, "", "Summary of the Mass-Repack process", false, false, true, "", "", "");
            //frm9.Show();
            return no_o1.ToString() + " - " + no_o2.ToString() + " - " + no_o3.ToString() + " - " + no_o4.ToString() + " - " + no_o5.ToString();// +" - "+;
        }
        public static string ApplyGroupRules(OleDbConnection cnb, SQLite.SQLiteConnection cnc, string grps, bool inclexcl)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            if (grps == "" || grps is null) return "";
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
            var tsst = "";
            string[] ret = grps.Split(';');
            var grp1 = ret[0];
            var grp21 = ret.Length > 1 ? ret[1] : "";
            var grp22 = ret.Length > 2 ? ret[2] : "";
            var grp23 = ret.Length > 3 ? ret[3] : "";
            var grp24 = ret.Length > 4 ? ret[4] : "";
            var grp25 = ret.Length > 6 ? ret[5] : "";

            var cmd1 = "";
            var norec = 0;
            if (inclexcl)
            {
                DataSet dfz = new DataSet();
                cmd1 = "SELECT CDLC_ID FROM Groups WHERE Groupz =\"" + grp1 + "\" AND" +
                    " CDLC_ID not in (SELECT CDLC_ID FROM Groups WHERE TYPE=\"DLC\" AND Groupz in (\"" + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25 + "\"))";
                dfz = SelectFromDB("Groups", cmd1, "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
                norec = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
                if (norec > 0)
                {
                    tsst = "11/12 Adding" + norec + " New groups: " + cmd1; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);

                    //check grp id
                    DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp21 + "\"", "", cnb, cnc);
                    var noOfRec = GetNoRec(dgs, cnb, cnc); var grp21ordno = noOfRec > 0 ? dgs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";
                    DataSet dhs = new DataSet(); dhs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp22 + "\"", "", cnb, cnc);
                    var noOfRe = GetNoRec(dhs, cnb, cnc); var grp22ordno = noOfRe > 0 ? dhs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";
                    DataSet djs = new DataSet(); djs = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp23 + "\"", "", cnb, cnc);
                    var noOfR = GetNoRec(djs, cnb, cnc); var grp23ordno = noOfR > 0 ? djs.Tables[0].Rows[0].ItemArray[0].ToString() : "99";
                    DataSet dks = new DataSet(); dks = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp24 + "\"", "", cnb, cnc);
                    var noOf = GetNoRec(dks, cnb, cnc); var grp24ordno = noOf > 0 ? dks.Tables[0].Rows[0].ItemArray[0].ToString() : "99";
                    DataSet dls = new DataSet(); dls = SelectFromDB("Groups", "SELECT distinct Comments FROM Groups WHERE Comments<>\"\" AND Type=\"DLC\" AND Groupz=\"" + grp25 + "\"", "", cnb, cnc);
                    var noO = GetNoRec(dls, cnb, cnc); var grp25ordno = noO > 0 ? dls.Tables[0].Rows[0].ItemArray[0].ToString() : "99";

                    foreach (DataRow dataRow in dfz.Tables[0].Rows)
                    {
                        var insertcmdd = "CDLC_ID, Groupz, Type, Date_Added, Comments";
                        var insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp21 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grp21 + "\"";
                        InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp22 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grp22 + "\"";
                        if (grp22 != "" && grp22 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp23 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grp23 + "\"";
                        if (grp23 != "" && grp23 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp24 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grp24 + "\"";
                        if (grp24 != "" && grp24 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                        insertvalues = "\"" + dataRow.ItemArray[0].ToString() + "\",\"" + grp25 + "\",\"DLC\"" + ",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\",\"" + grp25 + "\"";
                        if (grp25 != "" && grp25 is not null) InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0, cnc);
                    }
                }
                //    cmd1 = "DELETE Groups WHERE Groupz=\"" + grp1 + "\" AND CDLC_ID in (SELECT CDCL_ID FROM Groups WHERE TYPE=\"DLC\" AND Groupz in (\"" + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25 + "\"))";
                //DataSet dfu = new DataSet(); dfu = UpdateDB("Groups", cmd1 + ";", cnb, cnc);
                //cmd1 = "INSERT INTO Groups WHERE Groupz=\"" + grp21 + "\""; 
            }
            else
            {
                cmd1 = "DELETE * FROM Groups WHERE Groupz in (\"" + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25 + "\") AND " +
                    "CDLC_ID in (SELECT CDLC_ID FROM Groups WHERE TYPE=\"DLC\" AND Groupz=\"" + grp1 + "\")";

                DataSet dfz = new DataSet();
                dfz = SelectFromDB("Groups", cmd1.Replace("DELETE", "SELECT"), "", cnb, cnc); /*Artist_AutoGroup /*, Artist_AutoGroup,*/
                norec = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
                DataSet dhu = new DataSet();
                if (norec > 0) dhu = UpdateDB("Groups", cmd1 + ";", cnb, cnc);
                tsst = "11/12 Removing" + norec + "DLCs from Groups: " + cmd1; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);
            }
            tsst = "Apply Groups rules (" + (inclexcl ? "adding to groupas" : "deleting from group") + ")" + grp1 + "cause in: " + grp21 + "\",\"" + grp22 + "\",\"" + grp23 + "\",\"" + grp24 + "\",\"" + grp25; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null); //pB_ReadDLCs.Increment(1);
            return norec.ToString();
        }

        public static string ApplyAlbumShort(OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            var timestamp = DateTime.Now;
            //var tsst = "";
            var cmd = "SELECT iif(Album_Correction<>\"\", Album_Correction, Album), Album_Short" +
                " FROM Standardization WHERE (Album_Short <> \"\") GROUP BY iif(Album_Correction<>\"\", Album_Correction, Album)," +
                " Album_Short;";
            DataSet dfz = new DataSet(); var cmc = 0; var cmv = 0;
            DataSet dus = new DataSet(); var cms = 0; var cmx = 0;
            dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var norec = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            if (norec > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    var album_c = dataRow.ItemArray[0].ToString();
                    var short_c = dataRow.ItemArray[1].ToString();
                    var cmd1 = " WHERE Album=\"" + album_c + "\" AND Album_ShortName <> \"" + short_c + "\""; cms = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc); cms += cmc;
                    if (cms > 0)
                    {
                        UpdateLog(timestamp, "Updated Main Album_short=\"" + album_c + "\" to " + short_c, false, c("dlcm_TempPath"), "", "", null, null);
                        DataSet dis = UpdateDB("Main", "UPDATE Main SET Album_ShortName = \"" + short_c + "\"" + cmd1 + "; ", cnb, cnc);
                    }

                    cmd1 = "WHERE (Album=\"" + album_c
                        + "\" OR Album_Correction=\"" + album_c + "\") AND Album_Short <> \"" + short_c + "\""; cmx = GetNoRecords("SELECt ID FROM Standardization " + cmd1, cnb, cnc); cmv += cmx;
                    if (album_c != "" && short_c != "" && cmx > 0)
                    {
                        UpdateLog(timestamp, "Updated Standardization Album_Short=\"" + album_c + "\" to " + short_c, false, c("dlcm_TempPath"), "", "", null, null);
                        dus = UpdateDB("Standardization", "UPDATE Standardization SET Album_Short = \"" + short_c + "\" " + cmd1 + ";", cnb, cnc);
                    }
                }
            else UpdateLog(timestamp, "No Album shorts to update: " + cmd, false, c("dlcm_TempPath"), "", "", null, null);
            UpdateLog(timestamp, norec + "/" + cmc + "/" + cmv + " (total corrections and album shorts / Update Main / Multiply Standardization)", false, c("dlcm_TempPath"), "", "", null, null);
            return norec + "/" + cmc + "/" + cmv + " (total corrections and album shorts / Update Main / Multiply Standardization)";
        }

        public static void ApplyExistingTranlations_old(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            //var norec = 0;
            DataSet dfz = new DataSet();
            var cmd = "SELECT Artist, Artist_Correction  FROM Standardization WHERE" +
                " (Artist_Correction <> \"\") GROUP BY Artist, Artist_Correction;";
            dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            if (GetNoRec(dfz, cnb, cnc) > 0)/*dfz.Tables.Count*/
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    var artist = dataRow.ItemArray[0].ToString();
                    var artist_c = dataRow.ItemArray[1].ToString();
                    var cmd1 = "UPDATE Standardization SET Artist_Correction = \"" + artist_c + "\" , Has_Been_Corrected=\"Yes\"" +
                        " WHERE Artist=\"" + artist + "\"" +
                        // OR Artist_Correction=\"" + artist_c + "\" OR Artist=\"" + artist_c + "\" " +
                        "AND Artist_correction <> Null";
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            DataSet dgz = new DataSet();
            cmd = "SELECT Album, Album_Correction, Artist, Artist_Correction FROM Standardization WHERE" +
                 " (Album_Correction <> \"\") GROUP BY Album, Album_Correction, Artist, Artist_Correction;";
            dgz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            if (GetNoRec(dgz, cnb, cnc) > 0)/*dgz.Tables.Count */
                foreach (DataRow dataRow in dgz.Tables[0].Rows)
                {
                    var album = dataRow.ItemArray[0].ToString();
                    var album_c = dataRow.ItemArray[1].ToString();
                    var artist = dataRow.ItemArray[2].ToString();
                    var artist_c = dataRow.ItemArray[3].ToString();
                    var cmd1 = "UPDATE Standardization SET Album_Correction = \"" + album_c + "\", Has_Been_Corrected=\"Yes\"" +
                        " WHERE Artist=\"" + artist + "\"" +
                        //OR Artist_Correction=\"" + artist_c + "\" OR Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist + "\")" +
                        " AND Album=\"" + album + "\"";
                    //OR Album_Correction=\"" + album_c + "\" OR Album=\"" + album_c + "\" OR Album_Correction=\"" + album + "\"))";
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            DataSet dhz = new DataSet();
            cmd = "SELECT Year_Correction, Album, Album_Correction, Artist, Artist_Correction" +
                " FROM Standardization WHERE" +
                " (Year_Correction <> \"\") GROUP BY Year_Correction, Album, Album_Correction, Artist, Artist_Correction;";
            dhz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            if (GetNoRec(dhz, cnb, cnc) > 0)//));/)dhz.Tables.Count )
                foreach (DataRow dataRow in dhz.Tables[0].Rows)
                {
                    var year_c = dataRow.ItemArray[0].ToString();
                    var album = dataRow.ItemArray[1].ToString();
                    var album_c = dataRow.ItemArray[2].ToString();
                    var artist = dataRow.ItemArray[3].ToString();
                    var artist_c = dataRow.ItemArray[4].ToString();
                    var cmd1 = "UPDATE Standardization SET Year_Correction = \"" + year_c + "\", Has_Been_Corrected=\"Yes\"" +
                        " WHERE (Artist=\"" + artist + "\" OR Artist=\"" + artist_c + "\" OR ((Artist_Correction=\"" + artist + "\" OR Artist_Correction=\"" + artist_c + "\") AND Artist_Correction <> NULL))" +
                        " AND (Album=\"" + album + "\" OR Album=\"" + album_c + "\" OR ((Album_Correction=\"" + album + "\" OR Album_Correction=\"" + album_c + "\") AND Album_Correction <> Null))";
                    //" AND Year_Correction=\"" + year_c + "\")";
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }
        }

        public static string ApplyDefaultCover(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
         //}
            var timstamp = DateTime.Now;
            var cmd1 = "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath) " +
                "FROM Standardization WHERE (Default_Cover = \"Yes\") " +
                "GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album),IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath);";
            //var norec = 0; //get al Default ON entries in standardization table
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd1, "", cnb, cnc);
            var noOfRec = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            var tz = 0;
            if (noOfRec > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var album_c = dataRow.ItemArray[1].ToString();
                    var Default_Cover = dataRow.ItemArray[2].ToString();
                    //var cmd1 = "UPDATE Main SET Spotify_Artist_ID = \"" + SpotifyArtistID + "\",Spotify_Album_ID = \"" + SpotifyAlbumID + "\",Spotify_Album_URL = \"" + SpotifyAlbumURL + "\",Spotify_Album_Path = \"" + SpotifyAlbumPath + "\", WHERE Album=\"" + SpotifyAlbumID + "\"";
                    //DataSet dus = UpdateDB("Main", cmd1 + ";");
                    //dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", ""); try { norec = dus.Tables[0].Rows.Count; } catch { }
                    //apply only to Same Artist&Album Names
                    cmd1 = " WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                    var norec = GetNoRecords("SELECt ID FROM Main " + cmd1, cnb, cnc); tz += norec;
                    if (norec > 0)
                    {
                        UpdateLog(timstamp, "Update (" + norec + " times)default cover" + artist_c + "-" + album_c + "-" + Default_Cover, false, c("dlcm_TempPath"), "", "", null, null);
                        var dus = UpdateDB("Standardization", "UPDATE Standardization SET AlbumArt_Correction = \"" + Default_Cover + "\", Default_Cover == \"Yes\"" + cmd1 + "; ", cnb, cnc);
                    }
                    //else
                    //    UpdateLog(DateTime.Now, "No recordsfound: " + cmd1, false, c("dlcm_TempPath"), "", "", null, null);
                }
            else
                UpdateLog(timstamp, "No recordsfound: " + cmd1, false, c("dlcm_TempPath"), "", "", null, null);
            return noOfRec + "-" + tz + " (Defaulted Covers found in Standardization / actual records applied on Main)";
            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static bool copyallfilesinatemplate(MainDBfields SongRecord, int j, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs
                    //    , int norows, OleDbConnection cnb, bool arrangoff, string SongsPath, string format, SQLiteConnection cnz)/*, bool verbose = truestring */
                    , int norows, OleDbConnection cnb, bool arrangoff, string SongsPath, string format, SQLite.SQLiteConnection cnc)/*, bool verbose = truestring */
        {
            var timestamp = UpdateLog(DateTime.Now, j + " loading..." + Path.GetFileName(SongRecord.Folder_Name), true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var platfor = new Platform(GamePlatform.Pc, GameVersion.RS2014);
            //}
            //DLCPackageData info = null;
            //try
            //{
            //    info = DLCPackageData.LoadFromFolder(SongRecord.Folder_Name, platfor);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error at Loading ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    return false;
            //}
            var u = (DateTime.Now - timestamp);
            var dest = ""; /*var i = -1;*/
            //var format = "windows";
            var listofdlcs = "";
            //foreach (var file in info.ArtFiles)
            //{
            //for (var i = 0; i <= 20000; i++)
            //{
            var artp = SongRecord.AlbumArtPath.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds");
            var artpd = Path.GetDirectoryName(artp);
            var songanddlcname = Path.GetFileName(artp).Replace("album", "song").Replace("_128.dds", "").Replace("_256.dds", "").Replace("_64.dds", "");
            listofdlcs += songanddlcname;
            if (listofdlcs.IndexOf(";" + songanddlcname + ";") > 0)
                songanddlcname += j.ToString();
            var dlcname = Path.GetFileName(artp).Replace("album_", "").Replace("_256.dds", "");


            //var r = true;
            //if (SongRecord.Folder_Name.IndexOf("dlcpack") >= 0) r = CheckForRecord("Cache", "SELECT * FROM CACHE WHERE Removed=\"Yes\" AND AlbumArtPath=\"" + artp.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds") + "\"", cnb, cnc);
            //if (r == true) return false;

            //timestamp = UpdateLog(timestamp, "\n ArtAudio songs.", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //info.Arrangements[i].SongXml.Name.Substring(0, info.Arrangements[i].SongXml.Name.IndexOf("_") - 1);
            if (Directory.Exists(SongsPath + "\\" + songanddlcname))
                if (!Directory.Exists(SongsPath + "\\" + SongRecord.DLC_Name))
                    //    ;
                    //else
                    dest = SongsPath + "\\songs_" + SongRecord.DLC_Name;
                else
                    dest = SongsPath + "\\" + songanddlcname;

            if ((File.Exists(SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_" + dlcname + "\\songs_dlc_" + dlcname + ".hsan")
                && (dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan").Length > 250))
                dest = SongsPath + "\\songs_" + SongRecord.ID;
            if (dest.IndexOf("knockinonheavendoorfreddiemercurytribute1992solo1") >= 0)
                dest = SongsPath + "\\songs_" + SongRecord.ID;

            CreateFolder(dest);
            CreateFolder(dest + "\\" + "gfxassets\\album_art");
            FileCopy(artp, dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp), true, j, false);
            FileCopy(artp.Replace("_256.dds", "_128.dds"), dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp.Replace("_256.dds", "_128.dds")), true, j, false);
            FileCopy(artp.Replace("_256.dds", "_64.dds"), dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp.Replace("_256.dds", "_64.dds")), true, j, false);
            if (!File.Exists(dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp)) || !File.Exists(dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp.Replace("_256.dds", "_128.dds"))) || !File.Exists(dest + "\\" + "gfxassets\\album_art\\" + Path.GetFileName(artp.Replace("_256.dds", "_64.dds"))))
                timestamp = UpdateLog(DateTime.Now, " error at prep image files...template: " + dest + "\\" + "gfxassets\\album_art\\", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            else
                timestamp = UpdateLog(DateTime.Now, " copied image files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //if (artp.IndexOf("_128.dds") < 0 || artp.IndexOf("_64.dds") < 0) continue;
            timestamp = UpdateLog(timestamp, u + "-" + j + " Integrating DLC songs (art, wem-s, bnk, xblock, nt) " + j + "/" + norows + " songs. " + dlcname + " " + SongRecord.Folder_Name, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var sourceformat = SongRecord.AudioPath.Substring(SongRecord.AudioPath.IndexOf("\\audio\\") + 7, SongRecord.AudioPath.Length - SongRecord.AudioPath.IndexOf("\\audio\\") - 7);
            sourceformat = sourceformat.Substring(0, sourceformat.IndexOf("\\"));
            Directory.CreateDirectory(dest + "\\" + "audio\\" + ReturnPlatformFolder(format));

            var zile = dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname;
            FileCopy(SongRecord.AudioPath, zile + ".wem", true, j, false);
            //artpd.Replace("gfxassets\\album_art", "") + "audio\\" + format + "\\" + songanddlcname + ".wem"
            //, dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname + ".wem", true, j, false);//song//
            if (File.Exists(SongRecord.audioPreviewPath))
                //artpd.Replace("gfxassets\\album_art", "") + "audio\\" + format + "\\" + songanddlcname + "_preview.wem"))
                FileCopy(SongRecord.audioPreviewPath, zile + "_preview.wem", true, j, false);
            //artpd.Replace("gfxassets\\album_art", "") + "audio\\" + format + "\\" + songanddlcname + "_preview.wem"
            //, dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname + "_preview.wem", true, j, false);
            else
                FileCopy(artpd.Replace("gfxassets\\album_art", "") + "audio\\" + sourceformat + "\\" + songanddlcname + "_preview_fixed.wem", zile + ".wem", true, j, false);
            //SongRecord.audioPreviewPath                   

            //, dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname + "_preview.wem", true, j, false);


            FileCopy(artpd.Replace("gfxassets\\album_art", "") + "audio\\" + sourceformat + "\\" + songanddlcname + ".bnk", zile + ".bnk", true, j, false);
            //, dest + "\\" + "audio\\" + format + "\\" + songanddlcname + ".bnk", true, j);/*song*/
            //, dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileName(SongRecord.AudioPath).Replace(".wem", ".bnk"), true, j, false);

            FileCopy(artpd.Replace("gfxassets\\album_art", "") + "audio\\" + sourceformat + "\\" + songanddlcname + "_preview.bnk", zile + "_preview.bnk", true, j, false);
            //, dest + "\\" + "audio\\" + format + "\\" + songanddlcname + "_preview.bnk", true, j);
            //, dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileName(SongRecord.audioPreviewPath).Replace(".wem", ".bnk").Replace("_fixed",""), true, j, false);

            //if (!File.Exists(dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileName(SongRecord.audioPreviewPath).Replace(".wem", ".bnk").Replace("_fixed", "")) ||
            //    !File.Exists(dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileName(SongRecord.AudioPath).Replace(".wem", ".bnk")) ||
            //    !File.Exists(dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname + ".wem")||
            //    ((File.Exists(SongRecord.audioPreviewPath) && !File.Exists(dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + songanddlcname + "_preview.wem")) || (File.Exists(SongRecord.audioPreviewPath) && File.Êxists(dest + "\\" + "audio\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileName(SongRecord.AudioPath).Replace(".wem", ".bnk")))))
            if (!File.Exists(zile + ".bnk") || !File.Exists(zile + "_preview.bnk") || !File.Exists(zile + ".wem") || !File.Exists(zile + "_preview.wem"))
                timestamp = UpdateLog(DateTime.Now, " error at prep audio files...template: " + zile, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            else
                timestamp = UpdateLog(DateTime.Now, " copied audio files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);


            Directory.CreateDirectory(dest + "\\" + "gamexblocks\\nsongs");
            var xblock = artpd.Replace("gfxassets\\album_art", "") + "gamexblocks\\nsongs\\" + dlcname + "_fcp_disk.xblock";
            if (File.Exists(xblock))
                //{
                FileCopy(xblock, dest + "\\" + "gamexblocks\\nsongs\\" + dlcname + "_fcp_disk.xblock", true, j, false);
            //officialpackedsongs = dlcname + "\n"; cofficialpackedsongs++;
            //}
            else
                //{
                FileCopy(artpd.Replace("gfxassets\\album_art", "") + "gamexblocks\\nsongs\\"
                    + dlcname + ".xblock"
                    , dest + "\\" + "gamexblocks\\nsongs\\" + dlcname + ".xblock", true, j, false);
            //DLCpackedsongs = dlcname + "\n"; cDLCpackedsongs++;
            //}
            if (!File.Exists(dest + "\\" + "gamexblocks\\nsongs\\" + dlcname + "_fcp_disk.xblock") && !File.Exists(dest + "\\" + "gamexblocks\\nsongs\\" + dlcname + ".xblock"))
                timestamp = UpdateLog(DateTime.Now, " error at prep xblock files...template: " + dest + "\\" + "gamexblocks\\nsongs\\", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            else
                timestamp = UpdateLog(DateTime.Now, " copied block files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            if (File.Exists(SongRecord.Folder_Name + "\\" + dlcname + "_aggregategraph.nt")) FileCopy(SongRecord.Folder_Name + "\\" + dlcname + "_aggregategraph.nt"
                , dest + "\\" + dlcname + "_aggregategraph.nt", true, j, false);/*c("dlcm_TempPath") + "\\0_dlcpacks\\temp\\songs_psarc_RS2014_Pc*/
            else if (File.Exists(SongRecord.Folder_Name + "\\songs_aggregategraph.nt"))
                FileCopy(SongRecord.Folder_Name + "\\songs_aggregategraph.nt", dest + "\\" + dlcname + "_aggregategraph.nt", true, j, false);
            else FileCopy(SongRecord.Folder_Name + "\\songs_psarc_rs2014_pc_aggregategraph.nt", dest + "\\" + dlcname + "_aggregategraph.nt", true, j, false);
            if (!File.Exists(dest + "\\" + dlcname + "_aggregategraph.nt") && !File.Exists(dest + "\\" + dlcname + "_aggregategraph.nt") && !File.Exists(dest + "\\" + dlcname + "_aggregategraph.nt"))
                timestamp = UpdateLog(DateTime.Now, " error at prep .nt files...template: " + dest + "\\" + dlcname, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            else
                timestamp = UpdateLog(DateTime.Now, " copied .nt files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //dest = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs_psarc_RS2014_Pc\\song_" + dlcname;
            CopyFolder(SongRecord.Folder_Name + "\\flatmodels", dest + "\\flatmodels");
            //CopyFolder(SongRecord.Folder_Name + "\\gameblocks", dest + "\\gameblocks");
            //CopyFolder(SongRecord.Folder_Name + "\\manifests", dest + "\\manifests");
            //var arng = "";

            DataSet dvs = new DataSet(); dvs = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            //" + " AND ArrangementType=\"Vocal\";", "", cnb, cnc);
            var norec = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;
            var once = true;
            var hhh = "SELECT * FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + GetArrOfficSQLTxt(arrangoff);
            //if (norec == 0)
            //    ;
            for (int k = 0; k < norec; k++)
            {
                if (dvs.Tables[0].Rows[k].ItemArray[26].ToString() == "")
                    continue;//most likely not necessary as handled few lines above
                var xml = SongRecord.Folder_Name + "\\" + "songs\\arr" + "\\" + dvs.Tables[0].Rows[k].ItemArray[26].ToString();//"manifests\\songs_dlc_" + dlcname 

                //most likely not necessary as handled few lines above
                if (File.Exists(xml.Replace("songs\\arr", "manifests\\songs_dlc_songs_psarc_rs2014_pc\\") + ".json")
                    && (dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json").Length > 250)
                    continue;
                else if (File.Exists(xml.Replace("songs\\arr", "manifests\\songs\\") + ".json") &&
                    (dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json").Length > 250)
                    continue;
                else if ((dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json").Length > 250)
                    continue
                        ;
                //if (File.Exists(xml.Replace("songs\\arr", "songs\\bin\\generic") + ".sng")) format = "generic";
                //else if (File.Exists(xml.Replace("songs\\arr", "songs\\bin\\macos") + ".sng")) format = "macos";
                Directory.CreateDirectory(dest + "\\" + "songs\\arr");
                Directory.CreateDirectory(dest + "\\" + "songs\\bin\\" + ReturnPlatformFolder(format));
                FileCopy(xml + ".xml", dest + "\\" + "songs\\arr\\" + Path.GetFileName(xml) + ".xml", true, j, false);

                if (!File.Exists(dest + "\\" + "songs\\arr\\" + Path.GetFileName(xml) + ".xml"))
                    FileCopy((xml + ".xml").Replace(Path.GetDirectoryName(SongRecord.Folder_Name), "songs_psarc_RS2014_Pc"), dest + "\\" + "songs\\arr\\" + Path.GetFileName(xml) + ".xml", true, j, false);
                if (!File.Exists(dest + "\\" + "songs\\arr\\" + Path.GetFileName(xml) + ".xml"))
                    timestamp = UpdateLog(DateTime.Now, " error at prep xml  files...template: " + dest + "\\" + dlcname, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //else
                //    timestamp = UpdateLog(DateTime.Now, " copied xml files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                if (xml.IndexOf("showlights") < 0)
                {
                    var th = xml.Replace("songs\\arr", "songs\\bin\\" + ReturnPlatformBINFolder(sourceformat.ToLower())) + ".sng";
                    var gb = dest + "\\" + "songs\\bin\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".sng";

                    FileCopy(xml.Replace("songs\\arr", "songs\\bin\\" + ReturnPlatformBINFolder(sourceformat.ToLower())) + ".sng"
                        , dest + "\\" + "songs\\bin\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".sng", true, j, true);
                    if (!File.Exists(dest + "\\" + "songs\\bin\\" + ReturnPlatformFolder(format) + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".sng"))
                        timestamp = UpdateLog(DateTime.Now, "Error at prep sng files...: " + dest + "\\" + dlcname, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //else
                    //    timestamp = UpdateLog(DateTime.Now, " copied sng files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //if (File.Exists(xml.Replace("songs\\arr", "manifests\\songs_dlc_" + dlcname)+ ".json") )
                    //{
                    if (once)
                    {
                        Directory.CreateDirectory(dest + "\\" + "manifests\\songs_dlc_" + dlcname);
                        once = false;
                        //C:\t\0\0_dlcpacks\songs_psarc_RS2014_Pc\manifests\songs_dlc_songs_psarc_rs2014_pc
                        //SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_" + dlcname + "\\songs_dlc_" + dlcname + ".hsan"
                        ////"C:\\t\\0\\0_dlcpacks\\songs_psarc_RS2014_Pc\\manifests\\songs_dlc_alliwannado\\songs_dlc_alliwannado.hsan" string
                        if (File.Exists(SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_" + dlcname + "\\songs_dlc_" + dlcname + ".hsan"))
                            FileCopy(SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_" + dlcname + "\\songs_dlc_" + dlcname + ".hsan" //songs_dlc_" + dlcname + ".hsan"/*songs_dlc_" + dlcname */
                                , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j, false);
                        else if (File.Exists(SongRecord.Folder_Name + "\\" + "manifests\\songs\\songs.hsan"))
                            FileCopy(SongRecord.Folder_Name + "\\" + "manifests\\songs\\songs.hsan"/* */
                                , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j, false);
                        else
                            FileCopy(SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_songs_psarc_rs2014_pc\\songs_dlc_songs_psarc_rs2014_pc.hsan"/* */
                                , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j, false);
                        if (!File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan") && !File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan") && !File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan"))
                            timestamp = UpdateLog(DateTime.Now, " error at prep HSAN file...: " + dest + "\\" + dlcname, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        else
                            timestamp = UpdateLog(DateTime.Now, " copied HSAN files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                        //copy showlights too in case not captured (yet;todo)
                        FileCopy(SongRecord.Folder_Name + "\\" + "songs\\arr" + "\\" + dlcname + "_showlights.xml", dest + "\\" + "songs\\arr\\" + dlcname + "_showlights.xml", true, j, false);
                        //copy bin 
                        FileCopy(SongRecord.Folder_Name + "\\NamesBlock.bin", dest + "\\NamesBlock.bin", true, j, false);
                    }

                    //songs_dlc_songs_psarc_rs2014_pc
                    if (File.Exists(xml.Replace("songs\\arr", "manifests\\songs_dlc_songs_psarc_rs2014_pc\\") + ".json"))
                        FileCopy(xml.Replace("songs\\arr", "manifests\\songs_dlc_songs_psarc_rs2014_pc\\") + ".json"
                            , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json", true, j, true);
                    else if (File.Exists(xml.Replace("songs\\arr", "manifests\\songs\\") + ".json"))
                        FileCopy(xml.Replace("songs\\arr", "manifests\\songs\\") + ".json" //songs_dlc_songs_psarc_rs2014_pc
                           , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json", true, j, true);
                    else
                        FileCopy(xml.Replace("songs\\arr", "manifests\\songs_dlc_" + dlcname) + ".json" //songs_dlc_songs_psarc_rs2014_pc
                            , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json", true, j, true);
                    if (!File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json") && !File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json") && !File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json"))
                        timestamp = UpdateLog(DateTime.Now, " error at prep JSON file...: " + dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    else
                        timestamp = UpdateLog(DateTime.Now, " copied JSON files...", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    //}
                    //else
                    //{
                    //    FileCopy(xml.Replace("songs\\arr", "manifests\\songs") + ".json", dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + Path.GetFileNameWithoutExtension(xml + ".xml") + ".json", true, j);
                    //    if (once)
                    //    {
                    //        once = false;
                    //        FileCopy(SongRecord.Folder_Name + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan"
                    //            , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j);
                    //    }
                    //}
                }
            }
            //var hsanFiles = Directory.EnumerateFiles(dest, "*.hsan", System.IO.SearchOption.AllDirectories).ToArray();
            //if (hsanFiles.Length < 1)
            //    throw new DataException("No songs_*.hsan file found.");

            //// merge multiple hsan files into a single hsan file
            //if (hsanFiles.Length > 1)
            //{
            //    var mergeSettings = new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union };
            //    JObject hsanObject1 = new JObject();

            //    foreach (var hsan in hsanFiles)
            //    {
            //        JObject hsanObject2 = JObject.Parse(File.ReadAllText(hsan));
            //        hsanObject1.Merge(hsanObject2, mergeSettings);
            //    }
            //}

            //dest = "";
            //format = info.OggPath.Substring(info.OggPath.IndexOf("\\audio\\") + 7, info.OggPath.Length - info.OggPath.IndexOf("\\audio\\") - 7);
            //format = "generic"; /*i = 0;*/
            //foreach (var file in info.Arrangements)
            //{
            //i++;
            //var dlcname = .SongXml.Name.Substring(0, file.SongXml.Name.IndexOf("_"));
            //dest = c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\songs_psarc_RS2014_Pc\\song_" + dlcname;
            //if (!DirectoryExists(dest)) continue;
            //timestamp = UpdateLog(timestamp, u + "-" + j + " Adding " + i + "/" + info.Arrangements.Count + " (json+hsan): " + dlcname, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Directory.CreateDirectory(dest + "\\" + "manifests\\songs_dlc_" + dlcname);
            //if (file.SongXml.Name.IndexOf("showlights") < 0)
            //{
            //    if (File.Exists(file.SongXml.File.Replace("songs\\arr", "manifests\\songs_dlc_" + dlcname).Replace(".xml", ".json")))
            //    {
            //        FileCopy(file.SongXml.File.Replace("songs\\arr", "manifests\\songs_dlc_" + dlcname).Replace(".xml", ".json"), dest + "\\" + "manifests\\songs_dlc_"
            //            + dlcname + "\\" + file.SongXml.Name + ".json", true, j);
            //        if (!File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan"))
            //            FileCopy(file.SongXml.File.Replace("songs\\arr", "manifests\\songs_dlc_" + dlcname).Replace(file.SongXml.Name, "songs_dlc_" + dlcname).Replace(".xml", ".hsan")
            //            , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j);
            //    }
            //    else
            //    {
            //        FileCopy(file.SongXml.File.Replace("songs\\arr", "manifests\\songs").Replace(".xml", ".json"), dest + "\\" + "manifests\\songs_dlc_"
            //            + dlcname + "\\" + file.SongXml.Name + ".json", true, j);
            //        if (!File.Exists(dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan"))
            //            FileCopy(file.SongXml.File.Replace("songs\\arr", "manifests\\songs").Replace(file.SongXml.Name, "songs").Replace(".xml", ".hsan")
            //            , dest + "\\" + "manifests\\songs_dlc_" + dlcname + "\\" + dlcname + ".hsan", true, j);
            //    }
            //}

            //Directory.CreateDirectory(dest + "\\" + "songs\\arr");
            //FileCopy(file.SongXml.File, dest + "\\" + "songs\\arr\\" + file.SongXml.Name + ".xml", true, j);

            //if (File.Exists(file.SongXml.File.Replace("songs\\arr", "songs\\bin\\generic").Replace(".xml", ".sng"))) format = "generic";
            //if (File.Exists(file.SongXml.File.Replace("songs\\arr", "songs\\bin\\macos").Replace(".xml", ".sng"))) format = "macos";
            //Directory.CreateDirectory(dest + "\\" + "songs\\bin\\" + format);
            //if (file.SongXml.Name.IndexOf("showlights") < 0) FileCopy(file.SongXml.File.Replace("songs\\arr", "songs\\bin\\" + format).Replace(".xml", ".sng")
            //    , dest + "\\" + "songs\\bin\\" + format + "\\" + file.SongXml.Name + ".sng", true, j);

            //}/*file.SongXml.Name*/
            //info.CleanCache();
            return true;
        }
        public static string GetAlbumArtPath(string albm, string orig, string ext)
        {
            if (File.Exists(albm)) return albm.Replace(".dds", ext);
            else if (File.Exists(orig)) return orig.Replace(".dds", ext);

            UpdateLog(DateTime.Now, "Defaulted AlbumArtPath to: " + AppWD + "\\" + c("dlcm_defaultalbumart").Replace(".dds", ext)
               + "\nExisting: " + File.Exists(AppWD + "\\" + c("dlcm_defaultalbumart").Replace(".dds", ext)).ToString()
                , false, c("dlcm_TempPath"), "", "", null, null);

            return AppWD + "\\" + c("dlcm_defaultalbumart").Replace(".dds", ext);
        }

    }
}
