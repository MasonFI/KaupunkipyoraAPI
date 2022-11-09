using KaupunkipyoraAPI.Contracts;

namespace KaupunkipyoraAPI.Models.DTO
{
    public class BikeRouteDTO : IEntityDTO
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
    }
}
