﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using RocksmithToolkitLib.Extensions;
using System.Linq;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;
using System.Diagnostics;
using RocksmithToolkitLib.XmlRepository;

namespace RocksmithToolkitLib.Ogg
{
    public static class Wwise
    {
        static OggFile.WwiseVersion Selected { get; set; }

        /// <summary>
        /// Covert Wav to Wem using WwiseCLI.exe
        /// for faster conversion, source path should be wav file
        /// </summary>
        /// <param name="wavSourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="audioQuality"></param>
        public static void Wav2Wem(string wavSourcePath, string destinationPath, int audioQuality)
        {
            try
            {
                var wwiseCLIPath = GetWwisePath();
                var wwiseTemplateDir = LoadWwiseTemplate(wavSourcePath, audioQuality);

                // console writes may be captured by starting toolkit in a command window an redirecting the output to a file
                // e.g., ‘RocksmithToolkitGUI.exe >console.log’ 
                Console.WriteLine("WwiseCLI:\n\'" + wwiseCLIPath + "\'\n\nTemplate:\n\'" + wwiseTemplateDir + "\'");

                // apply magicDust to WwiseCLI.exe to force conversions (known Wwise2010 issue)
                ExternalApps.Wav2Wem(wwiseCLIPath, wwiseTemplateDir, 10);
                GetWwiseFiles(destinationPath, wwiseTemplateDir);
            }
            catch (Exception ex)
            {
                //overridden ex, can't get real ex/msg, use log + throw;
                throw new Exception("Wwise audio file conversion failed: " + ex.Message + Environment.NewLine);
            }
        }

