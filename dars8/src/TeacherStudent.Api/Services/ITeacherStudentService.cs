using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Services.Interfaces
{
    public interface ITeacherStudentService
    {
        Task<bool> AssignAsync(TeacherStudentAssignDto dto);
        Task RemoveAsync(TeacherStudentAssignDto dto);
        Task<IEnumerable<TeacherWithStudentsDto>> GetStudentsByTeacherAsync(int teacherId);
        Task<IEnumerable<StudentWithTeachersDto>> GetTeachersByStudentAsync(int studentId);
    }
}