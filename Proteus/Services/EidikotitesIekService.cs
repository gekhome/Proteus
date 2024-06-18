using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class EidikotitesIekService : IEidikotitesIekService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EidikotitesIekService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<IekEidikotitesViewModel> Read()
        {
            var data = (from d in entities.IEK_EIDIKOTITES
                        orderby d.IEK_ID, d.EIDIKOTITA_ID
                        select new IekEidikotitesViewModel
                        {
                            IE_ID = d.IE_ID,
                            IEK_ID = d.IEK_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID
                        }).ToList();
            return data;
        }

        public IEnumerable<IekEidikotitesViewModel> Read(int schoolId)
        {
            var data = (from d in entities.IEK_EIDIKOTITES
                        where d.IEK_ID == schoolId
                        orderby d.EIDIKOTITA_ID
                        select new IekEidikotitesViewModel
                        {
                            IE_ID = d.IE_ID,
                            IEK_ID = d.IEK_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID
                        }).ToList();
            return data;
        }

        public void Create(IekEidikotitesViewModel data)
        {
            IEK_EIDIKOTITES entity = new IEK_EIDIKOTITES()
            {
                IEK_ID = data.IEK_ID,
                EIDIKOTITA_ID = data.EIDIKOTITA_ID
            };
            entities.IEK_EIDIKOTITES.Add(entity);
            entities.SaveChanges();

            data.IE_ID = entity.IE_ID;
        }

        public void Create(IekEidikotitesViewModel data, int schoolId)
        {
            IEK_EIDIKOTITES entity = new IEK_EIDIKOTITES()
            {
                IEK_ID = schoolId,
                EIDIKOTITA_ID = data.EIDIKOTITA_ID
            };
            entities.IEK_EIDIKOTITES.Add(entity);
            entities.SaveChanges();

            data.IE_ID = entity.IE_ID;
        }

        public void Update(IekEidikotitesViewModel data)
        {
            IEK_EIDIKOTITES entity = entities.IEK_EIDIKOTITES.Find(data.IE_ID);

            entity.IEK_ID = data.IEK_ID;
            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(IekEidikotitesViewModel data, int schoolId)
        {
            IEK_EIDIKOTITES entity = entities.IEK_EIDIKOTITES.Find(data.IE_ID);

            entity.IEK_ID = schoolId;
            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(IekEidikotitesViewModel data)
        {
            IEK_EIDIKOTITES entity = entities.IEK_EIDIKOTITES.Find(data.IE_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.IEK_EIDIKOTITES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public IekEidikotitesViewModel Refresh(int entityId)
        {
            return entities.IEK_EIDIKOTITES.Select(d => new IekEidikotitesViewModel
            {
                IE_ID = d.IE_ID,
                IEK_ID = d.IEK_ID,
                EIDIKOTITA_ID = d.EIDIKOTITA_ID
            }).Where(d => d.IE_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}