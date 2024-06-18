using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class KladosUnifiedService : IKladosUnifiedService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public KladosUnifiedService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<KladosUnifiedViewModel> Read()
        {
            var data = (from d in entities.SYS_KLADOS_ENIAIOS
                        select new KladosUnifiedViewModel
                        {
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();
            return data;
        }

        public void Create(KladosUnifiedViewModel data)
        {
            SYS_KLADOS_ENIAIOS entity = new SYS_KLADOS_ENIAIOS()
            {
                ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ
            };
            entities.SYS_KLADOS_ENIAIOS.Add(entity);
            entities.SaveChanges();

            data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ;
        }

        public void Update(KladosUnifiedViewModel data)
        {
            SYS_KLADOS_ENIAIOS entity = entities.SYS_KLADOS_ENIAIOS.Find(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);

            entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(KladosUnifiedViewModel data)
        {
            SYS_KLADOS_ENIAIOS entity = entities.SYS_KLADOS_ENIAIOS.Find(data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.SYS_KLADOS_ENIAIOS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public KladosUnifiedViewModel Refresh(int entityId)
        {
            return entities.SYS_KLADOS_ENIAIOS.Select(d => new KladosUnifiedViewModel
            {
                ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ,
                ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
            }).Where(d => d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<sqlEidikotitesKUViewModel> GetEidikotites(int kladosunifiedId)
        {
            var data = (from d in entities.sqlEIDIKOTITES_KU
                        where d.KLADOS_UNIFIED == kladosunifiedId
                        orderby d.EIDIKOTITA_KLADOS_ID, d.EIDIKOTITA_PTYXIO
                        select new sqlEidikotitesKUViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_PTYXIO = d.EIDIKOTITA_PTYXIO,
                            KLADOS_UNIFIED = d.KLADOS_UNIFIED,
                            EIDIKOTITA_KLADOS_ID = d.EIDIKOTITA_KLADOS_ID
                        }).ToList();

            return (data);
        }

        public void SetEidikotita(sqlEidikotitesKUViewModel data, int kladosunifiedId)
        {
            SYS_EIDIKOTITES entity = entities.SYS_EIDIKOTITES.Find(data.EIDIKOTITA_ID);

            entity.KLADOS_UNIFIED = kladosunifiedId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void ResetEidikotita(sqlEidikotitesKUViewModel data, int kladosunifiedId)
        {
            SYS_EIDIKOTITES entity = entities.SYS_EIDIKOTITES.Find(data.EIDIKOTITA_ID);

            entity.KLADOS_UNIFIED = null;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public sqlEidikotitesKUViewModel RefreshEidikotita(int entityId)
        {
            return entities.sqlEIDIKOTITES_KU.Select(d => new sqlEidikotitesKUViewModel
            {
                EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                EIDIKOTITA_PTYXIO = d.EIDIKOTITA_PTYXIO,
                KLADOS_UNIFIED = d.KLADOS_UNIFIED,
                EIDIKOTITA_KLADOS_ID = d.EIDIKOTITA_KLADOS_ID
            }).Where(d => d.EIDIKOTITA_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}