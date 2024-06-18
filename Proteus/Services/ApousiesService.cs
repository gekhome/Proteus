using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ApousiesService : IApousiesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ApousiesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentApousiesViewModel> Read(int tmimaId, DateTime? theDate)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == theDate
                        orderby d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ
                        select new StudentApousiesViewModel
                        {
                            ΜΑ_ΚΩΔ = d.ΜΑ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΩΡΕΣ_ΑΠΟΥΣΙΑ = d.ΩΡΕΣ_ΑΠΟΥΣΙΑ,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ ?? DateTime.Today,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0
                        }).ToList();
            return data;
        }

        public void Create(StudentApousiesViewModel data, int tmimaId, DateTime? theDate, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ entity = new ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ,
                ΩΡΕΣ_ΑΠΟΥΣΙΑ = data.ΩΡΕΣ_ΑΠΟΥΣΙΑ,
                ΗΜΕΡΟΜΗΝΙΑ = theDate,
                ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                ΙΕΚ = schoolId,
                ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ),
            };
            entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΜΑ_ΚΩΔ = entity.ΜΑ_ΚΩΔ;
        }

        public void Update(StudentApousiesViewModel data, int tmimaId, DateTime? theDate, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ.Find(data.ΜΑ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ;
            entity.ΩΡΕΣ_ΑΠΟΥΣΙΑ = data.ΩΡΕΣ_ΑΠΟΥΣΙΑ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = theDate;
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΤΜΗΜΑ = tmimaId;
            entity.ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentApousiesViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ.Find(data.ΜΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentApousiesViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ.Select(d => new StudentApousiesViewModel
            {
                ΜΑ_ΚΩΔ = d.ΜΑ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΩΡΕΣ_ΑΠΟΥΣΙΑ = d.ΩΡΕΣ_ΑΠΟΥΣΙΑ,
                ΙΕΚ = d.ΙΕΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ ?? DateTime.Today,
                ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0
            }).Where(d => d.ΜΑ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}