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
    public class ErgodotesViewModel
    {
        [Display(Name = "Κωδικός")]
        public int ΕΡΓΟΔΟΤΗΣ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Επωνυμία")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [RegularExpression(@"^[Α-ΩA-Z]+[ Α-ΩA-Z-_ΪΫ]*$", ErrorMessage = "Μόνο κεφαλαία ελληνικά, λατινικά")]
        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Display(Name = "Φύλο υπεύθυνου")]
        public Nullable<int> ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Δεν είναι έγκυρη μορφή E-mail")]
        [Display(Name = "E-mail")]
        public string EMAIL { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Παρατηρήσεις")]
        public string ΠΑΡΑΤΗΡΗΣΕΙΣ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        // Constructor
        public ErgodotesViewModel() { }

        public ErgodotesViewModel(ΕΡΓΟΔΟΤΕΣ ergodotesVM)
        {
            this.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = ergodotesVM.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ;
            this.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = ergodotesVM.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ;
            this.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = ergodotesVM.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ;
            this.ΥΠΕΥΘΥΝΟΣ = ergodotesVM.ΥΠΕΥΘΥΝΟΣ;
            this.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = ergodotesVM.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ;
            this.ΤΗΛΕΦΩΝΑ = ergodotesVM.ΤΗΛΕΦΩΝΑ;
            this.EMAIL = ergodotesVM.EMAIL;
            this.ΔΙΕΥΘΥΝΣΗ = ergodotesVM.ΔΙΕΥΘΥΝΣΗ;
            this.ΠΑΡΑΤΗΡΗΣΕΙΣ = ergodotesVM.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            this.ΙΕΚ = ergodotesVM.ΙΕΚ ?? 0;
        }
    }

    // Ergodotis Praktiki

    public class ErgodotesPraktikiViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Τμήμα")]
        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σπουδαστής")]
        public int ΜΑΘΗΤΗΣ_ΑΜΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια από")]
        public System.DateTime? ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια έως")]
        public System.DateTime? ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(1, 1200, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 1200")]
        [Display(Name = "Ώρες πρακτικής")]
        public int ΩΡΕΣ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αντικείμενο (σύντομος τίτλος)")]
        public string ΑΝΤΙΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Αναλυτική περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία βεβαίωσης")]
        public Nullable<System.DateTime> ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public int ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }
    }

    public class PraktikiSubjectViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Αντικείμενο (σύντομος τίτλος)")]
        public string ΑΝΤΙΚΕΙΜΕΝΟ { get; set; }

        [Display(Name = "Αναλυτική περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία βεβαίωσης")]
        public Nullable<System.DateTime> ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ { get; set; }
    }

    public class PraktikiInfoViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΑΜΚ { get; set; }

        [Display(Name = "Ονοματεπώνυμο")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια από")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νια έως")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες πρακτικής")]
        public Nullable<int> ΩΡΕΣ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }


        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }


        public Nullable<int> ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

    }
}