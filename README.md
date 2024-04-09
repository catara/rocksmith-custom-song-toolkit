		# Date: 09.03.2024
		# Document Name: Rocksmith 2014 RM DLC Management tool README
						(fork of rocksmith-custom-song-toolkit)
		# Document purpose: To describe the project and capture a developmental history

## Rocksmith 2014 RM DLC Library Manager v1 b6 r3 (compiled beta available in https://github.com/catara/rocksmith-custom-song-toolkit/tree/Main/RocksmithTookitGUI/bin/Debug_Lite.7z )
*(forever to be unreleased version- for my own sake)*
# App Description: MASS Manipulation of Rocksmith 2014 RM DLC Library

#Main Features:
- Gather all Songs metadata into 1 Microsoft Access/SQLite3 DB
	- Manage Duplicates @Import and After (NEW keep trace of them and recall them if needed)
	- Edit Individual metadata fields
	- Fix Songs without
		- Preview
		- Cover
		- Lyrics
		- the standard sufficient Audio qualiity (>128kb bitrate)
	- Listen to songs Audio / Preview
	- Gathers Track No./Cover/Year from Spotify & original video and playthrough from Youtube (NEW: not working due to multiple spotify api updates and youtube qouta policies :))
- Mass Modify SongDetails / Metadata @repack per each Rocksmith song
	*e.g. Album Field: "<Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>"
	- Copies songs/packs directly to the PS3 by means of FTP
- Mass add/remove DD @repack (inc. Bass only option)(incl. Official DLCs)
- Setlists/Groups (incl. grouping by multitrack, acoustic, tuning, soundtrack  NEW: midi,game soundtrack etc.)
- Read Current Game Library and match it to the DLCManager Library (incl. PS3)
- Mass rename songs (Standardization) e.g. Black Keys->The Black Keys and maintain changes in a local DB
- Manipulates the Retail songs list of Rocksmith (Rocksmith 2014 disc, or Rocksmith 2012 DLC, or Rocksmith 2012 Import disc)
	NEW ability to insert CDLC in (PC atm; PS3&PS4 later) Retail version core files
- NEW: when building new DLC save some links to source files/todo/release notes etc
- Export HTML (web server ready) setlist
- Shift Notes (NEW: Manipulate Arrangements to be distributed along a specific timeline from a GuitarPro file that is not time-synced)

<img src="/RocksmithTookitGUI/DLCManager/Screenshot1.png" alt="Rocksmith DLC Library Manager Import&Pack"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot2.png" alt="Song Metadata DB Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot3.png" alt="Song Metadata Standardization Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot4.png" alt="Rocksmith Retail Manipulation Screen"/>
<img src="/RocksmithTookitGUI/DLCManager/Screenshot5.png" alt="Duplicate Management Import Screen"/>


## Known Issues:
- PS3 Packing of Rocksmith 2014 Retail manipulated files has 1 manual step
- when using ACCDB 64b 3rdparty Access DB viewer not available unless 32b plugin is installed(.msi) using /passive
- no yb parsing of links due to "no quota" Corona ggl lockdown
- no spofity track&cover retrieval due to api change and not yet catchup w latest 3rdparty implementation
- inserting songs directly in game files is still beta (PC shuold work, ps3/ps4/iOS pending)
- SQLITE3 is beta (not tested the import as there might be 1-2 more functions not ported to be compatible with both ACCDB & DB formats)
## ACCDB to SQL-Lite3 migration steps:
	- add missing columns with ALTER TABLE Main ADD Is_Deluxe; ALTER TABLE Main DROP Trial983; VACUUM;
	- clean manually 	DELETE FROM Arrangements;	DELETE FROM Cache;	DELETE FROM Groups;	DELETE FROM Import;	DELETE FROM Import_AuditTrail;	DELETE FROM LogImporting;	DELETE FROM LogImportingError;	DELETE FROM LogPacking;	DELETE FROM LogPackingError;	DELETE FROM Main;	DELETE FROM Pack_AuditTrail;	DELETE FROM Tones;	DELETE FROM Tones_GearList;	DELETE FROM OfficialSongs;	DELETE FROM Standardization;	DELETE FROM WEM2OGGCorrespondence;	DELETE FROM ODLC;	VACUUM;
	- Windows 64 bits: migrate ACCDB to SQLLite with LinkDB.accdb VBS script (activate immediate window and increase size): clean_DBlog()

