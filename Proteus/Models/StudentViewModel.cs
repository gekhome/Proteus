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
    public class StudentViewModel
    {
        public int STUDENT_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public int? ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z-_]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z-_]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z-_]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "του")]
        public string ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z-_]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "της")]
        public string ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Γένος")]
        public string ΓΕΝΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τόπος γέννησης")]
        public string ΤΟΠΟΣ_ΓΕΝΝΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Νομός γέννησης")]
        public string ΝΟΜΟΣ_ΓΕΝΝΗΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΔΟΥ")]
        public string ΔΟΥ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΑΜΚΑ")]
        public string ΑΜΚΑ { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "ΑΜΑ")]
        public string ΑΜΑ { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "ΑΔΤ")]
        public string ΑΔΤ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Δημότης")]
        public string ΔΗΜΟΤΗΣ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τόπος Μητρ. Αρρ.")]
        public string ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Αριθ. Μητρ. Αρρ.")]
        public string ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Υπηκοότητα")]
        public Nullable<int> ΥΠΗΚΟΟΤΗΤΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή E-mail")]
        [Display(Name = "E-mail")]
        public string EMAIL { get; set; }

        [Display(Name = "Απολυτήριο")]
        public Nullable<int> ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία πράξης εισόδου")]
        public Nullable<System.DateTime> ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Αριθ. πράξης εισόδου")]
        public Nullable<int> ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΑΡ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Είδος πράξης εισόδου")]
        public Nullable<int> ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΕΙΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία πράξης εξόδου")]
        public Nullable<System.DateTime> ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Αριθ. πράξης εξόδου")]
        public Nullable<int> ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΑΡ { get; set; }

        [Display(Name = "Είδος πράξης εξόδου")]
        public Nullable<int> ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Διάρκεια σπουδών (εξάμηνα)")]
        [Range(1, 5, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 5")]
        public Nullable<int> ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ { get; set; }

        [Display(Name = "Ολοκλήρωση πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ { get; set; }

        [Display(Name = "Πλήρης απαλλαγή πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ { get; set; }

        [Display(Name = "Ώρες απαλλαγής πρακτικής")]
        public Nullable<int> ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "Διακοπή πρακτικής")]
        public bool ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ { get; set; }

        [Display(Name = "Ημ/νια διακοπής πρακτικής")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Σχόλιο (Πρακτική)")]
        public string ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ { get; set; }

        [Display(Name = "Κατοχή άλλου πτυχίου ΙΕΚ (για ΕΛΣΤΑΤ)")]
        public bool ΠΤΥΧΙΟ_ΙΕΚ_ΑΛΛΟ { get; set; }

        [Display(Name = "Ηλικία (ως προς ημ/νία εισόδου)")]
        public Nullable<short> ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }
        [Display(Name = "Από κατάταξη")]
        public bool ΑΠΟ_ΚΑΤΑΤΑΞΗ { get; set; }

        [Display(Name = "Με επάρκεια ωρών")]
        public bool ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ { get; set; }

        [Display(Name = "Από εγκύκλιο")]
        public Nullable<int> ΑΠΟ_ΠΡΟΚΗΡΥΞΗ { get; set; }

        [Display(Name = "Από Μηχανογραφικό")]
        public bool ΜΗΧΑΝΟΓΡΑΦΙΚΟ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Κωδικός εξέτασης")]
        public string ΕΞΕΤΑΣΗ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Κατηγορία")]
        public Nullable<int> ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΑ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ
        {
            get { return ΕΠΩΝΥΜΟ + " " + ΟΝΟΜΑ; }
        }

    }

    // 12/6/2017: This is required so that a student can be added to the grid
    // skipping the other validation rules (in StudentViewModel) which
    // would otherwise prevent saving the new student.
    public class StudentGridViewModel
    {
        public int STUDENT_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ΑΜΚ")]
        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ειδικότητας")]
        [Display(Name = "Ειδικότητα")]
        public int ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση επώνυμου")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ονόματος")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

    }

    public class StudentAitisiViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int? ΑΜΚ { get; set; }

        [Display(Name = "IEK")]
        public int? ΙΕΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση πρωτόκολλου")]
        [Display(Name = "Πρωτόκολλο")]
        public int? ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ημερομηνίας")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Υποβλήθηκε")]
        public bool ΥΠΟΒΛΗΘΗΚΕ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class StudentBebeoseisViewModel
    {
        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "IEK")]
        public int? ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int? ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση πρωτόκολλου")]
        [Display(Name = "Πρωτόκολλο")]
        public int? ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ημερομηνίας")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Για χρήση")]
        public string ΓΙΑ_ΧΡΗΣΗ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Στρατολογικό γραφείο")]
        public string ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ { get; set; }

        [Display(Name = "Παραδόθηκε")]
        public bool ΠΑΡΑΔΟΔΗΚΕ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class StudentEgrafesViewModel
    {
        public int ΜΕ_ΚΩΔ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση τμήματος")]
        [Display(Name = "Τμήμα")]
        public int? ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση ημ/νίας εγγραφής")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία εγγραφής")]
        public DateTime? ΗΜΝΙΑ_ΕΓΓΡΑΦΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία λήξης (Πρακτική)")]
        public DateTime? ΗΜΝΙΑ_ΠΕΡΑΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση είδους εγγραφής")]
        [Display(Name = "Είδος εγγραφής")]
        public int? ΕΓΓΡΑΦΗ_ΕΙΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση φοίτησης")]
        [Display(Name = "Φοίτηση")]
        public int? ΦΟΙΤΗΣΗ { get; set; }

        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class StudentInfoViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string ΑΜΚΑ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια γέννησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Απολυτήριο")]
        public string ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια εισόδου")]
        public Nullable<System.DateTime> ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Πράξη εισόδου")]
        public string ΠΡΑΞΗ_ΕΙΣΟΔΟΥ { get; set; }

        [Display(Name = "Πράξη εξόδου")]
        public string ΠΡΑΞΗ_ΕΞΟΔΟΥ { get; set; }

        [Display(Name = "Εξάμ. σπουδών")]
        public Nullable<int> ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<short> ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Υπηκοότητα")]
        public string ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

    }

    public class EgrafesInfoViewModel
    {
        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια εγγραφής")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΓΓΡΑΦΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια περάτωσης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΠΕΡΑΣ { get; set; }

        [Display(Name = "Είδος εγγραφής")]
        public string ΕΓΓΡΑΦΗ_ΕΙΔΟΣ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "Φοίτηση")]
        public string ΦΟΙΤΗΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int? ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΙΕΚ { get; set; }

        public int ΜΕ_ΚΩΔ { get; set; }

        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public virtual ΜΑΘΗΤΕΣ ΜΑΘΗΤΕΣ { get; set; }

    }

    public class StudentSelectorViewModel
    {
        [Display(Name = "Κωδικός")]
        public int STUDENT_ID { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

    }

    public class StudentTmimaActiveViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        public int ΙΕΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        public Nullable<int> ΚΩΔ_ΤΜΗΜΑ { get; set; }

        public Nullable<int> ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ { get; set; }

        [Display(Name = "Πράξη εξόδου")]
        public string ΠΡΑΞΗ_ΕΞΟΔΟΥ { get; set; }

    }

    public class bekStudentsTableViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Εξάμ.")]
        public string TERM { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

    }

    // ΓΙΑ ΑΤΟΜΙΚΑ ΔΕΛΤΙΑ - ΜΑΘΗΜΑΤΑ ΑΠΑΛΛΑΓΗ
    // ΠΡΟΣΘΗΚΗ: 12/5/2018
    public class LessonNamesViewModel
    {
        public int ROW_ID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> LESSON_EIDIKOTITA { get; set; }

        [Display(Name = "Εξάμηνο")]
        public Nullable<int> LESSON_TERM { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_TEXT { get; set; }
    }

    public class adkStudentInfoViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int EIDIKOTITA_ID { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }
    }

    public class StudentApallagiViewModel
    {
        public int ΜΑΠ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int? ΙΕΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int? ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int? ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση εξάμηνου")]
        [Display(Name = "Εξάμηνο")]
        public int? ΕΞΑΜΗΝΟ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση μαθήματος")]
        [Display(Name = "Μάθημα")]
        public string ΜΑΘΗΜΑ_ΟΝΟΜΑ { get; set; }
    }


}