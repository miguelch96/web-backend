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
    public class CanchasController : ApiController
    {
        public HttpResponseMessage Get(String distritoId=null,Int32? horarioId=null,DateTime? fecha=null)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var canchas = entities.Cancha.Where(x => x.Estado == "ACT").ProjectTo<CanchaDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        Canchas=canchas
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
        }


        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = entities.Cancha.ProjectTo<CanchaDto>().SingleOrDefault(x => x.CanchaId == id);

                    if (cancha == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cancha con Id= " + id + " no encontrado");
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        Cancha = cancha
                    });
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);

            }
        }


        [HttpPost]
        public HttpResponseMessage Post([FromBody] CanchaDto canchaDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = Mapper.Map<CanchaDto, Cancha>(canchaDto);
                    cancha.Estado = "ACT";
                    entities.Cancha.Add(cancha);
                    entities.SaveChanges();
                    canchaDto.CanchaId = cancha.CanchaId;

                    var message = Request.CreateResponse(HttpStatusCode.Created, canchaDto);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + cancha.CanchaId);
                    return message;
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] CanchaDto canchaDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = entities.Cancha.SingleOrDefault(x => x.CanchaId == id);
                    if (cancha == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Cancha no encontrada");
                    }

                    Mapper.Map(canchaDto, cancha);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, canchaDto);
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
                    var canchaInDb = entities.Cancha.Where(x=>x.Estado=="ACT").SingleOrDefault(x => x.CanchaId == id);
                    if (canchaInDb == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Cancha no encontrada");
                    }

                    canchaInDb.Estado = "INA";
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }


        [Route("~/api/canchas/{canchaId:int}/reservaslibres")]
        public HttpResponseMessage GetReservasDisponibles(Int32 canchaId,String fecha=null,Int32? horarioId=null)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var cancha = entities.Cancha.SingleOrDefault(x => x.CanchaId == canchaId);
                    if (cancha == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Cancha no encontrado");

                    IQueryable<Reserva> reservasDisponibles = entities.Reserva.Where(x=>x.Estado.Equals("Libre") && x.CanchaId==canchaId);
                       

                    if (fecha != null)
                    {
                        DateTime fechaEscogida = DateTime.Parse(fecha);
                        reservasDisponibles = reservasDisponibles.Where(x => x.Fecha.Equals(fechaEscogida));
                    }

                    if (horarioId!=null)
                    {
                        reservasDisponibles = reservasDisponibles.Where(x => x.HorarioId == horarioId);
                    }

                    var reservasDtos = reservasDisponibles.ProjectTo<ReservaDto>().ToList();
             

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        cancha.CanchaId,
                        reservasDisponibles=reservasDtos,
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
