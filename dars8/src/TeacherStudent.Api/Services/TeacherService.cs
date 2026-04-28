using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;

namespace TeacherStudent.Api.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repo;

        public TeacherService(ITeacherRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TeacherGetDto>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<TeacherGetDto?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(TeacherCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Subject))
                throw new ArgumentException("Subject bo'sh bo'lishi mumkin emas");

            return await _repo.CreateAsync(dto);
        }

        public async Task UpdateAsync(int id, TeacherCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Subject))
                throw new ArgumentException("Subject bo'sh bo'lishi mumkin emas");

            await _repo.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}