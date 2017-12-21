using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Exceptions;

namespace Xmu.Crms.Web.ViceVersa
{
    public class ClassDao : BaseDao<ClassInfo>
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
              var classinfo = new ClassInfo { Id = (int)id };
                _db.ClassInfo.Attach(classinfo);
                _db.ClassInfo.Remove(classinfo);
                _db.SaveChanges();

                scope.Complete();
            }
     
        }

        public List<ClassInfo> QueryAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ClassInfo t)
        {
            throw new NotImplementedException();
        }

        public void Update(ClassInfo t)
        {
           
        }

        public ClassInfo Get(long id)
        { 
                var classinfo = _db.ClassInfo.SingleOrDefault(u => u.Id == id);
                if (classinfo == null)
                {
                    throw new ClassNotFoundException();
                }
                return classinfo;
            
        }
    }
}
