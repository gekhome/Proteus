using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TeacherAitisiService : ITeacherAitisiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TeacherAitisiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<TeacherAitiseisViewModel> Read(int teacherId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ
                        orderby d.ΕΚΠΑΙΔΕΥΤΕΣ.ΕΠΩΝΥΜΟ, d.ΕΚΠΑΙΔΕΥΤΕΣ.ΟΝΟΜΑ, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.TEACHER_ID == teacherId
                        select new TeacherAitiseisViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΦΜ = d.ΑΦΜ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΚΕΙΜΕΝΟ = d.ΚΕΙΜΕΝΟ,
                            ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true
                        }).ToList();
            return data;
        }

        public void Create(TeacherAitiseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ()
            {
                TEACHER_ID = teacherId,
                ΑΦΜ = Common.GetTeacherAfm(teacherId),
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΚΕΙΜΕΝΟ = data.ΚΕΙΜΕΝΟ,
                ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(TeacherAitiseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.TEACHER_ID = teacherId;
            entity.ΑΦΜ = Common.GetTeacherAfm(teacherId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΚΕΙΜΕΝΟ = data.ΚΕΙΜΕΝΟ;
            entity.ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherAitiseisViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherAitiseisViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΙΤΗΣΕΙΣ.Select(d => new TeacherAitiseisViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΑΦΜ = d.ΑΦΜ,
                TEACHER_ID = d.TEACHER_ID,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΚΕΙΜΕΝΟ = d.ΚΕΙΜΕΝΟ,
                ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}