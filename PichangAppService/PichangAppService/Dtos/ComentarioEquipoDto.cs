using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class ComentarioEquipoDto
    {
       

        public String Comentario { get; set; }
        public Decimal Calificacion { get; set; }
        public String NombreUsuario { get; set; }
        public String ImagenPerfilUrl { get; set; }
        public SkillDto Skill { get; set; }


        [JsonIgnore]
        public byte SkillId { get; set; }

        [JsonIgnore]
        public int ComentarioEquipoId { get; set; }
        [JsonIgnore]
        public int EquipoId { get; set; }

        [JsonIgnore]
        public int UsuarioId { get; set; }






    }
}