using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class EquipoUsuarioDto
    {
        public UsuarioDto Usuario { get; set; }
        public DateTime FechaUnion { get; set; }
    }
}