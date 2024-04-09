using System.Collections.Generic;
using RocksmithToolkitLib.DLCPackage;

namespace RocksmithToolkitGUI.DLCManager
{
    partial class PackNew
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
        private System.Windows.Forms.LinkLabel lbl_LinkYB;
        private System.Windows.Forms.Button btn_B3;
        private CueTextBox txt_TabLinks;
        private CueTextBox txt_BasedOnYB;
        private CueTextBox txt_YBLink;
        private CueTextBox txt_Author;
        private System.Windows.Forms.RichTextBox txt_toDos;
        private System.Windows.Forms.RichTextBox txt_Description;
        private CueTextBox txt_BasedOnCF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Settings;
        private CueTextBox txt_CDLC_Name;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Album2SortA;
        private System.Windows.Forms.LinkLabel lbl_LinkUG;
        private System.Windows.Forms.CheckBox chbx_SaveInDB;
        private System.Windows.Forms.CheckBox chbx_SaveInVerisonInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txt_ToneDetails;
        private System.Windows.Forms.LinkLabel lbl_LinkCF;
        private System.Windows.Forms.Button btn_Spotify;
        private CueTextBox txt_TrackNo;
        private CueTextBox txt_Spotify;
        private System.Windows.Forms.LinkLabel lbl_LinkTN;
        private System.Windows.Forms.LinkLabel lbl_LinkS;
        private System.Windows.Forms.CheckBox chbx_SaveRemotely;
        private CueTextBox txt_CDLCID;
        private CueTextBox txt_Version;
        private System.Windows.Forms.Button btn_DefaultAuthor;
        private CueTextBox txt_EoFPath;
        private CueTextBox txt_UpdateDate;
        private CueTextBox txt_PackageDate;
        private CueTextBox txt_GPFilePath;
        private System.Windows.Forms.Button btn_DBFolder;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_EoFPath;
        private CueTextBox txt_PrevDate;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.IContainer components;
    }
}