namespace TodoApi.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);    
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateManyAsync(IEnumerable<T> entities);    
        Task<T> CreateAsync(T entity);
    }
}
