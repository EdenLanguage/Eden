@echo off

:: .eden file type to registry
reg add "HKEY_CLASSES_ROOT\.eden" /ve /d "EdenFile" /f
reg add "HKEY_CLASSES_ROOT\.eden\ShellNew" /v "FileName" /t REG_SZ /d "%SystemRoot%\system32\notepad.exe,-470" /f

:: Context menu entry
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /ve /d "Create new Eden script" /f
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /v "Icon" /t REG_SZ /d "E:\Repositories\Eden\Eden\EdenRuntime\Logo.ico" /f
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%V\NewFile.eden\" && start \"\" notepad \"%%V\NewFile.eden\"" /f

:: Context menu entry for folders
reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /ve /d "Create New .eden File" /f
reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%1\NewFile.eden\" && start \"\" notepad \"%%1\NewFile.eden\"" /f