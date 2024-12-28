using Domain.DataTransferObjects;
using System.Security;

namespace WpfTechnoTester.Clients
{
    public interface IHttpUserClient
    {
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
    }
}
