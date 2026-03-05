using Backend_AgendaProApi.Application.DTOs;

namespace Backend_AgendaProApi.Application.Interface
{
    public interface IAuthService
    {
        Task<string> RegistrarUsuarioAsync(UsuarioRegisterDto dto);
        Task<LoginResponseDto> LoginAsync(UsuarioLoginDto dto);
    }
}
