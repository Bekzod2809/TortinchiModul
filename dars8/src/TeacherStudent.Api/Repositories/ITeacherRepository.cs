using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<TeacherGetDto>> GetAllAsync();
        Task<TeacherGetDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(TeacherCreateDto dto);
        Task UpdateAsync(int id, TeacherCreateDto dto);
        Task DeleteAsync(int id);
    }
}