using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;

namespace Xmu.Crms.Services.ViceVersa
{
    class ClassDao : IClassDao
    {
        private readonly CrmsContext _db;

        public ClassDao(CrmsContext db)
        {
            _db = db;
        }

        public void Delete(long id)
        {
            using (var scope = new TransactionScope())
            {
                var classinfo = new ClassInfo { Id = id };
                _db.ClassInfo.Attach(classinfo);
                _db.ClassInfo.Remove(classinfo);
                _db.SaveChanges();

                scope.Complete();
            }
        }

        public ClassInfo Get(long id)
        {
            using (var scope = new TransactionScope())
            {
                var classinfo = _db.ClassInfo.SingleOrDefault(u => u.Id == id);
                if (classinfo == null)
                {
                    throw new ClassNotFoundException();
                }
                return classinfo;
            }
        }

        public List<ClassInfo> QueryAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ClassInfo t)
        {
            using (var scope = new TransactionScope())
            {

                //var classinfo = new ClassInfo(t);
                _db.ClassInfo.Add(t);

                _db.SaveChanges();

                scope.Complete();
            }
        }

        public void Update(ClassInfo t)
        {
            using (var scope = new TransactionScope())
            {

                //var classinfo = new ClassInfo(t);
                //将实体附加到对象管理器中
                _db.ClassInfo.Attach(t);
                //把当前实体的状态改为Modified
                _db.Entry(t).State = EntityState.Modified;

                _db.SaveChanges();

                scope.Complete();
            }
        }
    }
}
