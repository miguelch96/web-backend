using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using PichangAppDataAccess;

namespace PichangAppService.Dtos
{
    public class CanchaDto
    {
        public Int32 CanchaId{ get; set; }
        public String Nombre{ get; set; }
        public Decimal Precio{ get; set; }

        public byte EstablecimientoId;
        public EstablecimientoDto Establecimiento { get; set; }

        public byte DeporteId;
        public DeporteDto Deporte { get; set; }

        public byte TipoSuperficieId;
        public TipoSuperficieDto TipoSuperficie { get; set; }
    }
}