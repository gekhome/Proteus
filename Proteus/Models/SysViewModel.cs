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

    public class SYS_GENDERSViewModel
    {
        public int GENDER_ID { get; set; }
        public string GENDER { get; set; }
    }

    public class SYS_KLADOSViewModel
    {
        public SYS_KLADOSViewModel()
        {
            this.SYS_EIDIKOTITES = new HashSet<SYS_EIDIKOTITES>();
        }
        public int KLADOS_ID { get; set; }

        [Display(Name = "Κλάδος")]
        public string KLADOS_NAME { get; set; }

        [Display(Name = "Εκπαίδευση")]
        public string KLADOS_CATEGORY { get; set; }

        [Display(Name = "Ωράριο")]
        public Nullable<int> KLADOS_HOURS { get; set; }

        public virtual ICollection<SYS_EIDIKOTITES> SYS_EIDIKOTITES { get; set; }
    }

    public class SYS_KLADOS_ENIAIOSViewModel
    {
        public int ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Κλάδος ενοποίησης")]
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }
    }

    public class SYS_EIDIKOTITESViewModel
    {
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Κωδικός")]
        public string EIDIKOTITA_CODE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα ενιαία")]
        public string EIDIKOTITA_UNIFIED { get; set; }

        [Display(Name = "Κλάδος - Ειδικότητα")]
        public string EIDIKOTITA_DESC 
        { 
            get 
            { return EIDIKOTITA_CODE + "-" + EIDIKOTITA_NAME; }
        }
        public int? EIDIKOTITA_KLADOS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος ενοποίησης")]
        public Nullable<int> KLADOS_UNIFIED { get; set; }

        public virtual SYS_KLADOS SYS_KLADOS { get; set; }

        public virtual SYS_KLADOS_ENIAIOS SYS_KLADOS_ENIAIOS { get; set; }

    }

    public class VD_EIDIKOTITESViewModel
    {
        public int EIDIKOTITA_ID { get; set; }
        public string EIDIKOTITA_DESC { get; set; }
        public string EIDIKOTITA_CODE { get; set; }
        public string EIDIKOTITA_NAME { get; set; }
        public Nullable<int> EIDIKOTITA_KLADOS_ID { get; set; }
        public string KLADOS_NAME { get; set; }
        public string EIDIKOTITA_UNIFIED { get; set; }
    }

    public class SYS_EIDIKOTITES_IEKViewModel
    {
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Κωδικός (ΓΓΔΒΜ)")]
        public string EIDIKOTITA_CODE { get; set; }

        [Display(Name = "ISCED(0000)")]
        public string ISCED_0000 { get; set; }

        [Display(Name = "Έγκριση")]
        public bool APPROVED { get; set; }

    }

    public class SYS_SCHOOLSViewModel
    {
        [Display(Name = "Κωδ. Σχολείου")]
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Ονομασία")]
        public string SCHOOL_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public int? SCHOOL_PERIFERIAKI_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφέρεια")]
        public Nullable<int> SCHOOL_PERIFERIA_ID { get; set; }

        [Display(Name = "Γεωγρ. Περιοχή")]
        public Nullable<int> SCHOOL_REGION_ID { get; set; }

        [Display(Name = "Δήμος")]
        public Nullable<int> SCHOOL_DIMOS { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(100, ErrorMessage = "Πρέπει να είναι μέχρι 100 χαρακτήρες.")]
        [Display(Name = "Διεύθυνση")]
        public string SCHOOL_ADDRESS { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "ΤΚ, Πόλη")]
        public string SCHOOL_TK_CITY { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή E-mail")]
        [Display(Name = "E-mail")]
        public string SCHOOL_EMAIL { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string SCHOOL_PHONE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Fax")]
        public string SCHOOL_FAX { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]      
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Διευθυντής")]
        public string SCHOOL_DIRECTOR { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Αναπληρωτής")]
        public string SCHOOL_DEPUTY { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [RegularExpression(@"^[Α-Ω]+[ Α-Ω-_]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά")]
        [Display(Name = "Πληροφορίες")]
        public string SCHOOL_INFO { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο διευθυντή")]
        public Nullable<int> DIRECTOR_GENDER { get; set; }

        [Display(Name = "Φύλο αναπληρωτή")]
        public Nullable<int> DEPUTY_GENDER { get; set; }
    }

    public class SYS_SCHOOLSGridViewModel
    {
        [Display(Name = "Κωδ. Σχολείου")]
        public int SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(60, ErrorMessage = "Πρέπει να είναι μέχρι 60 χαρακτήρες.")]
        [Display(Name = "Ονομασία")]
        public string SCHOOL_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περιφερειακή")]
        public int? SCHOOL_PERIFERIAKI_ID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Διευθυντής")]
        public string SCHOOL_DIRECTOR { get; set; }
    }

    public class SYS_PERIFERIAKESViewModel
    {
        public int PERIFERIAKI_ID { get; set; }
        public string PERIFERIAKI { get; set; }
    }

    public class SYS_PERIFERIESViewModel
    {
        public SYS_PERIFERIESViewModel()
        {
            this.SYS_DIMOS = new HashSet<SYS_DIMOS>();
        }

        [Display(Name = "Κωδ. Περιφέρειας")]
        public int PERIFERIA_ID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PERIFERIA_NAME { get; set; }

        [Display(Name = "Γεωγραφικός τομέας")]
        public Nullable<int> REGION_ID { get; set; }

        public virtual ICollection<SYS_DIMOS> SYS_DIMOS { get; set; }
    }

    public class SYS_DIMOSViewModel
    {
        public int DIMOS_ID { get; set; }
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
        public virtual SYS_PERIFERIES SYS_PERIFERIES { get; set; }
    }

    public class SYS_TERMViewModel
    {
        public int TERM_ID { get; set; }
       
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(5, ErrorMessage = "Πρέπει να είναι μέχρι 5 χαρακτήρες.")]
        [Display(Name = "Εξάμηνο")]
        public string TERM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Κείμενο")]
        public string TERM_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Λεκτικό")]
        public string TERM_WORD { get; set; }
    }

    public class ApolytiriaViewModel
    {
        public int ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Απολυτήριο")]
        public string ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Απολυτήριο κλάση")]
        public int? ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ { get; set; }

    }

    public class RegistrationViewModel
    {
        public int ΕΓΓΡΑΦΗ_ΕΙΔΟΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Είδος εγγραφής")]
        public string ΕΓΓΡΑΦΗ_ΕΙΔΟΣ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class ErgasiesViewModel
    {
        public int ΚΩΔ_ΕΡΓΑΣΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Εργασία")]
        public string ΕΡΓΑΣΙΑ { get; set; }
    }

    public class EtosViewModel
    {
        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Έτος")]
        public short ΕΤΟΣ { get; set; }
    }

    public class LessonCharacterizationViewModel
    {
        public int ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Χαρακτηρισμός")]
        public string ΧΑΡΑΚΤΗΡΙΣΜΟΣ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class PAViewModel
    {
        public int ΠΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Πρωί-Απόγευμα")]
        public string ΠΑ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class SchoolYearsViewModel
    {
        public int SY_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(9, ErrorMessage = "Πρέπει να είναι μέχρι 9 χαρακτήρες (π.χ.2015-2016).")]
        [Display(Name = "Σχολικό Έτος")]
        public string SY_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία Έναρξης")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> SY_DATESTART { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία Λήξης")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> SY_DATEEND { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία ΕΛΣΤΑΤ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> DATE_ELSTAT { get; set; }
    }

    public class PeriodosViewModel
    {
        public int PERIOD_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(5, ErrorMessage = "Πρέπει να είναι μέχρι 5 χαρακτήρες (π.χ. 2017Χ).")]
        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "Χειμερινό-Εαρινό")]
        public Nullable<int> ΧΕ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία Έναρξης")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΝΑΡΞΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ημ/νία Λήξης")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΛΗΞΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> ΣΧΟΛΙΚΟ_ΕΤΟΣ { get; set; }

        public string SY_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Διοικητής")]
        public string ΔΙΟΙΚΗΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Φύλο")]
        public Nullable<int> ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ { get; set; }
    }

    public class XEViewModel
    {
        public int ΧΕ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Χειμερινό-Εαρινό")]
        public string ΧΕ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class EisodosPraxiViewModel
    {
        public int ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(30, ErrorMessage = "Πρέπει να είναι μέχρι 30 χαρακτήρες.")]
        [Display(Name = "Πράξη εισόδου")]
        public string ΠΡΑΞΗ_ΕΙΣΟΔΟΥ { get; set; }
    }

    public class ExodosPraxiViewModel
    {
        public int ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(30, ErrorMessage = "Πρέπει να είναι μέχρι 30 χαρακτήρες.")]
        [Display(Name = "Πράξη εξόδου")]
        public string ΠΡΑΞΗ_ΕΞΟΔΟΥ { get; set; }
    }

    public class SpoudesViewModel
    {
        public int ΒΑΘΜΙΔΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Βασικές σπουδές")]
        public string ΒΑΘΜΙΔΑ { get; set; }
    }

    public class YpalilosViewModel
    {
        public int ΥΠΑΛΛΗΛΟΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Απασχόληση")]
        public string ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class NationalityViewModel
    {
        public int ΥΠΗΚΟΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Υπηκοότητα")]
        public string ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class AttendanceViewModel
    {
        public int ΦΟΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Είδος εγγραφής")]
        public string ΦΟΙΤΗΣΗ_ΛΕΚΤΙΚΟ { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Φοίτηση")]
        public string ΔΙΚΑΙΩΜΑ_ΛΕΚΤΙΚΟ { get; set; }
    }

    public class NumbersViewModel
    {
        public int NUMBER { get; set; } 
    }

    public class ApoxorisiAitiaViewModel
    {
        public int ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αιτία αποχώρησης")]
        public string ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ { get; set; }

    }

    public class ApasxolisiViewModel
    {
        public int ΑΠΑΣΧΟΛΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Απασχόληση")]
        public string ΑΠΑΣΧΟΛΗΣΗ_ΛΕΚΤΙΚΟ { get; set; }

    }

    public class SysReportViewModel
    {
        public int DOC_ID { get; set; }

        [Display(Name = "Ονομασία")]
        public string DOC_NAME { get; set; }

        [Display(Name = "Περιγραφή")]
        public string DOC_DESCRIPTION { get; set; }

        [Display(Name = "Κατηγορία")]
        public string DOC_CLASS { get; set; }

    }

    public class SchoolLoginsViewModel
    {
        public int LOGIN_ID { get; set; }

        [Display(Name = "Εκπαιδευτική μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Τελευταία είσοδος")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public Nullable<System.DateTime> LOGIN_DATETIME { get; set; }

    }

    public class ExamCategoriesViewModel
    {
        public int ΚΑΤΗΓΟΡΙΑ_ΚΩΔΙΚΟΣ { get; set; }

        public string ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ { get; set; }
    }



    #region SETUP
    //----------------------------------------------------
    // new addition 30-07-2016 for MasterChild grids
    public class PeriferiaViewModel
    {
        public int PERIFERIA_ID { get; set; }

        [Display(Name = "Περιφερειακή Ενότητα")]
        public string PERIFERIA_NAME { get; set; }
    }

    public class DimosViewModel
    {
        public int DIMOS_ID { get; set; }

        [Display(Name = "Δήμος")]
        public string DIMOS { get; set; }
        public Nullable<int> DIMOS_PERIFERIA { get; set; }
    }
    //----------------------------------------------------

    public class EidikotitesViewModel
    {
        public int EIDIKOTITA_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Κωδικός")]
        public string EIDIKOTITA_CODE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(150, ErrorMessage = "Πρέπει να είναι μέχρι 150 χαρακτήρες.")]
        [Display(Name = "Ειδικότητα ενιαία")]
        public string EIDIKOTITA_UNIFIED { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος")]
        public Nullable<int> EIDIKOTITA_KLADOS_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Κλάδος ενοποίησης")]
        public Nullable<int> KLADOS_UNIFIED { get; set; }

        [Display(Name = "Βαθμίδα")]
        public int? EDUCATION_CLASS { get; set; }

        public virtual SYS_KLADOS_ENIAIOS SYS_KLADOS_ENIAIOS { get; set; }
        public virtual SYS_KLADOS SYS_KLADOS { get; set; }

    }

    public class KladosUnifiedViewModel
    {
        public int ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "Κλάδος ενοποίησης")]
        public string ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ { get; set; }

        [Display(Name = "Κλάδος")]
        public Nullable<int> ΚΛΑΔΟΣ { get; set; }
    }

    public class GroupsViewModel
    {
        public int GROUP_ID { get; set; }

        [Display(Name = "Ομάδα")]
        public string GROUP_TEXT { get; set; }
    }

    public class sqlEidikotitesKUViewModel
    {
        public int EIDIKOTITA_ID { get; set; }
        public string EIDIKOTITA_PTYXIO { get; set; }
        public Nullable<int> KLADOS_UNIFIED { get; set; }
        public Nullable<int> EIDIKOTITA_KLADOS_ID { get; set; }
    }


    public class HoursConverterViewModel
    {

        [Range(0, 20000, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 0 και 20000")]
        [Display(Name = "Ώρες")]    
        public int Hours { get; set; }

        [Display(Name = "Λεκτικό")]
        public string HoursWord { get; set; }

        [Display(Name = "Μήνες")]
        public int PE_Months { get; set; }

        [Display(Name = "Ημέρες")]
        public int PE_Days { get; set; }

        [Display(Name = "Μήνες")]
        public int TE_Months { get; set; }

        [Display(Name = "Ημέρες")]
        public int TE_Days { get; set; }

        [Display(Name = "Μήνες")]
        public int DE_Months { get; set; }

        [Display(Name = "Ημέρες")]
        public int DE_Days { get; set; }

        [Display(Name = "Μήνες")]
        public int ET_Months { get; set; }

        [Display(Name = "Ημέρες")]
        public int ET_Days { get; set; }

    }

    public class GradeConverterViewModel
    {
        [Range(1, 100, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 100")]
        [Display(Name = "Πλήθος μαθημάτων")]
        public int NumberOfLessons { get; set; }

        [Range(1, 1000, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 1000")]
        [Display(Name = "Άθροισμα βαθμών")]
        public int GradeSum { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]      
        [Display(Name = "Δεκαδικός βαθμός")]
        public double GradeDecimal { get; set; }

        [Display(Name = "Μικτός βαθμός")]
        public string GradeFractional { get; set; }
    }

    #endregion


    //-------------------------------------------------
    // This one is used for print page
    // with ReportViewer for custom parameters
    // dropdown list. 
    // NOT USED in implemented version of Print.cshtml
    //-------------------------------------------------
    public class PERIFERIESViewModel
    {
        readonly List<SYS_PERIFERIES> periferies;

        [Display(Name = "Δήμοι")]
        public int SelectedPeriferiaId { get; set; }

        public string SelectedPeriferia
        {
            get { return this.periferies[this.SelectedPeriferiaId].PERIFERIA_NAME; }
        }

        public IEnumerable<SelectListItem> PeriferiaItems
        {
            get { return new SelectList(periferies, "PERIFERIA_ID", "PERIFERIA_NAME"); }
        }

        public PERIFERIESViewModel(List<SYS_PERIFERIES> periferies)
        {
            this.periferies = periferies;
        }
    }

    public class REGIONSViewModel
    {
        public int REGION_ID { get; set; }

        [Display(Name = "Γεωγραφικός τομέας")]
        public string REGION_NAME { get; set; }

    }

    //-------------------------------------------------
    // This is used for reporting pinakes of results.
    // NOT USED ANYWHERE
    //-------------------------------------------------
    public class GeneralReportParameters
    {
        public int? PERIFERIA_ID { get; set; }

        public int? PERIFERIAKI_ID { get; set; }

        public int? SCHOOL_ID { get; set; }

        public int? SYEAR_ID { get; set; }
    }

    public class DimoiParameters
    {
        public int PERIFERIA_ID { get; set; }
    }

    public class XmReportParameters
    {
        public int? PROKIRIXI_ID { get; set; }

        public int? SCHOOL_ID { get; set; }
    }

}