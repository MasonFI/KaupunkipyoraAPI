using KaupunkipyoraAPI.Contracts;

namespace KaupunkipyoraAPI.Models.Entity
{
    public class BikeRoute : IEntity
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public int DepartureStationId { get; set; }
        public string DepartureStationName { get; set; } = String.Empty;
        public int ReturnStationId { get; set; }
        public string ReturnStationName { get; set; } = String.Empty;
        public int CoveredDistanceInMeters { get; set; }
        public int DurationInSeconds { get; set; }

        public DateTime Created { get; set; }
        public int CreatedById { get; set; }
        public DateTime? Updated { get; set; }
        public int? UpdatedById { get; set; }
        public User CreatedBy { get; set; } = default!;
        public User? UpdatedBy { get; set; }
    }
}
