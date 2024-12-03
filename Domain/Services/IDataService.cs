using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);
        Task<bool> Delete(int id);    
        Task<T> Update(T entity);
        Task<T> UpdateMany(IEnumerable<T> entities);    
        Task<T> Create(T entity);
    }
}
