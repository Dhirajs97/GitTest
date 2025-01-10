using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> ValidateUserCredentials(string email, string password);

        Task<bool> IsEmailTaken(string email);

        Task RegisterUser(EndUser user);

    }
}
