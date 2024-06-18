using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiRegistryService : IPraktikiRegistryService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiRegistryService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<regPraktikiStudentViewModel> ReadStudents(int schoolId)
        {
            var data = (from d in entities.sqlSTUDENTS_PRAKTIKI_REGISTRY
                        where d.SCHOOL_ID == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiStudentViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SY_TEXT = d.SY_TEXT,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ,
                            PERIOD_ID = d.PERIOD_ID,
                            SY_ID = d.SY_ID,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            SCHOOL_ID = d.SCHOOL_ID,
                        }).ToList();
            return data;
        }

        public List<regPraktikiAitisiViewModel> ReadAitiseis(int schoolId)
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΕΙΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new regPraktikiAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiApofasiViewModel> ReadApofaseis(int schoolId)
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΕΙΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiApofasiViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiParousiaViewModel> ReadParousies(int schoolId)
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΕΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiParousiaViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiPeratosiViewModel> ReadPeratoseis(int schoolId)
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiPeratosiViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiElegxosViewModel> ReadElegxoi(int schoolId)
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΙ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiElegxosViewModel
                        {
                            ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = d.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = d.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ,
                            ΕΛΕΓΚΤΗΣ = d.ΕΛΕΓΚΤΗΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiStudentViewModel> ReadStudents()
        {
            var data = (from d in entities.sqlSTUDENTS_PRAKTIKI_REGISTRY
                        orderby d.SCHOOL_NAME, d.SY_TEXT, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiStudentViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            SY_TEXT = d.SY_TEXT,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ,
                            PERIOD_ID = d.PERIOD_ID,
                            SY_ID = d.SY_ID,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            SCHOOL_ID = d.SCHOOL_ID,
                        }).ToList();
            return data;
        }

        public List<regPraktikiAitisiViewModel> ReadAitiseis()
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΕΙΣ
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new regPraktikiAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiApofasiViewModel> ReadApofaseis()
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΕΙΣ
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiApofasiViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiParousiaViewModel> ReadParousies()
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΕΣ
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiParousiaViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiPeratosiViewModel> ReadPeratoseis()
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiPeratosiViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public List<regPraktikiElegxosViewModel> ReadElegxoi()
        {
            var data = (from d in entities.regΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΙ
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new regPraktikiElegxosViewModel
                        {
                            ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ,
                            STUDENT_ID = d.STUDENT_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            GENDER = d.GENDER,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = d.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ,
                            ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = d.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ,
                            ΕΛΕΓΚΤΗΣ = d.ΕΛΕΓΚΤΗΣ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}