using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class GradesService : IGradesService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public GradesService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentGradesViewModel> Read(int tmimaId, int lessonId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ
                        orderby d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΚΩΔ_ΜΑΘΗΜΑ == lessonId
                        select new StudentGradesViewModel
                        {
                            ΜΒ_ΚΩΔ = d.ΜΒ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ ?? 0,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ ?? 0,
                            ΑΜΚ = d.ΑΜΚ ?? 0,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΧΑΡΑΚΤΗΡΙΣΜΟΣ = d.ΧΑΡΑΚΤΗΡΙΣΜΟΣ,
                            ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = d.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ,
                            ΒΑΘΜΟΣ_ΤΕ = d.ΒΑΘΜΟΣ_ΤΕ,
                            ΒΑΘΜΟΣ_ΕΠ = d.ΒΑΘΜΟΣ_ΕΠ,
                            ΒΑΘΜΟΣ_ΜΟ = d.ΒΑΘΜΟΣ_ΜΟ
                        }).ToList();
            return data;
        }

        public void Create(StudentGradesViewModel data, int tmimaId, int lessonId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = new ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΚΩΔ_ΜΑΘΗΜΑ = lessonId,
                ΙΕΚ = schoolId,
                ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ),
                ΧΑΡΑΚΤΗΡΙΣΜΟΣ = data.ΧΑΡΑΚΤΗΡΙΣΜΟΣ,
                ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = data.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ,
                ΒΑΘΜΟΣ_ΤΕ = data.ΒΑΘΜΟΣ_ΤΕ,
                ΒΑΘΜΟΣ_ΕΠ = data.ΒΑΘΜΟΣ_ΕΠ,
                ΒΑΘΜΟΣ_ΜΟ = Common.CalculateGradeAverage(data)

            };
            entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΜΒ_ΚΩΔ = entity.ΜΒ_ΚΩΔ;
            data.ΒΑΘΜΟΣ_ΜΟ = entity.ΒΑΘΜΟΣ_ΜΟ;
        }

        public void Update(StudentGradesViewModel data, int tmimaId, int lessonId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Find(data.ΜΒ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΚΩΔ_ΜΑΘΗΜΑ = lessonId;
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΤΜΗΜΑ = tmimaId;
            entity.ΑΜΚ = Common.GetStudentAmk((int)data.ΜΑΘΗΤΗΣ_ΚΩΔ);
            entity.ΧΑΡΑΚΤΗΡΙΣΜΟΣ = data.ΧΑΡΑΚΤΗΡΙΣΜΟΣ;
            entity.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ = data.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ;
            entity.ΒΑΘΜΟΣ_ΤΕ = data.ΒΑΘΜΟΣ_ΤΕ;
            entity.ΒΑΘΜΟΣ_ΕΠ = data.ΒΑΘΜΟΣ_ΕΠ;
            entity.ΒΑΘΜΟΣ_ΜΟ = Common.CalculateGradeAverage(data);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();

            data.ΒΑΘΜΟΣ_ΜΟ = entity.ΒΑΘΜΟΣ_ΜΟ;
        }

        public void Destroy(StudentGradesViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Find(data.ΜΒ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}