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
    
    public partial class SYS_SCHOOLYEARS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_SCHOOLYEARS()
        {
            this.ΠΕΡΙΟΔΟΙ = new HashSet<ΠΕΡΙΟΔΟΙ>();
        }
    
        public int SY_ID { get; set; }
        public string SY_TEXT { get; set; }
        public Nullable<System.DateTime> SY_DATESTART { get; set; }
        public Nullable<System.DateTime> SY_DATEEND { get; set; }
        public Nullable<System.DateTime> DATE_ELSTAT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΠΕΡΙΟΔΟΙ> ΠΕΡΙΟΔΟΙ { get; set; }
    }
}