using Student_Management_System_BLL.DTOS.Student;
using Student_Management_System_BLL.Interfaces;
using Student_Management_System_DAL.Interfaces;
using Student_Management_System_DAL.Models;

namespace Student_Management_System_BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _repo.GetAllAsync();
            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                Course = s.Course,
                CreatedDate = s.CreatedDate
            });
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null) return null;

            return new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                Course = s.Course,
                CreatedDate = s.CreatedDate
            };
        }

        public async Task<StudentDTO> CreateAsync(CreateStudentDTO dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Course = dto.Course,
                CreatedDate = DateTime.UtcNow
            };

            await _repo.CreateAsync(student);

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course,
                CreatedDate = student.CreatedDate
            };
        }

        public async Task<StudentDTO> UpdateAsync(int id, UpdateStudentDTO dto)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null) return null;

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Age = dto.Age;
            student.Course = dto.Course;

            await _repo.UpdateAsync(student);

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course,
                CreatedDate = student.CreatedDate
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
