using System.Windows.Forms;

namespace RocksmithToolkitGUI.DLCManager
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
            components = new System.ComponentModel.Container();
            cbx_Export = new ComboBox();
            panel2 = new Panel();
            btn_Debug = new Button();
            chbx_FilterCompound = new CheckBox();
            rbtn_Population_PackNO = new CheckBox();
            txt_NoOfSplits = new NumericUpDown();
            label2 = new Label();
            cbx_Groups = new ComboBox();
            lbl_Access = new Label();
            chbx_iOS = new CheckBox();
            chbx_PS4 = new CheckBox();
            btn_GoRepack = new Button();
            lbl_NoRec2 = new Label();
            chbx_XBOX360 = new CheckBox();
            btn_Add2Retail = new Button();
            chbx_Mac = new CheckBox();
            chbx_PS3 = new CheckBox();
            chbx_PC = new CheckBox();
            btn_Cleanup_MainDB = new Button();
            btn_RePack = new Button();
            chbx_Rebuild = new CheckBox();
            mainBindingSource = new BindingSource(components);
            chbx_DefaultDB = new CheckBox();
            Export_To = new Button();
            chbx_Additional_Manipulations = new CheckedListBox();
            lbl_Mask = new Label();
            btn_Preview_Artist_Sort = new Button();
            cbx_Activ_Artist_Sort = new CheckBox();
            lbl_Artist_Sort = new Label();
            cbx_Artist_Sort = new ComboBox();
            txt_Artist_Sort = new TextBox();
            pB_ReadDLCs = new ProgressBar();
            btn_Preview_File_Name = new Button();
            btn_Preview_Album = new Button();
            btn_Preview_Artist = new Button();
            btn_Preview_Title_Sort = new Button();
            btn_Preview_Title = new Button();
            cbx_Activ_File_Name = new CheckBox();
            cbx_Activ_Album = new CheckBox();
            cbx_Activ_Artist = new CheckBox();
            cbx_Activ_Title_Sort = new CheckBox();
            cbx_Activ_Title = new CheckBox();
            lbl_Artist = new Label();
            cbx_Artist = new ComboBox();
            txt_Artist = new TextBox();
            cbx_Title = new ComboBox();
            txt_Title = new TextBox();
            lbl_File_Name = new Label();
            lbl_Album = new Label();
            lbl_Title_Sort = new Label();
            cbx_File_Name = new ComboBox();
            txt_File_Name = new TextBox();
            cbx_Album = new ComboBox();
            txt_Album = new TextBox();
            cbx_Title_Sort = new ComboBox();
            txt_Title_Sort = new TextBox();
            lbl_Title = new Label();
            chbx_DebugB = new CheckBox();
            txt_DBFolder = new TextBox();
            btn_DBFolder = new Button();
            label1 = new Label();
            btn_Close = new Button();
            txt_RocksmithDLCPath = new TextBox();
            txt_TempPath = new TextBox();
            chbx_CleanTemp = new CheckBox();
            btn_SteamDLCFolder = new Button();
            rtxt_StatisticsOnReadDLCs = new RichTextBox();
            btn_PopulateDB = new Button();
            btn_OpenMainDB = new Button();
            lbl_RocksmithDLCPath = new Label();
            btn_TempPath = new Button();
            lbl_TempFolders = new Label();
            lbl_PreviewText = new Label();
            helpProvider1 = new HelpProvider();
            toolTip1 = new ToolTip(components);
            btn_ApplyStandardization = new Button();
            btn_LoadRetailSongs = new Button();
            btn_Standardization = new Button();
            btn_GoImport = new Button();
            btn_GoTemp = new Button();
            btn_GoDB = new Button();
            btn_OpenDB = new Button();
            btn_ProfilesSave = new Button();
            btn_ProfileRemove = new Button();
            btn_OpenLogsFolder = new Button();
            btn_Enable_CDLC = new Button();
            btn_CalcNoOfImports = new Button();
            btn_CopyDefaultDBtoTemp = new Button();
            btn_Param = new Button();
            btn_Save = new Button();
            btn_Preview_Lyric_Info = new Button();
            btn_Preview_Album_Sort = new Button();
            btn_Album2SortA = new Button();
            btn_FilterParams = new Button();
            btn_Retail = new Button();
            button1 = new Button();
            btn_ApplyMultiFilters = new Button();
            btn_DistribNotes = new Button();
            chbx_Configurations = new ComboBox();
            lbl_Settings = new Label();
            cbx_Activ_Lyric_Info = new CheckBox();
            lbl_LyricInfo = new Label();
            cbx_Lyric_Info = new ComboBox();
            txt_Lyric_Info = new TextBox();
            cbx_Activ_Album_Sort = new CheckBox();
            lbl_AlbumSort = new Label();
            cbx_Album_Sort = new ComboBox();
            txt_Album_Sort = new TextBox();
            txt_FilterParams = new RichTextBox();
            lbl_Log = new Label();
            lbGroups = new ListBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txt_NoOfSplits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mainBindingSource).BeginInit();
            SuspendLayout();
            // 
            // cbx_Export
            // 
            cbx_Export.FormattingEnabled = true;
            cbx_Export.Items.AddRange(new object[] { "Excel", "WebPage", "for a CustomForge upload" });
            cbx_Export.Location = new System.Drawing.Point(128, 186);
            cbx_Export.Margin = new Padding(2);
            cbx_Export.Name = "cbx_Export";
            cbx_Export.Size = new System.Drawing.Size(76, 23);
            cbx_Export.TabIndex = 57;
            cbx_Export.Text = "WebPage";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_Debug);
            panel2.Controls.Add(chbx_FilterCompound);
            panel2.Controls.Add(rbtn_Population_PackNO);
            panel2.Controls.Add(txt_NoOfSplits);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cbx_Groups);
            panel2.Controls.Add(lbl_Access);
            panel2.Controls.Add(chbx_iOS);
            panel2.Controls.Add(chbx_PS4);
            panel2.Controls.Add(btn_GoRepack);
            panel2.Controls.Add(lbl_NoRec2);
            panel2.Controls.Add(chbx_XBOX360);
            panel2.Controls.Add(btn_Add2Retail);
            panel2.Controls.Add(chbx_Mac);
            panel2.Controls.Add(chbx_PS3);
            panel2.Controls.Add(chbx_PC);
            panel2.Controls.Add(btn_Cleanup_MainDB);
            panel2.Controls.Add(btn_RePack);
            panel2.Controls.Add(chbx_Rebuild);
            panel2.Location = new System.Drawing.Point(6, 64);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(382, 82);
            panel2.TabIndex = 204;
            // 
            // btn_Debug
            // 
            btn_Debug.BackColor = System.Drawing.SystemColors.Control;
            btn_Debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            btn_Debug.Location = new System.Drawing.Point(336, 60);
            btn_Debug.Margin = new Padding(0);
            btn_Debug.Name = "btn_Debug";
            btn_Debug.Size = new System.Drawing.Size(43, 20);
            btn_Debug.TabIndex = 434;
            btn_Debug.Text = "Debug";
            toolTip1.SetToolTip(btn_Debug, "Opens the Metadata DB using a 3rd party tool (not M$ Access)");
            btn_Debug.UseVisualStyleBackColor = false;
            btn_Debug.Visible = false;
            btn_Debug.Click += btn_Debug_Click;
            // 
            // chbx_FilterCompound
            // 
            chbx_FilterCompound.Font = new System.Drawing.Font("Calibri", 9F);
            chbx_FilterCompound.Location = new System.Drawing.Point(216, 42);
            chbx_FilterCompound.Margin = new Padding(2);
            chbx_FilterCompound.Name = "chbx_FilterCompound";
            chbx_FilterCompound.Size = new System.Drawing.Size(113, 18);
            chbx_FilterCompound.TabIndex = 437;
            chbx_FilterCompound.Text = "Compound Filter";
            toolTip1.SetToolTip(chbx_FilterCompound, "Keeps the previously selected filter and adds the one just selected");
            chbx_FilterCompound.UseVisualStyleBackColor = true;
            // 
            // rbtn_Population_PackNO
            // 
            rbtn_Population_PackNO.Location = new System.Drawing.Point(216, 24);
            rbtn_Population_PackNO.Margin = new Padding(2);
            rbtn_Population_PackNO.Name = "rbtn_Population_PackNO";
            rbtn_Population_PackNO.Size = new System.Drawing.Size(60, 18);
            rbtn_Population_PackNO.TabIndex = 436;
            rbtn_Population_PackNO.Text = "Batch";
            toolTip1.SetToolTip(rbtn_Population_PackNO, "Activating Batch mode packing (used when DLCManager has been multiplied for parallel Packing; in Batches)");
            rbtn_Population_PackNO.UseVisualStyleBackColor = true;
            // 
            // txt_NoOfSplits
            // 
            txt_NoOfSplits.Location = new System.Drawing.Point(278, 23);
            txt_NoOfSplits.Margin = new Padding(2);
            txt_NoOfSplits.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            txt_NoOfSplits.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txt_NoOfSplits.Name = "txt_NoOfSplits";
            txt_NoOfSplits.Size = new System.Drawing.Size(32, 23);
            txt_NoOfSplits.TabIndex = 434;
            toolTip1.SetToolTip(txt_NoOfSplits, "Batch No (used when DLCManager has been multiplied for parallel Packing; in Batches)");
            txt_NoOfSplits.Value = new decimal(new int[] { 5, 0, 0, 0 });
            txt_NoOfSplits.ValueChanged += txt_NoOfSplits_ValueChanged;
            // 
            // label2
            // 
            label2.ForeColor = System.Drawing.SystemColors.ControlText;
            label2.Location = new System.Drawing.Point(2, 24);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 16);
            label2.TabIndex = 433;
            label2.Text = "Selection";
            // 
            // cbx_Groups
            // 
            cbx_Groups.DropDownHeight = 600;
            cbx_Groups.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_Groups.DropDownWidth = 600;
            cbx_Groups.FlatStyle = FlatStyle.Popup;
            cbx_Groups.FormattingEnabled = true;
            cbx_Groups.IntegralHeight = false;
            cbx_Groups.ItemHeight = 15;
            cbx_Groups.Items.AddRange(new object[] { "0ALL", "ALL Others", "Songs in Rocksmith Game Lib", "Show Songs with FilesMissing Issues", "Reverse current Filter", "Imported Last", "No Cover", "No Guitar", "No Preview", "No Section", "No Vocals", "No Track No.", "No Version", "No Author", "No Bass DD", "No Bass", "No DD", "With DD", "In the works", "Improved wIth DLC Manager", "Same (imported/old) File Name", "Same hash File Name", "Different Artist/Album/Title vs Sort counterparts", "Alternate", "Duplicated", "Beta", "Broken", "Selected", "With Bonus", "Original", "CDLC", "Drop D", "E Standard", "Eb Standard", "Other Tunings", "Live", "Acoustic", "Instrumental", "Single", "Soundtrack", "EP", "Uncensored", "Remastered", "Full Album", "Demo", "Cover", "Remix", "Karaoke", "Featuring", "Imported as Pc", "Imported as PS3", "Imported as Mac", "Imported as XBOX360", "Packed (curr. Platform)", "Sorted by Last Packdate", "with Errors at Packing", "DLCID diff than Default", "Same DLCName", "Same Title&Artist", "Same Title(no[])&Artist", "Same Artist&Album different Year", "Same Artist&Title(no[]) different Year", "Same Artist&Title(no[]) different Album", "Same Artist&Title(no[]) & SongLenght", "Automatically generated Preview", "Any DLCManager generated Preview", "With Duplicates", "Main_NoOLD", "Imported Current Month", "Packed Last", "Packing Errors", "Songs IMPORTED later than current song value", "Songs ADDED later to the Groups value/group than the import date of the current song value", "Sorted by Groups value/Group added date", "Lyrics Changed", "Audio Changed", "Packed as Pc", "Packed as PS3", "<Packed as PS4>", "Packed as Mac", "Packed XBOX360", "with Errors at Last Packing", "Part of No Group", "Part of No Group (Excl. Default)", "Part of Any Group", "Part of Any Group (Excl. Default)", "Part of any Group besides the selected below and Default", "Part of the selected below and Others ignoring Default", "Digitech Drop compatible Guitar (Straight down conv from E Standard or Drop D)", "Digitech Drop compatible Guitar (Straight down conv from E Standard)", "Digitech Drop compatible Bass (Straight down conv from E Standard)", "Digitech Drop compatible (Straight down conv from E Standard)", "Digitech Drop compatible (Straight down conv from Drop D)", "Digitech Drop compatible Guitar (Straight down conv from D Standard)", "Digitech Drop compatible Bass (Straight down conv from D Standard)" });
            cbx_Groups.Location = new System.Drawing.Point(58, 22);
            cbx_Groups.Margin = new Padding(2);
            cbx_Groups.MaxDropDownItems = 90;
            cbx_Groups.Name = "cbx_Groups";
            cbx_Groups.Size = new System.Drawing.Size(158, 23);
            cbx_Groups.TabIndex = 433;
            toolTip1.SetToolTip(cbx_Groups, "Different Listing filters");
            cbx_Groups.DropDown += cbx_Groups_DropDown_1;
            cbx_Groups.SelectedIndexChanged += cbx_Groups_SelectedIndexChanged;
            // 
            // lbl_Access
            // 
            lbl_Access.AutoSize = true;
            lbl_Access.ForeColor = System.Drawing.Color.Red;
            lbl_Access.Location = new System.Drawing.Point(216, 62);
            lbl_Access.Margin = new Padding(2, 0, 2, 0);
            lbl_Access.Name = "lbl_Access";
            lbl_Access.Size = new System.Drawing.Size(113, 15);
            lbl_Access.TabIndex = 432;
            lbl_Access.Text = "Rksmith Folder Path";
            lbl_Access.Visible = false;
            // 
            // chbx_iOS
            // 
            chbx_iOS.Enabled = false;
            chbx_iOS.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_iOS.Location = new System.Drawing.Point(248, 4);
            chbx_iOS.Margin = new Padding(2);
            chbx_iOS.Name = "chbx_iOS";
            chbx_iOS.Size = new System.Drawing.Size(42, 18);
            chbx_iOS.TabIndex = 412;
            chbx_iOS.Text = "iOS";
            chbx_iOS.UseVisualStyleBackColor = true;
            // 
            // chbx_PS4
            // 
            chbx_PS4.Enabled = false;
            chbx_PS4.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_PS4.Location = new System.Drawing.Point(202, 4);
            chbx_PS4.Margin = new Padding(2);
            chbx_PS4.Name = "chbx_PS4";
            chbx_PS4.Size = new System.Drawing.Size(50, 18);
            chbx_PS4.TabIndex = 411;
            chbx_PS4.Text = "PS4";
            chbx_PS4.UseVisualStyleBackColor = true;
            // 
            // btn_GoRepack
            // 
            btn_GoRepack.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            helpProvider1.SetHelpKeyword(btn_GoRepack, "Open the folder contaning Repack-ed CDLCs.");
            btn_GoRepack.Location = new System.Drawing.Point(294, 4);
            btn_GoRepack.Margin = new Padding(2);
            btn_GoRepack.Name = "btn_GoRepack";
            helpProvider1.SetShowHelp(btn_GoRepack, true);
            btn_GoRepack.Size = new System.Drawing.Size(22, 16);
            btn_GoRepack.TabIndex = 390;
            btn_GoRepack.Text = "->";
            btn_GoRepack.UseVisualStyleBackColor = true;
            btn_GoRepack.Click += btn_GoRepack_Click;
            // 
            // lbl_NoRec2
            // 
            lbl_NoRec2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            lbl_NoRec2.Location = new System.Drawing.Point(310, 24);
            lbl_NoRec2.Margin = new Padding(2, 0, 2, 0);
            lbl_NoRec2.Name = "lbl_NoRec2";
            lbl_NoRec2.Size = new System.Drawing.Size(69, 20);
            lbl_NoRec2.TabIndex = 325;
            lbl_NoRec2.Text = " Records";
            toolTip1.SetToolTip(lbl_NoRec2, "Group/Selected//All");
            // 
            // chbx_XBOX360
            // 
            chbx_XBOX360.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_XBOX360.Location = new System.Drawing.Point(134, 4);
            chbx_XBOX360.Margin = new Padding(2);
            chbx_XBOX360.Name = "chbx_XBOX360";
            chbx_XBOX360.Size = new System.Drawing.Size(72, 18);
            chbx_XBOX360.TabIndex = 23;
            chbx_XBOX360.Text = "XBOX360";
            chbx_XBOX360.UseVisualStyleBackColor = true;
            // 
            // btn_Add2Retail
            // 
            btn_Add2Retail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            btn_Add2Retail.Location = new System.Drawing.Point(120, 44);
            btn_Add2Retail.Margin = new Padding(0);
            btn_Add2Retail.Name = "btn_Add2Retail";
            btn_Add2Retail.Size = new System.Drawing.Size(94, 36);
            btn_Add2Retail.TabIndex = 404;
            btn_Add2Retail.Text = "Insert directly into gamefiles";
            btn_Add2Retail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            btn_Add2Retail.UseVisualStyleBackColor = true;
            btn_Add2Retail.Click += btn_Add2Retail_Click;
            // 
            // chbx_Mac
            // 
            chbx_Mac.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_Mac.Location = new System.Drawing.Point(88, 4);
            chbx_Mac.Margin = new Padding(2);
            chbx_Mac.Name = "chbx_Mac";
            chbx_Mac.Size = new System.Drawing.Size(46, 18);
            chbx_Mac.TabIndex = 22;
            chbx_Mac.Text = "Mac";
            chbx_Mac.UseVisualStyleBackColor = true;
            // 
            // chbx_PS3
            // 
            chbx_PS3.Checked = true;
            chbx_PS3.CheckState = CheckState.Checked;
            chbx_PS3.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_PS3.Location = new System.Drawing.Point(44, 4);
            chbx_PS3.Margin = new Padding(2);
            chbx_PS3.Name = "chbx_PS3";
            chbx_PS3.Size = new System.Drawing.Size(52, 18);
            chbx_PS3.TabIndex = 21;
            chbx_PS3.Text = "PS3";
            chbx_PS3.UseVisualStyleBackColor = true;
            // 
            // chbx_PC
            // 
            chbx_PC.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_PC.Location = new System.Drawing.Point(4, 4);
            chbx_PC.Margin = new Padding(2);
            chbx_PC.Name = "chbx_PC";
            chbx_PC.Size = new System.Drawing.Size(49, 18);
            chbx_PC.TabIndex = 20;
            chbx_PC.Text = "PC";
            chbx_PC.UseVisualStyleBackColor = true;
            // 
            // btn_Cleanup_MainDB
            // 
            btn_Cleanup_MainDB.BackColor = System.Drawing.SystemColors.Control;
            btn_Cleanup_MainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Cleanup_MainDB.Location = new System.Drawing.Point(2, 44);
            btn_Cleanup_MainDB.Margin = new Padding(2);
            btn_Cleanup_MainDB.Name = "btn_Cleanup_MainDB";
            btn_Cleanup_MainDB.Size = new System.Drawing.Size(58, 36);
            btn_Cleanup_MainDB.TabIndex = 15;
            btn_Cleanup_MainDB.Text = "Remove";
            toolTip1.SetToolTip(btn_Cleanup_MainDB, "Removed CDCL Imported from the DB and disk.");
            btn_Cleanup_MainDB.UseVisualStyleBackColor = false;
            btn_Cleanup_MainDB.Click += btn_Cleanup_MainDB_Click;
            // 
            // btn_RePack
            // 
            btn_RePack.BackColor = System.Drawing.SystemColors.Control;
            btn_RePack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_RePack.Location = new System.Drawing.Point(64, 44);
            btn_RePack.Margin = new Padding(2);
            btn_RePack.Name = "btn_RePack";
            btn_RePack.Size = new System.Drawing.Size(54, 36);
            btn_RePack.TabIndex = 14;
            btn_RePack.Text = "RePack";
            toolTip1.SetToolTip(btn_RePack, "Mass Pack CDCLs. (Fixes audio, Gets spotify info and saves it, Marks as Brken if it fails. All based on Optionns selected)");
            btn_RePack.UseVisualStyleBackColor = false;
            btn_RePack.Click += Btn_RePack_Click;
            // 
            // chbx_Rebuild
            // 
            chbx_Rebuild.AutoSize = true;
            chbx_Rebuild.Enabled = false;
            chbx_Rebuild.Font = new System.Drawing.Font("Calibri", 8F);
            chbx_Rebuild.Location = new System.Drawing.Point(319, 5);
            chbx_Rebuild.Margin = new Padding(2);
            chbx_Rebuild.Name = "chbx_Rebuild";
            chbx_Rebuild.Size = new System.Drawing.Size(62, 17);
            chbx_Rebuild.TabIndex = 324;
            chbx_Rebuild.Text = "Rebuild";
            chbx_Rebuild.UseVisualStyleBackColor = true;
            chbx_Rebuild.Visible = false;
            chbx_Rebuild.CheckedChanged += chbx_Rebuild_CheckedChanged;
            // 
            // mainBindingSource
            // 
            mainBindingSource.DataMember = "Main";
            // 
            // chbx_DefaultDB
            // 
            chbx_DefaultDB.AutoSize = true;
            chbx_DefaultDB.Location = new System.Drawing.Point(322, 45);
            chbx_DefaultDB.Margin = new Padding(2);
            chbx_DefaultDB.Name = "chbx_DefaultDB";
            chbx_DefaultDB.Size = new System.Drawing.Size(101, 19);
            chbx_DefaultDB.TabIndex = 19;
            chbx_DefaultDB.Text = "Use DefaultDB";
            toolTip1.SetToolTip(chbx_DefaultDB, "Uses the Default DB stored in the App folder");
            chbx_DefaultDB.UseVisualStyleBackColor = true;
            chbx_DefaultDB.CheckedChanged += chbx_DefaultDB_CheckedChanged;
            // 
            // Export_To
            // 
            Export_To.BackColor = System.Drawing.SystemColors.Control;
            Export_To.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            Export_To.Location = new System.Drawing.Point(70, 182);
            Export_To.Margin = new Padding(2);
            Export_To.Name = "Export_To";
            Export_To.Size = new System.Drawing.Size(56, 26);
            Export_To.TabIndex = 56;
            Export_To.Text = "Export as";
            toolTip1.SetToolTip(Export_To, "Export Access Db and connected files as ...");
            Export_To.UseVisualStyleBackColor = false;
            Export_To.Click += Export_To_Click;
            // 
            // chbx_Additional_Manipulations
            // 
            chbx_Additional_Manipulations.CheckOnClick = true;
            chbx_Additional_Manipulations.Font = new System.Drawing.Font("Segoe UI", 7F);
            chbx_Additional_Manipulations.FormattingEnabled = true;
            chbx_Additional_Manipulations.HorizontalScrollbar = true;
            chbx_Additional_Manipulations.Items.AddRange(new object[] { "00. @Pack Add Increment to all songs Title", "01. @Pack Add Increment to all songs Title per artist", "02. @Pack Make all DLC IDs unique (&save)", "03. @Pack Remove DD", "04. Backup DB during Startup", "05. @Pack Remove DD only for Bass Guitar", "06. When converting Audio use local folder structure", "07. @Pack skip Broken songs", "08. @Pack Name to cross-platform Compatible Filenames", "09. @Pack Add Preview if missing 00:30 for 30sec (&save)", "10. @Pack Make all DLC IDs unique", "11. <@PackAdd DD (5 Levels)>", "12. Add DD (5 Levels) when missing", "13. Import all Duplicates as Alternates", "14. Import any Custom as Alternate if an Original exists", "15. Move the Imported files to temp/0_old", "16. Import with Artist/Title same as Artist/Title/Album Sort", "17. Repack with Artist/Title same as Artist/Title/Album Sort", "18. <Import without The/Die at the beginning of Artist/Title Sort>", "19. <Pack without The/Die at the beginning of Artist/Title Sort>", "20. Import with the The/Die at the end of Title Sort", "21. Pack with The/Die at the end of Title/Title Sort", "22. Import with the The/Die only at the end of Artist/Album/xx Sort", "23. Pack with The/Die only at the end of Artist/Album/xx Sort", "24. @Import Use translation tables for naming standardization", "25. If Original don't add QAs (NOs;DLC/ORIG;etc.)", "26. When packing Add 5 Levels of DD only to Guitar tracks", "27. Convert and Transfer/FTP", "28. If Original don't add QAs (NOs;DLC/ORIG;etc.) except for File Names", "29. When NOT importing a Duplicate Move it to _duplicate", "30. When NOT importing a broken song Move it to _broken", "31. When removing DD use internal logic not DDC", "32. When importing alternates add newer/older instead of alt.0author", "33. Forcibly Update Import location of all DB fields", "34. @Import Add Preview if missing (lenght> as per config)", "35. Remove illegal characters from Songs Metadata", "36. Keep the Uncompressed Songs superorganized", "37. Import other formats but PC", "38. Import only the unpacked songs already in the \"0/\" Temp folder", "39. Encrypt PS3 Retails songs, with External tool", "40. Delete ORIG HSAN/OGG when Packing Retails songs", "41. Try to get Track No. &Details from Spotify (&yb links)", "42. Save Log After Import (Imported Folder)", "43. @Import Set the DLCID autom", "44. @Pack Set the DLCID autom", "45. <Convert Originals>", "46. Duplicate Mangement, Title added info is inbetween separators: []", "47. Add New Toolkit v. and RePackedByAuthor", "48. @Import Remove Multitrack/Live/Acoustic info from Title", "49. @Pack Also Copy/FTP", "50. @Import Manually assess duplicates at the end", "51. @Import&Unpack Overwrite the XML", "52. @Pack keep Bass DD if indicated so", "53. @Pack keep All DD if indicated so", "54. @Pack consider All songs as beta (place them top of the list)", "55. Gen Preview if Preview=Audio or Preview is longer than config (default 30s)", "56. Duplicate manag ignores Multitracks", "57. Don't save Author when generic (i.e. Custom Song Creator)", "58. @Pack try to get Track No again (&don't save)", "59. @Pack try to get Track No again (&save)", "60. @Rebuild don't overwrite Standard Song Info (Tit,Art,Alb,Prw,Aut,Des,Com)", "61. @Rebuild don't overwrite Standard Song Info (Cover,Year)", "62. <@Pack duplicate singleTracks L->R / R->L>", "63. @Pack Remove Remote File if GameData has been read", "64. @Pack ONLY Copy/FTP the Last Packed song", "65. @Pack ONLY Copy/FTP the Initially Imported song", "66. Duplicate manag. ignores Live Songs", "67. Import duplicates (hash)", "68. Delete obvious duplicates (hash) during dupli assesment", "69. Compress AudioFiles to 128VBR @Pack/Import if bigger than 136k", "70. @Repack pack Preview (bugfix)", "71. <@Import/Repack check if Original flag is in the Official list and correct>", "72. Import other formats but PC, as standalone", "73. Add Track Info&Comments beginning of Lyrics", "74. Add Track start into Vocals", "75. Copy to \\0\\0_Old (Overwrites 15 Move to old)", "76. Include Tones/arangements Db changes", "77. After Import open MainDB", "78. @Import Fix Audio Issues at end", "79. @Import Manually Asses All Suspicious Duplicates", "80. Duplicate manag. ignores Acoustic Songs", "81. Any Delete (non psarc) goes to RecycleBin", "82. Show warning that It will connect to Spotify", "83. Import All suspicious Duplicates as Duplicates (Ignore)", "84. When checking Songs validate wem bitrate (10% wem conversion raises the bitrate)", "85. Apply standard naming to all duplicates", "86. Keep XML Manipulations", "87. Use Latest Spotify API (Web)", "88. Gen Preview if Preview is shorter than config (default 10s)", "89. @Mass pack split into xxx (param in xml) songs", "90. When adding times into vocals(74) add only in seconds", "91. Add group to Filename", "92. Package for a HAN enabled PS3", "93. If packaging for a HAN Enabled PS3 then also copy Retail(RS2012) Songs", "94. After lyrics manipulation Open them in Notepad", "95. @Export create Package (in@0_temp)", "96. @Export create Tabs", "97. Pack only never packed Songs (Overwrites 98)", "98. Pack only never packed Songs for the target Platform", "99. <If Group pack ignore songs that are also in other Groups>", "x100. <Pack anew instead of converting (e.g. Pack Orig file)>", "x101. After packing check song", "<102. >", "<103. >", "<104. >", "<105. >", "<106. >", "<107. >", "<108. >", "<109. >", "<110. >", "<111. >", "<112. >", "<113. >", "<114. >", "<115. >", "<116. >", "<117. >", "<118. >", "<119. >", "<120. >", "<121. >", "<122. >", "<123. >", "<124. >", "<125. >", "<126. >", "<127. >", "<128. >", "<129. >", "<130. >", "<131. >", "<132. >", "<133. >", "<134. >", "<135. >", "<136. >", "<137. >", "<138. >", "<139. >", "<140. >", "<141. >", "<142. >", "<143. >", "<144. >", "<145. >", "<146. >", "<147. >", "<148. >", "<149. >", "<150. >", "<151. >", "<152. >", "<153. >", "<154. >", "<155. >", "<156. >", "<157. >", "<158. >", "<159. >", "<160. >", "<161. >", "<162. >", "<163. >", "<164. >", "<165. >", "<166. >", "<167. >", "<168. >", "<169. >", "<170. >", "<171. >", "<172. >", "<173. >", "<174. >", "<175. >", "<176. >", "<177. >", "<178. >", "<179. >", "<180. >", "<181. >", "<182. >", "<183. >", "<184. >", "<185. >", "<186. >", "<187. >", "<188. >", "<189. >", "<190. >", "<191. >", "<192. >", "<193. >", "<194. >", "<195. >", "<196. >", "<197. >", "<198. >", "<199. >", "<200. >" });
            chbx_Additional_Manipulations.Location = new System.Drawing.Point(8, 388);
            chbx_Additional_Manipulations.Margin = new Padding(0);
            chbx_Additional_Manipulations.Name = "chbx_Additional_Manipulations";
            chbx_Additional_Manipulations.Size = new System.Drawing.Size(466, 94);
            chbx_Additional_Manipulations.TabIndex = 29;
            // 
            // lbl_Mask
            // 
            lbl_Mask.AutoSize = true;
            lbl_Mask.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Mask.Location = new System.Drawing.Point(6, 371);
            lbl_Mask.Margin = new Padding(2, 0, 2, 0);
            lbl_Mask.Name = "lbl_Mask";
            lbl_Mask.Size = new System.Drawing.Size(82, 15);
            lbl_Mask.TabIndex = 319;
            lbl_Mask.Text = "Mask Preview:";
            // 
            // btn_Preview_Artist_Sort
            // 
            btn_Preview_Artist_Sort.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Artist_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Artist_Sort.Location = new System.Drawing.Point(522, 270);
            btn_Preview_Artist_Sort.Margin = new Padding(2);
            btn_Preview_Artist_Sort.Name = "btn_Preview_Artist_Sort";
            btn_Preview_Artist_Sort.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Artist_Sort.TabIndex = 45;
            toolTip1.SetToolTip(btn_Preview_Artist_Sort, "Preview Artist Sort Mask");
            btn_Preview_Artist_Sort.UseVisualStyleBackColor = false;
            btn_Preview_Artist_Sort.Click += btn_Preview_Artist_Sort_Click;
            // 
            // cbx_Activ_Artist_Sort
            // 
            cbx_Activ_Artist_Sort.AutoSize = true;
            cbx_Activ_Artist_Sort.Checked = true;
            cbx_Activ_Artist_Sort.CheckState = CheckState.Checked;
            cbx_Activ_Artist_Sort.Location = new System.Drawing.Point(506, 270);
            cbx_Activ_Artist_Sort.Margin = new Padding(2);
            cbx_Activ_Artist_Sort.Name = "cbx_Activ_Artist_Sort";
            cbx_Activ_Artist_Sort.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Artist_Sort.TabIndex = 44;
            cbx_Activ_Artist_Sort.UseVisualStyleBackColor = true;
            cbx_Activ_Artist_Sort.CheckedChanged += cbx_Activ_Artist_Sort_CheckedChanged;
            // 
            // lbl_Artist_Sort
            // 
            lbl_Artist_Sort.AutoSize = true;
            lbl_Artist_Sort.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Artist_Sort.Location = new System.Drawing.Point(6, 272);
            lbl_Artist_Sort.Margin = new Padding(2, 0, 2, 0);
            lbl_Artist_Sort.Name = "lbl_Artist_Sort";
            lbl_Artist_Sort.Size = new System.Drawing.Size(59, 15);
            lbl_Artist_Sort.TabIndex = 316;
            lbl_Artist_Sort.Text = "Artist Sort";
            // 
            // cbx_Artist_Sort
            // 
            cbx_Artist_Sort.DropDownWidth = 280;
            cbx_Artist_Sort.FormattingEnabled = true;
            cbx_Artist_Sort.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Artist_Sort.Location = new System.Drawing.Point(398, 268);
            cbx_Artist_Sort.Margin = new Padding(2);
            cbx_Artist_Sort.Name = "cbx_Artist_Sort";
            cbx_Artist_Sort.Size = new System.Drawing.Size(106, 23);
            cbx_Artist_Sort.TabIndex = 43;
            cbx_Artist_Sort.SelectedIndexChanged += cbx_Artist_Sort_SelectedIndexChanged;
            // 
            // txt_Artist_Sort
            // 
            txt_Artist_Sort.Location = new System.Drawing.Point(68, 268);
            txt_Artist_Sort.Margin = new Padding(2);
            txt_Artist_Sort.Name = "txt_Artist_Sort";
            txt_Artist_Sort.Size = new System.Drawing.Size(328, 23);
            txt_Artist_Sort.TabIndex = 42;
            txt_Artist_Sort.Text = "<Beta><Artist>";
            // 
            // pB_ReadDLCs
            // 
            pB_ReadDLCs.Location = new System.Drawing.Point(6, 150);
            pB_ReadDLCs.Margin = new Padding(2);
            pB_ReadDLCs.Maximum = 20000;
            pB_ReadDLCs.Name = "pB_ReadDLCs";
            pB_ReadDLCs.Size = new System.Drawing.Size(530, 32);
            pB_ReadDLCs.Step = 1;
            pB_ReadDLCs.TabIndex = 263;
            toolTip1.SetToolTip(pB_ReadDLCs, "Progress bar for different operations of CDLC Manager.");
            // 
            // btn_Preview_File_Name
            // 
            btn_Preview_File_Name.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_File_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_File_Name.Location = new System.Drawing.Point(522, 333);
            btn_Preview_File_Name.Margin = new Padding(2);
            btn_Preview_File_Name.Name = "btn_Preview_File_Name";
            btn_Preview_File_Name.Size = new System.Drawing.Size(14, 14);
            btn_Preview_File_Name.TabIndex = 53;
            toolTip1.SetToolTip(btn_Preview_File_Name, "Preview File Name Mask");
            btn_Preview_File_Name.UseVisualStyleBackColor = false;
            btn_Preview_File_Name.Click += btn_Preview_File_Name_Click;
            // 
            // btn_Preview_Album
            // 
            btn_Preview_Album.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Album.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Album.Location = new System.Drawing.Point(522, 291);
            btn_Preview_Album.Margin = new Padding(2);
            btn_Preview_Album.Name = "btn_Preview_Album";
            btn_Preview_Album.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Album.TabIndex = 49;
            toolTip1.SetToolTip(btn_Preview_Album, "Preview Album Mask");
            btn_Preview_Album.UseVisualStyleBackColor = false;
            btn_Preview_Album.Click += btn_Preview_Album_Click;
            // 
            // btn_Preview_Artist
            // 
            btn_Preview_Artist.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Artist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Artist.Location = new System.Drawing.Point(522, 250);
            btn_Preview_Artist.Margin = new Padding(2);
            btn_Preview_Artist.Name = "btn_Preview_Artist";
            btn_Preview_Artist.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Artist.TabIndex = 41;
            toolTip1.SetToolTip(btn_Preview_Artist, "Preview Artist Mask");
            btn_Preview_Artist.UseVisualStyleBackColor = false;
            btn_Preview_Artist.Click += btn_Preview_Artist_Click;
            // 
            // btn_Preview_Title_Sort
            // 
            btn_Preview_Title_Sort.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Title_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Title_Sort.Location = new System.Drawing.Point(522, 232);
            btn_Preview_Title_Sort.Margin = new Padding(2);
            btn_Preview_Title_Sort.Name = "btn_Preview_Title_Sort";
            btn_Preview_Title_Sort.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Title_Sort.TabIndex = 37;
            toolTip1.SetToolTip(btn_Preview_Title_Sort, "Preview Title Sort Mask");
            btn_Preview_Title_Sort.UseVisualStyleBackColor = false;
            btn_Preview_Title_Sort.Click += btn_Preview_Title_Sort_Click;
            // 
            // btn_Preview_Title
            // 
            btn_Preview_Title.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Title.Location = new System.Drawing.Point(522, 214);
            btn_Preview_Title.Margin = new Padding(2);
            btn_Preview_Title.Name = "btn_Preview_Title";
            btn_Preview_Title.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Title.TabIndex = 33;
            toolTip1.SetToolTip(btn_Preview_Title, "Preview Title Mask");
            btn_Preview_Title.UseVisualStyleBackColor = false;
            btn_Preview_Title.Click += btn_Preview_Title_Click;
            // 
            // cbx_Activ_File_Name
            // 
            cbx_Activ_File_Name.AutoSize = true;
            cbx_Activ_File_Name.Checked = true;
            cbx_Activ_File_Name.CheckState = CheckState.Checked;
            cbx_Activ_File_Name.Location = new System.Drawing.Point(506, 333);
            cbx_Activ_File_Name.Margin = new Padding(2);
            cbx_Activ_File_Name.Name = "cbx_Activ_File_Name";
            cbx_Activ_File_Name.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_File_Name.TabIndex = 52;
            cbx_Activ_File_Name.UseVisualStyleBackColor = true;
            cbx_Activ_File_Name.CheckedChanged += cbx_Activ_File_Name_CheckedChanged;
            // 
            // cbx_Activ_Album
            // 
            cbx_Activ_Album.AutoSize = true;
            cbx_Activ_Album.Checked = true;
            cbx_Activ_Album.CheckState = CheckState.Checked;
            cbx_Activ_Album.Location = new System.Drawing.Point(506, 291);
            cbx_Activ_Album.Margin = new Padding(2);
            cbx_Activ_Album.Name = "cbx_Activ_Album";
            cbx_Activ_Album.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Album.TabIndex = 48;
            cbx_Activ_Album.UseVisualStyleBackColor = true;
            cbx_Activ_Album.CheckedChanged += cbx_Activ_Album_CheckedChanged;
            // 
            // cbx_Activ_Artist
            // 
            cbx_Activ_Artist.AutoSize = true;
            cbx_Activ_Artist.Checked = true;
            cbx_Activ_Artist.CheckState = CheckState.Checked;
            cbx_Activ_Artist.Location = new System.Drawing.Point(506, 250);
            cbx_Activ_Artist.Margin = new Padding(2);
            cbx_Activ_Artist.Name = "cbx_Activ_Artist";
            cbx_Activ_Artist.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Artist.TabIndex = 40;
            cbx_Activ_Artist.UseVisualStyleBackColor = true;
            cbx_Activ_Artist.CheckedChanged += cbx_Activ_Artist_CheckedChanged;
            // 
            // cbx_Activ_Title_Sort
            // 
            cbx_Activ_Title_Sort.AutoSize = true;
            cbx_Activ_Title_Sort.Checked = true;
            cbx_Activ_Title_Sort.CheckState = CheckState.Checked;
            cbx_Activ_Title_Sort.Location = new System.Drawing.Point(506, 234);
            cbx_Activ_Title_Sort.Margin = new Padding(2);
            cbx_Activ_Title_Sort.Name = "cbx_Activ_Title_Sort";
            cbx_Activ_Title_Sort.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Title_Sort.TabIndex = 36;
            cbx_Activ_Title_Sort.UseVisualStyleBackColor = true;
            cbx_Activ_Title_Sort.CheckedChanged += cbx_Activ_Title_Sort_CheckedChanged;
            // 
            // cbx_Activ_Title
            // 
            cbx_Activ_Title.AutoSize = true;
            cbx_Activ_Title.Checked = true;
            cbx_Activ_Title.CheckState = CheckState.Checked;
            cbx_Activ_Title.Location = new System.Drawing.Point(506, 214);
            cbx_Activ_Title.Margin = new Padding(2);
            cbx_Activ_Title.Name = "cbx_Activ_Title";
            cbx_Activ_Title.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Title.TabIndex = 32;
            cbx_Activ_Title.UseVisualStyleBackColor = true;
            cbx_Activ_Title.CheckedChanged += cbx_Activ_Title_CheckedChanged;
            // 
            // lbl_Artist
            // 
            lbl_Artist.AutoSize = true;
            lbl_Artist.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Artist.Location = new System.Drawing.Point(6, 250);
            lbl_Artist.Margin = new Padding(2, 0, 2, 0);
            lbl_Artist.Name = "lbl_Artist";
            lbl_Artist.Size = new System.Drawing.Size(35, 15);
            lbl_Artist.TabIndex = 303;
            lbl_Artist.Text = "Artist";
            // 
            // cbx_Artist
            // 
            cbx_Artist.DropDownWidth = 280;
            cbx_Artist.FormattingEnabled = true;
            cbx_Artist.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Artist.Location = new System.Drawing.Point(398, 250);
            cbx_Artist.Margin = new Padding(2);
            cbx_Artist.Name = "cbx_Artist";
            cbx_Artist.Size = new System.Drawing.Size(106, 23);
            cbx_Artist.TabIndex = 39;
            cbx_Artist.SelectedIndexChanged += cbx_Artist_SelectedIndexChanged;
            // 
            // txt_Artist
            // 
            txt_Artist.Location = new System.Drawing.Point(68, 250);
            txt_Artist.Margin = new Padding(2);
            txt_Artist.Name = "txt_Artist";
            txt_Artist.Size = new System.Drawing.Size(328, 23);
            txt_Artist.TabIndex = 38;
            txt_Artist.Text = "<Artist>-<CDLC>-<Avail. Tracks>-<DD>-<Quality Assurance Attributes>-<Bass Has DynamicDificulty>";
            // 
            // cbx_Title
            // 
            cbx_Title.DropDownWidth = 280;
            cbx_Title.FormattingEnabled = true;
            cbx_Title.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Title.Location = new System.Drawing.Point(398, 210);
            cbx_Title.Margin = new Padding(2);
            cbx_Title.Name = "cbx_Title";
            cbx_Title.Size = new System.Drawing.Size(106, 23);
            cbx_Title.TabIndex = 31;
            cbx_Title.SelectedIndexChanged += cbx_Title_SelectedIndexChanged;
            // 
            // txt_Title
            // 
            txt_Title.Location = new System.Drawing.Point(68, 210);
            txt_Title.Margin = new Padding(2);
            txt_Title.Name = "txt_Title";
            txt_Title.Size = new System.Drawing.Size(328, 23);
            txt_Title.TabIndex = 30;
            txt_Title.Text = "<Title>";
            // 
            // lbl_File_Name
            // 
            lbl_File_Name.AutoSize = true;
            lbl_File_Name.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_File_Name.Location = new System.Drawing.Point(6, 333);
            lbl_File_Name.Margin = new Padding(2, 0, 2, 0);
            lbl_File_Name.Name = "lbl_File_Name";
            lbl_File_Name.Size = new System.Drawing.Size(60, 15);
            lbl_File_Name.TabIndex = 297;
            lbl_File_Name.Text = "File Name";
            // 
            // lbl_Album
            // 
            lbl_Album.AutoSize = true;
            lbl_Album.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Album.Location = new System.Drawing.Point(6, 291);
            lbl_Album.Margin = new Padding(2, 0, 2, 0);
            lbl_Album.Name = "lbl_Album";
            lbl_Album.Size = new System.Drawing.Size(43, 15);
            lbl_Album.TabIndex = 296;
            lbl_Album.Text = "Album";
            // 
            // lbl_Title_Sort
            // 
            lbl_Title_Sort.AutoSize = true;
            lbl_Title_Sort.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Title_Sort.Location = new System.Drawing.Point(6, 230);
            lbl_Title_Sort.Margin = new Padding(2, 0, 2, 0);
            lbl_Title_Sort.Name = "lbl_Title_Sort";
            lbl_Title_Sort.Size = new System.Drawing.Size(53, 15);
            lbl_Title_Sort.TabIndex = 295;
            lbl_Title_Sort.Text = "Title Sort";
            // 
            // cbx_File_Name
            // 
            cbx_File_Name.DropDownWidth = 280;
            cbx_File_Name.FormattingEnabled = true;
            cbx_File_Name.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_File_Name.Location = new System.Drawing.Point(398, 329);
            cbx_File_Name.Margin = new Padding(2);
            cbx_File_Name.Name = "cbx_File_Name";
            cbx_File_Name.Size = new System.Drawing.Size(106, 23);
            cbx_File_Name.TabIndex = 51;
            cbx_File_Name.SelectedIndexChanged += cbx_File_Name_SelectedIndexChanged;
            // 
            // txt_File_Name
            // 
            txt_File_Name.Location = new System.Drawing.Point(68, 329);
            txt_File_Name.Margin = new Padding(2);
            txt_File_Name.Name = "txt_File_Name";
            txt_File_Name.Size = new System.Drawing.Size(328, 23);
            txt_File_Name.TabIndex = 50;
            txt_File_Name.Text = "<CDLC>-<Artist>-<Year>-<Album><Track No.>-<Title>-<DD>-<Avail. Tracks>-<Quality Assurance Attributes>-v<Version>-<Bass Has DynamicDificulty>";
            // 
            // cbx_Album
            // 
            cbx_Album.DropDownWidth = 280;
            cbx_Album.FormattingEnabled = true;
            cbx_Album.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Album.Location = new System.Drawing.Point(398, 287);
            cbx_Album.Margin = new Padding(2);
            cbx_Album.Name = "cbx_Album";
            cbx_Album.Size = new System.Drawing.Size(106, 23);
            cbx_Album.TabIndex = 47;
            cbx_Album.SelectedIndexChanged += cbx_Album_SelectedIndexChanged;
            // 
            // txt_Album
            // 
            txt_Album.Location = new System.Drawing.Point(68, 287);
            txt_Album.Margin = new Padding(2);
            txt_Album.Name = "txt_Album";
            txt_Album.Size = new System.Drawing.Size(328, 23);
            txt_Album.TabIndex = 46;
            txt_Album.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            // 
            // cbx_Title_Sort
            // 
            cbx_Title_Sort.DropDownWidth = 280;
            cbx_Title_Sort.FormattingEnabled = true;
            cbx_Title_Sort.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Title_Sort.Location = new System.Drawing.Point(398, 230);
            cbx_Title_Sort.Margin = new Padding(2);
            cbx_Title_Sort.Name = "cbx_Title_Sort";
            cbx_Title_Sort.Size = new System.Drawing.Size(106, 23);
            cbx_Title_Sort.TabIndex = 35;
            cbx_Title_Sort.SelectedIndexChanged += cbx_Title_Sort_SelectedIndexChanged;
            // 
            // txt_Title_Sort
            // 
            txt_Title_Sort.Location = new System.Drawing.Point(68, 230);
            txt_Title_Sort.Margin = new Padding(2);
            txt_Title_Sort.Name = "txt_Title_Sort";
            txt_Title_Sort.Size = new System.Drawing.Size(328, 23);
            txt_Title_Sort.TabIndex = 34;
            txt_Title_Sort.Text = "<Year><Album><Track No.><Title>";
            // 
            // lbl_Title
            // 
            lbl_Title.AutoSize = true;
            lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Title.Location = new System.Drawing.Point(6, 214);
            lbl_Title.Margin = new Padding(2, 0, 2, 0);
            lbl_Title.Name = "lbl_Title";
            lbl_Title.Size = new System.Drawing.Size(29, 15);
            lbl_Title.TabIndex = 300;
            lbl_Title.Text = "Title";
            // 
            // chbx_DebugB
            // 
            chbx_DebugB.AutoSize = true;
            chbx_DebugB.Location = new System.Drawing.Point(306, 3);
            chbx_DebugB.Margin = new Padding(2);
            chbx_DebugB.Name = "chbx_DebugB";
            chbx_DebugB.Size = new System.Drawing.Size(79, 19);
            chbx_DebugB.TabIndex = 16;
            chbx_DebugB.Text = "Advanced";
            toolTip1.SetToolTip(chbx_DebugB, "Activates a series of Advanced Options. \r\nDisables also:\r\n25. Import Use translation tables for naming standardization\r\n16. Move the Imported files to temp/0_old\r\n50. Pack Also Copy/FTP");
            chbx_DebugB.UseVisualStyleBackColor = true;
            chbx_DebugB.CheckedChanged += chbx_DebugB_CheckedChanged;
            // 
            // txt_DBFolder
            // 
            txt_DBFolder.Location = new System.Drawing.Point(126, 44);
            txt_DBFolder.Margin = new Padding(2);
            txt_DBFolder.Name = "txt_DBFolder";
            txt_DBFolder.Size = new System.Drawing.Size(158, 23);
            txt_DBFolder.TabIndex = 8;
            toolTip1.SetToolTip(txt_DBFolder, "Requires a Access 2010 DB.");
            txt_DBFolder.KeyDown += txt_DBFolder_KeyDown;
            txt_DBFolder.KeyPress += Txt_DBFolder_KeyPress;
            txt_DBFolder.Leave += txt_DBFolder_Leave;
            // 
            // btn_DBFolder
            // 
            btn_DBFolder.Location = new System.Drawing.Point(282, 43);
            btn_DBFolder.Margin = new Padding(0);
            btn_DBFolder.Name = "btn_DBFolder";
            btn_DBFolder.Size = new System.Drawing.Size(22, 20);
            btn_DBFolder.TabIndex = 9;
            btn_DBFolder.Text = "...";
            toolTip1.SetToolTip(btn_DBFolder, "Select an M$ Access DB File to store the CDLC & User metadata.");
            btn_DBFolder.UseVisualStyleBackColor = true;
            btn_DBFolder.Click += btn_DBFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.ControlText;
            label1.Location = new System.Drawing.Point(6, 46);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(85, 15);
            label1.TabIndex = 274;
            label1.Text = "DB Folder Path";
            // 
            // btn_Close
            // 
            btn_Close.BackColor = System.Drawing.Color.LightSteelBlue;
            btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Close.Location = new System.Drawing.Point(476, 182);
            btn_Close.Margin = new Padding(0);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new System.Drawing.Size(60, 26);
            btn_Close.TabIndex = 55;
            btn_Close.Text = "Close";
            toolTip1.SetToolTip(btn_Close, "Revert back to Normal Rocksmith ToolKit functions.");
            btn_Close.UseVisualStyleBackColor = false;
            btn_Close.Click += btn_Close_Click;
            // 
            // txt_RocksmithDLCPath
            // 
            txt_RocksmithDLCPath.Location = new System.Drawing.Point(126, 2);
            txt_RocksmithDLCPath.Margin = new Padding(2);
            txt_RocksmithDLCPath.Name = "txt_RocksmithDLCPath";
            txt_RocksmithDLCPath.Size = new System.Drawing.Size(158, 23);
            txt_RocksmithDLCPath.TabIndex = 2;
            toolTip1.SetToolTip(txt_RocksmithDLCPath, "Import Location.\r\nIf it is the Rocksmith Location then Files will be moved out and only replaced with Manged version at Repack.");
            txt_RocksmithDLCPath.KeyPress += txt_RocksmithDLCPath_KeyPress;
            txt_RocksmithDLCPath.Leave += txt_RocksmithDLCPath_Leave;
            // 
            // txt_TempPath
            // 
            txt_TempPath.Location = new System.Drawing.Point(126, 22);
            txt_TempPath.Margin = new Padding(2);
            txt_TempPath.Name = "txt_TempPath";
            txt_TempPath.Size = new System.Drawing.Size(158, 23);
            txt_TempPath.TabIndex = 5;
            txt_TempPath.KeyPress += txt_TempPath_KeyPress;
            // 
            // chbx_CleanTemp
            // 
            chbx_CleanTemp.AutoSize = true;
            chbx_CleanTemp.Location = new System.Drawing.Point(322, 24);
            chbx_CleanTemp.Margin = new Padding(2);
            chbx_CleanTemp.Name = "chbx_CleanTemp";
            chbx_CleanTemp.Size = new System.Drawing.Size(113, 19);
            chbx_CleanTemp.TabIndex = 17;
            chbx_CleanTemp.Text = "Clean DB&&Temp";
            toolTip1.SetToolTip(chbx_CleanTemp, "Cleans the Temp, Old, Duplicate, Repacked, Broken Folders");
            chbx_CleanTemp.UseVisualStyleBackColor = true;
            // 
            // btn_SteamDLCFolder
            // 
            btn_SteamDLCFolder.Location = new System.Drawing.Point(282, 3);
            btn_SteamDLCFolder.Margin = new Padding(0);
            btn_SteamDLCFolder.Name = "btn_SteamDLCFolder";
            btn_SteamDLCFolder.Size = new System.Drawing.Size(22, 20);
            btn_SteamDLCFolder.TabIndex = 3;
            btn_SteamDLCFolder.Text = "...";
            toolTip1.SetToolTip(btn_SteamDLCFolder, "Select an Importing CDLC Folder");
            btn_SteamDLCFolder.UseVisualStyleBackColor = true;
            btn_SteamDLCFolder.Click += btn_SteamDLCFolder_Click;
            // 
            // rtxt_StatisticsOnReadDLCs
            // 
            rtxt_StatisticsOnReadDLCs.Font = new System.Drawing.Font("Segoe UI", 7F);
            rtxt_StatisticsOnReadDLCs.Location = new System.Drawing.Point(6, 523);
            rtxt_StatisticsOnReadDLCs.Margin = new Padding(2);
            rtxt_StatisticsOnReadDLCs.Name = "rtxt_StatisticsOnReadDLCs";
            rtxt_StatisticsOnReadDLCs.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxt_StatisticsOnReadDLCs.Size = new System.Drawing.Size(528, 35);
            rtxt_StatisticsOnReadDLCs.TabIndex = 264;
            rtxt_StatisticsOnReadDLCs.Text = "";
            // 
            // btn_PopulateDB
            // 
            btn_PopulateDB.BackColor = System.Drawing.SystemColors.ActiveCaption;
            btn_PopulateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_PopulateDB.Location = new System.Drawing.Point(434, 60);
            btn_PopulateDB.Margin = new Padding(2);
            btn_PopulateDB.Name = "btn_PopulateDB";
            btn_PopulateDB.Size = new System.Drawing.Size(102, 24);
            btn_PopulateDB.TabIndex = 13;
            btn_PopulateDB.Text = "Import DLCs";
            btn_PopulateDB.UseVisualStyleBackColor = false;
            btn_PopulateDB.Click += btn_PopulateDB_Click;
            // 
            // btn_OpenMainDB
            // 
            btn_OpenMainDB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_OpenMainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_OpenMainDB.Location = new System.Drawing.Point(434, 26);
            btn_OpenMainDB.Margin = new Padding(0);
            btn_OpenMainDB.Name = "btn_OpenMainDB";
            btn_OpenMainDB.Size = new System.Drawing.Size(102, 34);
            btn_OpenMainDB.TabIndex = 10;
            btn_OpenMainDB.Text = "Open Main DB";
            toolTip1.SetToolTip(btn_OpenMainDB, "Main DB Listing All Imported CDLC");
            btn_OpenMainDB.UseVisualStyleBackColor = false;
            btn_OpenMainDB.Click += btn_DecompressAll_Click;
            // 
            // lbl_RocksmithDLCPath
            // 
            lbl_RocksmithDLCPath.AutoSize = true;
            lbl_RocksmithDLCPath.Font = new System.Drawing.Font("Calibri", 8F);
            lbl_RocksmithDLCPath.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_RocksmithDLCPath.Location = new System.Drawing.Point(6, 4);
            lbl_RocksmithDLCPath.Margin = new Padding(2, 0, 2, 0);
            lbl_RocksmithDLCPath.Name = "lbl_RocksmithDLCPath";
            lbl_RocksmithDLCPath.Size = new System.Drawing.Size(96, 13);
            lbl_RocksmithDLCPath.TabIndex = 259;
            lbl_RocksmithDLCPath.Text = "Importing DLC path";
            // 
            // btn_TempPath
            // 
            btn_TempPath.Location = new System.Drawing.Point(282, 23);
            btn_TempPath.Margin = new Padding(0);
            btn_TempPath.Name = "btn_TempPath";
            btn_TempPath.Size = new System.Drawing.Size(22, 20);
            btn_TempPath.TabIndex = 6;
            btn_TempPath.Text = "...";
            btn_TempPath.UseVisualStyleBackColor = true;
            btn_TempPath.Click += btn_TempPath_Click;
            // 
            // lbl_TempFolders
            // 
            lbl_TempFolders.AutoSize = true;
            lbl_TempFolders.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_TempFolders.Location = new System.Drawing.Point(4, 26);
            lbl_TempFolders.Margin = new Padding(2, 0, 2, 0);
            lbl_TempFolders.Name = "lbl_TempFolders";
            lbl_TempFolders.Size = new System.Drawing.Size(99, 15);
            lbl_TempFolders.TabIndex = 258;
            lbl_TempFolders.Text = "Temp Folder Path";
            // 
            // lbl_PreviewText
            // 
            lbl_PreviewText.AutoEllipsis = true;
            lbl_PreviewText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.125F);
            lbl_PreviewText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            lbl_PreviewText.Location = new System.Drawing.Point(86, 373);
            lbl_PreviewText.Margin = new Padding(2, 0, 2, 0);
            lbl_PreviewText.Name = "lbl_PreviewText";
            lbl_PreviewText.Size = new System.Drawing.Size(448, 18);
            lbl_PreviewText.TabIndex = 285;
            lbl_PreviewText.Text = "FN: Beta(0)CDLC/ORIG-Artist-Year-Album-TrackNo(ifexisting)-Title-TrackAvail(LRBVS)-Version.psarc";
            // 
            // btn_ApplyStandardization
            // 
            btn_ApplyStandardization.BackColor = System.Drawing.SystemColors.Control;
            btn_ApplyStandardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ApplyStandardization.Location = new System.Drawing.Point(404, 87);
            btn_ApplyStandardization.Margin = new Padding(2);
            btn_ApplyStandardization.Name = "btn_ApplyStandardization";
            btn_ApplyStandardization.Size = new System.Drawing.Size(12, 62);
            btn_ApplyStandardization.TabIndex = 61;
            btn_ApplyStandardization.Text = "Apply";
            toolTip1.SetToolTip(btn_ApplyStandardization, "Apply Standardization Rules");
            btn_ApplyStandardization.UseVisualStyleBackColor = false;
            btn_ApplyStandardization.Click += button1_Click;
            // 
            // btn_LoadRetailSongs
            // 
            btn_LoadRetailSongs.BackColor = System.Drawing.SystemColors.Control;
            btn_LoadRetailSongs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            btn_LoadRetailSongs.Location = new System.Drawing.Point(522, 86);
            btn_LoadRetailSongs.Margin = new Padding(2);
            btn_LoadRetailSongs.Name = "btn_LoadRetailSongs";
            btn_LoadRetailSongs.Size = new System.Drawing.Size(15, 62);
            btn_LoadRetailSongs.TabIndex = 62;
            btn_LoadRetailSongs.Text = "Load";
            toolTip1.SetToolTip(btn_LoadRetailSongs, "Load Retail files: Disc Songs, DLC Songs, RS1DLC Songs");
            btn_LoadRetailSongs.UseVisualStyleBackColor = false;
            btn_LoadRetailSongs.Click += btn_LoadRetailSongs_Click;
            // 
            // btn_Standardization
            // 
            btn_Standardization.BackColor = System.Drawing.SystemColors.Control;
            btn_Standardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            btn_Standardization.Location = new System.Drawing.Point(416, 86);
            btn_Standardization.Margin = new Padding(2);
            btn_Standardization.Name = "btn_Standardization";
            btn_Standardization.Size = new System.Drawing.Size(62, 62);
            btn_Standardization.TabIndex = 11;
            btn_Standardization.Text = "Open Standardization DB";
            toolTip1.SetToolTip(btn_Standardization, "Here you can decide what Standardizations you want to apply to Artist Names, Album Names, Covers or Short Names");
            btn_Standardization.UseVisualStyleBackColor = false;
            btn_Standardization.Click += btn_Standardization_Click;
            // 
            // btn_GoImport
            // 
            btn_GoImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoImport.Location = new System.Drawing.Point(102, 4);
            btn_GoImport.Margin = new Padding(2);
            btn_GoImport.Name = "btn_GoImport";
            btn_GoImport.Size = new System.Drawing.Size(22, 16);
            btn_GoImport.TabIndex = 1;
            btn_GoImport.Text = "<->";
            toolTip1.SetToolTip(btn_GoImport, "Open Importing CDLC Folder");
            btn_GoImport.UseVisualStyleBackColor = true;
            btn_GoImport.Click += btn_GoImport_Click;
            // 
            // btn_GoTemp
            // 
            btn_GoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoTemp.Location = new System.Drawing.Point(102, 24);
            btn_GoTemp.Margin = new Padding(2);
            btn_GoTemp.Name = "btn_GoTemp";
            btn_GoTemp.Size = new System.Drawing.Size(22, 16);
            btn_GoTemp.TabIndex = 4;
            btn_GoTemp.Text = "<->";
            toolTip1.SetToolTip(btn_GoTemp, "Open the location that stores the decompressed CDLCs");
            btn_GoTemp.UseVisualStyleBackColor = true;
            btn_GoTemp.Click += btn_GoTemp_Click;
            // 
            // btn_GoDB
            // 
            btn_GoDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoDB.Location = new System.Drawing.Point(102, 46);
            btn_GoDB.Margin = new Padding(2);
            btn_GoDB.Name = "btn_GoDB";
            btn_GoDB.Size = new System.Drawing.Size(22, 16);
            btn_GoDB.TabIndex = 7;
            btn_GoDB.Text = "<->";
            toolTip1.SetToolTip(btn_GoDB, "Open the location of the M$ Access DB used to store the CDLC metadata.");
            btn_GoDB.UseVisualStyleBackColor = true;
            btn_GoDB.Click += btn_GoDB_Click;
            // 
            // btn_OpenDB
            // 
            btn_OpenDB.BackColor = System.Drawing.SystemColors.Control;
            btn_OpenDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            btn_OpenDB.Location = new System.Drawing.Point(6, 182);
            btn_OpenDB.Margin = new Padding(0);
            btn_OpenDB.Name = "btn_OpenDB";
            btn_OpenDB.Size = new System.Drawing.Size(64, 26);
            btn_OpenDB.TabIndex = 54;
            btn_OpenDB.Text = "DB Viewer";
            toolTip1.SetToolTip(btn_OpenDB, "Opens the Metadata DB using a 3rd party tool (not M$ Access)");
            btn_OpenDB.UseVisualStyleBackColor = false;
            btn_OpenDB.Click += btn_DBViewer_Click;
            // 
            // btn_ProfilesSave
            // 
            btn_ProfilesSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ProfilesSave.Location = new System.Drawing.Point(490, 2);
            btn_ProfilesSave.Margin = new Padding(0);
            btn_ProfilesSave.Name = "btn_ProfilesSave";
            btn_ProfilesSave.Size = new System.Drawing.Size(22, 18);
            btn_ProfilesSave.TabIndex = 386;
            btn_ProfilesSave.Text = "+";
            toolTip1.SetToolTip(btn_ProfilesSave, "Add a new Profile.");
            btn_ProfilesSave.UseVisualStyleBackColor = true;
            btn_ProfilesSave.Click += btn_ProfilesSave_Click;
            // 
            // btn_ProfileRemove
            // 
            btn_ProfileRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ProfileRemove.Location = new System.Drawing.Point(514, 2);
            btn_ProfileRemove.Margin = new Padding(0);
            btn_ProfileRemove.Name = "btn_ProfileRemove";
            btn_ProfileRemove.Size = new System.Drawing.Size(22, 18);
            btn_ProfileRemove.TabIndex = 388;
            btn_ProfileRemove.Text = "-";
            toolTip1.SetToolTip(btn_ProfileRemove, "Remove Profile (If profile is left EMPTY then you can chose manually a Profile to be removed e.g. without 1st changin to it or if defective etc.)");
            btn_ProfileRemove.UseVisualStyleBackColor = true;
            btn_ProfileRemove.Click += btn_GroupsRemove_Click;
            // 
            // btn_OpenLogsFolder
            // 
            btn_OpenLogsFolder.BackColor = System.Drawing.SystemColors.Control;
            btn_OpenLogsFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_OpenLogsFolder.Location = new System.Drawing.Point(205, 182);
            btn_OpenLogsFolder.Margin = new Padding(0);
            btn_OpenLogsFolder.Name = "btn_OpenLogsFolder";
            btn_OpenLogsFolder.Size = new System.Drawing.Size(76, 26);
            btn_OpenLogsFolder.TabIndex = 389;
            btn_OpenLogsFolder.Text = "Logs Folder";
            toolTip1.SetToolTip(btn_OpenLogsFolder, "Opens Log Folder contaning Debug or Import, or Repack loggin info.");
            btn_OpenLogsFolder.UseVisualStyleBackColor = false;
            btn_OpenLogsFolder.Click += button1_Click_1;
            // 
            // btn_Enable_CDLC
            // 
            btn_Enable_CDLC.BackColor = System.Drawing.SystemColors.Control;
            btn_Enable_CDLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Enable_CDLC.Location = new System.Drawing.Point(282, 182);
            btn_Enable_CDLC.Margin = new Padding(0);
            btn_Enable_CDLC.Name = "btn_Enable_CDLC";
            btn_Enable_CDLC.Size = new System.Drawing.Size(87, 26);
            btn_Enable_CDLC.TabIndex = 390;
            btn_Enable_CDLC.Text = "Enable CDCL";
            toolTip1.SetToolTip(btn_Enable_CDLC, "Checks and Enable Rocksmith to show Custom Downloadabe extra-user-made Content.");
            btn_Enable_CDLC.UseVisualStyleBackColor = false;
            btn_Enable_CDLC.Click += btn_Enable_CDLC_Click;
            // 
            // btn_CalcNoOfImports
            // 
            btn_CalcNoOfImports.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_CalcNoOfImports.Location = new System.Drawing.Point(408, 64);
            btn_CalcNoOfImports.Margin = new Padding(0);
            btn_CalcNoOfImports.Name = "btn_CalcNoOfImports";
            btn_CalcNoOfImports.Size = new System.Drawing.Size(26, 18);
            btn_CalcNoOfImports.TabIndex = 392;
            btn_CalcNoOfImports.Text = "<>";
            toolTip1.SetToolTip(btn_CalcNoOfImports, "Refresh CDCL 2 Import.");
            btn_CalcNoOfImports.UseVisualStyleBackColor = true;
            btn_CalcNoOfImports.Click += btn_CalcNoOfImports_Click;
            // 
            // btn_CopyDefaultDBtoTemp
            // 
            btn_CopyDefaultDBtoTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_CopyDefaultDBtoTemp.Location = new System.Drawing.Point(304, 44);
            btn_CopyDefaultDBtoTemp.Margin = new Padding(0);
            btn_CopyDefaultDBtoTemp.Name = "btn_CopyDefaultDBtoTemp";
            btn_CopyDefaultDBtoTemp.Size = new System.Drawing.Size(18, 16);
            btn_CopyDefaultDBtoTemp.TabIndex = 402;
            btn_CopyDefaultDBtoTemp.Text = "<";
            toolTip1.SetToolTip(btn_CopyDefaultDBtoTemp, "Copies the Default DB to Temp folder");
            btn_CopyDefaultDBtoTemp.UseVisualStyleBackColor = true;
            btn_CopyDefaultDBtoTemp.Click += btn_CopyDefaultDBtoTemp_Click;
            // 
            // btn_Param
            // 
            btn_Param.BackColor = System.Drawing.SystemColors.Control;
            btn_Param.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_Param.Location = new System.Drawing.Point(372, 182);
            btn_Param.Margin = new Padding(0);
            btn_Param.Name = "btn_Param";
            btn_Param.Size = new System.Drawing.Size(50, 15);
            btn_Param.TabIndex = 403;
            btn_Param.Text = "Param";
            toolTip1.SetToolTip(btn_Param, "Open XML containing the Basic Options for DLCManager (Please note each profile most imp settings are maintained at DB level in the table Groups)");
            btn_Param.UseVisualStyleBackColor = false;
            btn_Param.Click += btn_Param_Click;
            // 
            // btn_Save
            // 
            btn_Save.BackColor = System.Drawing.SystemColors.Control;
            btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Save.ForeColor = System.Drawing.Color.Green;
            btn_Save.Location = new System.Drawing.Point(424, 182);
            btn_Save.Margin = new Padding(0);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new System.Drawing.Size(50, 26);
            btn_Save.TabIndex = 408;
            btn_Save.Text = "Save";
            toolTip1.SetToolTip(btn_Save, "Open XML containing the Basic Options for DLCManager (Please note each profile most imp settings are maintained at DB level in the table Groups)");
            btn_Save.UseVisualStyleBackColor = false;
            btn_Save.Click += Btn_Save_Click;
            // 
            // btn_Preview_Lyric_Info
            // 
            btn_Preview_Lyric_Info.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Lyric_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Lyric_Info.Location = new System.Drawing.Point(522, 355);
            btn_Preview_Lyric_Info.Margin = new Padding(2);
            btn_Preview_Lyric_Info.Name = "btn_Preview_Lyric_Info";
            btn_Preview_Lyric_Info.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Lyric_Info.TabIndex = 412;
            toolTip1.SetToolTip(btn_Preview_Lyric_Info, "Preview File Name Mask");
            btn_Preview_Lyric_Info.UseVisualStyleBackColor = false;
            btn_Preview_Lyric_Info.Click += Btn_Lyric_Info_Click;
            // 
            // btn_Preview_Album_Sort
            // 
            btn_Preview_Album_Sort.BackColor = System.Drawing.SystemColors.Control;
            btn_Preview_Album_Sort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_Preview_Album_Sort.Location = new System.Drawing.Point(522, 309);
            btn_Preview_Album_Sort.Margin = new Padding(2);
            btn_Preview_Album_Sort.Name = "btn_Preview_Album_Sort";
            btn_Preview_Album_Sort.Size = new System.Drawing.Size(14, 14);
            btn_Preview_Album_Sort.TabIndex = 417;
            toolTip1.SetToolTip(btn_Preview_Album_Sort, "Preview Album Mask");
            btn_Preview_Album_Sort.UseVisualStyleBackColor = false;
            btn_Preview_Album_Sort.Click += Btn_Album_Sort_Click;
            // 
            // btn_Album2SortA
            // 
            btn_Album2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_Album2SortA.Location = new System.Drawing.Point(304, 24);
            btn_Album2SortA.Margin = new Padding(2);
            btn_Album2SortA.Name = "btn_Album2SortA";
            btn_Album2SortA.Size = new System.Drawing.Size(18, 16);
            btn_Album2SortA.TabIndex = 429;
            btn_Album2SortA.Text = ">";
            toolTip1.SetToolTip(btn_Album2SortA, "Copy path DB folder");
            btn_Album2SortA.UseVisualStyleBackColor = true;
            btn_Album2SortA.Click += btn_Album2SortA_Click;
            // 
            // btn_FilterParams
            // 
            btn_FilterParams.Location = new System.Drawing.Point(476, 434);
            btn_FilterParams.Margin = new Padding(2);
            btn_FilterParams.Name = "btn_FilterParams";
            btn_FilterParams.Size = new System.Drawing.Size(60, 48);
            btn_FilterParams.TabIndex = 430;
            btn_FilterParams.Text = "Filter Params";
            toolTip1.SetToolTip(btn_FilterParams, "Screen to manage the Game Default Screens for RS1Retail, RS1DLC, or RS2014 Retail packs, Crossplatform songs.");
            btn_FilterParams.UseVisualStyleBackColor = true;
            btn_FilterParams.Click += btn_FilterParams_Click;
            // 
            // btn_Retail
            // 
            btn_Retail.BackColor = System.Drawing.SystemColors.Control;
            btn_Retail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            btn_Retail.Location = new System.Drawing.Point(477, 86);
            btn_Retail.Margin = new Padding(2);
            btn_Retail.Name = "btn_Retail";
            btn_Retail.Size = new System.Drawing.Size(45, 62);
            btn_Retail.TabIndex = 432;
            btn_Retail.Text = "Open RetailDB";
            toolTip1.SetToolTip(btn_Retail, "Here you can decide what Standardizations you want to apply to Artist Names, Album Names, Covers or Short Names");
            btn_Retail.UseVisualStyleBackColor = false;
            btn_Retail.Click += btn_Retail_Click;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.Control;
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            button1.Location = new System.Drawing.Point(372, 195);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(50, 15);
            button1.TabIndex = 433;
            button1.Text = "Run";
            toolTip1.SetToolTip(button1, "Starts Rocksmith (for windows assmingly)");
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_2;
            // 
            // btn_ApplyMultiFilters
            // 
            btn_ApplyMultiFilters.Font = new System.Drawing.Font("Segoe UI", 7F);
            btn_ApplyMultiFilters.Location = new System.Drawing.Point(476, 485);
            btn_ApplyMultiFilters.Margin = new Padding(2);
            btn_ApplyMultiFilters.Name = "btn_ApplyMultiFilters";
            btn_ApplyMultiFilters.Size = new System.Drawing.Size(61, 40);
            btn_ApplyMultiFilters.TabIndex = 435;
            btn_ApplyMultiFilters.Text = "Apply (0) Filters";
            toolTip1.SetToolTip(btn_ApplyMultiFilters, "Screen to manage the Game Default Screens for RS1Retail, RS1DLC, or RS2014 Retail packs, Crossplatform songs.");
            btn_ApplyMultiFilters.UseVisualStyleBackColor = true;
            btn_ApplyMultiFilters.Click += btn_ApplyMultiFilters_Click;
            // 
            // btn_DistribNotes
            // 
            btn_DistribNotes.BackColor = System.Drawing.SystemColors.Control;
            btn_DistribNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_DistribNotes.Location = new System.Drawing.Point(387, 65);
            btn_DistribNotes.Margin = new Padding(2);
            btn_DistribNotes.Name = "btn_DistribNotes";
            btn_DistribNotes.Size = new System.Drawing.Size(17, 84);
            btn_DistribNotes.TabIndex = 436;
            btn_DistribNotes.Text = "DistribNotes";
            toolTip1.SetToolTip(btn_DistribNotes, "Distribute notes of the XML Track based on real timings, start and end. Reimport after in EoF!");
            btn_DistribNotes.UseVisualStyleBackColor = false;
            btn_DistribNotes.Click += btn_DistribNotes_Click_1;
            // 
            // chbx_Configurations
            // 
            chbx_Configurations.FormattingEnabled = true;
            chbx_Configurations.Items.AddRange(new object[] { "DevHome", "AltDevHome", "Mac", "PC", "DevVirtualMachine" });
            chbx_Configurations.Location = new System.Drawing.Point(384, 2);
            chbx_Configurations.Margin = new Padding(2);
            chbx_Configurations.Name = "chbx_Configurations";
            chbx_Configurations.Size = new System.Drawing.Size(104, 23);
            chbx_Configurations.TabIndex = 325;
            chbx_Configurations.Text = "Select Profile";
            chbx_Configurations.SelectedIndexChanged += Chbx_Configurations_SelectedIndexChanged;
            // 
            // lbl_Settings
            // 
            lbl_Settings.AutoSize = true;
            lbl_Settings.Font = new System.Drawing.Font("Calibri", 9F);
            lbl_Settings.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Settings.Location = new System.Drawing.Point(327, 392);
            lbl_Settings.Margin = new Padding(2, 0, 2, 0);
            lbl_Settings.Name = "lbl_Settings";
            lbl_Settings.Size = new System.Drawing.Size(112, 14);
            lbl_Settings.TabIndex = 406;
            lbl_Settings.Text = ":Settings  {ParamID}";
            // 
            // cbx_Activ_Lyric_Info
            // 
            cbx_Activ_Lyric_Info.AutoSize = true;
            cbx_Activ_Lyric_Info.Checked = true;
            cbx_Activ_Lyric_Info.CheckState = CheckState.Checked;
            cbx_Activ_Lyric_Info.Location = new System.Drawing.Point(506, 355);
            cbx_Activ_Lyric_Info.Margin = new Padding(2);
            cbx_Activ_Lyric_Info.Name = "cbx_Activ_Lyric_Info";
            cbx_Activ_Lyric_Info.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Lyric_Info.TabIndex = 411;
            cbx_Activ_Lyric_Info.UseVisualStyleBackColor = true;
            cbx_Activ_Lyric_Info.CheckedChanged += Cbx_Activ_Lyric_Info_CheckedChanged;
            // 
            // lbl_LyricInfo
            // 
            lbl_LyricInfo.AutoSize = true;
            lbl_LyricInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_LyricInfo.Location = new System.Drawing.Point(6, 351);
            lbl_LyricInfo.Margin = new Padding(2, 0, 2, 0);
            lbl_LyricInfo.Name = "lbl_LyricInfo";
            lbl_LyricInfo.Size = new System.Drawing.Size(55, 15);
            lbl_LyricInfo.TabIndex = 413;
            lbl_LyricInfo.Text = "Lyric Info";
            // 
            // cbx_Lyric_Info
            // 
            cbx_Lyric_Info.DropDownWidth = 280;
            cbx_Lyric_Info.FormattingEnabled = true;
            cbx_Lyric_Info.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Lyric_Info.Location = new System.Drawing.Point(398, 351);
            cbx_Lyric_Info.Margin = new Padding(2);
            cbx_Lyric_Info.Name = "cbx_Lyric_Info";
            cbx_Lyric_Info.Size = new System.Drawing.Size(106, 23);
            cbx_Lyric_Info.TabIndex = 410;
            cbx_Lyric_Info.SelectedIndexChanged += cbx_Lyric_Info_SelectedIndexChanged;
            // 
            // txt_Lyric_Info
            // 
            txt_Lyric_Info.Location = new System.Drawing.Point(68, 351);
            txt_Lyric_Info.Margin = new Padding(2);
            txt_Lyric_Info.Name = "txt_Lyric_Info";
            txt_Lyric_Info.Size = new System.Drawing.Size(328, 23);
            txt_Lyric_Info.TabIndex = 409;
            txt_Lyric_Info.Text = "<Instr>";
            // 
            // cbx_Activ_Album_Sort
            // 
            cbx_Activ_Album_Sort.AutoSize = true;
            cbx_Activ_Album_Sort.Checked = true;
            cbx_Activ_Album_Sort.CheckState = CheckState.Checked;
            cbx_Activ_Album_Sort.Location = new System.Drawing.Point(506, 313);
            cbx_Activ_Album_Sort.Margin = new Padding(2);
            cbx_Activ_Album_Sort.Name = "cbx_Activ_Album_Sort";
            cbx_Activ_Album_Sort.Size = new System.Drawing.Size(15, 14);
            cbx_Activ_Album_Sort.TabIndex = 416;
            cbx_Activ_Album_Sort.UseVisualStyleBackColor = true;
            cbx_Activ_Album_Sort.CheckedChanged += Cbx_Activ_Album_Sort_CheckedChanged;
            // 
            // lbl_AlbumSort
            // 
            lbl_AlbumSort.AutoSize = true;
            lbl_AlbumSort.Font = new System.Drawing.Font("Calibri", 7.875F);
            lbl_AlbumSort.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_AlbumSort.Location = new System.Drawing.Point(6, 313);
            lbl_AlbumSort.Margin = new Padding(2, 0, 2, 0);
            lbl_AlbumSort.Name = "lbl_AlbumSort";
            lbl_AlbumSort.Size = new System.Drawing.Size(60, 13);
            lbl_AlbumSort.TabIndex = 418;
            lbl_AlbumSort.Text = "Album Sort ";
            // 
            // cbx_Album_Sort
            // 
            cbx_Album_Sort.DropDownWidth = 280;
            cbx_Album_Sort.FormattingEnabled = true;
            cbx_Album_Sort.Items.AddRange(new object[] { "<Artist>", "<Title>", "<Version>", "<DLCName>", "<CDLC>", "<Album>", "<Track No.>", "<Year>", "<Rating>", "<Alternate Version>", "<Duplicate>", "<Has Official>", "<Description>", "<Live Details>", "<Attributes extended>", "<Avail. Instr.>", "<Tuning>", "<Instr. Rating.>", "<Multi Track Details>", "<Group>", "<Groups>", "<GroupIndex>", "<GroupIndexAndName>", "<FirstGroupIndexAndName>", "<BetaOrGroupIndex>", "<Beta>", "<DD>", "<Broken>", "<File Name>", "<Bonus>", "<Artist Short>", "<Album Short>", "<Title Sort>", "<Artist Sort>", "<Album Sort>", "<Author>", "<Quality Assurance Attributes>", "<lastConversionDateTime>", "<Avail. Tracks>", "<Avail. Tracks w Bonus>", "<Avail. Tracks w Favorite>", "<Avail. Tracks w Bonus&Alternates&Favorites>", "<Avail. Tracks and Timings>", "<Avail. Tracks and ShortTimings>", "<Avail. Tracks and ShortTimings&Bonus>", "<Avail. Tracks and ShortTimings&Bonus&Favorite>", "<Bass Has DynamicDificulty>", "<Timestamp>", "<TimestampShort>", "<Live>", "<Acoustic>", "<Instrumental>", "<EP>", "<Uncensored>", "<SoundTrack>", "<Single>", "<Track Tile (potentially) removed details>", "<LyricsLanguage>", "<IntheWorks>", "<IntheWorksWDetails>", "<Karaoke>", "<Cover>", "<Demo>", "<Remix>", "<FullAlbum>", "<Remastered>", "<Manipulated>", "<Medley>", "<Multistrings>", "<Deluxe>", "<GreatestHits>", "<Midi>", "<GameSoundtrack>", "<TVTheme>", "<AmateurCover>", "<MetalCover>", "<Ukulele>", "<Official>", "<Volume>", "<Preview Volume>", "<Import Date>", "<Bitrate>", "<ToDos>", "<CDLC_ID>", "<DLCM Release>", "<DLCM ReleaseName>", "<DLCM ReleaseVersion>", "<Date>", "<DigitechDropFlag>", "<DigitechDropDetails>", "<Capo>", "<CapoFret>", "<Found on CF>", "<Uploaded on CF>" });
            cbx_Album_Sort.Location = new System.Drawing.Point(398, 309);
            cbx_Album_Sort.Margin = new Padding(2);
            cbx_Album_Sort.Name = "cbx_Album_Sort";
            cbx_Album_Sort.Size = new System.Drawing.Size(106, 23);
            cbx_Album_Sort.TabIndex = 415;
            // 
            // txt_Album_Sort
            // 
            txt_Album_Sort.Location = new System.Drawing.Point(68, 309);
            txt_Album_Sort.Margin = new Padding(2);
            txt_Album_Sort.Name = "txt_Album_Sort";
            txt_Album_Sort.Size = new System.Drawing.Size(328, 23);
            txt_Album_Sort.TabIndex = 414;
            txt_Album_Sort.Text = "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>";
            // 
            // txt_FilterParams
            // 
            txt_FilterParams.Location = new System.Drawing.Point(476, 388);
            txt_FilterParams.Margin = new Padding(2);
            txt_FilterParams.Multiline = false;
            txt_FilterParams.Name = "txt_FilterParams";
            txt_FilterParams.ScrollBars = RichTextBoxScrollBars.None;
            txt_FilterParams.ShortcutsEnabled = false;
            txt_FilterParams.Size = new System.Drawing.Size(60, 42);
            txt_FilterParams.TabIndex = 431;
            txt_FilterParams.Text = "";
            txt_FilterParams.KeyPress += txt_FilterParams_KeyPress;
            // 
            // lbl_Log
            // 
            lbl_Log.AutoSize = true;
            lbl_Log.ForeColor = System.Drawing.SystemColors.ControlText;
            lbl_Log.Location = new System.Drawing.Point(460, 540);
            lbl_Log.Margin = new Padding(2, 0, 2, 0);
            lbl_Log.Name = "lbl_Log";
            lbl_Log.Size = new System.Drawing.Size(54, 15);
            lbl_Log.TabIndex = 407;
            lbl_Log.Text = ":Live Log";
            // 
            // lbGroups
            // 
            lbGroups.Font = new System.Drawing.Font("Segoe UI", 7F);
            lbGroups.FormattingEnabled = true;
            lbGroups.ItemHeight = 12;
            lbGroups.Items.AddRange(new object[] { "test1", "test2", "test 3", "test4" });
            lbGroups.Location = new System.Drawing.Point(6, 485);
            lbGroups.Name = "lbGroups";
            lbGroups.SelectionMode = SelectionMode.MultiExtended;
            lbGroups.Size = new System.Drawing.Size(468, 40);
            lbGroups.TabIndex = 434;
            lbGroups.SelectedIndexChanged += lbGroups_SelectedIndexChanged;
            // 
            // DLCManager
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(btn_DistribNotes);
            Controls.Add(btn_ApplyMultiFilters);
            Controls.Add(lbGroups);
            Controls.Add(button1);
            Controls.Add(btn_Retail);
            Controls.Add(txt_FilterParams);
            Controls.Add(btn_FilterParams);
            Controls.Add(btn_Album2SortA);
            Controls.Add(btn_Preview_Album_Sort);
            Controls.Add(cbx_Activ_Album_Sort);
            Controls.Add(lbl_AlbumSort);
            Controls.Add(cbx_Album_Sort);
            Controls.Add(txt_Album_Sort);
            Controls.Add(btn_Preview_Lyric_Info);
            Controls.Add(cbx_Activ_Lyric_Info);
            Controls.Add(lbl_LyricInfo);
            Controls.Add(cbx_Lyric_Info);
            Controls.Add(txt_Lyric_Info);
            Controls.Add(btn_Save);
            Controls.Add(lbl_Log);
            Controls.Add(lbl_Settings);
            Controls.Add(pB_ReadDLCs);
            Controls.Add(btn_Param);
            Controls.Add(btn_CopyDefaultDBtoTemp);
            Controls.Add(chbx_DefaultDB);
            Controls.Add(btn_CalcNoOfImports);
            Controls.Add(btn_Enable_CDLC);
            Controls.Add(btn_OpenLogsFolder);
            Controls.Add(btn_ProfileRemove);
            Controls.Add(btn_ProfilesSave);
            Controls.Add(chbx_Configurations);
            Controls.Add(panel2);
            Controls.Add(btn_OpenDB);
            Controls.Add(btn_GoDB);
            Controls.Add(btn_GoTemp);
            Controls.Add(btn_GoImport);
            Controls.Add(btn_LoadRetailSongs);
            Controls.Add(btn_ApplyStandardization);
            Controls.Add(btn_Standardization);
            Controls.Add(cbx_Export);
            Controls.Add(Export_To);
            Controls.Add(chbx_Additional_Manipulations);
            Controls.Add(lbl_Mask);
            Controls.Add(btn_Preview_Artist_Sort);
            Controls.Add(cbx_Activ_Artist_Sort);
            Controls.Add(lbl_Artist_Sort);
            Controls.Add(cbx_Artist_Sort);
            Controls.Add(txt_Artist_Sort);
            Controls.Add(btn_Preview_File_Name);
            Controls.Add(btn_Preview_Album);
            Controls.Add(btn_Preview_Artist);
            Controls.Add(btn_Preview_Title_Sort);
            Controls.Add(btn_Preview_Title);
            Controls.Add(cbx_Activ_File_Name);
            Controls.Add(cbx_Activ_Album);
            Controls.Add(cbx_Activ_Artist);
            Controls.Add(cbx_Activ_Title_Sort);
            Controls.Add(cbx_Activ_Title);
            Controls.Add(lbl_Artist);
            Controls.Add(cbx_Artist);
            Controls.Add(txt_Artist);
            Controls.Add(cbx_Title);
            Controls.Add(txt_Title);
            Controls.Add(lbl_File_Name);
            Controls.Add(lbl_Album);
            Controls.Add(lbl_Title_Sort);
            Controls.Add(cbx_File_Name);
            Controls.Add(txt_File_Name);
            Controls.Add(cbx_Album);
            Controls.Add(txt_Album);
            Controls.Add(cbx_Title_Sort);
            Controls.Add(txt_Title_Sort);
            Controls.Add(lbl_Title);
            Controls.Add(chbx_DebugB);
            Controls.Add(txt_DBFolder);
            Controls.Add(btn_DBFolder);
            Controls.Add(label1);
            Controls.Add(btn_Close);
            Controls.Add(txt_RocksmithDLCPath);
            Controls.Add(txt_TempPath);
            Controls.Add(chbx_CleanTemp);
            Controls.Add(btn_SteamDLCFolder);
            Controls.Add(rtxt_StatisticsOnReadDLCs);
            Controls.Add(btn_PopulateDB);
            Controls.Add(btn_OpenMainDB);
            Controls.Add(lbl_RocksmithDLCPath);
            Controls.Add(btn_TempPath);
            Controls.Add(lbl_TempFolders);
            Controls.Add(lbl_PreviewText);
            Margin = new Padding(2);
            MaximumSize = new System.Drawing.Size(614, 764);
            MinimumSize = new System.Drawing.Size(614, 764);
            Name = "DLCManager";
            Size = new System.Drawing.Size(614, 764);
            Load += DLCManager_Load;
            Enter += DLCManager_Enter;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txt_NoOfSplits).EndInit();
            ((System.ComponentModel.ISupportInitialize)mainBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbx_Export;
        private Panel panel2;
        private BindingSource mainBindingSource;
        private Button Export_To;
        private CheckBox chbx_Rebuild;
        private Button btn_Cleanup_MainDB;
        private CheckedListBox chbx_Additional_Manipulations;
        private Label lbl_Mask;
        private Button btn_Preview_Artist_Sort;
        private CheckBox cbx_Activ_Artist_Sort;
        private Label lbl_Artist_Sort;
        private ComboBox cbx_Artist_Sort;
        private TextBox txt_Artist_Sort;
        private Button btn_Preview_File_Name;
        private Button btn_Preview_Album;
        private Button btn_Preview_Artist;
        private Button btn_Preview_Title_Sort;
        private Button btn_Preview_Title;
        private CheckBox cbx_Activ_File_Name;
        private CheckBox cbx_Activ_Album;
        private CheckBox cbx_Activ_Artist;
        private CheckBox cbx_Activ_Title_Sort;
        private CheckBox cbx_Activ_Title;
        private Label lbl_Artist;
        private ComboBox cbx_Artist;
        private TextBox txt_Artist;
        private ComboBox cbx_Title;
        private TextBox txt_Title;
        private Label lbl_File_Name;
        private Label lbl_Album;
        private Label lbl_Title_Sort;
        private ComboBox cbx_File_Name;
        private TextBox txt_File_Name;
        private ComboBox cbx_Album;
        private TextBox txt_Album;
        private ComboBox cbx_Title_Sort;
        private TextBox txt_Title_Sort;
        private Label lbl_Title;
        private CheckBox chbx_DebugB;
        private TextBox txt_DBFolder;
        private Button btn_DBFolder;
        private Label label1;
        private Button btn_Close;
        private TextBox txt_RocksmithDLCPath;
        private TextBox txt_TempPath;
        private CheckBox chbx_CleanTemp;
        private Button btn_SteamDLCFolder;
        public RichTextBox rtxt_StatisticsOnReadDLCs;
        private Button btn_PopulateDB;
        private Button btn_OpenMainDB;
        private Label lbl_RocksmithDLCPath;
        private Button btn_TempPath;
        private Label lbl_TempFolders;
        private Label lbl_PreviewText;
        private Button btn_RePack;
        private CheckBox chbx_XBOX360;
        private CheckBox chbx_Mac;
        private CheckBox chbx_PS3;
        private CheckBox chbx_PC;
        private ToolTip toolTip1;
        private HelpProvider helpProvider1;
        private Button btn_Standardization;
        private Button btn_ApplyStandardization;
        private CheckBox chbx_DefaultDB;
        private Button btn_LoadRetailSongs;
        private Button btn_GoImport;
        private Button btn_GoTemp;
        private Button btn_GoDB;
        private Button btn_OpenDB;
        private ComboBox chbx_Configurations;
        private Button btn_ProfilesSave;
        private Button btn_ProfileRemove;
        private Label lbl_NoRec2;
        private Button btn_OpenLogsFolder;
        private Button btn_GoRepack;
        private Button btn_Enable_CDLC;
        private Button btn_CalcNoOfImports;
        private Button btn_CopyDefaultDBtoTemp;
        private Button btn_Param;
        private Button btn_Add2Retail;
        public ProgressBar pB_ReadDLCs;
        private Label lbl_Settings;
        private Button btn_Save;
        private Button btn_Preview_Lyric_Info;
        private CheckBox cbx_Activ_Lyric_Info;
        private Label lbl_LyricInfo;
        private ComboBox cbx_Lyric_Info;
        private TextBox txt_Lyric_Info;
        private Button btn_Preview_Album_Sort;
        private CheckBox cbx_Activ_Album_Sort;
        private Label lbl_AlbumSort;
        private ComboBox cbx_Album_Sort;
        private TextBox txt_Album_Sort;
        private CheckBox chbx_iOS;
        private CheckBox chbx_PS4;
        private Button btn_Album2SortA;
        private Button btn_FilterParams;
        private Label lbl_Access;
        private Button btn_Retail;
        private RichTextBox txt_FilterParams;
        private ComboBox cbx_Groups;
        private Label label2;
        private NumericUpDown txt_NoOfSplits;
        private CheckBox rbtn_Population_PackNO;
        private Button button1;
        private Label lbl_Log;
        private CheckBox chbx_FilterCompound;
        private Button btn_Debug;
        private ListBox lbGroups;
        private Button btn_ApplyMultiFilters;
        private Button button2;
        private Button btn_DistribNotes;
        //public static ProgressBar pB_ReadDLCs;
    }
}
