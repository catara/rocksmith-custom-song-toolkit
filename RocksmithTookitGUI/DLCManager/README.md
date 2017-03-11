## Rocksmith DLC Library Manager v0.5
*(beta version-unreleased to the masses; branch of CSC)*
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
		- Lyrics (NEW)
	- Listen to songs Audio/Preview
	- Gathers Track No. from Spotify
- Mass Modify songdetails/metadata @repack per each Rocksmith song
	*e.g. Album Field: "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>"
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Setlists (NEW)
- Read Current Game Library and match it to the DLCManager Library (incl. PS3) (NEW)
- Mass rename songs(Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc for P
<img src="/RocksmithTookitGUI/DLCManager/Screenshot3.png" alt="Song Metadata Standardization Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot4.png" alt="Rocksmith Retail Manipulation Scree"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot5.png" alt="Duplicate Management Import Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot6.png" alt="Rocksmith EndScreen Sample"/>


## Known Issues:
-+ Packing of Rocksmith 2014 Retail manipulated files has 1 manual step for RS14 Retail songsC & Mac & PS3)

<img src="/RocksmithTookitGUI/DLCManager/Screenshot1.png" alt="Rocksmith DLC Library Manager Import&Pack"/> //width="400px"/
<img src="/RocksmithTookitGUI/DLCManager/Screenshot2.png" alt="Song Metadata DB Screen"/>

# Official tool / MAster Branch bugs:
[ ] CICAGO 26 25 original SONG FAILS AT PACK
[ ] open ticket for the dlcpackagedata crash for 311 down in
[ ] song fails at import

## ToDos/bugs:
-+ small feature: when opening MainDB and detecting directory check runs, if next directory exists (give a change to empty the db then if possible)
-+ small improv: add the proper lasconvdate in the db 
  [ ]make sure its read only once when imported and not anzmore in the duplication management section
-+ vs blue maybe also yes blue (blu happens also when small 1bite amybe 2 diff in filesize
[ ] feat:		 Alternate No for duplicates logic
[ ] feat:		 Include Standardization names into duplication checks
[ ] big feature: get the volume of the audio file and then compare against the rest or a norm
[ ] medium feature: add DLCs into cache.psarc to speed up the game startup
[ ] big feature [ ]For the tagging add the info to the Preview Image/Album Art
[ ] make a custom song out of retail
[ ] if i delete a folder do i get a warning in the maintenance check ?
  [ ] check for json existance as well. Maybe
[ ] increase the no of threads
  -[ ] progress bar update
[ ] duplicate window should return last maximum conv date and save it 
-[ ] reorder main db fields
	[ ] size
	[ ] save /offer the chance to reorder
[ ] rename the Options and use the xml code inside the tool (removing the 03 14 35 dependency)
[ ] improve progress bar of repacks
[ ] ask if you wanna have the packing folder deleted
[ ] fix/check changing path of library
[ ] add info box with folders sizes


## WiP:
[-] generate a garageband _(curently the mp3 is converted to wav for GB Import)
[ ] if cover was from someone else please compare against that (save old cover)
[+] remove all dd in Cache/Retails screen
[ ] Check Packer.cs bug
[ ] Bring year and picture from Spotify
[ ] muse uprising songs have preview lenght in minutes
[ ] duplicate window reformat not to sent backand forth 1mio variables but on dataset
[ ] consider live when searching for the track NO or dont :)
[ ] Repack only repack Initial or LAst only one platform 
[ ] Repack only copy and ftp,  Initial or LAst only one platform 
[-] all options should be a table
[ ] finish arrangement (demo with the hives overdrive all the time :) )
[ ] add checks to packed..remember to ask if they wanna clean ups or notification 
[ ] add ps3 as a duplicate
[ ] cause it doesnt pick up version it cannot diff shiro astronau
[ ] other platforms do not have official flag correctly detected
[ ] search using ignition
[ ] add convert multitone to single tone
[ ] add bonus into in lyrics

[ ] duplicate lead to Rhythm
[ ] update readme.md screenshots
[ ] try db constant connection
[ ] do i delete things three time, one more redundantly
[ ] standardise genhash to the already exiusting maindb.GetHashCode
[ ] copy imported files


