@echo off
cd /d "%~dp0"

net session >nul 2>&1
if %errorlevel% neq 0 (
	echo Script is not running with Admin rights!
    goto :eof
)

:: Directory location.
set "EdenRuntimeDirectory=%~dp0"
set "RuntimePath=%EdenRuntimeDirectory%Eden.exe"
set "IconPath=%EdenRuntimeDirectory%Logo.ico"

IF NOT EXIST "%RuntimePath%" (
    echo ERROR: Runtime file not found at "%RuntimePath%"
    goto :eof
)

IF NOT EXIST "%IconPath%" (
    echo ERROR: Icon file not found at "%IconPath%"
    goto :eof
)

powershell.exe -ExecutionPolicy Bypass -File "CreateEnvVar.ps1" >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed add enviroment variable.
	goto :eof
)

:: Set the file association silently and overwrite without prompt
reg add "HKEY_CLASSES_ROOT\.eden" /ve /t REG_SZ /d "EdenFile" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\.eden
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\.eden\ShellNew" /v "FileName" /t REG_SZ /d "%SystemRoot%\system32\notepad.exe,-470" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\.eden\ShellNew
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\EdenFile" /ve /t REG_SZ /d "Eden Script" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\EdenFile\shell\open\command" /ve /t REG_SZ /d "\"%RuntimePath%\" \"%%1\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\shell\open\command
	goto :eof
)

:: Set the custom icon silently
reg add "HKEY_CLASSES_ROOT\EdenFile\DefaultIcon" /ve /t REG_SZ /d "%IconPath%" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\DefaultIcon
	goto :eof
)

:: Add context menu entry (right-click background)
reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /ve /d "Create new Eden script" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\Directory\Shell ... 'Create new Eden Script'
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /v "Icon" /t REG_SZ /d "C:\Program Files (x86)\Eden\Logo.ico" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\Directory\Shell ... 'Add icon'
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%V\\NewFile.eden\" && start \"\" notepad \"%%V\\NewFile.eden\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\Directory\Shell ... 'Open with notepad'
	goto :eof
)

:: Add context menu entry (right-click folder)
reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /ve /d "Create New .eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\Directory\Shell ... 'Create new .eden file'
	goto :eof
)

reg add "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File\command" /ve /d "cmd.exe /c echo. > \"%%1\\NewFile.eden\" && start \"\" notepad \"%%1\\NewFile.eden\"" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to append registry entry at HKCR\EdenFile\Directory\Shell ... 'Open new .eden file with notepad'
	goto :eof
)

:: Refresh Explorer silently
taskkill /f /im explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed to kill 'explorer.exe'.
)

start explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed to start 'explorer.exe'.
)

echo EdenRuntime has beed added to this machine.