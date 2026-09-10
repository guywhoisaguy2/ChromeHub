# Chrome Hub - Installation & Build Guide

## Quick Start

### Prerequisites
- Windows 10 or later
- .NET 8.0 Runtime (included in self-contained build)
- Administrator privileges for installation

### Building the Application

#### On Windows:
```bash
.\build.bat
```

#### On Linux/Mac:
```bash
bash build.sh
```

### Installation

1. Navigate to `./publish/ChromeHub-Setup/`
2. Run `ChromeHubInstaller.exe`
3. Follow the installer wizard
4. The application will be installed to `C:\Program Files\ChromeHub\`
5. Shortcuts will be created on Desktop and Start Menu

### Running the Application

After installation, you can launch Chrome Hub by:
- Clicking the Desktop shortcut
- Searching for "Chrome Hub" in Windows Start Menu
- Running `C:\Program Files\ChromeHub\ChromeHub.exe` directly

### Uninstalling

#### Method 1: Via Installer
```bash
ChromeHubInstaller.exe /uninstall
```

#### Method 2: Via Settings
1. Open Settings > Apps > Apps & features
2. Find "Chrome Hub"
3. Click the three dots menu
4. Select "Uninstall"

## Project Structure

```
ChromeHub/
├── ChromeHub.csproj              # Main application project
├── App.xaml(.cs)                 # Application entry point
├── MainWindow.xaml(.cs)          # Main UI window
├── Models/                        # Data models
│   ├── Widget.cs                 # Widget model
│   └── AppSettings.cs            # Settings model
├── ViewModels/                   # MVVM view models
│   ├── MainViewModel.cs
│   ├── WidgetSpaceViewModel.cs
│   ├── BrowseViewModel.cs
│   └── SettingsViewModel.cs
├── Views/                        # UI pages/controls
│   ├── WidgetSpacePage.xaml(.cs)
│   ├── BrowsePage.xaml(.cs)
│   └── SettingsPage.xaml(.cs)
├── Themes/                       # UI themes
│   └── FluentDesign.xaml         # Fluent Design resources
├── Installer/                    # Installer application
│   ├── ChromeHubInstaller.csproj # Installer project
│   ├── InstallationManager.cs    # Installation logic
│   ├── InstallerWindow.xaml(.cs) # Installer UI
│   └── App.xaml(.cs)             # Installer entry point
├── build.bat                     # Windows build script
└── build.sh                      # Unix build script
```

## Features

### 🎨 Widget Space
- 30 customizable widgets (5 default, 25 additional)
- Drag-and-drop repositioning
- Add/remove widgets dynamically
- Beautiful Fluent Design interface

### 🌐 Browse
- Integrated search bar
- Opens in default browser
- Supports direct URLs and search queries

### ⚙️ Settings
- Theme customization (Dark, Light, Blue, Green)
- Animation controls
- Browser preferences
- Notification settings

## Development

### Requirements
- Visual Studio 2022 or later
- .NET 8.0 SDK
- Windows development components

### Opening in Visual Studio

1. Clone the repository
2. Open `ChromeHub.sln` (create if needed by opening the folder)
3. Restore NuGet packages: `dotnet restore`
4. Build the solution: `Ctrl+Shift+B`
5. Run the main app: `F5`

### Building from Command Line

```bash
# Debug build
dotnet build

# Release build
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true

# With installer
.\build.bat
```

## Troubleshooting

### Installation fails with permission error
- Run installer as Administrator
- Ensure you have write permissions to `C:\Program Files\`

### Application won't start after installation
- Try running the installer again (Repair/Reinstall)
- Check that .NET 8.0 Runtime is installed
- Verify installation path: `C:\Program Files\ChromeHub\`

### Shortcuts not appearing
- Run installer with Administrator privileges
- Create shortcuts manually:
  - Right-click Desktop > New > Shortcut
  - Target: `C:\Program Files\ChromeHub\ChromeHub.exe`

## Support

For issues or feature requests, visit the GitHub repository.

## License

Chrome Hub - © 2024
