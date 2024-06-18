using System;
using System.Collections.Generic;
using System.Linq;
using Proteus.BPM;
using Proteus.DAL;
using Proteus.Models;
using System.Data.Entity;

namespace Proteus.Services
{
    public class UserAdminService : IUserAdminService, IDisposable
    {
        private readonly ProteusDBEntities entities;

        public UserAdminService(ProteusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<UserAdminViewModel> Read()
        {
            var data = (from a in entities.USER_ADMINS
                        orderby a.FULLNAME
                        select new UserAdminViewModel
                        {
                            USER_ID = a.USER_ID,
                            USERNAME = a.USERNAME,
                            PASSWORD = a.PASSWORD,
                            ADMIN_LEVEL = a.ADMIN_LEVEL ?? 2,
                            FULLNAME = a.FULLNAME,
                            CREATEDATE = a.CREATEDATE,
                            ISACTIVE = a.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public void Create(UserAdminViewModel data)
        {
            USER_ADMINS entity = new USER_ADMINS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                FULLNAME = data.FULLNAME,
                ADMIN_LEVEL = data.ADMIN_LEVEL,
                ISACTIVE = data.ISACTIVE,
                CREATEDATE = data.CREATEDATE
            };
            entities.USER_ADMINS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            entity.USER_ID = data.USER_ID;
            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_ADMINS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}