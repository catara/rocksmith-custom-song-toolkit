﻿using System;
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

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Cache : Form
    {
        public Cache(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        public bool SaveOK = false;
        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION = "CacheDB";
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory; //when repacking
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath="";
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
                MessageBox.Show("Can not open Cache DB connection in Cache Edit screen ! " + DB_Path);
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i;
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

            if (txt_Arrangements.Text != "") picbx_AlbumArtPath.ImageLocation = txt_AlbumYear.Text.Replace(".dds", ".png");
            SaveOK = true;
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

            //DataGridView1.Rows[i].Cells[0].Value = txt_I.Text;
            //DataGridView1.Rows[i].Cells[1].Value = txt_Artist.Text;
            //DataGridView1.Rows[i].Cells[3].Value = txt_ArtistSort.Text;
            //DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
            if (chbx_Removed.Checked == true) DataGridView1.Rows[i].Cells[8].Value = "Yes";
            else DataGridView1.Rows[i].Cells[8].Value = "No";
            DataGridView1.Rows[i].Cells[10].Value = rtxt_Comments.Text;

            //var DB_Path = "../../../../tmp\\Files.accdb;";
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                //OleDbCommand command = new OleDbCommand();
                //Update StadardizationDB
                //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                command.CommandText = "UPDATE Cache SET ";

                command.CommandText += "Removed = @param8, ";
                command.CommandText += "Comments = @param10 ";
                //command.CommandText += "AlbumArtPath_Correction = @param6 ";
                command.CommandText += "WHERE ID = " + txt_ID.Text;

                command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
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
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {


            //DB_Path = "../../../../tmp\\Files.accdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Cache AS O", cn);
                da.Fill(dssx, "Cache");
                dssx.Dispose();
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
            }
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 30 };
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
                Identifier,
                Artist,
                ArtistSort,
                Album,
                Title,
                AlbumYear,
                Arrangements,
                Removed,
                AlbumArtPath,
                Comments,
                PSARCName,
                SongsHSANPath,
                Platform
            }
            );

            dssx.Tables["Cache"].AcceptChanges();
            var noOfRec = dssx.Tables[0].Rows.Count;
            bs.DataSource = dssx.Tables["Cache"];
            DataGridView.DataSource = bs;
            dssx.Dispose();
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from Cache AS O WHERE Removed=\"No\"", cn);
                da.Fill(dssx, "Cache");
                dssx.Dispose();
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
            }
            lbl_NoRec.Text = noOfRec.ToString() + "/"+ (dssx.Tables[0].Rows.Count- noOfRec).ToString() + " records.";


            //DataGridView.ExpandColumns();
        }

        public class Files
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
            public string Platform { get; set; }        }

        public Files[] files = new Files[10000];

        //Generic procedure to read and parse Cache.DB (&others..soon)
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
                    dax.Fill(dus, "Cache");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    if (MaximumSize > 0)
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

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
                MessageBox.Show(ee.Message + "Can not open Cache DB connection ! ");
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
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb", ""), TempPath, false, RocksmithDLCPath);
            frm.Show();
        }

        private void btn_GenerateHSAN_Click(object sender, EventArgs e)
        {
            generatehsan();
        }
        public void generatehsan()
        {
            DataSet drsx = new DataSet();
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT DISTINCT SongsHSANPath, PSARCName from Cache AS O", cn);
                da.Fill(drsx, "Cache");
                if (drsx.Tables[0].Rows.Count != 0)
                    foreach (DataRow dataRow in drsx.Tables[0].Rows)
                    {
                        var d = dataRow.ItemArray[1].ToString();
                        manipulateHSAN(dataRow.ItemArray[0].ToString());
                        if (d.ToString() == "CACHE")
                        {
                            DirectoryInfo di;
                            var startI = new ProcessStartInfo();
                            var hsanDir = dataRow.ItemArray[0].ToString();
                            startI.FileName = Path.Combine(AppWD, "7za.exe");
                            startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\";// Path.GetDirectoryName();
                            var za = TempPath + "\\0_dlcpacks\\cache_pc\\cache7.7z";
                            if (!Directory.Exists(TempPath + "\\0_dlcpacks\\manifests")) di = Directory.CreateDirectory(TempPath + "\\0_dlcpacks\\manifests");
                            if (!Directory.Exists(TempPath + "\\0_dlcpacks\\manifests\\songs")) di = Directory.CreateDirectory(TempPath + "\\0_dlcpacks\\manifests\\songs");
                            File.Copy(hsanDir, TempPath + "\\0_dlcpacks\\manifests\\songs\\songs.hsan", true);
                            startI.Arguments = String.Format(" a {0} {1}",
                                                                za,
                                                                "manifests\\songs\\songs.hsan");// + platformDLCP TempPath + "\\0_dlcpacks\\cache_pc\\
                            startI.UseShellExecute = true; startI.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            var startInfo = new ProcessStartInfo();
                            var unpackedDir = "";
                            if (chbx_Songs2Cache.Checked) unpackedDir = TempPath + "\\0_dlcpacks\\cache_pc";
                            else unpackedDir = TempPath + "\\0_dlcpacks\\songs_pc";
                            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            var t = TempPath + "\\0_dlcpacks\\manipulated\\cache.psarc";
                            startInfo.Arguments = String.Format(" create -y --lzma -N -o {0} -i {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t))
                                using (var DDC = new Process())
                                {
                                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                        }

                        if (d.ToString() == "RS1Retail")
                        {
                            var startInfo = new ProcessStartInfo();
                            var unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc.psarc";
                            startInfo.Arguments = String.Format(" create -y --zlib -N -o {0} -i {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                                                                                        //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                        }

                        if (d.ToString() == "COMPATIBILITY")
                        {
                            var startInfo = new ProcessStartInfo();
                            var unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydlc_PS3";
                            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc";
                            startInfo.Arguments = String.Format(" create -y --zlib -N -o {0} -i {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                                                                                        //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                        }
                    }
            }
            MessageBox.Show("Songs Removed from the Shipped version of the game");
        }


        public void manipulateHSAN(string hsanPath)
        {
            var inputFilePath =hsanPath;//cache.psarc

            string textfile = "";// File.ReadAllText(inputFilePath);

            //for each timestamp in the xml file take the highest level entry
            if (File.Exists(inputFilePath + ".orig")) File.Copy(inputFilePath + ".orig", inputFilePath, true);
            var fxml = File.OpenText(inputFilePath);
            if (!File.Exists(inputFilePath + ".orig")) File.Copy(inputFilePath, inputFilePath + ".orig", true);
            string tecst = "";
            string line;
            var header = "";
            var linedone = true;
            var songkey = "";
            var footer = "";

            textfile = "{";
            textfile += "\n    \"Entries\" : {";
            var IDD = "";
            var cmd = "";
            line = fxml.ReadLine(); line = fxml.ReadLine();
            //Read and Save Header
            while ((line = fxml.ReadLine()) != null)
            {
                //if (line == "") continue;
                
                if (line.Contains("\"DLCRS1Key\" : [") ) linedone = false;
                if (line.Contains("],")) linedone = true;
                //textfile += line;//if (header == "done") line.Contains("{") &&  && !linedone
                if (line.Contains("\"SongKey\" : \""))
                {
                    songkey = "";
                    songkey = ((line.Trim().ToLower()).Replace("\"songkey\" : \"", "").Replace("\"", "")).Replace(",", "");

                    cmd = "SELECT Removed from Cache AS O WHERE LCASE(Identifier)=\"" + songkey + "\"";
                    using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        DataSet disx = new DataSet();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
                        da.Fill(disx, "Cache");
                        //rtxt_StatisticsOnReadDLCs.Text = "Processing: " + dssx.Tables[0].Rows.Count + " " + songkey + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        if (disx.Tables[0].Rows.Count > 0) IDD = disx.Tables[0].Rows[0].ItemArray[0].ToString();
                        else
                        {
                            IDD = "No";
                            //rtxt_StatisticsOnReadDLCs.Text = "Removing: " + songkey + " " + dssx.Tables[0].Rows.Count  + " " + cmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                        }
                        disx.Dispose();
                    }
                }
                if (line.Contains("},") && linedone)//&& header == "done"
                {
                    if (IDD == "No") textfile += tecst + "\n" + line; //"\n" +
                    //else rtxt_StatisticsOnReadDLCs.Text = "Removed " + songkey + " " + cmd + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    IDD = "";
                    tecst = "";
                    linedone = true;
                }
                else tecst += "\n" + line;
            }
            textfile += "\n" + "    \"InsertRoot\" : \"Static.Songs.Headers\"";
            textfile += "\n" + "}";
            fxml.Close();
            File.WriteAllText(inputFilePath, textfile);
            //rtxt_StatisticsOnReadDLCs.Text = "Songs Removed from the Shipped version of the game" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
        }


        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (SaveOK) SaveRecord();
        }

        private void btn_InvertAll_Click(object sender, EventArgs e)
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

                var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    command.CommandText = "UPDATE Cache SET ";
                    command.CommandText += "Removed = @param8 WHERE Removed='Yes' ";
                    command.Parameters.AddWithValue("@param8", "Maybe");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    connection.Close();
                    command.Dispose(); command = connection.CreateCommand();
                    command.CommandText = "UPDATE Cache SET ";
                        command.CommandText += "Removed = @param9 WHERE Removed='No' ";
                        command.Parameters.AddWithValue("@param9", "Yes");
                        try
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                        connection.Close();
                        command.Dispose(); command = connection.CreateCommand();
                        command.CommandText = "UPDATE Cache SET ";
                            command.CommandText += "Removed = @param10 WHERE Removed='Maybe' ";
                            command.Parameters.AddWithValue("@param10", "No");
                            try
                            {
                                command.CommandType = CommandType.Text;
                                connection.Open();
                                command.ExecuteNonQuery();
                            connection.Close();
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
                MessageBox.Show("DB Removed values have ben inversed ;) Enjoy!");
                }
            }
    }
}