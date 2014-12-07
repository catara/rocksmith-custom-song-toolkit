using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
using RocksmithToolkitGUI;
using RocksmithToolkitLib.Extensions;
using System.IO;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class MainDB : Form
    {
        public MainDB(string txt_DBFolder, string txt_TempPath)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            DB_Path = DB_Path + "\\Files.accdb";
            TempPath = txt_TempPath;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void MainDB_Load(object sender, EventArgs e)
        {
            //DataAccess da = new DataAccess();
            //MessageBox.Show("test0");
            SearchCmd = "SELECT * FROM Main ORDER BY Artist, Album_Year, Album, Song_Title;";
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            btn_Search.Enabled = false;
            if (!Directory.Exists(DataGridView1.Rows[0].Cells[22].Value.ToString()))
            {
                var OLD_Path = DataGridView1.Rows[0].Cells[22].Value.ToString().Substring(0, DataGridView1.Rows[0].Cells[22].Value.ToString().IndexOf("\\0\\")); //files[0].Folder_Name.Replace
                if (!Directory.Exists(OLD_Path))
                {
                    var cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath,'" + OLD_Path + "','" + TempPath + "'), AudioPath=REPLACE(AudioPath,'" + OLD_Path + "','" + TempPath + "')";
                    cmd += ", audioPreviewPath=REPLACE(audioPreviewPath,'" + OLD_Path + "','" + TempPath + "'), Folder_Name=REPLACE(Folder_Name,'" + OLD_Path + "','" + TempPath + "');";
                    //txt_Description.Text = cmd;
                   DialogResult result1 = MessageBox.Show("DB Repository has been moved from " + OLD_Path + " to " + TempPath +"-"+cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                        try
                        {
                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                            {
                                DataSet dus = new DataSet();
                                OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                dax.Fill(dus, "Main");
                                dax.Dispose();
                                //MessageBox.Show("Main table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DataSet ds = new DataSet();

                                cmd = "UPDATE Main SET , OggPath=REPLACE(OggPath,'" + OLD_Path + "','" + TempPath + "') WHERE OggPath IS NOT NULL;";
                                OleDbDataAdapter dac = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                dac.Fill(ds, "Arrangements");
                                dac.Dispose();

                                cmd = "UPDATE Main SET oggPreviewPath=REPLACE(oggPreviewPath,'" + OLD_Path + "','" + TempPath + "') WHERE oggPreviewPath IS NOT NULL;";
                                OleDbDataAdapter daH = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                daH.Fill(ds, "Arrangements");
                                daH.Dispose();
                                MessageBox.Show("Main table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                cmd = "UPDATE Arrangements SET SNGFilePath=REPLACE(SNGFilePath,'" + OLD_Path + "','" + TempPath + "'), XMLFilePath=REPLACE(XMLFilePath,'" + OLD_Path + "','" + TempPath + "');";
                                OleDbDataAdapter daN = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                daN.Fill(ds, "Arrangements");
                                daN.Dispose();
                                MessageBox.Show("Arrangements table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Main DB connection to Update DB Reporsitory ! " + DB_Path + "-" + cmd);
                        }
                    else {
                        MessageBox.Show("1Issues with Temp Folder and DB Reporsitory");                    
                        return;
                        }
                }
            }
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
            //DLCManager.txt_DBFolder.Text
            //MessageBox.Show(DB_Path);
            try
            {
                Process process = Process.Start(@DB_Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + DB_Path );
            }
        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            ArrangementsDB frm = new ArrangementsDB(DB_Path);
            frm.Show();
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            TonesDB frm = new TonesDB(DB_Path);
            frm.Show();
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
            txt_Title.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_Title_Sort.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_Album.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
            txt_Artist.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
            txt_Artist_Sort.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            txt_Album_Year.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
            txt_AverageTempo.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
            txt_Volume.Text = DataGridView1.Rows[i].Cells[8].Value.ToString();
            txt_Preview_Volume.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
            txt_AlbumArtPath.Text = DataGridView1.Rows[i].Cells[10].Value.ToString();
            txt_Track_No.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
            txt_Author.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
            txt_Version.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
            txt_DLC_ID.Text = DataGridView1.Rows[i].Cells[16].Value.ToString();
            txt_APP_ID.Text = DataGridView1.Rows[i].Cells[17].Value.ToString();
            txt_Alt_No.Text = DataGridView1.Rows[i].Cells[33].Value.ToString();
            txt_Tuning.Text = DataGridView1.Rows[i].Cells[47].Value.ToString();
            txt_BassPicking.Text = DataGridView1.Rows[i].Cells[48].Value.ToString();
            txt_Group.Text = DataGridView1.Rows[i].Cells[50].Value.ToString();
            txt_Rating.Text = DataGridView1.Rows[i].Cells[51].Value.ToString();
            txt_Description.Text = DataGridView1.Rows[i].Cells[52].Value.ToString();
            txt_Artist_ShortName.Text = DataGridView1.Rows[i].Cells[85].Value.ToString();
            txt_Album_ShortName.Text = DataGridView1.Rows[i].Cells[86].Value.ToString();

            if (DataGridView1.Rows[i].Cells[26].Value.ToString() == "Yes") chbx_Original.Checked = true;
            else chbx_Original.Checked = false;
            if (DataGridView1.Rows[i].Cells[28].Value.ToString() == "Yes") chbx_Beta.Checked = true;
            else chbx_Beta.Checked = false;
            if (DataGridView1.Rows[i].Cells[29].Value.ToString() == "Yes") chbx_Alternate.Checked = true;
            else chbx_Alternate.Checked = false;
            if (DataGridView1.Rows[i].Cells[31].Value.ToString() == "Yes") chbx_Broken.Checked = true;
            else chbx_Broken.Checked = false;
            if (DataGridView1.Rows[i].Cells[35].Value.ToString() == "Yes") chbx_Bass.Checked = true;
            else chbx_Bass.Checked = false;
            if (DataGridView1.Rows[i].Cells[37].Value.ToString() == "Yes") chbx_Lead.Checked = true;
            else chbx_Lead.Checked = false;
            if (DataGridView1.Rows[i].Cells[39].Value.ToString() == "Yes") chbx_Combo.Checked = true;
            else chbx_Combo.Checked = false;
            if (DataGridView1.Rows[i].Cells[38].Value.ToString() == "Yes") chbx_Rhythm.Checked = true;
            else chbx_Rhythm.Checked = false;
            if (DataGridView1.Rows[i].Cells[41].Value.ToString() == "Yes") chbx_Sections.Checked = true;
            else chbx_Sections.Checked = false;
            if (DataGridView1.Rows[i].Cells[42].Value.ToString() == "Yes") chbx_Cover.Checked = true;
            else chbx_Cover.Checked = false;
            if (DataGridView1.Rows[i].Cells[43].Value.ToString() == "Yes") chbx_Preview.Checked = true;
            else chbx_Preview.Checked = false;
            if (DataGridView1.Rows[i].Cells[45].Value.ToString() == "Yes") chbx_DD.Checked = true;
            else chbx_DD.Checked = false;
            if (DataGridView1.Rows[i].Cells[69].Value.ToString() == "Yes") chbx_Selected.Checked = true;
            else chbx_Selected.Checked = false;
            if (DataGridView1.Rows[i].Cells[83].Value.ToString() == "Yes") chbx_BassDD.Checked = true;
            else chbx_BassDD.Checked = false;
            if (DataGridView1.Rows[i].Cells[84].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
            else chbx_Bonus.Checked = false;
            if (DataGridView1.Rows[i].Cells[87].Value.ToString() == "Yes") chbx_Avail_Old.Checked = true;
            else chbx_Avail_Old.Checked = false;
            if (DataGridView1.Rows[i].Cells[88].Value.ToString() == "Yes") chbx_Avail_Duplicate.Checked = true;
            else chbx_Avail_Duplicate.Checked = false;
            if (DataGridView1.Rows[i].Cells[89].Value.ToString() == "Yes") chbx_Has_Been_Corrected.Checked = true;
            else chbx_Has_Been_Corrected.Checked = false;


            //ImageSource imageSource = new BitmapImage(new Uri("C:\\Temp\\music_edit.png"));
            //txt_Description.Text = txt_AlbumArtPath.Text.Replace(".dds", ".png");
            picbx_AlbumArtPath.ImageLocation= txt_AlbumArtPath.Text.Replace(".dds",".png");
            //btn_Search.Enabled = false;
            btn_Save.Enabled = true;
            txt_Artist_Sort.Enabled = true;
            txt_Album.Enabled = true;
            txt_Title_Sort.Enabled = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i;
            DataSet dis = new DataSet();

            i = DataGridView1.SelectedCells[0].RowIndex;

            DataGridView1.Rows[i].Cells[1].Value = txt_Title.Text;
            DataGridView1.Rows[i].Cells[2].Value = txt_Title_Sort.Text;
            DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
            DataGridView1.Rows[i].Cells[4].Value = txt_Artist.Text;
            DataGridView1.Rows[i].Cells[5].Value = txt_Artist_Sort.Text;
            DataGridView1.Rows[i].Cells[6].Value = txt_Album_Year.Text;
            DataGridView1.Rows[i].Cells[7].Value = txt_AverageTempo.Text;
            DataGridView1.Rows[i].Cells[8].Value = txt_Volume.Text;
            DataGridView1.Rows[i].Cells[9].Value = txt_Preview_Volume.Text;
            DataGridView1.Rows[i].Cells[10].Value = txt_AlbumArtPath.Text;
            DataGridView1.Rows[i].Cells[13].Value = txt_Track_No.Text;
            DataGridView1.Rows[i].Cells[14].Value = txt_Author.Text;
            DataGridView1.Rows[i].Cells[15].Value = txt_Version.Text;
            DataGridView1.Rows[i].Cells[16].Value = txt_DLC_ID.Text;
            DataGridView1.Rows[i].Cells[17].Value = txt_APP_ID.Text;
            DataGridView1.Rows[i].Cells[33].Value = txt_Alt_No.Text;
            DataGridView1.Rows[i].Cells[47].Value = txt_Tuning.Text;
            DataGridView1.Rows[i].Cells[48].Value = txt_BassPicking.Text;
            DataGridView1.Rows[i].Cells[50].Value = txt_Group.Text;
            DataGridView1.Rows[i].Cells[51].Value = txt_Rating.Text;
            DataGridView1.Rows[i].Cells[52].Value = txt_Description.Text;
            DataGridView1.Rows[i].Cells[85].Value = txt_Artist_ShortName.Text;
            DataGridView1.Rows[i].Cells[86].Value = txt_Album_ShortName.Text;
            if (chbx_Original.Checked) DataGridView1.Rows[i].Cells[26].Value = "Yes";
            else DataGridView1.Rows[i].Cells[26].Value = "No";
            if (chbx_Beta.Checked) DataGridView1.Rows[i].Cells[28].Value = "Yes";
            else DataGridView1.Rows[i].Cells[28].Value = "No";
            if (chbx_Alternate.Checked) DataGridView1.Rows[i].Cells[29].Value = "Yes";
            else DataGridView1.Rows[i].Cells[29].Value = "No";
            if (chbx_Broken.Checked) DataGridView1.Rows[i].Cells[31].Value = "Yes";
            else DataGridView1.Rows[i].Cells[31].Value = "No";
            if (chbx_Bass.Checked) DataGridView1.Rows[i].Cells[35].Value = "Yes";
            else DataGridView1.Rows[i].Cells[35].Value = "No";
            if (chbx_Lead.Checked) DataGridView1.Rows[i].Cells[37].Value = "Yes";
            else DataGridView1.Rows[i].Cells[37].Value = "No";
            if (chbx_Rhythm.Checked) DataGridView1.Rows[i].Cells[38].Value = "Yes";
            else DataGridView1.Rows[i].Cells[38].Value = "No";
            if (chbx_Combo.Checked) DataGridView1.Rows[i].Cells[39].Value = "Yes";
            else DataGridView1.Rows[i].Cells[39].Value = "No";
            //if (chbx_Sections.Checked) DataGridView1.Rows[i].Cells[41].Value = "Yes";
            //else DataGridView1.Rows[i].Cells[41].Value = "No";
            if (chbx_Cover.Checked) DataGridView1.Rows[i].Cells[42].Value = "Yes";
            else DataGridView1.Rows[i].Cells[42].Value = "No";
            if (chbx_Preview.Checked) DataGridView1.Rows[i].Cells[43].Value = "Yes";
            else DataGridView1.Rows[i].Cells[43].Value = "No";
            if (chbx_DD.Checked) DataGridView1.Rows[i].Cells[45].Value = "Yes";
            else DataGridView1.Rows[i].Cells[45].Value = "No";
            if (chbx_Selected.Checked) DataGridView1.Rows[i].Cells[69].Value = "Yes";
            else DataGridView1.Rows[i].Cells[69].Value = "No";
            if (chbx_BassDD.Checked) DataGridView1.Rows[i].Cells[83].Value = "Yes";
            else DataGridView1.Rows[i].Cells[83].Value = "No";
            if (chbx_Bonus.Checked) DataGridView1.Rows[i].Cells[84].Value = "Yes";
            else DataGridView1.Rows[i].Cells[84].Value = "No";
            if (chbx_Avail_Old.Checked) DataGridView1.Rows[i].Cells[87].Value = "Yes";
            else DataGridView1.Rows[i].Cells[87].Value = "No";
            if (chbx_Avail_Duplicate.Checked) DataGridView1.Rows[i].Cells[88].Value = "Yes";
            else DataGridView1.Rows[i].Cells[88].Value = "No";
            if (chbx_Has_Been_Corrected.Checked) DataGridView1.Rows[i].Cells[89].Value = "Yes";
            else DataGridView1.Rows[i].Cells[89].Value = "No";

            //var DB_Path = "../../../../tmp\\Files.accdb;";
            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                //OleDbCommand command = new OleDbCommand(); ;
                //Update MainDB
                //SqlCommand cmds = new SqlCommand(sqlCmd, conn2);
                command.CommandText = "UPDATE Main SET ";

                command.CommandText += "Song_Title = @param1, ";
                command.CommandText += "Song_Title_Sort = @param2, ";
                command.CommandText += "Album = @param3, ";
                command.CommandText += "Artist = @param4, ";
                command.CommandText += "Artist_Sort = @param5, ";
                command.CommandText += "Album_Year = @param6, ";
                command.CommandText += "AverageTempo = @param7, ";
                command.CommandText += "Volume = @param8, ";
                command.CommandText += "Preview_Volume = @param9, ";
                command.CommandText += "AlbumArtPath = @param10, ";
                command.CommandText += "Track_No = @param13, ";
                command.CommandText += "Author = @param14, ";
                command.CommandText += "Version = @param15, ";
                command.CommandText += "DLC_Name = @param16, ";
                command.CommandText += "DLC_AppID = @param17, ";
                command.CommandText += "Is_Original = @param26, ";
                command.CommandText += "Is_Beta = @param28, ";
                command.CommandText += "Is_Alternate = @param29, ";
                //command.CommandText += "Is_Multitrack = @param30, ";
                command.CommandText += "Is_Broken = @param31, ";
                //command.CommandText += "MultiTrack_Version = @param32, ";
                command.CommandText += "Alternate_Version_No = @param33, ";
                //command.CommandText += "Has_Sections = @param41, ";
                command.CommandText += "Has_Cover = @param42, ";
                command.CommandText += "Has_Preview = @param43, ";
                command.CommandText += "Has_DD = @param45, ";
                command.CommandText += "Tunning = @param47, ";
                command.CommandText += "Bass_Picking = @param48, ";
                command.CommandText += "Tones = @param49, ";
                //command.CommandText += "Group = @param50, ";
                command.CommandText += "Rating = @param51, ";
                command.CommandText += "Description = @param52, ";
                command.CommandText += "Selected = @param69, ";
                //command.CommandText += "YouTube_Link = @param70, ";
                //command.CommandText += "CustomsForge_Link = @param71, ";
                //command.CommandText += "CustomsForge_Like = @param72, ";
                //command.CommandText += "CustomsForge_ReleaseNotes = @param73, ";
                //command.CommandText += "SignatureType = @param74, ";
                //command.CommandText += "ToolkitVersion = @param75, ";
                //command.CommandText += "UniqueDLCName = @param79, ";
                command.CommandText += "Bass_Has_DD = @param83, ";
                command.CommandText += "Has_Bonus_Arrangement = @param84, ";
                command.CommandText += "Artist_ShortName = @param85, ";
                command.CommandText += "Album_ShortName = @param86, ";
                command.CommandText += "Available_Old = @param87, ";
                command.CommandText += "Available_Duplicate = @param88 ";

                command.CommandText += "WHERE ID = " + txt_ID.Text;

                command.Parameters.AddWithValue("@param1", DataGridView1.Rows[i].Cells[1].Value.ToString());
                command.Parameters.AddWithValue("@param2", DataGridView1.Rows[i].Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@param3", DataGridView1.Rows[i].Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@param4", DataGridView1.Rows[i].Cells[4].Value.ToString());
                command.Parameters.AddWithValue("@param5", DataGridView1.Rows[i].Cells[5].Value.ToString());
                command.Parameters.AddWithValue("@param6", DataGridView1.Rows[i].Cells[6].Value.ToString());
                command.Parameters.AddWithValue("@param7", DataGridView1.Rows[i].Cells[7].Value.ToString());
                command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString());
                command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[9].Value.ToString());
                command.Parameters.AddWithValue("@param10", DataGridView1.Rows[i].Cells[10].Value.ToString());
                command.Parameters.AddWithValue("@param13", DataGridView1.Rows[i].Cells[13].Value.ToString());
                command.Parameters.AddWithValue("@param14", DataGridView1.Rows[i].Cells[14].Value.ToString());
                command.Parameters.AddWithValue("@param15", DataGridView1.Rows[i].Cells[15].Value.ToString());
                command.Parameters.AddWithValue("@param16", DataGridView1.Rows[i].Cells[16].Value.ToString());
                command.Parameters.AddWithValue("@param17", DataGridView1.Rows[i].Cells[17].Value.ToString());
                command.Parameters.AddWithValue("@param26", DataGridView1.Rows[i].Cells[26].Value.ToString());
                command.Parameters.AddWithValue("@param28", DataGridView1.Rows[i].Cells[28].Value.ToString());
                command.Parameters.AddWithValue("@param29", DataGridView1.Rows[i].Cells[29].Value.ToString());
                command.Parameters.AddWithValue("@param31", DataGridView1.Rows[i].Cells[31].Value.ToString());
                command.Parameters.AddWithValue("@param33", DataGridView1.Rows[i].Cells[33].Value.ToString());
                //command.Parameters.AddWithValue("@param41", DataGridView1.Rows[i].Cells[41].Value.ToString());
                command.Parameters.AddWithValue("@param42", DataGridView1.Rows[i].Cells[42].Value.ToString());
                command.Parameters.AddWithValue("@param43", DataGridView1.Rows[i].Cells[43].Value.ToString());
                command.Parameters.AddWithValue("@param45", DataGridView1.Rows[i].Cells[45].Value.ToString());
                command.Parameters.AddWithValue("@param47", DataGridView1.Rows[i].Cells[47].Value.ToString());
                command.Parameters.AddWithValue("@param48", DataGridView1.Rows[i].Cells[48].Value.ToString());
                command.Parameters.AddWithValue("@param49", DataGridView1.Rows[i].Cells[49].Value.ToString());
                //command.Parameters.AddWithValue("@param50", DataGridView1.Rows[i].Cells[50].Value.ToString());
                command.Parameters.AddWithValue("@param51", DataGridView1.Rows[i].Cells[51].Value.ToString());
                command.Parameters.AddWithValue("@param52", DataGridView1.Rows[i].Cells[52].Value.ToString());
                command.Parameters.AddWithValue("@param69", DataGridView1.Rows[i].Cells[69].Value.ToString());
                command.Parameters.AddWithValue("@param83", DataGridView1.Rows[i].Cells[83].Value.ToString());
                command.Parameters.AddWithValue("@param84", DataGridView1.Rows[i].Cells[84].Value.ToString());
                command.Parameters.AddWithValue("@param85", DataGridView1.Rows[i].Cells[85].Value.ToString());
                command.Parameters.AddWithValue("@param86", DataGridView1.Rows[i].Cells[86].Value.ToString());
                command.Parameters.AddWithValue("@param87", DataGridView1.Rows[i].Cells[87].Value.ToString());
                command.Parameters.AddWithValue("@param88", DataGridView1.Rows[i].Cells[88].Value.ToString());
                
                //MessageBox.Show(command.Parameters.);
                try
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + DB_Path + "-" + command.CommandText);
                    throw;
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
                ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                MessageBox.Show("Song Details Changes Saved");
                //das.SelectCommand.CommandText = "SELECT * FROM Main";
                //// das.Update(dssx, "Main");
            }


            //var DB_Path = "../../../../tmp\\Files.accdb;";
            ////DataAccess da = new DataAccess();
            //cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //dax = new OleDbDataAdapter("Select * from Main", cn);
            ////using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB_Path))
            ////{
            //dax.Update(da, "Main");
            //    da.AcceptChanges();
            //    //fillgrid();
            ////}
        }

    private void btn_Search_Click(object sender, EventArgs e)
    {
            try
            { 
            SearchCmd = "SELECT * FROM Main WHERE "+(txt_Artist.Text != "" ? " Artist Like '%"+ txt_Artist.Text+ "%'":"")+ (txt_Artist.Text != "" ? (txt_Title.Text !=""? " AND " :"") :"") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
            //DataGridView1.Dispose();
            dssx.Dispose();
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can run Search ! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        
        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            noOfRec = 0;
            //dssx.Dispose();
            lbl_NoRec.Text = " records.";
            bs.DataSource = null;
            dssx.Dispose();
            //MessageBox.Show("zero " + noOfRec.ToString() + SearchCmd);
            //DB_Path = "../../../../tmp\\Files.accdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cn);
                //MessageBox.Show("pop" + noOfRec.ToString() + SearchCmd);
                dssx.Clear();
                da.Fill(dssx, "Main");
                da.Dispose();
                cn.Dispose();
                noOfRec = dssx.Tables[0].Rows.Count;
                lbl_NoRec.Text = noOfRec.ToString() + " records.";
                //MessageBox.Show("pop" + noOfRec.ToString() + SearchCmd);
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
            }
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", HeaderText = "ID " };
            DataGridViewTextBoxColumn Song_Title = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title", HeaderText = "Song_Title " };
            DataGridViewTextBoxColumn Song_Title_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title_Sort", HeaderText = "Song_Title_Sort " };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", HeaderText = "Album " };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", HeaderText = "Artist " };
            DataGridViewTextBoxColumn Artist_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Sort", HeaderText = "Artist_Sort " };
            DataGridViewTextBoxColumn Album_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Year", HeaderText = "Album_Year " };
            DataGridViewTextBoxColumn AverageTempo = new DataGridViewTextBoxColumn { DataPropertyName = "AverageTempo", HeaderText = "AverageTempo " };
            DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
            DataGridViewTextBoxColumn Preview_Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Preview_Volume", HeaderText = "Preview_Volume " };
            DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath " };
            DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath " };
            DataGridViewTextBoxColumn audioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreviewPath", HeaderText = "audioPreviewPath " };
            DataGridViewTextBoxColumn Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Track_No", HeaderText = "Track_No " };
            DataGridViewTextBoxColumn Author = new DataGridViewTextBoxColumn { DataPropertyName = "Author", HeaderText = "Author " };
            DataGridViewTextBoxColumn Version = new DataGridViewTextBoxColumn { DataPropertyName = "Version", HeaderText = "Version " };
            DataGridViewTextBoxColumn DLC_Name = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_Name", HeaderText = "DLC_Name " };
            DataGridViewTextBoxColumn DLC_AppID = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_AppID", HeaderText = "DLC_AppID " };
            DataGridViewTextBoxColumn Current_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Current_FileName", HeaderText = "Current_FileName " };
            DataGridViewTextBoxColumn Original_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Original_FileName", HeaderText = "Original_FileName " };
            DataGridViewTextBoxColumn Import_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Path", HeaderText = "Import_Path " };
            DataGridViewTextBoxColumn Import_Date = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Date", HeaderText = "Import_Date " };
            DataGridViewTextBoxColumn Folder_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Folder_Name", HeaderText = "Folder_Name " };
            DataGridViewTextBoxColumn File_Size = new DataGridViewTextBoxColumn { DataPropertyName = "File_Size", HeaderText = "File_Size " };
            DataGridViewTextBoxColumn File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "File_Hash", HeaderText = "File_Hash " };
            DataGridViewTextBoxColumn Original_File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Original_File_Hash", HeaderText = "Original_File_Hash " };
            DataGridViewTextBoxColumn Is_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Original", HeaderText = "Is_Original " };
            DataGridViewTextBoxColumn Is_OLD = new DataGridViewTextBoxColumn { DataPropertyName = "Is_OLD", HeaderText = "Is_OLD " };
            DataGridViewTextBoxColumn Is_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Beta", HeaderText = "Is_Beta " };
            DataGridViewTextBoxColumn Is_Alternate = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Alternate", HeaderText = "Is_Alternate " };
            DataGridViewTextBoxColumn Is_Multitrack = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Multitrack", HeaderText = "Is_Multitrack " };
            DataGridViewTextBoxColumn Is_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Broken", HeaderText = "Is_Broken " };
            DataGridViewTextBoxColumn MultiTrack_Version = new DataGridViewTextBoxColumn { DataPropertyName = "MultiTrack_Version", HeaderText = "MultiTrack_Version " };
            DataGridViewTextBoxColumn Alternate_Version_No = new DataGridViewTextBoxColumn { DataPropertyName = "Alternate_Version_No", HeaderText = "Alternate_Version_No " };
            DataGridViewTextBoxColumn DLC = new DataGridViewTextBoxColumn { DataPropertyName = "DLC", HeaderText = "DLC " };
            DataGridViewTextBoxColumn Has_Bass = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bass", HeaderText = "Has_Bass " };
            DataGridViewTextBoxColumn Has_Guitar = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Guitar", HeaderText = "Has_Guitar " };
            DataGridViewTextBoxColumn Has_Lead = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Lead", HeaderText = "Has_Lead " };
            DataGridViewTextBoxColumn Has_Rhythm = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Rhythm", HeaderText = "Has_Rhythm " };
            DataGridViewTextBoxColumn Has_Combo = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Combo", HeaderText = "Has_Combo " };
            DataGridViewTextBoxColumn Has_Vocals = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Vocals", HeaderText = "Has_Vocals " };
            DataGridViewTextBoxColumn Has_Sections = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Sections", HeaderText = "Has_Sections " };
            DataGridViewTextBoxColumn Has_Cover = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Cover", HeaderText = "Has_Cover " };
            DataGridViewTextBoxColumn Has_Preview = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Preview", HeaderText = "Has_Preview " };
            DataGridViewTextBoxColumn Has_Custom_Tone = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Custom_Tone", HeaderText = "Has_Custom_Tone " };
            DataGridViewTextBoxColumn Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Has_DD", HeaderText = "Has_DD " };
            DataGridViewTextBoxColumn Has_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Version", HeaderText = "Has_Version " };
            DataGridViewTextBoxColumn Tunning = new DataGridViewTextBoxColumn { DataPropertyName = "Tunning", HeaderText = "Tunning " };
            DataGridViewTextBoxColumn Bass_Picking = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Picking", HeaderText = "Bass_Picking " };
            DataGridViewTextBoxColumn Tones = new DataGridViewTextBoxColumn { DataPropertyName = "Tones", HeaderText = "Tones " };
            DataGridViewTextBoxColumn Group = new DataGridViewTextBoxColumn { DataPropertyName = "Group", HeaderText = "Group " };
            DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating " };
            DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
            DataGridViewTextBoxColumn Show_Album = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Album", HeaderText = "Show_Album " };
            DataGridViewTextBoxColumn Show_Track = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Track", HeaderText = "Show_Track " };
            DataGridViewTextBoxColumn Show_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Year", HeaderText = "Show_Year " };
            DataGridViewTextBoxColumn Show_CDLC = new DataGridViewTextBoxColumn { DataPropertyName = "Show_CDLC", HeaderText = "Show_CDLC " };
            DataGridViewTextBoxColumn Show_Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Rating", HeaderText = "Show_Rating " };
            DataGridViewTextBoxColumn Show_Description = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Description", HeaderText = "Show_Description " };
            DataGridViewTextBoxColumn Show_Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Comments", HeaderText = "Show_Comments " };
            DataGridViewTextBoxColumn Show_Available_Instruments = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Available_Instruments", HeaderText = "Show_Available_Instruments " };
            DataGridViewTextBoxColumn Show_Alternate_Version = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Alternate_Version", HeaderText = "Show_Alternate_Version " };
            DataGridViewTextBoxColumn Show_MultiTrack_Details = new DataGridViewTextBoxColumn { DataPropertyName = "Show_MultiTrack_Details", HeaderText = "Show_MultiTrack_Details " };
            DataGridViewTextBoxColumn Show_Group = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Group", HeaderText = "Show_Group " };
            DataGridViewTextBoxColumn Show_Beta = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Beta", HeaderText = "Show_Beta " };
            DataGridViewTextBoxColumn Show_Broken = new DataGridViewTextBoxColumn { DataPropertyName = "Show_Broken", HeaderText = "Show_Broken " };
            DataGridViewTextBoxColumn Show_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Show_DD", HeaderText = "Show_DD " };
            DataGridViewTextBoxColumn Original = new DataGridViewTextBoxColumn { DataPropertyName = "Original", HeaderText = "Original " };
            DataGridViewTextBoxColumn Selected = new DataGridViewTextBoxColumn { DataPropertyName = "Selected", HeaderText = "Selected " };
            DataGridViewTextBoxColumn YouTube_Link = new DataGridViewTextBoxColumn { DataPropertyName = "YouTube_Link", HeaderText = "YouTube_Link " };
            DataGridViewTextBoxColumn CustomsForge_Link = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Link", HeaderText = "CustomsForge_Link " };
            DataGridViewTextBoxColumn CustomsForge_Like = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_Like", HeaderText = "CustomsForge_Like " };
            DataGridViewTextBoxColumn CustomsForge_ReleaseNotes = new DataGridViewTextBoxColumn { DataPropertyName = "CustomsForge_ReleaseNotes", HeaderText = "CustomsForge_ReleaseNotes " };
            DataGridViewTextBoxColumn SignatureType = new DataGridViewTextBoxColumn { DataPropertyName = "SignatureType", HeaderText = "SignatureType " };
            DataGridViewTextBoxColumn ToolkitVersion = new DataGridViewTextBoxColumn { DataPropertyName = "ToolkitVersion", HeaderText = "ToolkitVersion " };
            DataGridViewTextBoxColumn Has_Author = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Author", HeaderText = "Has_Author " };
            DataGridViewTextBoxColumn OggPath = new DataGridViewTextBoxColumn { DataPropertyName = "OggPath", HeaderText = "OggPath " };
            DataGridViewTextBoxColumn oggPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "oggPreviewPath", HeaderText = "oggPreviewPath " };
            DataGridViewTextBoxColumn UniqueDLCName = new DataGridViewTextBoxColumn { DataPropertyName = "UniqueDLCName", HeaderText = "UniqueDLCName " };
            DataGridViewTextBoxColumn AlbumArt_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArt_Hash", HeaderText = "AlbumArt_Hash " };
            DataGridViewTextBoxColumn Audio_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Audio_Hash", HeaderText = "Audio_Hash " };
            DataGridViewTextBoxColumn audioPreview_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreview_Hash", HeaderText = "audioPreview_Hash " };
            DataGridViewTextBoxColumn Bass_Has_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Bass_Has_DD", HeaderText = "Bass_Has_DD " };
            DataGridViewTextBoxColumn Has_Bonus_Arrangement = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Bonus_Arrangement", HeaderText = "Has_Bonus_Arrangement " };
            DataGridViewTextBoxColumn Artist_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_ShortName", HeaderText = "Artist_ShortName " };
            DataGridViewTextBoxColumn Album_ShortName = new DataGridViewTextBoxColumn { DataPropertyName = "Album_ShortName", HeaderText = "Album_ShortName " };
            DataGridViewTextBoxColumn Available_Old = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Old", HeaderText = "Available_Old " };
            DataGridViewTextBoxColumn Available_Duplicate = new DataGridViewTextBoxColumn { DataPropertyName = "Available_Duplicate", HeaderText = "Available_Duplicate " };
            DataGridViewTextBoxColumn Has_Been_Corrected = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Been_Corrected", HeaderText = "Has_Been_Corrected " };

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
                Song_Title,
                Song_Title_Sort,
                Album,
                Artist,
                Artist_Sort,
                Album_Year,
                AverageTempo,
                Volume,
                Preview_Volume,
                AlbumArtPath,
                AudioPath,
                audioPreviewPath,
                Track_No,
                Author,
                Version,
                DLC_Name,
                DLC_AppID,
                Current_FileName,
                Original_FileName,
                Import_Path,
                Import_Date,
                Folder_Name,
                File_Size,
                File_Hash,
                Original_File_Hash,
                Is_Original,
                Is_OLD,
                Is_Beta,
                Is_Alternate,
                Is_Multitrack,
                Is_Broken,
                MultiTrack_Version,
                Alternate_Version_No,
                DLC,
                Has_Bass,
                Has_Guitar,
                Has_Lead,
                Has_Rhythm,
                Has_Combo,
                Has_Vocals,
                Has_Sections,
                Has_Cover,
                Has_Preview,
                Has_Custom_Tone,
                Has_DD,
                Has_Version,
                Tunning,
                Bass_Picking,
                Tones,
                Group,
                Rating,
                Description,
                Comments,
                Show_Album,
                Show_Track,
                Show_Year,
                Show_CDLC,
                Show_Rating,
                Show_Description,
                Show_Comments,
                Show_Available_Instruments,
                Show_Alternate_Version,
                Show_MultiTrack_Details,
                Show_Group,
                Show_Beta,
                Show_Broken,
                Show_DD,
                Original,
                Selected,
                YouTube_Link,
                CustomsForge_Link,
                CustomsForge_Like,
                CustomsForge_ReleaseNotes,
                SignatureType,
                ToolkitVersion,
                Has_Author,
                OggPath,
                oggPreviewPath,
                UniqueDLCName,
                AlbumArt_Hash,
                Audio_Hash,
                audioPreview_Hash,
                Bass_Has_DD,
                Has_Bonus_Arrangement,
                Artist_ShortName,
                Album_ShortName,
                Available_Duplicate,
                Available_Old,
                Has_Been_Corrected
            }
            );
            bs.ResetBindings(false);
            dssx.Tables["Main"].AcceptChanges();
            bs.DataSource = dssx.Tables["Main"];
            DataGridView.DataSource = null;
            DataGridView.DataSource = bs;
            DataGridView.Refresh();
            //bs.Dispose();
            dssx.Dispose();
            //MessageBox.Show("-");
            //DataGridView.ExpandColumns();


        }

        public class Files
        {
            public string ID { get; set; }
            public string Song_Title { get; set; }
            public string Song_Title_Sort { get; set; }
            public string Album { get; set; }
            public string Artist { get; set; }
            public string Artist_Sort { get; set; }
            public string Album_Year { get; set; }
            public string AverageTempo { get; set; }
            public string Volume { get; set; }
            public string Preview_Volume { get; set; }
            public string AlbumArtPath { get; set; }
            public string AudioPath { get; set; }
            public string audioPreviewPath { get; set; }
            public string Track_No { get; set; }
            public string Author { get; set; }
            public string Version { get; set; }
            public string DLC_Name { get; set; }
            public string DLC_AppID { get; set; }
            public string Current_FileName { get; set; }
            public string Original_FileName { get; set; }
            public string Import_Path { get; set; }
            public string Import_Date { get; set; }
            public string Folder_Name { get; set; }
            public string File_Size { get; set; }
            public string File_Hash { get; set; }
            public string Original_File_Hash { get; set; }
            public string Is_Original { get; set; }
            public string Is_OLD { get; set; }
            public string Is_Beta { get; set; }
            public string Is_Alternate { get; set; }
            public string Is_Multitrack { get; set; }
            public string Is_Broken { get; set; }
            public string MultiTrack_Version { get; set; }
            public string Alternate_Version_No { get; set; }
            public string DLC { get; set; }
            public string Has_Bass { get; set; }
            public string Has_Guitar { get; set; }
            public string Has_Lead { get; set; }
            public string Has_Rhythm { get; set; }
            public string Has_Combo { get; set; }
            public string Has_Vocals { get; set; }
            public string Has_Sections { get; set; }
            public string Has_Cover { get; set; }
            public string Has_Preview { get; set; }
            public string Has_Custom_Tone { get; set; }
            public string Has_DD { get; set; }
            public string Has_Version { get; set; }
            public string Tunning { get; set; }
            public string Bass_Picking { get; set; }
            public string Tones { get; set; }
            public string Group { get; set; }
            public string Rating { get; set; }
            public string Description { get; set; }
            public string Comments { get; set; }
            public string Show_Album { get; set; }
            public string Show_Track { get; set; }
            public string Show_Year { get; set; }
            public string Show_CDLC { get; set; }
            public string Show_Rating { get; set; }
            public string Show_Description { get; set; }
            public string Show_Comments { get; set; }
            public string Show_Available_Instruments { get; set; }
            public string Show_Alternate_Version { get; set; }
            public string Show_MultiTrack_Details { get; set; }
            public string Show_Group { get; set; }
            public string Show_Beta { get; set; }
            public string Show_Broken { get; set; }
            public string Show_DD { get; set; }
            public string Original { get; set; }
            public string Selected { get; set; }
            public string YouTube_Link { get; set; }
            public string CustomsForge_Link { get; set; }
            public string CustomsForge_Like { get; set; }
            public string CustomsForge_ReleaseNotes { get; set; }
            public string SignatureType { get; set; }
            public string ToolkitVersion { get; set; }
            public string Has_Author { get; set; }
            public string OggPath { get; set; }
            public string oggPreviewPath { get; set; }
            public string UniqueDLCName { get; set; }
            public string AlbumArt_Hash { get; set; }
            public string Audio_Hash { get; set; }
            public string audioPreview_Hash { get; set; }
            public string Bass_Has_DD { get; set; }
            public string Has_Bonus_Arrangement { get; set; }
            public string Artist_ShortName { get; set; }
            public string Album_ShortName { get; set; }
            public string Available_Old { get; set; }
            public string Available_Duplicate { get; set; }
            public string Has_Been_Corrected { get; set; }
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
                //MessageBox.Show(DB_Path);
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                    dax.Fill(dus, "Main");

                    var i = 0;
                    //rtxt_StatisticsOnReadDLCs.Text += "\n  54= " +dus.Tables[0].Rows.Count;
                    MaximumSize = dus.Tables[0].Rows.Count;
                    foreach (DataRow dataRow in dus.Tables[0].Rows)
                    {
                        files[i] = new Files();

                        //rtxt_StatisticsOnReadDLCs.Text += "\n  a= " + i + MaximumSize+dataRow.ItemArray[0].ToString();
                        files[i].ID = dataRow.ItemArray[0].ToString();
                        files[i].Song_Title = dataRow.ItemArray[1].ToString();
                        files[i].Song_Title_Sort = dataRow.ItemArray[2].ToString();
                        files[i].Album = dataRow.ItemArray[3].ToString();
                        files[i].Artist = dataRow.ItemArray[4].ToString();
                        files[i].Artist_Sort = dataRow.ItemArray[5].ToString();
                        files[i].Album_Year = dataRow.ItemArray[6].ToString();
                        files[i].AverageTempo = dataRow.ItemArray[7].ToString();
                        files[i].Volume = dataRow.ItemArray[8].ToString();
                        files[i].Preview_Volume = dataRow.ItemArray[9].ToString();
                        files[i].AlbumArtPath = dataRow.ItemArray[10].ToString();
                        files[i].AudioPath = dataRow.ItemArray[11].ToString();
                        files[i].audioPreviewPath = dataRow.ItemArray[12].ToString();
                        files[i].Track_No = dataRow.ItemArray[13].ToString();
                        files[i].Author = dataRow.ItemArray[14].ToString();
                        files[i].Version = dataRow.ItemArray[15].ToString();
                        files[i].DLC_Name = dataRow.ItemArray[16].ToString();
                        files[i].DLC_AppID = dataRow.ItemArray[17].ToString();
                        files[i].Current_FileName = dataRow.ItemArray[18].ToString();
                        files[i].Original_FileName = dataRow.ItemArray[19].ToString();
                        files[i].Import_Path = dataRow.ItemArray[20].ToString();
                        files[i].Import_Date = dataRow.ItemArray[21].ToString();
                        files[i].Folder_Name = dataRow.ItemArray[22].ToString();
                        files[i].File_Size = dataRow.ItemArray[23].ToString();
                        files[i].File_Hash = dataRow.ItemArray[24].ToString();
                        files[i].Original_File_Hash = dataRow.ItemArray[25].ToString();
                        files[i].Is_Original = dataRow.ItemArray[26].ToString();
                        files[i].Is_OLD = dataRow.ItemArray[27].ToString();
                        files[i].Is_Beta = dataRow.ItemArray[28].ToString();
                        files[i].Is_Alternate = dataRow.ItemArray[29].ToString();
                        files[i].Is_Multitrack = dataRow.ItemArray[30].ToString();
                        files[i].Is_Broken = dataRow.ItemArray[31].ToString();
                        files[i].MultiTrack_Version = dataRow.ItemArray[32].ToString();
                        files[i].Alternate_Version_No = dataRow.ItemArray[33].ToString();
                        files[i].DLC = dataRow.ItemArray[34].ToString();
                        files[i].Has_Bass = dataRow.ItemArray[35].ToString();
                        files[i].Has_Guitar = dataRow.ItemArray[36].ToString();
                        files[i].Has_Lead = dataRow.ItemArray[37].ToString();
                        files[i].Has_Rhythm = dataRow.ItemArray[38].ToString();
                        files[i].Has_Combo = dataRow.ItemArray[39].ToString();
                        files[i].Has_Vocals = dataRow.ItemArray[40].ToString();
                        files[i].Has_Sections = dataRow.ItemArray[41].ToString();
                        files[i].Has_Cover = dataRow.ItemArray[42].ToString();
                        files[i].Has_Preview = dataRow.ItemArray[43].ToString();
                        files[i].Has_Custom_Tone = dataRow.ItemArray[44].ToString();
                        files[i].Has_DD = dataRow.ItemArray[45].ToString();
                        files[i].Has_Version = dataRow.ItemArray[46].ToString();
                        files[i].Tunning = dataRow.ItemArray[47].ToString();
                        files[i].Bass_Picking = dataRow.ItemArray[48].ToString();
                        files[i].Tones = dataRow.ItemArray[49].ToString();
                        files[i].Group = dataRow.ItemArray[50].ToString();
                        files[i].Rating = dataRow.ItemArray[51].ToString();
                        files[i].Description = dataRow.ItemArray[52].ToString();
                        files[i].Comments = dataRow.ItemArray[53].ToString();
                        files[i].Show_Album = dataRow.ItemArray[54].ToString();
                        files[i].Show_Track = dataRow.ItemArray[55].ToString();
                        files[i].Show_Year = dataRow.ItemArray[56].ToString();
                        files[i].Show_CDLC = dataRow.ItemArray[57].ToString();
                        files[i].Show_Rating = dataRow.ItemArray[58].ToString();
                        files[i].Show_Description = dataRow.ItemArray[59].ToString();
                        files[i].Show_Comments = dataRow.ItemArray[60].ToString();
                        files[i].Show_Available_Instruments = dataRow.ItemArray[61].ToString();
                        files[i].Show_Alternate_Version = dataRow.ItemArray[62].ToString();
                        files[i].Show_MultiTrack_Details = dataRow.ItemArray[63].ToString();
                        files[i].Show_Group = dataRow.ItemArray[64].ToString();
                        files[i].Show_Beta = dataRow.ItemArray[65].ToString();
                        files[i].Show_Broken = dataRow.ItemArray[66].ToString();
                        files[i].Show_DD = dataRow.ItemArray[67].ToString();
                        files[i].Original = dataRow.ItemArray[68].ToString();
                        files[i].Selected = dataRow.ItemArray[69].ToString();
                        files[i].YouTube_Link = dataRow.ItemArray[70].ToString();
                        files[i].CustomsForge_Link = dataRow.ItemArray[71].ToString();
                        files[i].CustomsForge_Like = dataRow.ItemArray[72].ToString();
                        files[i].CustomsForge_ReleaseNotes = dataRow.ItemArray[73].ToString();
                        files[i].SignatureType = dataRow.ItemArray[74].ToString();
                        files[i].ToolkitVersion = dataRow.ItemArray[75].ToString();
                        files[i].Has_Author = dataRow.ItemArray[76].ToString();
                        files[i].OggPath = dataRow.ItemArray[77].ToString();
                        files[i].oggPreviewPath = dataRow.ItemArray[78].ToString();
                        files[i].UniqueDLCName = dataRow.ItemArray[79].ToString();
                        files[i].AlbumArt_Hash = dataRow.ItemArray[80].ToString();
                        files[i].Audio_Hash = dataRow.ItemArray[81].ToString();
                        files[i].audioPreview_Hash = dataRow.ItemArray[82].ToString();
                        files[i].Bass_Has_DD = dataRow.ItemArray[83].ToString();
                        files[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
                        files[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
                        files[i].Album_ShortName = dataRow.ItemArray[86].ToString();
                        files[i].Available_Old = dataRow.ItemArray[87].ToString();
                        files[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
                        files[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
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
                MessageBox.Show(ee.Message + "Can not open Main DB connection ! ");
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //rtxt_StatisticsOnReadDLCs.Text += "\n  max rows" + MaximumSize;
            return MaximumSize;//files[10000];
        }

        private void btn_ChangeCover_Click(object sender, EventArgs e)
        {
            //ExternalApps.Png2Dds(IconFile, Path.Combine(tmpWorkDir, "icon.dds"), 512, 512);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(DB_Path, TempPath);
            frm.Show();
        }

        private void cbx_Format_SelectedValueChanged(object sender, EventArgs e)
        {
            btn_Conv_And_Transfer.Text = cbx_Format.SelectedValue.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            btn_ChangeCover.Enabled = false;
            btn_Save.Enabled = false;
            btn_Search.Enabled = true;
            txt_Title.Text = "";
            txt_Artist.Text = "";
            txt_Artist_Sort.Enabled = false;
            txt_Album.Enabled = false;
            txt_Title_Sort.Enabled = false;
            if (btn_Search.Enabled)
            {
                ////SearchCmd = "SELECT * FROM Main WHERE " + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                SearchCmd = "SELECT * FROM Main ORDER BY Artist, Album_Year, Album, Song_Title;";
                dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
                //btn_Search.Enabled = false;
            }
            }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}