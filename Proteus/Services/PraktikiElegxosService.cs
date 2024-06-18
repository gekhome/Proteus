using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiElegxosService : IPraktikiElegxosService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiElegxosService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PraktikiElegxosViewModel> Read(int ergodotisId, int studentId)
        {
            var data = (from d in entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ
                        where d.ΕΡΓΟΔΟΤΗΣ == ergodotisId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new PraktikiElegxosViewModel
                        {
                            ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΕΛΕΓΚΤΗΣ = d.ΕΛΕΓΚΤΗΣ,
                            ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = d.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ,
                            ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = d.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ
                        }).ToList();
            return data;
        }

        public void Create(PraktikiElegxosViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ entity = new ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID,
                ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                ΙΕΚ = source.ΙΕΚ,
                ΑΜΚ = source.ΑΜΚ,
                ΕΛΕΓΚΤΗΣ = data.ΕΛΕΓΚΤΗΣ,
                ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = data.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ,
                ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = data.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ
            };
            entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ.Add(entity);
            entities.SaveChanges();

            data.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = entity.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ;
        }

        public void Update(PraktikiElegxosViewModel data, StudentInPraktikiViewModel source)
        {
            ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ entity = entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ.Find(data.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = source.STUDENT_ID;
            entity.ΕΡΓΟΔΟΤΗΣ = source.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ;
            entity.ΙΕΚ = source.ΙΕΚ;
            entity.ΑΜΚ = source.ΑΜΚ;
            entity.ΕΛΕΓΚΤΗΣ = data.ΕΛΕΓΚΤΗΣ;
            entity.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = data.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = data.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PraktikiElegxosViewModel data)
        {
            ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ entity = entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ.Find(data.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public PraktikiElegxosViewModel Refresh(int entityId)
        {
            return entities.ΠΡΑΚΤΙΚΗ_ΕΛΕΓΧΟΣ.Select(d => new PraktikiElegxosViewModel
            {
                ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ = d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΕΛΕΓΚΤΗΣ = d.ΕΛΕΓΚΤΗΣ,
                ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ = d.ΕΛΕΓΧΟΣ_ΕΞΑΜΗΝΟ,
                ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ = d.ΕΛΕΓΧΟΣ_ΗΜΕΡΟΜΗΝΙΑ
            }).Where(d => d.ΕΛΕΓΧΟΣ_ΚΩΔΙΚΟΣ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}