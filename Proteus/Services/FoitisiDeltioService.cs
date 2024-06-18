using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class FoitisiDeltioService : IFoitisiDeltioService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public FoitisiDeltioService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentFoitisiDeltioViewModel> Read(int studentId)
        {
            List<StudentFoitisiDeltioViewModel> data = new List<StudentFoitisiDeltioViewModel>();

            try
            {
                data = (from d in entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ
                        orderby d.ΗΜΕΡΟΜΗΝΙΑ descending, d.ΠΡΩΤΟΚΟΛΛΟ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId
                        select new StudentFoitisiDeltioViewModel
                        {
                            ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΠΑΡΑΔΟΘΗΚΕ = d.ΠΑΡΑΔΟΘΗΚΕ ?? true
                        }).ToList();
            }
            catch { }

            return data;
        }

        public void Create(StudentFoitisiDeltioViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ entity = new ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ()
            {
                ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                ΙΕΚ = schoolId,
                ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΑΡΑΔΟΘΗΚΕ = data.ΠΑΡΑΔΟΘΗΚΕ
            };
            entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΑΔΚ_ΚΩΔ = entity.ΑΔΚ_ΚΩΔ;
        }

        public void Update(StudentFoitisiDeltioViewModel data, int studentId, int schoolId)
        {
            ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ entity = entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ.Find(data.ΑΔΚ_ΚΩΔ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = studentId;
            entity.ΙΕΚ = schoolId;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΠΑΡΑΔΟΘΗΚΕ = data.ΠΑΡΑΔΟΘΗΚΕ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentFoitisiDeltioViewModel data)
        {
            ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ entity = entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ.Find(data.ΑΔΚ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public StudentFoitisiDeltioViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΕΣ_ΦΟΙΤΗΣΗΔΕΛΤΙΑ.Select(d => new StudentFoitisiDeltioViewModel
            {
                ΑΔΚ_ΚΩΔ = d.ΑΔΚ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΙΕΚ = d.ΙΕΚ,
                ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΠΑΡΑΔΟΘΗΚΕ = d.ΠΑΡΑΔΟΘΗΚΕ ?? true
            }).Where(d => d.ΑΔΚ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public List<FoitisiDeltioViewModel> GetGradesApousies(int studentId, int termId)
        {
            var data = (from d in entities.FOITISI_DELTIA
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.TERM_ID == termId
                        select new FoitisiDeltioViewModel
                        {
                            ROW_ID = d.ROW_ID,
                            ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ,
                            ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ,
                            ΜΟΒΠ = d.ΜΟΒΠ,
                            ΜΟΤΕ = d.ΜΟΤΕ,
                            ΜΟΕΠ = d.ΜΟΕΠ,
                            ΜΟ = d.ΜΟ,
                            ΒΕ = d.ΒΕ ?? 0.0M,
                            ΣΥΝΟΛΟ_ΩΡΕΣ = d.ΣΥΝΟΛΟ_ΩΡΕΣ,
                            ΟΡΙΟ = d.ΟΡΙΟ,
                            ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                            LESSON_COUNT = d.LESSON_COUNT,
                            GRADES_SUM = d.GRADES_SUM
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}