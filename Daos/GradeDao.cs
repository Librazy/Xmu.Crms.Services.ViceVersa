using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;

namespace Xmu.Crms.Services.ViceVersa.Daos
{
    class GradeDao : IGradeDao
    {
        private readonly CrmsContext _db;

        // 在构造函数里添加依赖的Service（参考模块标准组的类图）
        public GradeDao(CrmsContext db)
        {
            _db = db;
        }

        public void DeleteStudentScoreGroupByTopicId(long topicId)
        {
            throw new NotImplementedException();
        }

        public void InsertGroupGradeByUserId(long userId, long seminarId, long groupId, long grade)
        {
            throw new NotImplementedException();
        }

        public List<long> ListSeminarGradeBySeminarGroupId(long userId, long seminarGroupId)
        {
            throw new NotImplementedException();
        }

        public List<long> ListSeminarGradeByStudentId(long userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroupByGroupId(long seminarGroupId, int grade)
        {
            try
            {
                SeminarGroup seminarGroup = _db.SeminarGroup.SingleOrDefault(s => s.Id == seminarGroupId);
                //如果找不到该组
                if (seminarGroup == null)
                {
                    throw new GroupNotFoundException();
                }
                //更新报告分
                seminarGroup.ReportGrade = grade;
                _db.SaveChanges();
            }catch
            {
                throw;
            }
        }
    }
}
