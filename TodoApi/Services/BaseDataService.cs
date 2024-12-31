using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class BaseDataService<T, S> : IDataService<T>
        where T : Domain.DataTransferObjects.ISearchable
        where S : IDataService<T>
        
    {
        private readonly IMapper _mapper;
        private readonly S _dataService;
        public BaseDataService(S dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        public async Task<T> CreateAsync(T entity)
        {
            entity.SearchIdKey = Guid.NewGuid();
            var result = await _dataService.CreateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataService.DeleteAsync(id).ConfigureAwait(false);
            return result;
        }

        public async IAsyncEnumerable<T> GetAllAsync()
        {
            var results = _dataService.GetAllAsync().ConfigureAwait(false);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result = await _dataService.GetAsync(id).ConfigureAwait(false);
            return result;
        }


        public async Task<T> GetBySearchKeyAsync(string search)
        {
            return await _dataService.GetBySearchKeyAsync(search).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await _dataService.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<T> UpdateManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
