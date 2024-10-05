using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class DistribXMLNotes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer clomponents = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (clomponents != null))
            {
                clomponents.Dispose();
            }
            base.Dispose(disposing);
        }

        //#region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponentt()
        {

            this.SuspendLayout();
            // 
            // DataGridView1
            // 

        }
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel lbl_Link;
        private System.Windows.Forms.Button btn_B3;
        private CueTextBox txt_TabLinks;
        private CueTextBox txt_BasedOnYB;
        private CueTextBox txt_YBLink;
        private CueTextBox txt_Author;
        private System.Windows.Forms.RichTextBox txt_toDos;
        private CueTextBox txt_BasedOnCF;
        private System.Windows.Forms.Label lbl_Settings;
        private CueTextBox txt_CDLC_Name;
        private System.Windows.Forms.Button btn_MoveAllNotesAfter;
        private System.Windows.Forms.Button btn_addcorrection;
        private System.Windows.Forms.Button btn_OpenXML;
        private System.Windows.Forms.Button btn_Album2SortA;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chbx_SaveInDB;
        private System.Windows.Forms.CheckBox chbx_SaveInVerisonInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txt_ToneDetails;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btn_Spotify;
        private CueTextBox txt_TrackNo;
        private CueTextBox txt_Spotify;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.CheckBox chbx_SaveRemotely;
        private CueTextBox txt_CDLCID;
        private CueTextBox txt_Version;
        private System.Windows.Forms.Button btm_DefaultAuthor;
        private CueTextBox txt_EoFPath;
        private CueTextBox txt_UpdateDate;
        private CueTextBox txt_PackageDate;
        private CueTextBox txt_GPFilePath;
        private System.Windows.Forms.Button btn_DBFolder;
        private System.Windows.Forms.Label lbl_Comments;
        private System.Windows.Forms.Button btn_Estimate;
        private System.Windows.Forms.DateTimePicker txt_PreviewStart;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_EOF;
        private System.Windows.Forms.Button btn_OpenGP5;
        private System.Windows.Forms.CheckBox chbx_BassToo;
        private System.Windows.Forms.Label lbl_Track;
        private System.Windows.Forms.TextBox txt_XMLPath;
        private System.Windows.Forms.TextBox txt_TempPath;
        private System.Windows.Forms.Label lbl_TempFolders;
        private System.Windows.Forms.TextBox txt_Ttime;
        private System.Windows.Forms.DateTimePicker txt_songStart;
        private System.Windows.Forms.DateTimePicker txt_songLast;
        private System.Windows.Forms.DateTimePicker xml_last;
        private System.Windows.Forms.DateTimePicker xml_first;
        private System.Windows.Forms.TextBox txt_GP5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker txt_Correction;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox xml_firstMili;
        private System.Windows.Forms.TextBox xml_lastMili;
        private System.Windows.Forms.TextBox txt_songLastMili;
        private System.Windows.Forms.TextBox txt_songStartMili;
        private System.Windows.Forms.TextBox txt_CorrectionMili;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_RestoreXML;
        private System.Windows.Forms.ComboBox cmb_Sections;
        private System.Windows.Forms.CheckedListBox cmb_Tracks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox txt_Description;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Distrib;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox txt_Sections;
        private System.Windows.Forms.Button btn_backup;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_CleanDescr;
        private System.Windows.Forms.Button btn_CleanStartSong;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chbx_;
        private System.Windows.Forms.CheckBox chbx_removehandshapes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_P3LNMili;
        private System.Windows.Forms.TextBox txt_P3FNMili;
        private System.Windows.Forms.DateTimePicker txt_P3LN;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_P3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker txt_P3FN;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_ApplyP3;
        private System.Windows.Forms.Button btn_AddSections;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_RealLastNoteMili;
        private System.Windows.Forms.DateTimePicker txt_RealLastNote;
        private System.Windows.Forms.TextBox txt_Percentage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_ApplyP1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_corr;
        private System.Windows.Forms.Button btn_ApplyP2;
        private System.Windows.Forms.TextBox txt_PercTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_DiffLenght;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Lastnote;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_ExpctLenght;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_CurrentLenght;
        private System.Windows.Forms.TextBox txt_MaxRealLenght;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_SyncPh2Sect;
        private System.Windows.Forms.Button btn_Reload;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Button btn_VisualDistrib;
        private System.Windows.Forms.TextBox txt_Backup;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
    }
}