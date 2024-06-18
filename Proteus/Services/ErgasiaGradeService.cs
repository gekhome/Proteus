using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ErgasiaGradeService : IErgasiaGradeService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ErgasiaGradeService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ErgasiaGradeViewModel> Read(int tmimaId, string lessonText)
        {
            var data = (from d in entities.STUDENT_ERGASIES
                        orderby d.ΜΑΘΗΤΕΣ.ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΕΣ.ΟΝΟΜΑ
                        where d.TMIMA_ID == tmimaId && d.LESSON_TEXT == lessonText
                        select new ErgasiaGradeViewModel
                        {
                            ERGASIA_ID = d.ERGASIA_ID,
                            STUDENT_ID = d.STUDENT_ID,
                            IEK = d.IEK,
                            TMIMA_ID = d.TMIMA_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            TERM_ID = d.TERM_ID,
                            LESSON_TEXT = d.LESSON_TEXT,
                            ERGASIA_TYPE = d.ERGASIA_TYPE ?? 0,
                            GRADE = d.GRADE
                        }).ToList();
            return data;
        }

        public void Create(ErgasiaGradeViewModel data, int tmimaId, string lessonText, int schoolId)
        {
            STUDENT_ERGASIES entity = new STUDENT_ERGASIES()
            {
                LESSON_TEXT = lessonText,
                IEK = schoolId,
                TMIMA_ID = tmimaId,
                STUDENT_ID = data.STUDENT_ID,
                ERGASIA_TYPE = data.ERGASIA_TYPE,
                GRADE = data.GRADE,
                EIDIKOTITA_ID = Common.GetEidikotitaFromTmima(tmimaId),
                TERM_ID = Common.GetTermFromTmima(tmimaId)
            };
            entities.STUDENT_ERGASIES.Add(entity);
            entities.SaveChanges();

            data.ERGASIA_ID = entity.ERGASIA_ID;
        }

        public void Update(ErgasiaGradeViewModel data, int tmimaId, string lessonText, int schoolId)
        {
            STUDENT_ERGASIES entity = entities.STUDENT_ERGASIES.Find(data.ERGASIA_ID);

            entity.LESSON_TEXT = lessonText;
            entity.IEK = schoolId;
            entity.TMIMA_ID = tmimaId;
            entity.STUDENT_ID = data.STUDENT_ID;
            entity.ERGASIA_TYPE = data.ERGASIA_TYPE;
            entity.GRADE = data.GRADE;
            entity.EIDIKOTITA_ID = Common.GetEidikotitaFromTmima(tmimaId);
            entity.TERM_ID = Common.GetTermFromTmima(tmimaId);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ErgasiaGradeViewModel data)
        {
            STUDENT_ERGASIES entity = entities.STUDENT_ERGASIES.Find(data.ERGASIA_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.STUDENT_ERGASIES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}