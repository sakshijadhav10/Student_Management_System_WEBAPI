using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System_BLL.DTOS.Student;
using Student_Management_System_BLL.Interfaces;

namespace Student_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound("Student not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDTO dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDTO dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null) return NotFound("Student not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound("Student not found");

            return Ok("Deleted Successfully");
        }
    }
}
