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
    
    public partial class ComentarioCancha
    {
        public int ComentarioCanchaId { get; set; }
        public int UsuarioId { get; set; }
        public string Comentario { get; set; }
        public decimal Calificacion { get; set; }
        public int CanchaId { get; set; }
    
        public virtual Cancha Cancha { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}