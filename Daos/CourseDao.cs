using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Xmu.Crms.Services.ViceVersa.Daos
{
    public class CourseDao : ICourseDao
    {
        private readonly CrmsContext _db;

        public CourseDao(CrmsContext db)
        {
            _db = db;
        }

        public void DeleteCourseByCourseId(long courseId)
        {
            try
            {
                Course course = new Course { Id = courseId };
                //将实体附加到对象管理器中
                _db.Course.Attach(course);
                //删除
                if (_db.Course.Remove(course) == null)
                    throw new CourseNotFoundException();
                _db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Course GetCourseByCourseId(BigInteger courseId)
        {
            try
            {
                Course course = _db.Course.SingleOrDefault(c => c.Id == courseId);
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
                _db.Course.Add(course);
                _db.SaveChanges();
                return course.Id;   //SaveChanges后Id变成了数据库里创建完course后自增的那个Id
            }
            catch
            {
                throw;
            }
        }

        public List<ClassInfo> ListClassByUserId(BigInteger userId)
        {
            throw new NotImplementedException();
        }

        public List<Course> ListCourseByCourseName(string courseName)
        {
            try
            {
                List<Course> courseList = _db.Course.Include(c => c.Teacher).Where(c => c.Name == courseName).ToList();
                return courseList;
            }catch
            {
                throw;
            }
        }

        public List<Course> ListCourseByUserId(BigInteger userId)
        {
            List<Course> courseList = null;
            try
            {
                //获得对应的选课信息
                var selectionList = _db.CourseSelection.Include(u=>u.ClassInfo).Include(u=>u.ClassInfo.Course).Where(u => u.Student.Id == userId).ToList();
                if (selectionList != null)
                {
                    courseList = new List<Course>();
                    foreach (var i in selectionList)
                    {
                        courseList.Add(i.ClassInfo.Course);
                    }
                }
            }
            catch
            {
                throw;
            }
            return courseList;
        }

        public void UpdateCourseByCourseId(long courseId, Course course)
        {
            try
            {
                Course cour = _db.Course.SingleOrDefault(c => c.Id == courseId);
                //如果找不到该课程
                if (cour == null)
                {
                    throw new CourseNotFoundException();
                }
                //更新该课程(更新界面上能够更改的内容)
                cour.Name = course.Name;
                cour.StartDate = course.StartDate;
                cour.EndDate = course.EndDate;
                cour.Description = course.Description;
                cour.ThreePointPercentage = course.ThreePointPercentage;
                cour.FourPointPercentage = course.FourPointPercentage;
                cour.FivePointPercentage = course.FivePointPercentage;
                cour.ReportPercentage = course.ReportPercentage;
                cour.PresentationPercentage = course.PresentationPercentage;
                _db.SaveChanges();
            }catch
            {
                throw;
            }
        }
    }
}
