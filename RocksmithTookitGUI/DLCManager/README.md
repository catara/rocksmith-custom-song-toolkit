## Rocksmith DLC Library Manager v0.7
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
		- Lyrics
		- the stanndard 128kb bitrate
	- Listen to songs Audio/Preview
	- Gathers Track No./Cover/Year from Spotify
- Mass Modify songdetails/metadata @repack per each Rocksmith song
	*e.g. Album Field: "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>"
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Setlists (NEW)
- Read Current Game Library and match it to the DLCManager Library (incl. PS3) (NEW)
- Mass rename songs(Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc)
<img src="/RocksmithTookitGUI/DLCManager/Screenshot3.png" alt="Song Metadata Standardization Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot4.png" alt="Rocksmith Retail Manipulation Scree"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot5.png" alt="Duplicate Management Import Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot6.png" alt="Rocksmith EndScreen Sample"/>


## Known Issues:
-+ Packing of Rocksmith 2014 Retail manipulated files has 1 manual step for RS14 Retail songsC & Mac & PS3)

<img src="/RocksmithTookitGUI/DLCManager/Screenshot1.png" alt="Rocksmith DLC Library Manager Import&Pack"/> //width="400px"/
<img src="/RocksmithTookitGUI/DLCManager/Screenshot2.png" alt="Song Metadata DB Screen"/>

# Official tool / MAster Branch bugs:
[-] CICAGO 26 25 original SONG FAILS AT PACK
[-] open ticket for the dlcpackagedata crash for 311 down in

## ToDos/bugs:

[ ] make sure its read only once when imported and not anymore in the duplication management section
[ ] feat:		 Alternate No for duplicates logic
[ ] feat:		 Include Standardization names into duplication checks
[ ] big feature: get the volume of the audio file and then compare against the rest or a norm
[ ] medium feature: add DLCs into cache.psarc to speed up the game startup
[ ] big feature [ ]For the tagging add the info to the Preview Image/Album Art
[ ] make a custom song out of retail
[ ] if i delete a folder do i get a warning in the maintenance check ?
  [ ] check for json existance as well. Maybe
[ ] increase the no of threads
  [ ] progress bar update
[ ] duplicate window should return last maximum conv date and save it 
-[ ] reorder main db fields
	[ ] size
	[ ] save /offer the chance to reorder
[ ] rename the Options and use the xml code inside the tool (removing the 03 14 35 dependency)
[ ] improve progress bar of repacks
[ ] ask if you wanna have the packing folder deleted
[ ] fix/check changing path of library
[ ] add info box with folders sizes
[ ] if cover was from someone else please compare against that (save old cover)
[ ] Check Packer.cs bug
[ ] muse uprising songs have preview lenght in minutes
[ ] duplicate window reformat not to sent backand forth 1mio variables but on dataset
[ ] consider live when searching for the track NO or dont :)
[ ] Repack only repack Initial or LAst only one platform 
[ ] Repack only copy and ftp,  Initial or LAst only one platform 
[ ] add checks to packed..remember to ask if they wanna clean ups or notification 
[ ] cause it doesnt pick up version it cannot diff shiro astronau
[ ] other platforms do not have official flag correctly detected
[ ] search using ignition
[ ] add convert multitone to single tone
[ ] duplicate lead to Rhythm
[ ] Repair broken CDLC
[ ] add 46&2 lyrics from old one
[ ] overrite rename buttons
[ ] replace all does not exist
[ ] each platform should have its own remote location
[ ] INCUBUS REDEFINE MULTI SHOWS IN RED AT DUPLICATION MANAGEMENT SCREEN
[ ] fix import current month
[ ] gasoline cannot be packed
[ ] check originals vs original dbb
[ ] why do i have duplciates on the ps3
[ ] multiple repacks cannt happen
[ ] progress bar on possible duplicate import is too short
[ ] populate new tone fields
[ ] cannot find sick sick sic (override)
[ ] audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
[ ] usa peaches long naming fails at packing (shortes folder the random id)
  [ ] ps3 long names sogs(149char)(cannot be read)
  [ ] after selecting deselect incl. Beta
