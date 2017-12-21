using System;
using System.Collections.Generic;
using System.Text;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;
using Xmu.Crms.Web.ViceVersa;
namespace Xmu.Crms.Services.ViceVersa
{
    class ClassService : IClassService
    {
        private readonly ISeminarService _seminarService;

        private readonly ClassDao _classDao;
        public  ClassService(ClassDao classDao,ISeminarService seminarService)
        {
            _classDao = classDao;
            _seminarService = seminarService;
        }
        

        public bool DeleteClassByClassId(long classId)
        {
            _classDao.Delete((int)classId);
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
            // 调用Entity framework
            var classinfo = _classDao.Get(classId);
            
            return classinfo;
        }

        public ClassInfo GetScoreRule(long classId)
        {
            var classinfo = _classDao.Get(classId);

            return classinfo;
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
            throw new NotImplementedException();
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
