using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class TableAtomikoDeltioService : ITableAtomikoDeltioService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public TableAtomikoDeltioService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public string Create(int studentId, int schoolId)
        {
            string msg = "Η δημιουργία του πίνακα ατομικού δελτίου του σπουδαστή ολοκληρώθηκε.";
            int result = 0;

            var egrafes_data = (from d in entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ
                                where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && (d.ΦΟΙΤΗΣΗ == 1 || d.ΦΟΙΤΗΣΗ == 2 || d.ΦΟΙΤΗΣΗ == 6 || d.ΦΟΙΤΗΣΗ == 7)
                                orderby d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ
                                select d).ToList();
            if (egrafes_data.Count == 0)
            {
                msg = "Δεν βρέθηκαν έγκυρες εγγραφές τμημάτων για ατομικά δελτία του σπουδαστή αυτού. Η διαδικασία ακυρώθηκε.";
                return msg;
            }

            foreach (var item in egrafes_data)
            {
                var target_data = (from d in entities.ATOMIKA_DELTIA where d.ΜΑΘΗΤΗΣ_ΚΩΔ == item.ΜΑΘΗΤΗΣ_ΚΩΔ && d.ΚΩΔ_ΤΜΗΜΑ == item.ΚΩΔ_ΤΜΗΜΑ select d).ToList();

                if (target_data.Count == 0)
                    result = CreateADKRecordSet(item.ΜΑΘΗΤΗΣ_ΚΩΔ, item.ΚΩΔ_ΤΜΗΜΑ, item.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ, item.ΦΟΙΤΗΣΗ, schoolId);
                if (result != 0)
                {
                    msg = ErrorCodesDictionary(result);
                    break;
                }
            }
            return msg;
        }

        public string Update(int studentId, int schoolId)
        {
            string msg = "Η ενημέρωση του πίνακα ατομικού δελτίου του σπουδαστή ολοκληρώθηκε.";

            // STEP 1: Remove data for this dtudent
            var target_data = (from d in entities.ATOMIKA_DELTIA where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId select d).ToList();
            if (target_data.Count > 0)
            {
                foreach (var item in target_data)
                {
                    if (item != null)
                    {
                        entities.Entry(item).State = EntityState.Deleted;
                        entities.ATOMIKA_DELTIA.Remove(item);
                        entities.SaveChanges();
                    }
                }
            }
            // STEP 2: Create recordset for this student
            int result = 0;

            var egrafes_data = (from d in entities.ΜΑΘΗΤΕΣ_ΕΓΓΡΑΦΕΣ
                                where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && (d.ΦΟΙΤΗΣΗ == 1 || d.ΦΟΙΤΗΣΗ == 2 || d.ΦΟΙΤΗΣΗ == 6)
                                orderby d.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ
                                select d).ToList();
            if (egrafes_data.Count == 0)
            {
                msg = "Δεν βρέθηκαν έγκυρες εγγραφές τμημάτων για ατομικά δελτία του σπουδαστή αυτού. Η διαδικασία ακυρώθηκε.";
                return msg;
            }

            foreach (var item in egrafes_data)
            {
                result = CreateADKRecordSet(item.ΜΑΘΗΤΗΣ_ΚΩΔ, item.ΚΩΔ_ΤΜΗΜΑ, item.ΕΓΓΡΑΦΗ_ΕΙΔΟΣ, item.ΦΟΙΤΗΣΗ, schoolId);
                if (result != 0)
                {
                    msg = ErrorCodesDictionary(result);
                    break;
                }
            }
            return msg;
        }

        public string Destroy(int studentId)
        {
            string msg = "Η διαγραφή του πίνακα ατομικού δελτίου του σπουδαστή ολοκληρώθηκε.";

            var target_data = (from d in entities.ATOMIKA_DELTIA where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId select d).ToList();
            if (target_data.Count == 0)
            {
                msg = "Δεν βρέθηκαν στοιχεία ατομικού δελτίου για τον επιλεγμένο σπουδαστή. Η διαγραφή ακυρώθηκε.";
                return msg;
            }
            foreach (var item in target_data)
            {
                if (item != null)
                {
                    entities.Entry(item).State = EntityState.Deleted;
                    entities.ATOMIKA_DELTIA.Remove(item);
                    entities.SaveChanges();
                }
            }
            return msg;
        }

        private int CreateADKRecordSet(int? studentId, int? tmimaId, int? egrafi_type, int? foitisi, int iek)
        {
            if (!(studentId > 0) || !(tmimaId > 0)) return -1;

            var source1 = (from d in entities.adk_GRADES_APOUSIES_OUTPUT where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΚΩΔ_ΤΜΗΜΑ == tmimaId orderby d.ΜΑΘΗΜΑ select d).ToList();

            if (source1.Count() == 0)
                return -2;
            var source2 = (from d in entities.adk_GRADES_GMO_OUTPUT where d.STUDENT_ID == studentId && d.ΚΩΔ_ΤΜΗΜΑ == tmimaId select d).FirstOrDefault();
            if (source2 == null)
                return -3;
            var source3 = (from d in entities.adk_ΤΜΗΜΑ_ΠΕΡΙΟΔΟΣ where d.ΤΜΗΜΑ_ΚΩΔ == tmimaId select d).FirstOrDefault();
            if (source3 == null)
                return -4;

            // Good to go with the data sources
            foreach (var d in source1)
            {
                ATOMIKA_DELTIA entity = new ATOMIKA_DELTIA()
                {
                    ΜΑΘΗΤΗΣ_ΚΩΔ = studentId,
                    ΚΩΔ_ΤΜΗΜΑ = tmimaId,
                    ΕΓΓΡΑΦΗ_ΕΙΔΟΣ = egrafi_type,
                    ΦΟΙΤΗΣΗ = foitisi,
                    ΣΧΟΛΕΙΟ = iek,
                    ΜΑΘΗΜΑ = d.ΜΑΘΗΜΑ,
                    ΜΟΒΠ = (int)d.ΜΟΒΠ,
                    ΜΟΤΕ = (int)d.ΜΟΤΕ,
                    ΜΟΕΠ = (int)d.ΜΟΕΠ,
                    ΒΕ = d.ΒΕ,
                    ΜΟ = (int)d.ΜΟ,
                    ΣΥΝΟΛΟ_ΩΡΕΣ = d.ΣΥΝΟΛΟ_ΩΡΕΣ,
                    ΟΡΙΟ = (int)d.ΟΡΙΟ,
                    ΑΠΟΥΣΙΕΣ = d.ΑΠΟΥΣΙΕΣ,
                    ΑΠΟΤΕΛΕΣΜΑ = d.ΑΠΟΤΕΛΕΣΜΑ,
                    LESSON_COUNT = source2.LESSON_COUNT,
                    GRADES_SUM = (int)source2.GRADES_SUM,
                    ΓΜΟ = (decimal)source2.ΓΜΟ,
                    TERM_ID = source3.TERM_ID,
                    TERM_TEXT = source3.TERM_TEXT,
                    ΠΕΡΙΟΔΟΣ = source3.ΠΕΡΙΟΔΟΣ,
                    PERIOD_ID = source3.PERIOD_ID,
                    ΧΕ_ΚΩΔ = source3.ΧΕ_ΚΩΔ,
                    SY_TEXT = source3.SY_TEXT
                };
                entities.ATOMIKA_DELTIA.Add(entity);
                entities.SaveChanges();
            }
            return 0;
        }

        private string ErrorCodesDictionary(int key)
        {
            var ErrorCodes = new Dictionary<int, string>()
            {
                {-1, "Παρουσιάστηκε σφάλμα μη έγκυρων κωδικών σπουδαστή ή/και τμήματος. Η διαδικασία διακόπηκε."},
                {-2, "Παρουσιάστηκε σφάλμα μη εύρεσης βαθμών ή/και απουσιών. Η διαδικασία διακόπηκε."},
                {-3, "Παρουσιάστηκε σφάλμα μη εύρεσης γενικού μέσου όρου του σπουδαστή για το τμήμα αυτό. Η διαδικασία διακόπηκε."},
                {-4, "Παρουσιάστηκε σφάλμα μη εύρεσης στοιχείων τμήματος του σπουδαστή. Η διαδικασία διακόπηκε."}
            };
            return ErrorCodes[key];
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}