//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ComentarioEquipo
    {
        public int ComentarioEquipoId { get; set; }
        public int EquipoId { get; set; }
        public int UsuarioId { get; set; }
        public string Comentario { get; set; }
        public Nullable<byte> SkillId { get; set; }
        public decimal Calificacion { get; set; }
    
        public virtual Equipo Equipo { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}