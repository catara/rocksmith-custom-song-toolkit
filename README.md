		# Date: 11.01.2021
		# Document Name: Rocksmith DLC Management tool README
						(fork of rocksmith-custom-song-toolkit)
		# Document purpose: To describe the functionailities and the way to change, the NEW tab that enable MASS Manipulation of Rocksmith DLC Library
							(DLC folder; including customs(CDLC), DLCs and songs embeded in the ready to ship version of Rocksmith (2014 Remastered version)) 

## Rocksmith DLC Library Manager v1 b1 (compiled beta available in \RocksmithToolkitGUI\bin\RK1)
*(forever alpha version- unnoficially, but available; version released for my own sake and not the masses's)*
# App Description: MASS Manipulation of Rocksmith DLC Library (initial realised but not current problem definition)
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
		- the standard sufficient Audio qualiity (>128kb bitrate)
	- Listen to songs Audio/Preview
	- Gathers Track No./Cover/Year from Spotify & original video and playthrough from Youtube
- Mass Modify songdetails/metadata @repack per each Rocksmith song
	*e.g. Album Field: "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>"
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Setlists/Groups
- Read Current Game Library and match it to the DLCManager Library (incl. PS3)
- Mass rename songs(Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc)

<img src="/RocksmithTookitGUI/DLCManager/Screenshot1.png" alt="Rocksmith DLC Library Manager Import&Pack"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot2.png" alt="Song Metadata DB Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot3.png" alt="Song Metadata Standardization Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot4.png" alt="Rocksmith Retail Manipulation Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot5.png" alt="Duplicate Management Import Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot6.png" alt="Rocksmith EndScreen Sample"/>


## Known Issues:
-+ Packing of Rocksmith 2014 Retail manipulated files has 1 manual step for RS14 Retail songs for PS3
- atm since compiled in 64b Access DB viewer not available
- no yb parsing of links due to "no quota" Corona ggl lockdown
- no spofity track&cover retrieval due to api change and not yet catchup w latest 3rdparty implementation 

# Official tool / Master Branch bugs:
[ ] CICAGO 26 25 original SONG FAILS AT PACK
[ ] open ticket for the dlcpackagedata crash for 311 down in
[-] official DDC remover does not work correctly breaking the songs  (removal is not endorsed by community/remastered version)
[-] usage of old old wwise 2013 vs /2019 (no known benefits for upgrading)

## ToDos/bugs:
[ ] feat:		 Alternate No for duplicates logic
[-] feat:		 Include Standardization names into duplication checks
[ ] big feature: get the volume of the audio file and then compare against the rest or a norm
[ ] medium feature: add DLCs into cache.psarc to speed up the game startup
[ ] big feature [ ]For the tagging add the info to the Preview Image/Album Art
[ ] make a custom song out of retail
[ ] progress bar update
	[ ] improve progress bar of repacks
-[ ] reorder main db fields
	[ ] size
	[ ] save /offer the chance to reorder
[ ] rename the Options and use the xml code inside the tool (removing the 03 14 35 dependency)
[ ] add info box with folders sizes
[ ] if cover was from someone else please compare against that (save old cover)
[ ] duplicate window reformat not to sent backand forth 1mio variables but on dataset
[ ] consider live when searching for the track NO or dont :)
[ ] Repack only repack Initial or LAst only one platform 
[ ] Repack only copy and ftp,  Initial or LAst only one platform 
[ ] check originals vs original dbb
[ ] search using ignition
[ ] add convert multitone to single tone
[ ] duplicate lead to Rhythm
[ ] each platform should have its own remote location
[ ] Add repair option
[ ] why album id is the same as artist id
[ ] if no delete then dont delete DBscocnvert and ftp is it working at mass packing
[ ] mantra cannot be imported
[-] test replace ....
[-] add copy last and initial to mass pack
[-] prepare the rebuild option
[x] delete lib (gather lib from ps3-ftp/mac/pc 1. save 2. copy new)
	[-] Delete All
	[ ] add simple logic to add sections
	[ ] check and fix the Sections flag
