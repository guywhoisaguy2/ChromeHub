#!/bin/bash
# Build script for Chrome Hub

echo "========================================"
echo "Chrome Hub - Build Script"
echo "========================================"
echo ""

# Colors
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo -e "${BLUE}Step 1: Building Main Application...${NC}"
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o ./publish/app
if [ $? -ne 0 ]; then
    echo "❌ Failed to build main application"
    exit 1
fi
echo -e "${GREEN}✓ Main application built successfully${NC}"
echo ""

echo -e "${BLUE}Step 2: Building Installer...${NC}"
cd Installer
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o ../publish/installer
if [ $? -ne 0 ]; then
    echo "❌ Failed to build installer"
    exit 1
fi
cd ..
echo -e "${GREEN}✓ Installer built successfully${NC}"
echo ""

echo -e "${BLUE}Step 3: Creating installer package...${NC}"
mkdir -p ./publish/ChromeHub-Setup
cp ./publish/app/* ./publish/ChromeHub-Setup/
cp ./publish/installer/ChromeHubInstaller.exe ./publish/ChromeHub-Setup/
echo -e "${GREEN}✓ Installer package created${NC}"
echo ""

echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}Build Complete!${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""
echo "Output Location: ./publish/ChromeHub-Setup/"
echo "Installer: ChromeHubInstaller.exe"
echo ""
echo "To install, run: ChromeHubInstaller.exe"
echo "To uninstall, run: ChromeHubInstaller.exe /uninstall"
echo ""
