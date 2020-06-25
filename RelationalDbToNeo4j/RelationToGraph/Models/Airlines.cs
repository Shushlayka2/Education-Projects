using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Airlines
    {
        public Airlines()
        {
            Aircrafts = new HashSet<Aircrafts>();
            Routes = new HashSet<Routes>();
        }

        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public float? AirlineRating { get; set; }
        public float? AirlineSafety { get; set; }
        public string AirlineSite { get; set; }
        public int? Country { get; set; }

        public Countries CountryNavigation { get; set; }
        public ICollection<Aircrafts> Aircrafts { get; set; }
        public ICollection<Routes> Routes { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(airline:Airline{{title:\"{0}\",rating:{1},safety:{2},site:\"{3}\",id:{4},country:{5}}});", AirlineName, AirlineRating.ToString().Replace(',', '.'), AirlineSafety.ToString().Replace(',', '.'), AirlineSite, AirlineId, Country);
        }

        public static string GetRelationshipQuery()
        {
            return "MATCH(country:Country),(airline:Airline) WHERE country.id = airline.country CREATE (airline)-[l:Based_In]->(country);";
        }
    }
}