# Official tool / Master Branch bugs:
[ ] CICAGO 26 25 original SONG FAILS AT PACK
[ ] to open ticket for the dlcpackagedata crash for 311 down in
[-] official DDC remover does not work correctly breaking the songs  (removal is not endorsed by community4remastered version)
[-] usage of old old wwise 2013 vs /2019 (no known benefits for upgrading)

## ToDos list/bugs/features/dump-catchall-list:
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
[ ] MAke Sure CHANGIN D THE AUDIO/PREVIEW DOES NOT deletes the old info useful for duplication comparison
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
[ ] fixed delete on maindb duplicate checking
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
multi lyrics files (i wanna be your dog)
check improv sections
add column for order txt and 
remove folders after file verification
v0.9a not detected welcome home sanitarium
show all same sanitisedtitle not alternate
add better sections improve lyrtics song für liam
add guitar and lyrics to drums
r8 clcik on current selected changes also values
save descrption in xml per each option
right click:  make 1st alternate, dynamic dd to grops (try, play, etc)
add replace txt in title,album,artist

cehck long wems
	try other 2013 wen conv
pack also whos options for packing
import also shows you options ot import
decapitalise all album/artist sort
standardization add multi select
standardizationm whenj delete move back to order
standarization mark clear duplicates
[x]COMPATIBILITY doenst have any audio ..ti investigate maybve reference it from Main.db or check the value oif this file where files are newer better etc.

[] improve button to open EoF
[] if u press dont download it will still install it
[] consider ApplyArtistAutoGroup multi insert into single insert logic
	at 1st start restore db
[] rerun capo check
[] fix file repo issues (pack audit trail and groups were wipped lately)
[] protejemois error
compact db doesnt work...add it manually
qa in some tags
not next filter MAKE as dropdow (add not old filter)
fix manually adede vocvals
make broken broken xmls

why is xbox loaded first ..?
duplicatre spotify on new stwqndardiztion
dont duplicate if spotify is standardization as it will create duplicate
add search in standarization
add lenght into dsame dupli management logic
	add text to same similar
make cache window resizeble dinamically witht he objects inside
moce sogs psarc to dlcpack not temp
select only 1 platform for add songs into game type (show warnign if more are selected)

use year album artwork from cache

pixies
rip
hana
go/blue boy
white woman
add:
	movie theme
	Series
	Original Series
	original sountrack 
	The Video Game
	Video Game
	movie
version-remix?

fix E r&rol quee lyrics
elongate paw patrol song
NO LYRICS ON PAW APREIL
no lyrics on sponge bob
beautiful day 8fret notes r mute
FIX NAME 0F MOENY PARTY
	add custom tags
	add lyrics to all burnam
	make hippo song longe
fix search for retail:
	in bloom
	heart shaped
impl add attribute in ui as a dropdown too

self packed files import weirdly
at pack bogdan’s dlc read info from comments
check last copy why failing
add script to copy from sqlite to accdb too :)

