using System;
using System.Collections.Generic;
using System.Text;
using Xmu.Crms.Services.ViceVersa.Daos;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;

namespace Xmu.Crms.Services.ViceVersa.Services
{
    class GradeService : IGradeService
    {
        private readonly IGradeDao _iGradeDao;
        private readonly IUserService _iUserService;

        public GradeService(IGradeDao iGradeDao, IUserService iUserService)
        {
            _iGradeDao = iGradeDao;
            _iUserService = iUserService;
        }

        public void DeleteStudentScoreGroupByTopicId(long topicId)
        {
            try
            {
                _iGradeDao.DeleteStudentScoreGroupByTopicId(topicId);
            }catch
            {
                throw;
            }
        }

        public void InsertGroupGradeByUserId(long userId, long seminarId, long groupId, long grade)
        {
            try
            {
                //调用UserService中方法
                UserInfo userInfo = _iUserService.GetUserByUserId(userId);

                //调用TopicService中的方法
                SeminarGroupTopic seminarGroupTopic;


                //_iGradeDao.InsertGroupGradeByUserId(userInfo, seminarGroupTopic, (int)grade);
            }catch
            {
                throw;
            }
        }

        public List<int> ListSeminarGradeBySeminarGroupId(long userId, long seminarGroupId)
        {
            throw new NotImplementedException();
        }

        public List<int> ListSeminarGradeByStudentId(long userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroupByGroupId(long seminarGroupId, long grade)
        {
            try
            {
                _iGradeDao.UpdateGroupByGroupId(seminarGroupId, (int)grade);
            }
            catch
            {
                throw;
            }
        }
    }
}
