using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Students.Services;
using SchoolManagementSystem.Teachers.Services;
using SchoolManagementSystem.Students.Repositories.Interfaces;
using SchoolManagementSystem.Teachers.Repositories.Interfaces;
using SchoolManagementSystem.Users.Repositories.Interfaces;
using SchoolManagementSystem.Students.Repositories;
using SchoolManagementSystem.Teachers.Repositories;
using SchoolManagementSystem.Users.Repositories;
using SchoolManagementSystem.Students.Services.Interfaces;
using SchoolManagementSystem.Teachers.Services.Interfaces;
using SchoolManagementSystem.Classes.Repositories.Interfaces;
using SchoolManagementSystem.Classes.Repositories;
using SchoolManagementSystem.Enrollments.Repositories.Interfaces;
using SchoolManagementSystem.Enrollments.Repositories;
using SchoolManagementSystem.Classes.Services.Interfaces;
using SchoolManagementSystem.Classes.Services;
using SchoolManagementSystem.Enrollments.Services.Interfaces;
using SchoolManagementSystem.Enrollments.Services;
using SchoolManagementSystem.Middlewares;
using Microsoft.AspNetCore.Authentication;
using SchoolManagementSystem.Auths.Services;
using SchoolManagementSystem.Auths.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load env file
DotNetEnv.Env.Load();
builder.Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

// add config db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// add BasicAuthentication
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthentication>("BasicAuthentication", null);
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// add repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// add services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// add automapper
builder.Services.AddAutoMapper(
    typeof(StudentMappingProfile),
    typeof(TeacherMappingProfile),
    typeof(ClassMappingProfile),
    typeof(EnrollmentMappingProfile)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers(); // mengaktifkan endpoint /api/students

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