copied copie coipied
overrite rename buttons
mac fails at readout
Profiles should include also Format
replace all does not exist
come together lyrics
each platform should have its own remote location

[x] clean maindb and others of Comments
[x] when marking at dupli gne a pack_auditrail
[x] duplicate should go into packed audit trail :) 
[x] astronaut does not pick the file version
[x] remove commented out code
[x] save error log separately
[x] save constantly not only at the end
[x] smart dEbug OUTPUTING function...
[x] Selecting a witha  new name should ask if you wanna rename
[x] new filter...show loaded songs
[x] initial
[x] at copy save in remote location
[ ] add time stamps in logging to improve performance
done:
(next release)
[x] add cancelation button
[x] duplicate management..on change..check and green color
[+] save in the db as well
[x] finish profiles (load Saved Profile names)
	[x] check they are saved at close
[x] save when exit search 
	[x] when clicking on a song disable Search
[x] delete all duplicates at Standardization
	[+] small bug: on inserting a new standardization
[+] add group to Beta
[x] standardiation delete does not refresh
[x] duplicate import should save the names of the files in a separate table :) (not req)
	[x] save also hash not to ask again for duplicates if already decided as duplicate (all imported are saved and so checked for hexa code)
[x] enable Cover correction in Standardization
[x] enable shortNames and Album correction in Standardization
[x] fix filter in CAche
[x] group display with issues
[+] some songs dont repack
[+] dont conv Orig
[+] some songs have preview in minutes (preview rename thing) recalculated at repack
	[+] why all songs have 30sec preview :)
[x] copy name fails title sort in duplicate management
[x] add read library option
[x] wrap up the audit trail...only at succesfull import u update the table
	[+] at deletion please remove from imported audit trail
[x] duplicate also shows in winmerge Tones
[x] fix stop import from duplicate screen
[x] cacke give errors
[x]  fix some dupli buttons not being enabled
[x] add unique key for each packed&imported thing so to read libs
	[+] use also name as identifier
[x] profiles should be a table
[x] duplic add numeric text box
[x] tarck no Issues (all rhcp r 9)(fixed)
[x] bass removed twice creates issues at packing(code cleanup)
[x] dupli buttons dont deactivate after copy
[x] backing is not recognized
[x] vs pixel to r8 on cover
[x] custom song creator still there after save
[x] capture live flag? as track no fails
[x] add live in Duplicate
[x] swich from missing files to remote path
[x] date bigger
[x] crashes if i go on new rec in the maindblist
[x] if multi info only in file name is not detected
[x] add bonus in the title
[x] a3 fails
[x] vocals is not acti dupli wannabeLA
[x] update DB
[x] at save dupli is off
[x] qa still doesnt says novales (was not in the xml)
[x] TEst Live Removal
[x] duplicate should also compare multitrack version and live_details
[+] at pack mark as selected - Conflicting with Select functionality (is this useful? :) )
[x] Create a new filter Deselect LAst PAcked. no deen
[x] Profile Temp is now debug
[x] Selecting no profile now is not gen error
[x] fix for import unique hash for any
[x] Fix for Copy Selection Old
[-] delete lib (gather lib from ps3-ftp/mac/pc 1. save 2. copy new)
	[ ] Delete All
[ ] cannot find sick sick sic (override)
[ ] audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
[ ] usa peaches long naming fails at packing (shortes folder the random id)
  [ ] ps3 long names sogs(149char)(cannot be read)
  [ ] after selecting deselect incl. Beta
[ ] update to wise 2015 (messages&testing)
[ ] add simple logic to add sections
	[ ] check and fix the Sections flag
	[+] add sections (bored to death; Renabled BPR program)
	[ ] add conter of sections
[x] ignore and del 4k/no psarc files invalid wil/should be added by nmew validation
[ ] progress bar on possible duplicate import is too short
[ ] populate new tone fields
[-] prepare the rebuild option
[-] is dupli marked as ignore/ duplicate decompressed folder deleted?
[-] redo save track at pack with update function
[-] improve the overlay
[-] fix for rebuild missing file
[-] disable old and L->r
[x] alternate 0 improve
	[+] with or without u still doesnt have the right alternate no when importing vs an existing song= a2....maybe OK :)
	[ ]alt 1 ahead of a.
