using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using RocksmithToolkitGUI;
using RocksmithToolkitGUI.DLCManager;
using RocksmithToolkitLib.Extensions; //dds
using RocksmithToolkitLib; //config
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.XmlRepository;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Standardization : Form
    {
        public Standardization(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes") btn_DeleteAll.Visible = true;
            cnb = cnnb;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        public string netstatus = ConfigRepository.Instance()["dlcm_netstatus"];

        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION = "StandardizationDB";
        public bool SaveOK = true;
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        public OleDbConnection cnb;

        private void Standardization_Load(object sender, EventArgs e)
        {
            Populate(ref databox, ref Main);
            databox.EditingControlShowing += DataGridView1_EditingControlShowing;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void _SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MAYBE HERE CAN ACTIVATE THE INDIV CELLS
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(@DB_Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Standardization DB connection in StandardizationDB ! " + DB_Path);
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //var line = -1;
            //line = databox.SelectedCells[0].RowIndex;
            //if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {
            int i;
            if (databox.SelectedCells.Count > 0 && databox.SelectedCells[0].ToString() != "")
            {
                i = databox.SelectedCells[0].RowIndex;
                txt_ID.Text = databox.Rows[i].Cells[0].Value.ToString();
                txt_Artist.Text = databox.Rows[i].Cells[2].Value.ToString();
                txt_Artist_Correction.Text = databox.Rows[i].Cells[3].Value.ToString();
                txt_Album.Text = databox.Rows[i].Cells[4].Value.ToString();
                txt_Album_Correction.Text = databox.Rows[i].Cells[5].Value.ToString();
                txt_AlbumArt_Correction.Text = databox.Rows[i].Cells[6].Value.ToString();
                txt_Comments.Text = databox.Rows[i].Cells[7].Value.ToString();
                txt_Artist_Short.Text = databox.Rows[i].Cells[8].Value.ToString();
                txt_Album_Short.Text = databox.Rows[i].Cells[9].Value.ToString();
                txt_Year_Correction.Text = databox.Rows[i].Cells[10].Value.ToString();
                if( databox.Rows[i].Cells[15].Value.ToString()=="Yes") chbx_Default_Cover.Checked= true;
                else chbx_Default_Cover.Checked= false;
                if (txt_AlbumArt_Correction.Text != "") picbx_AlbumArtPath.ImageLocation = txt_AlbumArt_Correction.Text.Replace(".dds", ".png");
                if (databox.Rows[i].Cells[14].Value != null) if (databox.Rows[i].Cells[14].Value.ToString() != "") pxbx_SavedSpotify.ImageLocation = databox.Rows[i].Cells[14].Value.ToString();
                    else pxbx_SavedSpotify.ImageLocation = null;
                //if (DataGridView1.Rows[i].Cells["Default_Cover"].Value.ToString() == "Yes") chbx_Default_Cover.Checked = true;
                //    else chbx_Default_Cover.Checked = false;
                //if (chbx_AutoSave.Checked) SaveOK = true;
                //else SaveOK = false;
                
                //else SaveOK = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            bs.DataSource = null;
            dssx.Dispose();
            //lbl_NoRec.Text = " songs.";
            //bs.DataSource = null;
            //try
            //{
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            dssx.Clear();
            //DataSet ds = new DataSet();
            dssx = SelectFromDB("Standardization", "SELECT ID, (SELECT IIF(count(*)>1,\"Yes\",\"\") as Suspect from Standardization AS O WHERE LCASE(S.Artist)=LCASE(O.Artist) and LCASE(S.Artist_Correction)=LCASE(O.Artist_Correction) and LCASE(S.Album_Correction)=LCASE(O.Album_Correction) and LCASE(S.Album)=LCASE(O.Album)) as Suspect, Artist, Artist_Correction, Album, Album_Correction, AlbumArt_Correction, Comments, Artist_Short, Album_Short, Year_Correction, SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Default_Cover FROM Standardization as S ORDER BY Artist, Album, Artist_Correction, Album_Correction;", "", cnb);
            //OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
            //da.Fill(dssx, "Standardization");
            if (dssx.Tables.Count > 0)
            {
                var noOfRec = dssx.Tables[0].Rows.Count;
                lbl_NoRec.Text = noOfRec.ToString() + " records.";
                //}
                DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 35 };
                DataGridViewTextBoxColumn Suspect = new DataGridViewTextBoxColumn { DataPropertyName = "Suspect", HeaderText = "Suspect ", Width = 35 };
                DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist ", Width = 135 };
                DataGridViewTextBoxColumn Artist_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Correction", HeaderText = "Artist_Correction ", Width = 135 };
                DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album ", Width = 165 };
                DataGridViewTextBoxColumn Album_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Correction", HeaderText = "Album_Correction ", Width = 165 };
                DataGridViewTextBoxColumn AlbumArt_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Correction", HeaderText = "AlbumArt_Correction ", Width = 55 };
                DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments ", Width = 45 };
                DataGridViewTextBoxColumn Artist_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Short", HeaderText = "Artist_Short ", Width = 40 };
                DataGridViewTextBoxColumn Album_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Short", HeaderText = "Album_Short ", Width = 40 };
                DataGridViewTextBoxColumn Year_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Year_Correction", HeaderText = "Year_Correction ", Width = 30 };
                DataGridViewTextBoxColumn SpotifyArtistID = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyArtistID", HeaderText = "SpotifyArtistID ", Width = 55 };
                DataGridViewTextBoxColumn SpotifyAlbumID = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumID", HeaderText = "SpotifyAlbumID ", Width = 55 };
                DataGridViewTextBoxColumn SpotifyAlbumURL = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumURL", HeaderText = "SpotifyAlbumURL ", Width = 55 };
                DataGridViewTextBoxColumn SpotifyAlbumPath = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumPath", HeaderText = "SpotifyAlbumPath ", Width = 55 };
                DataGridViewTextBoxColumn Default_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Default_Cover", HeaderText = "Default_Cover ", Width = 30 };
                DataGridView.AutoGenerateColumns = false;

                DataGridView.Columns.AddRange(new DataGridViewColumn[]
                    {
                        ID,
                        Suspect,
                        Artist,
                        Artist_Correction,
                        Album,
                        Album_Correction,
                        AlbumArt_Correction,
                        Comments,
                        Artist_Short,
                        Album_Short,
                        Year_Correction,
                        SpotifyArtistID,
                        SpotifyAlbumID,
                        SpotifyAlbumURL,
                        SpotifyAlbumPath,
                        Default_Cover
                    }
                );

                //bs.ResetBindings(false);
                //dssx.Tables["Standardization"].AcceptChanges();
                //bs.DataSource = dssx.Tables["Standardization"];
                //DataGridView.DataSource = null;
                //databox.DataSource = bs;
                //databox.Refresh();
                //dssx.Dispose();

                ////}
                ////catch (Exception ex)
                ////{
                ////    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the Standardization DB", false, false);
                ////    frm1.ShowDialog();
                ////    return;
                ////}
                ////advance or step back in the song list
                //int i = 0;
                //if (DataGridView.Rows.Count > 1)
                //{
                //    var prev = DataGridView.SelectedCells[0].RowIndex;
                //    if (DataGridView.Rows.Count == prev + 2)
                //        if (prev == 0) return;
                //        else
                //        {
                //            int rowindex;
                //            DataGridViewRow row;
                //            i = DataGridView.SelectedCells[0].RowIndex;
                //            rowindex = i;
                //            DataGridView.Rows[rowindex - 1].Selected = true;
                //            DataGridView.Rows[rowindex].Selected = false;
                //            row = DataGridView.Rows[rowindex - 1];
                //        }
                //    else
                //    {
                //        int rowindex;
                //        DataGridViewRow row;
                //        i = DataGridView.SelectedCells[0].RowIndex;
                //        rowindex = i;
                //        DataGridView.Rows[rowindex + 1].Selected = true;
                //        DataGridView.Rows[rowindex].Selected = false;
                //        row = DataGridView.Rows[rowindex + 1];
                //    }
                //}
                //DataGridView.AutoResizeColumns();
                bs.ResetBindings(false);
                dssx.Tables["Standardization"].AcceptChanges();
                bs.DataSource = dssx.Tables["Standardization"];
                DataGridView.DataSource = null;
                DataGridView.DataSource = bs;
                DataGridView.Refresh();
                dssx.Dispose();
                ChangeRow();
            }
        }

        private class Files
        {
            public string ID { get; set; }
            public string Suspect { get; set; }
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
        }

        private Files[] files = new Files[10000];

        //Generic procedure to read and parse Standardization.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            //try
            //{
            //MessageBox.Show(DB_Path);
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    DataSet dus = new DataSet();
            //    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //    dax.Fill(dus, "Standardization");
            DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd, "", cnb);
            var i = 0;
            //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
            MaximumSize = dus.Tables[0].Rows.Count;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                files[i] = new Files();

                //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                files[i].ID = dataRow.ItemArray[0].ToString();
                files[i].Suspect = dataRow.ItemArray[1].ToString();
                files[i].Artist = dataRow.ItemArray[2].ToString();
                files[i].Artist_Correction = dataRow.ItemArray[3].ToString();
                files[i].Album = dataRow.ItemArray[4].ToString();
                files[i].Album_Correction = dataRow.ItemArray[5].ToString();
                files[i].AlbumArt_Correction = dataRow.ItemArray[6].ToString();
                files[i].Comments = dataRow.ItemArray[6].ToString();
                files[i].Artist_Short = dataRow.ItemArray[8].ToString();
                files[i].Album_Short = dataRow.ItemArray[9].ToString();
                files[i].Year_Correction = dataRow.ItemArray[10].ToString();
                files[i].SpotifyArtistID = dataRow.ItemArray[11].ToString();
                files[i].SpotifyAlbumID = dataRow.ItemArray[12].ToString();
                files[i].SpotifyAlbumURL = dataRow.ItemArray[13].ToString();
                files[i].SpotifyAlbumPath = dataRow.ItemArray[14].ToString();
                files[i].Default_Cover = dataRow.ItemArray[15].ToString();
                i++;
            }
            //Closing Connection
            //        dax.Dispose();
            //        cnn.Close();
            //        //rtxt_StatisticsOnReadDLCs.Text += i;
            //        //var ex = 0;
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Standardization DB connection ! ");
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(DB_Path, TempPath, false, "", AllowEncriptb, AllowORIGDeleteb, cnb);//.Replace("\\AccessDB.accdb", "")
            frm.Show();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            //DLCManager v1 = new DLCManager();
            string txt = DB_Path;//.Replace("\\AccessDB.accdb", "");
            //MessageBox.Show(txt);
            GenericFunctions.Translation_And_Correction(txt, pB_ReadDLCs, cnb);
            //advance or step back in the song list
            int i = 0;
            if (databox.Rows.Count > 1 && databox.SelectedCells.Count>0)
            {
                var prev = databox.SelectedCells[0].RowIndex;
                if (databox.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        i = databox.SelectedCells[0].RowIndex;
                        rowindex = i;
                        databox.Rows[rowindex - 1].Selected = true;
                        databox.Rows[rowindex].Selected = false;
                        row = databox.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    i = databox.SelectedCells[0].RowIndex;
                    rowindex = i;
                    databox.Rows[rowindex + 1].Selected = true;
                    databox.Rows[rowindex].Selected = false;
                    row = databox.Rows[rowindex + 1];
                }
            }
            else if (databox.Rows.Count > 1)
            {
                int rowindex = 1;
                DataGridViewRow row;
                databox.Rows[rowindex + 1].Selected = true;
                databox.Rows[rowindex].Selected = false;
                row = databox.Rows[rowindex + 1];
            }
            ChangeRow();
        }

        private void btn_CopyArtist2ArtistSort_Click(object sender, EventArgs e)
        {
            //var cmd1 = "";
            ////DB_Path = DB_Path.Replace("dlc\\AccessDB.accdb","dlc");
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path.Replace("\\AccessDB.accdb;", "")))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET Artist_Sort = Artist";
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET Artist_Sort = Artist", cnb);
            MessageBox.Show("ArtistSort is now the same as Artist");
        }

        private void btn_CopyTitle2TitleSort_Click(object sender, EventArgs e)
        {
            //var cmd1 = "";
            ////var DB_Path = DB_Path + "\\AccessDB.accdb";
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET Song_Title_Sort = Song_Title";
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET Song_Title_Sort = Song_Title", cnb);
            MessageBox.Show("TitleSort is now the same as Title");
        }

        public void button1_Click_2(object sender, EventArgs e)
        {
            // MakeCover(DB_Path, txt_AlbumArt.Text, txt_Artist.Text, txt_Album.Text);
        }

        public static void MakeCover(OleDbConnection cnb)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
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

            DataSet dgt = new DataSet(); dgt = SelectFromDB("Standardization", "SELECT Artist, Album, AlbumArt_Correction FROM Standardization WHERE (AlbumArt_Correction <> \"\") GROUP BY Artist,Album,AlbumArt_Correction;", "", cnb);
            //NoRec = dgt.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = NoRec;
            foreach (DataRow dataRow in dgt.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var artpath_c = dataRow.ItemArray[2].ToString();
                var cmd1 = "";
                cmd1 = "UPDATE Main SET AlbumArtPath = \"" + artpath_c + "\" WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"";
                dgt = UpdateDB("Main", cmd1 + ";", cnb);
                dgt = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\"", "", cnb); try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                //cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + artpath_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\");";
                //dgt = UpdateDB("Standardization", cmd1 + ";");
            }
            //DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"");
            //DataSet dssx = new DataSet(); dxr = SelectFromDB("Main", "SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";");


            // lbl_NoRec = noOfRec.ToString() + " records.";
            //MessageBox.Show("Cover has been defaulted as Cover to " + NoRec.ToString() + " songs");
        }

        public static void ApplyArtistShort(OleDbConnection cnb)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short FROM Standardization WHERE (Artist_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short;", "", cnb);

            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var short_c = dataRow.ItemArray[1].ToString();
                var cmd1 = "UPDATE Main SET Artist_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\"";
                DataSet dus = UpdateDB("Main", cmd1 + ";", cnb);
                dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", "", cnb); try { norec = dus.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET Artist_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\"";
                dus = UpdateDB("Standardization", cmd1 + ";", cnb);
            }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplyAlbumShort(OleDbConnection cnb)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;

            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), Album_short FROM Standardization WHERE (Album_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist),Album_short,iif(Album_Correction<>\"\",Album_Correction,Album);", "", cnb);
            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var short_c = dataRow.ItemArray[2].ToString();
                var cmd1 = "UPDATE Main SET Album_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";";
                DataSet dus = UpdateDB("Main", cmd1, cnb);
                dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";", "", cnb); try { norec = dus.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET Album_Short = \"" + short_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                dus = UpdateDB("Standardization", cmd1 + ";", cnb);
            }
            //var noOfRec = dgt.Tables[0].Rows.Count;
            //lbl_NoRec = norec.ToString() + " records.";
            //MessageBox.Show("Album Short has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplySpotify(OleDbConnection cnb)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            //var norec = 0;
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL,SpotifyAlbumPath FROM Standardization WHERE (SpotifyArtistID <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), SpotifyArtistID, SpotifyAlbumID, SpotifyAlbumURL,SpotifyAlbumPath;", "", cnb);

            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var SpotifyArtistID = dataRow.ItemArray[2].ToString();
                var SpotifyAlbumID = dataRow.ItemArray[3].ToString();
                var SpotifyAlbumURL = dataRow.ItemArray[4].ToString();
                var SpotifyAlbumPath = dataRow.ItemArray[5].ToString();
                //var cmd1 = "UPDATE Main SET Spotify_Artist_ID = \"" + SpotifyArtistID + "\",Spotify_Album_ID = \"" + SpotifyAlbumID + "\",Spotify_Album_URL = \"" + SpotifyAlbumURL + "\",Spotify_Album_Path = \"" + SpotifyAlbumPath + "\", WHERE Album=\"" + SpotifyAlbumID + "\"";
                //DataSet dus = UpdateDB("Main", cmd1 + ";");
                //dus = SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\"", ""); try { norec = dus.Tables[0].Rows.Count; } catch { }
               var cmd1 = "UPDATE Standardization SET SpotifyArtistID = \"" + SpotifyArtistID + "\",SpotifyAlbumID = \"" + SpotifyAlbumID + "\",SpotifyAlbumURL = \"" + SpotifyAlbumURL + "\",SpotifyAlbumPath = \"" + SpotifyAlbumPath + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") and (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                var dus = UpdateDB("Standardization", cmd1 + ";", cnb);
            }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplyDefaultCover(OleDbConnection cnb)//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            //var norec = 0; //get al Default ON entries in standardization table
            DataSet dfz = new DataSet(); dfz = SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath) FROM Standardization WHERE (Default_Cover = \"Yes\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album),IIF(AlbumArt_Correction<>\"\", AlbumArt_Correction, SpotifyAlbumPath);", "", cnb);

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
                var dus = UpdateDB("Standardization", cmd1 + ";", cnb);
            }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (chbx_AutoSave.Checked && (txt_Artist_Correction.Text != "" || txt_Album_Correction.Text != "" || txt_Year_Correction.Text != "" || txt_AlbumArt_Correction.Text != "" || txt_Artist_Short.Text != "" || txt_Album_Short.Text != "")) SaveRecord();//&& SaveOK
            //else;
            //if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            //else SaveOK = false;
        }

        private void SaveRecord()
        {
            ConfigRepository.Instance()["dlcm_netstatus"] = netstatus;
            int i;
            DataSet dis = new DataSet();
            //try {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text!="")
            {
                i = databox.SelectedCells[0].RowIndex;
                //if (txt_Artist_Correction.Text != "")
                databox.Rows[i].Cells[3].Value = txt_Artist_Correction.Text;
                //if (txt_Album_Correction.Text != "")
                databox.Rows[i].Cells[5].Value = txt_Album_Correction.Text;
                //if (txt_AlbumArt_Correction.Text != "")
                databox.Rows[i].Cells[6].Value = txt_AlbumArt_Correction.Text;
                //if (txt_Comments.Text != "")
                databox.Rows[i].Cells[7].Value = txt_Comments.Text;
                databox.Rows[i].Cells[8].Value = txt_Artist_Short.Text;
                //if (txt_Artist_Short.Text != "")
                databox.Rows[i].Cells[9].Value = txt_Album_Short.Text;
                //if (txt_Year_Correction.Text != "")
                databox.Rows[i].Cells[10].Value = txt_Year_Correction.Text;
                if (chbx_Default_Cover.Checked) databox.Rows[i].Cells[15].Value = "Yes";
                else databox.Rows[i].Cells[15].Value = "No";
                //if (txt_Album_Short.Text != "")
                //Main.EndEdit();
                ////IDataAdapter.Update(dataTable);
                //Main.ResetBindings(false);


                var connection = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path);
                var command = connection.CreateCommand();
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    command.CommandText = "UPDATE Standardization SET ";

                    command.CommandText += "Artist_Correction = @param3, ";
                    command.CommandText += "Album_Correction = @param5, ";
                    command.CommandText += "AlbumArt_Correction = @param6, ";
                    command.CommandText += "Comments = @param7, ";
                    command.CommandText += "Artist_Short = @param8, ";
                    command.CommandText += "Album_Short = @param9, ";
                    command.CommandText += "Year_Correction = @param10, ";
                    command.CommandText += "SpotifyArtistID = @param11, ";
                    command.CommandText += "SpotifyAlbumID = @param12, ";
                    command.CommandText += "SpotifyAlbumURL = @param13, ";
                    command.CommandText += "SpotifyAlbumPath = @param14, ";
                    command.CommandText += "Default_Cover = @param15 ";
                    command.CommandText += "WHERE ID = " + txt_ID.Text;
                    command.Parameters.AddWithValue("@param3", databox.Rows[i].Cells[3].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param5", databox.Rows[i].Cells[5].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param6", databox.Rows[i].Cells[6].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param7", databox.Rows[i].Cells[7].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param8", databox.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param9", databox.Rows[i].Cells[9].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param10", databox.Rows[i].Cells[10].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param11", databox.Rows[i].Cells[11].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param12", databox.Rows[i].Cells[12].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param13", databox.Rows[i].Cells[13].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param14", databox.Rows[i].Cells[14].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param15", databox.Rows[i].Cells[15].Value.ToString() ?? DBNull.Value.ToString());
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        //Main.EndEdit();
                        //IDataAdapter.Update(dataTable);
                        //Main.ResetBindings(false);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Standardization DB connection in Standardization Edit screen ! " + DB_Path + "-" + command.CommandText);
                        throw;
                    }
                    finally
                    {
                        if (connection != null) connection.Close();
                    }
                    if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Correction Saved");
                }
            }
            //catch (Exception ex)
            //{ }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE ID IN (" + txt_ID.Text + ")", cnb);
            //var cmd = "DELETE * FROM Standardization WHERE ID IN (" + txt_ID.Text + ")";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    DataSet dhs = new DataSet();
            //    OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
            //    dhx.Fill(dhs, "Standardization");
            //    dhx.Dispose();
            //}


            //redresh 
            Populate(ref databox, ref Main);
            databox.EditingControlShowing += DataGridView1_EditingControlShowing;
            databox.Refresh();

            //advance or step back in the song list
            if (databox.Rows.Count > 1)
            {
                var prev = databox.SelectedCells[0].RowIndex;
                if (databox.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        i = databox.SelectedCells[0].RowIndex;
                        rowindex = i;
                        databox.Rows[rowindex - 1].Selected = true;
                        databox.Rows[rowindex].Selected = false;
                        row = databox.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    i = databox.SelectedCells[0].RowIndex;
                    rowindex = i;
                    databox.Rows[rowindex + 1].Selected = true;
                    databox.Rows[rowindex].Selected = false;
                    row = databox.Rows[rowindex + 1];
                }
            }
        }

        private void chbx_AutoSave_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            i = databox.SelectedCells[0].RowIndex;
            txt_AlbumArt_Correction.Text = databox.Rows[i].Cells[14].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (netstatus == "NOK" || netstatus == "") netstatus = ActivateSpotify_ClickAsync().Result.ToString();
            var artist = txt_Artist_Correction.Text == "" ? txt_Artist.Text : txt_Artist_Correction.Text;
            var album = txt_Album_Correction.Text == "" ? txt_Album.Text : txt_Album_Correction.Text;
            pB_ReadDLCs.Maximum = 5;
            pB_ReadDLCs.Value = 1;
            i = databox.SelectedCells[0].RowIndex;
            if (databox.Rows[i].Cells[14].Value.ToString() == ""|| databox.Rows[i].Cells[13].Value.ToString()=="" || !File.Exists(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png"))
            {
                pB_ReadDLCs.Value ++;
                Task<string> sptyfy = StartToGetSpotifyDetails(artist, album, "", txt_Year_Correction.Text, "");
                pB_ReadDLCs.Value++;
                //string s = sptyfy.Result.ToString();
                //string ert = "";
                //ert=s.Split(';')[0].ToString();
                //var trackno = sptyfy.Result.Split(';')[0].ToInt32();
                //var SpotifySongID = sptyfy.Result.Split(';')[1];
                var SpotifyArtistID = sptyfy.Result.Split(';')[2];
                var SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                var SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                var SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                var SpotifyAlbumYear = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";
                pB_ReadDLCs.Value++;
                if (SpotifyArtistID != "-" && SpotifyArtistID != "")
                {
                    databox.Rows[i].Cells[11].Value = SpotifyArtistID;
                    databox.Rows[i].Cells[12].Value = SpotifyAlbumID;
                    databox.Rows[i].Cells[13].Value = SpotifyAlbumURL;
                    databox.Rows[i].Cells[10].Value = SpotifyAlbumYear;
                    //using (WebClient wc = new WebClient())
                    //{
                    //    byte[] imageBytes = wc.DownloadData(new Uri(SpotifyAlbumURL));
                    //    FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png", FileMode.Create, System.IO.FileAccess.Write);
                    //    using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    pxbx_SavedSpotify.ImageLocation = SpotifyAlbumPath;// ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png";
                        databox.Rows[i].Cells[14].Value = pxbx_SavedSpotify.ImageLocation;
                        pB_ReadDLCs.Value++;
                    //}
                }
                else pB_ReadDLCs.Value++;
            }
            else
            {
                if (File.Exists(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png"))
                {
                    pB_ReadDLCs.Value++;
                    using (WebClient wc = new WebClient())
                    {
                        byte[] imageBytes = wc.DownloadData(new Uri(databox.Rows[i].Cells[13].Value.ToString()));
                        pB_ReadDLCs.Value++;
                        FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png", FileMode.Create, System.IO.FileAccess.Write);
                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                        pxbx_SavedSpotify.ImageLocation = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png";
                        databox.Rows[i].Cells[14].Value = pxbx_SavedSpotify.ImageLocation;
                        pB_ReadDLCs.Value++;
                    }
                    pB_ReadDLCs.Value++;
                }
                else pB_ReadDLCs.Value = 5;
            }
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            var result1 = MessageBox.Show("Are you sure you want to DELETE Standardizations (&Spotify downloaded info)?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.Yes)
            {
                DeleteFromDB("Standardization", "DELETE * FROM Standardization", cnb);
                Populate(ref databox, ref Main);
                databox.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            if (netstatus == "NOK" || netstatus == "") netstatus = ActivateSpotify_ClickAsync().Result.ToString();
            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Standardization", "SELECT IIF(Artist_Correction is null,Artist,Artist_Correction), IIF(Album_Correction is null,Album,Album_Correction), ID FROM Standardization WHERE SpotifyArtistID = \"-\" OR SpotifyArtistID = \"\" OR SpotifyArtistID is null ORDER BY SpotifyArtistID ASC;", "", cnb);
            var noOfRec = SongRecord.Tables[0].Rows.Count;
            //var vFilesMissingIssues = "";
            pB_ReadDLCs.Value = 0;pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.Maximum = noOfRec;
            
            //var MissingPSARC = false;
            for (var i = 0; i < noOfRec; i++)
            {
                pB_ReadDLCs.Increment(1);
                try
                {
                    Task<string> sptyfy = StartToGetSpotifyDetails(SongRecord.Tables[0].Rows[i].ItemArray[0].ToString(), SongRecord.Tables[0].Rows[i].ItemArray[1].ToString(), "", "", "");
                    //s = sptyfy.ToString();  
                    var trackno = sptyfy.Result.Split(';')[0].ToInt32();
                    //var Track_No = trackno.ToString();
                    var SpotifySongID = sptyfy.Result.Split(';')[1];
                    var SpotifyArtistID = sptyfy.Result.Split(';')[2];
                    var SpotifyAlbumID = sptyfy.Result.Split(';')[3];
                    var SpotifyAlbumURL = sptyfy.Result.Split(';')[4];
                    var SpotifyAlbumPath = sptyfy.Result.Split(';')[5];
                    var SpotifyAlbumYear = sptyfy.Result.Split(';')[6].Length >= 4 ? sptyfy.Result.Split(';')[6].Substring(0, 4) : "";
                    //if (ConfigRepository.Instance()["dlcm_AdditionalManipul59"] == "Yes")
                    //{
                    var cmds = "UPDATE Standardization SET ";// Spotify_Song_ID=\"" + SpotifySongID + "\", SpotifyArtistID=\"" + SpotifyArtistID + "\"";
                    cmds += " SpotifyAlbumID=\"" + SpotifyAlbumID + "\"" + ", SpotifyAlbumURL=\"" + SpotifyAlbumURL + "\"" + ",SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\",Year_Correction=\"" + SpotifyAlbumYear + "\"";
                    cmds += " WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[2].ToString();
                    DataSet dis = new DataSet(); 
                    if (trackno> 0 && SpotifySongID != "" && SpotifySongID != "-") dis = UpdateDB("Standardization", cmds + ";", cnb);
                    DateTime timestamp = UpdateLog(DateTime.Now, i + "/" + noOfRec + " Spotify details: " + trackno + " " + SpotifyAlbumPath, true , ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, null);
                    //ADD STADARDISATION UPDATE
                    //Updating the Standardization table
                    //DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\""
                    //    + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", ConfigRepository.Instance()["dlcm_DBFolder"], cnb);

                    //if (dzs.Tables[0].Rows.Count == 0)
                    //{
                    //var updcmd = "UPDATE Stadarization SET SpotifyArtistID=\"" + SpotifyArtistID + "\" , SpotifyAlbumID=\"" + SpotifyArtistID + "\", SpotifyAlbumURL=\""
                    //    + SpotifyAlbumURL + "\", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\" WHERE (Artist=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\" OR Artist_Correction=\""
                    //    + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\") AND (Artist=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\" OR Album_Correction=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\")";

                    //UpdateDB("Standardization", updcmd + ";", cnb);
                    //}

                }
                catch (Exception ex) { var tust = "Spotify Error ..." + ex; UpdateLog(DateTime.Now, tust, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            }
        }

        private void databox_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void databox_SelectionChanged(object sender, EventArgs e)
        {
            //if (txt_ID.Text != "") ChangeRow();
            var line = -1;
             if(databox.SelectedCells.Count>0) line = databox.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        private void btn_CheckOnline_Click(object sender, EventArgs e)
        {
            i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + txt_Artist.Text + "+" + txt_Album.Text;

            try
            {
                Process process = Process.Start(@link);
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_LogPath"], ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            }
        }
    }
}
