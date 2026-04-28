using Microsoft.AspNetCore.Mvc;
using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;

namespace TeacherStudent.Api.Controllers
{
    [ApiController]
    public class TeacherStudentController : ControllerBase
    {
        private readonly ITeacherStudentRepository _repo;

        public TeacherStudentController(ITeacherStudentRepository repo)
        {
            _repo = repo;
        }

        // POST api/teacher-students/assign
        [HttpPost("api/teacher-students/assign")]
        public async Task<IActionResult> Assign([FromBody] TeacherStudentAssignDto dto)
        {
            await _repo.AssignAsync(dto);
            return Ok("Muvaffaqiyatli bog'landi");
        }

        // DELETE api/teacher-students
        [HttpDelete("api/teacher-students")]
        public async Task<IActionResult> Remove([FromBody] TeacherStudentAssignDto dto)
        {
            await _repo.RemoveAsync(dto);
            return NoContent();
        }

        // GET api/teachers/{teacherId}/students
        [HttpGet("api/teachers/{teacherId}/students")]
        public async Task<IActionResult> GetStudentsByTeacher(int teacherId)
        {
            var students = await _repo.GetStudentsByTeacherAsync(teacherId);
            return Ok(students);
        }

        // GET api/students/{studentId}/teachers
        [HttpGet("api/students/{studentId}/teachers")]
        public async Task<IActionResult> GetTeachersByStudent(int studentId)
        {
            var teachers = await _repo.GetTeachersByStudentAsync(studentId);
            return Ok(teachers);
        }
    }
}