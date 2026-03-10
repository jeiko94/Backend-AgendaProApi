using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class CitaCreateDto
    {
        public int IdUsuario { get; set; }
        public int IdBloqueHorario { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Motivo { get; set; }
    }
}
