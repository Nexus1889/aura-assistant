$ErrorActionPreference = "Stop"

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  AURA Assistant Build & Setup" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$publishDir = Join-Path $root "publish"
$installerDir = Join-Path $root "installer"
$outputDir = Join-Path $installerDir "output"
$issFile = Join-Path $installerDir "AURA-Setup.iss"

Write-Host "[STEP 1] Building AURA.App..." -ForegroundColor Yellow
Write-Host ""
& (Join-Path $root "build.ps1")
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "`n[STEP 2] Checking for compiled executable..." -ForegroundColor Yellow
if (-not (Test-Path (Join-Path $publishDir "AURA Assistant.exe"))) {
    Write-Error "AURA Assistant.exe not found in $publishDir."
    exit 1
}
Write-Host "[OK] AURA Assistant.exe found." -ForegroundColor Green

Write-Host "`n[STEP 3] Building installer with Inno Setup..." -ForegroundColor Yellow
Write-Host ""

if (-not (Test-Path $installerDir)) { New-Item -ItemType Directory -Path $installerDir | Out-Null }
if (-not (Test-Path $outputDir)) { New-Item -ItemType Directory -Path $outputDir | Out-Null }

$iscc = Get-Command iscc -ErrorAction SilentlyContinue
if (-not $iscc) {
    Write-Error "Inno Setup Compiler (iscc.exe) not found. Install it from: https://jrsoftware.org/isinfo.php"
    exit 1
}

& $iscc.Path "/O$outputDir" $issFile
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "`n========================================" -ForegroundColor Green
Write-Host "  BUILD COMPLETE!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "`nExecutable: $publishDir\AURA Assistant.exe" -ForegroundColor Cyan
Write-Host "Installer:  $outputDir\AURA-Assistant-Setup-x64.exe`n" -ForegroundColor Cyan
