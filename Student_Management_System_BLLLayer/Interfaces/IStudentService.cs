using Student_Management_System_BLL.DTOS.Student;

namespace Student_Management_System_BLL.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetByIdAsync(int id);
        Task<StudentDTO> CreateAsync(CreateStudentDTO dto);
        Task<StudentDTO> UpdateAsync(int id, UpdateStudentDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
