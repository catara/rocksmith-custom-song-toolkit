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
using static RocksmithToolkitGUI.DLCManager.UtilitiesFunctions;
using RocksmithToolkitLib.Extensions; //dds
using System.Diagnostics;
using Ookii.Dialogs; //cue text
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.XmlRepository;
using System.Data.SQLite;
using SQLite;
using System.IO;
using RocksmithToolkitLib.Sng2014HSL;
using Windows.ApplicationModel.Appointments.DataProvider;

namespace RocksmithToolkitGUI.DLCManager
{
    public partial class DistribXMLNotes : Form
    {
        private string sXml; private string FilePath; private string gPlatform; private bool Arrangoff;
        private string BasedOn_CF; private string EoFPath;
        public double perc_time_betw_notes = 0;
        public DistribXMLNotes(string xml, string filePath, string sPlatform, bool arrangoff, string basedOn_CF, string eoFPath,
            OleDbConnection cnb, SQLite.SQLiteConnection cnc)//string txt_DBFolder,string txt_TempPath, string txt_RocksmithDLCPath, bool AllowEncript, bool AllowORIGDelete
        {

            InitializeComponent();

            sXml = xml; FilePath = filePath; sXml = xml; gPlatform = sPlatform; Arrangoff = arrangoff;
            BasedOn_CF = basedOn_CF; EoFPath = eoFPath;
            perc_time_betw_notes = perc_time_betw_notes;

        }

