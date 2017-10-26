using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class EquipoUsuarioDto
    {
        public Int32 MiembroId { get; set; }    
        public String NombreMiembro { get; set; }
        public String ImagenPerfilUrl { get; set; }
        public DateTime FechaUnion { get; set; }

        [JsonIgnore]
        public Int32 EquipoId { get; set; }    
        
    }
}