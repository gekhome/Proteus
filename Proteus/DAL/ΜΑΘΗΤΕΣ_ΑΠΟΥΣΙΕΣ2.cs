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
    
    public partial class ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2
    {
        public int ΜΑ2_ΚΩΔ { get; set; }
        public Nullable<int> ΑΜΚ { get; set; }
        public Nullable<int> ΙΕΚ { get; set; }
        public Nullable<int> ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
        public Nullable<int> ΑΠΟΥΣΙΕΣ { get; set; }
    
        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }
}
