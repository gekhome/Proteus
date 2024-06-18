using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TeacherInfoService : ITeacherInfoService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TeacherInfoService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<TeacherInfoViewModel> Read()
        {
            var data = (from d in entities.sqlTEACHER_INFO
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new TeacherInfoViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            KLADOS_NAME = d.KLADOS_NAME,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΑΜΑ = d.ΑΜΑ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ ?? 0,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            GENDER = d.GENDER,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ,
                            ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ = d.ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ
                        }).ToList();
            return (data);
        }

        public IEnumerable<TeacherInfoViewModel> Read(int schoolId)
        {
            var data = (from d in entities.sqlTEACHER_INFO
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new TeacherInfoViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            KLADOS_NAME = d.KLADOS_NAME,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΑΜΑ = d.ΑΜΑ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ ?? 0,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            GENDER = d.GENDER,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ,
                            ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ = d.ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ
                        }).ToList();
            return data;
        }

        public TeacherInfoViewModel GetRecord(int teacherId)
        {
            var data = (from d in entities.sqlTEACHER_INFO
                        where d.TEACHER_ID == teacherId
                        orderby d.ΙΕΚ, d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new TeacherInfoViewModel
                        {
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            KLADOS_NAME = d.KLADOS_NAME,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΑΜΑ = d.ΑΜΑ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΗΛΙΚΙΑ = d.ΗΛΙΚΙΑ ?? 0,
                            ΔΙΕΥΘΥΝΣΗ = d.ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            GENDER = d.GENDER,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ,
                            ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ = d.ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ
                        }).FirstOrDefault();
            return data;
        }

        public List<TeacherPeriodsInfoViewModel> GetPeriods(int teacherId)
        {
            List<TeacherPeriodsInfoViewModel> data = new List<TeacherPeriodsInfoViewModel>();

            try
            {
                data = (from d in entities.sqlTEACHER_PERIODS_INFO
                        where d.TEACHER_ID == teacherId
                        select new TeacherPeriodsInfoViewModel
                        {
                            ΕΠ_ΚΩΔ = d.ΕΠ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = (int)d.ΙΕΚ,
                            ΠΕΡΙΟΔΟΣ = d.ΠΕΡΙΟΔΟΣ,
                            ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ = d.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗΣ,
                            ΑΠΟΦΑΣΗ = d.ΑΠΟΦΑΣΗ,
                            ΑΔΑ = d.ΑΔΑ
                        }).ToList();
            }
            catch { }

            return data;
        }

        public List<TeacherAnatheseisInfoViewModel> GetAnatheseis(int teacherId)
        {
            List<TeacherAnatheseisInfoViewModel> data = new List<TeacherAnatheseisInfoViewModel>();

            try
            {
                data = (from d in entities.sqlTEACHER_ANATHESEIS_INFO
                        where d.TEACHER_ID == teacherId
                        select new TeacherAnatheseisInfoViewModel
                        {
                            ΕΑ_ΚΩΔ = d.ΕΑ_ΚΩΔ,
                            TEACHER_ID = d.TEACHER_ID,
                            ΑΦΜ = d.ΑΦΜ,
                            ΙΕΚ = d.ΙΕΚ,
                            EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT,
                            LESSON_DESC = d.LESSON_DESC,
                            ΩΡΕΣ_ΘΕΩΡΙΑ = d.ΩΡΕΣ_ΘΕΩΡΙΑ ?? 0,
                            ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ = d.ΩΡΕΣ_ΕΡΓΑΣΤΗΡΙΟ ?? 0
                        }).ToList();
            }
            catch { }

            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}