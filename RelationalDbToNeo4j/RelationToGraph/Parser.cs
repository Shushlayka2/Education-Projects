using RelationToGraph.Models;

namespace RelationToGraph
{
    public class Parser
    {
        private Neo4jProxy proxy = new Neo4jProxy();

        public virtual void Parse()
        {
            using (var db = new RelationalDBContext())
            {
                //Delete all nodes
                proxy.ExecuteQuery("MATCH (n) DETACH DELETE n;");

                //Create countries
                foreach (var country in db.Countries)
                {
                    proxy.ExecuteQuery(country.ToQuery());
                }

                //Create cities
                foreach (var city in db.Cities)
                {
                    proxy.ExecuteQuery(city.ToQuery());
                }
                proxy.ExecuteQuery(Cities.GetRelationshipQuery());

                //Create airports
                foreach (var airport in db.Airports)
                {
                    proxy.ExecuteQuery(airport.ToQuery());
                }
                proxy.ExecuteQuery(Airports.GetRelationshipQuery());

                //Create airlines
                foreach (var airline in db.Airlines)
                {
                    proxy.ExecuteQuery(airline.ToQuery());
                }
                proxy.ExecuteQuery(Airlines.GetRelationshipQuery());

                //Create aircrafts
                foreach (var aircraft in db.Aircrafts)
                {
                    proxy.ExecuteQuery(aircraft.ToQuery());
                }
                proxy.ExecuteQuery(Aircrafts.GetRelationshipQuery());

                //Create routes
                foreach (var route in db.Routes)
                {
                    proxy.ExecuteQuery(route.ToQuery());
                }
                foreach (var query in Routes.GetRelationshipQuery())
                {
                    proxy.ExecuteQuery(query);
                }

                //Create flights
                foreach (var flight in db.Flights)
                {
                    proxy.ExecuteQuery(flight.ToQuery());
                }
                foreach (var query in Flights.GetRelationshipQuery())
                {
                    proxy.ExecuteQuery(query);
                }
            }
        }
    }
}
