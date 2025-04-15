echo off
cd /d "%~dp0"

:: Check if the script is running with Administrator rights
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: This script requires Administrator rights.
    goto :eof
)

set "EdenRuntimeDirectory=%~dp0"
set "RemoveEnvironmentVarScript=%EdenRuntimeDirectory%RemoveEnvVar.ps1"

:: Check if the PowerShell script exists
IF NOT EXIST "%RemoveEnvironmentVarScript%" (
    echo ERROR: File not found at "%RemoveEnvironmentVarScript%"
    goto :eof
)

:: Remove file associations
reg delete "HKEY_CLASSES_ROOT\.eden" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to remove registry entry at HKCR\.eden
    goto :eof
)

reg delete "HKEY_CLASSES_ROOT\EdenFile" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to remove registry entry at HKCR\EdenFile
    goto :eof
)

reg delete "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to remove registry entry at HKCR\Directory\Background\Shell\Create Eden File
    goto :eof
)

reg delete "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /f >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo ERROR: Failed to remove registry entry at HKCR\Directory\Shell\Create Eden File
    goto :eof
)

powershell.exe -ExecutionPolicy Bypass -File "RemoveEnvVar.ps1"
if %ERRORLEVEL% neq 0 (
    echo ERROR: The operation failed to remove environment variable.
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

echo EdenRuntime has been successfully deleted from this machine.