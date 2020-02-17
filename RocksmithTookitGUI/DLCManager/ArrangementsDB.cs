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
using System.Diagnostics;
using RocksmithToolkitGUI;
using RocksmithToolkitLib.Extensions;
using Ookii.Dialogs; //cue text
using System.IO; //file io things
using RocksmithToolkitLib.XML; //For xml read library
using RocksmithToolkitLib.DLCPackage; //4packing
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;
using RocksmithToolkitLib.XmlRepository;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class ArrangementsDB : Form
    {
        public ArrangementsDB(string txt_DBFolder, string CDLC_ID, bool chbx_BassD, OleDbConnection cnnb)
        {
            InitializeComponent();
            DB_Path = txt_DBFolder;
            CDLCID = CDLC_ID;
            BassDD = chbx_BassD;
            chbx_BassDD.Checked = BassDD;
            cnb = cnnb;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "ArrangementsDB";

        //bcapi
        public string DB_Path = "";
        public string CDLCID = "";
        public bool BassDD;
        public int noOfRec = 0;
        public DataSet dssx = new DataSet();
        public bool SaveOK = false;
        public OleDbConnection cnb;

        private void ArrangementsDB_Load(object sender, EventArgs e)
        {
            Populate(ref databox, ref Main);//, ref bsPositions, ref bsBadges);
            databox.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (true) //(DataGridView1.CurrentCell.IsComboBoxCell())
            {
                if (databox.Columns[databox.CurrentCell.ColumnIndex].Name == "ContactsColumn")
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
            if (CheckBox1.Checked)
            {
                MessageBox.Show(((DataGridViewComboBoxEditingControl)sender).Text);
            }
            else
            {
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
            }
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
                MessageBox.Show("Can not open Arrangements DB connection in ArrangementsDB ! " + DB_Path);
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


            ////ImageSource imageSource = new BitmapImage(new Uri("C:\\Temp\\music_edit.png"));

            //picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            noOfRec = 0;
            lbl_NoRec.Text = " songs.";
            bs.DataSource = null;
            dssx.Dispose();
            //var cmd = "SELECT * FROM Arrangements WHERE CDLC_ID=" + CDLCID + ";";
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    try
            //    {
            //        OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
            //        da.Fill(dssx, "Arrangements");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        MessageBox.Show("-DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
            //        ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or you need to Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
            //        frm1.ShowDialog();
            //        return;
            //    }
            //cn.Dispose();
            dssx = SelectFromDB("Arrangements", "SELECT * FROM Arrangements WHERE CDLC_ID=" + CDLCID + ";", "", cnb);
            noOfRec = dssx.Tables[0].Rows.Count;
            lbl_NoRec.Text = noOfRec.ToString() + " records.";
            //}
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID " };
            DataGridViewTextBoxColumn Arrangement_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Arrangement_Name", HeaderText = "Arrangement_Name " };
            DataGridViewTextBoxColumn CDLC_ID = new DataGridViewTextBoxColumn { DataPropertyName = "CDLC_ID", HeaderText = "CDLC_ID " };
            DataGridViewTextBoxColumn Bonus = new DataGridViewTextBoxColumn { DataPropertyName = "Bonus", HeaderText = "Bonus " };
            DataGridViewTextBoxColumn SNGFilePath = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFilePath", HeaderText = "SNGFilePath " };
            DataGridViewTextBoxColumn XMLFilePath = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFilePath", HeaderText = "XMLFilePath " };
            DataGridViewTextBoxColumn XMLFile_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFile_Hash", HeaderText = "XMLFile_Hash " };
            DataGridViewTextBoxColumn ScrollSpeed = new DataGridViewTextBoxColumn { DataPropertyName = "ScrollSpeed", HeaderText = "ScrollSpeed " };
            DataGridViewTextBoxColumn Tunning = new DataGridViewTextBoxColumn { DataPropertyName = "Tunning", HeaderText = "Tunning " };
            DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating " };
            DataGridViewTextBoxColumn PlaythroughYBLink = new DataGridViewTextBoxColumn { DataPropertyName = "PlaythroughYBLink", HeaderText = "PlaythroughYBLink " };
            DataGridViewTextBoxColumn CustomsForge_Link = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Link", HeaderText = "CustomsForge_Link " };
            DataGridViewTextBoxColumn ArrangementSort = new DataGridViewTextBoxColumn { DataPropertyName = "ArrangementSort", HeaderText = "ArrangementSort " };
            DataGridViewTextBoxColumn TuningPitch = new DataGridViewTextBoxColumn { DataPropertyName = "TuningPitch", HeaderText = "TuningPitch " };
            DataGridViewTextBoxColumn ToneBase = new DataGridViewTextBoxColumn { DataPropertyName = "ToneBase", HeaderText = "ToneBase " };
            DataGridViewTextBoxColumn Idd = new DataGridViewTextBoxColumn { DataPropertyName = "Idd", HeaderText = "Idd " };
            DataGridViewTextBoxColumn MasterId = new DataGridViewTextBoxColumn { DataPropertyName = "MasterId", HeaderText = "MasterId " };
            DataGridViewTextBoxColumn ArrangementType = new DataGridViewTextBoxColumn { DataPropertyName = "ArrangementType", HeaderText = "ArrangementType " };
            DataGridViewTextBoxColumn String0 = new DataGridViewTextBoxColumn { DataPropertyName = "String0", HeaderText = "String0 " };
            DataGridViewTextBoxColumn String1 = new DataGridViewTextBoxColumn { DataPropertyName = "String1", HeaderText = "String1 " };
            DataGridViewTextBoxColumn String2 = new DataGridViewTextBoxColumn { DataPropertyName = "String2", HeaderText = "String2 " };
            DataGridViewTextBoxColumn String3 = new DataGridViewTextBoxColumn { DataPropertyName = "String3", HeaderText = "String3 " };
            DataGridViewTextBoxColumn String4 = new DataGridViewTextBoxColumn { DataPropertyName = "String4", HeaderText = "String4 " };
            DataGridViewTextBoxColumn String5 = new DataGridViewTextBoxColumn { DataPropertyName = "String5", HeaderText = "String5 " };
            DataGridViewTextBoxColumn PluckedType = new DataGridViewTextBoxColumn { DataPropertyName = "PluckedType", HeaderText = "PluckedType " };
            DataGridViewTextBoxColumn RouteMask = new DataGridViewTextBoxColumn { DataPropertyName = "RouteMask", HeaderText = "RouteMask " };
            DataGridViewTextBoxColumn XMLFileName = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFileName", HeaderText = "XMLFileName " };
            DataGridViewTextBoxColumn XMLFileLLID = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFileLLID", HeaderText = "XMLFileLLID " };
            DataGridViewTextBoxColumn XMLFileUUID = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFileUUID", HeaderText = "XMLFileUUID " };
            DataGridViewTextBoxColumn SNGFileName = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileName", HeaderText = "SNGFileName " };
            DataGridViewTextBoxColumn SNGFileLLID = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileLLID", HeaderText = "SNGFileLLID " };
            DataGridViewTextBoxColumn SNGFileUUID = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileUUID", HeaderText = "SNGFileUUID " };
            DataGridViewTextBoxColumn ToneMultiplayer = new DataGridViewTextBoxColumn { DataPropertyName = "ToneMultiplayer", HeaderText = "ToneMultiplayer " };
            DataGridViewTextBoxColumn ToneA = new DataGridViewTextBoxColumn { DataPropertyName = "ToneA", HeaderText = "ToneA " };
            DataGridViewTextBoxColumn ToneB = new DataGridViewTextBoxColumn { DataPropertyName = "ToneB", HeaderText = "ToneB " };
            DataGridViewTextBoxColumn ToneC = new DataGridViewTextBoxColumn { DataPropertyName = "ToneC", HeaderText = "ToneC " };
            DataGridViewTextBoxColumn ToneD = new DataGridViewTextBoxColumn { DataPropertyName = "ToneD", HeaderText = "ToneD " };
            DataGridViewTextBoxColumn lastConversionDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "lastConversionDateTime", HeaderText = "lastConversionDateTime " };
            DataGridViewTextBoxColumn SNGFileHash = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileHash", HeaderText = "SNGFileHash " };
            DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
            DataGridViewTextBoxColumn Start_Time = new DataGridViewTextBoxColumn { DataPropertyName = "Start_Time", HeaderText = "Start_Time " };
            DataGridViewTextBoxColumn SNGFileHash_Orig = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileHash_Orig", HeaderText = "SNGFileHash_Orig " };
            DataGridViewTextBoxColumn XMLFile_Hash_Orig = new DataGridViewTextBoxColumn { DataPropertyName = "XMLFile_Hash_Orig", HeaderText = "XMLFile_Hash_Orig " };
            DataGridViewTextBoxColumn Part = new DataGridViewTextBoxColumn { DataPropertyName = "Part", HeaderText = "Part " };
            DataGridViewTextBoxColumn MaxDifficulty = new DataGridViewTextBoxColumn { DataPropertyName = "MaxDifficulty", HeaderText = "MaxDifficulty " };

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

            //DataGridView.AutoGenerateColumns = false;

            //DataGridView.Columns.AddRange(new DataGridViewColumn[]
            //{
            //    ID,
            //    Arrangement_Name,
            //    CDLC_ID,
            //    Bonus,
            //    SNGFilePath,
            //    XMLFilePath,
            //    XMLFile_Hash,
            //    ScrollSpeed,
            //    Tunning,
            //    Rating,
            //    PlaythroughYBLink,
            //    CustomsForge_Link,
            //    ArrangementSort,
            //    TuningPitch,
            //    ToneBase,
            //    Idd,
            //    MasterId,
            //    ArrangementType,
            //    String0,
            //    String1,
            //    String2,
            //    String3,
            //    String4,
            //    String5,
            //    PluckedType,
            //    RouteMask,
            //    XMLFileName,
            //    XMLFileLLID,
            //    XMLFileUUID,
            //    SNGFileName,
            //    SNGFileLLID,
            //    SNGFileUUID,
            //    ToneMultiplayer,
            //    ToneA,
            //    ToneB,
            //    ToneC,
            //    ToneD,
            //    lastConversionDateTime,
            //    SNGFileHash
            //}
            //);

            dssx.Tables["Arrangements"].AcceptChanges();

            bs.DataSource = dssx.Tables["Arrangements"];
            DataGridView.DataSource = bs;
            //DataGridView.ExpandColumns();

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
        }

        private class Files
        {
            public string ID { get; set; }
            public string Arrangement_Name { get; set; }
            public string CDLC_ID { get; set; }
            public string Bonus { get; set; }
            public string SNGFilePath { get; set; }
            public string XMLFilePath { get; set; }
            public string XMLFile_Hash { get; set; }
            public string ScrollSpeed { get; set; }
            public string Tunning { get; set; }
            public string Rating { get; set; }
            public string PlaythroughYBLink { get; set; }
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
            public string lastConversionDateTime { get; set; }
            public string SNGFileHash { get; set; }
            public string Has_Sections { get; set; }
            public string Comments { get; set; }
            public string Start_Time { get; set; }
            public string SNGFileHash_Orig { get; set; }
            public string XMLFile_Hash_Orig { get; set; }
            public string Part { get; set; }
            public string MaxDifficulty { get; set; }
        }

        private Files[] files = new Files[10000];
        //Generic procedure to read and parse Main.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            //try
            //{
            //    //MessageBox.Show(DB_Path);
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //        dax.Fill(dus, "Arrangements");
            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", cmd, "", cnb);
            var i = 0;
            //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
            MaximumSize = dus.Tables[0].Rows.Count;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                files[i] = new Files();

                //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                files[i].ID = dataRow.ItemArray[0].ToString();
                files[i].Arrangement_Name = dataRow.ItemArray[1].ToString();
                files[i].CDLC_ID = dataRow.ItemArray[2].ToString();
                files[i].Bonus = dataRow.ItemArray[3].ToString();
                files[i].SNGFilePath = dataRow.ItemArray[5].ToString();
                files[i].XMLFilePath = dataRow.ItemArray[6].ToString();
                files[i].XMLFile_Hash = dataRow.ItemArray[7].ToString();
                files[i].ScrollSpeed = dataRow.ItemArray[8].ToString();
                files[i].Tunning = dataRow.ItemArray[9].ToString();
                files[i].Rating = dataRow.ItemArray[10].ToString();
                files[i].PlaythroughYBLink = dataRow.ItemArray[11].ToString();
                files[i].CustomsForge_Link = dataRow.ItemArray[12].ToString();
                files[i].ArrangementSort = dataRow.ItemArray[13].ToString();
                files[i].TuningPitch = dataRow.ItemArray[14].ToString();
                files[i].ToneBase = dataRow.ItemArray[15].ToString();
                files[i].Idd = dataRow.ItemArray[16].ToString();
                files[i].MasterId = dataRow.ItemArray[17].ToString();
                files[i].ArrangementType = dataRow.ItemArray[18].ToString();
                files[i].String0 = dataRow.ItemArray[19].ToString();
                files[i].String1 = dataRow.ItemArray[20].ToString();
                files[i].String2 = dataRow.ItemArray[21].ToString();
                files[i].String3 = dataRow.ItemArray[22].ToString();
                files[i].String4 = dataRow.ItemArray[23].ToString();
                files[i].String5 = dataRow.ItemArray[24].ToString();
                files[i].PluckedType = dataRow.ItemArray[25].ToString();
                files[i].RouteMask = dataRow.ItemArray[26].ToString();
                files[i].XMLFileName = dataRow.ItemArray[27].ToString();
                files[i].XMLFileLLID = dataRow.ItemArray[28].ToString();
                files[i].XMLFileUUID = dataRow.ItemArray[29].ToString();
                files[i].SNGFileName = dataRow.ItemArray[30].ToString();
                files[i].SNGFileLLID = dataRow.ItemArray[31].ToString();
                files[i].SNGFileUUID = dataRow.ItemArray[32].ToString();
                files[i].ToneMultiplayer = dataRow.ItemArray[33].ToString();
                files[i].ToneA = dataRow.ItemArray[34].ToString();
                files[i].ToneB = dataRow.ItemArray[35].ToString();
                files[i].ToneC = dataRow.ItemArray[36].ToString();
                files[i].ToneD = dataRow.ItemArray[37].ToString();
                files[i].lastConversionDateTime = dataRow.ItemArray[38].ToString();
                files[i].SNGFileHash = dataRow.ItemArray[39].ToString();
                files[i].Has_Sections = dataRow.ItemArray[40].ToString();
                files[i].Comments = dataRow.ItemArray[41].ToString();
                files[i].Start_Time = dataRow.ItemArray[42].ToString();
                files[i].SNGFileHash_Orig = dataRow.ItemArray[43].ToString();
                files[i].XMLFile_Hash_Orig = dataRow.ItemArray[44].ToString();
                files[i].Part = dataRow.ItemArray[45].ToString();
                files[i].MaxDifficulty = dataRow.ItemArray[46].ToString();
                i++;
            }
            //Closing Connection
            dus.Dispose();
            //        cnn.Close();
            //        //rtxt_StatisticsOnReadDLCs.Text += i;
            //        //var ex = 0;
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Arrangements DB connection ! ");
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = databox.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {

            var norec = 0;
            //DataSet ds = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneA FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(ds, "Arrangements");
            DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT DISTINCT ToneA FROM Arrangements;", "", cnb);
            norec = ds.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_ToneA.Items.Count > 0)
                {
                    chbx_ToneA.DataSource = null;
                    for (int k = chbx_ToneA.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_ToneA.Items[k].ToString().Contains("--"))
                        {
                            chbx_ToneA.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_ToneA.Items.Add(ds.Tables[0].Rows[j][0].ToString());
            }
            ds.Dispose();
            //}
            //DataSet dIs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneB FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dIs, "Arrangements");
            DataSet dIs = new DataSet(); dIs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneB FROM Arrangements;", "", cnb);
            norec = dIs.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_ToneB.Items.Count > 0)
                {
                    chbx_ToneB.DataSource = null;
                    for (int k = chbx_ToneB.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_ToneB.Items[k].ToString().Contains("--"))
                        {
                            chbx_ToneB.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_ToneB.Items.Add(dIs.Tables[0].Rows[j][0].ToString());
            }
            dIs.Dispose();
            //}
            //DataSet dfs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneC FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dfs, "Arrangements");
            DataSet dfs = new DataSet(); dfs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneC FROM Arrangements;", "", cnb);
            norec = dfs.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_ToneC.Items.Count > 0)
                {
                    chbx_ToneC.DataSource = null;
                    for (int k = chbx_ToneC.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_ToneC.Items[k].ToString().Contains("--"))
                        {
                            chbx_ToneC.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_ToneC.Items.Add(dfs.Tables[0].Rows[j][0].ToString());
            }
            dfs.Dispose();
            //}
            //DataSet dHs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneD FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dHs, "Arrangements");
            DataSet dHs = new DataSet(); dHs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneD FROM Arrangements;", "", cnb);
            norec = dHs.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_ToneD.Items.Count > 0)
                {
                    chbx_ToneD.DataSource = null;
                    for (int k = chbx_ToneD.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_ToneD.Items[k].ToString().Contains("--"))
                        {
                            chbx_ToneD.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_ToneD.Items.Add(dHs.Tables[0].Rows[j][0].ToString());
            }
            dHs.Dispose();
            //}

            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneBase FROM Arrangements;", "", cnb);
            //DataSet dxs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneBase FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dxs, "Arrangements");
            norec = dxs.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_ToneBase.Items.Count > 0)
                {
                    chbx_ToneBase.DataSource = null;
                    for (int k = chbx_ToneBase.Items.Count - 1; k >= 0; --k)
                    {
                        if (!chbx_ToneBase.Items[k].ToString().Contains("--"))
                        {
                            chbx_ToneBase.Items.RemoveAt(k);
                        }
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_ToneBase.Items.Add(dxs.Tables[0].Rows[j][0].ToString());
            }
            //}
            //DataSet dks = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT Tunning FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dks, "Arrangements");
            DataSet dks = new DataSet(); dks = SelectFromDB("Arrangements", "SELECT DISTINCT Tunning FROM Arrangements;", "", cnb);
            norec = dks.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_Tunning.Items.Count > 0)
                {
                    chbx_Tunning.DataSource = null;
                    for (int k = chbx_Tunning.Items.Count - 1; k >= 0; --k)
                    {
                        //if (!chbx_Tunning.Items[k].ToString().Contains("--"))
                        //{
                        chbx_Tunning.Items.RemoveAt(k);
                        //}
                    }
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_Tunning.Items.Add(dks.Tables[0].Rows[j][0].ToString());
            }
            //}

            int i;
            if (databox.SelectedCells.Count > 0)
            {
                i = databox.SelectedCells[0].RowIndex;
                txt_ID.Text = databox.Rows[i].Cells["ID"].Value.ToString();
                txt_Arrangement_Name.Text = databox.Rows[i].Cells["Arrangement_Name"].Value.ToString();
                txt_CDLC_ID.Text = databox.Rows[i].Cells["CDLC_ID"].Value.ToString();
                txt_SNGFilePath.Text = databox.Rows[i].Cells["SNGFilePath"].Value.ToString();
                txt_XMLFilePath.Text = databox.Rows[i].Cells["XMLFilePath"].Value.ToString();
                txt_ScrollSpeed.Text = databox.Rows[i].Cells["ScrollSpeed"].Value.ToString();
                chbx_Tunning.Text = databox.Rows[i].Cells["Tunning"].Value.ToString();
                txt_Rating.Text = databox.Rows[i].Cells["Rating"].Value.ToString();
                txt_TuningPitch.Text = databox.Rows[i].Cells["TuningPitch"].Value.ToString();
                chbx_ToneBase.Text = databox.Rows[i].Cells["ToneBase"].Value.ToString();
                //txt_Idd.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
                txt_ArrangementType.Text = databox.Rows[i].Cells["ArrangementType"].Value.ToString();
                txt_String0.Text = databox.Rows[i].Cells["String0"].Value.ToString();
                txt_String1.Text = databox.Rows[i].Cells["String1"].Value.ToString();
                txt_String2.Text = databox.Rows[i].Cells["String2"].Value.ToString();
                txt_String3.Text = databox.Rows[i].Cells["String3"].Value.ToString();
                txt_String4.Text = databox.Rows[i].Cells["String4"].Value.ToString();
                txt_String5.Text = databox.Rows[i].Cells["String5"].Value.ToString();
                chbx_BassPicking.Text = databox.Rows[i].Cells["PluckedType"].Value.ToString();
                txt_RouteMask.Text = databox.Rows[i].Cells["RouteMask"].Value.ToString();
                chbx_ToneA.Text = databox.Rows[i].Cells["ToneA"].Value.ToString();
                chbx_ToneB.Text = databox.Rows[i].Cells["ToneB"].Value.ToString();
                chbx_ToneC.Text = databox.Rows[i].Cells["ToneC"].Value.ToString();
                chbx_ToneD.Text = databox.Rows[i].Cells["ToneD"].Value.ToString();
                txt_lastConversionDateTime.Text = databox.Rows[i].Cells["lastConversionDateTime"].Value.ToString();
                txt_Description.Text = databox.Rows[i].Cells["Comments"].Value.ToString();

                if (databox.Rows[i].Cells["Bonus"].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
                else chbx_Bonus.Checked = false;
                if (databox.Rows[i].Cells["Has_Sections"].Value.ToString() == "Yes") chbx_HasSection.Checked = true;
                else chbx_HasSection.Checked = false;

                if (txt_ArrangementType.Text == "Bass" && !(chbx_BassDD.Checked)) btn_AddDD.Enabled = true;
                if (txt_ArrangementType.Text == "Bass" && chbx_BassDD.Checked) btn_RemoveDD.Enabled = true;

                //cmb_Tracks
                if (cmb_Tracks.Items.Count > 0) for (int k = cmb_Tracks.Items.Count - 1; k >= 0; --k) cmb_Tracks.Items.RemoveAt(k);//remove items

                //get dupli&songlenght
                var scmd = "SELECT Duplicate_Of,Song_Lenght,Song_Title FROM Main WHERE ID=" + txt_CDLC_ID.Text + ";";
                DataSet dzvs = new DataSet(); dzvs = SelectFromDB("Main", scmd, "", cnb);
                var sl = dzvs.Tables[0].Rows[0].ItemArray[1].ToString();
                var dupl = dzvs.Tables[0].Rows[0].ItemArray[0].ToString();
                var sname = dzvs.Tables[0].Rows[0].ItemArray[2].ToString();

                scmd = "SELECT ID FROM Main WHERE ID=" + dupl + " OR ID=" + txt_CDLC_ID.Text + " OR Duplicate_Of=\"" + (dupl == "0" ? "999999" : dupl)
                    + "\" OR Duplicate_Of=\"" + txt_CDLC_ID.Text + "\";";
                DataSet dzs = new DataSet(); dzs = SelectFromDB("Main", scmd, "", cnb);
                var norecs = dzs.Tables.Count == 0 ? 0 : dzs.Tables[0].Rows.Count;
                var IDs = "0";
                for (var j = 0; j <= norecs - 1; j++) IDs += "," + dzs.Tables[0].Rows[j].ItemArray[0].ToString();

                //same name
                scmd = "SELECT ID FROM Main WHERE SongTitle=\"" + CleanTitle(sname) + "\" AND ID not in (" + IDs + ");";
                DataSet dzcs = new DataSet(); dzcs = SelectFromDB("Main", scmd, "", cnb);
                norecs = dzcs.Tables.Count == 0 ? 0 : dzcs.Tables[0].Rows.Count;
                for (var j = 0; j <= norecs - 1; j++) IDs += "," + dzcs.Tables[0].Rows[j].ItemArray[0].ToString();

                scmd = "SELECT ID, XMLFileName, Start_Time, RouteMask, Bonus, ArrangementType, CDLC_ID FROM Arrangements WHERE CDLC_ID IN (" + IDs + ");";
                DataSet dnzs = new DataSet(); dnzs = SelectFromDB("Arrangements", scmd, "", cnb);
                norecs = dnzs.Tables.Count == 0 ? 0 : dnzs.Tables[0].Rows.Count;
                if (norecs > 0)
                    for (int j = 0; j < norecs; j++)
                        if (dnzs.Tables[0].Rows[j][0].ToString() != "" && dnzs.Tables[0].Rows[j][0].ToString() != null)
                        {
                            if (dnzs.Tables[0].Rows[j][5].ToString().IndexOf("ShowLight") >= 0) continue;
                            scmd = "SELECT Song_Lenght,Duplicate_Of FROM Main WHERE ID=" + dnzs.Tables[0].Rows[j][6].ToString();
                            DataSet djs = new DataSet(); djs = SelectFromDB("Main", scmd, "", cnb);
                            //norecs = djs.Tables.Count == 0 ? 0 : djs.Tables[0].Rows.Count;
                            //for (var j = 0; j <= norecs - 1; j++) if (dzs.Tables[0].Rows[j].ItemArray[1].ToString().IndexOf("ShowLight") < 0) IDs += "," + dzs.Tables[0].Rows[j].ItemArray[0].ToString();


                            var v = "DLC ID: " + dnzs.Tables[0].Rows[j][6].ToString() + "_" + dnzs.Tables[0].Rows[j][1].ToString() + "_Start Time: "
                            + dnzs.Tables[0].Rows[j][2].ToString() + "s_" + dnzs.Tables[0].Rows[j][3].ToString()
                            + "_" + (dnzs.Tables[0].Rows[j][4].ToString() == "True" ? "Bonus" : "") + "_Duplicate Of: " + djs.Tables[0].Rows[0].ItemArray[1].ToString()
                            + "_Dupli/Curent Lenght: " + djs.Tables[0].Rows[0].ItemArray[0].ToString() + "-" + sl + "s_ArrangementID=" + dnzs.Tables[0].Rows[j][0].ToString();
                            cmb_Tracks.Items.Add(v);//add items
                        }

                cmb_Tracks.Items.Add(""); cmb_Tracks.Text = "";

                if (chbx_AutoSave.Checked) SaveOK = true;
                else SaveOK = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var i = DataViewGrid.SelectedCells[0].RowIndex;
            //string filePath = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();
            var fileName = txt_SNGFilePath.Text;

            try
            {
                Process process = Process.Start("notepad.exe", fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();

            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = databox.SelectedCells[0].RowIndex;

                databox.Rows[i].Cells["ID"].Value = txt_ID.Text;
                databox.Rows[i].Cells["Arrangement_Name"].Value = txt_Arrangement_Name.Text;
                databox.Rows[i].Cells["CDLC_ID"].Value = txt_CDLC_ID.Text;
                //DataGridView1.Rows[i].Cells["SNGFilePath"].Value = txt_SNGFilePath.Text;
                //DataGridView1.Rows[i].Cells["XMLFilePath"].Value = txt_XMLFilePath.Text;
                databox.Rows[i].Cells["ScrollSpeed"].Value = txt_ScrollSpeed.Text;
                databox.Rows[i].Cells["Tunning"].Value = chbx_Tunning.Text;
                databox.Rows[i].Cells["Rating"].Value = txt_Rating.Text;
                databox.Rows[i].Cells["TuningPitch"].Value = txt_TuningPitch.Text;
                databox.Rows[i].Cells["ToneBase"].Value = chbx_ToneBase.Text;
                //DataGridView1.Rows[i].Cells[""].Value = txt_Idd.Text;
                databox.Rows[i].Cells["ArrangementType"].Value = txt_ArrangementType.Text;
                databox.Rows[i].Cells["String0"].Value = txt_String0.Text;
                databox.Rows[i].Cells["String1"].Value = txt_String1.Text;
                databox.Rows[i].Cells["String2"].Value = txt_String2.Text;
                databox.Rows[i].Cells["String3"].Value = txt_String3.Text;
                databox.Rows[i].Cells["String4"].Value = txt_String4.Text;
                databox.Rows[i].Cells["String5"].Value = txt_String5.Text;
                databox.Rows[i].Cells["PluckedType"].Value = chbx_BassPicking.Text;
                databox.Rows[i].Cells["RouteMask"].Value = txt_RouteMask.Text;
                databox.Rows[i].Cells["ToneA"].Value = chbx_ToneA.Text;
                databox.Rows[i].Cells["ToneB"].Value = chbx_ToneB.Text;
                databox.Rows[i].Cells["ToneC"].Value = chbx_ToneC.Text;
                databox.Rows[i].Cells["ToneD"].Value = chbx_ToneD.Text;
                databox.Rows[i].Cells["lastConversionDateTime"].Value = txt_lastConversionDateTime.Text;
                databox.Rows[i].Cells["Comments"].Value = txt_Description.Text;
                databox.Rows[i].Cells["Has_Sections"].Value = chbx_HasSection.Checked ? "Yes" : "No";
                databox.Rows[i].Cells["Comments"].Value = txt_Description.Text;
                //if (chbx_Bonus.Checked) DataGridView1.Rows[i].Cells["Bonus"].Value = "Yes";
                //else DataGridView1.Rows[i].Cells["Bonus"].Value = "No";


                //var DB_Path = "../../../../tmp\\AccessDB.accdb;";
                var connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    //OleDbCommand command = new OleDbCommand(); ;
                    //Update MainDB
                    //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                    command.CommandText = "UPDATE Arrangements SET ";

                    //command.CommandText += "ID = @param0, ";
                    command.CommandText += "Arrangement_Name = @param1, ";
                    command.CommandText += "CDLC_ID = @param2, ";
                    command.CommandText += "Bonus = @param3, ";
                    //command.CommandText += "SNGFilePath = @param4, ";
                    //command.CommandText += "XMLFilePath = @param5, ";
                    //command.CommandText += "XMLFile_Hash = @param6, ";
                    command.CommandText += "ScrollSpeed = @param7, ";
                    command.CommandText += "Tunning = @param8, ";
                    command.CommandText += "Rating = @param9, ";
                    //command.CommandText += "PlaythroughYBLink = @param10, ";
                    //command.CommandText += "CustomsForge_Link = @param11, ";
                    //command.CommandText += "ArrangementSort = @param12, ";
                    command.CommandText += "TuningPitch = @param13, ";
                    command.CommandText += "ToneBase = @param14, ";
                    //command.CommandText += "Idd = @param15, ";
                    //command.CommandText += "MasterId = @param16, ";
                    command.CommandText += "ArrangementType = @param17, ";
                    command.CommandText += "String0 = @param18, ";
                    command.CommandText += "String1 = @param19, ";
                    command.CommandText += "String2 = @param20, ";
                    command.CommandText += "String3 = @param21, ";
                    command.CommandText += "String4 = @param22, ";
                    command.CommandText += "String5 = @param23, ";
                    command.CommandText += "PluckedType = @param24, ";
                    command.CommandText += "RouteMask = @param25, ";
                    //command.CommandText += "XMLFileName = @param26, ";
                    //command.CommandText += "XMLFileLLID = @param27, ";
                    //command.CommandText += "XMLFileUUID = @param28, ";
                    //command.CommandText += "SNGFileName = @param29, ";
                    //command.CommandText += "SNGFileLLID = @param30, ";
                    //command.CommandText += "SNGFileUUID = @param31, ";
                    //command.CommandText += "ToneMultiplayer = @param32, ";
                    command.CommandText += "ToneA = @param33, ";
                    command.CommandText += "ToneB = @param34, ";
                    command.CommandText += "ToneC = @param35, ";
                    command.CommandText += "ToneD = @param36, ";
                    command.CommandText += "lastConversionDateTime = @param37, ";
                    //command.CommandText += "SNGFileHash = @param38 ";
                    //command.CommandText += "Has_Sections = @param39 ";
                    command.CommandText += "Comments = @param40 ";

                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", databox.Rows[i].Cells["Arrangement_Name"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param2", databox.Rows[i].Cells["CDLC_ID"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param3", databox.Rows[i].Cells["Bonus"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param7", databox.Rows[i].Cells["ScrollSpeed"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param8", databox.Rows[i].Cells["Tunning"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param9", databox.Rows[i].Cells["Rating"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param13", databox.Rows[i].Cells["TuningPitch"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param14", databox.Rows[i].Cells["ToneBase"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param17", databox.Rows[i].Cells["ArrangementType"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param18", databox.Rows[i].Cells["String0"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param19", databox.Rows[i].Cells["String1"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param20", databox.Rows[i].Cells["String2"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param21", databox.Rows[i].Cells["String3"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param22", databox.Rows[i].Cells["String4"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param23", databox.Rows[i].Cells["String5"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param24", databox.Rows[i].Cells["PluckedType"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param25", databox.Rows[i].Cells["RouteMask"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param33", databox.Rows[i].Cells["ToneA"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param34", databox.Rows[i].Cells["ToneB"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param35", databox.Rows[i].Cells["ToneC"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param36", databox.Rows[i].Cells["ToneD"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param37", databox.Rows[i].Cells["lastConversionDateTime"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param40", databox.Rows[i].Cells["Comments"].Value.ToString() ?? DBNull.Value.ToString());
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Arrangements DB connection in Arrangement Edit screen ! " + DB_Path + "-" + command.CommandText);

                        //throw;
                    }
                    finally
                    {
                        if (connection != null) connection.Close();
                    }
                    ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                    if (!chbx_AutoSave.Checked) MessageBox.Show("Arrangement Saved");
                    //das.SelectCommand.CommandText = "SELECT * FROM Main";
                    //// das.Update(dssx, "Main");
                    dis.Dispose();
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (txt_CDLC_ID.Text != "") ChangeRow();
        }

        private void btn_OpenXML_Click(object sender, EventArgs e)
        {
            var fileName = txt_XMLFilePath.Text;

            try
            {
                Process process = Process.Start("notepad.exe", fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //var cmd = "SELECT * FROM Arrangements ";
            //cmd += "WHERE ID = " + txt_ID.Text + "";
            ////Read from DB
            //var norows = 0;
            //norows = SQLAccess(cmd);

            var xmlFiles = Directory.GetFiles(Path.GetDirectoryName(txt_XMLFilePath.Text), "*.xml", SearchOption.AllDirectories);
            var platform = txt_XMLFilePath.Text.GetPlatform();

            foreach (var xml in xmlFiles)
            {
                Song2014 xmlContent = null;
                try
                {
                    xmlContent = Song2014.LoadFromFile(xml);
                    if (!(xmlContent.Arrangement.ToLower() == "showlights" || xmlContent.Arrangement.ToLower() == "vocals") || xml.IndexOf(".old") <= 0)
                    {
                        var DDRemoved = (RemoveDD(Path.GetDirectoryName(txt_XMLFilePath.Text), "", xml, platform, false, false, "No") == "Yes") ? "Yes" : "No";
                    }
                }
                catch (Exception ee)
                { Console.Write(ee); }
            }
            MessageBox.Show("DD Removed");
            chbx_BassDD.Checked = false;
            SaveRecord();
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; btn_RemoveDD.Enabled = true;//numericUpDown1.Enabled = false;
            //var cmd = "SELECT * FROM Arrangements ";
            //cmd += "WHERE ID = " + txt_ID.Text + "";
            ////Read from DB
            //var norows = 0;
            //norows = SQLAccess(cmd);

            var xmlFiles = Directory.GetFiles(Path.GetDirectoryName(txt_XMLFilePath.Text), "*.xml", SearchOption.AllDirectories);
            var platform = txt_XMLFilePath.Text.GetPlatform();

            foreach (var xml in xmlFiles)
                if (xml.IndexOf("showlights") < 1)
                {
                    var DDAdded = (AddDD(Path.GetDirectoryName(txt_XMLFilePath.Text), "", xml, platform, false, false, numericUpDown1.Value.ToString()) == "Yes") ? "No" : "Yes";
                }
            chbx_BassDD.Checked = true;
            MessageBox.Show("DD Added");
            SaveRecord();
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                TonesDB frm = new TonesDB(DB_Path, txt_ID.Text, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose a Tone.");
        }

        private void btn_Youtube_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_Playthrough.Text);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            //Delete Arangements
            DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE ID IN (" + txt_ID.Text + ")", cnb);
            Populate(ref databox, ref Main);
        }

        private void Bth_ShiftVocalNotes_Click(object sender, EventArgs e)
        {
            if (cmb_Tracks.Text.IndexOf("Vocals") >= 0)
            {
                var SongID = databox.SelectedCells[0].RowIndex;
                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
                //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var noOfRec = dus.Tables[0].Rows.Count;
                var XMLFilePath = "";
                for (var i = 0; i <= noOfRec - 1; i++)
                {
                    var ArrangementType = dus.Tables[0].Rows[i].ItemArray[1].ToString();
                    //var RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (ArrangementType == "Vocal") XMLFilePath = dus.Tables[0].Rows[i].ItemArray[0].ToString();/* : XMLFilePath;*/
                }

                Vocals xmlContent = null;
                if (XMLFilePath != "")
                    try
                    {
                        xmlContent = Vocals.LoadFromFile(XMLFilePath);
                        for (var j = 0; j < xmlContent.Vocal.Length; j++)
                            xmlContent.Vocal[j].Time = xmlContent.Vocal[j].Time + float.Parse(num_Lyrics.Value.ToString());
                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }

                using (var stream = File.Open(XMLFilePath, FileMode.Create))
                    xmlContent.Serialize(stream);
                //chbx_Lyrics.Checked = true;
            }
            else
            {
                var ArrangID = cmb_Tracks.Text.Substring(cmb_Tracks.Text.IndexOf("_ArrangementID="), cmb_Tracks.Text.Length - cmb_Tracks.Text.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
                var DLCID = cmb_Tracks.Text.Substring(8, cmb_Tracks.Text.IndexOf("_") - 8);

                var i = databox.SelectedCells[0].RowIndex;
                string destination_dir = databox.Rows[i].Cells["Folder_Name"].Value.ToString();

                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName, RouteMask, Start_Time FROM Arrangements WHERE ID=" + ArrangID + "", "", cnb);
                var noOfRec = dus.Tables[0].Rows.Count;//var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
                var s2s = float.Parse(num_Lyrics.Value.ToString());

                if (txt_ID.Text != DLCID)
                {
                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, SNGFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, SNGFileHash_Orig, XMLFile_Hash_Orig, Part, MaxDifficulty";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID.Text + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(SNGFilePath,len(SNGFilePath)-" +
                        "instr(SNGFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, \"added from" + txt_CDLC_ID.Text + " " + txt_XMLFilePath.Text + "\", Start_Time, SNGFileHash_Orig," +
                        " XMLFile_Hash_Orig, Part, MaxDifficulty FROM Arrangements WHERE ID = " + ArrangID;

                    InsertIntoDBwValues("Arrangements", insertcmdd, insertvalues, cnb, 0);

                    if (!File.Exists(newXMLFilePath + ".old2")) File.Copy(newXMLFilePath, newXMLFilePath + ".old2", true);
                    //else File.Copy(XMLFilePath, XMLFilePath + ".old", true);
                    File.Copy(XMLFilePath, newXMLFilePath, true);
                }
                else newXMLFilePath = XMLFilePath;

                try
                {
                    if (File.Exists(newXMLFilePath + ".old3")) File.Copy(newXMLFilePath + ".old3", newXMLFilePath, true);
                    //else File.Copy(newXMLFilePath, newXMLFilePath + ".old3", true);
                    Song2014 xmlContent = null;
                    if (XMLFilePath != "")
                        try
                        {
                            xmlContent = Song2014.LoadFromFile(newXMLFilePath);
                            xmlContent.SongLength += s2s;
                            for (var j = 0; j < xmlContent.PhraseIterations.Length; j++)
                                if (xmlContent.PhraseIterations[j].Time > 0) xmlContent.PhraseIterations[j].Time += s2s;
                            for (var j = 0; j < xmlContent.Ebeats.Length; j++)
                                if (xmlContent.Ebeats[j].Time > 0) xmlContent.Ebeats[j].Time += s2s;
                            for (var j = 0; j < xmlContent.Sections.Length; j++)
                                if (xmlContent.Sections[j].StartTime > 0) xmlContent.Sections[j].StartTime += s2s;
                            for (var j = 0; j < xmlContent.Levels.Length; j++)
                            {
                                for (var k = 0; k < xmlContent.Levels[j].Notes.Length; k++)
                                    if (xmlContent.Levels[j].Notes[k].Time > 0) xmlContent.Levels[j].Notes[k].Time += s2s;
                                for (var k = 0; k < xmlContent.Levels[j].Anchors.Length; k++)
                                    if (xmlContent.Levels[j].Anchors[k].Time > 0) xmlContent.Levels[j].Anchors[k].Time += s2s;
                            }
                        }
                        catch (Exception ex)
                        {
                            var tsst = "Error shifting timings..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                        }

                    using (var stream = File.Open(newXMLFilePath, FileMode.Create)) xmlContent.Serialize(stream);
                    //if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Bass") chbx_Bass.Checked = true;
                    //else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Lead") chbx_Lead.Checked = true;
                    //else if (dus.Tables[0].Rows[0].ItemArray[2].ToString() == "Rhythm") chbx_Rhythm.Checked = true;
                    //? RouteMask.Bass : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Lead" ? RouteMask.Lead : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "None" ? RouteMask.None : RouteMask.Any;
                    //                
                }
                catch (Exception ex)
                {
                    var tsst = "Error ..." + ex; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                }

            }



            return;
        }

        private void ArrangementsDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chbx_AutoSave.Checked && txt_CDLC_ID.Text != "" && txt_CDLC_ID.Text != null) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }
    }
}