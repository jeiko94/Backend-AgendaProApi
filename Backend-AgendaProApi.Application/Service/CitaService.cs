using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Domain.Entities;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend_AgendaProApi.Application.Service
{

    public class CitaService : ICitaService
    {
        private readonly AgendaDbContext _db;

        public CitaService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<CitaResponseDto> CrearCitaAsync(CitaCreateDto dto)
        {
            var usuarioExiste = await _db.Usuarios
                .AnyAsync(u => u.IdUsuario == dto.IdUsuario);

            if (!usuarioExiste)
                throw new Exception("El usuario no existe");

            var bloque = await _db.BloquesHorario
                .FirstOrDefaultAsync(b => b.IdBloqueHorario == dto.IdBloqueHorario);

            if (bloque == null)
                throw new Exception("El bloque horario no existe");

            if (!bloque.Disponibilidad)
                throw new Exception("El bloque horario no está disponible");

            if (string.IsNullOrWhiteSpace(dto.Motivo))
                throw new Exception("El motivo es obligatorio");

            var existeCitaActiva = await _db.Citas
                .AnyAsync(c => c.IdBloqueHorario == dto.IdBloqueHorario && c.Estado == true);

            if (existeCitaActiva)
                throw new Exception("Ya existe una cita activa para este bloque horario");

            var cita = new Cita
            {
                IdUsuario = dto.IdUsuario,
                IdBloqueHorario = dto.IdBloqueHorario,
                Fecha = dto.Fecha,
                Motivo = dto.Motivo.Trim(),
                Estado = true,
                FechaCreacion = DateTime.UtcNow
            };

            _db.Citas.Add(cita);

            bloque.Disponibilidad = false;

            await _db.SaveChangesAsync();

            return new CitaResponseDto
            {
                IdCitas = cita.IdCitas,
                IdUsuario = cita.IdUsuario,
                IdBloqueHorario = cita.IdBloqueHorario,
                Fecha = cita.Fecha,
                Motivo = cita.Motivo,
                Estado = cita.Estado,
                FechaCreacion = cita.FechaCreacion
            };
        }

        public async Task<List<CitaResponseDto>> ObtenerCitasPorUsuarioAsync(int idUsuario)
        {
            var usuarioExiste = await _db.Usuarios
                .AnyAsync(u => u.IdUsuario == idUsuario);

            if (!usuarioExiste)
                throw new Exception("El usuario no existe");

            return await _db.Citas
                .AsNoTracking()
                .Where(c => c.IdUsuario == idUsuario)
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.FechaCreacion)
                .Select(c => new CitaResponseDto
                {
                    IdCitas = c.IdCitas,
                    IdUsuario = c.IdUsuario,
                    IdBloqueHorario = c.IdBloqueHorario,
                    Fecha = c.Fecha,
                    Motivo = c.Motivo,
                    Estado = c.Estado,
                    FechaCreacion = c.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task<CitaResponseDto> ObtenerCitaPorIdAsync(int idCita)
        {
            var cita = await _db.Citas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCitas == idCita);

            if (cita == null)
                throw new Exception("Cita no encontrada");

            return new CitaResponseDto
            {
                IdCitas = cita.IdCitas,
                IdUsuario = cita.IdUsuario,
                IdBloqueHorario = cita.IdBloqueHorario,
                Fecha = cita.Fecha,
                Motivo = cita.Motivo,
                Estado = cita.Estado,
                FechaCreacion = cita.FechaCreacion
            };
        }
    }
}