[-] mac song gen errors at read dlc library
[+] remove all dd in Cache/Retails screen
[-] generate a garageband _(curently the mp3 is converted to wav for GB Import)
[ ] make processing static
[ ] add a 4 sec timestamp in each song
[x] clean up the saving settings logic
    [ ] at open it gets records for 3 times
[ ] progress bar having it overelayed text overlayered bug error
[ ] add some free space statistics
[ ] clean pack_audit trail duplicates
[ ] change profile saves old setting on new profile
[ ] if 89 selected the show right no in stats
[ ] kids orig was not replaced
[ ] korn for you arrangement insert break
[ ] pack as a group (incl. ps3)
[ ] ps3 decompression fail cause json manifest missing
[ ] standardization suggest similar album names based on capitalisation
[ ] think of adding that ref point for all tracks to be synced
[ ] version is not being saved?
[ ] what does audio mean in name "( audio)"
[ ] check rape me no bass
[ ] MAMAS AND PAPASE LEAD and back vocal split
[ ] can we have a dupli assement window :) (all 5 dupli what happened if automated)
[ ] MAke Sure CHANGIN D THE AUDIO/PREVIEW DOES NOT deletes the old info useful for Duplciation comparison
[ ] the rover preview issue , file gets a weird name
[ ] consider multitasking DDC style
[ ] report change in the house of flies remove ddd issue
[ ] use memory disk for audio operations, access DB, what else?
[ ] take me now baby weird timing in lyric
[ ] "C:\\t\\0\\0_repacked\\PS3\\CDLC-ACDC-1986-Who_Made_Who-00-D_T_"
[ ] too many oggs check and workaround celannup
[ ] add list of copies
[ ] fix dont show lyrics 
[ ] add tooltip that older newr is incomplete
[ ] dupli author duplis spaces
[ ] new tag at pack add older or year
[ ] clean songs w text(duplic..) after _p
[ ] if old is found bbut no entr copy to import and archive
[ ] if old is not found clean record raise alarm
[-] check broken
[ ] CFSM.AudioTools
[ ] PSARC packer refactored, 1:1 to offical compression ratio, but code n…
[ ] 1700 songs wo vocals
[-] find workaround for remote folders
[-] saving remote not working
[ ] fix on leave folders
[ ] duplicate add old values as tooltip
[ ] add export to excel (maybe not as u could simply export from access :) )
[ ] multiple repacks still occur
	[ ] single pack sometimes runs multiple times
[ ] add manualy modified same lyrics change flag (maybe based on hash)
[ ] preview starts twice bug
[ ] at import gather info and display
[ ] Feeling Good_158, Rape Me_3573, Pixies_1987_Come on Pilgrim_0_Nimrod's Son_3793
	[ ] "C:\\t\\f\\0\\0_data\\Pc_CDLC_Elton John_1983_Too Low for Zero_0_I'm Still Standing_16\\gfxassets\\album_art\\album_reaejimstillstanding_256.dds"
	[] var attribute = new Attributes2014(arrangementFileName, arr, info, platform);
	[] C:\GitHub\rocksmith-custom-song-toolkit\RocksmithToolkitLib\DLCPackage\DLCPackageCreator.cs
	[] 543
[ ] add text to audio fixing on progress ar
[ ] mass dont copy single do
[ ] update screenshots
[ ] is combo lead track in lyrics?
[ ] reubild s0ngds.psarc
[ ] db change doesnt not db change when leaving cell
[ ] to finish spotify status and yb status at geenraste?
[ ] at exxport diff tunnings should be displayed if avail
[ ] reduced update selected? in mai db as nnot to run more than once (or five times :) )
[ ] fix pack id?
[ ] antthenum pneuma issues wwitth hash but still it should have been up to assement
[ ] make all folders remote 0_temp=c("0_temp")
[ ] fix import moves to archive not old
[ ] add read only
[ ] https://explore.amd.com/e/659533/m-medium-email-utm-term-btn-tp/9mcnp/237446456?h=sGd69neOaxNCEXHNo4reoAoV2gv6Rm-q2kv1Cph14bk
[ ] copy latest meta from db to xml
[ ] same files not recog as dupli in the import file
[] fix fix not saving current record
[] fix (manual) search yb buton
[] check 10 audio not downsized
[] check search normal search song yb link
[] groups cdlc id not incresed correctly as string
[] main db duplicate needs to saves the other cdlc id in dupi field
[ ] fixed delete on maindb duplciate checking
[ ] use external executable nice windo from main
[ ] fixed saving or yb main/arrangement not to have https:\\yb.comhttps:\\yb.com
[] check arangements and thones as maybe not all have
	[] when checking files also check arangements and tones
	[] 3 dupli assesment does not save left side of 1/3 recs
