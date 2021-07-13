param (
  $output         = $PSScriptRoot,
  $project        = "HassUtils",
  $configuration  = "Release"
)

$outputRoot         = Join-Path $output "\";
$workingRoot        = Join-Path $PSScriptRoot "\";
$sourceDir          = Join-Path $workingRoot "src";
$projectRootDir     = Join-Path $sourceDir $project;

$artifactDir        = Join-Path $outputRoot "artifacts";
$publisRoot         = Join-Path $artifactDir "publish";
$publishDir         = Join-Path $publisRoot $project;
$fullPublishDir     = Join-Path $publishDir "\";


# =============================================================================
# Build project
# =============================================================================
#
$buildCmd         = "dotnet build $projectRootDir --configuration $configuration --output `"$fullPublishDir`""
Write-Host        "Running Build: $buildCmd"
Invoke-Expression $buildCmd;