fix current issue:
stop all packing after metad
.one meta
if grp 7 sel dont show 6 in artist sort name
113 (add first grp),91 (add grp to filename)
Error ...Main---SELECT ID, Artist, Song_Title, Track_No, Album, Album_Year, Author, Version, Import_Date, Is_Original, Selected, Tunning, Bass_Picking, Is_Beta, Platform, Has_DD, Bass_Has_DD, Is_Alternate, Is_Multitrack, Is_Live, Is_Acoustic, Is_Instrumental, Is_EP, Is_Soundtrack, Is_Single, Is_Broken, Is_FullAlbum, Is_Remastered,  Is_Uncensored, Is_Cover, Is_Demo, Is_Remix, Is_Karaoke, Has_Featuring, Is_Medley, Is_MultiStrings, Is_Deluxe, Is_GreatestHits, Is_Midi, Is_GameSoundtrack, IntheWorks, MultiTrack_Version, Alternate_Version_No, Live_Details, Groups, Rating, Description, PreviewTime, PreviewLenght, Song_Lenght, Comments, FilesMissingIssues, DLC_AppID, DLC_Name, Artist_Sort, Song_Title_Sort, Album_Sort, AverageTempo, Volume, Preview_Volume, SignatureType, ToolkitVersion, AlbumArtPath, Album_ArtPathOrig, AudioPath, audioPreviewPath, OggPath, oggPreviewPath, AlbumArt_Hash, Audio_Hash, audioPreview_Hash, File_Size, Current_FileName, Original_FileName, Import_Path, Folder_Name, File_Hash, Original_File_Hash, Has_Bass, Has_Guitar, Has_Lead, Has_Rhythm, Has_Combo, Has_Vocals, Has_Bonus_Arrangement, Has_Sections, Has_Cover, Has_Preview, Has_Custom_Tone, Has_Version, Has_Author, Has_Track_No, File_Creation_Date, Has_Been_Corrected, Has_Had_Lyrics_Changed, Has_Had_Audio_Changed, Has_Capo, Has_ShowLights, Has_Jvocals, Pack, Available_Old, Available_Duplicate, Duplicate_Of, Tones, Keep_BassDD, Keep_DD, Keep_Original, Original, YouTube_Link, Youtube_Playthrough, CustomForge_Followers, CustomForge_Version, CustomsForge_Link, CustomsForge_Like, CustomsForge_ReleaseNotes, UniqueDLCName, Artist_ShortName, Album_ShortName, DLC, Is_OLD, Remote_Path, audioBitrate, audioSampleRate, Top10, Has_Other_Officials, Spotify_Song_ID, Spotify_Artist_ID, Spotify_Album_ID, Spotify_Album_URL, Audio_OrigHash, Audio_OrigPreviewHash, AlbumArt_OrigHash, Split4Pack, UseInternalDDRemovalLogic, LyricsLanguage, ImprovedWithDM, PitchShiftableEsOrDd, Import_AuditTrail_ID, BasedOn_Youtube, BasedOn_CF, BasedOn_Tabs, BasedOn_GP, ToDos, ToneDetails, PackageDetails, PackingDate, UpdateVersionDate FROM Main u WHERE _Title Like "%In Bloom%" AND Song_Title Like "%In Bloom%" ORDER BY Artist, Album_Year, Album, Track_No, Song_TitleSystem.Data.OleDb.OleDbException (0x80040E14): Syntax error in query expression '_Title Like "%In Bloom%" AND Song_Title Like "%In Bloom%"'.
   at System.Data.OleDb.OleDbCommand.ExecuteCommandTextErrorHandling(OleDbHResult hr)
   at System.Data.OleDb.OleDbCommand.ExecuteCommandTextForSingleResult(tagDBPARAMS dbParams, Object& executeResult)
   at System.Data.OleDb.OleDbCommand.ExecuteCommandText(Object& executeResult)
   at System.Data.OleDb.OleDbCommand.ExecuteReaderInternal(CommandBehavior behavior, String method)
   at System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataSet dataSet, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataSet dataSet, String srcTable)
   at RocksmithToolkitGUI.DLCManager.UtilitiesFunctions.SelectFromDB(String ftable, String fcmds, String currentDB, OleDbConnection cn, SQLiteConnection cnc) in C:\GitHub\rocksmith-custom-song-toolkit\RocksmithTookitGUI\DLCManager\UtilitiesFunctions.cs:line 1935



load retails still using cnb only
selected in?
vocals concurency? also why not in summary log :)
is grapewine in? why blank in import auditrail
clean gearlist dlcm 3000
script create slim version
standardise debug (profile or compiled as debug)

[] VACUUM;
[] delete sqlite
	DELETE  FROM Arrangements;
	DELETE  FROM Cache;
	DELETE  FROM (SELECT  FROM Groups WHERE ID in (SELECT ID FROM Groups WHERE Type='DLC'));
	DELETE  FROM (SELECT  FROM Groups WHERE ID in (SELECT ID FROM Groups WHERE Profile_Name<>'Default' AND Type='Profile'));
	DELETE  FROM Import;
	DELETE  FROM Import_AuditTrail;
	DELETE  FROM LogImporting;
	DELETE  FROM LogImportingError;
	DELETE  FROM LogPacking;
	DELETE  FROM LogPackingError;
	DELETE  FROM Main;
	DELETE  FROM Pack_AuditTrail;
	DELETE  FROM Tones;
	DELETE  FROM Tones_GearList;
	VACUUM;
add timesteamp in log summary of translation
add timestamps at import
populate old data to all dlcs
fix import abort

check tones and gearlists are complet per song

fix duplicate not working in sqlite
add further check after checking for installed software :)
why sqlite startup maindb so slow

fix standardize cover
increase song part to add extra lyrics
impl 121
packing


	fix why no wems C:\t\0\0_data\Pc_CDLC_Danko Jones_2017_Wild Cat_0_You Are My Woman_7549\audio\windows\
	why at pack we get a new Pack (is that only for import :))?
	add check if deleating all from import :)
synbols load at compile ...pointing to dot net code
	-try again dsiable load and hope horeloading still works

	artist/album should also search in live

	add update date
