using Microsoft.Data.SqlClient;
using System.Data;
using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Repositories
{
    public class TeacherStudentRepository : ITeacherStudentRepository
    {
        private readonly string _connectionString;

        public TeacherStudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AssignAsync(TeacherStudentAssignDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_AssignStudentToTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", dto.TeacherId);
            cmd.Parameters.AddWithValue("@StudentId", dto.StudentId);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task RemoveAsync(TeacherStudentAssignDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_RemoveStudentFromTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", dto.TeacherId);
            cmd.Parameters.AddWithValue("@StudentId", dto.StudentId);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<TeacherWithStudentsDto>> GetStudentsByTeacherAsync(int teacherId)
        {
            var list = new List<TeacherWithStudentsDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetStudentsByTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", teacherId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new TeacherWithStudentsDto
                {
                    StudentId = reader.GetInt32("StudentId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Grade = reader.GetString("Grade")
                });
            }

            return list;
        }

        public async Task<IEnumerable<StudentWithTeachersDto>> GetTeachersByStudentAsync(int studentId)
        {
            var list = new List<StudentWithTeachersDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetTeachersByStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new StudentWithTeachersDto
                {
                    TeacherId = reader.GetInt32("TeacherId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Subject = reader.GetString("Subject")
                });
            }

            return list;
        }
    }
}