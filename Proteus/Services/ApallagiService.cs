using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ApallagiService : IApallagiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ApallagiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentApallagiViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        orderby d.ΕΞΑΜΗΝΟ_ΚΩΔ, d.ΜΑΘΗΜΑ_ΟΝΟΜΑ
                        select new StudentApallagiViewModel
                        {
                            ΜΑΠ_ΚΩΔ = d.ΜΑΠ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΕΞΑΜΗΝΟ_ΚΩΔ = d.ΕΞΑΜΗΝΟ_ΚΩΔ,
                            ΜΑΘΗΜΑ_ΟΝΟΜΑ = d.ΜΑΘΗΜΑ_ΟΝΟΜΑ
                        }).ToList();
            return data;
        }

        public void Create(StudentApallagiViewModel data, int studentId, int eidikotitaId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ entity = new ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΙΕΚ = schoolId,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = eidikotitaId,
                ΕΞΑΜΗΝΟ_ΚΩΔ = data.ΕΞΑΜΗΝΟ_ΚΩΔ,
                ΜΑΘΗΜΑ_ΟΝΟΜΑ = data.ΜΑΘΗΜΑ_ΟΝΟΜΑ
            };
            entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΜΑΠ_ΚΩΔ = entity.ΜΑΠ_ΚΩΔ;
        }

        public void Update(StudentApallagiViewModel data, int studentId, int eidikotitaId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Find(data.ΜΑΠ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΙΕΚ = schoolId;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = eidikotitaId;
            entity.ΕΞΑΜΗΝΟ_ΚΩΔ = data.ΕΞΑΜΗΝΟ_ΚΩΔ;
            entity.ΜΑΘΗΜΑ_ΟΝΟΜΑ = data.ΜΑΘΗΜΑ_ΟΝΟΜΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentApallagiViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ entity = entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Find(data.ΜΑΠ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentApallagiViewModel Refersh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΑΠΑΛΛΑΓΕΣ.Select(d => new StudentApallagiViewModel
            {
                ΜΑΠ_ΚΩΔ = d.ΜΑΠ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΕΞΑΜΗΝΟ_ΚΩΔ = d.ΕΞΑΜΗΝΟ_ΚΩΔ,
                ΜΑΘΗΜΑ_ΟΝΟΜΑ = d.ΜΑΘΗΜΑ_ΟΝΟΜΑ
            }).Where(d => d.ΜΑΠ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}