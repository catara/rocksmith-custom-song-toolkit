﻿using System;
using System.Reflection;
using Newtonsoft.Json;
using RocksmithToolkitLib.DLCPackage.AggregateGraph2014;
using RocksmithToolkitLib.Sng;
using RocksmithToolkitLib.XML;
using RocksmithToolkitLib.Extensions;
using RocksmithToolkitLib.XmlRepository;
using System.Xml.Serialization;
using System.ComponentModel;

namespace RocksmithToolkitLib.DLCPackage.Manifest2014.Header
{
    [Serializable]
    public class AttributesHeader2014
    {
        public static readonly string URN_TEMPLATE = "urn:{0}:{1}:{2}";
        public static readonly string URN_TEMPLATE_SHORT = "urn:{0}:{1}";

        internal bool IsVocal = false;
        internal Song2014 SongContent = null;

        public string AlbumArt { get; set; }
        public string AlbumName { get; set; }
        public string AlbumNameSort { get; set; }
        public string ArrangementName { get; set; }
        public string ArtistName { get; set; }
        public string ArtistNameSort { get; set; }
        // apply conditional serialization to BassPick - see comments at bottom of page
        public int BassPick { get; set; } // added to resolve issue #272
        public decimal CapoFret { get; set; }
        public double? CentOffset { get; set; } // tuning frequency, see Cents2Frequency method
        public bool DLC { get; set; }
        // usually DLCKey = SongKey, except that songs.psarc does not contain a DLCKey
        // in compatiblity packs DLCKey is always equal to RS1CompatibilityDisc
        // in ODLC/CDLC DLCKey is always equal to SongKey which is SongName with all spaces removed
        public string DLCKey { get; set; } 
        public double? DNA_Chords { get; set; }
        public double? DNA_Riffs { get; set; }
        public double? DNA_Solo { get; set; }
        public double? EasyMastery { get; set; }
        [DefaultValue(false)] // only serialize a non-false value
        public bool JapaneseVocal { get; set; }
        public int LeaderboardChallengeRating { get; set; }
        public string ManifestUrn { get; set; }
        public int MasterID_RDV { get; set; }
        public int? Metronome { get; set; } // see comments at bottom
        public double? MediumMastery { get; set; }
        public double? NotesEasy { get; set; }
        public double? NotesHard { get; set; }
        public double? NotesMedium { get; set; }
        public int? Representative { get; set; } // Header only
        public int? RouteMask { get; set; } // Header only
        public bool Shipping { get; set; }
        public string SKU { get; set; }
        public double? SongDiffEasy { get; set; }
        public double? SongDiffHard { get; set; }
        public double? SongDiffMed { get; set; }
        public double? SongDifficulty { get; set; }
        // usually SongKey = DLCKey, except that songs.psarc does not contain a DLCKey
        // usually SongKey is the SongName with all spaces removed
        public string SongKey { get; set; }
        public double? SongLength { get; set; }
        public string SongName { get; set; }
        public string SongNameSort { get; set; }
        public int? SongYear { get; set; }
        // strings are not serialized if the value has not been initialized
        public string JapaneseSongName { get; set; } //Unicode string, be cautious
        public string JapaneseArtistName { get; set; } //Unicode string, be cautious
        public TuningStrings Tuning { get; set; }
        public string PersistentID { get; set; }

        public AttributesHeader2014() { }

        public AttributesHeader2014(Attributes2014 attributes)
        {
            foreach (PropertyInfo prop in attributes.GetType().GetProperties())
                if (GetType().GetProperty(prop.Name) != null)
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(attributes, null), null);
        }

