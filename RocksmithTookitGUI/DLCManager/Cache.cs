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

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Cache : Form
    {
        public Cache(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            AllowEncriptb = AllowEncript;
            AllowORIGDeleteb = AllowORIGDelete;
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
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
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
            if (DataGridView1.Rows[i].Cells[14].Value != null) txt_AudioPath.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
            if (DataGridView1.Rows[i].Cells[15].Value != null) txt_AudioPreviewPath.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();

            //if (txt_Arrangements.Text != "") 
            picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text;//.Replace(".dds", ".png");
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
                Platform,
                AudioPath,
                AudioPreviewPath
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
            public string Platform { get; set; }
            public string AudioPath { get; set; }
            public string AudioPreviewPath { get; set; }
        }

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
                        files[i].AudioPath = dataRow.ItemArray[14].ToString();
                        files[i].AudioPreviewPath = dataRow.ItemArray[15].ToString();
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
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb", ""), TempPath, false, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb);
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
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT DISTINCT SongsHSANPath, PSARCName, Platform from Cache AS O;", cn);
                da.Fill(drsx, "Cache");
                pB_ReadDLCs.Value = 0;
                if (drsx.Tables[0].Rows.Count != 0)
                    pB_ReadDLCs.Maximum = 2 * drsx.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in drsx.Tables[0].Rows)
                    {
                        pB_ReadDLCs.Increment(1);
                        var dpsarc = dataRow.ItemArray[1].ToString();
                        var platfor = dataRow.ItemArray[2].ToString();
                        var HSAN = dataRow.ItemArray[0].ToString();
                        manipulateHSAN(HSAN);
                        if (dpsarc.ToString() == "CACHE")
                        {
                            DirectoryInfo di;
                            var startI = new ProcessStartInfo();
                            var hsanDir = dataRow.ItemArray[0].ToString();
                            startI.FileName = Path.Combine(AppWD, "7za.exe");
                            startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\";// Path.GetDirectoryName();
                            var za = TempPath + "\\0_dlcpacks\\cache_" + platfor + "\\cache7.7z";
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
                            if (chbx_Songs2Cache.Checked) unpackedDir = TempPath + "\\0_dlcpacks\\cache_" + platfor;//((platfor == "PS3") ? "_ps3" : ((platfor == "Mac") ? "_m" : ((platfor == "Pc") ? "_p" : ""));
                            else unpackedDir = TempPath + "\\0_dlcpacks\\songs_" + platfor;
                            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\psarc.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            var t = TempPath + "\\0_dlcpacks\\manipulated\\cache_" + platfor+".psarc";
                            startInfo.Arguments = String.Format(" create -y --lzma -S -N -o {0} -i {1}",
                                                                t,
                                                                unpackedDir);// + platformDLCP
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                            rtxt_Comments.Text = startInfo.FileName + " "+startInfo.Arguments;
                            //if (!File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }
                            //manual workaround for wrongly packing the files
                            MessageBox.Show("For CACHE.PSCARC-RS2014 as the Toolkit cannot pack with \"NO compression\" and PSARC.EXE(2011 version) cannot pack correctly,\n a manual workaround exists:\n1. Download&install TotalCommander http://ghisler.com/download.htm \n2. Download the psarc plugin 2013 http://www.totalcmd.net/plugring/PSARC.html \n3. While in TC open the zip archive with the plugin&install the plugin\n\n4. Enter the manipulated/cache.psarc and Pack with External No Compression LZMA\n(take out the _PS3/_Pc/_Mac from the name\n5. Copy in the Game(not DLC) DIR", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                        }

                        if (dpsarc.ToString() == "RS1Retail")
                        {
                            var startInfo = new ProcessStartInfo();
                            var unpackedDir = HSAN.Substring(0, HSAN.IndexOf("\\manifests"));//unpackedDir = TempPath + "\\0_dlcpacks\\rs1compatibilitydisc_PS3";
                            startInfo.FileName = Path.Combine(AppWD, "packer.exe");
                            startInfo.WorkingDirectory = unpackedDir;// Path.GetDirectoryName();
                            var t = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc"+((platfor=="PS3") ? "" : ((platfor=="Mac") ? "_m" :((platfor=="Pc") ?"_p" :"")))+".psarc";

                            //if (AllowORIGDeleteb) if (File.Exists(unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan.orig")) File.Delete(unpackedDir + "\\manifests\\songs_rs1disc\\songs_rs1disc.hsan.orig");
                            try //Copy dir
                            {
                                string source_dir = @unpackedDir;
                                string destination_dir = @unpackedDir.Replace("0_dlcpacks\\", "0_dlcpacks\\manipulated\\temp\\");

                                // substring is to remove destination_dir absolute path (E:\).

                                // Create subdirectory structure in destination    
                                foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
                                {
                                    Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
                                    // Example:
                                    //     > C:\sources (and not C:\E:\sources)
                                }

                                foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                                {
                                    if (AllowORIGDeleteb && (file_name.IndexOf(".ogg") > 0 || (file_name.IndexOf(".orig") > 0))) ;
                                    else File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                                }
                                Directory.Delete(source_dir, true);
                                //var ee = "";
                                //rtxt_StatisticsOnReadDLCs.Text = " DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                                unpackedDir = destination_dir;
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
                            //MessageBox.Show("As the toolkit cannot pack&encrypt/I don't know how,\n a manual workaround exists:\n1. Download&install TotalCommander http://ghisler.com/download.htm \n2. Download the psarc plugin 2013 http://www.totalcmd.net/plugring/PSARC.html \n3. While in TC open the zip archive witht he plugin&install the plugin\n4. Enter the manipulated/rs1compatibilitydisc.psarc and Pack with External No Compression Zlib Compression Ratio 4\n5. Encrypt using the aldotool http://ps3tools.aldostools.org/ps3_edattool_gui.rar \n6. Copy in the DLC DIR", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (platfor == "PS3" && AllowEncriptb)
                            {
                                var startI = new ProcessStartInfo();
                                var hsanDir = dataRow.ItemArray[0].ToString();
                                startI.FileName = Path.Combine(AppWD, "DLCManager\\edattool.exe");
                                startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\manipulated\\";// Path.GetDirectoryName();
                                var za = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc.psarc.edat";
                                var zi = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc.psarc";
                                startI.Arguments = String.Format(" encrypt -custom:CB4A06E85378CED307E63EFD1084C19D UP0001-BLUS31182_00-ROCKSMITHPATCH01 00 00 00 {0} {1}",
                                                                    zi,
                                                                    za);
                                startI.UseShellExecute = true; startI.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
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
                            startInfo.FileName = Path.Combine(AppWD, "packer.exe");
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
                                    // Example:
                                    //     > C:\sources (and not C:\E:\sources)
                                }

                                foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
                                {
                                    if (AllowORIGDeleteb && (file_name.IndexOf(".ogg") > 0 || (file_name.IndexOf(".orig") > 0))) ;
                                    else File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                                }
                                //Directory.Delete(source_dir, true); DONT DELETE

                                //var ee = "";
                                //rtxt_StatisticsOnReadDLCs.Text = " DIR Moved" + "..." + rtxt_StatisticsOnReadDLCs.Text;
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
                            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

                            //if (!File.Exists(t))
                            using (var DDC = new Process())
                            {
                                DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 90 * 1); //wait 1min
                                                                                                        //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            }

                            //rename the songs_rs1dlc folder to songs to enable the read of Browser function to work                                
                            //renamedir(unpackedDir + "\\manifests\\songs", unpackedDir + "\\manifests\\songs_rs1dlc");
                            //rtxt_StatisticsOnReadDLCs.Text = "packed CACHE \n" + rtxt_StatisticsOnReadDLCs;
                            //MessageBox.Show("As the toolkit cannot pack&encrypt/I don't know how,\n a manual workaround exists:\n1. Download&install TotalCommander http://ghisler.com/download.htm \n2. Download the psarc plugin 2013 http://www.totalcmd.net/plugring/PSARC.html \n3. While in TC open the zip archive witht he plugin&install the plugin\n4. Enter the manipulated/rs1compatibilitydisc.psarc and Pack with External No Compression Zlib Compression Ratio 4\n5. Encrypt using the aldotool http://ps3tools.aldostools.org/ps3_edattool_gui.rar \n6. Copy in the folder containing DLC DIR", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            if (platfor == "PS3" && AllowEncriptb)
                            {
                                var startI = new ProcessStartInfo();
                                var hsanDir = dataRow.ItemArray[0].ToString();
                                startI.FileName = Path.Combine(AppWD, "DLCManager\\edattool.exe");
                                startI.WorkingDirectory = TempPath + "\\0_dlcpacks\\manipulated\\";// Path.GetDirectoryName();
                                var za = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc.edat";
                                var zi = TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc.psarc";

                                startI.Arguments = String.Format(" encrypt -custom:CB4A06E85378CED307E63EFD1084C19D UP0001-BLUS31182_00-ROCKSMITHPATCH01 00 00 00 {0} {1}",
                                                                    zi,
                                                                    za);
                                startI.UseShellExecute = true; startI.CreateNoWindow = false; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;
                                using (var DDC = new Process())
                                {
                                    DDC.StartInfo = startI; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                                                                                                         //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                }
                            }
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
            var lastline = false; //if the last song is not removed then the end should be appended

            textfile = "{";
            textfile += "\n    \"Entries\" : {";
            var IDD = "";
            var cmd = "";
            line = fxml.ReadLine(); line = fxml.ReadLine();
            //Read and Save Header
            while ((line = fxml.ReadLine()) != null)
            {
                if (line.Contains("InsertRoot")) break; //got to the end so
                
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
                else textfile = (textfile+"},").Replace("}, },", "}\n         },") +"\n" + "    \"InsertRoot\" : \"Static.Songs.Headers\"";

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
            else if (cbx_Format.Text == "PC" || cbx_Format.Text == "Mac" )
            {
                var platfrm= (cbx_Format.Text == "PC" ? "_p": (cbx_Format.Text == "Mac" ? "_m":"") );
                var dest = "";
                if (RocksmithDLCPath.IndexOf("Rocksmith\\DLC") > 0)
                {
                    dest = RocksmithDLCPath;//!File.Exists(
                    File.Copy(RocksmithDLCPath + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc" + platfrm + ".psarc" + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest, true);

                    File.Copy(dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", true);

                    dest = RocksmithDLCPath.Replace("\\DLC", "");
                    File.Copy(dest + "\\cache.psarc", dest + "\\cache.psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\cache" + cbx_Format.Text + ".psarc", dest + "\\cache.psarc", true);
                }
                else if (RocksmithDLCPath != txt_FTPPath.Text)
                {
                    dest = txt_FTPPath.Text;//!File.Exists(
                    File.Copy(dest + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydlc" + platfrm + ".psarc" + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest, true);

                    File.Copy(dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\rs1compatibilitydisc" + platfrm + ".psarc", dest + "\\rs1compatibilitydisc" + platfrm + ".psarc", true);

                    File.Copy(dest + "\\cache.psarc", dest + "\\cache.psarc.orig", true);
                    File.Copy(TempPath + "\\0_dlcpacks\\manipulated\\cache" + cbx_Format.Text + ".psarc", dest + "\\cache.psarc", true);
                }
                else MessageBox.Show("Chose a different path to save");
            }
        }

        public void FTPFile(string filel, string filen)
        {
            // Get the object used to communicate with the server.
            var ddd = filel +filen;
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

            byte[] b = File.ReadAllBytes(TempPath + "\\0_dlcpacks\\manipulated\\"+filen);

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
            if (chbx_FTP2.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 FAV - BLES01862/PS3_GAME/USRDIR/";
        }

        private void chbx_FTP1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_FTP1.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 ALL DLC - BLUS31182/PS3_GAME/USRDIR/";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new VistaFolderBrowserDialog())
            {
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                var temppath = fbd.SelectedPath;
                txt_VLCPath.Text = temppath;
                //-Save the location in the Config file/reg
                //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
            }
        }

        private void chbx_VLCHome_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_VLCHome.Checked) txt_VLCPath.Text = "DLCManager\\VLCPortable.exe";
        }

        private void button12_Click(object sender, EventArgs e)
        {   //alternative impl could use http://nvorbis.codeplex.com/documentation
            //txt_VLCPath.Text = "DLCManager\\VLCPortable.exe" + " ";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_AudioPreviewPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p {0}",
                                                t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

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

        private void button4_Click(object sender, EventArgs e)
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

        private void button11_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_AudioPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p {0}", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var Pltfrm = txt_Platform.Text;
            if (Pltfrm == "Platform") MessageBox.Show("Select 1 respresentative of THE desire Source plafrom");
            else
            {
                var cmd = "SELECT Identifier, Removed, Comments FROM Cache AS O WHERE Platform=\"" + Pltfrm + "\"";
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet disx = new DataSet();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
                    da.Fill(disx, "Cache");



                    var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                    var command = connection.CreateCommand();
                    pB_ReadDLCs.Value = 0;

                    foreach (DataRow dataRow in disx.Tables[0].Rows)
                    {
                        pB_ReadDLCs.Maximum = 2 * disx.Tables[0].Rows.Count;
                        pB_ReadDLCs.Increment(1);
                        var iden = dataRow.ItemArray[0].ToString();
                        var remov = dataRow.ItemArray[1].ToString();
                        var comm = dataRow.ItemArray[2].ToString();
                        ////dssx = DataGridView1;
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            command.CommandText = "UPDATE Cache SET ";
                            command.CommandText += "Removed = @param8, Comments = @param9 WHERE Identifier=\"" + iden + "\" ";
                            command.Parameters.AddWithValue("@param8", remov);
                            command.Parameters.AddWithValue("@param9", comm);
                            try
                            {
                                command.CommandType = CommandType.Text;
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
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
                                if (connection != null) connection.Close();
                            }                            
                        }                        
                    }
                    MessageBox.Show("Current Selected Platform REMOVED setting have been spread along the other Loaded platforms ;) Enjoy!");
                }
            }
        }

        private void cbx_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Format.Text=="PS3")
            {
                chbx_FTP1.Enabled = true;
                chbx_FTP2.Enabled = true;
            }
            else
            {
                chbx_FTP1.Enabled=false;
                chbx_FTP2.Enabled=false;
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
    }
}
