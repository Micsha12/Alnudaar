namespace Alnudaar2.Server.Models
{
    public class Geofencing
    {
        public int GeofencingID { get; set; }
        public int UserID { get; set; }
        public string? SafeZoneName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Radius { get; set; } // in meters

        // Navigation property
        public User? User { get; set; }
    }
}