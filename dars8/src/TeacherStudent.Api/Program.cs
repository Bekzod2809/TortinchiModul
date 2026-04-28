using TeacherStudent.Api.Repositories;
using TeacherStudent.Repositories;

namespace TeacherStudent.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Repository registration
            builder.Services.AddScoped<ITeacherRepository>(
                _ => new TeacherRepository(connectionString!)
            );
            builder.Services.AddScoped<IStudentRepository>(
                _ => new StudentRepository(connectionString!)
            );
            builder.Services.AddScoped<ITeacherStudentRepository>(
                _ => new TeacherStudentRepository(connectionString!)
            );
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}