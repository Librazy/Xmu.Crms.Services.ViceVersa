using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text;
using Xmu.Crms.Shared.Models;


namespace Xmu.Crms.Services.ViceVersa.Daos
{
    public class CourseDao : ICourseDao
    {
        private readonly CrmsContext _db;

        public CourseDao(CrmsContext db)
        {
            _db = db;
        }

        public bool DeleteCourseByCourseId(BigInteger courseId)
        {
            throw new NotImplementedException();
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

        public BigInteger InsertCourseByUserId(BigInteger userId, Course course)
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

        public List<ClassInfo> ListClassByCourseName(string courseName)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> ListClassByTeacherName(string teacherName)
        {
            throw new NotImplementedException();
        }

        public List<ClassInfo> ListClassByUserId(BigInteger userId)
        {
            throw new NotImplementedException();
        }

        public List<Course> ListCourseByCourseName(string courseName)
        {
            throw new NotImplementedException();
        }

        public List<Course> ListCourseByUserId(BigInteger userId)
        {
            try
            {
                List<Course> courseList = null;
                //获得对应的选课信息
                var selectionList = _db.CourseSelection.Where(u => u.Student.Id == userId).ToList();
                if (selectionList != null)
                {
                    courseList = new List<Course>();
                    foreach (var i in selectionList)
                    {
                        courseList.Add(i.ClassInfo.Course);
                    }
                }
                return courseList;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCourseByCourseId(BigInteger courseId, Course course)
        {
            throw new NotImplementedException();
        }
    }
}
