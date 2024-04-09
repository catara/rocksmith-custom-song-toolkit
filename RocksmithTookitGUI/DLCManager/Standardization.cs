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
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Data.SQLite;
using SQLite;
using SpotifyApi.NetCore;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Standardization : Form
    {
        //public Standardization(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb, string artist, SQLiteConnection cnnz)
        public Standardization(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb, string artist, SQLite.SQLiteConnection cnnc)
        {
            this.Artist = artist;
            InitializeComponent();
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes") btn_DeleteAll.Visible = true;
            cnb = cnnb;
            cnc = cnnc;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        DateTime timestamp;
        public string netstatus = ConfigRepository.Instance()["dlcm_netstatus"];

        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION = "StandardizationDB";
        public bool SaveOK = true;
        public string SearchCmd = "Select * FROM Standardization;";
        public bool SearchON = false;
        int GoTocounter = 0;

        public string DB_Path = "";
        public string TempPath = "";
        public string Artist = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        //public OleDbConnection cnb;
        ////public SQLiteConnection cnz;
        //public SQLite.SQLiteConnection cnc;
        bool updateAutoGroups = true;


        private void Standardization_Load(object sender, EventArgs e)
        {
            Populate(ref databox, ref Main);
            databox.EditingControlShowing += DataGridView1_EditingControlShowing;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            if (Artist != null && Artist != "")
            {
                int i = 0;
                if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")

                    i = databox.SelectedCells[0].RowIndex;
                do
                {
                    //DataViewGrid.SelectedCells[/*0*/].RowIndex
                    if (databox.Rows[i].Cells[3].Value.ToString() == Artist)
                    {
                        //databox.Rows[0].Selected = false; databox.Rows[i];
                        databox.CurrentCell = databox.Rows[i].Cells[0];
                        //ChangeRow();
                        break;
                    }
                    i++;
                }
                while (i > 0 && databox.RowCount > 1 && i < databox.RowCount);
            }

            chbx_a1.Text = c("dlcm_CustomToAtribute_1"); if (c("dlcm_CustomToAtribute_1") != "") chbx_a1.Enabled = true;
            else chbx_a1.Enabled = false;
            chbx_a2.Text = c("dlcm_CustomToAtribute_2"); if (c("dlcm_CustomToAtribute_2") != "") chbx_a2.Enabled = true;
            else chbx_a2.Enabled = false;
            chbx_a3.Text = c("dlcm_CustomToAtribute_3"); if (c("dlcm_CustomToAtribute_3") != "") chbx_a3.Enabled = true;
            else chbx_a3.Enabled = false;
            chbx_a4.Text = c("dlcm_CustomToAtribute_4"); if (c("dlcm_CustomToAtribute_4") != "") chbx_a4.Enabled = true;
            else chbx_a4.Enabled = false;
            chbx_a5.Text = c("dlcm_CustomToAtribute_5"); if (c("dlcm_CustomToAtribute_5") != "") chbx_a5.Enabled = true;
            else chbx_a5.Enabled = false;

        }

        //public void OpenDb()
        //{
        //    var tz = ConfigRepository.Instance()["dlcm_DBFolder"];
        //    tz = tz.Replace("AccessDB.accdb", "SQLLiteDB.db");
        //    if (ConfigRepository.Instance()["dlcm_AdditionalManipul114"] != "Yes")
        //        try
        //        {
        //            if (File.Exists(cnb.DataSource.ToString())) cnb.Open();
        //        }
        //        catch (Exception exx)
        //        {

        //            ShowConnectivityError(exx, "", null);
        //            try
        //            {
        //                if (File.Exists(cnb.DataSource.ToString())) cnb.Open(); //2nd time makes it work sometimes e.g. x64 solution
        //            }
        //            catch (Exception ex)
        //            {
        //                string vb = null; vb = DisplayData();
        //                ShowConnectivityError(ex, "2nd FAIL to use M$ ACCESS plugin:\n" + vb, null);
        //                //revert to SQLite
        //                if (File.Exists(tz))
        //                {
        //                    ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";

        //                    ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
        //                }
        //                else MessageBox.Show("No Microsoft Access or SQLite databases (or access;plugins etc) available. Good Luck as (the) C-DLC Manager wont really work!");
        //            }
        //        }
        //    else
        //        try
        //        {
        //            ConfigRepository.Instance()["dlcm_DBFolder"] = tz;
        //            if (File.Exists(ConfigRepository.Instance()["dlcm_DBFolder"])) cnc = new SQLite.SQLiteConnection(ConfigRepository.Instance()["dlcm_DBFolder"]);
        //            ConfigRepository.Instance()["dlcm_AdditionalManipul114"] = "Yes";
        //        }
        //        catch (Exception exx)
        //        {
        //            ShowConnectivityError(exx, "", null);
        //        }
        //}
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

        private void btn_OpenAccess_Click(object sender, EventArgs e)
        {
            StartProcesss(@DB_Path, null);
            //try
            //{
            //    Process process = Process.Start(@DB_Path);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Standardization DB connection in StandardizationDB ! " + DB_Path);
            //}
        }

        public void ChangeRow()
        {

            updateAutoGroups = false;
            int i;
            if (databox.SelectedCells.Count > 0 && databox.SelectedCells[0].ToString() != "")
            {
                i = databox.SelectedCells[0].RowIndex;
                if (SearchON) SearchON = false;
                btn_GoTo.Enabled = false;
                //"SELECT ID-0, Suspect-1, Suspect_Reason-2, Artist-3, Artist_Correction-4, Album-5, Album_Correction-6, AlbumArt_Correction-7, Comments-8, Artist_Short-9, Album_Short-10
                //, Year_Correction-11, SpotifyArtistID-12, SpotifyAlbumID-13, SpotifyAlbumURL-14, SpotifyAlbumPath-15, Default_Cover-16, Artist_AutoGroup-17, CustomToAtribute_1-18
                //, CustomToAtribute_2-19, CustomToAtribute_3-20, CustomToAtribute_4-21, CustomToAtribute_5-22 FROM Standardization

                txt_ID.Text = databox.Rows[i].Cells[0].Value.ToString();
                txt_Artist.Text = databox.Rows[i].Cells[3].Value.ToString();
                txt_Artist_Correction.Text = databox.Rows[i].Cells[4].Value.ToString();
                txt_Album.Text = databox.Rows[i].Cells[5].Value.ToString();
                txt_Album_Correction.Text = databox.Rows[i].Cells[6].Value.ToString();
                txt_AlbumArt_Correction.Text = databox.Rows[i].Cells[7].Value.ToString();
                txt_Comments.Text = databox.Rows[i].Cells[8].Value.ToString();
                txt_Artist_Short.Text = databox.Rows[i].Cells[9].Value.ToString();
                txt_Album_Short.Text = databox.Rows[i].Cells[10].Value.ToString();
                txt_Year_Correction.Text = databox.Rows[i].Cells[11].Value.ToString();
                if (databox.Rows[i].Cells[16].Value.ToString() == "Yes") chbx_Default_Cover.Checked = true;
                else chbx_Default_Cover.Checked = false;

                if (databox.Rows[i].Cells[18].Value.ToString() != "") chbx_a1.Checked = true;
                else chbx_a1.Checked = false;
                if (databox.Rows[i].Cells[19].Value.ToString() != "") chbx_a2.Checked = true;
                else chbx_a2.Checked = false;
                if (databox.Rows[i].Cells[20].Value.ToString() != "") chbx_a3.Checked = true;
                else chbx_a3.Checked = false;
                if (databox.Rows[i].Cells[21].Value.ToString() != "") chbx_a4.Checked = true;
                else chbx_a4.Checked = false;
                if (databox.Rows[i].Cells[22].Value.ToString() != "") chbx_a5.Checked = true;
                else chbx_a5.Checked = false;

                //if (c("dlcm_CustomToAtribute_1") != "") chbx_a1.Enabled = false;
                //else chbx_a1.Enabled = true;
                //if (c("dlcm_CustomToAtribute_2") != "") chbx_a2.Enabled = false;
                //else chbx_a2.Enabled = true;
                //if (c("dlcm_CustomToAtribute_3") != "") chbx_a3.Enabled = false;
                //else chbx_a3.Enabled = true;
                //if (c("dlcm_CustomToAtribute_4") != "") chbx_a4.Enabled = false;
                //else chbx_a4.Enabled = true;
                //if (c("dlcm_CustomToAtribute_5") != "") chbx_a5.Enabled = false;
                //else chbx_a5.Enabled = true;
                if (txt_AlbumArt_Correction.Text != "" && File.Exists(txt_AlbumArt_Correction.Text.Replace(".dds", ".png")))
                {
                    picbx_AlbumArtPath.ImageLocation = txt_AlbumArt_Correction.Text;
                    lbl_corrected.Visible = true;
                    lbl_SpotifyCover.Text = "Saved from Spotify:";
                }
                else
                    lbl_corrected.Visible = false;

                if (databox.Rows[i].Cells[15].Value != null) if (databox.Rows[i].Cells[15].Value.ToString() != "" && File.Exists(databox.Rows[i].Cells[15].Value.ToString()))
                    {
                        pxbx_SavedSpotify.ImageLocation = databox.Rows[i].Cells[15].Value.ToString();
                        lbl_SpotifyCover.Visible = true;
                        lbl_SpotifyCover.Text = "Saved from Spotify:";
                    }
                    else
                    {
                        var ts = "SELECT AlbumArtPath, Album_ArtPathOrig FROM Main WHERE " +
                            "(Artist =\"" + txt_Artist_Correction.Text + "\" OR Artist =\"" + txt_Artist.Text + "\")"
                            + " AND (Album=\"" + txt_Album_Correction.Text + "\" OR Album=\"" + txt_Album.Text + "\")";
                        DataSet dus = new DataSet(); dus = SelectFromDB("Main", ts, "", cnb, cnc);
                        //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                        var rowc = GetNoRec(dus, cnb, cnc);//dus.Tables.Count == 0 ? 0 : dus.Tables[0].Rows.Count;
                        //foreach (DataRow dataRow in dus.Tables[0].Rows)
                        //{
                        //files[i] = new Files();
                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                        var pix = "";
                        if (rowc > 0)
                        {
                            pix = GetAlbumArtPath(dus.Tables[0].Rows[0].ItemArray[0].ToString(), dus.Tables[0].Rows[0].ItemArray[0].ToString(), ".png");
                            pxbx_SavedSpotify.ImageLocation = pix;
                            lbl_SpotifyCover.Visible = true;
                            lbl_SpotifyCover.Text = "Sample from Imported:";
                        }
                        else
                        {
                            pxbx_SavedSpotify.ImageLocation = null;
                            lbl_SpotifyCover.Visible = false;
                        }
                    }
                cbx_Groups.Text = databox.Rows[i].Cells[17].Value.ToString();
                //if (DataGridView1.Rows[i].Cells["Default_Cover"].Value.ToString() == "Yes") chbx_Default_Cover.Checked = true;
                //    else chbx_Default_Cover.Checked = false;
                //if (chbx_AutoSave.Checked) SaveOK = true;
                //else SaveOK = false;

                //else SaveOK = false;
                updateAutoGroups = true;
                SaveOK = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
           SaveOK = true; SaveRecord();
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

            var similar = "UPDATE Standardization AS S SET Suspect=\"Yes\", Suspect_Reason=\"Different\" where (SELECT ID from Standardization AS O" +
                 " WHERE LCASE(S.Artist)=LCASE(O.Artist) and LCASE(S.Artist_Correction)=LCASE(O.Artist_Correction)" +
                 " and LCASE(S.Album_Correction)=LCASE(O.Album_Correction) and LCASE(S.Album)=LCASE(O.Album) )";
            //IIF(count(*) > 1, ID,\"\") as Suspect 
            var samecont = "UPDATE Standardization AS S SET Suspect=\"Yes\", Suspect_Reason=\"Different\" where (SELECT ID from Standardization AS O" +
                 " WHERE LCASE(S.Artist)=LCASE(O.Artist) and LCASE(S.Artist_Correction)=LCASE(O.Artist_Correction)" +
                 " and LCASE(S.Album_Correction)=LCASE(O.Album_Correction) and LCASE(S.Album)=LCASE(O.Album) )";

            dssx = SelectFromDB("Standardization", "SELECT ID, Suspect, Suspect_Reason, Artist, Artist_Correction, Album, Album_Correction, AlbumArt_Correction, Comments, Artist_Short, Album_Short, Year_Correction, SpotifyArtistID," +
                " SpotifyAlbumID, SpotifyAlbumURL, SpotifyAlbumPath, Default_Cover, Artist_AutoGroup, CustomToAtribute_1, CustomToAtribute_2, CustomToAtribute_3, CustomToAtribute_4, CustomToAtribute_5 FROM Standardization as S" +
                " ORDER BY Artist, Album, Artist_Correction, Album_Correction, CustomToAtribute_1, CustomToAtribute_2, CustomToAtribute_3, CustomToAtribute_4, CustomToAtribute_5;", "", cnb, cnc);

            //        dssx = SelectFromDB("Standardization", "SELECT ID(0), Suspect(1), Suspect_Reason(2), Artist(3), Artist_Correction(4), Album(5), Album_Correction(6), AlbumArt_Correction(7), Comments(8), Artist_Short(9), Album_Short(10), Year_Correction(11)" +
            //            ", SpotifyArtistID(12),SpotifyAlbumID(13), SpotifyAlbumURL(14), SpotifyAlbumPath(15), Default_Cover(16), Artist_AutoGroup(17), CustomToAtribute_1(18), CustomToAtribute_2(19), CustomToAtribute_3(20), CustomToAtribute_4(21), CustomToAtribute_5(22) FROM Standardization as S" +
            //" ORDER BY Artist, Album, Artist_Correction, Album_Correction, CustomToAtribute_1, CustomToAtribute_2, CustomToAtribute_3, CustomToAtribute_4, CustomToAtribute_5;", "", cnb, cnc);
            //OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
            //da.Fill(dssx, "Standardization");
            var noOfRec = GetNoRec(dssx, cnb, cnc); //dssx.Tables[0].Rows.Count;
            lbl_NoRec.Text = noOfRec.ToString() + " records.";
            //}
            if (noOfRec > 0)
            {
                //DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 35 };
                //DataGridViewTextBoxColumn Suspect = new DataGridViewTextBoxColumn { DataPropertyName = "Suspect", HeaderText = "Suspect ", Width = 35 };
                //DataGridViewTextBoxColumn Suspect_Reason = new DataGridViewTextBoxColumn { DataPropertyName = "Suspect_Reason", HeaderText = "Suspect_Reason ", Width = 35 };
                //DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist ", Width = 135 };
                //DataGridViewTextBoxColumn Artist_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Correction", HeaderText = "Artist_Correction ", Width = 135 };
                //DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album ", Width = 165 };
                //DataGridViewTextBoxColumn Album_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Correction", HeaderText = "Album_Correction ", Width = 165 };
                //DataGridViewTextBoxColumn AlbumArt_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Correction", HeaderText = "AlbumArt_Correction ", Width = 30 };
                //DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments ", Width = 45 };
                //DataGridViewTextBoxColumn Artist_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Short", HeaderText = "Artist_Short ", Width = 55 };
                //DataGridViewTextBoxColumn Album_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Short", HeaderText = "Album_Short ", Width = 55 };
                //DataGridViewTextBoxColumn Year_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Year_Correction", HeaderText = "Year_Correction ", Width = 55 };
                //DataGridViewTextBoxColumn SpotifyArtistID = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyArtistID", HeaderText = "SpotifyArtistID ", Width = 30 };
                //DataGridViewTextBoxColumn SpotifyAlbumID = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumID", HeaderText = "SpotifyAlbumID ", Width = 30 };
                //DataGridViewTextBoxColumn SpotifyAlbumURL = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumURL", HeaderText = "SpotifyAlbumURL ", Width = 30 };
                //DataGridViewTextBoxColumn SpotifyAlbumPath = new DataGridViewTextBoxColumn { DataPropertyName = "SpotifyAlbumPath", HeaderText = "SpotifyAlbumPath ", Width = 30 };
                //DataGridViewTextBoxColumn Default_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Default_Cover", HeaderText = "Default_Cover ", Width = 30 };
                //DataGridViewTextBoxColumn Artist_AutoGroup = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_AutoGroup", HeaderText = "Artist_AutoGroup ", Width = 55 };
                //DataGridViewTextBoxColumn CustomToAtribute_1 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomToAtribute_1", HeaderText = "CustomToAtribute_1 ", Width = 55 };
                //DataGridViewTextBoxColumn CustomToAtribute_2 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomToAtribute_2", HeaderText = "CustomToAtribute_2 ", Width = 55 };
                //DataGridViewTextBoxColumn CustomToAtribute_3 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomToAtribute_3", HeaderText = "CustomToAtribute_3 ", Width = 55 };
                //DataGridViewTextBoxColumn CustomToAtribute_4 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomToAtribute_4", HeaderText = "CustomToAtribute_4 ", Width = 55 };
                //DataGridViewTextBoxColumn CustomToAtribute_5 = new DataGridViewTextBoxColumn { DataPropertyName = "CustomToAtribute_5", HeaderText = "CustomToAtribute_5 ", Width = 55 };
                //DataGridView.AutoGenerateColumns = false;

                //DataGridView.Columns.AddRange(new DataGridViewColumn[]
                //    {
                //        ID,
                //        Suspect,
                //        Artist,
                //        Artist_Correction,
                //        Album,
                //        Album_Correction,
                //        AlbumArt_Correction,
                //        Comments,
                //        Artist_Short,
                //        Album_Short,
                //        Year_Correction,
                //        SpotifyArtistID,
                //        SpotifyAlbumID,
                //        SpotifyAlbumURL,
                //        SpotifyAlbumPath,
                //        Default_Cover,
                //        Artist_AutoGroup,
                //        Suspect_Reason,
                //        CustomToAtribute_1,
                //        CustomToAtribute_2,
                //        CustomToAtribute_3,
                //        CustomToAtribute_4,
                //        CustomToAtribute_5
                //    }
                //);

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
                ////    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or You need to Download Connectivity patch 32/64 bit to match your version of Office @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the Standardization DB", false, false);
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
                //bs.ResetBindings(false);
                //dssx.Tables["Standardization"].AcceptChanges();
                //bs.DataSource 
                DataGridView.DataSource = dssx.Tables["Standardization"];
                //DataGridView.DataSource = null;
                //DataGridView.DataSource = bs;
                DataGridView.Refresh();
                dssx.Dispose();
                ChangeRow();
            }
        }

        //private class Files
        //{
        //    public string ID { get; set; }
        //    public string Suspect { get; set; }
        //    public string Artist { get; set; }
        //    public string Artist_Correction { get; set; }
        //    public string Album { get; set; }
        //    public string Album_Correction { get; set; }
        //    public string AlbumArt_Correction { get; set; }
        //    public string Comments { get; set; }
        //    public string Artist_Short { get; set; }
        //    public string Album_Short { get; set; }
        //    public string Year_Correction { get; set; }
        //    public string SpotifyArtistID { get; set; }
        //    public string SpotifyAlbumID { get; set; }
        //    public string SpotifyAlbumURL { get; set; }
        //    public string SpotifyAlbumPath { get; set; }
        //    public string Default_Cover { get; set; }
        //    public string Artist_AutoGroup { get; set; }
        //    public string Suspect_Reason { get; set; }
        //    public string CustomToAtribute_1 { get; set; }
        //    public string CustomToAtribute_2 { get; set; }
        //    public string CustomToAtribute_3 { get; set; }
        //    public string CustomToAtribute_4 { get; set; }
        //    public string CustomToAtribute_5 { get; set; }
        //}

        private UtilitiesFunctions.Standardization[] files = new UtilitiesFunctions.Standardization[20000];

        //Generic procedure to read and parse Standardization.DB (&others..soon)
        //public int SQLAccess(string cmd)
        //{
        //    //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
        //    //Files[] files = new Files[10000];

        //    var MaximumSize = 0;

        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
        //    //try
        //    //{
        //    //MessageBox.Show(DB_Path);
        //    //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + DB_Path))
        //    //{
        //    //    DataSet dus = new DataSet();
        //    //    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
        //    //    dax.Fill(dus, "Standardization");
        //    DataSet dus = new DataSet(); dus = SelectFromDB("Standardization", cmd, "", cnb, cnc);
        //    var i = 0;
        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
        //    MaximumSize = GetNoRec(dus, cnb, cnc);//dus.Tables[0].Rows.Count;
        //    foreach (DataRow dataRow in dus.Tables[0].Rows)
        //    {
        //        files[i] = new UtilitiesFunctions.Standardization();

        //        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
        //        files[i].ID = dataRow.ItemArray[0].ToString().ToInt32();
        //        files[i].Suspect = dataRow.ItemArray[1].ToString();
        //        files[i].Suspect_Reason = dataRow.ItemArray[17].ToString();
        //        files[i].Artist = dataRow.ItemArray[2].ToString();
        //        files[i].Artist_Correction = dataRow.ItemArray[3].ToString();
        //        files[i].Album = dataRow.ItemArray[4].ToString();
        //        files[i].Album_Correction = dataRow.ItemArray[5].ToString();
        //        files[i].AlbumArt_Correction = dataRow.ItemArray[6].ToString();
        //        files[i].Comments = dataRow.ItemArray[6].ToString();
        //        files[i].Artist_Short = dataRow.ItemArray[8].ToString();
        //        files[i].Album_Short = dataRow.ItemArray[9].ToString();
        //        files[i].Year_Correction = dataRow.ItemArray[10].ToString();
        //        files[i].SpotifyArtistID = dataRow.ItemArray[11].ToString();
        //        files[i].SpotifyAlbumID = dataRow.ItemArray[12].ToString();
        //        files[i].SpotifyAlbumURL = dataRow.ItemArray[13].ToString();
        //        files[i].SpotifyAlbumPath = dataRow.ItemArray[14].ToString();
        //        files[i].Default_Cover = dataRow.ItemArray[15].ToString();
        //        files[i].Artist_AutoGroup = dataRow.ItemArray[16].ToString();
        //        files[i].CustomToAtribute_1 = dataRow.ItemArray[17].ToString();
        //        files[i].CustomToAtribute_2 = dataRow.ItemArray[18].ToString();
        //        files[i].CustomToAtribute_3 = dataRow.ItemArray[19].ToString();
        //        files[i].CustomToAtribute_4 = dataRow.ItemArray[20].ToString();
        //        files[i].CustomToAtribute_5 = dataRow.ItemArray[21].ToString();
        //        i++;
        //    }
        //    //Closing Connection
        //    //        dax.Dispose();
        //    //        cnn.Close();
        //    //        //rtxt_StatisticsOnReadDLCs.Text += i;
        //    //        //var ex = 0;
        //    //    }
        //    //}
        //    //catch (System.IO.FileNotFoundException ee)
        //    //{
        //    //    MessageBox.Show(ee.Message + "Can not open Standardization DB connection ! ");
        //    //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //}
        //    ////rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
        //    return MaximumSize;//files[10000];
        //}

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(cnb, cnc, false);
            ///DB_Path, TempPath, false, "", AllowEncriptb, AllowORIGDeleteb,/.Replace("\\AccessDB.accdb", "")
            frm.Show();
        }

        public void Standardization_Click(object sender, EventArgs e)
        {
            //DLCManager v1 = new DLCManager();
            string txt = DB_Path;//.Replace("\\AccessDB.accdb", "");
                                 //MessageBox.Show(txt);
                                 //string returned = GenericFunctions.OneTranslation_And_Correction(txt, pB_ReadDLCs, cnb, null, txt_Artist_Correction.Text, txt_Album_Correction.Text, txt_Year_Correction.Text);

            GenericFunctions.Translation_And_Correction(txt, pB_ReadDLCs, cnb, null, cnc);
            //refresh 
            Populate(ref databox, ref Main);
            databox.Refresh();
            //advance or step back in the song list
            //int i = 0;
            //if (databox.Rows.Count > 1 && databox.SelectedCells.Count > 0)
            //{
            //    var prev = databox.SelectedCells[0].RowIndex;
            //    if (databox.Rows.Count == prev + 2)
            //        if (prev == 0) return;
            //        else
            //        {
            //            int rowindex;
            //            DataGridViewRow row;
            //            i = databox.SelectedCells[0].RowIndex;
            //            rowindex = i;
            //            databox.Rows[rowindex - 1].Selected = true;
            //            databox.Rows[rowindex].Selected = false;
            //            row = databox.Rows[rowindex - 1];
            //        }
            //    else
            //    {
            //        int rowindex;
            //        DataGridViewRow row;
            //        i = databox.SelectedCells[0].RowIndex;
            //        rowindex = i;
            //        databox.Rows[rowindex + 1].Selected = true;
            //        databox.Rows[rowindex].Selected = false;
            //        row = databox.Rows[rowindex + 1];
            //    }
            //}
            //else if (databox.Rows.Count > 1)
            //{
            //    int rowindex = 1;
            //    DataGridViewRow row;
            //    databox.Rows[rowindex + 1].Selected = true;
            //    databox.Rows[rowindex].Selected = false;
            //    row = databox.Rows[rowindex + 1];
            //}
            //ChangeRow();
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET Artist_Sort = Artist", cnb, cnc);
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
            DataSet dxr = new DataSet(); dxr = UpdateDB("Main", "UPDATE Main SET Song_Title_Sort = Song_Title", cnb, cnc);
            MessageBox.Show("TitleSort is now the same as Title");
        }

        private void SaveRecord()
        {
            if (!SaveOK) return;
            ConfigRepository.Instance()["dlcm_netstatus"] = netstatus;
            int i;
            DataSet dis = new DataSet();
            //try {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = databox.SelectedCells[0].RowIndex;
                //"SELECT ID-0, Suspect-1, Suspect_Reason-2, Artist-3, Artist_Correction-4, Album-5, Album_Correction-6, AlbumArt_Correction-7, Comments-8, Artist_Short-9, Album_Short-10
                //, Year_Correction-11, SpotifyArtistID-12, SpotifyAlbumID-13, SpotifyAlbumURL-14, SpotifyAlbumPath-15, Default_Cover-16, Artist_AutoGroup-17, CustomToAtribute_1-18
                //, CustomToAtribute_2-19, CustomToAtribute_3-20, CustomToAtribute_4-21, CustomToAtribute_5-22 FROM Standardization

                //if (txt_Artist_Correction.Text != "")
                databox.Rows[i].Cells[4].Value = txt_Artist_Correction.Text;
                //if (txt_Album_Correction.Text != "")
                databox.Rows[i].Cells[6].Value = txt_Album_Correction.Text;
                //if (txt_AlbumArt_Correction.Text != "")
                databox.Rows[i].Cells[7].Value = txt_AlbumArt_Correction.Text;
                //if (txt_Comments.Text != "")
                databox.Rows[i].Cells[8].Value = txt_Comments.Text;
                databox.Rows[i].Cells[9].Value = txt_Artist_Short.Text;
                //if (txt_Artist_Short.Text != "")
                databox.Rows[i].Cells[10].Value = txt_Album_Short.Text;
                //if (txt_Year_Correction.Text != "")
                databox.Rows[i].Cells[11].Value = txt_Year_Correction.Text;
                if (chbx_Default_Cover.Checked) databox.Rows[i].Cells[16].Value = "Yes";
                else databox.Rows[i].Cells[16].Value = "";
                databox.Rows[i].Cells[17].Value = cbx_Groups.Text;

                if (chbx_a1.Checked) databox.Rows[i].Cells[18].Value = "Yes";
                else databox.Rows[i].Cells[18].Value = "";
                if (chbx_a2.Checked) databox.Rows[i].Cells[19].Value = "Yes";
                else databox.Rows[i].Cells[19].Value = "";
                if (chbx_a3.Checked) databox.Rows[i].Cells[20].Value = "Yes";
                else databox.Rows[i].Cells[20].Value = "";
                if (chbx_a4.Checked) databox.Rows[i].Cells[21].Value = "Yes";
                else databox.Rows[i].Cells[21].Value = "";
                if (chbx_a5.Checked) databox.Rows[i].Cells[22].Value = "Yes";
                else databox.Rows[i].Cells[22].Value = "";
                //if (txt_Album_Short.Text != "")
                //Main.EndEdit();
                ////IDataAdapter.Update(dataTable);
                //Main.ResetBindings(false);


                //var connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path);
                var command = cnb.CreateCommand();
                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //{
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
                command.CommandText += "Default_Cover = @param15, ";
                command.CommandText += "Artist_AutoGroup = @param16, ";
                command.CommandText += "CustomToAtribute_1 = @param17,";
                command.CommandText += "CustomToAtribute_2 = @param18,";
                command.CommandText += "CustomToAtribute_3 = @param19,";
                command.CommandText += "CustomToAtribute_4 = @param20,";
                command.CommandText += "CustomToAtribute_5 = @param21 ";
                command.CommandText += " WHERE ID = " + txt_ID.Text;
                //"SELECT ID-0, Suspect-1, Suspect_Reason-2, Artist-3, Artist_Correction-4, Album-5, Album_Correction-6, AlbumArt_Correction-7, Comments-8, Artist_Short-9, Album_Short-10
                //, Year_Correction-11, SpotifyArtistID-12, SpotifyAlbumID-13, SpotifyAlbumURL-14, SpotifyAlbumPath-15, Default_Cover-16, Artist_AutoGroup-17, CustomToAtribute_1-18
                //, CustomToAtribute_2-19, CustomToAtribute_3-20, CustomToAtribute_4-21, CustomToAtribute_5-22 FROM Standardization
                command.Parameters.AddWithValue("@param3", databox.Rows[i].Cells[4].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param5", databox.Rows[i].Cells[6].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param6", databox.Rows[i].Cells[7].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param7", databox.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param8", databox.Rows[i].Cells[9].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param9", databox.Rows[i].Cells[10].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param10", databox.Rows[i].Cells[11].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param11", databox.Rows[i].Cells[12].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param12", databox.Rows[i].Cells[13].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param13", databox.Rows[i].Cells[14].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param14", databox.Rows[i].Cells[15].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param15", databox.Rows[i].Cells[16].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param16", databox.Rows[i].Cells[17].Value.ToString() ?? DBNull.Value.ToString());

                command.Parameters.AddWithValue("@param17", databox.Rows[i].Cells[18].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param18", databox.Rows[i].Cells[19].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param19", databox.Rows[i].Cells[20].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param20", databox.Rows[i].Cells[21].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param21", databox.Rows[i].Cells[22].Value.ToString() ?? DBNull.Value.ToString());
                command.CommandType = CommandType.Text;

                UpdateDBbyExecuteNonQuery(command, cnb, cnc);
                command.Dispose();

                //    try
                //    {
                //        connection.Open();
                //        command.ExecuteNonQuery();
                //        //Main.EndEdit();
                //        //IDataAdapter.Update(dataTable);
                //        //Main.ResetBindings(false);

                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        MessageBox.Show("Can not open Standardization DB connection in Standardization Edit screen ! " + DB_Path + "-" + command.CommandText);
                //        throw;
                //    }
                //    finally
                //    {
                //        if (connection != null) connection.Close();
                //    }
                //    if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Correction Saved");
                //}
            }
            //catch (Exception ex)
            //{ }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE ID IN (" + txt_ID.Text + ")", cnb, cnc);
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

        private void btn_CorrectWithSpotify_Click(object sender, EventArgs e)
        {
            i = databox.SelectedCells[0].RowIndex;
            txt_AlbumArt_Correction.Text = databox.Rows[i].Cells[15].Value.ToString();
        }

        private void Btn_CorrectWithSpotify_Click(object sender, EventArgs e)
        {
            if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            if (netstatus == "NOK" || netstatus == "") return;

            var artist = txt_Artist_Correction.Text == "" ? txt_Artist.Text : txt_Artist_Correction.Text;
            var album = txt_Album_Correction.Text == "" ? txt_Album.Text : txt_Album_Correction.Text;
            pB_ReadDLCs.Maximum = 5;
            pB_ReadDLCs.Value = 1;
            i = databox.SelectedCells[0].RowIndex;
            if (databox.Rows[i].Cells[15].Value.ToString() == "" || databox.Rows[i].Cells[14].Value.ToString() == "" || !File.Exists(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png"))
            {
                pB_ReadDLCs.Value++;
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
                {                //"SELECT ID-0, Suspect-1, Suspect_Reason-2, Artist-3, Artist_Correction-4, Album-5, Album_Correction-6, AlbumArt_Correction-7, Comments-8, Artist_Short-9, Album_Short-10
                                 //, Year_Correction-11, SpotifyArtistID-12, SpotifyAlbumID-13, SpotifyAlbumURL-14, SpotifyAlbumPath-15, Default_Cover-16, Artist_AutoGroup-17, CustomToAtribute_1-18
                                 //, CustomToAtribute_2-19, CustomToAtribute_3-20, CustomToAtribute_4-21, CustomToAtribute_5-22 FROM Standardization
                    databox.Rows[i].Cells[12].Value = SpotifyArtistID;
                    databox.Rows[i].Cells[13].Value = SpotifyAlbumID;
                    databox.Rows[i].Cells[14].Value = SpotifyAlbumURL;
                    databox.Rows[i].Cells[11].Value = SpotifyAlbumYear;
                    //using (WebClient wc = new WebClient())
                    //{
                    //    byte[] imageBytes = wc.DownloadData(new Uri(SpotifyAlbumURL));
                    //    FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png", FileMode.Create, System.IO.FileAccess.Write);
                    //    using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    pxbx_SavedSpotify.ImageLocation = SpotifyAlbumPath;// ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png";
                    databox.Rows[i].Cells[15].Value = pxbx_SavedSpotify.ImageLocation;
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
                        byte[] imageBytes = wc.DownloadData(new Uri(databox.Rows[i].Cells[14].Value.ToString()));
                        pB_ReadDLCs.Value++;
                        FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png", FileMode.Create, System.IO.FileAccess.Write);
                        using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                        pxbx_SavedSpotify.ImageLocation = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png";
                        databox.Rows[i].Cells[15].Value = pxbx_SavedSpotify.ImageLocation;
                        pB_ReadDLCs.Value++;
                    }
                    pB_ReadDLCs.Value++;
                }
                else pB_ReadDLCs.Value = 5;
            }
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void btn_DeleteAll_Click(object sender, EventArgs e)
        {
            var result1 = MessageBox.Show("Are you sure you want to DELETE Standardizations (&Spotify downloaded info)?", MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.Yes)
            {
                DeleteFromDB("Standardization", "DELETE * FROM Standardization", cnb, cnc);
                Populate(ref databox, ref Main);
                databox.EditingControlShowing += DataGridView1_EditingControlShowing;
                databox.Refresh();
            }
        }

        private void btn_GetSpotifyAll_Click(object sender, EventArgs e)
        {
            if (netstatus == "NOK" || netstatus == "") netstatus = CheckIfConnectedToInternet().Result.ToString();
            if (netstatus == "OK") netstatus = CheckIfConnectedToSpotify().Result.ToString();
            if (netstatus == "NOK" || netstatus == "") return;

            DataSet SongRecord = new DataSet(); SongRecord = SelectFromDB("Standardization", "SELECT IIF(Artist_Correction is null,Artist,Artist_Correction), IIF(Album_Correction is null,Album,Album_Correction), " +
                "ID FROM Standardization WHERE SpotifyArtistID = \"-\" OR SpotifyArtistID = \"\" OR SpotifyArtistID is null ORDER BY SpotifyArtistID ASC;", "", cnb, cnc);
            var noOfRec = GetNoRec(SongRecord, cnb, cnc);//.Tables[0].Rows.Count;
            //var vFilesMissingIssues = "";
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
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
                    //{"/* Spotify_Song_ID=\"" + SpotifySongID + "\", */+"
                    var cmds = "UPDATE Standardization SET SpotifyArtistID =\"" + SpotifyArtistID + "\",";
                    cmds += " SpotifyAlbumID=\"" + SpotifyAlbumID + "\"" + ", SpotifyAlbumURL=\"" + SpotifyAlbumURL + "\"" + ",SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\",Year_Correction=\"" + SpotifyAlbumYear + "\"";
                    cmds += " WHERE ID=" + SongRecord.Tables[0].Rows[i].ItemArray[2].ToString();
                    DataSet dis = new DataSet();
                    if (trackno > 0 && SpotifySongID != "" && SpotifySongID != "-") dis = UpdateDB("Standardization", cmds + ";", cnb, cnc);
                    timestamp = UpdateLog(timestamp, i + "/" + noOfRec + " Spotify details: " + trackno + " " + SpotifyAlbumPath, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "DLCManager", pB_ReadDLCs, null);
                    //ADD STADARDISATION UPDATE
                    //Updating the Standardization table
                    //DataSet dzs = new DataSet(); dzs = SelectFromDB("Standardization", "SELECT * FROM Standardization WHERE StrComp(Artist,\""
                    //    + info.SongInfo.Artist + "\", 0) = 0 AND StrComp(Album,\"" + info.SongInfo.Album + "\", 0) = 0;", ConfigRepository.Instance()["dlcm_DBFolder"], cnb, cnc);

                    //if (dzs.Tables[0].Rows.Count == 0)
                    //{
                    //var updcmd = "UPDATE Stadarization SET SpotifyArtistID=\"" + SpotifyArtistID + "\" , SpotifyAlbumID=\"" + SpotifyArtistID + "\", SpotifyAlbumURL=\""
                    //    + SpotifyAlbumURL + "\", SpotifyAlbumPath=\"" + SpotifyAlbumPath + "\" WHERE (Artist=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\" OR Artist_Correction=\""
                    //    + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\") AND (Artist=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\" OR Album_Correction=\"" + SongRecord.Tables[0].Rows[i].ItemArray[0].ToString() + "\")";

                    //UpdateDB("Standardization", updcmd + ";", cnb, cnc);
                    //}

                }
                catch (Exception ex) { var tust = "Spotify Error ..." + ex; UpdateLog(timestamp, tust, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }
            }

            //Get Album Covers of Album Covers that went missing
            //if (netstatus == "NOK" || netstatus == "") netstatus = ActivateSpotify_ClickAsync().Result.ToString();
            DataSet SongRecordC = new DataSet(); SongRecord = SelectFromDB("Standardization", "SELECT ID, Artist, Artist_Correction, Album, Album_Correction,SpotifyAlbumURL FROM Standardization WHERE SpotifyAlbumPath=\"\" AND SpotifyAlbumURL<>\"\" AND SpotifyAlbumURL != Null", "", cnb, cnc);

            var noOfRecC = GetNoRec(SongRecordC, cnb, cnc);//.Tables.Count == 0 ? 0 : SongRecordC.Tables[0].Rows.Count;
            //var vFilesMissingIssues = "";
            pB_ReadDLCs.Value = 0; pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.Maximum = noOfRec;

            //var MissingPSARC = false;
            for (var i = 0; i < noOfRec; i++)
            {
                var artist = SongRecordC.Tables[0].Rows[i].ItemArray[2].ToString() == "" ? SongRecordC.Tables[0].Rows[i].ItemArray[1].ToString() : SongRecordC.Tables[0].Rows[i].ItemArray[2].ToString();
                var album = SongRecordC.Tables[0].Rows[i].ItemArray[4].ToString() == "" ? SongRecordC.Tables[0].Rows[i].ItemArray[3].ToString() : SongRecordC.Tables[0].Rows[i].ItemArray[4].ToString();
                pB_ReadDLCs.Increment(1);
                using (WebClient wc = new WebClient())
                {
                    byte[] imageBytes = wc.DownloadData(new Uri(SongRecordC.Tables[0].Rows[i].ItemArray[5].ToString()));
                    pB_ReadDLCs.Value++;
                    FileStream file = new FileStream(ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png", FileMode.Create, System.IO.FileAccess.Write);
                    using (MemoryStream stream = new MemoryStream(imageBytes)) stream.WriteTo(file);
                    pxbx_SavedSpotify.ImageLocation = ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_albumCovers\\" + artist + " - " + album + ".png";
                    databox.Rows[i].Cells[15].Value = pxbx_SavedSpotify.ImageLocation;
                    pB_ReadDLCs.Value++;
                }
            }
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void databox_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) { SaveRecord(); SaveOK = false; }//  SaveOK = true; 
            else SaveOK = false;
        }

        private void databox_SelectionChanged(object sender, EventArgs e)
        {
            //if (txt_ID.Text != "") ChangeRow();
            var line = -1;
            if (databox.SelectedCells.Count > 0) line = databox.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        private void btn_CheckOnline_Click(object sender, EventArgs e)
        {
            i = databox.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + txt_Artist.Text + "+" + txt_Album.Text;
            StartProcesss(@link, null);
            //try
            //{
            //    Process process = Process.Start(@link);
            //}
            //catch (Exception ex)
            //{
            //    var tsst = "Error ..." + ex; UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //MessageBox.Show("Can't not open Song Folder in Exporer ! ");
            //}
        }

        private void cbx_Groups_DropDown(object sender, EventArgs e)
        {
            //populaet the Group  Dropdown
            DataSet ds = new DataSet(); ds = SelectFromDB("Groups", "SELECT DISTINCT Groupz FROM Groups WHERE Type=\"DLC\";", "", cnb, cnc);
            var norec = GetNoRec(ds, cnb, cnc);//ds.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (cbx_Groups.Items.Count > 0)
                {
                    cbx_Groups.DataSource = null;
                    for (int k = cbx_Groups.Items.Count - 1; k >= 0; --k)
                    {
                        if (!cbx_Groups.Items[k].ToString().Contains("--"))
                        {
                            cbx_Groups.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                cbx_Groups.DataSource = null;
                cbx_Groups.Items.Add("");
                for (int j = 0; j < norec; j++)
                {
                    var tem = ds.Tables[0].Rows[j].ItemArray[0].ToString();
                    cbx_Groups.Items.Add(tem);
                }
            }
        }

        private void cbx_Groups_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!updateAutoGroups)
            {
                updateAutoGroups = true; return;
            }

            i = databox.SelectedCells[0].RowIndex;

            //pB_ReadDLCs.Maximum = 3; pB_ReadDLCs.Value = 1; pB_ReadDLCs.Step = 1;
            var cmd1 = "UPDATE Standardization SET Artist_AutoGroup = \"" + cbx_Groups.Text + "\" WHERE (Artist=\"" + txt_Artist.Text + "\") OR (Artist_Correction=\"" + txt_Artist.Text + "\" AND Artist_Correction <> NULL AND Artist_Correction <> \"\") OR (Artist_Correction=\"" + txt_Artist_Correction.Text + "\" AND Artist_Correction <> NULL AND Artist_Correction <> \"\") OR (Artist=\"" + txt_Artist_Correction.Text + "\")";
            DataSet dus = UpdateDB("Standardization", cmd1 + ";", cnb, cnc);
            updateAutoGroups = false;
            //pB_ReadDLCs.Increment(1);
            //cmd1 = "UPDATE Main SET Artist_AutoGroup = \"" + cbx_Groups.Text + "\" WHERE Artist=\"" + txt_Artist.Text + "\" AND Album=\"" + txt_Album.Text + "\"";
            //DataSet dhj = UpdateDB("Main", cmd1 + ";", cnb, cnc);

            //probably here to make sure changes are saved
            //cnb.Close();
            //cnb = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Persist Security Info=False;Mode= Share Deny None;Data Source=" + ConfigRepository.Instance()["dlcm_DBFolder"]);

            Populate(ref databox, ref Main);

            databox.Rows[i].Selected = true;
            databox.CurrentCell = databox.Rows[i].Cells[0];
            //pB_ReadDLCs.Increment(1);

            //int i = 0;
            //if (databox.Rows.Count > 1 && databox.SelectedCells.Count > 0)
            //{
            //    var prev = databox.SelectedCells[0].RowIndex;
            //    if (databox.Rows.Count == prev + 2)
            //        if (prev == 0) return;
            //        else
            //        {
            //            int rowindex;
            //            DataGridViewRow row;
            //            i = databox.SelectedCells[0].RowIndex;
            //            rowindex = i;
            //            databox.Rows[rowindex - 1].Selected = true;
            //            databox.Rows[rowindex].Selected = false;
            //            row = databox.Rows[rowindex - 1];
            //        }
            //    else
            //    {
            //        int rowindex;
            //        DataGridViewRow row;
            //        i = databox.SelectedCells[0].RowIndex;
            //        rowindex = i;
            //        databox.Rows[rowindex + 1].Selected = true;
            //        databox.Rows[rowindex].Selected = false;
            //        row = databox.Rows[rowindex + 1];
            //    }
            //}
            //else if (databox.Rows.Count > 1)
            //{
            //    int rowindex = 1;
            //    DataGridViewRow row;
            //    databox.Rows[rowindex + 1].Selected = true;
            //    databox.Rows[rowindex].Selected = false;
            //    row = databox.Rows[rowindex + 1];
            //}
            //ref databox, ref Main
            //Main.ResetBindings(false);
            //Main.Dispose();
            //dssx.Tables["Standardization"].AcceptChanges();
            //Main.DataSource = dssx.Tables["Standardization"];
            //databox.DataSource = null;
            //databox.DataSource = Main;
            //databox.Refresh();
            //dssx.Dispose();
            //ChangeRow();
            //ChangeRow();
        }

        private void btn_ApplyCurrent_Click(object sender, EventArgs e)
        {
            string txt = DB_Path;
            string returned = GenericFunctions.OneTranslation_And_Correction(txt, pB_ReadDLCs, cnb, null, txt_Artist.Text, txt_Album.Text, txt_Year_Correction.Text, txt_AlbumArt_Correction.Text, txt_Artist_Correction.Text, txt_Album_Correction.Text, cnc);
            //refresh 
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void Pxbx_SavedSpotify_Click(object sender, EventArgs e)
        {

        }

        private void btn_ChangeCover_Click(object sender, EventArgs e)
        {

        }

        private void PB_ReadDLCs_Click(object sender, EventArgs e)
        {

        }

        private void Bbtn_ApplyYear_Click(object sender, EventArgs e)
        {
            GenericFunctions.MultiplyAndApplyYear(cnb, cnc);
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void Btn_ApplyDefault_Click(object sender, EventArgs e)
        {
            GenericFunctions.ApplyArtistAutoGroup(cnb, pB_ReadDLCs, null, cnc);
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void btn_Save_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
        }

        private void btn_GetSpotifyCover_Click(object sender, EventArgs e)
        {

        }

        private void MultiplyAndApplySpotify(object sender, EventArgs e)
        {
            GenericFunctions.MultiplyAndApplySpotify(cnb, cnc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenericFunctions.ApplyArtistAutoGroup(cnb, pB_ReadDLCs, null, cnc);
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void btn_RemoveDuplicates_Click(object sender, EventArgs e)
        {
            ManuallyRemoveDuplicates(cnb, cnc);
            Populate(ref databox, ref Main);
            databox.Refresh();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //cmb_Filter.Text = "";
            GoTocounter = 0;
            if (!SearchON)
            {
                if (chbx_AutoSave.Checked) SaveRecord();

                btn_GoTo.Enabled = true;
                btn_ChangeCover.Enabled = false;
                btn_Save.Enabled = false;

                txt_Artist_Correction.Text = "";
                txt_Artist.Text = "";
                txt_Album.Text = "";
                txt_Album_Correction.Text = "";

                SearchON = true;
            }
            else
               if (txt_Artist_Correction.Text != "" || txt_Album_Correction.Text != "")
                try
                {
                    btn_GoTo.Enabled = false; SaveOK = false;
                    SearchCmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + (txt_Artist_Correction.Text != "" ? "(Artist Like '%" + txt_Artist_Correction + "%' Or Artist_Correction Like '%" + txt_Artist_Correction + "%')" : "");
                    SearchCmd += " AND ";
                    SearchCmd += (txt_Album_Correction.Text != "" ? "(Album Like '%" + txt_Album_Correction.Text + "%' OR _Correction.Text Like '%" + txt_Album_Correction.Text + "%')" : "");
                    SearchCmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";
                    SearchCmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                    SearchCmd = SearchCmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
                    SearchCmd = SearchCmd.Replace("WHERE AND", "WHERE ");
                    SearchCmd = SearchCmd.Replace("AND ORDER BY ", "ORDER BY ");

                    Populate(ref databox, ref Main);
                    databox.Refresh(); SaveOK = true;
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                    MessageBox.Show(ex.Message + "Can't run Search ! " + SearchCmd);
                }
            else MessageBox.Show("Add a search criteria");
        }

        private void btn_GoTo_Click(object sender, EventArgs e)
        {
            Artist = ""; gotoS();
        }

        private void gotoS()
        {
            var i = 0;
            var cmd = "";
            if (Artist != "") cmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + (txt_Artist_Correction.Text != "" ? "(Artist Like '%" + txt_Artist_Correction + "%' Or Artist_Correction Like '%" + txt_Artist_Correction + "%')" : "");
            else cmd = SearchCmd.Substring(0, SearchCmd.IndexOf(" FROM")) + " FROM Main u WHERE " + "(Artist Like '%" + Artist + "%' Or Artist_Correction Like '%" + Artist + "%')";
            cmd += " AND ";
            cmd += (txt_Album_Correction.Text != "" ? "(Album Like '%" + txt_Album_Correction.Text + "%' OR _Correction.Text Like '%" + txt_Album_Correction.Text + "%')" : "");
            cmd += " ORDER BY " + c("dlcm_OrderOfFields") + " ;";
            cmd = SearchCmd.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            cmd = SearchCmd.Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND").Replace("AND AND", "AND");
            cmd = SearchCmd.Replace("WHERE AND", "WHERE ");
            cmd = SearchCmd.Replace("AND ORDER BY ", "ORDER BY ");

            DataSet dhxs = new DataSet(); dhxs = SelectFromDB("Main", cmd, "", cnb, cnc); var noOfRec = GetNoRec(dhxs, cnb, cnc);//dhxs.Tables[0].Rows.Count;
            DataSet dhs = new DataSet(); dhs = SelectFromDB("Main", SearchCmd, "", cnb, cnc); var noRec = GetNoRec(dhs, cnb, cnc);//dhs.Tables[0].Rows.Count;
            //var ID = 0;
            for (var j = GoTocounter; j <= noOfRec - 1; j++)
            {
                if (i != 0) break;
                for (var k = 0; k <= noRec - 1; k++)
                {
                    if (dhs.Tables[0].Rows[k].ItemArray[0].ToString() == dhxs.Tables[0].Rows[j].ItemArray[0].ToString())/*&& j == GoTocounter*/
                    {
                        i = k;
                        GoTocounter++;
                        break;
                    }
                }
            }
            if (i > 0 && databox.RowCount >= i)
            {
                databox.FirstDisplayedScrollingRowIndex = i;
                databox.Focus();
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Details not matching any Song in the DB", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_SearchReset_Click(object sender, EventArgs e)
        {
            SearchCmd = "Select * FROM Standardization;";
            GoTocounter = 0;
            btn_GoTo.Enabled = false;
            if (chbx_AutoSave.Checked) SaveRecord();
            SearchON = false;
        }

        private void txt_Artist_Correction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }

        private void txt_Album_Correction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchON)
                if (e.KeyChar == (char)Keys.Enter)
                    btn_Search.PerformClick();
        }

        private void btn_suspect_Click(object sender, EventArgs e)
        {

            //parse table
            //if %*Artist*% like %*artist*%
            //if %*album*%t like %*album*%

            var tsst = ""; var timestamp = DateTime.Now; var i = 0;

            DataSet dfz = UpdateDB("Standardization", "UPDATE Standardization SET suspect=\"\"", cnb, cnc);

            var cmd = "SELECT ID, Artist, Artist_Correction, Album, Album_Correction, Suspect, Suspect_Reason FROM Standardization;";

            dfz = new DataSet(); dfz = SelectFromDB("Standardization", cmd, "", cnb, cnc);
            var norecs = GetNoRec(dfz, cnb, cnc);//dfz.Tables.Count > 0 ? dfz.Tables[0].Rows.Count : 0;
            var tz = ""; pB_ReadDLCs.Maximum = norecs; pB_ReadDLCs.Step = 1; pB_ReadDLCs.Value = 0;
            if (norecs > 0)
                for (var k = 0; k < norecs; k++)
                {
                    pB_ReadDLCs.Increment(1);
                    var artistx = dfz.Tables[0].Rows[k].ItemArray[2].ToString() != "" ? dfz.Tables[0].Rows[k].ItemArray[2].ToString() : dfz.Tables[0].Rows[k].ItemArray[1].ToString();
                    var albumx = dfz.Tables[0].Rows[k].ItemArray[4].ToString() != "" ? dfz.Tables[0].Rows[k].ItemArray[4].ToString() : dfz.Tables[0].Rows[k].ItemArray[3].ToString();
                    for (var l = k + 1; l < norecs; l++)
                    {
                        var idd = dfz.Tables[0].Rows[l].ItemArray[0].ToString();
                        var artistt = dfz.Tables[0].Rows[l].ItemArray[2].ToString() != "" ? dfz.Tables[0].Rows[l].ItemArray[2].ToString() : dfz.Tables[0].Rows[l].ItemArray[1].ToString();
                        var albumt = dfz.Tables[0].Rows[l].ItemArray[4].ToString() != "" ? dfz.Tables[0].Rows[l].ItemArray[4].ToString() : dfz.Tables[0].Rows[l].ItemArray[3].ToString();


                        if (artistx != "" && albumx != "" && artistt != "" && albumt != "")
                            if (artistx.Length > 4 && albumx.Length > 4 && artistt.Length > 4 && albumt.Length > 4)
                            {
                                if (
                                    (artistx != artistt && (artistx.ToLower().Trim() == artistt.ToLower().Trim() || artistx.ToLower().Trim().Contains(artistt.ToLower().Trim()) || artistt.ToLower().Trim().Contains(artistx.ToLower().Trim())))
                                    ||
                                    (albumx != albumt && artistx.ToLower().Trim() == artistt.ToLower().Trim() && (albumx.ToLower().Trim() == albumt.ToLower().Trim() || albumx.ToLower().Trim().Contains(albumt.ToLower().Trim()) || albumt.ToLower().Trim().Contains(albumx.ToLower().Trim())))
                                   )
                                {
                                    tz += idd + ", ";
                                    i++;
                                    break;

                                }
                                else
                                {
                                    bool equal = String.Equals(albumt, albumx, StringComparison.InvariantCulture);
                                    if (equal && albumx.ToLower().Trim() != albumt.ToLower().Trim())
                                        cmd = idd;
                                }
                            }
                    }
                }
            else UpdateLog(timestamp, "no standardization to asses for supect as in need for correction" + cmd, false, c("dlcm_TempPath"), "", "", null, null);

            if (tz.Contains(",")) tz = tz.Substring(0, tz.Length - 2);
            if (tz != "") dfz = UpdateDB("Standardization", "UPDATE Standardization SET suspect=\"Yes\" WHERE ID IN (" + tz + ")", cnb, cnc);

            Populate(ref databox, ref Main);
            databox.Refresh();

            tsst = "Adding suspects :" + tz + "/" + i + " times."; UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
        }

        private void btn_StandCover_Click(object sender, EventArgs e)
        {
            //Apply Forced AlbumCoverDefaulting :)
            //pB_ReadDLCs.Maximum = maxtranslationprocesse; stage++; pB_ReadDLCs.Value = stage; tsst = stage + "/" + maxtranslationprocesse + " take first song in an album then make it DEfault for cover"; UpdateLog(DateTime.Now, tsst, false, c("dlcm_TempPath"), "", "", pB_ReadDLCs, rtxt_StatisticsOnReadDLCs);
            var afacd = ApplyForcedAlbumCoverDefaulting(cnb, cnc, pB_ReadDLCs, null);
        }

        //private void btn_GetSpotifyCover_Click(object sender, EventArgs e)
        //{

        //}
    }
}
