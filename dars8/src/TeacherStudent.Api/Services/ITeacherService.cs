using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Services;

public interface ITeacherService
{
    Task<IEnumerable<TeacherGetDto>> GetAllAsync();
    Task<TeacherGetDto> GetByIdAsync(int id);
    Task<int> CreateAsync(TeacherCreateDto dto);
    Task UpdateAsync(int id, TeacherCreateDto dto);
    Task DeleteAsync(int id);
}