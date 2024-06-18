using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ApoxorisiAitiaService : IApoxorisiAitiaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ApoxorisiAitiaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApoxorisiAitiaViewModel> Read()
        {
            var data = (from d in entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ
                        orderby d.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ
                        select new ApoxorisiAitiaViewModel
                        {
                            ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ,
                            ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ = d.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ
                        }).ToList();
            return data;
        }

        public void Create(ApoxorisiAitiaViewModel data)
        {
            ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ entity = new ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ()
            {
                ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ = data.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ
            };
            entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ = entity.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ;
        }

        public void Update(ApoxorisiAitiaViewModel data)
        {
            ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ entity = entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Find(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ);

            entity.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ = data.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();

        }

        public void Destroy(ApoxorisiAitiaViewModel data)
        {
            ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ entity = entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Find(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ApoxorisiAitiaViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ.Select(d => new ApoxorisiAitiaViewModel
            {
                ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ,
                ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ = d.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΑ
            }).Where(d => d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}