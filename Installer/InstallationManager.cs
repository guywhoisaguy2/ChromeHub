using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ChromeHub.Installer
{
    public class InstallationManager
    {
        private readonly string _appDataPath;
        private readonly string _programFilesPath;
        private readonly string _startMenuPath;
        private readonly string _desktopPath;
        
        public InstallationManager()
        {
            _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChromeHub");
            _programFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "ChromeHub");
            _startMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs", "ChromeHub");
            _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        
        public bool IsInstalled()
        {
            return Directory.Exists(_programFilesPath) && File.Exists(Path.Combine(_programFilesPath, "ChromeHub.exe"));
        }
        
        public void Install(Action<string> progressCallback)
        {
            try
            {
                progressCallback?.Invoke("Creating installation directories...");
                Directory.CreateDirectory(_programFilesPath);
                Directory.CreateDirectory(_appDataPath);
                Directory.CreateDirectory(_startMenuPath);
                
                progressCallback?.Invoke("Copying application files...");
                var currentLocation = Assembly.GetExecutingAssembly().Location;
                var currentDir = Path.GetDirectoryName(currentLocation);
                
                if (currentDir != null)
                {
                    foreach (var file in Directory.GetFiles(currentDir))
                    {
                        var fileName = Path.GetFileName(file);
                        File.Copy(file, Path.Combine(_programFilesPath, fileName), true);
                    }
                    
                    foreach (var dir in Directory.GetDirectories(currentDir))
                    {
                        var dirName = Path.GetFileName(dir);
                        CopyDirectory(dir, Path.Combine(_programFilesPath, dirName ?? ""));
                    }
                }
                
                progressCallback?.Invoke("Creating shortcuts...");
                CreateShortcuts();
                
                progressCallback?.Invoke("Registering application...");
                RegisterApplication();
                
                progressCallback?.Invoke("Installation complete!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Installation failed: {ex.Message}", ex);
            }
        }
        
        public void Uninstall(Action<string> progressCallback)
        {
            try
            {
                progressCallback?.Invoke("Removing shortcuts...");
                RemoveShortcuts();
                
                progressCallback?.Invoke("Unregistering application...");
                UnregisterApplication();
                
                progressCallback?.Invoke("Removing files...");
                if (Directory.Exists(_programFilesPath))
                {
                    Directory.Delete(_programFilesPath, true);
                }
                
                if (Directory.Exists(_startMenuPath))
                {
                    Directory.Delete(_startMenuPath, true);
                }
                
                progressCallback?.Invoke("Uninstallation complete!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Uninstallation failed: {ex.Message}", ex);
            }
        }
        
        private void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                File.Copy(file, Path.Combine(destDir, Path.GetFileName(file)), true);
            }
            
            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                CopyDirectory(dir, Path.Combine(destDir, Path.GetFileName(dir)));
            }
        }
        
        private void CreateShortcuts()
        {
            try
            {
                var exePath = Path.Combine(_programFilesPath, "ChromeHub.exe");
                
                // Start Menu Shortcut
                var startMenuShortcut = Path.Combine(_startMenuPath, "Chrome Hub.lnk");
                CreateShortcutFile(startMenuShortcut, exePath, "The Chrome Hub");
                
                // Desktop Shortcut
                var desktopShortcut = Path.Combine(_desktopPath, "Chrome Hub.lnk");
                CreateShortcutFile(desktopShortcut, exePath, "The Chrome Hub");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Warning: Could not create shortcuts. {ex.Message}", "Installation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void CreateShortcutFile(string shortcutPath, string targetPath, string description)
        {
            var shell = new IWshRuntimeLibrary.WshShellClass();
            var shortcut = shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath) ?? "";
            shortcut.Description = description;
            shortcut.Save();
        }
        
        private void RemoveShortcuts()
        {
            var startMenuShortcut = Path.Combine(_startMenuPath, "Chrome Hub.lnk");
            var desktopShortcut = Path.Combine(_desktopPath, "Chrome Hub.lnk");
            
            if (File.Exists(startMenuShortcut))
                File.Delete(startMenuShortcut);
            
            if (File.Exists(desktopShortcut))
                File.Delete(desktopShortcut);
        }
        
        private void RegisterApplication()
        {
            try
            {
                // Register in Programs and Features
                using (var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ChromeHub"))
                {
                    if (key != null)
                    {
                        key.SetValue("DisplayName", "Chrome Hub");
                        key.SetValue("DisplayVersion", "1.0.0");
                        key.SetValue("Publisher", "Chrome Hub");
                        key.SetValue("InstallLocation", _programFilesPath);
                        key.SetValue("UninstallString", Path.Combine(_programFilesPath, "ChromeHubInstaller.exe") + " /uninstall");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Warning: Could not register application. {ex.Message}", "Installation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void UnregisterApplication()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ChromeHub", true))
                {
                    if (key != null)
                    {
                        Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ChromeHub");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Warning: Could not unregister application. {ex.Message}", "Uninstallation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}