using System;
using System.Collections.Generic;
using Xmu.Crms.Services.ViceVersa.Daos;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa
{
    class ClassService : IClassService
    {
        //private readonly ISeminarService _seminarService;
        //private readonly ICourseService _courseService;

        private readonly IClassDao _classDao;
        public  ClassService(IClassDao classDao)
        {
            _classDao = classDao;
            //_seminarService = seminarService;
        }

        public void DeleteClassByClassId(long classId)
        {
            //    try
            //    {
            //        _classDao.Delete(classId);
            //        return true;
            //    }
            //    catch(ClassNotFoundException ec)
            //    {
            //        throw ;
            //    }
        }

        public void DeleteClassByCourseId(long courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteClassSelectionByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourseSelectionById(long userId, long classId)
        {
            throw new NotImplementedException();
        }

        public void DeleteScoreRuleById(long classId)
        {
            throw new NotImplementedException();
        }

        public ClassInfo GetCallGroupStatusById(long seminarId)
        {
            throw new NotImplementedException();
        }

        /// 按班级id获取班级详情.
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
            throw new NotImplementedException();
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

        ///// 按课程名称和教师名称获取班级列表.
        public List<ClassInfo> ListClassByName(string courseName, string teacherName)
        {
            throw new NotImplementedException();
            //    List<ClassInfo> classList = null;
            //    if (courseName!=null)
            //    {
            //      //  List<Course>courseList= _courseService.ListCourseByCourseName(courseName);
            //        List<ClassInfo> templist=null;
            //       // if (courseList == null) return null;
            //       // foreach (Course c in courseList)
            //        {
            //            templist = _classDao.QueryAll(c.Id);
            //            classList.AddRange(templist);
            //        }
            //    }
            //    else if(teacherName!=null)
            //    {
            //       // List<Course> courseList = _courseService.ListClassByTeacherName(teacherName);
            //    }
            //    return classList;
        }

        public void UpdateClassByClassId(long classId)
        {
            throw new NotImplementedException();
        }

        public void UpdateScoreRule(long classId, ClassInfo proportions)
        {
            throw new NotImplementedException();
        }


        //public bool DeleteClassByClassId(long classId)
        //{
       
        //}



        //public ClassInfo GetClassByClassId(long classId)
        //{
       
        //}

        //public ClassInfo GetScoreRule(long classId)
        //{
        //    try
        //    {
        //        var classinfo = _classDao.Get(classId);

        //        return classinfo;
        //    }
        //    catch { throw; }
        //}



        //public List<ClassInfo> ListClassByCourseId(long courseId)
        //{
        //    try
        //    {
        //        List<ClassInfo> list = _classDao.QueryAll(courseId);
        //        return list;
        //    }
        //    catch { throw; }
        //}



    }
}
