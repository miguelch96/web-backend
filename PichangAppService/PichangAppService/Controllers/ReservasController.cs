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
    public class ReservasController : ApiController
    {
        public IEnumerable<ReservaDto> Get()
        {
            using (PichangAppEntities entities = new PichangAppEntities())
            {
                return entities.Reserva.ToList().Select(Mapper.Map<Reserva, ReservaDto>);
            }
        }


        public IHttpActionResult Get(Int32 id)
        {
            using (PichangAppEntities entities = new PichangAppEntities())
            {
                var reserva = entities.Reserva.SingleOrDefault(x => x.ReservaId == id);
                if (reserva == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Ok(Mapper.Map<Reserva, ReservaDto>(reserva));
            }
        }
    }
}
