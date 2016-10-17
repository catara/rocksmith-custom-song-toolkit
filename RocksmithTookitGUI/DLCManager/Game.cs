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
using RocksmithToolkitLib;//config
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToTabLib;//for psarc browser
using System.Security.Cryptography; //For File hash

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class Game : Form
    {
        public Game(string txt_DBFolder, string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
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
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string SearchCmd = "";
        public bool GroupChanged = false;
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;

        // public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        //public string SearchCmd = "";
        public string SearchFields = "";
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void Standardization_Load(object sender, EventArgs e)
        {
            SearchFields = "ID, Artist, Song_Title, Track_No, Album, Album_Year, Author, Version, Import_Date, Is_Original, Selected, Tunning, Bass_Picking, Is_Beta, Platform, Has_DD, Bass_Has_DD, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, Groups, Rating, Description, PreviewTime, PreviewLenght, Song_Lenght, Comments, FilesMissingIssues, DLC_AppID, DLC_Name, Artist_Sort, Song_Title_Sort, AverageTempo, Volume, Preview_Volume, SignatureType, ToolkitVersion, AlbumArtPath, AudioPath, audioPreviewPath, OggPath, oggPreviewPath, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, File_Size, Current_FileName, Original_FileName, Import_Path, Folder_Name, File_Hash, Original_File_Hash, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Bonus_Arrangement, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_Version, Has_Author, Has_Track_No, File_Creation_Date, Has_Been_Corrected, Pack, Available_Old, Available_Duplicate, Tones, Keep_BassDD, Keep_DD, Keep_Original, Original, YouTube_Link, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, UniqueDLCName, Artist_ShortName, Album_ShortName, DLC, Is_OLD";
            SearchCmd = "SELECT " + SearchFields + " FROM Main u ORDER BY Artist, Album_Year, Album, Track_No, Song_Title;";
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;




            chbx_Autosave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            txt_FTPPath.Text = chbx_PreSavedFTP.Text == "EU" ? ConfigRepository.Instance()["dlcm_FTP1"] : ConfigRepository.Instance()["dlcm_FTP2"];
            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes")
            {
                txt_AudioPath.Visible = true;
                txt_AudioPreviewPath.Visible = true;
                txt_AlbumArtPath.Visible = true;
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

        public void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = DataGridView1.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {
            int i;
            if (DataGridView1.SelectedCells.Count > 0)
            {
                i = DataGridView1.SelectedCells[0].RowIndex;
                txt_ID.Text = DataGridView1.Rows[i].Cells[0].Value.ToString();
                //txt_Identifier.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
                txt_Artist.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
                txt_ArtistSort.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
                txt_Album.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
                txt_Title.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
                txt_AlbumYear.Text = DataGridView1.Rows[i].Cells[6].Value.ToString();
                txt_Arrangements.Text = DataGridView1.Rows[i].Cells[7].Value.ToString();
                // if (DataGridView1.Rows[i].Cells[8].Value.ToString() == "Yes") chbx_Removed.Checked = true;
                //else if (DataGridView1.Rows[i].Cells[8].Value.ToString() == "No") chbx_Removed.Checked = false;
                txt_AlbumArtPath.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();
                rtxt_Comments.Text = DataGridView1.Rows[i].Cells[10].Value.ToString();
                txt_PSARCName.Text = DataGridView1.Rows[i].Cells[11].Value.ToString();
                // txt_SongsHSANPath.Text = DataGridView1.Rows[i].Cells[12].Value.ToString();
                txt_Platform.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
                if (DataGridView1.Rows[i].Cells[14].Value != null) txt_AudioPath.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
                if (DataGridView1.Rows[i].Cells[15].Value != null) txt_AudioPreviewPath.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
                if (DataGridView1.Rows[i].Cells[16].Value.ToString() == "Yes") chbx_Selected.Checked = true;
                else if (DataGridView1.Rows[i].Cells[16].Value.ToString() == "No") chbx_Selected.Checked = false;

                //Create Groups list Dropbox
                DataSet ds = new DataSet();
                var norec = 0;
                try
                {
                    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        string SearchCmd = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\";";
                        OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
                        da.Fill(ds, "Main");
                        norec = ds.Tables[0].Rows.Count;

                        if (norec > 0)
                        {
                            //remove items
                            if (chbx_Group.Items.Count > 0)
                            {
                                chbx_Group.DataSource = null;
                                for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                                {
                                    chbx_Group.Items.RemoveAt(k);
                                    chbx_AllGroups.Items.RemoveAt(k);
                                }
                            }
                            //add items
                            for (int j = 0; j < norec; j++)
                            {
                                chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                                chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                            }
                        }

                        DataSet dds = new DataSet();
                        //Create Groups list MultiCheckbox
                        using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            string SearchCmds = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";";
                            OleDbDataAdapter dfa = new OleDbDataAdapter(SearchCmds, con); //WHERE id=253
                            dfa.Fill(dds, "Main");
                            var nocrec = dds.Tables[0].Rows.Count;

                            if (nocrec > 0)
                                for (int l = 0; l < norec; l++)
                                    for (int j = 0; j < nocrec; j++)
                                    // if (ds.Tables[0].Rows[j][0].ToString() == ds.Tables[0].Rows[l][0].ToString())
                                    //  chbx_AllGroups.SetSelected(j, true);
                                    {
                                        int index = chbx_AllGroups.Items.IndexOf(ds.Tables[0].Rows[j][0].ToString());
                                        chbx_AllGroups.SetItemChecked(index, true);
                                    }
                            //(ds.Tables[0].Rows[l][0].ToString()); 

                        }
                    }

                    //if (txt_Arrangements.Text != "") 
                    picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text;//.Replace(".dds", ".png");
                    if (chbx_Autosave.Checked) SaveOK = true;
                    else SaveOK = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the Retail DB", false, false);
                    frm1.ShowDialog();
                    return;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        public void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();
            if (DataGridView1.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = DataGridView1.SelectedCells[0].RowIndex;
                //nud_RemoveSlide.Value = i;
                //DataGridView1.Rows[i].Cells[0].Value = txt_I.Text;
                //DataGridView1.Rows[i].Cells[1].Value = txt_Artist.Text;
                //DataGridView1.Rows[i].Cells[3].Value = txt_ArtistSort.Text;
                //DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
                if (chbx_Selected.Checked == true) DataGridView1.Rows[i].Cells[16].Value = "Yes";
                else DataGridView1.Rows[i].Cells[16].Value = "No";
                DataGridView1.Rows[i].Cells[10].Value = rtxt_Comments.Text;

                //Save Groups
                if (GroupChanged)
                {
                    var cmdDel = "DELETE FROM Groups WHERE ";

                    DataSet dsz = new DataSet();
                    DataSet ddz = new DataSet();
                    for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                    {
                        using (OleDbConnection cmb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            DataSet dooz = new DataSet();
                            string updatecmd = "SELECT ID FROM Groups WHERE Type=\"Retail\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\";";
                            OleDbDataAdapter dbf = new OleDbDataAdapter(updatecmd, cmb);
                            dbf.Fill(dooz, "Groups");
                            dbf.Dispose();
                            var cmd = "INSERT INTO Groups(CDLC_ID,Groups,Type) VALUES";

                            var rr = dooz.Tables[0].Rows.Count;
                            if (chbx_AllGroups.GetItemChecked(j) && rr == 0)
                            {
                                cmd += "(\"" + txt_ID.Text + "\",\"" + chbx_AllGroups.Items[j] + "\",\"Retail\")";
                                OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cmb);
                                dab.Fill(dsz, "Groups");
                                dab.Dispose();
                            }
                            else if (rr > 0 && !chbx_AllGroups.GetItemChecked(j)) cmdDel += "(Type=\"Retail\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\") OR ";
                        }
                    }
                    cmdDel += ";";
                    cmdDel = cmdDel.Replace(" OR ;", ";");
                    //cmd += ";";
                    //cmd = cmd.Replace(",;", ";");
                    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        //if (cmd != "INSERT INTO Groups(CDLC_ID,Groups) VALUES")
                        //{
                        //    OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                        //  dab.Fill(dsz, "Groups");
                        //    dab.Dispose();
                        //}
                        if (cmdDel != "DELETE FROM Groups WHERE ;")
                        {
                            OleDbDataAdapter dac = new OleDbDataAdapter(cmdDel, cnb);
                            dac.Fill(ddz, "Groups");
                            dac.Dispose();
                        }
                    }
                }
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
                    command.CommandText += "Selected = @param9, ";
                    command.CommandText += "Comments = @param10 ";
                    //command.CommandText += "AlbumArtPath_Correction = @param6 ";
                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param8", DataGridView1.Rows[i].Cells[8].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param9", DataGridView1.Rows[i].Cells[16].Value.ToString() ?? DBNull.Value.ToString());
                    command.Parameters.AddWithValue("@param10", DataGridView1.Rows[i].Cells[10].Value.ToString() ?? DBNull.Value.ToString());
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        //command.ExecuteNonQuery();
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
                GroupChanged = false;
            }
        }

        public void Populate(ref DataGridView DataGridView, ref BindingSource bs) //, ref BindingSource bsPositions, ref BindingSource bsBadges
        {
            noOfRec = 0;
            //dssx.Dispose();
            lbl_NoRec.Text = " songs.";
            bs.DataSource = null;
            dssx.Dispose();
            //MessageBox.Show("zero " + noOfRec.ToString() + SearchCmd);
            //DB_Path = "../../../../tmp\\Files.accdb;";
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cn);
                //MessageBox.Show("pop" + noOfRec.ToString() + SearchCmd);
                dssx.Clear();
                try
                {
                    da.Fill(dssx, "Main");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("-DB Open in Design Mode or Download Connectivity patch @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                    frm1.ShowDialog();
                    return;
                }
                da.Dispose();
                //  Update_Selected();
            }
            //MessageBox.Show("test");
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn { DataPropertyName = "ID", DisplayIndex = 0, Width = 50, HeaderText = "ID " };
            DataGridViewTextBoxColumn Artist = new DataGridViewTextBoxColumn { DataPropertyName = "Artist", DisplayIndex = 1, HeaderText = "Artist " };
            DataGridViewTextBoxColumn Song_Title = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title", DisplayIndex = 2, HeaderText = "Song_Title " };
            DataGridViewTextBoxColumn Album = new DataGridViewTextBoxColumn { DataPropertyName = "Album", DisplayIndex = 3, HeaderText = "Album " };
            DataGridViewTextBoxColumn Album_Year = new DataGridViewTextBoxColumn { DataPropertyName = "Album_Year", DisplayIndex = 4, HeaderText = "Album_Year " };
            DataGridViewTextBoxColumn Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Track_No", DisplayIndex = 5, HeaderText = "Track_No " };
            DataGridViewTextBoxColumn Author = new DataGridViewTextBoxColumn { DataPropertyName = "Author", DisplayIndex = 6, HeaderText = "Author " };
            DataGridViewTextBoxColumn Version = new DataGridViewTextBoxColumn { DataPropertyName = "Version", DisplayIndex = 7, HeaderText = "Version " };
            DataGridViewTextBoxColumn Import_Date = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Date", DisplayIndex = 8, HeaderText = "Import_Date " };
            DataGridViewTextBoxColumn Is_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Is_Original", DisplayIndex = 9, HeaderText = "Is_Original " };

            DataGridViewTextBoxColumn Song_Title_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Title_Sort", HeaderText = "Song_Title_Sort " };
            DataGridViewTextBoxColumn Artist_Sort = new DataGridViewTextBoxColumn { DataPropertyName = "Artist_Sort", HeaderText = "Artist_Sort " };
            DataGridViewTextBoxColumn AverageTempo = new DataGridViewTextBoxColumn { DataPropertyName = "AverageTempo", HeaderText = "AverageTempo " };
            DataGridViewTextBoxColumn Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Volume", HeaderText = "Volume " };
            DataGridViewTextBoxColumn Preview_Volume = new DataGridViewTextBoxColumn { DataPropertyName = "Preview_Volume", HeaderText = "Preview_Volume " };
            DataGridViewTextBoxColumn AlbumArtPath = new DataGridViewTextBoxColumn { DataPropertyName = "AlbumArtPath", HeaderText = "AlbumArtPath " };
            DataGridViewTextBoxColumn AudioPath = new DataGridViewTextBoxColumn { DataPropertyName = "AudioPath", HeaderText = "AudioPath " };
            DataGridViewTextBoxColumn audioPreviewPath = new DataGridViewTextBoxColumn { DataPropertyName = "audioPreviewPath", HeaderText = "audioPreviewPath " };
            DataGridViewTextBoxColumn DLC_Name = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_Name", HeaderText = "DLC_Name " };
            DataGridViewTextBoxColumn DLC_AppID = new DataGridViewTextBoxColumn { DataPropertyName = "DLC_AppID", HeaderText = "DLC_AppID " };
            DataGridViewTextBoxColumn Current_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Current_FileName", HeaderText = "Current_FileName " };
            DataGridViewTextBoxColumn Original_FileName = new DataGridViewTextBoxColumn { DataPropertyName = "Original_FileName", HeaderText = "Original_FileName " };
            DataGridViewTextBoxColumn Import_Path = new DataGridViewTextBoxColumn { DataPropertyName = "Import_Path", HeaderText = "Import_Path " };
            DataGridViewTextBoxColumn Folder_Name = new DataGridViewTextBoxColumn { DataPropertyName = "Folder_Name", HeaderText = "Folder_Name " };
            DataGridViewTextBoxColumn File_Size = new DataGridViewTextBoxColumn { DataPropertyName = "File_Size", HeaderText = "File_Size " };
            DataGridViewTextBoxColumn File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "File_Hash", HeaderText = "File_Hash " };
            DataGridViewTextBoxColumn Original_File_Hash = new DataGridViewTextBoxColumn { DataPropertyName = "Original_File_Hash", HeaderText = "Original_File_Hash " };
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
            DataGridViewTextBoxColumn Groups = new DataGridViewTextBoxColumn { DataPropertyName = "Groups", HeaderText = "Groups " };
            DataGridViewTextBoxColumn Rating = new DataGridViewTextBoxColumn { DataPropertyName = "Rating", HeaderText = "Rating " };
            DataGridViewTextBoxColumn Description = new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description " };
            DataGridViewTextBoxColumn Comments = new DataGridViewTextBoxColumn { DataPropertyName = "Comments", HeaderText = "Comments " };
            DataGridViewTextBoxColumn Has_Track_No = new DataGridViewTextBoxColumn { DataPropertyName = "Has_Track_No", HeaderText = "Has_Track_No " };
            DataGridViewTextBoxColumn Platform = new DataGridViewTextBoxColumn { DataPropertyName = "Platform", HeaderText = "Platform " };
            DataGridViewTextBoxColumn PreviewTime = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewTime", HeaderText = "PreviewTime " };
            DataGridViewTextBoxColumn PreviewLenght = new DataGridViewTextBoxColumn { DataPropertyName = "PreviewLenght", HeaderText = "PreviewLenght " };
            DataGridViewTextBoxColumn Temp = new DataGridViewTextBoxColumn { DataPropertyName = "Temp", HeaderText = "Temp " };
            DataGridViewTextBoxColumn CustomForge_Followers = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Followers", HeaderText = "CustomForge_Followers " };
            DataGridViewTextBoxColumn CustomForge_Version = new DataGridViewTextBoxColumn { DataPropertyName = "CustomForge_Version", HeaderText = "CustomForge_Version " };
            DataGridViewTextBoxColumn FilesMissingIssues = new DataGridViewTextBoxColumn { DataPropertyName = "FilesMissingIssues", HeaderText = "FilesMissingIssues " };
            DataGridViewTextBoxColumn Duplicates = new DataGridViewTextBoxColumn { DataPropertyName = "Duplicates", HeaderText = "Duplicates " };
            DataGridViewTextBoxColumn Pack = new DataGridViewTextBoxColumn { DataPropertyName = "Pack", HeaderText = "Pack " };
            DataGridViewTextBoxColumn Keep_BassDD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_BassDD", HeaderText = "Keep_BassDD " };
            DataGridViewTextBoxColumn Keep_DD = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_DD", HeaderText = "Keep_DD " };
            DataGridViewTextBoxColumn Keep_Original = new DataGridViewTextBoxColumn { DataPropertyName = "Keep_Original", HeaderText = "Keep_Original " };
            DataGridViewTextBoxColumn Song_Lenght = new DataGridViewTextBoxColumn { DataPropertyName = "Song_Lenght", HeaderText = "Song_Lenght " };
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
            DataGridViewTextBoxColumn File_Creation_Date = new DataGridViewTextBoxColumn { DataPropertyName = "File_Creation_Date", HeaderText = "File_Creation_Date " };

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

            //DataGridView.AutoGenerateColumns = false;

            //DataGridView.Columns.AddRange(new DataGridViewColumn[]
            //{
            //    ID,
            //    Song_Title,
            //    Song_Title_Sort,
            //    Album,
            //    Artist,
            //    Artist_Sort,
            //    Album_Year,
            //    AverageTempo,
            //    Volume,
            //    Preview_Volume,
            //    AlbumArtPath,
            //    AudioPath,
            //    audioPreviewPath,
            //    Track_No,
            //    Author,
            //    Version,
            //    DLC_Name,
            //    DLC_AppID,
            //    Current_FileName,
            //    Original_FileName,
            //    Import_Path,
            //    Import_Date,
            //    Folder_Name,
            //    File_Size,
            //    File_Hash,
            //    Original_File_Hash,
            //    Is_Original,
            //    Is_OLD,
            //    Is_Beta,
            //    Is_Alternate,
            //    Is_Multitrack,
            //    Is_Broken,
            //    MultiTrack_Version,
            //    Alternate_Version_No,
            //    DLC,
            //    Has_Bass,
            //    Has_Guitar,
            //    Has_Lead,
            //    Has_Rhythm,
            //    Has_Combo,
            //    Has_Vocals,
            //    Has_Sections,
            //    Has_Cover,
            //    Has_Preview,
            //    Has_Custom_Tone,
            //    Has_DD,
            //    Has_Version,
            //    Tunning,
            //    Bass_Picking,
            //    Tones,
            //    Groups,
            //    Rating,
            //    Description,
            //    Comments,
            //    Has_Track_No,
            //    Platform,
            //    PreviewTime,
            //    PreviewLenght,
            //    Temp,
            //    CustomForge_Followers,
            //    CustomForge_Version,
            //    FilesMissingIssues,
            //    Duplicates,
            //    Pack,
            //    Keep_BassDD,
            //    Keep_DD,
            //    Keep_Original,
            //    Song_Lenght,
            //    Original,
            //    Selected,
            //    YouTube_Link,
            //    CustomsForge_Link,
            //    CustomsForge_Like,
            //    CustomsForge_ReleaseNotes,
            //    SignatureType,
            //    ToolkitVersion,
            //    Has_Author,
            //    OggPath,
            //    oggPreviewPath,
            //    UniqueDLCName,
            //    AlbumArt_Hash,
            //    Audio_Hash,
            //    audioPreview_Hash,
            //    Bass_Has_DD,
            //    Has_Bonus_Arrangement,
            //    Artist_ShortName,
            //    Album_ShortName,
            //    Available_Old,
            //    Available_Duplicate,
            //    Has_Been_Corrected
            //      File_Creation_Date
            //}
            //);
            DataGridView.AutoResizeColumns();
            bs.ResetBindings(false);
            dssx.Tables["Main"].AcceptChanges();
            bs.DataSource = dssx.Tables["Main"];
            //DataGridView.AutoGenerateColumns = false;
            DataGridView.DataSource = null;
            DataGridView.DataSource = bs;
            //DataGridView.AutoGenerateColumns = false;
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
            public string Groups { get; set; }
            public string Rating { get; set; }
            public string Description { get; set; }
            public string Comments { get; set; }
            public string Has_Track_No { get; set; }
            public string Platform { get; set; }
            public string PreviewTime { get; set; }
            public string PreviewLenght { get; set; }
            public string Temp { get; set; }
            public string CustomForge_Followers { get; set; }
            public string CustomForge_Version { get; set; }
            public string FilesMissingIssues { get; set; }
            public string Duplicates { get; set; }
            public string Pack { get; set; }
            public string Keep_BassDD { get; set; }
            public string Keep_DD { get; set; }
            public string Keep_Original { get; set; }
            public string Song_Lenght { get; set; }
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
            public string Has_BassDD { get; set; }
            public string Has_Bonus_Arrangement { get; set; }
            public string Artist_ShortName { get; set; }
            public string Album_ShortName { get; set; }
            public string Available_Old { get; set; }
            public string Available_Duplicate { get; set; }
            public string Has_Been_Corrected { get; set; }
            public string File_Creation_Date { get; set; }
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
                using (OleDbConnection cnn2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet dus = new DataSet();
                    OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn2); //WHERE id=253
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
                        files[i].Groups = dataRow.ItemArray[50].ToString();
                        files[i].Rating = dataRow.ItemArray[51].ToString();
                        files[i].Description = dataRow.ItemArray[52].ToString();
                        files[i].Comments = dataRow.ItemArray[53].ToString();
                        files[i].Has_Track_No = dataRow.ItemArray[54].ToString();
                        files[i].Platform = dataRow.ItemArray[55].ToString();
                        files[i].PreviewTime = dataRow.ItemArray[56].ToString();
                        files[i].PreviewLenght = dataRow.ItemArray[57].ToString();
                        files[i].Temp = dataRow.ItemArray[58].ToString();
                        files[i].CustomForge_Followers = dataRow.ItemArray[59].ToString();
                        files[i].CustomForge_Version = dataRow.ItemArray[60].ToString();
                        files[i].FilesMissingIssues = dataRow.ItemArray[61].ToString();
                        files[i].Duplicates = dataRow.ItemArray[62].ToString();
                        files[i].Pack = dataRow.ItemArray[63].ToString();
                        files[i].Keep_BassDD = dataRow.ItemArray[64].ToString();
                        files[i].Keep_DD = dataRow.ItemArray[65].ToString();
                        files[i].Keep_Original = dataRow.ItemArray[66].ToString();
                        files[i].Song_Lenght = dataRow.ItemArray[67].ToString();
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
                        files[i].Has_BassDD = dataRow.ItemArray[83].ToString();
                        files[i].Has_Bonus_Arrangement = dataRow.ItemArray[84].ToString();
                        files[i].Artist_ShortName = dataRow.ItemArray[85].ToString();
                        files[i].Album_ShortName = dataRow.ItemArray[86].ToString();
                        files[i].Available_Old = dataRow.ItemArray[87].ToString();
                        files[i].Available_Duplicate = dataRow.ItemArray[88].ToString();
                        files[i].Has_Been_Corrected = dataRow.ItemArray[89].ToString();
                        files[i].File_Creation_Date = dataRow.ItemArray[90].ToString();
                        i++;
                    }
                    //Closing Connection
                    dax.Dispose();
                    cnn2.Close();
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
            return MaximumSize;


        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_Autosave.Checked) SaveRecord();
            chbx_Autosave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;
            this.Close();
        }

        private void btn_DecompressAll_Click(object sender, EventArgs e)
        {
            //txt_Description.Text = DB_Path;
            MainDB frm = new MainDB(DB_Path.Replace("\\Files.accdb", ""), TempPath, false, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb);
            frm.Show();
        }



        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_Autosave.Checked && SaveOK) SaveRecord();
        }

        private void btn_InvertAll_Click(object sender, EventArgs e)
        {

            var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = connection.CreateCommand();
            //dssx = DataGridView1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                command.CommandText = "UPDATE Main SET ";
                command.CommandText += "Selected=@param9 WHERE Selected='Yes' ";
                command.Parameters.AddWithValue("@param9", "Maybe");
                try
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    command.Dispose(); command = connection.CreateCommand();
                    command.CommandText = "UPDATE Main SET ";
                    command.CommandText += "Selected=@param19 WHERE Selected='No' ";
                    command.Parameters.AddWithValue("@param19", "No");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        command.Dispose(); command = connection.CreateCommand();
                        command.CommandText = "UPDATE Main SET ";
                        command.CommandText += "Selected = @param39 WHERE Selected='Maybe' ";
                        command.Parameters.AddWithValue("@param38", "Yes");
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
                            MessageBox.Show("Can not open Main DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
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
                        MessageBox.Show("Can not open MAin DB connection in Cache Edit screen ! " + DB_Path + "-" + command.CommandText);
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
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
                MessageBox.Show("DB Removed values have been inversed ;) Enjoy!");
            }
        }



        public void FTPFile(string filel, string filen)
        {
            // Get the object used to communicate with the server.
            var ddd = filel + filen;
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

            byte[] b = File.ReadAllBytes(TempPath + "\\0_dlcpacks\\manipulated\\" + filen);

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
            // if (chbx_FTP2.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 FAV - BLES01862/PS3_GAME/USRDIR/";
        }

        private void chbx_FTP1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbx_FTP1.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 ALL DLC - BLUS31182/PS3_GAME/USRDIR/";
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    using (var fbd = new VistaFolderBrowserDialog())
        //    {
        //        if (fbd.ShowDialog() != DialogResult.OK)
        //            return;
        //        var temppath = fbd.SelectedPath;
        //        txt_VLCPath.Text = temppath;
        //        //-Save the location in the Config file/reg
        //        //ConfigRepository.Instance()["ManageTempFolder"] = temppath;
        //    }
        //}

        //private void chbx_VLCHome_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chbx_VLCHome.Checked) txt_VLCPath.Text = "DLCManager\\VLCPortable.exe";
        //}

        private void btn_PlayPreview_Click(object sender, EventArgs e)
        {   //alternative impl could use http://nvorbis.codeplex.com/documentation
            //txt_VLCPath.Text = "DLCManager\\VLCPortable.exe" + " ";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
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

        private void btn_PlayAudio_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
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



        private void cbx_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Format.Text == "PS3")
            {
                chbx_PreSavedFTP.Enabled = true;
                if (chbx_PreSavedFTP.Text == "EU") txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP1"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLUS31182/USRDIR/DLC/";
                else txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP2"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLES01862/USRDIR/DLC/"; txt_FTPPath.Text = "";
            }
            else
            {
                chbx_PreSavedFTP.Enabled = false;
                txt_FTPPath.Text = RocksmithDLCPath;
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

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (txt_Album.Text != "") ChangeRow();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //int halfWay = (DataGridView1.DisplayedRowCount(false) / 2);
            //if (DataGridView1.FirstDisplayedScrollingRowIndex + halfWay > DataGridView1.SelectedRows[0].Index ||
            //    (DataGridView1.FirstDisplayedScrollingRowIndex + DataGridView1.DisplayedRowCount(false) - halfWay) <= DataGridView1.SelectedRows[0].Index)
            //{
            //    int targetRow = DataGridView1.SelectedRows[0].Index;
            //    targetRow = Math.Max(targetRow - halfWay, 0);
            //            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.SelectedRows+1;
            //}
            //if (DataGridView1.CurrentCell.ReadOnly)
            //    DataGridView1.CurrentCell = GetNextCell(DataGridView1.CurrentCell);


            // now get the testinfo at the gridcoordinate
            //DataGridView.HitTestInfo hti = 10; //grdJobs.HitTest(pt.X, pt.Y);

            // is it a cell?
            //if (hti.Type == DataGridViewHitTestType.Cell)
            //{
            // make the cell the current on and select the row
            //DataGridView1.ClearSelection();
            //DataGridView1.CurrentCell = DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex+1].Cells[1];
            //DataGridView1.Rows[DataGridView1.SelectedCells[0].RowIndex+1].Selected = true;

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex + 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            row = DataGridView1.Rows[rowindex + 1];

            // do whatever you want to do, set contextmenu text for example

        }
        private DataGridViewCell GetNextCell(DataGridViewCell currentCell)
        {            //DataGridView1.
            int i = 0; //DataGridViewCell currentCell = DataGridView1.CurrentCell;
            DataGridViewCell nextCell = currentCell;

            do
            {
                int nextCellIndex = (nextCell.ColumnIndex + 1) % DataGridView1.ColumnCount;
                int nextRowIndex = nextCellIndex == 0 ? (nextCell.RowIndex + 1) % DataGridView1.RowCount : nextCell.RowIndex;
                nextCell = DataGridView1.Rows[nextRowIndex].Cells[nextCellIndex];
                i++;
            } while (i < DataGridView1.RowCount * DataGridView1.ColumnCount && nextCell.ReadOnly);

            return nextCell;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;

            //SaveRecord();// ChangeRow();
            //int rowindex;
            //DataGridViewRow row;
            //var i = DataGridView1.SelectedCells[0].RowIndex;
            //rowindex = i;// DataGridView1.SelectedRows[0].Index;
            ////DataGridView1.Rows[rowindex + 1].Selected = true;           
            ////DataGridView1.Rows[rowindex].Selected = false;
            ////if (prev>txt_Counter.Text.ToInt32()) row = DataGridView1.Rows[rowindex + 1];
            ////else row = DataGridView1.Rows[rowindex - 1];
            ////if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //txt_Counter.Text = prev.ToString();

        }

        private void btn_NextItem_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (DataGridView1.Rows.Count == prev + 2) return;

            SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex + 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataGridView1.Rows[rowindex + 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            txt_Counter.Text = prev.ToString();
        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataGridView1.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (prev == 0) return;

            SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataGridView1.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataGridView1.Rows[rowindex - 1].Selected = true;
            DataGridView1.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataGridView1.Rows[rowindex - 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            txt_Counter.Text = prev.ToString();
        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            var cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"Retail\");";
            DataSet dsz = new DataSet();
            using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                dab.Fill(dsz, "Groups");
                dab.Dispose();
            }
            GroupChanged = true;
            ChangeRow();
        }

        private void btn_GroupsRemove_Click(object sender, EventArgs e)
        {
            var cmd = "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
            try
            {
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    DataSet dhs = new DataSet();

                    OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
                    dhx.Fill(dhs, "Groups");
                    dhx.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not Delete Song folder ! ");
            }
            GroupChanged = true;
            ChangeRow();
        }

        private void btn_GroupSave_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text != null || chbx_Group.Text != "")
            {
                //var cmd = "UPDATE Removed=\"Yes\" FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
                var cmd = "DELETE FROM Groups WHERE Type=\"Retail\" AND Groups= \"" + chbx_Group.Text + "\"";
                try
                {
                    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        DataSet dhs = new DataSet();

                        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
                        dhx.Fill(dhs, "Groups");
                        dhx.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Can not Delete Song folder ! ");
                }

                //calc how many rows are filled with values
                DataSet dssx = new DataSet();
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
                var recs = dssx.Tables[0].Rows.Count;
                pB_ReadDLCs.Value = 0;
                if (recs != 0)
                    pB_ReadDLCs.Maximum = 2 * recs;
                DataSet dsz = new DataSet();
                for (var i = 0; i < recs; i++)
                {
                    string IDD = DataGridView1.Rows[i].Cells["ID"].Value.ToString();
                    cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + IDD + "\",\"" + chbx_Group.Text + "\",\"Retail\");";
                    if (DataGridView1.Rows[i].Cells["Removed"].Value.ToString() != "Yes")
                        using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                            dab.Fill(dsz, "Groups");
                            dab.Dispose();
                        }

                    pB_ReadDLCs.Increment(2);
                }
            }
        }

        private void btn_GroupLoad_Click(object sender, EventArgs e)
        {
            if (chbx_Group.Text != null || chbx_Group.Text != "")
            {
                var connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
                var command = connection.CreateCommand();
                //dssx = DataGridView1;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    command.CommandText = "UPDATE Cache SET ";
                    command.CommandText += "Removed = @param8 ";
                    command.CommandText += "Selected = @param9, ";
                    command.Parameters.AddWithValue("@param8", "Yes");
                    command.Parameters.AddWithValue("@param9", "No");
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show("Can not Delete Song folder ! ");
                    }
                }


                var cmd = "UPDATE Cache SET Removed=\"No\",Selected=\"Yes\" WHERE cstr(ID) IN (SELECT CDLC_ID From Groups WHERE Groups=\"" + chbx_Group.Text + "\")";
                try
                {
                    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        DataSet dhs = new DataSet();

                        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cnn);
                        dhx.Fill(dhs, "Cache");
                        dhx.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Can not Delete Song folder ! ");
                }
            }
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
            ChangeRow();
        }

        private void chbx_AllGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            GroupChanged = true;
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param9 ";
            command.Parameters.AddWithValue("@param9", "Yes");

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                //throw;
            }
            finally
            {
                //if (cnn != null) cnn.Close();
            }
            //}
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();


            var com = "SELECT * FROM Main";
            try
            {
                DataSet dhs = new DataSet();
                using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected and Unmarked as Removed :)");
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                MessageBox.Show("Error at select filtered " + ee);
            }
        }

        private void btn_SelectNone_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param9 ";
            command.Parameters.AddWithValue("@param8", "No");

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                cnn.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                throw;
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
            Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
            DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataGridView1.Refresh();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected");
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)//chbx_Group_SelectedValueChanged
        {

            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Cache WHERE ";
            var Filtertxt = cmb_Filter.Text;//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {

                case "Pc":
                    SearchCmd += "Platform = 'Pc'";
                    break;
                case "PS3":
                    SearchCmd += "Platform = 'PS3'";
                    break;
                case "Mac":
                    SearchCmd += "Platform ='Mac'";
                    break;
                case "XBOX360":
                    SearchCmd += "Platform = 'XBOX360'";
                    break;
                //0ALL
                case "0ALL":
                    SearchCmd = SearchCmd.Replace(" WHERE ", "");
                    break;
                case "Removed":
                    SearchCmd = "Removed = 'Yes'";
                    break;
                case "Selected":
                    SearchCmd = "Selected = 'Yes'";
                    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY ID";
            //MessageBox.Show(Filtertxt + SearchCmd);
            try
            {
                //this.DataGridView1.DataSource = null; //Then clear the rows:

                //this.DataGridView1.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chbx_Selected_Click(object sender, EventArgs e)
        {

        }

        private void txt_Album_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Filter_SelectedValueChanged1(object sender, EventArgs e)
        {
            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Cache u WHERE ";
            var Filtertxt = cmb_Filter.Text;//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {
                case "Mac":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "PS3":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "XBOX":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "PC":
                    SearchCmd += "Has_Cover <> 'Yes'";
                    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY Artist, Album_Year, Album, Song_Title ";
            //MessageBox.Show(Filtertxt + SearchCmd);
            try
            {
                this.DataGridView1.DataSource = null; //Then clear the rows:

                this.DataGridView1.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(txt_FTPPath.Text);
            var i = 0;
            var found = "\"";
            var newn = "";
            var norec = 0;
            var t = "";
            var j = 0;

            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = fileEntries.Length;
            pB_ReadDLCs.Increment(1);
            foreach (string fileName in fileEntries)
            {
                if (fileName.IndexOf("rs1compatibilitydlc") > 0) continue;
                i++;
                //calc hash and file size
                System.IO.FileInfo fi = null;
                try
                {
                    fi = new System.IO.FileInfo(fileName);
                }
                catch (System.IO.FileNotFoundException ee)
                {
                    // To inform the user and continue is 
                    // sufficient for this demonstration. 
                    // Your application may require different behavior.
                    Console.WriteLine(ee.Message);
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                    frm1.ShowDialog();
                    //                        return;
                    //continue;
                }
               // var cmds = "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileName=\"" + fi.Name + "\";";
                DataSet dfs = new DataSet();

                dfs = DLCManager.SelectFromDB("Pack_AuditTrail", "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileName=\"" + fi.Name + "\";");

                //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                //{
                //    try
                //    {
                //        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                //        da.Fill(dfs, "Pack_AuditTrail");
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                    norec = dfs.Tables[0].Rows.Count;
                    if (norec == 0) newn += fileName + "\",";//dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\",";
                                    else found += dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\",\"";
               // }
                pB_ReadDLCs.Increment(1);
            }
            found = found.Substring(0, found.Length - 2);
            var newnn = (newn.Length != 0) ? newn.Substring(0, newn.Length - 2) : "";
            newn = "";
            pB_ReadDLCs.Increment(-(newnn.Split(',').Length));
            for (j = 0; j < newnn.Split(',').Length; j++)
            {
                //var foundn = (newn != "") ? ((newn.Split(',').Length== 0) ? 1 : newn.Split(',').Length ): 0;
                //if (foundn > 0)
                //{
                //    if (foundn == 1) t = newn.Replace("\"", "");
                //    else
                t = newnn.Split(',')[j].Replace("\"", "");

                //Copy to decompress/import/FTP
                var tt = TempPath + "\\..\\" + Path.GetFileName(t);
                if (cbx_Format.Text == "PS3")
                    ;// var a = MainDB.FTPFile(txt_FTPPath.Text, source, TempPath, SearchCmd);
                else File.Copy(t, tt, true);

                //quickly read PSARC for basic data
                var browser = new PsarcBrowser(t);
                var songList = browser.GetSongList();
                var toolkitInfo = browser.GetToolkitInfo();
                foreach (var song in songList)
                {
                    var noreca = 0;
                    var norecb = 0;
                    var norecc = 0;
                    var norecd = 0;
                    var norece = 0;
                    var norecf = 0;
                    //var nu=song.Identifier;
                    //var cmds = "SELECT ID FROM Main WHERE Song_Title like \"" + song.Title + "*\";";
                    //var cmds = "SELECT ID FROM Main WHERE DLC_Name=\"" + song.Identifier + "\";";
                    //Generating the HASH code
                    var FileHash = "";
                    using (FileStream fs = File.OpenRead(tt))
                    {
                        SHA1 sha = new SHA1Managed();
                        FileHash = BitConverter.ToString(sha.ComputeHash(fs));
                        fs.Close();
                    }
                    DataSet dfa = new DataSet();
                    dfa = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name=\"" + song.Identifier + "\";");
                    DataSet dfb = new DataSet();
                    dfb = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + song.Title + "\";");
                    DataSet dfc = new DataSet();
                    dfc = DLCManager.SelectFromDB("Pack_AuditTrail", "SELECT DLC_ID FROM Pack_AuditTrail WHERE FileHash=\"" + FileHash + "\";");
                    DataSet dff = new DataSet();
                    dff = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name =\"" + song.Identifier.Substring(6, song.Identifier.Length - 6) + "\";");
                    DataSet dfd = new DataSet();
                    dfd = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE DLC_Name like \"*" + song.Identifier.Substring(6, song.Identifier.Length - 6) + "*\";");
                    DataSet dfe = new DataSet();
                    dfe = DLCManager.SelectFromDB("Main", "SELECT ID FROM Main WHERE Song_Title=\"" + song.Title + "\";");
                    //DataSet dfc = new DataSet();
                    //using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    //{
                    //    try
                    //    {
                    //        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                    //        da.Fill(dfb, "Main");
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    noreca = dfa.Tables[0].Rows.Count;
                    norecb = dfb.Tables[0].Rows.Count;
                    norecc = dfc.Tables[0].Rows.Count;
                    norecd = dfd.Tables[0].Rows.Count;
                    norece = dfe.Tables[0].Rows.Count;
                    norecf = dff.Tables[0].Rows.Count;
                    DataSet fxd = new DataSet();
                    if (norecc == 1) fxd = dfc;
                    else if (noreca == 1) fxd = dfa;
                    else if (norecb == 1) fxd = dfb;
                    else if (norecf == 1) fxd = dff;
                    else if (norecd == 1) fxd = dfd;
                    else if (norece == 1) fxd = dfe;
                    if (noreca == 1 || norecb == 1 || norec == 1 || norecd == 1 || norece == 1 || norecf == 1)
                    {
                        //  newn += ",\"" + t + "\",";//dfs.Tables[0].Rows[0].ItemArray[0].ToString() + "\",";
                        //calc file size
                        System.IO.FileInfo fi = null;
                        try
                        {
                            fi = new System.IO.FileInfo(tt);
                        }
                        catch (System.IO.FileNotFoundException ee)
                        {
                            // To inform the user and continue is 
                            // sufficient for this demonstration. 
                            // Your application may require different behavior.
                            Console.WriteLine(ee.Message);
                            // rtxt_StatisticsOnReadDLCs.Text = "error at pack details save" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                            ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                            frm1.ShowDialog();
                            //                        return;
                            //continue;
                        }



                        var fnn = t;
                        string updatecmdA = "CopyPath, PackPath, FileName, PackDate, FileHash, FileSize, DLC_ID, DLC_Name";
                        var fnnon = Path.GetFileName(fnn);
                        var packn = fnn.Substring(0, fnn.IndexOf(fnnon));
                        var udatevA = "\"" + t + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + FileHash + "\",\"" + fi.Length + "\"," + fxd.Tables[0].Rows[0].ItemArray[0].ToString() + ",\"" + "nada" + "\"";
                        // var udatevA = "Select "+ "\"" + dest + "\",\"" + packn + "\",\"" + fnnon + "\",\"" + DateTime.Today.ToString() + "\",\"" + fi.Length + "\",\"" + FileHash + "\"," + file.ID + ",\"" + file.DLC_Name + "\"" + " FROM Pack_AuditTrail WHERE FileHash<>\"" + FileHash + "\"";
                        DLCManager.InsertIntoDBwValues("Pack_AuditTrail", updatecmdA, udatevA);
                        found += dfb.Tables[0].Rows[0].ItemArray[0].ToString() + "\",\"";
                    }
                    //  else if (norec == 1) found += dfb.Tables[0].Rows[0].ItemArray[0].ToString() + "\",\"";
                    else newn += t + "\",";

                    pB_ReadDLCs.Increment(1);
                }

                //Upack
                //var unpackedDir = "";
                ////rtxt_StatisticsOnReadDLCs.Text = "1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //var packagePlatform = FullPath.GetPlatform();
                //if (!chbx_Additional_Manipulations.GetItemChecked(38)) //39. Use only unpacked songs already in the 0/dlcpacks folder
                //{
                //    var fgf = ConfigRepository.Instance()["general_wwisepath"] + "\\Authoring\\Win32\\Release\\bin\\Wwise.exe";
                //    if (!File.Exists(fgf))//Help\\WwiseHelp_en.chm"))//
                //    {
                //        ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2014.1.6 build 5318with Authorithy binaries : " + Environment.NewLine + "A restart is required for the Conversion to WEM, process to be succesfull, else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error at WEM Creation", true, true);
                //        frm1.ShowDialog();
                //        if (frm1.IgnoreSong) break;
                //        if (frm1.StopImport) { j = 10; i = 9999; break; }
                //    }
                //    try
                //    {
                // UNPACK
                //    if (chbx_Additional_Manipulations.GetItemChecked(51))
                //        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, null);//true,
                //    else
                //        unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, false, null);//true,
                //                                                                                   //packagePlatform = FullPath.GetPlatform();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Unpacking ..." + ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    // MessageBox.Show("Error decompressing the file!(BACH OFFICIAL DLC CAUSE OF WEIRD CHAR IN FILENAME) " + "-" );
                //    rtxt_StatisticsOnReadDLCs.Text = ex.Message + "problem at unpacking" + FullPath + "---" + Temp_Path_Import + "...\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    errr = false;
                //    //rtxt_StatisticsOnReadDLCs.Text = "predone" + "..." + rtxt_StatisticsOnReadDLCs.Text;
                //    try
                //    {
                //        var Pathh = broken_Path_Import + "\\" + ds.Tables[0].Rows[i].ItemArray[2].ToString();
                //        if (chbx_Additional_Manipulations.GetItemChecked(30))
                //        {
                //            File.Copy(FullPath, Pathh, true);//.GetPlatform() FullPath.Substring(FullPath.LastIndexOf("\\")+1, FullPath.Length));  
                //            File.Delete(FullPath);
                //        }
                //        UpdatePackingLog("LogImportingError", DB_Path, packid, Pathh.Replace("'", ""), tst);

                //        errr = true; //bcapi???
                //    }
                //    catch (System.IO.FileNotFoundException ee)
                //    {
                //        rtxt_StatisticsOnReadDLCs.Text = "FAILED2" + ee.Message + "----" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        Console.WriteLine(ee.Message);
                //    }
                //}

                ////}
            }


            found = found.Substring(0, found.Length - 2) == "," ? found.Substring(0, found.Length - 1) : found;
            newn = newn.Substring(0, newn.Length - 2);

            SearchCmd = "SELECT * FROM Main u WHERE ";
            SearchCmd += "CSTR(u.ID) IN (" + found + ")";

            SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ";
            //MessageBox.Show(Filtertxt + SearchCmd);
            try
            {
                this.DataGridView1.DataSource = null; //Then clear the rows:

                this.DataGridView1.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                dssx.Dispose();
                Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
                DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataGridView1.Refresh();
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Song Recognized/Read " + found.Split(',').Length + "/" + i + " from " + txt_FTPPath.Text);

        }
    }
}

