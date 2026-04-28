using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Repositories;

public interface ITeacherStudentRepository
{
    Task AssignAsync(TeacherStudentAssignDto dto);
    Task RemoveAsync(TeacherStudentAssignDto dto);
    Task<IEnumerable<TeacherWithStudentsDto>> GetStudentsByTeacherAsync(int teacherId);
    Task<IEnumerable<StudentWithTeachersDto>> GetTeachersByStudentAsync(int studentId);
}