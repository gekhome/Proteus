using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class EgyklioiService : IEgyklioiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EgyklioiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<XmEgykliosViewModel> Read()
        {
            var data = (from d in entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΕΝΑΡΞΗ descending
                        select new XmEgykliosViewModel
                        {
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΑΠ = d.ΕΓΚΥΚΛΙΟΣ_ΑΠ,
                            ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                            ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ,
                            ΚΑΤΑΣΤΑΣΗ = d.ΚΑΤΑΣΤΑΣΗ ?? 0,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ ?? 0,
                            ΕΝΕΡΓΗ = d.ΕΝΕΡΓΗ ?? false,
                            ΔΙΑΧΕΙΡΙΣΗ = d.ΔΙΑΧΕΙΡΙΣΗ ?? false
                        }).ToList();
            return data;
        }

        public void Create(XmEgykliosViewModel data)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ entity = new ΧΜ_ΕΓΚΥΚΛΙΟΣ()
            {
                ΕΓΚΥΚΛΙΟΣ_ΑΠ = data.ΕΓΚΥΚΛΙΟΣ_ΑΠ,
                ΗΜΝΙΑ_ΕΝΑΡΞΗ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                ΗΜΝΙΑ_ΛΗΞΗ = data.ΗΜΝΙΑ_ΛΗΞΗ,
                ΚΑΤΑΣΤΑΣΗ = data.ΚΑΤΑΣΤΑΣΗ,
                ΕΝΕΡΓΗ = data.ΕΝΕΡΓΗ,
                ΔΙΑΧΕΙΡΙΣΗ = data.ΔΙΑΧΕΙΡΙΣΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ
            };
            entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Add(entity);
            entities.SaveChanges();

            data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ;
        }

        public void Update(XmEgykliosViewModel data)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ entity = entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Find(data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ);

            entity.ΕΓΚΥΚΛΙΟΣ_ΑΠ = data.ΕΓΚΥΚΛΙΟΣ_ΑΠ;
            entity.ΗΜΝΙΑ_ΕΝΑΡΞΗ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗ;
            entity.ΗΜΝΙΑ_ΛΗΞΗ = data.ΗΜΝΙΑ_ΛΗΞΗ;
            entity.ΚΑΤΑΣΤΑΣΗ = data.ΚΑΤΑΣΤΑΣΗ;
            entity.ΕΝΕΡΓΗ = data.ΕΝΕΡΓΗ;
            entity.ΔΙΑΧΕΙΡΙΣΗ = data.ΔΙΑΧΕΙΡΙΣΗ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(XmEgykliosViewModel data)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ entity = entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Find(data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public XmEgykliosViewModel Refresh(int entityId)
        {
            return entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ.Select(d => new XmEgykliosViewModel
            {
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                ΕΓΚΥΚΛΙΟΣ_ΑΠ = d.ΕΓΚΥΚΛΙΟΣ_ΑΠ,
                ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ,
                ΚΑΤΑΣΤΑΣΗ = d.ΚΑΤΑΣΤΑΣΗ ?? 0,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ ?? 0,
                ΕΝΕΡΓΗ = d.ΕΝΕΡΓΗ ?? false,
                ΔΙΑΧΕΙΡΙΣΗ = d.ΔΙΑΧΕΙΡΙΣΗ ?? false
            }).Where(d => d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}