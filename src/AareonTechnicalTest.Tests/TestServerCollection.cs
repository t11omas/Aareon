using Microsoft.Extensions.Options;
using Xunit;

namespace AareonTechnicalTest.Tests
{
    [CollectionDefinition("TestServer collection")]
    public class TestServerCollection : ICollectionFixture<TestServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
