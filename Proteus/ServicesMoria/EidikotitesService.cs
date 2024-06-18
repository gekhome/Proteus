using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class EidikotitesService : IEidikotitesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EidikotitesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<XmEidikotitesViewModel> Read()
        {
            var data = (from d in entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ
                        orderby d.EIDIKOTITA_TEXT
                        select new XmEidikotitesViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            APPROVED = d.APPROVED ?? false
                        }).ToList();
            return data;
        }

        public void Create(XmEidikotitesViewModel data)
        {
            ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ entity = new ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ()
            {
                EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT,
                APPROVED = data.APPROVED
            };
            entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.EIDIKOTITA_ID = entity.EIDIKOTITA_ID;
        }

        public void Update(XmEidikotitesViewModel data)
        {
            ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.EIDIKOTITA_ID);

            entity.EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT;
            entity.APPROVED = data.APPROVED;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(XmEidikotitesViewModel data)
        {
            ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.EIDIKOTITA_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public XmEidikotitesViewModel Refresh(int entityId)
        {
            return entities.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.Select(d => new XmEidikotitesViewModel
            {
                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                APPROVED = d.APPROVED ?? false,
            }).Where(d => d.EIDIKOTITA_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}