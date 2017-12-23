using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa
{
    public class UserService : IUserService
    {
        private readonly CrmsContext _db;
        private readonly ISchoolService _schoolService;

        // 在构造函数里添加依赖的Service（参考模块标准组的类图）
        public UserService(CrmsContext db, ISchoolService schoolService)
        {
            _db = db;
            _schoolService = schoolService;
        }

        public void InsertAttendanceById(long classId, long seminarId, long userId, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public IList<Attendance> ListAttendanceById(long classId, long seminarId)
        {
            throw new NotImplementedException();
        }

        public UserInfo SignUpPhone(UserInfo user)
        {
            var us = _db.UserInfo.SingleOrDefault(u => u.Phone == user.Phone);
            if (us == null)
            {
                throw new UserNotFoundException();
            }
            if (user.Password != us.Password) // 千万不要真的用明文存储密码！
            {
                throw new PasswordErrorException();
            }
            return us;
        }

        public void DeleteTeacherAccount(long userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudentAccount(long userId)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserByUserId(long id)
        {
            // 调用Entity framework
            var user = _db.UserInfo.Include(u => u.School).SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            // 调用依赖的 Serivce
            _schoolService.GetSchoolBySchoolId(user.School.Id);
            return user;
        }

        public IList<long> ListUserIdByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserByUserId(long userId, UserInfo user)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> ListUserByClassId(long classId, string numBeginWith, string nameBeginWith)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> ListUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> ListPresentStudent(long seminarId, long classId)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> ListAbsenceStudent(long seminarId, long classId)
        {
            throw new NotImplementedException();
        }

        public IList<Course> ListCourseByTeacherName(string teacherName)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> ListLateStudent(long seminarId, long classId)
        {
            throw new NotImplementedException();
        }
    }
}
