using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChromeHub.ViewModels
{
    public class BrowseViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;
        private string _browserUrl;
        
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string BrowserUrl
        {
            get => _browserUrl;
            set
            {
                if (_browserUrl != value)
                {
                    _browserUrl = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public void OpenInBrowser()
        {
            try
            {
                string url = SearchQuery;
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = $"https://www.google.com/search?q={Uri.EscapeDataString(SearchQuery)}";
                }
                
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error opening browser: {ex.Message}");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}