        private void InitializeComponent()
        {
            lbl_Comments = new Label();
            btn_Apply = new Button();
            btn_Estimate = new Button();
            label1 = new Label();
            txt_songStart = new DateTimePicker();
            txt_Description = new RichTextBox();
            txt_songLast = new DateTimePicker();
            xml_last = new DateTimePicker();
            xml_first = new DateTimePicker();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btn_EOF = new Button();
            btn_OpenGP5 = new Button();
            btn_OpenXML = new Button();
            chbx_BassToo = new CheckBox();
            lbl_Track = new Label();
            txt_XMLPath = new TextBox();
            txt_Ttime = new TextBox();
            lbl_TempFolders = new Label();
            txt_GP5 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txt_Correction = new DateTimePicker();
            label10 = new Label();
            txt_MaxRealLenght = new TextBox();
            txt_Remaining = new TextBox();
            txt_Percentage = new TextBox();
            txt_CurrentLenght = new TextBox();
            SuspendLayout();
            // 
            // lbl_Comments
            // 
            lbl_Comments.AutoSize = true;
            lbl_Comments.ForeColor = SystemColors.ControlText;
            lbl_Comments.Location = new Point(-280, 693);
            lbl_Comments.Margin = new Padding(4, 0, 4, 0);
            lbl_Comments.Name = "lbl_Comments";
            lbl_Comments.Size = new Size(130, 32);
            lbl_Comments.TabIndex = 438;
            lbl_Comments.Text = "Comments";
            // 
            // btn_Apply
            // 
            btn_Apply.BackColor = Color.LightSteelBlue;
            btn_Apply.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Apply.Location = new Point(734, 414);
            btn_Apply.Margin = new Padding(0);
            btn_Apply.Name = "btn_Apply";
            btn_Apply.Size = new Size(140, 48);
            btn_Apply.TabIndex = 437;
            btn_Apply.Text = "Apply";
            btn_Apply.UseVisualStyleBackColor = false;
            btn_Apply.Click += btn_Close_Click_1;
            // 
            // btn_Estimate
            // 
            btn_Estimate.ForeColor = Color.Green;
            btn_Estimate.Location = new Point(446, 330);
            btn_Estimate.Margin = new Padding(4);
            btn_Estimate.Name = "btn_Estimate";
            btn_Estimate.Size = new Size(134, 44);
            btn_Estimate.TabIndex = 436;
            btn_Estimate.Text = "Estimate";
            btn_Estimate.UseVisualStyleBackColor = true;
            btn_Estimate.Click += btn_Estimate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(605, 477);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 32);
            label1.TabIndex = 441;
            label1.Text = "Sections";
            // 
            // txt_songStart
            // 
            txt_songStart.CustomFormat = "mm:ss";
            txt_songStart.Format = DateTimePickerFormat.Custom;
            txt_songStart.Location = new Point(264, 55);
            txt_songStart.Margin = new Padding(4);
            txt_songStart.Name = "txt_songStart";
            txt_songStart.ShowUpDown = true;
            txt_songStart.Size = new Size(197, 39);
            txt_songStart.TabIndex = 439;
            txt_songStart.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // txt_Description
            // 
            txt_Description.Location = new Point(22, 472);
            txt_Description.Margin = new Padding(4);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(726, 486);
            txt_Description.TabIndex = 440;
            txt_Description.Text = "";
            // 
            // txt_songLast
            // 
            txt_songLast.CustomFormat = "mm:ss";
            txt_songLast.Format = DateTimePickerFormat.Custom;
            txt_songLast.Location = new Point(264, 104);
            txt_songLast.Margin = new Padding(4);
            txt_songLast.Name = "txt_songLast";
            txt_songLast.ShowUpDown = true;
            txt_songLast.Size = new Size(197, 39);
            txt_songLast.TabIndex = 442;
            txt_songLast.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // xml_last
            // 
            xml_last.CustomFormat = "mm:ss";
            xml_last.Enabled = false;
            xml_last.Format = DateTimePickerFormat.Custom;
            xml_last.Location = new Point(677, 104);
            xml_last.Margin = new Padding(4);
            xml_last.Name = "xml_last";
            xml_last.ShowUpDown = true;
            xml_last.Size = new Size(197, 39);
            xml_last.TabIndex = 444;
            xml_last.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // xml_first
            // 
            xml_first.CustomFormat = "mm:ss";
            xml_first.Enabled = false;
            xml_first.Format = DateTimePickerFormat.Custom;
            xml_first.Location = new Point(677, 57);
            xml_first.Margin = new Padding(4);
            xml_first.Name = "xml_first";
            xml_first.ShowUpDown = true;
            xml_first.Size = new Size(197, 39);
            xml_first.TabIndex = 443;
            xml_first.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(5, 57);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(261, 32);
            label2.TabIndex = 445;
            label2.Text = "Audio Track's first Note";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(5, 109);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(257, 32);
            label3.TabIndex = 446;
            label3.Text = "Audio Track's last Note";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(473, 55);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(172, 32);
            label4.TabIndex = 447;
            label4.Text = "XML First Note";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(473, 109);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(169, 32);
            label5.TabIndex = 448;
            label5.Text = "XML Last Note";
            // 
            // btn_EOF
            // 
            btn_EOF.Font = new Font("Microsoft Sans Serif", 6.6F, FontStyle.Regular, GraphicsUnit.Point);
            btn_EOF.Location = new Point(653, 13);
            btn_EOF.Margin = new Padding(4);
            btn_EOF.Name = "btn_EOF";
            btn_EOF.Size = new Size(148, 36);
            btn_EOF.TabIndex = 449;
            btn_EOF.Text = "Editor on Fire";
            btn_EOF.UseVisualStyleBackColor = true;
            btn_EOF.Click += btn_EOF_Click;
            // 
            // btn_OpenGP5
            // 
            btn_OpenGP5.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btn_OpenGP5.Location = new Point(333, 150);
            btn_OpenGP5.Margin = new Padding(4, 6, 4, 6);
            btn_OpenGP5.Name = "btn_OpenGP5";
            btn_OpenGP5.Size = new Size(117, 32);
            btn_OpenGP5.TabIndex = 450;
            btn_OpenGP5.Text = "Open GP5";
            btn_OpenGP5.UseVisualStyleBackColor = true;
            btn_OpenGP5.Click += btn_OpenGP5_Click;
            // 
            // btn_OpenXML
            // 
            btn_OpenXML.Font = new Font("Microsoft Sans Serif", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btn_OpenXML.Location = new Point(793, 150);
            btn_OpenXML.Margin = new Padding(4, 6, 4, 6);
            btn_OpenXML.Name = "btn_OpenXML";
            btn_OpenXML.Size = new Size(116, 36);
            btn_OpenXML.TabIndex = 451;
            btn_OpenXML.Text = "Open xML";
            btn_OpenXML.UseVisualStyleBackColor = true;
            // 
            // chbx_BassToo
            // 
            chbx_BassToo.AutoCheck = false;
            chbx_BassToo.AutoSize = true;
            chbx_BassToo.Enabled = false;
            chbx_BassToo.Location = new Point(448, 382);
            chbx_BassToo.Margin = new Padding(4);
            chbx_BassToo.Name = "chbx_BassToo";
            chbx_BassToo.Size = new Size(223, 36);
            chbx_BassToo.TabIndex = 452;
            chbx_BassToo.Text = "Incl. other Tracks";
            chbx_BassToo.UseVisualStyleBackColor = true;
            // 
            // lbl_Track
            // 
            lbl_Track.AutoSize = true;
            lbl_Track.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Track.ForeColor = SystemColors.ControlText;
            lbl_Track.Location = new Point(13, 9);
            lbl_Track.Margin = new Padding(4, 0, 4, 0);
            lbl_Track.Name = "lbl_Track";
            lbl_Track.Size = new Size(86, 32);
            lbl_Track.TabIndex = 453;
            lbl_Track.Text = "Track:";
            // 
            // txt_XMLPath
            // 
            txt_XMLPath.Location = new Point(473, 150);
            txt_XMLPath.Margin = new Padding(4, 3, 4, 3);
            txt_XMLPath.Name = "txt_XMLPath";
            txt_XMLPath.Size = new Size(312, 39);
            txt_XMLPath.TabIndex = 454;
            txt_XMLPath.UseWaitCursor = true;
            // 
            // txt_Ttime
            // 
            txt_Ttime.Location = new Point(297, 400);
            txt_Ttime.Margin = new Padding(4, 3, 4, 3);
            txt_Ttime.Name = "txt_Ttime";
            txt_Ttime.Size = new Size(143, 39);
            txt_Ttime.TabIndex = 455;
            txt_Ttime.UseWaitCursor = true;
            // 
            // lbl_TempFolders
            // 
            lbl_TempFolders.AutoSize = true;
            lbl_TempFolders.ForeColor = SystemColors.ControlText;
            lbl_TempFolders.Location = new Point(17, 403);
            lbl_TempFolders.Margin = new Padding(4, 0, 4, 0);
            lbl_TempFolders.Name = "lbl_TempFolders";
            lbl_TempFolders.Size = new Size(272, 32);
            lbl_TempFolders.TabIndex = 456;
            lbl_TempFolders.Text = "Avg time to adapt notes";
            lbl_TempFolders.UseWaitCursor = true;
            // 
            // txt_GP5
            // 
            txt_GP5.Location = new Point(13, 147);
            txt_GP5.Margin = new Padding(4, 3, 4, 3);
            txt_GP5.Name = "txt_GP5";
            txt_GP5.Size = new Size(312, 39);
            txt_GP5.TabIndex = 457;
            txt_GP5.UseWaitCursor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(13, 243);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(217, 32);
            label6.TabIndex = 461;
            label6.Text = "New XML LastNote";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(13, 195);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(186, 32);
            label7.TabIndex = 460;
            label7.Text = "Max real Lenght";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(13, 336);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(125, 32);
            label8.TabIndex = 465;
            label8.Text = "Correction";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(13, 289);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(132, 32);
            label9.TabIndex = 464;
            label9.Text = "Percentage";
            // 
            // txt_Correction
            // 
            txt_Correction.CustomFormat = "mm:ss.ms";
            txt_Correction.Enabled = false;
            txt_Correction.Format = DateTimePickerFormat.Custom;
            txt_Correction.Location = new Point(234, 336);
            txt_Correction.Margin = new Padding(4);
            txt_Correction.Name = "txt_Correction";
            txt_Correction.ShowUpDown = true;
            txt_Correction.Size = new Size(197, 39);
            txt_Correction.TabIndex = 463;
            txt_Correction.Value = new DateTime(2015, 5, 24, 0, 0, 0, 0);
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = SystemColors.ControlText;
            label10.Location = new Point(464, 207);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(167, 32);
            label10.TabIndex = 467;
            label10.Text = "Curent Lenght";
            // 
            // txt_MaxRealLenght
            // 
            txt_MaxRealLenght.Location = new Point(234, 198);
            txt_MaxRealLenght.Margin = new Padding(4, 3, 4, 3);
            txt_MaxRealLenght.Name = "txt_MaxRealLenght";
            txt_MaxRealLenght.Size = new Size(197, 39);
            txt_MaxRealLenght.TabIndex = 468;
            txt_MaxRealLenght.UseWaitCursor = true;
            // 
            // txt_Remaining
            // 
            txt_Remaining.Location = new Point(234, 243);
            txt_Remaining.Margin = new Padding(4, 3, 4, 3);
            txt_Remaining.Name = "txt_Remaining";
            txt_Remaining.Size = new Size(197, 39);
            txt_Remaining.TabIndex = 469;
            txt_Remaining.UseWaitCursor = true;
            // 
            // txt_Percentage
            // 
            txt_Percentage.Location = new Point(234, 289);
            txt_Percentage.Margin = new Padding(4, 3, 4, 3);
            txt_Percentage.Name = "txt_Percentage";
            txt_Percentage.Size = new Size(197, 39);
            txt_Percentage.TabIndex = 470;
            txt_Percentage.UseWaitCursor = true;
            // 
            // txt_CurrentLenght
            // 
            txt_CurrentLenght.Location = new Point(653, 207);
            txt_CurrentLenght.Margin = new Padding(4, 3, 4, 3);
            txt_CurrentLenght.Name = "txt_CurrentLenght";
            txt_CurrentLenght.Size = new Size(197, 39);
            txt_CurrentLenght.TabIndex = 471;
            txt_CurrentLenght.UseWaitCursor = true;
            // 
            // DistribXMLNotes
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(913, 971);
            Controls.Add(txt_CurrentLenght);
            Controls.Add(txt_Percentage);
            Controls.Add(txt_Remaining);
            Controls.Add(txt_MaxRealLenght);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(txt_Correction);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(txt_GP5);
            Controls.Add(txt_Ttime);
            Controls.Add(lbl_TempFolders);
            Controls.Add(txt_XMLPath);
            Controls.Add(lbl_Track);
            Controls.Add(chbx_BassToo);
            Controls.Add(btn_OpenXML);
            Controls.Add(btn_OpenGP5);
            Controls.Add(btn_EOF);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(xml_last);
            Controls.Add(xml_first);
            Controls.Add(txt_songLast);
            Controls.Add(label1);
            Controls.Add(txt_songStart);
            Controls.Add(txt_Description);
            Controls.Add(lbl_Comments);
            Controls.Add(btn_Apply);
            Controls.Add(btn_Estimate);
            Name = "DistribXMLNotes";
            Load += DistribXMLNotes_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            //StopImport = true;
            this.Hide();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //exit();
            this.Hide();
        }

