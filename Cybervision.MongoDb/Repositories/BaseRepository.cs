using AutoMapper;
using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Helpers;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

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
            var serverInfo = new MongoServerAddress(config.GetConnectionString("TodoAppDb"));

            var client = new MongoClient(new MongoClientSettings
            {
                Server = serverInfo,
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e => InterceptCommand(e));
                }
            });

            var database = client.GetDatabase("TodoApp");

            _collection = database.GetCollection<U>(collectionName);
            _mapper = mapper;

        }

        private void InterceptCommand(CommandStartedEvent e)
        {
            Console.WriteLine($"{e.CommandName} - {e.Command.ToJson(new JsonWriterSettings { Indent = true })}");
            Console.WriteLine(new String('-', 32));
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
            var filter = SearchKeyHelper.CreateSearchKeyFilter<U>(searchKey);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<T>(result);
        }
    }
}
