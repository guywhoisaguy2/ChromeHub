using System.Windows;
using ChromeHub.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChromeHub.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentPage;
        
        public UserControl CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public MainViewModel()
        {
            NavigateToWidgetSpace();
        }
        
        public void NavigateToWidgetSpace()
        {
            CurrentPage = new WidgetSpacePage();
        }
        
        public void NavigateToBrowse()
        {
            CurrentPage = new BrowsePage();
        }
        
        public void NavigateToSettings()
        {
            CurrentPage = new SettingsPage();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}