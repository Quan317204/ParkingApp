using SQLite;
using System;

namespace ParkingApp.Models
{
    public class VehicleEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string ParkingSpot { get; set; } = string.Empty;
        public DateTime EntryTime { get; set; } = DateTime.Now;
        public DateTime? ExitTime { get; set; }
        public double Fee { get; set; }
    }
}
