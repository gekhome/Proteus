using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TmimaService : ITmimaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TmimaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TmimaViewModel> Read()
        {
            var data = (from d in entities.ΤΜΗΜΑ
                        orderby d.ΙΕΚ, d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ, d.ΠΕΡΙΟΔΟΣ_ΚΩΔ, d.ΕΞΑΜΗΝΟ, d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        select new TmimaViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ,
                            ΠΑ_ΚΩΔ = d.ΠΑ_ΚΩΔ,
                            ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ
                        }).ToList();
            return data;
        }

        public IEnumerable<TmimaViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΤΜΗΜΑ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ, d.ΠΕΡΙΟΔΟΣ_ΚΩΔ, d.ΕΞΑΜΗΝΟ, d.ΤΜΗΜΑ_ΟΝΟΜΑ
                        select new TmimaViewModel
                        {
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ,
                            ΠΑ_ΚΩΔ = d.ΠΑ_ΚΩΔ,
                            ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ
                        }).ToList();
            return data;
        }

        public void Create(TmimaViewModel data)
        {
            ΤΜΗΜΑ entity = new ΤΜΗΜΑ()
            {
                ΙΕΚ = data.ΙΕΚ,
                ΤΜΗΜΑ_ΟΝΟΜΑ = data.ΤΜΗΜΑ_ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ,
                ΠΑ_ΚΩΔ = data.ΠΑ_ΚΩΔ
            };
            entities.ΤΜΗΜΑ.Add(entity);
            entities.SaveChanges();

            data.ΤΜΗΜΑ_ΚΩΔ = entity.ΤΜΗΜΑ_ΚΩΔ;
        }

        public void Create(TmimaViewModel data, int schoolId)
        {
            ΤΜΗΜΑ entity = new ΤΜΗΜΑ()
            {
                ΙΕΚ = schoolId,
                ΤΜΗΜΑ_ΟΝΟΜΑ = data.ΤΜΗΜΑ_ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ,
                ΠΑ_ΚΩΔ = data.ΠΑ_ΚΩΔ
            };
            entities.ΤΜΗΜΑ.Add(entity);
            entities.SaveChanges();

            data.ΤΜΗΜΑ_ΚΩΔ = entity.ΤΜΗΜΑ_ΚΩΔ;
        }

        public void Update(TmimaViewModel data)
        {
            ΤΜΗΜΑ entity = entities.ΤΜΗΜΑ.Find(data.ΤΜΗΜΑ_ΚΩΔ);

            entity.ΙΕΚ = data.ΙΕΚ;
            entity.ΤΜΗΜΑ_ΟΝΟΜΑ = data.ΤΜΗΜΑ_ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ;
            entity.ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ;
            entity.ΠΑ_ΚΩΔ = data.ΠΑ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(TmimaViewModel data, int schoolId)
        {
            ΤΜΗΜΑ entity = entities.ΤΜΗΜΑ.Find(data.ΤΜΗΜΑ_ΚΩΔ);

            entity.ΙΕΚ = schoolId;
            entity.ΤΜΗΜΑ_ΟΝΟΜΑ = data.ΤΜΗΜΑ_ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ;
            entity.ΕΞΑΜΗΝΟ = data.ΕΞΑΜΗΝΟ;
            entity.ΠΑ_ΚΩΔ = data.ΠΑ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TmimaViewModel data)
        {
            ΤΜΗΜΑ entity = entities.ΤΜΗΜΑ.Find(data.ΤΜΗΜΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΤΜΗΜΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TmimaViewModel Refresh(int entityId)
        {
            return entities.ΤΜΗΜΑ.Select(d => new TmimaViewModel
            {
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ,
                ΠΑ_ΚΩΔ = d.ΠΑ_ΚΩΔ,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ
            }).Where(d => d.ΤΜΗΜΑ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}