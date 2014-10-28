using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class TonesDB : Form
    {
        public TonesDB()
        {
            InitializeComponent();
        }

        private string Filename = System.IO.Path.Combine(Application.StartupPath, "Text.txt");

        private BindingSource Tones = new BindingSource();
        //private BindingSource bsPositions = new BindingSource();
        //private BindingSource bsBadges = new BindingSource();

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            RocksmithToolkitGUI.DLCManager.DataAccess da = new DataAccess();
            da.Populate(ref DataGridView1, ref Tones);//, ref bsPositions, ref bsBadges);
            //DataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;
        }

    }
}
