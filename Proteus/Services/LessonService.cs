using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class LessonService : ILessonService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        private const int WEEKS = 15;

        public LessonService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<LessonsIekViewModel> Read(int eidikotitaId)
        {
            var data = (from d in entities.LESSONS_IEK
                        where d.LESSON_EIDIKOTITA == eidikotitaId
                        orderby d.LESSON_TERM, d.LESSON_TEXT, d.LESSON_TYPE
                        select new LessonsIekViewModel
                        {
                            LESSON_ID = d.LESSON_ID,
                            LESSON_TEXT = d.LESSON_TEXT,
                            LESSON_TERM = d.LESSON_TERM,
                            LESSON_TYPE = d.LESSON_TYPE,
                            LESSON_HOURS_WEEK = d.LESSON_HOURS_WEEK,
                            LESSON_HOURS = d.LESSON_HOURS,
                            LESSON_EIDIKOTITA = d.LESSON_EIDIKOTITA
                        }).ToList();
            return (data);
        }

        public void Create(LessonsIekViewModel data, int eidikotitaId)
        {
            LESSONS_IEK entity = new LESSONS_IEK()
            {
                LESSON_EIDIKOTITA = eidikotitaId,
                LESSON_TEXT = data.LESSON_TEXT,
                LESSON_TERM = data.LESSON_TERM,
                LESSON_TYPE = data.LESSON_TYPE,
                LESSON_HOURS_WEEK = data.LESSON_HOURS_WEEK,
                LESSON_HOURS = data.LESSON_HOURS_WEEK * WEEKS
            };
            entities.LESSONS_IEK.Add(entity);
            entities.SaveChanges();

            data.LESSON_ID = entity.LESSON_ID;
        }

        public void Update(LessonsIekViewModel data, int eidikotitaId)
        {
            LESSONS_IEK entity = entities.LESSONS_IEK.Find(data.LESSON_ID);

            entity.LESSON_EIDIKOTITA = eidikotitaId;
            entity.LESSON_TEXT = data.LESSON_TEXT;
            entity.LESSON_TERM = data.LESSON_TERM;
            entity.LESSON_TYPE = data.LESSON_TYPE;
            entity.LESSON_HOURS_WEEK = data.LESSON_HOURS_WEEK;
            entity.LESSON_HOURS = data.LESSON_HOURS_WEEK * WEEKS;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(LessonsIekViewModel data)
        {
            LESSONS_IEK entity = entities.LESSONS_IEK.Find(data.LESSON_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.LESSONS_IEK.Remove(entity);
                entities.SaveChanges();
            }
        }

        public LessonsIekViewModel Refresh(int entityId)
        {
            return entities.LESSONS_IEK.Select(d => new LessonsIekViewModel
            {
                LESSON_ID = d.LESSON_ID,
                LESSON_TEXT = d.LESSON_TEXT,
                LESSON_TERM = d.LESSON_TERM,
                LESSON_TYPE = d.LESSON_TYPE,
                LESSON_HOURS_WEEK = d.LESSON_HOURS_WEEK,
                LESSON_HOURS = d.LESSON_HOURS,
                LESSON_EIDIKOTITA = d.LESSON_EIDIKOTITA
            }).Where(d => d.LESSON_ID.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}