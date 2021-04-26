psarc v1.3 - wcx plugin for Total Commander.

This plugin allows to extract/view/create Sony PlayStation 3 (PS3)
PSARC format archives.
Supported compression algorithm are ZLIB and LZMA.
Multi CPU capability.

How to install the plugin in Total Commander:
1. Double click on the plugin zip file within Total Commander
2. Follow the instructions

Author:
BeketAta

History:
03.22.13 - Release v1.4
02.15.13 - Release v1.3
01.27.13 - Release v1.2
01.01.13 - Release v1.1
11.11.11 - Release v1.0

-------------------------------------------------------------------------------
What's new in v1.4:

+ Backward compatibility to Windows XP added.

+ Plugin crashing on opening PSARC with unknown compression algorithm fixed.
  E.g. in "PlayStation All-Stars Battle Royale" PSARCs.

-------------------------------------------------------------------------------
What's new in v1.3:

+ There is a new option "Absolute Path Names" in the Plugin Options
  dialog is added. Path names will be starting with \ when it's checked.
  It's recommended to set it the same as in the original psarc.
  You can see this option in the "PSARC" sheet of the standard Windows dialog
  box "File Properties" in the line "Path Names" for the original psarc.

-------------------------------------------------------------------------------
What's new in v1.2:

+ ZLIB compression algorithm is added.
  For exctracting it's chosen automatically, for creating new archive selected
  in the Plugin Options dialog.

+ Compare the content of all files, and merge identical files so that only one
  copy of the data is included to the new archive file.
  For creating new archive this feature selected in the Plugin Options dialog.

+ New property sheet "PSARC" is added to the standard Windows dialog box
  "File Properties". It's provide information about "PSARC Version",
  "Compression Algorithm", "MD5 Ignore Case" for calculating checksum of
  file names, "Number of Files".
  This new tab "PSARC" will be in place for the psarc archive files only.
  
+ There is a new option for selecting "PSARC Version" in the Plugin Options
  dialog is added. Actually it's do nothing except of adding this version
  number to the internal header of the new created archive.
  It's recommended to set it the same as in the original psarc.
  
+ Background working feature is added.
 
-------------------------------------------------------------------------------
What's new in v1.1:

+ The new unpacking method is added for a few games
  where several files in a PSARC contains identical data.

-------------------------------------------------------------------------------
