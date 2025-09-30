@echo off

NET SESSION >nul 2>&1
if %errorLevel% == 0 (
    "%~dp0\EasyAntiCheat\EasyAntiCheat_EOS_Setup.exe" install 14ddf52f6a2f9c
) else (
    echo This script requires administrator privileges.
    echo Please run it as an administrator.
    pause
    exit /b
)