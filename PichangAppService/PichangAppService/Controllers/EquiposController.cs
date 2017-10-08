using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;

namespace PichangAppService.Controllers
{
    public class EquiposController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var equipos= entities.Equipo.ProjectTo<EquipoDto>().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    StatusCode = HttpStatusCode.OK,
                    Equipos = equipos
                });
            }
        }


        public IHttpActionResult Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var equipo = entities.Equipo.SingleOrDefault(x => x.EquipoId == id);
                if (equipo == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Ok(Mapper.Map<Equipo, EquipoDto>(equipo));
            }
        }

    }
}
