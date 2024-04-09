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
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Windows.Devices.Geolocation;
using Swan;
using X360.Other;
using System.ComponentModel.Design;
using System.Windows.Documents;
using System.Data.Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Dynamic;
using System.Transactions;
using System.IO.Compression;
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using static Gpif.Rhythm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using System.Windows.Shell;
using Microsoft.Extensions.Logging;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Windows.Foundation.Metadata;
using Platform = RocksmithToolkitLib.Platform;
using System.Security.Policy;
using Windows.UI.StartScreen;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using FontStyle = System.Drawing.FontStyle;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Windows.UI.Core;
using static System.Net.Mime.MediaTypeNames;
using Windows.ApplicationModel.Background;
using EmbedIO.Utilities;

namespace RocksmithToolkitGUI.DLCManager
{
    class UtilitiesFunctions
    {

        public const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";

        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        public const long BUFFER_SIZE = 4096;
        public static StringBuilder errorsFound;
        public static OleDbConnection cnb;
        public static SQLite.SQLiteConnection cnc;
        public enum ConverterTypes
        {
            HeaderFix,
            Revorb,
            WEM,
            Ogg2Wem
        }
        public class Arrangements
        {
            public int ID { get; set; }
            public string Arrangement_Name { get; set; }
            public int CDLC_ID { get; set; }
            public string Bonus { get; set; }
            public string JSONFilePath { get; set; }
            public string XMLFilePath { get; set; }
            public string XMLFile_Hash { get; set; }
            public string ScrollSpeed { get; set; }
            public string Tunning { get; set; }
            public string Rating { get; set; }
            public string PlayThroughYBLink { get; set; }
            public string CustomsForge_Link { get; set; }
            public string ArrangementSort { get; set; }
            public string TuningPitch { get; set; }
            public string ToneBase { get; set; }
            public string Idd { get; set; }
            public string MasterId { get; set; }
            public string ArrangementType { get; set; }
            public string String0 { get; set; }
            public string String1 { get; set; }
            public string String2 { get; set; }
            public string String3 { get; set; }
            public string String4 { get; set; }
            public string String5 { get; set; }
            public string PluckedType { get; set; }
            public string RouteMask { get; set; }
            public string XMLFileName { get; set; }
            public string XMLFileLLID { get; set; }
            public string XMLFileUUID { get; set; }
            public string SNGFileName { get; set; }
            public string SNGFileLLID { get; set; }
            public string SNGFileUUID { get; set; }
            public string ToneMultiplayer { get; set; }
            public string ToneA { get; set; }
            public string ToneB { get; set; }
            public string ToneC { get; set; }
            public string ToneD { get; set; }
            public string ConversionDateTime { get; set; }
            public string SNGFileHash { get; set; }
            public string Has_Sections { get; set; }
            public string Comments { get; set; }
            public string Start_Time { get; set; }
            public string CleanedXML_Hash { get; set; }
            public string Json_Hash { get; set; }
            public string Part { get; set; }
            public string MaxDifficulty { get; set; }
            public string NoSections { get; set; }
            public string OrigSongTrack { get; set; }
            public string PrimaryTrack { get; set; }
            public string Favorite { get; set; }
            public string Broken { get; set; }
            public string Official { get; set; }
            public string PersistentID { get; set; }
            public string CapoFret { get; set; }
        }

        public class Cache
        {
            public int ID { get; set; }
            public string Identifier { get; set; }
            public string Artist { get; set; }
            public string ArtistSort { get; set; }
            public string Album { get; set; }
            public string Title { get; set; }
            public string AlbumYear { get; set; }
            public string Arrangements { get; set; }
            public string Removed { get; set; }
            public string AlbumArtPath { get; set; }
            public string Comments { get; set; }
            public string PSARCName { get; set; }
            public string SongsHSANPath { get; set; }
            public string Platform { get; set; }
            public string AudioPath { get; set; }
            public string AudioPreviewPath { get; set; }
            public string Selected { get; set; }
            public string PS3Region { get; set; }
            public string PSACRBackupPath { get; set; }
            public string PSARCHash { get; set; }
            public string AudioHash { get; set; }
            public string AudioPreviewHash { get; set; }
            public string ArtHash { get; set; }
            public string AudioPathWEM { get; set; }
            public string AudioPreviewPathWEM { get; set; }
            public string AlbumSort { get; set; }
            public string CACHEPSARCName { get; set; }
        }

        public class Groups
        {
            public int ID { get; set; }
            public string CDLC_ID { get; set; }
            public string Groupz { get; set; }
            public string Type { get; set; }
            public string Comments { get; set; }
            public string Profile_Name { get; set; }
            public string Description { get; set; }
            public string DisplayName { get; set; }
            public string DisplayGroup { get; set; }
            public string DisplayPosition { get; set; }
            public string Date_Added { get; set; }
        }

        public class Import
        {
            public int ID { get; set; }
            public string FullPath { get; set; }
            public string Path { get; set; }
            public string FileName { get; set; }
            public string FileCreationDate { get; set; }
            public string FileHash { get; set; }
            public string FileSize { get; set; }
            public string ImportDate { get; set; }
            public string Pack { get; set; }
            public string Platform { get; set; }
            public string Invalid { get; set; }
        }

        public class Import_AuditTrail
        {
            public int ID { get; set; }
            public string FullPath { get; set; }
            public string Path { get; set; }
            public string FileName { get; set; }
            public string FileCreationDate { get; set; }
            public string FileHash { get; set; }
            public string FileSize { get; set; }
            public string ImportDate { get; set; }
            public string Pack { get; set; }
            public string CDLC_ID { get; set; }
            public string Platform { get; set; }
            public string Comment { get; set; }
            public string Song_Title { get; set; }
            public string Song_Title_Sort { get; set; }
            public string Album { get; set; }
            public string Album_Sort { get; set; }
            public string Artist { get; set; }
            public string Artist_Sort { get; set; }
            public string Album_Year { get; set; }
            public string AlbumArtPath { get; set; }
            public string DLC_Name { get; set; }
            public string DLC_AppID { get; set; }
            public string PreviewLenght { get; set; }
            public string audioBitrate { get; set; }
            public string audioSampleRate { get; set; }
        }

        public class LogImporting
        {
            public int ID { get; set; }
            public string Pack { get; set; }
            public string CDLC_ID { get; set; }
            public string Dates { get; set; }
            public string Comments { get; set; }
        }

        public class LogImportingError
        {
            public int ID { get; set; }
            public string Pack { get; set; }
            public string CDLC_ID { get; set; }
            public string Dates { get; set; }
            public string Comments { get; set; }
        }

        public class LogPacking
        {
            public int ID { get; set; }
            public string Pack { get; set; }
            public int CDLC_ID { get; set; }
            public string Dates { get; set; }
            public string Comments { get; set; }
        }

        public class LogPackingError
        {
            public int ID { get; set; }
            public string Pack { get; set; }
            public int CDLC_ID { get; set; }
            public string Dates { get; set; }
            public string Comments { get; set; }
        }

        //public class Main
        //{
        //    public int ID { get; set; }
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
        //    public string Groups { get; set; }
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
        //    public string Is_Live { get; set; }
        //    public string Live_Details { get; set; }
        //    public string Remote_Path { get; set; }
        //    public string audioBitrat { get; set; }
        //    public string audioSampleRate { get; set; }
        //    public string is_Acoustic { get; set; }
        //    public string Top10 { get; set; }
        //    public string Has_Other_Officials { get; set; }
        //    public string Spotify_Song_ID { get; set; }
        //    public string Spotify_Artist_ID { get; set; }
        //    public string Spotify_Album_ID { get; set; }
        //    public string Spotify_Album_URL { get; set; }
        //    public string Audio_OrigHash { get; set; }
        //    public string Audio_OrigPreviewHash { get; set; }
        //    public string AlbumArt_OrigHash { get; set; }
        //    public string Duplicate_of { get; set; }
        //    public string Split4Pack { get; set; }
        //    public string UseInternalDDRemovalLogic { get; set; }
        //    public string Is_Instrumental { get; set; }
        //    public string Is_Single { get; set; }
        //    public string Is_Soundtrack { get; set; }
        //    public string Is_EP { get; set; }
        //    public string Has_Had_Audio_Changed { get; set; }
        //    public string Has_Had_Lyrics_Changed { get; set; }
        //    public string Album_Sort { get; set; }
        //    public string Is_Uncensored { get; set; }
        //    public string IntheWorks { get; set; }
        //    public string LyricsLanguage { get; set; }
        //    public string LastConversionDateTime { get; set; }
        //    public string ImprovedWithDM { get; set; }
        //    public string Is_FullAlbum { get; set; }
        //    public string PitchShiftableEsOrDd { get; set; }
        //    public string Import_AuditTrail_ID { get; set; }
        //    public string Is_Remastered { get; set; }
        //    public string EoFPath { get; set; }
        //    public string Is_Karaoke { get; set; }
        //    public string Is_Cover { get; set; }
        //    public string Has_Featuring { get; set; }
        //    public string Is_Demo { get; set; }
        //    public string Is_Remix { get; set; }
        //    public string BasedOn_Youtube { get; set; }
        //    public string BasedOn_CF { get; set; }
        //    public string BasedOn_Tabs { get; set; }
        //    public string ToDos { get; set; }
        //    public string ToneDetails { get; set; }
        //    public string PackageDetails { get; set; }
        //    public string PackingDate { get; set; }
        //    public string UpdateVersionDate { get; set; }
        //    public string Has_Capo { get; set; }
        //    public string Has_ShowLights { get; set; }
        //    public string Has_JVocals { get; set; }
        //    public string Is_Medley { get; set; }
        //    public string Is_MultiStrings { get; set; }
        //}

        public class ODLC
        {
            public string Band { get; set; }
            public string Song { get; set; }
            public string Type { get; set; }
            public int ID { get; set; }
        }

        public class OfficialSongs
        {
            public int ID { get; set; }
            public string Artist { get; set; }
            public string Title { get; set; }
            public string Album { get; set; }
            public string Tuning { get; set; }
            public string Version { get; set; }
            public string Author { get; set; }
            public string DateAdded { get; set; }
            public string DateUpdated { get; set; }
            public string Member { get; set; }
            public string DD { get; set; }
            public string Platform { get; set; }
            public string Hash { get; set; }
            public string FileName { get; set; }
            public string Ignore { get; set; }
            public string Alternate_Title { get; set; }
            public string Alternate_Artist { get; set; }
        }

        public class Pack_AuditTrail
        {
            public int ID { get; set; }
            public string CopyPath { get; set; }
            public string PackPath { get; set; }
            public string FileName { get; set; }
            public string PackDate { get; set; }
            public string FileHash { get; set; }
            public int FileSize { get; set; }
            public int CDLC_ID { get; set; }
            public string DLC_Name { get; set; }
            public string Platform { get; set; }
            public string Reason { get; set; }
            public string Official { get; set; }
            public string Pack { get; set; }
            public string FTPed { get; set; }
        }

        public class Standardization
        {
            public int ID { get; set; }
            public string Artist { get; set; }
            public string Artist_Correction { get; set; }
            public string Album { get; set; }
            public string Album_Correction { get; set; }
            public string AlbumArt_Correction { get; set; }
            public string Comments { get; set; }
            public string Artist_Short { get; set; }
            public string Album_Short { get; set; }
            public string Year_Correction { get; set; }
            public string SpotifyArtistID { get; set; }
            public string SpotifyAlbumID { get; set; }
            public string SpotifyAlbumURL { get; set; }
            public string SpotifyAlbumPath { get; set; }
            public string Default_Cover { get; set; }
            public string Artist_AutoGroup { get; set; }
            public string Suspect { get; set; }
            public string Suspect_Reason { get; set; }
            public string CustomToAtribute_1 { get; set; }
            public string CustomToAtribute_2 { get; set; }
            public string CustomToAtribute_3 { get; set; }
            public string CustomToAtribute_4 { get; set; }
            public string CustomToAtribute_5 { get; set; }
        }

        public class Tones
        {
            public int ID { get; set; }
            public string Tone_Name { get; set; }
            public int CDLC_ID { get; set; }
            public string Volume { get; set; }
            public string Keyy { get; set; }
            public string Is_Custom { get; set; }
            public string Description { get; set; }
            public string Favorite { get; set; }
            public string SortOrder { get; set; }
            public string NameSeparator { get; set; }
            public string ConversionDateTime { get; set; }
            public string lastConverjsonDateTime { get; set; }
            public string Comments { get; set; }
            public string Official { get; set; }
        }

        public class Tones_GearList
        {
            public int ID { get; set; }
            public int CDLC_ID { get; set; }
            public string Gear_Name { get; set; }
            public string Category { get; set; }
            public string KnobValuesKeys { get; set; }
            public string KnobValuesValues { get; set; }
            public string PedalKey { get; set; }
            public string Skin { get; set; }
            public string SkinIndex { get; set; }
            public string Type { get; set; }
            public string Comments { get; set; }
            public string Tone_Name { get; set; }
            public int Tone_ID { get; set; }
            public string Official { get; set; }
        }

        public class WEM2OGGCorrespondence
        {
            public int ID { get; set; }
            public string Platform { get; set; }
            public string Identifier { get; set; }
            public string EncryptedID { get; set; }
        }


        public class MainDBfields
        {
            public string NoRec { get; set; }   //	1 0
            public string ID { get; set; }   //	2 1
            public string Song_Title { get; set; }   //	3 2
            public string Song_Title_Sort { get; set; }  //	4 3
            public string Album { get; set; }    //	5 4
            public string Artist { get; set; }   //	6 5
            public string Artist_Sort { get; set; }  //	7
            public string Album_Year { get; set; }   //	8
            public string AverageTempo { get; set; }     //	9
            public string Volume { get; set; }   //	10
            public string Preview_Volume { get; set; }   //	11
            public string AlbumArtPath { get; set; }     //	12
            public string AudioPath { get; set; }    //	13
            public string audioPreviewPath { get; set; }     //	14
            public string Track_No { get; set; }     //	15
            public string Author { get; set; }   //	16
            public string Version { get; set; }  //	17
            public string DLC_Name { get; set; }     //	18
            public string DLC_AppID { get; set; }    //	19
            public string Current_FileName { get; set; }     //	20
            public string Original_FileName { get; set; }    //	21
            public string Import_Path { get; set; }  //	22
            public string Import_Date { get; set; }  //	23
            public string Folder_Name { get; set; }  //	24
            public string File_Size { get; set; }    //	25
            public string File_Hash { get; set; }    //	26
            public string Original_File_Hash { get; set; }   //	27
            public string Is_Original { get; set; }  //	28
            public string Is_OLD { get; set; }   //	29
            public string Is_Beta { get; set; }  //	30
            public string Is_Alternate { get; set; }     //	31
            public string Is_Multitrack { get; set; }    //	32
            public string Is_Broken { get; set; }    //	33
            public string MultiTrack_Version { get; set; }   //	34
            public string Alternate_Version_No { get; set; }     //	35
            public string DLC { get; set; }  //	36
            public string Has_Bass { get; set; }     //	37
            public string Has_Guitar { get; set; }   //	38
            public string Has_Lead { get; set; }     //	39
            public string Has_Rhythm { get; set; }   //	40
            public string Has_Combo { get; set; }    //	41
            public string Has_Vocals { get; set; }   //	42
            public string Has_Sections { get; set; }     //	43
            public string Has_Cover { get; set; }    //	44
            public string Has_Preview { get; set; }  //	45
            public string Has_Custom_Tone { get; set; }  //	46
            public string Has_DD { get; set; }   //	47
            public string Has_Version { get; set; }  //	48
            public string Tunning { get; set; }  //	49
            public string Bass_Picking { get; set; }     //	50
            public string Tones { get; set; }    //	51
            public string Groups { get; set; }    //	52
            public string Rating { get; set; }   //	53
            public string Description { get; set; }  //	54
            public string Comments { get; set; }     //	55
            public string Has_Track_No { get; set; }   //	56
            public string Platform { get; set; }   //	57
            public string PreviewTime { get; set; }    //	58
            public string PreviewLenght { get; set; }    //	59
            public string Youtube_Playthrough { get; set; }  //	60
            public string CustomForge_Followers { get; set; }     //	61
            public string CustomForge_Version { get; set; }    //	62
            public string FilesMissingIssues { get; set; }   //	63
            public string Duplicates { get; set; }   //	64
            public string Pack { get; set; }  //	65
            public string Keep_BassDD { get; set; }   //	66
            public string Keep_DD { get; set; }    //	67
            public string Keep_Original { get; set; }  //	68
            public string Song_Lenght { get; set; }  //	69
            public string Original { get; set; }     //	70
            public string Selected { get; set; }     //	71
            public string YouTube_Link { get; set; }     //	72
            public string CustomsForge_Link { get; set; }    //	73
            public string CustomsForge_Like { get; set; }    //	74
            public string CustomsForge_ReleaseNotes { get; set; }    //	75
            public string SignatureType { get; set; }   //76
            public string ToolkitVersion { get; set; }  //77
            public string Has_Author { get; set; }  //78
            public string OggPath { get; set; } //79
            public string oggPreviewPath { get; set; }  //80
            public string UniqueDLCName { get; set; }   //81
            public string AlbumArt_Hash { get; set; }   //82
            public string Audio_Hash { get; set; }  //83
            public string AudioPreview_Hash { get; set; }   //84
            public string Bass_Has_DD { get; set; }//Bass_Has_DD	//85
            public string Has_Bonus_Arrangement { get; set; }   //86
            public string Artist_ShortName { get; set; }    //87
            public string Album_ShortName { get; set; } //88
            public string Available_Old { get; set; }   //89
            public string Available_Duplicate { get; set; } //90
            public string Has_Been_Corrected { get; set; }  //91
            public string File_Creation_Date { get; set; }  //92
            public string Is_Live { get; set; } //93
            public string Live_Details { get; set; }    //94
            public string Remote_Path { get; set; } //95
            public string audioBitrate { get; set; }    //96
            public string audioSampleRate { get; set; } //97
            public string Is_Acoustic { get; set; } //98
            public string Top10 { get; set; }   //99
            public string Has_Other_Officials { get; set; } //100
            public string Spotify_Song_ID { get; set; } //101
            public string Spotify_Artist_ID { get; set; }   //102
            public string Spotify_Album_ID { get; set; }    //103
            public string Spotify_Album_URL { get; set; }   //104
            public string Audio_OrigHash { get; set; }  //105
            public string Audio_OrigPreviewHash { get; set; }   //106
            public string AlbumArt_OrigHash { get; set; }   //107
            public string Duplicate_Of { get; set; }    //108
            public string Split4Pack { get; set; }  //109
            public string UseInternalDDRemovalLogic { get; set; }   //110
            public string Is_Instrumental { get; set; } //111
            public string Is_Single { get; set; }   //112
            public string Is_Soundtrack { get; set; }   //113
            public string Is_EP { get; set; }   //114
            public string Has_Had_Audio_Changed { get; set; }   //115
            public string Has_Had_Lyrics_Changed { get; set; }  //116
            public string Album_Sort { get; set; }  //117
            public string Is_Uncensored { get; set; }   //118
            public string IntheWorks { get; set; }  //119
            public string LyricsLanguage { get; set; }  //120
            public string LastConversionDateTime { get; set; }  //121
            public string ImprovedWithDM { get; set; }  //122
            public string Is_FullAlbum { get; set; }    //123
            public string PitchShiftableEsOrDd { get; set; }    //124
            public string Import_AuditTrail_ID { get; set; }    //125
            public string Is_Remastered { get; set; }   //126
            public string EoFPath { get; set; } //127
            public string Is_Karaoke { get; set; }  //128
            public string Is_Cover { get; set; }    //129
            public string Has_Featuring { get; set; }   //130
            public string Is_Demo { get; set; } //131
            public string Is_Remix { get; set; }    //132
            public string BasedOn_Youtube { get; set; } //133
            public string BasedOn_CF { get; set; }  //134
            public string BasedOn_Tabs { get; set; }    //135
            public string ToDos { get; set; }   //136
            public string ToneDetails { get; set; } //137
            public string PackageDetails { get; set; }  //138
            public string PackingDate { get; set; } //139
            public string UpdateVersionDate { get; set; }   //140
            public string Has_Capo { get; set; }    //141
            public string Has_ShowLights { get; set; }  //142
            public string Has_JVocals { get; set; } //143
            public string Is_Medley { get; set; }   //144
            public string Is_MultiStrings { get; set; } //145
            public string BasedOn_GP { get; set; }  //146
            public string Album_ArtPathOrig { get; set; }   //147
            public string Is_Deluxe { get; set; }   //148
            public string Is_GreatestHits { get; set; } //149
            public string Is_Midi { get; set; } //150
            public string Is_GameSoundtrack { get; set; }   //151
            public string Is_AmateurCover { get; set; } //152
            public string Is_TVTheme { get; set; }  //153
            public string Has_Alternate_Audio { get; set; } //154
            public string Has_Alternate_Lyrics { get; set; }  //155
            public string Is_MetalCover { get; set; } //156
            public string Is_Ukulele { get; set; }  //157
            public string A440TunningFrecv { get; set; }  //158
        }	//154


        //public static SpotifyWebAPI _spotify = new SpotifyWebAPI
        //{
        //    AccessToken = null,
        //    TokenType = null
        //};
        public static object NullHandler(object instance)
        {
            if (instance != null)
                return instance.ToString();

            return DBNull.Value.ToString();// DBNull.Value;
        }

        public static string DisplayData()
        {
            var reader = OleDbEnumerator.GetRootEnumerator();
            var ret = "SOURCES_NAME:\n";
            var list = new List<System.String>();
            while (reader.Read())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i) == "SOURCES_NAME")
                    {
                        ret += reader.GetValue(i).ToString() + "\n";
                    }
                }
            }
            reader.Close();
            string AccessDBAsValue = string.Empty;
            ret += "\n\nSOFTWARE\\Classes:\n";
            RegistryKey rkACDBKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes");
            if (rkACDBKey != null)
                //int lnSubKeyCount = 0;
                //lnSubKeyCount =rkACDBKey.SubKeyCount; 
                foreach (string subKeyName in rkACDBKey.GetSubKeyNames())
                    if (subKeyName.Contains("Microsoft.ACE.OLEDB"))
                        ret += subKeyName + "\n";// do something what you want do
            return ret;
        }

        public static string CompactAndRepair(OleDbConnection cnb)
        {
            //https://stackoverflow.com/questions/58130446/net-core-3-0-and-ms-office-interop?msclkid=c246b2bcb21811ecb85b2c5e7437aac3
            var dates = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            try
            {
                //Microsoft.Office.Interop.Access app = new Microsoft.Office.Interop.Access.Application();
                //var app = new Microsoft.Office.Interop.Access.Application();
                //app.CompactRepair(cnb.DataSource.ToString(), cnb.DataSource.ToString(), false); app.Visible = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Microsoft Access must create a backup of your file before you perform the repair operation. Enter a name for the backup file.") > -1)
                {
                    //Microsoft.Office.Interop.Access.Application apps = new Microsoft.Office.Interop.Access.Application();
                    try
                    {
                        //apps.CompactRepair(cnb.DataSource.ToString(), cnb.DataSource.ToString() + dates, false); apps.Visible = false;
                        File.Delete(cnb.DataSource.ToString());
                        File.Move(cnb.DataSource.ToString() + dates, cnb.DataSource.ToString());
                    }
                    catch (Exception exx)
                    {
                        var tsst = "Error @compress..." + exx; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
            }
            return dates;
        }

        public static void StartProcesss(string procez, string attbute)
        {
            var starttmp = DateTime.Now;
            starttmp = UpdateLog(starttmp, "Running process: " + procez + attbute, false, c("dlcm_TempPath"), "", "", null, null);
            try
            {
                if (attbute == "" || attbute == null)
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = procez,
                        WorkingDirectory = Path.GetDirectoryName(procez)
                    };
                    Process DDC = new Process();
                    //startInfo.Arguments = "";
                    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

                    if (Directory.Exists(procez) || File.Exists(procez))
                    {
                        DDC.StartInfo = startInfo;
                        DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min"Error ..." + 
                        if (DDC.ExitCode > 0) starttmp = UpdateLog(starttmp, DDC.ExitCode.ToString(), false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
                else
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = procez,
                        WorkingDirectory = Path.GetDirectoryName(procez)
                    };
                    Process DDC = new Process();
                    startInfo.Arguments = attbute;
                    startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

                    if (Directory.Exists(procez) || File.Exists(procez))
                    {
                        DDC.StartInfo = startInfo;
                        DDC.Start(); DDC.WaitForExit(1000 * 60 * 4); //wait 1min"Error ..." + 
                        if (DDC.ExitCode > 0) starttmp = UpdateLog(starttmp, DDC.ExitCode.ToString(), false, c("dlcm_TempPath"), "", "", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                var tsst = "Erro ..." + ex; starttmp = UpdateLog(starttmp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not open Main DB connection in MainDB ! " + c("dlcm_DBFolder"));
            }
        }

        public static async Task<string> CheckIfConnectedToInternet()
        {
            var status = "NOK";
            try
            {
                Ping myPing = new Ping();
                string host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                status = (reply.Status == IPStatus.Success) ? "OK" : "NOK";
            }
            catch (Exception ex)
            {
                var tgst = "Erro1 ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
                status = "NOK";
            }
            return status;
        }
        public static Task<string> StartToGetSpotifyDetails(string Artist, string Album, string Title, string Year, string Status)
        {
            Task<string> bytesRead = RequestToGetSpotifyDetailsAsync(Artist, Album, Title, Year, Status);
            string sRead = "";
            if (bytesRead.Result.ToString().Split(';')[4] != "-" && bytesRead.Result.ToString().Split(';')[4] != "") sRead = DwdldAlbumImg(bytesRead.Result.ToString().Split(';')[4], bytesRead.Result.ToString().Split(';')[3]);
            return bytesRead;// +";"+ sRead;
        }

        public static async Task<string> RequestToGetSpotifyDetailsAsync(string Artist, string Album, string Title, string Year, string Status)
        {
            string bytesRead = await GetTrackNoFromSpotifyAsync(CleanTitle(Artist), CleanTitle(Album), CleanTitle(Title), Year, Status);
            return bytesRead;
        }

        public static string DwdldAlbumImg(string url, string spdetails)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = wc.DownloadData(new Uri(url));
                    FileStream file = new FileStream(c("dlcm_TempPath") + "\\0_albumCovers\\" + spdetails + ".png", FileMode.Create, System.IO.FileAccess.Write);
                    using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error2 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
            return "OK";
        }
        static public async Task ActivateSpotify_ClickAsync(object xsender, EventArgs e)
        {
            if (c("dlcm_AdditionalManipul82") == "Yes")
            {
                DialogResult result3 = MessageBox.Show("As selected by option 41 Tool will connect to Spotify to retrieve Track No, album covers, Year information, etc.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            string _clientId = c("dlcm_SpotifyClientAPI");
            string _secretId = c("dlcm_SpotifySecretAPI");
            //ImplicitGrantAuth auth =
            //    new ImplicitGrantAuth(_clientId, "http://localhost:4002", "http://localhost:4002", Scope.UserReadPrivate);
            //auth.AuthReceived += async (sender, payload) =>
            //{
            //    auth.Stop(); // `sender` is also the auth instance
            //    _spotify = new SpotifyWebAPI() { TokenType = payload.TokenType, AccessToken = payload.AccessToken };
            //    // Do requests with API client
            //};
            //auth.ShowDialog = true;
            //auth.Start(); // Starts an internal HTTP Server
            //auth.OpenBrowser();


        }

        //static public async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        //{
        //    AuthorizationCodeAuth auth = (AuthorizationCodeAuth)sender;
        //    auth.Stop();

        //    Token token = await auth.ExchangeCode(payload.Code);
        //    _spotify.AccessToken = token.AccessToken;
        //    _spotify.TokenType = token.TokenType;
        //}

        //public async void PrintUsefulData()/*SpotifyWebAPI _spotify*/
        //{
        //    PrivateProfile profile = await _spotify.GetPrivateProfileAsync();
        //    string name = string.IsNullOrEmpty(profile.DisplayName) ? profile.Id : profile.DisplayName;

        //    Paging<SimplePlaylist> playlists = await _spotify.GetUserPlaylistsAsync(profile.Id);
        //    do
        //    {
        //        playlists.Items.ForEach(playlist =>
        //        {
        //            //rtxt_StatisticsOnReadDLCs.Text += playlist.Name;
        //        });
        //        playlists = await _spotify.GetNextPageAsync(playlists);
        //    } while (playlists.HasNextPage());
        //}


        public static bool IsSymbolic(string path)
        {
            FileInfo pathInfo = new FileInfo(path);
            return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
        }
        public static void CreateMKLinks()
        {
            var copy = "";
            DateTime timestamp = DateTime.Now;
            if (!Directory.Exists(c("dlcm_TempPath"))) Directory.CreateDirectory(c("dlcm_TempPath"));
            var startInfo = new ProcessStartInfo
            {
                FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe",// Path.Combine(AppWD, "cmd.exe"),
                WorkingDirectory = c("dlcm_TempPath"),
                Verb = "runas"
            };

            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_data") || !Directory.Exists(c("dlcm_TempPath") + "\\0_data\\"))/**/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_data"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_data", c("dlcm_0_data") + "\\0_data");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_data", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_data " + c("dlcm_0_data") + "\\0_data");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_data") + "\\0_data", true, c("dlcm_TempPath"), "", "", null, null); } }
            }

            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_archive") || !Directory.Exists(c("dlcm_TempPath") + "\\0_archive"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_archive"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_archive", c("dlcm_0_archive") + "\\0_archive");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_archive", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_archive " + c("dlcm_0_archive") + "\\0_archive");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_archive") + "\\0_archive", true, c("dlcm_TempPath"), "", "", null, null); } }
            }

            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_broken") || !Directory.Exists(c("dlcm_TempPath") + "\\0_broken"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\dlcm_0_broken"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_broken", c("dlcm_0_broken") + "\\0_broken");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_broken", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_broken " + c("dlcm_0_broken") + "\\0_broken");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_broken") + "\\0_broken", true, c("dlcm_TempPath"), "", "", null, null); } }
            }

            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_duplicate") || !Directory.Exists(c("dlcm_TempPath") + "\\0_duplicate"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_duplicate"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_duplicate", c("dlcm_0_duplicate") + "\\0_duplicate");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_duplicate", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_duplicate " + c("dlcm_0_duplicate") + "\\0_duplicate");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_duplicate") + "\\0_duplicate", true, c("dlcm_TempPath"), "", "", null, null); } }
            }

            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_old") || !Directory.Exists(c("dlcm_TempPath") + "\\0_old"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_old"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_old", c("dlcm_0_old") + "\\0_old");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_old", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_old " + c("dlcm_0_old") + "\\0_old");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_old") + "\\0_old", true, c("dlcm_TempPath"), "", "", null, null); } }
            }

            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_dlcpacks") || !Directory.Exists(c("dlcm_TempPath") + "\\0_dlcpacks"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_dlcpacks"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_dlcpacks", c("dlcm_0_dlcpacks") + "\\0_dlcpacks");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_dlcpacks", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_dlcpacks " + c("dlcm_0_dlcpacks") + "\\0_dlcpacks");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_old") + "\\0_old", true, c("dlcm_TempPath"), "", "", null, null); } }
            }
            if (!IsSymbolic(c("dlcm_TempPath") + "\\0_old") || !Directory.Exists(c("dlcm_TempPath") + "\\0_old"))/*Directory.Exists(c("dlcm_TempPath") + "\\0_data\\")*/
            {
                if (Directory.Exists(c("dlcm_TempPath") + "\\0_old"))
                {
                    CopyFolder(c("dlcm_TempPath") + "\\0_old", c("dlcm_0_old") + "\\0_old");
                    DeleteDirectory(c("dlcm_TempPath") + "\\0_old", true);
                }
                startInfo.Arguments = string.Format(" /c mklink /D 0_old " + c("dlcm_0_old") + "\\0_old");
                using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); if (DDC.ExitCode > 0) { timestamp = UpdateLog(timestamp, "error at creation of " + c("dlcm_0_old") + "\\0_old", true, c("dlcm_TempPath"), "", "", null, null); } }
            }
            //startInfo.Arguments = string.Format(" /c mklink /D 0_data \\\\192.168.1.100\\Kits_Software\\t\0\\\0_data"); using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); }
            //startInfo.Arguments = string.Format(" /c mklink /D 0_data \\\\192.168.1.100\\Kits_Software\\t\0\\\0_data"); using (var DDC = new Process()) { DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 3 * 1); }
            if (Directory.Exists(c("dlcm_TempPath") + "\\0_old")) copy = "OLD;";
            if (Directory.Exists(c("dlcm_TempPath") + "\\0_archive")) copy = "ARCHIVE;";
            if (Directory.Exists(c("dlcm_TempPath") + "\\0_duplicate")) copy = "DUPLICATE;";
            if (Directory.Exists(c("dlcm_TempPath") + "\\0_data")) copy = "DATA;";
            if (Directory.Exists(c("dlcm_TempPath") + "\\0_broken")) copy = "BROKEN;";
            MessageBox.Show("Mklinks folders created:\n" + "\t" + c("dlcm_0_data") + "\\0_data" + "\n\t" + c("dlcm_0_archive") + "\\0_archive" + "\n\t" + c("dlcm_0_broken") + "\\0_broken" + "\n\t" + c("dlcm_0_duplicate") + "\\0_duplicate" + "\n\t" + c("dlcm_0_old") + "\\0_old"
                + (copy == "" ? "\nWith errors on the following" + copy : "")
                        , MESSAGEBOX_CAPTION + ": Creating MKLinks", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static async Task<string> GetTrackNoFromSpotifyAsync(string Artist, string Album, string Title, string Year, string Status)
        {
            WebClient webClient = new WebClient();
            string uriString = "https://api.spotify.com/v1/search";
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            ActivateSpotify_ClickAsync(null, null);

            NameValueCollection nameValueCollection = new NameValueCollection
            {
                { "query", keywordString }
            };
            var a1 = ""; var a2 = ""; var a3 = ""; var a4 = ""; var a5 = ""; var a6 = ""; var a7 = "";
            var output = "";
            var ab = "";
            var albump = 0;
            var artistp = 0;
            var tracknop = 0;
            try
            {
                //SearchItem Aitem = _spotify.SearchItems(Album, SearchType.Album);
                //if (!(Aitem.Error is null))
                //{
                //_spotify = null;
                ActivateSpotify_ClickAsync(null, null);
                //if (Aitem.Error == null || Aitem.Error.Message == "") Aitem = _spotify.SearchItems(Album, SearchType.Album);
                //else return "0" + ";-;-;-;-;-;-";
                //}
                //SearchItem Titem = _spotify.SearchItems(Title + "+" + Album + "+" + Artist, SearchType.All);
                //if (Titem.Error == null && Titem.Tracks.Total > 0)
                //    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem.Tracks.Items)
                //    {
                //        if (Titem.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                //                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                //                {
                //                    a1 = Trac.TrackNumber.ToString();
                //                    a2 = Trac.Id;
                //                    a3 = Artis.Id;
                //                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                //                    a4 = Trac.Album.Id;
                //                    a5 = FAitem.Images[0].Url;
                //                    a7 = FAitem.ReleaseDate;
                //                    if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                //                    else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                //                }
                //    }

                if (a1 == "")
                {
                    //SearchItem Titem2 = _spotify.SearchItems(Title, SearchType.Track, 500);
                    //if (Titem2.Error == null && Titem2.Tracks.Total > 0)
                    //    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem2.Tracks.Items)
                    //    {
                    //        if (Titem2.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                    //                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                    //                {
                    //                    a1 = Trac.TrackNumber.ToString();
                    //                    a2 = Trac.Id;
                    //                    a3 = Artis.Id;
                    //                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                    //                    a4 = Trac.Album.Id;
                    //                    a5 = FAitem.Images[0].Url;
                    //                    a7 = FAitem.ReleaseDate;
                    //                    if (Trac.Album.Name.ToString().ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                    //                    else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                    //                }
                    //    }

                    if (a1 == "")
                    {

                        //SearchItem Titem3 = _spotify.SearchItems(Album + "+" + Artist, SearchType.All);
                        //if (Titem3.Error == null && Titem3.Tracks.Total > 0)
                        //    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem3.Tracks.Items)
                        //    {
                        //        if (Titem3.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                        //                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                        //                {
                        //                    a1 = Trac.TrackNumber.ToString();
                        //                    a2 = Trac.Id;
                        //                    a3 = Artis.Id;
                        //                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                        //                    a4 = Trac.Album.Id;
                        //                    a5 = FAitem.Images[0].Url;
                        //                    a7 = FAitem.ReleaseDate;
                        //                    if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                        //                    else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                        //                }
                        //    }

                        if (a1 == "")
                        {
                            //SearchItem Titem4 = _spotify.SearchItems(Album, SearchType.Album);
                            //if (Titem4.Error == null && Titem4.Tracks != null)
                            //    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem4.Tracks.Items)
                            //    {
                            //        if (Titem4.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                            //                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                            //                {
                            //                    a1 = Trac.TrackNumber.ToString();
                            //                    a2 = Trac.Id;
                            //                    a3 = Artis.Id;
                            //                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                            //                    a4 = Trac.Album.Id;
                            //                    a5 = FAitem.Images[0].Url;
                            //                    a7 = FAitem.ReleaseDate;
                            //                    if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                            //                    else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                            //                }
                            //    }
                        }
                    }
                }

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
            catch (Exception ex) { var tsst = "Error3 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            goto finish;

        finish:
            a1 = a1.Trim();
            if (a1 != "")
            {
                if (output != "")
                {
                    string[] args = (output).ToString().Split(';');
                    if (args[5] != "" && args[5] != null) a6 = args[5];
                }
                if (a6 == "" || a6 == null)
                    a6 = (c("dlcm_TempPath") + "\\0_albumCovers\\" + Artist + " - " + Album.Replace(":", "") + ".png").Replace("/", "").Replace("?", "");
                if (!File.Exists(a6) && a5 != "" && a5 != null)
                    using (WebClient wc = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(new Uri(a5));
                        FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    }

                if (output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : "") + ";" + a7; }
                else output += (File.Exists(a6) ? a6 : "") + ";" + a7;
                return output;
            }
            else
                return "0" + ";-;-;-;-;-;-";
        }
        public static void OpenDBQuick()
        {
            cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security" +
                " Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security" +
                    " Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]); //running twice as some issues with compilatiojn in x86...sometime

        }
        public static void OpenDb()
        {/*OleDbConnection cnc*/
            if (cnb.State.ToString() == "Open") return;//notsure if ever reached
            if (cnc is not null)
                //if (cnc.ToString().Contains("open")) 
                return;
            //ConfigRepository.Instance()["dlcm_AccessDLLVersion"] = "ACE.OLEDB.12.0";
            var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
            //OleDbConnection cnf = null;
            //tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
            //#if (DEBUG)
            //{
            DialogResult result1 = DialogResult.Cancel;
            if (ShowMessagesCheck())
                result1 = MessageBox.Show("Select DB (mandatory when in debug project mode)\n(Yes) 1. SQL-Lite3\n(No) 2. M$ Access" +
               "\n(Cancel) 3. Use Default (" + Path.GetFileName(tz) + ") and Don't show informative (OK button only) windows" +
               "(indicators (show windows if any flag is false: i.e. yes + same machine + same day ): " + ConfigRepository.Instance()["dlcm_AdditionalManipul120"]
               + ";" + ConfigRepository.Instance()["dlcm_ShowMessages"] + ")\n\nUse SQL-Lite3 (Flag 114): "

                + (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] == "Yes" ? "Yes" : "No") + "\nProfile : " + ConfigRepository.Instance()["dlcm_Configurations"] + "\nDB Set:" + tz
            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            else UpdateLog(DateTime.Now, "Not showing DB select window desptite being in debug mode :)"
               , true, c("dlcm_TempPath"), "", "DLCManager", null, null);

            if (result1 == DialogResult.Yes || (result1 == DialogResult.Cancel && tz.Contains(".db")))/*&& tz.Contains(ConfigRepository.Instance()["dlcm_chosendb"]) */
            {
                ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                ConfigRepository.Instance()["dlcm_DBFolder"] = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
                ConfigRepository.Instance()["dlcm_chosendb"] = ".db";
            }
            if (result1 == DialogResult.No || (result1 == DialogResult.Cancel && tz.Contains(".accdb")))/*&& tz.Contains(ConfigRepository.Instance()["dlcm_chosendb"]) */
            {
                ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "No";
                ConfigRepository.Instance()["dlcm_DBFolder"] = tz.Replace("SQLLiteDB.db", "AccessDB.accdb");
                cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security" +
            " Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
                ConfigRepository.Instance()["dlcm_chosendb"] = ".accdb";
                ConfigRepository.Instance()["dlcm_ShowMessages"] = System.Environment.GetEnvironmentVariable("COMPUTERNAME") + ";" + DateTime.Now.ToString("yyyyMMdd");

            }
            if (result1 == DialogResult.Cancel)
            {
                ConfigRepository.Instance()["dlcm_AdditionalManipul120"] = "Yes";
                ConfigRepository.Instance()["dlcm_ShowMessages"] = System.Environment.GetEnvironmentVariable("COMPUTERNAME") + ";" + DateTime.Now.ToString("yyyyMMdd");
            }
            else
            {
                ConfigRepository.Instance()["dlcm_AdditionalManipul120"] = "No";
                ConfigRepository.Instance()["dlcm_ShowMessages"] = "";
            }

            var config = AppWD + "\\..\\..\\..\\..\\..\\..\\RocksmithToolkitLib\\RocksmithToolkitLib.Config.xml";
            if (File.Exists(config))
            {
                //Remove end empty lines
                //AND count no of lines
                var info = File.OpenText(config);
                string line;
                //Removes empty end lines or lines with late timing
                var nolines = 0;
                using (StreamWriter sw = File.CreateText(config + ".newvcl"))
                {
                    while ((line = info.ReadLine()) != null)
                    {
                        if (!(line.Contains("<Config Key=\"dlcm_ShowMessages\"")))
                            sw.WriteLine(line);
                        else
                            sw.WriteLine("  <Config Key=\"dlcm_ShowMessages\" Value=\"" + ConfigRepository.Instance()["dlcm_ShowMessages"] + "\" Descr=\"\" />;");
                    }
                }
                info.Close();
                File.Copy(config + ".newvcl", config, true);
                DeleteFile(config + ".newvcl", false);
            }
            //}
            //#endif
            DBPathChange(c("dlcm_DBFolder"), c("dlcm_Configurations"));
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
                        ShowConnectivityError(ex, "NO M$ DB plugin Currently found (" + cnb.ConnectionString.ToString() + ")\n:" + DisplayData() + "\n2nd FAIL to use M$ ACCESS plugin:\n" + vb + "\n\nError:" + ex);/*, null*/
                        //revert to SQLite
                        if (File.Exists(tz.Replace("AccessDB.accdb", "SQLLiteDB.db")))
                        {
                            ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                            ConfigRepository.Instance()["dlcm_DBFolder"] = tz.Replace("AccessDB.accdb", "SQLLiteDB.db"); ;
                        }
                        else MessageBox.Show("No Microsoft Access or SQLite databases" +
                            //" i.e.: C:\\Program Files\\SQLite ODBC Driver for Win64 (existing: "+ (Directory.Exists("C:\\Program Files\\SQLite ODBC Driver for Win64") ? "true":"false")+ ")" +
                            "\n(or access;plugins etc) available. Good Luck as (the) C-DLC Manager wont really work!");
                    }
                }
            else
                try
                {
                    ConfigRepository.Instance()["dlcm_DBFolder"] = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
                    if (File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"])) cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
                    ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                }
                catch (Exception exx)
                {
                    ShowConnectivityError(exx, "");/*, null*/
                }
        }
        public static bool ShowMessagesCheck()
        {

            return ConfigRepository.Instance()["dlcm_AdditionalManipul120"] == "Yes"
            && !(ConfigRepository.Instance()["dlcm_ShowMessages"].Contains(System.Environment.GetEnvironmentVariable("COMPUTERNAME"))
            && ConfigRepository.Instance()["dlcm_ShowMessages"].Contains(DateTime.Now.ToString("yyyyMMdd")) ? true : false);
        }

        public static bool DBPathChange(string txt_DBFolder, string chbx_Configurations)
        {
            bool t = PathChange(txt_DBFolder, chbx_Configurations);
            return t;
        }

        static public bool PathChange(string txt_DBFolder, string chbx_Configurations)
        {

            var tt = ConfigRepository.Instance()["dlcm_DBFolder"];
            //var tr = cnb.DataSource.ToString();
            //var tz = cnc.DatabasePath.ToString();
            try
            {
                if (!File.Exists(txt_DBFolder)) dbmissingHandle("");
                var cnn = "";
                if (cnb is not null && cnb.State.ToString() == "Opened")
                {
                    if (cnb.DataSource.Contains(".accdb") && cnb.DataSource == c("dlcm_DBFolder")) return false;
                    else if (c("dlcm_DBFolder").Contains(".accdb")) cnn = cnb.DataSource;
                }//if (cnb.DataSource.Contains(".accdb")) cnn = cnb.DataSource;
                if (cnc is not null)
                {
                    if (cnc is not null) if (cnc.DatabasePath.Contains(".db") && cnc.DatabasePath == c("dlcm_DBFolder")) return false;
                        else if (c("dlcm_DBFolder").Contains(".db")) cnn = cnc.DatabasePath;
                } //if (cnc.DatabasePath.Contains(".db")) cnn = cnc.DatabasePath;

                if (cnn == "") return false;

                DialogResult result1 = DialogResult.No;
                result1 = MessageBox.Show("Do you want change DB (\nconnection: " + cnn + "\n param: " + c("dlcm_DBFolder") +
                    ")?\n\nIf you chose No you can still modify/update profile, then change DB", "DB Change DETECTED!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result1 == DialogResult.No) return false;
                if (File.Exists(txt_DBFolder))
                {
                    var cmd = "SELECT * FROM Groups WHERE Groupz=\"" + txt_DBFolder + "\" AND Comments =\"dlcm_DBFolder\" AND Profile_Name=\"" + chbx_Configurations + "\" and Type=\"Profile\";";

                    if (cnn is not null && cnn != "")
                    {
                        DialogResult result2 = DialogResult.No;
                        DataSet ddzv = new DataSet(); ddzv = SelectFromDB("Groups", cmd, txt_DBFolder, cnb, cnc);
                        int max = GetNoRec(ddzv, cnb, cnc); //ddzv.Tables.Count > 0 ? ddzv.Tables.Count : 0;
                        if (txt_DBFolder != tt && max == 0) result2 = MessageBox.Show("Do you want to automatically Save the DB Folder Path change to " + txt_DBFolder
                            + ") in the Profile (" + chbx_Configurations + ") or MANUALLY update entry in ACCDB/DB, Groupz table, entry/Comments field: dlcm_DBFolder, Groupz value.", "DB Change DETECTED!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        //if (result2 == DialogResult.No) ;
                        else
                        {
                            var cmdz = "UPDATE Groups SET Groupz=\"" + txt_DBFolder + "\" WHERE Comments=\"dlcm_DBFolder\" AND Profile_Name=\"" + chbx_Configurations + "\" and Type=\"Profile\";";
                            DataSet dts = new DataSet(); dts = UpdateDB("Groups", cmdz, cnb, cnc);
                            //MessageBox.Show("Note Prev DB Profile's DB folder is not changed automatically, so you need to udpate that yourself in the old DB."
                            //       , MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    try
                    {
                        if (cnb is not null) cnb.Close(); if (cnc is not null) cnc.Close();
                        cnb.ConnectionString = "Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security Info=False;Mode= Share Deny None;Data Source=" + txt_DBFolder;
                        OpenDb();
                    }
                    catch (Exception ex) { var tsst = "Errorchange path..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                    //ConfigRepository.Instance()["dlcm_DBFolder"] = txt_DBFolder.Text;

                    cmd = "UPDATE Groups SET Groupz=\"" + txt_DBFolder + "\" WHERE Comments=\"dlcm_DBFolder\" AND Type=\"Profile\" AND Profile_Name=\"Default\"";/*AND Comments=\"" + c(dss.Tables[0].Rows[j][0].ToString()) + "\"chbx_Configurations.Text*/
                    UpdateDB("Groups", cmd, cnb, cnc);
                    cmd = "UPDATE Groups SET Groupz=\"Default\" WHERE Comments=\"dlcm_Configurations\" AND Type=\"Profile\" AND Profile_Name=\"Default\"";/*AND Comments=\"" + c(dss.Tables[0].Rows[j][0].ToString()) + "\"chbx_Configurations.Text*/
                    UpdateDB("Groups", cmd, cnb, cnc);

                    //if (txt_DBFolder != MyAppWD + "\\..\\AccessDB.accdb" || txt_DBFolder != MyAppWD + "\\..\\SLQLiteDB.db") chbx_DefaultDB.Checked = false;

                }
            }
            catch (Exception ex)
            {
                var tsst = "Error at change path..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                dbmissingHandle(ex.Message.ToString());
            }
            return true;
        }

        public static void ShowConnectivityError(Exception ex, string txt)/*, System.Windows.Forms.Label lbl*/
        {
            var mssg = "You need to Download Connectivity patch 32/64 bit v2010 (allows usage by 3rd party DB editors;also avail. at: " +
                AppWD + "\\AccessDatabaseEngine.exe)/2013-2016 (assumingly bet perf/comp)  to match your version of Office @ " +
         txt + ".\n" + "Reinstall in case you feel/see plugin there or is not diplayed when manually checking by running in Windows PowerShell:\n\n" +
         "(New-Object system.data.oledb.oledbenumerator).GetElements() | select SOURCES_NAME, SOURCES_DESCRIPTION\n\n" +
         "and if intending to install the x64 variant of plugin, please note in order to have it on top of Office 32bit," +
         " then you need to decompress it (64b also avail. at: " + c("dlcm_AccessACE.OLEDB.16.0Local64b") + ") and the .msi with /passive flag\n\nError:ex";

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
                if (File.Exists(c("dlcm_AccessACE.OLEDB.16.0Local64b")) || File.Exists(c("dlcm_AccessACE.OLEDB.16.0Local32b")) || File.Exists(c("dlcm_sqliteodbc")))
                {
                    DialogResult result1 = DialogResult.Cancel;
                    result1 = MessageBox.Show("Plugins Currently found for " + cnb.ConnectionString.ToString() + ":\n" + DisplayData()
                        + "\nAs no M$ Access connectivity plugin was found," +
                    " Do you want to:\n 1. (Yes) Install the locally stored 32 bit version (if matching your Office installation;" +
                    " if interested in using 3rd party Freeware ACCDB Editors <best is the 2010 version>)" +
                    "\n 2. (No) Install the locally stored 64 bit version (if matching your Excel installation) , or" +
                    "\n 3. (Cancel)" +
                    "\n \ta) Download by your self using the subsecvent instructions" +
                    "\n \tb) Use SQL-lite3 as DB (you can still use M$ Access (LINKDB.accdb) to access the DB using a OLEDBC plugin i.e.\n(1) " +
                    c("dlcm_AccessACE.OLEDB.16.0Local64b") + "\n(2) " + c("dlcm_AccessACE.OLEDB.16.0Local32b") + "\n(3) " + c("dlcm_sqliteodbc") + ").\n\nError:" + ex
                    , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes) xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_AccessACE.OLEDB.16.0Local32b"));
                    if (result1 == DialogResult.No) xx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, c("dlcm_AccessACE.OLEDB.16.0Local64b"));

                    if (!File.Exists(xx))
                    {
                        if (result1 != DialogResult.Cancel)
                        {
                            ErrorWindow frm2 = new ErrorWindow("Selected bit version(" + xx + ") not available instead the other one is there. close the program and open to try again" +
                            " or manually install it as pe subsecvent instructions", ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]]
                            , "Missing " + xx, false, false, true, "", "", "", false);
                            frm2.ShowDialog();
                        }
                        else
                        {
                            DialogResult result3 = DialogResult.Cancel;
                            result3 = MessageBox.Show("1. (Yes) Use M$ Access \n2. (No) Use SQL-lite3\n3. (Cancel) Don't use any DB (or any DCLM feature :) )"
                            , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                            if (result3 == DialogResult.No)/*&& tz.Contains(ConfigRepository.Instance()["dlcm_chosendb"]) */
                            {
                                ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
                                ConfigRepository.Instance()["dlcm_DBFolder"] = c("dlcm_DBFolder").Replace("AccessDB.accdb", "SQLLiteDB.db");
                                ConfigRepository.Instance()["dlcm_chosendb"] = ".db";
                            }
                            if (result3 == DialogResult.Yes)/*&& tz.Contains(ConfigRepository.Instance()["dlcm_chosendb"]) */
                            {
                                ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "No";
                                ConfigRepository.Instance()["dlcm_DBFolder"] = c("dlcm_DBFolder").Replace("SQLLiteDB.db", "AccessDB.accdb");
                                cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security" +
                            " Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
                                ConfigRepository.Instance()["dlcm_chosendb"] = ".accdb";
                                ConfigRepository.Instance()["dlcm_ShowMessages"] = System.Environment.GetEnvironmentVariable("COMPUTERNAME") + ";" + DateTime.Now.ToString("yyyyMMdd");

                            }
                            //if (result1 == DialogResult.Cancel)
                            //{
                            //    ConfigRepository.Instance()["dlcm_AdditionalManipul120"] = "Yes";
                            //    ConfigRepository.Instance()["dlcm_ShowMessages"] = System.Environment.GetEnvironmentVariable("COMPUTERNAME") + ";" + DateTime.Now.ToString("yyyyMMdd");
                            //}

                            ////if (result3 == DialogResult.No) ;
                            //if (result3 == DialogResult.Yes)
                            //{
                            //    ErrorWindow frm2 = new ErrorWindow(mssg
                            //    , ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]],
                            //    "Error @Import", false, false, true, "", "", "", false);
                            //    frm2.ShowDialog();
                            //}
                            //if (result3 == DialogResult.Cancel) ;
                        }
                    }
                    else
                    if (result1 == DialogResult.Cancel)
                    {
                        ErrorWindow frm1 = new ErrorWindow(mssg
                            , ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]],
                            "Error @Import", false, false, true, "", "", "", false);
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
                            //if (File.Exists(xx))
                            //    ;// GeneralExtension.RunExternalExecutable(xx, false);
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
                            MessageBox.Show("Can not open Local M$ Access plugin install ! " + xx);
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

        static public MainDBfields[] GetRecord_s(string cmd, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            var MaximumSize = 0;
            MainDBfields[] query = new MainDBfields[20000];
            DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb, cnc);

            var i = 0;
            MaximumSize = GetNoRec(dus, cnb, cnc); //dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;

            query[0] = new MainDBfields
            {
                NoRec = MaximumSize.ToString()
            };
            if (MaximumSize == 0) return query;
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
                query[i].Bass_Has_DD = dataRow.ItemArray[83].ToString();
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
                query[i].Is_Acoustic = dataRow.ItemArray[96].ToString();
                query[i].Top10 = dataRow.ItemArray[97].ToString();
                query[i].Has_Other_Officials = dataRow.ItemArray[98].ToString();
                query[i].Spotify_Song_ID = dataRow.ItemArray[99].ToString();
                query[i].Spotify_Artist_ID = dataRow.ItemArray[100].ToString();
                query[i].Spotify_Album_ID = dataRow.ItemArray[101].ToString();
                query[i].Spotify_Album_URL = dataRow.ItemArray[102].ToString();
                query[i].Audio_OrigHash = dataRow.ItemArray[103].ToString();
                query[i].Audio_OrigPreviewHash = dataRow.ItemArray[104].ToString();
                query[i].AlbumArt_OrigHash = dataRow.ItemArray[105].ToString();
                query[i].Duplicate_Of = dataRow.ItemArray[106].ToString();
                query[i].Split4Pack = dataRow.ItemArray[107].ToString();
                query[i].UseInternalDDRemovalLogic = dataRow.ItemArray[108].ToString();
                query[i].Is_Instrumental = dataRow.ItemArray[109].ToString();
                query[i].Is_Single = dataRow.ItemArray[110].ToString();
                query[i].Is_Soundtrack = dataRow.ItemArray[111].ToString();
                query[i].Is_EP = dataRow.ItemArray[112].ToString();
                query[i].Has_Had_Audio_Changed = dataRow.ItemArray[113].ToString();
                query[i].Has_Had_Lyrics_Changed = dataRow.ItemArray[114].ToString();
                query[i].Album_Sort = dataRow.ItemArray[115].ToString();
                query[i].Is_Uncensored = dataRow.ItemArray[116].ToString();
                query[i].IntheWorks = dataRow.ItemArray[117].ToString();
                query[i].LyricsLanguage = dataRow.ItemArray[118].ToString();
                query[i].LastConversionDateTime = dataRow.ItemArray[119].ToString();
                query[i].ImprovedWithDM = dataRow.ItemArray[120].ToString();
                query[i].Is_FullAlbum = dataRow.ItemArray[121].ToString();
                query[i].PitchShiftableEsOrDd = dataRow.ItemArray[122].ToString();
                query[i].Import_AuditTrail_ID = dataRow.ItemArray[123].ToString();
                query[i].Is_Remastered = dataRow.ItemArray[124].ToString();
                query[i].EoFPath = dataRow.ItemArray[125].ToString();
                query[i].Is_Karaoke = dataRow.ItemArray[126].ToString();
                query[i].Is_Cover = dataRow.ItemArray[127].ToString();
                query[i].Has_Featuring = dataRow.ItemArray[128].ToString();
                query[i].Is_Demo = dataRow.ItemArray[129].ToString();
                query[i].Is_Remix = dataRow.ItemArray[130].ToString();
                query[i].BasedOn_Youtube = dataRow.ItemArray[131].ToString();
                query[i].BasedOn_CF = dataRow.ItemArray[132].ToString();
                query[i].BasedOn_Tabs = dataRow.ItemArray[133].ToString();
                query[i].ToDos = dataRow.ItemArray[134].ToString();
                query[i].ToneDetails = dataRow.ItemArray[135].ToString();
                query[i].PackageDetails = dataRow.ItemArray[136].ToString();
                query[i].PackingDate = dataRow.ItemArray[137].ToString();
                query[i].UpdateVersionDate = dataRow.ItemArray[138].ToString();
                query[i].Has_Capo = dataRow.ItemArray[139].ToString();
                query[i].Has_ShowLights = dataRow.ItemArray[140].ToString();
                query[i].Has_JVocals = dataRow.ItemArray[141].ToString();
                query[i].Is_Medley = dataRow.ItemArray[142].ToString();
                query[i].Is_MultiStrings = dataRow.ItemArray[143].ToString();
                query[i].BasedOn_GP = dataRow.ItemArray[144].ToString();
                query[i].Album_ArtPathOrig = dataRow.ItemArray[145].ToString();
                query[i].Is_Deluxe = dataRow.ItemArray[146].ToString();
                query[i].Is_GreatestHits = dataRow.ItemArray[147].ToString();
                query[i].Is_Midi = dataRow.ItemArray[148].ToString();
                query[i].Is_GameSoundtrack = dataRow.ItemArray[149].ToString();
                query[i].Is_AmateurCover = dataRow.ItemArray[150].ToString();
                query[i].Is_TVTheme = dataRow.ItemArray[151].ToString();
                //query[i].Has_Alternate_Audio = dataRow.ItemArray[152].ToString();
                //query[i].Has_Alternate_Lyrics = dataRow.ItemArray[153].ToString();
                i++;
                query[i] = new MainDBfields();
            }
            return query;
        }

        static public string GetHash(string filename)
        {
            if (filename == "") return "";
            //Generating the HASH code
            var FileHash = "";
            try
            {
                if (File.Exists(filename))
                {
                    var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    SHA1 sha = new SHA1Managed();
                    using (var sr = new StreamReader(fs))
                    {
                        try
                        {
                            byte[] hashBytes = sha.ComputeHash(fs); fs.Close();
                            FileHash = BitConverter.ToString(hashBytes);
                        }
                        catch (Exception ex) { var tsst = "Erro ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                        sr.Close();
                    }
                }
            }
            catch (Exception ex) { var tsst = "Erro ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            return FileHash;
        }

        static public void CheckValidityGetHASHAdd2ImportMono(string e)
        {

        }
        static public string CheckValidityGetHASHAdd2ImportPub(string e)
        {

            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = c("dlcm_TempPath");

            string[] args = e.Split(';');
            string s = args[0];
            string i = args[1];
            string ImportPackNo = args[2];
            var invalid = "No";
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            var tsst = "Start Gathering ..."; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, i, "", null, null);


            if (!s.IsValidPSARC())
            {
                timestamp = UpdateLog(timestamp, "error at import " + string.Format("File '{0}' isn't valid. File extension was changed to '.invalid'",
                    Path.GetFileName(s)), true, tmpPath, "", "", null, null);
                if (!File.Exists(s) && File.Exists(s.Replace(".psarc", ".invalid"))) File.Move(s.Replace(".psarc", ".invalid"), s);
                invalid = "Yes";
            }

            //try to get the details
            // Create the FileInfo object only when needed to ensure 
            // the information is as current as possible.
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(s);
            }
            catch (Exception ee)
            {
                timestamp = UpdateLog(timestamp, "error at import" + ee.Message, true, tmpPath, "", "DLCManager", null, null);
                return null;
                //continue;
            }

            //details end

            //Generating the HASH code
            string FileHash = GetHash(s);
            string plt = fi.FullName.GetPlatform().platform.ToString();

            //Populate ImportDB
            string tst = "Check validity gather info on File " + (i + 1) + " :" + s;
            string tre = "\n" + tst;


            var ff = "-";
            ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;

            //var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";

            //var insertvalues = 
            return "\"" + s + "\";\"" + fi.DirectoryName + "\";\"" + fi.Name + "\";\"" + fi.CreationTime + "\";\""
            + FileHash + "\";\"" + fi.Length + "\";\"" + ff + "\";\"" + ImportPackNo + "\";\"" + (plt == "Pc" ? "Pc" : plt) + "\";\"" + invalid + "\"";

            //InsertIntoDBwValues("Import", insertcmdd, insertvalues, cnb, 0);

            //e.Result = "Done";
            //cnb.Close();
        }


        static public string AddIntheWorks(string s)
        {
            if (s.Contains("<IntheWorksWDetails>")) s = s.Replace("<IntheWorksWDetails>", "<IntheWorks>").Replace("<Author>", "").Replace("<Version>", "").Replace("<TimestampShort>", "") + "<Author>" + "<Version>" + "<TimestampShort>";

            return s;
        }

        static public string gettablename(string slctcmd)/**/
        {
            //get&split field names as to allow conversion to dataset from recordset, in the order of the select
            var tbname = slctcmd.Substring(slctcmd.IndexOf(" FROM ") + 6);
            if (tbname.Contains(" ")) tbname = tbname.Substring(0, tbname.IndexOf(" ")).Trim();
            if (tbname.Length > 18 || tbname.Length == 0)
                tbname = "Main";
            return tbname;
        }
        static public DataSet LiustToDataSet<T>(List<T> list, string slctcmd)/**/
        {
            var elementType = typeof(T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);

            //get&split field names as to allow conversion to dataset from recordset, in the order of the select
            var tbname = gettablename(slctcmd);
            ds.Tables[0].TableName = tbname;

            var slctfld = slctcmd.Substring(7, slctcmd.ToLower().IndexOf(" FROM ".ToLower()) - 6).Trim();
            string[] ret = slctfld.Split(',');

            if (slctfld.Trim() == "*")
            {
                foreach (var propInfo in elementType.GetProperties())//add a column to table for each public property on T. create a list of fields too
                {
                    if (propInfo.Name == "NoRec")
                        continue; //ignore NoRec as a vurtual column/field
                    var colTypef = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                    t.Columns.Add(propInfo.Name, colTypef);
                    slctfld += propInfo.Name.ToLower() + ",";
                }

                if (slctfld.Substring(slctfld.Length - 1) == ",")
                {
                    slctfld = slctfld.Substring(1, slctfld.Length - 2);
                    ret = null;
                    ret = slctfld.Split(',');
                }
            }
            //if (slctcmd == "SELECT DISTINCT CAST(CapoFret as numeric) as CapoFret FROM Arrangements WHERE CapoFret<>' AND CapoFret<>'0';")
            //    ;
            //if (slctcmd == "SELECT max(CAST(Pack as numeric)) as ID FROM Main;")
            //    ;
            //if (slctcmd.Contains(".") && !slctcmd.ToUpper().Contains(" AS "))
            //    ;
            for (var i = 0; i < ret.Count(); i++) //clean fields names
            {
                var tg = "";
                tg = ret[i].ToLower().Replace("DISTINCT".ToLower(), "").Replace("CAST(".ToLower(), "").Replace("as numeric".ToLower(), "")
                    .Replace("COUNT(".ToLower(), "").Replace("MAX(".ToLower(), "");//.Replace("VAL(".ToLower(), "").Replace("CSTR(".ToLower(), "").Replace("SWITCH(".ToLower(), "");
                                                                                   //if (tg.ToLower().Contains(" as ") && (ret[i].ToLower().Contains("DISTINCT".ToLower()) || ret[i].ToLower().Contains("CAST(".ToLower()) || ret[i].ToLower().Contains("COUNT(".ToLower()))
                                                                                   //    /*&& !ret[i].ToLower().Contains("MAX(".ToLower())*/
                                                                                   //    )
                                                                                   //    tg = tg.Substring(0, tg.ToLower().IndexOf(" as "));
                                                                                   //else
                if (tg.ToLower().Contains(" as ")) tg = tg.Substring(tg.ToLower().IndexOf(" as ") + 4);
                ret[i] = tg.Replace(")", "").Trim();
                //if (ret[i].Trim().Contains("(") || ret[i].Trim().Contains(")") || ret[i].Trim().Contains(" as "))
                //    ;
            }

            if (slctcmd.Substring(7, slctcmd.ToLower().IndexOf(" FROM ".ToLower()) - 6).Trim() != "*")
                foreach (var fld in ret)
                    foreach (var propInfo in elementType.GetProperties())//add a column to table for each public property on T
                        if (fld == propInfo.Name.ToLower())
                        {
                            t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType);
                            break;
                        }

            foreach (var item in list)//go through each property on T and add each value to the table
            {
                var row = t.NewRow();
                foreach (var propInfo in elementType.GetProperties())
                    foreach (var fld in ret)
                        if (fld == propInfo.Name.ToLower())
                        {
                            var fg = propInfo.GetValue(item, null);
                            row[propInfo.Name] = propInfo.GetValue(item, null);
                            break;
                        }
                t.Rows.Add(row);
            }

            try
            {
                var c = GetNoRec(ds, cnb, cnc);
                if (c == 0)
                    ; //
                      //        //ds.Tables.Count 
                      //        //    if (ds.Tables[0].Rows.Count > 0)
                      //        //if (ds.Tables[0].Rows[0].ItemArray[0].ToString() == "")
                      //        //    ;
            }
            catch (Exception ex)
            {
                ;
            }
            return ds;
        }

        static public void DeleteFromDB(string DB, string slct, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            slct = Conv2SQLLite(slct);
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            try
            {
                DataSet dss = new DataSet();
                if (DB_Path.Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
                {
                    slct = slct.Replace("\"", "'");
                    slct = slct.Replace("DELETE *", "DELETE");
                    int modifiedRows = cnc.Execute(slct);
                }
                else
                {
                    OleDbDataAdapter dan = new OleDbDataAdapter(slct, cnb);
                    dan.Fill(dss, "DB");
                    dan.Dispose();
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error4 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                ShowConnectivityError(ex, DB + "--------" + slct + "--------------");/*, null*/
                return;
            }
        }

        static public void UpdateDBbyExecuteNonQuery(OleDbCommand command, OleDbConnection connection, SQLite.SQLiteConnection cnc)
        {

            var f = 0;
            if (c("dlcm_DBFolder").Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
            {
                string commandString = command.CommandText;
                if (commandString.Contains("INSERT"))
                {
                    commandString = commandString.Replace("VALUES(", "VALUES (");
                    commandString = commandString.Contains("VALUES (") ? commandString.Substring(0, commandString.IndexOf("VALUES (")) + "VALUES (" : commandString;
                    foreach (OleDbParameter parameter in command.Parameters)
                        commandString += "\"" + parameter.Value.ToString() + "\", "; //commandString.Replace(parameter.ParameterName.ToString() + ", ", "\"" +)
                    commandString = commandString.Substring(0, commandString.Length - 2) + ");";
                }
                else
                {
                    foreach (OleDbParameter parameter in command.Parameters)
                        commandString = commandString.Replace(parameter.ParameterName.ToString() + ", ", "\"" + parameter.Value.ToString() + "\", "); //
                }
                var dfs = UpdateDB(gettablename(commandString), commandString, connection, cnc);
            }
            else
                //'30.10 using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + c("dlcm_AccessDLLVersion") + ";Data Source=" + c("dlcm_DBFolder")))
                //{
                try
                {
                    //using (var tx = new TransactionScope()) { 
                    //    var cmd = connection.CreateCommand();
                    //    cmd.CommandType = CommandType.Text;
                    //    cmd.CommandText = command.CommandText;
                    //var nsp = command.Parameters.Cast<ICloneable>().Select(x => x.Clone() as SqlParameter).Where(x => x != null).ToArray();
                    // Copy parameters into another command
                    ///*command*/.Parameters.AddRange(nsp);

                    if (connection.State.ToString() == "Closed") connection.Open();
                    if (command.Connection.State.ToString().ToLower() == "Closed".ToLower())
                    {
                        command.Connection.Open();
                        f = 1;
                    }
                    //OleDbTransaction OrderTrans = cnn.BeginTransaction();

                    //SqlCommand cmd = new SqlCommand();
                    //cmd.Connection = cnn;
                    //cmd.Transaction = OrderTrans;
                    //var dbTransaction = cnn.BeginTransaction();
                    //cmd.Transaction = dbTransaction;
                    //cmd.Transaction = true;
                    command.ExecuteNonQuery();
                    //OrderTrans.Commit();
                    //tx.Commit();
                    //} 
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; var timestamp = UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + c("dlcm_DBFolder") + "-" + command.CommandText + ex.Message);
                }
            //finally { if (connection != null) connection.Close(); }
            //}

            if (c("dlcm_Autosave").ToLower() != "Yes".ToLower()) MessageBox.Show("Song Details Changes Saved");
            //return dsm;
        }

        private List<Tuple<string, SqlDbType, string>> where_param;
        public IEnumerable<SqlParameter> RecycledParameters()
        {
            foreach (Tuple<string, SqlDbType, string> tuple in where_param)
            {
                SqlParameter local_arg = new SqlParameter(tuple.Item1, tuple.Item2);
                local_arg.Value = tuple.Item3;
                yield return local_arg;
            }
        }

        private DbParameterCollection cloneParms(DbCommand commandWithParms)
        {
            return ObjectCopier.Clone<DbParameterCollection>(commandWithParms.Parameters);
        }

        public static class ObjectCopier
        {
            /// <summary>
            /// Perform a deep Copy of the object.
            /// </summary>
            /// <typeparam name="T">The type of object being copied.</typeparam>
            /// <param name="source">The object instance to copy.</param>
            /// <returns>The copied object.</returns>
            public static T Clone<T>(T source)
            {
                if (!typeof(T).IsSerializable)
                {
                    throw new ArgumentException("The type must be serializable.", "source");
                }

                // Don't serialize a null object, simply return the default for that object
                if (System.Object.ReferenceEquals(source, null))
                {
                    return default(T);
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
        }

        static public DataSet UpdateDB(string ftable, string fcmds, OleDbConnection cn, SQLite.SQLiteConnection cnc)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DataSet dsm = new DataSet();
            try
            {
                if (File.Exists(DB_Path))
                {
                    if ((DB_Path.Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes") && cnc is not null)
                    {
                        fcmds = Conv2SQLLite(fcmds);

                        int modifiedRows = cnc.Execute(fcmds);

                        //var shrinkCommand = cnc.CreateCommand("vacuum");
                        //shrinkCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter
                        {
                            SelectCommand = new OleDbCommand(fcmds, cn)
                        };
                        OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);
                        try
                        {
                            myDataAdapter.Fill(dsm, ftable);
                            return dsm;
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error8 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            ShowConnectivityError(ex, ftable + "--------" + fcmds + "--------------");/*, null*/
                            return dsm;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                DateTime timestamp;
                timestamp = UpdateLog(DateTime.Now, "error at import " + ee.Message, true, null, null, "", null, null);
                ShowConnectivityError(ee, ftable + "--------" + fcmds + "--------------");/*, null*/
            }
            return dsm;
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues, OleDbConnection cnb, int mutit, SQLite.SQLiteConnection cnc)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            string insertcmd;


            //fcmds = fcmds.Replace("\"", "'");
            //if (fvalues.Substring(fvalues.Length - 1) != ";") fvalues += ";";
            if (fvalues.ToLower().IndexOf("select ") == 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
            else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";

            if (DB_Path.Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
            {
                fvalues = Conv2SQLLite(insertcmd);
                try
                {

                    int modifiedRows = cnc.Execute(fvalues);
                }
                catch (Exception egx)
                {
                    DateTime timestamp;
                    timestamp = UpdateLog(DateTime.Now, "error at import " + egx.Message + "\n" + egx.Message + "-" + insertcmd, true, null, mutit.ToString(), "", null, null);
                    ShowConnectivityError(egx, ftable + "--------" + fvalues + "--------------");/*, null*/
                }
                //var shrinkCommand = cnc.CreateCommand("vacuum");
                //shrinkCommand.ExecuteNonQuery();
            }
            else
            {
                try
                {
                    DataSet dsm = new DataSet();
                    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    dab.Fill(dsm, ftable);
                    dab.Dispose();
                }

                catch (Exception ee)
                {
                    try
                    {
                        DataSet dsm = new DataSet();
                        if (fvalues.ToLower().IndexOf("select ") == 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                        else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                        OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                        dab.Fill(dsm, ftable);
                        dab.Dispose();
                    }
                    catch (Exception ex)
                    {
                        string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
                        string tmpPath = c("dlcm_TempPath");
                        if (fvalues.ToLower().IndexOf("select ") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                        else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                        cnb.Close();
                        OpenDb();
                        DataSet dsm = new DataSet();
                        OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                        try
                        {
                            dab.Fill(dsm, ftable);
                        }
                        catch (Exception egx)
                        {
                            dab.Dispose();
                            DateTime timestamp;
                            timestamp = UpdateLog(DateTime.Now, "error at import " + ee.Message + "\n" + egx.Message + "-" + insertcmd, true, tmpPath, mutit.ToString(), "", null, null);
                            ShowConnectivityError(ex, ftable + "--------" + fvalues + "--------------");/*, null*/
                        }
                    }
                }
            }
        }
        static public string sqladapt(string fcmds, string start_old, string start_new, string end_new)
        {
            if (!fcmds.ToLower().Contains(start_old.ToLower())) return fcmds;
            var new_fcmds = "";
            var endreached = true;

            fcmds = fcmds.Replace("\"", "'");
            for (var i = 0; i < fcmds.Length - 1; i++)
            {
                if (fcmds[i].ToString() == "(")
                    if (i > start_old.Length + 1)
                        if (fcmds.Substring(i - start_old.Length + 1, start_old.Length).ToString().ToLower() == start_old.ToLower())
                        {
                            fcmds = fcmds.Substring(0, i - start_old.Length + 1) + start_new + fcmds.Substring(i + 1, fcmds.Length - i - 1);
                            endreached = false;
                        }

                if (fcmds[i].ToString() == ")" && !endreached)
                {
                    if (i + 1 <= fcmds.Length - 1) if (fcmds[i + 1].ToString() == "'") continue;
                    fcmds = fcmds.Substring(0, i - 1 + 1) + end_new + fcmds.Substring(i + 1, fcmds.Length - i - 1);
                    endreached = true;
                }
            }
            if (fcmds.Substring(fcmds.Length - 1) != ";") fcmds += ";";
            //commenting as SQLIite should accept " and would not need ' as reülpacement as lsots of songs have '
            //fcmds = fcmds.Replace("\"\"", "''");
            //fcmds = fcmds.Replace("\"", "'");
            //fcmds = fcmds.Replace("'", "'");
            return fcmds;
        }
        static public string Conv2SQLLite(string fcmds)
        {
            //fcmds = fcmds.Replace("\"\"", "''");
            //fcmds = fcmds.Replace("\"", "'");
            //fcmds = fcmds.Replace("'", "'");

            fcmds = fcmds.Replace("Val(", "VAL(").Replace("val(", "VAL(");
            fcmds = fcmds.Replace("Cstr(", "CSTR(").Replace("cstr(", "CSTR(");
            fcmds = fcmds.Replace("Len(", "LEN(").Replace("len(", "LEN(");
            fcmds = fcmds.Replace("Mid(", "MID(").Replace("mid(", "MID(");
            fcmds = fcmds.Contains("StrComp") ? fcmds.Replace(", 0", ",0").Replace(",0", "$0").Replace(",", "=").Replace("$0", ",0") : fcmds;
            fcmds = fcmds.Contains("Switch") ? fcmds.Replace(", 1=1, ", " ELSE ").Replace(", [", " THEN [") : fcmds;


            fcmds = sqladapt(fcmds, "VAL(", "CAST(", " as numeric)");
            fcmds = sqladapt(fcmds, "CSTR(", "CAST(", " as string)");
            fcmds = sqladapt(fcmds, "LCASE(", "LOWER(", ")");
            fcmds = sqladapt(fcmds, "UCASE(", "UPPER(", ")");
            fcmds = sqladapt(fcmds, "LEN(", "LENGTH(", ")");
            fcmds = sqladapt(fcmds, "MID(", "SUBSTR(", ")");
            fcmds = sqladapt(fcmds, "StrComp(", "IIF(", ",1)");
            fcmds = sqladapt(fcmds, "Switch(", "(CASE WHEN ", " END)").Replace("]);", "] END);");
            fcmds = fcmds.Replace("+", "||");
            fcmds = fcmds.Replace("&", "||");
            return fcmds;
        }
        static public DataSet SelectFromDB(string ftable, string fcmds, string currentDB, OleDbConnection cn, SQLite.SQLiteConnection cnc)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DataSet dfsm = new DataSet();
            var oldcmd = fcmds;
            var dt = DateTime.Now;

            if (File.Exists(DB_Path))
            {
                DataSet dsm = new DataSet();
                //if (!fcmds.Contains("Profile_Name=\"\""))
                try
                {
                    if (DB_Path.Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
                    {
                        fcmds = Conv2SQLLite(fcmds);
                        if (fcmds.ToLower().Contains(" top "))
                        {
                            if (fcmds.ToLower().Contains(" top 1 ")) fcmds = (fcmds.Replace(" top 1 ", " ").Replace(" Top 1 ", " ").Replace(" TOP 1 ", " ") + " LIMIT 1;").Trim();
                            if (fcmds.ToLower().Contains(" top 2 ")) fcmds = (fcmds.Replace(" top 2 ", " ").Replace(" Top 2 ", " ").Replace(" TOP 2 ", " ") + " LIMIT 2;").Trim();
                            if (fcmds.ToLower().Contains(" top 3 ")) fcmds = (fcmds.Replace(" top 3 ", " ").Replace(" Top 3 ", " ").Replace(" TOP 3 ", " ") + " LIMIT 3;").Trim();
                            if (fcmds.ToLower().Contains(" top 4 ")) fcmds = (fcmds.Replace(" top 4 ", " ").Replace(" Top 4 ", " ").Replace(" TOP 4 ", " ") + " LIMIT 4;").Trim();
                            if (fcmds.ToLower().Contains(" top 5 ")) fcmds = (fcmds.Replace(" top 5 ", " ").Replace(" Top 5 ", " ").Replace(" TOP 5 ", " ") + " LIMIT 5;").Trim();
                            if (fcmds.ToLower().Contains(" top 6 ")) fcmds = (fcmds.Replace(" top 6 ", " ").Replace(" Top 6 ", " ").Replace(" TOP 6 ", " ") + " LIMIT 6;").Trim();
                            if (fcmds.ToLower().Contains(" top 7 ")) fcmds = (fcmds.Replace(" top 7 ", " ").Replace(" Top 7 ", " ").Replace(" TOP 7 ", " ") + " LIMIT 7;").Trim();
                            if (fcmds.ToLower().Contains(" top 8 ")) fcmds = (fcmds.Replace(" top 8 ", " ").Replace(" Top 8 ", " ").Replace(" TOP 8 ", " ") + " LIMIT 8;").Trim();
                            if (fcmds.ToLower().Contains(" top 9 ")) fcmds = (fcmds.Replace(" top 9 ", " ").Replace(" Top 9 ", " ").Replace(" TOP 9 ", " ") + " LIMIT 9;").Trim();
                            if (fcmds.ToLower().Contains(" top 10 ")) fcmds = (fcmds.Replace(" top 10 ", " ").Replace(" Top 10 ", " ").Replace(" TOP 10 ", " ") + " LIMIT 10;").Trim();
                            if (fcmds.ToLower().Contains(" top 11 ")) fcmds = (fcmds.Replace(" top 11 ", " ").Replace(" Top 11 ", " ").Replace(" TOP 11 ", " ") + " LIMIT 11;").Trim();
                            if (fcmds.ToLower().Contains(" top 12 ")) fcmds = (fcmds.Replace(" top 12 ", " ").Replace(" Top 12 ", " ").Replace(" TOP 12 ", " ") + " LIMIT 12;").Trim();
                            if (fcmds.ToLower().Contains(" top 13 ")) fcmds = (fcmds.Replace(" top 13 ", " ").Replace(" Top 13 ", " ").Replace(" TOP 13 ", " ") + " LIMIT 13;").Trim();
                            if (fcmds.ToLower().Contains(" top 14 ")) fcmds = (fcmds.Replace(" top 14 ", " ").Replace(" Top 14 ", " ").Replace(" TOP 14 ", " ") + " LIMIT 14;").Trim();
                            if (fcmds.ToLower().Contains(" top 15 ")) fcmds = (fcmds.Replace(" top 15 ", " ").Replace(" Top 15 ", " ").Replace(" TOP 15 ", " ") + " LIMIT 15;").Trim();
                            if (fcmds.ToLower().Contains(" top 16 ")) fcmds = (fcmds.Replace(" top 16 ", " ").Replace(" Top 16 ", " ").Replace(" TOP 16 ", " ") + " LIMIT 16;").Trim();
                            if (fcmds.ToLower().Contains(" top 17 ")) fcmds = (fcmds.Replace(" top 17 ", " ").Replace(" Top 17 ", " ").Replace(" TOP 17 ", " ") + " LIMIT 17;").Trim();
                            if (fcmds.ToLower().Contains(" top 18 ")) fcmds = (fcmds.Replace(" top 18 ", " ").Replace(" Top 18 ", " ").Replace(" TOP 18 ", " ") + " LIMIT 18;").Trim();
                            if (fcmds.ToLower().Contains(" top 19 ")) fcmds = (fcmds.Replace(" top 19 ", " ").Replace(" Top 19 ", " ").Replace(" TOP 19 ", " ") + " LIMIT 19;").Trim();
                            if (fcmds.ToLower().Contains(" top 20 ")) fcmds = (fcmds.Replace(" top 20 ", " ").Replace(" Top 20 ", " ").Replace(" TOP 20 ", " ") + " LIMIT 20;").Trim();
                            if (fcmds.ToLower().Contains(" top 21 ")) fcmds = (fcmds.Replace(" top 21 ", " ").Replace(" Top 21 ", " ").Replace(" TOP 21 ", " ") + " LIMIT 21;").Trim();
                            if (fcmds.ToLower().Contains(" top 22 ")) fcmds = (fcmds.Replace(" top 22 ", " ").Replace(" Top 22 ", " ").Replace(" TOP 22 ", " ") + " LIMIT 22;").Trim();
                            if (fcmds.ToLower().Contains(" top 23 ")) fcmds = (fcmds.Replace(" top 23 ", " ").Replace(" Top 23 ", " ").Replace(" TOP 23 ", " ") + " LIMIT 23;").Trim();
                            if (fcmds.ToLower().Contains(" top 24 ")) fcmds = (fcmds.Replace(" top 24 ", " ").Replace(" Top 24 ", " ").Replace(" TOP 24 ", " ") + " LIMIT 24;").Trim();
                            if (fcmds.ToLower().Contains(" top 25 ")) fcmds = (fcmds.Replace(" top 25 ", " ").Replace(" Top 25 ", " ").Replace(" TOP 25 ", " ") + " LIMIT 25;").Trim();
                            if (fcmds.ToLower().Contains(" top 26 ")) fcmds = (fcmds.Replace(" top 26 ", " ").Replace(" Top 26 ", " ").Replace(" TOP 26 ", " ") + " LIMIT 26;").Trim();
                            if (fcmds.ToLower().Contains(" top 27 ")) fcmds = (fcmds.Replace(" top 27 ", " ").Replace(" Top 27 ", " ").Replace(" TOP 27 ", " ") + " LIMIT 27;").Trim();
                            if (fcmds.ToLower().Contains(" top 28 ")) fcmds = (fcmds.Replace(" top 28 ", " ").Replace(" Top 28 ", " ").Replace(" TOP 28 ", " ") + " LIMIT 28;").Trim();
                            if (fcmds.ToLower().Contains(" top 29 ")) fcmds = (fcmds.Replace(" top 29 ", " ").Replace(" Top 29 ", " ").Replace(" TOP 29 ", " ") + " LIMIT 29;").Trim();
                            if (fcmds.ToLower().Contains(" top 30 ")) fcmds = (fcmds.Replace(" top 30 ", " ").Replace(" Top 30 ", " ").Replace(" TOP 30 ", " ") + " LIMIT 30;").Trim();
                            if (fcmds.ToLower().Contains(" top 31 ")) fcmds = (fcmds.Replace(" top 31 ", " ").Replace(" Top 31 ", " ").Replace(" TOP 31 ", " ") + " LIMIT 31;").Trim();
                            if (fcmds.ToLower().Contains(" top 32 ")) fcmds = (fcmds.Replace(" top 32 ", " ").Replace(" Top 32 ", " ").Replace(" TOP 32 ", " ") + " LIMIT 32;").Trim();
                            if (fcmds.ToLower().Contains(" top 33 ")) fcmds = (fcmds.Replace(" top 33 ", " ").Replace(" Top 33 ", " ").Replace(" TOP 33 ", " ") + " LIMIT 33;").Trim();
                            if (fcmds.ToLower().Contains(" top 34 ")) fcmds = (fcmds.Replace(" top 34 ", " ").Replace(" Top 34 ", " ").Replace(" TOP 34 ", " ") + " LIMIT 34;").Trim();
                            if (fcmds.ToLower().Contains(" top 35 ")) fcmds = (fcmds.Replace(" top 35 ", " ").Replace(" Top 35 ", " ").Replace(" TOP 35 ", " ") + " LIMIT 35;").Trim();
                            if (fcmds.ToLower().Contains(" top 36 ")) fcmds = (fcmds.Replace(" top 36 ", " ").Replace(" Top 36 ", " ").Replace(" TOP 36 ", " ") + " LIMIT 36;").Trim();
                            if (fcmds.ToLower().Contains(" top 37 ")) fcmds = (fcmds.Replace(" top 37 ", " ").Replace(" Top 37 ", " ").Replace(" TOP 37 ", " ") + " LIMIT 37;").Trim();
                            if (fcmds.ToLower().Contains(" top 38 ")) fcmds = (fcmds.Replace(" top 38 ", " ").Replace(" Top 38 ", " ").Replace(" TOP 38 ", " ") + " LIMIT 38;").Trim();
                            if (fcmds.ToLower().Contains(" top 39 ")) fcmds = (fcmds.Replace(" top 39 ", " ").Replace(" Top 39 ", " ").Replace(" TOP 39 ", " ") + " LIMIT 39;").Trim();
                            if (fcmds.ToLower().Contains(" top 40 ")) fcmds = (fcmds.Replace(" top 40 ", " ").Replace(" Top 40 ", " ").Replace(" TOP 40 ", " ") + " LIMIT 40;").Trim();
                            if (fcmds.ToLower().Contains(" top 41 ")) fcmds = (fcmds.Replace(" top 41 ", " ").Replace(" Top 41 ", " ").Replace(" TOP 41 ", " ") + " LIMIT 41;").Trim();
                            if (fcmds.ToLower().Contains(" top 42 ")) fcmds = (fcmds.Replace(" top 42 ", " ").Replace(" Top 42 ", " ").Replace(" TOP 42 ", " ") + " LIMIT 42;").Trim();
                            if (fcmds.ToLower().Contains(" top 43 ")) fcmds = (fcmds.Replace(" top 43 ", " ").Replace(" Top 43 ", " ").Replace(" TOP 43 ", " ") + " LIMIT 43;").Trim();
                            if (fcmds.ToLower().Contains(" top 44 ")) fcmds = (fcmds.Replace(" top 44 ", " ").Replace(" Top 44 ", " ").Replace(" TOP 44 ", " ") + " LIMIT 44;").Trim();
                            if (fcmds.ToLower().Contains(" top 45 ")) fcmds = (fcmds.Replace(" top 45 ", " ").Replace(" Top 45 ", " ").Replace(" TOP 45 ", " ") + " LIMIT 45;").Trim();
                            if (fcmds.ToLower().Contains(" top 46 ")) fcmds = (fcmds.Replace(" top 46 ", " ").Replace(" Top 46 ", " ").Replace(" TOP 46 ", " ") + " LIMIT 46;").Trim();
                            if (fcmds.ToLower().Contains(" top 47 ")) fcmds = (fcmds.Replace(" top 47 ", " ").Replace(" Top 47 ", " ").Replace(" TOP 47 ", " ") + " LIMIT 47;").Trim();
                            if (fcmds.ToLower().Contains(" top 48 ")) fcmds = (fcmds.Replace(" top 48 ", " ").Replace(" Top 48 ", " ").Replace(" TOP 48 ", " ") + " LIMIT 48;").Trim();
                            if (fcmds.ToLower().Contains(" top 49 ")) fcmds = (fcmds.Replace(" top 49 ", " ").Replace(" Top 49 ", " ").Replace(" TOP 49 ", " ") + " LIMIT 49;").Trim();
                            if (fcmds.ToLower().Contains(" top 50 ")) fcmds = (fcmds.Replace(" top 50 ", " ").Replace(" Top 50 ", " ").Replace(" TOP 50 ", " ") + " LIMIT 50;").Trim();
                        }
                        fcmds = fcmds.Replace(" ;", ";").Replace("; ", ";");
                        fcmds = fcmds.Replace(";LIMIT", " LIMIT");

                        if (fcmds.Substring(fcmds.Length - 1) != ";")
                            fcmds += ";";
                        switch (true)
                        {
                            case true when ftable == "Main":
                                var Mains = cnc.Query<MainDBfields>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Mains, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Arrangements":
                                var Arrangementss = cnc.Query<Arrangements>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Arrangementss, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Cache":
                                var Caches = cnc.Query<Cache>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Caches, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Groups":
                                var Groupss = cnc.Query<Groups>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Groupss, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Import":
                                var Import = cnc.Query<Import>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Import, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Import_AuditTrail":
                                var Import_AuditTrail = cnc.Query<Import_AuditTrail>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Import_AuditTrail, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "LogImporting":
                                var LogImporting = cnc.Query<LogImporting>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(LogImporting, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "LogImportingError":
                                var LogImportingError = cnc.Query<LogImportingError>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(LogImportingError, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "LogPacking":
                                var LogPacking = cnc.Query<LogPacking>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(LogPacking, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "ODLC":
                                var ODLC = cnc.Query<ODLC>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(ODLC, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "OfficialSongs":
                                var OfficialSongs = cnc.Query<OfficialSongs>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(OfficialSongs, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Pack_AuditTrail":
                                var Pack_AuditTrail = cnc.Query<Pack_AuditTrail>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Pack_AuditTrail, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Standardization":
                                var Standardization = cnc.Query<Standardization>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Standardization, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Tones":
                                var Tones = cnc.Query<Tones>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Tones, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "Tones_GearList":
                                var Tones_GearList = cnc.Query<Tones_GearList>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(Tones_GearList, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                            case true when ftable == "WEM2OGGCorrespondence":
                                var WEM2OGGCorrespondence = cnc.Query<WEM2OGGCorrespondence>(fcmds); UpdateLog(dt, "Selecting (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                dsm = LiustToDataSet(WEM2OGGCorrespondence, fcmds); UpdateLog(dt, "dumping from T to dataset (" + (dt - DateTime.Now) + "): " + fcmds, false, c("dlcm_TempPath"), "", "", null, null); dt = DateTime.Now;
                                break;
                        }
                        return dsm;

                        //using (SQLite.SQLiteDataAdapter myAdapter = new SQLite.SQLiteDataAdapter(fcmds, cnc))
                        //{
                        //    myAdapter.Fill(dsm, ftable);
                        //    myAdapter.Dispose();
                        //}
                    }
                    else
                        using (OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cn))
                        {
                            //fcmds = fcmds.Replace(")$!", ")");
                            da.Fill(dsm, ftable);
                            da.Dispose();
                        }
                }
                catch (Exception ex)
                {
                    ShowConnectivityError(ex, ftable + "---" + fcmds);
                }/*, null*/
                //else
                //    MessageBox.Show("no data/connection");
                return dsm;
            }
            else return dfsm;
        }

        public static int GetNoRecords(string cmd, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            cmd = cmd.Replace(";", "");
            cmd = cmd.Replace("Maiu", "Main u");
            DataSet dms = new DataSet(); dms = SelectFromDB("Main", cmd, "", cnb, cnc);
            var noOfRec = GetNoRec(dms, cnb, cnc); //dms.Tables.Count == 0 ? 0 : (dms.Tables[0].Rows.Count > 0 ? dms.Tables[0].Rows.Count : 0);
            return noOfRec;
        }


        public static string GetSelectIDs(string cmd, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            //Speed up the re-run of any Query by only providing list of IDs
            //var cmd = "SELECT ID FROM (" + Search
            cmd = cmd.Replace(";", "");
            //.Replace(c("dlcm_SearchFields"), "ID").Replace("ORDER BY " + c("dlcm_OrderOfFields"), "") + ") order by ID DESC";
            //cmd = cmd.Replace(", )", ")").Replace("WHERE )", ")");
            cmd = cmd.Replace("Maiu", "Main u");
            DataSet dms = new DataSet(); dms = SelectFromDB("Main", cmd, "", cnb, cnc);
            var noOfRec = GetNoRec(dms, cnb, cnc); //dms.Tables.Count == 0 ? 0 : dms.Tables[0].Rows.Count;
            var IDS = "0";
            if (noOfRec > 0)
                for (var l = 0; l < noOfRec; l++)
                    IDS += ", " + dms.Tables[0].Rows[l].ItemArray[0].ToString();
            return IDS;
        }
        public static void fileCopy(string Source, string Dest, bool over)
        {
            try
            {
                File.Copy(Source, Dest, over);
            }
            catch (Exception ex)
            {
                var tsst = "Erro6 ..." + ex.Message + Source + Dest; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }
        public static string CopyMoveFileSafely(string Source, string Dest, bool Copy, string source_hash, bool over)
        {
            var dupli_already_exists = false;
            string FileHashO = ""; string FileHashI = "";
            if (File.Exists(Dest))
            {
                try
                {
                    if (source_hash != null && source_hash != "") FileHashI = source_hash;
                    else FileHashI = GetHash(Source);
                    FileHashO = GetHash(Dest);
                }
                catch (Exception ex)
                {
                    var tsst = "Error5 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    ErrorWindow frm1 = new ErrorWindow("error when calc file hash ", "", "Error at import", false, false, true, "", "", "", false);
                    frm1.ShowDialog();
                }
                if (FileHashI == FileHashO || !over)
                {
                    dupli_already_exists = true;
                    Dest = Dest.Replace(".psarc", "[Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "].psarc");
                }
            }

            if (!dupli_already_exists)
                try
                {
                    File.Copy(Source, Dest, true);
                    if (!Copy && File.Exists(Source)) DeleteFile(Source, false);
                }
                catch (Exception ex)
                {
                    var tsst = "Error6 ..." + ex.Message + Source + Dest; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }
            else
                try
                {
                    File.Copy(Source, Dest, true);
                    if (!Copy) FileSystem.DeleteFile(Source, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                }
                catch (Exception ex)
                {
                    var tsst = "Error6 ..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }

            return Dest;

        }

        static public string MoveTheAtEnd(string t)
        {
            if (t.Length <= 4) return t;
            var txt = (t.Substring(0, 4) == "The " ? t.Substring(4, t.Length - 4) + ",The" : t);
            txt = (txt.Substring(0, 4) == "Die " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);
            txt = (txt.Substring(0, 4) == "the " ? txt.Substring(4, t.Length - 4) + ",The" : txt);
            txt = (txt.Substring(0, 4) == "die " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);
            txt = (txt.Substring(0, 4) == "THE " ? txt.Substring(4, t.Length - 4) + ",The" : txt);
            txt = (txt.Substring(0, 4) == "DIE " ? txt.Substring(4, t.Length - 4) + ",Die" : txt);

            return txt;
        }
        static public int GetNoRec(DataSet ds, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            var noOfRecz = 0;
            try
            {
                noOfRecz = ds is null ? 0 : (ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count);
                if (noOfRecz > 0) { var z = ds.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32(); }// if it fails means the SQLite returned blank row :)
            }
            catch (Exception ex)
            {
                noOfRecz = 0;
            }

            return noOfRecz;
        }
        static public void CleanFolder(string pathfld, string exttoigno, bool copy, bool archive, string Archive_Path, string form, System.Windows.Forms.ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            var tst = "Assessing to clean Folders..." + pathfld; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            string[] args = new string[50]; for (var x = 0; x < 50; x++) args[x] = "";

            args = exttoigno.ToString().Split(';');
            if (pathfld != "" && pathfld != null && DirectoryExists(pathfld))
            {
                pathfld = pathfld + "\\";
                try
                {
                    System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(pathfld);
                    foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                    {
                        tst = "Assessing to clean Folders..." + file; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"),
                            "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (exttoigno == "")
                            CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), archive, null, false);
                        else
                        {
                            if (c("dlcm_DBFolder") == file.FullName
                                || (c("dlcm_DBFolder").Contains(".accdb") ? "SQLLiteDB.accdb" : "AccessDB.accdb") == file.Name
                                || "LinkDB.accdb" == file.Name) continue;
                            if (args.Count() <= 1) DeleteFile(file.FullName, false);
                            else if ((file.FullName.IndexOf(args[0]) > 0 || file.FullName.IndexOf(args[1]) > 0 || file.FullName.IndexOf(args[2]) > 0)
                                && archive & copy) //|| exttoigno == ""
                                CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), archive, null, false);
                            if (args.Count() <= 1) DeleteFile(file.FullName, false);
                            else if ((file.FullName.IndexOf(args[0]) > 0 || file.FullName.IndexOf(args[1]) > 0 || file.FullName.IndexOf(args[2]) > 0)
                                && archive & !copy) //|| exttoigno == ""
                                CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), copy, null, false);

                        }
                    }
                }
                catch (Exception ex) { var tsst = "Error7 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
        }

        public static void CleanNTGenNT(string hsanPath)
        {
            if (File.Exists(hsanPath + ".orig")) File.Copy(hsanPath + ".orig", hsanPath, true);
            var fxml = File.OpenText(hsanPath);
            if (!File.Exists(hsanPath + ".orig")) File.Copy(hsanPath, hsanPath + ".orig", true);

            var textfile = "";

            var linenew = "";
            //var cmd = "";
            var line = "";// fxml.ReadLine(); line = fxml.ReadLine();
                          //Read and Save Header
            var a = "";
            while ((line = fxml.ReadLine()) != null)
            {
                //if (line.Contains("/windows") || line.Contains("/generic") || line.Contains("/songs_dlc_songs") || line.Contains("\"songs_dlc_songs\""))
                //    ;
                linenew = line;
                linenew = linenew.Replace("/windows", "/ps3");
                linenew = linenew.Replace("/generic", "/ps3");
                linenew = linenew.Replace("/songs_dlc_songs", "/songs");
                linenew = linenew.Replace("\"songs_dlc_songs\"", "\"songs\"");
                if (line.Contains("relpath")) a = linenew;
                if (line.Contains("logpath")) linenew = a.Replace("relpath", "logpath");
                textfile += "\n" + linenew;
            }
            fxml.Close();
            File.WriteAllText(hsanPath, textfile);
        }


        //#region PS3 EDAT Encrypt/Decrypt
        private const string Flags = "0C",    //0x0c
                             Type = "00",
                             Version = "03";  //02 or 03
        private const string kLic = "CB4A06E85378CED307E63EFD1084C19D";
        private const string ContentID = "UP0001-BLES01862_00-RS001PACK0000003";
        public static string EncryptSongsPSARC()
        {
            if (!IsJavaInstalled())
                return "No JDK or JRE is installed on your machine";

            var errors = System.String.Empty;
            var files = Directory.EnumerateFiles(c("dlcm_TempPath") + "\\0_dlcpacks\\manipulated\\", "*.psarc");

            foreach (var InFile in files)
            {
                var OutFile = InFile;// + ".edat";
                var command = System.String.Format("EncryptEDAT \"{0}\" \"{1}\" {2} {3} {4} {5} {6}",
                    InFile, OutFile, kLic, ContentID, Flags, Type, Version);

                errors += EdatCrypto(command);
            }


            return System.String.IsNullOrEmpty(errors) ? Packer.EDAT_MSG : errors;
        }

        internal static string EdatCrypto(string command)
        {
            // Encrypt/Decrypt using TrueAncestor Edat Rebuilder v1.4c
            using (var PS3Process = new Process())
            {
                PS3Process.StartInfo.FileName = "java";
                PS3Process.StartInfo.Arguments = System.String.Format("-cp \"{0}\" -Xms256m -Xmx1024m {1}", Path.Combine(ExternalApps.TOOLKIT_ROOT, ExternalApps.APP_COREJAR), command);
                PS3Process.StartInfo.WorkingDirectory = ExternalApps.TOOLKIT_ROOT;
                PS3Process.StartInfo.UseShellExecute = false;
                PS3Process.StartInfo.CreateNoWindow = true;
                PS3Process.StartInfo.RedirectStandardError = true;
                PS3Process.Start();
                PS3Process.WaitForExit();

                var stdout = PS3Process.StandardError.ReadToEnd();
                //Improve me please
                if (!System.String.IsNullOrEmpty(stdout))
                    return System.String.Format("System error occurred {0}\n", stdout);

                return "";
            }
        }

        public static bool IsJavaInstalled()
        {
            try
            {
                using (var version = new Process())
                {
                    version.StartInfo.FileName = "java";
                    version.StartInfo.Arguments = "-version";
                    version.StartInfo.CreateNoWindow = true;
                    version.StartInfo.UseShellExecute = false;
                    // Java uses this output instead of stout.
                    version.StartInfo.RedirectStandardError = true;
                    version.Start();
                    version.WaitForExit();

                    // Get the output into a string
                    var output = version.StandardError.ReadLine();
                    if (!output.Contains("java version"))
                        return false;

                    // Parse java version and detect if it's good.
                    var javaVer = output.Split('\"')[1].Split('.');
                    int maj = int.Parse(javaVer[0]);
                    int min = int.Parse(javaVer[1]);

                    if (maj > 0 && min >= 0)
                        return true;

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static public void UpdateDBAltMet(string ftable, string fcmds, OleDbConnection cn)
        {
            try
            {
                var Cmd = new OleDbCommand(fcmds, cn);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var tsst = "Erroru ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                ShowConnectivityError(ex, ftable + "--------" + fcmds + "--------------");/*, null*/
            }

            return;
        }
        static public void DeleteFileIfExisting(string file)
        {
            if (!File.Exists(file)) return;
            try
            {
                DialogResult result1 = DialogResult.Yes;
                if ((file.IndexOf("rs1compatibilitydisc") >= 0 || file.IndexOf("rs1compatibilitydlc") >= 0 || file.IndexOf("cache") >= 0))
                    result1 = MessageBox.Show("Are you sure you want to Delete/Move Rocksmith corefiles: " + file + ".\n(No) will ignore Delete command.", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.No)
                {
                    return;
                }


                if (ConfigRepository.Instance()["dlcm_AdditionalManipul81"] == "Yes") FileSystem.DeleteFile(file, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                else File.Delete(file);
            }
            catch (Exception ex)
            {
                var tsst = "Erro @filedelete..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }
        static public void DeleteFile(string file, bool overr)
        {
            try
            {
                DialogResult result1 = DialogResult.Yes;
                if ((file.IndexOf("rs1compatibilitydisc") >= 0 || file.IndexOf("rs1compatibilitydlc") >= 0 || file.IndexOf("cache") >= 0) && !overr)
                    result1 = MessageBox.Show("Are you sure you want to Delete/Move Rocksmith corefiles: " + file + ".\n(No) will ignore Delete command.", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.No)
                {
                    return;
                }


                if (ConfigRepository.Instance()["dlcm_AdditionalManipul81"] == "Yes") FileSystem.DeleteFile(file, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                else File.Delete(file);
            }
            catch (Exception ex)
            {
                var tsst = "Erro @filedelete..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }


        static public void DeleteDirectory(string dir, bool overrite)
        {
            try
            {
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul81"] == "Yes" & !overrite) FileSystem.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                else Directory.Delete(dir, true);
            }
            catch (Exception ex)
            {
                var tsst = "Erro @dir delete..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }

        static public string WwiseInstalled(string Mss)
        {
            var wwisePath = "";
            if (!string.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                wwisePath = ConfigRepository.Instance()["general_wwisepath"];
            else
                wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
            if (wwisePath == "" || !DirectoryExists(wwisePath))
            {
                ErrorWindow frm1 = new ErrorWindow("Cause " + Mss + ".\nPlease Install Wwise Launcher then Wwise v" + wwisePath + "" +
                    " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesful," +
                    " else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/",
                    "Error at WEM Creation", false, false, true, "", "", "", false);
                frm1.ShowDialog();
                return "0" + ";" + frm1.IgnoreSong + ";" + frm1.StopImport;
            }
            else
                return "1" + ";0;0";
        }


        public static string GetArrOfficSQLTxt(bool arrangoff)
        {
            if (arrangoff) return " AND Official=\"Yes\"";
            else return " AND (Official<>\"Yes\" OR Official is NULL)";
        }

        public static void CreateFolder(string dest)/*, bool verbose = truestring */
        {
            if (!DirectoryExists(dest))
            {
                DirectoryInfo di;
                try
                {
                    di = Directory.CreateDirectory(dest);
                    //UpdateLog(DateTime.Now, "created folders: " + dest, false, c("dlcm_TempPath"), "", "", null, null);
                    //Directory.CreateDirectory();
                }
                catch (Exception ex)
                {
                    var tsst = "Error9 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }
            }
        }

        public static async Task<string> CheckIfConnectedToSpotify()
        {
            var netstatus = UtilitiesFunctions.CheckIfConnectedToInternet().Result.ToString();
            ActivateSpotify_ClickAsync(null, null);
            //if (netstatus == "OK" && _spotify != null)
            //    try
            //    {
            //        SearchItem Aitem = _spotify.SearchItems("Nevermind", SearchType.Album);
            //        if (!(Aitem.Error is null)) return "OK";
            //    }
            //    catch (Exception ex)
            //    {
            //        var tgst = "Error1 ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
            //    }

            return "NOK";
        }

        //public static string GetSelectedSongs(string Groupss, bool Population_PackNO, string NoOfSplits, OleDbConnection cnb, string cmds, int i, string format, string importdate, SQLiteConnection cnz)/*, bool verbose = truestring *//*, bool Population_Selected, bool Population_Groups*/
        public static string GetSelectedSongs(string Groupss, bool Population_PackNO, string NoOfSplits, OleDbConnection cnb, string cmds, int i, string format, string importdate, SQLite.SQLiteConnection cnc)/*, bool verbose = truestring *//*, bool Population_Selected, bool Population_Groups*/
        {
            var cmd = " WHERE cstr(ID) IN (" + GetFilter(Groupss, cmds, i, "", cnb, "", format, importdate, cnc) + ")";
            //if (Population_PackNO)                    cmd += " AND Split4Pack='" + NoOfSplits + "'";
            //cmd = "WHERE cstr(ID) IN (SELECT CDLC_ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + Groupss + "\")";
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul97"] == "Yes") cmd = cmd.Replace(" ORDER BY Artist,Album_Year,Album,Track_No,ID", "") + " AND ID NOT IN (SELECT ID FROM Pack_AuditTrail)" + " ORDER BY Artist,Album_Year,Album,Track_No,ID";
            if (ConfigRepository.Instance()["dlcm_AdditionalManipul99"] == "Yes") cmd += " AND ID NOT IN (SELECT ID FROM Groups WHERE Type=\"DLC\" AND Groupz=\"" + Groupss + "\")";
            cmd = cmd.Replace(c("dlcm_SearchFields"), "ID");
            if (Population_PackNO)
            {
                ConfigRepository.Instance()["dlcm_AdditionalManipul80"] = "Yes";
                return cmd + " AND Split4Pack=\"" + NoOfSplits + "\"";
            }
            else ConfigRepository.Instance()["dlcm_AdditionalManipul80"] = "Yes";
            return cmd + " ORDER BY Artist,Album_Year,Album,Track_No,ID";
        }

        public static string ReturnPlatformFolder(string format)/*, bool verbose = truestring */
        {
            return (format == "PS3" ? "ps3" : ((format == "PS4" ? "generic" : ((format == "Pc" ? "windows" : ((format == "Mac" ? "mac" : ("")))))))); /*format == "PS4" ? "generic" : (*/
        }

        public static string ReturnPlatformBINFolder(string format)/*, bool verbose = truestring */
        {
            return (format == "ps3" ? "ps3" : ((format == "ps4" ? "generic" : ((format == "windows" ? "generic" : ((format == "mac" ? "macos" : (""))))))));//format == "PS4" ? "generic" : ("")
        }

        public static void FileCopy(string fileFrom, string fileTo, bool overWrite, int count, bool ignorelog)/*, bool verbose = truestring */
        {
            //if (verbose)
            //    if (!PromptOverwrite(fileTo))
            //        return false;
            //    else
            //        overWrite = true;

            //var fileToDir = Path.GetDirectoryName(fileTo);
            //if (!Directory.Exists(fileToDir)) MakeDirectory(fileToDir);
            //System.IO.Stream fileFrom;
            //fileFrom
            //public static void Copy(System.IO.Stream inStream, string outputFilePath)
            //{
            //using (System.IO.Stream fileFrom = new Stream(fileTo))
            //{
            //    int bufferSize = 1024 * 1024;

            //    using (FileStream fileStream = new FileStream(fileTo, FileMode.OpenOrCreate, FileAccess.Write))
            //    {
            //        fileStream.SetLength(fileFrom.Length);
            //        int bytesRead = -1;
            //        byte[] bytes = new byte[bufferSize];

            //        while ((bytesRead = fileFrom.Read(bytes, 0, bufferSize)) > 0)
            //        {
            //            fileStream.Write(bytes, 0, bytesRead);
            //        }
            //    }
            //}
            //}
            try
            {
                var a = "echo f | xcopy";
                var b = " /r /h /y /t /q /c \"" + fileFrom + "\" \"" + fileTo + "\"";
                var c = a + b;
                var t = ConfigRepository.Instance()["dlcm_AdditionalManipul80"];
                //var z = "copy" + " \"" + fileFrom + "\" \"" + fileTo + "\"";
                var z = "copy" + " '" + fileFrom + "' '" + fileTo + "'";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "No")
                    System.Diagnostics.Process.Start(a, b);
                else if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "1")
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new
                    System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C copy /Y/Z \"" + fileFrom + "\" \"" + fileTo + "\"";
                    process.StartInfo = startInfo;
                    process.Start();
                    //System.Diagnostics.Process.Start(z);// "copy", " \"" + fileFrom + "\" \"" + fileTo+"\"");/*/Y /Z */
                }
                else if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "Yes") File.Copy(fileFrom, fileTo, overWrite);
                else if (ConfigRepository.Instance()["dlcm_AdditionalManipul80"] == "Maybe")
                {

                    string batFileName = @"C:\t\copy.bat";
                    if (!File.Exists(batFileName))
                    {
                        // Create a file to write to.
                        string createText = "@ECHO ON" + Environment.NewLine;
                        File.WriteAllText(batFileName, createText);
                    }
                    var tt = "echo " + count.ToString() + Environment.NewLine;
                    tt += "copy \"" + fileFrom + "\" \"" + fileTo + "\"" + Environment.NewLine;
                    System.IO.File.AppendAllText(batFileName, @tt);
                }
                //var startInfo = new ProcessStartInfo
                //{
                //    FileName = Path.Combine(AppWD, "cmd"),
                //    WorkingDirectory = Path.GetDirectoryName(fileFrom)
                //};
                ////var tr = AppWD + "\\" + c("dlcm_localjava");
                ////startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = string.Format("xcopy \""+ fileFrom + "\" \"" + fileTo + "\" /r/h/y");
                //startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;
                //Process DDC = new Process();
                //    DDC.StartInfo = startInfo;
                //    DDC.Start(); //DDC.WaitForExit(1000 * 60 * 1); //wait 1min
            }
            catch (Exception e)/*IO*/
            {
                if (ignorelog) UpdateLog(DateTime.Now, "Erro at file copy..." + e.Message.ToString().Replace("Error", "Erro"), false, c("dlcm_TempPath"), "", "", null, null);
                else UpdateLog(DateTime.Now, "Erro at file copy..." + e, false, c("dlcm_TempPath"), "", "", null, null);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new
                System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C copy /Y/Z \"" + fileFrom + "\" \"" + fileTo + "\"";
                process.StartInfo = startInfo;
                process.Start();
                //if (!overWrite || !verbose) return true; // be nice don't throw error
                //BetterDialog2.ShowDialog(
                //    "Could not copy file " + fileFrom + "\r\nError Code: " + e.Message +
                //    "\r\nMake sure associated file/folders are closed.",
                //    MESSAGEBOX_CAPTION, null, null, "OK", Bitmap.FromHicon(SystemIcons.Warning.Handle), "Warning ...", 150, 150);
                //return false;
            }
            //try
            //{
            //    File.SetAttributes(fileFrom, FileAttributes.Normal);
            //    File.Copy(fileFrom, fileTo, overWrite);
            //    //return true;
            //}
            //catch (Exception e)/*IO*/
            //{
            //    var tsst = "Error at file copy..." + e; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            //    //if (!overWrite || !verbose) return true; // be nice don't throw error
            //    //BetterDialog2.ShowDialog(
            //    //    "Could not copy file " + fileFrom + "\r\nError Code: " + e.Message +
            //    //    "\r\nMake sure associated file/folders are closed.",
            //    //    MESSAGEBOX_CAPTION, null, null, "OK", Bitmap.FromHicon(SystemIcons.Warning.Handle), "Warning ...", 150, 150);
            //    //return false;
            //}
        }

        public static bool DirectoryExists(string dir)
        {
            //return true;
            if (Directory.Exists(dir)) return true;
            //Tried some other ways when paths not working in Parallels
            //else
            //{                try  {
            //    //in case folder is on a network drive and DirectoryExists has issues returniung the real value
            //    System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(dir.Substring(0, dir.LastIndexOf("\\")));
            //  foreach (DirectoryInfo di in downloadedMessageInfo.GetDirectories())
            //    {
            //        if (di.Name == dir.Substring(dir.LastIndexOf("\\") + 1, dir.Length - dir.LastIndexOf("\\")-1)) return true;
            //    }
            //        }
            //    catch (Exception Ex)
            //    {
            //        try
            //        {
            //            File.Copy(AppWD+ "\\mdbplus.ini",dir + "\\mdbplus.ini", true);
            //            File.Delete(dir + "\\mdbplus.ini");
            //        }
            //        catch (Exception Exx)
            //        {
            //            return false;
            //        }
            //    }
            //}
            return false;
        }

        public static DialogResult CreateTempFolderStructure(string TempPathImport, string oldPathImport, string brokenPathImport, string dupliPathImport,
            string dlcpacks, string pathDLC, string repackedpath, string repackedXBOXPath, string repackedPCPath, string repackedMACPath, string repackedPSPath,
            string logPath, string albumCoversPSPath, string LogPSPath, string ArchivePath, string dataPath, string TempPath, string dflt_Path_Import
            , string intheworksPath, string exportPath)
        {
            DialogResult result1 = DialogResult.Yes;
            if (!DirectoryExists(TempPathImport) || !DirectoryExists(pathDLC) || !DirectoryExists(oldPathImport) || !DirectoryExists(brokenPathImport) ||
                !DirectoryExists(dupliPathImport) || !DirectoryExists(dlcpacks + "\\temp") || !DirectoryExists(dlcpacks + "\\manipulated") ||
                !DirectoryExists(dlcpacks + "\\manifests") || !DirectoryExists(dlcpacks + "\\origs")/*|| !DirectoryExists(dlcpacks + "\\manipulated\\temp")*/
                || !DirectoryExists(repackedpath) ||
                !DirectoryExists(repackedXBOXPath) || !DirectoryExists(repackedPCPath) || !DirectoryExists(repackedMACPath) ||
                !DirectoryExists(repackedPSPath) || (!DirectoryExists(logPath) && logPath != "") || !DirectoryExists(LogPSPath)
                || !DirectoryExists(albumCoversPSPath) || !DirectoryExists(ArchivePath) || !DirectoryExists(dataPath) || !DirectoryExists(TempPath)
                || !DirectoryExists(intheworksPath) || !DirectoryExists(exportPath))
            {
                var fldrm = "";
                if (!DirectoryExists(TempPathImport) && (TempPathImport != null)) fldrm += ";" + TempPathImport;
                if (!DirectoryExists(LogPSPath) && (LogPSPath != null)) fldrm += ";" + LogPSPath;
                if (!DirectoryExists(pathDLC) && (pathDLC != null)) fldrm += ";" + pathDLC;
                if (!DirectoryExists(oldPathImport) && (oldPathImport != null)) fldrm += ";" + oldPathImport;
                if (!DirectoryExists(brokenPathImport) && (brokenPathImport != null)) fldrm += ";" + brokenPathImport;
                if (!DirectoryExists(dupliPathImport) && (dupliPathImport != null)) fldrm += ";" + dupliPathImport;
                if (!DirectoryExists(dlcpacks) && (dlcpacks != null)) fldrm += ";" + dlcpacks;
                if (!DirectoryExists(dlcpacks + "\\manifests") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manifests";
                if (!DirectoryExists(dlcpacks + "\\manipulated") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manipulated";
                //if (!DirectoryExists(dlcpacks + "\\manipulated\\temp") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manipulated\\temp";
                if (!DirectoryExists(dlcpacks + "\\temp") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\temp";
                if (!DirectoryExists(dlcpacks + "\\origs") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\origs";
                if (!DirectoryExists(repackedpath) && (repackedpath != null)) fldrm += ";" + repackedpath;
                if (!DirectoryExists(repackedXBOXPath) && (repackedXBOXPath != null)) fldrm += ";" + repackedXBOXPath;
                if (!DirectoryExists(repackedPCPath) && (repackedPCPath != null)) fldrm += ";" + repackedPCPath;
                if (!DirectoryExists(repackedMACPath) && (repackedMACPath != null)) fldrm += ";" + repackedMACPath;
                if (!DirectoryExists(repackedPSPath) && (repackedPSPath != null)) fldrm += ";" + repackedPSPath;
                if (!DirectoryExists(logPath) && logPath != null && (logPath != "")) fldrm += ";" + logPath;
                if (!DirectoryExists(albumCoversPSPath) && (albumCoversPSPath != null)) fldrm += ";" + albumCoversPSPath;
                if (!DirectoryExists(ArchivePath) && (ArchivePath != null)) fldrm += ";" + ArchivePath;
                if (!DirectoryExists(dataPath) && (dataPath != null)) fldrm += ";" + dataPath;
                if (!DirectoryExists(TempPath) && (TempPath != null)) fldrm += ";" + TempPath;
                if (!DirectoryExists(dflt_Path_Import) && (dflt_Path_Import != null) && pathDLC != dflt_Path_Import) fldrm += ";" + dflt_Path_Import;
                if (!DirectoryExists(intheworksPath) && (intheworksPath != null)) fldrm += ";" + intheworksPath;
                if (!DirectoryExists(exportPath) && (exportPath != null)) fldrm += ";" + exportPath;

                DirectoryInfo di;
                result1 = MessageBox.Show("Following " + fldrm.Replace(";", ";\n") + "\n folder(s) is/are missing please" + " Chose:\n\n1. (Yes) Create Folders\n2. (No) Ignore and folder create structure after, manually through DLCManager->MainDB->tab\n3. (Cancel) Stop/Close Program"
                    , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {

                    DialogResult result2 = DialogResult.Yes;
                    if (fldrm.Contains("0_duplicate") || fldrm.Contains("0_old") || fldrm.Contains("0_albumCovers") || fldrm.Contains("0_data") ||
               fldrm.Contains("0_archive") || fldrm.Contains("0_broken") || fldrm.Contains("0_intheworks") || fldrm.Contains("0_export") || fldrm.Contains("0_to_import")
               || fldrm.Contains("0_temp") || fldrm.Contains("0_log"))
                        result2 = MessageBox.Show("1. (Yes) Create Folders\n\n2. (No) Create MKLink/symbolic-or-hard-link & move to Remote"
                            + "\n\ni.e.,\n0_data (dlcm_0_data): " + c("dlcm_0_data") + "\n" + "0_archive (dlcm_0_archive): " + c("dlcm_0_archive") + "\n"
                            + "0_broken (dlcm_0_broken): " + c("dlcm_0_broken") + "0_to_import (dlcm_0_to_import): " + c("dlcm_0_to_import")
                         + "\n" + "0_duplicate (dlcm_0_duplicate): " + c("dlcm_0_duplicate") + "\n" + "0_old (dlcm_0_old):" + c("dlcm_0_old")
                         + "\n" + "0_intheworks (dlcm_0_intheworks): " + c("dlcm_0_intheworks") + "\n" + "0_export (dlcm_0_export):" + c("dlcm_0_export")
                         + "\n" + "0_albumCovers (dlcm_0_albumCovers):" + c("dlcm_0_albumCovers")
                         , MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    string[] args = (fldrm).ToString().Split(';');
                    bool once = false;
                    foreach (string s in args)
                    {
                        if (s == "") continue;
                        try
                        {
                            if (result2 == DialogResult.No && !once &&
                            (s.Contains("0_duplicate") || s.Contains("0_old") || s.Contains("0_data") || s.Contains("0_to_import") || s.Contains("0_log") ||
                            s.Contains("0_archive") || s.Contains("0_broken") || s.Contains("0_repacked") || s.Contains("0_dlcpacks") || s.Contains("0_temp")
                             || s.Contains("0_intheworks") || s.Contains("0_export") || s.Contains("0_albumCovers")))
                            { CreateMKLinks(); once = true; }
                            else
                                di = Directory.CreateDirectory(s);
                            UpdateLog(DateTime.Now, "created folders: " + fldrm, false, c("dlcm_TempPath"), "", "", null, null);
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error9 ..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open create folders " + s);
                        }
                    }
                }

                else if (result1 == DialogResult.No) return result1;
                else System.Windows.Forms.Application.Exit();
            }
            return result1;
        }

        public static string GetTimestamps(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssfff");
        }

        public static string ReadPackageComment(string filePath)
        {
            if (filePath == "") return "";
            var info = File.OpenText(filePath);
            string comment = "";
            string line; bool found = false;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                if (line.Contains("Package Comment:"))
                {
                    if (line.Length > 15)
                    {
                        comment = line.Replace("Package Author:", "");
                        found = true;
                    }
                }
                else if (found) comment += "\n" + line;
            }
            info.Close();
            return comment;
        }

        public static string ReadPackageAuthor(string filePath)
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

        public static string Add2LinesInVocals(string filePath, int nooflines, string pos, int note1)
        {
            var info = File.OpenText(filePath);
            string firsttime = "";
            string fistline = "";
            bool once = true;
            string line;
            string firstlyric = "";

            using (StreamWriter sw = File.CreateText(filePath + ".newvcl"))
            {
                while ((line = info.ReadLine()) != null)
                {
                    if (line.Contains("<vocal time") && once)
                    {
                        for (var j = 0; j < nooflines; j++)
                            sw.WriteLine(" <vocal time=\"" + pos + "\" note=\"" + note1 + "\" length=\"0.9\" lyric=\"\"/>");
                        once = false;

                        firsttime = line.Substring(line.IndexOf(value: "\"") + 1, 8);
                        firsttime = firsttime.Substring(0, firsttime.IndexOf("\""));

                        var lyric = line.Substring(line.IndexOf("lyric") + 5);
                        lyric = lyric.Substring(lyric.IndexOf("\"") + 1);
                        firstlyric = lyric.Substring(0, lyric.IndexOf("\""));
                        sw.WriteLine(line);
                    }
                    else sw.WriteLine(line);
                }

            }
            info.Close();
            File.Copy(filePath + ".newvcl", filePath, true);
            DeleteFile(filePath + ".newvcl", false);
            return fistline;
        }

        public static string ReadPackageToolkitVersion(string filePath)
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

        public static string ReadPackageOLDToolkitVersion(string filePath)
        {
            var info = File.OpenText(filePath);
            string Toolkit_version = "";
            string line;
            //3 lines
            while ((line = info.ReadLine()) != null)
            {
                Toolkit_version = line.Split(':')[0].Trim();
            }
            info.Close();
            return Toolkit_version;
        }

        public static string GetShortNamet(string Format, string Artist, string Title, string Version, bool Acronym)
        {
            //if (!Acronym)
            //    return String.Format(Format, Artist.(GetValidName(true, true)), Title.GetValidName(true, true), Version).Replace(" ", "-");
            //return String.Format(Format, Artist.(Acronym()), Title.GetValidName(true, true), Version).Replace(" ", "-");
            return (Artist + Title + Version).Replace(" ", "");
        }


        public static string GetValidNameg(string value, bool allowSpace = false, bool allowStartsWithNumber = false, bool underscoreSpace = false, bool frets24 = false)
        {
            // valid characters developed from actually reviewing ODLC artist, title, album names
            string name = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                // ODLC artist, title, album character use allows these but not these
                // allow use of accents Über ñice \\p{L}
                // allow use of unicode punctuation \\p{P\\{S} not currently implimented
                // may need to be escaped \t\n\f\r#$()*+.?[\^{|  ... '-' needs to be escaped if not at the beginning or end of regex sequence
                // allow use of only these special characters \\-_ /&.:',!?()\"#
                // allow use of alphanumerics a-zA-Z0-9
                // tested and working ... Üuber!@#$%^&*()_+=-09{}][":';<>.,?/ñice

                Regex rgx = new Regex((allowSpace) ? "[^a-zA-Z0-9\\-_ /&.:',!?()\"#\\p{L}]" : "[^a-zA-Z0-9\\-_/&.:',!?()\"#\\p{L} ]");
                name = rgx.Replace(value, "");

                Regex rgx2 = new Regex(@"^[\d]*\s*");
                if (!allowStartsWithNumber)
                    name = rgx2.Replace(name, "");

                // prevent names from starting with special characters -_* etc
                Regex rgx3 = new Regex("^[^A-Za-z0-9]*");
                name = rgx3.Replace(name, "");

                if (frets24)
                {
                    if (name.Contains("24"))
                    {
                        name = name.Replace("_24_", "_");
                        name = name.Replace("_24", "");
                        name = name.Replace("24_", "");
                        name = name.Replace(" 24 ", " ");
                        name = name.Replace("24 ", " ");
                        name = name.Replace(" 24", " ");
                        name = name.Replace("24", "");
                    }
                    name = name.Trim() + " 24";
                }

                if (underscoreSpace)
                    name = name.Replace(" ", "_");
            }

            return name.Trim();
        }

        public static string StripPlatformEndName(string value)
        {
            if (value.EndsWith(GamePlatform.Pc.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.Mac.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.XBox360.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.PS3.GetPathName()[2]) ||
                value.EndsWith(GamePlatform.PS3.GetPathName()[2] + ".psarc"))
            {
                return value.Substring(0, value.LastIndexOf("_"));
            }

            return value;
        }

        public static void UpdatePackingLog(string DB, string DBc_Path, int packid, string dlcID, string ex, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            var insertcmdd = "Pack, CDLC_ID, Dates, Comments";
            var insertvalues = "\"" + packid + "\"," + dlcID + ",\"" + System.DateTime.Now + "\",\"" + ex.Replace("'", "") + "\"";
            InsertIntoDBwValues(DB, insertcmdd, insertvalues, cnb, 0, cnc);
            DataSet dxr = new DataSet();
            if (DB == "LogPackingError") dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"Yes\", FilesMissingIssues=FilesMissingIssues+(\"" + ex + "\") WHERE ID=" + dlcID + ";", cnb, cnc);/*FilesMissingIssues*/
            else
            {
                dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"No\" WHERE ID=" + dlcID + ";", cnb, cnc);
                DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type = \"DLC\" AND Groupz = \"Packing\" and CDLC_ID=\"" + dlcID + "\"", cnb, cnc);
            }
        }

        public static string calc_path(string jsonsFiles)
        {
            var ttt = Path.GetDirectoryName(jsonsFiles);
            var pattth = ttt.IndexOf("\\manifests\\");
            var ddd = ttt.Substring(pattth + 1, ttt.Length - pattth - 1);
            return ddd;
        }
        public static string calc_path_sng(string jsonsFiles)
        {
            var ttt = Path.GetDirectoryName(jsonsFiles);
            var pattth = ttt.IndexOf("\\songs\\");
            var ddd = ttt.Substring(pattth + 1, ttt.Length - pattth - 1);
            return ddd;
        }

        public static string CleanTitle(string st)
        {
            var rt = st.IndexOf("["); var rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            rt = st.IndexOf("["); rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            return st;
        }

        //Further Clean Metadata for purposes of comparison (e.g. Are you going to go my way (bogdan version) eg. Mr Bungle
        public static string CleanTitleFurther(string st)
        {
            st = CleanTitle(st);
            var rt = st.IndexOf("("); var rdt = st.IndexOf(")"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            rt = st.IndexOf("("); rdt = st.IndexOf(")"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            if (st.IndexOf(" - ") > 0 && !st.ToLower().Contains("bass") && !st.ToLower().Contains("guitar")) st = st.Substring(0, st.IndexOf(" - ") - 1);
            return st.Replace("'", "").Replace(".", "").Trim();/*&& !st.ToLower().Contains("Half-Life 2") && !st.ToLower().Contains("GoldenEye 64")*/
        }
        public static string CleanTitleOfWeirdChars(string st)
        {
            //  ), - , - ),  Audio )
            //var rt = "";
            st = st.Trim();
            st = st.Replace(" Audio)", "Audio)");
            st = st.Replace(" audio)", "Audio)");
            st = st.Replace(" Track)", ")");
            st = st.Replace(" )", ")");
            st = st.Replace(" ()", "");
            st = st.Replace("( ", "(");
            //st = st.Replace(" (", "(");
            st = st.Replace(" -)", ")");
            st = st.Replace("( - ", "(");
            st = st.Replace("( -", "(");
            st = st.Replace("(-)", "");
            st = st.Replace("()", "");
            st = st.Replace("(Standard Tuning)", "[Standard Tuning]");
            st = st.Replace("(Full Album)", "[Full Album]");
            st = st.Replace("(No Bass)", "[No Bass]");
            st = st.Replace("(No Lead)", "[No Lead]");
            st = st.Replace("(No Rhythm)", "[No Rhythm]");
            st = st.Replace("(No Guitars)", "[No Guitars]");
            st = st.Replace("(Full Band)", "[Full Band]");
            st = st.Replace("(Audio)", "[Audio]");
            st = st.Replace(" - Full Album", "[Full Album]");
            st = st.Replace("(440 Hz)", "[440Hz]");
            st = st.Replace("(440Hz)", "[440Hz]");
            st = st.Replace("(Only Back Track)", "[Only Back Track]");
            st = st.Replace("(6 String)", "[6 String]");
            st = st.Replace("(5 String)", "[5 String]");
            st = st.Replace("(7 String)", "[7 String]");
            st = st.Replace("(6String)", "[6 String]");
            st = st.Replace("(5String)", "[5 String]");
            st = st.Replace("(7String)", "[7 String]");
            st = st.Replace("(Karaoke)", "[Karaoke]");
            st = st.Replace("(Bass Melody)", "[Bass Melody]");
            st = st.Replace("(Rhythm)", "[Rhythm]");
            st = st.Replace("(No Bass)", "[No Bass]");
            st = st.Replace("(No Guitar)", "[No Guitar]");
            st = st.Replace(" - Version", "");
            st = st.Replace("(Version)", "");
            st = st.Replace(" a.1", "[a.1]");
            st = st.Replace(" a.2", "[a.2]");
            st = st.Replace(" a.3", "[a.3]");
            st = st.Replace(")]", "]");
            st = st.Replace("]]", "]");
            if (st.Length > 3) if (st.Substring(st.Length - 2, 2) == " -")
                    st = st.Substring(0, st.Length - 2);
            if (st.Length > 4) if (st.Substring(st.Length - 3, 3) == " - ")
                    st = st.Substring(0, st.Length - 3);
            if (st.Length > 2) if (st.Substring(st.Length - 1, 1) == "(")
                    st = st.Substring(0, st.Length - 1);
            if (st.IndexOf("]") >= 0 && st.IndexOf("[") < 0)
                st = st.Replace("]", "");
            if (st.IndexOf("]") < 0 && st.IndexOf("[") >= 0)
                st = st.Replace("[", "");
            //if (st.Substring(st.Length - 7, 6) == " Audio)") st = st.Substring(0, st.Length - 7);
            //if (st.Substring(st.Length - 3, 2) == " )") st = st.Substring(0, st.Length -3);
            //rt = st.IndexOf("["); var rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            //rt = st.IndexOf("["); rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();

            st = st.Replace(" -)", ")");
            st = st.Replace(" )", ")");
            st = st.Replace("( ", "(");
            //st = st.Replace(" (", "(");
            st = st.Trim();
            return st;
        }

        public static string[] GetFTPFilesPlusDLC(string filen, string Temp_Path_Import, string gameversion)
        {
            var jsonFile1 = GetFTPFiles(filen);
            string[] tmp = { "", "" };
            if (!(jsonFile1 == null)) foreach (string fileName in jsonFile1)
                {                //Copy to decompress/import/FTP
                    if (fileName == null) break;
                    var tt = Temp_Path_Import + "\\" + fileName + gameversion;
                    if (fileName.IndexOf("songs.psarc.edat") >= 0 || fileName.IndexOf("rs1compatibilitydlc.psarc.edat") >= 0
                        || fileName.IndexOf("rs1compatibilitydisc.psarc.edat") >= 0 || fileName.IndexOf("cache.psarc.edat") >= 0)
                        tt = CopyFTPFile(Path.GetFileName(fileName), tt, c("dlcm_TempPath"));
                }
            var jsonFile2 = (GetFTPFiles(filen + "\\DLC"));//.ToArray();
            if (!(jsonFile2 == null)) foreach (string fileName in jsonFile2)
                {//Copy to decompress/import/FTP
                    if (fileName == null) break;
                    var tt = c("dlcm_TempPath") + "\\" + fileName + gameversion;
                    if (fileName.IndexOf("songs.psarc.edat") >= 0 || fileName.IndexOf("rs1compatibilitydlc.psarc.edat") >= 0
                        || fileName.IndexOf("rs1compatibilitydisc.psarc.edat") >= 0 || fileName.IndexOf("cache.psarc.edat") >= 0)
                        tt = CopyFTPFile(Path.GetFileName(fileName), tt, c("dlcm_TempPath"));
                }
            var z = jsonFile2 == null ? tmp : jsonFile1.Concat(jsonFile2).ToArray();
            if (z == null || z.Count() == 0) return tmp;
            else return z;
        }
        public static string[] GetFilesPlusDLC(string filen)
        {
            string[] tmp = { "", "" };
            if (!DirectoryExists(filen)) return tmp;
            var jsonFile4 = Directory.GetFiles(filen, "*.psarc*", System.IO.SearchOption.AllDirectories);
            return jsonFile4 == null ? tmp : jsonFile4;
            //.Concat(Directory.GetFiles(filen + "\\DLC", "*.psarc*", System.IO.SearchOption.AllDirectories)).ToArray();

        }
        public static string[] GetFTPFiles(string filen)
        {
            if (c("dlcm_FTPstatus") == "NOK") return null;
            try
            {
                System.Net.FtpWebRequest ftpRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(filen);
                ftpRequest.Credentials = new System.Net.NetworkCredential("anonymous", "bogdan@capi.ro");
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftpRequest.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8, true);

                string[] directories = new string[10000];
                var i = 0;
                string line = "";// streamReader.ReadLine();
                do
                {
                    line = streamReader.ReadLine();
                    if (line != null)
                    {
                        if (Path.GetFileName(line).Length < 7) continue;
                    }
                    else break;
                    directories[i] = (Path.GetFileName(line)).Substring(3, Path.GetFileName(line).Length - 3) + ";" + (line.Substring(30, line.Length - 30)).Replace((Path.GetFileName(line)).Substring(3, Path.GetFileName(line).Length - 3), "");
                    //directories[i] = directories[i].Replace(directories[i], "");
                    if (line.IndexOf("psarc") > 0) i++;
                } while (!string.IsNullOrEmpty(line));

                streamReader.Close();
                return directories;
            }
            catch (Exception ex)
            {

                ConfigRepository.Instance()["dlcm_FTPstatus"] = "NOK";
                var tsst = "Erro17 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); return null;
            }
        }

        public static string c(string configstring)
        {
            return ConfigRepository.Instance()[configstring];
        }

        //public static string DeleteCOPYedSongs(string filen, string FTPPath, OleDbConnection cnb, string ID, string platform, SQLiteConnection cnz)
        public static string DeleteCOPYedSongs(string filen, string FTPPath, OleDbConnection cnb, string ID, string platform, SQLite.SQLiteConnection cnc)
        {
            File.Move(filen, FTPPath);//Delete latest copied file

            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT CopyPath FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " and Platform=\"" + platform.ToUpper() + "\" and PackPath not like \"%%\\\"ORDER BY ID DESC;", "", cnb, cnc);
            var rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables[0].Rows.Count;
            var txt = "";
            if (rec > 0)
            {
                for (var i = 0; i < rec; i++)
                {
                    var fn = dvr.Tables[0].Rows[i].ItemArray[1].ToString();
                    File.Move(fn, fn.Replace(fn + ".psarc", ".old"));
                    txt += " " + fn.Replace(fn + ".psarc", ".old");
                }
            }
            return txt;
        }

        //public static bool CheckForRecord(string table, string sql, OleDbConnection cnb, SQLiteConnection cnz)
        public static bool CheckForRecord(string table, string sql, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            DataSet dvr = new DataSet(); dvr = SelectFromDB(table, sql, "", cnb, cnc);
            var rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables.Count == 0 ? 0 : dvr.Tables[0].Rows.Count;
            //var txt = "";
            if (rec > 0)
                return true;
            else
                return false;
        }

        public static string DeleteFTPedSongs(string filen, string FTPPath, OleDbConnection cnb, string ID, string ftpstatus, SQLite.SQLiteConnection cnc)
        {
            if (ftpstatus.ToLower() != "ok") return "not";
            //return "";

            if (filen != "") DeleteFTPFiles(filen, FTPPath);//Delete latest remote file

            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT FileName FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " and Platform=\"PS3\" ORDER BY ID DESC;", "", cnb, cnc);
            var rec = GetNoRec(dvr, cnb, cnc);//dvr.Tables.Count == 0 ? 0 : dvr.Tables[0].Rows.Count;
            var txt = "";
            if (rec > 0)
                for (var i = 0; i < rec; i++)
                {
                    if (txt == "nok") return "not";
                    txt += " " + DeleteFTPFiles(dvr.Tables[0].Rows[i].ItemArray[0].ToString(), FTPPath);
                }
            return txt;
        }

        public static string FTPAvail(string FTPPath)
        {
            if (FTPPath == null || FTPPath == "") return "NOK";
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential("anonymous", "bogdan@capi.ro");
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var tgst = "FTP not on ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
                return "NOK";
            }
            return "OK";
        }

        public static string DeleteFTPFiles(string filen, string FTPPath)
        {
            try
            {
                System.Net.FtpWebRequest ftpRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FTPPath + filen);
                ftpRequest.Credentials = new System.Net.NetworkCredential("anonymous", "bogdan@capi.ro");
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftpRequest.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream());
                return response.StatusDescription;
            }
            catch (Exception ex)
            {
                var f = ex.Message.Replace("error", "xrror"); var tsst = "Warning ..." + f;
                UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                if (ex.Message.ToLower().IndexOf("timed out") > 0) return "nok";
                return "";
            }
        }

        public static string CopyFTPFile(string filen, string localf, string FTPPath)
        {
            try
            {
                System.Net.FtpWebRequest ftpRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FTPPath + filen);
                ftpRequest.Credentials = new System.Net.NetworkCredential("anonymous", "bogdan@capi.ro");
                System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftpRequest.GetResponse();
                System.IO.BinaryReader bbinaryReader = new System.IO.BinaryReader(response.GetResponseStream());
                string[] directories = new string[10000];

                FileStream writeStream = new FileStream(localf, FileMode.Create);
                int Length = 2048;
                byte[] buffer = new byte[Length];
                int bytesRead = bbinaryReader.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = bbinaryReader.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
                return "ok";
            }
            catch (Exception ex)
            {
                var tsst = "Error 18..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                return "nok";
            }
        }
        public static string FTPFile(string filel, string filen, string TempPat, string SearchCm, string ID, OleDbConnection cnb, string ftpstatus, SQLite.SQLiteConnection cnc)
        {
            if (ftpstatus.ToLower() != "ok") return "not";

            // Get the object used to communicate with the server.
            var ddd = filel + filen.Replace(TempPat + "\\0_repacked\\PS3\\", "");
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ddd);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("anonymous", "bogdan@capi.ro");

                byte[] b = File.ReadAllBytes(filen);

                request.ContentLength = b.Length;
                try
                {
                    using (Stream s = request.GetRequestStream())
                    {
                        s.Write(b, 0, b.Length);
                    }
                    FtpWebResponse ftpResp = (FtpWebResponse)request.GetResponse();
                    DataSet dxr = new DataSet(); var fn = filen.Substring(filen.IndexOf("PS3\\") + 4, filen.Length - filen.IndexOf("PS3\\") - 4);
                    dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\" WHERE ID=" + ID + ";", cnb, cnc);

                    return "Truely ";
                }
                catch (Exception ex)
                {
                    var tsst = "Error 19..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    return "Not ";
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error20 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                return "Not ";
            }
        }

        static public bool CheckSong(string song)
        {
            var Temp_Path = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_temp";
            var unpackedDir = "";
            DLCPackageData info = null;
            var platform = song.GetPlatform();
            var timestamp = UpdateLog(DateTime.Now, "Unpack song", true, Temp_Path, "", "", null, null);

            try
            {
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul51"] == "Yes")
                    unpackedDir = Packer.Unpack(song, Temp_Path, platform, true, true);
                else
                    unpackedDir = Packer.Unpack(song, Temp_Path, platform, true, false);
                timestamp = UpdateLog(timestamp, "Load song", true, Temp_Path, "", "DLCManager", null, null);
                info = DLCPackageData.LoadFromFolder(unpackedDir, platform); //Generating preview with different name
                DeleteDirectory(unpackedDir, false);
            }
            catch (Exception ee)
            {
                timestamp = UpdateLog(timestamp, "Erro" + ee.Message.Replace("ERROR", "Erro") + " Broken Song at Pack" + song, true, Temp_Path, "", "", null, null);
                //var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                //if (chbx_Additional_Manipulations.GetItemChecked(30))
                //    CopyMoveFileSafely(FullPath, Pathh, chbx_Additional_Manipulations.GetItemChecked(75), ds.Tables[0].Rows[i].ItemArray[3].ToString(), false);
                ConfigRepository.Instance()["dlcm_Global2TempVariable"] = ee.Message.Replace("ERROR", "Erro") + " Broken Song at Pack";
                return true;
            }

            timestamp = UpdateLog(timestamp, "Done check song", true, Temp_Path, "", "DLCManager", null, null);
            return false;
        }
        public static void Ogg2Wav(string sourcePath, string destinationPath)
        {
            var cmdArgs = string.Format(" -o \"{1}\" \"{0}\"", sourcePath, destinationPath);
            var APP_OGGDEC = "DLCManager\\external_tools\\oggdec.exe";
            GeneralExtension.RunExternalExecutable(APP_OGGDEC, true, true, true, cmdArgs);
        }
        public static void Ogg2Preview(string sourcePath, string destinationPath, long msLength = 30000, long msStart = 4000)
        {
            var cmdArgs = string.Format(" -s {2} -l {3} \"{0}\" \"{1}\"", sourcePath, destinationPath, msStart, msLength);
            var APP_OGGCUT = "DLCManager\\external_tools\\oggCut.exe";
            GeneralExtension.RunExternalExecutable(APP_OGGCUT, true, true, true, cmdArgs);
        }

        public static void Wav2Ogg(string sourcePath, string destinationPath, int qualityFactor)
        {
            if (destinationPath == null)
                destinationPath = string.Format("{0}", Path.ChangeExtension(sourcePath, "ogg"));
            // interestingly ODLC uses 44100 or 48000 interchangeably ... so resampling is not necessary
            var cmdArgs = string.Format(" -q {2} \"{0}\" -o \"{1}\" -c author=\"catara\"", sourcePath, destinationPath, Convert.ToString(qualityFactor));
            var APP_OGGENC = "DLCManager\\external_tools\\oggenc.exe";
            GeneralExtension.RunExternalExecutable(APP_OGGENC, true, true, true, cmdArgs);
        }

        public static string GetAlternativeAudioFile(string MultiTrackType, string SongFolder, string OggPath)
        {
            var wav = MultiTrackType;
            var v = "";
            if (wav.Contains("Only Bass")) v = "bass.ogg";
            else if (wav.Contains("Only Drums")) v = "drums.ogg";
            else if (wav.Contains("(Only BackTrack)")) v = "instrumental.ogg";
            else if (wav.Contains("(No Drums No Vocal)")) v = "other.ogg";
            else if (wav.Contains("Only Vocal")) v = "vocals.ogg";
            var ogg = SongFolder + "\\multitracks\\" + v;
            if (File.Exists(ogg)) return ogg;
            else return OggPath;
        }

        /// <summary>
        /// Convert ogg or wave audio files to Wwise 2013 wem audio, including preview wem file.
        /// </summary>
        /// <param name="audioPath"></param>
        /// <param name="audioQuality"></param>
        /// <param name="previewLength"></param>
        /// <param name="chorusTime"></param>
        /// <returns>wemPath</returns>
        public static string Convert2Wem(string audioPath, int audioQuality = 4, long previewLength = 30000, long chorusTime = 4000)
        {
            // ExternalApps.VerifyExternalApps(); // for testing
            var audioPathNoExt = Path.Combine(Path.GetDirectoryName(audioPath), Path.GetFileNameWithoutExtension(audioPath));
            var oggPath = string.Format(audioPathNoExt + ".ogg");
            var wavPath = string.Format(audioPathNoExt + ".wav");
            var wemPath = string.Format(audioPathNoExt + ".wem");/*.Replace("_fixed","") */
            var oggPreviewPath = string.Format(audioPathNoExt + ".ogg");
            var wavPreviewPath = string.Format(audioPathNoExt + ".wav");
            var wemPreviewPath = string.Format(audioPathNoExt + ".wem");

            if (audioPath.Substring(audioPath.Length - 4).ToLower() == ".ogg") //in RS1 ogg was actually wwise
            {
                UtilitiesFunctions.Ogg2Wav(audioPath, wavPath); //detect quality here
                if (!File.Exists(oggPreviewPath))
                {
                    UtilitiesFunctions.Ogg2Preview(audioPath, oggPreviewPath, previewLength, chorusTime);
                    UtilitiesFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                }
                audioPath = wavPath;
            }

            if (audioPath.Substring(audioPath.Length - 4).ToLower() == ".wav")
            {
                if (!File.Exists(wavPreviewPath))
                {
                    if (!File.Exists(oggPath))
                    {
                        //may cause issues if you've got another guitar.ogg in folder, but it's extremely rare.
                        UtilitiesFunctions.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    else
                    {
                        DeleteFile(oggPath, false);
                        UtilitiesFunctions.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    UtilitiesFunctions.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                    UtilitiesFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                }

                if (!File.Exists(wemPath) || File.Exists(audioPath))//weird behavior fixed 04.11
                {
                    Wwise.Wav2Wem(audioPath, wemPath, audioQuality);
                    audioPath = wemPath;
                }
                else
                    UpdateLog(DateTime.Now, "Wav Missing: " + wavPreviewPath, true, c("dlcm_TempPath"), "", "MainDB", null, null);
            }

            if (audioPath.Substring(audioPath.Length - 4).ToLower() == ".wem" && !File.Exists(wemPreviewPath))
            {
                OggFile.Revorb(audioPath, oggPath, OggFile.GetWwiseVersion(audioPath)); //, Path.GetDirectoryName(Application.ExecutablePath)
                UtilitiesFunctions.Ogg2Wav(oggPath, wavPath);
                UtilitiesFunctions.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                UtilitiesFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                Wwise.Wav2Wem(wavPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            return audioPath;
        }

        public static string MetaChangeActiv(string meta, string activ)
        {
            var a = "";
            // txt = "ArtistSort:  " + " | " + "Artist:  " + " | " + "SongDisplayName:  " + " | " + "SongDisplayNameSort:  " + " | " + "Album:  " + " | " + "AlbumSort:" + " | " + "DLCName: " + " | " + "DLC_ID:  " + " | " + "File Name:  " + " | " + "Lyrics:  " + " | " + "Package internal Comment:  " + "\n";
            if (meta == "ArtistSort") a = (activ == "Yes" ? ": " : "(Inctive): ");
            else if (meta == "Artist") a = (activ == "Yes" ? ": " : "(Inctive): ");
            else if (meta == "SongDisplayName") a = (activ == "Yes" ? ": " : "(Inctive): ");
            else if (meta == "SongDisplayNameSort") a = (activ == "Yes" ? ": " : "(Inctive): ");
            else if (meta == "Album") a = (activ == "Yes" ? ": " : "(Inctive): ");
            else if (meta == "AlbumSort") a = (activ == "Yes" ? ": " : "(Inctive): ");
            // else if (meta == "DLCName") return activ == "Yes" ? ": " : "(Inctive): ";
            //else if (meta == "FileName") return ": ";
            else if (meta == "Lyrics") a = ((ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && activ == "Yes" && c("dlcm_AdditionalManipul102") != "Yes") ? ": " : "(Inctive): ");
            //else if (meta == "Package internal Comment") return "): ";
            else a = ": ";
            return a;
        }
        public static void Converters(string file, ConverterTypes converterType, bool mssON, bool WinOn)
        {

            var txtOgg2FixHdr = string.Empty;
            var txtWwiseConvert = string.Empty;
            var txtWwise2Ogg = string.Empty;
            var txtAudio2Wem = string.Empty;

            Dictionary<string, string> errorFiles = new Dictionary<string, string>();
            List<string> successFiles = new List<string>();
            var e = ""; var i = 0;
            do
            {
                i++;
                e = "";
                try
                {

                    var extension = Path.GetExtension(file);
                    //switch (converterType)
                    //{
                    //    case 
                    if (converterType == ConverterTypes.Ogg2Wem) UtilitiesFunctions.Convert2Wem(file, 4, 4 * 1000);
                    ////Delete any preview_preview file created..by....?ccc//:
                    //foreach (string prev_prev in Directory.GetFiles(Path.GetDirectoryName(file), "*preview_preview*", System.IO.SearchOption.AllDirectories))
                    //{
                    //    DeleteFile(prev_prev);
                    //}
                    //break;
                    //}

                    successFiles.Add(file);
                }
                catch (Exception ex)
                {
                    e = ex.Message;
                    var tsst = "uErro ...i: " + i + e.Replace("ERROR", "ERRO") + file; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);

                }
            }
            while (i < 1);

            if (!File.Exists(file.Replace("ogg", "wem"))) errorFiles.Add(file.Replace("ogg", "wem"), e);

            if (errorFiles.Count <= 0 && successFiles.Count > 0)
            {
                if (mssON) MessageBox.Show("Conversion complete!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (errorFiles.Count > 0 && successFiles.Count > 0)
            {
                StringBuilder alertMessage = new StringBuilder(
                    "Conversion complete with errors." + Environment.NewLine + Environment.NewLine);
                alertMessage.AppendLine(
                    "Files converted with success:" + Environment.NewLine);

                foreach (var sFile in successFiles)
                    alertMessage.AppendLine(string.Format("File: {0}", sFile));
                alertMessage.AppendLine("Files converted with error:" + Environment.NewLine);
                foreach (var eFile in errorFiles)
                    alertMessage.AppendLine(string.Format("File: {0}; error: {1}", eFile.Key, eFile.Value));

                if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                StringBuilder alertMessage = new StringBuilder(
                    "Conversion complete with errors." + Environment.NewLine);
                alertMessage.AppendLine(
                    "Files converted with error: " + Environment.NewLine);
                foreach (var eFile in errorFiles)
                    alertMessage.AppendLine(string.Format("File: {0}, error: {1}", eFile.Key, eFile.Value));

                if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool GetMEtaBeforePacking(int idsong, System.Windows.Forms.ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, System.Windows.Forms.ProgressBar maindbpb
            , DateTime timestamp, string AppWD, string SearchCmd, bool chbx_Beta, string Groupss, string SearchFields)
        {
            var where = "WHERE ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")";
            var sel = "SELECT * FROM Main " + (idsong == 0 ? where : "WHERE ID=" + idsong);
            //sel= SearchCmd.Replace(c("dlcm_SearchFields"), "*")//AudioPath, audioPreviewPath, OggPath, oggPreviewPath, ID, Folder_Name, Original_FileName, Song_Lenght, PreviewLenght, Audio_Hash, audioPreview_Hash, audioBitrate, audioSampleRate, Has_Had_Audio_Changed
            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[20000];
            SongRecord = GetRecord_s(sel, cnb, cnc); var noOfRecs = SongRecord[0].NoRec.ToInt32(); //SearchCmd.Replace(c("dlcm_SearchFields"), "*")
            if (pB_ReadDLCs is not null) { pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs; }
            if (maindbpb is not null) { maindbpb.Value = 0; maindbpb.Step = 1; maindbpb.Maximum = noOfRecs; }
            var j = 0; var k = 0; var l = 0; var m = 0; var n = 0;
            var ord_no = 0; var txt = "";

            foreach (var filez in SongRecord)
            {
                if (filez.ID == null) break;
                if (pB_ReadDLCs is not null) pB_ReadDLCs.Value++;
                if (maindbpb is not null) maindbpb.Value++;

                ord_no++;

                DLCPackageData data;
                var packagePlatform = filez.Folder_Name.GetPlatform();
                data = DLCPackageData.LoadFromFolder(filez.Folder_Name, packagePlatform);

                var FN = "";
                if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")/*repacked_Path + "\\" + */
                    FN = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], ord_no - 1, false, false, false, SongRecord, "", "", chbx_Beta, true, false, cnc);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
                else
                    FN = ((filez.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear.ToString() + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul91"] == "Yes") FN = Groupss + FN;

                if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes")
                {
                    FN = CleanPath(FN);
                }

                data = getmetadata(data, timestamp, filez, Groupss, false, ord_no.ToString(), false, chbx_Beta, FN, true);
                txt = ConfigRepository.Instance()["dlcm_Global2TempVariable"].Replace("\n", " ; ");/*"\n" + */
                //txt = txt.Substring(0, txt.Length - 2);
                SongRecord[ord_no - 1].Comments = txt.Replace("ArtistSort: ", "").Replace("Artist: ", "").Replace("SongDisplayName: ", "")
                    .Replace("SongDisplayNameSort: ", "").Replace("Album: ", "").Replace("AlbumSort: ", "").Replace("DLCName: ", "")
                    .Replace("DLC_ID: ", "").Replace("File Name: ", "").Replace("Lyrics: ", "").Replace("Package internal Comment: ", "").Trim();
            }

            //sorting
            for (var i = 0; i < SongRecord[0].NoRec.ToInt32() - 1; i++)
                for (var kk = i; kk < SongRecord[0].NoRec.ToInt32(); kk++)
                {
                    int comparison = System.String.Compare(SongRecord[i].Comments, SongRecord[kk].Comments, comparisonType: StringComparison.Ordinal);

                    if (comparison < 0)
                    {
                        var tmp = SongRecord[i].Comments;
                        SongRecord[i].Comments = SongRecord[kk].Comments;
                        SongRecord[kk].Comments = tmp;
                    }
                }

            //figureoutpadding
            int[] colsize = new int[50];
            txt = "ArtistSort" + MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"])
                + " | " + "Artist" + MetaChangeActiv("Artist", ConfigRepository.Instance()["dlcm_Activ_Artist"])
                + " | " + "SongDisplayName" + MetaChangeActiv("SongDisplayName", ConfigRepository.Instance()["dlcm_Activ_Title"])
                + " | " + "SongDisplayNameSort" + MetaChangeActiv("SongDisplayNameSort", ConfigRepository.Instance()["dlcm_Activ_TitleSort"])
                + " | " + "Album" + MetaChangeActiv("Album", ConfigRepository.Instance()["dlcm_Activ_Album"])
                + " | " + "AlbumSort" + MetaChangeActiv("AlbumSort", ConfigRepository.Instance()["dlcm_Activ_AlbumSort"])
                + " | " + "DLCName: " //+ MetaChangeActiv("DLCName", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + " | " + "DLC_ID: " //+ MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + " | " + "File Name" + MetaChangeActiv("FileName", ConfigRepository.Instance()["dlcm_Activ_FileName"])
                + " | " + "Lyrics" + MetaChangeActiv("Lyrics", ConfigRepository.Instance()["dlcm_Activ_LyricsInfo"])
                + " | " + "Package internal Comment: " //+ MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + "\n";
            string[] head = (txt).Split('|');
            for (var i = 0; i < head.Count(); i++)
                colsize[i] = head[i].Length + 2;

            for (var i = 0; i < SongRecord[0].NoRec.ToInt32(); i++)
            {
                string[] args = (SongRecord[i].Comments).Split(';');
                for (var jj = 0; jj < args.Count(); jj++)
                {
                    if (colsize[jj] < args[jj].Length) colsize[jj] = args[jj].Length + 2;
                }
            }

            //apply padding
            for (var i = 0; i < SongRecord[0].NoRec.ToInt32(); i++)
            {
                string[] args = (SongRecord[i].Comments).Split(';');
                SongRecord[i].Comments = "";
                for (var jj = 0; jj < args.Count(); jj++)//(args.Count() >= 8 ? j < 8 : j < args.Count())
                {
                    SongRecord[i].Comments += (colsize[jj] > 0 ? args[jj].PadRight(colsize[jj]) : args[jj]) + " | ";//- args[j].Lengthcolsize[j] +
                }
            }
            txt = ("ArtistSort" + MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"])).PadRight(colsize[0])
                + " | " + ("Artist" + MetaChangeActiv("Artist", ConfigRepository.Instance()["dlcm_Activ_Artist"])).PadRight(colsize[1])
                + " | " + ("SongDisplayName" + MetaChangeActiv("SongDisplayName", ConfigRepository.Instance()["dlcm_Activ_Title"])).PadRight(colsize[2])
                + " | " + ("SongDisplayNameSort" + MetaChangeActiv("SongDisplayNameSort", ConfigRepository.Instance()["dlcm_Activ_TitleSort"])).PadRight(colsize[3])
                + " | " + ("Album" + MetaChangeActiv("Album", ConfigRepository.Instance()["dlcm_Activ_Album"])).PadRight(colsize[4])
                + " | " + ("AlbumSort" + MetaChangeActiv("AlbumSort", ConfigRepository.Instance()["dlcm_Activ_AlbumSort"])).PadRight(colsize[5])
                + " | " + ("DLCName: ").PadRight(colsize[6]) //+ MetaChangeActiv("DLCName", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + " | " + ("DLC_ID: ").PadRight(colsize[7]) //+ MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + " | " + ("File Name" + MetaChangeActiv("FileName", ConfigRepository.Instance()["dlcm_Activ_FileName"])).PadRight(colsize[8])
                + " | " + ("Lyrics" + MetaChangeActiv("Lyrics", ConfigRepository.Instance()["dlcm_Activ_LyricsInfo"])).PadRight(colsize[9])
                + " | " + ("Package internal Comment: ").PadRight(colsize[10]) //+ MetaChangeActiv("ArtistSort", ConfigRepository.Instance()["dlcm_Activ_ArtistSort"]).Contains("nactive")
                + "\n";
            //txt = "ArtistSort:".PadRight(colsize[0]) + " | " + "Artist:".PadRight(colsize[1])
            //    + " | " + "SongDisplayName:".PadRight(colsize[2]) + " | " + "SongDisplayNameSort:".PadRight(colsize[3]) + " | " + "Album: ".PadRight(colsize[4]) +
            //    " | " + "AlbumSort:".PadRight(colsize[5]) + " | " + "DLCName: ".PadRight(colsize[6]) + " | " + "DLC_ID: ".PadRight(colsize[7]) + " | " + "File Name:".PadRight(colsize[8])
            //    + " | " + "Lyrics: ".PadRight(colsize[9]) + " | " + "Package internal Comment:".PadRight(colsize[10]) + "\n";
            for (var jj = 0; jj < SongRecord[0].NoRec.ToInt32(); jj++) txt += SongRecord[jj].Comments + "\n";
            ErrorWindow frm1 = new ErrorWindow(txt, "", "Summary before packing (sorted by ArtistSort)", true, true, false, "Continue", "Stop import", "", true);
            frm1.ShowDialog();
            if (frm1.StopImport) return true;
            else return false;
        }

        public static bool FixAudiofileInconsist(int idsong, System.Windows.Forms.ProgressBar pB_ReadDLCs, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, System.Windows.Forms.ProgressBar maindbpb, DateTime timestamp, string AppWD, string SearchCmd)
        {
            //var SearchCmd = c("");
            var SearchFields = c("dlcm_SearchFields");
            var ret = false;
            var PreviewStart = c("dlcm_PreviewStart");
            var PreviewEnd = c("dlcm_PreviewLenght");
            var where = "WHERE ID IN(" + SearchCmd.Replace(" * ", " ID ").Replace("; ", "").Replace(SearchFields, "ID") + ")";
            var sel = "SELECT AudioPath, audioPreviewPath, OggPath, oggPreviewPath, ID, Folder_Name, Original_FileName" +
                ", Song_Lenght, PreviewLenght, Audio_Hash, audioPreview_Hash, audioBitrate, audioSampleRate, Has_Had_Audio_Changed FROM Main " + (idsong == 0 ? where : "WHERE ID=" + idsong);
            var cmdup = "UPDATE Main Set Is_Broken=\"\",FilesMissingIssues=\"\" " + where;
            DataSet dis = new DataSet(); dis = UpdateDB("Main", cmdup + ";", cnb, cnc);
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Main", sel, "", cnb, cnc);
            var noOfRecs = GetNoRec(SongRecord, cnb, cnc);
            if (pB_ReadDLCs is not null) { pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRecs; }
            if (maindbpb is not null)
            {
                maindbpb.Value = 0; maindbpb.Step = 1; maindbpb.Maximum = noOfRecs;
            }
            var j = 0; var k = 0; var l = 0; var m = 0; var n = 0;
            for (var i = 0; i < noOfRecs; i++)
            {

                //if (!ShowDialogue(dlg, " process all ", noOfRecs, "fix", 2)) break;
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
                var wem = SongRecord.Tables[0].Rows[i].ItemArray[1].ToString(); //preview
                var wemo = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                var ogg = SongRecord.Tables[0].Rows[i].ItemArray[3].ToString();
                var oggo = SongRecord.Tables[0].Rows[i].ItemArray[2].ToString();
                var id = SongRecord.Tables[0].Rows[i].ItemArray[4].ToString();
                var fldn = SongRecord.Tables[0].Rows[i].ItemArray[5].ToString();
                var Original_FileName = SongRecord.Tables[0].Rows[i].ItemArray[6].ToString();
                var Song_Lenght = SongRecord.Tables[0].Rows[i].ItemArray[7].ToString();
                var PreviewLenght = SongRecord.Tables[0].Rows[i].ItemArray[8].ToString();
                var Audio_Hash = SongRecord.Tables[0].Rows[i].ItemArray[9].ToString();
                var audioPreview_Hash = SongRecord.Tables[0].Rows[i].ItemArray[10].ToString();
                var audioBitrate = SongRecord.Tables[0].Rows[i].ItemArray[11].ToString();
                var audioSampleRate = SongRecord.Tables[0].Rows[i].ItemArray[12].ToString();
                var Has_Had_Audio_Changed = SongRecord.Tables[0].Rows[i].ItemArray[13].ToString();

                RocksmithToolkitLib.Platform platformaud = fldn.GetPlatform();
                var broken = false;

                wemo = checkaudioifreadable(wemo, AppWD, timestamp) ? "" : wemo;
                wem = checkaudioifreadable(wem, AppWD, timestamp) ? "" : wem;
                oggo = checkaudioifreadable(oggo, AppWD, timestamp) ? "" : oggo;
                ogg = checkaudioifreadable(ogg, AppWD, timestamp) ? "" : ogg;

                if (wemo == "" && oggo != "")
                {
                    Converters(oggo, ConverterTypes.Ogg2Wem, false, true);
                    File.Copy(oggo.Replace(".ogg", ".wem"), SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(), true);
                    if (!checkaudioifreadable(SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(), AppWD, timestamp))//((File.Exists()
                        wemo = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                    else
                    {
                        j++;
                        timestamp = UpdateLog(timestamp, j + " wem broken " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //wemo = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                        var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"wem broken\" WHERE ID = " + id + "";
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        broken = true;
                    }
                }

                if (oggo == "" && wemo != "")
                {
                    File.Copy(wemo, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wemo), true);
                    OggFile.Revorb(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wemo), c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(SongRecord.Tables[0].Rows[i].ItemArray[2].ToString()), OggFile.GetWwiseVersion(Path.GetExtension(wemo)));
                    File.Copy(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(SongRecord.Tables[0].Rows[i].ItemArray[2].ToString()), SongRecord.Tables[0].Rows[i].ItemArray[2].ToString(), false);
                    DeleteFileIfExisting(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(oggo));
                    DeleteFileIfExisting(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wemo));
                    if (!checkaudioifreadable(SongRecord.Tables[0].Rows[i].ItemArray[2].ToString(), AppWD, timestamp))//((File.Exists()
                        oggo = SongRecord.Tables[0].Rows[i].ItemArray[2].ToString();
                    else
                    {
                        j++;
                        timestamp = UpdateLog(timestamp, j + " wem broken " + oggo, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //wemo = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                        var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"ogg broken\" WHERE ID = " + id + "";
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        broken = true;
                    }
                }

                System.IO.FileInfo fi = null;
                try
                {
                    var rt = "";
                    var fil = File.Exists(wem) ? new System.IO.FileInfo(wem).Length : 0;
                    if (!File.Exists(ogg) && Directory.Exists(Path.GetDirectoryName(ogg)) && File.Exists(wem))
                    {
                        fi = new System.IO.FileInfo(wem);
                        File.Copy(wem, c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wem), true);
                        //  UtilitiesFunctions.Converters(t, UtilitiesFunctions.ConverterTypes.WEM, false, false);
                        OggFile.Revorb(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wem), c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(ogg), OggFile.GetWwiseVersion(Path.GetExtension(wem)));
                        File.Copy(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(ogg), ogg, false);
                        DeleteFileIfExisting(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(ogg));
                        DeleteFileIfExisting(c("dlcm_TempPath") + "\\0_temp\\" + Path.GetFileName(wem));

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
                        //                //Fix Bitrate
                        //                var cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "";
                        //                FixAudioIssues(cmd, cnb, AppWD, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, false, "MainDB", cnc);

                        //                j++; timestamp = UpdateLog(timestamp, SongRecord.Tables[0].Rows[i].ItemArray[5].ToString() + "_" + j + "/" + i + "/" + noOfRecs + " bitrate " + stdoutx.Replace("\r\n", ""), true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        //            }

                        //    }
                        //pB_ReadDLCs.Maximum = noOfRecs;
                        if (File.Exists(ogg) && Directory.Exists(Path.GetDirectoryName(ogg)))
                        {
                            k++;
                            //timestamp = UpdateLog(timestamp, "Fixing " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            //var cmdupd = "UPDATE Main Set oggPreviewPath=\"" + ogg + "\" WHERE ID = " + id + "";
                            //DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        }
                        else
                        {
                            timestamp = UpdateLog(timestamp, "Failed " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            l++;
                        }
                    }
                    else if (!Directory.Exists(Path.GetDirectoryName(ogg)))
                    {
                        j++;
                        timestamp = UpdateLog(timestamp, j + " Folder missing " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"Folder Missing\" WHERE ID = " + id + "";
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        broken = true;
                    }
                    else if ((fil == 0 || !File.Exists(wem)) && (File.Exists(wem.Replace(".wem", "_fixed.ogg")) || File.Exists(wem.Replace("preview_fixed.wem", "_fixed.ogg"))))
                    {
                        UpdateLog(timestamp, i + "-" + wem + " Fixing zero file size ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        MainDBfields[] SonRecord = new MainDBfields[20000];
                        var cmd = "SELECT * FROM Main WHERE ID=" + id + ";";
                        SonRecord = GetRecord_s(cmd, cnb, cnc);
                        if (fil == 0 && File.Exists(wem)) DeleteFileIfExisting(wem);
                        //foreach (var filez in SonRecord) 
                        FixPreview(SonRecord[0], DateTime.Now);
                        var fifi = new System.IO.FileInfo(wem);
                        if (fifi.Length == 0 || !File.Exists(wem))
                        {
                            l++;
                            timestamp = UpdateLog(timestamp, j + " failed at fixing preview missing " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                            var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"failed at fixing Missing preview\" WHERE ID = " + id + "";
                            DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                        }
                        else
                        {
                            m++;
                            //StartProcesss(Path.GetDirectoryName(wem), null);
                        }
                    }
                    else if (File.Exists(wemo) && File.Exists(oggo) && !File.Exists(wem))
                    {
                        UpdateLog(timestamp, i + "-" + wem + " Adding preview ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var startInfo = new ProcessStartInfo
                        {
                            FileName = Path.Combine(AppWD, "oggcut.exe"),
                            WorkingDirectory = AppWD.Replace("external_tools", "")
                        };
                        var t = oggo;
                        var tt = t.Replace(".ogg", "_preview_fixed.ogg");
                        string[] timepieces = PreviewStart.ToString().Split(':');
                        TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                        startInfo.Arguments = string.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                            t,
                                                            tt,
                                                            r.TotalMilliseconds,
                                                            (r.TotalMilliseconds + (PreviewEnd.ToInt32() * 1000)));
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                        if (File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                if (DDC.ExitCode == 0)
                                {
                                    var wwisePath = "";
                                    if (!string.IsNullOrEmpty(c("general_wwisepath")))
                                        wwisePath = c("general_wwisepath");
                                    else
                                        wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                                    if (wwisePath == "")
                                    {
                                        ErrorWindow frm1 = new ErrorWindow("In order to add a preview, please Install Wwise Launcher then Wwise v"
                                            + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the " +
                                            "Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files" +
                                            " Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation",
                                            false, false, true, "", "", "", false);
                                        frm1.ShowDialog();
                                    }
                                    Converters(tt, ConverterTypes.Ogg2Wem, false, true);
                                    var hf = GetHash(tt.Replace(".ogg", ".wem"));
                                    var cmdupd = "UPDATE Main Set audioPreviewPath=\"" + tt.Replace(".ogg", ".wem") + "\", oggPreviewPath=\"" + tt +
                                        "\",AudioPreview_Hash=\"" + hf + "\" WHERE ID = " + id + "";
                                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                                    //txt_OggPreviewPath.Text = tt;
                                    //chbx_HasPreview.Checked = true;
                                    //txt_AudioPreviewPath.Text = tt.Replace(".ogg", ".wem");
                                    //chbx_HasPreview.Checked = true;
                                    //btn_PlayPreview.Enabled = true;
                                    //txt_Preview_Hash.Text = GetHash(tt);
                                    //AddPreview = true;
                                    //chbx_AudioChanged.Checked = true;
                                    //if (chbx_AutoSave.Checked) SaveRecord();
                                    //chbx_A_IsImprovedWithDM.Checked = true;
                                }
                                else
                                {
                                    timestamp = UpdateLog(timestamp, j + " no main " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                                    var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"no main songs\" WHERE ID = " + id + "";
                                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                                }
                            }
                    }
                    else if (!File.Exists(wem))
                    {
                        File.Copy(getOldAudio(true, Original_FileName, platformaud, SongRecord.Tables[0].Rows[i].ItemArray[1].ToString(),
                            SongRecord.Tables[0].Rows[i].ItemArray[0].ToString()), SongRecord.Tables[0].Rows[i].ItemArray[1].ToString(), true);
                        wem = SongRecord.Tables[0].Rows[i].ItemArray[1].ToString();
                    }
                    else if (!File.Exists(wemo))
                    {
                        File.Copy(getOldAudio(false, Original_FileName, platformaud, SongRecord.Tables[0].Rows[i].ItemArray[1].ToString(),
                        SongRecord.Tables[0].Rows[i].ItemArray[0].ToString()), SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(), true);
                        wemo = SongRecord.Tables[0].Rows[i].ItemArray[0].ToString();
                    }
                    else
                        ;
                }
                catch (Exception ex)
                {
                    var tsst = "Erro ..." + ex.Message; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    timestamp = UpdateLog(timestamp, j + " failed at fixing preview missing " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"Failed at fixing preview\" WHERE ID = " + id + "";
                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                }

                //if (wemo == "")
                //{
                //    Converters(oggo, ConverterTypes.Ogg2Wem, false, true);
                //    File.Copy(oggo.Replace(".ogg", ".wem"), SongRecord.Tables[0].Rows[i].ItemArray[1].ToString());
                //    if (File.Exists(SongRecord.Tables[0].Rows[i].ItemArray[1].ToString()))
                //    {
                //        wem = SongRecord.Tables[0].Rows[i].ItemArray[1].ToString();

                //        File.Copy(oggo.Replace(".ogg", ".wem"), SongRecord.Tables[0].Rows[i].ItemArray[1].ToString());
                //    }
                //    else
                //    {
                //        File.Copy(oggo.Replace(".ogg", ".wem"), SongRecord.Tables[0].Rows[i].ItemArray[1].ToString());
                //        //tj++;imestamp = UpdateLog(timestamp, j + " Folder missing " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //        //var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"Folder Missing\" WHERE ID = " + id + "";
                //        //DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                //        //broken = true;
                //    }
                //}

                //verify if too many audios
                string bnks = "";
                //calc file size
                try
                {
                    if (!wem.Contains("AC\\DC") && !wem.Contains("AC/DC"))
                    {
                        var a1 = wem != "" ? Path.GetDirectoryName(wem.Substring(0, wem.IndexOf("\\audio\\") + 6)) : "";
                        var a2 = ogg != "" ? Path.GetDirectoryName(ogg.Substring(0, ogg.IndexOf("\\audio\\") + 6)) : "";
                        var a3 = wemo != "" ? Path.GetDirectoryName(wemo.Substring(0, wemo.IndexOf("\\audio\\") + 6)) : "";
                        var a4 = oggo != "" ? Path.GetDirectoryName(oggo.Substring(0, oggo.IndexOf("\\audio\\") + 6)) : "";
                        if (a1 != fldn || a2 != fldn || a3 != fldn || a4 != fldn) timestamp = UpdateLog(timestamp, "Weird Erro\n" + wem + "\n" + wemo + "\n" + ogg + "\n" + oggo
                                     + "\n" + fldn, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        if (!broken)
                            try
                            {
                                if (wem.Contains("preview_fixed") && File.Exists(wem))/*.Replace("preview_fixed", "preview")*/
                                {
                                    n++;
                                    timestamp = UpdateLog(timestamp, " renaming preview_fixed to preview " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                                    if (File.Exists(wem)) File.Copy(wem, wem.Replace("preview_fixed", "preview"), true);
                                    var t = GetHash(wem.Replace("preview_fixed", "preview"));
                                    var cmdupd = "UPDATE Main Set audioPreviewPath=\"" + wem.Replace("preview_fixed", "preview") + "\",AudioPreview_Hash=\"" + t + "\" WHERE ID = " + id + "";
                                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                                    DeleteFileIfExisting(wem);
                                    wem = wem.Replace("preview_fixed", "preview");
                                }
                                var xmlF = Directory.GetFiles(fldn, "*.bnk", System.IO.SearchOption.AllDirectories);
                                foreach (var xml in xmlF) if (xml != wem && xml != wemo)
                                        bnks += ";" + Path.GetFileName(xml);
                                var xmlFil = Directory.GetFiles(fldn, "*.wem", System.IO.SearchOption.AllDirectories);
                                foreach (var xml in xmlFil)
                                    if (xml != wem && xml != wemo)
                                        if (xml.Contains("preview") && !bnks.Contains(Path.GetFileName(xml.Replace(".wem", ".bnk"))))
                                            DeleteFile(xml, false);
                                        else if (!xml.Contains("preview") && !bnks.Contains(Path.GetFileName(xml.Replace(".wem", ".bnk"))))
                                            DeleteFile(xml, false);
                                //else 
                                //else if (!File.Exists(wem) && File.Exists(wem.Replace("preview", "preview_fixed")))
                                //    File.Copy(wem.Replace("preview", "preview_fixed"), wem, true);
                                //else
                                //    ;
                                if (File.Exists(wem) && File.Exists(wem.Replace("preview", "preview_fixed")) && !wem.Contains("preview_fixed"))
                                    DeleteFile(wem.Replace("preview", "preview_fixed"), false);
                                var xmlFi = Directory.GetFiles(fldn, "*.ogg", System.IO.SearchOption.AllDirectories);
                                foreach (var xml in xmlFi)
                                    if (xml != ogg && xml != oggo && !bnks.Contains(Path.GetFileName(oggo.Replace("_fixed", "").Replace(".ogg", ".bnk"))))
                                        if (!xml.Contains("\\audio\\multitracks\\")) DeleteFile(xml, false);
                                var xmlFilk = Directory.GetFiles(fldn, "*.wav", System.IO.SearchOption.AllDirectories);
                                foreach (var xml in xmlFilk) if (!xml.Contains("\\audio\\multitracks\\")) DeleteFile(xml, false);
                            }
                            catch (Exception ex)
                            {
                                ;
                            }
                    }
                    else if ((wem.Contains("AC\\DC") || wem.Contains("AC/DC")) && !broken)
                    {
                        timestamp = UpdateLog(timestamp, j + " acdc folder " + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                        var cmdupd = "UPDATE Main Set Is_Broken=\"" + "Yes" + "\", FilesMissingIssues=FilesMissingIssues+\"acdc Missing\" WHERE ID = " + id + "";
                        DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                    }

                }
                catch (Exception exx)
                {
                    var tsst = "Erro at too many wem verify ..." + exx.Message; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }

                if (pB_ReadDLCs is not null)
                {
                    pB_ReadDLCs.Increment(1); ProgressWithText(pB_ReadDLCs.Value + "/" + pB_ReadDLCs.Maximum + "REviewing ogg existence.." + ogg, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                }
                if (maindbpb is not null)
                {
                    maindbpb.Increment(1); ProgressWithText(maindbpb.Value + "/" + maindbpb.Maximum + "REviewing ogg existence.." + ogg, maindbpb, rtxt_StatisticsOnReadDLCs);
                }
                if (checkaudioifreadable(wemo, AppWD, timestamp)) { FixAudiofileInconsist(id.ToInt32(), pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd); UpdateLog(timestamp, "fix audio file incost" + wemo, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
                if (checkaudioifreadable(wem, AppWD, timestamp)) { FixAudiofileInconsist(id.ToInt32(), pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd); UpdateLog(timestamp, "fix audio file incost" + wem, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
                if (checkaudioifreadable(oggo, AppWD, timestamp)) { FixAudiofileInconsist(id.ToInt32(), pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd); UpdateLog(timestamp, "fix audio file incost" + oggo, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }
                if (checkaudioifreadable(ogg, AppWD, timestamp)) { FixAudiofileInconsist(id.ToInt32(), pB_ReadDLCs, rtxt_StatisticsOnReadDLCs, maindbpb, timestamp, AppWD, SearchCmd); UpdateLog(timestamp, "fix audio file incost" + ogg, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); }

                ret = CheckAndUpdateAudioIfChanged(false, wem, wemo, Song_Lenght, PreviewLenght, Audio_Hash, audioBitrate, audioSampleRate, Has_Had_Audio_Changed, id, audioPreview_Hash, timestamp);//filesize, 

                UpdateLog(timestamp, k + " Fixed ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                UpdateLog(timestamp, l + " Failed at fixing ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                UpdateLog(timestamp, m + " Preview Fixed ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                UpdateLog(timestamp, n + " Preview file name adjusted ", true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                //DataSet dhf = new DataSet(); var cmd = "SELECT XMLFilePath,ID FROM Arrangements where CleanedXML_Hash=\"\"" + GetArrOfficSQLTxt(arrangoff);// WHERE CDLC_ID=" + txt_ID.Text;
                //    dhf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb, cnc);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
                //    pB_ReadDLCs.Maximum = GetNoRec(dhf, cnb, cnc);//dhf.Tables[0].Rows.Count;dhf.Tables[0].Rows.Count
                //    for (var h = 0; h < pB_ReadDLCs.Maximum; h++)
                //    {
                //        var rt = GetHashCleanXML(dhf.Tables[0].Rows[h].ItemArray[0].ToString());
                //        var cmdupd = "UPDATE Arrangements Set CleanedXML_Hash=\"" + rt + "\" WHERE ID = " + dhf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                //        DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb, cnc);
                //        if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                //        var tst = "XML hasin: " + h; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //    }
                //    DataSet ghf = new DataSet(); cmd = "SELECT JSONFilePath,ID FROM Arrangements where Json_Hash=\"\"" + GetArrOfficSQLTxt(arrangoff);// WHERE CDLC_ID=" + txt_ID.Text;
                //    ghf = SelectFromDB("Arrangements", cmd, c("dlcm_DBFolder"), cnb, cnc);/*XMLFile_Hash, SNGFileHash, ConversionDateTime, Has_Sections,*/
                //    pB_ReadDLCs.Maximum = GetNoRec(ghf, cnb, cnc); //ghf.Tables[0].Rows.Count
                //    ; pB_ReadDLCs.Value = 0;
                //    for (var h = 0; h < pB_ReadDLCs.Maximum; h++)
                //    {
                //        var rt = ghf.Tables[0].Rows[h].ItemArray[0].ToString();//.Replace("\\arr\\", "\\bin\\");
                //                                                               //if (chbx_Additional_Manipulations.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized                                
                //                                                               //    rt = (rt.Replace(".xml", ".sng").Replace("\\EOF\\", "\\Toolkit\\"));
                //                                                               //else
                //                                                               //    rt = rt.Replace(".xml", ".sng").Replace("\\songs\\bin", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                //                                                               //snghlist.Add(GetHash(s1));
                //        rt = GetHashCleanXML(rt);
                //        var cmdupd = "UPDATE Arrangements Set Json_Hash=\"" + rt + "\" WHERE ID = " + ghf.Tables[0].Rows[h].ItemArray[1].ToString() + "";
                //        DataSet dus = new DataSet(); dus = UpdateDB("Arrangements", cmdupd + ";", cnb, cnc);
                //        if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                //        var tst = "Json hasin: " + pB_ReadDLCs.Value; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "MainDB", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
            return ret;
        }

        private static bool checkaudioifreadable(string audio, string AppWD, DateTime timestamp)
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
                timestamp = UpdateLog(timestamp, (!File.Exists(xx) ? "Install MediaInfo CLI if you want to use it." : "") + (!File.Exists(audio) ? "File still missing" : "")
                    , true, c("dlcm_TempPath"), "", "MainDB", null, null);
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
                    timestamp = UpdateLog(timestamp, audio + stdoutx, true, c("dlcm_TempPath"), "", "MainDB", null, null);

                }
            return bad;
        }

        private static float getwemdetails(string audio, string AppWD, DateTime timestamp, string detail)
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
                timestamp = UpdateLog(timestamp, (!File.Exists(xx) ? "Install MediaInfo CLI if you want to use it." : "") + (!File.Exists(audio) ? "File still missing" : "")
                    , true, c("dlcm_TempPath"), "", "MainDB", null, null);
                return !File.Exists(xx) ? -1 : (!File.Exists(audio) ? 0 : -1);
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = xx,
                WorkingDirectory = AppWD
            };
            var t = audio;
            startInfo.Arguments = string.Format(" --Inform=Audio;%" + detail + "% \"{0}\"", t);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            float bad = 0;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start();
                    string stdoutx = DDC.StandardOutput.ReadToEnd();
                    string stderrx = DDC.StandardError.ReadToEnd();
                    DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (stdoutx != "\r\n")
                        if (float.Parse(stdoutx.Replace("\r\n", ""), NumberStyles.Float, CultureInfo.CurrentCulture) <= 0)
                            bad = -1;
                        else
                            bad = float.Parse(stdoutx.Replace("\r\n", ""), NumberStyles.Float, CultureInfo.CurrentCulture);
                    timestamp = UpdateLog(timestamp, audio + stdoutx, true, c("dlcm_TempPath"), "", "MainDB", null, null);

                }
            return bad;//.ToString().ToInt32();
        }

        public static string getOldAudio(bool preview, string Original_FileName, Platform platform, string audioPreviewPath, string AudioPath)
        {
            var unpackedDir = Packer.Unpack(c("dlcm_TempPath") + "\\0_old\\" + Original_FileName, c("dlcm_TempPath") + "\\0_temp\\", platform, true, true);
            DLCPackageData infoz = null;
            infoz = DLCPackageData.LoadFromFolder(unpackedDir, platform);

            var oldprev = audioPreviewPath;
            var olda = AudioPath;
            audioPreviewPath = infoz.OggPreviewPath is null ? infoz.OggPath : infoz.OggPreviewPath;
            AudioPath = infoz.OggPath;


            var wem = checkaudiofilesifreadable(audioPreviewPath) ? oldprev : audioPreviewPath;
            var wemo = checkaudiofilesifreadable(AudioPath) ? olda : AudioPath;
            //var oggo = checkaudiofilesifreadable(filez.oggPreviewPath.ToString()) ? "" : filez.oggPreviewPath;
            //var ogg = checkaudiofilesifreadable(filez.OggPath.ToString()) ? "" : filez.OggPath;
            //data.OggPreviewPath = wemo;
            //data.OggPath = wem;
            if (preview) return wem;
            return wemo;

        }

        public static bool CheckAndUpdateAudioIfChanged(bool preview, string wem, string wemo, string Song_Lenght, string PreviewLenght, string Audio_Hash, string audioBitrate, string audioSampleRate, string Has_Had_Audio_Changed, string id, string audioPreview_Hash, DateTime timestamp)
        {

            var PreviewLenghtN = "";
            var Song_LenghtN = "";
            var Audio_HashN = GetHash(wemo);
            var audioBitrateN = "";
            var audioSampleRateN = "";
            var audioPreview_HashN = GetHash(wem);

            //using (var vorbis = new NVorbis.VorbisReader(wemo))
            //{
            //    audioBitrateM = vorbis.NominalBitrate.ToString();
            //    audioSampleRateN = vorbis.SampleRate.ToString();
            //    Song_LenghtN = vorbis.TotalTime.ToString();
            //}

            //using (var vorbis = new NVorbis.VorbisReader(wem))
            //{
            //    PreviewLenghtN = vorbis.TotalTime.ToString();
            //}

            audioBitrateN = getwemdetails(wemo, AppWD, timestamp, "BitRate").ToString();
            audioSampleRateN = getwemdetails(wemo, AppWD, timestamp, "SamplingRate").ToString();
            Song_LenghtN = (getwemdetails(wemo, AppWD, timestamp, "Duration") / 1000).ToString();
            PreviewLenghtN = (getwemdetails(wem, AppWD, timestamp, "Duration") / 1000).ToString();


            //AudioPath,audioPreviewPath
            //,PreviewLenght,Audio_Hash,audioPreview_Hash,audioBitrate,audioSampleRate,Has_Had_Audio_Changed,Song_Lenght //OggPath,oggPreviewPath

            var cmdupd = "UPDATE Main Set PreviewLenght=\"" + PreviewLenghtN + "\", Song_Lenght=\"" + Song_LenghtN +
                "\", Audio_Hash=\"" + Audio_HashN + "\", audioBitrate=\"" + audioBitrateN + "\", audioSampleRate=\"" + audioSampleRateN + "\", Has_Had_Audio_Changed=\"Yes\""
                + ", audioPreview_Hash=\"" + audioPreview_HashN + "\" WHERE ID = " + id + "";
            DataSet dus = new DataSet();
            if (float.Parse(PreviewLenghtN) > 0 && float.Parse(Song_LenghtN) > 0 && Audio_HashN != "" && audioBitrateN.ToInt32() >= 0 && audioSampleRateN.ToInt32() > 0 && audioPreview_HashN != "")
                if (PreviewLenghtN != PreviewLenght || Song_LenghtN != Song_Lenght || Audio_HashN != Audio_Hash || audioBitrateN != audioBitrate || audioSampleRateN != audioSampleRate || audioPreview_HashN != audioPreview_Hash)
                {
                    dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                    return true;
                }
            return false;
        }

        public static void CleanAudioFolder(string wem, string wemo, string ogg, string oggo, string fldn, int id)
        {

            string bnks = "";
            try
            {
                if (wem.Contains("preview_fixed") && File.Exists(wem))/*.Replace("preview_fixed", "preview")*/
                {
                    if (File.Exists(wem)) File.Copy(wem, wem.Replace("preview_fixed", "preview"), true);
                    var t = GetHash(wem.Replace("preview_fixed", "preview"));
                    var cmdupd = "UPDATE Main Set audioPreviewPath=\"" + wem.Replace("preview_fixed", "preview") + "\",AudioPreview_Hash=\"" + t + "\" WHERE ID = " + id + "";
                    DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb, cnc);
                    DeleteFileIfExisting(wem);
                    wem = wem.Replace("preview_fixed", "preview");
                }
                var xmlF = Directory.GetFiles(fldn, "*.bnk", System.IO.SearchOption.AllDirectories);
                foreach (var xml in xmlF) if (xml != wem && xml != wemo)
                        bnks += ";" + Path.GetFileName(xml);
                var xmlFil = Directory.GetFiles(fldn, "*.wem", System.IO.SearchOption.AllDirectories);
                foreach (var xml in xmlFil)
                    if (xml != wem && xml != wemo)
                        if (xml.Contains("preview") && !bnks.Contains(Path.GetFileName(xml.Replace(".wem", ".bnk"))))
                            DeleteFile(xml, false);
                        else if (!xml.Contains("preview") && !bnks.Contains(Path.GetFileName(xml.Replace(".wem", ".bnk"))))
                            DeleteFile(xml, false);

                if (File.Exists(wem) && File.Exists(wem.Replace("preview", "preview_fixed")) && !wem.Contains("preview_fixed"))
                    DeleteFile(wem.Replace("preview", "preview_fixed"), false);
                var xmlFi = Directory.GetFiles(fldn, "*.ogg", System.IO.SearchOption.AllDirectories);
                foreach (var xml in xmlFi)
                    if (xml != ogg && xml != oggo && !bnks.Contains(Path.GetFileName(oggo.Replace("_fixed", "").Replace(".ogg", ".bnk"))))
                        if (!xml.Contains("\\audio\\multitracks\\")) DeleteFile(xml, false);
                var xmlFilk = Directory.GetFiles(fldn, "*.wav", System.IO.SearchOption.AllDirectories);
                foreach (var xml in xmlFilk) if (!xml.Contains("\\audio\\multitracks\\")) DeleteFile(xml, false);
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public static bool IsNumbers(string value)
        {
            return value.All(char.IsDigit);
        }

        public static string IndexOfTest(string strSource)
        {
            //string strSource = "This is the string which we will perform the search on";
            Console.WriteLine("The search string is:{0}\"{1}\"{0}", Environment.NewLine, strSource);

            string strTarget = "";
            int found = 0;
            int totFinds = 0;

            do
            {
                Console.Write("Please enter a search value to look for in the above string (hit Enter to exit) ==> ");

                strTarget = Console.ReadLine();

                if (strTarget != "")
                {

                    for (int i = 0; i < strSource.Length; i++)
                    {

                        found = strSource.IndexOf(strTarget, i);

                        if (found >= 0)
                        {
                            totFinds++;
                            i = found;
                        }
                        else
                            break;
                    }
                }
                else
                    return "-";

                Console.WriteLine("{0}The search parameter '{1}' was found {2} times.{0}",
                        Environment.NewLine, strTarget, totFinds);

                totFinds = 0;

            } while (true);
        }

        public static void ProgressWithText(string txt, System.Windows.Forms.ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            //pB_ReadDLCs.CreateGraphics().Clear(System.Drawing.Color.HotPink);
            pB_ReadDLCs.CreateGraphics().DrawString(txt, new System.Drawing.Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        }
        public static void CreateLog(string logPath, string fn)
        {
            if (fn == "")
            {
                fn = (logPath == null || !DirectoryExists(logPath) ? (DirectoryExists(c("dlcm_TempPath") + "\\0_log") ? c("dlcm_TempPath") + "0_log" : AppWD.Replace("DLCManager\\external_tools", ""))
                   : logPath) + "\\" + c("dlcm_Split4Pack") + "current_" + "" + "temp" + ".txt";/*MultithreadNo +c("dlcm_Split4Pack")*/
            }
            //var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            // Clean temp log
            //var fnl = (logPath == null || !DirectoryExists(logPath) ? c("dlcm_TempPath") + "\\0_log" : logPath) + "\\" + "" + "current_temp.txt";
            //var starttmp = DateTime.Now;
            try
            {
                if (File.Exists(fn))//(logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + "" + "current_temp.txt"))
                {
                    //File.Copy((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_temp.txt"
                    //      , (logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_temp" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + ".txt", true);

                    //if exist why am i creating it?
                    //FileStream swt = File.Open((logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_temp.txt", FileMode.Create);
                    //swt.Close();
                    //swt.Dispose();
                    ;
                }
                else
                {
                    //var a = (logPath == null || !DirectoryExists(logPath) ? Log_PSPath : logPath) + "\\" + c("dlcm_Split4Pack") + "current_temp.txt";
                    if (!DirectoryExists(Path.GetDirectoryName(fn))) Directory.CreateDirectory(Path.GetDirectoryName(fn));
                    FileStream swt = File.Open(fn, FileMode.Create);
                    swt.Close();
                    swt.Dispose();
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }

        public static void unzipdb(string destination, string zippath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(c("dlcm_DBFolder"))))
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(c("dlcm_DBFolder")));
                }
                catch (Exception ezx)
                {
                    var tsst = "Error at path create..." + ezx; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                }

            if (!File.Exists(zippath) || !Directory.Exists(destination)) return;

            try
            {
                ZipFile.ExtractToDirectory(zippath, destination);
                System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(Path.GetDirectoryName(c("dlcm_DBFolder")));
                foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                {
                    if (!file.FullName.Contains("_bk")) continue;
                    CopyMoveFileSafely(file.FullName, file.FullName.Replace("_bk", ""), false, null, true);
                    return;
                }
            }
            catch (Exception es)
            {
                var tsst = "Issues at copy filestrem..." + es.Message.ToString(); var timestamp = UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            }
        }
        public static void AddFileToZip(string zipFilename, string fileToAdd)
        {
            if (!File.Exists(fileToAdd)) return;
            //think of adding a copy to archive if file read only (e.g. when repssing save)
            using (Package zip = global::System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + Path.GetFileName(fileToAdd);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                try
                {
                    using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                    {
                        using (Stream dest = part.GetStream())
                        {
                            CopyStream(fileStream, dest);
                        }
                    }
                }
                catch (Exception es)
                {
                    try
                    {

                        CopyMoveFileSafely(fileToAdd, fileToAdd + "_bk", true, null, true);
                        fileToAdd = fileToAdd + "_bk";
                        using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                        {
                            using (Stream dest = part.GetStream())
                            {
                                CopyStream(fileStream, dest);
                            }
                        }
                        DeleteFile(fileToAdd, true);
                    }
                    catch (Exception ex)
                    {
                        var tsst = "Error at copy filestrem..." + es.Message.ToString() + ex.Message.ToString(); var timestamp = UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    }
                }
            }
        }
        public static void CopyStream(global::System.IO.FileStream inputStream, global::System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bytesRead;
            }
        }
        public static void RemoveFileFromZip(string zipFilename, string fileToRemove)
        {
            using (Package zip = global::System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + fileToRemove;
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
            }
        }
        public static void Remove_Content_Types_FromZip(string zipFileName)
        {
            //using (ZipFile zipFile = new ZipFile(File.Open(zipFileName, FileMode.Open)))
            //{
            //string contents;
            //    ZipFile zipFile1 = new(File.Open(zipFileName, FileMode.Open));
            //using (ZipFile zipFile = zipFile1)
            //{
            //    //using (var zipFile = zipFile1)
            //    //{
            //    /*
            //    ZipEntry startPartEntry = zipFile.GetEntry("[Content_Types].xml");
            //    using (StreamReader reader = new StreamReader(zipFile.GetInputStream(startPartEntry)))
            //    {
            //        contents = reader.ReadToEnd();
            //    }
            //    XElement contentTypes = XElement.Parse(contents);
            //    XNamespace xs = contentTypes.GetDefaultNamespace();
            //    XElement newDefExt = new XElement(xs + "Default", new XAttribute("Extension", "sab"), new XAttribute("ContentType", @"application/binary; modeler=Acis; version=18.0.2application/binary; modeler=Acis; version=18.0.2"));
            //    contentTypes.Add(newDefExt);
            //    contentTypes.Save("[Content_Types].xml");
            //    zipFile.BeginUpdate();
            //    zipFile.Add("[Content_Types].xml");
            //    zipFile.CommitUpdate();
            //    File.Delete("[Content_Types].xml");
            //    */
            //    zipFile.BeginUpdate();
            //    try
            //    {
            //        zipFile.Delete("[Content_Types].xml");
            //        zipFile.CommitUpdate();
            //    }
            //    catch { }
            //}
        }
        public static DateTime UpdateLog(DateTime dt, string txt, bool bbl, string tmpPath, string MultithreadNo, string form, System.Windows.Forms.ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            DateTime dtt = System.DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            var ismaindb = "";
            try
            {
                if (pB_ReadDLCs != null && txt != null & txt != "")
                {
                    pB_ReadDLCs.CreateGraphics().Clear(System.Drawing.Color.HotPink);
                    pB_ReadDLCs.CreateGraphics().DrawString(txt.Replace("\n", ""), new System.Drawing.Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                }

                var a = dt - dtt;
                var b = a.TotalSeconds;
                var B = b.ToString();
                var c = double.Parse(B);
                var C = Math.Round(c, 2);
                var D = Math.Abs(C);
                var e = D.ToString();
                var ii = e.PadLeft(4, '0');
                if (form != null && form != "" && rtxt_StatisticsOnReadDLCs != null)
                    rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                if (form == "MainDB") ismaindb = "maindb";

                // Write the string to a file. packid+
                Random randomp = new Random();
                var packid = 0;
                packid = randomp.Next(0, 100000);
                var fn = (logPath == null || !DirectoryExists(logPath) ? (DirectoryExists(tmpPath + "\\0_log") ? tmpPath + "0_log" : AppWD.Replace("DLCManager\\external_tools", "")) : logPath) + "\\" + MultithreadNo + "current_" + ismaindb + "temp" + ".txt";/*MultithreadNo +c("dlcm_Split4Pack")*/
                var zipFile = fn + dtt.ToString().Replace("/", "").Replace(":", "").Substring(0, 8) + ".gz";// "C:\data\myzip.zip";c("dlcm_TempPath") + "\\0_log\\" +

                if (File.Exists(fn))
                    if (!File.Exists(zipFile))
                    {
                        AddFileToZip(zipFile, fn);
                        DeleteFile(fn, false);
                        CreateLog(logPath, fn);
                    }
                    else CreateLog(logPath, fn);
                using (StreamWriter sw = File.AppendText(fn))
                {
                    sw.WriteLine(dtt.ToString() + " - " + ii.ToString() + " - " + txt.ToString());// This text is always added, making the file longer over time if it is not deleted.
                }
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            if (c("dlcm_Debug").ToLower() == "yes" && txt.ToLower().Replace("terror", "").Replace("t-error", "").IndexOf("error") >= 0 && txt.ToLower().IndexOf("<ERROR>") < 0
                && c("dlcm_AdditionalManipul105") == "Yes" && !txt.ToLower().Contains("LogPackingError".ToLower()) && !txt.ToLower().Contains("LogImportingError".ToLower()))
            {
                ErrorWindow frm1 = new ErrorWindow(txt, "", "'Erro' captured throughout the running of the DLC Manager. Chose DEBUG If you wanna do a Debug line by line" +
                    " now, to continue to the block where error was coming from.", true, true, false, "Continue", "Debug", "", false);
                frm1.ShowDialog();
                if (frm1.StopImport)
                    UpdateLog(DateTime.Now, "----", false, c("dlcm_TempPath"), "", "", null, null);
                //if (frm1.IgnoreSong) {; }
                //;
            }
            else if (c("dlcm_Debug").ToLower() == "yes" && txt.ToLower().Replace("terror", "").Replace("t-error", "").IndexOf("erro") >= 0 && c("dlcm_AdditionalManipul112") == "Yes"
                && !txt.ToLower().Contains("LogPackingError".ToLower()) && !txt.ToLower().Contains("LogImportingError".ToLower()))
            {
                ErrorWindow frm1 = new ErrorWindow(txt, "", "'Erro' captured throughout the running of the DLC Manager. Chose DEBUG If you wanna do a Debug line by line" +
                    " now, to continue to the block where error was coming from.", true, true, false, "Continue", "Debug", "", false);
                frm1.ShowDialog();
                if (frm1.StopImport)
                    UpdateLog(DateTime.Now, "----", false, c("dlcm_TempPath"), "", "", null, null);
                //if (frm1.IgnoreSong) {; }
                //;
            }
            return dtt;
        }
        public static string GetTuningFrecv(string xml, bool ValueOrStandardTxt)
        {
            if (xml == null || xml == "") return "";
            DLCPackageData info = null;
            var frecv = "";
            //Song2014 xmlContent = null;
            // LOAD DATA
            try
            {
                //xmlContent = Song2014.LoadFromFile(xml);
                //if (xmlContent == null) return "";
                info = DLCPackageData.LoadFromFolder(Path.GetDirectoryName(xml).Replace("songs\\arr", ""), null);
                foreach (var arg in info.Arrangements)
                {
                    if (arg.SongXml is null) continue;
                    if (xml != arg.SongXml.File.ToString() || (arg.ArrangementName != ArrangementName.Lead && arg.ArrangementName != ArrangementName.Rhythm
                        && arg.ArrangementName != ArrangementName.Rhythm && arg.ArrangementName != ArrangementName.Bass)) continue;
                    if (ValueOrStandardTxt) frecv = arg.TuningPitch.ToString();
                    else frecv = arg.TuningPitch.Equals(440.0) ? "440Hz" : "NonStnd";
                    break;
                }
            }
            catch (Exception ee)
            {
                var timestamp = UpdateLog(DateTime.Now, xml + "-" + ee.Message, true, null, "", "DLCManager", null, null);
            }
            //try
            //{
            //    xmlContent = Song2014.LoadFromFile(xml);
            //    if (xmlContent == null) return "";
            //    if (ValueOrStandardTxt) frecv = xmlContent.ArrangementProperties.StandardTuning.ToString();
            //    else frecv = xmlContent.Arrangements[0].TuningPitch.Equals(440.0) ? "440Hz" : "NonStnd";
            //}
            //catch (Exception ee)
            //{
            //    var timestamp = UpdateLog(DateTime.Now, ee.Message, true, null, "", "DLCManager", null, null);
            //    //continue;
            //}
            return frecv;
        }

        public static string GetSections(string xml)
        {
            if (xml == null || xml == "") return "";
            Song2014 xmlContent = null;
            var Part = "";
            try
            {
                xmlContent = Song2014.LoadFromFile(xml);
                if (xmlContent == null) return "";
                Part = xmlContent.Sections.Count().ToString();
            }
            catch (Exception ee)
            {
                var timestamp = UpdateLog(DateTime.Now, ee.Message, true, null, "", "DLCManager", null, null);
                //continue;
            }
            //if (xml == null || xml == "") return "";
            //var file = File.OpenText(xml);
            //string line; string PArt = "";
            ////3 lines
            //while ((line = file.ReadLine()) != null)
            //{
            //    if (line.Contains("<part>"))
            //    {
            //        PArt = line.Replace("<part>", "").Replace("</part>", "").Trim();
            //        break;
            //    }
            //}
            //file.Close();
            return Part;
        }

        public static string GetPart(string xml)
        {
            if (xml == null || xml == "") return "";
            Song2014 xmlContent = null;
            var Part = "";
            try
            {
                xmlContent = Song2014.LoadFromFile(xml);
                if (xmlContent == null) return "";
                Part = xmlContent.Part.ToString();
            }
            catch (Exception ee)
            {
                var timestamp = UpdateLog(DateTime.Now, ee.Message + " Broken Song Not Imported" + "----", true, null, "", "DLCManager", null, null);
                //continue;
            }
            //if (xml == null || xml == "") return "";
            //var file = File.OpenText(xml);
            //string line; string PArt = "";
            ////3 lines
            //while ((line = file.ReadLine()) != null)
            //{
            //    if (line.Contains("<part>"))
            //    {
            //        PArt = line.Replace("<part>", "").Replace("</part>", "").Trim();
            //        break;
            //    }
            //}
            //file.Close();
            return Part;
        }
        public static string GetMaxDifficulty(string xml, DateTime timestamp, string broken_Path_Import, string pat, string FullPath, string p, string unpackedDir)
        {
            if (xml == null || xml == "") return "";
            Song2014 xmlContent = null;
            var DD = "";
            try
            {
                xmlContent = Song2014.LoadFromFile(xml);
                var manifestFunctions = new ManifestFunctions(RocksmithToolkitLib.GameVersion.RS2014);
                DD = manifestFunctions.GetMaxDifficulty(xmlContent).ToString();
            }
            catch (Exception ee)
            {
                timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported" + "----", true, null, "", "DLCManager", null, null);
                var Pathh = broken_Path_Import + "\\" + pat;
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul30"] == "Yes")
                    CopyMoveFileSafely(FullPath, Pathh, ConfigRepository.Instance()["dlcm_AdditionalManipul75"] == "Yes" ? true : false, p, false);
                //continue;
            }
            return DD;
        }
        public static string GetLastConversionDateTime(string xml, DateTime timestamp, string broken_Path_Import, string pat, string FullPath, string p, string unpackedDir)
        {
            if (xml == null || xml == "") return "";
            Song2014 xmlContent = null;
            var LastConversionDateTime = "";
            try
            {
                xmlContent = Song2014.LoadFromFile(xml);

                LastConversionDateTime = xmlContent.LastConversionDateTime;
                if (LastConversionDateTime.Length > 3)
                {
                    if (LastConversionDateTime.IndexOf("-") == 1) LastConversionDateTime = "0" + LastConversionDateTime;
                    if (LastConversionDateTime.IndexOf("-", 3) == 4) LastConversionDateTime = LastConversionDateTime.Substring(0, 3) + "0" + LastConversionDateTime.Substring(3, ((LastConversionDateTime.Length) - 3));
                    if (LastConversionDateTime.IndexOf(":") == 10) LastConversionDateTime = LastConversionDateTime.Substring(0, 9) + "0" + LastConversionDateTime.Substring(9, LastConversionDateTime.Length - 9);
                }
                //clist[k] = LastConversionDateTime;
            }
            catch (Exception ee)
            {
                timestamp = UpdateLog(timestamp, ee.Message + " Broken Song Not Imported" + "----", true, null, "", "DLCManager", null, null);
                var Pathh = broken_Path_Import + "\\" + pat;
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul30"] == "Yes")
                    CopyMoveFileSafely(FullPath, Pathh, ConfigRepository.Instance()["dlcm_AdditionalManipul75"] == "Yes" ? true : false, p, false);
                //continue;
            }
            //platform.version = RocksmithToolkitLib.GameVersion.RS2014;
            var manifestFunctions = new ManifestFunctions(RocksmithToolkitLib.GameVersion.RS2014);
            //lastconvdate
            var json = Directory.GetFiles(unpackedDir, string.Format("*{0}.json", Path.GetFileNameWithoutExtension(xml)), System.IO.SearchOption.AllDirectories);
            if (json.Length > 0)
            {
                foreach (var fl in json)
                {
                    if (Path.GetFileNameWithoutExtension(fl) != Path.GetFileNameWithoutExtension(xml)) continue;
                    //var o = 0;
                    if (Path.GetFileNameWithoutExtension(fl).ToLower().Contains("bass") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("lead")
                        || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("rhythm") || Path.GetFileNameWithoutExtension(fl).ToLower().Contains("combo"))
                    {
                        var attr = Manifest2014<Attributes2014>.LoadFromFile(fl).Entries.First().Value.First().Value;
                        manifestFunctions.GenerateSectionData(attr, xmlContent);

                        LastConversionDateTime = attr.LastConversionDateTime;
                        if (LastConversionDateTime.Length > 3)
                        {
                            if (LastConversionDateTime.IndexOf("-") == 1) LastConversionDateTime = "0" + LastConversionDateTime;
                            if (LastConversionDateTime.IndexOf("-", 3) == 4) LastConversionDateTime = LastConversionDateTime.Substring(0, 3) + "0" + LastConversionDateTime.Substring(3, ((LastConversionDateTime.Length) - 3));
                            if (LastConversionDateTime.IndexOf(":") == 10) LastConversionDateTime = LastConversionDateTime.Substring(0, 9) + "0" + LastConversionDateTime.Substring(9, LastConversionDateTime.Length - 9);
                        }
                        //if (LastConversionDateTime.Length > 3)
                        //    if (DateTime.ParseExact(LastConversionDateTime, "MM-dd-yy HH:mm", enUS) > DateTime.ParseExact(datemax, "MM-dd-yy HH:mm", enUS))
                        //        datemax = LastConversionDateTime;
                        //for (var nb = 0; nb < attr.Tones.Count; nb++)
                        //{
                        //    if (nb > 0) clist.Add("");
                        //    clist[k + o] = LastConversionDateTime;
                        //    o++;
                        //}
                    }
                }
            }

            return LastConversionDateTime;
        }
        public static void AddTones(DateTime timestamp, string MultithreadNo, string form, System.Windows.Forms.ProgressBar pB_ReadDLCs, List<string> xmlhlist, List<string> jsonhlist, List<string> hlist,
        List<string> dlist, List<string> snghlist, List<string> cxmlhlist, List<string> elist
            , string MaxDD, string norm_path, bool Rebuild, string platformTXT
            // , string unpackedDir, OleDbConnection connection, DLCPackageData info, string CDLC_ID, List<string> clist, int mutit, string Official, SQLiteConnection cnz)
            , string unpackedDir, OleDbConnection connection, DLCPackageData info, string CDLC_ID, List<string> clist, int mutit, string Official, SQLite.SQLiteConnection cnc)
        {
            var n = 0;
            foreach (var tn in info.TonesRS2014)
            {
                if (Official == "Yes")
                {
                    //connection.Close();
                    //connection.Open();
                    var fcmd = "SELECT TOP 1 ID FROM Cache WHERE Identifier=\"" + tn.Name.Substring(0, tn.Name.IndexOf("_")).ToLower() + "\" ORDER BY ID DESC";
                    DataSet dus = new DataSet(); var norec = 0;
                    dus = SelectFromDB("Cache", fcmd, c("dlcm_RocksmithDLCPath"), connection, cnc);
                    norec = dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
                    if (norec == 0)
                        continue;
                    CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                }

                var command = connection.CreateCommand();
                var insertvalues = ""; var tt = 0;
                try
                {
                    if (Rebuild)
                    {
                        command.CommandText = "Update INTO Tones(";
                        command.CommandText += "CDLC_ID = @param1, ";
                        command.CommandText += "Tone_Name = @param2, ";
                        command.CommandText += "Is_Custom = @param3, ";
                        command.CommandText += "SortOrder = @param4, ";
                        command.CommandText += "Volume = @param5, ";
                        command.CommandText += "Keyy = @param6, ";
                        command.CommandText += "NameSeparator = @param7, ";
                        command.CommandText += "ConversionDateTime = @param8, ";
                        command.CommandText += "Official = @param9 ";
                    }
                    else
                    {
                        command.CommandText = "INSERT INTO Tones(";
                        command.CommandText += "CDLC_ID, ";
                        command.CommandText += "Tone_Name, ";
                        command.CommandText += "Is_Custom, ";
                        command.CommandText += "SortOrder, ";
                        command.CommandText += "Volume, ";
                        command.CommandText += "Keyy, ";
                        command.CommandText += "NameSeparator, ";
                        command.CommandText += "ConversionDateTime, ";
                        command.CommandText += "Official ";
                        command.CommandText += ") VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9";
                        command.CommandText += ")";
                    }
                    command.Parameters.AddWithValue("@param1", NullHandler(CDLC_ID));
                    command.Parameters.AddWithValue("@param2", NullHandler(tn.Name));
                    command.Parameters.AddWithValue("@param3", NullHandler(tn.IsCustom));
                    command.Parameters.AddWithValue("@param4", NullHandler(tn.SortOrder));
                    command.Parameters.AddWithValue("@param5", NullHandler(tn.Volume));
                    command.Parameters.AddWithValue("@param6", NullHandler(tn.Key));
                    command.Parameters.AddWithValue("@param7", NullHandler(tn.NameSeparator));
                    command.Parameters.AddWithValue("@param8", (DBNull.Value.ToString()));/*clist[n].ToString() ?? */
                    command.Parameters.AddWithValue("@param9", Official);
                    //EXECUTE SQL/INSERT
                    string tid = "";
                    command.CommandType = CommandType.Text;
                    UpdateDBbyExecuteNonQuery(command, cnb, cnc);
                    //try
                    //{
                    //    connection.Open();
                    //    command.ExecuteNonQuery();

                    //}
                    //catch (Exception ex)
                    //{
                    //    timestamp = UpdateLog(timestamp, "error in tones " + CDLC_ID + " " + tn.Name + ex.Message, true, "", "", "DLCManager", pB_ReadDLCs, null);
                    //    throw;
                    //}
                    //finally
                    //{
                    //    if (connection != null)
                    //    {
                    //        connection.Close();
                    //    }
                    //}
                    tt++;
                    // Get and Store IDENTITY (Primary Key) for further
                    if (c("dlcm_DBFolder").Contains(".db") && ConfigRepository.Instance()["dlcm_AdditionalManipul114"].ToString() == "Yes" && cnc is not null)
                    {
                        var fcmd = "SELECT TOP 1 ID FROM Tones ORDER BY ID DESC";
                        DataSet dus = new DataSet(); var norec = 0;
                        dus = SelectFromDB("Main", fcmd, c("dlcm_DBFolder"), cnb, cnc);
                        norec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
                        if (norec > 0) tid = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                    }
                    else
                    {
                        command.CommandText = "SELECT @@identity";
                        tid = command.ExecuteScalar().ToString();
                    }
                    tt++;
                    var insertcmdd = "Tone_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex, CDLC_ID, Official";
                    insertvalues = ""; insertvalues += tid + ", \"Amp\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Type));
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Category));
                    string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Amp == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"Cabinet\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Type));
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Category));
                    vals = ""; keys = ""; if (tn.GearList.Cabinet != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Cabinet.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Cabinet == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PostPedal1\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Type));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Category));
                    vals = ""; keys = ""; if (tn.GearList.PostPedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PostPedal1 == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PostPedal2\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Type));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Category));
                    vals = ""; keys = ""; if (tn.GearList.PostPedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PostPedal2 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PostPedal3\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Type));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Category));
                    vals = ""; keys = ""; if (tn.GearList.PostPedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PostPedal3 == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PostPedal4\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Type));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Category));
                    vals = ""; keys = ""; if (tn.GearList.PostPedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PostPedal4 == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PrePedal1\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Type));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Category));
                    vals = ""; keys = ""; if (tn.GearList.PrePedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PrePedal1 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PrePedal2\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Type));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Category));
                    vals = ""; keys = ""; if (tn.GearList.PrePedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PrePedal2 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PrePedal3\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Type));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Category));
                    vals = ""; keys = ""; if (tn.GearList.PrePedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PrePedal3 == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"PrePedal4\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Type));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Category));
                    vals = ""; keys = ""; if (tn.GearList.PrePedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Skin));
                    insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.PrePedal4 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"Rack1\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Type));
                    insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Category));
                    vals = ""; keys = ""; if (tn.GearList.Rack1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Rack1 == null ? "0" : */
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"Rack2\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Type));
                    insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Category));
                    vals = ""; keys = ""; if (tn.GearList.Rack2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Rack2 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"Rack3\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Type));
                    insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Category));
                    vals = ""; keys = ""; if (tn.GearList.Rack3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Rack3 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    tt++;
                    insertvalues = ""; insertvalues += tid + ", \"Rack4\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Type));
                    insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Category));
                    vals = ""; keys = ""; if (tn.GearList.Rack4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.PedalKey));
                    insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Skin));
                    insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.SkinIndex));
                    insertvalues += "\", \"" + CDLC_ID + "\", \"" + Official + "\"";/*(tn.GearList.Rack4 == null ? "0" :*/
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, connection, mutit, cnc);
                    n++;
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tt + "-" + tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(CDLC_ID + "Can not open Tones DB connection in Import ! " + "" + "-" + tn.Name + "-" + command.CommandText + ex);
                }
            }
            timestamp = UpdateLog(timestamp, "ToneDB Updated " + info.TonesRS2014.Count, true, "", "", "DLCManager", pB_ReadDLCs, null);

        }
        public static string GetNormPath(string unpackedDir, string txt_TempPath, string dataPath, string platformTXT, DLCPackageData info, string Is_Original, string CDLC_ID, int trackno, string namernd)
        {
            int maxpath = 0; string max_path = "";
            var fil = Directory.GetFiles(unpackedDir, "*.*", System.IO.SearchOption.AllDirectories);
            foreach (var f in fil)
                if (maxpath < f.Length)
                {
                    maxpath = f.Length;
                    max_path = f;
                }

            max_path = max_path.Replace(txt_TempPath + "\\", "");
            max_path = max_path.Replace(max_path.Substring(0, max_path.IndexOf("\\")), "");
            var norm_path = platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + CleanTitle(info.SongInfo.Artist)
                + "_" + info.SongInfo.SongYear + "_" + CleanTitle(info.SongInfo.Album) + "_" + trackno.ToString() + "_" + CleanTitle(info.SongInfo.SongDisplayName) + "_" + namernd;
            norm_path = dataPath + "\\" + CleanPath(norm_path);

            if ((norm_path.Length + max_path.Length) > 250)
            {
                norm_path = dataPath + "\\" + CleanPath(platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + CleanTitle(info.SongInfo.Artist) + "_" + CDLC_ID);
                if ((norm_path.Length + max_path.Length) > 250)
                {
                    norm_path = dataPath + "\\" + CleanPath(platformTXT + "_" + (Is_Original == "Yes" ? "ORIG" : "CDLC") + "_" + CleanTitle(info.SongInfo.Artist).Substring(0, 1) + "_" + CDLC_ID);
                    if ((norm_path.Length + max_path.Length) > 250)
                    {
                        DialogResult result1 = MessageBox.Show(norm_path + "\nPath is too long: " + norm_path.Length, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Question);
                        norm_path = CDLC_ID;
                    }
                }
            }
            return norm_path;
        }
        public static string CleanPath(string path)
        {
            path = path.Replace(".", "_");
            path = path.Replace(" ", "_");
            path = path.Replace("__", "_");
            path = path.Replace("/", "");
            path = path.Replace("!", "");
            path = path.Replace("?", "");
            path = path.Replace("ș", "s");
            path = path.Replace("ț", "t");
            path = path.Replace("â", "i");
            path = path.Replace("î", "i");
            path = path.Replace("ă", "a");
            path = path.Replace("ü", "u");
            path = path.Replace("ö", "o");
            path = path.Replace("è", "e");
            path = path.Replace("é", "e");
            path = path.Replace("à", "a");
            path = path.Replace("\\", "");//.Replace("/", "")
            return path;
        }

        public static void AddSong(DateTime timestamp, string IDD, OleDbConnection cnb, RichTextBox rtxt_StatisticsOnReadDLCs, System.Windows.Forms.ProgressBar pB_ReadDLCs, List<string> xmlhlist, List<string> jsonhlist, List<string> hlist,
        List<string> dlist, List<string> snghlist, List<string> cxmlhlist, List<string> elist, string import_path, string original_FileName, DataSet ds, string unpackedDir, int i, DLCPackageData info
            , string AppIdD, string Guitar, string Combo, string Rhythm, string Bass, string Lead, string Vocalss, string sect1on, string Tones_Custom, string DD, string tkversion, string Is_Original
            , string Has_author, string Tunings, string author, string alt, string art_hash, string audio_hash, string audioPreview_hash, string Bass_Has_DD, string bonus, string Available_Duplicate
            , string Available_Old, string description, string comment, string PluckedType, int trackno, string platformTXT, string Is_MultiTrack, string MultiTrack_Version, string IsLive
            , string CustomsForge_Link, string CustomsForge_Like, string YouTube_Link, string CustomsForge_ReleaseNotes, string PreviewTime, string PreviewLenght, string SongLenght, string IsAcoustic
            , string LiveDetails, int bitrate, int SampleRate, string HasOrig, string SpotifySongID, string SpotifyArtistID, string SpotifyAlbumID, string SpotifyAlbumURL, string bbbroken
            , int Duplic, string ybSAddress, string ybRAddress, string IsSingle, string IsEP, string IsInstrumental, string IsSoundtrack, string ybAddress, string oldAlbumN
            , string audio_changed, string oldArtistN, string oldSongN, int oldYearN, string IsUncensored, string datemax, string IsFullAlbum, string PitchShiftableEsOrDd
            , string IsRemastered, string InTheWorks, string dupli_assesment, int j, string IsCover, string IsDemo, string IsRemix, string HasFeaturing, string IsKaraoke,
            string BasedOn_Youtube, string BasedOn_CF, string BasedOn_Tabs, string ToDos, string ToneDetails, string PackageDetails, string PackingDate, string UpdateVersionDate, string BasedOn_GP,
                    // string Has_Capo, string Has_Showlights, string Has_JVocals, string IsMedley, string IsMultiStrings, SQLiteConnection cnz)
                    string Has_Capo, string Has_Showlights, string Has_JVocals, string IsMedley, string IsMultiStrings, string IsDeluxe, string IsGreatestHits
            , string IsMidi, string IsGameSoundtrack, string IsTVTheme, string IsAmateurCover, string txt_EoFPathText, string txt_YBLinkText, string txt_SpotifyText
            , string IsMetalCover, string IsUkulele, string A440TunningFrecv, SQLite.SQLiteConnection cnc)
        {
            var command = cnb.CreateCommand();
            if (dupli_assesment == "Update")
            {
                //Update MainDB
                timestamp = UpdateLog(timestamp, "Updating / Overriting " + IDD + "-" + j + "-" + "" + "..", true, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                command.CommandText = "UPDATE Main SET ";
                command.CommandText += "Import_Path = @param1, ";
                command.CommandText += "Original_FileName = @param2, ";
                command.CommandText += "Current_FileName = @param3, ";
                command.CommandText += "File_Hash = @param4, ";
                command.CommandText += "Original_File_Hash = @param5, ";
                command.CommandText += "File_Size = @param6, ";
                command.CommandText += "Import_Date = @param7, ";
                command.CommandText += "Folder_Name = @param8, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Song_Title = @param9, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Song_Title_Sort = @param10, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Album = @param11, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Artist = @param12, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Artist_Sort = @param13, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.CommandText += "Album_Year = @param14, ";
                command.CommandText += "Version = @param15, ";
                command.CommandText += "AverageTempo = @param16, ";
                command.CommandText += "Volume = @param17, ";
                command.CommandText += "Preview_Volume = @param18, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "DLC_Name = @param19, ";
                command.CommandText += "DLC_AppID = @param20, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.CommandText += "AlbumArtPath = @param21, ";
                command.CommandText += "AudioPath = @param22, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "audioPreviewPath = @param23, ";
                command.CommandText += "Has_Bass = @param24, ";
                command.CommandText += "Has_Guitar = @param25, ";
                command.CommandText += "Has_Lead = @param26, ";
                command.CommandText += "Has_Rhythm = @param27, ";
                command.CommandText += "Has_Combo = @param28, ";
                command.CommandText += "Has_Vocals = @param29, ";
                command.CommandText += "Has_Sections = @param30, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.CommandText += "Has_Cover = @param31, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Has_Preview = @param32, ";
                command.CommandText += "Has_Custom_Tone = @param33, ";
                command.CommandText += "Has_DD = @param34, ";
                command.CommandText += "Has_Version = @param35, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Has_Author = @param36, ";
                command.CommandText += "Tunning = @param37, ";
                command.CommandText += "Bass_Picking = @param38, ";
                command.CommandText += "DLC = @param39, ";
                command.CommandText += "SignatureType = @param40, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Author = @param41, ";
                command.CommandText += "ToolkitVersion = @param42, ";
                command.CommandText += "Is_Original = @param43, ";
                command.CommandText += "Is_Alternate = @param44, ";
                command.CommandText += "Alternate_Version_No = @param45, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.CommandText += "AlbumArt_Hash = @param46, ";
                command.CommandText += "Audio_Hash = @param47, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "audioPreview_Hash = @param48, ";
                command.CommandText += "Bass_Has_DD = @param49, ";
                command.CommandText += "Has_Bonus_Arrangement = @param50, ";
                command.CommandText += "Available_Duplicate = @param51, ";
                command.CommandText += "Available_Old = @param52, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Description = @param53, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "Comments = @param54, ";
                command.CommandText += "OggPath = @param55, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "OggPreviewPath = @param56, ";
                command.CommandText += "Has_Track_No = @param57, ";
                command.CommandText += "Track_No = @param58, ";
                command.CommandText += "Platform = @param59, ";
                command.CommandText += "Is_Multitrack = @param60, ";
                command.CommandText += "MultiTrack_Version = @param61, ";
                command.CommandText += "YouTube_Link = @param62, ";
                command.CommandText += "CustomsForge_Link = @param63, ";
                command.CommandText += "CustomsForge_Like = @param64, ";
                command.CommandText += "CustomsForge_ReleaseNotes = @param65, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "PreviewTime = @param66, ";
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.CommandText += "PreviewLenght = @param67, ";
                command.CommandText += "Pack = @param68, ";
                command.CommandText += "Song_Lenght = @param69, ";
                command.CommandText += "File_Creation_Date = @param70,";
                command.CommandText += "Is_Live = @param71, ";
                command.CommandText += "Live_Details = @param72, ";
                command.CommandText += "audioBitrate = @param73, ";
                command.CommandText += "audioSampleRate = @param74, ";
                command.CommandText += "Is_Acoustic = @param75, ";
                command.CommandText += "Has_Other_Officials = @param76, ";
                command.CommandText += "Spotify_Song_ID = @param77, ";
                command.CommandText += "Spotify_Artist_ID = @param78, ";
                command.CommandText += "Spotify_Album_ID = @param79, ";
                command.CommandText += "Spotify_Album_URL = @param80, ";
                command.CommandText += "Is_Broken = @param81, ";
                command.CommandText += "Audio_OrigHash = @param82, ";
                command.CommandText += "Audio_OrigPreviewHash = @param83, ";
                command.CommandText += "AlbumArt_OrigHash = @param84, ";
                command.CommandText += "Duplicate_Of = @param85, ";
                command.CommandText += "Youtube_Playthrough = @param86, ";
                command.CommandText += "Is_Single = @param87, ";
                command.CommandText += "Is_EP = @param88, ";
                command.CommandText += "Is_Soundtrack = @param89, ";
                command.CommandText += "Is_Instrumental = @param90, ";
                command.CommandText += "Has_Had_Audio_Changed = @param91, ";
                command.CommandText += "Album_Sort = @param92, ";
                command.CommandText += "Has_Been_Corrected = @param93, ";
                command.CommandText += "Is_Uncensored = @param94, ";
                command.CommandText += "LastConversionDateTime = @param95, ";
                command.CommandText += "Is_FullAlbum = @param96, ";
                command.CommandText += "PitchShiftableEsOrDd = @param97, ";
                command.CommandText += "Import_AuditTrail_ID = @param98, ";
                command.CommandText += "Is_Remastered = @param99,";
                command.CommandText += "IntheWorks = @param100, ";
                command.CommandText += "Is_Cover = @param101, ";
                command.CommandText += "Is_Demo = @param102, ";
                command.CommandText += "Is_Remix = @param103, ";
                command.CommandText += "Is_Karaoke = @param104, ";
                command.CommandText += "Has_Featuring = @param105, ";
                command.CommandText += "BasedOn_Youtube = @param106, ";
                command.CommandText += "BasedOn_CF = @param107, ";
                command.CommandText += "BasedOn_Tabs = @param108, ";
                command.CommandText += "ToDos = @param109, ";
                command.CommandText += "ToneDetails = @param110, ";
                command.CommandText += "PackageDetails = @param111, ";
                command.CommandText += "PackingDate = @param112, ";
                command.CommandText += "UpdateVersionDate = @param113, ";
                command.CommandText += "Has_Capo = @param114, ";
                command.CommandText += "Has_ShowLights = @param115, ";
                command.CommandText += "Has_JVocals = @param116, ";
                command.CommandText += "LyricsLanguage = @param117, ";
                command.CommandText += "Is_Medley = @param118, ";
                command.CommandText += "Is_MultiStrings = @param119, ";
                command.CommandText += "BasedOn_GP = @param120, ";
                command.CommandText += "Album_ArtPathOrig = @param121, ";
                command.CommandText += "Is_Deluxe = @param122, ";
                command.CommandText += "Is_GreatestHits = @param123,";
                command.CommandText += "Is_Midi = @param124, ";
                command.CommandText += "Is_GameSoundtrack = @param125, ";
                command.CommandText += "Is_TVTheme= @param126, ";
                command.CommandText += "Is_AmateurCover = @param127, ";
                command.CommandText += "EoFPath= @param128, ";
                command.CommandText += "Is_MetalCover = @param129, ";
                command.CommandText += "Is_Ukulele = @param130, ";
                command.CommandText += "A440TunningFrecv = @param131 ";
                command.CommandText += " WHERE ID = " + IDD;

                command.Parameters.AddWithValue("@param1", import_path);
                command.Parameters.AddWithValue("@param2", original_FileName);
                command.Parameters.AddWithValue("@param3", original_FileName);
                command.Parameters.AddWithValue("@param4", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                command.Parameters.AddWithValue("@param5", ds.Tables[0].Rows[i].ItemArray[3].ToString());
                command.Parameters.AddWithValue("@param6", ds.Tables[0].Rows[i].ItemArray[4].ToString());
                command.Parameters.AddWithValue("@param7", ds.Tables[0].Rows[i].ItemArray[5].ToString());
                command.Parameters.AddWithValue("@param8", unpackedDir);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param9", info.SongInfo.SongDisplayName);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param10", info.SongInfo.SongDisplayNameSort);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param11", info.SongInfo.Album);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param12", info.SongInfo.Artist);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param13", info.SongInfo.ArtistSort);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.Parameters.AddWithValue("@param14", info.SongInfo.SongYear);
                command.Parameters.AddWithValue("@param15", (info.ToolkitInfo.PackageVersion ?? "1"));
                command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                command.Parameters.AddWithValue("@param17", TruncateExponentials(info.Volume.ToString()));
                command.Parameters.AddWithValue("@param18", info.PreviewVolume != null ? TruncateExponentials(info.PreviewVolume.ToString()) : TruncateExponentials(info.Volume.ToString()));
                //command.Parameters.AddWithValue("@param17", info.Volume);
                //command.Parameters.AddWithValue("@param18", info.PreviewVolume != null ? info.PreviewVolume : info.Volume);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param19", info.Name);
                command.Parameters.AddWithValue("@param20", AppIdD);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param22", info.OggPath);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));
                command.Parameters.AddWithValue("@param24", Bass);
                command.Parameters.AddWithValue("@param25", Guitar);
                command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                command.Parameters.AddWithValue("@param30", sect1on);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes"));
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                command.Parameters.AddWithValue("@param33", Tones_Custom);
                command.Parameters.AddWithValue("@param34", DD);
                command.Parameters.AddWithValue("@param35", ((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param36", Has_author);//((((author != "" && tkversion != "") || author == "Custom Song Creator") && Is_Original == "No") ? "Yes" : "No"));
                command.Parameters.AddWithValue("@param37", Tunings);
                command.Parameters.AddWithValue("@param38", PluckedType);
                command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                command.Parameters.AddWithValue("@param40", info.SignatureType);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param41", author);//
                command.Parameters.AddWithValue("@param42", tkversion);
                command.Parameters.AddWithValue("@param43", Is_Original);
                command.Parameters.AddWithValue("@param44", ((alt == "" || alt == null) ? "No" : "Yes"));
                command.Parameters.AddWithValue("@param45", ((alt == "" || alt == null) ? "" : alt));
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul61"] == "Yes") command.Parameters.AddWithValue("@param46", art_hash);
                command.Parameters.AddWithValue("@param47", audio_hash);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param48", audioPreview_hash);
                command.Parameters.AddWithValue("@param49", Bass_Has_DD ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param50", bonus ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param51", Available_Duplicate ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param52", Available_Old ?? DBNull.Value.ToString());
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param53", description ?? DBNull.Value.ToString());
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param54", comment ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param55", info.OggPath.Replace(".wem", "_fixed.ogg"));//_fixed//_fixed
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param56", (info.OggPreviewPath == null ? DBNull.Value.ToString() : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") : info.OggPreviewPath.Replace(".wem", "_fixed.ogg"))));
                command.Parameters.AddWithValue("@param57", (trackno == 0 ? "No" : "Yes"));
                command.Parameters.AddWithValue("@param58", trackno.ToString("D2"));
                command.Parameters.AddWithValue("@param59", platformTXT);
                command.Parameters.AddWithValue("@param60", Is_MultiTrack);
                command.Parameters.AddWithValue("@param61", MultiTrack_Version);
                command.Parameters.AddWithValue("@param62", ybAddress ?? txt_YBLinkText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param63", CustomsForge_Link);
                command.Parameters.AddWithValue("@param64", CustomsForge_Like);
                command.Parameters.AddWithValue("@param65", CustomsForge_ReleaseNotes);
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param66", PreviewTime ?? DBNull.Value.ToString());
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul60"] == "Yes") command.Parameters.AddWithValue("@param67", PreviewLenght ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param68", ds.Tables[0].Rows[i].ItemArray[6].ToString());
                command.Parameters.AddWithValue("@param69", SongLenght);
                command.Parameters.AddWithValue("@param70", ds.Tables[0].Rows[i].ItemArray[7].ToString());
                command.Parameters.AddWithValue("@param71", IsLive);
                command.Parameters.AddWithValue("@param72", LiveDetails);
                command.Parameters.AddWithValue("@param73", bitrate);
                command.Parameters.AddWithValue("@param74", SampleRate);
                command.Parameters.AddWithValue("@param75", IsAcoustic);
                command.Parameters.AddWithValue("@param76", HasOrig);
                command.Parameters.AddWithValue("@param77", SpotifySongID ?? txt_SpotifyText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param78", SpotifyArtistID ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param79", SpotifyAlbumID ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param80", SpotifyAlbumURL ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param81", bbbroken);
                command.Parameters.AddWithValue("@param82", audio_hash);
                command.Parameters.AddWithValue("@param83", audioPreview_hash);
                command.Parameters.AddWithValue("@param84", art_hash);
                command.Parameters.AddWithValue("@param85", Duplic.ToString());
                command.Parameters.AddWithValue("@param86", ybSAddress == null ? DBNull.Value.ToString() : ybRAddress);
                command.Parameters.AddWithValue("@param87", IsSingle);
                command.Parameters.AddWithValue("@param88", IsEP);
                command.Parameters.AddWithValue("@param89", IsSoundtrack);
                command.Parameters.AddWithValue("@param90", IsInstrumental);
                command.Parameters.AddWithValue("@param91", audio_changed);
                command.Parameters.AddWithValue("@param92", info.SongInfo.AlbumSort ?? info.SongInfo.Album);
                command.Parameters.AddWithValue("@param93", (oldArtistN != info.SongInfo.Artist || oldSongN != info.SongInfo.SongDisplayName
                    || oldAlbumN != info.SongInfo.Album || oldYearN != info.SongInfo.SongYear) ? "Yes" : "No");
                command.Parameters.AddWithValue("@param94", IsUncensored);
                command.Parameters.AddWithValue("@param95", datemax);
                command.Parameters.AddWithValue("@param96", IsFullAlbum);
                command.Parameters.AddWithValue("@param97", PitchShiftableEsOrDd);
                command.Parameters.AddWithValue("@param98", "" ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param99", IsRemastered ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param100", InTheWorks ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param101", IsCover ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param102", IsDemo ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param103", IsRemix ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param104", IsKaraoke ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param105", HasFeaturing ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param106", BasedOn_Youtube ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param107", BasedOn_CF ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param108", BasedOn_Tabs ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param109", ToDos ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param110", ToneDetails ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param111", PackageDetails ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param112", PackingDate ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param113", UpdateVersionDate ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param114", Has_Capo ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param115", Has_Showlights ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param116", Has_JVocals ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param117", Has_JVocals == "Yes" ? "JP" : DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param118", IsMedley ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param119", IsMultiStrings ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param120", BasedOn_GP ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param121", info.AlbumArtPath ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param122", IsDeluxe ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param123", IsGreatestHits ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param124", IsMidi ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param125", IsGameSoundtrack ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param126", IsTVTheme ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param127", IsAmateurCover ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param128", txt_EoFPathText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param129", IsMetalCover ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param130", IsUkulele ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param131", A440TunningFrecv ?? DBNull.Value.ToString());
                command.CommandType = CommandType.Text;
                UpdateDBbyExecuteNonQuery(command, cnb, cnc);
                ////EXECUTE SQL/UPDATE
                //try
                //{
                //    //try { cnb.Close(); cnc.Close(); } catch (Exception ex) {; }
                //    OpenDb();
                //    command.ExecuteNonQuery();
                //    var tst = "end updating ..."; timestamp = UpdateLog(timestamp, tst, true, null, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //}
                //catch (Exception ex)
                //{
                //    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //MessageBox.Show("Can not open Update Main DB connection in Import ! " + DB_Path + "-" + original_FileName + "-" + command.CommandText);
                //}
                //finally
                //{
                //    if (cnb != null) cnb.Close();
                //}
            }

            if (dupli_assesment == "Insert")
            {
                //if alternate add it to the same groups
                if (IDD is not null)
                {
                    DataSet dvs = new DataSet(); dvs = SelectFromDB("Group", "SELECT Groupz,Comments FROM Groups WHERE CDLC_ID=\"" + IDD + "\" AND Type=\"DLC\"", "", cnb, cnc);
                    var noOfRect = GetNoRec(dvs, cnb, cnc);//dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;

                    for (var jf = 0; jf <= noOfRect - 1; jf++)
                    {
                        var grp = dvs.Tables[0].Rows[jf].ItemArray[0].ToString();
                        var indx = dvs.Tables[0].Rows[jf].ItemArray[1].ToString();
                        string insertcmdAA = "CDLC_ID, Profile_Name, Type, Comments, Groupz,Date_Added";
                        var insertAA = "\"" + IDD + "\",\"\",\"DLC\",\"" + indx + "\",\"" + grp + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "\"";
                        InsertIntoDBwValues("Groups", insertcmdAA, insertAA, cnb, 0, cnc);
                    }
                }

                command = cnb.CreateCommand();
                timestamp = UpdateLog(timestamp, "Inserting ", true, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
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
                command.CommandText += "Comments, ";
                command.CommandText += "OggPath, ";
                command.CommandText += "OggPreviewPath, ";
                command.CommandText += "Has_Track_No, ";
                command.CommandText += "Track_No, ";
                command.CommandText += "Platform, ";
                command.CommandText += "Is_Multitrack, ";
                command.CommandText += "MultiTrack_Version, ";
                command.CommandText += "YouTube_Link, ";
                command.CommandText += "CustomsForge_Link, ";
                command.CommandText += "CustomsForge_Like, ";
                command.CommandText += "CustomsForge_ReleaseNotes, ";
                command.CommandText += "PreviewTime, ";
                command.CommandText += "PreviewLenght, ";
                command.CommandText += "Pack, ";
                command.CommandText += "Song_Lenght, ";
                command.CommandText += "File_Creation_Date, ";
                command.CommandText += "Is_Live, ";
                command.CommandText += "Live_Details, ";
                command.CommandText += "audioBitrate, ";
                command.CommandText += "audioSampleRate, ";
                command.CommandText += "Is_Acoustic, ";
                command.CommandText += "Has_Other_Officials, ";
                command.CommandText += "Spotify_Song_ID, ";
                command.CommandText += "Spotify_Artist_ID, ";
                command.CommandText += "Spotify_Album_ID, ";
                command.CommandText += "Spotify_Album_URL, ";
                command.CommandText += "Is_Broken, ";
                command.CommandText += "Audio_OrigHash, ";
                command.CommandText += "Audio_OrigPreviewHash, ";
                command.CommandText += "AlbumArt_OrigHash, ";
                command.CommandText += "Duplicate_Of, ";
                command.CommandText += "Youtube_Playthrough,  ";
                command.CommandText += "Is_Single, ";
                command.CommandText += "Is_EP, ";
                command.CommandText += "Is_Soundtrack, ";
                command.CommandText += "Is_Instrumental, ";
                command.CommandText += "Has_Had_Audio_Changed, ";
                command.CommandText += "Album_Sort, ";
                command.CommandText += "Has_Been_Corrected, ";
                command.CommandText += "Is_Uncensored, ";
                command.CommandText += "LastConversionDateTime, ";
                command.CommandText += "Is_FullAlbum, ";
                command.CommandText += "PitchShiftableEsOrDd, ";
                command.CommandText += "Import_AuditTrail_ID, ";
                command.CommandText += "Is_Remastered, ";
                command.CommandText += "IntheWorks, ";
                command.CommandText += "Is_Cover, ";
                command.CommandText += "Is_Demo, ";
                command.CommandText += "Is_Remix, ";
                command.CommandText += "Is_Karaoke, ";
                command.CommandText += "Has_Featuring,";
                command.CommandText += "BasedOn_Youtube, ";
                command.CommandText += "BasedOn_CF, ";
                command.CommandText += "BasedOn_Tabs, ";
                command.CommandText += "ToDos, ";
                command.CommandText += "ToneDetails, ";
                command.CommandText += "PackageDetails, ";
                command.CommandText += "PackingDate, ";
                command.CommandText += "UpdateVersionDate, ";
                command.CommandText += "Has_Capo, ";
                command.CommandText += "Has_ShowLights, ";
                command.CommandText += "Has_JVocals, ";
                command.CommandText += "LyricsLanguage, ";
                command.CommandText += "Is_Medley, ";
                command.CommandText += "Is_MultiStrings, ";
                command.CommandText += "BasedOn_GP, ";
                command.CommandText += "Album_ArtPathOrig, ";
                command.CommandText += "Is_Deluxe, ";
                command.CommandText += "Is_GreatestHits, ";
                command.CommandText += "Is_Midi, ";
                command.CommandText += "Is_GameSoundtrack, ";
                command.CommandText += "Is_TVTheme, ";
                command.CommandText += "Is_AmateurCover, ";
                command.CommandText += "EoFPath, ";
                command.CommandText += "Is_MetalCover, ";
                command.CommandText += "Is_Ukulele, ";
                command.CommandText += "A440TunningFrecv ";
                command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48,@param49";
                command.CommandText += ",@param50,@param51,@param52,@param53,@param54,@param55,@param56,@param57,@param58,@param59";
                command.CommandText += ",@param60,@param61,@param62,@param63,@param64,@param65,@param66,@param67,@param68,@param69";
                command.CommandText += ",@param70,@param71,@param72,@param73,@param74,@param75,@param76,@param77,@param78,@param79";
                command.CommandText += ",@param80,@param81,@param82,@param83,@param84,@param85,@param86,@param87,@param88,@param89";
                command.CommandText += ",@param90,@param91,@param92,@param93,@param94,@param95,@param96,@param97,@param98,@param99";
                command.CommandText += ",@param100,@param101,@param102,@param103,@param104,@param105,@param106,@param107,@param108,@param109";
                command.CommandText += ",@param110,@param111,@param112,@param113,@param114,@param115,@param116,@param117,@param118,@param119";
                command.CommandText += ",@param120,@param121,@param122,@param123,@param124,@param125,@param126,@param127,@param128";
                command.CommandText += ",@param129,@param130,@param131" + ")";/*,@param129,@param130*/

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
                command.Parameters.AddWithValue("@param15", (info.ToolkitInfo.PackageVersion ?? "1"));
                command.Parameters.AddWithValue("@param16", info.SongInfo.AverageTempo);
                command.Parameters.AddWithValue("@param17", TruncateExponentials(info.Volume.ToString()));
                command.Parameters.AddWithValue("@param18", info.PreviewVolume != null ? TruncateExponentials(info.PreviewVolume.ToString()) : TruncateExponentials(info.Volume.ToString()));
                command.Parameters.AddWithValue("@param19", info.Name);
                command.Parameters.AddWithValue("@param20", AppIdD);
                command.Parameters.AddWithValue("@param21", info.AlbumArtPath ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param22", info.OggPath);
                command.Parameters.AddWithValue("@param23", (info.OggPreviewPath ?? DBNull.Value.ToString()));
                command.Parameters.AddWithValue("@param24", Bass);
                command.Parameters.AddWithValue("@param25", Guitar);
                command.Parameters.AddWithValue("@param26", ((Lead != "") ? Lead : "No"));
                command.Parameters.AddWithValue("@param27", ((Rhythm != "") ? Rhythm : "No"));
                command.Parameters.AddWithValue("@param28", ((Combo != "") ? Combo : "No"));
                command.Parameters.AddWithValue("@param29", ((Vocalss != "") ? Vocalss : "No"));
                command.Parameters.AddWithValue("@param30", sect1on);
                command.Parameters.AddWithValue("@param31", ((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes"));
                command.Parameters.AddWithValue("@param32", ((info.OggPreviewPath != null) ? "Yes" : "No"));
                command.Parameters.AddWithValue("@param33", Tones_Custom);
                command.Parameters.AddWithValue("@param34", DD);
                command.Parameters.AddWithValue("@param35", ((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No"));
                command.Parameters.AddWithValue("@param36", Has_author);
                command.Parameters.AddWithValue("@param37", Tunings);
                command.Parameters.AddWithValue("@param38", PluckedType);
                command.Parameters.AddWithValue("@param39", ((Is_Original == "Yes") ? "ORIG" : "CDLC"));
                command.Parameters.AddWithValue("@param40", info.SignatureType);
                command.Parameters.AddWithValue("@param41", author);
                command.Parameters.AddWithValue("@param42", tkversion);
                command.Parameters.AddWithValue("@param43", Is_Original);
                command.Parameters.AddWithValue("@param44", ((alt == "" || alt == null) ? "No" : "Yes"));
                command.Parameters.AddWithValue("@param45", alt ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param46", art_hash);
                command.Parameters.AddWithValue("@param47", audio_hash);
                command.Parameters.AddWithValue("@param48", audioPreview_hash);
                command.Parameters.AddWithValue("@param49", Bass_Has_DD);
                command.Parameters.AddWithValue("@param50", bonus);
                command.Parameters.AddWithValue("@param51", Available_Duplicate);
                command.Parameters.AddWithValue("@param52", Available_Old);
                command.Parameters.AddWithValue("@param53", description);
                command.Parameters.AddWithValue("@param54", comment);
                command.Parameters.AddWithValue("@param55", info.OggPath.Replace(".wem", "_fixed.ogg"));/*_fixe*/
                command.Parameters.AddWithValue("@param56", (info.OggPreviewPath == null ? DBNull.Value.ToString() : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") : "")));
                command.Parameters.AddWithValue("@param57", (trackno == 0 ? "No" : "Yes"));
                command.Parameters.AddWithValue("@param58", trackno.ToString("D2"));
                command.Parameters.AddWithValue("@param59", platformTXT.ToString());
                command.Parameters.AddWithValue("@param60", Is_MultiTrack);
                command.Parameters.AddWithValue("@param61", MultiTrack_Version);
                command.Parameters.AddWithValue("@param62", ybAddress ?? txt_YBLinkText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param63", CustomsForge_Link);
                command.Parameters.AddWithValue("@param64", CustomsForge_Like);
                command.Parameters.AddWithValue("@param65", CustomsForge_ReleaseNotes);
                command.Parameters.AddWithValue("@param66", PreviewTime ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param67", PreviewLenght ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param68", ds.Tables[0].Rows[i].ItemArray[6]);
                command.Parameters.AddWithValue("@param69", SongLenght);
                command.Parameters.AddWithValue("@param70", ds.Tables[0].Rows[i].ItemArray[7]);
                command.Parameters.AddWithValue("@param71", IsLive ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param72", LiveDetails ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param73", bitrate);
                command.Parameters.AddWithValue("@param74", SampleRate);
                command.Parameters.AddWithValue("@param75", IsAcoustic ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param76", HasOrig ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param77", SpotifySongID ?? txt_SpotifyText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param78", SpotifyArtistID ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param79", SpotifyAlbumID ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param80", SpotifyAlbumURL ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param81", bbbroken);
                command.Parameters.AddWithValue("@param82", audio_hash);
                command.Parameters.AddWithValue("@param83", audioPreview_hash);
                command.Parameters.AddWithValue("@param84", art_hash);
                command.Parameters.AddWithValue("@param85", Duplic.ToString());
                command.Parameters.AddWithValue("@param86", ybSAddress == null ? txt_YBLinkText ?? DBNull.Value.ToString() : ybRAddress);
                command.Parameters.AddWithValue("@param87", IsSingle);
                command.Parameters.AddWithValue("@param88", IsEP);
                command.Parameters.AddWithValue("@param89", IsInstrumental);
                command.Parameters.AddWithValue("@param90", IsSoundtrack);
                command.Parameters.AddWithValue("@param91", audio_changed);
                command.Parameters.AddWithValue("@param92", info.SongInfo.AlbumSort ?? info.SongInfo.Album);
                command.Parameters.AddWithValue("@param93", (oldArtistN != info.SongInfo.Artist || oldSongN != info.SongInfo.SongDisplayName
                    || oldAlbumN != info.SongInfo.Album || oldYearN != info.SongInfo.SongYear) ? "Yes" : "No");
                command.Parameters.AddWithValue("@param94", IsUncensored);
                command.Parameters.AddWithValue("@param95", datemax);
                command.Parameters.AddWithValue("@param96", IsFullAlbum);
                command.Parameters.AddWithValue("@param97", PitchShiftableEsOrDd);
                command.Parameters.AddWithValue("@param98", "");
                command.Parameters.AddWithValue("@param99", IsRemastered);
                command.Parameters.AddWithValue("@param100", InTheWorks);
                command.Parameters.AddWithValue("@param101", IsCover);
                command.Parameters.AddWithValue("@param102", IsDemo);
                command.Parameters.AddWithValue("@param103", IsRemix);
                command.Parameters.AddWithValue("@param104", IsKaraoke);
                command.Parameters.AddWithValue("@param105", HasFeaturing);
                command.Parameters.AddWithValue("@param106", BasedOn_Youtube);
                command.Parameters.AddWithValue("@param107", BasedOn_CF);
                command.Parameters.AddWithValue("@param108", BasedOn_Tabs);
                command.Parameters.AddWithValue("@param109", ToDos);
                command.Parameters.AddWithValue("@param110", ToneDetails);
                command.Parameters.AddWithValue("@param111", PackageDetails);
                command.Parameters.AddWithValue("@param112", PackingDate);
                command.Parameters.AddWithValue("@param113", UpdateVersionDate);
                command.Parameters.AddWithValue("@param114", Has_Capo ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param115", Has_Showlights ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param116", Has_JVocals ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param117", Has_JVocals == "Yes" ? "JP" : DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param118", IsMedley ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param119", IsMultiStrings ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param120", BasedOn_GP ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param121", info.AlbumArtPath ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param122", IsDeluxe ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param123", IsGreatestHits ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param124", IsMidi ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param125", IsGameSoundtrack ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param126", IsTVTheme ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param127", IsAmateurCover ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param128", txt_EoFPathText ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param129", IsMetalCover ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param130", IsUkulele ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param131", A440TunningFrecv ?? DBNull.Value.ToString());
                //EXECUTE SQL/UPDATE

                //var rt = (import_path) + "\",\"" + (original_FileName) + "\",\"" + (original_FileName) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[3])
                //    + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[3]) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[4]) + "\",\"" +
                //    (ds.Tables[0].Rows[i].ItemArray[5]) + "\",\"" + (unpackedDir) + "\",\"" + (info.SongInfo.SongDisplayName) +
                //    "\",\"" + (info.SongInfo.SongDisplayNameSort) + "\",\"" + (info.SongInfo.Album) + "\",\"" + (info.SongInfo.Artist) +
                //    "\",\"" + (info.SongInfo.ArtistSort) + "\",\"" + (info.SongInfo.SongYear) + "\",\"" +
                //    ((info.ToolkitInfo.PackageVersion ?? "1")) + "\",\"" +
                //    (info.SongInfo.AverageTempo) + "\",\"" + (info.Volume) + "\",\"" + (info.PreviewVolume) + "\",\"" + (info.Name) +
                //    "\",\"" + (AppIdD) + "\",\"" + (info.AlbumArtPath ?? DBNull.Value.ToString()) + "\",\"" + (info.OggPath) +
                //    "\",\"" + ((info.OggPreviewPath ?? DBNull.Value.ToString())) + "\",\"" + (Bass) + "\",\"" +
                //    (Guitar) + "\",\"" + (((Lead != "") ? Lead : "No")) + "\",\"" + (((Rhythm != "") ? Rhythm : "No")) + "\",\"" +
                //    (((Combo != "") ? Combo : "No")) + "\",\"" + (((Vocalss != "") ? Vocalss : "No")) + "\",\"" + (sect1on) +
                //    "\",\"" + (((info.AlbumArtPath == "" || info.AlbumArtPath == null) ? "No" : "Yes")) + "\",\"" +
                //    (((info.OggPreviewPath != null) ? "Yes" : "No")) + "\",\"" + (Tones_Custom) + "\",\"" + (DD) + "\",\"" +
                //    (((info.ToolkitInfo.PackageVersion != null && tkversion != "" && Is_Original == "No") ? "Yes" : "No")) +
                //    "\",\"" + (Has_author) + "\",\"" + (Tunings) + "\",\"" + (PluckedType) + "\",\"" + (((Is_Original == "Yes") ? "ORIG" : "CDLC")) +
                //    "\",\"" + (info.SignatureType) + "\",\"" + (author) + "\",\"" + (tkversion) + "\",\"" + (Is_Original) + "\",\"" +
                //    (((alt == "" || alt == null) ? "No" : "Yes")) + "\",\"" + (alt ?? DBNull.Value.ToString()) + "\",\"" + (art_hash) +
                //    "\",\"" + (audio_hash) + "\",\"" + (audioPreview_hash) + "\",\"" + (Bass_Has_DD) + "\",\"" + (bonus) + "\",\"" +
                //    (Available_Duplicate) + "\",\"" + (Available_Old) + "\",\"" + (description) + "\",\"" + (comment) + "\",\"" +
                //    (info.OggPath.Replace(".wem", "_fixed.ogg")) + "\",\"" + ((info.OggPreviewPath == null ? DBNull.Value.ToString()
                //    : (File.Exists(info.OggPreviewPath.Replace(".wem", "_fixed.ogg")) ? info.OggPreviewPath.Replace(".wem", "_fixed.ogg") :
                //    info.OggPreviewPath.Replace(".wem", "_fixed.ogg")))) + "\",\"" + ((trackno == 0 ? "No" : "Yes")) + "\",\"" + (trackno.ToString()) +
                //    "\",\"" + (platformTXT.ToString()) + "\",\"" + (Is_MultiTrack) + "\",\"" + (MultiTrack_Version) + "\",\"" + (YouTube_Link) +
                //    "\",\"" + (CustomsForge_Link) + "\",\"" + (CustomsForge_Like) + "\",\"" + (CustomsForge_ReleaseNotes) + "\",\"" +
                //    (PreviewTime ?? DBNull.Value.ToString()) + "\",\"" + (PreviewLenght ?? DBNull.Value.ToString()) + "\",\"" +
                //    (ds.Tables[0].Rows[i].ItemArray[6]) + "\",\"" + (SongLenght) + "\",\"" + (ds.Tables[0].Rows[i].ItemArray[7]) +
                //    "\",\"" + (IsLive ?? DBNull.Value.ToString()) + "\",\"" +
                //    (LiveDetails ?? DBNull.Value.ToString()) + "\",\"" + (IsAcoustic ?? DBNull.Value.ToString()) +
                //    "\"";
                command.CommandType = CommandType.Text;
                var prm = "\"" + command.Parameters[0].Value;
                prm += "\","+command.Parameters[1].Value + "\", \"" + command.Parameters[2].Value + "\", \"" + command.Parameters[3].Value + "\", \"" + command.Parameters[4].Value + "\", \"" + command.Parameters[5].Value + "\", \"" + command.Parameters[6].Value + "\", \"" + command.Parameters[7].Value + "\", \"" + command.Parameters[8].Value + "\", \"" + command.Parameters[9].Value +
"\", \"" + command.Parameters[10].Value + "\", \"" + command.Parameters[11].Value + "\", \"" + command.Parameters[12].Value + "\", \"" + command.Parameters[13].Value + "\", \"" + command.Parameters[14].Value + "\", \"" + command.Parameters[15].Value + "\", \"" + command.Parameters[16].Value + "\", \"" + command.Parameters[17].Value + "\", \"" + command.Parameters[18].Value + "\", \"" + command.Parameters[19].Value +
"\", \"" + command.Parameters[20].Value + "\", \"" + command.Parameters[21].Value + "\", \"" + command.Parameters[22].Value + "\", \"" + command.Parameters[23].Value + "\", \"" + command.Parameters[24].Value + "\", \"" + command.Parameters[25].Value + "\", \"" + command.Parameters[26].Value + "\", \"" + command.Parameters[27].Value + "\", \"" + command.Parameters[28].Value + "\", \"" + command.Parameters[29].Value +
"\", \"" + command.Parameters[30].Value + "\", \"" + command.Parameters[31].Value + "\", \"" + command.Parameters[32].Value + "\", \"" + command.Parameters[33].Value + "\", \"" + command.Parameters[34].Value + "\", \"" + command.Parameters[35].Value + "\", \"" + command.Parameters[36].Value + "\", \"" + command.Parameters[37].Value + "\", \"" + command.Parameters[38].Value + "\", \"" + command.Parameters[39].Value +
"\", \"" + command.Parameters[40].Value + "\", \"" + command.Parameters[41].Value + "\", \"" + command.Parameters[42].Value + "\", \"" + command.Parameters[43].Value + "\", \"" + command.Parameters[44].Value + "\", \"" + command.Parameters[45].Value + "\", \"" + command.Parameters[46].Value + "\", \"" + command.Parameters[47].Value + "\", \"" + command.Parameters[48].Value + "\", \"" + command.Parameters[49].Value +
"\", \"" + command.Parameters[50].Value + "\", \"" + command.Parameters[51].Value + "\", \"" + command.Parameters[52].Value + "\", \"" + command.Parameters[53].Value + "\", \"" + command.Parameters[54].Value + "\", \"" + command.Parameters[55].Value + "\", \"" + command.Parameters[56].Value + "\", \"" + command.Parameters[57].Value + "\", \"" + command.Parameters[58].Value + "\", \"" + command.Parameters[59].Value +
"\", \"" + command.Parameters[60].Value + "\", \"" + command.Parameters[61].Value + "\", \"" + command.Parameters[62].Value + "\", \"" + command.Parameters[63].Value + "\", \"" + command.Parameters[64].Value + "\", \"" + command.Parameters[65].Value + "\", \"" + command.Parameters[66].Value + "\", \"" + command.Parameters[67].Value + "\", \"" + command.Parameters[68].Value + "\", \"" + command.Parameters[69].Value +
"\", \"" + command.Parameters[70].Value + "\", \"" + command.Parameters[71].Value + "\", \"" + command.Parameters[72].Value + "\", \"" + command.Parameters[73].Value + "\", \"" + command.Parameters[74].Value + "\", \"" + command.Parameters[75].Value + "\", \"" + command.Parameters[76].Value + "\", \"" + command.Parameters[77].Value + "\", \"" + command.Parameters[78].Value + "\", \"" + command.Parameters[79].Value +
"\", \"" + command.Parameters[80].Value + "\", \"" + command.Parameters[81].Value + "\", \"" + command.Parameters[82].Value + "\", \"" + command.Parameters[83].Value + "\", \"" + command.Parameters[84].Value + "\", \"" + command.Parameters[85].Value + "\", \"" + command.Parameters[86].Value + "\", \"" + command.Parameters[87].Value + "\", \"" + command.Parameters[88].Value + "\", \"" + command.Parameters[89].Value +
"\", \"" + command.Parameters[90].Value + "\", \"" + command.Parameters[91].Value + "\", \"" + command.Parameters[92].Value + "\", \"" + command.Parameters[93].Value + "\", \"" + command.Parameters[94].Value + "\", \"" + command.Parameters[95].Value + "\", \"" + command.Parameters[96].Value + "\", \"" + command.Parameters[97].Value + "\", \"" + command.Parameters[98].Value + "\", \"" + command.Parameters[99].Value +
"\", \"" + command.Parameters[100].Value + "\", \"" + command.Parameters[101].Value + "\", \"" + command.Parameters[102].Value + "\", \"" + command.Parameters[103].Value + "\", \"" + command.Parameters[104].Value + "\", \"" + command.Parameters[105].Value + "\", \"" + command.Parameters[106].Value + "\", \"" + command.Parameters[107].Value + "\", \"" + command.Parameters[108].Value + "\", \"" + command.Parameters[109].Value +
"\", \"" + command.Parameters[110].Value + "\", \"" + command.Parameters[111].Value + "\", \"" + command.Parameters[112].Value + "\", \"" + command.Parameters[113].Value + "\", \"" + command.Parameters[114].Value + "\", \"" + command.Parameters[115].Value + "\", \"" + command.Parameters[116].Value + "\", \"" + command.Parameters[117].Value + "\", \"" + command.Parameters[118].Value + "\", \"" + command.Parameters[119].Value +
"\", \"" + command.Parameters[120].Value + "\", \"" + command.Parameters[121].Value + "\", \"" + command.Parameters[122].Value + "\", \"" + command.Parameters[123].Value + "\", \"" + command.Parameters[124].Value + "\", \"" + command.Parameters[125].Value + "\", \"" + command.Parameters[126].Value + "\", \"" + command.Parameters[127].Value + "\", \"" + command.Parameters[128].Value +
"\", \"" + command.Parameters[129].Value + "\", \"" + command.Parameters[130].Value + "\"";//, \"" + command.Parameters[131].Value+ "\"";
                UpdateDBbyExecuteNonQuery(command, cnb, cnc);

                ////EXECUTE SQL/INSERT
                //try
                //{
                //    //try { cnb.Close(); cnc.Close(); } catch (Exception ex) {; }
                //    OpenDb();
                //    command.ExecuteNonQuery();
                //}
                //catch (Exception ex)
                //{
                //    timestamp = UpdateLog(timestamp, "error at update " + ex + "\n" + rt, true, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                //    //throw;
                //}
                //finally
                //{
                //    if (cnb != null) cnb.Close();
                //}
                //If No version found then defaulted to 1
                //TO DO If default album cover then mark it as suck !?
                //If no version found must by Rocksmith Original or DLC

                timestamp = UpdateLog(timestamp, "Records inserted in Main= " + (i + 1), true, "", "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            }
        }

        public static void AddArrangements(DateTime timestamp, string MultithreadNo, string form, System.Windows.Forms.ProgressBar pB_ReadDLCs, List<string> xmlhlist, List<string> jsonhlist, List<string> hlist,
            List<string> dlist, List<string> snghlist, List<string> cxmlhlist, List<string> elist, string ybLAddress, string ybBAddress, string ybRAddress, string ybCAddress, string MaxDD, string norm_path, bool Rebuild, string platformTXT
                    //, string unpackedDir, OleDbConnection connection, DLCPackageData info, string CDLC_ID, string Official, SQLiteConnection cnz)
                    , string unpackedDir, OleDbConnection connection, DLCPackageData info, string CDLC_ID, string Official, SQLite.SQLiteConnection cnc)
        {
            int n = 0;
            foreach (var arg in info.Arrangements)
            {
                if (Official == "Yes")
                {
                    //connection.Close();
                    //connection.Open();
                    var fcmd = "SELECT TOP 1 ID FROM Cache WHERE Identifier=\""
                        + Path.GetFileNameWithoutExtension(arg.SongXml.File).Replace("_" + arg.ArrangementName.ToString().ToLower() + "1", "")
                        .Replace("_" + arg.ArrangementName.ToString().ToLower() + "2", "")
                        .Replace("_" + arg.ArrangementName.ToString().ToLower() + "3", "").Replace("_" + arg.ArrangementName.ToString().ToLower(), "")
                        + "\" ORDER BY ID DESC";
                    DataSet dus = new DataSet(); var norec = 0;
                    dus = SelectFromDB("Cache", fcmd, c("dlcm_RocksmithDLCPath"), connection, cnc);
                    norec = GetNoRec(dus, cnb, cnc);//dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
                    if (norec == 0)
                        continue;
                    CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                var command = connection.CreateCommand();
                try
                {
                    var mss = arg.SongXml.File.ToString();
                    int poss = 0;

                    var StartTime = "";
                    StartTime = GetTrackStartTime(arg.SongXml.File, arg.RouteMask.ToString(), arg.ArrangementType.ToString(), false, -1);

                    if (mss.Length > 0)
                    {
                        poss = mss.ToString().LastIndexOf("\\") + 1;

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul36"] == "Yes") //37. Keep the Uncompressed Songs superorganized                                
                        {
                            arg.SongXml.File = norm_path + "\\EOF\\" + mss.Substring(poss);
                            arg.SongFile.File = norm_path + "\\Toolkit\\" + (mss.Substring(poss)).Replace(".xml", ".json");
                        }
                        else
                        {
                            arg.SongXml.File = norm_path + (platformTXT == "XBox360" ? "\\Root" : "") + "\\songs\\arr\\" + mss.Substring(poss);
                            arg.SongFile.File = arg.SongXml.File.Replace(".xml", ".json").Replace("\\songs\\arr", "\\" + calc_path(Directory.GetFiles(unpackedDir, "*.json", System.IO.SearchOption.AllDirectories)[0]));
                        }
                    }

                    if (Rebuild)
                    {
                        command.CommandText = "UPDATE Arrangements SET ";
                        command.CommandText += "CDLC_ID = @param1, ";
                        command.CommandText += "Arrangement_Name = @param2, ";
                        command.CommandText += "Tunning = @param3, ";
                        command.CommandText += "JSONFilePath = @param4, ";
                        command.CommandText += "SNGFileName = @param5, ";
                        command.CommandText += "SNGFileLLID = @param6, ";
                        command.CommandText += "SNGFileUUID = @param7, ";
                        command.CommandText += "XMLFilePath = @param8, ";
                        command.CommandText += "XMLFileName = @param9, ";
                        command.CommandText += "XMLFileLLID = @param10, ";
                        command.CommandText += "XMLFileUUID = @param11, ";
                        command.CommandText += "ArrangementSort = @param12, ";
                        command.CommandText += "TuningPitch = @param13, ";
                        command.CommandText += "ScrollSpeed = @param14, ";
                        command.CommandText += "Bonus = @param15, ";
                        command.CommandText += "ToneBase = @param16, ";
                        command.CommandText += "ToneMultiplayer = @param17, ";
                        command.CommandText += "ToneA = @param18, ";
                        command.CommandText += "ToneB = @param19, ";
                        command.CommandText += "ToneC = @param20, ";
                        command.CommandText += "ToneD = @param21, ";
                        command.CommandText += "Idd = @param22, ";
                        command.CommandText += "MasterId = @param23, ";
                        command.CommandText += "ArrangementType = @param24, ";
                        command.CommandText += "String0 = @param25, ";
                        command.CommandText += "String1 = @param26, ";
                        command.CommandText += "String2 = @param27, ";
                        command.CommandText += "String3 = @param28, ";
                        command.CommandText += "String4 = @param29, ";
                        command.CommandText += "String5 = @param30, ";
                        command.CommandText += "PluckedType = @param31, ";
                        command.CommandText += "RouteMask = @param32, ";
                        command.CommandText += "XMLFile_Hash = @param33, ";
                        command.CommandText += "SNGFileHash = @param34, ";
                        command.CommandText += "ConversionDateTime = @param35, ";
                        command.CommandText += "Has_Sections = @param36, ";
                        command.CommandText += "Start_Time = @param37, ";
                        command.CommandText += "Json_Hash = @param38, ";
                        command.CommandText += "CleanedXML_Hash = @param39, ";
                        command.CommandText += "Part = @param40, ";
                        command.CommandText += "PlaythroughYBLink = @param41,";
                        command.CommandText += "MaxDifficulty = @param42,";
                        command.CommandText += "OrigSongTrack = @param43, ";
                        command.CommandText += "Official = @param44, ";
                        command.CommandText += "NoSections = @param45, ";
                        command.CommandText += "CapoFret = @param46 ";
                        //command.CommandText += "Favorite = @param41,";
                        //command.CommandText += "Broken = @param42";
                    }
                    else
                    {
                        command.CommandText = "INSERT INTO Arrangements(";
                        command.CommandText += "CDLC_ID, ";
                        command.CommandText += "Arrangement_Name, ";
                        command.CommandText += "Tunning, ";
                        command.CommandText += "JSONFilePath, ";
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
                        command.CommandText += "RouteMask, ";
                        command.CommandText += "XMLFile_Hash, ";
                        command.CommandText += "SNGFileHash, ";
                        command.CommandText += "ConversionDateTime, ";
                        command.CommandText += "Has_Sections, ";
                        command.CommandText += "Start_Time, ";
                        command.CommandText += "Json_Hash, ";
                        command.CommandText += "CleanedXML_Hash, ";
                        command.CommandText += "Part, ";
                        command.CommandText += "PlaythroughYBLink, ";
                        command.CommandText += "MaxDifficulty, ";/*,*/
                        command.CommandText += "OrigSongTrack, ";
                        command.CommandText += "Official,";
                        command.CommandText += "NoSections,";
                        command.CommandText += "CapoFret";
                        command.CommandText += ") VALUES (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9";
                        command.CommandText += ",@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19";
                        command.CommandText += ",@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29";
                        command.CommandText += ",@param30,@param31,@param32,@param33,@param34,@param35,@param36,@param37,@param38,@param39";
                        command.CommandText += ",@param40,@param41,@param42,@param43,@param44,@param45,@param46)";/**/
                    }
                    command.Parameters.AddWithValue("@param1", CDLC_ID);
                    command.Parameters.AddWithValue("@param2", arg.ArrangementName.ToString());
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
                    command.Parameters.AddWithValue("@param33", (xmlhlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param34", (jsonhlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param35", (hlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param36", dlist[n] == "" || dlist[n] == "No" ? "No" : "Yes");
                    command.Parameters.AddWithValue("@param37", (StartTime ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param38", (snghlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param39", (cxmlhlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param40", (string.IsNullOrEmpty(elist[n]) ? "1" : elist[n].ToString()));
                    command.Parameters.AddWithValue("@param41", (arg.ArrangementType.ToString() == "Lead" ? ybLAddress : (arg.ArrangementType.ToString() == "Bass" ? ybBAddress : (arg.ArrangementType.ToString() == "Rhythm" ? ybRAddress : (arg.ArrangementType.ToString() == "Combo" ? ybCAddress : ""))) ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param42", MaxDD ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param43", "Yes".ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param44", Official);
                    command.Parameters.AddWithValue("@param45", (dlist[n].ToString() ?? DBNull.Value.ToString()));
                    command.Parameters.AddWithValue("@param46", arg.CapoFret.ToString() ?? DBNull.Value.ToString());
                    n++;

                    command.CommandType = CommandType.Text;
                    UpdateDBbyExecuteNonQuery(command, cnb, cnc);
                    ////EXECUTE SQL/INSERT
                    //try
                    //{
                    //    connection.Open();
                    //    command.ExecuteNonQuery();
                    //}
                    //catch (Exception ex)
                    //{
                    //    timestamp = UpdateLog(timestamp, "error at insert " + command.CommandText + "\n" + arg.ArrangementName + " " + arg.RouteMask.ToString() + ex.Message.ToString(), true, "", "", "DLCManager", pB_ReadDLCs, null);
                    //    throw;
                    //}
                    //finally
                    //{
                    //    if (connection != null) connection.Close();
                    //}
                }
                catch (Exception ex)
                {
                    var tsst = "Error at updatee..." + ex.Message; timestamp = UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                }
            }
            //
            timestamp = UpdateLog(timestamp, "Arrangements Updated " + info.Arrangements.Count, true, "", "", "DLCManager", pB_ReadDLCs, null);
        }

        //public static DateTime UpdateLogs(DateTime dt, string txt, bool bbl, string logPath, string tmpPath, string MultithreadNo, string form, ProgressBar pB_ReadDLCs)
        //{
        //    pB_ReadDLCs.Value += 1;
        //    DateTime dtt = System.DateTime.Now;
        //    var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');
        //    if (form == "DLCManager")
        //    {
        //        pB_ReadDLCs.Value += 1;
        //        pB_ReadDLCs.CreateGraphics().DrawString("-" + txt + "---------------", new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        //    }
        //    // Write the string to a file. packid+
        //    Random randomp = new Random();
        //    var packid = 0;
        //    packid = randomp.Next(0, 100000);
        //    var fn = (logPath == null || !DirectoryExists(logPath) ? tmpPath + "\\0_log" : logPath) + "\\" + "current_temp" + MultithreadNo + ".txt";
        //    // This text is always added, making the file longer over timev
        //    // if it is not deleted.
        //    if (File.Exists(fn))
        //    {
        //        using (StreamWriter sw = File.AppendText(fn))
        //        {
        //            sw.WriteLine(dtt + " - " + ii + " - " + txt);
        //        }
        //    }
        //    pB_ReadDLCs.Value += 1;
        //    return dtt;
        //}

        public static Platform TargetPlatform { get; set; }

        //public static string GetMax(string tab, string field, OleDbConnection cnb, SQLiteConnection cnz)
        public static string GetMax(string tab, string field, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            DataSet dms = new DataSet(); dms = SelectFromDB(tab, "SELECT max(val(" + field + ")) as ID FROM " + tab, null, cnb, cnc);
            //if (dms.Tables.Count > 0)
            //{
            if (GetNoRec(dms, cnb, cnc) > 0) return (float.Parse((dms.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? "0" : dms.Tables[0].Rows[0].ItemArray[0].ToString())) + 1).ToString();
            else return "0";//dms.Tables[0].Rows.Count > 0
            //}
            //else return "0";
        }

        static public string Add2Pack(string multithreadname, string form, string platfrm, bool chbx_Replace, bool chbx_ReplaceEnabled, string txt_RemotePath,
                    //  string source, string dest, OleDbConnection cnb, System.IO.FileInfo fi, string ID, string DLC_Name, string chbx_Format, string pack, string ftped, bool coppy, SQLiteConnection cnz)
                    string source, string dest, OleDbConnection cnb, System.IO.FileInfo fi, string ID, string DLC_Name, string chbx_Format, string pack, string ftped, bool coppy, SQLite.SQLiteConnection cnc)
        {
            var FileHash = GetHash(source);//Generating the HASH code

            DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb, cnc);

            var norec = 0;
            norec = GetNoRec(dfs, cnb, cnc);//dfs.Tables.Count == 0 ? 0 : dfs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                var sourcedir = source != "" && source != null ? source.Replace(Path.GetFileName(source), "") : "";
                string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack, FTPed";
                var insertA = "\"" + dest + "\",\"" + (sourcedir != null && sourcedir != "" ? sourcedir.Remove(sourcedir.Length - 1) : "") + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Now.ToString("yyyyMMdd HHmmssfff")
                + "\",\"" + FileHash + "\",\"" + (fi != null ? fi.Length : "") + "\"," + (ID != "" && source != null ? ID : 0) + ",\"" + DLC_Name + "\",\"" + chbx_Format + "\",\"" + pack + "\"" +
                    ",\"" + (ftped.Contains("Truely") ? "Yes" : "No") + "\"";

                InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0, cnc);
            }

            ///Update pack id
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Pack = \"" + pack + "\" WHERE ID=" + ID + ";", cnb, cnc);

            ///copy mac&pc
            var copyftp = "Not";
            if (platfrm != "_ps3" && coppy)
                try
                {
                    DataSet dgr = new DataSet(); dgr = UpdateDB("Main", "Update Main Set Remote_path = \"" + dest + "\" WHERE ID=" + ID + ";", cnb, cnc);
                    if (txt_RemotePath != "" && txt_RemotePath != null) if (chbx_Replace && File.Exists(txt_RemotePath) && !File.Exists(txt_RemotePath.Replace(platfrm + ".psarc", ".old")))
                            DeleteCOPYedSongs(txt_RemotePath, txt_RemotePath.Replace(platfrm + ".psarc", ".old"), cnb, ID, platfrm, cnc);
                    File.Copy(@source, @dest, true);
                    copyftp = "true";
                    var timestamp = UpdateLog(DateTime.Now, "copy _p.psarc in DLC folder" + @dest, true, "", multithreadname, form, null, null);
                    DataSet dcs = new DataSet(); dcs = SelectFromDB("Pack_AuditTrail", "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb, cnc);
                    DataSet dvr = new DataSet(); dvr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set FTPed = \"Yes\" WHERE ID=" + dcs.Tables[0].Rows[0][0].ToString() + ";", cnb, cnc);
                }
                catch (Exception ex)
                {
                    var tgst = "Erro @copy after pack..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                }
            return copyftp;
        }

        static public void HANPackagePreparation()
        {
            var TrueGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game\\") + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP"));
            var TrueTEmpFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game");
            var TrueTempGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\tmp\\" + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")));

            //clean game Directory in true
            CleanFolder(TrueTEmpFldr, "", false, true, "", "DLCManager", null, null);

            DeleteDirectory(TrueGameFldr, false);

            //copy game template in True
            CopyFolder(TrueTempGameFldr, TrueGameFldr);
        }

        static public void HANPackage()
        {
            var TrueGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game\\") + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP"));
            var TrueTEmpFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game");
            var TrueTempGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\tmp\\" + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")));

            // package
            var startInfo = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45", "repacker.exe"),
                WorkingDirectory = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45"),
                UseShellExecute = false,
                CreateNoWindow = false
            };
            using (var DDC = new Process())
            {
                MessageBox.Show("Manually package, please:\n1. Deactivate Patch & Resign (P&R)\n2. Fast Repack (1)\n", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DDC.StartInfo = startInfo;
                DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
            }
            MessageBox.Show("Done with manually packaging?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //copy to to re-signed
            var tz = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\pkg",
               "UP0001-_" + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")).Substring(33, 9) + "00-RS001PACK0000003-A0111-V0100.pkg");
            var trz = Path.Combine(AppWD, "PS3xploit-resigner-master\\input\\pkgs",
               "UP0001-_" + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")).Substring(33, 9) + "00-RS001PACK0000003-A0111-V0100.pkg");

            if (File.Exists(tz)) File.Copy(tz, trz, true);
            else return;

            //reassign
            var startInfo2 = new ProcessStartInfo
            {
                FileName = Path.Combine(AppWD, "PS3xploit-resigner-master", "resign_windows.bat"),
                WorkingDirectory = Path.Combine(AppWD, "PS3xploit-resigner-master"),
                UseShellExecute = false,
                CreateNoWindow = false
            };

            //if (File.Exists(t))
            using (var DDC = new Process())
            {
                DDC.StartInfo = startInfo2;
                DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                MessageBox.Show("Resigned?", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //copy to server
            //packagelist.pkg done by the resigner
            var srs = trz.Replace("\\input\\", "\\output\\").Replace(".pkg", ".pkg_signed.pkg");
            var dstn = ConfigRepository.Instance()["dlcm_PKG_Linker"] + "\\" + (
                ConfigRepository.Instance()["dlcm_MainDBFormat"].IndexOf("EU") >= 0 ? "UP0001-BLES01862_00-RS001PACK0000003-A0111-V0100.pkg" :
                "UP0001-BLUS31182_00-RS001PACK0000003-A0111-V0100.pkg");
            File.Copy(srs, dstn, true);
            MessageBox.Show("Copied to PKG_Linker_V2.0 Server? you can also copy it manually now by USB", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public void fixMissingTempArtfiles(DLCPackageData data)
        {
            if (data.ArtFiles.Count != 0)
            {
                if (!File.Exists(data.ArtFiles[0].destinationFile)) File.Copy(data.ArtFiles[0].sourceFile, data.ArtFiles[0].destinationFile);
                if (!File.Exists(data.ArtFiles[1].destinationFile)) File.Copy(data.ArtFiles[1].sourceFile, data.ArtFiles[1].destinationFile);
                if (!File.Exists(data.ArtFiles[2].destinationFile)) File.Copy(data.ArtFiles[2].sourceFile, data.ArtFiles[2].destinationFile);
            }
        }
        static public string TruncateExponentials(string exp)
        {
            if (exp.ToLower().IndexOf("e-") > 0)
                exp = exp.Substring(0, exp.ToLower().IndexOf("e-"));
            if (exp.ToLower().IndexOf("e+") > 0)
                exp = exp.Substring(0, exp.ToLower().IndexOf("e+"));
            return exp;
        }

        public static string GetTrackStartTime(string SongXml, string MaskRoute, string ArrangementType, bool lastt, double sections)
        {
            Song2014 xmlContent = null;
            Vocals xmlVocals = null;
            var startt = "0";
            if (MaskRoute == "Rhythm" || MaskRoute == "Lead" || MaskRoute == "Bass")
            {
                try
                {
                    xmlContent = Song2014.LoadFromFile(SongXml);
                    if (xmlContent is not null)
                        if (xmlContent.Levels is not null)
                            for (var i = 0; i < xmlContent.Levels.Count(); i++)
                            {
                                var st = double.Parse(startt);
                                //string fc = "0"; //string fn = "0";
                                if (xmlContent.Levels[i].Chords.Count() > 0)

                                    for (var j = 0; j < xmlContent.Levels[i].Chords.Count(); j++)
                                    {
                                        var cc = xmlContent.Levels[i].Chords.Count() > 0 || startt == "0" ? double.Parse(xmlContent.Levels[i].Chords[j].Time.ToString()) : 0;
                                        //var cn = xmlContent.Levels[i].Notes.Count() > 0 || startt == "0" ? double.Parse(fn) : 0;
                                        if (lastt) //last note
                                        {
                                            if (sections >= 0) if (sections < cc) break;
                                            if (double.Parse(startt) < cc) startt = cc.ToString();

                                            ////    else if (st < cn) startt = cn.ToString();
                                        }
                                        else
                                        {
                                            if (sections >= 0) if (sections > cc) continue;
                                            if ((double.Parse(startt) > cc || double.Parse(startt) == 0) && cc != 0) startt = cc.ToString();
                                            //else if (st > cn || st == 0) startt = cn.ToString();

                                        }
                                    }
                                for (var j = 0; j < xmlContent.Levels[i].Notes.Count(); j++)
                                {
                                    //var cc = xmlContent.Levels[i].Chords.Count() > 0 || startt == "0" ? double.Parse(fc) : 0;
                                    var cn = xmlContent.Levels[i].Notes.Count() > 0 || startt == "0" ? double.Parse(xmlContent.Levels[i].Notes[j].Time.ToString()) : 0;
                                    if (lastt) //last note
                                    {
                                        //if (st < cc) startt = cc.ToString();
                                        //else 
                                        if (sections >= 0) if (sections < cn) break;
                                        if (double.Parse(startt) < cn) startt = cn.ToString();
                                    }
                                    else
                                    {
                                        if (sections >= 0) if (sections > cn) continue;
                                        //if ((st > cc || st == 0) && cc != 0) startt = cc.ToString();
                                        //else
                                        if (double.Parse(startt) > cn || double.Parse(startt) == 0) startt = cn.ToString();
                                    }
                                }
                            }
                }
                catch (Exception ex)
                {
                    var tsst = "No Starting time...so trying to default..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    if (lastt) startt = xmlContent.Sections[0].StartTime.ToString();
                    else { tsst = "Error @endtimevocals..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }
            }
            if (lastt && startt == "0") startt = xmlContent.Sections[0].StartTime.ToString();
            if (ArrangementType is not null)
                if (ArrangementType.ToLower().Contains("vocal"))/*&& !(xmlVocals is null)*/
                {
                    // if (xmlVocals.Count > 0)
                    try
                    {
                        xmlVocals = Vocals.LoadFromFile(SongXml);
                        startt = xmlVocals.Vocal[0].Time.ToString();
                    }
                    catch (Exception ex) { var tsst = "Error @starttimevocals..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                }
            return startt;
        }

        //public static void cleanlyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLiteConnection cnz)
        public static void cleanlyrics(string SongID, OleDbConnection cnb, bool arrangoff, SQLite.SQLiteConnection cnc)
        {
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + SongID + GetArrOfficSQLTxt(arrangoff), "", cnb, cnc);
            var noOfRec = GetNoRec(dus, cnb, cnc);//dus.Tables[0].Rows.Count;
            var XMLFilePath = "";
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                if (ArrangementType == "Vocal") XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
            }

            Vocals xmlContent = null; var j = 0;
            if (XMLFilePath != "")
            {
                try
                {
                    File.Copy(XMLFilePath, XMLFilePath.Replace(".xml", "9.xml"), true);
                    xmlContent = Vocals.LoadFromFile(XMLFilePath.Replace(".xml", "9.xml"));
                    if (xmlContent is null)
                    {
                        UpdateLog(DateTime.Now, "Erro reading vocals so restoring", false, c("dlcm_TempPath"), "", "", null, null);
                        File.Copy(XMLFilePath.Replace(".xml", ".xml.old"), XMLFilePath, true);
                        File.Copy(XMLFilePath, XMLFilePath.Replace(".xml", "9.xml"), true);
                        xmlContent = Vocals.LoadFromFile(XMLFilePath.Replace(".xml", "9.xml"));
                        if (xmlContent is null)
                        {
                            UpdateLog(DateTime.Now, "Error reading vocals", false, c("dlcm_TempPath"), "", "", null, null);
                            return;
                        }
                    }
                    for (var i = 0; i < xmlContent.Vocal.Length; i++)
                    {
                        if (xmlContent.Vocal[i].Lyric == "")
                        {
                            ;
                        }
                        else if (i > 0)
                        {
                            if (xmlContent.Vocal[i].Time > xmlContent.Vocal[i - 1].Time || xmlContent.Vocal[i - 1].Lyric == "")
                            {
                                xmlContent.Vocal[j].Lyric = xmlContent.Vocal[i].Lyric.Trim();
                                xmlContent.Vocal[j].Length = xmlContent.Vocal[i].Length;
                                xmlContent.Vocal[j].Time = xmlContent.Vocal[i].Time;
                                j++;
                            }
                        }
                        else
                        {
                            xmlContent.Vocal[j].Lyric = xmlContent.Vocal[i].Lyric.Trim();
                            xmlContent.Vocal[j].Length = xmlContent.Vocal[i].Length;
                            xmlContent.Vocal[j].Time = xmlContent.Vocal[i].Time;
                            j++;
                        }

                    }

                    for (var i = 0; i < xmlContent.Vocal.Length; i++)
                    {
                        xmlContent.Vocal[i].Time = (float)Math.Round(xmlContent.Vocal[i].Time, 3);
                    }

                    for (var i = j; i < xmlContent.Vocal.Length; i++)
                    {
                        xmlContent.Vocal[i].Lyric = "";
                        xmlContent.Vocal[i].Length = (float)0.1;
                        xmlContent.Vocal[i].Time = (float)Math.Round(xmlContent.Vocal[j - 1].Time + xmlContent.Vocal[j - 1].Length + (float)(0.15 * (i - j)), 3);
                    }

                    using (var stream = File.Open(XMLFilePath, FileMode.Create))
                        xmlContent.Serialize(stream);
                    DeleteFile(XMLFilePath.Replace(".xml", "9.xml"), true);
                }
                catch (Exception ex) { var tsst = "Error @cleanlyrics..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

                //Remove end empty lines
                //AND count no of lines
                var info = File.OpenText(XMLFilePath);
                string line;
                //Removes empty end lines or lines with late timing
                var nolines = 0;
                using (StreamWriter sw = File.CreateText(XMLFilePath + ".newvcl"))
                {
                    while ((line = info.ReadLine()) != null)
                    {
                        if (!(line.Contains("lyric=\"\"") || line.Contains("lyric = \"\"") || line.Contains("lyric= \"\"")))
                        {
                            sw.WriteLine(line);
                            if (line.Contains("<vocal ")) nolines++;
                        }
                    }
                }
                info.Close();
                File.Copy(XMLFilePath + ".newvcl", XMLFilePath, true);
                DeleteFile(XMLFilePath + ".newvcl", false);

                //add count of lines<vocals count= "244" >
                var info2 = File.OpenText(XMLFilePath);
                using (StreamWriter sx = File.CreateText(XMLFilePath + ".newvcl"))
                {
                    while ((line = info2.ReadLine()) != null)
                    {
                        if (line.Contains("<vocals")) sx.WriteLine("<vocals count = \"" + nolines + "\">");
                        else sx.WriteLine(line);
                    }
                }
                info2.Close();
                File.Copy(XMLFilePath + ".newvcl", XMLFilePath, true);
                DeleteFile(XMLFilePath + ".newvcl", false);
            }
            return;
        }

        public static string GetHashCleanXML(string filename)
        {
            var r = "";
            if (filename == "") return "";
            if (!File.Exists(filename)) return r;
            try
            {
                File.Copy(filename, filename + ".newvcl", true);
                var info = File.OpenText(filename);
                string line;
                using (StreamWriter sw = File.CreateText(filename + ".newvcl"))
                {
                    while ((line = info.ReadLine()) != null)
                    {
                        if (!(line.Contains("<!--"))) sw.WriteLine(line);
                    }
                }
                info.Close();
                //File.Copy(filename + ".newvcl", filename, true);
                r = GetHash(filename + ".newvcl");
                DeleteFile(filename + ".newvcl", true);
            }
            catch (Exception ex)
            {
                var timestamp = UpdateLog(DateTime.Now, " Error at clening xml" + ex, true, c("dlcm_TempPath"), "", "MainDB", null, null);
            }
            return r;
        }

        public static void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
            //    pB_ReadDLCs.Value = e.ProgressPercentage;
            //else
            //    pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;

            //ShowCurrentOperation(e.UserState as string);
            //if (e.ProgressPercentage==100) e.ca
        }
        public static void ShowCurrentOperation(string message)
        {
            //currentOperationLabel.Text = message;
            //currentOperationLabel.Refresh();
        }

        public static void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(e.Result == null))
                switch (e.Result.ToString())
                {

                    case "generate":
                        var message = "Package was generated.";
                        if (errorsFound.Length > 0)
                            message = string.Format("Package was generated with errors! See below: {0}(1}", Environment.NewLine, errorsFound);
                        message += string.Format("{0}You want to open the folder in which the package was generated?{0}", Environment.NewLine);
                        if (MessageBox.Show(message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(Path.GetDirectoryName("-"));
                        }
                        break;
                    case "error":
                        var message2 = string.Format("Package generation failed. See below: {0}{1}{0}", Environment.NewLine, errorsFound);
                        MessageBox.Show(message2, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
        }

        //not used (anymore?)
        public static string FixWEMwDiffName(string OggPreviewPath, string Folder_Name, System.DateTime timestamp, string tsst, string logPath, string tmpPath, string multithreadname, string windw)
        {

            if (File.Exists(OggPreviewPath.Replace("_fixed.ogg", ".wem")) && File.Exists(OggPreviewPath.Replace("_fixed.ogg", "_fixed_preview.wem")))
            {
                try
                {
                    //DeleteFile(OggPreviewPath.Replace("_fixed.ogg", ".wem"), false);
                    tsst = "Fix _preview.OGG having a diff name than _preview.wem after oggged ..." +
                        OggPreviewPath.Replace("_fixed_preview.ogg", ".wem") + "-" + OggPreviewPath.Replace("_fixed.ogg", ".wem");
                    UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                    File.Copy(OggPreviewPath.Replace("_fixed.ogg", "_fixed_preview.wem"), OggPreviewPath.Replace("_fixed.ogg", ".wem"), true);
                    DeleteFileIfExisting(OggPreviewPath.Replace(".ogg", ".wav"));
                    DeleteFileIfExisting(OggPreviewPath.Replace(".ogg", "_preview.wem"));
                    //DeleteFile(OggPreviewPath.Replace(".ogg", ".wem"), false);
                }
                catch (Exception ee)
                {
                    tsst = "Failed Fix _preview.OGG having a diff name than _preview.wem after oggged ..." +
                       OggPreviewPath.Replace("_fixed_preview.ogg", ".wem") + "--" + OggPreviewPath.Replace("_fixed.ogg", ".wem");
                    UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                    Console.WriteLine(ee.Message);
                }
            }
            else
            if (File.Exists(OggPreviewPath.Replace(".ogg", ".wem")) && !File.Exists(OggPreviewPath.Replace("preview_fixed.ogg", "preview.wem")))
            {
                try
                {
                    //DeleteFile(OggPreviewPath.Replace("_fixed.ogg", ".wem"), false);
                    tsst = "Fix _preview.OGG having a diff name than _preview.wem after oggged ..." +
                        OggPreviewPath.Replace(".ogg", ".wem") + "->" + OggPreviewPath.Replace("preview_fixed.ogg", "preview.wem");
                    UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                    File.Copy(OggPreviewPath.Replace(".ogg", ".wem"), OggPreviewPath.Replace("preview_fixed.ogg", "preview.wem"), true);
                    DeleteFileIfExisting(OggPreviewPath.Replace(".ogg", ".wav"));
                    DeleteFileIfExisting(OggPreviewPath.Replace(".ogg", "_preview.wem"));
                    //DeleteFile(OggPreviewPath.Replace(".ogg", ".wem"), false);
                }
                catch (Exception ee)
                {
                    tsst = "Failed Fix _preview.OGG having a diff name than _preview.wem after oggged ..." +
                       OggPreviewPath.Replace(".ogg", ".wem") + "-->" + OggPreviewPath.Replace("preview_fixed.ogg", "preview.wem");
                    UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                    Console.WriteLine(ee.Message);
                }
            }

            //var previewN = OggPreviewPath == null ? null : ((File.Exists(OggPreviewPath.ToString())) ? OggPreviewPath.ToString().Replace(".wem", "_fixed.ogg") : null);
            ////if (!File.Exists(previewN))
            ////{
            //    foreach (string preview_name in Directory.GetFiles(Folder_Name, "*_preview.wem", System.IO.SearchOption.AllDirectories))
            //    {
            //        foreach (string file_name in Directory.GetFiles(Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories))
            //        {
            //            if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
            //            {
            //                var tl = previewN;
            //                var hg = preview_name;
            //                previewN = preview_name.Replace(".wem", "_fixed.ogg");
            //                if (!File.Exists(previewN))
            //                {
            //                    try
            //                    {
            //                        tsst = "Fix _preview.OGG having a diff name than _preview.wem after oggged ..." + Path.GetFileName(file_name) + "-" + Path.GetFileName(previewN); UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
            //                        File.Copy(previewN, file_name, true);
            //                        DeleteFile(file_name, false);
            //                    }
            //                    catch (Exception ee)
            //                    {
            //                        timestamp = UpdateLog(timestamp, "FAILED1 FixOggwDiffName" + ee.Message + "----" + file_name + "\n -" + previewN + "\n -" + file_name + ".ogg", true, "", "", windw, null, null);
            //                        Console.WriteLine(ee.Message);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return OggPreviewPath;//.Replace("_fixed.ogg", ".wem");
        }

        //public static void FixAudioIssues(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw, SQLiteConnection cnz)
        public static void FixAudioIssues(string cmd, OleDbConnection cnb, string AppWD, System.Windows.Forms.ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw, SQLite.SQLiteConnection cnc)
        {

            BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
            bwRFixAudio.DoWork += new DoWorkEventHandler(FixBitrate);
            bwRFixAudio.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRFixAudio.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            if (cancel)
            { if (bwRFixAudio.WorkerSupportsCancellation == true) bwRFixAudio.CancelAsync(); }// Cancel the asynchronous operation.
            else
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd, "", cnb, cnc);
                var noOfRec = GetNoRec(dhs, cnb, cnc);//dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
                for (var i = 0; i <= noOfRec - 1; i++)
                {
                    if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = i; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }
                    //if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                    var timestamp = UpdateLog(DateTime.Now, "\nAudiofixin" + i + "/" + (noOfRec - 1) + " song: " + dhs.Tables[0].Rows[i].ItemArray[1].ToString(), true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var ID = dhs.Tables[0].Rows[i].ItemArray[0].ToString();
                    var AudioPath = dhs.Tables[0].Rows[i].ItemArray[1].ToString();
                    float bitrate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[2].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                    float SampleRate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[3].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                    var audioPreviewPath = dhs.Tables[0].Rows[i].ItemArray[4].ToString();
                    var oggPath = dhs.Tables[0].Rows[i].ItemArray[5].ToString();
                    var oggPreviewPath = dhs.Tables[0].Rows[i].ItemArray[6].ToString();

                    //var tst = "AudioFixing: " + i + "/" + noOfRec + " " + AudioPath;
                    //timestamp = UpdateLog(DateTime.Now, tst, true, c("dlcm_TempPath"), "", windw, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var args = cmd.Replace(";", "") + ";" + AudioPath + ";" + bitrate + ";" + SampleRate + ";" + ID + ";" + audioPreviewPath
                        + ";" + oggPath + ";" + oggPreviewPath + ";" + i + ";" + windw;
                    bwRFixAudio.RunWorkerAsync(args);
                    do
                        System.Windows.Forms.Application.DoEvents();
                    while (bwRFixAudio.IsBusy);//keep singlethread as toolkit not multithread abled
                }
            }
        }

        //public static int FixMissingPreview(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw, SQLiteConnection cnz)
        public static int FixMissingPreview(string cmd, OleDbConnection cnb, string AppWD, System.Windows.Forms.ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw, SQLite.SQLiteConnection cnc)
        {
            var noOfRec = 0;
            BackgroundWorker bwFixA = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwFixA.DoWork += new DoWorkEventHandler(FixPreview);
            bwFixA.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwFixA.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwFixA.WorkerReportsProgress = true;
            if (cancel)
            { if (bwFixA.WorkerSupportsCancellation == true) bwFixA.CancelAsync(); }// Cancel the asynchronous operation.
            else
            {
                DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb, cnc);
                noOfRec = GetNoRec(dhxs, cnb, cnc);//dhxs.Tables.Count == 0 ? 0 : dhxs.Tables[0].Rows.Count;
                if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }

                for (var j = 0; j <= noOfRec - 1; j++)
                {
                    if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                    var timestamp = UpdateLog(DateTime.Now, "\nPreviewfixin " + j + "/" + (noOfRec - 1) + " song: " + Path.GetDirectoryName(dhxs.Tables[0].Rows[j].ItemArray[5].ToString()), true, null, "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var ID = dhxs.Tables[0].Rows[j].ItemArray[0].ToString();
                    var AudioPath = dhxs.Tables[0].Rows[j].ItemArray[1].ToString();
                    var bitrate = dhxs.Tables[0].Rows[j].ItemArray[2];
                    var SampleRate = dhxs.Tables[0].Rows[j].ItemArray[3];
                    var OggPreviewPath = dhxs.Tables[0].Rows[j].ItemArray[4].ToString();
                    var Folder_Name = dhxs.Tables[0].Rows[j].ItemArray[5].ToString();
                    var OggPath = dhxs.Tables[0].Rows[j].ItemArray[6].ToString();
                    var audioPreviewPath = dhxs.Tables[0].Rows[j].ItemArray[7].ToString();



                    var args = OggPath + ";" + AppWD + ";" + OggPreviewPath + ";" + cmd + ";" + Folder_Name + ";" + ID + ";" + j + ";" + windw + ";" + AudioPath + ";" + audioPreviewPath;
                    bwFixA.RunWorkerAsync(args);
                    do
                        System.Windows.Forms.Application.DoEvents();
                    while (bwFixA.IsBusy);//keep singlethread as toolkit not multithread abled

                }
                dhxs.Dispose();
            }
            return noOfRec;
        }

        public static void CheckJava()
        {
            if (!RijndaelEncryptor.IsJavaInstalled())
            {
                ErrorWindow frm2 = new ErrorWindow("If you want to covert to PS3 in DLCManager please download&Install Java" +
                    " (64bit if windows is for 64b https://www.java.com/en/download/manual.jsp )(chose OpenJDK 21.0.2 for ARM architecture use https://docs.microsoft.com/en-us/java/openjdk/download)" + Environment.NewLine + "A restart is required" + Environment.NewLine,
                    "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false, true, "", "", "", false);
                frm2.ShowDialog();
            }
        }
        //public static System.Windows.Forms.ComboBox GenerateFilterList(System.Windows.Forms.ComboBox cbx_Groups, OleDbConnection cnb, SQLiteConnection cnz)
        public static System.Windows.Forms.ComboBox GenerateFilterList(System.Windows.Forms.ComboBox cbx_Groups, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {
            //Get group and norder index
            //var n = 0;
            //var SearchCmd = "SELECT DisplayGroup FROM Groups u WHERE Type=\"Filter\" GROUP BY DisplayGroup ASC";
            //DataSet dv = new DataSet(); dv = SelectFromDB("Groups", SearchCmd, "", cnb, cnc);
            //if (dv.Tables.Count > 0) n = dv.Tables[0].Rows.Count;

            //SELECT all Params for current Profile

            var SearchCmd = "SELECT Type, Comments, DisplayName, DisplayGroup, DisplayPosition, Date_Added, Groupz FROM Groups u WHERE Type=\"Filter\" ORDER BY DisplayGroup, DisplayName ASC";
            DataSet dsz1 = new DataSet(); dsz1 = SelectFromDB("Groups", SearchCmd, "", cnb, cnc);
            var noOfRec = GetNoRec(dsz1, cnb, cnc);//dif (dsz1.Tables.Count > 0) noOfRec = dsz1.Tables[0].Rows.Count;

            //clear PArams
            cbx_Groups.DataSource = null;
            for (int i = cbx_Groups.Items.Count - 1; i >= 0; --i)
                cbx_Groups.Items.RemoveAt(i);

            ////Add Group categories
            //var DisplayGroup = "";
            //for (int j = 0; j < noOfRec; j++)
            //{
            //    DisplayGroup = dsz1.Tables[0].Rows[j][3].ToString();
            //    var DisplayPosition = dsz1.Tables[0].Rows[j][4].ToString();
            //    if (DisplayPosition.Length == 1) DisplayPosition = "0" + DisplayPosition;
            //    dsz1.Tables[0].Rows[j][5] = GiveOrder(dv, n, DisplayGroup) + DisplayPosition;
            //}

            //OrderList of Params based on Group order and then Item in the group order
            var tmp = "";
            for (int l = 0; l < noOfRec; l++)
                for (int m = l + 1; m < noOfRec; m++)
                {
                    if (dsz1.Tables[0].Rows[m][4].ToString().ToInt32() < dsz1.Tables[0].Rows[l][4].ToString().ToInt32())
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

            // Loads Groups in chbx_AllGroups Filter box cmb_Filter //Create Groups list Dropbox
            var norec = 0;
            DataSet dsn = new DataSet(); dsn = SelectFromDB("Groups", "SELECT DISTINCT Groupz,Comments FROM Groups WHERE Type =\"DLC\" ORDER BY Comments DESC;", "", cnb, cnc);
            norec = GetNoRec(dsn, cnb, cnc);//ddsn.Tables.Count < 1 ? 0 : dsn.Tables[0].Rows.Count;
            if (norec > 0 && c("dlcm_AdditionalManipul107") == "Yes")
            {
                cbx_Groups.Items.Add("----------Groups----------");
                for (int j = 0; j < norec; j++)
                {
                    //if (!dsn.Tables[0].Rows[j][0].ToString().Contains("Group Top " + c("dlcm_maxsongsinweekly") + "(weekly)") && dsn.Tables[0].Rows[j][0].ToString().Contains("(weekly)"))
                    //{
                    //    var cmd = "UPDATE Groups SET Groupz=\"" + "Group Top " + c("dlcm_maxsongsinweekly") + "(weekly)" + "\" WHERE Groupz=\"" + dsn.Tables[0].Rows[j][0].ToString() + "\"";
                    //    //UpdateDB("Groups", cmd, cnb, cnc); continue;
                    //}
                    if (!dsn.Tables[0].Rows[j][0].ToString().Contains("(weekly)"))/*Group Top " + c("dlcm_maxsongsinweekly") +*/
                        cbx_Groups.Items.Add("Group " + dsn.Tables[0].Rows[j][0].ToString());//add items!cbx_Groups.Items[j].ToString()
                }
                if (c("dlcm_maxsongsinweekly").ToInt32() > 0 && norec > 0)
                    cbx_Groups.Items.Add("Group Top " + c("dlcm_maxsongsinweekly") + "(weekly)");/*"Group " + */
            }

            //add items
            var DisplayGroup = "";
            //var z = 0;
            for (int k = 0; k < noOfRec; k++)
            {
                var Type = dsz1.Tables[0].Rows[k][0].ToString();
                var Comments = dsz1.Tables[0].Rows[k][1].ToString();
                var DisplayPosition = dsz1.Tables[0].Rows[k][4].ToString();
                var Groups = dsz1.Tables[0].Rows[k][6].ToString();

                var DisplayName = dsz1.Tables[0].Rows[k][2].ToString();
                if (DisplayName == "") continue;
                if (DisplayGroup != dsz1.Tables[0].Rows[k][3].ToString())
                    cbx_Groups.Items.Add("----------" + dsz1.Tables[0].Rows[k][3].ToString() + "----------");
                //{
                //cbx_Groups.SetItemCheckState(z, CheckState.Indeterminate);
                //    z++;
                //}
                cbx_Groups.Items.Add(DisplayName);
                //cbx_Groups.SetItemCheckState(z, Groupz.ToLower() == "no" ? CheckState.Unchecked : CheckState.Checked);.Replace("dlcm_AdditionalManipul", "")+ " {" + Comments + "}"

                DisplayGroup = dsz1.Tables[0].Rows[k][3].ToString();
                //z++;
            }

            // AddPacks in Filter box cmb_Filter
            norec = 0;
            DataSet dmn = new DataSet(); dmn = SelectFromDB("Main", "SELECT DISTINCT Split4Pack FROM Main ", "", cnb, cnc);//WHERE 1=1" + GetArrOfficSQLTxt(arrangoff)
            norec = GetNoRec(dmn, cnb, cnc);//ddmn.Tables.Count < 1 ? 0 : dmn.Tables[0].Rows.Count;
            if (norec > 0)
                //{
                //cbx_Groups.Items.Add("----------Packs----------");
                for (int j = 0; j < norec; j++)
                    cbx_Groups.Items.Add("Pack " + dmn.Tables[0].Rows[j][0].ToString());//add items
                                                                                        //}

            // Loads Tunnings in Filter box cmb_Filter
            norec = 0;
            DataSet dbn = new DataSet(); dbn = SelectFromDB("Arrangements", "SELECT DISTINCT Tunning FROM Arrangements ", "", cnb, cnc);//WHERE 1=1" + GetArrOfficSQLTxt(arrangoff)
            norec = GetNoRec(dbn, cnb, cnc);//ddbn.Tables.Count < 1 ? 0 : dbn.Tables[0].Rows.Count;
            if (norec > 0 && c("dlcm_AdditionalManipul108") == "Yes")
            {
                cbx_Groups.Items.Add("----------Tunings----------");
                for (int j = 0; j < norec; j++)
                    cbx_Groups.Items.Add("Tuning " + dbn.Tables[0].Rows[j][0].ToString());//add items
            }
            // Loads Capo in Filter box cmb_Filter
            norec = 0;
            DataSet djn = new DataSet(); djn = SelectFromDB("Arrangements", "SELECT DISTINCT VAL(CapoFret) as CapoFret FROM Arrangements WHERE CapoFret<>\"\" AND CapoFret<>\"0\"", "", cnb, cnc);//WHERE 1=1" + GetArrOfficSQLTxt(arrangoff)
            norec = GetNoRec(djn, cnb, cnc);//ddjn.Tables.Count < 1 ? 0 : djn.Tables[0].Rows.Count;
            if (norec > 0 && c("dlcm_AdditionalManipul110") == "Yes")
            {
                cbx_Groups.Items.Add("----------Capos----------");
                for (int j = 0; j < norec; j++)
                    cbx_Groups.Items.Add("Capo " + Math.Round(double.Parse(djn.Tables[0].Rows[j][0].ToString())));//add items
            }

            return cbx_Groups;
        }

        public static System.Windows.Forms.ListBox GenerateFilterListBox(System.Windows.Forms.ListBox lbGroups, System.Windows.Forms.ComboBox cbGroups, OleDbConnection cnb, SQLite.SQLiteConnection cnc)
        {

            //clear PArams
            lbGroups.DataSource = null;
            for (int i = lbGroups.Items.Count - 1; i >= 0; --i)
                lbGroups.Items.RemoveAt(i);

            for (int i = 0; i < cbGroups.Items.Count - 1; ++i)
                lbGroups.Items.Add(cbGroups.Items[i]);


            return lbGroups;
        }

        public static int GiveOrder(DataSet d, int n, string s)
        {
            for (int j = 0; j < n; j++)
                if (d.Tables[0].Rows[j][1].ToString() == s) return j;
            return 0;
        }

        public static string CopyFolder(string copy_dir, string destination_dir)
        {
            var er = "";
            if (!DirectoryExists(copy_dir)) return "Folder to copy doesnt exist";

            if (!DirectoryExists(destination_dir))
                try
                {
                    Directory.CreateDirectory(destination_dir);
                }
                catch (Exception r)
                {
                    er = "erro at create folder folder creation";
                    var timestamp = UpdateLog(DateTime.Now, er + r, true, c("dlcm_TempPath"), "", "", null, null);
                }

            foreach (string dir in Directory.GetDirectories(copy_dir + "\\", "*", System.IO.SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(destination_dir + dir.Substring(copy_dir.Length));
                }
                catch (Exception r)
                {
                    er = "erro at create folder folder creation";
                    var timestamp = UpdateLog(DateTime.Now, er + r, true, c("dlcm_TempPath"), "", "", null, null);
                }
            }

            foreach (string file_name in Directory.GetFiles(copy_dir, "*.*", System.IO.SearchOption.AllDirectories))
            {
                try
                {
                    var tt = (destination_dir + file_name.Replace(copy_dir, ""));/*.Replace("\\\\", "\\")*/
                    File.Copy(file_name, tt, true);
                }
                catch (Exception d)
                {
                    er += "erro at copy folder copy file";
                    var timestamp = UpdateLog(DateTime.Now, er + d + copy_dir + destination_dir, true, c("dlcm_TempPath"), "", "", null, null);
                }
            }
            return er;
        }

        //public async Task<string> YoutubeRun(MainDBfields SongRecord, int i, OleDbConnection cnb, string windw, bool arrangoff, SQLiteConnection cnz)
        public async Task<string> YoutubeRun(MainDBfields SongRecord, int i, OleDbConnection cnb, string windw, bool arrangoff, SQLite.SQLiteConnection cnc)
        {
            var ybAddress = SongRecord.YouTube_Link; //original song
            var ybSAddress = SongRecord.Youtube_Playthrough; //generic playthrough
            var ybLAddress = "-"; //Lead
            var ybBAddress = "-"; //Bass
            var ybRAddress = "-"; //Rhythm
            var ybCAddress = "-"; //Combo

            var scmd = "SELECT PlaythroughYBLink, RouteMask, Bonus FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + GetArrOfficSQLTxt(arrangoff);
            DataSet dnss = new DataSet(); dnss = SelectFromDB("Arrangements", scmd, "", cnb, cnc);
            var norecs = GetNoRec(dnss, cnb, cnc);//dnss.Tables.Count == 0 ? 0 : dnss.Tables[0].Rows.Count;
            if (norecs > 0) for (int j = 0; j < norecs; j++)
                    if (dnss.Tables[0].Rows[j][0].ToString() != "" && dnss.Tables[0].Rows[j][0].ToString() != null)
                        if (dnss.Tables[0].Rows[j][1].ToString() == "Bass") ybBAddress = dnss.Tables[0].Rows[j][0].ToString();
                        else if (dnss.Tables[0].Rows[j][1].ToString() == "Lead") ybLAddress = dnss.Tables[0].Rows[j][0].ToString();
                        else if (dnss.Tables[0].Rows[j][1].ToString() == "Rhythm") ybRAddress = dnss.Tables[0].Rows[j][0].ToString();
                        else if (dnss.Tables[0].Rows[j][1].ToString() == "Combo") ybCAddress = dnss.Tables[0].Rows[j][0].ToString();

            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = c("dlcm_YoutubeAPI"),
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                if (SongRecord.Has_Lead == "Yes" && (ybLAddress == "" || ybLAddress == "-"))
                {
                    ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "Lead", false);
                    ybLAddress = ybLAddress != "" && ybLAddress != "-" ? ybLAddress : ybAddress.Split(';')[0];
                    ybSAddress = ybSAddress != "" && ybSAddress != "-" ? ybSAddress : ybAddress.Split(';')[1] != "-" ? ybAddress.Split(';')[1] : "-";
                }
                if (SongRecord.Has_Bass == "Yes" && (ybBAddress == "" || ybBAddress == "-"))
                {
                    ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "Bass", false);
                    ybBAddress = ybBAddress != "" && ybBAddress != "-" ? ybBAddress : ybAddress.Split(';')[0];
                    ybSAddress = ybSAddress != "" && ybSAddress != "-" ? ybSAddress : ybAddress.Split(';')[1] != "-" ? ybAddress.Split(';')[1] : "-";
                }
                if (SongRecord.Has_Rhythm == "Yes" && (ybRAddress == "" || ybRAddress == "-"))
                {
                    ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "Rhythm", false);
                    ybRAddress = ybRAddress != "" && ybRAddress != "-" ? ybRAddress : ybAddress.Split(';')[0];
                    ybSAddress = ybSAddress != "" && ybSAddress != "-" ? ybSAddress : ybAddress.Split(';')[1] != "-" ? ybAddress.Split(';')[1] : "-";
                }
                if (SongRecord.Has_Combo == "Yes" && (ybCAddress == "" || ybCAddress == "-"))
                {
                    ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "Combo", false);
                    ybCAddress = ybCAddress != "" && ybCAddress != "-" ? ybCAddress : ybAddress.Split(';')[0];
                    ybSAddress = ybSAddress != "" && ybSAddress != "-" ? ybSAddress : ybAddress.Split(';')[1] != "-" ? ybAddress.Split(';')[1] : "-";
                }
                if (ybSAddress == "-" || ybSAddress == "")
                {
                    ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "", false);
                    ybSAddress = ybSAddress != "" && ybSAddress != "-" ? ybSAddress : ybAddress.Split(';')[0];
                }
                ybAddress = ybAddress != "" && ybAddress != "-" ? ybAddress : await RunYbASearch(SongRecord, searchListRequest, "", true);
                ybAddress = ybAddress.Split(';')[0];
            }
            catch (AggregateException) { var timestamp = UpdateLog(DateTime.Now, "yb error", true, c("dlcm_TempPath"), "", "", null, null); }

            UpdateLog(DateTime.Now, "Finishing " + SongRecord.Artist + " " + SongRecord.Song_Title, false, c("dlcm_TempPath"), "0", windw, null, null);

            return ybAddress + ";" + ybLAddress + ";" + ybBAddress + ";" + ybRAddress + ";" + ybCAddress + ";" + ybSAddress;
        }

        public static string Soundex(string data)
        {
            StringBuilder result = new StringBuilder();
            if (data != null && data.Length > 0)
            {
                string previousCode = "", currentCode = "",
                currentLetter = "";
                result.Append(data.Substring(0, 1));
                for (int i = 1; i < data.Length; i++)
                {
                    currentLetter = data.Substring(i, 1).ToLower();
                    currentCode = "";
                    if ("bfpv".IndexOf(currentLetter) > -1)
                        currentCode = "1";
                    else if ("cgjkqsxz".IndexOf(currentLetter) > -1)
                        currentCode = "2";
                    else if ("dt".IndexOf(currentLetter) > -1)
                        currentCode = "3";
                    else if (currentLetter == "1") currentCode = "4";
                    else if ("mn".IndexOf(currentLetter) > -1)
                        currentCode = "5";
                    else if (currentLetter == "r")
                        currentCode = "6";
                    if (currentCode != previousCode)
                        result.Append(currentCode);
                    if (result.Length == 4) break;
                    if (currentCode != "")
                        previousCode = currentCode;
                }
            }
            if (result.Length < 4)
                result.Append(new System.String('O', 4 - result.Length));
            return result.ToString().ToUpper();
        }


        public static int Difference(string datal, string data2)
        {
            int result = 0;
            string soundex1 = Soundex(datal);
            string soundex2 = Soundex(data2);

            if (soundex1 == soundex2) result = 4;
            else
            {
                string sub1 = soundex1.Substring(1, 3);
                string sub2 = soundex1.Substring(2, 2);
                string sub3 = soundex1.Substring(1, 2);
                string sub4 = soundex1.Substring(1, 1);
                string sub5 = soundex1.Substring(2, 1);
                string sub6 = soundex1.Substring(3, 1);

                if (soundex2.IndexOf(sub1) > -1) result = 3;
                else if (soundex2.IndexOf(sub2) > -1) result = 2;
                else if (soundex2.IndexOf(sub3) > -1) result = 2;
                else
                {
                    if (soundex2.IndexOf(sub4) > -1) result++;
                    if (soundex2.IndexOf(sub5) > -1) result++;
                    if (soundex2.IndexOf(sub6) > -1) result++;
                }
                if (soundex1.Substring(0, 1) == soundex2.Substring(0, 1)) result++;
            }
            return (result == 0) ? 1 : result;
        }

        public static bool GetParam(int t, System.Windows.Forms.CheckedListBox chbx_Additional_Manipulations)
        {
            if (chbx_Additional_Manipulations.Items.Count == 0)
                return false;
            if (chbx_Additional_Manipulations.Items[1].ToString().IndexOf("{") < 1) return chbx_Additional_Manipulations.GetItemChecked(t);
            for (var i = 0; i < chbx_Additional_Manipulations.Items.Count; i++)
            {
                string k = chbx_Additional_Manipulations.Items[i].ToString();
                if (k.IndexOf("{") < 1) continue;
                var orderno = k.Substring(k.IndexOf("{") + 1, k.IndexOf("}") - k.IndexOf("{") - 1);
                //string g = c("dlcm_AdditionalManipul" + t.ToString()).ToLower();
                if (orderno.ToInt32() == t)
                    //{
                    return chbx_Additional_Manipulations.GetItemChecked(i);//.ToString() == "true" ? true : false;
                                                                           //    break;
                                                                           //}
            }
            //c("dlcm_AdditionalManipul" + t.ToString()).ToLower()=="yes"? true : false;
            return false;
        }
        public static async Task<string> RunYbASearch(MainDBfields SongRecord, SearchResource.ListRequest searchListRequest, string instr, bool nonnrksmithvideo)
        {
            var ybRAddress = "-"; var ybSAddress = "-";
            searchListRequest.Q = CleanTitle(SongRecord.Artist).Replace(" ", "+") + "+" + CleanTitle(SongRecord.Song_Title).Replace(" ", "+") + "+" + (nonnrksmithvideo == true ? "" : ("rocksmith ".Replace(" ", instr.Length == 0 ? "" : "+"))) + instr;//+ " playthrough".Replace(" ", "+"); // Replace with your search term.
            searchListRequest.MaxResults = 50;

            try
            {
                var searchListResponse = await searchListRequest.ExecuteAsync();// Call the search.list method to retrieve results matching the specified query term.

                List<string> videos = new List<string>();//List<string> channels = new List<string>();List<string> playlists = new List<string>();
                                                         // Add each result to the appropriate list, and then display the lists of
                                                         // matching videos, channels, and playlists.
                foreach (var searchResult in searchListResponse.Items)
                {
                    if (searchResult.Id.Kind == "youtube#video")
                        if (searchResult.Snippet.Title.ToLower().IndexOf(CleanTitle(SongRecord.Artist).ToLower()) >= 0)
                            if (searchResult.Snippet.Title.ToLower().IndexOf(CleanTitle(SongRecord.Song_Title).ToLower()) >= 0)
                                if (searchResult.Snippet.Title.ToLower().IndexOf("rocksmith") >= 0 || nonnrksmithvideo)
                                {
                                    if (searchResult.Snippet.Title.ToLower().IndexOf(instr.ToLower()) >= 0)
                                    {
                                        ybRAddress = searchResult.Id.VideoId;
                                        break;
                                    }
                                    else ybSAddress = searchResult.Id.VideoId;
                                }
                }


                if ((ybRAddress == "" && ybSAddress == "") || (ybRAddress == "-" && ybSAddress == "-"))
                    foreach (var searchResult in searchListResponse.Items)
                    {
                        var xx = WebUtility.HtmlDecode(searchResult.Snippet.Title).ToLower().Replace(" hd ", " ").Replace("rocksmith 2014", "").Replace("rocksmith2014", "").Replace("rocksmith", "").Replace(" - ", " ").Replace(CleanTitle(SongRecord.Artist).ToLower(), "");
                        xx = instr == "" ? xx : xx.Replace(instr.ToLower(), "");
                        xx = xx.Replace("custom song", "").Replace("custom", "").Replace("cdlc", "").Replace("99%", "").Replace("100%", "").Replace("()", "").Replace("  ", " ").Replace("  ", " ").Trim().TrimEnd();
                        var yy = WebUtility.HtmlDecode(searchResult.Snippet.Title).ToLower().Replace(" hd ", " ").Replace("rocksmith 2014", "").Replace("rocksmith2014", "").Replace("rocksmith", "").Replace(" - ", " ").Replace(CleanTitle(SongRecord.Song_Title).ToLower(), "");
                        yy = instr == "" ? yy : yy.Replace(instr.ToLower(), "");
                        yy = yy.Replace("custom song", "").Replace("custom", "").Replace("cdlc", "").Replace("99%", "").Replace("100%", "").Replace("()", "").Replace("  ", " ").Replace("  ", " ").Trim().TrimEnd();
                        var yyy = Difference(yy, CleanTitle(SongRecord.Artist).ToLower());
                        var xxx = Difference(xx, CleanTitle(SongRecord.Song_Title).ToLower());

                        if (searchResult.Id.Kind == "youtube#video")
                            if (searchResult.Snippet.Title.ToLower().IndexOf("rocksmith") >= 0 || nonnrksmithvideo)
                                if (xxx >= 3 || xx.IndexOf(CleanTitle(SongRecord.Song_Title).ToLower()) >= 0)
                                    if (yyy >= 3 || yy.IndexOf(CleanTitle(SongRecord.Artist).ToLower()) >= 0)
                                    {
                                        if (searchResult.Snippet.Title.ToLower().IndexOf(instr.ToLower()) >= 0)
                                        {
                                            ybRAddress = ybRAddress == "" || ybRAddress == "-" ? searchResult.Id.VideoId : ybRAddress;
                                            break;
                                        }
                                        else
                                            if (ybSAddress == "-" || ybSAddress == "") ybSAddress = searchResult.Id.VideoId;
                                    }
                    }
            }
            catch (Exception Ex)
            {
                UpdateLog(DateTime.Now, "Errore " + SongRecord.Artist + " " + SongRecord.Song_Title + Ex.Message.ToString(), false, c("dlcm_TempPath"), "0", null, null, null);
                ConfigRepository.Instance()["dlcm_youtubestatus"] = "NOK";
            }
            return ybRAddress + ";" + ybSAddress;
        }

        public static string GetPackagingDetails(string PackageComment)
        {
            string[] gg = PackageComment.ToString().Split(';');
            return gg[0] + ";" + gg[1] + ";" + gg[2] + ";" + gg[3] + ";" + gg[4] + ";" + gg[5];
        }

        //public static async Task<string> GetYoutubeDetailsAsync(MainDBfields SongRecord, int i, OleDbConnection cnb, ProgressBar pB_ReadDLCs, string windw, bool arrangoff, SQLiteConnection cnz)
        public static async Task<string> GetYoutubeDetailsAsync(MainDBfields SongRecord, int i, OleDbConnection cnb, System.Windows.Forms.ProgressBar pB_ReadDLCs, string windw, bool arrangoff, SQLite.SQLiteConnection cnc)
        {
            string yAddress = null;
            try
            {

                yAddress = await new UtilitiesFunctions().YoutubeRun(SongRecord, i, cnb, windw, arrangoff, cnc);//.Result;//.Wait();
                var ybAddress = yAddress.Split(';')[0];
                var ybLAddress = yAddress.Split(';')[1];
                var ybBAddress = yAddress.Split(';')[2];
                var ybRAddress = yAddress.Split(';')[3];
                var ybCAddress = yAddress.Split(';')[4];
                var ybSAddress = yAddress.Split(';')[5];
                var cmdz = "UPDATE Main SET ";
                cmdz += ybSAddress == "-" ? "" : "Youtube_Playthrough =\"https://www.youtube.com/watch?v=" + ybSAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                cmdz += ybAddress == "-" ? "" : ((ybSAddress == "-" ? "" : ",") + " YouTube_Link=\"https://www.youtube.com/watch?v=" + ybAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"");
                cmdz += " WHERE ID=" + SongRecord.ID;
                DataSet dos = new DataSet();
                if (ybAddress != "-" || ybSAddress != "-")
                    dos = UpdateDB("Main", cmdz + ";", cnb, cnc);

                if (SongRecord.ID != null || SongRecord.ID != null)
                {
                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybLAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Lead\"";
                    dos = new DataSet();
                    if (ybLAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb, cnc);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybRAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Rhythm\"";
                    dos = new DataSet();
                    if (ybRAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb, cnc);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybBAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Bass\"";
                    dos = new DataSet();
                    if (ybBAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb, cnc);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybCAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Combo\"";
                    dos = new DataSet();
                    if (ybCAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb, cnc);
                }
                if (pB_ReadDLCs != null) pB_ReadDLCs.Increment(1);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    var timestamp = UpdateLog(DateTime.Now, "error567" + e.Message, true, c("dlcm_TempPath"), "", "", null, null);
                }
            }
            return yAddress;
        }

        public static decimal IncrementLastDigit(decimal value)
        {
            int[] bits1 = decimal.GetBits(value);
            int saved = bits1[3];
            bits1[3] = 0;   // Set scaling to 0, remove sign
            int[] bits2 = decimal.GetBits(new decimal(bits1) + 1);
            bits2[3] = saved; // Restore original scaling and sign
            return new decimal(bits2);
        }
    }
}
