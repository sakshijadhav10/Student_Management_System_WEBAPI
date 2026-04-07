using Microsoft.EntityFrameworkCore;
using Student_Management_System_DAL.Data;
using Student_Management_System_DAL.Interfaces;
using Student_Management_System_DAL.Models;

namespace Student_Management_System_DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;

        public StudentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _db.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _db.Students.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return false;

            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

