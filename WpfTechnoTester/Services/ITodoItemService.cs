using Domain.DataTransferObjects;
using Domain.Models;
using WpfTechnoTester.Services;

namespace TodoApi.Services
{
    public interface ITodoItemService : IDataService<TodoItem>
    {
    }
}
