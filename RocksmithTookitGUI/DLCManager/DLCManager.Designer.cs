﻿namespace RocksmithToolkitGUI.DLCManager
{
    partial class DLCManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbx_Export = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtn_Population_Groups = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_All = new System.Windows.Forms.RadioButton();
            this.rbtn_Population_Selected = new System.Windows.Forms.RadioButton();
            this.mainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Export_To = new System.Windows.Forms.Button();
            this.chbx_Rebuild = new System.Windows.Forms.CheckBox();
            this.btn_Cleanup_MainDB = new System.Windows.Forms.Button();
            this.cbx_Groups = new System.Windows.Forms.ComboBox();
            this.chbx_Additional_Manipualtions = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_Preview_Artist_Sort = new System.Windows.Forms.Button();
            this.cbx_Activ_Artist_Sort = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_Artist_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Artist_Sort = new System.Windows.Forms.TextBox();
            this.btn_Preview_File_Name = new System.Windows.Forms.Button();
            this.btn_Preview_Album = new System.Windows.Forms.Button();
            this.btn_Preview_Artist = new System.Windows.Forms.Button();
            this.btn_Preview_Title_Sort = new System.Windows.Forms.Button();
            this.btn_Preview_Title = new System.Windows.Forms.Button();
            this.cbx_Activ_File_Name = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Album = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Artist = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Title_Sort = new System.Windows.Forms.CheckBox();
            this.cbx_Activ_Title = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbx_Artist = new System.Windows.Forms.ComboBox();
            this.txt_Artist = new System.Windows.Forms.TextBox();
            this.cbx_Title = new System.Windows.Forms.ComboBox();
            this.txt_Title = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_File_Name = new System.Windows.Forms.ComboBox();
            this.txt_File_Name = new System.Windows.Forms.TextBox();
            this.cbx_Album = new System.Windows.Forms.ComboBox();
            this.txt_Album = new System.Windows.Forms.TextBox();
            this.cbx_Title_Sort = new System.Windows.Forms.ComboBox();
            this.txt_Title_Sort = new System.Windows.Forms.TextBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbx_CleanDLC = new System.Windows.Forms.CheckBox();
            this.rbtn_Mac = new System.Windows.Forms.RadioButton();
            this.pnl_ApplyChanges = new System.Windows.Forms.Panel();
            this.rbtn_Xbox = new System.Windows.Forms.RadioButton();
            this.rbtn_PS3 = new System.Windows.Forms.RadioButton();
            this.rbtn_All = new System.Windows.Forms.RadioButton();
            this.chbx_DebugB = new System.Windows.Forms.CheckBox();
            this.txt_DBFolder = new System.Windows.Forms.TextBox();
            this.btn_DBFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txt_RocksmithDLCPath = new System.Windows.Forms.TextBox();
            this.txt_TempPath = new System.Windows.Forms.TextBox();
            this.chbx_CleanTemp = new System.Windows.Forms.CheckBox();
            this.btn_SteamDLCFolder = new System.Windows.Forms.Button();
            this.chbx_CleanDB = new System.Windows.Forms.CheckBox();
            this.lbl_StatisticsOnReadDLCs = new System.Windows.Forms.Label();
            this.rtxt_StatisticsOnReadDLCs = new System.Windows.Forms.RichTextBox();
            this.pB_ReadDLCs = new System.Windows.Forms.ProgressBar();
            this.btn_PopulateDB = new System.Windows.Forms.Button();
            this.btn_DecompressAll = new System.Windows.Forms.Button();
            this.lbl_RocksmithDLCPath = new System.Windows.Forms.Label();
            this.btn_TempPath = new System.Windows.Forms.Button();
            this.lbl_TempFolders = new System.Windows.Forms.Label();
            this.lbl_PreviewText = new System.Windows.Forms.Label();
            this.grpbx_ApplyChanges = new System.Windows.Forms.GroupBox();
            this.btn_RePack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnl_ApplyChanges.SuspendLayout();
            this.grpbx_ApplyChanges.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbx_Export
            // 
            this.cbx_Export.Enabled = false;
            this.cbx_Export.FormattingEnabled = true;
            this.cbx_Export.Items.AddRange(new object[] {
            "Excel",
            "WebPage"});
            this.cbx_Export.Location = new System.Drawing.Point(364, 359);
            this.cbx_Export.Name = "cbx_Export";
            this.cbx_Export.Size = new System.Drawing.Size(61, 21);
            this.cbx_Export.TabIndex = 326;
            this.cbx_Export.Text = "Excel";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtn_Population_Groups);
            this.panel2.Controls.Add(this.rbtn_Population_All);
            this.panel2.Controls.Add(this.rbtn_Population_Selected);
            this.panel2.Location = new System.Drawing.Point(6, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 39);
            this.panel2.TabIndex = 204;
            // 
            // rbtn_Population_Groups
            // 
            this.rbtn_Population_Groups.AutoSize = true;
            this.rbtn_Population_Groups.Location = new System.Drawing.Point(3, 19);
            this.rbtn_Population_Groups.Name = "rbtn_Population_Groups";
            this.rbtn_Population_Groups.Size = new System.Drawing.Size(59, 17);
            this.rbtn_Population_Groups.TabIndex = 206;
            this.rbtn_Population_Groups.Text = "Groups";
            this.rbtn_Population_Groups.UseVisualStyleBackColor = true;
            this.rbtn_Population_Groups.CheckedChanged += new System.EventHandler(this.rbtn_Population_Groups_CheckedChanged);
            // 
            // rbtn_Population_All
            // 
            this.rbtn_Population_All.AutoSize = true;
            this.rbtn_Population_All.Checked = true;
            this.rbtn_Population_All.Location = new System.Drawing.Point(71, 3);
            this.rbtn_Population_All.Margin = new System.Windows.Forms.Padding(0);
            this.rbtn_Population_All.Name = "rbtn_Population_All";
            this.rbtn_Population_All.Size = new System.Drawing.Size(36, 17);
            this.rbtn_Population_All.TabIndex = 205;
            this.rbtn_Population_All.TabStop = true;
            this.rbtn_Population_All.Text = "All";
            this.rbtn_Population_All.UseVisualStyleBackColor = true;
            // 
            // rbtn_Population_Selected
            // 
            this.rbtn_Population_Selected.AutoSize = true;
            this.rbtn_Population_Selected.Location = new System.Drawing.Point(3, 3);
            this.rbtn_Population_Selected.Name = "rbtn_Population_Selected";
            this.rbtn_Population_Selected.Size = new System.Drawing.Size(67, 17);
            this.rbtn_Population_Selected.TabIndex = 204;
            this.rbtn_Population_Selected.Text = "Selected";
            this.rbtn_Population_Selected.UseVisualStyleBackColor = true;
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataMember = "Main";
            // 
            // Export_To
            // 
            this.Export_To.BackColor = System.Drawing.SystemColors.Control;
            this.Export_To.Enabled = false;
            this.Export_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Export_To.Location = new System.Drawing.Point(286, 359);
            this.Export_To.Margin = new System.Windows.Forms.Padding(2);
            this.Export_To.Name = "Export_To";
            this.Export_To.Size = new System.Drawing.Size(76, 21);
            this.Export_To.TabIndex = 325;
            this.Export_To.Text = "Export";
            this.Export_To.UseVisualStyleBackColor = false;
            this.Export_To.Click += new System.EventHandler(this.Export_To_Click);
            // 
            // chbx_Rebuild
            // 
            this.chbx_Rebuild.AutoSize = true;
            this.chbx_Rebuild.Enabled = false;
            this.chbx_Rebuild.Location = new System.Drawing.Point(378, 44);
            this.chbx_Rebuild.Name = "chbx_Rebuild";
            this.chbx_Rebuild.Size = new System.Drawing.Size(86, 17);
            this.chbx_Rebuild.TabIndex = 324;
            this.chbx_Rebuild.Text = "Rebuild Only";
            this.chbx_Rebuild.UseVisualStyleBackColor = true;
            // 
            // btn_Cleanup_MainDB
            // 
            this.btn_Cleanup_MainDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cleanup_MainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cleanup_MainDB.Location = new System.Drawing.Point(430, 359);
            this.btn_Cleanup_MainDB.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Cleanup_MainDB.Name = "btn_Cleanup_MainDB";
            this.btn_Cleanup_MainDB.Size = new System.Drawing.Size(106, 21);
            this.btn_Cleanup_MainDB.TabIndex = 323;
            this.btn_Cleanup_MainDB.Text = "Remove Only";
            this.btn_Cleanup_MainDB.UseVisualStyleBackColor = false;
            this.btn_Cleanup_MainDB.Click += new System.EventHandler(this.btn_Cleanup_MainDB_Click);
            // 
            // cbx_Groups
            // 
            this.cbx_Groups.DataSource = this.mainBindingSource;
            this.cbx_Groups.Enabled = false;
            this.cbx_Groups.FormattingEnabled = true;
            this.cbx_Groups.Location = new System.Drawing.Point(91, 185);
            this.cbx_Groups.Name = "cbx_Groups";
            this.cbx_Groups.Size = new System.Drawing.Size(120, 21);
            this.cbx_Groups.TabIndex = 322;
            // 
            // chbx_Additional_Manipualtions
            // 
            this.chbx_Additional_Manipualtions.FormattingEnabled = true;
            this.chbx_Additional_Manipualtions.Items.AddRange(new object[] {
            "1. When Packing Add Increment to all songs Title",
            "2. When Packing Add Increment to all songs Title per artist",
            "3. When Packing Make all DLC IDs unique(&save)",
            "<4. Add DD (4 Levels)>",
            "<5. Remove DD>",
            "<6. Remove DD only for Bass Guitar>",
            "<7. Remove the 4sec of the Preview song>",
            "8. Don\'t repack Broken songs",
            "9. Pack to cross-platform Compatible Filenames",
            "<10. Generate random 30sec Preview>",
            "<11. Use shortnames in the Filename for Artist&Album>",
            "<12. Repack Originals>",
            "<13. Repack PC>",
            "14. Import all Duplicates as Alternates",
            "15. Import any Custom as Alternate if an Original exists",
            "16. Move Original Imported files to temp/0_old"});
            this.chbx_Additional_Manipualtions.Location = new System.Drawing.Point(240, 88);
            this.chbx_Additional_Manipualtions.Name = "chbx_Additional_Manipualtions";
            this.chbx_Additional_Manipualtions.Size = new System.Drawing.Size(295, 109);
            this.chbx_Additional_Manipualtions.TabIndex = 321;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(484, 199);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 320;
            this.label10.Text = "Activ|Prev";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(5, 345);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 319;
            this.label8.Text = "Mask Preview:";
            // 
            // btn_Preview_Artist_Sort
            // 
            this.btn_Preview_Artist_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist_Sort.Location = new System.Drawing.Point(519, 279);
            this.btn_Preview_Artist_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Artist_Sort.Name = "btn_Preview_Artist_Sort";
            this.btn_Preview_Artist_Sort.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Artist_Sort.TabIndex = 318;
            this.btn_Preview_Artist_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist_Sort.Click += new System.EventHandler(this.btn_Preview_Artist_Sort_Click);
            // 
            // cbx_Activ_Artist_Sort
            // 
            this.cbx_Activ_Artist_Sort.AutoSize = true;
            this.cbx_Activ_Artist_Sort.Checked = true;
            this.cbx_Activ_Artist_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist_Sort.Location = new System.Drawing.Point(505, 280);
            this.cbx_Activ_Artist_Sort.Name = "cbx_Activ_Artist_Sort";
            this.cbx_Activ_Artist_Sort.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Artist_Sort.TabIndex = 317;
            this.cbx_Activ_Artist_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_Sort_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(5, 279);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 316;
            this.label3.Text = "Artist_Sort";
            // 
            // cbx_Artist_Sort
            // 
            this.cbx_Artist_Sort.FormattingEnabled = true;
            this.cbx_Artist_Sort.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Artist_Sort.Location = new System.Drawing.Point(398, 276);
            this.cbx_Artist_Sort.Name = "cbx_Artist_Sort";
            this.cbx_Artist_Sort.Size = new System.Drawing.Size(105, 21);
            this.cbx_Artist_Sort.TabIndex = 315;
            this.cbx_Artist_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_Sort_SelectedIndexChanged);
            // 
            // txt_Artist_Sort
            // 
            this.txt_Artist_Sort.Location = new System.Drawing.Point(54, 277);
            this.txt_Artist_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Artist_Sort.Name = "txt_Artist_Sort";
            this.txt_Artist_Sort.Size = new System.Drawing.Size(341, 20);
            this.txt_Artist_Sort.TabIndex = 314;
            this.txt_Artist_Sort.Text = "<Beta><Artist>";
            // 
            // btn_Preview_File_Name
            // 
            this.btn_Preview_File_Name.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_File_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_File_Name.Location = new System.Drawing.Point(519, 325);
            this.btn_Preview_File_Name.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_File_Name.Name = "btn_Preview_File_Name";
            this.btn_Preview_File_Name.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_File_Name.TabIndex = 313;
            this.btn_Preview_File_Name.UseVisualStyleBackColor = false;
            this.btn_Preview_File_Name.Click += new System.EventHandler(this.btn_Preview_File_Name_Click);
            // 
            // btn_Preview_Album
            // 
            this.btn_Preview_Album.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Album.Location = new System.Drawing.Point(519, 301);
            this.btn_Preview_Album.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Album.Name = "btn_Preview_Album";
            this.btn_Preview_Album.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Album.TabIndex = 312;
            this.btn_Preview_Album.UseVisualStyleBackColor = false;
            this.btn_Preview_Album.Click += new System.EventHandler(this.btn_Preview_Album_Click);
            // 
            // btn_Preview_Artist
            // 
            this.btn_Preview_Artist.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Artist.Location = new System.Drawing.Point(519, 258);
            this.btn_Preview_Artist.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Artist.Name = "btn_Preview_Artist";
            this.btn_Preview_Artist.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Artist.TabIndex = 311;
            this.btn_Preview_Artist.UseVisualStyleBackColor = false;
            this.btn_Preview_Artist.Click += new System.EventHandler(this.btn_Preview_Artist_Click);
            // 
            // btn_Preview_Title_Sort
            // 
            this.btn_Preview_Title_Sort.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title_Sort.Location = new System.Drawing.Point(519, 236);
            this.btn_Preview_Title_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Title_Sort.Name = "btn_Preview_Title_Sort";
            this.btn_Preview_Title_Sort.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Title_Sort.TabIndex = 310;
            this.btn_Preview_Title_Sort.UseVisualStyleBackColor = false;
            this.btn_Preview_Title_Sort.Click += new System.EventHandler(this.btn_Preview_Title_Sort_Click);
            // 
            // btn_Preview_Title
            // 
            this.btn_Preview_Title.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Preview_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Preview_Title.Location = new System.Drawing.Point(519, 215);
            this.btn_Preview_Title.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preview_Title.Name = "btn_Preview_Title";
            this.btn_Preview_Title.Size = new System.Drawing.Size(14, 15);
            this.btn_Preview_Title.TabIndex = 309;
            this.btn_Preview_Title.UseVisualStyleBackColor = false;
            this.btn_Preview_Title.Click += new System.EventHandler(this.btn_Preview_Title_Click);
            // 
            // cbx_Activ_File_Name
            // 
            this.cbx_Activ_File_Name.AutoSize = true;
            this.cbx_Activ_File_Name.Checked = true;
            this.cbx_Activ_File_Name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_File_Name.Location = new System.Drawing.Point(505, 326);
            this.cbx_Activ_File_Name.Name = "cbx_Activ_File_Name";
            this.cbx_Activ_File_Name.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_File_Name.TabIndex = 308;
            this.cbx_Activ_File_Name.UseVisualStyleBackColor = true;
            this.cbx_Activ_File_Name.CheckedChanged += new System.EventHandler(this.cbx_Activ_File_Name_CheckedChanged);
            // 
            // cbx_Activ_Album
            // 
            this.cbx_Activ_Album.AutoSize = true;
            this.cbx_Activ_Album.Checked = true;
            this.cbx_Activ_Album.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Album.Location = new System.Drawing.Point(505, 302);
            this.cbx_Activ_Album.Name = "cbx_Activ_Album";
            this.cbx_Activ_Album.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Album.TabIndex = 307;
            this.cbx_Activ_Album.UseVisualStyleBackColor = true;
            this.cbx_Activ_Album.CheckedChanged += new System.EventHandler(this.cbx_Activ_Album_CheckedChanged);
            // 
            // cbx_Activ_Artist
            // 
            this.cbx_Activ_Artist.AutoSize = true;
            this.cbx_Activ_Artist.Checked = true;
            this.cbx_Activ_Artist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Artist.Location = new System.Drawing.Point(505, 259);
            this.cbx_Activ_Artist.Name = "cbx_Activ_Artist";
            this.cbx_Activ_Artist.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Artist.TabIndex = 306;
            this.cbx_Activ_Artist.UseVisualStyleBackColor = true;
            this.cbx_Activ_Artist.CheckedChanged += new System.EventHandler(this.cbx_Activ_Artist_CheckedChanged);
            // 
            // cbx_Activ_Title_Sort
            // 
            this.cbx_Activ_Title_Sort.AutoSize = true;
            this.cbx_Activ_Title_Sort.Checked = true;
            this.cbx_Activ_Title_Sort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title_Sort.Location = new System.Drawing.Point(505, 237);
            this.cbx_Activ_Title_Sort.Name = "cbx_Activ_Title_Sort";
            this.cbx_Activ_Title_Sort.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Title_Sort.TabIndex = 305;
            this.cbx_Activ_Title_Sort.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title_Sort.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_Sort_CheckedChanged);
            // 
            // cbx_Activ_Title
            // 
            this.cbx_Activ_Title.AutoSize = true;
            this.cbx_Activ_Title.Checked = true;
            this.cbx_Activ_Title.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Activ_Title.Location = new System.Drawing.Point(505, 216);
            this.cbx_Activ_Title.Name = "cbx_Activ_Title";
            this.cbx_Activ_Title.Size = new System.Drawing.Size(15, 14);
            this.cbx_Activ_Title.TabIndex = 304;
            this.cbx_Activ_Title.UseVisualStyleBackColor = true;
            this.cbx_Activ_Title.CheckedChanged += new System.EventHandler(this.cbx_Activ_Title_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(5, 258);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 303;
            this.label7.Text = "Artist";
            // 
            // cbx_Artist
            // 
            this.cbx_Artist.FormattingEnabled = true;
            this.cbx_Artist.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Artist.Location = new System.Drawing.Point(398, 255);
            this.cbx_Artist.Name = "cbx_Artist";
            this.cbx_Artist.Size = new System.Drawing.Size(105, 21);
            this.cbx_Artist.TabIndex = 302;
            this.cbx_Artist.SelectedIndexChanged += new System.EventHandler(this.cbx_Artist_SelectedIndexChanged);
            // 
            // txt_Artist
            // 
            this.txt_Artist.Location = new System.Drawing.Point(54, 256);
            this.txt_Artist.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Artist.Name = "txt_Artist";
            this.txt_Artist.Size = new System.Drawing.Size(341, 20);
            this.txt_Artist.TabIndex = 301;
            this.txt_Artist.Text = "<Artist>";
            // 
            // cbx_Title
            // 
            this.cbx_Title.FormattingEnabled = true;
            this.cbx_Title.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Title.Location = new System.Drawing.Point(398, 212);
            this.cbx_Title.Name = "cbx_Title";
            this.cbx_Title.Size = new System.Drawing.Size(105, 21);
            this.cbx_Title.TabIndex = 299;
            this.cbx_Title.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_SelectedIndexChanged);
            // 
            // txt_Title
            // 
            this.txt_Title.Location = new System.Drawing.Point(54, 213);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.Size = new System.Drawing.Size(341, 20);
            this.txt_Title.TabIndex = 298;
            this.txt_Title.Text = "<Title>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(6, 326);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 297;
            this.label6.Text = "File Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 302);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 296;
            this.label5.Text = "Album";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(5, 237);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 295;
            this.label4.Text = "Title Sort";
            // 
            // cbx_File_Name
            // 
            this.cbx_File_Name.FormattingEnabled = true;
            this.cbx_File_Name.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_File_Name.Location = new System.Drawing.Point(399, 322);
            this.cbx_File_Name.Name = "cbx_File_Name";
            this.cbx_File_Name.Size = new System.Drawing.Size(104, 21);
            this.cbx_File_Name.TabIndex = 294;
            this.cbx_File_Name.SelectedIndexChanged += new System.EventHandler(this.cbx_File_Name_SelectedIndexChanged);
            // 
            // txt_File_Name
            // 
            this.txt_File_Name.Location = new System.Drawing.Point(66, 323);
            this.txt_File_Name.Margin = new System.Windows.Forms.Padding(2);
            this.txt_File_Name.Name = "txt_File_Name";
            this.txt_File_Name.Size = new System.Drawing.Size(329, 20);
            this.txt_File_Name.TabIndex = 293;
            this.txt_File_Name.Text = "<Beta><Broken><CDLC> - <Artist> - <Year> - <Album><Track No.> - <Title> - <DD> - " +
    "<Avail. Instr.> - v<Version>";
            // 
            // cbx_Album
            // 
            this.cbx_Album.FormattingEnabled = true;
            this.cbx_Album.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Album.Location = new System.Drawing.Point(398, 298);
            this.cbx_Album.Name = "cbx_Album";
            this.cbx_Album.Size = new System.Drawing.Size(105, 21);
            this.cbx_Album.TabIndex = 292;
            this.cbx_Album.SelectedIndexChanged += new System.EventHandler(this.cbx_Album_SelectedIndexChanged);
            // 
            // txt_Album
            // 
            this.txt_Album.Location = new System.Drawing.Point(54, 299);
            this.txt_Album.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Album.Name = "txt_Album";
            this.txt_Album.Size = new System.Drawing.Size(340, 20);
            this.txt_Album.TabIndex = 291;
            this.txt_Album.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            // 
            // cbx_Title_Sort
            // 
            this.cbx_Title_Sort.FormattingEnabled = true;
            this.cbx_Title_Sort.Items.AddRange(new object[] {
            "<Artist>",
            "<Title>",
            "<Version>",
            "<DLCName>",
            "<CDLC>",
            "<Album>",
            "<Track No.>",
            "<Year>",
            "<Rating>",
            "<Alt. Vers.>",
            "<Descr.>",
            "<Comm.>",
            "<Avail. Instr.>",
            "<Tuning>",
            "<Instr. Rating.>",
            "<MTrack Det.>",
            "<Group>",
            "<Beta>",
            "<DD>",
            "<Broken>",
            "<File Name>"});
            this.cbx_Title_Sort.Location = new System.Drawing.Point(398, 234);
            this.cbx_Title_Sort.Name = "cbx_Title_Sort";
            this.cbx_Title_Sort.Size = new System.Drawing.Size(105, 21);
            this.cbx_Title_Sort.TabIndex = 290;
            this.cbx_Title_Sort.SelectedIndexChanged += new System.EventHandler(this.cbx_Title_Sort_SelectedIndexChanged);
            // 
            // txt_Title_Sort
            // 
            this.txt_Title_Sort.Location = new System.Drawing.Point(54, 235);
            this.txt_Title_Sort.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Title_Sort.Name = "txt_Title_Sort";
            this.txt_Title_Sort.Size = new System.Drawing.Size(341, 20);
            this.txt_Title_Sort.TabIndex = 289;
            this.txt_Title_Sort.Text = "<Year><Album><Track No.><Title>";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(5, 215);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(27, 13);
            this.lbl_Title.TabIndex = 300;
            this.lbl_Title.Text = "Title";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(88, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 55);
            this.groupBox2.TabIndex = 282;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Repack Only";
            // 
            // chbx_CleanDLC
            // 
            this.chbx_CleanDLC.AutoSize = true;
            this.chbx_CleanDLC.Checked = true;
            this.chbx_CleanDLC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CleanDLC.Location = new System.Drawing.Point(324, 3);
            this.chbx_CleanDLC.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_CleanDLC.Name = "chbx_CleanDLC";
            this.chbx_CleanDLC.Size = new System.Drawing.Size(85, 17);
            this.chbx_CleanDLC.TabIndex = 288;
            this.chbx_CleanDLC.Text = "Clean Folder";
            this.chbx_CleanDLC.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mac
            // 
            this.rbtn_Mac.AutoSize = true;
            this.rbtn_Mac.Location = new System.Drawing.Point(112, 3);
            this.rbtn_Mac.Name = "rbtn_Mac";
            this.rbtn_Mac.Size = new System.Drawing.Size(46, 17);
            this.rbtn_Mac.TabIndex = 206;
            this.rbtn_Mac.Text = "Mac";
            this.rbtn_Mac.UseVisualStyleBackColor = true;
            // 
            // pnl_ApplyChanges
            // 
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_Xbox);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_Mac);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_PS3);
            this.pnl_ApplyChanges.Controls.Add(this.rbtn_All);
            this.pnl_ApplyChanges.Location = new System.Drawing.Point(3, 14);
            this.pnl_ApplyChanges.Name = "pnl_ApplyChanges";
            this.pnl_ApplyChanges.Size = new System.Drawing.Size(225, 25);
            this.pnl_ApplyChanges.TabIndex = 204;
            // 
            // rbtn_Xbox
            // 
            this.rbtn_Xbox.AutoSize = true;
            this.rbtn_Xbox.Location = new System.Drawing.Point(153, 3);
            this.rbtn_Xbox.Name = "rbtn_Xbox";
            this.rbtn_Xbox.Size = new System.Drawing.Size(68, 17);
            this.rbtn_Xbox.TabIndex = 207;
            this.rbtn_Xbox.Text = "XBox360";
            this.rbtn_Xbox.UseVisualStyleBackColor = true;
            // 
            // rbtn_PS3
            // 
            this.rbtn_PS3.AutoSize = true;
            this.rbtn_PS3.Checked = true;
            this.rbtn_PS3.Location = new System.Drawing.Point(69, 3);
            this.rbtn_PS3.Name = "rbtn_PS3";
            this.rbtn_PS3.Size = new System.Drawing.Size(45, 17);
            this.rbtn_PS3.TabIndex = 205;
            this.rbtn_PS3.TabStop = true;
            this.rbtn_PS3.Text = "PS3";
            this.rbtn_PS3.UseVisualStyleBackColor = true;
            // 
            // rbtn_All
            // 
            this.rbtn_All.AutoSize = true;
            this.rbtn_All.Location = new System.Drawing.Point(3, 3);
            this.rbtn_All.Name = "rbtn_All";
            this.rbtn_All.Size = new System.Drawing.Size(70, 17);
            this.rbtn_All.TabIndex = 204;
            this.rbtn_All.Text = "All Others";
            this.rbtn_All.UseVisualStyleBackColor = true;
            // 
            // chbx_DebugB
            // 
            this.chbx_DebugB.AutoSize = true;
            this.chbx_DebugB.Checked = true;
            this.chbx_DebugB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_DebugB.Location = new System.Drawing.Point(406, 68);
            this.chbx_DebugB.Name = "chbx_DebugB";
            this.chbx_DebugB.Size = new System.Drawing.Size(58, 17);
            this.chbx_DebugB.TabIndex = 277;
            this.chbx_DebugB.Text = "Debug";
            this.chbx_DebugB.UseVisualStyleBackColor = true;
            this.chbx_DebugB.CheckedChanged += new System.EventHandler(this.chbx_DebugB_CheckedChanged);
            // 
            // txt_DBFolder
            // 
            this.txt_DBFolder.Location = new System.Drawing.Point(118, 64);
            this.txt_DBFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txt_DBFolder.Name = "txt_DBFolder";
            this.txt_DBFolder.Size = new System.Drawing.Size(182, 20);
            this.txt_DBFolder.TabIndex = 275;
            // 
            // btn_DBFolder
            // 
            this.btn_DBFolder.Location = new System.Drawing.Point(300, 66);
            this.btn_DBFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DBFolder.Name = "btn_DBFolder";
            this.btn_DBFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_DBFolder.TabIndex = 273;
            this.btn_DBFolder.Text = "...";
            this.btn_DBFolder.UseVisualStyleBackColor = true;
            this.btn_DBFolder.Click += new System.EventHandler(this.btn_DBFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 274;
            this.label1.Text = "DB Folder Path";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.Location = new System.Drawing.Point(463, 64);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 20);
            this.btn_Close.TabIndex = 272;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_RocksmithDLCPath
            // 
            this.txt_RocksmithDLCPath.Location = new System.Drawing.Point(118, 1);
            this.txt_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(2);
            this.txt_RocksmithDLCPath.Name = "txt_RocksmithDLCPath";
            this.txt_RocksmithDLCPath.Size = new System.Drawing.Size(182, 20);
            this.txt_RocksmithDLCPath.TabIndex = 271;
            // 
            // txt_TempPath
            // 
            this.txt_TempPath.Location = new System.Drawing.Point(118, 25);
            this.txt_TempPath.Margin = new System.Windows.Forms.Padding(2);
            this.txt_TempPath.Name = "txt_TempPath";
            this.txt_TempPath.Size = new System.Drawing.Size(182, 20);
            this.txt_TempPath.TabIndex = 270;
            // 
            // chbx_CleanTemp
            // 
            this.chbx_CleanTemp.AutoSize = true;
            this.chbx_CleanTemp.Checked = true;
            this.chbx_CleanTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CleanTemp.Location = new System.Drawing.Point(324, 27);
            this.chbx_CleanTemp.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_CleanTemp.Name = "chbx_CleanTemp";
            this.chbx_CleanTemp.Size = new System.Drawing.Size(85, 17);
            this.chbx_CleanTemp.TabIndex = 269;
            this.chbx_CleanTemp.Text = "Clean Folder";
            this.chbx_CleanTemp.UseVisualStyleBackColor = true;
            // 
            // btn_SteamDLCFolder
            // 
            this.btn_SteamDLCFolder.Location = new System.Drawing.Point(300, 3);
            this.btn_SteamDLCFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            this.btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 15);
            this.btn_SteamDLCFolder.TabIndex = 268;
            this.btn_SteamDLCFolder.Text = "...";
            this.btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            this.btn_SteamDLCFolder.Click += new System.EventHandler(this.btn_SteamDLCFolder_Click);
            // 
            // chbx_CleanDB
            // 
            this.chbx_CleanDB.AutoSize = true;
            this.chbx_CleanDB.Checked = true;
            this.chbx_CleanDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_CleanDB.Location = new System.Drawing.Point(324, 66);
            this.chbx_CleanDB.Margin = new System.Windows.Forms.Padding(2);
            this.chbx_CleanDB.Name = "chbx_CleanDB";
            this.chbx_CleanDB.Size = new System.Drawing.Size(71, 17);
            this.chbx_CleanDB.TabIndex = 267;
            this.chbx_CleanDB.Text = "Clean DB";
            this.chbx_CleanDB.UseVisualStyleBackColor = true;
            // 
            // lbl_StatisticsOnReadDLCs
            // 
            this.lbl_StatisticsOnReadDLCs.AutoSize = true;
            this.lbl_StatisticsOnReadDLCs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_StatisticsOnReadDLCs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_StatisticsOnReadDLCs.Location = new System.Drawing.Point(5, 500);
            this.lbl_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_StatisticsOnReadDLCs.Name = "lbl_StatisticsOnReadDLCs";
            this.lbl_StatisticsOnReadDLCs.Size = new System.Drawing.Size(86, 13);
            this.lbl_StatisticsOnReadDLCs.TabIndex = 265;
            this.lbl_StatisticsOnReadDLCs.Text = "Progress Details:";
            // 
            // rtxt_StatisticsOnReadDLCs
            // 
            this.rtxt_StatisticsOnReadDLCs.Location = new System.Drawing.Point(8, 404);
            this.rtxt_StatisticsOnReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.rtxt_StatisticsOnReadDLCs.Name = "rtxt_StatisticsOnReadDLCs";
            this.rtxt_StatisticsOnReadDLCs.Size = new System.Drawing.Size(528, 94);
            this.rtxt_StatisticsOnReadDLCs.TabIndex = 264;
            this.rtxt_StatisticsOnReadDLCs.Text = "";
            // 
            // pB_ReadDLCs
            // 
            this.pB_ReadDLCs.Location = new System.Drawing.Point(8, 380);
            this.pB_ReadDLCs.Margin = new System.Windows.Forms.Padding(2);
            this.pB_ReadDLCs.Maximum = 10000;
            this.pB_ReadDLCs.Name = "pB_ReadDLCs";
            this.pB_ReadDLCs.Size = new System.Drawing.Size(527, 20);
            this.pB_ReadDLCs.Step = 1;
            this.pB_ReadDLCs.TabIndex = 263;
            // 
            // btn_PopulateDB
            // 
            this.btn_PopulateDB.BackColor = System.Drawing.SystemColors.Control;
            this.btn_PopulateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PopulateDB.Location = new System.Drawing.Point(463, 37);
            this.btn_PopulateDB.Margin = new System.Windows.Forms.Padding(2);
            this.btn_PopulateDB.Name = "btn_PopulateDB";
            this.btn_PopulateDB.Size = new System.Drawing.Size(73, 25);
            this.btn_PopulateDB.TabIndex = 262;
            this.btn_PopulateDB.Text = "Import";
            this.btn_PopulateDB.UseVisualStyleBackColor = false;
            this.btn_PopulateDB.Click += new System.EventHandler(this.btn_PopulateDB_Click);
            // 
            // btn_DecompressAll
            // 
            this.btn_DecompressAll.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_DecompressAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DecompressAll.Location = new System.Drawing.Point(463, 1);
            this.btn_DecompressAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DecompressAll.Name = "btn_DecompressAll";
            this.btn_DecompressAll.Size = new System.Drawing.Size(73, 35);
            this.btn_DecompressAll.TabIndex = 261;
            this.btn_DecompressAll.Text = "Open Main DB";
            this.btn_DecompressAll.UseVisualStyleBackColor = false;
            this.btn_DecompressAll.Click += new System.EventHandler(this.btn_DecompressAll_Click);
            // 
            // lbl_RocksmithDLCPath
            // 
            this.lbl_RocksmithDLCPath.AutoSize = true;
            this.lbl_RocksmithDLCPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_RocksmithDLCPath.Location = new System.Drawing.Point(5, 4);
            this.lbl_RocksmithDLCPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_RocksmithDLCPath.Name = "lbl_RocksmithDLCPath";
            this.lbl_RocksmithDLCPath.Size = new System.Drawing.Size(101, 13);
            this.lbl_RocksmithDLCPath.TabIndex = 259;
            this.lbl_RocksmithDLCPath.Text = "Importing DLC path:";
            // 
            // btn_TempPath
            // 
            this.btn_TempPath.Location = new System.Drawing.Point(300, 27);
            this.btn_TempPath.Margin = new System.Windows.Forms.Padding(2);
            this.btn_TempPath.Name = "btn_TempPath";
            this.btn_TempPath.Size = new System.Drawing.Size(22, 15);
            this.btn_TempPath.TabIndex = 257;
            this.btn_TempPath.Text = "...";
            this.btn_TempPath.UseVisualStyleBackColor = true;
            this.btn_TempPath.Click += new System.EventHandler(this.btn_TempPath_Click);
            // 
            // lbl_TempFolders
            // 
            this.lbl_TempFolders.AutoSize = true;
            this.lbl_TempFolders.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TempFolders.Location = new System.Drawing.Point(4, 28);
            this.lbl_TempFolders.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TempFolders.Name = "lbl_TempFolders";
            this.lbl_TempFolders.Size = new System.Drawing.Size(94, 13);
            this.lbl_TempFolders.TabIndex = 258;
            this.lbl_TempFolders.Text = "Temp Folder Path:";
            // 
            // lbl_PreviewText
            // 
            this.lbl_PreviewText.AutoEllipsis = true;
            this.lbl_PreviewText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbl_PreviewText.Location = new System.Drawing.Point(85, 345);
            this.lbl_PreviewText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PreviewText.Name = "lbl_PreviewText";
            this.lbl_PreviewText.Size = new System.Drawing.Size(448, 35);
            this.lbl_PreviewText.TabIndex = 285;
            this.lbl_PreviewText.Text = "File name CDLC/ORIG-Artist-Year-Album-TrackNo(if existing)-Title-TrackAvail(LRBVS" +
    ")-Version";
            // 
            // grpbx_ApplyChanges
            // 
            this.grpbx_ApplyChanges.Controls.Add(this.pnl_ApplyChanges);
            this.grpbx_ApplyChanges.Enabled = false;
            this.grpbx_ApplyChanges.Location = new System.Drawing.Point(8, 84);
            this.grpbx_ApplyChanges.Name = "grpbx_ApplyChanges";
            this.grpbx_ApplyChanges.Size = new System.Drawing.Size(243, 42);
            this.grpbx_ApplyChanges.TabIndex = 278;
            this.grpbx_ApplyChanges.TabStop = false;
            this.grpbx_ApplyChanges.Text = "RePack format";
            // 
            // btn_RePack
            // 
            this.btn_RePack.BackColor = System.Drawing.SystemColors.Control;
            this.btn_RePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RePack.Location = new System.Drawing.Point(8, 131);
            this.btn_RePack.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RePack.Name = "btn_RePack";
            this.btn_RePack.Size = new System.Drawing.Size(77, 78);
            this.btn_RePack.TabIndex = 283;
            this.btn_RePack.Text = "RePack";
            this.btn_RePack.UseVisualStyleBackColor = false;
            this.btn_RePack.Click += new System.EventHandler(this.btn_RePack_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 13);
            this.label2.TabIndex = 279;
            this.label2.Text = "Will be used store decompressed version of all DLCs and the original version";
            // 
            // DLCManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbx_Export);
            this.Controls.Add(this.Export_To);
            this.Controls.Add(this.chbx_Rebuild);
            this.Controls.Add(this.btn_Cleanup_MainDB);
            this.Controls.Add(this.cbx_Groups);
            this.Controls.Add(this.chbx_Additional_Manipualtions);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn_Preview_Artist_Sort);
            this.Controls.Add(this.cbx_Activ_Artist_Sort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_Artist_Sort);
            this.Controls.Add(this.txt_Artist_Sort);
            this.Controls.Add(this.btn_Preview_File_Name);
            this.Controls.Add(this.btn_Preview_Album);
            this.Controls.Add(this.btn_Preview_Artist);
            this.Controls.Add(this.btn_Preview_Title_Sort);
            this.Controls.Add(this.btn_Preview_Title);
            this.Controls.Add(this.cbx_Activ_File_Name);
            this.Controls.Add(this.cbx_Activ_Album);
            this.Controls.Add(this.cbx_Activ_Artist);
            this.Controls.Add(this.cbx_Activ_Title_Sort);
            this.Controls.Add(this.cbx_Activ_Title);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbx_Artist);
            this.Controls.Add(this.txt_Artist);
            this.Controls.Add(this.cbx_Title);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbx_File_Name);
            this.Controls.Add(this.txt_File_Name);
            this.Controls.Add(this.cbx_Album);
            this.Controls.Add(this.txt_Album);
            this.Controls.Add(this.cbx_Title_Sort);
            this.Controls.Add(this.txt_Title_Sort);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chbx_CleanDLC);
            this.Controls.Add(this.chbx_DebugB);
            this.Controls.Add(this.txt_DBFolder);
            this.Controls.Add(this.btn_DBFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_RocksmithDLCPath);
            this.Controls.Add(this.txt_TempPath);
            this.Controls.Add(this.chbx_CleanTemp);
            this.Controls.Add(this.btn_SteamDLCFolder);
            this.Controls.Add(this.chbx_CleanDB);
            this.Controls.Add(this.lbl_StatisticsOnReadDLCs);
            this.Controls.Add(this.rtxt_StatisticsOnReadDLCs);
            this.Controls.Add(this.pB_ReadDLCs);
            this.Controls.Add(this.btn_PopulateDB);
            this.Controls.Add(this.btn_DecompressAll);
            this.Controls.Add(this.lbl_RocksmithDLCPath);
            this.Controls.Add(this.btn_TempPath);
            this.Controls.Add(this.lbl_TempFolders);
            this.Controls.Add(this.lbl_PreviewText);
            this.Controls.Add(this.grpbx_ApplyChanges);
            this.Controls.Add(this.btn_RePack);
            this.Controls.Add(this.label2);
            this.Name = "DLCManager";
            this.Size = new System.Drawing.Size(613, 794);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.pnl_ApplyChanges.ResumeLayout(false);
            this.pnl_ApplyChanges.PerformLayout();
            this.grpbx_ApplyChanges.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbx_Export;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtn_Population_Groups;
        private System.Windows.Forms.RadioButton rbtn_Population_All;
        private System.Windows.Forms.RadioButton rbtn_Population_Selected;
        private System.Windows.Forms.BindingSource mainBindingSource;
        private System.Windows.Forms.Button Export_To;
        private System.Windows.Forms.CheckBox chbx_Rebuild;
        private System.Windows.Forms.Button btn_Cleanup_MainDB;
        private System.Windows.Forms.ComboBox cbx_Groups;
        private System.Windows.Forms.CheckedListBox chbx_Additional_Manipualtions;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Preview_Artist_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist_Sort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbx_Artist_Sort;
        private System.Windows.Forms.TextBox txt_Artist_Sort;
        private System.Windows.Forms.Button btn_Preview_File_Name;
        private System.Windows.Forms.Button btn_Preview_Album;
        private System.Windows.Forms.Button btn_Preview_Artist;
        private System.Windows.Forms.Button btn_Preview_Title_Sort;
        private System.Windows.Forms.Button btn_Preview_Title;
        private System.Windows.Forms.CheckBox cbx_Activ_File_Name;
        private System.Windows.Forms.CheckBox cbx_Activ_Album;
        private System.Windows.Forms.CheckBox cbx_Activ_Artist;
        private System.Windows.Forms.CheckBox cbx_Activ_Title_Sort;
        private System.Windows.Forms.CheckBox cbx_Activ_Title;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbx_Artist;
        private System.Windows.Forms.TextBox txt_Artist;
        private System.Windows.Forms.ComboBox cbx_Title;
        private System.Windows.Forms.TextBox txt_Title;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_File_Name;
        private System.Windows.Forms.TextBox txt_File_Name;
        private System.Windows.Forms.ComboBox cbx_Album;
        private System.Windows.Forms.TextBox txt_Album;
        private System.Windows.Forms.ComboBox cbx_Title_Sort;
        private System.Windows.Forms.TextBox txt_Title_Sort;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbx_CleanDLC;
        private System.Windows.Forms.RadioButton rbtn_Mac;
        private System.Windows.Forms.Panel pnl_ApplyChanges;
        private System.Windows.Forms.RadioButton rbtn_Xbox;
        private System.Windows.Forms.RadioButton rbtn_PS3;
        private System.Windows.Forms.RadioButton rbtn_All;
        private System.Windows.Forms.CheckBox chbx_DebugB;
        private System.Windows.Forms.TextBox txt_DBFolder;
        private System.Windows.Forms.Button btn_DBFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txt_RocksmithDLCPath;
        private System.Windows.Forms.TextBox txt_TempPath;
        private System.Windows.Forms.CheckBox chbx_CleanTemp;
        private System.Windows.Forms.Button btn_SteamDLCFolder;
        private System.Windows.Forms.CheckBox chbx_CleanDB;
        private System.Windows.Forms.Label lbl_StatisticsOnReadDLCs;
        private System.Windows.Forms.RichTextBox rtxt_StatisticsOnReadDLCs;
        private System.Windows.Forms.ProgressBar pB_ReadDLCs;
        private System.Windows.Forms.Button btn_PopulateDB;
        private System.Windows.Forms.Button btn_DecompressAll;
        private System.Windows.Forms.Label lbl_RocksmithDLCPath;
        private System.Windows.Forms.Button btn_TempPath;
        private System.Windows.Forms.Label lbl_TempFolders;
        private System.Windows.Forms.Label lbl_PreviewText;
        private System.Windows.Forms.GroupBox grpbx_ApplyChanges;
        private System.Windows.Forms.Button btn_RePack;
        private System.Windows.Forms.Label label2;
    }
}