        public AttributesHeader2014(string arrangementFileName, Arrangement arrangement, DLCPackageData info, Platform platform)
        {
            if (arrangement.ArrangementType == ArrangementType.ShowLight)
                return;

            IsVocal = arrangement.ArrangementType == ArrangementType.Vocal;
            SongContent = (IsVocal) ? null : Song2014.LoadFromFile(arrangement.SongXml.File);
            var dlcName = info.Name.ToLower();

            var albumUrn = String.Format(URN_TEMPLATE, TagValue.Image.GetDescription(), TagValue.DDS.GetDescription(), String.Format("album_{0}", dlcName));
            var jsonUrn = String.Format(URN_TEMPLATE, TagValue.Database.GetDescription(), TagValue.JsonDB.GetDescription(), String.Format("{0}_{1}", dlcName, arrangementFileName));

            //FILL ATTRIBUTES
            this.AlbumArt = albumUrn;
            JapaneseVocal |= arrangement.Name == Sng.ArrangementName.JVocals;
            ArrangementName = IsVocal ? Sng.ArrangementName.Vocals.ToString() : arrangement.Name.ToString(); //HACK: weird vocals stuff
            DLC = true;
            DLCKey = info.Name;
            LeaderboardChallengeRating = 0;
            ManifestUrn = jsonUrn;
            MasterID_RDV = arrangement.MasterId;
            PersistentID = arrangement.Id.ToString().Replace("-", "").ToUpper();
            Shipping = true;
            SKU = "RS2";
            SongKey = info.Name; // proof same same

            if (IsVocal) return;
            // added better AlbumNameSort feature
            AlbumName = info.SongInfo.Album;
            AlbumNameSort = info.SongInfo.AlbumSort;
            ArtistName = info.SongInfo.Artist;
            CentOffset = (!arrangement.TuningPitch.Equals(0)) ? TuningFrequency.Frequency2Cents(arrangement.TuningPitch) : 0.0;
            ArtistNameSort = info.SongInfo.ArtistSort;
            if (arrangement.ArrangementType == ArrangementType.Bass)
            {
                SerializeBassPick = true;
                BassPick = (int)arrangement.PluckedType;
            }
            else
                SerializeBassPick = false;

            CapoFret = (arrangement.Sng2014.Metadata.CapoFretId == 0xFF) ? CapoFret = 0 : Convert.ToDecimal(arrangement.Sng2014.Metadata.CapoFretId);
            DNA_Chords = arrangement.Sng2014.DNACount[(int)DNAId.Chord];
            DNA_Riffs = arrangement.Sng2014.DNACount[(int)DNAId.Riff];
            DNA_Solo = arrangement.Sng2014.DNACount[(int)DNAId.Solo];
            NotesEasy = arrangement.Sng2014.NoteCount[0];
            NotesMedium = arrangement.Sng2014.NoteCount[1];
            NotesHard = arrangement.Sng2014.NoteCount[2];
            EasyMastery = NotesEasy / NotesHard;
            MediumMastery = NotesMedium / NotesHard;
            Metronome = (int?)arrangement.Metronome;
            // TODO: check for bug here 
            // if there is an equivalent bonus arrangement then Representative is set to "1" otherwise "0"
            Representative = Convert.ToInt32(!arrangement.BonusArr);
            RouteMask = (int)arrangement.RouteMask;

            // TODO: use ManifestFunctions.GetSongDifficulty() method (fix generation algorithm)
            // TODO: improve calculation

            int techCoeff = arrangement.ArrangementPropeties.NonStandardChords +
                            3 * arrangement.ArrangementPropeties.BarreChords +
                            (arrangement.ArrangementPropeties.PowerChords | arrangement.ArrangementPropeties.DoubleStops) +
                            arrangement.ArrangementPropeties.DropDPower +
                            2 * arrangement.ArrangementPropeties.OpenChords +
                            arrangement.ArrangementPropeties.FingerPicking +
                            arrangement.ArrangementPropeties.TwoFingerPicking +
                            arrangement.ArrangementPropeties.PalmMutes +
                            arrangement.ArrangementPropeties.Harmonics +
                            3 * arrangement.ArrangementPropeties.PinchHarmonics +
                            arrangement.ArrangementPropeties.Hopo +
                            arrangement.ArrangementPropeties.Tremolo +
                            (arrangement.ArrangementPropeties.PathBass == 1 ? 4 : 1) * arrangement.ArrangementPropeties.Slides +
                            arrangement.ArrangementPropeties.UnpitchedSlides +
                            3 * arrangement.ArrangementPropeties.Bends +
                            4 * arrangement.ArrangementPropeties.Tapping +
                            2 * arrangement.ArrangementPropeties.Vibrato +
                            arrangement.ArrangementPropeties.FretHandMutes +
                            arrangement.ArrangementPropeties.SlapPop +
                            arrangement.ArrangementPropeties.Sustain +
                            arrangement.ArrangementPropeties.FifthsAndOctaves +
                            arrangement.ArrangementPropeties.Syncopation;

            if (techCoeff <= 4)
                techCoeff += 4;

            SongDiffHard = Math.Round(techCoeff * (double)NotesHard / SongContent.SongLength / 100, 9);
            SongDiffMed = Math.Round(techCoeff * (double)NotesMedium / SongContent.SongLength / 50, 9);
            SongDiffEasy = Math.Round(techCoeff * (double)NotesEasy / SongContent.SongLength / 25, 9);
            SongDifficulty = SongDiffHard;

            SongLength = Math.Round(SongContent.SongLength, 3, MidpointRounding.AwayFromZero);
            SongName = info.SongInfo.SongDisplayName;
            SongNameSort = info.SongInfo.SongDisplayNameSort;
            JapaneseSongName = string.IsNullOrEmpty(info.SongInfo.JapaneseSongName) ? null : info.SongInfo.JapaneseSongName;
            JapaneseArtistName = string.IsNullOrEmpty(info.SongInfo.JapaneseArtistName) ? null : info.SongInfo.JapaneseArtistName;
            SongYear = info.SongInfo.SongYear;

            //Detect tuning
            var tuning = TuningDefinitionRepository.Instance.Detect(SongContent.Tuning, platform.version, arrangement.ArrangementType == ArrangementType.Bass);
            Tuning = tuning.Tuning; //can we just use SongContent.Tuning
        }

        // semi-undocumented feature of Newtonsoft.Json selective conditional serialization
        // see www.geekytidbits.com/conditional-serialization-with-json-net
        // Newtonsoft.Json will look for this property at runtime
        public bool ShouldSerializeBassPick()
        {
            // only serialize if ArrangmentType is Bass
            return (this.SerializeBassPick);
        }

        // this property allows control over conditional serialization
        [XmlIgnore]
        [JsonIgnore]
        public bool SerializeBassPick { get; set; }

        // see stackoverflow.com/a/5818571
        // conditional serializer - prevents serialization when value is null
        public bool ShouldSerializeMetronome()
        {
            return Metronome.HasValue;
        }

        
    }
}
