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
using RocksmithToolkitLib.XmlRepository;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class TonesDB : Form
    {
        public TonesDB(string txt_DBFolder, string CDLC_ID, OleDbConnection cnnb)
        {
            InitializeComponent();
            CDLCID = CDLC_ID;
            DB_Path = txt_DBFolder;
            cnb = cnnb;
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
        public OleDbConnection cnb;
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
            // DB_Path = DB_Path + "\\AccessDB.accdb"; //DLCManager.txt_DBFolder.Text
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            if (DataGridView1.SelectedCells.Count > 0 && DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
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
                //chbx_AmpType.Text = DataGridView1.Rows[i].Cells["AmpType"].Value.ToString();
                //chbx_AmpCategory.Text = DataGridView1.Rows[i].Cells["AmpCategory"].Value.ToString();
                //chbx_AmpKnobValues.Text = DataGridView1.Rows[i].Cells["AmpKnobValues"].Value.ToString();
                //chbx_AmpPedalKey.Text = DataGridView1.Rows[i].Cells["AmpPedalKey"].Value.ToString();
                //chbx_CabinetCategory.Text = DataGridView1.Rows[i].Cells["CabinetCategory"].Value.ToString();
                //chbx_CabinetKnobValues.Text = DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value.ToString();
                //chbx_CabinetPedalKey.Text = DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value.ToString();
                //chbx_CabinetType.Text = DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString();
                //chbx_ToneA.Text = DataGridView1.Rows[i].Cells["ToneA"].Value.ToString();
                //chbx_ToneB.Text = DataGridView1.Rows[i].Cells["ToneB"].Value.ToString();
                //chbx_ToneC.Text = DataGridView1.Rows[i].Cells["ToneC"].Value.ToString();
                //chbx_ToneD.Text = DataGridView1.Rows[i].Cells["ToneD"].Value.ToString();
                //txt_lastConversionDateTime.Text = DataGridView1.Rows[i].Cells["lastConversionDateTime"].Value.ToString();
                //txt_Description.Text = DataGridView1.Rows[i].Cells["Comments"].Value.ToString();

                //if (DataGridView1.Rows[i].Cells["GearList"].Value.ToString() == "Yes") chbx_GearList.Checked = true;
                //else chbx_GearList.Checked = false;
                if (DataGridView1.Rows[i].Cells["Is_Custom"].Value.ToString().ToLower() == "true") chbx_Custom.Checked = true;
                else chbx_Custom.Checked = false;

                ////Read Tones
                var nrc = 0;
                DataSet dtc = new DataSet(); dtc = SelectFromDB("Tones_GearList", "SELECT Gear_Name FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + ";", "", cnb);
                nrc = dtc.Tables[0].Rows.Count; var TID = "";
                if (nrc > 0)
                {
                    for (int k = cbx_Gear_Name.Items.Count - 1; k >= 0; --k) cbx_Gear_Name.Items.RemoveAt(k);
                    for (int k = 0; k < nrc; k++)
                        cbx_Gear_Name.Items.Add(dtc.Tables[0].Rows[k].ItemArray[0].ToString());
                    cbx_Gear_Name.SelectedIndex = 0;

                    //if (txt_ArrangementType.Text == "Bass" && !(chbx_BassDD.Checked)) btn_AddDD.Enabled = true;
                    //if (txt_ArrangementType.Text == "Bass" && chbx_BassDD.Checked) btn_RemoveDD.Enabled = true;
                }

                if (chbx_AutoSave.Checked) SaveOK = true;
                else SaveOK = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        public void SaveRecord()
        {
            int i = -1;
            DataSet dis = new DataSet();
            if (DataGridView1.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = DataGridView1.SelectedCells[0].RowIndex;

                DataGridView1.Rows[i].Cells[0].Value = txt_ID.Text;
                DataGridView1.Rows[i].Cells["CDLC_ID"].Value = txt_CDLC_ID.Text;
                DataGridView1.Rows[i].Cells["Volume"].Value = txt_Volume.Text;
                DataGridView1.Rows[i].Cells["Keyy"].Value = txt_Keyy.Text;
                //DataGridView1.Rows[i].Cells[5].Value = chbx_Custom.Checked ? "Yes" :"No";
                DataGridView1.Rows[i].Cells["Description"].Value = txt_Description.Text;
                //DataGridView1.Rows[i].Cells["AmpType"].Value = chbx_AmpType.Text;
                //DataGridView1.Rows[i].Cells["AmpCategory"].Value = chbx_AmpCategory.Text;
                //DataGridView1.Rows[i].Cells["AmpKnobValues"].Value = chbx_AmpKnobValues.Text;
                ////DataGridView1.Rows[i].Cells["AmpKnobKeys"].Value = chbx_AmpKnobValues.Text;
                //DataGridView1.Rows[i].Cells["AmpPedalKey"].Value = chbx_AmpPedalKey.Text;
                //DataGridView1.Rows[i].Cells["CabinetCategory"].Value = chbx_CabinetCategory.Text;
                //DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value = chbx_CabinetKnobValues.Text;
                ////DataGridView1.Rows[i].Cells["CabinetKnobKeys"].Value = chbx_CabinetKnobValues.Text;
                //DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value = chbx_CabinetPedalKey.Text;
                //DataGridView1.Rows[i].Cells["CabinetType"].Value = chbx_CabinetType.Text;

                if (chbx_Custom.Checked) DataGridView1.Rows[i].Cells["Is_Custom"].Value = "True";
                else DataGridView1.Rows[i].Cells["Is_Custom"].Value = "False";

                //var DB_Path = "../../../../tmp\\AccessDB.accdb;";
                var connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
                    //command.CommandText += "AmpType = @param26, ";
                    //command.CommandText += "AmpCategory = @param27, ";
                    //command.CommandText += "AmpKnobValues = @param28, ";
                    //command.CommandText += "AmpPedalKey = @param29, ";
                    //command.CommandText += "CabinetCategory = @param30, ";
                    //command.CommandText += "CabinetKnobValues = @param31, ";
                    //command.CommandText += "CabinetPedalKey = @param32, ";
                    //command.CommandText += "CabinetType = @param33, ";
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
                    //command.Parameters.AddWithValue("@param26", DataGridView1.Rows[i].Cells["AmpType"].Value.ToString());
                    //command.Parameters.AddWithValue("@param27", DataGridView1.Rows[i].Cells["AmpCategory"].Value.ToString());
                    //command.Parameters.AddWithValue("@param28", DataGridView1.Rows[i].Cells["AmpKnobValues"].Value.ToString());
                    //command.Parameters.AddWithValue("@param29", DataGridView1.Rows[i].Cells["AmpPedalKey"].Value.ToString());
                    //command.Parameters.AddWithValue("@param30", DataGridView1.Rows[i].Cells["CabinetCategory"].Value.ToString());
                    //command.Parameters.AddWithValue("@param31", DataGridView1.Rows[i].Cells["CabinetKnobValues"].Value.ToString());
                    //command.Parameters.AddWithValue("@param32", DataGridView1.Rows[i].Cells["CabinetPedalKey"].Value.ToString());
                    //command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString());
                    //command.Parameters.AddWithValue("@param34", DataGridView1.Rows[i].Cells["CabinetType"].Value.ToString());
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

                    var tid = txt_ID.Text;
                    var insertcmdd = "CDLC_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex";
                    var insertvalues = ""; insertvalues += tid + ", \"" + cbx_Gear_Name.Text + "\", \"" + NullHandler(cbx_Type.Text);
                    insertvalues += "\", \"" + NullHandler(chbx_Category.Text);
                    //string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    insertvalues += "\", \"" + NullHandler(chbx_KnobValues.Text);
                    insertvalues += "\", \"" + NullHandler(chbx_KnobKeys.Text);
                    insertvalues += "\", \"" + NullHandler(chbx_PedalKey.Text);
                    insertvalues += "\", \"" + chbx_Skin.Text;
                    insertvalues += "\", \"" + NullHandler(chbx_SkinIndex.Text) + "\"";
                    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, 0);
                    //for (int k = 0;k< cbx_Type.Items.Count - 1; k++)
                    //{
                    //var insertcmdd = "CDLC_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex";
                    //var insertvalues = ""; insertvalues += tid + ", \""+ cbx_Gear_Name + "\", \"" + NullHandler(cbx_Type.Items[k]);
                    //insertvalues += "\", \"" + NullHandler(cbx_Type.Text);
                    //string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                    //insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                    //insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Amp == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Amp.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);


                    //}

                    //insertvalues = ""; insertvalues += tid + ", \"Cabinet\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Type));
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Category));
                    //vals = ""; keys = ""; if (tn.GearList.Cabinet != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Cabinet.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1)));
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1)));
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Cabinet == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Cabinet.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PostPedal1\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PostPedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal1.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PostPedal2\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PostPedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal2.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PostPedal3\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PostPedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal3.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PostPedal4\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PostPedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PostPedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PostPedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PostPedal4.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PrePedal1\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PrePedal1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal1.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PrePedal2\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PrePedal2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal2.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PrePedal3\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PrePedal3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal3.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"PrePedal4\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Type));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Category));
                    //vals = ""; keys = ""; if (tn.GearList.PrePedal4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.PrePedal4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.PrePedal4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.PrePedal4.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"Rack1\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Type));
                    //insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Category));
                    //vals = ""; keys = ""; if (tn.GearList.Rack1 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack1.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Rack1 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack1.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"Rack2\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Type));
                    //insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Category));
                    //vals = ""; keys = ""; if (tn.GearList.Rack2 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack2.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Rack2 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack2.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"Rack3\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Type));
                    //insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Category));
                    //vals = ""; keys = ""; if (tn.GearList.Rack3 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack3.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Rack3 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack3.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    //insertvalues = ""; insertvalues += tid + ", \"Rack4\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Type));
                    //insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Category));
                    //vals = ""; keys = ""; if (tn.GearList.Rack4 != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Rack4.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                    //insertvalues += "\", \"" + NullHandler(vals == "" ? DBNull.Value.ToString() : vals.Substring(1));
                    //insertvalues += "\", \"" + NullHandler(keys == "" ? DBNull.Value.ToString() : keys.Substring(1));
                    //insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.PedalKey));
                    //insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.Skin));
                    //insertvalues += "\", \"" + (tn.GearList.Rack4 == null ? DBNull.Value.ToString() : NullHandler(tn.GearList.Rack4.SkinIndex)) + "\"";
                    //InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, mutit);

                    ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                    if (!chbx_AutoSave.Checked) MessageBox.Show("Tones Saved");
                    //das.SelectCommand.CommandText = "SELECT * FROM Tones";
                    //// das.Update(dssx, "Tones");
                    dis.Dispose();
                }
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            dssx = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + CDLCID + ";", "", cnb);
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
            DataGridViewTextBoxColumn AmpKnobKeys = new DataGridViewTextBoxColumn { DataPropertyName = "AmpKnobKeys", HeaderText = "AmpKnobKeys " };
            DataGridViewTextBoxColumn AmpPedalKey = new DataGridViewTextBoxColumn { DataPropertyName = "AmpPedalKey", HeaderText = "AmpPedalKey " };
            DataGridViewTextBoxColumn CabinetCategory = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetCategory", HeaderText = "CabinetCategory " };
            DataGridViewTextBoxColumn CabinetKnobValues = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetKnobValues", HeaderText = "CabinetKnobValues " };
            DataGridViewTextBoxColumn CabinetKnobKeys = new DataGridViewTextBoxColumn { DataPropertyName = "CabinetKnobKeys", HeaderText = "CabinetKnobKeys " };
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

            //dssx.Tables["Tones"].AcceptChanges();

            //bs.DataSource = dssx.Tables["Tones"];
            //DataGridView.DataSource = bs;
            ////DataGridView.ExpandColumns();

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
            DataGridView.AutoResizeColumns();
            bs.ResetBindings(false);
            dssx.Tables["Tones"].AcceptChanges();
            bs.DataSource = dssx.Tables["Tones"];
            DataGridView.DataSource = null;
            DataGridView.DataSource = bs;
            DataGridView.Refresh();
            dssx.Dispose();

            //////Read Tones
            //var nrc = 0;
            //DataSet dtc = new DataSet(); dtc = SelectFromDB("Tones_GearList", "SELECT Gear_Name FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + ";", "", cnb);
            //nrc = dtc.Tables[0].Rows.Count; var TID = "";
            //if (nrc > 0)
            //{
            //    for (int k = 0; k < nrc; k++)
            //        cmbx_Gear_Name.Items.Add(dtc.Tables[0].Rows[k].ItemArray[0].ToString());
            //    cmbx_Gear_Name.SelectedIndex = 0;

            //    ChangeRow();
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
            public string AmpKnobKeys { get; set; }
            public string AmpPedalKey { get; set; }
            public string CabinetCategory { get; set; }
            public string CabinetKnobValues { get; set; }
            public string CabinetKnobKeys { get; set; }
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
            DataSet dus = new DataSet(); dus = SelectFromDB("Tones", cmd, "", cnb);
            //try
            //{
            //    MessageBox.Show(DB_Path);
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft."+ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
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
                files[i].AmpKnobKeys = dataRow.ItemArray[29].ToString();
                files[i].AmpPedalKey = dataRow.ItemArray[30].ToString();
                files[i].CabinetCategory = dataRow.ItemArray[31].ToString();
                files[i].CabinetKnobValues = dataRow.ItemArray[32].ToString();
                files[i].CabinetKnobKeys = dataRow.ItemArray[33].ToString();
                files[i].CabinetPedalKey = dataRow.ItemArray[34].ToString();
                files[i].CabinetType = dataRow.ItemArray[35].ToString();
                files[i].lastConversionDateTime = dataRow.ItemArray[36].ToString();
                files[i].lastConverjsonDateTime = dataRow.ItemArray[37].ToString();
                files[i].Comments = dataRow.ItemArray[38].ToString();

                ////Read Tones
                var nrc = 0;
                DataSet dtc = new DataSet(); dtc = SelectFromDB("Tones_GearList", "SELECT Gear_Name FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + ";", "", cnb);
                nrc = dtc.Tables[0].Rows.Count; var TID = "";
                if (nrc > 0)
                {
                    for (int k = 0; k < nrc; k++)
                        cbx_Gear_Name.Items.Add(dtc.Tables[0].Rows[k].ItemArray[0].ToString());
                    cbx_Gear_Name.SelectedIndex = 0;
                    //TID = dtc.Tables[0].Rows[0].ItemArray[0].ToString();
                    //dictionary types not saved in the DB yet

                    //}
                    //nrc = 0;
                    //DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Cabinet\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsa.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsa.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Type = dsa.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsa.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.Category = dsa.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsa.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsa.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.Cabinet.KnobValues = FS;
                    //    if (dsa.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Cabinet.PedalKey = dsa.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsa.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dsa.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsa.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dsa.Tables[0].Rows[k].ItemArray[6].ToString());
                    //}
                    //nrc = 0;
                    //DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal1\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dss1.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dss1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Type = dss1.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dss1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Category = dss1.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dss1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dss1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal1.KnobValues = FS;
                    //    if (dss1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.PedalKey = dss1.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dss1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.Skin = dss1.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dss1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal1.SkinIndex = float.Parse(dss1.Tables[0].Rows[k].ItemArray[6].ToString());
                    //}
                    //nrc = 0;
                    //DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal2\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dss2.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dss2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Type = dss2.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dss2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Category = dss2.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dss2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dss2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal2.KnobValues = FS;
                    //    if (dss2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.PedalKey = dss2.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dss2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.Skin = dss2.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dss2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal2.SkinIndex = float.Parse(dss2.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal3\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dss3.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dss3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Type = dss3.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dss3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Category = dss3.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dss3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dss3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal3.KnobValues = FS;
                    //    if (dss3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.PedalKey = dss3.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dss3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.Skin = dss3.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dss3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal3.SkinIndex = float.Parse(dss3.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PostPedal4\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dss4.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dss4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Type = dss4.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dss4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Category = dss4.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dss4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dss4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PostPedal4.KnobValues = FS;
                    //    if (dss4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.PedalKey = dss4.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dss4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.Skin = dss4.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dss4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PostPedal4.SkinIndex = float.Parse(dss4.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}

                    //nrc = 0;
                    //DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal1\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsp1.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsp1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Type = dsp1.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsp1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Category = dsp1.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsp1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsp1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal1.KnobValues = FS;
                    //    if (dsp1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.PedalKey = dsp1.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsp1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.Skin = dsp1.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsp1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal1.SkinIndex = float.Parse(dsp1.Tables[0].Rows[k].ItemArray[6].ToString());
                    //}
                    //nrc = 0;
                    //DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal2\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsp2.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsp2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Type = dsp2.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsp2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Category = dsp2.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsp2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsp2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal2.KnobValues = FS;
                    //    if (dsp2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.PedalKey = dsp2.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsp2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.Skin = dsp2.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsp2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal2.SkinIndex = float.Parse(dsp2.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal3\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsp3.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsp3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Type = dsp3.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsp3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Category = dsp3.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsp3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsp3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal3.KnobValues = FS;
                    //    if (dsp3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.PedalKey = dsp3.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsp3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.Skin = dsp3.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsp3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal3.SkinIndex = float.Parse(dsp3.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"PrePedal4\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsp4.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsp4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Type = dsp4.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsp4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Category = dsp4.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsp4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsp4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.PrePedal4.KnobValues = FS;
                    //    if (dsp4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.PedalKey = dsp4.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsp4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.Skin = dsp4.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsp4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.PrePedal4.SkinIndex = float.Parse(dsp4.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}

                    //nrc = 0;
                    //DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack1\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsr1.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsr1.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Type = dsr1.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsr1.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Category = dsr1.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsr1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsr1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack1.KnobValues = FS;
                    //    if (dsr1.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack1.PedalKey = dsr1.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsr1.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack1.Skin = dsr1.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsr1.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack1.SkinIndex = float.Parse(dsr1.Tables[0].Rows[k].ItemArray[6].ToString());
                    //}
                    //nrc = 0;
                    //DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack2\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsr2.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsr2.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Type = dsr2.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsr2.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Category = dsr2.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsr2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsr2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack2.KnobValues = FS;
                    //    if (dsr2.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack2.PedalKey = dsr2.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsr2.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack2.Skin = dsr2.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsr2.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack2.SkinIndex = float.Parse(dsr2.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack3\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsr3.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsr3.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Type = dsr3.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsr3.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Category = dsr3.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsr3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsr3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack3.KnobValues = FS;
                    //    if (dsr3.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack3.PedalKey = dsr3.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsr3.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack3.Skin = dsr3.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsr3.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack3.SkinIndex = float.Parse(dsr3.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                    //nrc = 0;
                    //DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + TID + " AND Gear_Name=\"Rack4\" ORDER BY Type DESC;", "", cnb);
                    //nrc = dsr4.Tables[0].Rows.Count;
                    //for (int k = 0; k < nrc; k++)
                    //{
                    //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                    //    if (dsr4.Tables[0].Rows[k].ItemArray[0].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Type = dsr4.Tables[0].Rows[k].ItemArray[0].ToString();
                    //    if (dsr4.Tables[0].Rows[k].ItemArray[1].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Category = dsr4.Tables[0].Rows[k].ItemArray[1].ToString();
                    //    Dictionary<string, float> FS = new Dictionary<string, float>();
                    //    strArrK = dsr4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                    //    strArrV = dsr4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                    //    for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] != "" && strArrV[l] != "") FS.Add(strArrK[l], float.Parse(strArrV[l]));
                    //    if (FS.Count != 0) data.TonesRS2014[j].GearList.Rack4.KnobValues = FS;
                    //    if (dsr4.Tables[0].Rows[k].ItemArray[4].ToString() != "") data.TonesRS2014[j].GearList.Rack4.PedalKey = dsr4.Tables[0].Rows[k].ItemArray[4].ToString();
                    //    if (dsr4.Tables[0].Rows[k].ItemArray[5].ToString() != "") data.TonesRS2014[j].GearList.Rack4.Skin = dsr4.Tables[0].Rows[k].ItemArray[5].ToString();
                    //    if (dsr4.Tables[0].Rows[k].ItemArray[6].ToString() != "") data.TonesRS2014[j].GearList.Rack4.SkinIndex = float.Parse(dsr4.Tables[0].Rows[k].ItemArray[6].ToString());

                    //}
                }
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            files[i].AmpType = dsc.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].AmpCategory = dsc.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FG = new Dictionary<string, float>();
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            strArrK = dsc.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsc.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; k <= strArrK.Length - 1; l++) FG.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].AmpKnobValues = FG.ToString();
                //            files[i].AmpPedalKey = dsc.Tables[0].Rows[k].ItemArray[4].ToString();
                //            //files[i].PostPedal1.Skin = dsc.Tables[0].Rows[k].ItemArray[5].ToString();
                //            //files[i].PostPedal1.SkinIndex = float.Parse(dsc.Tables[0].Rows[k].ItemArray[6].ToString());
                //        }
                //        nrc = 0;
                //        DataSet dsa = new DataSet(); dsa = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey," +
                //            " Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"Cabinet\";", "", cnb);
                //        nrc = dsa.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.Cabinet.Type = dsa.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.Cabinet.Category = dsa.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsa.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsa.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.Cabinet.KnobValues = FS;
                //            files[i].GearList.Cabinet.PedalKey = dsa.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PostPedal1.Skin = dsa.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PostPedal1.SkinIndex = float.Parse(dsa.Tables[0].Rows[k].ItemArray[6].ToString());
                //        }
                //        nrc = 0;
                //        DataSet dss1 = new DataSet(); dss1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey," +
                //            " Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PostPedal1\";", "", cnb);
                //        nrc = dss1.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PostPedal1.Type = dss1.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PostPedal1.Category = dss1.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dss1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dss1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PostPedal1.KnobValues = FS;
                //            files[i].GearList.PostPedal1.PedalKey = dss1.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PostPedal1.Skin = dss1.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PostPedal1.SkinIndex = float.Parse(dss1.Tables[0].Rows[k].ItemArray[6].ToString());
                //        }
                //        nrc = 0;
                //        DataSet dss2 = new DataSet(); dss2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey," +
                //            " Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PostPedal2\";", "", cnb);
                //        nrc = dss2.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PostPedal2.Type = dss2.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PostPedal2.Category = dss2.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dss2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dss2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PostPedal2.KnobValues = FS;
                //            files[i].GearList.PostPedal2.PedalKey = dss2.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PostPedal2.Skin = dss2.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PostPedal2.SkinIndex = float.Parse(dss2.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dss3 = new DataSet(); dss3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PostPedal3\";", "", cnb);
                //        nrc = dss3.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PostPedal3.Type = dss3.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PostPedal3.Category = dss3.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dss3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dss3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PostPedal3.KnobValues = FS;
                //            files[i].GearList.PostPedal3.PedalKey = dss3.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PostPedal3.Skin = dss3.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PostPedal3.SkinIndex = float.Parse(dss3.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dss4 = new DataSet(); dss4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey," +
                //            " Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PostPedal4\";", "", cnb);
                //        nrc = dss4.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PostPedal4.Type = dss4.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PostPedal4.Category = dss4.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dss4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dss4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PostPedal4.KnobValues = FS;
                //            files[i].GearList.PostPedal4.PedalKey = dss4.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PostPedal4.Skin = dss4.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PostPedal4.SkinIndex = float.Parse(dss4.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }

                //        nrc = 0;
                //        DataSet dsp1 = new DataSet(); dsp1 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PrePedal1\";", "", cnb);
                //        nrc = dsp1.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PrePedal1.Type = dsp1.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PrePedal1.Category = dsp1.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsp1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsp1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PrePedal1.KnobValues = FS;
                //            files[i].GearList.PrePedal1.PedalKey = dsp1.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PrePedal1.Skin = dsp1.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PrePedal1.SkinIndex = float.Parse(dsp1.Tables[0].Rows[k].ItemArray[6].ToString());
                //        }
                //        nrc = 0;
                //        DataSet dsp2 = new DataSet(); dsp2 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PrePedal2\";", "", cnb);
                //        nrc = dsp2.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PrePedal2.Type = dsp2.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PrePedal2.Category = dsp2.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsp2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsp2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PrePedal2.KnobValues = FS;
                //            files[i].GearList.PrePedal2.PedalKey = dsp2.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PrePedal2.Skin = dsp2.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PrePedal2.SkinIndex = float.Parse(dsp2.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dsp3 = new DataSet(); dsp3 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PrePedal3\";", "", cnb);
                //        nrc = dsp3.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PrePedal3.Type = dsp3.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PrePedal3.Category = dsp3.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsp3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsp3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PrePedal3.KnobValues = FS;
                //            files[i].GearList.PrePedal3.PedalKey = dsp3.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PrePedal3.Skin = dsp3.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PrePedal3.SkinIndex = float.Parse(dsp3.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dsp4 = new DataSet(); dsp4 = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"PrePedal4\";", "", cnb);
                //        nrc = dsp4.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.PrePedal4.Type = dsp4.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.PrePedal4.Category = dsp4.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsp4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsp4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.PrePedal4.KnobValues = FS;
                //            files[i].GearList.PrePedal4.PedalKey = dsp4.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.PrePedal4.Skin = dsp4.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.PrePedal4.SkinIndex = float.Parse(dsp4.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }

                //        nrc = 0;
                //        DataSet dsr1 = new DataSet(); dsr1 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"Rack1\";", "", cnb);
                //        nrc = dsr1.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.Rack1.Type = dsr1.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.Rack1.Category = dsr1.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsr1.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsr1.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.Rack1.KnobValues = FS;
                //            files[i].GearList.Rack1.PedalKey = dsr1.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.Rack1.Skin = dsr1.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.Rack1.SkinIndex = float.Parse(dsr1.Tables[0].Rows[k].ItemArray[6].ToString());
                //        }
                //        nrc = 0;
                //        DataSet dsr2 = new DataSet(); dsr2 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"Rack2\";", "", cnb);
                //        nrc = dsr2.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.Rack2.Type = dsr2.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.Rack2.Category = dsr2.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsr2.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsr2.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.Rack2.KnobValues = FS;
                //            files[i].GearList.Rack2.PedalKey = dsr2.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.Rack2.Skin = dsr2.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.Rack2.SkinIndex = float.Parse(dsr2.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dsr3 = new DataSet(); dsr3 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin," +
                //            " SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"Rack3\";", "", cnb);
                //        nrc = dsr3.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.Rack3.Type = dsr3.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.Rack3.Category = dsr3.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsr3.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsr3.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.Rack3.KnobValues = FS;
                //            files[i].GearList.Rack3.PedalKey = dsr3.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.Rack3.Skin = dsr3.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.Rack3.SkinIndex = float.Parse(dsr3.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
                //        nrc = 0;
                //        DataSet dsr4 = new DataSet(); dsr4 = SelectFromDB("Tones", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey," +
                //            " Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + " AND Gear_Name=\"Rack4\";", "", cnb);
                //        nrc = dsr4.Tables[0].Rows.Count;
                //        for (int k = 0; k < nrc; k++)
                //        {
                //            string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //            files[i].GearList.Rack4.Type = dsr4.Tables[0].Rows[k].ItemArray[0].ToString();
                //            files[i].GearList.Rack4.Category = dsr4.Tables[0].Rows[k].ItemArray[1].ToString();
                //            Dictionary<string, float> FS = new Dictionary<string, float>();
                //            strArrK = dsr4.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //            strArrV = dsr4.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //            for (int l = 0; l <= strArrK.Length - 1; k++) FS.Add(strArrK[l], float.Parse(strArrV[l]));
                //            files[i].GearList.Rack4.KnobValues = FS;
                //            files[i].GearList.Rack4.PedalKey = dsr4.Tables[0].Rows[k].ItemArray[4].ToString();
                //            files[i].GearList.Rack4.Skin = dsr4.Tables[0].Rows[k].ItemArray[5].ToString();
                //            files[i].GearList.Rack4.SkinIndex = float.Parse(dsr4.Tables[0].Rows[k].ItemArray[6].ToString());

                //        }
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

        private void cmbx_Gear_Name_SelectedIndexChanged(object sender, EventArgs e)
        {

            var nrc = 0;
            for (int k = cbx_Type.Items.Count - 1; k >= 0; --k) cbx_Type.Items.RemoveAt(k);
            for (int k = chbx_Category.Items.Count - 1; k >= 0; --k) chbx_Category.Items.RemoveAt(k);
            for (int k = chbx_KnobValues.Items.Count - 1; k >= 0; --k) chbx_KnobValues.Items.RemoveAt(k);
            for (int k = chbx_KnobKeys.Items.Count - 1; k >= 0; --k) chbx_KnobKeys.Items.RemoveAt(k);
            for (int k = chbx_PedalKey.Items.Count - 1; k >= 0; --k) chbx_PedalKey.Items.RemoveAt(k);
            for (int k = chbx_Skin.Items.Count - 1; k >= 0; --k) chbx_Skin.Items.RemoveAt(k);
            for (int k = chbx_SkinIndex.Items.Count - 1; k >= 0; --k) chbx_SkinIndex.Items.RemoveAt(k);


            DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + " AND Gear_Name=\"" + cbx_Gear_Name.Text + "\" ORDER BY Type DESC;", "", cnb);
            if (dsc.Tables.Count > 0) nrc = dsc.Tables[0].Rows.Count;
            //for (int k = 0; k < nrc; k++)
            if (nrc > 0)
            {
                if (dsc.Tables[0].Rows[0].ItemArray[0].ToString() != "") { cbx_Type.Items.Add(dsc.Tables[0].Rows[0].ItemArray[0].ToString()); cbx_Type.SelectedIndex = 0; }
                if (dsc.Tables[0].Rows[0].ItemArray[1].ToString() != "") { chbx_Category.Items.Add(dsc.Tables[0].Rows[0].ItemArray[1].ToString()); chbx_Category.SelectedIndex = 0; }
                Dictionary<string, float> FG = new Dictionary<string, float>();
                string strArrK = null; string strArrV = null;// char[] splitchar = { ';' };
                strArrK = dsc.Tables[0].Rows[0].ItemArray[2].ToString();//.Split(splitchar);
                strArrV = dsc.Tables[0].Rows[0].ItemArray[3].ToString();//.Split(splitchar);
                //for (int l = 0; l <= strArrK.Length - 1; l++) if (strArrK[l] == "" || strArrV[l] == "") FG.Add(strArrK[l], float.Parse(strArrV[l]));
                //if (strArrV != 0)
                chbx_KnobValues.Items.Add(strArrV); chbx_KnobValues.SelectedIndex = 0;
                //if (FG.strArrV != 0)
                chbx_KnobKeys.Items.Add(strArrK); chbx_KnobKeys.SelectedIndex = 0;
                if (dsc.Tables[0].Rows[0].ItemArray[4].ToString() != "") { chbx_PedalKey.Items.Add(dsc.Tables[0].Rows[0].ItemArray[4].ToString()); chbx_PedalKey.SelectedIndex = 0; }
                if (dsc.Tables[0].Rows[0].ItemArray[5].ToString() != "") { chbx_Skin.Items.Add(dsc.Tables[0].Rows[0].ItemArray[5].ToString()); chbx_Skin.SelectedIndex = 0; }
                if (dsc.Tables[0].Rows[0].ItemArray[6].ToString() != "") { chbx_SkinIndex.Items.Add(float.Parse(dsc.Tables[0].Rows[0].ItemArray[6].ToString())); chbx_SkinIndex.SelectedIndex = 0; }
                //var nrc = 0;
                //DataSet dsc = new DataSet(); dsc = SelectFromDB("Tones_GearList", "SELECT Type, Category, KnobValuesKeys, KnobValuesValues, PedalKey, Skin, SkinIndex FROM Tones_GearList WHERE CDLC_ID=" + txt_CDLC_ID.Text + " AND Gear_Name=\"" + cmbx_Gear_Name.Text + "\";", "", cnb);
                //nrc = dsc.Tables[0].Rows.Count;
                //for (int k = 0; k < nrc; k++)
                //{
                //    chbx_Type.Text = dsc.Tables[0].Rows[k].ItemArray[0].ToString();
                //    chbx_Category.Text = dsc.Tables[0].Rows[k].ItemArray[1].ToString();
                //    //Dictionary<string, float> FG = new Dictionary<string, float>();
                //    string[] strArrK = null; string[] strArrV = null; char[] splitchar = { ';' };
                //    strArrK = dsc.Tables[0].Rows[k].ItemArray[2].ToString().Split(splitchar);
                //    strArrV = dsc.Tables[0].Rows[k].ItemArray[3].ToString().Split(splitchar);
                //    //for (int l = 0; k <= strArrK.Length - 1; l++) chbx_KnobKeys.Add(strArrK[l]);
                //    //for (int l = 0; k <= strArrV.Length - 1; l++) chbx_KnobValues.Add(strArrV[l]);
                //    //chbx_KnobValues = FG;, float.Parse()
                //    chbx_KnobKeys.Text = dsc.Tables[0].Rows[k].ItemArray[2].ToString();
                //    chbx_KnobValues.Text = dsc.Tables[0].Rows[k].ItemArray[3].ToString();
                //    chbx_PedalKey.Text = dsc.Tables[0].Rows[k].ItemArray[4].ToString();
                //    chbx_Skin.Text = dsc.Tables[0].Rows[k].ItemArray[5].ToString();
                //    chbx_SkinIndex.Text = dsc.Tables[0].Rows[k].ItemArray[6].ToString();
            }

        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                ArrangementsDB frm = new ArrangementsDB(DB_Path, txt_ID.Text, false, cnb);
                frm.Show();
            }
            else MessageBox.Show("Chose an Arragement.");
        }

        private void cbx_Gear_Name_DropDown(object sender, EventArgs e)
        {
            //var tid = txt_ID.Text;
            //if (chbx_AutoSave.Checked)
            //{
            //    var insertcmdd = "CDLC_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex";
            //    var insertvalues = ""; insertvalues += tid + ", \"" + cbx_Gear_Name.Text + "\", \"" + NullHandler(cbx_Type.Text);
            //    insertvalues += "\", \"" + NullHandler(chbx_Category.Text);
            //    //string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
            //    insertvalues += "\", \"" + NullHandler(chbx_KnobValues.Text);
            //    insertvalues += "\", \"" + NullHandler(chbx_KnobKeys.Text);
            //    insertvalues += "\", \"" + NullHandler(chbx_PedalKey.Text);
            //    insertvalues += "\", \"" + chbx_Skin.Text;
            //    insertvalues += "\", \"" + NullHandler(chbx_SkinIndex.Text) + "\"";
            //    InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, 0);
            //}
        }

        private void cbx_Gear_Name_Enter(object sender, EventArgs e)
        {
            var tid = txt_ID.Text;
            if (chbx_AutoSave.Checked)
            {
                var insertcmdd = "CDLC_ID, Gear_Name, Type, Category, KnobValuesValues, KnobValuesKeys, PedalKey, Skin, SkinIndex";
                var insertvalues = ""; insertvalues += tid + ", \"" + cbx_Gear_Name.Text + "\", \"" + NullHandler(cbx_Type.Text);
                insertvalues += "\", \"" + NullHandler(chbx_Category.Text);
                //string vals = ""; string keys = ""; if (tn.GearList.Amp != null) foreach (KeyValuePair<string, float> glakv in tn.GearList.Amp.KnobValues) { vals += ";" + glakv.Value; keys += ";" + glakv.Key; }
                insertvalues += "\", \"" + NullHandler(chbx_KnobValues.Text);
                insertvalues += "\", \"" + NullHandler(chbx_KnobKeys.Text);
                insertvalues += "\", \"" + NullHandler(chbx_PedalKey.Text);
                insertvalues += "\", \"" + chbx_Skin.Text;
                insertvalues += "\", \"" + NullHandler(chbx_SkinIndex.Text) + "\"";
                InsertIntoDBwValues("Tones_GearList", insertcmdd, insertvalues, cnb, 0);
            }
        }
    }
}
