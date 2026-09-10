using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChromeHub.Models;
using ChromeHub.ViewModels;

namespace ChromeHub.Views
{
    public partial class WidgetSpacePage : UserControl
    {
        private WidgetSpaceViewModel _viewModel;
        private Widget _draggedWidget;
        private bool _isDragging;
        private Point _dragStart;
        
        public WidgetSpacePage()
        {
            InitializeComponent();
            _viewModel = new WidgetSpaceViewModel();
            this.DataContext = _viewModel;
        }
        
        private void OnAddWidget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var widget = button?.Tag as Widget;
            if (widget != null)
            {
                _viewModel.AddWidget(widget);
            }
        }
        
        private void OnRemoveWidget(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var widget = button?.Tag as Widget;
            if (widget != null)
            {
                _viewModel.RemoveWidget(widget);
            }
        }
        
        private void OnWidgetMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var border = sender as Border;
                var widget = border?.DataContext as Widget;
                if (widget != null)
                {
                    _isDragging = true;
                    _draggedWidget = widget;
                    _dragStart = e.GetPosition(WidgetCanvas);
                    border.CaptureMouse();
                }
            }
        }
        
        private void OnWidgetMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedWidget != null)
            {
                var currentPos = e.GetPosition(WidgetCanvas);
                _draggedWidget.X = currentPos.X - _dragStart.X;
                _draggedWidget.Y = currentPos.Y - _dragStart.Y;
            }
        }
        
        private void OnWidgetMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _draggedWidget = null;
                var border = sender as Border;
                if (border != null)
                {
                    border.ReleaseMouseCapture();
                }
            }
        }
        
        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            // Initial positioning of widgets
            int index = 0;
            foreach (var widget in _viewModel.Widgets)
            {
                if (widget.X == 0 && widget.Y == 0)
                {
                    widget.X = (index % 4) * 220 + 20;
                    widget.Y = (index / 4) * 220 + 20;
                }
                index++;
            }
        }
    }
}