using System;
using Microsoft.EntityFrameworkCore;
using WordsHeavenEndUser.Data;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Models;


namespace WordsHeavenEndUser.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<EndUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<EndUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(EndUser user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(EndUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<EndUser> GetUserByEmailAsync(string email)
        {
            EndUser user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }



    }
}
