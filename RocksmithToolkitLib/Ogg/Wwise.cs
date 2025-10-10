using System;
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
using Windows.Networking.Sockets;

namespace RocksmithToolkitLib.Ogg
{
    public static class Wwise
    {
        static OggFile.WwiseVersion Selected { get; set; }

        public static DateTime UpdateLog(DateTime dt, string txt, bool bbl, string tmpPath, string MultithreadNo, string form)
        {
            DateTime dtt = System.DateTime.Now;
            string logPath = ConfigRepository.Instance()["dlcm_LogPath"] == "" ? ConfigRepository.Instance()["dlcm_TempPath"] + "\\0_log" : ConfigRepository.Instance()["dlcm_LogPath"];
            var ismaindb = "";

            var ii = Math.Abs(Math.Round((dt - dtt).TotalSeconds, 2)).ToString().PadLeft(4, '0');

            if (form == "MainDB")
                ismaindb = "maindb";

            Random randomp = new Random();// Write the string to a file. packid+
            var packid = 0;
            packid = randomp.Next(0, 100000);
            var fn = (logPath == null || !Directory.Exists(logPath) ? tmpPath + "\\0_log" : logPath) + "\\" + "current_" + ismaindb + "temp" + MultithreadNo + ".txt";
            try
            {
                if (File.Exists(fn))
                {
                    using (StreamWriter sw = File.AppendText(fn))
                    {
                        sw.WriteLine(dtt.ToString() + " - " + ii.ToString() + " - " + txt.ToString());// This text is always added, making the file longer over time if it is not deleted.
                    }
                }
            }
            catch (Exception ex) { var tsst = "Erro ..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", ""); }
            return dtt;
        }

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
                try
                {
                    // console writes may be captured by starting toolkit in a command window an redirecting the output to a file
                    // e.g., ‘RocksmithToolkitGUI.exe >console.log’ 
                    Console.WriteLine("WwiseCLI:\n\'" + wwiseCLIPath + "\'\n\nTemplate:\n\'" + wwiseTemplateDir + "\'");
                    //System.Threading.Thread.Sleep(50);
                    // apply magicDust to WwiseCLI.exe to force conversions (known Wwise2010 issue)
                    ExternalApps.Wav2Wem(wwiseCLIPath, wwiseTemplateDir, 10);
                    GetWwiseFiles(destinationPath, wwiseTemplateDir);
                }
                catch (Exception ex)
                {
                    //overridden ex, can't get real ex/msg, use log + throw;
                    var tsst = "Erro  overridden ex, can't get real ex/msg, use log + throw"+ ex.Message; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "");
                    try
                    {
                        System.Threading.Thread.Sleep(5000);
                        ExternalApps.Wav2Wem(wwiseCLIPath, wwiseTemplateDir, 10);
                        System.Threading.Thread.Sleep(5000);
                        GetWwiseFiles(destinationPath, wwiseTemplateDir);
                        //throw new Exception("Wwise audio file conversion failed: " + ex.Message + Environment.NewLine);
                    }
                    catch (Exception exx)
                    {
                        System.Threading.Thread.Sleep(15000);
                        GetWwiseFiles(destinationPath, wwiseTemplateDir);
                        tsst = "Erro ..." + exx.Message; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                var tsst = "Erro ..." + ex.Message; UpdateLog(DateTime.Now, tsst, false, ConfigRepository.Instance()["dlcm_TempPath"], "", "");
            }
        }

        public static string GetWwisePath()
        {
            // support for Wwise v20xx series
            // Wwise may not be installed in the default location so use the Configuration Wwise Path if entered
            var wwiseRoot = !Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigRepository.Instance()["dlcm_localwwise"] + "\\" + ConfigRepository.Instance()["dlcm_wwise"])
                ? ConfigRepository.Instance()["general_wwisepath"]
                : (AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigRepository.Instance()["dlcm_localwwise"] + "\\" + ConfigRepository.Instance()["dlcm_wwise"]);////
            // otherwise use the WWISEROOT Environmental Variable
            if (String.IsNullOrEmpty(ConfigRepository.Instance()["general_wwisepath"]))
                wwiseRoot = Environment.GetEnvironmentVariable("WWISEROOT");

            if (String.IsNullOrEmpty(wwiseRoot))
                throw new FileNotFoundException("Could not find Audiokinetic Wwise installation." + Environment.NewLine +
                    "Please confirm that either Wwise v2013.2.x v2014.1.x 2015.1.x or 2016.2.xx or 2017.1.xx or" +
                    " 2018.1.x or 2019.2.x or 2021.1.13(latest w CLI support) or 2022.1.x or 2023.1.x or 2024.1.x or 2025.1.x beta series is installed." + Environment.NewLine);

            var wwiseCLIPath = Directory.EnumerateFiles(wwiseRoot, "WwiseC*.exe", SearchOption.AllDirectories);
            if (!wwiseCLIPath.Any())
            {
                // Check for wwise root if user has bad custom path to wwise
                if (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WWISEROOT")))
                    wwiseCLIPath = Directory.EnumerateFiles(Environment.GetEnvironmentVariable("WWISEROOT"), "WwiseC*.exe", SearchOption.AllDirectories);
            }

            if (!wwiseCLIPath.Any())
                throw new FileNotFoundException("Could not find WwiseCLI.exe/WwiseConsole.exe in " + wwiseRoot + Environment.NewLine +
                    "Please confirm that either Wwise v2013.2.x v2014.1.x 2015.1.x or 2016.2.x or 2017.1.xx or" +
                    " 2018.1.x or 2019.2.xx (latest with no issues on PS3) or 2021.1.13(latest w CLI support) or 2022.1.x or 2023.1.x or 2024.1.x or 2025.1.x beta series is installed." + Environment.NewLine);

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
            else if (wwiseVersion.StartsWith("2019"))
                Selected = OggFile.WwiseVersion.Wwise2019;
            else if (wwiseVersion.StartsWith("2021"))
                Selected = OggFile.WwiseVersion.Wwise2021;
            else if (wwiseVersion.StartsWith("2022.1"))
                Selected = OggFile.WwiseVersion.Wwise2022;
            else if (wwiseVersion.StartsWith("2023"))
                Selected = OggFile.WwiseVersion.Wwise2023;
            else if (wwiseVersion.StartsWith("2024"))
                Selected = OggFile.WwiseVersion.Wwise2024;
            else if (wwiseVersion.StartsWith("2025"))
                Selected = OggFile.WwiseVersion.Wwise2025;
            // add support for new versions here, code is expandable
            //else if (wwiseVersion.StartsWith("xxxx.x"))
            //    Selected = OggFile.WwiseVersion.WwiseXXXX;
            else
                Selected = OggFile.WwiseVersion.None;

            if (Selected == OggFile.WwiseVersion.None)
                throw new FileNotFoundException("You have no compatible version of Audiokinetic Wwise installed." + Environment.NewLine +
                    "Install supportend Wwise version, which are v2013.2.x || v2014.1.x || v2015.1.x || v2016.2.x series || v2017.1.x series" +
                    " || v2018.1.x series || v2019.2.x series (last not generating ps3 weird tape-delay issues) || v2021.1.13(latest w CLI support)" +
                    " || v2022.1.x series || v2023.1.6 series || v2024.1.x series || v2025.1.x beta series " + Environment.NewLine +
                    "if you would like to use toolkit's Wwise autoconvert feature. Did you remember to set the Wwise" + Environment.NewLine +
                    "installation path in the toolkit General Config menu?" + Environment.NewLine);
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

            // for fewer headaches ... start with fresh Wwise 2023 Template
            if (Directory.Exists(templateDir)) IOExtension.DeleteDirectory(templateDir, true);///bcapi && Selected == OggFile.WwiseVersion.Wwise2019
            ExtractTemplate(Path.Combine(ExternalApps.TOOLKIT_ROOT, Selected + ".tar.bz2"));
            if (Directory.Exists(templateDir + Path.GetFileName(Path.GetDirectoryName(sourcePath)) + Path.GetFileName(sourcePath)))
                IOExtension.DeleteDirectory(templateDir + Path.GetFileName(Path.GetDirectoryName(sourcePath)) + Path.GetFileName(sourcePath), true);
            Directory.Move(templateDir, templateDir + Path.GetFileName(Path.GetDirectoryName(sourcePath)) + Path.GetFileName(sourcePath));//some wem files get locked maybe a new template directory would help
            templateDir = templateDir + Path.GetFileName(Path.GetDirectoryName(sourcePath)) + Path.GetFileName(sourcePath);

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
                case OggFile.WwiseVersion.Wwise2021:
                case OggFile.WwiseVersion.Wwise2022:
                case OggFile.WwiseVersion.Wwise2023:
                case OggFile.WwiseVersion.Wwise2024:
                case OggFile.WwiseVersion.Wwise2025:
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
                tw.Close();
                tw.Dispose();
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
                        MessageBox.Show("Error at load template"+ex.Message);//var timestamp = GenericFunctions.UpdateLog(timestamp, "error at copy wav: "+ sourcePreviewWave, true, ConfigRepository.Instance()["dlcm_TempPath"], "", "MainDB", null, null);
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
            var ww2024 = new DirectoryInfo(Path.Combine(wwiseTemplateDir, ".cache"));
            Console.WriteLine("Wwise '.cache': " + wemPath);

            if (!wemPathInfo.Exists)
                throw new FileNotFoundException("Could not find Wwise template .cache Windows SFX directory" + Environment.NewLine);

            var fileExt = ".wem";
            if (Selected == OggFile.WwiseVersion.Wwise2010)//bcapi2019?reverted2023
                fileExt = ".ogg";

            var srcPaths = wemPathInfo.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Where(fi => fi.FullName.ToLower().EndsWith(fileExt)).ToList();
            if (!srcPaths.Any())
                srcPaths = ww2024.EnumerateFiles("*", SearchOption.AllDirectories).Where(fi => fi.FullName.ToLower().EndsWith(fileExt)).ToList();
            if (!srcPaths.Any())
                throw new Exception("<ERROR> Did not find any converted Wwise audio files ..." + Environment.NewLine);

            if (srcPaths.Count < 2) //Selected != OggFile.WwiseVersion.Wwise2019 &&
                ;// throw new Exception("<ERROR> Did not find converted Wwise audio and preview files (i.e. " + srcPaths.Count + "/min2)..." + Environment.NewLine);

            var destPreviewPath = string.Format("{0}_preview.wem", destinationPath.Substring(0, destinationPath.LastIndexOf(".", StringComparison.Ordinal)));
            foreach (var srcPath in srcPaths)
            {
                //fix headers for wwise v2016 wem's //bcapi2019?reverted2023- seems to lock templates
                if ((int)Selected >= (int)OggFile.WwiseVersion.Wwise2016)
                    OggFile.DowngradeWemVersion(srcPath.FullName, srcPath.Name.Contains("_preview_") ? destPreviewPath : destinationPath);
                else
                    File.Copy(srcPath.FullName, srcPath.Name.Contains("_preview_") ? destPreviewPath : destinationPath, true);
            }
        }


    }
}