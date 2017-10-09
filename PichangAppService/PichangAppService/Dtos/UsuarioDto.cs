using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class UsuarioDto
    {
        public Int32 UsuarioId{ get; set; }
        public String Email{ get; set; }
        public String Passwd{ get; set; }
        public String Nombre{ get; set; }
        public String Apellido{ get; set; }
        public byte RolId { get; set; }
        public RolDto Rol { get; set; }
        //public IEnumerable<EquipoUsuarioDto> EquipoUsuario { get; set; }
    }
}