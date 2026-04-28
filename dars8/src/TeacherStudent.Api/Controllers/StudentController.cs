using Microsoft.AspNetCore.Mvc;
using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;

namespace TeacherStudent.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repo;

        public StudentController(IStudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repo.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null)
                return NotFound($"Student {id} topilmadi");

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            var newId = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Student {id} topilmadi");

            await _repo.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Student {id} topilmadi");

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}