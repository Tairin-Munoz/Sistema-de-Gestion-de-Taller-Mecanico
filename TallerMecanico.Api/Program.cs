using Microsoft.EntityFrameworkCore;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Infrastructure.Mappings;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Services;
using TallerMecanico.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TallerMecanicoContext>(options =>
    options.UseSqlServer("Server=.;Database=TallerMecanicoDb;Trusted_Connection=True;TrustServerCertificate=True;")
);

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IVehiculoService, VehiculoService>();

builder.Services.AddAutoMapper(typeof(VehiculoProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CrearVehiculoDtoValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();