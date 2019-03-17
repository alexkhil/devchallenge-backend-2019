namespace SC.DevChallenge.Configuration.Abstractions
{
    public interface IDbConfiguration
    {
        string ConnectionString { get; set; }

        int CommandTimeout { get; set; }

        int RetryCount { get; set; }
    }
}
