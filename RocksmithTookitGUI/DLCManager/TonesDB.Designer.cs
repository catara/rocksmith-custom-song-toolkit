namespace RocksmithToolkitGUI.DLCManager
{
    partial class TonesDB
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
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_lastConversionDateTime = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_AmpPedalKey = new System.Windows.Forms.ComboBox();
            this.chbx_AmpKnobValues = new System.Windows.Forms.ComboBox();
            this.chbx_AmpCategory = new System.Windows.Forms.ComboBox();
            this.chbx_AmpType = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetType = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetPedalKey = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetKnobValues = new System.Windows.Forms.ComboBox();
            this.chbx_CabinetCategory = new System.Windows.Forms.ComboBox();
            this.txt_Keyy = new RocksmithToolkitGUI.CueTextBox();
            this.chbx_Custom = new System.Windows.Forms.CheckBox();
            this.txt_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_CDLC_ID = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Tone_Name = new RocksmithToolkitGUI.CueTextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.chbx_Broken = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.chbx_AutoSave = new System.Windows.Forms.CheckBox();
            this.cueTextBox1 = new RocksmithToolkitGUI.CueTextBox();
            this.txt_Volume = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Description = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Volume)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Location = new System.Drawing.Point(0, 0);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(989, 149);
            this.DataGridView1.TabIndex = 38;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick_1);
            this.DataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellLeave);
            this.DataGridView1.SelectionChanged += new System.EventHandler(this.DataGridView1_SelectionChanged);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.label12);
            this.Panel1.Controls.Add(this.label6);
            this.Panel1.Controls.Add(this.label7);
            this.Panel1.Controls.Add(this.label8);
            this.Panel1.Controls.Add(this.label9);
            this.Panel1.Controls.Add(this.label11);
            this.Panel1.Controls.Add(this.txt_Description);
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Controls.Add(this.label4);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.txt_Volume);
            this.Panel1.Controls.Add(this.cueTextBox1);
            this.Panel1.Controls.Add(this.chbx_AutoSave);
            this.Panel1.Controls.Add(this.button1);
            this.Panel1.Controls.Add(this.btn_Close);
            this.Panel1.Controls.Add(this.txt_lastConversionDateTime);
            this.Panel1.Controls.Add(this.chbx_AmpPedalKey);
            this.Panel1.Controls.Add(this.chbx_AmpKnobValues);
            this.Panel1.Controls.Add(this.chbx_AmpCategory);
            this.Panel1.Controls.Add(this.chbx_AmpType);
            this.Panel1.Controls.Add(this.chbx_CabinetType);
            this.Panel1.Controls.Add(this.chbx_CabinetPedalKey);
            this.Panel1.Controls.Add(this.chbx_CabinetKnobValues);
            this.Panel1.Controls.Add(this.chbx_CabinetCategory);
            this.Panel1.Controls.Add(this.txt_Keyy);
            this.Panel1.Controls.Add(this.chbx_Custom);
            this.Panel1.Controls.Add(this.txt_ID);
            this.Panel1.Controls.Add(this.txt_CDLC_ID);
            this.Panel1.Controls.Add(this.txt_Tone_Name);
            this.Panel1.Controls.Add(this.button8);
            this.Panel1.Controls.Add(this.button3);
            this.Panel1.Controls.Add(this.chbx_Broken);
            this.Panel1.Controls.Add(this.CheckBox1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 155);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(989, 114);
            this.Panel1.TabIndex = 41;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 36);
            this.button1.TabIndex = 274;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(901, 86);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(84, 21);
            this.btn_Close.TabIndex = 273;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_lastConversionDateTime
            // 
            this.txt_lastConversionDateTime.Cue = "lastConversionDateTime";
            this.txt_lastConversionDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_lastConversionDateTime.ForeColor = System.Drawing.Color.Gray;
            this.txt_lastConversionDateTime.Location = new System.Drawing.Point(823, 3);
            this.txt_lastConversionDateTime.Name = "txt_lastConversionDateTime";
            this.txt_lastConversionDateTime.Size = new System.Drawing.Size(113, 20);
            this.txt_lastConversionDateTime.TabIndex = 139;
            this.txt_lastConversionDateTime.Visible = false;
            // 
            // chbx_AmpPedalKey
            // 
            this.chbx_AmpPedalKey.FormattingEnabled = true;
            this.chbx_AmpPedalKey.Location = new System.Drawing.Point(456, 81);
            this.chbx_AmpPedalKey.Name = "chbx_AmpPedalKey";
            this.chbx_AmpPedalKey.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpPedalKey.TabIndex = 138;
            this.chbx_AmpPedalKey.SelectedIndexChanged += new System.EventHandler(this.chbx_AmpPedalKey_SelectedIndexChanged);
            // 
            // chbx_AmpKnobValues
            // 
            this.chbx_AmpKnobValues.FormattingEnabled = true;
            this.chbx_AmpKnobValues.Location = new System.Drawing.Point(456, 54);
            this.chbx_AmpKnobValues.Name = "chbx_AmpKnobValues";
            this.chbx_AmpKnobValues.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpKnobValues.TabIndex = 137;
            this.chbx_AmpKnobValues.SelectedIndexChanged += new System.EventHandler(this.chbx_AmpKnobValues_SelectedIndexChanged);
            // 
            // chbx_AmpCategory
            // 
            this.chbx_AmpCategory.FormattingEnabled = true;
            this.chbx_AmpCategory.Location = new System.Drawing.Point(456, 27);
            this.chbx_AmpCategory.Name = "chbx_AmpCategory";
            this.chbx_AmpCategory.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpCategory.TabIndex = 136;
            this.chbx_AmpCategory.SelectedIndexChanged += new System.EventHandler(this.chbx_AmpCategory_SelectedIndexChanged);
            // 
            // chbx_AmpType
            // 
            this.chbx_AmpType.FormattingEnabled = true;
            this.chbx_AmpType.Location = new System.Drawing.Point(456, 0);
            this.chbx_AmpType.Name = "chbx_AmpType";
            this.chbx_AmpType.Size = new System.Drawing.Size(121, 21);
            this.chbx_AmpType.TabIndex = 135;
            this.chbx_AmpType.SelectedIndexChanged += new System.EventHandler(this.chbx_AmpType_SelectedIndexChanged);
            // 
            // chbx_CabinetType
            // 
            this.chbx_CabinetType.FormattingEnabled = true;
            this.chbx_CabinetType.Location = new System.Drawing.Point(696, 82);
            this.chbx_CabinetType.Name = "chbx_CabinetType";
            this.chbx_CabinetType.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetType.TabIndex = 134;
            // 
            // chbx_CabinetPedalKey
            // 
            this.chbx_CabinetPedalKey.FormattingEnabled = true;
            this.chbx_CabinetPedalKey.Location = new System.Drawing.Point(696, 55);
            this.chbx_CabinetPedalKey.Name = "chbx_CabinetPedalKey";
            this.chbx_CabinetPedalKey.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetPedalKey.TabIndex = 133;
            // 
            // chbx_CabinetKnobValues
            // 
            this.chbx_CabinetKnobValues.FormattingEnabled = true;
            this.chbx_CabinetKnobValues.Location = new System.Drawing.Point(696, 28);
            this.chbx_CabinetKnobValues.Name = "chbx_CabinetKnobValues";
            this.chbx_CabinetKnobValues.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetKnobValues.TabIndex = 132;
            // 
            // chbx_CabinetCategory
            // 
            this.chbx_CabinetCategory.FormattingEnabled = true;
            this.chbx_CabinetCategory.Location = new System.Drawing.Point(696, 1);
            this.chbx_CabinetCategory.Name = "chbx_CabinetCategory";
            this.chbx_CabinetCategory.Size = new System.Drawing.Size(121, 21);
            this.chbx_CabinetCategory.TabIndex = 131;
            // 
            // txt_Keyy
            // 
            this.txt_Keyy.Cue = "Key";
            this.txt_Keyy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Keyy.ForeColor = System.Drawing.Color.Gray;
            this.txt_Keyy.Location = new System.Drawing.Point(143, 44);
            this.txt_Keyy.Name = "txt_Keyy";
            this.txt_Keyy.Size = new System.Drawing.Size(56, 20);
            this.txt_Keyy.TabIndex = 130;
            // 
            // chbx_Custom
            // 
            this.chbx_Custom.AutoSize = true;
            this.chbx_Custom.Enabled = false;
            this.chbx_Custom.Location = new System.Drawing.Point(823, 51);
            this.chbx_Custom.Name = "chbx_Custom";
            this.chbx_Custom.Size = new System.Drawing.Size(61, 17);
            this.chbx_Custom.TabIndex = 128;
            this.chbx_Custom.Text = "Custom";
            this.chbx_Custom.UseVisualStyleBackColor = true;
            // 
            // txt_ID
            // 
            this.txt_ID.Cue = "ID";
            this.txt_ID.Enabled = false;
            this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_ID.Location = new System.Drawing.Point(143, 89);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(45, 20);
            this.txt_ID.TabIndex = 127;
            // 
            // txt_CDLC_ID
            // 
            this.txt_CDLC_ID.Cue = "CDLC_ID";
            this.txt_CDLC_ID.Enabled = false;
            this.txt_CDLC_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_CDLC_ID.ForeColor = System.Drawing.Color.Gray;
            this.txt_CDLC_ID.Location = new System.Drawing.Point(143, 22);
            this.txt_CDLC_ID.Name = "txt_CDLC_ID";
            this.txt_CDLC_ID.Size = new System.Drawing.Size(56, 20);
            this.txt_CDLC_ID.TabIndex = 126;
            // 
            // txt_Tone_Name
            // 
            this.txt_Tone_Name.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txt_Tone_Name.Cue = "Tone Name";
            this.txt_Tone_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_Tone_Name.ForeColor = System.Drawing.Color.Gray;
            this.txt_Tone_Name.Location = new System.Drawing.Point(143, 1);
            this.txt_Tone_Name.Name = "txt_Tone_Name";
            this.txt_Tone_Name.Size = new System.Drawing.Size(222, 20);
            this.txt_Tone_Name.TabIndex = 125;
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Green;
            this.button8.Location = new System.Drawing.Point(901, 60);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(84, 27);
            this.button8.TabIndex = 123;
            this.button8.Text = "Save";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 36);
            this.button3.TabIndex = 122;
            this.button3.Text = "Open DB in M$ Access";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbx_Broken
            // 
            this.chbx_Broken.AutoSize = true;
            this.chbx_Broken.Enabled = false;
            this.chbx_Broken.Location = new System.Drawing.Point(835, 90);
            this.chbx_Broken.Name = "chbx_Broken";
            this.chbx_Broken.Size = new System.Drawing.Size(60, 17);
            this.chbx_Broken.TabIndex = 121;
            this.chbx_Broken.Text = "Broken";
            this.chbx_Broken.UseVisualStyleBackColor = true;
            this.chbx_Broken.Visible = false;
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
            // chbx_AutoSave
            // 
            this.chbx_AutoSave.AutoSize = true;
            this.chbx_AutoSave.Checked = true;
            this.chbx_AutoSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_AutoSave.Location = new System.Drawing.Point(822, 70);
            this.chbx_AutoSave.Name = "chbx_AutoSave";
            this.chbx_AutoSave.Size = new System.Drawing.Size(73, 17);
            this.chbx_AutoSave.TabIndex = 275;
            this.chbx_AutoSave.Text = "AutoSave";
            this.chbx_AutoSave.UseVisualStyleBackColor = true;
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = "lastConversionDateTime";
            this.cueTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cueTextBox1.ForeColor = System.Drawing.Color.Gray;
            this.cueTextBox1.Location = new System.Drawing.Point(823, 27);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(113, 20);
            this.cueTextBox1.TabIndex = 276;
            this.cueTextBox1.Visible = false;
            // 
            // txt_Volume
            // 
            this.txt_Volume.DecimalPlaces = 2;
            this.txt_Volume.Location = new System.Drawing.Point(166, 66);
            this.txt_Volume.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txt_Volume.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.txt_Volume.Name = "txt_Volume";
            this.txt_Volume.Size = new System.Drawing.Size(60, 20);
            this.txt_Volume.TabIndex = 326;
            this.txt_Volume.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 331;
            this.label3.Text = "Volume";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 332;
            this.label1.Text = "AmpType";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 333;
            this.label2.Text = "AmpCategory";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(369, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 334;
            this.label4.Text = "AmpKnobValues";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 335;
            this.label5.Text = "AmpPedalKey";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(205, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 343;
            this.label11.Text = "Description:";
            // 
            // txt_Description
            // 
            this.txt_Description.Location = new System.Drawing.Point(227, 39);
            this.txt_Description.Name = "txt_Description";
            this.txt_Description.Size = new System.Drawing.Size(136, 67);
            this.txt_Description.TabIndex = 342;
            this.txt_Description.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(595, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 347;
            this.label6.Text = "CabinetType";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(595, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 346;
            this.label7.Text = "CabinetPedalKey";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(595, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 345;
            this.label8.Text = "CabinetKnobValues";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(595, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 344;
            this.label9.Text = "CabinetCategory";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DarkRed;
            this.label12.Location = new System.Drawing.Point(524, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(171, 13);
            this.label12.TabIndex = 348;
            this.label12.Text = "Information not user @repack YET";
            // 
            // TonesDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 269);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "TonesDB";
            this.Text = "TonesDB";
            this.Load += new System.EventHandler(this.TonesDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Volume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chbx_Broken;
        private CueTextBox txt_Keyy;
        private System.Windows.Forms.CheckBox chbx_Custom;
        private CueTextBox txt_ID;
        private CueTextBox txt_CDLC_ID;
        private CueTextBox txt_Tone_Name;
        private System.Windows.Forms.ComboBox chbx_AmpPedalKey;
        private System.Windows.Forms.ComboBox chbx_AmpKnobValues;
        private System.Windows.Forms.ComboBox chbx_AmpCategory;
        private System.Windows.Forms.ComboBox chbx_AmpType;
        private System.Windows.Forms.ComboBox chbx_CabinetType;
        private System.Windows.Forms.ComboBox chbx_CabinetPedalKey;
        private System.Windows.Forms.ComboBox chbx_CabinetKnobValues;
        private System.Windows.Forms.ComboBox chbx_CabinetCategory;
        private CueTextBox txt_lastConversionDateTime;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chbx_AutoSave;
        private CueTextBox cueTextBox1;
        private System.Windows.Forms.NumericUpDown txt_Volume;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox txt_Description;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
    }
}