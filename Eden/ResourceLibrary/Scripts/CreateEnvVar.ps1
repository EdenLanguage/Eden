# === CONFIGURATION ===
# Get the current script directory
$pathToAdd = $PSScriptRoot
$backupFile = "$env:SystemDrive\PathBackup_Eden_Add.txt"

Write-Output "Script path: '$pathToAdd'"

# Check if Eden.exe exists in the same folder
$edenExecutable = Join-Path $pathToAdd "Eden.exe"
if (-not (Test-Path $edenExecutable)) {
    Write-Output "ERROR: Eden.exe not found in '$pathToAdd'. Aborting."
    exit 1
}

# === FETCH CURRENT SYSTEM PATH ===
$currentPath = [System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine)

# === BACKUP THE ORIGINAL PATH ===
try {
    Set-Content -Path $backupFile -Value $currentPath -Encoding UTF8
    Write-Output "Original system PATH backed up to: $backupFile"
} catch {
    Write-Output "Failed to create backup: $_"
	exit 1
}

# === CLEAN AND SPLIT PATHS ===
$paths = $currentPath -split ";" | ForEach-Object { $_.Trim() }

# === CHECK FOR DUPLICATE ===
if ($paths -contains $pathToAdd) {
    Write-Output "Path '$pathToAdd' is already in the system PATH. No changes made."
} else {
    $newPaths = $paths + $pathToAdd
    $newPath = ($newPaths -join ";").TrimEnd(";")

    # === APPLY CHANGES ===
    try {
        [System.Environment]::SetEnvironmentVariable("Path", $newPath, [System.EnvironmentVariableTarget]::Machine)
        Write-Output "Path '$pathToAdd' has been added to the system PATH."
    } catch {
        Write-Output "Failed to update system PATH: $_"
		exit 1
    }
}