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
using System.Diagnostics;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Standardization : Form
    {
        public Standardization(string txt_DBFolder,string txt_TempPath)
        { 
            InitializeComponent();
        //MessageBox.Show("test0");
        DB_Path = txt_DBFolder;
        TempPath = txt_TempPath;
        }

    private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

    private BindingSource Main = new BindingSource();
    private readonly string MESSAGEBOX_CAPTION = "StandardizationDB";
    //private object cbx_Lead;
    //public DataAccess da = new DataAccess();
    //bcapi
    public string DB_Path = "";
    public string TempPath = "";
    public DataSet dssx = new DataSet();
    //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

    //private BindingSource bsPositions = new BindingSource();
    //private BindingSource bsBadges = new BindingSource();

    private void Standardization_Load(object sender, EventArgs e)
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
        // DB_Path = DB_Path + "\\Files.accdb"; //DLCManager.txt_DBFolder.Text
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
        int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_Artist.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_Artist_Correction.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
            txt_Album.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
            txt_Album_Correction.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            //txt_AlbumArtPath_Correction.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();

           if (txt_AlbumArtPath_Correction.Text != "") picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");

        }

    private void button8_Click(object sender, EventArgs e)
    {
        int i;
        DataSet dis = new DataSet();

        i = DataGridView1.SelectedCells[0].RowIndex;

            //DataGridView1.Rows[i].Cells[0].Value = txt_ID.Text;
            //DataGridView1.Rows[i].Cells[1].Value = txt_Artist.Text;
            DataGridView1.Rows[i].Cells[3].Value = txt_Artist_Correction.Text;
            //DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
            DataGridView1.Rows[i].Cells[5].Value = txt_Album_Correction.Text;
            DataGridView1.Rows[i].Cells[6].Value = txt_AlbumArtPath_Correction.Text;

            //var DB_Path = "../../../../tmp\\Files.accdb;";
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
        {
                //OleDbCommand command = new OleDbCommand();
                //Update StadardizationDB
                //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                command.CommandText = "UPDATE Standardization SET ";

                command.CommandText += "Artist_Correction = @param3, ";
                command.CommandText += "Album_Correction = @param5 ";
                //command.CommandText += "AlbumArtPath_Correction = @param6 ";
                command.CommandText += "WHERE ID = " + txt_ID.Text;

                command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells[3].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param5", DataGridView1.Rows[i].Cells[5].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param6", DataGridView1.Rows[i].Cells[6].Value.ToString() ?? DBNull.Value.ToString());
                try
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
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
            ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
            MessageBox.Show("Song Details Correction Saved");
            //das.SelectCommand.CommandText = "SELECT * FROM Tones";
            //// das.Update(dssx, "Tones");
        }
    }

    public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
    {


        //DB_Path = "../../../../tmp\\Files.accdb;";
        using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID, (SELECT IIF(count(*)>1,\"Yes\",\"\") as Suspect from Standardization AS O WHERE LCASE(S.Artist)=LCASE(O.Artist) and LCASE(S.Album)=LCASE(O.Album)) as Suspect, Artist, Artist_Correction, Album, Album_Correction, AlbumArt_Correction FROM Standardization as S ORDER BY Artist, Album;", cn);
            da.Fill(dssx, "Standardization");
            //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //da.Fill(ds, "PositionType");
            //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //da.Fill(ds, "Badge");
        }
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 40 };
            DataGridViewTextBoxColumn Suspect = new DataGridViewTextBoxColumn { DataPropertyName = "Suspect", HeaderText = "Suspect ", Width = 40 };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist ", Width = 185 };
            DataGridViewTextBoxColumn Artist_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Correction", HeaderText = "Artist_Correction ", Width = 185 };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album ", Width = 185 };
            DataGridViewTextBoxColumn Album_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Correction", HeaderText = "Album_Correction ", Width = 185 };
            DataGridViewTextBoxColumn AlbumArtPath_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath_Correction", HeaderText = "AlbumArtPath_Correction ", Width = 495 };


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
                Suspect,
                Artist,
                Artist_Correction,
                Album,
                Album_Correction,
                AlbumArtPath_Correction
            }
        );

        dssx.Tables["Standardization"].AcceptChanges();

        bs.DataSource = dssx.Tables["Standardization"];
        DataGridView.DataSource = bs;
        //DataGridView.ExpandColumns();
    }

    public class Files
    {
            public string ID { get; set; }
            public string Artist { get; set; }
            public string Artist_Correction { get; set; }
            public string Album { get; set; }
            public string Album_Correction { get; set; }
            public string AlbumArtPath_Correction { get; set; }
    }

    public Files[] files = new Files[10000];
    
    //Generic procedure to read and parse Standardization.DB (&others..soon)
    public int SQLAccess(string cmd)
    {
        //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
        //Files[] files = new Files[10000];

        var MaximumSize = 0;

        //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
        try
        {
            MessageBox.Show(DB_Path);
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + DB_Path))
            {
                DataSet dus = new DataSet();
                OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                dax.Fill(dus, "Standardization");

                var i = 0;
                //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                MaximumSize = dus.Tables[0].Rows.Count;
                foreach (DataRow dataRow in dus.Tables[0].Rows)
                {
                    files[i] = new Files();

                    //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                    files[i].ID = dataRow.ItemArray[0].ToString();
                    files[i].Artist = dataRow.ItemArray[2].ToString();
                    files[i].Artist_Correction = dataRow.ItemArray[3].ToString();
                    files[i].Album = dataRow.ItemArray[4].ToString();
                    files[i].Album_Correction = dataRow.ItemArray[5].ToString();
                    files[i].AlbumArtPath_Correction = dataRow.ItemArray[6].ToString();
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
            MessageBox.Show(ee.Message + "Can not open Standardization DB connection ! ");
            //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
        return MaximumSize;//files[10000];
    }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb", ""), TempPath);
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DLCManager v1 = new DLCManager();
            string txt = DB_Path.Replace("\\Files.accdb", "");
            //MessageBox.Show(txt);
            v1.Translation_And_Correction(txt);
        }

        private void btn_CopyArtist2ArtistSort_Click(object sender, EventArgs e)
        {
            var cmd1 = "";
            //DB_Path = DB_Path.Replace("dlc\\Files.accdb","dlc");
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path.Replace("\\Files.accdb;", "")))
                {
                    DataSet dus = new DataSet();
                    cmd1 = "UPDATE Main SET Artist_Sort = Artist";
                    OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
                    das.Fill(dus, "Main");
                    das.Dispose();
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                Console.WriteLine(ee.Message);
                //continue;
            }
            MessageBox.Show("ArtistSort is now the same as Artist");
        }

        private void btn_CopyTitle2TitleSort_Click(object sender, EventArgs e)
        {
            var cmd1 = "";
            //var DB_Path = DB_Path + "\\Files.accdb";
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    cmd1 = "UPDATE Main SET Song_Title_Sort = Song_Title";
                    OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
                    das.Fill(dus, "Main");
                    das.Dispose();
                }
            }
            catch (System.IO.FileNotFoundException ee)
            {
                // To inform the user and continue is 
                // sufficient for this demonstration. 
                // Your application may require different behavior.
                Console.WriteLine(ee.Message);
                //continue;
            }
            MessageBox.Show("TitleSort is now the same as Title");
        }
    }
} 
