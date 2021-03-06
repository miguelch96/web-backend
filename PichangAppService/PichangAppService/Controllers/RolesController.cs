﻿using System;
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
    public class RolesController : ApiController
    {
        public IEnumerable<RolDto> Get()
        {
            using (PichangAppDBEntities entities = new PichangAppDBEntities())
            {
                return entities.Rol.ToList().Select(Mapper.Map<Rol, RolDto>);
            }
        }
    }
}
