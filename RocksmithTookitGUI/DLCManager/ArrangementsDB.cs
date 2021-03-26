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
        DateTime timestamp;
        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "ArrangementsDB";
        int i=0;

        //bcapi
        public string DB_Path = "";
        public bool onceTunning = true;
        public bool onceTone = true;
        public bool onceToneA = true;
        public bool onceToneB = true;
        public bool onceToneC = true;
        public bool onceToneD = true;
        public string CDLCID = "";
        public bool BassDD;
        public int noOfRec = 0;
        public DataSet dssx = new DataSet();
        public bool SaveOK = false;
        public OleDbConnection cnb;
        //DateTime timestamp;
        string logPath = c("dlcm_LogPath") == "" ? c("dlcm_TempPath") + "\\0_log" : c("dlcm_LogPath");
        string tmpPath = c("dlcm_TempPath");

        private void ArrangementsDB_Load(object sender, EventArgs e)
        {
            var startT = DateTime.Now.ToString("yyyyMMdd HHmmssfff");
            var Log_PSPath = c("dlcm_TempPath") + "\\0_log";
            var fnl = (logPath == null || !Directory.Exists(logPath) ? c("dlcm_TempPath") + "\\0_log" : logPath) + "\\" + "current_arangtemp.txt";

            var starttmp = DateTime.Now;
            if (File.Exists((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_arangtemp.txt"))
            {
                File.Copy((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_arangtemp.txt"
                      , (logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_arangtemp" + startT + ".txt", true);
                FileStream swt = File.Open((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_arangtemp.txt", FileMode.Create);
                swt.Dispose();
            }
            else
            {
                FileStream swt = File.Open((logPath == null || !Directory.Exists(logPath) ? Log_PSPath : logPath) + "\\" + "current_maindbtemp.txt", FileMode.Create);
                swt.Dispose();
            }
            var tst = "Starting... " + startT; timestamp = UpdateLog(starttmp, tst, false, c("dlcm_TempPath"), "", "ArangDB", pB_ReadDLCs, null);

            Populate(ref databox, ref Main);//, ref bsPositions, ref bsBadges);
            databox.EditingControlShowing += DataGridView1_EditingControlShowing;
        }
        private void loadTones()
        {

            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT  ToneBase, ID, CDLC_ID, RouteMask FROM Arrangements;", "", cnb);
            var norec = dxs.Tables[0].Rows.Count;//DISTINCT

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
                pB_ReadDLCs.Value = 0;
                pB_ReadDLCs.Maximum = norec;
                for (int j = 0; j < norec; j++)
                {
                    var tr = dxs.Tables[0].Rows[j][0].ToString() + " - " + dxs.Tables[0].Rows[j][1].ToString() + "" + dxs.Tables[0].Rows[j][2].ToString() + "" + dxs.Tables[0].Rows[j][3].ToString();
                    chbx_ToneBase.Items.Add(tr);
                    pB_ReadDLCs.CreateGraphics().DrawString(tr, new Font("Arial", 7, FontStyle.Bold), Brushes.Blue, new PointF(1, pB_ReadDLCs.Height / 4));
                    pB_ReadDLCs.Value++;
                }
            }
        }

        private void loadTunnings()
        {
            DataSet dks = new DataSet(); dks = SelectFromDB("Arrangements", "SELECT DISTINCT Tunning FROM Arrangements;", "", cnb);
            var norec = dks.Tables[0].Rows.Count;

            if (norec > 0)
            {
                //remove items
                if (chbx_Tunning.Items.Count > 0)
                {
                    chbx_Tunning.DataSource = null;
                    for (int k = chbx_Tunning.Items.Count - 1; k >= 0; --k)
                        chbx_Tunning.Items.RemoveAt(k);
                }
                //add items
                for (int j = 0; j < norec; j++)
                    chbx_Tunning.Items.Add(dks.Tables[0].Rows[j][0].ToString());
            }
        }

        private void loadToneA()
        {
            DataSet ds = new DataSet(); ds = SelectFromDB("Arrangements", "SELECT DISTINCT ToneA FROM Arrangements;", "", cnb);
            var norec = ds.Tables[0].Rows.Count;

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
        }

        private void loadToneB()
        {
            DataSet dIs = new DataSet(); dIs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneB FROM Arrangements;", "", cnb);
            var norec = dIs.Tables[0].Rows.Count;

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
        }

        private void loadToneC()
        {
            DataSet dfs = new DataSet(); dfs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneC FROM Arrangements;", "", cnb);
            var norec = dfs.Tables[0].Rows.Count;

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
        }
        private void loadToneD()
        {
            DataSet dHs = new DataSet(); dHs = SelectFromDB("Arrangements", "SELECT DISTINCT ToneD FROM Arrangements;", "", cnb);
            var norec = dHs.Tables[0].Rows.Count;

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

        }

        public void Populate(ref DataGridView databox, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
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
            //        MessageBox.Show("-DB Open in Design Mode, or Missing, or You need to Download Connectivity patch 32/64 bit to match your version of Office @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
            //        ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode, or Missing, or You need to Download Connectivity patch 32/64 bit to match your version of Office @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
            //        frm1.ShowDialog();
            //        return;
            //    }
            //cn.Dispose();
            var ft = "SELECT " + c("dlcm_ArangementFields") + " FROM Arrangements WHERE CDLC_ID=" + CDLCID + ";";
            dssx = SelectFromDB("Arrangements", ft , "", cnb);
            noOfRec = dssx.Tables[0].Rows.Count;
            lbl_NoRec.Text = noOfRec.ToString() + " records.";
            //}
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID " };
            DataGridViewTextBoxColumn Arrangement_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Arrangement_Name", HeaderText = "Arrangement_Name " };
            DataGridViewTextBoxColumn CDLC_ID = new DataGridViewTextBoxColumn { DataPropertyName = "CDLC_ID", HeaderText = "CDLC_ID " };
            DataGridViewTextBoxColumn Bonus = new DataGridViewTextBoxColumn { DataPropertyName = "Bonus", HeaderText = "Bonus " };
            DataGridViewTextBoxColumn JSONFilePath = new DataGridViewTextBoxColumn { DataPropertyName = "JSONFilePath", HeaderText = "JSONFilePath " };
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
            DataGridViewTextBoxColumn ConversionDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "ConversionDateTime", HeaderText = "ConversionDateTime " };
            DataGridViewTextBoxColumn SNGFileHash = new DataGridViewTextBoxColumn { DataPropertyName = "SNGFileHash", HeaderText = "SNGFileHash " };
            DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
            DataGridViewTextBoxColumn Start_Time = new DataGridViewTextBoxColumn { DataPropertyName = "Start_Time", HeaderText = "Start_Time " };
            DataGridViewTextBoxColumn CleanedXML_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "CleanedXML_Hash", HeaderText = "CleanedXML_Hash " };
            DataGridViewTextBoxColumn Json_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Json_Hash", HeaderText = "Json_Hash " };
            DataGridViewTextBoxColumn Part = new DataGridViewTextBoxColumn { DataPropertyName = "Part", HeaderText = "Part " };
            DataGridViewTextBoxColumn MaxDifficulty = new DataGridViewTextBoxColumn { DataPropertyName = "MaxDifficulty", HeaderText = "MaxDifficulty " };
            DataGridViewTextBoxColumn NoSections = new DataGridViewTextBoxColumn { DataPropertyName = "NoSections", HeaderText = "NoSections " };
            DataGridViewTextBoxColumn OrigSongTrack = new DataGridViewTextBoxColumn { DataPropertyName = "OrigSongTrack", HeaderText = "OrigSongTrack " };
            DataGridViewTextBoxColumn PrimaryTrack = new DataGridViewTextBoxColumn { DataPropertyName = "PrimaryTrack", HeaderText = "PrimaryTrack " };
            DataGridViewTextBoxColumn Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Broken", HeaderText = "Broken " };
            DataGridViewTextBoxColumn Favorite = new DataGridViewTextBoxColumn { DataPropertyName = "Favorite", HeaderText = "Favorite " };

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
            //    JSONFilePath,
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
            //    ConversionDateTime,
            //    SNGFileHash
            //}
            //);

            dssx.Tables["Arrangements"].AcceptChanges();

            bs.DataSource = dssx.Tables["Arrangements"];
            databox.DataSource = bs;
            //DataGridView.ExpandColumns();

            //advance or step back in the song list
            //int i = 0;
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
            ChangeRow();
        }

        private class Files
        {
            public string ID { get; set; }
            public string Arrangement_Name { get; set; }
            public string CDLC_ID { get; set; }
            public string Bonus { get; set; }
            public string JSONFilePath { get; set; }
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
        }

        private Files[] files = new Files[10000];
        //Generic procedure to read and parse Main.DB (&others..soon)
        //public int SQLAccess(string cmd)
        //{
        //    //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
        //    //Files[] files = new Files[10000];

        //    var MaximumSize = 0;

        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
        //    //try
        //    //{
        //    //    //MessageBox.Show(DB_Path);
        //    //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
        //    //    {
        //    //        DataSet dus = new DataSet();
        //    //        OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
        //    //        dax.Fill(dus, "Arrangements");
        //    DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", cmd, "", cnb);
        //    //var i = 0;
        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
        //    MaximumSize = dus.Tables[0].Rows.Count;
        //    foreach (DataRow dataRow in dus.Tables[0].Rows)
        //    {
        //        files[i] = new Files();

        //        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
        //        files[i].ID = dataRow.ItemArray[0].ToString();
        //        files[i].Arrangement_Name = dataRow.ItemArray[1].ToString();
        //        files[i].CDLC_ID = dataRow.ItemArray[2].ToString();
        //        files[i].Bonus = dataRow.ItemArray[3].ToString();
        //        files[i].JSONFilePath = dataRow.ItemArray[5].ToString();
        //        files[i].XMLFilePath = dataRow.ItemArray[6].ToString();
        //        files[i].XMLFile_Hash = dataRow.ItemArray[7].ToString();
        //        files[i].ScrollSpeed = dataRow.ItemArray[8].ToString();
        //        files[i].Tunning = dataRow.ItemArray[9].ToString();
        //        files[i].Rating = dataRow.ItemArray[10].ToString();
        //        files[i].PlaythroughYBLink = dataRow.ItemArray[11].ToString();
        //        files[i].CustomsForge_Link = dataRow.ItemArray[12].ToString();
        //        files[i].ArrangementSort = dataRow.ItemArray[13].ToString();
        //        files[i].TuningPitch = dataRow.ItemArray[14].ToString();
        //        files[i].ToneBase = dataRow.ItemArray[15].ToString();
        //        files[i].Idd = dataRow.ItemArray[16].ToString();
        //        files[i].MasterId = dataRow.ItemArray[17].ToString();
        //        files[i].ArrangementType = dataRow.ItemArray[18].ToString();
        //        files[i].String0 = dataRow.ItemArray[19].ToString();
        //        files[i].String1 = dataRow.ItemArray[20].ToString();
        //        files[i].String2 = dataRow.ItemArray[21].ToString();
        //        files[i].String3 = dataRow.ItemArray[22].ToString();
        //        files[i].String4 = dataRow.ItemArray[23].ToString();
        //        files[i].String5 = dataRow.ItemArray[24].ToString();
        //        files[i].PluckedType = dataRow.ItemArray[25].ToString();
        //        files[i].RouteMask = dataRow.ItemArray[26].ToString();
        //        files[i].XMLFileName = dataRow.ItemArray[27].ToString();
        //        files[i].XMLFileLLID = dataRow.ItemArray[28].ToString();
        //        files[i].XMLFileUUID = dataRow.ItemArray[29].ToString();
        //        files[i].SNGFileName = dataRow.ItemArray[30].ToString();
        //        files[i].SNGFileLLID = dataRow.ItemArray[31].ToString();
        //        files[i].SNGFileUUID = dataRow.ItemArray[32].ToString();
        //        files[i].ToneMultiplayer = dataRow.ItemArray[33].ToString();
        //        files[i].ToneA = dataRow.ItemArray[34].ToString();
        //        files[i].ToneB = dataRow.ItemArray[35].ToString();
        //        files[i].ToneC = dataRow.ItemArray[36].ToString();
        //        files[i].ToneD = dataRow.ItemArray[37].ToString();
        //        files[i].ConversionDateTime = dataRow.ItemArray[38].ToString();
        //        files[i].SNGFileHash = dataRow.ItemArray[39].ToString();
        //        files[i].Has_Sections = dataRow.ItemArray[40].ToString();
        //        files[i].Comments = dataRow.ItemArray[41].ToString();
        //        files[i].Start_Time = dataRow.ItemArray[42].ToString();
        //        files[i].CleanedXML_Hash = dataRow.ItemArray[43].ToString();
        //        files[i].Json_Hash = dataRow.ItemArray[44].ToString();
        //        files[i].Part = dataRow.ItemArray[45].ToString();
        //        files[i].MaxDifficulty = dataRow.ItemArray[46].ToString();
        //        files[i].NoSections = dataRow.ItemArray[47].ToString();
        //        files[i].OrigSongTrack = dataRow.ItemArray[48].ToString();
        //        files[i].PrimaryTrack = dataRow.ItemArray[49].ToString();
        //        files[i].Favorite = dataRow.ItemArray[50].ToString();
        //        files[i].Broken = dataRow.ItemArray[51].ToString();
        //        i++;
        //    }
        //    //Closing Connection
        //    dus.Dispose();
        //    //        cnn.Close();
        //    //        //rtxt_StatisticsOnReadDLCs.Text += i;
        //    //        //var ex = 0;
        //    //    }
        //    //}
        //    //catch (System.IO.FileNotFoundException ee)
        //    //{
        //    //    MessageBox.Show(ee.Message + "Can not open Arrangements DB connection ! ");
        //    //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //}
        //    //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
        //    return MaximumSize;//files[10000];
        //}

        private void DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = databox.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            var norec = 0;
            //DataSet ds = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneA FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(ds, "Arrangements");

            //}
            //DataSet dIs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneB FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dIs, "Arrangements");

            //}
            //DataSet dfs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneC FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dfs, "Arrangements");

            //}
            //DataSet dHs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneD FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dHs, "Arrangements");

            //}


            //}

            //int i;
            if (databox.SelectedCells.Count > 0)
            {
                i = databox.SelectedCells[0].RowIndex;
                txt_ID.Text = databox.Rows[i].Cells["ID"].Value.ToString();
                txt_Arrangement_Name.Text = databox.Rows[i].Cells["Arrangement_Name"].Value.ToString();
                txt_CDLC_ID.Text = databox.Rows[i].Cells["CDLC_ID"].Value.ToString();
                txt_JSONFilePath.Text = databox.Rows[i].Cells["JSONFilePath"].Value.ToString();
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
                txt_lastConversionDateTime.Text = databox.Rows[i].Cells["ConversionDateTime"].Value.ToString();
                txt_Description.Text = databox.Rows[i].Cells["Comments"].Value.ToString();
                txt_StartTime.Text = databox.Rows[i].Cells["Start_Time"].Value.ToString();
                txt_Part.Text = databox.Rows[i].Cells["Part"].Value.ToString();
                txt_MaxDifficulty.Text = databox.Rows[i].Cells["MaxDifficulty"].Value.ToString();

                if (databox.Rows[i].Cells["Bonus"].Value.ToString().ToLower() == "true") chbx_Bonus.Checked = true;
                else chbx_Bonus.Checked = false;
                if (databox.Rows[i].Cells["OrigSongTrack"].Value.ToString() == "Yes") chbx_Default.Checked = true;
                else chbx_Default.Checked = false;
                if (databox.Rows[i].Cells["PrimaryTrack"].Value.ToString() == "Yes") chbx_Primary.Checked = true;
                else chbx_Primary.Checked = false;
                if (databox.Rows[i].Cells["Broken"].Value.ToString() == "Yes") chbx_Broken.Checked = true;
                else chbx_Broken.Checked = false;
                if (databox.Rows[i].Cells["Favorite"].Value.ToString() == "Yes") chbx_Favorite.Checked = true;
                else chbx_Favorite.Checked = false;
                var f = databox.Rows[i].Cells["Has_Sections"].Value.ToString();
                if (databox.Rows[i].Cells["Has_Sections"].Value.ToString().IndexOf("Yes") >= 0)
                {
                    if (databox.Rows[i].Cells["Has_Sections"].Value.ToString().Length == 3) chbx_HasSection.Checked = true;/*== "Yes"*/
                }
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

        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void SaveRecord()
        {
            //int i;
            DataSet dis = new DataSet();

            if (databox.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                //i = databox.SelectedCells[0].RowIndex;

                databox.Rows[i].Cells["ID"].Value = txt_ID.Text;
                databox.Rows[i].Cells["Arrangement_Name"].Value = txt_Arrangement_Name.Text;
                databox.Rows[i].Cells["CDLC_ID"].Value = txt_CDLC_ID.Text;
                databox.Rows[i].Cells["JSONFilePath"].Value = txt_JSONFilePath.Text;
                //DataGridView1.Rows[i].Cells["XMLFilePath"].Value = txt_XMLFilePath.Text;
                databox.Rows[i].Cells["ScrollSpeed"].Value = txt_ScrollSpeed.Text;
                databox.Rows[i].Cells["Tunning"].Value = chbx_Tunning.Text;
                databox.Rows[i].Cells["Rating"].Value = txt_Rating.Text;
                databox.Rows[i].Cells["TuningPitch"].Value = txt_TuningPitch.Text;
                databox.Rows[i].Cells["ToneBase"].Value = chbx_ToneBase.Text;//if (chbx_ToneBase.Text.Length > 0) .Substring(0, chbx_ToneBase.Text.IndexOf(" - "));
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
                databox.Rows[i].Cells["ConversionDateTime"].Value = txt_lastConversionDateTime.Text;
                databox.Rows[i].Cells["Comments"].Value = txt_Description.Text;
                if (databox.Rows[i].Cells["Has_Sections"].Value.ToString().Length < 4) databox.Rows[i].Cells["Has_Sections"].Value = chbx_HasSection.Checked ? "Yes" : "No";
                databox.Rows[i].Cells["Start_Time"].Value = txt_StartTime.Text;
                databox.Rows[i].Cells["Part"].Value = txt_Part.Text;
                databox.Rows[i].Cells["MaxDifficulty"].Value = txt_MaxDifficulty.Text;
                if (chbx_Default.Checked) databox.Rows[i].Cells["OrigSongTrack"].Value = "Yes";
                else databox.Rows[i].Cells["OrigSongTrack"].Value = "No";
                if (chbx_Primary.Checked) databox.Rows[i].Cells["PrimaryTrack"].Value = "Yes";
                else databox.Rows[i].Cells["PrimaryTrack"].Value = "No";
                if (chbx_Broken.Checked) databox.Rows[i].Cells["Broken"].Value = "Yes";
                else databox.Rows[i].Cells["Broken"].Value = "No";
                if (chbx_Favorite.Checked) databox.Rows[i].Cells["Favorite"].Value = "Yes";
                else databox.Rows[i].Cells["Favorite"].Value = "No";
                if (chbx_Bonus.Checked) databox.Rows[i].Cells["Bonus"].Value = "True";
                else databox.Rows[i].Cells["Bonus"].Value = "False";


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
                    command.CommandText += "JSONFilePath = @param4, ";
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
                    command.CommandText += "ConversionDateTime = @param37, ";
                    //command.CommandText += "SNGFileHash = @param38 ";
                    //command.CommandText += "Has_Sections = @param39 ";
                    command.CommandText += "Comments = @param38, ";
                    //command.CommandText += "Start_Time = @param41, ";
                    //command.CommandText += "Part = @param42, ";
                    //command.CommandText += "MaxDifficulty = @param43 ";
                    command.CommandText += "OrigSongTrack = @param39, ";
                    command.CommandText += "PrimaryTrack = @param40, ";
                    command.CommandText += "Broken = @param41, ";
                    command.CommandText += "Favorite = @param42 ";
                    //command.CommandText += "Bonus = @param43 ";

                    command.CommandText += " WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", databox.Rows[i].Cells["Arrangement_Name"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param2", databox.Rows[i].Cells["CDLC_ID"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param3", databox.Rows[i].Cells["Bonus"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param4", databox.Rows[i].Cells["JSONFilePath"].Value.ToString() ?? DBNull.Value.ToString());
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
                    command.Parameters.AddWithValue("@param37", databox.Rows[i].Cells["ConversionDateTime"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param38", databox.Rows[i].Cells["Comments"].Value.ToString() ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param41", databox.Rows[i].Cells["Start_Time"].Value.ToString() ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param42", databox.Rows[i].Cells["Part"].Value.ToString() ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param43", databox.Rows[i].Cells["MaxDifficulty"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param39", databox.Rows[i].Cells["OrigSongTrack"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param40", databox.Rows[i].Cells["PrimaryTrack"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param41", databox.Rows[i].Cells["Broken"].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param42", databox.Rows[i].Cells["Favorite"].Value.ToString() ?? DBNull.Value.ToString());
                    //command.Parameters.AddWithValue("@param43", databox.Rows[i].Cells["Bonus"].Value.ToString() ?? DBNull.Value.ToString());
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

        }

        private void Bth_ShiftVocalNotes_Click(object sender, EventArgs e)
        {
        }

        private void ArrangementsDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chbx_AutoSave.Checked && txt_CDLC_ID.Text != "" && txt_CDLC_ID.Text != null) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void chbx_ToneBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            var CDLCID = chbx_ToneBase.Text.Substring(chbx_ToneBase.Text.IndexOf(" - ") + 3, chbx_ToneBase.Text.Length - chbx_ToneBase.Text.IndexOf(" - ") + 3);
            CDLCID = CDLCID.Substring(0, CDLCID.IndexOf(" - "));
            DataSet dxs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT JSONFilePath FROM Arrangements where CDLC_ID=" + CDLCID + ";", "", cnb);
            noOfRec = dxs.Tables.Count <= 0 ? 0 : dxs.Tables[0].Rows.Count;
            //var i = databox.SelectedCells[0].RowIndex;
            DataSet dfs = new DataSet(); dxs = SelectFromDB("Arrangements", "SELECT JSONFilePath FROM Arrangements where JSONFilePath<>'' and CDLC_ID=" + txt_CDLC_ID.Text + ";", "", cnb);
            var noOfRecs = dfs.Tables.Count <= 0 ? 0 : dfs.Tables[0].Rows.Count;
            string destination_dir = Path.GetDirectoryName(txt_XMLFilePath.Text);
            if (noOfRecs > 0) destination_dir = Path.GetDirectoryName(dfs.Tables[0].Rows[0].ItemArray[0].ToString());// databox.Rows[i].Cells["JSONFilePath"].Value.ToString();
            if (noOfRec > 0)
            {
                string json = dxs.Tables[0].Rows[0].ItemArray[0].ToString();
                var newJSONFilePath = destination_dir + "\\" + Path.GetFileName(json);
                if (!File.Exists(newJSONFilePath + ".old2") && File.Exists(newJSONFilePath)) File.Copy(newJSONFilePath, newJSONFilePath + ".old2", true);
                File.Copy(json, newJSONFilePath, true);
                txt_JSONFilePath.Text = newJSONFilePath;
            }
            chbx_ToneBase.Text = chbx_ToneBase.Text.Substring(0, chbx_ToneBase.Text.IndexOf(" - "));
        }

        private void chbx_Tunning_DropDown(object sender, EventArgs e)
        {
            if (onceTunning) loadTunnings();
            onceTunning = false;
        }

        private void chbx_ToneBase_DropDown(object sender, EventArgs e)
        {
            if (onceTone) loadTones();
            onceTone = false;
        }

        private void chbx_ToneA_DropDown(object sender, EventArgs e)
        {
            if (onceToneA) loadToneA();
            onceToneA = false;
        }

        private void chbx_ToneB_DropDown(object sender, EventArgs e)
        {
            if (onceToneB) loadToneB();
            onceToneB = false;
        }

        private void chbx_ToneC_DropDown(object sender, EventArgs e)
        {
            if (onceToneC) loadToneC();
            onceToneC = false;
        }

        private void chbx_ToneD_DropDown(object sender, EventArgs e)
        {
            if (onceToneD) loadToneD();
            onceToneD = false;
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void databox_SelectionChanged(object sender, EventArgs e)
        {
            //i = databox.SelectedCells[0].RowIndex;
            if (txt_CDLC_ID.Text != "") ChangeRow();
        }

        private void btn_OpenJSON_Click(object sender, EventArgs e)
        {
            var fileName = txt_JSONFilePath.Text;

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

        private void btn_OpenDB_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = Process.Start(@c("dlcm_DBFolder"));
            }
            catch (Exception ex)
            {
                var tsst = "Error ..." + ex; timestamp = UpdateLog(timestamp, tsst, false, c("dlcm_TempPath"), "", "", null, null);
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + c("dlcm_DBFolder"));
            }
        }

        private void bth_ShiftVocalNotes_Click(object sender, EventArgs e)
        {

            if (cmb_Tracks.Text.IndexOf("Vocals") >= 0)
            {
                var SongID = databox.SelectedCells[0].RowIndex;
                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, ArrangementType, RouteMask, Start_Time FROM Arrangements WHERE CDLC_ID=" + SongID + "", "", cnb);
                //var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var noOfRec = dus.Tables[0].Rows.Count;
                var XMLFilePath = "";
                for (var ii = 0; ii <= noOfRec - 1; ii++)
                {
                    var ArrangementType = dus.Tables[0].Rows[ii].ItemArray[1].ToString();
                    //var RouteMask = dus.Tables[0].Rows[i].ItemArray[2].ToString();
                    if (ArrangementType == "Vocal") XMLFilePath = dus.Tables[0].Rows[ii].ItemArray[0].ToString();/* : XMLFilePath;*/
                }

                Vocals xmlContent = null;
                if (XMLFilePath != "")
                    try
                    {
                        xmlContent = Vocals.LoadFromFile(XMLFilePath);
                        for (var j = 0; j < xmlContent.Vocal.Length; j++)
                            xmlContent.Vocal[j].Time = xmlContent.Vocal[j].Time + float.Parse(num_Lyrics.Value.ToString());
                    }
                    catch (Exception ex) { var tsst = "Error ..." + ex; UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null); }

                using (var stream = File.Open(XMLFilePath, FileMode.Create))
                    xmlContent.Serialize(stream);
                //chbx_Lyrics.Checked = true;
            }
            else
            {
                var ArrangID = cmb_Tracks.Text.Substring(cmb_Tracks.Text.IndexOf("_ArrangementID="), cmb_Tracks.Text.Length - cmb_Tracks.Text.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
                var DLCID = cmb_Tracks.Text.Substring(8, cmb_Tracks.Text.IndexOf("_") - 8);

                //var i = databox.SelectedCells[0].RowIndex;
                string destination_dir = databox.Rows[i].Cells["Folder_Name"].Value.ToString();//pending to develop + (txt_Platform.Text.ToLower() == "XBOX360".ToLower() ? "\\Root" : "") +

                DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName, RouteMask, Start_Time FROM Arrangements WHERE ID=" + ArrangID + "", "", cnb);
                var noOfRec = dus.Tables[0].Rows.Count;//var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
                var newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
                var s2s = float.Parse(num_Lyrics.Value.ToString());

                if (txt_ID.Text != DLCID)
                {
                    var insertcmdd = "Arrangement_Name, CDLC_ID, Bonus, JSONFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link," +
                        "ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask," +
                        "XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime," +
                        "SNGFileHash, Has_Sections, Comments, Start_Time, CleanedXML_Hash, Json_Hash, Part, MaxDifficulty, NoSections,OrigSongTrack, PrimaryTrack, Favorite, Broken";
                    var insertvalues = "SELECT Arrangement_Name, " + txt_ID.Text + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(JSONFilePath,len(JSONFilePath)-" +
                        "instr(JSONFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10)," +
                        " XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlaythroughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType," +
                        " String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID," +
                        " SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, ConversionDateTime, SNGFileHash, Has_Sections, \"added from" + txt_CDLC_ID.Text + " " + txt_XMLFilePath.Text + "\", Start_Time, CleanedXML_Hash," +
                        " Json_Hash, Part, MaxDifficulty, \"0\", \"Yes\", \"\", Favorite, Broken FROM Arrangements WHERE ID = " + ArrangID;

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
                            var tsst = "Error shifting timings..." + ex; UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
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
                    var tsst = "Error ..." + ex; UpdateLog(timestamp, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "", null, null);
                }

            }
            return;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            //Delete Arangements
            DeleteFromDB("Arrangements", "DELETE * FROM Arrangements WHERE ID IN (" + txt_ID.Text + ")", cnb);
            Populate(ref databox, ref Main);
        }

        private void btn_RemoveDD_Click(object sender, EventArgs e)
        {
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

        private void chbx_BassPicking_DropDown(object sender, EventArgs e)
        {

        }

        private void chbx_ToneBase_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}