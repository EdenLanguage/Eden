echo off
cd /d "%~dp0"

:: Check if the script is running with Administrator rights
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: This script requires Administrator rights.
    goto :eof
)

:: Directory location.
set "EdenRuntimeDirectory=%~dp0"
set "RuntimePath=%EdenRuntimeDirectory%Eden.exe"
set "IconPath=%EdenRuntimeDirectory%Logo.ico"
set "SetEnvironmentVarScript=%EdenRuntimeDirectory%CreateEnvVar.ps1"

:: Check if Eden runtime file exists
IF NOT EXIST "%RuntimePath%" (
    echo ERROR: Runtime file not found at "%RuntimePath%"
    goto :eof
)

:: Check if Eden icon file exists
IF NOT EXIST "%IconPath%" (
    echo ERROR: Icon file not found at "%IconPath%"
    goto :eof
)

:: Check if the PowerShell script exists
IF NOT EXIST "%SetEnvironmentVarScript%" (
    echo ERROR: File not found at "%SetEnvironmentVarScript%"
    goto :eof
)

powershell.exe -ExecutionPolicy Bypass -File "CreateEnvVar.ps1"
if %ERRORLEVEL% neq 0 (
    echo ERROR: The operation failed to add environment variable.
    goto :eof
)

:: Set file association for .eden files
reg add "HKEY_CLASSES_ROOT\.eden" /ve /t REG_SZ /d "EdenFile" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\.eden
    goto :eof
)

:: Set 'ShellNew' registry for right-click "Create Eden File"
reg add "HKEY_CLASSES_ROOT\.eden\ShellNew" /v "FileName" /t REG_SZ /d "%SystemRoot%\system32\notepad.exe,-470" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\.eden\ShellNew
    goto :eof
)

:: Set EdenFile registry to specify the type
reg add "HKEY_CLASSES_ROOT\EdenFile" /ve /t REG_SZ /d "Eden Script" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\EdenFile
    goto :eof
)

:: Configure command for opening .eden files
reg add "HKEY_CLASSES_ROOT\EdenFile\shell\open\command" /ve /t REG_SZ /d "\"%RuntimePath%\" \"%%1\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\EdenFile\shell\open\command
    goto :eof
)

:: Set the custom icon for EdenFile
reg add "HKEY_CLASSES_ROOT\EdenFile\DefaultIcon" /ve /t REG_SZ /d "%IconPath%" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\EdenFile\DefaultIcon
    goto :eof
)

:: Add context menu entry (right-click background)
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /ve /d "Create new Eden script" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\Directory\Background\Shell\Create Eden File
    goto :eof
)

:: Set icon for context menu item
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /v "Icon" /t REG_SZ /d "%IconPath%" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append icon registry entry at HKCR\Directory\Background\Shell\Create Eden File
    goto :eof
)

:: Set command for context menu (background right-click)
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%V\\NewFile.eden\" && start \"\" notepad \"%%V\\NewFile.eden\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append command registry entry at HKCR\Directory\Background\Shell\Create Eden File
    goto :eof
)

:: Add context menu entry (right-click folder)
reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /ve /d "Create New .eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append registry entry at HKCR\Directory\Shell\Create Eden File
    goto :eof
)

:: Set command for context menu (folder right-click)
reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%1\\NewFile.eden\" && start \"\" notepad \"%%1\\NewFile.eden\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to append command registry entry at HKCR\Directory\Shell\Create Eden File
    goto :eof
)

:: Refresh Explorer to apply changes
taskkill /f /im explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to kill 'explorer.exe'. It may need to be manually restarted.
)

start explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to start 'explorer.exe'. It may need to be manually restarted.
)

echo EdenRuntime has been successfully added to this machine.