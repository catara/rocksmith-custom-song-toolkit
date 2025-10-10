${SegmentFile}

${SegmentPrePrimary}
	;Loop through profiles
	${If} ${FileExists} "$EXEDIR\Data\config\obs-studio\basic\profiles\*.*"
		ExpandEnvStrings $4 "%PAL:LastDrive%"
		ExpandEnvStrings $5 "%PAL:Drive%"
		ExpandEnvStrings $6 "%PAL:LastPortableAppsBaseDir%"
		ExpandEnvStrings $7 "%PAL:PortableAppsBaseDir%"
		ExpandEnvStrings $8 "%PAL:LastDrive%%PAL:LastPackagePartialDir%"
		ExpandEnvStrings $9 "%PAL:Drive%%PAL:PackagePartialDir%"
		
		${WordReplace} $6 "\" "/" "+" $R6
		${WordReplace} $7 "\" "/" "+" $R7
		${WordReplace} $8 "\" "/" "+" $R8
		${WordReplace} $9 "\" "/" "+" $R9
		${WordReplace} $6 "\" "\\" "+" $6
		${WordReplace} $7 "\" "\\" "+" $7
		${WordReplace} $8 "\" "\\" "+" $8
		${WordReplace} $9 "\" "\\" "+" $9
		
	
		FindFirst $0 $1 "$EXEDIR\Data\config\obs-studio\basic\profiles\*.*"
		${DoWhile} $1 != ""
			${If} $1 != "."
			${AndIf} $1 != ".."
				${If} ${FileExists} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$8\\" "=$9\\"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$6\\" "=$7\\"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$4\\" "=$5\\"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$R8/" "=$R9/"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$R6/" "=$R7/"
					${ReplaceInFile} "$EXEDIR\Data\config\obs-studio\basic\profiles\$1\basic.ini" "=$R4/" "=$R5/"
				${EndIf}
			${EndIf}
			FindNext $0 $1
		${Loop}
		FindClose $0
	${EndIf}
!macroend