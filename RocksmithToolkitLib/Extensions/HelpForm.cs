﻿using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace RocksmithToolkitLib.Extensions
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            this.Text = String.Format("Help for {0}", AssemblyTitle);
            rtbBlank.BackColor = rtbNotes.BackColor;

            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = @"https://www.rscustom.net";
            linkLabel1.Links.Add(link);
            PopulateRichText();
        }

        public void PopulateRichText(Stream streamRtfNotes = null, bool wordWrap = true)
        {
            if (streamRtfNotes == null)
            {
                this.Size = new Size(550, 132);
                rtbNotes.Text = "Additional help will be displayed here when available.";
            }
            else
            {
                this.Size = new Size(780, 450);
                rtbNotes.LoadFile(streamRtfNotes, RichTextBoxStreamType.RichText);
            }
        }

        public void PopulateAsciiText(string textNotes = "")
        {
            if (String.IsNullOrEmpty(textNotes))
            {
                this.Size = new Size(550, 132);
                rtbNotes.Text = "Additional help will be displayed here when available.";
            }
            else
            {
                this.Size = new Size(780, 450);
                rtbNotes.Font = new Font("Arial", 11.0F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                rtbNotes.Text = textNotes;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }


    }
}
