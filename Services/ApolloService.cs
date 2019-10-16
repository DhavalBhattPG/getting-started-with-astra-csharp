using Cassandra;

namespace getting_started_with_apollo_csharp.Services
{
    public class ApolloService : Interfaces.IDataStaxService
    {
        private static readonly ApolloService _ApolloServiceInstance = new ApolloService();
        private ISession _session;

        public ISession Session
        {
            get
            {
                if (_session==null)
                {
                    _session =  Cluster.Builder()
                       .WithCloudSecureConnectionBundle(@"/Users/dave.bechberger/Downloads/secure-connect-getting-started.zip")
                       .WithCredentials("getting", "started")
                       .Build()
                       .Connect("getting_started");   
                    
                    _session.UserDefinedTypes.Define(UdtMap.For<Models.location_udt>());
                } 
                    return _session;
            }
        }
    }
}