using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Proteus.Models;
using Proteus.DAL;
using System.Web.Mvc;

namespace Proteus.Models
{
    public class sqlGradesSourceViewModel
    {
        public int ΜΒ_ΚΩΔ { get; set; }
        public int ΙΕΚ { get; set; }
        public string ΠΕΡΙΟΔΟΣ { get; set; }
        public string TERM { get; set; }
        public int STUDENT_ID { get; set; }
        public int ΑΜΚ { get; set; }
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public string LESSON_TEXT { get; set; }
        public int? ΘΕ { get; set; }
        public float? ΠΡ { get; set; }
        public float? ΤΕ { get; set; }
        public float? ΕΠ { get; set; }
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public int EIDIKOTITA_ID { get; set; }
        public int LESSON_ID { get; set; }
        public int TERM_ID { get; set; }
    }

    public class StudentGradesReportViewModel
    {
        public int GRADES_ID { get; set; }

        [Display(Name = "Σχολείο")]
        public int? IEK { get; set; }

        [Display(Name = "Περίοδος")]
        public string PERIOD_TEXT { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? STUDENT_ID { get; set; }

        [Display(Name = "AMK")]
        public int? AMK { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string FULLNAME { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string TERM_TEXT { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_TEXT { get; set; }

        [Display(Name = "Τμήμα")]
        public int? TMIMA_ID { get; set; }

        [Display(Name = "Ειδικότητα κωδ.")]
        public int? EIDIKOTITA_ID { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int? TERM_ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "ΠΡ(Θ)")]
        public decimal? ΠΡΘ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "ΠΡ(Ε)")]
        public decimal? ΠΡΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "ΤΕ(Θ)")]
        public decimal? ΤΕΘ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "ΤΕ(Ε)")]
        public decimal? ΤΕΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "ΕΠ")]
        public decimal? ΕΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "ΠΡ")]
        public decimal? ΠΡΟΟΔΟΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "ΒΕ")]
        public decimal? ΕΡΓΑΣΙΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "ΤΕ")]
        public decimal? ΕΞΕΤΑΣΗ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "ΤΒ")]
        public decimal? ΤΕΛΙΚΟΣ { get; set; }
    }

}