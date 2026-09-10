using System.Windows;
using ChromeHub.ViewModels;
using ChromeHub.Views;

namespace ChromeHub
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
        }
        
        private void OnWidgetSpaceClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NavigateToWidgetSpace();
        }
        
        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NavigateToBrowse();
        }
        
        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            _viewModel.NavigateToSettings();
        }
    }
}