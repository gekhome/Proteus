//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proteus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYS_KLADOS_ENIAIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_KLADOS_ENIAIOS()
        {
            this.SYS_EIDIKOTITES = new HashSet<SYS_EIDIKOTITES>();
        }
    
        public int ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ { get; set; }
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_EIDIKOTITES> SYS_EIDIKOTITES { get; set; }
    }
}