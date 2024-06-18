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
    public class PraktikiAitisiViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Υποβλήθηκε")]
        public bool ΥΠΟΒΛΗΘΗΚΕ { get; set; }

        [Display(Name = "Εργοδότης")]
        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΤΜΗΜΑ_ΚΩΔ { get; set; }

    }

    public class PraktikiApofasiViewModel
    {
        public int ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Display(Name = "Εργοδότης")]
        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημ/νία έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες πρακτικής")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "Αίτηση")]
        public Nullable<int> ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΤΜΗΜΑ_ΚΩΔ { get; set; }

    }

    public class PraktikiElegxosViewModel
    {
        public int ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Display(Name = "Εργοδότης")]
        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ελεγκτής")]
        public string ΕΛΕΓΚΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εξάμηνο")]
        public Nullable<int> ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

    }

    public class PraktikiParousiaViewModel
    {
        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Display(Name = "Εργοδότης")]
        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ { get; set; }

        [Display(Name = "Παρελήφθηκε")]
        public bool ΠΑΡΑΛΑΒΗ { get; set; }

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

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΤΜΗΜΑ_ΚΩΔ { get; set; }

    }

    public class PraktikiPeratosiViewModel
    {
        public int ΒΕΒΑΙΩΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public Nullable<int> ΑΜΚ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωτόκολλο")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Από")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΑΠΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Έως")]
        public Nullable<System.DateTime> ΠΡΑΚΤΙΚΗ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες πρακτικής")]
        public Nullable<int> ΠΡΑΚΤΙΚΗ_ΩΡΕΣ { get; set; }

        [Display(Name = "Παραδόθηκε")]
        public bool ΠΑΡΑΔΟΔΗΚΕ { get; set; }

        [Display(Name = "Εργοδότης")]
        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        [Display(Name = "Τμήμα")]
        public Nullable<int> ΤΜΗΜΑ_ΚΩΔ { get; set; }

    }

    public class StudentPraktikiSelector
    {
        public int STUDENT_ID { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        public int ΑΜΚ { get; set; }
        public int ΙΕΚ { get; set; }

        public int ΚΩΔ_ΤΜΗΜΑ { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int ΕΞΑΜΗΝΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

    }

    public class StudentInPraktikiViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

        public int STUDENT_ID { get; set; }

        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }

        public int ΑΜΚ { get; set; }

        public int ΙΕΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Τμήμα")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }


        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        public Nullable<int> ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Ημ/νια από")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [Display(Name = "Ημ/νια έως")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Display(Name = "ώρες")]
        public Nullable<int> ΩΡΕΣ { get; set; }

    }

    public class AitiseisPraktikisViewModel
    {
        public int ΑΙΤΗΣΗ_ΚΩΔ { get; set; }

        [Display(Name = "Αρ.Πρωτ.")]
        public Nullable<int> ΠΡΩΤΟΚΟΛΛΟ { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> ΗΜΕΡΟΜΗΝΙΑ { get; set; }

        public Nullable<int> ΙΕΚ { get; set; }

        public Nullable<int> ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public string EIDIKOTITA_TEXT { get; set; }

        public Nullable<int> ΕΡΓΟΔΟΤΗΣ { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΩΣ { get; set; }

        public Nullable<int> ΩΡΕΣ { get; set; }

        [Display(Name = "Αντικείμενο")]
        public string ΠΕΡΙΓΡΑΦΗ { get; set; }

        public Nullable<int> ΜΑΘΗΤΗΣ_ΚΩΔ { get; set; }

        public Nullable<int> ΤΜΗΜΑ_ΚΩΔ { get; set; }

        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }

    }

    #region NEW ADDITIONS 2018-06-02

    public class PraktikiExploreViewModel
    {
        public int ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ { get; set; }
        public int STUDENT_ID { get; set; }
        public int ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Εξάμ.")]
        public Nullable<int> ΕΞΑΜΗΝΟ { get; set; }

        [Display(Name = "Περίοδος")]
        public Nullable<int> ΠΕΡΙΟΔΟΣ_ΚΩΔ { get; set; }

        public Nullable<int> ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ { get; set; }

        [Display(Name = "Εργοδότης")]
        public string ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ { get; set; }

        [Display(Name = "Έναρξη")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΑΠΟ { get; set; }

        [Display(Name = "Λήξη")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> ΗΜΝΙΑ_ΕΩΣ { get; set; }

        [Display(Name = "Ώρες")]
        public Nullable<int> ΩΡΕΣ { get; set; }

        [Display(Name = "Περάτωση")]
        public string ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ { get; set; }
    }

    public class PraktikiApallagiViewModel
    {
        public int STUDENT_ID { get; set; }

        public int ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Απαλλαγή")]
        public string ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ { get; set; }

        [Display(Name = "Σχόλιο (πρακτική)")]
        public string ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ { get; set; }
    }

    public class PraktikiDiakopiViewModel
    {
        public int STUDENT_ID { get; set; }

        public int ΙΕΚ { get; set; }

        [Display(Name = "ΑΜΚ")]
        public int ΑΜΚ { get; set; }

        [Display(Name = "Σπουδαστής")]
        public string ΟΝΟΜΑΤΕΠΩΝΥΜΟ { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ { get; set; }

        [Display(Name = "Διακοπή")]
        public string ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ { get; set; }

        [Display(Name = "Σχόλιο (πρακτική)")]
        public string ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ { get; set; }
    }

    #endregion NEW ADDITIONS 2018-06-02
}