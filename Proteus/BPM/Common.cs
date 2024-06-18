using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Proteus.DAL;
using Proteus.Models;
using Proteus.BPM;
using Proteus.Notification;

namespace Proteus.BPM
{

    public static class Common
    {
        #region String Functions (equivalent to VB)

        public static string Right(string text, int numberCharacters)
        {
            return text.Substring(numberCharacters > text.Length ? 0 : text.Length - numberCharacters);
        }

        public static string Left(string text, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length <= length)
                return text;
            else
                return text.Substring(0, length);
        }

        public static int Len(string text)
        {
            int _length;
            _length = text.Length;
            return _length;
        }

        public static byte Asc(string src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src + "")[0]);
        }

        public static char Chr(byte src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] { src })[0]);
        }

        public static bool IsNumber(string param)
        {
            Regex isNum = new Regex("[^0-9]");
            return !isNum.IsMatch(param);
        }

        #endregion


        #region Date Functions

        public static int DayOfWeek(int day, int month, int year)
        {
            string strDate = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            DateTime theDate;
            int weekday;

            if (DateTime.TryParse(strDate, out theDate) == true)
            {
                theDate = Convert.ToDateTime(strDate);
                weekday = (int)theDate.DayOfWeek;
            }
            else weekday = -1;

            return (weekday);
        }

        /// <summary>
        /// Μετατρέπει τον αριθμό ημέρας εβδομάδας σε
        /// ελληνικό λεκτικό
        /// </summary>
        /// <param name="weekday"></param>
        /// <returns>DayString</returns>
        public static string WeekdayToString(int weekday)
        {
            string DayString = "";

            switch (weekday)
            {
                case 0: DayString = "Κυριακή"; break;
                case 1: DayString = "Δευτέρα"; break;
                case 2: DayString = "Τρίτη"; break;
                case 3: DayString = "Τετάρτη"; break;
                case 4: DayString = "Πέμπτη"; break;
                case 5: DayString = "Παρασκευή"; break;
                case 6: DayString = "Σάββατο"; break;
                default: break;
            }
            return (DayString);
        }

        /// <summary>
        /// Μετατρέπει τον αριθμό του μήνα σε λεκτικό
        /// στη γενική πτώση.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string MonthToGRstring(int m)
        {
            string stGRmonth = "";

            switch (m)
            {
                case 1: stGRmonth = "Ιανουαρίου"; break;
                case 2: stGRmonth = "Φεβρουαρίου"; break;
                case 3: stGRmonth = "Μαρτίου"; break;
                case 4: stGRmonth = "Απριλίου"; break;
                case 5: stGRmonth = "Μαϊου"; break;
                case 6: stGRmonth = "Ιουνίου"; break;
                case 7: stGRmonth = "Ιουλίου"; break;
                case 8: stGRmonth = "Αυγούστου"; break;
                case 9: stGRmonth = "Σεπτεμβρίου"; break;
                case 10: stGRmonth = "Οκτωβρίου"; break;
                case 11: stGRmonth = "Νοεμβρίου"; break;
                case 12: stGRmonth = "Δεκεμβρίου"; break;
                default: break;
            }
            return stGRmonth;
        }

        /// <summary>
        /// Ελέγχει αν η αρχική ημερομηνία είναι μικρότερη
        /// ή ίση με την τελική ημερομηνία.
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static bool ValidStartEndDates(DateTime dateStart, DateTime dateEnd)
        {
            bool result;

            if (dateStart > dateEnd)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες ανήκουν στο ίδιο έτος.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool DatesInSameYear(DateTime date1, DateTime date2)
        {
            bool result;

            if (date1.Year != date2.Year)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες είναι μέσα στο ίδιο Σχ. Έτος
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="schoolYearID"></param>
        /// <returns></returns>
        public static bool DatesInSchoolYear(DateTime dateStart, DateTime dateEnd, int schoolYearID)
        {
            bool result = true;

            using (var db = new ProteusDBEntities())
            {
                var schoolYear = (from s in db.SYS_SCHOOLYEARS
                                  where s.SY_ID == schoolYearID
                                  select new { s.SY_DATESTART, s.SY_DATEEND }).FirstOrDefault();

                if (dateStart < schoolYear.SY_DATESTART || dateEnd > schoolYear.SY_DATEEND)
                    result = false;

                return result;
            }
        }

        /// <summary>
        /// Ελέγχει αν το σχολικό έτος έχει τη μορφή ΝΝΝΝ-ΝΝΝΝ
        /// και αν τα έτη είναι συμβατά με τις ημερομηνίες
        /// έναρξης και λήξης.
        /// </summary>
        /// <param name="syear"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool VerifySchoolYear(string syear, DateTime d1, DateTime d2)
        {

            if (syear.IndexOf('-') == -1)
            {
                return false;
            }

            string[] split = syear.Split(new Char[] { '-' });
            string sy1 = Convert.ToString(split[0]);
            string sy2 = Convert.ToString(split[1]);

            if (!IsNumber(sy1) || !IsNumber(sy2))
            {
                return false;
            }
            else
            {
                int y1 = Convert.ToInt32(sy1);
                int y2 = Convert.ToInt32(sy2);

                if (y2 - y1 > 1 || y2 - y1 <= 0)
                {
                    return false;
                }
                if (d1.Year != y1 || d2.Year != y2)
                {
                    return false;
                }
            }
            // at this point everything is ok
            return true;
        }

        /// <summary>
        /// Ελέγχει αν το χολικό έτος μορφής ΝΝΝΝ-ΝΝΝΝ υπάρχει ήδη.
        /// </summary>
        /// <param name="syear"></param>
        /// <returns></returns>
        public static bool SchoolYearExists(int syear)
        {
            using (var db = new ProteusDBEntities())
            {
                var syear_recs = (from s in db.SYS_SCHOOLYEARS
                                  where s.SY_ID == syear
                                  select s).Count();
                if (syear_recs != 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Υπολογίζει τα έτη (στρογγυλοποιημένα) μεταξύ δύο ημερομηνιών
        /// </summary>
        /// <param name="sdate">αρχική ημερομηνία</param>
        /// <param name="edate">τελική ημερομηνία</param>
        /// <returns></returns>
        public static int YearsDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            double _years = days / 365.25;

            int years = Convert.ToInt32(Math.Ceiling(_years));

            return years;
        }

        public static int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        public static int DaysDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            return days;
        }

        public static int CalculateMonths(DateTime? startDate, DateTime? endDate)
        {
            int value = 0;

            if (startDate == null || endDate == null) return value;

            DateTime date1 = (DateTime)startDate;
            DateTime date2 = (DateTime)endDate;

            double _months = Math.Abs(date2.Subtract(date1).Days / (365.25 / 12.0));
            value = (int)Math.Round(_months, 0);

            return value;
        }

        /// <summary>	
        /// Get Orthodox easter for requested year	
        /// </summary>	
        /// <param name="year">Year of easter</param>	
        /// <returns>DateTime of Orthodox Easter</returns>	

        public static DateTime GetOrthodoxEaster(int year)
        {
            var a = year % 19;
            var b = year % 7;
            var c = year % 4;

            var d = (19 * a + 16) % 30;
            var e = (2 * c + 4 * b + 6 * d) % 7;
            var f = (19 * a + 16) % 30;

            var key = f + e + 3;
            var month = (key > 30) ? 5 : 4;
            var day = (key > 30) ? key - 30 : key;

            return new DateTime(year, month, day);
        }

        public static void GetEasterDates(DateTime EasterDate, out DateTime EasterStartDate, out DateTime EasterFinalDate)
        {
            EasterStartDate = EasterDate.AddDays(-6);
            EasterFinalDate = EasterDate.AddDays(7);
        }

        public static void GetChristmasDates(int year, out DateTime ChristmasStartDate, out DateTime ChristmasFinalDate)
        {
            ChristmasStartDate = new DateTime(year, 12, 24);
            ChristmasFinalDate = new DateTime(year + 1, 1, 6);
        }

        #endregion


        #region CUSTOM PROTEUS FUNCTIONS

        public static float Max(params float[] values)
        {
            return Enumerable.Max(values);
        }

        public static float Min(params float[] values)
        {
            return Enumerable.Min(values);
        }

        public static int Days360(DateTime initial_date, DateTime final_date)
        {
            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            var y1 = date1.Year;
            var y2 = date2.Year;
            var m1 = date1.Month;
            var m2 = date2.Month;
            var d1 = date1.Day;
            var d2 = date2.Day;

            DateTime tempDate = date1.AddDays(1);
            if (tempDate.Day == 1 && date1.Month == 2)
            {
                d1 = 30;
            }
            if (d2 == 31 && d1 == 30)
            {
                d2 = 30;
            }

            double meres = (y2 - y1) * 360 + (m2 - m1) * 30 + (d2 - d1);
            meres = (meres / 30) * 25;
            meres = Math.Ceiling(meres);

            return Convert.ToInt32(meres);
        }

        public static float CalculateGradeAverage(StudentGradesViewModel data)
        {
            // grade averages produced by this are not valid according to IEK regulation rules
            decimal ratio_proodos = 0.4m;
            decimal ratio_exam = 0.6m;
            decimal gradeProodos = (decimal)data.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ;
            decimal gradeTe = (decimal)data.ΒΑΘΜΟΣ_ΤΕ;
            decimal gradeEp = (decimal)data.ΒΑΘΜΟΣ_ΕΠ;

            decimal grade_mo = Math.Round(ratio_proodos * gradeProodos + ratio_exam * gradeTe, MidpointRounding.AwayFromZero);
            if (gradeEp > 0.0m)
            {
                grade_mo = gradeEp;
            }
            return ((float)grade_mo);
        }

        public static string ValidateGrades(StudentGradesViewModel data)
        {
            string errMsg = "";
            bool predicate1 = data.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ >= 0 && data.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΥ <= 10;
            bool predicate2 = data.ΒΑΘΜΟΣ_ΤΕ >= 0 && data.ΒΑΘΜΟΣ_ΤΕ <= 10;
            bool predicate3 = data.ΒΑΘΜΟΣ_ΕΠ >= 0 && data.ΒΑΘΜΟΣ_ΕΠ <= 10;

            if (!predicate1 || !predicate2 || !predicate3) errMsg = "Οι βαθμοί πρέπει να είναι μεταξύ 0 και 10";
            return (errMsg);
        }

        public static bool CanCreateStudentGrade(int studentId, int tmimaId, int lessonId)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΚΩΔ_ΤΜΗΜΑ == tmimaId && d.ΚΩΔ_ΜΑΘΗΜΑ == lessonId select d).Count();
                if (count > 0) return false;
                else return true;
            }

        }

        public static string ValidateLesson(int lessonId, int ergasiaId)
        {
            string errMsg = "";
            using (var db  = new ProteusDBEntities())
            {
                var data = (from d in db.LESSONS_IEK where d.LESSON_ID == lessonId select d).FirstOrDefault();
                int lessonType = (int)data.LESSON_TYPE;

                if (lessonType == 1 && ergasiaId == 2)
                {
                    errMsg = "Το μάθημα είναι θεωρία και έχει καταχωρηθεί ως εργαστήριο!";
                }
                else if (lessonType == 2 && ergasiaId == 1)
                {
                    errMsg = "Το μάθημα είναι εργαστήριο και έχει καταχωρηθεί ως θεωρία!";
                }
                return errMsg;
            }
        }

        public static bool ProgrammaItemExists(ProgrammaDayViewModel item)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                             where d.ΚΩΔ_ΤΜΗΜΑ == item.ΚΩΔ_ΤΜΗΜΑ && d.ΗΜΕΡΟΜΗΝΙΑ == item.ΗΜΕΡΟΜΗΝΙΑ && d.ΩΡΑ == item.ΩΡΑ
                             select d).Count();

                if (count > 0) return true;
                else return false;
            }
        }

        public static bool ProgrammaTmimaExists(int tmimaId)
        {
            bool value = false;

            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.ΠΡΟΓΡΑΜΜΑ_ΗΜΕΡΑ
                             where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId
                             orderby d.ΕΒΔΟΜΑΔΑ descending, d.ΗΜΕΡΟΜΗΝΙΑ ascending
                             select d).Count();

                if (count > 0) value = true;
                return (value);
            }
        }

        public static int GetEidikotitaHours(int eidikotitaId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.sqlΕΙΔΙΚΟΤΗΤΕΣ_ΙΕΚ_ΩΡΕΣ where d.EIDIKOTITA_ID == eidikotitaId select d).FirstOrDefault();

                int Hours = data.ΩΡΕΣ ?? 0;
                return (Hours);
            }
        }

        public static int GetHoursTheory(StudentBekViewModel bek)
        {
            int termsNumber = bek.ΕΞΑΜΗΝΑ ?? 0;
            int hoursTheory = 0;
            int hoursDone = 0;
            int apousies = 0;

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.sqlSTUDENTS_BEK_APOUSIES where d.STUDENT_ID == bek.ΜΑΘΗΤΗΣ_ΚΩΔ select d).ToList();
                if (data.Count() > 0)
                {
                    apousies = (from d in db.sqlSTUDENTS_BEK_APOUSIES where d.STUDENT_ID == bek.ΜΑΘΗΤΗΣ_ΚΩΔ select d).FirstOrDefault().APOUSIES;
                }
                if (termsNumber >= 4 || termsNumber == 0) // default value if termsNumber = 0
                {
                    hoursTheory = (from d in db.sqlΕΙΔΙΚΟΤΗΤΕΣ_ΙΕΚ_ΩΡΕΣ where d.EIDIKOTITA_ID == bek.ΕΙΔΙΚΟΤΗΤΑ select d).FirstOrDefault().ΩΡΕΣ ?? 0;
                }
                else
                {
                    hoursTheory = (from d in db.sqlΕΙΔΙΚΟΤΗΤΕΣ_ΙΕΚ_ΩΡΕΣ_ΓΔ where d.EIDIKOTITA_ID == bek.ΕΙΔΙΚΟΤΗΤΑ select d).FirstOrDefault().ΩΡΕΣ ?? 0;

                }
                hoursDone = hoursTheory - apousies;
                return (hoursDone);
            }
        }

        public static int GetHoursPraktiki(StudentBekViewModel bek)
        {
            int hours = 0;

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.sqlΠΡΑΚΤΙΚΗ_ΜΑΘΗΤΗΣ_ΩΡΕΣ where d.STUDENT_ID == bek.ΜΑΘΗΤΗΣ_ΚΩΔ select d).ToList();
                if (data.Count() == 0) return (hours);

                hours = (from d in db.sqlΠΡΑΚΤΙΚΗ_ΜΑΘΗΤΗΣ_ΩΡΕΣ where d.STUDENT_ID == bek.ΜΑΘΗΤΗΣ_ΚΩΔ select d).FirstOrDefault().ΣΥΝΟΛΟ_ΩΡΕΣ ?? 0;
                return (hours);
            }
        }

        public static sqlTEACHER_INFO GetTeacherInfo(int id)
        {
            using (var db = new ProteusDBEntities())
            {
                var teacher = (from d in db.sqlTEACHER_INFO where d.TEACHER_ID == id select d).FirstOrDefault();

                return (teacher);
            }
        }

        // Used by Ergasies Grades (New 29-11-2022)
        public static bool CanCreateErgasiaGrade(int studentId, int tmimaId, string lessonText)
        {
            using (var db = new ProteusDBEntities())
            {
                int count = (from d in db.STUDENT_ERGASIES where d.STUDENT_ID == studentId && d.TMIMA_ID == tmimaId && d.LESSON_TEXT == lessonText select d).Count();
                if (count > 0) return false;
                else return true;
            }

        }

        // Used by Ergasies Grades (New 10-12-2022)
        public static string GetLessonNameFromRowId(int rowId)
        {
            string lesson_name = null;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.qryIEK_EIDIKOTITA_LESSON_TEXT where d.ROW_ID == rowId select d).FirstOrDefault();
                if (data != null)
                    lesson_name = data.LESSON_TEXT;

                return lesson_name;
            }
        }

        // Used by Ergasies Grades (New 29-11-2022)
        public static int GetEidikotitaFromTmima(int tmimaId)
        {
            int eidikotitaID = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
                if (data != null)
                {
                    eidikotitaID = (int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
                }
                return eidikotitaID;
            }
        }

        // Used by Ergasies Grades (New 29-11-2022)
        public static int GetTermFromTmima(int tmimaId)
        {
            int termID = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
                if (data != null)
                {
                    termID = (int)data.ΕΞΑΜΗΝΟ;
                }
                return termID;
            }
        }

        // Used by Ergasies Grades (New 29-11-2022)
        public static string ValidateErgasiaGrade(ErgasiaGradeViewModel data)
        {
            string errMsg = "";

            if (data.GRADE < 0 || data.GRADE > 10) errMsg = "Οι βαθμοί πρέπει να είναι μεταξύ 0 και 10";
            return (errMsg);
        }


        #endregion


        #region Protocol Generator

        public static string Get8Digits()
        {
            //var bytes = new byte[4];
            //var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(bytes);
            //uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            //return String.Format("{0:D8}", random);

            // try this
            Random rnd = new Random();
            int random = rnd.Next(1, 100000);
            return string.Format("{0:00000000}", random);
        }

        public static string Get6Digits()
        {
            int newId = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ orderby d.ΑΙΤΗΣΗ_ΚΩΔ descending select d).FirstOrDefault();
                if (data == null)
                    newId += 1;
                else
                    newId = data.ΑΙΤΗΣΗ_ΚΩΔ + 1;

                return string.Format("{0:000000}", newId);
            }
        }

        public static string GenerateProtocol()
        {
            DateTime date1 = DateTime.Now;
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);

            string protocol = Get6Digits() + "/" + stDate;
            return protocol;
        }

        public static string GeneratePassword(Random rnd)
        {
            int random = rnd.Next(1, 100000);
            return string.Format("{0:00000}", random);
        }

        #endregion


        #region Age calculators

        // Age calculator
        public static int CalculateAge(ΜΑΘΗΤΕΣ student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        public static int CalculateAge(StudentViewModel student)
        {
            DateTime BirthDate;
            DateTime RegDate = ((DateTime)student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ).Date;
            int age = 0;

            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                BirthDate = ((DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
                age = YearsDiff(BirthDate, RegDate);
            }
            return age;
        }

        public static int XmCalculateApofitisi(XmAitisiViewModel data)
        {
            DateTime aDate;
            DateTime RefDate = DateTime.Today;

            if (data.ΗΜΕΡΟΜΗΝΙΑ != null) RefDate = ((DateTime)data.ΗΜΕΡΟΜΗΝΙΑ).Date;

            int years = 0;

            if (data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ != null)
            {
                aDate = ((DateTime)data.ΗΜΝΙΑ_ΑΠΟΦΟΙΤΗΣΗ).Date;
                years = YearsDiff(aDate, RefDate);
            }
            return years;
        }

        public static int CalculateTeacherAge(ΕΚΠΑΙΔΕΥΤΕΣ teacher)
        {
            DateTime BirthDate = ((DateTime)teacher.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
            int age = 0;

            if (teacher.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null)
            {
                DateTime currentDate = DateTime.Now.Date;
                age = YearsDiff(BirthDate, currentDate);
                return age;
            }   
            DateTime HireDate = ((DateTime)teacher.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ).Date;
            if (teacher.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                age = YearsDiff(BirthDate, HireDate);
            }
            return age;
        }

        public static int CalculateTeacherAge(TeacherViewModel teacher)
        {
            DateTime BirthDate = ((DateTime)teacher.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ).Date;
            int age = 0;

            if (teacher.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null)
            {
                DateTime currentDate = DateTime.Now.Date;
                age = YearsDiff(BirthDate, currentDate);
                return age;
            }
            DateTime HireDate = ((DateTime)teacher.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ).Date;
            if (teacher.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ != null)
            {
                age = YearsDiff(BirthDate, HireDate);
            }
            return age;
        }

        #endregion


        #region AFM validation

        /// ------------------------------------------------------------------------
        /// CheckAFM: Ελέγχει αν ένα ΑΦΜ είναι σωστό
        /// Το ΑΦΜ που θα ελέγξουμε
        /// true = ΑΦΜ σωστό, false = ΑΦΜ Λάθος
        /// Αυτή είναι η χρησιμοποιούμενη μεθοδος.
        /// Προσθήκη: Αποκλεισμός όταν όλα τα ψηφία = 0 (ο αλγόριθμος τα δέχεται!)
        /// Ημ/νια: 12/3/2013
        /// ------------------------------------------------------------------------
        public static bool CheckAFM(string cAfm)
        {
            int nExp = 1;
            // Ελεγχος αν περιλαμβάνει μόνο γράμματα
            try { long nAfm = Convert.ToInt64(cAfm); }

            catch (Exception) { return false; }

            // Ελεγχος μήκους ΑΦΜ
            if (string.IsNullOrWhiteSpace(cAfm))
            {
                return false;
            }

            cAfm = cAfm.Trim();
            int nL = cAfm.Length;
            if (nL != 9) return false;

            // Έλεγχος αν όλα τα ψηφία είναι 0
            var count = cAfm.Count(x => x == '0');
            if (count == cAfm.Length) return false;

            //Υπολογισμός αν το ΑΦΜ είναι σωστό

            int nSum = 0;
            int xDigit = 0;
            int nT = 0;

            for (int i = nL - 2; i >= 0; i--)
            {
                xDigit = int.Parse(cAfm.Substring(i, 1));
                nT = xDigit * (int)(Math.Pow(2, nExp));
                nSum += nT;
                nExp++;
            }

            xDigit = int.Parse(cAfm.Substring(nL - 1, 1));

            nT = nSum / 11;
            int k = nT * 11;
            k = nSum - k;
            if (k == 10) k = 0;
            if (xDigit != k) return false;

            return true;

        }

        #endregion


        #region Validations

        public static bool ValidateBirthdate(StudentViewModel student)
        {
            if (student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ == null) return false;

            DateTime _birthdate = (DateTime)student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;

            string refDate = "01/01/" + _birthdate.Year.ToString();

            DateTime studentDate;
            studentDate = DateTime.ParseExact(refDate, "dd/MM/yyyy", null);
            if (!ValidBirthDate(studentDate)) return false;
            else return true;
        }

        public static bool ValidBirthDate(DateTime birthdate)
        {
            int maxAge = 90;
            int minAge = 18;

            DateTime minDate = DateTime.Today.Date.AddYears(-maxAge);
            DateTime maxDate = DateTime.Today.Date.AddYears(-minAge);

            bool result;
            if (birthdate >= minDate && birthdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }

        public static bool ValidateRegdate(ΜΑΘΗΤΕΣ student)
        {
            if (student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ == null) return false;

            DateTime _regdate = (DateTime)student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ;

            if (!ValidRegDate(_regdate)) return false;
            else return true;
        }

        public static bool ValidateRegdate(StudentViewModel student)
        {
            if (student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ == null) return false;

            DateTime _regdate = (DateTime)student.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΗΜΝΙΑ;

            if (!ValidRegDate(_regdate)) return false;
            else return true;
        }

        public static bool ValidRegDate(DateTime regdate)
        {
            int yearsbefore = 15;
            int yearsafter = 15;

            DateTime minDate = DateTime.Today.Date.AddYears(-yearsbefore);
            DateTime maxDate = DateTime.Today.Date.AddYears(yearsafter);

            bool result;
            if (regdate >= minDate && regdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }

        public static string ValidateStudentFields(StudentViewModel s)
        {
            string errMsg = "";

            if (!ValidateBirthdate(s)) 
                errMsg += "-> Η ηλικία είναι εκτός λογικών ορίων. ";

            if (!CheckAFM(s.ΑΦΜ)) 
                errMsg += "-> Το ΑΦΜ δεν είναι έγκυρο. ";

            if (!ValidateRegdate(s)) 
                errMsg += "-> Η ημ/νία πράξης εισόδου είναι εκτός λογικών ορίων. ";

            return (errMsg);
        }

        public static string ValidateTeacherFields(TeacherViewModel entity)
        {
            string errMsg = "";

            if (!ValidBirthDate((DateTime)entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ)) 
                errMsg += "-> Η ημ/νία γέννησης είναι εκτός λογικών ορίων. ";

            if (!CheckAFM(entity.ΑΦΜ)) 
                errMsg += "-> Το ΑΦΜ δεν είναι έγκυρο. ";

            if (entity.ΤΕΚΝΑ < 0 || entity.ΤΕΚΝΑ > 20)
                errMsg += "Ο αριθμός τέκνων είναι εκτός λογικών ορίων.";

            return errMsg;
        }

        public static bool ValidFoitisiPraktiki(int tmimaId, int foitisiId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΤΜΗΜΑ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
                if (data != null)
                {
                    if (data.ΕΞΑΜΗΝΟ != 5 && (foitisiId == 4 || foitisiId == 5 || foitisiId == 8)) return false;
                    else return true;
                }
                return true;
            }
        }


        #endregion


        #region Primary key getters

        public static int GetStudentSchool(int studentId)
        {
            int schoolId = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΜΑΘΗΤΕΣ where d.STUDENT_ID == studentId select new { d.ΙΕΚ }).FirstOrDefault();
                if (data != null)
                    schoolId = data.ΙΕΚ;

                return schoolId;
            }
        }

        public static int GetTeacherSchool(int teacherId)
        {
            int schoolId = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ where d.TEACHER_ID == teacherId select new { d.ΙΕΚ }).FirstOrDefault();
                if (data != null)
                    schoolId = data.ΙΕΚ;

                return schoolId;
            }
        }

        public static int GetErgodotisSchool(int ergodotisId)
        {
            int school = 0;
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΕΡΓΟΔΟΤΕΣ where d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ == ergodotisId select d).FirstOrDefault();
                if (data != null) school = (int)data.ΙΕΚ;

                return school;
            }
        }

        public static int GetStudentID(int amk, int iek)
        {
            int studentId = 0;

            using (var db = new ProteusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΕΣ
                               where d.ΑΜΚ == amk && d.ΙΕΚ == iek
                               select d).FirstOrDefault();
                if (student != null)
                {
                    studentId = student.STUDENT_ID;
                }
                return (studentId);
            }
        }

        public static int GetStudentAmk(int studentId)
        {
            int studentAmk = 0;
            using (var db = new ProteusDBEntities())
            {
                var student = (from d in db.ΜΑΘΗΤΕΣ
                               where d.STUDENT_ID == studentId
                               select d).FirstOrDefault();

                if (student != null)
                {
                    studentAmk = student.ΑΜΚ;
                }
                return (studentAmk);
            }
        }

        public static int GetTeacherID(string afm, int iek)
        {
            int teacherId = 0;
            using (var db = new ProteusDBEntities())
            {
                var teacher = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ
                               where d.ΑΦΜ == afm && d.ΙΕΚ == iek
                               select d).FirstOrDefault();

                if (teacher != null)
                {
                    teacherId = teacher.TEACHER_ID;
                }
                return (teacherId);
            }
        }

        public static string GetTeacherAfm(int teacherId)
        {
            string teacherAfm = "";
            using (var db = new ProteusDBEntities())
            {
                var teacher = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ
                               where d.TEACHER_ID == teacherId
                               select d).FirstOrDefault();

                if (teacher != null)
                {
                    teacherAfm = teacher.ΑΦΜ;
                }
                return (teacherAfm);
            }
        }

        public static int GetErgodotisID(string afm, int iek, string name = null)
        {
            int ergodotisId = 0;

            using (var db = new ProteusDBEntities())
            {
                ΕΡΓΟΔΟΤΕΣ ergodotis = new ΕΡΓΟΔΟΤΕΣ();

                if (string.IsNullOrEmpty(afm))
                {
                    ergodotis = (from d in db.ΕΡΓΟΔΟΤΕΣ
                                 where d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ == name && d.ΙΕΚ == iek
                                 select d).FirstOrDefault();

                }
                else
                {
                    ergodotis = (from d in db.ΕΡΓΟΔΟΤΕΣ
                                 where d.ΕΡΓΟΔΟΤΗΣ_ΑΦΜ == afm && d.ΙΕΚ == iek
                                 select d).FirstOrDefault();
                }

                if (ergodotis != null)
                {
                    ergodotisId = ergodotis.ΕΡΓΟΔΟΤΗΣ_ΚΩΔ;
                }
                return (ergodotisId);
            }
        }

        #endregion


        #region Converters

        public static int HoursToMonths(int kladosId, int hours)
        {
            using (var db = new ProteusDBEntities())
            {
                var klados = (from d in db.SYS_KLADOS where d.KLADOS_ID == kladosId select d).FirstOrDefault();

                double weekly_hours = (double)klados.KLADOS_HOURS;
                double factor = (weekly_hours * 25.0) / 6.0;

                int months = (int)(hours / factor);

                return (months);
            }
        }

        public static int HoursToMonths(int kladosId, int hours, ProteusDBEntities db)
        {
            var klados = (from d in db.SYS_KLADOS where d.KLADOS_ID == kladosId select d).FirstOrDefault();

            double weekly_hours = (double)klados.KLADOS_HOURS;
            double factor = (weekly_hours * 25.0) / 6.0;

            int months = (int)(hours / factor);

            return (months);
        }

        public static int HoursToDays(int kladosId, int hours)
        {
            using (var db = new ProteusDBEntities())
            {
                var klados = (from d in db.SYS_KLADOS where d.KLADOS_ID == kladosId select d).FirstOrDefault();

                double weekly_hours = (double)klados.KLADOS_HOURS;
                double factor = (weekly_hours * 25.0) / 6.0;

                double months_dec = hours / factor;
                int months = (int)(hours / factor);

                int days = (int)Math.Round((months_dec - months) * 25.0, 0);

                return (days);
            }
        }

        public static string DecimalToFractional(int NumLessons, int gradeSum)
        {
            double grade_decimal = GradeDecimal(NumLessons, gradeSum);

            int decPart = (int)((grade_decimal -Math.Floor(grade_decimal)) * 100.0);

            int nominator = (int)Math.Round((double)(decPart * NumLessons / 100.0), 0);

            string OutGrade = Math.Floor(grade_decimal).ToString() + " " + nominator.ToString() + "/" + NumLessons.ToString();

            return (OutGrade);
        }

        public static double GradeDecimal(int NumLessons, int gradeSum)
        {
            double grade_decimal = (double)gradeSum / (double)NumLessons;

            return (grade_decimal);
        }

        public static string NominatorWord(int number)
        {
            string s;

            if (number == 0) s = "ΜΗΔΕΝ";
            else if (number == 1) s = "ENA";
            else if (number == 2) s = "ΔΥΟ";
            else if (number == 3) s = "ΤΡΙΑ";
            else if (number == 4) s = "ΤΕΣΣΕΡΑ";
            else if (number == 5) s = "ΠΕΝΤΕ";
            else if (number == 6) s = "ΕΞΙ";
            else if (number == 7) s = "ΕΠΤΑ";
            else if (number == 8) s = "ΟΚΤΩ";
            else if (number == 9) s = "ΕΝΝΕΑ";
            else if (number == 10) s = "ΔΕΚΑ";
            else if (number == 11) s = "ENTEKA";
            else if (number == 12) s = "ΔΩΔΕΚΑ";
            else if (number == 13) s = "ΔΕΚΑΤΡΙΑ";
            else if (number == 14) s = "ΔΕΚΑΤΕΣΣΕΡΑ";
            else if (number == 15) s = "ΔΕΚΑΠΕΝΤΕ";
            else if (number == 16) s = "ΔΕΚΑΕΞΙ";
            else if (number == 17) s = "ΔΕΚΑΕΠΤΑ";
            else if (number == 18) s = "ΔΕΚΑΟΚΤΩ";
            else if (number == 19) s = "ΔΕΚΑΕΝΝΕΑ";
            else if (number == 20) s = "ΕΙΚΟΣΙ";
            else if (number == 21) s = "ΕΙΚΟΣΙΕΝΑ";
            else if (number == 22) s = "ΕΙΚΟΣΙΔΥΟ";
            else if (number == 23) s = "ΕΙΚΟΣΙΤΡΙΑ";
            else if (number == 24) s = "ΕΙΚΟΣΙΤΕΣΣΕΡΑ";
            else if (number == 25) s = "ΕΙΚΟΣΙΠΕΝΤΕ";
            else if (number == 26) s = "ΕΙΚΟΣΙΕΞΙ";
            else if (number == 27) s = "ΕΙΚΟΣΙΕΠΤΑ";
            else if (number == 28) s = "ΕΙΚΟΣΙΟΚΤΩ";
            else if (number == 29) s = "ΕΙΚΟΣΙΕΝΝΕΑ";
            else if (number == 30) s = "ΤΡΙΑΝΤΑ";
            else s = "";

            return (s);
        }

        public static string DenominatorWord(int number)
        {
            string s;

            if (number == 0) s = "ΜΗΔΕΝ";
            else if (number == 1) s = "ΠΡΩΤΑ";
            else if (number == 2) s = "ΔΕΥΤΕΡΑ";
            else if (number == 3) s = "ΤΡΙΤΑ";
            else if (number == 4) s = "ΤΕΤΑΡΤΑ";
            else if (number == 5) s = "ΠΕΜΠΤΑ";
            else if (number == 6) s = "ΕΚΤΑ";
            else if (number == 7) s = "ΕΒΔΟΜΑ";
            else if (number == 8) s = "ΟΓΔΟΑ";
            else if (number == 9) s = "ΕΝΝΑΤΑ";
            else if (number == 10) s = "ΔΕΚΑΤΑ";
            else if (number == 11) s = "ΕΝΤΕΚΑΤΑ";
            else if (number == 12) s = "ΔΩΔΕΚΑΤΑ";
            else if (number == 13) s = "ΔΕΚΑΤΑ ΤΡΙΤΑ";
            else if (number == 14) s = "ΔΕΚΑΤΑ ΤΕΤΑΡΤΑ";
            else if (number == 15) s = "ΔΕΚΑΤΑ ΠΕΜΠΤΑ";
            else if (number == 16) s = "ΔΕΚΑΤΑ ΕΚΤΑ";
            else if (number == 17) s = "ΔΕΚΑΤΑ ΕΒΔΟΜΑ";
            else if (number == 18) s = "ΔΕΚΑΤΑ ΟΓΔΟΑ";
            else if (number == 19) s = "ΔΕΚΑΤΑ ΕΝΑΤΑ";
            else if (number == 20) s = "ΕΙΚΟΣΤΑ";
            else if (number == 21) s = "ΕΙΚΟΣΤΑ ΠΡΩΤΑ";
            else if (number == 22) s = "ΕΙΚΟΣΤΑ ΔΕΥΤΕΡΑ";
            else if (number == 23) s = "ΕΙΚΟΣΤΑ ΤΡΙΤΑ";
            else if (number == 24) s = "ΕΙΚΟΣΤΑ ΤΕΤΑΡΤΑ";
            else if (number == 25) s = "ΕΙΚΟΣΤΑ ΠΕΜΠΤΑ";
            else if (number == 26) s = "ΕΙΚΟΣΤΑ ΕΚΤΑ";
            else if (number == 27) s = "ΕΙΚΟΣΤΑ ΕΒΔΟΜΑ";
            else if (number == 28) s = "ΕΙΚΟΣΤΑ ΟΓΔΟΑ";
            else if (number == 29) s = "ΕΙΚΟΣΤΑ ΕΝΑΤΑ";
            else if (number == 30) s = "ΤΡΙΑΚΟΣΤΑ";
            else s = "";

            return (s);
        }

        public static string textGradeParse(string grade)
        {
            string result;
            string con1 = " & ";
            string con2 = "/";

            int posSpace = grade.IndexOf(" ");
            int posSlash = grade.IndexOf("/");

            string intGrade = grade.Substring(0, posSpace);
            string strFraction = grade.Substring(posSpace + 1);

            int endIndex = strFraction.Length;
            int startIndex = strFraction.IndexOf("/");
            int DenomLen = strFraction.Substring(startIndex + 1).Length;
            int NomLen = strFraction.Substring(0, startIndex).Length;
            int sLen = endIndex - startIndex;

            string intNom = strFraction.Substring(0, NomLen);
            string intDenom = strFraction.Substring(startIndex + 1);

            int wholeNum = Int32.Parse(intGrade);
            int fractionNom = Int32.Parse(intNom);
            int fractionDenom = Int32.Parse(intDenom);

            result = NominatorWord(wholeNum) + con1 + NominatorWord(fractionNom) + con2 + DenominatorWord(fractionDenom);
            return (result);
        }

        #endregion


        #region ΜΟΡΙΟΔΟΤΗΣΗ ΥΠΟΨΗΦΙΩΝ

        public static string GetSchoolYearText(int syearId)
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.SYS_SCHOOLYEARS
                            where d.SY_ID == syearId
                            select d).FirstOrDefault();

                string syearText = data.SY_TEXT;
                return (syearText);
            }
        }

        public static XmEgykliosViewModel GetAdminEgyklios()
        {
            using (var db = new ProteusDBEntities())
            {
                XmEgykliosViewModel data = new XmEgykliosViewModel();

                data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ
                        where d.ΔΙΑΧΕΙΡΙΣΗ == true
                        select new XmEgykliosViewModel
                        {
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΑΠ = d.ΕΓΚΥΚΛΙΟΣ_ΑΠ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ ?? 0,
                            ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                            ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ,
                            ΚΑΤΑΣΤΑΣΗ = d.ΚΑΤΑΣΤΑΣΗ ?? 0,
                            ΕΝΕΡΓΗ = d.ΕΝΕΡΓΗ ?? false,
                            ΔΙΑΧΕΙΡΙΣΗ = d.ΔΙΑΧΕΙΡΙΣΗ ?? false
                        }).FirstOrDefault();
                return (data);
            }
        }

        public static XmEgykliosViewModel GetActiveEgyklios()
        {
            using (var db = new ProteusDBEntities())
            {
                XmEgykliosViewModel data = new XmEgykliosViewModel();

                data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ
                        where d.ΕΝΕΡΓΗ == true
                        select new XmEgykliosViewModel
                        {
                            ΕΓΚΥΚΛΙΟΣ_ΚΩΔ = d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ,
                            ΕΓΚΥΚΛΙΟΣ_ΑΠ = d.ΕΓΚΥΚΛΙΟΣ_ΑΠ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ ?? 0,
                            ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                            ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ,
                            ΚΑΤΑΣΤΑΣΗ = d.ΚΑΤΑΣΤΑΣΗ ?? 0,
                            ΕΝΕΡΓΗ = d.ΕΝΕΡΓΗ ?? false,
                            ΔΙΑΧΕΙΡΙΣΗ = d.ΔΙΑΧΕΙΡΙΣΗ ?? false
                        }).FirstOrDefault();
                return (data);
            }
        }

        public static int GetOpenEgykliosID()
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ where d.ΚΑΤΑΣΤΑΣΗ == 1 select d).FirstOrDefault();
                if (data == null)
                    return 0;
                else
                    return data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ;
            }
        }

        public static int GetActiveEgykliosID()
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ where d.ΕΝΕΡΓΗ == true select d).FirstOrDefault();
                if (data == null)
                    return 0;
                else
                    return data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ;
            }
        }

        public static int GetAdminEgykliosID()
        {
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ where d.ΔΙΑΧΕΙΡΙΣΗ == true select d).FirstOrDefault();
                if (data == null)
                    return 0;
                else
                    return data.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ;
            }
        }

        public static string GenerateProtocol(DateTime date1)
        {
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);

            string protocol = Get6Digits() + "/" + stDate;
            return protocol;
        }

        #endregion


        #region ΜΕΤΑΦΟΡΤΩΣΗ ΑΡΧΕΙΩΝ

        public static bool VerifyUploadIntegrity(int egykliosId, USER_STUDENTS loggedStudent)
        {
            string studentAfm = loggedStudent.USER_AFM;

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_UPLOADS where d.EGYKLIOS_ID == egykliosId && d.STUDENT_AFM == studentAfm select d).FirstOrDefault();
                if (data == null)
                    return true;

                if (!UploadFilesExist(data.UPLOAD_ID))
                    return true;

                string username = GetUserSchoolFromSchoolId((int)data.SCHOOL_ID);

                var files = (from d in db.ΧΜ_UPLOADS_FILES where d.UPLOAD_ID == data.UPLOAD_ID && d.SCHOOL_USER == username select d).ToList();
                if (files.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool UploadFilesExist(int uploadId)
        {
            using (var db = new ProteusDBEntities())
            {
                int countFiles = (from d in db.ΧΜ_UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();
                if (countFiles > 0)
                    return true;
                return false;
            }
        }

        public static Tuple<int, int, int> GetUploadInfo(int uploadId)
        {
            int school_id = 0;
            int egyklios_id = 0;
            int aitisi_id = 0;

            using (var db = new ProteusDBEntities())
            {
                var upload = (from d in db.ΧΜ_UPLOADS where d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (upload != null)
                {
                    school_id = (int)upload.SCHOOL_ID;
                    egyklios_id = (int)upload.EGYKLIOS_ID;
                    aitisi_id = (int)upload.AITISI_ID;
                }

                var data = Tuple.Create(school_id, egyklios_id, aitisi_id);
                return (data);
            }
        }

        public static string GetUserSchoolFromAitisi(int aitisiId)
        {
            using (var db = new ProteusDBEntities())
            {
                int schoolId = (int)db.ΧΜ_ΥΠΟΨΗΦΙΟΣ.Find(aitisiId).ΙΕΚ1;

                var data = (from d in db.USER_SCHOOLS where d.USER_SCHOOLID == schoolId select d).FirstOrDefault();
                if (data != null)
                {
                    return data.USERNAME;
                }
                else
                {
                    return "iek.demo";
                }
            }
        }

        public static string GetUserSchoolFromSchoolId(int schoolId)
        {
            string username = "";
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.USER_SCHOOLS where d.USER_SCHOOLID == schoolId select d).FirstOrDefault();
                if (data != null)
                {
                    username = data.USERNAME;
                }
                return (username);
            }
        }

        public static string GetStudentNameFromUser(string AFM)
        {
            string fullname = "X";
            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΑΦΜ == AFM select d).FirstOrDefault();
                if (data != null)
                    fullname = data.ΕΠΩΝΥΜΟ + " " + data.ΟΝΟΜΑ;

                return fullname;
            }
        }

        public static string GetFileGuidFromName(string filename, int uploadId)
        {
            string file_id = "";
            using (var db = new ProteusDBEntities())
            {
                var fileData = (from d in db.ΧΜ_UPLOADS_FILES where d.FILENAME == filename && d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (fileData != null) file_id = fileData.ID;

                return (file_id);
            }
        }

        public static int GetAitisiIDFromAFM(string studentAfm)
        {
            int egykliosId = GetOpenEgykliosID();

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΑΦΜ == studentAfm select d).FirstOrDefault();
                if (data != null)
                    return data.ΑΙΤΗΣΗ_ΚΩΔ;
                return 0;
            }
        }

        public static int GetSchoolIDFromAFM(string studentAfm)
        {
            int egykliosId = GetOpenEgykliosID();

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.ΧΜ_ΥΠΟΨΗΦΙΟΣ where d.ΕΓΚΥΚΛΙΟΣ_ΚΩΔ == egykliosId && d.ΑΦΜ == studentAfm select d).FirstOrDefault();
                if (data != null)
                    return (int)data.ΙΕΚ1;
                return 0;
            }
        }

        #endregion

        public static int GetKladosFromEidikotita(int eidikotita)
        {
            int klados = 0;

            using (var db = new ProteusDBEntities())
            {
                var data = (from d in db.SYS_EIDIKOTITES where d.EIDIKOTITA_ID == eidikotita select d).FirstOrDefault();
                if (data != null) klados = (int)data.EIDIKOTITA_KLADOS_ID;

                return klados;
            }
        }

        public static USER_SCHOOLS GetLoginSchool()
        {
            using (var db = new ProteusDBEntities())
            {
                USER_SCHOOLS loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

                return loggedSchool;
            }
        }

    }   // class Common


    public static class HumanFriendlyInteger
    {
        static string[] ones = new string[] { "", "ΜΙΑ", "ΔΥΟ", "ΤΡΕΙΣ", "ΤΕΣΣΕΡΙΣ", "ΠΕΝΤΕ", "ΕΞΙ", "ΕΠΤΑ", "ΟΚΤΩ", "ΕΝΝΕΑ" };
        static string[] teens = new string[] { "ΔΕΚΑ", "ΕΝΤΕΚΑ", "ΔΩΔΕΚΑ", "ΔΕΚΑΤΡΕΙΣ", "ΔΕΚΑΤΕΣΣΕΡΙΣ", "ΔΕΚΑΠΕΝΤΕ", "ΔΕΚΑΕΞΙ", "ΔΕΚΑΕΠΤΑ", "ΔΕΚΑΟΚΤΩ", "ΔΕΚΑΕΝΝΕΑ" };
        static string[] tens = new string[] { "ΕΙΚΟΣΙ", "ΤΡΙΑΝΤΑ", "ΣΑΡΑΝΤΑ", "ΠΕΝΗΝΤΑ", "ΕΞΗΝΤΑ", "ΕΒΔΟΜΗΝΤΑ", "ΟΓΔΟΝΤΑ", "ΕΝΕΝΗΝΤΑ" };
        static string[] hundreds = new string[] {"ΕΚΑΤΟ", "ΕΚΑΤΟΝ ", "ΔΙΑΚΟΣΙΕΣ ", "ΤΡΙΑΚΟΣΙΕΣ ", "ΤΕΤΡΑΚΟΣΙΕΣ ", "ΠΕΝΤΑΚΟΣΙΕΣ ", "ΕΞΑΚΟΣΙΕΣ ", "ΕΠΤΑΚΟΣΙΕΣ ", "ΟΚΤΑΚΟΣΙΕΣ ", "ΕΝΝΙΑΚΟΣΙΕΣ ", "ΧΙΛΙΕΣ " };
        static string[] thousandsGroups = { "", " ΧΙΛΙΑΔΕΣ", " ΕΚΑΤΟΜΜΥΡΙΑ", " ΔΙΣΕΚΑΤΟΜΜΥΡΙΑ" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n > 99999) return "*** ΑΝΩΤΑΤΟ ΟΡΙΟ ΥΠΟΛΟΓΙΣΜΩΝ ***";

            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                if (n == 100)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100 - 1] + ones[(n % 100)]), 0);
                    return friendlyInt;
                }

                if (n > 100 && n <= 109 || n >= 200 && n <= 209 || n >= 300 && n <= 309 || n >= 400 && n <= 409 || n >= 500 && n <= 509 ||
                    n >= 600 && n <= 609 || n >= 700 && n <= 709 || n >= 800 && n <= 809 || n >= 900 && n <= 909) 
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + ones[(n % 100)]), 0);
                    return friendlyInt;
                }
                if (n > 109 && n <= 119 || n > 209 && n <= 219 || n > 309 && n <= 319 || n > 409 && n <= 419 || n > 509 && n <= 519 ||
                    n > 609 && n <= 619 || n > 709 && n <= 719 || n > 809 && n <= 819 || n > 909 && n <= 919)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + teens[(n % 100) % 10]), 0);
                    return friendlyInt;
                }
                if (n > 119 && n <= 199 || n > 219 && n <= 299 || n > 319 && n <= 399 || n > 419 && n <= 499 || n > 519 && n <= 599 ||
                    n > 619 && n <= 699 || n > 719 && n <= 799 || n > 819 && n <= 899 || n > 919 && n <= 999)
                {
                    friendlyInt += FriendlyInteger(0, (hundreds[n / 100] + tens[(n % 100) / 10 - 2] + " " + ones[(n % 100) % 10]), 0);
                    return friendlyInt;
                }
            }
            else if (n >= 1000 && n < 2000)
            {
                friendlyInt += FriendlyInteger(n-1000, "ΧΙΛΙΕΣ", 0);
                return friendlyInt;
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "ΜΗΔΕΝ";
            }
            else if (n < 0)
            {
                return "ΜΕΙΟΝ " + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }
    }

    // View Engine extension of Proteus
    public class ProteusViewEngine : RazorViewEngine
    {
        public ProteusViewEngine()
        {
            string[] locations = new string[] 
            {  
                "~/Views/{1}/{0}.cshtml",
                "~/Views/School/Archive/{0}.cshtml",
                "~/Views/School/Ergodotes/{0}.cshtml",
                "~/Views/School/Moria/{0}.cshtml",
                "~/Views/School/Teachers/{0}.cshtml",
                "~/Views/School/Students/{0}.cshtml",
                "~/Views/School/Setup/{0}.cshtml",
                "~/Views/School/Programma/{0}.cshtml",
                "~/Views/School/Statistics/{0}.cshtml",

                "~/Views/Admin/{1}/{0}.cshtml",
                "~/Views/Admin/Schools/{0}.cshtml",
                "~/Views/Admin/Ergodotes/{0}.cshtml",
                "~/Views/Admin/Programma/{0}.cshtml",
                "~/Views/Admin/Teachers/{0}.cshtml",
                "~/Views/Admin/Students/{0}.cshtml",
                "~/Views/Admin/Setup/{0}.cshtml",
                "~/Views/Admin/Moria/{0}.cshtml",
                "~/Views/Admin/Statistics/{0}.cshtml",

                "~/Views/Shared/PartialViews/{0}.cshtml",
                "~/Views/Shared/EditorTemplates/{0}.cshtml",
                "~/Views/Shared/Layouts/{0}.cshtml"
            };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;
        }
    }

}   // namespace