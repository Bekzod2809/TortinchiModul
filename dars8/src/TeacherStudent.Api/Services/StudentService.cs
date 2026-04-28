using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;
using TeacherStudent.Api.Services.Interfaces;

namespace TeacherStudent.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StudentGetDto>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<StudentGetDto?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(StudentCreateDto dto)
        {
            return await _repo.CreateAsync(dto);
        }

        public async Task UpdateAsync(int id, StudentCreateDto dto)
        {
            await _repo.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}