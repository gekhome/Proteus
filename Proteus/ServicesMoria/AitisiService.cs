using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class AitisiService : IAitisiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public AitisiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<XmAitisiGridViewModel> Read(int egykliosId)
        {
            var data = (from d in entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId
                        orderby d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                        select new XmAitisiGridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΙΕΚ1 = d.ΙΕΚ1,
                            ΙΕΚ2 = d.ΙΕΚ2,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ
                        }).ToList();
            return data;
        }

        public void Create(XmAitisiGridViewModel data, int egykliosId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = new ΧΜ_ΥΠΟΨΗΦΙΟΣ()
            {
                ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GenerateProtocol(),
                ΗΜΕΡΟΜΗΝΙΑ = DateTime.Now.Date,
                ΑΦΜ = data.ΑΦΜ,
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = data.ΟΝΟΜΑ,
                ΙΕΚ1 = data.ΙΕΚ1,
                ΙΕΚ2 = data.ΙΕΚ2,
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId
            };
            entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(XmAitisiGridViewModel data, int egykliosId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΙΕΚ1 = data.ΙΕΚ1;
            entity.ΙΕΚ2 = data.ΙΕΚ2;
            entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public IEnumerable<XmAitisiGridViewModel> Read(int egykliosId, int schoolId)
        {
            var data = (from d in entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΙΕΚ1 == schoolId
                        orderby d.ΕΠΩΝΥΜΟ, d.ΟΝΟΜΑ
                        select new XmAitisiGridViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΙΕΚ1 = d.ΙΕΚ1,
                            ΙΕΚ2 = d.ΙΕΚ2,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ
                        }).ToList();
            return data;
        }

        public void Create(XmAitisiGridViewModel data, int egykliosId, int schoolId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = new ΧΜ_ΥΠΟΨΗΦΙΟΣ()
            {
                ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = Common.GenerateProtocol(),
                ΗΜΕΡΟΜΗΝΙΑ = DateTime.Now.Date,
                ΑΦΜ = data.ΑΦΜ,
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = data.ΟΝΟΜΑ,
                ΙΕΚ1 = schoolId,
                ΙΕΚ2 = data.ΙΕΚ2,
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId
            };
            entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΙΤΗΣΗ_ΚΩΔ = entity.ΑΙΤΗΣΗ_ΚΩΔ;
        }

        public void Update(XmAitisiGridViewModel data, int egykliosId, int schoolId)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);

            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΙΕΚ1 = schoolId;
            entity.ΙΕΚ2 = data.ΙΕΚ2;
            entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(XmAitisiGridViewModel data)
        {
            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(data.ΑΙΤΗΣΗ_ΚΩΔ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public XmAitisiGridViewModel Refresh(int entityId)
        {
            return entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Select(d => new XmAitisiGridViewModel
            {
                ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΑΦΜ = d.ΑΦΜ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΙΕΚ1 = d.ΙΕΚ1,
                ΙΕΚ2 = d.ΙΕΚ2,
                ΜΟΡΙΑ = d.ΜΟΡΙΑ
            }).Where(d => d.ΑΙΤΗΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public XmAitisiViewModel GetRecord(int aitisiID)
        {
            var data = (from d in entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ
                        where d.ΑΙΤΗΣΗ_ΚΩΔ == aitisiID
                        select new XmAitisiViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            EMAIL = d.EMAIL,
                            ΑΔΤ = d.ΑΔΤ,
                            ΑΔΤ_ΑΡΧΗ = d.ΑΔΤ_ΑΡΧΗ,
                            ΑΔΤ_ΗΜΝΙΑ = d.ΑΔΤ_ΗΜΝΙΑ,
                            ΑΜ_ΑΡΡΕΝΩΝ = d.ΑΜ_ΑΡΡΕΝΩΝ,
                            ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = d.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ,
                            ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = d.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = d.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ,
                            ΤΗΛΕΦΩΝΟ = d.ΤΗΛΕΦΩΝΟ,
                            ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                            ΒΑΘΜΟΣ = d.ΒΑΘΜΟΣ,
                            ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = d.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ,
                            ΤΡΙΤΕΚΝΟΣ = d.ΤΡΙΤΕΚΝΟΣ ?? false,
                            ΠΟΛΥΤΕΚΝΟΣ = d.ΠΟΛΥΤΕΚΝΟΣ ?? false,
                            ΜΟΝΟΓΟΝΕΙΚΟΣ = d.ΜΟΝΟΓΟΝΕΙΚΟΣ ?? false,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΙΕΚ1 = d.ΙΕΚ1,
                            ΕΙΔΙΚΟΤΗΤΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ1,
                            ΕΙΔΙΚΟΤΗΤΑ2 = d.ΕΙΔΙΚΟΤΗΤΑ2,
                            ΕΙΔΙΚΟΤΗΤΑ3 = d.ΕΙΔΙΚΟΤΗΤΑ3,
                            TERM1 = d.TERM1,
                            TERM2 = d.TERM2,
                            TERM3 = d.TERM3,
                            ΙΕΚ2 = d.ΙΕΚ2,
                            ΕΙΔΙΚΟΤΗΤΑ4 = d.ΕΙΔΙΚΟΤΗΤΑ4,
                            ΕΙΔΙΚΟΤΗΤΑ5 = d.ΕΙΔΙΚΟΤΗΤΑ5,
                            TERM4 = d.TERM4,
                            TERM5 = d.TERM5,
                            ΔΙΑΒΑΤΗΡΙΟ = d.ΔΙΑΒΑΤΗΡΙΟ,
                            ΕΘΝΙΚΟΤΗΤΑ = d.ΕΘΝΙΚΟΤΗΤΑ,
                            ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ = d.ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ,
                            ΜΟΡΙΑ = d.ΜΟΡΙΑ,
                            ΜΟΡΙΑ_ΒΑΘΜΟΣ = d.ΜΟΡΙΑ_ΒΑΘΜΟΣ,
                            ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ = d.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ,
                            ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ = d.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ,
                            ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ = d.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ,
                            ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ = d.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ,
                            ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ = d.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }).FirstOrDefault();
            return (data);
        }

        public void UpdateRecord(XmAitisiViewModel data, int aitisiID, int egykliosID)
        {
            XmAitisiViewModel aitisi = GetRecord(aitisiID);

            ΧΜ_ΥΠΟΨΗΦΙΟΣ entity = entities.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(aitisiID);

            entity.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ = aitisi.ΑΙΤΗΣΗ_ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = aitisi.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΑΦΜ = aitisi.ΑΦΜ;
            entity.ΑΜΚΑ = data.ΑΜΚΑ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ;
            entity.ΑΔΤ = data.ΑΔΤ;
            entity.ΑΔΤ_ΑΡΧΗ = data.ΑΔΤ_ΑΡΧΗ;
            entity.ΑΔΤ_ΗΜΝΙΑ = data.ΑΔΤ_ΗΜΝΙΑ;
            entity.ΑΜ_ΑΡΡΕΝΩΝ = data.ΑΜ_ΑΡΡΕΝΩΝ;
            entity.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ = data.ΑΡ_ΔΗΜΟΤΟΛΟΓΙΟ;
            entity.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ = data.ΔΗΜΟΣ_ΕΓΓΡΑΦΗΣ;
            entity.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ = data.ΚΑΤΟΙΚΙΑ_ΔΝΣΗ;
            entity.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ = data.ΚΑΤΟΙΚΙΑ_ΠΟΛΗ;
            entity.ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ;
            entity.EMAIL = data.EMAIL;
            entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ = data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ;
            entity.ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ;
            entity.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ = data.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔ;
            entity.ΤΡΙΤΕΚΝΟΣ = data.ΤΡΙΤΕΚΝΟΣ;
            entity.ΠΟΛΥΤΕΚΝΟΣ = data.ΠΟΛΥΤΕΚΝΟΣ;
            entity.ΜΟΝΟΓΟΝΕΙΚΟΣ = data.ΜΟΝΟΓΟΝΕΙΚΟΣ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;
            entity.ΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ = Kerberos.GetMonthsWork(entity);
            entity.ΙΕΚ1 = data.ΙΕΚ1;
            entity.ΕΙΔΙΚΟΤΗΤΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ1;
            entity.ΕΙΔΙΚΟΤΗΤΑ2 = data.ΕΙΔΙΚΟΤΗΤΑ2;
            entity.ΕΙΔΙΚΟΤΗΤΑ3 = data.ΕΙΔΙΚΟΤΗΤΑ3;
            entity.TERM1 = data.TERM1;
            entity.TERM2 = data.TERM2;
            entity.TERM3 = data.TERM3;
            entity.ΙΕΚ2 = data.ΙΕΚ2;
            entity.ΕΙΔΙΚΟΤΗΤΑ4 = data.ΕΙΔΙΚΟΤΗΤΑ4;
            entity.ΕΙΔΙΚΟΤΗΤΑ5 = data.ΕΙΔΙΚΟΤΗΤΑ5;
            entity.TERM4 = data.TERM4;
            entity.TERM5 = data.TERM5;
            entity.ΑΠΟΤΕΛΕΣΜΑ = data.ΑΠΟΤΕΛΕΣΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΔΙΑΒΑΤΗΡΙΟ = data.ΔΙΑΒΑΤΗΡΙΟ;
            entity.ΕΘΝΙΚΟΤΗΤΑ = data.ΕΘΝΙΚΟΤΗΤΑ;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            entity.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = egykliosID;
            entity.ΑΠΟΤΕΛΕΣΜΑ = data.ΑΠΟΤΕΛΕΣΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            // Moria calculators here
            entity.ΜΟΡΙΑ_ΒΑΘΜΟΣ = Kerberos.MoriaGrade(entity);
            entity.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ = Kerberos.MoriaWork(entity);
            entity.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ = Kerberos.MoriaApofitisi(entity);
            entity.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ = Kerberos.MoriaTriteknos(entity);
            entity.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ = Kerberos.MoriaPolyteknos(entity);
            entity.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ = Kerberos.MoriaMonogoneikos(entity);
            entity.ΜΟΡΙΑ = TotalMoria(entity);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        private decimal? TotalMoria(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal? totalMoria = entity.ΜΟΡΙΑ_ΒΑΘΜΟΣ
                + entity.ΜΟΡΙΑ_ΕΜΠΕΙΡΙΑ
                + entity.ΜΟΡΙΑ_ΑΠΟΦΟΙΤΗΣΗ
                + entity.ΜΟΡΙΑ_ΤΡΙΤΕΚΝΟΣ
                + entity.ΜΟΡΙΑ_ΠΟΛΥΤΕΚΝΟΣ
                + entity.ΜΟΡΙΑ_ΜΟΝΟΓΟΝΕΙΚΟΣ;
            return totalMoria;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}