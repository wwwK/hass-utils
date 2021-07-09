$PublishDirectoryName  = "artifacts";
$Configuration         = "Release";
$Version               = "1.0.0.0-local";

# Setup directories
$workingDir            = $PSScriptRoot;
$sourceDir             = Join-Path $workingDir "src";
$sourceApplicationDir  = Join-Path $sourceDir "HassUtils";
$publishDir            = Join-Path $workingDir $PublishDirectoryName;
$tempDir               = Join-Path $publishDir "temp";
$applicationTempDir    = Join-Path $tempDir "application";


# Build and Restore (Restore is implicit)
$buildCmd           = "dotnet build $sourceDir --configuration $Configuration /p:Version=$Version" 
Invoke-Expression $buildCmd;

#Publish Application
$publishCmd       = "dotnet publish $sourceApplicationDir --configuration $Configuration --no-restore -o $applicationTempDir";
Invoke-Expression $publishCmd;
