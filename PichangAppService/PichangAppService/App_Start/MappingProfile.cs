using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using AutoMapper;
using PichangAppDataAccess;
using PichangAppService.Dtos;
using PichangAppService.Dtos.POSTDtos;

namespace PichangAppService.App_Start
{
    public class MappingProfile:AutoMapper.Profile
    {
        public MappingProfile()
        {

            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<ComentarioEquipo, ComentarioEquipoDto>()
                .ForMember(dest => dest.NombreUsuario, opts => opts.MapFrom(src => src.Usuario.Nombre+" "+src.Usuario.Apellido))
                .ForMember(dest => dest.ImagenPerfilUrl, opts => opts.MapFrom(src => src.Usuario.ImagenPerfilUrl)).ReverseMap();

            CreateMap<ImagenEquipo, ImagenEquipoDto>().ReverseMap();
            CreateMap<ImagenCancha, ImagenCanchaDto>().ReverseMap();

            CreateMap<Horario, HorarioDto>().ReverseMap();

            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.Horas, opts => opts.MapFrom(src => src.Horario.HoraInicio.Hours + "-" + src.Horario.HoraFin.Hours))
                .ForMember(dest => dest.NombreCancha, opts => opts.MapFrom(src => src.Cancha.Nombre))
                .ForMember(dest => dest.Dia, opts => opts.MapFrom(src => SqlFunctions.DateName("dw", src.Fecha))).ReverseMap();

            CreateMap<Reserva, ReservaUsuarioDto>()
                .ForMember(dest => dest.Horas, opts => opts.MapFrom(src => src.Horario.HoraInicio.Hours + "-" + src.Horario.HoraFin.Hours))
                .ForMember(dest => dest.NombreCancha, opts => opts.MapFrom(src => src.Cancha.Nombre))
                .ForMember(dest => dest.Dia, opts => opts.MapFrom(src => SqlFunctions.DateName("dw", src.Fecha))).ReverseMap();


            CreateMap<Rol, RolDto>().ReverseMap();

            CreateMap<Servicio, ServicioDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<SkillEquipo, SkillEquipoDto>()
                .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => src.Skill.Nombre))
                .ForMember(dest => dest.ImagenSkillUrl, opts => opts.MapFrom(src => src.Skill.ImagenSkillUrl)).ReverseMap();
            CreateMap<TipoSuperficie, TipoSuperficieDto>().ReverseMap();

            CreateMap<Cancha, CanchaDto>()
                .ForMember(dest => dest.Servicios, opts => opts.MapFrom(src => src.Servicio))
                .ForMember(dest => dest.NombreDistrito, opts => opts.MapFrom(src => src.Distrito.Nombre))
                .ForMember(dest => dest.NombreTipoSuperficie, opts => opts.MapFrom(src => src.TipoSuperficie.Nombre))
                .ForMember(dest => dest.Imagenes, opts => opts.MapFrom(src => src.ImagenCancha))
                .ForMember(dest => dest.ReservasDisponibles, opts => opts.MapFrom(src => src.Reserva))
                .ForMember(dest => dest.Comentarios, opts => opts.MapFrom(src => src.ComentarioCancha)).ReverseMap();


            CreateMap<ComentarioCancha,ComentarioCanchaDto>()
                .ForMember(dest => dest.NombreUsuario, opts => opts.MapFrom(src => src.Usuario.Nombre + " " + src.Usuario.Apellido))
                .ForMember(dest => dest.ImagenPerfilUrl, opts => opts.MapFrom(src => src.Usuario.ImagenPerfilUrl)).ReverseMap();


            CreateMap<Equipo, EquipoDto>()
                .ForMember(dest => dest.Capitan, opts => opts.MapFrom(src => src.Usuario))  
                .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.SkillEquipo))
                .ForMember(dest => dest.Comentarios, opts => opts.MapFrom(src => src.ComentarioEquipo))
                .ForMember(dest => dest.Retos, opts => opts.MapFrom(src => src))
                .ForMember(dest => dest.Miembros, opts => opts.MapFrom(src => src.Usuario1))
                .ForMember(dest => dest.Imagenes, opts => opts.MapFrom(src => src.ImagenEquipo)).ReverseMap();

            CreateMap<Equipo, RetosEquipoAux>()
                .ForMember(dest => dest.Enviados, opts => opts.MapFrom(src => src.Reto))
                .ForMember(dest => dest.Recibidos, opts => opts.MapFrom(src => src.Reto1)).ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.NombreEquipo, opts => opts.MapFrom(src => src.Equipo1.Nombre))
                .ForMember(dest => dest.NombreRol, opts => opts.MapFrom(src => src.Rol.Nombre)).ReverseMap();
            CreateMap<Distrito, DistritoDto>().ReverseMap();

            CreateMap<Reto, RetoDto>()
                .ForMember(dest => dest.Horario, opts => opts.MapFrom(src => src.Reserva.Horario.HoraInicio+"-"+ src.Reserva.Horario.HoraFin))
                .ForMember(dest => dest.Cancha, opts => opts.MapFrom(src => src.Reserva.Cancha.Nombre))
                .ForMember(dest => dest.CanchaId, opts => opts.MapFrom(src => src.Reserva.Cancha.CanchaId))
                .ForMember(dest => dest.Distrito, opts => opts.MapFrom(src => src.Reserva.Cancha.Distrito.Nombre))
                .ForMember(dest => dest.EquipoRetado, opts => opts.MapFrom(src => src.Equipo1.Nombre))
                .ForMember(dest => dest.EquipoRetadoId, opts => opts.MapFrom(src => src.Equipo1.EquipoId))
                .ForMember(dest => dest.EquipoRetador, opts => opts.MapFrom(src => src.Equipo.Nombre))
                .ForMember(dest => dest.EquipoRetadorId, opts => opts.MapFrom(src => src.Equipo.EquipoId))
                .ForMember(dest => dest.FechaEncuentro, opts => opts.MapFrom(src => src.Reserva.Fecha)).ReverseMap();

            //POST
            CreateMap<Usuario, UsuarioPostDto>().ReverseMap();
            CreateMap<Equipo, EquipoPostDto>()
                .ForMember(dest => dest.CapitanId, opts => opts.MapFrom(src => src.UsuarioCapitanId)).ReverseMap();

            CreateMap<Equipo, EquipoPutDto>()
                .ForMember(dest => dest.CapitanId, opts => opts.MapFrom(src => src.UsuarioCapitanId)).ReverseMap();

            CreateMap<Reto, RetoPostDto>()
                .ForMember(x => x.UsuarioId, opt => opt.Ignore()).ReverseMap();
                
            CreateMap<Reserva, ReservaPutDto>().ReverseMap();


        }
    }
}