[] add copy to ftp on the export website (incl han store)
[] artist origin (spotify based, in standardization table?)[] add artist origin in standardization table (presetup for spotify enhancement-retrieval)
[] add inworks 00
[] if (chbx_InclGroups.Checked)
[] export html add download artist and add download album
[] improve check for file issues final window reporting
[] save sections count additionally
[ ] Add option not to import CDLC packed 'by Catara/DLCMANAGER
[ ] open folder in the root of the input text box
[ ] audio slave bring back alive error when moving something
[ ] euology 10.3 1.02 is not detected as duplix
[ ] clean pack audio ...copy path folder instad of full path
[ ] when autom deciding something is not a duplicate dont continue maybe
[ ] INCUBUS REDEFINE MULTI SHOWS IN RED AT DUPLICATION MANAGEMENT SCREEN
[ ] cannot find sick sick sic (override)
[ ] audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
[ ] add checks to packed..remember to ask if they wanna clean ups or notification
[ ] ask if you wanna have the packing folder deleted
[ ] cause it doesnt pick up version it cannot diff shiro astronau
[ ] other platforms do not have official flag correctly detected
[ ] Repair broken CDLC (lost track of this)
[ ] overrite rename buttons
[ ] replace all does not exist
  [ ] ps3 long names sogs(149char)(cannot be read)
  [ ] fix official set alternate 4392
[ ] when copying maybe consider updating any existing packed &copy link
[ ] Viva la Vida or Death and All His Friends did not get into standardization with all its different spellings
[ ] make all unique check(02-05 look the same) at mass pack
[ ] check db also checks arrangements, standardization, tones,
[ ] orion changed cover
[ ] fixed a vocal at dupli missing 
[ ] When setting a (base) tone copy json(&manifest?) too
	[] recreate sng
[ ] make latest x.2.x wwise to work
[ ] java  can work if offline? (done & wwise)
[ ] add release noteas at pack ...if normAL INPUT BOX..IF repack..default
[ ] improving allow dlc in win (copy dll if missing)
[ ] shift track in Arrangment or in main db should use the same code :)
[ ] check how many imported dont have an audit trail
[ ] check if hash doesnt match file hash
[ ] perfect day merge lyrics&bass  
[ ] fixing add standard in duplcaite was not removing older or adding doub along new id
calculate all drop down options using pitch shifting systems and display it in maindb infobox

## WiP:
(this release)




