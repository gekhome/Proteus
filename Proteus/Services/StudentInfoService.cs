using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class StudentInfoService : IStudentInfoService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public StudentInfoService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentInfoViewModel> Read()
        {
            var students = (from d in entities.sqlSTUDENT_INFO
                            orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                            select new StudentInfoViewModel
                            {
                                STUDENT_ID = d.STUDENT_ID,
                                ΑΜΚ = d.ΑΜΚ,
                                ΙΕΚ = d.ΙΕΚ,
                                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                                ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                                ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                                ΑΦΜ = d.ΑΦΜ,
                                ΑΜΚΑ = d.ΑΜΚΑ,
                                ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                                ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ,
                                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ,
                                ΠΡΑΞΗ_ΕΞΟΔΟΥ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ,
                                ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = d.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ,
                                ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                                ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                                ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ
                            }).ToList();
            return students;
        }

        public IEnumerable<StudentInfoViewModel> Read(int schoolId)
        {
            var students = (from d in entities.sqlSTUDENT_INFO
                            where d.ΙΕΚ == schoolId
                            orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                            select new StudentInfoViewModel
                            {
                                STUDENT_ID = d.STUDENT_ID,
                                ΑΜΚ = d.ΑΜΚ,
                                ΙΕΚ = d.ΙΕΚ,
                                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                                ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                                ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                                ΑΦΜ = d.ΑΦΜ,
                                ΑΜΚΑ = d.ΑΜΚΑ,
                                ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                                ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ,
                                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ,
                                ΠΡΑΞΗ_ΕΞΟΔΟΥ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ,
                                ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = d.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ,
                                ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                                ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                                ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ
                            }).ToList();
            return students;
        }

        public StudentInfoViewModel GetRecord(int studentId)
        {
            var data = (from d in entities.sqlSTUDENT_INFO
                        where d.STUDENT_ID == studentId
                        select new StudentInfoViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΑΜΚ = d.ΑΜΚ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                            ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ,
                            ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = d.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ
                        }).FirstOrDefault();
            return data;
        }

        public IEnumerable<EgrafesInfoViewModel> GetEgrafes(int studentId)
        {
            List<EgrafesInfoViewModel> data = new List<EgrafesInfoViewModel>();

            data = (from d in entities.sqlEGRAFES_INFO
                    where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                    select new EgrafesInfoViewModel
                    {
                        ΜΕ_ΚΩΔ = d.ΜΕ_ΚΩΔ,
                        ΑΜΚ = d.ΑΜΚ,
                        ΙΕΚ = d.ΙΕΚ,
                        ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                        ΗΜΝΙΑ_ΕΓΓΡΑΦΗ = d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ,
                        ΗΜΝΙΑ_ΠΕΡΑΣ = d.ΗΜΝΙΑ_ΠΕΡΑΣ,
                        ΕΓΓΡΑΦΗ_ΕΙΔΟΣ_ΛΕΚΤΙΚΟ = d.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ_ΛΕΚΤΙΚΟ,
                        ΦΟΙΤΗΣΗ_ΛΕΚΤΙΚΟ = d.ΦΟΙΤΗΣΗ_ΛΕΚΤΙΚΟ
                    }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}