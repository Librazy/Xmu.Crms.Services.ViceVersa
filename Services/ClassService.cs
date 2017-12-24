using System;
using System.Collections.Generic;
using System.Transactions;
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
       // private readonly IUserService _userService;

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
                DeleteClassSelectionByClassId(classId);
                //_classDao.Delete(classId);
                    
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
               // _courseService.GetCourseByCourseId(courseId);
                List<ClassInfo> deleteClasses = _classDao.QueryAll(courseId);
                foreach (ClassInfo c in deleteClasses)
                {
                    _classDao.DeleteSelection(0, c.Id);
                    _classDao.Delete(c.Id);
                }
            }catch(CourseNotFoundException e) { throw e; }
        }


        /// 按classId删除CourseSelection表的记录.
        public void DeleteClassSelectionByClassId(long classId)
        {
            _classDao.DeleteSelection(0, classId);
        }


        /// 学生按班级id取消选择班级.
        public void DeleteCourseSelectionById(long userId, long classId)
        {
            try
            {
                //_userService.GetUserByUserId(userId);
                GetClassByClassId(classId);
                _classDao.DeleteSelection(userId, classId);

            }catch(UserNotFoundException eu) { throw eu; }
            catch (ClassNotFoundException ec) { throw ec; }
        }


        /// 按classId删除ScoreRule.
        public void DeleteScoreRuleById(long classId)
        {
            try
            {
                ClassInfo newclass = new ClassInfo() { Id=classId};
                newclass.ReportPercentage = 0;
                newclass.PresentationPercentage = 0;
                newclass.FivePointPercentage = 0;
                newclass.FourPointPercentage = 0;
                newclass.ThreePointPercentage = 0;
                var result = _classDao.Update(newclass);
                _classDao.Update(newclass);
            }
            catch (ClassNotFoundException e) { throw e; }
        }


        /// 老师获取该班级签到、分组状态.
        public Location GetCallStatusById(long seminarId, long classId)
        {
            try
            {
                //_seminarService.GetSeminarBySeminarId(seminarId);
                //_classDao.Get(classId);
                return _classDao.GetLocation(seminarId, classId);
            }
            catch (SeminarNotFoundException e) { throw e; }
        }


        /// 按班级id获取班级详情.
         public ClassInfo GetClassByClassId(long classId)
        {
            try
            {
                var classinfo = _classDao.Get(classId);

                return classinfo;
            }
            catch (ClassNotFoundException e){ throw e; }
        }


        /// 查询评分规则.
        public ClassInfo GetScoreRule(long classId)
        {
            try
            {
                ClassInfo classinfo = _classDao.Get(classId);
               
                return classinfo;
            }
            catch (ClassNotFoundException e) { throw e; }
        }


        /// 新建班级.
        public long InsertClassById(long userId, long courseId, ClassInfo classInfo)
        {
            try
            {
                //_userService.GetUserByUserId(userId);
                //classInfo.Course = _courseService.GetCourseByCourseId(courseId);
                if (classInfo.ReportPercentage < 0 || classInfo.ReportPercentage > 100 ||
                   classInfo.PresentationPercentage < 0 || classInfo.PresentationPercentage > 100 ||
                   classInfo.ReportPercentage + classInfo.PresentationPercentage != 100 ||
                   classInfo.FivePointPercentage < 0 || classInfo.FivePointPercentage > 10 ||
                   classInfo.FourPointPercentage < 0 || classInfo.FourPointPercentage > 10 ||
                   classInfo.ThreePointPercentage < 0 || classInfo.ThreePointPercentage > 10 ||
                   classInfo.FivePointPercentage + classInfo.FourPointPercentage + classInfo.ThreePointPercentage != 10)
                    throw new InvalidOperationException();
                return _classDao.Save(classInfo);    //返回classid

            }catch(UserNotFoundException eu) { throw eu; }
            catch(CourseNotFoundException ec) { throw ec; }
        }


        /// 学生按班级id选择班级.
        public string InsertCourseSelectionById(long userId, long classId)
        {
            try
            {
                var url =" 1";    //？？？？
                //_userService.GetUserByUserId(userId);
                ClassInfo classinfo= GetClassByClassId(classId);

                //找到该班级所属课程下的所有班级
                IList<ClassInfo> classList = ListClassByCourseId(classinfo.Course.Id);
                foreach(ClassInfo c in classList)
                {
                    if (_classDao.GetSelection(userId, c.Id) != 0)//学生已选同课程下其他班级
                        return url;//？？？
                }
               CourseSelection coursesele = new CourseSelection();

                //测试数据
                 UserInfo student = new UserInfo() { Id = 91,Password="123" ,Phone="123"};
                //UserInfo student = _userService.GetUserByUserId(userId);
                coursesele.Student = student;
                coursesele.ClassInfo = classinfo;
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
                if (proportions.ReportPercentage < 0 || proportions.ReportPercentage > 100 ||
                    proportions.PresentationPercentage < 0 || proportions.PresentationPercentage > 100 ||
                    proportions.ReportPercentage + proportions.PresentationPercentage != 100 ||
                    proportions.FivePointPercentage < 0 || proportions.FivePointPercentage > 10 ||
                    proportions.FourPointPercentage < 0 || proportions.FourPointPercentage > 10 ||
                    proportions.ThreePointPercentage < 0 || proportions.ThreePointPercentage > 10 ||
                    proportions.FivePointPercentage + proportions.FourPointPercentage + proportions.ThreePointPercentage != 10)
                    throw new InvalidOperationException();
                var result = _classDao.Update(proportions);//新建班级时已经建了一个空的
                if (result != 0) return -1;
                return classId;
            }
            catch (InvalidOperationException ei) { throw ei; }
            catch (ClassNotFoundException ec) { throw ec; }

        }


        /// 根据课程ID获得班级列表.
        public IList<ClassInfo> ListClassByCourseId(long courseId)
        {
            try
            {
                //_courseService.GetCourseByCourseId(courseId);
                List<ClassInfo> list = _classDao.QueryAll(courseId);
                return list;
            }
            catch(ClassNotFoundException e) { throw e; }
        }




        // 按课程名称和教师名称获取班级列表.
        public IList<ClassInfo> ListClassByName(string courseName, string teacherName)
        {
            try
            {
                long userId = 1;   //jwt？？？？？
                List<ClassInfo> classList = new List<ClassInfo>();
                if (teacherName == null)//根据课程名称查
                {
                    IList<ClassInfo> courseClassList = _courseService.ListClassByCourseName (courseName);
                    classList.AddRange(courseClassList);
                }
                else if (courseName == null)//根据教师姓名查
                {
                    IList<ClassInfo> teacherClassList = _courseService.ListClassByTeacherName(teacherName);
                    classList.AddRange(teacherClassList);
                }
                else  //联合查找
                {
                    IList<ClassInfo> courseClassList = _courseService.ListClassByCourseName(courseName);
                    IList<ClassInfo> teacherClassList = _courseService.ListClassByTeacherName(teacherName);
                    foreach (ClassInfo cc in courseClassList)
                        foreach (ClassInfo ct in teacherClassList)
                            if (cc.Id == ct.Id) { classList.Add(cc); break; }
                }

                //该学生已选班级列表
                List<ClassInfo> studentClass = _classDao.ListClassByUserId(userId);
                foreach (ClassInfo c in classList)
                    foreach (ClassInfo cs in studentClass)
                        if (c.Id == cs.Id) classList.Remove(c);//学生已选的就不列出

                return classList;

            }catch(CourseNotFoundException ec) { throw ec; }
            catch(UserNotFoundException eu) { throw eu; }
        }


        public void UpdateClassByClassId(long classId, ClassInfo newclass)
        {
            try
            {
                var result = _classDao.Update(newclass);  
            }
            catch (ClassNotFoundException e) { throw e; }
        }


        /// 修改评分规则.
        public void UpdateScoreRule(long classId, ClassInfo proportions)
        {
            try
            {
                if (proportions.ReportPercentage < 0 || proportions.ReportPercentage > 100 ||
                    proportions.PresentationPercentage < 0 || proportions.PresentationPercentage > 100 ||
                    proportions.ReportPercentage + proportions.PresentationPercentage != 100 ||
                    proportions.FivePointPercentage < 0 || proportions.FivePointPercentage > 10 ||
                    proportions.FourPointPercentage < 0 || proportions.FourPointPercentage > 10 ||
                    proportions.ThreePointPercentage < 0 || proportions.ThreePointPercentage > 10 ||
                    proportions.FivePointPercentage + proportions.FourPointPercentage + proportions.ThreePointPercentage != 10)
                    throw new InvalidOperationException();
                var result = _classDao.Update(proportions);//新建班级时已经建了一个空的
                //if (result != 0) return -1;
                //return classId;
            }
            catch (InvalidOperationException ei) { throw ei; }
            catch (ClassNotFoundException ec){ throw ec; }
        }
    }
}
