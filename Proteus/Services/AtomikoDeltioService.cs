using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class AtomikoDeltioService : IAtomikoDeltioService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public AtomikoDeltioService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentAtomikoDeltioViewModel> Read(int studentId)
        {
            List<StudentAtomikoDeltioViewModel> data = new List<StudentAtomikoDeltioViewModel>();

            try
            {
                data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ
                        orderby d.ΗΜΕΡΟΜΗΝΙΑ descending, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new StudentAtomikoDeltioViewModel
                        {
                            ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΑΡΑΔΟΘΗΚΕ = d.ΠΑΡΑΔΟΘΗΚΕ ?? true
                        }).ToList();
            }
            catch { }

            return data;
        }

        public void Create(StudentAtomikoDeltioViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ entity = new ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΑΡΑΔΟΘΗΚΕ = data.ΠΑΡΑΔΟΘΗΚΕ
            };
            entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΑΔΚ_ΚΩΔ = entity.ΑΔΚ_ΚΩΔ;
        }

        public void Update(StudentAtomikoDeltioViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ entity = entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ.Find(data.ΑΔΚ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΠΑΡΑΔΟΘΗΚΕ = data.ΠΑΡΑΔΟΘΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentAtomikoDeltioViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ entity = entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ.Find(data.ΑΔΚ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentAtomikoDeltioViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΑΤΟΜΙΚΑΔΕΛΤΙΑ.Select(d => new StudentAtomikoDeltioViewModel
            {
                ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΑΡΑΔΟΘΗΚΕ = d.ΠΑΡΑΔΟΘΗΚΕ ?? true
            }).Where(d => d.ΑΔΚ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public List<AtomikoDeltioViewModel> GetGradesApousies(int studentId, int termId)
        {
            var data = (from d in entities.ATOMIKA_DELTIA
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.TERM_ID == termId
                        select new AtomikoDeltioViewModel
                        {
                            ROW_ID = d.ROW_ID,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ,
                            ΜΟΒΠ = d.ΜΟΒΠ,
                            ΜΟΤΕ = d.ΜΟΤΕ,
                            ΜΟΕΠ = d.ΜΟΕΠ,
                            ΒΕ = d.ΒΕ ?? 0.0M,
                            ΜΟ = d.ΜΟ,
                            ΣΥΝΟΛΟ_ΩΡΕΣ = d.ΣΥΝΟΛΟ_ΩΡΕΣ,
                            ΟΡΙΟ = d.ΟΡΙΟ,
                            ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                            LESSON_COUNT = d.LESSON_COUNT,
                            GRADES_SUM = d.GRADES_SUM
                        }).ToList();
            return data;
        }

        public List<adkStudentPraktikiViewModel> GetPraktikiData(int studentId)
        {
            var data = (from d in entities.adk_STUDENT_PRAKTIKI
                        where d.STUDENT_ID == studentId
                        select new adkStudentPraktikiViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΙΕΚ = d.ΙΕΚ
                        }).ToList();
            return data;
        }

        public adkStudentDataViewModel GetStudentData(int studentId)
        {
            var data = (from d in entities.adk_STUDENT_DATA
                        where d.STUDENT_ID == studentId
                        select new adkStudentDataViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΑΜΚ = d.ΑΜΚ,
                            ΔΗΜΟΤΗΣ = d.ΔΗΜΟΤΗΣ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ = d.ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ,
                            ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ = d.ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ = d.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ ?? false,
                            ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ = d.ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ ?? false,
                            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ = d.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ ?? false,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ,
                            ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = d.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ,
                            ΤΟΠΟΣ_ΓΕΝΝΗΣΗ = d.ΤΟΠΟΣ_ΓΕΝΝΗΣΗ,
                            ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ = d.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΦΥΛΟ_ΚΕΙΜΕΝΟ = "",
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΕΙΜΕΝΟ = ""
                        }).FirstOrDefault();

            data.ΦΥΛΟ_ΚΕΙΜΕΝΟ = GetGenderText(data.ΦΥΛΟ);
            data.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΕΙΜΕΝΟ = GetPraxiExitText(data.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ);

            return data;
        }

        private string GetGenderText(int? gender)
        {
            string gender_text = "";
            var data = (from d in entities.SYS_GENDERS where d.GENDER_ID == gender select d).FirstOrDefault();
            if (data != null) gender_text = data.GENDER;
            return (gender_text);
        }

        private string GetPraxiExitText(int? praxi_type)
        {
            string praxi_text = "";
            var data = (from d in entities.ΠΡΑΞΕΙΣ_ΕΞΟΔΟΥ where d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΩΔ == praxi_type select d).FirstOrDefault();
            if (data != null) praxi_text = data.ΠΡΑΞΗ_ΕΞΟΔΟΥ;
            return (praxi_text);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}