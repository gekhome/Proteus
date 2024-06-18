using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class ErgodotesService : IErgodotesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ErgodotesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ErgodotesViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΕΡΓΟΔΟΤΕΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ
                        select new ErgodotesViewModel
                        {
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ,
                            ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = d.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }).ToList();
            return data;
        }

        public void Create(ErgodotesViewModel data, int schoolId)
        {
            ΕΡΓΟΔΟΤΕΣ entity = new ΕΡΓΟΔΟΤΕΣ()
            {
                ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = data.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ.Trim(),
                ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ.Trim(),
                ΙΕΚ = schoolId,
                ΥΠΕΥΘΥΝΟΣ = data.ΥΠΕΥΘΥΝΟΣ.Trim(),
                ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = data.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                ΔΙΕΥΘΥΝΣΗ = data.ΔΙΕΥΘΥΝΣΗ,
                ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ,
                EMAIL = data.EMAIL,
                ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ
            };
            entities.ΕΡΓΟΔΟΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = entity.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ;
        }

        public void Update(ErgodotesViewModel data, int schoolId)
        {
            ΕΡΓΟΔΟΤΕΣ entity = entities.ΕΡΓΟΔΟΤΕΣ.Find(data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ);

            entity.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = data.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ.Trim();
            entity.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ.Trim();
            entity.ΙΕΚ = schoolId;
            entity.ΥΠΕΥΘΥΝΟΣ = data.ΥΠΕΥΘΥΝΟΣ.Trim();
            entity.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = data.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ;
            entity.ΔΙΕΥΘΥΝΣΗ = data.ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = data.EMAIL;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ErgodotesViewModel data)
        {
            ΕΡΓΟΔΟΤΕΣ entity = entities.ΕΡΓΟΔΟΤΕΣ.Find(data.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ);

            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΕΡΓΟΔΟΤΕΣ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public ErgodotesViewModel Refresh(int entityId)
        {
            return entities.ΕΡΓΟΔΟΤΕΣ.Select(d => new ErgodotesViewModel
            {
                ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ ?? 0,
                ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ,
                ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = d.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                EMAIL = d.EMAIL,
                ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
            }).Where(d => d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public ErgodotesViewModel GetRecord(int ergodotisId)
        {
            var data = (from d in entities.ΕΡΓΟΔΟΤΕΣ
                        where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ == ergodotisId
                        orderby d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ
                        select new ErgodotesViewModel
                        {
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΥΠΕΥΘΥΝΟΣ = d.ΥΠΕΥΘΥΝΟΣ,
                            ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = d.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(ErgodotesViewModel data, int ergodotisId, int schoolId = 0)
        {
            ΕΡΓΟΔΟΤΕΣ entity = entities.ΕΡΓΟΔΟΤΕΣ.Find(ergodotisId);

            entity.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = data.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ.Trim();
            entity.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = data.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ.Trim();
            if (schoolId > 0)
            {
                entity.ΙΕΚ = schoolId;
            }
            else
            {
                entity.ΙΕΚ = data.ΙΕΚ;
            }
            entity.ΥΠΕΥΘΥΝΟΣ = data.ΥΠΕΥΘΥΝΟΣ.Trim();
            entity.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = data.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ;
            entity.ΔΙΕΥΘΥΝΣΗ = data.ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = data.EMAIL;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}