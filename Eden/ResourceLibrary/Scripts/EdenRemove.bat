@echo off
setlocal
cd /d "%~dp0"

net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Script is not running with Admin rights!
    goto :eof
)

set "EdenRuntimeDirectory=%~dp0"
set "RemoveEnvironmentVarScript=%EdenRuntimeDirectory%RemoveEnvVar.ps1"

IF NOT EXIST "%RemoveEnvironmentVarScript%" (
    echo File not found at "%RemoveEnvironmentVarScript%"
    goto :eof
)

:: Remove file associations silently
reg delete "HKEY_CLASSES_ROOT\.eden" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to remove registry entry at HKCR\.eden
	goto :eof
)

reg delete "HKEY_CLASSES_ROOT\EdenFile" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to remove registry entry at HKCR\EdenFile
	goto :eof
)

reg delete "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to remove registry entry at HKCR\Directory\Background\Shell\Create Eden File
	goto :eof
)

reg delete "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
	echo Failed to remove registry entry at HKCR\Directory\Shell\Create Eden File
	goto :eof
)

powershell.exe -ExecutionPolicy Bypass -File "RemoveEnvVar.ps1" >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed remove enviroment variable.
	goto :eof
)

:: Refresh Explorer silently
taskkill /f /im explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed to kill 'explorer.exe'.
)

start explorer.exe >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo The operation failed to start 'explorer.exe'%.
)

echo EdenRuntime has beed deleted from this machine.