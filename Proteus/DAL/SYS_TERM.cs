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
    
    public partial class SYS_TERM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SYS_TERM()
        {
            this.LESSONS_IEK = new HashSet<LESSONS_IEK>();
        }
    
        public int TERM_ID { get; set; }
        public string TERM { get; set; }
        public string TERM_TEXT { get; set; }
        public string TERM_WORD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LESSONS_IEK> LESSONS_IEK { get; set; }
    }
}
