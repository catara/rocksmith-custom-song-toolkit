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
using System.Globalization;

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

        private void TonesDB_Load(object sender, EventArgs e)
        {
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
            int i;
            if (DataGridView1.SelectedCells.Count > 0 && DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
                i = DataGridView1.SelectedCells[0].RowIndex;
                txt_ID.Text = DataGridView1.Rows[i].Cells["ID"].Value.ToString();
                txt_Tone_Name.Text = DataGridView1.Rows[i].Cells["Tone_Name"].Value.ToString();
                txt_CDLC_ID.Text = DataGridView1.Rows[i].Cells["CDLC_ID"].Value.ToString();
                txt_Volume.Text = DataGridView1.Rows[i].Cells["Volume"].Value.ToString();
                txt_Keyy.Text = DataGridView1.Rows[i].Cells["Keyy"].Value.ToString();
                txt_Description.Text = DataGridView1.Rows[i].Cells["Description"].Value.ToString();
                if (DataGridView1.Rows[i].Cells["Is_Custom"].Value.ToString().ToLower() == "true") chbx_Custom.Checked = true;
                else chbx_Custom.Checked = false;
                if (DataGridView1.Rows[i].Cells["Favorite"].Value.ToString().ToLower() == "true") chbx_Favorite.Checked = true;
                else chbx_Favorite.Checked = false;

                ////Read Tones
                var nrc = 0;
                DataSet dtc = new DataSet(); dtc = SelectFromDB("Tones_GearList", "SELECT Gear_Name FROM Tones_GearList WHERE CDLC_ID=" + txt_ID.Text + ";", "", cnb);
                nrc = dtc.Tables[0].Rows.Count; /*var TID = "";*/
                if (nrc > 0)
                {
                    for (int k = cbx_Gear_Name.Items.Count - 1; k >= 0; --k) cbx_Gear_Name.Items.RemoveAt(k);
                    for (int k = 0; k < nrc; k++)
                        cbx_Gear_Name.Items.Add(dtc.Tables[0].Rows[k].ItemArray[0].ToString());
                    cbx_Gear_Name.SelectedIndex = 0;
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
                DataGridView1.Rows[i].Cells["Description"].Value = txt_Description.Text;
                if (chbx_Favorite.Checked) DataGridView1.Rows[i].Cells["Favorite"].Value = "True";
                else DataGridView1.Rows[i].Cells["Favorite"].Value = "False";

                if (chbx_Custom.Checked) DataGridView1.Rows[i].Cells["Is_Custom"].Value = "True";
                else DataGridView1.Rows[i].Cells["Is_Custom"].Value = "False";

                var connection = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft." + ConfigRepository.Instance()["dlcm_AccessDLLVersion"] + ";Data Source=" + DB_Path))
                {
                    command.CommandText = "UPDATE Tones SET ";
                    command.CommandText += "Tone_Name = @param1, ";
                    command.CommandText += "CDLC_ID = @param2, ";
                    command.CommandText += "Volume = @param3, ";
                    command.CommandText += "Keyy = @param4, ";
                    command.CommandText += "Is_Custom = @param5, ";
                    command.CommandText += "Description = @param9, ";
                    command.CommandText += "Favorite = @param10 ";
                    //command.CommandText += "lastConversionDateTime = @param34 ";

                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", DataGridView1.Rows[i].Cells["Tone_Name"].Value.ToString());
                    command.Parameters.AddWithValue("@param2", DataGridView1.Rows[i].Cells["CDLC_ID"].Value.ToString());
                    command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells["Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param4", DataGridView1.Rows[i].Cells["Keyy"].Value.ToString());
                    command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells["Description"].Value.ToString());
                    command.Parameters.AddWithValue("@param5", DataGridView1.Rows[i].Cells["Is_Custom"].Value.ToString());
                    command.Parameters.AddWithValue("@param10", DataGridView1.Rows[i].Cells["Favorite"].Value.ToString());
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
                    if (!chbx_AutoSave.Checked) MessageBox.Show("Tones Saved");
                    dis.Dispose();
                }
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            dssx = SelectFromDB("Tones", "SELECT * FROM Tones WHERE CDLC_ID=" + CDLCID + ";", "", cnb);
            var noOfRec = dssx.Tables[0].Rows.Count;
            lbl_NoRec.Text = " songs.";
            lbl_NoRec.Text = noOfRec.ToString() + " records.";
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID " };
            DataGridViewTextBoxColumn Tone_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Tone_Name", HeaderText = "Tone_Name " };
            DataGridViewTextBoxColumn CDLC_ID = new DataGridViewTextBoxColumn { DataPropertyName = "CDLC_ID", HeaderText = "CDLC_ID " };
            DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
            DataGridViewTextBoxColumn Keyy = new DataGridViewTextBoxColumn { DataPropertyName = "Keyy", HeaderText = "Keyy " };
            DataGridViewTextBoxColumn Is_Custom = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Custom", HeaderText = "Is_Custom " };
            DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
            DataGridViewTextBoxColumn Favorite = new DataGridViewTextBoxColumn { DataPropertyName = "Favorite", HeaderText = "Favorite " };
            DataGridViewTextBoxColumn SortOrder = new DataGridViewTextBoxColumn { DataPropertyName = "SortOrder", HeaderText = "SortOrder " };
            DataGridViewTextBoxColumn NameSeparator = new DataGridViewTextBoxColumn { DataPropertyName = "NameSeparator", HeaderText = "NameSeparator " };
            DataGridViewTextBoxColumn ConversionDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "ConversionDateTime", HeaderText = "ConversionDateTime " };
            DataGridViewTextBoxColumn lastConverjsonDateTime = new DataGridViewTextBoxColumn { DataPropertyName = "lastConverjsonDateTime", HeaderText = "lastConverjsonDateTime " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };

            DataGridView.AutoResizeColumns();
            bs.ResetBindings(false);
            dssx.Tables["Tones"].AcceptChanges();
            bs.DataSource = dssx.Tables["Tones"];
            DataGridView.DataSource = null;
            DataGridView.DataSource = bs;
            DataGridView.Refresh();
            dssx.Dispose();
        }

        private class Files
        {
            public string ID { get; set; }
            public string Tone_Name { get; set; }
            public string CDLC_ID { get; set; }
            public string Volume { get; set; }
            public string Keyy { get; set; }
            public string Is_Custom { get; set; }
            public string Description { get; set; }
            public string Favorite { get; set; }
            public string SortOrder { get; set; }
            public string NameSeparator { get; set; }
            public string ConversionDateTime { get; set; }
            public string lastConverjsonDateTime { get; set; }
            public string Comments { get; set; }
        }
        private Files[] files = new Files[10000];
        //Generic procedure to read and parse Tones.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            var MaximumSize = 0;

            DataSet dus = new DataSet(); dus = SelectFromDB("Tones", cmd, "", cnb);

            var i = 0;
            //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
            MaximumSize = dus.Tables[0].Rows.Count;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                files[i] = new Files();

                files[i].ID = dataRow.ItemArray[0].ToString();
                files[i].Tone_Name = dataRow.ItemArray[1].ToString();
                files[i].CDLC_ID = dataRow.ItemArray[2].ToString();
                files[i].Volume = dataRow.ItemArray[3].ToString();
                files[i].Keyy = dataRow.ItemArray[4].ToString();
                files[i].Is_Custom = dataRow.ItemArray[5].ToString();
                files[i].Description = dataRow.ItemArray[9].ToString();
                files[i].Favorite = dataRow.ItemArray[10].ToString();
                files[i].SortOrder = dataRow.ItemArray[11].ToString();
                files[i].NameSeparator = dataRow.ItemArray[12].ToString();
                files[i].ConversionDateTime = dataRow.ItemArray[36].ToString();
                files[i].lastConverjsonDateTime = dataRow.ItemArray[37].ToString();
                files[i].Comments = dataRow.ItemArray[38].ToString();

                ////Read Tones
                var nrc = 0;
                DataSet dtc = new DataSet(); dtc = SelectFromDB("Tones_GearList", "SELECT Gear_Name FROM Tones_GearList WHERE CDLC_ID=" + files[i].ID + ";", "", cnb);
                nrc = dtc.Tables[0].Rows.Count; /*var TID = "";*/
                if (nrc > 0)
                {
                    for (int k = 0; k < nrc; k++)
                        cbx_Gear_Name.Items.Add(dtc.Tables[0].Rows[k].ItemArray[0].ToString());
                    cbx_Gear_Name.SelectedIndex = 0;
                }
                i++;
            }
            return MaximumSize;//files[10000];
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {

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
                string strArrK = null; string strArrV = null;
                strArrK = dsc.Tables[0].Rows[0].ItemArray[2].ToString();
                strArrV = dsc.Tables[0].Rows[0].ItemArray[3].ToString();
                chbx_KnobValues.Items.Add(strArrV); chbx_KnobValues.SelectedIndex = 0;
                chbx_KnobKeys.Items.Add(strArrK); chbx_KnobKeys.SelectedIndex = 0;
                if (dsc.Tables[0].Rows[0].ItemArray[4].ToString() != "") { chbx_PedalKey.Items.Add(dsc.Tables[0].Rows[0].ItemArray[4].ToString()); chbx_PedalKey.SelectedIndex = 0; }
                if (dsc.Tables[0].Rows[0].ItemArray[5].ToString() != "") { chbx_Skin.Items.Add(dsc.Tables[0].Rows[0].ItemArray[5].ToString()); chbx_Skin.SelectedIndex = 0; }
                if (dsc.Tables[0].Rows[0].ItemArray[6].ToString() != "") { chbx_SkinIndex.Items.Add(float.Parse(dsc.Tables[0].Rows[0].ItemArray[6].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture)); chbx_SkinIndex.SelectedIndex = 0; }
 
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

        private void TonesDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chbx_AutoSave.Checked && txt_CDLC_ID.Text != "" && txt_CDLC_ID.Text != null) { SaveOK = true; SaveRecord(); }
            else SaveOK = false;
        }
    }
}
