using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Cybervision.Dapr.Repositories
{
    public class BaseRepository<T, U> : IBaseRepository<T, U> 
        where U : DataModels.ISearchable
        where T : Domain.DataTransferObjects.ISearchable
    {
        protected readonly IMongoCollection<U> _collection;
        private readonly IMapper _mapper;

        public BaseRepository(IConfiguration config, IMapper mapper, string collectionName)
        {
            var client = new MongoClient(config.GetConnectionString("TodoAppDb"));
            var database = client.GetDatabase("TodoApp");

            _collection = database.GetCollection<U>(collectionName);
            _mapper = mapper;
        }

        public async IAsyncEnumerable<T> GetAllAsync()
        {
            var list = await _collection.Find(task => true).ToListAsync();

            foreach (var item in list)
            {
                yield return _mapper.Map<T>(item);
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _collection.Find(item => item.SearchIdKey == id).FirstOrDefaultAsync();
            return _mapper.Map<T>(result);
        }

        public async Task<T> CreateAsync(T itemToCreate)
        {
            var document = _mapper.Map<U>(itemToCreate);
            await _collection.InsertOneAsync(document, new InsertOneOptions { BypassDocumentValidation = true });
            //Todo retrieve Id

            return itemToCreate;
        }

        public async Task<bool> UpdateAsync(T itemToUpdate)
        {
            var document = _mapper.Map<U>(itemToUpdate);
            var result = await _collection.ReplaceOneAsync(item => item.SearchIdKey == itemToUpdate.SearchIdKey, document);
            return result.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _collection.DeleteOneAsync(item => item.SearchIdKey == id);
            return result.IsAcknowledged;
        }

        public async Task<T> GetBySearchKey(string searchKey)
        {
            var result = await _collection.Find(item => item.SearchKey == searchKey).FirstOrDefaultAsync();
            return _mapper.Map<T>(result);
        }
    }
}
