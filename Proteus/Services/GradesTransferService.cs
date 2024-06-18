using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class GradesTransferService : IGradesTransferService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public GradesTransferService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentGradesViewModel> Read(int tmimaId, int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ
                        orderby d.LESSONS_IEK.LESSON_TEXT, d.LESSONS_IEK.LESSON_TYPE
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
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

        public void Create(StudentGradesViewModel data, int tmimaId, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = new ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ,
                ΙΕΚ = schoolId,
                ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                ΑΜΚ = Common.GetStudentAmk(studentId),
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

        public void Update(StudentGradesViewModel data, int tmimaId, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ entity = entities.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ.Find(data.ΜΒ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΤΜΗΜΑ = tmimaId;
            entity.ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ;
            entity.ΑΜΚ = Common.GetStudentAmk(studentId);
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