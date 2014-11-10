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
    public partial class TonesDB : Form
    {
        public TonesDB(string txt_DBFolder)
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
            MessageBox.Show("Can not open Tones DB connection in TonesDB ! " + DB_Path);
        }
    }

    private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
    {
        int i;
            //i = DataGridView1.SelectedCells[0].RowIndex;
            //txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
            //txt_Arrangement_Name.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
            //txt_CDLC_ID.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            ////txt_SNGFilePath.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            ////txt_XMLFilePath.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
            //txt_ScrollSpeed.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
            //txt_Tunning.Text = DataGridView1.Rows[i].Cells[8].Value.ToString();
            //txt_Rating.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
            //txt_TuningPitch.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
            //txt_ToneBase.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
            ////txt_Idd.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
            //txt_ArrangementType.Text = DataGridView1.Rows[i].Cells[17].Value.ToString();
            //txt_String0.Text = DataGridView1.Rows[i].Cells[18].Value.ToString();
            //txt_String1.Text = DataGridView1.Rows[i].Cells[19].Value.ToString();
            //txt_String2.Text = DataGridView1.Rows[i].Cells[20].Value.ToString();
            //txt_String3.Text = DataGridView1.Rows[i].Cells[21].Value.ToString();
            //txt_String4.Text = DataGridView1.Rows[i].Cells[22].Value.ToString();
            //txt_String5.Text = DataGridView1.Rows[i].Cells[23].Value.ToString();
            //txt_BassPicking.Text = DataGridView1.Rows[i].Cells[24].Value.ToString();
            //txt_RouteMask.Text = DataGridView1.Rows[i].Cells[25].Value.ToString();
            //chbx_CabinetCategory.Text = DataGridView1.Rows[i].Cells[33].Value.ToString();
            //chbx_ToneB.Text = DataGridView1.Rows[i].Cells[34].Value.ToString();
            //chbx_ToneC.Text = DataGridView1.Rows[i].Cells[35].Value.ToString();
            //chbx_ToneD.Text = DataGridView1.Rows[i].Cells[36].Value.ToString();
            //txt_lastConversionDateTime.Text = DataGridView1.Rows[i].Cells[37].Value.ToString();

            //if (DataGridView1.Rows[i].Cells[3].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
            //else chbx_Bonus.Checked = false;

            ////ImageSource imageSource = new BitmapImage(new Uri("C:\\Temp\\music_edit.png"));

            //picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");

        }

    private void button8_Click(object sender, EventArgs e)
    {
        int i;
        DataSet dis = new DataSet();

        i = DataGridView1.SelectedCells[0].RowIndex;

            //DataGridView1.Rows[i].Cells[0].Value = txt_ID.Text;
            //DataGridView1.Rows[i].Cells[1].Value = txt_Arrangement_Name.Text;
            //DataGridView1.Rows[i].Cells[2].Value = txt_CDLC_ID.Text;
            ////DataGridView1.Rows[i].Cells[5].Value = txt_SNGFilePath.Text;
            ////DataGridView1.Rows[i].Cells[6].Value = txt_XMLFilePath.Text;
            //DataGridView1.Rows[i].Cells[7].Value = txt_ScrollSpeed.Text;
            //DataGridView1.Rows[i].Cells[8].Value = txt_Tunning.Text;
            //DataGridView1.Rows[i].Cells[9].Value = txt_Rating.Text;
            //DataGridView1.Rows[i].Cells[13].Value = txt_TuningPitch.Text;
            //DataGridView1.Rows[i].Cells[14].Value = txt_ToneBase.Text;
            ////DataGridView1.Rows[i].Cells[15].Value = txt_Idd.Text;
            //DataGridView1.Rows[i].Cells[17].Value = txt_ArrangementType.Text;
            //DataGridView1.Rows[i].Cells[18].Value = txt_String0.Text;
            //DataGridView1.Rows[i].Cells[19].Value = txt_String1.Text;
            //DataGridView1.Rows[i].Cells[20].Value = txt_String2.Text;
            //DataGridView1.Rows[i].Cells[21].Value = txt_String3.Text;
            //DataGridView1.Rows[i].Cells[22].Value = txt_String4.Text;
            //DataGridView1.Rows[i].Cells[23].Value = txt_String5.Text;
            //DataGridView1.Rows[i].Cells[24].Value = txt_BassPicking.Text;
            //DataGridView1.Rows[i].Cells[25].Value = txt_RouteMask.Text;
            //DataGridView1.Rows[i].Cells[33].Value = chbx_CabinetCategory.Text;
            //DataGridView1.Rows[i].Cells[34].Value = chbx_ToneB.Text;
            //DataGridView1.Rows[i].Cells[35].Value = chbx_ToneC.Text;
            //DataGridView1.Rows[i].Cells[36].Value = chbx_ToneD.Text;
            //DataGridView1.Rows[i].Cells[37].Value = txt_lastConversionDateTime.Text;
            //if (chbx_Bonus.Checked) DataGridView1.Rows[i].Cells[3].Value = "Yes";
            //else DataGridView1.Rows[i].Cells[3].Value = "No";

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

                //    command.CommandText += "ID = @param0, ";
                //    command.CommandText += "Arrangement_Name = @param1, ";
                //    command.CommandText += "CDLC_ID = @param2, ";
                //    command.CommandText += "Bonus = @param3, ";
                //    //command.CommandText += "SNGFilePath = @param4, ";
                //    //command.CommandText += "XMLFilePath = @param5, ";
                //    //command.CommandText += "XMLFile_Hash = @param6, ";
                //    command.CommandText += "ScrollSpeed = @param7, ";
                //    command.CommandText += "Tunning = @param8, ";
                //    command.CommandText += "Rating = @param9, ";
                //    //command.CommandText += "PlayThoughYBLink = @param10, ";
                //    //command.CommandText += "CustomsForge_Link = @param11, ";
                //    //command.CommandText += "Tonesort = @param12, ";
                //    command.CommandText += "TuningPitch = @param13, ";
                //    command.CommandText += "ToneBase = @param14, ";
                //    //command.CommandText += "Idd = @param15, ";
                //    //command.CommandText += "MasterId = @param16, ";
                //    command.CommandText += "ArrangementType = @param17, ";
                //    command.CommandText += "String0 = @param18, ";
                //    command.CommandText += "String1 = @param19, ";
                //    command.CommandText += "String2 = @param20, ";
                //    command.CommandText += "String3 = @param21, ";
                //    command.CommandText += "String4 = @param22, ";
                //    command.CommandText += "String5 = @param23, ";
                //    command.CommandText += "PluckedType = @param24, ";
                //    command.CommandText += "RouteMask = @param25, ";
                //    //command.CommandText += "XMLFileName = @param26, ";
                //    //command.CommandText += "XMLFileLLID = @param27, ";
                //    //command.CommandText += "XMLFileUUID = @param28, ";
                //    //command.CommandText += "SNGFileName = @param29, ";
                //    //command.CommandText += "SNGFileLLID = @param30, ";
                //    //command.CommandText += "SNGFileUUID = @param31, ";
                //    //command.CommandText += "ToneMultiplayer = @param32, ";
                //    command.CommandText += "ToneA = @param33, ";
                //    command.CommandText += "ToneB = @param34, ";
                //    command.CommandText += "ToneC = @param35, ";
                //    command.CommandText += "ToneD = @param36, ";
                //    command.CommandText += "lastConversionDateTime = @param37 ";
                //    //command.CommandText += "SNGFileHash = @param38 ";

                command.CommandText += "WHERE ID = " + txt_ID.Text;

        //    command.Parameters.AddWithValue("@param1", DataGridView1.Rows[i].Cells[1].Value.ToString());
        //    command.Parameters.AddWithValue("@param2", DataGridView1.Rows[i].Cells[2].Value.ToString());
        //    command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells[3].Value.ToString());
        //    command.Parameters.AddWithValue("@param7", DataGridView1.Rows[i].Cells[7].Value.ToString());
        //    command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString());
        //    command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[9].Value.ToString());
        //    command.Parameters.AddWithValue("@param13", DataGridView1.Rows[i].Cells[13].Value.ToString());
        //    command.Parameters.AddWithValue("@param14", DataGridView1.Rows[i].Cells[14].Value.ToString());
        //    command.Parameters.AddWithValue("@param17", DataGridView1.Rows[i].Cells[17].Value.ToString());
        //    command.Parameters.AddWithValue("@param18", DataGridView1.Rows[i].Cells[18].Value.ToString());
        //    command.Parameters.AddWithValue("@param19", DataGridView1.Rows[i].Cells[19].Value.ToString());
        //    command.Parameters.AddWithValue("@param20", DataGridView1.Rows[i].Cells[20].Value.ToString());
        //    command.Parameters.AddWithValue("@param21", DataGridView1.Rows[i].Cells[21].Value.ToString());
        //    command.Parameters.AddWithValue("@param22", DataGridView1.Rows[i].Cells[22].Value.ToString());
        //    command.Parameters.AddWithValue("@param23", DataGridView1.Rows[i].Cells[23].Value.ToString());
        //    command.Parameters.AddWithValue("@param24", DataGridView1.Rows[i].Cells[24].Value.ToString());
        //    command.Parameters.AddWithValue("@param25", DataGridView1.Rows[i].Cells[25].Value.ToString());
        //    command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells[33].Value.ToString());
        //    command.Parameters.AddWithValue("@param34", DataGridView1.Rows[i].Cells[34].Value.ToString());
        //    command.Parameters.AddWithValue("@param35", DataGridView1.Rows[i].Cells[35].Value.ToString());
        //    command.Parameters.AddWithValue("@param36", DataGridView1.Rows[i].Cells[36].Value.ToString());
        //    command.Parameters.AddWithValue("@param37", DataGridView1.Rows[i].Cells[37].Value.ToString());
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
            //MessageBox.Show(das.UpdateCommand.CommandText);
            //das.SelectCommand.CommandText = "SELECT * FROM Tones";
            //// das.Update(dssx, "Tones");
        }
    }

    public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
    {


        //DB_Path = "../../../../tmp\\Files.accdb;";
        using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Tones;", cn);
            da.Fill(dssx, "Tones");
            //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //da.Fill(ds, "PositionType");
            //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //da.Fill(ds, "Badge");
        }
            //MessageBox.Show("test");
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

            DataGridView.AutoGenerateColumns = false;

        DataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                ID,
                Tone_Name,
                CDLC_ID,
                Volume,
                Keyy,
                Is_Custom,
                GearList,
                AmpRack,
                Pedals,
                Description,
                Favorite,
                SortOrder,
                NameSeparator,
                Cabinet,
                PostPedal1,
                PostPedal2,
                PostPedal3,
                PostPedal4,
                PrePedal1,
                PrePedal2,
                PrePedal3,
                PrePedal4,
                Rack1,
                Rack2,
                Rack3,
                Rack4,
                AmpType,
                AmpCategory,
                AmpKnobValues,
                AmpPedalKey,
                CabinetCategory,
                CabinetKnobValues,
                CabinetPedalKey,
                CabinetType,
                lastConversionDateTime,
                lastConverjsonDateTime
            }
        );

        dssx.Tables["Tones"].AcceptChanges();

        bs.DataSource = dssx.Tables["Tones"];
        DataGridView.DataSource = bs;
        //DataGridView.ExpandColumns();
    }

    public class Files
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
        }
        public Files[] files = new Files[10000];
    //Generic procedure to read and parse Tones.DB (&others..soon)
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
                dax.Fill(dus, "Tones");

                var i = 0;
                //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                MaximumSize = dus.Tables[0].Rows.Count;
                foreach (DataRow dataRow in dus.Tables[0].Rows)
                {
                    files[i] = new Files();

                    //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                    files[i].ID = dataRow.ItemArray[0].ToString();
                    files[i].Tone_Name = dataRow.ItemArray[0].ToString();
                    files[i].CDLC_ID = dataRow.ItemArray[0].ToString();
                    files[i].Volume = dataRow.ItemArray[0].ToString();
                    files[i].Keyy = dataRow.ItemArray[0].ToString();
                    files[i].Is_Custom = dataRow.ItemArray[0].ToString();
                    files[i].GearList = dataRow.ItemArray[0].ToString();
                    files[i].AmpRack = dataRow.ItemArray[0].ToString();
                    files[i].Pedals = dataRow.ItemArray[0].ToString();
                    files[i].Description = dataRow.ItemArray[0].ToString();
                    files[i].Favorite = dataRow.ItemArray[0].ToString();
                    files[i].SortOrder = dataRow.ItemArray[0].ToString();
                    files[i].NameSeparator = dataRow.ItemArray[0].ToString();
                    files[i].Cabinet = dataRow.ItemArray[0].ToString();
                    files[i].PostPedal1 = dataRow.ItemArray[0].ToString();
                    files[i].PostPedal2 = dataRow.ItemArray[0].ToString();
                    files[i].PostPedal3 = dataRow.ItemArray[0].ToString();
                    files[i].PostPedal4 = dataRow.ItemArray[0].ToString();
                    files[i].PrePedal1 = dataRow.ItemArray[0].ToString();
                    files[i].PrePedal2 = dataRow.ItemArray[0].ToString();
                    files[i].PrePedal3 = dataRow.ItemArray[0].ToString();
                    files[i].PrePedal4 = dataRow.ItemArray[0].ToString();
                    files[i].Rack1 = dataRow.ItemArray[0].ToString();
                    files[i].Rack2 = dataRow.ItemArray[0].ToString();
                    files[i].Rack3 = dataRow.ItemArray[0].ToString();
                    files[i].Rack4 = dataRow.ItemArray[0].ToString();
                    files[i].AmpType = dataRow.ItemArray[0].ToString();
                    files[i].AmpCategory = dataRow.ItemArray[0].ToString();
                    files[i].AmpKnobValues = dataRow.ItemArray[0].ToString();
                    files[i].AmpPedalKey = dataRow.ItemArray[0].ToString();
                    files[i].CabinetCategory = dataRow.ItemArray[0].ToString();
                    files[i].CabinetKnobValues = dataRow.ItemArray[0].ToString();
                    files[i].CabinetPedalKey = dataRow.ItemArray[0].ToString();
                    files[i].CabinetType = dataRow.ItemArray[0].ToString();
                    files[i].lastConversionDateTime = dataRow.ItemArray[0].ToString();
                    files[i].lastConverjsonDateTime = dataRow.ItemArray[0].ToString();
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
            MessageBox.Show(ee.Message + "Can not open Tones DB connection ! ");
            //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
        return MaximumSize;//files[10000];
    }
}
} 
