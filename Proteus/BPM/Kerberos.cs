using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proteus.DAL;
using Proteus.Models;
using Proteus.BPM;

namespace Proteus.BPM
{
    public static class Kerberos
    {
        public const int TICKET_TIMEOUT_MINUTES = 240;
        public const int MIN_GRADUATION_YEAR = 1950;

        /// <summary>
        /// Υπολογίζει τις εργάσιμες ημέρες μεταξύ δύο ημερομηνιών,
        /// δηλ. χωρίς τα Σαββατοκύριακα.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="daycount"></returns>
        public static int WorkingDays(DateTime initial_date, DateTime final_date)
        {
            int daycount = 0;

            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            while (date1 <= date2)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daycount++;
                        break;
                    default:
                        break;
                }
                date1 = date1.AddDays(1);
            }
            return daycount;
        }


        #region DELETE RULES

        public static bool CanDeleteSchoolYear(int schoolyearId)
        {
            using (var db = new ProteusDBEntities())
            {
                var count = (from d in db.ΠΕΡΙΟΔΟΙ where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId select d).Count();
                if (count > 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool CanDeleteStudent(int studentId)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Where(d => d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        public static bool CanDeleteTeacher(int teacherId)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata1 = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ.Where(s => s.TEACHER_ID == teacherId).Count();
                var existingdata2 = db.ΕΚΠΑΙΔΕΥΤΕΣ_ΑΝΑΘΕΣΕΙΣ.Where(s => s.TEACHER_ID == teacherId).Count();

                if (existingdata1 == 0 && existingdata2 == 0) return true;
                else return false;
            }
        }

        public static bool CanDeleteEidikotita(int eidikotita, int iek)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΤΜΗΜΑ.Where(s => s.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ == eidikotita && s.ΙΕΚ == iek).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        public static bool CanDeleteTmima(int tmima, int iek)
        {
            using (var db = new ProteusDBEntities())
            {
                var data1 = db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ.Where(s => s.ΚΩΔ_ΤΜΗΜΑ == tmima && s.ΙΕΚ == iek).Count();
                var data2 = db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ.Where(s => s.ΚΩΔ_ΤΜΗΜΑ == tmima && s.ΙΕΚ == iek).Count();
                var data3 = db.ΠΡΟΓΡΑΜΜΑ_ΑΡΧΕΙΟ.Where(s => s.ΚΩΔ_ΤΜΗΜΑ == tmima && s.ΙΕΚ == iek).Count();

                if (data1 == 0 && data2 == 0 && data3 == 0) return true;
                else return false;
            }
        }

        public static bool CanDeleteErgodotis(int ergodotisId, int iek)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΕΡΓΟΔΟΤΕΣ_ΠΡΑΚΤΙΚΗ.Where(s => s.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ == ergodotisId && s.ΙΕΚ == iek).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        public static bool CanDeleteTeacherEidikotita(int eidikotitaId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ where d.ΕΙΔΙΚΟΤΗΤΑ == eidikotitaId select d).Count();

                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteKladosUnified(int kladosId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.SYS_EIDIKOTITES where d.KLADOS_UNIFIED == kladosId select d).Count();
                if (data > 0) return false;
                else return true;
            }
        }

        public static bool CanDeletePeriodos(int periodId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΤΜΗΜΑ where d.ΠΕΡΙΟΔΟΣ_ΚΩΔ == periodId select d).Count();
                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteEidikotitaKatartisi(int eidikotitaId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.IEK_EIDIKOTITES where d.EIDIKOTITA_ID == eidikotitaId select d).Count();
                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteIekEidikotita(int iekId, int eidikotitaId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΜΑΘΗΤΕΣ where d.ΙΕΚ == iekId && d.ΕΙΔΙΚΟΤΗΤΑ == eidikotitaId select d).Count();
                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteTmima(int tmimaId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId select d).Count();
                if (count > 0) return false;
                else return true;
            }
        }

        public static bool CanDeleteLesson(int lessonId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count1 = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ where d.ΚΩΔ_ΜΑΘΗΜΑ == lessonId select d).Count();
                int count2 = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΑΡΧΕΙΟ where d.ΚΩΔ_ΜΑΘΗΜΑ == lessonId select d).Count();

                if (count1 > 0 || count2 > 0) return false;
                else return true;
            }
        }

        public static bool CanDeletePraktikiAitisi(int aitisiId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΠΡΑΚΤΙΚΗ_ΑΠΟΦΑΣΗ where d.ΑΙΤΗΣΗ_ΚΩΔΙΚΟΣ == aitisiId select d).Count();
                if (data > 0) return false;
                else return true;
            }
        }


        #endregion


        #region PRIMARY KEY VALIDATORS

        public static bool ValidatePrimaryKeyStudent(int amk, int iek)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΜΑΘΗΤΕΣ.Where(s => s.ΑΜΚ == amk && s.ΙΕΚ == iek).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        public static bool ValidatePrimaryKeyTeacher(string afm, int iek, int eidikotita)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΕΚΠΑΙΔΕΥΤΕΣ.Where(s => s.ΑΦΜ == afm && s.ΙΕΚ == iek && s.ΕΙΔΙΚΟΤΗΤΑ == eidikotita).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        public static bool CanEditTeacherEidikotita(int teacherId)
        {
            bool ok = true;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ_ΠΕΡΙΟΔΟΙ where d.TEACHER_ID == teacherId select d).Count();
                if (data > 0) return false;

                return (ok);
            }
        }

        public static bool ValidatePrimaryKeyErgodotis(string afm, int iek)
        {
            using (var db = new ProteusDBEntities())
            {
                var existingdata = db.ΕΡΓΟΔΟΤΕΣ.Where(s => s.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ == afm && s.ΙΕΚ == iek).Count();
                if (existingdata == 0) return true;
                else return false;
            }
        }

        #endregion


        #region Ε-ΑΙΤΗΣΗ ΛΕΙΤΟΥΡΓΙΕΣ

        public static bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".PDF", ".JPG", ".JPEG" };

            List<string> allowed_extensions = new List<string>(extensions);

            if (allowed_extensions.Contains(extension.ToUpper()))
                return true;
            return false;
        }

        public static string ValidAitisiData(XmAitisiViewModel entity)
        {
            string errMsg = "";
            int _count = 0;

            bool rule1 = entity.ΕΙΔΙΚΟΤΗΤΑ1 == entity.ΕΙΔΙΚΟΤΗΤΑ2 && (entity.ΕΙΔΙΚΟΤΗΤΑ1 != null && entity.ΕΙΔΙΚΟΤΗΤΑ2 != null);
            bool rule2 = entity.ΕΙΔΙΚΟΤΗΤΑ1 == entity.ΕΙΔΙΚΟΤΗΤΑ3 && (entity.ΕΙΔΙΚΟΤΗΤΑ1 != null && entity.ΕΙΔΙΚΟΤΗΤΑ3 != null);
            bool rule3 = entity.ΕΙΔΙΚΟΤΗΤΑ2 == entity.ΕΙΔΙΚΟΤΗΤΑ3 && (entity.ΕΙΔΙΚΟΤΗΤΑ2 != null && entity.ΕΙΔΙΚΟΤΗΤΑ3 != null);
            bool rule4 = entity.ΕΙΔΙΚΟΤΗΤΑ4 == entity.ΕΙΔΙΚΟΤΗΤΑ5 && (entity.ΕΙΔΙΚΟΤΗΤΑ4 != null && entity.ΕΙΔΙΚΟΤΗΤΑ5 != null);

            bool rule5 = entity.ΕΙΔΙΚΟΤΗΤΑ1 > 0 && !(entity.TERM1 > 0);
            bool rule6 = entity.ΕΙΔΙΚΟΤΗΤΑ2 > 0 && !(entity.TERM2 > 0);
            bool rule7 = entity.ΕΙΔΙΚΟΤΗΤΑ3 > 0 && !(entity.TERM3 > 0);
            bool rule8 = entity.ΕΙΔΙΚΟΤΗΤΑ4 > 0 && !(entity.TERM4 > 0);
            bool rule9 = entity.ΕΙΔΙΚΟΤΗΤΑ5 > 0 && !(entity.TERM5 > 0);

            if (entity.ΤΡΙΤΕΚΝΟΣ == true && entity.ΠΟΛΥΤΕΚΝΟΣ == true) 
                errMsg = "-> Ο υποψήφιος δεν μπορεί να είναι τρίτεκνος και πολύτεκνος ταυτόχρονα.";

            if (entity.ΙΕΚ1 == entity.ΙΕΚ2)
                errMsg += "-> Το ΙΕΚ 2ης επιλογής δεν μπορεί να είναι ίδιο με αυτό της 1ης επιλογής.";

            if (!ValidGraduationDate(entity))
                errMsg += "-> Το έτος αποφοίτησης είναι εκτός αποδεκτής ελάχιστης τιμής.";

            // VALIDATE: ONLY 3 EIDIKOTITES MUST BE SELECTED
            if (entity.ΕΙΔΙΚΟΤΗΤΑ1 > 0) _count++;
            if (entity.ΕΙΔΙΚΟΤΗΤΑ2 > 0) _count++;
            if (entity.ΕΙΔΙΚΟΤΗΤΑ3 > 0) _count++;
            if (entity.ΕΙΔΙΚΟΤΗΤΑ4 > 0) _count++;
            if (entity.ΕΙΔΙΚΟΤΗΤΑ5 > 0) _count++;

            if (_count > 3)
                errMsg += "-> Έχετε επιλέξει περισσότερες από τρεις ειδικότητες. Πρέπει να επιλέξετε μέχρι τρεις.";

            if (rule1 || rule2 || rule3 || rule4)
                errMsg += "-> Πρέπει οι ειδικότητες προτίμησης να είναι διαφορετικές μεταξύ τους.";

            if (rule5 || rule6 || rule7 || rule8 || rule9)
                errMsg += "-> Οι επιλεγμένες ειδικότητες πρέπει να έχουν συμπληρωμένα και τα αντίστοιχα εξάμηνα.";

            return (errMsg);
        }

        public static bool ValidGraduationDate(XmAitisiViewModel entity)
        {
            DateTime grad_date = (DateTime)entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ;

            int grad_year = grad_date.Year;
            if (grad_year < MIN_GRADUATION_YEAR)
                return false;

            return true;
        }

        public static bool CandidateExists(string afm)
        {
            using (var db = new ProteusDBEntities())
            {
                int egykliosId = Common.GetOpenEgykliosID();
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == afm && d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId select d).FirstOrDefault();
                if (data == null)
                    return false;
                return true;
            }
        }

        public static bool CanDeleteUpload(int uploadId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();
                if (data > 0)
                    return false;
                return true;
            }
        }

        #endregion


        #region ΜΟΡΙΑ ΥΠΟΛΟΓΙΣΜΟΙ

        public static int GetMonthsWork(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            int months = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_sqlΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ where d.ΑΙΤΗΣΗ_ΚΩΔ == entity.ΑΙΤΗΣΗ_ΚΩΔ select d).FirstOrDefault();
                if (data != null) months = (int)data.ΜΗΝΕΣ;

                return months;
            }
        }

        public static decimal MoriaGrade(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria = 0.00M;

            if (entity.ΒΑΘΜΟΣ > 0)
            {
                moria = (decimal)entity.ΒΑΘΜΟΣ * 1.0M;
            }
            return (moria);
        }

        // ΑΛΛΑΓΗ ΑΠΟ 2022
        public static decimal MoriaWork(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria = 0.00M;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_sqlΕΜΠΕΙΡΙΑ_ΜΗΝΕΣ where d.ΑΙΤΗΣΗ_ΚΩΔ == entity.ΑΙΤΗΣΗ_ΚΩΔ select d).FirstOrDefault();
                if (data == null) return moria;

                if (data.ΜΗΝΕΣ > 0 && data.ΜΗΝΕΣ <= 3) moria = 2;
                else if (data.ΜΗΝΕΣ > 3 && data.ΜΗΝΕΣ <= 6) moria = 3;
                else if (data.ΜΗΝΕΣ > 6 && data.ΜΗΝΕΣ <= 9) moria = 4;
                else if (data.ΜΗΝΕΣ > 9 && data.ΜΗΝΕΣ <= 12) moria = 6;
                else if (data.ΜΗΝΕΣ > 12) moria = 6;

                moria = 0;  // από 2022 δεν μοριοδοτείται η προύπηρεσία
                return (moria);
            }
        }

        // ΑΛΛΑΓΗ ΑΠΟ 2022
        public static decimal MoriaApofitisi(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            int years = 0;

            if (entity.ΗΜΕΡΟΜΗΝΙΑ != null && entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ != null)
            {
                years = Common.Years((DateTime)entity.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ, (DateTime)entity.ΗΜΕΡΟΜΗΝΙΑ);
            }
            decimal moria;
            if (years <= 1) moria = 10;
            else if (years > 1 && years <= 2) moria = 7;
            else if (years > 2 && years <= 3) moria = 5;
            else if (years > 3) moria = 2;

            moria = 0;  // από 2022 δεν μοριοδοτείται το διάστημα αποφοίτησης
            return (moria);
        }

        public static decimal MoriaPolyteknos(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria = 0.00M;

            if (entity.ΠΟΛΥΤΕΚΝΟΣ == true) moria = 5;
            return moria;
        }

        public static decimal MoriaTriteknos(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria = 0.00M;

            if (entity.ΤΡΙΤΕΚΝΟΣ == true) moria = 3;
            return moria;
        }

        public static decimal MoriaMonogoneikos(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria = 0.00M;

            if (entity.ΜΟΝΟΓΟΝΕΙΚΟΣ == true) moria = 3;
            return moria;
        }

        public static decimal MoriaTotal(ΧΜ_ΥΠΟΨΗΦΙΟΣ entity)
        {
            decimal moria_total = 0.0M;

            moria_total = MoriaGrade(entity) + MoriaWork(entity) + MoriaApofitisi(entity) 
                        + MoriaPolyteknos(entity) + MoriaTriteknos(entity) + MoriaMonogoneikos(entity);

            return (moria_total);
        }

        #endregion
    }
}