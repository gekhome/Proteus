using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class EidikotitesKatartisiService : IEidikotitesKatartisiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EidikotitesKatartisiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SYS_EIDIKOTITES_IEKViewModel> Read()
        {
            var data = (from d in entities.SYS_EIDIKOTITES_IEK
                        orderby d.EIDIKOTITA_TEXT
                        select new SYS_EIDIKOTITES_IEKViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            EIDIKOTITA_CODE = d.EIDIKOTITA_CODE,
                            ISCED_0000 = d.ISCED_0000,
                            APPROVED = d.APPROVED ?? false
                        }).ToList();
            return data;
        }

        public void Create(SYS_EIDIKOTITES_IEKViewModel data)
        {
            SYS_EIDIKOTITES_IEK entity = new SYS_EIDIKOTITES_IEK()
            {
                EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT,
                EIDIKOTITA_CODE = data.EIDIKOTITA_CODE,
                ISCED_0000 = data.ISCED_0000,
                APPROVED = data.APPROVED
            };
            entities.SYS_EIDIKOTITES_IEK.Add(entity);
            entities.SaveChanges();

            data.EIDIKOTITA_ID = entity.EIDIKOTITA_ID;
        }

        public void Update(SYS_EIDIKOTITES_IEKViewModel data)
        {
            SYS_EIDIKOTITES_IEK entity = entities.SYS_EIDIKOTITES_IEK.Find(data.EIDIKOTITA_ID);

            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;
            entity.EIDIKOTITA_TEXT = data.EIDIKOTITA_TEXT;
            entity.EIDIKOTITA_CODE = data.EIDIKOTITA_CODE;
            entity.ISCED_0000 = data.ISCED_0000;
            entity.APPROVED = data.APPROVED;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SYS_EIDIKOTITES_IEKViewModel data)
        {
            SYS_EIDIKOTITES_IEK entity = entities.SYS_EIDIKOTITES_IEK.Find(data.EIDIKOTITA_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_EIDIKOTITES_IEK.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SYS_EIDIKOTITES_IEKViewModel Refresh(int entityId)
        {
            return entities.SYS_EIDIKOTITES_IEK.Select(d => new SYS_EIDIKOTITES_IEKViewModel
            {
                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                EIDIKOTITA_CODE = d.EIDIKOTITA_CODE,
                APPROVED = d.APPROVED ?? false,
                ISCED_0000 = d.ISCED_0000
            }).Where(d => d.EIDIKOTITA_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}