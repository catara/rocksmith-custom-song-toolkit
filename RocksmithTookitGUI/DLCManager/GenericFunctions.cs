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
using Microsoft.Office.Interop.Access;
using System.Collections.Specialized;

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {

        public const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";

        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC

        public static StringBuilder errorsFound;

        public enum ConverterTypes
        {
            HeaderFix,
            Revorb,
            WEM,
            Ogg2Wem
        }


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
            public string Is_Acoustic { get; set; }
            public string Top10 { get; set; }
            public string Has_Other_Officials { get; set; }
            public string Spotify_Song_ID { get; set; }
            public string Spotify_Artist_ID { get; set; }
            public string Spotify_Album_ID { get; set; }
            public string Spotify_Album_URL { get; set; }
            public string Audio_OrigHash { get; set; }
            public string Audio_OrigPreviewHash { get; set; }
            public string AlbumArt_OrigHash { get; set; }
            public string Duplicate_Of { get; set; }
            public string Split4Pack { get; set; }
            public string UseInternalDDRemovalLogic { get; set; }
            public string Is_Instrumental { get; set; }
            public string Is_Single { get; set; }
            public string Is_Soundtrack { get; set; }
            public string Is_EP { get; set; }
            public string Has_Had_Audio_Changed { get; set; }
            public string Has_Had_Lyrics_Changed { get; set; }
            public string Album_Sort { get; set; }
            public string IntheWorks { get; set; }
            public string Is_Uncensored { get; set; }
            public string LyricsLanguage { get; set; }
            public string LastConversionDateTime { get; set; }
            public string ImprovedWithDM { get; set; }
            public string Is_FullAlbum { get; set; }

        }



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
        public static async Task<string> CheckIfConnectedToSpotify()
        {
            var netstatus = GenericFunctions.CheckIfConnectedToInternet().Result.ToString();
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

        public static void ShowConnectivityError(Exception ex, string txt)
        {

            if (ex.Message.IndexOf("The 'Microsoft.ACE.OLEDB.") > -1 && ex.Message.IndexOf("provider is not registered on the local machine.") > -1)
            {
                ErrorWindow frm1 = new ErrorWindow("You need to Download Connectivity patch 32/64 bit to match your version of Office @ " + txt, ConfigRepository.Instance()["dlcm_Access" + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]], "Error @Import", false, false, true, "", "", "");
                frm1.ShowDialog();
            }
        }

        public static string CompactAndRepair(OleDbConnection cnb)
        {
            var dates = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            try
            {
                Microsoft.Office.Interop.Access.Application app = new Microsoft.Office.Interop.Access.Application();
                app.CompactRepair(cnb.DataSource.ToString(), cnb.DataSource.ToString(), false); app.Visible = false;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Microsoft Access must create a backup of your file before you perform the repair operation. Enter a name for the backup file.") > -1)
                {
                    Microsoft.Office.Interop.Access.Application apps = new Microsoft.Office.Interop.Access.Application();
                    try
                    {
                        apps.CompactRepair(cnb.DataSource.ToString(), cnb.DataSource.ToString() + dates, false); apps.Visible = false;
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
                var tgst = "Error1 ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
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

        static public MainDBfields[] GetRecord_s(string cmd, OleDbConnection cnb)
        {
            var MaximumSize = 0;
            MainDBfields[] query = new MainDBfields[10000];
            DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb);

            var i = 0;
            MaximumSize = dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;

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
                i++;
                query[i] = new MainDBfields();
            }
            return query;
        }
        static public string GetHash(string filename)
        {
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
                        catch (Exception ex) { var tsst = "Errorf ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
                        sr.Close();
                    }
                }
            }
            catch (Exception ex) { var tsst = "Errorg ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            return FileHash;
        }

        static public void DeleteFromDB(string DB, string slct, OleDbConnection cnb)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            try
            {
                DataSet dss = new DataSet();
                OleDbDataAdapter dan = new OleDbDataAdapter(slct, cnb);
                dan.Fill(dss, "DB");
                dan.Dispose();
            }
            catch (Exception ex)
            {
                var tsst = "Error4 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                ShowConnectivityError(ex, DB + "--------" + slct + "--------------");
                return;
            }
        }

        static public void CheckValidityGetHASHAdd2ImportPub(object sender, DoWorkEventArgs e)
        {

            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = c("dlcm_TempPath");

            string[] args = (e.Argument).ToString().Split(';');
            string s = args[0];
            string i = args[1];
            string ImportPackNo = args[2];
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            var tsst = "Start Gathering ..."; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, tmpPath, i, "", null, null);

            var invalid = "No";
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
                return;
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

            var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";

            var insertvalues = "\"" + s + "\",\"" + fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\""
                + FileHash + "\",\"" + fi.Length + "\",\"" + ff + "\",\"" + ImportPackNo + "\",\"" + (plt == "Pc" ? "Pc" : plt) + "\",\"" + invalid + "\"";

            InsertIntoDBwValues("Import", insertcmdd, insertvalues, cnb, 0);

            e.Result = "Done";
            cnb.Close();
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues, OleDbConnection cnb, int mutit)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            string insertcmd;
            try
            {
                DataSet dsm = new DataSet();
                if (fvalues.ToLower().IndexOf("select ") == 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
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
                    DateTime timestamp;
                    timestamp = UpdateLog(DateTime.Now, "error at import " + ee.Message + "-" + insertcmd, true, tmpPath, mutit.ToString(), "", null, null);
                    ShowConnectivityError(ex, ftable + "--------" + fvalues + "--------------");
                }
            }
        }
        static public DataSet SelectFromDB(string ftable, string fcmds, string currentDB, OleDbConnection cn)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DataSet dfsm = new DataSet();
            if (File.Exists(DB_Path))
            {
                DataSet dsm = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cn))
                    try
                    {
                        da.Fill(dsm, ftable);
                        da.Dispose();
                    }
                    catch (Exception ex) { ShowConnectivityError(ex, ftable + "---" + fcmds); }
                return dsm;
            }
            else return dfsm;
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
                    ErrorWindow frm1 = new ErrorWindow("error when calc file hash ", "", "Error at import", false, false, true, "", "", "");
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
                    if (!Copy)
                    {
                        File.Copy(Source, Dest, true);
                        if (File.Exists(Source)) DeleteFile(Source);
                    }
                    else File.Copy(Source, Dest, true);
                }
                catch (Exception ex)
                {
                    var tsst = "Error6 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                }
            else
                try
                {
                    File.Copy(Source, Dest, true);
                    if (!Copy) FileSystem.DeleteFile(Source, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                }
                catch (Exception ex)
                {
                    var tsst = "Error6 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
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

        static public void CleanFolder(string pathfld, string exttoigno, bool copy, bool archive, string Archive_Path, string form, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            var tst = "Assessing to clean Folders..." + pathfld; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            string[] args = new string[50]; for (var x = 0; x < 50; x++) args[x] = "";

            args = exttoigno.ToString().Split(';');
            if (pathfld != "" && pathfld != null && Directory.Exists(pathfld))
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
                            if (c("dlcm_DBFolder") == file.FullName) continue;
                            if ((file.FullName.IndexOf(args[0]) > 0 || file.FullName.IndexOf(args[1]) > 0) && archive & copy) //|| exttoigno == ""
                                CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), archive, null, false);
                            if ((file.FullName.IndexOf(args[0]) > 0 || file.FullName.IndexOf(args[1]) > 0) && archive & !copy) //|| exttoigno == ""
                                CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), copy, null, false);
                            else DeleteFile(file.FullName);
                        }
                    }
                }
                catch (Exception ex) { var tsst = "Error7 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
        }


        static public DataSet UpdateDB(string ftable, string fcmds, OleDbConnection cn)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DataSet dsm = new DataSet();
            if (File.Exists(DB_Path))
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
                    ShowConnectivityError(ex, ftable + "--------" + fcmds + "--------------");
                    return dsm;
                }
            }
            else return dsm;
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
                var tsst = "Error8 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                ShowConnectivityError(ex, ftable + "--------" + fcmds + "--------------");
            }

            return;
        }

        static public void DeleteFile(string file)
        {
            try
            {
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul81"] == "Yes") FileSystem.DeleteFile(file, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                else File.Delete(file);
            }
            catch (Exception ex)
            {
                var tsst = "Error @filedelete..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }

        static public void DeleteDirectory(string dir)
        {
            try
            {
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul81"] == "Yes") FileSystem.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                else Directory.Delete(dir, true);
            }
            catch (Exception ex)
            {
                var tsst = "Error @dir delete..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
            }
        }

        static public string WwiseInstalled(string Mss)
        {
            var wwisePath = "";
            if (!string.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                wwisePath = ConfigRepository.Instance()["general_wwisepath"];
            else
                wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
            if (wwisePath == "" || !Directory.Exists(wwisePath))
            {
                ErrorWindow frm1 = new ErrorWindow("Cause " + Mss + ".\nPlease Install Wwise Launcher then Wwise v" + wwisePath + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", false, false, true, "", "", "");
                frm1.ShowDialog();
                return "0" + ";" + frm1.IgnoreSong + ";" + frm1.StopImport;
            }
            else
                return "1" + ";0;0";
        }

        public static string Manipulate_strings(string words, int k, bool ifn, bool orig_flag, bool bassRemoved, GenericFunctions.MainDBfields[] SongRecord, string sep1, string sep2, bool beta, bool sort)//, string always_grp, string grp)
        {

            //2. Read from DB
            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = sep1;
            var readt = false;
            var oldtxt = "";
            var last_ = 0;
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            for (i = 0; i <= txt.Length - 1; i++)
            {
                curtext = txt[i].ToString();
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
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
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Artist.Length > 4 && sort)
                                tzt = sep2 + MoveTheAtEnd(SongRecord[k].Artist) + sep1;
                            else tzt = sep2 + SongRecord[k].Artist + sep1;
                            break;
                        case "<Title>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Song_Title.Length > 4 && sort)
                                tzt = sep2 + MoveTheAtEnd(SongRecord[k].Song_Title) + sep1;
                            else tzt = sep2 + SongRecord[k].Song_Title + sep1;
                            break;
                        case "<Artist Sort>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Artist_Sort.Length > 4)
                                tzt = sep2 + MoveTheAtEnd(SongRecord[k].Artist_Sort) + sep1;
                            break;
                        case "<Title Sort>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && SongRecord[k].Song_Title_Sort.Length > 4)                                                                                         //    data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                tzt = sep2 + MoveTheAtEnd(SongRecord[k].Song_Title_Sort) + sep1;
                            else tzt = sep2 + SongRecord[k].Song_Title_Sort + sep1;
                            break;
                        case "<Album>":
                            tzt = sep2 + SongRecord[k].Album + sep1;
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
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Group", "SELECT Groups FROM Groups WHERE CDLC_ID=\"" + SongRecord[k].ID + "\" AND Type=\"DLC\"", "", cnb);
                            var noOfRect = dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;

                            for (var j = 0; j <= noOfRect - 1; j++)
                            {
                                var grp = dvs.Tables[0].Rows[j].ItemArray[0].ToString() + " ";
                                tzt += grp;
                            }
                            break;
                        case "<GroupIndex>":
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Groups", "SELECT TOP 1 Comments FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + SongRecord[k].Groups + "\"", "", cnb);
                            var noOfRehc = dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;
                            if (noOfRehc > 0) tzt = dbs.Tables[0].Rows[0].ItemArray[1].ToString();
                            break;
                        case "<BetaGroupOrIndex>":
                            DataSet dgs = new DataSet(); dgs = SelectFromDB("Groups", "SELECT TOP 1 Comments FROM Groups WHERE Type=\"DLC\" AND Groups=\"" + SongRecord[k].Groups + "\"", "", cnb);
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
                        case "<<IntheWorks>>":
                            tzt = ((SongRecord[k].IntheWorks == "Yes") ? "-IntheWorks " : "");
                            break;
                        case "<Lyrics Language>":
                            tzt = SongRecord[k].LyricsLanguage;
                            break;
                        case "<Track version>":
                            tzt = ((SongRecord[k].Is_Acoustic == "Yes") ? "-Acoustic " + SongRecord[k].Live_Details : "");
                            tzt += ((SongRecord[k].Is_Live == "Yes") ? "-Live " : "");
                            tzt += ((SongRecord[k].Is_Instrumental == "Yes") ? "-Instrumental " : "");
                            tzt += ((SongRecord[k].Is_EP == "Yes") ? "-EP " : "");
                            tzt += ((SongRecord[k].Is_Uncensored == "Yes") ? "-Uncensored " : "");
                            tzt += ((SongRecord[k].Is_Soundtrack == "Yes") ? "-SoundTrack " : "");
                            tzt += ((SongRecord[k].Is_Single == "Yes") ? "-Single " : "");
                            break;
                        case "<CDLC_ID>":
                            tzt = SongRecord[k].ID;
                            break;
                        case "<DLCM Version>":
                            tzt = c("dlcm_DLCManager_Version");
                            break;
                        case "<Date>":
                            tzt = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
                            break;
                        case "<Random5>":
                            Random randomp = new Random();
                            int rn = randomp.Next(0, 100000);
                            tzt = rn.ToString();
                            break;
                        case "<Artist Short>":
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && SongRecord[k].Artist.Length > 4 && sort && SongRecord[k].Artist_ShortName == "")
                                tzt = sep2 + MoveTheAtEnd(SongRecord[k].Artist) + sep1;
                            else tzt = SongRecord[k].Artist_ShortName != "" ? SongRecord[k].Artist_ShortName : SongRecord[k].Artist;
                            break;
                        case "<Album Short>":
                            tzt = SongRecord[k].Album_ShortName != "" ? SongRecord[k].Album_ShortName : SongRecord[k].Album;
                            break;
                        case "<Author>":
                            tzt = SongRecord[k].Author;
                            break;
                        case "<Space>":
                            tzt = " ";
                            break;
                        case "<Avail. Tracks>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Lead == "Yes") ? "L" : "") + ((SongRecord[k].Has_Combo == "Yes") ? "C" : "") + ((SongRecord[k].Has_Rhythm == "Yes") ? "R" : "") + ((SongRecord[k].Has_Vocals == "Yes") ? "V" : "");
                            break;
                        case "<Avail. Tracks w Bonus>":
                            DataSet dcs = new DataSet(); dcs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
                            var noOfRezc = dcs.Tables[0].Rows.Count;
                            float FirstLyric = 5000;
                            float FirstVocal = 0; var B = ""; var L = ""; var R = ""; var C = "";

                            for (var j = 0; j <= noOfRezc - 1; j++)
                            {
                                var Bonus = dcs.Tables[0].Rows[j].ItemArray[1].ToString();
                                var ArrangementType = dcs.Tables[0].Rows[j].ItemArray[3].ToString();
                                var RouteMask = dcs.Tables[0].Rows[j].ItemArray[4].ToString();
                                if (ArrangementType == "Vocal" && ArrangementType == "ShowLight") continue;
                                if (RouteMask == "Bass") B = "B";
                                if (RouteMask == "Lead") L = "L";
                                if (RouteMask == "Rhythm") R = "R";
                                if (RouteMask == "Combo") C = "C";
                                if (Bonus.ToLower() == "true")
                                {
                                    if (RouteMask == "Bass") B += "b";
                                    if (RouteMask == "Lead") L += "b";
                                    if (RouteMask == "Rhythm") R += "b";
                                    if (RouteMask == "Combo") C += "b";
                                }
                            }
                            tzt = L + B + R + C;
                            break;
                        case "<Bass_HasDD>":
                            tzt = ((SongRecord[k].Has_BassDD == "No" || bassRemoved) && SongRecord[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        case "<Avail. Tracks and Timings>":
                            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
                            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
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
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                var b = ""; var p = "";
                                if (noOfRecP > 1) p = Part;

                                tzt += (RouteMask == "Lead" ? " L" + p + shortstart : "") + (RouteMask == "Bass" ? " B" + p + shortstart : "") + (RouteMask == "Rhythm" ? " R" + p + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + p + shortstart : ""));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings&Bonus>":
                            DataSet dxp = new DataSet(); dxp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
                            var noOfRecc = dxp.Tables.Count > 0 ? (string.IsNullOrEmpty(dxp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dxp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
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
                                string shortstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".") + 2) + "s") : StartTime;

                                var b = ""; var p = "";
                                if (Bonus.ToLower() == "true") b = "b";
                                if (noOfRecc > 1) p = Part;

                                tzt += (RouteMask == "Lead" ? " L" + b + p + shortstart : "") + (RouteMask == "Bass" ? " B" + b + p + shortstart : "") + (RouteMask == "Rhythm" ? " R" + b + p + shortstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + b + p + shortstart : ""));
                            }
                            break;
                        case "<Avail. Tracks and ShortTimings>":
                            DataSet dsp = new DataSet(); dsp = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
                            var noOfRecPS = dsp.Tables.Count > 0 ? (string.IsNullOrEmpty(dsp.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dsp.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

                            DataSet dss = new DataSet(); dss = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongRecord[k].ID + "", "", cnb);
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
                                string shorterstart = StartTime != "" && StartTime != null && StartTime.IndexOf(".") > 0 ? (StartTime.Substring(0, StartTime.IndexOf(".")) + "s") : StartTime;

                                var b = ""; var p = "";
                                if (Bonus.ToLower() == "true") b = "b";
                                if (noOfRecPS > 1) p = Part;

                                tzt += (RouteMask == "Lead" ? " L" + b + p + shorterstart : "") + (RouteMask == "Bass" ? " B" + b + p + shorterstart : "") + (RouteMask == "Rhythm" ? " R" + b + p + shorterstart : ((RouteMask == "None" && ArrangementType == "Vocal") ? " V" + b + p + shorterstart : ""));
                            }
                            break;
                        case "<Timestamp>":
                            tzt = DateTime.Now.ToString("yyyyMMdd HHmmssfff").Replace(":", "").Replace(" ", "").Replace(".", "");
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
                                            + ((SongRecord[k].FilesMissingIssues != "") ? ("_" + SongRecord[k].FilesMissingIssues) : "");
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
                        if (SongRecord[0].Album.ToLower().IndexOf("live at") < 0 || (curelem != "<Live>"))
                            fulltxt = fulltxt.IndexOf("[") > 0 ? fulltxt.Replace(tzt.Replace("[", "").Replace("]", ""), "") + tzt : fulltxt + tzt;
                        else fulltxt += tzt;

                    if (oldtxt == fulltxt && last_ > 0) fulltxt = fulltxt.Substring(0, last_);
                    last_ = fulltxt.Length;
                }
            }

            if ((sep1 + sep2).Length > 0) return (fulltxt + sep2).Replace(sep1 + sep2, "");
            else return fulltxt.Trim('-');
        }

        public static void DeleteRecords(string IDs, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb)
        {
            //Delete records
            DialogResult result1 = MessageBox.Show(norows + " of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("), "", cnb);
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

                if (cmmd != cmmd)
                {
                    DialogResult resultgf = MessageBox.Show(psarcPath + " have missing original files. Are you sure you want the records removed as atm you could still generate the songs?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultgf == DialogResult.No) cmd = cmmd.Replace(", ,", ",").Replace(",,", ",").Replace(", )", ")").Replace(",)", ")");
                }

                DialogResult resultf = MessageBox.Show(norows + "Do you wanna have Audit Trail of these to be deletes songs, deleted too, as to import them again?" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultf == DialogResult.Yes)
                {
                    // //Delete Audit trail of import
                    DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + cmd.Replace("ID IN (SELECT ID ", "FileHash IN (SELECT FileHash ") + "\")", cnb);
                }


                for (var i = 0; i < rcount; i++)
                {
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    DeleteDirectory(filePath);

                    //Move psarc file to Duplicates                        
                    string psarcPathh = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    var fh = GetHash(psarcPathh);
                    psarcPathh = CopyMoveFileSafely(psarcPathh, psarcPathh.Replace("0_old", "0_archive"), false, fh, false);
                }

                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb);

                //Delete Arangements
                DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE CDLC_ID IN (" + IDs + ")", cnb);

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE * FROM Tones WHERE CDLC_ID IN (" + IDs + ")", cnb);

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE * FROM Tones_GearList WHERE CDLC_ID IN (" + IDs + ")", cnb);

                //// //Delete Audit trail of import
                //DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash.Replace(", ", "\", \"") + "\")", cnb); 

                //Delete Audit trail of pack
                DeleteFromDB("Pack_AuditTrail", "DELETE * FROM Pack_AuditTrail WHERE CDLC_ID IN (" + IDs + ")", cnb);

                //Delete songs from Groups
                DeleteFromDB("Groups", "DELETE * FROM Groups WHERE Type=\"DLC\" AND CDLC_ID IN (\"" + IDs + "\")", cnb);

                MessageBox.Show(rcount + "  Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static DialogResult CreateTempFolderStructure(string TempPathImport, string oldPathImport, string brokenPathImport, string dupliPathImport,
            string dlcpacks, string pathDLC, string repackedpath, string repackedXBOXPath, string repackedPCPath, string repackedMACPath, string repackedPSPath,
            string logPath, string albumCoversPSPath, string LogPSPath, string ArchivePath, string dataPath, string TempPath, string dflt_Path_Import)
        {
            DialogResult result1 = DialogResult.Yes;
            if (!Directory.Exists(TempPathImport) || !Directory.Exists(pathDLC) || !Directory.Exists(oldPathImport) || !Directory.Exists(brokenPathImport) ||
                !Directory.Exists(dupliPathImport) || !Directory.Exists(dlcpacks + "\\temp") || !Directory.Exists(dlcpacks + "\\manipulated") ||
                !Directory.Exists(dlcpacks + "\\manifests") || !Directory.Exists(dlcpacks + "\\manipulated\\temp") || !Directory.Exists(repackedpath) ||
                !Directory.Exists(repackedXBOXPath) || !Directory.Exists(repackedPCPath) || !Directory.Exists(repackedMACPath) ||
                !Directory.Exists(repackedPSPath) || (!Directory.Exists(logPath) && logPath != "") || !Directory.Exists(LogPSPath)
                || !Directory.Exists(albumCoversPSPath) || !Directory.Exists(ArchivePath) || !Directory.Exists(dataPath) || !Directory.Exists(TempPath))
            {
                var fldrm = "";
                if (!Directory.Exists(TempPathImport) && (TempPathImport != null)) fldrm += ";" + TempPathImport;
                if (!Directory.Exists(pathDLC) && (pathDLC != null)) fldrm += ";" + pathDLC;
                if (!Directory.Exists(oldPathImport) && (oldPathImport != null)) fldrm += ";" + oldPathImport;
                if (!Directory.Exists(brokenPathImport) && (brokenPathImport != null)) fldrm += ";" + brokenPathImport;
                if (!Directory.Exists(dupliPathImport) && (dupliPathImport != null)) fldrm += ";" + dupliPathImport;
                if (!Directory.Exists(dlcpacks) && (dlcpacks != null)) fldrm += ";" + dlcpacks;
                if (!Directory.Exists(dlcpacks + "\\manifests") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manifests";
                if (!Directory.Exists(dlcpacks + "\\manipulated") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manipulated";
                if (!Directory.Exists(dlcpacks + "\\manipulated\\temp") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\manipulated\\temp";
                if (!Directory.Exists(dlcpacks + "\\temp") && (dlcpacks != null)) fldrm += ";" + dlcpacks + "\\temp";
                if (!Directory.Exists(repackedpath) && (repackedpath != null)) fldrm += ";" + repackedpath;
                if (!Directory.Exists(repackedXBOXPath) && (repackedXBOXPath != null)) fldrm += ";" + repackedXBOXPath;
                if (!Directory.Exists(repackedPCPath) && (repackedPCPath != null)) fldrm += ";" + repackedPCPath;
                if (!Directory.Exists(repackedMACPath) && (repackedMACPath != null)) fldrm += ";" + repackedMACPath;
                if (!Directory.Exists(repackedPSPath) && (repackedPSPath != null)) fldrm += ";" + repackedPSPath;
                if (!Directory.Exists(logPath) && logPath != null && (logPath != "")) fldrm += ";" + logPath;
                if (!Directory.Exists(albumCoversPSPath) && (albumCoversPSPath != null)) fldrm += ";" + albumCoversPSPath;
                if (!Directory.Exists(LogPSPath) && (LogPSPath != null)) fldrm += ";" + LogPSPath;
                if (!Directory.Exists(ArchivePath) && (ArchivePath != null)) fldrm += ";" + ArchivePath;
                if (!Directory.Exists(dataPath) && (dataPath != null)) fldrm += ";" + dataPath;
                if (!Directory.Exists(TempPath) && (TempPath != null)) fldrm += ";" + TempPath;
                if (!Directory.Exists(dflt_Path_Import) && (dflt_Path_Import != null)) fldrm += ";" + dflt_Path_Import;

                DirectoryInfo di;
                result1 = MessageBox.Show("Some( " + fldrm + " ) folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Ignore\n3. Cancel Import operation", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {
                    string[] args = (fldrm).ToString().Split(';');

                    foreach (string s in args)
                    {
                        try
                        {
                            di = Directory.CreateDirectory(s);
                            UpdateLog(DateTime.Now, "created folders: " + fldrm, false, c("dlcm_TempPath"), "", "", null, null);
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error9 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
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
            DeleteFile(filePath + ".newvcl");
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

        public static void UpdatePackingLog(string DB, string DBc_Path, int packid, string dlcID, string ex, OleDbConnection cnb)
        {

            var insertcmdd = "Pack, CDLC_ID, Dates, Comments";
            var insertvalues = "\"" + packid + "\"," + dlcID + ",\"" + System.DateTime.Now + "\",\"" + ex.Replace("'", "") + "\"";
            InsertIntoDBwValues(DB, insertcmdd, insertvalues, cnb, 0);
            DataSet dxr = new DataSet();
            if (DB == "LogPackingError")
                dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"Yes\" WHERE ID=" + dlcID + ";", cnb);
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
            var Has_BassDD = "No";

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
                        Has_BassDD = "Yes";
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

            return Has_BassDD;

        }

        public static void Downstream(string fn, float bitrate, string windw)
        {
            File.Copy(fn, fn + ".old", true);
            var startInfo = new ProcessStartInfo();
            var tst = ""; var timestamp = DateTime.Now;
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = fn.Replace(".wem", "_fixed.ogg").Replace("_fixed_fixed.ogg", "_fixed.ogg").TrimStart(' ');// (fn.IndexOf("preview.wem") > 0 ? : "");//fn.Replace(".wem", "_fixed.ogg")
            var tt = t + "l";
            startInfo.Arguments = string.Format(" \"{0}\" -o \"{1}\" -Q",
                                                t,
                                                tt);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;
            //to capture error mss
            //startInfo.RedirectStandardOutput = true; 
            //startInfo.RedirectStandardError = true;
            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start();
                    //string stdoutx = DDC.StandardOutput.ReadToEnd();
                    //string stderrx = DDC.StandardError.ReadToEnd();
                    DDC.WaitForExit(1000 * 60 * 5); //wait 1min 
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

                                DDgC.StartInfo = startInfo; DDgC.Start(); DDgC.WaitForExit(1000 * 60 * 5); //wait 1min
                                if (DDgC.ExitCode == 0)
                                {
                                    DeleteFile(tt);

                                    DeleteFile(fn);
                                    var i = 1;
                                    do //sometimes it fails
                                    {
                                        tst = "Convert to wem ... " + i + " - " + fn;
                                        timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "", null, null);
                                        GenericFunctions.Converters(t, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);

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
                                                DeleteDirectory(templateDir);
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
                                        DeleteFile(t.Replace(".ogg", ".wem"));
                                    }
                                    else if (t.Replace(".ogg", ".wem") != fn) File.Copy(fn + ".old", fn, true);
                                    if (File.Exists(t.Replace(".ogg", ".wav"))) DeleteFile(t.Replace(".ogg", ".wav"));
                                    if (File.Exists(t.Replace("_fixed.ogg", "_preview_fixed.wav"))) DeleteFile(t.Replace("_fixed.ogg", "_preview_fixed.wav"));
                                    if (File.Exists(t.Replace(".ogg", "_preview.wem"))) DeleteFile(t.Replace(".ogg", "_preview.wem"));
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
            DeleteFile(fn + ".old");
        }

        public static string CleanTitle(string st)
        {
            var rt = st.IndexOf("["); var rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            rt = st.IndexOf("["); rdt = st.IndexOf("]"); if (rt >= 0 && rdt > 0) st = st.Replace(st.Substring(rt, rdt - rt + 1), "").Trim();
            return st;
        }


        public static string[] GetFTPFiles(string filen)
        {
            try
            {
                System.Net.FtpWebRequest ftpRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(filen);
                ftpRequest.Credentials = new System.Net.NetworkCredential("anonymous", "bogdan@capi.ro");
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftpRequest.GetResponse();
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF7, true);

                string[] directories = new string[10000];
                var i = 0;
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directories[i] = (Path.GetFileName(line)).Substring(3, Path.GetFileName(line).Length - 3);
                    if (line.IndexOf("psarc") > 0) i++;
                    line = streamReader.ReadLine();
                }

                streamReader.Close();
                return directories;
            }
            catch (Exception ex) { var tsst = "Error17 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); return null; }
        }

        public static string c(string configstring)
        {
            return ConfigRepository.Instance()[configstring];
        }

        public static string DeleteCOPYedSongs(string filen, string FTPPath, OleDbConnection cnb, string ID, string platform)
        {
            File.Move(filen, FTPPath);//Delete latest copied file

            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT CopyPath FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " and Platform=\"" + platform.ToUpper() + "\" and PackPath not like \"%%\\\"ORDER BY ID DESC;", "", cnb);
            var rec = dvr.Tables[0].Rows.Count;
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

        public static string DeleteFTPedSongs(string filen, string FTPPath, OleDbConnection cnb, string ID, string ftpstatus)
        {
            if (ftpstatus.ToLower() != "ok") return "not";

            DeleteFTPFiles(filen, FTPPath);//Delete latest remote file

            DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT FileName FROM Pack_AuditTrail WHERE CDLC_ID=" + ID + " and Platform=\"PS3\" ORDER BY ID DESC;", "", cnb);
            var rec = dvr.Tables.Count == 0 ? 0 : dvr.Tables[0].Rows.Count;
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
                var tsst = "Error18 ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
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
        public static string FTPFile(string filel, string filen, string TempPat, string SearchCm, string ID, OleDbConnection cnb, string ftpstatus)
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
                    dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\" WHERE ID=" + ID + ";", cnb);

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
                GenericFunctions.Ogg2Wav(audioPath, wavPath); //detect quality here
                if (!File.Exists(oggPreviewPath))
                {
                    GenericFunctions.Ogg2Preview(audioPath, oggPreviewPath, previewLength, chorusTime);
                    GenericFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
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
                        GenericFunctions.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    else
                    {
                        DeleteFile(oggPath);
                        GenericFunctions.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    GenericFunctions.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                    GenericFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                }

                if (!File.Exists(wavPreviewPath) || !File.Exists(audioPath))
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
                GenericFunctions.Ogg2Wav(oggPath, wavPath);
                GenericFunctions.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                GenericFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                Wwise.Wav2Wem(wavPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            return audioPath;
        }
        public static void Converters(string file, ConverterTypes converterType, bool mssON, bool WinOn)
        {

            var txtOgg2FixHdr = string.Empty;
            var txtWwiseConvert = string.Empty;
            var txtWwise2Ogg = string.Empty;
            var txtAudio2Wem = string.Empty;

            Dictionary<string, string> errorFiles = new Dictionary<string, string>();
            List<string> successFiles = new List<string>();

            try
            {
                var extension = Path.GetExtension(file);
                switch (converterType)
                {
                    case ConverterTypes.Ogg2Wem:
                        GenericFunctions.Convert2Wem(file, 4, 4 * 1000);
                        ////Delete any preview_preview file created..by....?ccc
                        //foreach (string prev_prev in Directory.GetFiles(Path.GetDirectoryName(file), "*preview_preview*", System.IO.SearchOption.AllDirectories))
                        //{
                        //    DeleteFile(prev_prev);
                        //}
                        break;
                }

                successFiles.Add(file);
            }
            catch (Exception ex)
            {
                var tsst = "uError ...i" + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                errorFiles.Add(file, ex.Message);
            }

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

        public static void ProgressWithText(string txt, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            //pB_ReadDLCs.CreateGraphics().Clear(System.Drawing.Color.HotPink);
            pB_ReadDLCs.CreateGraphics().DrawString(txt, new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        }

        public static DateTime UpdateLog(DateTime dt, string txt, bool bbl, string tmpPath, string MultithreadNo, string form, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            DateTime dtt = System.DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? c("dlcm_TempPath") + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            var ismaindb = "";
            if (pB_ReadDLCs != null)
            {
                pB_ReadDLCs.CreateGraphics().Clear(System.Drawing.Color.HotPink);
                pB_ReadDLCs.CreateGraphics().DrawString(txt, new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }

            var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');
            if (form != null && form != "" && rtxt_StatisticsOnReadDLCs != null)
                rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text;


            if (form == "MainDB")
                ismaindb = "maindb";
            // Write the string to a file. packid+
            Random randomp = new Random();
            var packid = 0;
            packid = randomp.Next(0, 100000);
            var fn = (logPath == null || !Directory.Exists(logPath) ? tmpPath + "\\0_log" : logPath) + "\\" + "current_" + ismaindb + "temp" + MultithreadNo + ".txt";
            try
            {
                if (File.Exists(fn))
                {
                    using (StreamWriter sw = File.AppendText(fn))
                    {
                        sw.WriteLine(dtt.ToString() + " - " + ii.ToString() + " - " + txt.ToString());// This text is always added, making the file longer over time if it is not deleted.
                    }
                }
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            if (c("dlcm_Debug").ToLower() == "yes" && txt.ToLower().IndexOf("error") >= 0)
                ;
            return dtt;
        }

        public static DateTime UpdateLogs(DateTime dt, string txt, bool bbl, string logPath, string tmpPath, string MultithreadNo, string form, ProgressBar pB_ReadDLCs)
        {
            pB_ReadDLCs.Value += 1;
            DateTime dtt = System.DateTime.Now;
            var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');
            if (form == "DLCManager")
            {
                pB_ReadDLCs.Value += 1;
                pB_ReadDLCs.CreateGraphics().DrawString("-" + txt + "---------------", new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }
            // Write the string to a file. packid+
            Random randomp = new Random();
            var packid = 0;
            packid = randomp.Next(0, 100000);
            var fn = (logPath == null || !Directory.Exists(logPath) ? tmpPath + "\\0_log" : logPath) + "\\" + "current_temp" + MultithreadNo + ".txt";
            // This text is always added, making the file longer over timev
            // if it is not deleted.
            if (File.Exists(fn))
            {
                using (StreamWriter sw = File.AppendText(fn))
                {
                    sw.WriteLine(dtt + " - " + ii + " - " + txt);
                }
            }
            pB_ReadDLCs.Value += 1;
            return dtt;
        }

        public static Platform SourcePlatform { get; set; }
        public static Platform TargetPlatform { get; set; }
        public static void GeneratePackage(object sender, DoWorkEventArgs e)
        {

            Random randomp = new Random();
            var packid = "";
            string[] args = (e.Argument).ToString().Split(';');
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
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

            //Read from DB
            MainDBfields[] SongRecord = new MainDBfields[10000];
            SongRecord = GetRecord_s(cmd, cnb);
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
                bool chbx_Beta = args[7].ToLower() == "true" ? true : false;
                string chbx_Group = args[8];
                string Groupss = args[9];
                TempPath = args[10];
                bool chbx_UniqueID = args[11].ToLower() == "true" ? true : false;
                bool chbx_Last_Packed = args[12].ToLower() == "true" ? true : false;
                bool chbx_Last_PackedEnabled = args[13].ToLower() == "true" ? true : false;
                bool chbx_CopyOld = args[14].ToLower() == "true" ? true : false;
                bool chbx_CopyOldEnabled = args[15].ToLower() == "true" ? true : false;
                bool chbx_Copy = args[16].ToLower() == "true" ? true : false;
                bool chbx_Replace = args[17].ToLower() == "true" ? true : false;
                bool chbx_ReplaceEnabled = args[18].ToLower() == "true" ? true : false;
                packid = args[19];
                string windw = args[20];
                string Original_FileName = SongRecord[0].Original_FileName;

                string txt_RemotePath = SongRecord[0].Remote_Path;
                string txt_FTPPath = args[25];
                bool chbx_RemoveBassDD = args[26].ToLower() == "true" ? true : false;
                bool chbx_BassDD = SongRecord[0].Has_BassDD.ToLower() == "yes" ? true : false;
                bool chbx_KeepBassDD = SongRecord[0].Keep_BassDD.ToLower() == "yes" ? true : false;
                bool chbx_KeepDD = SongRecord[0].Keep_DD.ToLower() == "yes" ? true : false;
                string chbx_Original = SongRecord[0].Is_Original;
                string txt_DLC_ID = args[31];
                string SearchCmd = args[32];
                string RocksmithDLCPath = args[33];
                string DLC_Name = SongRecord[0].DLC_Name;
                bool updateTonesArrangs = ConfigRepository.Instance()["dlcm_AdditionalManipul76"].ToLower() == "yes" ? true : false;
                multithreadname = args[36];
                form = args[37];
                var ord_no = args[38];
                var spotystatus = args[39];
                var ybstatus = args[40];
                var ftpstatus = args[41];
                string chbx_UseInternalDD = ConfigRepository.Instance()["dlcm_AdditionalManipul31"].ToLower() == "yes" ? "Yes" : SongRecord[0].UseInternalDDRemovalLogic;//(.ToLower() == "yes" ? true : false;
                string chbx_Format = (chbx_PC != "" ? "PC" : (chbx_PS3 != "" ? "PS3" : (chbx_XBOX != "" ? "XBOX360" : (chbx_Mac != "" ? "Mac" : ""))));
                UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, form, null, null);
                if (c("dlcm_MuliThreading") == "No" && form != "DLCManager")
                {
                    ConfigRepository.Instance()["dlcm_MuliThreading"] = "Maybe";
                }
                else if (c("dlcm_MuliThreading") == "Maybe") return;

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
                        if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")/*repacked_Path + "\\" + */
                            targetFileName = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul91"] == "Yes") targetFileName = Groupss + targetFileName;

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
                                    DeleteDirectory(newDir);
                                    Directory.Move(dir, newDir);
                                }
                                else if (dir.EndsWith(sourceDir1))
                                {
                                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
                                    DeleteDirectory(newDir);
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
                            DeleteDirectory(unpackedDir);
                        }
                        h = chbx_Format == "PS3" ? h.Replace(".", "_").Replace(" ", "_").Replace("/", "") : h;
                        h += (chbx_Format == "PC" ? "_p.psarc" : (chbx_Format == "Mac" ? "_m.psarc" : (chbx_Format == "PS3" ? "_ps3.psarc.edat" : "")));
                    }
                }
                if (((chbx_Last_Packed && chbx_Last_PackedEnabled) && !(chbx_CopyOld && chbx_CopyOldEnabled)) || (!File.Exists(h) || h == ""))
                {
                    DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+\"\\\"+FileName FROM Pack_AuditTrail WHERE Platform=\"" + chbx_Format + "\" and CDLC_ID=" + ID + " ORDER BY ID DESC;", "", cnb);
                    rec = dvr.Tables[0].Rows.Count;
                    if (rec > 0) h = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                if ((!(chbx_Last_Packed && chbx_Last_PackedEnabled) || (chbx_Last_Packed && chbx_Last_PackedEnabled && rec == 0)) && !(chbx_CopyOld && chbx_CopyOldEnabled) || (!File.Exists(h) || h == ""))
                {
                    var i = 0;
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
                        bassRemoved = false;
                        Platform platformz = Folder_Name.GetPlatform();
                        var xmlFilez = Directory.GetFiles(Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFilez)
                        {
                            if (xml.ToLower().IndexOf("showlights") < 0 && xml.ToLower().IndexOf("vocals") < 0)
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
                        cleanlyrics(filez.ID, cnb);
                        string ttt2 = (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && filez.Has_Vocals == "Yes")
                            ? AddStuffToLyrics(filez.ID, filez.Description, Groupss, filez.Has_DD, (filez.Has_BassDD == "Yes") ? "No" : "Yes", filez.Has_BassDD, filez.Author, filez.Is_Acoustic, filez.Is_Live, filez.Live_Details, filez.Is_Multitrack, filez.Is_Original, cnb) : "";
                        string ttt1 = (ConfigRepository.Instance()["dlcm_AdditionalManipul74"]).ToLower() == "Yes".ToLower() && filez.Has_Vocals.ToLower() == "Yes".ToLower()
                            ? AddTrackStart2Lyrics(filez.ID, cnb) : "";
                        if (ttt2 != "" || ttt1 != "") cleanlyrics(filez.ID, cnb);

                        //open lyrics after manipulation
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && ConfigRepository.Instance()["dlcm_AdditionalManipul94"] == "Yes" && filez.Has_Vocals == "Yes")
                        {
                            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType FROM Arrangements WHERE CDLC_ID=" + filez.ID + "", "", cnb);

                            var noOfRec = dus.Tables[0].Rows.Count;
                            var ST = "";
                            for (i = 0; i <= noOfRec - 1; i++)
                            {
                                var XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
                                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                                if (ArrangementType == "Vocal") ST = XMLFilePath;
                            }

                            string filePath = ST;
                            if (ST != null && ST != "") try
                                {
                                    Process process = Process.Start(filePath);
                                }
                                catch (Exception ex)
                                {
                                    var trst = "Error lyrics..." + ex; UpdateLog(DateTime.Now, trst, false, c("dlcm_TempPath"), "", "", null, null);
                                }
                            MessageBox.Show("Are you done with reading the Lyrics file?.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // LOAD DATA
                        timestamp = UpdateLog(timestamp, "Loading song.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, tmpPath, multithreadname, form, null, null);
                        //verify if too many audios
                        var xmlFil = Directory.GetFiles(filez.Folder_Name, "*.wem", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFil) if (xml != filez.AudioPath && xml != filez.audioPreviewPath) DeleteFile(xml);
                        var xmlFi = Directory.GetFiles(filez.Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories);
                        foreach (var xml in xmlFi) if (xml != filez.OggPath && xml != filez.oggPreviewPath) DeleteFile(xml);

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

                            AlbumArtPath = filez.AlbumArtPath,
                            OggPath = filez.AudioPath,
                            OggPreviewPath = ((filez.audioPreviewPath != "") ? filez.audioPreviewPath : filez.AudioPath),
                            Arrangements = info.Arrangements,
                            Tones = info.Tones,
                            TonesRS2014 = info.TonesRS2014,
                            Volume = volume,
                            PreviewVolume = volumep,
                            SignatureType = info.SignatureType
                        };

                        //IF Vocals have been added  but Repack is set not to cosider them
                        if (updateTonesArrangs)
                        {
                            DataSet dbs = new DataSet(); dbs = SelectFromDB("Tones_GearList", "SELECT * FROM Tones_GearList WHERE Tone_ID in (SELECT ID FROM Tones WHERE CDLC_ID=" + ID + ");", "", cnb);
                            var norecx = dbs.Tables.Count > 0 ? dbs.Tables[0].Rows.Count : 0;

                            if (norecx == 0 && info.TonesRS2014.Count != 0) updateTonesArrangs = false;// MessageBox.Show("Vocals not included as added in the DLCManager tool, but Option 76 is Unselected ergo no DLCManager-DB changes are considered at packing");
                            else updateTonesArrangs = true;
                        }//else if (ConfigRepository.Instance()["dlcm_AdditionalManipul76"].ToLower() == "yes") 

                        //IF Vocals have been added  but Repack is set not to cosider them
                        if (!updateTonesArrangs)
                        {
                            DataSet dvs = new DataSet(); dvs = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + " AND ArrangementType=\"Vocal\";", "", cnb);
                            var norec = dvs.Tables.Count > 0 ? dvs.Tables[0].Rows.Count : 0;
                            bool vocalmissing = true;
                            foreach (var arg in info.Arrangements)//, Type
                            {
                                if (arg.ArrangementType.ToString() == "Vocal") vocalmissing = false;
                            }
                            if (norec > 0 && vocalmissing) MessageBox.Show("Vocals not included; as manually added in the DLCManager tool, but Option 76 (use songs changes made in DLCManager) is Unselected ergo no DLCManager-DB changes are considered at packing");
                        }

                        var tz = 0; var j = 0; var jf = 0;
                        try
                        {
                            if (updateTonesArrangs)
                            {
                                //Update Tones
                                var norec = 0;
                                DataSet dfs = new DataSet(); dfs = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + ID + ";", "", cnb);
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
                                                DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Amp\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Cabinet\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal1\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal2\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal3\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PostPedal4\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal1\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal2\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal3\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"PrePedal4\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack1\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack2\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack3\" ORDER BY Type DESC;", "", cnb);
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
                                                DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE Tone_ID=" + TID + " AND Gear_Name=\"Rack4\" ORDER BY Type DESC;", "", cnb);
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
                                    error = true; error_reason = "error updating tones?" + data.SongInfo.Artist + " " + data.SongInfo.SongDisplayName + " " + j + jf + tz + ex;
                                }

                                //Add Arrangements
                                norec = 0;
                                string sds = "";
                                DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID = " + ID + "; ", "", cnb);

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
                                        var a = (sd == "3" ? "-" : (sd == "0" ? "-_" :
                                            (sd == "4" ? "--" : (sd == "1" ? "---" : (sd == "6" ? "----" : "l")))));
                                        data.Arrangements.Add(new Arrangement
                                        {
                                            ArrangementName = (sd == "3" ? ArrangementName.Bass : (sd == "0" ? ArrangementName.Lead :
                                            (sd == "4" ? ArrangementName.Vocals : (sd == "1" ? ArrangementName.Rhythm : (sd == "6" ? ArrangementName.ShowLights
                                            : ArrangementName.Combo))))),// ArrangementName.Vocals,
                                                                         //ArrangementType = ;// ArrangementType.Vocal,
                                                                         //        ScrollSpeed = 20,
                                            SongXml = new SongXML { File = ds.Tables[0].Rows[k].ItemArray[5].ToString() },
                                            //        //SongFile = new SongFile { File = "" },
                                            //        CustomFont = false
                                        }
                                                                );
                                    }
                                }
                                var n = 0;
                                foreach (var arg in info.Arrangements)//, Type
                                {
                                    sds = ds.Tables[0].Rows[n].ItemArray[1].ToString();
                                    //data.Arrangements[n].Name = ArrangementName.Vocals;
                                    //data.Arrangements[n].Name = ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementName.Bass : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Lead" ? RocksmithToolkitLib.Sng.ArrangementName.Lead : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Vocals" ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Rhythm" ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : ds.Tables[0].Rows[n].ItemArray[12].ToString() == "ShowLights" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
                                    data.Arrangements[n].ArrangementName = (sds == "3" ? ArrangementName.Bass : (sds == "0" ? ArrangementName.Lead : (sds == "4" ? ArrangementName.Vocals : (sds == "1" ? ArrangementName.Rhythm : (sds == "6" ? ArrangementName.ShowLights : ArrangementName.Combo)))));
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
                            error = true; error_reason = "error updating arangement?" + data.SongInfo.Artist + " " + data.SongInfo.SongDisplayName + " " + j + jf + tz + ex;
                        }
                        //get track no
                        if ((ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") && netstatus != "NOK")
                        {
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
                                    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmds + ";", cnb);
                                    //ADD STADARDISATION UPDATE
                                    //Updating the Standardization table

                                    var updcmd = "UPDATE Standardization SET SpotifyArtistID=\"" + SpotifyArtistID + "\" , SpotifyAlbumID=\"" + SpotifyArtistID + "\", SpotifyAlbumURL=\""
                                        + SpotifyAlbumURL + "\", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\", Year_Correction=\"" + SpotifyAlbumYear + "\" WHERE (Artist=\"" + info.SongInfo.Artist + "\" OR Artist_Correction=\""
                                        + info.SongInfo.Artist + "\") AND (Album=\"" + info.SongInfo.Album + "\" OR Album_Correction=\"" + info.SongInfo.Album + "\")";

                                    UpdateDB("Standardization", updcmd + ";", cnb);
                                }
                            }
                            catch (Exception ex) { var tust = "Spotify Error ..." + ex; UpdateLog(DateTime.Now, tust, false, c("dlcm_TempPath"), multithreadname, form, null, null); }
                        }

                        //Gather song Lenght
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
                                    cmd = "SELECT ID, AudioPath, audioBitrate, audioSampleRate, audioPreviewPath, Folder_Name, OggPath, oggPreviewPath FROM Main WHERE ";
                                    cmd += "ID=" + ID + "";
                                    if (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes"
                                        && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture)
                                        ||
                                        ConfigRepository.Instance()["dlcm_AdditionalManipul88"] == "Yes"
                                        && float.Parse(PreviewLenght, NumberStyles.Float, CultureInfo.CurrentCulture)
                                        < float.Parse(ConfigRepository.Instance()["dlcm_MinPreviewLenght"], NumberStyles.Float, CultureInfo.CurrentCulture)
                                        && info.OggPreviewPath != null) DeleteFile(info.OggPreviewPath);
                                    FixMissingPreview(cmd, cnb, AppWD, null, null, false, windw);
                                }

                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && info.OggPath != null)
                                {
                                    {
                                        cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, OggPath, oggPreviewPath  FROM Main " +
                                            "WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                        cmd += " AND ID=" + ID;
                                        FixAudioIssues(cmd, cnb, AppWD, null, null, false, windw);
                                    }
                                }

                            }
                            if (d1.Split(';')[2] == "1") { System.Windows.Forms.Application.Exit(); }
                        }
                        //Fix bug
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes")
                        {
                            GenericFunctions.Converters(filez.oggPreviewPath, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
                            if (File.Exists(filez.oggPreviewPath.Replace(".ogg", ".wem")))
                            {
                                //fix as sometime the template folder gets poluted and breaks eveything
                                var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                var templateDir = Path.Combine(appRootDir, "Template");
                                var backup_dir = AppWD + "\\Template";
                                DeleteDirectory(templateDir);
                                CopyFolder(backup_dir, templateDir);
                            }
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_fixed.wav"));
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview_fixed.wav"));
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview_fixed.ogg"));
                            //tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul71"] == "Yes")
                        {
                            //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
                            //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
                            //tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }


                        data.ToolkitInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo
                        {
                            PackageAuthor = filez.Author
                        };
                        SongRecord[0].Author = filez.Author;

                        SongRecord[0].Groups = Groupss;

                        DirectoryInfo di;
                        var repacked_Path = TempPath + "\\0_repacked";
                        if (!Directory.Exists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

                        //manipulating the info
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul23"] == "Yes" && data.SongInfo.ArtistSort.Length > 4) //21.Pack with The/ Die only at the end of Title Sort 
                        {
                            if (ConfigRepository.Instance()["dlcm_AdditionalManipul21"] == "Yes" && data.SongInfo.SongDisplayNameSort.Length > 4)
                            {
                                data.SongInfo.SongDisplayNameSort = MoveTheAtEnd(data.SongInfo.SongDisplayNameSort);
                                SongRecord[0].Song_Title_Sort = data.SongInfo.SongDisplayNameSort;
                            }
                            data.SongInfo.ArtistSort = MoveTheAtEnd(data.SongInfo.ArtistSort);
                            SongRecord[0].Artist_Sort = data.SongInfo.ArtistSort;
                            data.SongInfo.AlbumSort = MoveTheAtEnd(data.SongInfo.AlbumSort);
                            SongRecord[0].Album_Sort = data.SongInfo.AlbumSort;
                        }

                        if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") data.SongInfo.SongDisplayName = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false);
                        if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") data.SongInfo.SongDisplayNameSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true);
                        if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") data.SongInfo.Artist = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, false);
                        if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true);
                        if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") data.SongInfo.Album = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true);
                        if (ConfigRepository.Instance()["dlcm_Activ_AlbumSort"] == "Yes") data.SongInfo.AlbumSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album_Sort"], 0, false, false, bassRemoved, SongRecord, "[", "]", chbx_Beta, true);

                        var no_ord = 1;
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul1"] == "Yes")
                            data.SongInfo.SongDisplayName = no_ord + " " + data.SongInfo.SongDisplayName;

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
                            FN = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "", "", chbx_Beta, true);//, ConfigRepository.Instance()["dlcm_AdditionalManipul87"], ConfigRepository.Instance()["dlcm_AdditionalManipul88"]);
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

                        UpdateLog(DateTime.Now, "Metadata:\n" +
                        "SongDisplayName: " + data.SongInfo.SongDisplayName +
                        "\nSongDisplayNameSort: " + data.SongInfo.SongDisplayNameSort +
                        "\nArtist: " + data.SongInfo.Artist +
                        "\nArtistSort: " + data.SongInfo.ArtistSort +
                        "\nAlbum: " + data.SongInfo.Album +
                        "\nAlbumSort: " + data.SongInfo.AlbumSort +
                        "\nFile Name: " + FN
                        , false, c("dlcm_TempPath"), "", "", null, null);

                        data.ToolkitInfo.PackageVersion = filez.Version;

                        int progress = 0;
                        var errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        numPlatforms++;

                        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                        timestamp = UpdateLog(timestamp, "Packing" + PreviewLenght, true, tmpPath, multithreadname, form, null, null);

                        //check if already packed
                        if (c("dlcm_AdditionalManipul98") == "Yes" && form != "MainDB")
                        {
                            DataSet dvr = new DataSet(); if (chbx_PC == "PC") dvr = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Pc\"", "", cnb);
                            if (dvr.Tables.Count > 0) if (dvr.Tables[0].Rows.Count > 0) chbx_PC = "";
                            DataSet dvd = new DataSet(); if (chbx_Mac == "Mac") dvd = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"Mac\"", "", cnb);
                            if (dvd.Tables.Count > 0) if (dvd.Tables[0].Rows.Count > 0) chbx_Mac = "";
                            DataSet dvx = new DataSet(); if (chbx_PS3 == "PS3") dvx = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE CDLC_ID=" + filez.ID + " AND Platform =\"PS3\"", "", cnb);
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
                                var tgst = "Error generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason = "@PC pack";
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
                                error_reason = "@Mac pack";
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
                                var tgst = "Error at xbox generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason = "@XBOX pack";
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
                                var tgst = "Error @ps3generate..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                                error = true;
                                error_reason = "@PS3 pack" + ex;
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
                    timestamp = UpdateLog(timestamp, "Start the Copy/FTPing process", true, tmpPath, multithreadname, form, null, null);

                    //calc hash and file size
                    System.IO.FileInfo fi = null;
                    try
                    {
                        var platfrm = "_ps3";
                        if (chbx_PS3 == "PS3" && chbx_Copy)
                        {
                            h = h.Replace("\\0_repacked\\PC", "\\0_repacked\\PS3").Replace("\\0_repacked\\Mac", "\\0_repacked\\PC").Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC");
                            source = h.IndexOf("_ps3.psarc.edat") <= 0 ? h + "_ps3.psarc.edat" : h; fi = new System.IO.FileInfo(source);
                            if (fi.Length == 0 || !File.Exists(source))
                            {
                                error = true; error_reason = "ps3 filesize zero";
                            }
                            var u = ""; var a = "";

                            if (File.Exists(source))
                            {
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul92"] == "Yes")
                                {
                                    var TrueGameFldr = Path.Combine(AppWD, "TrueAncestor_PKG_Repacker_v2.45\\game\\") + c("dlcm_" + c("dlcm_MainDBFormat").Replace("PS3_", "FTP")).Substring(33, 9);
                                    //copy edat in DLC folder
                                    var dst = TrueGameFldr + "\\USRDIR\\DLC\\" + Path.GetFileName(source);
                                    if (File.Exists(source) && dst.Length < 256) File.Copy(source, dst, true);
                                }
                                else
                                {
                                    dest = txt_FTPPath;
                                    if (chbx_Replace) u = DeleteFTPedSongs(txt_RemotePath, dest, cnb, txt_DLC_ID, ftpstatus);
                                    a = FTPFile(txt_FTPPath, source, TempPath, SearchCmd, ID, cnb, ftpstatus);
                                    copyftp = (" and " + a + " FTPed(PS3)").Replace("  ", " ");
                                }
                            }
                            else
                            {
                                error = true; error_reason = "@FTP";
                            }


                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest + fi.Name;
                            if (!error)
                                Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_FTPPath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PS3, packid, a);
                        }
                        platfrm = "_p"; copiedpath = "";
                        if (chbx_PC == "PC" && chbx_Copy)
                        {
                            source = h.IndexOf("_p.psarc") <= 0 ? h.Replace("\\0_repacked\\PS3", "\\0_repacked\\PC").Replace("\\0_repacked\\Mac", "\\0_repacked\\PC").Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC") + platfrm + ".psarc" : h;
                            fi = new System.IO.FileInfo(source);
                            if (fi.Length == 0 || !File.Exists(source))
                            {
                                error = true; error_reason = "chbx_PC filesize zero";
                            }
                            dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));

                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest.Replace(Path.GetDirectoryName(dest), File.Exists(c("general_rs2014path")) && c("general_rs2014path").IndexOf(":\\") >= 0 ? c("general_rs2014path") : c("dlcm_PC"));
                            if (!error)
                                copyftp += " and " + Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_RemotePath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_PC, packid, "") //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                 + " Copied(PC)";
                        }
                        platfrm = "_m"; copiedpath = "";
                        if (chbx_Mac == "Mac" && chbx_Copy)
                        {
                            source = h.IndexOf("_m.psarc") <= 0 ? h.Replace("\\0_repacked\\XBOX360", "\\0_repacked\\PC").Replace("\\0_repacked\\PS3", "\\0_repacked\\Mac").Replace("\\0_repacked\\PC", "\\0_repacked\\Mac") + platfrm + ".psarc" : h;
                            fi = new System.IO.FileInfo(source);
                            if (fi.Length == 0 || !File.Exists(source))
                            {
                                error = true; error_reason = "mac filezise zero";
                            }
                            dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));

                            ////Add Pack Audit Trail
                            if (!(chbx_CopyOld && chbx_CopyOldEnabled && needRebuildPackage) && !(chbx_Last_Packed && chbx_Last_PackedEnabled))
                                copiedpath = dest.Replace(Path.GetDirectoryName(dest), File.Exists(c("general_rs2014path")) ? c("general_rs2014path") : c("dlcm_Mac"));
                            if (!error)
                                copyftp += " and " + Add2Pack(multithreadname, form, platfrm, chbx_Replace, chbx_ReplaceEnabled, txt_RemotePath, source, copiedpath, cnb, fi, ID, DLC_Name, chbx_Mac, packid, "") //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                + " Copied(Mac)";
                        }
                        timestamp = UpdateLog(timestamp, "Stop the Copy/FTPing process", true, tmpPath, multithreadname, form, null, null);
                    }
                    catch (Exception ex)
                    {
                        var tgst = "Error after packing at ftping..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), multithreadname, form, null, null);
                        error = true; error_reason = tgst;
                    }
                }
                else
                {
                    error = true; error_reason = "Packed path is empty";
                }
                //Add Pack Audit Trail

                cnb.Close();
            }
            catch (Exception ex)
            {
                error = true; error_reason = "overall?" + ex;
            }

            //Restore XML changes
            var xmlFilex = Directory.Exists(Folder_Name) ? Directory.GetFiles(Folder_Name, "*.old", System.IO.SearchOption.AllDirectories) : null;
            if (xmlFilex != null) foreach (var xml in xmlFilex)
                {
                    if (xml.ToLower().IndexOf("showlights") < 0)
                        try
                        {
                            File.Copy(xml, xml.Replace(".old", ""), true);
                            timestamp = UpdateLog(timestamp, "Restore XML", true, tmpPath, multithreadname, form, null, null);
                            DeleteFile(xml);
                        }
                        catch (Exception ex)
                        {
                            var tgst = "Error at restoring xml changes..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null);
                        }
                }

            if (error)
            {
                e.Cancel = true;
                UpdatePackingLog("LogPackingError", ConfigRepository.Instance()["dlcm_DBFolder"], packid.ToInt32(), args[0], error_reason, cnb);
                timestamp = UpdateLog(timestamp, "End Packing", true, tmpPath, multithreadname, form, null, null);
            }
            else
                UpdatePackingLog("LogPacking", ConfigRepository.Instance()["dlcm_DBFolder"], packid.ToInt32(), args[0], "", cnb);

            e.Cancel = true;
            e.Result = "done";
            ConfigRepository.Instance()["dlcm_GlobalTempVariable"] = "Yes";
            return;
        }

        static public string Add2Pack(string multithreadname, string form, string platfrm, bool chbx_Replace, bool chbx_ReplaceEnabled, string txt_RemotePath,
            string source, string dest, OleDbConnection cnb, System.IO.FileInfo fi, string ID, string DLC_Name, string chbx_Format, string pack, string ftped)
        {
            var FileHash = GetHash(source);//Generating the HASH code

            DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);

            var norec = 0;
            norec = dfs.Tables[0].Rows.Count;
            if (norec == 0)
            {
                var sourcedir = source.Replace(Path.GetFileName(source), "");
                string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, CDLC_ID, DLC_Name, Platform, Pack, FTPed";
                var insertA = "\"" + dest + "\",\"" + sourcedir.Remove(sourcedir.Length - 1) + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Today.ToString()
                    + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + ID + ",\"" + DLC_Name + "\",\"" + chbx_Format + "\",\"" + pack + "\"" +
                    ",\"" + (ftped.IndexOf("Truely") >= 0 ? "Yes" : "No") + "\"";

                InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
            }

            ///Update pack id
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Pack = \"" + pack + "\" WHERE ID=" + ID + ";", cnb);

            ///copy mac&pc
            var copyftp = "";
            if (platfrm != "_ps3")
                try
                {
                    DataSet dgr = new DataSet(); dgr = UpdateDB("Main", "Update Main Set Remote_path = \"" + dest + "\" WHERE ID=" + ID + ";", cnb);
                    if (chbx_Replace && File.Exists(txt_RemotePath) && !File.Exists(txt_RemotePath.Replace(platfrm + ".psarc", ".old")))
                        DeleteCOPYedSongs(txt_RemotePath, txt_RemotePath.Replace(platfrm + ".psarc", ".old"), cnb, ID, platfrm);
                    File.Copy(@source, @dest, true);
                    copyftp = "true";

                    DataSet dcs = new DataSet(); dcs = SelectFromDB("Pack_AuditTrail", "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);
                    DataSet dvr = new DataSet(); dvr = UpdateDB("Pack_AuditTrail", "Update Pack_AuditTrail Set FTPed = \"Yes\" WHERE ID=" + dcs.Tables[0].Rows[0][0].ToString() + ";", cnb);
                }
                catch (Exception ex)
                {
                    copyftp = "Not"; var tgst = "Error @copy after pack..." + ex; UpdateLog(DateTime.Now, tgst, false,
                    c("dlcm_TempPath"), multithreadname, form, null, null);
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

            DeleteDirectory(TrueGameFldr);

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
            if (exp.ToLower().IndexOf("e-") > 0) exp = exp.Substring(0, exp.ToLower().IndexOf("e-"));
            if (exp.ToLower().IndexOf("e+") > 0) exp = exp.Substring(0, exp.ToLower().IndexOf("e+"));
            return exp;
        }

        public static string GetTrackStartTime(string SongXml, string MaskRoute, string ArrangementType)
        {
            Song2014 xmlContent = null;
            Vocals xmlVocals = null;
            var startt = "";
            if (MaskRoute == "Rhythm" || MaskRoute == "Lead" || MaskRoute == "Bass")
            {
                try
                {
                    xmlContent = Song2014.LoadFromFile(SongXml);
                    startt = xmlContent.Levels[0].Notes[0].Time.ToString();
                }
                catch (Exception ex)
                {
                    var tsst = "No Levels only Sections..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    startt = xmlContent.Sections[0].StartTime.ToString();
                }
            }
            if (ArrangementType == "Vocal")
            {
                try
                {
                    xmlVocals = Vocals.LoadFromFile(SongXml);
                    startt = xmlVocals.Vocal[0].Time.ToString();
                }
                catch (Exception ex) { var tsst = "Error @starttimevocals..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }
            }
            return startt;
        }

        public static void cleanlyrics(string SongID, OleDbConnection cnb)
        {
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            var noOfRec = dus.Tables[0].Rows.Count;
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
                    xmlContent = Vocals.LoadFromFile(XMLFilePath);
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
                }
                catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", null, null); }

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
                DeleteFile(XMLFilePath + ".newvcl");

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
                DeleteFile(XMLFilePath + ".newvcl");
            }
            return;
        }

        public static string GetHashCleanXML(string filename)
        {
            var r = "";
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
                DeleteFile(filename + ".newvcl");
            }
            catch (Exception ex)
            {
                var timestamp = UpdateLog(DateTime.Now, " Error at clening xml" + ex, true, c("dlcm_TempPath"), "", "MainDB", null, null);
            }
            return r;
        }
        public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb)
        {
            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time, Bonus, Part FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            var noOfRec = dus.Tables[0].Rows.Count;
            var XMLFilePath = ""; var j = 0; var i = 0;
            for (i = 0; i <= noOfRec - 1; i++)
            {
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                if (ArrangementType == "Vocal") XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
            }

            Vocals newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(XMLFilePath);
            }
            catch (Exception ex) { var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, c("dlcm_TempPath"), "", "", null, null); }
            int note1 = newLyrics.Vocal[0].Note;

            for (i = 0; i <= noOfRec - 1; i++)
            {
                var RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                if (RouteMask == "Lead" || RouteMask == "Bass" || RouteMask == "Rhythm")
                    Add2LinesInVocals(XMLFilePath, 1, "0.001", note1); //per each arrangement add an empty line
            }

            var maxL = int.Parse(ConfigRepository.Instance()["dlcm_MaxLyricLenght_PS3"]);

            Vocals xmlContent = null;
            if (XMLFilePath != "")
                try
                {
                    xmlContent = Vocals.LoadFromFile(XMLFilePath);
                    for (i = 0; i <= noOfRec - 1; i++)
                    {
                        var changeaplied = false;
                        var insertedinlyric = 0;

                        var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                        var RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                        var strartt = dus.Tables[0].Rows[i].ItemArray[3].ToString();
                        var Part = dus.Tables[0].Rows[i].ItemArray[5].ToString();
                        var b = dus.Tables[0].Rows[i].ItemArray[4].ToString() == "True" ? "b" : "";
                        var p = noOfRecP > 1 ? Part : "";
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

        public static string AddStuffToLyrics(string SongID, string Comments, string Group, string Has_DD, string bassRemoved, string Has_BassDD, string Author, string Is_Acoustic, string Is_Live, string Live_Details, string Is_Multitrack, string Is_Original, OleDbConnection cnb)
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

            DataSet dup = new DataSet(); dup = SelectFromDB("Arrangements", "SELECT Max(Part) FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            var noOfRecP = dup.Tables.Count > 0 ? (string.IsNullOrEmpty(dup.Tables[0].Rows[0].ItemArray[0].ToString()) ? 0 : int.Parse(dup.Tables[0].Rows[0].ItemArray[0].ToString())) : 0;

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask, Start_Time, Part FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
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
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"]
                + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source="
                + ConfigRepository.Instance()["dlcm_DBFolder"]);
            do
                System.Threading.Thread.Sleep(1000);
            while (cnb.State.ToString() == "Connecting");
            cnb.Open();

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
                    DataSet dios = new DataSet(); dios = UpdateDB("Main", cmd + ";", cnb);

                    //Update Preview
                    if (audioPreviewPath != null && audioPreviewPath != "")
                    {
                        if (File.Exists(audioPreviewPath)) Downstream(audioPreviewPath, bitrate, windw);
                        audio_hash = GetHash(audioPreviewPath);
                        cmd = "UPDATE Main SET ";
                        cmd += "audioPreview_Hash=\"" + audio_hash;
                        cmd += "\" WHERE ID=" + ID;
                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                        DeleteFile(AudioPath.Replace(".wem", "_preview.wem") + ".orig");
                        DeleteFile(AudioPath.Replace(".wem", "_preview_fixed.ogg") + ".orig");
                    }
                    //Delete any Wav file created..by....?ccc
                    foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(AudioPath), "*.wav", System.IO.SearchOption.AllDirectories))
                    {
                        DeleteFile(wav_name);
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
                DeleteFile(AudioPath + ".origi");
            else
                File.Move(AudioPath + ".origi", AudioPath);
            if (File.Exists(audioPreviewPath))
                DeleteFile(audioPreviewPath + ".origi");
            else
                File.Move(audioPreviewPath + ".origi", audioPreviewPath);
            if (File.Exists(oggPath))
                DeleteFile(oggPath + ".origi");
            else
                File.Move(oggPath + ".origi", oggPath);
            if (File.Exists(oggPreviewPath))
                DeleteFile(oggPreviewPath + ".origi");
            else
                File.Move(oggPreviewPath + ".origi", oggPreviewPath);

            if (err != "")
            {
                var cmdupd = "UPDATE Main Set FilesMissingIssues =\"Issues at audiofix" + err + "\" WHERE ID =" + ID;
                DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb);
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
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            cnb.Open();

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
            else return;
            if (File.Exists(OggPath)) File.Copy(OggPath, OggPath + ".orig", true);
            else return;
            if (File.Exists(OggPreviewPath)) File.Copy(OggPreviewPath, OggPreviewPath + ".orig", true);
            else return;

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
                    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                    if (File.Exists(t))
                        using (var DDC = new Process())
                        {
                            tsst = "Cut Ogg for preview ..." + OggPath; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, "", null, null);
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            if (DDC.ExitCode == 0)
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
                                if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath);
                                tsst = "Convert to wem preview ..."; UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                                var i = 0;
                                do
                                {
                                    GenericFunctions.Converters(tt, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
                                    i++;
                                    if (!File.Exists(tt.Replace(".ogg", ".wem")))
                                    {
                                        //fix as sometime the template folder gets poluted and breaks eveything
                                        var appRootDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                                        var templateDir = Path.Combine(appRootDir, "Template");
                                        var backup_dir = AppWD + "\\Template";
                                        DeleteDirectory(templateDir);
                                        CopyFolder(backup_dir, templateDir);
                                    }
                                }
                                while (!File.Exists(tt.Replace(".ogg", ".wem")) && i < 10);
                                if (File.Exists(tt.Replace(".ogg", ".wav"))) DeleteFile(tt.Replace(".ogg", ".wav"));
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

                            var rrr = true;
                            try
                            {
                                File.Copy(t, g, true);
                                File.Copy(tt, gg, true);
                                rrr = false;
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
                        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                    }
                    //Delete any Wav file created..by....?ccc
                    foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(OggPath), "*.wav", System.IO.SearchOption.AllDirectories))
                    {
                        DeleteFile(wav_name);
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
                DeleteFile(AudioPath + ".orig");
            else
                File.Move(AudioPath + ".orig", AudioPath);
            if (File.Exists(audioPreviewPath))
                DeleteFile(audioPreviewPath + ".orig");
            else
                File.Move(audioPreviewPath + ".orig", audioPreviewPath);
            if (File.Exists(OggPath))
                DeleteFile(OggPath + ".orig");
            else
                File.Move(OggPath + ".orig", OggPath);
            if (File.Exists(tr))
                DeleteFile(tr + ".orig");
            else
                File.Move(tr + ".orig", tr);

            if (err != "")
            {
                var cmdupd = "UPDATE Main Set FilesMissingIssues =\"Issues at audiofix" + err + "\" WHERE ID =" + ID;
                DataSet dus = new DataSet(); dus = UpdateDB("Main", cmdupd + ";", cnb);
            }
            e.Result = "done";
        }

        //not used (anymore?)
        public static string FixOggwDiffName(string OggPreviewPath, string Folder_Name, System.DateTime timestamp, string tsst, string logPath, string tmpPath, string multithreadname, string windw)
        {
            var previewN = OggPreviewPath == null ? null : ((File.Exists(OggPreviewPath.ToString())) ? OggPreviewPath.ToString().Replace(".wem", "_fixed.ogg") : null);
            if (!File.Exists(previewN))
            {
                foreach (string preview_name in Directory.GetFiles(Folder_Name, "*_preview.wem", System.IO.SearchOption.AllDirectories))
                {
                    foreach (string file_name in Directory.GetFiles(Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories))
                    {
                        if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
                        {
                            var tl = previewN;
                            var hg = preview_name;
                            previewN = preview_name.Replace(".wem", "fixed.ogg");
                            if (!File.Exists(previewN))
                            {
                                try
                                {
                                    tsst = "Fix _preview.OGG having a diff name than _preview.wem after oggged ..." + Path.GetFileName(file_name) + "-" + Path.GetFileName(previewN); UpdateLog(timestamp, tsst, false, tmpPath, multithreadname, windw, null, null);
                                    File.Copy(file_name, previewN, true);
                                    DeleteFile(file_name);
                                }
                                catch (Exception ee)
                                {
                                    timestamp = UpdateLog(timestamp, "FAILED1 FixOggwDiffName" + ee.Message + "----" + file_name + "\n -" + previewN + "\n -" + file_name + ".ogg", true, "", "", windw, null, null);
                                    Console.WriteLine(ee.Message);
                                }
                            }
                        }
                    }
                }
            }
            return previewN;
        }
        public static void FixAudioIssues(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw)
        {

            BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
            bwRFixAudio.DoWork += new DoWorkEventHandler(FixBitrate);
            bwRFixAudio.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRFixAudio.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            if (cancel)
            { if (bwRFixAudio.WorkerSupportsCancellation == true) bwRFixAudio.CancelAsync(); }// Cancel the asynchronous operation.
            else
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd, "", cnb); var noOfRec = dhs.Tables.Count == 0 ? 0 : dhs.Tables[0].Rows.Count;
                for (var i = 0; i <= noOfRec - 1; i++)
                {
                    if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = i; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }
                    var ID = dhs.Tables[0].Rows[i].ItemArray[0].ToString();
                    var AudioPath = dhs.Tables[0].Rows[i].ItemArray[1].ToString();
                    float bitrate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[2].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                    float SampleRate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[3].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
                    var audioPreviewPath = dhs.Tables[0].Rows[i].ItemArray[4].ToString();
                    var oggPath = dhs.Tables[0].Rows[i].ItemArray[5].ToString();
                    var oggPreviewPath = dhs.Tables[0].Rows[i].ItemArray[6].ToString();

                    if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                    var tst = "AudioFixing: " + i + "/" + noOfRec + " " + AudioPath; var timestamp = UpdateLog(DateTime.Now, tst, true, c("dlcm_TempPath"), "", windw, pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var args = cmd.Replace(";", "") + ";" + AudioPath + ";" + bitrate + ";" + SampleRate + ";" + ID + ";" + audioPreviewPath
                        + ";" + oggPath + ";" + oggPreviewPath + ";" + i + ";" + windw;
                    bwRFixAudio.RunWorkerAsync(args);
                    do
                        System.Windows.Forms.Application.DoEvents();
                    while (bwRFixAudio.IsBusy);//keep singlethread as toolkit not multithread abled
                }
            }
        }

        public static int FixMissingPreview(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel, string windw)
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
                DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb); noOfRec = dhxs.Tables.Count == 0 ? 0 : dhxs.Tables[0].Rows.Count;
                if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }

                for (var j = 0; j <= noOfRec - 1; j++)
                {
                    var ID = dhxs.Tables[0].Rows[j].ItemArray[0].ToString();
                    var OggPath = dhxs.Tables[0].Rows[j].ItemArray[1].ToString();
                    var bitrate = dhxs.Tables[0].Rows[j].ItemArray[2];
                    var SampleRate = dhxs.Tables[0].Rows[j].ItemArray[3];
                    var OggPreviewPath = dhxs.Tables[0].Rows[j].ItemArray[4].ToString();
                    var Folder_Name = dhxs.Tables[0].Rows[j].ItemArray[5].ToString();
                    var AudioPath = dhxs.Tables[0].Rows[j].ItemArray[6].ToString();
                    var audioPreviewPath = dhxs.Tables[0].Rows[j].ItemArray[7].ToString();

                    if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;

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

        public static string Check4MultiT(string origFN, string noMFN, string text)
        {
            var FN = origFN.ToLower();
            var ST = noMFN.ToLower();
            text = text.ToLower();
            var retrn = "";
            if (origFN.ToLower().IndexOf(text) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || origFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0
                || noMFN.ToLower().IndexOf(text) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "_")) > 0 || noMFN.ToLower().IndexOf(text.Replace(" ", "-")) > 0)
            {
                noMFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(noMFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                origFN = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(origFN, text.Replace(" ", ""), "", RegexOptions.IgnoreCase), text, "", RegexOptions.IgnoreCase), text.Replace(" ", "_"), "", RegexOptions.IgnoreCase), text.Replace(" ", "-"), "", RegexOptions.IgnoreCase);
                retrn = noMFN + ";" + ((FN != origFN || noMFN != ST) ? "Yes" : "No");
                return retrn;
            }
            retrn = noMFN + ";" + "No";
            return retrn;
        }

        public static string GetTranslation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb
        , System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, string Artist, string Album, string Year)
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
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd1, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb);
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
            , System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs, string Artist, string Album, string Year, string ArtPath, string artist_c, string album_c)
        // For 1 song
        // Select all Corrected Arstist OR Album OR Year
        {
            var tst = "Running Translation_And_Correction..."; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            var cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + "\"," : "")
                + (album_c != "" ? " Album = \"" + album_c + "\"," : "") + (ArtPath != "" ? " AlbumArtPath = \"" + ArtPath + "\"," : "")
                + (Year != "" ? " Album_Year = \"" + Year + "\"," : "");
            cmd1 += " Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + Artist + "\" AND Album=\"" + Album + "\"";
            var dus = UpdateDB("Main", cmd1, cnb);

            cmd1 = "UPDATE Standardization SET "
