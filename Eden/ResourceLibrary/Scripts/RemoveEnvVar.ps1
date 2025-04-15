# === CONFIGURATION ===
# Get the current script directory
$pathToRemove = $PSScriptRoot
$backupFile = "$env:SystemDrive\PathBackup_Eden_Remove.txt"

Write-Output "Script path: '$pathToRemove'"

# Check if Eden.exe exists in the same folder
$edenExecutable = Join-Path $pathToRemove "Eden.exe"
if (-not (Test-Path $edenExecutable)) {
    Write-Output "ERROR: Eden.exe not found in '$pathToRemove'. Aborting."
    exit 1
}

# === FETCH CURRENT PATH ===
$currentPath = [System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine)

# === BACKUP THE ORIGINAL PATH ===
try {
    Set-Content -Path $backupFile -Value $currentPath -Encoding UTF8
    Write-Output "Original system PATH backed up to: $backupFile"
} catch {
    Write-Output "Failed to create backup: $_"
	exit 1
}

# === PROCESS PATHS ===
$paths = $currentPath -split ";" | ForEach-Object { $_.Trim() }
$newPaths = $paths | Where-Object { $_ -ne $pathToRemove }
$newPath = ($newPaths -join ";").TrimEnd(";")

# === APPLY CHANGES IF NEEDED ===
if ($paths.Count -ne $newPaths.Count) {
    try {
        [System.Environment]::SetEnvironmentVariable("Path", $newPath, [System.EnvironmentVariableTarget]::Machine)
        Write-Output "Path '$pathToRemove' has been removed from the system PATH."
    } catch {
        Write-Output "Failed to update the system PATH: $_"
		exit 1
    }
} else {
    Write-Output "Path '$pathToRemove' was not found in the system PATH. No changes made."
}