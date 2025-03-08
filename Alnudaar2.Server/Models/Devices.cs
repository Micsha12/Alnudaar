namespace Alnudaar2.Server.Models
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string? Name { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
        public ICollection<AppUsageReport>? AppUsageReports { get; set; }
        public ICollection<ScreenTimeSchedule>? ScreenTimeSchedules { get; set; }
    }
}