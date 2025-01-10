using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<EndUser> GetUserByIdAsync(int id);

        Task<IEnumerable<EndUser>> GetAllUsersAsync();


        Task AddUserAsync(EndUser user);
        Task UpdateUserAsync(EndUser user);
        Task DeleteUserAsync(int id);


        Task<EndUser> GetUserByEmailAsync(string email);
    }
}
