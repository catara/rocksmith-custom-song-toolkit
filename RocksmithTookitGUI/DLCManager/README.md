## Rocksmith DLC Library Manager v0.7.1
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
[ ] feat:		 Alternate No for duplicates logic
[ ] feat:		 Include Standardization names into duplication checks
[ ] big feature: get the volume of the audio file and then compare against the rest or a norm
[ ] medium feature: add DLCs into cache.psarc to speed up the game startup
[ ] big feature [ ]For the tagging add the info to the Preview Image/Album Art
[ ] make a custom song out of retail
  [ ] check for json existance as well. Maybe
[ ] increase the no of threads
  [ ] progress bar update
[ ] improve progress bar of repacks
[ ] duplicate window should return last maximum conv date and save it 
-[ ] reorder main db fields
	[ ] size
	[ ] save /offer the chance to reorder
[ ] rename the Options and use the xml code inside the tool (removing the 03 14 35 dependency)
[ ] ask if you wanna have the packing folder deleted
[x] fix/check changing path of library
[ ] add info box with folders sizes
[ ] if cover was from someone else please compare against that (save old cover)
[ ] duplicate window reformat not to sent backand forth 1mio variables but on dataset
[ ] consider live when searching for the track NO or dont :)
[ ] Repack only repack Initial or LAst only one platform 
[ ] Repack only copy and ftp,  Initial or LAst only one platform 
[ ] add checks to packed..remember to ask if they wanna clean ups or notification 
[ ] cause it doesnt pick up version it cannot diff shiro astronau
[ ] other platforms do not have official flag correctly detected
[ ] check originals vs original dbb
[ ] search using ignition
[ ] add convert multitone to single tone
[ ] duplicate lead to Rhythm
[ ] Repair broken CDLC
[ ] overrite rename buttons
[ ] replace all does not exist
[ ] each platform should have its own remote location
[ ] INCUBUS REDEFINE MULTI SHOWS IN RED AT DUPLICATION MANAGEMENT SCREEN
[ ] cannot find sick sick sic (override)
[ ] audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
[ ] usa peaches long naming fails at packing (shortes folder the random id)
  [ ] ps3 long names sogs(149char)(cannot be read)
