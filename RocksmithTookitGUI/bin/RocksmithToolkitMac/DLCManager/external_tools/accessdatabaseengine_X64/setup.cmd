@echo off

REM Install Universal CRT if necessary
InstallUCRT.exe

REM InstallUCRT returns 0 for insalled, 1 for already_installed, 2 for failed
IF %ERRORLEVEL% equ 2 (
	echo Failure installing Universal CRT. Aborting installation...
	goto :EOF
)

REM Install Access Database Engine Redistributable
msiexec %1 /i AceRedist.msi
