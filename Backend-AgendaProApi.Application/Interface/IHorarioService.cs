using Backend_AgendaProApi.Application.DTOs;


namespace Backend_AgendaProApi.Application.Interface
{
    public interface IHorarioService
    {
        Task<HorarioResponseDto> CrearHorarioAsync(HorarioCreateDto dto);
        Task<List<HorarioResponseDto>> ObtenerHorariosPorEspecialistaAsync(int idEspecialista);
    }
}
