﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PichangAppDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PichangAppDBEntities : DbContext
    {
        public PichangAppDBEntities()
            : base("name=PichangAppDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cancha> Cancha { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<ComentarioCancha> ComentarioCancha { get; set; }
        public virtual DbSet<ComentarioEquipo> ComentarioEquipo { get; set; }
        public virtual DbSet<Distrito> Distrito { get; set; }
        public virtual DbSet<Equipo> Equipo { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<HorarioCancha> HorarioCancha { get; set; }
        public virtual DbSet<ImagenCancha> ImagenCancha { get; set; }
        public virtual DbSet<ImagenEquipo> ImagenEquipo { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Reto> Reto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<SkillEquipo> SkillEquipo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoSuperficie> TipoSuperficie { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
