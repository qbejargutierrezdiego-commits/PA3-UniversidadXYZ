using MicroservicioEstudiantes.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();