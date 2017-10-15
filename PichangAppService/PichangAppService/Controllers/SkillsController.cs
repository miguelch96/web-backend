using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;

namespace PichangAppService.Controllers
{
    public class SkillsController : ApiController
    {
        public IEnumerable<SkillDto> Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                return entities.Skill.ToList().Select(Mapper.Map<Skill, SkillDto>);
            }
        }
    }
}
