using API.Contexts;
using API.Contracts;
using API.Models;
using API.Utility;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//dapatkan connectionstring
var connectionString= builder.Configuration.GetConnectionString("DefaultConnection");
//configuration dengan connection string
builder.Services.AddDbContext<BookingManagementDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
    smtpServer: builder.Configuration["Email:SmtpServer"],
    smtpPort: int.Parse(builder.Configuration["Email:SmtpPort"]),
    fromEmailAddress: builder.Configuration["Email:FromEmailAddress"]
    ));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app = middleware, request lewat v
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