        public static string GetWwisePath()
        {
            // support for Wwise v2013.2.x v2014.1.x 2015.1.x or 2016.2.x series
            // Wwise may not be installed in the default location so use the Configuration Wwise Path if entered
            var wwiseRoot = ConfigRepository.Instance()["general_wwisepath"];
            // otherwise use the WWISEROOT Environmental Variable
            if (String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                wwiseRoot = Environment.GetEnvironmentVariable("WWISEROOT");

            if (String.IsNullOrEmpty(wwiseRoot))
                //<<<<<<< HEAD
                throw new FileNotFoundException("Could not find Audiokinetic Wwise installation." + Environment.NewLine +
                    "Please confirm that either Wwise v2013.2.x v2014.1.x 2015.1.x or 2016.2.xx or 2017.1.xx or 2018.1.x or 2019.1.x series is installed." + Environment.NewLine);
            //=======
            //                throw new ApplicationException("Could not find Audiokinetic Wwise installation." + new string(' ', 2) +
            //                    "Download and install Wwise v2013.2.x from: http://ignition.customsforge.com/cfsm/wwise" + Environment.NewLine);
            //>>>>>>> pr/40

            var wwiseCLIPath = Directory.EnumerateFiles(wwiseRoot, "WwiseCLI.exe", SearchOption.AllDirectories);
            if (!wwiseCLIPath.Any())
            {
                // Check for wwise root if user has bad custom path to wwise
                if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WWISEROOT")))
                    wwiseCLIPath = Directory.EnumerateFiles(Environment.GetEnvironmentVariable("WWISEROOT"), "WwiseCLI.exe", SearchOption.AllDirectories);
            }

            if (!wwiseCLIPath.Any())
                //<<<<<<< HEAD
                throw new FileNotFoundException("Could not find WwiseCLI.exe in " + wwiseRoot + Environment.NewLine +
                    "Please confirm that either Wwise v2013.2.x v2014.1.x 2015.1.x or 2016.2.x or 2017.1.xx or 2018.1.x or 2019.1.xx series is installed." + Environment.NewLine);
            //=======
            //                throw new FileNotFoundException("Could not find 'WwiseCLI.exe' in " + wwiseRoot + " subfolders." + new string(' ', 2) +
            //                    "Download and install Wwise v2013.2.x from: http://ignition.customsforge.com/cfsm/wwise" + Environment.NewLine);
            //>>>>>>> pr/40

            //win32 = 32bit x64 = 64bit
            string wwiseCLIexe = wwiseCLIPath.AsParallel().SingleOrDefault(e => e.Contains("Authoring\\Win32"));
            // use the 64bit version if it is installed
            if (Environment.Is64BitOperatingSystem)
            {
                var etmp = wwiseCLIPath.AsParallel().FirstOrDefault(e => e.Contains("Authoring\\x64"));
                if (!String.IsNullOrEmpty(etmp))
                    wwiseCLIexe = etmp;
            }

            // a final error check
            var wwiseVersion = FileVersionInfo.GetVersionInfo(wwiseCLIexe).ProductVersion;

            if (wwiseVersion.StartsWith("2010.3"))
                Selected = OggFile.WwiseVersion.Wwise2010;
            else if (wwiseVersion.StartsWith("2013.2"))
                Selected = OggFile.WwiseVersion.Wwise2013;
            else if (wwiseVersion.StartsWith("2014.1"))
                Selected = OggFile.WwiseVersion.Wwise2014;
            else if (wwiseVersion.StartsWith("2015.1"))
                Selected = OggFile.WwiseVersion.Wwise2015;
            else if (wwiseVersion.StartsWith("2016.2"))
                Selected = OggFile.WwiseVersion.Wwise2016;
            else if (wwiseVersion.StartsWith("2017.1"))
                Selected = OggFile.WwiseVersion.Wwise2017;
            else if (wwiseVersion.StartsWith("2018.1"))
                Selected = OggFile.WwiseVersion.Wwise2018;
            else if (wwiseVersion.StartsWith("2019.1"))
                Selected = OggFile.WwiseVersion.Wwise2019;
            else if (wwiseVersion.StartsWith("2019.2"))
                Selected = OggFile.WwiseVersion.Wwise2019;
            else if (wwiseVersion.StartsWith("2019.3"))
                Selected = OggFile.WwiseVersion.Wwise2019;
            // add support for new versions here, code is expandable
            //else if (wwiseVersion.StartsWith("xxxx.x"))
            //    Selected = OggFile.WwiseVersion.WwiseXXXX;
            else
                Selected = OggFile.WwiseVersion.None;

            if (Selected == OggFile.WwiseVersion.None)
                //<<<<<<< HEAD
                throw new FileNotFoundException("You have no compatible version of Audiokinetic Wwise installed." + Environment.NewLine +
                    "Install supportend Wwise version, which are v2013.2.x || v2014.1.x || v2015.1.x || v2016.2.x series || v2017.1.x series || v2018.1.x series || v2019.1.x series  " + Environment.NewLine +
                    "if you would like to use toolkit's Wwise autoconvert feature.   Did you remember to set the Wwise" + Environment.NewLine +
                    "installation path in the toolkit General Config menu?" + Environment.NewLine);
            //=======
            //                throw new ApplicationException("Could not find an installed compatible version of Audiokinetic Wwise." + new string(' ', 2) +
            //                    "Download and install Wwise v2013.2.x from: http://ignition.customsforge.com/cfsm/wwise" + new string(' ', 5) +
            //                    "Be sure to set the 'Wwise Path' in the General Config menu if you have a custom installation." + Environment.NewLine);
            //>>>>>>> pr/40

            return wwiseCLIexe;
        }

        /// <summary>
        /// Unpack\modify\load wwise template.
        /// </summary>
        /// <returns>Modified wwise template directory.</returns>
        /// <param name="sourcePath">Source path.</param>
        /// <param name="audioQuality">Audio quality (-2 to 10).</param>
        public static string LoadWwiseTemplate(string sourcePath, int audioQuality)
        {
            // TODO: add Wwise template validation to ExternalApps
            var templateDir = Path.Combine(ExternalApps.TOOLKIT_ROOT, "Template");

            // Unpack required template here, based on Wwise version installed.
            switch (Selected)
            {
                // add legacy support for RS1 CDLC here
                case OggFile.WwiseVersion.Wwise2010:
                // add support for new versions of Wwise for RS2014 here
                case OggFile.WwiseVersion.Wwise2013:
                case OggFile.WwiseVersion.Wwise2014:
                case OggFile.WwiseVersion.Wwise2015:
                case OggFile.WwiseVersion.Wwise2016:
                case OggFile.WwiseVersion.Wwise2017:
                case OggFile.WwiseVersion.Wwise2018:
                case OggFile.WwiseVersion.Wwise2019:
                    // for fewer headaches ... start with fresh Wwise 2019 Template
                    if (Directory.Exists(templateDir) && Selected == OggFile.WwiseVersion.Wwise2019) ///bcapi
                        IOExtension.DeleteDirectory(templateDir, true);


                    ExtractTemplate(Path.Combine(ExternalApps.TOOLKIT_ROOT, Selected + ".tar.bz2"));
                    break;
                default:
                    throw new FileNotFoundException("<ERROR> Wwise path is incompatible." + Environment.NewLine);
            }

            var workUnitPath = Path.Combine(templateDir, "Interactive Music Hierarchy", "Default Work Unit.wwu");
            using (var sr = new StreamReader(File.OpenRead(workUnitPath)))
            {
                var workUnit = sr.ReadToEnd();
                sr.Close();
                workUnit = workUnit.Replace("%QF1%", Convert.ToString(audioQuality));

                if (Selected != OggFile.WwiseVersion.Wwise2010)
                    workUnit = workUnit.Replace("%QF2%", "4"); //preview audio

                var tw = new StreamWriter(workUnitPath, false);
                tw.Write(workUnit);
                tw.Flush();
            }

            // use IOExtensions here for better control
            // deleting GeneratedSoundBanks gives new hex value to wem/ogg files
            var bnk = Path.Combine(templateDir, "GeneratedSoundBanks");
            if (Directory.Exists(bnk))
                IOExtension.DeleteDirectory(bnk, true);

            var orgSfxDir = Path.Combine(templateDir, "Originals", "SFX");
            if (Directory.Exists(Path.Combine(templateDir, "Originals")))
                IOExtension.DeleteDirectory(Path.Combine(templateDir, "Originals"), true);
            IOExtension.MakeDirectory(orgSfxDir);

            var cacheWinSfxDir = Path.Combine(templateDir, ".cache", "Windows", "SFX");
            if (Directory.Exists(Path.Combine(templateDir, ".cache")))
                IOExtension.DeleteDirectory(Path.Combine(templateDir, ".cache"), true);
            IOExtension.MakeDirectory(cacheWinSfxDir);

            var vcache = Directory.EnumerateFiles(templateDir, "Template.*.validationcache").FirstOrDefault();
            if (File.Exists(vcache))
                IOExtension.DeleteFile(vcache);

            if (Selected != OggFile.WwiseVersion.Wwise2010)
            {
                var dirName = Path.GetDirectoryName(sourcePath);
                var fileName = Path.GetFileNameWithoutExtension(sourcePath);
                var dirFileName = Path.Combine(dirName, fileName);
                var sourcePreviewWave = String.Format("{0}_{1}.wav", dirFileName, "preview");
                sourcePreviewWave = sourcePreviewWave.Replace("_preview_fixed_preview", "_preview_fixed");///bcapi

                if (File.Exists(sourcePreviewWave)) //bcapi
                    IOExtension.CopyFile(sourcePreviewWave, Path.Combine(orgSfxDir, "Audio_preview.wav"), true, false);
                else try///bcapi
                    {
                        IOExtension.CopyFile(sourcePath, Path.Combine(orgSfxDir, "Audio_preview.wav"), true, false);
                    }
                    catch (Exception ex)
                    {
                        //var timestamp = GenericFunctions.UpdateLog(timestamp, "error at copy wav: "+ sourcePreviewWave, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "MainDB", null, null);
                    }
            }

            IOExtension.CopyFile(sourcePath, Path.Combine(orgSfxDir, "Audio.wav"), true, false);

            return templateDir;
        }

        public static void ExtractTemplate(string packedTemplatePath)
        {
            if (!File.Exists(packedTemplatePath))
                throw new Exception("<ERROR> Could not find packed template: " + packedTemplatePath + Environment.NewLine + "Try re-installing the toolkit ..." + Environment.NewLine);

            var templateDir = Path.Combine(ExternalApps.TOOLKIT_ROOT, "Template");
            IOExtension.DeleteDirectory(templateDir);

            using (var packedTemplate = File.OpenRead(packedTemplatePath))
            using (var bz2 = new BZip2InputStream(packedTemplate))
            using (var tar = TarArchive.CreateInputTarArchive(bz2))
            {
                tar.ExtractContents(ExternalApps.TOOLKIT_ROOT);
            }
        }

        public static void GetWwiseFiles(string destinationPath, string wwiseTemplateDir)
        {
            var wemDir = Path.Combine(".cache", "Windows", "SFX"); //could be platform dependent like "Mac" or "PS3"
            var wemPath = Path.Combine(wwiseTemplateDir, wemDir);
            var wemPathInfo = new DirectoryInfo(wemPath);
            Console.WriteLine("Wwise '.cache': " + wemPath);

            if (!wemPathInfo.Exists)
                throw new FileNotFoundException("Could not find Wwise template .cache Windows SFX directory" + Environment.NewLine);

            var fileExt = ".wem";
            if (Selected == OggFile.WwiseVersion.Wwise2010)
                fileExt = ".ogg";

            var srcPaths = wemPathInfo.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Where(fi => fi.FullName.ToLower().EndsWith(fileExt)).ToList();
            if (!srcPaths.Any())
                throw new Exception("<ERROR> Did not find any converted Wwise audio files ..." + Environment.NewLine);

            if (Selected != OggFile.WwiseVersion.Wwise2010 && srcPaths.Count < 2)
                throw new Exception("<ERROR> Did not find converted Wwise audio and preview files ..." + Environment.NewLine);

            var destPreviewPath = string.Format("{0}_preview.wem", destinationPath.Substring(0, destinationPath.LastIndexOf(".", StringComparison.Ordinal)));
            foreach (var srcPath in srcPaths)
            {
                //fix headers for wwise v2016 wem's //bcapi2019
                if ((int)Selected >= (int)OggFile.WwiseVersion.Wwise2019)
                    OggFile.DowngradeWemVersion(srcPath.FullName, srcPath.Name.Contains("_preview_") ? destPreviewPath : destinationPath);
                else
                    File.Copy(srcPath.FullName, srcPath.Name.Contains("_preview_") ? destPreviewPath : destinationPath, true);
            }
        }


    }
}