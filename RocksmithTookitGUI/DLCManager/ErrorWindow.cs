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
using Ookii.Dialogs;
using RocksmithToolkitLib.XmlRepository; //cue text

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class ErrorWindow : Form
    {
        public bool IgnoreSong { get; set; }
        public bool StopImport { get; set; }
        public string erorrfeedback { get; set; }
        public ErrorWindow(string mss, string link, string Title, bool B1Visi, bool B2Visi, bool B3Visi, string B1Txt, string B2Txt, string B3Txt, bool wordwrap)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {
            InitializeComponent();
            lbl_Link.Text = link;
            txt_Description.Text = mss;
            IgnoreSong = false;
            StopImport = false;
            this.Text = Title;
            btn_B1.Visible = B1Visi;
            btn_B2.Visible = B2Visi;
            btn_B3.Visible = B3Visi;
            if (B1Txt != "") btn_B1.Text = B1Txt;
            if (B2Txt != "") btn_B2.Text = B2Txt;
            if (B3Txt != "") btn_B3.Text = B3Txt;
            if (wordwrap)
            {
                txt_Description.WordWrap = false;
                txt_Description.Font = new Font("Courier New", 9);
                //txt_Description.Font. = "Courier New";
            }
            else
            {
                txt_Description.WordWrap = true;
                txt_Description.Font = new Font("Segoe UI", 9);
            }
            //MessageBox.Show("test0");
            //DB_Path = txt_DBFolder;
            //TempPath = txt_TempPath;
            //RocksmithDLCPath = txt_RocksmithDLCPath;
        }

        private void InitializeComponent()
        {
            helpProvider1 = new HelpProvider();
            splitContainer1 = new SplitContainer();
            txt_Description = new RichTextBox();
            btn_B1 = new Button();
            lbl_Link = new LinkLabel();
            btn_B2 = new Button();
            btn_B3 = new Button();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(txt_Description);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btn_B1);
            splitContainer1.Panel2.Controls.Add(lbl_Link);
            splitContainer1.Panel2.Controls.Add(btn_B2);
            splitContainer1.Panel2.Controls.Add(btn_B3);
            splitContainer1.Size = new Size(1105, 442);
            splitContainer1.SplitterDistance = 301;
            splitContainer1.TabIndex = 336;
            // 
            // txt_Description
            // 
            txt_Description.BorderStyle = BorderStyle.None;
            txt_Description.Dock = DockStyle.Fill;
            txt_Description.Font = new Font("Courier New", 9F);
            txt_Description.Location = new Point(0, 0);
            txt_Description.Margin = new Padding(4, 5, 4, 5);
            txt_Description.Name = "txt_Description";
            txt_Description.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            txt_Description.Size = new Size(1105, 301);
            txt_Description.TabIndex = 337;
            txt_Description.Text = "";
            // 
            // btn_B1
            // 
            btn_B1.Dock = DockStyle.Bottom;
            btn_B1.Location = new Point(0, 47);
            btn_B1.Name = "btn_B1";
            btn_B1.Size = new Size(1105, 30);
            btn_B1.TabIndex = 8;
            btn_B1.Text = "Ignore Song";
            btn_B1.UseVisualStyleBackColor = true;
            btn_B1.Click += btn_StopImport_Click;
            // 
            // lbl_Link
            // 
            lbl_Link.AutoSize = true;
            lbl_Link.Location = new Point(12, 17);
            lbl_Link.Name = "lbl_Link";
            lbl_Link.Size = new Size(60, 15);
            lbl_Link.TabIndex = 7;
            lbl_Link.TabStop = true;
            lbl_Link.Text = "linkLabel1";
            lbl_Link.LinkClicked += Lbl_Link_LinkClicked;
            // 
            // btn_B2
            // 
            btn_B2.Dock = DockStyle.Bottom;
            btn_B2.Location = new Point(0, 77);
            btn_B2.Name = "btn_B2";
            btn_B2.Size = new Size(1105, 30);
            btn_B2.TabIndex = 6;
            btn_B2.Text = "Stop Import";
            btn_B2.UseVisualStyleBackColor = true;
            btn_B2.Visible = false;
            btn_B2.Click += btn_Close_Click;
            // 
            // btn_B3
            // 
            btn_B3.Dock = DockStyle.Bottom;
            btn_B3.Location = new Point(0, 107);
            btn_B3.Name = "btn_B3";
            btn_B3.Size = new Size(1105, 30);
            btn_B3.TabIndex = 5;
            btn_B3.Text = "OK";
            btn_B3.UseVisualStyleBackColor = true;
            btn_B3.Click += btn_OK_Click;
            // 
            // ErrorWindow
            // 
            AutoSize = true;
            ClientSize = new Size(1105, 442);
            Controls.Add(splitContainer1);
            Name = "ErrorWindow";
            Text = "generic window";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            StopImport = true;
erorrfeedback= txt_Description.Text.ToString();
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ConfigRepository.Instance()["dlcm_Global2TempVariable"] = txt_Description.Text;
            erorrfeedback= txt_Description.Text.ToString();            
            this.Hide();
        }

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            IgnoreSong = true;
            erorrfeedback= txt_Description.Text.ToString();
            this.Hide();
        }

        private void lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(lbl_Link.Text as string);
        }
    }
}
