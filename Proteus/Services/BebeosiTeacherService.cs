using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class BebeosiTeacherService : IBebeosiTeacherService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public BebeosiTeacherService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TeacherBebeoseisViewModel> Read(int teacherId)
        {
            // Πριν την ημερομηνία αυτή οι βεβαιώσεις είναι στο αρχείο
            DateTime value = new DateTime(2019, 08, 31);

            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        orderby d.ΕΚΠΑΙΔΕΥΤΕΣ.ΕΠΩΝΥΜΟ, d.ΕΚΠΑΙΔΕΥΤΕΣ.ΟΝΟΜΑ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        where d.TEACHER_ID == teacherId && d.ΗΜΕΡΟΜΗΝΙΑ >= value
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).ToList();
            return data;
        }

        public void Create(TeacherBebeoseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ()
            {
                ΒΕΒΑΙΩΣΗ_ΚΩΔ = data.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                TEACHER_ID = teacherId,
                ΑΦΜ = Common.GetTeacherAfm(teacherId),
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΒΕΒΑΙΩΣΗ_ΚΩΔ = entity.ΒΕΒΑΙΩΣΗ_ΚΩΔ;
        }

        public void Update(TeacherBebeoseisViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            entity.TEACHER_ID = teacherId;
            entity.ΑΦΜ = Common.GetTeacherAfm(teacherId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ;
            entity.ΓΙΑ_ΧΡΗΣΗ = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ).ΓΙΑ_ΧΡΗΣΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherBebeoseisViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherBebeoseisViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Select(d => new TeacherBebeoseisViewModel
            {
                ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
            }).Where(d => d.ΒΕΒΑΙΩΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public List<TeacherBebeoseisViewModel> ReadArchive(int teacherId)
        {
            // Πριν την ημερομηνία αυτή οι βεβαιώσεις είναι στο αρχείο
            DateTime value = new DateTime(2019, 08, 31);

            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        orderby d.ΕΚΠΑΙΔΕΥΤΕΣ.ΕΠΩΝΥΜΟ, d.ΕΚΠΑΙΔΕΥΤΕΣ.ΟΝΟΜΑ, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        where d.TEACHER_ID == teacherId && d.ΗΜΕΡΟΜΗΝΙΑ < value
                        select new TeacherBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}