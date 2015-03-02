App Description: MASS Manipulation of Rocksmith DLC Library 
            e.g. 1. in Rocksmith in the Library, each songs Album, to contain a personal rating, if it has DD, instr. avail
            e.g. 2. in Rocksmith in the Library, each song to be sorted by Album(Year) and Track No



DLC Library Manager v0.2.0.7 (beta version)
Main Features:
- Gather all DLCs metadata into 1 Access DB
	- Manage Duplicates
	- Edit Individual metadata fields
- Mass Modify meta data @repack per each Rocksmith song: e.g. Album Field: <Broken><Year> - <Album> - r<Rating> - <Avail. Instr.> - <DD> - <Tuning>
- Mass add/remove DD @repack (inc. Bass only option)
- Mass rename songs e.g. Black Keys->The Black Keys and maintain changes in a local DB


Known Issues:
- Import fails randomly...Import again (dont clear the DB)
- Edit of Arragements and Tones DB can only be done from Access for the moment

ToDos/bugs:
- feat:		 Alternate No for duplicates logic
- feat:		 Include Standardization names into duplication checks
- small improv: add audio hash also as duplicate criteria
- bug?:		 pin wish now last convtime at alternate comparision screen
- small bug: on inserting a new standardization 
- big bug: repack&import seems to randmly fail
- small bug: last thibgs by billy talend fails at unpack/rorg
- small improv: add the proper lasconvdate in the db 
- small bug: sometimes preview shows empty but the vs is not red incubus echo
- big feature: get the volume of the audio file and then compare against the rest or a norm
- small bug: mj beat it issue with adding older to titles and so changing the xml path
- audioslave sections missing maybe cause its an original and i used my own logic to strip the DD
- medium bug: uncompressed ogg preview song has a diff name the wem original preview (not incl.ogg or _fixed)
- big feature - slimdown/control DLC/ORIG packages (multiplatform)
- big feature - For the tagging add the info to the Preview Image/Album Art
- small improv: Any change of song in the list should also save :)
- big feature: Manage RS12, RS12 DLC & RS14 retail songs
- medium feature: add DLCs into cache.psarc to speed up the game startup

dev issues:
- Toolkit version flag does not sync to&from Github
- Bin\debug folder content is needed(rockmisht.lib)
- activate debug(can't remember how i did it....)

Version History(release date):
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
	wip: 0.2.0.8 (20.02.2015) (20%) has_section flag(to be tested with a song missing sections); Add 30 sec preview midsong; 
	wip: 0.2.0.9 (20.02.2015) (10%) close bugs on Conversion to Ps3(nin hell) and analyse acuracy on conversion to ps3 (1979,7army..)
	wip: 0.2.0.10 (22.02.2015)  (90%)remove bug on auto import if original
	wip: 0.2.0.11 (24.12) (85%) full release (anyone can download and use the tool..no bugs..and all unimplemented featues disabled) DB independece/dependence on already provided 1, repack wo bugs, edit screens functional
	wip: 0.2.2 (31.03.2015) (10%) HTML&Excel exports
	tbr: 0.2.2.1 (15.04.2015) Implement a logic to properly read DLCManager renamed DLCs
	wip: 0.2.3 (26.04.2015) (75%) If importing an original over a alternate the alternate flag should be set no the Alternate
	tbr: 0.3.1 move Import DB to Main.DB or at least use an official data source as DB source to also be able to edit from the grid
	tbr: 0.3.2 ?move Access code to project? or from hardcoded to views
	tbr: 0.3.3 (12.05.2015) (0%) save settings in Toolkit config
	wip: 0.3.4 (03.06.2015) (70%) use parameterized SQL everywhere (&/ integrate template DB into project or a SQL DB)
	wip: 0.3.4 (03.07.2015) (30%) Remove getlastconversiondate from Duplicates as already coming from the arrangements table
	wip: 0.4 (21.08.2015) (75%) Redesign MainDB+Edit Screen
	tbr: 0.5 (26.09.2015) (0%) Implement FTP to PS3 (also as a copy to any other location)


Date: 20.01.2015
Document Name: Rocksmith DLC Management tool README
				(fork of rocksmith-custom-song-toolkit)
Document purpose: To describe the functionailities and the way to change, the NEW tab that enable MASS Manipulation of Rocksmith DLC Library
					(DLC folder; including customs(CDLC), DLCs and songs embeded in the ready to ship version of Rocksmith 2014) 
Legend:
- to be implemented
+ done
_ future release
-- WIP
{} old unimplemented feature

IDE-Setup <old>
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
9. --activate debug by traying to debug?!
10. copy missing dlls from...a prev version !?

Main Features:
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
	- New App ID
	- Convert to Mac & PS3
	- FTP to PS3
	Design description:
		- Replicate functions already existng w a tweak
		- Use a FTP external utility

+4. Ability to Repack all DLCs
	-- ?Original files changed should also be packaged?
		+ Ability to package songs by Groups(e.g. party songs, great bass, rating 10) :)


_5. Future feastures
	- Read the customforge.net website (Parse costonsforge.com and enrich the DB/Library w playthrough videos, yb preview, update required, release notes....)
		- Reconcile the version no
		- Keep Download links
	- Help manage and create MultiTracks
		- Based on a original track plus aaudicity track creat e a new version
	- Manage Official DLCs
		- Ability to create a package out of a list of Official DLCs
			- Initially all DCLS will be manually matched & hardcoded as to the audio file name and the song title
	+ Flag files with no bass, no sections, no lyrics, no preview no DD, riff repeater..
		- FIX when possible
	+ Show duplicates and solve conflicts
	-- Audio Preview full track&preview track
	+ Remove DD (arragement dependent..e.g. only bass)