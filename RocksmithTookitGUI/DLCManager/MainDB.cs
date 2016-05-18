using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//bcapi
using System.Data.OleDb;
//using RocksmithToolkitGUI.OggConverter;//convert ogg to wem
using RocksmithToolkitLib.Ogg;//convert ogg to wem
using RocksmithToolkitLib.Xml; //For xml read library
using RocksmithToolkitLib.Extensions;
using System.IO;
using Ookii.Dialogs; //cue text
using System.Net; //4ftp
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToolkitLib;//4REPACKING
using System.Collections.Specialized;//webparsing
using System.Security.Cryptography; //For File hash
using System.Data.SqlClient;
using RocksmithToolkitLib.DLCPackage.AggregateGraph;
using RocksmithToolkitGUI.DLCPackageCreator;
//using Newtonsoft.Json;
//using SpotifyAPI.Web.Enums;
//using SpotifyAPI.Web;
//using SpotifyAPI.Web.Models;

using System.Threading;
using RocksmithToolkitLib.Sng;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class MainDB : Form
    {
        public MainDB(string txt_DBFolder, string txt_TempPath, bool updateDB, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete)
        {
            InitializeComponent();
            //MessageBox.Show("test0");
            DB_Path = txt_DBFolder;
            DB_Path = DB_Path + "\\Files.accdb";
            TempPath = txt_TempPath;
            RocksmithDLCPath = txt_RocksmithDLCPath;
            updateDBb = updateDB;
            AllowEncriptb = AllowEncript;
            AllowORIGDeleteb = AllowORIGDelete;
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");
        public BackgroundWorker bwRGenerate = new BackgroundWorker(); //bcapi
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when repacking
        public bool SaveOK = false;
        public bool SearchON = false;
        public bool SearchExit = false;
        public bool AddPreview = false;
        DLCPackageData data;

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public bool GroupChanged = false;
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        public bool updateDBb = false;
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        public string GetTrkTxt = "";
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void MainDB_Load(object sender, EventArgs e)
        {
            //DataAccess da = new DataAccess();
            //MessageBox.Show("test0");

            btn_Copy_old.Text = char.ConvertFromUtf32(8595);
            btn_Copy_old.Enabled = true;
            var Fields = "ID, Artist, Song_Title, Track_No, Album, Album_Year, Author, Version, Import_Date, Is_Original, Selected, Tunning, Bass_Picking, Is_Beta, Platform, Has_DD, Bass_Has_DD, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, Groups, Rating, Description, PreviewTime, PreviewLenght, Song_Lenght, Comments, FilesMissingIssues, DLC_AppID, DLC_Name, Artist_Sort, Song_Title_Sort, AverageTempo, Volume, Preview_Volume, SignatureType, ToolkitVersion, AlbumArtPath, AudioPath, audioPreviewPath, OggPath, oggPreviewPath, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, File_Size, Current_FileName, Original_FileName, Import_Path, Folder_Name, File_Hash, Original_File_Hash, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Bonus_Arrangement, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_Version, Has_Author, Has_Track_No, File_Creation_Date, Has_Been_Corrected, Pack, Available_Old, Available_Duplicate, Tones, Keep_BassDD, Keep_DD, Keep_Original, Original, YouTube_Link, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, UniqueDLCName, Artist_ShortName, Album_ShortName, DLC, Is_OLD";
            SearchCmd = "SELECT " + Fields + " FROM Main u ORDER BY Artist, Album_Year, Album, Track_No, Song_Title;";
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            btn_Search.Enabled = false;
            //Defaults
            chbx_PreSavedFTP.Text = ConfigRepository.Instance()["dlcm_FTP"];
            txt_FTPPath.Text = chbx_PreSavedFTP.Text == "EU" ? ConfigRepository.Instance()["dlcm_FTP1"] : ConfigRepository.Instance()["dlcm_FTP2"];
            txt_PreviewStart.Value.AddMinutes(10);
            if (ConfigRepository.Instance()["dlcm_RemoveBassDD"] == "Yes") chbx_RemoveBassDD.Checked = true;
            else chbx_RemoveBassDD.Checked = false;
            if (ConfigRepository.Instance()["dlcm_UniqueID"] == "Yes") chbx_UniqueID.Checked = true;
            else chbx_UniqueID.Checked = false;
            if (ConfigRepository.Instance()["dlcm_andCopy"] == "Yes") chbx_Copy.Checked = true;
            else chbx_Copy.Checked = false;
            chbx_AutoSave.Checked = ConfigRepository.Instance()["dlcm_Autosave"] == "Yes" ? true : false;

            // Loads Groups in chbx_AllGroups Filter box cmb_Filter //Create Groups list Dropbox
            DataSet dsn = new DataSet();
            var norec = 0;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                string SearchCmd = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";";
                OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
                da.Fill(dsn, "Main");
                norec = dsn.Tables[0].Rows.Count;

                if (norec > 0)
                {
                    ////remove items
                    //if (chbx_Group.Items.Count > 0)
                    //{
                    //    chbx_Group.DataSource = null;
                    //    for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                    //    {
                    //        chbx_Group.Items.RemoveAt(k);
                    //        chbx_AllGroups.Items.RemoveAt(k);
                    //    }
                    //}
                    //add items
                    for (int j = 0; j < norec; j++)
                    {
                        cmb_Filter.Items.Add("Group " + dsn.Tables[0].Rows[j][0].ToString());
                        //chbx_AllGroups.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                    }
                }
            }
            chbx_Format.Text = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? "PC" : ((ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? "Mac" : ((ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? "PS3" : ((ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? "XBOX360" : "-")));

            if (ConfigRepository.Instance()["dlcm_Debug"] == "Yes")
            {
                txt_debug.Visible = true;
                btn_Debug.Visible = true;
                txt_OggPath.Visible = true;
                txt_OggPreviewPath.Visible = true;
                txt_Lyrics.Visible = true;
                txt_AlbumArtPath.Visible = true;
                btn_AddSections.Enabled = true;
            }

            if (ConfigRepository.Instance()["general_defaultauthor"] == "" || ConfigRepository.Instance()["general_defaultauthor"] == "Custom Song Creator") ConfigRepository.Instance()["general_defaultauthor"] = "catara";
            //MessageBox.Show(updateDBb.ToString());
            if (lbl_NoRec.Text != "0 records." && noOfRec > 0)
                if (!Directory.Exists(DataViewGrid.Rows[0].Cells["Folder_Name"].Value.ToString()) || updateDBb)
                {
                    var tmpp = "\\ORIG"; var OLD_Path = ""; var cmd = "";
                    for (var h = 0; h < 2; h++)
                    {
                        try
                        {
                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                            {
                                var SearchCmd1 = "SELECT top 1 AlbumArtPath, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE AudioPath LIKE '%" + tmpp + "%' AND audioPreviewPath is not null and oggPreviewPath is not null;";
                                DataSet duk = new DataSet();
                                OleDbDataAdapter dal = new OleDbDataAdapter(SearchCmd1, cnn); //WHERE id=253
                                dal.Fill(duk, "Main");

                                if (duk.Tables[0].Rows.Count > 0)
                                    OLD_Path = duk.Tables[0].Rows[0].ItemArray[0].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[0].ToString().IndexOf(tmpp)) + tmpp;
                                if (OLD_Path != "")
                                    if (!Directory.Exists(OLD_Path) || updateDBb)
                                    {
                                        //var cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath,'" + OLD_Path + "','" + TempPath + tmpp + "'), AudioPath=REPLACE(AudioPath,'" + OLD_Path + "','" + TempPath + tmpp + "')";
                                        //cmd += ", audioPreviewPath=REPLACE(audioPreviewPath,'" + OLD_Path + "','" + TempPath + tmpp + "'), Folder_Name=REPLACE(Folder_Name,'" + OLD_Path + "','" + TempPath + tmpp + "');";
                                        cmd = "UPDATE Main SET AlbumArtPath=REPLACE(AlbumArtPath, left(AlbumArtPath,instr(AlbumArtPath, '" + tmpp + "')-1),'" + TempPath + "'), AudioPath=REPLACE(AudioPath,left(AudioPath,instr(AudioPath, '" + tmpp + "')-1),'" + TempPath + "')";
                                        cmd += ", Folder_Name=REPLACE(Folder_Name, left(Folder_Name,instr(Folder_Name, '" + tmpp + "')-1),'" + TempPath + "')";
                                        cmd += " WHERE instr(AlbumArtPath, '" + tmpp + "')>0";
                                        //txt_Description.Text = cmd;
                                        //MessageBox.Show(duk.Tables[0].Rows[0].ItemArray[0].ToString());
                                        DialogResult result1 = MessageBox.Show("DB Repository has been moved from " + OLD_Path + "\n\n to " + TempPath + tmpp + "\n\n-" + cmd, MESSAGEBOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (result1 == DialogResult.Yes)  //|| updateDBb
                                        //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                                        {
                                            DataSet dus = new DataSet();
                                            OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                            dax.Fill(dus, "Main");
                                            dax.Dispose();
                                            // MessageBox.Show("Main table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            DataSet ds = new DataSet();
                                            OLD_Path = duk.Tables[0].Rows[0].ItemArray[2].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[2].ToString().IndexOf(tmpp)) + tmpp;
                                            cmd = "UPDATE Main SET OggPath=REPLACE(OggPath, left(OggPath,instr(OggPath, '" + tmpp + "')-1),'" + TempPath + tmpp + "') WHERE OggPath IS NOT NULL AND instr(OggPath, '" + tmpp + "')>0";
                                            OleDbDataAdapter dac = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                            // MessageBox.Show(cmd + "\n" + OLD_Path + "\n" + TempPath + tmpp);
                                            dac.Fill(ds, "Main");
                                            dac.Dispose();

                                            //tmpp = "\\ORIG";
                                            //MessageBox.Show(duk.Tables[0].Rows[0].ItemArray[3].ToString());
                                            OLD_Path = duk.Tables[0].Rows[0].ItemArray[3].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[3].ToString().IndexOf(tmpp)) + tmpp;
                                            cmd = "UPDATE Main SET oggPreviewPath=REPLACE(oggPreviewPath, left(oggPreviewPath,instr(oggPreviewPath, '" + tmpp + "')-1),'" + TempPath + tmpp + "'), audioPreviewPath=REPLACE(audioPreviewPath, left(audioPreviewPath,instr(audioPreviewPath, '" + tmpp + "')-1),'" + TempPath + "') WHERE oggPreviewPath IS NOT NULL AND instr(oggPreviewPath, '" + tmpp + "')>0";
                                            OleDbDataAdapter daH = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                            //MessageBox.Show("3");
                                            daH.Fill(ds, "Main");
                                            daH.Dispose();
                                            MessageBox.Show("Main table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            //OLD_Path = DataGridView1.Rows[0].Cells[].Value.ToString().Substring(0, DataGridView1.Rows[0].Cells[77].Value.ToString().IndexOf("\\0\\")) + "\\0"; //files[0].Folder_Name.Replace
                                            cmd = "UPDATE Arrangements SET SNGFilePath=REPLACE(SNGFilePath, left(SNGFilePath,instr(SNGFilePath, '" + tmpp + "')-1),'" + TempPath + tmpp + "'), XMLFilePath=REPLACE(XMLFilePath, left(XMLFilePath,instr(XMLFilePath, '" + tmpp + "')-1),'" + TempPath + tmpp + "') WHERE instr(XMLFilePath, '" + tmpp + "')>0";
                                            OleDbDataAdapter daN = new OleDbDataAdapter(cmd, cnn); //WHERE id=253
                                            //MessageBox.Show(cmd);
                                            daN.Fill(ds, "Arrangements");
                                            daN.Dispose();
                                            MessageBox.Show("Arrangements table has been updated", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Issues with Temp Folder and DB Reporsitory");
                                            return;
                                        }
                                    }
                                cnn.Close();
                            }
                            DataViewGrid.Rows[0].Selected = true;
                            DataViewGrid.Rows[1].Selected = true;
                            DataGridViewRow row;
                            var i = DataViewGrid.SelectedCells[0].RowIndex;
                            int rowindex = i;// DataGridView1.SelectedRows[0].Index;
                            DataViewGrid.Rows[0].Selected = true;
                            DataViewGrid.Rows[rowindex].Selected = false;
                            DataViewGrid.Rows[0].Selected = true;
                            //if (prev>txt_Counter.Text.ToInt32())
                            row = DataViewGrid.Rows[0];
                            btn_SearchReset.Text = "Start Search";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Main DB connection to Update DB Reporsitory ! " + DB_Path + "-" + cmd);
                        }
                        tmpp = "\\CDLC";

                    }
                }
        }

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (true) //(DataGridView1.CurrentCell.IsComboBoxCell())
            {
                if (DataViewGrid.Columns[DataViewGrid.CurrentCell.ColumnIndex].Name == "ContactsColumn")
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
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + DB_Path);
            }
        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            ArrangementsDB frm = new ArrangementsDB(DB_Path, txt_ID.Text, chbx_BassDD.Checked);
            frm.Show();
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            TonesDB frm = new TonesDB(DB_Path, txt_ID.Text);
            frm.Show();
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var line = -1;
            line = DataViewGrid.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
            pB_ReadDLCs.Value = 0;
        }

        public void ChangeRow()
        {
            if (SearchON && SearchExit)
            {
                SearchON = true;
                SearchExit = false;
                btn_Search.Enabled = true;
            }
            else
                btn_Search.Enabled = false;
            //btn_SearchReset.Text = "Start Search";

            int i;
            if (DataViewGrid.SelectedCells.Count > 0 && DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["ID"].ToString() != "")
            {
                //Create Groups list Dropbox
                DataSet ds = new DataSet();
                var norec = 0;
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    string SearchCmd = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\";";
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
                        string SearchCmds = "SELECT DISTINCT Groups FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + DataViewGrid.Rows[DataViewGrid.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString() + "\";";
                        OleDbDataAdapter dfa = new OleDbDataAdapter(SearchCmds, con); //WHERE id=253
                        dfa.Fill(dds, "Main");
                        var nocrec = dds.Tables[0].Rows.Count;

                        if (nocrec > 0)
                            //for (int l = 0; l < norec; l++)
                            for (int j = 0; j < nocrec; j++)
                            //  if (dds.Tables[0].Rows[j][0].ToString() == ds.Tables[0].Rows[l][0].ToString())
                            //  chbx_AllGroups.SetSelected(j, true);
                            {
                                int index = chbx_AllGroups.Items.IndexOf(dds.Tables[0].Rows[j][0].ToString());
                                chbx_AllGroups.SetItemChecked(index, true);
                            }
                        //(ds.Tables[0].Rows[l][0].ToString()); 

                    }

                    i = DataViewGrid.SelectedCells[0].RowIndex;
                    txt_ID.Text = DataViewGrid.Rows[i].Cells["ID"].Value.ToString();
                    txt_Title.Text = DataViewGrid.Rows[i].Cells["Song_Title"].Value.ToString();
                    txt_Title_Sort.Text = DataViewGrid.Rows[i].Cells["Song_Title_Sort"].Value.ToString();
                    txt_Album.Text = DataViewGrid.Rows[i].Cells["Album"].Value.ToString();
                    txt_Artist.Text = DataViewGrid.Rows[i].Cells["Artist"].Value.ToString();
                    txt_Artist_Sort.Text = DataViewGrid.Rows[i].Cells["Artist_Sort"].Value.ToString();
                    txt_Album_Year.Text = DataViewGrid.Rows[i].Cells["Album_Year"].Value.ToString();
                    txt_AverageTempo.Text = DataViewGrid.Rows[i].Cells["AverageTempo"].Value.ToString();
                    txt_Volume.Text = (DataViewGrid.Rows[i].Cells["Volume"].Value.ToString());
                    txt_Preview_Volume.Text = (DataViewGrid.Rows[i].Cells["Preview_Volume"].Value.ToString());
                    txt_AlbumArtPath.Text = DataViewGrid.Rows[i].Cells["AlbumArtPath"].Value.ToString();
                    txt_Track_No.Text = DataViewGrid.Rows[i].Cells["Track_No"].Value.ToString();
                    txt_Author.Text = DataViewGrid.Rows[i].Cells["Author"].Value.ToString();
                    txt_Version.Text = DataViewGrid.Rows[i].Cells["Version"].Value.ToString();
                    txt_DLC_ID.Text = DataViewGrid.Rows[i].Cells["DLC_Name"].Value.ToString();
                    txt_APP_ID.Text = DataViewGrid.Rows[i].Cells["DLC_AppID"].Value.ToString();//SelectedIndex
                    txt_MultiTrackType.Text = DataViewGrid.Rows[i].Cells["MultiTrack_Version"].Value.ToString();// != "" ? txt_MultiTrackType.FindString(DataViewGrid.Rows[i].Cells[32].Value.ToString()) : 5;
                    txt_Alt_No.Text = DataViewGrid.Rows[i].Cells["Alternate_Version_No"].Value.ToString();
                    txt_Tuning.Text = DataViewGrid.Rows[i].Cells["Tunning"].Value.ToString();
                    txt_BassPicking.Text = DataViewGrid.Rows[i].Cells["Bass_Picking"].Value.ToString();
                    //chbx_Group.Text = DataViewGrid.Rows[i].Cells["Groups"].Value.ToString();
                    txt_Rating.Text = DataViewGrid.Rows[i].Cells["Rating"].Value.ToString();
                    txt_Description.Text = DataViewGrid.Rows[i].Cells["Description"].Value.ToString();
                    txt_Platform.Text = DataViewGrid.Rows[i].Cells["Platform"].Value.ToString();
                    if (DataViewGrid.Rows[i].Cells["PreviewTime"].Value.ToString() == "") txt_PreviewStart.Value = Convert.ToDateTime("00:00");
                    else txt_PreviewStart.Value = Convert.ToDateTime("00:" + DataViewGrid.Rows[i].Cells["PreviewTime"].Value.ToString());
                    if (DataViewGrid.Rows[i].Cells["PreviewLenght"].Value.ToString() == "") txt_PreviewEnd.Value = 30;
                    else txt_PreviewEnd.Text = DataViewGrid.Rows[i].Cells["PreviewLenght"].Value.ToString();
                    txt_YouTube_Link.Text = DataViewGrid.Rows[i].Cells["YouTube_Link"].Value.ToString();
                    btn_Youtube.Enabled = DataViewGrid.Rows[i].Cells["YouTube_Link"].Value.ToString() == "" ? false : true;
                    txt_CustomsForge_Link.Text = DataViewGrid.Rows[i].Cells["CustomsForge_Link"].Value.ToString();
                    btn_CustomForge_Link.Enabled = DataViewGrid.Rows[i].Cells["CustomsForge_Link"].Value.ToString() == "" ? false : true;
                    txt_CustomsForge_Like.Text = DataViewGrid.Rows[i].Cells["CustomsForge_Like"].Value.ToString();
                    //txt_CustomsForge_Like.Enabled = DataViewGrid.Rows[i].Cells[72].Value.ToString() == "" ? false : true;
                    txt_CustomsForge_ReleaseNotes.Text = DataViewGrid.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value.ToString();
                    //txt_CustomsForge_ReleaseNotes.Enabled = DataViewGrid.Rows[i].Cells[73].Value.ToString() == "" ? false : true;

                    txt_OggPath.Text = DataViewGrid.Rows[i].Cells["OggPath"].Value.ToString();//.Replace(".ogg", "_fixed.ogg");
                    txt_OggPreviewPath.Text = DataViewGrid.Rows[i].Cells["OggPreviewPath"].Value.ToString();//.Replace(".ogg", "_fixed.ogg");

                    txt_Artist_ShortName.Text = DataViewGrid.Rows[i].Cells["Artist_ShortName"].Value.ToString();
                    txt_Album_ShortName.Text = DataViewGrid.Rows[i].Cells["Artist_ShortName"].Value.ToString();

                    //txt_Volume.Text = DataGridView1.Rows[i].Cells[86].Value.ToString();
                    //txt_Preview_Volume.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();

                    if (DataViewGrid.Rows[i].Cells["Is_Original"].Value.ToString() == "Yes") { chbx_Original.Checked = true; chbx_Original.ForeColor = btn_Debug.ForeColor; }
                    else { chbx_Original.Checked = false; chbx_Original.ForeColor = btn_Duplicate.ForeColor; }
                    if (DataViewGrid.Rows[i].Cells["Is_Beta"].Value.ToString() == "Yes") chbx_Beta.Checked = true;
                    else chbx_Beta.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Is_Alternate"].Value.ToString() == "Yes") { chbx_Alternate.Checked = true; txt_Alt_No.Enabled = true; }
                    else { chbx_Alternate.Checked = false; txt_Alt_No.Enabled = false; }
                    if (DataViewGrid.Rows[i].Cells["Is_Multitrack"].Value.ToString() == "Yes") { chbx_MultiTrack.Checked = true; txt_MultiTrackType.Enabled = true; }
                    else { chbx_MultiTrack.Checked = false; txt_MultiTrackType.Enabled = false; }
                    if (DataViewGrid.Rows[i].Cells["Is_Broken"].Value.ToString() == "Yes") chbx_Broken.Checked = true;
                    else chbx_Broken.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Bass"].Value.ToString() == "Yes") chbx_Bass.Checked = true;
                    else chbx_Bass.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Lead"].Value.ToString() == "Yes") chbx_Lead.Checked = true;
                    else chbx_Lead.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Combo"].Value.ToString() == "Yes") chbx_Combo.Checked = true;
                    else chbx_Combo.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Rhythm"].Value.ToString() == "Yes") chbx_Rhythm.Checked = true;
                    else chbx_Rhythm.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Vocals"].Value.ToString() == "Yes") { chbx_Lyrics.Checked = true; btn_CreateLyrics.Enabled = false; }
                    else { chbx_Lyrics.Checked = false; btn_CreateLyrics.Enabled = true; }
                    if (DataViewGrid.Rows[i].Cells["Has_Sections"].Value.ToString() == "Yes") chbx_Sections.Checked = true;
                    else chbx_Sections.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Cover"].Value.ToString() == "Yes") chbx_Cover.Checked = true;
                    else chbx_Cover.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Preview"].Value.ToString() == "Yes") { chbx_Preview.Checked = true; btn_PlayPreview.Enabled = true; }
                    else { chbx_Preview.Checked = false; btn_PlayPreview.Enabled = false; }
                    if (DataViewGrid.Rows[i].Cells["Has_DD"].Value.ToString() == "Yes") { numericUpDown1.Enabled = false; chbx_DD.Checked = true; btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; }
                    else { chbx_DD.Checked = false; btn_AddDD.Enabled = true; btn_RemoveDD.Enabled = false; } //numericUpDown1.Enabled = true;
                    if (DataViewGrid.Rows[i].Cells["Keep_BassDD"].Value.ToString() == "Yes") { chbx_KeepBassDD.Checked = true; }
                    else { chbx_KeepBassDD.Checked = false; }
                    if (DataViewGrid.Rows[i].Cells["Keep_DD"].Value.ToString() == "Yes") { chbx_KeepDD.Checked = true; }
                    else { chbx_KeepDD.Checked = false; }
                    if (DataViewGrid.Rows[i].Cells["Selected"].Value.ToString() == "Yes") chbx_Selected.Checked = true;
                    else chbx_Selected.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Has_Author"].Value.ToString() == "Yes") chbx_Author.Checked = true;
                    else chbx_Author.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Bass_Has_DD"].Value.ToString() == "Yes") { chbx_BassDD.Checked = true; btn_RemoveBassDD.Enabled = true; chbx_KeepBassDD.Enabled = true; chbx_RemoveBassDD.Enabled = true; }
                    else { chbx_BassDD.Checked = false; btn_RemoveBassDD.Enabled = false; chbx_RemoveBassDD.Enabled = false; }//txt_BassPicking.Text = "";
                    if (DataViewGrid.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
                    else chbx_Bonus.Checked = false;
                    chbx_Avail_Old.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes") { chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; }
                    else { chbx_Avail_Old.Checked = false; btn_OldFolder.Enabled = false; }
                    chbx_Avail_Duplicate.Checked = false;
                    if (DataViewGrid.Rows[i].Cells["Available_Duplicate"].Value.ToString() == "Yes") { chbx_Avail_Duplicate.Checked = true; btn_DuplicateFolder.Enabled = true; }
                    else { chbx_Avail_Duplicate.Checked = false; btn_DuplicateFolder.Enabled = false; }
                    if (DataViewGrid.Rows[i].Cells["Has_Been_Corrected"].Value.ToString() == "Yes") chbx_Has_Been_Corrected.Checked = true;
                    else chbx_Has_Been_Corrected.Checked = false;

                    //Lyrics
                    using (OleDbConnection cnn7 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        string SearchCmdz = "SELECT XMLFilePath FROM Arrangements WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";";
                        OleDbDataAdapter dag = new OleDbDataAdapter(SearchCmdz, cnn7); //WHERE id=253
                        DataSet dsr = new DataSet();
                        dag.Fill(dsr, "Main");
                        if (dsr.Tables[0].Rows.Count > 0) txt_Lyrics.Text = dsr.Tables[0].Rows[0].ItemArray[0].ToString();
                        else txt_Lyrics.Text = "";
                    }

                    //ImageSource imageSource = new BitmapImage(new Uri("C:\\Temp\\music_edit.png"));
                    //txt_Description.Text = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                    picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                    btn_Delete.Enabled = true;
                    btn_Duplicate.Enabled = true;
                    btn_Package.Enabled = true;
                    btn_ChangeCover.Enabled = true;
                    btn_AddPreview.Enabled = true;
                    btn_SelectPreview.Enabled = true;
                    btn_PlayAudio.Enabled = true;
                    //btn_PlayPreview.Enabled = true;
                    //btn_Search.Enabled = false;
                    btn_Save.Enabled = true;
                    txt_Artist_Sort.Enabled = true;
                    txt_Album.Enabled = true;
                    txt_Title_Sort.Enabled = true;

                    cnn.Close();
                    //chbx_Group.Items.Add("");
                }


                if (chbx_AutoSave.Checked) SaveOK = true;
                else SaveOK = false;
            }
        }

        public void button8_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }
        public void SaveRecord()
        {
            int i;
            DataSet dis = new DataSet();

            if (DataViewGrid.SelectedCells.Count > 0 && txt_ID.Text != "")
            {
                i = DataViewGrid.SelectedCells[0].RowIndex;

                DataViewGrid.Rows[i].Cells["Song_Title"].Value = txt_Title.Text;
                //dataGridView1.Rows[0].Cells[“Knight”]
                DataViewGrid.Rows[i].Cells["Song_Title_Sort"].Value = txt_Title_Sort.Text;
                DataViewGrid.Rows[i].Cells["Album"].Value = txt_Album.Text;
                DataViewGrid.Rows[i].Cells["Artist"].Value = txt_Artist.Text;
                DataViewGrid.Rows[i].Cells["Artist_Sort"].Value = txt_Artist_Sort.Text;
                DataViewGrid.Rows[i].Cells["Album_Year"].Value = txt_Album_Year.Text;
                DataViewGrid.Rows[i].Cells["AverageTempo"].Value = txt_AverageTempo.Text;
                DataViewGrid.Rows[i].Cells["Volume"].Value = txt_Volume.Text;
                DataViewGrid.Rows[i].Cells["Preview_Volume"].Value = txt_Preview_Volume.Text;
                DataViewGrid.Rows[i].Cells["AlbumArtPath"].Value = txt_AlbumArtPath.Text;
                DataViewGrid.Rows[i].Cells["Track_No"].Value = txt_Track_No.Text;
                DataViewGrid.Rows[i].Cells["Author"].Value = txt_Author.Text;
                DataViewGrid.Rows[i].Cells["Version"].Value = txt_Version.Text;
                DataViewGrid.Rows[i].Cells["DLC_Name"].Value = txt_DLC_ID.Text;
                DataViewGrid.Rows[i].Cells["DLC_AppID"].Value = txt_APP_ID.Text;
                DataViewGrid.Rows[i].Cells["MultiTrack_Version"].Value = txt_MultiTrackType.Text;
                DataViewGrid.Rows[i].Cells["Alternate_Version_No"].Value = txt_Alt_No.Text;
                DataViewGrid.Rows[i].Cells["Tunning"].Value = txt_Tuning.Text;
                DataViewGrid.Rows[i].Cells["Bass_Picking"].Value = txt_BassPicking.Text;
                DataViewGrid.Rows[i].Cells["Rating"].Value = txt_Rating.Text;
                DataViewGrid.Rows[i].Cells["Description"].Value = txt_Description.Text;
                DataViewGrid.Rows[i].Cells["Platform"].Value = txt_Platform.Text;
                //commented out As i dont want to add a tinestamp altough the preview has not been generated with the Tool
                if (AddPreview) DataViewGrid.Rows[i].Cells["PreviewStart"].Value = txt_PreviewStart.Text;
                if (AddPreview) DataViewGrid.Rows[i].Cells["PreviewEnd"].Value = txt_PreviewEnd.Text;
                DataViewGrid.Rows[i].Cells["Youtube_Playthrough"].Value = txt_Playthough.Text;
                DataViewGrid.Rows[i].Cells["YouTube_Link"].Value = txt_YouTube_Link.Text;
                DataViewGrid.Rows[i].Cells["CustomsForge_Link"].Value = txt_CustomsForge_Link.Text;
                DataViewGrid.Rows[i].Cells["CustomsForge_Like"].Value = txt_CustomsForge_Like.Text;
                DataViewGrid.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value = txt_CustomsForge_ReleaseNotes.Text;
                DataViewGrid.Rows[i].Cells["OggPreviewPath"].Value = txt_OggPreviewPath.Text;
                DataViewGrid.Rows[i].Cells["Artist_ShortName"].Value = txt_Artist_ShortName.Text;
                DataViewGrid.Rows[i].Cells["Album_ShortName"].Value = txt_Album_ShortName.Text;
                if (chbx_Original.Checked) DataViewGrid.Rows[i].Cells["Is_Original"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Is_Original"].Value = "No";
                if (chbx_Beta.Checked) DataViewGrid.Rows[i].Cells["Is_Beta"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Is_Beta"].Value = "No";
                if (chbx_Alternate.Checked) DataViewGrid.Rows[i].Cells["Is_Alternate"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Is_Alternate"].Value = "No";
                if (chbx_MultiTrack.Checked) DataViewGrid.Rows[i].Cells["Is_MultiTrack"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Is_MultiTrack"].Value = "No";
                if (chbx_Broken.Checked) DataViewGrid.Rows[i].Cells["Is_Broken"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Is_Broken"].Value = "No";
                if (chbx_Bass.Checked) DataViewGrid.Rows[i].Cells["Has_Bass"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Bass"].Value = "No";
                if (chbx_Lead.Checked) DataViewGrid.Rows[i].Cells["Has_Lead"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Lead"].Value = "No";
                if (chbx_Rhythm.Checked) DataViewGrid.Rows[i].Cells["Has_Rhythm"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Rhythm"].Value = "No";
                if (chbx_Combo.Checked) DataViewGrid.Rows[i].Cells["Has_Combo"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Combo"].Value = "No";
                if (chbx_Lyrics.Checked) DataViewGrid.Rows[i].Cells["Has_Vocals"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Vocals"].Value = "No";
                if (chbx_Sections.Checked) DataViewGrid.Rows[i].Cells["Has_Sections"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Sections"].Value = "No";
                if (chbx_Cover.Checked) DataViewGrid.Rows[i].Cells["Has_Cover"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Cover"].Value = "No";
                if (chbx_Preview.Checked) DataViewGrid.Rows[i].Cells["Has_Preview"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Preview"].Value = "No";
                if (chbx_DD.Checked) DataViewGrid.Rows[i].Cells["Has_DD"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_DD"].Value = "No";
                if (chbx_TrackNo.Checked) DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "No";
                if (chbx_KeepBassDD.Checked) DataViewGrid.Rows[i].Cells["Keep_BassDD"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Keep_BassDD"].Value = "No";
                if (chbx_KeepDD.Checked) DataViewGrid.Rows[i].Cells["Keep_DD"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Keep_DD"].Value = "No";
                if (chbx_Selected.Checked) DataViewGrid.Rows[i].Cells["Selected"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Selected"].Value = "No";
                if (chbx_Author.Checked) DataViewGrid.Rows[i].Cells["Has_Author"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Author"].Value = "No";
                if (chbx_BassDD.Checked) DataViewGrid.Rows[i].Cells["Bass_Has_DD"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Bass_Has_DD"].Value = "No";
                if (chbx_Bonus.Checked) DataViewGrid.Rows[i].Cells["Has_Bonus_Arrangement"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Has_Bonus_Arrangement"].Value = "No";
                if (chbx_Avail_Old.Checked) DataViewGrid.Rows[i].Cells["Available_Old"].Value = "Yes";
                else DataViewGrid.Rows[i].Cells["Available_Old"].Value = "No";
                //if (chbx_Avail_Duplicate.Checked) DataViewGrid.Rows[i].Cells[88].Value = "Yes";
                //else DataViewGrid.Rows[i].Cells[88].Value = "No";
                //if (chbx_Has_Been_Corrected.Checked) DataViewGrid.Rows[i].Cells[89].Value = "Yes";
                //else DataViewGrid.Rows[i].Cells[89].Value = "No";

                //Save Groups
                if (GroupChanged)
                {
                    var cmdDel = "DELETE FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\"";
                    DataSet dbz = new DataSet();
                    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        OleDbDataAdapter dac = new OleDbDataAdapter(cmdDel, cnb);
                        dac.Fill(dbz, "Groups");
                        dac.Dispose();
                    }

                    DataSet dsz = new DataSet();
                    DataSet ddz = new DataSet();
                    for (int j = 0; j < chbx_AllGroups.Items.Count; j++)
                    {
                        using (OleDbConnection cmb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            //DataSet dooz = new DataSet();
                            //string updatecmd = "SELECT ID FROM Groups WHERE Type=\"DLC\" AND CDLC_ID=\"" + txt_ID.Text + "\" AND Groups=\"" + chbx_AllGroups.Items[j] + "\";";
                            //OleDbDataAdapter dbf = new OleDbDataAdapter(updatecmd, cmb);
                            //dbf.Fill(dooz, "Groups");
                            //dbf.Dispose();

                            var cmd = "INSERT INTO Groups(CDLC_ID,Groups,Type) VALUES";
                            // var rr = dooz.Tables[0].Rows.Count;&& rr == 0
                            if (chbx_AllGroups.GetItemChecked(j))
                            {
                                cmd += "(\"" + txt_ID.Text + "\",\"" + chbx_AllGroups.Items[j] + "\",\"DLC\")";
                                OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cmb);
                                dab.Fill(dsz, "Groups");
                                dab.Dispose();
                            }
                        }
                    }
                    GroupChanged = false;
                }

                //Update Song / Main DB
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
                    command.CommandText += "AudioPreviewPath = @param12, ";
                    command.CommandText += "Track_No = @param13, ";
                    command.CommandText += "Author = @param14, ";
                    command.CommandText += "Version = @param15, ";
                    command.CommandText += "DLC_Name = @param16, ";
                    command.CommandText += "DLC_AppID = @param17, ";
                    command.CommandText += "Is_Original = @param26, ";
                    command.CommandText += "Is_Beta = @param28, ";
                    command.CommandText += "Is_Alternate = @param29, ";
                    command.CommandText += "Is_Multitrack = @param30, ";
                    command.CommandText += "Is_Broken = @param31, ";
                    command.CommandText += "MultiTrack_Version = @param32, ";
                    command.CommandText += "Alternate_Version_No = @param33, ";
                    command.CommandText += "Has_Vocals = @param40, ";
                    command.CommandText += "Has_Sections = @param41, ";
                    command.CommandText += "Has_Cover = @param42, ";
                    command.CommandText += "Has_Preview = @param43, ";
                    command.CommandText += "Has_DD = @param45, ";
                    command.CommandText += "Tunning = @param47, ";
                    command.CommandText += "Bass_Picking = @param48, ";
                    command.CommandText += "Tones = @param49, ";
                    command.CommandText += "Groups = @param50, ";
                    command.CommandText += "Rating = @param51, ";
                    command.CommandText += "Description = @param52, ";
                    command.CommandText += "Has_Track_No = @param54, ";
                    command.CommandText += "PreviewTime = @param56, ";
                    command.CommandText += "PreviewLenght = @param57, ";
                    command.CommandText += "Youtube_Playthrough = @param58, ";
                    command.CommandText += "Keep_BassDD = @param64, ";
                    command.CommandText += "Keep_DD = @param65, ";
                    command.CommandText += "Selected = @param69, ";
                    command.CommandText += "YouTube_Link = @param70, ";
                    command.CommandText += "CustomsForge_Link = @param71, ";
                    command.CommandText += "CustomsForge_Like = @param72, ";
                    command.CommandText += "CustomsForge_ReleaseNotes = @param73, ";
                    command.CommandText += "Has_Author = @param76, ";
                    command.CommandText += "oggPath = @param77, ";
                    command.CommandText += "oggPreviewPath = @param78, ";
                    //command.CommandText += "UniqueDLCName = @param79, ";
                    command.CommandText += "AlbumArt_Hash = @param80, ";
                    command.CommandText += "Audio_Hash = @param81, ";
                    command.CommandText += "AudioPreview_Hash = @param82, ";
                    command.CommandText += "Bass_Has_DD = @param83, ";
                    command.CommandText += "Has_Bonus_Arrangement = @param84, ";
                    command.CommandText += "Artist_ShortName = @param85, ";
                    command.CommandText += "Album_ShortName = @param86 ";//,
                    //command.CommandText += "Available_Old = @param87, ";
                    //command.CommandText += "Available_Duplicate = @param88 ";

                    command.CommandText += "WHERE ID = " + txt_ID.Text;

                    command.Parameters.AddWithValue("@param1", DataViewGrid.Rows[i].Cells["Song_Title"].Value.ToString());
                    command.Parameters.AddWithValue("@param2", DataViewGrid.Rows[i].Cells["Song_Title_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param3", DataViewGrid.Rows[i].Cells["Album"].Value.ToString());
                    command.Parameters.AddWithValue("@param4", DataViewGrid.Rows[i].Cells["Artist"].Value.ToString());
                    command.Parameters.AddWithValue("@param5", DataViewGrid.Rows[i].Cells["Artist_Sort"].Value.ToString());
                    command.Parameters.AddWithValue("@param6", DataViewGrid.Rows[i].Cells["Album_Year"].Value.ToString());
                    command.Parameters.AddWithValue("@param7", DataViewGrid.Rows[i].Cells["AverageTempo"].Value.ToString());
                    command.Parameters.AddWithValue("@param8", DataViewGrid.Rows[i].Cells["Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param9", DataViewGrid.Rows[i].Cells["Preview_Volume"].Value.ToString());
                    command.Parameters.AddWithValue("@param10", DataViewGrid.Rows[i].Cells["AlbumArtPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param12", DataViewGrid.Rows[i].Cells["AudioPreviewPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param13", DataViewGrid.Rows[i].Cells["Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param14", DataViewGrid.Rows[i].Cells["Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param15", DataViewGrid.Rows[i].Cells["Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param16", DataViewGrid.Rows[i].Cells["DLC_Name"].Value.ToString());
                    command.Parameters.AddWithValue("@param17", DataViewGrid.Rows[i].Cells["DLC_AppID"].Value.ToString());
                    command.Parameters.AddWithValue("@param26", DataViewGrid.Rows[i].Cells["Is_Original"].Value.ToString());
                    command.Parameters.AddWithValue("@param28", DataViewGrid.Rows[i].Cells["Is_Beta"].Value.ToString());
                    command.Parameters.AddWithValue("@param29", DataViewGrid.Rows[i].Cells["Is_Alternate"].Value.ToString());
                    command.Parameters.AddWithValue("@param30", DataViewGrid.Rows[i].Cells["Is_Multitrack"].Value.ToString());
                    command.Parameters.AddWithValue("@param31", DataViewGrid.Rows[i].Cells["Is_Broken"].Value.ToString());
                    command.Parameters.AddWithValue("@param32", DataViewGrid.Rows[i].Cells["MultiTrack_Version"].Value.ToString());
                    command.Parameters.AddWithValue("@param33", DataViewGrid.Rows[i].Cells["Alternate_Version_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param40", DataViewGrid.Rows[i].Cells["Has_Vocals"].Value.ToString());
                    command.Parameters.AddWithValue("@param41", DataViewGrid.Rows[i].Cells["Has_Sections"].Value.ToString());
                    command.Parameters.AddWithValue("@param42", DataViewGrid.Rows[i].Cells["Has_Cover"].Value.ToString());
                    command.Parameters.AddWithValue("@param43", DataViewGrid.Rows[i].Cells["Has_Preview"].Value.ToString());
                    command.Parameters.AddWithValue("@param45", DataViewGrid.Rows[i].Cells["Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param47", DataViewGrid.Rows[i].Cells["Tunning"].Value.ToString());
                    command.Parameters.AddWithValue("@param48", DataViewGrid.Rows[i].Cells["Bass_Picking"].Value.ToString());
                    command.Parameters.AddWithValue("@param49", DataViewGrid.Rows[i].Cells["Tones"].Value.ToString());
                    command.Parameters.AddWithValue("@param50", DataViewGrid.Rows[i].Cells["Groups"].Value.ToString());
                    command.Parameters.AddWithValue("@param51", DataViewGrid.Rows[i].Cells["Rating"].Value.ToString());
                    command.Parameters.AddWithValue("@param52", DataViewGrid.Rows[i].Cells["Description"].Value.ToString());
                    command.Parameters.AddWithValue("@param54", DataViewGrid.Rows[i].Cells["Has_Track_No"].Value.ToString());
                    command.Parameters.AddWithValue("@param56", DataViewGrid.Rows[i].Cells["PreviewTime"].Value.ToString());
                    command.Parameters.AddWithValue("@param57", DataViewGrid.Rows[i].Cells["PreviewLenght"].Value.ToString());
                    command.Parameters.AddWithValue("@param58", DataViewGrid.Rows[i].Cells["Youtube_Playthrough"].Value.ToString());
                    command.Parameters.AddWithValue("@param64", DataViewGrid.Rows[i].Cells["Keep_BassDD"].Value.ToString());
                    command.Parameters.AddWithValue("@param65", DataViewGrid.Rows[i].Cells["Keep_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param69", DataViewGrid.Rows[i].Cells["Selected"].Value.ToString());
                    command.Parameters.AddWithValue("@param70", DataViewGrid.Rows[i].Cells["YouTube_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param71", DataViewGrid.Rows[i].Cells["CustomsForge_Link"].Value.ToString());
                    command.Parameters.AddWithValue("@param72", DataViewGrid.Rows[i].Cells["CustomsForge_Like"].Value.ToString());
                    command.Parameters.AddWithValue("@param73", DataViewGrid.Rows[i].Cells["CustomsForge_ReleaseNotes"].Value.ToString());
                    command.Parameters.AddWithValue("@param76", DataViewGrid.Rows[i].Cells["Has_Author"].Value.ToString());
                    command.Parameters.AddWithValue("@param77", DataViewGrid.Rows[i].Cells["oggPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param78", DataViewGrid.Rows[i].Cells["oggPreviewPath"].Value.ToString());
                    command.Parameters.AddWithValue("@param80", DataViewGrid.Rows[i].Cells["AlbumArt_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param81", DataViewGrid.Rows[i].Cells["Audio_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param82", DataViewGrid.Rows[i].Cells["AudioPreview_Hash"].Value.ToString());
                    command.Parameters.AddWithValue("@param83", DataViewGrid.Rows[i].Cells["Bass_Has_DD"].Value.ToString());
                    command.Parameters.AddWithValue("@param84", DataViewGrid.Rows[i].Cells["Has_Bonus_Arrangement"].Value.ToString());
                    command.Parameters.AddWithValue("@param85", DataViewGrid.Rows[i].Cells["Artist_ShortName"].Value.ToString());
                    command.Parameters.AddWithValue("@param86", DataViewGrid.Rows[i].Cells["Album_ShortName"].Value.ToString());
                    //command.Parameters.AddWithValue("@param87", DataViewGrid.Rows[i].Cell"Album_ShortName"s[87].Value.ToString());
                    //command.Parameters.AddWithValue("@param88", DataViewGrid.Rows[i].Cells[88].Value.ToString());

                    //MessageBox.Show(command.Parameters.);
                    try
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + DB_Path + "-" + command.CommandText + ex.Message);
                        //throw;
                    }
                    finally
                    {
                        if (connection != null) connection.Close();
                    }
                    ////OleDbDataAdapter das = new OleDbDataAdapter(command.CommandText, cnn);
                    if (!chbx_AutoSave.Checked) MessageBox.Show("Song Details Changes Saved");
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
            GroupChanged = false;
            Update_Selected();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (txt_Artist.Text != "" || txt_Title.Text != "")
                try
                {
                    SearchCmd = "SELECT * FROM Main u WHERE " + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ;";
                    //DataGridView1.Dispose();
                    //txt_Description.Text = SearchCmd;
                    dssx.Dispose();
                    Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
                    DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                    DataViewGrid.Refresh();
                }
                catch (System.IO.FileNotFoundException ee)
                {
                    MessageBox.Show(ee.Message + "Can't run Search ! " + SearchCmd);
                    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else MessageBox.Show("Add a search criteria");
            //SearchON = true;
            //btn_Search.Enabled = false;
            btn_SearchReset.Text = "Exit Search";
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
                Update_Selected();
                //MessageBox.Show("pop" + noOfRec.ToString() + S, Width = 50 earchCmd);
                //da = new OleDbDataAdapter("SELECT Identifier,ContactPosition FROM PositionType;", cn);
                //da.Fill(ds, "PositionType");
                //da = new OleDbDataAdapter("SELECT Identifier, Badge FROM Badge", cn);
                //da.Fill(ds, "Badge");
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
        void Update_Selected()
        {

            DataSet dsz1 = new DataSet();
            DataSet dsz2 = new DataSet();

            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                OleDbDataAdapter dsa = new OleDbDataAdapter(SearchCmd, cn);
                //MessageBox.Show("pop" + noOfRec.ToString() + SearchCmd);

                try
                {
                    dsa.Fill(dsz1, "Main");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("-DB Open in Design Mode or Download Connectivity patch @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                    frm1.ShowDialog();
                    return;
                }
                dsa.Dispose();
                noOfRec = dsz1.Tables[0].Rows.Count;

                var SearchCmd22 = SearchCmd;
                if (SearchCmd22.IndexOf("u WHERE") > 0)
                {
                    SearchCmd22 = SearchCmd22.Replace("FROM Main u WHERE", "FROM Main u WHERE (Selected=\"Yes\") AND (");
                    SearchCmd22 = SearchCmd22.Replace("ORDER BY", ") ORDER BY");
                }
                else
                    SearchCmd22 = SearchCmd22.Replace("FROM Main u", "FROM Main u WHERE Selected=\"Yes\" ");
                OleDbDataAdapter dca = new OleDbDataAdapter(SearchCmd22, cn);
                //MessageBox.Show("pop" + noOfRec.ToString() + SearchCmd);

                try
                {
                    dca.Fill(dsz2, "Main");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("-DB Open in Design Mode or Download Connectivity patch @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
                    ErrorWindow frm1 = new ErrorWindow("DB Open in Design Mode or Download Connectivity patch @ ", "https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734", "Error when opening the DB", false, false);
                    frm1.ShowDialog();
                    return;
                }
                var noOfSelRec = dsz2.Tables[0].Rows.Count;
                dca.Dispose();
                cn.Dispose();
                lbl_NoRec.Text = noOfSelRec.ToString() + "/" + noOfRec.ToString() + " records.";
            }
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
            return MaximumSize;//files[10000];
        }

        private void btn_ChangeCover_Click(object sender, EventArgs e)
        {
            var temppath = String.Empty;
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Cover PNG file";
                fbd.Filter = "PNG file (*.png)|*.png";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                temppath = fbd.FileName;

                var tmpWorkDir = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(temppath));// Create workDir folder
                if (File.Exists(temppath.Replace(".png", ".dds"))) ExternalApps.Png2Dds(temppath, Path.Combine(tmpWorkDir, temppath.Replace(".png", ".dds")), 512, 512);
                txt_AlbumArtPath.Text = temppath;
                picbx_AlbumArtPath.ImageLocation = txt_AlbumArtPath.Text.Replace(".dds", ".png");
                chbx_Cover.Checked = true;
                var i = DataViewGrid.SelectedCells[0].RowIndex;
                DataViewGrid.Rows[i].Cells["Audio_Hash"].Value = temppath.Replace(".png", ".dds");
                if (File.Exists(temppath))
                    using (FileStream fss = File.OpenRead(temppath))
                    {
                        SHA1 sha = new SHA1Managed();
                        DataViewGrid.Rows[i].Cells["Audio_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                        fss.Close();
                    }
                SaveRecord();
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Standardization frm = new Standardization(DB_Path, TempPath, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb);
            frm.Show();
        }

        private void cbx_Format_SelectedValueChanged(object sender, EventArgs e)
        {
            //btn_Conv_And_Transfer.Text = chbx_Format.Text;// SelectedValue.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            if (!SearchON && !SearchExit)
            {
                btn_ChangeCover.Enabled = false;
                btn_Save.Enabled = false;

                txt_Title.Text = "";
                txt_Artist.Text = "";
                txt_Artist_Sort.Enabled = false;
                txt_Album.Enabled = false;
                txt_Title_Sort.Enabled = false;

                btn_Search.Enabled = true;
                btn_SearchReset.Text = "Exit Search";

                SearchON = true;
                SearchExit = true;
            }
            else
                if (SearchON) //|| (SearchON && SearchExit))
            {
                ////SearchCmd = "SELECT * FROM Main WHERE " + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                SearchCmd = "SELECT * FROM Main u ORDER BY Artist, Album_Year, Album, Track_No, Song_Title;";
                dssx.Dispose();
                Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
                DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataViewGrid.Refresh();
                btn_SearchReset.Text = "Start Search";
                btn_Search.Enabled = false;

                SearchON = false;
                SearchExit = false;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveRecord();
            ConfigRepository.Instance()["dlcm_FTP"] = chbx_PreSavedFTP.Text;
            if (chbx_PreSavedFTP.Text == "EU") ConfigRepository.Instance()["dlcm_FTP1"] = txt_FTPPath.Text;
            if (chbx_PreSavedFTP.Text == "US") ConfigRepository.Instance()["dlcm_FTP2"] = txt_FTPPath.Text;
            ConfigRepository.Instance()["dlcm_RemoveBassDD"] = chbx_RemoveBassDD.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_UniqueID"] = chbx_UniqueID.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_andCopy"] = chbx_Copy.Checked ? "Yes" : "No";
            ConfigRepository.Instance()["dlcm_Autosave"] = chbx_AutoSave.Checked == true ? "Yes" : "No";
            //ConfigRepository.Instance()["dlcm_PathForBRM"] = ;
            //ConfigRepository.Instance()["dlcm_Preview Lenght"] = ;
            //ConfigRepository.Instance()["dlcm_PreviewStart"] = ;
            //ConfigRepository.Instance()[""] = ;
            this.Close();
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)
        {
            //No Guitar
            //No Preview
            //No Section
            //No Vocals
            //No Track No.
            //No Version
            //No Author
            //No Bass DD
            //No Bass
            //No DD
            //With DD
            //Alternate
            //Beta
            //Broken
            //Selected
            //With Bonus
            //Original
            //CDLC
            //Drop D
            //E Standard
            //Eb Standard
            //Other Tunings
            //Pc
            //PS3
            //Mac
            //XBOX360
            //0ALL
            //Track No. 1
            //DLCID diff than Default
            //Autom gen Preview
            //With Duplicates

            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Main u WHERE ";
            var Filtertxt = cmb_Filter.Text;//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {
                case "No Cover":
                    SearchCmd += "Has_Cover <> 'Yes'";// + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
                    break;
                case "No Preview":
                    SearchCmd += "Has_Preview <> 'Yes'";
                    break;
                case "No Vocals":
                    SearchCmd += "Has_Vocals <> 'Yes'";
                    break;
                case "No Section":
                    SearchCmd += "Has_Sections <> 'Yes'";
                    break;
                case "No Bass":
                    SearchCmd += "Has_Bass <> 'Yes'";
                    break;
                case "No Guitar":
                    SearchCmd += "Has_Guitar <> 'Yes'";
                    break;
                case "No Track No.":
                    SearchCmd += "Has_Track_No <> 'Yes' OR Track_No='-1' OR Track_No='0'";
                    break;
                case "No Version":
                    SearchCmd += "Has_Version <> 'Yes'";
                    break;
                case "No Author":
                    SearchCmd += "Has_Author <> 'Yes'";
                    break;
                case "Original":
                    SearchCmd += "Is_Original = 'Yes'";
                    break;
                case "CDLC":
                    SearchCmd += "Is_Original <> 'Yes'";
                    break;
                case "Selected":
                    SearchCmd += "Selected = 'Yes'";
                    break;
                case "Beta":
                    SearchCmd += "Is_Beta = 'Yes'";
                    break;
                case "Broken":
                    SearchCmd += "Is_Broken = 'Yes'";
                    break;
                case "Alternate":
                    SearchCmd += "Is_Alternate = 'Yes'";
                    break;
                case "With DD":
                    SearchCmd += "Has_DD = 'Yes'";
                    break;
                case "No DD":
                    SearchCmd += "Has_DD <> 'Yes'";
                    break;
                case "No Bass DD":
                    SearchCmd += "Bass_Has_DD <> 'Yes'";
                    break;
                case "E Standard":
                    SearchCmd += "Tunning = 'E Standard'";
                    break;
                case "Eb Standard":
                    SearchCmd += "Tunning = 'Eb Standard'";
                    break;
                case "Drop D":
                    SearchCmd += "Tunning = 'Drop D'";
                    break;
                case "Other Tunings":
                    SearchCmd += "Tunning not in ('E Standard','Eb Standard','Drop D')";
                    break;
                case "With Bonus":
                    SearchCmd += "Has_Bonus_Arrangement = 'Yes'";
                    break;
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
                //Track No. 1
                case "Track No. 1":
                    SearchCmd += "Track_No = '1'";
                    break;
                //DLCID diff than Default
                case "DLCID diff than Default":
                    SearchCmd += "DLC_AppID <> '" + ConfigRepository.Instance()["general_defaultappid_RS2014"] + "'";
                    break;
                //Autom gen Preview
                case "Automatically generated Preview":
                    SearchCmd += "PreviewTime = '" + ConfigRepository.Instance()["dlcm_PreviewStart"] + "' AND PreviewLenght='" + ConfigRepository.Instance()["dlcm_PreviewLenght"] + "'";
                    break;
                //Autom gen Preview
                case "Any DLCManager generated Preview":
                    SearchCmd += "PreviewTime <> '' AND PreviewLenght <> ''";
                    break;
                //With Duplicates
                case "With Duplicates":
                    SearchCmd += "Available_Duplicate = 'Yes'";
                    break;
                case "Main_NoOLD":
                    SearchCmd += "Available_Old <> 'Yes'";
                    break;
                case "Main_FilesMissingIssues":
                    var SearchCmd2 = "SELECT * FROM Main;";// "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";

                    //var DB_Path = "";
                    //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    try
                    {
                        using (OleDbConnection cnn2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            DataSet dms = new DataSet();

                            OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd2, cnn2);
                            dan.Fill(dms, "Main");
                            var noOfRec = dms.Tables[0].Rows.Count;
                            var vFilesMissingIssues = "";
                            pB_ReadDLCs.Value = 0;
                            pB_ReadDLCs.Maximum = noOfRec;// *2;
                            pB_ReadDLCs.Increment(1);
                            for (var i = 0; i < noOfRec; i++)
                            {
                                vFilesMissingIssues = "";
                                DataSet dss = new DataSet();
                                DataSet dcs = new DataSet();

                                var ID = dms.Tables[0].Rows[i].ItemArray[0].ToString();
                                var AlbumArtPath = dms.Tables[0].Rows[i].ItemArray[10].ToString();
                                var AudioPath = dms.Tables[0].Rows[i].ItemArray[11].ToString();
                                var AudioPreviewPath = dms.Tables[0].Rows[i].ItemArray[12].ToString();
                                var OggPath = dms.Tables[0].Rows[i].ItemArray[77].ToString();
                                var OggPreviewPath = dms.Tables[0].Rows[i].ItemArray[78].ToString();
                                var OrigFileName = dms.Tables[0].Rows[i].ItemArray[19].ToString();
                                var hasOld = dms.Tables[0].Rows[i].ItemArray[87].ToString();
                                var hasPrev = dms.Tables[0].Rows[i].ItemArray[43].ToString();
                                var hasCov = dms.Tables[0].Rows[i].ItemArray[42].ToString();
                                var SearchCmd9 = "SELECT * FROM Arrangements WHERE CDLC_ID=" + ID + ";";
                                OleDbDataAdapter dbn = new OleDbDataAdapter(SearchCmd9, cnn2);
                                dbn.Fill(dss, "Arrangements");
                                var noOfArr = 0;
                                noOfArr = dss.Tables[0].Rows.Count;
                                if (noOfArr == 0) vFilesMissingIssues = "No Arrangements!!!";
                                for (var k = 0; k < noOfArr; k++)//, Type
                                {
                                    try
                                    {
                                        var ms1 = dss.Tables[0].Rows[k].ItemArray[4].ToString();//.ItemArray[4].ToString();
                                        var ms2 = dss.Tables[0].Rows[k].ItemArray[5].ToString();//.ItemArray[4].ToString();
                                        var ms3 = dss.Tables[0].Rows[k].ItemArray[26].ToString();
                                        if (!File.Exists(ms1) && (ms3.LastIndexOf("showlights") < 1)) vFilesMissingIssues += " SNG " + ms3 + "; "; //showlights
                                        if (!File.Exists(ms2)) vFilesMissingIssues += " XML " + ms3 + "; ";
                                        //var tones = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done                                        

                                    }
                                    catch (Exception ee)
                                    {
                                        // To inform the user and continue is 
                                        // sufficient for this demonstration. 
                                        // Your application may require different behavior.
                                        Console.WriteLine(ee.Message);
                                        //continue;
                                    }
                                }
                                pB_ReadDLCs.Increment(1);
                                var old = TempPath + "\\0_old\\" + OrigFileName;
                                //var duplicate = dms.Tables[0].Rows[i].ItemArray[78].ToString();//not done
                                if (!File.Exists(AlbumArtPath) && hasCov == "Yes") vFilesMissingIssues += " AlbumArtPath; ";
                                if (!File.Exists(AudioPath)) vFilesMissingIssues += " AudioPath; ";
                                if (!File.Exists(AudioPreviewPath) && hasPrev == "Yes") vFilesMissingIssues += " AudioPreviewPath; ";
                                if (!File.Exists(OggPath)) vFilesMissingIssues += " OggPath; ";
                                if (!File.Exists(OggPreviewPath)) vFilesMissingIssues += " OggPreviewPath; ";
                                if (!File.Exists(old) && hasOld == "Yes") vFilesMissingIssues += " old; ";
                                //if (!File.Exists(OggPath)) vFilesMissingIssues += "OggPath";
                                var sel2 = "Update Main Set FilesMissingIssues = '" + vFilesMissingIssues + "' WHERE ID=" + ID + ";";
                                //rtxt_StatisticsOnReadDLCs.Text = max.ToString()+"-"+sel2 + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                                DataSet dxr = new DataSet();
                                OleDbDataAdapter dax = new OleDbDataAdapter(sel2, cnn2);
                                if (vFilesMissingIssues != "")
                                    dax.Fill(dxr, "Main");
                                dax.Dispose();
                            }
                            //dbn.Dispose();
                            //dan.Dispose();
                        }
                    }
                    catch (Exception ee)
                    {
                        // To inform the user and continue is 
                        // sufficient for this demonstration. 
                        // Your application may require different behavior.
                        Console.WriteLine(ee.Message);
                    }
                    SearchCmd += "FilesMissingIssues <> ''";
                    break;
                case "Main_NoPreviewFile":
                    SearchCmd += "audioPreviewPath = ''";
                    break;
                case "Imported Last":
                    var SearchCmd3 = "SELECT top 1 Pack FROM Main order by ID DESC;";// ORDER BY Pack,Import_Date DESC "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";
                    DataSet dds = new DataSet();
                    //var DB_Path = "";
                    //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    try
                    {
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd3, cnn);
                            dan.Fill(dds, "Main");
                            var noOfRec = dds.Tables[0].Rows.Count;
                            if (noOfRec > 0)
                                SearchCmd += "Pack='" + dds.Tables[0].Rows[0].ItemArray[0].ToString() + "'";//Import_Date > .Replace(" AM", "").Replace(" PM", "")
                            else SearchCmd += "1 = 2";
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
                    break;
                case "Imported Current Month":
                    var SearchCmd4 = "SELECT Import_Date FROM Main ORDER BY Import_Date DESC;";// "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";
                    DataSet djs = new DataSet();
                    //var DB_Path = "";
                    //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    try
                    {
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd4, cnn);
                            dan.Fill(djs, "Main");
                            if (noOfRec > 0)
                                SearchCmd += "Import_Date > '" + djs.Tables[0].Rows[0].ItemArray[0].ToString().Substring(0, 10) + "'";
                            else SearchCmd += "1 = 2";
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
                    break;
                case "Packed Last":
                    var SearchCmd6 = "SELECT top 1 Pack FROM LogPacking order by ID DESC;";// ORDER BY Pack,Import_Date DESC "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";
                    DataSet dzs = new DataSet();
                    //var DB_Path = "";
                    //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    try
                    {
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd6, cnn);
                            dan.Fill(dzs, "Main");
                            var noOfRec = dzs.Tables[0].Rows.Count;
                            if (noOfRec > 0)
                                SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPacking WHERE Pack='" + dzs.Tables[0].Rows[0].ItemArray[0].ToString() + "')";//Import_Date > .Replace(" AM", "").Replace(" PM", "")
                            else SearchCmd += "1 = 2";
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
                    break;
                case "Packing Errors":
                    var SearchCmd7 = "SELECT top 1 Pack FROM LogPackingError order by ID DESC;";// ORDER BY Pack,Import_Date DESC "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";
                    DataSet dks = new DataSet();
                    //var DB_Path = "";
                    //DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        try
                        {
                            OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd7, cnn);
                            dan.Fill(dks, "Main");
                            var noOfRec = dks.Tables[0].Rows.Count;
                            if (noOfRec > 0)
                                SearchCmd += "CSTR(ID) in (SELECT CDLC_ID FROM LogPackingError WHERE Pack='" + dks.Tables[0].Rows[0].ItemArray[0].ToString() + "')";
                            else SearchCmd += "1 = 2";
                        }
                        catch (System.IO.FileNotFoundException ee)
                        {
                            // To inform the user and continue is 
                            // sufficient for this demonstration. 
                            // Your application may require different behavior.
                            Console.WriteLine(ee.Message);
                            //continue;
                        }
                    }
                    break;
                case "Same DLCName":
                    var SearchCmd5 = "SELECT n.ID as IDs FROM Main AS m LEFT JOIN Main AS n ON (m.ID <> n.ID) AND (n.DLC_Name = m.DLC_Name)";
                    //SELECT ID FROM Main WHERE COUNT(DLC_Name)=2";// "SELECT MAX(ID),Import_Date FROM Main;";// WHERE Import_Date=''";
                    //DataSet dbs = new DataSet();
                    ////var DB_Path = "";
                    ////DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb;";
                    //try
                    //{
                    //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    //    {
                    //        OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd5, cnn);
                    //        dan.Fill(dbs, "Main");
                    SearchCmd += "ID IN (" + SearchCmd5 + ")";
                    //    }
                    //}
                    //catch (System.IO.FileNotFoundException ee)
                    //{
                    //    // To inform the user and continue is 
                    //    // sufficient for this demonstration. 
                    //    // Your application may require different behavior.
                    //    Console.WriteLine(ee.Message);
                    //    //continue;
                    //}
                    break;
                default:
                    break;
            }

            if (Filtertxt.Length > 5) Filtertxt = cmb_Filter.Text.Substring(0, 5);//cmb_Filter.SelectedValue.ToString();

            switch (Filtertxt)
            {
                case "Group":
                    var SearchCmd6 = "SELECT m.CDLC_ID FROM Groups AS m WHERE m.CDLC_ID = CSTR(u.ID) AND m.Groups =  '" + cmb_Filter.Text.Substring(6, cmb_Filter.Text.Length - 6) + "'";
                    SearchCmd += "CSTR(u.ID) IN (" + SearchCmd6 + ")";
                    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY Artist, Album_Year, Album, Track_No, Song_Title ";
            //MessageBox.Show(Filtertxt + SearchCmd);
            try
            {
                this.DataViewGrid.DataSource = null; //Then clear the rows:

                this.DataViewGrid.Rows.Clear();//                Then set the data source to the new list:

                //this.dataGridView.DataSource = this.GetNewValues();
                dssx.Dispose();
                Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
                DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataViewGrid.Refresh();
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "Can't run Filter ! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dDataGridView1_CellContentClick_1(object sender, KeyEventArgs eee)
        {
            DataGridView1_CellContentClick_1(sender, eee);
        }

        private void DataGridView1_CellContentClick_1(object sender, KeyEventArgs eee)
        {
            throw new NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cache frm = new Cache(DB_Path, TempPath, RocksmithDLCPath, AllowEncriptb, AllowORIGDeleteb);
            frm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = txt_OggPath.Text;
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                }
        }

        private void btm_PlayPreview_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;
            var t = txt_OggPreviewPath.Text;
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
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
            }
        }

        private void btn_Conv_And_Transfer_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 7;
            pB_ReadDLCs.Increment(1);
            //var bassRemoved = "";
            if (chbx_AutoSave.Checked) SaveOK = true;
            else SaveOK = false;
            pB_ReadDLCs.Increment(1);

            var Temp_Path_Import = TempPath;
            var old_Path_Import = TempPath + "\\0_old";
            var broken_Path_Import = TempPath + "\\0_broken";
            var dupli_Path_Import = TempPath + "\\0_duplicate";
            var dlcpacks = TempPath + "\\0_dlcpacks";
            var repacked_Path = TempPath + "\\0_repacked";
            var repacked_XBOXPath = TempPath + "\\0_repacked\\XBOX360";
            var repacked_PCPath = TempPath + "\\0_repacked\\PC";
            var repacked_MACPath = TempPath + "\\0_repacked\\MAC";
            var repacked_PSPath = TempPath + "\\0_repacked\\PS3";
            var log_Path = ConfigRepository.Instance()["dlcm_LogPath"];
            string pathDLC = RocksmithDLCPath;
            DLCManager.CreateTempFolderStructure(Temp_Path_Import, old_Path_Import, broken_Path_Import, dupli_Path_Import, dlcpacks, pathDLC, repacked_Path, repacked_XBOXPath, repacked_PCPath, repacked_MACPath, repacked_PSPath, log_Path);



            var i = DataViewGrid.SelectedCells[0].RowIndex;
            var bassRemoved = "No";
            if (chbx_RemoveBassDD.Checked && chbx_BassDD.Checked && (!(chbx_KeepBassDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") && !(chbx_KeepDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes")))
            {
                var xmlFiles = Directory.GetFiles(DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString(), "*.xml", SearchOption.AllDirectories);
                Platform platform = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString().GetPlatform();

                foreach (var xml in xmlFiles)
                {
                    Song2014 xmlContent = null;
                    try
                    {
                        xmlContent = Song2014.LoadFromFile(xml);
                        if (xmlContent.Arrangement.ToLower() == "bass" && !(xml.IndexOf(".old") > 0))
                        {
                            bassRemoved = (DLCManager.RemoveDD(DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString(), chbx_Original.Text, xml, platform, false, false) == "Yes") ? "Yes" : "No";
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                }
            }

            string h = GeneratePackage(txt_ID.Text, bassRemoved == "No" ? false : true);
            string copyftp = "";
            pB_ReadDLCs.Increment(1);
            if (chbx_Format.Text == "PS3" && chbx_Copy.Checked)
            {
                //var GameID = txt_FTPPath.Text.Substring(txt_FTPPath.Text.LastIndexOf("BL"), 9);
                //var startno = txt_FTPPath.Text.LastIndexOf("GAMES/");
                //var endno = (txt_FTPPath.Text.LastIndexOf("BL")) + 9;
                //var GameName = ((txt_FTPPath.Text).Substring(startno, endno - startno)).Replace("GAMES/", "");
                //var newpath = txt_FTPPath.Text.Replace("GAMES", "game").Replace("PS3_GAME", GameID).Replace(GameName + "/", "");
                var a = FTPFile(txt_FTPPath.Text, h + "_ps3.psarc.edat", TempPath, SearchCmd);
                copyftp = " and " + a + " FTPed";
            }
            else if ((chbx_Format.Text == "PC" || chbx_Format.Text == "Mac") && chbx_Copy.Checked)
            {
                var platfrm = (chbx_Format.Text == "PC" ? "_p" : (chbx_Format.Text == "Mac" ? "_m" : ""));
                var dest = "";
                //if (RocksmithDLCPath.ToLower().IndexOf(("Rocksmith2014\\DLC").ToLower()) > 0)
                //{
                var source = h + platfrm + ".psarc";
                dest = RocksmithDLCPath + source.Substring(source.LastIndexOf("\\"));
                //File.Copy(RocksmithDLCPath + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", false);
                try
                {
                    File.Copy(source, dest, true);
                }
                catch (Exception ee)
                {
                    copyftp = "Not";
                }
                copyftp = "and " + copyftp + " Copied";
            }

            if (chbx_RemoveBassDD.Checked && chbx_BassDD.Checked && !(chbx_KeepBassDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul52"] == "Yes") || !(chbx_KeepDD.Checked && ConfigRepository.Instance()["dlcm_AdditionalManipul53"] == "Yes"))
            {
                var xmlFilez = Directory.GetFiles(files[0].Folder_Name, "*.old", SearchOption.AllDirectories);

                foreach (var xml in xmlFilez)
                {
                    Song2014 xmlContent = null;
                    try
                    {
                        xmlContent = Song2014.LoadFromFile(xml);
                        if (xmlContent.Arrangement.ToLower() == "bass" && xml.IndexOf(".old") > 0)
                        {
                            File.Copy(xml, xml.Replace(".old", ""), true);
                            File.Delete(xml);
                        }
                    }
                    catch (Exception ee)
                    {
                    }
                }
            }

            pB_ReadDLCs.Increment(1);
            MessageBox.Show("Song Packed" + copyftp);

        }

        public static string FTPFile(string filel, string filen, string TempPat, string SearchCm)
        {
            // Get the object used to communicate with the server.
            var ddd = filel + filen.Replace(TempPat + "\\0_repacked\\PS3\\", "");
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ddd);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("anonymous", "bogdan@capi.ro");



                byte[] b = File.ReadAllBytes(filen);//TempPath + "\\0_dlcpacks\\manipulated\\" +

                request.ContentLength = b.Length;
                try
                {
                    using (Stream s = request.GetRequestStream())
                    {
                        s.Write(b, 0, b.Length);
                    }
                    FtpWebResponse ftpResp = (FtpWebResponse)request.GetResponse();
                    return "Truly ";
                }
                catch (Exception ee)
                {
                    return "Not ";
                    //MessageBox.Show(ee.Message + "PS3 is down :(! " + SearchCm);
                    //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                return "Not ";
                //MessageBox.Show(ee.Message + "PS3 is down :(! " + SearchCm);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        //for conversion the wwise needs to be downlaoded from https://www.audiokinetic.com/download/?id=2014.1.6_5318 or https://www.audiokinetic.com/download/?id=2013.2.10_4884
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");// Path.GetDirectoryName();
            var t = txt_OggPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var tt = t.Replace(".ogg", "_preview.ogg");
            string[] timepieces = txt_PreviewStart.Text.ToString().Split(':');
            TimeSpan r = new TimeSpan(0, timepieces[0].ToInt32(), timepieces[1].ToInt32());
            startInfo.Arguments = String.Format(" -i \"{0}\" -o \"{1}\" -s \"{2}\" -e \"{3}\"",
                                                t,
                                                tt,
                                                r.TotalMilliseconds,
                                                (r.TotalMilliseconds + (txt_PreviewEnd.Text.ToInt32() * 1000)));
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    var dfssdf = ConfigRepository.Instance()["general_wwisepath"] + "\\Authoring\\Win32\\Release\\bin\\Wwise.exe";
                    if (DDC.ExitCode == 0)
                    {
                        if (!File.Exists(dfssdf))
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Wwise v2014.1.6 build 5318: " + Environment.NewLine + "A restart is required for the Conversion to WEM process to be succesful,l else the errors can be captured through the Missing Files Query" + Environment.NewLine, "https://www.audiokinetic.com/download/", "Error when Generating a Preview", false, false);
                            frm1.ShowDialog();

                        }
                        Converters(tt, ConverterTypes.Ogg2Wem, true);
                        txt_OggPreviewPath.Text = tt;
                        chbx_Preview.Checked = true;
                        var i = DataViewGrid.SelectedCells[0].RowIndex;
                        DataViewGrid.Rows[i].Cells["Has_Preview"].Value = "Yes";
                        DataViewGrid.Rows[i].Cells["audioPreviewPath"].Value = tt.Replace(".ogg", ".wem");
                        DataViewGrid.Rows[i].Cells["PreviewTime"].Value = txt_PreviewStart.Text;
                        DataViewGrid.Rows[i].Cells["PreviewLenght"].Value = txt_PreviewEnd.Text;
                        btn_PlayPreview.Enabled = true;

                        if (File.Exists(tt))
                            using (FileStream fss = File.OpenRead(tt))
                            {
                                SHA1 sha = new SHA1Managed();
                                DataViewGrid.Rows[i].Cells["audioPreview_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                                fss.Close();
                            }
                        AddPreview = true;
                        SaveRecord();
                    }

                }

        }

        public enum ConverterTypes
        {
            HeaderFix,
            Revorb,
            WEM,
            Ogg2Wem
        }
        public static void Converters(string file, ConverterTypes converterType, bool mssON)
        {

            var txtOgg2FixHdr = String.Empty;
            var txtWwiseConvert = String.Empty;
            var txtWwise2Ogg = String.Empty;
            var txtAudio2Wem = String.Empty;

            //using (var fd = new OpenFileDialog())
            //{
            //    fd.Multiselect = true;
            //    fd.Filter = "Wwise 2010.3.3 OGG files (*.ogg)|*.ogg";
            //    if (converterType == ConverterType.Revorb || converterType == ConverterType.WEM)
            //        fd.Filter += "|Wwise 2013 WEM files (*.wem)|*.wem";
            //    else if (converterType == ConverterType.Ogg2Wem)
            //        fd.Filter = "Vobis Ogg or Wave files (*.ogg, *.wav)|*.ogg; *.wav";

            //    fd.ShowDialog();
            //    if (!fd.FileNames.Any())
            //        return;

            //InputAudioFiles = fd.FileNames;
            Dictionary<string, string> errorFiles = new Dictionary<string, string>();
            List<string> successFiles = new List<string>();

            try
            {
                var extension = Path.GetExtension(file);
                var outputFileName = Path.Combine(Path.GetDirectoryName(file), String.Format("{0}_fixed{1}", Path.GetFileNameWithoutExtension(file), ".ogg"));
                switch (converterType)
                {
                    case ConverterTypes.Ogg2Wem:
                        //txtAudio2Wem.Text = file;
                        OggFile.Convert2Wem(file, 4, 4 * 1000);
                        //Delete any preview_preview file created..by....?ccc
                        foreach (string prev_prev in Directory.GetFiles(Path.GetDirectoryName(file), "*preview_preview*", System.IO.SearchOption.AllDirectories))
                        {
                            File.Delete(prev_prev);
                        }
                        break;
                }

                successFiles.Add(file);
            }
            catch (Exception ex)
            {
                errorFiles.Add(file, ex.Message);
            }


            if (errorFiles.Count <= 0 && successFiles.Count > 0)
                if (mssON) MessageBox.Show("Conversion complete!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (errorFiles.Count > 0 && successFiles.Count > 0)
                {
                    StringBuilder alertMessage = new StringBuilder(
                        "Conversion complete with errors." + Environment.NewLine + Environment.NewLine);
                    alertMessage.AppendLine(
                        "Files converted with success:" + Environment.NewLine);

                    foreach (var sFile in successFiles)
                        alertMessage.AppendLine(String.Format("File: {0}", sFile));
                    alertMessage.AppendLine("Files converted with error:" + Environment.NewLine);
                    foreach (var eFile in errorFiles)
                        alertMessage.AppendLine(String.Format("File: {0}; error: {1}", eFile.Key, eFile.Value));

                    if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    StringBuilder alertMessage = new StringBuilder(
                        "Conversion complete with errors." + Environment.NewLine);
                    alertMessage.AppendLine(
                        "Files converted with error: " + Environment.NewLine);
                    foreach (var eFile in errorFiles)
                        alertMessage.AppendLine(String.Format("File: {0}, error: {1}", eFile.Key, eFile.Value));

                    if (mssON) MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void chbx_FTP1_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbx_FTP1.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 ALL DLC - BLUS31182/PS3_GAME/USRDIR/";
        }

        private void chbx_FTP2_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbx_FTP2.Checked) txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/GAMES/Rocksmith 2014 FAV - BLES01862/PS3_GAME/USRDIR/";
        }

        private void txt_PreviewStart_Leave(object sender, EventArgs e)
        {
            if (txt_PreviewEnd.Text != null)
            {
                //string[] r = Convert.ToDateTime("00:" + txt_PreviewStart.Text).AddSeconds(30).ToString().Split(' ');
                //txt_PreviewEnd.Text = r[1].Substring(3,5);
                txt_PreviewEnd.Text = "30";
            }
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (chbx_AutoSave.Checked && SaveOK) SaveRecord();
        }

        private void txt_Author_Leave(object sender, EventArgs e)
        {
            if (txt_Author.Text != null) chbx_Author.Checked = true;
        }

        private void btn_SelectPreview_Click(object sender, EventArgs e)
        {
            var temppath = String.Empty;
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Preview OGG file";
                fbd.Filter = "OGG file (*.ogg)|*.ogg";
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                temppath = fbd.FileName;
                txt_OggPreviewPath.Text = temppath;
                chbx_Preview.Checked = true;
                var i = DataViewGrid.SelectedCells[0].RowIndex;
                DataViewGrid.Rows[i].Cells["oggPreviewPath"].Value = temppath;
                Converters(temppath, ConverterTypes.Ogg2Wem, true);
                DataViewGrid.Rows[i].Cells["audioPreviewPath"].Value = temppath.Replace(".ogg", ".wem");
                if (File.Exists(temppath.Replace(".ogg", ".wem")))
                    using (FileStream fss = File.OpenRead(temppath.Replace(".ogg", ".wem")))
                    {
                        SHA1 sha = new SHA1Managed();
                        DataViewGrid.Rows[i].Cells["audioPreview_Hash"].Value = BitConverter.ToString(sha.ComputeHash(fss));
                        fss.Close();
                    }
                SaveRecord();
            }
        }

        private void txt_Author_TextChanged(object sender, EventArgs e)
        {
            if (txt_Author.Text != null) chbx_Author.Checked = true;
        }

        private void ChangeRowD(object sender, EventArgs e)
        {

        }

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (txt_Album.Text != "") ChangeRow();
        }

        private void chbx_Combo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbx_Rhythm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_PreviewStart_J_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Prev_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataViewGrid.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (prev == 0) return;

            SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataViewGrid.Rows[rowindex - 1].Selected = true;
            DataViewGrid.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataViewGrid.Rows[rowindex - 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //txt_Counter.Text = prev.ToString();
        }

        private void btn_NextItem_Click(object sender, EventArgs e)
        {
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            var prev = DataViewGrid.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
            if (DataViewGrid.Rows.Count == prev + 2) return;

            SaveRecord();// ChangeRow();

            int rowindex;
            DataGridViewRow row;
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            rowindex = i;// DataGridView1.SelectedRows[0].Index;
            DataViewGrid.Rows[rowindex + 1].Selected = true;
            DataViewGrid.Rows[rowindex].Selected = false;
            //if (prev>txt_Counter.Text.ToInt32())
            row = DataViewGrid.Rows[rowindex + 1];
            //else row = DataGridView1.Rows[rowindex - 1];
            //if (DataGridView1.SelectedCells[0].RowIndex > 0) 
            //txt_Counter.Text = prev.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //var SearchCmd2="UPDATE Main SET Selected = 'Yes' WHERE ID IN ("+SearchCmd.Replace("*","ID").Replace(";","")+")";
            //DataSet dxr = new DataSet();
            ////OleDbDataAdapter dax = new OleDbDataAdapter(sel2, cnn);
            //try
            //{
            //    using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //    {
            //        OleDbDataAdapter dan = new OleDbDataAdapter(SearchCmd2, cnn);
            //        dan.Fill(dxr, "Main");
            //        var noOfRec = dxr.Tables[0].Rows.Count;
            //    }
            //}

            if  (chbx_Group.Text == "" && chbx_InclGroups.Checked) 
                    {
                MessageBox.Show("Select a Group from the DROPDOWN to Mass-apply.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "Yes");

            var test = "";

            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "Yes");
                test = " or Beta";
            }

            //command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
            if (SearchCmd.IndexOf("ID, Artist") > 0) command.CommandText += " WHERE ID IN ( SELECT ID FROM Main ORDER BY Artist, Album_Year, Album, Track_No, Song_Title)";
            else
                command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";

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


            if (chbx_InclGroups.Checked)
            {
                var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\" LEFT JOIN Main on Main.ID=Groups.CDLC_ID and Main.Selected='Yes'";
                try
                {
                    using (OleDbConnection cns = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        DataSet dhs = new DataSet();

                        OleDbDataAdapter dhx = new OleDbDataAdapter(cmd, cns);
                        dhx.Fill(dhs, "Groups");
                        dhx.Dispose();
                    }
                    cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"DLC\");";
                    DataSet dsz = new DataSet();
                    using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    {
                        OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                        dab.Fill(dsz, "Groups");
                        dab.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("Can not Delete Song folder ! ");
                }
            }

            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();
            try
            {
                var com = "Select * FROM Main";

                if (SearchCmd.IndexOf("ID, Artist") > 0) com += " WHERE ID IN ( SELECT ID FROM Main ORDER BY Artist, Album_Year, Album, Track_No, Song_Title)";
                else
                    com += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
                DataSet dhs = new DataSet();
                using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected" + test);
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
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "No");
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "No");
                test = " or Beta";
            }
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
            //}
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();
            MessageBox.Show("All songs in DB have been UNmarked from being Selected" + test);
        }

        private void cbx_Format_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void txt_Group_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_AverageTempo_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void chbx_Avail_Old_CheckedChanged(object sender, EventArgs e)
        {

        }

        //public void GeneratePackage(object sender, DoWorkEventArgs e)
        public string GeneratePackage(string ID, bool bassRemoved)
        {
            string dlcSavePath = "";
            var cmd = "SELECT * FROM Main ";
            //if (rbtn_Population_Selected.Checked == true) 
            cmd += "WHERE ID = " + ID + "";
            //else if (rbtn_Population_All.Checked) ;
            //else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            //cmd += " ORDER BY Artist";
            //Read from DB
            var norows = 0;
            norows = SQLAccess(cmd);
            //bcapirtxt_StatisticsOnReadDLCs.Text = "Processing &Repackaging for " + norows + " " + cmd + "\n \n" + rtxt_StatisticsOnReadDLCs.Text;

            var i = 0;
            //var artist = "";
            //var cmd = "";
            //rtxt_StatisticsOnReadDLCs.Text = "Repack backgroundworker.."+ norows +  rtxt_StatisticsOnReadDLCs.Text;
            foreach (var file in files)
            {
                if (i > 0) //ONLY 1  FILE WILL BE READ
                    break;
                i++;
                //bcapirtxt_StatisticsOnReadDLCs.Text = "...Pack" + i + " " + file.Artist + " " + file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;// UNPACK
                //if (file.Is_Broken != "Yes" || (file.Is_Broken == "Yes" && !chbx_Broken.Checked)) //"8. Don't repack Broken songs")
                //{
                //var unpackedDir = Packer.Unpack(FullPath, Temp_Path_Import, true, true, false);
                //MessageBox.Show(file.Artist+file.Song_Title);
                var packagePlatform = file.Folder_Name.GetPlatform();
                // REORGANIZE
                //rtxt_StatisticsOnReadDLCs.Text = "...0.1.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var structured = ConfigRepository.Instance().GetBoolean("creator_structured");
                //if (structured)
                //file.Folder_Name = DLCPackageData.DoLikeProject(file.Folder_Name);
                // LOAD DATA
                //rtxt_StatisticsOnReadDLCs.Text = "...0.5.." + file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var info = DLCPackageData.LoadFromFolder(file.Folder_Name, packagePlatform);
                //rtxt_StatisticsOnReadDLCs.Text = "...1.."+ file.Folder_Name + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //var bassRemoved = "No";
                //var DDAdded = "No";

                var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", SearchOption.AllDirectories);
                var platform = file.Folder_Name.GetPlatform();
                pB_ReadDLCs.Increment(1);
                //if (chbx_Additional_Manipualtions.GetItemChecked(3) || chbx_Additional_Manipualtions.GetItemChecked(5) || chbx_Additional_Manipualtions.GetItemChecked(12) || chbx_Additional_Manipualtions.GetItemChecked(26))
                //{
                //    foreach (var xml in xmlFiles)
                //    {
                //        if (chbx_Additional_Manipualtions.GetItemChecked(12) || chbx_Additional_Manipualtions.GetItemChecked(26))
                //            //ADD DD
                //            if (
                //                (//chbx_Additional_Manipualtions.GetItemChecked(12)
                //        false && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                //                && ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm")) && file.Has_DD == "No") || (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && file.Has_BassDD == "No")
                //                )
                //                || //chbx_Additional_Manipualtions.GetItemChecked(26)
                //        (false && (Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm"))
                //                && file.Has_DD == "No" && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old")
                //                )
                //               )
                //            {
                //                File.Copy(xml, xml + ".old", true);
                //                string json = "";
                //                if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized
                //                    json = xml.Replace("EOF", "Toolkit").Replace(".xml", ".json");
                //                else
                //                    json = (xml.Replace(".xml", ".json").Replace("\\songs\\arr\\", calc_path(Directory.GetFiles(file.Folder_Name, "*.json", SearchOption.AllDirectories)[0])));

                //                File.Copy(json, json + ".old", true);
                //                //bcapirtxt_StatisticsOnReadDLCs.Text = chbx_Additional_Manipualtions.GetItemChecked(12).ToString()+ chbx_Additional_Manipualtions.GetItemChecked(26).ToString()+"...." + Path.GetFileNameWithoutExtension(xml) + "...Adding DD using DDC tool" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //                var startInfo = new ProcessStartInfo();

                //                var r = String.Format(" -m \"{0}\"", Path.GetFullPath("ddc\\ddc_5_max_levels.xml"));
                //                var c = String.Format(" -c \"{0}\"", Path.GetFullPath("ddc\\ddc_keep_all_levels.xml"));
                //                startInfo.FileName = Path.Combine(AppWD, "ddc", "ddc.exe");

                //                if (chbx_Additional_Manipualtions.GetItemChecked(36)) //37. Keep the Uncompressed Songs superorganized
                //                    startInfo.WorkingDirectory = file.Folder_Name + "\\EOF\\";// Path.GetDirectoryName();
                //                else
                //                    startInfo.WorkingDirectory = file.Folder_Name + "\\songs\\arr\\";// Path.GetDirectoryName();

                //                startInfo.Arguments = String.Format("\"{0}\" -l {1} -s {2}{3}{4}{5}",
                //                                                    Path.GetFileName(xml),
                //                                                    2, "N", r, c,
                //                                                       " -p Y", " -t N");
                //    //rtxt_StatisticsOnReadDLCs.Text = "working dir: "+ startInfo.WorkingDirectory + "...\n--"+startInfo.FileName+"..." +startInfo.Arguments + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    startInfo.UseShellExecute = false;
                //    startInfo.CreateNoWindow = true;
                //    startInfo.RedirectStandardOutput = true;
                //    startInfo.RedirectStandardError = true;

                //    using (var DDC = new Process())
                //    {
                //        // rtxt_StatisticsOnReadDLCs.Text = "...1" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //        DDC.StartInfo = startInfo;
                //        DDC.Start();
                //        //consoleOutput = DDC.StandardOutput.ReadToEnd();
                //        //consoleOutput += DDC.StandardError.ReadToEnd();
                //        DDC.WaitForExit(1000 * 60 * 5); //wait 15 minutes
                //        // if (DDC.ExitCode > 0 ) rtxt_StatisticsOnReadDLCs.Text = "Issues when adding DD !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        DDAdded = "Yes"; rtxt_StatisticsOnReadDLCs.Text = "DDAdded: " + DDAdded + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        file.Has_BassDD = "Yes";
                //    }
                //}

                //REMOVE DD
                //rtxt_StatisticsOnReadDLCs.Text = "...=.." + xml + "\n\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        if ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass") && file.Has_BassDD == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(5))
                //            || ((Path.GetFileNameWithoutExtension(xml).ToLower().Contains("lead") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("combo") || Path.GetFileNameWithoutExtension(xml).ToLower().Contains("rthythm"))
                //            && file.Has_Guitar == "Yes" && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains(".old") && chbx_Additional_Manipualtions.GetItemChecked(3)))
                //        // continue;
                //        {
                //            if (chbx_Additional_Manipualtions.GetItemChecked(5) && !chbx_Additional_Manipualtions.GetItemChecked(3) && !Path.GetFileNameWithoutExtension(xml).ToLower().Contains("bass")) continue;
                //            bassRemoved = (RemoveDD(file.Folder_Name, file.Is_Original, xml, platform) == "Yes") ? "No" : "Yes";
                //            file.Has_BassDD = (bassRemoved == "Yes") ? "No" : "Yes";

                //        }
                //        //rtxt_StatisticsOnReadDLCs.Text = "...°.." + xmlFiles.Length + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    }
                //}
                ////rtxt_StatisticsOnReadDLCs.Text = "ooooo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (chbx_Additional_Manipualtions.GetItemChecked(17)) //18.Repack with Artist/ Title same as Artist / Title Sort
                //{
                //    file.Artist_Sort = file.Artist;
                //    file.Song_Title_Sort = file.Song_Title;
                //}
                ////rtxt_StatisticsOnReadDLCs.Text = "ggggoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (chbx_Additional_Manipualtions.GetItemChecked(23) && file.Artist_Sort.Length > 4) //24.Pack with The/ Die only at the end of Title Sort 
                //{
                //    //rtxt_StatisticsOnReadDLCs.Text = "1eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    if (chbx_Additional_Manipualtions.GetItemChecked(21) && file.Song_Title_Sort.Length > 4)
                //    {
                //        //rtxt_StatisticsOnReadDLCs.Text = "2eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "The " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "Die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "the " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "die " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "THE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",The" : file.Song_Title_Sort);
                //        file.Song_Title_Sort = (file.Song_Title_Sort.Substring(0, 4) == "DIE " ? file.Song_Title_Sort.Substring(4, file.Song_Title_Sort.Length - 4) + ",Die" : file.Song_Title_Sort);
                //    }
                //    //rtxt_StatisticsOnReadDLCs.Text = file.Artist_Sort + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "The " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "Die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "the " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "die " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "THE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",The" : file.Artist_Sort);
                //    file.Artist_Sort = (file.Artist_Sort.Substring(0, 4) == "DIE " ? file.Artist_Sort.Substring(4, file.Artist_Sort.Length - 4) + ",Die" : file.Artist_Sort);
                //}
                //rtxt_StatisticsOnReadDLCs.Text = "4eeeeeeoo" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //data = new DLCPackageData
                //{
                //    GameVersion = RocksmithToolkitLib.GameVersion.RS2014,
                //    Pc = cbx_Format.Text == "PC" ? true : false,
                //    Mac = cbx_Format.Text == "Mac" ? true : false,
                //    XBox360 = cbx_Format.Text == "XBOX360" ? true : false,
                //    PS3 = cbx_Format.Text == "PS3" ? true : false,
                //    Name = txt_DLC_ID.Text,
                //    AppId = txt_APP_ID.Text,

                //    //USEFUL CMDs String.IsNullOrEmpty(
                //    SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
                //    {
                //        SongDisplayName = txt_Title.Text,
                //        SongDisplayNameSort = txt_Title_Sort.Text,
                //        Album = txt_Album.Text,
                //        SongYear = txt_Album_Year.Text.ToInt32(),
                //        Artist = txt_Artist.Text,
                //        ArtistSort = txt_Artist_Sort.Text,
                //        AverageTempo = txt_AverageTempo.Text.ToInt32()
                //    },

                //    AlbumArtPath = txt_AlbumArtPath.Text,
                //    OggPath = txt_AudioPath.Text,
                //    OggPreviewPath = ((txt_AudioPath.Text != "") ? txt_AudioPath.Text : txt_AudioPath.Text),
                //    Arrangements = info.Arrangements, //Not yet done
                //    Tones = info.Tones,//Not yet done
                //    TonesRS2014 = info.TonesRS2014,//Not yet done
                //    Volume = txt_Volume.Text.ToInt32(),
                //    PreviewVolume = txt_Preview_Volume.Text.ToInt32(),
                //    SignatureType = txt_SignatureType.Text,
                //    PackageVersion = txt_Version.Text
                //};

                data = new DLCPackageData
                {
                    GameVersion = GameVersion.RS2014,
                    Pc = chbx_Format.Text == "PC" || file.Platform == "Pc" ? true : false, //txt_Platform.Text 
                    Mac = chbx_Format.Text == "Mac" || file.Platform == "Mac" ? true : false,
                    XBox360 = chbx_Format.Text == "XBOX360" || file.Platform == "Xbox360" ? true : false,
                    PS3 = chbx_Format.Text == "PS3" || file.Platform == "Ps3" ? true : false,
                    Name = file.DLC_Name,
                    AppId = file.DLC_AppID,
                    ArtFiles = info.ArtFiles, //not complete
                    Showlights = true,//info.Showlights, //apparently this info is not read..also the tone base is removed/not read also
                    Inlay = info.Inlay,
                    LyricArtPath = info.LyricArtPath,

                    //USEFUL CMDs String.IsNullOrEmpty(
                    SongInfo = new RocksmithToolkitLib.DLCPackage.SongInfo
                    {
                        SongDisplayName = file.Song_Title,
                        SongDisplayNameSort = file.Song_Title_Sort,
                        Album = file.Album,
                        SongYear = file.Album_Year.ToInt32(),
                        Artist = file.Artist,
                        ArtistSort = file.Artist_Sort,
                        AverageTempo = file.AverageTempo.ToInt32()
                    },

                    AlbumArtPath = file.AlbumArtPath,
                    OggPath = file.AudioPath,
                    OggPreviewPath = ((file.audioPreviewPath != "") ? file.audioPreviewPath : file.AudioPath),
                    Arrangements = info.Arrangements, //Not yet done
                    Tones = info.Tones,//Not yet done
                    TonesRS2014 = info.TonesRS2014,//Not yet done
                    Volume = Convert.ToSingle(file.Volume),
                    PreviewVolume = Convert.ToSingle(file.Preview_Volume),
                    SignatureType = info.SignatureType,
                    PackageVersion = file.ToolkitVersion//Version
                };

                //Add Tones
                var cmds = "SELECT * FROM Tones WHERE CDLC_ID=" + txt_ID.Text + ";";
                DataSet dfs = new DataSet();
                var norec = 0;
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    try
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                        da.Fill(dfs, "Tones");
                    }
                    catch (Exception ex)
                    {

                    }

                    foreach (var arg in info.TonesRS2014)//, Type
                    {
                        norec = dfs.Tables[0].Rows.Count;
                        for (int j = 0; j < norec; j++)
                        {
                            data.TonesRS2014[j].Name = dfs.Tables[0].Rows[j].ItemArray[1].ToString();
                            data.TonesRS2014[j].Volume = dfs.Tables[0].Rows[j].ItemArray[3].ToString().ToInt32();
                            data.TonesRS2014[j].Key = dfs.Tables[0].Rows[j].ItemArray[4].ToString();
                            data.TonesRS2014[j].IsCustom = dfs.Tables[0].Rows[j].ItemArray[5].ToString() =="True"? true: false;
                            data.TonesRS2014[j].SortOrder = dfs.Tables[0].Rows[j].ItemArray[11].ToString().ToInt32();
                            data.TonesRS2014[j].NameSeparator = dfs.Tables[0].Rows[j].ItemArray[12].ToString();
                            //dictionary types not saved in the DB yet
                            //data.TonesRS2014[j].GearList.PostPedal1 = dfs.Tables[0].Rows[14].ItemArray[0].ToString().ToInt32();
                            //data.TonesRS2014[j].GearList.PostPedal2 = dfs.Tables[0].Rows[15].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.PostPedal3 = dfs.Tables[0].Rows[16].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.PostPedal4 = dfs.Tables[0].Rows[17].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.PrePedal1 = dfs.Tables[0].Rows[18].ItemArray[0].ToString().ToInt32();
                            //data.TonesRS2014[j].GearList.PrePedal2 = dfs.Tables[0].Rows[19].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.PrePedal3 = dfs.Tables[0].Rows[20].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.PrePedal4 = dfs.Tables[0].Rows[21].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.Rack1 = dfs.Tables[0].Rows[22].ItemArray[0].ToString().ToInt32();
                            //data.TonesRS2014[j].GearList.Rack2 = dfs.Tables[0].Rows[23].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.Rack3 = dfs.Tables[0].Rows[24].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.Rack4 = dfs.Tables[0].Rows[25].ItemArray[0].ToString();
                            //data.TonesRS2014[j].GearList.Amp.Type = dfs.Tables[0].Rows[26].ItemArray[0].ToString();
                            data.TonesRS2014[j].GearList.Amp.Category = dfs.Tables[0].Rows[j].ItemArray[27].ToString();
                           // data.TonesRS2014[j].GearList.Amp.KnobValues =  dfs.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
                            data.TonesRS2014[j].GearList.Amp.PedalKey = dfs.Tables[0].Rows[j].ItemArray[29].ToString();
                            data.TonesRS2014[j].GearList.Cabinet.Category = dfs.Tables[0].Rows[j].ItemArray[30].ToString();
                           // data.TonesRS2014[j].GearList.Cabinet.KnobValues =  dfs.Tables[0].Rows[31].ItemArray[0].ToString().ToInt32();
                            data.TonesRS2014[j].GearList.Cabinet.PedalKey = dfs.Tables[0].Rows[j].ItemArray[32].ToString();
                            data.TonesRS2014[j].GearList.Cabinet.Type = dfs.Tables[0].Rows[j].ItemArray[33].ToString();

                        }
                    }
                }


                //Add Arrangements
                cmds = "SELECT * FROM Arrangements WHERE CDLC_ID=" + txt_ID.Text + ";";
                norec = 0;
                DataSet ds = new DataSet();
                string sds = "";
                using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    try
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter(cmds, cn);
                        da.Fill(ds, "Arrangements");
                    }
                    catch (Exception ex)
                    {

                    }

                    norec = ds.Tables[0].Rows.Count;
                    if (info.Arrangements.Capacity < norec )
                    data.Arrangements.Add(new Arrangement
                    {
                        Name = ArrangementName.Vocals,
                        ArrangementType = ArrangementType.Vocal,
                        ScrollSpeed = 20,
                        //SongXml = new SongXML { File = xmlFile },
                        //SongFile = new SongFile { File = "" },
                        CustomFont = false
                    });
                    //norec += 1;

                    foreach (var arg in info.Arrangements)//, Type
                    {                       
                        for (int j = 0; j < norec; j++)
                        {
                            //if (j == data.Arrangements.Count) {
                            //    //mArr.Add(GenMetronomeArr(data.Arrangements));
                            //    //mArr[0].SongXml.File = "1";
                            //    //var mArr = new List<Arrangement>();
                            //    //data.Arrangements.Capacity = 5;
                            //    //data.Arrangements.Add(data.Arrangements[j]);
                            //    // Add Vocal Arrangement
                            //    data.Arrangements.Add(new Arrangement
                            //    {
                            //        Name = ArrangementName.Vocals,
                            //        ArrangementType = ArrangementType.Vocal,
                            //        ScrollSpeed = 20,
                            //        //SongXml = new SongXML { File = xmlFile },
                            //        //SongFile = new SongFile { File = "" },
                            //        CustomFont = false
                            //    });
                            //    norec += 1;
                            //}
                            sds = ds.Tables[0].Rows[j].ItemArray[1].ToString();
                            //data.Arrangements[j].Name = ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementName.Bass : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Lead" ? RocksmithToolkitLib.Sng.ArrangementName.Lead : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Vocals" ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : ds.Tables[0].Rows[j].ItemArray[1].ToString() == "Rhythm" ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : ds.Tables[0].Rows[j].ItemArray[12].ToString() == "ShowLights" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
                            data.Arrangements[j].Name = (sds == "3") ? RocksmithToolkitLib.Sng.ArrangementName.Bass : (sds == "0") ? RocksmithToolkitLib.Sng.ArrangementName.Lead : (sds == "4") ? RocksmithToolkitLib.Sng.ArrangementName.Vocals : (sds == "1") ? RocksmithToolkitLib.Sng.ArrangementName.Rhythm : sds == "6" ? RocksmithToolkitLib.Sng.ArrangementName.ShowLights : RocksmithToolkitLib.Sng.ArrangementName.Combo;
                            data.Arrangements[j].BonusArr = ds.Tables[0].Rows[j].ItemArray[3].ToString()=="false" ? false : true ;
                            sds=ds.Tables[0].Rows[j].ItemArray[4].ToString();
                            data.Arrangements[j].SongFile = new SongFile { File = ds.Tables[0].Rows[j].ItemArray[4].ToString() }; // if (File.Exists(sds))
                            data.Arrangements[j].SongXml = new SongXML { File = ds.Tables[0].Rows[j].ItemArray[5].ToString() };
                            data.Arrangements[j].ScrollSpeed = ds.Tables[0].Rows[j].ItemArray[7].ToString().ToInt32();
                            data.Arrangements[j].Tuning = ds.Tables[0].Rows[j].ItemArray[8].ToString();
                            data.Arrangements[j].ArrangementSort = ds.Tables[0].Rows[j].ItemArray[12].ToString().ToInt32();
                            data.Arrangements[j].TuningPitch = ds.Tables[0].Rows[j].ItemArray[13].ToString().ToInt32();
                            data.Arrangements[j].ToneBase = ds.Tables[0].Rows[j].ItemArray[14].ToString();
                            //data.Arrangements[j].Id = ds.Tables[0].Rows[15].ItemArray[0].ToString().ToInt16();
                            data.Arrangements[j].MasterId = ds.Tables[0].Rows[j].ItemArray[16].ToString().ToInt32();
                            data.Arrangements[j].ArrangementType = ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Bass" ? RocksmithToolkitLib.Sng.ArrangementType.Bass : ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Guitar" ? RocksmithToolkitLib.Sng.ArrangementType.Guitar : ds.Tables[0].Rows[j].ItemArray[17].ToString() == "Vocal" ? RocksmithToolkitLib.Sng.ArrangementType.Vocal : RocksmithToolkitLib.Sng.ArrangementType.ShowLight;
                            //RocksmithToolkitLib.Sng.ArrangementType.Bass ds.Tables[0].Rows[17].ItemArray[0].ToString();
                            //data.Arrangements[j].TuningStrings.String0 = ds.Tables[0].Rows[18].ItemArray[0].ToInt32();
                            //data.Arrangements[j].TuningStrings.String1 = ds.Tables[0].Rows[19].ItemArray[0].ToString();
                            //data.Arrangements[j].TuningStrings.String2 = ds.Tables[0].Rows[20].ItemArray[0].ToString();
                            //data.Arrangements[j].TuningStrings.String3 = ds.Tables[0].Rows[21].ItemArray[0].ToString();
                            //data.Arrangements[j].TuningStrings.String4 = ds.Tables[0].Rows[22].ItemArray[0].ToString();
                            //data.Arrangements[j].TuningStrings.String5 = ds.Tables[0].Rows[23].ItemArray[0].ToString();
                            data.Arrangements[j].PluckedType = ds.Tables[0].Rows[j].ItemArray[24].ToString()=="Plucked" ? RocksmithToolkitLib.Sng.PluckedType.Picked: RocksmithToolkitLib.Sng.PluckedType.NotPicked;
                            data.Arrangements[j].RouteMask = ds.Tables[0].Rows[j].ItemArray[25].ToString()=="Bass" ? RouteMask.Bass: ds.Tables[0].Rows[j].ItemArray[25].ToString() == "Lead"? RouteMask.Lead: ds.Tables[0].Rows[j].ItemArray[25].ToString()== "Rhythm" ? RouteMask.Rhythm : ds.Tables[0].Rows[j].ItemArray[25].ToString() == "None" ? RouteMask.None: RouteMask.Any;
                           // data.Arrangements[j].SongXml.Name = ds.Tables[0].Rows[26].ItemArray[0].ToString();
                            //data.Arrangements[j].SongXml.LLID = ds.Tables[0].Rows[27].ItemArray[0].ToInt32().ToInt32();
                            //data.Arrangements[j].SongXml.UUID = ds.Tables[0].Rows[28].ItemArray[0].ToString().ToInt32();
                            //data.Arrangements[j].SongFile.Name = ds.Tables[0].Rows[29].ItemArray[0].ToString();
                            //data.Arrangements[j].SongFile.LLID = ds.Tables[0].Rows[30].ItemArray[0].ToString().ToInt32();
                            //data.Arrangements[j].SongFile.UUID = ds.Tables[0].Rows[31].ItemArray[0].ToString();
                            data.Arrangements[j].ToneMultiplayer = ds.Tables[0].Rows[j].ItemArray[32].ToString();
                            data.Arrangements[j].ToneA = ds.Tables[0].Rows[j].ItemArray[33].ToString();
                            data.Arrangements[j].ToneB = ds.Tables[0].Rows[j].ItemArray[34].ToString();
                            data.Arrangements[j].ToneC = ds.Tables[0].Rows[j].ItemArray[35].ToString();
                            data.Arrangements[j].ToneD = ds.Tables[0].Rows[j].ItemArray[36].ToString();

                        }
                    }
                }

                //get track no
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul48"] == "Yes")
                {
                    var CleanTitle = "";
                    if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
                    if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
                    else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

                    string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
                    txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
                }

                if ((file.Author == "Custom Song Creator" || file.Author == "") && ConfigRepository.Instance()["dlcm_AdditionalManipul47"] == "Yes" && file.Is_Original != "Yes")
                {
                    //sving in the txt file is not rally usefull as the system gen the file at every pack :)
                    file.Author = "RepackedBy " + ConfigRepository.Instance()["general_defaultauthor"].ToUpper();
                    var fxml = File.OpenText(file.Folder_Name + "\\toolkit.version");
                    string line;
                    string header = "";
                    //Read and Save Header
                    while ((line = fxml.ReadLine()) != null)
                    {

                        if (line.Contains("Package Author:")) header += line + System.Environment.NewLine + "Package Author: " + file.Author;
                        else header += line + System.Environment.NewLine;
                    }
                    fxml.Close();
                    File.WriteAllText(file.Folder_Name + "\\toolkit.version", header);
                }

                DirectoryInfo di;
                var repacked_Path = TempPath + "\\0_repacked";
                if (!Directory.Exists(repacked_Path) && (repacked_Path != null)) di = Directory.CreateDirectory(repacked_Path);

                //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+" test"+i+ data.SongInfo.Artist + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var norm_path = "";
                if (ConfigRepository.Instance()["dlcm_Activ_FileName"] == "Yes")
                    norm_path = repacked_Path + "\\" + Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved);
                else
                    norm_path = ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                //rtxt_StatisticsOnReadDLCs.Text = "8"+data.PackageVersion+"...manipul" + norm_path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //manipulating the info
               // if (chbx_PackGroup.Checked) ;

                if (ConfigRepository.Instance()["dlcm_Activ_Title"] == "Yes") data.SongInfo.SongDisplayName = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title"], 0, false, false, bassRemoved);
                // rtxt_StatisticsOnReadDLCs.Text = "...manipul: "+ file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Title_Sort.Checked)
                if (ConfigRepository.Instance()["dlcm_Activ_TitleSort"] == "Yes") data.SongInfo.SongDisplayNameSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Title_sort"], 0, false, false, bassRemoved);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Artist.Checked)
                if (ConfigRepository.Instance()["dlcm_Activ_Artist"] == "Yes") data.SongInfo.Artist = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist"], 0, false, false, bassRemoved);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Artist_Sort.Checked)
                if (ConfigRepository.Instance()["dlcm_Activ_ArtistSort"] == "Yes") data.SongInfo.ArtistSort = Manipulate_strings(ConfigRepository.Instance()["dlcm_Artist_Sort"], 0, false, false, bassRemoved);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Album.Checked)
                if (ConfigRepository.Instance()["dlcm_Activ_Album"] == "Yes") data.SongInfo.Album = Manipulate_strings(ConfigRepository.Instance()["dlcm_Album"], 0, false, false, bassRemoved);
                //rtxt_StatisticsOnReadDLCs.Text = "...3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //rtxt_StatisticsOnReadDLCs.Text = "...nipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (chbx_Additional_Manipualtions.GetItemChecked(0)) //"1. Add Increment to all Titles"
                //data.Name = i + data.Name;

                //rtxt_StatisticsOnReadDLCs.Text = "...mpl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //artist = "";
                //if (chbx_Additional_Manipualtions.GetItemChecked(1)) //"2. Add Increment to all songs(&Separately per artist)"
                //{
                //    if (i > 0)
                //        if (data.SongInfo.Artist == files[i - 1].Artist) no_ord += 1;
                //        else no_ord = 1;
                //    else no_ord += 1;
                //    artist = no_ord + " ";
                //    data.SongInfo.SongDisplayName = i + artist + data.SongInfo.SongDisplayName;
                //}

                //if (chbx_Additional_Manipualtions.GetItemChecked(7)) //"8. Don't repack Broken songs"
                //    if (file.Is_Broken == "Yes") break;
                //rtxt_StatisticsOnReadDLCs.Text = "...4" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //rtxt_StatisticsOnReadDLCs.Text = "...manipl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (chbx_Additional_Manipualtions.GetItemChecked(2)) //"3. Make all DLC IDs unique (&save)"
                pB_ReadDLCs.Increment(1);

               

                    if (chbx_UniqueID.Checked)
                //if (file.UniqueDLCName != "") data.Name = file.UniqueDLCName;
                //else
                {
                    Random random = new Random();
                    data.Name = random.Next(0, 100000) + data.Name;
                    norm_path += data.Name;
                    //repacked_Path + "\\" + Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved);

                    //var k = DataViewGrid.SelectedCells[0].RowIndex;
                    //DataViewGrid.Rows[k].Cells[79].Value = data.Name;
                    ////var DB_Path = DBFolder + "\\Files.accdb;";
                    //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                    //{
                    //    DataSet dis = new DataSet();
                    //    cmd = "UPDATE Main SET UniqueDLCName=\"" + data.Name + "\" WHERE ID=" + file.ID;
                    //    OleDbDataAdapter das = new OleDbDataAdapter(cmd, cnn);
                    //    das.Fill(dis, "Main");
                    //    das.Dispose();
                    //}
                }

                //Fix the _preview_preview issue
                var ms = data.OggPath; //var audiopath = ""; var audioprevpath = "";
                var tst = "";
                //MessageBox.Show("One or more");
                //rtxt_StatisticsOnReadDLCs.Text = "maybe fixing .."+ file.Folder_Name+"\n"+ norm_path + "\n"+ rtxt_StatisticsOnReadDLCs.Text;
                try
                {
                    var sourceAudioFiles = Directory.GetFiles(file.Folder_Name, "*.wem", SearchOption.AllDirectories);
                    //if (sourceAudioFiles.Length>0)
                    //var targetAudioFiles = new List<string>();

                    foreach (var fil in sourceAudioFiles)
                    {
                        tst = fil;
                        //MessageBox.Show("test2.02 " + fil);
                        //rtxt_StatisticsOnReadDLCs.Text = "thinking about fixing _preview_preview issue.." + norm_path +"-"+tst+ "\n"+rtxt_StatisticsOnReadDLCs.Text;
                        if (fil.LastIndexOf("_preview_preview.wem") > 0)
                        {
                            ms = fil.Substring(0, fil.LastIndexOf("_preview_preview.wem"));
                            File.Move((ms + "_preview.wem"), (ms + ".wem"));
                            File.Move((ms + "_preview_preview.wem"), (ms + "_preview.wem"));
                            //bcapirtxt_StatisticsOnReadDLCs.Text = "fixing _preview_preview issue..." + rtxt_StatisticsOnReadDLCs.Text;
                        }
                    }
                }
                catch (Exception ee)
                {
                    //bcapirtxt_StatisticsOnReadDLCs.Text = "FAILED6-" + ee.Message + tst + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                    Console.WriteLine(ee.Message);
                }
                if (data == null)
                {
                    MessageBox.Show("One or more fields are missing information.", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                //rtxt_StatisticsOnReadDLCs.Text = "...5" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //dlcSavePath = ds.Tables[0].Rows[i].ItemArray[1].ToString() + "\\";// + ((info.PackageVersion == null) ? "Original" : "CDLC") + "-" + info.SongInfo.SongYear +".psarc";
                //var dlcSavePath = GeneralExtensions.GetShortName("{0}_{1}_v{2}", (((file.Version == null) ? "Original" : "CDLC") + "_" + info.SongInfo.SongDisplayName), (info.SongInfo.SongDisplayName + "_" + info.SongInfo.Album + "_" + info.SongInfo.SongYear), info.PackageVersion, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));
                var FN = "";
                //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+ "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_File_Name.Checked) FN = Manipulate_strings(txt_File_Name.Text, i, true, false);
                //els


                //FN = GeneralExtensions.GetShortName("{0}-{1}-v{2}", (((file.Version == null) ? "ORIG" : "CDLC") + "_" + file.Artist), (file.Album_Year.ToInt32() + "_" + file.Album + "_" + file.Song_Title), file.Version, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));//((data.PackageVersion == null) ? "Original" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                FN = Manipulate_strings(ConfigRepository.Instance()["dlcm_File_Name"], 0, false, false, bassRemoved);
                //if (file.Is_Alternate == "Yes") FN += "a." + file.Alternate_Version_No + file.Author;

                //rtxt_StatisticsOnReadDLCs.Text = "fn: " + FN + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (ConfigRepository.Instance()["dlcm_AdditionalManipul8"] == "Yes" || chbx_Format.Text == "PS3")
                {
                    FN = FN.Replace(".", "_");
                    FN = FN.Replace(" ", "_");
                }

                dlcSavePath = repacked_Path + "\\" + chbx_Format.Text + "\\" + FN;
                //rtxt_StatisticsOnReadDLCs.Text = "rez : " + dlcSavePath + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (GameVersion.RS2014 == GameVersion.RS2012) //old code
                //{
                //    try
                //    {
                //        OggFile.VerifyHeaders(data.OggPath);
                //    }
                //    catch (InvalidDataException ex)
                //    {
                //        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
                //rtxt_StatisticsOnReadDLCs.Text = "genf : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                int progress = 0;
                var errorsFound = new StringBuilder();
                var numPlatforms = 0;
                //if (platformPC.Checked)
                numPlatforms++;
                //if (chbx_Format=="Mac")
                //    numPlatforms++;
                //if (chbx_XBOX360.Checked)
                //    numPlatforms++;
                //if (chbx_PS3.Checked)
                //    numPlatforms++;

                var step = (int)Math.Round(1.0 / numPlatforms * 100, 0);
                // rtxt_StatisticsOnReadDLCs.Text = "...6" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (chbx_Format.Text == "PC")
                    try
                    {
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                        progress += step;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                            frm1.ShowDialog();
                        }
                        errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                    }

                if (chbx_Format.Text == "Mac")
                    try
                    {
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
                        progress += step;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                            frm1.ShowDialog();
                        }
                        errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                    }

                if (chbx_Format.Text == "XBOX360")
                    try
                    {
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
                        progress += step;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                            frm1.ShowDialog();
                        }
                        errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                    }

                if (chbx_Format.Text == "PS3")
                    try
                    {
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                        progress += step;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.IndexOf("No JDK or JRE") > 0)//Help\\WwiseHelp_en.chm"))//
                        {
                            ErrorWindow frm1 = new ErrorWindow("Please Install Java" + Environment.NewLine + "A restart is required" + Environment.NewLine, "http://www.java.com/en/download/win10.jsp", "Error at Packing", false, false);
                            frm1.ShowDialog();
                        }
                        string ss = String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace);
                        MessageBox.Show(ex + ss);
                        errorsFound.AppendLine(ss);
                    }
                data.CleanCache();
                //rtxt_StatisticsOnReadDLCs.Text = "gen2 : " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                i++;
                //rtxt_StatisticsOnReadDLCs.Text = "gen r: " + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //TO DO DELETE the ORIGINAL IMPORTED FILES or not
                //bcapirtxt_StatisticsOnReadDLCs.Text = "\nRepack bkworkerdone.." + i + rtxt_StatisticsOnReadDLCs.Text;


                //}
                //MessageBox.Show("tst");
            }
            //bcapirtxt_StatisticsOnReadDLCs.Text = "\n...Repack done.." + rtxt_StatisticsOnReadDLCs.Text;
            pB_ReadDLCs.Increment(1);
            return dlcSavePath;
            MessageBox.Show("Repack done");
        }

        //private Arrangement GenMetronomeArr(List<Arrangement> arrangements)
        //{
        //    throw new NotImplementedException();
        //}

        public Arrangement GenMetronomeArr(Arrangement arr)
        {
            var mArr = GeneralExtensions.Copy(arr);
            var songXml = Song2014.LoadFromFile(mArr.SongXml.File);
            var newXml = Path.GetTempFileName();
            mArr.SongXml = new RocksmithToolkitLib.DLCPackage.AggregateGraph.SongXML { File = newXml };
            mArr.SongFile = new RocksmithToolkitLib.DLCPackage.AggregateGraph.SongFile { File = "" };
            mArr.ClearCache();
            mArr.BonusArr = true;
            mArr.Id = IdGenerator.Guid();
            mArr.MasterId = RandomGenerator.NextInt();
            mArr.Metronome = Metronome.Itself;
            songXml.ArrangementProperties.Metronome = (int)Metronome.Itself;

            var ebeats = songXml.Ebeats;
            var songEvents = new RocksmithToolkitLib.Xml.SongEvent[ebeats.Length];
            for (var i = 0; i < ebeats.Length; i++)
            {
                songEvents[i] = new RocksmithToolkitLib.Xml.SongEvent
                {
                    Code = ebeats[i].Measure == -1 ? "B1" : "B0",
                    Time = ebeats[i].Time
                };
            }
            songXml.Events = songXml.Events.Union(songEvents, new EqSEvent()).OrderBy(x => x.Time).ToArray();
            using (var stream = File.OpenWrite(mArr.SongXml.File))
            {
                songXml.Serialize(stream, true);
            }
            return mArr;
        }

        private class EqSEvent : IEqualityComparer<RocksmithToolkitLib.Xml.SongEvent>
        {
            public bool Equals(RocksmithToolkitLib.Xml.SongEvent x, RocksmithToolkitLib.Xml.SongEvent y)
            {
                if (x == null)
                    return y == null;

                return x.Code == y.Code && x.Time.Equals(y.Time);
            }

            public int GetHashCode(RocksmithToolkitLib.Xml.SongEvent obj)
            {
                if (ReferenceEquals(obj, null))
                    return 0;
                return obj.Code.GetHashCode() | obj.Time.GetHashCode();
            }
        }

        public string Manipulate_strings(string words, int k, bool ifn, bool orig_flag, bool bassRemoved)
        {
            //Read from DB
            int norows = 0;
            //1. Get Random Song ID
            var cmd = "";
            if (k == -1)
            {
                k = 0;
                cmd = "SELECT TOP 1 * FROM Main";
                //Files IDs = new Files();
                norows = SQLAccess(cmd);
            }

            //2. Read from DB
            //cmd = "SELECT * FROM Main WHERE ID = " + files[k].ID;
            //norows = SQLAccess(cmd);
            //rtxt_StatisticsOnReadDLCs.Text ="f" + rtxt_StatisticsOnReadDLCs.Text;
            // Parse the text char by char
            // If <> makes sense then bring that info
            // If not inbetween <> then just add to the final string
            var i = 0;
            var txt = words;
            var curtext = "";
            var curelem = "";
            var fulltxt = "";
            var readt = false;
            var oldtxt = "";
            var last_ = 0;
            for (i = 0; i <= txt.Length - 1; i++)
            {
                //rtxt_StatisticsOnReadDLCs.Text = k  +"\n"+ rtxt_StatisticsOnReadDLCs.Text;
                curtext = txt[i].ToString();//.Substring(i,1);
                if (curtext == "<")
                {
                    readt = true;
                    curelem = "";
                }

                if (readt == true)
                    curelem += curtext;
                else fulltxt += curtext;

                bool origQAs = true;
                if (orig_flag) origQAs = false;
                //else origQAs = false;

                oldtxt = fulltxt;
                if (curtext == ">")
                {
                    readt = false;
                    switch (curelem)
                    {
                        case "<Artist>":
                            fulltxt += files[k].Artist;
                            break;
                        case "<Title>":
                            fulltxt += files[k].Song_Title;
                            break;
                        case "<Version>":
                            fulltxt += files[k].Version;
                            break;
                        case "<DLCName>":
                            fulltxt += files[k].DLC_Name;
                            break;
                        case "<Album>":
                            fulltxt += files[k].Album;
                            break;
                        case "<Track No.>":
                            fulltxt += ((files[k].Track_No != "") ? ("-" + files[k].Track_No) : "");
                            break;
                        case "<Year>":
                            fulltxt += files[k].Album_Year;
                            break;
                        case "<Rating>":
                            fulltxt += ((files[k].Rating == "") ? "" : "r." + files[k].Rating);
                            break;
                        case "<Alt. Vers.>":
                            fulltxt += files[k].Alternate_Version_No == "" ? "" : "a." + files[k].Alternate_Version_No;
                            break;
                        case "<Descr.>":
                            fulltxt += files[k].Description;
                            break;
                        case "<Comm.>":
                            fulltxt += files[k].Comments;
                            break;
                        case "<Tuning>":
                            fulltxt += files[k].Tunning;
                            break;
                        case "<Instr. Rating.>":
                            fulltxt += ((files[k].Has_Guitar == "Yes") ? "G" : "") + "" + ((files[k].Has_Bass == "Yes") ? "B" : ""); //not yet done for all arrangements
                            break;
                        case "<MTrack Det.>":
                            fulltxt += files[k].MultiTrack_Version;//?
                            break;
                        case "<Group>":
                            fulltxt += files[k].Groups;
                            break;
                        case "<Beta>":
                            fulltxt = ((files[k].Is_Beta == "Yes") ? "0" : "") + fulltxt;
                            break;
                        case "<Broken>":
                            fulltxt = ((files[k].Is_Broken == "Yes") ? "<B>" : "") + fulltxt;
                            break;
                        case "<File Name>":
                            fulltxt += files[k].Current_FileName;
                            break;
                        case "<Bonus>":
                            fulltxt += ((files[k].Has_Bonus_Arrangement == "Yes") ? "Bonus" : ""); //not yet done for all arrangements
                            break;
                        case "<Artist Short>":
                            fulltxt += files[k].Artist_ShortName;
                            break;
                        case "<Album Short>":
                            fulltxt += files[k].Album_ShortName;
                            break;
                        case "<Author>":
                            fulltxt += files[k].Author;
                            break;
                        case "<Space>":
                            fulltxt += " ";
                            break;
                        case "<Avail. Tracks>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Lead == "Yes") ? "L" : "") + ((files[k].Has_Combo == "Yes") ? "C" : "") + ((files[k].Has_Rhythm == "Yes") ? "R" : "") + ((files[k].Has_Vocals == "Yes") ? "V" : "");
                            break;
                        case "<Bass_HasDD>":
                            fulltxt += ((files[k].Has_BassDD == "No" || bassRemoved) && files[k].Has_DD == "Yes" ? "NoBDD" : "");
                            break;
                        case "<Avail. Instr.>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Guitar == "Yes") ? "G" : "");
                            break;
                        case "<Timestamp>":
                            fulltxt += System.DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
                            break;
                        default:
                            if ((origQAs) || (ifn))
                            {
                                switch (curelem)
                                {
                                    case "<DD>":
                                        fulltxt += files[k].Has_DD == "Yes" ? "DD" : "noDD";
                                        break;
                                    case "<CDLC>":
                                        fulltxt += files[k].DLC;
                                        break;
                                    case "<QAs>":
                                        fulltxt += (((files[k].Has_Cover == "Yes") || (files[k].Has_Preview == "Yes") || (files[k].Has_Vocals == "Yes")) ? "" : "NOs-") + ((files[k].Has_Cover == "Yes") ? "" : "C") + ((files[k].Has_Preview == "Yes") ? "" : "P") + ((files[k].Has_Vocals == "Yes") ? "" : "V"); //+((files[k].Has_Sections == "Yes") ? "" : "S")
                                        break;
                                    case "<lastConversionDateTime>":
                                        fulltxt += files[k].Import_Date; //not yet done
                                        break;
                                    default: break;
                                }
                            }
                            break;
                    }
                    if (oldtxt == fulltxt && last_ > 0)
                        //{
                        //    rtxt_StatisticsOnReadDLCs.Text = i + "...\n"+rtxt_StatisticsOnReadDLCs.Text;                        
                        fulltxt = fulltxt.Substring(0, last_);
                    //}
                    last_ = fulltxt.Length;
                }
            }
            //rtxt_StatisticsOnReadDLCs.Text = "returning " + i + " char " + fulltxt + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            return fulltxt;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chbx_PreSavedFTP.Text == "EU") txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP1"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLUS31182/USRDIR/DLC/";
            else txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP2"];//"ftp://192.168.1.12/" + "dev_hdd0/game/BLES01862/USRDIR/DLC/";
        }

        private void btn_RemoveBassDD_Click(object sender, EventArgs e)
        {

            var cmd = "SELECT * FROM Main ";
            //if (rbtn_Population_Selected.Checked == true) 
            cmd += "WHERE ID = " + txt_ID.Text + "";
            //else if (rbtn_Population_All.Checked) ;
            //else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            cmd += " ORDER BY Artist";
            //Read from DB
            var norows = 0;
            norows = SQLAccess(cmd);

            var xmlFiles = Directory.GetFiles(files[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = files[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
            {
                Song2014 xmlContent = null;
                try
                {
                    xmlContent = Song2014.LoadFromFile(xml);
                    if (xmlContent.Arrangement.ToLower() == "bass" && xml.IndexOf(".old") <= 0)
                    {
                        var bassRemoved = (DLCManager.RemoveDD(files[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false) == "Yes") ? "Yes" : "No";
                        chbx_BassDD.Checked = false;
                        btn_RemoveBassDD.Enabled = false;
                        SaveRecord();
                    }
                }
                catch (Exception ee)
                {
                }
            }
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; btn_RemoveBassDD.Enabled = true;//numericUpDown1.Enabled = false;
            var cmd = "SELECT * FROM Main ";
            //if (rbtn_Population_Selected.Checked == true) 
            cmd += "WHERE ID = " + txt_ID.Text + "";
            //else if (rbtn_Population_All.Checked) ;
            //else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            cmd += " ORDER BY Artist";
            //Read from DB
            var norows = 0;
            norows = SQLAccess(cmd);

            var xmlFiles = Directory.GetFiles(files[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = files[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
                if (xml.IndexOf("showlights") < 1)
                {
                    var DDAdded = (DLCManager.AddDD(files[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false, numericUpDown1.Value.ToString()) == "Yes") ? "No" : "Yes";
                }
            chbx_BassDD.Checked = true;
            chbx_DD.Checked = true;
            SaveRecord();
        }

        private void btn_RemoveDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = false; btn_AddDD.Enabled = true; btn_RemoveBassDD.Enabled = false;//numericUpDown1.Enabled = true; 
            var cmd = "SELECT * FROM Main ";
            //if (rbtn_Population_Selected.Checked == true) 
            cmd += "WHERE ID = " + txt_ID.Text + "";
            //else if (rbtn_Population_All.Checked) ;
            //else if (rbtn_Population_Groups.Checked) cmd += "WHERE Groups = " + cbx_Groups.SelectedText;

            cmd += " ORDER BY Artist";
            //Read from DB
            var norows = 0;
            norows = SQLAccess(cmd);

            var xmlFiles = Directory.GetFiles(files[0].Folder_Name, "*.xml", SearchOption.AllDirectories);
            var platform = files[0].Folder_Name.GetPlatform();

            foreach (var xml in xmlFiles)
            {
                Song2014 xmlContent = null;
                try
                {
                    xmlContent = Song2014.LoadFromFile(xml);
                    if (!(xmlContent.Arrangement.ToLower() == "showlights" || xmlContent.Arrangement.ToLower() == "vocals") || xml.IndexOf(".old") <= 0)
                    {
                        var DDRemoved = (DLCManager.RemoveDD(files[0].Folder_Name, chbx_Original.Checked ? "Yes" : "", xml, platform, false, false) == "Yes") ? "Yes" : "No";
                    }
                }
                catch (Exception ee)
                { }
            }
            chbx_DD.Checked = false;
            chbx_BassDD.Checked = false;
            SaveRecord();
        }

        private void btn_OldFolder_Click(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = TempPath + "\\0_old\\" + DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString();
            //dus.Tables[0].Rows[DataGridView1.SelectedCells[0].RowIndex].ItemArray[19].ToString();// files[0].Original_FileName;
            try
            {
                Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Old Folder in Explorer ! ");
            }
        }

        private void btn_DuplicateFolder_Click(object sender, EventArgs e)
        {
            string t = TempPath + "\\0_duplicate";
            try
            {
                Process process = Process.Start(@t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Duplicate folder in Exporer ! ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ////1. Delete Song Folder
            //var j = DataViewGrid.SelectedCells[0].RowIndex;
            //string filePath = DataViewGrid.Rows[j].Cells[22].Value.ToString(); //TempPath + "\\0_old\\" + 
            // try
            //{
            //     Directory.Delete(filePath, true);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not Delete Song folder ! ");
            //}

            // //Move psarc file to Duplicates
            // var iii = DataViewGrid.SelectedCells[0].RowIndex;
            //string psarcPath = TempPath + "\\0_old\\"+ DataViewGrid.Rows[iii].Cells[19].Value.ToString();
            // try
            // {
            //     if (!File.Exists(psarcPath.Replace("0_old", "0_duplicate\\")))
            //     File.Move(psarcPath, psarcPath.Replace("0_old", "0_duplicate\\"));
            //     else File.Delete(psarcPath);
            // }
            // catch (Exception ex)
            // {
            //     //MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //     //MessageBox.Show("Can not Move psarc ! ");
            //     //File.Delete(psarcPath);
            // }

            var cmd = "DELETE FROM Main WHERE ID IN (" + txt_ID.Text + ")";
            //var DB_Path = DBFolder;

            //1. Delete DB records
            DLCManager.DeleteRecords(txt_ID.Text, cmd, DB_Path, TempPath, "1");

            //redresh 
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();

            //advance or step back in the song list
            if (DataViewGrid.Rows.Count > 1)
            {
                var prev = DataViewGrid.SelectedCells[0].RowIndex; //nud_RemoveSlide.Value;
                if (DataViewGrid.Rows.Count == prev + 2)
                    if (prev == 0) return;
                    else
                    {
                        int rowindex;
                        DataGridViewRow row;
                        var i = DataViewGrid.SelectedCells[0].RowIndex;
                        rowindex = i;// DataGridView1.SelectedRows[0].Index;
                        DataViewGrid.Rows[rowindex - 1].Selected = true;
                        DataViewGrid.Rows[rowindex].Selected = false;
                        //if (prev>txt_Counter.Text.ToInt32())
                        row = DataViewGrid.Rows[rowindex - 1];
                    }
                else
                {
                    int rowindex;
                    DataGridViewRow row;
                    var i = DataViewGrid.SelectedCells[0].RowIndex;
                    rowindex = i;// DataGridView1.SelectedRows[0].Index;
                    DataViewGrid.Rows[rowindex + 1].Selected = true;
                    DataViewGrid.Rows[rowindex].Selected = false;
                    //if (prev>txt_Counter.Text.ToInt32())
                    row = DataViewGrid.Rows[rowindex + 1];
                }
            }

        }

        private void btn_Duplicate_Click(object sender, EventArgs e)
        {
            if (chbx_AutoSave.Checked) SaveOK = true;
            else SaveOK = false;

            //1. Copy Files
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString(); //TempPath + "\\0_old\\" +
            //Random random = new Random();random.Next(0, 100000)
            //Generate MAX Alternate NO
            var sel = "SELECT * FROM Main WHERE LCASE(Artist)=LCASE(\"" + txt_Artist.Text + "\") AND "; //AND LCASE(Album)=LCASE(\"" + info.SongInfo.Album + "\")
            sel += "(LCASE(Song_Title) = LCASE(\"" + txt_Title.Text + "\") ";
            sel += "OR LCASE(Song_Title) like \"%" + txt_Title.Text.ToLower() + "%\" ";
            //sel += "OR (\"%LCASE(Song_Title)%\" like LCASE(\"" + info.SongInfo.SongDisplayName + "\") ";
            sel += "OR LCASE(Song_Title_Sort) =LCASE(\"" + txt_Title_Sort.Text + "\")) OR LCASE(DLC_Name) like LCASE(\"%" + txt_DLC_ID.Text + "%\") ORDER BY Is_Original ASC";
            var sel1 = sel.Replace("SELECT *", "SELECT max(Alternate_Version_No)");
            sel1 = sel1.Replace(" ORDER BY Is_Original ASC", "");
            //rtxt_StatisticsOnReadDLCs.Text = sel1 + "-" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
            int max = 1;
            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {
                DataSet ddzv = new DataSet();
                OleDbDataAdapter dat = new OleDbDataAdapter(sel1, cnn);
                dat.Fill(ddzv, "Main");
                dat.Dispose();
                max = ddzv.Tables[0].Rows[0].ItemArray[0].ToString() == "" ? 0 : ddzv.Tables[0].Rows[0].ItemArray[0].ToString().ToInt32();
                max += 1;
            }
            string t = filePath + max.ToString();
            string source_dir = @filePath;
            string destination_dir = @t;
            var fold = "";
            var fold2 = "";
            try //Copy dir
            {
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
                    File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                }

                //copy old
                if (DataViewGrid.Rows[i].Cells["Available_Old"].Value.ToString() == "Yes")
                {
                    fold = TempPath + "\\0_old\\" + DataViewGrid.Rows[i].Cells["Original_FileName"].Value.ToString();
                    fold2 = Path.GetFileNameWithoutExtension(fold) + "" + max.ToString();
                    File.Copy(fold, fold2.Replace(fold, fold2), true);
                }
                //Directory.Delete(source_dir, true); DONT DELETE

            }
            catch (Exception ee)
            {
                //rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                MessageBox.Show("FAILED To copy Files" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }

            //2. Copy Records
            try //Copy dir
            {
                var AlbumArtPath = DataViewGrid.Rows[i].Cells["AlbumArtPath"].Value.ToString().Replace(source_dir, destination_dir);
                var AudioPath = DataViewGrid.Rows[i].Cells["AudioPath"].Value.ToString().Replace(source_dir, destination_dir);
                var audioPreviewPath = DataViewGrid.Rows[i].Cells["audioPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var OggPath = DataViewGrid.Rows[i].Cells["OggPath"].Value.ToString().Replace(source_dir, destination_dir);
                var oggPreviewPath = DataViewGrid.Rows[i].Cells["oggPreviewPath"].Value.ToString().Replace(source_dir, destination_dir);
                var cmd = "INSERT into Main (Song_Title, Song_Title_Sort, Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, AlbumArtPath, AudioPath, audioPreviewPath, Track_No, Author, Version, DLC_Name, DLC_AppID, Current_FileName, Original_FileName, Import_Path, Import_Date, Folder_Name, File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description, Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD, Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, OggPath, oggPreviewPath, UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date) SELECT Song_Title+\" alt" + max.ToString() + "\", Song_Title_Sort+\" alt" + max.ToString() + "\", Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, \"" + AlbumArtPath + "\", \"" + AudioPath + "\", \"" + audioPreviewPath + "\", Track_No, Author, Version, DLC_Name+\"" + max.ToString() + "\", DLC_AppID, Current_FileName, \"" + fold2.Replace(fold, fold2) + "\", Import_Path, Import_Date, Folder_Name+\"" + max.ToString() + "\", File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, \"" + "Yes" + "\", Is_Multitrack, Is_Broken, MultiTrack_Version, " + max.ToString() + ", DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description+\" duplicate\", Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, FilesMissingIssues, Duplicates, Pack, Keep_BassDD, Keep_DD, Keep_Original, Song_Lenght, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, \"" + OggPath + "\", \"" + oggPreviewPath + "\", UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected, File_Creation_Date FROM Main  WHERE ID = " + txt_ID.Text;
                DataSet dsz = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                    dab.Fill(dsz, "Main");
                    dab.Dispose();
                }


                //getting ID
                DataSet dus = new DataSet();
                cmd = "SELECT ID FROM Main WHERE Song_Title=\"" + txt_Title.Text + " alt" + max.ToString() + "\"";
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dad = new OleDbDataAdapter(cmd, cnb);
                    dad.Fill(dus, "Main");
                    dad.Dispose();
                }
                var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();

                //var SNGFilePath = ""; SNGFilePath = SNGFilePath.Replace(source_dir, destination_dir);//= REPLACE(OggPath, left(OggPath, instr(OggPath, '" + tmpp + "') - 1), '" + TempPath + tmpp + "')
                //var XMLFilePath = ""; XMLFilePath = XMLFilePath.Replace(source_dir, destination_dir);
                cmd = "INSERT into Arrangements (Arrangement_Name, CDLC_ID, Bonus, SNGFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments) SELECT Arrangement_Name, " + CDLC_ID + ", Bonus, \"" + destination_dir + "\\manifests\\\"+right(SNGFilePath,len(SNGFilePath)-instr(SNGFilePath, 'manifests')-9), \"" + destination_dir + "\\songs\\arr\\\"+right(XMLFilePath,len(XMLFilePath)-instr(XMLFilePath, '\\songs\\arr\\')-10), XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments FROM Arrangements WHERE CDLC_ID = " + txt_ID.Text;
                DataSet dgz = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dah = new OleDbDataAdapter(cmd, cnb);
                    dah.Fill(dgz, "Arrangements");
                    dah.Dispose();
                }

                cmd = "INSERT into Tones (Tone_Name, CDLC_ID, Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime) SELECT Tone_Name, " + CDLC_ID + ", Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime FROM Tones WHERE CDLC_ID = " + txt_ID.Text;
                DataSet djz = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter djb = new OleDbDataAdapter(cmd, cnb);
                    djb.Fill(djz, "Tones");
                    djb.Dispose();
                }

                MessageBox.Show("Record has been duplicated");

                //redresh 
                Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
                DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataViewGrid.Refresh();
            }
            catch (Exception ee)
            {
                //rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                MessageBox.Show("FAILED To copy REcords" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var vorbis = new NVorbis.VorbisReader(@DataViewGrid.Rows[0].Cells["OggPreviewPath"].Value.ToString()))
            {
                int channels = vorbis.Channels;
                int sampleRate = vorbis.SampleRate;
                var duration = vorbis.TotalTime;

                var buffer = new float[16384];
                int count;
                //while ((count = vorbis.ReadSamples(buffer, 0, buffer.Length)) > 0)
                //{
                // Do stuff with the samples returned...
                // Sample value range is -0.99999994f to 0.99999994f
                // Samples are interleaved (chan0, chan1, chan0, chan1, etc.)
                txt_debug.Text += duration;
                //}
            }

            //Read Track no
            //www.metrolyrics.com: Nirvana Bleach Swap Meet
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.google.com/?gws_rd=ssl#q=www.metrolyrics.com:" + txt_Artist.Text + txt_Album.Text + txt_Title.Text.Replace(" ", "+"));
            //request.Proxy = WebProxy.GetDefaultProxy();
            //request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //// Set the Method property of the request to POST.
            //request.Method = "POST";

            //// Create POST data and convert it to a byte array.
            //string postData = "This is a test that posts this string to a Web server.";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //// Set the ContentType property of the WebRequest.
            //request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;

            //// Get the request stream.
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.
            //dataStream.Write(byteArray, 0, byteArray.Length);
            //// Close the Stream object.
            //dataStream.Close();

            //// Get the response.
            //WebResponse response = request.GetResponse();
            //// Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //// Get the stream containing content returned by the server.
            //dataStream = response.GetResponseStream();
            //// Open the stream using a StreamReader for easy access.
            //StreamReader reader = new StreamReader(dataStream);
            //// Read the content.
            //string responseFromServer = reader.ReadToEnd();
            //// Display the content.
            //Console.WriteLine(responseFromServer);
            //// Clean up the streams.
            //reader.Close();
            //dataStream.Close();
            //response.Close();

            //ttpWebRequest request = WebRequest.Create("http://google.com") as HttpWebRequest;

            //request.Accept = "application/xrds+xml";  
            ////HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            ////WebHeaderCollection header = response.Headers;

            ////var encoding = ASCIIEncoding.ASCII;
            ////using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            ////{
            ////    string responseText = reader.ReadToEnd();
            ////    string a= responseText.Substring(responseText.IndexOf("#"),3);
            ////}

            ////tring uriString = "http://www.google.com/search";
            ////string keywordString = "Test Keyword";

            ////WebClient webClient = new WebClient();

            ////NameValueCollection nameValueCollection = new NameValueCollection();
            ////nameValueCollection.Add("q", keywordString);

            ////webClient.QueryString.Add(nameValueCollection);
            ////textBox1.Text = webClient.DownloadString(uriString);

            txt_Track_No.Text = (GetTrackNo(txt_Artist.Text, txt_Album.Text, txt_Title.Text)).ToString();
            string uriString = "https://www.youtube.com/results?search_query=";// "http://www.google.com/search";
            string keywordString = txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\""; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", keywordString);

            webClient.QueryString.Add(nameValueCollection);
            var aa = (webClient.DownloadString(uriString));
            var a1 = "";
            //if ( (aa.ToLower()).IndexOf(", <b>"+ txt_Title.Text.ToLower() ) >0 ) a1 = aa.Substring(((aa.ToLower()).IndexOf(", <b>"+txt_Title.Text.ToLower())) -1, 1);//<b>+ txt_Title.Text

            if ((aa.ToLower()).IndexOf("<track-number>") > 0) a1 = aa.Substring(((aa.ToLower()).IndexOf("<track-number>")) + 14, 1);//<b>+ txt_Title.Text

            //if (IsNumbers(a1)) return a1.ToInt32();
            //else return 0;
            var a2 = aa.Substring(((aa.ToLower()).IndexOf("<track-number>")) + 1, 15);
            var a3 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 7, 1);
            var a4 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 8, 1) + aa;

            //DLCManager.
            txt_debug.Text += keywordString + "s\n" + a1 + "\n\n " + a2 + "\n " + a3 + "\n " + a4;


        }

        //public class AutorizationCodeAuth
        //{
        //    public delegate void OnResponseReceived(AutorizationCodeAuthResponse response);

        //    private SimpleHttpServer _httpServer;
        //    private Thread _httpThread;
        //    public String ClientId { get; set; }
        //    public String RedirectUri { get; set; }
        //    public String State { get; set; }
        //    public Scope Scope { get; set; }
        //    public Boolean ShowDialog { get; set; }

        //    /// <summary>
        //    ///     Will be fired once the user authenticated
        //    /// </summary>
        //    public event OnResponseReceived OnResponseReceivedEvent;

        //    /// <summary>
        //    ///     Start the auth process (Make sure the internal HTTP-Server ist started)
        //    /// </summary>
        //    public void DoAuth()
        //    {
        //        String uri = GetUri();
        //        Process.Start(uri);
        //    }

        //    /// <summary>
        //    ///     Refreshes auth by providing the clientsecret (Don't use this if you're on a client)
        //    /// </summary>
        //    /// <param name="refreshToken">The refresh-token of the earlier gathered token</param>
        //    /// <param name="clientSecret">Your Client-Secret, don't provide it if this is running on a client!</param>
        //    public Token RefreshToken(string refreshToken, string clientSecret)
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Proxy = null;
        //            wc.Headers.Add("Authorization",
        //                "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientId + ":" + clientSecret)));
        //            NameValueCollection col = new NameValueCollection
        //        {
        //            {"grant_type", "refresh_token"},
        //            {"refresh_token", refreshToken}
        //        };

        //            String response;
        //            try
        //            {
        //                byte[] data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
        //                response = Encoding.UTF8.GetString(data);
        //            }
        //            catch (WebException e)
        //            {
        //                using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
        //                {
        //                    response = reader.ReadToEnd();
        //                }
        //            }
        //            return JsonConvert.DeserializeObject<Token>(response);
        //        }
        //    }

        //    private String GetUri()
        //    {
        //        StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize/?");
        //        builder.Append("client_id=" + ClientId);
        //        builder.Append("&response_type=code");
        //        builder.Append("&redirect_uri=" + RedirectUri);
        //        builder.Append("&state=" + State);
        //        builder.Append("&scope=" + Scope.GetStringAttribute(" "));
        //        builder.Append("&show_dialog=" + ShowDialog);
        //        return builder.ToString();
        //    }

        //    /// <summary>
        //    ///     Start the internal HTTP-Server
        //    /// </summary>
        //    public void StartHttpServer(int port = 80)
        //    {
        //        _httpServer = new SimpleHttpServer(port, AuthType.Authorization);
        //        _httpServer.OnAuth += HttpServerOnOnAuth;

        //        _httpThread = new Thread(_httpServer.Listen);
        //        _httpThread.Start();
        //    }

        //    private void HttpServerOnOnAuth(AuthEventArgs e)
        //    {
        //        OnResponseReceivedEvent?.Invoke(new AutorizationCodeAuthResponse()
        //        {
        //            Code = e.Code,
        //            State = e.State,
        //            Error = e.Error
        //        });
        //    }

        //    /// <summary>
        //    ///     This will stop the internal HTTP-Server (Should be called after you got the Token)
        //    /// </summary>
        //    public void StopHttpServer()
        //    {
        //        _httpServer = null;
        //    }

        //    /// <summary>
        //    ///     Exchange a code for a Token (Don't use this if you're on a client)
        //    /// </summary>
        //    /// <param name="34392dbf46d04a94b778de26f2324472">The gathered code from the response</param>
        //    /// <param name="7e940a33ba274f5a88fe9ef7934b74d4">Your Client-Secret, don't provide it if this is running on a client!</param>
        //    /// <returns></returns>
        //    public Token ExchangeAuthCode(String code, String clientSecret)
        //    {
        //        using (WebClient wc = new WebClient())
        //        {
        //            wc.Proxy = null;

        //            NameValueCollection col = new NameValueCollection
        //        {
        //            {"grant_type", "authorization_code"},
        //            {"code", code},
        //            {"redirect_uri", RedirectUri},
        //            {"client_id", "34392dbf46d04a94b778de26f2324472"},//ClientId
        //            {"client_secret", "7e940a33ba274f5a88fe9ef7934b74d4"}//clientSecret
        //        };

        //            String response;
        //            try
        //            {
        //                byte[] data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
        //                response = Encoding.UTF8.GetString(data);
        //            }
        //            catch (WebException e)
        //            {
        //                using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
        //                {
        //                    response = reader.ReadToEnd();
        //                }
        //            }
        //            return JsonConvert.DeserializeObject<Token>(response);
        //        }
        //    }
        //}

        //public struct AutorizationCodeAuthResponse
        //{
        //    public String Code { get; set; }
        //    public String State { get; set; }
        //    public String Error { get; set; }
        //}

        public static int GetTrackNo(string Artist, string Album, string Title)
        {
            //txt_Track_No.Text = (GetTrackNo(txt_Artist.Text, txt_Album.Text, txt_Title.Text)).ToString();
            string uriString = "https://api.spotify.com/v1/search";// "?q=panick%20switch&type=track
            string keywordString = "";

            if (Artist != "" && Album != "" && Title != "") keywordString = "album%3A" + Album.Replace(" ", " +").ToLower() + "+artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Album == "" && Artist != "" && Title != "") keywordString = "artist%3A" + Artist.Replace(" ", " +").ToLower() + "+" + Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            if (Artist == "" && Album == "" && Title != "") keywordString = Title.Replace(" ", "+").ToLower() + "&offset=0&limit=20&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            //txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + 
            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("query", keywordString);
            var a1 = ""; var ab = "";
            var albump = 0;
            var artistp = 0;
            var tracknop = 0;
            try
            {
                webClient.QueryString.Add(nameValueCollection);
                var aa = (webClient.DownloadString(uriString));
                ab = aa;
                //if ( (aa.ToLower()).IndexOf(", <b>"+ txt_Title.Text.ToLower() ) >0 ) a1 = aa.Substring(((aa.ToLower()).IndexOf(", <b>"+txt_Title.Text.ToLower())) -1, 1);//<b>+ txt_Title.Text
                albump = (aa.ToLower()).IndexOf(Album.ToLower());
                if (albump > 0) aa = aa.Substring(albump, aa.Length - albump);
                artistp = (aa.ToLower()).IndexOf(Artist.ToLower());
                if (artistp > 0) aa = aa.Substring(artistp, aa.Length - artistp);
                tracknop = (aa.ToLower()).IndexOf("track_number");
                if (tracknop > 0) a1 = aa.Substring(tracknop + 15, 2);//<b>+ txt_Title.Text

                //if (IsNumbers(a1)) return a1.ToInt32();
                //else return 0;
                //var a2 = aa.Substring(((aa.ToLower()).IndexOf("track_number")) + 15, 2);
                //var a3 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 7, 1);
                //var a4 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 8, 1) + aa;

                //DLCManager.
                //txt_debug.Text += keywordString + "\n" + a1 + "\n---------------------------------\n " + ab;// + "\n " + a3 + "\n " + a4;
            }
            catch (Exception ee)
            {
                //MessageBox.Show("FAILED To get track" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }
            a1 = a1.Trim();
            if (a1 == "" && Album != "")
            {
                a1 = GetTrackNo(Artist, "", Title).ToString();
            }
            if (a1 == "" && Artist != "")
            {
                a1 = GetTrackNo("", "", Title).ToString();
            }
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
            //return a1.Trim().ToInt32();
        }


        public int GetTrackNo1(string Artist, string Album, string Title)
        {
            //txt_Track_No.Text = (GetTrackNo(txt_Artist.Text, txt_Album.Text, txt_Title.Text)).ToString();
            string uriString = "https://api.spotify.com/v1/search";// "?q=panick%20switch&type=track
            string keywordString = Title.Replace(" ", "%20").ToLower() + "&type=track"; //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 
            //txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + 
            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", keywordString);
            var a1 = ""; var ab = "";
            var albump = 0;
            var artistp = 0;
            var tracknop = 0;
            try
            {
                webClient.QueryString.Add(nameValueCollection);
                var aa = (webClient.DownloadString(uriString));
                ab = aa;
                //if ( (aa.ToLower()).IndexOf(", <b>"+ txt_Title.Text.ToLower() ) >0 ) a1 = aa.Substring(((aa.ToLower()).IndexOf(", <b>"+txt_Title.Text.ToLower())) -1, 1);//<b>+ txt_Title.Text
                albump = (aa.ToLower()).IndexOf(Album.ToLower());
                if (albump > 0) aa = aa.Substring(albump, aa.Length - albump);
                artistp = (aa.ToLower()).IndexOf(Artist.ToLower());
                if (artistp > 0) aa = aa.Substring(artistp, aa.Length - artistp);
                tracknop = (aa.ToLower()).IndexOf("track_number");
                if (tracknop > 0) a1 = aa.Substring(tracknop + 15, 2);//<b>+ txt_Title.Text

                //if (IsNumbers(a1)) return a1.ToInt32();
                //else return 0;
                //var a2 = aa.Substring(((aa.ToLower()).IndexOf("track_number")) + 15, 2);
                //var a3 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 7, 1);
                //var a4 = aa.Substring(((aa.ToLower()).IndexOf("track")) + 8, 1) + aa;

                //DLCManager.
                txt_debug.Text += keywordString + "s\n" + a1 + "\n---------------------------------\n " + ab;// + "\n " + a3 + "\n " + a4;
            }
            catch (Exception ee)
            {
                MessageBox.Show("FAILED To get track" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }
            a1 = a1.Trim();
            if (a1 == "" && Album != "")
            {
                a1 = GetTrackNo1(Artist, "", Title).ToString();
            }
            if (a1 == "" && Artist != "")
            {
                a1 = GetTrackNo1("", "", Title).ToString();
            }
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;
            //return a1.Trim().ToInt32();
        }

        public static int GetTrackNo_old(string Artist, string Album, string Title)
        {


            //StartHttpServer();
            //_auth.StartHttpServer(8000);
            //_auth.DoAuth();
            var a1 = "";
            try
            {

                string uriString = "http://ws.spotify.com/search/1/track";// "http://www.google.com/search";
                string keywordString = ("\"" + Artist + "\" \"" + Album + "\" \"" + Title + "\"").Replace("&", "%26"); //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

                WebClient webClient = new WebClient();

                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add("q", keywordString);

                webClient.QueryString.Add(nameValueCollection);

                var aa = (webClient.DownloadString(uriString));

                //if ( (aa.ToLower()).IndexOf(", <b>"+ txt_Title.Text.ToLower() ) >0 ) a1 = aa.Substring(((aa.ToLower()).IndexOf(", <b>"+txt_Title.Text.ToLower())) -1, 1);//<b>+ txt_Title.Text
                //GetTrkTxt=aa;
                if ((aa.ToLower()).IndexOf("<track-number>") > 0) a1 = aa.Substring(((aa.ToLower()).IndexOf("<track-number>")) + 14, 2);//<b>+ txt_Title.Text
                a1 = a1.Replace("<", "");
            }
            catch (Exception ee)
            {
                //MessageBox.Show("FAILED To get track" + ee.Message + "----");
                Console.WriteLine(ee.Message);
            }

            if (a1 == "" && Album != "")
            {
                a1 = GetTrackNo(Artist, "", Title).ToString();
            }
            if (a1 == "" && Artist != "")
            {
                a1 = GetTrackNo("", "", Title).ToString();
            }
            if (IsNumbers(a1)) return a1.ToInt32();
            else return 0;


        }

        public static Boolean IsNumbers(String value)
        {
            return value.All(Char.IsDigit);
        }

        public static string IndexOfTest(string strSource)
        {
            //string strSource = "This is the string which we will perform the search on";

            Console.WriteLine("The search string is:{0}\"{1}\"{0}", Environment.NewLine, strSource);

            string strTarget = "";
            int found = 0;
            int totFinds = 0;

            do
            {
                Console.Write("Please enter a search value to look for in the above string (hit Enter to exit) ==> ");

                strTarget = Console.ReadLine();

                if (strTarget != "")
                {

                    for (int i = 0; i < strSource.Length; i++)
                    {

                        found = strSource.IndexOf(strTarget, i);

                        if (found >= 0)
                        {
                            totFinds++;
                            i = found;
                        }
                        else
                            break;
                    }
                }
                else
                    return "-";

                Console.WriteLine("{0}The search parameter '{1}' was found {2} times.{0}",
                        Environment.NewLine, strTarget, totFinds);

                totFinds = 0;

            } while (true);
        }

        private void bth_GetTrackNo_Click(object sender, EventArgs e)
        {
            var CleanTitle = "";
            if (txt_Title.Text.IndexOf("[") > 0) CleanTitle = txt_Title.Text.Substring(0, txt_Title.Text.IndexOf("["));
            if (txt_Title.Text.IndexOf("]") > 0) CleanTitle += txt_Title.Text.Substring(txt_Title.Text.IndexOf("]"), txt_Title.Text.Length - txt_Title.Text.IndexOf("]"));
            else if (txt_Title.Text.IndexOf("[") == 0 || txt_Title.Text.Substring(0, 1) != "[") CleanTitle = txt_Title.Text;

            //string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
            string z = (GetTrackNo(txt_Artist.Text, txt_Album.Text, CleanTitle)).ToString();
            txt_Track_No.Text = z == "0" && txt_Track_No.Text != "" ? txt_Track_No.Text : z;
            var i = DataViewGrid.SelectedCells[0].RowIndex;

            if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "No"; }
            else { chbx_TrackNo.Checked = true; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "Yes"; }
        }

        private void txt_Track_No_TextChanged(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;

            if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "No"; }
            else { chbx_TrackNo.Checked = true; DataViewGrid.Rows[i].Cells["Has_Track_No"].Value = "Yes"; }
        }

        private void btn_OpenSongFolder_Click(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            ////dus.Tables[0].Rows[DataGridView1.SelectedCells[0].RowIndex].ItemArray[19].ToString();// files[0].Original_FileName;
            //try
            //{
            //    Process process = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show("Can not open Old Folder in Explorer ! ");
            //}
        }

        private void chbx_Alternate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_Youtube_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_YouTube_Link.Text);
        }

        private void btn_Playthrough_Click(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_Playthough.Text);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Process.Start("IExplore.exe", txt_CustomsForge_Link.Text);
        }

        private void chbx_MultiTrack_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_MultiTrack.Checked) txt_MultiTrackType.Enabled = true;
            else txt_MultiTrackType.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Standardization.MakeCover(DB_Path, txt_AlbumArtPath.Text, txt_Artist.Text, txt_Album.Text);
        }

        private void btn_AddSections_Click(object sender, EventArgs e)
        {
            var j = DataViewGrid.SelectedCells[0].RowIndex;
            var xx = Path.Combine(AppWD, "bpr_v0.3\\bpr.exe");// +" " + DataViewGrid.Rows[j].Cells[18].Value.ToString(); 
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + xx);
            }
        }

        private void btn_InvertSelect_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{

            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "Yes");
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "Yes");
                test = " or Beta";
            }
            command.CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
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
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();
            try
            {
                var com = "Select * FROM Main";
                com += " WHERE ID Not IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
                DataSet dhs = new DataSet();
                using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    MessageBox.Show("All NON Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected" + test);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                MessageBox.Show("Error at select inverted " + ee);
            }
        }

        private void btn_Copy_old_Click(object sender, EventArgs e)
        {
            pB_ReadDLCs.Value = 0;
            using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            {// 1. If hash already exists do not insert
                try
                {
                    var com = "SELECT * FROM Main";
                    com += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
                    DataSet dhs = new DataSet();

                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    noOfRec = dhs.Tables[0].Rows.Count;
                    var dest = "";
                    pB_ReadDLCs.Maximum = noOfRec;
                    for (var i = 0; i <= noOfRec - 1; i++)
                    {
                        string filePath = TempPath + "\\0_old\\" + dhs.Tables[0].Rows[i].ItemArray[19];//Original_FileName
                        dest = RocksmithDLCPath + "\\" + dhs.Tables[0].Rows[i].ItemArray[19];//..\\
                        var eef = dhs.Tables[0].Rows[i].ItemArray[87].ToString();
                        if (eef == "Yes")//OLd available
                        {
                            try
                            {
                                File.Copy(filePath, dest, true);
                            }
                            catch (System.IO.FileNotFoundException ee)
                            {
                                Console.WriteLine(ee.Message);
                                MessageBox.Show(filePath + "----" + dest + "Error at copy OLD " + ee);
                            }
                        }
                        pB_ReadDLCs.Value++;
                    }
                    MessageBox.Show(noOfRec + " Files Copied to " + RocksmithDLCPath + "\\");
                }

                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    MessageBox.Show("Error at copy OLD " + ee);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{

            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "No");
            var test = "";
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "No");
                test = " or Beta";
            }
            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";

            try
            {
                command.CommandType = CommandType.Text;
                cnn.Open();
                command.ExecuteNonQuery();
                //cnn.Close();
                //command.Dispose();
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

            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
            command.Parameters.AddWithValue("@param8", "Yes");
            if (chbx_InclBeta.Checked)
            {
                command.CommandText += ",Is_Beta = @param9 ";
                command.Parameters.AddWithValue("@param9", "Yes");
            }

            command.CommandText += " WHERE not ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
            try
            {
                //command.CommandType = CommandType.Text;
                //cnn.Open();
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
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();
            try
            {
                var com = "Select * FROM Main";
                com += " WHERE ID Not IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
                DataSet dhs = new DataSet();
                using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    MessageBox.Show("All NON Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Selected" + test);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                MessageBox.Show("Error at select inverted " + ee);
            }
        }

        private void txt_Artist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_SearchReset.Text = "Start Search";
                btn_Search.PerformClick();
            }
        }

        private void btn_Beta_Click(object sender, EventArgs e)
        {
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Is_Beta = @param8 ";
            command.CommandText += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
            command.Parameters.AddWithValue("@param8", "Yes");
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
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            DataViewGrid.Refresh();
            try
            {
                var com = "Select * FROM Main";
                com += " WHERE ID IN (" + SearchCmd.Replace("*", "ID").Replace(";", "") + ")";
                DataSet dhs = new DataSet();
                using (OleDbConnection cBn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {// 1. If hash already exists do not insert
                    OleDbDataAdapter dBs = new OleDbDataAdapter(com, cBn);
                    dBs.Fill(dhs, "Main");
                    dBs.Dispose();
                    MessageBox.Show("All Filtered songs(" + dhs.Tables[0].Rows.Count + ") in DB have been marked as Beta");
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                MessageBox.Show("Error at select filtered " + ee);
            }
        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            eof();
        }

        public void eof()
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            var xx = Path.Combine(AppWD, "eof1.8RC10(r1337)\\eof.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");// Path.GetDirectoryName();
            startInfo.Arguments = String.Format(" \"" + txt_OggPath.Text+"\"");
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
            try
            {
                Process process = Process.Start(@xx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + DB_Path);
            }
        }

        private void btn_CreateLyrics_Click(object sender, EventArgs e)
        {
            //1. Open Internet Explorer

            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string link = "https://www.google.com/#q=" + DataViewGrid.Rows[i].Cells["Artist"].Value.ToString() + "+" + DataViewGrid.Rows[i].Cells["Song_Title"].Value.ToString() + "+" + "Lyrics";
            MessageBox.Show("1. Google the Lyrics e.g."+ link + " \n2. Use Ultrastar Creator Tab lyrics to the songs time signature\n3. Using EditorOnFire Transform tabbed lyrics to Rocksmith Format(Import - Adjust and Save)\n4. When done press Import lyrics by using Change Lyrics button", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);


            try
            {
                Process process = Process.Start(@link);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            //2. Open Song Folder
            string filePath = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();

            try
            {
                Process process = Process.Start(@filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Song Folder in Exporer ! ");
            }

            //3. Open Ultrastar pointing at the Song Ogg
            //var DB_Path = (chbx_DefaultDB.Checked == true ? MyAppWD : txt_DBFolder.Text) + "\\Files.accdb";
            var xx = Path.Combine(AppWD, "UltraStar Creator\\usc.exe");

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = xx;
            startInfo.WorkingDirectory = AppWD.Replace("external_tools", "");// Path.GetDirectoryName();
            startInfo.Arguments = String.Format(" \"" + txt_OggPath.Text+"\"");
            startInfo.UseShellExecute = false; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(xx) && File.Exists(DB_Path))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1);
                }
            //try
            //{
            //    Process process = Process.Start(@xx);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //MessageBox.Show("Can not open External tool for phase beats and section fixes ! " + DB_Path);
            //}

            //4. Open EoF
            eof();

            //5. Import PART VOCALS_RS2.xml

        }

        private void btn_GroupsAdd_Click(object sender, EventArgs e)
        {
            var cmd = "INSERT into Groups (CDLC_ID, Groups, Type) VALUES (\"" + txt_ID.Text + "\",\"" + chbx_Group.Text + "\",\"DLC\");";
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
            var cmd = "DELETE FROM Groups WHERE Type=\"DLC\" AND Groups= \"" + chbx_Group.Text + "\"";
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

        private void chbx_AllGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            GroupChanged = true;
        }

        private void chbx_Group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            //rtxt_StatisticsOnReadDLCs.Text = "Trying to add preview as missing.\n" + rtxt_StatisticsOnReadDLCs.Text;
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_OggPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            var tt = t.Replace(".ogg", ".wav");
            startInfo.Arguments = String.Format(" -w \"{0}\" \"{1}\"",
                                                tt, t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    if (DDC.ExitCode == 0)
                    {
                        //var i = DataViewGrid.SelectedCells[0].RowIndex;
                        string filePath = tt.Substring(0, tt.LastIndexOf("\\")); ;// DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();
                        try
                        {
                            Process process = Process.Start(@filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("Can not open Garagebnad ready Song Folder in Exporer ! ");
                        }
                    }
                }
        }

        private void btn_Artist2SortA_Click(object sender, EventArgs e)
        {
            txt_Artist_Sort.Text = txt_Artist.Text;
        }

        private void btn_Title2SortT_Click(object sender, EventArgs e)
        {
            txt_Title_Sort.Text = txt_Title.Text;
        }

        private void chbx_Alternate_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbx_Alternate.Checked)
            {
                txt_Alt_No.Enabled = true;
                txt_Alt_No.Value = 1;
            }
            else
            {
                //txt_Alt_No.Value = 0;
                txt_Alt_No.Enabled = false;

            }
        }

        private void btn_ChangeLyrics_Click(object sender, EventArgs e)
        {
            var temppath = String.Empty;
            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Select you NEW Lyric XML file";
                fbd.Filter = "Rocksmith 2014 Lyrics file (*.xml)|*.xml";
                var i = DataViewGrid.SelectedCells[0].RowIndex;
                fbd.InitialDirectory = DataViewGrid.Rows[i].Cells["Folder_Name"].Value.ToString();
                fbd.Multiselect = false;
                //fbd.FileOk += OpenFileDialog_FileLimit; // Event handler
                if (fbd.ShowDialog() != DialogResult.OK)
                    return;
                temppath = fbd.FileName;

                using (OleDbConnection cnn7 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    var sql = "";
                    if (chbx_Lyrics.Checked)
                        sql = "UPDATE Arrangements SET XMLFilePath=\"" + temppath + "\" WHERE ArrangementType=\"Vocal\" AND CDLC_ID=" + txt_ID.Text + ";";
                    else
                        sql = "INSERT INTO Arrangements (Arrangement_Name,CDLC_ID, ArrangementType, Bonus, ArrangementSort, TuningPitch, RouteMask, Has_Sections, XMLFilePath) VALUES(\"4\", " + txt_ID.Text + ", \"Vocal\", \"false\", \"0\", \"0\", \"None\",\"No\",\""+ temppath+"\")";

                    OleDbDataAdapter dag = new OleDbDataAdapter(sql, cnn7); //WHERE id=253
                    DataSet dsr = new DataSet();
                    dag.Fill(dsr, "Main");
                    txt_Lyrics.Text = temppath;
                    chbx_Lyrics.Checked = true;
                    SaveRecord();
                }
            }
        }
    }
}