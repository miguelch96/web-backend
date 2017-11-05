using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PichangAppDataAccess;
using PichangAppService.Dtos.POSTDtos;

namespace PichangAppService.Dtos
{
    public class UsuarioDto
    {
        public Int32? UsuarioId { get; set; }
        public String Email { get; set; }
        public String Passwd { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String ImagenPerfilUrl { get; set; }
        public Int32? EquipoId { get; set; }
        public String NombreEquipo { get; set; }
        public String NombreRol { get; set; }
    }
}