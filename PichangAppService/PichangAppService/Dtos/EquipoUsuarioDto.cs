using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class EquipoUsuarioDto
    {
        public Int32 UsuarioId { get; set; }    
        public UsuarioDto Usuario { get; set; }

        [JsonIgnore]
        public Int32 EquipoId { get; set; }    
        public DateTime? FechaUnion { get; set; }
    }
}