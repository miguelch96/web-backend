using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class SkillEquipoDto
    {
        [JsonIgnore]
        public Int32 EquipoId { get; set; }   
        public byte SkillId { get; set; }
        public String Nombre { get; set; }
        public int Cantidad { get; set; }  
    }
}