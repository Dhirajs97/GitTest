using AutoMapper;
using WordsHeavenEndUser.Dtos;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Interfaces.Services;
using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task AddUserAsync(EndUserDto userDto)
        {
            var user = _mapper.Map<EndUser>(userDto);
            await _userRepository.AddUserAsync(user);
        }

        

        public async Task<IEnumerable<EndUserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<EndUserDto>>(users);
        }

        public async Task<EndUserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<EndUserDto>(user);
        }

        public async Task UpdateUserAsync(EndUserDto userDto)
        {
            var user = _mapper.Map<EndUser>(userDto);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