[x] fix importing own songs, eg. ana
[ ] standardiation double counted songs 7981
[ ] stadardization still fedup adding duplicates
[x] check help that crashed at infi.nhame (broken now) partial file now we do some checks on extension
[ ] add copy last and initial to mass copy
[+] third eye investig
[x] count the trully ftp
[x] added vocals hash is missing
[x] for newly added lyrics create sng and save the path in the DB
[x] no cover Not Listening papa roach
[x] remove should have a progress bar
 


(prev release; but maybe not thuroughly tested)
[x] duplic manag make date bigger
[x] duplic manag make date readonly
[x] when opening no java...
[x] remove author if custom
[x] bring in no of duplicates in title
[x] if multitrack dont bring in the duplicate
[x] fix duplicate song for all the connected files at DB level. Copied OLD file as well.
[x] Update Selectd stats after Save
[?] Bass Picked changes when moving up and down in MainDB
[x] Custom song creators only saved if Advanced option set
[x] Fix for unnecessary preview creation for songs wo Albumart
[x] Fix no album art detection
[x] Remove BassDD only if DD exits
[x] Add trully FPT to mass pack
[x] Update to latest Master (template broken revert will gen DLCName change back from dlckey)
[x] Spotify old API is dead, use a new way to... (bring also cover)
[x] save track at repack
[x] replace () w [] (ADD that to alt.;)
[x] when in filter select none looks weird
[x] it's my life..breaks because of the aphostrofe' (update log issue)(update duplication)
[x] Filter by group
[x] How many audioslave bring themback alive foler r? duplicate song issue? (old duplicqwrtes not deleted maybe)
[x] Minerva gives a wwise error check on a repaired hdd...check and open a tix (no issue lately)
[x] add incl group
[x] at search when chicking it changes from exit to search
[x] multitrack dont get their track no if they have (no guitar removed) as assumgly they stilla re searched witrh full name 
[x] alternate doesnt go into title sort
[x] trim SONG details
[x] original bass remove fails on billy weathers  (fix)
[x] beta does not apear on originals or indiv repack(mqiandb)(non issue-added save at repack)
[x] some tracks no don't get read/picked cause" (1) " when importing other formats this should be ok
[x] fixed remove dd on some originals
[+] started to add details tones&arrangements from db at pack
[x] Add Lyrics simple steps
[x] Start thinking of importing other platforms(duplicate window platform indicator)
[x] fix multiplatofrm import as folder created is always pc_ iggy pop lust for life
[x] add exbox and mac to missing paths AudioP = songshsanP.Replace("\\manifests\\songs_rs1disc\\songs_rs1disc.hsan", "\\audio\\" + (platformDLCP == "PS3" ? platformDLCP : (platformDLCP == "Pc" ? "windows" : platformDLCP)) + "\\") + AudioP1 + (platformDLCP == "PS3" ? ".ogg" : "_fixed.ogg");
[x] add external software registry paths
[x] fixed reorder issue in Standardiyation


- diff between master and branch
DLCManager folder
.xml
mainform.cs
mainfor.designer
gui.csprj
dlcpackagedata
                var newPreviewFileName = Path.Combine(Path.GetDirectoryName(audioPath), String.Format("{0}_preview{1}", Path.GetFileNameWithoutExtension(audioPath), Path.GetExtension(audioPath)));
                if (!File.Exists(newPreviewFileName)) File.Move(audioPreviewPath, newPreviewFileName); //bcapi as some original create an error here
                //old File.Move(audioPreviewPath, newPreviewFileName);
                data.OggPreviewPath = newPreviewFileName;

