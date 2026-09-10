namespace ChromeHub.Models
{
    public class AppSettings
    {
        public string Theme { get; set; } = "Dark";
        public bool EnableAnimations { get; set; } = true;
        public double AnimationSpeed { get; set; } = 1.0;
        public bool EnableNotifications { get; set; } = true;
        public string DefaultBrowser { get; set; } = "Default";
        public bool AutoArrangeWidgets { get; set; } = false;
    }
}