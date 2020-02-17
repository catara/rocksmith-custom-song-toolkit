		# Date: 20.09.2019
		# Document Name: Rocksmith DLC Management tool README
						(fork of rocksmith-custom-song-toolkit)
		# Document purpose: To describe the functionailities and the way to change, the NEW tab that enable MASS Manipulation of Rocksmith DLC Library
							(DLC folder; including customs(CDLC), DLCs and songs embeded in the ready to ship version of Rocksmith (2014 Remastered version)) 

## Rocksmith DLC Library Manager v0.7.5
*(alpha version- unnoficially, but available, version unreleased to the masses)*
# App Description: MASS Manipulation of Rocksmith DLC Library 
		e.g. 1. in Rocksmith, in the Library, each song's Album, to contain a personal rating, if it has DD, instr. avail
		e.g. 2. in Rocksmith, in the Library, each song to be sorted by Album(Year) and Track No
		e.g. 3. Eliminate all the songs you dont like/want to see, from the Play A Song Menu for RS14, RS12 & RS12 DLC

#Main Features:
- Gather all DLCs metadata into 1 Microsoft Access DB
	- Manage Duplicates @Import and After (NEW keep trace of them and recall them if needed)
	- Edit Individual metadata fields
	- Fix Songs without
		- Preview
		- Cover
		- Lyrics
		- the standard sufficient Audio qualkity (>128kb bitrate)
	- Listen to songs Audio/Preview
	- Gathers Track No./Cover/Year from Spotify & Youtube
- Mass Modify songdetails/metadata @repack per each Rocksmith song
	*e.g. Album Field: "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>"
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Setlists/Groups (NEW)
- Read Current Game Library and match it to the DLCManager Library (incl. PS3) (NEW)
- Mass rename songs(Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc)

<img src="/RocksmithTookitGUI/DLCManager/Screenshot1.png" alt="Rocksmith DLC Library Manager Import&Pack"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot2.png" alt="Song Metadata DB Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot3.png" alt="Song Metadata Standardization Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot4.png" alt="Rocksmith Retail Manipulation Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot5.png" alt="Duplicate Management Import Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot6.png" alt="Rocksmith EndScreen Sample"/>
[![Build status](https://ci.appveyor.com/api/projects/status/olxik31du2rhd8dn/branch/master?svg=true)](https://ci.appveyor.com/project/rscustom/rocksmith-custom-song-toolkit/branch/master)
