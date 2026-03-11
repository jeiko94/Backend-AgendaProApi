using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Application.Service;
using Backend_AgendaProApi.Application.Services;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 📦 Base de datos
builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IEspecialistaService, EspecialistaService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IBloqueHorarioService, BloqueHorarioService>();
builder.Services.AddScoped<ICitaService, CitaService>();

builder.Services.AddControllers();

// CORS (para React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000",
                "http://localhost:8080"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Activar CORS...
app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();

app.Run();
