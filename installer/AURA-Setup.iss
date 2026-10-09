; Inno Setup Script for AURA Assistant
; Generates a Windows x64 installer executable.

#define MyAppName "AURA Assistant"
#define MyAppVersion "0.4.4"
#define MyAppPublisher "Nexus1889"
#define MyAppURL "https://github.com/Nexus1889/aura-assistant"
#define MyAppExeName "AURA Assistant.exe"

[Setup]
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={userpf}\AURA Assistant
DefaultGroupName={#MyAppName}
OutputBaseFilename=AURA-Assistant-Setup-x64
Compression=lzma
SolidCompression=yes
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest
UsePreviousAppDir=no
CreateAppDir=yes
Compression=lzma
WizardStyle=modern
ShowLanguageDialog=no
DisableDirPage=false
DisableProgramGroupPage=false
AlwaysShowGroupOnReadyPage=yes
UninstallDisplayIcon={app}\{#MyAppExeName}
OutputDir=.

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{#MyAppName} Uninstall"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch AURA Assistant"; Flags: nowait postinstall skipifsilent

[UninstallRun]
Filename: "{uninstallexe}"; Parameters: "/VERYSILENT /NORESTART"; Flags: waituntilterminated
