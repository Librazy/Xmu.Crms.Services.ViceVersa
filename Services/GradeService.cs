using System;
using System.Collections.Generic;
using System.Text;
using Xmu.Crms.Services.ViceVersa.Daos;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa.Services
{
    class GradeService : IGradeService
    {
        private readonly IGradeDao _iGradeDao;

        public GradeService(IGradeDao iGradeDao)
        {
            _iGradeDao = iGradeDao;
        }

        public bool DeleteStudentScoreGroupByTopicId(long topicId)
        {
            throw new NotImplementedException();
        }

        public bool InsertGroupGradeByUserId(long userId, long seminarId, long groupId, long grade)
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

        public bool UpdateGroupByGroupId(long seminarGroupId, long grade)
        {
            try
            {
                _iGradeDao.UpdateGroupByGroupId(seminarGroupId, (int)grade);
                return true;
            }catch
            {
                throw;
            }
            
        }
    }
}
