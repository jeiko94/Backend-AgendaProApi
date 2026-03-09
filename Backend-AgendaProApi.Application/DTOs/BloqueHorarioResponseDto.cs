using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class BloqueHorarioResponseDto
    {
        public int IdBloqueHorario { get; set; }
        public int IdHorarios { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Disponibilidad { get; set; }
    }
}
