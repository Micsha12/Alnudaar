namespace Alnudaar2.Server.Models
{
    public class ActivityLog
    {
        public int ActivityLogID { get; set; } // Primary key
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Activity { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Device? Device { get; set; }
    }
}