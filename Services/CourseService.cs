using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xmu.Crms.Services.ViceVersa.Daos;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa.Services
{
    class CourseService : ICourseService
    {
        private readonly ICourseDao _iCourseDao;
        private readonly ISeminarService _iSeminarService;
        private readonly IClassService _iClassService;
        private readonly IUserService _iUserService;

        public CourseService(ICourseDao iCourseDao, ISeminarService iSeminarService, IClassService iClassService, IUserService iUserService)
        {
            _iCourseDao = iCourseDao;
            _iSeminarService = iSeminarService;
            _iClassService = iClassService;
            _iUserService = iUserService;
        }

        public void DeleteCourseByCourseId(long courseId)
        {
            try
            {
                //事务
                using (var scope = new TransactionScope())
                {
                    //删除course下的class
                    _iClassService.DeleteClassByCourseId(courseId);
                    //删除course下的seminar
                    _iSeminarService.DeleteSeminarByCourseId(courseId);
                    //删除course
                    _iCourseDao.DeleteCourseByCourseId(courseId);
                    scope.Complete();
                }
            }
            catch
            {
                throw;
            }
        }

        public Course GetCourseByCourseId(long courseId)
        {
            try
            {
                Course course = _iCourseDao.GetCourseByCourseId(courseId);
                //没查到该门课
                if (course == null)
                {
                    throw new CourseNotFoundException();
                }
                return course;
            }
            catch
            {
                throw;
            }
        }

        public long InsertCourseByUserId(long userId, Course course)
        {
            try
            {
                long courseId = _iCourseDao.InsertCourseByUserId(userId, course);
                return courseId;
            }catch
            {
                throw;
            }
        }

        public List<ClassInfo> ListClassByCourseName(string courseName)
        {
            try
            {
                //根据课程名获得对应的课程列表
                List<Course> courseList = ListCourseByCourseName(courseName);
                //根据课程id获得该课程下的班级
                List<ClassInfo> classList = new List<ClassInfo>();
                foreach (var i in courseList)
                     classList.AddRange( _iClassService.ListClassByCourseId(i.Id));
                return classList;
            }catch
            {
                throw;
            }
        }

        public List<ClassInfo> ListClassByTeacherName(string teacherName)
        {
            try
            {
                List<long> idList = _iUserService.ListUserIdByUserName(teacherName);
                if (idList == null || idList.Count == 0)
                    return null;
                List<ClassInfo> classList = new List<ClassInfo>();
                foreach (var i in idList)
                    classList.AddRange(ListClassByUserId(i));
                return classList;
            }catch
            {
                throw;
            }
        }

        public List<ClassInfo> ListClassByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public List<Course> ListCourseByCourseName(string courseName)
        {
            try
            {
                List<Course> courseList = _iCourseDao.ListCourseByCourseName(courseName);
                if (courseList == null || courseList.Count == 0)
                {
                    //throw new CourseNotFoundException();
                }
                return courseList;
            }catch
            {
                throw;
            }
        }

        public List<Course> ListCourseByUserId(long userId)
        {
            try
            {
                List<Course> courseList = _iCourseDao.ListCourseByUserId(userId);
                //查不到课程
                if (courseList==null || courseList.Count==0)
                    throw new CourseNotFoundException();
                return courseList;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCourseByCourseId(long courseId, Course course)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _iCourseDao.UpdateCourseByCourseId(courseId, course);
                    scope.Complete();
                }
            }catch
            {
                throw;
            }
        }
    }
}
