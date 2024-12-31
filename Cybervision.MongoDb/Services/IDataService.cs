namespace Cybervision.Dapr.Services
{
    public interface IDataService<T>
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> GetBySearchKeyAsync(string search);
        Task<bool> DeleteAsync(Guid id);    
        Task<bool> UpdateAsync(T entity);
        Task<T> UpdateManyAsync(IEnumerable<T> entities);    
        Task<T> CreateAsync(T entity);
    }
}
