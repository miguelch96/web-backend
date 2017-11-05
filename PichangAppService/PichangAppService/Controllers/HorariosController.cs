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
    public class HorariosController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var horarios = entities.Horario.ProjectTo<HorarioDto>().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, horarios);
            }
        }
    }
}
