using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChromeHub.Models;

namespace ChromeHub.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private AppSettings _settings;
        private ObservableCollection<string> _themes;
        private string _selectedTheme;
        private ObservableCollection<string> _browsers;
        private string _selectedBrowser;
        
        public AppSettings Settings
        {
            get => _settings;
            set
            {
                if (_settings != value)
                {
                    _settings = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<string> Themes
        {
            get => _themes;
            set
            {
                if (_themes != value)
                {
                    _themes = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<string> Browsers
        {
            get => _browsers;
            set
            {
                if (_browsers != value)
                {
                    _browsers = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string SelectedBrowser
        {
            get => _selectedBrowser;
            set
            {
                if (_selectedBrowser != value)
                {
                    _selectedBrowser = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public SettingsViewModel()
        {
            Settings = new AppSettings();
            InitializeThemes();
            InitializeBrowsers();
        }
        
        private void InitializeThemes()
        {
            Themes = new ObservableCollection<string> { "Dark", "Light", "Blue", "Green" };
            SelectedTheme = Settings.Theme;
        }
        
        private void InitializeBrowsers()
        {
            Browsers = new ObservableCollection<string> { "Default", "Chrome", "Firefox", "Edge" };
            SelectedBrowser = Settings.DefaultBrowser;
        }
        
        public void SaveSettings()
        {
            Settings.Theme = SelectedTheme;
            Settings.DefaultBrowser = SelectedBrowser;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}