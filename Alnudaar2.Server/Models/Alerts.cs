namespace Alnudaar2.Server.Models
{
    public class Alert
    {
        public int AlertID { get; set; }
        public int UserID { get; set; }
        public int DeviceID { get; set; }
        public string? AlertType { get; set; }
        public DateTime AlertTime { get; set; }
        public string? Details { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Device? Device { get; set; }
    }
}