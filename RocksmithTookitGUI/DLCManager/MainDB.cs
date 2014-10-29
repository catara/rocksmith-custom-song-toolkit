using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class MainDB : Form
    {
        public MainDB()
        {
            InitializeComponent();
            //MessageBox.Show("test0");
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Main = new BindingSource();
        private readonly string MESSAGEBOX_CAPTION;
        private object cbx_Lead;


        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void MainDB_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            //MessageBox.Show("test0");
            da.Populate(ref DataGridView1, ref Main);//, ref bsPositions, ref bsBadges);
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
            var DB_Path = "C:\\Temp\\GitHub\\tmp" + "\\Files.accdb"; //DLCManager.txt_DBFolder.Text
            try
            {
                Process process = Process.Start("msaccess.exe " + DB_Path + "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MESSAGEBOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Can not open Main DB connection in MainDB ! " + DB_Path );
            }
        }

        private void btn_Arrangements_Click(object sender, EventArgs e)
        {
            ArrangementsDB frm = new ArrangementsDB();
            frm.Show();
        }

        private void btn_Tones_Click(object sender, EventArgs e)
        {
            TonesDB frm = new TonesDB();
            frm.Show();
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            txt_Artist.Text = DataGridView1.Rows[i].Cells[4].Value.ToString();
            txt_Artist_Sort.Text = DataGridView1.Rows[i].Cells[5].Value.ToString();
            txt_Title.Text = DataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_Title_Sort.Text = DataGridView1.Rows[i].Cells[2].Value.ToString();
            txt_Album.Text = DataGridView1.Rows[i].Cells[3].Value.ToString();
            txt_Track_No.Text = DataGridView1.Rows[i].Cells[13].Value.ToString();
            txt_Author.Text = DataGridView1.Rows[i].Cells[14].Value.ToString();
            txt_Version.Text = DataGridView1.Rows[i].Cells[15].Value.ToString();
            txt_Rating.Text = DataGridView1.Rows[i].Cells[51].Value.ToString();
            txt_Tuning.Text = DataGridView1.Rows[i].Cells[47].Value.ToString();
            txt_DLC_ID.Text = DataGridView1.Rows[i].Cells[16].Value.ToString();
            txt_APP_ID.Text = DataGridView1.Rows[i].Cells[17].Value.ToString();
            txt_Description.Text = DataGridView1.Rows[i].Cells[52].Value.ToString();

            if (DataGridView1.Rows[i].Cells[37].Value.ToString() == "Yes") chbx_Lead.Checked = true;
            if (DataGridView1.Rows[i].Cells[39].Value.ToString() == "Yes") chbx_Combo.Checked = true;
            if (DataGridView1.Rows[i].Cells[38].Value.ToString() == "Yes") chbx_Rhythm.Checked = true;
            if (DataGridView1.Rows[i].Cells[35].Value.ToString() == "Yes") chbx_Bass.Checked = true;
            if (DataGridView1.Rows[i].Cells[26].Value.ToString() == "Yes") chbx_Original.Checked = true;
            //if (DataGridView1.Rows[i].Cells[26].Value.ToString() == "Yes") chbx_Sections.Checked = true;
            if (DataGridView1.Rows[i].Cells[45].Value.ToString() == "Yes") chbx_DD.Checked = true;
            if (DataGridView1.Rows[i].Cells[29].Value.ToString() == "Yes") chbx_Alternate.Checked = true;
            if (DataGridView1.Rows[i].Cells[31].Value.ToString() == "Yes") chbx_Broken.Checked = true;
            if (DataGridView1.Rows[i].Cells[43].Value.ToString() == "Yes") chbx_Preview.Checked = true;



        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i;
            i = DataGridView1.SelectedCells[0].RowIndex;
            DataGridView1.Rows[i].Cells[4].Value = txt_Artist.Text;
            DataGridView1.Rows[i].Cells[5].Value = txt_Artist_Sort.Text;
            DataGridView1.Rows[i].Cells[1].Value = txt_Title.Text;
            DataGridView1.Rows[i].Cells[2].Value = txt_Title_Sort.Text;
            DataGridView1.Rows[i].Cells[3].Value = txt_Album.Text;
            DataGridView1.Rows[i].Cells[13].Value = txt_Track_No.Text;
            DataGridView1.Rows[i].Cells[14].Value = txt_Author.Text;
            DataGridView1.Rows[i].Cells[15].Value = txt_Version.Text;
            DataGridView1.Rows[i].Cells[51].Value = txt_Rating.Text;
            DataGridView1.Rows[i].Cells[47].Value = txt_Tuning.Text;
            DataGridView1.Rows[i].Cells[16].Value = txt_DLC_ID.Text;
            DataGridView1.Rows[i].Cells[17].Value = txt_APP_ID.Text;
            DataGridView1.Rows[i].Cells[52].Value = txt_Description.Text;

            if (chbx_Lead.Checked) DataGridView1.Rows[i].Cells[37].Value = "Yes";
            else DataGridView1.Rows[i].Cells[37].Value = "No";
            if (chbx_Combo.Checked) DataGridView1.Rows[i].Cells[39].Value = "Yes";
            else DataGridView1.Rows[i].Cells[39].Value = "No";
            if (chbx_Rhythm.Checked) DataGridView1.Rows[i].Cells[38].Value = "Yes";
            else DataGridView1.Rows[i].Cells[38].Value = "No";
            if (chbx_Bass.Checked) DataGridView1.Rows[i].Cells[35].Value = "Yes";
            else DataGridView1.Rows[i].Cells[35].Value = "No";
            if (chbx_Original.Checked) DataGridView1.Rows[i].Cells[26].Value = "Yes";
            else DataGridView1.Rows[i].Cells[26].Value = "No";
            //if (chbx_Sections.Checked) DataGridView1.Rows[i].Cells[26].Value = "Yes";
            //else DataGridView1.Rows[i].Cells[26].Value = "No";
            if (chbx_DD.Checked) DataGridView1.Rows[i].Cells[45].Value = "Yes";
            else DataGridView1.Rows[i].Cells[45].Value = "No";
            if (chbx_Alternate.Checked) DataGridView1.Rows[i].Cells[29].Value = "Yes";
            DataGridView1.Rows[i].Cells[29].Value = "No";
            if (chbx_Broken.Checked) DataGridView1.Rows[i].Cells[31].Value = "Yes";
            else DataGridView1.Rows[i].Cells[31].Value = "No";
            if (chbx_Preview.Checked) DataGridView1.Rows[i].Cells[43].Value = "Yes";
            else DataGridView1.Rows[i].Cells[43].Value = "No";
        }
    }
}