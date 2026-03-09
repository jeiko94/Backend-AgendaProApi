using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Backend_AgendaProApi.Domain.Entities;
using Backend_AgendaProApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Backend_AgendaProApi.Application.Service
{

    public class HorarioService : IHorarioService
    {
        private readonly AgendaDbContext _db;

        public HorarioService(AgendaDbContext db)
        {
            _db = db;
        }

        public async Task<HorarioResponseDto> CrearHorarioAsync(HorarioCreateDto dto)
        {
            var especialistaExiste = await _db.Especialistas
                .AnyAsync(e => e.IdEspecialista == dto.IdEspecialista);

            if (!especialistaExiste)
                throw new Exception("El especialista no existe");

            if (dto.DiaSemana < 1 || dto.DiaSemana > 7)
                throw new Exception("El día de la semana debe estar entre 1 y 7");

            if (dto.HoraInicio >= dto.HoraFin)
                throw new Exception("La hora de inicio debe ser menor que la hora de fin");

            var existeSuperposicion = await _db.Horarios.AnyAsync(h =>
                h.IdEspecialista == dto.IdEspecialista &&
                h.DiaSemana == dto.DiaSemana &&
                h.Estado == true &&
                dto.HoraInicio < h.HoraFin &&
                dto.HoraFin > h.HoraInicio);

            if (existeSuperposicion)
                throw new Exception("Ya existe un horario que se superpone para este especialista en ese día");

            var horario = new Horario
            {
                IdEspecialista = dto.IdEspecialista,
                DiaSemana = dto.DiaSemana,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                Estado = true
            };

            _db.Horarios.Add(horario);
            await _db.SaveChangesAsync();

            return new HorarioResponseDto
            {
                IdHorarios = horario.IdHorarios,
                IdEspecialista = horario.IdEspecialista,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = horario.Estado
            };
        }

        public async Task<List<HorarioResponseDto>> ObtenerHorariosPorEspecialistaAsync(int idEspecialista)
        {
            var especialistaExiste = await _db.Especialistas
                .AnyAsync(e => e.IdEspecialista == idEspecialista);

            if (!especialistaExiste)
                throw new Exception("El especialista no existe");

            return await _db.Horarios
                .AsNoTracking()
                .Where(h => h.IdEspecialista == idEspecialista)
                .OrderBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .Select(h => new HorarioResponseDto
                {
                    IdHorarios = h.IdHorarios,
                    IdEspecialista = h.IdEspecialista,
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin,
                    Estado = h.Estado
                })
                .ToListAsync();
        }

        public async Task<HorarioResponseDto> ObtenerHorarioPorIdAsync(int idHorario)
        {
            var horario = await _db.Horarios
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.IdHorarios == idHorario);

            if (horario == null)
                throw new Exception("Horario no encontrado");

            return new HorarioResponseDto
            {
                IdHorarios = horario.IdHorarios,
                IdEspecialista = horario.IdEspecialista,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = horario.Estado
            };
        }

        public async Task<HorarioResponseDto> ActualizarHorarioAsync(int idHorario, HorarioUpdateDto dto)
        {
            var horario = await _db.Horarios
                .FirstOrDefaultAsync(h => h.IdHorarios == idHorario);

            if (horario == null)
                throw new Exception("Horario no encontrado");

            var especialistaExiste = await _db.Especialistas
                .AnyAsync(e => e.IdEspecialista == dto.IdEspecialista);

            if (!especialistaExiste)
                throw new Exception("El especialista no existe");

            if (dto.DiaSemana < 1 || dto.DiaSemana > 7)
                throw new Exception("El día de la semana debe estar entre 1 y 7");

            if (dto.HoraInicio >= dto.HoraFin)
                throw new Exception("La hora de inicio debe ser menor que la hora de fin");

            var existeSuperposicion = await _db.Horarios.AnyAsync(h =>
                h.IdHorarios != idHorario &&
                h.IdEspecialista == dto.IdEspecialista &&
                h.DiaSemana == dto.DiaSemana &&
                h.Estado == true &&
                dto.HoraInicio < h.HoraFin &&
                dto.HoraFin > h.HoraInicio);

            if (existeSuperposicion)
                throw new Exception("Ya existe un horario que se superpone para este especialista en ese día");

            horario.IdEspecialista = dto.IdEspecialista;
            horario.DiaSemana = dto.DiaSemana;
            horario.HoraInicio = dto.HoraInicio;
            horario.HoraFin = dto.HoraFin;

            await _db.SaveChangesAsync();

            return new HorarioResponseDto
            {
                IdHorarios = horario.IdHorarios,
                IdEspecialista = horario.IdEspecialista,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = horario.Estado
            };
        }

        public async Task<HorarioResponseDto> CambiarEstadoHorarioAsync(int idHorario, bool estado)
        {
            var horario = await _db.Horarios
                .FirstOrDefaultAsync(h => h.IdHorarios == idHorario);

            if (horario == null)
                throw new Exception("Horario no encontrado");

            horario.Estado = estado;

            await _db.SaveChangesAsync();

            return new HorarioResponseDto
            {
                IdHorarios = horario.IdHorarios,
                IdEspecialista = horario.IdEspecialista,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = horario.Estado
            };
        }
    }
}
