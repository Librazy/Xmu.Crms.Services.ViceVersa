using System;
using System.Collections.Generic;
using Xmu.Crms.Services.ViceVersa.Daos;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa
{
    class ClassService : IClassService
    {
        //private readonly ISeminarService _seminarService;

        private readonly IClassDao _classDao;
        public  ClassService(IClassDao classDao)
        {
            _classDao = classDao;
            //_seminarService = seminarService;
        }
        

        public bool DeleteClassByClassId(long classId)
        {
            _classDao.Delete(classId);
            return true;
        }

        public bool DeleteClassByCourseId(long courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteClassSelectionByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCourseSelectionById(long userId, long classId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteScoreRuleById(long classId)
        {
            throw new NotImplementedException();
        }

        public ClassInfo GetCallGroupStatusById(long seminarId)
        {
            throw new NotImplementedException();
        }

        public ClassInfo GetClassByClassId(long classId)
        {
            try
            {
                var classinfo = _classDao.Get(classId);

                return classinfo;
            }
            catch { throw; }
        }

        public ClassInfo GetScoreRule(long classId)
        {
            try
            {
                var classinfo = _classDao.Get(classId);

                return classinfo;
            }
            catch { throw; }
        }

        public long InsertClassById(long userId, long courseId)
        {
            throw new NotImplementedException();
        }

        public string InsertCourseSelectionById(long userId, long classId)
        {
            throw new NotImplementedException();
        }

        public long InsertScoreRule(long classId, ClassInfo proportions)
        {
            try
            {
                _classDao.Update(proportions);
                return 1;    //???
            }
            catch { throw; }
        }

        public List<ClassInfo> ListClassByCourseId(long courseId)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> ListClassByName(string courseName, string teacherName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClassByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateScoreRule(long classId, ClassInfo proportions)
        {
            throw new NotImplementedException();
        }
    }
}
