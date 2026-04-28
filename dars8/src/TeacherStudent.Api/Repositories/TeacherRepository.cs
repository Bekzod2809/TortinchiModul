using Microsoft.Data.SqlClient;
using System.Data;
using TeacherStudent.Api.Dtos;
using TeacherStudent.Api.Repositories;

namespace TeacherStudent.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TeacherGetDto>> GetAllAsync()
        {
            var list = new List<TeacherGetDto>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetAllTeachers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new TeacherGetDto
                {
                    TeacherId = reader.GetInt32("TeacherId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Subject = reader.GetString("Subject")
                });
            }

            return list;
        }

        public async Task<TeacherGetDto?> GetByIdAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetTeacherById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", id);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TeacherGetDto
                {
                    TeacherId = reader.GetInt32("TeacherId"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.GetString("LastName"),
                    Subject = reader.GetString("Subject")
                };
            }

            return null;
        }

        public async Task<int> CreateAsync(TeacherCreateDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("CreateTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Subject", dto.Subject);

            await con.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(int id, TeacherCreateDto dto)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("UpdateTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Parameters.AddWithValue("@FirstName", dto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", dto.LastName);
            cmd.Parameters.AddWithValue("@Subject", dto.Subject);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DeleteTeacher", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TeacherId", id);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}