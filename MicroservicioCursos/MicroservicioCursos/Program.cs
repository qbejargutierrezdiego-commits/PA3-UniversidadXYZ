using MicroservicioCursos.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();