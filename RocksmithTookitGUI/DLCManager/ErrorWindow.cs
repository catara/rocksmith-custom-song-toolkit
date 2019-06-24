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
using Ookii.Dialogs; //cue text

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class ErrorWindow : Form
    {
        public bool IgnoreSong { get; set; }
        public bool StopImport { get; set; }
        public ErrorWindow(string mss, string link, string Title, bool B1Visi, bool B2Visi, bool B3Visi, string B1Txt, string B2Txt, string B3Txt)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        { 
            InitializeComponent();
            lbl_Link.Text = link;
            txt_Description.Text = mss;
            IgnoreSong = false;
            StopImport = false;
            //ErrorWindow.ActiveForm.Text = Title;
            btn_B1.Visible = B1Visi;
            btn_B2.Visible = B2Visi;
            btn_B3.Visible = B3Visi;
            if (B1Txt != "") btn_B1.Text = B1Txt;
            if (B2Txt != "") btn_B2.Text = B2Txt;
            if (B3Txt != "") btn_B3.Text = B3Txt;
            //MessageBox.Show("test0");
            //DB_Path = txt_DBFolder;
            //TempPath = txt_TempPath;
            //RocksmithDLCPath = txt_RocksmithDLCPath;
        }    

        private void InitializeComponent()
        {
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.btn_B1 = new System.Windows.Forms.Button();
            this.lbl_Link = new System.Windows.Forms.LinkLabel();
            this.btn_B2 = new System.Windows.Forms.Button();
            this.btn_B3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.txt_Description);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_B1);
            this.splitContainer1.Panel2.Controls.Add(this.lbl_Link);
            this.splitContainer1.Panel2.Controls.Add(this.btn_B2);
            this.splitContainer1.Panel2.Controls.Add(this.btn_B3);
            this.splitContainer1.Size = new System.Drawing.Size(1115, 474);
            this.splitContainer1.SplitterDistance = 323;
            this.splitContainer1.TabIndex = 336;
            // 
            // txt_Description
            // 
            this.txt_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Description.Location = new System.Drawing.Point(0, 0);
            this.txt_Description.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(1115, 323);
            this.txt_Description.TabIndex = 337;
            this.txt_Description.Text = "";
            // 
            // btn_B1
            // 
            this.btn_B1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_B1.Location = new System.Drawing.Point(0, 57);
            this.btn_B1.Name = "btn_B1";
            this.btn_B1.Size = new System.Drawing.Size(1115, 30);
            this.btn_B1.TabIndex = 8;
            this.btn_B1.Text = "Ignore Song";
            this.btn_B1.UseVisualStyleBackColor = true;
            this.btn_B1.Click += new System.EventHandler(this.btn_StopImport_Click);
            // 
            // lbl_Link
            // 
            this.lbl_Link.AutoSize = true;
            this.lbl_Link.Location = new System.Drawing.Point(12, 17);
            this.lbl_Link.Name = "lbl_Link";
            this.lbl_Link.Size = new System.Drawing.Size(55, 13);
            this.lbl_Link.TabIndex = 7;
            this.lbl_Link.TabStop = true;
            this.lbl_Link.Text = "linkLabel1";
            this.lbl_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lbl_Link_LinkClicked);
            // 
            // btn_B2
            // 
            this.btn_B2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_B2.Location = new System.Drawing.Point(0, 87);
            this.btn_B2.Name = "btn_B2";
            this.btn_B2.Size = new System.Drawing.Size(1115, 30);
            this.btn_B2.TabIndex = 6;
            this.btn_B2.Text = "Stop Import";
            this.btn_B2.UseVisualStyleBackColor = true;
            this.btn_B2.Visible = false;
            this.btn_B2.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_B3
            // 
            this.btn_B3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_B3.Location = new System.Drawing.Point(0, 117);
            this.btn_B3.Name = "btn_B3";
            this.btn_B3.Size = new System.Drawing.Size(1115, 30);
            this.btn_B3.TabIndex = 5;
            this.btn_B3.Text = "OK";
            this.btn_B3.UseVisualStyleBackColor = true;
            this.btn_B3.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // ErrorWindow
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1115, 474);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ErrorWindow";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            StopImport = true;
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            IgnoreSong = true;
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