### Dev Tips:
+ Toolkit version flag does not sync to&from Github: Run RunMeFirst.bat
+ activate debug: set RocksmithToolkitGUI Folder as Start-up project
+ When having GitHub conficting issue
 https://help.github.com/articles/resolving-a-merge-conflict-from-the-command-line/
 git status
 edit in notepad 
 git add xxx.xxx
 git commit

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
	0.2.0.12 (26.09.2015) If importing an original over a alternate the alternate flag should be set no the Alternate
	0.2.0.13 (12.11.2015) Finally implement Arrangements and Tones and song Groups	
	0.2.0.14 (20.06.2016) analyse accuracy on conversion to ps3 (1979,7army..)
	0.3 (10.05.2016) full release (anyone can download and use the tool..no bugs..and all un-implemented features disabled), repack wo bugs, edit screens functional
	0.4 (21.11.2016) Major improvements	and features
	0.5 (15.03.2017) Implement a logic to properly read DLCManager manipulated DLCs
	wip: 0.5.0.1 (03.06.2017) (90%) Remove getlastconversiondate from Duplicates as already coming from the arrangements table
	wip: 0.5.0.2 (20.06.2017) (20%) has_section flag(to be tested with a song missing sections); 
	wip: 0.5.1 (31.07.2017) (10%) HTML&Excel exports
	tbr: 0.5.2 move Import DB to Main.DB or at least use an official data source as DB source to also be able to edit from the grid
	tbr: 0.5.3 ?move Access code to project? or from hardcoded to views	
	wip: 0.5.4 (03.10.2017) (10%) use parameterized SQL everywhere (&/ integrate template DB into project or a SQL DB)



		# Date: 12.03.2017
		# Document Name: Rocksmith DLC Management tool README
						(fork of rocksmith-custom-song-toolkit)
		# Document purpose: To describe the functionailities and the way to change, the NEW tab that enable MASS Manipulation of Rocksmith DLC Library
							(DLC folder; including customs(CDLC), DLCs and songs embeded in the ready to ship version of Rocksmith 2014) 
		# Legend:
		- to be implemented
		+ done
		_ future release
		-- WIP
		{} old unimplemented feature

# IDE-Setup <old>
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

# Implementation Tracking for the Main Features:
		+1. Ability to Generate a Database with all DLC
			Design description:
				+ Read a preset folder and generate a list of DLCs
					+ Save a hash id for each file to record version change/need to decompress 
						{+ Initially the filesize stamp will be used}
				+ Decompress all
				+ Read(parse) the info file and populate a DB
					+ If hash already exiting in DB don't populate Main DB
					+ If doesn't Exist check and then not/save it as New/Alternate
					+ Add Audit trail DB for not importing old versions anymore
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
				+ Any changed file can be easily reverted back to the original setting
				+ Repack the changed files

		-3. Ability to watch out for new Downloaded DLC and repackage them with:
			+ New App ID
			+ Convert to Mac & PS3
			+ FTP to PS3


		+4. Ability to Repack all DLCs
			-- ?Original files changed should also be packaged?
				+ Ability to package songs by Groups(e.g. party songs, great bass, rating 10) :)


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
*This program is maintained by catara, and it's a fork of the Rocksmith Custom Song Project(http://www.rscustom.net/), and is not affiliated with Ubisoft®
*and/or the Rocksmith™ team.  This program is for personal or educational use only and*
*may not be sold or purchased.  Any activities that may negatively effect*
*the original authors, Ubisosft® or Steam content in anyway are not condoned*
*or supported.  Rocksmith users should remember to support Ubisoft® and*
*Steam by purchasing original game content and software releases so that*
*these companies may continue making products that we enjoy.*

Additioanlly, No modification have been applied to Rocksmith Custom Song Project sourcecode (besides new UI tab).
  Except:
  -  1 fix for 311 Down In dlcpackagedata crash

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

		- MDB Viewver - alternative view of mdb container DB
		http://www.alexnolan.net/software/mdb_viewer_plus.htm

		-NVORBIS library - reading ogg lenght
		https://nvorbis.codeplex.com/documentation

		-DevOnly additional software
			EOF v1.8b (c)2008-2010 T³ Software eof1.8RC11(5-19-2016) http://ignition.customsforge.com/eof http://customsforge.com/topic/1529-latest-eof-releases-5-19-2016/page-86
			UltraStar Creator 1.2 https://sourceforge.net/projects/usc/

## Contact

mailto:bogdan@capi.ro  
http://capi.ro/  
https://github.com/catara/rocksmith-custom-song-toolkit

# Rksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)

