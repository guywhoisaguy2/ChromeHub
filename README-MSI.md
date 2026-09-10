# Chrome Hub - MSI Installer Setup Guide

## Prerequisites

### Required Software
- **Windows 10 or later**
- **.NET 8.0 SDK** (for building)
- **WiX Toolset v4.0 or later** (for creating MSI)
- **Administrator privileges** (for installation)

### Installing WiX Toolset

1. Visit: https://github.com/wixtoolset/wix4/releases
2. Download the latest WiX Toolset v4.0 installer
3. Run the installer and follow the wizard
4. Restart your computer if prompted
5. Open a new Command Prompt/PowerShell to verify:
   ```bash
   wix --version
   ```

## Building the MSI Installer

### On Windows:
```bash
.\build-msi.bat
```

### On Linux/Mac (requires WiX via Wine or similar):
```bash
bash build-msi.sh
```

## Installation

### Method 1: GUI Installation
1. Navigate to `./publish/Distribution/`
2. Double-click `ChromeHub-Setup.msi`
3. Follow the installer wizard
4. Click "Finish" to complete installation

### Method 2: Command Line Installation
```bash
# Standard installation
msiexec /i ChromeHub-Setup.msi

# Silent installation (no UI)
msiexec /i ChromeHub-Setup.msi /quiet

# Installation with logging
msiexec /i ChromeHub-Setup.msi /l*v install.log
```

## Uninstallation

### Method 1: Via Windows Settings
1. Open `Settings` > `Apps` > `Apps & features`
2. Find "Chrome Hub"
3. Click it and select "Uninstall"

### Method 2: Control Panel
1. Open `Control Panel` > `Programs` > `Programs and Features`
2. Find "Chrome Hub"
3. Click "Uninstall"

### Method 3: Command Line
```bash
# Standard uninstallation
msiexec /x ChromeHub-Setup.msi

# Silent uninstallation
msiexec /x ChromeHub-Setup.msi /quiet

# Uninstall using Product Code
msiexec /x {PRODUCT-CODE} /quiet
```

## Installation Locations

- **Application**: `C:\Program Files\Chrome Hub\`
- **Shortcuts**: 
  - Desktop: `%UserProfile%\Desktop\Chrome Hub.lnk`
  - Start Menu: `%AppData%\Microsoft\Windows\Start Menu\Programs\Chrome Hub\`
- **Registry**: `HKCU\Software\Chrome Hub`

## Build Output

After successful build:
```
publish/
├── Distribution/
│   └── ChromeHub-Setup.msi          (Ready to distribute)
├── app/
│   ├── ChromeHub.exe                (Main application)
│   └── [All dependencies]
└── ChromeHub.msi                    (Raw MSI file)
```

## Advanced MSI Options

### Silent Installation with Custom Path
```bash
msiexec /i ChromeHub-Setup.msi /quiet INSTALLFOLDER="C:\CustomPath\"
```

### Repair Installation
```bash
msiexec /f ChromeHub-Setup.msi
```

### Installation Log
```bash
msiexec /i ChromeHub-Setup.msi /l*v debug.log
```

## Troubleshooting

### Error: "WiX Toolset not found"
- Install WiX Toolset v4.0 from: https://github.com/wixtoolset/wix4/releases
- Restart your computer after installation
- Verify with: `wix --version`

### Error: "MSI installation failed"
- Run Command Prompt as Administrator
- Check installation logs: `msiexec /i ChromeHub-Setup.msi /l*v install.log`
- Ensure .NET 8.0 Runtime is installed
- Check disk space (minimum 500MB recommended)

### Application won't start after installation
- Verify installation: `C:\Program Files\Chrome Hub\ChromeHub.exe` exists
- Check .NET 8.0 Runtime installation
- Try repair installation: `msiexec /f ChromeHub-Setup.msi`

### Uninstall issues
- Use "Repair" first to fix corrupted installation
- Clear registry: `regedit` > `HKCU\Software\Chrome Hub` > Delete
- Manual removal: Delete `C:\Program Files\Chrome Hub\` folder

## Distribution

### Creating Setup Packages
```bash
# Copy MSI to distribution location
copy "publish\Distribution\ChromeHub-Setup.msi" "D:\MyDistribution\"

# Create a self-extracting archive (optional)
# Use WinRAR, 7-Zip, or similar to create a .7z or .zip with the MSI
```

### Hosting Online
1. Upload `ChromeHub-Setup.msi` to your server
2. Create a download page with:
   - System requirements
   - Installation instructions
   - Support information
3. Users can download and run the MSI directly

## System Requirements

- **OS**: Windows 10 or later
- **RAM**: 2GB minimum (4GB recommended)
- **Disk Space**: 500MB minimum
- **.NET Runtime**: 8.0 or later (self-contained)
- **Administrator Rights**: Required for installation

## Support

For issues or questions:
- Check the GitHub repository
- Review installation logs
- Consult WiX Toolset documentation: https://wixtoolset.org/docs/

## License

Chrome Hub - © 2024
