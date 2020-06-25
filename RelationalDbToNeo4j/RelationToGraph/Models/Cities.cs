using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Airports = new HashSet<Airports>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? Country { get; set; }

        public Countries CountryNavigation { get; set; }
        public ICollection<Airports> Airports { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(city:City{{title:\"{0}\",id:{1},country:{2}}});", CityName, CityId, Country);
        }

        public static string GetRelationshipQuery()
        {
            return "MATCH(country:Country),(city:City) WHERE country.id = city.country CREATE (city)-[l:Located_In]->(country);";
        }
    }
}
