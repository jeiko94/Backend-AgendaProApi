using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AgendaProApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialistasController : ControllerBase
    {
        private readonly IEspecialistaService _especialistaService;

        public EspecialistasController(IEspecialistaService especialistaService)
        {
            _especialistaService = especialistaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEspecialista([FromBody] EspecialistaCreateDto dto)
        {
            try
            {
                var result = await _especialistaService.CrearEspecialistaAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEspecialistas()
        {
            var result = await _especialistaService.ObtenerEspecialistasAsync();
            return Ok(result);
        }
    }
}
