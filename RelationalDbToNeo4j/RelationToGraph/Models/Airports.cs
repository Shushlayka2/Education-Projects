using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Airports
    {
        public Airports()
        {
            RoutesDeparturePointNavigation = new HashSet<Routes>();
            RoutesDestinationPointNavigation = new HashSet<Routes>();
        }

        public int AirportId { get; set; }
        public string AirportName { get; set; }
        public string AirportCode { get; set; }
        public int? City { get; set; }

        public Cities CityNavigation { get; set; }
        public ICollection<Routes> RoutesDeparturePointNavigation { get; set; }
        public ICollection<Routes> RoutesDestinationPointNavigation { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(airport:Airport{{title:\"{0}\",code:\"{1}\",id:{2},city:{3}}});", AirportName, AirportCode, AirportId, City);
        }

        public static string GetRelationshipQuery()
        {
            return "MATCH(city:City),(airport:Airport) WHERE city.id = airport.city CREATE (airport)-[l:Located_In]->(city);";
        }
    }
}
