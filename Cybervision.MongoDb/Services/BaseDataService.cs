using Cybervision.Dapr.Repositories;

namespace Cybervision.Dapr.Services
{
    public class BaseDataService<T, R, U> : IDataService<T>
        where T : Domain.DataTransferObjects.ISearchable
        where U : DataModels.ISearchable
        where R : IBaseRepository<T, U>
    {
        protected readonly R _repository;
        public BaseDataService(R repository)
        {
            _repository = repository;
        }
        public async Task<T> CreateAsync(T entity)
        {
            entity.SearchIdKey = Guid.NewGuid();
            var result = await _repository.CreateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id).ConfigureAwait(false);
            return result;
        }

        public async IAsyncEnumerable<T> GetAllAsync()
        {
            var results = _repository.GetAllAsync().ConfigureAwait(false);
            await foreach (var item in results)
            {
                yield return item;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<T> UpdateManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
