using WordsHeavenEndUser.Dtos;

namespace WordsHeavenEndUser.Interfaces.Services
{
    public interface IUserService
    {

        Task<EndUserDto> GetUserByIdAsync(int id);

        Task<IEnumerable<EndUserDto>> GetAllUsersAsync();
        Task AddUserAsync(EndUserDto userDto);
        Task UpdateUserAsync(EndUserDto userDto);
        Task DeleteUserAsync(int id);

    }
}
