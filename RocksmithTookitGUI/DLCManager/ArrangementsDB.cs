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

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class ArrangementsDB : Form
    {
        public ArrangementsDB(string txt_DBFolder)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION;
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public DataSet dssx = new DataSet();
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void ArrangementsDB_Load(object sender, EventArgs e)
        {
            //DataAccess da = new DataAccess();
            //MessageBox.Show("test0");
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
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
           // DB_Path = DB_Path + "\\Files.accdb"; //DLCManager.txt_DBFolder.Text
            try
            {
                Process process = Process.Start("msaccess.exe " + DB_Path + "");
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
            int i;
            DataSet dis = new DataSet();

            i = DataGridView1.SelectedCells[0].RowIndex;

            DataGridView1.Rows[i].Cells[0].Value = txt_ID.Text;
            DataGridView1.Rows[i].Cells[1].Value = txt_Arrangement_Name.Text;
            DataGridView1.Rows[i].Cells[2].Value = txt_CDLC_ID.Text;
            //DataGridView1.Rows[i].Cells[5].Value = txt_SNGFilePath.Text;
            //DataGridView1.Rows[i].Cells[6].Value = txt_XMLFilePath.Text;
            DataGridView1.Rows[i].Cells[7].Value = txt_ScrollSpeed.Text;
            DataGridView1.Rows[i].Cells[8].Value = txt_Tunning.Text;
            DataGridView1.Rows[i].Cells[9].Value = txt_Rating.Text;
            DataGridView1.Rows[i].Cells[13].Value = txt_TuningPitch.Text;
            DataGridView1.Rows[i].Cells[14].Value = txt_ToneBase.Text;
            //DataGridView1.Rows[i].Cells[15].Value = txt_Idd.Text;
            DataGridView1.Rows[i].Cells[17].Value = txt_ArrangementType.Text;
            DataGridView1.Rows[i].Cells[18].Value = txt_String0.Text;
            DataGridView1.Rows[i].Cells[19].Value = txt_String1.Text;
            DataGridView1.Rows[i].Cells[20].Value = txt_String2.Text;
            DataGridView1.Rows[i].Cells[21].Value = txt_String3.Text;
            DataGridView1.Rows[i].Cells[22].Value = txt_String4.Text;
            DataGridView1.Rows[i].Cells[23].Value = txt_String5.Text;
            DataGridView1.Rows[i].Cells[24].Value = txt_BassPicking.Text;
            DataGridView1.Rows[i].Cells[25].Value = txt_RouteMask.Text;
            DataGridView1.Rows[i].Cells[33].Value = chbx_ToneA.Text;
            DataGridView1.Rows[i].Cells[34].Value = chbx_ToneB.Text;
            DataGridView1.Rows[i].Cells[35].Value = chbx_ToneC.Text;
            DataGridView1.Rows[i].Cells[36].Value = chbx_ToneD.Text;
            DataGridView1.Rows[i].Cells[37].Value = txt_lastConversionDateTime.Text;
            if (chbx_Bonus.Checked) DataGridView1.Rows[i].Cells[3].Value = "Yes";
            else DataGridView1.Rows[i].Cells[3].Value = "No";

            //var DB_Path = "../../../../tmp\\Files.accdb;";
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                //OleDbCommand command = new OleDbCommand(); ;
                //Update MainDB
                //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                command.CommandText = "UPDATE Arrangements SET ";

                command.CommandText += "ID = @param0, ";
                command.CommandText += "Arrangement_Name = @param1, ";
                command.CommandText += "CDLC_ID = @param2, ";
                command.CommandText += "Bonus = @param3, ";
                //command.CommandText += "SNGFilePath = @param4, ";
                //command.CommandText += "XMLFilePath = @param5, ";
                //command.CommandText += "XMLFile_Hash = @param6, ";
                command.CommandText += "ScrollSpeed = @param7, ";
                command.CommandText += "Tunning = @param8, ";
                command.CommandText += "Rating = @param9, ";
                //command.CommandText += "PlayThoughYBLink = @param10, ";
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
                command.CommandText += "lastConversionDateTime = @param37 ";
                //command.CommandText += "SNGFileHash = @param38 ";

                command.CommandText += "WHERE ID = " + txt_ID.Text;

                command.Parameters.AddWithValue("@param1", DataGridView1.Rows[i].Cells[1].Value.ToString());
                command.Parameters.AddWithValue("@param2", DataGridView1.Rows[i].Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@param7", DataGridView1.Rows[i].Cells[7].Value.ToString());
                command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString());
                command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[9].Value.ToString());
                command.Parameters.AddWithValue("@param13", DataGridView1.Rows[i].Cells[13].Value.ToString());
                command.Parameters.AddWithValue("@param14", DataGridView1.Rows[i].Cells[14].Value.ToString());
                command.Parameters.AddWithValue("@param17", DataGridView1.Rows[i].Cells[17].Value.ToString());
                command.Parameters.AddWithValue("@param18", DataGridView1.Rows[i].Cells[18].Value.ToString());
                command.Parameters.AddWithValue("@param19", DataGridView1.Rows[i].Cells[19].Value.ToString());
                command.Parameters.AddWithValue("@param20", DataGridView1.Rows[i].Cells[20].Value.ToString());
                command.Parameters.AddWithValue("@param21", DataGridView1.Rows[i].Cells[21].Value.ToString());
                command.Parameters.AddWithValue("@param22", DataGridView1.Rows[i].Cells[22].Value.ToString());
                command.Parameters.AddWithValue("@param23", DataGridView1.Rows[i].Cells[23].Value.ToString());
                command.Parameters.AddWithValue("@param24", DataGridView1.Rows[i].Cells[24].Value.ToString());
                command.Parameters.AddWithValue("@param25", DataGridView1.Rows[i].Cells[25].Value.ToString());
                command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells[33].Value.ToString());
                command.Parameters.AddWithValue("@param34", DataGridView1.Rows[i].Cells[34].Value.ToString());
                command.Parameters.AddWithValue("@param35", DataGridView1.Rows[i].Cells[35].Value.ToString());
                command.Parameters.AddWithValue("@param36", DataGridView1.Rows[i].Cells[36].Value.ToString());
                command.Parameters.AddWithValue("@param37", DataGridView1.Rows[i].Cells[37].Value.ToString());
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

                    throw;
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
                ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                //MessageBox.Show(das.UpdateCommand.CommandText);
                //das.SelectCommand.CommandText = "SELECT * FROM Main";
                //// das.Update(dssx, "Main");
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {


            //DB_Path = "../../../../tmp\\Files.accdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Arrangements;", cn);
                da.Fill(dssx, "Arrangements");
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
            }
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
            DataGridViewTextBoxColumn PlayThoughYBLink = new DataGridViewTextBoxColumn { DataPropertyName = "PlayThoughYBLink", HeaderText = "PlayThoughYBLink " };
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
                Arrangement_Name,
                CDLC_ID,
                Bonus,
                SNGFilePath,
                XMLFilePath,
                XMLFile_Hash,
                ScrollSpeed,
                Tunning,
                Rating,
                PlayThoughYBLink,
                CustomsForge_Link,
                ArrangementSort,
                TuningPitch,
                ToneBase,
                Idd,
                MasterId,
                ArrangementType,
                String0,
                String1,
                String2,
                String3,
                String4,
                String5,
                PluckedType,
                RouteMask,
                XMLFileName,
                XMLFileLLID,
                XMLFileUUID,
                SNGFileName,
                SNGFileLLID,
                SNGFileUUID,
                ToneMultiplayer,
                ToneA,
                ToneB,
                ToneC,
                ToneD,
                lastConversionDateTime,
                SNGFileHash
            }
            );

            dssx.Tables["Arrangements"].AcceptChanges();

            bs.DataSource = dssx.Tables["Arrangements"];
            DataGridView.DataSource = bs;
            //DataGridView.ExpandColumns();
        }

        public class Files
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
            public string PlayThoughYBLink { get; set; }
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
                    dax.Fill(dus, "Arrangements");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                        files[i].ID = dataRow.ItemArray[0].ToString();
                        files[i].Arrangement_Name = dataRow.ItemArray[0].ToString();
                        files[i].CDLC_ID = dataRow.ItemArray[0].ToString();
                        files[i].Bonus = dataRow.ItemArray[0].ToString();
                        files[i].SNGFilePath = dataRow.ItemArray[0].ToString();
                        files[i].XMLFilePath = dataRow.ItemArray[0].ToString();
                        files[i].XMLFile_Hash = dataRow.ItemArray[0].ToString();
                        files[i].ScrollSpeed = dataRow.ItemArray[0].ToString();
                        files[i].Tunning = dataRow.ItemArray[0].ToString();
                        files[i].Rating = dataRow.ItemArray[0].ToString();
                        files[i].PlayThoughYBLink = dataRow.ItemArray[0].ToString();
                        files[i].CustomsForge_Link = dataRow.ItemArray[0].ToString();
                        files[i].ArrangementSort = dataRow.ItemArray[0].ToString();
                        files[i].TuningPitch = dataRow.ItemArray[0].ToString();
                        files[i].ToneBase = dataRow.ItemArray[0].ToString();
                        files[i].Idd = dataRow.ItemArray[0].ToString();
                        files[i].MasterId = dataRow.ItemArray[0].ToString();
                        files[i].ArrangementType = dataRow.ItemArray[0].ToString();
                        files[i].String0 = dataRow.ItemArray[0].ToString();
                        files[i].String1 = dataRow.ItemArray[0].ToString();
                        files[i].String2 = dataRow.ItemArray[0].ToString();
                        files[i].String3 = dataRow.ItemArray[0].ToString();
                        files[i].String4 = dataRow.ItemArray[0].ToString();
                        files[i].String5 = dataRow.ItemArray[0].ToString();
                        files[i].PluckedType = dataRow.ItemArray[0].ToString();
                        files[i].RouteMask = dataRow.ItemArray[0].ToString();
                        files[i].XMLFileName = dataRow.ItemArray[0].ToString();
                        files[i].XMLFileLLID = dataRow.ItemArray[0].ToString();
                        files[i].XMLFileUUID = dataRow.ItemArray[0].ToString();
                        files[i].SNGFileName = dataRow.ItemArray[0].ToString();
                        files[i].SNGFileLLID = dataRow.ItemArray[0].ToString();
                        files[i].SNGFileUUID = dataRow.ItemArray[0].ToString();
                        files[i].ToneMultiplayer = dataRow.ItemArray[0].ToString();
                        files[i].ToneA = dataRow.ItemArray[0].ToString();
                        files[i].ToneB = dataRow.ItemArray[0].ToString();
                        files[i].ToneC = dataRow.ItemArray[0].ToString();
                        files[i].ToneD = dataRow.ItemArray[0].ToString();
                        files[i].lastConversionDateTime = dataRow.ItemArray[0].ToString();
                        files[i].SNGFileHash = dataRow.ItemArray[0].ToString();
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
                MessageBox.Show(ee.Message + "Can not open Arrangements DB connection ! ");
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_Arrangement_Name.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_CDLC_ID.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            //txt_SNGFilePath.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            //txt_XMLFilePath.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
            txt_ScrollSpeed.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
            txt_Tunning.Text = DataGridView1.Rows[i].Cells[8].Value.ToString();
            txt_Rating.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
            txt_TuningPitch.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
            txt_ToneBase.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
            //txt_Idd.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
            txt_ArrangementType.Text = DataGridView1.Rows[i].Cells[17].Value.ToString();
            txt_String0.Text = DataGridView1.Rows[i].Cells[18].Value.ToString();
            txt_String1.Text = DataGridView1.Rows[i].Cells[19].Value.ToString();
            txt_String2.Text = DataGridView1.Rows[i].Cells[20].Value.ToString();
            txt_String3.Text = DataGridView1.Rows[i].Cells[21].Value.ToString();
            txt_String4.Text = DataGridView1.Rows[i].Cells[22].Value.ToString();
            txt_String5.Text = DataGridView1.Rows[i].Cells[23].Value.ToString();
            txt_BassPicking.Text = DataGridView1.Rows[i].Cells[24].Value.ToString();
            txt_RouteMask.Text = DataGridView1.Rows[i].Cells[25].Value.ToString();
            chbx_ToneA.Text = DataGridView1.Rows[i].Cells[33].Value.ToString();
            chbx_ToneB.Text = DataGridView1.Rows[i].Cells[34].Value.ToString();
            chbx_ToneC.Text = DataGridView1.Rows[i].Cells[35].Value.ToString();
            chbx_ToneD.Text = DataGridView1.Rows[i].Cells[36].Value.ToString();
            txt_lastConversionDateTime.Text = DataGridView1.Rows[i].Cells[37].Value.ToString();

            if (DataGridView1.Rows[i].Cells[3].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
            else chbx_Bonus.Checked = false;
        }
    }
} 