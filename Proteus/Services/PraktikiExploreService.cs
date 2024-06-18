using Proteus.DAL;
using Proteus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Proteus.Services
{
    public class PraktikiExploreService : IPraktikiExploreService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public PraktikiExploreService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<PraktikiExploreViewModel> ReadStudents(int schoolId)
        {
            var data = (from d in entities.sqlSTUDENTS_PRAKTIKI_EXPLORE
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new PraktikiExploreViewModel
                        {
                            ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ = d.ΕΡΓ_ΠΡΑΚΤΙΚΗ_ΚΩΔ,
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΕΞΑΜΗΝΟ = d.ΕΞΑΜΗΝΟ,
                            ΠΕΡΙΟΔΟΣ_ΚΩΔ = d.ΠΕΡΙΟΔΟΣ_ΚΩΔ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΕΩΣ = d.ΗΜΝΙΑ_ΕΩΣ,
                            ΩΡΕΣ = d.ΩΡΕΣ,
                            ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ = d.ΕΡΓΟΔΟΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ = d.ΕΡΓΟΔΟΤΗΣ_ΕΠΩΝΥΜΙΑ,
                            ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ = d.ΠΡΑΚΤΙΚΗ_ΠΕΡΑΤΩΣΗ
                        }).ToList();
            return data;
        }

        public List<PraktikiApallagiViewModel> ReadApallages(int schoolId)
        {
            var data = (from d in entities.sqlSTUDENTS_PRAKTIKI_APALLAGI
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new PraktikiApallagiViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ = d.ΠΡΑΚΤΙΚΗ_ΑΠΑΛΛΑΓΗ,
                            ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ = d.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ
                        }).ToList();
            return data;
        }

        public List<PraktikiDiakopiViewModel> ReadDiakopes(int schoolId)
        {
            var data = (from d in entities.sqlSTUDENTS_PRAKTIKI_DIAKOPI
                        where d.ΙΕΚ == schoolId
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new PraktikiDiakopiViewModel
                        {
                            STUDENT_ID = d.STUDENT_ID,
                            ΙΕΚ = d.ΙΕΚ,
                            ΑΜΚ = d.ΑΜΚ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ = d.ΠΡΑΚΤΙΚΗ_ΔΙΑΚΟΠΗ,
                            ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ = d.ΠΡΑΚΤΙΚΗ_ΣΧΟΛΙΟ
                        }).ToList();
            return data;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}