        private void btn_StopImport_Click(object sender, EventArgs e)
        {
            //IgnoreSong = true;
            this.Hide();
        }

        private void lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Lbl_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            //Process.Start(txt_YBLink.Text);
        }

        public void DistribXMLNotes_Load(object sender, EventArgs e)
        {
            var ud = ""; var emt = false;
            var ArrangID = sXml.Substring(sXml.IndexOf("_ArrangementID="), sXml.Length - sXml.IndexOf("_ArrangementID=")).Replace("_ArrangementID=", "");
            var DLCID = sXml.Substring(8, sXml.IndexOf("_") - 8);

            //var i = databox.SelectedCells[0].RowIndex;
            string destination_dir = FilePath + (gPlatform.ToLower() == "XBOX360".ToLower() ? "\\Root" : "");

            DataSet dus = new DataSet(); dus = SelectFromDB("Arrangements", "SELECT XMLFilePath, XMLFileName" +
                ", RouteMask, Start_Time, ArrangementType,SNGFileName  FROM Arrangements WHERE ID=" + ArrangID + GetArrOfficSQLTxt(Arrangoff), "", cnb, cnc);
            var noOfRec = GetNoRec(dus, cnb, cnc); //dus.Tables[0].Rows.Count;
            var XMLFilePath = dus.Tables[0].Rows[0].ItemArray[0].ToString();
            var RouteMask = dus.Tables[0].Rows[0].ItemArray[2].ToString();
            string ArrangementType = dus.Tables[0].Rows[0].ItemArray[4].ToString();
            var newXMLFilePath = destination_dir + "\\songs\\arr\\" + dus.Tables[0].Rows[0].ItemArray[1].ToString() + ".xml";
            txt_XMLPath.Text = newXMLFilePath;
            string tx = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, false);// dus.Tables[0].Rows[0].ItemArray[3].ToString());
            string ty = GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, true);

            xml_first.Value = xml_first.Value.AddSeconds(tx.ToInt32());// ToString(@"hh\:mm\:ss\:fff"); ;
            xml_last.Value = xml_last.Value.AddSeconds(ty.ToInt32());/// DateTime.Parse(GetTrackStartTime(newXMLFilePath, RouteMask, ArrangementType, true));
            xml_first.Value = xml_first.Value.AddMilliseconds(Double.Parse(tx) - Math.Truncate(Double.Parse(tx)));
            xml_last.Value = xml_last.Value.AddMilliseconds(Double.Parse(ty) - Math.Truncate(Double.Parse(ty)));
            txt_CurrentLenght.Text = (xml_last.Value.Minute * 60 + xml_last.Value.Second + xml_last.Value.Millisecond / 1000
                - xml_first.Value.Minute * 60 - xml_first.Value.Second - xml_first.Value.Millisecond / 1000).ToString();
            if (File.Exists(BasedOn_CF)) txt_GP5.Text = BasedOn_CF;
            else txt_GP5.Text = GetGPfile();
        }

        public string GetGPfile()
        {
            if (EoFPath == "") return "";
            if (!File.Exists(EoFPath)) return "";
            var templateList = Directory.EnumerateFiles(Path.GetDirectoryName(EoFPath));
            var files = "";
            foreach (var template in templateList)
            {
                if (Path.GetExtension(template).ToLower() == ".gp" || Path.GetExtension(template).ToLower() == ".gpx" || Path.GetExtension(template).ToLower() == ".gp5")
                {
                    files += template + ",";
                }
            }
            return files;
        }

        public string calctime()
        {
            var files = "";
            txt_MaxRealLenght.Text = (txt_songLast.Value.Minute * 60 + txt_songLast.Value.Second + txt_songLast.Value.Millisecond / 1000
                - txt_songStart.Value.Minute * 60 - txt_songStart.Value.Second + txt_songStart.Value.Millisecond / 1000).ToString();//(xml_last.Value.Ticks - xml_first.Value.Ticks).ToString();
            txt_Percentage.Text = (double.Parse(txt_MaxRealLenght.Text) * 100 / double.Parse(txt_CurrentLenght.Text)).ToString();
            return txt_Percentage.Text;
        }

        private void btn_DBFolder_Click(object sender, EventArgs e)
        {
            var result1 = MessageBox.Show("chose file.", "GuitarPro file", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

        }

        private void btn_EOF_Click(object sender, EventArgs e)
        {
            StartProcesss("notepad.exe", txt_XMLPath.Text);
        }

        private void btn_Close_Click_1(object sender, EventArgs e)
        {
            calctime();
            perc_time_betw_notes = double.Parse(txt_Ttime.Text);
            this.Hide();
        }

        private void btn_Estimate_Click(object sender, EventArgs e)
        {
            calctime();
        }

        private void btn_OpenGP5_Click(object sender, EventArgs e)
        {
            StartProcesss("notepad.exe", txt_GP5.Text);
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
