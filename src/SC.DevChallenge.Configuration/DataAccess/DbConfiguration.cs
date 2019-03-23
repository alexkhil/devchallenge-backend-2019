using SC.DevChallenge.Configuration.Abstractions;

namespace SC.DevChallenge.Configuration.DataAccess
{
    public class DbConfiguration : IDbConfiguration
    {
        public string ConnectionString { get; set; }
    }
}