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
using RocksmithToolkitLib.Extensions; //dds
using Ookii.Dialogs; //cue text
using static RocksmithToolkitGUI.DLCManager.GenericFunctions;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class TonesDB : Form
    {
        public TonesDB(string txt_DBFolder, string CDLC_ID)
        {
            InitializeComponent();
            CDLCID = CDLC_ID;
            DB_Path = txt_DBFolder;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "TonesDB";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string CDLCID = "";
        public DataSet dssx = new DataSet();
        public bool SaveOK = false;
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void TonesDB_Load(object sender, EventArgs e)
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
                Process process = Process.Start(@DB_Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Tones DB connection in TonesDB ! " + DB_Path);
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = DataGridView1.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }
        public void ChangeRow()
        {

           // var norec = 0;
            //DataSet ds = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneA FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(ds, "Arrangements");
            //    norec = ds.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_ToneA.Items.Count > 0)
            //        {
            //            chbx_ToneA.DataSource = null;
            //            for (int k = chbx_ToneA.Items.Count - 1; k >= 0; --k)
            //            {
            //                if (!chbx_ToneA.Items[k].ToString().Contains("--"))
            //                {
            //                    chbx_ToneA.Items.RemoveAt(k);
            //                }
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_ToneA.Items.Add(ds.Tables[0].Rows[j][0].ToString());
            //    }
            //}
            //DataSet dIs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneB FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dIs, "Arrangements");
            //    norec = dIs.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_ToneB.Items.Count > 0)
            //        {
            //            chbx_ToneB.DataSource = null;
            //            for (int k = chbx_ToneB.Items.Count - 1; k >= 0; --k)
            //            {
            //                if (!chbx_ToneB.Items[k].ToString().Contains("--"))
            //                {
            //                    chbx_ToneB.Items.RemoveAt(k);
            //                }
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_ToneB.Items.Add(dIs.Tables[0].Rows[j][0].ToString());
            //    }
            //}
            //DataSet dfs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneC FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dfs, "Arrangements");
            //    norec = dfs.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_ToneC.Items.Count > 0)
            //        {
            //            chbx_ToneC.DataSource = null;
            //            for (int k = chbx_ToneC.Items.Count - 1; k >= 0; --k)
            //            {
            //                if (!chbx_ToneC.Items[k].ToString().Contains("--"))
            //                {
            //                    chbx_ToneC.Items.RemoveAt(k);
            //                }
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_ToneC.Items.Add(dfs.Tables[0].Rows[j][0].ToString());
            //    }
            //}
            //DataSet dHs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneD FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dHs, "Arrangements");
            //    norec = dHs.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_ToneD.Items.Count > 0)
            //        {
            //            chbx_ToneD.DataSource = null;
            //            for (int k = chbx_ToneD.Items.Count - 1; k >= 0; --k)
            //            {
            //                if (!chbx_ToneD.Items[k].ToString().Contains("--"))
            //                {
            //                    chbx_ToneD.Items.RemoveAt(k);
            //                }
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_ToneD.Items.Add(dHs.Tables[0].Rows[j][0].ToString());
            //    }
            //}
            //DataSet dxs = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT ToneBase FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dxs, "Arrangements");
            //    norec = dxs.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_ToneBase.Items.Count > 0)
            //        {
            //            chbx_ToneBase.DataSource = null;
            //            for (int k = chbx_ToneBase.Items.Count - 1; k >= 0; --k)
            //            {
            //                if (!chbx_ToneBase.Items[k].ToString().Contains("--"))
            //                {
            //                    chbx_ToneBase.Items.RemoveAt(k);
            //                }
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_ToneBase.Items.Add(dxs.Tables[0].Rows[j][0].ToString());
            //    }
            //}
            //DataSet dks = new DataSet();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    string SearchCmd = "SELECT DISTINCT Tunning FROM Arrangements;";
            //    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
            //    da.Fill(dks, "Arrangements");
            //    norec = dks.Tables[0].Rows.Count;

            //    if (norec > 0)
            //    {
            //        //remove items
            //        if (chbx_Tunning.Items.Count > 0)
            //        {
            //            chbx_Tunning.DataSource = null;
            //            for (int k = chbx_Tunning.Items.Count - 1; k >= 0; --k)
            //            {
            //                //if (!chbx_Tunning.Items[k].ToString().Contains("--"))
            //                //{
            //                chbx_Tunning.Items.RemoveAt(k);
            //                //}
            //            }
            //        }
            //        //add items
            //        for (int j = 0; j < norec; j++)
            //            chbx_Tunning.Items.Add(dks.Tables[0].Rows[j][0].ToString());
            //    }
            //}

            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_ID.Text = DataGridView1.Rows[i].Cells["ID"].Value.ToString();
            txt_Tone_Name.Text = DataGridView1.Rows[i].Cells["Tone_Name"].Value.ToString();
            txt_CDLC_ID.Text = DataGridView1.Rows[i].Cells["CDLC_ID"].Value.ToString();
            txt_Volume.Text = DataGridView1.Rows[i].Cells["Volume"].Value.ToString();
            txt_Keyy.Text = DataGridView1.Rows[i].Cells["Keyy"].Value.ToString();
            //txt_GearList.Text = DataGridView1.Rows[i].Cells["GearList"].Value.ToString();
            //txt_AmpRack.Text = DataGridView1.Rows[i].Cells["AmpRack"].Value.ToString();
            txt_Description.Text = DataGridView1.Rows[i].Cells["Description"].Value.ToString();
            //txt_TuningPitch.Text = DataGridView1.Rows[i].Cells["TuningPitch"].Value.ToString();
            //chbx_ToneBase.Text = DataGridView1.Rows[i].Cells["ToneBase"].Value.ToString();
            ////txt_Idd.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
            //txt_ArrangementType.Text = DataGridView1.Rows[i].Cells["ArrangementType"].Value.ToString();
            chbx_AmpType.Text = DataGridView1.Rows[i].Cells["AmpType"].Value.ToString();
            chbx_AmpCategory.Text = DataGridView1.Rows[i].Cells["AmpCategory"].Value.ToString();
            chbx_AmpKnobValues.Text = DataGridView1.Rows[i].Cells["AmpKnobValues"].Value.ToString();
            chbx_AmpPedalKey.Text = DataGridView1.Rows[i].Cells["AmpPedalKey"].Value.ToString();
            chbx_CabinetCategory.Text = DataGridView1.Rows[i].Cells["CabinetCategory"].Value.ToString();
            chbx_CabinetKnobValues.Text = DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value.ToString();
            chbx_CabinetPedalKey.Text = DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value.ToString();
            chbx_CabinetType.Text = DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString();
            //chbx_ToneA.Text = DataGridView1.Rows[i].Cells["ToneA"].Value.ToString();
            //chbx_ToneB.Text = DataGridView1.Rows[i].Cells["ToneB"].Value.ToString();
            //chbx_ToneC.Text = DataGridView1.Rows[i].Cells["ToneC"].Value.ToString();
            //chbx_ToneD.Text = DataGridView1.Rows[i].Cells["ToneD"].Value.ToString();
            //txt_lastConversionDateTime.Text = DataGridView1.Rows[i].Cells["lastConversionDateTime"].Value.ToString();
            //txt_Description.Text = DataGridView1.Rows[i].Cells["Comments"].Value.ToString();

            //if (DataGridView1.Rows[i].Cells["GearList"].Value.ToString() == "Yes") chbx_GearList.Checked = true;
            //else chbx_GearList.Checked = false;
            if (DataGridView1.Rows[i].Cells["Is_Custom"].Value.ToString() == "True") chbx_Custom.Checked = true;
            else chbx_Custom.Checked = false;

            //if (txt_ArrangementType.Text == "Bass" && !(chbx_BassDD.Checked)) btn_AddDD.Enabled = true;
            //if (txt_ArrangementType.Text == "Bass" && chbx_BassDD.Checked) btn_RemoveDD.Enabled = true;

            if (chbx_AutoSave.Checked) SaveOK = true;
            else SaveOK = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        public void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();

            i = DataGridView1.SelectedCells[0].RowIndex;

            DataGridView1.Rows[i].Cells[0].Value = txt_ID.Text;
            DataGridView1.Rows[i].Cells["CDLC_ID"].Value = txt_CDLC_ID.Text;
            DataGridView1.Rows[i].Cells["Volume"].Value = txt_Volume.Text;
            DataGridView1.Rows[i].Cells["Keyy"].Value = txt_Keyy.Text;
            //DataGridView1.Rows[i].Cells[5].Value = chbx_Custom.Checked ? "Yes" :"No";
            DataGridView1.Rows[i].Cells["Description"].Value = txt_Description.Text;
            DataGridView1.Rows[i].Cells["AmpType"].Value = chbx_AmpType.Text;
            DataGridView1.Rows[i].Cells["AmpCategory"].Value = chbx_AmpCategory.Text;
            DataGridView1.Rows[i].Cells["AmpKnobValues"].Value = chbx_AmpKnobValues.Text;
            DataGridView1.Rows[i].Cells["AmpPedalKey"].Value = chbx_AmpPedalKey.Text;
            DataGridView1.Rows[i].Cells["CabinetCategory"].Value = chbx_CabinetCategory.Text;
            DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value = chbx_CabinetKnobValues.Text;
            DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value = chbx_CabinetPedalKey.Text;
            DataGridView1.Rows[i].Cells["CabinetType"].Value = chbx_CabinetType.Text;

            if (chbx_Custom.Checked) DataGridView1.Rows[i].Cells["Is_Custom"].Value = "True";
            else DataGridView1.Rows[i].Cells["Is_Custom"].Value = "False";

            //var DB_Path = "../../../../tmp\\Files.accdb;";
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                //OleDbCommand command = new OleDbCommand(); ;
                //Update TonesDB
                //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                command.CommandText = "UPDATE Tones SET ";

                //command.CommandText += "ID = @param0, ";
                command.CommandText += "Tone_Name = @param1, ";
                command.CommandText += "CDLC_ID = @param2, ";
                command.CommandText += "Volume = @param3, ";
                command.CommandText += "Keyy = @param4, ";
                command.CommandText += "Is_Custom = @param5, ";
                //command.CommandText += "GearList = @param6, ";
                //command.CommandText += "AmpRack = @param7, ";
                //command.CommandText += "Pedals = @param8, ";
                command.CommandText += "Description = @param9, ";
                //command.CommandText += "Favorite = @param10, ";
                //command.CommandText += "SortOrder = @param11, ";
                //command.CommandText += "NameSeparator = @param12, ";
                //command.CommandText += "Cabinet = @param13, ";
                //command.CommandText += "PostPedal1 = @param14, ";
                //command.CommandText += "PostPedal2 = @param15, ";
                //command.CommandText += "PostPedal3 = @param16, ";
                //command.CommandText += "PostPedal4 = @param17, ";
                //command.CommandText += "PrePedal1 = @param18, ";
                //command.CommandText += "PrePedal2 = @param19, ";
                //command.CommandText += "PrePedal3 = @param20, ";
                //command.CommandText += "PrePedal4 = @param21, ";
                //command.CommandText += "Rack1 = @param22, ";
                //command.CommandText += "Rack2 = @param23, ";
                //command.CommandText += "Rack3 = @param24, ";
                //command.CommandText += "Rack4 = @param25, ";
                command.CommandText += "AmpType = @param26, ";
                command.CommandText += "AmpCategory = @param27, ";
                command.CommandText += "AmpKnobValues = @param28, ";
                command.CommandText += "AmpPedalKey = @param29, ";
                command.CommandText += "CabinetCategory = @param30, ";
                command.CommandText += "CabinetKnobValues = @param31, ";
                command.CommandText += "CabinetPedalKey = @param32, ";
                command.CommandText += "CabinetType = @param33, ";
                command.CommandText += "lastConversionDateTime = @param34 ";
                //command.CommandText += "lastConverjsonDateTime = @param35, ";
                //command.CommandText += "Comments = @param36 ";

                command.CommandText += "WHERE ID = " + txt_ID.Text;

                command.Parameters.AddWithValue("@param1", DataGridView1.Rows[i].Cells["Tone_Name"].Value.ToString());
                command.Parameters.AddWithValue("@param2", DataGridView1.Rows[i].Cells["CDLC_ID"].Value.ToString());
                command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells["Volume"].Value.ToString());
                command.Parameters.AddWithValue("@param4", DataGridView1.Rows[i].Cells["Keyy"].Value.ToString());
                command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells["Pedals"].Value.ToString());
                command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells["Description"].Value.ToString());
                command.Parameters.AddWithValue("@param26", DataGridView1.Rows[i].Cells["AmpType"].Value.ToString());
                command.Parameters.AddWithValue("@param27", DataGridView1.Rows[i].Cells["AmpCategory"].Value.ToString());
                command.Parameters.AddWithValue("@param28", DataGridView1.Rows[i].Cells["AmpKnobValues"].Value.ToString());
                command.Parameters.AddWithValue("@param29", DataGridView1.Rows[i].Cells["AmpPedalKey"].Value.ToString());
                command.Parameters.AddWithValue("@param30", DataGridView1.Rows[i].Cells["CabinetCategory"].Value.ToString());
                command.Parameters.AddWithValue("@param31", DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value.ToString());
                command.Parameters.AddWithValue("@param32", DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value.ToString());
                command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString());
                command.Parameters.AddWithValue("@param34", DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString());
                command.Parameters.AddWithValue("@param5", DataGridView1.Rows[i].Cells["Is_Custom"].Value.ToString());
                //command.Parameters.AddWithValue("@param25", DataGridView1.Rows[i].Cells[""].Value.ToString());
                //command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells[""].Value.ToString());
                //command.Parameters.AddWithValue("@param34", DataGridView1.Rows[i].Cells[""].Value.ToString());
                //command.Parameters.AddWithValue("@param35", DataGridView1.Rows[i].Cells[""].Value.ToString());
                //command.Parameters.AddWithValue("@param36", DataGridView1.Rows[i].Cells[""].Value.ToString());
                //command.Parameters.AddWithValue("@param37", DataGridView1.Rows[i].Cells[""].Value.ToString());
                try
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Tones DB connection in Tones Edit screen ! " + DB_Path + "-" + command.CommandText);

                    throw;
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
                ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                if (!chbx_AutoSave.Checked) MessageBox.Show("Tones Saved");
                //das.SelectCommand.CommandText = "SELECT * FROM Tones";
                //// das.Update(dssx, "Tones");
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            dssx = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + CDLCID + ";");
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    var cmd = "SELECT * FROM Tones WHERE CDLC_ID=" + CDLCID + ";";
            //    OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
            //    da.Fill(dssx, "Tones");
            //}
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID " };
            DataGridViewTextBoxColumn Tone_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Tone_Name", HeaderText = "Tone_Name " };
            DataGridViewTextBoxColumn CDLC_ID = new DataGridViewTextBoxColumn { DataPropertyName = "CDLC_ID", HeaderText = "CDLC_ID " };
            DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
            DataGridViewTextBoxColumn Keyy = new DataGridViewTextBoxColumn { DataPropertyName = "Keyy", HeaderText = "Keyy " };
            DataGridViewTextBoxColumn Is_Custom = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Custom", HeaderText = "Is_Custom " };
            DataGridViewTextBoxColumn GearList = new DataGridViewTextBoxColumn { DataPropertyName = "GearList", HeaderText = "GearList " };
            DataGridViewTextBoxColumn AmpRack = new DataGridViewTextBoxColumn { DataPropertyName = "AmpRack", HeaderText = "AmpRack " };
            DataGridViewTextBoxColumn Pedals = new DataGridViewTextBoxColumn { DataPropertyName = "Pedals", HeaderText = "Pedals " };
            DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
            DataGridViewTextBoxColumn Favorite = new DataGridViewTextBoxColumn { DataPropertyName = "Favorite", HeaderText = "Favorite " };
            DataGridViewTextBoxColumn SortOrder = new DataGridViewTextBoxColumn { DataPropertyName = "SortOrder", HeaderText = "SortOrder " };
            DataGridViewTextBoxColumn NameSeparator = new DataGridViewTextBoxColumn { DataPropertyName = "NameSeparator", HeaderText = "NameSeparator " };
            DataGridViewTextBoxColumn Cabinet = new DataGridViewTextBoxColumn { DataPropertyName = "Cabinet", HeaderText = "Cabinet " };
            DataGridViewTextBoxColumn PostPedal1 = new DataGridViewTextBoxColumn { DataPropertyName = "PostPedal1", HeaderText = "PostPedal1 " };
            DataGridViewTextBoxColumn PostPedal2 = new DataGridViewTextBoxColumn { DataPropertyName = "PostPedal2", HeaderText = "PostPedal2 " };
            DataGridViewTextBoxColumn PostPedal3 = new DataGridViewTextBoxColumn { DataPropertyName = "PostPedal3", HeaderText = "PostPedal3 " };
            DataGridViewTextBoxColumn PostPedal4 = new DataGridViewTextBoxColumn { DataPropertyName = "PostPedal4", HeaderText = "PostPedal4 " };
            DataGridViewTextBoxColumn PrePedal1 = new DataGridViewTextBoxColumn { DataPropertyName = "PrePedal1", HeaderText = "PrePedal1 " };
            DataGridViewTextBoxColumn PrePedal2 = new DataGridViewTextBoxColumn { DataPropertyName = "PrePedal2", HeaderText = "PrePedal2 " };
            DataGridViewTextBoxColumn PrePedal3 = new DataGridViewTextBoxColumn { DataPropertyName = "PrePedal3", HeaderText = "PrePedal3 " };
            DataGridViewTextBoxColumn PrePedal4 = new DataGridViewTextBoxColumn { DataPropertyName = "PrePedal4", HeaderText = "PrePedal4 " };
            DataGridViewTextBoxColumn Rack1 = new DataGridViewTextBoxColumn { DataPropertyName = "Rack1", HeaderText = "Rack1 " };
            DataGridViewTextBoxColumn Rack2 = new DataGridViewTextBoxColumn { DataPropertyName = "Rack2", HeaderText = "Rack2 " };
            DataGridViewTextBoxColumn Rack3 = new DataGridViewTextBoxColumn { DataPropertyName = "Rack3", HeaderText = "Rack3 " };
            DataGridViewTextBoxColumn Rack4 = new DataGridViewTextBoxColumn { DataPropertyName = "Rack4", HeaderText = "Rack4 " };
            DataGridViewTextBoxColumn AmpType = new DataGridViewTextBoxColumn { DataPropertyName = "AmpType", HeaderText = "AmpType " };
            DataGridViewTextBoxColumn AmpCategory = new DataGridViewTextBoxColumn { DataPropertyName = "AmpCategory", HeaderText = "AmpCategory " };
            DataGridViewTextBoxColumn AmpKnobValues = new DataGridViewTextBoxColumn { DataPropertyName = "AmpKnobValues", HeaderText = "AmpKnobValues " };
            DataGridViewTextBoxColumn AmpPedalKey = new DataGridViewTextBoxColumn { DataPropertyName = "AmpPedalKey", HeaderText = "AmpPedalKey " };
            DataGridViewTextBoxColumn CabinetCategory = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetCategory", HeaderText = "CabinetCategory " };
            DataGridViewTextBoxColumn CabinetKnobValues = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetKnobValues", HeaderText = "CabinetKnobValues " };
            DataGridViewTextBoxColumn CabinetPedalKey = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetPedalKey", HeaderText = "CabinetPedalKey " };
            DataGridViewTextBoxColumn CabinetType = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetType", HeaderText = "CabinetType " };
            DataGridViewTextBoxColumn lastConversionDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "lastConversionDateTime", HeaderText = "lastConversionDateTime " };
            DataGridViewTextBoxColumn lastConverjsonDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "lastConverjsonDateTime", HeaderText = "lastConverjsonDateTime " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };



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
            //    {
            //        ID,
            //        Tone_Name,
            //        CDLC_ID,
            //        Volume,
            //        Keyy,
            //        Is_Custom,
            //        GearList,
            //        AmpRack,
            //        Pedals,
            //        Description,
            //        Favorite,
            //        SortOrder,
            //        NameSeparator,
            //        Cabinet,
            //        PostPedal1,
            //        PostPedal2,
            //        PostPedal3,
            //        PostPedal4,
            //        PrePedal1,
            //        PrePedal2,
            //        PrePedal3,
            //        PrePedal4,
            //        Rack1,
            //        Rack2,
            //        Rack3,
            //        Rack4,
            //        AmpType,
            //        AmpCategory,
            //        AmpKnobValues,
            //        AmpPedalKey,
            //        CabinetCategory,
            //        CabinetKnobValues,
            //        CabinetPedalKey,
            //        CabinetType,
            //        lastConversionDateTime,
            //        lastConverjsonDateTime,
            //        Comments
            //    }
            //);

            dssx.Tables["Tones"].AcceptChanges();

            bs.DataSource = dssx.Tables["Tones"];
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
            public string Tone_Name { get; set; }
            public string CDLC_ID { get; set; }
            public string Volume { get; set; }
            public string Keyy { get; set; }
            public string Is_Custom { get; set; }
            public string GearList { get; set; }
            public string AmpRack { get; set; }
            public string Pedals { get; set; }
            public string Description { get; set; }
            public string Favorite { get; set; }
            public string SortOrder { get; set; }
            public string NameSeparator { get; set; }
            public string Cabinet { get; set; }
            public string PostPedal1 { get; set; }
            public string PostPedal2 { get; set; }
            public string PostPedal3 { get; set; }
            public string PostPedal4 { get; set; }
            public string PrePedal1 { get; set; }
            public string PrePedal2 { get; set; }
            public string PrePedal3 { get; set; }
            public string PrePedal4 { get; set; }
            public string Rack1 { get; set; }
            public string Rack2 { get; set; }
            public string Rack3 { get; set; }
            public string Rack4 { get; set; }
            public string AmpType { get; set; }
            public string AmpCategory { get; set; }
            public string AmpKnobValues { get; set; }
            public string AmpPedalKey { get; set; }
            public string CabinetCategory { get; set; }
            public string CabinetKnobValues { get; set; }
            public string CabinetPedalKey { get; set; }
            public string CabinetType { get; set; }
            public string lastConversionDateTime { get; set; }
            public string lastConverjsonDateTime { get; set; }
            public string Comments { get; set; }
        }
        private Files[] files = new Files[10000];
        //Generic procedure to read and parse Tones.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            DataSet dus = new DataSet(); dus = SelectFromDB("Tones", cmd);
            //try
            //{
            //    MessageBox.Show(DB_Path);
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //        dax.Fill(dus, "Tones");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                        files[i].ID = dataRow.ItemArray[0].ToString();
                        files[i].Tone_Name = dataRow.ItemArray[1].ToString();
                        files[i].CDLC_ID = dataRow.ItemArray[2].ToString();
                        files[i].Volume = dataRow.ItemArray[3].ToString();
                        files[i].Keyy = dataRow.ItemArray[4].ToString();
                        files[i].Is_Custom = dataRow.ItemArray[5].ToString();
                        files[i].GearList = dataRow.ItemArray[6].ToString();
                        files[i].AmpRack = dataRow.ItemArray[7].ToString();
                        files[i].Pedals = dataRow.ItemArray[8].ToString();
                        files[i].Description = dataRow.ItemArray[9].ToString();
                        files[i].Favorite = dataRow.ItemArray[10].ToString();
                        files[i].SortOrder = dataRow.ItemArray[11].ToString();
                        files[i].NameSeparator = dataRow.ItemArray[12].ToString();
                        files[i].Cabinet = dataRow.ItemArray[13].ToString();
                        files[i].PostPedal1 = dataRow.ItemArray[14].ToString();
                        files[i].PostPedal2 = dataRow.ItemArray[15].ToString();
                        files[i].PostPedal3 = dataRow.ItemArray[16].ToString();
                        files[i].PostPedal4 = dataRow.ItemArray[17].ToString();
                        files[i].PrePedal1 = dataRow.ItemArray[18].ToString();
                        files[i].PrePedal2 = dataRow.ItemArray[19].ToString();
                        files[i].PrePedal3 = dataRow.ItemArray[20].ToString();
                        files[i].PrePedal4 = dataRow.ItemArray[21].ToString();
                        files[i].Rack1 = dataRow.ItemArray[22].ToString();
                        files[i].Rack2 = dataRow.ItemArray[23].ToString();
                        files[i].Rack3 = dataRow.ItemArray[24].ToString();
                        files[i].Rack4 = dataRow.ItemArray[25].ToString();
                        files[i].AmpType = dataRow.ItemArray[26].ToString();
                        files[i].AmpCategory = dataRow.ItemArray[27].ToString();
                        files[i].AmpKnobValues = dataRow.ItemArray[28].ToString();
                        files[i].AmpPedalKey = dataRow.ItemArray[29].ToString();
                        files[i].CabinetCategory = dataRow.ItemArray[30].ToString();
                        files[i].CabinetKnobValues = dataRow.ItemArray[31].ToString();
                        files[i].CabinetPedalKey = dataRow.ItemArray[32].ToString();
                        files[i].CabinetType = dataRow.ItemArray[33].ToString();
                        files[i].lastConversionDateTime = dataRow.ItemArray[34].ToString();
                        files[i].lastConverjsonDateTime = dataRow.ItemArray[35].ToString();
                        files[i].Comments = dataRow.ItemArray[36].ToString();
                        i++;
                    }
                    //Closing Connection
                    //dax.Dispose();
                    //cnn.Close();
                    //rtxt_StatisticsOnReadDLCs.Text += i;
                    //var ex = 0;
                //}
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Tones DB connection ! ");
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked && txt_CDLC_ID.Text != "" && txt_CDLC_ID.Text != null) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
            this.Close();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (txt_CDLC_ID.Text != "") ChangeRow();
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }

        private void chbx_AmpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_AmpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_AmpKnobValues_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbx_AmpPedalKey_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
