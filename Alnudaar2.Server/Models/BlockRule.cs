namespace Alnudaar2.Server.Models
{
    public class BlockRule
    {
        public int BlockRuleID { get; set; }
        public int UserID { get; set; }
        public string Type { get; set; } // "website" or "application"
        public string Value { get; set; } // Website URL or application name
        public string TimeRange { get; set; } // e.g., "09:00-17:00"
    }
}