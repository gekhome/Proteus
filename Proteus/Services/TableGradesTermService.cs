using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TableGradesTermService : ITableGradesTermService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TableGradesTermService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public string Create(int tmimaId)
        {
            string msg = "Η δημιουργία του πίνακα βαθμολογιών του τμήματος ολοκληρώθηκε.";

            IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> source = (from d in entities.asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.LESSON_TEXT, d.ΘΕ select d).ToList();
            if (source.Count() == 0)
            {
                msg = "Δεν βρέθηκαν καταχωρημένες βαθμολογίες για το τμήμα αυτό. Η δημιουργία του πίνακα είναι αδύνατη.";
                return msg;
            }

            List<STUDENT_GRADES> target = (from d in entities.STUDENT_GRADES where d.TMIMA_ID == tmimaId select d).ToList();
            if (target.Count > 0)
            {
                msg = "Ο πίνακας βαθμολογιών έχει ήδη εγγραφές για το τμήμα αυτό. Για ενημέρωση πατήστε 'Ενημέρωση πίνακα τμήματος'";
                return msg;
            }
            else
            {
                InsertGrades(source, tmimaId);
            }
            return msg;
        }

        public string Update(int tmimaId)
        {
            string msg = "Η ενημέρωση του πίνακα βαθμολογιών του τμήματος ολοκληρώθηκε.";

            IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> source = (from d in entities.asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ where d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, d.LESSON_TEXT, d.ΘΕ select d).ToList();
            if (source.Count() == 0)
            {
                msg = "Δεν βρέθηκαν καταχωρημένες βαθμολογίες για το τμήμα αυτό. Η ενημέρωση του πίνακα είναι αδύνατη.";
                return msg;
            }
            List<STUDENT_GRADES> target = (from d in entities.STUDENT_GRADES where d.TMIMA_ID == tmimaId orderby d.FULLNAME, d.LESSON_TEXT select d).ToList();
            if (target.Count == 0)
            {
                msg = "Ο πίνακας βαθμολογιών δεν έχει εγγραφές για το τμήμα αυτό. Για τη δημιουργία των εγγραφών πατήστε 'Δημιουργία πίνακα τμήματος'";
                return msg;
            }
            else
            {
                UpdateGrades(source, tmimaId);
            }
            return msg;
        }

        public string Destroy(int tmimaId)
        {
            string msg = "Η διαγραφή του πίνακα βαθμολογιών του τμήματος ολοκληρώθηκε.";

            var data = (from d in entities.STUDENT_GRADES where d.TMIMA_ID == tmimaId select d).ToList();
            if (data.Count == 0)
            {
                msg = "Δεν βρέθηκαν καταχωρημένες βαθμολογίες για το τμήμα αυτό. Η διαγραφή του πίνακα ακυρώθηκε.";
                return msg;
            }
            foreach (var item in data)
            {
                STUDENT_GRADES entity = entities.STUDENT_GRADES.Find(item.GRADES_ID);
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.STUDENT_GRADES.Remove(entity);
                    entities.SaveChanges();
                }

            }
            return msg;
        }

        private void InsertGrades(IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> source, int tmimaId)
        {
            decimal gradeProodosTheory = 0.0M;
            decimal gradeProodosLab = 0.0M;
            decimal gradeExamTheory = 0.0M;
            decimal gradeExamLab = 0.0M;
            decimal gradeEpanTheory = 0.0M;
            decimal gradeEpanLab = 0.0M;

            decimal gradeProodos = 0.0M;
            decimal gradeErgasia = 0.0M;
            decimal gradeExam = 0.0M;
            decimal gradeEpan = 0.0M;

            foreach (var item in source)
            {
                var source2a = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.ΘΕ == 1 select d).FirstOrDefault();
                if (source2a != null)
                {
                    gradeProodosTheory = (decimal)source2a.ΠΡ;
                    gradeExamTheory = (decimal)source2a.ΤΕ;
                    gradeEpanTheory = (decimal)source2a.ΕΠ;
                }
                else
                {
                    gradeProodosTheory = 0;
                    gradeExamTheory = 0;
                    gradeEpanTheory = 0;
                }
                var source2b = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.ΘΕ == 2 select d).FirstOrDefault();
                if (source2b != null)
                {
                    gradeProodosLab = (decimal)source2b.ΠΡ;
                    gradeExamLab = (decimal)source2b.ΤΕ;
                    gradeEpanLab = (decimal)source2b.ΕΠ;
                }
                else
                {
                    gradeProodosLab = 0;
                    gradeExamLab = 0;
                    gradeEpanLab = 0;
                }
                // Προσθήκη βαθμού εργασίας (νέο 30-11-2022)
                var source3 = (from d in entities.STUDENT_ERGASIES where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.TMIMA_ID == tmimaId select d).FirstOrDefault();
                if (source3 != null)
                {
                    gradeErgasia = (decimal)source3.GRADE;
                }

                gradeProodos = CalculateAverage(gradeProodosTheory, gradeProodosLab);
                gradeExam = CalculateAverage(gradeExamTheory, gradeExamLab);
                gradeEpan = CalculateAverage(gradeEpanTheory, gradeEpanLab);

                if (source2a != null || source2b != null)
                {
                    CreateStudentGrades(item, gradeProodosTheory, gradeProodosLab, gradeEpan, gradeExamTheory, gradeExamLab, gradeProodos, gradeExam, gradeErgasia);
                }
                IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> remove = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT select d).ToList();
                source = source.Except(remove);
            }
        }

        private void UpdateGrades(IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> source, int tmimaId)
        {
            decimal gradeProodosTheory = 0.0M;
            decimal gradeProodosLab = 0.0M;
            decimal gradeExamTheory = 0.0M;
            decimal gradeExamLab = 0.0M;
            decimal gradeEpanTheory = 0.0M;
            decimal gradeEpanLab = 0.0M;

            decimal gradeProodos = 0.0M;
            decimal gradeErgasia = 0.0M;
            decimal gradeExam = 0.0M;
            decimal gradeEpan = 0.0M;

            foreach (var item in source)
            {
                var source2a = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.ΘΕ == 1 select d).FirstOrDefault();
                if (source2a != null)
                {
                    gradeProodosTheory = (decimal)source2a.ΠΡ;
                    gradeExamTheory = (decimal)source2a.ΤΕ;
                    gradeEpanTheory = (decimal)source2a.ΕΠ;
                }
                else
                {
                    gradeProodosTheory = 0;
                    gradeExamTheory = 0;
                    gradeEpanTheory = 0;
                }
                var source2b = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.ΘΕ == 2 select d).FirstOrDefault();
                if (source2b != null)
                {
                    gradeProodosLab = (decimal)source2b.ΠΡ;
                    gradeExamLab = (decimal)source2b.ΤΕ;
                    gradeEpanLab = (decimal)source2b.ΕΠ;
                }
                else
                {
                    gradeProodosLab = 0;
                    gradeExamLab = 0;
                    gradeEpanLab = 0;
                }
                // Προσθήκη βαθμού εργασίας (νέο 30-11-2022)
                var source3 = (from d in entities.STUDENT_ERGASIES where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT && d.TMIMA_ID == tmimaId select d).FirstOrDefault();
                if (source3 != null)
                {
                    gradeErgasia = (decimal)source3.GRADE;
                }

                gradeProodos = CalculateAverage(gradeProodosTheory, gradeProodosLab);
                gradeExam = CalculateAverage(gradeExamTheory, gradeExamLab);
                gradeEpan = CalculateAverage(gradeEpanTheory, gradeEpanLab);

                if (source2a != null || source2b != null)
                {
                    UpdateStudentGrades(item, gradeProodosTheory, gradeProodosLab, gradeEpan, gradeExamTheory, gradeExamLab, gradeProodos, gradeExam, gradeErgasia);
                }
                IEnumerable<asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ> remove = (from d in source where d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT select d).ToList();
                source = source.Except(remove);
                //int stopper = 1;
            }
        }

        private void CreateStudentGrades(asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ item, decimal gradeProodosTheory, decimal gradeProodosLab, decimal gradeEpan,
                                decimal gradeExamTheory, decimal gradeExamLab, decimal gradeProodos, decimal gradeExam, decimal gradeErgasia)
        {
            STUDENT_GRADES entity = new STUDENT_GRADES()
            {
                IEK = item.ΙΕΚ,
                STUDENT_ID = item.STUDENT_ID,
                AMK = item.ΑΜΚ,
                FULLNAME = item.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                EIDIKOTITA_TEXT = item.EIDIKOTITA_TEXT,
                LESSON_TEXT = item.LESSON_TEXT,
                PERIOD_TEXT = item.ΠΕΡΙΟΔΟΣ,
                TERM_TEXT = item.TERM,
                EIDIKOTITA_ID = item.EIDIKOTITA_ID,
                TERM_ID = item.TERM_ID,
                TMIMA_ID = item.ΚΩΔ_ΤΜΗΜΑ,
                ΠΡΘ = gradeProodosTheory,
                ΠΡΕ = gradeProodosLab,
                ΤΕΘ = gradeExamTheory,
                ΤΕΕ = gradeExamLab,
                ΕΠ = gradeEpan,
                ΠΡΟΟΔΟΣ = gradeProodos,
                ΕΞΕΤΑΣΗ = gradeExam,
                ΕΡΓΑΣΙΑ = gradeErgasia,
                ΤΕΛΙΚΟΣ = CalculateTelikos(gradeProodos, gradeExam, gradeEpan, gradeErgasia)
            };
            entities.STUDENT_GRADES.Add(entity);
            entities.SaveChanges();
        }

        private void UpdateStudentGrades(asrc_ΜΑΘΗΤΕΣ_ΒΑΘΜΟΙ_ΘΕ item, decimal gradeProodosTheory, decimal gradeProodosLab, decimal gradeEpan,
                                        decimal gradeExamTheory, decimal gradeExamLab, decimal gradeProodos, decimal gradeExam, decimal gradeErgasia)
        {
            var data = (from d in entities.STUDENT_GRADES
                        where d.TMIMA_ID == item.ΚΩΔ_ΤΜΗΜΑ && d.STUDENT_ID == item.STUDENT_ID && d.LESSON_TEXT == item.LESSON_TEXT
                        select d).FirstOrDefault();

            if (data != null)
            {
                STUDENT_GRADES entity = entities.STUDENT_GRADES.Find(data.GRADES_ID);

                entity.ΠΡΘ = gradeProodosTheory;
                entity.ΠΡΕ = gradeProodosLab;
                entity.ΤΕΘ = gradeExamTheory;
                entity.ΤΕΕ = gradeExamLab;
                entity.ΕΠ = gradeEpan;
                entity.ΠΡΟΟΔΟΣ = gradeProodos;
                entity.ΕΡΓΑΣΙΑ = gradeErgasia;
                entity.ΕΞΕΤΑΣΗ = gradeExam;
                entity.ΤΕΛΙΚΟΣ = CalculateTelikos(gradeProodos, gradeExam, gradeEpan, gradeErgasia);

                entities.Entry(entity).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        private decimal CalculateAverage(decimal theory, decimal lab)
        {
            if (theory > 0 && lab > 0) return Math.Round((theory + lab) / 2.0m, MidpointRounding.AwayFromZero);
            else if (theory > 0) return theory;
            else return lab;
        }

        private decimal CalculateTelikos(decimal Proodos, decimal Exam, decimal Epanal, decimal Ergasia)
        {
            decimal Telikos;
            if (Epanal > 0)
            {
                Telikos = Epanal;
                return Telikos;
            }

            if (Ergasia > 0.0M)
            {
                Telikos = Math.Round(Proodos * 0.3M + Ergasia * 0.1M + Exam * 0.6M, MidpointRounding.AwayFromZero);
            }
            else
            {
                Telikos = Math.Round(Proodos * 0.4M + Exam * 0.6M, MidpointRounding.AwayFromZero);
            }
            return Telikos;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}