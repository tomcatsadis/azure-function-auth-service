using AuthService.Persistence.IntegrationTests.Util;
using Xunit;

namespace AuthService.API.IntegrationTests.Util
{
    [CollectionDefinition(nameof(APITestCollection))]
    public class APITestCollection : ICollectionFixture<APITestFixture>, 
        ICollectionFixture<PersistenceTestFixture>
    {
    }
}
