using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class ProgrammaService : IProgrammaService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public ProgrammaService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ProgrammaDayViewModel> Read(int tmimaId, DateTime? theDate)
        {
            var data = (from d in entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ == theDate
                        select new ProgrammaDayViewModel
                        {
                            ΠΡΟΓΡΑΜΜΑ_ΚΩΔ = d.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ,
                            ΕΒΔΟΜΑΔΑ = d.ΕΒΔΟΜΑΔΑ ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3 ?? 0,
                            Π1 = d.Π1 ?? false,
                            Π2 = d.Π2 ?? false,
                            Π3 = d.Π3 ?? false,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ ?? 0,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΩΡΑ = d.ΩΡΑ
                        }).ToList();
            return data;
        }

        public void Create(ProgrammaDayViewModel data, int tmimaId, DateTime? theDate, int week, int schoolId)
        {
            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
            {
                ΕΒΔΟΜΑΔΑ = week,
                ΕΚΠΑΙΔΕΥΤΗΣ1 = data.ΕΚΠΑΙΔΕΥΤΗΣ1,
                ΕΚΠΑΙΔΕΥΤΗΣ2 = data.ΕΚΠΑΙΔΕΥΤΗΣ2,
                ΕΚΠΑΙΔΕΥΤΗΣ3 = data.ΕΚΠΑΙΔΕΥΤΗΣ3,
                Π1 = data.Π1,
                Π2 = data.Π2,
                Π3 = data.Π3,
                ΗΜΕΡΟΜΗΝΙΑ = (DateTime)theDate,
                ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                ΙΕΚ = schoolId,
                ΚΩΔ_ΕΡΓΑΣΙΑ = data.ΚΩΔ_ΕΡΓΑΣΙΑ,
                ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ,
                ΩΡΑ = data.ΩΡΑ
            };
            entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(entity);
            entities.SaveChanges();

            data.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ = entity.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ;
        }

        public void Update(ProgrammaDayViewModel data, int tmimaId, DateTime? theDate, int week, int schoolId)
        {
            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Find(data.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ);

            entity.ΕΒΔΟΜΑΔΑ = week;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ1 = data.ΕΚΠΑΙΔΕΥΤΗΣ1;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ2 = data.ΕΚΠΑΙΔΕΥΤΗΣ2;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ3 = data.ΕΚΠΑΙΔΕΥΤΗΣ3;
            entity.Π1 = data.Π1;
            entity.Π2 = data.Π2;
            entity.Π3 = data.Π3;
            entity.ΗΜΕΡΟΜΗΝΙΑ = (DateTime)theDate;
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΕΡΓΑΣΙΑ = data.ΚΩΔ_ΕΡΓΑΣΙΑ;
            entity.ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ;
            entity.ΚΩΔ_ΤΜΗΜΑ = tmimaId;
            entity.ΩΡΑ = data.ΩΡΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ProgrammaDayViewModel data)
        {
            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Find(data.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public IEnumerable<ProgrammaDayViewModel> Read(int tmimaId, DateTime? theDate1, DateTime? theDate2)
        {
            List<ProgrammaDayViewModel> data = new List<ProgrammaDayViewModel>();

            if (tmimaId > 0 && theDate1 != null && theDate2 != null)
            {
                data = (from d in entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                        where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΗΜΕΡΟΜΗΝΙΑ >= theDate1 && d.ΗΜΕΡΟΜΗΝΙΑ <= theDate2
                        orderby d.ΕΒΔΟΜΑΔΑ, d.ΗΜΕΡΟΜΗΝΙΑ, d.ΩΡΑ
                        select new ProgrammaDayViewModel
                        {
                            ΠΡΟΓΡΑΜΜΑ_ΚΩΔ = d.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ,
                            ΕΒΔΟΜΑΔΑ = d.ΕΒΔΟΜΑΔΑ ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ1 = d.ΕΚΠΑΙΔΕΥΤΗΣ1 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ2 = d.ΕΚΠΑΙΔΕΥΤΗΣ2 ?? 0,
                            ΕΚΠΑΙΔΕΥΤΗΣ3 = d.ΕΚΠΑΙΔΕΥΤΗΣ3 ?? 0,
                            Π1 = d.Π1 ?? true,
                            Π2 = d.Π2 ?? false,
                            Π3 = d.Π3 ?? false,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΚΩΔ_ΕΡΓΑΣΙΑ = d.ΚΩΔ_ΕΡΓΑΣΙΑ ?? 0,
                            ΚΩΔ_ΜΑΘΗΜΑ = d.ΚΩΔ_ΜΑΘΗΜΑ ?? 0,
                            ΚΩΔ_ΤΜΗΜΑ = d.ΚΩΔ_ΤΜΗΜΑ,
                            ΩΡΑ = d.ΩΡΑ
                        }).ToList();
            }
            return data;
        }

        public void Create(ProgrammaDayViewModel data, int tmimaId, int schoolId)
        {
            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = new ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ()
            {
                ΕΒΔΟΜΑΔΑ = data.ΕΒΔΟΜΑΔΑ,
                ΕΚΠΑΙΔΕΥΤΗΣ1 = data.ΕΚΠΑΙΔΕΥΤΗΣ1,
                ΕΚΠΑΙΔΕΥΤΗΣ2 = data.ΕΚΠΑΙΔΕΥΤΗΣ2,
                ΕΚΠΑΙΔΕΥΤΗΣ3 = data.ΕΚΠΑΙΔΕΥΤΗΣ3,
                Π1 = data.Π1,
                Π2 = data.Π2,
                Π3 = data.Π3,
                ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ,
                ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                ΙΕΚ = schoolId,
                ΚΩΔ_ΕΡΓΑΣΙΑ = data.ΚΩΔ_ΕΡΓΑΣΙΑ,
                ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ,
                ΩΡΑ = data.ΩΡΑ
            };
            entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Add(entity);
            entities.SaveChanges();
        }

        public void Update(ProgrammaDayViewModel data, int tmimaId, int schoolId)
        {
            ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ entity = entities.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Find(data.ΠΡΟΓΡΑΜΜΑ_ΚΩΔ);

            entity.ΕΒΔΟΜΑΔΑ = data.ΕΒΔΟΜΑΔΑ;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ1 = data.ΕΚΠΑΙΔΕΥΤΗΣ1;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ2 = data.ΕΚΠΑΙΔΕΥΤΗΣ2;
            entity.ΕΚΠΑΙΔΕΥΤΗΣ3 = data.ΕΚΠΑΙΔΕΥΤΗΣ3;
            entity.Π1 = data.Π1;
            entity.Π2 = data.Π2;
            entity.Π3 = data.Π3;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΙΕΚ = schoolId;
            entity.ΚΩΔ_ΕΡΓΑΣΙΑ = data.ΚΩΔ_ΕΡΓΑΣΙΑ;
            entity.ΚΩΔ_ΜΑΘΗΜΑ = data.ΚΩΔ_ΜΑΘΗΜΑ;
            entity.ΚΩΔ_ΤΜΗΜΑ = tmimaId;
            entity.ΩΡΑ = data.ΩΡΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}