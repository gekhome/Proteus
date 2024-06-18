using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class AnatheseisService : IAnatheseisService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public AnatheseisService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TeacherAnatheseisViewModel> Read(int teacherId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ
                        where d.TEACHER_ID == teacherId
                        orderby d.ΕΙΔΙΚΟΤΗΤΑ, d.ΕΞΑΜΗΝΟ, d.ΜΑΘΗΜΑ_ΚΩΔ
                        select new TeacherAnatheseisViewModel
                        {
                            ΕΑ_ΚΩΔ = d.ΕΑ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                            ΜΑΘΗΜΑ_ΚΩΔ = d.ΜΑΘΗΜΑ_ΚΩΔ ?? 0,
                            ΩΡΕΣ_ΘΕΩΡΙΑ = d.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                            ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = d.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0,
                            ΣΥΝΟΛΟ = d.ΣΥΝΟΛΟ ?? 0,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                            ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ
                        }).ToList();
            return data;
        }

        public void Create(TeacherAnatheseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ()
            {
                ΑΦΜ = Common.GetTeacherAfm(teacherId),
                ΙΕΚ = schoolId,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΜΑΘΗΜΑ_ΚΩΔ = data.ΜΑΘΗΜΑ_ΚΩΔ,
                ΩΡΕΣ_ΘΕΩΡΙΑ = data.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = data.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0,
                ΣΥΝΟΛΟ = (short)(data.ΩΡΕΣ_ΘΕΩΡΙΑ + data.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ),
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ,
                ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ,
                TEACHER_ID = teacherId
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΕΑ_ΚΩΔ = entity.ΕΑ_ΚΩΔ;
        }

        public void Update(TeacherAnatheseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Find(data.ΕΑ_ΚΩΔ);

            entity.ΑΦΜ = Common.GetTeacherAfm(teacherId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ;
            entity.ΜΑΘΗΜΑ_ΚΩΔ = data.ΜΑΘΗΜΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΘΕΩΡΙΑ = data.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0;
            entity.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = data.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0;
            entity.ΣΥΝΟΛΟ = (short)(data.ΩΡΕΣ_ΘΕΩΡΙΑ + data.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ);
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ;
            entity.TEACHER_ID = teacherId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherAnatheseisViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Find(data.ΕΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherAnatheseisViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Select(d => new TeacherAnatheseisViewModel
            {
                ΕΑ_ΚΩΔ = d.ΕΑ_ΚΩΔ,
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΜΑΘΗΜΑ_ΚΩΔ = d.ΜΑΘΗΜΑ_ΚΩΔ ?? 0,
                ΩΡΕΣ_ΘΕΩΡΙΑ = d.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = d.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0,
                ΣΥΝΟΛΟ = d.ΣΥΝΟΛΟ ?? 0,
                ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ
            }).Where(d => d.ΕΑ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<QueryAnatheseisViewModel> View(int schoolId)
        {
            var data = (from d in entities.sqlANATHESEIS_VIEW
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΠΕΡΙΟΔΟΣ, d.EIDIKOTITA_TEXT
                        select new QueryAnatheseisViewModel
                        {
                            ΕΑ_ΚΩΔ = d.ΕΑ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_DESC = d.EIDIKOTITA_DESC,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_DESC = d.LESSON_DESC,
                            ΩΡΕΣ_ΘΕΩΡΙΑ = d.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                            ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = d.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0,
                            SCHOOL_NAME = d.SCHOOL_NAME
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}