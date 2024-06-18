using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class BekService : IBekService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public BekService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentBekViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΕΚ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        orderby d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ
                        select new StudentBekViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = d.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ ?? false,
                            ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                            ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                            ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                            ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = d.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ,
                            ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ,
                            ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΚΔΟΣΗ = d.ΕΚΔΟΣΗ ?? true,
                            ΕΞΑΜΗΝΑ = d.ΕΞΑΜΗΝΑ,
                            ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                            ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ
                        }).ToList();
            return data;
        }

        public void Create(StudentBekViewModel data, int studentId, int schoolId)
        {
            data = SetStudentFields(data, studentId);

            ΜΑΘΗΤΕΣ_ΒΕΚ entity = new ΜΑΘΗΤΕΣ_ΒΕΚ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ,
                ΑΜΚ = Common.GetStudentAmk(studentId),
                ΙΕΚ = schoolId,
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ,
                ΕΞΑΜΗΝΑ = data.ΕΞΑΜΗΝΑ,
                ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ,
                ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ,
                EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT,
                ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = data.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = data.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ,
                ΑΠΟ_ΚΑΤΑΤΑΞΗ = data.ΑΠΟ_ΚΑΤΑΤΑΞΗ,
                ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ,
                ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = data.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = data.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                ΕΚΔΟΣΗ = data.ΕΚΔΟΣΗ,
                ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = Common.GetEidikotitaHours((int)data.ΕΙΔΙΚΟΤΗΤΑ),
                ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = Common.GetHoursTheory(data),
                ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = Common.GetHoursPraktiki(data)
            };
            entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Add(entity);
            entities.SaveChanges();

            data.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = entity.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ;
        }

        public void Update(StudentBekViewModel data, int studentId, int schoolId)
        {
            data = SetStudentFields(data, studentId);

            ΜΑΘΗΤΕΣ_ΒΕΚ entity = entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Find(data.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ;
            entity.ΑΜΚ = Common.GetStudentAmk(studentId);
            entity.ΙΕΚ = schoolId;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΕΞΑΜΗΝΑ = data.ΕΞΑΜΗΝΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
            entity.EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT;
            entity.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = data.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ;
            entity.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = data.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ;
            entity.ΑΠΟ_ΚΑΤΑΤΑΞΗ = data.ΑΠΟ_ΚΑΤΑΤΑΞΗ;
            entity.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ;
            entity.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = data.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ;
            entity.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = data.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ;
            entity.ΕΚΔΟΣΗ = data.ΕΚΔΟΣΗ;
            entity.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = Common.GetEidikotitaHours((int)data.ΕΙΔΙΚΟΤΗΤΑ);
            entity.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = Common.GetHoursTheory(data);
            entity.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = Common.GetHoursPraktiki(data);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentBekViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΒΕΚ entity = entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Find(data.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentBekViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Select(d => new StudentBekViewModel
            {
                ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΙΕΚ = d.ΙΕΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = d.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ ?? false,
                ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = d.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ,
                ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ,
                ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ,
                ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                ΕΚΔΟΣΗ = d.ΕΚΔΟΣΗ ?? true,
                ΕΞΑΜΗΝΑ = d.ΕΞΑΜΗΝΑ,
                ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ
            }).Where(d => d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<StudentBekViewModel> ReadAll(int schoolId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΕΚ
                        where d.ΙΕΚ == schoolId
                        orderby d.EIDIKOTITA_TEXT, d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ
                        select new StudentBekViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΒΕΚ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = d.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ ?? false,
                            ΑΠΟ_ΚΑΤΑΤΑΞΗ = d.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false,
                            ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = d.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false,
                            ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = d.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ,
                            ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = d.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ,
                            ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ,
                            ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = d.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΚΔΟΣΗ = d.ΕΚΔΟΣΗ ?? true,
                            ΕΞΑΜΗΝΑ = d.ΕΞΑΜΗΝΑ,
                            ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = d.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ,
                            ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = d.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ
                        }).ToList();
            return data;
        }

        public void UpdateRecord(StudentBekViewModel data, int bekId, int schoolId)
        {
            StudentBekViewModel bek = Refresh(bekId);

            ΜΑΘΗΤΕΣ_ΒΕΚ entity = entities.ΜΑΘΗΤΕΣ_ΒΕΚ.Find(bekId);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = bek.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΙΕΚ = schoolId;
            entity.ΑΜΚ = bek.ΑΜΚ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = bek.ΕΙΔΙΚΟΤΗΤΑ;
            entity.EIDIKOTITA_TEXT = bek.EIDIKOTITA_TEXT;
            entity.ΚΩΔ_ΤΜΗΜΑ = bek.ΚΩΔ_ΤΜΗΜΑ;
            entity.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ = bek.ΔΙΑΡΚΕΙΑ_ΘΕΩΡΙΑ;
            entity.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ = bek.ΔΙΑΡΚΕΙΑ_ΠΡΑΚΤΙΚΗ;
            entity.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ = bek.ΔΙΑΡΚΕΙΑ_ΣΥΝΟΛΙΚΗ;
            entity.ΕΚΔΟΣΗ = bek.ΕΚΔΟΣΗ;
            entity.ΕΞΑΜΗΝΑ = data.ΕΞΑΜΗΝΑ;

            entity.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ = data.ΑΡ_ΒΙΒΛΙΟΥ_ΒΕΚ;
            entity.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ = data.ΗΜΝΙΑ_ΕΚΔΟΣΗΣ_ΒΕΚ;
            entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = data.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ;
            entity.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = data.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ;
            entity.ΑΠΟ_ΚΑΤΑΤΑΞΗ = data.ΑΠΟ_ΚΑΤΑΤΑΞΗ;
            entity.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        private StudentBekViewModel SetStudentFields(StudentBekViewModel data, int studentId)
        {
            var info = (from d in entities.qrySTUDENT_BEK_SELECTOR where d.STUDENT_ID == studentId select d).FirstOrDefault();

            data.ΕΙΔΙΚΟΤΗΤΑ = info.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            data.ΚΩΔ_ΤΜΗΜΑ = info.ΚΩΔ_ΤΜΗΜΑ;
            data.EIDIKOTITA_TEXT = info.EIDIKOTITA_TEXT;
            data.ΑΠΑΛΛΑΓΗ_ΠΡΑΚΤΙΚΗΣ = info.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ ?? false;
            data.ΑΠΟ_ΚΑΤΑΤΑΞΗ = info.ΑΠΟ_ΚΑΤΑΤΑΞΗ ?? false;
            data.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ = info.ΜΕ_ΕΠΑΡΚΕΙΑ_ΩΡΕΣ ?? false;
            data.ΕΠΩΝΥΜΟ_ΟΝΟΜΑ = info.ΟΝΟΜΑΤΕΠΩΝΥΜΟ;
            data.ΜΗΤΡΩΝΥΜΟ = info.ΜΗΤΡΩΝΥΜΟ;
            data.ΠΑΤΡΩΝΥΜΟ = info.ΠΑΤΡΩΝΥΜΟ;
            data.ΕΞΑΜΗΝΑ = info.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ == 0 ? 5 : info.ΣΠΟΥΔΕΣ_ΔΙΑΡΚΕΙΑ;

            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}