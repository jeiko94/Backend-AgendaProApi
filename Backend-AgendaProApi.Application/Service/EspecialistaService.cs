using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Domain.Entities;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Backend_AgendaProApi.Application.Service
{
    public class EspecialistaService : IEspecialistaService
    {
        private readonly AgendaDbContext _db;

        public EspecialistaService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<EspecialistaResponseDto> CrearEspecialistaAsync(EspecialistaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                string.IsNullOrWhiteSpace(dto.Especialidad) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new Exception("Nombre, Especialidad y Email son obligatorios");
            }

            var email = dto.Email.Trim();

            var existeEmail = await _db.Especialistas.AnyAsync(e => e.Email == email);
            if (existeEmail)
            {
                throw new Exception("Ya existe un especialista registrado con ese email");
            }

            var especialista = new Especialista
            {
                Nombre = dto.Nombre.Trim(),
                Especialidad = dto.Especialidad.Trim(),
                Email = email,
                Estado = true
            };

            _db.Especialistas.Add(especialista);
            await _db.SaveChangesAsync();

            return new EspecialistaResponseDto
            {
                IdEspecialista = especialista.IdEspecialista,
                Nombre = especialista.Nombre,
                Especialidad = especialista.Especialidad,
                Email = especialista.Email,
                Estado = especialista.Estado
            };
        }

        public async Task<List<EspecialistaResponseDto>> ObtenerEspecialistasAsync()
        {
            return await _db.Especialistas
                .AsNoTracking()
                .OrderBy(e => e.IdEspecialista)
                .Select(e => new EspecialistaResponseDto
                {
                    IdEspecialista = e.IdEspecialista,
                    Nombre = e.Nombre,
                    Especialidad = e.Especialidad,
                    Email = e.Email,
                    Estado = e.Estado
                })
                .ToListAsync();
        }
    }
}
