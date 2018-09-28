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
using System.IO; //file functions
using RocksmithToolkitGUI;
using RocksmithToolkitGUI.DLCManager;
using RocksmithToolkitLib.Extensions; //dds
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using System.Data.SqlClient;
using System.Net; //4ftp
using RocksmithToolkitLib;//config
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToolkitLib.XmlRepository;
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Cache : Form
    {
        public Cache(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete, OleDbConnection cnnb)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            AllowEncriptb = AllowEncript;
            AllowORIGDeleteb = AllowORIGDelete;
            cnb = cnnb;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        public bool SaveOK = false;
        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION = "CacheDB";
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string SearchCmd = "";
        public bool GroupChanged = false;
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        public OleDbConnection cnb;
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void Standardization_Load(object sender, EventArgs e)
        {
            //DataAccess da = new DataAccess();
            //MessageBox.Show("test0");
            SearchCmd = "SELECT * from Cache AS O";
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            chbx_Autosave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            txt_FTPPath.Text = chbx_PreSavedFTP.Text == "EU" ? ConfigRepository.Instance()["dlcm_FTP1"] : ConfigRepository.Instance()["dlcm_FTP2"];
            if (ConfigRepository.Instance()["dlcm_RemoveBassDD"] == "Yes") chbx_RemoveBassDD.Checked = true;
            else chbx_RemoveBassDD.Checked = false;
            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes")
            {
                txt_AudioPath.Visible = true;
                txt_AudioPreviewPath.Visible = true;
                txt_AlbumArtPath.Visible = true;
                txt_SongsHSANPath.Visible = true;
            }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (true) //(DataGridView1.CurrentCell.IsComboBoxCell())
            {
                if (DataGridView1.Columns[DataGridView1.CurrentCell.ColumnIndex].Name == "ContactsColumn")
                {
                    ComboBox cb = e.Control as ComboBox;
                    if (cb != null)
                    {
                        cb.SelectionChangeCommitted -= _SelectionChangeCommitted;
                        cb.SelectionChangeCommitted += _SelectionChangeCommitted;
                    }
                }
            }
        }

        private void _SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (CheckBox1.Checked)
            //{
            //    MessageBox.Show(((DataGridViewComboBoxEditingControl)sender).Text);
            //}
            //else
            //{
            //if (bsPositions.Current != null)
            //{
            //    Int32 Index = bsPositions.Find("ContactPosition", ((DataGridViewComboBoxEditingControl)sender).Text);

            //    if (Index != -1)
            //    {
            //        bsBadges.Position = Index;
            //        DataGridView1.CurrentRow.Cells[DataGridView1.Columns["BadgeColumn"].Index].Value =
            //            (
            //                (DataRowView)bsBadges.Current).Row.Field<string>("Badge");
            //    }
            //}
            //}
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MAYBE HERE CAN ACTIVATE THE INDIV CELLS
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // DB_Path = DB_Path + "\\AccessDB.accdb"; //DLCManager.txt_DBFolder.Text
            try
            {
                Process process = Process.Start(@DB_Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path);
            }
        }

        public void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = DataGridView1.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {
            int i;
            if (DataGridView1.SelectedCells.Count > 0)
            {
                i = DataGridView1.SelectedCells[0].RowIndex;
                txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
                txt_Identifier.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
                txt_Artist.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
                txt_ArtistSort.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
                txt_Album.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
                txt_Title.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
                txt_AlbumYear.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
                txt_Arrangements.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
                if (DataGridView1.Rows[i].Cells[8].Value.ToString() == "Yes") chbx_Removed.Checked = true;
                else if (DataGridView1.Rows[i].Cells[8].Value.ToString() == "No") chbx_Removed.Checked = false;
                txt_AlbumArtPath.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
                rtxt_Comments.Text = DataGridView1.Rows[i].Cells[10].Value.ToString();
                txt_PSARCName.Text = DataGridView1.Rows[i].Cells[11].Value.ToString();
                txt_SongsHSANPath.Text = DataGridView1.Rows[i].Cells[12].Value.ToString();
                txt_Platform.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
                if (DataGridView1.Rows[i].Cells[14].Value != null) txt_AudioPath.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
                if (DataGridView1.Rows[i].Cells[15].Value != null) txt_AudioPreviewPath.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
                if (DataGridView1.Rows[i].Cells[16].Value.ToString() == "Yes") chbx_Selected.Checked = true;
                else if (DataGridView1.Rows[i].Cells[16].Value.ToString() == "No") chbx_Selected.Checked = false;

                //Create Groups list Dropbox
                //DataSet ds = new DataSet();
                var norec = 0;
                //try
                //{
                //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //    {
                //        string SearchCmd = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\";";
                //        OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
                //        da.Fill(ds, "Main");
                DataSet ds = new DataSet(); ds = SelectFromDB("Main", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\";", "", cnb);
                norec = ds.Tables[0].Rows.Count;

                        if (norec > 0)
                        {
                            //remove items
                            if (chbx_Group.Items.Count > 0)
                            {
                                chbx_Group.DataSource = null;
                                for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                                {
                                    chbx_Group.Items.RemoveAt(k);
                                    chbx_AllGroups.Items.RemoveAt(k);
                                }
                            }
                            //add items
                            for (int j = 0; j < norec; j++)
                            {
                                chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                                chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                            }
                        }

                DataSet dds = new DataSet(); dds = SelectFromDB("Main", "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";", "", cnb);
                //DataSet dds = new DataSet();
                ////Create Groups list MultiCheckbox
                //using (OleDbConnection con = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //{
                //    string SearchCmds = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";";
                //    OleDbDataAdapter dfa = new OleDbDataAdapter(SearchCmds, con); //WHERE id=253
                //    dfa.Fill(dds, "Main");
                var nocrec = dds.Tables[0].Rows.Count;

                            if (nocrec > 0)
                                for (int l = 0; l < norec; l++)
                                    for (int j = 0; j < nocrec; j++)
                                    // if (ds.Tables[0].Rows[j][0].ToString() == ds.Tables[0].Rows[l][0].ToString())
                                    //  chbx_AllGroups.SetSelected(j, true);
                                    {
                                        int index = chbx_AllGroups.Items.IndexOf(ds.Tables[0].Rows[j][0].ToString());
                                        chbx_AllGroups.SetItemChecked(index, true);
                                    }
                            //(ds.Tables[0].Rows[l][0].ToString()); 

                    //    }
                    //}

                    //if (txt_Arrangements.Text != "") 
                    picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text;//.Replace(".dds", ".png");
                    if (chbx_Autosave.Checked) SaveOK = true;
                    else SaveOK = false;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the Retail DB", false, false);
                //    frm1.ShowDialog();
                //    return;
                //}
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        public void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();
            if (DataGridView1.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = DataGridView1.SelectedCells[0].RowIndex;
                //nud_RemoveSlide.Value = i;
                //DataGridView1.Rows[i].Cells[0].Value = txt_I.Text;
                //DataGridView1.Rows[i].Cells[1].Value = txt_Artist.Text;
                //DataGridView1.Rows[i].Cells[3].Value = txt_ArtistSort.Text;
                //DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
                if (chbx_Removed.Checked == true) DataGridView1.Rows[i].Cells[8].Value = "Yes";
                else DataGridView1.Rows[i].Cells[8].Value = "No";
                if (chbx_Selected.Checked == true) DataGridView1.Rows[i].Cells[16].Value = "Yes";
                else DataGridView1.Rows[i].Cells[16].Value = "No";
                DataGridView1.Rows[i].Cells[10].Value = rtxt_Comments.Text;

                //Save Groups
                if (GroupChanged)
                {
                    var cmdDel = "DELETE FROM Groups WHERE ";

                    //DataSet dsz = new DataSet();
                    //DataSet ddz = new DataSet();
                    for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                    {
                        //using (OleDbConnection cmb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                        //{
                        //    DataSet dooz = new DataSet();
                        //    string updatecmd = "SELECT ID FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\";";
                        //    OleDbDataAdapter dbf = new OleDbDataAdapter(updatecmd, cmb);
                        //    dbf.Fill(dooz, "Groups");
                        //    dbf.Dispose();
                        DataSet dooz = new DataSet(); dooz = SelectFromDB("Groups", "SELECT ID FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\";", "", cnb);
                        //var cmd = "INSERT INTO Groups(CDLC_ID,Groups,Type) VALUES";

                            var rr = dooz.Tables[0].Rows.Count;
                            if (chbx_AllGroups.GetItemChecked(j) && rr == 0)
                            {
                             //   cmd += "(\"" + txt_ID.Text + "\",\"" + chbx_AllGroups.Items[j] + "\",\"Retail\")";
                            var insertcmdd = "CDLC_ID, Groups, Type";
                            var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_AllGroups.Items[j] + "\",\"Retail\"";
                            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0);
                            //OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cmb);
                            //dab.Fill(dsz, "Groups");
                            //dab.Dispose();
                        }
                            else if (rr > 0 && !chbx_AllGroups.GetItemChecked(j)) cmdDel += "(Type=\"Retail\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\") OR ";
                        }
                    //}
                    cmdDel += ";";
                    cmdDel = cmdDel.Replace(" OR ;", ";");
                    //cmd += ";";
                    //cmd = cmd.Replace(",;", ";");
                    //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                    //{
                    //if (cmd != "INSERT INTO Groups(CDLC_ID,Groups) VALUES")
                    //{
                    //    OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                    //  dab.Fill(dsz, "Groups");
                    //    dab.Dispose();
                    //}
                    if (cmdDel != "DELETE FROM Groups WHERE ;") DeleteFromDB("Groups", cmdDel, cnb);                        //{
                        //    //OleDbDataAdapter dac = new OleDbDataAdapter(cmdDel, cnb);
                        //    //dac.Fill(ddz, "Groups");
                        //    //dac.Dispose();

                        //}
                    //}
                }
                //var DB_Path = "../../../../tmp\\AccessDB.accdb;";
                var connection = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    //OleDbCommand command = new OleDbCommand();
                    //Update StadardizationDB
                    //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                    command.CommandText = "UPDATE Cache SET ";
                    command.CommandText += "Removed = @param8, ";
                    command.CommandText += "Selected = @param9, ";
                    command.CommandText += "Comments = @param10 ";
                    //command.CommandText += "AlbumArtPath_Correction = @param6 ";
                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[16].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param10", DataGridView1.Rows[i].Cells[10].Value.ToString() ?? DBNull.Value.ToString());
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);

                        throw;
                    }
                    finally
                    {
                        if (connection != null) connection.Close();
                    }
                    ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                    //MessageBox.Show("Default Menu song Availability Saved");
                    //das.SelectCommand.CommandText = "SELECT * FROM Tones";
                    //// das.Update(dssx, "Tones");
                }
                GroupChanged = false;
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {

            SearchCmd = SearchCmd.Replace("WHERE  ORDER", "ORDER");
             dssx = SelectFromDB("Cache", SearchCmd, "", cnb);
            //DB_Path = "../../../../tmp\\AccessDB.accdb;";
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cn);
            //    da.Fill(dssx, "Cache");
            //    dssx.Dispose();
            //    //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //    //da.Fill(ds, "PositionType");
            //    //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //    //da.Fill(ds, "Badge");
            //}
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 50 };
            DataGridViewTextBoxColumn Identifier = new DataGridViewTextBoxColumn { DataPropertyName = "Identifier", HeaderText = "Identifier ", Width = 70 };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist ", Width = 155 };
            DataGridViewTextBoxColumn ArtistSort = new DataGridViewTextBoxColumn { DataPropertyName = "ArtistSort", HeaderText = "ArtistSort ", Width = 155 };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album ", Width = 175 };
            DataGridViewTextBoxColumn Title = new DataGridViewTextBoxColumn { DataPropertyName = "Title", HeaderText = "Title ", Width = 175 };
            DataGridViewTextBoxColumn AlbumYear = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumYear", HeaderText = "AlbumYear ", Width = 50 };
            DataGridViewTextBoxColumn Arrangements = new DataGridViewTextBoxColumn { DataPropertyName = "Arrangements", HeaderText = "Arrangements ", Width = 155 };
            DataGridViewTextBoxColumn Removed = new DataGridViewTextBoxColumn { DataPropertyName = "Removed", HeaderText = "Removed ", Width = 50 };
            DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath ", Width = 55 };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments ", Width = 495 };
            DataGridViewTextBoxColumn PSARCName = new DataGridViewTextBoxColumn { DataPropertyName = "PSARCName", HeaderText = "PSARCName ", Width = 195 };
            DataGridViewTextBoxColumn SongsHSANPath = new DataGridViewTextBoxColumn { DataPropertyName = "SongsHSANPath", HeaderText = "SongsHSANPath ", Width = 295 };
            DataGridViewTextBoxColumn Platform = new DataGridViewTextBoxColumn { DataPropertyName = "Platform", HeaderText = "Platform ", Width = 40 };
            DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath ", Width = 295 };
            DataGridViewTextBoxColumn AudioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPreviewPath", HeaderText = "AudioPreviewPath ", Width = 295 };
            DataGridViewTextBoxColumn Selected = new DataGridViewTextBoxColumn { DataPropertyName = "Selected", HeaderText = "Selected ", Width = 25 };

            //bsPositions.DataSource = ds.Tables["Tones"];
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

            //DataGridView.AutoGenerateColumns = false;

            //DataGridView.Columns.AddRange(new DataGridViewColumn[]
            //{
            //    ID,
            //    Identifier,
            //    Artist,
            //    ArtistSort,
            //    Album,
            //    Title,
            //    AlbumYear,
            //    Arrangements,
            //    Removed,
            //    AlbumArtPath,
            //    Comments,
            //    PSARCName,
            //    SongsHSANPath,
            //    Platform,
            //    AudioPath,
            //    AudioPreviewPath
            //Selected
            //}
            //);

            dssx.Tables["Cache"].AcceptChanges();
            var noOfRec = dssx.Tables[0].Rows.Count;
            bs.DataSource = dssx.Tables["Cache"];
            DataGridView.DataSource = bs;
            dssx.Dispose();
            DataSet dooz = new DataSet(); dooz = SelectFromDB("Groups", "SELECT * from Cache AS O WHERE Removed=\"No\"", "", cnb);
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Cache AS O WHERE Removed=\"No\"", cn);
            //    da.Fill(dssx, "Cache");
            //    dssx.Dispose();
            //    //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //    //da.Fill(ds, "PositionType");
            //    //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //    //da.Fill(ds, "Badge");
            //}
            lbl_NoRec.Text = noOfRec.ToString() + "/" + (dooz.Tables[0].Rows.Count - noOfRec).ToString() + " records.";

            //advance or step back in the song list
            int i = 0;
            if (DataGridView.Rows.Count > 1)
            {
                var prev = DataGridView.SelectedCells[0].RowIndex;
                if (DataGridView.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        i = DataGridView.SelectedCells[0].RowIndex;
                        rowindex = i;
                        DataGridView.Rows[rowindex - 1].Selected = true;
                        DataGridView.Rows[rowindex].Selected = false;
                        row = DataGridView.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    i = DataGridView.SelectedCells[0].RowIndex;
                    rowindex = i;
                    DataGridView.Rows[rowindex + 1].Selected = true;
                    DataGridView.Rows[rowindex].Selected = false;
                    row = DataGridView.Rows[rowindex + 1];
                }
            }
            ChangeRow();
            //DataGridView.ExpandColumns();
        }

        private class CacheRecs
        {
            public string ID { get; set; }
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
        }

        private CacheRecs[] files = new CacheRecs[10000];

        //Generic procedure to read and parse Cache.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            ////rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            //try
            //{
                MessageBox.Show(DB_Path);
                DataSet dus = new DataSet(); dus = SelectFromDB("Groups", cmd, "", cnb);
                //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + DB_Path))
                //{
                //    DataSet dus = new DataSet();
                //    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                //    dax.Fill(dus, "Cache");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    if (MaximumSize > 0)
                        foreach (DataRow dataRow in dus.Tables[0].Rows)
                        {
                            files[i] = new CacheRecs();

                            //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                            files[i].ID = dataRow.ItemArray[0].ToString();
                            files[i].Identifier = dataRow.ItemArray[1].ToString();
                            files[i].Artist = dataRow.ItemArray[2].ToString();
                            files[i].ArtistSort = dataRow.ItemArray[3].ToString();
                            files[i].Album = dataRow.ItemArray[4].ToString();
                            files[i].Title = dataRow.ItemArray[5].ToString();
                            files[i].AlbumYear = dataRow.ItemArray[6].ToString();
                            files[i].Arrangements = dataRow.ItemArray[7].ToString();
                            files[i].Removed = dataRow.ItemArray[8].ToString();
                            files[i].AlbumArtPath = dataRow.ItemArray[9].ToString();
                            files[i].Comments = dataRow.ItemArray[10].ToString();
                            files[i].PSARCName = dataRow.ItemArray[11].ToString();
                            files[i].SongsHSANPath = dataRow.ItemArray[12].ToString();
                            files[i].Platform = dataRow.ItemArray[13].ToString();
                            files[i].AudioPath = dataRow.ItemArray[14].ToString();
                            files[i].AudioPreviewPath = dataRow.ItemArray[15].ToString();
                            files[i].Selected = dataRow.ItemArray[16].ToString();
                            i++;
                        }
            //        //Closing Connection
            //        dax.Dispose();
            //        cnn.Close();
            //        //rtxt_StatisticsOnReadDLCs.Text += i;
            //        //var ex = 0;
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Cache DB connection ! ");
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_Autosave.Checked) SaveRecord();
            chbx_Autosave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(DB_Path, TempPath, false, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb, cnb);//.Replace("\\AccessDB.accdb", "")
            frm.Show();
        }

        private void btn_GenerateHSAN_Click(object sender, EventArgs e)
        {
            generatehsan();
        }
        public void generatehsan()
        {
            DataSet drsx = new DataSet(); drsx = SelectFromDB("Cache", "SELECT DISTINCT SongsHSANPath, PSARCName, Platform from Cache AS O;", "", cnb);
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT DISTINCT SongsHSANPath, PSARCName, Platform from Cache AS O;", cn);
            //    da.Fill(drsx, "Cache");
            pB_ReadDLCs.Value = 0;
                if (drsx.Tables[0].Rows.Count != 0)
                    pB_ReadDLCs.Maximum = 2 * drsx.Tables[0].Rows.Count;
                foreach (DataRow dataRow in drsx.Tables[0].Rows)
                {
                    pB_ReadDLCs.Increment(2);
                    var dpsarc = dataRow.ItemArray[1].ToString();
                    var platfor = dataRow.ItemArray[2].ToString();
                    var HSAN = dataRow.ItemArray[0].ToString();
                    manipulateHSAN(HSAN);
                    if (dpsarc.ToString() == "CACHE")
                    {
                        ////Remove Bass DD
                        //if (chbx_RemoveBassDD.Checked) //REMOVE Bass DD
                        //{
                        //    var source_dir1 = Path.GetDirectoryName(TempPath + "\\0_dlcpacks\\songs_" + platfor + "\\manifests\\");
                        //    var destination_dir1 = Path.GetDirectoryName(TempPath + "\\0_dlcpacks\\songs_" + platfor + "\\songs\\bin\\"); ;
                        //    var previewN = "";
                        //    foreach (string preview_name in Directory.GetFiles(source_dir1, "*_bass.xml", System.IO.SearchOption.AllDirectories))
                        //    {

                        //        //foreach (string file_name in Directory.GetFiles(source_dir1, "*.sng", System.IO.SearchOption.AllDirectories))
                        //        //{
                        //        //}
                        //    }
                        //}


                        //Compress Hsan into cache.xxx and its cache7.7z component
                        DirectoryInfo di;
                        var startI = new ProcessStartInfo();
                        var hsanDir = dataRow.ItemArray[0].ToString();
                        startI.FileName = Path.Combine(AppWD, "..\\..\\7za.exe");
                        startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\";// Path.GetDirectoryName();
                        var za = TempPath + "\\0_dlcpacks\\cache_" + platfor + "\\cache7.7z";
                        if (!Directory.Exists(TempPath + "\\0_dlcpacks\\manifests")) di = Directory.CreateDirectory(TempPath + "\\0_dlcpacks\\manifests");
                        if (!Directory.Exists(TempPath + "\\0_dlcpacks\\manifests\\songs")) di = Directory.CreateDirectory(TempPath + "\\0_dlcpacks\\manifests\\songs");
                        File.Copy(hsanDir, TempPath + "\\0_dlcpacks\\manifests\\songs\\songs.hsan", true);

                        startI.Arguments = String.Format(" a {0} {1}",
                                                            za,
                                                            "manifests\\songs\\songs.hsan");// + platformDLCP TempPath + "\\0_dlcpacks\\cache_pc\\
                        startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }

                        //compress cache.xxx
                        var startInfo = new ProcessStartInfo();
                        var unpackedDir = "";
                        if (chbx_Songs2Cache.Checked) unpackedDir = TempPath + "\\0_dlcpacks\\cache_" + platfor;//((platfor == "PS3") ? "_ps3" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""));
                        else unpackedDir = TempPath + "\\0_dlcpacks\\songs_" + platfor;
                        startInfo.FileName = Path.Combine(AppWD, "psarc.exe");
                        startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                        var t = TempPath + "\\0_dlcpacks\\manipulated\\cache_" + platfor + ".psarc";
                        startInfo.Arguments = String.Format(" create -y --lzma -S -N -o {0} -i {1}",
                                                            t,
                                                            unpackedDir);// + platformDLCP
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                        //rtxt_Comments.Text = startInfo.FileName + " "+startInfo.Arguments;
                        //if (!File.Exists(t))
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        //manual workaround for wrongly packing the files
                        MessageBox.Show("For CACHE.psarcC-RS2014 as the Toolkit cannot pack with \"NO compression\" and PSARC.EXE(2011 version) cannot pack correctly,\n a manual workaround exists:\n1. Download&install TotalCommander http://ghisler.com/download.htm \n2. Download the psarc plugin 2013 http://www.totalcmd.net/plugring/PSARC.html \n3. While in TC open the zip archive with the plugin&install the plugin\n\n4. Enter the manipulated/cache.psarc and Pack with External No Compression LZMA\n(take out the _PS3/_Pc/_Mac from the name\n5. Copy in the Game(not DLC) DIR", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                        //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                        //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                    }

                    if (dpsarc.ToString() == "RS1Retail")
                    {
                        var startInfo = new ProcessStartInfo();
                        var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));//unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                        startInfo.FileName = Path.Combine(AppWD, "..\\..\\packer.exe");
                        startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                        var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + ((platfor == "PS3") ? "" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""))) + ".psarc";

                        //Copy temp dir for manipulations and packing
                        try
                        {
                            string source_dir = @unpackedDir;
                            string destination_dir = @unpackedDir.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\temp\\");

                            // substring is to remove destination_dir absolute path (E:\).

                            // Create subdirectory structure in destination    
                            foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                            {
                                Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                            }

                            foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                if (!(AllowORIGDeleteb && (file_name.IndexOf(".ogg") > 0 || (file_name.IndexOf(".orig") > 0))))
                                    File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                            }
                            //Directory.Delete(source_dir, true);                            
                            unpackedDir = destination_dir;

                            //Remove Bass DD
                            if (chbx_RemoveBassDD.Checked) //REMOVE Bass DD
                            {
                                var platforms = unpackedDir.GetPlatform();
                                foreach (string xmln in Directory.GetFiles(unpackedDir + "\\songs\\arr\\", "*_bass.xml", System.IO.SearchOption.AllDirectories))
                                {
                                    {
                                        var bassRemoved = (RemoveDD(unpackedDir, "Yes", xmln, unpackedDir.GetPlatform(), false, false, "No") == "Yes") ? "Yes" : "No";
                                    }
                                }
                            }
                        }
                        catch (Exception ee)
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                            Console.WriteLine(ee.Message);
                        }

                        startInfo.Arguments = String.Format(" --pack --version=RS2014 --platform={0} --output={1} --input={2}",
                                                            platfor,
                                                            t,
                                                            unpackedDir);// + platformDLCP
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                        //pack
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                            //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }

                        //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                        if (platfor == "PS3" && AllowEncriptb)
                        {
                            var startI = new ProcessStartInfo();
                            var hsanDir = dataRow.ItemArray[0].ToString();
                            startI.FileName = Path.Combine(AppWD, "edattool.exe");
                            startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\manipulated\\";// Path.GetDirectoryName();
                            var za = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc.psarc.edat";
                            var zi = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc.psarc";
                            startI.Arguments = String.Format(" encrypt -custom:CB4A06E85378CED307E63EFD1084C19D UP0001-BLUS31182_00-ROCKSMITHPATCH01 00 00 00 {0} {1}",
                                                                zi,
                                                                za);
                            startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }

                    }

                    if (dpsarc.ToString() == "COMPATIBILITY")
                    {
                        var startInfo = new ProcessStartInfo();
                        var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));// var unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydlc_PS3";
                        //startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");//not working
                        startInfo.FileName = Path.Combine(AppWD, "..\\..\\packer.exe");
                        startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                        //var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc";
                        var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc" + ((platfor == "PS3") ? "" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""))) + ".psarc";


                        try //Copy dir
                        {
                            string source_dir = @unpackedDir;
                            string destination_dir = @unpackedDir.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\temp\\");

                            // substring is to remove destination_dir absolute path (E:\).

                            // Create subdirectory structure in destination    
                            foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                            {
                                Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                            }

                            foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                            {
                                if (!(AllowORIGDeleteb && (file_name.IndexOf(".ogg") > 0 || (file_name.IndexOf(".orig") > 0))))
                                    File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                            }
                            unpackedDir = destination_dir;
                            //Directory.Delete(source_dir, true); DONT DELETE

                            //Remove Bass DD
                            if (chbx_RemoveBassDD.Checked) //REMOVE Bass DD
                            {
                                var platforms = unpackedDir.GetPlatform();
                                foreach (string xmln in Directory.GetFiles(unpackedDir + "\\songs\\arr\\", "*_bass.xml", System.IO.SearchOption.AllDirectories))
                                {
                                    {
                                        var bassRemoved = (RemoveDD(unpackedDir, "Yes", xmln, unpackedDir.GetPlatform(), false, false, "No") == "Yes") ? "Yes" : "No";
                                    }
                                }
                            }
                            unpackedDir = destination_dir;
                        }
                        catch (Exception ee)
                        {
                            //rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                            Console.WriteLine(ee.Message);
                        }


                        //if (AllowORIGDeleteb) File.Delete(TempPath + "\\0_dlcpacks\\rs1compatibilitydlc_PS3\\manifests\\songs_rs1dlc\\songs_rs1dlc.hsan.orig");
                        //if (AllowORIGDeleteb) if (File.Exists(unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan.orig")) File.Delete(unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan.orig");

                        //startInfo.Arguments = String.Format(" --pack --version=RS2014 --platform=PS3 --input --zlib --level=4 -o {0} -i {1}",//not working
                        startInfo.Arguments = String.Format(" --pack --version=RS2014 --platform={0} --output={1} --input={2}",
                                                            platfor,
                                                            t,
                                                            unpackedDir);// + platformDLCP
                        startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                        //if (!File.Exists(t))
                        using (var DDC = new Process())
                        {
                            DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 90 * 1); //wait 1min
                            //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }

                        //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                        if (platfor == "PS3" && AllowEncriptb)
                        {
                            var startI = new ProcessStartInfo();
                            var hsanDir = dataRow.ItemArray[0].ToString();
                            startI.FileName = Path.Combine(AppWD, "edattool.exe");
                            startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\manipulated\\";// Path.GetDirectoryName();
                            var za = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc.edat";
                            var zi = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc";

                            startI.Arguments = String.Format(" encrypt -custom:CB4A06E85378CED307E63EFD1084C19D UP0001-BLUS31182_00-ROCKSMITHPATCH01 00 00 00 {0} {1}",
                                                                zi,
                                                                za);
                            startI.UseShellExecute = false; startI.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                        }
                    }
                }
          //  }
            MessageBox.Show("Songs Removed from the Shipped version of the game");
        }


        public void manipulateHSAN(string hsanPath)
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
            //var cmd = "";
            line = fxml.ReadLine(); line = fxml.ReadLine();
            //Read and Save Header
            while ((line = fxml.ReadLine()) != null)
            {
                if (line.Contains("InsertRoot")) break; //got to the end so

                if (line.Contains("\"DLCRS1Key\" : [")) linedone = false;
                if (line.Contains("],")) linedone = true;
                //textfile += line;//if (header == "done") line.Contains("{") &&  && !linedone
                if (line.Contains("\"SongKey\" : \""))
                {
                    songkey = "";
                    songkey = ((line.Trim().ToLower()).Replace("\"songkey\" : \"", "").Replace("\"", "")).Replace(",", "");

                    DataSet disx = new DataSet(); disx = SelectFromDB("Groups", "SELECT Removed from Cache AS O WHERE LCASE(Identifier)=\"" + songkey + "\"", "", cnb);
                    //cmd = "SELECT Removed from Cache AS O WHERE LCASE(Identifier)=\"" + songkey + "\"";
                    //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                    //{
                    //    DataSet disx = new DataSet();
                    //    OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
                    //    da.Fill(disx, "Cache");
                    //rtxt_StatisticsOnReadDLCs.Text = "Processing: " + dssx.Tables[0].Rows.Count + " " + songkey + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    var rec = disx.Tables[0].Rows.Count;
                    if (rec> 0) IDD = disx.Tables[0].Rows[0].ItemArray[0].ToString();
                        else
                        //{
                            IDD = "No";
                            //rtxt_StatisticsOnReadDLCs.Text = "Removing: " + songkey + " " + dssx.Tables[0].Rows.Count  + " " + cmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        //}
                    //    disx.Dispose();
                    //}
                }

                lastline = false;
                if (line.Contains("},") && linedone)//&& header == "done"
                {
                    if (IDD == "No") { textfile += tecst + "\n" + line; lastline = true; }
                    IDD = "";
                    tecst = "";
                    linedone = true;
                }
                else tecst += "\n" + line;
            }
            if (lastline) textfile += "\n" + "    \"InsertRoot\" : \"Static.Songs.Headers\"";
            else textfile = (textfile + "},").Replace("}, },", "}\n         },") + "\n" + "    \"InsertRoot\" : \"Static.Songs.Headers\"";

            textfile += "\n" + "}";
            fxml.Close();
            File.WriteAllText(inputFilePath, textfile);
            //rtxt_StatisticsOnReadDLCs.Text = "Songs Removed from the Shipped version of the game" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
        }


        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_Autosave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void btn_InvertAll_Click(object sender, EventArgs e)
        {
            try
            {
                var inputFilePath = RocksmithDLCPath + "\\songs.hsan";
                if (!File.Exists(inputFilePath + ".Yes"))
                {
                    File.Copy(inputFilePath, inputFilePath + ".Yes", true);
                    File.Copy(inputFilePath + ".orig", inputFilePath, true);
                }
                else
                {
                    File.Copy(inputFilePath, inputFilePath + ".No", true);
                    File.Copy(inputFilePath + ".Yes", inputFilePath, true);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                MessageBox.Show("Error at set to original " + ee);
            }
            var connection = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            {
                command.CommandText = "UPDATE Cache SET ";
                command.CommandText += "Removed = @param8, Selected=@param9 WHERE Removed='Yes' ";
                command.Parameters.AddWithValue("@param8", "Maybe");
                command.Parameters.AddWithValue("@param9", "Maybe");
                try
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    command.Dispose(); command = connection.CreateCommand();
                    command.CommandText = "UPDATE Cache SET ";
                    command.CommandText += "Removed = @param18, Selected=@param19 WHERE Removed='No' ";
                    command.Parameters.AddWithValue("@param18", "Yes");
                    command.Parameters.AddWithValue("@param19", "No");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        //connection.Close();
                        command.Dispose(); command = connection.CreateCommand();
                        command.CommandText = "UPDATE Cache SET ";
                        command.CommandText += "Removed = @param38, Selected = @param39 WHERE Removed='Maybe' ";
                        command.Parameters.AddWithValue("@param38", "No");
                        command.Parameters.AddWithValue("@param38", "Yes");
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                            //connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
                            throw;
                        }
                        finally
                        {
                            if (connection != null) connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
                        throw;
                    }
                    finally
                    {
                        if (connection != null) connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
                    throw;
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
                MessageBox.Show("DB Removed values have been inversed ;) Enjoy!");
            }
        }

        private void btn_FTP_Click(object sender, EventArgs e)
        {
            if (cbx_Format.Text == "PS3")
            {
                var GameID = txt_FTPPath.Text.Substring(txt_FTPPath.Text.LastIndexOf("BL"), 9);
                var startno = txt_FTPPath.Text.LastIndexOf("GAMES/");
                var endno = (txt_FTPPath.Text.LastIndexOf("BL")) + 9;
                var GameName = ((txt_FTPPath.Text).Substring(startno, endno - startno)).Replace("GAMES/", "");
                var newpath = txt_FTPPath.Text.Replace("GAMES", "game").Replace("PS3_GAME", GameID).Replace(GameName + "/", "");
                FTPFile(newpath, "rs1compatibilitydlc.psarc.edat");
                FTPFile(newpath + "DLC/", "rs1compatibilitydisc.psarc.edat");
                FTPFile(txt_FTPPath.Text, "cache_PS3.psarc");
                MessageBox.Show("FTPed");
            }
            else if (cbx_Format.Text == "PC" || cbx_Format.Text == "Mac")
            {
                var platfrm = (cbx_Format.Text == "PC" ? "_p" : (cbx_Format.Text == "Mac" ? "_m" : ""));
                var dest = "";
                if (RocksmithDLCPath.IndexOf("Rocksmith2014\\DLC") > 0)
                {
                    dest = RocksmithDLCPath;//
                    File.Copy(RocksmithDLCPath + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "rs1compatibilitydlc" + platfrm + ".psarc", true);

                    File.Copy(dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", true);

                    dest = RocksmithDLCPath.Replace("\\DLC", "");
                    File.Copy(dest + "\\cache.psarc", dest + "\\cache.psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\cache_" + cbx_Format.Text + ".psarc", dest + "\\cache.psarc", true);
                }
                else if (RocksmithDLCPath != txt_FTPPath.Text)
                {
                    dest = txt_FTPPath.Text;//!File.Exists(
                    File.Copy(dest + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc" + platfrm + ".psarc" + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest, true);

                    File.Copy(dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", true);

                    File.Copy(dest + "\\cache.psarc", dest + "\\cache.psarc.orig", false);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\cache" + cbx_Format.Text + ".psarc", dest + "\\cache.psarc", true);
                }
                else MessageBox.Show("Chose a different path to save");
            }
        }

        public void FTPFile(string filel, string filen)
        {
            // Get the object used to communicate with the server.
            var ddd = filel + filen;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ddd);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            //// Copy the contents of the file to the request stream.
            //StreamReader sourceStream = new StreamReader("c:\\GitHub\\tmp\\0\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc.edat");
            //byte[] fileContents =Encoding.UTF16.GetBytes(sourceStream.ReadToEnd());

            //sourceStream.Close();
            //request.ContentLength = fileContents.Length;

            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(fileContents, 0, fileContents.Length);
            //requestStream.Close();

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            byte[] b = File.ReadAllBytes(TempPath + "\\0_dlcpacks\\manipulated\\" + filen);

            request.ContentLength = b.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(b, 0, b.Length);
            }

            FtpWebResponse ftpResp = (FtpWebResponse)request.GetResponse();


            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            //response.Close();

        }

        private void chbx_FTP2_CheckedChanged(object sender, EventArgs e)
        {
            // if (chbx_FTP2.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 FAV - BLES01862/PS3_GAME/USRDIR/";
        }

        private void chbx_FTP1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbx_FTP1.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 ALL DLC - BLUS31182/PS3_GAME/USRDIR/";
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    using (var fbd = new VistaFolderBrowserDialog())
        //    {
        //        if (fbd.ShowDialog() != DialogResult.OK)
        //            return;
        //        var temppath = fbd.SelectedPath;
        //        txt_VLCPath.Text = temppath;
        //        //-Save the location in the Config file/reg
        //        //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
        //    }
        //}

        //private void chbx_VLCHome_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbx_VLCHome.Checked) txt_VLCPath.Text = "DLCManager\\VLCPortable.exe";
        //}

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {   //alternative impl could use http://nvorbis.codeplex.com/documentation
            //txt_VLCPath.Text = "DLCManager\\VLCPortable.exe" + " ";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_AudioPreviewPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p {0}",
                                                t);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }

            //try
            //{
            //    Process process = Process.Start(@DB_Path);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not Play the oggin Retails songs Edit screen ! " + DB_Path);
            //}
        }

        private void btn_OpeHSAN_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(txt_SongsHSANPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path);
            }
        }

        private void btn_PlayAudio_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec2.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_AudioPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p {0}", t);
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        private void btn_ExpandSelCrossP_Click(object sender, EventArgs e)
        {
            var Pltfrm = txt_Platform.Text;
            if (Pltfrm == "Platform") MessageBox.Show("Select 1 respresentative of THE desire Source plafrom");
            else
            {
                var cmd = "SELECT Identifier, Removed,Selected, Comments FROM Cache AS O WHERE Platform=\"" + Pltfrm + "\"";
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    DataSet disx = new DataSet();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
                    da.Fill(disx, "Cache");



                    var connection = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                    var command = connection.CreateCommand();
                    pB_ReadDLCs.Value = 0;

                    foreach (DataRow dataRow in disx.Tables[0].Rows)
                    {
                        pB_ReadDLCs.Maximum = 2 * disx.Tables[0].Rows.Count;
                        pB_ReadDLCs.Increment(1);
                        var iden = dataRow.ItemArray[0].ToString();
                        var remov = dataRow.ItemArray[1].ToString();
                        var sele = dataRow.ItemArray[2].ToString();
                        var comm = dataRow.ItemArray[3].ToString();
                        ////dssx = DataGridView1;
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                        {
                            command.CommandText = "UPDATE Cache SET ";
                            command.CommandText += "Removed = @param8, Selected = @param9, Comments = @param10 WHERE Identifier=\"" + iden + "\" ";
                            command.Parameters.AddWithValue("@param8", remov);
                            command.Parameters.AddWithValue("@param9", sele);
                            command.Parameters.AddWithValue("@param10", comm);
                            try
                            {
                                command.CommandType = CommandType.Text;
                                connection.Open();
                                command.ExecuteNonQuery();
                                //connection.Close();
                                command.Dispose(); command = connection.CreateCommand();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
                                throw;
                            }
                            finally
                            {
                                //if (connection != null) connection.Close();
                            }
                        }
                    }
                    MessageBox.Show("Current Selected Platform REMOVED setting have been spread along the other Loaded platforms ;) Enjoy!");
                }
            }
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
        }

        private void cbx_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Format.Text == "PS3")
            {
                chbx_PreSavedFTP.Enabled = true;
                //chbx_FTP2.Enabled = true;
            }
            else
            {
                chbx_PreSavedFTP.Enabled = false;
                //chbx_FTP2.Enabled=false;
            }
        }

        private void btn_SteamDLCFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_FTPPath.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
            }
        }

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (txt_Album.Text != "") ChangeRow();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //int halfWay = (DataGridView1.DisplayedRowCount(false) / 2);
            //if (DataGridView1.FirstDisplayedScrollingRowIndex + halfWay > DataGridView1.SelectedRows[0].Index ||
            //    (DataGridView1.FirstDisplayedScrollingRowIndex + DataGridView1.DisplayedRowCount(false) - halfWay) <= DataGridView1.SelectedRows[0].Index)
            //{
            //    int targetRow = DataGridView1.SelectedRows[0].Index;
            //    targetRow = Math.Max(targetRow - halfWay, 0);
            //            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.SelectedRows+1;
            //}
            //if (DataGridView1.CurrentCell.ReadOnly)
            //    DataGridView1.CurrentCell = GetNextCell(DataGridView1.CurrentCell);


            // now get the testinfo at the gridcoordinate
            //DataGridView.HitTestInfo hti = 10; //grdJobs.HitTest(pt.X, pt.Y);

            // is it a cell?
            //if (hti.Type == DataGridViewHitTestType.Cell)
            //{
            // make the cell the current on and select the row
            //DataGridView1.ClearSelection();
            //DataGridView1.CurrentCell = DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex+1].Cells[1];
            //DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex+1].Selected = true;

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex + 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            row = DataGridView1.Rows[rowindex + 1];

            // do whatever you want to do, set contextmenu text for example

        }
        private DataGridViewCell GetNextCell(DataGridViewCell currentCell)
        {            //DataGridView1.
            int i = 0; //DataGridViewCell currentCell = DataGridView1.CurrentCell;
            DataGridViewCell nextCell = currentCell;

            do
            {
                int nextCellIndex = (nextCell.ColumnIndex + 1) % DataGridView1.ColumnCount;
                int nextRowIndex = nextCellIndex == 0 ? (nextCell.RowIndex + 1) % DataGridView1.RowCount : nextCell.RowIndex;
                nextCell = DataGridView1.Rows[nextRowIndex].Cells[nextCellIndex];
                i++;
            } while (i < DataGridView1.RowCount * DataGridView1.ColumnCount && nextCell.ReadOnly);

            return nextCell;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;

            //SaveRecord();// ChangeRow();
            //int rowindex;
            //DataGridViewRow row;
            //var i = DataGridView1.SelectedCells[0].RowIndex;
            //rowindex = i;// DataGridView1.SelectedRows[0].Index;
            ////DataGridView1.Rows[rowindex + 1].Selected = true;           
            ////DataGridView1.Rows[rowindex].Selected = false;
            ////if (prev>txt_Counter.Text.ToInt32()) row = DataGridView1.Rows[rowindex + 1];
            ////else row = DataGridView1.Rows[rowindex - 1];
            ////if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //txt_Counter.Text = prev.ToString();

        }

        private void btn_NextItem_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (DataGridView1.Rows.Count == prev + 2) return;

            //SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex + 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataGridView1.Rows[rowindex + 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            txt_Counter.Text = prev.ToString();
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (prev == 0) return;

            //SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex - 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataGridView1.Rows[rowindex - 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            txt_Counter.Text = prev.ToString();
        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            //var cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"Retail\");";
            //DataSet dsz = new DataSet();
            //using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
            //    dab.Fill(dsz, "Groups");
            //    dab.Dispose();
            //}
            var insertcmdd = "CDLC_ID, Groups, Type";
            var insertvalues = "\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"Retail\"";
            InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0);
            GroupChanged = true;
            ChangeRow();
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            //var cmd = "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            //        DataSet dhs = new DataSet();

            //        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
            //        dhx.Fill(dhs, "Groups");
            //        dhx.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //MessageBox.Show("Can not Delete Song folder ! ");
            //}
            DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"", cnb);
            GroupChanged = true;
            ChangeRow();
        }

        private void btn_GroupSave_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text != null || chbx_Group.Text != "")
            {
                //var cmd = "UPDATE Removed=\"Yes\" FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
                //var cmd = "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
                //try
                //{
                //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //    {
                //        DataSet dhs = new DataSet();

                //        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
                //        dhx.Fill(dhs, "Groups");
                //        dhx.Dispose();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //MessageBox.Show("Can not Delete Song folder ! ");
                //}

                ////calc how many rows are filled with values
                //DataSet dssx = new DataSet();
                //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //{
                //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Cache AS O", cn);
                //    da.Fill(dssx, "Cache");
                //    dssx.Dispose();
                //    //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //    //da.Fill(ds, "PositionType");
                //    //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //    //da.Fill(ds, "Badge");
                //}
                DeleteFromDB("Groups", "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"", cnb);
                DataSet ds = new DataSet(); ds = SelectFromDB("Cache", "SELECT * from Cache AS O", "", cnb);
                var recs = dssx.Tables[0].Rows.Count;
                pB_ReadDLCs.Value = 0;
                if (recs != 0)
                    pB_ReadDLCs.Maximum = 2 * recs;
                DataSet dsz = new DataSet();
                for (var i = 0; i < recs; i++)
                {
                    string IDD = DataGridView1.Rows[i].Cells["ID"].Value.ToString();
                    //cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + IDD + "\",\"" + chbx_Group.Text + "\",\"Retail\");";
                    //if (DataGridView1.Rows[i].Cells["Removed"].Value.ToString() != "Yes")
                    //    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                    //    {
                    //        OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                    //        dab.Fill(dsz, "Groups");
                    //        dab.Dispose();
                    //    }
                    var insertcmdd = "CDLC_ID, Groups, Type";
                    var insertvalues = "\"" + IDD + "\",\"" + chbx_Group.Text + "\",\"Retail\"";
                    InsertIntoDBwValues("Groups", insertcmdd, insertvalues, cnb, 0);

                    pB_ReadDLCs.Increment(2);
                }
            }
        }

        private void btn_GroupLoad_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text != null || chbx_Group.Text != "")
            {
                var connection = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    command.CommandText = "UPDATE Cache SET ";
                    command.CommandText += "Removed = @param8 ";
                    command.CommandText += "Selected = @param9, ";
                    command.Parameters.AddWithValue("@param8", "Yes");
                    command.Parameters.AddWithValue("@param9", "No");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can not Delete Song folder ! ");
                    }
                }


                //var cmd = "UPDATE Cache SET Removed=\"No\",Selected=\"Yes\" WHERE cstr(ID) IN (SELECT CDLC_ID From Groups WHERE Groups=\"" + chbx_Group.Text + "\")";
                //try
                //{
                //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                //    {
                //        DataSet dhs = new DataSet();

                //        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
                //        dhx.Fill(dhs, "Cache");
                //        dhx.Dispose();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //MessageBox.Show("Can not Delete Song folder ! ");
                //}
                DataSet dhs = new DataSet(); dhs = UpdateDB("Cache", "UPDATE Cache SET Removed=\"No\",Selected=\"Yes\" WHERE cstr(ID) IN (SELECT CDLC_ID From Groups WHERE Groups=\"" + chbx_Group.Text + "\")", cnb);
            }
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
            ChangeRow();
        }

        private void chbx_AllGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            GroupChanged = true;
        }

        private void chbx_Removed_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbx_Removed.Checked) chbx_Selected.Checked = false;
            else chbx_Selected.Checked = true;
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Cache SET ";
            command.CommandText += "Removed = @param8, ";
            command.CommandText += "Selected = @param9 ";
            command.Parameters.AddWithValue("@param8", "No");
            command.Parameters.AddWithValue("@param9", "Yes");

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                //throw;
            }
            finally
            {
                //if (cnn != null) cnn.Close();
            }
            //}
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();


            //var com = "SELECT * FROM Cache";
            //try
            //{
            //    DataSet dhs = new DataSet();
            //    using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {// 1. If hash already exists do not insert
            //        OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
            //        dBs.Fill(dhs, "Main");
            //        dBs.Dispose();
            //        MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected and Unmarked as Removed :)");
            //    }
            //}
            //catch (Exception ee)
            //{
            //    Console.WriteLine(ee.Message);
            //    MessageBox.Show("Error at select filtered " + ee);
            //}
            DataSet dxr = new DataSet(); dxr = UpdateDB("Cache", "SELECT * FROM Cache", cnb);
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Cache SET ";
            command.CommandText += "Removed = @param8,";
            command.CommandText += "Selected = @param9 ";
            command.Parameters.AddWithValue("@param8", "No");
            command.Parameters.AddWithValue("@param9", "Yes");

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                //cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                throw;
            }
            finally
            {
                //if (cnn != null) cnn.Close();
            }
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
            cnn.Dispose();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected");
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)//chbx_Group_SelectedValueChanged
        {

            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Cache WHERE ";
            var Filtertxt = cmb_Filter.Text;//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {
                //0ALL
                case "0ALL":
                    SearchCmd = SearchCmd.Replace(" WHERE ", "");
                    break;
                case "Pc":
                    SearchCmd += "Platform = 'Pc'";
                    break;
                case "PS3":
                    SearchCmd += "Platform = 'PS3'";
                    break;
                case "Mac":
                    SearchCmd += "Platform ='Mac'";
                    break;
                case "XBOX360":
                    SearchCmd += "Platform = 'XBOX360'";
                    break;
                case "Removed":
                    SearchCmd += "Removed = 'Yes'";
                    break;
                case "Selected":
                    SearchCmd += "Selected = 'Yes'";
                    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY ID";
            //MessageBox.Show(Filtertxt + SearchCmd);
            //try
            //{
                //this.DataGridView1.DataSource = null; //Then clear the rows:

                //this.DataGridView1.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                //dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            //    DataGridView1.Refresh();
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void chbx_Selected_Click(object sender, EventArgs e)
        {

        }

        private void chbx_Selected_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbx_Selected.Checked) chbx_Removed.Checked = false;
            else chbx_Removed.Checked = true;
        }

        private void txt_Album_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Filter_SelectedValueChanged1(object sender, EventArgs e)
        {
            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Cache u WHERE ";
            var Filtertxt = cmb_Filter.Text;//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {
                case "Mac":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "PS3":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "XBOX":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "PC":
                    SearchCmd += "Has_Cover <> 'Yes'";
                    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY Artist, Album_Year, Album, Song_Title ";
            //MessageBox.Show(Filtertxt + SearchCmd);
            //try
            //{
                this.DataGridView1.DataSource = null; //Then clear the rows:

                this.DataGridView1.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void chbx_PreSavedFTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chbx_PreSavedFTP.Text == "EU") txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP1"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLUS31182/USRDIR/DLC/";
            else txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP2"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLES01862/USRDIR/DLC/";
        }

        private void btn_OpenCorrespondence_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            WEM2OGGCorrespondence frm = new WEM2OGGCorrespondence(DB_Path, TempPath, RocksmithDLCPath, cnb);//.Replace("\\AccessDB.accdb", "")
            frm.Show();
        }
    }
}

