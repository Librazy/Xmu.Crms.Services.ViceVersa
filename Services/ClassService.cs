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
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        private readonly IClassDao _classDao;
        public  ClassService(IClassDao classDao)
        {
            _classDao = classDao;
            //_seminarService = seminarService;
        }


        /// 按班级id删除班级.
        public void DeleteClassByClassId(long classId)
        {
            try
            {
                _classDao.Delete(classId);
            }
            catch (ClassNotFoundException ec)
            {
                throw ec;
            }
        }


        /// 按courseId删除Class.
        public void DeleteClassByCourseId(long courseId)
        {
            try
            {
                _courseService.GetCourseByCourseId(courseId);
                List<ClassInfo> deleteClasses = _classDao.QueryAll(courseId);
                foreach (ClassInfo c in deleteClasses)
                    _classDao.Delete(c.Id);
            }catch(CourseNotFoundException e) { throw e; }
        }


        /// 按classId删除CourseSelection表的一条记录.
        public void DeleteClassSelectionByClassId(long classId)
        {
            throw new NotImplementedException();
        }


        /// 学生按班级id取消选择班级.
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


        /// 查询评分规则.
        public ClassInfo GetScoreRule(long classId)
        {
            try
            {
                ClassInfo classinfo = _classDao.Get(classId);
               
                return classinfo;
            }
            catch { throw; }
        }


        /// 新建班级.  只修改了班级表
        public long InsertClassById(long userId, long courseId)
        {
            ClassInfo newclass = new ClassInfo();
            newclass.Course = _courseService.GetCourseByCourseId(courseId);
            return _classDao.Save(newclass);
        }


        /// 学生按班级id选择班级.
        public string InsertCourseSelectionById(long userId, long classId)
        {
            try
            {
                var url =" 1";    //？？？？
                _userService.GetUserByUserId(userId);
                GetClassByClassId(classId);
                CourseSelection coursesele = new CourseSelection();
                coursesele.Student.Id = userId;
                coursesele.ClassInfo.Id = classId;
                _classDao.InsertSelection(coursesele);
                return url;
            }
            catch (UserNotFoundException eu) { throw eu; }
            catch(ClassNotFoundException ec) { throw ec; }
           
        }


        /// 新增评分规则.  返回班级id？
        public long InsertScoreRule(long classId, ClassInfo proportions)
        {
            try
            {
                var result = _classDao.Update(proportions);//新建班级时已经建了一个空的
                if (result != 0) return -1;
                return classId;
            }
            catch (ClassNotFoundException e) { throw; }

        }


        /// 根据课程ID获得班级列表.
        public List<ClassInfo> ListClassByCourseId(long courseId)
        {
            try
            {
                List<ClassInfo> list = _classDao.QueryAll(courseId);
                return list;
            }
            catch(ClassNotFoundException e) { throw; }
        }




        // 按课程名称和教师名称获取班级列表.
        public List<ClassInfo> ListClassByName(string courseName, string teacherName)
        {
            throw new NotImplementedException();
            List<ClassInfo> classList = null;
            if (courseName != null)
            {
                List<Course>courseList= _courseService.ListCourseByCourseName(courseName);
                List<ClassInfo> templist = null;
                if (courseList == null) return null;
                 foreach (Course c in courseList)
                {
                    templist = _classDao.QueryAll(c.Id);
                    classList.AddRange(templist);
                }
            }
            else if (teacherName != null)
            {
                long userId = 1;   //jwt？？？？？
                 List<ClassInfo> teacherClassList = _courseService.ListClassByTeacherName(teacherName);
                List<ClassInfo> studentClassList = _courseService.ListClassByUserId(userId);
                foreach (ClassInfo ct in teacherClassList)
                {
                    foreach (ClassInfo cs in studentClassList)
                        if (ct.Id == cs.Id) break;
                    classList.Add(ct);
                }
                            
            }
            return classList;
        }


        /// 按班级id和班级修改班级信息.
        public void UpdateClassByClassId(long classId)
        {
            //try
            //{
            //    var result = _classDao.Update(proportions);
            //}
            //catch (ClassNotFoundException e) { throw; }
        }


        /// 修改评分规则.
        public void UpdateScoreRule(long classId, ClassInfo proportions)
        {
            try
            {
                var result = _classDao.Update(proportions);
            }
            catch (ClassNotFoundException e) { throw; }
        }

    }
}
