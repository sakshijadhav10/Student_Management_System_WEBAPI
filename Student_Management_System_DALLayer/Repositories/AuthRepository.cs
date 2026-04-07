using Microsoft.EntityFrameworkCore;
using Student_Management_System_DAL.Data;
using Student_Management_System_DAL.Interfaces;
using Student_Management_System_DAL.Models;

namespace Student_Management_System_DAL.Repositories
{
    public class AuthRepository :IAuthRepository
    {
        private readonly AppDbContext _db;

        public AuthRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
