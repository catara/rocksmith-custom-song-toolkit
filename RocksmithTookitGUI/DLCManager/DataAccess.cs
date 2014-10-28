using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//bcapi
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DataAccess
    {
        public DataAccess() { }

        //bcapi
        public string DB_Path = "";
        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            DataSet ds = new DataSet();

            DB_Path = "../../../../tmp\\Files.accdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Main ORDER BY Artist, Album_Year, Album, Song_Title;", cn);
                da.Fill(ds, "Main");
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
            }

            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID" };
            DataGridViewTextBoxColumn Song_Title = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title", HeaderText = "Song_Title" };
            DataGridViewTextBoxColumn Song_Title_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title_Sort", HeaderText = "Song_Title_Sort" };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album" };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist" };
            DataGridViewTextBoxColumn Artist_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Sort", HeaderText = "Artist_Sort" };
            DataGridViewTextBoxColumn Album_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Year", HeaderText = "Album_Year" };
            DataGridViewTextBoxColumn AverageTempo = new DataGridViewTextBoxColumn { DataPropertyName = "AverageTempo", HeaderText = "AverageTempo" };
            DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume" };
            DataGridViewTextBoxColumn Preview_Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Preview_Volume", HeaderText = "Preview_Volume" };
            DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath" };
            DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath" };
            DataGridViewTextBoxColumn audioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreviewPath", HeaderText = "audioPreviewPath" };
            DataGridViewTextBoxColumn Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Track_No", HeaderText = "Track_No" };
            DataGridViewTextBoxColumn Author = new DataGridViewTextBoxColumn { DataPropertyName = "Author", HeaderText = "Author" };
            DataGridViewTextBoxColumn Version = new DataGridViewTextBoxColumn { DataPropertyName = "Version", HeaderText = "Version" };
            DataGridViewTextBoxColumn DLC_Name = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_Name", HeaderText = "DLC_Name" };
            DataGridViewTextBoxColumn DLC_AppID = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_AppID", HeaderText = "DLC_AppID" };
            DataGridViewTextBoxColumn Current_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Current_FileName", HeaderText = "Current_FileName" };
            DataGridViewTextBoxColumn Original_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Original_FileName", HeaderText = "Original_FileName" };
            DataGridViewTextBoxColumn Import_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Path", HeaderText = "Import_Path" };
            DataGridViewTextBoxColumn Import_Date = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Date", HeaderText = "Import_Date" };
            DataGridViewTextBoxColumn Folder_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Folder_Name", HeaderText = "Folder_Name" };
            DataGridViewTextBoxColumn File_Size = new DataGridViewTextBoxColumn { DataPropertyName = "File_Size", HeaderText = "File_Size" };
            DataGridViewTextBoxColumn File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "File_Hash", HeaderText = "File_Hash" };
            DataGridViewTextBoxColumn Original_File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Original_File_Hash", HeaderText = "Original_File_Hash" };
            DataGridViewTextBoxColumn Is_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Original", HeaderText = "Is_Original" };
            DataGridViewTextBoxColumn Is_OLD = new DataGridViewTextBoxColumn { DataPropertyName = "Is_OLD", HeaderText = "Is_OLD" };
            DataGridViewTextBoxColumn Is_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Beta", HeaderText = "Is_Beta" };
            DataGridViewTextBoxColumn Is_Alternate = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Alternate", HeaderText = "Is_Alternate" };
            DataGridViewTextBoxColumn Is_Multitrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Multitrack", HeaderText = "Is_Multitrack" };
            DataGridViewTextBoxColumn Is_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Broken", HeaderText = "Is_Broken" };
            DataGridViewTextBoxColumn MultiTrack_Version = new DataGridViewTextBoxColumn { DataPropertyName = "MultiTrack_Version", HeaderText = "MultiTrack_Version" };
            DataGridViewTextBoxColumn Alternate_Version_No = new DataGridViewTextBoxColumn { DataPropertyName = "Alternate_Version_No", HeaderText = "Alternate_Version_No" };
            DataGridViewTextBoxColumn DLC = new DataGridViewTextBoxColumn { DataPropertyName = "DLC", HeaderText = "DLC" };
            DataGridViewTextBoxColumn Has_Bass = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bass", HeaderText = "Has_Bass" };
            DataGridViewTextBoxColumn Has_Guitar = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Guitar", HeaderText = "Has_Guitar" };
            DataGridViewTextBoxColumn Has_Lead = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Lead", HeaderText = "Has_Lead" };
            DataGridViewTextBoxColumn Has_Rhythm = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Rhythm", HeaderText = "Has_Rhythm" };
            DataGridViewTextBoxColumn Has_Combo = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Combo", HeaderText = "Has_Combo" };
            DataGridViewTextBoxColumn Has_Vocals = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Vocals", HeaderText = "Has_Vocals" };
            DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections" };
            DataGridViewTextBoxColumn Has_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Cover", HeaderText = "Has_Cover" };
            DataGridViewTextBoxColumn Has_Preview = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Preview", HeaderText = "Has_Preview" };
            DataGridViewTextBoxColumn Has_Custom_Tone = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Custom_Tone", HeaderText = "Has_Custom_Tone" };
            DataGridViewTextBoxColumn Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Has_DD", HeaderText = "Has_DD" };
            DataGridViewTextBoxColumn Has_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Version", HeaderText = "Has_Version" };
            DataGridViewTextBoxColumn Tunning = new DataGridViewTextBoxColumn { DataPropertyName = "Tunning", HeaderText = "Tunning" };
            DataGridViewTextBoxColumn Bass_Picking = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Picking", HeaderText = "Bass_Picking" };
            DataGridViewTextBoxColumn Tones = new DataGridViewTextBoxColumn { DataPropertyName = "Tones", HeaderText = "Tones" };
            DataGridViewTextBoxColumn Group = new DataGridViewTextBoxColumn { DataPropertyName = "Group", HeaderText = "Group" };
            DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating" };
            DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description" };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments" };
            DataGridViewTextBoxColumn Show_Album = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Album", HeaderText = "Show_Album" };
            DataGridViewTextBoxColumn Show_Track = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Track", HeaderText = "Show_Track" };
            DataGridViewTextBoxColumn Show_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Year", HeaderText = "Show_Year" };
            DataGridViewTextBoxColumn Show_CDLC = new DataGridViewTextBoxColumn { DataPropertyName = "Show_CDLC", HeaderText = "Show_CDLC" };
            DataGridViewTextBoxColumn Show_Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Rating", HeaderText = "Show_Rating" };
            DataGridViewTextBoxColumn Show_Description = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Description", HeaderText = "Show_Description" };
            DataGridViewTextBoxColumn Show_Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Comments", HeaderText = "Show_Comments" };
            DataGridViewTextBoxColumn Show_Available_Instruments = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Available_Instruments", HeaderText = "Show_Available_Instruments" };
            DataGridViewTextBoxColumn Show_Alternate_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Alternate_Version", HeaderText = "Show_Alternate_Version" };
            DataGridViewTextBoxColumn Show_MultiTrack_Details = new DataGridViewTextBoxColumn { DataPropertyName = "Show_MultiTrack_Details", HeaderText = "Show_MultiTrack_Details" };
            DataGridViewTextBoxColumn Show_Group = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Group", HeaderText = "Show_Group" };
            DataGridViewTextBoxColumn Show_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Beta", HeaderText = "Show_Beta" };
            DataGridViewTextBoxColumn Show_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Broken", HeaderText = "Show_Broken" };
            DataGridViewTextBoxColumn Show_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Show_DD", HeaderText = "Show_DD" };
            DataGridViewTextBoxColumn Original = new DataGridViewTextBoxColumn { DataPropertyName = "Original", HeaderText = "Original" };
            DataGridViewTextBoxColumn Selected = new DataGridViewTextBoxColumn { DataPropertyName = "Selected", HeaderText = "Selected" };
            DataGridViewTextBoxColumn YouTube_Link = new DataGridViewTextBoxColumn { DataPropertyName = "YouTube_Link", HeaderText = "YouTube_Link" };
            DataGridViewTextBoxColumn CustomsForge_Link = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Link", HeaderText = "CustomsForge_Link" };
            DataGridViewTextBoxColumn CustomsForge_Like = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Like", HeaderText = "CustomsForge_Like" };
            DataGridViewTextBoxColumn CustomsForge_ReleaseNotes = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_ReleaseNotes", HeaderText = "CustomsForge_ReleaseNotes" };
            DataGridViewTextBoxColumn SignatureType = new DataGridViewTextBoxColumn { DataPropertyName = "SignatureType", HeaderText = "SignatureType" };
            DataGridViewTextBoxColumn ToolkitVersion = new DataGridViewTextBoxColumn { DataPropertyName = "ToolkitVersion", HeaderText = "ToolkitVersion" };
            DataGridViewTextBoxColumn Has_Author = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Author", HeaderText = "Has_Author" };
            DataGridViewTextBoxColumn OggPath = new DataGridViewTextBoxColumn { DataPropertyName = "OggPath", HeaderText = "OggPath" };
            DataGridViewTextBoxColumn oggPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "oggPreviewPath", HeaderText = "oggPreviewPath" };



            //bsPositions.DataSource = ds.Tables["Main"];
            //bsBadges.DataSource = ds.Tables["Badge"];

            //DataGridViewComboBoxColumn ContactPositionColumn = new DataGridViewComboBoxColumn 
            //    { 
            //        DataPropertyName = "ContactPosition", 
            //        DataSource = bsPositions, 
            //        DisplayMember = "ContactPosition", 
            //        DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing, 
            //        Name = "ContactsColumn", 
            //        HeaderText = "Position", 
            //        SortMode = DataGridViewColumnSortMode.Automatic, 
            //        ValueMember = "ContactPosition" 
            //    };

            //DataGridViewComboBoxColumn BadgeColumn = new DataGridViewComboBoxColumn 
            //    { 
            //        DataPropertyName = "Badge", 
            //        DataSource = bsBadges, 
            //        DisplayMember = "Badge", 
            //        DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing, 
            //        Name = "BadgeColumn", 
            //        HeaderText = "Badge", 
            //        SortMode = DataGridViewColumnSortMode.Automatic, 
            //        ValueMember = "Badge" 
            //    };

            DataGridView.AutoGenerateColumns = false;

            DataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                ID,
                Song_Title,
                Song_Title_Sort,
                Album,
                Artist,
                Artist_Sort,
                Album_Year,
                AverageTempo,
                Volume,
                Preview_Volume,
                AlbumArtPath,
                AudioPath,
                audioPreviewPath,
                Track_No,
                Author,
                Version,
                DLC_Name,
                DLC_AppID,
                Current_FileName,
                Original_FileName,
                Import_Path,
                Import_Date,
                Folder_Name,
                File_Size,
                File_Hash,
                Original_File_Hash,
                Is_Original,
                Is_OLD,
                Is_Beta,
                Is_Alternate,
                Is_Multitrack,
                Is_Broken,
                MultiTrack_Version,
                Alternate_Version_No,
                DLC,
                Has_Bass,
                Has_Guitar,
                Has_Lead,
                Has_Rhythm,
                Has_Combo,
                Has_Vocals,
                Has_Sections,
                Has_Cover,
                Has_Preview,
                Has_Custom_Tone,
                Has_DD,
                Has_Version,
                Tunning,
                Bass_Picking,
                Tones,
                Group,
                Rating,
                Description,
                Comments,
                Show_Album,
                Show_Track,
                Show_Year,
                Show_CDLC,
                Show_Rating,
                Show_Description,
                Show_Comments,
                Show_Available_Instruments,
                Show_Alternate_Version,
                Show_MultiTrack_Details,
                Show_Group,
                Show_Beta,
                Show_Broken,
                Show_DD,
                Original,
                Selected,
                YouTube_Link,
                CustomsForge_Link,
                CustomsForge_Like,
                CustomsForge_ReleaseNotes,
                SignatureType,
                ToolkitVersion,
                Has_Author,
                OggPath,
                oggPreviewPath
            }
            );

            ds.Tables["Main"].AcceptChanges();

            bs.DataSource = ds.Tables["Main"];
            DataGridView.DataSource = bs;
            //DataGridView.ExpandColumns();
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
            //public string Selected { get; set; }
            public string SignatureType { get; set; }
            public string ToolkitVersion { get; set; }
            public string Has_Author { get; set; }
            public string OggPath { get; set; }
            public string oggPreviewPath { get; set; }
        }
        public Files[] files = new Files[10000];
        //Generic procedure to read and parse Main.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            try
            {
                MessageBox.Show(DB_Path);
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                    dax.Fill(dus, "Main");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                        files[i].ID = dataRow.ItemArray[0].ToString();
                        //rtxt_StatisticsOnReadDLCs.Text += "\n  artist= " + i + files[i].ID;
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
                        //files.Selected = dus.Tables[0].Rows[0].ItemArray[74].ToString();
                        files[i].SignatureType = dataRow.ItemArray[74].ToString();
                        files[i].ToolkitVersion = dataRow.ItemArray[75].ToString();
                        files[i].Has_Author = dataRow.ItemArray[75].ToString();
                        files[i].OggPath = dataRow.ItemArray[76].ToString();
                        files[i].oggPreviewPath = dataRow.ItemArray[77].ToString();
                        i++;
                    }
                    //Closing Connection
                    dax.Dispose();
                    cnn.Close();
                    //rtxt_StatisticsOnReadDLCs.Text += i;
                    //var ex = 0;
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can not open Main DB connection ! ");
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }
    }
}

