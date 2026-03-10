using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Domain.Entities;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend_AgendaProApi.Application.Service
{
    public class BloqueHorarioService : IBloqueHorarioService
    {
        private readonly AgendaDbContext _db;

        public BloqueHorarioService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<List<BloqueHorarioResponseDto>> GenerarBloquesAsync(GenerarBloquesDto dto)
        {
            var horario = await _db.Horarios
                .FirstOrDefaultAsync(h => h.IdHorarios == dto.IdHorarios);

            if (horario == null)
                throw new Exception("El horario no existe");

            if (!horario.Estado)
                throw new Exception("El horario está inactivo");

            if (dto.DuracionMinutos <= 0)
                throw new Exception("La duración de los bloques debe ser mayor que cero");

            var yaExistenBloques = await _db.BloquesHorario
                .AnyAsync(b => b.IdHorarios == dto.IdHorarios);

            if (yaExistenBloques)
                throw new Exception("Ya existen bloques generados para este horario");

            var bloques = new List<BloqueHorario>();
            var horaActual = horario.HoraInicio;

            while (horaActual.AddMinutes(dto.DuracionMinutos) <= horario.HoraFin)
            {
                var nuevoBloque = new BloqueHorario
                {
                    IdHorarios = horario.IdHorarios,
                    HoraInicio = horaActual,
                    HoraFin = horaActual.AddMinutes(dto.DuracionMinutos),
                    Disponibilidad = true
                };

                bloques.Add(nuevoBloque);
                horaActual = horaActual.AddMinutes(dto.DuracionMinutos);
            }

            if (!bloques.Any())
                throw new Exception("No fue posible generar bloques con la duración indicada");

            _db.BloquesHorario.AddRange(bloques);
            await _db.SaveChangesAsync();

            return bloques
                .Select(b => new BloqueHorarioResponseDto
                {
                    IdBloqueHorario = b.IdBloqueHorario,
                    IdHorarios = b.IdHorarios,
                    HoraInicio = b.HoraInicio,
                    HoraFin = b.HoraFin,
                    Disponibilidad = b.Disponibilidad
                })
                .ToList();
        }

        public async Task<List<BloqueHorarioResponseDto>> ObtenerBloquesPorHorarioAsync(int idHorario)
        {
            var horarioExiste = await _db.Horarios
                .AnyAsync(h => h.IdHorarios == idHorario);

            if (!horarioExiste)
                throw new Exception("El horario no existe");

            return await _db.BloquesHorario
                .AsNoTracking()
                .Where(b => b.IdHorarios == idHorario)
                .OrderBy(b => b.HoraInicio)
                .Select(b => new BloqueHorarioResponseDto
                {
                    IdBloqueHorario = b.IdBloqueHorario,
                    IdHorarios = b.IdHorarios,
                    HoraInicio = b.HoraInicio,
                    HoraFin = b.HoraFin,
                    Disponibilidad = b.Disponibilidad
                })
                .ToListAsync();
        }

        public async Task<BloqueHorarioResponseDto> CambiarDisponibilidadAsync(int idBloqueHorario, bool disponibilidad)
        {
            var bloque = await _db.BloquesHorario
                .FirstOrDefaultAsync(b => b.IdBloqueHorario == idBloqueHorario);

            if (bloque == null)
                throw new Exception("Bloque horario no encontrado");

            bloque.Disponibilidad = disponibilidad;

            await _db.SaveChangesAsync();

            return new BloqueHorarioResponseDto
            {
                IdBloqueHorario = bloque.IdBloqueHorario,
                IdHorarios = bloque.IdHorarios,
                HoraInicio = bloque.HoraInicio,
                HoraFin = bloque.HoraFin,
                Disponibilidad = bloque.Disponibilidad
            };
        }
    }
}
