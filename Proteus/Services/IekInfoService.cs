using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class IekInfoService : IIekInfoService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public IekInfoService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SYS_SCHOOLSViewModel> Read()
        {
            var data = (from d in entities.SYS_SCHOOLS
                        orderby d.SCHOOL_NAME
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOL_PERIFERIA_ID = d.SCHOOL_PERIFERIA_ID,
                            SCHOOL_PERIFERIAKI_ID = d.SCHOOL_PERIFERIAKI_ID,
                            SCHOOL_ADDRESS = d.SCHOOL_ADDRESS,
                            SCHOOL_TK_CITY = d.SCHOOL_TK_CITY,
                            SCHOOL_PHONE = d.SCHOOL_PHONE,
                            SCHOOL_FAX = d.SCHOOL_FAX,
                            SCHOOL_EMAIL = d.SCHOOL_EMAIL,
                            SCHOOL_DIMOS = d.SCHOOL_DIMOS,
                            SCHOOL_DIRECTOR = d.SCHOOL_DIRECTOR,
                            SCHOOL_DEPUTY = d.SCHOOL_DEPUTY,
                            SCHOOL_INFO = d.SCHOOL_INFO,
                            DEPUTY_GENDER = d.DEPUTY_GENDER,
                            DIRECTOR_GENDER = d.DIRECTOR_GENDER
                        }).ToList();
            return (data);
        }

        public SYS_SCHOOLSViewModel GetRecord(int schoolId)
        {
            var data = (from d in entities.SYS_SCHOOLS
                        where d.SCHOOL_ID == schoolId
                        select new SYS_SCHOOLSViewModel
                        {
                            SCHOOL_ID = d.SCHOOL_ID,
                            SCHOOL_NAME = d.SCHOOL_NAME,
                            SCHOOL_PERIFERIAKI_ID = d.SCHOOL_PERIFERIAKI_ID,
                            SCHOOL_PERIFERIA_ID = d.SCHOOL_PERIFERIA_ID,
                            SCHOOL_DIMOS = d.SCHOOL_DIMOS,
                            SCHOOL_ADDRESS = d.SCHOOL_ADDRESS,
                            SCHOOL_TK_CITY = d.SCHOOL_TK_CITY,
                            SCHOOL_EMAIL = d.SCHOOL_EMAIL,
                            SCHOOL_PHONE = d.SCHOOL_PHONE,
                            SCHOOL_FAX = d.SCHOOL_FAX,
                            SCHOOL_DIRECTOR = d.SCHOOL_DIRECTOR,
                            SCHOOL_DEPUTY = d.SCHOOL_DEPUTY,
                            SCHOOL_INFO = d.SCHOOL_INFO,
                            DIRECTOR_GENDER = d.DIRECTOR_GENDER,
                            DEPUTY_GENDER = d.DEPUTY_GENDER
                        }).FirstOrDefault();
            return data;
        }

        public IEnumerable<qryIekEidikotitesViewModel> GetEidikotites(int schoolId)
        {
            var eidikotites = (from d in entities.qryIEK_EIDIKOTITES
                               where d.IEK_ID == schoolId
                               select new qryIekEidikotitesViewModel
                               {
                                   IEK_ID = d.IEK_ID,
                                   EIDIKOTITA_ID = d.EIDIKOTITA_ID,
                                   EIDIKOTITA_TEXT = d.EIDIKOTITA_TEXT
                               }).ToList();
            return (eidikotites);
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}