using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.ServicesMoria
{
    public class UploadService : IUploadService
    {
        private readonly ProteusDBEntities entities;

        public UploadService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UploadsViewModel> Read(int egykliosId, string Afm)
        {
            var data = (from d in entities.ΧΜ_UPLOADS
                        where d.EGYKLIOS_ID == egykliosId && d.STUDENT_AFM == Afm
                        orderby d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            AITISI_ID = d.AITISI_ID,
                            STUDENT_AFM = d.STUDENT_AFM,
                            SCHOOL_ID = d.SCHOOL_ID,
                            EGYKLIOS_ID = d.EGYKLIOS_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();
            return (data);
        }

        public IEnumerable<UploadsViewModel> Read(int egykliosId, int schoolId)
        {
            var data = (from d in entities.ΧΜ_UPLOADS
                        where d.EGYKLIOS_ID == egykliosId && d.SCHOOL_ID == schoolId
                        orderby d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            AITISI_ID = d.AITISI_ID,
                            STUDENT_AFM = d.STUDENT_AFM,
                            SCHOOL_ID = d.SCHOOL_ID,
                            EGYKLIOS_ID = d.EGYKLIOS_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();
            return (data);
        }

        public void Create(UploadsViewModel data, int egykliosId, string Afm)
        {
            ΧΜ_UPLOADS entity = new ΧΜ_UPLOADS()
            {
                STUDENT_AFM = Afm,
                EGYKLIOS_ID = egykliosId,
                AITISI_ID = Common.GetAitisiIDFromAFM(Afm),
                SCHOOL_ID = Common.GetSchoolIDFromAFM(Afm),
                UPLOAD_NAME = Common.GetStudentNameFromUser(Afm),
                UPLOAD_DATE = data.UPLOAD_DATE,
                UPLOAD_SUMMARY = data.UPLOAD_SUMMARY
            };
            entities.ΧΜ_UPLOADS.Add(entity);
            entities.SaveChanges();

            data.UPLOAD_ID = entity.UPLOAD_ID;
        }

        public void Update(UploadsViewModel data, int egykliosId, string Afm)
        {
            ΧΜ_UPLOADS entity = entities.ΧΜ_UPLOADS.Find(data.UPLOAD_ID);

            entity.STUDENT_AFM = Afm;
            entity.EGYKLIOS_ID = egykliosId;
            entity.AITISI_ID = Common.GetAitisiIDFromAFM(Afm);
            entity.SCHOOL_ID = Common.GetSchoolIDFromAFM(Afm);
            entity.UPLOAD_NAME = Common.GetStudentNameFromUser(Afm);
            entity.UPLOAD_DATE = data.UPLOAD_DATE;
            entity.UPLOAD_SUMMARY = data.UPLOAD_SUMMARY;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UploadsViewModel data)
        {
            ΧΜ_UPLOADS entity = entities.ΧΜ_UPLOADS.Find(data.UPLOAD_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_UPLOADS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public string Delete(int uploadId)
        {
            string msg = "";

            ΧΜ_UPLOADS entity = entities.ΧΜ_UPLOADS.Find(uploadId);
            if (entity != null)
            {
                if (Kerberos.CanDeleteUpload(entity.UPLOAD_ID))
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΧΜ_UPLOADS.Remove(entity);
                    entities.SaveChanges();
                }
                else
                {
                    msg = "Για να γίνει η διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά αρχεία μεταφόρτωσης.";
                }
            }
            return msg;
        }

        public UploadsViewModel Refresh(int entityId)
        {
            return entities.ΧΜ_UPLOADS.Select(d => new UploadsViewModel
            {
                UPLOAD_ID = d.UPLOAD_ID,
                AITISI_ID = d.AITISI_ID,
                STUDENT_AFM = d.STUDENT_AFM,
                SCHOOL_ID = d.SCHOOL_ID,
                EGYKLIOS_ID = d.EGYKLIOS_ID,
                UPLOAD_DATE = d.UPLOAD_DATE,
                UPLOAD_NAME = d.UPLOAD_NAME,
                UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
            }).Where(d => d.UPLOAD_ID.Equals(entityId)).FirstOrDefault();
        }

        public IEnumerable<UploadsFilesViewModel> GetFiles(int uploadId)
        {
            var data = (from d in entities.ΧΜ_UPLOADS_FILES
                        where d.UPLOAD_ID == uploadId
                        orderby d.SCHOOL_USER, d.SCHOOLYEAR_TEXT, d.FILENAME
                        select new UploadsFilesViewModel
                        {
                            ID = d.ID,
                            UPLOAD_ID = d.UPLOAD_ID,
                            SCHOOL_USER = d.SCHOOL_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION
                        }).ToList();
            return (data);
        }

        public void DestroyFile(UploadsFilesViewModel data)
        {
            ΧΜ_UPLOADS_FILES entity = entities.ΧΜ_UPLOADS_FILES.Find(data.ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΧΜ_UPLOADS_FILES.Remove(entity);
                entities.SaveChanges();
            }
        }
    }
}