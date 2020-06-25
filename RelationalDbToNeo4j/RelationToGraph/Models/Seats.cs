namespace RelationToGraph.Models
{
    public partial class Seats
    {
        public int SeatId { get; set; }
        public string SeatNum { get; set; }
        public string SeatType { get; set; }
        public int? Aircraft { get; set; }

        public Aircrafts AircraftNavigation { get; set; }
    }
}
