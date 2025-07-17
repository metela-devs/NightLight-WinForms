; Script for Inno Setup

[Setup]
AppName=Night Light
AppVersion=1.0
AppPublisher=Metela devs
DefaultDirName={autopf}\NightLight
DefaultGroupName=Night Light
UninstallDisplayIcon={app}\NightLight.exe
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
OutputBaseFilename=NightLight-Setup

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\drgn\Desktop\ФиНоСв\bin\Release\net8.0-windows\*.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\drgn\Desktop\ФиНоСв\icon.ico"; DestDir: "{app}"

[Icons]
Name: "{group}\Night Light"; Filename: "{app}\NightLight.exe"
Name: "{group}\{cm:UninstallProgram,Night Light}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\Night Light"; Filename: "{app}\NightLight.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\Night Light"; Filename: "{app}\NightLight.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\NightLight.exe"; Description: "{cm:LaunchProgram, Night Light}"; Flags: nowait postinstall skipifsilent

