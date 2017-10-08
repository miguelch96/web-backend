using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class EquipoDto
    {
        public Int32 EquipoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public DeporteDto Deporte { get; set; }
        public UsuarioDto Capitan { get; set; }
        public IEnumerable<EquipoUsuarioDto> Miembros { get; set; }
        public IEnumerable<SkillEquipoDto> SkillEquipo { get; set; }


    }
}