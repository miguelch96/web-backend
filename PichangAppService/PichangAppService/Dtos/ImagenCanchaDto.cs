using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PichangAppService.Dtos
{
    public class ImagenCanchaDto
    {
        [JsonIgnore]
        public int ImagenCanchaId { get; set; }
        [JsonIgnore]
        public int CanchaId { get; set; }
        public String ImagenUrl { get; set; }
        public DateTime FechaSubida { get; set; }
    }
}