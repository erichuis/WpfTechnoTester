namespace WpfTechnoTester.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);    
        Task<bool> UpdateAsync(T entity);
        Task<T> UpdateManyAsync(IEnumerable<T> entities);    
        Task<T> CreateAsync(T entity);
    }
}
