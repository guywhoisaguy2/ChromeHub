@echo off
REM Chrome Hub MSI Build Script

echo ========================================
echo Chrome Hub - MSI Build System
echo ========================================
echo.

echo [1/4] Building Main Application...
dotnet publish ChromeHub.csproj -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o "./publish/app"
if %ERRORLEVEL% neq 0 (
    echo Failed to build main application
    exit /b 1
)
echo Done!
echo.

echo [2/4] Checking for WiX Toolset...
wix --version >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo WiX Toolset not found. Installing...
    echo Please visit: https://github.com/wixtoolset/wix4/releases
    echo Download and install WiX Toolset v4.0 or later
    echo Then run this script again
    pause
    exit /b 1
)
echo WiX Toolset found!
echo.

echo [3/4] Building MSI Installer...
cd Installer
wix build -o ..\publish\ChromeHub.msi Product.wxs -d PublishDir=..\publish\app\ -b ..\publish\app\
if %ERRORLEVEL% neq 0 (
    echo Failed to build MSI
    cd ..
    exit /b 1
)
cd ..
echo Done!
echo.

echo [4/4] Creating distribution package...
if not exist "publish\Distribution" mkdir "publish\Distribution"
copy "publish\ChromeHub.msi" "publish\Distribution\ChromeHub-Setup.msi" /Y >nul
echo Done!
echo.

echo ========================================
echo Build Complete!
echo ========================================
echo.
echo MSI Location: .\publish\Distribution\ChromeHub-Setup.msi
echo.
echo To install:
    echo   Right-click ChromeHub-Setup.msi and select "Install"
    echo   OR
    echo   msiexec /i ChromeHub-Setup.msi
echo.
echo To uninstall:
    echo   Control Panel ^> Programs ^> Programs and Features
    echo   OR
    echo   msiexec /x ChromeHub-Setup.msi
echo.
pause