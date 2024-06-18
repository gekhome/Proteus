using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TeacherAnalipsiService : ITeacherAnalipsiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TeacherAnalipsiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<TeacherAnalipsiViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.PERIOD_ID descending, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new TeacherAnalipsiViewModel
                        {
                            ΑΝΑΛΗΨΗ_ΚΩΔ = d.ΑΝΑΛΗΨΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            PERIOD_ID = d.PERIOD_ID,
                            ΥΠΕΓΡΑΦΗ = d.ΥΠΕΓΡΑΦΗ ?? true
                        }).ToList();
            return data;
        }

        public void Create(TeacherAnalipsiViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ()
            {
                TEACHER_ID = data.TEACHER_ID,
                ΑΦΜ = Common.GetTeacherAfm((int)data.TEACHER_ID),
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                PERIOD_ID = data.PERIOD_ID,
                ΥΠΕΓΡΑΦΗ = data.ΥΠΕΓΡΑΦΗ
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΑΛΗΨΗ_ΚΩΔ = entity.ΑΝΑΛΗΨΗ_ΚΩΔ;
        }

        public void Update(TeacherAnalipsiViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ.Find(data.ΑΝΑΛΗΨΗ_ΚΩΔ);

            entity.TEACHER_ID = data.TEACHER_ID;
            entity.ΑΦΜ = Common.GetTeacherAfm((int)data.TEACHER_ID);
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.PERIOD_ID = data.PERIOD_ID;
            entity.ΥΠΕΓΡΑΦΗ = data.ΥΠΕΓΡΑΦΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherAnalipsiViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ.Find(data.ΑΝΑΛΗΨΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherAnalipsiViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΛΗΨΕΙΣ.Select(d => new TeacherAnalipsiViewModel
            {
                ΑΝΑΛΗΨΗ_ΚΩΔ = d.ΑΝΑΛΗΨΗ_ΚΩΔ,
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                PERIOD_ID = d.PERIOD_ID,
                ΥΠΕΓΡΑΦΗ = d.ΥΠΕΓΡΑΦΗ ?? true
            }).Where(d => d.ΑΝΑΛΗΨΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}