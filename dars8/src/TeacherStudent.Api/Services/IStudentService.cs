using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentGetDto>> GetAllAsync();
        Task<StudentGetDto> GetByIdAsync(int id);
        Task<int> CreateAsync(StudentCreateDto dto);
        Task UpdateAsync(int id, StudentCreateDto dto);
        Task DeleteAsync(int id);
    }
}