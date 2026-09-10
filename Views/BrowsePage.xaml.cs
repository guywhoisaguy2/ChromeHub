using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChromeHub.ViewModels;

namespace ChromeHub.Views
{
    public partial class BrowsePage : UserControl
    {
        private BrowseViewModel _viewModel;
        
        public BrowsePage()
        {
            InitializeComponent();
            _viewModel = new BrowseViewModel();
            this.DataContext = _viewModel;
        }
        
        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenInBrowser();
        }
        
        private void OnSearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _viewModel.OpenInBrowser();
                e.Handled = true;
            }
        }
    }
}