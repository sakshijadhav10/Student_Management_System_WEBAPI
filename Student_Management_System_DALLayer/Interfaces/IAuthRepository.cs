using Student_Management_System_DAL.Models;

namespace Student_Management_System_DAL.Interfaces
{
    public interface IAuthRepository
    {

        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
    }
}
