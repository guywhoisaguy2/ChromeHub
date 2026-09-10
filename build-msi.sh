#!/bin/bash
# Chrome Hub MSI Build Script

echo "========================================"
echo "Chrome Hub - MSI Build System"
echo "========================================"
echo ""

# Colors
GREEN='\033[0;32m'
BLUE='\033[0;34m'
RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${BLUE}Step 1: Building Main Application...${NC}"
dotnet publish ChromeHub.csproj -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true -o "./publish/app"
if [ $? -ne 0 ]; then
    echo -e "${RED}✗ Failed to build main application${NC}"
    exit 1
fi
echo -e "${GREEN}✓ Main application built successfully${NC}"
echo ""

echo -e "${BLUE}Step 2: Checking for WiX Toolset...${NC}"
wix --version >/dev/null 2>&1
if [ $? -ne 0 ]; then
    echo -e "${RED}✗ WiX Toolset not found${NC}"
    echo "Please visit: https://github.com/wixtoolset/wix4/releases"
    echo "Download and install WiX Toolset v4.0 or later"
    echo "Then run this script again"
    exit 1
fi
echo -e "${GREEN}✓ WiX Toolset found$(wix --version)${NC}"
echo ""

echo -e "${BLUE}Step 3: Building MSI Installer...${NC}"
cd Installer
wix build -o ../publish/ChromeHub.msi Product.wxs -d PublishDir=../publish/app/ -b ../publish/app/
if [ $? -ne 0 ]; then
    echo -e "${RED}✗ Failed to build MSI${NC}"
    cd ..
    exit 1
fi
cd ..
echo -e "${GREEN}✓ MSI built successfully${NC}"
echo ""

echo -e "${BLUE}Step 4: Creating distribution package...${NC}"
mkdir -p ./publish/Distribution
cp ./publish/ChromeHub.msi ./publish/Distribution/ChromeHub-Setup.msi
echo -e "${GREEN}✓ Distribution package created${NC}"
echo ""

echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}Build Complete!${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""
echo "MSI Location: ./publish/Distribution/ChromeHub-Setup.msi"
echo ""
echo "To install:"
echo "  ./publish/Distribution/ChromeHub-Setup.msi"
echo ""
echo "To uninstall:"
echo "  Control Panel > Programs > Programs and Features"
echo ""
