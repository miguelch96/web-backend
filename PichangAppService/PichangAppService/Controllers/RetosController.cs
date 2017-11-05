using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using PichangAppService.Dtos.POSTDtos;
using PichangAppService.SwaggerExamples.EquipoExamples;
using Swashbuckle.Examples;

namespace PichangAppService.Controllers
{
    public class RetosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    var retos = entities.Reto.ProjectTo<RetoDto>().ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, retos);
                    
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }

        [HttpGet]
        public HttpResponseMessage Get(Int32 id)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    if (entities.Equipo.SingleOrDefault(x=>x.EquipoId== id) == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Equipo no encontrado");

                    var retosEnviados = entities.Reto.Where(x=>x.EquipoRetadorId== id).ProjectTo<RetoDto>().ToList();
                    var retosRecibidos = entities.Reto.Where(x=>x.EquipoRetadoId== id).ProjectTo<RetoDto>().ToList();


                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        EquipoId = id,
                        Enviados=retosEnviados,
                        Recibidos= retosRecibidos
                    });
                }
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(RetoPostDto), typeof(RetoPostRequestExample))]
        public HttpResponseMessage Post([FromBody] RetoPostDto retoDto)
        {
            try
            {
                using (PichangAppDBEntities entities = new PichangAppDBEntities())
                {
                    if (retoDto.EquipoRetadoId == retoDto.EquipoRetadorId)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"No es posible retarse a si mismo");
                    }
                    using (var Transaction = new TransactionScope())
                    {
                        var reto = Mapper.Map<RetoPostDto, Reto>(retoDto);
                        var reserva = new Reserva()
                        {
                            FechaSolicitud = DateTime.Now,
                            UsuarioId = retoDto.UsuarioId,
                            HorarioId = retoDto.HorarioId,
                            Fecha = retoDto.FechaEncuentro,
                            CanchaId = retoDto.CanchaId,
                            Estado = "ACT"
                        };

                        entities.Reserva.Add(reserva);
                        entities.SaveChanges();

                        reto.ReservaId = reserva.ReservaId;
                        reto.Estado = "Pendiente";
                        reto.FechaEnvio=DateTime.Now;
                        entities.Reto.Add(reto);
                        entities.SaveChanges();
                        Transaction.Complete();
                        retoDto.RetoId = reto.RetoId;
                        retoDto.ReservaId = reto.ReservaId;
                        var message = Request.CreateResponse(HttpStatusCode.Created, retoDto);
                        message.Headers.Location = new Uri(Request.RequestUri + "/" + retoDto.RetoId);
                        return message;
                    }
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
                    var reto = entities.Reto.SingleOrDefault(x => x.RetoId == id);
                    if (reto == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Reto not found");
                    }
                    reto.Estado = "Cancelado";
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
