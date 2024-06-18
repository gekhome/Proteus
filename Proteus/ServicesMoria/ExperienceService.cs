using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class ExperienceService : IExperienceService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ExperienceService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<XmExperienceViewModel> Read(int aitisiId)
        {
            var data = (from d in entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        orderby d.ΕΝΑΡΞΗ descending
                        select new XmExperienceViewModel
                        {
                            ΕΜΠΕΙΡΙΑ_ΚΩΔ = d.ΕΜΠΕΙΡΙΑ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΝΑΡΞΗ = d.ΕΝΑΡΞΗ,
                            ΛΗΞΗ = d.ΛΗΞΗ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                            ΔΙΑΡΚΕΙΑ = d.ΔΙΑΡΚΕΙΑ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                        }).ToList();
            return data;
        }

        public void Create(XmExperienceViewModel data, int aitisiId, int egykliosId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ entity = new ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ()
            {
                ΑΙΤΗΣΗ_ΚΩΔ = aitisiId,
                ΕΝΑΡΞΗ = data.ΕΝΑΡΞΗ,
                ΛΗΞΗ = data.ΛΗΞΗ,
                ΠΕΡΙΓΡΑΦΗ = data.ΠΕΡΙΓΡΑΦΗ,
                ΔΙΑΡΚΕΙΑ = Common.CalculateMonths(data.ΕΝΑΡΞΗ, data.ΛΗΞΗ),
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId,
            };
            entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΕΜΠΕΙΡΙΑ_ΚΩΔ = entity.ΕΜΠΕΙΡΙΑ_ΚΩΔ;
        }

        public void Update(XmExperienceViewModel data, int aitisiId, int egykliosId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ.Find(data.ΕΜΠΕΙΡΙΑ_ΚΩΔ);

            entity.ΕΝΑΡΞΗ = data.ΕΝΑΡΞΗ;
            entity.ΛΗΞΗ = data.ΛΗΞΗ;
            entity.ΠΕΡΙΓΡΑΦΗ = data.ΠΕΡΙΓΡΑΦΗ;
            entity.ΔΙΑΡΚΕΙΑ = Common.CalculateMonths(data.ΕΝΑΡΞΗ, data.ΛΗΞΗ);
            entity.ΑΙΤΗΣΗ_ΚΩΔ = aitisiId;
            entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(XmExperienceViewModel data)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ.Find(data.ΕΜΠΕΙΡΙΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public XmExperienceViewModel Refresh(int entityId)
        {
            return entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ_ΕΜΠΕΙΡΙΑ.Select(d => new XmExperienceViewModel
            {
                ΕΜΠΕΙΡΙΑ_ΚΩΔ = d.ΕΜΠΕΙΡΙΑ_ΚΩΔ,
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΕΝΑΡΞΗ = d.ΕΝΑΡΞΗ,
                ΛΗΞΗ = d.ΛΗΞΗ,
                ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ,
                ΔΙΑΡΚΕΙΑ = d.ΔΙΑΡΚΕΙΑ,
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
            }).Where(d => d.ΕΜΠΕΙΡΙΑ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}