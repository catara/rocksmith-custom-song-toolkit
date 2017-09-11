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
using Image = System.Drawing.Image;
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

namespace RocksmithToolkitGUI.DLCManager
{
    class GenericFunctions
    {

        public const string MESSAGEBOX_CAPTION = "Manage a Library of DLCs";

        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC

        private StringBuilder errorsFound;

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
        }



        public static SpotifyWebAPI _spotify;
        public string _trackno;
        public string _year;
        public static PrivateProfile _profile;
        public List<FullTrack> _savedTracks;
        public List<SimplePlaylist> _playlists;

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
            catch (Exception)
            {
                status = "NOK";
            }

            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "26d287105e31491889f3cd293d85bfea",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);

            if (status == "OK") try
                {
                    _spotify = await webApiFactory.GetWebApi();
                    status = "OK";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    status = "NOK";
                }
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
            string bytesRead = await GetTrackNoFromSpotifyAsync(Artist, Album, Title, Year, Status);
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
            catch (Exception ex) { MessageBox.Show(ex.Message); return "NOK"; }
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
                                    a6 = (ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + Artis.Name.ToString() + " - " + Trac.Album.Name.ToString().Replace(":", "") + ".png").Replace("/", "").Replace("?", "");
                                        if (!File.Exists(a6))
                                            using (WebClient wc = new WebClient())
                                            {
                                                byte[] imageBytes = webClient.DownloadData(new Uri(FAitem.Images[0].Url));
                                                FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                                                using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                                            }
                                        a4 = Trac.Album.Id;
                                        a5 = FAitem.Images[0].Url;
                                    if (Trac.Album.Name.ToLower() == Album.ToLower())
                                        break;
                                    }
                        }
                
                else
                {
                    SearchItem Titem2 = _spotify.SearchItems(Title, SearchType.Track, 50);
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
                                            if (Trac.Album.Name.ToString().ToLower() == Album.ToLower())
                                            {
                                                a6 = (ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + Artis.Name.ToString() + Trac.Album.Name.ToString().Replace(":", "") + ".png").Replace("/", "").Replace("?", "");
                                                if (!File.Exists(a6))
                                                    using (WebClient wc = new WebClient())
                                                    {
                                                        byte[] imageBytes = webClient.DownloadData(new Uri(FAitem.Images[0].Url));
                                                        FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                                                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                                                    }
                                            break; ;
                                            }
                                        }
                            }
                    
                    else
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
                                                a6 = (ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + Artis.Name.ToString() + " - " + Trac.Album.Name.ToString().Replace(":", "") + ".png").Replace("/", "").Replace("?", "");
                                                if (!File.Exists(a6))
                                                    using (WebClient wc = new WebClient())
                                                    {
                                                        byte[] imageBytes = webClient.DownloadData(new Uri(FAitem.Images[0].Url));
                                                        FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                                                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                                                    }
                                                a4 = Trac.Album.Id;
                                                a5 = FAitem.Images[0].Url;
                                            if (Trac.Album.Name.ToLower() == Album.ToLower()) break;
                                        }
                                }
                        
                        else
                        {
                            SearchItem Titem4 = _spotify.SearchItems(Album, SearchType.Album);
                            if (Titem4.Error == null && Titem4.Albums.Total > 0)
                                    foreach (SpotifyAPI.Web.Models.FullTrack Trac in Titem4.Tracks.Items)
                                    {
                                        if (Titem4.Tracks.Total > 0) foreach (SpotifyAPI.Web.Models.SimpleArtist Artis in Trac.Artists)
                                                if (Artis.Name.ToString().ToLower() == Artist.ToLower())
                                                {
                                                    a1 = Trac.TrackNumber.ToString();
                                                    a2 = Trac.Id;
                                                    a3 = Artis.Id;
                                                    FullAlbum FAitem = _spotify.GetAlbum(Trac.Album.Id);
                                                    a6 = (ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + Artis.Name.ToString() + " - " + Trac.Album.Name.ToString().Replace(":", "") + ".png").Replace("/", "").Replace("?", ""); 
                                                    if (!File.Exists(a6))
                                                        using (WebClient wc = new WebClient())
                                                        {
                                                            byte[] imageBytes = webClient.DownloadData(new Uri(FAitem.Images[0].Url));
                                                            FileStream file = new FileStream(a6, FileMode.Create, System.IO.FileAccess.Write);
                                                            using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                                                        }
                                                    a4 = Trac.Album.Id;
                                                    a5 = FAitem.Images[0].Url;
                                                if (Trac.Album.Name.ToLower() == Album.ToLower()) break;
                                            }
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
            catch (Exception ee) { Console.WriteLine(ee.Message); }
            a1 = a1.Trim();
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
            if (a1 != "") return a1 + ";" + a2 + ";" + a3 + ";" + a4 + ";" + a5 + ";" + (File.Exists(a6) ?a6 :"") ;
            else return "0" + ";-;-;-;-;-";
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
            DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "");

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
                query[i].Is_Acoustic = dataRow.ItemArray[96].ToString();
                query[i].Top10 = dataRow.ItemArray[97].ToString();
                query[i].Has_Other_Officials = dataRow.ItemArray[98].ToString();
                query[i].Spotify_Song_ID = dataRow.ItemArray[98].ToString();
                query[i].Spotify_Artist_ID = dataRow.ItemArray[98].ToString();
                query[i].Spotify_Album_ID = dataRow.ItemArray[98].ToString();
                query[i].Spotify_Album_URL = dataRow.ItemArray[98].ToString();
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
                    using (FileStream fs = File.OpenRead(filename))
                    {
                        SHA1 sha = new SHA1Managed();
                        FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                        fs.Close();
                    }
            }
            catch (Exception ee) { Console.WriteLine(ee.Message); }
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
                    ErrorWindow frm1 = new ErrorWindow(DB + "--------" + slct + "--------------" + ee+"DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                }
        }

        static public void InsertIntoDBwValues(string ftable, string ffields, string fvalues)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb";
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
                    ErrorWindow frm1 = new ErrorWindow(ftable+"--------"+ fvalues +"--------------"+ ee + "DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
                    frm1.ShowDialog();
                    return;
                }
            }

        }

        static public DataSet SelectFromDB(string ftable, string fcmds, string currentDB)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb";
            if (!File.Exists(DB_Path.Replace(";", ""))) DB_Path = currentDB + "\\Files.accdb";
            DataSet dsm = new DataSet();
            if (File.Exists(DB_Path))
            {
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
                        ErrorWindow frm1 = new ErrorWindow(ftable + "--------" + fcmds + "--------------"  + "DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import" + ee, false, false);
                        frm1.ShowDialog();
                        return dsm;
                    }
                }
            }
            else return dsm;
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

        static public void CleanFolder(string pathfld, string exttoigno)
        {
            pathfld = pathfld + "\\";
            try
            {
                System.IO.DirectoryInfo downloadedMessageInfo2 = new DirectoryInfo(pathfld);
                foreach (FileInfo file in downloadedMessageInfo2.GetFiles())
                {
                    if (file.FullName.IndexOf(exttoigno) == 0 || exttoigno == "") DeleteFile(file.FullName);
                }
            }
            catch (Exception ex) { Console.Write(ex); }
        }


        static public DataSet UpdateDB(string ftable, string fcmds)
        {
            var DB_Path = ConfigRepository.Instance()["dlcm_DBFolder"].ToString() + "\\Files.accdb";
            DataSet dsm = new DataSet();
            if (File.Exists(DB_Path))
            {
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
                    ErrorWindow frm1 = new ErrorWindow(ftable + "--------" + fcmds + "--------------"+ee + "DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734 ", "Error @Import", false, false);
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
                FileSystem.DeleteFile(file, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
            catch (Exception ex) { Console.Write(ex); }
        }
        static public void DeleteDirectory(string dir)
        {
            try
            {
                FileSystem.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
            catch (Exception ex) { Console.Write(ex); }
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
                ErrorWindow frm1 = new ErrorWindow("Cause " + Mss + ".\nPlease Install Wwise v2016.2.1.5995 with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
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

        public static void DeleteRecords(string ID, string cmd, string DBPath, string TempPath, string norows, string hash)
        {
            //Delete records
            DialogResult result1 = MessageBox.Show(norows + " of the Following record(s) will be deleted: " + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result1 == DialogResult.Yes)
            {

                DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", cmd.Replace("DELETE FROM Main WHERE ID IN (", "SELECT * FROM Main WHERE ID IN ("), "");
                var rcount = dhs.Tables[0].Rows.Count;
                for (var i = 0; i < rcount; i++)
                {
                    string filePath = dhs.Tables[0].Rows[i].ItemArray[22].ToString();
                    try
                    {
                        DeleteDirectory(filePath);
                    }
                    catch (Exception ex) { Console.Write(ex); }

                    //Move psarc file to Duplicates                        
                    string psarcPath = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19].ToString();
                    try
                    {
                        if (!File.Exists(psarcPath.Replace("0_old", "0_duplicate\\")))
                            File.Move(psarcPath, psarcPath.Replace("0_old", "0_duplicate\\"));
                        else DeleteFile(psarcPath);
                    }
                    catch (Exception ex) { Console.Write(ex); }
                }

                DataSet dus = new DataSet(); dus = SelectFromDB("Main", cmd, "");

                //Delete Arangements
                DeleteFromDB("Arrangements", "DELETE FROM Arrangements WHERE CDLC_ID IN (" + ID + ")");

                // //Delete Tones
                DeleteFromDB("Tones", "DELETE FROM Tones WHERE CDLC_ID IN (" + ID + ")");

                // //Delete Audit trail of import
                DeleteFromDB("Import_AuditTrail", "DELETE * FROM Import_AuditTrail WHERE FileHash IN (\"" + hash.Replace(", ", "\", \"") + "\")");

                //Delete Audit trail of pack
                DeleteFromDB("Pack_AuditTrail", "DELETE FROM Pack_AuditTrail WHERE DLC_ID IN (" + ID + ")");

                MessageBox.Show(rcount + "  Song(s)/Record(s) has(ve) been deleted", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void CreateTempFolderStructure(string Temp_Path_Import, string old_Path_Import, string broken_Path_Import, string dupli_Path_Import, string dlcpacks, string pathDLC, string repacked_path, string repacked_XBOXPath, string repacked_PCPath, string repacked_MACPath, string repacked_PSPath, string log_Path, string albumCovers_PSPath, string Log_PSPath)
        {
            if (!Directory.Exists(Temp_Path_Import) || !Directory.Exists(pathDLC) || !Directory.Exists(old_Path_Import) || !Directory.Exists(broken_Path_Import) || !Directory.Exists(dupli_Path_Import) || !Directory.Exists(dlcpacks + "\\temp") || !Directory.Exists(dlcpacks + "\\manipulated") || !Directory.Exists(dlcpacks + "\\manifests") || !Directory.Exists(dlcpacks + "\\manipulated\\temp") || !Directory.Exists(repacked_path) || !Directory.Exists(repacked_XBOXPath) || !Directory.Exists(repacked_PCPath) || !Directory.Exists(repacked_MACPath) || !Directory.Exists(repacked_PSPath) || (!Directory.Exists(log_Path)&& log_Path!="") || !Directory.Exists(Log_PSPath) || !Directory.Exists(albumCovers_PSPath))
            {
                MessageBox.Show(String.Format("One of the mandatory backend, folder is missing " + ", " + Temp_Path_Import + ", " + pathDLC + ", " + old_Path_Import + ", " + broken_Path_Import + ", " + dupli_Path_Import + ", " + dlcpacks + "(manipulated or manipulated-temp or manifests or temp), " + repacked_path + "(split by platform), " + log_Path, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error));
                try
                {
                    DirectoryInfo di;
                    DialogResult result1 = MessageBox.Show("Some folder is missing please" + "\n\nChose:\n\n1. Create Folders\n2. Cancel Import command\n3. Ignore", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                    }
                    else if (result1 == DialogResult.No) return;
                    else Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open create folders " + Temp_Path_Import + "-" + pathDLC + "-" + old_Path_Import + "-" + broken_Path_Import + "-" + dupli_Path_Import + "-" + dlcpacks + "-" + repacked_path + "-" + albumCovers_PSPath + "-" + Log_PSPath);
                }
            }
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



        public static void UpdatePackingLog(string DB, string DBc_Path, int packid, string dlcID, string ex)
        {
            var insertcmdd = "Pack, CDLC_ID, Dates, Comments";
            var insertvalues = "\"" + packid + "\"," + dlcID + ",\"" + System.DateTime.Now + "\",\"" + ex.Replace("'", "") + "\"";
            InsertIntoDBwValues(DB, insertcmdd, insertvalues);
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
                                catch
                                {
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
                                catch
                                {
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


        public static void Downstream(string fn)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = fn.IndexOf("preview.wem") > 0 ? fn.Replace(".wem", ".ogg") : fn.Replace(".wem", "_fixed.ogg");
            var tt = t + "l";
            startInfo.Arguments = String.Format(" \"{0}\" -o \"{1}\" -Q",
                                                t,
                                                tt);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        startInfo = new ProcessStartInfo();
                        startInfo.FileName = Path.Combine(AppWD, "oggenc.exe");
                        startInfo.WorkingDirectory = AppWD;
                        startInfo.Arguments = String.Format(" \"{0}\" -o \"{1}\" -b \"{2}\"  --resample \"{3}\" -Q",
                                                            tt,
                                                            t,
                                                            "128",
                                                            "44100");
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true;

                        if (File.Exists(t))
                            using (var DDgC = new Process())
                            {
                                DDgC.StartInfo = startInfo; DDgC.Start(); DDgC.WaitForExit(1000 * 190 * 1); //wait 1min
                                if (DDC.ExitCode == 0)
                                {
                                    DeleteFile(tt);
                                    //MainDB.Converters(t, MainDB.ConverterTypes.Ogg2Wem, false);
                                    DeleteFile(fn);
                                    OggFile.Convert2Wem(t, 4, 30000, 34000);
                                    DeleteFile(t.Replace(".ogg", ".wav"));
                                    DeleteFile(t.Replace(".ogg", "_preview.wav"));
                                    DeleteFile(t.Replace(".ogg", "_preview.wem"));
                                    DeleteFile(t.Replace(".ogg", "_preview.ogg"));
                                    if (!(fn.IndexOf("preview.wem") > 0)) File.Move(t.Replace(".ogg", ".wem"), fn);//.Replace("_fixed.ogg", ".wem")
                                }
                            }
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
            catch (Exception ex)
            { Console.Write(ex); return null; }
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
                Console.Write(ex);
            }

            return "nok";
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
            catch (Exception ex) { Console.Write(ex); return "nok"; }
        }
        public static string FTPFile(string filel, string filen, string TempPat, string SearchCm)
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
                    dxr = UpdateDB("Main", "Update Main Set Remote_path = \"" + fn + "\";");

                    return "Truly ";
                }
                catch (Exception ee) { Console.Write(ee); return "Not "; }
            }
            catch (Exception ee) { Console.Write(ee); return "Not "; }
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
                ExternalApps.Ogg2Wav(audioPath, wavPath); //detect quality here
                if (!File.Exists(oggPreviewPath))
                {
                    ExternalApps.Ogg2Preview(audioPath, oggPreviewPath, previewLength, chorusTime);
                    ExternalApps.Ogg2Wav(oggPreviewPath, wavPreviewPath);
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
                        ExternalApps.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    else
                    {
                        DeleteFile(oggPath);
                        ExternalApps.Wav2Ogg(audioPath, oggPath, audioQuality); // 4
                    }
                    ExternalApps.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                    ExternalApps.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                }
                Wwise.Convert2Wem(audioPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            if (audioPath.Substring(audioPath.Length - 4).ToLower() == ".wem" && !File.Exists(wemPreviewPath))
            {
                OggFile.Revorb(audioPath, oggPath, Path.GetDirectoryName(Application.ExecutablePath), OggFile.WwiseVersion.Wwise2013);
                ExternalApps.Ogg2Wav(oggPath, wavPath);
                ExternalApps.Ogg2Preview(oggPath, oggPreviewPath, previewLength, chorusTime);
                ExternalApps.Ogg2Wav(oggPreviewPath, wavPreviewPath);
                Wwise.Convert2Wem(wavPath, wemPath, audioQuality);
                audioPath = wemPath;
            }

            return audioPath;
        }
        public static void Converters(string file, ConverterTypes converterType, bool mssON)
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

    }
}
