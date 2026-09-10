using System.Windows;
using System.Windows.Controls;
using ChromeHub.ViewModels;

namespace ChromeHub.Views
{
    public partial class SettingsPage : UserControl
    {
        private SettingsViewModel _viewModel;
        
        public SettingsPage()
        {
            InitializeComponent();
            _viewModel = new SettingsViewModel();
            this.DataContext = _viewModel;
        }
        
        private void OnSaveSettings(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveSettings();
            MessageBox.Show("Settings saved successfully!", "Chrome Hub", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}