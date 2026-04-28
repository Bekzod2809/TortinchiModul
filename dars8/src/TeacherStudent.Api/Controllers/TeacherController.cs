using Microsoft.AspNetCore.Mvc;
using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;

namespace TeacherStudent.Api.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repo;

        public TeacherController(ITeacherRepository repo)
        {
            _repo = repo;
        }

        // GET api/teachers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _repo.GetAllAsync();
            return Ok(teachers);
        }

        // GET api/teachers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _repo.GetByIdAsync(id);
            if (teacher == null)
                return NotFound($"Teacher {id} topilmadi");

            return Ok(teacher);
        }

        // POST api/teachers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            var newId = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
        }

        // PUT api/teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Teacher {id} topilmadi");

            await _repo.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE api/teachers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Teacher {id} topilmadi");

            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
