﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class PermisoDto
    {
        public Int32 PermisoId { get; set; }
        public String Nombre { get; set; }
        public String Codigo { get; set; }
        public String Descripcion { get; set; }
        public String Estado { get; set; }
    }
}