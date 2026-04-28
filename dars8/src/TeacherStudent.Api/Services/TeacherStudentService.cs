using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;
using TeacherStudent.Api.Services.Interfaces;

namespace TeacherStudent.Api.Services;

public class TeacherStudentService : ITeacherStudentService
{
    private readonly ITeacherStudentRepository _repo;
    private readonly ITeacherRepository _teacherRepo;
    private readonly IStudentRepository _studentRepo;

    public TeacherStudentService(
        ITeacherStudentRepository repo,
        ITeacherRepository teacherRepo,
        IStudentRepository studentRepo)
    {
        _repo = repo;
        _teacherRepo = teacherRepo;
        _studentRepo = studentRepo;
    }

    public async Task<bool> AssignAsync(TeacherStudentAssignDto dto)
    {
        var teacher = await _teacherRepo.GetByIdAsync(dto.TeacherId);
        if (teacher == null) return false;

        var student = await _studentRepo.GetByIdAsync(dto.StudentId);
        if (student == null) return false;

        await _repo.AssignAsync(dto);
        return true;
    }

    public async Task RemoveAsync(TeacherStudentAssignDto dto)
    {
        await _repo.RemoveAsync(dto);
    }

    public async Task<IEnumerable<TeacherWithStudentsDto>> GetStudentsByTeacherAsync(int teacherId)
    {
        return await _repo.GetStudentsByTeacherAsync(teacherId);
    }

    public async Task<IEnumerable<StudentWithTeachersDto>> GetTeachersByStudentAsync(int studentId)
    {
        return await _repo.GetTeachersByStudentAsync(studentId);
    }
}