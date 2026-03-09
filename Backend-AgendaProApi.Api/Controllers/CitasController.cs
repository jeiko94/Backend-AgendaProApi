using Backend_AgendaProApi.Application.DTOs;
using Backend_AgendaProApi.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend_AgendaProApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCita([FromBody] CitaCreateDto dto)
        {
            try
            {
                var result = await _citaService.CrearCitaAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerCitasPorUsuario(int idUsuario)
        {
            try
            {
                var result = await _citaService.ObtenerCitasPorUsuarioAsync(idUsuario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCitaPorId(int id)
        {
            try
            {
                var result = await _citaService.ObtenerCitaPorIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
