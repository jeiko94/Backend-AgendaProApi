using Backend_AgendaProApi.Application.DTOs;

namespace Backend_AgendaProApi.Application.Interface
{
    public interface IBloqueHorarioService
    {
        Task<List<BloqueHorarioResponseDto>> GenerarBloquesAsync(GenerarBloquesDto dto);
        Task<List<BloqueHorarioResponseDto>> ObtenerBloquesPorHorarioAsync(int idHorario);
        Task<BloqueHorarioResponseDto> CambiarDisponibilidadAsync(int idBloqueHorario, bool disponibilidad);
    }
}
