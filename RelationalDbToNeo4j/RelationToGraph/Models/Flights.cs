using System;
using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Flights
    {
        public int FlightId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime DestinationDate { get; set; }
        public int? HandLuggageWeight { get; set; }
        public int? LuggageWeight { get; set; }
        public int? Route { get; set; }
        public int? Aircraft { get; set; }

        public Aircrafts AircraftNavigation { get; set; }
        public Routes RouteNavigation { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(flight:Flight{{title:\"Flight\",departureDate:\"{0}\",destinationDate:\"{1}\",handLuggageWeight:{2},luggageWeight:{3},id:{4},route:{5},aircraft:{6}}});", DepartureDate.ToString(), DestinationDate.ToString(), HandLuggageWeight, LuggageWeight, FlightId, Route, Aircraft);
        }

        public static IEnumerable<string> GetRelationshipQuery()
        {
            yield return "MATCH(flight:Flight),(aircraft:Aircraft) WHERE aircraft.id = flight.aircraft CREATE (flight)-[l:Carried_On]->(aircraft);";
            yield return "MATCH(flight:Flight),(route:Route) WHERE route.id = flight.route CREATE (flight)-[l:Refers_To]->(route);";
        }
    }
}
