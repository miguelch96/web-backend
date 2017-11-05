using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class CategoriaDto
    {
        public byte CategoriaId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
    }
}