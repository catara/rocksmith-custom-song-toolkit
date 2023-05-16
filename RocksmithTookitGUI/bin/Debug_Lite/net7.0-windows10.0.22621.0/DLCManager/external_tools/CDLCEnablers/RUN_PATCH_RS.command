RS_PATH="/Volumes/Drive/Users/ladmin/SteamLibrary/steamapps/common/Rocksmith2014/Rocksmith2014.app/Contents/MacOS"
cd "`dirname "$0"`"
cp ./libRSBypass.dylib "$RS_PATH/"
./insert_dylib --inplace "/Volumes/Drive/Users/ladmin/SteamLibrary/steamapps/common/Rocksmith2014/Rocksmith2014.app/Contents/MacOS/libRSBypass.dylib" "/Volumes/Drive/Users/ladmin/SteamLibrary/steamapps/common/Rocksmith2014/Rocksmith2014.app/Contents/MacOS/Rocksmith2014"
