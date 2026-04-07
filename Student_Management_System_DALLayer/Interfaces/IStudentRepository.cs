using Student_Management_System_DAL.Models;

namespace Student_Management_System_DAL.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
    }
}
