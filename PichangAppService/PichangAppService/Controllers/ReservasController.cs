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
using PichangAppService.Dtos.POSTDtos;

namespace PichangAppService.Controllers
{
    public class ReservasController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(String estado=null)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                IQueryable<Reserva> reservasInDb = entities.Reserva;
                if (estado != null)
                {
                    reservasInDb = reservasInDb.Where(x => x.Estado.Equals(estado));
                }

                var reservasDto = reservasInDb.ProjectTo<ReservaDto>().ToList();
                return Request.CreateResponse(HttpStatusCode.OK,new
                {
                   Reservas=reservasDto
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var reserva = entities.Reserva.ProjectTo<ReservaDto>().SingleOrDefault(x => x.ReservaId == id);
                if (reserva == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontro la reserva");
              
                return Request.CreateResponse(HttpStatusCode.OK, reserva);
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

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] ReservaPutDto reservaDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var reserva = entities.Reserva.SingleOrDefault(x => x.ReservaId == id);
                    if (reserva == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Reserva no encontrado");
                    }

                    reserva.ReservaId = reservaDto.ReservaId;
                    reserva.FechaSolicitud = DateTime.Now;
                    reserva.Estado = reservaDto.Estado;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, reservaDto);
                }
            }

            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var reserva = entities.Reserva.SingleOrDefault(x => x.ReservaId == id);
                    if (reserva == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Reserva no encontrada");
                    }
                    reserva.UsuarioId = null;
                    reserva.FechaSolicitud = null;
                    reserva.Estado = "Libre";
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}

