using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AgendaProApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorariosController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearHorario([FromBody] HorarioCreateDto dto)
        {
            try
            {
                var result = await _horarioService.CrearHorarioAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("especialista/{idEspecialista}")]
        public async Task<IActionResult> ObtenerHorariosPorEspecialista(int idEspecialista)
        {
            try
            {
                var result = await _horarioService.ObtenerHorariosPorEspecialistaAsync(idEspecialista);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerHorarioPorId(int id)
        {
            try
            {
                var result = await _horarioService.ObtenerHorarioPorIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarHorario(int id, [FromBody] HorarioUpdateDto dto)
        {
            try
            {
                var result = await _horarioService.ActualizarHorarioAsync(id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstadoHorario(int id, [FromQuery] bool estado)
        {
            try
            {
                var result = await _horarioService.CambiarEstadoHorarioAsync(id, estado);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
