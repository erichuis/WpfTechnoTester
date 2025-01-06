using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace IdentityServer.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(ConfigurationManager configuration)
        {
            var serverInfo = new MongoServerAddress(configuration["ConnectionStrings:TodoAppDb"]);
            var databaseName = configuration["ConnectionStrings:DatabaseName"];

            var client = new MongoClient(new MongoClientSettings
            {
                Server = serverInfo,
                //ClusterConfigurator = cb =>
                //{
                //    cb.Subscribe<CommandStartedEvent>(e => InterceptCommand(e));
                //}
            });

            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<IdentityUser> Users => _database.GetCollection<IdentityUser>("Users");

        public IMongoCollection<IdentityRole> Roles => _database.GetCollection<IdentityRole>("Roles");
    }
}

