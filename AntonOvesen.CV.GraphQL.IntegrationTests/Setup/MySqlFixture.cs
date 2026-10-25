using Testcontainers.MySql;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Setup
{
    [CollectionDefinition("mysql")]
    public class ContainerFixture : ICollectionFixture<MySqlFixture>
    {
    }

    public class MySqlFixture : IAsyncLifetime
    {
        private readonly MySqlContainer mySqlContainer = new MySqlBuilder("mysql:8.0.26").Build();
        public static string ConnectionString { get; private set; }

        public async ValueTask InitializeAsync()
        {
            await mySqlContainer.StartAsync();
            ConnectionString = mySqlContainer.GetConnectionString();
        }

        public new ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            return mySqlContainer.DisposeAsync();
        }
    }
}