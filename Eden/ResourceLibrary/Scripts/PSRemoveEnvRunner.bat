:: This script calls a PowerShell script to remove Eden from the system environment variables.
:: This approach was chosen because running the script directly on my local machine resulted in an error stating that script execution is restricted.
@echo off
powershell.exe -ExecutionPolicy Bypass -File "RemoveEnv.ps1"