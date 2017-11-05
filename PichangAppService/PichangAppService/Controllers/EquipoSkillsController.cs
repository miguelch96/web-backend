using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PichangAppDataAccess;
using PichangAppService.Dtos;

namespace PichangAppService.Controllers
{
    public class EquipoSkillsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                    if (equipo == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    var equipoDto= Mapper.Map<Equipo, EquipoDto>(equipo);

                    return Request.CreateResponse(HttpStatusCode.OK,new
                    {
                        equipoDto.EquipoId,
                        equipoDto.Skills
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
             