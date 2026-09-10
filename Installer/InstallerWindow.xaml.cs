using System;
using System.Windows;

namespace ChromeHub.Installer
{
    public partial class InstallerWindow : Window
    {
        private readonly InstallationManager _installationManager;
        private bool _isInstalling = false;
        
        public InstallerWindow()
        {
            InitializeComponent();
            _installationManager = new InstallationManager();
            
            if (_installationManager.IsInstalled())
            {
                InstallButton.Content = "Repair/Reinstall";
            }
        }
        
        private async void OnInstall(object sender, RoutedEventArgs e)
        {
            if (_isInstalling) return;
            
            _isInstalling = true;
            InstallButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    _installationManager.Install(progress =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            StatusText.Text = progress;
                            ProgressText.Text = progress;
                            InstallProgress.Value = Math.Min(InstallProgress.Value + 15, 95);
                        });
                    });
                });
                
                InstallProgress.Value = 100;
                StatusText.Text = "Installation completed successfully!";
                ProgressText.Text = "Chrome Hub has been installed. You can now launch it from your Start Menu or Desktop.";
                InstallButton.Content = "Launch Chrome Hub";
                InstallButton.IsEnabled = true;
                InstallButton.Click -= OnInstall;
                InstallButton.Click += OnLaunchApp;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Installation failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Installation failed";
                ProgressText.Text = ex.Message;
                InstallButton.IsEnabled = true;
                InstallButton.Content = "Retry";
                _isInstalling = false;
            }
        }
        
        private void OnLaunchApp(object sender, RoutedEventArgs e)
        {
            try
            {
                var exePath = System.IO.Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles),
                    "ChromeHub", "ChromeHub.exe");
                
                if (System.IO.File.Exists(exePath))
                {
                    System.Diagnostics.Process.Start(exePath);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Chrome Hub executable not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch application: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (_isInstalling)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Installation in progress. Do you want to cancel?",
                    "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}