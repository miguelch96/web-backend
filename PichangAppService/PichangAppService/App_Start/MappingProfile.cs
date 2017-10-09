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
            
            CreateMap<EquipoUsuario, EquipoUsuarioDto>().ReverseMap();
            
            CreateMap<EstablecimientoServicio, EstablecimientoServicioDto>().ReverseMap();
            CreateMap<HorarioCancha, HorarioCanchaDto>().ReverseMap();
            CreateMap<Horario, HorarioDto>().ReverseMap();
            CreateMap<Permiso, PermisoDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<RolPermiso, RolPermisoDto>().ReverseMap();
            CreateMap<Servicio, ServicioDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<SkillEquipo, SkillEquipoDto>().ReverseMap();
            CreateMap<TipoSuperficie, TipoSuperficieDto>().ReverseMap();



            CreateMap<Cancha, CanchaDto>()
                .ForMember(dest => dest.Establecimiento, opts => opts.MapFrom(src => src.Establecimiento))
                .ForMember(dest => dest.Deporte, opts => opts.MapFrom(src => src.Deporte))
                .ForMember(dest => dest.TipoSuperficie, opts => opts.MapFrom(src => src.TipoSuperficie));

            CreateMap<Equipo, EquipoDto>()
                .ForMember(dest => dest.Capitan, opts => opts.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Miembros, opts => opts.MapFrom(src => src.EquipoUsuario))
                .ForMember(dest => dest.CapitanId, opts => opts.MapFrom(src => src.UsuarioCapitanId)).ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Distrito, DistritoDto>().ReverseMap();

            CreateMap<Establecimiento, EstablecimientoDto>()
                .ForMember(dest => dest.Distrito, opts => opts.MapFrom(src => src.Distrito))
                .ForMember(dest => dest.Propietario, opts => opts.MapFrom(src => src.Usuario))
                .ForMember(dest => dest.Servicios, opts => opts.MapFrom(src => src.EstablecimientoServicio))
                .ForMember(dest => dest.PropietarioId, opts => opts.MapFrom(src => src.UsuarioPropietarioId)).ReverseMap();
            //.ForMember(dest => dest.Canchas, opts => opts.MapFrom(src => src.Cancha));





        }
    }
}