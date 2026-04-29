using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Mappings;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Services;
using TallerMecanico.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TallerMecanicoContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TallerMecanicoDb;Trusted_Connection=True;")
);


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();


builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();


builder.Services.AddScoped<IServicioService, ServicioService>();


builder.Services.AddAutoMapper(typeof(VehiculoProfile));


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CrearVehiculoDtoValidator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

// Crear DB si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TallerMecanicoContext>();
    db.Database.EnsureCreated();
}

app.Run();