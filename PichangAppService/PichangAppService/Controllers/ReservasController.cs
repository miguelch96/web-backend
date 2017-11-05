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
    public class ReservasController : ApiController
    {
        public IEnumerable<ReservaDto> Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                return entities.Reserva.ToList().Select(Mapper.Map<Reserva, ReservaDto>);
            }
        }

        public IHttpActionResult Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var reserva = entities.Reserva.SingleOrDefault(x => x.ReservaId == id);
                if (reserva == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Ok(Mapper.Map<Reserva, ReservaDto>(reserva));
            }
        }

        [Route("~/api/reservas/cancha")]
        public HttpResponseMessage GetReservasLibres(Int32 canchaId, String fecha = null, Int32? horarioId = null)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = entities.Cancha.SingleOrDefault(x => x.CanchaId == canchaId);
                    if (cancha == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cancha no encontrado");

                    IQueryable<Reserva> reservasDisponibles = entities.Reserva.Where(x => x.Estado.Equals("Libre") && x.CanchaId == canchaId);


                    if (fecha != null)
                    {
                        DateTime fechaEscogida = DateTime.Parse(fecha);
                        reservasDisponibles = reservasDisponibles.Where(x => x.Fecha.Equals(fechaEscogida));
                    }

                    if (horarioId != null)
                    {
                        reservasDisponibles = reservasDisponibles.Where(x => x.HorarioId == horarioId);
                    }

                    var reservasDtos = reservasDisponibles.ProjectTo<ReservaDto>().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        reservasDisponibles = reservasDtos
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
