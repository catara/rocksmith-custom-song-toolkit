using RocksmithToolkitLib.XmlRepository;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualBasic.FileIO; //addded references Asembly VB for deleting to recycle bin

using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
//using Image = System.Drawing.Image;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using RocksmithToolkitLib;
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.Ogg;
using System.Diagnostics;
using RocksmithToolkitLib.DLCPackage.Manifest.Functions;
using RocksmithToolkitLib.Extensions;
using System.Text;
using System.Linq;
using RocksmithToolkitLib.DLCPackage.AggregateGraph;
using RocksmithToolkitLib.Sng;
using System.ComponentModel;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using System.Drawing;

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {

        public const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";
        //static DLCPackageData data;

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
        }



        public static SpotifyWebAPI _spotify;
        //public string _trackno;
        //public string _year;
        //public static PrivateProfile _profile;
        //public List<FullTrack> _savedTracks;
        //public List<SimplePlaylist> _playlists;
        public static object NullHandler(object instance)
        {
            if (instance != null)
                return instance.ToString();

            return DBNull.Value.ToString();// DBNull.Value;
        }

        public static async Task<string> ActivateSpotify_ClickAsync()
        {
            var status = "NOK";
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                status = (reply.Status == IPStatus.Success) ? "OK" : "NOK";
            }
            catch (Exception ex)
            {
                var tgst = "Error1 ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                status = "NOK";
            }

            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "26d287105e31491889f3cd293d85bfea",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);
            var tsst = "";
            if (status == "OK") try
                {
                    _spotify = await webApiFactory.GetWebApi();
                    status = "OK";
                }
                catch (Exception ex)
                {
                    tsst = "1stError connecting to Spotify..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    try
                    {
                        _spotify = await webApiFactory.GetWebApi();
                        status = "OK";
                    }
                    catch (Exception exi)
                    {
                        tsst = "2ndError connecting to Spotify..."; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        try
                        {
                            _spotify = await webApiFactory.GetWebApi();
                            status = "OK";
                        }
                        catch (Exception exci)
                        {
                            tsst = "3rdError connecting to Spotify..."; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);

                            try
                            {
                                _spotify = await webApiFactory.GetWebApi();
                                status = "OK";
                            }
                            catch (Exception ei)
                            {
                                MessageBox.Show(ei.Message + "\n(This is a known occurence every 4 or so connections)");
                                tsst = "4thError connecting to Spotify..."; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                status = "NOK";
                            }
                        }
                    }
                }
            //if (status == "NOK") try
            //    {
            //        _spotify = await webApiFactory.GetWebApi();
            //        status = "OK";
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        status = "NOK";
            //    }$
            //ConfigRepository.Instance()["dlcm_netstatus"] = status;
            return status;
        }
        public static Task<string> StartToGetSpotifyDetails(string Artist, string Album, string Title, string Year, string Status)
        {
            Task<string> bytesRead = RequestToGetSpotifyDetailsAsync(Artist, Album, Title, Year, Status);
            //string sRead = "";
            //if (bytesRead.Result.ToString().Split(';')[4] != "-" && bytesRead.Result.ToString().Split(';')[4] != "") sRead = DwdldAlbumImg(bytesRead.Result.ToString().Split(';')[4], bytesRead.Result.ToString().Split(';')[3]);
            return bytesRead;// +";"+ sRead;
        }
        //public static string StartToGetSpotifyAlbumDetails(string url, string albumcover)
        //{
        //    //Task<string> bytesRead = RequestToGetSpotifyDetailsAsync(Artist, Album, Title, Year, Status);
        //    Task<string> sRead = EAsync(url, albumcover);
        //    return "OK";
        //}
        public static async Task<string> RequestToGetSpotifyDetailsAsync(string Artist, string Album, string Title, string Year, string Status)
        {
            string bytesRead = await GetTrackNoFromSpotifyAsync(CleanTitle(Artist), CleanTitle(Album), CleanTitle(Title), Year, Status);
            return bytesRead;
        }
        //public static async Task<string> RequestToGetSpotifyAlbbumCoverAsync(string Artist)
        //{
        //    using (WebClient wc = new WebClient())
        //    {
        //        byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(Artist.ToString().Split(';')[4]));
        //        using (MemoryStream stream = new MemoryStream(imageBytes)) ;
        //        //picbx_SpotifyCover.Image = Image.FromStream(stream);
        //    }
        //}

        public static string DwdldAlbumImg(string url, string spdetails)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = wc.DownloadData(new Uri(url));
                    FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + spdetails + ".png", FileMode.Create, System.IO.FileAccess.Write);
                    using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    //picbx_SpotifyCover.Image = Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error2 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                MessageBox.Show(ex.Message); return "NOK";
            }
            return "OK";
        }

        public static async Task<string> GetTrackNoFromSpotifyAsync(string Artist, string Album, string Title, string Year, string Status)
        {

            //ActivateSpotify_ClickAsync();
            //string uriString = "https://api.spotify.com/v1/search";
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("query", keywordString);
            var a1 = ""; var a2 = ""; var a3 = ""; var a4 = ""; var a5 = ""; var a6 = "";
            var output = "";
            //var ab = "";
            //var albump = 0;
            //var artistp = 0;
            //var tracknop = 0;
            try
            {
                //_profile = await _spotify.GetPrivateProfileAsync();
                //SearchItem Aitem = _spotify.SearchItems(Album, SearchType.Album);
                //FullAlbum FAitem = _spotify.GetAlbum(Aitem.Albums.Items.);
                //if (Aitem.Error==null) if (Aitem.Albums.Total>0) if (Aitem.Albums.Items[0]. > 0)
                SearchItem Titem = _spotify.SearchItems(Title + "+" + Album + "+" + Artist, SearchType.All);
                if (Titem.Error == null && Titem.Tracks.Total > 0)
                    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem.Tracks.Items)
                    {
                        if (Titem.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                {
                                    a1 = Trac.TrackNumber.ToString();
                                    a2 = Trac.Id;
                                    a3 = Artis.Id;
                                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                    a4 = Trac.Album.Id;
                                    a5 = FAitem.Images[0].Url;
                                    if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                                    else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                                }
                        //if (output != "") break;
                    }

                if (a1 == "")
                {
                    SearchItem Titem2 = _spotify.SearchItems(Title, SearchType.Track, 500);
                    if (Titem2.Error == null && Titem2.Tracks.Total > 0)
                        foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem2.Tracks.Items)
                        {
                            if (Titem2.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                    if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                    {
                                        a1 = Trac.TrackNumber.ToString();
                                        a2 = Trac.Id;
                                        a3 = Artis.Id;
                                        FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                        a4 = Trac.Album.Id;
                                        a5 = FAitem.Images[0].Url;
                                        if (Trac.Album.Name.ToString().ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                                        else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                                    }
                            //if (output != "") break;
                        }

                    if (a1 == "")
                    {

                        SearchItem Titem3 = _spotify.SearchItems(Album + "+" + Artist, SearchType.All);
                        if (Titem3.Error == null && Titem3.Tracks.Total > 0)
                            foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem3.Tracks.Items)
                            {
                                if (Titem3.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                        if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                        {
                                            a1 = Trac.TrackNumber.ToString();
                                            a2 = Trac.Id;
                                            a3 = Artis.Id;
                                            FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                            a4 = Trac.Album.Id;
                                            a5 = FAitem.Images[0].Url;
                                            if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                                            else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                                        }
                                //if (output != "") break;
                            }

                        if (a1 == "")
                        {
                            SearchItem Titem4 = _spotify.SearchItems(Album, SearchType.Album);
                            if (Titem4.Error == null && Titem4.Tracks != null)
                                foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem4.Tracks.Items)
                                {
                                    if (Titem4.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                            if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                            {
                                                a1 = Trac.TrackNumber.ToString();
                                                a2 = Trac.Id;
                                                a3 = Artis.Id;
                                                FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                                a4 = Trac.Album.Id;
                                                a5 = FAitem.Images[0].Url;
                                                if (Trac.Album.Name.ToLower() == Album.ToLower()) { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); goto finish; }
                                                else if ((Trac.Album.Name.ToLower()).IndexOf(Album.ToLower()) >= 0 && output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                                            }
                                    //if (output != "") break;
                                }
                        }
                    }
                }
                //if (_profile.Images != null && _profile.Images.Count > 0)
                //{
                //    using (WebClient wc = new WebClient())
                //    {
                //        byte[] imageBytes = await wc.DownloadDataTaskAsync(new Uri(_profile.Images[0].Url));
                //        using (MemoryStream stream = new MemoryStream(imageBytes))
                //            picbx_AlbumArtPath.Image = Image.FromStream(stream);
                //    }
                //}


                //txt_Artist.Text + "+" +| SearchType.Artist
                // Console.WriteLine(item.Albums.Total); //How many results are there in total? NOTE: item.Tracks = item.Artists = null

                //webClient.QueryString.Add(nameValueCollection);
                //var aa = (webClient.DownloadString(uriString));
                //ab = aa;
                //albump = (aa.ToLower()).IndexOf(Album.ToLower());
                //if (albump > 0) aa = aa.Substring(albump, aa.Length - albump);
                //artistp = (aa.ToLower()).IndexOf(Artist.ToLower());
                //if (artistp > 0) aa = aa.Substring(artistp, aa.Length - artistp);
                //tracknop = (aa.ToLower()).IndexOf("track_number");
                //if (tracknop > 0) a1 = aa.Substring(tracknop + 15, 3);
                //a1 = a1.Replace(",", "");
            }
            catch (Exception ex) { var tsst = "Error3 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            goto finish;
            //txt_Track_No.Value = a1;
            //if (a1 == "" && Album != "")
            //{
            //    a1 = GetTrackNoFromSpotifyAsync(Artist, "", Title).ToString();
            //}
            //if (a1 == "" && Artist != "")
            //{
            //    a1 = GetTrackNoFromSpotifyAsync("", "", Title).ToString();
            //}
            //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
            finish:
            a1 = a1.Trim();
            if (a1 != "")
            {
                //if (output != "")
                //{
                //    string[] args = (output).ToString().Split(';');
                //    if (args[5] != "" && args[5] != null) a6= args[5];
                //}
                //else
                a6 = (ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + Artist + " - " + Album.Replace(":", "") + ".png").Replace("/", "").Replace("?", "");
                if (!File.Exists(a6))
                    using (WebClient wc = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(new Uri(a5));
                        FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    }

                if (output == "") { output = a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ? a6 : ""); }
                else output += (File.Exists(a6) ? a6 : "");
                return output;
            }
            else return "0" + ";-;-;-;-;-";
        }

        static public MainDBfields[] GetRecord_s(string cmd, OleDbConnection cnb) //static
        {
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            //Files[] files = new Files[10000];
            //rtxt_StatisticsOnReadDLCs.Text = "re" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            var MaximumSize = 0;
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            //DataSet dus = new DataSet();
            //OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //dax.Fill(dus, "Main");
            //dax.Dispose();
            MainDBfields[] query = new MainDBfields[10000];
            DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb);

            var i = 0;
            MaximumSize = dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;

            query[0] = new MainDBfields();
            query[0].NoRec = MaximumSize.ToString();
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
            {
                if (File.Exists(filename))
                {
                    var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    SHA1 sha = new SHA1Managed();
                    using (var sr = new StreamReader(fs))
                    {
                        try
                        {
                            //UpdateLog(DateTime.Now, "before calc hash", false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                            byte[] hashBytes = sha.ComputeHash(fs); fs.Close();
                            FileHash = BitConverter.ToString(hashBytes);
                            //UpdateLog(DateTime.Now, "after calc hash", false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }
                        catch (Exception ex) { var tsst = "Errorf ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                        sr.Close();
                    }
                }
            }
            catch (Exception ex) { var tsst = "Errorg ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            return FileHash;
        }

        static public void DeleteFromDB(string DB, string slct, OleDbConnection cnb)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();// "";+ "\\Files.accdb;"
                                                                                  //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            try
            {
                DataSet dss = new DataSet();
                OleDbDataAdapter dan = new OleDbDataAdapter(slct, cnb);
                dan.Fill(dss, "DB");
                dan.Dispose();
            }
            catch (Exception ex)
            {
                var tsst = "Error4 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                ErrorWindow frm1 = new ErrorWindow(DB + "--------" + slct + "--------------" + ex.Message + "DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ ", "https://www.microsoft.com/en-us/download/details.aspx?id=39358", "Error @Import", false, false); //access 2016 file x86 32bitsacedbole6  https://www.microsoft.com/en-us/download/details.aspx?id=50040 //access 2013 file x86 32bitsacedbole15 https://www.microsoft.com/en-us/download/details.aspx?id=39358 //access 2007 smaller file x86 32bitsacedbole12 https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734
                frm1.ShowDialog();
                return;
            }
        }

        static public void CheckValidityGetHASHAdd2ImportPub(object sender, DoWorkEventArgs e)// string s, string logPath, string Temp_Path_Import, int i, string ImportPackNo)//ProgressBar pB_ReadDLCs,, RichTextBox rtxt_StatisticsOnReadDLCs, OleDbConnection cnb
        {

            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];

            string[] args = (e.Argument).ToString().Split(';');
            string s = args[0];
            string i = args[1];
            //float bitrate = float.Parse(args[2]);
            //float SampleRate = float.Parse(args[3]);
            string ImportPackNo = args[2];
            //string audioPreviewPath = args[5];
            //string oggPreviewPath = args[6];
            //string multithreadname = args[7];
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            var tsst = "Start Gathering ..."; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, logPath, tmpPath, i, "", null, null);

            var invalid = "No";
            //DateTime timestamp = DateTime.Now;
            if (!s.IsValidPSARC())
            {
                //errorsFound.AppendLine(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)));
                //MessageBox.Show(String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'", Path.GetFileName(s)));
                timestamp = UpdateLog(timestamp, "error at import " + String.Format("File '{0}' isn't valid. File extension was changed to '.invalid'",
                    Path.GetFileName(s)), true, logPath, tmpPath, "", "", null, null);
                //CopyMoveFileSafely(s.Replace(".psarc", ".invalid"), s, chbx_Additional_Manipulations.GetItemChecked(75));
                //File.Move(s.Replace(".psarc", ".invalid"), s);
                ///*if (!chbx_Additional_Manipulations.GetItemChecked(75)) F*/   else File.Copy(s.Replace(".psarc", ".invalid"), s);
                if (!File.Exists(s) && File.Exists(s.Replace(".psarc", ".invalid"))) File.Move(s.Replace(".psarc", ".invalid"), s);
                invalid = "Yes";
                //broken_fl[bb] = s;
                ///bb++;
                //continue;
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
                timestamp = UpdateLog(timestamp, "error at import" + ee.Message, true, logPath, tmpPath, "", "DLCManager", null, null);
                return;
                //continue;
            }
            //- To remove usage of ee and loading
            //Console.WriteLine("{0} : {1} : {2}", fi.Name, fi.Directory, loading);

            //details end

            //Generating the HASH code
            string FileHash = GetHash(s);
            string plt = fi.FullName.GetPlatform().platform.ToString();

            //Populate ImportDB
            //Populate ImportDB
            string tst = "Check validity gather info on File " + (i + 1) + " :" + s;
            string tre = "\n" + tst;
            //pB_ReadDLCs.CreateGraphics().DrawString(tst, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            // DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";


            var ff = "-";
            ff = DateTime.Now.ToString("yyyyMMdd HHmmssfff"); ;

            var insertcmdd = "FullPath, Path, FileName, FileCreationDate, FileHash, FileSize, ImportDate, Pack, Platform, Invalid";

            var insertvalues = "\"" + s + "\",\"" + fi.DirectoryName + "\",\"" + fi.Name + "\",\"" + fi.CreationTime + "\",\""
                + FileHash + "\",\"" + fi.Length + "\",\"" + ff + "\",\"" + ImportPackNo + "\",\"" + (plt == "Pc" ? "Pc" : plt) + "\",\"" + invalid + "\"";
            //try
            //{ cnb.Open(); }
            //catch (Exception ee)
            //{ }
            InsertIntoDBwValues("Import", insertcmdd, insertvalues, cnb, 0);
            //cnb.Close();
            //pB_ReadDLCs.Increment(1);
            e.Result = "Done";
            cnb.Close();
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues, OleDbConnection cnb, int mutit)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();// + "\\Files.accdb"
            string insertcmd;                                                                      //save import table to reference the hashcodes in future imports
                                                                                                   //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                                                                                                   //{
            try
            {
                DataSet dsm = new DataSet();
                if (fvalues.ToLower().IndexOf("select ") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
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
                    if (fvalues.ToLower().IndexOf("select ") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    dab.Fill(dsm, ftable);
                    dab.Dispose();
                }
                catch (Exception ex)
                {
                    //try
                    //{
                    //    DataSet dsm = new DataSet();
                    //    if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    //    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    //    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    //    dab.Fill(dsm, ftable);
                    //    dab.Dispose();
                    //}
                    //catch (Exception ec)
                    //{
                    //    try
                    //    {
                    //        DataSet dsm = new DataSet();
                    //        if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    //        else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    //        OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    //        dab.Fill(dsm, ftable);
                    //        dab.Dispose();
                    //    }
                    //    catch (Exception edc)
                    //    {
                    //        try
                    //        {
                    //            DataSet dsm = new DataSet();
                    //            if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    //            else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    //            OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    //            dab.Fill(dsm, ftable);
                    //            dab.Dispose();
                    //        }
                    //        catch (Exception efc)
                    //        {
                    //            try
                    //            {
                    //                DataSet dsm = new DataSet();
                    //                if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    //                else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    //                OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    //                dab.Fill(dsm, ftable);
                    //                dab.Dispose();
                    //            }
                    //            catch (Exception g)
                    //            {
                    //                try
                    //                {
                    //                    DataSet dsm = new DataSet();
                    //                    if (fvalues.ToLower().IndexOf("select") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    //                    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    //                    OleDbDataAdapter dab = new OleDbDataAdapter(insertcmd, cnb);
                    //                    dab.Fill(dsm, ftable);
                    //                    dab.Dispose();
                    //                }
                    //                catch (Exception ef)
                    //                {
                    string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
                    string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];
                    if (fvalues.ToLower().IndexOf("select ") >= 0) insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") " + fvalues + "";
                    else insertcmd = "INSERT INTO " + ftable + " (" + ffields + ") VALUES (" + fvalues + ");";
                    DateTime timestamp;
                    timestamp = UpdateLog(DateTime.Now, "error at import " + ee.Message + "-" + insertcmd, true, logPath, tmpPath, mutit.ToString(), "", null, null);
                    ErrorWindow frm1 = new ErrorWindow(ftable + "--------" + fvalues + "--------------" + ex.Message + "DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ ", "https://www.microsoft.com/en-us/download/details.aspx?id=39358", "Error @Import", false, false); //access 2016 file x86 32bitsacedbole6  https://www.microsoft.com/en-us/download/details.aspx?id=50040 //access 2013 file x86 32bitsacedbole15 https://www.microsoft.com/en-us/download/details.aspx?id=39358 //access 2007 smaller file x86 32bitsacedbole12 https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734
                    frm1.ShowDialog();
                    //        }
                    //    }
                    //}
                    //}
                    //return;
                    //}
                }
                //return;
            }
            //}
            //cnb.Close(); cnb.Open();
        }
        // + "\\Files.accdb"
        //if (!File.Exists(DB_Path.Replace(";", ""))) DB_Path = currentDB + "\\Files.accdb";
        //using (OleDbConnection bbgb=new OleDbConnection())
        //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + DB_Path))
        //{
        //    try
        //    {
        //        cnb.Close();
        //        if (cnb.State.ToString() == "Open")
        //        {
        //            //System.Threading.Thread.Sleep(200); }
        //        cnb.Open();
        //        }
        //OleDbCommand command=null;
        //command.CommandText = fcmds;
        ////command.CommandType=
        //
        //try
        //{
        //    command.CommandType = CommandType.Text;
        //    cSystem.Data.CommandBehavior.SequentialAccess
        //    command.ExecuteNonQuery();
        //}
        //catch (Exception ex)
        //{
        //    //timestamp = UpdateLog(timestamp, "error at update " + ex + "\n" + rt, true);
        //    throw;
        //}
        //finally
        //{
        //    if (cnb != null) cnb.Close();
        //}
        //System.Data.CommandBehavior.SequentialAccess
        //da.FillLoadOption = "IgnoreChanges";
        //da.SelectCommand.CommandType. = fcmds;
        //var er=da.FillError();
        //cnb.Close();               
        //return "";
        //}
        //catch (Exception ee)
        //{
        //    Console.WriteLine(ee.Message);
        //    ErrorWindow frm1 = new ErrorWindow(ftable + "--------" + fcmds + "--------------" + "DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import" + ee, false, false);
        //    frm1.ShowDialog();
        //    //return "";
        //}
        //cnb.Dispose();
        //}
        static public DataSet SelectFromDB(string ftable, string fcmds, string currentDB, OleDbConnection cn)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            //cnb.Database = "Files.accdb";
            //if (cn.State.ToString() == "Open")
            //    cn.Close();
            DataSet dfsm = new DataSet();
            if (File.Exists(DB_Path))
            {
                //fcmds += ";";
                DataSet dsm = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(fcmds, cn))

                    //da.ContinueUpdateOnError = true;
                    try
                    {
                        da.Fill(dsm, ftable);
                        //var norec = 0;
                        //if (fcmds == "SELECT  FullPath, Path, FileName, FileHash, FileSize, ImportDate,i.Pack, i.FileCreationDate, i.ID, i.Invalid FROM Import as i ORDER BY i.Platform ASC;")
                        //    norec = dsm.Tables[0].Rows.Count;

                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().IndexOf("The 'Microsoft.ACE.OLEDB.15.0' provider is not registered on the local machine.") >= 0)
                        {
                            ErrorWindow frm1 = new ErrorWindow("You need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                            frm1.ShowDialog();
                        }
                    }
                return dsm;

            }
            else return dfsm;
        }

        public static string CopyMoveFileSafely(string Source, string Dest, bool Copy, string source_hash)
        {
            var dupli_already_exists = false;
            string FileHashO = ""; string FileHashI = "";
            if (File.Exists(Dest)) //;// Dest = Dest;
            //else
            {
                //System.IO.FileInfo fi = null; System.IO.FileInfo fo = null;
                try
                {
                    //fi = new System.IO.FileInfo(Source);
                    if (source_hash != null && source_hash != "") FileHashI = source_hash;
                    else FileHashI = GetHash(Source);//Generating the HASH code
                    //fo = new System.IO.FileInfo(Dest);
                    FileHashO = GetHash(Dest);//Generating the HASH code
                }
                catch (Exception ex)
                {
                    var tsst = "Error5 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    //timestamp = UpdateLog(timestamp, "error at file.hash", true, logPath, tmpPath, "", "DLCManager", null, null);
                    ErrorWindow frm1 = new ErrorWindow("error when calc file hash ", "", "Error at import", false, false);
                    frm1.ShowDialog();
                    //return;
                    //continue;
                }
                if (FileHashI == FileHashO) dupli_already_exists = true;
                else Dest = Dest.Replace(".psarc", "[Duplic_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "].psarc");
            }
            //else dupli_already_exists = true;
            if (!dupli_already_exists)
                try
                {
                    if (!Copy)
                        File.Move(Source, Dest);
                    else File.Copy(Source, Dest, true);
                }
                catch (Exception ex)
                {
                    var tsst = "Error6 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    //MessageBox.Show("Issues when moving to duplicate folder after dupli ignore" + "-" + ex.Message + Source);
                }
            else if (!Copy) FileSystem.DeleteFile(Source, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);


            return Dest;

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

        static public void CleanFolder(string pathfld, string exttoigno, bool archive, string Archive_Path)
        {
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
                        if ((file.FullName.IndexOf(args[0]) == 0 || file.FullName.IndexOf(args[1]) == 0 || exttoigno == "") && !archive) DeleteFile(file.FullName);
                        else CopyMoveFileSafely(file.FullName, Archive_Path + "\\" + Path.GetFileName(file.FullName), archive, null);
                    }
                }
                catch (Exception ex) { var tsst = "Error7 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            }
        }


        static public DataSet UpdateDB(string ftable, string fcmds, OleDbConnection cn)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString();
            DataSet dsm = new DataSet();
            if (File.Exists(DB_Path))
            {
                //OleDbConnection myConn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
                myDataAdapter.SelectCommand = new OleDbCommand(fcmds, cn);
                OleDbCommandBuilder custCB = new OleDbCommandBuilder(myDataAdapter);
                try
                {

                    myDataAdapter.Fill(dsm, ftable);
                    //cn.Close();cn.Open();
                    return dsm;
                }
                catch (Exception ex)
                {
                    var tsst = "Error8 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    ErrorWindow frm1 = new ErrorWindow(ftable + "--------" + fcmds + "--------------" + ex.Message + "DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ ", "https://www.microsoft.com/en-us/download/details.aspx?id=39358", "Error @Import", false, false); //access 2016 file x86 32bitsacedbole6  https://www.microsoft.com/en-us/download/details.aspx?id=50040 //access 2013 file x86 32bitsacedbole15 https://www.microsoft.com/en-us/download/details.aspx?id=39358 //access 2007 smaller file x86 32bitsacedbole12 https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734
                    frm1.ShowDialog();
                    return dsm;
                }
            }
            else return dsm;
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
                var tsst = "Error @filedelete..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                var tsst = "Error @dir delete..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                //MessageBox.Show("Issues when moving to duplicate folder at dupli Update" + "-" + ex.Message + dir);
            }
        }

        static public string WwiseInstalled(string Mss)
        {
            var wwisePath = "";
            if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                wwisePath = ConfigRepository.Instance()["general_wwisepath"];
            else
                wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
            if (wwisePath == "" || !Directory.Exists(wwisePath))
            {
                ErrorWindow frm1 = new ErrorWindow("Cause " + Mss + ".\nPlease Install Wwise Launcher then Wwise v" + ConfigRepository.Instance()["general_wwisepath"] + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                frm1.ShowDialog();
                //if (frm1.IgnoreSong) break;
                //if (frm1.StopImport) { j = 10; i = 9999; break; }
                return "0" + ";" + frm1.IgnoreSong + ";" + frm1.StopImport;
            }
            else
                return "1" + ";0;0";
        }

        public static string Manipulate_strings(string words, int k, bool ifn, bool orig_flag, bool bassRemoved, GenericFunctions.MainDBfields[] SongRecord, string sep1, string sep2)
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
                            tzt = sep2 + SongRecord[k].Artist + sep1;
                            break;
                        //case "<Artist Short>":
                        //    tzt = sep2 + (SongRecord[k].Artist_ShortName=="" ? SongRecord[k].Artist : SongRecord[k].Artist_ShortName) + sep1;
                        //    break;
                        case "<Title>":
                            tzt = sep2 + SongRecord[k].Song_Title + sep1;
                            break;
                        case "<Artist Sort>":
                            tzt = sep2 + SongRecord[k].Artist_Sort + sep1;
                            break;
                        case "<Title Sort>":
                            tzt = sep2 + SongRecord[k].Song_Title_Sort + sep1;
                            break;
                        case "<Album>":
                            tzt = sep2 + SongRecord[k].Album + sep1;
                            break;
                        //case "<Album Short>":
                        //    tzt = sep2 +(SongRecord[k].Album_ShortName==""? SongRecord[k].Album:SongRecord[k].Album_ShortName) + sep1;
                        //    break;
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
                        case "<Beta>":
                            fulltxt = ((SongRecord[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
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
                            tzt = ((SongRecord[k].Is_Live == "Yes") ? "-Live " + SongRecord[k].Live_Details : ""); //not yet done for all arrangements
                            break;
                        case "<Acoustic>":
                            tzt = ((SongRecord[k].Is_Acoustic == "Yes") ? "-Acoustic " + SongRecord[k].Live_Details : ""); //not yet done for all arrangements
                            break;
                        //case "<Live/Acoustic Details>":
                        //    tzt =SongRecord[k].Live_Details; //not yet done for all arrangements
                        //    break;
                        case "<CDLC_ID>":
                            tzt = SongRecord[k].ID; //not yet done for all arrangements
                            break;
                        case "<Random5>":
                            Random randomp = new Random();
                            int rn = randomp.Next(0, 100000);
                            tzt = rn.ToString(); //not yet done for all arrangements
                            break;
                        case "<Artist Short>":
                            tzt = SongRecord[k].Artist_ShortName != "" ? SongRecord[k].Artist_ShortName : SongRecord[k].Artist;
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
                        case "<Bass_HasDD>":
                            tzt = ((SongRecord[k].Has_BassDD == "No" || bassRemoved) && SongRecord[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            tzt = ((SongRecord[k].Has_Bass == "Yes") ? "B" : "") + ((SongRecord[k].Has_Guitar == "Yes") ? "G" : "");
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
                                        tzt = (((SongRecord[k].Has_Cover == "No") || (SongRecord[k].Has_Preview == "No") || (SongRecord[k].Has_Vocals == "No")) ? "NOs-" : "") + ((SongRecord[k].Has_Cover == "Yes") ? "" : "C") + ((SongRecord[k].Has_Preview == "Yes") ? "" : "P") + ((SongRecord[k].Has_Vocals == "Yes") ? "" : "V"); //+((SongRecord[k].Has_Sections == "Yes") ? "" : "S")
                                        break;
                                    case "<lastConversionDateTime>":
                                        tzt = SongRecord[k].Import_Date; //not yet done
                                        break;
                                    default: break;
                                }
                            }
                            break;
                    }
                    if (tzt != "")
                        fulltxt = fulltxt.IndexOf("[") > 0 ? fulltxt.Replace(tzt.Replace("[", "").Replace("]", ""), "") + tzt : fulltxt + tzt;

                    if (oldtxt == fulltxt && last_ > 0) fulltxt = fulltxt.Substring(0, last_);
                    last_ = fulltxt.Length;
                }
            }

            if ((sep1 + sep2).Length > 0) return (fulltxt + sep2).Replace(sep1 + sep2, "");
            else return fulltxt;
        }

        public static void DeleteRecords(string ID, string cmd, string DBPath, string TempPath, string norows, string hash, OleDbConnection cnb)
        {
            //Delete records
            DialogResult result1 = MessageBox.Show(norows + " of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {

                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("), "", cnb);
                var rcount = dhs.Tables[0].Rows.Count;
                var tsst = "Updating PAck detail to point to Archive"; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                for (var i = 0; i < rcount; i++)
                {
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    //try
                    //{
                    DeleteDirectory(filePath);
                    //}
                    //catch (Exception ex) { Console.Write(ex); }

                    //Move psarc file to Duplicates                        
                    string psarcPath = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    var fh = GetHash(psarcPath);
                    psarcPath = CopyMoveFileSafely(psarcPath, psarcPath.Replace("0_old", "0_archive"), false, fh);
                    //var cmdupd = "UPDATE Pack_AuditTrail Set FileName=\"" + Path.GetFileName(psarcPath) + "\", PackPath =REPLACE(PackPath,'\\0_old','\\0_archive') WHERE FileHash='" + fh + "' AND PackPath='" + TempPath + "\\0_old" + "'";
                    //DataSet duis = new DataSet(); duis = UpdateDB("Pack_AuditTrail", cmdupd + ";", cnb);
                    //try
                    //{
                    //    if (!File.Exists(psarcPath.Replace("0_old", "0_duplicate\\")))
                    //        File.Move(psarcPath, psarcPath.Replace("0_old", "0_duplicate\\"));
                    //    else DeleteFile(psarcPath);
                    //}
                    //catch (Exception ex) { Console.Write(ex); }
                }

                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "", cnb);

                //Delete Arangements
                DeleteFromDB("Arrangements", "DELETE FROM Arrangements WHERE CDLC_ID IN (" + ID + ")", cnb);

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE FROM Tones WHERE CDLC_ID IN (" + ID + ")", cnb);

                // //Delete Audit trail of import
                DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash.Replace(", ", "\", \"") + "\")", cnb);

                //Delete Audit trail of pack
                DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + ID + ")", cnb);

                MessageBox.Show(rcount + "  Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static DialogResult CreateTempFolderStructure(string Temp_Path_Import, string old_Path_Import, string broken_Path_Import, string dupli_Path_Import,
            string dlcpacks, string pathDLC, string repacked_path, string repacked_XBOXPath, string repacked_PCPath, string repacked_MACPath, string repacked_PSPath,
            string log_Path, string albumCovers_PSPath, string Log_PSPath, string Archive_Path)
        {
            DialogResult result1 = DialogResult.Yes;
            if (!Directory.Exists(Temp_Path_Import) || !Directory.Exists(pathDLC) || !Directory.Exists(old_Path_Import) || !Directory.Exists(broken_Path_Import) ||
                !Directory.Exists(dupli_Path_Import) || !Directory.Exists(dlcpacks + "\\temp") || !Directory.Exists(dlcpacks + "\\manipulated") ||
                !Directory.Exists(dlcpacks + "\\manifests") || !Directory.Exists(dlcpacks + "\\manipulated\\temp") || !Directory.Exists(repacked_path) ||
                !Directory.Exists(repacked_XBOXPath) || !Directory.Exists(repacked_PCPath) || !Directory.Exists(repacked_MACPath) ||
                !Directory.Exists(repacked_PSPath) || (!Directory.Exists(log_Path) && log_Path != "") || !Directory.Exists(Log_PSPath)
                || !Directory.Exists(albumCovers_PSPath) || !Directory.Exists(Archive_Path))
            {
                MessageBox.Show(String.Format("One of the mandatory backend, folder is missing " + ", " + Temp_Path_Import + ", " + pathDLC + ", " + old_Path_Import + ", " + broken_Path_Import + ", " + dupli_Path_Import + ", " + dlcpacks + "(manipulated or manipulated-temp or manifests or temp), " + repacked_path + "(split by platform), " + log_Path + ", " + albumCovers_PSPath + ", " + Log_PSPath + ", " + Archive_Path, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error));
                try
                {
                    DirectoryInfo di;
                    result1 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Ignore\n3. Cancel Import operation", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        if (!Directory.Exists(Temp_Path_Import) && (Temp_Path_Import != null)) di = Directory.CreateDirectory(Temp_Path_Import);
                        if (!Directory.Exists(pathDLC) && (pathDLC != null)) di = Directory.CreateDirectory(pathDLC);
                        if (!Directory.Exists(old_Path_Import) && (old_Path_Import != null)) di = Directory.CreateDirectory(old_Path_Import);
                        if (!Directory.Exists(broken_Path_Import) && (broken_Path_Import != null)) di = Directory.CreateDirectory(broken_Path_Import);
                        if (!Directory.Exists(dupli_Path_Import) && (dupli_Path_Import != null)) di = Directory.CreateDirectory(dupli_Path_Import);
                        if (!Directory.Exists(dlcpacks) && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks);
                        if (!Directory.Exists(dlcpacks + "\\manifests") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manifests");
                        if (!Directory.Exists(dlcpacks + "\\manipulated") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manipulated");
                        if (!Directory.Exists(dlcpacks + "\\manipulated\\temp") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\manipulated\\temp");
                        if (!Directory.Exists(dlcpacks + "\\temp") && (dlcpacks != null)) di = Directory.CreateDirectory(dlcpacks + "\\temp");
                        if (!Directory.Exists(repacked_path) && (repacked_path != null)) di = Directory.CreateDirectory(repacked_path);
                        if (!Directory.Exists(repacked_XBOXPath) && (repacked_XBOXPath != null)) di = Directory.CreateDirectory(repacked_XBOXPath);
                        if (!Directory.Exists(repacked_PCPath) && (repacked_PCPath != null)) di = Directory.CreateDirectory(repacked_PCPath);
                        if (!Directory.Exists(repacked_MACPath) && (repacked_MACPath != null)) di = Directory.CreateDirectory(repacked_MACPath);
                        if (!Directory.Exists(repacked_PSPath) && (repacked_PSPath != null)) di = Directory.CreateDirectory(repacked_PSPath);
                        if (!Directory.Exists(log_Path) && log_Path != null && (log_Path != "")) di = Directory.CreateDirectory(log_Path);
                        if (!Directory.Exists(albumCovers_PSPath) && (albumCovers_PSPath != null)) di = Directory.CreateDirectory(albumCovers_PSPath);
                        if (!Directory.Exists(Log_PSPath) && (Log_PSPath != null)) di = Directory.CreateDirectory(Log_PSPath);
                        if (!Directory.Exists(Archive_Path) && (Archive_Path != null)) di = Directory.CreateDirectory(Archive_Path);
                    }
                    else if (result1 == DialogResult.No) return result1;
                    else Application.Exit();
                }
                catch (Exception ex)
                {
                    var tsst = "Error9 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open create folders " + Temp_Path_Import + "-" + pathDLC + "-" + old_Path_Import + "-" + broken_Path_Import + "-" + dupli_Path_Import + "-" + dlcpacks + "-" + repacked_path + "-" + albumCovers_PSPath + "-" + Log_PSPath);
                }
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

        public static string Add2LinesInVocals(string filePath, int nooflines)
        {
            var info = File.OpenText(filePath);
            string firsttime = "";
            string secondtime = "";
            string secondline = "";
            string fistline = "";
            string fistlineorig = "";
            string line;
            //var timestamp = UpdateLog(DateTime.Now, "Add2LinesInVocals", true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);

            using (StreamWriter sw = File.CreateText(filePath + ".newvcl"))
            {
                while ((line = info.ReadLine()) != null)
                {
                    if (line.Contains("<vocal time=\"") && firsttime == "")
                    {
                        fistlineorig = line;
                        firsttime = line.Substring(line.IndexOf(value: "\"") + 1, 8);
                        firsttime = firsttime.Substring(0, firsttime.IndexOf("\""));
                        if (firsttime.ToInt32() < 4)
                        { sw.WriteLine(" <vocal time=\"0.61\" note=\"254\" length=\"2\" lyric=\"\"/>"); if (nooflines == 1) sw.WriteLine(line); }
                        else fistline = line.Substring(line.IndexOf("lyric=\\\"") + 7, line.Length - (line.IndexOf("lyric=\\\"") + 8));
                    }
                    else
                        if (line.Contains("<vocal time=\"") && firsttime != "" && secondtime == "" && nooflines == 2)
                    {
                        secondtime = line.Substring(line.IndexOf(value: "\"") + 1, 8);
                        secondtime = secondtime.Substring(0, secondtime.IndexOf("\""));
                        if (secondtime.ToInt32() < 4)
                            sw.WriteLine(" <vocal time=\"3\" note=\"254\" length=\"0.9\" lyric=\"\"/>");
                        else { secondline = line; secondline = line; int ii = line.IndexOf("lyric=") + 9; int jj = line.Length - line.IndexOf("lyric=") + 10; secondline = line; jj = line.Length - (line.IndexOf("lyric=") + 10); secondline = line.Substring(line.IndexOf("lyric=") + 9, line.Length - (line.IndexOf("lyric=") + 10)); }
                        //(line.IndexOf("lyric = \\\"") >= 0)? line.Substring(line.IndexOf("lyric = \\\"") + 7, line.Length - line.IndexOf("lyric = \\\"") + 8):

                        sw.WriteLine(fistlineorig);
                        sw.WriteLine(line);
                    }
                    else sw.WriteLine(line);
                }

            }
            info.Close();
            File.Copy(filePath + ".newvcl", filePath, true);
            DeleteFile(filePath + ".newvcl");
            return fistline + ";" + secondline; ;// return firsttime.Substring(0, firsttime.Length - firsttime.IndexOf("\""));
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
            string name = String.Empty;

            if (!String.IsNullOrEmpty(value))
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
            DataSet dxr = new DataSet(); //var fn = filen.Substring(filen.IndexOf("PS3\\") + 4, filen.Length - filen.IndexOf("PS3\\") - 4);
            if (DB == "LogPackingError") dxr = UpdateDB("Main", "Update Main Set Is_Broken = \"Yes\" WHERE ID=" + dlcID + ";", cnb);
        }

        public static string calc_path(string jsonsFiles)
        {
            var ttt = Path.GetDirectoryName(jsonsFiles);
            var pattth = ttt.IndexOf("\\manifests\\");
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

            var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
            startInfo.FileName = Path.Combine(AppWD, "..\\..\\ddc", "ddc.exe");

            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                startInfo.WorkingDirectory = Folder_Name + "\\EOF\\";
            else
                startInfo.WorkingDirectory = Folder_Name + "\\songs\\arr\\";

            startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2} {3}{4}{5}",
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

        public static string RemoveDD(string Folder_Name, string Is_Original, string xml, Platform platform, bool superOrg, bool InternalLog)
        {
            var Has_BassDD = "No";

            var jsons = "";
            if (superOrg) //37. Keep the Uncompressed Songs superorganized
                jsons = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
            else
                jsons = (xml.Replace(".xml", ".json").Replace("songs\\arr", calc_path(Directory.GetFiles(Folder_Name, "*.json", System.IO.SearchOption.AllDirectories)[0])));

            //Save a copy
            if (!File.Exists(xml + ".old")) File.Copy(xml, xml + ".old", false);
            else File.Copy(xml, xml + ".old", true);
            var json = jsons;
            if (!File.Exists(json + ".old")) File.Copy(json, json + ".old", false);
            else { File.Copy(json + ".old", json, true); }

            var startInfo = new ProcessStartInfo();

            var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_dd_remover.xml"));
            var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_default.xml"));
            startInfo.FileName = Path.Combine(AppWD, "..\\..\\ddc", "ddc.exe");
            startInfo.WorkingDirectory = Folder_Name;
            startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2}{3}{4}",
                                                Path.GetFileName(xml),
                                                40, "N", r,
                                                 " -p Y");
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            var DDCExitCode = 5;
            using (var DDC = new Process())
            {

                if (!InternalLog)//32. When removing DD use internal logic not DDC
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start();
                    DDC.WaitForExit(1000 * 60 * 5);
                }//wait 5 minutes}
                else DDCExitCode = 5;

                if (Is_Original == "Yes" || DDCExitCode == 5)
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
                            if (line.Contains("<level difficulty=\""))
                            {
                                line = line.Replace("<level difficulty=\"", "").TrimStart();
                                line = line.Replace("\">", "");
                                try { diff = line.ToInt32(); }
                                catch
                                {
                                    MessageBox.Show("Errors at DD lvl READ removal");
                                }
                                if (line != v.ToString())
                                {
                                    MessageBox.Show("Errors at DD removal");
                                    break;
                                }
                                v++;
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
                                    var tsst = "Error11 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                                    var tsst = "Error12 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                }
                Has_BassDD = "Yes";
            }

            //remove altough original or t0o old dd
            platform.version = RocksmithToolkitLib.GameVersion.RS2014;
            Song2014 xmlContent = Song2014.LoadFromFile(xml);
            var manifestFunctions = new ManifestFunctions(platform.version);
            return Has_BassDD;

        }

        public static void Downstream(string fn, float bitrate)
        {
            File.Copy(fn, fn + ".old", true);
            var startInfo = new ProcessStartInfo();
            var tst = ""; var timestamp = DateTime.Now;
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = (fn.IndexOf("preview.wem") > 0 ? fn.Replace(".wem", ".ogg") : fn.Replace(".wem", "_fixed.ogg")).TrimStart(' ');
            var tt = t + "l";
            startInfo.Arguments = String.Format(" \"{0}\" -o \"{1}\" -Q",
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
                    DDC.WaitForExit(1000 * 60 * 4); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        startInfo = new ProcessStartInfo();
                        startInfo.FileName = Path.Combine(AppWD, "oggenc.exe");
                        startInfo.WorkingDirectory = AppWD;
                        startInfo.Arguments = String.Format(" \"{0}\" -o \"{1}\" -b \"{2}\"  --resample \"{3}\" -Q",
                                                            tt,
                                                            t,
                                                            ConfigRepository.Instance()["dlcm_BitRate"].Substring(0, 3),
                                                            ConfigRepository.Instance()["dlcm_SampleRate"]);
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                        if (File.Exists(t))
                            using (var DDgC = new Process())
                            {
                                tst = "Downstream from " + bitrate.ToString() + " to" + ConfigRepository.Instance()["dlcm_BitRate"] + "-" + ConfigRepository.Instance()["dlcm_SampleRate"] + "..."; timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                DDgC.StartInfo = startInfo; DDgC.Start(); DDgC.WaitForExit(1000 * 60 * 2); //wait 1min
                                if (DDgC.ExitCode == 0)
                                {
                                    DeleteFile(tt);
                                    //MainDB.Converters(t, MainDB.ConverterTypes.Ogg2Wem, false);

                                    DeleteFile(fn);
                                    var i = 1;
                                    //float ass = 0;
                                    do //sometimes it fails
                                    {
                                        //if (i > 1)
                                        //    ;
                                        tst = "Convert to wem ... " + i + " - " + fn;
                                        timestamp = UpdateLog(timestamp, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                        //OggFile.Convert2Wem(t, 4, 30000, 34000);
                                        GenericFunctions.Converters(t, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);

                                        System.IO.FileInfo fi = null; //calc file size
                                        try { fi = new System.IO.FileInfo(t.Replace(".ogg", ".wem")); }
                                        catch (Exception ex) { var tsst = "Error13 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                                        if (!File.Exists(t.Replace(".ogg", ".wem")) || fi.Length == 0)
                                        {
                                            File.Copy(fn + ".old", t.Replace(".ogg", ".wem"), true);
                                            if (i > 3)
                                            {
                                                //fix as sometime the template folder gets poluted and breaks eveything
                                                var appRootDir = Path.GetDirectoryName(Application.ExecutablePath);
                                                var templateDir = Path.Combine(appRootDir, "Template");
                                                var backup_dir = AppWD + "\\Template";
                                                DeleteDirectory(templateDir);
                                                CopyFolder(backup_dir, templateDir);
                                            }
                                        }
                                        else break;
                                        i++;
                                        //var startInfos = new ProcessStartInfo(); var ts = t.Replace(".ogg", ".wem");
                                        //startInfos.FileName = Path.Combine(AppWD, "MediaInfo_CLI_17.12_Windows_x64", "MediaInfo.exe"); startInfos.WorkingDirectory = AppWD;
                                        //startInfos.Arguments = String.Format(" --Inform=Audio;%BitRate% \"{0}\"", ts);
                                        //startInfos.UseShellExecute = false; startInfos.CreateNoWindow = true; startInfos.RedirectStandardOutput = true; startInfos.RedirectStandardError = true;
                                        //if (File.Exists(t))
                                        //    using (var DDCs = new Process())
                                        //    {
                                        //        DDCs.StartInfo = startInfo; DDCs.Start(); string stdoutx = DDCs.StandardOutput.ReadToEnd(); string stderrx = DDCs.StandardError.ReadToEnd();
                                        //        DDCs.WaitForExit(1000 * 60 * 1); ass = 0;//wait 1min
                                        //        if (stdoutx != "\r\n") ass = float.Parse(stdoutx.Replace("\r\n", ""));
                                        //    }
                                    }
                                    while (i < 10);//&& ass <= float.Parse(ConfigRepository.Instance()["dlcm_MaxBitRate"]));//!File.Exists(t.Replace(".ogg", ".wem")) && 

                                    if (!File.Exists(t.Replace(".ogg", ".wem"))) { File.Copy(fn + ".old", t.Replace(".ogg", ".wem"), false); DeleteFile(fn + ".old"); }
                                    else DeleteFile(fn + ".old");
                                    if (File.Exists(t.Replace(".ogg", ".wav"))) DeleteFile(t.Replace(".ogg", ".wav"));
                                    if (File.Exists(t.Replace(".ogg", "_preview.wav"))) DeleteFile(t.Replace(".ogg", "_preview.wav"));
                                    if (File.Exists(t.Replace(".ogg", "_preview.wem"))) DeleteFile(t.Replace(".ogg", "_preview.wem"));
                                    if (File.Exists(t.Replace(".ogg", "_preview.ogg"))) DeleteFile(t.Replace(".ogg", "_preview.ogg"));
                                    if (!(fn.IndexOf("preview.wem") > 0))
                                    {
                                        File.Move(t.Replace(".ogg", ".wem"), fn);//.Replace("_fixed.ogg", ".wem")
                                        if (!File.Exists(fn))
                                        {
                                            var tsst = "Error14 ..." + "Error downsizingpreview" + DDgC.ExitCode + DDgC.StartInfo.FileName + DDgC.StartInfo.ErrorDialog + DDgC.StartInfo.Arguments; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                            //ErrorWindow frm1 = new ErrorWindow("Error downsizingpreview" + DDgC.ExitCode + DDgC.StartInfo.FileName + DDgC.StartInfo.ErrorDialog + DDC.StartInfo.Arguments, "", "", false, false);
                                            //frm1.ShowDialog();
                                        }
                                    }
                                }
                                else
                                {
                                    var tsst = "Error15 ..." + "Error downsizingpreview" + DDgC.ExitCode + DDgC.StartInfo.FileName + DDgC.StartInfo.ErrorDialog + DDgC.StartInfo.Arguments; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                                    //ErrorWindow frm1 = new ErrorWindow("Error downsizing" + DDgC.ExitCode + DDgC.StartInfo.FileName + DDgC.StartInfo.ErrorDialog + DDC.StartInfo.Arguments, "", "", false, false);
                                    //frm1.ShowDialog();
                                }
                            }
                        //else;
                    }
                    else
                    {
                        var tsst = "Error16 ..." + "Error downsizingpreview" + DDC.ExitCode + DDC.StartInfo.FileName + DDC.StartInfo.ErrorDialog + DDC.StartInfo.Arguments; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        //ErrorWindow frm1 = new ErrorWindow("Error downsizing" + DDC.ExitCode + DDC.StartInfo.FileName + DDC.StartInfo.ErrorDialog + DDC.StartInfo.Arguments, "", "", false, false);
                        //frm1.ShowDialog();
                    }
                }
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
                System.IO.StreamReader streamReader = new System.IO.StreamReader(response.GetResponseStream());

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
            catch (Exception ex) { var tsst = "Error17 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); return null; }
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
                var tsst = "Error18 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                return "nok";
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
                Byte[] buffer = new Byte[Length];
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
                var tsst = "Error 18..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                return "nok";
            }
        }
        public static string FTPFile(string filel, string filen, string TempPat, string SearchCm, OleDbConnection cnb)
        {
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
                    dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\";", cnb);

                    return "Truly ";
                }
                catch (Exception ex)
                {
                    var tsst = "Error 19..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                    return "Not ";
                }
            }
            catch (Exception ex)
            {
                var tsst = "Error20 ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                return "Not ";
            }
        }
        public static void Ogg2Wav(string sourcePath, string destinationPath)
        {
            var cmdArgs = String.Format(" -o \"{1}\" \"{0}\"", sourcePath, destinationPath);


            var APP_OGGDEC = "oggdec.exe";
            GeneralExtensions.RunExternalExecutable(APP_OGGDEC, true, true, true, cmdArgs);
        }
        public static void Ogg2Preview(string sourcePath, string destinationPath, long msLength = 30000, long msStart = 4000)
        {
            var cmdArgs = String.Format(" -s {2} -l {3} \"{0}\" \"{1}\"", sourcePath, destinationPath, msStart, msLength);
            var APP_OGGCUT = "oggCut.exe";
            GeneralExtensions.RunExternalExecutable(APP_OGGCUT, true, true, true, cmdArgs);
        }

        public static void Wav2Ogg(string sourcePath, string destinationPath, int qualityFactor)
        {
            if (destinationPath == null)
                destinationPath = String.Format("{0}", Path.ChangeExtension(sourcePath, "ogg"));
            // interestingly ODLC uses 44100 or 48000 interchangeably ... so resampling is not necessary
            var cmdArgs = String.Format(" -q {2} \"{0}\" -o \"{1}\"", sourcePath, destinationPath, Convert.ToString(qualityFactor));
            var APP_OGGENC = "oggenc.exe";
            GeneralExtensions.RunExternalExecutable(APP_OGGENC, true, true, true, cmdArgs);
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
            var oggPath = String.Format(audioPathNoExt + ".ogg");
            var wavPath = String.Format(audioPathNoExt + ".wav");
            var wemPath = String.Format(audioPathNoExt + ".wem");
            var oggPreviewPath = String.Format(audioPathNoExt + "_preview.ogg");
            var wavPreviewPath = String.Format(audioPathNoExt + "_preview.wav");
            var wemPreviewPath = String.Format(audioPathNoExt + "_preview.wem");

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
                Wwise.Convert2Wem(audioPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            if (audioPath.Substring(audioPath.Length - 4).ToLower() == ".wem" && !File.Exists(wemPreviewPath))
            {
                OggFile.Revorb(audioPath, oggPath, Path.GetDirectoryName(Application.ExecutablePath), OggFile.WwiseVersion.Wwise2017);
                GenericFunctions.Ogg2Wav(oggPath, wavPath);
                GenericFunctions.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                GenericFunctions.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                Wwise.Convert2Wem(wavPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            return audioPath;
        }
        public static void Converters(string file, ConverterTypes converterType, bool mssON, bool WinOn)
        {

            var txtOgg2FixHdr = String.Empty;
            var txtWwiseConvert = String.Empty;
            var txtWwise2Ogg = String.Empty;
            var txtAudio2Wem = String.Empty;

            Dictionary<string, string> errorFiles = new Dictionary<string, string>();
            List<string> successFiles = new List<string>();

            try
            {
                var extension = Path.GetExtension(file);
                var outputFileName = Path.Combine(Path.GetDirectoryName(file), String.Format("{0}_fixed{1}", Path.GetFileNameWithoutExtension(file), ".ogg"));
                switch (converterType)
                {
                    case ConverterTypes.Ogg2Wem:
                        GenericFunctions.Convert2Wem(file, 4, 4 * 1000);
                        //Delete any preview_preview file created..by....?ccc
                        foreach (string prev_prev in Directory.GetFiles(Path.GetDirectoryName(file), "*preview_preview*", System.IO.SearchOption.AllDirectories))
                        {
                            DeleteFile(prev_prev);
                        }
                        break;
                }

                successFiles.Add(file);
            }
            catch (Exception ex)
            {
                var tsst = "uError ...i" + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                errorFiles.Add(file, ex.Message);
            }

            if (errorFiles.Count <= 0 && successFiles.Count > 0)
                if (mssON) MessageBox.Show("Conversion complete!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (errorFiles.Count > 0 && successFiles.Count > 0)
                {
                    StringBuilder alertMessage = new StringBuilder(
                        "Conversion complete with errors." + Environment.NewLine + Environment.NewLine);
                    alertMessage.AppendLine(
                        "Files converted with success:" + Environment.NewLine);

                    foreach (var sFile in successFiles)
                        alertMessage.AppendLine(String.Format("File: {0}", sFile));
                    alertMessage.AppendLine("Files converted with error:" + Environment.NewLine);
                    foreach (var eFile in errorFiles)
                        alertMessage.AppendLine(String.Format("File: {0}; error: {1}", eFile.Key, eFile.Value));

                    if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    StringBuilder alertMessage = new StringBuilder(
                        "Conversion complete with errors." + Environment.NewLine);
                    alertMessage.AppendLine(
                        "Files converted with error: " + Environment.NewLine);
                    foreach (var eFile in errorFiles)
                        alertMessage.AppendLine(String.Format("File: {0}, error: {1}", eFile.Key, eFile.Value));

                    if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        public static Boolean IsNumbers(String value)
        {
            return value.All(Char.IsDigit);
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


        public static DateTime UpdateLog(DateTime dt, string txt, bool bbl, string logPath, string tmpPath, string MultithreadNo, string form, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs)
        {
            DateTime dtt = System.DateTime.Now;
            var ismaindb = "";
            var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');
            if (form != null && form != "" && rtxt_StatisticsOnReadDLCs != null)
            //else if (form == "DLCManager")
            {
                rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //pB_ReadDLCs.Value += 1;
                //pB_ReadDLCs.CreateGraphics().DrawString("####################################################################################"
                //    , new Font("Arial", (float)7, FontStyle.Bold), Brushes.Green, new PointF(1, pB_ReadDLCs.Height / 4));
                pB_ReadDLCs.CreateGraphics().DrawString(txt, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }
            if (form == "MainDB") ismaindb = "maindb";
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
                        sw.WriteLine(dtt + " - " + ii + " - " + txt);// This text is always added, making the file longer over time if it is not deleted.
                    }
                }
            }
            catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            return dtt;
        }

        public static DateTime UpdateLogs(DateTime dt, string txt, bool bbl, string logPath, string tmpPath, string MultithreadNo, string form, ProgressBar pB_ReadDLCs)
        {
            pB_ReadDLCs.Value += 1;
            DateTime dtt = System.DateTime.Now;
            var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');
            //rtxt_StatisticsOnReadDLCs.Text = dtt + " - " + ii + " - " + txt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            //MainDB.UpdateProgressBar("w"); ;
            //if (form == "MainDB") ;
            ////MainDB.pB_ReadDLCs.CreateGraphics().DrawString(txt, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, MainDB.pB_ReadDLCs.Height / 4));
            //else 
            if (form == "DLCManager")
            {
                pB_ReadDLCs.Value += 1;
                pB_ReadDLCs.CreateGraphics().DrawString("-" + txt + "---------------", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
            }
            // Write the string to a file. packid+
            Random randomp = new Random();
            var packid = 0;
            packid = randomp.Next(0, 100000);
            var fn = (logPath == null || !Directory.Exists(logPath) ? tmpPath + "\\0_log" : logPath) + "\\" + "current_temp" + MultithreadNo + ".txt";
            // This text is always added, making the file longer over time
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

        //public static string GeneratePackage(string ID, bool bassRemoved, string chbx_Format, string netstatus, Boolean chbx_Beta, string chbx_Group, string Groupss, string TempPath, Boolean chbx_UniqueID)
        public static void GeneratePackage(object sender, DoWorkEventArgs e)
        {
            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);
            string[] args = (e.Argument).ToString().Split(';');
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            string ID = args[0];
            bool error = false;
            var startT = DateTime.Now;
            string TempPath = "";
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];
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
                //string SourcePlatform = args[19];
                //string TargetPlatform = args[20];
                string Original_FileName = SongRecord[0].Original_FileName;
                string Folder_Name = SongRecord[0].Folder_Name;
                //string copyj = args[23];
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
                bool updateTonesArrangs = ConfigRepository.Instance()["dlcm_AdditionalManipul76"] == "yes" ? true : false;
                multithreadname = args[36];
                form = args[37];

                string chbx_Format = (chbx_PC != "" ? "PC" : (chbx_PS3 != "" ? "PS3" : (chbx_XBOX != "" ? "XBOX360" : (chbx_Mac != "" ? "Mac" : ""))));
                UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, form, null, null);

                string dlcSavePath = "";
                string h = "";
                //var bassfile = "";
                string oldfilePath = ""; var rec = 0;
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
                        //var convdone = "beginn";
                        TargetPlatform = new Platform(chbx_Format, GameVersion.RS2014.ToString());

                        var needRebuildPackage = SourcePlatform.IsConsole != TargetPlatform.IsConsole;
                        var tmpDir = Path.GetTempPath();

                        var unpackedDir = Packer.Unpack(oldfilePath, tmpDir, false, false, SourcePlatform);

                        // DESTINATION
                        var nameTemplate = (!TargetPlatform.IsConsole) ? "{0}{1}.psarc" : "{0}{1}";
                        randomp = new Random();
                        packid = randomp.Next(0, 100000);
                        var packageName = Path.GetFileNameWithoutExtension(oldfilePath).StripPlatformEndName();
                        if (chbx_UniqueID) packageName += packid;
                        packageName = packageName.Replace(".", "_");
                        var targetFileName = String.Format(nameTemplate, Path.Combine(Path.GetDirectoryName(oldfilePath), packageName), TargetPlatform.GetPathName()[2]);

                        // CONVERSION
                        if (needRebuildPackage)
                        {
                            data = DLCPackageData.LoadFromFolder(unpackedDir, TargetPlatform, SourcePlatform);
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
                                    DirectoryExtension.Move(dir, newDir);
                                }
                                else if (dir.EndsWith(sourceDir1))
                                {
                                    var newDir = dir.Substring(0, dir.LastIndexOf(sourceDir1)) + targetDir1;
                                    DeleteDirectory(newDir);
                                    DirectoryExtension.Move(dir, newDir);
                                }
                            }

                            // Recreates SNG because SNG have different keys in PC and Mac
                            bool updateSNG = ((SourcePlatform.platform == GamePlatform.Pc && TargetPlatform.platform == GamePlatform.Mac) || (SourcePlatform.platform == GamePlatform.Mac && TargetPlatform.platform == GamePlatform.Pc));

                            // Packing
                            var dirToPack = unpackedDir;
                            if (SourcePlatform.platform == GamePlatform.XBox360)
                                dirToPack = Directory.GetDirectories(Path.Combine(unpackedDir, Packer.ROOT_XBox360))[0];

                            Packer.Pack(dirToPack, targetFileName, updateSNG, TargetPlatform);
                            DeleteDirectory(unpackedDir);
                        }
                        //DirectoryExtension.SafeDelete(unpackedDir);

                        var s = TempPath + "\\0_old\\";
                        h = TempPath + "\\0_repacked\\" + (chbx_Format == "PC" ? "PC" : chbx_Format == "Mac" ? "MAC" : chbx_Format == "PS3" ? "PS3" : "XBOX360") + "\\"; //+ Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString()));
                        h += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_Format == "PC" ? ".psarc" : (chbx_Format == "Mac" ? ".psarc" : (chbx_Format == "PS3" ? ".psarc.edat" : "")));
                        s += Path.GetFileNameWithoutExtension(targetFileName) + (chbx_Format == "PC" ? ".psarc" : (chbx_Format == "Mac" ? ".psarc" : (chbx_Format == "PS3" ? ".psarc.edat" : "")));// targetFileName;//s.Substring(0, s.LastIndexOf("_")) + (chbx_Format.Text == "PC" ? "_p.psarc" : (chbx_Format.Text == "Mac" ? "_m .psarc" : (chbx_Format.Text == "PS3" ? "_ps3.psarc.edat" : "")));

                        if (File.Exists(h)) { DeleteFile(h); File.Move(s, h); }
                        else File.Copy(s, h, true);

                        ////Generating the HASH code
                        //var FileHash = "";
                        //FileStream fs;
                        //FileHash = GetHash(h);
                        //using (fs = File.OpenRead(h))
                        //{
                        //    //SHA1 sha = new SHA1Managed();
                        //    //FileHash = BitConverter.ToString(sha.ComputeHash(fs));

                        //    System.IO.FileInfo fi = null; //calc file size
                        //    try { fi = new System.IO.FileInfo(h); }
                        //    catch (Exception ex)
                        //    {
                        //        var tust = "Error ..." + ex; UpdateLog(DateTime.Now, tust, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        //        ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                        //    }

                        //    var insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                        //    var fnnon = Path.GetFileName(h);
                        //    var packn = h.Substring(0, h.IndexOf(fnnon));
                        //    var insertA = "\"" + h + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fs.Length + "\"," + ID + ",\"" + txt_DLC_ID + "\",\"" + h.GetPlatform().platform.ToString() + "\"";
                        //    InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                        //    fs.Close();
                        //}
                    }
                }
                if ((chbx_Last_Packed && chbx_Last_PackedEnabled) || (!File.Exists(h) && h != ""))
                {
                    DataSet dvr = new DataSet(); dvr = SelectFromDB("Pack_AuditTrail", "SELECT TOP 1 PackPath+FileName FROM Pack_AuditTrail WHERE Platform=\"" + chbx_Format + "\" and DLC_ID=" + ID + " ORDER BY ID DESC;", "", cnb);
                    rec = dvr.Tables[0].Rows.Count;
                    if (rec > 0) h = dvr.Tables[0].Rows[0].ItemArray[0].ToString();
                }
                if (!(chbx_Last_Packed && chbx_Last_PackedEnabled) || (chbx_Last_Packed && chbx_Last_PackedEnabled && rec == 0) || (!File.Exists(h) && h != ""))
                {
                    var i = 0;
                    foreach (var filez in SongRecord)
                    {
                        if (i > 0) //ONLY 1  FILE WILL BE READ
                            break;
                        i++;
                        var packagePlatform = filez.Folder_Name.GetPlatform();
                        // REORGANIZE
                        var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                        //if (structured)
                        //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);

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
                                            bassRemoved = (RemoveDD(Folder_Name, chbx_Original, xml, platformz, false, false) == "Yes") ? true : false;
                                            //bassfile = xml;
                                            timestamp = UpdateLog(timestamp, "Removing Bass.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, logPath, TempPath, multithreadname, form, null, null);
                                            break;
                                        }
                                    }
                                    if (xmlContent.Arrangement.ToLower() != "bass" && xml.IndexOf(".old") <= 0)
                                    {
                                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul3"] == "Yes" && !(chbx_KeepDD && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes"))

                                        {
                                            bassRemoved = (RemoveDD(Folder_Name, chbx_Original, xml, platformz, false, false) == "Yes") ? true : false;
                                            //bassfile = xml;
                                            timestamp = UpdateLog(timestamp, "Removing non bass DD.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, logPath, TempPath, multithreadname, form, null, null);
                                            break;
                                        }
                                    }
                                }
                                catch (Exception ex) { var tust = "Error ..." + ex; UpdateLog(DateTime.Now, tust, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null); }
                        }

                        // LOAD DATA
                        timestamp = UpdateLog(timestamp, "Loading song.." + filez.ID + "-" + filez.Artist + "-" + filez.Song_Title, true, logPath, TempPath, multithreadname, form, null, null);

                        var info = DLCPackageData.LoadFromFolder(filez.Folder_Name, packagePlatform);

                        var xmlFiles = Directory.GetFiles(filez.Folder_Name, "*.xml", System.IO.SearchOption.AllDirectories);
                        var platform = filez.Folder_Name.GetPlatform();
                        //pB_ReadDLCs.Increment(1);chbx_Format == "PC" || chbx_Format == "PS3" || fchbx_Format == "XBOX360" || chbx_Format == "Mac" || 
                        //var tz = Convert.ToSingle(filez.Preview_Volume);
                        //var vb = Convert.ToDouble(filez.Preview_Volume);
                        //float vfjb;
                        //float.TryParse(filez.Preview_Volume, out vfjb);
                        // float volume = float.Parse(filez.Preview_Volume.Replace(".","."));
                        //var fgh = (filez.Preview_Volume);
                        ////float fnum = (float)fgh;
                        float volume = float.Parse(filez.Volume.Replace(",", "."));
                        float volumep = float.Parse(filez.Preview_Volume.Replace(",", "."));

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
                            LyricArtPath = info.LyricArtPath,

                            //USEFUL CMDs String.IsNullOrEmpty(
                            SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
                            {
                                SongDisplayName = filez.Song_Title,
                                SongDisplayNameSort = filez.Song_Title_Sort,
                                Album = filez.Album,
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
                        //RocksmithToolkitLib.DLCPackage.ToolkitInfo.PackageVersion = filez.ToolkitVersion//Version

                        //Add Tones
                        //var cmds = "SELECT * FROM Tones WHERE CDLC_ID=" + txt_ID.Text + ";";
                        //DataSet dfs = new DataSet();
                        //var norec = 0;
                        //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                        //{
                        //    try
                        //    {
                        //        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                        //        da.Fill(dfs, "Tones");
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        if (updateTonesArrangs)
                        {
                            //Update Tones
                            var norec = 0;
                            DataSet dfs = new DataSet(); dfs = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + ID + ";", "", cnb);
                            foreach (var arg in info.TonesRS2014)//, Type
                            {
                                norec = dfs.Tables[0].Rows.Count;
                                for (int j = 0; j < norec; j++)
                                {
                                    var TID = dfs.Tables[0].Rows[j].ItemArray[0].ToString();
                                    data.TonesRS2014[j].Name = dfs.Tables[0].Rows[j].ItemArray[1].ToString();
                                    data.TonesRS2014[j].Volume = float.Parse(dfs.Tables[0].Rows[j].ItemArray[3].ToString());
                                    data.TonesRS2014[j].Key = dfs.Tables[0].Rows[j].ItemArray[4].ToString();
                                    data.TonesRS2014[j].IsCustom = dfs.Tables[0].Rows[j].ItemArray[5].ToString().ToLower() == "true" ? true : false;
                                    data.TonesRS2014[j].SortOrder = decimal.Parse(dfs.Tables[0].Rows[j].ItemArray[11].ToString());
                                    data.TonesRS2014[j].NameSeparator = dfs.Tables[0].Rows[j].ItemArray[12].ToString();
                                    //dictionary types not saved in the DB yet
                                    var nrc = 0;
                                    DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Amp\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsc.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        if (dsc.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Amp.Type = dsc.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsc.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Amp.Category = dsc.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FG = new Dictionary<string, float>();
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        strArrK = dsc.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsc.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] == "" || strArrV[l] == "") FG.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FG.Count != 0) data.TonesRS2014[j].GearList.Amp.KnobValues = FG;
                                        if (dsc.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Amp.PedalKey = dsc.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsc.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dsc.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsc.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dsc.Tables[0].Rows[k].ItemArray[6].ToString());
                                    }
                                    nrc = 0;
                                    DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Cabinet\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsa.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsa.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Type = dsa.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsa.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Category = dsa.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsa.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsa.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
                                        if (dsa.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.PedalKey = dsa.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsa.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dsa.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsa.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dsa.Tables[0].Rows[k].ItemArray[6].ToString());
                                    }
                                    nrc = 0;
                                    DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal1\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dss1.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dss1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Type = dss1.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dss1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Category = dss1.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dss1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dss1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal1.KnobValues = FS;
                                        if (dss1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.PedalKey = dss1.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dss1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dss1.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dss1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dss1.Tables[0].Rows[k].ItemArray[6].ToString());
                                    }
                                    nrc = 0;
                                    DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal2\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dss2.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dss2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Type = dss2.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dss2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Category = dss2.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dss2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dss2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal2.KnobValues = FS;
                                        if (dss2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.PedalKey = dss2.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dss2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Skin = dss2.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dss2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.SkinIndex = float.Parse(dss2.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal3\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dss3.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dss3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Type = dss3.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dss3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Category = dss3.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dss3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dss3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal3.KnobValues = FS;
                                        if (dss3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.PedalKey = dss3.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dss3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Skin = dss3.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dss3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.SkinIndex = float.Parse(dss3.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal4\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dss4.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dss4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Type = dss4.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dss4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Category = dss4.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dss4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dss4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal4.KnobValues = FS;
                                        if (dss4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.PedalKey = dss4.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dss4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Skin = dss4.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dss4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.SkinIndex = float.Parse(dss4.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }

                                    nrc = 0;
                                    DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal1\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsp1.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsp1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Type = dsp1.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsp1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Category = dsp1.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsp1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsp1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal1.KnobValues = FS;
                                        if (dsp1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.PedalKey = dsp1.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsp1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Skin = dsp1.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsp1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.SkinIndex = float.Parse(dsp1.Tables[0].Rows[k].ItemArray[6].ToString());
                                    }
                                    nrc = 0;
                                    DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal2\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsp2.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsp2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Type = dsp2.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsp2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Category = dsp2.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsp2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsp2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal2.KnobValues = FS;
                                        if (dsp2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.PedalKey = dsp2.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsp2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Skin = dsp2.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsp2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.SkinIndex = float.Parse(dsp2.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal3\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsp3.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsp3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Type = dsp3.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsp3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Category = dsp3.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsp3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsp3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal3.KnobValues = FS;
                                        if (dsp3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.PedalKey = dsp3.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsp3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Skin = dsp3.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsp3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.SkinIndex = float.Parse(dsp3.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal4\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsp4.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsp4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Type = dsp4.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsp4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Category = dsp4.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsp4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsp4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal4.KnobValues = FS;
                                        if (dsp4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.PedalKey = dsp4.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsp4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Skin = dsp4.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsp4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.SkinIndex = float.Parse(dsp4.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }

                                    nrc = 0;
                                    DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack1\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsr1.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsr1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Type = dsr1.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsr1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Category = dsr1.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsr1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsr1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack1.KnobValues = FS;
                                        if (dsr1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack1.PedalKey = dsr1.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsr1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Skin = dsr1.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsr1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack1.SkinIndex = float.Parse(dsr1.Tables[0].Rows[k].ItemArray[6].ToString());
                                    }
                                    nrc = 0;
                                    DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack2\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsr2.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsr2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Type = dsr2.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsr2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Category = dsr2.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsr2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsr2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack2.KnobValues = FS;
                                        if (dsr2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack2.PedalKey = dsr2.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsr2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Skin = dsr2.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsr2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack2.SkinIndex = float.Parse(dsr2.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack3\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsr3.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsr3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Type = dsr3.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsr3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Category = dsr3.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsr3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsr3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack3.KnobValues = FS;
                                        if (dsr3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack3.PedalKey = dsr3.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsr3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Skin = dsr3.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsr3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack3.SkinIndex = float.Parse(dsr3.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }
                                    nrc = 0;
                                    DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack4\" ORDER BY Type DESC;", "", cnb);
                                    nrc = dsr4.Tables[0].Rows.Count;
                                    for (int k = 0; k < nrc; k++)
                                    {
                                        string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                                        if (dsr4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Type = dsr4.Tables[0].Rows[k].ItemArray[0].ToString();
                                        if (dsr4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Category = dsr4.Tables[0].Rows[k].ItemArray[1].ToString();
                                        Dictionary<string, float> FS = new Dictionary<string, float>();
                                        strArrK = dsr4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                                        strArrV = dsr4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                                        for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                                        if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack4.KnobValues = FS;
                                        if (dsr4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack4.PedalKey = dsr4.Tables[0].Rows[k].ItemArray[4].ToString();
                                        if (dsr4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Skin = dsr4.Tables[0].Rows[k].ItemArray[5].ToString();
                                        if (dsr4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack4.SkinIndex = float.Parse(dsr4.Tables[0].Rows[k].ItemArray[6].ToString());

                                    }

                                    //Type myType = typeof(RocksmithToolkitLib.DLCPackage.Manifest2014.Tone.Gear2014);
                                    //PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                                    //myPropInfo.SetValue(this, value, null);
                                    //data.TonesRS2014[j].GearList.PostPedal1.;//.sdfs.Tables[0].Rows[14].ItemArray[0].ToString() };
                                    //data.TonesRS2014[j].GearList.PostPedal2 = dfs.Tables[0].Rows[15].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.PostPedal3 = dfs.Tables[0].Rows[16].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.PostPedal4 = dfs.Tables[0].Rows[17].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.PrePedal1 = dfs.Tables[0].Rows[18].ItemArray[0].ToString().ToInt32();
                                    //data.TonesRS2014[j].GearList.PrePedal2 = dfs.Tables[0].Rows[19].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.PrePedal3 = dfs.Tables[0].Rows[20].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.PrePedal4 = dfs.Tables[0].Rows[21].ItemArray[0].ToString();
                                    //Pedal2014 rack1 = new Pedal2014();
                                    //Pedal2014 rack2 = new Pedal2014();
                                    //rack1.
                                    //data.TonesRS2014[j].GearList.Rack1 = dfs.Tables[0].Rows[22].ItemArray[0].ToString().ToInt32();
                                    //data.TonesRS2014[j].GearList.Rack2 = dfs.Tables[0].Rows[23].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.Rack3 = dfs.Tables[0].Rows[24].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.Rack4 = dfs.Tables[0].Rows[25].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.Amp.Type = dfs.Tables[0].Rows[26].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.Amp.Category = dfs.Tables[0].Rows[j].ItemArray[27].ToString();
                                    ////IDictionary dictionar;// ="";
                                    ////KeyCollection Kes;//= dfs.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
                                    //Dictionary<string, float> FG = new Dictionary<string, float>();
                                    //string[] strArrK = null;
                                    //string[] strArrV = null;
                                    ////int count = 0;
                                    ////str = "CSharp split test";
                                    //char[] splitchar = { ';' };
                                    //strArrK = dfs.Tables[0].Rows[28].ItemArray[0].ToString().Split(splitchar);
                                    //strArrV = dfs.Tables[0].Rows[29].ItemArray[0].ToString().Split(splitchar);
                                    //for (int k = 0; k <= strArrK.Length - 1; k++) FG.Add(strArrK[k], strArrV[k].ToInt32());
                                    //data.TonesRS2014[j].GearList.Amp.KnobValues = FG;
                                    ////data.TonesRS2014[j].GearList.Amp.KnobValues.Keys = dfs.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
                                    ////data.TonesRS2014[j].GearList.Amp.KnobValues.Keys = float.Parse((dfs.Tables[0].Rows[29].ItemArray[0].ToString());
                                    //data.TonesRS2014[j].GearList.Amp.PedalKey = dfs.Tables[0].Rows[j].ItemArray[30].ToString();
                                    //data.TonesRS2014[j].GearList.Cabinet.Category = dfs.Tables[0].Rows[j].ItemArray[31].ToString();
                                    //Dictionary<string, float> FS = new Dictionary<string, float>();
                                    //strArrK = dfs.Tables[0].Rows[32].ItemArray[0].ToString().Split(splitchar);
                                    //strArrV = dfs.Tables[0].Rows[33].ItemArray[0].ToString().Split(splitchar);
                                    //for (int k = 0; k <= strArrK.Length - 1; k++)  FG.Add(strArrK[k], strArrV[k].ToInt32());
                                    //data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
                                    ////data.TonesRS2014[j].GearList.Cabinet.KnobValues.Values = float.Parse((dfs.Tables[0].Rows[32].ItemArray[0].ToString());
                                    ////data.TonesRS2014[j].GearList.Cabinet.KnobValues.Keys = dfs.Tables[0].Rows[33].ItemArray[0].ToString();
                                    //data.TonesRS2014[j].GearList.Cabinet.PedalKey = dfs.Tables[0].Rows[j].ItemArray[34].ToString();
                                    //data.TonesRS2014[j].GearList.Cabinet.Type = dfs.Tables[0].Rows[j].ItemArray[35].ToString();

                                }
                                //}
                            }


                            //Add Arrangements
                            norec = 0;
                            string sds = "";
                            DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID = " + ID + "; ", "", cnb);

                            norec = ds.Tables[0].Rows.Count;
                            if (info.Arrangements.Capacity < norec)
                                data.Arrangements.Add(new RocksmithToolkitLib.DLCPackage.Arrangement
                                {
                                    Name = RocksmithToolkitLib.Sng.ArrangementName.Vocals,
                                    ArrangementType = ArrangementType.Vocal,
                                    ScrollSpeed = 20,

                                    Id = IdGenerator.Guid(),
                                    MasterId = RandomGenerator.NextInt(),
                                    //SongXml = new SongXML { File = xmlFile },
                                    //SongFile = new SongFile { File = "" },
                                    CustomFont = false
                                });
                            //norec += 1;
                            if (norec > data.Arrangements.Count)
                            {
                                //mArr.Add(GenMetronomeArr(data.Arrangements));
                                //mArr[0].SongXml.File = "1";
                                //var mArr = new List<Arrangement>();
                                //data.Arrangements.Capacity = 5;
                                //data.Arrangements.Add(data.Arrangements[j]);
                                // Add Vocal Arrangement
                                data.Arrangements.Add(new Arrangement
                                {
                                    Name = ArrangementName.Vocals,
                                    ArrangementType = ArrangementType.Vocal,
                                    ScrollSpeed = 20,
                                    //SongXml = new SongXML { File = xmlFile },
                                    //SongFile = new SongFile { File = "" },
                                    CustomFont = false
                                });
                                //norec += 1;
                            }
                            var n = 0;
                            foreach (var arg in info.Arrangements)//, Type
                            {

                                //for (int j = 0; j < norec; j++)
                                //{

                                sds = ds.Tables[0].Rows[n].ItemArray[1].ToString();
                                //data.Arrangements[n].Name = ArrangementName.Vocals;
                                //data.Arrangements[n].Name = ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementName.Bass : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Lead" ? RocksmithToolkitLib.Sng.ArrangementName.Lead : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Vocals" ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : ds.Tables[0].Rows[n].ItemArray[1].ToString() == "Rhythm" ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : ds.Tables[0].Rows[n].ItemArray[12].ToString() == "ShowLights" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
                                data.Arrangements[n].Name = (sds == "3" ? ArrangementName.Bass : (sds == "0" ? ArrangementName.Lead : (sds == "4" ? ArrangementName.Vocals : (sds == "1" ? ArrangementName.Rhythm : (sds == "6" ? ArrangementName.ShowLights : ArrangementName.Combo)))));
                                data.Arrangements[n].BonusArr = ds.Tables[0].Rows[n].ItemArray[3].ToString().ToLower() == "true" ? true : false;
                                sds = ds.Tables[0].Rows[n].ItemArray[4].ToString();
                                data.Arrangements[n].SongXML = new SongFile { File = ds.Tables[0].Rows[n].ItemArray[4].ToString() == "" ? ds.Tables[0].Rows[n].ItemArray[5].ToString().Replace(".xml", ".json") : ds.Tables[0].Rows[n].ItemArray[4].ToString() }; // if (File.Exists(sds))
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
                                if (ds.Tables[0].Rows[n].ItemArray[18].ToString() != "") data.Arrangements[n].TuningStrings.String0 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[18].ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[19].ToString() != "") data.Arrangements[n].TuningStrings.String1 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[19].ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[20].ToString() != "") data.Arrangements[n].TuningStrings.String2 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[20].ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[21].ToString() != "") data.Arrangements[n].TuningStrings.String3 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[21].ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[22].ToString() != "") data.Arrangements[n].TuningStrings.String4 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[22].ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[23].ToString() != "") data.Arrangements[n].TuningStrings.String5 = Int16.Parse(ds.Tables[0].Rows[n].ItemArray[23].ToString());
                                data.Arrangements[n].PluckedType = ds.Tables[0].Rows[n].ItemArray[24].ToString() == "Picked" ? PluckedType.Picked : PluckedType.NotPicked;
                                data.Arrangements[n].RouteMask = ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Bass" ? RouteMask.Bass : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Lead" ? RouteMask.Lead : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[n].ItemArray[25].ToString() == "None" ? RouteMask.None : RouteMask.Any;
                                //data.Arrangements[n].SongXml.Name = ds.Tables[0].Rows[n].ItemArray[26].ToString();
                                //data.Arrangements[n].SongXml.LLID = ds.Tables[0].Rows[n].ItemArray[27].ToInt32().ToInt32();
                                if (ds.Tables[0].Rows[n].ItemArray[28].ToString() != "") data.Arrangements[n].SongXml.UUID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[28].ToString());
                                else data.Arrangements[n].SongXml.UUID = Guid.NewGuid();
                                //data.Arrangements[n].SongFile.Name = ds.Tables[0].Rows[n].ItemArray[29].ToString();
                                //data.Arrangements[n].SongFile.LLID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[30].ToString().ToString());
                                if (ds.Tables[0].Rows[n].ItemArray[31].ToString() != "") data.Arrangements[n].SongXML.UUID = Guid.Parse(ds.Tables[0].Rows[n].ItemArray[31].ToString());
                                else data.Arrangements[n].SongXML.UUID = Guid.NewGuid();
                                //data.Arrangements[n].SongXML.
                                data.Arrangements[n].ToneMultiplayer = ds.Tables[0].Rows[n].ItemArray[32].ToString();
                                data.Arrangements[n].ToneA = ds.Tables[0].Rows[n].ItemArray[33].ToString();
                                data.Arrangements[n].ToneB = ds.Tables[0].Rows[n].ItemArray[34].ToString();
                                data.Arrangements[n].ToneC = ds.Tables[0].Rows[n].ItemArray[35].ToString();
                                data.Arrangements[n].ToneD = ds.Tables[0].Rows[n].ItemArray[36].ToString();
                                n++;
                            }
                        }
                        timestamp = UpdateLog(timestamp, "End Loading song..", true, logPath, TempPath, multithreadname, form, null, null);

                        //get track no
                        //string s = "";
                        if ((ConfigRepository.Instance()["dlcm_AdditionalManipul58"] == "Yes" || ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes") && netstatus != "NOK")
                        {
                            //var CleanTitle = "";
                            //if (data.SongInfo.SongDisplayName.IndexOf("[") > 0) CleanTitle = data.SongInfo.SongDisplayName.Substring(0, data.SongInfo.SongDisplayName.IndexOf("["));
                            //if (data.SongInfo.SongDisplayName.IndexOf("]") > 0) CleanTitle += data.SongInfo.SongDisplayName.Substring(data.SongInfo.SongDisplayName.IndexOf("]"), data.SongInfo.SongDisplayName.Length - data.SongInfo.SongDisplayName.IndexOf("]"));
                            //else if (data.SongInfo.SongDisplayName.IndexOf("[") == 0 || data.SongInfo.SongDisplayName.Substring(0, 1) != "[") CleanTitle = data.SongInfo.SongDisplayName;

                            //string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
                            //txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
                            //int z = await GetTrackNoFromSpotifyAsync(txt_Artist.Text, txt_Album.Text, txt_Title.Text, txt_Album_Year.Text, txt_SpotifyStatus.Text);
                            //txt_Track_No.Text = z == 0 && txt_Track_No.Text != "" ? txt_Track_No.Text : z.ToString();


                            //if (netstatus == "OK")
                            //{
                            try
                            {
                                Task<string> sptyfy = StartToGetSpotifyDetails(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName, info.SongInfo.SongYear.ToString(), "");
                                //s = sptyfy.ToString();  
                                var trackno = sptyfy.Result.Split(';')[0].ToInt32();
                                SongRecord[0].Track_No = trackno.ToString();
                                var SpotifySongID = sptyfy.Result.Split(';')[1];
                                var SpotifyArtistID = sptyfy.Result.Split(';')[2];
                                var SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                                var SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                                var SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                                UpdateLog(DateTime.Now, "Retrieved Spotify details " + SpotifyAlbumPath, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes" && SpotifySongID != "" && SpotifySongID != "-" && trackno > 0)
                                {
                                    var cmds = "UPDATE Main SET Spotify_Song_ID=\"" + SpotifySongID + "\", Spotify_Artist_ID=\"" + SpotifyArtistID + "\"";
                                    cmds += ", Spotify_Album_ID=\"" + SpotifyAlbumID + "\"" + ", Spotify_Album_URL=\"" + SpotifyAlbumURL + "\"";// + ",Spotify_Album_Path=\"" + SpotifyAlbumPath + "\"";
                                    cmds += " WHERE ID=" + filez.ID;
                                    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmds + ";", cnb);
                                    //ADD STADARDISATION UPDATE
                                    //Updating the Standardization table
                                    //DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\""
                                    //    + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", ConfigRepository.Instance()["dlcm_DBFolder"], cnb);

                                    //if (dzs.Tables[0].Rows.Count == 0)
                                    //{
                                    var updcmd = "UPDATE Standardization SET SpotifyArtistID=\"" + SpotifyArtistID + "\" , SpotifyAlbumID=\"" + SpotifyArtistID + "\", SpotifyAlbumURL=\""
                                        + SpotifyAlbumURL + "\", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\" WHERE (Artist=\"" + info.SongInfo.Artist + "\" OR Artist_Correction=\""
                                        + info.SongInfo.Artist + "\") AND (Album=\"" + info.SongInfo.Album + "\" OR Album_Correction=\"" + info.SongInfo.Album + "\")";

                                    UpdateDB("Standardization", updcmd + ";", cnb);
                                    //}
                                }
                            }
                            catch (Exception ex) { var tust = "Spotify Error ..." + ex; UpdateLog(DateTime.Now, tust, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null); }
                            //info.t = s.Split(';')[0];
                            //}

                            //SpotifySongID = s.Split(';')[1];
                            //SpotifyArtistID = s.Split(';')[2];
                            //SpotifyAlbumID = s.Split(';')[3];
                            //SpotifyAlbumURL = s.Split(';')[4];
                            // GetTrackNoSpotifyAsync(info.SongInfo.Artist, info.SongInfo.Album, info.SongInfo.SongDisplayName));

                        }

                        //Gather song Lenght
                        //var duration = "";
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
                                //if ((PreviewLenght.Split(':'))[0] == "00" && (PreviewLenght.Split(':'))[1] == "00")
                                PreviewLenght = (PreviewLenght.Split(':')[0].ToInt32() * 3600 + PreviewLenght.Split(':')[1].ToInt32() * 60 + PreviewLenght.Split(':')[2].ToInt32()).ToString();
                                //C:\GitHub\rocksmith - custom - song - toolkit\RocksmithTookitGUI\DLCManager\DLCManager.cs(5869):                                        DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                //else
                                //    PreviewLenght = PreviewLenght;

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
                           || (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght == "" ? "0" : PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"]))
                           && info.OggPath != null)))
                        {
                            //timestamp = UpdateLog(timestamp, "Fixing Preview ("+ PreviewLenght, true, logPath, TempPath, "", "", null, null);
                            var d1 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
                            if (d1.Split(';')[0] == "1")
                            {

                                if ((ConfigRepository.Instance()["dlcm_AdditionalManipul9"] == "Yes" && (info.OggPreviewPath == null || info.OggPreviewPath == "")) || (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"])) && info.OggPath != null)
                                {
                                    //tst = "start set preview"; timestamp = UpdateLog(timestamp, tst, true, logPath, TempPath, "", "", null, null);
                                    cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath,Folder_Name FROM Main WHERE ";
                                    cmd += "ID=" + ID + "";
                                    if (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && float.Parse(PreviewLenght) > float.Parse(ConfigRepository.Instance()["dlcm_MaxPreviewLenght"]) && info.OggPath != null) DeleteFile(info.OggPreviewPath);
                                    FixMissingPreview(cmd, cnb, AppWD, null, null, false);
                                }
                                //tst = "end set preview ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, TempPath, "", "", null, null);
                                //tst = "start encoding Main audio to 128kb from... " + bitrate + " ... " + SampleRate; timestamp = UpdateLog(timestamp, tst, true, logPath, TempPath, "", "", null, null);
                                if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && info.OggPath != null)
                                {
                                    {
                                        //pB_ReadDLCs.CreateGraphics().DrawString("start encoding Main audio to 128kb from... " + bitrate + " ... " + SampleRate, new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                                        cmd = "SELECT ID,AudioPath,audioBitrate,audioSampleRate,audioPreviewPath, oggPreviewPath FROM Main WHERE (VAL(audioBitrate) > " + (ConfigRepository.Instance()["dlcm_MaxBitRate"]) + " or VAL(audioSampleRate) > " + (ConfigRepository.Instance()["dlcm_MaxSampleRate"]) + ")";
                                        cmd += " AND ID=" + ID + "";
                                        FixAudioIssues(cmd, cnb, AppWD, null, null, false);
                                    }
                                    //tst = "end set encoding to128kb 44khz ..."; timestamp = UpdateLog(timestamp, tst, true, logPath, TempPath, "", "", null, null);
                                }

                            }
                            //if (d1.Split(';')[1] == "1") ;
                            if (d1.Split(';')[2] == "1") { Application.Exit(); }
                        }
                        //if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && filez.AudioPath != null && (bitrate > ConfigRepository.Instance()["dlcm_MaxBitrate"].ToInt32()))
                        //{
                        //    //tsst = "START set main audio reconv ..." + bitrate; timestamp = UpdateLog(timestamp, tsst, false);
                        //    var d3 = WwiseInstalled("Convert Audio if bitrate> ConfigRepository");
                        //    if (d3.Split(';')[0] == "1")
                        //    {
                        //Downstream(filez.AudioPath);
                        //        using (var vorbis = new NVorbis.VorbisReader(filez.OggPath))
                        //        {
                        //            bitrate = vorbis.NominalBitrate;
                        //            SampleRate = vorbis.SampleRate;
                        //        }

                        //        //save new new hash
                        //        cmd = "UPDATE Main SET ";
                        //        var audio_hash = "";
                        //        audio_hash = GetHash(filez.AudioPath);
                        //        cmd += "Audio_Hash=\"" + audio_hash + "\", audioBitrate=\"" + bitrate + "\"";
                        //        cmd += ",audioSampleRate=\"" + SampleRate + "\"";
                        //        cmd += " WHERE ID=" + filez.ID;
                        //        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                        //    }

                        //if (d3.Split(';')[1] == "1") break;
                        //if (d3.Split(';')[2] == "1")
                        //{
                        //    btn_RePack.Text = "RePack";
                        //    if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();
                        //}
                        //tsst = "end set main audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        //}

                        //Fix bug
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes")
                        {
                            GenericFunctions.Converters(filez.oggPreviewPath, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
                            if (File.Exists(filez.oggPreviewPath.Replace(".ogg", ".wem")))
                            {
                                //fix as sometime the template folder gets poluted and breaks eveything
                                var appRootDir = Path.GetDirectoryName(Application.ExecutablePath);
                                var templateDir = Path.Combine(appRootDir, "Template");
                                var backup_dir = AppWD + "\\Template";
                                DeleteDirectory(templateDir);
                                CopyFolder(backup_dir, templateDir);
                            }
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", ".wav"));
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.wav"));
                            //File.Delete(file.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", ".wav"));
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview.wav"));
                            DeleteFile(filez.oggPreviewPath.Replace(".ogg", "_preview.ogg"));
                            //tsst = "recompress preview...bbug..wierd..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }
                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul71"] == "Yes")
                        {
                            //var sel = "SELECT ID FROM Pack_AuditTrail WHERE FileHash=\"" + "" + "\" OR (FileName=\"" + "" + "\" AND PackPath=\"" + "" + "\");";
                            //DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", sel);
                            //tsst = "fix originals..."; timestamp = UpdateLog(timestamp, tsst, false);
                        }

                        //Set Preview
                        //if ((ConfigRepository.Instance()["dlcm_AdditionalManipul9"] == "Yes" && filez.oggPreviewPath == null ||
                        //    (ConfigRepository.Instance()["dlcm_AdditionalManipul55"] == "Yes" && (filez.AudioPreview_Hash == filez.Audio_Hash
                        //    || filez.Song_Lenght == filez.PreviewLenght || recalc_Preview))))
                        //{
                        //    var PreviewLenght = "";
                        //    //delete old previews!
                        //    if (filez.oggPreviewPath != null) DeleteFile(filez.oggPreviewPath);
                        //    if (filez.audioPreviewPath != null) DeleteFile(filez.audioPreviewPath);

                        //    var startInfo = new ProcessStartInfo();
                        //    startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
                        //    startInfo.WorkingDirectory = AppWD;
                        //    var t = filez.OggPath;
                        //    var tt = t.Replace(".ogg", "_preview.ogg");
                        //    var times = ConfigRepository.Instance()["dlcm_PreviewStart"];
                        //    string[] timepieces = times.Split(':');
                        //    TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
                        //    startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                        //                                        t,
                        //                                        tt,
                        //                                        r.TotalMilliseconds,
                        //                                        (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
                        //    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                        //    //save new previews
                        //    cmd = "UPDATE Main SET ";
                        //    if (File.Exists(filez.oggPreviewPath))
                        //        if (PreviewLenght == "" || PreviewLenght == null)
                        //            using (var vorbis = new NVorbis.VorbisReader(filez.oggPreviewPath))
                        //            {
                        //                var durations = vorbis.TotalTime;
                        //                bitratep = vorbis.NominalBitrate;
                        //                PreviewLenght = durations.ToString();
                        //            }
                        //    var audioPreview_hash = "";
                        //    audioPreview_hash = GetHash(filez.audioPreviewPath);

                        //    cmd += " audioPreviewPath=\"" + filez.audioPreviewPath + "\"" + " , audioPreview_Hash=\"" + audioPreview_hash + "\"" + " , PreviewTime=\"" + times + "\", audioBitrate =\"" + bitratep + "\"";
                        //    cmd += " , oggPreviewPath=\"" + filez.oggPreviewPath + "\" , PreviewLenght=\"" + (PreviewLenght.IndexOf(":") > 0 ? (PreviewLenght.Split(':'))[2] : PreviewLenght) + "\"";// previewN + "\"";

                        //    cmd += " WHERE ID=" + filez.ID;
                        //    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                        //tsst = "end set preview ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        //}

                        //compress Preview
                        //if (ConfigRepository.Instance()["dlcm_AdditionalManipul69"] == "Yes" && filez.audioPreviewPath != null && (bitratep > ConfigRepository.Instance()["dlcm_MaxBitRate"].ToInt32()))
                        //{
                        //tsst = "start set preview audio reconv ..." + bitratep; timestamp = UpdateLog(timestamp, tsst, false);
                        //  var d4 = WwiseInstalled("Convert Preview Audio if bitrate> ConfigRepository");
                        //   if (d4.Split(';')[0] == "1")
                        //{
                        //Downstream(filez.audioPreviewPath, bitratep);
                        //save new new hash
                        //cmd = "UPDATE Main SET ";
                        //var audio_previewhash = "";
                        //audio_previewhash = GetHash(filez.audioPreviewPath);
                        //cmd += "audioPreview_Hash=\"" + audio_previewhash + "\"";
                        //cmd += " WHERE ID=" + filez.ID;
                        //        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                        //}

                        //if (d4.Split(';')[1] == "1") break;
                        //if (d4.Split(';')[2] == "1")
                        //{
                        //    btn_RePack.Text = "RePack";
                        //    if (bwRGenerate.WorkerSupportsCancellation == true) bwRGenerate.CancelAsync();
                        //}
                        //tsst = "end set preview audio reconv ..."; timestamp = UpdateLog(timestamp, tsst, false);
                        //}


                        data.ToolkitInfo = new RocksmithToolkitLib.DLCPackage.ToolkitInfo();
                        data.ToolkitInfo.PackageAuthor = filez.Author;
                        if ((filez.Author == "Custom Song Creator" || filez.Author == "") && ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" && filez.Is_Original != "Yes")
                        {
                            //saving in the txt file is not rally usefull as the system gen the file at every pack :)
                            filez.Author = "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
                            data.ToolkitInfo.PackageAuthor = "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
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

                        DirectoryInfo di;
                        var repacked_Path = TempPath + "\\0_repacked";
                        if (!Directory.Exists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

                        var norm_path = "";
                        if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")
                            norm_path = repacked_Path + "\\" + Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved, SongRecord, "[", "]");
                        else
                            norm_path = ((filez.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear.ToString() + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                        //manipulating the info

                        if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") data.SongInfo.SongDisplayName = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved, SongRecord, "[", "]");
                        if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") data.SongInfo.SongDisplayNameSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_Sort"], 0, false, false, bassRemoved, SongRecord, "", "");
                        if (chbx_Beta) if (chbx_Group != "") data.SongInfo.SongDisplayNameSort = "0" + Groupss + data.SongInfo.SongDisplayNameSort.Substring(1, data.SongInfo.SongDisplayNameSort.Length - 2); //).Replace("][", "-").Replace("]0", "");

                        if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") data.SongInfo.Artist = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved, SongRecord, "[", "]");
                        if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved, SongRecord, "", "");
                        if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") data.SongInfo.Album = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved, SongRecord, "[", "]");

                        //pB_ReadDLCs.Increment(1);
                        if (chbx_UniqueID)
                        {
                            Random random = new Random();
                            data.Name = random.Next(0, 100000) + data.Name;
                            norm_path += data.Name;
                        }

                        //Fix the _preview_preview issue
                        var ms = data.OggPath;
                        //var tst = "";
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
                            var tust = "Error ..." + ex; UpdateLog(DateTime.Now, tust, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                            //MessageBox.Show(ex.Message);
                        }
                        if (data == null)
                            UpdateLog(DateTime.Now, "One or more fields are missing information.", false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        //MessageBox.Show(, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //if (ConfigRepository.Instance()["dlcm_AdditionalManipul70"] == "Yes")
                        //{
                        //    GenericFunctions.Converters(data.OggPreviewPath.Replace(".wem", ".ogg"), GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
                        //    DeleteFile(data.OggPreviewPath.Replace(".wem", ".wav"));
                        //}

                        //Add comments to beginning of the lyrics
                        //var der=chbx_Additional_Manipulations.GetItemChecked(72);
                        //var ft=file.Has_Vocals;
                        var ttt2 = (ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && filez.Has_Vocals == "Yes") ? AddStuffToLyrics(filez.ID, filez.Comments, Groupss, filez.Has_DD, (filez.Has_BassDD == "Yes") ? "No" : "Yes", filez.Has_BassDD, filez.Author, filez.Is_Acoustic, filez.Is_Live, filez.Live_Details, filez.Is_Multitrack, filez.Is_Original, cnb) : "";
                        var ttt1 = ConfigRepository.Instance()["dlcm_AdditionalManipul74"] == "Yes" && filez.Has_Vocals == "Yes" ? AddTrackStart2Lyrics(filez.ID, cnb) : "";

                        //var FN = txt_File_Name.Text;

                        var FN = ConfigRepository.Instance()["dlcm_File_Name"];
                        FN = Manipulate_strings(FN, 0, false, false, bassRemoved, SongRecord, "", "");
                        if (chbx_Beta) if (chbx_Group != "") FN = "0" + Groupss + FN.Substring(1, FN.Length - 2); //}).Replace("][", "-").Replace("]0", "");

                        if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes")
                        {
                            FN = FN.Replace(".", "_");
                            FN = FN.Replace(" ", "_");
                            FN = FN.Replace("__", "_");
                            FN = FN.Replace("/", "");
                        }
                        //dlcSavePath = repacked_Path + "\\" + chbx_Format + "\\" + FN;

                        data.ToolkitInfo.PackageVersion = filez.Version;

                        int progress = 0;
                        var errorsFound = new StringBuilder();
                        var numPlatforms = 0;
                        numPlatforms++;

                        var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                        timestamp = UpdateLog(timestamp, "Packing" + PreviewLenght, true, logPath, TempPath, multithreadname, form, null, null);

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
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                                error = true;
                                error_reason = "@PC pack";
                            }

                        if (chbx_Mac == "Mac")
                            try
                            {
                                if (!File.Exists(data.ArtFiles[0].destinationFile)) File.Copy(data.ArtFiles[0].sourceFile, data.ArtFiles[0].destinationFile);
                                if (!File.Exists(data.ArtFiles[1].destinationFile)) File.Copy(data.ArtFiles[1].sourceFile, data.ArtFiles[1].destinationFile);
                                if (!File.Exists(data.ArtFiles[2].destinationFile)) File.Copy(data.ArtFiles[2].sourceFile, data.ArtFiles[2].destinationFile);
                                dlcSavePath = repacked_Path + "\\" + chbx_Mac + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                error = true;
                                error_reason = "@Mac pack";
                            }

                        if (chbx_XBOX == "XBOX360")
                            try
                            {
                                if (!File.Exists(data.ArtFiles[0].destinationFile)) File.Copy(data.ArtFiles[0].sourceFile, data.ArtFiles[0].destinationFile);
                                if (!File.Exists(data.ArtFiles[1].destinationFile)) File.Copy(data.ArtFiles[1].sourceFile, data.ArtFiles[1].destinationFile);
                                if (!File.Exists(data.ArtFiles[2].destinationFile)) File.Copy(data.ArtFiles[2].sourceFile, data.ArtFiles[2].destinationFile);
                                dlcSavePath = repacked_Path + "\\" + chbx_XBOX + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                                error = true;
                                error_reason = "@XBOX pack";
                            }

                        if (chbx_PS3 == "PS3")
                            try
                            {
                                if (!File.Exists(data.ArtFiles[0].destinationFile)) File.Copy(data.ArtFiles[0].sourceFile, data.ArtFiles[0].destinationFile);
                                if (!File.Exists(data.ArtFiles[1].destinationFile)) File.Copy(data.ArtFiles[1].sourceFile, data.ArtFiles[1].destinationFile);
                                if (!File.Exists(data.ArtFiles[2].destinationFile)) File.Copy(data.ArtFiles[2].sourceFile, data.ArtFiles[2].destinationFile);
                                dlcSavePath = repacked_Path + "\\" + chbx_PS3 + "\\" + FN;
                                RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                                progress += step;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                                {
                                    ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                                    frm1.ShowDialog();
                                }
                                string ss = String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace);
                                //MessageBox.Show(ex + ss);
                                var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                                error = true;
                                error_reason = "@PS3 pack" + ex;
                            }
                        data.CleanCache();
                        i++;
                    }
                    //pB_ReadDLCs.Increment(1);
                    h = dlcSavePath;
                }

                //Restore XML changes
                if (!((chbx_Last_Packed && chbx_Last_PackedEnabled) || (chbx_CopyOld && chbx_CopyOldEnabled)) && ConfigRepository.Instance()["dlcm_AdditionalManipul86"] == "No")
                {
                    Platform platformz = Folder_Name.GetPlatform();
                    var xmlFilez = Directory.GetFiles(Folder_Name, "*.old", System.IO.SearchOption.AllDirectories);
                    foreach (var xml in xmlFilez)
                    {
                        //Song2014 xmlContent = null;
                        if (xml.ToLower().IndexOf("showlights") < 0)/*&& xml.IndexOf(".old") > 0*/
                            try
                            {
                                File.Copy(xml, xml.Replace(".old", ""), true);
                                timestamp = UpdateLog(timestamp, "Restore XML", true, logPath, TempPath, multithreadname, form, null, null);
                                DeleteFile(xml);//if (File.Exists(xml.Replace(".old", "")))
                            }
                            catch (Exception ex) { var tgst = "Error at restoring xml changes..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                    }
                }

                if (h != "")
                {
                    timestamp = UpdateLog(timestamp, "Start the Copy/FTPing process", true, logPath, TempPath, multithreadname, form, null, null);
                    var source = "";
                    //calc hash and file size
                    System.IO.FileInfo fi = null;
                    try
                    {
                        fi = new System.IO.FileInfo(source);
                        if (fi.Length == 0) error = true;
                        else
                        {
                            string copyftp = "";
                            //pB_ReadDLCs.Increment(1);
                            var dest = "";
                            //source = "";
                            if (chbx_PS3 == "PS3" && chbx_Copy)
                            {
                                source = h + "_ps3.psarc.edat";
                                var u = ""; if (chbx_Replace && chbx_ReplaceEnabled) u = DeleteFTPFiles(txt_RemotePath, txt_FTPPath);
                                var a = "";
                                if (File.Exists(source))
                                {
                                    FTPFile(txt_FTPPath, source, TempPath, SearchCmd, cnb);
                                    copyftp = " and " + a + " FTPed(PS3)";
                                }
                                else {error = true; error_reason = "@FTP";}
                                //Add Pack Audit Trail
                                if (!(chbx_CopyOld && chbx_CopyOldEnabled) && !(chbx_Last_Packed && chbx_Last_PackedEnabled) && !error) //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                {

                                    var FileHash = GetHash(source);//Generating the HASH code

                                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);

                                    var norec = 0;
                                    norec = dfs.Tables[0].Rows.Count;
                                    if (norec == 0)
                                    {
                                        var sourcedir = source.Replace(Path.GetFileName(source), "");
                                        string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                        var insertA = "\"" + dest + "\",\"" + sourcedir.Remove(sourcedir.Length - 1) + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + ID + ",\"" + DLC_Name + "\",\"" + chbx_Format + "\"";

                                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                                    }
                                }
                            }

                            if (chbx_PC == "PC" && chbx_Copy)
                            {
                                var platfrm = "_p";
                                //source = h + (h.IndexOf(platfrm + ".psarc") > 0 ? "" : platfrm + ".psarc");
                                dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
                                try
                                {
                                    var fn = source;//.Substring(source.IndexOf("PC\\") + 3, source.Length - source.IndexOf("PS3\\") - 3);
                                    DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\" AND ID=" + ID + ";", cnb);
                                    if (chbx_Replace && chbx_ReplaceEnabled && File.Exists(txt_RemotePath)) File.Move(txt_RemotePath, txt_RemotePath.Replace(platfrm + ".psarc", ".old"));
                                    File.Copy(source, dest, true);
                                }
                                catch (Exception ex) { error = true; copyftp = "Not"; var tgst = "Error @copy after pack..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null); }
                                copyftp = " and " + copyftp + " Copied(PC)";
                                //Add Pack Audit Trail
                                if (!(chbx_CopyOld && chbx_CopyOldEnabled) && !(chbx_Last_Packed && chbx_Last_PackedEnabled) && !error) //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                {

                                    var FileHash = GetHash(source);//Generating the HASH code

                                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);

                                    var norec = 0;
                                    norec = dfs.Tables[0].Rows.Count;
                                    if (norec == 0)
                                    {
                                        var sourcedir = source.Replace(Path.GetFileName(source), "");
                                        string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                        var insertA = "\"" + dest + "\",\"" + sourcedir.Remove(sourcedir.Length - 1) + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + ID + ",\"" + DLC_Name + "\",\"" + chbx_Format + "\"";

                                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                                    }
                                }
                            }

                            if (chbx_Mac == "Mac" && chbx_Copy)
                            {
                                var platfrm = "_m";
                                //source = h + (h.IndexOf(platfrm + ".psarc") > 0 ? "" : platfrm + ".psarc");
                                dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
                                try
                                {
                                    var fn = source;//.Substring(source.IndexOf("PC\\") + 3, source.Length - source.IndexOf("PS3\\") - 3);
                                    DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\" AND ID=" + ID + ";", cnb);
                                    if (chbx_Replace && chbx_ReplaceEnabled && File.Exists(txt_RemotePath)) File.Move(txt_RemotePath, txt_RemotePath.Replace(platfrm + ".psarc", ".old"));
                                    File.Copy(source, dest, true);
                                }
                                catch (Exception ex) { error = true; copyftp = "Not"; var tgst = "Error @copy after pack..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null); }
                                copyftp = " and " + copyftp + " Copied(Mac)";

                                //else if (chbx_Format == "PS3") source = h + "_ps3.psarc.edat";
                                //else source = h + (chbx_Format == "PC" ? "_p" : (chbx_Format == "Mac" ? "_m" : "")) + ".psarc";

                                //Add Pack Audit Trail
                                if (!(chbx_CopyOld && chbx_CopyOldEnabled) && !(chbx_Last_Packed && chbx_Last_PackedEnabled) && !error) //&& oldfilePath.GetPlatform().platform.ToString() != chbx_Format
                                {

                                    var FileHash = GetHash(source);//Generating the HASH code

                                    DataSet dfs = new DataSet(); dfs = SelectFromDB("Pack_AuditTrail", "SELECT * FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";", "", cnb);

                                    var norec = 0;
                                    norec = dfs.Tables[0].Rows.Count;
                                    if (norec == 0)
                                    {
                                        var sourcedir = source.Replace(Path.GetFileName(source), "");
                                        string insertcmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name, Platform";
                                        var insertA = "\"" + dest + "\",\"" + sourcedir.Remove(sourcedir.Length - 1) + "\",\"" + Path.GetFileName(source) + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + ID + ",\"" + DLC_Name + "\",\"" + chbx_Format + "\"";

                                        InsertIntoDBwValues("Pack_AuditTrail", insertcmdA, insertA, cnb, 0);
                                    }
                                }
                            }
                        }
                        //pB_ReadDLCs.Increment(1);
                        timestamp = UpdateLog(timestamp, "Stop the Copy/FTPing process", true, logPath, TempPath, multithreadname, form, null, null);
                    }
                    catch (Exception ex)
                    {
                        var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], multithreadname, form, null, null);
                        ErrorWindow frm1 = new ErrorWindow(ex.Message + "DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ ", "https://www.microsoft.com/en-us/download/details.aspx?id=39358", "Error @Import", false, false); //access 2016 file x86 32bitsacedbole6  https://www.microsoft.com/en-us/download/details.aspx?id=50040 //access 2013 file x86 32bitsacedbole15 https://www.microsoft.com/en-us/download/details.aspx?id=39358 //access 2007 smaller file x86 32bitsacedbole12 https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734
                        frm1.ShowDialog(); error = true;
                    }
                }
                else { error = true; error_reason = "Packed path is empty"; }
                //else { error = true; ; error_reason = "file doesnt exists"; }
                //timestamp = UpdateLog(timestamp, "End Packing", true, logPath, TempPath, "", "", null, null);
                cnb.Close();
            }
            catch (Exception ex)
            {
                error = true; error_reason = "?" + ex;
            }

            if (error)
            {
                UpdatePackingLog("LogPackingError", ConfigRepository.Instance()["dlcm_DBFolder"], packid, args[0], error_reason, cnb);
                timestamp = UpdateLog(timestamp, "End Packing", true, logPath, TempPath, multithreadname, form, null, null);
            }
            else UpdatePackingLog("LogPacking", ConfigRepository.Instance()["dlcm_DBFolder"], packid, args[0], "", cnb);
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
                    var tsst = "No Levels only Sections..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                catch (Exception ex) { var tsst = "Error @starttimevocals..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            }
            //for (var i = 0; i < xmlContent.Levels[0].Notes.Length; i++)
            //{
            //if (xmlContent.Levels[0].Notes[0].Time > 5)
            //{

            //break;
            //}
            //else startt = xmlContent.Vocal[i].Time.ToString();
            //}
            return startt;
        }

        public static string AddTrackStart2Lyrics(string SongID, OleDbConnection cnb)
        {
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            var noOfRec = dus.Tables[0].Rows.Count;
            var XMLFilePath = "";
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                XMLFilePath = ArrangementType == "Vocal" ? dus.Tables[0].Rows[i].ItemArray[0].ToString() : XMLFilePath;
            }
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                Add2LinesInVocals(XMLFilePath, 1);
                //XMLFilePath =  dus.Tables[0].Rows[i].ItemArray[0].ToString();// : XMLFilePath;ArrangementType == "Vocal" ?
                var RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                var strartt = dus.Tables[0].Rows[i].ItemArray[3].ToString();
                if (RouteMask == "Lead" || RouteMask == "Bass" || RouteMask == "Rhythm")
                {
                    Vocals xmlContent = null;
                    try
                    {
                        xmlContent = Vocals.LoadFromFile(XMLFilePath);
                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
                    //var startt = "";
                    for (var j = 1; j < xmlContent.Count; j++)
                        //{
                        if (xmlContent.Vocal[j].Time > float.Parse(strartt) && j > 0)
                        {
                            xmlContent.Vocal[j - 1].Lyric = "[" + (RouteMask == "Lead" ? "L" : RouteMask == "Bass" ? "B" : RouteMask == "Rhythm" ? "R" : "-") + "]";
                            xmlContent.Vocal[j - 1].Length = float.Parse("0.5");
                            xmlContent.Vocal[j - 1].Time = float.Parse(strartt);
                            ////if (File.Exists(XMLFilePath)) File.Delete(XMLFilePath);
                            //File.Copy(XMLFilePath, XMLFilePath + "orig",false);
                            using (var stream = File.Open(XMLFilePath, FileMode.Create))
                                xmlContent.Serialize(stream);
                            j = xmlContent.Count;
                        }
                        else { xmlContent.Vocal[j - 1].Lyric = xmlContent.Vocal[j].Lyric; xmlContent.Vocal[j - 1].Length = xmlContent.Vocal[j].Length; xmlContent.Vocal[j - 1].Time = xmlContent.Vocal[j].Time; }
                    //else if (xmlContent.Vocal[j].Time < float.Parse(strartt) && j == 0 && xmlContent.Vocal[j+1].Time > float.Parse(strartt))
                    //{
                    //    xmlContent.Vocal[0].Lyric += "Start-";
                    //    using (var stream = File.Open(XMLFilePath, FileMode.Create))
                    //        xmlContent.Serialize(stream);
                    //    continue;
                    //}
                    //}
                }
            }
            return "";
        }

        public static string AddStuffToLyrics(string SongID, string Comments, string Group, string Has_DD, string bassRemoved, string Has_BassDD, string Author, string Is_Acoustic, string Is_Live, string Live_Details, string Is_Multitrack, string Is_Original, OleDbConnection cnb)
        {
            var ST = "";
            var sdetails = "SongDetails: " + (Comments == "" ? "" : "Comment_" + Comments) + " DD=" + Has_DD + (Has_DD == "Yes" && bassRemoved == "Yes" ? "(BDDremoved)" : "") + (Author == "" ? "" : " by: " + Author) + (Is_Acoustic == "Yes" ? " Acoustic" : "") + (Is_Live == "Yes" ? " Live" : "" + Is_Live + (Live_Details == "" ? "" : "(" + Live_Details + ")")) + (Is_Original == "No" ? " Custom-DLC" : " ORIGINAL ") + " Tracks=";
            var scomments = "";
            var tsst = "Start TH ..."; var timestamp = UpdateLog(DateTime.Now, tsst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);


            //DataSet dus = new DataSet(); dus = SelectFromDB("Main", "SELECT XMLFilePath, Bonus, Comments FROM Main WHERE ArrangementType=\"ArrangementType\" AND DLC_ID=\""+ SongID + "\"", "");
            //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, Bonus, Comments, ArrangementType, RouteMask FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
            //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            var noOfRec = dus.Tables[0].Rows.Count;
            for (var i = 0; i <= noOfRec - 1; i++)
            {
                var XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();
                var Bonus = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                var Commentz = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                var ArrangementType = dus.Tables[0].Rows[i].ItemArray[3].ToString();
                var RouteMask = dus.Tables[0].Rows[i].ItemArray[4].ToString();
                if (ArrangementType == "Vocal") ST = XMLFilePath;//ArrangementType
                sdetails += (RouteMask == "Lead" ? " Lead" : "") + (RouteMask == "Bass" ? " Bass" : "") + (RouteMask == "Rhythm" ? " Rhythm" : "");
                sdetails += (RouteMask == "Lead" && Bonus.ToLower() == "true" ? " L_Bonus" : "") + (RouteMask == "Bass" && Bonus.ToLower() == "true" ? " B_Bonus" : "") + (RouteMask == "Rhythm" && Bonus.ToLower() == "true" ? " R_Bonus" : "");
                scomments += (RouteMask == "Lead" && Commentz != "" ? " L_" + Commentz : "") + (RouteMask == "Bass" && Commentz != "" ? " B_" + Commentz : "") + (RouteMask == "Rhythm" && Commentz != "" ? " R_" + Commentz : "");
            }
            if (!File.Exists(ST + ".old")) File.Copy(ST, ST + ".old");
            Add2LinesInVocals(ST, 2);

            Vocals newLyrics = null;
            try
            {
                newLyrics = Vocals.LoadFromFile(ST);//+ ".newvcl"
            }
            catch (Exception ex) { var tgst = "Error ..." + ex; UpdateLog(DateTime.Now, tgst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }

            newLyrics.Vocal[0].Time = float.Parse("0.61");
            newLyrics.Vocal[0].Note = 254;
            newLyrics.Vocal[0].Length = float.Parse("2.39");
            newLyrics.Vocal[0].Lyric = "Song Details: " + sdetails;
            newLyrics.Vocal[1].Time = 3;
            newLyrics.Vocal[1].Note = 254;
            newLyrics.Vocal[1].Length = 2;
            newLyrics.Vocal[1].Lyric = "Lyrics Details:" + scomments;

            //write new file
            using (var stream = File.Open(ST, FileMode.Create))
                newLyrics.Serialize(stream);
            //if (File.Exists(ST + ".newvcl"))
            //{ File.Delete(ST);
            //File.Copy(ST + ".newvcl", ST,true);
            //}
            tsst = "End add stuff to lyrics ..."; timestamp = UpdateLog(timestamp, tsst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);

            return ST;
        }
        public static void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (e.ProgressPercentage <= pB_ReadDLCs.Maximum)
            //    pB_ReadDLCs.Value = e.ProgressPercentage;
            //else
            //    pB_ReadDLCs.Value = pB_ReadDLCs.Maximum;

            ShowCurrentOperation(e.UserState as string);
        }
        public static void ShowCurrentOperation(string message)
        {
            //currentOperationLabel.Text = message;
            //currentOperationLabel.Refresh();
        }
        //private StringBuilder errorsFound;//bcapi1
        public static void ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(e.Result == null))
                switch (e.Result.ToString())
                {

                    case "generate":
                        var message = "Package was generated.";
                        if (errorsFound.Length > 0)
                            message = String.Format("Package was generated with errors! See below: {0}(1}", Environment.NewLine, errorsFound);
                        message += String.Format("{0}You want to open the folder in which the package was generated?{0}", Environment.NewLine);
                        if (MessageBox.Show(message, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(Path.GetDirectoryName("-"));
                        }
                        break;
                    case "error":
                        var message2 = String.Format("Package generation failed. See below: {0}{1}{0}", Environment.NewLine, errorsFound);
                        MessageBox.Show(message2, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Parent.Focus();
                        break;
                }
            //btn_RePack.Text = "RePack";
        }

        public static void FixBitrate(object sender, DoWorkEventArgs e)

        {
            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];

            string[] args = (e.Argument).ToString().Split(';');
            string cmd = args[0];
            string AudioPath = args[1];
            float bitrate = float.Parse(args[2]);
            float SampleRate = float.Parse(args[3]);
            string ID = args[4];
            string audioPreviewPath = args[5];
            string oggPreviewPath = args[6];
            string multithreadname = args[7];
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            do
                System.Threading.Thread.Sleep(1000);
            while (cnb.State.ToString() == "Connecting");
            cnb.Open(); //if (cnb.State.ToString()=="Closed") 

            var tsst = "Start FixBitRate TH ..." + AudioPath + "-" + bitrate + "-" + SampleRate; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);
            var d1 = WwiseInstalled("Convert Audio if bitrate > ConfigRepository");
            var audio_hash = "";
            if (d1.Split(';')[0] == "1")
            {
                if (File.Exists(AudioPath)) Downstream(AudioPath, bitrate);
                audio_hash = GetHash(AudioPath);
                using (var vorbis = new NVorbis.VorbisReader(AudioPath.Replace(".wem", "_fixed.ogg")))
                {
                    bitrate = vorbis.NominalBitrate;
                    SampleRate = vorbis.SampleRate;
                }
                cmd = "UPDATE Main SET ";
                cmd += "Audio_Hash=\"" + audio_hash + "\"" + ", audioBitrate =\"" + bitrate + "\"";
                cmd += ", audioSampleRate=\"" + SampleRate + "\"";
                cmd += " WHERE ID=" + ID;
                DataSet dios = new DataSet(); dios = UpdateDB("Main", cmd + ";", cnb);

                //Update Preview
                if (audioPreviewPath != null && audioPreviewPath != "")
                {
                    if (File.Exists(audioPreviewPath)) Downstream(audioPreviewPath, bitrate);
                    audio_hash = GetHash(audioPreviewPath);
                    cmd = "UPDATE Main SET ";
                    cmd += "audioPreview_Hash=\"" + audio_hash;
                    cmd += "\" WHERE ID=" + ID;
                    DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
                }
                //Delete any Wav file created..by....?ccc
                foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(AudioPath), "*.wav", System.IO.SearchOption.AllDirectories))
                {
                    DeleteFile(wav_name);
                }
            }
            e.Result = "done";
            cnb.Close();
            //sender.ReportProgress(100);            
            //MainDB.bwRFixAudio.ReportProgress(100);
            //MainDB.bwRFixAudio.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(ProcessCompleted);
            //e.Cancel = true;
            //MainDB.bwRFixAudio.ReportProgress(0);
            //MainDB.bwRFixAudio.IsBusy=false;
            //MainDB.bwRFixAudio.CancelAsync();
        }


        public static void FixPreview(object sender, DoWorkEventArgs e)

        {
            var startT = DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"];
            string tmpPath = ConfigRepository.Instance()["dlcm_TempPath"];

            //OggPath, AppWD, OggPreviewPath, cmd, Folder_Name, ID, cnb
            string[] args = (e.Argument).ToString().Split(';');
            string OggPath = args[0];
            string AppWD = args[1];
            string OggPreviewPath = args[2];
            string cmd = args[3];
            string Folder_Name = args[4];
            string ID = args[5];
            string multithreadname = args[6];
            var zt = ConfigRepository.Instance()["dlcm_DBFolder"];
            OleDbConnection cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";OLE DB Services=-2;Mode=Read;Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);
            cnb.Open();

            var tsst = "Start FixPreview TH ..."; DateTime timestamp = startT; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);

            Random randomp = new Random();
            var packid = randomp.Next(0, 100000);
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = OggPath.Replace(".wem", "_fixed.ogg"); //"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var tt = t.Replace("_fixed.ogg", "_preview.ogg");
            var times = ConfigRepository.Instance()["dlcm_PreviewStart"]; //00:30
            string[] timepieces = times.Split(':');
            var audioPreview_hash = "";
            //var PreviewTime = "";
            var PreviewLenght = "";
            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
            startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                t,
                                                tt,
                                                r.TotalMilliseconds,
                                                (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    tsst = "Cut Ogg for preview ..." + OggPath; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        var wwisePath = "";
                        if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                            wwisePath = ConfigRepository.Instance()["general_wwisepath"];
                        else
                            wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
                        if (wwisePath == "")
                        {
                            ErrorWindow frm1 = new ErrorWindow("In order to use the FixAudioIssues-Preview, please Install Wwise Launcher then Wwise v" + ConfigRepository.Instance()["general_wwisepath"] + " with Authoring binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                            frm1.ShowDialog();
                            if (frm1.StopImport) return;// "0";// break;
                                                        //if (frm1.StopImport) { j = 10; return "0"; }// break; }
                        }
                        if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath);
                        //pB_ReadDLCs.CreateGraphics().DrawString("Creating a preview", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                        tsst = "Convert to wem preview ..."; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);
                        var i = 0;
                        do
                        {
                            GenericFunctions.Converters(tt, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
                            i++;
                            if (!File.Exists(tt.Replace(".ogg", ".wem")))
                            {
                                //fix as sometime the template folder gets poluted and breaks eveything
                                var appRootDir = Path.GetDirectoryName(Application.ExecutablePath);
                                var templateDir = Path.Combine(appRootDir, "Template");
                                var backup_dir = AppWD + "\\Template";
                                DeleteDirectory(templateDir);
                                CopyFolder(backup_dir, templateDir);
                            }
                        }
                        while (!File.Exists(tt.Replace(".ogg", ".wem")) && i < 10);
                        if (File.Exists(OggPreviewPath.Replace(".wem", ".wav"))) DeleteFile(OggPreviewPath.Replace(".wem", ".wav"));
                        //if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath.Replace(".wem", ".ogg"));
                        OggPreviewPath = tt.Replace(".ogg", ".wem");
                        if (!File.Exists(tt.Replace(".ogg", ".wem")))
                        {
                            tsst = "error @ogg cut..."; timestamp = startT; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);
                        }
                    }
                    else tsst = "error @ogg cut..."; timestamp = startT; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);

                }

            var previewN = FixOggwDiffName(OggPreviewPath, Folder_Name, timestamp, tsst, logPath, tmpPath, multithreadname);//Fix _preview.OGG having a diff name than _preview.wem after oggged

            PreviewLenght = "";
            audioPreview_hash = "";
            //int SampleRate = 0;
            //int bitrate = 0;
            if (File.Exists(previewN))
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

                cmd = "UPDATE Main SET ";
                cmd += " audioPreviewPath=\"" + OggPreviewPath + "\" ,audioPreview_Hash =\"" + audioPreview_hash + "\"" + ", OggPreviewPath=\"" + previewN + "\", Has_Preview=\"Yes\"";// previewN + "\"";
                cmd += ", PreviewLenght=\"" + PreviewLenght + "\"";//, audioSampleRate=\"" + SampleRate + "\"";
                cmd += " WHERE ID=" + ID;
                DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
            }
            //Delete any Wav file created..by....?ccc
            foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(OggPath), "*.wav", System.IO.SearchOption.AllDirectories))
            {
                DeleteFile(wav_name);//, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin); //File.Delete(wav_name);
            }
            cnb.Close();
            e.Result = "done";
        }

        public static string FixOggwDiffName(string OggPreviewPath, string Folder_Name, System.DateTime timestamp, string tsst, string logPath, string tmpPath, string multithreadname)
        {
            var previewN = OggPreviewPath == null ? null : ((File.Exists(OggPreviewPath.ToString())) ? OggPreviewPath.ToString().Replace(".wem", ".ogg") : null);
            if (!File.Exists(previewN))
            {
                foreach (string preview_name in Directory.GetFiles(Folder_Name, "*_preview.wem", System.IO.SearchOption.AllDirectories))
                {
                    foreach (string file_name in Directory.GetFiles(Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories))
                    {
                        //tsst = "Fixx Ogg with diff name after decompression ..." + OggPreviewPath; UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "");
                        if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
                        {
                            var tl = previewN;
                            var hg = preview_name;
                            previewN = preview_name.Replace(".wem", ".ogg");
                            if (!File.Exists(previewN))
                            {
                                try
                                {
                                    tsst = "Fix _preview.OGG having a diff name than _preview.wem after oggged ..." + Path.GetFileName(file_name) + "-" + Path.GetFileName(previewN); UpdateLog(timestamp, tsst, false, logPath, tmpPath, multithreadname, "", null, null);
                                    File.Copy(file_name, previewN, true);
                                    DeleteFile(file_name);
                                    //File.Delete(file_name);
                                }
                                catch (Exception ee)
                                {
                                    timestamp = UpdateLog(timestamp, "FAILED1 FixOggwDiffName" + ee.Message + "----" + file_name + "\n -" + previewN + "\n -" + file_name + ".ogg", true, logPath, "", "", "DLCManager", null, null);
                                    Console.WriteLine(ee.Message);
                                }
                            }
                        }
                    }
                }
            }
            return previewN;
        }
        //public static void FixBitrateST(string cmd, string AudioPreview, float bitrate, float SampleRate, string ID, string audioPreviewPath, string oggPreviewPath, OleDbConnection cnb)
        //{
        //    var d1 = WwiseInstalled("Convert Audio if bitrate > ConfigRepository");
        //    var audio_hash = "";
        //    if (d1.Split(';')[0] == "1")
        //    {
        //        Downstream(AudioPreview);//.Replace(".wem", "_fixed.ogg")
        //        audio_hash = GetHash(AudioPreview);
        //        using (var vorbis = new NVorbis.VorbisReader(AudioPreview.Replace(".wem", "_fixed.ogg")))
        //        {
        //            bitrate = vorbis.NominalBitrate;
        //            SampleRate = vorbis.SampleRate;
        //        }
        //        cmd = "UPDATE Main SET ";
        //        cmd += "Audio_Hash=\"" + audio_hash + "\"" + ", audioBitrate =\"" + bitrate + "\"";
        //        cmd += ", audioSampleRate=\"" + SampleRate + "\"";// previewN + "\"";
        //        cmd += " WHERE ID=" + ID;
        //        DataSet dios = new DataSet(); dios = UpdateDB("Main", cmd + ";", cnb);

        //        //Update Preview
        //        if (audioPreviewPath != null && audioPreviewPath != "")
        //        {
        //            Downstream(audioPreviewPath);//.Replace(".wem", "_fixed.ogg")
        //            audio_hash = GetHash(audioPreviewPath);
        //            using (var vorbis = new NVorbis.VorbisReader(oggPreviewPath))
        //            {
        //                bitrate = vorbis.NominalBitrate;
        //                SampleRate = vorbis.SampleRate;
        //            }
        //            cmd = "UPDATE Main SET ";
        //            cmd += "audioPreview_Hash=\"" + audio_hash;
        //            cmd += "\" WHERE ID=" + ID;
        //            DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
        //        }
        //        //Delete any Wav file created..by....?ccc
        //        foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(AudioPreview), "*.wav", System.IO.SearchOption.AllDirectories))
        //        {
        //            DeleteFile(wav_name);//, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin); //File.Delete(wav_name);
        //        }
        //    }
        //}
        //public static void FixPreviewST(string OggPath, string AppWD, string OggPreviewPath, string cmd, string Folder_Name, string ID, OleDbConnection cnb)
        //{
        //    var startInfo = new ProcessStartInfo();
        //    startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
        //    startInfo.WorkingDirectory = AppWD;
        //    var t = OggPath.Replace(".wem", "_fixed.ogg"); //"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
        //    var tt = t.Replace("_fixed.ogg", "_preview.ogg");
        //    var times = ConfigRepository.Instance()["dlcm_PreviewStart"]; //00:30
        //    string[] timepieces = times.Split(':');
        //    var audioPreview_hash = "";
        //    var PreviewTime = "";
        //    var PreviewLenght = "";
        //    TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
        //    startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
        //                                        t,
        //                                        tt,
        //                                        r.TotalMilliseconds,
        //                                        (r.TotalMilliseconds + (ConfigRepository.Instance()["dlcm_PreviewLenght"].ToInt32() * 1000)));
        //    startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

        //    if (File.Exists(t))
        //        using (var DDC = new Process())
        //        {
        //            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
        //            if (DDC.ExitCode == 0)
        //            {
        //                var wwisePath = "";
        //                if (!String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
        //                    wwisePath = ConfigRepository.Instance()["general_wwisepath"];
        //                else
        //                    wwisePath = Environment.GetEnvironmentVariable("WWISEROOT");
        //                if (wwisePath == "")
        //                {
        //                    ErrorWindow frm1 = new ErrorWindow("(2483)Please Install Wwise v" + ConfigRepository.Instance()["general_wwisepath"] + " with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
        //                    //v2016.2.1.5995 is currently breaking PC cocnversion (not Ps3)
        //                    frm1.ShowDialog();
        //                    if (frm1.StopImport) return;// "0";// break;
        //                                                //if (frm1.StopImport) { j = 10; return "0"; }// break; }
        //                }
        //                if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath);
        //                //pB_ReadDLCs.CreateGraphics().DrawString("Creating a preview", new Font("Arial", (float)7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
        //                GenericFunctions.Converters(tt, GenericFunctions.ConverterTypes.Ogg2Wem, false, false);
        //                if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath.Replace(".wem", ".wav"));
        //                if (File.Exists(OggPreviewPath)) DeleteFile(OggPreviewPath.Replace(".wem", ".ogg"));
        //                OggPreviewPath = tt.Replace(".ogg", ".wem");
        //                //if (!File.Exists(OggPreviewPath))
        //                //    tg = "-";
        //                audioPreview_hash = GetHash(OggPreviewPath);

        //                PreviewTime = ConfigRepository.Instance()["dlcm_PreviewStart"];
        //                PreviewLenght = ConfigRepository.Instance()["dlcm_PreviewLenght"];
        //            }
        //        }

        //    //Fix _preview.OGG having a diff name than _preview.wem after oggged
        //    var previewN = OggPreviewPath == null ? null : ((File.Exists(OggPreviewPath.ToString())) ? OggPreviewPath.ToString().Replace(".wem", ".ogg") : null);
        //    if (!File.Exists(previewN))
        //    {
        //        foreach (string preview_name in Directory.GetFiles(Folder_Name, "*_preview.wem", System.IO.SearchOption.AllDirectories))
        //        {
        //            foreach (string file_name in Directory.GetFiles(Folder_Name, "*.ogg", System.IO.SearchOption.AllDirectories))
        //            {
        //                if (file_name.Replace("_fixed.ogg", ".ogg") != preview_name.Replace("_preview.wem", ".ogg"))
        //                {
        //                    var tl = previewN;
        //                    var hg = preview_name;
        //                    previewN = preview_name.Replace(".wem", ".ogg");
        //                    if (!File.Exists(previewN))
        //                    {
        //                        try
        //                        {
        //                            File.Copy(file_name, previewN, true);
        //                            DeleteFile(file_name);
        //                            //File.Delete(file_name);
        //                        }
        //                        catch (Exception ee)
        //                        {
        //                            //timestamp = UpdateLog(timestamp, "FAILED1 preview fix" + ee.Message + "----" + OggPath + "\n -" + previewN + "\n -" + file_name + ".ogg", true);
        //                            Console.WriteLine(ee.Message);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    PreviewLenght = "";
        //    audioPreview_hash = "";
        //    int SampleRate = 0;
        //    int bitrate = 0;
        //    if (File.Exists(previewN))
        //    {
        //        using (var vorbis = new NVorbis.VorbisReader(previewN))
        //        {
        //            bitrate = vorbis.NominalBitrate;
        //            if ((vorbis.TotalTime.ToString().Split(':'))[0] == "00" && (vorbis.TotalTime.ToString().Split(':'))[1] == "00")
        //                PreviewLenght = (vorbis.TotalTime.ToString().Split(':'))[2];
        //            else PreviewLenght = vorbis.TotalTime.ToString();
        //            audioPreview_hash = GetHash(OggPreviewPath);
        //            SampleRate = vorbis.SampleRate;
        //        }

        //        cmd = "UPDATE Main SET ";
        //        cmd += " audioPreview_Hash=\"" + audioPreview_hash + "\"" + ", OggPreviewPath=\"" + previewN + "\", Has_Preview=\"Yes\"";// previewN + "\"";
        //        cmd += ", PreviewLenght=\"" + PreviewLenght + "\"";
        //        cmd += " WHERE ID=" + ID;
        //        DataSet dis = new DataSet(); dis = UpdateDB("Main", cmd + ";", cnb);
        //    }
        //    //Delete any Wav file created..by....?ccc
        //    foreach (string wav_name in Directory.GetFiles(Path.GetDirectoryName(OggPath), "*.wav", System.IO.SearchOption.AllDirectories))
        //    {
        //        DeleteFile(wav_name);//, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin); //File.Delete(wav_name);
        //    }
        //}
        public static void FixAudioIssues(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel)
        {

            BackgroundWorker bwRFixAudio = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true }; //bcapi
            bwRFixAudio.DoWork += new DoWorkEventHandler(FixBitrate);
            bwRFixAudio.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwRFixAudio.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            if (cancel)
            { if (bwRFixAudio.WorkerSupportsCancellation == true) bwRFixAudio.CancelAsync(); }// Cancel the asynchronous operation.
            else
            {
                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd, "", cnb); var noOfRec = dhs.Tables[0].Rows.Count;
                //if (pB_ReadDLCs != null) {pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; pB_ReadDLCs.Value = 0;}
                for (var i = 0; i <= noOfRec - 1; i++)
                {
                    //var tst = "Starting...  Fixing Audio Issues preview&bitrate&Spotfy Metadata"; var timestamp = UpdateLog(DateTime.Now, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = i; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }
                    var ID = dhs.Tables[0].Rows[i].ItemArray[0].ToString();
                    var AudioPath = dhs.Tables[0].Rows[i].ItemArray[1].ToString();
                    float bitrate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[2].ToString());
                    float SampleRate = float.Parse(dhs.Tables[0].Rows[i].ItemArray[3].ToString());
                    var audioPreviewPath = dhs.Tables[0].Rows[i].ItemArray[4].ToString();
                    var oggPreviewPath = dhs.Tables[0].Rows[i].ItemArray[5].ToString();

                    if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                    var tst = "AudioFixing: " + i + "/" + noOfRec + " " + AudioPath; var timestamp = UpdateLog(DateTime.Now, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var args = cmd.Replace(";", "") + ";" + AudioPath + ";" + bitrate + ";" + SampleRate + ";" + ID + ";" + audioPreviewPath + ";" + oggPreviewPath + ";" + i;
                    //do
                    //    Application.DoEvents();
                    //while (bwRFixAudio.IsBusy);//keep singlethread as toolkit not multithread abled
                    //if (!bwRFixAudio.IsBusy)
                    bwRFixAudio.RunWorkerAsync(args);
                    do
                        Application.DoEvents();
                    while (bwRFixAudio.IsBusy);//keep singlethread as toolkit not multithread abled
                }
            }
        }



        public static void FixMissingPreview(string cmd, OleDbConnection cnb, string AppWD, ProgressBar pB_ReadDLCs, RichTextBox rtxt_StatisticsOnReadDLCs, bool cancel)
        {

            BackgroundWorker bwFixA = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            bwFixA.DoWork += new DoWorkEventHandler(FixPreview);
            bwFixA.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bwFixA.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ProcessCompleted);
            bwFixA.WorkerReportsProgress = true;
            if (cancel)
            { if (bwFixA.WorkerSupportsCancellation == true) bwFixA.CancelAsync(); }// Cancel the asynchronous operation.
            else
            {
                DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb); var noOfRec = dhxs.Tables[0].Rows.Count;
                if (pB_ReadDLCs != null) { pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Maximum = noOfRec; }

                for (var j = 0; j <= noOfRec - 1; j++)
                {
                    //var tst = "Starting...  Fixing Audio Issues preview"; var timestamp = UpdateLog(DateTime.Now, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
                    var ID = dhxs.Tables[0].Rows[j].ItemArray[0].ToString();
                    var OggPath = dhxs.Tables[0].Rows[j].ItemArray[1].ToString();
                    var bitrate = dhxs.Tables[0].Rows[j].ItemArray[2];
                    var SampleRate = dhxs.Tables[0].Rows[j].ItemArray[3];
                    var OggPreviewPath = dhxs.Tables[0].Rows[j].ItemArray[4].ToString();
                    var Folder_Name = dhxs.Tables[0].Rows[j].ItemArray[5].ToString();

                    if (pB_ReadDLCs != null) pB_ReadDLCs.Value += 1;
                    //tst = "AudioFixing: " + j + "/" + noOfRec + " " + OggPath; timestamp = UpdateLog(DateTime.Now, tst, true, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);

                    var args = OggPath + ";" + AppWD + ";" + OggPreviewPath + ";" + cmd + ";" + Folder_Name + ";" + ID + ";" + j;
                    bwFixA.RunWorkerAsync(args);
                    do
                        Application.DoEvents();
                    while (bwFixA.IsBusy);//keep singlethread as toolkit not multithread abled

                }
                dhxs.Dispose();
            }
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


        public static void Translation_And_Correction(string dbp, ProgressBar pB_ReadDLCs, OleDbConnection cnb)
        // Select only Corrected Arstist OR Album OR Cover combination
        // For Each Corrected Record build up an Update sentence
        // Insert any translation if not already existing
        {
            var cmd1 = "";
            var artpath_c = "";
            var artist_c = "";
            var album_c = "";
            var DB_Path = dbp;//+ "\\Files.accdb";
            int aa = 0;
            pB_ReadDLCs.Value = 0;
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE (Artist_Correction <> \"\") or (Album_Correction <> \"\")" +
                " OR (AlbumArt_Correction <> \"\") order by id;", ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb);
            var norec = dus.Tables[0].Rows.Count;
            pB_ReadDLCs.Maximum = norec;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                artist_c = dataRow.ItemArray[2].ToString();
                album_c = dataRow.ItemArray[4].ToString();
                artpath_c = dataRow.ItemArray[5].ToString();

                cmd1 = "UPDATE Main SET " + (artist_c != "" ? "Artist = \"" + artist_c + "\"," : "") + (artist_c != "" ? " Artist_Sort = \"" + artist_c + (album_c != "" ? "\"," : "\"") : "") + (album_c != "" ? " Album = \"" + album_c + (artpath_c != "" ? "\"," : "\"") : "") + (artpath_c != "" ? " AlbumArtPath = \"" + artpath_c + "\"" : "");
                cmd1 += ", Has_Been_Corrected=\"Yes\" WHERE Artist=\"" + dataRow.ItemArray[1].ToString() + "\" AND Album=\"" + dataRow.ItemArray[3].ToString() + "\"";
                dus = UpdateDB("Main", cmd1 + ";", cnb);
                pB_ReadDLCs.Increment(1);
            }
            //insert any translation if not already existing

            var insertcmdd = "Artist, Album";
            var insertvalues = "SELECT DISTINCT(Switch([S].[Artist_Correction] <> \"\", [S].[Artist_Correction], 1=1, [S].[Artist])) AS ArtistN, (Switch(S.Album_Correction <> \"\", [S].[Album_Correction], 1=1, [S].[Album])) AS AlbumN FROM Standardization AS S WHERE (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=-1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=-1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0)) OR (((StrComp([S].[ARTIST],[S].[Artist_Correction],0))=1) AND ((StrComp([S].[ALBUM],[S].[Album_Correction],0))=1) AND (((SELECT COUNT(*) FROM Standardization AS SS WHERE (StrComp([SS].[Artist],[S].[Artist_Correction],0)=0) AND (StrComp([SS].[Album],[S].[Album_Correction],0)=0) ))=0));";
            DataSet dooz = new DataSet(); dooz = SelectFromDB("Main", insertvalues, ConfigRepository.Instance()["dlcm_DBFolder"].ToString(), cnb); aa = dooz.Tables[0].Rows.Count; //Get No Of NEW/existing Standardizatiins ???
            InsertIntoDBwValues("Standardization", insertcmdd, insertvalues, cnb, 0);

            var cmd3 = "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)";
            DeleteFromDB("Groups", cmd3, cnb); //Cleans out duplicates

            //Apply Artist Short Name
            Standardization.ApplyArtistShort(cnb);

            //Apply Album Short Name
            Standardization.ApplyAlbumShort(cnb);

            //Multiply Spotify
            Standardization.ApplySpotify(cnb);

            //Multiply Cover
            Standardization.ApplyDefaultCover(cnb);

            //Apply DefaultCover
            Standardization.MakeCover(cnb);

            //DeleteFromDB("Standardization", "DELETE * FROM Standardization as s WHERE ((SELECT count(*) FROM Standardization as o WHERE STRCOMP(o.Artist&o.Album&o.Artist_correction&o.Album_Correction,S.Artist&s.Album&s.Artist_correction&s.Album_Correction,0)=0 and s.id>o.id)>1)");
            //DeleteFromDB("Standardization", "DELETE* FROM Standardization WHERE id NOT IN (SELECT min(id) FROM Standardization GROUP BY Artist, Artist_Correction, Album, Album_Correction);");
            DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE id NOT IN( SELECT min(ID) FROM(SELECT ID, Artist , IIF(Artist_Correction is NULL or Artist_Correction = '', '0', Artist_Correction) as Artist_Corrections , Album , IIF(Album_Correction is NULL or Album_Correction = '', '0', Album_Correction) as Album_Corrections FROM Standardization) GROUP BY Artist, Artist_Corrections, Album, Album_Corrections); ", cnb);
            MessageBox.Show("Artist/Album Translation_And_Correction Standardization rules applied (correction recs :" + aa + ")");
        }

        public static void CopyFolder(string copy_dir, string destination_dir)
        {
            foreach (string dir in Directory.GetDirectories(copy_dir, "*", System.IO.SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(destination_dir + dir.Substring(copy_dir.Length));
            }

            foreach (string file_name in Directory.GetFiles(copy_dir, "*.*", System.IO.SearchOption.AllDirectories))
            {
                File.Copy(file_name, destination_dir + file_name.Substring(copy_dir.Length), true);
            }
        }
    }
}
