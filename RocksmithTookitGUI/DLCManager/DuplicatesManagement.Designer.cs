using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RocksmithToolkitLib;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.DLCPackage;
using Ookii.Dialogs; //cue text

namespace RocksmithToolkitGUI.DLCManager
{
    partial class frm_Duplicates_Management
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
            components = new System.ComponentModel.Container();
            label16 = new Label();
            txt_DescriptionExisting = new RichTextBox();
            txt_DescriptionNew = new RichTextBox();
            lbl_AvailTrk = new Label();
            lbl_AudioPreview = new Label();
            lbl_AudioMain = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            lbl_AlbumArt = new Label();
            lbl_Title = new Label();
            picbx_AlbumArtPathNew = new PictureBox();
            picbx_AlbumArtPathExisting = new PictureBox();
            btn_Update = new Button();
            chbx_IsAlternateNew = new CheckBox();
            btn_Alternate = new Button();
            btn_Ignore = new Button();
            lbl_TitleSort = new Label();
            lbl_ArtistSort = new Label();
            lbl_FileName = new Label();
            lbl_IsOriginal = new Label();
            lbl_Toolkit = new Label();
            lbl_Author = new Label();
            lbl_Version = new Label();
            lbl_Tuning = new Label();
            lbl_DLCID = new Label();
            lbl_DD = new Label();
            lbl_AvailableTracks = new Label();
            lbl_Audio = new Label();
            lbl_Preview = new Label();
            lbl_XMLLead = new Label();
            lbl_XMLBass = new Label();
            lbl_XMLCombo = new Label();
            lbl_XMLRhythm = new Label();
            lbl_JSONLead = new Label();
            lbl_JSONBass = new Label();
            lbl_JSONCombo = new Label();
            lbl_JSONRhythm = new Label();
            lbl_Reference = new Label();
            label1 = new Label();
            label2 = new Label();
            chbx_IsOriginal = new CheckBox();
            label17 = new Label();
            lbl_NewIs_Original = new Label();
            label19 = new Label();
            label20 = new Label();
            label21 = new Label();
            lbl_Platfdormtoolkit = new Label();
            label23 = new Label();
            btn_UpdateExisting = new Button();
            lblNew = new Label();
            lblExisting = new Label();
            lbl_IDExisting = new Label();
            label26 = new Label();
            btn_OpenMainDB = new Button();
            label27 = new Label();
            label28 = new Label();
            label29 = new Label();
            label30 = new Label();
            label31 = new Label();
            lbl_diffCount = new Label();
            chbx_IgnoreDupli = new CheckBox();
            btn_RemoveOldNew = new Button();
            lbl_Vocals = new Label();
            lbl_txt_Vocals = new Label();
            btn_TitleNew = new Button();
            btn_TitleExisting = new Button();
            btn_TitleSortNew = new Button();
            btn_TitleSortExisting = new Button();
            btn_AuthorNew = new Button();
            btn_AuthorExisting = new Button();
            lbl_Album = new Label();
            lbl_Artist = new Label();
            btn_AlbumNew = new Button();
            btn_AlbumExisting = new Button();
            btn_ArtistNew = new Button();
            btn_ArtistExisting = new Button();
            label4 = new Label();
            label18 = new Label();
            btn_ArtistSortNew = new Button();
            btn_ArtistSortExisting = new Button();
            groupBox2 = new GroupBox();
            btn_UseDates = new CheckBox();
            btn_TN_Rhythm = new Button();
            btn_TN_Combo = new Button();
            btn_TN_Bass = new Button();
            btn_TN_Lead = new Button();
            btn_WM_Rhythm = new Button();
            btn_WM_Combo = new Button();
            btn_WM_Bass = new Button();
            btn_WM_Leads = new Button();
            lbl_DateExisting = new Label();
            lbl_DateNew = new Label();
            lbl_tonediff = new Label();
            btn_GoToNew = new Button();
            btn_GoToExisting = new Button();
            btn_AddAge = new Button();
            lbl_Existing = new Label();
            txt_JSONLeadExisting = new CueTextBox();
            lbl_New = new Label();
            txt_XMLLeadNew = new CueTextBox();
            txt_XMLLeadExisting = new CueTextBox();
            txt_XMLBassNew = new CueTextBox();
            txt_XMLBassExisting = new CueTextBox();
            txt_XMLComboNew = new CueTextBox();
            txt_XMLComboExisting = new CueTextBox();
            txt_XMLRhythmNew = new CueTextBox();
            txt_XMLRhythmExisting = new CueTextBox();
            txt_JSONLeadNew = new CueTextBox();
            txt_JSONBassNew = new CueTextBox();
            txt_JSONBassExisting = new CueTextBox();
            txt_JSONComboNew = new CueTextBox();
            txt_JSONComboExisting = new CueTextBox();
            txt_JSONRhythmNew = new CueTextBox();
            txt_JSONRhythmExisting = new CueTextBox();
            chbx_IsAlternateExisting = new CheckBox();
            txt_AlternateNoExisting = new NumericUpDown();
            txt_AlternateNoNew = new NumericUpDown();
            chbx_MultiTrackExisting = new CheckBox();
            chbx_MultiTrackNew = new CheckBox();
            txt_MultiTrackNew = new ComboBox();
            txt_MultiTrackExisting = new ComboBox();
            btn_CoverNew = new Button();
            btn_CoverExisting = new Button();
            groupBox3 = new GroupBox();
            txt_VocalsExisting = new CueTextBox();
            txt_PreviewExisting = new CueTextBox();
            txt_AudioExisting = new CueTextBox();
            txt_AvailTracksExisting = new CueTextBox();
            txt_DDExisting = new CueTextBox();
            txt_VocalsNew = new CueTextBox();
            txt_PreviewNew = new CueTextBox();
            txt_AudioNew = new CueTextBox();
            txt_AvailTracksNew = new CueTextBox();
            txt_DDNew = new CueTextBox();
            btn_GoImport = new Button();
            chbx_CapoExisting = new CheckBox();
            chbx_MultiStringsNew = new CheckBox();
            chbx_CapoNew = new CheckBox();
            btn_WM_Vocals = new Button();
            chbx_MultiStringsExisting = new CheckBox();
            lbl_previewFootnote = new Label();
            btn_PlayPreviewNew = new Button();
            btn_PlayAudioNew = new Button();
            btn_PlayPreviewExisting = new Button();
            btn_PlayAudioExisting = new Button();
            btn_AddDD = new Button();
            btn_AddTracks = new Button();
            txt_DDNew_1 = new CueTextBox();
            txt_DDExisting_1 = new CueTextBox();
            txt_AvailTracksNew_1 = new CueTextBox();
            txt_AvailTracksExisting_1 = new CueTextBox();
            txt_AudioNew_1 = new CueTextBox();
            txt_AudioExisting_1 = new CueTextBox();
            txt_PreviewNew_1 = new CueTextBox();
            txt_PreviewExisting_1 = new CueTextBox();
            txt_VocalsNew_1 = new CueTextBox();
            txt_VocalsExisting_1 = new CueTextBox();
            groupBox4 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            lbl_CustomsForge_ReleaseNotes = new Label();
            txt_CustomsForge_ReleaseNotesNew = new CueTextBox();
            txt_CustomsForge_ReleaseNotesExisting = new CueTextBox();
            label59 = new Label();
            txt_YouTube_LinkNew = new CueTextBox();
            txt_CustomsForge_LinkExisting = new CueTextBox();
            txt_YouTube_LinkExisting = new CueTextBox();
            lbl_CustomsForge_Like = new Label();
            txt_CustomsForge_LinkNew = new CueTextBox();
            lbl_CustomsForge_LinkNew = new Label();
            lbl_YouTube_LinkNew = new Label();
            txt_CustomsForge_LikeNew = new CueTextBox();
            lbfl_YouTube_Link = new Label();
            txt_CustomsForge_LikeExisting = new CueTextBox();
            label33 = new Label();
            label32 = new Label();
            btn_AddTunning = new Button();
            btn_AddVersion1 = new Button();
            btn_AddAuthor = new Button();
            lblSoye = new Label();
            lbl_Size = new Label();
            lbl_Multitrack = new Label();
            chbx_UseBrakets = new CheckBox();
            btn_AddAlternate = new Button();
            btn_StopImport = new Button();
            chbx_DeleteTemp = new CheckBox();
            btn_Title2SortT = new Button();
            btn_Artist2SortA = new Button();
            chbx_Autosave = new CheckBox();
            chbx_Sort = new CheckBox();
            chbx_LiveExisting = new CheckBox();
            chbx_LiveNew = new CheckBox();
            btn_AddPlatform = new Button();
            chbx_AcousticExisting = new CheckBox();
            chbx_AcousticNew = new CheckBox();
            btn_NotADuplicate = new Button();
            btn_AddStandard = new Button();
            toolTip1 = new ToolTip(components);
            btn_AlbumSortNew = new Button();
            btn_AlbumSortExisting = new Button();
            btn_Album2SortA = new Button();
            btn_CommentSimilar = new Button();
            lbl_FileHash = new Label();
            btn_OpenStandardization = new Button();
            chbx_DescriptionSave = new CheckBox();
            lbl_Covers = new Label();
            lbl_Attention = new Label();
            chbx_MedleyExisting = new CheckBox();
            chbx_DeluxeNew = new CheckBox();
            chbx_GreatestHitsNew = new CheckBox();
            chbx_DeluxeExisting = new CheckBox();
            chbx_GreatestHitsExisting = new CheckBox();
            btn_Replace_Brakets = new Button();
            chbx_GameSoundtrackNew = new CheckBox();
            btn_OpenNewFolder = new Button();
            chbx_MetalExisting = new CheckBox();
            btn_SaveExisting = new Button();
            lbl_LenghtExisting = new Label();
            btn_ShowInfoOthers = new Button();
            label13 = new Label();
            lbl_AlbumSort = new Label();
            txt_AlbumSortExisting = new CueTextBox();
            txt_AlbumSortNew = new CueTextBox();
            txt_YearNew = new CueTextBox();
            txt_YearExisting = new CueTextBox();
            txt_LenghtNew = new CueTextBox();
            txt_LenghtExisting = new CueTextBox();
            txt_VersionExisting = new CueTextBox();
            txt_VersionNew = new CueTextBox();
            txt_LiveDetailsNew = new CueTextBox();
            txt_LiveDetailsExisting = new CueTextBox();
            txt_PlatformNew = new CueTextBox();
            txt_PlatformExisting = new CueTextBox();
            txt_FileDateNew = new CueTextBox();
            txt_FileDateExisting = new CueTextBox();
            txt_SizeExisting = new CueTextBox();
            txt_SizeNew = new CueTextBox();
            txt_AlbumExisting = new CueTextBox();
            txt_ArtistExisting = new CueTextBox();
            txt_AlbumNew = new CueTextBox();
            txt_FileNameExisting = new CueTextBox();
            txt_FileNameNew = new CueTextBox();
            txt_ArtistNew = new CueTextBox();
            txt_DLCIDExisting = new CueTextBox();
            txt_DLCIDNew = new CueTextBox();
            txt_TuningExisting = new CueTextBox();
            txt_TuningNew = new CueTextBox();
            txt_AuthorExisting = new CueTextBox();
            txt_AuthorNew = new CueTextBox();
            txt_IsOriginalExisting = new CueTextBox();
            txt_IsOriginalNew = new CueTextBox();
            txt_ToolkitExisting = new CueTextBox();
            txt_ToolkitNew = new CueTextBox();
            txt_TitleSortExisting = new CueTextBox();
            txt_TitleSortNew = new CueTextBox();
            txt_ArtistSortExisting = new CueTextBox();
            txt_ArtistSortNew = new CueTextBox();
            txt_TitleExisting = new CueTextBox();
            txt_TitleNew = new CueTextBox();
            lbl_YearExisting = new Label();
            lbl_YearNew = new Label();
            lbl_IDNew = new Label();
            lbl_ExistingIs_Original = new Label();
            chbx_SingleNew = new CheckBox();
            chbx_EPNew = new CheckBox();
            chbx_SoundtrackNew = new CheckBox();
            chbx_InstrumentalNew = new CheckBox();
            chbx_UncensoredNew = new CheckBox();
            chbx_FullAlbumNew = new CheckBox();
            chbx_RemasteredNew = new CheckBox();
            chbx_FullAlbumExisting = new CheckBox();
            chbx_InstrumentalExisting = new CheckBox();
            chbx_SoundtrackExisting = new CheckBox();
            chbx_SingleExisting = new CheckBox();
            chbx_UncensoredExisting = new CheckBox();
            chbx_EPExisting = new CheckBox();
            chbx_RemasteredExisting = new CheckBox();
            lbl_P1 = new Label();
            lbl_P2 = new Label();
            lbl_P3 = new Label();
            lbl_P4 = new Label();
            chbx_InTheWorksNew = new CheckBox();
            chbx_InTheWorksExisting = new CheckBox();
            chbx_DemoExisting = new CheckBox();
            chbx_DemoNew = new CheckBox();
            chbx_RemixExisting = new CheckBox();
            chbx_RemixNew = new CheckBox();
            chbx_KaraokeNew = new CheckBox();
            chbx_KaraokeExisting = new CheckBox();
            chbx_FeaturingNew = new CheckBox();
            chbx_FeaturingExisting = new CheckBox();
            chbx_CoverNew = new CheckBox();
            chbx_CoverExisting = new CheckBox();
            chbx_MedleyNew = new CheckBox();
            chbx_MidiNew = new CheckBox();
            chbx_MidiExisting = new CheckBox();
            chbx_GameSoundtrackExisting = new CheckBox();
            lbl_P5 = new Label();
            chbx_AmateurCoverNew = new CheckBox();
            chbx_AmateurCoverExisting = new CheckBox();
            chbx_TVThemeNew = new CheckBox();
            chbx_TVThemeExisting = new CheckBox();
            chbx_UkuleleExisting = new CheckBox();
            chbx_MetalNew = new CheckBox();
            chbx_UkuleleNew = new CheckBox();
            txt_TuningFrecvExisting = new CueTextBox();
            txt_TuningFrecvNew = new CueTextBox();
            lbl_Frecv = new Label();
            label3 = new Label();
            txt_ExistingDuplicateOfID = new CueTextBox();
            bt_AddP1 = new Button();
            bt_AddP2 = new Button();
            bt_AddP3 = new Button();
            bt_AddP4 = new Button();
            bt_AddP5 = new Button();
            chbx_SlideExisting = new CheckBox();
            chbx_SlideNew = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPathNew).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPathExisting).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txt_AlternateNoExisting).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txt_AlternateNoNew).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // label16
            // 
            label16.Location = new System.Drawing.Point(50, 206);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(80, 15);
            label16.TabIndex = 283;
            label16.Text = "JSON Rhythm";
            // 
            // txt_DescriptionExisting
            // 
            txt_DescriptionExisting.Location = new System.Drawing.Point(656, 287);
            txt_DescriptionExisting.Margin = new Padding(2, 3, 2, 3);
            txt_DescriptionExisting.Name = "txt_DescriptionExisting";
            txt_DescriptionExisting.Size = new System.Drawing.Size(156, 66);
            txt_DescriptionExisting.TabIndex = 277;
            txt_DescriptionExisting.Text = "";
            // 
            // txt_DescriptionNew
            // 
            txt_DescriptionNew.Location = new System.Drawing.Point(656, 203);
            txt_DescriptionNew.Margin = new Padding(2, 3, 2, 3);
            txt_DescriptionNew.Name = "txt_DescriptionNew";
            txt_DescriptionNew.Size = new System.Drawing.Size(156, 66);
            txt_DescriptionNew.TabIndex = 276;
            txt_DescriptionNew.Text = "";
            // 
            // lbl_AvailTrk
            // 
            lbl_AvailTrk.Location = new System.Drawing.Point(25, 47);
            lbl_AvailTrk.Margin = new Padding(2, 0, 2, 0);
            lbl_AvailTrk.Name = "lbl_AvailTrk";
            lbl_AvailTrk.Size = new System.Drawing.Size(90, 16);
            lbl_AvailTrk.TabIndex = 275;
            lbl_AvailTrk.Text = "Available Tracks";
            // 
            // lbl_AudioPreview
            // 
            lbl_AudioPreview.Location = new System.Drawing.Point(72, 100);
            lbl_AudioPreview.Margin = new Padding(2, 0, 2, 0);
            lbl_AudioPreview.Name = "lbl_AudioPreview";
            lbl_AudioPreview.Size = new System.Drawing.Size(48, 16);
            lbl_AudioPreview.TabIndex = 274;
            lbl_AudioPreview.Text = "Preview";
            toolTip1.SetToolTip(lbl_AudioPreview, "When green Strong Indication of Having the same Source, since Hash is the same.");
            // 
            // lbl_AudioMain
            // 
            lbl_AudioMain.Location = new System.Drawing.Point(73, 74);
            lbl_AudioMain.Margin = new Padding(2, 0, 2, 0);
            lbl_AudioMain.Name = "lbl_AudioMain";
            lbl_AudioMain.Size = new System.Drawing.Size(47, 16);
            lbl_AudioMain.TabIndex = 273;
            lbl_AudioMain.Text = "Audio";
            toolTip1.SetToolTip(lbl_AudioMain, "When green Strong Indication of Having the same Source, since Hash is the same.");
            // 
            // label12
            // 
            label12.Location = new System.Drawing.Point(71, 48);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(59, 15);
            label12.TabIndex = 272;
            label12.Text = "XML Lead";
            // 
            // label11
            // 
            label11.Location = new System.Drawing.Point(73, 70);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(57, 15);
            label11.TabIndex = 271;
            label11.Text = "XML Bass";
            // 
            // label10
            // 
            label10.Location = new System.Drawing.Point(56, 94);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(74, 15);
            label10.TabIndex = 270;
            label10.Text = "XML Combo";
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(54, 116);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(76, 15);
            label9.TabIndex = 269;
            label9.Text = "XML Rhythm";
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(67, 139);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(63, 15);
            label7.TabIndex = 267;
            label7.Text = "JSON Lead";
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(69, 160);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(61, 15);
            label6.TabIndex = 266;
            label6.Text = "JSON Bass";
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(52, 185);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(78, 15);
            label5.TabIndex = 265;
            label5.Text = "JSON Combo";
            // 
            // lbl_AlbumArt
            // 
            lbl_AlbumArt.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_AlbumArt.Location = new System.Drawing.Point(648, 587);
            lbl_AlbumArt.Margin = new Padding(2, 0, 2, 0);
            lbl_AlbumArt.Name = "lbl_AlbumArt";
            lbl_AlbumArt.Size = new System.Drawing.Size(22, 16);
            lbl_AlbumArt.TabIndex = 258;
            lbl_AlbumArt.Text = "Vs.";
            // 
            // lbl_Title
            // 
            lbl_Title.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Title.Location = new System.Drawing.Point(274, 93);
            lbl_Title.Margin = new Padding(2, 0, 2, 0);
            lbl_Title.Name = "lbl_Title";
            lbl_Title.Size = new System.Drawing.Size(22, 16);
            lbl_Title.TabIndex = 218;
            lbl_Title.Text = "Vs.";
            // 
            // picbx_AlbumArtPathNew
            // 
            picbx_AlbumArtPathNew.Location = new System.Drawing.Point(508, 529);
            picbx_AlbumArtPathNew.Margin = new Padding(2, 3, 2, 3);
            picbx_AlbumArtPathNew.Name = "picbx_AlbumArtPathNew";
            picbx_AlbumArtPathNew.Size = new System.Drawing.Size(140, 140);
            picbx_AlbumArtPathNew.SizeMode = PictureBoxSizeMode.StretchImage;
            picbx_AlbumArtPathNew.TabIndex = 216;
            picbx_AlbumArtPathNew.TabStop = false;
            // 
            // picbx_AlbumArtPathExisting
            // 
            picbx_AlbumArtPathExisting.Location = new System.Drawing.Point(666, 529);
            picbx_AlbumArtPathExisting.Margin = new Padding(2, 3, 2, 3);
            picbx_AlbumArtPathExisting.Name = "picbx_AlbumArtPathExisting";
            picbx_AlbumArtPathExisting.Size = new System.Drawing.Size(140, 140);
            picbx_AlbumArtPathExisting.SizeMode = PictureBoxSizeMode.StretchImage;
            picbx_AlbumArtPathExisting.TabIndex = 215;
            picbx_AlbumArtPathExisting.TabStop = false;
            // 
            // btn_Update
            // 
            btn_Update.Font = new System.Drawing.Font("Segoe UI", 8F);
            btn_Update.Location = new System.Drawing.Point(682, 117);
            btn_Update.Margin = new Padding(2, 3, 2, 3);
            btn_Update.Name = "btn_Update";
            btn_Update.Size = new System.Drawing.Size(129, 34);
            btn_Update.TabIndex = 214;
            btn_Update.Text = "Update and Overwrite Existing";
            toolTip1.SetToolTip(btn_Update, "Archive previously imported sopng and replace its entries with the new CDLC");
            btn_Update.UseVisualStyleBackColor = true;
            btn_Update.Click += btn_Update_Click;
            // 
            // chbx_IsAlternateNew
            // 
            chbx_IsAlternateNew.Appearance = Appearance.Button;
            chbx_IsAlternateNew.Location = new System.Drawing.Point(78, 189);
            chbx_IsAlternateNew.Margin = new Padding(2, 3, 2, 3);
            chbx_IsAlternateNew.Name = "chbx_IsAlternateNew";
            chbx_IsAlternateNew.Size = new System.Drawing.Size(65, 25);
            chbx_IsAlternateNew.TabIndex = 211;
            chbx_IsAlternateNew.Text = "Alternate";
            chbx_IsAlternateNew.UseVisualStyleBackColor = true;
            chbx_IsAlternateNew.CheckedChanged += chbx_IsAlternateNew_CheckedChanged;
            // 
            // btn_Alternate
            // 
            btn_Alternate.Font = new System.Drawing.Font("Segoe UI", 8F);
            btn_Alternate.Location = new System.Drawing.Point(682, 52);
            btn_Alternate.Margin = new Padding(2, 3, 2, 3);
            btn_Alternate.Name = "btn_Alternate";
            btn_Alternate.Size = new System.Drawing.Size(129, 34);
            btn_Alternate.TabIndex = 212;
            btn_Alternate.Text = "Import New as Alternate";
            toolTip1.SetToolTip(btn_Alternate, "Import and mark it as Alternate (add Alt or Author ; make dlc name/id unique)");
            btn_Alternate.UseVisualStyleBackColor = true;
            btn_Alternate.Click += btn_Alternate_Click;
            // 
            // btn_Ignore
            // 
            btn_Ignore.Font = new System.Drawing.Font("Segoe UI", 8F);
            btn_Ignore.Location = new System.Drawing.Point(682, 83);
            btn_Ignore.Margin = new Padding(2, 3, 2, 3);
            btn_Ignore.Name = "btn_Ignore";
            btn_Ignore.Size = new System.Drawing.Size(129, 34);
            btn_Ignore.TabIndex = 213;
            btn_Ignore.Text = "Ignore New as OLD/Duplicate";
            btn_Ignore.UseVisualStyleBackColor = true;
            btn_Ignore.Click += btn_Ignore_Click;
            // 
            // lbl_TitleSort
            // 
            lbl_TitleSort.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_TitleSort.Location = new System.Drawing.Point(274, 112);
            lbl_TitleSort.Margin = new Padding(2, 0, 2, 0);
            lbl_TitleSort.Name = "lbl_TitleSort";
            lbl_TitleSort.Size = new System.Drawing.Size(22, 16);
            lbl_TitleSort.TabIndex = 284;
            lbl_TitleSort.Text = "Vs.";
            // 
            // lbl_ArtistSort
            // 
            lbl_ArtistSort.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_ArtistSort.Location = new System.Drawing.Point(274, 131);
            lbl_ArtistSort.Margin = new Padding(2, 0, 2, 0);
            lbl_ArtistSort.Name = "lbl_ArtistSort";
            lbl_ArtistSort.Size = new System.Drawing.Size(22, 16);
            lbl_ArtistSort.TabIndex = 285;
            lbl_ArtistSort.Text = "Vs.";
            lbl_ArtistSort.Click += Lbl_ArtistSort_Click;
            // 
            // lbl_FileName
            // 
            lbl_FileName.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_FileName.Location = new System.Drawing.Point(274, 173);
            lbl_FileName.Margin = new Padding(2, 0, 2, 0);
            lbl_FileName.Name = "lbl_FileName";
            lbl_FileName.Size = new System.Drawing.Size(22, 16);
            lbl_FileName.TabIndex = 286;
            lbl_FileName.Text = "Vs.";
            // 
            // lbl_IsOriginal
            // 
            lbl_IsOriginal.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_IsOriginal.Location = new System.Drawing.Point(274, 194);
            lbl_IsOriginal.Margin = new Padding(2, 0, 2, 0);
            lbl_IsOriginal.Name = "lbl_IsOriginal";
            lbl_IsOriginal.Size = new System.Drawing.Size(22, 16);
            lbl_IsOriginal.TabIndex = 287;
            lbl_IsOriginal.Text = "Vs.";
            // 
            // lbl_Toolkit
            // 
            lbl_Toolkit.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Toolkit.Location = new System.Drawing.Point(274, 218);
            lbl_Toolkit.Margin = new Padding(2, 0, 2, 0);
            lbl_Toolkit.Name = "lbl_Toolkit";
            lbl_Toolkit.Size = new System.Drawing.Size(22, 16);
            lbl_Toolkit.TabIndex = 288;
            lbl_Toolkit.Text = "Vs.";
            // 
            // lbl_Author
            // 
            lbl_Author.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Author.Location = new System.Drawing.Point(274, 239);
            lbl_Author.Margin = new Padding(2, 0, 2, 0);
            lbl_Author.Name = "lbl_Author";
            lbl_Author.Size = new System.Drawing.Size(22, 16);
            lbl_Author.TabIndex = 289;
            lbl_Author.Text = "Vs.";
            // 
            // lbl_Version
            // 
            lbl_Version.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Version.Location = new System.Drawing.Point(274, 261);
            lbl_Version.Margin = new Padding(2, 0, 2, 0);
            lbl_Version.Name = "lbl_Version";
            lbl_Version.Size = new System.Drawing.Size(22, 16);
            lbl_Version.TabIndex = 290;
            lbl_Version.Text = "Vs.";
            // 
            // lbl_Tuning
            // 
            lbl_Tuning.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Tuning.Location = new System.Drawing.Point(274, 487);
            lbl_Tuning.Margin = new Padding(2, 0, 2, 0);
            lbl_Tuning.Name = "lbl_Tuning";
            lbl_Tuning.Size = new System.Drawing.Size(22, 16);
            lbl_Tuning.TabIndex = 291;
            lbl_Tuning.Text = "Vs.";
            // 
            // lbl_DLCID
            // 
            lbl_DLCID.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_DLCID.Location = new System.Drawing.Point(274, 284);
            lbl_DLCID.Margin = new Padding(2, 0, 2, 0);
            lbl_DLCID.Name = "lbl_DLCID";
            lbl_DLCID.Size = new System.Drawing.Size(22, 16);
            lbl_DLCID.TabIndex = 292;
            lbl_DLCID.Text = "Vs.";
            // 
            // lbl_DD
            // 
            lbl_DD.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_DD.Location = new System.Drawing.Point(252, 23);
            lbl_DD.Margin = new Padding(2, 0, 2, 0);
            lbl_DD.Name = "lbl_DD";
            lbl_DD.Size = new System.Drawing.Size(22, 16);
            lbl_DD.TabIndex = 293;
            lbl_DD.Text = "Vs.";
            // 
            // lbl_AvailableTracks
            // 
            lbl_AvailableTracks.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_AvailableTracks.Location = new System.Drawing.Point(252, 48);
            lbl_AvailableTracks.Margin = new Padding(2, 0, 2, 0);
            lbl_AvailableTracks.Name = "lbl_AvailableTracks";
            lbl_AvailableTracks.Size = new System.Drawing.Size(22, 16);
            lbl_AvailableTracks.TabIndex = 294;
            lbl_AvailableTracks.Text = "Vs.";
            // 
            // lbl_Audio
            // 
            lbl_Audio.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Audio.Location = new System.Drawing.Point(252, 77);
            lbl_Audio.Margin = new Padding(2, 0, 2, 0);
            lbl_Audio.Name = "lbl_Audio";
            lbl_Audio.Size = new System.Drawing.Size(22, 16);
            lbl_Audio.TabIndex = 295;
            lbl_Audio.Text = "Vs.";
            // 
            // lbl_Preview
            // 
            lbl_Preview.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Preview.Location = new System.Drawing.Point(252, 102);
            lbl_Preview.Margin = new Padding(2, 0, 2, 0);
            lbl_Preview.Name = "lbl_Preview";
            lbl_Preview.Size = new System.Drawing.Size(22, 16);
            lbl_Preview.TabIndex = 296;
            lbl_Preview.Text = "Vs.";
            // 
            // lbl_XMLLead
            // 
            lbl_XMLLead.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_XMLLead.Location = new System.Drawing.Point(252, 48);
            lbl_XMLLead.Margin = new Padding(2, 0, 2, 0);
            lbl_XMLLead.Name = "lbl_XMLLead";
            lbl_XMLLead.Size = new System.Drawing.Size(22, 16);
            lbl_XMLLead.TabIndex = 297;
            lbl_XMLLead.Text = "Vs.";
            lbl_XMLLead.Visible = false;
            // 
            // lbl_XMLBass
            // 
            lbl_XMLBass.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_XMLBass.Location = new System.Drawing.Point(252, 69);
            lbl_XMLBass.Margin = new Padding(2, 0, 2, 0);
            lbl_XMLBass.Name = "lbl_XMLBass";
            lbl_XMLBass.Size = new System.Drawing.Size(22, 16);
            lbl_XMLBass.TabIndex = 298;
            lbl_XMLBass.Text = "Vs.";
            lbl_XMLBass.Visible = false;
            // 
            // lbl_XMLCombo
            // 
            lbl_XMLCombo.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_XMLCombo.Location = new System.Drawing.Point(252, 94);
            lbl_XMLCombo.Margin = new Padding(2, 0, 2, 0);
            lbl_XMLCombo.Name = "lbl_XMLCombo";
            lbl_XMLCombo.Size = new System.Drawing.Size(22, 16);
            lbl_XMLCombo.TabIndex = 299;
            lbl_XMLCombo.Text = "Vs.";
            lbl_XMLCombo.Visible = false;
            // 
            // lbl_XMLRhythm
            // 
            lbl_XMLRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_XMLRhythm.Location = new System.Drawing.Point(252, 116);
            lbl_XMLRhythm.Margin = new Padding(2, 0, 2, 0);
            lbl_XMLRhythm.Name = "lbl_XMLRhythm";
            lbl_XMLRhythm.Size = new System.Drawing.Size(22, 16);
            lbl_XMLRhythm.TabIndex = 300;
            lbl_XMLRhythm.Text = "Vs.";
            lbl_XMLRhythm.Visible = false;
            // 
            // lbl_JSONLead
            // 
            lbl_JSONLead.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_JSONLead.Location = new System.Drawing.Point(252, 139);
            lbl_JSONLead.Margin = new Padding(2, 0, 2, 0);
            lbl_JSONLead.Name = "lbl_JSONLead";
            lbl_JSONLead.Size = new System.Drawing.Size(22, 16);
            lbl_JSONLead.TabIndex = 302;
            lbl_JSONLead.Text = "Vs.";
            lbl_JSONLead.Visible = false;
            // 
            // lbl_JSONBass
            // 
            lbl_JSONBass.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_JSONBass.Location = new System.Drawing.Point(252, 160);
            lbl_JSONBass.Margin = new Padding(2, 0, 2, 0);
            lbl_JSONBass.Name = "lbl_JSONBass";
            lbl_JSONBass.Size = new System.Drawing.Size(22, 16);
            lbl_JSONBass.TabIndex = 303;
            lbl_JSONBass.Text = "Vs.";
            lbl_JSONBass.Visible = false;
            // 
            // lbl_JSONCombo
            // 
            lbl_JSONCombo.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_JSONCombo.Location = new System.Drawing.Point(252, 186);
            lbl_JSONCombo.Margin = new Padding(2, 0, 2, 0);
            lbl_JSONCombo.Name = "lbl_JSONCombo";
            lbl_JSONCombo.Size = new System.Drawing.Size(22, 16);
            lbl_JSONCombo.TabIndex = 304;
            lbl_JSONCombo.Text = "Vs.";
            lbl_JSONCombo.Visible = false;
            // 
            // lbl_JSONRhythm
            // 
            lbl_JSONRhythm.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_JSONRhythm.Location = new System.Drawing.Point(252, 208);
            lbl_JSONRhythm.Margin = new Padding(2, 0, 2, 0);
            lbl_JSONRhythm.Name = "lbl_JSONRhythm";
            lbl_JSONRhythm.Size = new System.Drawing.Size(22, 15);
            lbl_JSONRhythm.TabIndex = 305;
            lbl_JSONRhythm.Text = "Vs.";
            lbl_JSONRhythm.Visible = false;
            // 
            // lbl_Reference
            // 
            lbl_Reference.ForeColor = System.Drawing.Color.OrangeRed;
            lbl_Reference.Location = new System.Drawing.Point(273, 35);
            lbl_Reference.Margin = new Padding(2, 0, 2, 0);
            lbl_Reference.Name = "lbl_Reference";
            lbl_Reference.Size = new System.Drawing.Size(22, 15);
            lbl_Reference.TabIndex = 307;
            lbl_Reference.Text = "Vs.";
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(656, 185);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 15);
            label1.TabIndex = 310;
            label1.Text = "Description New";
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(656, 270);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(111, 15);
            label2.TabIndex = 311;
            label2.Text = "Description Existing";
            // 
            // chbx_IsOriginal
            // 
            chbx_IsOriginal.Checked = true;
            chbx_IsOriginal.CheckState = CheckState.Checked;
            chbx_IsOriginal.Enabled = false;
            chbx_IsOriginal.Location = new System.Drawing.Point(554, 187);
            chbx_IsOriginal.Margin = new Padding(2, 3, 2, 3);
            chbx_IsOriginal.Name = "chbx_IsOriginal";
            chbx_IsOriginal.Size = new System.Drawing.Size(68, 19);
            chbx_IsOriginal.TabIndex = 312;
            chbx_IsOriginal.Text = "Original";
            chbx_IsOriginal.UseVisualStyleBackColor = true;
            chbx_IsOriginal.Visible = false;
            // 
            // label17
            // 
            label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            label17.Location = new System.Drawing.Point(234, 33);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(57, 9);
            label17.TabIndex = 313;
            label17.Text = "MM-DD-YYYY";
            // 
            // lbl_NewIs_Original
            // 
            lbl_NewIs_Original.Location = new System.Drawing.Point(172, 194);
            lbl_NewIs_Original.Margin = new Padding(2, 0, 2, 0);
            lbl_NewIs_Original.Name = "lbl_NewIs_Original";
            lbl_NewIs_Original.Size = new System.Drawing.Size(56, 15);
            lbl_NewIs_Original.TabIndex = 314;
            lbl_NewIs_Original.Text = "Is Official";
            // 
            // label19
            // 
            label19.Location = new System.Drawing.Point(46, 20);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(76, 22);
            label19.TabIndex = 315;
            label19.Text = "DD Available";
            // 
            // label20
            // 
            label20.Location = new System.Drawing.Point(493, 260);
            label20.Margin = new Padding(2, 0, 2, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(116, 25);
            label20.TabIndex = 316;
            label20.Text = "Version/FileCreation";
            label20.Click += label20_Click;
            // 
            // label21
            // 
            label21.Location = new System.Drawing.Point(113, 236);
            label21.Margin = new Padding(2, 0, 2, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(44, 15);
            label21.TabIndex = 317;
            label21.Text = "Author";
            // 
            // lbl_Platfdormtoolkit
            // 
            lbl_Platfdormtoolkit.Location = new System.Drawing.Point(22, 214);
            lbl_Platfdormtoolkit.Margin = new Padding(2, 0, 2, 0);
            lbl_Platfdormtoolkit.Name = "lbl_Platfdormtoolkit";
            lbl_Platfdormtoolkit.Size = new System.Drawing.Size(93, 15);
            lbl_Platfdormtoolkit.TabIndex = 318;
            lbl_Platfdormtoolkit.Text = "Platform/Toolkit";
            // 
            // label23
            // 
            label23.Location = new System.Drawing.Point(483, 487);
            label23.Margin = new Padding(2, 0, 2, 0);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(161, 17);
            label23.TabIndex = 320;
            label23.Text = "Tuning Strings";
            // 
            // btn_UpdateExisting
            // 
            btn_UpdateExisting.Location = new System.Drawing.Point(428, 30);
            btn_UpdateExisting.Margin = new Padding(2, 3, 2, 3);
            btn_UpdateExisting.Name = "btn_UpdateExisting";
            btn_UpdateExisting.Size = new System.Drawing.Size(94, 19);
            btn_UpdateExisting.TabIndex = 321;
            btn_UpdateExisting.Text = "Update Existing";
            btn_UpdateExisting.UseVisualStyleBackColor = true;
            btn_UpdateExisting.Click += button2_Click;
            // 
            // lblNew
            // 
            lblNew.BorderStyle = BorderStyle.Fixed3D;
            lblNew.Location = new System.Drawing.Point(159, 32);
            lblNew.Margin = new Padding(2, 0, 2, 0);
            lblNew.Name = "lblNew";
            lblNew.Size = new System.Drawing.Size(114, 17);
            lblNew.TabIndex = 322;
            lblNew.Text = "Currently Importing";
            // 
            // lblExisting
            // 
            lblExisting.BorderStyle = BorderStyle.Fixed3D;
            lblExisting.Location = new System.Drawing.Point(298, 32);
            lblExisting.Margin = new Padding(2, 0, 2, 0);
            lblExisting.Name = "lblExisting";
            lblExisting.Size = new System.Drawing.Size(101, 17);
            lblExisting.TabIndex = 323;
            lblExisting.Text = "Already Imported";
            // 
            // lbl_IDExisting
            // 
            lbl_IDExisting.BorderStyle = BorderStyle.FixedSingle;
            lbl_IDExisting.Location = new System.Drawing.Point(388, 32);
            lbl_IDExisting.Margin = new Padding(2, 0, 2, 0);
            lbl_IDExisting.Name = "lbl_IDExisting";
            lbl_IDExisting.Size = new System.Drawing.Size(64, 17);
            lbl_IDExisting.TabIndex = 324;
            lbl_IDExisting.Text = "ID Existing";
            // 
            // label26
            // 
            label26.Location = new System.Drawing.Point(2, 283);
            label26.Margin = new Padding(2, 0, 2, 0);
            label26.Name = "label26";
            label26.Size = new System.Drawing.Size(153, 15);
            label26.TabIndex = 326;
            label26.Text = "DLC Name (autom. unique)";
            // 
            // btn_OpenMainDB
            // 
            btn_OpenMainDB.BackColor = System.Drawing.SystemColors.MenuHighlight;
            btn_OpenMainDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btn_OpenMainDB.Location = new System.Drawing.Point(722, 817);
            btn_OpenMainDB.Margin = new Padding(2);
            btn_OpenMainDB.Name = "btn_OpenMainDB";
            btn_OpenMainDB.Size = new System.Drawing.Size(90, 43);
            btn_OpenMainDB.TabIndex = 327;
            btn_OpenMainDB.Text = "Open Main DB";
            btn_OpenMainDB.UseVisualStyleBackColor = false;
            btn_OpenMainDB.Click += btn_DecompressAll_Click;
            // 
            // label27
            // 
            label27.BorderStyle = BorderStyle.Fixed3D;
            label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            label27.Location = new System.Drawing.Point(244, 10);
            label27.Margin = new Padding(2, 0, 2, 0);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(88, 18);
            label27.TabIndex = 328;
            label27.Text = "Differences";
            // 
            // label28
            // 
            label28.Location = new System.Drawing.Point(18, 89);
            label28.Margin = new Padding(2, 0, 2, 0);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(29, 15);
            label28.TabIndex = 329;
            label28.Text = "Title";
            // 
            // label29
            // 
            label29.Location = new System.Drawing.Point(13, 108);
            label29.Margin = new Padding(2, 0, 2, 0);
            label29.Name = "label29";
            label29.Size = new System.Drawing.Size(34, 15);
            label29.TabIndex = 330;
            label29.Text = "TSort";
            // 
            // label30
            // 
            label30.Location = new System.Drawing.Point(6, 130);
            label30.Margin = new Padding(2, 0, 2, 0);
            label30.Name = "label30";
            label30.Size = new System.Drawing.Size(47, 15);
            label30.TabIndex = 331;
            label30.Text = "Art Sort";
            // 
            // label31
            // 
            label31.Location = new System.Drawing.Point(25, 174);
            label31.Margin = new Padding(2, 0, 2, 0);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(22, 15);
            label31.TabIndex = 332;
            label31.Text = "FN";
            // 
            // lbl_diffCount
            // 
            lbl_diffCount.BorderStyle = BorderStyle.Fixed3D;
            lbl_diffCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            lbl_diffCount.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_diffCount.Location = new System.Drawing.Point(336, 5);
            lbl_diffCount.Margin = new Padding(2, 0, 2, 0);
            lbl_diffCount.Name = "lbl_diffCount";
            lbl_diffCount.Size = new System.Drawing.Size(60, 26);
            lbl_diffCount.TabIndex = 335;
            lbl_diffCount.Text = "xx/yy";
            // 
            // chbx_IgnoreDupli
            // 
            chbx_IgnoreDupli.Enabled = false;
            chbx_IgnoreDupli.Location = new System.Drawing.Point(567, 11);
            chbx_IgnoreDupli.Margin = new Padding(2, 3, 2, 3);
            chbx_IgnoreDupli.Name = "chbx_IgnoreDupli";
            chbx_IgnoreDupli.Size = new System.Drawing.Size(175, 19);
            chbx_IgnoreDupli.TabIndex = 336;
            chbx_IgnoreDupli.Text = "Ignore remaining Duplicates";
            chbx_IgnoreDupli.UseVisualStyleBackColor = true;
            // 
            // btn_RemoveOldNew
            // 
            btn_RemoveOldNew.Location = new System.Drawing.Point(582, 108);
            btn_RemoveOldNew.Margin = new Padding(2, 3, 2, 3);
            btn_RemoveOldNew.Name = "btn_RemoveOldNew";
            btn_RemoveOldNew.Size = new System.Drawing.Size(93, 26);
            btn_RemoveOldNew.TabIndex = 338;
            btn_RemoveOldNew.Text = "Clear extra txt";
            toolTip1.SetToolTip(btn_RemoveOldNew, "Cear away any added");
            btn_RemoveOldNew.UseVisualStyleBackColor = true;
            btn_RemoveOldNew.Click += btn_RemoveOldNew_Click;
            // 
            // lbl_Vocals
            // 
            lbl_Vocals.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Vocals.Location = new System.Drawing.Point(252, 124);
            lbl_Vocals.Margin = new Padding(2, 0, 2, 0);
            lbl_Vocals.Name = "lbl_Vocals";
            lbl_Vocals.Size = new System.Drawing.Size(22, 16);
            lbl_Vocals.TabIndex = 342;
            lbl_Vocals.Text = "Vs.";
            lbl_Vocals.Visible = false;
            // 
            // lbl_txt_Vocals
            // 
            lbl_txt_Vocals.Location = new System.Drawing.Point(144, 122);
            lbl_txt_Vocals.Margin = new Padding(2, 0, 2, 0);
            lbl_txt_Vocals.Name = "lbl_txt_Vocals";
            lbl_txt_Vocals.Size = new System.Drawing.Size(40, 16);
            lbl_txt_Vocals.TabIndex = 341;
            lbl_txt_Vocals.Text = "Vocals";
            toolTip1.SetToolTip(lbl_txt_Vocals, "When green Strong Indication of Having the same Source, since Hash is the same.");
            // 
            // btn_TitleNew
            // 
            btn_TitleNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TitleNew.Location = new System.Drawing.Point(520, 93);
            btn_TitleNew.Margin = new Padding(2);
            btn_TitleNew.Name = "btn_TitleNew";
            btn_TitleNew.Size = new System.Drawing.Size(18, 16);
            btn_TitleNew.TabIndex = 349;
            btn_TitleNew.Text = "<";
            toolTip1.SetToolTip(btn_TitleNew, "Overrite New Title with Existing Title");
            btn_TitleNew.UseVisualStyleBackColor = true;
            btn_TitleNew.Click += btn_TitleNew_Click;
            // 
            // btn_TitleExisting
            // 
            btn_TitleExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TitleExisting.Location = new System.Drawing.Point(540, 93);
            btn_TitleExisting.Margin = new Padding(2);
            btn_TitleExisting.Name = "btn_TitleExisting";
            btn_TitleExisting.Size = new System.Drawing.Size(18, 16);
            btn_TitleExisting.TabIndex = 348;
            btn_TitleExisting.Text = ">";
            toolTip1.SetToolTip(btn_TitleExisting, "Overrite Existing Title with New Title");
            btn_TitleExisting.UseVisualStyleBackColor = true;
            btn_TitleExisting.Click += btn_TitleExisting_Click;
            // 
            // btn_TitleSortNew
            // 
            btn_TitleSortNew.Enabled = false;
            btn_TitleSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TitleSortNew.Location = new System.Drawing.Point(520, 112);
            btn_TitleSortNew.Margin = new Padding(2);
            btn_TitleSortNew.Name = "btn_TitleSortNew";
            btn_TitleSortNew.Size = new System.Drawing.Size(18, 16);
            btn_TitleSortNew.TabIndex = 351;
            btn_TitleSortNew.Text = "<";
            toolTip1.SetToolTip(btn_TitleSortNew, "Overrite New Title SORT with Existing Title SORT");
            btn_TitleSortNew.UseVisualStyleBackColor = true;
            btn_TitleSortNew.Click += btn_TitleSortNew_Click;
            // 
            // btn_TitleSortExisting
            // 
            btn_TitleSortExisting.Enabled = false;
            btn_TitleSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TitleSortExisting.Location = new System.Drawing.Point(540, 112);
            btn_TitleSortExisting.Margin = new Padding(2);
            btn_TitleSortExisting.Name = "btn_TitleSortExisting";
            btn_TitleSortExisting.Size = new System.Drawing.Size(18, 16);
            btn_TitleSortExisting.TabIndex = 350;
            btn_TitleSortExisting.Text = ">";
            toolTip1.SetToolTip(btn_TitleSortExisting, "Overrite Existing Title SORT with New Title SORT");
            btn_TitleSortExisting.UseVisualStyleBackColor = true;
            btn_TitleSortExisting.Click += btnTitleSortExisting_Click;
            // 
            // btn_AuthorNew
            // 
            btn_AuthorNew.Enabled = false;
            btn_AuthorNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AuthorNew.Location = new System.Drawing.Point(418, 237);
            btn_AuthorNew.Margin = new Padding(2);
            btn_AuthorNew.Name = "btn_AuthorNew";
            btn_AuthorNew.Size = new System.Drawing.Size(18, 16);
            btn_AuthorNew.TabIndex = 353;
            btn_AuthorNew.Text = "<";
            btn_AuthorNew.UseVisualStyleBackColor = true;
            btn_AuthorNew.Click += btn_AuthorNew_Click;
            // 
            // btn_AuthorExisting
            // 
            btn_AuthorExisting.Enabled = false;
            btn_AuthorExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AuthorExisting.Location = new System.Drawing.Point(438, 237);
            btn_AuthorExisting.Margin = new Padding(2);
            btn_AuthorExisting.Name = "btn_AuthorExisting";
            btn_AuthorExisting.Size = new System.Drawing.Size(18, 16);
            btn_AuthorExisting.TabIndex = 352;
            btn_AuthorExisting.Text = ">";
            btn_AuthorExisting.UseVisualStyleBackColor = true;
            btn_AuthorExisting.Click += btn_AuthorExisting_Click;
            // 
            // lbl_Album
            // 
            lbl_Album.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Album.Location = new System.Drawing.Point(274, 74);
            lbl_Album.Margin = new Padding(2, 0, 2, 0);
            lbl_Album.Name = "lbl_Album";
            lbl_Album.Size = new System.Drawing.Size(22, 16);
            lbl_Album.TabIndex = 356;
            lbl_Album.Text = "Vs.";
            // 
            // lbl_Artist
            // 
            lbl_Artist.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Artist.Location = new System.Drawing.Point(274, 54);
            lbl_Artist.Margin = new Padding(2, 0, 2, 0);
            lbl_Artist.Name = "lbl_Artist";
            lbl_Artist.Size = new System.Drawing.Size(22, 16);
            lbl_Artist.TabIndex = 357;
            lbl_Artist.Text = "Vs.";
            // 
            // btn_AlbumNew
            // 
            btn_AlbumNew.Enabled = false;
            btn_AlbumNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AlbumNew.Location = new System.Drawing.Point(520, 73);
            btn_AlbumNew.Margin = new Padding(2);
            btn_AlbumNew.Name = "btn_AlbumNew";
            btn_AlbumNew.Size = new System.Drawing.Size(18, 16);
            btn_AlbumNew.TabIndex = 359;
            btn_AlbumNew.Text = "<";
            toolTip1.SetToolTip(btn_AlbumNew, "Overrite New Album with Existing Album");
            btn_AlbumNew.UseVisualStyleBackColor = true;
            btn_AlbumNew.Click += btn_AlbumNew_Click;
            // 
            // btn_AlbumExisting
            // 
            btn_AlbumExisting.Enabled = false;
            btn_AlbumExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AlbumExisting.Location = new System.Drawing.Point(540, 73);
            btn_AlbumExisting.Margin = new Padding(2);
            btn_AlbumExisting.Name = "btn_AlbumExisting";
            btn_AlbumExisting.Size = new System.Drawing.Size(18, 16);
            btn_AlbumExisting.TabIndex = 358;
            btn_AlbumExisting.Text = ">";
            toolTip1.SetToolTip(btn_AlbumExisting, "Overrite New Album with Existing Album");
            btn_AlbumExisting.UseVisualStyleBackColor = true;
            btn_AlbumExisting.Click += btn_AlbumExisting_Click;
            // 
            // btn_ArtistNew
            // 
            btn_ArtistNew.Enabled = false;
            btn_ArtistNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ArtistNew.Location = new System.Drawing.Point(520, 52);
            btn_ArtistNew.Margin = new Padding(2);
            btn_ArtistNew.Name = "btn_ArtistNew";
            btn_ArtistNew.Size = new System.Drawing.Size(18, 16);
            btn_ArtistNew.TabIndex = 361;
            btn_ArtistNew.Text = "<";
            toolTip1.SetToolTip(btn_ArtistNew, "Overrite New Artist name with Existing Artist name");
            btn_ArtistNew.UseVisualStyleBackColor = true;
            btn_ArtistNew.Click += button2_Click_1;
            // 
            // btn_ArtistExisting
            // 
            btn_ArtistExisting.Enabled = false;
            btn_ArtistExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ArtistExisting.Location = new System.Drawing.Point(540, 52);
            btn_ArtistExisting.Margin = new Padding(2);
            btn_ArtistExisting.Name = "btn_ArtistExisting";
            btn_ArtistExisting.Size = new System.Drawing.Size(18, 16);
            btn_ArtistExisting.TabIndex = 360;
            btn_ArtistExisting.Text = ">";
            toolTip1.SetToolTip(btn_ArtistExisting, "Overrite New Artist name with Existing Artist name");
            btn_ArtistExisting.UseVisualStyleBackColor = true;
            btn_ArtistExisting.Click += btn_ArtistExisting_Click;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(18, 50);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(35, 15);
            label4.TabIndex = 362;
            label4.Text = "Artist";
            // 
            // label18
            // 
            label18.Location = new System.Drawing.Point(13, 68);
            label18.Margin = new Padding(2, 0, 2, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(40, 15);
            label18.TabIndex = 363;
            label18.Text = "Abum";
            // 
            // btn_ArtistSortNew
            // 
            btn_ArtistSortNew.Enabled = false;
            btn_ArtistSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ArtistSortNew.Location = new System.Drawing.Point(520, 134);
            btn_ArtistSortNew.Margin = new Padding(2);
            btn_ArtistSortNew.Name = "btn_ArtistSortNew";
            btn_ArtistSortNew.Size = new System.Drawing.Size(18, 16);
            btn_ArtistSortNew.TabIndex = 366;
            btn_ArtistSortNew.Text = "<";
            toolTip1.SetToolTip(btn_ArtistSortNew, "Overrite New Artist SORT name with Existing Artist SORT name");
            btn_ArtistSortNew.UseVisualStyleBackColor = true;
            btn_ArtistSortNew.Click += btn_ArtistSortNew_Click;
            // 
            // btn_ArtistSortExisting
            // 
            btn_ArtistSortExisting.Enabled = false;
            btn_ArtistSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_ArtistSortExisting.Location = new System.Drawing.Point(540, 133);
            btn_ArtistSortExisting.Margin = new Padding(2);
            btn_ArtistSortExisting.Name = "btn_ArtistSortExisting";
            btn_ArtistSortExisting.Size = new System.Drawing.Size(18, 16);
            btn_ArtistSortExisting.TabIndex = 365;
            btn_ArtistSortExisting.Text = ">";
            toolTip1.SetToolTip(btn_ArtistSortExisting, "Overrite Existing Artist SORT name with New Artist SORT name");
            btn_ArtistSortExisting.UseVisualStyleBackColor = true;
            btn_ArtistSortExisting.Click += btn_ArtistSortExisting_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_UseDates);
            groupBox2.Controls.Add(btn_TN_Rhythm);
            groupBox2.Controls.Add(btn_TN_Combo);
            groupBox2.Controls.Add(btn_TN_Bass);
            groupBox2.Controls.Add(btn_TN_Lead);
            groupBox2.Controls.Add(btn_WM_Rhythm);
            groupBox2.Controls.Add(btn_WM_Combo);
            groupBox2.Controls.Add(btn_WM_Bass);
            groupBox2.Controls.Add(btn_WM_Leads);
            groupBox2.Controls.Add(lbl_DateExisting);
            groupBox2.Controls.Add(lbl_DateNew);
            groupBox2.Controls.Add(lbl_tonediff);
            groupBox2.Controls.Add(btn_GoToNew);
            groupBox2.Controls.Add(btn_GoToExisting);
            groupBox2.Controls.Add(btn_AddAge);
            groupBox2.Controls.Add(lbl_Existing);
            groupBox2.Controls.Add(txt_JSONLeadExisting);
            groupBox2.Controls.Add(lbl_New);
            groupBox2.Controls.Add(txt_XMLLeadNew);
            groupBox2.Controls.Add(txt_XMLLeadExisting);
            groupBox2.Controls.Add(txt_XMLBassNew);
            groupBox2.Controls.Add(txt_XMLBassExisting);
            groupBox2.Controls.Add(txt_XMLComboNew);
            groupBox2.Controls.Add(txt_XMLComboExisting);
            groupBox2.Controls.Add(txt_XMLRhythmNew);
            groupBox2.Controls.Add(txt_XMLRhythmExisting);
            groupBox2.Controls.Add(txt_JSONLeadNew);
            groupBox2.Controls.Add(txt_JSONBassNew);
            groupBox2.Controls.Add(txt_JSONBassExisting);
            groupBox2.Controls.Add(txt_JSONComboNew);
            groupBox2.Controls.Add(txt_JSONComboExisting);
            groupBox2.Controls.Add(txt_JSONRhythmNew);
            groupBox2.Controls.Add(txt_JSONRhythmExisting);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(lbl_XMLLead);
            groupBox2.Controls.Add(lbl_XMLBass);
            groupBox2.Controls.Add(lbl_XMLCombo);
            groupBox2.Controls.Add(lbl_XMLRhythm);
            groupBox2.Controls.Add(lbl_JSONLead);
            groupBox2.Controls.Add(lbl_JSONBass);
            groupBox2.Controls.Add(lbl_JSONCombo);
            groupBox2.Controls.Add(lbl_JSONRhythm);
            groupBox2.Controls.Add(label17);
            groupBox2.Location = new System.Drawing.Point(22, 640);
            groupBox2.Margin = new Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 3, 2, 3);
            groupBox2.Size = new System.Drawing.Size(480, 224);
            groupBox2.TabIndex = 367;
            groupBox2.TabStop = false;
            groupBox2.Text = "XML-notes(BIN/SNG & cleaned XML)/JSON-tone comparison on Last Conversion Date or Hash";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // btn_UseDates
            // 
            btn_UseDates.Checked = true;
            btn_UseDates.CheckState = CheckState.Checked;
            btn_UseDates.Location = new System.Drawing.Point(6, 13);
            btn_UseDates.Margin = new Padding(2, 3, 2, 3);
            btn_UseDates.Name = "btn_UseDates";
            btn_UseDates.Size = new System.Drawing.Size(152, 18);
            btn_UseDates.TabIndex = 408;
            btn_UseDates.Text = "Use Age instead of Date";
            toolTip1.SetToolTip(btn_UseDates, "Use older/newer or 2018//2016 to differentiate CDLCs. Note older and newer is only comparing the currently displayed CDCLs");
            btn_UseDates.UseVisualStyleBackColor = true;
            btn_UseDates.CheckedChanged += btn_UseDates_CheckedChanged;
            // 
            // btn_TN_Rhythm
            // 
            btn_TN_Rhythm.Enabled = false;
            btn_TN_Rhythm.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TN_Rhythm.Location = new System.Drawing.Point(390, 202);
            btn_TN_Rhythm.Margin = new Padding(2, 3, 2, 3);
            btn_TN_Rhythm.Name = "btn_TN_Rhythm";
            btn_TN_Rhythm.Size = new System.Drawing.Size(28, 16);
            btn_TN_Rhythm.TabIndex = 407;
            btn_TN_Rhythm.Text = "WM";
            btn_TN_Rhythm.UseVisualStyleBackColor = true;
            btn_TN_Rhythm.Click += btn_TN_Rhythm_Click;
            // 
            // btn_TN_Combo
            // 
            btn_TN_Combo.Enabled = false;
            btn_TN_Combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TN_Combo.Location = new System.Drawing.Point(390, 179);
            btn_TN_Combo.Margin = new Padding(2, 3, 2, 3);
            btn_TN_Combo.Name = "btn_TN_Combo";
            btn_TN_Combo.Size = new System.Drawing.Size(28, 16);
            btn_TN_Combo.TabIndex = 406;
            btn_TN_Combo.Text = "WM";
            btn_TN_Combo.UseVisualStyleBackColor = true;
            btn_TN_Combo.Click += btn_TN_Combo_Click;
            // 
            // btn_TN_Bass
            // 
            btn_TN_Bass.Enabled = false;
            btn_TN_Bass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TN_Bass.Location = new System.Drawing.Point(390, 156);
            btn_TN_Bass.Margin = new Padding(2, 3, 2, 3);
            btn_TN_Bass.Name = "btn_TN_Bass";
            btn_TN_Bass.Size = new System.Drawing.Size(28, 16);
            btn_TN_Bass.TabIndex = 405;
            btn_TN_Bass.Text = "WM";
            btn_TN_Bass.UseVisualStyleBackColor = true;
            btn_TN_Bass.Click += btn_TN_Bass_Click;
            // 
            // btn_TN_Lead
            // 
            btn_TN_Lead.Enabled = false;
            btn_TN_Lead.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_TN_Lead.Location = new System.Drawing.Point(390, 134);
            btn_TN_Lead.Margin = new Padding(2, 3, 2, 3);
            btn_TN_Lead.Name = "btn_TN_Lead";
            btn_TN_Lead.Size = new System.Drawing.Size(28, 16);
            btn_TN_Lead.TabIndex = 404;
            btn_TN_Lead.Text = "WM";
            btn_TN_Lead.UseVisualStyleBackColor = true;
            btn_TN_Lead.Click += btn_TN_Lead_Click;
            // 
            // btn_WM_Rhythm
            // 
            btn_WM_Rhythm.Enabled = false;
            btn_WM_Rhythm.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_WM_Rhythm.Location = new System.Drawing.Point(390, 112);
            btn_WM_Rhythm.Margin = new Padding(2, 3, 2, 3);
            btn_WM_Rhythm.Name = "btn_WM_Rhythm";
            btn_WM_Rhythm.Size = new System.Drawing.Size(28, 16);
            btn_WM_Rhythm.TabIndex = 403;
            btn_WM_Rhythm.Text = "WM";
            btn_WM_Rhythm.UseVisualStyleBackColor = true;
            btn_WM_Rhythm.Click += btn_WM_Rhythm_Click;
            // 
            // btn_WM_Combo
            // 
            btn_WM_Combo.Enabled = false;
            btn_WM_Combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_WM_Combo.Location = new System.Drawing.Point(390, 90);
            btn_WM_Combo.Margin = new Padding(2, 3, 2, 3);
            btn_WM_Combo.Name = "btn_WM_Combo";
            btn_WM_Combo.Size = new System.Drawing.Size(28, 16);
            btn_WM_Combo.TabIndex = 402;
            btn_WM_Combo.Text = "WM";
            btn_WM_Combo.UseVisualStyleBackColor = true;
            btn_WM_Combo.Click += btn_WM_Combo_Click;
            // 
            // btn_WM_Bass
            // 
            btn_WM_Bass.Enabled = false;
            btn_WM_Bass.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_WM_Bass.Location = new System.Drawing.Point(390, 66);
            btn_WM_Bass.Margin = new Padding(2, 3, 2, 3);
            btn_WM_Bass.Name = "btn_WM_Bass";
            btn_WM_Bass.Size = new System.Drawing.Size(28, 16);
            btn_WM_Bass.TabIndex = 401;
            btn_WM_Bass.Text = "WM";
            btn_WM_Bass.UseVisualStyleBackColor = true;
            btn_WM_Bass.Click += button1_Click;
            // 
            // btn_WM_Leads
            // 
            btn_WM_Leads.Enabled = false;
            btn_WM_Leads.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_WM_Leads.Location = new System.Drawing.Point(390, 45);
            btn_WM_Leads.Margin = new Padding(2, 3, 2, 3);
            btn_WM_Leads.Name = "btn_WM_Leads";
            btn_WM_Leads.Size = new System.Drawing.Size(28, 16);
            btn_WM_Leads.TabIndex = 400;
            btn_WM_Leads.Text = "WM";
            btn_WM_Leads.UseVisualStyleBackColor = true;
            btn_WM_Leads.Click += btn_WM_Lead_Click_1;
            // 
            // lbl_DateExisting
            // 
            lbl_DateExisting.BorderStyle = BorderStyle.Fixed3D;
            lbl_DateExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lbl_DateExisting.Location = new System.Drawing.Point(290, 29);
            lbl_DateExisting.Margin = new Padding(2, 0, 2, 0);
            lbl_DateExisting.Name = "lbl_DateExisting";
            lbl_DateExisting.Size = new System.Drawing.Size(72, 14);
            lbl_DateExisting.TabIndex = 398;
            lbl_DateExisting.Text = "older";
            // 
            // lbl_DateNew
            // 
            lbl_DateNew.BorderStyle = BorderStyle.Fixed3D;
            lbl_DateNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lbl_DateNew.Location = new System.Drawing.Point(159, 29);
            lbl_DateNew.Margin = new Padding(2, 0, 2, 0);
            lbl_DateNew.Name = "lbl_DateNew";
            lbl_DateNew.Size = new System.Drawing.Size(74, 15);
            lbl_DateNew.TabIndex = 397;
            lbl_DateNew.Text = "newer";
            // 
            // lbl_tonediff
            // 
            lbl_tonediff.BorderStyle = BorderStyle.FixedSingle;
            lbl_tonediff.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lbl_tonediff.Location = new System.Drawing.Point(209, 15);
            lbl_tonediff.Margin = new Padding(2, 0, 2, 0);
            lbl_tonediff.Name = "lbl_tonediff";
            lbl_tonediff.Size = new System.Drawing.Size(98, 18);
            lbl_tonediff.TabIndex = 399;
            lbl_tonediff.Text = "?Tone difference";
            lbl_tonediff.Visible = false;
            // 
            // btn_GoToNew
            // 
            btn_GoToNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoToNew.Location = new System.Drawing.Point(136, 29);
            btn_GoToNew.Margin = new Padding(2, 3, 2, 3);
            btn_GoToNew.Name = "btn_GoToNew";
            btn_GoToNew.Size = new System.Drawing.Size(22, 14);
            btn_GoToNew.TabIndex = 396;
            btn_GoToNew.Text = "<->";
            btn_GoToNew.UseVisualStyleBackColor = true;
            btn_GoToNew.Click += btn_GoToNew_Click;
            // 
            // btn_GoToExisting
            // 
            btn_GoToExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoToExisting.Location = new System.Drawing.Point(363, 28);
            btn_GoToExisting.Margin = new Padding(2, 3, 2, 3);
            btn_GoToExisting.Name = "btn_GoToExisting";
            btn_GoToExisting.Size = new System.Drawing.Size(23, 16);
            btn_GoToExisting.TabIndex = 395;
            btn_GoToExisting.Text = "<->";
            btn_GoToExisting.UseVisualStyleBackColor = true;
            btn_GoToExisting.Click += btn_GoToExisting_Click;
            // 
            // btn_AddAge
            // 
            btn_AddAge.BackColor = System.Drawing.SystemColors.Control;
            btn_AddAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddAge.Location = new System.Drawing.Point(344, 15);
            btn_AddAge.Margin = new Padding(2);
            btn_AddAge.Name = "btn_AddAge";
            btn_AddAge.Size = new System.Drawing.Size(14, 14);
            btn_AddAge.TabIndex = 390;
            btn_AddAge.Text = "+";
            btn_AddAge.UseVisualStyleBackColor = false;
            btn_AddAge.TextChanged += ExistingChanged;
            btn_AddAge.Click += btn_AddAge_Click;
            // 
            // lbl_Existing
            // 
            lbl_Existing.BorderStyle = BorderStyle.Fixed3D;
            lbl_Existing.Location = new System.Drawing.Point(304, 13);
            lbl_Existing.Margin = new Padding(2, 0, 2, 0);
            lbl_Existing.Name = "lbl_Existing";
            lbl_Existing.Size = new System.Drawing.Size(40, 17);
            lbl_Existing.TabIndex = 391;
            lbl_Existing.Text = "newer";
            // 
            // txt_JSONLeadExisting
            // 
            txt_JSONLeadExisting.Cue = "JSON Lead Existing";
            txt_JSONLeadExisting.Enabled = false;
            txt_JSONLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONLeadExisting.ForeColor = System.Drawing.Color.Gray;
            txt_JSONLeadExisting.Location = new System.Drawing.Point(272, 137);
            txt_JSONLeadExisting.Margin = new Padding(4, 6, 4, 6);
            txt_JSONLeadExisting.Name = "txt_JSONLeadExisting";
            txt_JSONLeadExisting.Size = new System.Drawing.Size(117, 20);
            txt_JSONLeadExisting.TabIndex = 253;
            // 
            // lbl_New
            // 
            lbl_New.BorderStyle = BorderStyle.Fixed3D;
            lbl_New.Location = new System.Drawing.Point(170, 15);
            lbl_New.Margin = new Padding(2, 0, 2, 0);
            lbl_New.Name = "lbl_New";
            lbl_New.Size = new System.Drawing.Size(36, 17);
            lbl_New.TabIndex = 390;
            lbl_New.Text = "older";
            // 
            // txt_XMLLeadNew
            // 
            txt_XMLLeadNew.Cue = "XML Lead New";
            txt_XMLLeadNew.Enabled = false;
            txt_XMLLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLLeadNew.ForeColor = System.Drawing.Color.Gray;
            txt_XMLLeadNew.Location = new System.Drawing.Point(132, 46);
            txt_XMLLeadNew.Margin = new Padding(4, 6, 4, 6);
            txt_XMLLeadNew.Name = "txt_XMLLeadNew";
            txt_XMLLeadNew.Size = new System.Drawing.Size(120, 20);
            txt_XMLLeadNew.TabIndex = 240;
            txt_XMLLeadNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_XMLLeadExisting
            // 
            txt_XMLLeadExisting.Cue = "XML Lead Existing";
            txt_XMLLeadExisting.Enabled = false;
            txt_XMLLeadExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLLeadExisting.ForeColor = System.Drawing.Color.Gray;
            txt_XMLLeadExisting.Location = new System.Drawing.Point(272, 44);
            txt_XMLLeadExisting.Margin = new Padding(4, 6, 4, 6);
            txt_XMLLeadExisting.Name = "txt_XMLLeadExisting";
            txt_XMLLeadExisting.Size = new System.Drawing.Size(117, 20);
            txt_XMLLeadExisting.TabIndex = 241;
            // 
            // txt_XMLBassNew
            // 
            txt_XMLBassNew.Cue = "XML Bass New";
            txt_XMLBassNew.Enabled = false;
            txt_XMLBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLBassNew.ForeColor = System.Drawing.Color.Gray;
            txt_XMLBassNew.Location = new System.Drawing.Point(130, 65);
            txt_XMLBassNew.Margin = new Padding(4, 6, 4, 6);
            txt_XMLBassNew.Name = "txt_XMLBassNew";
            txt_XMLBassNew.Size = new System.Drawing.Size(122, 20);
            txt_XMLBassNew.TabIndex = 244;
            txt_XMLBassNew.TextAlign = HorizontalAlignment.Right;
            txt_XMLBassNew.TextChanged += txt_XMLBassNew_TextChanged;
            // 
            // txt_XMLBassExisting
            // 
            txt_XMLBassExisting.Cue = "XML Bass Existing";
            txt_XMLBassExisting.Enabled = false;
            txt_XMLBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLBassExisting.ForeColor = System.Drawing.Color.Gray;
            txt_XMLBassExisting.Location = new System.Drawing.Point(272, 65);
            txt_XMLBassExisting.Margin = new Padding(4, 6, 4, 6);
            txt_XMLBassExisting.Name = "txt_XMLBassExisting";
            txt_XMLBassExisting.Size = new System.Drawing.Size(118, 20);
            txt_XMLBassExisting.TabIndex = 245;
            // 
            // txt_XMLComboNew
            // 
            txt_XMLComboNew.Cue = "XML Combo New";
            txt_XMLComboNew.Enabled = false;
            txt_XMLComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLComboNew.ForeColor = System.Drawing.Color.Gray;
            txt_XMLComboNew.Location = new System.Drawing.Point(130, 90);
            txt_XMLComboNew.Margin = new Padding(4, 6, 4, 6);
            txt_XMLComboNew.Name = "txt_XMLComboNew";
            txt_XMLComboNew.Size = new System.Drawing.Size(123, 20);
            txt_XMLComboNew.TabIndex = 246;
            txt_XMLComboNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_XMLComboExisting
            // 
            txt_XMLComboExisting.Cue = "XML Combo Existing";
            txt_XMLComboExisting.Enabled = false;
            txt_XMLComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLComboExisting.ForeColor = System.Drawing.Color.Gray;
            txt_XMLComboExisting.Location = new System.Drawing.Point(272, 89);
            txt_XMLComboExisting.Margin = new Padding(4, 6, 4, 6);
            txt_XMLComboExisting.Name = "txt_XMLComboExisting";
            txt_XMLComboExisting.Size = new System.Drawing.Size(117, 20);
            txt_XMLComboExisting.TabIndex = 247;
            // 
            // txt_XMLRhythmNew
            // 
            txt_XMLRhythmNew.Cue = "XML Rhythm New";
            txt_XMLRhythmNew.Enabled = false;
            txt_XMLRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLRhythmNew.ForeColor = System.Drawing.Color.Gray;
            txt_XMLRhythmNew.Location = new System.Drawing.Point(130, 112);
            txt_XMLRhythmNew.Margin = new Padding(4, 6, 4, 6);
            txt_XMLRhythmNew.Name = "txt_XMLRhythmNew";
            txt_XMLRhythmNew.Size = new System.Drawing.Size(122, 20);
            txt_XMLRhythmNew.TabIndex = 248;
            txt_XMLRhythmNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_XMLRhythmExisting
            // 
            txt_XMLRhythmExisting.Cue = "XML Rhythm Existing";
            txt_XMLRhythmExisting.Enabled = false;
            txt_XMLRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_XMLRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            txt_XMLRhythmExisting.Location = new System.Drawing.Point(272, 114);
            txt_XMLRhythmExisting.Margin = new Padding(4, 6, 4, 6);
            txt_XMLRhythmExisting.Name = "txt_XMLRhythmExisting";
            txt_XMLRhythmExisting.Size = new System.Drawing.Size(117, 20);
            txt_XMLRhythmExisting.TabIndex = 249;
            // 
            // txt_JSONLeadNew
            // 
            txt_JSONLeadNew.Cue = "JSON Lead New";
            txt_JSONLeadNew.Enabled = false;
            txt_JSONLeadNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONLeadNew.ForeColor = System.Drawing.Color.Gray;
            txt_JSONLeadNew.Location = new System.Drawing.Point(130, 135);
            txt_JSONLeadNew.Margin = new Padding(4, 6, 4, 6);
            txt_JSONLeadNew.Name = "txt_JSONLeadNew";
            txt_JSONLeadNew.Size = new System.Drawing.Size(122, 20);
            txt_JSONLeadNew.TabIndex = 252;
            txt_JSONLeadNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_JSONBassNew
            // 
            txt_JSONBassNew.Cue = "JSON Bass New";
            txt_JSONBassNew.Enabled = false;
            txt_JSONBassNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONBassNew.ForeColor = System.Drawing.Color.Gray;
            txt_JSONBassNew.Location = new System.Drawing.Point(130, 158);
            txt_JSONBassNew.Margin = new Padding(4, 6, 4, 6);
            txt_JSONBassNew.Name = "txt_JSONBassNew";
            txt_JSONBassNew.Size = new System.Drawing.Size(122, 20);
            txt_JSONBassNew.TabIndex = 254;
            txt_JSONBassNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_JSONBassExisting
            // 
            txt_JSONBassExisting.Cue = "JSON Bass Existing";
            txt_JSONBassExisting.Enabled = false;
            txt_JSONBassExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONBassExisting.ForeColor = System.Drawing.Color.Gray;
            txt_JSONBassExisting.Location = new System.Drawing.Point(272, 158);
            txt_JSONBassExisting.Margin = new Padding(4, 6, 4, 6);
            txt_JSONBassExisting.Name = "txt_JSONBassExisting";
            txt_JSONBassExisting.Size = new System.Drawing.Size(118, 20);
            txt_JSONBassExisting.TabIndex = 255;
            // 
            // txt_JSONComboNew
            // 
            txt_JSONComboNew.Cue = "JSON Combo New";
            txt_JSONComboNew.Enabled = false;
            txt_JSONComboNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONComboNew.ForeColor = System.Drawing.Color.Gray;
            txt_JSONComboNew.Location = new System.Drawing.Point(130, 185);
            txt_JSONComboNew.Margin = new Padding(4, 6, 4, 6);
            txt_JSONComboNew.Name = "txt_JSONComboNew";
            txt_JSONComboNew.Size = new System.Drawing.Size(122, 20);
            txt_JSONComboNew.TabIndex = 256;
            txt_JSONComboNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_JSONComboExisting
            // 
            txt_JSONComboExisting.Cue = "JSON Combo Existing";
            txt_JSONComboExisting.Enabled = false;
            txt_JSONComboExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONComboExisting.ForeColor = System.Drawing.Color.Gray;
            txt_JSONComboExisting.Location = new System.Drawing.Point(272, 182);
            txt_JSONComboExisting.Margin = new Padding(4, 6, 4, 6);
            txt_JSONComboExisting.Name = "txt_JSONComboExisting";
            txt_JSONComboExisting.Size = new System.Drawing.Size(117, 20);
            txt_JSONComboExisting.TabIndex = 257;
            // 
            // txt_JSONRhythmNew
            // 
            txt_JSONRhythmNew.Cue = "JSON Rhythm New";
            txt_JSONRhythmNew.Enabled = false;
            txt_JSONRhythmNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONRhythmNew.ForeColor = System.Drawing.Color.Gray;
            txt_JSONRhythmNew.Location = new System.Drawing.Point(130, 206);
            txt_JSONRhythmNew.Margin = new Padding(4, 6, 4, 6);
            txt_JSONRhythmNew.Name = "txt_JSONRhythmNew";
            txt_JSONRhythmNew.Size = new System.Drawing.Size(122, 20);
            txt_JSONRhythmNew.TabIndex = 259;
            txt_JSONRhythmNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_JSONRhythmExisting
            // 
            txt_JSONRhythmExisting.Cue = "JSON Rhythm Existing";
            txt_JSONRhythmExisting.Enabled = false;
            txt_JSONRhythmExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_JSONRhythmExisting.ForeColor = System.Drawing.Color.Gray;
            txt_JSONRhythmExisting.Location = new System.Drawing.Point(272, 204);
            txt_JSONRhythmExisting.Margin = new Padding(4, 6, 4, 6);
            txt_JSONRhythmExisting.Name = "txt_JSONRhythmExisting";
            txt_JSONRhythmExisting.Size = new System.Drawing.Size(117, 20);
            txt_JSONRhythmExisting.TabIndex = 260;
            // 
            // chbx_IsAlternateExisting
            // 
            chbx_IsAlternateExisting.Appearance = Appearance.Button;
            chbx_IsAlternateExisting.Location = new System.Drawing.Point(436, 189);
            chbx_IsAlternateExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_IsAlternateExisting.Name = "chbx_IsAlternateExisting";
            chbx_IsAlternateExisting.Size = new System.Drawing.Size(65, 25);
            chbx_IsAlternateExisting.TabIndex = 368;
            chbx_IsAlternateExisting.Text = "Alternate";
            chbx_IsAlternateExisting.UseVisualStyleBackColor = true;
            chbx_IsAlternateExisting.CheckedChanged += chbx_IsAlternateExisting_CheckedChanged;
            // 
            // txt_AlternateNoExisting
            // 
            txt_AlternateNoExisting.Location = new System.Drawing.Point(403, 190);
            txt_AlternateNoExisting.Margin = new Padding(2);
            txt_AlternateNoExisting.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            txt_AlternateNoExisting.Name = "txt_AlternateNoExisting";
            txt_AlternateNoExisting.Size = new System.Drawing.Size(32, 23);
            txt_AlternateNoExisting.TabIndex = 370;
            // 
            // txt_AlternateNoNew
            // 
            txt_AlternateNoNew.Location = new System.Drawing.Point(142, 191);
            txt_AlternateNoNew.Margin = new Padding(2);
            txt_AlternateNoNew.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            txt_AlternateNoNew.Name = "txt_AlternateNoNew";
            txt_AlternateNoNew.Size = new System.Drawing.Size(32, 23);
            txt_AlternateNoNew.TabIndex = 371;
            // 
            // chbx_MultiTrackExisting
            // 
            chbx_MultiTrackExisting.Appearance = Appearance.Button;
            chbx_MultiTrackExisting.Location = new System.Drawing.Point(362, 459);
            chbx_MultiTrackExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_MultiTrackExisting.Name = "chbx_MultiTrackExisting";
            chbx_MultiTrackExisting.Size = new System.Drawing.Size(72, 25);
            chbx_MultiTrackExisting.TabIndex = 372;
            chbx_MultiTrackExisting.Text = "MultiTrack";
            chbx_MultiTrackExisting.UseVisualStyleBackColor = true;
            chbx_MultiTrackExisting.CheckedChanged += chbx_MultiTrackExisting_CheckedChanged;
            // 
            // chbx_MultiTrackNew
            // 
            chbx_MultiTrackNew.Appearance = Appearance.Button;
            chbx_MultiTrackNew.Location = new System.Drawing.Point(134, 459);
            chbx_MultiTrackNew.Margin = new Padding(2, 3, 2, 3);
            chbx_MultiTrackNew.Name = "chbx_MultiTrackNew";
            chbx_MultiTrackNew.Size = new System.Drawing.Size(72, 25);
            chbx_MultiTrackNew.TabIndex = 373;
            chbx_MultiTrackNew.Text = "MultiTrack";
            chbx_MultiTrackNew.UseVisualStyleBackColor = true;
            chbx_MultiTrackNew.CheckedChanged += chbx_MultiTrackNew_CheckedChanged;
            // 
            // txt_MultiTrackNew
            // 
            txt_MultiTrackNew.Enabled = false;
            txt_MultiTrackNew.FormattingEnabled = true;
            txt_MultiTrackNew.Items.AddRange(new object[] { "No Bass", "No Lead", "No Rhythm", "No Guitar", "No Drums", "No Vocal", "No Guitars", "Only Bass", "Only Lead", "Only Rhythm", "Only Guitars", "Only Drums", "Only Vocal", "(Only BackTrack)", "(No Drums No Vocal)", "(No Guitar No Vocal)", "(No Bass No Vocal)" });
            txt_MultiTrackNew.Location = new System.Drawing.Point(205, 460);
            txt_MultiTrackNew.Margin = new Padding(2, 3, 2, 3);
            txt_MultiTrackNew.Name = "txt_MultiTrackNew";
            txt_MultiTrackNew.Size = new System.Drawing.Size(66, 23);
            txt_MultiTrackNew.TabIndex = 374;
            // 
            // txt_MultiTrackExisting
            // 
            txt_MultiTrackExisting.FormattingEnabled = true;
            txt_MultiTrackExisting.Items.AddRange(new object[] { "No Bass", "No Lead", "No Rhythm", "No Guitar", "No Drums", "No Vocal", "No Guitars", "Only Bass", "Only Lead", "Only Rhythm", "Only Guitars", "Only Drums", "Only Vocal", "(Only BackTrack)", "(No Drums No Vocal)", "(No Guitar No Vocal)", "(No Bass No Vocal)" });
            txt_MultiTrackExisting.Location = new System.Drawing.Point(295, 460);
            txt_MultiTrackExisting.Margin = new Padding(2, 3, 2, 3);
            txt_MultiTrackExisting.Name = "txt_MultiTrackExisting";
            txt_MultiTrackExisting.Size = new System.Drawing.Size(66, 23);
            txt_MultiTrackExisting.TabIndex = 375;
            // 
            // btn_CoverNew
            // 
            btn_CoverNew.Enabled = false;
            btn_CoverNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_CoverNew.Location = new System.Drawing.Point(648, 569);
            btn_CoverNew.Margin = new Padding(2);
            btn_CoverNew.Name = "btn_CoverNew";
            btn_CoverNew.Size = new System.Drawing.Size(18, 16);
            btn_CoverNew.TabIndex = 378;
            btn_CoverNew.Text = "<";
            btn_CoverNew.UseVisualStyleBackColor = true;
            btn_CoverNew.Click += btn_CoverNew_Click;
            // 
            // btn_CoverExisting
            // 
            btn_CoverExisting.Enabled = false;
            btn_CoverExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_CoverExisting.Location = new System.Drawing.Point(648, 601);
            btn_CoverExisting.Margin = new Padding(2);
            btn_CoverExisting.Name = "btn_CoverExisting";
            btn_CoverExisting.Size = new System.Drawing.Size(18, 16);
            btn_CoverExisting.TabIndex = 377;
            btn_CoverExisting.Text = ">";
            btn_CoverExisting.UseVisualStyleBackColor = true;
            btn_CoverExisting.Click += btn_CoverExisting_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(chbx_SlideNew);
            groupBox3.Controls.Add(chbx_SlideExisting);
            groupBox3.Controls.Add(txt_VocalsExisting);
            groupBox3.Controls.Add(txt_PreviewExisting);
            groupBox3.Controls.Add(txt_AudioExisting);
            groupBox3.Controls.Add(txt_AvailTracksExisting);
            groupBox3.Controls.Add(txt_DDExisting);
            groupBox3.Controls.Add(txt_VocalsNew);
            groupBox3.Controls.Add(txt_PreviewNew);
            groupBox3.Controls.Add(txt_AudioNew);
            groupBox3.Controls.Add(txt_AvailTracksNew);
            groupBox3.Controls.Add(txt_DDNew);
            groupBox3.Controls.Add(btn_GoImport);
            groupBox3.Controls.Add(chbx_CapoExisting);
            groupBox3.Controls.Add(chbx_MultiStringsNew);
            groupBox3.Controls.Add(chbx_CapoNew);
            groupBox3.Controls.Add(btn_WM_Vocals);
            groupBox3.Controls.Add(chbx_MultiStringsExisting);
            groupBox3.Controls.Add(lbl_previewFootnote);
            groupBox3.Controls.Add(btn_PlayPreviewNew);
            groupBox3.Controls.Add(btn_PlayAudioNew);
            groupBox3.Controls.Add(btn_PlayPreviewExisting);
            groupBox3.Controls.Add(btn_PlayAudioExisting);
            groupBox3.Controls.Add(btn_AddDD);
            groupBox3.Controls.Add(btn_AddTracks);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(lbl_AudioMain);
            groupBox3.Controls.Add(lbl_AudioPreview);
            groupBox3.Controls.Add(lbl_AvailTrk);
            groupBox3.Controls.Add(lbl_DD);
            groupBox3.Controls.Add(lbl_AvailableTracks);
            groupBox3.Controls.Add(lbl_Audio);
            groupBox3.Controls.Add(lbl_Preview);
            groupBox3.Controls.Add(lbl_txt_Vocals);
            groupBox3.Controls.Add(lbl_Vocals);
            groupBox3.Location = new System.Drawing.Point(22, 502);
            groupBox3.Margin = new Padding(2, 3, 2, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 3, 2, 3);
            groupBox3.Size = new System.Drawing.Size(480, 140);
            groupBox3.TabIndex = 379;
            groupBox3.TabStop = false;
            groupBox3.Text = "Comparisons based on Availability or Hash";
            // 
            // txt_VocalsExisting
            // 
            txt_VocalsExisting.Cue = "Vocals Existing";
            txt_VocalsExisting.Enabled = false;
            txt_VocalsExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VocalsExisting.ForeColor = System.Drawing.Color.Gray;
            txt_VocalsExisting.Location = new System.Drawing.Point(272, 122);
            txt_VocalsExisting.Margin = new Padding(4, 6, 4, 6);
            txt_VocalsExisting.Name = "txt_VocalsExisting";
            txt_VocalsExisting.Size = new System.Drawing.Size(53, 20);
            txt_VocalsExisting.TabIndex = 606;
            // 
            // txt_PreviewExisting
            // 
            txt_PreviewExisting.Cue = "Preview Existing";
            txt_PreviewExisting.Enabled = false;
            txt_PreviewExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PreviewExisting.ForeColor = System.Drawing.Color.Gray;
            txt_PreviewExisting.Location = new System.Drawing.Point(274, 100);
            txt_PreviewExisting.Margin = new Padding(4, 6, 4, 6);
            txt_PreviewExisting.Name = "txt_PreviewExisting";
            txt_PreviewExisting.Size = new System.Drawing.Size(51, 20);
            txt_PreviewExisting.TabIndex = 605;
            // 
            // txt_AudioExisting
            // 
            txt_AudioExisting.Cue = "Audio Existing";
            txt_AudioExisting.Enabled = false;
            txt_AudioExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioExisting.ForeColor = System.Drawing.Color.Gray;
            txt_AudioExisting.Location = new System.Drawing.Point(275, 73);
            txt_AudioExisting.Margin = new Padding(4, 6, 4, 6);
            txt_AudioExisting.Name = "txt_AudioExisting";
            txt_AudioExisting.Size = new System.Drawing.Size(52, 20);
            txt_AudioExisting.TabIndex = 604;
            // 
            // txt_AvailTracksExisting
            // 
            txt_AvailTracksExisting.Cue = "Avail. Tracks Existing";
            txt_AvailTracksExisting.Enabled = false;
            txt_AvailTracksExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AvailTracksExisting.ForeColor = System.Drawing.Color.Gray;
            txt_AvailTracksExisting.Location = new System.Drawing.Point(272, 45);
            txt_AvailTracksExisting.Margin = new Padding(4, 6, 4, 6);
            txt_AvailTracksExisting.Name = "txt_AvailTracksExisting";
            txt_AvailTracksExisting.Size = new System.Drawing.Size(55, 20);
            txt_AvailTracksExisting.TabIndex = 603;
            // 
            // txt_DDExisting
            // 
            txt_DDExisting.Cue = "DD Existing";
            txt_DDExisting.Enabled = false;
            txt_DDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DDExisting.ForeColor = System.Drawing.Color.Gray;
            txt_DDExisting.Location = new System.Drawing.Point(274, 20);
            txt_DDExisting.Margin = new Padding(4, 6, 4, 6);
            txt_DDExisting.Name = "txt_DDExisting";
            txt_DDExisting.Size = new System.Drawing.Size(53, 20);
            txt_DDExisting.TabIndex = 602;
            // 
            // txt_VocalsNew
            // 
            txt_VocalsNew.Cue = "Vocals New";
            txt_VocalsNew.Enabled = false;
            txt_VocalsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VocalsNew.ForeColor = System.Drawing.Color.Gray;
            txt_VocalsNew.Location = new System.Drawing.Point(206, 120);
            txt_VocalsNew.Margin = new Padding(4, 6, 4, 6);
            txt_VocalsNew.Name = "txt_VocalsNew";
            txt_VocalsNew.Size = new System.Drawing.Size(46, 20);
            txt_VocalsNew.TabIndex = 601;
            // 
            // txt_PreviewNew
            // 
            txt_PreviewNew.Cue = "Preview New";
            txt_PreviewNew.Enabled = false;
            txt_PreviewNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PreviewNew.ForeColor = System.Drawing.Color.Gray;
            txt_PreviewNew.Location = new System.Drawing.Point(206, 99);
            txt_PreviewNew.Margin = new Padding(4, 6, 4, 6);
            txt_PreviewNew.Name = "txt_PreviewNew";
            txt_PreviewNew.Size = new System.Drawing.Size(47, 20);
            txt_PreviewNew.TabIndex = 600;
            // 
            // txt_AudioNew
            // 
            txt_AudioNew.Cue = "Audio New";
            txt_AudioNew.Enabled = false;
            txt_AudioNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioNew.ForeColor = System.Drawing.Color.Gray;
            txt_AudioNew.Location = new System.Drawing.Point(206, 74);
            txt_AudioNew.Margin = new Padding(4, 6, 4, 6);
            txt_AudioNew.Name = "txt_AudioNew";
            txt_AudioNew.Size = new System.Drawing.Size(48, 20);
            txt_AudioNew.TabIndex = 599;
            // 
            // txt_AvailTracksNew
            // 
            txt_AvailTracksNew.Cue = "Avail. Tracks New";
            txt_AvailTracksNew.Enabled = false;
            txt_AvailTracksNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AvailTracksNew.ForeColor = System.Drawing.Color.Gray;
            txt_AvailTracksNew.Location = new System.Drawing.Point(206, 45);
            txt_AvailTracksNew.Margin = new Padding(4, 6, 4, 6);
            txt_AvailTracksNew.Name = "txt_AvailTracksNew";
            txt_AvailTracksNew.Size = new System.Drawing.Size(48, 20);
            txt_AvailTracksNew.TabIndex = 598;
            // 
            // txt_DDNew
            // 
            txt_DDNew.Cue = "DD New";
            txt_DDNew.Enabled = false;
            txt_DDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DDNew.ForeColor = System.Drawing.Color.Gray;
            txt_DDNew.Location = new System.Drawing.Point(206, 20);
            txt_DDNew.Margin = new Padding(4, 6, 4, 6);
            txt_DDNew.Name = "txt_DDNew";
            txt_DDNew.Size = new System.Drawing.Size(48, 20);
            txt_DDNew.TabIndex = 487;
            // 
            // btn_GoImport
            // 
            btn_GoImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_GoImport.Location = new System.Drawing.Point(430, 20);
            btn_GoImport.Margin = new Padding(2);
            btn_GoImport.Name = "btn_GoImport";
            btn_GoImport.Size = new System.Drawing.Size(22, 16);
            btn_GoImport.TabIndex = 476;
            btn_GoImport.Text = "<->";
            toolTip1.SetToolTip(btn_GoImport, "Open Importing CDLC Folder");
            btn_GoImport.UseVisualStyleBackColor = true;
            btn_GoImport.UseWaitCursor = true;
            // 
            // chbx_CapoExisting
            // 
            chbx_CapoExisting.Appearance = Appearance.Button;
            chbx_CapoExisting.Location = new System.Drawing.Point(331, 45);
            chbx_CapoExisting.Margin = new Padding(2);
            chbx_CapoExisting.Name = "chbx_CapoExisting";
            chbx_CapoExisting.Size = new System.Drawing.Size(46, 21);
            chbx_CapoExisting.TabIndex = 469;
            chbx_CapoExisting.Text = "Capo";
            chbx_CapoExisting.UseVisualStyleBackColor = true;
            chbx_CapoExisting.CheckedChanged += chbx_CapoExisting_CheckedChanged;
            // 
            // chbx_MultiStringsNew
            // 
            chbx_MultiStringsNew.Appearance = Appearance.Button;
            chbx_MultiStringsNew.Location = new System.Drawing.Point(124, 18);
            chbx_MultiStringsNew.Margin = new Padding(2);
            chbx_MultiStringsNew.Name = "chbx_MultiStringsNew";
            chbx_MultiStringsNew.Size = new System.Drawing.Size(81, 25);
            chbx_MultiStringsNew.TabIndex = 467;
            chbx_MultiStringsNew.Text = "MultiStrings";
            chbx_MultiStringsNew.UseVisualStyleBackColor = true;
            // 
            // chbx_CapoNew
            // 
            chbx_CapoNew.Appearance = Appearance.Button;
            chbx_CapoNew.Location = new System.Drawing.Point(159, 44);
            chbx_CapoNew.Margin = new Padding(2);
            chbx_CapoNew.Name = "chbx_CapoNew";
            chbx_CapoNew.Size = new System.Drawing.Size(46, 21);
            chbx_CapoNew.TabIndex = 470;
            chbx_CapoNew.Text = "Capo";
            chbx_CapoNew.UseVisualStyleBackColor = true;
            // 
            // btn_WM_Vocals
            // 
            btn_WM_Vocals.Enabled = false;
            btn_WM_Vocals.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_WM_Vocals.Location = new System.Drawing.Point(331, 123);
            btn_WM_Vocals.Margin = new Padding(2, 3, 2, 3);
            btn_WM_Vocals.Name = "btn_WM_Vocals";
            btn_WM_Vocals.Size = new System.Drawing.Size(28, 16);
            btn_WM_Vocals.TabIndex = 408;
            btn_WM_Vocals.Text = "WM";
            btn_WM_Vocals.UseVisualStyleBackColor = true;
            btn_WM_Vocals.Click += btn_WM_Vocals_Click;
            // 
            // chbx_MultiStringsExisting
            // 
            chbx_MultiStringsExisting.Appearance = Appearance.Button;
            chbx_MultiStringsExisting.Location = new System.Drawing.Point(328, 18);
            chbx_MultiStringsExisting.Margin = new Padding(2);
            chbx_MultiStringsExisting.Name = "chbx_MultiStringsExisting";
            chbx_MultiStringsExisting.Size = new System.Drawing.Size(81, 25);
            chbx_MultiStringsExisting.TabIndex = 465;
            chbx_MultiStringsExisting.Text = "MultiStrings";
            chbx_MultiStringsExisting.UseVisualStyleBackColor = true;
            // 
            // lbl_previewFootnote
            // 
            lbl_previewFootnote.BorderStyle = BorderStyle.FixedSingle;
            lbl_previewFootnote.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            lbl_previewFootnote.Location = new System.Drawing.Point(0, 100);
            lbl_previewFootnote.Margin = new Padding(2, 0, 2, 0);
            lbl_previewFootnote.Name = "lbl_previewFootnote";
            lbl_previewFootnote.Size = new System.Drawing.Size(69, 18);
            lbl_previewFootnote.TabIndex = 402;
            lbl_previewFootnote.Text = "Autom gen";
            lbl_previewFootnote.Visible = false;
            // 
            // btn_PlayPreviewNew
            // 
            btn_PlayPreviewNew.Enabled = false;
            btn_PlayPreviewNew.Location = new System.Drawing.Point(122, 96);
            btn_PlayPreviewNew.Margin = new Padding(2, 3, 2, 3);
            btn_PlayPreviewNew.Name = "btn_PlayPreviewNew";
            btn_PlayPreviewNew.Size = new System.Drawing.Size(83, 25);
            btn_PlayPreviewNew.TabIndex = 398;
            btn_PlayPreviewNew.Text = "Play Preview";
            btn_PlayPreviewNew.UseVisualStyleBackColor = true;
            btn_PlayPreviewNew.Click += btn_PlayPreviewNew_Click;
            // 
            // btn_PlayAudioNew
            // 
            btn_PlayAudioNew.Location = new System.Drawing.Point(122, 70);
            btn_PlayAudioNew.Margin = new Padding(2, 3, 2, 3);
            btn_PlayAudioNew.Name = "btn_PlayAudioNew";
            btn_PlayAudioNew.Size = new System.Drawing.Size(83, 25);
            btn_PlayAudioNew.TabIndex = 597;
            btn_PlayAudioNew.Text = "Play Audio";
            btn_PlayAudioNew.UseVisualStyleBackColor = true;
            btn_PlayAudioNew.Click += btn_PlayAudioNew_Click;
            // 
            // btn_PlayPreviewExisting
            // 
            btn_PlayPreviewExisting.Enabled = false;
            btn_PlayPreviewExisting.Font = new System.Drawing.Font("Calibri", 9F);
            btn_PlayPreviewExisting.Location = new System.Drawing.Point(328, 96);
            btn_PlayPreviewExisting.Margin = new Padding(2, 3, 2, 3);
            btn_PlayPreviewExisting.Name = "btn_PlayPreviewExisting";
            btn_PlayPreviewExisting.Size = new System.Drawing.Size(84, 26);
            btn_PlayPreviewExisting.TabIndex = 396;
            btn_PlayPreviewExisting.Text = "Play Preview";
            btn_PlayPreviewExisting.UseVisualStyleBackColor = true;
            btn_PlayPreviewExisting.Click += btn_PlayPreview_Click;
            // 
            // btn_PlayAudioExisting
            // 
            btn_PlayAudioExisting.Font = new System.Drawing.Font("Calibri", 9F);
            btn_PlayAudioExisting.Location = new System.Drawing.Point(328, 70);
            btn_PlayAudioExisting.Margin = new Padding(2, 3, 2, 3);
            btn_PlayAudioExisting.Name = "btn_PlayAudioExisting";
            btn_PlayAudioExisting.Size = new System.Drawing.Size(84, 25);
            btn_PlayAudioExisting.TabIndex = 395;
            btn_PlayAudioExisting.Text = "Play Audio";
            btn_PlayAudioExisting.UseVisualStyleBackColor = true;
            btn_PlayAudioExisting.Click += btn_PlayAudio_Click;
            // 
            // btn_AddDD
            // 
            btn_AddDD.BackColor = System.Drawing.SystemColors.Control;
            btn_AddDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddDD.Location = new System.Drawing.Point(416, 24);
            btn_AddDD.Margin = new Padding(2);
            btn_AddDD.Name = "btn_AddDD";
            btn_AddDD.Size = new System.Drawing.Size(14, 14);
            btn_AddDD.TabIndex = 386;
            btn_AddDD.Text = "+";
            btn_AddDD.UseVisualStyleBackColor = false;
            btn_AddDD.Click += btn_AddDD_Click;
            // 
            // btn_AddTracks
            // 
            btn_AddTracks.BackColor = System.Drawing.SystemColors.Control;
            btn_AddTracks.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddTracks.Location = new System.Drawing.Point(434, 47);
            btn_AddTracks.Margin = new Padding(2);
            btn_AddTracks.Name = "btn_AddTracks";
            btn_AddTracks.Size = new System.Drawing.Size(14, 14);
            btn_AddTracks.TabIndex = 385;
            btn_AddTracks.Text = "+";
            btn_AddTracks.UseVisualStyleBackColor = false;
            btn_AddTracks.Click += btn_AddInstruments_Click;
            // 
            // txt_DDNew_1
            // 
            txt_DDNew_1.Cue = "DD New";
            txt_DDNew_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DDNew_1.ForeColor = System.Drawing.Color.Gray;
            txt_DDNew_1.Location = new System.Drawing.Point(22, 32);
            txt_DDNew_1.Margin = new Padding(4, 6, 4, 6);
            txt_DDNew_1.Name = "txt_DDNew_1";
            txt_DDNew_1.Size = new System.Drawing.Size(80, 20);
            txt_DDNew_1.TabIndex = 234;
            txt_DDNew_1.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_DDExisting_1
            // 
            txt_DDExisting_1.Cue = "DD Existing";
            txt_DDExisting_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DDExisting_1.ForeColor = System.Drawing.Color.Gray;
            txt_DDExisting_1.Location = new System.Drawing.Point(48, 32);
            txt_DDExisting_1.Margin = new Padding(4, 6, 4, 6);
            txt_DDExisting_1.Name = "txt_DDExisting_1";
            txt_DDExisting_1.Size = new System.Drawing.Size(88, 20);
            txt_DDExisting_1.TabIndex = 235;
            // 
            // txt_AvailTracksNew_1
            // 
            txt_AvailTracksNew_1.Cue = "Available Tracks New";
            txt_AvailTracksNew_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            txt_AvailTracksNew_1.ForeColor = System.Drawing.Color.Gray;
            txt_AvailTracksNew_1.Location = new System.Drawing.Point(12, 80);
            txt_AvailTracksNew_1.Margin = new Padding(4, 6, 4, 6);
            txt_AvailTracksNew_1.Name = "txt_AvailTracksNew_1";
            txt_AvailTracksNew_1.Size = new System.Drawing.Size(80, 21);
            txt_AvailTracksNew_1.TabIndex = 236;
            txt_AvailTracksNew_1.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_AvailTracksExisting_1
            // 
            txt_AvailTracksExisting_1.Cue = "Available Tracks Existing";
            txt_AvailTracksExisting_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            txt_AvailTracksExisting_1.ForeColor = System.Drawing.Color.Gray;
            txt_AvailTracksExisting_1.Location = new System.Drawing.Point(48, 80);
            txt_AvailTracksExisting_1.Margin = new Padding(4, 6, 4, 6);
            txt_AvailTracksExisting_1.Name = "txt_AvailTracksExisting_1";
            txt_AvailTracksExisting_1.Size = new System.Drawing.Size(88, 21);
            txt_AvailTracksExisting_1.TabIndex = 237;
            // 
            // txt_AudioNew_1
            // 
            txt_AudioNew_1.Cue = "Audio New";
            txt_AudioNew_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioNew_1.ForeColor = System.Drawing.Color.Gray;
            txt_AudioNew_1.Location = new System.Drawing.Point(12, 32);
            txt_AudioNew_1.Margin = new Padding(4, 6, 4, 6);
            txt_AudioNew_1.Name = "txt_AudioNew_1";
            txt_AudioNew_1.Size = new System.Drawing.Size(80, 20);
            txt_AudioNew_1.TabIndex = 238;
            txt_AudioNew_1.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_AudioExisting_1
            // 
            txt_AudioExisting_1.Cue = "Audio Existing";
            txt_AudioExisting_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AudioExisting_1.ForeColor = System.Drawing.Color.Gray;
            txt_AudioExisting_1.Location = new System.Drawing.Point(548, 132);
            txt_AudioExisting_1.Margin = new Padding(4, 6, 4, 6);
            txt_AudioExisting_1.Name = "txt_AudioExisting_1";
            txt_AudioExisting_1.Size = new System.Drawing.Size(88, 20);
            txt_AudioExisting_1.TabIndex = 239;
            // 
            // txt_PreviewNew_1
            // 
            txt_PreviewNew_1.Cue = "Preview New";
            txt_PreviewNew_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PreviewNew_1.ForeColor = System.Drawing.Color.Gray;
            txt_PreviewNew_1.Location = new System.Drawing.Point(412, 180);
            txt_PreviewNew_1.Margin = new Padding(4, 6, 4, 6);
            txt_PreviewNew_1.Name = "txt_PreviewNew_1";
            txt_PreviewNew_1.Size = new System.Drawing.Size(80, 20);
            txt_PreviewNew_1.TabIndex = 242;
            txt_PreviewNew_1.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_PreviewExisting_1
            // 
            txt_PreviewExisting_1.Cue = "Preview Existing";
            txt_PreviewExisting_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PreviewExisting_1.ForeColor = System.Drawing.Color.Gray;
            txt_PreviewExisting_1.Location = new System.Drawing.Point(548, 180);
            txt_PreviewExisting_1.Margin = new Padding(4, 6, 4, 6);
            txt_PreviewExisting_1.Name = "txt_PreviewExisting_1";
            txt_PreviewExisting_1.Size = new System.Drawing.Size(88, 20);
            txt_PreviewExisting_1.TabIndex = 243;
            // 
            // txt_VocalsNew_1
            // 
            txt_VocalsNew_1.Cue = "Vocals New";
            txt_VocalsNew_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VocalsNew_1.ForeColor = System.Drawing.Color.Black;
            txt_VocalsNew_1.Location = new System.Drawing.Point(200, 100);
            txt_VocalsNew_1.Margin = new Padding(2, 0, 2, 0);
            txt_VocalsNew_1.Name = "txt_VocalsNew_1";
            txt_VocalsNew_1.Size = new System.Drawing.Size(80, 20);
            txt_VocalsNew_1.TabIndex = 397;
            txt_VocalsNew_1.Text = "kkkkkk";
            txt_VocalsNew_1.TextAlign = HorizontalAlignment.Right;
            txt_VocalsNew_1.Visible = false;
            // 
            // txt_VocalsExisting_1
            // 
            txt_VocalsExisting_1.Cue = "Vocals Existing";
            txt_VocalsExisting_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VocalsExisting_1.ForeColor = System.Drawing.Color.Gray;
            txt_VocalsExisting_1.Location = new System.Drawing.Point(48, 32);
            txt_VocalsExisting_1.Margin = new Padding(4, 6, 4, 6);
            txt_VocalsExisting_1.Name = "txt_VocalsExisting_1";
            txt_VocalsExisting_1.Size = new System.Drawing.Size(88, 20);
            txt_VocalsExisting_1.TabIndex = 340;
            txt_VocalsExisting_1.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(button5);
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(button3);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(lbl_CustomsForge_ReleaseNotes);
            groupBox4.Controls.Add(txt_CustomsForge_ReleaseNotesNew);
            groupBox4.Controls.Add(txt_CustomsForge_ReleaseNotesExisting);
            groupBox4.Controls.Add(label59);
            groupBox4.Controls.Add(txt_YouTube_LinkNew);
            groupBox4.Controls.Add(txt_CustomsForge_LinkExisting);
            groupBox4.Controls.Add(txt_YouTube_LinkExisting);
            groupBox4.Controls.Add(lbl_CustomsForge_Like);
            groupBox4.Controls.Add(txt_CustomsForge_LinkNew);
            groupBox4.Controls.Add(lbl_CustomsForge_LinkNew);
            groupBox4.Controls.Add(lbl_YouTube_LinkNew);
            groupBox4.Controls.Add(txt_CustomsForge_LikeNew);
            groupBox4.Controls.Add(lbfl_YouTube_Link);
            groupBox4.Controls.Add(txt_CustomsForge_LikeExisting);
            groupBox4.Controls.Add(label33);
            groupBox4.Controls.Add(label32);
            groupBox4.Location = new System.Drawing.Point(504, 669);
            groupBox4.Margin = new Padding(2, 3, 2, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(2, 3, 2, 3);
            groupBox4.Size = new System.Drawing.Size(315, 116);
            groupBox4.TabIndex = 380;
            groupBox4.TabStop = false;
            groupBox4.Text = "CustomsForge Details";
            // 
            // button5
            // 
            button5.Enabled = false;
            button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            button5.Location = new System.Drawing.Point(292, 90);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(18, 16);
            button5.TabIndex = 369;
            button5.Text = ">";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            button4.Location = new System.Drawing.Point(292, 70);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(18, 16);
            button4.TabIndex = 368;
            button4.Text = ">";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            button3.Location = new System.Drawing.Point(292, 46);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(18, 16);
            button3.TabIndex = 367;
            button3.Text = ">";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            button2.Location = new System.Drawing.Point(292, 18);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(18, 16);
            button2.TabIndex = 366;
            button2.Text = ">";
            button2.UseVisualStyleBackColor = true;
            // 
            // lbl_CustomsForge_ReleaseNotes
            // 
            lbl_CustomsForge_ReleaseNotes.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_CustomsForge_ReleaseNotes.Location = new System.Drawing.Point(176, 90);
            lbl_CustomsForge_ReleaseNotes.Margin = new Padding(2, 0, 2, 0);
            lbl_CustomsForge_ReleaseNotes.Name = "lbl_CustomsForge_ReleaseNotes";
            lbl_CustomsForge_ReleaseNotes.Size = new System.Drawing.Size(22, 15);
            lbl_CustomsForge_ReleaseNotes.TabIndex = 329;
            lbl_CustomsForge_ReleaseNotes.Text = "Vs.";
            // 
            // txt_CustomsForge_ReleaseNotesNew
            // 
            txt_CustomsForge_ReleaseNotesNew.Cue = "ReleaseNotes New";
            txt_CustomsForge_ReleaseNotesNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_ReleaseNotesNew.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_ReleaseNotesNew.Location = new System.Drawing.Point(94, 86);
            txt_CustomsForge_ReleaseNotesNew.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_ReleaseNotesNew.Name = "txt_CustomsForge_ReleaseNotesNew";
            txt_CustomsForge_ReleaseNotesNew.Size = new System.Drawing.Size(84, 20);
            txt_CustomsForge_ReleaseNotesNew.TabIndex = 326;
            txt_CustomsForge_ReleaseNotesNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_CustomsForge_ReleaseNotesExisting
            // 
            txt_CustomsForge_ReleaseNotesExisting.Cue = "ReleaseNotes Existing";
            txt_CustomsForge_ReleaseNotesExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_ReleaseNotesExisting.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_ReleaseNotesExisting.Location = new System.Drawing.Point(198, 87);
            txt_CustomsForge_ReleaseNotesExisting.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_ReleaseNotesExisting.Name = "txt_CustomsForge_ReleaseNotesExisting";
            txt_CustomsForge_ReleaseNotesExisting.Size = new System.Drawing.Size(94, 20);
            txt_CustomsForge_ReleaseNotesExisting.TabIndex = 327;
            txt_CustomsForge_ReleaseNotesExisting.TextChanged += ExistingChanged;
            // 
            // label59
            // 
            label59.Location = new System.Drawing.Point(8, 89);
            label59.Margin = new Padding(2, 0, 2, 0);
            label59.Name = "label59";
            label59.Size = new System.Drawing.Size(80, 15);
            label59.TabIndex = 328;
            label59.Text = "Release Notes";
            // 
            // txt_YouTube_LinkNew
            // 
            txt_YouTube_LinkNew.Cue = "YouTube Link New";
            txt_YouTube_LinkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_YouTube_LinkNew.ForeColor = System.Drawing.Color.Gray;
            txt_YouTube_LinkNew.Location = new System.Drawing.Point(96, 18);
            txt_YouTube_LinkNew.Margin = new Padding(4, 6, 4, 6);
            txt_YouTube_LinkNew.Name = "txt_YouTube_LinkNew";
            txt_YouTube_LinkNew.ScrollBars = ScrollBars.Horizontal;
            txt_YouTube_LinkNew.Size = new System.Drawing.Size(82, 20);
            txt_YouTube_LinkNew.TabIndex = 314;
            txt_YouTube_LinkNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_CustomsForge_LinkExisting
            // 
            txt_CustomsForge_LinkExisting.Cue = "CustomsForge Existing";
            txt_CustomsForge_LinkExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_LinkExisting.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_LinkExisting.Location = new System.Drawing.Point(198, 42);
            txt_CustomsForge_LinkExisting.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_LinkExisting.Name = "txt_CustomsForge_LinkExisting";
            txt_CustomsForge_LinkExisting.Size = new System.Drawing.Size(94, 20);
            txt_CustomsForge_LinkExisting.TabIndex = 317;
            txt_CustomsForge_LinkExisting.TextChanged += ExistingChanged;
            // 
            // txt_YouTube_LinkExisting
            // 
            txt_YouTube_LinkExisting.Cue = "YouTube Link Existing";
            txt_YouTube_LinkExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_YouTube_LinkExisting.ForeColor = System.Drawing.Color.Gray;
            txt_YouTube_LinkExisting.Location = new System.Drawing.Point(198, 19);
            txt_YouTube_LinkExisting.Margin = new Padding(4, 6, 4, 6);
            txt_YouTube_LinkExisting.Name = "txt_YouTube_LinkExisting";
            txt_YouTube_LinkExisting.Size = new System.Drawing.Size(96, 20);
            txt_YouTube_LinkExisting.TabIndex = 315;
            txt_YouTube_LinkExisting.TextChanged += ExistingChanged;
            // 
            // lbl_CustomsForge_Like
            // 
            lbl_CustomsForge_Like.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_CustomsForge_Like.Location = new System.Drawing.Point(176, 68);
            lbl_CustomsForge_Like.Margin = new Padding(2, 0, 2, 0);
            lbl_CustomsForge_Like.Name = "lbl_CustomsForge_Like";
            lbl_CustomsForge_Like.Size = new System.Drawing.Size(22, 15);
            lbl_CustomsForge_Like.TabIndex = 325;
            lbl_CustomsForge_Like.Text = "Vs.";
            // 
            // txt_CustomsForge_LinkNew
            // 
            txt_CustomsForge_LinkNew.Cue = "CustomsForge New";
            txt_CustomsForge_LinkNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_LinkNew.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_LinkNew.Location = new System.Drawing.Point(96, 42);
            txt_CustomsForge_LinkNew.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_LinkNew.Name = "txt_CustomsForge_LinkNew";
            txt_CustomsForge_LinkNew.Size = new System.Drawing.Size(82, 20);
            txt_CustomsForge_LinkNew.TabIndex = 316;
            txt_CustomsForge_LinkNew.TextAlign = HorizontalAlignment.Right;
            // 
            // lbl_CustomsForge_LinkNew
            // 
            lbl_CustomsForge_LinkNew.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_CustomsForge_LinkNew.Location = new System.Drawing.Point(176, 44);
            lbl_CustomsForge_LinkNew.Margin = new Padding(2, 0, 2, 0);
            lbl_CustomsForge_LinkNew.Name = "lbl_CustomsForge_LinkNew";
            lbl_CustomsForge_LinkNew.Size = new System.Drawing.Size(22, 15);
            lbl_CustomsForge_LinkNew.TabIndex = 324;
            lbl_CustomsForge_LinkNew.Text = "Vs.";
            // 
            // lbl_YouTube_LinkNew
            // 
            lbl_YouTube_LinkNew.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_YouTube_LinkNew.Location = new System.Drawing.Point(176, 21);
            lbl_YouTube_LinkNew.Margin = new Padding(2, 0, 2, 0);
            lbl_YouTube_LinkNew.Name = "lbl_YouTube_LinkNew";
            lbl_YouTube_LinkNew.Size = new System.Drawing.Size(22, 15);
            lbl_YouTube_LinkNew.TabIndex = 323;
            lbl_YouTube_LinkNew.Text = "Vs.";
            // 
            // txt_CustomsForge_LikeNew
            // 
            txt_CustomsForge_LikeNew.Cue = "Like New";
            txt_CustomsForge_LikeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_LikeNew.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_LikeNew.Location = new System.Drawing.Point(98, 62);
            txt_CustomsForge_LikeNew.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_LikeNew.Name = "txt_CustomsForge_LikeNew";
            txt_CustomsForge_LikeNew.Size = new System.Drawing.Size(80, 20);
            txt_CustomsForge_LikeNew.TabIndex = 318;
            txt_CustomsForge_LikeNew.TextAlign = HorizontalAlignment.Right;
            // 
            // lbfl_YouTube_Link
            // 
            lbfl_YouTube_Link.Location = new System.Drawing.Point(8, 22);
            lbfl_YouTube_Link.Margin = new Padding(2, 0, 2, 0);
            lbfl_YouTube_Link.Name = "lbfl_YouTube_Link";
            lbfl_YouTube_Link.Size = new System.Drawing.Size(53, 15);
            lbfl_YouTube_Link.TabIndex = 322;
            lbfl_YouTube_Link.Text = "YouTube";
            // 
            // txt_CustomsForge_LikeExisting
            // 
            txt_CustomsForge_LikeExisting.Cue = "Like Existing";
            txt_CustomsForge_LikeExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_CustomsForge_LikeExisting.ForeColor = System.Drawing.Color.Gray;
            txt_CustomsForge_LikeExisting.Location = new System.Drawing.Point(198, 64);
            txt_CustomsForge_LikeExisting.Margin = new Padding(4, 6, 4, 6);
            txt_CustomsForge_LikeExisting.Name = "txt_CustomsForge_LikeExisting";
            txt_CustomsForge_LikeExisting.Size = new System.Drawing.Size(96, 20);
            txt_CustomsForge_LikeExisting.TabIndex = 319;
            txt_CustomsForge_LikeExisting.TextChanged += ExistingChanged;
            // 
            // label33
            // 
            label33.Location = new System.Drawing.Point(8, 44);
            label33.Margin = new Padding(2, 0, 2, 0);
            label33.Name = "label33";
            label33.Size = new System.Drawing.Size(84, 15);
            label33.TabIndex = 321;
            label33.Text = "CustomsForge";
            // 
            // label32
            // 
            label32.Location = new System.Drawing.Point(9, 67);
            label32.Margin = new Padding(2, 0, 2, 0);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(28, 15);
            label32.TabIndex = 320;
            label32.Text = "Like";
            // 
            // btn_AddTunning
            // 
            btn_AddTunning.BackColor = System.Drawing.SystemColors.Control;
            btn_AddTunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddTunning.Location = new System.Drawing.Point(468, 488);
            btn_AddTunning.Margin = new Padding(2);
            btn_AddTunning.Name = "btn_AddTunning";
            btn_AddTunning.Size = new System.Drawing.Size(14, 14);
            btn_AddTunning.TabIndex = 387;
            btn_AddTunning.Text = "+";
            btn_AddTunning.UseVisualStyleBackColor = false;
            btn_AddTunning.Click += btn_AddTunning_Click;
            // 
            // btn_AddVersion1
            // 
            btn_AddVersion1.BackColor = System.Drawing.SystemColors.Control;
            btn_AddVersion1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddVersion1.Location = new System.Drawing.Point(376, 261);
            btn_AddVersion1.Margin = new Padding(2);
            btn_AddVersion1.Name = "btn_AddVersion1";
            btn_AddVersion1.Size = new System.Drawing.Size(14, 14);
            btn_AddVersion1.TabIndex = 388;
            btn_AddVersion1.Text = "+";
            btn_AddVersion1.UseVisualStyleBackColor = false;
            btn_AddVersion1.Click += btn_AddVersion_Click;
            // 
            // btn_AddAuthor
            // 
            btn_AddAuthor.BackColor = System.Drawing.SystemColors.Control;
            btn_AddAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddAuthor.Location = new System.Drawing.Point(456, 238);
            btn_AddAuthor.Margin = new Padding(2);
            btn_AddAuthor.Name = "btn_AddAuthor";
            btn_AddAuthor.Size = new System.Drawing.Size(14, 14);
            btn_AddAuthor.TabIndex = 389;
            btn_AddAuthor.Text = "+";
            btn_AddAuthor.UseVisualStyleBackColor = false;
            btn_AddAuthor.Click += btn_AddAuthor_Click;
            // 
            // lblSoye
            // 
            lblSoye.Location = new System.Drawing.Point(-1, 303);
            lblSoye.Margin = new Padding(2, 0, 2, 0);
            lblSoye.Name = "lblSoye";
            lblSoye.Size = new System.Drawing.Size(48, 15);
            lblSoye.TabIndex = 392;
            lblSoye.Text = "File Size";
            // 
            // lbl_Size
            // 
            lbl_Size.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Size.Location = new System.Drawing.Point(274, 305);
            lbl_Size.Margin = new Padding(2, 0, 2, 0);
            lbl_Size.Name = "lbl_Size";
            lbl_Size.Size = new System.Drawing.Size(22, 16);
            lbl_Size.TabIndex = 393;
            lbl_Size.Text = "Vs.";
            // 
            // lbl_Multitrack
            // 
            lbl_Multitrack.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_Multitrack.Location = new System.Drawing.Point(274, 464);
            lbl_Multitrack.Margin = new Padding(2, 0, 2, 0);
            lbl_Multitrack.Name = "lbl_Multitrack";
            lbl_Multitrack.Size = new System.Drawing.Size(22, 16);
            lbl_Multitrack.TabIndex = 394;
            lbl_Multitrack.Text = "Vs.";
            // 
            // chbx_UseBrakets
            // 
            chbx_UseBrakets.Checked = true;
            chbx_UseBrakets.CheckState = CheckState.Checked;
            chbx_UseBrakets.Location = new System.Drawing.Point(641, 372);
            chbx_UseBrakets.Margin = new Padding(2, 3, 2, 3);
            chbx_UseBrakets.Name = "chbx_UseBrakets";
            chbx_UseBrakets.Size = new System.Drawing.Size(174, 51);
            chbx_UseBrakets.TabIndex = 395;
            chbx_UseBrakets.Text = "Use Brackets for Additional Title/Metadata added info";
            chbx_UseBrakets.UseVisualStyleBackColor = true;
            // 
            // btn_AddAlternate
            // 
            btn_AddAlternate.BackColor = System.Drawing.SystemColors.Control;
            btn_AddAlternate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddAlternate.Location = new System.Drawing.Point(501, 195);
            btn_AddAlternate.Margin = new Padding(2);
            btn_AddAlternate.Name = "btn_AddAlternate";
            btn_AddAlternate.Size = new System.Drawing.Size(14, 14);
            btn_AddAlternate.TabIndex = 396;
            btn_AddAlternate.Text = "+";
            btn_AddAlternate.UseVisualStyleBackColor = false;
            btn_AddAlternate.Click += btn_AddAlternate_Click;
            // 
            // btn_StopImport
            // 
            btn_StopImport.Location = new System.Drawing.Point(522, 819);
            btn_StopImport.Margin = new Padding(2, 3, 2, 3);
            btn_StopImport.Name = "btn_StopImport";
            btn_StopImport.Size = new System.Drawing.Size(104, 40);
            btn_StopImport.TabIndex = 397;
            btn_StopImport.Text = "Stop the Import";
            btn_StopImport.UseVisualStyleBackColor = true;
            btn_StopImport.Click += btn_StopImport_Click;
            // 
            // chbx_DeleteTemp
            // 
            chbx_DeleteTemp.Enabled = false;
            chbx_DeleteTemp.Location = new System.Drawing.Point(641, 350);
            chbx_DeleteTemp.Margin = new Padding(2, 3, 2, 3);
            chbx_DeleteTemp.Name = "chbx_DeleteTemp";
            chbx_DeleteTemp.Size = new System.Drawing.Size(177, 19);
            chbx_DeleteTemp.TabIndex = 398;
            chbx_DeleteTemp.Text = "Delete Sikipped Songs Temp ";
            chbx_DeleteTemp.UseVisualStyleBackColor = true;
            // 
            // btn_Title2SortT
            // 
            btn_Title2SortT.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_Title2SortT.Location = new System.Drawing.Point(560, 94);
            btn_Title2SortT.Margin = new Padding(2);
            btn_Title2SortT.Name = "btn_Title2SortT";
            btn_Title2SortT.Size = new System.Drawing.Size(18, 16);
            btn_Title2SortT.TabIndex = 400;
            btn_Title2SortT.Text = ">";
            toolTip1.SetToolTip(btn_Title2SortT, "Replicate all Title to Title SORT");
            btn_Title2SortT.UseVisualStyleBackColor = true;
            btn_Title2SortT.Click += btn_Title2SortT_Click;
            // 
            // btn_Artist2SortA
            // 
            btn_Artist2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_Artist2SortA.Location = new System.Drawing.Point(560, 52);
            btn_Artist2SortA.Margin = new Padding(2);
            btn_Artist2SortA.Name = "btn_Artist2SortA";
            btn_Artist2SortA.Size = new System.Drawing.Size(18, 16);
            btn_Artist2SortA.TabIndex = 401;
            btn_Artist2SortA.Text = ">";
            toolTip1.SetToolTip(btn_Artist2SortA, "Replicate all Artist names to Artists SORT names");
            btn_Artist2SortA.UseVisualStyleBackColor = true;
            btn_Artist2SortA.Click += btn_Artist2SortA_Click;
            // 
            // chbx_Autosave
            // 
            chbx_Autosave.Checked = true;
            chbx_Autosave.CheckState = CheckState.Checked;
            chbx_Autosave.Location = new System.Drawing.Point(735, 32);
            chbx_Autosave.Margin = new Padding(2, 3, 2, 3);
            chbx_Autosave.Name = "chbx_Autosave";
            chbx_Autosave.Size = new System.Drawing.Size(76, 19);
            chbx_Autosave.TabIndex = 346;
            chbx_Autosave.Text = "AutoSave";
            chbx_Autosave.UseVisualStyleBackColor = true;
            chbx_Autosave.CheckedChanged += chbx_Autosave_CheckedChanged;
            // 
            // chbx_Sort
            // 
            chbx_Sort.Checked = true;
            chbx_Sort.CheckState = CheckState.Checked;
            chbx_Sort.Location = new System.Drawing.Point(522, 30);
            chbx_Sort.Margin = new Padding(2, 3, 2, 3);
            chbx_Sort.Name = "chbx_Sort";
            chbx_Sort.Size = new System.Drawing.Size(179, 19);
            chbx_Sort.TabIndex = 402;
            chbx_Sort.Text = "Title and Artist sync with Sort";
            chbx_Sort.UseVisualStyleBackColor = true;
            // 
            // chbx_LiveExisting
            // 
            chbx_LiveExisting.Appearance = Appearance.Button;
            chbx_LiveExisting.Location = new System.Drawing.Point(431, 459);
            chbx_LiveExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_LiveExisting.Name = "chbx_LiveExisting";
            chbx_LiveExisting.Size = new System.Drawing.Size(38, 25);
            chbx_LiveExisting.TabIndex = 407;
            chbx_LiveExisting.Text = "Live";
            chbx_LiveExisting.UseVisualStyleBackColor = true;
            chbx_LiveExisting.CheckedChanged += chbx_LiveExisting_CheckedChanged;
            // 
            // chbx_LiveNew
            // 
            chbx_LiveNew.Appearance = Appearance.Button;
            chbx_LiveNew.Location = new System.Drawing.Point(98, 459);
            chbx_LiveNew.Margin = new Padding(2, 3, 2, 3);
            chbx_LiveNew.Name = "chbx_LiveNew";
            chbx_LiveNew.Size = new System.Drawing.Size(38, 25);
            chbx_LiveNew.TabIndex = 409;
            chbx_LiveNew.Text = "Live";
            chbx_LiveNew.UseVisualStyleBackColor = true;
            chbx_LiveNew.CheckedChanged += chbx_LiveNew_CheckedChanged;
            // 
            // btn_AddPlatform
            // 
            btn_AddPlatform.BackColor = System.Drawing.SystemColors.Control;
            btn_AddPlatform.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AddPlatform.Location = new System.Drawing.Point(465, 216);
            btn_AddPlatform.Margin = new Padding(2);
            btn_AddPlatform.Name = "btn_AddPlatform";
            btn_AddPlatform.Size = new System.Drawing.Size(14, 14);
            btn_AddPlatform.TabIndex = 411;
            btn_AddPlatform.Text = "+";
            btn_AddPlatform.UseVisualStyleBackColor = false;
            btn_AddPlatform.Click += btn_AddPlatform_Click_1;
            // 
            // chbx_AcousticExisting
            // 
            chbx_AcousticExisting.Appearance = Appearance.Button;
            chbx_AcousticExisting.Location = new System.Drawing.Point(294, 356);
            chbx_AcousticExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_AcousticExisting.Name = "chbx_AcousticExisting";
            chbx_AcousticExisting.Size = new System.Drawing.Size(63, 25);
            chbx_AcousticExisting.TabIndex = 413;
            chbx_AcousticExisting.Text = "Acoustic";
            chbx_AcousticExisting.UseVisualStyleBackColor = true;
            chbx_AcousticExisting.CheckedChanged += chbx_LiveExisting_CheckedChanged;
            // 
            // chbx_AcousticNew
            // 
            chbx_AcousticNew.Appearance = Appearance.Button;
            chbx_AcousticNew.Location = new System.Drawing.Point(212, 356);
            chbx_AcousticNew.Margin = new Padding(2, 3, 2, 3);
            chbx_AcousticNew.Name = "chbx_AcousticNew";
            chbx_AcousticNew.Size = new System.Drawing.Size(63, 25);
            chbx_AcousticNew.TabIndex = 414;
            chbx_AcousticNew.Text = "Acoustic";
            chbx_AcousticNew.UseVisualStyleBackColor = true;
            chbx_AcousticNew.CheckedChanged += chbx_LiveNew_CheckedChanged;
            // 
            // btn_NotADuplicate
            // 
            btn_NotADuplicate.Font = new System.Drawing.Font("Segoe UI", 8F);
            btn_NotADuplicate.Location = new System.Drawing.Point(682, 152);
            btn_NotADuplicate.Margin = new Padding(2, 3, 2, 3);
            btn_NotADuplicate.Name = "btn_NotADuplicate";
            btn_NotADuplicate.Size = new System.Drawing.Size(129, 34);
            btn_NotADuplicate.TabIndex = 415;
            btn_NotADuplicate.Text = "(neither) NOT a Duplicate";
            toolTip1.SetToolTip(btn_NotADuplicate, "Consider the current song as not a duplicate (NOT the same Artist and Title, OR DLCName)");
            btn_NotADuplicate.UseVisualStyleBackColor = true;
            btn_NotADuplicate.Click += btn_NotADuplicate_Click;
            // 
            // btn_AddStandard
            // 
            btn_AddStandard.Font = new System.Drawing.Font("Calibri", 9F);
            btn_AddStandard.Location = new System.Drawing.Point(582, 132);
            btn_AddStandard.Margin = new Padding(2, 3, 2, 3);
            btn_AddStandard.Name = "btn_AddStandard";
            btn_AddStandard.Size = new System.Drawing.Size(93, 52);
            btn_AddStandard.TabIndex = 416;
            btn_AddStandard.Text = "Add Standard Differences";
            toolTip1.SetToolTip(btn_AddStandard, "Add set of differences like Instruments, author, DD, Tuning");
            btn_AddStandard.UseVisualStyleBackColor = true;
            btn_AddStandard.Click += btn_AddStandard_Click;
            // 
            // btn_AlbumSortNew
            // 
            btn_AlbumSortNew.Enabled = false;
            btn_AlbumSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AlbumSortNew.Location = new System.Drawing.Point(520, 154);
            btn_AlbumSortNew.Margin = new Padding(2);
            btn_AlbumSortNew.Name = "btn_AlbumSortNew";
            btn_AlbumSortNew.Size = new System.Drawing.Size(18, 16);
            btn_AlbumSortNew.TabIndex = 425;
            btn_AlbumSortNew.Text = "<";
            toolTip1.SetToolTip(btn_AlbumSortNew, "Overrite New Artist SORT name with Existing Artist SORT name");
            btn_AlbumSortNew.UseVisualStyleBackColor = true;
            btn_AlbumSortNew.Click += Btn_AlbumSortNew_Click;
            // 
            // btn_AlbumSortExisting
            // 
            btn_AlbumSortExisting.Enabled = false;
            btn_AlbumSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_AlbumSortExisting.Location = new System.Drawing.Point(538, 154);
            btn_AlbumSortExisting.Margin = new Padding(2);
            btn_AlbumSortExisting.Name = "btn_AlbumSortExisting";
            btn_AlbumSortExisting.Size = new System.Drawing.Size(18, 16);
            btn_AlbumSortExisting.TabIndex = 424;
            btn_AlbumSortExisting.Text = ">";
            toolTip1.SetToolTip(btn_AlbumSortExisting, "Overrite Existing Artist SORT name with New Artist SORT name");
            btn_AlbumSortExisting.UseVisualStyleBackColor = true;
            btn_AlbumSortExisting.Click += Btn_AlbumSortExisting_Click;
            // 
            // btn_Album2SortA
            // 
            btn_Album2SortA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_Album2SortA.Location = new System.Drawing.Point(560, 73);
            btn_Album2SortA.Margin = new Padding(2);
            btn_Album2SortA.Name = "btn_Album2SortA";
            btn_Album2SortA.Size = new System.Drawing.Size(18, 16);
            btn_Album2SortA.TabIndex = 428;
            btn_Album2SortA.Text = ">";
            toolTip1.SetToolTip(btn_Album2SortA, "Replicate all Artist names to Artists SORT names");
            btn_Album2SortA.UseVisualStyleBackColor = true;
            btn_Album2SortA.Click += Btn_Album2SortA_Click;
            // 
            // btn_CommentSimilar
            // 
            btn_CommentSimilar.Font = new System.Drawing.Font("Calibri", 9F);
            btn_CommentSimilar.Location = new System.Drawing.Point(560, 203);
            btn_CommentSimilar.Margin = new Padding(2, 3, 2, 3);
            btn_CommentSimilar.Name = "btn_CommentSimilar";
            btn_CommentSimilar.Size = new System.Drawing.Size(92, 48);
            btn_CommentSimilar.TabIndex = 430;
            btn_CommentSimilar.Text = "Add comment cause Similar";
            toolTip1.SetToolTip(btn_CommentSimilar, "Add set of differences like Instruments, author, DD, Tuning");
            btn_CommentSimilar.UseVisualStyleBackColor = true;
            btn_CommentSimilar.Click += btn_CommentSimilar_Click;
            // 
            // lbl_FileHash
            // 
            lbl_FileHash.BorderStyle = BorderStyle.FixedSingle;
            lbl_FileHash.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lbl_FileHash.Location = new System.Drawing.Point(486, 213);
            lbl_FileHash.Margin = new Padding(2, 0, 2, 0);
            lbl_FileHash.Name = "lbl_FileHash";
            lbl_FileHash.Size = new System.Drawing.Size(65, 17);
            lbl_FileHash.TabIndex = 409;
            lbl_FileHash.Text = "SameHash";
            toolTip1.SetToolTip(lbl_FileHash, "Both Existing and about to be imported files have the same Hash. Option 79 must have been selected");
            lbl_FileHash.Visible = false;
            // 
            // btn_OpenStandardization
            // 
            btn_OpenStandardization.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
            btn_OpenStandardization.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            btn_OpenStandardization.Location = new System.Drawing.Point(630, 817);
            btn_OpenStandardization.Margin = new Padding(2);
            btn_OpenStandardization.Name = "btn_OpenStandardization";
            btn_OpenStandardization.Size = new System.Drawing.Size(88, 43);
            btn_OpenStandardization.TabIndex = 431;
            btn_OpenStandardization.Text = "Open Standarization DB";
            toolTip1.SetToolTip(btn_OpenStandardization, "Open screen to manage textual corrections and standardization (Song Title, Year, etc.)");
            btn_OpenStandardization.UseVisualStyleBackColor = false;
            btn_OpenStandardization.Click += btn_OpenStandardization_Click;
            // 
            // chbx_DescriptionSave
            // 
            chbx_DescriptionSave.Enabled = false;
            chbx_DescriptionSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            chbx_DescriptionSave.Location = new System.Drawing.Point(580, 73);
            chbx_DescriptionSave.Margin = new Padding(2, 3, 2, 3);
            chbx_DescriptionSave.Name = "chbx_DescriptionSave";
            chbx_DescriptionSave.Size = new System.Drawing.Size(62, 14);
            chbx_DescriptionSave.TabIndex = 451;
            chbx_DescriptionSave.Text = "DescrSave";
            toolTip1.SetToolTip(chbx_DescriptionSave, "Copy Replaced Text into Description");
            chbx_DescriptionSave.UseVisualStyleBackColor = true;
            // 
            // lbl_Covers
            // 
            lbl_Covers.Location = new System.Drawing.Point(508, 511);
            lbl_Covers.Margin = new Padding(2, 0, 2, 0);
            lbl_Covers.Name = "lbl_Covers";
            lbl_Covers.Size = new System.Drawing.Size(46, 15);
            lbl_Covers.TabIndex = 409;
            lbl_Covers.Text = "Covers:";
            toolTip1.SetToolTip(lbl_Covers, "When green Strong Indication of Having the same Source, since Hash is the same.");
            lbl_Covers.Click += lbl_Covers_Click;
            // 
            // lbl_Attention
            // 
            lbl_Attention.Font = new System.Drawing.Font("Segoe UI", 10.875F);
            lbl_Attention.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            lbl_Attention.Location = new System.Drawing.Point(648, 410);
            lbl_Attention.Margin = new Padding(2, 0, 2, 0);
            lbl_Attention.Name = "lbl_Attention";
            lbl_Attention.Size = new System.Drawing.Size(162, 116);
            lbl_Attention.TabIndex = 464;
            lbl_Attention.Text = "Attention some-things are identical";
            toolTip1.SetToolTip(lbl_Attention, "Note either Audoi/Preivew/Cover hash or Lenght are the same sugegsting same author or same source");
            lbl_Attention.Click += lbl_Atention_Click;
            // 
            // chbx_MedleyExisting
            // 
            chbx_MedleyExisting.Appearance = Appearance.Button;
            chbx_MedleyExisting.Location = new System.Drawing.Point(475, 384);
            chbx_MedleyExisting.Margin = new Padding(2);
            chbx_MedleyExisting.Name = "chbx_MedleyExisting";
            chbx_MedleyExisting.Size = new System.Drawing.Size(56, 25);
            chbx_MedleyExisting.TabIndex = 466;
            chbx_MedleyExisting.Text = "Medley";
            toolTip1.SetToolTip(chbx_MedleyExisting, "Medley Existing Flag");
            chbx_MedleyExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_DeluxeNew
            // 
            chbx_DeluxeNew.Appearance = Appearance.Button;
            chbx_DeluxeNew.Location = new System.Drawing.Point(80, 433);
            chbx_DeluxeNew.Margin = new Padding(2, 3, 2, 3);
            chbx_DeluxeNew.Name = "chbx_DeluxeNew";
            chbx_DeluxeNew.Size = new System.Drawing.Size(53, 25);
            chbx_DeluxeNew.TabIndex = 470;
            chbx_DeluxeNew.Text = "Deluxe";
            toolTip1.SetToolTip(chbx_DeluxeNew, "New Deluxe flag");
            chbx_DeluxeNew.UseVisualStyleBackColor = true;
            // 
            // chbx_GreatestHitsNew
            // 
            chbx_GreatestHitsNew.Appearance = Appearance.Button;
            chbx_GreatestHitsNew.Location = new System.Drawing.Point(4, 433);
            chbx_GreatestHitsNew.Margin = new Padding(2, 3, 2, 3);
            chbx_GreatestHitsNew.Name = "chbx_GreatestHitsNew";
            chbx_GreatestHitsNew.Size = new System.Drawing.Size(81, 25);
            chbx_GreatestHitsNew.TabIndex = 469;
            chbx_GreatestHitsNew.Text = "GreatestHits";
            toolTip1.SetToolTip(chbx_GreatestHitsNew, "New Greatest Hits flag");
            chbx_GreatestHitsNew.UseVisualStyleBackColor = true;
            // 
            // chbx_DeluxeExisting
            // 
            chbx_DeluxeExisting.Appearance = Appearance.Button;
            chbx_DeluxeExisting.Location = new System.Drawing.Point(440, 433);
            chbx_DeluxeExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_DeluxeExisting.Name = "chbx_DeluxeExisting";
            chbx_DeluxeExisting.Size = new System.Drawing.Size(53, 25);
            chbx_DeluxeExisting.TabIndex = 472;
            chbx_DeluxeExisting.Text = "Deluxe";
            toolTip1.SetToolTip(chbx_DeluxeExisting, "Existing Deluxe flag");
            chbx_DeluxeExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_GreatestHitsExisting
            // 
            chbx_GreatestHitsExisting.Appearance = Appearance.Button;
            chbx_GreatestHitsExisting.Location = new System.Drawing.Point(491, 433);
            chbx_GreatestHitsExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_GreatestHitsExisting.Name = "chbx_GreatestHitsExisting";
            chbx_GreatestHitsExisting.Size = new System.Drawing.Size(81, 25);
            chbx_GreatestHitsExisting.TabIndex = 471;
            chbx_GreatestHitsExisting.Text = "GreatestHits";
            toolTip1.SetToolTip(chbx_GreatestHitsExisting, "Existing Greatest Hits flag");
            chbx_GreatestHitsExisting.UseVisualStyleBackColor = true;
            // 
            // btn_Replace_Brakets
            // 
            btn_Replace_Brakets.Font = new System.Drawing.Font("Calibri", 5F);
            btn_Replace_Brakets.Location = new System.Drawing.Point(582, 94);
            btn_Replace_Brakets.Margin = new Padding(2);
            btn_Replace_Brakets.Name = "btn_Replace_Brakets";
            btn_Replace_Brakets.Size = new System.Drawing.Size(22, 16);
            btn_Replace_Brakets.TabIndex = 473;
            btn_Replace_Brakets.Text = "()=[]";
            toolTip1.SetToolTip(btn_Replace_Brakets, "Replaces () with []");
            btn_Replace_Brakets.UseVisualStyleBackColor = true;
            btn_Replace_Brakets.Click += btn_Replace_Brakets_Click;
            // 
            // chbx_GameSoundtrackNew
            // 
            chbx_GameSoundtrackNew.Appearance = Appearance.Button;
            chbx_GameSoundtrackNew.Location = new System.Drawing.Point(165, 434);
            chbx_GameSoundtrackNew.Margin = new Padding(2, 3, 2, 3);
            chbx_GameSoundtrackNew.Name = "chbx_GameSoundtrackNew";
            chbx_GameSoundtrackNew.Size = new System.Drawing.Size(108, 25);
            chbx_GameSoundtrackNew.TabIndex = 474;
            chbx_GameSoundtrackNew.Text = "GameSoundtrack";
            toolTip1.SetToolTip(chbx_GameSoundtrackNew, "New Greatest Hits flag");
            chbx_GameSoundtrackNew.UseVisualStyleBackColor = true;
            // 
            // btn_OpenNewFolder
            // 
            btn_OpenNewFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            btn_OpenNewFolder.Location = new System.Drawing.Point(-2, 50);
            btn_OpenNewFolder.Margin = new Padding(2);
            btn_OpenNewFolder.Name = "btn_OpenNewFolder";
            btn_OpenNewFolder.Size = new System.Drawing.Size(22, 16);
            btn_OpenNewFolder.TabIndex = 478;
            btn_OpenNewFolder.Text = "<->";
            toolTip1.SetToolTip(btn_OpenNewFolder, "Open Importing CDLC Folder");
            btn_OpenNewFolder.UseVisualStyleBackColor = true;
            btn_OpenNewFolder.UseWaitCursor = true;
            btn_OpenNewFolder.Click += btn_OpenNewFolder_Click;
            // 
            // chbx_MetalExisting
            // 
            chbx_MetalExisting.Appearance = Appearance.Button;
            chbx_MetalExisting.Location = new System.Drawing.Point(532, 384);
            chbx_MetalExisting.Margin = new Padding(2);
            chbx_MetalExisting.Name = "chbx_MetalExisting";
            chbx_MetalExisting.Size = new System.Drawing.Size(77, 25);
            chbx_MetalExisting.TabIndex = 488;
            chbx_MetalExisting.Text = "MetalCover";
            toolTip1.SetToolTip(chbx_MetalExisting, "Medley Existing Flag");
            chbx_MetalExisting.UseVisualStyleBackColor = true;
            // 
            // btn_SaveExisting
            // 
            btn_SaveExisting.Location = new System.Drawing.Point(522, 790);
            btn_SaveExisting.Margin = new Padding(2, 3, 2, 3);
            btn_SaveExisting.Name = "btn_SaveExisting";
            btn_SaveExisting.Size = new System.Drawing.Size(156, 26);
            btn_SaveExisting.TabIndex = 495;
            btn_SaveExisting.Text = "Save Existing Metadata";
            toolTip1.SetToolTip(btn_SaveExisting, "Cear away any added");
            btn_SaveExisting.UseVisualStyleBackColor = true;
            btn_SaveExisting.Click += button6_Click_4;
            // 
            // lbl_LenghtExisting
            // 
            lbl_LenghtExisting.Location = new System.Drawing.Point(531, 464);
            lbl_LenghtExisting.Margin = new Padding(2, 0, 2, 0);
            lbl_LenghtExisting.Name = "lbl_LenghtExisting";
            lbl_LenghtExisting.Size = new System.Drawing.Size(44, 15);
            lbl_LenghtExisting.TabIndex = 412;
            lbl_LenghtExisting.Text = "Lenght";
            // 
            // btn_ShowInfoOthers
            // 
            btn_ShowInfoOthers.Enabled = false;
            btn_ShowInfoOthers.Font = new System.Drawing.Font("Calibri", 9F);
            btn_ShowInfoOthers.Location = new System.Drawing.Point(396, 8);
            btn_ShowInfoOthers.Margin = new Padding(2, 3, 2, 3);
            btn_ShowInfoOthers.Name = "btn_ShowInfoOthers";
            btn_ShowInfoOthers.Size = new System.Drawing.Size(167, 19);
            btn_ShowInfoOthers.TabIndex = 417;
            btn_ShowInfoOthers.Text = "Show the other Duplicates info";
            btn_ShowInfoOthers.UseVisualStyleBackColor = true;
            btn_ShowInfoOthers.Click += btn_ShowInfoOthers_Click;
            // 
            // label13
            // 
            label13.Location = new System.Drawing.Point(22, 152);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(54, 15);
            label13.TabIndex = 423;
            label13.Text = "/Alb Sort";
            // 
            // lbl_AlbumSort
            // 
            lbl_AlbumSort.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_AlbumSort.Location = new System.Drawing.Point(274, 152);
            lbl_AlbumSort.Margin = new Padding(2, 0, 2, 0);
            lbl_AlbumSort.Name = "lbl_AlbumSort";
            lbl_AlbumSort.Size = new System.Drawing.Size(22, 16);
            lbl_AlbumSort.TabIndex = 422;
            lbl_AlbumSort.Text = "Vs.";
            // 
            // txt_AlbumSortExisting
            // 
            txt_AlbumSortExisting.Cue = "Album Sort Existing";
            txt_AlbumSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumSortExisting.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumSortExisting.Location = new System.Drawing.Point(343, 148);
            txt_AlbumSortExisting.Margin = new Padding(4, 6, 4, 6);
            txt_AlbumSortExisting.Name = "txt_AlbumSortExisting";
            txt_AlbumSortExisting.Size = new System.Drawing.Size(179, 20);
            txt_AlbumSortExisting.TabIndex = 421;
            txt_AlbumSortExisting.TextChanged += ExistingChanged;
            // 
            // txt_AlbumSortNew
            // 
            txt_AlbumSortNew.Cue = "Album Sort New";
            txt_AlbumSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumSortNew.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumSortNew.Location = new System.Drawing.Point(74, 150);
            txt_AlbumSortNew.Margin = new Padding(4, 6, 4, 6);
            txt_AlbumSortNew.Name = "txt_AlbumSortNew";
            txt_AlbumSortNew.Size = new System.Drawing.Size(154, 20);
            txt_AlbumSortNew.TabIndex = 420;
            txt_AlbumSortNew.TextAlign = HorizontalAlignment.Right;
            txt_AlbumSortNew.TextChanged += ExistingChanged;
            // 
            // txt_YearNew
            // 
            txt_YearNew.Cue = "Year New";
            txt_YearNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_YearNew.ForeColor = System.Drawing.Color.Gray;
            txt_YearNew.Location = new System.Drawing.Point(230, 150);
            txt_YearNew.Margin = new Padding(4, 6, 4, 6);
            txt_YearNew.Name = "txt_YearNew";
            txt_YearNew.Size = new System.Drawing.Size(44, 20);
            txt_YearNew.TabIndex = 419;
            txt_YearNew.TextChanged += ExistingChanged;
            // 
            // txt_YearExisting
            // 
            txt_YearExisting.Cue = "Year Existing";
            txt_YearExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_YearExisting.ForeColor = System.Drawing.Color.Gray;
            txt_YearExisting.Location = new System.Drawing.Point(298, 148);
            txt_YearExisting.Margin = new Padding(4, 6, 4, 6);
            txt_YearExisting.Name = "txt_YearExisting";
            txt_YearExisting.Size = new System.Drawing.Size(44, 20);
            txt_YearExisting.TabIndex = 418;
            txt_YearExisting.TextChanged += ExistingChanged;
            // 
            // txt_LenghtNew
            // 
            txt_LenghtNew.Cue = "Available Tracks New";
            txt_LenghtNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            txt_LenghtNew.ForeColor = System.Drawing.Color.Black;
            txt_LenghtNew.Location = new System.Drawing.Point(44, 460);
            txt_LenghtNew.Margin = new Padding(4, 6, 4, 6);
            txt_LenghtNew.Name = "txt_LenghtNew";
            txt_LenghtNew.ReadOnly = true;
            txt_LenghtNew.Size = new System.Drawing.Size(51, 21);
            txt_LenghtNew.TabIndex = 409;
            txt_LenghtNew.Text = "Lenght";
            txt_LenghtNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_LenghtExisting
            // 
            txt_LenghtExisting.Cue = "Available Tracks New";
            txt_LenghtExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            txt_LenghtExisting.ForeColor = System.Drawing.Color.Black;
            txt_LenghtExisting.Location = new System.Drawing.Point(470, 460);
            txt_LenghtExisting.Margin = new Padding(4, 6, 4, 6);
            txt_LenghtExisting.Name = "txt_LenghtExisting";
            txt_LenghtExisting.ReadOnly = true;
            txt_LenghtExisting.Size = new System.Drawing.Size(61, 21);
            txt_LenghtExisting.TabIndex = 411;
            txt_LenghtExisting.Text = "Lenght";
            txt_LenghtExisting.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_VersionExisting
            // 
            txt_VersionExisting.Cue = "Version Existing";
            txt_VersionExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VersionExisting.ForeColor = System.Drawing.Color.Gray;
            txt_VersionExisting.Location = new System.Drawing.Point(297, 257);
            txt_VersionExisting.Margin = new Padding(4, 6, 4, 6);
            txt_VersionExisting.Name = "txt_VersionExisting";
            txt_VersionExisting.Size = new System.Drawing.Size(80, 20);
            txt_VersionExisting.TabIndex = 412;
            txt_VersionExisting.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_VersionNew
            // 
            txt_VersionNew.Cue = "Version New";
            txt_VersionNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_VersionNew.ForeColor = System.Drawing.Color.Gray;
            txt_VersionNew.Location = new System.Drawing.Point(204, 258);
            txt_VersionNew.Margin = new Padding(4, 6, 4, 6);
            txt_VersionNew.Name = "txt_VersionNew";
            txt_VersionNew.Size = new System.Drawing.Size(71, 20);
            txt_VersionNew.TabIndex = 409;
            txt_VersionNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_LiveDetailsNew
            // 
            txt_LiveDetailsNew.Cue = "Live/Acoustic Details New";
            txt_LiveDetailsNew.Enabled = false;
            txt_LiveDetailsNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_LiveDetailsNew.ForeColor = System.Drawing.Color.Gray;
            txt_LiveDetailsNew.Location = new System.Drawing.Point(51, 301);
            txt_LiveDetailsNew.Margin = new Padding(4, 6, 4, 6);
            txt_LiveDetailsNew.Name = "txt_LiveDetailsNew";
            txt_LiveDetailsNew.Size = new System.Drawing.Size(132, 20);
            txt_LiveDetailsNew.TabIndex = 410;
            // 
            // txt_LiveDetailsExisting
            // 
            txt_LiveDetailsExisting.Cue = "Live/Acoustic Details Existing";
            txt_LiveDetailsExisting.Enabled = false;
            txt_LiveDetailsExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_LiveDetailsExisting.ForeColor = System.Drawing.Color.Gray;
            txt_LiveDetailsExisting.Location = new System.Drawing.Point(397, 301);
            txt_LiveDetailsExisting.Margin = new Padding(4, 6, 4, 6);
            txt_LiveDetailsExisting.Name = "txt_LiveDetailsExisting";
            txt_LiveDetailsExisting.Size = new System.Drawing.Size(154, 20);
            txt_LiveDetailsExisting.TabIndex = 408;
            // 
            // txt_PlatformNew
            // 
            txt_PlatformNew.Cue = "Platform New";
            txt_PlatformNew.Enabled = false;
            txt_PlatformNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PlatformNew.ForeColor = System.Drawing.Color.Gray;
            txt_PlatformNew.Location = new System.Drawing.Point(231, 214);
            txt_PlatformNew.Margin = new Padding(4, 6, 4, 6);
            txt_PlatformNew.Name = "txt_PlatformNew";
            txt_PlatformNew.Size = new System.Drawing.Size(43, 20);
            txt_PlatformNew.TabIndex = 403;
            txt_PlatformNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_PlatformExisting
            // 
            txt_PlatformExisting.Cue = "Platform Existing";
            txt_PlatformExisting.Enabled = false;
            txt_PlatformExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_PlatformExisting.ForeColor = System.Drawing.Color.Gray;
            txt_PlatformExisting.Location = new System.Drawing.Point(302, 213);
            txt_PlatformExisting.Margin = new Padding(4, 6, 4, 6);
            txt_PlatformExisting.Name = "txt_PlatformExisting";
            txt_PlatformExisting.Size = new System.Drawing.Size(39, 20);
            txt_PlatformExisting.TabIndex = 404;
            // 
            // txt_FileDateNew
            // 
            txt_FileDateNew.Cue = "Date File";
            txt_FileDateNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            txt_FileDateNew.ForeColor = System.Drawing.Color.Gray;
            txt_FileDateNew.Location = new System.Drawing.Point(87, 260);
            txt_FileDateNew.Margin = new Padding(4, 6, 4, 6);
            txt_FileDateNew.Name = "txt_FileDateNew";
            txt_FileDateNew.ReadOnly = true;
            txt_FileDateNew.Size = new System.Drawing.Size(116, 18);
            txt_FileDateNew.TabIndex = 404;
            txt_FileDateNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_FileDateExisting
            // 
            txt_FileDateExisting.Cue = "DateFile";
            txt_FileDateExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            txt_FileDateExisting.ForeColor = System.Drawing.Color.Gray;
            txt_FileDateExisting.Location = new System.Drawing.Point(388, 258);
            txt_FileDateExisting.Margin = new Padding(4, 6, 4, 6);
            txt_FileDateExisting.Name = "txt_FileDateExisting";
            txt_FileDateExisting.ReadOnly = true;
            txt_FileDateExisting.Size = new System.Drawing.Size(105, 18);
            txt_FileDateExisting.TabIndex = 403;
            // 
            // txt_SizeExisting
            // 
            txt_SizeExisting.Cue = "Size Existing";
            txt_SizeExisting.Enabled = false;
            txt_SizeExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_SizeExisting.ForeColor = System.Drawing.Color.Gray;
            txt_SizeExisting.Location = new System.Drawing.Point(298, 301);
            txt_SizeExisting.Margin = new Padding(4, 6, 4, 6);
            txt_SizeExisting.Name = "txt_SizeExisting";
            txt_SizeExisting.Size = new System.Drawing.Size(98, 20);
            txt_SizeExisting.TabIndex = 391;
            // 
            // txt_SizeNew
            // 
            txt_SizeNew.Cue = "Size New";
            txt_SizeNew.Enabled = false;
            txt_SizeNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_SizeNew.ForeColor = System.Drawing.Color.Gray;
            txt_SizeNew.Location = new System.Drawing.Point(185, 301);
            txt_SizeNew.Margin = new Padding(4, 6, 4, 6);
            txt_SizeNew.Name = "txt_SizeNew";
            txt_SizeNew.Size = new System.Drawing.Size(86, 20);
            txt_SizeNew.TabIndex = 390;
            txt_SizeNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_AlbumExisting
            // 
            txt_AlbumExisting.Cue = "Album Existing";
            txt_AlbumExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumExisting.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumExisting.Location = new System.Drawing.Point(298, 70);
            txt_AlbumExisting.Margin = new Padding(4, 6, 4, 6);
            txt_AlbumExisting.Name = "txt_AlbumExisting";
            txt_AlbumExisting.Size = new System.Drawing.Size(222, 20);
            txt_AlbumExisting.TabIndex = 344;
            txt_AlbumExisting.TextAlign = HorizontalAlignment.Center;
            txt_AlbumExisting.TextChanged += ExistingChanged;
            // 
            // txt_ArtistExisting
            // 
            txt_ArtistExisting.Cue = "Artist Existing";
            txt_ArtistExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ArtistExisting.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistExisting.Location = new System.Drawing.Point(298, 52);
            txt_ArtistExisting.Margin = new Padding(4, 6, 4, 6);
            txt_ArtistExisting.Name = "txt_ArtistExisting";
            txt_ArtistExisting.Size = new System.Drawing.Size(224, 20);
            txt_ArtistExisting.TabIndex = 343;
            txt_ArtistExisting.TextAlign = HorizontalAlignment.Center;
            txt_ArtistExisting.TextChanged += ExistingChanged;
            // 
            // txt_AlbumNew
            // 
            txt_AlbumNew.Cue = "Album New";
            txt_AlbumNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AlbumNew.ForeColor = System.Drawing.Color.Gray;
            txt_AlbumNew.Location = new System.Drawing.Point(59, 70);
            txt_AlbumNew.Margin = new Padding(4, 6, 4, 6);
            txt_AlbumNew.Name = "txt_AlbumNew";
            txt_AlbumNew.Size = new System.Drawing.Size(215, 20);
            txt_AlbumNew.TabIndex = 319;
            txt_AlbumNew.TextAlign = HorizontalAlignment.Center;
            txt_AlbumNew.TextChanged += ExistingChanged;
            // 
            // txt_FileNameExisting
            // 
            txt_FileNameExisting.Cue = "File Name Existing";
            txt_FileNameExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_FileNameExisting.ForeColor = System.Drawing.Color.Gray;
            txt_FileNameExisting.Location = new System.Drawing.Point(298, 167);
            txt_FileNameExisting.Margin = new Padding(4, 6, 4, 6);
            txt_FileNameExisting.Name = "txt_FileNameExisting";
            txt_FileNameExisting.ReadOnly = true;
            txt_FileNameExisting.Size = new System.Drawing.Size(226, 20);
            txt_FileNameExisting.TabIndex = 280;
            txt_FileNameExisting.TextChanged += ExistingChanged;
            // 
            // txt_FileNameNew
            // 
            txt_FileNameNew.Cue = "File Name New";
            txt_FileNameNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_FileNameNew.ForeColor = System.Drawing.Color.Gray;
            txt_FileNameNew.Location = new System.Drawing.Point(52, 169);
            txt_FileNameNew.Margin = new Padding(4, 6, 4, 6);
            txt_FileNameNew.Name = "txt_FileNameNew";
            txt_FileNameNew.ReadOnly = true;
            txt_FileNameNew.ScrollBars = ScrollBars.Horizontal;
            txt_FileNameNew.Size = new System.Drawing.Size(222, 20);
            txt_FileNameNew.TabIndex = 279;
            txt_FileNameNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_ArtistNew
            // 
            txt_ArtistNew.Cue = "Artist New";
            txt_ArtistNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ArtistNew.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistNew.Location = new System.Drawing.Point(59, 52);
            txt_ArtistNew.Margin = new Padding(4, 6, 4, 6);
            txt_ArtistNew.Name = "txt_ArtistNew";
            txt_ArtistNew.Size = new System.Drawing.Size(215, 20);
            txt_ArtistNew.TabIndex = 278;
            txt_ArtistNew.TextAlign = HorizontalAlignment.Center;
            txt_ArtistNew.TextChanged += ExistingChanged;
            // 
            // txt_DLCIDExisting
            // 
            txt_DLCIDExisting.Cue = "DLC Name Existing";
            txt_DLCIDExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DLCIDExisting.ForeColor = System.Drawing.Color.Gray;
            txt_DLCIDExisting.Location = new System.Drawing.Point(298, 281);
            txt_DLCIDExisting.Margin = new Padding(4, 6, 4, 6);
            txt_DLCIDExisting.Name = "txt_DLCIDExisting";
            txt_DLCIDExisting.Size = new System.Drawing.Size(115, 20);
            txt_DLCIDExisting.TabIndex = 233;
            txt_DLCIDExisting.TextChanged += ExistingChanged;
            // 
            // txt_DLCIDNew
            // 
            txt_DLCIDNew.Cue = "DLC Name New";
            txt_DLCIDNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_DLCIDNew.ForeColor = System.Drawing.Color.Gray;
            txt_DLCIDNew.Location = new System.Drawing.Point(148, 281);
            txt_DLCIDNew.Margin = new Padding(4, 6, 4, 6);
            txt_DLCIDNew.Name = "txt_DLCIDNew";
            txt_DLCIDNew.ScrollBars = ScrollBars.Horizontal;
            txt_DLCIDNew.Size = new System.Drawing.Size(126, 20);
            txt_DLCIDNew.TabIndex = 232;
            txt_DLCIDNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_TuningExisting
            // 
            txt_TuningExisting.Cue = "Tuning Existing";
            txt_TuningExisting.Enabled = false;
            txt_TuningExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TuningExisting.ForeColor = System.Drawing.Color.Gray;
            txt_TuningExisting.Location = new System.Drawing.Point(297, 485);
            txt_TuningExisting.Margin = new Padding(4, 6, 4, 6);
            txt_TuningExisting.Name = "txt_TuningExisting";
            txt_TuningExisting.Size = new System.Drawing.Size(114, 20);
            txt_TuningExisting.TabIndex = 231;
            // 
            // txt_TuningNew
            // 
            txt_TuningNew.Cue = "Tuning New";
            txt_TuningNew.Enabled = false;
            txt_TuningNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TuningNew.ForeColor = System.Drawing.Color.Gray;
            txt_TuningNew.Location = new System.Drawing.Point(157, 485);
            txt_TuningNew.Margin = new Padding(4, 6, 4, 6);
            txt_TuningNew.Name = "txt_TuningNew";
            txt_TuningNew.Size = new System.Drawing.Size(114, 20);
            txt_TuningNew.TabIndex = 230;
            txt_TuningNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_AuthorExisting
            // 
            txt_AuthorExisting.Cue = "Author Existing";
            txt_AuthorExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AuthorExisting.ForeColor = System.Drawing.Color.Gray;
            txt_AuthorExisting.Location = new System.Drawing.Point(298, 235);
            txt_AuthorExisting.Margin = new Padding(4, 6, 4, 6);
            txt_AuthorExisting.Name = "txt_AuthorExisting";
            txt_AuthorExisting.Size = new System.Drawing.Size(118, 20);
            txt_AuthorExisting.TabIndex = 229;
            txt_AuthorExisting.TextChanged += ExistingChanged;
            // 
            // txt_AuthorNew
            // 
            txt_AuthorNew.Cue = "Author New";
            txt_AuthorNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_AuthorNew.ForeColor = System.Drawing.Color.Gray;
            txt_AuthorNew.Location = new System.Drawing.Point(158, 236);
            txt_AuthorNew.Margin = new Padding(4, 6, 4, 6);
            txt_AuthorNew.Name = "txt_AuthorNew";
            txt_AuthorNew.Size = new System.Drawing.Size(116, 20);
            txt_AuthorNew.TabIndex = 228;
            txt_AuthorNew.TextAlign = HorizontalAlignment.Right;
            txt_AuthorNew.TextChanged += ExistingChanged;
            // 
            // txt_IsOriginalExisting
            // 
            txt_IsOriginalExisting.Cue = "Is Original Existing";
            txt_IsOriginalExisting.Enabled = false;
            txt_IsOriginalExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_IsOriginalExisting.ForeColor = System.Drawing.Color.Gray;
            txt_IsOriginalExisting.Location = new System.Drawing.Point(298, 190);
            txt_IsOriginalExisting.Margin = new Padding(4, 6, 4, 6);
            txt_IsOriginalExisting.Name = "txt_IsOriginalExisting";
            txt_IsOriginalExisting.Size = new System.Drawing.Size(47, 20);
            txt_IsOriginalExisting.TabIndex = 227;
            // 
            // txt_IsOriginalNew
            // 
            txt_IsOriginalNew.Cue = "Is Original New";
            txt_IsOriginalNew.Enabled = false;
            txt_IsOriginalNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_IsOriginalNew.ForeColor = System.Drawing.Color.Gray;
            txt_IsOriginalNew.Location = new System.Drawing.Point(227, 190);
            txt_IsOriginalNew.Margin = new Padding(4, 6, 4, 6);
            txt_IsOriginalNew.Name = "txt_IsOriginalNew";
            txt_IsOriginalNew.Size = new System.Drawing.Size(47, 20);
            txt_IsOriginalNew.TabIndex = 226;
            txt_IsOriginalNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_ToolkitExisting
            // 
            txt_ToolkitExisting.Cue = "Toolkit Existing";
            txt_ToolkitExisting.Enabled = false;
            txt_ToolkitExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ToolkitExisting.ForeColor = System.Drawing.Color.Gray;
            txt_ToolkitExisting.Location = new System.Drawing.Point(343, 213);
            txt_ToolkitExisting.Margin = new Padding(4, 6, 4, 6);
            txt_ToolkitExisting.Name = "txt_ToolkitExisting";
            txt_ToolkitExisting.Size = new System.Drawing.Size(122, 20);
            txt_ToolkitExisting.TabIndex = 225;
            // 
            // txt_ToolkitNew
            // 
            txt_ToolkitNew.Cue = "Toolkit New";
            txt_ToolkitNew.Enabled = false;
            txt_ToolkitNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ToolkitNew.ForeColor = System.Drawing.Color.Gray;
            txt_ToolkitNew.Location = new System.Drawing.Point(113, 214);
            txt_ToolkitNew.Margin = new Padding(4, 6, 4, 6);
            txt_ToolkitNew.Name = "txt_ToolkitNew";
            txt_ToolkitNew.Size = new System.Drawing.Size(117, 20);
            txt_ToolkitNew.TabIndex = 224;
            txt_ToolkitNew.TextAlign = HorizontalAlignment.Right;
            // 
            // txt_TitleSortExisting
            // 
            txt_TitleSortExisting.Cue = "Title Sort Existing";
            txt_TitleSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TitleSortExisting.ForeColor = System.Drawing.Color.Gray;
            txt_TitleSortExisting.Location = new System.Drawing.Point(298, 109);
            txt_TitleSortExisting.Margin = new Padding(4, 6, 4, 6);
            txt_TitleSortExisting.Name = "txt_TitleSortExisting";
            txt_TitleSortExisting.Size = new System.Drawing.Size(222, 20);
            txt_TitleSortExisting.TabIndex = 223;
            txt_TitleSortExisting.TextChanged += ExistingChanged;
            // 
            // txt_TitleSortNew
            // 
            txt_TitleSortNew.Cue = "Title Sort New";
            txt_TitleSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TitleSortNew.ForeColor = System.Drawing.Color.Gray;
            txt_TitleSortNew.Location = new System.Drawing.Point(59, 110);
            txt_TitleSortNew.Margin = new Padding(4, 6, 4, 6);
            txt_TitleSortNew.Name = "txt_TitleSortNew";
            txt_TitleSortNew.Size = new System.Drawing.Size(215, 20);
            txt_TitleSortNew.TabIndex = 222;
            txt_TitleSortNew.TextAlign = HorizontalAlignment.Right;
            txt_TitleSortNew.TextChanged += ExistingChanged;
            // 
            // txt_ArtistSortExisting
            // 
            txt_ArtistSortExisting.Cue = "Artist Sort Existing";
            txt_ArtistSortExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ArtistSortExisting.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistSortExisting.Location = new System.Drawing.Point(298, 129);
            txt_ArtistSortExisting.Margin = new Padding(4, 6, 4, 6);
            txt_ArtistSortExisting.Name = "txt_ArtistSortExisting";
            txt_ArtistSortExisting.Size = new System.Drawing.Size(224, 20);
            txt_ArtistSortExisting.TabIndex = 221;
            txt_ArtistSortExisting.TextChanged += ExistingChanged;
            // 
            // txt_ArtistSortNew
            // 
            txt_ArtistSortNew.Cue = "Artist Sort New";
            txt_ArtistSortNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ArtistSortNew.ForeColor = System.Drawing.Color.Gray;
            txt_ArtistSortNew.Location = new System.Drawing.Point(59, 129);
            txt_ArtistSortNew.Margin = new Padding(4, 6, 4, 6);
            txt_ArtistSortNew.Name = "txt_ArtistSortNew";
            txt_ArtistSortNew.Size = new System.Drawing.Size(215, 20);
            txt_ArtistSortNew.TabIndex = 220;
            txt_ArtistSortNew.TextAlign = HorizontalAlignment.Right;
            txt_ArtistSortNew.TextChanged += ExistingChanged;
            // 
            // txt_TitleExisting
            // 
            txt_TitleExisting.Cue = "Title Existing";
            txt_TitleExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TitleExisting.ForeColor = System.Drawing.Color.Gray;
            txt_TitleExisting.Location = new System.Drawing.Point(298, 90);
            txt_TitleExisting.Margin = new Padding(4, 6, 4, 6);
            txt_TitleExisting.Name = "txt_TitleExisting";
            txt_TitleExisting.Size = new System.Drawing.Size(224, 20);
            txt_TitleExisting.TabIndex = 219;
            txt_TitleExisting.TextChanged += ExistingChanged;
            // 
            // txt_TitleNew
            // 
            txt_TitleNew.Cue = "Title New";
            txt_TitleNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TitleNew.ForeColor = System.Drawing.Color.Gray;
            txt_TitleNew.Location = new System.Drawing.Point(59, 90);
            txt_TitleNew.Margin = new Padding(4, 6, 4, 6);
            txt_TitleNew.Name = "txt_TitleNew";
            txt_TitleNew.Size = new System.Drawing.Size(215, 20);
            txt_TitleNew.TabIndex = 217;
            txt_TitleNew.TextAlign = HorizontalAlignment.Right;
            txt_TitleNew.TextChanged += ExistingChanged;
            // 
            // lbl_YearExisting
            // 
            lbl_YearExisting.Location = new System.Drawing.Point(-4, 152);
            lbl_YearExisting.Margin = new Padding(2, 0, 2, 0);
            lbl_YearExisting.Name = "lbl_YearExisting";
            lbl_YearExisting.Size = new System.Drawing.Size(29, 15);
            lbl_YearExisting.TabIndex = 426;
            lbl_YearExisting.Text = "Year";
            // 
            // lbl_YearNew
            // 
            lbl_YearNew.Location = new System.Drawing.Point(6, 194);
            lbl_YearNew.Margin = new Padding(2, 0, 2, 0);
            lbl_YearNew.Name = "lbl_YearNew";
            lbl_YearNew.Size = new System.Drawing.Size(29, 15);
            lbl_YearNew.TabIndex = 427;
            lbl_YearNew.Text = "Year";
            lbl_YearNew.Visible = false;
            // 
            // lbl_IDNew
            // 
            lbl_IDNew.BorderStyle = BorderStyle.FixedSingle;
            lbl_IDNew.Location = new System.Drawing.Point(110, 32);
            lbl_IDNew.Margin = new Padding(2, 0, 2, 0);
            lbl_IDNew.Name = "lbl_IDNew";
            lbl_IDNew.Size = new System.Drawing.Size(47, 17);
            lbl_IDNew.TabIndex = 429;
            lbl_IDNew.Text = "ID New";
            // 
            // lbl_ExistingIs_Original
            // 
            lbl_ExistingIs_Original.Location = new System.Drawing.Point(343, 193);
            lbl_ExistingIs_Original.Margin = new Padding(2, 0, 2, 0);
            lbl_ExistingIs_Original.Name = "lbl_ExistingIs_Original";
            lbl_ExistingIs_Original.Size = new System.Drawing.Size(56, 15);
            lbl_ExistingIs_Original.TabIndex = 432;
            lbl_ExistingIs_Original.Text = "Is Official";
            // 
            // chbx_SingleNew
            // 
            chbx_SingleNew.Appearance = Appearance.Button;
            chbx_SingleNew.Location = new System.Drawing.Point(224, 328);
            chbx_SingleNew.Margin = new Padding(2, 3, 2, 3);
            chbx_SingleNew.Name = "chbx_SingleNew";
            chbx_SingleNew.Size = new System.Drawing.Size(49, 25);
            chbx_SingleNew.TabIndex = 433;
            chbx_SingleNew.Text = "Single";
            chbx_SingleNew.UseVisualStyleBackColor = true;
            // 
            // chbx_EPNew
            // 
            chbx_EPNew.Appearance = Appearance.Button;
            chbx_EPNew.Location = new System.Drawing.Point(244, 384);
            chbx_EPNew.Margin = new Padding(2, 3, 2, 3);
            chbx_EPNew.Name = "chbx_EPNew";
            chbx_EPNew.Size = new System.Drawing.Size(30, 25);
            chbx_EPNew.TabIndex = 434;
            chbx_EPNew.Text = "EP";
            chbx_EPNew.UseVisualStyleBackColor = true;
            // 
            // chbx_SoundtrackNew
            // 
            chbx_SoundtrackNew.Appearance = Appearance.Button;
            chbx_SoundtrackNew.Location = new System.Drawing.Point(196, 410);
            chbx_SoundtrackNew.Margin = new Padding(2, 3, 2, 3);
            chbx_SoundtrackNew.Name = "chbx_SoundtrackNew";
            chbx_SoundtrackNew.Size = new System.Drawing.Size(77, 25);
            chbx_SoundtrackNew.TabIndex = 435;
            chbx_SoundtrackNew.Text = "Soundtrack";
            chbx_SoundtrackNew.UseVisualStyleBackColor = true;
            // 
            // chbx_InstrumentalNew
            // 
            chbx_InstrumentalNew.Appearance = Appearance.Button;
            chbx_InstrumentalNew.Location = new System.Drawing.Point(140, 328);
            chbx_InstrumentalNew.Margin = new Padding(2, 3, 2, 3);
            chbx_InstrumentalNew.Name = "chbx_InstrumentalNew";
            chbx_InstrumentalNew.Size = new System.Drawing.Size(84, 25);
            chbx_InstrumentalNew.TabIndex = 436;
            chbx_InstrumentalNew.Text = "Instrumental";
            chbx_InstrumentalNew.UseVisualStyleBackColor = true;
            // 
            // chbx_UncensoredNew
            // 
            chbx_UncensoredNew.Appearance = Appearance.Button;
            chbx_UncensoredNew.Location = new System.Drawing.Point(118, 410);
            chbx_UncensoredNew.Margin = new Padding(2, 3, 2, 3);
            chbx_UncensoredNew.Name = "chbx_UncensoredNew";
            chbx_UncensoredNew.Size = new System.Drawing.Size(80, 25);
            chbx_UncensoredNew.TabIndex = 437;
            chbx_UncensoredNew.Text = "Uncensored";
            chbx_UncensoredNew.UseVisualStyleBackColor = true;
            // 
            // chbx_FullAlbumNew
            // 
            chbx_FullAlbumNew.Appearance = Appearance.Button;
            chbx_FullAlbumNew.Location = new System.Drawing.Point(140, 356);
            chbx_FullAlbumNew.Margin = new Padding(2, 3, 2, 3);
            chbx_FullAlbumNew.Name = "chbx_FullAlbumNew";
            chbx_FullAlbumNew.Size = new System.Drawing.Size(75, 25);
            chbx_FullAlbumNew.TabIndex = 438;
            chbx_FullAlbumNew.Text = "Full Album";
            chbx_FullAlbumNew.UseVisualStyleBackColor = true;
            // 
            // chbx_RemasteredNew
            // 
            chbx_RemasteredNew.Appearance = Appearance.Button;
            chbx_RemasteredNew.Location = new System.Drawing.Point(171, 384);
            chbx_RemasteredNew.Margin = new Padding(2, 3, 2, 3);
            chbx_RemasteredNew.Name = "chbx_RemasteredNew";
            chbx_RemasteredNew.Size = new System.Drawing.Size(79, 25);
            chbx_RemasteredNew.TabIndex = 439;
            chbx_RemasteredNew.Text = "Remastered";
            chbx_RemasteredNew.UseVisualStyleBackColor = true;
            chbx_RemasteredNew.CheckedChanged += checkBox7_CheckedChanged;
            // 
            // chbx_FullAlbumExisting
            // 
            chbx_FullAlbumExisting.Appearance = Appearance.Button;
            chbx_FullAlbumExisting.Location = new System.Drawing.Point(354, 356);
            chbx_FullAlbumExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_FullAlbumExisting.Name = "chbx_FullAlbumExisting";
            chbx_FullAlbumExisting.Size = new System.Drawing.Size(72, 25);
            chbx_FullAlbumExisting.TabIndex = 440;
            chbx_FullAlbumExisting.Text = "FullAlbum";
            chbx_FullAlbumExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_InstrumentalExisting
            // 
            chbx_InstrumentalExisting.Appearance = Appearance.Button;
            chbx_InstrumentalExisting.Location = new System.Drawing.Point(342, 328);
            chbx_InstrumentalExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_InstrumentalExisting.Name = "chbx_InstrumentalExisting";
            chbx_InstrumentalExisting.Size = new System.Drawing.Size(84, 25);
            chbx_InstrumentalExisting.TabIndex = 442;
            chbx_InstrumentalExisting.Text = "Instrumental";
            chbx_InstrumentalExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_SoundtrackExisting
            // 
            chbx_SoundtrackExisting.Appearance = Appearance.Button;
            chbx_SoundtrackExisting.Location = new System.Drawing.Point(294, 410);
            chbx_SoundtrackExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_SoundtrackExisting.Name = "chbx_SoundtrackExisting";
            chbx_SoundtrackExisting.Size = new System.Drawing.Size(77, 25);
            chbx_SoundtrackExisting.TabIndex = 441;
            chbx_SoundtrackExisting.Text = "Soundtrack";
            chbx_SoundtrackExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_SingleExisting
            // 
            chbx_SingleExisting.Appearance = Appearance.Button;
            chbx_SingleExisting.Location = new System.Drawing.Point(294, 328);
            chbx_SingleExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_SingleExisting.Name = "chbx_SingleExisting";
            chbx_SingleExisting.Size = new System.Drawing.Size(49, 25);
            chbx_SingleExisting.TabIndex = 443;
            chbx_SingleExisting.Text = "Single";
            chbx_SingleExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_UncensoredExisting
            // 
            chbx_UncensoredExisting.Appearance = Appearance.Button;
            chbx_UncensoredExisting.Location = new System.Drawing.Point(371, 410);
            chbx_UncensoredExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_UncensoredExisting.Name = "chbx_UncensoredExisting";
            chbx_UncensoredExisting.Size = new System.Drawing.Size(80, 25);
            chbx_UncensoredExisting.TabIndex = 444;
            chbx_UncensoredExisting.Text = "Uncensored";
            chbx_UncensoredExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_EPExisting
            // 
            chbx_EPExisting.Appearance = Appearance.Button;
            chbx_EPExisting.Location = new System.Drawing.Point(294, 384);
            chbx_EPExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_EPExisting.Name = "chbx_EPExisting";
            chbx_EPExisting.Size = new System.Drawing.Size(30, 25);
            chbx_EPExisting.TabIndex = 445;
            chbx_EPExisting.Text = "EP";
            chbx_EPExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_RemasteredExisting
            // 
            chbx_RemasteredExisting.Appearance = Appearance.Button;
            chbx_RemasteredExisting.Location = new System.Drawing.Point(323, 384);
            chbx_RemasteredExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_RemasteredExisting.Name = "chbx_RemasteredExisting";
            chbx_RemasteredExisting.Size = new System.Drawing.Size(79, 25);
            chbx_RemasteredExisting.TabIndex = 446;
            chbx_RemasteredExisting.Text = "Remastered";
            chbx_RemasteredExisting.UseVisualStyleBackColor = true;
            // 
            // lbl_P1
            // 
            lbl_P1.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_P1.Location = new System.Drawing.Point(274, 334);
            lbl_P1.Margin = new Padding(2, 0, 2, 0);
            lbl_P1.Name = "lbl_P1";
            lbl_P1.Size = new System.Drawing.Size(22, 16);
            lbl_P1.TabIndex = 447;
            lbl_P1.Text = "Vs.";
            // 
            // lbl_P2
            // 
            lbl_P2.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_P2.Location = new System.Drawing.Point(274, 362);
            lbl_P2.Margin = new Padding(2, 0, 2, 0);
            lbl_P2.Name = "lbl_P2";
            lbl_P2.Size = new System.Drawing.Size(22, 16);
            lbl_P2.TabIndex = 448;
            lbl_P2.Text = "Vs.";
            // 
            // lbl_P3
            // 
            lbl_P3.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_P3.Location = new System.Drawing.Point(274, 390);
            lbl_P3.Margin = new Padding(2, 0, 2, 0);
            lbl_P3.Name = "lbl_P3";
            lbl_P3.Size = new System.Drawing.Size(22, 16);
            lbl_P3.TabIndex = 449;
            lbl_P3.Text = "Vs.";
            // 
            // lbl_P4
            // 
            lbl_P4.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_P4.Location = new System.Drawing.Point(274, 415);
            lbl_P4.Margin = new Padding(2, 0, 2, 0);
            lbl_P4.Name = "lbl_P4";
            lbl_P4.Size = new System.Drawing.Size(22, 16);
            lbl_P4.TabIndex = 450;
            lbl_P4.Text = "Vs.";
            // 
            // chbx_InTheWorksNew
            // 
            chbx_InTheWorksNew.Appearance = Appearance.Button;
            chbx_InTheWorksNew.Location = new System.Drawing.Point(97, 384);
            chbx_InTheWorksNew.Margin = new Padding(2, 3, 2, 3);
            chbx_InTheWorksNew.Name = "chbx_InTheWorksNew";
            chbx_InTheWorksNew.Size = new System.Drawing.Size(79, 25);
            chbx_InTheWorksNew.TabIndex = 452;
            chbx_InTheWorksNew.Text = "InTheWorks";
            chbx_InTheWorksNew.UseVisualStyleBackColor = true;
            // 
            // chbx_InTheWorksExisting
            // 
            chbx_InTheWorksExisting.Appearance = Appearance.Button;
            chbx_InTheWorksExisting.Location = new System.Drawing.Point(396, 384);
            chbx_InTheWorksExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_InTheWorksExisting.Name = "chbx_InTheWorksExisting";
            chbx_InTheWorksExisting.Size = new System.Drawing.Size(79, 25);
            chbx_InTheWorksExisting.TabIndex = 453;
            chbx_InTheWorksExisting.Text = "InTheWorks";
            chbx_InTheWorksExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_DemoExisting
            // 
            chbx_DemoExisting.Appearance = Appearance.Button;
            chbx_DemoExisting.Location = new System.Drawing.Point(424, 328);
            chbx_DemoExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_DemoExisting.Name = "chbx_DemoExisting";
            chbx_DemoExisting.Size = new System.Drawing.Size(49, 25);
            chbx_DemoExisting.TabIndex = 454;
            chbx_DemoExisting.Text = "Demo";
            chbx_DemoExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_DemoNew
            // 
            chbx_DemoNew.Appearance = Appearance.Button;
            chbx_DemoNew.Location = new System.Drawing.Point(94, 328);
            chbx_DemoNew.Margin = new Padding(2, 3, 2, 3);
            chbx_DemoNew.Name = "chbx_DemoNew";
            chbx_DemoNew.Size = new System.Drawing.Size(49, 25);
            chbx_DemoNew.TabIndex = 455;
            chbx_DemoNew.Text = "Demo";
            chbx_DemoNew.UseVisualStyleBackColor = true;
            // 
            // chbx_RemixExisting
            // 
            chbx_RemixExisting.Appearance = Appearance.Button;
            chbx_RemixExisting.Location = new System.Drawing.Point(424, 356);
            chbx_RemixExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_RemixExisting.Name = "chbx_RemixExisting";
            chbx_RemixExisting.Size = new System.Drawing.Size(50, 25);
            chbx_RemixExisting.TabIndex = 456;
            chbx_RemixExisting.Text = "Remix";
            chbx_RemixExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_RemixNew
            // 
            chbx_RemixNew.Appearance = Appearance.Button;
            chbx_RemixNew.Location = new System.Drawing.Point(96, 356);
            chbx_RemixNew.Margin = new Padding(2, 3, 2, 3);
            chbx_RemixNew.Name = "chbx_RemixNew";
            chbx_RemixNew.Size = new System.Drawing.Size(50, 25);
            chbx_RemixNew.TabIndex = 457;
            chbx_RemixNew.Text = "Remix";
            chbx_RemixNew.UseVisualStyleBackColor = true;
            // 
            // chbx_KaraokeNew
            // 
            chbx_KaraokeNew.Appearance = Appearance.Button;
            chbx_KaraokeNew.Location = new System.Drawing.Point(64, 410);
            chbx_KaraokeNew.Margin = new Padding(2, 3, 2, 3);
            chbx_KaraokeNew.Name = "chbx_KaraokeNew";
            chbx_KaraokeNew.Size = new System.Drawing.Size(59, 25);
            chbx_KaraokeNew.TabIndex = 458;
            chbx_KaraokeNew.Text = "Karaoke";
            chbx_KaraokeNew.UseVisualStyleBackColor = true;
            // 
            // chbx_KaraokeExisting
            // 
            chbx_KaraokeExisting.Appearance = Appearance.Button;
            chbx_KaraokeExisting.Location = new System.Drawing.Point(447, 410);
            chbx_KaraokeExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_KaraokeExisting.Name = "chbx_KaraokeExisting";
            chbx_KaraokeExisting.Size = new System.Drawing.Size(59, 25);
            chbx_KaraokeExisting.TabIndex = 459;
            chbx_KaraokeExisting.Text = "Karaoke";
            chbx_KaraokeExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_FeaturingNew
            // 
            chbx_FeaturingNew.Appearance = Appearance.Button;
            chbx_FeaturingNew.Location = new System.Drawing.Point(58, 356);
            chbx_FeaturingNew.Margin = new Padding(2, 3, 2, 3);
            chbx_FeaturingNew.Name = "chbx_FeaturingNew";
            chbx_FeaturingNew.Size = new System.Drawing.Size(42, 25);
            chbx_FeaturingNew.TabIndex = 460;
            chbx_FeaturingNew.Text = "Feat.";
            chbx_FeaturingNew.UseVisualStyleBackColor = true;
            // 
            // chbx_FeaturingExisting
            // 
            chbx_FeaturingExisting.Appearance = Appearance.Button;
            chbx_FeaturingExisting.Location = new System.Drawing.Point(473, 356);
            chbx_FeaturingExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_FeaturingExisting.Name = "chbx_FeaturingExisting";
            chbx_FeaturingExisting.Size = new System.Drawing.Size(42, 25);
            chbx_FeaturingExisting.TabIndex = 461;
            chbx_FeaturingExisting.Text = "Feat.";
            chbx_FeaturingExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_CoverNew
            // 
            chbx_CoverNew.Appearance = Appearance.Button;
            chbx_CoverNew.Location = new System.Drawing.Point(51, 328);
            chbx_CoverNew.Margin = new Padding(2, 3, 2, 3);
            chbx_CoverNew.Name = "chbx_CoverNew";
            chbx_CoverNew.Size = new System.Drawing.Size(48, 25);
            chbx_CoverNew.TabIndex = 462;
            chbx_CoverNew.Text = "Cover";
            chbx_CoverNew.UseVisualStyleBackColor = true;
            // 
            // chbx_CoverExisting
            // 
            chbx_CoverExisting.Appearance = Appearance.Button;
            chbx_CoverExisting.Location = new System.Drawing.Point(472, 328);
            chbx_CoverExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_CoverExisting.Name = "chbx_CoverExisting";
            chbx_CoverExisting.Size = new System.Drawing.Size(48, 25);
            chbx_CoverExisting.TabIndex = 463;
            chbx_CoverExisting.Text = "Cover";
            chbx_CoverExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_MedleyNew
            // 
            chbx_MedleyNew.Appearance = Appearance.Button;
            chbx_MedleyNew.Location = new System.Drawing.Point(44, 384);
            chbx_MedleyNew.Margin = new Padding(2);
            chbx_MedleyNew.Name = "chbx_MedleyNew";
            chbx_MedleyNew.Size = new System.Drawing.Size(56, 25);
            chbx_MedleyNew.TabIndex = 468;
            chbx_MedleyNew.Text = "Medley";
            chbx_MedleyNew.UseVisualStyleBackColor = true;
            // 
            // chbx_MidiNew
            // 
            chbx_MidiNew.Appearance = Appearance.Button;
            chbx_MidiNew.Location = new System.Drawing.Point(129, 433);
            chbx_MidiNew.Margin = new Padding(2, 3, 2, 3);
            chbx_MidiNew.Name = "chbx_MidiNew";
            chbx_MidiNew.Size = new System.Drawing.Size(41, 25);
            chbx_MidiNew.TabIndex = 475;
            chbx_MidiNew.Text = "Midi";
            chbx_MidiNew.UseVisualStyleBackColor = true;
            // 
            // chbx_MidiExisting
            // 
            chbx_MidiExisting.Appearance = Appearance.Button;
            chbx_MidiExisting.Location = new System.Drawing.Point(400, 433);
            chbx_MidiExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_MidiExisting.Name = "chbx_MidiExisting";
            chbx_MidiExisting.Size = new System.Drawing.Size(41, 25);
            chbx_MidiExisting.TabIndex = 480;
            chbx_MidiExisting.Text = "Midi";
            chbx_MidiExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_GameSoundtrackExisting
            // 
            chbx_GameSoundtrackExisting.Appearance = Appearance.Button;
            chbx_GameSoundtrackExisting.Location = new System.Drawing.Point(294, 433);
            chbx_GameSoundtrackExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_GameSoundtrackExisting.Name = "chbx_GameSoundtrackExisting";
            chbx_GameSoundtrackExisting.Size = new System.Drawing.Size(108, 25);
            chbx_GameSoundtrackExisting.TabIndex = 479;
            chbx_GameSoundtrackExisting.Text = "GameSoundtrack";
            chbx_GameSoundtrackExisting.UseVisualStyleBackColor = true;
            // 
            // lbl_P5
            // 
            lbl_P5.ForeColor = System.Drawing.Color.DarkGreen;
            lbl_P5.Location = new System.Drawing.Point(274, 439);
            lbl_P5.Margin = new Padding(2, 0, 2, 0);
            lbl_P5.Name = "lbl_P5";
            lbl_P5.Size = new System.Drawing.Size(22, 16);
            lbl_P5.TabIndex = 482;
            lbl_P5.Text = "Vs.";
            // 
            // chbx_AmateurCoverNew
            // 
            chbx_AmateurCoverNew.Appearance = Appearance.Button;
            chbx_AmateurCoverNew.Location = new System.Drawing.Point(-2, 356);
            chbx_AmateurCoverNew.Margin = new Padding(2, 3, 2, 3);
            chbx_AmateurCoverNew.Name = "chbx_AmateurCoverNew";
            chbx_AmateurCoverNew.Size = new System.Drawing.Size(63, 25);
            chbx_AmateurCoverNew.TabIndex = 483;
            chbx_AmateurCoverNew.Text = "Amateur";
            chbx_AmateurCoverNew.UseVisualStyleBackColor = true;
            // 
            // chbx_AmateurCoverExisting
            // 
            chbx_AmateurCoverExisting.Appearance = Appearance.Button;
            chbx_AmateurCoverExisting.Location = new System.Drawing.Point(515, 356);
            chbx_AmateurCoverExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_AmateurCoverExisting.Name = "chbx_AmateurCoverExisting";
            chbx_AmateurCoverExisting.Size = new System.Drawing.Size(63, 25);
            chbx_AmateurCoverExisting.TabIndex = 484;
            chbx_AmateurCoverExisting.Text = "Amateur";
            chbx_AmateurCoverExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_TVThemeNew
            // 
            chbx_TVThemeNew.Appearance = Appearance.Button;
            chbx_TVThemeNew.Location = new System.Drawing.Point(3, 410);
            chbx_TVThemeNew.Margin = new Padding(2, 3, 2, 3);
            chbx_TVThemeNew.Name = "chbx_TVThemeNew";
            chbx_TVThemeNew.Size = new System.Drawing.Size(66, 25);
            chbx_TVThemeNew.TabIndex = 485;
            chbx_TVThemeNew.Text = "TVTheme";
            chbx_TVThemeNew.UseVisualStyleBackColor = true;
            // 
            // chbx_TVThemeExisting
            // 
            chbx_TVThemeExisting.Appearance = Appearance.Button;
            chbx_TVThemeExisting.Location = new System.Drawing.Point(505, 410);
            chbx_TVThemeExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_TVThemeExisting.Name = "chbx_TVThemeExisting";
            chbx_TVThemeExisting.Size = new System.Drawing.Size(66, 25);
            chbx_TVThemeExisting.TabIndex = 486;
            chbx_TVThemeExisting.Text = "TVTheme";
            chbx_TVThemeExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_UkuleleExisting
            // 
            chbx_UkuleleExisting.Appearance = Appearance.Button;
            chbx_UkuleleExisting.Location = new System.Drawing.Point(523, 328);
            chbx_UkuleleExisting.Margin = new Padding(2, 3, 2, 3);
            chbx_UkuleleExisting.Name = "chbx_UkuleleExisting";
            chbx_UkuleleExisting.Size = new System.Drawing.Size(55, 25);
            chbx_UkuleleExisting.TabIndex = 487;
            chbx_UkuleleExisting.Text = "Ukulele";
            chbx_UkuleleExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_MetalNew
            // 
            chbx_MetalNew.Appearance = Appearance.Button;
            chbx_MetalNew.Location = new System.Drawing.Point(-1, 384);
            chbx_MetalNew.Margin = new Padding(2);
            chbx_MetalNew.Name = "chbx_MetalNew";
            chbx_MetalNew.Size = new System.Drawing.Size(50, 25);
            chbx_MetalNew.TabIndex = 489;
            chbx_MetalNew.Text = "Metal";
            chbx_MetalNew.UseVisualStyleBackColor = true;
            // 
            // chbx_UkuleleNew
            // 
            chbx_UkuleleNew.Appearance = Appearance.Button;
            chbx_UkuleleNew.Location = new System.Drawing.Point(-1, 328);
            chbx_UkuleleNew.Margin = new Padding(2, 3, 2, 3);
            chbx_UkuleleNew.Name = "chbx_UkuleleNew";
            chbx_UkuleleNew.Size = new System.Drawing.Size(57, 25);
            chbx_UkuleleNew.TabIndex = 490;
            chbx_UkuleleNew.Text = "Ukulele";
            chbx_UkuleleNew.UseVisualStyleBackColor = true;
            // 
            // txt_TuningFrecvExisting
            // 
            txt_TuningFrecvExisting.Cue = "Frecvency of Tuning Existing";
            txt_TuningFrecvExisting.Enabled = false;
            txt_TuningFrecvExisting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TuningFrecvExisting.ForeColor = System.Drawing.Color.Gray;
            txt_TuningFrecvExisting.Location = new System.Drawing.Point(412, 484);
            txt_TuningFrecvExisting.Margin = new Padding(4, 6, 4, 6);
            txt_TuningFrecvExisting.Name = "txt_TuningFrecvExisting";
            txt_TuningFrecvExisting.Size = new System.Drawing.Size(55, 20);
            txt_TuningFrecvExisting.TabIndex = 491;
            // 
            // txt_TuningFrecvNew
            // 
            txt_TuningFrecvNew.Cue = "Frecvency of Tuning Existing";
            txt_TuningFrecvNew.Enabled = false;
            txt_TuningFrecvNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_TuningFrecvNew.ForeColor = System.Drawing.Color.Gray;
            txt_TuningFrecvNew.Location = new System.Drawing.Point(100, 484);
            txt_TuningFrecvNew.Margin = new Padding(4, 6, 4, 6);
            txt_TuningFrecvNew.Name = "txt_TuningFrecvNew";
            txt_TuningFrecvNew.Size = new System.Drawing.Size(55, 20);
            txt_TuningFrecvNew.TabIndex = 492;
            // 
            // lbl_Frecv
            // 
            lbl_Frecv.Location = new System.Drawing.Point(564, 487);
            lbl_Frecv.Margin = new Padding(2, 0, 2, 0);
            lbl_Frecv.Name = "lbl_Frecv";
            lbl_Frecv.Size = new System.Drawing.Size(161, 17);
            lbl_Frecv.TabIndex = 496;
            lbl_Frecv.Text = "&& Frecvencies";
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(598, 50);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(85, 15);
            label3.TabIndex = 498;
            label3.Text = "Duplicate of ID";
            // 
            // txt_ExistingDuplicateOfID
            // 
            txt_ExistingDuplicateOfID.Cue = "Existing ID";
            txt_ExistingDuplicateOfID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            txt_ExistingDuplicateOfID.ForeColor = System.Drawing.Color.Gray;
            txt_ExistingDuplicateOfID.Location = new System.Drawing.Point(622, 66);
            txt_ExistingDuplicateOfID.Margin = new Padding(4, 6, 4, 6);
            txt_ExistingDuplicateOfID.Name = "txt_ExistingDuplicateOfID";
            txt_ExistingDuplicateOfID.Size = new System.Drawing.Size(61, 20);
            txt_ExistingDuplicateOfID.TabIndex = 497;
            txt_ExistingDuplicateOfID.TextAlign = HorizontalAlignment.Right;
            // 
            // bt_AddP1
            // 
            bt_AddP1.BackColor = System.Drawing.SystemColors.Control;
            bt_AddP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            bt_AddP1.Location = new System.Drawing.Point(582, 334);
            bt_AddP1.Margin = new Padding(2);
            bt_AddP1.Name = "bt_AddP1";
            bt_AddP1.Size = new System.Drawing.Size(14, 14);
            bt_AddP1.TabIndex = 499;
            bt_AddP1.Text = "+";
            bt_AddP1.UseVisualStyleBackColor = false;
            bt_AddP1.Click += bt_AddP1_Click;
            // 
            // bt_AddP2
            // 
            bt_AddP2.BackColor = System.Drawing.SystemColors.Control;
            bt_AddP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            bt_AddP2.Location = new System.Drawing.Point(582, 362);
            bt_AddP2.Margin = new Padding(2);
            bt_AddP2.Name = "bt_AddP2";
            bt_AddP2.Size = new System.Drawing.Size(14, 14);
            bt_AddP2.TabIndex = 500;
            bt_AddP2.Text = "+";
            bt_AddP2.UseVisualStyleBackColor = false;
            bt_AddP2.Click += bt_AddP2_Click;
            // 
            // bt_AddP3
            // 
            bt_AddP3.BackColor = System.Drawing.SystemColors.Control;
            bt_AddP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            bt_AddP3.Location = new System.Drawing.Point(613, 390);
            bt_AddP3.Margin = new Padding(2);
            bt_AddP3.Name = "bt_AddP3";
            bt_AddP3.Size = new System.Drawing.Size(14, 14);
            bt_AddP3.TabIndex = 501;
            bt_AddP3.Text = "+";
            bt_AddP3.UseVisualStyleBackColor = false;
            bt_AddP3.Click += bt_AddP3_Click;
            // 
            // bt_AddP4
            // 
            bt_AddP4.BackColor = System.Drawing.SystemColors.Control;
            bt_AddP4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            bt_AddP4.Location = new System.Drawing.Point(575, 417);
            bt_AddP4.Margin = new Padding(2);
            bt_AddP4.Name = "bt_AddP4";
            bt_AddP4.Size = new System.Drawing.Size(14, 14);
            bt_AddP4.TabIndex = 502;
            bt_AddP4.Text = "+";
            bt_AddP4.UseVisualStyleBackColor = false;
            bt_AddP4.Click += bt_AddP4_Click;
            // 
            // bt_AddP5
            // 
            bt_AddP5.BackColor = System.Drawing.SystemColors.Control;
            bt_AddP5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            bt_AddP5.Location = new System.Drawing.Point(575, 439);
            bt_AddP5.Margin = new Padding(2);
            bt_AddP5.Name = "bt_AddP5";
            bt_AddP5.Size = new System.Drawing.Size(14, 14);
            bt_AddP5.TabIndex = 503;
            bt_AddP5.Text = "+";
            bt_AddP5.UseVisualStyleBackColor = false;
            bt_AddP5.Click += bt_AddP5_Click;
            // 
            // chbx_SlideExisting
            // 
            chbx_SlideExisting.Appearance = Appearance.Button;
            chbx_SlideExisting.Location = new System.Drawing.Point(378, 45);
            chbx_SlideExisting.Margin = new Padding(2);
            chbx_SlideExisting.Name = "chbx_SlideExisting";
            chbx_SlideExisting.Size = new System.Drawing.Size(46, 21);
            chbx_SlideExisting.TabIndex = 607;
            chbx_SlideExisting.Text = "Slide";
            chbx_SlideExisting.UseVisualStyleBackColor = true;
            // 
            // chbx_SlideNew
            // 
            chbx_SlideNew.Appearance = Appearance.Button;
            chbx_SlideNew.Location = new System.Drawing.Point(112, 44);
            chbx_SlideNew.Margin = new Padding(2);
            chbx_SlideNew.Name = "chbx_SlideNew";
            chbx_SlideNew.Size = new System.Drawing.Size(46, 21);
            chbx_SlideNew.TabIndex = 608;
            chbx_SlideNew.Text = "Slide";
            chbx_SlideNew.UseVisualStyleBackColor = true;
            // 
            // frm_Duplicates_Management
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(831, 878);
            Controls.Add(bt_AddP5);
            Controls.Add(bt_AddP4);
            Controls.Add(bt_AddP3);
            Controls.Add(bt_AddP2);
            Controls.Add(bt_AddP1);
            Controls.Add(label3);
            Controls.Add(txt_ExistingDuplicateOfID);
            Controls.Add(lbl_Frecv);
            Controls.Add(btn_SaveExisting);
            Controls.Add(txt_TuningFrecvNew);
            Controls.Add(txt_TuningFrecvExisting);
            Controls.Add(chbx_UkuleleNew);
            Controls.Add(chbx_MetalNew);
            Controls.Add(chbx_MetalExisting);
            Controls.Add(chbx_UkuleleExisting);
            Controls.Add(chbx_TVThemeNew);
            Controls.Add(chbx_AmateurCoverNew);
            Controls.Add(lbl_P5);
            Controls.Add(chbx_MidiExisting);
            Controls.Add(chbx_GameSoundtrackExisting);
            Controls.Add(btn_OpenNewFolder);
            Controls.Add(chbx_MidiNew);
            Controls.Add(chbx_GameSoundtrackNew);
            Controls.Add(btn_Replace_Brakets);
            Controls.Add(chbx_DeluxeExisting);
            Controls.Add(chbx_GreatestHitsExisting);
            Controls.Add(chbx_DeluxeNew);
            Controls.Add(chbx_GreatestHitsNew);
            Controls.Add(chbx_MedleyNew);
            Controls.Add(lbl_Attention);
            Controls.Add(chbx_CoverExisting);
            Controls.Add(chbx_CoverNew);
            Controls.Add(chbx_FeaturingExisting);
            Controls.Add(chbx_FeaturingNew);
            Controls.Add(chbx_KaraokeExisting);
            Controls.Add(chbx_KaraokeNew);
            Controls.Add(chbx_RemixNew);
            Controls.Add(chbx_RemixExisting);
            Controls.Add(chbx_DemoNew);
            Controls.Add(chbx_DemoExisting);
            Controls.Add(chbx_InTheWorksExisting);
            Controls.Add(chbx_InTheWorksNew);
            Controls.Add(chbx_DescriptionSave);
            Controls.Add(lbl_P4);
            Controls.Add(lbl_P3);
            Controls.Add(lbl_P2);
            Controls.Add(lbl_P1);
            Controls.Add(chbx_RemasteredExisting);
            Controls.Add(chbx_EPExisting);
            Controls.Add(chbx_UncensoredExisting);
            Controls.Add(chbx_SingleExisting);
            Controls.Add(chbx_InstrumentalExisting);
            Controls.Add(chbx_SoundtrackExisting);
            Controls.Add(chbx_FullAlbumExisting);
            Controls.Add(chbx_RemasteredNew);
            Controls.Add(chbx_FullAlbumNew);
            Controls.Add(chbx_UncensoredNew);
            Controls.Add(chbx_InstrumentalNew);
            Controls.Add(chbx_SoundtrackNew);
            Controls.Add(chbx_EPNew);
            Controls.Add(chbx_SingleNew);
            Controls.Add(lbl_ExistingIs_Original);
            Controls.Add(btn_OpenStandardization);
            Controls.Add(lbl_FileHash);
            Controls.Add(btn_CommentSimilar);
            Controls.Add(lbl_IDNew);
            Controls.Add(btn_Album2SortA);
            Controls.Add(lbl_YearNew);
            Controls.Add(lbl_YearExisting);
            Controls.Add(btn_AlbumSortNew);
            Controls.Add(btn_AlbumSortExisting);
            Controls.Add(label13);
            Controls.Add(lbl_AlbumSort);
            Controls.Add(txt_AlbumSortExisting);
            Controls.Add(txt_AlbumSortNew);
            Controls.Add(txt_YearNew);
            Controls.Add(txt_YearExisting);
            Controls.Add(btn_ShowInfoOthers);
            Controls.Add(txt_LenghtNew);
            Controls.Add(txt_LenghtExisting);
            Controls.Add(lbl_LenghtExisting);
            Controls.Add(lbl_Covers);
            Controls.Add(btn_AddStandard);
            Controls.Add(btn_NotADuplicate);
            Controls.Add(chbx_AcousticNew);
            Controls.Add(chbx_AcousticExisting);
            Controls.Add(txt_VersionExisting);
            Controls.Add(txt_VersionNew);
            Controls.Add(btn_AddPlatform);
            Controls.Add(txt_LiveDetailsNew);
            Controls.Add(chbx_LiveNew);
            Controls.Add(txt_LiveDetailsExisting);
            Controls.Add(chbx_LiveExisting);
            Controls.Add(txt_PlatformNew);
            Controls.Add(txt_PlatformExisting);
            Controls.Add(txt_FileDateNew);
            Controls.Add(txt_FileDateExisting);
            Controls.Add(picbx_AlbumArtPathExisting);
            Controls.Add(chbx_Sort);
            Controls.Add(btn_Artist2SortA);
            Controls.Add(btn_Title2SortT);
            Controls.Add(chbx_DeleteTemp);
            Controls.Add(btn_StopImport);
            Controls.Add(btn_AddAlternate);
            Controls.Add(chbx_UseBrakets);
            Controls.Add(lbl_Multitrack);
            Controls.Add(lbl_Size);
            Controls.Add(lblSoye);
            Controls.Add(txt_SizeExisting);
            Controls.Add(txt_SizeNew);
            Controls.Add(btn_AddAuthor);
            Controls.Add(btn_AddVersion1);
            Controls.Add(btn_AddTunning);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(btn_CoverNew);
            Controls.Add(btn_CoverExisting);
            Controls.Add(txt_MultiTrackExisting);
            Controls.Add(txt_MultiTrackNew);
            Controls.Add(chbx_MultiTrackNew);
            Controls.Add(chbx_MultiTrackExisting);
            Controls.Add(txt_AlternateNoNew);
            Controls.Add(txt_AlternateNoExisting);
            Controls.Add(chbx_IsAlternateExisting);
            Controls.Add(groupBox2);
            Controls.Add(btn_ArtistSortNew);
            Controls.Add(btn_ArtistSortExisting);
            Controls.Add(label18);
            Controls.Add(label4);
            Controls.Add(btn_ArtistNew);
            Controls.Add(btn_ArtistExisting);
            Controls.Add(btn_AlbumNew);
            Controls.Add(btn_AlbumExisting);
            Controls.Add(lbl_Artist);
            Controls.Add(lbl_Album);
            Controls.Add(btn_AuthorNew);
            Controls.Add(btn_AuthorExisting);
            Controls.Add(btn_TitleSortNew);
            Controls.Add(btn_TitleSortExisting);
            Controls.Add(btn_TitleNew);
            Controls.Add(btn_TitleExisting);
            Controls.Add(chbx_Autosave);
            Controls.Add(txt_AlbumExisting);
            Controls.Add(txt_ArtistExisting);
            Controls.Add(btn_RemoveOldNew);
            Controls.Add(chbx_IgnoreDupli);
            Controls.Add(lbl_diffCount);
            Controls.Add(label31);
            Controls.Add(label30);
            Controls.Add(label29);
            Controls.Add(label28);
            Controls.Add(label27);
            Controls.Add(btn_OpenMainDB);
            Controls.Add(label26);
            Controls.Add(lbl_IDExisting);
            Controls.Add(lblExisting);
            Controls.Add(lblNew);
            Controls.Add(btn_UpdateExisting);
            Controls.Add(label23);
            Controls.Add(txt_AlbumNew);
            Controls.Add(lbl_Platfdormtoolkit);
            Controls.Add(label21);
            Controls.Add(label20);
            Controls.Add(lbl_NewIs_Original);
            Controls.Add(chbx_IsOriginal);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbl_Reference);
            Controls.Add(lbl_DLCID);
            Controls.Add(lbl_Tuning);
            Controls.Add(lbl_Version);
            Controls.Add(lbl_Author);
            Controls.Add(lbl_Toolkit);
            Controls.Add(lbl_IsOriginal);
            Controls.Add(lbl_FileName);
            Controls.Add(lbl_ArtistSort);
            Controls.Add(lbl_TitleSort);
            Controls.Add(txt_FileNameExisting);
            Controls.Add(txt_FileNameNew);
            Controls.Add(txt_ArtistNew);
            Controls.Add(txt_DescriptionExisting);
            Controls.Add(txt_DescriptionNew);
            Controls.Add(lbl_AlbumArt);
            Controls.Add(txt_DLCIDExisting);
            Controls.Add(txt_DLCIDNew);
            Controls.Add(txt_TuningExisting);
            Controls.Add(txt_TuningNew);
            Controls.Add(txt_AuthorExisting);
            Controls.Add(txt_AuthorNew);
            Controls.Add(txt_IsOriginalExisting);
            Controls.Add(txt_IsOriginalNew);
            Controls.Add(txt_ToolkitExisting);
            Controls.Add(txt_ToolkitNew);
            Controls.Add(txt_TitleSortExisting);
            Controls.Add(txt_TitleSortNew);
            Controls.Add(txt_ArtistSortExisting);
            Controls.Add(txt_ArtistSortNew);
            Controls.Add(txt_TitleExisting);
            Controls.Add(lbl_Title);
            Controls.Add(txt_TitleNew);
            Controls.Add(picbx_AlbumArtPathNew);
            Controls.Add(btn_Update);
            Controls.Add(chbx_IsAlternateNew);
            Controls.Add(btn_Alternate);
            Controls.Add(btn_Ignore);
            Controls.Add(chbx_MedleyExisting);
            Controls.Add(chbx_AmateurCoverExisting);
            Controls.Add(chbx_TVThemeExisting);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            HelpButton = true;
            Margin = new Padding(2, 3, 2, 3);
            Name = "frm_Duplicates_Management";
            Text = "Duplicates Management";
            Load += DuplicatesManagement_Load;
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPathNew).EndInit();
            ((System.ComponentModel.ISupportInitialize)picbx_AlbumArtPathExisting).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txt_AlternateNoExisting).EndInit();
            ((System.ComponentModel.ISupportInitialize)txt_AlternateNoNew).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label16;
        private CueTextBox txt_FileNameExisting;
        private CueTextBox txt_FileNameNew;
        private CueTextBox txt_ArtistNew;
        private RichTextBox txt_DescriptionExisting;
        private RichTextBox txt_DescriptionNew;
        private Label lbl_AvailTrk;
        private Label lbl_AudioPreview;
        private Label lbl_AudioMain;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private CueTextBox txt_JSONRhythmExisting;
        private CueTextBox txt_JSONRhythmNew;
        private Label lbl_AlbumArt;
        private CueTextBox txt_JSONComboExisting;
        private CueTextBox txt_JSONComboNew;
        private CueTextBox txt_JSONBassExisting;
        private CueTextBox txt_JSONBassNew;
        private CueTextBox txt_JSONLeadExisting;
        private CueTextBox txt_JSONLeadNew;
        private CueTextBox txt_XMLRhythmExisting;
        private CueTextBox txt_XMLRhythmNew;
        private CueTextBox txt_XMLComboExisting;
        private CueTextBox txt_XMLComboNew;
        private CueTextBox txt_XMLBassExisting;
        private CueTextBox txt_XMLBassNew;
        private CueTextBox txt_PreviewExisting_1;
        private CueTextBox txt_PreviewNew_1;
        private CueTextBox txt_XMLLeadExisting;
        private CueTextBox txt_XMLLeadNew;
        private CueTextBox txt_AudioExisting_1;
        private CueTextBox txt_AudioNew_1;
        private CueTextBox txt_AvailTracksExisting_1;
        private CueTextBox txt_AvailTracksNew_1;
        private CueTextBox txt_DDExisting_1;
        private CueTextBox txt_DDNew_1;
        private CueTextBox txt_DLCIDExisting;
        private CueTextBox txt_DLCIDNew;
        private CueTextBox txt_TuningExisting;
        private CueTextBox txt_TuningNew;
        private CueTextBox txt_AuthorExisting;
        private CueTextBox txt_AuthorNew;
        private CueTextBox txt_IsOriginalExisting;
        private CueTextBox txt_IsOriginalNew;
        private CueTextBox txt_ToolkitExisting;
        private CueTextBox txt_ToolkitNew;
        private CueTextBox txt_TitleSortExisting;
        private CueTextBox txt_TitleSortNew;
        private CueTextBox txt_ArtistSortExisting;
        private CueTextBox txt_ArtistSortNew;
        private CueTextBox txt_TitleExisting;
        private Label lbl_Title;
        private CueTextBox txt_TitleNew;
        private PictureBox picbx_AlbumArtPathNew;
        private PictureBox picbx_AlbumArtPathExisting;
        private Button btn_Update;
        private CheckBox chbx_IsAlternateNew;
        private Button btn_Alternate;
        private Button btn_Ignore;
        private Label lbl_TitleSort;
        private Label lbl_ArtistSort;
        private Label lbl_FileName;
        private Label lbl_IsOriginal;
        private Label lbl_Toolkit;
        private Label lbl_Author;
        private Label lbl_Version;
        private Label lbl_Tuning;
        private Label lbl_DLCID;
        private Label lbl_DD;
        private Label lbl_AvailableTracks;
        private Label lbl_Audio;
        private Label lbl_Preview;
        private Label lbl_XMLLead;
        private Label lbl_XMLBass;
        private Label lbl_XMLCombo;
        private Label lbl_XMLRhythm;
        private Label lbl_JSONLead;
        private Label lbl_JSONBass;
        private Label lbl_JSONCombo;
        private Label lbl_JSONRhythm;
        private Label lbl_Reference;
        private Label label1;
        private Label label2;
        private CheckBox chbx_IsOriginal;
        private Label label17;
        private Label lbl_NewIs_Original;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private CueTextBox txt_AlbumNew;
        private Label label23;
        private Button btn_UpdateExisting;
        private Label lblNew;
        private Label lblExisting;
        private Label lbl_IDExisting;
        private Label label26;
        private Button btn_OpenMainDB;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label30;
        private Label label31;
        private Label lbl_diffCount;
        private CheckBox chbx_IgnoreDupli;
        private Button btn_RemoveOldNew;
        private Label lbl_Vocals;
        private Label lbl_txt_Vocals;
        private CueTextBox txt_VocalsExisting_1;
        private CueTextBox txt_VocalsNew_1;
        private CueTextBox txt_AlbumExisting;
        private CueTextBox txt_ArtistExisting;
        private Button btn_TitleNew;
        private Button btn_TitleExisting;
        private Button btn_TitleSortNew;
        private Button btn_TitleSortExisting;
        private Button btn_AuthorNew;
        private Button btn_AuthorExisting;
        private Label lbl_Album;
        private Label lbl_Artist;
        private Button btn_AlbumNew;
        private Button btn_AlbumExisting;
        private Button btn_ArtistNew;
        private Button btn_ArtistExisting;
        private Label label4;
        private Label label18;
        private Button btn_ArtistSortNew;
        private Button btn_ArtistSortExisting;
        private GroupBox groupBox2;
        private CheckBox chbx_IsAlternateExisting;
        private NumericUpDown txt_AlternateNoExisting;
        private NumericUpDown txt_AlternateNoNew;
        private CheckBox chbx_MultiTrackExisting;
        private CheckBox chbx_MultiTrackNew;
        private ComboBox txt_MultiTrackNew;
        private ComboBox txt_MultiTrackExisting;
        private Button btn_CoverNew;
        private Button btn_CoverExisting;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label lbl_CustomsForge_ReleaseNotes;
        private CueTextBox txt_CustomsForge_ReleaseNotesNew;
        private CueTextBox txt_CustomsForge_ReleaseNotesExisting;
        private Label label59;
        private CueTextBox txt_YouTube_LinkNew;
        private CueTextBox txt_CustomsForge_LinkExisting;
        private CueTextBox txt_YouTube_LinkExisting;
        private Label lbl_CustomsForge_Like;
        private CueTextBox txt_CustomsForge_LinkNew;
        private Label lbl_CustomsForge_LinkNew;
        private Label lbl_YouTube_LinkNew;
        private CueTextBox txt_CustomsForge_LikeNew;
        private Label lbfl_YouTube_Link;
        private CueTextBox txt_CustomsForge_LikeExisting;
        private Label label33;
        private Label label32;
        private Button btn_AddDD;
        private Button btn_AddTracks;
        private Button btn_AddTunning;
        private Button btn_AddVersion1;
        private Button btn_AddAuthor;
        private Label lbl_Existing;
        private Label lbl_New;
        private Button btn_AddAge;
        private Label lblSoye;
        private CueTextBox txt_SizeExisting;
        private CueTextBox txt_SizeNew;
        private Label lbl_Size;
        private Label lbl_Multitrack;
        private Button btn_GoToNew;
        private Button btn_GoToExisting;
        private Button btn_PlayPreviewNew;
        private Button btn_PlayAudioNew;
        private Button btn_PlayPreviewExisting;
        private Button btn_PlayAudioExisting;
        private CheckBox chbx_UseBrakets;
        private Button btn_AddAlternate;
        private Button btn_StopImport;
        private CheckBox chbx_DeleteTemp;
        private Label lbl_tonediff;
        private Button btn_Title2SortT;
        private Button btn_Artist2SortA;
        private Label lbl_DateExisting;
        private Label lbl_DateNew;
        private Label lbl_previewFootnote;
        private CheckBox chbx_Autosave;
        private CheckBox chbx_Sort;
        private CueTextBox txt_FileDateExisting;
        private CueTextBox txt_FileDateNew;
        private CueTextBox txt_PlatformNew;
        private CueTextBox txt_PlatformExisting;
        private Button btn_WM_Rhythm;
        private Button btn_WM_Combo;
        private Button btn_WM_Bass;
        private Button btn_WM_Leads;
        private Button btn_TN_Rhythm;
        private Button btn_TN_Combo;
        private Button btn_TN_Bass;
        private Button btn_TN_Lead;
        private Button btn_WM_Vocals;
        private CheckBox chbx_LiveExisting;
        private CueTextBox txt_LiveDetailsExisting;
        private CueTextBox txt_LiveDetailsNew;
        private CheckBox chbx_LiveNew;
        private Button btn_AddPlatform;
        private CueTextBox txt_VersionNew;
        private CueTextBox txt_VersionExisting;
        private CheckBox chbx_AcousticExisting;
        private CheckBox chbx_AcousticNew;
        private Button btn_NotADuplicate;
        private Button btn_AddStandard;
        private ToolTip toolTip1;
        private Label lbl_Covers;
        private CheckBox btn_UseDates;
        private CueTextBox txt_LenghtNew;
        private CueTextBox txt_LenghtExisting;
        private Label lbl_LenghtExisting;
        private Button btn_ShowInfoOthers;
        private CueTextBox txt_YearExisting;
        private CueTextBox txt_YearNew;
        private Button btn_AlbumSortNew;
        private Button btn_AlbumSortExisting;
        private Label label13;
        private Label lbl_AlbumSort;
        private CueTextBox txt_AlbumSortExisting;
        private CueTextBox txt_AlbumSortNew;
        private Label lbl_YearExisting;
        private Label lbl_YearNew;
        private Button btn_Album2SortA;
        private Label lbl_IDNew;
        private Button btn_CommentSimilar;
        private Label lbl_FileHash;
        private Button btn_OpenStandardization;
        private Label lbl_ExistingIs_Original;
        private CheckBox chbx_SingleNew;
        private CheckBox chbx_EPNew;
        private CheckBox chbx_SoundtrackNew;
        private CheckBox chbx_InstrumentalNew;
        private CheckBox chbx_UncensoredNew;
        private CheckBox chbx_FullAlbumNew;
        private CheckBox chbx_RemasteredNew;
        private CheckBox chbx_FullAlbumExisting;
        private CheckBox chbx_InstrumentalExisting;
        private CheckBox chbx_SoundtrackExisting;
        private CheckBox chbx_SingleExisting;
        private CheckBox chbx_UncensoredExisting;
        private CheckBox chbx_EPExisting;
        private CheckBox chbx_RemasteredExisting;
        private Label lbl_P1;
        private Label lbl_P2;
        private Label lbl_P3;
        private Label lbl_P4;
        private CheckBox chbx_DescriptionSave;
        private CheckBox chbx_InTheWorksNew;
        private CheckBox chbx_InTheWorksExisting;
        private CheckBox chbx_DemoExisting;
        private CheckBox chbx_DemoNew;
        private CheckBox chbx_RemixExisting;
        private CheckBox chbx_RemixNew;
        private CheckBox chbx_KaraokeNew;
        private CheckBox chbx_KaraokeExisting;
        private CheckBox chbx_FeaturingNew;
        private CheckBox chbx_FeaturingExisting;
        private CheckBox chbx_CoverNew;
        private CheckBox chbx_CoverExisting;
        private Label lbl_Attention;
        private CheckBox chbx_MultiStringsExisting;
        private CheckBox chbx_MedleyExisting;
        private CheckBox chbx_MultiStringsNew;
        private CheckBox chbx_MedleyNew;
        private CheckBox chbx_CapoExisting;
        private CheckBox chbx_CapoNew;
        private CheckBox chbx_DeluxeNew;
        private CheckBox chbx_GreatestHitsNew;
        private CheckBox chbx_DeluxeExisting;
        private CheckBox chbx_GreatestHitsExisting;
        private Button btn_Replace_Brakets;
        private CheckBox chbx_GameSoundtrackNew;
        private CheckBox chbx_MidiNew;
        private Button btn_GoImport;
        private Button btn_OpenNewFolder;
        private CheckBox chbx_MidiExisting;
        private CheckBox chbx_GameSoundtrackExisting;
        private Label lbl_P5;
        private CheckBox chbx_AmateurCoverNew;
        private CheckBox chbx_AmateurCoverExisting;
        private CheckBox chbx_TVThemeNew;
        private CheckBox chbx_TVThemeExisting;
        private CueTextBox txt_VocalsExisting;
        private CueTextBox txt_PreviewExisting;
        private CueTextBox txt_AudioExisting;
        private CueTextBox txt_AvailTracksExisting;
        private CueTextBox txt_DDExisting;
        private CueTextBox txt_VocalsNew;
        private CueTextBox txt_PreviewNew;
        private CueTextBox txt_AudioNew;
        private CueTextBox txt_AvailTracksNew;
        private CueTextBox txt_DDNew;
        private CheckBox chbx_UkuleleExisting;
        private CheckBox chbx_MetalExisting;
        private CheckBox chbx_MetalNew;
        private CheckBox chbx_UkuleleNew;
        private CueTextBox txt_TuningFrecvExisting;
        private CueTextBox txt_TuningFrecvNew;
        private Button btn_SaveExisting;
        private Label lbl_Frecv;
        private Label label3;
        private CueTextBox txt_ExistingDuplicateOfID;
        private Label lbl_Platfdormtoolkit;
        private Button bt_AddP1;
        private Button bt_AddP2;
        private Button bt_AddP3;
        private Button bt_AddP4;
        private Button bt_AddP5;
        private CheckBox chbx_SlideNew;
        private CheckBox chbx_SlideExisting;
    }
}
