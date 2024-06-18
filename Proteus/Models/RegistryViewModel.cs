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
    public  class regStudentBebeosiViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Πρωτ.")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Για χρήση")]
        public string ΓΙΑ_ΧΡΗΣΗ { get; set; }

        [Display(Name = "Στρατολ.Γραφείο")]
        public string ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ { get; set; }

        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }
    }

    public class regPraktikiAitisiViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Display(Name = "Πρωτ.")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }
    }

    public class regPraktikiApofasiViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Display(Name = "Πρωτ.")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public int ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ { get; set; }
    }

    public class regPraktikiElegxosViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "IEK")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Display(Name = "Ελεγκτής")]
        public string ΕΛΕΓΚΤΗΣ { get; set; }

        [Display(Name = "Εξάμηνο")]
        public string ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]      
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public int ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ { get; set; }
    }

    public class regPraktikiParousiaViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }
    }

    public class regPraktikiPeratosiViewModel
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Φύλο")]
        public string GENDER { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "Πρωτ.")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        [Display(Name = "Περιγραφή")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }
    }

    public class regPraktikiStudentViewModel
    {
        [Display(Name = "Σχολ. έτος")]
        public string SY_TEXT { get; set; }

        [Display(Name = "Περίοδος")]
        public string ΠΕΡΙΟΔΟΣ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public int ΙΕΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Σχολική Μονάδα")]
        public string SCHOOL_NAME { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία από")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία έως")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΩΡΕΣ { get; set; }

        [Display(Name = "Αντικείμενο")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        [Display(Name = "Εργ. ΑΦΜ")]
        public string ΕΡΓΟΔΟΤΗΣ_ΑΦΜ { get; set; }

        [Display(Name = "Υπεύθυνος")]
        public string ΥΠΕΥΘΥΝΟΣ { get; set; }

        [Display(Name = "Τηλέφωνα")]
        public string ΤΗΛΕΦΩΝΑ { get; set; }

        [Display(Name = "Διεύθυνση")]
        public string ΔΙΕΥΘΥΝΣΗ { get; set; }

        public int STUDENT_ID { get; set; }
        public int SY_ID { get; set; }
        public int PERIOD_ID { get; set; }
        public int SCHOOL_ID { get; set; }
        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }
        public int ΕΡΓΟΔΟΤΗΣ_ΚΩΔ { get; set; }

    }


}