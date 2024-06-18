using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class PraktikiApofasiService : IPraktikiApofasiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiApofasiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PraktikiApofasiViewModel> Read(int aitisiId)
        {
            var data = (from d in entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ
                        where d.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ == aitisiId
                        select new PraktikiApofasiViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ = d.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ
                        }).ToList();
            return data;
        }

        public void Create(PraktikiApofasiViewModel data, AitiseisPraktikisViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ entity = new ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = source.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ,
                ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ = source.ΑΙΤΗΣΗ_ΚΩΔ,
                ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ,
                ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ,
                ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ,
                ΙΕΚ = source.ΙΕΚ,
                ΑΜΚ = source.ΑΜΚ,
                ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
            };
            entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = entity.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ;
        }

        public void Update(PraktikiApofasiViewModel data, AitiseisPraktikisViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ);

            entity.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ = source.ΑΙΤΗΣΗ_ΚΩΔ;
            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = source.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ;
            entity.ΙΕΚ = source.ΙΕΚ;
            entity.ΑΜΚ = source.ΑΜΚ;
            entity.ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ;
            entity.ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ;
            entity.ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ;
            entity.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PraktikiApofasiViewModel data)
        {
            ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public PraktikiApofasiViewModel Refresh(int entityId)
        {
            return entities.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ.Select(d => new PraktikiApofasiViewModel
            {
                ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ = d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ,
                ΑΜΚ = d.ΑΜΚ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                ΙΕΚ = d.ΙΕΚ,
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ = d.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ
            }).Where(d => d.ΑΠΟΦΑΣΗ_ΚΩΔΙΚΟΣ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}