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
    
    public partial class ΕΡΓΟΔΟΤΕΣ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ΕΡΓΟΔΟΤΕΣ()
        {
            this.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ = new HashSet<ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ>();
        }
    
        public int ΕΡΓΟΔΟΤΗΣ_ΚΩΔ { get; set; }
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }
        public Nullable<int> ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ { get; set; }
        public string ΤΗΛΕΦΩΝΑ { get; set; }
        public string EMAIL { get; set; }
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }
        public Nullable<int> ΙΕΚ { get; set; }
    
        public virtual SYS_SCHOOLS SYS_SCHOOLS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ> ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ { get; set; }
    }
}