change row in standard window is mega slow
additional custom attributes in standardization fail to be displayed
cover issues in  standardization
vs label on instr and tunning issues at duplcaition
if playlist then add track no as the order from the playlist
packing grp should be by platform too
filter by date (aded, packed)


	empty table:
		.schema Tones_GearList
		drpo table Tones_GearList
		create Tones_GearList
		CONSTRAINT "pk_Pack_AuditTrail" PRIMARY KEY("ID"));
	full table:
		CREATE TABLE IF NOT EXISTS "Groupsu" ("ID" INTEGER NOT NULL,
		"CDLC_ID" VARCHAR(255),
		"Groupz" TEXT,
		"Type" VARCHAR(255),
		"Comments" VARCHAR(255),
		"Profile_Name" VARCHAR(255),
		"Description" VARCHAR(255),
		"DisplayName" VARCHAR(255),
		"DisplayGroup" VARCHAR(255),
		"DisplayPosition" VARCHAR(255),
		"Date_Added" VARCHAR(30));

		INSERT INTO Groupsu (ID, CDLC_ID,Groupz, Type, Comments,Profile_Name,Description,DisplayName,DisplayGroup,DisplayPosition,Date_Added)
		  SELECT ID, CDLC_ID,Groupz, Type, Comments,Profile_Name,Description,DisplayName,DisplayGroup,DisplayPosition,Date_Added
		  FROM Groups;

		drop table "Groups";

		CREATE TABLE IF NOT EXISTS "Groups" ("ID" INTEGER,
		"CDLC_ID" VARCHAR(255),
		"Groupz" TEXT,
		"Type" VARCHAR(255),
		"Comments" VARCHAR(255),
		"Profile_Name" VARCHAR(255),
		"Description" VARCHAR(255),
		"DisplayName" VARCHAR(255),
		"DisplayGroup" VARCHAR(255),
		"DisplayPosition" VARCHAR(255),
		"Date_Added" VARCHAR(30),
		CONSTRAINT "pk_Pack_AuditTrail" PRIMARY KEY("ID"));

		INSERT INTO Groups (ID, CDLC_ID,Groupz, Type, Comments,Profile_Name,Description,DisplayName,DisplayGroup,DisplayPosition,Date_Added)
		  SELECT ID, CDLC_ID,Groupz, Type, Comments,Profile_Name,Description,DisplayName,DisplayGroup,DisplayPosition,Date_Added
		  FROM Groupsu;
	no sections quite a lot..can we add some..? :)
	
