using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class AitiseisService : IAitiseisService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public AitiseisService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentAitisiViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ
                        orderby d.ΗΜΕΡΟΜΗΝΙΑ descending, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new StudentAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΚΕΙΜΕΝΟ = d.ΚΕΙΜΕΝΟ,
                            ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true
                        }).ToList();
            return data;
        }

        public void Create(StudentAitisiViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ entity = new ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΑΜΚ = Common.GetStudentAmk(studentId),
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ
            };
            entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(StudentAitisiViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΑΜΚ = Common.GetStudentAmk(studentId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentAitisiViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentAitisiViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ.Select(d => new StudentAitisiViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΚΕΙΜΕΝΟ = d.ΚΕΙΜΕΝΟ,
                ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public StudentAitisiViewModel GetRecord(int aitisiId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΙΤΗΣΕΙΣ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new StudentAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΚΕΙΜΕΝΟ = d.ΚΕΙΜΕΝΟ,
                            ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true
                        }).FirstOrDefault();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}