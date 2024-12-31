using AutoMapper;
using Cybervision.Dapr.Services;
using Domain.DataTransferObjects;

namespace TodoApi.Services
{
    public class UserService : BaseDataService<UserDto, IUserDataService>, IUserService
    {
        private readonly IUserDataService _dataService;
        private readonly IMapper _mapper;
        public UserService(IUserDataService dataService, IMapper mapper) :base(dataService, mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public Task<UserDto> UpdateManyAsync(IEnumerable<UserDto> entities)
        {
            throw new NotImplementedException();
        }
    }
}
