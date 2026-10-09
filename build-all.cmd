@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================
echo   AURA Assistant Build & Setup
echo ========================================
echo.

set "ROOT=%~dp0"
set "PUBLISH_DIR=%ROOT%publish"
set "INSTALLER_DIR=%ROOT%installer"
set "OUTPUT_DIR=%INSTALLER_DIR%\output"

echo [STEP 1] Building AURA.App...
echo.
call "%ROOT%build.cmd"
if errorlevel 1 (
    echo [ERROR] Build failed.
    exit /b 1
)

echo.
echo [STEP 2] Checking for compiled executable...
if not exist "%PUBLISH_DIR%\AURA Assistant.exe" (
    echo [ERROR] AURA Assistant.exe not found in %PUBLISH_DIR%.
    exit /b 1
)
echo [OK] AURA Assistant.exe found.

echo.
echo [STEP 3] Building installer with Inno Setup...
echo.

if not exist "%INSTALLER_DIR%" mkdir "%INSTALLER_DIR%"
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

where iscc >nul 2>nul
if errorlevel 1 (
    echo [ERROR] Inno Setup Compiler (iscc.exe) not found.
    echo Install Inno Setup from: https://jrsoftware.org/isinfo.php
    echo Make sure to add iscc.exe to your PATH.
    exit /b 1
)

iscc.exe /O"%OUTPUT_DIR%" "%INSTALLER_DIR%\AURA-Setup.iss"
if errorlevel 1 (
    echo [ERROR] Installer build failed.
    exit /b 1
)

echo.
echo ========================================
echo   BUILD COMPLETE!
echo ========================================
echo.
echo Executable: %PUBLISH_DIR%\AURA Assistant.exe
echo Installer:  %OUTPUT_DIR%\AURA-Assistant-Setup-x64.exe
echo.
echo.
pause
