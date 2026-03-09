using Backend_AgendaProApi.Application.DTOs;


namespace Backend_AgendaProApi.Application.Interface
{
    public interface IHorarioService
    {
        Task<HorarioResponseDto> CrearHorarioAsync(HorarioCreateDto dto);
        Task<List<HorarioResponseDto>> ObtenerHorariosPorEspecialistaAsync(int idEspecialista);
        Task<HorarioResponseDto> ObtenerHorarioPorIdAsync(int idHorario);
        Task<HorarioResponseDto> ActualizarHorarioAsync(int idHorario, HorarioUpdateDto dto);
        Task<HorarioResponseDto> CambiarEstadoHorarioAsync(int idHorario, bool estado);
    }
}
