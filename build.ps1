$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$solution = Join-Path $repoRoot "AURA.sln"
$appProject = Join-Path $repoRoot "AURA.App\AURA.App.csproj"
$publishDir = Join-Path $repoRoot "publish"

Write-Host "[1/3] Restoring NuGet packages..." -ForegroundColor Cyan
& dotnet restore $solution
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "[2/3] Building solution in Release mode..." -ForegroundColor Cyan
& dotnet build $solution -c Release --nologo
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "[3/3] Publishing AURA.App for win-x64..." -ForegroundColor Cyan
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }
& dotnet publish $appProject -c Release -r win-x64 --self-contained false -o $publishDir --nologo
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Build finished successfully." -ForegroundColor Green
Write-Host "Publish output: $publishDir" -ForegroundColor Green
