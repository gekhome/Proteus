using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class PraktikiAitisiService : IPraktikiAitisiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiAitisiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PraktikiAitisiViewModel> Read(int ergodotisId, int studentId, int tmimaId)
        {
            var data = (from d in entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ
                        where d.ΕΡΓΟΔΟΤΗΣ == ergodotisId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΤΜΗΜΑ_ΚΩΔ == tmimaId
                        select new PraktikiAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true,
                        }).ToList();
            return data;
        }

        public void Create(PraktikiAitisiViewModel data, int ergodotisId, int studentId, int tmimaId, int schoolId)
        {
            ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ entity = new ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΕΡΓΟΔΟΤΗΣ = ergodotisId,
                ΤΜΗΜΑ_ΚΩΔ = tmimaId,
                ΙΕΚ = schoolId,
                ΑΜΚ = Common.GetStudentAmk(studentId),
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ,
            };
            entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(PraktikiAitisiViewModel data, int ergodotisId, int studentId, int tmimaId, int schoolId)
        {
            ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΕΡΓΟΔΟΤΗΣ = ergodotisId;
            entity.ΤΜΗΜΑ_ΚΩΔ = tmimaId;
            entity.ΙΕΚ = schoolId;
            entity.ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΥΠΟΒΛΗΘΗΚΕ = data.ΥΠΟΒΛΗΘΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PraktikiAitisiViewModel data)
        {
            ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ entity = entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public PraktikiAitisiViewModel Refresh(int entityId)
        {
            return entities.ΠΡΑΚΤΙΚΗ_ΑΙΤΗΣΗ.Select(d => new PraktikiAitisiViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ ?? 0,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΥΠΟΒΛΗΘΗΚΕ = d.ΥΠΟΒΛΗΘΗΚΕ ?? true,
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<AitiseisPraktikisViewModel> ReadInfo(int schoolId)
        {
            var data = (from d in entities.sqlAITISEIS_PRAKTIKIS_VIEW
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.ΙΕΚ == schoolId
                        select new AitiseisPraktikisViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ
                        }).ToList();

            return data;
        }

        public AitiseisPraktikisViewModel GetInfo(int aitisiId)
        {
            var data = (from d in entities.sqlAITISEIS_PRAKTIKIS_VIEW
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiId
                        select new AitiseisPraktikisViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ = d.ΕΡΓΟΔΟΤΗΣ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΤΜΗΜΑ_ΚΩΔ = d.ΤΜΗΜΑ_ΚΩΔ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΠΕΡΙΓΡΑΦΗ = d.ΠΕΡΙΓΡΑΦΗ
                        }).FirstOrDefault();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}