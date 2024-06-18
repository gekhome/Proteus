using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ApolytiriaService : IApolytiriaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ApolytiriaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApolytiriaViewModel> Read()
        {
            var data = (from d in entities.ΑΠΟΛΥΤΗΡΙΑ
                        orderby d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ
                        select new ApolytiriaViewModel
                        {
                            ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ,
                            ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                            ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ
                        }).ToList();
            return data;
        }

        public void Create(ApolytiriaViewModel data)
        {
            ΑΠΟΛΥΤΗΡΙΑ entity = new ΑΠΟΛΥΤΗΡΙΑ()
            {
                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = data.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ
            };
            entities.ΑΠΟΛΥΤΗΡΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ = entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ;
        }

        public void Update(ApolytiriaViewModel data)
        {
            ΑΠΟΛΥΤΗΡΙΑ entity = entities.ΑΠΟΛΥΤΗΡΙΑ.Find(data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ);

            entity.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = data.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ;
            entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ApolytiriaViewModel data)
        {
            ΑΠΟΛΥΤΗΡΙΑ entity = entities.ΑΠΟΛΥΤΗΡΙΑ.Find(data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΠΟΛΥΤΗΡΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ApolytiriaViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΛΥΤΗΡΙΑ.Select(d => new ApolytiriaViewModel
            {
                ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ,
                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = d.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ,
                ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΛΑΣΗ
            }).Where(d => d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}