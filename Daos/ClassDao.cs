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

            ClassInfo classinfo = _db.ClassInfo.Where(u => u.Id == id).SingleOrDefault<ClassInfo>();
               if (classinfo == null)
                {
                    throw new ClassNotFoundException();
                }
                return classinfo;
           
        }


        //根据课程id列出所有班级
        public List<ClassInfo> QueryAll(long id)  
        {

            List<ClassInfo>  list=_db.ClassInfo.Where(u => u.Course.Id == id).ToList<ClassInfo>();
            if (list == null)
            {
                throw new ClassNotFoundException();
            }
            return list;
        }


        //添加班级返回id
        public long Save(ClassInfo t)
        {
            using (var scope = new TransactionScope())
            {
                
                _db.ClassInfo.Add(t);
                
                _db.SaveChanges();

                scope.Complete();
                return t.Id;
            }
            
        }

        //添加学生选课表返回id
        public long SaveSelection(CourseSelection t)
        {
            using (var scope = new TransactionScope())
            {

                _db.CourseSelection.Add(t);

                _db.SaveChanges();

                scope.Complete();
                return t.Id;
            }

        }

        public int Update(ClassInfo t)
        {
            using (var scope = new TransactionScope())
            {

                ClassInfo c = _db.ClassInfo.Where(u => u.Id == t.Id).SingleOrDefault<ClassInfo>();
                if (c == null) throw new ClassNotFoundException();
                c.Name = t.Name;
                c.Course = t.Course;
                c.Site = t.Site;
                c.ClassTime = t.ClassTime;
                c.ReportPercentage = t.ReportPercentage;
                c.PresentationPercentage = t.PresentationPercentage;
                c.FivePointPercentage = t.FivePointPercentage;
                c.FourPointPercentage = t.FourPointPercentage;
                c.ThreePointPercentage = t.ThreePointPercentage;
                
                _db.SaveChanges();

                scope.Complete();
                return 0;
            }
        }
    }
}
