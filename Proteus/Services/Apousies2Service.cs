using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class Apousies2Service : IApousies2Service, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public Apousies2Service(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentApousies2ViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2
                        where d.ΙΕΚ == schoolId
                        orderby d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ
                        select new StudentApousies2ViewModel
                        {
                            ΜΑ2_ΚΩΔ = d.ΜΑ2_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0
                        }).ToList();
            return data;
        }

        public void Create(StudentApousies2ViewModel data, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2 entity = new ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ,
                ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ),
                ΙΕΚ = schoolId,
                ΑΠΟΥΣΙΕΣ = data.ΑΠΟΥΣΙΕΣ,
            };
            entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2.Add(entity);
            entities.SaveChanges();

            data.ΜΑ2_ΚΩΔ = entity.ΜΑ2_ΚΩΔ;
        }

        public void Update(StudentApousies2ViewModel data, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2 entity = entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2.Find(data.ΜΑ2_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΚΩΔ_ΤΜΗΜΑ = data.ΚΩΔ_ΤΜΗΜΑ;
            entity.ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            entity.ΙΕΚ = schoolId;
            entity.ΑΠΟΥΣΙΕΣ = data.ΑΠΟΥΣΙΕΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentApousies2ViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2 entity = entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2.Find(data.ΜΑ2_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentApousies2ViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΑΠΟΥΣΙΕΣ2.Select(d => new StudentApousies2ViewModel
            {
                ΜΑ2_ΚΩΔ = d.ΜΑ2_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ,
                ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                ΙΕΚ = d.ΙΕΚ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0
            }).Where(d => d.ΜΑ2_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}