[ ] Add repair option
[ ] year inconsitencies (check should show these as well)
[ ] why album id is the same as artist id
[ ] if no delete then dont delete DBscocnvert and ftp is it working at mass packing
[ ] mantra cannot be imported
[ ] check db also checks arrangements, standardization, tones,
[-] vs blue maybe also yes blue (blu happens also when small 1bite amybe 2 diff in filesize
[-] small improv: add the proper lasconvdate in the db 
[-] fix songs w only bass and dd attached
[-] select incl groups has a small error (userissues)
[-] test replace ....
[-] add copy last and initial to mass pack
[-] prepare the rebuild option
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
[+] remove all dd in Cache/Retails screen
[-] generate a garageband _(curently the mp3 is converted to wav for GB Import)
[ ] at import gather
[ ] make processing static
[ ] make all unique check(02-05 look the same) at mass pack
[ ] orion changed cover
[ ] fixed a vocal at dupli missing 
[ ] improve maybe tone diff message
[ ] add a 4 sec timestamp in each song
[-] is dupli marked as ignore/ duplicate decompressed folder deleted?
[-] small feature: when opening MainDB and detecting directory check runs, if next directory exists (give a change to empty the db then if possible)
[x] clean up the saving settings logic
    [ ] at open it gets records for 3 times
[ ] progress bar having it overelayed text overlayered bug error
[ ] add some free space statistics
[ ] open folder in the root of the input text box
[ ] audio slave bring back alive error when moving something
[ ] euology 10.3 1.02 is not detected as duplix
[ ] clean pack audio ...copy path folder instad of full path
[ ] when autom deciding something is not a duplicate dont continue maybe
[ ] L:\Temp\Arctic-Monkeys_Perhaps-Vampires-Is-A-Bit-Strong-But_v2_p.psarc path too long
[ ] fix official set alternate 4392
[ ] when copying maybe consider updating any existing packed &copy link
[ ] restore old dd/xmls button in main db
[ ] duplicate dlcname?
[ ] add year to standardization (...maybe not so important)
[ ] clean pack_audit trail duplicates
[ ] Add option not to import CDLC packed by Catara/DLCMANAGER
[ ] Viva la Vida or Death and All His Friends did not get into standardization with all its different spellings
[ ] add 46&2 lyrics from old one
[ ] add starting times as tooltip on instruments
[ ] change profile saves old setting on new profile
[ ] fix color instr that doesnt change
[ ] if 89 selected the show right no in stats
[ ] kids orig was not replaced
[ ] korn for you arrangement insert break
[ ] mass dont copy single do
[ ] pack as a group (incl. ps3)
[ ] ps3 decompression fail cause json manifest missing
[ ] replace not always work..
[ ] saving remote not working
[ ] standardization suggest similar album names based on capitalisation
[ ] think of adding that ref point for all tracks to be synced
[ ] version is not being saved?
[ ] what does audio mean in name "( audio)"
[ ] add options to install missing tools (winmerge; pkg linker, reasigner, etc.)
[ ] update screenshots
[x] update wwise to latest 2017.2.xx/2018
[ ] check rape me no bass
[ ] MAMAS AND PAPASE LEAD and back vocal split
[ ] is combo lead track in lyrics?
[ ] why eu us is not changing
[ ] why sometimes ps3 ftp is not transmited
[ ] can we have a dupli assement window :) (all 5 dupli what happened if automated)
[ ] single pack sometimes runs multiple times
[ ] MAke Sure CHANGIN D THE AUDIO/PREVIEW DOES NOT deletes the old info useful for Duplciation comparison
[ ] the rover preview issue , file gets a weird name
[ ] consider multitasking DDC style
report change in the house of flies remove ddd issue
add dynamic filter for all tunnings in Arrangements:)
set a proper window default$reactivate tooltip
not &compound ruin the selection after first execution
use memory disk for audio operations, access DB, what else?
[] active accdb compress at CleanDB or adhoc by button (not import)
take me now baby weird timing in lyric
add multi select
"C:\\t\\0\\0_repacked\\PS3\\CDLC-ACDC-1986-Who_Made_Who-00-D_T_"
too many oggs
add list of copies
fix dont show lyrics 
add tooltip that older newr is incomplete
dupli author duplis spaces
delete rk folders as split4pack and minus button
new tag at pack add older or year
clean songs w text(duplic..) after _p
if old is found bbut no entr copy to import and archive
if old is not found clean record rsaise alarm
check broken
CFSM.AudioTools
PSARC packer refactored, 1:1 to offical compression ratio, but code n…
1700 songs wo vocals
find workaround for remote folders
fix on leave folders

