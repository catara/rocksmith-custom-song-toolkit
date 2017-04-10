RS_PATH="/Users/$USER/Library/Application Support/Steam/steamapps/common/Rocksmith2014/Rocksmith2014.app/Contents/MacOS"
cd "`dirname "$0"`"
cp ./libRSBypass.dylib "$RS_PATH/"
./insert_dylib --inplace "$RS_PATH/libRSBypass.dylib" "$RS_PATH/Rocksmith2014"
