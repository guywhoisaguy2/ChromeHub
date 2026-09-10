using System;
using System.Windows;

namespace ChromeHub
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Check if running from installation directory
            var installationPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "ChromeHub");
            
            var appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            
            // If not running from installed location and not first run, suggest installation
            if (!appPath.StartsWith(installationPath, StringComparison.OrdinalIgnoreCase))
            {
                var result = MessageBox.Show(
                    "Chrome Hub is not installed in the standard location.\n\n" +
                    "Would you like to run the installer?",
                    "Chrome Hub",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);
                
                if (result == MessageBoxResult.Yes)
                {
                    // Look for installer in same directory
                    var installerPath = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(appPath) ?? "",
                        "ChromeHubInstaller.exe");
                    
                    if (System.IO.File.Exists(installerPath))
                    {
                        System.Diagnostics.Process.Start(installerPath);
                    }
                }
            }
        }
    }
}