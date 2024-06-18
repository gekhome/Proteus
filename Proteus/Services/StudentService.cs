using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class StudentService : IStudentService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public StudentService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentGridViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                        select new StudentGridViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΑΜΚ = d.ΑΜΚ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 1
                        }).ToList();
            return data;
        }

        public void Create(StudentGridViewModel data, int schoolId)
        {
            ΜΑΘΗΤΕΣ entity = new ΜΑΘΗΤΕΣ()
            {
                ΑΜΚ = data.ΑΜΚ,
                ΑΦΜ = data.ΑΦΜ,
                ΙΕΚ = schoolId,
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim(),
                ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim(),
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ
            };
            entities.ΜΑΘΗΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.STUDENT_ID = entity.STUDENT_ID;
        }

        public void Update(StudentGridViewModel data, int schoolId)
        {
            ΜΑΘΗΤΕΣ entity = entities.ΜΑΘΗΤΕΣ.Find(data.STUDENT_ID);

            if (data.ΑΜΚ != entity.ΑΜΚ)
            {
                UpdateEgrafesAmk(data.STUDENT_ID, data.ΑΜΚ);
            }
            entity.ΑΜΚ = data.ΑΜΚ;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΙΕΚ = schoolId;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentGridViewModel data)
        {
            ΜΑΘΗΤΕΣ entity = entities.ΜΑΘΗΤΕΣ.Find(data.STUDENT_ID);

            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΜΑΘΗΤΕΣ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public StudentGridViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ.Select(d => new StudentGridViewModel
            {
                STUDENT_ID = d.STUDENT_ID,
                ΑΜΚ = d.ΑΜΚ,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 1
            }).Where(d => d.STUDENT_ID.Equals(entityId)).FirstOrDefault();
        }

        public StudentViewModel GetRecord(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ
                        where d.STUDENT_ID == studentId
                        select new StudentViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΑΜΚ = d.ΑΜΚ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = d.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ,
                            ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = d.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ,
                            ΓΕΝΟΣ = d.ΓΕΝΟΣ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΤΟΠΟΣ_ΓΕΝΝΗΣΗ = d.ΤΟΠΟΣ_ΓΕΝΝΗΣΗ,
                            ΝΟΜΟΣ_ΓΕΝΝΗΣΗ = d.ΝΟΜΟΣ_ΓΕΝΝΗΣΗ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΔΟΥ = d.ΔΟΥ,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΑΜΑ = d.ΑΜΑ,
                            ΑΔΤ = d.ΑΔΤ,
                            ΔΗΜΟΤΗΣ = d.ΔΗΜΟΤΗΣ,
                            ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ = d.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ,
                            ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ = d.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ,
                            ΥΠΗΚΟΟΤΗΤΑ = d.ΥΠΗΚΟΟΤΗΤΑ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΑΡ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΑΡ,
                            ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΕΙΔΟΣ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΕΙΔΟΣ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΗΜΝΙΑ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΗΜΝΙΑ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΑΡ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΑΡ,
                            ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ,
                            ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = d.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ = d.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ ?? false,
                            ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ = d.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ ?? false,
                            ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ = d.ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ,
                            ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ = d.ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ ?? false,
                            ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ = d.ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ = d.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ,
                            ΠΤΥΧΙΟ_ΙΕΚ_ΑΛΛΟ = d.ΠΤΥΧΙΟ_ΙΕΚ_ΑΛΛΟ ?? false,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ ?? 0,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                            ΜΗΧΑΝΟΓΡΑΦΙΚΟ = d.ΜΗΧΑΝΟΓΡΑΦΙΚΟ ?? false,
                            ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                            ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                            ΕΞΕΤΑΣΗ_ΚΩΔΙΚΟΣ = d.ΕΞΕΤΑΣΗ_ΚΩΔΙΚΟΣ,
                            ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΑ = d.ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΑ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(StudentViewModel data, int studentId, int schoolId = 0)
        {
            ΜΑΘΗΤΕΣ entity = entities.ΜΑΘΗΤΕΣ.Find(studentId);

            entity.ΑΜΚ = data.ΑΜΚ;
            if (schoolId > 0)
            {
                entity.ΙΕΚ = schoolId;
            }
            else
            {
                entity.ΙΕΚ = data.ΙΕΚ;
            }
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = data.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = data.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ.Trim();
            entity.ΓΕΝΟΣ = data.ΓΕΝΟΣ.Trim();
            entity.ΦΥΛΟ = data.ΦΥΛΟ;
            entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = data.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            entity.ΤΟΠΟΣ_ΓΕΝΝΗΣΗ = data.ΤΟΠΟΣ_ΓΕΝΝΗΣΗ;
            entity.ΝΟΜΟΣ_ΓΕΝΝΗΣΗ = data.ΝΟΜΟΣ_ΓΕΝΝΗΣΗ;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΔΟΥ = data.ΔΟΥ;
            entity.ΑΜΚΑ = data.ΑΜΚΑ;
            entity.ΑΜΑ = data.ΑΜΑ;
            entity.ΑΔΤ = data.ΑΔΤ;
            entity.ΔΗΜΟΤΗΣ = data.ΔΗΜΟΤΗΣ;
            entity.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ = data.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ;
            entity.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ = data.ΜΗΤΡΩΟ_ΑΡΡΕΝΩΝ_ΑΡ;
            entity.ΥΠΗΚΟΟΤΗΤΑ = data.ΥΠΗΚΟΟΤΗΤΑ;
            entity.ΔΙΕΥΘΥΝΣΗ = data.ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = data.EMAIL;
            entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ;
            entity.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ = data.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ;
            entity.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΑΡ = data.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΑΡ;
            entity.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΕΙΔΟΣ = data.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΕΙΔΟΣ;
            entity.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΗΜΝΙΑ = data.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΗΜΝΙΑ;
            entity.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΑΡ = data.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΑΡ;
            entity.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ = data.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΕΙΔΟΣ;
            entity.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ = data.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ;
            entity.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ = data.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ;
            entity.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ = data.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ;
            entity.ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ = data.ΠΡ_ΑΠΑΛΛΑΓΗ_ΩΡΕΣ;
            entity.ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ = data.ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ;
            entity.ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ = data.ΠΡ_ΔΙΑΚΟΠΗ_ΗΜΝΙΑ;
            entity.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ = data.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ;
            entity.ΑΠΟ_ΚΑΤΑΤΑΞΗ = data.ΑΠΟ_ΚΑΤΑΤΑΞΗ;
            entity.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ;
            entity.ΠΤΥΧΙΟ_ΙΕΚ_ΑΛΛΟ = data.ΠΤΥΧΙΟ_ΙΕΚ_ΑΛΛΟ;
            entity.ΗΛΙΚΙΑ = (short)Common.CalculateAge(entity);
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            entity.ΜΗΧΑΝΟΓΡΑΦΙΚΟ = data.ΜΗΧΑΝΟΓΡΑΦΙΚΟ;
            entity.ΕΞΕΤΑΣΗ_ΚΩΔΙΚΟΣ = data.ΕΞΕΤΑΣΗ_ΚΩΔΙΚΟΣ;
            entity.ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΑ = data.ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        private void UpdateEgrafesAmk(int studentID, int amk)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentID select d).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    item.ΑΜΚ = amk;
                    entities.Entry(item).State = EntityState.Modified;
                    entities.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}