## WiP:
(this release)
[-] added dupli button in maindb 
[x] fix import current month
[-] File Name should be standardised.. 
[x] add warning if lyrics added not to forget to select add new arangement
[-] add auto groups
[-] add maybe chery love to favs
[-] at import get youtube rksmith link (unsolved bug: async stays in waiting)
[-] at import get youtube song link (unsolved bug: async stays in waiting)
[-] compare/dupli manag 2-x selected songs
[-] add java 64bit if windows is for 64b https://www.java.com/en/download/manual.jsp
[-] 1st feeling inc. not coverting
[-] add better initial setup options & messages
[-] replaced way to call DDC to latest standard
[-] mamas and papas bonus (Added Part Field)
[-] mMa nd pPAS VOCALS (1st lyris ismissin)
[-] mMa nd pPAS VOCALS (1st lyric has the same start at second ()
[-] instead of old and new maybe add timestamp
[-] add song lenght in the duplicate management window
[-] tomahawk is not imported w the right version (same hash; any update/overrite of a song updates also original_filehash)
[-] search again should work if returning nada
[x] dlc name should only be unique after all dupli have been assesed
[x] cassius breaks (added stauff to lyrics from 0.o01 and dynamically size them)
[x] fix duplicate songs to incl new fields
[x] fix add vocals with new path
[x] if gearlist is NOT there disable use of db arg&tones values
[-] add dificulty field in arrangements
[-] check the in sort titles (re-enable it as lost in the single pack to multipack functions)
[-] clean lyrics doesnt clear some end empty lines
[-] renablin 1 and 2 option
[-] fascination stret bass is not properly removed (check if it really has DD as if the bonus/2nd track has the flag is set)
[-] love cats remove bass removal has issues (check if it really has DD as if the bonus/2nd track has the flag is set)
[x] change from pc to ps3 loses the ftp address (save only if PS3 is last format)
[x] fix for delete song incl also gearlist
[-] duplicate remov should clean title also for alt 1 alt 2
[-] added use internal logic flag per song
[x] fix for packing with arrang&tones details (amp and cab settings)
[x] fix for using internal logic not being selected
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Godsmack-2018-When_Legends_Rise-00-Someday_ps3.psarc.edat'.DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ 
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Godsmack-2018-When_Legends_Rise-00-Say_My_Name_ps3.psarc.edat'.DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ 
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Interpol-2014-El_Pintor-00-My_Desire_ps3.psarc.edat'.
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Jimmy_Eat_World-2001-Jimmy_Eat_World-00-The_Middle_ps3.psarc.edat'.DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ 
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Sting-1993-Ten_Summoner's_Tales-00-Heavy_Cloud_No_Rain_[BRLV]_ps3.psarc.edat'.DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ 
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\CDLC-Metallica-2008-Death_Magnetic-00-The_Day_That_Never_Comes_ps3.psarc.edat'.DB Open in Design Mode, or Missing, or you need to Download the 32bit Connectivity library @ 
Could not find file 'C:\GitHub\new\0\0_repacked\PS3\ORIG-Ghost-2015-Meliora-00-He_Is_ps3.psarc.edat
[x] add tag timeing in file&song name
[x] show start time with each track
[x] show bonus/part/
[x] fix add sort artist 
1. finalise duplicates
2. replace spotify
3. finalise import library ()
[x] add soundrtrack
[-] add contextual menu
[x] clean wrongly timed lyrics
"Sweet Child O' Mine [BRL newer]"
[x] Are You In
[-] why there are tones with zero cdcl_id
[x] remove time from dupli
[x] no section (less or equyl w one)
[x] fix enter to search
[x] add minutes to somewhere
[-] unplugged is live
[-] fix/improve save profile
[-] auto play as separate process
[x] autoplay is gray
[x] old is not gray on no searches
[x] Added AlbumSort
[] yb links are not consitently retrieved
[x] ADD TAG AUTHOR TO other ogg compressing scripts
[-] bring year and album sort into duplicates
	what else 
[x] colour song lenght & ALBUM YEAR
[x] add log entrie in the right log file 
[x] 4912 generic windw
	[x] FixOggwDiffName
	[x] improve copy safely for non hash simil files to still not be overitten (lots of Metallica_Fade-to-Black_v1_p.psarc same name)
[x] error ok? not working
	[x] whispers
	[x] -Are_You_Gonna_Go_My_Way-00
	[x] eric clapton
	[x] judah priest is ps3?paclking
	[x] Mamas_&_The_Papas,The
	[x] moving x2
[-] Black Rebel Motorcycle Club-Spread Your Love
[x] sync album_sort if sync album
[x] year diff, albbum sort diff
[x] log spotify error 

## done:
(prev release)
[x] add filter/show show the All Others (reverse current filter)
[x] add misfits to favs
[x] another one bytest the dust freeses at bullets rip (bass issue?sustain note)(issue:bass bend 0.5 sec not only 1 sec)
[x] come together crashing (bend info is not transported in the DD free internal version)
[x] come together lyrics
[x] double /time remove from vocals
[x] enable multi instance packing using DLCManager
[x] fix additional stuff to lyrics sometimes breaking the song (make a limit for 50 char)
[x] fix audio at the end includes broken songs
[x] fix monthly imports filter
[x] fix one filter (all originals not in Setilist)
[x] fixe single packing wont to break 
[x] fixed add lyrics to include new fields
[x] fixed an isssue at duplciate management where some dupli would be cosidered same quicker than expected
[x] imported this month
[x] increasedy of Song_Comments till the 1st lyric
[x] is there a Pc_CDLC_The Offspring_1998_Americana_0_The Kid's Aren't Alright_2109 (name typo fixed)
[x] lengthen the start of 1,2,3 lines of intro vocal text
[x] no zero added if 87 and 88 are on
[x] ps3 special beginning of lyrics lyrics dont always(ps3, 2nd line not indipendendt) display
[x] removed artist folder (fixed)
[x] shorten start of track to seconds
[x] talk issue w double bass
[x] why single threading happens 4 time at pack from maindb (fixed using a global variab)
[x] MAYBE  WHAT IF i wanna structure my files based on group anyway line 3858 (MAYBEno need for 0&Group at the beginning) 

# Version History(release date):
	0.1(12.08.2014) prototype, 
	0.1.4 (22.08.2014) populating the Read Folder - File.DB,
	0.1.4.1 (12.09.2014) fixed a solution folder issue when a folder includes another folder breaking the BUILD;
	0.1.5 (12.10.2014) Read + change + Rebuild
	0.1.6 (27.10.2014) Manage Import of Duplicates (upgrade to Access 2013 DB) 
	0.1.7 (30.10.2014) FORK from/on git from 06.12.2014
	0.2 (15.11.2014) Import 1000 dlcs, provide screen to edit, repack in any format
	0.2.0.1 (06.11.2014) manage the the/die at import(remove when creating a folder name), manage the errors at import (move broken files in a broken folder), fix the whitestripes 7armies import issue
	0.2.0.2 (10.11.2014) bugfixes and drafts on screen an future features
	0.2.0.3 (18.11.2014) Implement translation for cleanups (every artist The Black Key = Black Keys) plus add/remove DD.Added field in Main DB to say that record had naming issues.
	0.2.0.4 (20.11.2014) Redesign stats & Duplicates screen as an independendent form not a yes no cancel alert window
	0.2.0.5 (01.12.2014) Search screen prototype
	0.2.0.6 (29.11.2014) add save confirm for any save operation and add DLCID checks in all updates that mighht affect it WHERE (SELECT NO OTHER DLCNAME)
	0.2.0.7 (15.12.2014) New features: Add: lastconversiondata field per each arrangement, MainDb filters
	0.2.0.8 (31.03.2015) Manage RS12, RS12 DLC & RS14 retail songs. Pending platform independent...checks on compression platform dependent...DB independece/dependence on already provided 1..play and FTP and Preview Adding
	0.2.0.9 (30.04.2015) Implement FTP to PS3 (also as a copy to any other location)
	0.2.0.10 (05.05.2015) save settings in Toolkit config, getrck no, groups, Add 30 sec preview midsong; close bugs on Conversion to Ps3(nin hell)
	0.2.0.11 (22.08.2015) remove bug on auto import if original
	0.2.0.12 (26.09.2015) If importing an original over a alternate the alternate flag should be set no the Alternate
	0.2.0.13 (12.11.2015) Finally implement Arrangements and Tones and song Groups	
	0.2.0.14 (20.06.2016) analyse accuracy on conversion to ps3 (1979,7army..)
	0.3 (10.05.2016) full release (anyone can download and use the tool..no bugs..and all un-implemented features disabled), repack wo bugs, edit screens functional
	0.4 (21.11.2016) Major improvements	and features
	0.5 (15.03.2017) Implement a logic to properly read DLCManager manipulated DLCs	
	0.6 (28.10.2017) Modify Lyrics, get tool ready for multithreading, 
	0.7 (28.01.2018) Code review
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

 ### old goals
	wip: 0.7.1 (03.06.2017) (90%) Remove getlastconversiondate from Duplicates as already coming from the arrangements table
	wip: 0.7.2 (20.06.2017) (20%) has_section flag(to be tested with a song missing sections); 
	wip: 0.8.1 (31.07.2017) (10%) HTML&Excel exports
	tbr: 0.8.2 move Import DB to Main.DB or at least use an official data source as DB source to also be able to edit from the grid
	tbr: 0.8.3 ?move Access code to project? or from hardcoded to views	
	wip: 0.8.4 (03.10.2017) (10%) use parameterized SQL everywhere (&/ integrate template DB into project or a SQL DB)



		# Date: 12.01.2018
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
		http://www.aldostools.org/ps3tools.html

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

		- PS3xploit resigner - resigning pkg for PS3 HAN enabled CDLCs
		https://github.com/PS3Xploit/PS3xploit-resigner

		- TrueAncestor PKG Creator - packing PS3 HAN enabled CDLCs
		http://www.psx-place.com/threads/trueancestor-pkg-repacker-v2-45-by-jjkkyu.10067/#post-48238

		- PKG Linker - WEBServer for PS3 HAN enabled delivered packages
		http://www.psx-place.com/threads/pkg-linker-2-0-serve-packages-to-your-ps3-han-cfw.17252/page-20#post-125162

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

