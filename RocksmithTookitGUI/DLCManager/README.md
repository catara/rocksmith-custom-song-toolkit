﻿# Rksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)



# Rksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)



## App Description: MASS Manipulation of Rocksmith DLC Library 
            e.g. 1. in Rocksmith in the Library, each songs Album, to contain a personal rating, if it has DD, instr. avail
            e.g. 2. in Rocksmith in the Library, each song to be sorted by Album(Year) and Track No
			e.g. 3. Eliminate all the songs you dont like/want to see, from the Play A Song Menu for RS14, RS12 & RS12 DLC



# Roksmith DLC Library Manager v0.2.0.12 (beta version)
Main Features:
- Gather all DLCs metadata into 1 Microsoft Access DB
	- Manage Duplicates
	- Edit Individual metadata fields
	- Fix Songs without
		- Preview
		- Cover
	- Listen to songs Audio
- Mass Modify songdetails/metadata @repack per each Rocksmith song: e.g. Album Field: <Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Mass rename songs(Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc for PC & Mac & PS3)


## Known Issues:
+ Import fails randomly...workaround Import again (don't clear the DB)
+ Tool is based on Rocksmith Toolkit 5th Sept '15 version
-- Edit of Arragements and Tones DB can only be done from Access for the moment
- Packing of Rocksmith 2014 Retail manipulated files has 1 manual step for RS14 Retail songs
-- When having GitHub conficting issue
 https://help.github.com/articles/resolving-a-merge-conflict-from-the-command-line/
 git status
 edit in notepad 
 git add xxx.xxx
 git commit

## ToDos/bugs:
n2h:
-+ with or without u still doesnt have the right alternate no when importing vs an existing song= a2....maybe OK :)
-+ some tracks no don't get read/picked cause" (1) "
-+ small bug: on inserting a new standardization 
-+ small feature: when opening MainDB and detecting directory check runs, if next directory exists (give a change to empty the db then if possible)
-- cannot find sick sick sic (override)
  -? consistency check function
-+ small improv: add the proper lasconvdate in the db 
  - make sure its read only once when imported and not anzmore in the duplication management section
-+ at pack mark as selected - Conflicting with Select functionality (is this useful? :) )
-+ vs blue maybe also yes blue
-+ small improv: Any change of song in the list should also save :)
-+ standardiation delete

wip:
- feat:		 Alternate No for duplicates logic
- feat:		 Include Standardization names into duplication checks
- big feature: get the volume of the audio file and then compare against the rest or a norm
- medium feature: add DLCs into cache.psarc to speed up the game startup
- big feature - For the tagging add the info to the Preview Image/Album Art
- audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
- save error log separately
  - save constantly not only at the end
  - smart dEbug OUTPUTING function...
  -+ save in the db as well
- make a custom song out of retail
- finish arrangement (demo with the hives overdrive sall the time :) )
- remove commented out code 
- duplicate import should save the names of the files in a multivalue(;) field :)
  - save also hash not to ask again for duplciates if already decided as duplicate
- if i delete a foolder do i get a warning in the maintenance check ?
  - check for json existance as well. Maybe
- ignore Multitracks as duplicates
- add date as duplication text
- increase the no of threads
 -- add cancelation button
 -- progress bar update
- duplicate window should return last maximum conv date and save it 
- update to latest(15oct)
- answer to ps3 bug https://github.com/rscustom/rocksmith-custom-song-toolkit/issues/227 https://github.com/rscustom/rocksmith-custom-song-toolkit/issues/231

wd:
- make Groups as multivalue
- reorder main db fields
- rename the Options and use the xml code inside the tool (removing the 03 14 35 dependency)
- improve progress bar of repacks
- enable shortNames and Album correction in Standardization
- ask if you wanna have the packing folder deleted
- dont conv Orig

### dev issues:
+ Toolkit version flag does not sync to&from Github: Run RunMeFirst.bat
+ activate debug: set RocksmithToolkitGUI Folder as Start-up project

