using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class EidikotitaEgykliosService : IEidikotitaEgykliosService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public EidikotitaEgykliosService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<XmEgykliosEidikotitesViewModel> Read(int egykliosId, int schoolId)
        {
            var data = (from d in entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == schoolId
                        orderby d.ΧΜ_ΕΙΔΙΚΟΤΗΤΕΣ.EIDIKOTITA_TEXT
                        select new XmEgykliosEidikotitesViewModel
                        {
                            RECORD_ID = d.RECORD_ID,
                            EGYKLIOS_ID = d.EGYKLIOS_ID,
                            SCHOOL_ID = d.SCHOOL_ID,
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            TERM_ID = d.TERM_ID ?? 0,
                            STUDENTS = d.STUDENTS
                        }).ToList();
            return data;
        }

        public void Create(XmEgykliosEidikotitesViewModel data, int egykliosId, int schoolId)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = new ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ()
            {
                EGYKLIOS_ID = egykliosId,
                SCHOOL_ID = schoolId,
                EIDIKOTITA_ID = data.EIDIKOTITA_ID,
                TERM_ID = data.TERM_ID,
                STUDENTS = data.STUDENTS
            };
            entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.EGYKLIOS_ID = entity.EGYKLIOS_ID;
        }

        public void Update(XmEgykliosEidikotitesViewModel data, int egykliosId, int schoolId)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.RECORD_ID);

            entity.EGYKLIOS_ID = egykliosId;
            entity.SCHOOL_ID = schoolId;
            entity.EIDIKOTITA_ID = data.EIDIKOTITA_ID;
            entity.TERM_ID = data.TERM_ID;
            entity.STUDENTS = data.STUDENTS;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(XmEgykliosEidikotitesViewModel data)
        {
            ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.RECORD_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}