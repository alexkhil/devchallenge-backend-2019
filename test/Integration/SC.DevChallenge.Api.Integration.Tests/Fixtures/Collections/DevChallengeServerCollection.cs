using Xunit;

namespace SC.DevChallenge.Api.Integration.Tests.Fixtures.Collections
{
    [CollectionDefinition(nameof(DevChallengeServerCollection))]
    public class DevChallengeServerCollection : ICollectionFixture<DevChallengeServerFixture>
    {
    }
}
