using Microsoft.Data.SqlClient;
using System.Data;
using TeacherStudent.Api.Dtos;

namespace TeacherStudent.Api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<StudentGetDto>> GetAllAsync()
        {
            var list = new List<StudentGetDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetAllStudents", con);
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new StudentGetDto
                {
                    StudentId = reader.GetInt32("StudentId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Grade = reader.GetString("Grade")
                });
            }

            return list;
        }

        public async Task<StudentGetDto?> GetByIdAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_GetStudentById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new StudentGetDto
                {
                    StudentId = reader.GetInt32("StudentId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Grade = reader.GetString("Grade")
                };
            }

            return null;
        }

        public async Task<int> CreateAsync(StudentCreateDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_CreateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Grade", dto.Grade);

            await con.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(int id, StudentCreateDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_UpdateStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@FirstName", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Grade", dto.Grade);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_DeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", id);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}