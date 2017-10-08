using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class RolDto
    {
        public Int32 RolId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public IEnumerable<RolPermisoDto> RolPermiso { get; set; }
    }
}