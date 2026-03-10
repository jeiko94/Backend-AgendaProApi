using Backend_AgendaProApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.Interface
{
    public interface IEspecialistaService
    {
        Task<EspecialistaResponseDto> CrearEspecialistaAsync(EspecialistaCreateDto dto);
        Task<List<EspecialistaResponseDto>> ObtenerEspecialistasAsync();
        Task<EspecialistaResponseDto> ObtenerEspecialistaPorIdAsync(int idEspecialista);
        Task<EspecialistaResponseDto> ActualizarEspecialistaAsync(int idEspecialista, EspecialistaUpdateDto dto);
        Task<EspecialistaResponseDto> CambiarEstadoEspecialistaAsync(int idEspecialista, bool estado);
    }
}
