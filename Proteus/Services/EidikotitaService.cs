using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class EidikotitaService : IEidikotitaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EidikotitaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EidikotitesViewModel> Read()
        {
            var data = (from d in entities.SYS_EIDIKOTITES
                        orderby d.EIDIKOTITA_KLADOS_ID, d.EIDIKOTITA_CODE, d.EIDIKOTITA_NAME
                        select new EidikotitesViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_CODE = d.EIDIKOTITA_CODE,
                            EIDIKOTITA_NAME = d.EIDIKOTITA_NAME,
                            EIDIKOTITA_UNIFIED = d.EIDIKOTITA_UNIFIED,
                            KLADOS_UNIFIED = d.KLADOS_UNIFIED,
                            EIDIKOTITA_KLADOS_ID = d.EIDIKOTITA_KLADOS_ID,
                        }).ToList();
            return data;
        }

        public void Create(EidikotitesViewModel data)
        {
            SYS_EIDIKOTITES entity = new SYS_EIDIKOTITES()
            {
                EIDIKOTITA_CODE = data.EIDIKOTITA_CODE,
                EIDIKOTITA_NAME = data.EIDIKOTITA_NAME,
                EIDIKOTITA_UNIFIED = data.EIDIKOTITA_UNIFIED,
                KLADOS_UNIFIED = data.KLADOS_UNIFIED,
                EIDIKOTITA_KLADOS_ID = data.EIDIKOTITA_KLADOS_ID,
            };
            entities.SYS_EIDIKOTITES.Add(entity);
            entities.SaveChanges();

            data.EIDIKOTITA_ID = entity.EIDIKOTITA_ID;
        }

        public void Update(EidikotitesViewModel data)
        {
            SYS_EIDIKOTITES entity = entities.SYS_EIDIKOTITES.Find(data.EIDIKOTITA_ID);

            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;
            entity.EIDIKOTITA_CODE = data.EIDIKOTITA_CODE;
            entity.EIDIKOTITA_NAME = data.EIDIKOTITA_NAME;
            entity.EIDIKOTITA_UNIFIED = data.EIDIKOTITA_UNIFIED;
            entity.KLADOS_UNIFIED = data.KLADOS_UNIFIED;
            entity.EIDIKOTITA_KLADOS_ID = data.EIDIKOTITA_KLADOS_ID;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EidikotitesViewModel data)
        {
            SYS_EIDIKOTITES entity = entities.SYS_EIDIKOTITES.Find(data.EIDIKOTITA_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_EIDIKOTITES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public EidikotitesViewModel Refresh(int entityId)
        {
            return entities.SYS_EIDIKOTITES.Select(d => new EidikotitesViewModel
            {
                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                EIDIKOTITA_CODE = d.EIDIKOTITA_CODE,
                EIDIKOTITA_NAME = d.EIDIKOTITA_NAME,
                EIDIKOTITA_UNIFIED = d.EIDIKOTITA_UNIFIED,
                KLADOS_UNIFIED = d.KLADOS_UNIFIED,
                EIDIKOTITA_KLADOS_ID = d.EIDIKOTITA_KLADOS_ID,
            }).Where(d => d.EIDIKOTITA_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}