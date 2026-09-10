using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using ChromeHub.Models;

namespace ChromeHub.ViewModels
{
    public class WidgetSpaceViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Widget> _widgets;
        private ObservableCollection<Widget> _availableWidgets;
        private Widget _selectedWidget;
        
        public ObservableCollection<Widget> Widgets
        {
            get => _widgets;
            set
            {
                if (_widgets != value)
                {
                    _widgets = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ObservableCollection<Widget> AvailableWidgets
        {
            get => _availableWidgets;
            set
            {
                if (_availableWidgets != value)
                {
                    _availableWidgets = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public Widget SelectedWidget
        {
            get => _selectedWidget;
            set
            {
                if (_selectedWidget != value)
                {
                    _selectedWidget = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public WidgetSpaceViewModel()
        {
            InitializeWidgets();
        }
        
        private void InitializeWidgets()
        {
            Widgets = new ObservableCollection<Widget>();
            AvailableWidgets = new ObservableCollection<Widget>();
            
            // Default widgets (5)
            var defaultWidgets = new[]
            {
                new Widget { Id = 1, Name = "Clock", Description = "Digital Clock", Icon = "🕐", Type = WidgetType.Clock, AddedDate = DateTime.Now },
                new Widget { Id = 2, Name = "Calendar", Description = "Calendar View", Icon = "📅", Type = WidgetType.Calendar, AddedDate = DateTime.Now },
                new Widget { Id = 3, Name = "Notes", Description = "Quick Notes", Icon = "📝", Type = WidgetType.Notes, AddedDate = DateTime.Now },
                new Widget { Id = 4, Name = "Weather", Description = "Weather Info", Icon = "🌤️", Type = WidgetType.Weather, AddedDate = DateTime.Now },
                new Widget { Id = 5, Name = "Todo", Description = "To-Do List", Icon = "✓", Type = WidgetType.Todo, AddedDate = DateTime.Now }
            };
            
            foreach (var widget in defaultWidgets)
            {
                widget.IsActive = true;
                Widgets.Add(widget);
            }
            
            // Available widgets to add (25 more)
            var availableWidgets = new[]
            {
                new Widget { Id = 6, Name = "News Feed", Description = "Latest News", Icon = "📰", Type = WidgetType.News },
                new Widget { Id = 7, Name = "Stock Ticker", Description = "Stock Prices", Icon = "📈", Type = WidgetType.Stocks },
                new Widget { Id = 8, Name = "Music Player", Description = "Play Music", Icon = "🎵", Type = WidgetType.Music },
                new Widget { Id = 9, Name = "Health Tracker", Description = "Track Health", Icon = "❤️", Type = WidgetType.Health },
                new Widget { Id = 10, Name = "Productivity", Description = "Productivity Stats", Icon = "⚡", Type = WidgetType.Productivity },
                new Widget { Id = 11, Name = "Analytics", Description = "Data Analytics", Icon = "📊", Type = WidgetType.Analytics },
                new Widget { Id = 12, Name = "Timer", Description = "Timer/Stopwatch", Icon = "⏱️", Type = WidgetType.Custom },
                new Widget { Id = 13, Name = "Email", Description = "Email Client", Icon = "📧", Type = WidgetType.Custom },
                new Widget { Id = 14, Name = "Tasks", Description = "Task Manager", Icon = "📋", Type = WidgetType.Custom },
                new Widget { Id = 15, Name = "Reminders", Description = "Set Reminders", Icon = "🔔", Type = WidgetType.Custom },
                new Widget { Id = 16, Name = "Calculator", Description = "Quick Calc", Icon = "🧮", Type = WidgetType.Custom },
                new Widget { Id = 17, Name = "Dictionary", Description = "Word Lookup", Icon = "📖", Type = WidgetType.Custom },
                new Widget { Id = 18, Name = "Converter", Description = "Unit Converter", Icon = "🔄", Type = WidgetType.Custom },
                new Widget { Id = 19, Name = "Quotes", Description = "Daily Quotes", Icon = "💡", Type = WidgetType.Custom },
                new Widget { Id = 20, Name = "Games", Description = "Mini Games", Icon = "🎮", Type = WidgetType.Custom },
                new Widget { Id = 21, Name = "Social", Description = "Social Feed", Icon = "👥", Type = WidgetType.Custom },
                new Widget { Id = 22, Name = "Crypto", Description = "Crypto Prices", Icon = "₿", Type = WidgetType.Custom },
                new Widget { Id = 23, Name = "Maps", Description = "Quick Maps", Icon = "🗺️", Type = WidgetType.Custom },
                new Widget { Id = 24, Name = "Translator", Description = "Language Translate", Icon = "🌐", Type = WidgetType.Custom },
                new Widget { Id = 25, Name = "Podcast", Description = "Podcast Player", Icon = "🎙️", Type = WidgetType.Custom },
                new Widget { Id = 26, Name = "Video", Description = "Video Player", Icon = "🎬", Type = WidgetType.Custom },
                new Widget { Id = 27, Name = "Files", Description = "File Manager", Icon = "📁", Type = WidgetType.Custom },
                new Widget { Id = 28, Name = "Code", Description = "Code Snippets", Icon = "💻", Type = WidgetType.Custom },
                new Widget { Id = 29, Name = "Drawing", Description = "Drawing Board", Icon = "🎨", Type = WidgetType.Custom },
                new Widget { Id = 30, Name = "Metrics", Description = "System Metrics", Icon = "⚙️", Type = WidgetType.Custom }
            };
            
            foreach (var widget in availableWidgets)
            {
                AvailableWidgets.Add(widget);
            }
        }
        
        public void AddWidget(Widget widget)
        {
            if (AvailableWidgets.Contains(widget))
            {
                AvailableWidgets.Remove(widget);
                widget.IsActive = true;
                widget.X = Widgets.Count * 210 % 800;
                widget.Y = (Widgets.Count / 4) * 210;
                Widgets.Add(widget);
            }
        }
        
        public void RemoveWidget(Widget widget)
        {
            if (Widgets.Contains(widget))
            {
                Widgets.Remove(widget);
                widget.IsActive = false;
                AvailableWidgets.Add(widget);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}