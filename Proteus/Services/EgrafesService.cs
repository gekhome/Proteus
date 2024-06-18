using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class EgrafesService : IEgrafesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EgrafesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentEgrafesViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        orderby d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ
                        select new StudentEgrafesViewModel
                        {
                            ΜΕ_ΚΩΔ = d.ΜΕ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΗΜΝΙΑ_ΕΓΓΡΑΦΗ = d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ,
                            ΗΜΝΙΑ_ΠΕΡΑΣ = d.ΗΜΝΙΑ_ΠΕΡΑΣ,
                            ΕΓΓΡΑΦΗ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ,
                            ΦΟΙΤΗΣΗ = d.ΦΟΙΤΗΣΗ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ
                        }).ToList();
            return data;
        }

        public void Create(StudentEgrafesViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ entity = new ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ()
            {
                ΑΜΚ = Common.GetStudentAmk(studentId),
                ΙΕΚ = schoolId,
                ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ,
                ΗΜΝΙΑ_ΕΓΓΡΑΦΗ = data.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ,
                ΗΜΝΙΑ_ΠΕΡΑΣ = data.ΗΜΝΙΑ_ΠΕΡΑΣ,
                ΕΓΓΡΑΦΗ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ,
                ΦΟΙΤΗΣΗ = data.ΦΟΙΤΗΣΗ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId
            };
            entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΜΕ_ΚΩΔ = entity.ΜΕ_ΚΩΔ;
        }

        public void Update(StudentEgrafesViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Find(data.ΜΕ_ΚΩΔ);

            entity.ΑΜΚ = Common.GetStudentAmk(studentId);
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ;
            entity.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ = data.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ;
            entity.ΗΜΝΙΑ_ΠΕΡΑΣ = data.ΗΜΝΙΑ_ΠΕΡΑΣ;
            entity.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ;
            entity.ΦΟΙΤΗΣΗ = data.ΦΟΙΤΗΣΗ;
            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentEgrafesViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Find(data.ΜΕ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentEgrafesViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Select(d => new StudentEgrafesViewModel
            {
                ΜΕ_ΚΩΔ = d.ΜΕ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ,
                ΙΕΚ = d.ΙΕΚ,
                ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                ΗΜΝΙΑ_ΕΓΓΡΑΦΗ = d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ,
                ΗΜΝΙΑ_ΠΕΡΑΣ = d.ΗΜΝΙΑ_ΠΕΡΑΣ,
                ΕΓΓΡΑΦΗ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ,
                ΦΟΙΤΗΣΗ = d.ΦΟΙΤΗΣΗ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ
            }).Where(d => d.ΜΕ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}