## done:
(prev release)
[-] in any group filter
[-] in no group filter
[-] 10427 why no preview 
[-] search by id had enter search function missing and does not laod fully the song and fucks it up (2nd part not anymore)
[-] fixed packing summary
[-] dont ftp delete if remote path not saved
[-] actually delete ftp at mass repack (no id transmited :))
[-] rename the debug button to give a clue why there :)
[-] changed the Error at FTP file not filde to WArning as really not a codding issue and not to trigger debug in update log function
[-] split version by number name and type :) (fixed Temp group to have same no of things as other, multiplied default to Bogdan as anyway onyl default will go live)
[-] add Pitch Shifter/Digitec Drop Filter options and meta info or lyrics instructions
[-] added check on overitting preview/albumart/lyric
[-] improved meta tags adding to diff songs meta :)
[-] removed defaulted [] meta seoparators vs adding it from the setup filed itself
[-] added sorted by pack date
[-] better reset filters
[-] improved some of the calc of maindb and slected
[-] saved historical data (connected also to main by id in import_audittrail)
[-] displaying historical data
[-] adding pitch shifting compatible flag from e standard down or from dropd down
[-] adding multiselect right click inclusion
[-] add a check after packing that you can psarc works
[-] fixed owerrite duplciatr manag button text
[-] if selecting a pack no but not having it selected dont refresh nos
[-] 37756 repack has weird date
[-] duplicate songs have "import" pack id in pack_auditrail
[-] using multiselect for pack, beta
[-] updates c3custom creators to https://rhythmgamingworld.com/forums/topic/c3-con-tools-v401-8142020-weve-only-just-begun/
[-] updates Add lyrics instructions



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
	0.6 (28.10.2017) Modify Lyrics, get tool ready for multithreading
	0.7 (01.07.2018) Code review
	0.7.2 (26.03.2019) Code review (+'19 Toolkit code base)
	0.7.3 (6.05.2019) Find YB videos and export as webpage imrovements
	0.7.4 (6.05.2019) 
	0.7.5 (20.02.2020) Small improvements 2020
	0.7.6 (20.02.2020) Fixes after reintegration of base changes	
	0.7.7 (20.04.2020) Fixes & improvements around internal duplication and filters (tons tons tons of fixes and polishing of existing "features", all params are in the Groups table)
	1.0 b1 (11.01.2021) Adding Pitch Shift instructions based on E standard or DropD
	1.0 (xx.04.2021) Released on Customforge and GitHub Release "tab" (2 versions one w all 3rd party software, one without; can be installed/decompressed and quickly used; 1 60sec video describing why you should use this)
	1.1 (xx.06.2021) Reactivating Spotify checks

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
			-+ Get Rocksmith playthough or original videos from youtube
			-+ Get Trackno, Year, Cover for standarization from spotify (build your own playlists)
			- Add songs to PS4/5 (jailbroken/?) existing songs.psarc
			- PoC iPAD CDLC

### Diff between master and branch
DLCManager folder
.xml
mainform.cs
mainfor.designer
wwise twds 2019 upgrades and remplate
some failsafes for 

### Dev Tips:
+ Use MS2019 community or whateva other older version
+ Toolkit version flag does not sync to&from Github: Run RunMeFirst.bat
+ activate debug: set RocksmithToolkitGUI Folder as Start-up project
+ Complile RocksmithToolkitGUI as AnyCPU (no 32bit)
± if dpi is problematic add registry entry http://www.visualstudioextensibility.com/2017/01/13/running-visual-studio-or-setups-with-dpi-virtualization-dpi-unaware-on-high-dpi-displays/
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

# Legend:
- to be implemented
+ done
_ future release
-- WIP
{} old unimplemented feature

# IDE-Setup <old>
1. Download Git Client and Visual Studio 2019 Community Edition
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

		- MDB Viewver 2.63 - alternative view of mdb container DB
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
			EOF v1.8b (c)2008-2010 T³ Software eof1.8RC11(xx-09-2020) http://ignition.customsforge.com/eof http://customsforge.com/topic/1529-latest-eof-releases-5-19-2016/page-86 -4 transforming lyrics into RS Vocals
			UltraStar Creator 1.2 https://sourceforge.net/projects/usc/ -4creati ng lyrics files to import in EoF
			TotalCommander https://gisler.com -4Encripting PS3 Retail Sog PSARCS (0 encription level only avail here)
			MediaInfo https://sourceforge.net/projects/mediainfo/ -4checking wem bitrate
			WinMerge http://winmerge.org/?lang=en -used in comparing duplicates (and their respecitve differential track)
			C3 CON Tools 4.0.1 https://rhythmgamingworld.com/forums/topic/c3-con-tools-v401-8142020-weve-only-just-begun/ -used to decompress songs made for Rockband to quickly copy their vocal track to Rocksmith

## Contact

mailto:bogdan@capi.ro  
http://capi.ro/  
https://github.com/catara/rocksmith-custom-song-toolkit

# Rksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)

