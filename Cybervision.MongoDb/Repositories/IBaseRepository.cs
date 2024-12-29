namespace Cybervision.Dapr.Repositories
{
    public interface IBaseRepository<T, U>
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetBySearchKey(string searchKey);
        Task<T> CreateAsync(T itemToCreate);
        Task<bool> UpdateAsync(T itemToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}
