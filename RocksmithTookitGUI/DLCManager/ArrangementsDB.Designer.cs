namespace RocksmithToolkitGUI.DLCManager
{
    partial class ArrangementsDB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.chbx_HasSection = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Rating = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.chbx_Tunning = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chbx_BassPicking = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chbx_ToneBase = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
            this.btn_RemoveDD = new System.Windows.Forms.Button();
            this.lbl_NoRec = new System.Windows.Forms.Label();
            this.btn_OpenJSON = new System.Windows.Forms.Button();
            this.btn_OpenXML = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_RouteMask = new RocksmithToolkitGUI.CueTextBox();
            this.txt_TuningPitch = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_ToneD = new System.Windows.Forms.ComboBox();
            this.chbx_ToneC = new System.Windows.Forms.ComboBox();
            this.chbx_ToneB = new System.Windows.Forms.ComboBox();
            this.chbx_ToneA = new System.Windows.Forms.ComboBox();
            this.txt_String0 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_String2 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_String3 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_String4 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_String5 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_String1 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_BassDD = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.btn_AddDD = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_lastConversionDateTime = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ArrangementType = new RocksmithToolkitGUI.CueTextBox();
            this.cueTextBox2 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Tuning_Speed = new RocksmithToolkitGUI.CueTextBox();
            this.txt_ScrollSpeed = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Bonus = new System.Windows.Forms.CheckBox();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.txt_XMLFilePath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_SNGFilePath = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CDLC_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Arrangement_Name = new RocksmithToolkitGUI.CueTextBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(766, 245);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 35);
            this.button1.TabIndex = 39;
            this.button1.Text = "Open DB in M$ Access";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(1177, 172);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_2);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_2);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_2);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.label12);
            this.Panel1.Controls.Add(this.chbx_HasSection);
            this.Panel1.Controls.Add(this.label10);
            this.Panel1.Controls.Add(this.txt_Rating);
            this.Panel1.Controls.Add(this.label11);
            this.Panel1.Controls.Add(this.txt_Description);
            this.Panel1.Controls.Add(this.chbx_Tunning);
            this.Panel1.Controls.Add(this.label9);
            this.Panel1.Controls.Add(this.chbx_BassPicking);
            this.Panel1.Controls.Add(this.label8);
            this.Panel1.Controls.Add(this.label7);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.chbx_ToneBase);
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.numericUpDown1);
            this.Panel1.Controls.Add(this.chbx_AutoSave);
            this.Panel1.Controls.Add(this.btn_RemoveDD);
            this.Panel1.Controls.Add(this.lbl_NoRec);
            this.Panel1.Controls.Add(this.btn_OpenJSON);
            this.Panel1.Controls.Add(this.btn_OpenXML);
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.txt_RouteMask);
            this.Panel1.Controls.Add(this.txt_TuningPitch);
            this.Panel1.Controls.Add(this.chbx_ToneD);
            this.Panel1.Controls.Add(this.chbx_ToneC);
            this.Panel1.Controls.Add(this.chbx_ToneB);
            this.Panel1.Controls.Add(this.chbx_ToneA);
            this.Panel1.Controls.Add(this.txt_String0);
            this.Panel1.Controls.Add(this.txt_String2);
            this.Panel1.Controls.Add(this.txt_String3);
            this.Panel1.Controls.Add(this.txt_String4);
            this.Panel1.Controls.Add(this.txt_String5);
            this.Panel1.Controls.Add(this.txt_String1);
            this.Panel1.Controls.Add(this.txt_ID);
            this.Panel1.Controls.Add(this.chbx_BassDD);
            this.Panel1.Controls.Add(this.button8);
            this.Panel1.Controls.Add(this.btn_AddDD);
            this.Panel1.Controls.Add(this.button3);
            this.Panel1.Controls.Add(this.txt_lastConversionDateTime);
            this.Panel1.Controls.Add(this.txt_ArrangementType);
            this.Panel1.Controls.Add(this.cueTextBox2);
            this.Panel1.Controls.Add(this.txt_Tuning_Speed);
            this.Panel1.Controls.Add(this.txt_ScrollSpeed);
            this.Panel1.Controls.Add(this.chbx_Bonus);
            this.Panel1.Controls.Add(this.chbx_Broken);
            this.Panel1.Controls.Add(this.txt_XMLFilePath);
            this.Panel1.Controls.Add(this.txt_SNGFilePath);
            this.Panel1.Controls.Add(this.txt_CDLC_ID);
            this.Panel1.Controls.Add(this.txt_Arrangement_Name);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 178);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1177, 114);
            this.Panel1.TabIndex = 40;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DarkRed;
            this.label12.Location = new System.Drawing.Point(805, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(183, 13);
            this.label12.TabIndex = 345;
            this.label12.Text = "Updated info not used @repack YET";
            // 
            // chbx_HasSection
            // 
            this.chbx_HasSection.AutoSize = true;
            this.chbx_HasSection.Enabled = false;
            this.chbx_HasSection.Location = new System.Drawing.Point(873, 24);
            this.chbx_HasSection.Name = "chbx_HasSection";
            this.chbx_HasSection.Size = new System.Drawing.Size(89, 17);
            this.chbx_HasSection.TabIndex = 344;
            this.chbx_HasSection.Text = "Has Sections";
            this.chbx_HasSection.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(464, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 18);
            this.label10.TabIndex = 342;
            this.label10.Text = "/5 CDLC stars";
            // 
            // txt_Rating
            // 
            this.txt_Rating.Location = new System.Drawing.Point(428, 59);
            this.txt_Rating.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Rating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_Rating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Rating.Name = "txt_Rating";
            this.txt_Rating.Size = new System.Drawing.Size(31, 20);
            this.txt_Rating.TabIndex = 343;
            this.txt_Rating.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(948, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 341;
            this.label11.Text = "Description:";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(1019, 6);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(155, 32);
            this.txt_Description.TabIndex = 340;
            this.txt_Description.Text = "";
            // 
            // chbx_Tunning
            // 
            this.chbx_Tunning.FormattingEnabled = true;
            this.chbx_Tunning.Items.AddRange(new object[] {
            "",
            "Picked",
            "Not Picked"});
            this.chbx_Tunning.Location = new System.Drawing.Point(427, 33);
            this.chbx_Tunning.Name = "chbx_Tunning";
            this.chbx_Tunning.Size = new System.Drawing.Size(121, 21);
            this.chbx_Tunning.TabIndex = 339;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(239, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 338;
            this.label9.Text = "Last Conv Time";
            // 
            // chbx_BassPicking
            // 
            this.chbx_BassPicking.FormattingEnabled = true;
            this.chbx_BassPicking.Items.AddRange(new object[] {
            "",
            "Picked",
            "Not Picked"});
            this.chbx_BassPicking.Location = new System.Drawing.Point(624, 85);
            this.chbx_BassPicking.Name = "chbx_BassPicking";
            this.chbx_BassPicking.Size = new System.Drawing.Size(121, 21);
            this.chbx_BassPicking.TabIndex = 337;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(137, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 336;
            this.label8.Text = "No. Ordr.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(511, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 335;
            this.label7.Text = "String";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(358, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 334;
            this.label6.Text = "Scrool Speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 333;
            this.label5.Text = "CDLC ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 332;
            this.label4.Text = "ID";
            // 
            // chbx_ToneBase
            // 
            this.chbx_ToneBase.FormattingEnabled = true;
            this.chbx_ToneBase.Location = new System.Drawing.Point(497, 85);
            this.chbx_ToneBase.Name = "chbx_ToneBase";
            this.chbx_ToneBase.Size = new System.Drawing.Size(121, 21);
            this.chbx_ToneBase.TabIndex = 331;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 330;
            this.label3.Text = "XML";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 329;
            this.label2.Text = "SNG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(805, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 328;
            this.label1.Text = "Route Mask";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(1105, 42);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown1.TabIndex = 327;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(936, 91);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(73, 17);
            this.chbx_AutoSave.TabIndex = 101;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveDD
            // 
            this.btn_RemoveDD.Enabled = false;
            this.btn_RemoveDD.Location = new System.Drawing.Point(1016, 64);
            this.btn_RemoveDD.Name = "btn_RemoveDD";
            this.btn_RemoveDD.Size = new System.Drawing.Size(156, 22);
            this.btn_RemoveDD.TabIndex = 116;
            this.btn_RemoveDD.Text = "Remove BassDD";
            this.btn_RemoveDD.UseVisualStyleBackColor = true;
            this.btn_RemoveDD.Click += new System.EventHandler(this.button10_Click);
            // 
            // lbl_NoRec
            // 
            this.lbl_NoRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NoRec.Location = new System.Drawing.Point(253, 28);
            this.lbl_NoRec.Name = "lbl_NoRec";
            this.lbl_NoRec.Size = new System.Drawing.Size(68, 12);
            this.lbl_NoRec.TabIndex = 278;
            this.lbl_NoRec.Text = " Records";
            // 
            // btn_OpenJSON
            // 
            this.btn_OpenJSON.Location = new System.Drawing.Point(27, 47);
            this.btn_OpenJSON.Name = "btn_OpenJSON";
            this.btn_OpenJSON.Size = new System.Drawing.Size(51, 38);
            this.btn_OpenJSON.TabIndex = 277;
            this.btn_OpenJSON.Text = "Open JSON";
            this.btn_OpenJSON.UseVisualStyleBackColor = true;
            this.btn_OpenJSON.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_OpenXML
            // 
            this.btn_OpenXML.Location = new System.Drawing.Point(80, 47);
            this.btn_OpenXML.Name = "btn_OpenXML";
            this.btn_OpenXML.Size = new System.Drawing.Size(51, 38);
            this.btn_OpenXML.TabIndex = 276;
            this.btn_OpenXML.Text = "Open XML";
            this.btn_OpenXML.UseVisualStyleBackColor = true;
            this.btn_OpenXML.Click += new System.EventHandler(this.btn_OpenXML_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(1100, 86);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 25);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_RouteMask
            // 
            this.txt_RouteMask.Cue = "Route Mask";
            this.txt_RouteMask.Enabled = false;
            this.txt_RouteMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_RouteMask.ForeColor = System.Drawing.Color.Gray;
            this.txt_RouteMask.Location = new System.Drawing.Point(804, 85);
            this.txt_RouteMask.Name = "txt_RouteMask";
            this.txt_RouteMask.Size = new System.Drawing.Size(107, 20);
            this.txt_RouteMask.TabIndex = 133;
            // 
            // txt_TuningPitch
            // 
            this.txt_TuningPitch.Cue = "Tuning Pitch";
            this.txt_TuningPitch.Enabled = false;
            this.txt_TuningPitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_TuningPitch.ForeColor = System.Drawing.Color.Gray;
            this.txt_TuningPitch.Location = new System.Drawing.Point(747, 8);
            this.txt_TuningPitch.Name = "txt_TuningPitch";
            this.txt_TuningPitch.Size = new System.Drawing.Size(56, 20);
            this.txt_TuningPitch.TabIndex = 132;
            // 
            // chbx_ToneD
            // 
            this.chbx_ToneD.FormattingEnabled = true;
            this.chbx_ToneD.Location = new System.Drawing.Point(682, 57);
            this.chbx_ToneD.Name = "chbx_ToneD";
            this.chbx_ToneD.Size = new System.Drawing.Size(121, 21);
            this.chbx_ToneD.TabIndex = 131;
            // 
            // chbx_ToneC
            // 
            this.chbx_ToneC.FormattingEnabled = true;
            this.chbx_ToneC.Location = new System.Drawing.Point(682, 34);
            this.chbx_ToneC.Name = "chbx_ToneC";
            this.chbx_ToneC.Size = new System.Drawing.Size(121, 21);
            this.chbx_ToneC.TabIndex = 130;
            // 
            // chbx_ToneB
            // 
            this.chbx_ToneB.FormattingEnabled = true;
            this.chbx_ToneB.Location = new System.Drawing.Point(555, 57);
            this.chbx_ToneB.Name = "chbx_ToneB";
            this.chbx_ToneB.Size = new System.Drawing.Size(121, 21);
            this.chbx_ToneB.TabIndex = 129;
            // 
            // chbx_ToneA
            // 
            this.chbx_ToneA.FormattingEnabled = true;
            this.chbx_ToneA.Location = new System.Drawing.Point(555, 34);
            this.chbx_ToneA.Name = "chbx_ToneA";
            this.chbx_ToneA.Size = new System.Drawing.Size(121, 21);
            this.chbx_ToneA.TabIndex = 128;
            // 
            // txt_String0
            // 
            this.txt_String0.Cue = "String 0";
            this.txt_String0.Enabled = false;
            this.txt_String0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String0.ForeColor = System.Drawing.Color.Gray;
            this.txt_String0.Location = new System.Drawing.Point(550, 9);
            this.txt_String0.Name = "txt_String0";
            this.txt_String0.Size = new System.Drawing.Size(27, 20);
            this.txt_String0.TabIndex = 127;
            // 
            // txt_String2
            // 
            this.txt_String2.Cue = "String";
            this.txt_String2.Enabled = false;
            this.txt_String2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String2.ForeColor = System.Drawing.Color.Gray;
            this.txt_String2.Location = new System.Drawing.Point(616, 9);
            this.txt_String2.Name = "txt_String2";
            this.txt_String2.Size = new System.Drawing.Size(27, 20);
            this.txt_String2.TabIndex = 126;
            // 
            // txt_String3
            // 
            this.txt_String3.Cue = "String";
            this.txt_String3.Enabled = false;
            this.txt_String3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String3.ForeColor = System.Drawing.Color.Gray;
            this.txt_String3.Location = new System.Drawing.Point(649, 9);
            this.txt_String3.Name = "txt_String3";
            this.txt_String3.Size = new System.Drawing.Size(27, 20);
            this.txt_String3.TabIndex = 125;
            // 
            // txt_String4
            // 
            this.txt_String4.Cue = "String";
            this.txt_String4.Enabled = false;
            this.txt_String4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String4.ForeColor = System.Drawing.Color.Gray;
            this.txt_String4.Location = new System.Drawing.Point(682, 9);
            this.txt_String4.Name = "txt_String4";
            this.txt_String4.Size = new System.Drawing.Size(27, 20);
            this.txt_String4.TabIndex = 124;
            // 
            // txt_String5
            // 
            this.txt_String5.Cue = "String";
            this.txt_String5.Enabled = false;
            this.txt_String5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String5.ForeColor = System.Drawing.Color.Gray;
            this.txt_String5.Location = new System.Drawing.Point(715, 9);
            this.txt_String5.Name = "txt_String5";
            this.txt_String5.Size = new System.Drawing.Size(27, 20);
            this.txt_String5.TabIndex = 123;
            // 
            // txt_String1
            // 
            this.txt_String1.Cue = "String";
            this.txt_String1.Enabled = false;
            this.txt_String1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_String1.ForeColor = System.Drawing.Color.Gray;
            this.txt_String1.Location = new System.Drawing.Point(583, 9);
            this.txt_String1.Name = "txt_String1";
            this.txt_String1.Size = new System.Drawing.Size(27, 20);
            this.txt_String1.TabIndex = 122;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(191, 89);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 121;
            // 
            // chbx_BassDD
            // 
            this.chbx_BassDD.AutoSize = true;
            this.chbx_BassDD.Enabled = false;
            this.chbx_BassDD.Location = new System.Drawing.Point(873, 7);
            this.chbx_BassDD.Name = "chbx_BassDD";
            this.chbx_BassDD.Size = new System.Drawing.Size(68, 17);
            this.chbx_BassDD.TabIndex = 119;
            this.chbx_BassDD.Text = "Bass DD";
            this.chbx_BassDD.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(1016, 86);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 25);
            this.button8.TabIndex = 118;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // btn_AddDD
            // 
            this.btn_AddDD.Enabled = false;
            this.btn_AddDD.Location = new System.Drawing.Point(1016, 42);
            this.btn_AddDD.Name = "btn_AddDD";
            this.btn_AddDD.Size = new System.Drawing.Size(81, 21);
            this.btn_AddDD.TabIndex = 115;
            this.btn_AddDD.Text = "Add DD";
            this.btn_AddDD.UseVisualStyleBackColor = true;
            this.btn_AddDD.Click += new System.EventHandler(this.btn_AddDD_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(27, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 40);
            this.button3.TabIndex = 114;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_lastConversionDateTime
            // 
            this.txt_lastConversionDateTime.Cue = "lastConversionDateTime";
            this.txt_lastConversionDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_lastConversionDateTime.ForeColor = System.Drawing.Color.Gray;
            this.txt_lastConversionDateTime.Location = new System.Drawing.Point(331, 89);
            this.txt_lastConversionDateTime.Name = "txt_lastConversionDateTime";
            this.txt_lastConversionDateTime.Size = new System.Drawing.Size(100, 20);
            this.txt_lastConversionDateTime.TabIndex = 113;
            // 
            // txt_ArrangementType
            // 
            this.txt_ArrangementType.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_ArrangementType.Cue = "Arrangement Type";
            this.txt_ArrangementType.Enabled = false;
            this.txt_ArrangementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ArrangementType.ForeColor = System.Drawing.Color.Gray;
            this.txt_ArrangementType.Location = new System.Drawing.Point(242, 3);
            this.txt_ArrangementType.Name = "txt_ArrangementType";
            this.txt_ArrangementType.Size = new System.Drawing.Size(113, 20);
            this.txt_ArrangementType.TabIndex = 111;
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "ScrollSpeed";
            this.cueTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cueTextBox2.ForeColor = System.Drawing.Color.Gray;
            this.cueTextBox2.Location = new System.Drawing.Point(747, 85);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.Size = new System.Drawing.Size(51, 20);
            this.cueTextBox2.TabIndex = 109;
            // 
            // txt_Tuning_Speed
            // 
            this.txt_Tuning_Speed.Cue = "Tuning Speed";
            this.txt_Tuning_Speed.Enabled = false;
            this.txt_Tuning_Speed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tuning_Speed.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tuning_Speed.Location = new System.Drawing.Point(435, 86);
            this.txt_Tuning_Speed.Name = "txt_Tuning_Speed";
            this.txt_Tuning_Speed.Size = new System.Drawing.Size(56, 20);
            this.txt_Tuning_Speed.TabIndex = 108;
            // 
            // txt_ScrollSpeed
            // 
            this.txt_ScrollSpeed.Cue = "ScrollSpeed";
            this.txt_ScrollSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ScrollSpeed.ForeColor = System.Drawing.Color.Gray;
            this.txt_ScrollSpeed.Location = new System.Drawing.Point(437, 8);
            this.txt_ScrollSpeed.Name = "txt_ScrollSpeed";
            this.txt_ScrollSpeed.Size = new System.Drawing.Size(56, 20);
            this.txt_ScrollSpeed.TabIndex = 105;
            // 
            // chbx_Bonus
            // 
            this.chbx_Bonus.AutoSize = true;
            this.chbx_Bonus.Enabled = false;
            this.chbx_Bonus.Location = new System.Drawing.Point(808, 8);
            this.chbx_Bonus.Name = "chbx_Bonus";
            this.chbx_Bonus.Size = new System.Drawing.Size(56, 17);
            this.chbx_Bonus.TabIndex = 104;
            this.chbx_Bonus.Text = "Bonus";
            this.chbx_Bonus.UseVisualStyleBackColor = true;
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Enabled = false;
            this.chbx_Broken.Location = new System.Drawing.Point(808, 24);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 102;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            this.chbx_Broken.Visible = false;
            // 
            // txt_XMLFilePath
            // 
            this.txt_XMLFilePath.Cue = "XMLFilePath";
            this.txt_XMLFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_XMLFilePath.ForeColor = System.Drawing.Color.Gray;
            this.txt_XMLFilePath.Location = new System.Drawing.Point(191, 65);
            this.txt_XMLFilePath.Name = "txt_XMLFilePath";
            this.txt_XMLFilePath.ReadOnly = true;
            this.txt_XMLFilePath.Size = new System.Drawing.Size(222, 20);
            this.txt_XMLFilePath.TabIndex = 69;
            this.txt_XMLFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_SNGFilePath
            // 
            this.txt_SNGFilePath.Cue = "SNGFilePath";
            this.txt_SNGFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_SNGFilePath.ForeColor = System.Drawing.Color.Gray;
            this.txt_SNGFilePath.Location = new System.Drawing.Point(191, 42);
            this.txt_SNGFilePath.Name = "txt_SNGFilePath";
            this.txt_SNGFilePath.ReadOnly = true;
            this.txt_SNGFilePath.Size = new System.Drawing.Size(222, 20);
            this.txt_SNGFilePath.TabIndex = 68;
            this.txt_SNGFilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_CDLC_ID
            // 
            this.txt_CDLC_ID.Cue = "CDLC_ID";
            this.txt_CDLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CDLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_CDLC_ID.Location = new System.Drawing.Point(191, 22);
            this.txt_CDLC_ID.Name = "txt_CDLC_ID";
            this.txt_CDLC_ID.ReadOnly = true;
            this.txt_CDLC_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_CDLC_ID.TabIndex = 67;
            // 
            // txt_Arrangement_Name
            // 
            this.txt_Arrangement_Name.Cue = "Arrangements ID";
            this.txt_Arrangement_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Arrangement_Name.ForeColor = System.Drawing.Color.Gray;
            this.txt_Arrangement_Name.Location = new System.Drawing.Point(191, 5);
            this.txt_Arrangement_Name.Name = "txt_Arrangement_Name";
            this.txt_Arrangement_Name.Size = new System.Drawing.Size(45, 20);
            this.txt_Arrangement_Name.TabIndex = 66;
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Enabled = false;
            this.CheckBox1.Location = new System.Drawing.Point(-153, 96);
            this.CheckBox1.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(139, 17);
            this.CheckBox1.TabIndex = 34;
            this.CheckBox1.Text = "Show only MessageBox";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // ArrangementsDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 292);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "ArrangementsDB";
            this.Text = "ArrangementsDB";
            this.Load += new System.EventHandler(this.ArrangementsDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Rating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.CheckBox CheckBox1;
        private CueTextBox txt_XMLFilePath;
        private CueTextBox txt_SNGFilePath;
        private CueTextBox txt_CDLC_ID;
        private CueTextBox txt_Arrangement_Name;
        private CueTextBox txt_ArrangementType;
        private CueTextBox cueTextBox2;
        private CueTextBox txt_Tuning_Speed;
        private CueTextBox txt_ScrollSpeed;
        private System.Windows.Forms.CheckBox chbx_Bonus;
        private System.Windows.Forms.CheckBox chbx_Broken;
        private CueTextBox txt_lastConversionDateTime;
        private System.Windows.Forms.Button btn_RemoveDD;
        private System.Windows.Forms.Button btn_AddDD;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chbx_BassDD;
        private System.Windows.Forms.Button button8;
        private CueTextBox txt_ID;
        private System.Windows.Forms.ComboBox chbx_ToneD;
        private System.Windows.Forms.ComboBox chbx_ToneC;
        private System.Windows.Forms.ComboBox chbx_ToneB;
        private System.Windows.Forms.ComboBox chbx_ToneA;
        private CueTextBox txt_String0;
        private CueTextBox txt_String2;
        private CueTextBox txt_String3;
        private CueTextBox txt_String4;
        private CueTextBox txt_String5;
        private CueTextBox txt_String1;
        private CueTextBox txt_TuningPitch;
        private CueTextBox txt_RouteMask;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_OpenJSON;
        private System.Windows.Forms.Button btn_OpenXML;
        private System.Windows.Forms.Label lbl_NoRec;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox chbx_ToneBase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox chbx_Tunning;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox chbx_BassPicking;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txt_Rating;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox txt_Description;
        private System.Windows.Forms.CheckBox chbx_HasSection;
        private System.Windows.Forms.Label label12;
    }
}