[ ] Add repair option
[ ] update readme.md screenshots
[ ] year inconsitencies (check should show these as well)
[ ] check author db saving being used at pack
[ ] why album id is the same as artist id
[ ] if no delete then dont delete DBscocnvert and ftp is it working at mass packing
[ ] mantra cannot be imported
[ ] come together lyrics
[ ] check db also checks arrangements, standardization, tones,
[-] vs blue maybe also yes blue (blu happens also when small 1bite amybe 2 diff in filesize
[-] small improv: add the proper lasconvdate in the db 
[-] fix songs w only bass and dd attached
[-] select incl groups has a small error (userissues)
[-] test replace ....
[-] add copy last and initial to mass pack
[-] prepare the rebuild option
[-] redo save track at pack with update function
[-] improve the overlay
[-] fix for rebuild missing file
[-] all options should be a table
[x] delete lib (gather lib from ps3-ftp/mac/pc 1. save 2. copy new)
	[-] Delete All
	[ ] add simple logic to add sections
	[ ] check and fix the Sections flag
	[+] add sections (bored to death; Renabled BPR program)
	[x] add conter of sections
[-] mac song gen errors at read dlc library
[-] Enable CDLC
[+] remove all dd in Cache/Retails screen
[-] generate a garageband _(curently the mp3 is converted to wav for GB Import)
[-] song fails at import
[ ] at import get youtube rksmith link
[ ] at import get youtube song link
[ ] at import gather
[ ] make processing static
[ ] optimize the variable transfer to generate package
[ ] make all unique check(02-05 look the same) at mass pack
[ ] georlitt_m.psarc
[ ] "Alice-In-Chains_Nutshell-Unplugged_v6_p.psarc"
[ ] orion changed cover
[ ] fixed a vocal at dupli missing 
[ ] improve maybe tone diff message
[ ] add a 4 sec timestamp in each song
[ ] the pretender doesnt register as alternate
[ ] check smooth sailying and c file that breaks
[ ] check crossed album art
[ ] check iron maiden album art
[-] is dupli marked as ignore/ duplicate decompressed folder deleted?
[-] small feature: when opening MainDB and detecting directory check runs, if next directory exists (give a change to empty the db then if possible)
[x] clean up the saving settings logic
    [ ] at open it gets records for 3 times
[ ] finish arrangement (demo with the hives overdrive all the time :) )
[ ] delete does not delete the actual record from repack_audittrail
[ ] check if there are any platforms None
[ ] fix audio add cancel
[ ] 4483
[ ] fix tones Sceeen
[ ] progress bar having it overelayed text overlayered bug error
[ ] add some free space statistics
[ ] save old db when changing to new db
[ ] open folder in the root of the input text box
[ ] audio slave bring back alive error when moving something
[ ] euology 10.3 1.02 is not detected as dupli
[ ] comparison should not take in account info inbetween []
[ ] clean pack_audit trail duplicates
[ ] clean pack audio ...copy path folder instad of full path
[ ] when autom deciding something is not a duplicate dont continue maybe
[ ] MAke Sure CHANGIN D THE AUDIO/PREVIEW DOES NOT deletes the old info useful for Duplciation comparison
where is duplciate reason, add duplciate number
L:\Temp\Arctic-Monkeys_Perhaps-Vampires-Is-A-Bit-Strong-But_v2_p.psarc path too long
add import as diff to dupli manag window
fix official set alternate 4392

## WiP:
(next release)
[x] try db constant connection
[x] unify pack function
[x] add missin tones and arrangement parameters
[x] make mass pack also use db values
[x] log should also update progressbar
[x] think multithreading the decompression before import (now done at each import file Processing procedure)
[x] check why spofity fails after 100 requests
[x] dont let spotify crash after 100 requests and failure notice
[x] ignore and del 4k/no psarc files invalid wil/should be added by nmew validation
[x] alternate 0 improve
	[+] with or without u still doesnt have the right alternate no when importing vs an existing song= a2....maybe OK :)
	[ ] alt 1 ahead of a.
