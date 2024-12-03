using Cybervision.Dapr.DataModels;
using MongoDB.Driver;

public class MongoRepository
{
    private readonly IMongoCollection<UserDocument> _collection;
    private readonly MongoDbContext _mongoDbContext;
    public MongoRepository(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _collection = _mongoDbContext.GetCollection<UserDocument>("products");
    }

    public async Task<IEnumerable<UserDocument>> GetUserDocumentsAsync()
    {
        return await _collection.Find(Builders<UserDocument>.Filter.Empty).ToListAsync();
    }

    public async Task AddDocumentAsync(UserDocument document)
    {
        await _collection.InsertOneAsync(document);
    }
}