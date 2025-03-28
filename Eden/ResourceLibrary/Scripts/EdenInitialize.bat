@echo off

:: DEFINE INSTALATION FOLDER!!!!!!!!
set "EdenPath=E:\Repositories\Eden\Eden\EdenRuntime\bin\Debug\net8.0"
set "EdenExe=%EdenPath%\Eden.exe"
set "IconPath=E:\Repositories\Eden\Eden\EdenRuntime\Logo.ico"

:: Ensure the Eden directory exists
if not exist "%EdenPath%" (
    echo Error: The directory %EdenPath% does not exist.
    exit /b
)

:: Add Eden interpreter to the system PATH (if not already added)
for /f "tokens=*" %%P in ('powershell -command "[System.Environment]::GetEnvironmentVariable('Path', 'Machine')"') do set CURRENT_PATH=%%P
echo %CURRENT_PATH% | findstr /I /C:"%EdenPath%" >nul
if errorlevel 1 (
    setx PATH "%CURRENT_PATH%;%EdenPath%" /M
    echo Eden added to PATH.
) else (
    echo Eden is already in PATH.
)

:: Set the file association
reg add "HKEY_CLASSES_ROOT\.eden" /ve /t REG_SZ /d "EdenFile" /f
reg add "HKEY_CLASSES_ROOT\EdenFile" /ve /t REG_SZ /d "Eden Script" /f
reg add "HKEY_CLASSES_ROOT\EdenFile\shell\open\command" /ve /d "E:\Repositories\Eden\Eden\EdenRuntime\bin\Debug\net8.0\Eden.exe %%1\"

:: Set the custom icon for Eden files
reg add "HKEY_CLASSES_ROOT\EdenFile\DefaultIcon" /ve /t REG_SZ /d "%IconPath%" /f

call EdenCreateContextMenu.bat

:: Refresh Explorer
taskkill /f /im explorer.exe
start explorer.exe