using System;
using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Routes
    {
        public Routes()
        {
            Flights = new HashSet<Flights>();
        }

        public int RouteId { get; set; }
        public string RouteCode { get; set; }
        public string AircraftType { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan DestinationTime { get; set; }
        public float? FirstClassPrice { get; set; }
        public float? BusinessClassPrice { get; set; }
        public float? EconomyClassPrice { get; set; }
        public int? DeparturePoint { get; set; }
        public int? DestinationPoint { get; set; }
        public int? Airline { get; set; }

        public Airlines AirlineNavigation { get; set; }
        public Airports DeparturePointNavigation { get; set; }
        public Airports DestinationPointNavigation { get; set; }
        public ICollection<Flights> Flights { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(route:Route{{title:\"{0}\",type:\"{1}\",departureTime:\"{2}\",destinationTime:\"{3}\",firstClassPrice:{4},businessClassPrice:{5},economyClassPrice:{6},id:{7},departurePoint:{8},destinationPoint:{9},airline:{10}}});", RouteCode, AircraftType, DepartureTime.ToString(), DepartureTime.ToString(), FirstClassPrice.ToString().Replace(',', '.'), BusinessClassPrice.ToString().Replace(',', '.'), EconomyClassPrice.ToString().Replace(',', '.'), RouteId, DeparturePoint, DestinationPoint, Airline);
        }

        public static IEnumerable<string> GetRelationshipQuery()
        {
            yield return "MATCH(route:Route),(airline:Airline) WHERE airline.id = route.airline CREATE (route)-[l:Offered_By]->(airline);";
            yield return "MATCH(route:Route),(city:City) WHERE city.id = route.departurePoint CREATE (route)-[l:Crashes_Out_Of]->(city);";
            yield return "MATCH(route:Route),(city:City) WHERE city.id = route.destinationPoint CREATE (route)-[l:Arrives_To]->(city);";
        }
    }
}
