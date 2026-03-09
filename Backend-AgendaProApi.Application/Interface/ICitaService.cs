using Backend_AgendaProApi.Application.DTOs;


namespace Backend_AgendaProApi.Application.Interface
{
    public interface ICitaService
    {
        Task<CitaResponseDto> CrearCitaAsync(CitaCreateDto dto);
    }
}
