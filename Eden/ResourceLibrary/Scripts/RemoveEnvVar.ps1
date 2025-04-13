$pathToRemove = "C:\Program Files (x86)\Eden"; 
$systemVariables = [System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine); 

# Trim the path for any surrounding spaces
$systemVariables = $systemVariables.Trim()

# Match the exact path including possible leading/trailing spaces or semicolons
if ($systemVariables -match "(\b" + [Regex]::Escape($pathToRemove) + "\b)(;|$)") { 
    $newPath = ($systemVariables -split ";" | Where-Object { $_.Trim() -ne $pathToRemove }) -join ";"; 
    [System.Environment]::SetEnvironmentVariable("Path", $newPath, [System.EnvironmentVariableTarget]::Machine); 
    Write-Output "The path '$pathToRemove' has been removed from the system environment variables."; 
} else { 
    Write-Output "The path '$pathToRemove' was not found in the system environment variables.";
}
[System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine)
