using System.Windows;

namespace ChromeHub.Installer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Check for command line arguments
            if (e.Args.Length > 0 && e.Args[0].ToLower() == "/uninstall")
            {
                // Handle uninstallation
                var manager = new InstallationManager();
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to uninstall Chrome Hub?",
                    "Uninstall Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        manager.Uninstall(progress =>
                        {
                            // Log progress if needed
                        });
                        
                        MessageBox.Show("Chrome Hub has been uninstalled successfully.", "Uninstall Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Uninstallation failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                
                this.Shutdown();
            }
        }
    }
}