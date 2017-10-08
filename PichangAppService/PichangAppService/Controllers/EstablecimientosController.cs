using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;

namespace PichangAppService.Controllers
{
    public class EstablecimientosController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var establecimientos = entities.Establecimiento.ProjectTo<EstablecimientoDto>().ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    StatusCode = HttpStatusCode.OK,
                    Establecimientos = establecimientos
                });
            }
        }


        public EstablecimientoDto Get(Int32 id)
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                var establecimiento = entities.Establecimiento.SingleOrDefault(x => x.EstablecimientoId == id);
                if (establecimiento == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                return Mapper.Map<Establecimiento, EstablecimientoDto>(establecimiento);
            }
        }
    }
}
