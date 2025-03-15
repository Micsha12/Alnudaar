namespace Alnudaar2.Server.Models
{
    public class AppUsageReport
    {
        public int AppUsageReportID { get; set; } // Primary key
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public DateTime Timestamp { get; set; }
        public string? AppName { get; set; }
        public TimeSpan UsageDuration { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Device? Device { get; set; }
    }
}