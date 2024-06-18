using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ErgodotisInfoService : IErgodotisInfoService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ErgodotisInfoService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ErgodotesViewModel> Read()
        {
            var data = (from s in entities.ΕΡΓΟΔΟΤΕΣ
                        orderby s.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ
                        select new ErgodotesViewModel
                        {
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = s.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = s.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = s.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΙΕΚ = s.ΙΕΚ ?? 0,
                            ΥΠΕΥΘΥΝΟΣ = s.ΥΠΕΥΘΥΝΟΣ,
                            ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = s.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                            ΔΙΕΥΘΥΝΣΗ = s.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = s.ΤΗΛΕΦΩΝΑ,
                            EMAIL = s.EMAIL,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = s.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }).ToList();
            return data;
        }

        public ErgodotesViewModel GetRecord(int ergodotisId)
        {
            var data = (from s in entities.ΕΡΓΟΔΟΤΕΣ
                        where s.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ == ergodotisId
                        select new ErgodotesViewModel
                        {
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔ = s.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = s.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΑΦΜ = s.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ,
                            ΙΕΚ = s.ΙΕΚ ?? 0,
                            ΥΠΕΥΘΥΝΟΣ = s.ΥΠΕΥΘΥΝΟΣ,
                            ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ = s.ΥΠΕΥΘΥΝΟΣ_ΦΥΛΟ,
                            ΔΙΕΥΘΥΝΣΗ = s.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = s.ΤΗΛΕΦΩΝΑ,
                            EMAIL = s.EMAIL,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = s.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }).FirstOrDefault();
            return data;
        }

        public IEnumerable<PraktikiInfoViewModel> GetPraktikes(int ergodotisId)
        {
            var data = (from d in entities.sqlPRAKTIKI_INFO
                        where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId
                        select new PraktikiInfoViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΜΑΘΗΤΗΣ_ΑΜΚ = d.ΜΑΘΗΤΗΣ_ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΤΜΗΜΑ_ΟΝΟΜΑ = d.ΤΜΗΜΑ_ΟΝΟΜΑ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}