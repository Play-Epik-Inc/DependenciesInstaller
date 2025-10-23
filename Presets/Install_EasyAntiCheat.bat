@echo off

NET SESSION >nul 2>&1
if %errorLevel% == 0 (
    "%~dp0\EasyAntiCheat\EasyAntiCheat_EOS_Setup.exe" install bca5888500f422
) else (
    echo This script requires administrator privileges.
    echo Please run it as an administrator.
    pause
    exit /b
)