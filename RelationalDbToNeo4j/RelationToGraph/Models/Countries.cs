using System.Collections.Generic;

namespace RelationToGraph.Models
{
    public partial class Countries : Queryable
    {
        public Countries()
        {
            Airlines = new HashSet<Airlines>();
            Cities = new HashSet<Cities>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<Airlines> Airlines { get; set; }
        public ICollection<Cities> Cities { get; set; }

        public virtual string ToQuery()
        {
            return string.Format("CREATE(country:Country{{title:\"{0}\",id:{1}}});", CountryName, CountryId);
        }
    }
}
