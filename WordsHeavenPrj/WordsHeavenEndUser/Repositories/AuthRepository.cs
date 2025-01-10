using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using WordsHeavenEndUser.Data;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            var isTaken = await _context.Users.AnyAsync(u => u.Email == email);

            return isTaken;
        }

        public async Task RegisterUser(EndUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserCredentials(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return false;
            }
            return true;
        }
    }
}
