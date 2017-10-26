using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class ImagenEquipoDto
    {
        [JsonIgnore]
        public int ImagenEquipoId { get; set; }
        [JsonIgnore]
        public int EquipoId { get; set; }
        public String ImagenUrl { get; set; }
        public DateTime FechaSubida { get; set; }
        

    }
}