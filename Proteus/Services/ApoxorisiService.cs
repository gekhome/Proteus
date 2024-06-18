using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ApoxorisiService : IApoxorisiService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ApoxorisiService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<TeacherWithdrawalViewModel> Read(int schoolId)
        {
            var data = (from d in entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΕΡΟΜΗΝΙΑ descending
                        select new TeacherWithdrawalViewModel
                        {
                            ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΙΤΙΟΛΟΓΙΑ = d.ΑΙΤΙΟΛΟΓΙΑ
                        }).ToList();
            return data;
        }

        public void Create(TeacherWithdrawalViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ entity = new ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ()
            {
                TEACHER_ID = data.TEACHER_ID,
                ΑΦΜ = Common.GetTeacherAfm((int)data.TEACHER_ID),
                ΙΕΚ = schoolId,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΙΤΙΟΛΟΓΙΑ = data.ΑΙΤΙΟΛΟΓΙΑ
            };
            entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = entity.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ;
        }

        public void Update(TeacherWithdrawalViewModel data, int schoolId)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ.Find(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ);

            entity.TEACHER_ID = data.TEACHER_ID;
            entity.ΑΦΜ = Common.GetTeacherAfm((int)data.TEACHER_ID);
            entity.ΙΕΚ = schoolId;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΑΙΤΙΟΛΟΓΙΑ = data.ΑΙΤΙΟΛΟΓΙΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(TeacherWithdrawalViewModel data)
        {
            ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ entity = entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ.Find(data.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public TeacherWithdrawalViewModel Refresh(int entityId)
        {
            return entities.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΠΟΧΩΡΗΣΕΙΣ.Select(d => new TeacherWithdrawalViewModel
            {
                ΑΠΟΧΩΡΗΣΗ_ΚΩΔ = d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ,
                TEACHER_ID = d.TEACHER_ID,
                ΑΦΜ = d.ΑΦΜ,
                ΙΕΚ = d.ΙΕΚ,
                ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΙΤΙΟΛΟΓΙΑ = d.ΑΙΤΙΟΛΟΓΙΑ
            }).Where(d => d.ΑΠΟΧΩΡΗΣΗ_ΚΩΔ.Equals(entityId)).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}