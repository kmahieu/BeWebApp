using DocumentService.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace DocumentService.Repositories
{
    public class AppDbContext : DbContext
    {
        // Pont entre notre model et notre BDD, récupere notre DbContext au niveau du startup
        private readonly IMongoDatabase _db;
        // Pont entre notre model et notre BDD, récupere notre DbContext au niveau du startup
        public AppDbContext(IMongoClient client, string dbName)
        {
            _db = client.GetDatabase(dbName);
        }
        public IMongoCollection<Document> document => _db.GetCollection<Document>("Document");
    }
}