## WiP:
(this release)
1.0 b6 (04.03.2024) (pre3) Prototyping GuitarPro simple time distribution for new songs
[x] fix improvedimport no eof no gp5
[x] improved getfirst note, also added get last
[x] fix move of buttons import selection of param screen
[x] fix new attribute to always give if songs match standardization rule
[x] fix some empty/broken audio/album paths
[x] fix some original album paths & code at path fix (not sure where the code issues is)
[x] why there are no Packing details in db
[x] standardise the get records specialyl since SQL-Lite might return an empty record=1 if tableempty
[x] save setting doesnt seem to work
[x] 3rd extension was not cleaned (psarc from celand folders
[x] fixes on cleanups
[x] further sqlite improve impl strcom,switch,ucase etc.
	[x] weird pack no
	[x] why zero packed songs
	[x] sqlite dlcmnanger 1700 also made theser calls avaiul in sqlite
[x] where is sample (not the case on single processing as anyway sumamry at end)
[x] sqlite other windows (not required as now selects etc standardized only in utilities fuction files)
[x] improved packing no detection at single and harmogenised with mass same function
[x] harmonised generate param list same function, and added general to all (still to do put it sec :))
[x] generate packing stats ignores platform case
[x] workaround sqlite table with id not null contrains not being able to insert in even if not inserting id
[-] add instrumntal AI split https://github.com/stemrollerapp/stemroller
[-] fix search 2-3times
[-] full album not detected
[-] setting attribute no displayed
[-] fix acdc
[-] check full files
[-] check for file size zero
[-] if preview missing check add full song as
[-] add weird char from file name to path an
[-] add remove doublefiled wems
[-] check twhat wems i deleted
[-] check wehat bnk still are after cleanup
[x] Has_Alternate_Lyrics
[x] Has_Alternate_Audio
[x] a440
[x] ukulele
[x] Metallcover
[x] mix
[x]  make attributes dynamically position  themselves
[x] when changig metadata give a change to edit
[x] remaster not detected on album (added)
[-] dont add packing grp to meta
[x] fix last meta
[x] check digitec on torn cover by neck
[x] strech few maindb
[x] increse font in error window
[x] fix pack regress issue
[x] move call to proced as to show progress
[x] fixstandardization screen zoom 
[x] fixstandardization screen load
[x] open standard doesnt getsu top the artist&album
[x] pack no doenst increase(failed pack is the same untill increased) (maybe get max and insease then)its ok as isO)
[x] fix add attribute through right click
[x] if indiv repack is not in failed dont mark as broken :)
[x] reinforced packing audio file using logic (mostly cause of ps3 issues) and error reporting out of it
[x] enhance right clicks on the 3 right click menu grps
[x] fix duplicate groups
[x] if you dont add stuff to lyrics display a warning :)(ConfigRepository.Instance()["dlcm_AdditionalManipul73"] == "Yes" && filez.Has_Vocals == "Yes" && c("dlcm_AdditionalManipul102") != "Yes") 
[x] increase song details to max-1 in lyrics
[x] add def dlc_id_
[x] dlcname should be part of getmeta
[x] (qnap modif file) fix bass dankoC:\t\0\0_data\Pc_CDLC_Danko Jones_2015_Fire Music_0_Gonna Be A Fight Tonight_12128\songs\arr\fakk_djtgbaft_bass.xml
[x] add title to error window
[x] 	Failed at packing (1010970) :4 (inprov clean path of umlaut chars)
[x] fix sections not being gathered since 2021 :( :( :( :( :( 
[x] add regather attributes (tunning frecv, sections++later)
[x] fixed no doubt sunda qnap broken bass
[x] C:\t\0\0_data\Pc_CDLC_Tommy Johansson_2022_I'm Still Standing_0_I'm Still Standing_14244 fixed missin audio and bnk
[x] C:\t\0\0_data\Pc_CDLC_Rise Against_2001_The Unraveling_0_Faint Resemblance_7583\manifests\songs_dlc_cusrafaintresemblance fixed empty json
[x] fix dlcm opening
[-] fix import when w duplic doenst import new
[] why grp is not correct for smnoko
[] whatsw eof

## done:
(prev release)
1.0 b6 (10.07.2023) Finalising GuitarPro simple time distri…
[x] fix expand move notes (inlc negative move)
[x] fix custom attributes use for standard in getextrattrib
[x] fix duplications no running at end
[x] fix get extra atrib running at end
[.] fix old is not copied (fed up options , whodid it?)
[x] fix sqlite delete conv when smallewr letters
[x] again fixing pack
	[x] (NOT waas a opck issue) add info for not not imp folders/non songs(dclm file)
[x] at delete/copy_old dont delete/copy if old not avail
[-] fixed sqlite select when from is small caps and also standarized at select " to ' and added ;
[x] fix sql-lite standarization issues when replacing strcomp w iif (util files)
[]change to wem audiokinetik 2023
[]remove old cell based ref inmaindb
[]add to way 1&2:
	[] dont move 1
	[] only move if all stay before of eachother
[] import songs w audio issues (ogg file; also adding additional 2019 wem ref)
	overcome the audio issues
	[x] nin
	hives
	[x] 8sodagreen
	Only the Good Die Young
	Rise-Against_Sooner-or-Later_v1
	Killers_GLAMOROUS-INDIE-ROCK-N-ROLL
[-] reimport old missing imports button
[-] add bonus&parts and full no of tracks to duplicate manag
	[] is alternate track also any option?
[.] u2 vulcano
[] fix button autosize font in mainDB
check duplicate window issue in displaying/comparing tracks


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
	0.2.0.10 (05.05.2015) save settings in Toolkit config, getrck no, Groupz, Add 30 sec preview midsong; close bugs on Conversion to Ps3(nin hell)
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
	1.0 b1 (29.02.2021) Making Param sorted by type and configurable in the DB
	1.0 b2 (29.03.2021) backend fixes
	1.0 b3 (29.05.2021) Add DLCs directyly intro GAmes fiels (songs.psarc and implicitely the hsan into cache.psarc)
	1.0 b4 (29.07.2021) moved to .NET6 as to allow development in windows for ARM (Apple,etc.)added SQLite capabiltites (removes dependency on ACCESS on Windows for ARM as sometimes not being detected)
	1.0 b5 (09.10.2022) startup/dependencies improvements, weekly dyanmic last 5 in monthly hot list and sqlite further integration
	1.0 b6 (12.02.2024) (pre3) Prototyping GuitarPro to Rocksmith workflow for simple time distribution for new songs
	1.0 b7 (9.05.2024) Finalising addings songs directly to CACHE (Pc works, targetting Ps3 and Ps4)
	1.0 b8(xx.11.2024) Released on Customforge and GitHub Release "tab" (2 versions one w all 3rd party software, one without; can be installed/decompressed and quickly used; 1 60sec video describing why you should use this)
	1.1 (xx.12.2024) Reactivating Spotify checks

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
+ Use MS2022 community or whateva other older version
+ Start Visual Studio with Admin rights (no clue but it requires to debug)
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

		-NVORBIS library - reading ogg lenght
		https://nvorbis.codeplex.com/documentation

		- PS3xploit resigner - resigning pkg for PS3 HAN enabled CDLCs
		https://github.com/PS3Xploit/PS3xploit-resigner

		- TrueAncestor PKG Creator - packing PS3 HAN enabled CDLCs
		http://www.psx-place.com/threads/trueancestor-pkg-repacker-v2-45-by-jjkkyu.10067/#post-48238

		- PKG Linker - WEBServer for PS3 HAN enabled delivered packages
		http://www.psx-place.com/threads/pkg-linker-2-0-serve-packages-to-your-ps3-han-cfw.17252/page-20#post-125162

		- Microsoft Access 2016 driver - for usage on x32 x64 xARM(when and if working) to use and access .accdb file format to store meta-data info on each song
			https://www.microsoft.com/en-us/download/details.aspx?id=54920
			https://www.microsoft.com/en-us/download/details.aspx?id=13255
			2010 for 32bit viewers/editors

		- MDB Viewver 2.63 - alternative view of mdb container DB
		http://www.alexnolan.net/software/mdb_viewer_plus.htm

		- Database .NET v35.2 (on win4ARM use Safe emulation in Compatibiltiy) - generic DB editor for .accdb (microsoft access) and .db (sqlite3)
		http://fishcodelib.com/Database.htm 

		-DevOnly additional software
			EOF v1.8b (c)2008-2010 T³ Software eof1.8RC12(26-09-2020) http://ignition.customsforge.com/eof http://customsforge.com/topic/1529-latest-eof-releases-5-19-2016/page-86 https://github.com/raynebc/editor-on-fire -4 transforming lyrics into RS Vocals
			UltraStar Creator 1.2 https://sourceforge.net/projects/usc/ - 4 creating lyrics files to import in EoF
			TotalCommander v11b1 x64 (doubleckick on zip to install plugin, pack with no compression for cache.ps3) https://gisler.com -4Encripting PS3 Retail Sog PSARCS (0 encription level only avail here)
			MediaInfo CLI v23 x64 https://mediaarea.net/en/MediaInfo/Download/Windows -4checking wem bitrate
			WinMerge v2.16.28 x64 http://winmerge.org/?lang=en -used in comparing duplicates (and their respecitve differential track)
			C3 Tools 4.1 https://rhythmgamingworld.com/forums/topic/c3-con-tools-v401-8142020-weve-only-just-begun/ -used to decompress songs made for Rockband to quickly copy their vocal track to Rocksmith
			Rocksmith Mods 1.6.0.3 -for tweaking e.g. remove UI elements to allow streaming of overlay-ing of videos trough OBC https://github.com/Lovrom8/RSMods
			Custom DLC enabler OSX - only way to play songs not sold by Ubioft/Rocksmith-store on Mac https://github.com/aik002/RSBypass
			Custom DLC enabler PC - only way to play songs not sold by Ubioft/Rocksmith-store on Windows https://customsforge.com/index.php?/topic/901-how-to-use-custom-dlcs-in-rs2014-remastered/
			SQLite3 driver x64-for reading .db slq-lite-3 (windows 64 ONLY) database by commandLine or ODBC through Microsoft Access UI http://www.ch-werner.de/sqliteodbc/
			DLC builder 1.62 - usefuly for generating a notes/EoF-file out a psarc https://github.com/iminashi/Rocksmith2014.NET
			StemRoller 2.0.2 - AI splitting of tracks https://github.com/stemrollerapp
## Contact

mailto:bogdan@capi.ro  
http://capi.ro/  
https://github.com/catara/rocksmith-custom-song-toolkit

# Rocksmith DLC Library Manager [![Latest release](http://img.shields.io/github/release/catara/rocksmith-custom-song-toolkit.svg)](https://github.com/catara/rocksmith-custom-song-toolkit/releases/)

