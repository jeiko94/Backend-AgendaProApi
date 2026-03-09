using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class CitaResponseDto
    {
        public int IdCitas { get; set; }
        public int IdUsuario { get; set; }
        public int IdBloqueHorario { get; set; }
        public DateOnly Fecha { get; set; }
        public string Motivo { get; set; } = null!;
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
