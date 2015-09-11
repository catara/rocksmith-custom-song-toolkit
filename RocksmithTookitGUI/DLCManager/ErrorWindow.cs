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
        public ErrorWindow(string mss, string link)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        { 
            InitializeComponent();
            lbl_Link.Text = link;
            lbl_Message.Text = mss;
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
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(27, 112);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(156, 30);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(189, 112);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(156, 30);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Visible = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // lbl_Link
            // 
            this.lbl_Link.AutoSize = true;
            this.lbl_Link.Location = new System.Drawing.Point(65, 60);
            this.lbl_Link.Name = "lbl_Link";
            this.lbl_Link.Size = new System.Drawing.Size(55, 13);
            this.lbl_Link.TabIndex = 2;
            this.lbl_Link.TabStop = true;
            this.lbl_Link.Text = "linkLabel1";
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Location = new System.Drawing.Point(34, 28);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(35, 13);
            this.lbl_Message.TabIndex = 3;
            this.lbl_Message.Text = "label1";
            // 
            // ErrorWindow
            // 
            this.ClientSize = new System.Drawing.Size(367, 148);
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

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

        }
    }
} 
