@echo off

NET SESSION >nul 2>&1
if %errorLevel% == 0 (
    "%~dp0\EasyAntiCheat\EasyAntiCheat_EOS_Setup.exe" install 886796dcf0f723 REM substitute this number with the current activated-build on dev.epicgames.com
) else (
    echo This script requires administrator privileges.
    echo Please run it as an administrator.
    pause
    exit /b
)