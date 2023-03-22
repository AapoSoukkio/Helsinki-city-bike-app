namespace Solita.HelsinkiBikeApp.Shared
{
    public class Summer21Journey
    {
        public int ID { get; set; }
        public string? Departure { get; set; }
        public string? Return { get; set; }
        public int? DepartureStationId { get; set; }
        public string? DepartureStationName { get; set; }
        public int? ReturnStationId { get; set; }
        public string? ReturnStationName { get; set; }
        public int? CoveredDistance { get; set; }
        public int? Duration { get; set; }
    }
}
