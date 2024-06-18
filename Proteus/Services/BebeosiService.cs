using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class BebeosiService : IBebeosiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public BebeosiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentBebeoseisViewModel> Read(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        orderby d.ΗΜΕΡΟΜΗΝΙΑ descending, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = d.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).ToList();

            return (data);
        }

        public void Create(StudentBebeoseisViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = new ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΑΜΚ = Common.GetStudentAmk(studentId),
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ,
                ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = data.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ
            };
            entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΒΕΒΑΙΩΣΗ_ΚΩΔ = entity.ΒΕΒΑΙΩΣΗ_ΚΩΔ;
        }

        public void Update(StudentBebeoseisViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΑΜΚ = Common.GetStudentAmk(studentId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΓΙΑ_ΧΡΗΣΗ = data.ΓΙΑ_ΧΡΗΣΗ;
            entity.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = data.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ;
            entity.ΠΑΡΑΔΟΔΗΚΕ = data.ΠΑΡΑΔΟΔΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentBebeoseisViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ entity = entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Find(data.ΒΕΒΑΙΩΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentBebeoseisViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ.Select(d => new StudentBebeoseisViewModel
            {
                ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                ΑΜΚ = d.ΑΜΚ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = d.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
            }).Where(d => d.ΒΕΒΑΙΩΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public StudentBebeoseisViewModel GetRecord(int bebeosiId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΕΒΑΙΩΣΕΙΣ
                        where d.ΒΕΒΑΙΩΣΗ_ΚΩΔ == bebeosiId
                        select new StudentBebeoseisViewModel
                        {
                            ΒΕΒΑΙΩΣΗ_ΚΩΔ = d.ΒΕΒΑΙΩΣΗ_ΚΩΔ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ = d.ΣΤΡΑΤΟΛΟΓΙΚΟ_ΓΡΑΦΕΙΟ,
                            ΓΙΑ_ΧΡΗΣΗ = d.ΓΙΑ_ΧΡΗΣΗ,
                            ΠΑΡΑΔΟΔΗΚΕ = d.ΠΑΡΑΔΟΔΗΚΕ ?? true
                        }).FirstOrDefault();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}