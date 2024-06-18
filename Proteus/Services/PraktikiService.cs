using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiService : IPraktikiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ErgodotesPraktikiViewModel> Read(int ergodotisId, int schoolId)
        {
            var data = (from d in entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ
                        where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId && d.ΙΕΚ == schoolId
                        select new ErgodotesPraktikiViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΜΑΘΗΤΗΣ_ΑΜΚ = d.ΜΑΘΗΤΗΣ_ΑΜΚ ?? 0,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ ?? 0,
                            ΑΝΤΙΚΕΙΜΕΝΟ = d.ΑΝΤΙΚΕΙΜΕΝΟ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
                        }).ToList();
            return data;
        }

        public void Create(ErgodotesPraktikiViewModel data, int ergodotisId, int schoolId)
        {
            ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ entity = new ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ()
            {
                ΤΜΗΜΑ_ΚΩΔ = data.ΤΜΗΜΑ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = ergodotisId,
                ΙΕΚ = schoolId,
                ΜΑΘΗΤΗΣ_ΑΜΚ = Common.GetStudentAmk(data.ΜΑΘΗΤΗΣ_ΚΩΔ),
                ΗΜΝΙΑ_ΑΠΟ = data.ΗΜΝΙΑ_ΑΠΟ,
                ΗΜΝΙΑ_ΕΩΣ = data.ΗΜΝΙΑ_ΕΩΣ,
                ΩΡΕΣ = data.ΩΡΕΣ,
                ΑΝΤΙΚΕΙΜΕΝΟ = data.ΑΝΤΙΚΕΙΜΕΝΟ,
                ΠΕΡΙΓΡΑΦΗ = data.ΠΕΡΙΓΡΑΦΗ,
                ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = data.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
            };
            entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Add(entity);
            entities.SaveChanges();

            data.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = entity.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ;
        }

        public void Update(ErgodotesPraktikiViewModel data, int ergodotisId, int schoolId)
        {
            ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ entity = entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Find(data.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ);

            entity.ΤΜΗΜΑ_ΚΩΔ = data.ΤΜΗΜΑ_ΚΩΔ;
            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = ergodotisId;
            entity.ΙΕΚ = schoolId;
            entity.ΜΑΘΗΤΗΣ_ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            entity.ΗΜΝΙΑ_ΑΠΟ = data.ΗΜΝΙΑ_ΑΠΟ;
            entity.ΗΜΝΙΑ_ΕΩΣ = data.ΗΜΝΙΑ_ΕΩΣ;
            entity.ΩΡΕΣ = data.ΩΡΕΣ;
            entity.ΑΝΤΙΚΕΙΜΕΝΟ = data.ΑΝΤΙΚΕΙΜΕΝΟ;
            entity.ΠΕΡΙΓΡΑΦΗ = data.ΠΕΡΙΓΡΑΦΗ;
            entity.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = data.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ErgodotesPraktikiViewModel data)
        {
            ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ entity = entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Find(data.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ErgodotesPraktikiViewModel Refresh(int entityId)
        {
            return entities.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Select(d => new ErgodotesPraktikiViewModel
            {
                ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΜΑΘΗΤΗΣ_ΑΜΚ = d.ΜΑΘΗΤΗΣ_ΑΜΚ ?? 0,
                ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                ΩΡΕΣ = d.ΩΡΕΣ ?? 0,
                ΑΝΤΙΚΕΙΜΕΝΟ = d.ΑΝΤΙΚΕΙΜΕΝΟ,
                ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
            }).Where(d => d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<PraktikiInfoViewModel> ReadInfo(int ergodotisId)
        {
            var data = (from d in entities.sqlPRAKTIKI_INFO
                        where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId
                        select new PraktikiInfoViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΜΑΘΗΤΗΣ_ΑΜΚ = d.ΜΑΘΗΤΗΣ_ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ
                        }).ToList();
            return data;
        }

        public PraktikiInfoViewModel GetInfo(int praktikiId)
        {
            return entities.sqlPRAKTIKI_INFO.Select(d => new PraktikiInfoViewModel
            {
                ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΜΑΘΗΤΗΣ_ΑΜΚ = d.ΜΑΘΗΤΗΣ_ΑΜΚ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                ΩΡΕΣ = d.ΩΡΕΣ,
                ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ
            }).Where(d => d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ.Equals(praktikiId)).FirstOrDefault();
        }

        public IEnumerable<StudentInPraktikiViewModel> ReadStudents(int schoolId)
        {
            var students = (from d in entities.sqlSTUDENTS_IN_PRAKTIKI
                            where d.ΙΕΚ == schoolId
                            orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΤΜΗΜΑ_ΚΩΔ, d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ
                            select new StudentInPraktikiViewModel
                            {
                                ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                                STUDENT_ID = d.STUDENT_ID,
                                ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                                ΙΕΚ = d.ΙΕΚ,
                                ΑΜΚ = d.ΑΜΚ,
                                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                                ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                                ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                                ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                                ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                                ΩΡΕΣ = d.ΩΡΕΣ
                            }).ToList();
            return students;
        }

        public StudentInPraktikiViewModel GetStudent(int ergodotisId, int studentId, int tmimaId)
        {
            var student = (from d in entities.sqlSTUDENTS_IN_PRAKTIKI
                           where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId && d.STUDENT_ID == studentId && d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                           select new StudentInPraktikiViewModel
                           {
                               ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                               STUDENT_ID = d.STUDENT_ID,
                               ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                               ΙΕΚ = d.ΙΕΚ,
                               ΑΜΚ = d.ΑΜΚ,
                               ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                               ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                               EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                               ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                               ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                               ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                               ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                               ΩΡΕΣ = d.ΩΡΕΣ
                           }).FirstOrDefault();
            return student;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}