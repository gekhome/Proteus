using Kendo.Mvc.Extensions;
using Proteus.DAL;
using Proteus.Models;
using Proteus.Notification;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Proteus.Controllers.DataControllers
{
    public class ControllerUnit : Controller
    {
        public USER_SCHOOLS loggedSchool;
        public USER_ADMINS loggedAdmin;

        public const string UPLOAD_PATH = "~/Uploads/";
        public readonly DateTime currentDate = DateTime.Today;
        public const int PERIOD_ARCHIVE = 11;    // 2019X

        private readonly ProteusDBEntities db;

        public ControllerUnit(ProteusDBEntities entities)
        {
            db = entities;
        }


        #region POPULATORS

        public void PopulateLessonNames()
        {
            var lessondata = (from d in db.adk_LESSON_NAMES orderby d.LESSON_TERM, d.LESSON_TEXT select d).ToList();

            ViewData["lessons"] = lessondata;
            ViewData["defaultLesson"] = lessondata.First().LESSON_TEXT;
        }

        public void PopulateKladoiUnified()
        {
            var kladosUnified = (from d in db.SYS_KLADOS_ENIAIOS select d).ToList();
            ViewData["kladoiUnified"] = kladosUnified;
        }

        public void PopulateKladoi()
        {
            var kladosTypes = (from d in db.SYS_KLADOS where d.KLADOS_ID != 4 select d).ToList();
            ViewData["kladoi"] = kladosTypes;
        }

        public void PopulateGroups()
        {
            var groups = (from d in db.SYS_EIDIKOTITES_GROUPS select d).ToList();
            ViewData["groups"] = groups;
        }

        public void PopulateAitiesApoxorisi()
        {
            var aities = (from d in db.ΑΠΟΧΩΡΗΣΗ_ΑΙΤΙΕΣ select d).ToList();
            ViewData["aities"] = aities;
            ViewData["defaultAitia"] = aities.First().ΑΠΟΧΩΡΗΣΗ_ΚΩΔΙΚΟΣ;
        }

        public void PopulateTeachersInPeriods()
        {
            var teachers = (from d in db.sqlTEACHERS_IN_PERIODS orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["teachers"] = teachers;
            ViewData["defaultTeacher"] = teachers.First().TEACHER_ID;
        }

        public void PopulateTeachersInPeriods(int schoolId)
        {
            var teachers = (from d in db.sqlTEACHERS_IN_PERIODS where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["teachers"] = teachers;
            ViewData["defaultTeacher"] = teachers.First().TEACHER_ID;
        }

        public void PopulateSchoolYears()
        {
            var schoolyears = (from p in db.SYS_SCHOOLYEARS orderby p.SY_TEXT select p).ToList();
            ViewData["schoolyears"] = schoolyears;
            ViewData["defaultSchoolYear"] = schoolyears.First().SY_ID;
        }

        public void PopulateTeachers(int schoolId)
        {
            var teachers = (from d in db.qryTEACHER_SELECTOR where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["teachers"] = teachers;
            ViewData["defaultTeacher"] = teachers.First().TEACHER_ID;
        }

        public void PopulateEidikotites()
        {
            var data = (from d in db.VD_EIDIKOTITES orderby d.EIDIKOTITA_KLADOS_ID, d.EIDIKOTITA_DESC select d).ToList();
            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateSchools()
        {
            var Schools = (from d in db.SYS_SCHOOLS where d.SCHOOL_ID != 999 orderby d.SCHOOL_NAME select d).ToList();
            ViewData["schools"] = Schools;
            ViewData["defaultSchool"] = Schools.First().SCHOOL_ID;
        }

        public void PopulatePeriferies()
        {
            var periferies = (from periferia in db.SYS_PERIFERIES select periferia).ToList();
            ViewData["Periferies"] = periferies;
        }

        public void PopulateXE()
        {
            var xe = (from d in db.ΧΕ select d).ToList();
            ViewData["xe"] = xe;
            ViewData["defaultxe"] = xe.First();
        }

        public void PopulateShoolYears()
        {
            var SchoolYears = (from sy in db.SYS_SCHOOLYEARS orderby sy.SY_TEXT select sy).ToList();
            ViewData["schoolyears"] = SchoolYears;
            ViewData["defaultSchoolYear"] = SchoolYears.First();
        }

        public void PopulateGenders()
        {
            var genders = (from g in db.SYS_GENDERS select g).ToList();
            ViewData["genders"] = genders;
            ViewData["defaultGender"] = genders.First();
        }

        public void PopulatePA()
        {
            var pa = (from d in db.ΠΑ select d).ToList();
            ViewData["pa"] = pa;
        }

        public void PopulatePeriodoi()
        {
            var periods = (from p in db.ΠΕΡΙΟΔΟΙ orderby p.ΠΕΡΙΟΔΟΣ select p).ToList();
            ViewData["periodoi"] = periods;
            ViewData["defaultPeriodos"] = periods.First().PERIOD_ID;
        }

        public void PopulateTerms()
        {
            var terms = (from t in db.SYS_TERM select t).ToList();
            ViewData["terms"] = terms;
        }

        public void PopulateLessonTerms()
        {
            var terms = (from t in db.SYS_TERM where t.TERM_ID < 5 select t).ToList();
            ViewData["terms"] = terms;
        }

        public void PopulateIekEidikotites(int schoolId)
        {
            var iedata = (from d in db.viewIEK_EIDIKOTITES where d.IEK_ID == schoolId orderby d.EIDIKOTITA_TEXT select d).ToList();

            ViewData["eidikotitesIek"] = iedata;
            ViewData["defaultEidikotita"] = iedata.First().EIDIKOTITA_ID;
        }

        public void PopulateIekTmimata(int schoolId)
        {
            var tmimata = (from d in db.ΤΜΗΜΑ where d.ΙΕΚ == schoolId orderby d.ΠΕΡΙΟΔΟΣ_ΚΩΔ, d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();
            ViewData["tmimata"] = tmimata;
            ViewData["defaultTmima"] = tmimata.First().ΤΜΗΜΑ_ΚΩΔ;
        }

        public void PopulateLessonTypes()
        {
            var lessonTypes = (from d in db.LESSON_TYPES select d).ToList();
            ViewData["lessontypes"] = lessonTypes;
        }

        public void PopulateEidikotitesKatartisi()
        {
            var eidikotites = (from e in db.SYS_EIDIKOTITES_IEK where e.APPROVED == true orderby e.EIDIKOTITA_TEXT select e).ToList();
            ViewData["eidikotites"] = eidikotites;
            ViewData["defaultEidikotita"] = eidikotites.First().EIDIKOTITA_ID;
        }

        public void PopulateAllEidikotitesKatartisi()
        {
            var data = (from d in db.SYS_EIDIKOTITES_IEK orderby d.EIDIKOTITA_TEXT select d).ToList();
            ViewData["eidikotites"] = data;
            ViewData["defaultEidikotita"] = data.First().EIDIKOTITA_ID;
        }

        public void PopulateRegisterTypes()
        {
            var regtypes = (from d in db.ΕΓΓΡΑΦΗ_ΕΙΔΗ select d).ToList();
            ViewData["registerTypes"] = regtypes;
        }

        public void PopulateFoitisi()
        {
            var foitisi = (from d in db.ΦΟΙΤΗΣΗ select d).ToList();
            ViewData["foitisi"] = foitisi;
        }

        public void PopulateErgasiesTypes()
        {
            var data = (from d in db.ΘΕΜΑΤΙΚΕΣ_ΕΙΔΗ select d).ToList();

            ViewData["ergasiesTypes"] = data;
            ViewData["defaultType"] = data.First().TYPE_ID;
        }

        public void PopulateTransferLessons(int schoolId)
        {
            var data = (from d in db.sqlIEK_LESSONS where d.IEK_ID == schoolId orderby d.LESSON_DESC select d).ToList();
            ViewData["lessons"] = data;
            ViewData["defaultLesson"] = data.First().LESSON_ID;
        }

        public void PopulateTeachersWithPeriods(int schoolId)
        {
            var teachers = (from d in db.sqlTEACHERS_WITH_PERIODS where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();

            ViewData["teachers"] = teachers;
            ViewData["defaultTeacher"] = teachers.First().TEACHER_ID;
        }

        public void PopulateIekTmimataBek(int schoolId)
        {
            var tmimata = (from d in db.sqlΤΜΗΜΑΤΑ_ΒΕΚ where d.ΙΕΚ == schoolId orderby d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();

            ViewData["tmimata"] = tmimata;
            ViewData["defaultTmima"] = tmimata.First().ΤΜΗΜΑ_ΚΩΔ;
        }

        public void PopulateStudents(int schoolId)
        {
            var students = (from d in db.qrySTUDENT_TMIMA_SELECTOR where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();

            ViewData["students"] = students;
            ViewData["defaultStudent"] = students.First().STUDENT_ID;
        }

        public void PopulateErgasies()
        {
            var data = (from d in db.ΕΡΓΑΣΙΕΣ select d).ToList();
            ViewData["ergasies"] = data;
            ViewData["defaultErgasia"] = data.First().ΚΩΔ_ΕΡΓΑΣΙΑ;
        }

        public void PopulateWeeks()
        {
            var data = (from d in db.NUMBERS select d).ToList();
            ViewData["weeks"] = data;
        }

        public void PopulateHours()
        {
            var data = (from d in db.NUMBERS select d).ToList();
            ViewData["hours"] = data;
        }

        public void PopulateLessons(int schoolId, int eidikotitaId, int termId)
        {
            var data = (from d in db.qryIEK_EIDIKOTITES_LESSONS
                        where d.LESSON_EIDIKOTITA == eidikotitaId && d.LESSON_TERM == termId && d.IEK_ID == schoolId
                        orderby d.LESSON_DESC
                        select d).ToList();
            ViewData["lessons"] = data;
            ViewData["defaultLesson"] = data.First().LESSON_ID;
        }

        public void PopulateLessons(int schoolId)
        {
            var data = (from d in db.qryIEK_EIDIKOTITES_LESSONS where d.IEK_ID == schoolId orderby d.LESSON_DESC select d).ToList();
            ViewData["lessons"] = data;
            ViewData["defaultLesson"] = data.First().LESSON_ID;
        }

        public void PopulateApousiesLessons(int schoolId)
        {
            var data = (from d in db.qryIEK_LESSONS_IN_PROGRAMMADAY where d.IEK_ID == schoolId orderby d.LESSON_DESC select d).ToList();
            if (data.Count > 0)
            {
                ViewData["lessons"] = data;
                ViewData["defaultLesson"] = data.First().LESSON_ID;
            }
            else
            {
                ViewData["lessons"] = data;
                ViewData["defaultLesson"] = 0;
            }
        }

        public void PopulateLessonTags()
        {
            var lessonTags = (from d in db.ΜΑΘΗΜΑ_ΧΑΡΑΚΤΗΡΙΣΜΟΣ select d).ToList();
            ViewData["lesson_tags"] = lessonTags;
        }

        public void PopulateErgodotes(int schoolId)
        {
            var data = (from d in db.ΕΡΓΟΔΟΤΕΣ
                        where d.ΙΕΚ == schoolId
                        orderby d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ
                        select d).ToList();

            ViewData["ergodotes"] = data;
        }

        public void PopulateTmimataPraktiki(int schoolId)
        {
            var tmimata = (from d in db.qryTMIMA_PRAKTIKI_SELECTOR where d.ΙΕΚ == schoolId orderby d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();

            ViewData["tmimata"] = tmimata;
            ViewData["defaultTmima"] = tmimata.First().ΤΜΗΜΑ_ΚΩΔ;
        }

        public void PopulateStudentsPraktiki(int schoolId)
        {
            var students = (from d in db.qrySTUDENT_PRAKTIKI_SELECTOR where d.ΙΕΚ == schoolId orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();

            ViewData["students"] = students;
            ViewData["defaultStudent"] = students.First().STUDENT_ID;
        }

        public void PopulateIekTmimataBek()
        {
            var tmimata = (from d in db.sqlΤΜΗΜΑΤΑ_ΒΕΚ orderby d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();
            ViewData["tmimata"] = tmimata;
            ViewData["defaultTmima"] = tmimata.First().ΤΜΗΜΑ_ΚΩΔ;
        }

        public void PopulateBekStudents()
        {
            var students = (from d in db.qrySTUDENT_BEK_SELECTOR orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["students"] = students;
            ViewData["defaultStudent"] = students.First().STUDENT_ID;
        }

        public void PopulateStudents()
        {
            var students = (from d in db.qrySTUDENT_GLOBAL_SELECTOR orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["students"] = students;
            ViewData["defaultStudent"] = students.First().STUDENT_ID;
        }

        public void PopulateApolytiriaKlaseis()
        {
            var data = (from d in db.ΑΠΟΛΥΤΗΡΙΑ_ΚΛΑΣΕΙΣ select d).ToList();
            ViewData["klaseis"] = data;
        }

        public void PopulateStatus()
        {
            var data = (from d in db.ΧΜ_ΕΓΚΥΚΛΙΟΣ_ΚΑΘΕΣΤΩΣ select d).ToList();
            ViewData["Status"] = data;
        }

        public void PopulateTeachers()
        {
            var teachers = (from d in db.qryTEACHER_SELECTOR orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ select d).ToList();
            ViewData["teachers"] = teachers;
            ViewData["defaultTeacher"] = teachers.First().TEACHER_ID;
        }

        public void PopulatePeriferiakes()
        {
            var data = (from d in db.SYS_PERIFERIAKES orderby d.PERIFERIAKI select d).ToList();

            ViewData["periferiakes"] = data;
        }

        public void PopulateIekEidikotites()
        {
            var iedata = (from d in db.viewIEK_EIDIKOTITES orderby d.EIDIKOTITA_TEXT select d).ToList();
            ViewData["eidikotitesIek"] = iedata;
            ViewData["defaultEidikotita"] = iedata.First().EIDIKOTITA_ID;
        }

        public void PopulateIekTmimata()
        {
            var tmimata = (from d in db.ΤΜΗΜΑ orderby d.ΠΕΡΙΟΔΟΣ_ΚΩΔ, d.ΤΜΗΜΑ_ΟΝΟΜΑ select d).ToList();
            ViewData["tmimata"] = tmimata;
        }

        public void PopulateKlados()
        {
            var data = (from d in db.SYS_KLADOS orderby d.KLADOS_ID select d).ToList();
            ViewData["kladoi"] = data;
        }

        public void PopulateSqlEidikotites2()
        {
            var data = (from d in db.sqlEIDIKOTITES_KU
                        orderby d.EIDIKOTITA_KLADOS_ID, d.EIDIKOTITA_PTYXIO
                        select new sqlEidikotitesKUViewModel
                        {
                            EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                            EIDIKOTITA_PTYXIO = d.EIDIKOTITA_PTYXIO,
                            KLADOS_UNIFIED = d.KLADOS_UNIFIED,
                            EIDIKOTITA_KLADOS_ID = d.EIDIKOTITA_KLADOS_ID
                        }).ToList();

            ViewData["sqlEidikotites2"] = data;
            ViewData["sqlDefaultEidikotita2"] = data.First().EIDIKOTITA_ID;
        }

        #endregion


        #region COMBOS DATA SOURCES - GETTERS

        public JsonResult GetExamCategories()
        {
            var data = db.ΕΞΕΤΑΣΗ_ΚΑΤΗΓΟΡΙΕΣ.Select(d => new ExamCategoriesViewModel
            {
                ΚΑΤΗΓΟΡΙΑ_ΚΩΔΙΚΟΣ = d.ΚΑΤΗΓΟΡΙΑ_ΚΩΔΙΚΟΣ,
                ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ = d.ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKlados()
        {
            var kladoi = db.SYS_KLADOS.Select(d => new SYS_KLADOSViewModel
            {
                KLADOS_ID = d.KLADOS_ID,
                KLADOS_NAME = d.KLADOS_NAME
            });
            return Json(kladoi, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherEidikotites(int? klados)
        {
            var eidikotites = db.qryEIDIKOTITES_SELECTOR.AsQueryable();

            if (klados != null)
            {
                eidikotites = db.qryEIDIKOTITES_SELECTOR.Where(m => m.EIDIKOTITA_KLADOS_ID == klados);
            }
            return Json(eidikotites.OrderBy(m => m.EIDIKOTITA_DESC), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSpoudes()
        {
            var spoudes = db.ΣΠΟΥΔΕΣ.Select(s => new SpoudesViewModel
            {
                ΒΑΘΜΙΔΑ_ΚΩΔ = s.ΒΑΘΜΙΔΑ_ΚΩΔ,
                ΒΑΘΜΙΔΑ = s.ΒΑΘΜΙΔΑ
            });
            return Json(spoudes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetYpalilos()
        {
            var data = db.ΥΠΑΛΛΗΛΟΣ.Select(d => new YpalilosViewModel
            {
                ΥΠΑΛΛΗΛΟΣ_ΚΩΔ = d.ΥΠΑΛΛΗΛΟΣ_ΚΩΔ,
                ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ = d.ΥΠΑΛΛΗΛΟΣ_ΛΕΚΤΙΚΟ
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApasxolisi()
        {
            var data = db.ΑΠΑΣΧΟΛΗΣΗ.Select(d => new ApasxolisiViewModel
            {
                ΑΠΑΣΧΟΛΗΣΗ_ΚΩΔ = d.ΑΠΑΣΧΟΛΗΣΗ_ΚΩΔ,
                ΑΠΑΣΧΟΛΗΣΗ_ΛΕΚΤΙΚΟ = d.ΑΠΑΣΧΟΛΗΣΗ_ΛΕΚΤΙΚΟ
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSchools()
        {
            var data = (from d in db.SYS_SCHOOLS
                        orderby d.SCHOOL_NAME
                        where d.SCHOOL_ID != 999
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOL_PERIFERIAKI_ID = d.SCHOOL_PERIFERIAKI_ID
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGenders(string text)
        {
            var genders = db.SYS_GENDERS.Select(p => new SYS_GENDERSViewModel
            {
                GENDER = p.GENDER,
                GENDER_ID = p.GENDER_ID
            });

            if (!string.IsNullOrEmpty(text))
            {
                genders = genders.Where(p => p.GENDER.Contains(text));
            }
            return Json(genders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNationality(string text)
        {
            var data = db.ΥΠΗΚΟΟΤΗΤΑ.Select(p => new NationalityViewModel
            {
                ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ = p.ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                ΥΠΗΚΟΟΤΗΤΑ_ΚΩΔ = p.ΥΠΗΚΟΟΤΗΤΑ_ΚΩΔ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΥΠΗΚΟΟΤΗΤΑ_ΛΕΚΤΙΚΟ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPraxisEisodos(string text)
        {
            var data = db.ΠΡΑΞΕΙΣ_ΕΙΣΟΔΟΥ.Select(d => new EisodosPraxiViewModel
            {
                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΚΩΔ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ_ΚΩΔ,
                ΠΡΑΞΗ_ΕΙΣΟΔΟΥ = d.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΠΡΑΞΗ_ΕΙΣΟΔΟΥ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPraxisExodos(string text)
        {
            var data = db.ΠΡΑΞΕΙΣ_ΕΞΟΔΟΥ.Select(d => new ExodosPraxiViewModel
            {
                ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΩΔ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ_ΚΩΔ,
                ΠΡΑΞΗ_ΕΞΟΔΟΥ = d.ΠΡΑΞΗ_ΕΞΟΔΟΥ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΠΡΑΞΗ_ΕΞΟΔΟΥ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApolytiria(string text)
        {
            var data = db.ΑΠΟΛΥΤΗΡΙΑ.Select(p => new ApolytiriaViewModel
            {
                ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ = p.ΑΠΟΛΥΤΗΡΙΟ_ΚΩΔΙΚΟΣ,
                ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ = p.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.ΑΠΟΛΥΤΗΡΙΟ_ΛΕΚΤΙΚΟ.Contains(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNumbers(string text)
        {
            var data = db.NUMBERS.Select(d => new NumbersViewModel
            {
                NUMBER = d.NUMBER
            });

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(p => p.NUMBER.Equals(text));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeriferiakes(string text)
        {
            var periferiakes = db.SYS_PERIFERIAKES.Select(p => new SYS_PERIFERIAKESViewModel
            {
                PERIFERIAKI_ID = p.PERIFERIAKI_ID,
                PERIFERIAKI = p.PERIFERIAKI
            });

            if (!string.IsNullOrEmpty(text))
            {
                int possibleInt;
                if (int.TryParse(text, out possibleInt))
                {
                    periferiakes = periferiakes.Where(p => p.PERIFERIAKI_ID.Equals(possibleInt));
                }
                else
                {
                    periferiakes = periferiakes.Where(p => p.PERIFERIAKI.Contains(text));
                }
                periferiakes = periferiakes.Where(p => p.PERIFERIAKI.Contains(text));
            }
            return Json(periferiakes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeriferies(string text)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new SYS_PERIFERIESViewModel
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });

            if (!string.IsNullOrEmpty(text))
            {
                int possibleInt;
                if (int.TryParse(text, out possibleInt))
                {
                    periferies = periferies.Where(p => p.PERIFERIA_ID.Equals(possibleInt));
                }
                else
                {
                    periferies = periferies.Where(p => p.PERIFERIA_NAME.Contains(text));
                }
                periferies = periferies.Where(p => p.PERIFERIA_NAME.Contains(text));
            }
            return Json(periferies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeDimoi(int? periferia, string dimosFilter)
        {
            var dimoi = db.SYS_DIMOS.AsQueryable();

            if (periferia != null)
            {
                dimoi = dimoi.Where(p => p.DIMOS_PERIFERIA == periferia);
            }
            else if (!string.IsNullOrEmpty(dimosFilter))
            {
                int possibleInt;
                if (int.TryParse(dimosFilter, out possibleInt))
                {
                    dimoi = dimoi.Where(p => p.DIMOS_ID.Equals(possibleInt));
                }
                else
                {
                    dimoi = dimoi.Where(p => p.DIMOS.Contains(dimosFilter));
                }
                dimoi = dimoi.Where(p => p.DIMOS.Contains(dimosFilter));
            }
            return Json(dimoi.Select(p => new { DIMOS_ID = p.DIMOS_ID, DIMOS = p.DIMOS }), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region VALIDATIONS (required in case everything is empty)

        public bool StudentsExist(int schoolId)
        {
            var students = (from d in db.ΜΑΘΗΤΕΣ where d.ΙΕΚ == schoolId select d).Count();

            if (students > 0) return true;
            else return false;
        }

        public bool IekEidikotitesExist(int schoolId)
        {
            var eidikotites = (from d in db.IEK_EIDIKOTITES where d.IEK_ID == schoolId select d).Count();

            if (eidikotites > 0) return true;
            else return false;
        }

        public bool IekTmimataExist(int schoolId)
        {
            var tmimata = (from d in db.ΤΜΗΜΑ where d.ΙΕΚ == schoolId select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        public bool TeachersExist(int schoolId)
        {
            var teachers = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ where d.ΙΕΚ == schoolId select d).Count();

            if (teachers > 0) return true;
            else return false;
        }

        public bool IekTmimataBekExist(int schoolId)
        {
            var tmimata = (from d in db.sqlΤΜΗΜΑΤΑ_ΒΕΚ where d.ΙΕΚ == schoolId select d).ToList();

            if (tmimata.Count() > 0) return true;
            else return false;
        }

        public bool TeacherWithPeriodsExist(int schoolId)
        {
            var data = (from d in db.sqlTEACHERS_WITH_PERIODS where d.ΙΕΚ == schoolId select d).ToList();
            if (data.Count > 0) return true;
            else return false;
        }

        public bool ErgodotesExist()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var teachers = (from d in db.ΕΡΓΟΔΟΤΕΣ where d.ΙΕΚ == schoolId select d).Count();

            if (teachers > 0) return true;
            else return false;
        }

        public bool TmimataPraktikiExist()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var tmimata = (from d in db.qryTMIMA_PRAKTIKI_SELECTOR where d.ΙΕΚ == schoolId select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        public bool StudentsPraktikiExist()
        {
            int schoolId = (int)GetLoginSchool().USER_SCHOOLID;

            var tmimata = (from d in db.qrySTUDENT_PRAKTIKI_SELECTOR where d.ΙΕΚ == schoolId select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        public bool TeachersExist()
        {
            var teachers = (from d in db.ΕΚΠΑΙΔΕΥΤΕΣ select d).Count();

            if (teachers > 0) return true;
            else return false;
        }

        public bool StudentsExist()
        {
            var students = (from d in db.ΜΑΘΗΤΕΣ select d).Count();

            if (students > 0) return true;
            else return false;
        }

        public bool IekEidikotitesExist()
        {
            var eidikotites = (from d in db.IEK_EIDIKOTITES select d).Count();

            if (eidikotites > 0) return true;
            else return false;
        }

        public bool IekTmimataExist()
        {
            var tmimata = (from d in db.ΤΜΗΜΑ select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        public bool IekTmimataBekExist()
        {
            var tmimata = (from d in db.sqlΤΜΗΜΑΤΑ_ΒΕΚ select d).Count();

            if (tmimata > 0) return true;
            else return false;
        }

        #endregion


        #region LOGIN GETTERS

        public USER_SCHOOLS GetLoginSchool()
        {
            loggedSchool = db.USER_SCHOOLS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int SchoolID = loggedSchool.USER_SCHOOLID ?? 0;
            var _school = (from s in db.sqlUSER_SCHOOL
                           where s.USER_SCHOOLID == SchoolID
                           select new { s.SCHOOL_NAME }).FirstOrDefault();

            ViewBag.loggedUser = _school.SCHOOL_NAME;
            return loggedSchool;
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;

            return loggedAdmin;
        }

        #endregion


        #region ERROR PAGES

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #endregion

    }
}