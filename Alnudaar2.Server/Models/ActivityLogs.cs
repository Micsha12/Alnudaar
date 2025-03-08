namespace Alnudaar2.Server.Models
{
    public class ActivityLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public string? ActivityType { get; set; }
        public DateTime ActivityTime { get; set; }
        public string? Details { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Device? Device { get; set; }
    }
}