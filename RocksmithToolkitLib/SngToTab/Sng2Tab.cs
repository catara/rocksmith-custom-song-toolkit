﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using RocksmithToolkitLib.DLCPackage;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.Sng;

namespace RocksmithToolkitLib.SngToTab
{
    public class Sng2Tab : IDisposable
    {
        private string tmpWorkDir
        {
            get { return Path.Combine(Path.GetTempPath()); }
        }

        public void Convert(string sngFilePath, string outputDir, bool allDif)
        {
            SngFile sngFile = new SngFile(sngFilePath);

            if (String.IsNullOrEmpty(sngFile.Metadata.Arrangement))
                return; // Vocal

            int maxDifficulty = Common.getMaxDifficulty(sngFile);

            int[] difficulties;
            if (allDif)
                difficulties = Enumerable.Range(0, maxDifficulty + 1).ToArray();
            else // if (max)
                //  difficulties = new int[] { maxDifficulty };
                difficulties = new int[] { 255 };

            foreach (int d in difficulties)
            {
                TabFile tabFile = new TabFile(sngFile, d);

                var outputFileName = String.Empty;
                if (sngFile != null && sngFile.Metadata != null)
                    if (sngFile.Metadata.Artist == "DUMMY")
                        outputFileName = String.Format("{0}", sngFile.Metadata.SongTitle);
                    else
                        outputFileName = String.Format("{0} - {1}", sngFile.Metadata.Artist, sngFile.Metadata.SongTitle);
                else
                    outputFileName = String.Format("{0}", "Unknown Song");

                outputFileName += (difficulties.Length != 1) ? String.Format(" (level {0:D2})", d) : "";
                outputFileName = outputFileName.GetValidName(true);
                var outputFilePath = Path.Combine(outputDir, outputFileName + ".txt");

                using (TextWriter tw = new StreamWriter(outputFilePath))
                {
                    tw.Write(tabFile.ToString());
                }
            }
        }

        public void ExtractBeforeConvert(string inputPath, string outputDir, bool allDif)
        {
            string sng2tabDir = Path.Combine(tmpWorkDir, "sng2tab");
            // unpacker creates the sng2tabDir directory if it doesn't exist
            Packer.Unpack(inputPath, sng2tabDir);
            string unpackedDir = Path.Combine(sng2tabDir,
                                              Path.GetFileNameWithoutExtension(inputPath) +
                                              String.Format("_{0}", inputPath.GetPlatform().platform.ToString()));
            string[] sngFiles = Directory.GetFiles(unpackedDir, "*.sng", SearchOption.AllDirectories);

            foreach (var sngFilePath in sngFiles)
            {
                Convert(sngFilePath, outputDir, allDif);
            }

            DirectoryExtension.SafeDelete(sng2tabDir);
        }

        public void Dispose()
        {
        }
    }
}
