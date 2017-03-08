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
        public ErrorWindow(string mss, string link, string Title, bool ignoresng, bool stopimport)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        { 
            InitializeComponent();
            lbl_Link.Text = link;
            lbl_Message.Text = mss;
            IgnoreSong = false;
            StopImport = false;
            //ErrorWindow.ActiveForm.Text = Title;
            btn_Close.Visible = stopimport;
            btn_StopImport.Visible = ignoresng;
            //MessageBox.Show("test0");
            //DB_Path = txt_DBFolder;
            //TempPath = txt_TempPath;
            //RocksmithDLCPath = txt_RocksmithDLCPath;
        }    

        private void InitializeComponent()
        {
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.lbl_Link = new System.Windows.Forms.LinkLabel();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.btn_StopImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(214, 112);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(156, 30);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(415, 112);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(156, 30);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Stop Import";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Visible = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_Link
            // 
            this.lbl_Link.AutoSize = true;
            this.lbl_Link.Location = new System.Drawing.Point(65, 60);
            this.lbl_Link.Name = "lbl_Link";
            this.lbl_Link.Size = new System.Drawing.Size(72, 17);
            this.lbl_Link.TabIndex = 2;
            this.lbl_Link.TabStop = true;
            this.lbl_Link.Text = "linkLabel1";
            this.lbl_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_Link_LinkClicked);
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Location = new System.Drawing.Point(34, 28);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(46, 17);
            this.lbl_Message.TabIndex = 3;
            this.lbl_Message.Text = "label1";
            // 
            // btn_StopImport
            // 
            this.btn_StopImport.Location = new System.Drawing.Point(12, 112);
            this.btn_StopImport.Name = "btn_StopImport";
            this.btn_StopImport.Size = new System.Drawing.Size(156, 30);
            this.btn_StopImport.TabIndex = 4;
            this.btn_StopImport.Text = "Ignore Song";
            this.btn_StopImport.UseVisualStyleBackColor = true;
            this.btn_StopImport.Click += new System.EventHandler(this.btn_StopImport_Click);
            // 
            // ErrorWindow
            // 
            this.ClientSize = new System.Drawing.Size(583, 148);
            this.Controls.Add(this.btn_StopImport);
            this.Controls.Add(this.lbl_Message);
            this.Controls.Add(this.lbl_Link);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_OK);
            this.Name = "ErrorWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

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
            // Send the URL to the operating system.
            Process.Start(lbl_Link.Text as string);
        }
    }
} 
