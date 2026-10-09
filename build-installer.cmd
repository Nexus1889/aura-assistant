@echo off
setlocal enabledelayedexpansion

set "ROOT=%~dp0"
set "PUBLISH_DIR=%ROOT%publish"
set "INSTALLER_DIR=%ROOT%installer"
set "OUTPUT_DIR=%INSTALLER_DIR%\output"

if not exist "%PUBLISH_DIR%\AURA Assistant.exe" (
    echo [ERROR] AURA Assistant.exe was not found in %PUBLISH_DIR%.
    echo Please run build.cmd first.
    exit /b 1
)

if not exist "%INSTALLER_DIR%" mkdir "%INSTALLER_DIR%"
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

where iscc >nul 2>nul
if errorlevel 1 (
    echo [ERROR] Inno Setup Compiler (iscc.exe) was not found.
    echo Install Inno Setup and ensure iscc.exe is on PATH.
    echo Download: https://jrsoftware.org/isinfo.php
    exit /b 1
)

iscc.exe /O"%OUTPUT_DIR%" "%INSTALLER_DIR%\AURA-Setup.iss"
if errorlevel 1 exit /b %errorlevel%

echo.
echo Installer build finished successfully.
echo Output folder: %OUTPUT_DIR%
