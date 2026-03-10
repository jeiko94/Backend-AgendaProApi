using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AgendaProApi.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BloquesHorarioController : ControllerBase
    {
        private readonly IBloqueHorarioService _bloqueHorarioService;

        public BloquesHorarioController(IBloqueHorarioService bloqueHorarioService)
        {
            _bloqueHorarioService = bloqueHorarioService;
        }

        [HttpPost("generar")]
        public async Task<IActionResult> GenerarBloques([FromBody] GenerarBloquesDto dto)
        {
            try
            {
                var result = await _bloqueHorarioService.GenerarBloquesAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("horario/{idHorario}")]
        public async Task<IActionResult> ObtenerBloquesPorHorario(int idHorario)
        {
            try
            {
                var result = await _bloqueHorarioService.ObtenerBloquesPorHorarioAsync(idHorario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/disponibilidad")]
        public async Task<IActionResult> CambiarDisponibilidad(int id, [FromQuery] bool disponibilidad)
        {
            try
            {
                var result = await _bloqueHorarioService.CambiarDisponibilidadAsync(id, disponibilidad);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