# Version History(release date):
	0.1(12.08.2014) prototype, 
	0.1.4 (22.08.2014) populating the Read Folder - File.DB,
	0.1.4.1 (12.09.2014) fixed a solution folder issue when a folder includes another folder breaking the BUILD;
	0.1.5 (12.10.2014) Read + change + Rebuild
	0.1.6 (27.10.2014) Manage Import of Duplicates (upgrade to Access 2013 DB) 
	0.1.7 (30.10.2014) FORK from/on git from 06.12.2014
	0.2 (15.11.2014) Import 1000 dlcs, provide screen to edit, repack in any format
	0.2.0.1 (06.11.2014) manage the the/die at import(remove when creating a folder name), manage the errors at import (move broken files in a broken folder), fix the whitestripes 7armies import issue
	0.2.0.2 (10.11) bugfixes and drafts on screen an future features
	0.2.0.3 (18.11) Implement translation for cleanups (every artist The Black Key = Black Keys) plus add/remove DD.Added field in Main DB to say that record had naming issues.
	0.2.0.4 (20.11) Redesign stats & Duplicates screen as an independendent form not a yes no cancel alert window
	0.2.0.5 (01.12) Search screen prototype
	0.2.0.6 (29.11) add save confirm for any save operation and add DLCID checks in all updates that mighht affect it WHERE (SELECT NO OTHER DLCNAME)
	0.2.0.7 (15.12) New features: Add: lastconversiondata field per each arrangement, MainDb filters
	0.2.0.8 (31.03) Manage RS12, RS12 DLC & RS14 retail songs. Pending platform independent...checks on compression platform dependent...DB independece/dependence on already provided 1..play and FTP and Preview Adding
	0.2.0.9 (30.04) Implement FTP to PS3 (also as a copy to any other location)
	0.2.0.10 (05.05) save settings in Toolkit config, getrck no, groups, Add 30 sec preview midsong; close bugs on Conversion to Ps3(nin hell)
	0.2.0.11 (22.08.2015) remove bug on auto import if original
	0.2.2.12 (26.09.2015) If importing an original over a alternate the alternate flag should be set no the Alternate
	wip: 0.2.0.13 (10.10.2015) (95%) full release (anyone can download and use the tool..no bugs..and all un-implemented features disabled) , repack wo bugs, edit screens functional
	wip: 0.2.0.14 (03.10.2015) (90%) Remove getlastconversiondate from Duplicates as already coming from the arrangements table
	wip: 0.2.0.15 (20.10.2015) (60%)  analyse acuracy on conversion to ps3 (1979,7army..)
	wip: 0.2.0.16 (20.10.2015) (20%) has_section flag(to be tested with a song missing sections); 
	wip: 0.2.1 (31.10.2015) (10%) HTML&Excel exports
	tbr: 0.2.1.1 (15.09.2015) Implement a logic to properly read DLCManager renamed DLCs
	tbr: 0.3.1 move Import DB to Main.DB or at least use an official data source as DB source to also be able to edit from the grid
	tbr: 0.3.2 ?move Access code to project? or from hardcoded to views	
	wip: 0.3.3 (03.10.2015) (10%) use parameterized SQL everywhere (&/ integrate template DB into project or a SQL DB)
	wip: 0.4 (21.10.2015) (85%) Redesign MainDB+Edit Screens



# Date: 10.10.2015
# Document Name: Rocksmith DLC Management tool README
				(fork of rocksmith-custom-song-toolkit)
# Document purpose: To describe the functionailities and the way to change, the NEW tab that enable MASS Manipulation of Rocksmith DLC Library
					(DLC folder; including customs(CDLC), DLCs and songs embeded in the ready to ship version of Rocksmith 2014) 
## Legend:
- to be implemented
+ done
_ future release
-- WIP
{} old unimplemented feature

#### IDE-Setup <old>
1. Download Git Client and Visual Studio 2013 Desktop Edition
2. Update Visual Studio, SQL, download HELP, and FORM-Controls-Object reference
3.0 Create a new Folder DLCManager
3.1 Add a new Item README.txt
3.1 Create a new Tab and a new Menu Item
3.2. Add User Control
3.3. Add New User Control &paste/create yr own assets(buttons..textboxes&logic)
3.4. Rebuild and then Add the tab to the MainFormControl from the Toolbox DLCManager object
3.5. Comment/fix regression Git issues
6. --Add new libraries (include)
7. --Add controls to activate User Controls
8. --Add Interop.Excel REference (Tools-AddReference-Extensions-Microsoft.Office.Interop.Excel)
9. Activate debug by trying to debug?!
10. copy missing dlls from...a prev version !?

