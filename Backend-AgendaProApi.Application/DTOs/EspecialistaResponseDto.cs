using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class EspecialistaResponseDto
    {
        public int IdEspecialista { get; set; }
        public string Nombre { get; set; } = null!;
        public string Especialidad { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
