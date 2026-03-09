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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEspecialistaPorId(int id)
        {
            try
            {
                var result = await _especialistaService.ObtenerEspecialistaPorIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEspecialista(int id, [FromBody] EspecialistaUpdateDto dto)
        {
            try
            {
                var result = await _especialistaService.ActualizarEspecialistaAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
