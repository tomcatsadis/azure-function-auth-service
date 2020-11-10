using Xunit;

namespace AuthService.Persistence.IntegrationTests.Util
{
    [CollectionDefinition(nameof(PersistenceTestCollection))]
    public class PersistenceTestCollection : ICollectionFixture<PersistenceTestFixture>
    {
    }
}
