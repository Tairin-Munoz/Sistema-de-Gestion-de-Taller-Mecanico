using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TallerMecanico.Core.Interfaces;
using TallerMecanico.Infrastructure.Data;
using TallerMecanico.Infrastructure.Mappings;
using TallerMecanico.Infrastructure.Repositories;
using TallerMecanico.Services.Interfaces;
using TallerMecanico.Services.Services;
using TallerMecanico.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

// =========================
// 📌 Controllers + JSON
// =========================
builder.Services.AddControllers();

// =========================
// 📌 Swagger (DOCUMENTACIÓN PRO)
// =========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "API Taller Mecánico",
        Version = "v1",
        Description = "API REST para la gestión de propietarios, vehículos, servicios y órdenes de trabajo",
        Contact = new()
        {
            Name = "Equipo de desarrollo",
            Email = "taller@ucb.edu.bo"
        }
    });

    // 🔥 IMPORTANTE: leer comentarios XML (///)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // 🔥 Para anotaciones tipo SwaggerSchema
    options.EnableAnnotations();
});

// =========================
// 📌 Base de Datos
// =========================
builder.Services.AddDbContext<TallerMecanicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =========================
// 📌 Repositorios + UnitOfWork
// =========================
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// =========================
// 📌 Dapper (GET optimizados)
// =========================
builder.Services.AddScoped<IDapperContext, DapperContext>();

// =========================
// 📌 Services (lógica negocio)
// =========================
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IOrdenTrabajoService, OrdenTrabajoService>();

// =========================
// 📌 AutoMapper
// =========================
builder.Services.AddAutoMapper(typeof(VehiculoProfile));

// =========================
// 📌 FluentValidation
// =========================
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CrearVehiculoDtoValidator>();

// =========================
// 🚀 BUILD
// =========================
var app = builder.Build();

// =========================
// 📌 Swagger UI
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Taller Mecánico v1");
        options.RoutePrefix = "swagger"; // puedes poner "" si quieres que abra en la raíz
    });
}

// =========================
// 📌 Middlewares
// =========================
app.UseHttpsRedirection();
app.UseAuthorization();

// =========================
// 📌 Endpoints
// =========================
app.MapControllers();

// =========================
// 📌 Crear DB automáticamente
// =========================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TallerMecanicoContext>();
    db.Database.EnsureCreated();
}

app.Run();