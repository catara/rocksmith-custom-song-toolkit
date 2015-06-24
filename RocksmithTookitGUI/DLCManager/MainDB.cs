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
//using RocksmithToolkitGUI.OggConverter;//convert ogg to wem
using RocksmithToolkitLib.Ogg;//convert ogg to wem
using RocksmithToolkitLib.Extensions;
using System.IO;
using Ookii.Dialogs; //cue text
using System.Net; //4ftp
using RocksmithToolkitLib.DLCPackage; //4packing
using RocksmithToolkitLib;//4REPACKING
using RocksmithToolkitGUI.DLCManager;//4 using then
using System.Collections;//webparsing
using System.Collections.Specialized;//webparsing
using System.Security.Cryptography; //For File hash
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
        internal static string AppWD = AppDomain.CurrentDomain.BaseDirectory; //when repacking
        public bool SaveOK = false;
        DLCPackageData data;

        private BindingSource Main = new BindingSource();
        private const string MESSAGEBOX_CAPTION = "MainDB";
        //private object cbx_Lead;
        //public DataAccess da = new DataAccess();
        //bcapi
        public string DB_Path = "";
        public string TempPath = "";
        public string RocksmithDLCPath = "";
        public DataSet dssx = new DataSet();
        public DataSet dssx2 = new DataSet();
        public int noOfRec = 0;
        public string SearchCmd = "";
        public bool updateDBb = false;
        public bool AllowORIGDeleteb = false;
        public bool AllowEncriptb = false;
        //public OleDbDataAdapter dax = new OleDbDataAdapter(cmd, cnn);

        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void MainDB_Load(object sender, EventArgs e)
        {
            //DataAccess da = new DataAccess();
            //MessageBox.Show("test0");
            SearchCmd = "SELECT * FROM Main ORDER BY Artist, Album_Year, Album, Song_Title;";
            Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
            DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
            btn_Search.Enabled = false;
            //MessageBox.Show(updateDBb.ToString());
            if (lbl_NoRec.Text != "0 records." && noOfRec>0)
                if (!Directory.Exists(DataViewGrid.Rows[0].Cells[22].Value.ToString()) || updateDBb)
                {
                    var tmpp = "\\ORIG"; var OLD_Path = ""; var cmd = "";
                    for (var h = 0; h < 2; h++)
                    {
                        try
                        {
                            using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                            {
                                SearchCmd = "SELECT top 1 AlbumArtPath, audioPreviewPath, OggPath, oggPreviewPath FROM Main WHERE AudioPath LIKE '%" + tmpp + "%' AND audioPreviewPath is not null and oggPreviewPath is not null;";
                                DataSet duk = new DataSet();
                                OleDbDataAdapter dal = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
                                dal.Fill(duk, "Main");

                                //dax.Dispose();
                                OLD_Path = duk.Tables[0].Rows[0].ItemArray[0].ToString().Substring(0, duk.Tables[0].Rows[0].ItemArray[0].ToString().IndexOf(tmpp)) + tmpp;
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
            var line = -1;
            line = DataViewGrid.SelectedCells[0].RowIndex;
            if (line > -1) ChangeRow();
        }

        public void ChangeRow()
        {

            int i;
            if (DataViewGrid.SelectedCells.Count > 0)
            {

                DataSet ds = new DataSet();
                using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    string SearchCmd = "SELECT DISTINCT Groups FROM Main;";
                    OleDbDataAdapter da = new OleDbDataAdapter(SearchCmd, cnn); //WHERE id=253
                    da.Fill(ds, "Main");
                    var norec = ds.Tables[0].Rows.Count;

                    if (norec > 0)
                    {
                        //remove items
                        if (chbx_Group.Items.Count > 0)
                        {
                            chbx_Group.DataSource = null;
                            for (int k = chbx_Group.Items.Count - 1; k >= 0; --k)
                            {
                                if (!chbx_Group.Items[k].ToString().Contains("--"))
                                {
                                    chbx_Group.Items.RemoveAt(k);
                                }
                            }
                        }
                        //add items
                        for (int j = 0; j < norec; j++)
                            chbx_Group.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    chbx_Group.DataSource = ds.Tables[0];
                        //    chbx_Group.DataTextField = "company_name";
                        //    chbx_Group.DataValueField = "Company_ID";
                        //    chbx_Group.DataBind();
                        //    chbx_Group.Items.Insert(0, new ListItem("--Select--", "0"));
                        //}
                    }


                i = DataViewGrid.SelectedCells[0].RowIndex;
                txt_ID.Text = DataViewGrid.Rows[i].Cells[0].Value.ToString();
                txt_Title.Text = DataViewGrid.Rows[i].Cells[1].Value.ToString();
                txt_Title_Sort.Text = DataViewGrid.Rows[i].Cells[2].Value.ToString();
                txt_Album.Text = DataViewGrid.Rows[i].Cells[3].Value.ToString();
                txt_Artist.Text = DataViewGrid.Rows[i].Cells[4].Value.ToString();
                txt_Artist_Sort.Text = DataViewGrid.Rows[i].Cells[5].Value.ToString();
                txt_Album_Year.Text = DataViewGrid.Rows[i].Cells[6].Value.ToString();
                txt_AverageTempo.Text = DataViewGrid.Rows[i].Cells[7].Value.ToString();
                txt_Volume.Text = (DataViewGrid.Rows[i].Cells[8].Value.ToString());
                txt_Preview_Volume.Text = (DataViewGrid.Rows[i].Cells[9].Value.ToString());
                txt_AlbumArtPath.Text = DataViewGrid.Rows[i].Cells[10].Value.ToString();
                txt_Track_No.Text = DataViewGrid.Rows[i].Cells[13].Value.ToString();
                txt_Author.Text = DataViewGrid.Rows[i].Cells[14].Value.ToString();
                txt_Version.Text = DataViewGrid.Rows[i].Cells[15].Value.ToString();
                txt_DLC_ID.Text = DataViewGrid.Rows[i].Cells[16].Value.ToString();
                txt_APP_ID.Text = DataViewGrid.Rows[i].Cells[17].Value.ToString();//SelectedIndex
                txt_MultiTrackType.Text = DataViewGrid.Rows[i].Cells[32].Value.ToString();// != "" ? txt_MultiTrackType.FindString(DataViewGrid.Rows[i].Cells[32].Value.ToString()) : 5;
                txt_Alt_No.Text = DataViewGrid.Rows[i].Cells[33].Value.ToString();
                txt_Tuning.Text = DataViewGrid.Rows[i].Cells[47].Value.ToString();
                txt_BassPicking.Text = DataViewGrid.Rows[i].Cells[48].Value.ToString();
                chbx_Group.Text = DataViewGrid.Rows[i].Cells[50].Value.ToString();
                txt_Rating.Text = DataViewGrid.Rows[i].Cells[51].Value.ToString();
                txt_Description.Text = DataViewGrid.Rows[i].Cells[52].Value.ToString();
                txt_Platform.Text = DataViewGrid.Rows[i].Cells[55].Value.ToString();
                if (DataViewGrid.Rows[i].Cells[56].Value.ToString() == "") txt_PreviewStart.Value = Convert.ToDateTime("00:00");
                else txt_PreviewStart.Value = Convert.ToDateTime("00:"+DataViewGrid.Rows[i].Cells[56].Value.ToString());
                if (DataViewGrid.Rows[i].Cells[57].Value.ToString() == "") txt_PreviewEnd.Value = 30;
                    else txt_PreviewEnd.Text = DataViewGrid.Rows[i].Cells[57].Value.ToString();
                txt_YouTube_Link.Text = DataViewGrid.Rows[i].Cells[70].Value.ToString();
                btn_Youtube.Enabled = DataViewGrid.Rows[i].Cells[70].Value.ToString() == "" ? false : true;
                txt_CustomsForge_Link.Text = DataViewGrid.Rows[i].Cells[71].Value.ToString();
                btn_CustomForge_Link.Enabled = DataViewGrid.Rows[i].Cells[71].Value.ToString() == "" ? false : true;
                txt_CustomsForge_Like.Text = DataViewGrid.Rows[i].Cells[72].Value.ToString();
                //txt_CustomsForge_Like.Enabled = DataViewGrid.Rows[i].Cells[72].Value.ToString() == "" ? false : true;
                txt_CustomsForge_ReleaseNotes.Text = DataViewGrid.Rows[i].Cells[73].Value.ToString();
                //txt_CustomsForge_ReleaseNotes.Enabled = DataViewGrid.Rows[i].Cells[73].Value.ToString() == "" ? false : true;

                txt_OggPath.Text = DataViewGrid.Rows[i].Cells[77].Value.ToString();//.Replace(".ogg", "_fixed.ogg");
                txt_OggPreviewPath.Text = DataViewGrid.Rows[i].Cells[78].Value.ToString();//.Replace(".ogg", "_fixed.ogg");

                txt_Artist_ShortName.Text = DataViewGrid.Rows[i].Cells[85].Value.ToString();
                txt_Album_ShortName.Text = DataViewGrid.Rows[i].Cells[86].Value.ToString();

                //txt_Volume.Text = DataGridView1.Rows[i].Cells[86].Value.ToString();
                //txt_Preview_Volume.Text = DataGridView1.Rows[i].Cells[9].Value.ToString();

                if (DataViewGrid.Rows[i].Cells[26].Value.ToString() == "Yes") chbx_Original.Checked = true;
                else chbx_Original.Checked = false;
                if (DataViewGrid.Rows[i].Cells[28].Value.ToString() == "Yes") chbx_Beta.Checked = true;
                else chbx_Beta.Checked = false;
                if (DataViewGrid.Rows[i].Cells[29].Value.ToString() == "Yes") {chbx_Alternate.Checked = true;txt_Alt_No.Enabled = true; }
                else { chbx_Alternate.Checked = false; txt_Alt_No.Enabled = false; }
                if (DataViewGrid.Rows[i].Cells[30].Value.ToString() == "Yes") { chbx_MultiTrack.Checked = true; txt_MultiTrackType.Enabled = true; }
                else {chbx_MultiTrack.Checked = false;txt_MultiTrackType.Enabled = false; }
                if (DataViewGrid.Rows[i].Cells[31].Value.ToString() == "Yes") chbx_Broken.Checked = true;
                else chbx_Broken.Checked = false;
                if (DataViewGrid.Rows[i].Cells[35].Value.ToString() == "Yes") chbx_Bass.Checked = true;
                else chbx_Bass.Checked = false;
                if (DataViewGrid.Rows[i].Cells[37].Value.ToString() == "Yes") chbx_Lead.Checked = true;
                else chbx_Lead.Checked = false;
                if (DataViewGrid.Rows[i].Cells[39].Value.ToString() == "Yes") chbx_Combo.Checked = true;
                else chbx_Combo.Checked = false;
                if (DataViewGrid.Rows[i].Cells[38].Value.ToString() == "Yes") chbx_Rhythm.Checked = true;
                else chbx_Rhythm.Checked = false;
                if (DataViewGrid.Rows[i].Cells[40].Value.ToString() == "Yes") chbx_Lyrics.Checked = true;
                else chbx_Lyrics.Checked = false;
                if (DataViewGrid.Rows[i].Cells[41].Value.ToString() == "Yes") chbx_Sections.Checked = true;
                else chbx_Sections.Checked = false;
                if (DataViewGrid.Rows[i].Cells[42].Value.ToString() == "Yes") chbx_Cover.Checked = true;
                else chbx_Cover.Checked = false;
                if (DataViewGrid.Rows[i].Cells[43].Value.ToString() == "Yes") { chbx_Preview.Checked = true; btn_PlayPreview.Enabled = true; }
                else { chbx_Preview.Checked = false; btn_PlayPreview.Enabled = false; }
                if (DataViewGrid.Rows[i].Cells[45].Value.ToString() == "Yes") { numericUpDown1.Enabled=false;chbx_DD.Checked = true; btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false; }
                else { chbx_DD.Checked = false; btn_AddDD.Enabled = true; btn_RemoveDD.Enabled = false; } //numericUpDown1.Enabled = true;
                if (DataViewGrid.Rows[i].Cells[69].Value.ToString() == "Yes") chbx_Selected.Checked = true;
                else chbx_Selected.Checked = false;
                if (DataViewGrid.Rows[i].Cells[76].Value.ToString() == "Yes") chbx_Author.Checked = true;
                else chbx_Author.Checked = false;
                if (DataViewGrid.Rows[i].Cells[83].Value.ToString() == "Yes") { chbx_BassDD.Checked = true; btn_RemoveBassDD.Enabled = true; chbx_RemoveBassDD.Enabled = true; }
                else { chbx_BassDD.Checked = false; btn_RemoveBassDD.Enabled = false; chbx_RemoveBassDD.Enabled = false; txt_BassPicking.Text = ""; }
                if (DataViewGrid.Rows[i].Cells[84].Value.ToString() == "Yes") chbx_Bonus.Checked = true;
                else chbx_Bonus.Checked = false;
                chbx_Avail_Old.Checked = false;
                if (DataViewGrid.Rows[i].Cells[87].Value.ToString() == "Yes") { chbx_Avail_Old.Checked = true; btn_OldFolder.Enabled = true; }
                else { chbx_Avail_Old.Checked = false; btn_OldFolder.Enabled = false; }
                chbx_Avail_Duplicate.Checked = false;
                if (DataViewGrid.Rows[i].Cells[88].Value.ToString() == "Yes") { chbx_Avail_Duplicate.Checked = true; btn_DuplicateFolder.Enabled = true; }
                else { chbx_Avail_Duplicate.Checked = false; btn_DuplicateFolder.Enabled = false; }
                if (DataViewGrid.Rows[i].Cells[89].Value.ToString() == "Yes") chbx_Has_Been_Corrected.Checked = true;
                else chbx_Has_Been_Corrected.Checked = false;


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
                //Defaults
                txt_FTPPath.Text = ConfigRepository.Instance()["dlcm_FTP"];;
                txt_PreviewStart.Value.AddMinutes(10);
                if (ConfigRepository.Instance()["dlcm_RemoveBassDD"]=="Yes") chbx_RemoveBassDD.Checked=true;
                else chbx_RemoveBassDD.Checked=false;
                if (ConfigRepository.Instance()["dlcm_UniqueID"] == "Yes") chbx_UniqueID.Checked = true;
                else chbx_UniqueID.Checked = false;
                if (ConfigRepository.Instance()["dlcm_andCopy"] == "Yes") chbx_Copy.Checked = true;
                else chbx_Copy.Checked = false;
                chbx_Format.Text = (ConfigRepository.Instance()["dlcm_chbx_PC"] == "Yes") ? "PC" : (ConfigRepository.Instance()["dlcm_chbx_Mac"] == "Yes") ? "Mac" : (ConfigRepository.Instance()["dlcm_chbx_PS3"] == "Yes") ? "PS3" : (ConfigRepository.Instance()["dlcm_chbx_XBOX360"] == "Yes") ? "XBOX360" : "";




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

                DataViewGrid.Rows[i].Cells[1].Value = txt_Title.Text;
                DataViewGrid.Rows[i].Cells[2].Value = txt_Title_Sort.Text;
                DataViewGrid.Rows[i].Cells[3].Value = txt_Album.Text;
                DataViewGrid.Rows[i].Cells[4].Value = txt_Artist.Text;
                DataViewGrid.Rows[i].Cells[5].Value = txt_Artist_Sort.Text;
                DataViewGrid.Rows[i].Cells[6].Value = txt_Album_Year.Text;
                DataViewGrid.Rows[i].Cells[7].Value = txt_AverageTempo.Text;
                DataViewGrid.Rows[i].Cells[8].Value = txt_Volume.Text;
                DataViewGrid.Rows[i].Cells[9].Value = txt_Preview_Volume.Text;
                DataViewGrid.Rows[i].Cells[10].Value = txt_AlbumArtPath.Text;
                DataViewGrid.Rows[i].Cells[13].Value = txt_Track_No.Text;
                DataViewGrid.Rows[i].Cells[14].Value = txt_Author.Text;
                DataViewGrid.Rows[i].Cells[15].Value = txt_Version.Text;
                DataViewGrid.Rows[i].Cells[16].Value = txt_DLC_ID.Text;
                DataViewGrid.Rows[i].Cells[17].Value = txt_APP_ID.Text;
                DataViewGrid.Rows[i].Cells[32].Value = txt_MultiTrackType.Text ;
                DataViewGrid.Rows[i].Cells[33].Value = txt_Alt_No.Text;
                DataViewGrid.Rows[i].Cells[47].Value = txt_Tuning.Text;
                DataViewGrid.Rows[i].Cells[48].Value = txt_BassPicking.Text;
                DataViewGrid.Rows[i].Cells[50].Value = chbx_Group.Text;
                DataViewGrid.Rows[i].Cells[51].Value = txt_Rating.Text;
                DataViewGrid.Rows[i].Cells[52].Value = txt_Description.Text;
                DataViewGrid.Rows[i].Cells[55].Value = txt_Platform.Text;
                //commented out As i dont want to add a tinestamp altough the preview has not been generated with the Tool
                DataViewGrid.Rows[i].Cells[56].Value = txt_PreviewStart.Text;
                DataViewGrid.Rows[i].Cells[57].Value = txt_PreviewEnd.Text;
                DataViewGrid.Rows[i].Cells[70].Value = txt_YouTube_Link.Text;
                DataViewGrid.Rows[i].Cells[71].Value = txt_CustomsForge_Link.Text;
                DataViewGrid.Rows[i].Cells[72].Value = txt_CustomsForge_Like.Text;
                DataViewGrid.Rows[i].Cells[73].Value = txt_CustomsForge_ReleaseNotes.Text;
                DataViewGrid.Rows[i].Cells[78].Value = txt_OggPreviewPath.Text;
                DataViewGrid.Rows[i].Cells[85].Value = txt_Artist_ShortName.Text;
                DataViewGrid.Rows[i].Cells[86].Value = txt_Album_ShortName.Text;
                if (chbx_Original.Checked) DataViewGrid.Rows[i].Cells[26].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[26].Value = "No";
                if (chbx_Beta.Checked) DataViewGrid.Rows[i].Cells[28].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[28].Value = "No";
                if (chbx_Alternate.Checked) DataViewGrid.Rows[i].Cells[29].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[29].Value = "No";
                if (chbx_MultiTrack.Checked) DataViewGrid.Rows[i].Cells[30].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[30].Value = "No";
                if (chbx_Broken.Checked) DataViewGrid.Rows[i].Cells[31].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[31].Value = "No";
                if (chbx_Bass.Checked) DataViewGrid.Rows[i].Cells[35].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[35].Value = "No";
                if (chbx_Lead.Checked) DataViewGrid.Rows[i].Cells[37].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[37].Value = "No";
                if (chbx_Rhythm.Checked) DataViewGrid.Rows[i].Cells[38].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[38].Value = "No";
                if (chbx_Combo.Checked) DataViewGrid.Rows[i].Cells[39].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[39].Value = "No";
                if (chbx_Lyrics.Checked) DataViewGrid.Rows[i].Cells[40].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[40].Value = "No";
                if (chbx_Sections.Checked) DataViewGrid.Rows[i].Cells[41].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[41].Value = "No";
                if (chbx_Cover.Checked) DataViewGrid.Rows[i].Cells[42].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[42].Value = "No";
                if (chbx_Preview.Checked) DataViewGrid.Rows[i].Cells[43].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[43].Value = "No";
                if (chbx_DD.Checked) DataViewGrid.Rows[i].Cells[45].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[45].Value = "No";
                if (chbx_TrackNo.Checked) DataViewGrid.Rows[i].Cells[54].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[54].Value = "No";
                if (chbx_Selected.Checked) DataViewGrid.Rows[i].Cells[69].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[69].Value = "No";
                if (chbx_Author.Checked) DataViewGrid.Rows[i].Cells[76].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[76].Value = "No";
                if (chbx_BassDD.Checked) DataViewGrid.Rows[i].Cells[83].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[83].Value = "No";
                if (chbx_Bonus.Checked) DataViewGrid.Rows[i].Cells[84].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[84].Value = "No";
                if (chbx_Avail_Old.Checked) DataViewGrid.Rows[i].Cells[87].Value = "Yes";
                else DataViewGrid.Rows[i].Cells[87].Value = "No";
                //if (chbx_Avail_Duplicate.Checked) DataViewGrid.Rows[i].Cells[88].Value = "Yes";
                //else DataViewGrid.Rows[i].Cells[88].Value = "No";
                //if (chbx_Has_Been_Corrected.Checked) DataViewGrid.Rows[i].Cells[89].Value = "Yes";
                //else DataViewGrid.Rows[i].Cells[89].Value = "No";

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
                    //command.CommandText += "MultiTrackType = @param58, ";
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

                    command.Parameters.AddWithValue("@param1", DataViewGrid.Rows[i].Cells[1].Value.ToString());
                    command.Parameters.AddWithValue("@param2", DataViewGrid.Rows[i].Cells[2].Value.ToString());
                    command.Parameters.AddWithValue("@param3", DataViewGrid.Rows[i].Cells[3].Value.ToString());
                    command.Parameters.AddWithValue("@param4", DataViewGrid.Rows[i].Cells[4].Value.ToString());
                    command.Parameters.AddWithValue("@param5", DataViewGrid.Rows[i].Cells[5].Value.ToString());
                    command.Parameters.AddWithValue("@param6", DataViewGrid.Rows[i].Cells[6].Value.ToString());
                    command.Parameters.AddWithValue("@param7", DataViewGrid.Rows[i].Cells[7].Value.ToString());
                    command.Parameters.AddWithValue("@param8", DataViewGrid.Rows[i].Cells[8].Value.ToString());
                    command.Parameters.AddWithValue("@param9", DataViewGrid.Rows[i].Cells[9].Value.ToString());
                    command.Parameters.AddWithValue("@param10", DataViewGrid.Rows[i].Cells[10].Value.ToString());
                    command.Parameters.AddWithValue("@param12", DataViewGrid.Rows[i].Cells[12].Value.ToString());
                    command.Parameters.AddWithValue("@param13", DataViewGrid.Rows[i].Cells[13].Value.ToString());
                    command.Parameters.AddWithValue("@param14", DataViewGrid.Rows[i].Cells[14].Value.ToString());
                    command.Parameters.AddWithValue("@param15", DataViewGrid.Rows[i].Cells[15].Value.ToString());
                    command.Parameters.AddWithValue("@param16", DataViewGrid.Rows[i].Cells[16].Value.ToString());
                    command.Parameters.AddWithValue("@param17", DataViewGrid.Rows[i].Cells[17].Value.ToString());
                    command.Parameters.AddWithValue("@param26", DataViewGrid.Rows[i].Cells[26].Value.ToString());
                    command.Parameters.AddWithValue("@param28", DataViewGrid.Rows[i].Cells[28].Value.ToString());
                    command.Parameters.AddWithValue("@param29", DataViewGrid.Rows[i].Cells[29].Value.ToString());
                    command.Parameters.AddWithValue("@param30", DataViewGrid.Rows[i].Cells[30].Value.ToString());
                    command.Parameters.AddWithValue("@param31", DataViewGrid.Rows[i].Cells[31].Value.ToString());
                    command.Parameters.AddWithValue("@param32", DataViewGrid.Rows[i].Cells[32].Value.ToString());
                    command.Parameters.AddWithValue("@param33", DataViewGrid.Rows[i].Cells[33].Value.ToString());
                    command.Parameters.AddWithValue("@param40", DataViewGrid.Rows[i].Cells[40].Value.ToString());
                    command.Parameters.AddWithValue("@param41", DataViewGrid.Rows[i].Cells[41].Value.ToString());
                    command.Parameters.AddWithValue("@param42", DataViewGrid.Rows[i].Cells[42].Value.ToString());
                    command.Parameters.AddWithValue("@param43", DataViewGrid.Rows[i].Cells[43].Value.ToString());
                    command.Parameters.AddWithValue("@param45", DataViewGrid.Rows[i].Cells[45].Value.ToString());
                    command.Parameters.AddWithValue("@param47", DataViewGrid.Rows[i].Cells[47].Value.ToString());
                    command.Parameters.AddWithValue("@param48", DataViewGrid.Rows[i].Cells[48].Value.ToString());
                    command.Parameters.AddWithValue("@param49", DataViewGrid.Rows[i].Cells[49].Value.ToString());
                    command.Parameters.AddWithValue("@param50", DataViewGrid.Rows[i].Cells[50].Value.ToString());
                    command.Parameters.AddWithValue("@param51", DataViewGrid.Rows[i].Cells[51].Value.ToString());
                    command.Parameters.AddWithValue("@param52", DataViewGrid.Rows[i].Cells[52].Value.ToString());
                    command.Parameters.AddWithValue("@param54", DataViewGrid.Rows[i].Cells[54].Value.ToString());
                    command.Parameters.AddWithValue("@param56", DataViewGrid.Rows[i].Cells[56].Value.ToString());
                    command.Parameters.AddWithValue("@param57", DataViewGrid.Rows[i].Cells[57].Value.ToString());
                    //command.Parameters.AddWithValue("@param58", DataViewGrid.Rows[i].Cells[58].Value.ToString());
                    command.Parameters.AddWithValue("@param69", DataViewGrid.Rows[i].Cells[69].Value.ToString());
                    command.Parameters.AddWithValue("@param70", DataViewGrid.Rows[i].Cells[70].Value.ToString());
                    command.Parameters.AddWithValue("@param71", DataViewGrid.Rows[i].Cells[71].Value.ToString());
                    command.Parameters.AddWithValue("@param72", DataViewGrid.Rows[i].Cells[72].Value.ToString());
                    command.Parameters.AddWithValue("@param73", DataViewGrid.Rows[i].Cells[73].Value.ToString());
                    command.Parameters.AddWithValue("@param76", DataViewGrid.Rows[i].Cells[76].Value.ToString());
                    command.Parameters.AddWithValue("@param77", DataViewGrid.Rows[i].Cells[77].Value.ToString());
                    command.Parameters.AddWithValue("@param78", DataViewGrid.Rows[i].Cells[78].Value.ToString());
                    command.Parameters.AddWithValue("@param80", DataViewGrid.Rows[i].Cells[80].Value.ToString());
                    command.Parameters.AddWithValue("@param81", DataViewGrid.Rows[i].Cells[81].Value.ToString());
                    command.Parameters.AddWithValue("@param82", DataViewGrid.Rows[i].Cells[82].Value.ToString());
                    command.Parameters.AddWithValue("@param83", DataViewGrid.Rows[i].Cells[83].Value.ToString());
                    command.Parameters.AddWithValue("@param84", DataViewGrid.Rows[i].Cells[84].Value.ToString());
                    command.Parameters.AddWithValue("@param85", DataViewGrid.Rows[i].Cells[85].Value.ToString());
                    command.Parameters.AddWithValue("@param86", DataViewGrid.Rows[i].Cells[86].Value.ToString());
                    //command.Parameters.AddWithValue("@param87", DataViewGrid.Rows[i].Cells[87].Value.ToString());
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
                        MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Can not open Main DB connection in Edit Main screen ! " + DB_Path + "-" + command.CommandText);
                        throw;
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
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (txt_Artist.Text != "" || txt_Title.Text != "")
                try
                {
                    SearchCmd = "SELECT * FROM Main WHERE " + (txt_Artist.Text != "" ? " Artist Like '%" + txt_Artist.Text + "%'" : "") + (txt_Artist.Text != "" ? (txt_Title.Text != "" ? " AND " : "") : "") + (txt_Title.Text != "" ? " Song_Title Like '%" + txt_Title.Text + "%'" : "") + " ORDER BY Artist, Album_Year, Album, Song_Title ;";
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
            btn_Search.Enabled = false;
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
                    MessageBox.Show("DB Open in Design Mode or Download Connectivity patch @ https://www.microsoft.com/en-us/download/confirmation.aspx?id=23734");
                    return;
                }
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
                Groups,
                Rating,
                Description,
                Comments,
                Has_Track_No,
                Platform,
                PreviewTime,
                PreviewLenght,
                Temp,
                CustomForge_Followers,
                CustomForge_Version,
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
                Available_Old,                
                Available_Duplicate,
                Has_Been_Corrected
            }
            );
            bs.ResetBindings(false);
            dssx.Tables["Main"].AcceptChanges();
            bs.DataSource = dssx.Tables["Main"];
            DataGridView.AutoGenerateColumns = false;
            DataGridView.DataSource = null;
            DataGridView.DataSource = bs;
            DataGridView.AutoGenerateColumns = false;
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
            public string Has_BassDD { get; set; }
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
                        files[i].Has_BassDD = dataRow.ItemArray[83].ToString();
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
                DataViewGrid.Rows[i].Cells[80].Value = temppath.Replace(".png", ".dds");
                if (File.Exists(temppath))
                    using (FileStream fss = File.OpenRead(temppath))
                    {
                        SHA1 sha = new SHA1Managed();
                        DataViewGrid.Rows[i].Cells[80].Value = BitConverter.ToString(sha.ComputeHash(fss));
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
            btn_ChangeCover.Enabled = false;
            btn_Save.Enabled = false;

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
                Populate(ref DataViewGrid, ref Main);//, ref bsPositions, ref bsBadges);
                DataViewGrid.EditingControlShowing += DataGridView1_EditingControlShowing;
                DataViewGrid.Refresh();
                //btn_Search.Enabled = false;
            }
            btn_Search.Enabled = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_Filter_SelectedValueChanged(object sender, EventArgs e)
        {
            //No Cover
            //No Preview
            //No Vocals
            //No Section
            //No Bass
            //No Guitar
            //Original
            //CDLC
            //Selected
            //Beta
            //Broken
            //Alternate
            //With DD
            //No DD
            //No Bass DD
            //E Standard
            //Eb Standard
            //Drop D
            //Other Tunings
            //With Bonus

            //MessageBox.Show(cmb_Filter.Text.ToString() + SearchCmd);
            SearchCmd = "SELECT * FROM Main WHERE ";
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
                    SearchCmd += "Has_Track_No <> 'Yes'";
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
                //case "":
                //    SearchCmd = "";
                //    break;
                default:
                    break;
            }

            SearchCmd += " ORDER BY Artist, Album_Year, Album, Song_Title ;";
            //MessageBox.Show(Filtertxt + SearchCmd);
            try
            {
                //DataGridView1.Dispose();
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
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_OggPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p \"{0}\"", t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            //var outputBuilder = new StringBuilder();
            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo;
                    //DDC.OutputDataReceived += new DataReceivedEventHandler
                    //(
                    //    delegate(object senderd, DataReceivedEventArgs fe)
                    //    {
                    //        // append the new data to the data already read-in
                    //        outputBuilder.Append(fe.Data);
                    //    }
                    //);
                    DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
                    //DDC.BeginOutputReadLine();
                    //DDC.CancelOutputRead();

                    //// use the output
                    //txt_Description.Text = outputBuilder.ToString();
                    //if (DDC.ExitCode > 0) rtxt_StatisticsOnReadDLCs.Text = "Issues when packing rs1dlc DLC pack !" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                }
        }

        private void btm_PlayPreview_Click(object sender, EventArgs e)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "DLCManager\\oggdec.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
            var t = txt_OggPreviewPath.Text;//"C:\\GitHub\\tmp\\0\\0_dlcpacks\\rs1compatibilitydisc_PS3\\audio\\ps3\\149627248.ogg";//txt_TempPath.Text + "\\0_dlcpacks\\rs1compatibilitydlc.psarc";
            startInfo.Arguments = String.Format(" -p \"{0}\"",
                                                t);
            startInfo.UseShellExecute = true; startInfo.CreateNoWindow = true; //startInfo.RedirectStandardOutput = true; startInfo.RedirectStandardError = true;

            if (File.Exists(t))
                using (var DDC = new Process())
                {
                    DDC.StartInfo = startInfo; DDC.Start(); DDC.WaitForExit(1000 * 60 * 1); //wait 1min
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
            //DLCManager.

            //if (!bwRGenerate.IsBusy) //&& data != null&& norows > 0
            //{
            //bwRGenerate.RunWorkerAsync(data);
            //}
            //else
            //{

            //}
            pB_ReadDLCs.Value = 0;
            pB_ReadDLCs.Maximum = 3;
            pB_ReadDLCs.Increment(1);

            var i = DataViewGrid.SelectedCells[0].RowIndex;
            if (chbx_RemoveBassDD.Checked && chbx_BassDD.Checked)
            {
                var xmlFiles = Directory.GetFiles(DataViewGrid.Rows[i].Cells[22].Value.ToString(), "*.xml", SearchOption.AllDirectories);
                Platform platform = DataViewGrid.Rows[i].Cells[22].Value.ToString().GetPlatform();

                foreach (var xml in xmlFiles)
                    if (xml.IndexOf("bass") > 0 && !(xml.IndexOf(".old")>0))
                    //chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                    {
                        var bassRemoved = (DLCManager.RemoveDD(DataViewGrid.Rows[i].Cells[22].Value.ToString(), chbx_Original.Text, xml, platform, false, false) == "Yes") ? "No" : "Yes";
                    }
            }

            string h = GeneratePackage(txt_ID.Text);
            string copyftp = "";
            pB_ReadDLCs.Increment(1);
            if (chbx_Format.Text == "PS3" && chbx_Copy.Checked)
            {
                //var GameID = txt_FTPPath.Text.Substring(txt_FTPPath.Text.LastIndexOf("BL"), 9);
                //var startno = txt_FTPPath.Text.LastIndexOf("GAMES/");
                //var endno = (txt_FTPPath.Text.LastIndexOf("BL")) + 9;
                //var GameName = ((txt_FTPPath.Text).Substring(startno, endno - startno)).Replace("GAMES/", "");
                //var newpath = txt_FTPPath.Text.Replace("GAMES", "game").Replace("PS3_GAME", GameID).Replace(GameName + "/", "");
                FTPFile(txt_FTPPath.Text, h + "_ps3.psarc.edat");
                copyftp = " and FTPed";
            }
            else if ((chbx_Format.Text == "PC" || chbx_Format.Text == "Mac") && chbx_Copy.Checked)
            {
                var platfrm = (chbx_Format.Text == "PC" ? "_p" : (chbx_Format.Text == "Mac" ? "_m" : ""));
                var dest = "";
                if (RocksmithDLCPath.IndexOf("Rocksmith\\DLC") > 0)
                {
                    dest = RocksmithDLCPath;
                    //File.Copy(RocksmithDLCPath + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", false);
                    File.Copy(h + platfrm + ".psarc", dest, true);
                }
                else if (RocksmithDLCPath != txt_FTPPath.Text)
                {
                    dest = txt_FTPPath.Text;//!File.Exists(
                    //File.Copy(dest + "\\rs1compatibilitydlc" + platfrm + ".psarc", dest + "\\rs1compatibilitydlc" + platfrm + ".psarc.orig", false);
                    File.Copy(h + platfrm + ".psarc", dest, true);
                }
                else MessageBox.Show("Chose a different path to save");
                copyftp = " and Copied";
            }

            if (chbx_RemoveBassDD.Checked && chbx_BassDD.Checked)
            {
                var xmlFiles = Directory.GetFiles(files[0].Folder_Name, "*.old", SearchOption.AllDirectories);
                var platform = files[0].Folder_Name.GetPlatform();

                foreach (var xml in xmlFiles)
                    if (xml.IndexOf("bass") > 0 && (xml.IndexOf(".old") > 0))
                    //chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                    {
                        //if (!File.Exists(xml)) 
                        File.Copy(xml, xml.Replace(".old",""), true);
                        File.Delete(xml);                
                    }
            }

            pB_ReadDLCs.Increment(1);
            MessageBox.Show("Song Packed" + copyftp );

        }

        public void FTPFile(string filel, string filen)
        {
            // Get the object used to communicate with the server.
            var ddd = filel + filen.Replace(TempPath + "\\", "");
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
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show(ee.Message + "PS3 is down :(! " + SearchCmd);
                //MessageBox.Show(ee.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        //for conversion the wwise needs to be downlaoded from https://www.audiokinetic.com/download/?id=2013.2.10_4884
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(AppWD, "oggcut.exe");
            startInfo.WorkingDirectory = AppWD;// Path.GetDirectoryName();
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
                    if (DDC.ExitCode == 0)
                    {
                        Converters(tt, ConverterTypes.Ogg2Wem);
                        txt_OggPreviewPath.Text = tt;
                        chbx_Preview.Checked = true;
                        var i = DataViewGrid.SelectedCells[0].RowIndex;
                        DataViewGrid.Rows[i].Cells[43].Value = "Yes";
                        DataViewGrid.Rows[i].Cells[12].Value = tt.Replace(".ogg",".wem");
                        DataViewGrid.Rows[i].Cells[56].Value = txt_PreviewStart.Text;
                        DataViewGrid.Rows[i].Cells[57].Value = txt_PreviewEnd.Text;
                        btn_PlayPreview.Enabled = true;

                        if (File.Exists(tt))
                            using (FileStream fss = File.OpenRead(tt))
                            {
                                SHA1 sha = new SHA1Managed();
                                DataViewGrid.Rows[i].Cells[82].Value = BitConverter.ToString(sha.ComputeHash(fss));
                                fss.Close();
                            }

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
        public static void Converters(string file, ConverterTypes converterType)
        {
            
            var txtOgg2FixHdr = String.Empty;
            var txtWwiseConvert = String.Empty;
            var txtWwise2Ogg = String.Empty;
           var  txtAudio2Wem = String.Empty;

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
                                break;
                        }

                        successFiles.Add(file);
                    }
                    catch (Exception ex)
                    {
                        errorFiles.Add(file, ex.Message);
                    }
                

                if (errorFiles.Count <= 0 && successFiles.Count > 0)
                    MessageBox.Show("Conversion complete!", MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    StringBuilder alertMessage = new StringBuilder(
                        "Conversion complete with errors." + Environment.NewLine);
                    alertMessage.AppendLine(
                        "Files converted with error: " + Environment.NewLine);
                    foreach (var eFile in errorFiles)
                        alertMessage.AppendLine(String.Format("File: {0}, error: {1}", eFile.Key, eFile.Value));

                    MessageBox.Show(alertMessage.ToString(), MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DataViewGrid.Rows[i].Cells[78].Value = temppath;
                Converters(temppath, ConverterTypes.Ogg2Wem);
                DataViewGrid.Rows[i].Cells[12].Value = temppath.Replace(".ogg", ".wem");
                if (File.Exists(temppath.Replace(".ogg", ".wem")))
                    using (FileStream fss = File.OpenRead(temppath.Replace(".ogg", ".wem")))
                    {
                        SHA1 sha = new SHA1Managed();
                        DataViewGrid.Rows[i].Cells[82].Value = BitConverter.ToString(sha.ComputeHash(fss));
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
            var cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path); //+ ";Persist Security Info=False"
            var command = cnn.CreateCommand();
            //using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
            //{
            command.CommandText = "UPDATE Main SET ";
            command.CommandText += "Selected = @param8 ";
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
                MessageBox.Show("Can not open Main DB connection in Main Edit screen ! " + DB_Path + "-" + command.CommandText);
                throw;
            }
            finally
            {
                if (cnn != null) cnn.Close();
            }
            //}
            MessageBox.Show("All songs in DB have been marked as Selected");
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
            MessageBox.Show("All songs in DB have been UNmarked from Selected");
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
        public string GeneratePackage(string ID)
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
            var artist = "";
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

                var bassRemoved = "No";
                var DDAdded = "No";

                var xmlFiles = Directory.GetFiles(file.Folder_Name, "*.xml", SearchOption.AllDirectories);
                var platform = file.Folder_Name.GetPlatform();
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
                    Pc = chbx_Format.Text == "PC" ? true : false, //txt_Platform.Text 
                    Mac = chbx_Format.Text == "Mac" ? true : false,
                    XBox360 = chbx_Format.Text == "XBOX360" ? true : false,
                    PS3 = chbx_Format.Text == "PS3" ? true : false,
                    Name = file.DLC_Name,
                    AppId = file.DLC_AppID,
                    ArtFiles=info.ArtFiles,

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
                    Volume = file.Volume.ToInt32(),
                    PreviewVolume = file.Preview_Volume.ToInt32(),
                    SignatureType = info.SignatureType,
                    PackageVersion = file.Version
                };
                //bcapirtxt_StatisticsOnReadDLCs.Text = file.Song_Title+" test"+i+ data.SongInfo.Artist + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                var norm_path = TempPath + "\\" + ((file.ToolkitVersion == "") ? "ORIG" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;
                //rtxt_StatisticsOnReadDLCs.Text = "8"+data.PackageVersion+"...manipul" + norm_path + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //manipulating the info
                //if (cbx_Activ_Title.Checked)
                data.SongInfo.SongDisplayName = Manipulate_strings(txt_Title.Text, i, false, false);
                // rtxt_StatisticsOnReadDLCs.Text = "...manipul: "+ file.Song_Title + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Title_Sort.Checked)
                data.SongInfo.SongDisplayNameSort = Manipulate_strings(txt_Title_Sort.Text, i, false, false);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Artist.Checked)
                data.SongInfo.Artist = Manipulate_strings(txt_Artist.Text, i, false, false);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Artist_Sort.Checked)
                data.SongInfo.ArtistSort = Manipulate_strings(txt_Artist_Sort.Text, i, false, false);
                //rtxt_StatisticsOnReadDLCs.Text = "...manipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (cbx_Activ_Album.Checked)
                data.SongInfo.Album = Manipulate_strings(txt_Album.Text, i, false, false);
                //rtxt_StatisticsOnReadDLCs.Text = "...3" + "\n" + rtxt_StatisticsOnReadDLCs.Text;

                //rtxt_StatisticsOnReadDLCs.Text = "...nipul" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                //if (chbx_Additional_Manipualtions.GetItemChecked(0)) //"1. Add Increment to all Titles"
                //data.Name = i + data.Name;

                //rtxt_StatisticsOnReadDLCs.Text = "...mpl" + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                artist = "";
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
                if (chbx_UniqueID.Checked)
                    if (file.UniqueDLCName != "" ) data.Name = file.UniqueDLCName;
                    else
                    {
                        Random random = new Random();
                        data.Name = random.Next(0, 100000) + data.Name;
                        var k = DataViewGrid.SelectedCells[0].RowIndex;
                        DataViewGrid.Rows[k].Cells[79].Value = data.Name;
                        //var DB_Path = DBFolder + "\\Files.accdb;";
                        using (OleDbConnection cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                        {
                            DataSet dis = new DataSet();
                            cmd = "UPDATE Main SET UniqueDLCName=\"" + data.Name + "\" WHERE ID=" + file.ID;
                            OleDbDataAdapter das = new OleDbDataAdapter(cmd, cnn);
                            das.Fill(dis, "Main");
                            das.Dispose();
                        }
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
                //else
                FN = GeneralExtensions.GetShortName("{0}-{1}-v{2}", (((file.Version == null) ? "ORIG" : "CDLC") + "_" + file.Artist), (file.Album_Year.ToInt32() + "_" + file.Album + "_" + file.Song_Title), file.Version, ConfigRepository.Instance().GetBoolean("creator_useacronyms"));//((data.PackageVersion == null) ? "Original" : "CDLC") + "_" + data.SongInfo.Artist + "_" + data.SongInfo.SongYear + "_" + data.SongInfo.Album + "_" + data.SongInfo.SongDisplayName;

                if (file.Is_Alternate == "Yes") FN += "a." + file.Alternate_Version_No + file.Author;

                //rtxt_StatisticsOnReadDLCs.Text = "fn: " + FN + "\n" + rtxt_StatisticsOnReadDLCs.Text;
                if (chbx_Format.Text == "PS3")
                {
                    FN = FN.Replace(".", "_");
                    FN = FN.Replace(" ", "_");
                }

                dlcSavePath = TempPath + "\\" + FN;
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
                        //bwRGenerate.ReportProgress(progress, "Generating PC package");
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "1pc..." + rtxt_StatisticsOnReadDLCs.Text;
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Pc, GameVersion.RS2014));
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "2pc..." + rtxt_StatisticsOnReadDLCs.Text;
                        progress += step;
                        //bwRGenerate.ReportProgress(progress);
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "3pc..." + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    catch (Exception ex)
                    {
                        errorsFound.AppendLine(String.Format("Error 0 generate PC package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "...0pc ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                    }

                if (chbx_Format.Text == "Mac")
                    try
                    {
                        //bwRGenerate.ReportProgress(progress, "Generating Mac package");
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.Mac, GameVersion.RS2014));
                        progress += step;
                        //bwRGenerate.ReportProgress(progress);
                    }
                    catch (Exception ex)
                    {
                        errorsFound.AppendLine(String.Format("Error 1 generate Mac package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "...0mac ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                    }

                if (chbx_Format.Text == "XBOX360")
                    try
                    {
                        //bwRGenerate.ReportProgress(progress, "Generating XBox 360 package");
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.XBox360, GameVersion.RS2014));
                        progress += step;
                        //bwRGenerate.ReportProgress(progress);
                    }
                    catch (Exception ex)
                    {
                        errorsFound.AppendLine(String.Format("Error generate XBox 360 package: {0}{1}{0}{2}{0}", Environment.NewLine, ex.Message, ex.StackTrace));
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "...0mac ERROR..." + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
                    }

                if (chbx_Format.Text == "PS3")
                    try
                    {
                        //rtxt_StatisticsOnReadDLCs.Text = "ps3...start..." + rtxt_StatisticsOnReadDLCs.Text;
                        //bwRGenerate.ReportProgress(progress, "Generating PS3 package");
                        //rtxt_StatisticsOnReadDLCs.Text = dlcSavePath + rtxt_StatisticsOnReadDLCs.Text;
                        RocksmithToolkitLib.DLCPackage.DLCPackageCreator.Generate(dlcSavePath, data, new Platform(GamePlatform.PS3, GameVersion.RS2014));
                        //progress += step;
                        //bwRGenerate.ReportProgress(progress);
                        // rtxt_StatisticsOnReadDLCs.Text = "ps3...off..." + rtxt_StatisticsOnReadDLCs.Text;
                    }
                    catch (Exception ex)
                    {
                        errorsFound.AppendLine(String.Format("Error 2generate PS3 package: {0}{1}. {0}PS3 package require 'JAVA x86' (32 bits) installed on your machine to generate properly.{0}", Environment.NewLine, ex.StackTrace));
                        //bcapirtxt_StatisticsOnReadDLCs.Text = "...0Ps3 ERROR..."+ dlcSavePath+"---"+ dlcSavePath.Length+ "----" + ex.Message + rtxt_StatisticsOnReadDLCs.Text;
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
            return dlcSavePath;
            MessageBox.Show("Repack done");
        }



        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chbx_PreSavedFTP.Text == "US") txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/game/BLUS31182/USRDIR/DLC/";
            else txt_FTPPath.Text = "ftp://192.168.1.12/" + "dev_hdd0/game/BLES01862/USRDIR/DLC/";
        }

        public string Manipulate_strings(string words, int k, bool ifn, bool orig_flag) //static
        //words: 
        //k :
        //ifn: 
        {
            //rtxt_StatisticsOnReadDLCs.Text = "ff" + rtxt_StatisticsOnReadDLCs.Text;
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
                            fulltxt += ((files[k].Track_No != "") ? (" - " + files[k].Track_No) : "");
                            break;
                        case "<Year>":
                            fulltxt += files[k].Album_Year;
                            break;
                        case "<Rating>":
                            fulltxt += ((files[k].Rating == "") ? "0" : files[k].Rating);
                            break;
                        case "<Alt. Vers.>":
                            fulltxt += "ALT" + files[k].Alternate_Version_No;
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
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Lead == "Yes") ? "L" : "") + ((files[k].Has_Combo == "Yes") ? "C" : "") + ((files[k].Has_Rhythm == "Yes") ? "R" : "");
                            break;
                        case "<Bass_HasDD>":
                            fulltxt += (files[k].Has_BassDD == "No" && files[k].Has_DD == "Yes" ? "NoBDD" : ""); //not yet done
                            break;
                        case "<Avail. Instr.>":
                            fulltxt += ((files[k].Has_Bass == "Yes") ? "B" : "") + ((files[k].Has_Guitar == "Yes") ? "G" : "");
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

        private void btn_RemoveBassDD_Click(object sender, EventArgs e)
        {           
            btn_RemoveBassDD.Enabled = false;
            
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
                if (xml.IndexOf("bass") > 0 && (xml.IndexOf(".old")==0))
                //chbx_Additional_Manipulations.GetItemChecked(3) || chbx_Additional_Manipulations.GetItemChecked(5) || chbx_Additional_Manipulations.GetItemChecked(12) || chbx_Additional_Manipulations.GetItemChecked(26))
                {
                    var bassRemoved = (DLCManager.RemoveDD(files[0].Folder_Name, chbx_Original.Text, xml, platform, false, false) == "Yes") ? "No" : "Yes";
                }
            //chbx_DD.Checked = false;
            chbx_BassDD.Checked = false;
            SaveRecord();
        }

        private void btn_AddDD_Click(object sender, EventArgs e)
        {
            btn_RemoveDD.Enabled = true; btn_AddDD.Enabled = false;  btn_RemoveBassDD.Enabled = true;//numericUpDown1.Enabled = false;
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
                if (xml.IndexOf("showlights") <1)
                {
                    var DDAdded = (DLCManager.AddDD(files[0].Folder_Name, chbx_Original.Text, xml, platform, false, false, numericUpDown1.Value.ToString()) == "Yes") ? "No" : "Yes";
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
                if (xml.IndexOf("showlights") < 1)
                {
                    var DDRemoved = (DLCManager.RemoveDD(files[0].Folder_Name, chbx_Original.Text, xml, platform, false, false) == "Yes") ? "No" : "Yes";
                }
            chbx_DD.Checked = false;
            chbx_BassDD.Checked = false;
            SaveRecord();
        }

        private void btn_OldFolder_Click(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = TempPath + "\\0_old\\"+ DataViewGrid.Rows[i].Cells[19].Value.ToString();
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
            //1. Delete Song Folder
             try
            {
                var j = DataViewGrid.SelectedCells[0].RowIndex;
                string filePath = DataViewGrid.Rows[j].Cells[22].Value.ToString(); //TempPath + "\\0_old\\" + 
                Directory.Delete(filePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not Delete Song folder ! ");
            }

            var cmd = "DELETE FROM Main WHERE ID IN ("+txt_ID.Text+")";
            //var DB_Path = DBFolder;

            //1. Delete DB records
            DLCManager.DeleteRecords(txt_ID.Text, cmd, DB_Path);

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

        private void button7_Click(object sender, EventArgs e)
        {
            //1. Copy Files
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = DataViewGrid.Rows[i].Cells[22].Value.ToString(); //TempPath + "\\0_old\\" + 
            string t = filePath + "2";
            try //Copy dir
            {
                string source_dir = @filePath;
                string destination_dir = @t;

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
                //Directory.Delete(source_dir, true); DONT DELETE

            }
            catch (Exception ee)
            {
                //rtxt_StatisticsOnReadDLCs.Text = "FAILED3 .." + "\n" + rtxt_StatisticsOnReadDLCs.Text;//ee.Message + "----" +
                Console.WriteLine(ee.Message);
            }

            //2. Copy Records
            try //Copy dir
            {
                var cmd = "INSERT into Main (Song_Title, Song_Title_Sort, Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, AlbumArtPath, AudioPath, audioPreviewPath, Track_No, Author, Version, DLC_Name, DLC_AppID, Current_FileName, Original_FileName, Import_Path, Import_Date, Folder_Name, File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description, Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Temp, CustomForge_Followers, CustomForge_Version, Show_Available_Instruments, Show_Alternate_Version, Show_MultiTrack_Details, Show_Group, Show_Beta, Show_Broken, Show_DD, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, OggPath, oggPreviewPath, UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected) SELECT Song_Title+\"2\", Song_Title_Sort+\"2\", Album, Artist, Artist_Sort, Album_Year, AverageTempo, Volume, Preview_Volume, AlbumArtPath, AudioPath, audioPreviewPath, Track_No, Author, Version, DLC_Name, DLC_AppID+\"2\", Current_FileName, Original_FileName, Import_Path, Import_Date, Folder_Name+\"2\", File_Size, File_Hash, Original_File_Hash, Is_Original, Is_OLD, Is_Beta, Is_Alternate, Is_Multitrack, Is_Broken, MultiTrack_Version, Alternate_Version_No, DLC, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_DD, Has_Version, Tunning, Bass_Picking, Tones, Groups, Rating, Description+\" duplicate\", Comments, Has_Track_No, Platform, PreviewTime, PreviewLenght, Temp, CustomForge_Followers, CustomForge_Version, Show_Available_Instruments, Show_Alternate_Version, Show_MultiTrack_Details, Show_Group, Show_Beta, Show_Broken, Show_DD, Original, Selected, YouTube_Link, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, SignatureType, ToolkitVersion, Has_Author, OggPath, oggPreviewPath, UniqueDLCName, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, Bass_Has_DD, Has_Bonus_Arrangement, Artist_ShortName, Album_ShortName, Available_Old, Available_Duplicate, Has_Been_Corrected FROM Main  WHERE ID = " + txt_ID.Text;
                DataSet dsz = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dab = new OleDbDataAdapter(cmd, cnb);
                    dab.Fill(dsz, "Main");
                    dab.Dispose();
                }


                //getting ID
                DataSet dus = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dad = new OleDbDataAdapter("SELECT ID FROM Main WHERE Song_Title=\"" + txt_Title.Text + "2\"", cnb);
                    dad.Fill(dus, "Main");
                    dad.Dispose();
                }
                var CDLC_ID = dus.Tables[0].Rows[0].ItemArray[0].ToString();


                cmd = "INSERT into Main (Arrangement_Name, CDLC_ID, Bonus, SNGFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments) SELECT Arrangement_Name, " + CDLC_ID + ", Bonus, SNGFilePath, XMLFilePath, XMLFile_Hash, ScrollSpeed, Tunning, Rating, PlayThoughYBLink, CustomsForge_Link, ArrangementSort, TuningPitch, ToneBase, Idd, MasterId, ArrangementType, String0, String1, String2, String3, String4, String5, PluckedType, RouteMask, XMLFileName, XMLFileLLID, XMLFileUUID, SNGFileName, SNGFileLLID, SNGFileUUID, ToneMultiplayer, ToneA, ToneB, ToneC, ToneD, lastConversionDateTime, SNGFileHash, Has_Sections, Comments FROM Arrangements WHERE CDLC_ID = " + CDLC_ID;
                DataSet dgz = new DataSet();
                using (OleDbConnection cnb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DB_Path))
                {
                    OleDbDataAdapter dah = new OleDbDataAdapter(cmd, cnb);
                    dah.Fill(dgz, "Tones");
                    dah.Dispose();
                }

                cmd = "INSERT into Main (Tone_Name, CDLC_ID, Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime) SELECT Tone_Name, " + CDLC_ID+ ", Volume, Keyy, Is_Custom, GearList, AmpRack, Pedals, Description, Favorite, SortOrder, NameSeparator, Cabinet, PostPedal1, PostPedal2, PostPedal3, PostPedal4, PrePedal1, PrePedal2, PrePedal3, PrePedal4, Rack1, Rack2, Rack3, Rack4, AmpType, AmpCategory, AmpKnobValues, AmpPedalKey, CabinetCategory, CabinetKnobValues, CabinetPedalKey, CabinetType, lastConversionDateTime, lastConverjsonDateTime FROM Tones WHERE CDLC_ID = " + CDLC_ID;
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
                Console.WriteLine(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            
            txt_Track_No.Text = (GetTrackNo(txt_Artist.Text , txt_Album.Text, txt_Title.Text)).ToString();
            string uriString = "http://ws.spotify.com/search/1/track";// "http://www.google.com/search";
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
            txt_debug.Text = a1 + "\n\n " + a2 + "\n " + a3 + "\n " + a4;
            

        }

        public static int GetTrackNo(string Artist, string Album, string Title)
        {

            string uriString = "http://ws.spotify.com/search/1/track";// "http://www.google.com/search";
            string keywordString = (Artist + "\" \"" + Album + "\" \"" + Title + "\"").Replace("&","%26"); //"discorg.com:\"" + txt_Artist.Text + "\" \"" + txt_Album.Text + "\" \"" + txt_Title.Text + "\" \"track\""; //"www.metrolyrics.com:" + 

            WebClient webClient = new WebClient();

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", keywordString);

            webClient.QueryString.Add(nameValueCollection);
            var a1 = "";
            try
            {
            var aa = (webClient.DownloadString(uriString));
            
            //if ( (aa.ToLower()).IndexOf(", <b>"+ txt_Title.Text.ToLower() ) >0 ) a1 = aa.Substring(((aa.ToLower()).IndexOf(", <b>"+txt_Title.Text.ToLower())) -1, 1);//<b>+ txt_Title.Text

            if ((aa.ToLower()).IndexOf("<track-number>") > 0) a1 = aa.Substring(((aa.ToLower()).IndexOf("<track-number>")) + 14, 2);//<b>+ txt_Title.Text
            a1 = a1.Replace("<","");
            }
            catch (System.IO.FileNotFoundException ee)
            {
                MessageBox.Show("FAILED To get track" + ee.Message + "----");
                Console.WriteLine(ee.Message);
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

            do {
                Console.Write("Please enter a search value to look for in the above string (hit Enter to exit) ==> ");

                strTarget = Console.ReadLine();

                if (strTarget != "") {

                    for (int i = 0; i < strSource.Length; i++) {

                        found = strSource.IndexOf(strTarget, i);

                        if (found >= 0) {
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

            } while ( true );
        }

        private void bth_GetTrackNo_Click(object sender, EventArgs e)
        {
            txt_Track_No.Text = (GetTrackNo(txt_Artist.Text, txt_Album.Text, txt_Title.Text)).ToString();
            var i = DataViewGrid.SelectedCells[0].RowIndex;

            if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; DataViewGrid.Rows[i].Cells[54].Value = "No"; }
            else { chbx_TrackNo.Checked = true; DataViewGrid.Rows[i].Cells[54].Value = "Yes"; }
        }

        private void txt_Track_No_TextChanged(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;

            if (txt_Track_No.Text == "-1") { chbx_TrackNo.Checked = false; DataViewGrid.Rows[i].Cells[54].Value = "No"; }
            else { chbx_TrackNo.Checked = true; DataViewGrid.Rows[i].Cells[54].Value = "Yes"; }
        }

        private void btn_OpenSongFolder_Click(object sender, EventArgs e)
        {
            var i = DataViewGrid.SelectedCells[0].RowIndex;
            string filePath = DataViewGrid.Rows[i].Cells[22].Value.ToString();

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
            Standardization.MakeCover();
        }
    }
}