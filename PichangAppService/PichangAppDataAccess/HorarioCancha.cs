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
    
    public partial class HorarioCancha
    {
        public int HorarioId { get; set; }
        public int CanchaId { get; set; }
        public string Estado { get; set; }
        public Nullable<int> ReservaId { get; set; }
    
        public virtual Cancha Cancha { get; set; }
        public virtual Horario Horario { get; set; }
        public virtual Reserva Reserva { get; set; }
    }
}