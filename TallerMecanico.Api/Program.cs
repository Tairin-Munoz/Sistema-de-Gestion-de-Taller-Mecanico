using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Mappings;
using TallerMecanico.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Services;


builder.Services.AddTransient<IVehiculoService, VehiculoService>();
builder.Services.AddTransient<IVehiculoService, VehiculoService>();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=.;Database=TallerMecanicoDb;Trusted_Connection=True;"));

builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

builder.Services.AddAutoMapper(typeof(VehiculoProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<VehiculoDtoValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();