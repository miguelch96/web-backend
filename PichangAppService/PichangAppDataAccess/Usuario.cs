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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.EquipoUsuario = new HashSet<EquipoUsuario>();
            this.Establecimiento = new HashSet<Establecimiento>();
        }
    
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<byte> RolId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipoUsuario> EquipoUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Establecimiento> Establecimiento { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
