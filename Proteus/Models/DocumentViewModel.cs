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
    ///////////////////////////////////////
    ///  ΑΤΟΜΙΚΑ ΔΕΛΤΙΑ ΝΕΟ (13-09-2019)
    ///////////////////////////////////////

    public class adkGradesApousiesViewModel
    {
        public long ROW_ID { get; set; }
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public string ΜΑΘΗΜΑ { get; set; }
        public double? ΜΟΒΠ { get; set; }
        public double? ΜΟΤΕ { get; set; }
        public double? ΜΟΕΠ { get; set; }
        public double? ΜΟ { get; set; }
        public int? ΣΥΝΟΛΟ_ΩΡΕΣ { get; set; }
        public decimal? ΟΡΙΟ { get; set; }
        public int? ΑΠΟΥΣΙΕΣ { get; set; }
        public string ΑΠΟΤΕΛΕΣΜΑ { get; set; }
    }

    public class adkStudentDataViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public DateTime? ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Display(Name = "Τόπος γέννησης")]
        public string ΤΟΠΟΣ_ΓΕΝΝΗΣΗ { get; set; }

        [Display(Name = "Δημότης")]
        public string ΔΗΜΟΤΗΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εισόδου")]
        public DateTime? ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Πράξη εισόδου")]
        public string ΠΡΑΞΗ_ΕΙΣΟΔΟΥ { get; set; }

        [Display(Name = "Εξάμηνα σπουδών")]
        public int? ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ { get; set; }

        [Display(Name = "Διευθυντής")]
        public string SCHOOL_DIRECTOR { get; set; }

        [Display(Name = "Φύλο Δ/ντή")]
        public int? DIRECTOR_GENDER { get; set; }

        [Display(Name = "Φύλο")]
        public int? ΦΥΛΟ { get; set; }

        [Display(Name = "Φύλο")]
        public string ΦΥΛΟ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Είδος εξόδου")]
        public int? ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Είδος εξόδου")]
        public string ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Απαλλαγή πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ { get; set; }

        [Display(Name = "Ώρες απαλλαγής")]
        public int? ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "Διακοπή πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία διακοπής")]
        public DateTime? ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ { get; set; }

        [Display(Name = "Περάτωση πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ { get; set; }
    }

    public class adkStudentPraktikiViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? STUDENT_ID { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΙΕΚ { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public DateTime? ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public DateTime? ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες πρακτικής")]
        public int? ΩΡΕΣ { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }
    }

    public class AtomikoDeltioViewModel
    {
        public int ROW_ID { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΣΧΟΛΕΙΟ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Display(Name = "Μαθημα")]
        public string ΜΑΘΗΜΑ { get; set; }

        [Display(Name = "ΜΟΒΠ")]
        public int? ΜΟΒΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Ε.")]
        public decimal? ΒΕ { get; set; }

        [Display(Name = "ΜΟΤΕ")]
        public int? ΜΟΤΕ { get; set; }

        [Display(Name = "ΜΟΕΠ")]
        public int? ΜΟΕΠ { get; set; }

        [Display(Name = "Μ.Ο.")]
        public int? ΜΟ { get; set; }

        [Display(Name = "Πρ.Ώρ.")]
        public int? ΣΥΝΟΛΟ_ΩΡΕΣ { get; set; }

        [Display(Name = "Όριο")]
        public int? ΟΡΙΟ { get; set; }

        [Display(Name = "Απουσίες")]
        public int? ΑΠΟΥΣΙΕΣ { get; set; }

        [Display(Name = "Αποτέλεσμα")]
        public string ΑΠΟΤΕΛΕΣΜΑ { get; set; }

        [Display(Name = "Πλήθος μαθ.")]
        public int? LESSON_COUNT { get; set; }

        [Display(Name = "Άθροισμα βαθ.")]
        public int? GRADES_SUM { get; set; }

        [Display(Name = "Γ.Μ.Ο.")]
        public decimal? ΓΜΟ { get; set; }

        [Display(Name = "Γ.Μ.Ο.")]
        public string ΓΜΟ_FRACTION { get; set; }

        [Display(Name = "Εξ.")]
        public int? TERM_ID { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string TERM_TEXT { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "Περίοδος")]
        public int? PERIOD_ID { get; set; }

        [Display(Name = "Χ/Ε")]
        public int? ΧΕ_ΚΩΔ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SY_TEXT { get; set; }

        [Display(Name = "Εγγραφή είδος")]
        public int? ΕΓΓΡΑΦΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Φοίτηση")]
        public int? ΦΟΙΤΗΣΗ { get; set; }
    }

    public class StudentAtomikoDeltioViewModel
    {
        public int ΑΔΚ_ΚΩΔ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "IEK")]
        public int? ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public int? ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Παραδόθηκε")]
        public bool ΠΑΡΑΔΟΘΗΚΕ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }

    public class FoitisiDeltioViewModel
    {
        public int ROW_ID { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΣΧΟΛΕΙΟ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Display(Name = "Μαθημα")]
        public string ΜΑΘΗΜΑ { get; set; }

        [Display(Name = "ΜΟΒΠ")]
        public int? ΜΟΒΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Ε.")]
        public decimal? ΒΕ { get; set; }

        [Display(Name = "ΜΟΤΕ")]
        public int? ΜΟΤΕ { get; set; }

        [Display(Name = "ΜΟΕΠ")]
        public int? ΜΟΕΠ { get; set; }

        [Display(Name = "Μ.Ο.")]
        public int? ΜΟ { get; set; }

        [Display(Name = "Πρ.Ώρ.")]
        public int? ΣΥΝΟΛΟ_ΩΡΕΣ { get; set; }

        [Display(Name = "Όριο")]
        public int? ΟΡΙΟ { get; set; }

        [Display(Name = "Απουσίες")]
        public int? ΑΠΟΥΣΙΕΣ { get; set; }

        [Display(Name = "Αποτέλεσμα")]
        public string ΑΠΟΤΕΛΕΣΜΑ { get; set; }

        [Display(Name = "Πλήθος μαθ.")]
        public int? LESSON_COUNT { get; set; }

        [Display(Name = "Άθροισμα βαθ.")]
        public int? GRADES_SUM { get; set; }

        [Display(Name = "Γ.Μ.Ο.")]
        public decimal? ΓΜΟ { get; set; }

        [Display(Name = "Γ.Μ.Ο.")]
        public string ΓΜΟ_FRACTION { get; set; }

        [Display(Name = "Εξ.")]
        public int? TERM_ID { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string TERM_TEXT { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "Περίοδος")]
        public int? PERIOD_ID { get; set; }

        [Display(Name = "Χ/Ε")]
        public int? ΧΕ_ΚΩΔ { get; set; }

        [Display(Name = "Σχολ. έτος")]
        public string SY_TEXT { get; set; }

        [Display(Name = "Εγγραφή είδος")]
        public int? ΕΓΓΡΑΦΗ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Φοίτηση")]
        public int? ΦΟΙΤΗΣΗ { get; set; }
    }

    public class StudentFoitisiDeltioViewModel
    {
        public int ΑΔΚ_ΚΩΔ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "IEK")]
        public int? ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public int? ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Παραδόθηκε")]
        public bool ΠΑΡΑΔΟΘΗΚΕ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }

}