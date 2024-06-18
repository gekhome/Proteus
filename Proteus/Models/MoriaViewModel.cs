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
    public class XmEgykliosViewModel
    {
        public int ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Αρ. Πρωτ.")]
        public string ΕΓΚΥΚΛΙΟΣ_ΑΠ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έναρξη")]
        public DateTime? ΗΜΝΙΑ_ΕΝΑΡΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Λήξη")]
        public DateTime? ΗΜΝΙΑ_ΛΗΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. Έτος")]
        public int ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κατάσταση")]
        public int ΚΑΤΑΣΤΑΣΗ { get; set; }

        [Display(Name = "Ενεργή")]
        public bool ΕΝΕΡΓΗ { get; set; }

        [Display(Name = "Διαχείριση")]
        public bool ΔΙΑΧΕΙΡΙΣΗ { get; set; }

    }

    public class XmEgykliosStatusViewModel
    {
        public int STATUS_ID { get; set; }

        [Display(Name = "Κατάσταση")]
        public string STATUS { get; set; }
    }

    public class XmEidikotitesViewModel
    {
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα κατάρτισης")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έγκριση")]
        public bool APPROVED { get; set; }

    }

    public class XmEgykliosEidikotitesViewModel
    {
        public int RECORD_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εγκύκλιος")]
        public int EGYKLIOS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολείο")]
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εξάμ.")]
        public int TERM_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σπουδαστές")]
        public int? STUDENTS { get; set; }

        public virtual ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ { get; set; }

    }

    public class XmEidikotitaSelectorViewModel
    {
        public int RECORD_ID { get; set; }
        public int EGYKLIOS_ID { get; set; }
        public int SCHOOL_ID { get; set; }
        public int EIDIKOTITA_ID { get; set; }
        public string EIDIKOTITA_TERM { get; set; }
        public string TERM { get; set; }
        public int TERM_ID { get; set; }
    }

    public class XmResultViewModel
    {
        public int ΑΠΟΤΕΛΕΣΜΑ_ΚΩΔ { get; set; }

        [Display(Name = "Αποτέλεσμα")]
        public string ΑΠΟΤΕΛΕΣΜΑ { get; set; }
    }

    public class XmExperienceViewModel
    {
        public int ΕΜΠΕΙΡΙΑ_ΚΩΔ { get; set; }

        public int? ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public DateTime? ΕΝΑΡΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public DateTime? ΛΗΞΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        [Display(Name = "Μήνες")]
        public int? ΔΙΑΡΚΕΙΑ { get; set; }

        [Display(Name = "Εγκύκλιος")]
        public int? ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        public virtual ΧΜ_ΥΠΟΨΗΦΙΟΣ ΧΜ_ΥΠΟΨΗΦΙΟΣ { get; set; }
    }

    public class XmAitisiViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
        public int? ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Αρ. Πρωτ.")]
        public string ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Α.Μ.Κ.")]
        public int? ΑΜΚ { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Α.Φ.Μ.")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Μ.Κ.Α. *")]
        public string ΑΜΚΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Επώνυμο *")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Όνομα *")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Πατρώνυμο *")]
        public string ΠΑΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Μητρώνυμο *")]
        public string ΜΗΤΡΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο *")]
        public int? ΦΥΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Δ.Τ. *")]
        public string ΑΔΤ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ.]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Εκδούσα Αρχή *")]
        public string ΑΔΤ_ΑΡΧΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία έκδοσης *")]
        public DateTime? ΑΔΤ_ΗΜΝΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]        
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ.]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Δήμος εγγραφής *")]
        public string ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Αρ. Δημοτολ. *")]
        public string ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Α.Μ. Αρρένων")]
        public string ΑΜ_ΑΡΡΕΝΩΝ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ.]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Πόλη κατοικίας *")]
        public string ΚΑΤΟΙΚΙΑ_ΠΟΛΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z0-9-_ΪΫ.,']*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά και ψηφία")]
        [Display(Name = "Οδός, Αριθ., ΤΚ *")]
        public string ΚΑΤΟΙΚΙΑ_ΔΝΣΗ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα *")]
        public string ΤΗΛΕΦΩΝΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή e-mail")]
        [Display(Name = "E-mail *")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "ΙΕΚ 1ης επιλογής *")]
        public int? ΙΕΚ1 { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα 1η, Εξ. *")]
        public int? ΕΙΔΙΚΟΤΗΤΑ1 { get; set; }

        [Display(Name = "Ειδικότητα 2η, Εξ.")]
        public int? ΕΙΔΙΚΟΤΗΤΑ2 { get; set; }

        [Display(Name = "Ειδικότητα 3η, Εξ.")]
        public int? ΕΙΔΙΚΟΤΗΤΑ3 { get; set; }

        [Display(Name = "ΙΕΚ 2ης επιλογής")]
        public int? ΙΕΚ2 { get; set; }

        [Display(Name = "Ειδικότητα 2η, Εξ.")]
        public int? ΕΙΔΙΚΟΤΗΤΑ4 { get; set; }

        [Display(Name = "Ειδικότητα 3η, Εξ.")]
        public int? ΕΙΔΙΚΟΤΗΤΑ5 { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σπουδές *")]
        public int? ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        [Display(Name = "Βαθμός *")]
        public decimal? ΒΑΘΜΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία αποφοίτ. από Δ/θμια Εκπ. *")]
        public DateTime? ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ { get; set; }

        [Display(Name = "Τρίτεκνη οικογένεια")]
        public bool ΤΡΙΤΕΚΝΟΣ { get; set; }

        [Display(Name = "Πολύτεκνη οικογένεια")]
        public bool ΠΟΛΥΤΕΚΝΟΣ { get; set; }

        [Display(Name = "Μονογονεϊκή οικογ.")]
        public bool ΜΟΝΟΓΟΝΕΙΚΟΣ { get; set; }

        [Display(Name = "Μήνες εμπειρίας")]
        public int? ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ { get; set; }

        [Display(Name = "Μόρια βαθμού")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΒΑΘΜΟΣ { get; set; }

        [Display(Name = "Μόρια αποφοίτησης")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ { get; set; }

        [Display(Name = "Μόρια τρίτεκνης")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ { get; set; }

        [Display(Name = "Μόρια πολύτεκνης")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ { get; set; }

        [Display(Name = "Μόρια μονογονεϊκής")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ { get; set; }

        [Display(Name = "Μόρια εμπειρίας")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ { get; set; }

        [Display(Name = "Μόρια σύνολο")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ { get; set; }

        [Display(Name = "Αποτέλεσμα")]
        public int? ΑΠΟΤΕΛΕΣΜΑ { get; set; }

        [Display(Name = "Ειδικότητα ένταξης")]
        public int? ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Υπηκοότητα *")]
        public int? ΕΘΝΙΚΟΤΗΤΑ { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Διαβατήριο")]
        public string ΔΙΑΒΑΤΗΡΙΟ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        [Display(Name = "Εξάμ.")]
        public int? TERM1 { get; set; }

        [Display(Name = "Εξάμ.")]
        public int? TERM2 { get; set; }

        [Display(Name = "Εξάμ.")]
        public int? TERM3 { get; set; }

        [Display(Name = "Εξάμ.")]
        public int? TERM4 { get; set; }

        [Display(Name = "Εξάμ.")]
        public int? TERM5 { get; set; }

    }

    public class XmAitisiGridViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
        public int? ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Αρ. Πρωτ.")]
        public string ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public DateTime? ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Α.Φ.Μ.")]
        public string ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Display(Name = "ΙΕΚ 1ης επιλογής")]
        public int? ΙΕΚ1 { get; set; }

        [Display(Name = "ΙΕΚ 2ης επιλογής")]
        public int? ΙΕΚ2 { get; set; }

        [Display(Name = "Μόρια")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ { get; set; }

    }

    public class XmAitiseisResultsViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
        public int? ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα 1η")]
        public int ΕΙΔΙΚΟΤΗΤΑ1 { get; set; }

        [Display(Name = "Μόρια")]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal? ΜΟΡΙΑ { get; set; }

        [Display(Name = "Αποτέλεσμα")]
        public int ΑΠΟΤΕΛΕΣΜΑ { get; set; }

    }

    public class XmAitiseisAmkViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        public int? ΕΓΚΥΚΛΙΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Α.Μ.Κ.")]
        public int? ΑΜΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Επώνυμο")]
        public string ΕΠΩΝΥΜΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Όνομα")]
        public string ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα ένταξης")]
        public int ΕΙΔΙΚΟΤΗΤΑ { get; set; }

    }

    public class XmAutoAmkViewModel
    {
        [Display(Name = "Αρχικός Α.Μ.")]
        public int initialAmk { get; set; }
    }

    public class XmNationalityViewModel
    {
        public int NATIONALITY_ID { get; set; }

        [Display(Name = "Εθνικότητα")]
        public string NATIONALITY_TEXT { get; set; }
    }

    public class sqlUploadedFilesViewModel
    {
        public long? FILE_ID { get; set; }
        public string ID { get; set; }
        public int? AITISI_ID { get; set; }
        public int? SCHOOL_ID { get; set; }
        public int? EGYKLIOS_ID { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string STUDENT_AFM { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string UPLOAD_NAME { get; set; }

        [Display(Name = "Περιγραφή")]
        public string UPLOAD_SUMMARY { get; set; }

        [Display(Name = "Όνομα αρχείου")]
        public string FILENAME { get; set; }

        [Display(Name = "Επέκταση")]
        public string EXTENSION { get; set; }

        [Display(Name = "Φάκελος")]
        public string SCHOOL_USER { get; set; }

        [Display(Name = "Υποφάκελος")]
        public string SCHOOLYEAR_TEXT { get; set; }

    }


    public class XM_REPORT_PARAMETERS
    {
        public int? EGYKLIOS_ID { get; set; }

        public int? AITISI_ID { get; set; }

        public int? SCHOOL_ID { get; set; }
    }

}