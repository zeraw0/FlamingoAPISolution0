using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flamingo_API.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly FlamingoDbContext _context;

        public UserRepository(FlamingoDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync() // Implement this method
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }



        public User? ValidateUser(string uname, string pwd)
        {
            User user = _context.Users.SingleOrDefault(u => u.Email == uname);

            if (user != null && user.Password == pwd)
            {
                return user;
            }

            return null;  // Return null if user is not found or password doesn't match
        }




    }
}
