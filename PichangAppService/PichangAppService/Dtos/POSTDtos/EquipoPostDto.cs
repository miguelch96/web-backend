using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos.POSTDtos
{
    public class EquipoPostDto
    {
        public Int32? EquipoId { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String ImagenPortadaUrl { get; set; }
        public Int32 CapitanId { get; set; }
        public Int32 DistritoId { get; set; }
        public byte CategoriaId { get; set; }
        
    }
}