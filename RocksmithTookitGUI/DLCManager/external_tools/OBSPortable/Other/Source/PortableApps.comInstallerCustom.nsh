Var bolCustomCreatedLegacy

!macro CustomCodePreInstall
	System::Call kernel32::GetCurrentProcess()i.s
	System::Call kernel32::IsWow64Process(is,*i.r0)
	ReadRegStr $1 HKLM "Software\Microsoft\Windows NT\CurrentVersion" "CurrentBuild"
	
	${If} $1 < 10000 ;Windows 7/8
	${OrIf} $0 == 0 ;or 32-bit 10
		ReadINIStr $0 "$INSTDIR\App\AppInfo\appinfo.ini" "Version" "PackageVersion"
		${VersionCompare} $0 "30.0.0.0" $R0
		${If} $R0 = 2
			${GetParent} $INSTDIR $1
			${IfNot} ${FileExists} "$1\OBSPortableLegacy32bit\*.*"
				${GetParent} $INSTDIR $1
				CreateDirectory "$1\OBSPortableLegacy32bit"
				CreateDirectory "$1\OBSPortableLegacy32bit\App"
				CreateDirectory "$1\OBSPortableLegacy32bit\App\AppInfo"
				CreateDirectory "$1\OBSPortableLegacy32bit\App\DefaultData"
				CreateDirectory "$1\OBSPortableLegacy32bit\Data"
				CreateDirectory "$1\OBSPortableLegacy32bit\Other"
				CopyFiles /SILENT /FILESONLY "$INSTDIR\*.*" "$1\OBSPortableLegacy32bit"
				CopyFiles /SILENT "$INSTDIR\App\AppInfo\*.*" "$1\OBSPortableLegacy32bit\App\AppInfo"
				CopyFiles /SILENT "$INSTDIR\App\DefaultData\*.*" "$1\OBSPortableLegacy32bit\App\DefaultData"
				Rename "$INSTDIR\App\obs-studio32" "$1\OBSPortableLegacy32bit\App\obs-studio32"
				CopyFiles /SILENT "$INSTDIR\Data\*.*" "$1\OBSPortableLegacy32bit\Data"
				CopyFiles /SILENT "$INSTDIR\Other\*.*" "$1\OBSPortableLegacy32bit\Other"
				StrCpy $bolCustomCreatedLegacy true
			${EndIf}	
		${EndIf}
	${EndIf}
!macroend

!macro CustomCodePostInstall
	${If} $bolCustomCreatedLegacy == true
		${GetParent} $INSTDIR $1
		Delete "$1\OBSPortableLegacy32bit\OBSPortable.exe"
		Delete "$1\OBSPortableLegacy32bit\App\AppInfo\Launcher\custom.nsh"
		Delete "$1\OBSPortableLegacy32bit\App\AppInfo\Launcher\OBSPortable.ini"
		CopyFiles /SILENT "$INSTDIR\Other\Source\Legacy\OBSPortableLegacy32bit.exe" "$1\OBSPortableLegacy32bit"
		CopyFiles /SILENT "$INSTDIR\Other\Source\Legacy\OBSPortableLegacy32bit.ini" "$1\OBSPortableLegacy32bit\App\AppInfo\Launcher"
		CopyFiles /SILENT "$INSTDIR\Other\Source\Legacy\appinfo.ini" "$1\OBSPortableLegacy32bit\App\AppInfo"
	${EndIf}
!macroend
