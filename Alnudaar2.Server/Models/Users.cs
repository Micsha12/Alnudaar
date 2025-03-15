using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alnudaar2.Server.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? UserType { get; set; }
        public string? Email { get; set; }

        [NotMapped]
        public string? Password { get; set; } // Plain text password, not mapped to the database

        // Navigation properties
        public ICollection<Geofencing>? Geofences { get; set; }
        public ICollection<ScreenTimeSchedule>? ScreenTimeSchedules { get; set; }
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
        public ICollection<AppUsageReport>? AppUsageReports { get; set; }
        public ICollection<Device>? Devices { get; set; }
    }
}