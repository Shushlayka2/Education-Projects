using Neo4j.Driver.V1;
using System.Configuration;

namespace RelationToGraph
{
    public class Neo4jProxy
    {
        private readonly IDriver _driver;

        public Neo4jProxy()
        {
            _driver = GraphDatabase.Driver(ConfigurationManager.AppSettings["uri"], AuthTokens.Basic(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]));
        }

        public void ExecuteQuery(string query)
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx =>
                {
                    var result = tx.Run(query);
                });
            }
        }
    }
}
