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
    public class TeacherViewModel
    {
        public int TEACHER_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ απασχόλησης")]
        public int ΙΕΚ { get; set; }

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
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }


        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "του")]
        public string ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "της")]
        public string ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο")]
        public Nullable<int> ΦΥΛΟ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΔΟΥ")]
        public string ΔΟΥ { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "ΑΜΚΑ")]
        public string ΑΜΚΑ { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "ΑΜΑ")]
        public string ΑΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

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

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κωδ. ειδικότητας")]
        public string ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Βασικό πτυχίο")]
        public Nullable<int> ΠΤΥΧΙΟ { get; set; }

        [Display(Name = "Μεταπτυχιακό")]
        public bool MASTER { get; set; }

        [Display(Name = "Διδακτορικό")]
        public bool ΔΙΔΑΚΤΟΡΙΚΟ { get; set; }

        [Display(Name = "Παιδαγωγικό")]
        public bool ΠΑΙΔΑΓΩΓΙΚΟ { get; set; }

        [Display(Name = "Απασχόληση")]
        public Nullable<int> ΥΠΑΛΛΗΛΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία 1ης πρόσληψης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ { get; set; }

        [Display(Name = "Ηλικία")]
        public Nullable<int> ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        [Display(Name = "Τράπεζα")]
        public string BANK_NAME { get; set; }

        [Display(Name = "Αρ. λογ/σμού")]
        public string BANK_ACCOUNT { get; set; }

        [Display(Name = "ΙΒΑΝ")]
        public string BANK_IBAN { get; set; }

        [Display(Name = "ΑΔΤ")]
        public string ΑΔΤ { get; set; }

        [Display(Name = "Τέκνα")]
        public int ΤΕΚΝΑ { get; set; }

        [Display(Name = "Κωδ. μισθοδοσίας")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        public string ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ 
        {
            get { return ΕΠΩΝΥΜΟ + " " + ΟΝΟΜΑ; }
        }

        // Constructor
        public TeacherViewModel() { }
        public TeacherViewModel(ΕΚΠΑΙΔΕΥΤΕΣ teacherVM)
        {
            this.ΑΦΜ = teacherVM.ΑΦΜ;
            this.ΙΕΚ = teacherVM.ΙΕΚ;
            this.ΕΠΩΝΥΜΟ = teacherVM.ΕΠΩΝΥΜΟ;
            this.ΟΝΟΜΑ = teacherVM.ΟΝΟΜΑ;
            this.ΠΑΤΡΩΝΥΜΟ = teacherVM.ΠΑΤΡΩΝΥΜΟ;
            this.ΜΗΤΡΩΝΥΜΟ = teacherVM.ΜΗΤΡΩΝΥΜΟ;
            this.ΦΥΛΟ = teacherVM.ΦΥΛΟ;
            this.ΔΟΥ = teacherVM.ΔΟΥ;
            this.ΑΜΚΑ = teacherVM.ΑΜΚΑ;
            this.ΑΜΑ = teacherVM.ΑΜΑ;
            this.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = teacherVM.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            this.ΔΙΕΥΘΥΝΣΗ = teacherVM.ΔΙΕΥΘΥΝΣΗ;
            this.ΤΗΛΕΦΩΝΑ = teacherVM.ΤΗΛΕΦΩΝΑ;
            this.EMAIL = teacherVM.EMAIL;
            this.ΚΛΑΔΟΣ = teacherVM.ΚΛΑΔΟΣ;
            this.ΕΙΔΙΚΟΤΗΤΑ = teacherVM.ΕΙΔΙΚΟΤΗΤΑ;
            this.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = teacherVM.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
            this.ΠΤΥΧΙΟ = teacherVM.ΠΤΥΧΙΟ;
            this.MASTER = teacherVM.MASTER ?? false;
            this.ΔΙΔΑΚΤΟΡΙΚΟ = teacherVM.ΔΙΔΑΚΤΟΡΙΚΟ ?? false;
            this.ΠΑΙΔΑΓΩΓΙΚΟ = teacherVM.ΠΑΙΔΑΓΩΓΙΚΟ ?? false;
            this.ΥΠΑΛΛΗΛΟΣ = teacherVM.ΥΠΑΛΛΗΛΟΣ;
            this.ΠΑΡΑΤΗΡΗΣΕΙΣ = teacherVM.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            this.BANK_ACCOUNT = teacherVM.BANK_ACCOUNT;
            this.BANK_IBAN = teacherVM.BANK_IBAN;
            this.BANK_NAME = teacherVM.BANK_NAME;
        }
    }

    public class TeacherGridViewModel
    {
        public int TEACHER_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ απασχόλησης")]
        public int ΙΕΚ { get; set; }

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
        [Display(Name = "Ειδικότητα")]
        public int ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Κλάδος")]
        public int ΚΛΑΔΟΣ { get; set; }

        [Display(Name = "Κωδ.Μισθ.")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        public string ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ { get; set; }

    }

    public class TeacherAnatheseisViewModel
    {
        public int ΕΑ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα κατάρτισης")]
        public int ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περίοδος")]
        public Nullable<int> ΠΕΡΙΟΔΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Μάθημα")]
        public int ΜΑΘΗΜΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εξάμηνο")]
        public Nullable<int> ΕΞΑΜΗΝΟ { get; set; }

        [Display(Name = "Θ")]
        public Nullable<short> ΩΡΕΣ_ΘΕΩΡΙΑ { get; set; }

        [Display(Name = "Ε")]
        public Nullable<short> ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ { get; set; }

        [Display(Name = "Σύνολο")]
        public Nullable<short> ΣΥΝΟΛΟ { get; set; }

        public Nullable<int> TEACHER_ID { get; set; }

    }

    public class TeacherPeriodsViewModel
    {
        public int ΕΠ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περίοδος")]
        public Nullable<int> ΠΕΡΙΟΔΟΣ_ΚΩΔ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία πρόσληψης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [Display(Name = "Απόφαση")]
        public string ΑΠΟΦΑΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        public Nullable<int> TEACHER_ID { get; set; }

    }

    public class TeacherAitiseisViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

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

        [Display(Name = "Κείμενο")]
        public string ΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Υποβλήθηκε")]
        public bool ΥΠΟΒΛΗΘΗΚΕ { get; set; }

        [Display(Name = "Εκπαιδευτής")]
        public Nullable<int> TEACHER_ID { get; set; }

        public virtual ΕΚΠΑΙΔΕΥΤΕΣ ΕΚΠΑΙΔΕΥΤΕΣ { get; set; }
    }

    public class TeacherInfoViewModel
    {
        public int TEACHER_ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Πατρώνυμο")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Μητρώνυμο")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "ΑΜΚΑ")]
        public string ΑΜΚΑ { get; set; }

        [Display(Name = "ΑΜΑ")]
        public string ΑΜΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία γέννησης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΓΕΝΝΗΣΗ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }

        [Display(Name = "Κλάδος")]
        public string KLADOS_NAME { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία πρόσληψης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ { get; set; }

        [Display(Name = "Ηλικία")]
        public int ΗΛΙΚΙΑ { get; set; }

        [Display(Name = "Απασχόληση")]
        public string ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ { get; set; }

    }

    public class TeacherSelectorViewModel
    {
        public int TEACHER_ID { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

    }

    public class TeacherPeriodsInfoViewModel
    {
        public int ΕΠ_ΚΩΔ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία πρόσληψης")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ { get; set; }

        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [Display(Name = "Απόφαση")]
        public string ΑΠΟΦΑΣΗ { get; set; }

        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [Display(Name = "ΑΔΑ")]
        public string ΑΔΑ { get; set; }

        public Nullable<int> TEACHER_ID { get; set; }

    }

    public class TeacherAnatheseisInfoViewModel
    {
        public int ΕΑ_ΚΩΔ { get; set; }

        public int TEACHER_ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Ειδικότητα κατάρτισης")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [Display(Name = "Ώρες Θ")]
        public Nullable<short> ΩΡΕΣ_ΘΕΩΡΙΑ { get; set; }

        [Display(Name = "Ώρες Ε")]
        public Nullable<short> ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ { get; set; }

    }

    public class TeacherAnalipsiViewModel
    {
        public int ΑΝΑΛΗΨΗ_ΚΩΔ { get; set; }

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

        [Display(Name = "Περίοδος")]
        public Nullable<int> PERIOD_ID { get; set; }

        [Display(Name = "Υπεγράφη")]
        public bool ΥΠΕΓΡΑΦΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εκπαιδευτής")]
        public Nullable<int> TEACHER_ID { get; set; }

    }

    public class TeacherWithdrawalViewModel
    {
        public int ΑΠΟΧΩΡΗΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εκπαιδευτής")]
        public Nullable<int> TEACHER_ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "IEK")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ.έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Αιτιολογία")]
        public Nullable<int> ΑΙΤΙΟΛΟΓΙΑ { get; set; }

    }

    public class QueryAnatheseisViewModel
    {
        public int ΕΑ_ΚΩΔ { get; set; }
        public int TEACHER_ID { get; set; }

        public int ΙΕΚ { get; set; }

        [Display(Name = "Εκπ. Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΑΦΜ { get; set; }

        [Display(Name = "Εκπαιδευτής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Κλάδος-Ειδικότητα")]
        public string EIDIKOTITA_DESC { get; set; }

        [Display(Name = "Ειδικότητα κατάρτισης")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [Display(Name = "Θ")]
        public Nullable<short> ΩΡΕΣ_ΘΕΩΡΙΑ { get; set; }

        [Display(Name = "Ε")]
        public Nullable<short> ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

    }
}