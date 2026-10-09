$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$publishDir = Join-Path $root "publish"
$installerDir = Join-Path $root "installer"
$outputDir = Join-Path $installerDir "output"
$issFile = Join-Path $installerDir "AURA-Setup.iss"

if (-not (Test-Path (Join-Path $publishDir "AURA Assistant.exe"))) {
    Write-Error "AURA Assistant.exe not found in $publishDir. Run build.ps1 first."
    exit 1
}

if (-not (Test-Path $installerDir)) { New-Item -ItemType Directory -Path $installerDir | Out-Null }
if (-not (Test-Path $outputDir)) { New-Item -ItemType Directory -Path $outputDir | Out-Null }

$iscc = Get-Command iscc -ErrorAction SilentlyContinue
if (-not $iscc) {
    Write-Error "Inno Setup Compiler (iscc.exe) was not found. Install Inno Setup and add it to PATH."
    exit 1
}

& $iscc.Path "/O$outputDir" $issFile
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Installer build finished successfully." -ForegroundColor Green
Write-Host "Output folder: $outputDir" -ForegroundColor Green
