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

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {
        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        public const long BUFFER_SIZE = 4096;
        public static StringBuilder errorsFound;

        public static void OpenDb()
        {
            var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
            tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
                try
                {
                    if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
                }
                catch (Exception exx)
                {

                    ShowConnectivityError(exx, "");/*, null*/
                    try
                    {
                        if (File.Exists(cnb.DataSource.ToString())) cnb.Open(); //2nd time makes it work sometimes e.g. x64 solution
                    }
                    catch (Exception ex)
                    {
                        string vb = null; vb = DisplayData();
                        ShowConnectivityError(ex, "2nd FAIL to use M$ ACCESS plugin:\n" + vb);/*, null*/
                        //revert to SQLite
                        if (File.Exists(tz))
                        {
                            ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                            ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
                        }
                        else MessageBox.Show("No Microsoft Access or SQLite databases (or access;plugins etc) available. Good Luck as (the) C-DLC Manager wont really work!");
                    }
                }
            else
                try
                {
                    ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
                    if (File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"])) cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
                    ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                }
                catch (Exception exx)
                {
                    ShowConnectivityError(exx, "");/*, null*/
                }
        }
        public static void ShowConnectivityError(Exception ex, string txt)/*, System.Windows.Forms.Label lbl*/
        {
            var mssg = "You need to Download Connectivity patch 32/64 bit to match your version of Office @ " +
         txt + ".\n" + "Reinstall in case you feel/see plugin there or is not diplayed when manually checking by running in Windows PowerShell:\n" +
         "(New-Object system.data.oledb.oledbenumerator).GetElements() | select SOURCES_NAME, SOURCES_DESCRIPTION\n" +
         "and if intending to install the x64 variant of plugin please note in order to have it on top of Office 32bit then you need to decompress it and the .msi with /passive flag";

            //Download and install plugin from Microsoft website
            if (ex.Message.IndexOf("The 'Microsoft.ACE.OLEDB.") > -1 && ex.Message.IndexOf("provider is not registered on the local machine.") > -1)
            {
                if (c("dlcm_ShowConenctivityOnce") == "Yes")
                    //{
                    ConfigRepository.Instance()["dlcm_ShowConenctivityOnce"] = "Maybe";//default always to show message only once
                                                                                       //return;
                                                                                       //}
                else if (c("dlcm_ShowConenctivityOnce") == "Maybe") return;
                //Use locally saved 2016 installation kit 
                var xx = "";
                if (File.Exists(c("dlcm_AccessACE.OLEDB.16.0Local64b")) || File.Exists(c("dlcm_AccessACE.OLEDB.16.0Local32b")))
                {
                    DialogResult result1 = DialogResult.Cancel;
                    result1 = MessageBox.Show("As no M$ Access connectivity plugin was found," +
                    " Do you want to:\n 1. (Yes) Install the locally stored 32 bit version" +
                    "\n 2. (No) Install the locally stored 64 bit version , or" +
                    "\n 3. (Cancel)" +
                    "\n \ta) Download by your self using the subsecvent instructions" +
                    "\n \tb) Use SQL-lite3 as DB (you can still use M$ Access (LINKDB.accdb) to access the DB using a OLEDBC plugin)."
                    , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes) xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_AccessACE.OLEDB.16.0Local32b"));
                    if (result1 == DialogResult.No) xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_AccessACE.OLEDB.16.0Local64b"));

                    if (!File.Exists(xx))
                    {
                        if (result1 != DialogResult.Cancel)
                        {
                            ErrorWindow frm2 = new ErrorWindow("Selected bit version(" + xx + ") not available instead the other one is there. close the program and open to try again" +
                            " or manually install it as pe subsecvent instructions", ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]]
                            , "Missing " + xx, false, false, true, "", "", "");
                            frm2.ShowDialog();
                        }
                        else
                        {
                            DialogResult result3 = DialogResult.Cancel;
                            result3 = MessageBox.Show("1. (Yes) Use M$ Access " +
                                "\n2. (No) Use SQL-lite3"
                            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result3 == DialogResult.No) ;
                            if (result3 == DialogResult.Yes)
                            {
                                ErrorWindow frm2 = new ErrorWindow(mssg
                                , ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]], "Error @Import", false, false, true, "", "", "");
                                frm2.ShowDialog();
                            }
                        }
                    }
                    else
                    if (result1 == DialogResult.Cancel)
                    {
                        ErrorWindow frm1 = new ErrorWindow(mssg
                            , ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]], "Error @Import", false, false, true, "", "", "");
                        frm1.ShowDialog();

                    }
                    else
                        try
                        {
                            //xx = "cmd /C " + xx;
                            //Process process = Process.Start(@xx);
                            StartProcesss(@xx, null);
                        }
                        catch (Exception exx)
                        {
                            if (File.Exists(xx))
                                ;// GeneralExtension.RunExternalExecutable(xx, false);
                            var startInfo = new ProcessStartInfo
                            {
                                FileName = xx,
                                WorkingDirectory = Path.GetDirectoryName(xx)
                            };
                            Process DDC = new Process();
                            //startInfo.Arguments = "";
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = false;

                            if (File.Exists(xx))
                            {
                                DDC.StartInfo = startInfo;
                                DDC.Start(); DDC.WaitForExit(1000 * 30 * 1); //wait 1min
                            }
                            var tsst = "Erro0 ..." + exx; var timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Local Access plugin install ! " + xx);
                        }
                }

                //if (lbl != null)
                //{
                //    lbl.Text = "missing Access plugin!";
                //    lbl.Visible = true;
                //}
            }
            else
            {
                var timestamp = UpdateLog(DateTime.Now, "Error ..." + txt + ex, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }

        static public string GetExtraAttributes(string origFN, string noMFN, string gom, string SongDisplayName, string Album, string PackageAuthor, string Name, string Artist)
        {
            var Is_MultiTrack = ""; var MultiTrack_Version = "";
            var IsLive = ""; var LiveDetails = ""; var IsAcoustic = ""; var IsSingle = ""; var IsSoundtrack = "";
            var IsInstrumental = ""; var IsEP = ""; var IsUncensored = ""; var IsFullAlbum = ""; var IsRemastered = ""; var InTheWorks = "";
            var IsKaraoke = ""; var IsDemo = ""; var HasFeaturing = ""; var IsRemix = ""; var IsCover = ""; var IsMultiStrings = ""; var IsMedley = "";
            var Titl = "";
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
            multxt = "Single"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSingle = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "(EP)"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "(EP)"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; Album = Titl.Split(';')[0]; }
            //multxt = " EP "; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            //multxt = " EP "; Titl = Check4MultiT(origFN, Album, multxt, multibool); if ("Yes" == Titl.Split(';')[1]) { IsEP = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack from the Motion Picture"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack from the Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Soundtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "The Original Motion Picture Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Soundtrack"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Original Game Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "(movie ver.)"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Soundtrack"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Original Motion Picture"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            if (Album.ToLower().Contains(" ost") || Album.ToLower().Contains("(ost") || Album.ToLower().Substring(0, 2) == "ost")
            { multxt = "OST"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; } }
            if (SongDisplayName.ToLower().Contains(" ost") || SongDisplayName.ToLower().Contains("(ost"))
            { multxt = "OST"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; } }
            multxt = " Theme"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = " Theme"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsSoundtrack = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Uncensored"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsUncensored = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "FullAlbum"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsFullAlbum = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Full Album"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsFullAlbum = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remastered"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemastered = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Karaoke Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsKaraoke = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Karaoke"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsKaraoke = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Demo"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsDemo = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Demo"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsDemo = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Extended Remixed"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Extended Remixed"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remixed"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; Album = Titl.Split(';')[0]; }
            multxt = "Remixed"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Remix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Extended Mix"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Extended Mix"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Extended"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsRemix = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
            multxt = "Cover"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsCover = "Yes"; /*SongDisplayName = Titl.Split(';')[0];*/ }
            multxt = "Medley"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album"); if ("Yes" == Titl.Split(';')[1]) { IsMedley = "Yes"; Album = Titl.Split(';')[0]; }
            multxt = "Medley"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName"); if ("Yes" == Titl.Split(';')[1]) { IsMedley = "Yes"; SongDisplayName = Titl.Split(';')[0]; }
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
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6)
            { IsLive = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }
            multxt = "Unplugged"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            { IsLive = "Yes"; IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }
            multxt = "Live"; Titl = Check4MultiT(origFN, Album, multxt, false, "Album");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 4)
            { IsLive = "Yes"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Album.IndexOf(multxt) + 4), ""); }
            multxt = "Unplugged"; Titl = Check4MultiT(origFN, Album, multxt, multibool, "Album");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            { IsLive = "Yes"; IsAcoustic = "Yes"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Album.IndexOf(multxt) + 4), ""); }

            //Detect Featuring
            multxt = "Feat."; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 5)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Feat.", "[Ft.").Replace(" feat.", "[Ft.")).Replace("(Feat.", "[Ft.").Replace("(feat.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            multxt = "Ft."; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 3)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Ft.", "[Ft.").Replace(" ft.", "[Ft.")).Replace("(Ft.", "[Ft.").Replace("(ft.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            multxt = "Featuring"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, false, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            {
                HasFeaturing = "Yes"; SongDisplayName = (SongDisplayName.Replace(" Featuring", "[Ft.").Replace(" featuring", "[Ft.")).Replace("(Featuring", "[Ft.").Replace("(featuring", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            multxt = "Feat."; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 5)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Feat.", "[Ft.").Replace(" feat.", "[Ft.")).Replace("(Feat.", "[Ft.").Replace("(feat.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            multxt = "Ft."; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 3)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Ft.", "[Ft.").Replace(" ft.", "[Ft.")).Replace("(Ft.", "[Ft.").Replace("(ft.", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            multxt = "Featuring"; Titl = Check4MultiT(origFN, Artist, multxt, false, "Artist");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 9)
            {
                HasFeaturing = "Yes"; Artist = (Artist.Replace(" Featuring", "[Ft.").Replace(" featuring", "[Ft.")).Replace("(Featuring", "[Ft.").Replace("(featuring", "[Ft.")
                      + "]"; LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, Artist.IndexOf(multxt) + 4), "");
            }/*(Titl.Split(';')[0].TrimEnd().TrimStart())*/

            if (SongDisplayName.IndexOf("Rocker") >= 0)
                ;
            //Detect Acoustic
            multxt = "Acoustic Version"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6)
            { IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }

            multxt = "Acoustic"; Titl = Check4MultiT(origFN, SongDisplayName, multxt, multibool, "SongDisplayName");
            if ("Yes" == Titl.Split(';')[1] && Titl.Split(';')[0].Length > 6)
            { IsAcoustic = "Yes"; SongDisplayName = Titl.Split(';')[0].TrimEnd().TrimStart().Replace(" ()", ""); LiveDetails += gom.IndexOf(multxt) <= gom.Length - 4 ? "" : gom.Replace(gom.Substring(0, SongDisplayName.IndexOf(multxt) + 4), ""); }
            //var r = "";

            return Is_MultiTrack + ";" + MultiTrack_Version + ";" + IsLive + ";" + LiveDetails + ";" + IsAcoustic + ";" + IsSingle + ";" + IsSoundtrack + ";" + IsInstrumental + ";" + IsEP + ";" + IsUncensored + ";" + IsFullAlbum + ";"
                + IsRemastered + ";" + InTheWorks + ";" + IsKaraoke + ";" + IsDemo + ";" + HasFeaturing + ";" + IsRemix + ";" + IsCover + ";" + SongDisplayName + ";" + Album + ";" + IsMedley + ";" + IsMultiStrings;
        }




        public static void CreatePackingGroup(string cmd, OleDbConnection cnb, string filter, int norows, SQLite.SQLiteConnection cnc)
        {
            //var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DateTime timestamp;
            timestamp = UpdateLog(DateTime.Now, "Deleting All Packing groups & inserintg newly " + norows, true, null, null, "", null, null);
            if (filter == "Packing") return;
            DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type = \"DLC\" AND Groupz = \"Packing\"", cnb, cnc);

            string insertcmd;
            try
            {
                DataSet dsm = new DataSet();
                insertcmd = "INSERT INTO Groups (CDLC_ID, Groupz, Type, Comments, Date_Added) " + cmd.Replace("*", "ID, \"Packing\", \"DLC\", \"89\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"") + ";";
                OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                dab.Fill(dsm, "Groups");
                dab.Dispose();
            }
            catch (Exception ee)
            {
                ShowConnectivityError(ee, "");/*, null*/
            }
        }

        //public static string GetFilter(string Filtertxt, string SearchCmd, int i, string Searchcmdf, OleDbConnection cnb, string Group, string chbx_Format, string Import_Date, SQLiteConnection cnz)
        public static string GetFilter(string Filtertxt, string SearchCmd, int i, string Searchcmdf, OleDbConnection cnb, string Group, string chbx_Format, string Import_Date, SQLite.SQLiteConnection cnc)
        {
            var oldfilter = Filtertxt;
            var SearchCmdf = "";
            if (new[] { "Sorted by Groups value/Group added date" }.Contains(Filtertxt)) SearchCmdf = SearchCmd;/*, "Part of No Group", "Part of Any Group" */


            var oldSearchCmd = SearchCmd;
            SearchCmd = SearchCmd.Length == 0 ? "SELECT * FROM Main " : SearchCmd.Substring(0, (SearchCmd.IndexOf(" WHERE") - 1) > 0 ? (SearchCmd.IndexOf(" WHERE") - 1) : ((SearchCmd.IndexOf(" ORDER") - 1) > 0 ? (SearchCmd.IndexOf(" ORDER") - 1) : SearchCmd.Length - 1));
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
                case true when Filtertxt == "Same (imported/old) File Name":
                    //SLOW SearchCmd += "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    //var SearchCmd52 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.Original_FileName) = LCASE(Main_1.Original_FileName) AND Main.ID<> Main_1.ID";
                    DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT m.ID,Original_FileName FROM Main AS m ORDER BY LCASE(Original_FileName)", "", cnb, cnc);
                    noOfRec = dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
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
                    noOfRec = dvs.Tables.Count == 0 ? 0 : dvs.Tables[0].Rows.Count;
                    var IDb = "";
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dvs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dvs.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (CleanTitle(dvs.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitle(dvs.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
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
                    noOfRec = dgc.Tables.Count == 0 ? 0 : dgc.Tables[0].Rows.Count;
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
                case true when Filtertxt == "with Errors at Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID FROM LogPackingError)";
                    break;
                case true when Filtertxt == "with Errors at Last Packing":
                    SearchCmd += " ID IN (SELECT CDLC_ID from LogPackingError WHERE Pack=(SELECT TOP 1 Pack from LogPackingError GROUP BY Pack ORDER BY val(Pack) DESC))";
                    break;
                case true when Filtertxt == "Imported Last":
                    DataSet dds = new DataSet(); dds = SelectFromDB("Main", "SELECT top 1 Pack FROM Main order by ID DESC;", "", cnb, cnc);
                    noOfRec = dds.Tables[0].Rows.Count;
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
                    noOfRec = dzs.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPacking WHERE Pack=\"" + dzs.Tables[0].Rows[0].ItemArray[0].ToString() + "\")";
                    else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Packing Errors":
                    DataSet dks = new DataSet(); dks = SelectFromDB("LogPackingError", "SELECT top 1 Pack FROM LogPackingError order by ID DESC;", "", cnb, cnc);

                    noOfRec = dks.Tables[0].Rows.Count;
                    if (noOfRec > 0)
                        SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPackingError WHERE Pack=\"" + dks.Tables[0].Rows[0].ItemArray[0].ToString() + "\")";
                    else SearchCmd += "1 = 2";
                    break;
                case true when Filtertxt == "Same DLCName":
                    //SLOW var SearchCmd5 = "SELECT Main.ID FROM Main INNER JOIN Main AS Main_1 ON LCASE(Main.DLC_Name) = LCASE(Main_1.DLC_Name) AND Main.ID <> Main_1.ID";
                    DataSet dos = new DataSet(); dos = SelectFromDB("Main", "SELECT m.ID,DLC_Name FROM Main AS m ORDER BY LCASE(DLC_Name)", "", cnb, cnc);
                    noOfRec = dos.Tables.Count == 0 ? 0 : dos.Tables[0].Rows.Count;
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
                    noOfRec = dqs.Tables.Count == 0 ? 0 : dqs.Tables[0].Rows.Count;
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
                    noOfRec = dns.Tables.Count == 0 ? 0 : dns.Tables[0].Rows.Count;
                    var IDf = "";/* bool done = false;*/
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                        {
                            for (var v = l + 1; v < noOfRec; v++)
                            {
                                if (dns.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dns.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitle(dns.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitle(dns.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
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
                    noOfRec = das.Tables.Count == 0 ? 0 : das.Tables[0].Rows.Count;
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
                    noOfRec = dws.Tables.Count == 0 ? 0 : dws.Tables[0].Rows.Count;
                    var IDc = ""; /*var dones = false;*/
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                if (dws.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dws.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                    if (CleanTitle(dws.Tables[0].Rows[l].ItemArray[2].ToString().ToLower()) == CleanTitle(dws.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()))
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
                    noOfRec = dts.Tables.Count == 0 ? 0 : dts.Tables[0].Rows.Count;
                    var IDe = ""; var donez = false;
                    if (noOfRec > 0)
                        for (var l = 0; l < noOfRec; l++)
                            for (var v = l + 1; v < noOfRec; v++)
                                //{
                                if (dts.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dts.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                                {
                                    if (CleanTitle(dts.Tables[0].Rows[l].ItemArray[2].ToString()) == CleanTitle(dts.Tables[0].Rows[v].ItemArray[2].ToString()))
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
                        DialogResult result1 = MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        DialogResult result1 = MessageBox.Show("Please Select the group to Filter out songs on.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }
                    var SearchCmd11 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz <> \"" + Group + "\" AND Groupz <>\"Default\"";
                    SearchCmd += "ID IN (" + SearchCmd11 + ")";
                    break;
                case true when Filtertxt == "Part of the selected below and Others ignoring Default":
                    if (Group == "")
                    {
                        DialogResult result1 = MessageBox.Show("Please Select the group to Filter out songs on.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchCmd = SearchCmdf;
                        return "";
                    }

                    var SearchCmdj1 = "SELECT DISTINCT VAL(CDLC_ID) FROM Groups WHERE Type=\"DLC\" AND Groupz = \"" + Group + "\"";
                    DataSet dys = new DataSet(); dys = SelectFromDB("Main", SearchCmdj1, "", cnb, cnc);
                    var noOftRec = dys.Tables.Count == 0 ? 0 : dys.Tables[0].Rows.Count;

                    DataSet dbs = new DataSet(); dbs = SelectFromDB("Main", "SELECT CDLC_ID FROM vw_CountGroups WHERE vw_CountGroups.CountGrp>1", "", cnb, cnc);
                    noOfRec = dbs.Tables.Count == 0 ? 0 : dbs.Tables[0].Rows.Count;
                    var IDw = ""; var doney = false;
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
                        DialogResult result1 = MessageBox.Show("Please Select the group to be sorted by Added_Date about?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        SearchCmd = "SELECT u.ID FROM Main u LEFT JOIN Groups AS m ON m.CDLC_ID = CSTR(u.ID) WHERE m.Groupz =  \"" + Filtertxt.Substring(6, Filtertxt.Length - 6).Trim() + "\"";// + ")";
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
            noOfRec = dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
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
            string tecst = "";
            string line;
            //var header = "";
            var linedone = true;
            var songkey = "";
            //var footer = "";
            var lastline = false; //if the last song is not removed then the end should be appended

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
            var norec = ds.Tables.Count < 1 ? 0 : ds.Tables[0].Rows.Count;
            if (norec > 0)
            {
                var fnn = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                var cmd = "";
                //saving  connfig values to dba

                var norecs = 0;
                DataSet dsg = new DataSet(); dsg = SelectFromDB("Groups", "SELECT DISTINCT Comments, Groupz, ID FROM Groups WHERE Type=\"Profile\" AND Profile_Name=\"" + oldprof + "\"; ", "", cnb, cnc);/*c("dlcm_Configurations")*/
                norecs = dsg.Tables[0].Rows.Count; var rt = 0; var t = ""; var tt = "";
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
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
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
                            tzt = SongRecord[k].Author;
                            break;
                        case "<Version>":
                            tzt = SongRecord[k].Version;
                            break;
                        case "<DLCName>":
                            tzt = SongRecord[k].DLC_Name;
                            break;
                        case "<Track No.>":
                            tzt = ((SongRecord[k].Track_No != "" || SongRecord[k].Track_No != "0") ? ("-" + SongRecord[k].Track_No) : "");
                            break;
                        case "<Year>":
                            tzt = SongRecord[k].Album_Year;
                            break;
                        case "<Rating>":
                            tzt = ((SongRecord[k].Rating == "") ? "" : "r." + SongRecord[k].Rating);
                            break;
                        case "<Alt. Vers.>":
                            tzt = SongRecord[k].Alternate_Version_No == "" ? "" : "a." + SongRecord[k].Alternate_Version_No;
                            break;
                        case "<Descr.>":
                            tzt = SongRecord[k].Description;
                            break;
                        case "<Comm.>":
                            tzt = SongRecord[k].Comments;
                            break;
                        case "<Tuning>":
                            tzt = SongRecord[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            tzt = ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((SongRecord[k].Has_Bass == "Yes") ? "B" : ""); //not yet done for all arrangements
                            break;
                        case "<MTrack Det.>":
                            tzt = SongRecord[k].MultiTrack_Version == "" ? "" : "-MultiTrack " + SongRecord[k].MultiTrack_Version;//?
                            break;
                        case "<Group>":
                            tzt = SongRecord[k].Groups;
                            break;
                        case "<Groups>":
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Group", "SELECT Groupz,Comments FROM Groups WHERE CDLC_ID=\"" + SongRecord[k].ID + "\" AND Type=\"DLC\" ORDER BY Groupz,Comments", "", cnb, cnc);
                            var noOfRect = dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;

                            for (var j = 0; j <= noOfRect - 1; j++)
                            {
                                var grp = dvs.Tables[0].Rows[j].ItemArray[0].ToString() + " ";
                                tzt += grp;
                            }
                            break;
                        case "<GroupIndex>":
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRehc = dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;
                            if (noOfRehc > 0) tzt = dbs.Tables[0].Rows[0].ItemArray[0].ToString();
                            break;
                        case "<GroupIndexAndName>":
                            DataSet dqs = new DataSet(); dqs = SelectFromDB("Groups", "SELECT TOP 1 Groupz, Comments FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRehq = dqs.Tables.Count > 0 ? dqs.Tables[0].Rows.Count : 0;
                            if (noOfRehq > 0) tzt = dqs.Tables[0].Rows[0].ItemArray[0].ToString() + dqs.Tables[0].Rows[0].ItemArray[1].ToString();
                            break;
                        case "<FirstGroupIndexAndName>":
                            DataSet dps = new DataSet(); dps = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + SongRecord[k].ID + "\" ORDER BY Comments", "", cnb, cnc);
                            var noOfRepq = dps.Tables.Count > 0 ? dps.Tables[0].Rows.Count : 0;
                            if (noOfRepq > 0) tzt = dps.Tables[0].Rows[0].ItemArray[0].ToString() + dps.Tables[0].Rows[0].ItemArray[1].ToString();
                            break;
                        case "<BetaOrGroupIndex>":
                            DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT TOP 1 Comments,Groupz FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + SongRecord[k].Groups + "\" ORDER BY Comments,Groupz", "", cnb, cnc);
                            var noOfRegc = dgs.Tables.Count > 0 ? dgs.Tables[0].Rows.Count : 0;
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
                        case "<IntheWorks>":
                            tzt = ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                            break;
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
                        case "<Lyrics Language>":
                            tzt = SongRecord[k].LyricsLanguage + " ";
                            break;
                        case "<Track version>":
                            tzt = ((SongRecord[k].Is_Acoustic == "Yes") ? "-Acoustic " + SongRecord[k].Live_Details : "");
                            tzt += ((SongRecord[k].Is_Live == "Yes") ? "-Live " : "");
                            tzt += ((SongRecord[k].Is_Instrumental == "Yes") ? "-Instrumental " : "");
                            tzt += ((SongRecord[k].Is_EP == "Yes") ? "-EP " : "");
                            tzt += ((SongRecord[k].Is_Uncensored == "Yes") ? "-Uncensored " : "");
                            tzt += ((SongRecord[k].Is_Soundtrack == "Yes") ? "-SoundTrack " : "");
                            tzt += ((SongRecord[k].Is_Single == "Yes") ? "-Single " : "");
                            tzt += ((SongRecord[k].Is_FullAlbum == "Yes") ? "-FullAlbum  " : "");
                            tzt += ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                            tzt += ((SongRecord[k].Is_Remastered == "Yes") ? "-Remastered " : "");
                            tzt += ((SongRecord[k].Is_Karaoke == "Yes") ? "-Karaoke " : "");
                            tzt += ((SongRecord[k].Is_Cover == "Yes") ? "-Cover " : "");
                            tzt += ((SongRecord[k].Is_Demo == "Yes") ? "-Demo  " : "");
                            tzt += ((SongRecord[k].Is_Remix == "Yes") ? "-Remix " : "");
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
                            var noOfReoc = dos.Tables[0].Rows.Count;

                            for (var j = 0; j <= noOfReoc - 1; j++)
                            {
                                var CapoFret = dos.Tables[0].Rows[j].ItemArray[0].ToString();
                                tzt = "CapoOn-" + CapoFret + " ";
                                break;
                            }
                            break;
                        case "<DigitechDropFlag>":
                            DataSet dys = new DataSet(); dys = SelectFromDB("Arrangements", "SELECT Tunning, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRekc = dys.Tables[0].Rows.Count;

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
                            var noOfRenc = dns.Tables[0].Rows.Count;

                            for (var j = 0; j <= noOfRenc - 1; j++)
                            {
                                var Tunning = dns.Tables[0].Rows[j].ItemArray[0].ToString();
                                var ArrangementType = dns.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dns.Tables[0].Rows[j].ItemArray[4].ToString();
                                var Arrangement_Name = dns.Tables[0].Rows[j].ItemArray[7].ToString();
                                if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                                var instr = RouteMask == "Bass" ? " B-" : (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C-" : (RouteMask == "Rhythm" ? " R-" : (RouteMask == "Lead" ? " L-" : "?")));
                                tzt = instr + GetDropTunningInstr(Tunning);
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
                            DataSet dcs = new DataSet(); dcs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRezc = dcs.Tables[0].Rows.Count;
                            //float FirstLyric = 5000;
                            //float FirstVocal = 0;
                            var B = ""; var L = ""; var R = ""; var C = "";

                            for (var j = 0; j <= noOfRezc - 1; j++)
                            {
                                var Bonus = dcs.Tables[0].Rows[j].ItemArray[1].ToString();
                                var ArrangementType = dcs.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dcs.Tables[0].Rows[j].ItemArray[4].ToString();
                                var Arrangement_Name = dcs.Tables[0].Rows[j].ItemArray[7].ToString();
                                if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                                if (Arrangement_Name == "2" || Arrangement_Name == "Combo") C = "C";
                                else if (RouteMask == "Bass") B = "B";
                                else if (RouteMask == "Lead") L = "L";
                                else if (RouteMask == "Rhythm") R = "R";

                                if (Bonus.ToLower() == "true")
                                {
                                    if (RouteMask == "Bass") B += "b";
                                    if (Arrangement_Name == "2" || Arrangement_Name == "Combo") C += "b";
                                    else
                                    {
                                        if (RouteMask == "Lead") L += "b";
                                        if (RouteMask == "Rhythm") R += "b";
                                    }
                                }
                            }
                            tzt = L + B + R + C;
                            break;
                        case "<Avail. Tracks w Favorite>":
                            DataSet dks = new DataSet(); dks = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Favorite, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRezk = dks.Tables[0].Rows.Count; var Bk = ""; var Lk = ""; var Rk = ""; var Ck = "";

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
                        case "<Bass_HasDD>":
                            tzt = ((SongRecord[k].Bass_Has_DD == "No" || bassRemoved) && SongRecord[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        case "<Avail. Tracks and Timings>":
                            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRec = dus.Tables[0].Rows.Count;
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

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C" + p + shortstart : "") + (RouteMask == "Bass" ? " B" + p + shortstart : "") + (RouteMask == "Rhythm" ? " R" + p + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + p + shortstart : ((RouteMask == "Lead") ? " L" + p + shortstart : "")));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings&Bonus>":
                            DataSet dxp = new DataSet(); dxp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb, cnc);
                            var noOfRecc = dxp.Tables.Count > 0 ? (string.IsNullOrEmpty(dxp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dxp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecv = dxs.Tables[0].Rows.Count;
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
                            DataSet dbp = new DataSet(); dbp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecb = dbp.Tables.Count > 0 ? (string.IsNullOrEmpty(dbp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dbp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dts = new DataSet(); dts = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Favorite FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecm = dts.Tables[0].Rows.Count;
                            for (var j = 0; j <= noOfRecm - 1; j++)
                            {
                                var XMLFilePath = dts.Tables[0].Rows[j].ItemArray[0].ToString();
                                var Bonus = dts.Tables[0].Rows[j].ItemArray[1].ToString();
                                var Commentz = dts.Tables[0].Rows[j].ItemArray[2].ToString();
                                var ArrangementType = dts.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dts.Tables[0].Rows[j].ItemArray[4].ToString();
                                var StartTime = dts.Tables[0].Rows[j].ItemArray[5].ToString();
                                var Part = dts.Tables[0].Rows[j].ItemArray[6].ToString().ToLower() == "yes" ? "p" : "";
                                var Favorite = dts.Tables[0].Rows[j].ItemArray[7].ToString().ToLower();
                                var Arrangement_Name = dts.Tables[0].Rows[j].ItemArray[8].ToString();
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                var b = ""; var p = ""; var f = "";
                                if (Bonus.ToLower() == "true") b = "b";
                                if (Favorite.ToLower() == "yes") b = "f";
                                if (noOfRecb > 1) p = Part;

                                tzt += (Arrangement_Name == "2" || Arrangement_Name == "Combo" ? " C" + b + p + f + shortstart : "") + (RouteMask == "Bass" ? " B" + b + p + f + shortstart : "") +
                                    (RouteMask == "Rhythm" ? " R" + b + p + f + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + b + p + f + shortstart
                                    : ((RouteMask == "Lead") ? " L" + b + p + f + shortstart : "")));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings>":
                            DataSet dsp = new DataSet(); dsp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecPS = dsp.Tables.Count > 0 ? (string.IsNullOrEmpty(dsp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dsp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part, Arrangement_Name FROM Arrangements WHERE CDLC_ID="
                                + SongRecord[k].ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var noOfRecS = dss.Tables[0].Rows.Count;
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
                                    case "<QAs>":
                                        tzt = (((SongRecord[k].Has_Cover == "No") || (SongRecord[k].Has_Preview == "No") || (SongRecord[k].Has_Vocals == "No"))
                                            ? "NOs-" : "") + ((SongRecord[k].Has_Cover == "Yes") ? "" : "C") + ((SongRecord[k].Has_Preview == "Yes") ? "" : "P")
                                            + ((SongRecord[k].Has_Vocals == "Yes") ? "" : "V") + ((SongRecord[k].Is_Broken == "Yes") ? " Broken" : "")
                                            + ((SongRecord[k].FilesMissingIssues != "") ? "_w FilesMissingIssues" : "");
                                        break;
                                    case "<LastConversionDateTime>":
                                        tzt = SongRecord[k].LastConversionDateTime;
                                        break;
                                    default: break;
                                }
                            }
                            break;
                    }
                    if (tzt != "")
                        //if (SongRecord[0].Album.ToLower().IndexOf("live at") < 0 || (curelem != "<Live>"))
                        // fulltxt = fulltxt.IndexOf(sep1) > 0 ? fulltxt.Replace(tzt.Replace(sep1, "").Replace(sep2, ""), "") + tzt : fulltxt + tzt;
                        //else
                        fulltxt += tzt;

                    if (oldtxt == fulltxt && last_ > 0) fulltxt = fulltxt.Substring(0, last_);
                    last_ = fulltxt.Length;
                }
            }

            if ((sep1 + sep2).Length > 0) return (fulltxt.Trim()).Replace(sep1 + sep2, "").Replace(sep1 + " " + sep2, "").Replace(sep1 + "-", sep1).Replace("-" + sep2, sep2).Replace(sep1 + " ", sep1).Replace(" " + sep2, sep2).Replace("--", "-").Replace("  ", " ").Replace("]-", "-").Replace("] -", "-");
            // return (fulltxt + sep2).Replace(sep1 + sep2, "").Replace(sep1 + "-", sep1).Replace("-" + sep2, sep2).Replace(sep1 + " ", sep1).Replace(" " + sep2, sep2);
            else return fulltxt.Trim().Replace("- ", "-");/*'-'*/
        }

        //public static void DeleteRecords(string IDs, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb, ProgressBar pB_ReadDLCs, SQLiteConnection cnz)
        public static void DeleteRecords(string IDs, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb, ProgressBar pB_ReadDLCs, SQLite.SQLiteConnection cnc)
        {
            //Delete records
            DialogResult result1 = MessageBox.Show(norows + " of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("), "", cnb, cnc);
                var rcount = dhs.Tables[0].Rows.Count;
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
                for (var i = 0; i < rcount; i++)
                {
                    if (dhs.Tables[0].Rows[i].ItemArray[120].ToString() == "Yes")
                    {
                        DialogResult result11 = MessageBox.Show("This Song " + dhs.Tables[0].Rows[i].ItemArray[4].ToString() + " - " + dhs.Tables[0].Rows[i].ItemArray[1].ToString()
                            + "was (marked as) manipulated" + (dhs.Tables[0].Rows[i].ItemArray[117].ToString() == "Yes" ? "&intheworks" : "")
                            + " with DLC Manager are you sure you want it to be deleted?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result11 == DialogResult.No) continue;
                    }
                    if (dhs.Tables[0].Rows[i].ItemArray[117].ToString() == "Yes")
                    {
                        DialogResult result11 = MessageBox.Show("This Song " + dhs.Tables[0].Rows[i].ItemArray[4].ToString() + " - " + dhs.Tables[0].Rows[i].ItemArray[1].ToString()
                            + "was marked as in the Works with DLC Manager are you sure you want it to be deleted?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result11 == DialogResult.No) continue;
                    }
                    psarcPath += (File.Exists(TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString()) ? "" : ", " + TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString());
                    cmmd = cmd.Replace(dhs.Tables[0].Rows[i].ItemArray[19].ToString(), "");
                }

                if (cmd != cmmd)
                {
                    DialogResult resultgf = MessageBox.Show(psarcPath + " have missing original files. Are you sure you want the records removed as atm you could still generate the songs?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultgf == DialogResult.No) cmd = cmmd.Replace(", ,", ",").Replace(",,", ",").Replace(", )", ")").Replace(",)", ")");
                }

                DialogResult resultf = MessageBox.Show(norows + "Do you wanna have Audit Trail of these to be deletes songs, deleted too, as to import them again?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultf == DialogResult.Yes)
                {
                    // //Delete Audit trail of import
                    DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + cmd.Replace("ID IN (SELECT ID ", "FileHash IN (SELECT FileHash ") + "\")", cnb, cnc);
                }

                pB_ReadDLCs.Maximum = rcount;
                pB_ReadDLCs.Step = 1;
                pB_ReadDLCs.Value = 1;
                for (var i = 0; i < rcount; i++)
                {
                    pB_ReadDLCs.Increment(1);
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    DeleteDirectory(filePath, false);

                    //Move psarc file to Duplicates                        
                    string psarcPathh = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    var fh = GetHash(psarcPathh);
                    psarcPathh = CopyMoveFileSafely(psarcPathh, psarcPathh.Replace("0_old", "0_archive"), false, fh, false);
                }

                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb, cnc);

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

                MessageBox.Show(rcount + "  Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        MessageBox.Show("Errors at DD lvl READ removal");
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
                                        MessageBox.Show("Errors at DD time notes READ removal");
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
                                        MessageBox.Show("Errors at DD time anchor READ removal");
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
                                                DeleteDirectory(templateDir, false);
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
                if (cnb.State.ToString() == "Open") cnb.Open();

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
                        SourcePlatform = new Platform(oldfilePath.GetPlatform().platform.ToString(), GameVersion.RS2014.ToString());
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
                if (((chbx_Last_Packed && chbx_Last_PackedEnabled) && !(chbx_CopyOld && chbx_CopyOldEnabled)) || (!File.Exists(h) || h == ""))
                {
                    DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+\"\\\"+FileName FROM Pack_AuditTrail WHERE Platform=\"" + chbx_Format + "\" and CDLC_ID=" + ID + " ORDER BY ID DESC;", "", cnb, cnc);
                    rec = dvr.Tables[0].Rows.Count;
                    if (rec > 0) h = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                if ((!(chbx_Last_Packed && chbx_Last_PackedEnabled) || (chbx_Last_Packed && chbx_Last_PackedEnabled && rec == 0)) && !(chbx_CopyOld && chbx_CopyOldEnabled) || (!File.Exists(h) || h == ""))
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
                        Platform platformz = Folder_Name.GetPlatform(); string[] xmlFilez = new string[30000]; bool done = true; var countd = 0;
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

                        foreach (var xml in xmlFilez)
                        {
                            if (xml.ToLower().IndexOf("showlights") < 0 && xml.ToLower().IndexOf("vocals") < 0 && c("dlcm_AdditionalManipul102") != "Yes" && xml != null)
                                try
                                {
                                    Song2014 xmlContent = null;
                                    xmlContent = Song2014.LoadFromFile(xml);
                                    if (xmlContent.Arrangement.ToLower() == "bass" && !(xml.IndexOf(".old") > 0))
                                    {
                                        if ((ConfigRepository.Instance()["dlcm_AdditionalManipul3"] == "Yes" || chbx_RemoveBassDD) && chbx_BassDD && (!(chbx_KeepBassDD && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") && !(chbx_KeepDD && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes")))

                                        {
                                            bassRemoved = (RemoveDD(Folder_Name, chbx_Original, xml, platformz, false, false, chbx_UseInternalDD) == "Yes") ? true : false;
                                            timestamp = UpdateLog(timestamp, "Removing Bass.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);
                                        }
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

                            var noOfRec = dus.Tables[0].Rows.Count;
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
                            MessageBox.Show("Are you done with reading the Lyrics file?.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // LOAD DATA
                        timestamp = UpdateLog(timestamp, "Loading song.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);
                        //verify if too many audios
                        var xmlFil = Directory.GetFiles(filez.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFil) if (xml != filez.AudioPath && xml != filez.audioPreviewPath) DeleteFile(xml, false);
                        var xmlFi = Directory.GetFiles(filez.Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFi) if (xml != filez.OggPath && xml != filez.oggPreviewPath) DeleteFile(xml, false);

                        var info = DLCPackageData.LoadFromFolder(filez.Folder_Name, packagePlatform);

                        var xmlFiles = Directory.GetFiles(filez.Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                        var platform = filez.Folder_Name.GetPlatform();
                        float volume = float.Parse(filez.Volume.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                        float volumep = float.Parse(filez.Preview_Volume, NumberStyles.Float, CultureInfo.CurrentCulture);


                        data = new DLCPackageData
                        {
                            GameVersion = GameVersion.RS2014,
                            Pc = filez.Platform == "Pc" ? true : false,
                            Mac = filez.Platform == "Mac" ? true : false,
                            XBox360 = filez.Platform == "Xbox360" ? true : false,
                            PS3 = filez.Platform == "Ps3" ? true : false,
                            Name = filez.DLC_Name,
                            AppId = filez.DLC_AppID,
                            ArtFiles = info.ArtFiles, //not complete
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
                            OggPath = filez.AudioPath,
                            OggPreviewPath = ((filez.audioPreviewPath != "") ? filez.audioPreviewPath : filez.AudioPath),
                            //OggPath = filez.OggPath,
                            //OggPreviewPath = ((filez.oggPreviewPath != "") ? filez.oggPreviewPath : filez.OggPath),
                            Arrangements = info.Arrangements,
                            Tones = info.Tones,
                            TonesRS2014 = info.TonesRS2014,
                            Volume = volume,
                            PreviewVolume = volumep,
                            SignatureType = info.SignatureType
                        };

                        //IF Vocals have been added  but Repack is set not to consider them
                        if (updateTonesArrangs)
                        {
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Tones_GearList", "SELECT * FROM Tones_GearList WHERE Tone_ID in (SELECT ID FROM Tones WHERE CDLC_ID=" + ID + GetArrOfficSQLTxt(arrangoff) + ");", "", cnb, cnc);
                            var norecx = dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;

                            if (norecx == 0 && info.TonesRS2014.Count != 0) updateTonesArrangs = false;// MessageBox.Show("Vocals not included as added in the DLCManager tool, but Option 76 is Unselected ergo no DLCManager-DB changes are considered at packing");
                            else updateTonesArrangs = true;
                        }//else if (ConfigRepository.Instance()["dlcm_AdditionalManipul76"].ToLower() == "yes") 

                        //IF Vocals have been added  but Repack is set not to consider them
                        if (!updateTonesArrangs)
                        {
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + " AND ArrangementType=\"Vocal\"" + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
                            var norec = dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;
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
                                        norec = dfs.Tables[0].Rows.Count;
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
                                                nrc = dsc.Tables[0].Rows.Count; tz++;//2
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
                                                nrc = dsa.Tables[0].Rows.Count; tz++;//3
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
                                                nrc = dss1.Tables[0].Rows.Count; tz++;//4
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
                                                nrc = dss2.Tables[0].Rows.Count; tz++;//5
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
                                                nrc = dss3.Tables[0].Rows.Count; tz++;//6
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
                                                nrc = dss4.Tables[0].Rows.Count; tz++; //7
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
                                                nrc = dsp1.Tables[0].Rows.Count; tz++;//8
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
                                                nrc = dsp2.Tables[0].Rows.Count; tz++;//9
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
                                                nrc = dsp3.Tables[0].Rows.Count; tz++;//10
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
                                                nrc = dsp4.Tables[0].Rows.Count; tz++;//11
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
                                                nrc = dsr1.Tables[0].Rows.Count; tz++;//12
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
                                                nrc = dsr2.Tables[0].Rows.Count; tz++;//13
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
                                                nrc = dsr3.Tables[0].Rows.Count; tz++;//14
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
                                                nrc = dsr4.Tables[0].Rows.Count; tz++;//15
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
                                norec = ds.Tables[0].Rows.Count;
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
                                SongRecord[0].Track_No = trackno.ToString("D2");
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
                            var d1 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
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
                                }

                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && info.OggPath != null)
                                {
                                    cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, OggPath, oggPreviewPath  FROM Main " +
                                        "WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                    cmd += " AND ID=" + ID;
                                    FixAudioIssues(cmd, cnb, AppWD, null, null, false, windw, cnc);
                                }

                            }
                            if (d1.Split(';')[2] == "1") { System.Windows.Forms.Application.Exit(); }
                        }

                        //Fix bug
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes")
                        {
                            tsst = "framework bug 1 time fixe"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                            UtilitiesFunctions.Converters(filez.oggPreviewPath, UtilitiesFunctions.ConverterTypes.Ogg2Wem, false, false);
                            if (File.Exists(filez.oggPreviewPath.Replace(".ogg", ".wem")))
                            {
                                //fix as sometime the template folder gets poluted and breaks eveything
                                var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                var templateDir = Path.Combine(appRootDir, "Template");
                                var backup_dir = AppWD + "\\Template";
                                DeleteDirectory(templateDir, false);
                                CopyFolder(backup_dir, templateDir);
                            }
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_fixed.wav"), false);
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview_fixed.wav"), false);
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview_fixed.ogg"), false);
                            //tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul71"] == "Yes")
                        {
                            //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
                            //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
                            //tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }

                        tsst = "Medata data manipulating"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        data.ToolkitInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo
                        {
                            PackageAuthor = filez.Author
                        };
                        SongRecord[0].Author = filez.Author;

                        SongRecord[0].Groups = Groupss;

                        DirectoryInfo di;
                        var repacked_Path = TempPath + "\\0_repacked";
                        if (!DirectoryExists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

                        //manipulating the info
                        if (c("dlcm_AdditionalManipul102") != "Yes")
                        {
                            if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") data.SongInfo.SongDisplayName = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false, false, cnc);
                            if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") data.SongInfo.SongDisplayNameSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);
                            if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") data.SongInfo.Artist = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false, false, cnc);
                            if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);
                            if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") data.SongInfo.Album = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true, false, cnc);
                            if (ConfigRepository.Instance()["dlcm_Activ_AlbumSort"] == "Yes") data.SongInfo.AlbumSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album_Sort"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true, false, cnc);

                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes") //21.Pack with The/ Die only at the end of Title Sort 
                            {
                                //    if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && data.SongInfo.SongDisplayNameSort.Length > 4)
                                //    {
                                if (data.SongInfo.SongDisplayNameSort.Length > 4) data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                SongRecord[0].Song_Title_Sort = data.SongInfo.SongDisplayNameSort;
                                //}
                                if (data.SongInfo.ArtistSort.Length > 4) data.SongInfo.ArtistSort = MoveTheAtEnd(data.SongInfo.ArtistSort);
                                SongRecord[0].Artist_Sort = data.SongInfo.ArtistSort;
                                if (data.SongInfo.AlbumSort.Length > 4) data.SongInfo.AlbumSort = MoveTheAtEnd(data.SongInfo.AlbumSort);
                                SongRecord[0].Album_Sort = data.SongInfo.AlbumSort;
                            }
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && data.SongInfo.ArtistSort.Length > 4) //21.Pack with The/ Die only at the end of Title Sort 
                            {
                                //    if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && data.SongInfo.SongDisplayNameSort.Length > 4)
                                //    {
                                if (data.SongInfo.SongDisplayNameSort.Length > 4) data.SongInfo.SongDisplayName = MoveTheAtEnd(data.SongInfo.SongDisplayName);
                                SongRecord[0].Song_Title = data.SongInfo.SongDisplayNameSort;
                                //}
                                if (data.SongInfo.Artist.Length > 4) data.SongInfo.Artist = MoveTheAtEnd(data.SongInfo.Artist);
                                SongRecord[0].Artist = data.SongInfo.Artist;
                                if (data.SongInfo.Album.Length > 4) data.SongInfo.Album = MoveTheAtEnd(data.SongInfo.Album);
                                SongRecord[0].Album = data.SongInfo.Album;
                            }

                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul1"] == "Yes")
                                data.SongInfo.SongDisplayName = ord_no + "_" + data.SongInfo.SongDisplayName;
                        }
                        if (c("dlcm_AdditionalManipul113") == "Yes")
                            //{
                            if (!ConfigRepository.Instance()["dlcm_Activ_ArtistSort"].Contains("GroupIndex"))
                                data.SongInfo.ArtistSort = Manipulate_strings("<FirstGroupIndexAndName>", 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc) + data.SongInfo.ArtistSort;
                        //}
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
                                    tsst = "Fix the _preview_preview issue"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
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
                        if (data == null)
                            UpdateLog(DateTime.Now, "One or more fields are missing information.", false, c("dlcm_TempPath"), "", "", null, null);

                        //Add comments to beginning of the lyrics
                        var der = ConfigRepository.Instance()["dlcm_AdditionalManipul73"];
                        var ft = filez.Has_Vocals;

                        /*File Name should be standardised.. no need for 0&Group at the beginning MAYBE MAYBE WHAT IF i wanna structure my files based on group anyway((ConfigRepository.Instance()["dlcm_File_Name"].IndexOf("<Beta>") > -1) ? "" : "0") + */
                        var FN = "";
                        if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")/*repacked_Path + "\\" + */
                            FN = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
                        else
                            FN = ((filez.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear.ToString() + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul91"] == "Yes") FN = Groupss + FN;

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes")
                        {
                            FN = FN.Replace(".", "_");
                            FN = FN.Replace(" ", "_");
                            FN = FN.Replace("__", "_");
                            FN = FN.Replace("/", "");
                        }

                        data.ToolkitInfo.PackageVersion = filez.Version;

                        int progress = 0;
                        var errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        numPlatforms++;

                        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                        timestamp = UpdateLog(timestamp, "Packing" + PreviewLenght, true, tmpPath, multithreadname, form, null, null);

                        ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = data.ToolkitInfo.PackageAuthor + ";" + data.Name + ";" + SongRecord[0].Track_No + ";" + data.ToolkitInfo.PackageVersion + ";" + SongRecord[0].ID
                            + ";" + SongRecord[0].EoFPath + ";" + SongRecord[0].YouTube_Link + ";" + SongRecord[0].BasedOn_Youtube
                        + ";" + SongRecord[0].BasedOn_CF + ";" + SongRecord[0].BasedOn_Tabs + ";" + SongRecord[0].Spotify_Song_ID + ";" + SongRecord[0].Description + ";" + SongRecord[0].ToDos
                        + ";" + SongRecord[0].ToneDetails + ";" + "Yes"
                        + ";" + ";" + "Yes" + ";" + "Yes" + ConfigRepository.Instance()["dlcm_EoFPath"]
                        + ";" + ";" + SongRecord[0].PackingDate + ";" + SongRecord[0].UpdateVersionDate
                        + "Author,DLC_Name,TrackNo,Version,CDLCID,txt_EoFPath,YBLink,BasedOnYB,BasedOnCF,TabLinks,Spotify,Description,toDo,ToneDetails,SaveInVerisonInfo,SaveInDB,SaveRemotely,SaveRemotelyPath,PackageDate,UpdateDate";

                        data.ToolkitInfo.PackageComment = ConfigRepository.Instance()["dlcm_GlobalTempVariable"] + data.ToolkitInfo.PackageComment;
                        ConfigRepository.Instance()["dlcm_Global2TempVariable"] = "\nSongDisplayName: " + data.SongInfo.SongDisplayName +
                            "\nSongDisplayNameSort: " + data.SongInfo.SongDisplayNameSort +
                            "\nArtist: " + data.SongInfo.Artist +
                            "\nArtistSort: " + data.SongInfo.ArtistSort +
                            "\nAlbum: " + data.SongInfo.Album +
                            "\nAlbumSort: " + data.SongInfo.AlbumSort +
                            "\nFile Name: " + FN +
                            "\nPackage internal Comment: " + (data.ToolkitInfo.PackageComment == null ? "" : data.ToolkitInfo.PackageComment.ToString()) + "----";

                        UpdateLog(DateTime.Now, "Metadata:\n" + ConfigRepository.Instance()["dlcm_Global2TempVariable"] + "\n", false, c("dlcm_TempPath"), "", "", null, null);/*\n*/

                        //check if already packed
                        if (c("dlcm_AdditionalManipul98") == "Yes" && form != "MainDB")
                        {
                            DataSet dvr = new DataSet(); if (chbx_PC == "PC") dvr = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Pc\"", "", cnb, cnc);
                            if (dvr.Tables.Count > 0) if (dvr.Tables[0].Rows.Count > 0) chbx_PC = "";
                            DataSet dvd = new DataSet(); if (chbx_Mac == "Mac") dvd = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Mac\"", "", cnb, cnc);
                            if (dvd.Tables.Count > 0) if (dvd.Tables[0].Rows.Count > 0) chbx_Mac = "";
                            DataSet dvx = new DataSet(); if (chbx_PS3 == "PS3") dvx = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"PS3\"", "", cnb, cnc);
                            if (dvx.Tables.Count > 0) if (dvx.Tables[0].Rows.Count > 0) chbx_PS3 = "";
                        }

                        if (chbx_PC == "PC")
                            try
                            {
                                dlcSavePath = repacked_Path + "\\" + chbx_PC + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true, "", "", "");
                                    frm1.ShowDialog();
                                }
                                var tgst = "Erro generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@PC pack" + ex.Message;
                            }

                        if (chbx_Mac == "Mac")
                            try
                            {
                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_Mac + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true, "", "", "");
                                    frm1.ShowDialog();
                                }
                                error = true;
                                error_reason += "@Mac pack" + ex.Message;
                            }

                        if (chbx_XBOX == "XBOX360")
                            try
                            {
                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_XBOX + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true, "", "", "");
                                    frm1.ShowDialog();
                                }
                                var tgst = "Erro at xbox generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@XBOX pack" + ex.Message;
                            }

                        if (chbx_PS3 == "PS3")
                            try
                            {
                                fixMissingTempArtfiles(data);
                                dlcSavePath = repacked_Path + "\\" + chbx_PS3 + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java (64bit if windows is for 64b https://www.java.com/en/download/manual.jsp)" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true, "", "", "");
                                    frm1.ShowDialog();
                                }
                                string ss = string.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace);
                                var tgst = "Erro @ps3generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason += "@PS3 pack" + ex.Message;
                            }
                        data.CleanCache();
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

                            if (!error && ConfigRepository.Instance()["dlcm_AdditionalManipul101"] == "Yes") error = CheckSong(source);
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
                            if (!error && ConfigRepository.Instance()["dlcm_AdditionalManipul101"] == "Yes") error = CheckSong(source);
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

                cnb.Close();
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

        //public static void GeneratePackingSummary(string pack, string metainfo, int brkn, OleDbConnection cnb, int total, int norows, SQLiteConnection cnz) 
        public static void GeneratePackingSummary(string pack, string metainfo, int brkn, OleDbConnection cnb, int total, int norows, SQLite.SQLiteConnection cnc)
        {
            //GenerateSumamrty
            /*var total = 0;*/
            var PS3P = 0; var PCP = 0; var MACP = 0; var XBOXP = 0; var FailedP = 0; var ListP = "\n"; ; var ListNP = "\n";
            var PS3F = 0; var PCF = 0; var MACF = 0; var XBOXF = 0; var cmds = ""; var cpy = 0;
            ////DataSet dnz = new DataSet(); dnz = SelectFromDB("Main", cmds, null, cnb, cnc);
            ////if (dnz.Tables.Count > 0) total = dmz.Tables[0].Rows.Count;
            cmds = "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\"";
            DataSet dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PS3P = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            cmds = "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"";
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", cmds, null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PCP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Mac\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) MACP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"XBOX360\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            //DataSet dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"", txt_DBFolder.Text, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows.Count;
            //DataSet dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\"", txt_DBFolder.Text, cnb, cnc);
            //if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXP = dmz.Tables[0].Rows.Count;dmz = SelectFromDB("Pack_AuditTrail", "SELECT Sum(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\"", txt_DBFolder.Text, cnb, cnc);
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"PS3\" AND FTPed=\"Yes\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PS3F = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Pc\" AND FTPed=\"Yes\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) PCF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"Mac\" AND FTPed=\"Yes\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) MACF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND Platform=\"XBOX360\" AND FTPed=\"Yes\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) XBOXF = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("Pack_AuditTrail", "SELECT COUNT(ID) as ID FROM Pack_AuditTrail where Pack=\"" + pack + "\" AND FTPed=\"Yes\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) cpy = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT COUNT(ID) as ID FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb, cnc);
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) FailedP = dmz.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();

            dmz.Dispose(); dmz = new DataSet(); dmz = SelectFromDB("LogPackingError", "SELECT CDLC_ID, Comments FROM LogPackingError where Pack=\"" + pack + "\"", null, cnb, cnc);
            var noOfRecs = 0;
            if (dmz.Tables.Count > 0) if (dmz.Tables[0].Rows.Count > 0) noOfRecs = dmz.Tables.Count == 0 ? 0 : dmz.Tables[0].Rows.Count;
            var packapth = "";
            for (var j = 0; j < noOfRecs; j++)
            {
                var dnz = new DataSet(); dnz = SelectFromDB("Main", "SELECT Artist, Song_Title FROM Main where ID=" + dmz.Tables[0].Rows[j].ItemArray[0].ToString() + "", null, cnb, cnc);
                //if (dnz.Tables.Count > 0) if (dnz.Tables[0].Rows.Count > 0) noOfRecs = dmz.Tables[0].Rows.Count;
                ListNP += j + ". " + dmz.Tables[0].Rows[j].ItemArray[0].ToString() + "-" + dnz.Tables[0].Rows[0].ItemArray[0].ToString() + "-" + dnz.Tables[0].Rows[0].ItemArray[1].ToString() + "-" +
                    dmz.Tables[0].Rows[j].ItemArray[1].ToString() + "\n";
                //dnz.Dispose();
                //packapth = dmz.Tables[0].Rows[j].ItemArray[1].ToString();
            }

            var cmz = new DataSet(); cmz = SelectFromDB("Pack_AuditTrail", "SELECT FileName, PackPath, CDLC_ID FROM Pack_AuditTrail where Pack=\"" + pack + "\"", null, cnb, cnc);
            noOfRecs = dmz.Tables.Count == 0 ? 0 : cmz.Tables[0].Rows.Count;
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
                "\n\nFailed at packing: " + FailedP + "\n" + ListNP +
                ("\n\nListP: " + ListP);
            ErrorWindow frm9 = new ErrorWindow(summary, "", "Summary of the Mass-Repack process", false, false, true, "", "", "");
            frm9.Show();
            UpdateLog(DateTime.Now, "Ending Packing " + summary + "\n songs.", true, null, "", "DLCManager", null, null);
        }


        //public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLiteConnection cnz)
        public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLite.SQLiteConnection cnc)
        {
            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time, Bonus, Part FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRec = dus.Tables[0].Rows.Count;
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
            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRec = dus.Tables[0].Rows.Count;
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

                scomments += (RouteMask == "Lead" && Commentz != "" ? " L_" + Commentz + b + p : "") + (RouteMask == "Bass" && Commentz != "" ? " B_" + Commentz + b + p : "") + (RouteMask == "Rhythm" && Commentz != "" ? " R_" + Commentz + b + p : "");
                var Instr = (RouteMask == "Lead" ? " Lead" + bonus + p : "") + (RouteMask == "Bass" ? " Bass" + bonus + p : "") + (RouteMask == "Rhythm" ? " Rhythm" + bonus + p : "") + (ArrangementType == "Vocal" ? " Vocal" : "");
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
            }

            var spacetoadd = FirstLyric - 0.001;
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
            Add2LinesInVocals(ST, int.Parse(Math.Ceiling(decimal.Parse(fdt.ToString())).ToString()), ((float)(Math.Floor((FirstLyric - 0.001) / 2))).ToString(), note1);
            newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(ST);
            }
            catch (Exception ex) { tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }

            var size = int.Parse(Math.Ceiling(decimal.Parse(((scomments.Length + int.Parse(fdt.ToString())) / fdt).ToString())).ToString());
            for (i = 0; i < (fdt); i++)
            {
                newLyrics.Vocal[i].Time = (float)(Math.Round(float.Parse("0.001", NumberStyles.Float, CultureInfo.CurrentCulture) + ((FirstLyric - 0.001) / 2) + i * spacetoadd / fdt, 3));//(float)(Math.Round((float)spacetoadd + newLyrics.Vocal[i].Length, 3));
                newLyrics.Vocal[i].Note = note1;
                newLyrics.Vocal[i].Length = (float)(Math.Round((spacetoadd / fdt / 2 - float.Parse("0.002", NumberStyles.Float, CultureInfo.CurrentCulture)), 3));/*(double)FirstLyric -  / 2*/
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

            spacetoadd = (FirstLyric - 0.001) / 2 / (ft);
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

            System.Data.OleDb.OleDbConnection cnb = null;
            SQLite.SQLiteConnection cnc = null;
            try
            {
                cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]
               + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source="
               + ConfigRepository.Instance()["dlcm_DBFolder"]);
                do
                    System.Threading.Thread.Sleep(1000);
                while (cnb.State.ToString() == "Connecting");
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
                cnb.Close();
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

            if (File.Exists(AudioPath))
            { if (File.Exists(AudioPath + ".origi")) DeleteFile(AudioPath + ".origi", false); }
            else
                if (File.Exists(AudioPath + ".origi")) File.Move(AudioPath + ".origi", AudioPath);

            if (File.Exists(audioPreviewPath))
            { if (File.Exists(audioPreviewPath + ".origi")) DeleteFile(audioPreviewPath + ".origi", false); }
            else
                if (File.Exists(audioPreviewPath + ".origi")) File.Move(audioPreviewPath + ".origi", audioPreviewPath);

            if (File.Exists(oggPath))
            { if (File.Exists(oggPath + ".origi")) DeleteFile(oggPath + ".origi", false); }
            else
                 if (File.Exists(oggPath + ".origi")) if (File.Exists(oggPath + ".origi")) File.Move(oggPath + ".origi", oggPath);

            if (File.Exists(oggPreviewPath))
            { if (File.Exists(oggPreviewPath + ".origi")) DeleteFile(oggPreviewPath + ".origi", false); }
            else
                if (File.Exists(oggPreviewPath + ".origi")) File.Move(oggPreviewPath + ".origi", oggPreviewPath);

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
                    TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                    startInfo.Arguments = string.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                        t,
                                                        tt,
                                                        r.TotalMilliseconds,
                                                        (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
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
                                    ErrorWindow frm1 = new ErrorWindow("In order to use the FixAudioIssues-Preview, please Install Wwise Launcher then Wwise v" + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true, true, "", "", "");
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
                                        //fix as sometime the template folder gets poluted and breaks eveything
                                        var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                        var templateDir = Path.Combine(appRootDir, "Template");
                                        var backup_dir = AppWD + "\\Template";
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

            cnb.Close();

            if (File.Exists(AudioPath))
            { if (File.Exists(AudioPath + ".orig")) DeleteFile(AudioPath + ".orig", false); }
            else
                File.Move(AudioPath + ".orig", AudioPath);

            if (File.Exists(audioPreviewPath))
            { if (File.Exists(audioPreviewPath + ".orig")) DeleteFile(audioPreviewPath + ".orig", false); }
            else
                File.Move(audioPreviewPath + ".orig", audioPreviewPath);

            if (File.Exists(OggPath))
            { if (File.Exists(OggPath + ".orig")) DeleteFile(OggPath + ".orig", false); }
            else
                File.Move(OggPath + ".orig", OggPath);

            if (File.Exists(tr))
            { if (File.Exists(tr + ".orig")) DeleteFile(tr + ".orig", false); }
            else
                File.Move(tr + ".orig", tr);

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
            var norec = dus.Tables.Count > 0 ? dus.Tables[0].Rows.Count : 0;
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

            //Make sure no Albbum & Artist are blank
            var cmd1 = "UPDATE Standardization SET Artist = \"xxx\" WHERE Artist is null";
            var gdus = UpdateDB("Standardization", cmd1, cnb, cnc);
            var cmd2 = "UPDATE Standardization SET Album = \"xxx\" WHERE Album is null";
            var gdud = UpdateDB("Standardization", cmd2, cnb, cnc);

            //Multiply
            //tst = "Apply Already existing translations"; UpdateLog(DateTime.Now, tst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //Standardization.ApplyExistingTranlations(cnb);

            cmd1 = "SELECT * FROM Standardization WHERE Artist_Correction <> \"\" or Album_Correction <> \"\"  order by id;";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var albumyear_c = "";
            var DB_Path = dbp;
            //int aa = 0;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd1, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb, cnc);
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = 11; var cnt = 0;
            var tsst = "1/12 Applying " + norec + "corrections"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);

            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                cnt++;
                artist_c = dataRow.ItemArray[2].ToString();
                album_c = dataRow.ItemArray[4].ToString();
                artpath_c = dataRow.ItemArray[5].ToString();
                albumyear_c = dataRow.ItemArray[9].ToString();

                //tst = cnt + "\"" + norec + "Running Translation_And_Correction..." + artist_c + " " + album_c + " " + albumyear_c; timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + "\"," : "") + (album_c != "" ? " Album = \"" + album_c + "\"," : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"," : "") + (albumyear_c != "" ? " Album_Year = \"" + albumyear_c + "\"," : "");
                cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"";
                cmd1 = cmd1.Replace("SET ,", "SET ").Replace(", ,", ", ").Replace(",,", ", ") + ";";
                dus = UpdateDB("Main", cmd1, cnb, cnc);

                pB_ReadDLCs.Increment(1);
            }

            //insert any translation if not already existing
            tsst = "2/12 insert any translation if not already existing"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
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

            DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT distinct Artist, Album FROM Main ORDER BY Artist", "", cnb, cnc);
            var noOfRec = dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
            DataSet dg = new DataSet(); dg = SelectFromDB("Main", "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
                ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S ORDER BY Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])", "", cnb, cnc);
            var noOfRecz = dg.Tables.Count == 0 ? 0 : dg.Tables[0].Rows.Count;
            var found = false/*; var album = ""; var artist = ""*/; var tz = 0;
            if (noOfRec > 0 && noOfRecz > 0)
                for (var l = 0; l < noOfRec; l++)
                {
                    found = false;
                    for (var v = 0; v < noOfRecz; v++)
                    {
                        tz++;
                        var artfound = false;
                        if (dgs.Tables[0].Rows[l].ItemArray[0].ToString().ToLower() == dg.Tables[0].Rows[v].ItemArray[0].ToString().ToLower())
                        {
                            artfound = true;
                            if (dgs.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dg.Tables[0].Rows[v].ItemArray[1].ToString().ToLower())
                            {
                                found = true; break;/*album = dgs.Tables[0].Rows[l].ItemArray[1].ToString(); artist = dgs.Tables[0].Rows[l].ItemArray[0].ToString(); */
                            }
                        }
                        else if (artfound) break;
                    }
                    if (!found)
                        InsertIntoDBwValues("Standardization", "Artist, Album", "\"" + dgs.Tables[0].Rows[l].ItemArray[0].ToString() + "\",\"" + dgs.Tables[0].Rows[l].ItemArray[1].ToString() + "\"", cnb, 0, cnc);
                }

            tsst = "3/12 Cleans out duplicates (prev" + tz + ")"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            ManuallyRemoveDuplicates(cnb, cnc);

            //Apply Artist Short Name
            tsst = "4/12 Apply Artist Short Name (prev:" + tz + ""; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            ApplyArtistShort(cnb, cnc);

            //Apply Album Short Name    
            tsst = "5/12 Apply Album Short Name"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            ApplyAlbumShort(cnb, cnc);

            //Multiply Spotify
            tsst = "6/12 Multiply Spotify"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            MultiplySpotify(cnb, cnc);

            //Multiply Cover
            tsst = "7/12 Multiply Cover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            ApplyDefaultCover(cnb, cnc);

            //Apply DefaultCover
            tsst = "8/12 Apply DefaultCover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            MakeCover(cnb, cnc);

            //Apply Artist Auto Group
            tsst = "9/12 Apply Artist Auto Group"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            ApplyArtistAutoGroup(cnb, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, cnc);

            //Apply YearCorrection
            tsst = "10/12 Multiply 1st and apply Year Correction"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            MultiplyAndApplyYear(cnb, cnc);

            //Apply Spotify
            tsst = "11/12 Multiply 1st and apply Spotify data"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            //Standardization.MultiplyAndApplySpotify(cnb);

            tsst = "12/12 Finished applying Standardization"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + cnt + ")");
        }

        public static void ManuallyRemoveDuplicates(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            DataSet dr = new DataSet(); dr = SelectFromDB("Main", "SELECT distinct Artist, Album, Artist_Correction, Album_Correction, ID FROM Standardization ORDER BY Artist", "", cnb, cnc);
            var noOfRec = dr.Tables.Count == 0 ? 0 : dr.Tables[0].Rows.Count;
            var IDs = ""; var tz = 0;
            if (noOfRec > 0)
                for (var l = 0; l < noOfRec; l++)
                    for (var v = l + 1; v < noOfRec; v++)
                    {
                        tz++;
                        if (dr.Tables[0].Rows[l].ItemArray[0].ToString().ToLower() == dr.Tables[0].Rows[v].ItemArray[0].ToString().ToLower())
                        {
                            if (dr.Tables[0].Rows[l].ItemArray[1].ToString().ToLower() == dr.Tables[0].Rows[v].ItemArray[1].ToString().ToLower()
                                && dr.Tables[0].Rows[l].ItemArray[2].ToString().ToLower() == dr.Tables[0].Rows[v].ItemArray[2].ToString().ToLower()
                                && dr.Tables[0].Rows[l].ItemArray[3].ToString().ToLower() == dr.Tables[0].Rows[v].ItemArray[3].ToString().ToLower())
                                IDs += dr.Tables[0].Rows[v].ItemArray[4].ToString().ToLower() + ", ";
                        }
                        else break;
                    }
            if (IDs.Length > 0)
                DeleteFromDB("Groups", "Delete * from Standardization WHERE ID IN (" + (IDs.Substring(0, IDs.Length - 2)) + ")", cnb, cnc); //Cleans out duplicates
        }

        public static void MultiplySpotify(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            //var norec = 0;
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT distinct iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Year_Correction FROM Standardization WHERE (SpotifyArtistID <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL,SpotifyAlbumPath, Year_Correction;", "", cnb, cnc);
            if (dfz.Tables.Count > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
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
                    var cmd1 = "UPDATE Standardization SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \""
                        + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";/*+ "\",Year_Correction = \"" + SpotifyYear +*/
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void MakeCover(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
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
            //DataSet dssx = new DataSet();
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DBs_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";", cn);
            //    da.Fill(dssx, "Standardization");
            //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //da.Fill(ds, "PositionType");
            //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //da.Fill(ds, "Badge");
            //}

            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", "SELECT Artist, Album, AlbumArt_Correction FROM Standardization WHERE (AlbumArt_Correction <> \"\") GROUP BY Artist,Album,AlbumArt_Correction;", "", cnb, cnc);
            //NoRec = dgt.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = NoRec;
            if (dgt.Tables.Count > 0)
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var album_c = dataRow.ItemArray[1].ToString();
                    var artpath_c = dataRow.ItemArray[2].ToString();
                    var cmd1 = "";
                    cmd1 = "UPDATE Main SET AlbumArtPath = \"" + artpath_c + "\" WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"";
                    dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
                    if (artpath_c != "" && album_c != "" && artpath_c != "") dgt = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"", "", cnb, cnc);
                    try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                    //cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + artpath_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\");";
                    //dgt = UpdateDB("Standardization", cmd1 + ";");
                }
            //DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"");
            //DataSet dssx = new DataSet(); dxr = SelectFromDB("Main", "SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";");


            // lbl_NoRec = noOfRec.ToString() + " records.";
            //MessageBox.Show("Cover has been defaulted as Cover to " + NoRec.ToString() + " songs");
        }
        public static void MultiplyAndApplyYear(OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            //Multiply
            var cmd = "SELECT o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " FROM Standardization AS o LEFT JOIN (SELECT count(artist) as c, artist FROM Standardization group by artist, album)  AS f ON o.Artist = f.Artist" +
                        " WHERE o.Year_Correction<>\"\"" +
                        " GROUP BY  o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " ORDER BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album)";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var artist_c = "";
            var album_c = "";
            if (dfz.Tables.Count > 0)
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
                    var cmd1 = "UPDATE Standardization SET Year_Correction = \"" + SpotifyYear + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                    //SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \""
                    //+ SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\",
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            var NoRec = 0;

            //DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", "SELECT iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
            cmd = "SELECT distinct iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                " FROM Standardization o WHERE o.Year_Correction<>\"\"" +
                " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction;";
            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", cmd, "", cnb, cnc);

            if (dgt.Tables.Count > 0)
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[0].ToString();
                    album_c = dataRow.ItemArray[1].ToString();
                    var year_c = dataRow.ItemArray[2].ToString();
                    var cmd1 = "";
                    cmd1 = "UPDATE Main SET Album_Year = \"" + year_c + "\" WHERE Artist=\"" + artist_c + "\" AND (Album=\"" + album_c + "\")";
                    dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
                    //if (artist_c != "" && album_c != "" && year_c != "") dgt = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"", "", cnb, cnc);
                    //try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                }
        }
        public static void MultiplyAndApplySpotify(OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            //Multiply
            var cmd = "SELECT o.ID, iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.Year_Correction" +
                        " FROM Standardization AS o LEFT JOIN (SELECT count(artist) as c, artist FROM Standardization group by artist, album)  AS f ON o.Artist = f.Artist" +
                        " WHERE o.SpotifyArtistID<>\"\"" +
                        " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath" +
                        " ORDER BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album)";
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var artist_c = "";
            var album_c = "";
            if (dfz.Tables.Count > 0)
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
                    var cmd1 = "UPDATE Main SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \"" + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\"" +
                        " WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\""
                        + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                    var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            var NoRec = 0;
            cmd = "SELECT distinct iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath" +
                " FROM Standardization o WHERE o.SpotifyArtistID<>\"\"" +
                " GROUP BY iif(o.Artist_Correction <> \"\", o.Artist_Correction, o.Artist), iif(o.Album_Correction <> \"\", o.Album_Correction, o.Album), o.SpotifyArtistID, o.SpotifyAlbumID, o.SpotifyAlbumURL, o.SpotifyAlbumPath;";
            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", cmd, "", cnb, cnc);

            if (dgt.Tables.Count > 0)
                foreach (DataRow dataRow in dgt.Tables[0].Rows)
                {
                    artist_c = dataRow.ItemArray[0].ToString();
                    album_c = dataRow.ItemArray[1].ToString();
                    var SpotifyArtistID = dataRow.ItemArray[2].ToString();
                    var SpotifyAlbumID = dataRow.ItemArray[3].ToString();
                    var SpotifyAlbumURL = dataRow.ItemArray[4].ToString();
                    var SpotifyAlbumPath = dataRow.ItemArray[5].ToString();
                    var cmd1 = "";
                    cmd1 = "UPDATE Main SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \"" + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\"" +
                        " WHERE Artist=\"" + artist_c + "\" AND (Album=\"" + album_c + "\")";
                    dgt = UpdateDB("Main", cmd1 + ";", cnb, cnc);
                }
        }

        public static void ApplyArtistShort(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;
            DataSet dfz = new DataSet();
            dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short" +
                " FROM Standardization WHERE (Artist_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist)," +
                " Artist_Short;", "", cnb, cnc);

            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            if (dfz.Tables.Count > 0)
                foreach (DataRow dataRow in dfz.Tables[0].Rows)
                {
                    var artist_c = dataRow.ItemArray[0].ToString();
                    var short_c = dataRow.ItemArray[1].ToString();
                    var cmd1 = "UPDATE Main SET Artist_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\"";
                    DataSet dus = UpdateDB("Main", cmd1 + ";", cnb, cnc);
                    dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", "", cnb, cnc);
                    try { norec = dus.Tables[0].Rows.Count; } catch { }
                    cmd1 = "UPDATE Standardization SET Artist_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c
                        + "\" OR Artist_Correction=\"" + artist_c + "\"";
                    if (artist_c != "" && short_c != "") dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
                }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplyArtistAutoGroup(OleDbConnection cnb, ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            var timestamp = DateTime.Now;//.ToString("yyyyMMdd HHmmssfff");
            DataSet dgf = new DataSet();
            dgf = SelectFromDB("Groups", "SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\"", "", cnb, cnc);
            if (dgf.Tables.Count > 0) ;

            DataSet df = new DataSet();
            df = SelectFromDB("Standardization", "SELECT DISTINCT Artist_AutoGroup,iif(Artist_Correction<>\"\", Artist_Correction, Artist) FROM Standardization WHERE Artist_AutoGroup<>\"\"", "", cnb, cnc);
            if (df.Tables.Count > 0) foreach (DataRow defaultgrp in df.Tables[0].Rows)
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
                    var tsst = "9/12 Apply Artist Auto Group DLC in Default grp check"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);

                    if (dfz.Tables.Count > 0)
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
                    tsst = "9/12 Apply Artist Auto Group end check"; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
                }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplyAlbumShort(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;

            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), Album_short FROM Standardization WHERE (Album_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist),Album_short,iif(Album_Correction<>\"\",Album_Correction,Album);", "", cnb, cnc);
            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var short_c = dataRow.ItemArray[2].ToString();
                var cmd1 = "UPDATE Main SET Album_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";";
                DataSet dus = UpdateDB("Main", cmd1, cnb, cnc);
                dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";", "", cnb, cnc); try { norec = dus.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET Album_Short = \"" + short_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
            }
            //var noOfRec = dgt.Tables[0].Rows.Count;
            //lbl_NoRec = norec.ToString() + " records.";
            //MessageBox.Show("Album Short has been defaulted onto " + norec.ToString() + " songs");
        }
        public static void ApplyExistingTranlations(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            //var norec = 0;
            DataSet dfz = new DataSet();
            var cmd = "SELECT Artist, Artist_Correction  FROM Standardization WHERE" +
                " (Artist_Correction <> \"\") GROUP BY Artist, Artist_Correction;";
            dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            if (dfz.Tables.Count > 0)
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
            if (dgz.Tables.Count > 0)
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
            if (dhz.Tables.Count > 0)
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

        public static void ApplyDefaultCover(OleDbConnection cnb, SQLite.SQLiteConnection cnc)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            //var norec = 0; //get al Default ON entries in standardization table
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath) FROM Standardization WHERE (Default_Cover = \"Yes\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album),IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath);", "", cnb, cnc);

            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var Default_Cover = dataRow.ItemArray[2].ToString();
                //var cmd1 = "UPDATE Main SET Spotify_Artist_ID = \"" + SpotifyArtistID + "\",Spotify_Album_ID = \"" + SpotifyAlbumID + "\",Spotify_Album_URL = \"" + SpotifyAlbumURL + "\",Spotify_Album_Path = \"" + SpotifyAlbumPath + "\", WHERE Album=\"" + SpotifyAlbumID + "\"";
                //DataSet dus = UpdateDB("Main", cmd1 + ";");
                //dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", ""); try { norec = dus.Tables[0].Rows.Count; } catch { }
                //apply only to Same Artist&Album Names
                var cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + Default_Cover + "\", Default_Cover == \"Yes\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                var dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
            }

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


            var r = true;
            //if (SongRecord.Folder_Name.IndexOf("dlcpack") >= 0) r = CheckForRecord("Cache", "SELECT * FROM CACHE WHERE Removed=\"Yes\" AND AlbumArtPath=\"" + artp.Replace("_128.dds", "_256.dds").Replace("_64.dds", "_256.dds") + "\"", cnb, cnc);
            //if (r == true) return false;

            //timestamp = UpdateLog(timestamp, "\n ArtAudio songs.", true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //info.Arrangements[i].SongXml.Name.Substring(0, info.Arrangements[i].SongXml.Name.IndexOf("_") - 1);
            if (Directory.Exists(SongsPath + "\\" + songanddlcname))
                if (Directory.Exists(SongsPath + "\\" + SongRecord.DLC_Name))
                    ;
                else
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
            var arng = "";

            DataSet dvs = new DataSet(); dvs = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            //" + " AND ArrangementType=\"Vocal\";", "", cnb, cnc);
            var norec = dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0; var once = true;
            var hhh = "SELECT * FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + GetArrOfficSQLTxt(arrangoff);
            if (norec == 0)
                ;
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

    }
}
