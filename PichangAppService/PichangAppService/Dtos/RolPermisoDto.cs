using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class RolPermisoDto
    {
        public PermisoDto Permiso { get; set; }
        public String Estado { get; set; }
    }
}