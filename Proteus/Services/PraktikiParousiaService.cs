using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiParousiaService : IPraktikiParousiaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiParousiaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PraktikiParousiaViewModel> Read(int ergodotisId, int studentId, int tmimaId)
        {
            var data = (from d in entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ
                        where d.ΕΡΓΟΔΟΤΗΣ == ergodotisId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new PraktikiParousiaViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΠΑΡΑΛΑΒΗ = d.ΠΑΡΑΛΑΒΗ ?? true,
                            ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΟ = d.ΠΡΑΚΤΙΚΗ_ΑΠΟ,
                            ΠΡΑΚΤΙΚΗ_ΕΩΣ = d.ΠΡΑΚΤΙΚΗ_ΕΩΣ,
                            ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = d.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ
                        }).ToList();
            return data;
        }

        public void Create(PraktikiParousiaViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ entity = new ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID,
                ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                ΙΕΚ = source.ΙΕΚ,
                ΑΜΚ = source.ΑΜΚ,
                ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ,
                ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ,
                ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ,
                ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ,
                ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = data.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
                ΠΑΡΑΛΑΒΗ = data.ΠΑΡΑΛΑΒΗ
            };
            entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΒΕΒΑΙΩΣΗ_ΚΩΔ = entity.ΒΕΒΑΙΩΣΗ_ΚΩΔ;
        }

        public void Update(PraktikiParousiaViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ entity = entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID;
            entity.ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ;
            entity.ΙΕΚ = source.ΙΕΚ;
            entity.ΑΜΚ = source.ΑΜΚ;
            entity.ΤΜΗΜΑ_ΚΩΔ = source.ΤΜΗΜΑ_ΚΩΔ;
            entity.ΠΡΑΚΤΙΚΗ_ΑΠΟ = source.ΗΜΝΙΑ_ΑΠΟ;
            entity.ΠΡΑΚΤΙΚΗ_ΕΩΣ = source.ΗΜΝΙΑ_ΕΩΣ;
            entity.ΠΡΑΚΤΙΚΗ_ΩΡΕΣ = source.ΩΡΕΣ;
            entity.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = data.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ;
            entity.ΠΑΡΑΛΑΒΗ = data.ΠΑΡΑΛΑΒΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PraktikiParousiaViewModel data)
        {
            ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ entity = entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public PraktikiParousiaViewModel Refresh(int entityId)
        {
            return entities.ΠΡΑΚΤΙΚΗ_ΠΑΡΟΥΣΙΑ.Select(d => new PraktikiParousiaViewModel
            {
                ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                ΠΑΡΑΛΑΒΗ = d.ΠΑΡΑΛΑΒΗ ?? true,
                ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ = d.ΒΕΒΑΙΩΣΗ_ΗΜΝΙΑ,
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