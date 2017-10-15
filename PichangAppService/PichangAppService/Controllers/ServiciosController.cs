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
    public class ServiciosController : ApiController
    {
        public IEnumerable<ServicioDto> Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                return entities.Servicio.ToList().Select(Mapper.Map<Servicio, ServicioDto>);
            }
        }
    }
}
