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
    
    public partial class Deporte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deporte()
        {
            this.Equipo = new HashSet<Equipo>();
            this.EspacioDeportivo = new HashSet<EspacioDeportivo>();
        }
    
        public byte DeporteId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipo> Equipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EspacioDeportivo> EspacioDeportivo { get; set; }
    }
}