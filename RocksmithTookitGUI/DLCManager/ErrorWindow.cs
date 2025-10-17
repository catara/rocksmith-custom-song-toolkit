using Ookii.Dialogs;
using RocksmithToolkitGUI;
using RocksmithToolkitGUI.DLCManager;
using RocksmithToolkitLib.Extensions; //dds
using RocksmithToolkitLib.XmlRepository; //cue text
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//bcapi
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using static System.Net.Mime.MediaTypeNames;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class ErrorWindow : Form
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public string erorrfeedback { get; set; }

        static string AppWD = AppDomain.CurrentDomain.BaseDirectory + "DLCManager\\external_tools"; //when removing DDC
        public ErrorWindow(string mss, string link, string Title, bool B1Visi, bool B2Visi, bool B3Visi, string B1Txt, string B2Txt, string B3Txt
            , bool wordwrap)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {
            InitializeComponent();
            var link1 = link; var link2 = "";
            if (link.Contains(";")) link2 = link.Split(';')[1];
            lbl_Link1.Text = link1; if (link1 != "" && link1 is not null) lbl_Link1.Visible = true; else lbl_Link1.Visible = false;
            lbl_Link2.Text = link2; if (link2 != "" && link2 is not null) lbl_Link2.Visible = true; else lbl_Link2.Visible = false;
            txt_Description.Text = mss;
            B1 = false;
            B2 = false;
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
                txt_Description.Font = new System.Drawing.Font("Courier New", 9);
                //txt_Description.Font. = "Courier New";
            }
            else
            {
                txt_Description.WordWrap = true;
                txt_Description.Font = new System.Drawing.Font("Segoe UI", 9);
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
            lbl_Link2 = new LinkLabel();
            btn_B1 = new Button();
            lbl_Link1 = new LinkLabel();
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
            splitContainer1.Panel2.Controls.Add(lbl_Link2);
            splitContainer1.Panel2.Controls.Add(btn_B1);
            splitContainer1.Panel2.Controls.Add(lbl_Link1);
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
            txt_Description.Font = new System.Drawing.Font("Courier New", 9F);
            txt_Description.Location = new Point(0, 0);
            txt_Description.Margin = new Padding(4, 5, 4, 5);
            txt_Description.Name = "txt_Description";
            txt_Description.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            txt_Description.Size = new Size(1105, 301);
            txt_Description.TabIndex = 337;
            txt_Description.Text = "";
            // 
            // lbl_Link2
            // 
            lbl_Link2.AutoSize = true;
            lbl_Link2.Location = new Point(12, 19);
            lbl_Link2.Name = "lbl_Link2";
            lbl_Link2.Size = new Size(60, 15);
            lbl_Link2.TabIndex = 9;
            lbl_Link2.TabStop = true;
            lbl_Link2.Text = "linkLabel2";
            lbl_Link2.LinkClicked += lbl_Link2_LinkClicked;
            // 
            // btn_B1
            // 
            btn_B1.Dock = DockStyle.Bottom;
            btn_B1.Location = new Point(0, 35);
            btn_B1.Name = "btn_B1";
            btn_B1.Size = new Size(1105, 30);
            btn_B1.TabIndex = 8;
            btn_B1.Text = "Ignore Song";
            btn_B1.UseVisualStyleBackColor = true;
            btn_B1.Click += btn_B1_Click;
            // 
            // lbl_Link1
            // 
            lbl_Link1.AutoSize = true;
            lbl_Link1.Location = new Point(12, 4);
            lbl_Link1.Name = "lbl_Link1";
            lbl_Link1.Size = new Size(60, 15);
            lbl_Link1.TabIndex = 7;
            lbl_Link1.TabStop = true;
            lbl_Link1.Text = "linkLabel1";
            lbl_Link1.LinkClicked += Lbl_Link_LinkClicked;
            // 
            // btn_B2
            // 
            btn_B2.Dock = DockStyle.Bottom;
            btn_B2.Location = new Point(0, 65);
            btn_B2.Name = "btn_B2";
            btn_B2.Size = new Size(1105, 30);
            btn_B2.TabIndex = 6;
            btn_B2.Text = "Stop Import";
            btn_B2.UseVisualStyleBackColor = true;
            btn_B2.Visible = false;
            btn_B2.Click += btn_B2_Click;
            // 
            // btn_B3
            // 
            btn_B3.Dock = DockStyle.Bottom;
            btn_B3.Font = new System.Drawing.Font("Segoe UI", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_B3.Location = new Point(0, 95);
            btn_B3.Name = "btn_B3";
            btn_B3.Size = new Size(1105, 42);
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

        private void btn_B2_Click(object sender, EventArgs e)
        {
            B2 = true;
            erorrfeedback = txt_Description.Text.ToString();
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            ConfigRepository.Instance()["dlcm_Global2TempVariable"] = txt_Description.Text;
            erorrfeedback = txt_Description.Text.ToString();
            this.Hide();
        }

        private void btn_B1_Click(object sender, EventArgs e)
        {
            B1 = true;
            erorrfeedback = txt_Description.Text.ToString();/*AppWD.Replace("DLCManager\\external_tools", "") + "\\" + c("dlcm_Database.NET")*/
            if (btn_B1.Text.Contains("Install from local") || btn_B1.Text.Contains("Open SQLITE3 DB and run command manually from local "))
                StartProcesss(btn_B1.Text.Replace("Install from local ", "").Replace("Open SQLITE3 DB and run command manually from local ", ""), null);
            this.Hide();
        }

        private void lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            //Process.Start(lbl_Link1.Text as string);
            OpenLink(lbl_Link1.Text);
        }

        private void lbl_Link2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            //Process.Start(lbl_Link2.Text as string);
            OpenLink(lbl_Link2.Text);
        }
    }
}
