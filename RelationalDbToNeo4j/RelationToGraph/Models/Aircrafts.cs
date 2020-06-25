using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Aircrafts
    {
        public Aircrafts()
        {
            Flights = new HashSet<Flights>();
            Seats = new HashSet<Seats>();
        }

        public int AircraftId { get; set; }
        public string AircraftNum { get; set; }
        public string AircraftType { get; set; }
        public int? Airline { get; set; }

        public Airlines AirlineNavigation { get; set; }
        public ICollection<Flights> Flights { get; set; }
        public ICollection<Seats> Seats { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(aircraft:Aircraft{{title:\"{0}\",type:\"{1}\",id:{2},airline:{3}}});", AircraftNum, AircraftType, AircraftId, Airline);
        }

        public static string GetRelationshipQuery()
        {
            return "MATCH(aircraft:Aircraft),(airline:Airline) WHERE airline.id = aircraft.airline CREATE (aircraft)-[l:Belongs_To]->(airline);";
        }
    }
}
