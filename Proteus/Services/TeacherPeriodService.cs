using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TeacherPeriodService : ITeacherPeriodService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TeacherPeriodService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TeacherPeriodsViewModel> Read(int teacherId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ
                        where d.TEACHER_ID == teacherId
                        select new TeacherPeriodsViewModel
                        {
                            ΕΠ_ΚΩΔ = d.ΕΠ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ,
                            ΑΠΟΦΑΣΗ = d.ΑΠΟΦΑΣΗ,
                            ΑΔΑ = d.ΑΔΑ
                        }).ToList();
            return data;
        }

        public void Create(TeacherPeriodsViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ()
            {
                ΑΦΜ = Common.GetTeacherAfm(teacherId),
                ΙΕΚ = schoolId,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = data.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ,
                ΑΠΟΦΑΣΗ = data.ΑΠΟΦΑΣΗ,
                ΑΔΑ = data.ΑΔΑ,
                TEACHER_ID = teacherId
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΕΠ_ΚΩΔ = entity.ΕΠ_ΚΩΔ;
        }

        public void Update(TeacherPeriodsViewModel data, int teacherId, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Find(data.ΕΠ_ΚΩΔ);

            entity.ΑΦΜ = Common.GetTeacherAfm(teacherId);
            entity.ΙΕΚ = schoolId;
            entity.ΠΕΡΙΟΔΟΣ_ΚΩΔ = data.ΠΕΡΙΟΔΟΣ_ΚΩΔ;
            entity.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = data.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ;
            entity.ΑΠΟΦΑΣΗ = data.ΑΠΟΦΑΣΗ;
            entity.ΑΔΑ = data.ΑΔΑ;
            entity.TEACHER_ID = teacherId;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherPeriodsViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Find(data.ΕΠ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherPeriodsViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Select(d => new TeacherPeriodsViewModel
            {
                ΕΠ_ΚΩΔ = d.ΕΠ_ΚΩΔ,
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = (int)d.ΙΕΚ,
                ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ,
                ΑΠΟΦΑΣΗ = d.ΑΠΟΦΑΣΗ,
                ΑΔΑ = d.ΑΔΑ
            }).Where(d => d.ΕΠ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}