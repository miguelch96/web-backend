using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;

namespace PichangAppService.App_Start
{
    public class MappingProfile:AutoMapper.Profile
    {
        public MappingProfile()
        {

         
            CreateMap<Deporte, DeporteDto>().ReverseMap();
            
           
            CreateMap<HorarioCancha, HorarioCanchaDto>().ReverseMap();
            CreateMap<Horario, HorarioDto>().ReverseMap();
            CreateMap<Permiso, PermisoDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();


            CreateMap<Rol, RolDto>()
                .ForMember(dest => dest.Permisos, opts => opts.MapFrom(src => src.RolPermiso.Select(x => x.Permiso))).ReverseMap();


            CreateMap<RolPermiso, RolPermisoDto>().ReverseMap();
            CreateMap<Servicio, ServicioDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<SkillEquipo, SkillEquipoDto>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => src.Skill.Nombre)).ReverseMap();
            CreateMap<TipoSuperficie, TipoSuperficieDto>().ReverseMap();

            CreateMap<EstablecimientoServicio, EstablecimientoServicioDto>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => src.Servicio.Nombre)).ReverseMap();

            CreateMap<Cancha, CanchaDto>()
                .ForMember(dest => dest.Establecimiento, opts => opts.MapFrom(src => src.Establecimiento))
                .ForMember(dest => dest.Deporte, opts => opts.MapFrom(src => src.Deporte))
                .ForMember(dest => dest.TipoSuperficie, opts => opts.MapFrom(src => src.TipoSuperficie)).ReverseMap();

            CreateMap<EquipoUsuario, EquipoUsuarioDto>().ReverseMap();

            CreateMap<Equipo, EquipoDto>()
                .ForMember(dest => dest.Capitan, opts => opts.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Miembros, opts => opts.MapFrom(src => src.EquipoUsuario.Select(x=>x.Usuario)))
                .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.SkillEquipo.Select(x=>x.Skill)))
                .ForMember(dest => dest.CapitanId, opts => opts.MapFrom(src => src.UsuarioCapitanId)).ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Distrito, DistritoDto>().ReverseMap();

            CreateMap<Establecimiento, EstablecimientoDto>()
                .ForMember(dest => dest.Distrito, opts => opts.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Propietario, opts => opts.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Servicios, opts => opts.MapFrom(src => src.EstablecimientoServicio.Select(x=>x.Servicio)))
                .ForMember(dest => dest.PropietarioId, opts => opts.MapFrom(src => src.UsuarioPropietarioId)).ReverseMap();
      




        }
    }
}