# Main Features:
+1. Ability to Generate a Database with all DLC
	Design description:
		+ Read a preset folder and generate a list of DLCs
			+ Save a hash id for each file to record version change/need to decompress 
				{+ Initially the filesize stamp will be used}
		+ Decompress all
		+ Read(parse) the info file and populate a DB
			+ If hash already exiting in DB don't populate Main DB
			+If doesn't Exist check and then not/save it as New/Alternate
			_ Add Audit trail DB for not importing old versions anymore
		_ (future)Build CDLC(single container) of official RS2012 songs
		+ Determine if no ORIGINAL, RS1DLC, DLC, CDLC, Bass, Rhythm, Lead, Vocals, Sections

+2. Ability to change metadata
	+ Name
		+ Add Album, Year, Version, Custom fields(Avail Instruments,Alternate Version, Multitrack)
		+ Add Description (e.g. Bass only, too acoutic)
		+ Keep&Add Comments
		+ Keep&Add Rating (i.e. 1/10)
		+ Keep&Add Album Order No.
		+ Keep&Add Multitrack info
		+ Keep&Add Grouping info (e.g. custom groups to be packaged out)
		+ Keep&Add Beta/WIP tag
			+ These songs appear at the top of the sorted list as they will have 0 in front of the name (0Nirvana - Bleach - School v0.1 beta)
	+ File & Song Name standardization
	Design description:
		+ UI for Automated and Manual operations
		- Any changed file can be easily reverted back to the original setting
		+ Repack the changed files

-3. Ability to watch out for new Downloaded DLC and repackage them with:
	+ New App ID
	+ Convert to Mac & PS3
	+ FTP to PS3


+4. Ability to Repack all DLCs
	-- ?Original files changed should also be packaged?
		-- Ability to package songs by Groups(e.g. party songs, great bass, rating 10) :)


_5. Future features
	-- Read the customforge.net website (Parse costonsforge.com and enrich the DB/Library w playthrough videos, yb preview, update required, release notes....)
		- Reconcile the version no
		- Keep Download links
	-- Help manage and create MultiTracks
		- Based on a original track plus audicity track create a new version
	-- Manage Official DLCs
		- Ability to create a package out of a list of Official DLCs
			-- Initially all DCLS will be manually matched & hardcoded as to the audio file name and the song title
		+ Hide specific Official songs from view/Main screen
	+ Flag files with no bass, no sections, no lyrics, no preview, no DD, riff repeater..
		-- FIX when possible
	+ Show duplicates and solve conflicts
	+ Audio Preview full track&preview track
	+ Remove DD (arragement dependent..e.g. only bass)

## Disclaimer:
This program is maintained by catara, and it's a fork of the Rocksmith Custom Song Project(http://www.rscustom.net/), and is not affiliated with Ubisoft®
and/or the Rocksmith™ team.  This program is for personal or educational use only and
may not be sold or purchased.  Any activities that may negatively effect
the original authors, Ubisosft® or Steam content in anyway are not condoned
or supported.  Rocksmith users should remember to support Ubisoft® and
Steam by purchasing original game content and software releases so that
these companies may continue making products that we enjoy.  

Additioanlly, No modification have been applied to Rocksmith Custom Song Project sourcecode (besides new UI tab).
  - 1fix for PS3 conversion (#273 on github)

This software makes use of Applications not made
by developers who are part of this project.

All claims and liabilities of any misuse of the programs
of this folder should be directed to the respective developer.


- psarc.exe decompress packs and WEM
?aldotools?

- oggdec.exe - play ogg
http://www.rarewares.org/ogg-oggdec.php

- edattool.exe -- encript PS3 Retail DLCs packs
http://www.aldostools.org/ps3tools.html

- audiocrossreference.exe not actively used; but contained in the package (possible usage in decompressing PS3 WEM or in the future to Match Songs ws ecripted Audio Filenames, currently hardcoded)
https://sites.google.com/site/cozy1cgi/

- Beats & Phrases Resynchronizer by Svengraph
http://customsforge.com/topic/15687-beats-phrases-resynchronizer/

- Cover Manipualtions
https://github.com/Lovroman/RS-CDLC-Tagger/
http://customsforge.com/topic/20334-tool-cdlc-tagger/

- MDB Viewver
http://www.alexnolan.net/software/mdb_viewer_plus.htm


## Contact

mailto:bogdan@capi.ro  
http://capi.ro/  
https://github.com/catara/rocksmith-custom-song-toolkit