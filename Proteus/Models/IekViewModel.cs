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
    public class IekEidikotitesViewModel
    {
        public int IE_ID { get; set; }

        [Display(Name = "ΙΕΚ")]
        public int IEK_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα κατάρτισης")]
        public int EIDIKOTITA_ID { get; set; }

    }

    public class qryIekEidikotitesViewModel
    {

        [Display(Name = "ΙΕΚ")]
        public int IEK_ID { get; set; }
        public int EIDIKOTITA_ID { get; set; }

        [Display(Name = "Ειδικότητα κατάρτισης")]
        public string EIDIKOTITA_TEXT { get; set; }

    }

    public class LessonTypesViewModel
    {
        public int LESSON_TYPE_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Είδος (Θ/Ε)")]
        public string LESSON_TYPE_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Ετικέτα")]
        public string LESSON_TYPE_TAG { get; set; }

    }

    public class LessonsIekViewModel
    {
        public int LESSON_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(255, ErrorMessage = "Πρέπει να είναι μέχρι 255 χαρακτήρες.")]
        [Display(Name = "Μάθημα")]
        public string LESSON_TEXT { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εξάμηνο")]
        public Nullable<int> LESSON_TERM { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Θ/Ε")]
        public Nullable<int> LESSON_TYPE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Range(1, 30, ErrorMessage = "Η τιμή πρέπει να είναι μεταξύ 1 και 30")]
        [Display(Name = "Ώρες/εβδ")]
        public Nullable<int> LESSON_HOURS_WEEK { get; set; }

        [Display(Name = "Σύνολο")]
        public Nullable<int> LESSON_HOURS { get; set; }

        [Display(Name = "Ειδικότητα")]
        public Nullable<int> LESSON_EIDIKOTITA { get; set; }

    }

    public class LessonSelectorViewModel
    {
        public int LESSON_ID { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int LESSON_EIDIKOTITA { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int LESSON_TERM { get; set; }

    }

    public class IekLessonsViewModel
    {
        public int IE_ID { get; set; }
        public int IEK_ID { get; set; }
        public int LESSON_ID { get; set; }

        [Display(Name = "Μάθημα")]
        public string LESSON_DESC { get; set; }

        [Display(Name = "Ειδικότητα")]
        public int LESSON_EIDIKOTITA { get; set; }

        [Display(Name = "Εξάμηνο")]
        public int LESSON_TERM { get; set; }

    }

    public class TmimaViewModel
    {
        public int ΤΜΗΜΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(20, ErrorMessage = "Πρέπει να είναι μέχρι 20 χαρακτήρες.")]
        [Display(Name = "Όνομασία")]
        public string ΤΜΗΜΑ_ΟΝΟΜΑ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Ειδικότητα")]
        public Nullable<int> ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Εξάμηνο")]
        public Nullable<int> ΕΞΑΜΗΝΟ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Πρωί-Απόγευμα")]
        public Nullable<int> ΠΑ_ΚΩΔ { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Περίοδος")]
        public Nullable<int> ΠΕΡΙΟΔΟΣ_ΚΩΔ { get; set; }

        [Display(Name = "ΙΕΚ")]
        public Nullable<int> ΙΕΚ { get; set; }

    }

}