@echo off

:: DEFINE INSTALATION FOLDER!!!!!!!!
set "EdenPath=E:\Repositories\Eden\Eden\EdenRuntime\bin\Debug\net8.0"

:: Remove the file association
reg delete "HKEY_CLASSES_ROOT\.eden" /f
reg delete "HKEY_CLASSES_ROOT\EdenFile" /f
reg delete "HKEY_CLASSES_ROOT\EdenFile\shell\open\command" /f

:: Remove Eden interpreter from the system PATH using PowerShell
call PSRemoveEnvRunner.bat
call EdenDeleteContextMenu.bat

:: Refresh Explorer
taskkill /f /im explorer.exe
start explorer.exe