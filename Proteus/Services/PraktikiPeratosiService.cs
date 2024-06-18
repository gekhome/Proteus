using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiPeratosiService : IPraktikiPeratosiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiPeratosiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PraktikiPeratosiViewModel> Read(int ergodotisId, int studentId, int tmimaId)
        {
            var data = (from d in entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                        where d.ΕΡΓΟΔΟΤΗΣ == ergodotisId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new PraktikiPeratosiViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ
                        }).ToList();
            return data;
        }

        public void Create(PraktikiPeratosiViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ entity = new ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID,
                ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                ΙΕΚ = source.ΙΕΚ,
                ΑΜΚ = source.ΑΜΚ,
                ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ,
                ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ,
                ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ,
                ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ
            };
            entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ.Add(entity);
            entities.SaveChanges();

            data.ΒΕΒΑΙΩΣΗ_ΚΩΔ = entity.ΒΕΒΑΙΩΣΗ_ΚΩΔ;
        }

        public void Update(PraktikiPeratosiViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID;
            entity.ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ;
            entity.ΙΕΚ = source.ΙΕΚ;
            entity.ΑΜΚ = source.ΑΜΚ;
            entity.ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ;
            entity.ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ;
            entity.ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ;
            entity.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PraktikiPeratosiViewModel data)
        {
            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public PraktikiPeratosiViewModel Refresh(int entityId)
        {
            return entities.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ.Select(d => new PraktikiPeratosiViewModel
            {
                ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ
            }).Where(d => d.ΒΕΒΑΙΩΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}