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
    public class StudentApousies2ViewModel
    {
        public int ΜΑ2_ΚΩΔ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Range(1, 1000, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 1000")]
        [Display(Name = "Απουσίες")]
        public Nullable<int> ΑΠΟΥΣΙΕΣ { get; set; }

        [Display(Name = "Τμήμα")]
        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class StudentApousiesViewModel
    {
        public int ΜΑ_ΚΩΔ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "IEK")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public System.DateTime ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Μάθημα")]
        public int ΚΩΔ_ΜΑΘΗΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ώρες")]
        public Nullable<int> ΩΡΕΣ_ΑΠΟΥΣΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }

    public class StudentGradesViewModel
    {
        public int ΜΒ_ΚΩΔ { get; set; }

        [Display(Name = "AMK")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "IEK")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Μάθημα")]
        public int ΚΩΔ_ΜΑΘΗΜΑ { get; set; }

        [Display(Name = "Τμήμα")]
        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Π.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]  
        [Display(Name = "Β.Τ.Ε.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΤΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.ΕΠ.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΕΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "Μ.Ο.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΜΟ { get; set; }

        [Display(Name = "Χαρακτηρισμός")]
        public Nullable<int> ΧΑΡΑΚΤΗΡΙΣΜΟΣ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

        public virtual LESSONS_IEK LESSONS_IEK { get; set; }

    }

    public class ErgasiaGradeViewModel
    {
        public int ERGASIA_ID { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? STUDENT_ID { get; set; }

        [Display(Name = "Σχολείο")]
        public int? IEK { get; set; }

        [Display(Name = "Τμήμα")]
        public int? TMIMA_ID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int? EIDIKOTITA_ID { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int? TERM_ID { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Μάθημα")]
        public string LESSON_TEXT { get; set; }

        [Display(Name = "Βαθμός")]
        public decimal? GRADE { get; set; }

        [Display(Name = "Είδος εργασίας")]
        public int ERGASIA_TYPE { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }
    }


    public class StudentGradesDetailViewModel
    {
        public int ΜΒ_ΚΩΔ { get; set; }

        [Display(Name = "IEK")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "Περίοδος κωδ.")]
        public int TERM_ID { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string TERM_TEXT { get; set; }

        [Display(Name = "Ειδικ. κωδ.")]
        public int EIDIKOTITA_ID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "Μάθημα")]
        public string ΜΑΘΗΜΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Π.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Τ.Ε.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΤΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.ΕΠ.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΕΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μ.Ο.")]
        public Nullable<float> ΒΑΘΜΟΣ_ΜΟ { get; set; }

    }

    public class ProgrammaDayViewModel
    {
        public int ΠΡΟΓΡΑΜΜΑ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Τμήμα")]
        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public System.DateTime ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Εβδ.")]
        public int ΕΒΔΟΜΑΔΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ώρα")]
        public int ΩΡΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Μάθημα")]
        public int ΚΩΔ_ΜΑΘΗΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Θ/Ε")]
        public int ΚΩΔ_ΕΡΓΑΣΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "1ος Εκπαιδευτής")]
        public int ΕΚΠΑΙΔΕΥΤΗΣ1 { get; set; }

        [Display(Name = "2ος Εκπαιδευτής")]
        public int? ΕΚΠΑΙΔΕΥΤΗΣ2 { get; set; }

        [Display(Name = "3ος Εκπαιδευτής")]
        public int? ΕΚΠΑΙΔΕΥΤΗΣ3 { get; set; }

        [Display(Name = "Παρών")]
        public bool Π1 { get; set; }

        [Display(Name = "Παρών")]
        public bool Π2 { get; set; }

        [Display(Name = "Παρών")]
        public bool Π3 { get; set; }

    }

    public class sqlTmimataViewModel
    {
        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string TERM { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "Περίοδος")]
        public int PERIOD_ID { get; set; }

        public int TERM_ID { get; set; }

        public int EIDIKOTITA_ID { get; set; }
    }

    public class ProgrammaParameters
    {
        public int tmimaId { get; set; }

        public DateTime theDate { get; set; }
    }

    public class TeacherParousiesParams
    {
        public int schoolId { get; set; }

        public int tmimaId { get; set; }

        public int periodId { get; set; }

        public string theDate1 { get; set; }

        public string theDate2 { get; set; }

    }

    public class StudentBekViewModel
    {
        public int ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αρ.Β. ΒΕΚ")]
        public string ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ονοματεπώνυμο")]
        public string ΕΠΩΝΥΜΟ_ΟΝΟΜΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Απαλλαγή πρακτικής")]
        public bool ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ { get; set; }

        [Display(Name = "Από κατάταξη")]
        public bool ΑΠΟ_ΚΑΤΑΤΑΞΗ { get; set; }

        [Display(Name = "Με επάρκεια ωρών")]
        public bool ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ { get; set; }

        [Display(Name = "Εξάμηνα φοίτησης")]
        public Nullable<int> ΕΞΑΜΗΝΑ { get; set; }

        [Display(Name = "Συνολικές ώρες ειδικότητας")]
        public int? ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ { get; set; }

        [Display(Name = "Ώρες θεωρητικής κατάρτισης")]
        public int? ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ { get; set; }

        [Display(Name = "Ώρες πρακτικής άσκησης")]
        public int? ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία έκδοσης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ { get; set; }

        [Display(Name = "Εκδόθηκε")]
        public bool ΕΚΔΟΣΗ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class TeacherBebeoseisViewModel
    {
        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Για χρήση")]
        public string ΓΙΑ_ΧΡΗΣΗ { get; set; }

        [Display(Name = "Παραδόθηκε")]
        public bool ΠΑΡΑΔΟΔΗΚΕ { get; set; }

        [Display(Name = "Σχολικό έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Display(Name = "Εκπαιδευτικός")]
        public Nullable<int> TEACHER_ID { get; set; }

        public virtual ΕΚΠΑΙΔΕΥΤΕΣ ΕΚΠΑΙΔΕΥΤΕΣ { get; set; }
    }

    public class StudentApousiesDetailViewModel
    {
        public int ΙΕΚ { get; set; }

        public string SCHOOL_NAME { get; set; }

        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        public Nullable<int> ΚΩΔ_ΜΑΘΗΜΑ { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Ώρες απουσίας")]
        public Nullable<int> ΩΡΕΣ_ΑΠΟΥΣΙΑ { get; set; }

        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }
        public int SCHOOL_ID { get; set; }
        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }
        public int EIDIKOTITA_ID { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public string ΠΕΡΙΟΔΟΣ { get; set; }
        public string TERM_TEXT { get; set; }

    }

    public class StudentApousiesLessonViewModel
    {
        public int STUDENT_ID { get; set; }
        public int ΙΕΚ { get; set; }
        public int ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [Display(Name = "Απουσίες")]
        public Nullable<int> ΑΠΟΥΣΙΕΣ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "Όριο(15%)")]
        public Nullable<decimal> ΟΡΙΟ_15 { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}")]
        [Display(Name = "Όριο(20%)")]
        public Nullable<decimal> ΟΡΙΟ_20 { get; set; }

        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }

        public Nullable<int> ΚΩΔ_ΜΑΘΗΜΑ { get; set; }

    }

    public class StudentGradesMiktaViewModel
    {
        public int ΙΕΚ { get; set; }
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Μάθημα")]
        public string ΜΑΘΗΜΑ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Π.")]
        public double? ΜΟΒΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Ε.")]
        public decimal? ΒΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.Τ.Ε.")]
        public double? ΜΟΤΕ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Β.ΕΠ.")]
        public double? ΜΟΕΠ { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Μ.Ο.")]
        public double? ΓΜΟ { get; set; }

        public string ΠΕΡΙΟΔΟΣ { get; set; }
        public int TERM_ID { get; set; }
        public string TERM_TEXT { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

    }

    public class LessonHoursMonitorViewModel
    {
        public string ΠΕΡΙΟΔΟΣ { get; set; }
        public int ΙΕΚ { get; set; }
        public int EIDIKOTITA_ID { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }
        public int LESSON_ID { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_TEXT { get; set; }


        public Nullable<int> ΚΩΔ_ΕΡΓΑΣΙΑ { get; set; }

        [Display(Name = "Θ/Ε")]
        public string ΕΡΓΑΣΙΑ { get; set; }
        public int TERM_ID { get; set; }
        public string TERM_TEXT { get; set; }

        [Display(Name = "Υλοπ. ώρες")]
        public Nullable<int> ΩΡΕΣ_ΜΑΘΗΜΑ { get; set; }

        [Display(Name = "Σύν. ωρών")]
        public Nullable<int> ΩΡΕΣ_ΟΡΙΟ { get; set; }

        [Display(Name = "Υπόλ. ώρες")]
        public Nullable<int> ΥΠΟΛΟΙΠΟ { get; set; }

    }

    public class TeacherParousiesViewModel
    {
        public string ΠΕΡΙΟΔΟΣ { get; set; }
        public int ΙΕΚ { get; set; }
        public Nullable<int> ΕΒΔΟΜΑΔΑ { get; set; }
        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public string ΗΜΕΡΟΜΗΝΙΑ { get; set; }
        public int ΩΡΑ { get; set; }
        public string LESSON_TEXT { get; set; }
        public string LESSON_TYPE_TAG { get; set; }
        public string ΕΚΠΑΙΔΕΥΤΗΣ1 { get; set; }
        public string ΕΚΠΑΙΔΕΥΤΗΣ2 { get; set; }
        public string ΕΚΠΑΙΔΕΥΤΗΣ3 { get; set; }
        public string SCHOOL_NAME { get; set; }
        public string SCHOOL_DIRECTOR { get; set; }
        public Nullable<int> DIRECTOR_GENDER { get; set; }
        public string DAY_GREEK { get; set; }
        public string ΑΝΑΠΛΗΡΩΣΗ { get; set; }

    }

    public class sqlTeacherInPeriodViewModel
    {
        public int TEACHER_ID { get; set; }
        public string ΑΦΜ { get; set; }
        public int ΙΕΚ { get; set; }
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }
        public string EIDIKOTITA_DESC { get; set; }
        public int PERIOD_ID { get; set; }

    }

    public class sqlIekLessonsViewModel
    {
        public int LESSON_ID { get; set; }
        public string LESSON_DESC { get; set; }
        public int IEK_ID { get; set; }
        public int EIDIKOTITA_ID { get; set; }
        public Nullable<int> LESSON_TERM { get; set; }
        public string EIDIKOTITA_TEXT { get; set; }
        public string TERM { get; set; }
        public Nullable<int> LESSON_HOURS { get; set; }
    }

}