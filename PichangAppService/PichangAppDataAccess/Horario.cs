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
    
    public partial class Horario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horario()
        {
            this.HorarioEspacioDeportivo = new HashSet<HorarioEspacioDeportivo>();
        }
    
        public int HorarioId { get; set; }
        public System.DateTime HoraInicio { get; set; }
        public System.DateTime HoraFin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorarioEspacioDeportivo> HorarioEspacioDeportivo { get; set; }
    }
}
