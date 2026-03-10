using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_AgendaProApi.Application.DTOs
{
    public class RolResponseDto
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
