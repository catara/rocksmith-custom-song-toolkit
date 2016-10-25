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
using RocksmithToolkitLib; //config
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.XmlRepository;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Standardization : Form
    {
        public Standardization(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION = "StandardizationDB";
        public bool SaveOK = false;
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;

        private void Standardization_Load(object sender, EventArgs e)
        {
            Populate(ref DataGridView1, ref Main);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void _SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MAYBE HERE CAN ACTIVATE THE INDIV CELLS
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            var line = -1;
            line = DataGridView1.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {
            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_Artist.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_Artist_Correction.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
            txt_Album.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
            txt_Album_Correction.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            txt_AlbumArt_Correction.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
            txt_Comments.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
            txt_Artist_Short.Text = DataGridView1.Rows[i].Cells[8].Value.ToString();
            txt_Album_Short.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
            txt_Year_Correction.Text = DataGridView1.Rows[i].Cells[10].Value.ToString();
            if (txt_AlbumArt_Correction.Text != "") picbx_AlbumArtPath.ImageLocation = txt_AlbumArt_Correction.Text.Replace(".dds", ".png");
            //if (DataGridView1.Rows[i].Cells["Default_Cover"].Value.ToString() == "Yes") chbx_Default_Cover.Checked = true;
            //    else chbx_Default_Cover.Checked = false;
            if (chbx_AutoSave.Checked) SaveOK = true;
            else SaveOK = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs)
        {
            bs.DataSource = null;
            dssx.Dispose();
            //try
            //{
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            dssx.Clear();
            //DataSet ds = new DataSet();
            dssx = DLCManager.SelectFromDB("Standardization", "SELECT ID, (SELECT IIF(count(*)>1,\"Yes\",\"\") as Suspect from Standardization AS O WHERE LCASE(S.Artist)=LCASE(O.Artist) and LCASE(S.Artist_Correction)=LCASE(O.Artist_Correction) and LCASE(S.Album_Correction)=LCASE(O.Album_Correction) and LCASE(S.Album)=LCASE(O.Album)) as Suspect, Artist, Artist_Correction, Album, Album_Correction, AlbumArt_Correction, Comments, Artist_Short, Album_Short, Year_Correction FROM Standardization as S ORDER BY Artist, Album, Artist_Correction, Album_Correction;");
            //OleDbDataAdapter da = new OleDbDataAdapter(cmd, cn);
            //da.Fill(dssx, "Standardization");
            var noOfRec = dssx.Tables[0].Rows.Count;
            lbl_NoRec.Text = noOfRec.ToString() + " records.";
            //}
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID ", Width = 40 };
            DataGridViewTextBoxColumn Suspect = new DataGridViewTextBoxColumn { DataPropertyName = "Suspect", HeaderText = "Suspect ", Width = 40 };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist ", Width = 185 };
            DataGridViewTextBoxColumn Artist_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Correction", HeaderText = "Artist_Correction ", Width = 185 };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album ", Width = 185 };
            DataGridViewTextBoxColumn Album_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Correction", HeaderText = "Album_Correction ", Width = 185 };
            DataGridViewTextBoxColumn AlbumArt_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Correction", HeaderText = "AlbumArt_Correction ", Width = 495 };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments ", Width = 195 };
            DataGridViewTextBoxColumn Artist_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Short", HeaderText = "Artist_Short ", Width = 185 };
            DataGridViewTextBoxColumn Album_Short = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Short", HeaderText = "Album_Short ", Width = 45 };
            DataGridViewTextBoxColumn Year_Correction = new DataGridViewTextBoxColumn { DataPropertyName = "Year_Correction", HeaderText = "Year_Correction ", Width = 85 };
            DataGridView.AutoGenerateColumns = false;

            DataGridView.Columns.AddRange(new DataGridViewColumn[]
                {
                        ID,
                        Suspect,
                        Artist,
                        Artist_Correction,
                        Album,
                        Album_Correction,
                        AlbumArt_Correction,
                        Comments,
                        Artist_Short,
                        Album_Short,
                        Year_Correction
                }
            );

            bs.ResetBindings(false);
            dssx.Tables["Standardization"].AcceptChanges();
            bs.DataSource = dssx.Tables["Standardization"];
            DataGridView.DataSource = null;
            DataGridView1.DataSource = bs;
            DataGridView1.Refresh();
            dssx.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the Standardization DB", false, false);
            //    frm1.ShowDialog();
            //    return;
            //}
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
            public string Suspect { get; set; }
            public string Artist { get; set; }
            public string Artist_Correction { get; set; }
            public string Album { get; set; }
            public string Album_Correction { get; set; }
            public string AlbumArt_Correction { get; set; }
            public string Comments { get; set; }
            public string Artist_Short { get; set; }
            public string Album_Short { get; set; }
            public string Year_Correction { get; set; }
        }

        private Files[] files = new Files[10000];

        //Generic procedure to read and parse Standardization.DB (&others..soon)
        public int SQLAccess(string cmd)
        {
            //var DB_Path = txt_DBFolder.Text + "\\Files.mdb;";
            //Files[] files = new Files[10000];

            var MaximumSize = 0;

            //rtxt_StatisticsOnReadDLCs.Text += "\n  ee= ";
            //try
            //{
            //MessageBox.Show(DB_Path);
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    DataSet dus = new DataSet();
            //    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
            //    dax.Fill(dus, "Standardization");
            DataSet dus = new DataSet(); dus = DLCManager.SelectFromDB("Standardization", cmd);
            var i = 0;
            //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
            MaximumSize = dus.Tables[0].Rows.Count;
            foreach (DataRow dataRow in dus.Tables[0].Rows)
            {
                files[i] = new Files();

                //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                files[i].ID = dataRow.ItemArray[0].ToString();
                files[i].Suspect = dataRow.ItemArray[1].ToString();
                files[i].Artist = dataRow.ItemArray[2].ToString();
                files[i].Artist_Correction = dataRow.ItemArray[3].ToString();
                files[i].Album = dataRow.ItemArray[4].ToString();
                files[i].Album_Correction = dataRow.ItemArray[5].ToString();
                files[i].AlbumArt_Correction = dataRow.ItemArray[6].ToString();
                files[i].Comments = dataRow.ItemArray[6].ToString();
                files[i].Artist_Short = dataRow.ItemArray[8].ToString();
                files[i].Album_Short = dataRow.ItemArray[9].ToString();
                files[i].Year_Correction = dataRow.ItemArray[10].ToString();
                i++;
            }
            //Closing Connection
            //        dax.Dispose();
            //        cnn.Close();
            //        //rtxt_StatisticsOnReadDLCs.Text += i;
            //        //var ex = 0;
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    MessageBox.Show(ee.Message + "Can not open Standardization DB connection ! ");
            //    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            ////rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb", ""), TempPath, false, "", AllowEncriptb, AllowORIGDeleteb);
            frm.Show();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            DLCManager v1 = new DLCManager();
            string txt = DB_Path.Replace("\\Files.accdb", "");
            //MessageBox.Show(txt);
            v1.Translation_And_Correction(txt);
            //advance or step back in the song list
            int i = 0;
            if (DataGridView1.Rows.Count > 1)
            {
                var prev = DataGridView1.SelectedCells[0].RowIndex;
                if (DataGridView1.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        i = DataGridView1.SelectedCells[0].RowIndex;
                        rowindex = i;
                        DataGridView1.Rows[rowindex - 1].Selected = true;
                        DataGridView1.Rows[rowindex].Selected = false;
                        row = DataGridView1.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    i = DataGridView1.SelectedCells[0].RowIndex;
                    rowindex = i;
                    DataGridView1.Rows[rowindex + 1].Selected = true;
                    DataGridView1.Rows[rowindex].Selected = false;
                    row = DataGridView1.Rows[rowindex + 1];
                }
            }
            ChangeRow();
        }

        private void btn_CopyArtist2ArtistSort_Click(object sender, EventArgs e)
        {
            //var cmd1 = "";
            ////DB_Path = DB_Path.Replace("dlc\\Files.accdb","dlc");
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path.Replace("\\Files.accdb;", "")))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET Artist_Sort = Artist";
            //        OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
            //        das.Fill(dus, "Main");
            //        das.Dispose();
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}
            DataSet dxr = new DataSet(); dxr = DLCManager.UpdateDB("Main", "UPDATE Main SET Artist_Sort = Artist");
            MessageBox.Show("ArtistSort is now the same as Artist");
        }

        private void btn_CopyTitle2TitleSort_Click(object sender, EventArgs e)
        {
            //var cmd1 = "";
            ////var DB_Path = DB_Path + "\\Files.accdb";
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET Song_Title_Sort = Song_Title";
            //        OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
            //        das.Fill(dus, "Main");
            //        das.Dispose();
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}
            DataSet dxr = new DataSet(); dxr = DLCManager.UpdateDB("Main", "UPDATE Main SET Song_Title_Sort = Song_Title");
            MessageBox.Show("TitleSort is now the same as Title");
        }

        public void button1_Click_2(object sender, EventArgs e)
        {
            // MakeCover(DB_Path, txt_AlbumArt.Text, txt_Artist.Text, txt_Album.Text);
        }

        public static void MakeCover()//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {
            //var cmd1 = "";
            ////var DB_Path = DB_Path + "\\Files.accdb";
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBs_Path))
            //    {
            //        DataSet dus = new DataSet();
            //        cmd1 = "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"";
            //        OleDbDataAdapter das = new OleDbDataAdapter(cmd1, cnn);
            //        das.Fill(dus, "Main");
            //        das.Dispose();
            //    }
            //}
            //catch (System.IO.FileNotFoundException ee)
            //{
            //    
            //    
            //    
            //    Console.WriteLine(ee.Message);
            //    //continue;
            //}

            var NoRec = 0;
            //DataSet dssx = new DataSet();
            //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBs_Path))
            //{
            //    OleDbDataAdapter da = new OleDbDataAdapter("SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";", cn);
            //    da.Fill(dssx, "Standardization");
            //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
            //da.Fill(ds, "PositionType");
            //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
            //da.Fill(ds, "Badge");
            //}

            DataSet dgt = new DataSet(); dgt = DLCManager.SelectFromDB("Standardization", "SELECT Artist, Album, AlbumArt_Correction FROM Standardization WHERE (AlbumArt_Correction <> \"\") GROUP BY Artist,Album,AlbumArt_Correction;");
            //NoRec = dgt.Tables[0].Rows.Count;
            //pB_ReadDLCs.Maximum = NoRec;
            foreach (DataRow dataRow in dgt.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var artpath_c = dataRow.ItemArray[2].ToString();
                var cmd1 = "UPDATE Main SET AlbumArtPath = \"" + artpath_c + "\" WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\";";
                dgt = DLCManager.UpdateDB("Main", cmd1 + ";");
                dgt=DLCManager.SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" and Album=\"" + album_c + "\";"); try { NoRec = dgt.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET AlbumArt_Correction = \"" + artpath_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\");";
                dgt = DLCManager.UpdateDB("Main", cmd1 + ";");
            }
            //DataSet dxr = new DataSet(); dxr = DLCManager.UpdateDB("Main", "UPDATE Main SET AlbumArt = \"" + AlbumArt + "\" WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\"");
            //DataSet dssx = new DataSet(); dxr = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE Artist=\"" + Artist + "\" and Album=\"" + Albums + "\";");


            // lbl_NoRec = noOfRec.ToString() + " records.";
            //MessageBox.Show("Cover has been defaulted as Cover to " + NoRec.ToString() + " songs");
        }

        public static void ApplyArtistShort()//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;
            DataSet dfz = new DataSet(); dfz = DLCManager.SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short FROM Standardization WHERE (Artist_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist), Artist_Short;");

            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var short_c = dataRow.ItemArray[1].ToString();
                var cmd1 = "UPDATE Main SET Artist_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\"";
                DataSet dus = DLCManager.UpdateDB("Main", cmd1 + ";");
                dus=DLCManager.SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\""); try { norec = dus.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET Artist_Short = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\"";
                dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            }

            //MessageBox.Show("Artist Short Name has been defaulted onto " + norec.ToString() + " songs");
        }

        public static void ApplyAlbumShort()//(string DBs_Path)//, string AlbumArt, string Artist, string Albums)
        {//continue;
            //}

            var norec = 0;

            DataSet dfz = new DataSet(); dfz = DLCManager.SelectFromDB("Standardization", "SELECT iif(Artist_Correction<>\"\", Artist_Correction, Artist), iif(Album_Correction<>\"\", Album_Correction, Album), Album_short FROM Standardization WHERE (Album_Short <> \"\") GROUP BY iif(Artist_Correction<>\"\", Artist_Correction, Artist),Album_short,iif(Album_Correction<>\"\",Album_Correction,Album);");
            //pB_ReadDLCs.Maximum = norec;
            //pB_ReadDLCs.Value = 0;
            foreach (DataRow dataRow in dfz.Tables[0].Rows)
            {
                var artist_c = dataRow.ItemArray[0].ToString();
                var album_c = dataRow.ItemArray[1].ToString();
                var short_c = dataRow.ItemArray[2].ToString();
                var cmd1 = "UPDATE Main SET Album_ShortName = \"" + short_c + "\" WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";";
                DataSet dus = DLCManager.UpdateDB("Main", cmd1);
                dus = DLCManager.SelectFromDB("Main", "SELECT * FROM Main WHERE Artist=\"" + artist_c + "\" AND Album=\"" + album_c + "\";"); try { norec = dus.Tables[0].Rows.Count; } catch { }
                cmd1 = "UPDATE Standardization SET Album_Short = \"" + short_c + "\" WHERE (Artist=\"" + artist_c + "\" OR Artist_Correction=\"" + artist_c + "\") AND (Album=\"" + album_c + "\" OR Album_Correction=\"" + album_c + "\")";
                dus = DLCManager.UpdateDB("Main", cmd1 + ";");
            }
            //var noOfRec = dgt.Tables[0].Rows.Count;
            //lbl_NoRec = norec.ToString() + " records.";
            //MessageBox.Show("Album Short has been defaulted onto " + norec.ToString() + " songs");
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && (txt_Artist_Correction.Text != "" || txt_Album_Correction.Text != "" || txt_Year_Correction.Text != "" || txt_AlbumArt_Correction.Text != "" || txt_Artist_Short.Text != "" || txt_Album_Short.Text != "")) SaveRecord();//&& SaveOK
            else;
        }

        private void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();
            //try {
            i = DataGridView1.SelectedCells[0].RowIndex;
            //if (txt_Artist_Correction.Text != "")
            DataGridView1.Rows[i].Cells[3].Value = txt_Artist_Correction.Text;
            //if (txt_Album_Correction.Text != "")
            DataGridView1.Rows[i].Cells[5].Value = txt_Album_Correction.Text;
            //if (txt_AlbumArt_Correction.Text != "")
            DataGridView1.Rows[i].Cells[6].Value = txt_AlbumArt_Correction.Text;
            //if (txt_Comments.Text != "")
            DataGridView1.Rows[i].Cells[7].Value = txt_Artist_Short.Text;
            //if (txt_Artist_Short.Text != "")
            DataGridView1.Rows[i].Cells[8].Value = txt_Artist_Short.Text;
            //if (txt_Year_Correction.Text != "")
            DataGridView1.Rows[i].Cells[9].Value = txt_Year_Correction.Text;
            //if (txt_Album_Short.Text != "")
            DataGridView1.Rows[i].Cells[10].Value = txt_Year_Correction.Text;
            //Main.EndEdit();
            ////IDataAdapter.Update(dataTable);
            //Main.ResetBindings(false);


            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path);
            var command = connection.CreateCommand();
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                command.CommandText = "UPDATE Standardization SET ";

                command.CommandText += "Artist_Correction = @param3, ";
                command.CommandText += "Album_Correction = @param5, ";
                command.CommandText += "AlbumArt_Correction = @param6, ";
                command.CommandText += "Comments = @param7, ";
                command.CommandText += "Artist_Short = @param8, ";
                command.CommandText += "Album_Short = @param9, ";
                command.CommandText += "Year_Correction = @param10 ";
                command.CommandText += "WHERE ID = " + txt_ID.Text;
                command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells[3].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param5", DataGridView1.Rows[i].Cells[5].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param6", DataGridView1.Rows[i].Cells[6].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param7", DataGridView1.Rows[i].Cells[7].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[9].Value.ToString() ?? DBNull.Value.ToString());
                command.Parameters.AddWithValue("@param10", DataGridView1.Rows[i].Cells[10].Value.ToString() ?? DBNull.Value.ToString());
                try
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    //Main.EndEdit();
                    //IDataAdapter.Update(dataTable);
                    //Main.ResetBindings(false);

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
                if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Correction Saved");
            }
            //catch (Exception ex)
            //{ }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DLCManager.DeleteFromDB("Standardization", "DELETE * FROM Standardization WHERE ID IN (" + txt_ID.Text + ")");
            //var cmd = "DELETE * FROM Standardization WHERE ID IN (" + txt_ID.Text + ")";
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            //    DataSet dhs = new DataSet();
            //    OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
            //    dhx.Fill(dhs, "Standardization");
            //    dhx.Dispose();
            //}


            //redresh 
            Populate(ref DataGridView1, ref Main);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();

            //advance or step back in the song list
            if (DataGridView1.Rows.Count > 1)
            {
                var prev = DataGridView1.SelectedCells[0].RowIndex;
                if (DataGridView1.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        i = DataGridView1.SelectedCells[0].RowIndex;
                        rowindex = i;
                        DataGridView1.Rows[rowindex - 1].Selected = true;
                        DataGridView1.Rows[rowindex].Selected = false;
                        row = DataGridView1.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    i = DataGridView1.SelectedCells[0].RowIndex;
                    rowindex = i;
                    DataGridView1.Rows[rowindex + 1].Selected = true;
                    DataGridView1.Rows[rowindex].Selected = false;
                    row = DataGridView1.Rows[rowindex + 1];
                }
            }
        }

        private void chbx_AutoSave_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
