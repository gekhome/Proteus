using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class UserSchoolService : IUserSchoolService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public UserSchoolService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<UserSchoolViewModel> Read()
        {
            var data = (from d in entities.USER_SCHOOLS
                        orderby d.USERNAME
                        select new UserSchoolViewModel
                        {
                            USER_ID = d.USER_ID,
                            USERNAME = d.USERNAME,
                            PASSWORD = d.PASSWORD,
                            USER_SCHOOLID = d.USER_SCHOOLID ?? 0,
                            ISACTIVE = d.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public void Create(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = new USER_SCHOOLS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                USER_SCHOOLID = data.USER_SCHOOLID,
                ISACTIVE = data.ISACTIVE,
            };
            entities.USER_SCHOOLS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = entities.USER_SCHOOLS.Find(data.USER_ID);

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.USER_SCHOOLID = data.USER_SCHOOLID;
            entity.ISACTIVE = data.ISACTIVE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = entities.USER_SCHOOLS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_SCHOOLS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}