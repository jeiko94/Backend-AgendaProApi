using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Application.Service;
using Backend_AgendaProApi.Application.Services;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IEspecialistaService, EspecialistaService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IBloqueHorarioService, BloqueHorarioService>();
builder.Services.AddScoped<ICitaService, CitaService>();

builder.Services.AddControllers();
// HEAD


// CORS (para React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("https://green-glacier-0eb0fc40f.4.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});





// Developomar
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

//HEAD

app.UseAuthorization();


// Activar CORS...
app.UseCors("AllowReact");
// Developomar

app.UseAuthorization();

app.MapControllers();

app.Run();