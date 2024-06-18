using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class SpoudesService : ISpoudesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public SpoudesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SpoudesViewModel> Read()
        {
            var data = (from d in entities.ΣΠΟΥΔΕΣ
                        orderby d.ΒΑΘΜΙΔΑ
                        select new SpoudesViewModel
                        {
                            ΒΑΘΜΙΔΑ_ΚΩΔ = d.ΒΑΘΜΙΔΑ_ΚΩΔ,
                            ΒΑΘΜΙΔΑ = d.ΒΑΘΜΙΔΑ
                        }).ToList();
            return data;
        }

        public void Create(SpoudesViewModel data)
        {
            ΣΠΟΥΔΕΣ entity = new ΣΠΟΥΔΕΣ()
            {
                ΒΑΘΜΙΔΑ = data.ΒΑΘΜΙΔΑ
            };
            entities.ΣΠΟΥΔΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΒΑΘΜΙΔΑ_ΚΩΔ = entity.ΒΑΘΜΙΔΑ_ΚΩΔ;
        }

        public void Update(SpoudesViewModel data)
        {
            ΣΠΟΥΔΕΣ entity = entities.ΣΠΟΥΔΕΣ.Find(data.ΒΑΘΜΙΔΑ_ΚΩΔ);

            entity.ΒΑΘΜΙΔΑ = data.ΒΑΘΜΙΔΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SpoudesViewModel data)
        {
            ΣΠΟΥΔΕΣ entity = entities.ΣΠΟΥΔΕΣ.Find(data.ΒΑΘΜΙΔΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΠΟΥΔΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SpoudesViewModel Refresh(int entityId)
        {
            return entities.ΣΠΟΥΔΕΣ.Select(d => new SpoudesViewModel
            {
                ΒΑΘΜΙΔΑ_ΚΩΔ = d.ΒΑΘΜΙΔΑ_ΚΩΔ,
                ΒΑΘΜΙΔΑ = d.ΒΑΘΜΙΔΑ
            }).Where(d => d.ΒΑΘΜΙΔΑ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}