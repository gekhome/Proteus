using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TeacherService : ITeacherService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TeacherService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TeacherGridViewModel> Read(int schoolId)
        {
            var data = (from s in entities.ΕΚΠΑΙΔΕΥΤΕΣ
                        where s.ΙΕΚ == schoolId
                        orderby s.ΕΠΩΝΥΜΟ, s.ΟΝΟΜΑ
                        select new TeacherGridViewModel
                        {
                            TEACHER_ID = s.TEACHER_ID,
                            ΑΦΜ = s.ΑΦΜ,
                            ΙΕΚ = s.ΙΕΚ,
                            ΕΠΩΝΥΜΟ = s.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = s.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = s.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                            ΚΛΑΔΟΣ = s.ΚΛΑΔΟΣ ?? 0,
                            ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = s.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ
                        }).ToList();
            return data;
        }

        public void Create(TeacherGridViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ()
            {
                ΑΦΜ = data.ΑΦΜ,
                ΙΕΚ = schoolId,
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim(),
                ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim(),
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ,
                ΚΛΑΔΟΣ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ),
                ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = data.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.TEACHER_ID = entity.TEACHER_ID;

            CopyTeacherData(entity.TEACHER_ID, entity.ΑΦΜ);
        }

        public void Update(TeacherGridViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ.Find(data.TEACHER_ID);

            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΙΕΚ = schoolId;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΚΛΑΔΟΣ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ);
            entity.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = data.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherGridViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ.Find(data.TEACHER_ID);

            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΕΚΠΑΙΔΕΥΤΕΣ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public TeacherGridViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ.Select(d => new TeacherGridViewModel
            {
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ ?? 0,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ ?? 0,
                ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = d.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ
            }).Where(d => d.TEACHER_ID.Equals(entityId)).FirstOrDefault();
        }

        public TeacherViewModel GetRecord(int teacherId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ
                        where d.TEACHER_ID == teacherId
                        select new TeacherViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΔΟΥ = d.ΔΟΥ,
                            ΑΜΑ = d.ΑΜΑ,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = d.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ,
                            ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = d.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            ΠΤΥΧΙΟ = d.ΠΤΥΧΙΟ,
                            ΥΠΑΛΛΗΛΟΣ = d.ΥΠΑΛΛΗΛΟΣ,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            MASTER = d.MASTER ?? false,
                            ΔΙΔΑΚΤΟΡΙΚΟ = d.ΔΙΔΑΚΤΟΡΙΚΟ ?? false,
                            ΠΑΙΔΑΓΩΓΙΚΟ = d.ΠΑΙΔΑΓΩΓΙΚΟ ?? false,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ,
                            BANK_ACCOUNT = d.BANK_ACCOUNT,
                            BANK_NAME = d.BANK_NAME,
                            BANK_IBAN = d.BANK_IBAN,
                            ΑΔΤ = d.ΑΔΤ,
                            ΤΕΚΝΑ = d.ΤΕΚΝΑ ?? 0,
                            ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = d.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(TeacherViewModel data, int teacherId, int schoolId = 0)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ.Find(teacherId);

            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            if (schoolId > 0)
            {
                entity.ΙΕΚ = schoolId;
            }
            else
            {
                entity.ΙΕΚ = data.ΙΕΚ;
            }
            entity.ΔΟΥ = data.ΔΟΥ;
            entity.ΑΜΚΑ = data.ΑΜΚΑ;
            entity.ΑΜΑ = data.ΑΜΑ;
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = entities.SYS_EIDIKOTITES.Find(data.ΕΙΔΙΚΟΤΗΤΑ).EIDIKOTITA_CODE;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ = data.ΜΗΤΡΩΝΥΜΟ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = data.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = data.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ.Trim();
            entity.ΦΥΛΟ = data.ΦΥΛΟ;
            entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = data.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            entity.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = data.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ;
            entity.MASTER = data.MASTER;
            entity.ΔΙΔΑΚΤΟΡΙΚΟ = data.ΔΙΔΑΚΤΟΡΙΚΟ;
            entity.ΠΑΙΔΑΓΩΓΙΚΟ = data.ΠΑΙΔΑΓΩΓΙΚΟ;
            entity.ΠΤΥΧΙΟ = data.ΠΤΥΧΙΟ;
            entity.ΥΠΑΛΛΗΛΟΣ = data.ΥΠΑΛΛΗΛΟΣ;
            entity.ΔΙΕΥΘΥΝΣΗ = data.ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = data.EMAIL;
            entity.ΗΛΙΚΙΑ = Common.CalculateTeacherAge(entity);
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = data.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            entity.BANK_ACCOUNT = data.BANK_ACCOUNT;
            entity.BANK_NAME = data.BANK_NAME;
            entity.BANK_IBAN = data.BANK_IBAN;
            entity.ΑΔΤ = data.ΑΔΤ;
            entity.ΤΕΚΝΑ = data.ΤΕΚΝΑ;
            entity.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = data.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        private void CopyTeacherData(int targetTeacherId, string afm)
        {
            var source = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ where d.ΑΦΜ == afm && d.TEACHER_ID != targetTeacherId select d).FirstOrDefault();
            if (source == null) return;

            ΕΚΠΑΙΔΕΥΤΕΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ.Find(targetTeacherId);

            entity.ΑΔΤ = source.ΑΔΤ;
            entity.ΑΜΑ = source.ΑΜΑ;
            entity.ΑΜΚΑ = source.ΑΜΚΑ;
            entity.ΔΙΕΥΘΥΝΣΗ = source.ΔΙΕΥΘΥΝΣΗ;
            entity.ΔΟΥ = source.ΔΟΥ;
            entity.ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = source.ΟΝΟΜΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ;
            entity.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = source.ΠΑΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ;
            entity.ΜΗΤΡΩΝΥΜΟ = source.ΜΗΤΡΩΝΥΜΟ;
            entity.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ = source.ΜΗΤΡΩΝΥΜΟ_ΓΕΝΙΚΗ;
            entity.ΗΛΙΚΙΑ = source.ΗΛΙΚΙΑ;
            entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = source.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            entity.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = source.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ;
            entity.ΚΛΑΔΟΣ = source.ΚΛΑΔΟΣ;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = source.ΠΑΡΑΤΗΡΗΣΕΙΣ;
            entity.ΤΕΚΝΑ = source.ΤΕΚΝΑ;
            entity.ΤΗΛΕΦΩΝΑ = source.ΤΗΛΕΦΩΝΑ;
            entity.ΥΠΑΛΛΗΛΟΣ = source.ΥΠΑΛΛΗΛΟΣ;
            entity.ΦΥΛΟ = source.ΦΥΛΟ;
            entity.BANK_ACCOUNT = source.BANK_ACCOUNT;
            entity.BANK_IBAN = source.BANK_IBAN;
            entity.BANK_NAME = source.BANK_NAME;
            entity.EMAIL = source.EMAIL;
            entity.ΠΤΥΧΙΟ = source.ΠΤΥΧΙΟ;
            entity.MASTER = source.MASTER;
            entity.ΔΙΔΑΚΤΟΡΙΚΟ = source.ΔΙΔΑΚΤΟΡΙΚΟ;
            entity.ΠΑΙΔΑΓΩΓΙΚΟ = source.ΠΑΙΔΑΓΩΓΙΚΟ;
            entity.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ = source.ΜΙΣΘΟΔΟΣΙΑ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}