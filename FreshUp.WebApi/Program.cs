using Serilog;
using FluentValidation;
using FreshUp.WebApi.Extentions;
using FreshUp.Application.Mappings;
using FreshUp.Application.Interfaces;
using FreshUp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using FreshUp.Infrastructure.Repositories;
using FreshUp.WebApi.Middlewares;
using FreshUp.WebApi.Services;
using FreshUp.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extention for Swagger
builder.Services.ConfigureSwagger();

//PathRoot
PathHelper.WebRootPath = Path.GetFullPath("wwwroot");

//AppDbContext
builder.Services.AddDbContext<AppDbContext>(option
    => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

//Logger
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//ServiceCollection
builder.Services.AddServices();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

//JWT
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
