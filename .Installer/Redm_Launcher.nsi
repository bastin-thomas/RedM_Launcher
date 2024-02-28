;NSIS Modern User Interface
;Welcome/Finish Page Example Script
;Written by Joost Verburg

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;Include Dialog creator

  !include "nsDialogs.nsh"

;--------------------------------
;General

  ;Name and file
  Name "RedM Launcher"
  OutFile "RedM_Launcher-1.0.0.1-Installer.exe"
  Unicode True

  ;Default installation folder
  InstallDir "$LOCALAPPDATA\RedM_Launcher"

  ;Request application privileges for Windows Vista
  RequestExecutionLevel user

;--------------------------------
;Interface Settings

  !define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange.bmp"
  !define MUI_ABORTWARNING
  !define MUI_ICON "E:\GitHub\RedM_Launcher\.Installer\Redm.ico"
  !define MUI_UNICON "E:\GitHub\RedM_Launcher\.Installer\Redm.ico"

  ; !define MUI_BGCOLOR 252525
  ; !define MUI_TEXTCOLOR FFFFFF

;--------------------------------
;Options finish

  !define MUI_FINISHPAGE_RUN "$LOCALAPPDATA\RedM_Launcher\RedM_Launcher.exe"
  !define MUI_FINISHPAGE_RUN_TEXT "Launch RedM_Launcher"

;--------------------------------
;Pages

  Function dotnet8
    !insertmacro MUI_HEADER_TEXT "test1" "test2"
  FunctionEnd

  !insertmacro MUI_PAGE_WELCOME
  ;!insertmacro MUI_PAGE_LICENSE "${NSISDIR}\Docs\Modern UI\License.txt"
  ;!insertmacro MUI_PAGE_COMPONENTS
  Page custom dotnet8
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...
  File /r "RedMLauncher_x64\"

  ;Store installation folder
  ;WriteRegStr HKCU "Software\Modern UI Test" "" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

	# Shortcut
  createShortCut "$DESKTOP\RedM Launcher.lnk" "$INSTDIR\RedM Launcher.exe" "" "$INSTDIR\Redm.ico"
 
	# Registry information for add/remove programs
	WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "DisplayName"           "RedM Launcher"
	WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "UninstallString"       "$\"$INSTDIR\uninstall.exe$\""
	WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "QuietUninstallString"  "$\"$INSTDIR\uninstall.exe$\" /S"
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "InstallLocation"       "$\"$INSTDIR$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "DisplayIcon"           "$\"$INSTDIR\Redm.ico$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "Publisher"             "$\"Thomas Bastin$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "HelpLink"              "$\"https://github.com/bastin-thomas/RedM_Launcher/releases/latest$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "URLUpdateInfo"         "$\"https://github.com/bastin-thomas/RedM_Launcher/releases/latest$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "URLInfoAbout"          "$\"https://github.com/bastin-thomas/RedM_Launcher$\""
	WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "DisplayVersion"        "1.0.0.1"
	WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "VersionMajor"        1
	WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "VersionMinor"        0
	# There is no option for modifying or repairing the install
	WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "NoModify" 1
	WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "NoRepair" 1
	# Set the INSTALLSIZE constant (!defined at the top of this script) so Add/Remove Programs can accurately report the size
	WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher" "EstimatedSize" 53800

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  RMDir /r $INSTDIR

	# Shortcut
  Delete "$DESKTOP\RedM Launcher.lnk"

  ;DeleteRegKey /ifempty HKCU "Software\Modern UI Test"
	# Remove uninstaller information from the registry
	DeleteRegKey HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\RedM Launcher"

SectionEnd
