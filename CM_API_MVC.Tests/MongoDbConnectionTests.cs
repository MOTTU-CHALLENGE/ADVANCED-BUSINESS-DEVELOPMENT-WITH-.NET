using MongoDB.Driver;

namespace CM_API_MVC.Tests
{
    public class MongoDbConnectionTests
    {
        private const string ConnectionString = "mongodb://admin:adminpass@localhost:27017";
        private const string DatabaseName = "mottuDB";

        // [Fact] 
        public async Task DeveConectarComMongoDb()
        {
            // Arrange
            var client = new MongoClient(ConnectionString);

            // Act
            var database = client.GetDatabase(DatabaseName);

            // Tenta listar as collections (força a conexão)
            var collections = await database.ListCollectionsAsync();
            var lista = await collections.ToListAsync();

            // Assert
            Assert.NotNull(lista); // Se chegou aqui sem exception, está OK
        }
    }
}
