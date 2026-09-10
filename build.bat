@echo off
REM Build script for Chrome Hub

echo ========================================
echo Chrome Hub - Build Script
echo ========================================
echo.

echo [1/3] Building Main Application...
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o "./publish/app"
if %ERRORLEVEL% neq 0 (
    echo Failed to build main application
    exit /b 1
)
echo Done!
echo.

echo [2/3] Building Installer...
cd Installer
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o "../publish/installer"
if %ERRORLEVEL% neq 0 (
    echo Failed to build installer
    cd ..
    exit /b 1
)
cd ..
echo Done!
echo.

echo [3/3] Creating installer package...
if not exist "publish\ChromeHub-Setup" mkdir "publish\ChromeHub-Setup"
xcopy "publish\app\*" "publish\ChromeHub-Setup\" /E /I /Y >nul
copy "publish\installer\ChromeHubInstaller.exe" "publish\ChromeHub-Setup\" /Y >nul
echo Done!
echo.

echo ========================================
echo Build Complete!
echo ========================================
echo.
echo Output Location: .\publish\ChromeHub-Setup\
echo Installer: ChromeHubInstaller.exe
echo.
echo To install, run: ChromeHubInstaller.exe
echo To uninstall, run: ChromeHubInstaller.exe /uninstall
echo.
pause