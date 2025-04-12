# The path to add to the system 'Path' environment variable
$pathToAdd = "C:\Program Files (x86)\Eden"

# Get the current system 'Path' environment variable
$currentPath = [System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine)

# Check if the path is already in the 'Path' variable to avoid duplicates
if ($currentPath -notcontains $pathToAdd) {
    # Append the new path to the system 'Path' environment variable
    $newPath = $currentPath + ";" + $pathToAdd

    # Set the updated 'Path' variable back to the system
    [System.Environment]::SetEnvironmentVariable("Path", $newPath, [System.EnvironmentVariableTarget]::Machine)
    Write-Output "The path '$pathToAdd' has been added to the system 'Path' variable."
} else {
    Write-Output "The path '$pathToAdd' is already in the system 'Path' variable."
}