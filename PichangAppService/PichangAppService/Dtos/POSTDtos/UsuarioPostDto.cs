using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos.POSTDtos
{
    public class UsuarioPostDto
    {
        public Int32? UsuarioId { get; set; }
        public byte RolId { get; set; }
        public Int32? EquipoId { get; set; }
        public String Email { get; set; }
        public String Passwd { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String ImagenPerfilUrl { get; set; }
    }
}