+ (Year != "" ? " Year_Correction = \"" + Year + "\"" : "Year_Correction =Year_Correction");
            cmd1 += " WHERE (Artist=\"" + Artist + "\" or Artist_Correction=\"" + Artist + "\") AND (Album=\"" + Album + "\" or Album_Correction=\"" + Album + "\")";

            var dis = UpdateDB("Standardization", cmd1, cnb);

            cmd1 = "UPDATE Standardization SET "
              + (artist_c != "" ? "Artist_Correction = \"" + artist_c + "\"" : "")
        + (album_c != "" ? (artist_c != "" ? "," : "") + " Album_Correction = \"" + album_c + "\"" : "") + (ArtPath != "" ? (artist_c != "" || album_c != "" ? "," : "") + " AlbumArtPath_Correction = \"" + ArtPath + "\"" : "");
            cmd1 += " WHERE (Artist=\"" + Artist + "\") AND (Album=\"" + Album + "\")";
            var dxxs = UpdateDB("Standardization", cmd1, cnb);

            return (Artist + ";" + Album + ";" + Year);
        }

        public static void Translation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb, System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs)
        // Select only Corrected Arstist OR Album OR Cover combination
        // For Each Corrected Record build up an Update sentence
        // Insert any translation if not already existing
        {
            var tst = "Running Translation_And_Correction..."; var timestamp = DateTime.Now; timestamp = UpdateLog(timestamp, tst, true, c("dlcm_TempPath"), "", "DLCManager", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

            //Make sure no Albbum & Artist are blank
            var cmd1 = "UPDATE Standardization SET Artist = \"xxx\" WHERE Artist is null";
            var gdus = UpdateDB("Standardization", cmd1, cnb);
            var cmd2 = "UPDATE Standardization SET Album = \"xxx\" WHERE Album is null";
            var gdud = UpdateDB("Standardization", cmd2, cnb);

            //Multiply
            //tst = "Apply Already existing translations"; UpdateLog(DateTime.Now, tst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            //Standardization.ApplyExistingTranlations(cnb);

            cmd1 = "SELECT * FROM Standardization WHERE Artist_Correction <> \"\" or Album_Correction <> \"\"  order by id;";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var albumyear_c = "";
            var DB_Path = dbp;
            int aa = 0;
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd1, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb);
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = 11; var cnt = 0;
            var tsst = "1/11 Applying " + norec + "corrections"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);

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
                dus = UpdateDB("Main", cmd1, cnb);

                pB_ReadDLCs.Increment(1);
            }

            //insert any translation if not already existing
            tsst = "2/11 insert any translation if not already existing"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            //var insertcmdd = "Artist, Album";
            //var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
            //    ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN" +
            //    " FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1)" +
            //    " AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND" +
            //    " (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND" +
            //    " ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0)" +
            //    " AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            //DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", insertvalues, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            //InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb, 0);

            DataSet dgs = new DataSet(); dgs = SelectFromDB("Main", "SELECT distinct Artist, Album FROM Main ORDER BY Artist", "", cnb);
            var noOfRec = dgs.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
            DataSet dg = new DataSet(); dg = SelectFromDB("Main", "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
                ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S ORDER BY Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])", "", cnb);
            var noOfRecz = dg.Tables.Count == 0 ? 0 : dg.Tables[0].Rows.Count;
            var found = false; var album = ""; var artist = ""; var tz = 0;
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
                        InsertIntoDBwValues("Standardization", "Artist, Album", "\"" + dgs.Tables[0].Rows[l].ItemArray[0].ToString() + "\",\"" + dgs.Tables[0].Rows[l].ItemArray[1].ToString() + "\"", cnb, 0);
                }

            tsst = "3/11 Cleans out duplicates (prev" + tz + ")"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            //var cmd3 = "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)";
            //DeleteFromDB("Groups", cmd3, cnb); //Cleans out duplicates
            DataSet dr = new DataSet(); dr = SelectFromDB("Main", "SELECT distinct Artist, Album, Artist_Correction, Album_Correction,ID FROM Standardization ORDER BY Artist", "", cnb);
            noOfRec = dr.Tables.Count == 0 ? 0 : dgs.Tables[0].Rows.Count;
            //DataSet dc = new DataSet(); dc = SelectFromDB("Main", "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN" +
            //    ", (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S ORDER BY Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])", "", cnb);
            //noOfRecz = dc.Tables.Count == 0 ? 0 : dc.Tables[0].Rows.Count;
            //var found = false; var album = ""; var artist = ""; var tz = 0;
            var IDs = ""; tz = 0;
            if (noOfRec > 0)
                for (var l = 0; l < noOfRec; l++)
                    for (var v = l + 1; v < noOfRecz; v++)
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
                DeleteFromDB("Groups", "Delete * from Standardization WHERE ID IN (" + (IDs.Substring(0, IDs.Length - 2)) + ")", cnb); //Cleans out duplicates

            //Apply Artist Short Name
            tsst = "4/11 Apply Artist Short Name (prev:" + tz + ""; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.ApplyArtistShort(cnb);

            //Apply Album Short Name    
            tsst = "5/11 Apply Album Short Name"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.ApplyAlbumShort(cnb);

            //Multiply Spotify
            tsst = "6/11 Multiply Spotify"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.MultiplySpotify(cnb);

            //Multiply Cover
            tsst = "7/11 Multiply Cover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.ApplyDefaultCover(cnb);

            //Apply DefaultCover
            tsst = "8/11 Apply DefaultCover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.MakeCover(cnb);

            //Apply Artist Auto Group
            tsst = "9/11 Apply Artist Auto Group"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.ApplyArtistAutoGroup(cnb);

            //Apply YearCorrection
            tsst = "10/11 Multiply 1st and apply Year Correction"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            Standardization.MultiplyAndApplyYear(cnb);

            tsst = "11/11 Finished applying Standardization"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs); pB_ReadDLCs.Increment(1);
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + cnt + ")");
        }

        public static void CopyFolder(string copy_dir, string destination_dir)
        {
            if (!Directory.Exists(copy_dir)) return;
            foreach (string dir in Directory.GetDirectories(copy_dir, "*", System.IO.SearchOption.AllDirectories))
            {
                try
                { Directory.CreateDirectory(destination_dir + dir.Substring(copy_dir.Length)); }
                catch (Exception r)
                {
                    var timestamp = UpdateLog(DateTime.Now, "error at copy folder folder creation" + r, true, c("dlcm_TempPath"), "", "", null, null);
                }
            }

            foreach (string file_name in Directory.GetFiles(copy_dir, "*.*", System.IO.SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(file_name, destination_dir + file_name.Substring(copy_dir.Length), true);
                }
                catch (Exception d)
                {
                    var timestamp = UpdateLog(DateTime.Now, "error at copy folder copy file" + d, true, c("dlcm_TempPath"), "", "", null, null);
                }
            }
        }

        public async Task<string> YoutubeRun(MainDBfields SongRecord, int i, OleDbConnection cnb, string windw)
        {
            var ybAddress = SongRecord.YouTube_Link; //original song
            var ybSAddress = SongRecord.Youtube_Playthrough; //generic playthrough
            var ybLAddress = "-"; //Lead
            var ybBAddress = "-"; //Bass
            var ybRAddress = "-"; //Rhythm
            var ybCAddress = "-"; //Combo

            var scmd = "SELECT PlaythroughYBLink, RouteMask, Bonus FROM Arrangements WHERE CDLC_ID=" + SongRecord.ID + ";";
            DataSet dnss = new DataSet(); dnss = SelectFromDB("Arrangements", scmd, "", cnb);
            var norecs = dnss.Tables.Count == 0 ? 0 : dnss.Tables[0].Rows.Count;
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
                result.Append(new String('O', 4 - result.Length));
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

        public static async Task<string> GetYoutubeDetailsAsync(MainDBfields SongRecord, int i, OleDbConnection cnb, ProgressBar pB_ReadDLCs, string windw)
        {
            string yAddress = null;
            try
            {

                yAddress = await new GenericFunctions().YoutubeRun(SongRecord, i, cnb, windw);//.Result;//.Wait();
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
                    dos = UpdateDB("Main", cmdz + ";", cnb);

                if (SongRecord.ID != null || SongRecord.ID != null)
                {
                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybLAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Lead\"";
                    dos = new DataSet();
                    if (ybLAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybRAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Rhythm\"";
                    dos = new DataSet();
                    if (ybRAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybBAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Bass\"";
                    dos = new DataSet();
                    if (ybBAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb);

                    cmdz = "UPDATE Arrangements SET PlaythroughYBLink=\"https://www.youtube.com/watch?v=" + ybCAddress.Replace("https://www.youtube.com/watch?v=", "") + "\"";//YouTube_Link
                    cmdz += " WHERE CDLC_ID=" + SongRecord.ID + " AND RouteMask=\"Combo\"";
                    dos = new DataSet();
                    if (ybCAddress != "-") dos = UpdateDB("Arrangements", cmdz + ";", cnb);
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
    }
}
