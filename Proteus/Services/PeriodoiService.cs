using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class PeriodoiService : IPeriodoiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PeriodoiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<PeriodosViewModel> Read()
        {
            var data = (from d in entities.ΠΕΡΙΟΔΟΙ
                        orderby d.ΠΕΡΙΟΔΟΣ
                        select new PeriodosViewModel
                        {
                            PERIOD_ID = d.PERIOD_ID,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΧΕ_ΚΩΔ = d.ΧΕ_ΚΩΔ,
                            ΗΜΝΙΑ_ΕΝΑΡΞΗΣ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗΣ,
                            ΗΜΝΙΑ_ΛΗΞΗΣ = d.ΗΜΝΙΑ_ΛΗΞΗΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            SY_TEXT = d.SY_TEXT,
                            ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ,
                            ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ = d.ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(PeriodosViewModel data)
        {
            ΠΕΡΙΟΔΟΙ entity = new ΠΕΡΙΟΔΟΙ()
            {
                ΠΕΡΙΟΔΟΣ = data.ΠΕΡΙΟΔΟΣ,
                ΧΕ_ΚΩΔ = data.ΧΕ_ΚΩΔ,
                ΗΜΝΙΑ_ΕΝΑΡΞΗΣ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗΣ,
                ΗΜΝΙΑ_ΛΗΞΗΣ = data.ΗΜΝΙΑ_ΛΗΞΗΣ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                SY_TEXT = GetSchoolYearText(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ),
                ΔΙΟΙΚΗΤΗΣ = data.ΔΙΟΙΚΗΤΗΣ,
                ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ = data.ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ
            };
            entities.ΠΕΡΙΟΔΟΙ.Add(entity);
            entities.SaveChanges();

            data.PERIOD_ID = entity.PERIOD_ID;
        }

        public void Update(PeriodosViewModel data)
        {
            ΠΕΡΙΟΔΟΙ entity = entities.ΠΕΡΙΟΔΟΙ.Find(data.PERIOD_ID);

            entity.PERIOD_ID = data.PERIOD_ID;
            entity.ΠΕΡΙΟΔΟΣ = data.ΠΕΡΙΟΔΟΣ;
            entity.ΧΕ_ΚΩΔ = data.ΧΕ_ΚΩΔ;
            entity.ΗΜΝΙΑ_ΕΝΑΡΞΗΣ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗΣ;
            entity.ΗΜΝΙΑ_ΛΗΞΗΣ = data.ΗΜΝΙΑ_ΛΗΞΗΣ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.SY_TEXT = GetSchoolYearText(data.ΣΧΟΛΙΚΟ_ΕΤΟΣ);
            entity.ΔΙΟΙΚΗΤΗΣ = data.ΔΙΟΙΚΗΤΗΣ;
            entity.ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ = data.ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(PeriodosViewModel data)
        {
            ΠΕΡΙΟΔΟΙ entity = entities.ΠΕΡΙΟΔΟΙ.Find(data.PERIOD_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΕΡΙΟΔΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public PeriodosViewModel Refresh(int entityId)
        {
            return entities.ΠΕΡΙΟΔΟΙ.Select(d => new PeriodosViewModel
            {
                PERIOD_ID = d.PERIOD_ID,
                ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                ΧΕ_ΚΩΔ = d.ΧΕ_ΚΩΔ,
                ΗΜΝΙΑ_ΕΝΑΡΞΗΣ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗΣ,
                ΗΜΝΙΑ_ΛΗΞΗΣ = d.ΗΜΝΙΑ_ΛΗΞΗΣ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                SY_TEXT = d.SY_TEXT,
                ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ,
                ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ = d.ΔΙΟΙΚΗΤΗΣ_ΦΥΛΟ
            }).Where(d => d.PERIOD_ID.Equals(entityId)).FirstOrDefault();
        }

        private string GetSchoolYearText(int? schoolyearId)
        {
            var data = (from d in entities.SYS_SCHOOLYEARS where d.SY_ID == schoolyearId select d).FirstOrDefault();
            string SchoolYearText = data.SY_TEXT;

            return SchoolYearText;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}