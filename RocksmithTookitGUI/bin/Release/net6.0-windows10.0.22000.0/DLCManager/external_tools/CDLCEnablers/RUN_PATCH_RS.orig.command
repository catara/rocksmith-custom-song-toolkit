RS_PATH="/Volumes/HFS/SteamLibrary/steamapps/common/Rocksmith2014/Rocksmith2014.app/Contents/MacOS"
cd "`dirname "$0"`"
cp ./libRSBypass.dylib "$RS_PATH/"
./insert_dylib --inplace "$RS_PATH/libRSBypass.dylib" "$RS_PATH/Rocksmith2014"