[x] maybe fix audio should not be async (ok)
[x] copy imported files
[x] george babker section standardization insert
[x] preview still generated..why (improvements to reading the profiles when changin db)
[x] first add preview then audio fix
[x] some import files are not read by the valid and hash background workers
[x] 72 should be 37 (no)
[x] take out archive from delete; add note of its use
[x] check if preview &downstream are generated and flag updated see u2 boy twilight where has preview flag is off
[x] archiv, logs, covere is being deleted
[x] tones are not being updated on duplicate update? (just replaced)
[x] delete arrangements and tones at overrite
[x] check log errors regarding file delete 1045 genericfunctions
[x] add info on log why preview was generated
[x] think to move validations outside
[x] vocalas compare xml not json
[x] color platform if diff at duplciatiopn check
[x] change order of fix all audio issues opreview first then bitrate
[x] improve pack logging
[x] dont let pack crash ... and log
[x] wtf happened w packing log...not all imported..
[x] no packe ones
[x] go back to pack get track
[x] check hash checks audio&preview
[x]fix                 //Microsoft.Office.Interop.Access.Application app = new Microsoft.Office.Interop.Access.Application();
[x] no old displayed
[x] take slash double on repacked
[x] no bdd
[x] monalisa no sound
[x] volcas ice ice baby
[x] george barker


## done:
(this release)


- diff between master and branch
DLCManager folder
.xml
mainform.cs
mainfor.designer

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
	0.6 (28.10.2017) Modify Lyrics, get tool ready for multithreading, 
	wip: 0.7.1 (03.06.2017) (90%) Remove getlastconversiondate from Duplicates as already coming from the arrangements table
	wip: 0.7.2 (20.06.2017) (20%) has_section flag(to be tested with a song missing sections); 
	wip: 0.8.1 (31.07.2017) (10%) HTML&Excel exports
	tbr: 0.8.2 move Import DB to Main.DB or at least use an official data source as DB source to also be able to edit from the grid
	tbr: 0.8.3 ?move Access code to project? or from hardcoded to views	
	wip: 0.8.4 (03.10.2017) (10%) use parameterized SQL everywhere (&/ integrate template DB into project or a SQL DB)



		# Date: 12.09.2017
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
9. Activate debug by trying to debug?! &aet GUI folder as startup (right click)
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

		- oggdec.exe - change bitrate of ogg to 128 aw any orig
		http://www.rarewares.org/ogg-oggenc.php

		- edattool.exe -- encript PS3 Retail DLCs packs
		http://www.aldostools.org/ps3tools.html

		- audiocrossreference.exe not actively used; but contained in the package (possible usage in decompressing PS3 WEM or in the future to Match Songs ws ecripted Audio Filenames, currently hardcoded)
		https://sites.google.com/site/cozy1cgi/

		- Beats & Phrases Resynchronizer by Svengraph
		http://customsforge.com/topic/15687-beats-phrases-resynchronizer/

		- Cover Manipulations (not used)
		https://github.com/Lovroman/RS-CDLC-Tagger/
		http://customsforge.com/topic/20334-tool-cdlc-tagger/

		- MDB Viewver - alternative view of mdb container DB
		http://www.alexnolan.net/software/mdb_viewer_plus.htm

		-NVORBIS library - reading ogg lenght
		https://nvorbis.codeplex.com/documentation

		-DevOnly additional software
			EOF v1.8b (c)2008-2010 T³ Software eof1.8RC11(5-19-2016) http://ignition.customsforge.com/eof http://customsforge.com/topic/1529-latest-eof-releases-5-19-2016/page-86 -4 transforming lyrics into RS Vocals
			UltraStar Creator 1.2 https://sourceforge.net/projects/usc/ -4creati ng lyrics files to import in EoF
			TotalCommander https://gisler.com -4Encripting PS3 Retail Sog PSARCS (0 encription level only avail here)
			MediaInfo https://sourceforge.net/projects/mediainfo/ -4checking wem bitrate

## Contact

mailto:bogdan@capi.ro  
http://capi.ro/  
https://github.com/catara/rocksmith-custom-song-toolkit

# Rksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)

