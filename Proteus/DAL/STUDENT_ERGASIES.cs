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
    
    public partial class STUDENT_ERGASIES
    {
        public int ERGASIA_ID { get; set; }
        public Nullable<int> STUDENT_ID { get; set; }
        public Nullable<int> IEK { get; set; }
        public Nullable<int> TMIMA_ID { get; set; }
        public Nullable<int> EIDIKOTITA_ID { get; set; }
        public Nullable<int> TERM_ID { get; set; }
        public Nullable<int> ERGASIA_TYPE { get; set; }
        public string LESSON_TEXT { get; set; }
        public Nullable<decimal> GRADE { get; set; }
    
        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }
}