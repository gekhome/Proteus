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
    public class UploadsViewModel
    {
        public int UPLOAD_ID { get; set; }

        [Display(Name = "Εκπαιδευτικός")]
        public string STUDENT_AFM { get; set; }

        [Display(Name = "Πρόσκληση")]
        public Nullable<int> EGYKLIOS_ID { get; set; }

        [Display(Name = "Αίτηση")]
        public Nullable<int> AITISI_ID { get; set; }

        [Display(Name = "Σχολείο")]
        public Nullable<int> SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> UPLOAD_DATE { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Ονοματεπώνυμο")]
        public string UPLOAD_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Περιγραφή αρχείων")]
        public string UPLOAD_SUMMARY { get; set; }
    }

    public class UploadsFilesViewModel
    {
        [Display(Name = "Κωδικός")]
        public string ID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Φάκελος (ΙΕΚ)")]
        public string SCHOOL_USER { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [StringLength(120, ErrorMessage = "Πρέπει να είναι μέχρι 120 χαρακτήρες.")]
        [Display(Name = "Όνομα αρχείου")]
        public string FILENAME { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Επέκταση")]
        public string EXTENSION { get; set; }

        public Nullable<int> UPLOAD_ID { get; set; }

        public virtual ΧΜ_UPLOADS ΧΜ_UPLOADS { get; set; }
    }

}