$cfgSolutionFile    = "HassUtils.sln";
$cfgConfiguration   = "Release";

 
# ===============================================================================
# region :: Setup Directories
# ===============================================================================
$workingDir            = $PSScriptRoot;
$PublishDirectoryName  = "artifacts";
$sourceDir             = Join-Path $workingDir "src";
$toolsDir              = Join-Path $workingDir "tools";
$slnFilePath           = Join-Path $sourceDir $cfgSolutionFile
$publishDir            = Join-Path $workingDir $PublishDirectoryName
$T1TestDir             = Join-Path $publishDir "t1-test";
$coverageDirectory     = Join-Path $T1TestDir "coverage-report"
$coverageDirectoryHtml = Join-Path $coverageDirectory "html"
$ProjectIncludePattern = "*T1.Tests.csproj";
$coverage              = Join-Path $workingDir "/src/HassUtils.T1.Tests/bin/Release/net5.0/HassUtils.T1.Tests.dll"


# ===============================================================================
# region :: Cleanup
# ===============================================================================
$cleanupDirectories = @();
$cleanupDirectories += $publishDir;
$cleanupDirectories += $toolsDir;
 
Foreach ($cleanupDirectory in $cleanupDirectories) {
  if (Test-Path $cleanupDirectory) {
    Remove-item $cleanupDirectory -Recurse -Force | Out-Null;
  }
}


# ===============================================================================
# region :: Required tooling
# ===============================================================================
$installCmd          = "";
$coverletExe         = "$toolsDir\coverlet.exe";
$reportGeneratorExe  = "$toolsDir\reportgenerator.exe"

if(!(Test-Path $coverletExe)){
  Write-Host "Installing: coverlet.console"
  $installCmd = "dotnet tool install coverlet.console --tool-path $toolsDir";
  Invoke-Expression -Command $installCmd -ErrorAction 'Continue';
}

if(!(Test-Path $reportGeneratorExe)){
  Write-Host "Installing: reportgenerator.exe"
  $installCmd = "dotnet tool install dotnet-reportgenerator-globaltool --tool-path `"$toolsDir`"";
  Invoke-Expression -Command $installCmd -ErrorAction 'Continue';
}


# ===============================================================================
# region :: Discover and run tests
# ===============================================================================
$testProjects = Get-ChildItem -Path $sourceDir -Include $ProjectIncludePattern -Recurse -File

if ($testProjects.count -eq 0) {
  throw "No files matched the $ProjectIncludePattern pattern. The script cannot continue."
}

foreach ($testProject in $testProjects) {
  $testResultFileName = Join-Path $T1TestDir "$( $testProject.BaseName )_results.trx"
  $coverageReportOutputPath = Join-Path $coverageDirectory "\"
 
  # The arguments passed to the `dotnet test` command
  # Reference: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test
  $dotnetTestTargetArgs = @(
    "test",
    "$( $testProject.FullName )",
    "--logger:trx;LogFileName=$testResultFileName",
    "--configuration $cfgConfiguration",
    "--no-build", 
    "--no-restore";
  )
 
  # The arguments passed to Coverlet
  # Reference: https://www.jetbrains.com/help/dotcover/dotCover__Console_Runner_Commands.html
  $coverletDotnetArguments = @(
    $coverage,
    "-t `"dotnet`"",
    "-a `"$dotnetTestTargetArgs`"",
    "-o $coverageReportOutputPath",
    "-f `"cobertura`"",
    "-f `"opencover`""
  )
 
  $coverletCmd = "$coverletExe $coverletDotnetArguments";
  Write-Output "Executing Coverlet with following arguments: $coverletCmd";
  Invoke-Expression -Command $coverletCmd;
}
