using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace ChromeHub.Models
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public WidgetType Type { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 200;
        public bool IsActive { get; set; }
        public DateTime AddedDate { get; set; }
    }
    
    public enum WidgetType
    {
        Clock,
        Weather,
        Todo,
        Notes,
        Calendar,
        News,
        Stocks,
        Music,
        Health,
        Productivity,
        Analytics,
        Custom
    }
}