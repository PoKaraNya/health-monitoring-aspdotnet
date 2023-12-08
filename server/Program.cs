using Microsoft.EntityFrameworkCore;
using server;
using server.Repository.IRepository;
using server.Repository;
using server.Profiles;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("HealthMonitoringDB")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
