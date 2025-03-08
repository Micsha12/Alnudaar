namespace Alnudaar2.Server.Models
{
    public class AppUsageReport
    {
        public int ReportID { get; set; }
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public string? AppName { get; set; }
        public int UsageDuration { get; set; }
        public string? ReportDate { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Device? Device { get; set; }
    }
}