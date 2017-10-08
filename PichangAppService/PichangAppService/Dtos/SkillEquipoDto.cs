using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PichangAppService.Dtos
{
    public class SkillEquipoDto
    {
        public SkillDto Skill { get; set; }
        public Decimal Promedio { get; set; }
    }
}